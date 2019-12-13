using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ImageLibrary;

namespace ImageForms
{
    public partial class FormFirst : Form
    {
        public FormFirst()
        {
            InitializeComponent();
        }

        #region Обработчики собитий бази даних

        // Обработчик ошибок
        private static void ErrorMessage(object sender, DataBaseEventArgs e)
        {
            //MessageBox.Show("Форма FormFirst: " + e.Message + ": " + e.DescriptionMessage, "Помилка");
        }

        // Добавлений новий образ
        private void AddedNewImageEventMessage(object sender, DataBaseEventArgs e)
        {

        }

        // Видалений образ
        private void DeleteImageEventMessage(object sender, DataBaseEventArgs e)
        {

        }

        // Обновлений образ
        private void UpdateImageEventMessage(object sender, DataBaseEventArgs e)
        {

        }

        #endregion 

        /// <summary>
        /// Функція заповнює дерево образами
        /// Використовується тілький базовий образ, без колекцій
        /// </summary>
        private void ReloadTreeView()
        {
            //Загрузка списку контекстів
            List<ImageContext> imageContextList = new List<ImageContext>();
            Program.GlobalKernel.LoadAllContextList(imageContextList);

            foreach (ImageContext imageContextItem in imageContextList)
            {
                string keyContextNode = "context_" + imageContextItem.ID.ToString();

                TreeNode contextNode = null;

                if (treeViewImages.Nodes.ContainsKey(keyContextNode))
                    contextNode = treeViewImages.Nodes[keyContextNode];
                else
                    contextNode = treeViewImages.Nodes.Add(keyContextNode, imageContextItem.ToString());

                List<ImageBase> listImageBase = new List<ImageBase>();
                Program.GlobalKernel.LoadAllImageBase(listImageBase, imageContextItem.ID);

                foreach (ImageBase imageBase in listImageBase)
                {
                    string keyImageBaseNode = imageBase.ID.ToString();

                    if (contextNode.Nodes.ContainsKey(keyImageBaseNode))
                        contextNode.Nodes[keyImageBaseNode].Text = imageBase.Name;
                    else
                        contextNode.Nodes.Add(keyImageBaseNode, imageBase.Name);
                }

                //contextNode.Expand();
            }
        }

        private void ReloadEventJournal()
        {
            List<EventJournalMessage> messList = new List<EventJournalMessage>();
            Program.GlobalKernel.LoadAllEventJournal(messList);

            Program.Global_NormalizeDataGridRows(messList.Count, dataGridViewEventMessage.Rows);

            int index_row = 0;

            foreach (EventJournalMessage eventMessage in messList)
            {
                DataGridViewRow row = dataGridViewEventMessage.Rows[index_row];
                row.Cells["EventJournal_ID"].Value = eventMessage.ID;
                row.Cells["EventJournal_Type"].Value = eventMessage.EventType;
                row.Cells["EventJournal_DataTime"].Value = eventMessage.EventDataTime;
                row.Cells["EventJournal_Message"].Value = eventMessage.Message;
                row.Cells["EventJournal_Description"].Value = eventMessage.Description;

                index_row++;
            }
        }

        private void ReloadTreeViewAbstractImages(string searchText = "")
        {
            List<ImageBase> AbstractImageBase = Program.GlobalKernel.SearchImageBaseByName(searchText + "%", 1);

            treeViewAbstarctImages.Nodes.Clear();

            foreach (ImageBase itemImageBase in AbstractImageBase)
            {
                TreeNode newNode = treeViewAbstarctImages.Nodes.Add(itemImageBase.ID.ToString(), itemImageBase.Name);
                newNode.Tag = itemImageBase;
            }
        }

        private void treeViewAbstarctImages_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode SelectNode = e.Node;

            if (SelectNode != null)
            {
                if (SelectNode.Tag != null)
                {
                    ImageBase imageBaseTag = (ImageBase)SelectNode.Tag;
                    ReloadTreeViewImageInfo(imageBaseTag);
                }
            }
        }

