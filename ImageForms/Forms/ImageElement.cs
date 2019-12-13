using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ImageLibrary;

namespace ImageForms
{
    public partial class frmImageElement : Form
    {
        private Image m_ImageElementItem;
        private Image m_ImageElementItemTemplate;

        /// <summary>
        /// Образ з яким на даний час працює форма або null якщо це новий елемент
        /// </summary>
        public Image ImageElementItem
        {
            get
            {
                return m_ImageElementItem;
            }

            set
            {
                m_ImageElementItem = value;
            }
        }

        /// <summary>
        /// Тимчасовий шаблон для нового образу
        /// </summary>
        public Image ImageElementItemTemplate
        {
            get
            {
                return m_ImageElementItemTemplate;
            }

            set
            {
                m_ImageElementItemTemplate = value;
            }
        }

        public frmImageElement()
        {
            InitializeComponent();
        }

        // Обработчик ошибок
        private static void ErrorMessage(object sender, DataBaseEventArgs e)
        {
            MessageBox.Show("Форма ImageElement: " + e.Message + ": " + e.DescriptionMessage, "Помилка");
        }

        /// <summary>
        /// Добавляє характеристику в таблицю
        /// </summary>
        /// <param name="characterystyka"></param>
        public void AddOrUpadateCharacterystyka(CharacterystykaItem characterystyka)
        {
            string characterystyka_Code = characterystyka.Code.Trim().ToLower();

            if (characterystyka_Code.Length > 0)
            {
                foreach (DataGridViewRow row in dataGridViewCharList.Rows)
                {
                    if (row.Cells["colCode"].Value.ToString().ToLower() == characterystyka_Code)
                    {
                        row.Cells["colName"].Value = characterystyka.ItemName;
                        row.Cells["colValue"].Value = characterystyka.ItemValue;
                        row.Cells["colCode"].Value = characterystyka.Code;
                        return;
                    }
                }

                int item = dataGridViewCharList.Rows.Add();

                dataGridViewCharList.Rows[item].Cells["colCode"].Value = characterystyka.Code.Trim();
                dataGridViewCharList.Rows[item].Cells["colName"].Value = characterystyka.ItemName.Trim();
                dataGridViewCharList.Rows[item].Cells["colValue"].Value = characterystyka.ItemValue;
            }
        }

        /// <summary>
        /// Функція загружає образи з контексту абстракт для вибору вказівника
        /// </summary>
        private void LoadImageBaseForPointer()
        {
            List<ImageBase> imageBaseList = new List<ImageBase>();
            Program.GlobalKernel.LoadAllImageBase(imageBaseList, 1);

            comboBoxPointerImage.Items.Clear();

            foreach (ImageBase imageBaseItem in imageBaseList)
                comboBoxPointerImage.Items.Add(imageBaseItem);
        }

        /// <summary>
        /// Шукає інградієнти по назві
        /// </summary>
        private void GetByNameIngradient()
        {
            comboBoxFindImages.Items.Clear();

            List<ImageBase> listImageBase = new List<ImageBase>();
            Program.GlobalKernel.GetListImageBaseByName(listImageBase, textBoxFindIngradientName.Text);

            foreach (ImageBase item in listImageBase)
                comboBoxFindImages.Items.Add(item);

            if (comboBoxFindImages.Items.Count > 0)
                comboBoxFindImages.SelectedIndex = 0;
        }

        /// <summary>
        /// Добавляє в список вибраний інградієнт і видаляє його із комбобокса
        /// </summary>
        private void buttonAddFindImage_Click(object sender, EventArgs e)
        {
            if (comboBoxFindImages.SelectedItem != null)
            {
                ImageBase selectImage = (ImageBase)comboBoxFindImages.SelectedItem;
                bool elementIsAdded = false;

                //Пошук повторень
                foreach (ImageBase item in listBoxIngradienty.Items)
                    if (selectImage.ID == item.ID)
                    {
                        elementIsAdded = true;
                        continue;
                    }

                if (!elementIsAdded)
                    listBoxIngradienty.Items.Add(selectImage);

                comboBoxFindImages.Items.Remove(selectImage);

                if (comboBoxFindImages.Items.Count > 0)
                    comboBoxFindImages.SelectedIndex = 0;
                else
                    comboBoxFindImages.Text = "";
            }
        }

