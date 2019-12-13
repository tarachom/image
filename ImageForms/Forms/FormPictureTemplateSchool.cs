using System;
using System.Windows.Forms;
using ImageLibrary;

namespace ImageForms
{
    public partial class FormPictureTemplateSchool : Form
    {
        private PicturesTemplate m_picturesTemplateItem;

        /// <summary>
        /// Заготовка для аналізу і обучення
        /// </summary>
        public PicturesTemplate PicturesTemplateItem
        {
            get
            {
                return m_picturesTemplateItem;
            }

            set
            {
                m_picturesTemplateItem = value;
            }
        }

        public FormPictureTemplateSchool()
        {
            InitializeComponent();
        }

        private void FormPictureTemplateSchool_Load(object sender, EventArgs e)
        {
            textBoxTemplateText.Text = this.PicturesTemplateItem.Template;
        }

        private string[] SplitTemplate()
        {
            string templateText = textBoxTemplateText.Text;

            string[] words = templateText.Split(new string[]
            {
                     " ", ",", ".", ";", ":",
                     "!", "?",
                     "(", ")", "{", "}", "[", "]",
                     "|", "/", "\\",
                     "\n", "\r", "\t"
            }, StringSplitOptions.RemoveEmptyEntries);

            //

            return words;
        }

        private void RefreshTableWords()
        {

        }

        private void buttonAnalizeProcess_Click(object sender, EventArgs e)
        {
            string[] words = SplitTemplate();

            Program.Global_NormalizeDataGridRows(words.Length, dataGridViewWords.Rows);

            int index_row = 0;

            DataGridViewCellStyle styleByFindOK = new DataGridViewCellStyle();
            styleByFindOK.ForeColor = System.Drawing.Color.DarkGray;

            foreach (string word in words)
            {
                //Перевірити чи є вже таке слово в базі
                int countPictures = 0; // !!! Program.GlobalKernel.FindPictureByName(word);

                DataGridViewRow row = dataGridViewWords.Rows[index_row];

                row.Cells["Words_index"].Value = index_row + 1;
                row.Cells["Words_word"].Value = word;
                row.Cells["Words_CreateImage"].Value = false;
                row.Cells["Words_CreatePictures"].Value = (countPictures > 0 ? false : true);

                //if (countPictures > 0)
                //    row.Cells["Words_word"].Style = styleByFindOK;

                index_row++;
            }
        }

        private void dataGridViewWords_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridViewSelectedRowCollection selectRow = dataGridViewWords.SelectedRows;

            //Видалити вибрані рядки
            if (selectRow.Count > 0 && e.KeyCode == Keys.Delete)
            {
                DialogResult result = MessageBox.Show("Видалити вибрані слова?", "Повідомлення", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    foreach (DataGridViewRow row in selectRow)
                        dataGridViewWords.Rows.Remove(row);
                }
            }

            //

        }

        private void buttonAddToTemplates_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow rowWord in dataGridViewWords.Rows)
            {
                PicturesTemplate pictureTemplateItem = new PicturesTemplate(rowWord.Cells["Words_word"].Value.ToString());
                Program.GlobalKernel.AddPictureTemplate(pictureTemplateItem);
            }
        }

        private void buttonCreateImage_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow rowWord in dataGridViewWords.Rows)
            {
                if ((bool)rowWord.Cells["Words_CreateImage"].Value)
                {
                    Image imageTemplate = new Image();

                    imageTemplate.Name = rowWord.Cells["Words_word"].Value.ToString();
                    imageTemplate.Description = rowWord.Cells["Words_word"].Value.ToString();

                    imageTemplate.Characterystyka.Add(new CharacterystykaItem("Поле", "Значення"));

                    frmImageElement formNewImageElement = new frmImageElement();
                    formNewImageElement.ImageElementItemTemplate = imageTemplate;
                    formNewImageElement.Show();
                }
            }
        }

        private void buttonCreatePictures_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow rowWord in dataGridViewWords.Rows)
            {
                if ((bool)rowWord.Cells["Words_CreatePictures"].Value)
                {
                    Pictures pictureTemplate = new Pictures();

                    pictureTemplate.Name = rowWord.Cells["Words_word"].Value.ToString();
                    pictureTemplate.Description = rowWord.Cells["Words_word"].Value.ToString();

                    FormPicturesElement frmPictureElement = new FormPicturesElement();
                    frmPictureElement.PictureElementItemTemplate = pictureTemplate;
                    frmPictureElement.Show();
                }
            }


        }

        private void buttonSavePictures_Click(object sender, EventArgs e)
        {
            buttonSavePictures.Enabled = false;

            foreach (DataGridViewRow rowWord in dataGridViewWords.Rows)
            {
                if ((bool)rowWord.Cells["Words_CreatePictures"].Value)
                {
                    Pictures pictureTemp = new Pictures();

                    pictureTemp.Name = rowWord.Cells["Words_word"].Value.ToString();
                    pictureTemp.Description = rowWord.Cells["Words_word"].Value.ToString();

                    bool result = Program.GlobalKernel.InsertPicture(pictureTemp);

                    System.Diagnostics.Debug.WriteLine("Результат запису нового малюнка '" + pictureTemp.Name + "' : " + result.ToString());
                }
            }

            buttonSavePictures.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxTemplateText.Text = "";
        }
    }
}
