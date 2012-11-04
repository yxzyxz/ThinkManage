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
        public ThinkManageMain()
        {
            InitializeComponent();
            this.radTreeView1.SelectedNodeChanged += new RadTreeView.RadTreeViewEventHandler(radTreeView1_SelectedNodeChanged);
        }

        void radTreeView1_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            Category selectedCategory = this.dataSource.Current as Category;
            //int currentId = selectedCategory.Id;
            //MessageBox.Show("Current Id is:" + currentId);
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
            if (selectedCategory == null)
                return;

            //if (newNode != null)
            //{
            //    this.radTreeView1.SelectedNode = newNode;
            //    this.radTreeView1.BeginEdit();
            //}
            Category newCategory = new Category();
            newCategory.CategoryName = newNode.Name;
            //newCategory.PId = selectedCategory.Id;
            newCategory.DateCreated = DateTime.Now;
            dataSource.Add(newCategory);
            dbContent.Add(newCategory);
            this.dbContent.SaveChanges();
            //MessageBox.Show("newCategory PId is:" + selectedCategory.Id);

        }

        private void ThinkManageMain_Load(object sender, EventArgs e)
        {
            this.radTreeView1.DisplayMember = "CategoryName";
            //this.radTreeView1.ParentMember = "Pid";
            this.radTreeView1.DataMember = "Id";
            
            this.dataSource.DataSource = dbContent.Categories.ToList();
            radTreeView1.DataSource = this.dataSource;
            radTreeView1.AllowEdit = true;
            radMenuItem1.Click += new EventHandler(this.addButton_Click);
            radMenuItem2.Click += new EventHandler(this.editButton_Click);
            radMenuItem3.Click += new EventHandler(this.removeButton_Click);
            radMenuItem4.Click += new EventHandler(this.removeButton_Click);
            radMenuItem5.Click += new EventHandler(this.removeButton_Click);

            //ThinkManageEntitiesModel entities = new ThinkManageEntitiesModel();
            //var query = from content in entities.ThinkContents select content;
            this.radGridView1.DataSource = dbContent.ThinkContents.ToList();
            this.radGridView1.AutoGenerateHierarchy = true;
            this.radTreeView1.AutoSize = true;
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
    }
}
