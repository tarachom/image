namespace ImageForms
{
    partial class FormExportXML
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
            this.progressBarExport = new System.Windows.Forms.ProgressBar();
            this.buttonExpotXML = new System.Windows.Forms.Button();
            this.buttonXSLTransform = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBarExport
            // 
            this.progressBarExport.Location = new System.Drawing.Point(30, 29);
            this.progressBarExport.Name = "progressBarExport";
            this.progressBarExport.Size = new System.Drawing.Size(452, 31);
            this.progressBarExport.TabIndex = 0;
            // 
            // buttonExpotXML
            // 
            this.buttonExpotXML.Location = new System.Drawing.Point(30, 84);
            this.buttonExpotXML.Name = "buttonExpotXML";
            this.buttonExpotXML.Size = new System.Drawing.Size(160, 34);
            this.buttonExpotXML.TabIndex = 1;
            this.buttonExpotXML.Text = "Вигрузити модель в XML";
            this.buttonExpotXML.UseVisualStyleBackColor = true;
            this.buttonExpotXML.Click += new System.EventHandler(this.buttonExpotXML_Click);
            // 
            // buttonXSLTransform
            // 
            this.buttonXSLTransform.Location = new System.Drawing.Point(196, 84);
            this.buttonXSLTransform.Name = "buttonXSLTransform";
            this.buttonXSLTransform.Size = new System.Drawing.Size(160, 34);
            this.buttonXSLTransform.TabIndex = 2;
            this.buttonXSLTransform.Text = "XSL трансформация";
            this.buttonXSLTransform.UseVisualStyleBackColor = true;
            this.buttonXSLTransform.Click += new System.EventHandler(this.buttonXSLTransform_Click);
            // 
            // FormExportXML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 144);
            this.Controls.Add(this.buttonXSLTransform);
            this.Controls.Add(this.buttonExpotXML);
            this.Controls.Add(this.progressBarExport);
            this.Name = "FormExportXML";
            this.Text = "FormExportXML";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarExport;
        private System.Windows.Forms.Button buttonExpotXML;
        private System.Windows.Forms.Button buttonXSLTransform;
    }
}