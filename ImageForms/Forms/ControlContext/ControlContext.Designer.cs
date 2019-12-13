namespace ImageForms
{
    partial class ControlContext
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
            this.labelTitleText = new System.Windows.Forms.Label();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.MenuListContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // labelTitleText
            // 
            this.labelTitleText.AutoSize = true;
            this.labelTitleText.Location = new System.Drawing.Point(3, 4);
            this.labelTitleText.Name = "labelTitleText";
            this.labelTitleText.Size = new System.Drawing.Size(57, 13);
            this.labelTitleText.TabIndex = 0;
            this.labelTitleText.Text = "Контекст:";
            // 
            // buttonSelect
            // 
            this.buttonSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelect.Location = new System.Drawing.Point(306, -1);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(28, 22);
            this.buttonSelect.TabIndex = 2;
            this.buttonSelect.Text = "...";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // MenuListContext
            // 
            this.MenuListContext.Name = "MenuListContext";
            this.MenuListContext.Size = new System.Drawing.Size(61, 4);
            // 
            // ControlContext
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.labelTitleText);
            this.Name = "ControlContext";
            this.Size = new System.Drawing.Size(335, 20);
            this.Load += new System.EventHandler(this.ControlContext_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitleText;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.ContextMenuStrip MenuListContext;
    }
}
