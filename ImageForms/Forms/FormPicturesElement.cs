using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Windows.Forms;
using ImageLibrary;

namespace ImageForms
{
    public partial class FormPicturesElement : Form
    {
        private Pictures m_PictureElementItem;
        private Pictures m_PictureElementItemTemplate;

        /// <summary>
        /// Малюнок з яким на даний час працює форма або null якщо це новий елемент
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

        /// <summary>
        /// Тимчасовий шаблон для нового малюнку
        /// </summary>
        public Pictures PictureElementItemTemplate
        {
            get
            {
                return m_PictureElementItemTemplate;
            }

            set
            {
                m_PictureElementItemTemplate = value;
            }
        }

        public FormPicturesElement()
        {
            InitializeComponent();
        }

        private void FormPicturesElement_Load(object sender, EventArgs e)
        {
            if (PictureElementItem != null)
            {
                textBoxPictureName.Text = PictureElementItem.Name;
                textBoxPictureDescription.Text = PictureElementItem.Description;

                //Колекції малюнків
                foreach (PicturesBase picturesBaseElement in PictureElementItem.PicturesPictureChild)
                {
                    int indexNewRow = dataGridViewPictures.Rows.Add();
                    DataGridViewRow NewRow = dataGridViewPictures.Rows[indexNewRow];

                    NewRow.Cells["Pictures_ID"].Value = picturesBaseElement.ID;
                    NewRow.Cells["Pictures_Name"].Value = picturesBaseElement;
                }

                //Колекції образів
                foreach (ImageBase imageBaseElement in PictureElementItem.PicturesImageChild)
                {
                    int indexNewRow = dataGridViewImages.Rows.Add();
                    DataGridViewRow NewRow = dataGridViewImages.Rows[indexNewRow];

                    NewRow.Cells["Images_ID"].Value = imageBaseElement.ID;
                    NewRow.Cells["Images_Name"].Value = imageBaseElement;
                }
            }
            else if (PictureElementItemTemplate != null)
            {
                textBoxPictureName.Text = PictureElementItemTemplate.Name;
                textBoxPictureDescription.Text = PictureElementItemTemplate.Description;

                //Колекції образів
                foreach (ImageBase imageBaseElement in PictureElementItemTemplate.PicturesImageChild)
                {
                    int indexNewRow = dataGridViewImages.Rows.Add();
                    DataGridViewRow NewRow = dataGridViewImages.Rows[indexNewRow];

                    NewRow.Cells["Images_ID"].Value = imageBaseElement.ID;
                    NewRow.Cells["Images_Name"].Value = imageBaseElement;
                }
            }

            controlSearchImage.TableName = "image";
            controlSearchPictures.TableName = "pictures";
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Save(bool close)
        {
            Pictures pictureTemp = new Pictures();

            if (PictureElementItem != null)
                pictureTemp.ID = PictureElementItem.ID;

            pictureTemp.Name = textBoxPictureName.Text;
            pictureTemp.Description = textBoxPictureDescription.Text;

            //Колекції Pictures
            foreach (DataGridViewRow rowWord in dataGridViewPictures.Rows)
            {
                PicturesBase picturesBaseElement = Program.GlobalKernel.GetPicturesBaseByID(int.Parse(rowWord.Cells["Pictures_ID"].Value.ToString()));

                if (picturesBaseElement != null)
                    pictureTemp.PicturesPictureChild.Add(picturesBaseElement);
            }

            //Колекції Images
            foreach (DataGridViewRow rowWord in dataGridViewImages.Rows)
            {
                ImageBase imageBaseElement = Program.GlobalKernel.GetImageBaseByID(long.Parse(rowWord.Cells["Images_ID"].Value.ToString()));

                if (imageBaseElement != null)
                    pictureTemp.PicturesImageChild.Add(imageBaseElement);
            }

            if (PictureElementItem != null)
            {
                //Update
                if (!Program.GlobalKernel.UpdatePicture(pictureTemp))
                    MessageBox.Show("Невдалось обновити малюнок");
                else
                {
                    if (close)
                        this.Close();
                }
            }
            else
            {
                //Insert
                if (!Program.GlobalKernel.InsertPicture(pictureTemp))
                    MessageBox.Show("Невдалось записати новий малюнок");
                else
                {
                    if (close)
                        this.Close();
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Save(true);
        }

        private void buttonOneSave_Click(object sender, EventArgs e)
        {
            Save(false);
        }

        private void buttonAddPicture_Click(object sender, EventArgs e)
        {
            if (controlSearchPictures.SelectSearchElement != null)
            {
                PicturesBase picturesBaseElement = Program.GlobalKernel.GetPicturesBaseByID(controlSearchPictures.SelectSearchElement.ID);

                if (picturesBaseElement != null)
                {
                    int indexNewRow = dataGridViewPictures.Rows.Add();
                    DataGridViewRow NewRow = dataGridViewPictures.Rows[indexNewRow];

                    NewRow.Cells["Pictures_ID"].Value = picturesBaseElement.ID;
                    NewRow.Cells["Pictures_Name"].Value = picturesBaseElement;
                }
            }
        }

        private void buttonAddImage_Click(object sender, EventArgs e)
        {
            if (controlSearchImage.SelectSearchElement != null)
            {
                ImageBase imageBaseElement = Program.GlobalKernel.GetImageBaseByID((long)controlSearchImage.SelectSearchElement.ID);

                if (imageBaseElement != null)
                {
                    int indexNewRow = dataGridViewImages.Rows.Add();
                    DataGridViewRow NewRow = dataGridViewImages.Rows[indexNewRow];

                    NewRow.Cells["Images_ID"].Value = imageBaseElement.ID;
                    NewRow.Cells["Images_Name"].Value = imageBaseElement;
                }
            }

        }

        private void dataGridViewPictures_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridViewSelectedRowCollection selectRow = dataGridViewPictures.SelectedRows;
            if (selectRow.Count > 0 && e.KeyCode == Keys.Delete)
            {
                DialogResult result = MessageBox.Show("Видалити вибрані рядки?", "Повідомлення", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    foreach (DataGridViewRow row in selectRow)
                        dataGridViewPictures.Rows.Remove(row);
                }
            }
        }

        private void dataGridViewImages_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridViewSelectedRowCollection selectRow = dataGridViewImages.SelectedRows;
            if (selectRow.Count > 0 && e.KeyCode == Keys.Delete)
            {
                DialogResult result = MessageBox.Show("Видалити вибрані рядки?", "Повідомлення", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    foreach (DataGridViewRow row in selectRow)
                        dataGridViewImages.Rows.Remove(row);
                }
            }
        }

        private void buttonBuildPictureSchema_Click(object sender, EventArgs e)
        {
            FormBuildPictureSchema frmPicSchema = new FormBuildPictureSchema();
            frmPicSchema.PictureElementItem = this.PictureElementItem;
            frmPicSchema.Show();
        }

        private void buttonAddUnionItem_Click(object sender, EventArgs e)
        {
            if (this.PictureElementItem != null)
            {
                FormPicturesElementUnionTrack FormUnionTrack = new FormPicturesElementUnionTrack();
                FormUnionTrack.PictureElement = PictureElementItem;
                FormUnionTrack.Show();
            }
        }
    }
}
