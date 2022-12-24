using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bKash_Desktop_Application
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            panel15.Visible = true;
            label17.Visible = false;
            label18.Visible = true;
        }

        private void label18_Click(object sender, EventArgs e)
        {
            panel15.Visible = false;
            label18.Visible = false;
            label17.Visible = true;
        }

        private void label11_Click(object sender, EventArgs e)
        {
            SendMoney sendMoney = new SendMoney();
            this.Hide();
            sendMoney.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click_1(object sender, EventArgs e)
        {
            panel15.Visible = false;
            label18.Visible = false;
            label17.Visible = true;
        }
    }
}
