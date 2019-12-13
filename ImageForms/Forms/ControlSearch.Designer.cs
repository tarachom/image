namespace ImageForms
{
    partial class ControlSearch
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxSearchList = new System.Windows.Forms.ListBox();
            this.contextMenuStripFunc = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.buttonContextMenu = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.Location = new System.Drawing.Point(49, 0);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(277, 20);
            this.textBoxSearch.TabIndex = 0;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            this.textBoxSearch.Enter += new System.EventHandler(this.textBoxSearch_Enter);
            this.textBoxSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSearch_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Пошук:";
            // 
            // listBoxSearchList
            // 
            this.listBoxSearchList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxSearchList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxSearchList.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.listBoxSearchList.FormattingEnabled = true;
            this.listBoxSearchList.ItemHeight = 15;
            this.listBoxSearchList.Location = new System.Drawing.Point(0, 27);
            this.listBoxSearchList.Name = "listBoxSearchList";
            this.listBoxSearchList.Size = new System.Drawing.Size(344, 270);
            this.listBoxSearchList.TabIndex = 3;
            this.listBoxSearchList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBoxSearchList_KeyDown);
            this.listBoxSearchList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxSearchList_MouseDoubleClick);
            // 
            // contextMenuStripFunc
            // 
            this.contextMenuStripFunc.Name = "contextMenuStripFunc";
            this.contextMenuStripFunc.Size = new System.Drawing.Size(61, 4);
            // 
            // buttonContextMenu
            // 
            this.buttonContextMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonContextMenu.Location = new System.Drawing.Point(327, -1);
            this.buttonContextMenu.Name = "buttonContextMenu";
            this.buttonContextMenu.Size = new System.Drawing.Size(18, 22);
            this.buttonContextMenu.TabIndex = 6;
            this.buttonContextMenu.Text = "+";
            this.buttonContextMenu.UseVisualStyleBackColor = true;
            this.buttonContextMenu.Click += new System.EventHandler(this.buttonContextMenu_Click);
            // 
            // ControlSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this.buttonContextMenu);
            this.Controls.Add(this.listBoxSearchList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxSearch);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ControlSearch";
            this.Size = new System.Drawing.Size(344, 299);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.ControlSearch_Validating);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxSearchList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFunc;
        private System.Windows.Forms.Button buttonContextMenu;
    }
}