        private void LoadTreeLinksNode(TreeNode rootNode, Image imageItem)
        {
            TreeNode ItemGroup = rootNode.Nodes.Add("", imageItem.Name);

            //Ссилка контекст
            if (imageItem.LinkContext != null)
            {
                List<Image> imageLinkContextList = Program.GlobalKernel.LoadAllImageByContext(imageItem.LinkContext.ID);

                if (imageLinkContextList.Count > 0)
                {
                    TreeNode LinkContextGroup = ItemGroup.Nodes.Add("", "Розширення");
                    LinkContextGroup.ForeColor = System.Drawing.Color.Blue;

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
                TreeNode chGroup = ItemGroup.Nodes.Add("", "Характеристики");
                chGroup.ForeColor = System.Drawing.Color.Blue;

                foreach (CharacterystykaItem chItem in imageItem.Characterystyka)
                {
                    TreeNode LinkContextNode = chGroup.Nodes.Add(chItem.Code, chItem.ItemName);
                }

                chGroup.Expand();
            }

            //Розкрутка інградієнтів
            if (imageItem.Ingradienty.Count > 0)
            {
                TreeNode IngradientGroup = ItemGroup.Nodes.Add("", "Інградієнти");
                IngradientGroup.ForeColor = System.Drawing.Color.Blue;

                foreach (ImageBase ingradient in imageItem.Ingradienty)
                {
                    Image imageIngradient = Program.GlobalKernel.GetImageByID(ingradient.ID);

                    TreeNode IngradientNode = IngradientGroup.Nodes.Add(imageIngradient.ID.ToString(), imageIngradient.Name);

                    // LoadTreeLinksNode(IngradientNode, imageIngradient);
                }

                IngradientGroup.Expand();
            }

            ItemGroup.Expand();
            rootNode.Expand();
        }

        private void LoadTreeLinks()
        {
            treeViewLinks.Nodes.Clear();

            if (ImageElementItem != null)
            {
                TreeNode rootNode = treeViewLinks.Nodes.Add("root", "root");

                LoadTreeLinksNode(rootNode, ImageElementItem);


            }
        }

        private void ImageElement_Load(object sender, EventArgs e)
        {
            controlContextContext.TitleText = "Контекст: ";
            controlContextLink.TitleText = "Розширення: ";

            //Образи для вибору вказівника
            LoadImageBaseForPointer();

            if (ImageElementItem != null)
            {
                //Образ відкритий для редагування
                textBoxImageName.Text = ImageElementItem.Name;
                textBoxDescription.Text = ImageElementItem.Description;
                textBoxSynonymy.Text = ImageElementItem.Synonymy;

                //Мітки
                checkBoxPointer.Checked = ImageElementItem.Pointer;
                checkBoxPlural.Checked = ImageElementItem.Plural;
                checkBoxIntermediate.Checked = ImageElementItem.Intermediate;

                //Вказівник
                if (ImageElementItem.Pointer || ImageElementItem.Plural)
                {
                    if (ImageElementItem.PointerImage != null)
                        foreach (ImageBase imageBaseItem in comboBoxPointerImage.Items)
                        {
                            if (ImageElementItem.PointerImage.ID == imageBaseItem.ID)
                            {
                                comboBoxPointerImage.SelectedItem = imageBaseItem;
                                continue;
                            }
                        }
                }

                //Контексти
                controlContextContext.ImageContextSelect = ImageElementItem.Context;
                controlContextLink.ImageContextSelect = ImageElementItem.LinkContext;

                //Заповнити атрибути
                foreach (ImageAtribute atribute in ImageElementItem.Atributes)
                {
                    if (atribute == ImageAtribute.Abstract)
                        checkBoxAbstract.Checked = true;

                    if (atribute == ImageAtribute.Object)
                        checkBoxObject.Checked = true;

                    if (atribute == ImageAtribute.Process)
                        checkBoxProces.Checked = true;

                    if (atribute == ImageAtribute.Multy)
                        checkBoxMulty.Checked = true;
                }

                //Заповнити характеристики
                foreach (CharacterystykaItem characterystyka in ImageElementItem.Characterystyka)
                    AddCharacterystykaRow(characterystyka);

                //Заповнити інградієнти
                foreach (ImageBase imageBase in ImageElementItem.Ingradienty)
                    listBoxIngradienty.Items.Add(imageBase);
            }
            else
            {
                //Початкові дані для нового образу
                if (ImageElementItemTemplate != null)
                {
                    textBoxImageName.Text = ImageElementItemTemplate.Name;
                    textBoxDescription.Text = ImageElementItemTemplate.Description;
                    textBoxSynonymy.Text = ImageElementItemTemplate.Synonymy;

                    //Мітки
                    checkBoxPointer.Checked = ImageElementItemTemplate.Pointer;
                    checkBoxPlural.Checked = ImageElementItemTemplate.Plural;
                    checkBoxIntermediate.Checked = ImageElementItemTemplate.Intermediate;

                    //Вказівник
                    // ...

                    //Контексти
                    controlContextContext.ImageContextSelect = ImageElementItemTemplate.Context;
                    controlContextLink.ImageContextSelect = ImageElementItemTemplate.LinkContext;

                    //Атрибути
                    foreach (ImageAtribute atribute in ImageElementItemTemplate.Atributes)
                    {
                        if (atribute == ImageAtribute.Abstract)
                            checkBoxAbstract.Checked = true;

                        if (atribute == ImageAtribute.Object)
                            checkBoxObject.Checked = true;

                        if (atribute == ImageAtribute.Process)
                            checkBoxProces.Checked = true;

                        if (atribute == ImageAtribute.Multy)
                            checkBoxMulty.Checked = true;
                    }

                    //Заповнити характеристики
                    foreach (CharacterystykaItem characterystyka in ImageElementItemTemplate.Characterystyka)
                        AddCharacterystykaRow(characterystyka);
                }
            }

            //Дерево ссилок
            LoadTreeLinks();
        }

        /// <summary>
        /// Функція добавляє новий рядок з характеритиками в ДатаГрід
        /// </summary>
        /// <param name="characterystyka"></param>
        private void AddCharacterystykaRow(CharacterystykaItem characterystyka)
        {
            int itemRow = dataGridViewCharList.Rows.Add();

            DataGridViewRow row = dataGridViewCharList.Rows[itemRow];

            row.Cells["colCode"].Value = characterystyka.Code;
            row.Cells["colName"].Value = characterystyka.ItemName;
            row.Cells["colValue"].Value = characterystyka.ItemValue;
        }

        /// <summary>
        /// Запис в базу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            Image ImageElementItem_Temp = new Image();

            //ID образу
            if (ImageElementItem != null) ImageElementItem_Temp.ID = ImageElementItem.ID;

            //Назва образу
            string imageName = textBoxImageName.Text.Trim();

            ImageElementItem_Temp.Name = imageName.Trim();
            if (!(ImageElementItem_Temp.Name.Length > 0))
            {
                MessageBox.Show("Назва не може бути пуста", "Помилка");
                return;
            }

            ImageElementItem_Temp.Description = textBoxDescription.Text;
            ImageElementItem_Temp.Synonymy = textBoxSynonymy.Text;

            //Мітки
            ImageElementItem_Temp.Pointer = checkBoxPointer.Checked;
            ImageElementItem_Temp.Plural = checkBoxPlural.Checked;
            ImageElementItem_Temp.Intermediate = checkBoxIntermediate.Checked;

            if (ImageElementItem_Temp.Pointer || ImageElementItem_Temp.Plural)
                if (comboBoxPointerImage.SelectedItem != null)
                    ImageElementItem_Temp.PointerImage = (ImageBase)comboBoxPointerImage.SelectedItem;
                else
                {
                    MessageBox.Show("Не вибраний елемент для вказівника чи множини", "Помилка");
                    return;
                }

            //Контексти
            if (controlContextContext.ImageContextSelect != null)
                ImageElementItem_Temp.Context = controlContextContext.ImageContextSelect;
            else
            {
                MessageBox.Show("Не вибраний контекст", "Помилка");
                return;
            }

            if (controlContextLink.ImageContextSelect != null)
                ImageElementItem_Temp.LinkContext = controlContextLink.ImageContextSelect;

            //Заповнюємо атрибути
            if (checkBoxAbstract.Checked)
                ImageElementItem_Temp.Atributes.Add(ImageAtribute.Abstract);

            if (checkBoxObject.Checked)
                ImageElementItem_Temp.Atributes.Add(ImageAtribute.Object);

            if (checkBoxProces.Checked)
                ImageElementItem_Temp.Atributes.Add(ImageAtribute.Process);

            if (checkBoxMulty.Checked)
                ImageElementItem_Temp.Atributes.Add(ImageAtribute.Multy);

            //Заповнюємо характеристики
            foreach (DataGridViewRow dridRowItem in dataGridViewCharList.Rows)
            {
                string charName = dridRowItem.Cells["colName"].Value.ToString();
                string charValue = dridRowItem.Cells["colValue"].Value.ToString();
                string charCode = dridRowItem.Cells["colCode"].Value.ToString();

                if (!(charCode.Trim().Length > 0))
                {
                    MessageBox.Show("Для характеристики " + charName + " не вказаний код!", "Помилка");
                    return;
                }

                ImageElementItem_Temp.Characterystyka.Add(new CharacterystykaItem(charName, charValue, charCode));
            }

            //Заповнюємо інградієнти
            foreach (ImageBase imageBase in listBoxIngradienty.Items)
                ImageElementItem_Temp.Ingradienty.Add(imageBase);

            //Записуємо
            if (ImageElementItem == null)
            {
                if (Program.GlobalKernel.InsertImage(ImageElementItem_Temp))
                {
                    //ImageElementItem = ImageElementItem_Temp;
                    this.Close();
                }
                else
                    MessageBox.Show("Невдалось створити елемент: ", "Помилка", MessageBoxButtons.OK);
            }
            else
            {
                if (Program.GlobalKernel.UpdateImage(ImageElementItem_Temp))
                {
                    //ImageElementItem = ImageElementItem_Temp;
                    this.Close();
                }
                else
                    MessageBox.Show("Невдалось записати елемент: ", "Помилка", MessageBoxButtons.OK);
            }
        }

