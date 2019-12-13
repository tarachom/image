namespace ImageForms
{
    partial class FormPicturesElementUnionTrack
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
            this.buttonCreateResult = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewPictures = new System.Windows.Forms.TreeView();
            this.treeViewResult = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonDeleteSelectForResult = new System.Windows.Forms.Button();
            this.buttonAddField = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCreateResult
            // 
            this.buttonCreateResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCreateResult.Location = new System.Drawing.Point(6, 11);
            this.buttonCreateResult.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonCreateResult.Name = "buttonCreateResult";
            this.buttonCreateResult.Size = new System.Drawing.Size(113, 24);
            this.buttonCreateResult.TabIndex = 21;
            this.buttonCreateResult.Text = "Копіювати вибрані";
            this.buttonCreateResult.UseVisualStyleBackColor = true;
            this.buttonCreateResult.Click += new System.EventHandler(this.buttonCreateResult_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(636, 456);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(141, 32);
            this.buttonSave.TabIndex = 20;
            this.buttonSave.Text = "Записати і закрити";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.treeViewPictures);
            this.splitContainer1.Panel1.Controls.Add(this.buttonCreateResult);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.buttonAddField);
            this.splitContainer1.Panel2.Controls.Add(this.buttonDeleteSelectForResult);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.treeViewResult);
            this.splitContainer1.Size = new System.Drawing.Size(760, 422);
            this.splitContainer1.SplitterDistance = 376;
            this.splitContainer1.TabIndex = 24;
            // 
            // treeViewPictures
            // 
            this.treeViewPictures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewPictures.BackColor = System.Drawing.SystemColors.Info;
            this.treeViewPictures.CheckBoxes = true;
            this.treeViewPictures.Location = new System.Drawing.Point(3, 68);
            this.treeViewPictures.Name = "treeViewPictures";
            this.treeViewPictures.Size = new System.Drawing.Size(370, 351);
            this.treeViewPictures.TabIndex = 0;
            // 
            // treeViewResult
            // 
            this.treeViewResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewResult.BackColor = System.Drawing.SystemColors.Info;
            this.treeViewResult.CheckBoxes = true;
            this.treeViewResult.Location = new System.Drawing.Point(3, 68);
            this.treeViewResult.Name = "treeViewResult";
            this.treeViewResult.Size = new System.Drawing.Size(374, 351);
            this.treeViewResult.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(489, 456);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 32);
            this.button1.TabIndex = 25;
            this.button1.Text = "Отмена";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Елементи:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Результати:";
            // 
            // buttonDeleteSelectForResult
            // 
            this.buttonDeleteSelectForResult.Location = new System.Drawing.Point(6, 11);
            this.buttonDeleteSelectForResult.Name = "buttonDeleteSelectForResult";
            this.buttonDeleteSelectForResult.Size = new System.Drawing.Size(121, 24);
            this.buttonDeleteSelectForResult.TabIndex = 22;
            this.buttonDeleteSelectForResult.Text = "Видалити відмічені";
            this.buttonDeleteSelectForResult.UseVisualStyleBackColor = true;
            this.buttonDeleteSelectForResult.Click += new System.EventHandler(this.buttonDeleteSelectForResult_Click);
            // 
            // buttonAddField
            // 
            this.buttonAddField.Location = new System.Drawing.Point(133, 11);
            this.buttonAddField.Name = "buttonAddField";
            this.buttonAddField.Size = new System.Drawing.Size(121, 24);
            this.buttonAddField.TabIndex = 23;
            this.buttonAddField.Text = "Додати нове поле";
            this.buttonAddField.UseVisualStyleBackColor = true;
            // 
            // FormPicturesElementUnionTrack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 499);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonSave);
            this.Name = "FormPicturesElementUnionTrack";
            this.Text = "FormPicturesElementUnionTrack";
            this.Load += new System.EventHandler(this.FormPicturesElementUnionTrack_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonCreateResult;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewPictures;
        private System.Windows.Forms.TreeView treeViewResult;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonDeleteSelectForResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonAddField;
    }
}