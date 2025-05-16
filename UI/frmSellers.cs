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
    public partial class frmSellers : Form
    {
        sellerBLL s = new sellerBLL();
        sellerDAL dal = new sellerDAL();
        public frmSellers()
        {
            InitializeComponent();
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSellers_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Select();
            dgvSellers.DataSource = dt;
            // Change column headers
            dgvSellers.Columns["id"].HeaderText = "Seller ID";
            dgvSellers.Columns["seller_name"].HeaderText = "Seller Name";
            dgvSellers.Columns["address"].HeaderText = "Address";
            dgvSellers.Columns["contact_no"].HeaderText = "Contact Number";
            dgvSellers.Columns["sbrokerage_rate"].HeaderText = "Brokerage (₹)";
            dgvSellers.Columns["gst_no"].HeaderText = "GST Number";
        }

        private void clearFields()
        {
            txtSellerName.Text = "";
            txtAddress.Text = "";
            txtContact.Text = "";
            txtSellerRate.Text = "";
            txtGSTNo.Text = "";
            lblSellerID.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Getting Data from UI
            s.seller_name = txtSellerName.Text;
            s.address = txtAddress.Text;
            s.contact_no = txtContact.Text;
            s.sbrokerage_rate = float.Parse(txtSellerRate.Text);
            s.gst_no = txtGSTNo.Text;

            // Inserting Data into Database
            bool success = dal.Insert(s);
            // If data is successfully inserted then the value of success will be true else it will be false
            if (success == true)
            {
                // Data inserted successfully
                MessageBox.Show("Seller successfully added...");
                clearFields();
            }
            else
            {
                // Failed to Insert
                MessageBox.Show("Failed to add new Seller!!!");
            }
            // Refreshing DataGrid View
            DataTable dt = dal.Select();
            dgvSellers.DataSource = dt;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Get the values from User UI
            s.id = Convert.ToInt32(lblSellerID.Text);
            s.seller_name = txtSellerName.Text;
            s.address = txtAddress.Text;
            s.contact_no = txtContact.Text;
            s.sbrokerage_rate = float.Parse(txtSellerRate.Text);
            s.gst_no = txtGSTNo.Text;

            // Updating Data into database
            bool success = dal.Update(s);
            // if data is updated successfully then the value of success will be true else it will be false
            if (success == true)
            {
                // Data Updated Successfully
                MessageBox.Show("Seller's data updated successfully...");
                clearFields();
            }
            else
            {
                // failed to update user
                MessageBox.Show("Failed to update Seller's data!!!");
            }
            // Refreshing DataGrid View
            DataTable dt = dal.Select();
            dgvSellers.DataSource = dt;
            // Change column headers
            dgvSellers.Columns["id"].HeaderText = "Seller ID";
            dgvSellers.Columns["seller_name"].HeaderText = "Seller Name";
            dgvSellers.Columns["address"].HeaderText = "Address";
            dgvSellers.Columns["contact_no"].HeaderText = "Contact Number";
            dgvSellers.Columns["sbrokerage_rate"].HeaderText = "Brokerage (₹)";
            dgvSellers.Columns["gst_no"].HeaderText = "GST Number";
        }

        private void dgvSellers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            lblSellerID.Text = dgvSellers.Rows[rowIndex].Cells[0].Value.ToString();
            txtSellerName.Text = dgvSellers.Rows[rowIndex].Cells[1].Value.ToString();
            txtAddress.Text = dgvSellers.Rows[rowIndex].Cells[2].Value.ToString();
            txtContact.Text = dgvSellers.Rows[rowIndex].Cells[3].Value.ToString();
            txtSellerRate.Text = dgvSellers.Rows[rowIndex].Cells[4].Value.ToString();
            txtGSTNo.Text = dgvSellers.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Getting User ID from Form
            s.id = Convert.ToInt32(lblSellerID.Text);

            bool success = dal.Delete(s);

            // If data is deleted then the value of success will be true else it will be false
            if (success == true)
            {
                // User Deleted Successfully
                MessageBox.Show("Seller Deleted Successfully...");
                clearFields();
            }
            else
            {
                // Failed to Delete User
                MessageBox.Show("Failed to delete Seller!!!");
            }
            // Refreshing DataGrid View
            DataTable dt = dal.Select();
            dgvSellers.DataSource = dt;
            dgvSellers.Columns["id"].HeaderText = "Seller ID";
            dgvSellers.Columns["seller_name"].HeaderText = "Seller Name";
            dgvSellers.Columns["address"].HeaderText = "Address";
            dgvSellers.Columns["contact_no"].HeaderText = "Contact Number";
            dgvSellers.Columns["sbrokerage_rate"].HeaderText = "Brokerage (₹)";
            dgvSellers.Columns["gst_no"].HeaderText = "GST Number";
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
                dgvSellers.DataSource = dt;
            }
            else
            {
                // Show all users from the databased
                DataTable dt = dal.Select();
                dgvSellers.DataSource = dt;
            }
        }
    }
}
