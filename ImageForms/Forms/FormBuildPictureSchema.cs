using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ImageLibrary;

namespace ImageForms
{
    public partial class FormBuildPictureSchema : Form
    {
        private Pictures m_PictureElementItem;

        /// <summary>
        /// Малюнок для якого треба побудувати схему
        /// </summary>
        public Pictures PictureElementItem
        {
            get
            {
                return m_PictureElementItem;
            }

            set
            {
                m_PictureElementItem = value;
            }
        }

        public FormBuildPictureSchema()
        {
            InitializeComponent();
        }

        private int counter = 0;

        private void Func_ImageChildNode(Image ImageChildElement, TreeNode rootNode)
        {
            counter++;

            if (counter > 30)
                return;

            if (ImageChildElement.LinkContext != null)
            {
                //rootNode.Text += " --> " + ImageChildElement.LinkContext.Name;

                TreeNode ImageChildLinkNode = rootNode.Nodes.Add(
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
            }

            foreach (ImageBase IngradientItem in ImageChildElement.Ingradienty)
            {
                TreeNode ImageChildNode = rootNode.Nodes.Add(
                    IngradientItem.ID.ToString(),
                    "<Ingr>: " + IngradientItem.ToString());

                Image ImageChildNextElement = Program.GlobalKernel.GetImageByID(IngradientItem.ID);

                Func_ImageChildNode(ImageChildNextElement, ImageChildNode);
            }

            counter--;
        }

        private void Func_PictureChildNode(Pictures pictureChildElement, TreeNode rootNode)
        {
            counter++;

            if (counter > 30)
                return;

            if (pictureChildElement.PicturesPictureChild.Count > 0)
            {
                TreeNode pictureChildRootNode = rootNode.Nodes.Add(
                    "root_picture_" + pictureChildElement.ID.ToString(), "[Pictures]");

                foreach (PicturesBase pictureChild in pictureChildElement.PicturesPictureChild)
                {
                    TreeNode pictureChildNode = pictureChildRootNode.Nodes.Add(
                        pictureChild.ID.ToString(), "<PicChild>: " + pictureChild.Name);

                    Pictures pictureChildNextElement = Program.GlobalKernel.GetPicturesByID(pictureChild.ID);

                    Func_PictureChildNode(pictureChildNextElement, pictureChildNode);
                }
            }

            if (pictureChildElement.PicturesImageChild.Count > 0)
            {
                TreeNode imageChildRootNode = rootNode.Nodes.Add(
                    "root_image_" + pictureChildElement.ID.ToString(), "[Image]");

                foreach (ImageBase ImageChild in pictureChildElement.PicturesImageChild)
                {
                    TreeNode imageChildNode = imageChildRootNode.Nodes.Add(
                        ImageChild.ID.ToString(), "<ImgChild>: " + ImageChild.ToString());

                    Image ImageChildNextElement = Program.GlobalKernel.GetImageByID(ImageChild.ID);

                    Func_ImageChildNode(ImageChildNextElement, imageChildNode);
                }
            }

            counter--;
        }

        private void FormBuildPictureSchema_Load(object sender, EventArgs e)
        {
            TreeNode rootNode = treeViewSchema.Nodes.Add("root", PictureElementItem.Name);

            Func_PictureChildNode(PictureElementItem, rootNode);

            rootNode.ExpandAll();
        }
    }
}
