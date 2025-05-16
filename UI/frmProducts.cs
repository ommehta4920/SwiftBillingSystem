using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SwiftBillingSystem.BLL;
using SwiftBillingSystem.DAL;

namespace SwiftBillingSystem.UI
{
    public partial class frmProducts : Form
    {
        productBLL p = new productBLL();
        productDAL dal = new productDAL();

        public frmProducts()
        {
            InitializeComponent();
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmProducts_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Select();
            dgvProducts.DataSource = dt;
            // Change column headers
            dgvProducts.Columns["id"].HeaderText = "Product ID";
            dgvProducts.Columns["product_name"].HeaderText = "Product Name";
            dgvProducts.Columns["brand"].HeaderText = "Brand";
            dgvProducts.Columns["packing"].HeaderText = "Packing";
        }

        private void clearFields()
        {
            txtProductName.Text = "";
            txtBrandName.Text = "";
            txtPacking.Text = "";
            lblProductID.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Getting Data from UI
            p.product_name = txtProductName.Text;
            p.brand = txtBrandName.Text;
            p.packing = txtPacking.Text;

            // Inserting Data into Database
            bool success = dal.Insert(p);
            // If data is successfully inserted then the value of success will be true else it will be false
            if (success == true)
            {
                // Data inserted successfully
                MessageBox.Show("Product successfully added...");
                clearFields();
            }
            else
            {
                // Failed to Insert
                MessageBox.Show("Failed to add new Product!!!");
            }
            // Refreshing DataGrid View
            DataTable dt = dal.Select();
            dgvProducts.DataSource = dt;
        }

        private void dgvProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            lblProductID.Text = dgvProducts.Rows[rowIndex].Cells[0].Value.ToString();
            txtProductName.Text = dgvProducts.Rows[rowIndex].Cells[1].Value.ToString();
            txtBrandName.Text = dgvProducts.Rows[rowIndex].Cells[2].Value.ToString();
            txtPacking.Text = dgvProducts.Rows[rowIndex].Cells[3].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Get the values from User UI
            p.id = Convert.ToInt32(lblProductID.Text);
            p.product_name = txtProductName.Text;
            p.brand = txtBrandName.Text;
            p.packing = txtPacking.Text;

            // Updating Data into database
            bool success = dal.Update(p);
            // if data is updated successfully then the value of success will be true else it will be false
            if (success == true)
            {
                // Data Updated Successfully
                MessageBox.Show("Product data updated successfully...");
                clearFields();
            }
            else
            {
                // failed to update user
                MessageBox.Show("Failed to update Product data!!!");
            }
            // Refreshing DataGrid View
            DataTable dt = dal.Select();
            dgvProducts.DataSource = dt;
            // Change column headers
            dgvProducts.Columns["id"].HeaderText = "Product ID";
            dgvProducts.Columns["product_name"].HeaderText = "Product Name";
            dgvProducts.Columns["brand"].HeaderText = "Brand";
            dgvProducts.Columns["packing"].HeaderText = "Packing";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Getting User ID from Form
            p.id = Convert.ToInt32(lblProductID.Text);

            bool success = dal.Delete(p);

            // If data is deleted then the value of success will be true else it will be false
            if (success == true)
            {
                // User Deleted Successfully
                MessageBox.Show("Product Deleted Successfully...");
                clearFields();
            }
            else
            {
                // Failed to Delete User
                MessageBox.Show("Failed to delete Product!!!");
            }
            // Refreshing DataGrid View
            DataTable dt = dal.Select();
            dgvProducts.DataSource = dt;
            // Change column headers
            dgvProducts.Columns["id"].HeaderText = "Product ID";
            dgvProducts.Columns["product_name"].HeaderText = "Product Name";
            dgvProducts.Columns["brand"].HeaderText = "Brand";
            dgvProducts.Columns["packing"].HeaderText = "Packing";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Getting keyword from Text Box
            string keywords = txtSearch.Text;

            // Check if the keywords has value or not
            if (keywords != null)
            {
                // Show user based on keywords
                DataTable dt = dal.Search(keywords);
                dgvProducts.DataSource = dt;
            }
            else
            {
                // Show all users from the databased
                DataTable dt = dal.Select();
                dgvProducts.DataSource = dt;
            }
        }
    }
}
