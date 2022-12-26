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
using System.IO;

namespace bKash_Desktop_Application
{
    public partial class RegistrationInformation : Form
    {
        string user, phoneNo;
        string userName, fatherName, motherName, DOB, bloodGroup, address, division;
        public RegistrationInformation(string user, string phoneNo)
        {
            InitializeComponent();
            this.user = user;
            this.phoneNo = phoneNo;
        }

        private void button1submit_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text != null &&
                textBoxFather != null &&
                textBoxMother != null &&
                comboBoxBlood.SelectedItem != null &&
                textBoxAddress.Text != null &&
                comboBoxDivision.SelectedItem != null)
            {
                this.userName = textBoxName.Text;
                this.fatherName = textBoxFather.Text;
                this.motherName = textBoxMother.Text;
                this.DOB = Convert.ToString(dateTimePickerDOB.Value.Day) + "-" + Convert.ToString(dateTimePickerDOB.Value.Month) + "-" + Convert.ToString(dateTimePickerDOB.Value.Year);
                this.bloodGroup = comboBoxBlood.Text;
                this.address = textBoxAddress.Text;
                this.division = comboBoxDivision.Text;

                bKashPin bkashpin = new bKashPin(this.user, this.phoneNo, this.userName, this.fatherName, this.motherName, this.DOB, this.bloodGroup, this.address, this.division);
                bkashpin.Show();
                this.Close();
            }

            else
            {
                MessageBox.Show("Input Incomplete...\nMake sure all information full-filled", "Registration Error");
            }
        }

    }
}