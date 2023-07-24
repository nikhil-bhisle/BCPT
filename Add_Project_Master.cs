using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2;

namespace WindowsFormsApp1
{
    public partial class Add_Project_Master : Form
    {
        public string loginId;

        public Add_Project_Master(string loginid)
        {
            InitializeComponent();

            loginId = loginid;
            date1.CloseUp += date1_CloseUp;
        }

        private void date1_CloseUp(object sender, EventArgs e)
        {
 
            DateTime selectedDate = date1.Value;

            date2.MinDate = selectedDate.AddDays(1);
        }

        

   


        MySqlConnection connection = new MySqlConnection("server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings");
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string connectionString = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings"; 

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query1 = "SELECT resource_name FROM resource_master where role='PM'";
                MySqlCommand command1 = new MySqlCommand(query1, connection);
                MySqlDataReader reader1 = command1.ExecuteReader();
                while (reader1.Read())
                {
                    string resourceName = reader1["resource_name"].ToString();
                    PM.Items.Add(resourceName);
                }
                reader1.Close();


                string query2 = "SELECT DISTINCT program_id FROM project_master";
                MySqlCommand command2 = new MySqlCommand(query2, connection);
                MySqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    string ProgramId = reader2["program_id"].ToString();
                    progId.Items.Add(ProgramId);
                }
                reader2.Close();

                string query3 = "SELECT DISTINCT project_type FROM project_master";
                MySqlCommand command3 = new MySqlCommand(query3, connection);
                MySqlDataReader reader3 = command3.ExecuteReader();
                while (reader3.Read())
                {
                    string projtype = reader3["project_type"].ToString();
                    projecttype.Items.Add(projtype);
                }
                reader3.Close();


                string query4 = "SELECT DISTINCT piac_category FROM project_master";
                MySqlCommand command4 = new MySqlCommand(query4, connection);
                MySqlDataReader reader4 = command4.ExecuteReader();
                while (reader4.Read())
                {
                    string piaccat = reader4["piac_category"].ToString();
                    piac.Items.Add(piaccat);
                }
                reader4.Close();

                string query5 = "SELECT DISTINCT country FROM project_master";
                MySqlCommand command5 = new MySqlCommand(query5, connection);
                MySqlDataReader reader5 = command5.ExecuteReader();
                while (reader5.Read())
                {
                    string coun = reader5["country"].ToString();
                    country_name.Items.Add(coun);
                }
                reader5.Close();

                
                string query6 = "SELECT DISTINCT product FROM project_master";
                MySqlCommand command6 = new MySqlCommand(query6, connection);
                MySqlDataReader reader6 = command6.ExecuteReader();
                while (reader6.Read())
                {
                    string prodname = reader6["product"].ToString();
                    product_name.Items.Add(prodname);
                }
                reader6.Close();
                
                date1.Value = DateTime.Today;               
                date1.MinDate = DateTime.Today;

                date2.Value = DateTime.Today.AddDays(1);
                date2.MinDate = DateTime.Today.AddDays(1);


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private string FetchingSoeid(string project_manager)
        {
            string pm_soeid = "";
            string sql;
            MySqlCommand cmd;
            MySqlDataReader dr;
            MySqlConnection conn = new MySqlConnection("server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings");
            try
            {
                conn.Open();
                sql = "SELECT SOEID from resource_master where resource_name='" + project_manager + "'";
                cmd = new MySqlCommand(sql, conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    pm_soeid = dr.GetString(0);
                }
            }
            catch
            {
                MessageBox.Show("No match Found");
            }


            return pm_soeid;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            // Retrieve form data
            string PtsID = PTSID.Text;
            string description = Description.Text;
            string p_type = projecttype.SelectedItem.ToString();
            string program_id = progId.SelectedItem.ToString();
            string project_manager = PM.SelectedItem.ToString();
            string project_manager_soeid = FetchingSoeid(project_manager);
            string piac_category = piac.SelectedItem.ToString();
            DateTime target_release_date = date1.Value;
            DateTime project_end_date = date2.Value;
            string country = country_name.SelectedItem.ToString();
            string product = product_name.SelectedItem.ToString();
            string status = product_status.SelectedItem.ToString();
            int secore_l0 = Convert.ToInt32(SecoreL0.Text);
            int dotnet_l0 = Convert.ToInt32(NetL0.Text);
            int secore_l1 = Convert.ToInt32(SecoreL1.Text);
            int dotnet_l1 = Convert.ToInt32(NetL1.Text);
            DateTime dateTime = DateTime.Now;

            string modified_time = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            string modified_by = loginId;


            string sqlInsert = "INSERT INTO project_master VALUES (@PTSId, @Description, @p_type, @program_id, @project_manager, @piac, @date1, @date2, @country, @product, @status, @secoreL0, @dotnetL0, @secoreL1, @dotnetL1, @name, CURRENT_TIMESTAMP, @archieve )";

            connection.Open();

            MySqlCommand command = new MySqlCommand(sqlInsert, connection);

            command.Parameters.AddWithValue("@PTSId", PtsID);
            command.Parameters.AddWithValue("@Description", description);
            command.Parameters.AddWithValue("@p_type", p_type);
            command.Parameters.AddWithValue("@program_id", program_id);
            command.Parameters.AddWithValue("@project_manager", project_manager_soeid);
            command.Parameters.AddWithValue("@piac", piac_category);
            command.Parameters.AddWithValue("@date1", target_release_date);
            command.Parameters.AddWithValue("@date2", project_end_date);
            command.Parameters.AddWithValue("@country", country);
            command.Parameters.AddWithValue("@product", product);
            command.Parameters.AddWithValue("@status", status);
            command.Parameters.AddWithValue("@secoreL0", secore_l0);
            command.Parameters.AddWithValue("@dotnetL0", dotnet_l0);
            command.Parameters.AddWithValue("@secoreL1", secore_l1);
            command.Parameters.AddWithValue("@dotnetL1", dotnet_l1);
            command.Parameters.AddWithValue("@time", modified_time);
            command.Parameters.AddWithValue("@name", modified_by);
            command.Parameters.AddWithValue("@archieve", 0);


            if (command.ExecuteNonQuery() == 1)
            {
                //MessageBox.Show(project_manager);
                MessageBox.Show("Data Inserted");
            }
            else
            {
                MessageBox.Show("Data Not Inserted");

            }

            connection.Close();
            this.Close();

            ProjectMaster.instance.viewAllFunc();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void NetL0_TextChanged(object sender, EventArgs e)
        {

        }

        private void PTSID_TextChanged(object sender, EventArgs e)
        {

        }

        private void date1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void NetL1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void date2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label_date2_Click(object sender, EventArgs e)
        {

        }

        private void label_sec0_Click(object sender, EventArgs e)
        {

        }

        private void label_product_Click(object sender, EventArgs e)
        {

        }

        private void product_name_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SecoreL0_TextChanged(object sender, EventArgs e)
        {

        }

        private void label_net1_Click(object sender, EventArgs e)
        {

        }

        private void product_status_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PM_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void piac_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            this.Close();
            ProjectMaster.instance.viewAllFunc();
        }

        private void SecoreL0_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the entered key is a digit or a control key (e.g., backspace or delete)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancel the key press event to prevent the character from being entered
                e.Handled = true;
            }
        }

        private void SecoreL1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void NetL0_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void NetL1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
