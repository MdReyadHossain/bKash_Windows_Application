using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Admin_bKash
{
    public partial class Admin : Form
    {
        string phoneNo;
        double amount;

        public Admin()
        {
            InitializeComponent();
        }

        private void button1submit_Click(object sender, EventArgs e)
        {
            this.phoneNo = textBoxPhone.Text;
            this.amount = Convert.ToDouble(textBoxAmount.Text);

            bool isValid = false;
            string valid = @"[0][1][3 4 5 6 7 8 9][0-9]{8}$";

            if (textBoxPhone.Text != null)
            {
                if (Regex.IsMatch(textBoxPhone.Text, valid))
                {
                    this.phoneNo = textBoxPhone.Text;
                    labelWrong.Visible = false;

                    SqlDataReader data;
                    try
                    {
                        SqlCommand cmd;
                        using (SqlConnection con = new SqlConnection(@"Data Source=sql.bsite.net\MSSQL2016;Initial Catalog=blazeaxel_bKash;Persist Security Info=True;User ID=blazeaxel_bKash;Password=123"))
                        {
                            con.Open();
                            using (cmd = new SqlCommand("select * from Agent order by ID;", con))
                            {
                                data = cmd.ExecuteReader();
                                while (data.Read())
                                {
                                    if (Convert.ToString(data.GetValue(1)) == this.phoneNo)
                                    {
                                        isValid = true;

                                        DateTime today = DateTime.Now;
                                        MessageBox.Show("TK" + this.amount + " transferred to A/C:Agent " + this.phoneNo + "\nDate: " + Convert.ToString(today.Date), "Admin");

                                        this.amount += Convert.ToDouble(data.GetValue(9));
                                        updateAmount(this.amount, this.phoneNo);

                                        cmd.Dispose();
                                        data.Close();
                                        con.Close();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    catch (SystemException ex)
                    {
                        //MessageBox.Show(string.Format("An error occurred: {0}", ex.Message));
                    }
                }
                else
                {
                    isValid = false;
                    labelWrong.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("Input Incomplete!", "Registration Error");
            }

            if(!isValid)
            {
                MessageBox.Show("Phone Number not found from bKash database!", "Admin Portal");
            }
        }

        private void buttonOut_Click(object sender, EventArgs e)
        {
            this.phoneNo = textBoxPhone.Text;
            this.amount = Convert.ToDouble(textBoxAmount.Text);

            bool isValid = false;
            string valid = @"[0][1][3 4 5 6 7 8 9][0-9]{8}$";

            if (textBoxPhone.Text != null)
            {
                if (Regex.IsMatch(textBoxPhone.Text, valid))
                {
                    this.phoneNo = textBoxPhone.Text;
                    labelWrong.Visible = false;

                    SqlDataReader data;
                    try
                    {
                        SqlCommand cmd;
                        using (SqlConnection con = new SqlConnection(@"Data Source=sql.bsite.net\MSSQL2016;Initial Catalog=blazeaxel_bKash;Persist Security Info=True;User ID=blazeaxel_bKash;Password=123"))
                        {
                            con.Open();
                            using (cmd = new SqlCommand("select * from Agent order by ID;", con))
                            {
                                data = cmd.ExecuteReader();
                                while (data.Read())
                                {
                                    if (Convert.ToString(data.GetValue(1)) == this.phoneNo)
                                    {
                                        isValid = true;

                                        DateTime today = DateTime.Now;
                                        double tk = this.amount;
                                        this.amount = Convert.ToDouble(data.GetValue(9)) - this.amount;
                                        if(this.amount < 20)
                                        {
                                            MessageBox.Show("Insufficient Balance for Cash Out A/C:Agent " + this.phoneNo, "Admin Portal");
                                        }
                                        else
                                        {
                                            MessageBox.Show("TK" + tk + " Out from A/C:Agent " + this.phoneNo + "\nDate: " + Convert.ToString(today.Date), "Admin Portal");
                                            updateAmount(this.amount, this.phoneNo);
                                        }

                                        cmd.Dispose();
                                        data.Close();
                                        con.Close();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    catch (SystemException ex)
                    {
                        //MessageBox.Show(string.Format("An error occurred: {0}", ex.Message));
                    }
                }
                else
                {
                    isValid = false;
                    labelWrong.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("Input Incomplete!", "Registration Error");
            }

            if (!isValid)
            {
                MessageBox.Show("Phone Number not found from bKash database!", "Cash In ");
            }
        }

        public static void updateAmount(double amount, string phoneNo)
        {
            SqlDataReader data;
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                using (SqlConnection con = new SqlConnection(@"Data Source=sql.bsite.net\MSSQL2016;Initial Catalog=blazeaxel_bKash;Persist Security Info=True;User ID=blazeaxel_bKash;Password=123"))
                {
                    con.Open();
                    string sql = "Update Agent set Balance = '" + amount + "' where PhoneNo = " + phoneNo;
                    using (adapter.UpdateCommand = new SqlCommand(sql, con))
                    {
                        adapter.UpdateCommand.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch (SystemException ex)
            {
                MessageBox.Show(string.Format("An error occurred: {0}", ex.Message));
            }
        }
    }
}
