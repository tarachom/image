namespace ImageForms
{
    partial class FormPicturesList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPicturesList));
            this.dataGridViewTemplateList = new System.Windows.Forms.DataGridView();
            this.Template_colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Template_colTemplate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStripButtonAddPicture = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonRefreshTemplateList = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSearsh = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridViewPictures = new System.Windows.Forms.DataGridView();
            this.Pictures_colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pictures_colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTemplateList)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPictures)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewTemplateList
            // 
            this.dataGridViewTemplateList.AllowUserToAddRows = false;
            this.dataGridViewTemplateList.AllowUserToDeleteRows = false;
            this.dataGridViewTemplateList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewTemplateList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTemplateList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Template_colID,
            this.Template_colTemplate});
            this.dataGridViewTemplateList.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewTemplateList.Name = "dataGridViewTemplateList";
            this.dataGridViewTemplateList.ReadOnly = true;
            this.dataGridViewTemplateList.Size = new System.Drawing.Size(355, 377);
            this.dataGridViewTemplateList.TabIndex = 0;
            this.dataGridViewTemplateList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTemplateList_CellDoubleClick);
            this.dataGridViewTemplateList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewTemplateList_KeyDown);
            // 
            // Template_colID
            // 
            this.Template_colID.HeaderText = "ID";
            this.Template_colID.Name = "Template_colID";
            this.Template_colID.ReadOnly = true;
            this.Template_colID.Width = 50;
            // 
            // Template_colTemplate
            // 
            this.Template_colTemplate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Template_colTemplate.HeaderText = "Заготовка";
            this.Template_colTemplate.Name = "Template_colTemplate";
            this.Template_colTemplate.ReadOnly = true;
            // 
            // toolStripButtonAddPicture
            // 
            this.toolStripButtonAddPicture.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddPicture.Image")));
            this.toolStripButtonAddPicture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddPicture.Name = "toolStripButtonAddPicture";
            this.toolStripButtonAddPicture.Size = new System.Drawing.Size(121, 22);
            this.toolStripButtonAddPicture.Text = "Додати малюнок";
            this.toolStripButtonAddPicture.Click += new System.EventHandler(this.toolStripButtonAddPicture_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAddPicture,
            this.toolStripSeparator1,
            this.toolStripButtonRefreshTemplateList});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(793, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonRefreshTemplateList
            // 
            this.toolStripButtonRefreshTemplateList.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRefreshTemplateList.Image")));
            this.toolStripButtonRefreshTemplateList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefreshTemplateList.Name = "toolStripButtonRefreshTemplateList";
            this.toolStripButtonRefreshTemplateList.Size = new System.Drawing.Size(127, 22);
            this.toolStripButtonRefreshTemplateList.Text = "Обновити таблиці";
            this.toolStripButtonRefreshTemplateList.Click += new System.EventHandler(this.toolStripButtonRefreshTemplateList_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Пошук:";
            // 
            // textBoxSearsh
            // 
            this.textBoxSearsh.Location = new System.Drawing.Point(61, 37);
            this.textBoxSearsh.Name = "textBoxSearsh";
            this.textBoxSearsh.Size = new System.Drawing.Size(457, 20);
            this.textBoxSearsh.TabIndex = 3;
            this.textBoxSearsh.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSearch_KeyDown);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(524, 34);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(65, 25);
            this.buttonSearch.TabIndex = 4;
            this.buttonSearch.Text = "Пошук";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 67);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewPictures);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewTemplateList);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(769, 383);
            this.splitContainer1.SplitterDistance = 404;
            this.splitContainer1.TabIndex = 5;
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
            this.Pictures_colID,
            this.Pictures_colName});
            this.dataGridViewPictures.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewPictures.Name = "dataGridViewPictures";
            this.dataGridViewPictures.ReadOnly = true;
            this.dataGridViewPictures.Size = new System.Drawing.Size(398, 377);
            this.dataGridViewPictures.TabIndex = 0;
            this.dataGridViewPictures.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewPictures_CellMouseDoubleClick);
            this.dataGridViewPictures.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewPictures_KeyDown);
            // 
            // Pictures_colID
            // 
            this.Pictures_colID.HeaderText = "ID";
            this.Pictures_colID.Name = "Pictures_colID";
            this.Pictures_colID.ReadOnly = true;
            this.Pictures_colID.Width = 50;
            // 
            // Pictures_colName
            // 
            this.Pictures_colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Pictures_colName.HeaderText = "Name";
            this.Pictures_colName.Name = "Pictures_colName";
            this.Pictures_colName.ReadOnly = true;
            // 
            // FormPicturesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 462);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textBoxSearsh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormPicturesList";
            this.Text = "FormPicturesList";
            this.Load += new System.EventHandler(this.FormPicturesList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTemplateList)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPictures)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewTemplateList;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddPicture;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSearsh;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefreshTemplateList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridViewPictures;
        private System.Windows.Forms.DataGridViewTextBoxColumn Template_colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Template_colTemplate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pictures_colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pictures_colName;
    }
}