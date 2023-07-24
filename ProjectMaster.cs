using ConsolFromApp;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp2
{
    public partial class ProjectMaster : UserControl
    {
        public string loginId;
        public static ProjectMaster instance;

        DataTable projectMasterTable; 

        private string selectedProjectType = ""; 

        public ProjectMaster()
        {
            InitializeComponent();

            instance = this;

            comboBox_p_type.SelectedIndexChanged += comboBox_p_type_SelectedIndexChanged;

        }
        static string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
        MySqlConnection connection = new MySqlConnection(connstring);
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataTable table;
        string ptsid_row;
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
        private DataTable GetProjectMasterData()
        {
            MySqlConnection conn = new MySqlConnection(connstring);
            DataTable dataTable = new DataTable();
            try
            {
                conn.Open();
                string sql = "SELECT project_master.PTSID as 'PTS ID',project_master.description as 'Project Description', project_master.project_type as 'Project Type', project_master.program_id as 'Program Id', project_master.project_manager as 'Project Manager', project_master.piac_category as 'PIAC Category', project_master.target_release_date as 'Target Release Date', project_master.project_end_date as 'Project End Date', project_master.country as 'Country', project_master.product as 'Product', project_master.status as 'Status', project_master.secore_l0 as 'Secore L0', project_master.dotnet_l0 as '.Net L0', project_master.secore_l1 as 'Secore L1', project_master.dotnet_l1 as '.Net L1' FROM project_master";
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


        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;

            // Check if the new row is the add row
            if (e.RowIndex == dataGridView.Rows.Count - 1 && !dataGridView.Rows[e.RowIndex].IsNewRow)
            {
                // Set the checkbox value for the new row
                DataGridViewCheckBoxCell checkboxCell = (DataGridViewCheckBoxCell)dataGridView.Rows[e.RowIndex].Cells["CheckboxColumn"];
                checkboxCell.Value = false;
            }
        }



        private void UserControl1_Load(object sender, EventArgs e)
        {
            /*
            string userRole = GetRole(MainForm.instance.user.Text);

            // Check the user's role and set the visibility of buttons accordingly
            if (userRole == "admin")
            {
                // If user is an admin, show the buttons
                button4.Visible = true;
                button5.Visible = true;
                button1.Visible = true;
                button10.Visible = true;
                button_modify.Visible = true;
            }
            else
            {
                // If user is not an admin, hide the buttons
                button4.Visible = false;            
                button5.Visible = false;
                button1.Visible = false;
                button10.Visible = false;
                button_modify.Visible = false;
            }
            */

            projectMasterTable = GetProjectMasterData(); 

            dataGridView1.DataSource = projectMasterTable;

            DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn
            {
                Name = "CheckboxColumn",
                HeaderText = "Select",
                DataPropertyName = "Select",
                Width = 50
            };
            dataGridView1.Columns.Insert(0, checkboxColumn);

            dataGridView1.RowsAdded += dataGridView1_RowsAdded;
            button4.Click += button4_Click;

            string query = "SELECT DISTINCT project_type FROM project_master";
            command = new MySqlCommand(query, connection);

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string projectType = reader.GetString(reader.GetOrdinal("project_type"));

                comboBox_p_type.Items.Add(projectType);
            }

            reader.Close();
            connection.Close();

            viewAllFunc();

        }
     
        private void button1_Click_1(object sender, EventArgs e)
        {
            WindowsFormsApp1.Add_Project_Master form2 = new WindowsFormsApp1.Add_Project_Master(loginId);

            form2.FormClosed += Form2__FormClosed;
            form2.StartPosition = FormStartPosition.CenterScreen;
            form2.Show();

            viewAllFunc();
        }
     
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        public void viewAllFunc()
        {
            string query = "SELECT project_master.PTSID AS 'PTS ID',project_master.description AS 'Project Description',project_master.project_type AS 'Project Type',project_master.program_id AS 'Program Id',resource_master.resource_name AS 'Project Manager', project_master.piac_category AS 'PIAC Category', project_master.target_release_date AS 'Target Release Date', project_master.project_end_date AS 'Project End Date', project_master.country AS 'Country', project_master.product AS 'Product', project_master.status AS 'Status', project_master.secore_l0 AS 'Secore L0', project_master.dotnet_l0 AS '.Net L0', project_master.secore_l1 AS 'Secore L1', project_master.dotnet_l1 AS '.Net L1' FROM project_master JOIN resource_master ON resource_master.soeid = project_master.project_manager WHERE project_master.archieve = 0;";
            command = new MySqlCommand(query, connection);

            adapter = new MySqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;

            textBox1.Clear();
            comboBox_p_type.Text = string.Empty;
            comboBox_p_type.SelectedItem = null;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            viewAllFunc();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string valueToSearch = textBox1.Text.ToString();
             // Retrieve the selected project type, or an empty string if none is selected
            Debug.WriteLine(selectedProjectType);
            searchData(valueToSearch);
        }

        public void searchData(string valueToSearch)
        {
            
            string query = "";
            if (comboBox_p_type.SelectedIndex != -1)
            {
                string selectedProjectType = comboBox_p_type.SelectedItem.ToString();
                query = "SELECT project_master.PTSID as 'PTS ID',project_master.description as 'Project Description', project_master.project_type as 'Project Type', project_master.program_id as 'Program Id', project_master.project_manager as 'Project Manager', project_master.piac_category as 'PIAC Category', project_master.target_release_date as 'Target Release Date', project_master.project_end_date as 'Project End Date', project_master.country as 'Country', project_master.product as 'Product', project_master.status as 'Status', project_master.secore_l0 as 'Secore L0', project_master.dotnet_l0 as '.Net L0', project_master.secore_l1 as 'Secore L1', project_master.dotnet_l1 as '.Net L1' FROM project_master where project_type='" + selectedProjectType + "' AND description LIKE '%" + valueToSearch + "%'";
               
            }
            else
            {
                query = "SELECT project_master.PTSID as 'PTS ID',project_master.description as 'Project Description', project_master.project_type as 'Project Type', project_master.program_id as 'Program Id', project_master.project_manager as 'Project Manager', project_master.piac_category as 'PIAC Category', project_master.target_release_date as 'Target Release Date', project_master.project_end_date as 'Project End Date', project_master.country as 'Country', project_master.product as 'Product', project_master.status as 'Status', project_master.secore_l0 as 'Secore L0', project_master.dotnet_l0 as '.Net L0', project_master.secore_l1 as 'Secore L1', project_master.dotnet_l1 as '.Net L1' from project_master where description LIKE '%" + valueToSearch + "%'";
            }
            command = new MySqlCommand(query, connection);
            adapter = new MySqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button4_Click(object sender, EventArgs e)
        {
 
            List<string> deletedRowIds = new List<string>();

            for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                DataGridViewRow row = dataGridView1.Rows[i];

                DataGridViewCheckBoxCell checkboxCell = (DataGridViewCheckBoxCell)row.Cells["CheckboxColumn"]; 
                if (Convert.ToBoolean(checkboxCell.Value))
                {
                    string rowId = Convert.ToString(row.Cells[1].Value); 

                    deletedRowIds.Add(rowId);
 
                }
            }

            if (deletedRowIds.Count > 0)
            {
                MessageBox.Show("Selected rows will be deleted");
            }

            for (int j=0; j < deletedRowIds.Count; j++)
            {
                string ptstodelete = deletedRowIds[j];
                Debug.WriteLine(ptstodelete);
                string querytoupdate = "update project_master set modified_by = @modified, modified_datetime=@time where PTSID = @ptsid";
                DateTime time = DateTime.Now;
                string modified_time = time.ToString("yyyy-MM-dd HH:mm:ss");
                using (MySqlConnection connection = new MySqlConnection(connstring))
                {
                    try
                    {
                        connection.Open();
                        MySqlCommand cmd = new MySqlCommand(querytoupdate, connection);
                        cmd.Parameters.AddWithValue("@ptsid", ptstodelete);
                        cmd.Parameters.AddWithValue("@modified", loginId);
                        cmd.Parameters.AddWithValue("@time", modified_time);
                        int rowsAffected=cmd.ExecuteNonQuery();
                    }

                    catch(Exception ex)
                    {
                        MessageBox.Show("An error ocucured: " + ex.Message);
                    }
                }
            }
       
            DeleteRowsFromDatabase(deletedRowIds);
  
            viewAllFunc();
        }

        private void DeleteRowsFromDatabase(List<string> deletedRowIds)
        {
            string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
            MySqlConnection conn = new MySqlConnection(connstring);

            try
            {
                conn.Open();

                foreach (string rowId in deletedRowIds)
                {
                    string deleteQuery = "DELETE FROM project_master WHERE PTSID = '" + rowId + "'"; 
                    MySqlCommand cmd = new MySqlCommand(deleteQuery, conn);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The project can not be deleted because it is currently in use", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }
      
        private void button5_Click(object sender, EventArgs e)
        {
            bool isRowSelected = false;
            DataGridViewRow selectedRow = null;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCheckBoxCell checkboxCell = row.Cells["CheckboxColumn"] as DataGridViewCheckBoxCell;
                bool isChecked = Convert.ToBoolean(checkboxCell.Value);

                if (isChecked)
                {
                    if (isRowSelected)
                    {
                        MessageBox.Show("Please select only one row at a time.");
                        return;
                    }

                    isRowSelected = true;
                    selectedRow = row;
                }
            }

            if (isRowSelected)
            {
                string ptsid = selectedRow.Cells["PTS ID"].Value.ToString();
                
                Edit_Project_Master editForm = new Edit_Project_Master(ptsid, loginId);
                editForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a row with the checkbox selected.");
            }
        }

        public void Form5__FormClosed(object sender, FormClosedEventArgs e)
        {
            string query = "SELECT project_master.PTSID as 'PTS ID',project_master.description as 'Project Description', project_master.project_type as 'Project Type', project_master.program_id as 'Program Id', project_master.project_manager as 'Project Manager', project_master.piac_category as 'PIAC Category', project_master.target_release_date as 'Target Release Date', project_master.project_end_date as 'Project End Date', project_master.country as 'Country', project_master.product as 'Product', project_master.status as 'Status', project_master.secore_l0 as 'Secore L0', project_master.dotnet_l0 as '.Net L0', project_master.secore_l1 as 'Secore L1', project_master.dotnet_l1 as '.Net L1' FROM project_master";
            command = new MySqlCommand(query, connection);

            adapter = new MySqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            dataGridView1.Refresh();
        }

        public void Form2__FormClosed(object sender, FormClosedEventArgs e)
        {
            string query = "SELECT project_master.PTSID as 'PTS ID',project_master.description as 'Project Description', project_master.project_type as 'Project Type', project_master.program_id as 'Program Id', project_master.project_manager as 'Project Manager', project_master.piac_category as 'PIAC Category', project_master.target_release_date as 'Target Release Date', project_master.project_end_date as 'Project End Date', project_master.country as 'Country', project_master.product as 'Product', project_master.status as 'Status', project_master.secore_l0 as 'Secore L0', project_master.dotnet_l0 as '.Net L0', project_master.secore_l1 as 'Secore L1', project_master.dotnet_l1 as '.Net L1' FROM project_master";
            command = new MySqlCommand(query, connection);

            adapter = new MySqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            dataGridView1.Refresh();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {

            using (connection = new MySqlConnection(connstring))
            {
                connection.Open();
                int totalRows = this.dataGridView1.RowCount;

                List<string> listOfPtsids = new List<string>();

                for (int i = 0; i < totalRows; i++)
                {
                    if (this.dataGridView1.Rows[i].Cells[0].Value != null && (bool)this.dataGridView1.Rows[i].Cells[0].Value == true)
                    {
                        listOfPtsids.Add((((System.Data.DataTable)this.dataGridView1.DataSource).Rows[i]["PTS ID"]).ToString());
                    }
                }

                int j = listOfPtsids.Count();
                for (int i = 0; i < j; i++)
                {
                    string ptsid = listOfPtsids[i];
                    string query = "UPDATE project_master SET archieve = 1 WHERE PTSID = @ptsid";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ptsid", ptsid);

                        // Execute the update command
                        command.ExecuteNonQuery();
                    }
                }

                connection.Close();

                if (listOfPtsids.Count > 0)
                {
                    MessageBox.Show(listOfPtsids.Count.ToString() + " rows will be archived", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    string query1 = "SELECT project_master.PTSID as 'PTS ID',project_master.description as 'Project Description', project_master.project_type as 'Project Type', project_master.program_id as 'Program Id', project_master.project_manager as 'Project Manager', project_master.piac_category as 'PIAC Category', project_master.target_release_date as 'Target Release Date', project_master.project_end_date as 'Project End Date', project_master.country as 'Country', project_master.product as 'Product', project_master.status as 'Status', project_master.secore_l0 as 'Secore L0', project_master.dotnet_l0 as '.Net L0', project_master.secore_l1 as 'Secore L1', project_master.dotnet_l1 as '.Net L1' FROM project_master WHERE archieve <> 1";
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

            viewAllFunc();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string query = "SELECT project_master.PTSID AS 'PTS ID',project_master.description AS 'Project Description',project_master.project_type AS 'Project Type',project_master.program_id AS 'Program Id',resource_master.resource_name AS 'Project Manager', project_master.piac_category AS 'PIAC Category', project_master.target_release_date AS 'Target Release Date', project_master.project_end_date AS 'Project End Date', project_master.country AS 'Country', project_master.product AS 'Product', project_master.status AS 'Status', project_master.secore_l0 AS 'Secore L0', project_master.dotnet_l0 AS '.Net L0', project_master.secore_l1 AS 'Secore L1', project_master.dotnet_l1 AS '.Net L1' FROM project_master JOIN resource_master ON resource_master.soeid = project_master.project_manager WHERE project_master.archieve = 1;";
            command = new MySqlCommand(query, connection);

            adapter = new MySqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;

            
        }       

        private void UnarchieveRows(List<string> selectedRowIds)
        {
            try
            {
                connection.Open();

                foreach (string rowId in selectedRowIds)
                {
                    string updateQuery = "UPDATE project_master SET archieve = 0 WHERE PTSID = '" + rowId + "'";
                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void comboBox_p_type_SelectedIndexChanged(object sender, EventArgs e)
        {

            string selectedProjectType = comboBox_p_type.SelectedItem?.ToString();
            string valueToSearch = textBox1.Text;

            if (selectedProjectType != null)
            {
                DataView dataView = projectMasterTable.DefaultView;
                dataView.RowFilter = "[Project Type]='" + selectedProjectType + "'";
                dataGridView1.DataSource = dataView.ToTable();
            }
            else
            {
                viewAllFunc();
            }

            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            using (connection = new MySqlConnection(connstring))
            {
                connection.Open();
                int totalRows = this.dataGridView1.RowCount;

                List<string> listOfPtsids = new List<string>();

                for (int i = 0; i < totalRows; i++)
                {
                    if (this.dataGridView1.Rows[i].Cells[0].Value != null && (bool)this.dataGridView1.Rows[i].Cells[0].Value == true)
                    {
                        listOfPtsids.Add((((System.Data.DataTable)this.dataGridView1.DataSource).Rows[i]["PTS ID"]).ToString());
                    }
                }

                int j = listOfPtsids.Count();
                for (int i = 0; i < j; i++)
                {
                    string ptsid = listOfPtsids[i];
                    string query = "UPDATE project_master SET archieve =0 WHERE PTSID = @ptsid";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ptsid", ptsid);

                        // Execute the update command
                        command.ExecuteNonQuery();
                    }
                }

                connection.Close();

                if (listOfPtsids.Count > 0)
                {
                    MessageBox.Show(listOfPtsids.Count.ToString() + " Rows will be unarchived", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    string query1 = "SELECT project_master.PTSID as 'PTS ID',project_master.description as 'Project Description', project_master.project_type as 'Project Type', project_master.program_id as 'Program Id', project_master.project_manager as 'Project Manager', project_master.piac_category as 'PIAC Category', project_master.target_release_date as 'Target Release Date', project_master.project_end_date as 'Project End Date', project_master.country as 'Country', project_master.product as 'Product', project_master.status as 'Status', project_master.secore_l0 as 'Secore L0', project_master.dotnet_l0 as '.Net L0', project_master.secore_l1 as 'Secore L1', project_master.dotnet_l1 as '.Net L1' FROM project_master WHERE archieve <> 1";
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

            viewAllFunc();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewCell cell = dataGridView1[e.ColumnIndex, e.RowIndex];

            if (cell.OwningColumn.Name != "CheckboxColumn")
            {
                // Cancel the editing for all columns except the checkbox column
                e.Cancel = true;
            }
        }
               
    }
}