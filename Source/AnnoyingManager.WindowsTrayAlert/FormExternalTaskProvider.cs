using AnnoyingManager.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnnoyingManager.Core.Entities;
using AnnoyingManager.Core.Repository;

namespace AnnoyingManager.WindowsTrayAlert
{
    public partial class FormExternalTaskProvider : Form, IExternalTaskProvider
    {
        private const int DAYS_TO_LOAD = 7; // one week
        private IConfigRepository _config;
        private ITaskRepository _repository;
        private Task _suggestedTask;

        public FormExternalTaskProvider()
        {
            InitializeComponent();
            this.SetDefaults();
            
            _config = new XmlConfigRepository();   // TODO: use factory
            _repository = new XmlRepository();   // TODO: use factory
            tvwTasks = FillTreeView(tvwTasks);
            tvwTasks.ExpandAll();
        }

        public Task GetExternalTask()
        {
            if (ShowDialog() == DialogResult.OK)
            {
                return _suggestedTask;
            }
            return new Task();
        }

        private TreeView FillTreeView(TreeView treeView)
        {
            DateTime date = _config.GetCurrentDateTime().Date;
            var tasks = _repository.SearchTasks(date.AddDays(-1 * (DAYS_TO_LOAD)), date);

            treeView.Nodes.AddRange(GetValuesAsTreeNode(tasks
                .Select(t => t.Group)));
            foreach (TreeNode nodeLevel1 in tvwTasks.Nodes)
            {
                nodeLevel1.Nodes.AddRange(GetValuesAsTreeNode(tasks
                    .Where(t => t.Group == nodeLevel1.Text)
                    .Select(t => t.ReferenceID)));
                foreach (TreeNode nodeLevel2 in nodeLevel1.Nodes)
                {
                    nodeLevel2.Nodes.AddRange(GetValuesAsTreeNode(tasks
                        .Where(t => t.Group == nodeLevel1.Text && t.ReferenceID == nodeLevel2.Text)
                        .Select(t => t.Description)));
                }
            }
            return treeView;
        }

        private TreeNode[] GetValuesAsTreeNode(IEnumerable<string> values)
        {
            return values.Distinct()
                        .Where(t => !string.IsNullOrEmpty(t))
                        .Select(t => new TreeNode(t))
                        .ToArray();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string group = string.Empty, referenceID = string.Empty, description = string.Empty;
            var node = tvwTasks.SelectedNode;
            if (node != null)
            {
                _suggestedTask = new Task();
                if (node.Level == 2)
                {
                    _suggestedTask.Description = node.Text;
                    node = node.Parent;
                }
                if (node.Level == 1)
                {
                    _suggestedTask.ReferenceID = node.Text;
                    node = node.Parent;
                }
                if (node.Level == 0)
                {
                    _suggestedTask.Group = node.Text;
                }
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
