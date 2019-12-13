namespace ImageForms
{
    partial class FormContext
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormContext));
            this.dataGridViewContext = new System.Windows.Forms.DataGridView();
            this.colContextID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContextName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContextDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewContext)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewContext
            // 
            this.dataGridViewContext.AllowUserToAddRows = false;
            this.dataGridViewContext.AllowUserToDeleteRows = false;
            this.dataGridViewContext.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewContext.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewContext.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colContextID,
            this.colContextName,
            this.colContextDescription});
            this.dataGridViewContext.Location = new System.Drawing.Point(12, 28);
            this.dataGridViewContext.Name = "dataGridViewContext";
            this.dataGridViewContext.ReadOnly = true;
            this.dataGridViewContext.Size = new System.Drawing.Size(678, 322);
            this.dataGridViewContext.TabIndex = 0;
            this.dataGridViewContext.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewContext_CellDoubleClick);
            this.dataGridViewContext.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewContext_KeyDown);
            // 
            // colContextID
            // 
            this.colContextID.HeaderText = "ID";
            this.colContextID.Name = "colContextID";
            this.colContextID.ReadOnly = true;
            this.colContextID.Width = 50;
            // 
            // colContextName
            // 
            this.colContextName.HeaderText = "Name";
            this.colContextName.Name = "colContextName";
            this.colContextName.ReadOnly = true;
            this.colContextName.Width = 300;
            // 
            // colContextDescription
            // 
            this.colContextDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colContextDescription.HeaderText = "Description";
            this.colContextDescription.Name = "colContextDescription";
            this.colContextDescription.ReadOnly = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAdd,
            this.toolStripSeparator1,
            this.toolStripButtonRefresh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(702, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripButtonAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAdd.Image")));
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Size = new System.Drawing.Size(117, 22);
            this.toolStripButtonAdd.Text = "Додати контекст";
            this.toolStripButtonAdd.Click += new System.EventHandler(this.toolStripButtonAdd_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRefresh.Image")));
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(82, 22);
            this.toolStripButtonRefresh.Text = "Обновити";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
            // 
            // FormContext
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 362);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dataGridViewContext);
            this.Name = "FormContext";
            this.Text = "FormContext";
            this.Load += new System.EventHandler(this.FormContext_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewContext)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewContext;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContextID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContextName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContextDescription;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
    }
}