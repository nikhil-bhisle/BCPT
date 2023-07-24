using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCPT
{
    public partial class auditUserControl : UserControl
    {
        
        string connstring = "server = localhost; uid = root; pwd = Y1012Jqkhkp; database = learnings";
        DateTime SelectedStartDate= DateTime.MinValue;
        DateTime selectedEndDate= DateTime.Now;
        
        public auditUserControl()
        {
            InitializeComponent();
            



        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

       

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //DateTime start;
            SelectedStartDate = dateTimePicker1.Value;
            

            //Debug.WriteLine("1 "+startDate);


            //Debug.WriteLine("2 "+startDate);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

            selectedEndDate = dateTimePicker2.Value; 
            
          
        }

        private void pmaudit_Click(object sender, EventArgs e)
        {


            //-string connstring = "server=localhost;uid=root;pwd=Y1012Jqkhkp;database=learnings";
            string startDate = SelectedStartDate.ToString("yyyy-MM-dd 00:00:00");
            string endDate = selectedEndDate.ToString("yyyy-MM-dd 23:59:59");
            DateTime startDateValue = DateTime.ParseExact(startDate, "yyyy-MM-dd HH:mm:ss", null);
            DateTime endDateValue = DateTime.ParseExact(endDate, "yyyy-MM-dd HH:mm:ss", null);

            if (startDateValue > endDateValue)
            {
                MessageBox.Show("Select the Date Range Properly", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            else
            {
                DataTable dataTable = new DataTable();
                string sql;
                MySqlCommand cmd;
                MySqlConnection conn = new MySqlConnection(connstring);

                try
                {
                    conn.Open();

                    // sql = "SELECT audit_project_master.PTSID as 'PTS ID',audit_project_master.description as 'Project Description', audit_project_master.project_type as 'Project Type', audit_project_master.program_id as 'Program Id', audit_project_master.project_manager as 'Project Manager', audit_project_master.piac_category as 'PIAC Category', audit_project_master.target_release_date as 'Target Release Date', audit_project_master.project_end_date as 'Project End Date', audit_project_master.country as 'Country', audit_project_master.product as 'Product', audit_project_master.status as 'Status', audit_project_master.secore_l0 as 'Secore L0', audit_project_master.dotnet_l0 as '.NET L0', audit_project_master.secore_l1 as 'Secore L1', audit_project_master.dotnet_l1 as '.NET L1', audit_project_master.modified_by as 'Modified By',audit_project_master.modified_datetime as 'Modified DateTime',audit_project_master.audit_datetime as 'Audit DateTime' FROM learnings.audit_project_master WHERE modified_datetime >= '" + startDate + "' AND modified_datetime<= '" + endDate + "';";
                    //ssql = "";
                    sql = "SELECT IF(m.PTSID IS NULL AND a.modified_datetime = (SELECT MAX(modified_datetime) FROM audit_project_master WHERE PTSID = a.PTSID), 'Deleted', 'Updated') AS Action, a.PTSID AS 'PTS ID', a.description AS 'Description', a.project_type AS 'Project Type', a.program_id AS 'Program ID', a.project_manager AS 'Project Manager', a.piac_category AS 'PIAC Category', a.target_release_date AS 'Target Release Date', a.project_end_date AS 'Project End Date', a.country AS 'Country', a.product AS 'Product', a.status AS 'Status', a.secore_l0 AS 'Secore L0', a.dotnet_l0 AS '.Net L0', a.secore_l1 AS 'Secore L1', a.dotnet_l1 AS '.Net L1', a.modified_by AS 'Modified By', a.modified_datetime AS 'Modified DateTime', a.archieve AS 'Archive' FROM audit_project_master a LEFT JOIN project_master m ON a.PTSID=m.PTSID where a.modified_datetime>='" + startDate + "' And a.modified_datetime<='" + endDate + "';";
                    cmd = new MySqlCommand(sql, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataTable);
                    dataGridViewpm.DataSource = dataTable;
                    dataGridViewpm.Refresh();
                    //to have the action column at end
                    DataGridViewColumn actionColumn = dataGridViewpm.Columns["Action"];
                    dataGridViewpm.Columns.Remove(actionColumn);
                    dataGridViewpm.Columns.Add(actionColumn);
                    dataGridViewpm.Refresh();
                    dataGridViewpm.Visible = true;
                    dataGridViewam.Visible = false;
                    dataGridViewgm.Visible = false;
                    dataGridViewrm.Visible = false;
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
        }

        private void gmaudit_Click(object sender, EventArgs e)
        {

            
            string startDate = SelectedStartDate.ToString("yyyy-MM-dd 00:00:00");
            string endDate = selectedEndDate.ToString("yyyy-MM-dd 23:59:59");
            DateTime startDateValue = DateTime.ParseExact(startDate, "yyyy-MM-dd HH:mm:ss", null);
            DateTime endDateValue = DateTime.ParseExact(endDate, "yyyy-MM-dd HH:mm:ss", null);

           if(startDateValue > endDateValue)
            {
                MessageBox.Show("Select the Date Range Properly", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
            else
            {
                DataTable dataTable = new DataTable();
                string sql;
                MySqlCommand cmd;
                MySqlConnection conn = new MySqlConnection(connstring);

                try
                {
                    conn.Open();

                    //sql = "SELECT audit_gocmaster.goc as 'GOC Code',audit_gocmaster.rate as 'Rate',audit_gocmaster.one_month_cost_per_resource as 'Cost/Resource', audit_gocmaster.goc_description as 'GOC Description', audit_gocmaster.modified_by as 'Modified By', audit_gocmaster.modified_datetime as 'Modified DateTime',audit_gocmaster.audit_datetime as 'Audit DateTime' FROM learnings.audit_gocmaster WHERE modified_datetime >= '"+startDate+"' AND modified_datetime<= '"+endDate+"';";

                    //sql = "SELECT  a.goc, a.rate, a.one_month_cost_per_resource, a.goc_description, a.modified_by, a.modified_datetime, a.audit_datetime,IF(m.goc IS NULL AND a.modified_datetime = (SELECT MAX(modified_datetime) FROM audit_gocmaster WHERE goc = a.goc), 'deleted', 'updated') AS status FROM  audit_gocmaster a LEFT JOIN  gocmaster m ON a.goc = m.goc where a.modified_datetime>='"+startDate+"' And a.modified_datetime<='"+endDate+"';";
                    sql = "SELECT  IF(m.goc IS NULL AND a.modified_datetime = (SELECT MAX(modified_datetime) FROM audit_gocmaster WHERE goc = a.goc), 'Deleted', 'Updated') AS Action, a.goc AS 'GOC Code', a.rate AS 'Rate', a.one_month_cost_per_resource AS 'Cost/Resource', a.goc_description AS 'Description', a.modified_by AS 'Modified By', a.modified_datetime AS 'Modified DateTime' FROM audit_gocmaster a LEFT JOIN gocmaster m ON a.goc = m.goc  where a.modified_datetime>='" + startDate+"' And a.modified_datetime<='"+endDate+"';";
                    cmd = new MySqlCommand(sql, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataTable);
                    dataGridViewgm.DataSource = dataTable;
                    dataGridViewgm.Refresh();
                    dataGridViewpm.Visible = false;
                    dataGridViewam.Visible = false;
                    dataGridViewgm.Visible = true;
                    dataGridViewrm.Visible = false;
                    //to have the Action column at the end
                    //DataGridViewColumn actionColumn = dataGridViewpm.Columns["Action"];
                    //dataGridViewpm.Columns.Remove(actionColumn);
                    //dataGridViewpm.Columns.Add(actionColumn);
                    //dataGridViewpm.Refresh();
                }
                
                catch(Exception ex)
                {
                    //MessageBox.Show("Some Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }


            }
           
        }

        private void rmaudit_Click(object sender, EventArgs e)
        {
            string startDate = SelectedStartDate.ToString("yyyy-MM-dd 00:00:00");
            string endDate = selectedEndDate.ToString("yyyy-MM-dd 23:59:59");
            DateTime startDateValue = DateTime.ParseExact(startDate, "yyyy-MM-dd HH:mm:ss", null);
            DateTime endDateValue = DateTime.ParseExact(endDate, "yyyy-MM-dd HH:mm:ss", null);

            if (startDateValue > endDateValue)
            {
                MessageBox.Show("Select the Date Range Properly", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            else
            {
                DataTable dataTable = new DataTable();
                string sql;
                MySqlCommand cmd;
                MySqlConnection conn = new MySqlConnection(connstring);

                try
                {
                    conn.Open();


                    //sql = "SELECT audit_resource_master.SOEID, audit_resource_master.resource_name as 'Resource Name',audit_resource_master.resource_manager_name as 'Resource Manager Name',audit_resource_master.goc as 'GOC Code', audit_resource_master.role as 'Role',audit_resource_master.application as 'Application', audit_resource_master.location as 'Location',audit_resource_master.feature_team as 'Feature Team',audit_resource_master.modified_by as 'Modified By', audit_resource_master.modified_datetime as 'Modified DateTime',audit_resource_master.audit_datetime as 'Audit DateTime' FROM learnings.audit_resource_master WHERE modified_datetime >= '" + startDate + "' AND modified_datetime<= '" + endDate + "';";
                    //sql = "SELECT audit_gocmaster.goc as 'GOC Code',audit_gocmaster.rate as 'Rate',audit_gocmaster.one_month_cost_per_resource as 'Cost/Resource', audit_gocmaster.goc_description as 'GOC Description', audit_gocmaster.modified_by as 'Modified By', audit_gocmaster.modified_datetime as 'Modified DateTime',audit_gocmaster.audit_datetime as 'Audit DateTime' FROM learnings.audit_gocmaster WHERE modified_datetime >= '" + startDate + "' AND modified_datetime<= '" + endDate + "';";
                    //sql = "SELECT a.SOEID AS 'SOEID', a.resource_name AS 'Resource Name', a.resource_manager_name AS 'Resource Manager Name', a.goc AS 'GOC Code', a.role AS 'Role', a.application AS 'Application', a.location AS 'Location', a.feature_team AS 'Feature Team', a.modified_by AS 'Modified By', a.modified_datetime AS 'Modified DateTime', IF(m.SOEID IS NULL AND a.modified_datetime = (SELECT MAX(modified_datetime) FROM audit_resource_master WHERE SOEID = a.SOEID), 'Deleted', 'Updated') AS Action FROM audit_resource_master a LEFT JOIN resource_master m ON a.SOEID=m.SOEID where a.modified_datetime>='"+startDate+"' And a.modified_datetime<='"+endDate+"';";
                    // sql = "SELECT a.SOEID as 'SOEID', a.resource_name as 'Resource Name', a.resource_manager_name as 'Resource Manager Name', a.goc as 'GOC Code', a.role as 'Role', a.application as 'Application', a.location as 'Location', a.feature_team as 'Feature Team', a.modified_by as 'Modified By', a.modified_datetime as 'Modified DateTime', a.audit_datetime as'Audit DateTime', IF(m.SOEID IS NULL  AND a.modified_datetime = (SELECT MAX(modified_datetime) FROM audit_resource_master WHERE SOEID = a.SOEID), 'Deleted', 'Updated') AS Action FROM audit_resource_master a LEFT JOIN resource_master m ON a.SOEID=m.SOEID where a.modified_datetime>='"+startDate+"' AND a.modified_datetime<='"+endDate+"';";
                    sql = "SELECT IF(m.SOEID IS NULL  AND a.modified_datetime = (SELECT MAX(modified_datetime) FROM audit_resource_master WHERE SOEID = a.SOEID), 'Deleted', 'Updated') AS Action, a.SOEID as 'SOEID',a.resource_name as 'Resource Name',a.resource_manager_name as 'Resource Manager Name',a.goc as 'GOC Code',a.role as 'Role',a.application as 'Application',a.location as 'Location',a.feature_team as 'Feature Team', a.modified_by as 'Modified By',a.modified_datetime as 'Modified DateTime' ,a.archieve AS 'Archive' FROM  audit_resource_master a LEFT JOIN  resource_master m ON a.SOEID=m.SOEID where a.modified_datetime>='" + startDate+"' AND a.modified_datetime<='"+endDate+"';";
                    cmd = new MySqlCommand(sql, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataTable);
                    //int columnIndex = dataTable.Columns.IndexOf("Action");
                    //if(columnIndex !=-1)
                    //{
                    //    dataTable.Columns[columnIndex].SetOrdinal(10) ;
                    //}
                    dataGridViewrm.DataSource = dataTable;
                    dataGridViewpm.Visible = false;
                    dataGridViewam.Visible = false;
                    dataGridViewgm.Visible = false;
                    dataGridViewrm.Visible = true;
                    dataGridViewrm.Refresh();

                   
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Some Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }


            }
        }

        private void amaudit_Click(object sender, EventArgs e)
        {
            string startDate = SelectedStartDate.ToString("yyyy-MM-dd 00:00:00");
            string endDate = selectedEndDate.ToString("yyyy-MM-dd 23:59:59");
            DateTime startDateValue = DateTime.ParseExact(startDate, "yyyy-MM-dd HH:mm:ss", null);
            DateTime endDateValue = DateTime.ParseExact(endDate, "yyyy-MM-dd HH:mm:ss", null);

            if (startDateValue > endDateValue)
            {
                MessageBox.Show("Select the Date Range Properly", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            else
            {
                DataTable dataTable = new DataTable();
                string sql;
                MySqlCommand cmd;
                MySqlConnection conn = new MySqlConnection(connstring);

                try
                {
                    conn.Open();
                    Debug.WriteLine(startDate);
                    Debug.WriteLine(endDate);

                    // sql = "SELECT audit_gocmaster.goc as 'GOC Code',audit_gocmaster.rate as 'Rate',audit_gocmaster.one_month_cost_per_resource as 'Cost/Resource', audit_gocmaster.goc_description as 'GOC Description', audit_gocmaster.modified_by as 'Modified By', audit_gocmaster.modified_datetime as 'Modified DateTime',audit_gocmaster.audit_datetime as 'Audit DateTime' FROM learnings.audit_gocmaster WHERE modified_datetime >= '" + startDate + "' AND modified_datetime<= '" + endDate + "';";
                    //sql = "SELECT audit_allocation_master.PTSID as 'PTSID', audit_allocation_master.SOEID as 'SOEID',audit_allocation_master.month as 'Month', audit_allocation_master.year as 'Year', audit_allocation_master.allocation as 'Allocation', audit_allocation_master.modified_by as 'Modified By', audit_allocation_master.modified_datetime as 'Modified DateTime',audit_allocation_master.audit_datetime as 'Audit DateTime' FROM learnings.audit_allocation_master SELECT a.PTSID AS 'PTS ID', a.SOEID AS 'SOEID', a.month AS 'Month', a.year AS 'Year', a.allocation AS 'Allocation', a.modified_by AS 'Modified By', a.modified_datetime AS 'Modified DateTime', IF(m.PTSID AND m.SOEID IS NULL AND a.modified_datetime = (SELECT MAX(modified_datetime) FROM audit_allocation_master WHERE PTSID = a.PTSID AND SOEID = a.SOEID), 'Deleted', 'Updated') AS Status FROM audit_allocation_master a LEFT JOIN allocation_master m ON a.SOEID = m.SOEID AND a.PTSID = m.PTSID;";
                    //sql = "SELECT  IF(m.PTSID IS NULL AND m.SOEID IS NULL AND a.modified_datetime = (SELECT MAX(modified_datetime) FROM audit_allocation_master WHERE PTSID = a.PTSID AND SOEID = a.SOEID), 'Deleted', 'Updated') AS Action,a.PTSID AS 'PTSID', a.SOEID AS 'SOEID', a.month AS 'Month', a.year AS 'Year', a.allocation AS 'Allocation', a.modified_by AS 'Modified By', a.modified_datetime AS 'Modified DateTime'  IF(m.PTSID IS NULL AND m.SOEID IS NULL AND a.modified_datetime = (SELECT MAX(modified_datetime) FROM audit_allocation_master WHERE PTSID = a.PTSID AND SOEID = a.SOEID), 'Deleted', 'Updated') AS Action FROM audit_allocation_master a LEFT JOIN allocation_master m ON a.SOEID = m.SOEID AND a.PTSID = m.PTSID where a.modified_datetime>='" + startDate+"' And a.modified_datetime<='"+endDate+"';";
                    sql = "SELECT IF(m.PTSID IS NULL And m.SOEID IS NULL AND a.modified_datetime = (SELECT MAX(modified_datetime) FROM audit_allocation_master WHERE PTSID = a.PTSID AND SOEID=a.SOEID), 'Deleted', 'Updated') AS 'Action', a.PTSID as 'PTS ID', a.SOEID as 'SOEID', a.month as'Month', a.year as 'Year', a.allocation as 'Allocation', a.modified_by as 'Modified By', a.modified_datetime as 'Modified DateTime', a.archieve AS 'Archive' FROM audit_allocation_master a LEFT JOIN allocation_master m ON a.SOEID = m.SOEID and a.PTSID=m.PTSID  where a.modified_datetime>='" + startDate+"' AND a.modified_datetime<='"+endDate+"';";
                    cmd = new MySqlCommand(sql, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataTable);
                    dataGridViewam.DataSource = dataTable;
                    dataGridViewrm.DataSource = dataTable;
                    dataGridViewpm.Visible = false;
                    dataGridViewam.Visible = true;
                    dataGridViewgm.Visible = false;
                    dataGridViewrm.Visible = false;
                    dataGridViewam.Refresh();

                    //DataGridViewColumn actionColumn = dataGridViewpm.Columns["Action"];
                    //dataGridViewpm.Columns.Remove(actionColumn);
                    //dataGridViewpm.Columns.Add(actionColumn);
                    //dataGridViewpm.Refresh();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Some Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }


            }
        }

        private void auditUserControl_Load(object sender, EventArgs e)
        {
           //projectMasterAuditUserControl.instance.SelectedStartDate = SelectedStartDate;
           // projectMasterAuditUserControl.instance.selectedEndDate = selectedEndDate;
        }
    }
}
