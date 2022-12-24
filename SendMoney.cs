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
    public partial class SendMoney : Form
    {
        public SendMoney()
        {
            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button1submit_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TransactionForm transactionForm = new TransactionForm();
            this.Hide();
            transactionForm.Show();
        }
    }
}
