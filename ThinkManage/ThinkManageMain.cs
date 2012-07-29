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
        }

        private void toolTabStrip3_Click(object sender, EventArgs e)
        {

        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Category newCategory = new Category();
            newCategory.CategoryName = "Work";
            newCategory.DateCreated = DateTime.Now;
            dataSource.Add(newCategory);
            dbContent.Add(newCategory);
           
        }

        private void ThinkManageMain_Load(object sender, EventArgs e)
        {
            this.dataSource.DataSource = dbContent.Categories.ToList();
            radTreeView1.DataSource = this.dataSource;
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
    }
}
