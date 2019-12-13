using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using ImageLibrary;

namespace ImageForms
{
    public partial class FormTreeModel : Form
    {
        public FormTreeModel()
        {
            InitializeComponent();
        }

        private void LoadIngadient(TreeNode rootNode)
        {
            if (rootNode == null)
                return;

            string ID = rootNode.Name;

            Dictionary<string, string> dictionary_list = new Dictionary<string, string>(); // Program.GlobalKernel.GetIngradientyForImage(ID);

            if (dictionary_list.Count > 0)
            {
                if (rootNode.Nodes.Count > 0)
                    rootNode.Nodes.Clear();

                foreach (string item in dictionary_list.Keys)
                {
                    TreeNode itemNode = rootNode.Nodes.Add(item, dictionary_list[item]);
                    itemNode.Nodes.Add("empty", "<...>");
                }
            }
            else
            {
                if (ID == "empty")
                {

                }
            }

            //rootNode.Expand();

        }

        private void LoadRootNodes()
        {
            List<ImageBase> listImageBase = new List<ImageBase>();

            Program.GlobalKernel.LoadAllImageBase(listImageBase);

            TreeNode rootNode = treeViewModel.Nodes.Add("root", "Товар");

            foreach (ImageBase imageBase in listImageBase)
            {
                TreeNode itemNode = rootNode.Nodes.Add(imageBase.ID.ToString(), imageBase.Name);
                itemNode.Nodes.Add("empty", "<...>");
            }

           // rootNode.Expand();
        }

        private void FormTreeModel_Load(object sender, EventArgs e)
        {
            LoadRootNodes();
        }

        private void treeViewModel_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        private void treeViewModel_AfterExpand(object sender, TreeViewEventArgs e)
        {
            //LoadIngadient(e.Node);
        }

        private void treeViewModel_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        private void treeViewModel_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            LoadIngadient(e.Node);
        }
    }
}
