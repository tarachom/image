namespace ImageForms
{
    partial class FormContextElement
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
            this.components = new System.ComponentModel.Container();
            this.labelName = new System.Windows.Forms.Label();
            this.labelDesc = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxContextName = new System.Windows.Forms.TextBox();
            this.textBoxContextDescription = new System.Windows.Forms.TextBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 28);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(42, 13);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Назва:";
            // 
            // labelDesc
            // 
            this.labelDesc.AutoSize = true;
            this.labelDesc.Location = new System.Drawing.Point(12, 61);
            this.labelDesc.Name = "labelDesc";
            this.labelDesc.Size = new System.Drawing.Size(36, 13);
            this.labelDesc.TabIndex = 1;
            this.labelDesc.Text = "Опис:";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(65, 149);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(141, 37);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "ЗАПИСАТИ";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxContextName
            // 
            this.textBoxContextName.Location = new System.Drawing.Point(65, 25);
            this.textBoxContextName.Name = "textBoxContextName";
            this.textBoxContextName.Size = new System.Drawing.Size(515, 20);
            this.textBoxContextName.TabIndex = 3;
            // 
            // textBoxContextDescription
            // 
            this.textBoxContextDescription.Location = new System.Drawing.Point(65, 58);
            this.textBoxContextDescription.Multiline = true;
            this.textBoxContextDescription.Name = "textBoxContextDescription";
            this.textBoxContextDescription.Size = new System.Drawing.Size(515, 72);
            this.textBoxContextDescription.TabIndex = 4;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(439, 149);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(141, 37);
            this.buttonClose.TabIndex = 5;
            this.buttonClose.Text = "Закрити";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 26);
            // 
            // FormContextElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 198);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.textBoxContextDescription);
            this.Controls.Add(this.textBoxContextName);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelDesc);
            this.Controls.Add(this.labelName);
            this.MaximizeBox = false;
            this.Name = "FormContextElement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormContextElement";
            this.Load += new System.EventHandler(this.FormContextElement_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelDesc;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxContextName;
        private System.Windows.Forms.TextBox textBoxContextDescription;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}