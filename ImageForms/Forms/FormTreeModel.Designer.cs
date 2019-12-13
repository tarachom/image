namespace ImageForms
{
    partial class FormTreeModel
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
            this.treeViewModel = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeViewModel
            // 
            this.treeViewModel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewModel.Location = new System.Drawing.Point(12, 12);
            this.treeViewModel.Name = "treeViewModel";
            this.treeViewModel.Size = new System.Drawing.Size(573, 423);
            this.treeViewModel.TabIndex = 0;
            this.treeViewModel.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewModel_BeforeExpand);
            this.treeViewModel.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeViewModel_AfterExpand);
            this.treeViewModel.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewModel_NodeMouseClick);
            this.treeViewModel.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewModel_NodeMouseDoubleClick);
            // 
            // FormTreeModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 447);
            this.Controls.Add(this.treeViewModel);
            this.Name = "FormTreeModel";
            this.Text = "FormTreeModel";
            this.Load += new System.EventHandler(this.FormTreeModel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewModel;
    }
}