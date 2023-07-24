using MySql.Data.MySqlClient;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;

namespace ConsolFromApp
{
    public partial class avaiCapView : UserControl


    {


        MySqlConnection connection = new MySqlConnection("server=localhost;uid=root;pwd=Y1012Jqkhkp" +
            ";database=learnings");
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataTable table;
       
        int start_month=1;
        int end_month=12;

        int year;
        int end_year;
        int currentYear;
        int currentMonth;
        //string s_year;
        // string e_year;

        public avaiCapView()
        {
            InitializeComponent();
            DateTime currentDate = DateTime.Now;
            currentYear = currentDate.Year;
            currentMonth = currentDate.Month;
            start_month = currentMonth;
            end_month = start_month + 3;
            year = currentYear;
            if (start_month + 3 <= 12)
            {
                end_year = year;
            }
            else
            {
                end_year = year + 1;
            }
           
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;



        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;

            // Specify the row and column index of the cell you want to make bold
            int targetRowIndex = dataGridView1.RowCount-1; // Example: 0 for the first row
            int targetColumnIndex = 5; // Example: 2 for the third column

            // Check if it's the target cell
            if (e.RowIndex == targetRowIndex && e.ColumnIndex == targetColumnIndex)
            {
                // Apply bold font style to the cell
                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
            }
        }

        private void avaiCapView1_Load(object sender, EventArgs e)
        {


        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void filterName(string res_name)
        {
            string query = "SELECT a.PTSID, a.SOEID, r.resource_name as 'Resource Name', r.application as 'Application', r.feature_team as 'Feature Team', p.project_type as 'Project Type',";

            for (int y = year; y <= year; y++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    string monthYear = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)}_{y}";
                    string caseStatement = $"SUM(CASE WHEN a.month = {month} AND a.year = {y} THEN 1 - a.allocation ELSE 0 END) AS {monthYear},";
                    query += caseStatement;
                }
            }

            string futureTotalStatement = "SUM(CASE WHEN a.month > @startMonth AND a.month <= @endMonth THEN 1 - a.allocation ELSE 0 END) AS 'Sum Of Future Total'";
            query += futureTotalStatement;

            // Remove the trailing comma from the query
            query = query.TrimEnd(',');

            query += "\r\nFROM allocation_master a ";
            query += "\r\nJOIN resource_master r ON a.SOEID = r.SOEID ";
            query += "\r\nJOIN project_master p ON a.PTSID = p.PTSID ";
            query += $"\r\nWHERE r.resource_name LIKE '%{res_name}%' "; // Apply the resource name filter
            query += "\r\nGROUP BY a.PTSID, a.SOEID, r.resource_name, r.application, r.feature_team, p.ptsid, p.project_type;";

            command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@startMonth", start_month);
            command.Parameters.AddWithValue("@endMonth", 12);
            adapter = new MySqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;