        private void ReloadTreeViewImageInfo(ImageBase imageBaseItem)
        {
            treeViewImageInfo.Nodes.Clear();

            Image imageItem = Program.GlobalKernel.GetImageByID(imageBaseItem.ID);

            TreeNode ItemRootNode = treeViewImageInfo.Nodes.Add("", imageItem.Name);

            //Ссилка контекст
            if (imageItem.LinkContext != null)
            {
                List<Image> imageLinkContextList = Program.GlobalKernel.LoadAllImageByContext(imageItem.LinkContext.ID);

                if (imageLinkContextList.Count > 0)
                {
                    TreeNode LinkContextGroup = ItemRootNode.Nodes.Add("", "Ссилки:");

                    foreach (Image imageLinkContext in imageLinkContextList)
                    {
                        TreeNode LinkContextNode = LinkContextGroup.Nodes.Add(imageLinkContext.ID.ToString(), imageLinkContext.Name);

                        // LoadTreeLinksNode(LinkContextNode, imageLinkContext);
                    }

                    LinkContextGroup.Expand();
                }
            }

            //Характеристики
            if (imageItem.Characterystyka.Count > 0)
            {
                TreeNode chGroup = ItemRootNode.Nodes.Add("", "Характеристики:");

                foreach (CharacterystykaItem chItem in imageItem.Characterystyka)
                {
                    TreeNode LinkContextNode = chGroup.Nodes.Add(chItem.Code, chItem.ItemName);
                }

                chGroup.Expand();
            }

            //Розкрутка інградієнтів
            if (imageItem.Ingradienty.Count > 0)
            {
                TreeNode IngradientGroup = ItemRootNode.Nodes.Add("", "Інградієнти:");

                foreach (ImageBase ingradient in imageItem.Ingradienty)
                {
                    Image imageIngradient = Program.GlobalKernel.GetImageByID(ingradient.ID);

                    TreeNode IngradientNode = IngradientGroup.Nodes.Add(imageIngradient.ID.ToString(), imageIngradient.Name);

                    // LoadTreeLinksNode(IngradientNode, imageIngradient);
                }

                IngradientGroup.Expand();
            }

            ItemRootNode.Expand();
        }

        private void FormFirst_Load(object sender, EventArgs e)
        {
            ReloadTreeView();
            ReloadEventJournal();

            ReloadTreeViewAbstractImages();
        }

        private void TreeViewImages_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode nodeClick = e.Node;

                //Меню для ноди з кодом context_
                if (nodeClick.Name.Length > 8)
                {
                    string nodeKey = nodeClick.Name.Substring(0, 8);

                    if (nodeKey == "context_")
                    {
                        string contextID = nodeClick.Name.Substring(8);

                        ImageContext imageContext = Program.GlobalKernel.GetImageContexByID(int.Parse(contextID));

                        if (imageContext != null)
                        {
                            nodeClick.ContextMenuStrip = contextMenuForTreeNode;

                            contextMenuForTreeNode.Items.Clear();

                            //Додати образ
                            ToolStripItem OpenForm_ImageElement = contextMenuForTreeNode.Items.Add("Додати новий образ до контексту <" + imageContext.Name + ">");
                            OpenForm_ImageElement.Tag = imageContext;
                            OpenForm_ImageElement.Click += OpenForm_ImageElement_Click;

                            //Копіювати до абстрактних понять код 1
                            ToolStripItem CopyToAbstractContext = contextMenuForTreeNode.Items.Add("Копіювати <" + imageContext.Name + "> до Абстрактних понять");
                            CopyToAbstractContext.Tag = imageContext;
                            CopyToAbstractContext.Click += CopyToAbstractContext_Click;

                            //Копіювати до малюнків
                            ToolStripItem CopyToPictures = contextMenuForTreeNode.Items.Add("Копіювати <" + imageContext.Name + "> до малюнків");
                            CopyToPictures.Tag = imageContext;
                            CopyToPictures.Click += CopyToPictures_Click;
                        }
                    }
                }

