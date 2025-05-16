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
    public partial class frmBuyers : Form
    {
        public frmBuyers()
        {
            InitializeComponent();
        }

        buyerBLL b = new buyerBLL();
        buyerDAL dal = new buyerDAL();

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBuyers_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Select();
            dgvBuyers.DataSource = dt;
            // Change column headers
            dgvBuyers.Columns["id"].HeaderText = "Buyer ID";
            dgvBuyers.Columns["buyer_name"].HeaderText = "Buyer Name";
            dgvBuyers.Columns["address"].HeaderText = "Address";
            dgvBuyers.Columns["contact_no"].HeaderText = "Contact Number";
            dgvBuyers.Columns["brokerage_rate"].HeaderText = "Brokerage (₹)";
            dgvBuyers.Columns["gst_no"].HeaderText = "GST Number";
        }

        private void clearFields()
        {
            txtBuyerName.Text = "";
            txtAddress.Text = "";
            txtContact.Text = "";
            txtBuyerRate.Text = "";
            txtGSTNo.Text = "";
            lblBuyerID.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Getting Data from UI
            b.buyer_name = txtBuyerName.Text;
            b.address = txtAddress.Text;
            b.contact_no = txtContact.Text;
            b.brokerage_rate = float.Parse(txtBuyerRate.Text);
            b.gst_no = txtGSTNo.Text;

            // Inserting Data into Database
            bool success = dal.Insert(b);
            // If data is successfully inserted then the value of success will be true else it will be false
            if (success == true)
            {
                // Data inserted successfully
                MessageBox.Show("Buyer successfully added...");
                clearFields();
            }
            else
            {
                // Failed to Insert
                MessageBox.Show("Failed to add new Buyer!!!");
            }
            // Refreshing DataGrid View
            DataTable dt = dal.Select();
            dgvBuyers.DataSource = dt;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Get the values from User UI
            b.id = Convert.ToInt32(lblBuyerID.Text);
            b.buyer_name = txtBuyerName.Text;
            b.address= txtAddress.Text;
            b.contact_no= txtContact.Text;
            b.brokerage_rate= float.Parse(txtBuyerRate.Text);
            b.gst_no= txtGSTNo.Text;

            // Updating Data into database
            bool success = dal.Update(b);
            // if data is updated successfully then the value of success will be true else it will be false
            if (success == true)
            {
                // Data Updated Successfully
                MessageBox.Show("Buyer's data updated successfully...");
                clearFields();
            }
            else
            {
                // failed to update user
                MessageBox.Show("Failed to update Buyer's data!!!");
            }
            // Refreshing DataGrid View
            DataTable dt = dal.Select();
            dgvBuyers.DataSource = dt;
            // Change column headers
            dgvBuyers.Columns["id"].HeaderText = "Buyer ID";
            dgvBuyers.Columns["buyer_name"].HeaderText = "Buyer Name";
            dgvBuyers.Columns["address"].HeaderText = "Address";
            dgvBuyers.Columns["contact_no"].HeaderText = "Contact Number";
            dgvBuyers.Columns["brokerage_rate"].HeaderText = "Brokerage (₹)";
            dgvBuyers.Columns["gst_no"].HeaderText = "GST Number";
        }

        private void dgvBuyers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            lblBuyerID.Text = dgvBuyers.Rows[rowIndex].Cells[0].Value.ToString();
            txtBuyerName.Text = dgvBuyers.Rows[rowIndex].Cells[1].Value.ToString();
            txtAddress.Text = dgvBuyers.Rows[rowIndex].Cells[2].Value.ToString();
            txtContact.Text = dgvBuyers.Rows[rowIndex].Cells[3].Value.ToString();
            txtBuyerRate.Text = dgvBuyers.Rows[rowIndex].Cells[4].Value.ToString();
            txtGSTNo.Text = dgvBuyers.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Getting User ID from Form
            b.id = Convert.ToInt32(lblBuyerID.Text);

            bool success = dal.Delete(b);

            // If data is deleted then the value of success will be true else it will be false
            if (success == true)
            {
                // User Deleted Successfully
                MessageBox.Show("Buyer Deleted Successfully...");
                clearFields();
            }
            else
            {
                // Failed to Delete User
                MessageBox.Show("Failed to delete Buyer!!!");
            }
            // Refreshing DataGrid View
            DataTable dt = dal.Select();
            dgvBuyers.DataSource = dt;
            dgvBuyers.Columns["id"].HeaderText = "Buyer ID";
            dgvBuyers.Columns["buyer_name"].HeaderText = "Buyer Name";
            dgvBuyers.Columns["address"].HeaderText = "Address";
            dgvBuyers.Columns["contact_no"].HeaderText = "Contact Number";
            dgvBuyers.Columns["brokerage_rate"].HeaderText = "Brokerage (₹)";
            dgvBuyers.Columns["gst_no"].HeaderText = "GST Number";
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
                dgvBuyers.DataSource = dt;
            }
            else
            {
                // Show all users from the databased
                DataTable dt = dal.Select();
                dgvBuyers.DataSource = dt;
            }
        }
    }
}
