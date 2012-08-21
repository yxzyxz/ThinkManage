using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.OpenAccess;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace ThinkManage
{
    public partial class ThinkManageMain : RadForm
    {
        private ThinkManageEntitiesModel dbContent = new ThinkManageEntitiesModel();
        private BindingSource dataSource = new BindingSource();
        private int treeViewId;
        public ThinkManageMain()
        {
            InitializeComponent();
        }

        private void toolTabStrip3_Click(object sender, EventArgs e)
        {

        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            RadTreeNode newNode = new RadTreeNode("New Node");
            Category selectedCategory = this.dataSource.Current as Category;
            //MessageBox.Show("selectedCategory.Id=" + selectedCategory.Id);

            if (selectedCategory == null)
            {
                treeViewId = 0;
            }
            else
            {
                treeViewId = selectedCategory.Id;
                MessageBox.Show("selectedCategory ！= null! treeViewPid=" + treeViewId);
            }
            Category newCategory = new Category();
            newCategory.CategoryName = newNode.Name;
            newCategory.PId = treeViewId;
            newCategory.DateCreated = DateTime.Now;
            dataSource.Add(newCategory);
            dbContent.Add(newCategory);
            this.dbContent.SaveChanges();
            this.radTreeView1.SelectedNode.Expand();
        }

        private void ThinkManageMain_Load(object sender, EventArgs e)
        {
            this.dataSource.DataSource = dbContent.Categories.ToList();
            radTreeView1.DataSource = this.dataSource;
            radTreeView1.AllowEdit = true;
            radMenuItem1.Click += new EventHandler(this.addButton_Click);
            radMenuItem2.Click += new EventHandler(this.editButton_Click);
            radMenuItem3.Click += new EventHandler(this.removeButton_Click);
            radMenuItem4.Click += new EventHandler(this.removeButton_Click);
            radMenuItem5.Click += new EventHandler(this.removeButton_Click);
            if (this.radTreeView1.Nodes.Count >= 1)
            {
                TreeViewExpand();
                MessageBox.Show("radTreeView1.Nodes.Count >= 1,TreeViewExpand");
            }
        }
        private void TreeViewExpand()
        {
                ////this.radTreeView1.Nodes[0].Expand();
                this.radTreeView1.ParentMember = "Pid";
                this.radTreeView1.ChildMember = "Id";
                this.radTreeView1.DisplayMember = "CategoryName";
                //this.radTreeView1.SelectedNode = this.radTreeView1.Nodes[0].Nodes[1];
                radTreeView1.ExpandAll();
                this.radTreeView1.SpacingBetweenNodes = 2;
        }
        private void ThinkManageMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            dbContent.Dispose();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            Category selectedCategory = this.dataSource.Current as Category;
            if (selectedCategory == null)
                return;
            this.dataSource.Remove(selectedCategory);
            this.dbContent.Delete(selectedCategory);
        }

        private void radButtonElement5_Click(object sender, EventArgs e)
        {
            this.dbContent.SaveChanges();

        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (this.radTreeView1.SelectedNode != null)
            {
                this.radTreeView1.SelectedNode.BeginEdit();
            }
            //Category selectedCategory = this.dataSource.Current as Category;
            //if (selectedCategory == null)
            //    return;

        }

        private void radTreeView1_ContextMenuOpening(object sender, TreeViewContextMenuOpeningEventArgs e)
        {
            for (int i = e.Menu.Items.Count - 1; i >= 0; i--)
            {
                if (e.Menu.Items[i].Name == "Delete")
                {
                    this.removeButton_Click(null,null);
                }

                if (e.Menu.Items[i].Name == "New")
                {
                    this.addButton_Click(null, null);
                }
            }         
        }

        private void ThinkManageMain_Leave(object sender, EventArgs e)
        {

        }

        private void radTreeView1_NodeAdded(object sender, RadTreeViewEventArgs e)
        {
            MessageBox.Show("radTreeView1.Nodes.Count == " + radTreeView1.Nodes.Count);

            if (this.radTreeView1.Nodes.Count == 1)
            {
                TreeViewExpand();
                MessageBox.Show("radTreeView1.Nodes.Count == 1,TreeViewExpand");
            }

        }
    }
}
