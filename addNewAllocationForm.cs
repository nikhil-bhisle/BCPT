using ConsolFromApp;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCPT
{
    public partial class addNewAllocationForm : Form
    {
        private Allocation_Master allocationMaster;
        public addNewAllocationForm(Allocation_Master allocationMaster)
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

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

       

        private DataTable GetProjectData()
        {
            DataTable dataTable = new DataTable();

            try
            {
                string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
                MySqlConnection conn = new MySqlConnection(connstring);

                conn.Open();

                string sql = "SELECT pm.PTSID, pm.description, pm.project_type, pm.program_id, resource_master.resource_name AS project_manager, pm.piac_category, pm.target_release_date, pm.project_end_date, pm.country, pm.product, pm.status, pm.secore_l0, pm.dotnet_l0, pm.secore_l1, pm.dotnet_l1, pm.modified_by, pm.modified_datetime\r\nFROM project_master pm\r\nJOIN resource_master ON pm.project_manager = resource_master.SOEID;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dataTable.Load(reader);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dataTable;
        }

        private DataTable GetResourceData()
        {
            DataTable dataTable = new DataTable();

            try
            {
                string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
                MySqlConnection conn = new MySqlConnection(connstring);

                conn.Open();

                string sql = "SELECT * FROM resource_master";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dataTable.Load(reader);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dataTable;
        }


        private void PopulateComboBoxes()
        {
            DataTable projectDataTable = GetProjectData();
            DataTable resourceDataTable = GetResourceData();

            // Populate PTSID ComboBox
            foreach (DataRow row in projectDataTable.Rows)
            {
                string ptsidValue = row["PTSID"].ToString(); // Assuming the PTSID column name in the DataTable
                ptsidCb.Items.Add(ptsidValue);
            }

            // Populate SOEID ComboBox
            foreach (DataRow row in resourceDataTable.Rows)
            {
                string soeidValue = row["SOEID"].ToString(); // Assuming the SOEID column name in the DataTable
                soeidCb.Items.Add(soeidValue);
            }


            // Populate the ComboBox with months from the dictionary
            foreach (var month in monthNames)
            {
                // Check if the month number is greater than or equal to the current month
                if (month.Key >= DateTime.Now.Month)
                {
                    // Add the month name to the ComboBox
                    monthCb.Items.Add(month.Value);
                }
            }


            // Populate yearCb with selectable years
            var years = Enumerable.Range(DateTime.Now.Year - 0, 2); // Adjust the range of years as per your requirement
            foreach (int year in years)
            {
                yearCb.Items.Add(year);
            }




        }

        private void addNewAllocationForm_Load(object sender, EventArgs e)
        {
            PopulateComboBoxes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if the PTSID ComboBox is not selected or has no text
            if (string.IsNullOrEmpty(ptsidCb.Text))
            {
                MessageBox.Show("Please select or enter a valid PTSID.");
                return; // Exit the method
            }

            // Check if the SOEID ComboBox is not selected or has no text
            if (string.IsNullOrEmpty(soeidCb.Text))
            {
                MessageBox.Show("Please select or enter a valid SOEID.");
                return; // Exit the method
            }

            // Retrieve the selected PTSID and SOEID from the ComboBoxes
            string selectedPTSID = ptsidCb.Text;
            string selectedSOEID = soeidCb.Text;

            // Check if the selected PTSID exists in the available options
            bool isValidPTSID = ptsidCb.Items.Contains(selectedPTSID);
            if (!isValidPTSID)
            {
                MessageBox.Show("Selected PTSID is not valid.");
                return; // Exit the method
            }

            // Check if the selected SOEID exists in the available options
            bool isValidSOEID = soeidCb.Items.Contains(selectedSOEID);
            if (!isValidSOEID)
            {
                MessageBox.Show("Selected SOEID is not valid.");
                return; // Exit the method
            }

            // Retrieve the data from the resource_master table based on the selected SOEID
            DataTable resourceDataTable = GetResourceData();
            DataRow[] resourceRows = resourceDataTable.Select($"SOEID = '{selectedSOEID}'");

            // Check if any matching row is found
            if (resourceRows.Length > 0)
            {
                // Retrieve the first matching row
                DataRow resourceRow = resourceRows[0];

                // Populate the corresponding fields with the retrieved data
                nameTextBox.Text = resourceRow[1].ToString();
                gocTextBox.Text = resourceRow[3].ToString();
                locationTextBox.Text = resourceRow[6].ToString();
                featureTeamTextBox.Text = resourceRow[7].ToString();
            }

            // Retrieve the data from the project_master table based on the selected PTSID
            DataTable projectDataTable = GetProjectData();
            DataRow[] projectRows = projectDataTable.Select($"PTSID = '{selectedPTSID}'");

            // Check if any matching row is found
            if (projectRows.Length > 0)
            {
                // Retrieve the first matching row
                DataRow projectRow = projectRows[0];

                // Populate the corresponding fields with the retrieved data
                projectNameTextBox.Text = projectRow[1].ToString();
                projectManagerTextBox.Text = projectRow[4].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            MainForm mainForm = Application.OpenForms["MainForm"] as MainForm;
            // Retrieve the selected PTSID and SOEID from the ComboBoxes
            string selectedPTSID = ptsidCb.Text;
            string selectedSOEID = soeidCb.Text;

            // Retrieve the selected month name from the ComboBox
            string selectedMonthName = monthCb.SelectedItem.ToString();

            // Get the corresponding month number from the dictionary
            int selectedMonth = monthNames.FirstOrDefault(x => x.Value == selectedMonthName).Key;
            int selectedYear = Convert.ToInt32(yearCb.SelectedItem);
                string modified_by = mainForm.user.Text;
                DateTime modified_datetime = DateTime.Now;


                // Retrieve the allocation value
                decimal allocation = 100000;
    
                // Try parsing the allocation input as a decimal
                if (decimal.TryParse(allocationTextBox.Text, out decimal allocationValue))
                {
                    // Valid allocation value, continue with saving the allocation
                    allocation = allocationValue;
                    // Validate the allocation value
                    if (allocation >= 1)
                    {
                        MessageBox.Show("Allocation must be less than or equal to 1.");

                    }

                    // Truncate allocation value to two decimal places
                    allocation = Math.Truncate(allocation * 100) / 100;

                    // Insert the data into the Allocation master table
                    InsertAllocationData(selectedPTSID, selectedSOEID, selectedMonth, selectedYear, allocation, modified_by, modified_datetime);
                    mainForm?.RefreshAllocationMaster();
                    
            }
                else
                {
                    // Invalid allocation value, display an error message
                    MessageBox.Show("Please enter a valid allocation number.", "Invalid Allocation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

        }

        private void InsertAllocationData(string ptsid, string soeid, int month, int year, decimal allocation, string modified_by, DateTime modified_datetime)
        {
            try
            {
                string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();

                    // Check if the allocation month and year are beyond the project end date
                    string checkProjectEndDateQuery = "SELECT project_end_date FROM project_master WHERE PTSID = @PTSID";
                    MySqlCommand checkProjectEndDateCmd = new MySqlCommand(checkProjectEndDateQuery, conn);
                    checkProjectEndDateCmd.Parameters.AddWithValue("@PTSID", ptsid);
                    object projectEndDateValue = checkProjectEndDateCmd.ExecuteScalar();

                    if (projectEndDateValue != null && projectEndDateValue != DBNull.Value)
                    {
                        DateTime projectEndDate = Convert.ToDateTime(projectEndDateValue);
                        DateTime allocationDate = new DateTime(year, month, 1); // Assuming day is always 1 for the allocation date

                        if (allocationDate > projectEndDate)
                        {
                            MessageBox.Show("The allocation month and year are beyond the project end date.\r\n Project End Date for is - " + projectEndDate.ToString() , "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Check if the allocation for the SOEID exceeds 1 for the given month
                    string checkAllocationForSOEIDQuery = "SELECT SUM(allocation) FROM allocation_master WHERE SOEID = @SOEID AND Month = @Month AND Year = @Year";
                    MySqlCommand checkAllocationForSOEIDCmd = new MySqlCommand(checkAllocationForSOEIDQuery, conn);
                    checkAllocationForSOEIDCmd.Parameters.AddWithValue("@SOEID", soeid);
                    checkAllocationForSOEIDCmd.Parameters.AddWithValue("@Month", month);
                    checkAllocationForSOEIDCmd.Parameters.AddWithValue("@Year", year);
                    object allocationForSOEIDValue = checkAllocationForSOEIDCmd.ExecuteScalar();

                    decimal allocationForSOEID = 0;
                    if (allocationForSOEIDValue != null && allocationForSOEIDValue != DBNull.Value)
                    {
                        allocationForSOEID = Convert.ToDecimal(allocationForSOEIDValue);
                    }
                
                    decimal totalAllocation = allocationForSOEID + allocation;

                    if (totalAllocation > 1)
                    {
                        MessageBox.Show("The allocation for the selected resource exceeds the maximum limit of 1 for the given month. Free capacity for the SOEID: " + (1 - allocationForSOEID), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Check if an existing row with the same PTSID and SOEID exists
                    string checkExistingRowQuery = "SELECT archieve FROM allocation_master WHERE PTSID = @PTSID AND SOEID = @SOEID LIMIT 1";
                    MySqlCommand checkExistingRowCmd = new MySqlCommand(checkExistingRowQuery, conn);
                    checkExistingRowCmd.Parameters.AddWithValue("@PTSID", ptsid);
                    checkExistingRowCmd.Parameters.AddWithValue("@SOEID", soeid);
                    object isarchievedValue = checkExistingRowCmd.ExecuteScalar();

                    bool isarchieved = false;
                    if (isarchievedValue != null && isarchievedValue != DBNull.Value)
                    {
                        // If an existing row is found, get the Isarchieved value
                        isarchieved = Convert.ToBoolean(isarchievedValue);
                    }

                    // Insert the new allocation entry with the Isarchieved value
                    string sql = "INSERT INTO allocation_master (PTSID, SOEID, Month, Year, allocation, modified_by, modified_datetime, archieve) VALUES (@PTSID, @SOEID, @Month, @Year, @Allocation, @ModifiedBy, @ModifiedDateTime, @Isarchieved)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@PTSID", ptsid);
                    cmd.Parameters.AddWithValue("@SOEID", soeid);
                    cmd.Parameters.AddWithValue("@Month", month);
                    cmd.Parameters.AddWithValue("@Year", year);
                    cmd.Parameters.AddWithValue("@Allocation", allocation);
                    cmd.Parameters.AddWithValue("@ModifiedBy", modified_by);
                    cmd.Parameters.AddWithValue("@ModifiedDateTime", modified_datetime);
                    cmd.Parameters.AddWithValue("@Isarchieved", isarchieved);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Allocation added successfully.", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Hide the form
                        this.Hide();
                    }
                    else
                    {
                        // Failed to insert data
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while inserting the data: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
