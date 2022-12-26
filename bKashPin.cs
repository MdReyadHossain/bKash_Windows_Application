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
using System.Text.RegularExpressions;

namespace bKash_Desktop_Application
{
    public partial class bKashPin : Form
    {
        string user, phoneNo;
        string userName, fatherName, motherName, DOB, bloodGroup, address, division;
        string pin = @"[0-9]{5}";

        public bKashPin(string user, string phoneNo, string userName, string fatherName, string motherName, string DOB, string bloodGroup, string address, string division)
        {
            InitializeComponent();
            this.user = user;
            this.phoneNo = phoneNo;
            this.userName = userName;
            this.fatherName = fatherName;
            this.motherName = motherName;
            this.DOB = DOB;
            this.bloodGroup = bloodGroup;
            this.address = address;
            this.division = division;
        }

        private void textBoxRePIN_TextChanged(object sender, EventArgs e)
        {
            if (textBoxRePIN.Text != "" && textBoxPIN.Text == textBoxRePIN.Text && textBoxPIN.Text.Length == 5 && textBoxRePIN.Text.Length == 5 &&
                Regex.IsMatch(textBoxPIN.Text, this.pin) && Regex.IsMatch(textBoxRePIN.Text, this.pin))
            {
                labelWrong.Visible = false;
                labelRight.Visible = true;
            }

            else if (textBoxRePIN.Text == "")
            {
                labelWrong.Visible = false;
                labelRight.Visible = false;
            }

            else
            {
                labelWrong.Visible = true;
                labelRight.Visible = false;
            }
        }

        private void textBoxPIN_TextChanged(object sender, EventArgs e)
        {
            if (textBoxRePIN.Text != "" && textBoxPIN.Text == textBoxRePIN.Text && textBoxPIN.Text.Length == 5 && textBoxRePIN.Text.Length == 5 &&
                Regex.IsMatch(textBoxPIN.Text, this.pin) && Regex.IsMatch(textBoxRePIN.Text, this.pin))
            {
                labelWrong.Visible = false;
                labelRight.Visible = true;
            }

            else if (textBoxRePIN.Text == "")
            {
                labelWrong.Visible = false;
                labelRight.Visible = false;
            }

            else
            {
                labelWrong.Visible = true;
                labelRight.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxPIN.Text == textBoxRePIN.Text && labelRight.Visible)
            {
                if (this.user == "Personal")
                {
                    insertDB_Personal(this.user, this.phoneNo, this.userName, this.fatherName, this.motherName, this.DOB, this.bloodGroup, this.address, this.division, textBoxPIN.Text);
                    MessageBox.Show("Registration Complete!\nWelcome to bKash.", "Registration");
                }
                else if (this.user == "Agent")
                {
                    insertDB_Agent(this.user, this.phoneNo, this.userName, this.fatherName, this.motherName, this.DOB, this.bloodGroup, this.address, this.division, textBoxPIN.Text);
                    MessageBox.Show("Registration Complete!\nYou are now bKash Agent.", "Registration");
                }
                else
                {
                    MessageBox.Show(this.user + " User not found", "Registration Error");
                }

                Home home = new Home();
                home.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Set PIN correctly!", "Registration Error");
            }
        }

        public static void insertDB_Personal(string user, string phoneNo, string userName, string fatherName, string motherName, string DOB, string bloodGroup, string address, string division, string pin)
        {
            try
            {
                SqlCommand cmd;
                using (SqlConnection con = new SqlConnection(@"Data Source=sql.bsite.net\MSSQL2016;Initial Catalog=blazeaxel_bKash;Persist Security Info=True;User ID=blazeaxel_bKash;Password=123"))
                {
                    con.Open();
                    using (cmd = new SqlCommand("insert into " + user + " (PhoneNo, Name, FathersName, MothersName, DateOfBirth, BloodGroup, Address, Division, Balance, Pin, Point) VALUES ('" + phoneNo + "', '" + userName + "', '" + fatherName + "', '" + motherName + "', '" + DOB + "', '" + bloodGroup + "', '" + address + "', '" + division + "', '" + 25.00 + "', '" + pin + "', '" + 5 + "')", con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch (SystemException ex)
            {
                MessageBox.Show(string.Format("An error occurred: {0}", ex.Message));
            }
        }

        public static void insertDB_Agent(string user, string phoneNo, string userName, string fatherName, string motherName, string DOB, string bloodGroup, string address, string division, string pin)
        {
            try
            {
                SqlCommand cmd;
                using (SqlConnection con = new SqlConnection(@"Data Source=sql.bsite.net\MSSQL2016;Initial Catalog=blazeaxel_bKash;Persist Security Info=True;User ID=blazeaxel_bKash;Password=123"))
                {
                    con.Open();
                    using (cmd = new SqlCommand("insert into " + user + " (PhoneNo, Name, FathersName, MothersName, DateOfBirth, BloodGroup, Address, Division, Balance, Pin) VALUES ('" + phoneNo + "', '" + userName + "', '" + fatherName + "', '" + motherName + "', '" + DOB + "', '" + bloodGroup + "', '" + address + "', '" + division + "', '" + 25.00 + "', '" + pin + "')", con))
                    {
                        cmd.ExecuteNonQuery();
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