            table.Rows.Add();
            calcTotal();



        }
        private void button2_Click(object sender, EventArgs e)
        {


        }

        public void filterAppln(string appln)
        {
             // Replace with the desired application name

            string query = "SELECT a.PTSID, a.SOEID, r.resource_name as 'Resource Name', r.application as 'Application', r.feature_team as 'Feature Team', p.project_type as 'Project Type',";

            for (int y = year; y <= year; y++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    string monthYear = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)}_{y}";
                    string caseStatement = $"SUM(CASE WHEN a.month = {month} AND a.year = {y} THEN 1 - a.allocation ELSE 0 END) AS {monthYear},";
                    query += caseStatement;
                }
            }

            string futureTotalStatement = "SUM(CASE WHEN a.month > @startMonth AND a.month <= @endMonth THEN 1 - a.allocation ELSE 0 END) AS 'Sum Of Future Total'";
            query += futureTotalStatement;

            // Remove the trailing comma from the query
            query = query.TrimEnd(',');

            query += "\r\nFROM allocation_master a ";
            query += "\r\nJOIN resource_master r ON a.SOEID = r.SOEID ";
            query += "\r\nJOIN project_master p ON a.PTSID = p.PTSID ";
            query += $"\r\nWHERE r.application LIKE '%{appln}%' "; // Apply the application filter
            query += "\r\nGROUP BY a.PTSID, a.SOEID, r.resource_name, r.application, r.feature_team, p.ptsid, p.project_type;";

            command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@startMonth", start_month);
            command.Parameters.AddWithValue("@endMonth", end_month);
            adapter = new MySqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;

            table.Rows.Add();
            calcTotal();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        public void filterFt_team(string ft_team)
        {
            // Replace with the desired application name

            string query = "SELECT a.PTSID, a.SOEID, r.resource_name as 'Resource Name', r.application as 'Application', r.feature_team as 'Feature Team', p.project_type as 'Project Type',";

            for (int y = year; y <= year; y++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    string monthYear = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)}_{y}";
                    string caseStatement = $"SUM(CASE WHEN a.month = {month} AND a.year = {y} THEN 1 - a.allocation ELSE 0 END) AS {monthYear},";
                    query += caseStatement;
                }
            }

            // Remove the trailing comma from the query
            string futureTotalStatement = "SUM(CASE WHEN a.month > @startMonth AND a.month <= @endMonth THEN 1 - a.allocation ELSE 0 END) AS 'Sum Of Future Total'";
            query += futureTotalStatement;
            query = query.TrimEnd(',');

            query += "\r\nFROM allocation_master a ";
            query += "\r\nJOIN resource_master r ON a.SOEID = r.SOEID ";
            query += "\r\nJOIN project_master p ON a.PTSID = p.PTSID ";
            query += $"\r\nWHERE r.feature_team LIKE '%{ft_team}%' "; // Apply the application filter
            query += "\r\nGROUP BY a.PTSID, a.SOEID, r.resource_name, r.application, r.feature_team, p.ptsid, p.project_type;";

            command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@startMonth", start_month);
            command.Parameters.AddWithValue("@endMonth", end_month);
            adapter = new MySqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;

            table.Rows.Add();
            calcTotal();

        }

        private void button4_Click(object sender, EventArgs e)
        {


        }

        public void filterPt(string pt)
        {
            

            string query = "SELECT a.PTSID, a.SOEID, r.resource_name as 'Resource Name', r.application as 'Application', r.feature_team as 'Feature Team', p.project_type as 'Project Type',";

            for (int y = year; y <= year; y++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    string monthYear = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {y}";
                    string caseStatement = $"SUM(CASE WHEN a.month = {month} AND a.year = {y} THEN 1 - a.allocation ELSE 0 END) AS '{monthYear}',";
                    query += caseStatement;
                }
            }
            string futureTotalStatement = "SUM(CASE WHEN a.month > @startMonth AND a.month <= @endMonth THEN 1 - a.allocation ELSE 0 END) AS 'Sum Of Future Total'";
            query += futureTotalStatement;
            // Remove the trailing comma from the query
            query = query.TrimEnd(',');

            query += "\r\nFROM allocation_master a ";
            query += "\r\nJOIN resource_master r ON a.SOEID = r.SOEID ";
            query += "\r\nJOIN project_master p ON a.PTSID = p.PTSID ";
            query += $"\r\nWHERE p.project_type LIKE '%{pt}%' "; // Apply the project_type filter
            query += "\r\nGROUP BY a.PTSID, a.SOEID, r.resource_name, r.application, r.feature_team, p.ptsid, p.project_type;";

            command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@startMonth", start_month);
            command.Parameters.AddWithValue("@endMonth", 12);
            adapter = new MySqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;

            table.Rows.Add();
            calcTotal();
        }




        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        //clear filters
       

      

        private void ExportToExcel(DataGridView dataGridView, FileInfo excelFile)
        {
            using (ExcelPackage package = new ExcelPackage(excelFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells["A1"].LoadFromDataTable(table, true);

                package.Save();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {




        }


        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            // Check if it's the total row
            
        }

        //VIEW ALL
        public void viewAll() {
            dataGridView1.DataSource = new DataTable();

            dataGridView1.ScrollBars = ScrollBars.Both;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Candara", 10,FontStyle.Bold);
            dataGridView1.RowHeadersVisible = true;
            dataGridView1.ColumnHeadersVisible = true;
            dataGridView1.Focus();
            dataGridView1.TabStop = true;
            if (comboBox5.SelectedIndex != -1 && comboBox6.SelectedIndex != -1 && comboBox7.SelectedIndex != -1 && comboBox8.SelectedIndex != -1) {
                start_month = comboBox5.SelectedIndex + 1;
                end_month = comboBox7.SelectedIndex + 1;
                string sy = (comboBox6.SelectedItem.ToString());
                year = int.Parse(sy);
                string ey = comboBox8.SelectedItem.ToString();
                end_year = int.Parse(ey);
               // MessageBox.Show(comboBox6.SelectedItem.ToString() + " " + comboBox8.SelectedItem.ToString());
            }
          
            if (year <= end_year)
            {
                string query4 = "SELECT a.PTSID, a.SOEID, r.resource_name as 'Resource Name', r.application as 'Application', r.feature_team as 'Feature Team', p.project_type as 'Project Type',";

                for (int y = year; y <= end_year; y++)
                {

                    int currentStartMonth = (y == year) ? start_month : 1;
                    int currentEndMonth = (y == end_year) ? end_month : 12;

                    for (int month = currentStartMonth; month <= currentEndMonth; month++)
                    {
                        string monthYear = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)}{y}";
                        string caseStatement = $"SUM(CASE WHEN a.month = {month} AND a.year = {y} THEN 1 - a.allocation ELSE 0 END) AS {monthYear},";
                        query4 += caseStatement;
                    }

                }
                string futureTotalStatement = "SUM(CASE WHEN a.month > @startMonth AND a.month <= @endMonth THEN 1 - a.allocation ELSE 0 END) AS 'Sum Of Future Total'";
                query4 += futureTotalStatement;
                // Remove the trailing comma from the query

                query4 = query4.TrimEnd(',');

                query4 += "\r\nFROM allocation_master a ";
                query4 += "\r\nJOIN resource_master r ON a.SOEID = r.SOEID ";
                query4 += "\r\nJOIN project_master p ON a.PTSID = p.PTSID ";
                query4 += "\r\nGROUP BY a.PTSID, a.SOEID, r.resource_name, r.application, r.feature_team, p.ptsid, p.project_type;";

                command = new MySqlCommand(query4, connection);
                command.Parameters.AddWithValue("@startMonth", currentMonth);
                command.Parameters.AddWithValue("@endMonth", 12);
                adapter = new MySqlDataAdapter(command);
                table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                dataGridView1.Enabled = true;
                table.Rows.Add();
                calcTotal();
            }
            else {
                MessageBox.Show("Invalid Month-Year Range");
            }

          
        }
           private void button3_Click_2(object sender, EventArgs e)
        {

            viewAll();
        }

        //download
        private void button2_Click_2(object sender, EventArgs e)
        {
            
        }

        //clear
        private void button1_Click_2(object sender, EventArgs e)
        {
            
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            comboBox6.SelectedIndex = -1;
            comboBox7.SelectedIndex = -1;
            comboBox8.SelectedIndex = -1;
            start_month = currentMonth;
            year = currentYear;
            
            end_month = start_month + 3;
            if (start_month + 3 <= 12)
            {
                end_year = year;
            }
            else {
                end_year = year + 1;
            }
{}           
            viewAll();
        }

        //filter res name
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string res_name = comboBox1.Text.ToString();
            filterName(res_name);
            calcTotal();
        }

        //filter appln
        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
             string appln = comboBox2.Text.ToString();
             filterAppln(appln);

             calcTotal();
        
        }

        //filter project type
        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string pt = comboBox3.Text.ToString();
            filterPt(pt);
            calcTotal();
        }

        //filter feature team
        private void comboBox4_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string ft_team = comboBox4.Text.ToString();
  

            filterFt_team(ft_team);
            calcTotal();
        }





        //calculate total allocation
        private void calcTotal() 
        {
           
            dataGridView1[5, dataGridView1.Rows.Count - 1].Value = "Total: ";
           // dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[5].Style.ForeColor = Color.Blue;

            for (int j = 6; j <= dataGridView1.ColumnCount - 1; j++) {
                decimal tot = 0;

                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    var value1 = dataGridView1.Rows[i].Cells[j].Value;

                    if (value1 != DBNull.Value)
                    {
                        tot += Convert.ToDecimal(value1);



                    }
                }
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[j].Value = tot.ToString();
            }
            int test2 = dataGridView1.Rows.Count - 1;
            
            int test = 10;

        }


        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void avaiCapView_Load(object sender, EventArgs e)
        {
             connection.Open();
            viewAll();
            string query = "SELECT DISTINCT application FROM resource_master";

            command = new MySqlCommand(query, connection);
            // Execute the query and read the distinct values
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                // Add each distinct value to the combobox
                string value = reader["application"].ToString();
                comboBox2.Items.Add(value);
            }

            // Close the reader and the connection
            reader.Close();
            connection.Close();

            connection.Open();

            string query1 = "SELECT DISTINCT feature_team FROM resource_master";

            command = new MySqlCommand(query1, connection);
            // Execute the query and read the distinct values
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                // Add each distinct value to the combobox
                string value1 = reader["feature_team"].ToString();
                comboBox4.Items.Add(value1);
            }

            // Close the reader and the connection
            reader.Close();
            connection.Close();

            connection.Open();
            string query2 = "SELECT DISTINCT resource_name FROM resource_master";

            command = new MySqlCommand(query2, connection);
            // Execute the query and read the distinct values
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                // Add each distinct value to the combobox
                string value = reader["resource_name"].ToString();
                comboBox1.Items.Add(value);
            }

            // Close the reader and the connection
            reader.Close();

            string query3 = "SELECT DISTINCT project_type FROM project_master";

            command = new MySqlCommand(query3, connection);
            // Execute the query and read the distinct values
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                // Add each distinct value to the combobox
                string value = reader["project_type"].ToString();
                comboBox3.Items.Add(value);
            }

            // Close the reader and the connection
            reader.Close();

      
            string query6 = "SELECT DISTINCT year FROM allocation_master";

            command = new MySqlCommand(query6, connection);
            // Execute the query and read the distinct values
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                // Add each distinct value to the combobox
                string value = reader["year"].ToString();
                comboBox6.Items.Add(value);
                comboBox8.Items.Add(value);
            }

            // Close the reader and the connection
            reader.Close();

            //viewAll();

           
     
            connection.Close();
        }


        //download button new
        private void button4_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx";
            saveFileDialog.Title = "Save Excel File";

            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                ExportToExcel(dataGridView1, excelFile);
                MessageBox.Show("Excel file saved successfully!");
            }

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
        private void dataGridView1_RowPrePaint_1(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            calcTotal();
            if (e.RowIndex == dataGridView1.Rows.Count - 1)
            {
                // Set the background color of the row
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkBlue;

                // Set the font weight to bold
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Bold);
            }
        }
    }
}






