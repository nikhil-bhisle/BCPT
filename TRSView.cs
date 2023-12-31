﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Windows.Media;
using MySqlX.XDevAPI.Relational;
using System.Diagnostics;

namespace ConsolFromApp
{
    public partial class TRSView : UserControl
    {
        MySqlConnection connection = new MySqlConnection("server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings");
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataTable table;
        AutoCompleteStringCollection autoCompleteCollectionSOEIDs; // Collection for suggestions SOEIDs
        AutoCompleteStringCollection autoCompleteCollectionResourceNames; // Collection for suggestions Resource Names
        AutoCompleteStringCollection autoCompleteCollectionPTSIDs; // Collection for suggestions PTSIDs
        AutoCompleteStringCollection autoCompleteCollectionProjectNames; // Collection for suggestions Project Names
        AutoCompleteStringCollection autoCompleteCollectionYears; // Collection for suggestions Years

        public TRSView()
        {
            InitializeComponent();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            // Initialize the collection for suggestions
            autoCompleteCollectionSOEIDs = new AutoCompleteStringCollection();
            autoCompleteCollectionResourceNames = new AutoCompleteStringCollection();
            autoCompleteCollectionPTSIDs = new AutoCompleteStringCollection();
            autoCompleteCollectionProjectNames = new AutoCompleteStringCollection();
            autoCompleteCollectionYears = new AutoCompleteStringCollection();

            FetchSuggestionsFromDatabase();

            // Enable suggestion feature for TextBox1
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox1.AutoCompleteCustomSource = autoCompleteCollectionSOEIDs;

            // Enable suggestion feature for TextBox2
            textBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox2.AutoCompleteCustomSource = autoCompleteCollectionResourceNames;

            // Enable suggestion feature for TextBox3
            textBox3.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox3.AutoCompleteCustomSource = autoCompleteCollectionPTSIDs;

            // Enable suggestion feature for TextBox4
            textBox4.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox4.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox4.AutoCompleteCustomSource = autoCompleteCollectionProjectNames;

            // Enable suggestion feature for TextBox5
            textBox5.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox5.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox5.AutoCompleteCustomSource = autoCompleteCollectionYears;
        }
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
                    autoCompleteCollectionSOEIDs.Add(suggestion);
                }
                reader.Close();

                // Fetch PTSIDs suggestions from the database
                string queryPTSIDs = "SELECT DISTINCT PTSID FROM project_master";
                command = new MySqlCommand(queryPTSIDs, connection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string suggestion = reader["PTSID"].ToString();
                    autoCompleteCollectionPTSIDs.Add(suggestion);
                }
                reader.Close();

                // Fetch Resource Names suggestions from the database
                string queryResourceNames = "SELECT DISTINCT resource_name FROM resource_master";
                command = new MySqlCommand(queryResourceNames, connection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string suggestion = reader["resource_name"].ToString();
                    autoCompleteCollectionResourceNames.Add(suggestion);
                }
                reader.Close();

                // Fetch project names suggestions from the database
                string queryProjectNames = "SELECT DISTINCT description FROM project_master";
                command = new MySqlCommand(queryProjectNames, connection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string suggestion = reader["description"].ToString();
                    autoCompleteCollectionProjectNames.Add(suggestion);
                }
                reader.Close();

                // Fetch years suggestions from the database
                string queryYears = "SELECT DISTINCT year FROM allocation_master";
                command = new MySqlCommand(queryYears, connection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string suggestion = reader["year"].ToString();
                    autoCompleteCollectionYears.Add(suggestion);
                }

