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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        loginBLL l = new loginBLL();
        loginDAL dal = new loginDAL();
        public static string loggedIn;

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            l.username = txtUserName.Text.Trim();
            l.password = txtPassword.Text.Trim();
            l.user_type = cmbUserType.Text;

            // Checking Login Credentials
            bool success = dal.loginCheck(l);
            if (success == true)
            {
                // Login Successfull
                MessageBox.Show("Login Successful...");
                loggedIn = l.username;
                // Need to open respective Forms based on User Type
                switch (l.user_type)
                {
                    case "Admin":
                        {
                            // Display Admin Dashboard
                            frmAdminDashboard admin = new frmAdminDashboard();
                            admin.Show();
                            this.Hide();
                        }
                        break;
                    case "User":
                        {
                            // Display User Dashboard
                            frmUserDashboard user = new frmUserDashboard();
                            user.Show();
                            this.Hide();
                        }
                        break;
                    default:
                        {
                            // Display an error message
                            MessageBox.Show("Invalid User Type.");
                        }
                        break;
                }
            }
            else
            {
                // Login Failed
                MessageBox.Show("Login Failed. Please Try Again!!!");
            }
        }
    }
}
