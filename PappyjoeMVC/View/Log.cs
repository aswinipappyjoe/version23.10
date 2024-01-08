using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PappyjoeMVC.Controller;

namespace PappyjoeMVC.View
{
    public partial class Log : Form
    {
        public Log()
        {
            InitializeComponent();
        }

        Patients_controller cntrl = new Patients_controller(); public int i;
        public string doctor_id = "";
        private void Log_Load(object sender, EventArgs e)
        {
            cmb_action.Items.Add("All Actions");
            cmb_action.ValueMember = "0";
            cmb_action.DisplayMember = "All Actions";
            DataTable dt_action = this.cntrl.log_action();
            if(dt_action.Rows.Count>0)
            {
                for(int i=0;i<dt_action.Rows.Count;i++)
                {
                    cmb_action.Items.Add(dt_action.Rows[i][0].ToString());
                }
                cmb_action.SelectedIndex = 0;
            }

            DataTable rs_log = this.cntrl.log_details();
            fill(rs_log);
            foreach (DataGridViewColumn column in DGV_Log.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            DGV_Log.EnableHeadersVisualStyles = false;
            DGV_Log.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            DGV_Log.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DGV_Log.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            DGV_Log.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            DGV_Log.ColumnHeadersVisible = true;
            DGV_Log.ScrollBars = ScrollBars.Vertical;
        }
        public void fill(DataTable rs_log)
        {
            if (rs_log.Rows.Count > 0)
            {
                DGV_Log.Rows.Clear();
                for (int i = 0; i < rs_log.Rows.Count; i++)
                {
                    DataTable dt_date = this.cntrl.log_date(rs_log.Rows[i]["log_id"].ToString());
                    DGV_Log.Rows.Add();
                    DGV_Log.Rows[i].Cells["log_id"].Value = rs_log.Rows[i]["log_id"].ToString();
                    DGV_Log.Rows[i].Cells["user_id"].Value = rs_log.Rows[i]["doctor_name"].ToString();
                    DGV_Log.Rows[i].Cells["log_type"].Value = rs_log.Rows[i]["log_type"].ToString();
                    DGV_Log.Rows[i].Cells["log_Description"].Value = rs_log.Rows[i]["log_description"].ToString();
                    if(dt_date.Rows.Count>0)
                    {
                        DGV_Log.Rows[i].Cells["date"].Value = rs_log.Rows[i]["date"].ToString();
                        DGV_Log.Rows[i].Cells["time"].Value = rs_log.Rows[i]["Time"].ToString();
                    }
                  
                    DGV_Log.Rows[i].Cells["log_stage"].Value = rs_log.Rows[i]["log_stage"].ToString();
                }
            }
            else
            {
                DGV_Log.Rows.Clear();
            }
            
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chk_username_Click(object sender, EventArgs e)
        {
        }

        private void chk_action_Click(object sender, EventArgs e)
        {
           
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
          if (txtsearch.Text != "")
            {
                    DataTable rs_log = this.cntrl.log_search(txtsearch.Text);
                    fill(rs_log);
            }
            else
            {
                DataTable rs_log = this.cntrl.log_details();
                fill(rs_log);
            }
              
        }

        private void cmb_action_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmb_action.SelectedIndex>=0)
            {
                if (txtsearch.Text != "")
                {
                    if(cmb_action.Text== "All Actions")
                    {
                        DataTable rs_log = this.cntrl.log_search(txtsearch.Text);
                        fill(rs_log);
                    }
                    else
                    {
                        DataTable rs_log = this.cntrl.log_search_wit_action(txtsearch.Text, cmb_action.Text);
                        fill(rs_log);
                    }
                    
                }
                else
                {
                    if (cmb_action.Text == "All Actions")
                    {
                        DataTable rs_log = this.cntrl.log_details();
                        fill(rs_log);
                    }
                    else
                    {
                        DataTable rs_log = this.cntrl.log_search_action(cmb_action.Text);
                        fill(rs_log);
                    }
                       
                } 
            }
            else
            {
                DataTable rs_log = this.cntrl.log_details();
                fill(rs_log);
            }   
        }
        int rowcount = 0;
        private void lb_showmore_Click(object sender, EventArgs e)
        {
            rowcount = DGV_Log.Rows.Count;
            int count = rowcount + 100;
            DataTable dt_load = this.cntrl.log_details_count(count.ToString());
            fill(dt_load);
        }

        private void DGV_Log_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            { 
            var frm = new Log_details();
            frm.id = DGV_Log.Rows[e.RowIndex].Cells["log_id"].Value.ToString();
            frm.ShowDialog();
            frm.Dispose();
            }
        }

       
    }
}
