using PappyjoeMVC.Controller;
using PappyjoeMVC.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Forms;
namespace PappyjoeMVC.View
{
    public partial class Dental_Treatment_Plans : Form
    {
        Treatment_controller cntrl = new Treatment_controller();
        Show_Appointment_controller Apptmt_cntrl = new Show_Appointment_controller();
        Vital_Signs_controller vital_cntrl = new Vital_Signs_controller();
        Clinical_Findings_controller clinical_cntrl = new Clinical_Findings_controller();
        Finished_Procedre_controller fnsdtrt_cntrl = new Finished_Procedre_controller();
        Prescription_Show_controller prescr_cntrl = new Prescription_Show_controller();
        LabWorks_controller lab_cntrl = new LabWorks_controller();
        Attachments_controller attach_cntrl = new Attachments_controller();
        Invoice_controller invo_cntrl = new Invoice_controller();
        Receipt_controller recpt_cntrl = new Receipt_controller();
        Connection db = new Connection();
        public string doctor_id = "";
        public string patient_id = "";
        string logo_name = "";
        string path = "";
        int btnEnabled = 0;
        string treatment_plan_id = "0";
        public string toothprt = "";
        public Dental_Treatment_Plans()
        {
            InitializeComponent();
        }
        public void SetController(Treatment_controller controller)
        {
            cntrl = controller;
        }
        private void TreatmentPlans_Load(object sender, EventArgs e)
        {
            if (PappyjoeMVC.Model.GlobalVariables.Subscription == "Silver")
            {
                labelfinished.Enabled = false;
                labelprescription.Enabled = false;
                labl_Lab.Enabled = false;
                labelattachment.Enabled = false;
                labelinvoice.Enabled = false;
                labelpayment.Enabled = false;
                labelledger.Enabled = false;
                if (doctor_id != "1")
                {
                    string privid;
                    privid = this.cntrl.check_privillege(doctor_id);
                    if (int.Parse(privid) < 0)
                    {
                        BtnAdd.Enabled = false;
                    }
                    else
                    {
                        BtnAdd.Enabled = true;
                    }
                    //..Edit
                    privid = this.cntrl.check_edit_privillege(doctor_id);
                    if (int.Parse(privid) < 0)
                    {
                        editToolStripMenuItem1.Enabled = false;
                    }
                    else
                    {
                        editToolStripMenuItem1.Enabled = true;
                    }
                    //Delete
                    privid = this.cntrl.delete_privillege(doctor_id);
                    if (int.Parse(privid) < 0)
                    {
                        deleteToolStripMenuItem1.Enabled = false;
                    }
                    else
                    {
                        deleteToolStripMenuItem1.Enabled = true;
                    }
                }
                DataTable clinicname = this.cntrl.Get_CompanyNAme();
                if (clinicname.Rows.Count > 0)
                {
                    string clinicn = "";
                    clinicn = clinicname.Rows[0][0].ToString();
                    path = clinicname.Rows[0]["path"].ToString();
                    string docnam = this.cntrl.Get_DoctorName(doctor_id);
                    if (path != "")
                    {
                        string curFile = this.cntrl.server() + "\\Pappyjoe_utilities\\Logo\\" + path;
                        if (File.Exists(curFile))
                        {
                            logo_name = "";
                            logo_name = path;
                            string Apppath = System.IO.Directory.GetCurrentDirectory();
                            if (!File.Exists(Apppath + "\\" + logo_name))
                            {
                                System.IO.File.Copy(curFile, Apppath + "\\" + logo_name);
                            }
                        }
                        else
                        {
                            logo_name = "";
                        }
                    }
                }
                System.Data.DataTable rs_patients = this.cntrl.Get_Patient_Details(patient_id);
                if (rs_patients.Rows[0]["pt_name"].ToString() != "")
                {
                    linkLabel_Name.Text = rs_patients.Rows[0]["pt_name"].ToString();
                }
                if (rs_patients.Rows[0]["pt_id"].ToString() != "")
                {
                    linkLabel_id.Text = rs_patients.Rows[0]["pt_id"].ToString();
                }
                dataGridView1_treatment_paln.ColumnHeadersVisible = false;
                dataGridView1_treatment_paln.RowHeadersVisible = false;
                dataGridView1_treatment_paln.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1_treatment_paln.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1_treatment_paln.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1_treatment_paln.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                DataTable dtb = this.cntrl.get_treatments(patient_id);
                load_treatment(dtb);
                appointment_count();
                vitals_count();
                clinical_count();
                treatment_count(); // nurse_count();
            }
            else
            {
                if (doctor_id != "1")
                {
                    string privid;
                    privid = this.cntrl.check_privillege(doctor_id);
                    if (int.Parse(privid) < 0)
                    {
                        BtnAdd.Enabled = false;
                        pb_trtmntAdd.Enabled = false;
                    }
                    else
                    {
                        BtnAdd.Enabled = true;
                        pb_trtmntAdd.Enabled = true;
                    }
                    //..Edit
                    privid = this.cntrl.check_edit_privillege(doctor_id);
                    if (int.Parse(privid) < 0)
                    {
                        editToolStripMenuItem1.Enabled = false;
                    }
                    else
                    {
                        editToolStripMenuItem1.Enabled = true;
                    }
                    //Delete
                    privid = this.cntrl.delete_privillege(doctor_id);
                    if (int.Parse(privid) < 0)
                    {
                        deleteToolStripMenuItem1.Enabled = false;
                    }
                    else
                    {
                        deleteToolStripMenuItem1.Enabled = true;
                    }
                }
                DataTable clinicname = this.cntrl.Get_CompanyNAme();
                if (clinicname.Rows.Count > 0)
                {
                    string clinicn = "";
                    clinicn = clinicname.Rows[0][0].ToString();
                    path = clinicname.Rows[0]["path"].ToString();
                    string docnam = this.cntrl.Get_DoctorName(doctor_id);
                    if (path != "")
                    {
                        string curFile = this.cntrl.server() + "\\Pappyjoe_utilities\\Logo\\" + path;
                        if (File.Exists(curFile))
                        {
                            logo_name = "";
                            logo_name = path;
                            string Apppath = System.IO.Directory.GetCurrentDirectory();
                            if (!File.Exists(Apppath + "\\" + logo_name))
                            {
                                System.IO.File.Copy(curFile, Apppath + "\\" + logo_name);
                            }
                        }
                        else
                        {
                            logo_name = "";
                        }
                    }
                }
                System.Data.DataTable rs_patients = this.cntrl.Get_Patient_Details(patient_id);
                if (rs_patients.Rows[0]["pt_name"].ToString() != "")
                {
                    linkLabel_Name.Text = rs_patients.Rows[0]["pt_name"].ToString();
                }
                if (rs_patients.Rows[0]["pt_id"].ToString() != "")
                {
                    linkLabel_id.Text = rs_patients.Rows[0]["pt_id"].ToString();
                }
                dataGridView1_treatment_paln.ColumnHeadersVisible = false;
                dataGridView1_treatment_paln.RowHeadersVisible = false;
                dataGridView1_treatment_paln.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1_treatment_paln.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1_treatment_paln.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1_treatment_paln.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                DataTable dtb = this.cntrl.get_treatments(patient_id);
                load_treatment(dtb);
                appointment_count();
                vitals_count();
                clinical_count();
                treatment_count();
                fnsdtrt_count();
                prescr_count();
                lab_count();
                attach_count();
                invoice_count();
                reciept_count();
                //nurse_count();
            }
            //if (doctor_id != "1")
            //{
            //    string privid;
            //    privid = this.cntrl.check_privillege(doctor_id);
            //    if (int.Parse(privid) > 0)
            //    {
            //        BtnAdd.Enabled = false;
            //    }
            //    else
            //    {
            //        BtnAdd.Enabled = true;
            //    }
            //    //..Edit
            //    privid = this.cntrl.check_edit_privillege(doctor_id);
            //    if (int.Parse(privid) > 0)
            //    {
            //        editToolStripMenuItem1.Enabled = false;
            //    }
            //    else
            //    {
            //        editToolStripMenuItem1.Enabled = true;
            //    }
            //    //Delete
            //    privid = this.cntrl.delete_privillege(doctor_id);
            //    if (int.Parse(privid) > 0)
            //    {
            //        deleteToolStripMenuItem1.Enabled = false;
            //    }
            //    else
            //    {
            //        deleteToolStripMenuItem1.Enabled = true;
            //    }
            //}
            //DataTable clinicname = this.cntrl.Get_CompanyNAme();
            //if (clinicname.Rows.Count > 0)
            //{
            //    string clinicn = "";
            //    clinicn = clinicname.Rows[0][0].ToString();
            //    path = clinicname.Rows[0]["path"].ToString();
            //    string docnam = this.cntrl.Get_DoctorName(doctor_id);
            //    if (path != "")
            //    {
            //        string curFile = this.cntrl.server() + "\\Pappyjoe_utilities\\Logo\\" + path;
            //        if (File.Exists(curFile))
            //        {
            //            logo_name = "";
            //            logo_name = path;
            //            string Apppath = System.IO.Directory.GetCurrentDirectory();
            //            if (!File.Exists(Apppath + "\\" + logo_name))
            //            {
            //                System.IO.File.Copy(curFile, Apppath + "\\" + logo_name);
            //            }
            //        }
            //        else
            //        {
            //            logo_name = "";
            //        }
            //    }
            //}
            //System.Data.DataTable rs_patients = this.cntrl.Get_Patient_Details(patient_id);
            //if (rs_patients.Rows[0]["pt_name"].ToString() != "")
            //{
            //    linkLabel_Name.Text = rs_patients.Rows[0]["pt_name"].ToString();
            //}
            //if (rs_patients.Rows[0]["pt_id"].ToString() != "")
            //{
            //    linkLabel_id.Text = rs_patients.Rows[0]["pt_id"].ToString();
            //}
            //dataGridView1_treatment_paln.ColumnHeadersVisible = false;
            //dataGridView1_treatment_paln.RowHeadersVisible = false;
            //dataGridView1_treatment_paln.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1_treatment_paln.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1_treatment_paln.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1_treatment_paln.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //DataTable dtb = this.cntrl.get_treatments(patient_id);
            //load_treatment(dtb);
            //appointment_count();
            //vitals_count();
            //clinical_count();
            //treatment_count();
            //fnsdtrt_count();
            //prescr_count();
            //lab_count();
            //attach_count();
            //invoice_count();
            //reciept_count();
        }
        public void appointment_count()
        {
            DataTable dt = this.Apptmt_cntrl.show(patient_id);
            if (dt.Rows.Count > 0)
            {
                lb_appmnt_cnt.Text = dt.Rows.Count.ToString();
            }
        }
        public void vitals_count()
        {
            DataTable vital = this.vital_cntrl.vital(patient_id);
            if (vital.Rows.Count > 0)
            {
                lb_Vitals_cnt.Text = vital.Rows.Count.ToString();
            }
        }
        public void clinical_count()
        {
            System.Data.DataTable dt_cf_main = this.clinical_cntrl.dt_cf_main(patient_id);
            if (dt_cf_main.Rows.Count > 0)
            {
                lb_clinical_cnt.Text = dt_cf_main.Rows.Count.ToString();
            }
        }
        public void treatment_count()
        {
            DataTable dtb = this.cntrl.get_treatments(patient_id);
            if (dtb.Rows.Count > 0)
            {
                lb_trtment_cnt.Text = dtb.Rows.Count.ToString();
            }
        }
        public void fnsdtrt_count()
        {
            DataTable dtb = this.fnsdtrt_cntrl.get_completed_id_date(patient_id);
            if (dtb.Rows.Count > 0)
            {
                lb_finisdtrt_cnt.Text = dtb.Rows.Count.ToString();
            }
        }
        public void prescr_count()
        {
            DataTable dt_pre_main = this.prescr_cntrl.Get_maindtails(patient_id);
            if (dt_pre_main.Rows.Count > 0)
            {
                lb_prescr_cnt.Text = dt_pre_main.Rows.Count.ToString();
            }
        }
        public void lab_count()
        {
            DataTable dt = this.lab_cntrl.Getdata(patient_id);
            if (dt.Rows.Count > 0)
            {
                lb_Lac_cnt.Text = dt.Rows.Count.ToString();
            }
        }
        public void attach_count()
        {
            DataTable attach = this.attach_cntrl.getattachment(patient_id);
            if (attach.Rows.Count > 0)
            {
                lb_Attchmnt_cnt.Text = attach.Rows.Count.ToString();
            }
        }
        public void invoice_count()
        {
            DataTable dt_invoice_main = this.invo_cntrl.Get_invoice_mainDetails(patient_id);
            if (dt_invoice_main.Rows.Count > 0)
            {
                lb_Invoice_cnt.Text = dt_invoice_main.Rows.Count.ToString();
            }
        }
        public void reciept_count()
        {
            DataTable Payments = db.table("select receipt_no,amount_paid,invoice_no,procedure_name,mode_of_payment from tbl_payment where pt_id='" + patient_id + "'");
            if (Payments.Rows.Count > 0)
            {
                lb_Reciepts_cnt.Text = Payments.Rows.Count.ToString();
            }
        }

