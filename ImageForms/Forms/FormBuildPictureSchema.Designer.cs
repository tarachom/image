namespace ImageForms
{
    partial class FormBuildPictureSchema
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
            this.treeViewSchema = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeViewSchema
            // 
            this.treeViewSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewSchema.BackColor = System.Drawing.SystemColors.Info;
            this.treeViewSchema.CheckBoxes = true;
            this.treeViewSchema.Location = new System.Drawing.Point(10, 8);
            this.treeViewSchema.Name = "treeViewSchema";
            this.treeViewSchema.Size = new System.Drawing.Size(641, 456);
            this.treeViewSchema.TabIndex = 0;
            // 
            // FormBuildPictureSchema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 470);
            this.Controls.Add(this.treeViewSchema);
            this.Name = "FormBuildPictureSchema";
            this.Text = "FormBuildPictureSchema";
            this.Load += new System.EventHandler(this.FormBuildPictureSchema_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewSchema;
    }
}