using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SwiftBillingSystem.UI;

namespace SwiftBillingSystem
{
    public partial class frmUserDashboard : Form
    {
        public frmUserDashboard()
        {
            InitializeComponent();
        }

        private void btnLogout_MouseEnter(object sender, EventArgs e)
        {
            btnLogout.BackColor = Color.Red;
            btnLogout.ForeColor = Color.White;
        }

        private void btnLogout_MouseLeave(object sender, EventArgs e)
        {
            btnLogout.BackColor = Color.White;
            btnLogout.ForeColor = ColorTranslator.FromHtml("#276396");
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
            this.Close();
        }

        private void btnBuyers_Click(object sender, EventArgs e)
        {
            frmBuyers buyers = new frmBuyers();
            buyers.Show();
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            frmProducts products = new frmProducts();
            products.Show();
        }

        private void btnSellers_Click(object sender, EventArgs e)
        {
            frmSellers sellers = new frmSellers();
            sellers.Show();
        }
    }
}
