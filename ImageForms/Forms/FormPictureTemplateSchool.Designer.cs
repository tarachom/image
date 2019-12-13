namespace ImageForms
{
    partial class FormPictureTemplateSchool
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
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxTemplateText = new System.Windows.Forms.TextBox();
            this.buttonAnalizeProcess = new System.Windows.Forms.Button();
            this.dataGridViewWords = new System.Windows.Forms.DataGridView();
            this.Words_Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Words_word = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Words_CreatePictures = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Words_CreateImage = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.buttonAddToTemplates = new System.Windows.Forms.Button();
            this.buttonCreateImage = new System.Windows.Forms.Button();
            this.buttonCreatePictures = new System.Windows.Forms.Button();
            this.buttonSavePictures = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWords)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "ЗАГОТОВКА:";
            // 
            // textBoxTemplateText
            // 
            this.textBoxTemplateText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTemplateText.Location = new System.Drawing.Point(92, 15);
            this.textBoxTemplateText.Multiline = true;
            this.textBoxTemplateText.Name = "textBoxTemplateText";
            this.textBoxTemplateText.Size = new System.Drawing.Size(600, 73);
            this.textBoxTemplateText.TabIndex = 2;
            // 
            // buttonAnalizeProcess
            // 
            this.buttonAnalizeProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAnalizeProcess.Location = new System.Drawing.Point(703, 52);
            this.buttonAnalizeProcess.Name = "buttonAnalizeProcess";
            this.buttonAnalizeProcess.Size = new System.Drawing.Size(104, 37);
            this.buttonAnalizeProcess.TabIndex = 3;
            this.buttonAnalizeProcess.Text = "Аналізувати";
            this.buttonAnalizeProcess.UseVisualStyleBackColor = true;
            this.buttonAnalizeProcess.Click += new System.EventHandler(this.buttonAnalizeProcess_Click);
            // 
            // dataGridViewWords
            // 
            this.dataGridViewWords.AllowUserToAddRows = false;
            this.dataGridViewWords.AllowUserToDeleteRows = false;
            this.dataGridViewWords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewWords.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Words_Index,
            this.Words_word,
            this.Words_CreatePictures,
            this.Words_CreateImage});
            this.dataGridViewWords.Location = new System.Drawing.Point(92, 110);
            this.dataGridViewWords.Name = "dataGridViewWords";
            this.dataGridViewWords.Size = new System.Drawing.Size(487, 320);
            this.dataGridViewWords.TabIndex = 4;
            this.dataGridViewWords.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewWords_KeyDown);
            // 
            // Words_Index
            // 
            this.Words_Index.HeaderText = "№";
            this.Words_Index.Name = "Words_Index";
            this.Words_Index.Width = 30;
            // 
            // Words_word
            // 
            this.Words_word.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Words_word.HeaderText = "Слова";
            this.Words_word.Name = "Words_word";
            // 
            // Words_CreatePictures
            // 
            this.Words_CreatePictures.HeaderText = "Створити Малюнок";
            this.Words_CreatePictures.Name = "Words_CreatePictures";
            this.Words_CreatePictures.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Words_CreatePictures.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Words_CreatePictures.Width = 60;
            // 
            // Words_CreateImage
            // 
            this.Words_CreateImage.HeaderText = "Створити Образ";
            this.Words_CreateImage.Name = "Words_CreateImage";
            this.Words_CreateImage.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Words_CreateImage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Words_CreateImage.Width = 60;
            // 
            // buttonAddToTemplates
            // 
            this.buttonAddToTemplates.Location = new System.Drawing.Point(585, 110);
            this.buttonAddToTemplates.Name = "buttonAddToTemplates";
            this.buttonAddToTemplates.Size = new System.Drawing.Size(129, 37);
            this.buttonAddToTemplates.TabIndex = 5;
            this.buttonAddToTemplates.Text = "Додати в заготовки";
            this.buttonAddToTemplates.UseVisualStyleBackColor = true;
            this.buttonAddToTemplates.Click += new System.EventHandler(this.buttonAddToTemplates_Click);
            // 
            // buttonCreateImage
            // 
            this.buttonCreateImage.Location = new System.Drawing.Point(585, 171);
            this.buttonCreateImage.Name = "buttonCreateImage";
            this.buttonCreateImage.Size = new System.Drawing.Size(129, 37);
            this.buttonCreateImage.TabIndex = 6;
            this.buttonCreateImage.Text = "Створити образи";
            this.buttonCreateImage.UseVisualStyleBackColor = true;
            this.buttonCreateImage.Click += new System.EventHandler(this.buttonCreateImage_Click);
            // 
            // buttonCreatePictures
            // 
            this.buttonCreatePictures.Location = new System.Drawing.Point(585, 236);
            this.buttonCreatePictures.Name = "buttonCreatePictures";
            this.buttonCreatePictures.Size = new System.Drawing.Size(129, 37);
            this.buttonCreatePictures.TabIndex = 7;
            this.buttonCreatePictures.Text = "Створити малюнки";
            this.buttonCreatePictures.UseVisualStyleBackColor = true;
            this.buttonCreatePictures.Click += new System.EventHandler(this.buttonCreatePictures_Click);
            // 
            // buttonSavePictures
            // 
            this.buttonSavePictures.Location = new System.Drawing.Point(585, 351);
            this.buttonSavePictures.Name = "buttonSavePictures";
            this.buttonSavePictures.Size = new System.Drawing.Size(129, 37);
            this.buttonSavePictures.TabIndex = 8;
            this.buttonSavePictures.Text = "Записати малюнки";
            this.buttonSavePictures.UseVisualStyleBackColor = true;
            this.buttonSavePictures.Click += new System.EventHandler(this.buttonSavePictures_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(698, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 36);
            this.button1.TabIndex = 9;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormPictureTemplateSchool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 461);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonSavePictures);
            this.Controls.Add(this.buttonCreatePictures);
            this.Controls.Add(this.buttonCreateImage);
            this.Controls.Add(this.buttonAddToTemplates);
            this.Controls.Add(this.dataGridViewWords);
            this.Controls.Add(this.buttonAnalizeProcess);
            this.Controls.Add(this.textBoxTemplateText);
            this.Controls.Add(this.label2);
            this.Name = "FormPictureTemplateSchool";
            this.Text = "FormPictureTemplateSchool";
            this.Load += new System.EventHandler(this.FormPictureTemplateSchool_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWords)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxTemplateText;
        private System.Windows.Forms.Button buttonAnalizeProcess;
        private System.Windows.Forms.DataGridView dataGridViewWords;
        private System.Windows.Forms.DataGridViewTextBoxColumn Words_Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn Words_word;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Words_CreatePictures;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Words_CreateImage;
        private System.Windows.Forms.Button buttonAddToTemplates;
        private System.Windows.Forms.Button buttonCreateImage;
        private System.Windows.Forms.Button buttonCreatePictures;
        private System.Windows.Forms.Button buttonSavePictures;
        private System.Windows.Forms.Button button1;
    }
}