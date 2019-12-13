namespace ImageForms
{
    partial class FormFirst
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFirst));
            this.treeViewImages = new System.Windows.Forms.TreeView();
            this.toolStripGeneral = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonContextList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPictureList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonOpenFormTreeModel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonFixed = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSearch = new System.Windows.Forms.ToolStripButton();
            this.contextMenuForTreeNode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dataGridViewEventMessage = new System.Windows.Forms.DataGridView();
            this.EventJournal_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventJournal_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventJournal_DataTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventJournal_Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventJournal_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBoxSearsch = new System.Windows.Forms.TextBox();
            this.treeViewAbstarctImages = new System.Windows.Forms.TreeView();
            this.treeViewImageInfo = new System.Windows.Forms.TreeView();
            this.toolStripGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEventMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewImages
            // 
            this.treeViewImages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewImages.Location = new System.Drawing.Point(723, 28);
            this.treeViewImages.Name = "treeViewImages";
            this.treeViewImages.Size = new System.Drawing.Size(251, 315);
            this.treeViewImages.TabIndex = 1;
            this.treeViewImages.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeViewImages_NodeMouseClick);
            this.treeViewImages.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewImages_NodeMouseDoubleClick);
            // 
            // toolStripGeneral
            // 
            this.toolStripGeneral.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonRefresh,
            this.toolStripSeparator1,
            this.toolStripButton1,
            this.toolStripSeparator2,
            this.toolStripButtonContextList,
            this.toolStripSeparator3,
            this.toolStripButtonPictureList,
            this.toolStripSeparator4,
            this.toolStripButtonOpenFormTreeModel,
            this.toolStripSeparator5,
            this.toolStripButtonFixed,
            this.toolStripSeparator6,
            this.toolStripButtonSearch});
            this.toolStripGeneral.Location = new System.Drawing.Point(0, 0);
            this.toolStripGeneral.Name = "toolStripGeneral";
            this.toolStripGeneral.Size = new System.Drawing.Size(986, 25);
            this.toolStripGeneral.TabIndex = 5;
            this.toolStripGeneral.Text = "toolStrip1";
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRefresh.Image")));
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(107, 22);
            this.toolStripButtonRefresh.Text = "Обновити дерево";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(82, 22);
            this.toolStripButton1.Text = "Новий образ";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonContextList
            // 
            this.toolStripButtonContextList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonContextList.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonContextList.Image")));
            this.toolStripButtonContextList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonContextList.Name = "toolStripButtonContextList";
            this.toolStripButtonContextList.Size = new System.Drawing.Size(67, 22);
            this.toolStripButtonContextList.Text = "Контексти";
            this.toolStripButtonContextList.Click += new System.EventHandler(this.toolStripButtonContextList_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonPictureList
            // 
            this.toolStripButtonPictureList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonPictureList.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPictureList.Image")));
            this.toolStripButtonPictureList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPictureList.Name = "toolStripButtonPictureList";
            this.toolStripButtonPictureList.Size = new System.Drawing.Size(65, 22);
            this.toolStripButtonPictureList.Text = "Малюнки";
            this.toolStripButtonPictureList.Click += new System.EventHandler(this.toolStripButtonPictureList_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonOpenFormTreeModel
            // 
            this.toolStripButtonOpenFormTreeModel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonOpenFormTreeModel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpenFormTreeModel.Image")));
            this.toolStripButtonOpenFormTreeModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenFormTreeModel.Name = "toolStripButtonOpenFormTreeModel";
            this.toolStripButtonOpenFormTreeModel.Size = new System.Drawing.Size(54, 22);
            this.toolStripButtonOpenFormTreeModel.Text = "Модель";
            this.toolStripButtonOpenFormTreeModel.Click += new System.EventHandler(this.toolStripButtonOpenFormTreeModel_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonFixed
            // 
            this.toolStripButtonFixed.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonFixed.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonFixed.Image")));
            this.toolStripButtonFixed.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFixed.Name = "toolStripButtonFixed";
            this.toolStripButtonFixed.Size = new System.Drawing.Size(91, 22);
            this.toolStripButtonFixed.Text = "ЗАФІКСУВАТИ";
            this.toolStripButtonFixed.Click += new System.EventHandler(this.toolStripButtonFixed_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonSearch
            // 
            this.toolStripButtonSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSearch.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSearch.Image")));
            this.toolStripButtonSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSearch.Name = "toolStripButtonSearch";
            this.toolStripButtonSearch.Size = new System.Drawing.Size(50, 22);
            this.toolStripButtonSearch.Text = "Пошук";
            this.toolStripButtonSearch.Click += new System.EventHandler(this.toolStripButtonSearch_Click);
            // 
            // contextMenuForTreeNode
            // 
            this.contextMenuForTreeNode.Name = "contextMenuForTreeNode";
            this.contextMenuForTreeNode.Size = new System.Drawing.Size(61, 4);
            // 
            // dataGridViewEventMessage
            // 
            this.dataGridViewEventMessage.AllowUserToAddRows = false;
            this.dataGridViewEventMessage.AllowUserToDeleteRows = false;
            this.dataGridViewEventMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewEventMessage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEventMessage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EventJournal_ID,
            this.EventJournal_Type,
            this.EventJournal_DataTime,
            this.EventJournal_Message,
            this.EventJournal_Description});
            this.dataGridViewEventMessage.Location = new System.Drawing.Point(12, 349);
            this.dataGridViewEventMessage.Name = "dataGridViewEventMessage";
            this.dataGridViewEventMessage.ReadOnly = true;
            this.dataGridViewEventMessage.Size = new System.Drawing.Size(962, 102);
            this.dataGridViewEventMessage.TabIndex = 6;
            // 
            // EventJournal_ID
            // 
            this.EventJournal_ID.HeaderText = "ID";
            this.EventJournal_ID.Name = "EventJournal_ID";
            this.EventJournal_ID.ReadOnly = true;
            this.EventJournal_ID.Width = 50;
            // 
            // EventJournal_Type
            // 
            this.EventJournal_Type.HeaderText = "Type";
            this.EventJournal_Type.Name = "EventJournal_Type";
            this.EventJournal_Type.ReadOnly = true;
            this.EventJournal_Type.Width = 50;
            // 
            // EventJournal_DataTime
            // 
            this.EventJournal_DataTime.HeaderText = "DataTime";
            this.EventJournal_DataTime.Name = "EventJournal_DataTime";
            this.EventJournal_DataTime.ReadOnly = true;
            // 
            // EventJournal_Message
            // 
            this.EventJournal_Message.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EventJournal_Message.HeaderText = "Message";
            this.EventJournal_Message.Name = "EventJournal_Message";
            this.EventJournal_Message.ReadOnly = true;
            // 
            // EventJournal_Description
            // 
            this.EventJournal_Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EventJournal_Description.HeaderText = "Description";
            this.EventJournal_Description.Name = "EventJournal_Description";
            this.EventJournal_Description.ReadOnly = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBoxSearsch);
            this.splitContainer1.Panel1.Controls.Add(this.treeViewAbstarctImages);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.treeViewImageInfo);
            this.splitContainer1.Size = new System.Drawing.Size(705, 315);
            this.splitContainer1.SplitterDistance = 126;
            this.splitContainer1.TabIndex = 7;
            // 
            // textBoxSearsch
            // 
            this.textBoxSearsch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearsch.Location = new System.Drawing.Point(4, 8);
            this.textBoxSearsch.Name = "textBoxSearsch";
            this.textBoxSearsch.Size = new System.Drawing.Size(119, 20);
            this.textBoxSearsch.TabIndex = 1;
            this.textBoxSearsch.TextChanged += new System.EventHandler(this.textBoxSearsch_TextChanged);
            // 
            // treeViewAbstarctImages
            // 
            this.treeViewAbstarctImages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewAbstarctImages.Location = new System.Drawing.Point(3, 34);
            this.treeViewAbstarctImages.Name = "treeViewAbstarctImages";
            this.treeViewAbstarctImages.Size = new System.Drawing.Size(120, 278);
            this.treeViewAbstarctImages.TabIndex = 0;
            this.treeViewAbstarctImages.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewAbstarctImages_NodeMouseClick);
            // 
            // treeViewImageInfo
            // 
            this.treeViewImageInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewImageInfo.BackColor = System.Drawing.SystemColors.Window;
            this.treeViewImageInfo.Location = new System.Drawing.Point(3, 3);
            this.treeViewImageInfo.Name = "treeViewImageInfo";
            this.treeViewImageInfo.Size = new System.Drawing.Size(569, 309);
            this.treeViewImageInfo.TabIndex = 0;
            // 
            // FormFirst
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 463);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.dataGridViewEventMessage);
            this.Controls.Add(this.toolStripGeneral);
            this.Controls.Add(this.treeViewImages);
            this.Name = "FormFirst";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Основна форма";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormFirst_Load);
            this.toolStripGeneral.ResumeLayout(false);
            this.toolStripGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEventMessage)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TreeView treeViewImages;
        private System.Windows.Forms.ToolStrip toolStripGeneral;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonContextList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonPictureList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpenFormTreeModel;
        private System.Windows.Forms.ContextMenuStrip contextMenuForTreeNode;
        private System.Windows.Forms.DataGridView dataGridViewEventMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventJournal_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventJournal_Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventJournal_DataTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventJournal_Message;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventJournal_Description;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButtonFixed;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewAbstarctImages;
        private System.Windows.Forms.TreeView treeViewImageInfo;
        private System.Windows.Forms.TextBox textBoxSearsch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton toolStripButtonSearch;
    }
}

