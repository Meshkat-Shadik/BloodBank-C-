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
using System.Threading;
namespace BloodBank
{
    public partial class Form1 : Form
    {

        int id;
        String name,mobile;
        public Form1()
        {
         
            InitializeComponent();
       
        }
 
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Green;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            panel2.BackColor = Color.Green;
        }

        private void textBox1_MouseHover(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Green;
        }

        private void textBox2_MouseHover(object sender, EventArgs e)
        {
            panel2.BackColor = Color.Green;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registration obj = new Registration();
            obj.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string connection = "datasource=localhost;username=root;password=; database=bloodbank";
                string query = "SELECT * from recieverdata where Phone='" + this.textBox2.Text + "'and Password='" + this.textBox1.Text + "';";
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
                    mobile = dr.GetString(5);


                    MessageBox.Show("Login Successfull\n " + name);
                    // mainmenu 
                    Profile obj = new Profile(name,mobile);
                    obj.Show();
                    this.Hide();
                }
                else
                {
                    label4.Visible = true;

                    MessageBox.Show("wrong Username/password \n " + name);
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registration obj = new Registration();
            obj.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string connection = "datasource=localhost;username=root;password=; database=bloodbank";
                string query = "SELECT * from donordata where Phone='" + this.textBox_email.Text + "'and Password='" + this.textBox_Passs.Text + "';";
                MySqlConnection con = new MySqlConnection(connection);
                MySqlCommand command = new MySqlCommand(query, con);

                // MySqlDataReader reader;
                con.Open();


                var dr = command.ExecuteReader();
                if (dr.HasRows)
                {

                    dr.Read();
                    id = Convert.ToInt32(dr.GetString(0));
                    name = dr.GetString(1);


                    MessageBox.Show("Login Successfull\n " + name);
                    // mainmenu 
                    donor_profile obj = new donor_profile(id);
                    obj.Show();
                    this.Hide();
                }
                else
                {
                    label4.Visible = true;

                    MessageBox.Show("wrong Username/password \n " + name);
                }
                dr.Close();
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
