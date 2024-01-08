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
    public partial class Log_details : Form
    {
       
        public string uname;
        public string type;
        public string id;
        
        public Log_details()
        {
            InitializeComponent();
        }
        Patients_controller cntrl = new Patients_controller();

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Log_details_Load(object sender, EventArgs e)
        {
          
            DataTable log_dtls = this.cntrl.pur_log_detail(id);
            if (log_dtls.Rows.Count >0)
            {
                dgv_log_details.Rows.Clear();
                for (int i = 0; i < log_dtls.Rows.Count; i++)
                {
                    dgv_log_details.Rows.Add();
                    dgv_log_details.Rows[i].Cells["c_uname"].Value = log_dtls.Rows[i]["doctor_name"].ToString(); 
                    dgv_log_details.Rows[i].Cells["c_ltype"].Value = log_dtls.Rows[i]["log_type"].ToString(); 



                    if (log_dtls.Rows[i]["log_type"].ToString() == "Purchase")
                    {

                        dgv_log_details.Columns["c_id"].HeaderText = "Purchase Number";
                        dgv_log_details.Rows[i].Cells["c_id"].Value = log_dtls.Rows[i]["log_type_id"].ToString();
                    }
                    else if (log_dtls.Rows[i]["log_type"].ToString() == "Purchase Return")
                    {

                        dgv_log_details.Columns["c_id"].HeaderText = "Purchase Return Number";
                        dgv_log_details.Rows[0].Cells["c_id"].Value = log_dtls.Rows[i]["log_type_id"].ToString();
                    }
                    else if (log_dtls.Rows[i]["log_type"].ToString() == "Purchase Order")
                    {

                        dgv_log_details.Columns["c_id"].HeaderText = "Purchase Order Number";
                        dgv_log_details.Rows[0].Cells["c_id"].Value = log_dtls.Rows[i]["log_type_id"].ToString();
                    }
                    else if (log_dtls.Rows[i]["log_type"].ToString() == "Sales")
                    {

                        dgv_log_details.Columns["c_id"].HeaderText = "Invoice Number";
                        dgv_log_details.Rows[0].Cells["c_id"].Value = log_dtls.Rows[i]["log_type_id"].ToString();
                    }
                    else if (log_dtls.Rows[i]["log_type"].ToString() == "Sales Order")
                    {

                        dgv_log_details.Columns["c_id"].HeaderText = "Document Number";
                      dgv_log_details.Rows[0].Cells["c_id"].Value = log_dtls.Rows[i]["log_type_id"].ToString();
                    }
                    else if (log_dtls.Rows[i]["log_type"].ToString() == "Sales Return")
                    {

                        dgv_log_details.Columns["c_id"].HeaderText = "Invoice Number";
                        dgv_log_details.Rows[0].Cells["c_id"].Value = log_dtls.Rows[i]["log_type_id"].ToString();
                    }

                    else if (log_dtls.Rows[i]["log_type"].ToString() == "Finished Procedure")
                    {

                        dgv_log_details.Columns["c_id"].HeaderText = "Patient Id";
                        dgv_log_details.Rows[0].Cells["c_id"].Value = log_dtls.Rows[i]["log_type_id"].ToString();
                    }
                    else if (log_dtls.Rows[i]["log_type"].ToString() == "Lab Work")
                    {

                        dgv_log_details.Columns["c_id"].HeaderText = "Patient Id";
                        dgv_log_details.Rows[0].Cells["c_id"].Value = log_dtls.Rows[i]["log_type_id"].ToString();
                    }
                    else if (log_dtls.Rows[i]["log_type"].ToString() == "Patient")
                    {

                        dgv_log_details.Columns["c_id"].HeaderText = "Patient id";
                       dgv_log_details.Rows[0].Cells["c_id"].Value = log_dtls.Rows[i]["log_type_id"].ToString();
                    }
                    else if (log_dtls.Rows[i]["log_type"].ToString() == "Nurses Notes")
                    {

                        dgv_log_details.Columns["c_id"].HeaderText = "Patient Id";
                        dgv_log_details.Rows[0].Cells["c_id"].Value = log_dtls.Rows[i]["log_type_id"].ToString();
                    }
                    else if (log_dtls.Rows[i]["log_type"].ToString() == "Receipt")
                    {

                        dgv_log_details.Columns["c_id"].HeaderText = "Patient Id";
                        dgv_log_details.Rows[0].Cells["c_id"].Value = log_dtls.Rows[i]["log_type_id"].ToString();
                    }
                    else if (log_dtls.Rows[i]["log_type"].ToString() == "Treatment Plan")
                    {

                        dgv_log_details.Columns["c_id"].HeaderText = "Patient Id";
                        dgv_log_details.Rows[0].Cells["c_id"].Value = log_dtls.Rows[i]["log_type_id"].ToString();
                    }

                    else if (log_dtls.Rows[i]["log_type"].ToString() == "Vital Sign")
                    {

                        dgv_log_details.Columns["c_id"].HeaderText = "Patient Id";
                        dgv_log_details.Rows[0].Cells["c_id"].Value = log_dtls.Rows[i]["log_type_id"].ToString();
                    }
                    else if (log_dtls.Rows[i]["log_type"].ToString() == "Invoice")
                    {

                        dgv_log_details.Columns["c_id"].HeaderText = "Bill Number";
                        dgv_log_details.Rows[0].Cells["c_id"].Value = log_dtls.Rows[i]["log_type_id"].ToString();
                    }
                    else if (log_dtls.Rows[i]["log_type"].ToString() == "Appointment")
                    {

                        dgv_log_details.Columns["c_id"].HeaderText = "Appointment Id";
                        dgv_log_details.Rows[0].Cells["c_id"].Value = log_dtls.Rows[i]["log_type_id"].ToString();
                    }
                    else if (log_dtls.Rows[i]["log_type"].ToString() == "Attachments")
                    {

                        dgv_log_details.Columns["c_id"].HeaderText = "Patient Id";
                        dgv_log_details.Rows[0].Cells["c_id"].Value = log_dtls.Rows[i]["log_type_id"].ToString();
                    }
                    else if (log_dtls.Rows[i]["log_type"].ToString() == "Clinical Findings")
                    {

                        dgv_log_details.Columns["c_id"].HeaderText = "Patient Id";//
                        dgv_log_details.Rows[0].Cells["c_id"].Value = log_dtls.Rows[i]["log_type_id"].ToString();
                    }
                    else if (log_dtls.Rows[i]["log_type"].ToString() == "Consultation")
                    {

                        dgv_log_details.Columns["c_id"].HeaderText = "Patient_Id";
                        dgv_log_details.Rows[0].Cells["c_id"].Value = log_dtls.Rows[i]["log_type_id"].ToString();
                    }


                    else if (log_dtls.Rows[i]["log_type"].ToString() == "Prescription")
                    {

                        dgv_log_details.Columns["c_id"].HeaderText = "Patient Id";
                        dgv_log_details.Rows[0].Cells["c_id"].Value = log_dtls.Rows[i]["log_type_id"].ToString();
                    }
                    else if (log_dtls.Rows[i]["log_type"].ToString() == "Stock Transfer")
                    {

                        dgv_log_details.Columns["c_id"].HeaderText = "Reference Number";
                        dgv_log_details.Rows[0].Cells["c_id"].Value = log_dtls.Rows[i]["log_type_id"].ToString();
                    }
                    else if (log_dtls.Rows[i]["log_type"].ToString() == "Stock Adjustment")
                    {

                        dgv_log_details.Columns["c_id"].HeaderText = "Item Id";
                        dgv_log_details.Rows[0].Cells["c_id"].Value = log_dtls.Rows[i]["log_type_id"].ToString();
                    }
                    else if (log_dtls.Rows[i]["log_type"].ToString() == "Add Drug")
                    {

                        dgv_log_details.Columns["c_id"].HeaderText = "Drug Name";
                        dgv_log_details.Rows[0].Cells["c_id"].Value = log_dtls.Rows[i]["log_type_id"].ToString();
                    }
                    



                }
            }
        }
    }
}
