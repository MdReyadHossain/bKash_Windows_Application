using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Windows.Forms;
using System.IO;

namespace bKash_Desktop_Application
{
    public partial class RegistrationInformation : Form
    {
        string user, phoneNo;
        string userName, fatherName, motherName, DOB, bloodGroup, address, division;

        VideoCaptureDevice videoCapture;
        FilterInfoCollection camDevice;

        public RegistrationInformation(string user, string phoneNo)
        {
            InitializeComponent();
            this.user = user;
            this.phoneNo = phoneNo;

            StartCamera();
        }

        void StartCamera()
        {
            try
            {
                camDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                videoCapture = new VideoCaptureDevice(camDevice[0].MonikerString);
                videoCapture.NewFrame += new NewFrameEventHandler(Camera_ON);
                videoCapture.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Camera_ON(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void RegistrationInformation_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                videoCapture.Stop();
            }
            catch
            {
                return;
            }
        }

        private void buttonCapture_Click_1(object sender, EventArgs e)
        {
            pictureBox2.Image = pictureBox.Image;

            string fileName = @"..\..\Image\" + this.phoneNo + ".jpg";
            Directory.CreateDirectory(@"..\..\Image");

            pictureBox2.Visible = true;
            buttonAgain.Visible = true;
            try
            {
                pictureBox2.Image.Save(fileName, ImageFormat.Jpeg);
                MessageBox.Show("Image Captured", "Camera");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            panel2.Visible = true;
        }

        private void buttonAgain_Click_1(object sender, EventArgs e)
        {
            StartCamera();
            buttonAgain.Visible = false;
            pictureBox2.Visible = false;
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