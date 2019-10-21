using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
//using System.Device.Location;


namespace BloodBank
{
    
    public partial class Profile : Form
    {
        MySqlConnection connection = new MySqlConnection("datasource=localhost;username=root;database=bloodbank;password=");
        MySqlCommand command;
        MySqlDataAdapter da;
        String ID;
        String query;
        String emailadd;
        string location;
        string name;
        string Phone;
        double longitude=0.0, lattitude=0.0;
        public Profile(String x, string y)
        {
            InitializeComponent();
            ID = x;
            this.Text = x;
            Phone = y;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

         

        }
        void search(String query)
        {
            try
            {

           
                command = new MySqlCommand(query, connection);
                da = new MySqlDataAdapter(command);

                DataTable table = new DataTable();

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.AllowUserToAddRows = false;

                da.Fill(table);

                dataGridView1.DataSource = table;
                dataGridView1.Columns["Password"].Visible = false;
                dataGridView1.Columns["Id"].Visible = false;
                dataGridView1.Columns["BloodGroup"].Visible = false;
                da.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox_area_SelectedIndexChanged(object sender, EventArgs e)
        {
      
        }

        private void comboBox_bg_MouseClick(object sender, MouseEventArgs e)
        {
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.comboBox_bg.Text!="" && this.comboBox_area.Text !="")
            {
              //  "SELECT * FROM `donordata` WHERE Phone in (SELECT phone from donorstatus)";
                //"SELECT * FROM `donordata` WHERE ((Location='"+ this.comboBox_area.Text +"'and BloodGroup='"+this.comboBox_bg.Text+"' )and Phone in (SELECT phone from donorstatus))"
                search("SELECT * FROM `donordata` WHERE ((Location='" + this.comboBox_area.Text + "'and BloodGroup='" + this.comboBox_bg.Text + "' )and Phone in (SELECT phone from donorstatus))");
            }
            else if(this.comboBox_bg.Text!="" && this.comboBox_area.Text =="")
            {
                search("SELECT * FROM `donordata` WHERE ((BloodGroup='" + this.comboBox_bg.Text + "')and Phone in (SELECT phone from donorstatus))");
            }
            else if(this.comboBox_bg.Text=="" && this.comboBox_area.Text !="")
            {
                search("SELECT * FROM `donordata` WHERE ((Location='" + this.comboBox_area.Text + "')and Phone in (SELECT phone from donorstatus))");
            }
        }

        /*
         * 
         *
Rajshahi
Khulna
Barishal
Chattagram
Sylhet
Dhaka
Mymensing
Rangpur */
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            button2.Visible = true;
            button3.Visible = true;
            if (e.ColumnIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                try
                {
                    name  = dataGridView1.SelectedRows[0].Cells["Name"].Value.ToString();
                    emailadd = dataGridView1.SelectedRows[0].Cells["Email"].Value.ToString();
                    location = dataGridView1.SelectedRows[0].Cells["Location"].Value.ToString();

                    lattitude = Convert.ToDouble (dataGridView1.SelectedRows[0].Cells["Lattitude"].Value);
                    longitude = Convert.ToDouble(dataGridView1.SelectedRows[0].Cells["longitude"].Value);
                  //  Phone = dataGridView1.SelectedRows[0].Cells["Phone"].Value.ToString();
                    label3.Text = name;
                  
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
         try
            {
                var from = new MailAddress("bolod.bank26587@gmail.com");
                var pass = "chodasesh01";
                var to = new MailAddress(emailadd);
                string sub = "Blood Requested";
                string body = "Hello," + name + ", I have come to know  through BloodBase app that you have due blood to be donated. I am urgently in need on blood of your type. Please contact me with the- \n"+"phone number " + Phone + "\n"+"Sincerely, " + ID;

                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(from.Address, pass)
                };
                using (var message = new MailMessage(from, to)
                {
                    Subject = sub,
                    Body = body
                })
                    smtp.Send(message);
                MessageBox.Show("Email Sent!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Profile_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 obj = new Form1();
            obj.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Map obj = new Map(lattitude,longitude,name);
            obj.Show();
        }
    }
}