        public void openform(Form myForm)
        {
            Panel p = this.Parent as Panel;
            if (p != null)
            {
                myForm.FormBorderStyle = FormBorderStyle.None;
                myForm.TopLevel = false;
                myForm.Dock = DockStyle.Fill;
                p.Controls.Add(myForm);
                myForm.Show();
                this.Close();
            }
        }
        public void load_treatment(DataTable dt_pt_main)
        {
            try
            {
                if (dt_pt_main.Rows.Count > 0)
                {
                    int i = 0;
                    for (int j = 0; j < dt_pt_main.Rows.Count; j++)
                    {
                        dataGridView1_treatment_paln.Rows.Add("0", "", String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(dt_pt_main.Rows[j]["date"].ToString())), "","", "", "", "", "", "0");
                        dataGridView1_treatment_paln.Rows[i].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                        dataGridView1_treatment_paln.Rows[i].Cells[2].Style.ForeColor = Color.DarkGreen;
                        dataGridView1_treatment_paln.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                        dataGridView1_treatment_paln.Rows[i].Cells["img"].Value = PappyjoeMVC.Properties.Resources.blank;
                        i = i + 1;
                        DataTable dt_enablecost = this.cntrl.get_enablecost();
                        if (dt_enablecost.Rows.Count > 0)
                        {
                            if (dt_enablecost.Rows[0][1].ToString() == "Yes")
                            {
                                dataGridView1_treatment_paln.Rows.Add(dt_pt_main.Rows[j]["id"].ToString(), "", "TREATMENTS", "TOOTH", "COST", "DISCOUNT", "TOTAL", "NOTE", "0", "");

                            }
                            else
                            {
                                dataGridView1_treatment_paln.Rows.Add(dt_pt_main.Rows[j]["id"].ToString(), "", "TREATMENTS", "TOOTH", "", "", "", "NOTE", "0", "");

                            }
                        }
                        else
                        {
                            dataGridView1_treatment_paln.Rows.Add(dt_pt_main.Rows[j]["id"].ToString(), "", "TREATMENTS", "TOOTH", "COST", "DISCOUNT", "TOTAL", "NOTE", "0", "");
                        }

                        //dataGridView1_treatment_paln.Rows.Add(dt_pt_main.Rows[j]["id"].ToString(), "", "TREATMENTS", "TOOTH", "COST", "DISCOUNT", "TOTAL", "NOTE", "0", "");//*******
                        dataGridView1_treatment_paln.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.gry;
                        dataGridView1_treatment_paln.Rows[i].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                        dataGridView1_treatment_paln.Rows[i].Cells[2].Style.ForeColor = Color.White;
                        dataGridView1_treatment_paln.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                        dataGridView1_treatment_paln.Rows[i].Cells[3].Style.ForeColor = Color.White;
                        dataGridView1_treatment_paln.Rows[i].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                        dataGridView1_treatment_paln.Rows[i].Cells[4].Style.ForeColor = Color.White;
                        dataGridView1_treatment_paln.Rows[i].Cells[5].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                        dataGridView1_treatment_paln.Rows[i].Cells[5].Style.ForeColor = Color.White;
                        dataGridView1_treatment_paln.Rows[i].Cells[6].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                        dataGridView1_treatment_paln.Rows[i].Cells[6].Style.ForeColor = Color.White;
                        dataGridView1_treatment_paln.Rows[i].Cells[7].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);//****
                        dataGridView1_treatment_paln.Rows[i].Cells[7].Style.ForeColor = Color.White;//*****
                        dataGridView1_treatment_paln.Rows[i].Cells[1].Style.BackColor = Color.DarkGray;
                        dataGridView1_treatment_paln.Rows[i].Cells[2].Style.BackColor = Color.DarkGray;
                        dataGridView1_treatment_paln.Rows[i].Cells[3].Style.BackColor = Color.DarkGray;
                        dataGridView1_treatment_paln.Rows[i].Cells[4].Style.BackColor = Color.DarkGray;
                        dataGridView1_treatment_paln.Rows[i].Cells[5].Style.BackColor = Color.DarkGray;
                        dataGridView1_treatment_paln.Rows[i].Cells[6].Style.BackColor = Color.DarkGray;
                        dataGridView1_treatment_paln.Rows[i].Cells[7].Style.BackColor = Color.DarkGray;//***
                        dataGridView1_treatment_paln.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridView1_treatment_paln.Rows[i].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridView1_treatment_paln.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridView1_treatment_paln.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                         dataGridView1_treatment_paln.Rows[i].Cells[9].Value = PappyjoeMVC.Properties.Resources.Bill;
                        DataTable dt_pt_sub = this.cntrl.treatment_sub_details(dt_pt_main.Rows[j]["id"].ToString());
                        Double totalEst = 0;//**********
                        for (int k = 0; k < dt_pt_sub.Rows.Count; k++)
                        {
                            string tooth_no = "";
                            string O = ""; string M = ""; string D = ""; string B = ""; string L = "";
                            DataTable dtb_procedure_tooth = this.cntrl.get_procedure_tooth(dt_pt_sub.Rows[k]["procedure_id"].ToString());
                            string tooth_folderNo = "", surface_folderNo = "";
                            if (dtb_procedure_tooth.Rows.Count > 0)
                            {
                                tooth_folderNo = dtb_procedure_tooth.Rows[0]["Tooth_Image"].ToString();
                                surface_folderNo = dtb_procedure_tooth.Rows[0]["Surface_Image"].ToString();
                            }
                            DataTable dt_toothrltn = this.cntrl.tooth_relation(patient_id, dt_pt_main.Rows[j]["id"].ToString(), dt_pt_sub.Rows[k]["procedure_id"].ToString());
                            for (int m = 0; m < dt_toothrltn.Rows.Count; m++)///image load
                            {
                                string tno = dt_toothrltn.Rows[m]["Tooth_Number"].ToString();
                                DataTable dtget_tooth = this.cntrl.tooth_relation_tooth(patient_id, dt_pt_main.Rows[j]["id"].ToString(), dt_pt_sub.Rows[k]["procedure_id"].ToString(), tno);
                                colour_tooth(tno, tooth_folderNo, surface_folderNo, dtget_tooth);
                                if (dt_toothrltn.Rows[m]["Occlusal"].ToString() == "Yes")
                                {
                                    O = "O,";
                                }
                                
                                if (dt_toothrltn.Rows[m]["Mesial"].ToString() == "Yes")
                                {
                                    M = "M,";
                                }
                                if (dt_toothrltn.Rows[m]["Distal"].ToString() == "Yes")
                                { 
                                    D = "D,";
                                }
                                if (dt_toothrltn.Rows[m]["Buccal"].ToString() == "Yes")
                                {
                                    B = "B,";
                                }
                                if (dt_toothrltn.Rows[m]["Lingual"].ToString() == "Yes")
                                {
                                    L = "L,";
                                }
                                string a = O + M + D + B + L;
                                if(a!="")
                                {
                                    a = a.Remove(a.Length - 1);
                                    tooth_no += tno + "(" + a + ")";
                                }
                               
                                O = ""; M = ""; D = ""; B = ""; L = "";
                            }
                            string discount_string = "";
                            if (dt_pt_sub.Rows[k]["discount_type"].ToString() == "INR")
                            {
                                discount_string = "";
                            }
                            else
                            {
                                discount_string = "(" + dt_pt_sub.Rows[k]["discount"].ToString() + "%)";
                            }
                            Decimal totalcost = Convert.ToDecimal(dt_pt_sub.Rows[k]["cost"].ToString()) * Convert.ToDecimal(dt_pt_sub.Rows[k]["quantity"].ToString());
                            i = i + 1; //procedure_id 
                            if (dt_enablecost.Rows.Count > 0)
                            {
                                if (dt_enablecost.Rows[0][1].ToString() == "Yes")
                                {//                                            1                               2            3                                                  4                                                          5                                                                                                             6                                                                                 7                                                                                 8                        9 10            
                                    dataGridView1_treatment_paln.Rows.Add(dt_pt_sub.Rows[k]["id"].ToString(), "", dt_pt_sub.Rows[k]["procedure_name"].ToString(), dt_pt_sub.Rows[k]["note"].ToString() + " " + dt_pt_sub.Rows[k]["tooth"].ToString(), String.Format("{0:C2}", Convert.ToDecimal(totalcost)), String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discount_inrs"].ToString())) + discount_string, String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString())),"", dt_pt_sub.Rows[k]["status"].ToString(),"");

                                }
                                else
                                {
                                    dataGridView1_treatment_paln.Rows.Add(dt_pt_sub.Rows[k]["id"].ToString(), "", dt_pt_sub.Rows[k]["procedure_name"].ToString(), "", "", "", dt_pt_sub.Rows[k]["note"].ToString() + " " + dt_pt_sub.Rows[k]["tooth"].ToString(),"", dt_pt_sub.Rows[k]["status"].ToString(),"");

                                }
                            }
                            else
                            {
                                dataGridView1_treatment_paln.Rows.Add(dt_pt_sub.Rows[k]["id"].ToString(), "", dt_pt_sub.Rows[k]["procedure_name"].ToString(), dt_pt_sub.Rows[k]["note"].ToString() + " " + dt_pt_sub.Rows[k]["tooth"].ToString(), String.Format("{0:C2}", Convert.ToDecimal(totalcost)), String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discount_inrs"].ToString())) + discount_string, String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString())),"",  dt_pt_sub.Rows[k]["status"].ToString(), "");
                            }

                            //dataGridView1_treatment_paln.Rows.Add(dt_pt_sub.Rows[k]["id"].ToString(), "", dt_pt_sub.Rows[k]["procedure_name"].ToString(), tooth_no, String.Format("{0:C}", Convert.ToDecimal(totalcost)), String.Format("{0:C}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discount_inrs"].ToString())) + discount_string, String.Format("{0:C}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString())), dt_pt_sub.Rows[k]["note"].ToString(), dt_pt_sub.Rows[k]["status"].ToString(), dt_pt_sub.Rows[k]["procedure_id"].ToString());
                            dataGridView1_treatment_paln.Rows[i].Height = 30;
                            dataGridView1_treatment_paln.Columns[8].Width = 200;
                            dataGridView1_treatment_paln.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dataGridView1_treatment_paln.Rows[i].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dataGridView1_treatment_paln.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dataGridView1_treatment_paln.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                            if (dt_pt_sub.Rows[k]["status"].ToString() == "0")
                            {
                                dataGridView1_treatment_paln.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.TickRed;
                            }
                            else
                            {
                                dataGridView1_treatment_paln.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.Borderblank;
                            }
                            totalEst = totalEst + Convert.ToDouble(dt_pt_sub.Rows[k]["total"].ToString());
                            dataGridView1_treatment_paln.Rows[i].Cells["img"].Value = PappyjoeMVC.Properties.Resources.blank;
                        }
                        i = i + 1;
                        //dataGridView1_treatment_paln.Rows.Add("", "", "Planned by " + dt_pt_main.Rows[j]["dr_name"].ToString(), "", "Estimated amount:", String.Format("{0:C}", Convert.ToDecimal(totalEst)), "", "", "0");
                        if (dt_enablecost.Rows.Count > 0)
                        {
                            if (dt_enablecost.Rows[0][0].ToString() == "Yes")
                            {
                                dataGridView1_treatment_paln.Rows.Add("", "", "Planned by " + dt_pt_main.Rows[j]["dr_name"].ToString(), "", "Estimated amount:", String.Format("{0:C2}", Convert.ToDecimal(totalEst)),"","", "", "","0");

                            }
                            else
                            {
                                dataGridView1_treatment_paln.Rows.Add("", "", "Planned by " + dt_pt_main.Rows[j]["dr_name"].ToString(), "", "", "", "","","", "0");

                            }
                        }
                        else
                        {
                            dataGridView1_treatment_paln.Rows.Add("", "", "Planned by " + dt_pt_main.Rows[j]["dr_name"].ToString(), "", "Estimated amount:", String.Format("{0:C2}", Convert.ToDecimal(totalEst)), "","","","0");
                        }
                        dataGridView1_treatment_paln.Rows[i].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                        dataGridView1_treatment_paln.Rows[i].Cells[5].Style.ForeColor = Color.Red;
                        dataGridView1_treatment_paln.Rows[i].Cells[5].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                        dataGridView1_treatment_paln.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                        dataGridView1_treatment_paln.Rows[i].Cells["img"].Value = PappyjoeMVC.Properties.Resources.blank;
                        dataGridView1_treatment_paln.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        i = i + 1;
                        dataGridView1_treatment_paln.Rows.Add("", "", "", "", "", "", "", "","", "0");
                        dataGridView1_treatment_paln.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                        dataGridView1_treatment_paln.Rows[i].Cells["img"].Value = PappyjoeMVC.Properties.Resources.blank;
                        i = i + 1;
                    }
                }
                if (dataGridView1_treatment_paln.Rows.Count <= 0)
                {
                    int x = (panel3.Size.Width - lab_NoRecordFound_AlertMsg.Size.Width) / 2;
                    lab_NoRecordFound_AlertMsg.Location = new Point(x, lab_NoRecordFound_AlertMsg.Location.Y);
                    lab_NoRecordFound_AlertMsg.Show();
                }
                else
                {
                    lab_NoRecordFound_AlertMsg.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !.....", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void colour_tooth(string tno, string tooth_folderNo, string surface_folderNo, DataTable dt_toothrltn)
        {
            string curFile = "", surface_file = ""; // image load
            if (tooth_folderNo != "" && tooth_folderNo !="0")
            {
                curFile = System.IO.Directory.GetCurrentDirectory() + "\\Toothimages" + "\\" + tooth_folderNo + "\\" + "F" + tno + ".gif";
            }
            else if (tooth_folderNo == "0")
            {
                curFile = System.IO.Directory.GetCurrentDirectory() + "\\Permanent" + "\\" + "F" + tno + ".gif";
            }
            if (surface_folderNo != "" && surface_folderNo != "0")
            {
                surface_file = System.IO.Directory.GetCurrentDirectory() + "\\Toothimages" + "\\" + surface_folderNo + "\\" + "O" + tno + ".gif";
            }
            else if (surface_folderNo == "0")
            {
                surface_file = System.IO.Directory.GetCurrentDirectory() + "\\Permanent" + "\\" + "O" + tno + ".gif";
            }
            if (System.IO.File.Exists(curFile))
            {
                Control[] contrl = this.Controls.Find("p" + tno, true);
                {
                    if (contrl != null && contrl.Length > 0)
                    {
                        foreach (Control control in contrl)
                        {
                            if (control.GetType() == typeof(PictureBox))
                            {
                                PictureBox pictureBox = control as PictureBox;
                                pictureBox.Image = Image.FromFile(curFile);
                            }
                        }
                    }
                }
            }
            if (System.IO.File.Exists(surface_file))
            {
                Control[] contrl1 = this.Controls.Find("s" + tno, true);
                {
                    if (contrl1 != null && contrl1.Length > 0)
                    {
                        foreach (Control control in contrl1)
                        {
                            if (control.GetType() == typeof(PictureBox))
                            {
                                PictureBox pictureBox = control as PictureBox;
                                pictureBox.Image = Image.FromFile(surface_file);
                            }
                        }
                    }
                }
            }
            if(dt_toothrltn.Rows.Count>0)
            {
                Control[] contrl2 = this.Controls.Find("_" + tno, true);
                {
                    if (contrl2 != null && contrl2.Length > 0)
                    {
                        foreach (Control control in contrl2)
                        {
                            if (control.GetType() == typeof(PictureBox))
                            {
                                PictureBox pictureBox = control as PictureBox;
                                Bitmap bmp = (Bitmap)pictureBox.Image;
                                if (dt_toothrltn.Rows[0]["Occlusal"].ToString() == "Yes")
                                {
                                    Point pt = new Point(110, 96);
                                    Color c0 = bmp.GetPixel(110, 96);
                                    Fill4(bmp, pt, c0, Color.CadetBlue, tno);
                                }
                                if (dt_toothrltn.Rows[0]["Mesial"].ToString() == "Yes")
                                {
                                    Point pt = new Point(31, 52);
                                    Color c0 = bmp.GetPixel(31, 52);
                                    Fill4(bmp, pt, c0, Color.CadetBlue, tno);
                                }
                                if (dt_toothrltn.Rows[0]["Distal"].ToString() == "Yes")
                                {
                                    Point pt = new Point(172, 52);
                                    Color c0 = bmp.GetPixel(172, 52);
                                    Fill4(bmp, pt, c0, Color.CadetBlue, tno);

                                }
                                if (dt_toothrltn.Rows[0]["Buccal"].ToString() == "Yes")
                                {
                                    Point pt = new Point(157, 43);
                                    Color c0 = bmp.GetPixel(157, 43);
                                    Fill4(bmp, pt, c0, Color.CadetBlue, tno);

                                }
                                if (dt_toothrltn.Rows[0]["Lingual"].ToString() == "Yes")
                                {
                                    Point pt = new Point(117, 157);
                                    Color c0 = bmp.GetPixel(117, 157);
                                    Fill4(bmp, pt, c0, Color.CadetBlue, tno);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void Fill4(Bitmap bmp, Point pt, Color c0, Color c1, string re_move)
        {
            Color cx = bmp.GetPixel(pt.X, pt.Y);
            Rectangle bmpRect = new Rectangle(Point.Empty, bmp.Size);
            Stack<Point> stack = new Stack<Point>();
            Stack<Point> stack1 = new Stack<Point>();
            int x0 = pt.X;
            int y0 = pt.Y;
            if (cx.GetBrightness() < 0.01f)
                return;
            else if (cx.GetBrightness() == 1.0f)
            {
                stack.Push(new Point(x0, y0));
                while (stack.Any())
                {
                    Point p = stack.Pop();
                    if (!bmpRect.Contains(p)) continue;
                    cx = bmp.GetPixel(p.X, p.Y);
                    if (cx == c0)
                    {
                        bmp.SetPixel(p.X, p.Y, c1);
                        stack.Push(new Point(p.X, p.Y + 1));
                        stack.Push(new Point(p.X, p.Y - 1));
                        stack.Push(new Point(p.X + 1, p.Y));
                        stack.Push(new Point(p.X - 1, p.Y));
                    }
                }
            }
        }

        private void dataGridView1_treatment_paln_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1_treatment_paln.Rows.Count > 0 && (dataGridView1_treatment_paln.Rows[e.RowIndex].Cells[8].Value.ToString() == "1" || dataGridView1_treatment_paln.Rows[e.RowIndex].Cells[8].Value.ToString() == "2"))
                {
                    if (e.ColumnIndex == 1)
                    {
                        if (dataGridView1_treatment_paln.Rows[e.RowIndex].Cells[8].Value.ToString() == "1")
                        {
                            btnEnabled = btnEnabled + 1;
                            dataGridView1_treatment_paln.Rows[e.RowIndex].Cells[8].Value = "2";
                            dataGridView1_treatment_paln.Rows[e.RowIndex].Cells[1].Value = PappyjoeMVC.Properties.Resources.Bordertick;
                        }
                        else if (dataGridView1_treatment_paln.Rows[e.RowIndex].Cells[8].Value.ToString() == "2")
                        {
                            dataGridView1_treatment_paln.Rows[e.RowIndex].Cells[8].Value = "1";
                            dataGridView1_treatment_paln.Rows[e.RowIndex].Cells[1].Value = PappyjoeMVC.Properties.Resources.Borderblank;
                            btnEnabled = btnEnabled - 1;
                        }
                        if (btnEnabled > 0)
                        {
                            BtnMarkAsFinished.Enabled = true;
                        }
                        else
                        {
                            BtnMarkAsFinished.Enabled = false;
                        }
                    }
                }
                if (dataGridView1_treatment_paln.Rows.Count > 0 && (dataGridView1_treatment_paln.Rows[e.RowIndex].Cells[2].Value.ToString() != ""))
                {
                    if (e.ColumnIndex == 2)
                    {
                        pb11.Visible = false; pb12.Visible = false; pb13.Visible = false; pb14.Visible = false; pb15.Visible = false; pb16.Visible = false; pb17.Visible = false; pb18.Visible = false; pb21.Visible = false; pb22.Visible = false; pb23.Visible = false; pb24.Visible = false; pb25.Visible = false; pb26.Visible = false; pb27.Visible = false; pb28.Visible = false; pb31.Visible = false; pb32.Visible = false;
                        pb33.Visible = false; pb34.Visible = false; pb35.Visible = false; pb36.Visible = false; pb37.Visible = false; pb38.Visible = false; pb41.Visible = false; pb42.Visible = false; pb43.Visible = false; pb44.Visible = false; pb45.Visible = false; pb46.Visible = false; pb47.Visible = false; pb48.Visible = false;
                        string a = dataGridView1_treatment_paln.Rows[e.RowIndex].Cells[3].Value.ToString();
                        string pro_name = dataGridView1_treatment_paln.Rows[e.RowIndex].Cells[2].Value.ToString();
                        string[] numbers = Regex.Split(a, @"\D+");
                        {
                            foreach (string item in numbers)
                            {
                                if (item != "")
                                {
                                    string c = "pb" + item;
                                    PictureBox p = (PictureBox)this.Controls.Find(c, true)[0];
                                    p.Name = c;
                                    p.Visible = true;//load clcked surface
                                    DataTable dtb_procedure_tooth = this.cntrl.get_procedureid(dataGridView1_treatment_paln.Rows[e.RowIndex].Cells["Procedure"].Value.ToString());
                                    string tooth_folderNo = "", surface_folderNo = "";
                                    if (dtb_procedure_tooth.Rows.Count > 0)
                                    {
                                        tooth_folderNo = dtb_procedure_tooth.Rows[0]["Tooth_Image"].ToString();
                                        surface_folderNo = dtb_procedure_tooth.Rows[0]["Surface_Image"].ToString();
                                    }
                                    DataTable dtb_planid = this.cntrl.get_planid(dataGridView1_treatment_paln.Rows[e.RowIndex].Cells["Procedure"].Value.ToString(), dataGridView1_treatment_paln.Rows[e.RowIndex].Cells["id"].Value.ToString(),patient_id);

                                    DataTable dt_toothrltn = this.cntrl.tooth_relation_tooth(patient_id, dtb_planid.Rows[0]["plan_main_id"].ToString(), dtb_procedure_tooth.Rows[0]["id"].ToString(), item);
                                    Control[] contrl2 = this.Controls.Find("_" + item, true);
                                    {
                                        if (contrl2 != null && contrl2.Length > 0)
                                        {
                                            foreach (Control control in contrl2)
                                            {
                                                if (control.GetType() == typeof(PictureBox))
                                                {
                                                    PictureBox pictureBox = control as PictureBox;
                                                    pictureBox.Image = PappyjoeMVC.Properties.Resources.New_Image;
                                                }
                                            }
                                        }
                                    }
                                    colour_tooth(item, tooth_folderNo, surface_folderNo, dt_toothrltn);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !.....", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_treatment_paln_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int currentMouseOverRow = dataGridView1_treatment_paln.HitTest(e.X, e.Y).RowIndex;
                int currentMouseOverColumn = dataGridView1_treatment_paln.HitTest(e.X, e.Y).ColumnIndex;
                if (currentMouseOverRow >= 0)
                {
                    if (currentMouseOverColumn == 9)
                    {
                        if (dataGridView1_treatment_paln.Rows[currentMouseOverRow].Cells[0].Value.ToString() != "0" && dataGridView1_treatment_paln.Rows[currentMouseOverRow].Cells[2].Value.ToString() == "TREATMENTS")
                        {
                            treatment_plan_id = dataGridView1_treatment_paln.Rows[currentMouseOverRow].Cells[0].Value.ToString();
                            contextMenuStrip1.Show(dataGridView1_treatment_paln, new System.Drawing.Point(6000 - 1800, e.Y));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !.....", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (doctor_id != "1")
                {
                    string id;
                    id = this.cntrl.delete_privillege(doctor_id);
                    if (int.Parse(id) > 0)
                    {
                        dlt_Privilage();
                    }
                    else
                    {
                        MessageBox.Show("There is No Privilege to Delete Treatment Plan", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    dlt_Privilage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !.....", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void dlt_Privilage()
        {
            if (treatment_plan_id != "0")
            {
                string curFile = "", surface_file = "";
                DialogResult res = MessageBox.Show("Are you sure you want to delete..?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    System.Data.DataTable dt_pt_check = this.cntrl.get_plan_id(treatment_plan_id);
                    if (dt_pt_check.Rows.Count > 0)
                    {
                        MessageBox.Show("Treatment Plan is used in procedures. Cannot Delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        this.cntrl.delete_treatment(treatment_plan_id);
                        dataGridView1_treatment_paln.RowCount = 0;
                        dataGridView1_treatment_paln.ColumnHeadersVisible = false;
                        dataGridView1_treatment_paln.RowHeadersVisible = false;
                        dataGridView1_treatment_paln.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridView1_treatment_paln.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridView1_treatment_paln.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridView1_treatment_paln.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DataTable dtb = this.cntrl.get_treatments(patient_id);
                        /////////////
                        DataTable dt_toothrltn = this.cntrl.Get_tooth_relation(patient_id, treatment_plan_id);
                        for (int m = 0; m < dt_toothrltn.Rows.Count; m++)
                        {
                            string tno = dt_toothrltn.Rows[m]["Tooth_Number"].ToString();

                            Control[] contrl = this.Controls.Find("p" + tno, true);
                            {
                                curFile = System.IO.Directory.GetCurrentDirectory() + "\\Permanent" + "\\" + "F" + tno + ".gif";
                                if (contrl != null && contrl.Length > 0)
                                {
                                    foreach (Control control in contrl)
                                    {
                                        if (control.GetType() == typeof(PictureBox))
                                        {
                                            PictureBox pictureBox = control as PictureBox;
                                            Graphics g1 = pictureBox.CreateGraphics();
                                            pictureBox.Image = Image.FromFile(curFile);
                                        }
                                    }
                                }
                            }
                            Control[] contrl1 = this.Controls.Find("s" + tno, true);
                            {
                                surface_file = System.IO.Directory.GetCurrentDirectory() + "\\Permanent" + "\\" + "O" + tno + ".gif";
                                if (contrl1 != null && contrl1.Length > 0)
                                {
                                    foreach (Control control in contrl1)
                                    {
                                        if (control.GetType() == typeof(PictureBox))
                                        {
                                            PictureBox pictureBox = control as PictureBox;
                                            Graphics g1 = pictureBox.CreateGraphics();
                                            pictureBox.Image = Image.FromFile(surface_file);
                                        }
                                    }
                                }
                            }
                            Control[] contrl2 = this.Controls.Find("_" + tno, true);
                            {
                                if (contrl2 != null && contrl2.Length > 0)
                                {
                                    foreach (Control control in contrl2)
                                    {
                                        if (control.GetType() == typeof(PictureBox))
                                        {
                                            PictureBox pictureBox = control as PictureBox;
                                            pictureBox.Image = PappyjoeMVC.Properties.Resources.New_Image;
                                        }
                                    }
                                }
                            }
                            Control[] contl = this.Controls.Find("pb" + tno, true);
                            {
                                if (contl != null && contl.Length > 0)
                                {
                                    foreach (Control control in contl)
                                    {
                                        if (control.GetType() == typeof(PictureBox))
                                        {
                                            PictureBox pictureBox = control as PictureBox;
                                            if (pictureBox.Visible == true)
                                                pictureBox.Visible = false;
                                        }
                                    }
                                }
                            }
                        }
                        this.cntrl.delete_tooth(treatment_plan_id);
                        load_treatment(dtb);
                    }
                }
            }
        }
        user_privillage_model privi_mdl = new user_privillage_model();
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id;
                id = privi_mdl.treatment_add(doctor_id);// this.treatmnt_cntrl.check_privillege(doctor_id);
                if (int.Parse(id) > 0)
                {
                    if (PappyjoeMVC.Model.Connection.MyGlobals.project_type.TrimEnd() == "Dental")
                    {
                        var form2 = new Add_Dental_Treatment_Plan();
                        form2.doctor_id = doctor_id;
                        form2.patient_id = patient_id;
                        openform(form2);
                    }
                    else
                    {
                        var form2 = new Add_Treatment_Plan();
                        form2.doctor_id = doctor_id;
                        form2.patient_id = patient_id;
                        openform(form2);
                    }

                }
                else
                {
                    MessageBox.Show("There is No Privilege to Add Treatment Plan", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                if (PappyjoeMVC.Model.Connection.MyGlobals.project_type.TrimEnd() == "Dental")
                {
                    var form2 = new Add_Dental_Treatment_Plan();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
                else
                {
                    var form2 = new Add_Treatment_Plan();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
            }
        }

        private void BtnMarkAsFinished_Click(object sender, EventArgs e)
        {
            try
            {
                if (doctor_id != "1")
                {
                    string id;
                    id = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRFP' and Permission='A'");
                    if (int.Parse(id) > 0)
                    {
                        int rec_count = 0;
                        string a_plan_id = "";
                        for (int i = 0; i < dataGridView1_treatment_paln.Rows.Count; i++)
                        {
                            if (dataGridView1_treatment_paln.Rows[i].Cells[8].Value.ToString() == "2")
                            {
                                rec_count = rec_count + 1;
                                a_plan_id = a_plan_id + "," + dataGridView1_treatment_paln.Rows[i].Cells[0].Value.ToString();
                            }
                        }
                        if (rec_count != 0)
                        {
                            a_plan_id = a_plan_id.Substring(1, a_plan_id.Length - 1);
                            var form2 = new Add_Finished_Procedure();
                            form2.doctor_id = doctor_id;
                            form2.patient_id = patient_id;
                            form2.treatment_plan = a_plan_id;
                            openform(form2);
                        }
                        else
                        {
                            MessageBox.Show("Please select the Treatment..!", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("There is No Privilege to add Finished Procedure", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    int rec_count = 0;
                    string a_plan_id = "";
                    for (int i = 0; i < dataGridView1_treatment_paln.Rows.Count; i++)
                    {
                        if (dataGridView1_treatment_paln.Rows[i].Cells[8].Value.ToString() == "2")
                        {
                            rec_count = rec_count + 1;
                            a_plan_id = a_plan_id + "," + dataGridView1_treatment_paln.Rows[i].Cells[0].Value.ToString();
                        }
                    }
                    if (rec_count != 0)
                    {
                        a_plan_id = a_plan_id.Substring(1, a_plan_id.Length - 1);
                        var form2 = new Add_Finished_Procedure();
                        form2.doctor_id = doctor_id;
                        form2.patient_id = patient_id;
                        form2.treatment_plan = a_plan_id;
                        openform(form2);
                    }
                    else
                    {
                        MessageBox.Show("Please select the Treatment..!", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //try
            //{
            //    int rec_count = 0;
            //    string a_plan_id = "";
            //    for (int i = 0; i < dataGridView1_treatment_paln.Rows.Count; i++)
            //    {
            //        if (dataGridView1_treatment_paln.Rows[i].Cells[8].Value.ToString() == "2")
            //        {
            //            rec_count = rec_count + 1;
            //            a_plan_id = a_plan_id + "," + dataGridView1_treatment_paln.Rows[i].Cells[0].Value.ToString();
            //        }
            //    }
            //    if (rec_count != 0)
            //    {
            //        a_plan_id = a_plan_id.Substring(1, a_plan_id.Length - 1);
            //        var form2 = new Add_Finished_Procedure();
            //        form2.doctor_id = doctor_id;
            //        form2.patient_id = patient_id;
            //        form2.treatment_plan = a_plan_id;
            //        openform(form2);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Please select the Treatment..!", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void emailToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            mprintosave();
        }
        public void mprintosave()
        {
            System.Data.DataTable dt_cf = this.cntrl.Get_treatment_details(treatment_plan_id);
            string doctor_name = "";
            string tr_date = "";
            if (dt_cf.Rows.Count > 0)
            {
                doctor_name = dt_cf.Rows[0]["dr_name"].ToString();
                tr_date = dt_cf.Rows[0]["date"].ToString();
            }
            System.Data.DataTable patient = this.cntrl.Get_Patient_Details(patient_id);
            string Pname = "", Gender = "", address = "", DOA = "", age = "", Mobile = "", DOB = "";
            if (patient.Rows.Count > 0)
            {
                Pname = patient.Rows[0]["pt_name"].ToString();
                Gender = patient.Rows[0]["gender"].ToString();
                address = patient.Rows[0]["street_address"].ToString() + " , " + patient.Rows[0]["city"].ToString();
                Mobile = patient.Rows[0]["primary_mobile_number"].ToString();
                DOA = DateTime.Parse(patient.Rows[0]["date"].ToString()).ToString("dd/MM/yyyy");
                DOB = patient.Rows[0]["date_of_birth"].ToString();
                age = patient.Rows[0]["age"].ToString();
            }
            string contact_no = "";
            string clinic_name = "";
            System.Data.DataTable dtp = this.cntrl.get_company_details();
            if (dtp.Rows.Count > 0)
            {
                string clinicn = "";
                string clinic = "";
                clinicn = dtp.Rows[0]["name"].ToString();
                clinic = clinicn.Replace("¤", "'");
                clinic_name = clinic;
                contact_no = dtp.Rows[0]["contact_no"].ToString();
            }
            string Apppath = System.IO.Directory.GetCurrentDirectory();
            StreamWriter sWrite = new StreamWriter(Apppath + "\\TreatmentMail.html");
            sWrite.WriteLine("<html>");
            sWrite.WriteLine("<head>");
            sWrite.WriteLine("</head>");
            sWrite.WriteLine("<body >");
            sWrite.WriteLine("<br><br><br>");
            sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
            sWrite.WriteLine("<tr><th align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=4>" + clinic_name.ToString() + "</font></th></tr>");
            sWrite.WriteLine("<tr><th align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>" + contact_no.ToString() + "</font></th></tr>");
            sWrite.WriteLine("</table>");
            sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
            sWrite.WriteLine("<tr><th align='center'><br><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=5>Treatment Details </font></th></tr>");
            sWrite.WriteLine("</table>");
            sWrite.WriteLine("<br>");
            sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
            sWrite.WriteLine(" <tr height='40px'>");
            sWrite.WriteLine("    <td align='left' width='400px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Consulted by : <b> " + doctor_name.ToString() + " </b></font></td>");
            sWrite.WriteLine("	<td align='left' width='170px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2></font></td>");
            sWrite.WriteLine("	<td align='left' width='130px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2></font></td>");
            sWrite.WriteLine(" </tr>");
            sWrite.WriteLine("  <tr>  <td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Name :<b>" + Pname.ToString() + " </b></font></td><td align='right' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Gender : <b>" + Gender.ToString() + " </b></font></td> </tr>");
            sWrite.WriteLine("   <tr>  <td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Address :<b> " + address.ToString() + "</b></font></td> <td align='right' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Age : <b> " + age + "</b></font></td> </tr>");
            sWrite.WriteLine("  <tr>  <td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Mobile No:<b> " + Mobile.ToString() + "</b></font></td> <td align='right' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Tratment Date : <b> " + DateTime.Parse(dt_cf.Rows[0]["date"].ToString()).ToString("dd/MM/yyyy") + "</b></font></td> </tr>");
            sWrite.WriteLine("</table>");
            sWrite.WriteLine("<br>");
            sWrite.WriteLine("<table align='center' style='width:700px;border: 1px;border-collapse: collapse;'>");
            sWrite.WriteLine("<tr>");
            sWrite.WriteLine("<td>");
            sWrite.WriteLine("<hr>");
            sWrite.WriteLine("<table align='center'  style='width:700px; border: 1px ;border-collapse: collapse;' >");
            //Treatment
            sWrite.WriteLine("<tr>");
            sWrite.WriteLine("    <td align='left' colspan='2' >");
            sWrite.WriteLine("<table align='center'  style='border: 1px ;border-collapse: collapse;' >");
            sWrite.WriteLine("<tr>");
            sWrite.WriteLine("<td align='left' width='100px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>&nbsp;Treatment</b></font></td>");
            sWrite.WriteLine("<td align='left' width='100px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>&nbsp;Tooth</b></font></td>");
            sWrite.WriteLine("<td align='right' width='100px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>&nbsp;Cost</b></font></td>");
            sWrite.WriteLine("<td align='right' width='100px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>&nbsp;Discount</b></font></td>");
            sWrite.WriteLine("<td align='right' width='100px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>&nbsp;Total Cost</b></font></td>");
            sWrite.WriteLine("<td align='right' width='100px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>&nbsp;Total</b></font></td>");
            sWrite.WriteLine("<td align='left' width='200px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>&nbsp;&nbsp;&nbsp;&nbsp;Notes</b> </font></td>");
            sWrite.WriteLine("</tr>");
            System.Data.DataTable dt_Treat_Plan = this.cntrl.treatment_sub_details(treatment_plan_id);
            if (dt_Treat_Plan.Rows.Count > 0)
            {
                int i = 0;
                Double totalEst = 0;
                for (i = 0; i < dt_Treat_Plan.Rows.Count; i++)
                {
                    //
                    DataTable dt_toothrltn = this.cntrl.tooth_relation(patient_id, treatment_plan_id, dt_Treat_Plan.Rows[i]["procedure_id"].ToString());
                    //image load
                    for (int m = 0; m < dt_toothrltn.Rows.Count; m++)
                    {
                        string O = ""; string M = ""; string D = ""; string B = ""; string L = "";

                        string tno = dt_toothrltn.Rows[m]["Tooth_Number"].ToString();
                        if (dt_toothrltn.Rows[m]["Occlusal"].ToString() == "Yes")
                        {
                            O = "O,";
                        }
                        if (dt_toothrltn.Rows[m]["Mesial"].ToString() == "Yes")
                        {
                            M = "M,";
                        }
                        if (dt_toothrltn.Rows[m]["Distal"].ToString() == "Yes")
                        {
                            D = "D,";
                        }
                        if (dt_toothrltn.Rows[m]["Buccal"].ToString() == "Yes")
                        {
                            B = "B,";
                        }
                        if (dt_toothrltn.Rows[m]["Lingual"].ToString() == "Yes")
                        {
                            L = "L,";
                        }
                        string a = O + M + D + B + L;
                        a = a.Remove(a.Length - 1);
                        toothprt += tno + "(" + a + ")";
                        O = ""; M = ""; D = ""; B = ""; L = "";
                    }
                    Double totaldiscount = 0;
                    string discount_string = "";
                    if (dt_Treat_Plan.Rows[i]["discount_type"].ToString() == "INR")
                    {
                        discount_string = "";
                    }
                    else
                    {
                        discount_string = "(" + dt_Treat_Plan.Rows[i]["discount"].ToString() + "%)";
                    }
                    Decimal totalcost = Convert.ToDecimal(dt_Treat_Plan.Rows[i]["cost"].ToString()) * Convert.ToDecimal(dt_Treat_Plan.Rows[i]["quantity"].ToString());
                    //clculation
                    decimal cost = 0; int qty = 0; decimal paid = 0;
                    decimal totCost = 0;
                    decimal totalsubst = 0;
                    cost = decimal.Parse(dt_Treat_Plan.Rows[i]["cost"].ToString());
                    qty = Convert.ToInt32(dt_Treat_Plan.Rows[i]["quantity"].ToString());
                    paid = decimal.Parse(dt_Treat_Plan.Rows[i]["total"].ToString());
                    totCost = cost * qty;
                    totalsubst = totCost - paid;
                    //end calulation
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left'   width='250px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + dt_Treat_Plan.Rows[i]["procedure_name"].ToString() + " </font></td>");
                    sWrite.WriteLine("<td align='left'   width='250px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + toothprt + " </font></td>");
                    sWrite.WriteLine("<td align='right'  width='250px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + String.Format("{0:C}", Convert.ToDecimal(dt_Treat_Plan.Rows[i]["cost"].ToString())) + "</font></td>");
                    sWrite.WriteLine("<td align='right'  width='250px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + dt_Treat_Plan.Rows[i]["discount_inrs"].ToString() + "</font></td>");
                    sWrite.WriteLine("<td align='right'   width='250px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + String.Format("{0:C}", Convert.ToDecimal(totalcost)) + " </font></td>");
                    sWrite.WriteLine("<td align='right'  width='250px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + String.Format("{0:C}", Convert.ToDecimal(dt_Treat_Plan.Rows[i]["total"].ToString())) + "</font></td>");
                    sWrite.WriteLine("<td align='left'  width='250px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + dt_Treat_Plan.Rows[i]["note"].ToString() +" </font></td>");
                    sWrite.WriteLine("</tr>");
                    totalEst = totalEst + Convert.ToDouble(dt_Treat_Plan.Rows[i]["total"].ToString());
                    totaldiscount = totaldiscount + Convert.ToDouble(dt_Treat_Plan.Rows[i]["discount_inrs"].ToString());
                    sWrite.WriteLine("<tr >");
                }
                sWrite.WriteLine("<td align='Right'   colspan='5'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + "Estimated Amount : <b>" + String.Format("{0:C}", Convert.ToDecimal(totalEst)) + "</b> </font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("</body>");
                sWrite.WriteLine("</html>");
                sWrite.Close();
                // mail senting...
                string email = "", emailName = "", emailPass = "";
                System.Data.DataTable sr = this.cntrl.getpatemail(patient_id);
                if (sr.Rows.Count > 0)
                {
                    email = sr.Rows[0]["email_address"].ToString();
                    if (email != "")
                    {
                        System.Data.DataTable sms = this.cntrl.send_email();
                        if (sms.Rows.Count > 0)
                        {
                            emailName = sms.Rows[0]["emailName"].ToString();
                            emailPass = sms.Rows[0]["emailPass"].ToString();
                            try
                            {
                                StreamReader reader = new StreamReader(Apppath + "\\TreatmentMail.html");
                                string readFile = reader.ReadToEnd();
                                string StrContent = "";
                                StrContent = readFile;
                                MailMessage message = new MailMessage();
                                message.From = new MailAddress(email);
                                message.To.Add(email);
                                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                                message.Subject = "Treatment Details";
                                message.Body = StrContent.ToString();
                                message.IsBodyHtml = true;
                                smtp.Port = 587;
                                smtp.Host = "smtp.gmail.com";
                                smtp.EnableSsl = true;
                                smtp.UseDefaultCredentials = false;
                                smtp.Credentials = new System.Net.NetworkCredential(emailName, emailPass);
                                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                                smtp.Send(message);
                                MessageBox.Show("Email is Sent To : " + email, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                reader.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Mail server authentication failed ", "Mail Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Email is not Correct", "Invalid Email !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void printToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            printhtml();
        }
        public void printhtml()
        {
            int kw = 0;
            System.Data.DataTable dt1 = this.cntrl.Get_Patient_Details(patient_id);
            System.Data.DataTable dt_cf = this.cntrl.Get_treatment_details(treatment_plan_id);
            string doctor_name = "";
            string tr_date = "";
            if (dt_cf.Rows.Count > 0)
            {
                doctor_name = dt_cf.Rows[0]["dr_name"].ToString();
                tr_date = dt_cf.Rows[0]["date"].ToString();
            }
            string Pname = "", Gender = "", address = "", DOA = "", age = "", Mobile = "";
            if (dt1.Rows.Count > 0)
            {
                Pname = dt1.Rows[0]["pt_name"].ToString();
                Gender = dt1.Rows[0]["gender"].ToString();
                Mobile = dt1.Rows[0]["primary_mobile_number"].ToString();
                DOA = DateTime.Parse(dt1.Rows[0]["date"].ToString()).ToString("dd/MM/yyyy");
                if (dt1.Rows[0]["date_of_birth"].ToString() != "")
                {
                    age = DateTime.Parse(dt1.Rows[0]["date_of_birth"].ToString()).ToString("dd/MM/yyyy");
                }
            }
            int Dexist = 0;
            string clinicn = "";
            string Clinic = "";
            System.Data.DataTable dtp = this.cntrl.get_company_details();
            if (dtp.Rows.Count > 0)
            {
                clinicn = dtp.Rows[0]["name"].ToString();
                Clinic = clinicn.Replace("¤", "'");
            }
            string Apppath = System.IO.Directory.GetCurrentDirectory();
            StreamWriter sWrite = new StreamWriter(Apppath + "\\TreatmentMail.html");
            sWrite.WriteLine("<html>");
            sWrite.WriteLine("<head>");
            sWrite.WriteLine("</head>");
            sWrite.WriteLine("<body >");
            sWrite.WriteLine("<br>");
            if (logo_name != "")
            {
                string Appath = System.IO.Directory.GetCurrentDirectory();
                if (File.Exists(Appath + "\\" + logo_name))
                {
                    sWrite.WriteLine("<table align='center' style='width:100%;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td width='100' height='75px' align='left' rowspan='3'><img src='" + Appath + "\\" + logo_name + "' width='77' height='78' style='width:100px;height:100px;'></td>  ");
                    sWrite.WriteLine("<td width='588' align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + Clinic + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;&nbsp;" + dtp.Rows[0]["street_address"].ToString() + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + dtp.Rows[0]["contact_no"].ToString() + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                    sWrite.WriteLine("</table>");
                }
                else
                {
                    sWrite.WriteLine("<table align='center' style='width:100%;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td  align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + Clinic + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;&nbsp;" + dtp.Rows[0]["street_address"].ToString() + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + dtp.Rows[0]["contact_no"].ToString() + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                    sWrite.WriteLine("</table>");
                }
            }
            else
            {
                sWrite.WriteLine("<table align='center' style='width:100%;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td  align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + Clinic + "</font></td></tr>");
                sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;&nbsp;" + dtp.Rows[0]["street_address"].ToString() + "</font></td></tr>");
                sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + dtp.Rows[0]["contact_no"].ToString() + "</font></td></tr>");
                sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                sWrite.WriteLine("</table>");
            }
            string sexage = "";
            address = "";
            sWrite.WriteLine("<table align='center' style='width:100%;border: 1px ;border-collapse: collapse;'>");
            sWrite.WriteLine("<tr>");
            if (dt1.Rows[0]["gender"].ToString() != "")
            {
                sexage = dt1.Rows[0]["gender"].ToString();
                Dexist = 1;
            }
            if (dt1.Rows[0]["age"].ToString() != "")
            {
                if (Dexist == 1)
                {
                    sexage = sexage + ", " + dt1.Rows[0]["age"].ToString() + " Years";
                }
                else
                {
                    sexage = dt1.Rows[0]["age"].ToString() + " Years";
                }
            }
            sWrite.WriteLine(" <td align='left' height=25><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>" + dt1.Rows[0]["pt_name"].ToString() + "</b><i> (" + sexage + ")</i></font></td>");
            sWrite.WriteLine(" </tr>");
            sWrite.WriteLine("<tr>");
            sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Patient Id:" + dt1.Rows[0]["pt_id"].ToString() + " </font></td>");
            sWrite.WriteLine(" </tr>");
            Dexist = 0;
            if (dt1.Rows[0]["street_address"].ToString() != "")
            {
                address = dt1.Rows[0]["street_address"].ToString();
                Dexist = 1;
            }
            if (dt1.Rows[0]["locality"].ToString() != "")
            {
                if (Dexist == 1)
                {
                    address = address + ",";
                }
                address = address + dt1.Rows[0]["locality"].ToString();
                Dexist = 1;
            }
            if (dt1.Rows[0]["city"].ToString() != "")
            {
                if (Dexist == 1)
                {
                    address = address + ",";
                }
                address = address + dt1.Rows[0]["city"].ToString();
                Dexist = 1;
            }
            if (dt1.Rows[0]["pincode"].ToString() != "")
            {
                if (Dexist == 1)
                {
                    address = address + ",";
                }
                address = address + dt1.Rows[0]["pincode"].ToString();
                Dexist = 1;
            }
            if (address != "")
            {
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + address + " </font></td>");
                sWrite.WriteLine(" </tr>");
            }
            if (dt1.Rows[0]["aadhar_id"].ToString() != "")
            {
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + "Aadhaar ID:" + dt1.Rows[0]["aadhar_id"].ToString() + " </font></td>");
                sWrite.WriteLine(" </tr>");
            }
            sWrite.WriteLine("<tr>");
            sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + dt1.Rows[0]["primary_mobile_number"].ToString() + " </font></td>");
            sWrite.WriteLine(" </tr>");
            if (dt1.Rows[0]["email_address"].ToString() != "")
            {
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + dt1.Rows[0]["email_address"].ToString() + " </font></td>");
                sWrite.WriteLine(" </tr>");
            }
            sWrite.WriteLine("<tr><td colspan=2><hr></td></tr>");
            sWrite.WriteLine("<tr>");
            sWrite.WriteLine("<td align='left' width='400px' height='30px'><FONT FACE='Geneva, Segoe UI' SIZE=2><FONT COLOR=black >By</FONT> :Dr. <b>" + doctor_name + " </b></font></td>");
            sWrite.WriteLine("</tr>");
            sWrite.WriteLine("</table>");
            sWrite.WriteLine("<br>");
            sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");
            sWrite.WriteLine("<tr>");
            sWrite.WriteLine("<td><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=5>Treatment Plan</FONT></td>");
            sWrite.WriteLine("<td width=250px></td>");
            if (dt_cf.Rows.Count > 0)
            {
                sWrite.WriteLine("<td align='right'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> <FONT COLOR=black>Date : </FONT>" + DateTime.Parse(dt_cf.Rows[0]["date"].ToString()).ToString("dd MMM yyyy") + "</font></td>");
            }
            else
            {
                sWrite.WriteLine("<td align='right'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> <FONT COLOR=black>Date : </FONT>" + DateTime.Now.ToString("dd MMM yyyy") + "</font></td>");
            }
            sWrite.WriteLine("</tr>");
            sWrite.WriteLine("</table>");
            sWrite.WriteLine("<table align='center'   style='width:100%;border: 1px ;border-collapse: collapse; table-layout: fixed'; >");
            sWrite.WriteLine("<tr style='background:#999999'>");
            sWrite.WriteLine("<td align='left' width='14.2%' height='30' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;Treatment</font></td>");
            sWrite.WriteLine("<td align='left' width='21.4%' height='30' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;Tooth</font></td>");
            sWrite.WriteLine("<td align='right' width='4%' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;Qty </font></td>");
            sWrite.WriteLine("<td align='right' width='9.427%' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;Cost</font></td>");
            sWrite.WriteLine("<td align='right' width='9.427%' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;Discount</font></td>");
            sWrite.WriteLine("<td align='right' width='11.42%' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;Total Cost</font></td>");
            sWrite.WriteLine("<td align='right' width='11.42%' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;Total</font></td>");
            sWrite.WriteLine("<td align='center' width='18.57%' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;Notes </font></td>");
            sWrite.WriteLine("</tr>");
            System.Data.DataTable dt_Treat_Plan = this.cntrl.treatment_sub_details(treatment_plan_id);
            if (dt_Treat_Plan.Rows.Count > 0)
            {
                Double totaldiscount = 0;
                string discount_string = "";
                if (dt_Treat_Plan.Rows[kw]["discount_type"].ToString() == "INR")
                {
                    discount_string = "";
                }
                else
                {
                    discount_string = "(" + dt_Treat_Plan.Rows[kw]["discount"].ToString() + "%)";
                }
                decimal totalcot_treat = 0;
                for (int i = 0; i < dt_Treat_Plan.Rows.Count; i++)
                {
                    //
                    DataTable dt_toothrltn = this.cntrl.tooth_relation(patient_id, treatment_plan_id, dt_Treat_Plan.Rows[i]["procedure_id"].ToString());
                    //image load
                    for (int m = 0; m < dt_toothrltn.Rows.Count; m++)
                    {
                        string O = ""; string M = ""; string D = ""; string B = ""; string L = "";
                        
                        string tno = dt_toothrltn.Rows[m]["Tooth_Number"].ToString();
                        if (dt_toothrltn.Rows[m]["Occlusal"].ToString() == "Yes")
                        {
                            O = "O,";
                        }
                        if (dt_toothrltn.Rows[m]["Mesial"].ToString() == "Yes")
                        {
                            M = "M,";
                        }
                        if (dt_toothrltn.Rows[m]["Distal"].ToString() == "Yes")
                        {
                            D = "D,";
                        }
                        if (dt_toothrltn.Rows[m]["Buccal"].ToString() == "Yes")
                        {
                            B = "B,";
                        }
                        if (dt_toothrltn.Rows[m]["Lingual"].ToString() == "Yes")
                        {
                            L = "L,";
                        }
                        string a = O + M + D + B + L;
                        if (a.Length>0)
                        {
                            a = a.Remove(a.Length - 1);
                            toothprt += tno + "(" + a + ")";
                            O = ""; M = ""; D = ""; B = ""; L = "";
                        }

                    }                    
                        Decimal totalcost = Convert.ToDecimal(dt_Treat_Plan.Rows[i]["cost"].ToString()) * Convert.ToDecimal(dt_Treat_Plan.Rows[i]["quantity"].ToString());
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' width='14.2%' height='30'></br><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + dt_Treat_Plan.Rows[i]["procedure_name"].ToString() + " </font></td>");
                    sWrite.WriteLine("<td style='word-wrap:break-word' align='left' width='21.4%' height='30px' ><br><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + toothprt + " </font></td>");
                    sWrite.WriteLine("<td style='word-wrap:break-word' align='center' width='4%'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + dt_Treat_Plan.Rows[i]["quantity"].ToString() + "</font></td>");
                    sWrite.WriteLine("<td style='word-wrap:break-word' align='right' width='9.427%'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + String.Format("{0:C}", Convert.ToDecimal(dt_Treat_Plan.Rows[i]["cost"].ToString())) + "</font></td>");
                    sWrite.WriteLine("<td style='word-wrap:break-word' align='right' width='9.427%'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + String.Format("{0:C}", Convert.ToDecimal(dt_Treat_Plan.Rows[i]["discount_inrs"].ToString())) + "</font></td>");
                    sWrite.WriteLine("<td style='word-wrap:break-word' align='right' width='11.42%'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + String.Format("{0:C}", Convert.ToDecimal(totalcost)) + " </font></td>");
                    sWrite.WriteLine("<td style='word-wrap:break-word' align='right' width='11.42%'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + String.Format("{0:C}", Convert.ToDecimal(dt_Treat_Plan.Rows[i]["total"].ToString())) + "</font></td>");
                    sWrite.WriteLine("<td style='word-wrap:break-word' align='char' style='text-align:left' width='18.57%'><br><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + dt_Treat_Plan.Rows[i]["note"].ToString() + "</font></td>");
                    sWrite.WriteLine("</tr>");
                    totalcot_treat = totalcot_treat + Convert.ToDecimal(totalcost);
                        totaldiscount = totaldiscount + Convert.ToDouble(dt_Treat_Plan.Rows[i]["discount_inrs"].ToString());
                    toothprt = "";
                }
                sWrite.WriteLine("<tr><td align='left' colspan=8><hr/></td></tr>");
                sWrite.WriteLine("<tr >");
                sWrite.WriteLine("<td align='Right' colspan=4><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>&nbsp;" + "Estimated Amount : " + " </font></td>");
                sWrite.WriteLine("<td align='Right'   colspan=2><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>&nbsp;" + String.Format("{0:C}", Convert.ToDecimal(totalcot_treat)) + " </font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<script>window.print();</script>");
                sWrite.WriteLine("</body>");
                sWrite.WriteLine("</html>");
                sWrite.Close();
                System.Diagnostics.Process.Start(Apppath + "\\TreatmentMail.html");
            }
        }

        private void labelprofile_Click(object sender, EventArgs e)
        {
            var form2 = new PappyjoeMVC.View.Patient_Profile_Details();
            form2.doctor_id = doctor_id;
            form2.patient_id = patient_id;
            openform(form2);
        }

        private void labelallpatient_Click(object sender, EventArgs e)
        {
            var form2 = new Patients();
            form2.doctor_id = doctor_id;
            openform(form2);
        }

        private void labelappointment_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id = privi_mdl.show_privillege(doctor_id);
                if (int.Parse(id) > 0)
                {
                    var form2 = new Show_Appointment();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
                else
                {
                    MessageBox.Show("There is No Privilege to Show Appointments", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                var form2 = new Show_Appointment();
                form2.doctor_id = doctor_id;
                form2.patient_id = patient_id;
                openform(form2);
            }
        }
        private void label44_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id = privi_mdl.show_vitals(doctor_id);
                if (int.Parse(id) > 0)
                {
                    var form2 = new Vital_Signs();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
                else
                {
                    MessageBox.Show("There is No Privilege to Show Vital signs", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                var form2 = new Vital_Signs();
                form2.doctor_id = doctor_id;
                form2.patient_id = patient_id;
                openform(form2);
            }
        }

        private void labeltreatment_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id = privi_mdl.treatment_show(doctor_id);
                if (int.Parse(id) > 0)
                {
                    if (PappyjoeMVC.Model.Connection.MyGlobals.project_type.TrimEnd() == "Dental")
                    {
                        var form2 = new Dental_Treatment_Plans();
                        form2.doctor_id = doctor_id;
                        form2.patient_id = patient_id;
                        openform(form2);
                    }
                    else
                    {
                        var form2 = new Treatment_Plans();
                        form2.doctor_id = doctor_id;
                        form2.patient_id = patient_id;
                        openform(form2);
                    }

                }
                else
                {
                    MessageBox.Show("There is No Privilege to Show Treatment plan", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                if (PappyjoeMVC.Model.Connection.MyGlobals.project_type.TrimEnd() == "Dental")
                {
                    var form2 = new Dental_Treatment_Plans();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
                else
                {
                    var form2 = new Treatment_Plans();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
            }
        }

        private void labelfinished_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id = privi_mdl.finishedtreatment_show(doctor_id);
                if (int.Parse(id) > 0)
                {
                    var form2 = new Finished_Procedure();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
                else
                {
                    MessageBox.Show("There is No Privilege to Show Finished procedure", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                var form2 = new Finished_Procedure();
                form2.doctor_id = doctor_id;
                form2.patient_id = patient_id;
                openform(form2);
            }
        }

        private void labelattachment_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id = privi_mdl.lab_show(doctor_id);
                if (int.Parse(id) > 0)
                {
                    var form2 = new Attachments();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
                else
                {
                    MessageBox.Show("There is No Privilege to Show Attachment", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                var form2 = new Attachments();
                form2.doctor_id = doctor_id;
                form2.patient_id = patient_id;
                openform(form2);
            }
        }

        private void labelinvoice_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id = privi_mdl.invoice_show(doctor_id);
                if (int.Parse(id) > 0)
                {
                    var form2 = new Invoice();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
                else
                {
                    MessageBox.Show("There is No Privilege to Show Invoice", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                var form2 = new Invoice();
                form2.doctor_id = doctor_id;
                form2.patient_id = patient_id;
                openform(form2);
            }
        }

        private void labelpayment_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id = privi_mdl.payments_show(doctor_id);
                if (int.Parse(id) > 0)
                {
                    var form2 = new Receipt();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
                else
                {
                    MessageBox.Show("There is No Privilege to Show Receipts", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                var form2 = new Receipt();
                form2.doctor_id = doctor_id;
                form2.patient_id = patient_id;
                openform(form2);
            }
        }

        private void labelledger_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id = privi_mdl.payments_add(doctor_id);
                if (int.Parse(id) > 0)
                {
                    var form2 = new Ledger();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
                else
                {
                    MessageBox.Show("There is No Privilege to show ledger", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                var form2 = new Ledger();
                form2.doctor_id = doctor_id;
                form2.patient_id = patient_id;
                openform(form2);
            }
        }

        private void labelclinical_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id = privi_mdl.findings_show(doctor_id);
                if (int.Parse(id) > 0)
                {
                    var form2 = new Clinical_Findings();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
                else
                {
                    MessageBox.Show("There is No Privilege to Show Clinical findings", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                var form2 = new Clinical_Findings();
                form2.doctor_id = doctor_id;
                form2.patient_id = patient_id;
                openform(form2);
            }
        }

        private void labelprescription_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id = privi_mdl.prescription_show(doctor_id);
                if (int.Parse(id) > 0)
                {
                    var form2 = new Prescription_Show();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
                else
                {
                    MessageBox.Show("There is No Privilege to Show Prescription", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                var form2 = new Prescription_Show();
                form2.doctor_id = doctor_id;
                form2.patient_id = patient_id;
                openform(form2);
            }
        }

        private void labl_Lab_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id = privi_mdl.lab_show(doctor_id);
                if (int.Parse(id) > 0)
                {
                    var form2 = new LabWorks();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
                else
                {
                    MessageBox.Show("There is No Privilege to Show lab", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                var form2 = new LabWorks();
                form2.doctor_id = doctor_id;
                form2.patient_id = patient_id;
                openform(form2);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var form2 = new Main_Calendar();
            form2.doctor_id = doctor_id;
            openform(form2);
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            var form2 = new LabtrackingReport();
            form2.patient_id = patient_id;
            form2.doctor_id = doctor_id;
            openform(form2);
        }

        private void linkLabel_Name_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form2 = new PappyjoeMVC.View.Patient_Profile_Details();
            form2.doctor_id = doctor_id;
            form2.patient_id = patient_id;
            openform(form2);
        }

        private void linkLabel_id_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form2 = new PappyjoeMVC.View.Patient_Profile_Details();
            form2.doctor_id = doctor_id;
            form2.patient_id = patient_id;
            openform(form2);
        }

        public static string apntid = "";
        private void pb_AppntmntAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (doctor_id != "1")
                {
                    string id = privi_mdl.Add_privillege(doctor_id);
                    if (int.Parse(id) > 0)
                    {
                        //DataTable dt = this.Apptmt_cntrl.show(patient_id);
                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    apntid = dt.Rows[i]["a_id"].ToString();
                        //}
                        var form2 = new Add_Appointment();
                        form2.patient_id = patient_id;
                        form2.doctor_id = doctor_id;
                        //form2.appointment_id = apntid;
                        openform(form2);
                    }
                    else
                    {
                        MessageBox.Show("There is No Privilege to Add Appointments", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    }
                }
                else
                {
                    //DataTable dt = this.Apptmt_cntrl.show(patient_id);
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    apntid = dt.Rows[i]["a_id"].ToString();
                    //}
                    var form2 = new Add_Appointment();
                    form2.patient_id = patient_id;
                    form2.doctor_id = doctor_id;
                    //form2.appointment_id = apntid;
                    openform(form2);
                }

            }
            catch (Exception ex)
            { MessageBox.Show("Error!..", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void pb_vitalsAdd_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id = privi_mdl.add_vitals(doctor_id);
                if (int.Parse(id) > 0)
                {
                    var form2 = new PappyjoeMVC.View.Add_Vital_Signs();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
                else
                {
                    MessageBox.Show("There is No Privilege to Add Vital Signs", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                var form2 = new PappyjoeMVC.View.Add_Vital_Signs();
                form2.doctor_id = doctor_id;
                form2.patient_id = patient_id;
                openform(form2);
            }
        }

        private void pb_ClinicalAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (doctor_id != "1")
                {
                    string id = privi_mdl.findings_Add(doctor_id);// this.clinical_cntrl.user_priv_EMRCF_A(doctor_id);
                    if (int.Parse(id) > 0)
                    {
                        var form2 = new Add_Clinical_Notes();
                        form2.doctor_id = doctor_id;
                        form2.patient_id = patient_id;
                        openform(form2);
                    }
                    else
                    {
                        MessageBox.Show("There is No Privilege to Add Clinical findings", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    }
                }
                else
                {
                    var form2 = new Add_Clinical_Notes();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pb_trtmntAdd_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id;
                id = privi_mdl.treatment_add(doctor_id);// this.treatmnt_cntrl.check_privillege(doctor_id);
                if (int.Parse(id) > 0)
                {
                    if (PappyjoeMVC.Model.Connection.MyGlobals.project_type.TrimEnd() == "Dental")
                    {
                        var form2 = new Add_Dental_Treatment_Plan();
                        form2.doctor_id = doctor_id;
                        form2.patient_id = patient_id;
                        openform(form2);
                    }
                    else
                    {
                        var form2 = new Add_Treatment_Plan();
                        form2.doctor_id = doctor_id;
                        form2.patient_id = patient_id;
                        openform(form2);
                    }

                }
                else
                {
                    MessageBox.Show("There is No Privilege to Add Treatment Plan", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                if (PappyjoeMVC.Model.Connection.MyGlobals.project_type.TrimEnd() == "Dental")
                {
                    var form2 = new Add_Dental_Treatment_Plan();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
                else
                {
                    var form2 = new Add_Treatment_Plan();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
            }
        }

        private void pb_fnsdtrtAdd_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id;
                id = privi_mdl.finishedtreatment_add(doctor_id);// this.treatmnt_cntrl.check_privillege(doctor_id);
                if (int.Parse(id) > 0)
                {
                    var form2 = new Add_Finished_Procedure();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
                else
                {
                    MessageBox.Show("There is No Privilege to Add Finished Treatment", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                var form2 = new Add_Finished_Procedure();
                form2.doctor_id = doctor_id;
                form2.patient_id = patient_id;
                openform(form2);
            }
        }

        private void pb_prescrptAdd_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id = privi_mdl.prescription_add(doctor_id);
                if (int.Parse(id) > 0)
                {
                    var form2 = new Prescription_Add();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
                else
                {
                    MessageBox.Show("There is No Privilege to Add Prescription", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                var form2 = new Prescription_Add();
                form2.doctor_id = doctor_id;
                form2.patient_id = patient_id;
                openform(form2);
            }
        }

        private void pb_labAdd_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id = privi_mdl.lab_add(doctor_id);
                if (int.Parse(id) > 0)
                {
                    var form2 = new PappyjoeMVC.View.Add_Labwork();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
                else
                {
                    MessageBox.Show("There is No Privilege to Add Lab", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                var form2 = new PappyjoeMVC.View.Add_Labwork();
                form2.doctor_id = doctor_id;
                form2.patient_id = patient_id;
                openform(form2);
            }
        }

        private void pb_attchmntAdd_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id = this.privi_mdl.add_attachments(doctor_id);
                //if (id != "")
                //{
                if (int.Parse(id) > 0)
                {
                    var form2 = new Add_Attachments();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
                else
                {
                    MessageBox.Show("There is No Privilege to Add Attachment", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                //}
            }
            else
            {
                var form2 = new Add_Attachments();
                form2.doctor_id = doctor_id;
                form2.patient_id = patient_id;
                openform(form2);
            }
        }

        private void pb_invoiceAdd_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id = this.privi_mdl.invoice_add(doctor_id);
                //if (id != "")
                //{
                if (int.Parse(id) > 0)
                {
                    var form2 = new Add__invoice();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
                else
                {
                    MessageBox.Show("There is No Privilege to Add Invoice", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                //}
            }
            else
            {
                var form2 = new Add__invoice();
                form2.doctor_id = doctor_id;
                form2.patient_id = patient_id;
                openform(form2);
            }
        }

        private void pb_recieptsAdd_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id = privi_mdl.payments_add(doctor_id);
                if (int.Parse(id) > 0)
                {
                    var form2 = new Add_Receipt();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
                else
                {
                    MessageBox.Show("There is No Privilege to Add Receipt", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                var form2 = new Add_Receipt();
                form2.doctor_id = doctor_id;
                form2.patient_id = patient_id;
                openform(form2);
            }
        }
    }
}
