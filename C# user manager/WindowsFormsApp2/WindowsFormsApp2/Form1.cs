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

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        public void list_data()
        {
            
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void chart3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("host=localhost;user=root;password=pass;port=3306;database=users");
            DataSet ds;
            MySqlDataAdapter da;
            ds = new DataSet();
            con.Open();
            da = new MySqlDataAdapter("select * from usertable", con);
            da.Fill(ds,"users");
            chart_1.DataSource = ds;
            chart_1.Series["NumericValue"].XValueMember = "mission";
            chart_1.Series["NumericValue"].YValueMembers = "NV";

            chart_2.DataSource = ds;
            chart_2.Series["Series2"].XValueMember = "category";
            chart_2.Series["Series2"].YValueMembers = "NV";

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "users";
            


            con.Close();




        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //delete user
            try
            {
                string MyConnection2 = "host=localhost;user=root;password=pass;port=3306;database=users";
                string QueryDelete = "delete from users.usertable where ID='" + this.textBox1.Text + "';";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(QueryDelete, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("User Deleted Successfully!");
                while (MyReader2.Read())
                {
                }
                MyConn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oh no! We have a problem! ERROR message: " + ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string MyConnection2 = "datasource=localhost;port=3306;username=root;password=pass;database=users";
                string Query = "INSERT INTO usertable VALUES (" + this.textBox1.Text + ",'" + this.textBox4.Text + "','" + this.textBox2.Text + "','" + this.textBox3.Text + "','" + this.textBox5.Text + "','" + this.textBox6.Text + "');";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("User Added Successfully!");
                while (MyReader2.Read())
                {
                }
                MyConn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oh no! We have a problem! ERROR message: " + ex.Message);
            }
        }
    }
}
