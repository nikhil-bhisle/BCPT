using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Utilities.Collections;
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
using WindowsFormsApp2;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace WindowsFormsApp1
{
    public partial class Edit_Project_Master : Form
    {

        static string connection = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";

        MySqlConnection conn = new MySqlConnection(connection);

        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataTable table;


        private string ptsid;

        public string loginId;

        public Edit_Project_Master(string ptsid, string loginid)
        {
            InitializeComponent();
            //date1.MinDate = DateTime.Today;
            loginId = loginid;  
            date1.CloseUp += date1_CloseUp;
            
            this.ptsid = ptsid;
            string query = "SELECT project_master.PTSID AS 'PTS ID',project_master.description AS 'Project Description',project_master.project_type AS 'Project Type',project_master.program_id AS 'Program Id', resource_master.resource_name AS 'Project Manager',project_master.piac_category AS 'PIAC Category', project_master.target_release_date AS 'Target Release Date',project_master.project_end_date AS 'Project End Date', project_master.country AS 'Country', project_master.product AS 'Product', project_master.status AS 'Status', project_master.secore_l0 AS 'Secore L0', project_master.dotnet_l0 AS '.Net L0', project_master.secore_l1 AS 'Secore L1', project_master.dotnet_l1 AS '.Net L1' FROM project_master JOIN resource_master ON resource_master.soeid = project_master.project_manager WHERE PTSID = @ptsid";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ptsid", ptsid);
                DataTable table = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }

                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];

                    PTSID.Text = row["PTS ID"].ToString();
                    Description.Text = row["Project Description"].ToString();
                    projecttype.Text = row["Project Type"].ToString();
                    progId.Text = row["Program Id"].ToString();
                    PM.Text = row["Project Manager"].ToString();
                    piac.Text = row["PIAC Category"].ToString();
                    date1.Text = row["Target Release Date"].ToString();
                    date2.Text = row["Project End Date"].ToString();
                    country_name.Text = row["Country"].ToString();
                    product_name.Text = row["Product"].ToString();
                    product_status.Text = row["Status"].ToString();
                    SecoreL0.Text = row["Secore L0"].ToString();
                    NetL0.Text = row[".Net L0"].ToString();
                    SecoreL1.Text = row["Secore L1"].ToString();
                    NetL1.Text = row[".Net L1"].ToString();
                }
            }

            
        }

        private void date1_CloseUp(object sender, EventArgs e)
        {
            // Retrieve the selected value of date1
            DateTime selectedDate = date1.Value;

            // Set the minimum date or disable dates in date2 based on selectedDate
            date2.MinDate = selectedDate.AddDays(1);
        }



        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void NewForm_modify_Load(object sender, EventArgs e)
        {
             PTSID.ReadOnly = true;

            string connectionString = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query1 = "SELECT resource_master.resource_name FROM resource_master where resource_master.role='PM'";
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
                

                
            }
        }


        private string FetchingSoeid(string project_manager)
        {
            string resourceName = string.Empty;
            string sql = "SELECT SOEID FROM resource_master WHERE resource_name = @rn";

            using (MySqlConnection conn = new MySqlConnection("server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings"))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@rn", project_manager);
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        resourceName = result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while fetching the resource name: " + ex.Message);
                }
            }

            return resourceName;
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            // Retrieve form data
            string PtsID = PTSID.Text;
            string description = Description.Text;
            string p_type = projecttype.Text;
            string program_id = progId.Text;
            string project_manager = FetchingSoeid(PM.Text);
           
            string piac_category = piac.Text;
            DateTime target_release_date = date1.Value;
            DateTime project_end_date = date2.Value;
            string country = country_name.Text;
            string product = product_name.Text;
            string status = product_status.Text;
            int secore_l0 = Convert.ToInt32(SecoreL0.Text);
            int dotnet_l0 = Convert.ToInt32(NetL0.Text);
            int secore_l1 = Convert.ToInt32(SecoreL1.Text);
            int dotnet_l1 = Convert.ToInt32(NetL1.Text);
            //DateTime modified_datetime = nameTextBox.Text;
            DateTime dateTime = DateTime.Now;

            string modified_time = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            string modified_by = loginId;

            string sqlinsert = "UPDATE project_master SET description = @Description, project_type = @p_type, program_id=@program_id, project_manager=@project_manager, piac_category=@piac, target_release_date=@date1, project_end_date=@date2, country=@country, product=@product, status=@status, secore_l0=@secoreL0, dotnet_l0=@dotnetL0, secore_l1=@secoreL1, dotnet_l1=@dotnetL1, modified_by=@name, modified_datetime=@time WHERE ptsid = @PTSId";

            
            conn.Open();

            MySqlCommand command = new MySqlCommand(sqlinsert, conn);

            command.Parameters.AddWithValue("@PTSId", PtsID);
            command.Parameters.AddWithValue("@Description", description);
            command.Parameters.AddWithValue("@p_type", p_type);
            command.Parameters.AddWithValue("@program_id", program_id);
            command.Parameters.AddWithValue("@project_manager", project_manager);
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
            //command.Parameters.AddWithValue("@archieve", archieve);


            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("data updated");
                this.Close();   
            }
            else
            {
                MessageBox.Show("data not updated");

            }

            
            conn.Close();
            //dataGridView1.Refresh();
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

        private void label_pm_Click(object sender, EventArgs e)
        {

        }

        private void PM_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label_pi_Click(object sender, EventArgs e)
        {

        }

        private void progId_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void piac_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label_product_Click(object sender, EventArgs e)
        {

        }

        private void product_name_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label_net1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SecoreL0_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
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
