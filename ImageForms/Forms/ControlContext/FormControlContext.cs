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
    public partial class FormControlContext : Form
    {
        private ControlContext m_ControlContextItem;

        /// <summary>
        /// Контрол який викликав дану форму
        /// </summary>
        public ControlContext ControlContextItem
        {
            get
            {
                return m_ControlContextItem;
            }
            set
            {
                m_ControlContextItem = value;
            }
        }

        public FormControlContext()
        {
            InitializeComponent();
        }

        private void FormControlContext_Load(object sender, EventArgs e)
        {
            if (ControlContextItem == null)
                return;

            if (ControlContextItem.ImageContextSelect != null)
            {
                ReloadContextList(ControlContextItem.ImageContextSelect);
            }
            else
                ReloadContextList();
        }

        private void ReloadContextList(ImageContext selectImageContext = null, string searchText = "")
        {
            List<ImageContext> contextList = new List<ImageContext>();

            Program.GlobalKernel.LoadAllContextList(contextList);

            Program.Global_NormalizeDataGridRows(contextList.Count, dataGridViewList.Rows);

            int index_row = 0;

            foreach (ImageContext imageContext in contextList)
            {
                DataGridViewRow row = dataGridViewList.Rows[index_row];

                row.Cells["colID"].Value = imageContext.ID;
                row.Cells["colName"].Value = imageContext.Name;

                if (selectImageContext != null)
                {
                    if (selectImageContext.ID == imageContext.ID)
                    {
                        row.Selected = true;
                        dataGridViewList.CurrentCell = row.Cells["colID"];
                    }
                }

                index_row++;
            }
        }

        private void dataGridViewList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewList.Rows[rowIndex];

                ImageContext selectImageContext = Program.GlobalKernel.GetImageContexByID(int.Parse(row.Cells["colID"].Value.ToString()));

                if (selectImageContext != null)
                    ControlContextItem.ImageContextSelect = selectImageContext;

                this.Close();
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string txt = textBoxSearch.Text.Trim().ToLower();
            int txt_length = txt.Length;

            if (txt_length > 0)
            {
                foreach (DataGridViewRow row in dataGridViewList.Rows)
                {
                    string colName = row.Cells["colName"].Value.ToString().ToLower();
                    int colName_length = colName.Length;
                    string colNamePiec = "";

                    if (colName_length > txt_length)
                        colNamePiec = colName.Substring(0, txt_length);
                    else if (colName_length < txt_length)
                        continue;
                    else
                        colNamePiec = colName;

                    if (colNamePiec == txt)
                    {
                        row.Selected = true;
                        dataGridViewList.CurrentCell = row.Cells["colID"];

                        return;
                    }
                }
            }
            else
            {
                if (dataGridViewList.Rows.Count > 0)
                    dataGridViewList.CurrentCell = dataGridViewList.Rows[0].Cells[0];
            }
        }
    }
}
