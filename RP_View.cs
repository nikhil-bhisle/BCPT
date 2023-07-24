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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ConsolFromApp
{
    public partial class RP_View : UserControl
    {

        public RP_View()
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




        private void RP_View_Load(object sender, EventArgs e)
        {

            rpViewDataGrid.DataSource = GetRPViewData();
            rpViewDataGrid.Columns[0].HeaderText = "SOEID";
            rpViewDataGrid.Columns[1].HeaderText = "Resource Name";
            rpViewDataGrid.Columns[2].HeaderText = "Resource Manager";
            rpViewDataGrid.Columns[3].HeaderText = "GOC";
            rpViewDataGrid.Columns[4].HeaderText = "Location";
            rpViewDataGrid.Columns[5].HeaderText = "PTSID";
            rpViewDataGrid.Columns[6].HeaderText = "Project Manager";
            rpViewDataGrid.Columns[7].HeaderText = "Project End Date";
            rpViewDataGrid.Columns[8].HeaderText = "Project Name";

            PopulateComboBoxes();
            calcTotal();
            // Set the selected item of monthCb to the current month
            monthCb.SelectedIndex = DateTime.Now.Month - 1;




        }

        //This is a function to get the data from dataset and inserting it into the datagridview 
        private DataTable GetRPViewData()
        {
            string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
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
                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();

                    string sql = "SELECT \r\n    DISTINCT rm.SOEID, \r\n    rm.resource_name, \r\n    rm.resource_manager_name, \r\n    rm.goc, \r\n    rm.location, \r\n    pm.PTSID, \r\n    rm_pm.resource_name AS project_manager, \r\n    pm.project_end_date, \r\n    pm.description AS project_name, \r\n    COALESCE((SELECT SUM(allocation_master.allocation) FROM allocation_master WHERE allocation_master.SOEID = rm.SOEID AND allocation_master.PTSID = pm.PTSID AND allocation_master.month = 1 AND allocation_master.year = '" + selectedYear + "'), 0) AS Jan, \r\n    COALESCE((SELECT SUM(allocation_master.allocation) FROM allocation_master WHERE allocation_master.SOEID = rm.SOEID AND allocation_master.PTSID = pm.PTSID AND allocation_master.month = 2 AND allocation_master.year = '" + selectedYear + "'), 0) AS Feb, \r\n    COALESCE((SELECT SUM(allocation_master.allocation) FROM allocation_master WHERE allocation_master.SOEID = rm.SOEID AND allocation_master.PTSID = pm.PTSID AND allocation_master.month = 3 AND allocation_master.year = '" + selectedYear + "'), 0) AS Mar, \r\n    COALESCE((SELECT SUM(allocation_master.allocation) FROM allocation_master WHERE allocation_master.SOEID = rm.SOEID AND allocation_master.PTSID = pm.PTSID AND allocation_master.month = 4 AND allocation_master.year = '" + selectedYear + "'), 0) AS Apr, \r\n    COALESCE((SELECT SUM(allocation_master.allocation) FROM allocation_master WHERE allocation_master.SOEID = rm.SOEID AND allocation_master.PTSID = pm.PTSID AND allocation_master.month = 5 AND allocation_master.year = '" + selectedYear + "'), 0) AS May, \r\n    COALESCE((SELECT SUM(allocation_master.allocation) FROM allocation_master WHERE allocation_master.SOEID = rm.SOEID AND allocation_master.PTSID = pm.PTSID AND allocation_master.month = 6 AND allocation_master.year = '" + selectedYear + "'), 0) AS Jun, \r\n    COALESCE((SELECT SUM(allocation_master.allocation) FROM allocation_master WHERE allocation_master.SOEID = rm.SOEID AND allocation_master.PTSID = pm.PTSID AND allocation_master.month = 7 AND allocation_master.year = '" + selectedYear + "'), 0) AS Jul, \r\n    COALESCE((SELECT SUM(allocation_master.allocation) FROM allocation_master WHERE allocation_master.SOEID = rm.SOEID AND allocation_master.PTSID = pm.PTSID AND allocation_master.month = 8 AND allocation_master.year = '" + selectedYear + "'), 0) AS Aug, \r\n    COALESCE((SELECT SUM(allocation_master.allocation) FROM allocation_master WHERE allocation_master.SOEID = rm.SOEID AND allocation_master.PTSID = pm.PTSID AND allocation_master.month = 9 AND allocation_master.year = '" + selectedYear + "'), 0) AS Sep, \r\n    COALESCE((SELECT SUM(allocation_master.allocation) FROM allocation_master WHERE allocation_master.SOEID = rm.SOEID AND allocation_master.PTSID = pm.PTSID AND allocation_master.month = 10 AND allocation_master.year = '" + selectedYear + "'), 0) AS Oct, \r\n    COALESCE((SELECT SUM(allocation_master.allocation) FROM allocation_master WHERE allocation_master.SOEID = rm.SOEID AND allocation_master.PTSID = pm.PTSID AND allocation_master.month = 11 AND allocation_master.year = '" + selectedYear + "'), 0) AS Nov, \r\n    COALESCE((SELECT SUM(allocation_master.allocation) FROM allocation_master WHERE allocation_master.SOEID = rm.SOEID AND allocation_master.PTSID = pm.PTSID AND allocation_master.month = 12 AND allocation_master.year = '" + selectedYear + "'), 0) AS `Dec` \r\nFROM \r\n    resource_master rm \r\nJOIN \r\n    allocation_master am ON rm.SOEID = am.SOEID \r\nJOIN \r\n    project_master pm ON am.PTSID = pm.PTSID \r\nJOIN \r\n    resource_master rm_pm ON pm.project_manager = rm_pm.SOEID \r\nORDER BY \r\n    rm.resource_manager_name\r\n";


                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
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
            yearCb.SelectedItem = DateTime.Now.Year.ToString();
            
            DataTable dataTable = GetRPViewData();

            // Populate ComboBox1
            foreach (DataRow row in dataTable.Rows)
            {
                string value = row["SOEID"].ToString(); // Assuming the first column in the DataTable
                if (!soeidCb.Items.Contains(value))
                {
                    soeidCb.Items.Add(value);
                }
            }

            // Populate ComboBox2
            foreach (DataRow row in dataTable.Rows)
            {
                string value = row["resource_name"].ToString(); // Assuming the second column in the DataTable
                if (!rnCb.Items.Contains(value))
                {
                    rnCb.Items.Add(value);
                }
            }

            // Populate ComboBox3
            foreach (DataRow row in dataTable.Rows)
            {
                string value = row["resource_manager_name"].ToString(); // Assuming the third column in the DataTable
                if (!rmCb.Items.Contains(value))
                {
                    rmCb.Items.Add(value);
                }
            }

            // Populate ComboBox4
            foreach (DataRow row in dataTable.Rows)
            {
                string value = row["goc"].ToString(); // Assuming the third column in the DataTable
                if (!gocCb.Items.Contains(value))
                {
                    gocCb.Items.Add(value);
                }
            }

            
        }

        // Event handler for the ComboBox's selection change event
        private void yearCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            rpViewDataGrid.DataSource = GetRPViewData();
            calcTotal();
            
            
           
        }

        private void monthCb_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            HandleColumnVisibility();
        }

        private void HandleColumnVisibility()
        {
            // Get the selected month value from monthCb
            int selectedMonth = monthCb.SelectedIndex + 1;

            // Loop through the columns of the DataGridView
            foreach (DataGridViewColumn column in rpViewDataGrid.Columns)
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
            string columnName1 = "resource_manager_name"; // Replace with the actual column name
            string columnName2 = "resource_name"; // Replace with the actual column name
            string columnName3 = "SOEID"; // Replace with the actual column name
            string columnName4 = "goc"; // Replace with the actual column name

            string filterQuery = "";

            // Check if ComboBox1 has a selected value
            if (rmCb.SelectedIndex != -1)
            {
                string selectedValue1 = rmCb.SelectedItem.ToString();
                filterQuery += $"{columnName1} = '{selectedValue1}'";
            }

            // Check if ComboBox2 has a selected value
            if (rnCb.SelectedIndex != -1)
            {
                string selectedValue2 = rnCb.SelectedItem.ToString();
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
            if (gocCb.SelectedIndex != -1)
            {
                string selectedValue4 = gocCb.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(filterQuery))
                    filterQuery += " AND ";
                filterQuery += $"{columnName4} = '{selectedValue4}'";
            }

            // Apply the filter to the DataTable
            DataTable filteredData = ((DataTable)rpViewDataGrid.DataSource).Clone();
            DataRow[] filteredRows = ((DataTable)rpViewDataGrid.DataSource).Select(filterQuery);
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
            rpViewDataGrid.DataSource = filteredData;

            // Calculate and display the total row for the filtered data
            calcTotal();

        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            rpViewDataGrid.DataSource = GetRPViewData();
            FilterDataGridView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rmCb.SelectedIndex = -1 ;
            rnCb.SelectedIndex = -1;
            soeidCb.SelectedIndex = -1;
            gocCb.SelectedIndex = -1;
            monthCb.SelectedIndex = DateTime.Now.Month - 1;
            yearCb.SelectedItem = DateTime.Now.Year;
            rpViewDataGrid.DataSource = GetRPViewData();
            calcTotal();
            rpViewDataGrid.Refresh();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void calcTotal()
        {
            // Get the DataTable from the existing DataSource
            DataTable allocationTable = (DataTable)rpViewDataGrid.DataSource;

            // Check if the totalRow already exists
            DataRow totalRow = allocationTable.Rows.Cast<DataRow>()
                .FirstOrDefault(row => row[9].ToString() == "Total: ");

            // If totalRow exists, remove it before calculating the new total
            if (totalRow != null)
            {
                allocationTable.Rows.Remove(totalRow);
            }

            // Create a new row for the totals
            totalRow = allocationTable.NewRow();

            // Set the values for the new total row

            totalRow[0] = " "; 
            totalRow[1] = DBNull.Value; // Blank value
            totalRow[2] = DBNull.Value;  // Blank value
            totalRow[3] = DBNull.Value;  // Blank value
            totalRow[4] = DBNull.Value;  // Blank value
            totalRow[5] = " ";  // Blank value
            totalRow[6] = DBNull.Value; // Blank value
            totalRow[7] = DBNull.Value; // Blank value          
            totalRow[8] = "Total: "; // Set the value to "Total"

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

          

            // Add the new total row to the DataTable
            allocationTable.Rows.Add(totalRow);

            // Refresh the DataGridView to reflect the changes
            rpViewDataGrid.Refresh();
        }

        private void rpViewDataGrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            // Check if it's the total row
            if (rpViewDataGrid.Rows[e.RowIndex].Cells[7].Value == DBNull.Value)
            {
                // Set the background color of the row
                rpViewDataGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                rpViewDataGrid.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkBlue;

                // Set the font weight to bold
                rpViewDataGrid.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(rpViewDataGrid.DefaultCellStyle.Font, FontStyle.Bold);
            }
        }
    }
}
