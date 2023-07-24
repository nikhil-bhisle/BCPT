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
using System.Xml.Linq;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace BCPT
{
    public partial class editAllocationForm : Form
    {
        
        public editAllocationForm(string ptsid, string soeid, string projectName, string projectManager, string featureTeam, string name)
        {
            InitializeComponent();
            // Set the text box values with the provided data
            ptsidTextBox.Text = ptsid;
            soeidTextBox.Text = soeid;
            projectNameTextBox.Text = projectName;
            projectManagerTextBox.Text = projectManager;
            featureTeamTextBox.Text = featureTeam;
            nameTextBox.Text = name;
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


        private string fetchedAllocation;



        private void PopulateComboBoxes()
        {


            // Populate the ComboBox with the month names
            monthCb.DataSource = new BindingSource(monthNames, null);
            monthCb.DisplayMember = "Value";
            monthCb.ValueMember = "Key";

            // Populate yearCb with selectable years
            var years = Enumerable.Range(DateTime.Now.Year - 1, 3); // Adjust the range of years as per your requirement
            foreach (int year in years)
            {
                yearCb.Items.Add(year);
            }




        }

        private void lookupBtn_Click_1(object sender, EventArgs e)
        {
            // Check if the monthComboBox has no selected value
            if (monthCb.SelectedItem == null)
            {
                MessageBox.Show("Please select a valid month.");
                return; // Exit the method
            }

            // Check if the yearComboBox is not selected or has no text
            if (string.IsNullOrEmpty(yearCb.Text))
            {
                MessageBox.Show("Please select or enter a valid year.");
                return; // Exit the method
            }

            // Get the month and year values
            int month;
            int year;

            // Check if the monthComboBox has a valid selected value
            if (!int.TryParse(monthCb.SelectedValue.ToString(), out int m) || m < 1 || m > 12)
            {
                MessageBox.Show("Invalid month value. Please select a valid month (1-12).");
                return; // Exit the method
            }


            // Check if the year value is a valid integer
            if (!int.TryParse(yearCb.Text, out year) || year < 2013 || year > 2032)
            {
                MessageBox.Show("Invalid year value. Please enter a valid year (2013-2032).");
                return; // Exit the method
            }

            // Get the SOEID and PTSID values
            string soeid = soeidTextBox.Text;
            string ptsid = ptsidTextBox.Text;

            try
            {
                // Retrieve the allocation from the allocation_master table
                string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();

                    // Query to fetch the allocation
                    string sql = "SELECT allocation FROM allocation_master WHERE month = @Month AND year = @Year AND SOEID = @SOEID AND PTSID = @PTSID";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Month", m);
                    cmd.Parameters.AddWithValue("@Year", year);
                    cmd.Parameters.AddWithValue("@SOEID", soeid);
                    cmd.Parameters.AddWithValue("@PTSID", ptsid);

                    object allocationValue = cmd.ExecuteScalar();
                    decimal allocation = allocationValue != null && allocationValue != DBNull.Value ? Convert.ToDecimal(allocationValue) : 0;

                    // Set the allocation value in the allocationTextBox
                    fetchedAllocation = allocation.ToString();
                    if(fetchedAllocation == "0")
                    {
                        MessageBox.Show("There is no allocation to edit for this data, Please add a new allocation for this!");
                        this.Hide();
                       
                    }
                    allocationTextBox.Text = allocation.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving the allocation: " + ex.Message);
            }
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void editAllocationForm_Load(object sender, EventArgs e)
        {
            PopulateComboBoxes();
        }

        private void UpdateAllocationData(string ptsid, string soeid, int month, int year, decimal allocation, string modified_by, DateTime modified_datetime)
        {
            try
            {
                string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();
                    decimal allocationForSOEID = GetAllocationForSOEID(soeid, month, year);
                    // Retrieve the current allocation value
                    decimal currentallocation;
                    if (!decimal.TryParse(fetchedAllocation, out currentallocation))
                    {
                        MessageBox.Show("Please enter a valid allocation value.");
                        return;
                    }



                    decimal totalAllocation = allocationForSOEID + allocation - currentallocation ;

                    if (totalAllocation > 1)
                    {
                        MessageBox.Show("The allocation for the selected resource exceeds the maximum limit of 1 for the given month. Free capacity for the SOEID: " + (1 - allocationForSOEID));
                       
                    }

                   

                    // Update the allocation entry in the Allocation master table
                    string sql = "UPDATE allocation_master SET allocation = @Allocation, modified_by = @ModifiedBy, modified_datetime = @ModifiedDateTime WHERE PTSID = @PTSID AND SOEID = @SOEID AND Month = @Month AND Year = @Year";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@PTSID", ptsid);
                    cmd.Parameters.AddWithValue("@SOEID", soeid);
                    cmd.Parameters.AddWithValue("@Month", month);
                    cmd.Parameters.AddWithValue("@Year", year);
                    cmd.Parameters.AddWithValue("@Allocation", allocation);
                    cmd.Parameters.AddWithValue("@ModifiedBy", modified_by);
                    cmd.Parameters.AddWithValue("@ModifiedDateTime", modified_datetime);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Show success message
                        MessageBox.Show("Allocation updated successfully.", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Failed to update data
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating the data: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
                MainForm mainForm = Application.OpenForms["MainForm"] as MainForm;
                

                // Retrieve the selected PTSID and SOEID from the TextBoxes
                string selectedPTSID = ptsidTextBox.Text;
                string selectedSOEID = soeidTextBox.Text;

                // Retrieve the selected month and year from the ComboBoxes
                if (monthCb.SelectedItem == null || yearCb.SelectedItem == null)
                {
                    MessageBox.Show("Please select a valid month and year.");
                    return;
                }
                int selectedMonth = monthCb.SelectedIndex + 1;
                int selectedYear = Convert.ToInt32(yearCb.SelectedItem);

                // Retrieve the current allocation value
                decimal currentallocation;
                if (!decimal.TryParse(fetchedAllocation, out currentallocation))
                {
                    MessageBox.Show("Please enter a valid allocation value.");
                    return;
                }

               

                decimal editedallocation;
                

                // Try parsing the allocation input as a decimal
                if (decimal.TryParse(allocationTextBox.Text, out decimal allocationValue))
                {
                    editedallocation = allocationValue;
                    if (!decimal.TryParse(allocationTextBox.Text, out editedallocation))
                    {
                        MessageBox.Show("Invalid allocation value. Please enter a valid decimal number.");
                        return;
                    }
                    // Validate the allocation value
                    if (editedallocation < 0 || editedallocation > 1)
                    {
                        MessageBox.Show("Allocation must be between 0 and 1.");
                        return;
                    }

                    // Check if the allocation for the SOEID exceeds 1 for the given month
                    decimal allocationForSOEID = (GetAllocationForSOEID(selectedSOEID, selectedMonth, selectedYear) - currentallocation);
                    if (allocationForSOEID + editedallocation > 1)
                    {
                        decimal remainingCapacityForSOEID = 1 - GetAllocationForSOEID(selectedSOEID, selectedMonth, selectedYear);
                        MessageBox.Show($"The allocation for the selected resource exceeds the maximum limit of 1 for the given month. fffffFree capacity for the SOEID: {remainingCapacityForSOEID}");
                        return;
                    }



                    // Update the allocation entry in the allocation_master table
                    string modified_by = mainForm.user.Text;
                    DateTime modified_datetime = DateTime.Now;

                    UpdateAllocationData(selectedPTSID, selectedSOEID, selectedMonth, selectedYear, editedallocation, modified_by, modified_datetime);



                    // Refresh the DataGridView in the allocation master screen
                    mainForm?.RefreshAllocationMaster();

                    // Close the Edit Allocation form
                    this.Close();
                }
                else
                {
                    
                    MessageBox.Show("Please enter a valid allocation number.", "Invalid Allocation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
            }


        private decimal GetAllocationForSOEID(string soeid, int month, int year)
        {
            decimal allocationForSOEID = 0;

            try
            {
                string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();

                    string query = "SELECT SUM(allocation) FROM allocation_master WHERE SOEID = @SOEID AND Month = @Month AND Year = @Year";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SOEID", soeid);
                    cmd.Parameters.AddWithValue("@Month", month);
                    cmd.Parameters.AddWithValue("@Year", year);

                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        allocationForSOEID = Convert.ToDecimal(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving the allocation for the SOEID: " + ex.Message);
            }

            return allocationForSOEID;
        }

        


    }
}
