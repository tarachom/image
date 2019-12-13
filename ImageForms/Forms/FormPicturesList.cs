using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ImageLibrary;

namespace ImageForms
{
    public partial class FormPicturesList : Form
    {
        public FormPicturesList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Записує нову заготовку в базу
        /// </summary>
        /// <param name="templateText"></param>
        private void AddTemplate(string templateText)
        {
            PicturesTemplate pictureTemplate = new PicturesTemplate(templateText);

            if (!Program.GlobalKernel.AddPictureTemplate(pictureTemplate))
                MessageBox.Show("Невдалось добавити заготовку в базу");
            else
                RefreshTemplateList();
        }

        /// <summary>
        /// Пошук малюнка 
        /// </summary>
        /// <param name="searchText"></param>
        private void Search(string searchText)
        {
            string searshText = textBoxSearsh.Text.Trim();

            if (!(searshText.Length > 0))
                return;

            //Добавити заготовку
            AddTemplate(searshText);
        }

        /// <summary>
        /// Обновлення таблиці малюнків
        /// </summary>
        private void RefreshPictureList()
        {
            List<PicturesBase> pictureBaseList = new List<PicturesBase>();
            Program.GlobalKernel.LoadAllPicturesBaseList(pictureBaseList);

            Program.Global_NormalizeDataGridRows(pictureBaseList.Count, dataGridViewPictures.Rows);

            int index_row = 0;

            foreach (PicturesBase pictureItem in pictureBaseList)
            {
                DataGridViewRow row = dataGridViewPictures.Rows[index_row];
                row.Cells["Pictures_colID"].Value = pictureItem.ID;
                row.Cells["Pictures_colName"].Value = pictureItem.Name;

                index_row++;
            }
        }

        /// <summary>
        /// Обновлення таблиці заготовок
        /// </summary>
        private void RefreshTemplateList()
        {
            List<PicturesTemplate> pictureTemplateList = new List<PicturesTemplate>();
            Program.GlobalKernel.LoadAllPicturesTemplateList(pictureTemplateList);

            Program.Global_NormalizeDataGridRows(pictureTemplateList.Count, dataGridViewTemplateList.Rows);

            int index_row = 0;

            foreach (PicturesTemplate pictureTemplate in pictureTemplateList)
            {
                DataGridViewRow row = dataGridViewTemplateList.Rows[index_row];
                row.Cells["Template_colID"].Value = pictureTemplate.ID;
                row.Cells["Template_colTemplate"].Value = pictureTemplate.Template;

                index_row++;
            }
        }

        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search(textBoxSearsh.Text);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Search(textBoxSearsh.Text);
        }

        private void toolStripButtonRefreshTemplateList_Click(object sender, EventArgs e)
        {
            RefreshPictureList();
            RefreshTemplateList();
        }

        private void FormPicturesList_Load(object sender, EventArgs e)
        {
            RefreshPictureList();
            RefreshTemplateList();
        }

        private void dataGridViewTemplateList_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridViewSelectedRowCollection selectRow = dataGridViewTemplateList.SelectedRows;
            if (selectRow.Count > 0 && e.KeyCode == Keys.Delete)
            {
                DialogResult result = MessageBox.Show("Видалити вибрані заготовки?", "Повідомлення", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    foreach (DataGridViewRow row in selectRow)
                        if (!Program.GlobalKernel.DeletePictureTemplate(int.Parse(row.Cells["Template_colID"].Value.ToString())))
                            //Шось рявкнути Що невдалось видалити
                            MessageBox.Show("Невдалось видалити заготовку з бази");

                    RefreshTemplateList();
                }
            }
        }

        private void dataGridViewPictures_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridViewSelectedRowCollection selectRow = dataGridViewPictures.SelectedRows;
            if (selectRow.Count > 0 && e.KeyCode == Keys.Delete)
            {
                DialogResult result = MessageBox.Show("Видалити вибрані малюнки?", "Повідомлення", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    foreach (DataGridViewRow row in selectRow)
                        if (!Program.GlobalKernel.DeletePicture(int.Parse(row.Cells["Pictures_colID"].Value.ToString())))
                            //Шось рявкнути Що невдалось видалити
                            MessageBox.Show("Невдалось видалити малюнок з бази");

                    RefreshPictureList();
                }
            }
        }

        private void dataGridViewTemplateList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int Rowindex = e.RowIndex;

            if (Rowindex >= 0)
            {
                DataGridViewRow row = dataGridViewTemplateList.Rows[Rowindex];

                PicturesTemplate pictureTemplateItem = new PicturesTemplate(
                    int.Parse(row.Cells["Template_colID"].Value.ToString()),
                    row.Cells["Template_colTemplate"].Value.ToString());

                FormPictureTemplateSchool frmPictureTemplateSchool = new FormPictureTemplateSchool();
                frmPictureTemplateSchool.PicturesTemplateItem = pictureTemplateItem;
                frmPictureTemplateSchool.Show();
            }
        }

        private void dataGridViewPictures_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int Rowindex = e.RowIndex;

            if (Rowindex >= 0)
            {
                DataGridViewRow row = dataGridViewPictures.Rows[Rowindex];

                FormPicturesElement frmPictureElement = new FormPicturesElement();
                frmPictureElement.PictureElementItem = Program.GlobalKernel.GetPicturesByID(int.Parse(row.Cells["Pictures_colID"].Value.ToString()));

                try
                {
                    frmPictureElement.Show();
                }
                catch (Exception exeptionItem)
                {
                    MessageBox.Show("Помилка при відкритті форми " + exeptionItem.Message);
                }
            }
        }

        private void toolStripButtonAddPicture_Click(object sender, EventArgs e)
        {
            FormPicturesElement frmPicturesElement = new FormPicturesElement();
            frmPicturesElement.Show();
        }
    }
}
