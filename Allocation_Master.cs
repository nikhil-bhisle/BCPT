using ConsolFromApp;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace BCPT
{
    public partial class Allocation_Master : UserControl
    {
        public Allocation_Master()
        {
            InitializeComponent();
        }

        // Create a dictionary to map month numbers to month names
        Dictionary<int, string> monthNames = new Dictionary<int, string>()
        {
                { 1, "January" },
                { 2, "February" },
                { 3, "March" },
                { 4, "April" },
                { 5, "May" },
                { 6, "June" },
                { 7, "July" },
                { 8, "August" },
                { 9, "September" },
                { 10, "October" },
                { 11, "November" },
                { 12, "December" }
        };

        private void amViewDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        private void Allocation_Master_Load(object sender, EventArgs e)
        {
        
            /*
            string userRole=GetRole(MainForm.instance.user.Text); ;
            
            // Check the user's role and set the visibility of buttons accordingly
            if (userRole == "admin")
            {
                // If user is an admin, show the buttons
                button4.Visible = true;
                button3.Visible = true;
                button5.Visible = true;
                editAllocationBtn.Visible = true;
                archieveButton.Visible = true;
            }
            else
            {
                // If user is not an admin, hide the buttons
                button4.Visible = false;
                button3.Visible = false;
                editAllocationBtn.Visible = false;
                archieveButton.Visible = false;
                button5.Visible = false;
            }
            */

            PopulateComboBoxes();
            // Assuming the column index of the "Month" column is 10 (0-based index)
            int monthColumnIndex = 10;


            // Handle the CellFormatting event of the DataGridView
            amViewDataGrid.CellFormatting += (datagridSender, cellFormattingEventArgs) =>
            {
                // Check if the current cell is in the "Month" column
                if (cellFormattingEventArgs.ColumnIndex == monthColumnIndex && cellFormattingEventArgs.RowIndex >= 0)
                {
                    // Get the original value of the cell
                    string monthValue = cellFormattingEventArgs.Value?.ToString();

                    // Convert the month value to an integer
                    if (int.TryParse(monthValue, out int monthNumber))
                    {
                        // Check if the month number exists in the dictionary
                        if (monthNames.ContainsKey(monthNumber))
                        {
                            // Set the displayed value of the cell to the corresponding month name
                            cellFormattingEventArgs.Value = monthNames[monthNumber];
                            cellFormattingEventArgs.FormattingApplied = true;
                        }
                    }
                }
            };

            // Set the selected item of monthCb to the current month
            monthCb.SelectedIndex = DateTime.Now.Month - 1;
            isarchievedCb.SelectedIndex = 1;
            amViewDataGrid.DataSource = GetAMViewData();
            PopulateTotalColumns();
            FilterDataGridView();
            HandleColumnVisibility();
            


            // Find the "Isarchieved" column in the DataGridView
            DataGridViewColumn isarchievedColumn = amViewDataGrid.Columns["archieve"];

            // Set the Visible property to false
            isarchievedColumn.Visible = false;
            amViewDataGrid.Columns[3].HeaderText = "Resource Name";
            amViewDataGrid.Columns[5].HeaderText = "Project Name";
            amViewDataGrid.Columns[6].HeaderText = "Project Manager";
            amViewDataGrid.Columns[7].HeaderText = "Release Date";
            amViewDataGrid.Columns[10].HeaderText = "Feature Team";

        }

        private void amViewDataGrid_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            // Check if the added column is the "Isarchieved" column
            if (e.Column.Name == "Isarchieved")
            {
                // Set the Visible property to false
                e.Column.Visible = false;
            }
        }




        // Event handler for SelectedIndexChanged
        private void yearCb_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // Call the GetAMViewData() function here
            amViewDataGrid.DataSource = GetAMViewData();
            // Populate the "Total" and "Future Total" columns
            PopulateTotalColumns();


        }

        private DataTable GetAMViewData()
        {
            string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
            MySqlConnection conn = new MySqlConnection(connstring);
            DataTable dataTable = new DataTable();
            string selectedYear;
            if (string.IsNullOrEmpty(yearCb.Text))
            {
                selectedYear = DateTime.Now.Year.ToString();
                yearCb.Text = selectedYear;
            }
            else
            {
                selectedYear = yearCb.Text;
            }
            try
            {
                conn.Open();
                string sql = "SELECT DISTINCT allocationTable.SOEID, allocationTable.archieve, allocationTable.Name, allocationTable.PTSID, allocationTable.project_name, resource_master.resource_name AS project_manager, allocationTable.ReleaseDate, allocationTable.Goc, allocationTable.Location, allocationTable.Feature_Team, COALESCE(new_table.Jan, 0) AS Jan, COALESCE(new_table.Feb, 0) AS Feb, COALESCE(new_table.Mar, 0) AS Mar, COALESCE(new_table.Apr, 0) AS Apr, COALESCE(new_table.May, 0) AS May, COALESCE(new_table.Jun, 0) AS Jun, COALESCE(new_table.Jul, 0) AS Jul, COALESCE(new_table.Aug, 0) AS Aug, COALESCE(new_table.Sep, 0) AS Sep, COALESCE(new_table.Oct, 0) AS Oct, COALESCE(new_table.Nov, 0) AS Nov, COALESCE(new_table.`Dec`, 0) AS `Dec` FROM (SELECT rm.SOEID, rm.resource_name AS Name, pm.PTSID, pm.description AS project_name, pm.project_manager, pm.target_release_date AS ReleaseDate, rm.goc AS Goc, rm.location AS Location, rm.feature_team AS Feature_Team, am.archieve FROM resource_master rm JOIN allocation_master am ON rm.SOEID = am.SOEID JOIN project_master pm ON am.PTSID = pm.PTSID ORDER BY rm.resource_manager_name) AS allocationTable INNER JOIN (SELECT PTSID, SOEID, SUM(CASE WHEN month = 1 THEN `hr` END) AS Jan, SUM(CASE WHEN month = 2 THEN `hr` END) AS Feb, SUM(CASE WHEN month = 3 THEN `hr` END) AS Mar, SUM(CASE WHEN month = 4 THEN `hr` END) AS Apr, SUM(CASE WHEN month = 5 THEN `hr` END) AS May, SUM(CASE WHEN month = 6 THEN `hr` END) AS Jun, SUM(CASE WHEN month = 7 THEN `hr` END) AS Jul, SUM(CASE WHEN month = 8 THEN `hr` END) AS Aug, SUM(CASE WHEN month = 9 THEN `hr` END) AS Sep, SUM(CASE WHEN month = 10 THEN `hr` END) AS Oct, SUM(CASE WHEN month = 11 THEN `hr` END) AS Nov, SUM(CASE WHEN month = 12 THEN `hr` END) AS `Dec` FROM (SELECT allocation_master.month, allocation_master.year, allocation_master.PTSID, allocation_master.SOEID, SUM(allocation_master.allocation) AS hr FROM allocation_master INNER JOIN resource_master ON allocation_master.SOEID = resource_master.SOEID WHERE allocation_master.year = '" + selectedYear + "' GROUP BY allocation_master.month, allocation_master.year, allocation_master.PTSID, allocation_master.SOEID) AS new_table GROUP BY PTSID, SOEID) AS new_table ON allocationTable.PTSID = new_table.PTSID AND allocationTable.SOEID = new_table.SOEID INNER JOIN resource_master ON allocationTable.project_manager = resource_master.SOEID;\r\n";
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

        private void PopulateComboBoxes()
        {
            DataTable dataTable = GetAMViewData();

            // Populate ComboBox1 with unique values
            foreach (DataRow row in dataTable.Rows)
            {
                string value = row["SOEID"].ToString(); // Assuming the first column in the DataTable
                if (!soeidCb.Items.Contains(value))
                {
                    soeidCb.Items.Add(value);
                }
            }

            // Populate ComboBox2 with unique values
            foreach (DataRow row in dataTable.Rows)
            {
                string value = row["Name"].ToString(); // Assuming the second column in the DataTable
                if (!nameCb.Items.Contains(value))
                {
                    nameCb.Items.Add(value);
                }
            }

            // Populate ComboBox3 with unique values
            foreach (DataRow row in dataTable.Rows)
            {
                string value = row["PTSID"].ToString(); // Assuming the third column in the DataTable
                if (!ptsidCb.Items.Contains(value))
                {
                    ptsidCb.Items.Add(value);
                }
            }

            // Populate ComboBox4 with unique values
            foreach (DataRow row in dataTable.Rows)
            {
                string value = row["project_name"].ToString(); // Assuming the fourth column in the DataTable
                if (!pnCb.Items.Contains(value))
                {
                    pnCb.Items.Add(value);
                }
            }

            // Populate ComboBox5 with unique values
            foreach (DataRow row in dataTable.Rows)
            {
                string value = row["Feature_Team"].ToString(); // Assuming the ninth column in the DataTable
                if (!ftCb.Items.Contains(value))
                {
                    ftCb.Items.Add(value);
                }
            }

           


            // Populate the ComboBox with the month names
            monthCb.DataSource = new BindingSource(monthNames, null);
            monthCb.DisplayMember = "Value";
            monthCb.ValueMember = "Key";
            int currentMonth = DateTime.Now.Month;
            monthCb.SelectedValue = currentMonth.ToString();


            // Populate yearCb with selectable years
            var years = Enumerable.Range(DateTime.Now.Year - 1, 3); // Adjust the range of years as per your requirement
            foreach (int year in years)
            {
                yearCb.Items.Add(year);
            }
        }

        private void HandleColumnVisibility()
        {
            // Get the selected month value from monthCb
            int selectedMonth = monthCb.SelectedIndex+1;

            // Loop through the columns of the DataGridView
            foreach (DataGridViewColumn column in amViewDataGrid.Columns)
            {
                // Exclude the non-month columns from being hidden
                if (!IsMonthColumn(column.Name))
                {
                    column.Visible = true;
                }
                else
                {
                    // Get the month number from the column name
                    int columnMonth = GetMonthFromColumnName(column.Name);

                    // Check if the column month is before the selected month
                    if (columnMonth < selectedMonth)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
            }
        }

        private void monthCb_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            HandleColumnVisibility();

        }

        private void amViewDataGrid_CellBeginEdit_1(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Get the current cell
            DataGridViewCell cell = amViewDataGrid[e.ColumnIndex, e.RowIndex];

            // Check if the current cell belongs to the checkbox column
            if (cell.OwningColumn.Name != "Select")
            {
                // Cancel the editing for all columns except the checkbox column
                e.Cancel = true;
            }
        }


        // Helper method to check if the column is a month column
        private bool IsMonthColumn(string columnName)
        {
            return columnName.StartsWith("Jan") || columnName.StartsWith("Feb") || columnName.StartsWith("Mar") ||
                   columnName.StartsWith("Apr") || columnName.StartsWith("May") || columnName.StartsWith("Jun") ||
                   columnName.StartsWith("Jul") || columnName.StartsWith("Aug") || columnName.StartsWith("Sep") ||
                   columnName.StartsWith("Oct") || columnName.StartsWith("Nov") || columnName.StartsWith("Dec");
        }

        // Helper method to get the month number from the column name
        private int GetMonthFromColumnName(string columnName)
        {
            string[] monthNames = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            string monthPrefix = columnName.Substring(0, 3);
            return Array.IndexOf(monthNames, monthPrefix) + 1;
        }


        private void FilterDataGridView()
        {
            string columnName1 = "Name";
            string columnName2 = "PTSID";
            string columnName3 = "SOEID";
            string columnName4 = "project_name";
            string columnName5 = "Feature_Team";
            string columnName6 = "archieve";

            string filterQuery = "";

            // Check if ComboBox1 has a selected value
            if (nameCb.SelectedIndex != -1)
            {
                string selectedValue1 = nameCb.SelectedItem.ToString();
                filterQuery += $"{columnName1} = '{selectedValue1}'";
            }

            // Check if ComboBox2 has a selected value
            if (ptsidCb.SelectedIndex != -1)
            {
                string selectedValue2 = ptsidCb.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(filterQuery))
                    filterQuery += " AND ";
                filterQuery += $"{columnName2} = '{selectedValue2}'";
            }

            // Check if ComboBox3 has a selected value
            if (soeidCb.SelectedIndex != -1)
            {
                string selectedValue3 = soeidCb.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(filterQuery))
                    filterQuery += " AND ";
                filterQuery += $"{columnName3} = '{selectedValue3}'";
            }

            // Check if ComboBox4 has a selected value
            if (pnCb.SelectedIndex != -1)
            {
                string selectedValue4 = pnCb.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(filterQuery))
                    filterQuery += " AND ";
                filterQuery += $"{columnName4} = '{selectedValue4}'";
            }

            // Check if ComboBox4 has a selected value
            if (ftCb.SelectedIndex != -1)
            {
                string selectedValue4 = ftCb.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(filterQuery))
                    filterQuery += " AND ";
                filterQuery += $"{columnName5} = '{selectedValue4}'";
            }

            // Check if ComboBox6 has a selected value
            if (isarchievedCb.SelectedIndex != -1)
            {
                bool isarchieved = isarchievedCb.SelectedItem.ToString() == "True";
                if (!string.IsNullOrEmpty(filterQuery))
                    filterQuery += " AND ";
                filterQuery += $"{columnName6} = {Convert.ToInt32(isarchieved)}";
            }

            // Apply the filter to the DataTable
            DataTable filteredData = ((DataTable)amViewDataGrid.DataSource).Clone();
            DataRow[] filteredRows = ((DataTable)amViewDataGrid.DataSource).Select(filterQuery);
            foreach (DataRow row in filteredRows)
            {
                filteredData.ImportRow(row);
            }

            // Check if the totalRow exists in the filtered data
            DataRow totalRow = filteredData.Rows.Cast<DataRow>()
                .FirstOrDefault(row => row[9].ToString() == "Total: ");

            // If totalRow exists, remove it before displaying the filtered data
            if (totalRow != null)
            {
                filteredData.Rows.Remove(totalRow);
            }

            // Bind the filtered data to the DataGridView
            amViewDataGrid.DataSource = filteredData;
            // Calculate and display the total row for the filtered data
            calcTotal();
        }


        public void LoadData()
        {
            // Retrieve the data for the DataGridView
            DataTable dataTable = GetAMViewData();

            // Set the DataTable as the data source for the DataGridView
            amViewDataGrid.DataSource = dataTable;
            // Populate the "Total" and "Future Total" columns
            PopulateTotalColumns();
            FilterDataGridView();

        }




        private void SearchBtn_Click(object sender, EventArgs e)
        {
            amViewDataGrid.DataSource = GetAMViewData();
            PopulateTotalColumns();
            FilterDataGridView();
 
        }


        private void button2_Click(object sender, EventArgs e)
        {
            nameCb.SelectedIndex = -1;
            pnCb.SelectedIndex = -1;
            soeidCb.SelectedIndex = -1;
            ptsidCb.SelectedIndex = -1;
            ftCb.SelectedIndex = -1;
            isarchievedCb.SelectedIndex = 1;
            monthCb.SelectedIndex = DateTime.Now.Month - 1;
            yearCb.SelectedItem = DateTime.Now.Year;
            amViewDataGrid.DataSource = GetAMViewData();
            PopulateTotalColumns();
            FilterDataGridView();
            amViewDataGrid.Refresh();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Create a list to store the rows to be deleted
            List<DataGridViewRow> rowsToDelete = new List<DataGridViewRow>();

            // Iterate through the DataGridView rows
            foreach (DataGridViewRow row in amViewDataGrid.Rows)
            {
                // Retrieve the checkbox cell for each row
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["Select"] as DataGridViewCheckBoxCell;

                // Check if the checkbox in the current row is checked
                if (checkBoxCell != null && checkBoxCell.Value != null && (bool)checkBoxCell.Value)
                {
                    // Add the row to the list of rows to be deleted
                    rowsToDelete.Add(row);
                }
            }

            // Check if there are rows selected for deletion
            if (rowsToDelete.Count > 0)
            {
                // Confirm the deletion with the user
                DialogResult result = MessageBox.Show("Are you sure you want to delete the selected rows?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Delete the rows from the DataGridView and the database
                    foreach (DataGridViewRow rowToDelete in rowsToDelete)
                    {
                        // Retrieve the PTSID and SOEID from the selected row
                        string ptsid = rowToDelete.Cells["PTSID"].Value.ToString();
                        string soeid = rowToDelete.Cells["SOEID"].Value.ToString();

                        //InsertInAuditTable(soeid, ptsid);

                        // Delete the row from the database using PTSID and SOEID
                        DeleteRowFromDatabase(ptsid, soeid);

                        // Remove the row from the DataGridView
                        amViewDataGrid.Rows.Remove(rowToDelete);
                    }

                    // Refresh the DataGridView
                    
                    amViewDataGrid.Refresh();
                    

                }
                
            }
            else
            {
                MessageBox.Show("No rows selected for deletion.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            calcTotal();
        }

        private void DeleteRowFromDatabase(string ptsid, string soeid)
        {
            string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                try
                {
                    conn.Open();
                    string username = MainForm.instance.user.Text;
                    DateTime time = DateTime.Now;

                    string updateQuery = "UPDATE allocation_master SET modified_by = @modi, modified_datetime = @time WHERE PTSID = @PTSID AND SOEID = @SOEID";
                    using (MySqlCommand command = new MySqlCommand(updateQuery, conn))
                    {
                        // Add the parameters and their values
                        command.Parameters.AddWithValue("@PTSID", ptsid);
                        command.Parameters.AddWithValue("@SOEID", soeid);
                        command.Parameters.AddWithValue("@modi", username);
                        command.Parameters.AddWithValue("@time", time);

                        // Execute the delete query
                        command.ExecuteNonQuery();
                    }

                    // Create the SQL delete query with parameters
                    string deleteQuery = "DELETE FROM allocation_master WHERE PTSID = @PTSID AND SOEID = @SOEID";

                    // Create the MySqlCommand object
                    using (MySqlCommand command = new MySqlCommand(deleteQuery, conn))
                    {
                        // Add the parameters and their values
                        command.Parameters.AddWithValue("@PTSID", ptsid);
                        command.Parameters.AddWithValue("@SOEID", soeid);

                        // Execute the delete query
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void InsertInAuditTable(string soeId, string ptsId)
        {
            string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
            string auditDatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();

                    string sql = "SELECT * FROM allocation_master WHERE SOEID = @soeId AND PTSID = @ptsId";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@soeId", soeId);
                    cmd.Parameters.AddWithValue("@ptsId", ptsId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string month = reader["month"].ToString();
                            string year = reader["year"].ToString();
                            string allocation = reader["allocation"].ToString();
                            string modifiedBy = "Your Modified By Value"; // Set the modified by value here

                            sql = "INSERT INTO audit_allocation_master (PTSID, SOEID, month, year, allocation, modified_by, modified_datetime, audit_datetime) " +
                                  "VALUES (@ptsId, @soeId, @month, @year, @allocation, @modifiedBy, NOW(), @auditDatetime)";
                            cmd = new MySqlCommand(sql, conn);
                            cmd.Parameters.AddWithValue("@ptsId", ptsId);
                            cmd.Parameters.AddWithValue("@soeId", soeId);
                            cmd.Parameters.AddWithValue("@month", month);
                            cmd.Parameters.AddWithValue("@year", year);
                            cmd.Parameters.AddWithValue("@allocation", allocation);
                            cmd.Parameters.AddWithValue("@modifiedBy", modifiedBy);
                            cmd.Parameters.AddWithValue("@auditDatetime", auditDatetime);

                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }




        private void button4_Click(object sender, EventArgs e)
        {
            addNewAllocationForm newAllocationForm = new addNewAllocationForm(this);
            newAllocationForm.Show();

        }

        private void archieveButton_Click(object sender, EventArgs e)
        {
            DataTable dataSource = (DataTable)amViewDataGrid.DataSource;

            foreach (DataGridViewRow row in amViewDataGrid.Rows)
            {
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["Select"] as DataGridViewCheckBoxCell;

                if (checkBoxCell != null && checkBoxCell.Value != null && (bool)checkBoxCell.Value)
                {
                    string selectedPTSID = row.Cells["PTSID"].Value.ToString();
                    string selectedSOEID = row.Cells["SOEID"].Value.ToString();

                    DataRow[] matchingRows = dataSource.Select($"PTSID = '{selectedPTSID}' AND SOEID = '{selectedSOEID}'");

                    foreach (DataRow matchingRow in matchingRows)
                    {
                        matchingRow["archieve"] = true;
                    }

                    SaveChangesToDatabase(selectedPTSID, selectedSOEID, true);
                }
            }
            MessageBox.Show("Rows archived Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            FilterDataGridView();
        }




        private void button5_Click(object sender, EventArgs e)
        {
            DataTable dataSource = (DataTable)amViewDataGrid.DataSource;

            foreach (DataGridViewRow row in amViewDataGrid.Rows)
            {
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["Select"] as DataGridViewCheckBoxCell;

                if (checkBoxCell != null && checkBoxCell.Value != null && (bool)checkBoxCell.Value)
                {
                    string selectedPTSID = row.Cells["PTSID"].Value.ToString();
                    string selectedSOEID = row.Cells["SOEID"].Value.ToString();

                    DataRow[] matchingRows = dataSource.Select($"PTSID = '{selectedPTSID}' AND SOEID = '{selectedSOEID}'");

                    foreach (DataRow matchingRow in matchingRows)
                    {
                        matchingRow["Isarchieved"] = false;
                    }

                    SaveChangesToDatabase(selectedPTSID, selectedSOEID, false);
                }
            }
            MessageBox.Show("Rows Unarchived Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            FilterDataGridView();

        }


        // To save the archieve/unarchieve state in the database
        private void SaveChangesToDatabase(string selectedPTSID, string selectedSOEID, bool isarchieved)
        {
            string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
            MySqlConnection conn = new MySqlConnection(connstring);

            try
            {
                conn.Open();

                // Save changes to the database
                MySqlCommand cmd = new MySqlCommand("UPDATE allocation_master SET archieve = @Isarchieved WHERE PTSID = @PTSID AND SOEID = @SOEID", conn);

                // Set the parameter values
                cmd.Parameters.AddWithValue("@Isarchieved", isarchieved);
                cmd.Parameters.AddWithValue("@PTSID", selectedPTSID);
                cmd.Parameters.AddWithValue("@SOEID", selectedSOEID);

                // Execute the query
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void editAllocationBtn_Click(object sender, EventArgs e)
        {
            bool isRowSelected = false;
            DataGridViewRow selectedRow = null;

            // Loop through all rows in the DataGridView
            foreach (DataGridViewRow row in amViewDataGrid.Rows)
            {
                // Check if the checkbox is selected for the row
                DataGridViewCheckBoxCell checkboxCell = row.Cells["Select"] as DataGridViewCheckBoxCell;
                bool isChecked = Convert.ToBoolean(checkboxCell.Value);

                if (isChecked)
                {
                    // Check if another row with the checkbox selected has already been found
                    if (isRowSelected)
                    {
                        // Multiple rows with checkbox selected found, show message and return
                        MessageBox.Show("Please select only one row at a time.");
                        return;
                    }

                    isRowSelected = true;
                    selectedRow = row;
                }
            }

            if (isRowSelected)
            {
                // Get the necessary data from the selected row
                string ptsid = selectedRow.Cells["PTSID"].Value.ToString();
                string soeid = selectedRow.Cells["SOEID"].Value.ToString();
                string projectName = selectedRow.Cells["project_name"].Value.ToString();
                string projectManager = selectedRow.Cells["project_manager"].Value.ToString();
                string featureTeam = selectedRow.Cells["Feature_Team"].Value.ToString();
                string name = selectedRow.Cells["Name"].Value.ToString();

                // Open the edit allocation form
                editAllocationForm editForm = new editAllocationForm(ptsid, soeid, projectName, projectManager, featureTeam, name);
                editForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a row with the checkbox selected.");
            }
        }

        private decimal CalculateTotalAllocation(string soeid, string ptsid, DateTime targetReleaseDate, int endMonth)
        {
            decimal totalAllocation = 0;

            // Calculate the start and end year based on the target_release_date and current date
            int startYear = targetReleaseDate.Year;
            int endYear = DateTime.Now.Year;

            // Iterate over the years and months to calculate the total allocation
            for (int year = startYear; year <= endYear; year++)
            {
                int startMonth = (year == startYear) ? targetReleaseDate.Month : 1;
                int loopEndMonth = (year == endYear) ? endMonth : 12;

                // Replace this with your actual database query and calculation logic
                // Example query to retrieve allocation data for the specified SOEID, year, and month range
                string query = "SELECT allocation FROM allocation_master WHERE SOEID = @SOEID AND PTSID = @PTSID AND Year = @Year AND Month >= @StartMonth AND Month <= @EndMonth";
                using (MySqlConnection conn = new MySqlConnection("server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings"))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SOEID", soeid);
                        cmd.Parameters.AddWithValue("@PTSID", ptsid);
                        cmd.Parameters.AddWithValue("@Year", year);
                        cmd.Parameters.AddWithValue("@StartMonth", startMonth);
                        cmd.Parameters.AddWithValue("@EndMonth", loopEndMonth);
                        conn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                decimal allocation = reader.GetDecimal("allocation");
                                totalAllocation += allocation;
                            }
                        }
                    }
                }

                // Set the start month to January for subsequent years
                startMonth = 1;
            }

            return totalAllocation;
        }

        private void PopulateTotalColumns()
        {
            try
            {
                // Get the DataTable from the existing DataSource
                DataTable allocationTable = (DataTable)amViewDataGrid.DataSource;

                // Add "Total" and "Future Total" columns if they don't already exist
                if (!allocationTable.Columns.Contains("Total"))
                    allocationTable.Columns.Add("Total", typeof(decimal));
                if (!allocationTable.Columns.Contains("Future Total"))
                    allocationTable.Columns.Add("Future Total", typeof(decimal));
                if (!allocationTable.Columns.Contains("Cost"))
                    allocationTable.Columns.Add("Cost", typeof(decimal));

                // Iterate over each row and calculate the "Total", "Future Total", and "Cost" values
                foreach (DataRow row in allocationTable.Rows)
                {
                    string ptsid = row["PTSID"].ToString();
                    string soeid = row["SOEID"].ToString();
                    string goc = row["Goc"].ToString();
                    DateTime targetReleaseDate = GetTargetReleaseDateFromDatabase(ptsid);
                    decimal total = CalculateTotalAllocation(soeid, ptsid, targetReleaseDate, 12);
                    decimal futureTotal = CalculateTotalAllocation(soeid, ptsid, targetReleaseDate, DateTime.Now.Month);
                    decimal oneMonthCost = GetOneMonthCost(goc); // Fetch the one_month_cost_per_resource from goc_master table based on Goc
                    decimal cost = oneMonthCost * futureTotal;

                    row["Total"] = total;
                    row["Future Total"] = futureTotal;
                    row["Cost"] = Math.Round(cost, 2);
                }

                // Update the DataSource with the modified DataTable
                amViewDataGrid.DataSource = allocationTable;
                calcTotal();



                // Refresh the DataGridView to reflect the changes
                amViewDataGrid.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while populating the Total, Future Total, and Cost columns: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private DateTime GetTargetReleaseDateFromDatabase(string ptsid)
        {
            DateTime targetReleaseDate = DateTime.MinValue;
            try
            {
                string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();

                    string query = "SELECT target_release_date FROM project_master WHERE PTSID = @PTSID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@PTSID", ptsid);

                    object targetReleaseDateValue = cmd.ExecuteScalar();
                    if (targetReleaseDateValue != null && targetReleaseDateValue != DBNull.Value)
                    {
                        targetReleaseDate = Convert.ToDateTime(targetReleaseDateValue);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving the target release date from the database: " + ex, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return targetReleaseDate;
        }


        private decimal GetOneMonthCost(string goc)
        {
            decimal oneMonthCost = 0;

            try
            {
                string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();

                    string query = "SELECT one_month_cost_per_resource FROM gocmaster WHERE goc = @goc";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@goc", goc);

                    object oneMonthCostValue = cmd.ExecuteScalar();
                    if (oneMonthCostValue != null && oneMonthCostValue != DBNull.Value)
                    {
                        oneMonthCost = Convert.ToDecimal(oneMonthCostValue);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving the one_month_cost_per_resource from the database: " + ex, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return oneMonthCost;
        }

        private void amViewDataGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                // Check if it's the total row and the "Select" column
                if (amViewDataGrid.Rows[e.RowIndex].Cells[7].Value == DBNull.Value && e.ColumnIndex == amViewDataGrid.Columns["Select"].Index)
                {
                    // Fill the cell with the background color to hide its content
                    e.PaintBackground(e.CellBounds, true);

                    // Indicate that the cell is painted, so the default painting is not performed
                    e.Handled = true;
                }
            }
            
        }

        private void amViewDataGrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            // Check if it's the total row
            if (amViewDataGrid.Rows[e.RowIndex].Cells[7].Value == DBNull.Value)
            {
                // Set the background color of the row
                amViewDataGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                amViewDataGrid.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkBlue;

                // Set the font weight to bold
                amViewDataGrid.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(amViewDataGrid.DefaultCellStyle.Font, FontStyle.Bold);
            }
        }

        private void calcTotal()
        {
            // Get the DataTable from the existing DataSource
            DataTable allocationTable = (DataTable)amViewDataGrid.DataSource;

            // Filter out deleted rows
            DataRow[] undeletedRows = allocationTable.Select("", "", DataViewRowState.CurrentRows);

            // Check if the totalRow already exists
            DataRow totalRow = undeletedRows.FirstOrDefault(row => row[9].ToString() == "Total: ");

            // If totalRow exists, remove it before calculating the new total
            if (totalRow != null)
            {
                allocationTable.Rows.Remove(totalRow);
            }

            // Create a new row for the totals
            totalRow = allocationTable.NewRow();

            // Set the values for the new total row

            totalRow[0] = "";
            totalRow[1] = DBNull.Value; // Blank value
            totalRow[2] = ""; // Blank value
            totalRow[3] = ""; // Blank value
            totalRow[4] = ""; // Blank value
            totalRow[5] = ""; // Blank value
            totalRow[6] = DBNull.Value; // Blank value
            totalRow[7] = DBNull.Value; // Blank value
            totalRow[8] = ""; // Blank value
            totalRow[9] = "Total: "; // Set the value to "Total"

            // Loop through the month columns and calculate the sum for each month
            for (int month = 1; month <= 12; month++)
            {
                string monthColumnName = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month);
                object monthColumnSumObject = allocationTable.Compute($"SUM([{monthColumnName}])", "");
                decimal monthColumnSum = 0;

                if (monthColumnSumObject != DBNull.Value && monthColumnSumObject != null)
                {
                    if (decimal.TryParse(monthColumnSumObject.ToString(), out decimal result))
                    {
                        monthColumnSum = result;
                    }
                }

                totalRow[monthColumnName] = monthColumnSum;
            }

            // Calculate the sum for the "Total" column
            object totalColumnSumObject = allocationTable.Compute("SUM([Total])", "");
            decimal totalColumnSum = totalColumnSumObject is DBNull ? 0 : Convert.ToDecimal(totalColumnSumObject);
            totalRow["Total"] = totalColumnSum;

            // Calculate the sum for the "Future Total" column
            object futureTotalColumnSumObject = allocationTable.Compute("SUM([Future Total])", "");
            decimal futureTotalColumnSum = futureTotalColumnSumObject is DBNull ? 0 : Convert.ToDecimal(futureTotalColumnSumObject);
            totalRow["Future Total"] = futureTotalColumnSum;

            // Calculate the sum for the "Cost" column
            object costColumnSumObject = allocationTable.Compute("SUM([Cost])", "");
            decimal costColumnSum = costColumnSumObject is DBNull ? 0 : Convert.ToDecimal(costColumnSumObject);
            totalRow["Cost"] = costColumnSum;

            // Add the new total row to the DataTable
            allocationTable.Rows.Add(totalRow);

            // Refresh the DataGridView to reflect the changes
            amViewDataGrid.Refresh();
        }

        

        

    }


}