                //Меню для ноди з кодом ...
            }
        }

        private void CopyToAbstractContext_Click(object sender, EventArgs e)
        {
            ToolStripItem ItemMenu = (ToolStripItem)sender;

            if (ItemMenu.Tag != null)
            {
                ImageContext imageContext = (ImageContext)ItemMenu.Tag;

                ImageContext AbstractContext = Program.GlobalKernel.GetImageContexByID(1);

                frmImageElement FormImageElement = new frmImageElement();
                FormImageElement.ImageElementItemTemplate = new Image();
                FormImageElement.ImageElementItemTemplate.Name = imageContext.Name;
                FormImageElement.ImageElementItemTemplate.Context = AbstractContext;
                FormImageElement.ImageElementItemTemplate.LinkContext = imageContext;

                FormImageElement.ImageElementItemTemplate.Atributes.Add(ImageAtribute.Abstract);

                FormImageElement.Show();
            }
        }

        private void CopyToPictures_Click(object sender, EventArgs e)
        {
            ToolStripItem ItemMenu = (ToolStripItem)sender;

            if (ItemMenu.Tag != null)
            {
                ImageContext imageContext = (ImageContext)ItemMenu.Tag;

                Pictures imagePicture = Program.GlobalKernel.GetPicturesByName(imageContext.Name);
                if (imagePicture == null)
                {
                    FormPicturesElement formPictureElement = new FormPicturesElement();
                    formPictureElement.PictureElementItemTemplate = new Pictures();
                    formPictureElement.PictureElementItemTemplate.Name = imageContext.Name;
                    formPictureElement.Show();
                }
                else
                    MessageBox.Show("Малюнок з назвою <" + imageContext.Name + "> вже є");
            }
        }

        private void OpenForm_ImageElement_Click(object sender, EventArgs e)
        {
            ToolStripItem ItemMenu = (ToolStripItem)sender;

            if (ItemMenu.Tag != null)
            {
                frmImageElement FormImageElement = new frmImageElement();
                FormImageElement.ImageElementItemTemplate = new Image();
                FormImageElement.ImageElementItemTemplate.Context = (ImageContext)ItemMenu.Tag;
                FormImageElement.Show();
            }
        }

        private void treeViewImages_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode treeNodeSelect = treeViewImages.SelectedNode;

            if (treeNodeSelect != null)
            {
                long resIDImage;

                if (long.TryParse(treeNodeSelect.Name, out resIDImage))
                {
                    if (resIDImage != 0)
                    {
                        //Шукаємо образ по ІД
                        Image findImage = Program.GlobalKernel.GetImageByID(resIDImage);

                        if (findImage != null)
                        {
                            frmImageElement frmImageElement = new frmImageElement();
                            frmImageElement.WindowState = FormWindowState.Maximized;
                            frmImageElement.ImageElementItem = findImage;
                            frmImageElement.Show();
                        }
                    }
                }
            }
        }

        private void buttonExportForm_Click(object sender, EventArgs e)
        {
            FormExportXML ftmFormExportXML = new FormExportXML();
            ftmFormExportXML.Show();
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            toolStripButtonRefresh.Enabled = false;
            ReloadTreeView();
            ReloadEventJournal();
            toolStripButtonRefresh.Enabled = true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmImageElement frmImageElement = new frmImageElement();
            frmImageElement.Show();
        }

        private void toolStripButtonContextList_Click(object sender, EventArgs e)
        {
            FormContext ftmFormContext = new FormContext();
            ftmFormContext.Show();
        }

        private void toolStripButtonPictureList_Click(object sender, EventArgs e)
        {
            FormPicturesList frmPictureList = new ImageForms.FormPicturesList();
            frmPictureList.Show();
        }

        private void toolStripButtonOpenFormTreeModel_Click(object sender, EventArgs e)
        {
            FormTreeModel ftmTreeModel = new FormTreeModel();
            ftmTreeModel.Show();
        }

        private void toolStripButtonFixed_Click(object sender, EventArgs e)
        {
            toolStripButtonFixed.Enabled = false;
            System.Diagnostics.Process.Start("..\\..\\..\\bin\\Debug\\Image.exe", "transformation");
            toolStripButtonFixed.Enabled = true;
        }

        private void textBoxSearsch_TextChanged(object sender, EventArgs e)
        {
            ReloadTreeViewAbstractImages(textBoxSearsch.Text.Trim());
        }

        private void toolStripButtonSearch_Click(object sender, EventArgs e)
        {
            FormSearch formSearch = new FormSearch();
            formSearch.Show();
        }
    }
}
