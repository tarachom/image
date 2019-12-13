using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ImageLibrary;

namespace ImageForms
{
    public partial class FormContext : Form
    {
        public FormContext()
        {
            InitializeComponent();
        }

        private void LoadContextList()
        {
            dataGridViewContext.Rows.Clear();

            List<ImageContext> contextList = new List<ImageContext>();
            Program.GlobalKernel.LoadAllContextList(contextList);

            Program.Global_NormalizeDataGridRows(contextList.Count, dataGridViewContext.Rows);

            int index_row = 0;

            foreach (ImageContext imageContext in contextList)
            {
                DataGridViewRow row = dataGridViewContext.Rows[index_row];

                row.Cells["colContextID"].Value = imageContext.ID;
                row.Cells["colContextName"].Value = imageContext.Name;
                row.Cells["colContextDescription"].Value = imageContext.Description;

                index_row++;
            }
        }

        private void FormContext_Load(object sender, EventArgs e)
        {
            LoadContextList();
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            FormContextElement frmContextElement = new ImageForms.FormContextElement();
            frmContextElement.Show();
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            LoadContextList();
        }

        private void dataGridViewContext_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;

            if (row >= 0)
            {
                FormContextElement frmContextElement = new ImageForms.FormContextElement();
                frmContextElement.ImageContextElement = Program.GlobalKernel.GetImageContexByID(int.Parse(dataGridViewContext.Rows[row].Cells["colContextID"].Value.ToString()));
                frmContextElement.Show();
            }
        }

        private void dataGridViewContext_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridViewSelectedRowCollection selectRow = dataGridViewContext.SelectedRows;

            if (selectRow.Count > 0)
                if (e.KeyCode == Keys.Delete)
                {
                    DialogResult result = MessageBox.Show("Видалити вибрані контексти?", "Повідомлення", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        foreach (DataGridViewRow row in selectRow)
                        {
                            if (Program.GlobalKernel.DeleteImageContext(int.Parse(row.Cells["colContextID"].Value.ToString())))
                                dataGridViewContext.Rows.Remove(row);
                        }
                            
                    }
                }
        }
    }
}
