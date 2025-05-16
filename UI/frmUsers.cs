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
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }

        userBLL u = new userBLL();
        userDAL dal = new userDAL();

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearFields()
        {
            txtUserID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            cmbUserType.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Getting UserName of the logged in User
            string loggedUser = frmLogin.loggedIn;
            // Getting Data from UI
            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.email = txtEmail.Text;
            u.username = txtUserName.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.user_type = cmbUserType.Text;

            userBLL usr = dal.GetIDFromUsername(loggedUser);
            u.added_by = usr.id;

            // Inserting Data into Database
            bool success = dal.Insert(u);
            // If data is successfully inserted then the value of success will be true else it will be false
            if (success == true)
            {
                // Data inserted successfully
                MessageBox.Show("User successfully created.");
                clearFields();
            }
            else
            {
                // Failed to Insert
                MessageBox.Show("Failed to add new user");
            }
            // Refreshing DataGrid View
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            // Refreshing DataGrid View
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }

        private void dgvUsers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Get Index of the Particular Row
            int rowIndex = e.RowIndex;
            lblID.Text = dgvUsers.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvUsers.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvUsers.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvUsers.Rows[rowIndex].Cells[3].Value.ToString();
            txtUserName.Text = dgvUsers.Rows[rowIndex].Cells[4].Value.ToString();
            txtPassword.Text = dgvUsers.Rows[rowIndex].Cells[5].Value.ToString();
            txtContact.Text = dgvUsers.Rows[rowIndex].Cells[6].Value.ToString();
            txtAddress.Text = dgvUsers.Rows[rowIndex].Cells[7].Value.ToString();
            cmbUserType.Text = dgvUsers.Rows[rowIndex].Cells[8].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Getting UserName of the logged in User
            string loggedUser = frmLogin.loggedIn;
            // Get the values from User UI
            u.id = Convert.ToInt32(lblID.Text);
            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.email = txtEmail.Text;
            u.username = txtUserName.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.user_type = cmbUserType.Text;
            userBLL usr = dal.GetIDFromUsername(loggedUser);
            u.added_by = usr.id;

            // Updating Data into database
            bool success = dal.Update(u);
            // if data is updated successfully then the value of success will be true else it will be false
            if (success == true)
            {
                // Data Updated Successfully
                MessageBox.Show("User successfully updated");
                clearFields();
            }
            else
            {
                // failed to update user
                MessageBox.Show("Failed to update user");
            }
            // Refreshing DataGrid View
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Getting User ID from Form
            u.id = Convert.ToInt32(lblID.Text);

            bool success = dal.Delete(u);

            // If data is deleted then the value of success will be true else it will be false
            if (success == true)
            {
                // User Deleted Successfully
                MessageBox.Show("User Deleted Successfully");
                clearFields();
            }
            else
            {
                // Failed to Delete User
                MessageBox.Show("Failed to delete user");
            }
            // Refreshing DataGrid View
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
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
                dgvUsers.DataSource = dt;
            }
            else
            {
                // Show all users from the databased
                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;
            }
        }
    }
}
