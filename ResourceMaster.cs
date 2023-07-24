using ConsolFromApp;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace BCPT
{
    public partial class ResourceMaster : UserControl
    {
        //For username credentials
        public string userName;
        public string loginId;


        public static ResourceMaster instance;
        public ResourceMaster()
        {
            InitializeComponent();
            instance = this;
        }
        static string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
        MySqlConnection connection = new MySqlConnection(connstring);
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataTable table;
        private DataTable GetResourceMasterData()
        {
            //string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
            MySqlConnection conn = new MySqlConnection(connstring);
            DataTable dataTable = new DataTable();
            try
            {
                conn.Open();
                string sql = " SELECT  resource_master.SOEID as 'SOEID',resource_master.resource_name as 'Resource Name', resource_master.resource_manager_name as 'Resorce Manager', resource_master.goc as 'GOC', resource_master.role as 'Role', resource_master.application as 'Application',  resource_master.location as 'Location',resource_master.feature_team as 'Feature Team' FROM learnings.resource_master resource_master";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dataTable.Load(reader);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dataTable;
        }
        /*
        private string GetRole(string username)
        {
            string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
            string role = string.Empty;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();

                    string sql = $"SELECT role FROM login_new WHERE username = @username";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        role = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return role;
        }
        */

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           AddResource form2 = new AddResource(userName,dataGridView1);
            form2.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "SELECT resource_master.SOEID as 'SOEID',resource_master.resource_name as 'Resource Name', resource_master.resource_manager_name as 'Resorce Manager', resource_master.goc as 'GOC', resource_master.role as 'Role', resource_master.application as 'Application',  resource_master.location as 'Location',resource_master.feature_team as 'Feature Team' FROM learnings.resource_master WHERE archieve = 0";
            command = new MySqlCommand(query, connection);

            adapter = new MySqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;


            updateComboboxes();

        }
        private void updateComboboxes()
        {
            using (connection)
            {

                

                DataTable combobox1Data = GetDistinctColumnValues("SOEID");
                comboBox1.DisplayMember = "resource_manager_name";
                comboBox1.DataSource = combobox1Data;

                // Populate combobox2
                DataTable combobox2Data = GetDistinctColumnValues("feature_team");
                comboBox2.DisplayMember = "goc";
                comboBox2.DataSource = combobox2Data;

                // Populate combobox3
                DataTable combobox3Data = GetDistinctColumnValues("application");
                comboBox3.DisplayMember = "application";
                comboBox3.DataSource = combobox3Data;

               
            }
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
           
            connection.Close();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string query = "SELECT  resource_master.SOEID as 'SOEID',resource_master.resource_name as 'Resource Name', resource_master.resource_manager_name as 'Resorce Manager', resource_master.goc as 'GOC', resource_master.role as 'Role', resource_master.application as 'Application',  resource_master.location as 'Location',resource_master.feature_team as 'Feature Team' FROM learnings.resource_master WHERE archieve = 1";
            command = new MySqlCommand(query, connection);

            adapter = new MySqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;


            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (connection = new MySqlConnection(connstring))
            {
                connection.Open();
                int totalRows = this.dataGridView1.RowCount;

                List<string> listOfsoeid = new List<string>();

                for (int i = 0; i < totalRows; i++)
                {
                    if (this.dataGridView1.Rows[i].Cells[0].Value != null && (bool)this.dataGridView1.Rows[i].Cells[0].Value == true)
                    {
                        listOfsoeid.Add((((System.Data.DataTable)this.dataGridView1.DataSource).Rows[i]["SOEID"]).ToString());
                    }
                }

                int j = listOfsoeid.Count();
                for (int i = 0; i < j; i++)
                {
                    string soeid = listOfsoeid[i];
                    string query = "UPDATE resource_master SET archieve = 1 WHERE SOEID = @soeid";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@soeid", soeid);

                        // Execute the update command
                        command.ExecuteNonQuery();
                    }
                }

                // Close the connection after all updates have been performed
                connection.Close();

                // Display the message box and retrieve the updated data
                if (listOfsoeid.Count > 0)
                {
                    MessageBox.Show(listOfsoeid.Count.ToString() + " rows will be archived", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    string query1 = "SELECT resource_master.SOEID as 'SOEID',resource_master.resource_name as 'Resource Name', resource_master.resource_manager_name as 'Resorce Manager', resource_master.goc as 'GOC', resource_master.role as 'Role', resource_master.application as 'Application',  resource_master.location as 'Location',resource_master.feature_team as 'Feature Team' FROM learnings.resource_master WHERE archieve <> 1";
                    using (MySqlCommand command1 = new MySqlCommand(query1, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command1))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dataGridView1.DataSource = dataTable;
                        }
                    }
                }
            }
        }

        private void UnarchieveRows(List<string> listOfSoeid)
        {
            try
            {
                connection.Open();

                foreach (string soeid in listOfSoeid)
                {
                    string updateQuery = "UPDATE resource_master SET archieve = 0 WHERE SOEID = @soeid";
                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            List<string> deletedRowIds = new List<string>();

            
            for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                DataGridViewRow row = dataGridView1.Rows[i];

                
                DataGridViewCheckBoxCell checkboxCell = (DataGridViewCheckBoxCell)row.Cells["select"]; 
                if (Convert.ToBoolean(checkboxCell.Value))
                {


                    // Get the primary key or unique identifier of the deleted row
                    string rowId = Convert.ToString(row.Cells[1].Value); 
                    MessageBox.Show("SOEID " + rowId.ToString()+" will be deleted ?","Alert", MessageBoxButtons.OK);

                    deletedRowIds.Add(rowId);
                   

                }
               


                for (int j = 0; j < deletedRowIds.Count; j++)
                {
                    string soeidToDelete = deletedRowIds[j];
                    Debug.WriteLine(soeidToDelete);
                    string querytoupdate = "update learnings.resource_master set modified_by = @modified, modified_datetime=@time where SOEID = @soeid";
                    DateTime time = DateTime.Now;
                    string modified_time = time.ToString("yyyy-MM-dd HH:mm:ss");
                    using (MySqlConnection connection = new MySqlConnection(connstring))
                    {
                        try
                        {
                            connection.Open();
                            MySqlCommand cmd = new MySqlCommand(querytoupdate, connection);
                            cmd.Parameters.AddWithValue("@soeid", soeidToDelete);
                            cmd.Parameters.AddWithValue("@modified", loginId);
                            cmd.Parameters.AddWithValue("@time", modified_time);
                            int rowsAffected = cmd.ExecuteNonQuery();

                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show("An error ocurred: " + ex.Message);
                        }
                    }
                }
            }

            // Delete the rows from the database using the list of deletedRowIds
            DeleteRowsFromDatabase(deletedRowIds);

            string query = "SELECT resource_master.SOEID as 'SOEID',resource_master.resource_name as 'Resource Name', resource_master.resource_manager_name as 'Resorce Manager', resource_master.goc as 'GOC', resource_master.role as 'Role', resource_master.application as 'Application',  resource_master.location as 'Location',resource_master.feature_team as 'Feature Team' FROM learnings.resource_master WHERE archieve = 0";
            command = new MySqlCommand(query, connection);

            adapter = new MySqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;


            updateComboboxes();
        }
        private void DeleteRowsFromDatabase(List<string> deletedRowIds)
        {
            
             connection = new MySqlConnection(connstring);
            int k = deletedRowIds.Count;
            if (k == 0)
            {
                MessageBox.Show("Select one row to delete");
            }

            try
            {
                connection.Open();

                foreach (string rowId in deletedRowIds)
                {
                    // Execute the DELETE query using the rowId
                    string deleteQuery = "DELETE FROM learnings.resource_master WHERE SOEID = '" + rowId + "'"; 
                    MySqlCommand cmd = new MySqlCommand(deleteQuery, connection);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("This row can't be deleted.");
            }
            finally
            {
                connection.Close();
            }
        }

        private void button_modify_Click(object sender, EventArgs e)
        {
            using (connection = new MySqlConnection(connstring))
            {
                connection.Open();
                int totalRows = this.dataGridView1.RowCount;

                List<string> listOfSoeids = new List<string>();


                for (int i = 0; i < totalRows; i++)
                {

                    if (this.dataGridView1.Rows[i].Cells[0].Value != null && (bool)this.dataGridView1.Rows[i].Cells[0].Value == true)
                    {
                        listOfSoeids.Add((((System.Data.DataTable)this.dataGridView1.DataSource).Rows[i]["SOEID"]).ToString());

                    }

                }
                int j = listOfSoeids.Count;
                if (j > 1)
                {

                    MessageBox.Show("Choose only one resource to edit at one time.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                else if (j == 0)
                {
                    MessageBox.Show("Select one row to edit");
                }
                else
                {

                    MessageBox.Show(listOfSoeids.Count().ToString()+ " row will be edited!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    string soeid = listOfSoeids[0];

                    EditResource form3 = new EditResource(soeid,userName);
                    form3.Show();

                    form3.FormClosed += Form3__FormClosed;




                }


            }


        }
        public void Form3__FormClosed(object sender, FormClosedEventArgs e)
        {
            string query = "SELECT * FROM resource_master";
            command = new MySqlCommand(query, connection);

            adapter = new MySqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            dataGridView1.Refresh();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       
        private void PopulateComboBoxes()
        {
            // Populate soeid combobox
            DataTable soeidData = GetDistinctColumnValues("SOEID");
            comboBox1.DisplayMember = "SOEID";
            comboBox1.DataSource = soeidData;

            // Populate Application combobox
            DataTable applicationData = GetDistinctColumnValues("application");
            comboBox3.DisplayMember = "application";
            comboBox3.DataSource = applicationData;

            // Populate Feature Team combobox
            DataTable featureTeamData = GetDistinctColumnValues("feature_team");
            comboBox2.DisplayMember = "feature_team";
            comboBox2.DataSource = featureTeamData;

            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
        }

        private DataTable GetDistinctColumnValues(string columnName)
        {
            
            string query = $"SELECT DISTINCT {columnName} FROM resource_master";

            using (MySqlConnection connection = new MySqlConnection(connstring))
            using (MySqlCommand command = new MySqlCommand(query, connection))
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

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM learnings.resource_master WHERE 1=1";
            string selectedSoeid = comboBox1.Text;
            string selectedApplication = comboBox3.Text;
            string selectedFeatureTeam = comboBox2.Text;

            if (comboBox1.SelectedItem != null && !string.IsNullOrWhiteSpace(comboBox1.SelectedItem.ToString()))
            {
                query += " AND SOEID = @selectedSoeid";
            }
            if (comboBox3.SelectedItem != null && !string.IsNullOrWhiteSpace(comboBox3.SelectedItem.ToString()))
            {
                query += " AND application = @selectedApplication";
            }
             if (comboBox2.SelectedItem != null && !string.IsNullOrWhiteSpace(comboBox2.SelectedItem.ToString()))
            {
                query += " AND feature_team = @selectedFeatureTeam";
            }

            using (MySqlConnection connection = new MySqlConnection(connstring))
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@selectedSoeid", selectedSoeid);
                command.Parameters.AddWithValue("@selectedApplication", selectedApplication);
                command.Parameters.AddWithValue("@selectedFeatureTeam", selectedFeatureTeam);

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();

                    try
                    {
                        connection.Open();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions that occur
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            using (connection = new MySqlConnection(connstring))
            {
                connection.Open();
                int totalRows = this.dataGridView1.RowCount;

                List<string> listOfsoeid = new List<string>();

                for (int i = 0; i < totalRows; i++)
                {
                    if (this.dataGridView1.Rows[i].Cells[0].Value != null && (bool)this.dataGridView1.Rows[i].Cells[0].Value == true)
                    {
                        listOfsoeid.Add((((System.Data.DataTable)this.dataGridView1.DataSource).Rows[i]["SOEID"]).ToString());
                    }
                }

                int j = listOfsoeid.Count();
                for (int i = 0; i < j; i++)
                {
                    string soeid = listOfsoeid[i];
                    string query = "UPDATE resource_master SET archieve = 0 WHERE SOEID = @soeid";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@soeid", soeid);

                        // Execute the update command
                        command.ExecuteNonQuery();
                    }
                }

                // Close the connection after all updates have been performed
                connection.Close();

                // Display the message box and retrieve the updated data
                if (listOfsoeid.Count > 0)
                {
                    MessageBox.Show(listOfsoeid.Count.ToString() + " rows will be unarchived", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);

                   
                }
                string query1 = "SELECT  resource_master.SOEID as 'SOEID',resource_master.resource_name as 'Resource Name', resource_master.resource_manager_name as 'Resorce Manager', resource_master.goc as 'GOC', resource_master.role as 'Role', resource_master.application as 'Application',  resource_master.location as 'Location',resource_master.feature_team as 'Feature Team' FROM learnings.resource_master WHERE archieve <> 1";
                using (MySqlCommand command1 = new MySqlCommand(query1, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command1))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }


            }
        }

        private void ResourceMaster_Load(object sender, EventArgs e)
        {
            /*
            string userRole = GetRole(MainForm.instance.user.Text);

            // Check the user's role and set the visibility of buttons accordingly
            if (userRole == "admin")
            {
                // If user is an admin, show the buttons
                button7.Visible = true;
                button5.Visible = true;
                button4.Visible = true;
                button1.Visible = true;
                button_modify.Visible = true;
            }
            else
            {
                // If user is not an admin, hide the buttons
                button7.Visible = false;
                button5.Visible = false;
                button4.Visible = false;
                button1.Visible = false;
                button_modify.Visible = false;
            }
            */
            using (connection)
            {
                connection.Open();
                DataTable combobox1Data = GetDistinctColumnValues("SOEID");
                comboBox1.DisplayMember = "SOEID";
                comboBox1.DataSource = combobox1Data;

                DataTable combobox2Data = GetDistinctColumnValues("feature_team");
                comboBox2.DisplayMember = "feature_team";
                comboBox2.DataSource = combobox2Data;

                DataTable combobox3Data = GetDistinctColumnValues("application");
                comboBox3.DisplayMember = "application";
                comboBox3.DataSource = combobox3Data;
            }
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;

            string query = "SELECT resource_master.SOEID as 'SOE" +
                "ID',resource_master.resource_name as 'Resource Name', resource_master.resource_manager_name as 'Resorce Manager', resource_master.goc as 'GOC', resource_master.role as 'Role', resource_master.application as 'Application',  resource_master.location as 'Location',resource_master.feature_team as 'Feature Team' FROM learnings.resource_master WHERE archieve = 0";
            command = new MySqlCommand(query, connection);

            adapter = new MySqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;


            connection.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Get the current cell
            DataGridViewCell cell = dataGridView1[e.ColumnIndex, e.RowIndex];

            // Check if the current cell belongs to the checkbox column
            if (cell.OwningColumn.Name != "select")
            {
                // Cancel the editing for all columns except the checkbox column
                e.Cancel = true;
            }
        }
    }
}
