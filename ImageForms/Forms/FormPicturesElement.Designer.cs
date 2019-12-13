namespace ImageForms
{
    partial class FormPicturesElement
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
            this.textBoxPictureDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPictureName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.buttonAddPicture = new System.Windows.Forms.Button();
            this.dataGridViewPictures = new System.Windows.Forms.DataGridView();
            this.Pictures_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pictures_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.controlSearchPictures = new ImageForms.ControlSearch();
            this.buttonAddImage = new System.Windows.Forms.Button();
            this.dataGridViewImages = new System.Windows.Forms.DataGridView();
            this.Images_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Images_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.controlSearchImage = new ImageForms.ControlSearch();
            this.buttonBuildPictureSchema = new System.Windows.Forms.Button();
            this.buttonCreateUnionItem = new System.Windows.Forms.Button();
            this.buttonOneSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPictures)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewImages)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxPictureDescription
            // 
            this.textBoxPictureDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPictureDescription.Location = new System.Drawing.Point(109, 42);
            this.textBoxPictureDescription.Name = "textBoxPictureDescription";
            this.textBoxPictureDescription.Size = new System.Drawing.Size(691, 20);
            this.textBoxPictureDescription.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Опис:";
            // 
            // textBoxPictureName
            // 
            this.textBoxPictureName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPictureName.Location = new System.Drawing.Point(109, 9);
            this.textBoxPictureName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxPictureName.Name = "textBoxPictureName";
            this.textBoxPictureName.Size = new System.Drawing.Size(691, 20);
            this.textBoxPictureName.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Назва малюнку:";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(365, 352);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(141, 32);
            this.buttonClose.TabIndex = 19;
            this.buttonClose.Text = "Отмена";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(659, 352);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(141, 32);
            this.buttonSave.TabIndex = 18;
            this.buttonSave.Text = "Записати і закрити";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(16, 68);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.buttonAddPicture);
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewPictures);
            this.splitContainer1.Panel1.Controls.Add(this.controlSearchPictures);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.buttonAddImage);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewImages);
            this.splitContainer1.Panel2.Controls.Add(this.controlSearchImage);
            this.splitContainer1.Size = new System.Drawing.Size(784, 273);
            this.splitContainer1.SplitterDistance = 390;
            this.splitContainer1.TabIndex = 20;
            // 
            // buttonAddPicture
            // 
            this.buttonAddPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddPicture.Location = new System.Drawing.Point(326, 11);
            this.buttonAddPicture.Name = "buttonAddPicture";
            this.buttonAddPicture.Size = new System.Drawing.Size(60, 22);
            this.buttonAddPicture.TabIndex = 1;
            this.buttonAddPicture.Text = "Додати";
            this.buttonAddPicture.UseVisualStyleBackColor = true;
            this.buttonAddPicture.Click += new System.EventHandler(this.buttonAddPicture_Click);
            // 
            // dataGridViewPictures
            // 
            this.dataGridViewPictures.AllowUserToAddRows = false;
            this.dataGridViewPictures.AllowUserToDeleteRows = false;
            this.dataGridViewPictures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPictures.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPictures.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Pictures_ID,
            this.Pictures_Name});
            this.dataGridViewPictures.Location = new System.Drawing.Point(3, 40);
            this.dataGridViewPictures.Name = "dataGridViewPictures";
            this.dataGridViewPictures.ReadOnly = true;
            this.dataGridViewPictures.Size = new System.Drawing.Size(383, 230);
            this.dataGridViewPictures.TabIndex = 0;
            this.dataGridViewPictures.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewPictures_KeyDown);
            // 
            // Pictures_ID
            // 
            this.Pictures_ID.HeaderText = "ID";
            this.Pictures_ID.Name = "Pictures_ID";
            this.Pictures_ID.ReadOnly = true;
            this.Pictures_ID.Width = 50;
            // 
            // Pictures_Name
            // 
            this.Pictures_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Pictures_Name.HeaderText = "Малюнки";
            this.Pictures_Name.Name = "Pictures_Name";
            this.Pictures_Name.ReadOnly = true;
            // 
            // controlSearchPictures
            // 
            this.controlSearchPictures.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlSearchPictures.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.controlSearchPictures.Location = new System.Drawing.Point(3, 12);
            this.controlSearchPictures.Margin = new System.Windows.Forms.Padding(0);
            this.controlSearchPictures.Name = "controlSearchPictures";
            this.controlSearchPictures.SelectSearchElement = null;
            this.controlSearchPictures.Size = new System.Drawing.Size(321, 258);
            this.controlSearchPictures.TabIndex = 22;
            this.controlSearchPictures.TableName = null;
            // 
            // buttonAddImage
            // 
            this.buttonAddImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddImage.Location = new System.Drawing.Point(327, 11);
            this.buttonAddImage.Name = "buttonAddImage";
            this.buttonAddImage.Size = new System.Drawing.Size(60, 22);
            this.buttonAddImage.TabIndex = 2;
            this.buttonAddImage.Text = "Додати";
            this.buttonAddImage.UseVisualStyleBackColor = true;
            this.buttonAddImage.Click += new System.EventHandler(this.buttonAddImage_Click);
            // 
            // dataGridViewImages
            // 
            this.dataGridViewImages.AllowUserToAddRows = false;
            this.dataGridViewImages.AllowUserToDeleteRows = false;
            this.dataGridViewImages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewImages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewImages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Images_ID,
            this.Images_Name});
            this.dataGridViewImages.Location = new System.Drawing.Point(3, 40);
            this.dataGridViewImages.Name = "dataGridViewImages";
            this.dataGridViewImages.ReadOnly = true;
            this.dataGridViewImages.Size = new System.Drawing.Size(383, 230);
            this.dataGridViewImages.TabIndex = 0;
            this.dataGridViewImages.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewImages_KeyDown);
            // 
            // Images_ID
            // 
            this.Images_ID.HeaderText = "ID";
            this.Images_ID.Name = "Images_ID";
            this.Images_ID.ReadOnly = true;
            this.Images_ID.Width = 50;
            // 
            // Images_Name
            // 
            this.Images_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Images_Name.HeaderText = "Образи";
            this.Images_Name.Name = "Images_Name";
            this.Images_Name.ReadOnly = true;
            // 
            // controlSearchImage
            // 
            this.controlSearchImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlSearchImage.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.controlSearchImage.Location = new System.Drawing.Point(3, 12);
            this.controlSearchImage.Margin = new System.Windows.Forms.Padding(0);
            this.controlSearchImage.Name = "controlSearchImage";
            this.controlSearchImage.SelectSearchElement = null;
            this.controlSearchImage.Size = new System.Drawing.Size(321, 258);
            this.controlSearchImage.TabIndex = 3;
            this.controlSearchImage.TableName = null;
            // 
            // buttonBuildPictureSchema
            // 
            this.buttonBuildPictureSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonBuildPictureSchema.Location = new System.Drawing.Point(19, 352);
            this.buttonBuildPictureSchema.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonBuildPictureSchema.Name = "buttonBuildPictureSchema";
            this.buttonBuildPictureSchema.Size = new System.Drawing.Size(141, 32);
            this.buttonBuildPictureSchema.TabIndex = 21;
            this.buttonBuildPictureSchema.Text = "Побудувати схему";
            this.buttonBuildPictureSchema.UseVisualStyleBackColor = true;
            this.buttonBuildPictureSchema.Click += new System.EventHandler(this.buttonBuildPictureSchema_Click);
            // 
            // buttonCreateUnionItem
            // 
            this.buttonCreateUnionItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCreateUnionItem.Location = new System.Drawing.Point(166, 352);
            this.buttonCreateUnionItem.Name = "buttonCreateUnionItem";
            this.buttonCreateUnionItem.Size = new System.Drawing.Size(126, 32);
            this.buttonCreateUnionItem.TabIndex = 24;
            this.buttonCreateUnionItem.Text = "Обєднання";
            this.buttonCreateUnionItem.UseVisualStyleBackColor = true;
            this.buttonCreateUnionItem.Click += new System.EventHandler(this.buttonAddUnionItem_Click);
            // 
            // buttonOneSave
            // 
            this.buttonOneSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOneSave.Location = new System.Drawing.Point(512, 352);
            this.buttonOneSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonOneSave.Name = "buttonOneSave";
            this.buttonOneSave.Size = new System.Drawing.Size(141, 32);
            this.buttonOneSave.TabIndex = 24;
            this.buttonOneSave.Text = "Записати";
            this.buttonOneSave.UseVisualStyleBackColor = true;
            this.buttonOneSave.Click += new System.EventHandler(this.buttonOneSave_Click);
            // 
            // FormPicturesElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 395);
            this.Controls.Add(this.buttonCreateUnionItem);
            this.Controls.Add(this.buttonOneSave);
            this.Controls.Add(this.buttonBuildPictureSchema);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxPictureDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxPictureName);
            this.Controls.Add(this.label4);
            this.Name = "FormPicturesElement";
            this.Text = "FormPicturesElement";
            this.Load += new System.EventHandler(this.FormPicturesElement_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPictures)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewImages)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPictureDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPictureName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridViewPictures;
        private System.Windows.Forms.DataGridView dataGridViewImages;
        private System.Windows.Forms.Button buttonAddPicture;
        private System.Windows.Forms.Button buttonAddImage;
        private ControlSearch controlSearchPictures;
        private ControlSearch controlSearchImage;
        private System.Windows.Forms.Button buttonBuildPictureSchema;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pictures_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pictures_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Images_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Images_Name;
        private System.Windows.Forms.Button buttonCreateUnionItem;
        private System.Windows.Forms.Button buttonOneSave;
    }
}