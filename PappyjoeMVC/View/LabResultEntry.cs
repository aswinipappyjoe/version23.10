using PappyjoeMVC.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PappyjoeMVC.View
{
    public partial class LabResultEntry : Form
    {
        public LabResultEntry()
        {
            InitializeComponent();
        }
        int j;
        LabResultEntry_controller ctrlr=new LabResultEntry_controller();
        public string patient_id = "", doctor_id = "", workid = "",flag="",flagup="";
        private void LabResultEntry_Shown(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                //dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[8];
                //dataGridView1.CurrentCell.Selected = true;
                //dataGridView1.BeginEdit(true);
            }
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)//8,11,12,13
                {
                    j = this.ctrlr.rsltupdate(dataGridView1.Rows[i].Cells["result"].Value.ToString(), dataGridView1.Rows[i].Cells["typeid"].Value.ToString(), dataGridView1.Rows[i].Cells["id"].Value.ToString(), dataGridView1.Rows[i].Cells["typemainid"].Value.ToString());
                }
                if (j > 0)
                {
                    this.ctrlr.update_lab_status(workid, patient_id); 
                    MessageBox.Show("Updated Sucessfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void LabResultEntry_Load(object sender, EventArgs e)
        {
            try
            {
                
                if (flagup == "1")
                {
                    DataTable dt_main = this.ctrlr.load_main_value(patient_id, workid);
                    if(dt_main.Rows.Count>0)
                    {
                        dataGridView1.Rows.Clear();
                           DataTable dt_result = this.ctrlr.load_result_value(patient_id, workid);
                        if(dt_result.Rows.Count>0)
                        {
                            for(int i=0;i<dt_result.Rows.Count;i++ )
                            {
                                dataGridView1.Rows.Add();
                                dataGridView1.Rows[i].Cells["id"].Value= dt_result.Rows[i]["id"].ToString();
                                dataGridView1.Rows[i].Cells["result"].Value = dt_result.Rows[i]["results"].ToString(); 
                                dataGridView1.Rows[i].Cells["unit"].Value = dt_result.Rows[i]["Units"].ToString();
                                dataGridView1.Rows[i].Cells["normal"].Value = dt_result.Rows[i]["Normalvalue"].ToString();
                                dataGridView1.Rows[i].Cells["typemainid"].Value = dt_result.Rows[i]["Resultmain_id"].ToString();

                                DataTable dt_maintest = this.ctrlr.get_maintest(dt_result.Rows[i]["maintest_id"].ToString());
                                if(dt_maintest.Rows.Count>0)
                                {
                                    dataGridView1.Rows[i].Cells["department"].Value = dt_maintest.Rows[0]["Main_test"].ToString();
                                }
                                DataTable dt_testid = this.ctrlr.get_testid(dt_result.Rows[i]["test_id"].ToString());
                               if( dt_testid.Rows.Count>0)
                                {
                                    dataGridView1.Rows[i].Cells["typeid"].Value = dt_testid.Rows[0]["id"].ToString();
                                    dataGridView1.Rows[i].Cells["test"].Value = dt_testid.Rows[0]["Name"].ToString();
                                    DataTable dt_testtype= this.ctrlr.get_testtype(dt_testid.Rows[0]["TestTypeID"].ToString());
                                    if (dt_testtype.Rows.Count > 0)
                                    {
                                        dataGridView1.Rows[i].Cells["testtype"].Value = dt_testtype.Rows[0]["Name"].ToString();

                                    }
                                    //DataTable dt_tes_temp = this.ctrlr.get_testemplate(dt_testid.Rows[0]["id"].ToString());
                                    //if (dt_tes_temp.Rows.Count > 0)
                                    //{

                                    //}
                                }
                            }
                        }
                    }
                    //DataTable dt = this.ctrlr.LoadResult(patient_id, workid);
                    ////dataGridView1.DataSource = dt;
                    dataGridView1.Enabled = false;
                    btnsave.Visible = false;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        //dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[8];
                        //dataGridView1.CurrentCell.Selected = false;
                        dataGridView1.Rows[i].Cells[8].Style.BackColor = Color.White;
                        dataGridView1.Rows[i].Cells[8].Style.ForeColor = Color.Black;
                    }
                }
                if (flag == "1")
                {
                    DataTable dt_main = this.ctrlr.load_main_value(patient_id, workid);
                    if (dt_main.Rows.Count > 0)
                    {
                        dataGridView1.Rows.Clear();
                        DataTable dt_result = this.ctrlr.load_result_value(patient_id, workid);
                        if (dt_result.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt_result.Rows.Count; i++)//
                            {
                                dataGridView1.Rows.Add();
                                dataGridView1.Rows[i].Cells["id"].Value = dt_result.Rows[i]["id"].ToString();
                                dataGridView1.Rows[i].Cells["result"].Value = dt_result.Rows[i]["results"].ToString();
                                dataGridView1.Rows[i].Cells["unit"].Value = dt_result.Rows[i]["Units"].ToString();
                                dataGridView1.Rows[i].Cells["normal"].Value = dt_result.Rows[i]["Normalvalue"].ToString();
                                dataGridView1.Rows[i].Cells["typemainid"].Value = dt_result.Rows[i]["Resultmain_id"].ToString();

                                DataTable dt_maintest = this.ctrlr.get_maintest(dt_result.Rows[i]["maintest_id"].ToString());
                                if (dt_maintest.Rows.Count > 0)
                                {
                                    dataGridView1.Rows[i].Cells["department"].Value = dt_maintest.Rows[0]["Main_test"].ToString();
                                }
                                DataTable dt_testid = this.ctrlr.get_testid(dt_result.Rows[i]["test_id"].ToString());
                                if (dt_testid.Rows.Count > 0)
                                {
                                    dataGridView1.Rows[i].Cells["typeid"].Value = dt_testid.Rows[0]["id"].ToString();
                                    dataGridView1.Rows[i].Cells["test"].Value = dt_testid.Rows[0]["Name"].ToString();
                                    DataTable dt_testtype = this.ctrlr.get_testtype(dt_testid.Rows[0]["TestTypeID"].ToString());
                                    if (dt_testtype.Rows.Count > 0)
                                    {
                                        dataGridView1.Rows[i].Cells["testtype"].Value = dt_testtype.Rows[0]["Name"].ToString();

                                    }
                                    //DataTable dt_tes_temp = this.ctrlr.get_testemplate(dt_testid.Rows[0]["id"].ToString());
                                    //if (dt_tes_temp.Rows.Count > 0)
                                    //{

                                    //}
                                }
                            }
                        }
                    }


                    //DataTable dt = this.ctrlr.LoadResult(patient_id, workid);
                    //dataGridView1.DataSource = dt;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        //this.dataGridView1.Columns["typeid"].Visible = false;//11
                        //this.dataGridView1.Columns["update"].Visible = false;//12
                        //this.dataGridView1.Columns["delete"].Visible = false;//13
                        //dataGridView1.Rows[i].Cells[8].Value = "";
                    }
                }
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                dataGridView1.EnableHeadersVisualStyles = false;
                foreach (DataGridViewColumn cl in dataGridView1.Columns)
                {
                    cl.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
