namespace ImageForms
{
    partial class frmImageElement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxMulty = new System.Windows.Forms.CheckBox();
            this.checkBoxProces = new System.Windows.Forms.CheckBox();
            this.checkBoxObject = new System.Windows.Forms.CheckBox();
            this.checkBoxAbstract = new System.Windows.Forms.CheckBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonAddNewChar = new System.Windows.Forms.Button();
            this.dataGridViewCharList = new System.Windows.Forms.DataGridView();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxImageName = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonAddFindImage = new System.Windows.Forms.Button();
            this.comboBoxFindImages = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.listBoxIngradienty = new System.Windows.Forms.ListBox();
            this.buttonGetByNameIngradient = new System.Windows.Forms.Button();
            this.textBoxFindIngradientName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.textBoxSynonymy = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboBoxPointerImage = new System.Windows.Forms.ComboBox();
            this.checkBoxIntermediate = new System.Windows.Forms.CheckBox();
            this.checkBoxPlural = new System.Windows.Forms.CheckBox();
            this.checkBoxPointer = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.controlContextContext = new ImageForms.ControlContext();
            this.controlContextLink = new ImageForms.ControlContext();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.treeViewLinks = new System.Windows.Forms.TreeView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCharList)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBoxMulty);
            this.groupBox1.Controls.Add(this.checkBoxProces);
            this.groupBox1.Controls.Add(this.checkBoxObject);
            this.groupBox1.Controls.Add(this.checkBoxAbstract);
            this.groupBox1.Location = new System.Drawing.Point(8, 134);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(495, 39);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Атрибути";
            // 
            // checkBoxMulty
            // 
            this.checkBoxMulty.AutoSize = true;
            this.checkBoxMulty.Location = new System.Drawing.Point(280, 17);
            this.checkBoxMulty.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxMulty.Name = "checkBoxMulty";
            this.checkBoxMulty.Size = new System.Drawing.Size(102, 17);
            this.checkBoxMulty.TabIndex = 3;
            this.checkBoxMulty.Text = "Багатозначний";
            this.checkBoxMulty.UseVisualStyleBackColor = true;
            // 
            // checkBoxProces
            // 
            this.checkBoxProces.AutoSize = true;
            this.checkBoxProces.Location = new System.Drawing.Point(201, 17);
            this.checkBoxProces.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxProces.Name = "checkBoxProces";
            this.checkBoxProces.Size = new System.Drawing.Size(64, 17);
            this.checkBoxProces.TabIndex = 2;
            this.checkBoxProces.Text = "Процес";
            this.checkBoxProces.UseVisualStyleBackColor = true;
            // 
            // checkBoxObject
            // 
            this.checkBoxObject.AutoSize = true;
            this.checkBoxObject.Location = new System.Drawing.Point(120, 17);
            this.checkBoxObject.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxObject.Name = "checkBoxObject";
            this.checkBoxObject.Size = new System.Drawing.Size(59, 17);
            this.checkBoxObject.TabIndex = 1;
            this.checkBoxObject.Text = "Об\'єкт";
            this.checkBoxObject.UseVisualStyleBackColor = true;
            // 
            // checkBoxAbstract
            // 
            this.checkBoxAbstract.AutoSize = true;
            this.checkBoxAbstract.Location = new System.Drawing.Point(6, 17);
            this.checkBoxAbstract.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxAbstract.Name = "checkBoxAbstract";
            this.checkBoxAbstract.Size = new System.Drawing.Size(91, 17);
            this.checkBoxAbstract.TabIndex = 0;
            this.checkBoxAbstract.Text = "Абстрактний";
            this.checkBoxAbstract.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(890, 608);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(141, 32);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Зберегти";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.buttonAddNewChar);
            this.groupBox2.Controls.Add(this.dataGridViewCharList);
            this.groupBox2.Location = new System.Drawing.Point(8, 233);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(494, 156);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Характеристики (додпткові поля):";
            // 
            // buttonAddNewChar
            // 
            this.buttonAddNewChar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddNewChar.Location = new System.Drawing.Point(406, 18);
            this.buttonAddNewChar.Name = "buttonAddNewChar";
            this.buttonAddNewChar.Size = new System.Drawing.Size(81, 26);
            this.buttonAddNewChar.TabIndex = 7;
            this.buttonAddNewChar.Text = "Додати";
            this.buttonAddNewChar.UseVisualStyleBackColor = true;
            this.buttonAddNewChar.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridViewCharList
            // 
            this.dataGridViewCharList.AllowUserToAddRows = false;
            this.dataGridViewCharList.AllowUserToDeleteRows = false;
            this.dataGridViewCharList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewCharList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCharList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCode,
            this.colName,
            this.colValue});
            this.dataGridViewCharList.Location = new System.Drawing.Point(6, 18);
            this.dataGridViewCharList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewCharList.Name = "dataGridViewCharList";
            this.dataGridViewCharList.ReadOnly = true;
            this.dataGridViewCharList.Size = new System.Drawing.Size(394, 126);
            this.dataGridViewCharList.TabIndex = 0;
            this.dataGridViewCharList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCharList_CellDoubleClick);
            this.dataGridViewCharList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewCharList_KeyDown);
            // 
            // colCode
            // 
            this.colCode.HeaderText = "Код";
            this.colCode.Name = "colCode";
            this.colCode.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.HeaderText = "Назва";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 150;
            // 
            // colValue
            // 
            this.colValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colValue.HeaderText = "Опис";
            this.colValue.Name = "colValue";
            this.colValue.ReadOnly = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Назва:";
            // 
            // textBoxImageName
            // 
            this.textBoxImageName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxImageName.Location = new System.Drawing.Point(67, 10);
            this.textBoxImageName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxImageName.Name = "textBoxImageName";
            this.textBoxImageName.Size = new System.Drawing.Size(435, 20);
            this.textBoxImageName.TabIndex = 5;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.buttonAddFindImage);
            this.groupBox3.Controls.Add(this.comboBoxFindImages);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.listBoxIngradienty);
            this.groupBox3.Controls.Add(this.buttonGetByNameIngradient);
            this.groupBox3.Controls.Add(this.textBoxFindIngradientName);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(8, 393);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(494, 193);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Інградієнти (образи що входять в даний образ):";
            // 
            // buttonAddFindImage
            // 
            this.buttonAddFindImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddFindImage.Location = new System.Drawing.Point(406, 42);
            this.buttonAddFindImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonAddFindImage.Name = "buttonAddFindImage";
            this.buttonAddFindImage.Size = new System.Drawing.Size(81, 23);
            this.buttonAddFindImage.TabIndex = 7;
            this.buttonAddFindImage.Text = "Добавити";
            this.buttonAddFindImage.UseVisualStyleBackColor = true;
            this.buttonAddFindImage.Click += new System.EventHandler(this.buttonAddFindImage_Click);
            // 
            // comboBoxFindImages
            // 
            this.comboBoxFindImages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFindImages.FormattingEnabled = true;
            this.comboBoxFindImages.Location = new System.Drawing.Point(51, 44);
            this.comboBoxFindImages.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxFindImages.Name = "comboBoxFindImages";
            this.comboBoxFindImages.Size = new System.Drawing.Size(349, 21);
            this.comboBoxFindImages.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Образи:";
            // 
            // listBoxIngradienty
            // 
            this.listBoxIngradienty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxIngradienty.FormattingEnabled = true;
            this.listBoxIngradienty.Location = new System.Drawing.Point(6, 74);
            this.listBoxIngradienty.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBoxIngradienty.Name = "listBoxIngradienty";
            this.listBoxIngradienty.Size = new System.Drawing.Size(481, 108);
            this.listBoxIngradienty.TabIndex = 4;
            this.listBoxIngradienty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBoxIngradienty_KeyDown);
            // 
            // buttonGetByNameIngradient
            // 
            this.buttonGetByNameIngradient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGetByNameIngradient.Location = new System.Drawing.Point(406, 15);
            this.buttonGetByNameIngradient.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonGetByNameIngradient.Name = "buttonGetByNameIngradient";
            this.buttonGetByNameIngradient.Size = new System.Drawing.Size(81, 23);
            this.buttonGetByNameIngradient.TabIndex = 2;
            this.buttonGetByNameIngradient.Text = "Знайти";
            this.buttonGetByNameIngradient.UseVisualStyleBackColor = true;
            this.buttonGetByNameIngradient.Click += new System.EventHandler(this.buttonGetByNameIngradient_Click);
            // 
            // textBoxFindIngradientName
            // 
            this.textBoxFindIngradientName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFindIngradientName.Location = new System.Drawing.Point(51, 17);
            this.textBoxFindIngradientName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxFindIngradientName.Name = "textBoxFindIngradientName";
            this.textBoxFindIngradientName.Size = new System.Drawing.Size(349, 20);
            this.textBoxFindIngradientName.TabIndex = 1;
            this.textBoxFindIngradientName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxFindIngradientName_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Назва:";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(732, 608);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(141, 32);
            this.buttonClose.TabIndex = 9;
            this.buttonClose.Text = "Отмена";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // textBoxSynonymy
            // 
            this.textBoxSynonymy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSynonymy.Location = new System.Drawing.Point(67, 60);
            this.textBoxSynonymy.Name = "textBoxSynonymy";
            this.textBoxSynonymy.Size = new System.Drawing.Size(435, 20);
            this.textBoxSynonymy.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Синоніми:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Опис:";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Location = new System.Drawing.Point(67, 35);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(435, 20);
            this.textBoxDescription.TabIndex = 13;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.comboBoxPointerImage);
            this.groupBox4.Controls.Add(this.checkBoxIntermediate);
            this.groupBox4.Controls.Add(this.checkBoxPlural);
            this.groupBox4.Controls.Add(this.checkBoxPointer);
            this.groupBox4.Location = new System.Drawing.Point(8, 86);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(495, 43);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Мітки";
            // 
            // comboBoxPointerImage
            // 
            this.comboBoxPointerImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPointerImage.FormattingEnabled = true;
            this.comboBoxPointerImage.Location = new System.Drawing.Point(363, 13);
            this.comboBoxPointerImage.Name = "comboBoxPointerImage";
            this.comboBoxPointerImage.Size = new System.Drawing.Size(126, 21);
            this.comboBoxPointerImage.TabIndex = 3;
            // 
            // checkBoxIntermediate
            // 
            this.checkBoxIntermediate.AutoSize = true;
            this.checkBoxIntermediate.Location = new System.Drawing.Point(6, 15);
            this.checkBoxIntermediate.Name = "checkBoxIntermediate";
            this.checkBoxIntermediate.Size = new System.Drawing.Size(88, 17);
            this.checkBoxIntermediate.TabIndex = 2;
            this.checkBoxIntermediate.Text = "Посередник";
            this.checkBoxIntermediate.UseVisualStyleBackColor = true;
            // 
            // checkBoxPlural
            // 
            this.checkBoxPlural.AutoSize = true;
            this.checkBoxPlural.Location = new System.Drawing.Point(201, 15);
            this.checkBoxPlural.Name = "checkBoxPlural";
            this.checkBoxPlural.Size = new System.Drawing.Size(73, 17);
            this.checkBoxPlural.TabIndex = 1;
            this.checkBoxPlural.Text = "Множина";
            this.checkBoxPlural.UseVisualStyleBackColor = true;
            // 
            // checkBoxPointer
            // 
            this.checkBoxPointer.AutoSize = true;
            this.checkBoxPointer.Location = new System.Drawing.Point(280, 15);
            this.checkBoxPointer.Name = "checkBoxPointer";
            this.checkBoxPointer.Size = new System.Drawing.Size(77, 17);
            this.checkBoxPointer.TabIndex = 0;
            this.checkBoxPointer.Text = "Вказівник";
            this.checkBoxPointer.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(7, 6);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox5);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxImageName);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxSynonymy);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxDescription);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(1024, 590);
            this.splitContainer1.SplitterDistance = 505;
            this.splitContainer1.TabIndex = 20;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.controlContextContext);
            this.groupBox5.Controls.Add(this.controlContextLink);
            this.groupBox5.Location = new System.Drawing.Point(8, 178);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(494, 49);
            this.groupBox5.TabIndex = 20;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Контексти:";
            // 
            // controlContextContext
            // 
            this.controlContextContext.ImageContextSelect = null;
            this.controlContextContext.Location = new System.Drawing.Point(6, 19);
            this.controlContextContext.Name = "controlContextContext";
            this.controlContextContext.Size = new System.Drawing.Size(273, 21);
            this.controlContextContext.TabIndex = 18;
            this.controlContextContext.TitleText = "Контекст: ";
            // 
            // controlContextLink
            // 
            this.controlContextLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlContextLink.ImageContextSelect = null;
            this.controlContextLink.Location = new System.Drawing.Point(280, 19);
            this.controlContextLink.Name = "controlContextLink";
            this.controlContextLink.Size = new System.Drawing.Size(208, 21);
            this.controlContextLink.TabIndex = 17;
            this.controlContextLink.TitleText = "Контекст: ";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox6);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox7);
            this.splitContainer2.Size = new System.Drawing.Size(515, 590);
            this.splitContainer2.SplitterDistance = 285;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Location = new System.Drawing.Point(3, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(279, 160);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Посередники";
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.treeViewLinks);
            this.groupBox7.Location = new System.Drawing.Point(3, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(220, 581);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Дерево";
            // 
            // treeViewLinks
            // 
            this.treeViewLinks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewLinks.BackColor = System.Drawing.SystemColors.Info;
            this.treeViewLinks.Location = new System.Drawing.Point(6, 19);
            this.treeViewLinks.Name = "treeViewLinks";
            this.treeViewLinks.Size = new System.Drawing.Size(208, 556);
            this.treeViewLinks.TabIndex = 0;
            // 
            // frmImageElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 651);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonClose);
            this.Name = "frmImageElement";
            this.Text = "Образи";
            this.Load += new System.EventHandler(this.ImageElement_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCharList)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void TextBoxFindIngradientName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxObject;
        private System.Windows.Forms.CheckBox checkBoxAbstract;
        private System.Windows.Forms.CheckBox checkBoxProces;
        private System.Windows.Forms.CheckBox checkBoxMulty;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridViewCharList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxImageName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox listBoxIngradienty;
        private System.Windows.Forms.Button buttonGetByNameIngradient;
        private System.Windows.Forms.TextBox textBoxFindIngradientName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonAddFindImage;
        private System.Windows.Forms.ComboBox comboBoxFindImages;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonAddNewChar;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TextBox textBoxSynonymy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxDescription;
        private ControlContext controlContextLink;
        private ControlContext controlContextContext;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkBoxPointer;
        private System.Windows.Forms.CheckBox checkBoxIntermediate;
        private System.Windows.Forms.CheckBox checkBoxPlural;
        private System.Windows.Forms.ComboBox comboBoxPointerImage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TreeView treeViewLinks;
    }
}