using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageLibrary;

namespace ImageForms
{
    public partial class FormSearch : Form
    {
        public FormSearch()
        {
            InitializeComponent();
        }

        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Program.GlobalKernel.SearchTestAlfa(textBoxSearch.Text);
            }
        }

        private void FormSearch_Load(object sender, EventArgs e)
        {
            textBoxSearch.Text = "Ноутбук asus процесор celeron магазин львів материнська плата";
            textBoxSearch_KeyDown(null, new KeyEventArgs(Keys.Enter));
        }
    }
}
