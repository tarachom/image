using System;
using System.Windows.Forms;
using ImageLibrary;

namespace ImageForms
{
    public partial class FormCharacterystykaElement : Form
    {
        public FormCharacterystykaElement()
        {
            InitializeComponent();
        }

        public CharacterystykaItem Characterystyka { get; set; }
        public frmImageElement ImageParentForm { get; set; }

        private void FormCharacterystykaElement_Load(object sender, EventArgs e)
        {
            if (Characterystyka != null)
            {
                textBoxCode.Enabled = false;

                textBoxName.Text = Characterystyka.ItemName;
                textBoxValue.Text = Characterystyka.ItemValue;
                textBoxCode.Text = Characterystyka.Code;
            }
            else
                Characterystyka = new CharacterystykaItem();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Characterystyka.ItemName = textBoxName.Text;
            Characterystyka.ItemValue = textBoxValue.Text;
            Characterystyka.Code = textBoxCode.Text;

            if (ImageParentForm != null)
                ImageParentForm.AddOrUpadateCharacterystyka(Characterystyka);

            Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
