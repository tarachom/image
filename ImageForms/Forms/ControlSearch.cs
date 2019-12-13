using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ImageLibrary;

namespace ImageForms
{
    public partial class ControlSearch : UserControl
    {
        private bool contextMenuCreate = false;

        private string m_tableName;

        /// <summary>
        /// Таблиця по якій буде відбуватися пошук
        /// </summary>
        public string TableName
        {
            get
            {
                return m_tableName;
            }

            set
            {
                m_tableName = value;
            }
        }

        private SearchElement m_SelectSearchElement;

        /// <summary>
        /// Вибраний із списку знайдений елемент
        /// </summary>
        public SearchElement SelectSearchElement
        {
            get
            {
                return m_SelectSearchElement;
            }

            set
            {
                m_SelectSearchElement = value;
            }
        }

        public ControlSearch()
        {
            InitializeComponent();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            listBoxSearchList.Items.Clear();

            List<SearchElement> result = new List<SearchElement>();
            Program.GlobalKernel.SearchForControlSearch(this.TableName, textBoxSearch.Text, 30, result);

            foreach (SearchElement searchElement in result)
                listBoxSearchList.Items.Add(searchElement);

            if (listBoxSearchList.Items.Count > 0)
                this.BringToFront();
        }

        private void ControlSearch_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.SendToBack();
        }

        private void listBoxSearchList_SelectElement()
        {
            int selectIndex = listBoxSearchList.SelectedIndex;
            if (selectIndex >= 0)
            {
                this.SelectSearchElement = (SearchElement)listBoxSearchList.Items[selectIndex];
                textBoxSearch.Text = this.SelectSearchElement.Name;

                this.SendToBack();
            }
        }

        private void listBoxSearchList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listBoxSearchList_SelectElement();
        }

        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
                if (listBoxSearchList.Items.Count > 0)
                {
                    listBoxSearchList.SelectedIndex = 0;
                    listBoxSearchList.Focus();
                }
        }

        private void listBoxSearchList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                listBoxSearchList_SelectElement();
        }

        private void textBoxSearch_Enter(object sender, EventArgs e)
        {
            if (textBoxSearch.Text.Length > 0)
                this.BringToFront();
        }

        private void AddImageItem_Click(object sender, EventArgs e)
        {
            frmImageElement newFrmImageElement = new frmImageElement();
            newFrmImageElement.Show();
        }

        private void AddPicturesItem_Click(object sender, EventArgs e)
        {
            FormPicturesElement newFormPicturesElement = new FormPicturesElement();
            newFormPicturesElement.Show();
        }

        private void buttonContextMenu_Click(object sender, EventArgs e)
        {
            if (!contextMenuCreate)
            {
                if (TableName == "image")
                {
                    ToolStripItem addImageItem = contextMenuStripFunc.Items.Add("Створити новий образ");
                    addImageItem.Click += AddImageItem_Click;

                }
                else if (TableName == "pictures")
                {
                    ToolStripItem addPicturesItem = contextMenuStripFunc.Items.Add("Створити новий малюнок");
                    addPicturesItem.Click += AddPicturesItem_Click;
                }
                else
                    throw new Exception("Невірно задана таблиця для пошуку");

                contextMenuCreate = true;
            }

            System.Drawing.Point pointPosition = buttonContextMenu.PointToScreen(new System.Drawing.Point());
            pointPosition.Offset(0, buttonContextMenu.Height);

            contextMenuStripFunc.Show(pointPosition);
        }
    }
}
