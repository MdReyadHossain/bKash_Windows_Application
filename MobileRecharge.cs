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
    public partial class MobileRecharge : Form
    {
        public MobileRecharge()
        {
            InitializeComponent();
        }

        private void button1submit_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel7.Visible = true;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            panel8.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TransactionForm transactionForm = new TransactionForm();
            this.Hide();
            transactionForm.Show();
        }
    }
}
