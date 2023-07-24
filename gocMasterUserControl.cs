using ConsolFromApp;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BCPT
{
    public partial class gocMasterUserControl : UserControl
    {
        //for login id
        //private string loginID;

        public string loginID;
        
        //{
        //    get { return loginID; }
        //    set { loginID = value; }
        //}

        public static gocMasterUserControl instance;
        public gocMasterUserControl()
        {
            InitializeComponent();
            dropDownGOC();
            maintable();
            instance = this;

        }
        private void maintable()
        {
           
            DataTable dataTable = new DataTable();
            string sql;
            MySqlCommand cmd;
            string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
            MySqlConnection conn = new MySqlConnection(connstring);
            try
            {
                conn.Open();
                sql = "SELECT gocmaster.goc as \"GOC Code\",gocmaster.rate as \"Rate\",gocmaster.one_month_cost_per_resource as \"Cost/Resource\",gocmaster.goc_description as \"GOC Description\" FROM gocmaster";
                cmd = new MySqlCommand(sql, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                DataGridViewColumn gocColumn = dataGridView1.Columns["GOC Code"];
                gocColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                gocColumn.Width = 100;
                DataGridViewColumn rateColumn = dataGridView1.Columns["Rate"];
                rateColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                rateColumn.Width = 100;
                DataGridViewColumn costColumn = dataGridView1.Columns["Cost/Resource"];
                costColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                costColumn.Width = 100;
                dataGridView1.Refresh();

            }
            catch 
            {
                MessageBox.Show("Some Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally 
            { 
                conn.Close(); 
            }
        }
        private void dropDownGOC()
        {

            string sql;
            MySqlCommand cmd;
            MySqlDataReader dr;
            string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
            MySqlConnection conn = new MySqlConnection(connstring);
            try
            {
                conn.Open();
                sql = "SELECT * FROM gocmaster";
                cmd = new MySqlCommand(sql, conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string gocId = dr.GetString(0);//0 is index in database
                    string gocDescription = dr.GetString(3);
                    string gocAll = gocId + " - " + gocDescription;
                    comboBox1.Items.Add(gocAll);
                }


            }
            catch
            {
                MessageBox.Show("Invalid GOC Id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void createGocbtn_Click(object sender, EventArgs e)
        {
            newGOCDetail newform= new newGOCDetail(loginID);
            newform.FormClosed += Newform__FormClosed;
            newform.ShowDialog();
        }
        private void Newform__FormClosed(object sender, FormClosedEventArgs e)
        {
            maintable();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                DataTable dt = new DataTable();

                string selectedItem1 = comboBox1.SelectedItem.ToString(); // contains description as well
                string[] parts = selectedItem1.Split('-');
                string selectedItem = "";

                if (parts.Length > 0)
                {
                    selectedItem = parts[0];

                }

                MySqlCommand cmd;
                string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
                MySqlConnection conn = new MySqlConnection(connstring);
                conn.Open();
                string sql = "SELECT gocmaster.goc as \"GOC Code\",gocmaster.rate as \"Rate\",gocmaster.one_month_cost_per_resource as \"Cost/Resource\",gocmaster.goc_description as \"GOC Description\" FROM gocmaster WHERE gocmaster.goc=" + selectedItem;
                cmd = new MySqlCommand(sql, conn);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                DataGridViewColumn gocColumn = dataGridView1.Columns["GOC Code"];
                gocColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                gocColumn.Width = 100;
                DataGridViewColumn rateColumn = dataGridView1.Columns["Rate"];
                rateColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                rateColumn.Width = 100;
                DataGridViewColumn costColumn = dataGridView1.Columns["Cost/Resource"];
                costColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                costColumn.Width = 100;
                dataGridView1.Refresh();





            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                DataTable dataTable = new DataTable();
                string sql;
                MySqlCommand cmd;
                string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
                MySqlConnection conn = new MySqlConnection(connstring);
                try
                {
                    conn.Open();
                    sql = "SELECT gocmaster.goc as \"GOC Code\",gocmaster.rate as \"Rate\",gocmaster.one_month_cost_per_resource as \"Cost/Resource\",gocmaster.goc_description as \"GOC Description\" FROM gocmaster";
                    cmd = new MySqlCommand(sql, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                    DataGridViewColumn gocColumn = dataGridView1.Columns["GOC Code"];
                    gocColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    gocColumn.Width = 100;
                    DataGridViewColumn rateColumn = dataGridView1.Columns["Rate"];
                    rateColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    rateColumn.Width = 100;
                    DataGridViewColumn costColumn = dataGridView1.Columns["Cost/Resource"];
                    costColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    costColumn.Width = 100;
                    dataGridView1.Refresh();

                }
                catch
                {
                    MessageBox.Show("Some Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                    comboBox1.SelectedIndex = -1;
                    
                }
                
            }
        }

        private void editGocbtn_Click(object sender, EventArgs e)
        {
            //string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
            //using (MySqlConnection connection =new MySqlConnection(connstring))
            {
                //connection.Open();
                int totalrows=this.dataGridView1.RowCount;
                List<string> listOfSelectedGoc = new List<string>();

                for(int i=0;i<totalrows;i++)
                {
                    if (this.dataGridView1.Rows[i].Cells[0].Value != null && (bool)this.dataGridView1.Rows[i].Cells[0].Value==true)
                    {
                        listOfSelectedGoc.Add((((System.Data.DataTable)this.dataGridView1.DataSource).Rows[i]["GOC Code"]).ToString());
                    }
                }
               

                if(listOfSelectedGoc.Count > 1)
                {
                    MessageBox.Show("Choose only one GOC to edit at a time","Alert",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else if (listOfSelectedGoc.Count <= 0)
                {
                    MessageBox.Show("Select GOC to Edit", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string gocToEdit = listOfSelectedGoc[0];
                    editGocMaster newform1 = new editGocMaster(loginID,gocToEdit);
                    newform1.FormClosed += Newform__FormClosed;
                    newform1.ShowDialog();


                }
            }

            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings"; 
            int totalrows = this.dataGridView1.RowCount;
            List<string> listOfSelectedGoc = new List<string>();
            int countDeletedRows = 0;

            for (int i = 0; i < totalrows; i++)
            {
                if (this.dataGridView1.Rows[i].Cells[0].Value != null && (bool)this.dataGridView1.Rows[i].Cells[0].Value == true)
                {
                    listOfSelectedGoc.Add((((System.Data.DataTable)this.dataGridView1.DataSource).Rows[i]["GOC Code"]).ToString());
                }
            }
            if(listOfSelectedGoc.Count <= 0)
            {
                MessageBox.Show("Select atleast one GOC to Delete", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else

            {
                DialogResult result = MessageBox.Show("Are you sure you want to proceed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string modified = loginID;
                    DateTime now = DateTime.Now;
                    for (int i = 0; i < listOfSelectedGoc.Count; i++)
                    {
                        
                        string gocToDelete = listOfSelectedGoc[i];
                        string queryUpdate = "UPDATE gocmaster SET modified_by= @Modified,modified_datetime=@Time WHERE goc=@GOC";
                        DateTime time = DateTime.Now;
                        string modified_time = now.ToString("yyyy-MM-dd HH:mm:ss");
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            try
                            {
                                connection.Open();
                                MySqlCommand command = new MySqlCommand(queryUpdate, connection);
                                command.Parameters.AddWithValue("@GOC", gocToDelete);
                                command.Parameters.AddWithValue("@Modified", modified);
                                command.Parameters.AddWithValue("@Time", modified_time);
                                int rowsAffected = command.ExecuteNonQuery();



                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("An error occurred: " + ex.Message);
                            }
                        }
                        string queryDelete = "DELETE FROM gocmaster WHERE goc=@GOC";
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            try
                            {
                                connection.Open();
                                MySqlCommand command = new MySqlCommand(queryDelete, connection);
                                command.Parameters.AddWithValue("@GOC", gocToDelete);
                                
                                int rowsAffected = command.ExecuteNonQuery();
                                countDeletedRows++;



                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Selected GOC "+gocToDelete+" is currently in Use " );
                            }
                        }


                    }
                    if (countDeletedRows== listOfSelectedGoc.Count)
                    {
                        MessageBox.Show("All the selected GOC deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            maintable();
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
        private void gocMasterUserControl_Load(object sender, EventArgs e)
        {
            /*
            string userRole = GetRole(MainForm.instance.user.Text);

            // Check the user's role and set the visibility of buttons accordingly
            if (userRole == "admin")
            {
                // If user is an admin, show the buttons
                createGocbtn.Visible = true;
                editGocbtn.Visible = true;
                button3.Visible = true;
            }
            else
            {
                // If user is not an admin, hide the buttons
                createGocbtn.Visible = false;
                editGocbtn.Visible = false;
                button3.Visible = false;
            }
            */
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewCell cell = dataGridView1[e.ColumnIndex, e.RowIndex];

            // Check if the current cell belongs to the checkbox column
            if (cell.OwningColumn.Name != "Selected_column")
            {
                // Cancel the editing for all columns except the checkbox column
                e.Cancel = true;
            }
        }
    }
}