                reader.Close();
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
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            func();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void clearfields()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            DateTime currentDate = DateTime.Now;
            int currentMonth = currentDate.Month; // Current month as an integer (1 to 12)
            Dictionary<int, string> monthDictionary = new Dictionary<int, string>()
            {
                { 0, "Jan" },
                { 1, "Feb" },
                { 2, "Mar" },
                { 3, "Apr" },
                { 4, "May" },
                { 5, "Jun" },
                { 6, "Jul" },
                { 7, "Aug" },
                { 8, "Sep" },
                { 9, "Oct" },
                { 10, "Nov" },
                { 11, "Dec" }
            };
            comboBox1.SelectedIndex = currentMonth - 1; // Month ComboBox
            string currentYear = currentDate.Year.ToString(); // Current year as an integer
            textBox5.Text = currentYear;
            string month = monthDictionary[currentMonth - 1];
            string query1 = $@"SELECT rm.goc AS GOC, rm.resource_name AS 'Resource Name', rm.SOEID AS SOEID, pm.PTSID AS PTSID, pm.description AS 'Project Name', rm_pm.resource_name AS 'Project Manager', am.allocation AS '{month}-{currentYear}'
                  FROM allocation_master AS am
                  JOIN resource_master rm ON am.SOEID = rm.SOEID
                  JOIN project_master pm ON am.PTSID = pm.PTSID
                  JOIN resource_master rm_pm ON pm.project_manager = rm_pm.SOEID
                    WHERE (
                        am.month LIKE '%{currentMonth}%' AND 
                        am.year LIKE '%{currentYear}%'
                    )
                  ORDER BY rm.goc";
            command = new MySqlCommand(query1, connection);
            adapter = new MySqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
        public void func()
        {
            int selectedMonth = comboBox1.SelectedIndex;
            Dictionary<int, string> monthDictionary = new Dictionary<int, string>()
            {
                { 0, "Jan" },
                { 1, "Feb" },
                { 2, "Mar" },
                { 3, "Apr" },
                { 4, "May" },
                { 5, "Jun" },
                { 6, "Jul" },
                { 7, "Aug" },
                { 8, "Sep" },
                { 9, "Oct" },
                { 10, "Nov" },
                { 11, "Dec" }
            };
            string selectedYear = textBox5.Text.ToString();
            string SOEID = textBox1.Text.ToString();
            string resource_name = textBox2.Text.ToString();
            string PTSID = textBox3.Text.ToString();
            string project_name = textBox4.Text.ToString();
            if (selectedMonth != -1 && !string.IsNullOrEmpty(textBox5.Text))
            {
                string month = monthDictionary[selectedMonth];
                string query = $@"SELECT rm.goc AS GOC, rm.resource_name AS 'Resource Name', rm.SOEID AS SOEID, pm.PTSID AS PTSID, pm.description AS 'Project Name', rm_pm.resource_name AS 'Project Manager', am.allocation AS '{month}-{selectedYear}'
                  FROM allocation_master AS am
                  JOIN resource_master rm ON am.SOEID = rm.SOEID
                  JOIN project_master pm ON am.PTSID = pm.PTSID
                  JOIN resource_master rm_pm ON pm.project_manager = rm_pm.SOEID
                   WHERE (
                       am.month LIKE '{selectedMonth + 1}' AND 
                       am.year LIKE '{selectedYear}' AND
                       rm.SOEID LIKE '%{SOEID}%' AND
                       rm.resource_name LIKE '%{resource_name}%' AND
                       pm.PTSID LIKE '%{PTSID}%' AND
		               pm.description LIKE '%{project_name}%'
                    )
                  ORDER BY rm.goc";
                command = new MySqlCommand(query, connection);
                adapter = new MySqlDataAdapter(command);
                table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
            else if (selectedMonth != -1 && string.IsNullOrEmpty(textBox5.Text))
            {
                string month = monthDictionary[selectedMonth];
                string query = $@"SELECT rm.goc AS GOC, rm.resource_name AS 'Resource Name', rm.SOEID AS SOEID, pm.PTSID AS PTSID, pm.description AS 'Project Name', rm_pm.resource_name AS 'Project Manager'
                  FROM allocation_master AS am
                  JOIN resource_master rm ON am.SOEID = rm.SOEID
                  JOIN project_master pm ON am.PTSID = pm.PTSID
                  JOIN resource_master rm_pm ON pm.project_manager = rm_pm.SOEID
                   WHERE (
                       am.month LIKE '{selectedMonth + 1}' AND 
                       rm.SOEID LIKE '%{SOEID}%' AND
                       rm.resource_name LIKE '%{resource_name}%' AND
                        pm.PTSID LIKE '%{PTSID}%' AND
		                pm.description LIKE '%{project_name}%'
                    )
                  ORDER BY rm.goc";
                command = new MySqlCommand(query, connection);
                adapter = new MySqlDataAdapter(command);
                table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
            else
            {
                string query = $@"SELECT rm.goc AS GOC, rm.resource_name AS 'Resource Name', rm.SOEID AS SOEID, pm.PTSID AS PTSID, pm.description AS 'Project Name', rm_pm.resource_name AS 'Project Manager'
                  FROM allocation_master AS am
                  JOIN resource_master rm ON am.SOEID = rm.SOEID
                  JOIN project_master pm ON am.PTSID = pm.PTSID
                  JOIN resource_master rm_pm ON pm.project_manager = rm_pm.SOEID
                   WHERE (
                        am.year LIKE '{selectedYear}' AND
                       rm.SOEID LIKE '%{SOEID}%' AND
                       rm.resource_name LIKE '%{resource_name}%' AND
                        pm.PTSID LIKE '%{PTSID}%' AND
		                pm.description LIKE '%{project_name}%'
                    )
                  ORDER BY rm.goc";
                command = new MySqlCommand(query, connection);
                adapter = new MySqlDataAdapter(command);
                table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
        }

        private void ExportToExcel(DataGridView dataGridView, FileInfo excelFile)
        {
            using (ExcelPackage package = new ExcelPackage(excelFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells["A1"].LoadFromDataTable(table, true);

                package.Save();
            }
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // SOEID TextBox
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Resource Name TextBox
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // PTSID TextBox
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            // Project Name TextBox
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            // Year TextBox
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            // Clear filter buuton
            clearfields();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TRSView_Load(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            int currentMonth = currentDate.Month; // Current month as an integer (1 to 12)
            int currentYear = currentDate.Year; // Current year as an integer
            comboBox1.SelectedIndex = currentMonth - 1; // Month ComboBox
            textBox5.Text = currentYear.ToString();
            Dictionary<int, string> monthDictionary = new Dictionary<int, string>()
            {
                { 0, "Jan" },
                { 1, "Feb" },
                { 2, "Mar" },
                { 3, "Apr" },
                { 4, "May" },
                { 5, "Jun" },
                { 6, "Jul" },
                { 7, "Aug" },
                { 8, "Sep" },
                { 9, "Oct" },
                { 10, "Nov" },
                { 11, "Dec" }
            };
            string month = monthDictionary[currentMonth - 1];
            string query1 = $@"SELECT rm.goc AS GOC, rm.resource_name AS 'Resource Name', rm.SOEID AS SOEID, pm.PTSID AS PTSID, pm.description AS 'Project Name', rm_pm.resource_name AS 'Project Manager', am.allocation AS '{month}-{currentYear}'
                  FROM allocation_master AS am
                  JOIN resource_master rm ON am.SOEID = rm.SOEID
                  JOIN project_master pm ON am.PTSID = pm.PTSID
                  JOIN resource_master rm_pm ON pm.project_manager = rm_pm.SOEID
                    WHERE (
                        am.month LIKE '{currentMonth}' AND 
                        am.year LIKE '{currentYear}'
                    )
                  ORDER BY rm.goc";
            command = new MySqlCommand(query1, connection);
            adapter = new MySqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string filename = "TRSView"; // Default filename
            Dictionary<int, string> monthDictionary = new Dictionary<int, string>()
            {
                { 0, "Jan" },
                { 1, "Feb" },
                { 2, "Mar" },
                { 3, "Apr" },
                { 4, "May" },
                { 5, "Jun" },
                { 6, "Jul" },
                { 7, "Aug" },
                { 8, "Sep" },
                { 9, "Oct" },
                { 10, "Nov" },
                { 11, "Dec" }
            };
            // Check if a specific month and year are selected
            if (comboBox1.SelectedIndex != -1 && !string.IsNullOrEmpty(textBox5.Text))
            {
                int selectedMonth = comboBox1.SelectedIndex + 1;
                string selectedYear = textBox5.Text.ToString();

                // Generate filename using the selected month and year
                string monthName = monthDictionary[selectedMonth - 1];
                filename = $"TRSView_{monthName}_{selectedYear}";
            }
            // Check if only a month is selected
            else if (comboBox1.SelectedIndex != -1)
            {
                int selectedMonth = comboBox1.SelectedIndex + 1;
                string monthName = monthDictionary[selectedMonth - 1];
                filename = $"TRSView_{monthName}";
            }
            // Check if only a year is selected
            else if (!string.IsNullOrEmpty(textBox5.Text))
            {
                string selectedYear = textBox5.Text.ToString();
                filename = $"TRSView_{selectedYear}";
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx";
            saveFileDialog.Title = "Save Excel File";
            saveFileDialog.FileName = filename;
            saveFileDialog.ShowDialog();

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog.FileName != "")
                {
                    FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                    ExportToExcel(dataGridView1, excelFile);
                    MessageBox.Show("Excel file saved successfully!");
                }
            }
        }
    }
}
