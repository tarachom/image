using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ImageLibrary;

namespace ImageForms
{
    public partial class FormPicturesElementUnionTrack : Form
    {
        private Pictures m_PictureElement;

        public Pictures PictureElement
        {
            get
            {
                return m_PictureElement;
            }

            set
            {
                m_PictureElement = value;
            }
        }

        public FormPicturesElementUnionTrack()
        {
            InitializeComponent();
        }

        // Обработчик ошибок
        private static void ErrorMessage(object sender, DataBaseEventArgs e)
        {
            MessageBox.Show("Data Error: " + e.Message + ": " + e.DescriptionMessage, "Помилка");
        }

        #region РЕКУРСИВНА БУДОВА ДЕРЕВА
        //----------------- РЕКУРСИВНА БУДОВА ДЕРЕВА --------------------->

        private void Func_ImageChildNode(Image ImageChildElement, TreeNode rootNode)
        {
            TreeNode ImageContextNode = rootNode.Nodes.Add(
                    ImageChildElement.ID.ToString(),
                    ImageChildElement.Name + " <" + ImageChildElement.Context.Name + ">");

            if (ImageChildElement.LinkContext != null)
            {
                TreeNode ImageChildLinkNode = ImageContextNode.Nodes.Add(
                    "LinkContext_" + ImageChildElement.ID.ToString(),
                    "<Link>: <" + ImageChildElement.LinkContext.Name + ">");

                ImageChildLinkNode.ForeColor = System.Drawing.Color.Blue;

                List<Image> imageList = Program.GlobalKernel.LoadAllImageByContext(ImageChildElement.LinkContext.ID);

                if (imageList != null)
                    foreach (Image itemImage in imageList)
                    {
                        TreeNode ImageChildImageNode = ImageChildLinkNode.Nodes.Add(
                       "Node_" + itemImage.ID.ToString(),
                        "<N>: { " + itemImage.Name + " }");

                        Func_ImageChildNode(itemImage, ImageChildImageNode);
                    }

                ImageChildLinkNode.Expand();
            }

            foreach (ImageBase IngradientItem in ImageChildElement.Ingradienty)
            {
                TreeNode ImageChildNode = rootNode.Nodes.Add(
                    IngradientItem.ID.ToString(),
                    "<Ingr>: " + IngradientItem.ToString());

                Image ImageChildNextElement = Program.GlobalKernel.GetImageByID(IngradientItem.ID);

                Func_ImageChildNode(ImageChildNextElement, ImageChildNode);
            }

            ImageContextNode.Expand();
        }

        private void Func_PictureChildNode(Pictures pictureChildElement, TreeNode rootNode)
        {
            if (pictureChildElement.PicturesPictureChild.Count > 0)
                foreach (PicturesBase pictureChild in pictureChildElement.PicturesPictureChild)
                {
                    Pictures pictureChildNextElement = Program.GlobalKernel.GetPicturesByID(pictureChild.ID);

                    if (pictureChildNextElement.PicturesImageChild.Count > 0)
                        foreach (ImageBase ImageChild in pictureChildNextElement.PicturesImageChild)
                        {
                            Image ImageChildNextElement = Program.GlobalKernel.GetImageByID(ImageChild.ID);
                            Func_ImageChildNode(ImageChildNextElement, rootNode);
                        }
                }
        }

        #endregion


        private void FormPicturesElementUnionTrack_Load(object sender, EventArgs e)
        {
            //Program.GlobalKernel.Data.DataBaseExceptionEvent += ErrorMessage;

            if (PictureElement != null)
            {
                TreeNode rootNode = treeViewPictures.Nodes.Add("root", PictureElement.Name);
                Func_PictureChildNode(PictureElement, rootNode);

                rootNode.Expand();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCreateResult_Click(object sender, EventArgs e)
        {
            TreeNode rootNode = treeViewPictures.Nodes["root"];

            treeViewResult.Nodes.Clear();
            TreeNode rootNodeResult = treeViewResult.Nodes.Add("root", PictureElement.Name);

            foreach (TreeNode treeNode in rootNode.Nodes)
                if (treeNode.Checked)
                    rootNodeResult.Nodes.Add(treeNode.Name, treeNode.Text);

            rootNodeResult.Expand();
        }

        private void FindSelectNodeForDeleteInTreeViewResult(List<TreeNode> listForDeleteNode, TreeNode rootNode)
        {
            foreach (TreeNode treeNode in rootNode.Nodes)
            {
                if (treeNode.Checked)
                    listForDeleteNode.Add(treeNode);
                else
                    FindSelectNodeForDeleteInTreeViewResult(listForDeleteNode, treeNode);
            }
        }

        private void buttonDeleteSelectForResult_Click(object sender, EventArgs e)
        {
            TreeNode rootNode = treeViewResult.Nodes["root"];

            if (rootNode != null)
            {
                List<TreeNode> listForDeleteNode = new List<TreeNode>();
                FindSelectNodeForDeleteInTreeViewResult(listForDeleteNode, rootNode);

                foreach (TreeNode treeNode in listForDeleteNode)
                    treeNode.Remove();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            TreeNode rootNode = treeViewResult.Nodes["root"];

            if (rootNode != null)
            {
                //Глобальний контекст UNION
                ImageContext ContextUnion = Program.GlobalKernel.GetOrCreateNewContext("UNION");

                //Контекст для нового образу
                ImageContext ContextLink = Program.GlobalKernel.GetOrCreateNewContext(PictureElement.Name);

                Image imageInUnion = Program.GlobalKernel.GetImageByName(PictureElement.Name, ContextUnion.ID);

                if (imageInUnion == null)
                {
                    imageInUnion = new Image();
                    imageInUnion.Name = PictureElement.Name;
                    imageInUnion.Context = ContextUnion;
                    imageInUnion.LinkContext = ContextLink;
                    imageInUnion.Atributes.Add(ImageAtribute.Abstract);

                    if (!Program.GlobalKernel.InsertImage(imageInUnion))
                    {
                        MessageBox.Show("Невдалось записати новий образ");
                        return;
                    }
                }
                else
                {
                    // !!! Можна переписати колекції
                }

                foreach (TreeNode treeNode in rootNode.Nodes)
                {
                    //Образ який був прототипом для копіювання
                    Image imageInTreeNode = Program.GlobalKernel.GetImageByID(long.Parse(treeNode.Name));

                    if (imageInTreeNode == null)
                    {
                        MessageBox.Show("Невдалось знайти образ по ІД: " + treeNode.Name);
                        return;
                    }

                    //Шукаємо образ
                    Image imageInContextLink = Program.GlobalKernel.GetImageByName(imageInTreeNode.Name, ContextLink.ID);

                    if (imageInContextLink == null)
                    {
                        imageInContextLink = new Image();
                        imageInContextLink.Name = imageInTreeNode.Name;
                        imageInContextLink.Context = ContextLink;
                        imageInContextLink.LinkContext = imageInTreeNode.LinkContext;

                        // !!! можна перекопіювати колеції з imageInTreeNode

                        if (!Program.GlobalKernel.InsertImage(imageInContextLink))
                        {
                            MessageBox.Show("Невдалось записати новий образ");
                            return;
                        }
                    }
                }

                //Добавити в PicturesImageChild
                // !!! Перевірити чи нема дублів. 
                // Наразі просто очищаю колекцію
                PictureElement.PicturesImageChild.Clear();
                PictureElement.PicturesImageChild.Add(imageInUnion);
                Program.GlobalKernel.UpdatePicture(PictureElement);

                this.Close();
            }
        }
    }
}
