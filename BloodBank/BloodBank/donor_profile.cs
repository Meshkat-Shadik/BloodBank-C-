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
using System.Device.Location;

namespace BloodBank
{
    public partial class donor_profile : Form
    {
        int Id;
        int ret=0;
        String name, location, bg, age, phone, email, pass,Query2;
        private GeoCoordinateWatcher Watcher = null;
        public donor_profile(int x)
        {
            InitializeComponent();
            Id = x;
           // MessageBox.Show(" " + Id);
            display();
            // Create the watcher.

            Watcher = new GeoCoordinateWatcher();
            // Catch the StatusChanged event.

            Watcher.StatusChanged += Watcher_StatusChanged;
            // Start the watcher.

            Watcher.Start();
        }

        void display()
        {
            try
            {
                string connection = "datasource=localhost;username=root;password=; database=bloodbank";
                string query = "SELECT * from donordata where Id='" + Id +"';";
                MySqlConnection con = new MySqlConnection(connection);
                MySqlCommand command = new MySqlCommand(query, con);

                // MySqlDataReader reader;
                con.Open();


                var dr = command.ExecuteReader();
                if (dr.HasRows)
                {

                    dr.Read();
                   // id = Convert.ToInt32(dr.GetString(0));
                     name = dr.GetString(1);
                     bg = dr.GetString(2);
                     location = dr.GetString(3);
                     age = dr.GetString(4);
                     phone = dr.GetString(5);
                     email = dr.GetString(6);
                     pass = dr.GetString(7);

                     this.textBox5.Text = name;
                   //  MessageBox.Show(" " + name);
                   // MessageBox.Show("Login Successfull\n " + name);
                    // mainmenu 
                    //donor_profile obj = new donor_profile(id);
                    //obj.Show();
                    //this.Hide();
                     this.comboBox_bg.Text = bg;
                     this.comboBox_area.Text = location;
                     this.textBox2.Text = age;
                     this.textBox3.Text = email;
                     this.textBox4.Text = pass;
                     this.textBox7.Text = phone;
                     
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            comboBox_area.Enabled = true;
            comboBox_bg.Enabled = true;
            comboBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            button1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //This is my connection string i have assigned the database file address path  
                string MyConnection2 = "datasource=localhost;username=root;password=;database=bloodbank";
                string MyConnection3 = "datasource=localhost;username=root;password=;database=bloodbank";
                //This is my update query in which i am taking input from the user through windows forms and update the record.  

                string Query = "update donordata set Name='" + this.textBox5.Text + "',BloodGroup='" + this.comboBox_bg.Text + "',Location='" + this.comboBox_area.Text + "',Age='" + this.textBox2.Text + "',Email='" + this.textBox3.Text + "',Password='" + this.textBox4.Text + "',Lattitude='" + this.textBox8.Text + "',Longitude='" + this.textBox9.Text + "' where Phone='" + this.textBox7.Text + "';";
                //This is  MySqlConnection here i have created the object and pass my connection string.  
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MyConn2.Close();

              //  MessageBox.Show("" + kajbaj(phone));
                if (kajbaj(phone) == 1)
                {
                    Query2 = "update donorstatus set Status='" + this.comboBox1.Text + "' where Phone='" + phone + "'";
                }
                else if (kajbaj(phone) == 0)
                {
                    Query2 = "insert into donorstatus (Phone,Status) values('" + this.textBox7.Text + "','" + this.comboBox1.Text + "')";
                }
                MySqlConnection MyConn3 = new MySqlConnection(MyConnection3);
                MySqlCommand MyCommand3 = new MySqlCommand(Query2, MyConn3);
                MySqlDataReader MyReader3;
                MyConn3.Open();
                MyReader3 = MyCommand3.ExecuteReader();
                MyConn3.Close();
                MessageBox.Show("Updated!");

                //  gridview_data();
                //Connection closed here  

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        int kajbaj(String phn)
        {
            try
            {
                string connection = "datasource=localhost;username=root;password=; database=bloodbank";
                string query = "SELECT * from donorstatus where Phone='" + phn + "';";
                MySqlConnection con = new MySqlConnection(connection);
                MySqlCommand command = new MySqlCommand(query, con);

                // MySqlDataReader reader;
                con.Open();


                var dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    ret = 1;
                }
                else
                {
                    ret = 0;
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ret;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 obj = new Form1();
            obj.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Watcher_StatusChanged(sender, (GeoPositionStatusChangedEventArgs)e);
        }

        private void Watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            if (e.Status == GeoPositionStatus.Ready)
            {
                // Display the latitude and longitude.
                if (Watcher.Position.Location.IsUnknown)
                {
                    textBox8.Text = "Cannot find location data";
                }
                else
                {
                    textBox8.Text = Watcher.Position.Location.Latitude.ToString();
                    textBox9.Text = Watcher.Position.Location.Longitude.ToString();
                }
            }
        }
    }
}
