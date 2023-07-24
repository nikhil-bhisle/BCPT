using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Engineering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BCPT
{
    
    public partial class EditResource : Form
    {

        private string soeid;
        private string userName;
        public DateTime currentTime;
        static string connection = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
        MySqlConnection conn = new MySqlConnection(connection);
       
        public EditResource(string soeid, string userName)
        {
            InitializeComponent();
            this.soeid = soeid;
            this.userName = userName;
            currentTime= DateTime.Now;
            string query = "Select * from resource_master where SOEID = @soeid";
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@soeid", soeid);
                DataTable table = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }

                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];

                    textBox1.Text = row["SOEID"].ToString();
                    textBox2.Text = row["resource_name"].ToString();
                    comboBox1.Text = row["resource_manager_name"].ToString();
                    comboBox2.Text = row["goc"].ToString();
                    comboBox3.Text = row["role"].ToString();
                    comboBox4.Text = row["application"].ToString();
                    comboBox5.Text = row["location"].ToString();
                    comboBox6.Text = row["feature_team"].ToString();

                   
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
                // Retrieve form data
                string soeID = textBox1.Text;
                string resourceName = textBox2.Text;
                string resourceManager = comboBox1.Text;
            int selectGoc;

            if (int.TryParse(comboBox2.Text, out selectGoc))
            {
                int goc = selectGoc;
            }
            else
            {
                // Conversion failed, handle the error condition
                MessageBox.Show("Invalid GOC value. Please enter a valid integer value for GOC.");

                comboBox2.Text = ""; // Clear the ComboBox value
                                     // Or, you can set a default value
                                     // goc = 0; // Set a default value
            }

            string role = comboBox3.Text;
                string application = comboBox4.Text;
                string location = comboBox5.Text;
                string featureTeam = comboBox6.Text;
                string modified = userName;
                DateTime modifiedTime = currentTime;
            bool archieve = false;

                string sqlUpdate = "UPDATE resource_master SET resource_name = @resourceName, resource_manager_name = @resourceManager, goc = @goc, role = @role, application = @application, location = @location, feature_team = @featureTeam, modified_by = @modifiedBy, modified_datetime = @modifiedTime,archieve= @archieve WHERE SOEID = @soeID";
                conn.Open();

                MySqlCommand command = new MySqlCommand(sqlUpdate, conn);

                command.Parameters.AddWithValue("@soeID", soeID);
                command.Parameters.AddWithValue("@resourceName", resourceName);
                command.Parameters.AddWithValue("@resourceManager", resourceManager);
                command.Parameters.AddWithValue("@goc", selectGoc);
                command.Parameters.AddWithValue("@role", role);
                command.Parameters.AddWithValue("@application", application);
                command.Parameters.AddWithValue("@location", location);
                command.Parameters.AddWithValue("@featureTeam", featureTeam);
                command.Parameters.AddWithValue("@modifiedBy", modified);
                command.Parameters.AddWithValue("@modifiedTime", modifiedTime);
            command.Parameters.AddWithValue("@archieve", archieve);

            if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Resource Edited Successfully!");
                }
                else
                {
                    MessageBox.Show("There was some error.");
                }

           

            conn.Close();
                this.Close();
        }

        private void EditResource_Load(object sender, EventArgs e)
        {

            using (MySqlConnection conn= new MySqlConnection(connection))
            {

                conn.Open();

                DataTable combobox1Data = GetDistinctColumnValues("resource_manager_name");
                comboBox1.DisplayMember = "role";
                comboBox1.DataSource = combobox1Data;
               
                comboBox1.ValueMember = "resource_manager_name";

                // Populate combobox2
                DataTable combobox2Data = GetDistinctColumnValues("goc");
                comboBox2.DisplayMember = "role";
                comboBox2.DataSource = combobox2Data;

                comboBox2.ValueMember = "goc";

                // Populate combobox3
                DataTable combobox3Data = GetDistinctColumnValues("role"); 
                comboBox3.DisplayMember = "role";
                comboBox3.DataSource = combobox3Data;
                comboBox3.ValueMember = "role";

                // Populate combobox4
                DataTable combobox4Data = GetDistinctColumnValues("application"); 
                comboBox4.DisplayMember = "application";
                comboBox4.DataSource = combobox4Data;
                comboBox4.ValueMember = "application";

                // Populate combobox5
                DataTable combobox5Data = GetDistinctColumnValues("location");
                comboBox6.DisplayMember = "location";
                comboBox5.DataSource = combobox5Data;
                comboBox5.ValueMember = "location";

                // Populate combobox6
                DataTable combobox6Data = GetDistinctColumnValues("feature_team");
                comboBox6.DisplayMember = "feature_team";
                comboBox6.DataSource = combobox6Data;
                comboBox6.ValueMember = "feature_team";
            }
           

            conn.Close();
        }
        private DataTable GetDistinctColumnValues(string columnName)
        {
            using (MySqlConnection connection = new MySqlConnection("server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings"))
            using (MySqlCommand command = new MySqlCommand($"SELECT DISTINCT `{columnName}` FROM resource_master", connection))
            {
                connection.Open();
                DataTable dataTable = new DataTable();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    dataTable.Load(reader);
                }
                return dataTable;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
