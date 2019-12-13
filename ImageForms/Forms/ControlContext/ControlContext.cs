using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ImageLibrary;

namespace ImageForms
{
    public partial class ControlContext : UserControl
    {
        private ImageContext m_imageContextSelect;
        private string m_titleText = "Контекст: ";

        //Вибраний контекст
        public ImageContext ImageContextSelect
        {
            get
            {
                return m_imageContextSelect;
            }

            set
            {
                m_imageContextSelect = value;
                RewriteTitleText();
            }
        }

        public string TitleText
        {
            get
            {
                return m_titleText;
            }
            set
            {
                m_titleText = value;
                RewriteTitleText();
            }
        }

        public ControlContext()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обновляє напис
        /// </summary>
        public void RewriteTitleText()
        {
            if (ImageContextSelect != null)
                if (ImageContextSelect.Name.Length > 0)
                    labelTitleText.Text = TitleText + m_imageContextSelect.Name;
                else
                    labelTitleText.Text = TitleText + "<Пусто>";
            else
                labelTitleText.Text = TitleText + "<Пусто>";
        }

        private void ControlContext_Load(object sender, EventArgs e)
        {
            RewriteTitleText();
        }

        private void ItemMenuEmpty_Click(object sender, EventArgs e)
        {
            ImageContextSelect = null;
        }

        private void ItemMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem itemMenu = (ToolStripItem)sender;

            if (itemMenu.Tag != null)
            {
                ImageContext imageContextItem = (ImageContext)itemMenu.Tag;
                ImageContextSelect = imageContextItem;

                RewriteTitleText();
            }
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            MenuListContext.Items.Clear();

            List<ImageContext> imageContextAll = new List<ImageContext>();
            Program.GlobalKernel.LoadAllContextList(imageContextAll);

            //Для відміни вибору контексту
            ToolStripItem itemMenuEmpty = MenuListContext.Items.Add("<Пусто>");
            itemMenuEmpty.Click += ItemMenuEmpty_Click;

            foreach (ImageContext imageContextItem in imageContextAll)
            {
                ToolStripItem itemMenu = MenuListContext.Items.Add(imageContextItem.Name);
                itemMenu.Tag = imageContextItem;
                itemMenu.Click += ItemMenu_Click;
            }

            if (MenuListContext.Items.Count > 0)
            {
                System.Drawing.Point buttonPosition = buttonSelect.PointToScreen(new System.Drawing.Point());
                buttonPosition.Offset(-100, 20);

                FormControlContext form = new FormControlContext();

                form.ControlContextItem = this;

                form.StartPosition = FormStartPosition.Manual;
                form.Location = buttonPosition;

                form.ShowDialog();
            }
        }
    }
}
