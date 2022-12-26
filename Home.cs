using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bKash_Desktop_Application
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            this.Hide();
            registration.Show();
        }

        private void button1submit_Click(object sender, EventArgs e)
        {
            bool isValid = true, isLogin = false;
            string user = textBox1accountNumber.Text;
            string pass = textBox2bkashPin.Text;

            if (user == "" || pass == "")
            {
                MessageBox.Show("Input Incomplete", "Login Error");
                isValid = false;
            }

            if (isValid)
            {
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
                                if (Convert.ToString(data.GetValue(1)) == user && Convert.ToString(data.GetValue(10)) == pass)
                                {
                                    AgentDashboard agent = new AgentDashboard();
                                    agent.Show();
                                    isLogin = true;
                                    this.Hide();

                                    cmd.Dispose();
                                    data.Close();
                                    con.Close();
                                }
                            }
                            data.Close();
                        }

                        using (cmd = new SqlCommand("select * from Personal order by ID;", con))
                        {
                            data = cmd.ExecuteReader();
                            while (data.Read())
                            {
                                if (Convert.ToString(data.GetValue(1)) == user && Convert.ToString(data.GetValue(10)) == pass)
                                {
                                    Dashboard dashboard = new Dashboard();
                                    this.Hide();
                                    dashboard.Show();
                                    isLogin = true;

                                    cmd.Dispose();
                                    data.Close();
                                    con.Close();
                                }
                            }
                        }
                    }
                }
                catch (SystemException ex)
                {
                    //MessageBox.Show(string.Format("An error occurred: {0}", ex.Message));
                }

                if (!isLogin)
                {
                    MessageBox.Show("User Number or Password Incorrect", "Login Error");
                }
            }
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }
    }
}
