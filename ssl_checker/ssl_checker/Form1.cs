using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ssl_checker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string input;
        string[] input_array;



        static DateTime ReadExpirDate(string website)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(website);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            response.Close();

            X509Certificate cert = request.ServicePoint.Certificate;
            X509Certificate2 cert2 = new System.Security.Cryptography.X509Certificates.X509Certificate2(cert);

            string cedate = cert2.GetExpirationDateString();
            return DateTime.Parse(cedate);
        }

        static int GetReminderDays(DateTime date)
        {
            DateTime thisDay = DateTime.Today;
            TimeSpan difference = thisDay - date;
            int days = (int)difference.TotalDays;

            return days;

        }


        private void button1_Click(object sender, EventArgs e)
        {


            input = websites.Text;
            input_array = Regex.Split(input, "\r\n|\r|\n");
            string output = "";

         
            foreach (string website in input_array)
            {


                try
                {

                    DateTime expireDate = ReadExpirDate(website);
                    int days = GetReminderDays(expireDate);

                    output += "Expire at: " + expireDate + " after " + Math.Abs(days) + " days \r\n ";

                }catch(Exception ex)
                {
                    output += "Error message:" + ex.Message+ " \r\n";
                }

               


            }

            result.Text = output;

        }



        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void websites_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
