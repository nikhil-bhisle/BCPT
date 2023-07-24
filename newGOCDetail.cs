using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace BCPT
{
    public partial class newGOCDetail : Form
    {
        //private string loginID;

        //public string LoginID
        //{
        //    get { return loginID; }
        //    set { loginID = value; }
        //}
        private string loginID;
        public newGOCDetail( string loginID)
        {
            InitializeComponent();
            this.loginID = loginID;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to proceed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {


                //string gocid = textBox1.Text;
                //Debug.WriteLine(gocid);
                //string rate = textBox2.Text;
                //string cost = textBox3.Text;
                //string description = textBox4.Text;
                //DateTime curr = DateTime.Now;
                //MySqlCommand cmd1;
                //string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
                //MySqlConnection conn = new MySqlConnection(connstring);
                //conn.Open();
                ////string sql1 = "INSERT INTO gocmaster Values ('"+gocid+"' , '"+rate+ "' , '"+cost+ "' , '"+description+ "' , '"+"admin"+ "' , '"+curr+"');";
                //string sql1 = "INSERT INTO `learnings`.`gocmaster` (`goc`, `rate`, `one_month_cost_per_resource`, `goc_description`, `modified_by`, `modified_datetime`) VALUES ('11', '5.9', '1200', 'GOC 11 Description', 'nikhil', '2023-07-05 12:31:41');";
                //cmd1 = new MySqlCommand(sql1, conn);
                //Debug.WriteLine(cmd1);
                //MessageBox.Show("GOC Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // User clicked "Yes"

                {
                    string connectionString = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings"; 

                    string goc = textBox1.Text;
                    string rate = textBox2.Text;
                    string oneCost = textBox3.Text;
                    string descriptionGoc = textBox4.Text;
                    string modified = loginID;
                    DateTime now=DateTime.Now;
                    string modified_time = now.ToString("yyyy-MM-dd HH:mm:ss");

                    string query = "INSERT INTO learnings.gocmaster (goc, rate, one_month_cost_per_resource, goc_description, modified_by, modified_datetime) " + "VALUES (@GOC, @Rate, @OneCost, @descript, @Modified, @Time)";


                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();

                            MySqlCommand command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@GOC", goc);
                            command.Parameters.AddWithValue("@Rate", rate);
                            command.Parameters.AddWithValue("@OneCost", oneCost);
                            command.Parameters.AddWithValue("@descript", descriptionGoc);
                            command.Parameters.AddWithValue("@Modified", modified);
                            command.Parameters.AddWithValue("@Time", modified_time);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("GOC Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                                
                            }
                            else
                            {
                                MessageBox.Show("Data insertion failed.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Invalid Fields Entered", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