        private void buttonGetByNameIngradient_Click(object sender, EventArgs e)
        {
            GetByNameIngradient();
        }

        private void textBoxFindIngradientName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                GetByNameIngradient();
        }

        private void listBoxIngradienty_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBoxIngradienty.SelectedIndex >= 0)
                if (e.KeyCode == Keys.Delete)
                {
                    DialogResult result = MessageBox.Show("Видалити <" + listBoxIngradienty.SelectedItem.ToString() + "> з інградієнтів?", "Повідомлення", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                        listBoxIngradienty.Items.RemoveAt(listBoxIngradienty.SelectedIndex);
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormCharacterystykaElement frmCharacterystykaElement = new ImageForms.FormCharacterystykaElement();
            frmCharacterystykaElement.ImageParentForm = this;
            frmCharacterystykaElement.Show();
        }

        private void dataGridViewCharList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                FormCharacterystykaElement frmCharacterystykaElement = new FormCharacterystykaElement();
                frmCharacterystykaElement.ImageParentForm = this;

                DataGridViewRow GridRow = dataGridViewCharList.Rows[rowIndex];

                frmCharacterystykaElement.Characterystyka = new CharacterystykaItem(
                    GridRow.Cells["colName"].Value.ToString(),
                    GridRow.Cells["colValue"].Value.ToString(),
                    GridRow.Cells["colCode"].Value.ToString()
                );

                frmCharacterystykaElement.Show();
            }
        }

        private void dataGridViewCharList_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridViewSelectedRowCollection selectRow = dataGridViewCharList.SelectedRows;

            if (selectRow.Count > 0)
                if (e.KeyCode == Keys.Delete)
                {
                    DialogResult result = MessageBox.Show("Видалити вибрані характеристики?", "Повідомлення", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        foreach (DataGridViewRow row in selectRow)
                            dataGridViewCharList.Rows.Remove(row);
                    }
                }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
