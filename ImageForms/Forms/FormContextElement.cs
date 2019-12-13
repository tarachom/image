using System;
using System.Windows.Forms;
using ImageLibrary;

namespace ImageForms
{
    public partial class FormContextElement : Form
    {
        public FormContextElement()
        {
            InitializeComponent();
        }

        private ImageContext m_imageContextElement;

        public ImageContext ImageContextElement
        {
            get
            {
                return m_imageContextElement;
            }
            set
            {
                m_imageContextElement = value;
            }
        }

        private void FormContextElement_Load(object sender, EventArgs e)
        {
            if (ImageContextElement != null)
            {
                textBoxContextName.Text = this.ImageContextElement.Name;
                textBoxContextDescription.Text = this.ImageContextElement.Description;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (this.ImageContextElement != null)
            {
                this.ImageContextElement.Name = textBoxContextName.Text;
                this.ImageContextElement.Description = textBoxContextDescription.Text;

                if (Program.GlobalKernel.UpdateImageContext(this.ImageContextElement))
                    this.Close();
            }
            else
            {
                ImageContext imageContextElementNew = new ImageContext(textBoxContextName.Text, textBoxContextDescription.Text);
                if (Program.GlobalKernel.InsertImageContext(imageContextElementNew))
                {
                    this.ImageContextElement = imageContextElementNew;
                    this.Close();
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
