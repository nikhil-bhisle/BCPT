using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static Google.Protobuf.Collections.MapField<TKey, TValue>;
using static OfficeOpenXml.ExcelErrorValue;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace BCPT
{
    public partial class AddResource : Form
    {

        private string userName;
        private DataGridView dataGridView1;
        //To add current date and time to the database table
        private DateTime currentTime;
        
        
        public AddResource(string userName, DataGridView dataGridView1)
        {
            InitializeComponent();
            this.userName = userName;
            this.dataGridView1 = dataGridView1;
           


          
           



        }
        private void AddResource_Load(object sender, EventArgs e)
        {
            currentTime = DateTime.Now;
        }
        MySqlConnection connection = new MySqlConnection("server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings");
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataTable table;
        public void FetchSuggestionsFromDatabase()
        {
            try
            {
                connection.Open();

                // Fetch SOEIDs suggestions from the database
                string querySOEIDs = "SELECT DISTINCT SOEID FROM resource_master";
                command = new MySqlCommand(querySOEIDs, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string suggestion = reader["SOEID"].ToString();
                   
                }
                reader.Close();

              

                // Fetch Resource Names suggestions from the database
                string queryResourceNames = "SELECT DISTINCT resource_name FROM resource_master";
                command = new MySqlCommand(queryResourceNames, connection);
                reader = command.ExecuteReader();

              

              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while fetching suggestions: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
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
                MessageBox.Show("Invalid SOEID value. Please enter a valid value for SOEID.");

                comboBox2.Text = "";
            }
            string queryCheck1 = "SELECT COUNT(*) FROM learnings.resource_master WHERE  goc = @selectGoc";
            MySqlCommand command11 = new MySqlCommand(queryCheck1, connection);
            connection.Open();

            command11.Parameters.AddWithValue("@selectGoc", selectGoc);
            int count1 = Convert.ToInt32(command11.ExecuteScalar());

            connection.Close();
            string role = comboBox3.Text;
            string application = comboBox4.Text;
            string location = comboBox5.Text;
            string featureTeam = comboBox6.Text;
            string modified = userName;
            DateTime modifiedTime = currentTime;
            bool archieve = false;
            string queryCheck = "SELECT COUNT(*) FROM learnings.resource_master WHERE  SOEID = @soeID;";
            MySqlCommand command = new MySqlCommand(queryCheck, connection);
            connection.Open();

            command.Parameters.AddWithValue("@soeID", soeID);
            int count = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();


            if (count1 == 0 || count >0)
            {

                if(count >0)
                    MessageBox.Show("SOEID already exists!");
                else
                MessageBox.Show("Please select valid GOC ID.");

                textBox1.Clear();
                textBox2.Clear();
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
                comboBox4.SelectedIndex = -1;
                comboBox5.SelectedIndex = -1;
                comboBox6.SelectedIndex = -1;

            }

        

            

            else
            {
                string sqlInsert = "INSERT INTO resource_master(SOEID,resource_name,resource_manager_name,goc,role,application, location,feature_team, modified_by, modified_datetime,archieve) " + "VALUES (@soeID, @resourceName, @resourceManager, @goc, @role, @application, @location, @featureTeam, @modified, @modifiedDateTime, @archieve)";

                connection.Open();

                command = new MySqlCommand(sqlInsert, connection);

                command.Parameters.AddWithValue("@soeID", soeID);
                command.Parameters.AddWithValue("@resourceName", resourceName);
                command.Parameters.AddWithValue("@resourceManager", resourceManager);
                command.Parameters.AddWithValue("@goc", selectGoc);
                command.Parameters.AddWithValue("@role", role);
                command.Parameters.AddWithValue("@application", application);
                command.Parameters.AddWithValue("@location", location);
                command.Parameters.AddWithValue("@featureTeam", featureTeam);
                command.Parameters.AddWithValue("@modified", modified);
                command.Parameters.AddWithValue("@modifiedDateTime", modifiedTime);
                command.Parameters.AddWithValue("@archieve", archieve);

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Resource Added Succcessfully!");
                    updateComboboxes();
                }
                else
                {
                    MessageBox.Show("There was some error.");

                }

                string query = "SELECT resource_master.SOEID as 'SOEID',resource_master.resource_name as 'Resource Name', resource_master.resource_manager_name as 'Resorce Manager', resource_master.goc as 'GOC', resource_master.role as 'Role', resource_master.application as 'Application',  resource_master.location as 'Location',resource_master.feature_team as 'Feature Team' FROM learnings.resource_master WHERE archieve = 0";
                command = new MySqlCommand(query, connection);

                adapter = new MySqlDataAdapter(command);
                table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;


                updateComboboxes();

                connection.Close();
                this.Close();
            }
        }

      
           private void updateComboboxes() {
               using (connection)
                                                 {

                

                DataTable combobox1Data = GetDistinctColumnValues("resource_manager_name");
                comboBox1.DisplayMember = "resource_manager_name";
                comboBox1.DataSource = combobox1Data;

                // Populate combobox2
                DataTable combobox2Data = GetDistinctColumnValues("goc");
                comboBox2.DisplayMember = "goc";
                comboBox2.DataSource = combobox2Data;

                // Populate combobox3
                DataTable combobox3Data = GetDistinctColumnValues("role");
                comboBox3.DisplayMember = "role";
                comboBox3.DataSource = combobox3Data;

                // Populate combobox4
                DataTable combobox4Data = GetDistinctColumnValues("application");
                comboBox4.DisplayMember = "application";
                comboBox4.DataSource = combobox4Data;

                //Populate combobox5
               DataTable combobox5Data = GetDistinctColumnValues("location");
                comboBox5.DisplayMember = "location";
                comboBox5.DataSource = combobox5Data;

                // Populate 
                // Populate combobox6
                DataTable combobox6Data = GetDistinctColumnValues("feature_team");
                comboBox6.DisplayMember = "feature_team";
                comboBox6.DataSource = combobox6Data;
               }
            connection.Close() ;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            comboBox6.SelectedIndex = -1;

        }


        private void AddResource_Load_1(object sender, EventArgs e)
        {
           

            using (connection)
            {

                connection.Open();

                DataTable combobox1Data = GetDistinctColumnValues("resource_manager_name"); 
                comboBox1.DisplayMember = "resource_manager_name";
                comboBox1.DataSource = combobox1Data;

                // Populate combobox2
                DataTable combobox2Data = GetDistinctColumnValues("goc"); 
                comboBox2.DisplayMember = "goc";
                comboBox2.DataSource = combobox2Data;

                // Populate combobox3
                DataTable combobox3Data = GetDistinctColumnValues("role"); 
                comboBox3.DisplayMember = "role";
                comboBox3.DataSource = combobox3Data;

                // Populate combobox4
                DataTable combobox4Data = GetDistinctColumnValues("application"); 
                comboBox4.DisplayMember = "application";
                comboBox4.DataSource = combobox4Data;

                // Populate combobox5
                DataTable combobox5Data = GetDistinctColumnValues("location"); 
                comboBox5.DisplayMember = "location";
                comboBox5.DataSource = combobox5Data;

                // Populate combobox6
                DataTable combobox6Data = GetDistinctColumnValues("feature_team"); 
                comboBox6.DisplayMember = "feature_team";
                comboBox6.DataSource = combobox6Data;
            }
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            comboBox6.SelectedIndex = -1;
            connection.Close();
        }
        private DataTable GetDistinctColumnValues(string columnName)
        {
            using (MySqlConnection connection= new MySqlConnection("server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings"))
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
