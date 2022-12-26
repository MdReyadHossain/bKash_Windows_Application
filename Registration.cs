using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace bKash_Desktop_Application
{
    public partial class Registration : Form
    {
        string user, phoneNo, varificationCode;
        public Registration()
        {
            InitializeComponent();
        }

        private void button1submit_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            string valid = @"[0][1][3 4 5 6 7 8 9][0-9]{8}$";
            Random random = new Random();

            if (textBoxPhone.Text != null && radioButtonAgent.Checked || radioButtonPersonal.Checked)
            {
                if (Regex.IsMatch(textBoxPhone.Text, valid))
                {
                    this.varificationCode = random.Next(1000, 9999).ToString();
                    this.phoneNo = textBoxPhone.Text;
                    labelWrong.Visible = false;
                    if (radioButtonAgent.Checked)
                        this.user = radioButtonAgent.Text;
                    else
                        this.user = radioButtonPersonal.Text;

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
                                        isValid = false;
                                        break;
                                    }
                                }
                                data.Close();
                            }

                            using (cmd = new SqlCommand("select * from Personal order by ID;", con))
                            {
                                data = cmd.ExecuteReader();
                                while (data.Read() && isValid)
                                {
                                    if (Convert.ToString(data.GetValue(1)) == this.phoneNo)
                                    {
                                        isValid = false;
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

                    if(isValid)
                    {
                        panel2.Visible = true;
                        MessageBox.Show("Code sent\n" + this.varificationCode + "\n\n*Code will be sent to your mobile Number \nafter update v2.0 ", "Varification Code");
                    }
                    else if (!isValid)
                    {
                        MessageBox.Show("This Phone Number is already used", "Registration Error");
                    }
                }
                else
                {
                    isValid = false;
                    labelWrong.Visible = true;
                }

                if (isValid)
                {
                    
                }
            }
            else
            {
                MessageBox.Show("Input Incomplete!", "Registration Error");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.varificationCode == textBoxCode.Text)
            {
                labelCodeWrong.Visible = false;
                panel3.Visible = true;
            }
            else
            {
                labelCodeWrong.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegistrationInformation registrationInformation = new RegistrationInformation(this.user, this.phoneNo);
            registrationInformation.Show();
            this.Close();
        }
    }
}