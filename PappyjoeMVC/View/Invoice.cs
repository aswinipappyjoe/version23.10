using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PappyjoeMVC.Controller;
using PappyjoeMVC.Model;
namespace PappyjoeMVC.View
{
    public partial class Invoice : Form
    {
        public string doctor_id = "0";
        public string staff_id = "";
        public string patient_id = "";
        string logo_name = "";
        string path = "";
        string invoice_plan_id = "0";
        int button_value = 0;
        int k = 0;
        System.Drawing.Image logo = null;
        Invoice_controller cntrl = new Invoice_controller();
        Show_Appointment_controller Apptmt_cntrl = new Show_Appointment_controller();
        Vital_Signs_controller vital_cntrl = new Vital_Signs_controller();
        Clinical_Findings_controller clinical_cntrl = new Clinical_Findings_controller();
        Treatment_controller treatmnt_cntrl = new Treatment_controller();
        Finished_Procedre_controller fnsdtrt_cntrl = new Finished_Procedre_controller();
        Prescription_Show_controller prescr_cntrl = new Prescription_Show_controller();
        LabWorks_controller lab_cntrl = new LabWorks_controller();
        Attachments_controller attach_cntrl = new Attachments_controller();
        Invoice_controller invo_cntrl = new Invoice_controller();
        Receipt_controller recpt_cntrl = new Receipt_controller();
        Common_model cmodel = new Common_model();
        Inventory_model inv_model = new Inventory_model(); user_privillage_model privi_mdl = new user_privillage_model();
        Connection db = new Connection();
        string combo_topmargin = "";
        string combo_leftmargin = "";
        string combo_bottommargin = "";
        string combo_rightmargin = "";
        string combo_paper_size = "";
        string combo_footer_topmargin = "";
        string rich_fullwidth = "";
        string rich_leftsign = "";
        string rich_rightsign = "";
        string patient_details = "";
        string med = "";
        string patient = "";
        string address = "";
        string phone = "";
        string blood = "";
        string gender = "";
        string orientation = "";
        string includeheader = "0";
        string includelogo = "0";
        
        public Invoice()
        {
            InitializeComponent();
        }
        private void Invoice_Load(object sender, EventArgs e)
        {
            try
            {
                //if (doctor_id != "1")
                //{
                //    // add
                //    string privid;
                //    privid = this.cntrl.check_addprivillege(doctor_id);
                //    if (int.Parse(privid) > 0)
                //    {
                //        btn_ADD.Enabled = false;
                //        pb_invoiceAdd.Enabled = false;
                //    }
                //    else
                //    {
                //        btn_ADD.Enabled = true;
                //        pb_invoiceAdd.Enabled = true;
                //    }
                //    //Delete
                //    privid = this.cntrl.check_delete_privillege(doctor_id);
                //    if (int.Parse(privid) > 0)
                //    {
                //        deleteToolStripMenuItem1.Enabled = false;
                //    }
                //    else
                //    {
                //        deleteToolStripMenuItem1.Enabled = true;
                //    }
                //    privid = this.cntrl.addpayment_privillege(doctor_id);
                //    if (int.Parse(privid) > 0)
                //    {
                //        btn_paySelectedInvoice.Visible = false;
                //    }
                //    else
                //    {
                //        btn_paySelectedInvoice.Visible = true;
                //    }
                //}
                System.Data.DataTable clinicname = cmodel.Get_CompanyNAme();
                if (clinicname.Rows.Count > 0)
                {
                    string clinicn = "";
                    clinicn = clinicname.Rows[0]["name"].ToString();
                    path = clinicname.Rows[0]["path"].ToString();
                    string docnam = cmodel.Get_DoctorName(doctor_id);
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
                System.Data.DataTable pat = cmodel.Get_Patient_id_NAme(patient_id);
                if (pat.Rows.Count > 0)
                {
                    linkLabel_id.Text = pat.Rows[0]["pt_id"].ToString();
                    linkLabel_Name.Text = pat.Rows[0]["pt_name"].ToString();
                }
                DataTable dtb = this.cntrl.get_total_payment(patient_id);
                set_totalPayment(dtb);
                DataTable dt_invoice_main = this.cntrl.Get_invoice_mainDetails(patient_id);
                Load_MainTable(dt_invoice_main);
                if (dt_invoice_main.Rows.Count > 35)
                {
                    label1.Visible = true;
                }
                else
                {
                    label1.Visible = false;
                }
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            DataTable dtb = this.treatmnt_cntrl.get_treatments(patient_id);
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
        public void set_totalPayment(DataTable pay)
        {
            if (pay.Rows.Count > 0)
            {
                label8.Text = pay.Rows[0]["total_payment"].ToString() + " due";
            }
            dgv_invoice.Show();
            dgv_invoice.ColumnCount = 10;
            dgv_invoice.RowCount = 0;
            dgv_invoice.ColumnHeadersVisible = false;
            dgv_invoice.RowHeadersVisible = false;
            dgv_invoice.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_invoice.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_invoice.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_invoice.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
        public void Setcontroller(Invoice_controller controller)
        {
            cntrl = controller;
        }
        public void Load_MainTable(DataTable dt_invoice_main)
        {

            int i = 0;
            dgv_invoice.Rows.Clear();
            if (dt_invoice_main.Rows.Count > 0)
            {
                for (int j = 0; j < dt_invoice_main.Rows.Count; j++)
                {
                    dgv_invoice.Rows.Add("0", "", String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(dt_invoice_main.Rows[j]["date"].ToString())), "", "", "", "", "", "0", "");
                    dgv_invoice.Rows[i].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Bold);
                    dgv_invoice.Rows[i].Cells[2].Style.ForeColor = Color.DarkGreen;
                    dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                    dgv_invoice.Rows[i].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
                    i = i + 1;
                    dgv_invoice.Rows.Add(dt_invoice_main.Rows[j]["id"].ToString(), "", "INVOICE NUMBER", "TREATMENT & PRODUCTS", "COST", "DISCOUNT", "TAX", "AMOUNT DUE", dt_invoice_main.Rows[j]["status"].ToString(), "");
                    dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.gry;
                    dgv_invoice.Rows[i].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    dgv_invoice.Rows[i].Cells[2].Style.ForeColor = Color.White;
                    dgv_invoice.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    dgv_invoice.Rows[i].Cells[3].Style.ForeColor = Color.White;
                    dgv_invoice.Rows[i].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    dgv_invoice.Rows[i].Cells[4].Style.ForeColor = Color.White;
                    dgv_invoice.Rows[i].Cells[5].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    dgv_invoice.Rows[i].Cells[5].Style.ForeColor = Color.White;
                    dgv_invoice.Rows[i].Cells[6].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    dgv_invoice.Rows[i].Cells[6].Style.ForeColor = Color.White;
                    dgv_invoice.Rows[i].Cells[7].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    dgv_invoice.Rows[i].Cells[7].Style.ForeColor = Color.White;
                    dgv_invoice.Rows[i].Cells[8].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    dgv_invoice.Rows[i].Cells[8].Style.ForeColor = Color.White;
                    dgv_invoice.Rows[i].Cells[1].Style.BackColor = Color.DarkGray;
                    dgv_invoice.Rows[i].Cells[2].Style.BackColor = Color.DarkGray;
                    dgv_invoice.Rows[i].Cells[3].Style.BackColor = Color.DarkGray;
                    dgv_invoice.Rows[i].Cells[4].Style.BackColor = Color.DarkGray;
                    dgv_invoice.Rows[i].Cells[5].Style.BackColor = Color.DarkGray;
                    dgv_invoice.Rows[i].Cells[6].Style.BackColor = Color.DarkGray;
                    dgv_invoice.Rows[i].Cells[7].Style.BackColor = Color.DarkGray;
                    dgv_invoice.Rows[i].Cells[8].Style.BackColor = Color.DarkGray;
                    dgv_invoice.Rows[i].Cells[9].Value = PappyjoeMVC.Properties.Resources.Bill;
                    if (dt_invoice_main.Rows[j]["status"].ToString() != "0")
                    {
                        dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.Borderblank;
                    }
                    else
                    {
                        dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.billed;
                    }
                    System.Data.DataTable dt_pt_sub = this.cntrl.get_invoiceDetails(dt_invoice_main.Rows[j]["id"].ToString());
                    decimal totalEst = 0;
                    Decimal total_cost = 0;
                    Decimal total_discount = 0;
                    Decimal total_tax = 0;
                    int row_no = 0;
                    string discount_string = "";
                    string Dr_name = "";
                    if (dt_pt_sub.Rows.Count > 0)
                    {
                        for (int k = 0; k < dt_pt_sub.Rows.Count; k++)
                        {
                            System.Data.DataTable dt_dr = db.table("SELECT doctor_name FROM tbl_doctor where id='" + dt_pt_sub.Rows[k]["dr_id"].ToString() + "' ORDER BY id");
                            if (dt_dr.Rows.Count > 0)
                            {
                                Dr_name = dt_dr.Rows[0]["doctor_name"].ToString();
                            }
                            if (dt_pt_sub.Rows[k]["discount_type"].ToString() == "INR")
                            {
                                discount_string = "";
                            }
                            else
                            {
                                discount_string = "(" + dt_pt_sub.Rows[k]["discount"].ToString() + "%)";
                            }
                            Decimal totalcost = Convert.ToDecimal(dt_pt_sub.Rows[k]["cost"].ToString()) * Convert.ToDecimal(dt_pt_sub.Rows[k]["unit"].ToString());
                            total_cost = total_cost + Convert.ToDecimal(totalcost);
                            total_discount = total_discount + Convert.ToDecimal(dt_pt_sub.Rows[k]["discountin_rs"].ToString());
                            total_tax = total_tax + Convert.ToDecimal(dt_pt_sub.Rows[k]["tax_inrs"].ToString());
                            if (k == 0 || k > 2)
                            {
                                i = i + 1;
                                if (k == 0)
                                {
                                    dgv_invoice.Rows.Add(dt_pt_sub.Rows[k]["id"].ToString(), "", dt_invoice_main.Rows[j]["invoice"].ToString(), dt_pt_sub.Rows[k]["services"].ToString(), String.Format("{0:C2}", Convert.ToDecimal(totalcost)), String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discountin_rs"].ToString())) + discount_string, String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["tax_inrs"].ToString())), String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString())), "");
                                    dgv_invoice.Rows[i].Height = 25;
                                    dgv_invoice.Rows[i].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
                                    dgv_invoice.Rows[i].Cells[2].Style.ForeColor = Color.DodgerBlue;
                                    dgv_invoice.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
                                    dgv_invoice.Rows[i].Cells[3].Style.ForeColor = Color.DodgerBlue;
                                    dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                                    dgv_invoice.Rows[i].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
                                }
                                else
                                {
                                    dgv_invoice.Rows.Add(dt_pt_sub.Rows[k]["id"].ToString(), "", "", dt_pt_sub.Rows[k]["services"].ToString(), String.Format("{0:C2}", Convert.ToDecimal(totalcost)), String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discountin_rs"].ToString())) + discount_string, String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["tax_inrs"].ToString())), String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString())), "");
                                    dgv_invoice.Rows[i].Height = 25;
                                    dgv_invoice.Rows[i].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
                                    dgv_invoice.Rows[i].Cells[2].Style.ForeColor = Color.DodgerBlue;
                                    dgv_invoice.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
                                    dgv_invoice.Rows[i].Cells[3].Style.ForeColor = Color.DodgerBlue;
                                    dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                                    dgv_invoice.Rows[i].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
                                    i = i + 1;
                                    dgv_invoice.Rows.Add("0", "", "", "Completed by " + Dr_name, "", "", "", "", "", "");
                                    dgv_invoice.Rows[i].Height = 25;
                                    dgv_invoice.Rows[i].Cells[2].Style.ForeColor = Color.DimGray;
                                    dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                                    dgv_invoice.Rows[i].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
                                }
                            }
                            if (k == 0)
                            {
                                i = i + 1;
                                dgv_invoice.Rows.Add("0", "", "Balance", "Completed by " + Dr_name, "", "", "", "", "", "");
                                dgv_invoice.Rows[i].Height = 20;
                                dgv_invoice.Rows[i].Cells[2].Style.ForeColor = Color.DimGray;
                                dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                                dgv_invoice.Rows[i].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
                                i = i + 1;
                                row_no = i;
                                dgv_invoice.Rows.Add("0", "", "0000.00", "", "", "", "", "", "", "");
                                dgv_invoice.Rows[i].Height = 20;
                                dgv_invoice.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                                dgv_invoice.Rows[i].Cells[2].Style.Alignment = DataGridViewContentAlignment.TopLeft;
                                dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                                dgv_invoice.Rows[i].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
                                dgv_invoice.Rows[i].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                                i = i + 1;
                                dgv_invoice.Rows.Add("0", "", "", "", "", "", "", "", "", "");
                                dgv_invoice.Rows[i].Height = 20;
                                dgv_invoice.Rows[i].Cells[2].Style.ForeColor = Color.DimGray;
                                dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                                dgv_invoice.Rows[i].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
                                i = i + 1;
                                dgv_invoice.Rows.Add("0", "", "Total                   Paid", "", "", "", "", "", "", "");
                                dgv_invoice.Rows[i].Height = 20;
                                dgv_invoice.Rows[i].Cells[2].Style.ForeColor = Color.DimGray;
                                dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                                dgv_invoice.Rows[i].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
                                i = i + 1;
                                dgv_invoice.Rows.Add("0", "", "0000.00        0000.00", "", "", "", "", "", "", "");
                                dgv_invoice.Rows[i].Height = 15;
                                dgv_invoice.Rows[i].Cells[2].Style.ForeColor = Color.DimGray;
                                dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                                dgv_invoice.Rows[i].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
                                dgv_invoice.Rows[i].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                            }
                            if (k == 1)
                            {
                                dgv_invoice.Rows[i - 3].Cells[0].Value = dt_pt_sub.Rows[k]["id"].ToString();
                                dgv_invoice.Rows[i - 3].Cells[3].Value = dt_pt_sub.Rows[k]["services"].ToString();
                                dgv_invoice.Rows[i - 3].Cells[4].Value = String.Format("{0:C2}", Convert.ToDecimal(totalcost));
                                dgv_invoice.Rows[i - 3].Cells[5].Value = String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discountin_rs"].ToString())) + discount_string;
                                dgv_invoice.Rows[i - 3].Cells[6].Value = String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["tax_inrs"].ToString()));
                                dgv_invoice.Rows[i - 3].Cells[7].Value = String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString()));
                                dgv_invoice.Rows[i - 3].Cells[8].Value = "";
                                dgv_invoice.Rows[i - 3].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
                                dgv_invoice.Rows[i - 3].Cells[3].Style.ForeColor = Color.DodgerBlue;
                                dgv_invoice.Rows[i - 3].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                                dgv_invoice.Rows[i - 3].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
                                dgv_invoice.Rows[i - 2].Cells[3].Value = "Completed by " + Dr_name;
                            }
                            else if (k == 2)
                            {
                                dgv_invoice.Rows[i - 1].Cells[0].Value = dt_pt_sub.Rows[k]["id"].ToString();
                                dgv_invoice.Rows[i - 1].Cells[3].Value = dt_pt_sub.Rows[k]["services"].ToString();
                                dgv_invoice.Rows[i - 1].Cells[4].Value = String.Format("{0:C2}", Convert.ToDecimal(totalcost));
                                dgv_invoice.Rows[i - 1].Cells[5].Value = String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discountin_rs"].ToString())) + discount_string;
                                dgv_invoice.Rows[i - 1].Cells[6].Value = String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["tax_inrs"].ToString()));
                                dgv_invoice.Rows[i - 1].Cells[7].Value = String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString()));
                                dgv_invoice.Rows[i - 1].Cells[8].Value = "";
                                dgv_invoice.Rows[i - 1].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
                                dgv_invoice.Rows[i - 1].Cells[3].Style.ForeColor = Color.DodgerBlue;
                                dgv_invoice.Rows[i - 1].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                                dgv_invoice.Rows[i - 1].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
                                dgv_invoice.Rows[i].Cells[3].Value = "Completed by " + Dr_name;
                            }
                            dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                            totalEst = totalEst + Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString());//Balance
                            dgv_invoice.Rows[i].Cells[8].Value = PappyjoeMVC.Properties.Resources.blank;
                        }
                        i = i + 1;
                        dgv_invoice.Rows.Add("0", "", "", "", "", "", "", "", "", "");
                        dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                        dgv_invoice.Rows[i].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
                        i = i + 1;
                        decimal gtotal = 0, paid = 0;
                        //gtotal = Convert.ToDouble(total_cost) - (Convert.ToDouble(total_discount) + Convert.ToDouble(total_tax));
                        gtotal = (Convert.ToDecimal(total_cost) - Convert.ToDecimal(total_discount)) + Convert.ToDecimal(total_tax);
                        paid = gtotal - totalEst;
                        dgv_invoice.Rows[row_no].Cells[2].Value = totalEst.ToString("0.00");
                        dgv_invoice.Rows[row_no + 3].Cells[2].Value = gtotal.ToString("0.00") + "                  " + paid.ToString("0.00");
                    }

                }
                if (dgv_invoice.Rows.Count <= 0)
                {
                    int x = (panel6.Size.Width - Lab_Msg.Size.Width) / 2;
                    Lab_Msg.Location = new Point(x, Lab_Msg.Location.Y);
                    Lab_Msg.Show();
                }
                else
                {
                    Lab_Msg.Hide();
                }
                btn_ADD.Show();
            }
        }
        //public void Load_MainTable(DataTable dt_invoice_main)
        //{

        //    int i = 0;
        //    dgv_invoice.Rows.Clear();
        //    if (dt_invoice_main.Rows.Count > 0)
        //    {
        //        for (int j = 0; j < dt_invoice_main.Rows.Count; j++)
        //        {
        //            dgv_invoice.Rows.Add("0", "", String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(dt_invoice_main.Rows[j]["date"].ToString())), "", "", "", "", "", "0", "");
        //            dgv_invoice.Rows[i].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Bold);
        //            dgv_invoice.Rows[i].Cells[2].Style.ForeColor = Color.DarkGreen;
        //            dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
        //            dgv_invoice.Rows[i].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
        //            i = i + 1;
        //            dgv_invoice.Rows.Add(dt_invoice_main.Rows[j]["id"].ToString(), "", "INVOICE NUMBER", "TREATMENT & PRODUCTS", "COST", "DISCOUNT", "TAX", "AMOUNT DUE", dt_invoice_main.Rows[j]["status"].ToString(), "");
        //            dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.gry;
        //            dgv_invoice.Rows[i].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
        //            dgv_invoice.Rows[i].Cells[2].Style.ForeColor = Color.White;
        //            dgv_invoice.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
        //            dgv_invoice.Rows[i].Cells[3].Style.ForeColor = Color.White;
        //            dgv_invoice.Rows[i].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
        //            dgv_invoice.Rows[i].Cells[4].Style.ForeColor = Color.White;
        //            dgv_invoice.Rows[i].Cells[5].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
        //            dgv_invoice.Rows[i].Cells[5].Style.ForeColor = Color.White;
        //            dgv_invoice.Rows[i].Cells[6].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
        //            dgv_invoice.Rows[i].Cells[6].Style.ForeColor = Color.White;
        //            dgv_invoice.Rows[i].Cells[7].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
        //            dgv_invoice.Rows[i].Cells[7].Style.ForeColor = Color.White;
        //            dgv_invoice.Rows[i].Cells[8].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
        //            dgv_invoice.Rows[i].Cells[8].Style.ForeColor = Color.White;
        //            dgv_invoice.Rows[i].Cells[1].Style.BackColor = Color.DarkGray;
        //            dgv_invoice.Rows[i].Cells[2].Style.BackColor = Color.DarkGray;
        //            dgv_invoice.Rows[i].Cells[3].Style.BackColor = Color.DarkGray;
        //            dgv_invoice.Rows[i].Cells[4].Style.BackColor = Color.DarkGray;
        //            dgv_invoice.Rows[i].Cells[5].Style.BackColor = Color.DarkGray;
        //            dgv_invoice.Rows[i].Cells[6].Style.BackColor = Color.DarkGray;
        //            dgv_invoice.Rows[i].Cells[7].Style.BackColor = Color.DarkGray;
        //            dgv_invoice.Rows[i].Cells[8].Style.BackColor = Color.DarkGray;
        //            dgv_invoice.Rows[i].Cells[9].Value = PappyjoeMVC.Properties.Resources.Bill;
        //            if (dt_invoice_main.Rows[j]["status"].ToString() != "0")
        //            {
        //                dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.Borderblank;
        //            }
        //            else
        //            {
        //                dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.billed;
        //            }
        //            System.Data.DataTable dt_pt_sub = this.cntrl.get_invoiceDetails(dt_invoice_main.Rows[j]["id"].ToString());
        //            decimal totalEst = 0;
        //            Decimal total_cost = 0;
        //            Decimal total_discount = 0;
        //            Decimal total_tax = 0;
        //            int row_no = 0;
        //            string discount_string = "";
        //            string Dr_name = "";
        //            if(dt_pt_sub.Rows.Count>0)
        //            {
        //                for (int k = 0; k < dt_pt_sub.Rows.Count; k++)
        //                {
        //                    System.Data.DataTable dt_dr = db.table("SELECT doctor_name FROM tbl_doctor where id='" + dt_pt_sub.Rows[k]["dr_id"].ToString() + "' ORDER BY id");
        //                    if (dt_dr.Rows.Count > 0)
        //                    {
        //                        Dr_name = dt_dr.Rows[0]["doctor_name"].ToString();
        //                    }
        //                    if (dt_pt_sub.Rows[k]["discount_type"].ToString() == "INR")
        //                    {
        //                        discount_string = "";
        //                    }
        //                    else
        //                    {
        //                        discount_string = "(" + dt_pt_sub.Rows[k]["discount"].ToString() + "%)";
        //                    }
        //                    Decimal totalcost = Convert.ToDecimal(dt_pt_sub.Rows[k]["cost"].ToString()) * Convert.ToDecimal(dt_pt_sub.Rows[k]["unit"].ToString());
        //                    total_cost = total_cost + Convert.ToDecimal(totalcost);
        //                    total_discount = total_discount + Convert.ToDecimal(dt_pt_sub.Rows[k]["discountin_rs"].ToString());
        //                    total_tax = total_tax + Convert.ToDecimal(dt_pt_sub.Rows[k]["tax_inrs"].ToString());
        //                    if (k == 0 || k > 2)
        //                    {
        //                        i = i + 1;
        //                        if (k == 0)
        //                        {
        //                            dgv_invoice.Rows.Add(dt_pt_sub.Rows[k]["id"].ToString(), "", dt_invoice_main.Rows[j]["invoice"].ToString(), dt_pt_sub.Rows[k]["services"].ToString(), String.Format("{0:C2}", Convert.ToDecimal(totalcost)), String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discountin_rs"].ToString())) + discount_string, String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["tax_inrs"].ToString())), String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString())), "");
        //                            dgv_invoice.Rows[i].Height = 25;
        //                            dgv_invoice.Rows[i].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
        //                            dgv_invoice.Rows[i].Cells[2].Style.ForeColor = Color.DodgerBlue;
        //                            dgv_invoice.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
        //                            dgv_invoice.Rows[i].Cells[3].Style.ForeColor = Color.DodgerBlue;
        //                            dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
        //                            dgv_invoice.Rows[i].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
        //                        }
        //                        else
        //                        {
        //                            dgv_invoice.Rows.Add(dt_pt_sub.Rows[k]["id"].ToString(), "", "", dt_pt_sub.Rows[k]["services"].ToString(), String.Format("{0:C2}", Convert.ToDecimal(totalcost)), String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discountin_rs"].ToString())) + discount_string, String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["tax_inrs"].ToString())), String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString())), "");
        //                            dgv_invoice.Rows[i].Height = 25;
        //                            dgv_invoice.Rows[i].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
        //                            dgv_invoice.Rows[i].Cells[2].Style.ForeColor = Color.DodgerBlue;
        //                            dgv_invoice.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
        //                            dgv_invoice.Rows[i].Cells[3].Style.ForeColor = Color.DodgerBlue;
        //                            dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
        //                            dgv_invoice.Rows[i].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
        //                            i = i + 1;
        //                            dgv_invoice.Rows.Add("0", "", "", "Completed by " + Dr_name, "", "", "", "", "", "");
        //                            dgv_invoice.Rows[i].Height = 25;
        //                            dgv_invoice.Rows[i].Cells[2].Style.ForeColor = Color.DimGray;
        //                            dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
        //                            dgv_invoice.Rows[i].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
        //                        }
        //                    }
        //                    if (k == 0)
        //                    {
        //                        i = i + 1;
        //                        dgv_invoice.Rows.Add("0", "", "Balance", "Completed by " + Dr_name, "", "", "", "", "", "");
        //                        dgv_invoice.Rows[i].Height = 20;
        //                        dgv_invoice.Rows[i].Cells[2].Style.ForeColor = Color.DimGray;
        //                        dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
        //                        dgv_invoice.Rows[i].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
        //                        i = i + 1;
        //                        row_no = i;
        //                        dgv_invoice.Rows.Add("0", "", "0000.00", "", "", "", "", "", "", "");
        //                        dgv_invoice.Rows[i].Height = 20;
        //                        dgv_invoice.Rows[i].Cells[2].Style.ForeColor = Color.Red;
        //                        dgv_invoice.Rows[i].Cells[2].Style.Alignment = DataGridViewContentAlignment.TopLeft;
        //                        dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
        //                        dgv_invoice.Rows[i].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
        //                        dgv_invoice.Rows[i].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
        //                        i = i + 1;
        //                        dgv_invoice.Rows.Add("0", "", "", "", "", "", "", "", "", "");
        //                        dgv_invoice.Rows[i].Height = 20;
        //                        dgv_invoice.Rows[i].Cells[2].Style.ForeColor = Color.DimGray;
        //                        dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
        //                        dgv_invoice.Rows[i].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
        //                        i = i + 1;
        //                        dgv_invoice.Rows.Add("0", "", "Total                   Paid", "", "", "", "", "", "", "");
        //                        dgv_invoice.Rows[i].Height = 20;
        //                        dgv_invoice.Rows[i].Cells[2].Style.ForeColor = Color.DimGray;
        //                        dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
        //                        dgv_invoice.Rows[i].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
        //                        i = i + 1;
        //                        dgv_invoice.Rows.Add("0", "", "0000.00        0000.00", "", "", "", "", "", "", "");
        //                        dgv_invoice.Rows[i].Height = 15;
        //                        dgv_invoice.Rows[i].Cells[2].Style.ForeColor = Color.DimGray;
        //                        dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
        //                        dgv_invoice.Rows[i].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
        //                        dgv_invoice.Rows[i].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
        //                    }
        //                    if (k == 1)
        //                    {
        //                        dgv_invoice.Rows[i - 3].Cells[0].Value = dt_pt_sub.Rows[k]["id"].ToString();
        //                        dgv_invoice.Rows[i - 3].Cells[3].Value = dt_pt_sub.Rows[k]["services"].ToString();
        //                        dgv_invoice.Rows[i - 3].Cells[4].Value = String.Format("{0:C2}", Convert.ToDecimal(totalcost));
        //                        dgv_invoice.Rows[i - 3].Cells[5].Value = String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discountin_rs"].ToString())) + discount_string;
        //                        dgv_invoice.Rows[i - 3].Cells[6].Value = String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["tax_inrs"].ToString()));
        //                        dgv_invoice.Rows[i - 3].Cells[7].Value = String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString()));
        //                        dgv_invoice.Rows[i - 3].Cells[8].Value = "";
        //                        dgv_invoice.Rows[i - 3].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
        //                        dgv_invoice.Rows[i - 3].Cells[3].Style.ForeColor = Color.DodgerBlue;
        //                        dgv_invoice.Rows[i - 3].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
        //                        dgv_invoice.Rows[i - 3].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
        //                        dgv_invoice.Rows[i - 2].Cells[3].Value = "Completed by " + Dr_name;
        //                    }
        //                    else if (k == 2)
        //                    {
        //                        dgv_invoice.Rows[i - 1].Cells[0].Value = dt_pt_sub.Rows[k]["id"].ToString();
        //                        dgv_invoice.Rows[i - 1].Cells[3].Value = dt_pt_sub.Rows[k]["services"].ToString();
        //                        dgv_invoice.Rows[i - 1].Cells[4].Value = String.Format("{0:C2}", Convert.ToDecimal(totalcost));
        //                        dgv_invoice.Rows[i - 1].Cells[5].Value = String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discountin_rs"].ToString())) + discount_string;
        //                        dgv_invoice.Rows[i - 1].Cells[6].Value = String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["tax_inrs"].ToString()));
        //                        dgv_invoice.Rows[i - 1].Cells[7].Value = String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString()));
        //                        dgv_invoice.Rows[i - 1].Cells[8].Value = "";
        //                        dgv_invoice.Rows[i - 1].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
        //                        dgv_invoice.Rows[i - 1].Cells[3].Style.ForeColor = Color.DodgerBlue;
        //                        dgv_invoice.Rows[i - 1].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
        //                        dgv_invoice.Rows[i - 1].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
        //                        dgv_invoice.Rows[i].Cells[3].Value = "Completed by " + Dr_name;
        //                    }
        //                    dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
        //                    totalEst = totalEst + Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString());//Balance
        //                    dgv_invoice.Rows[i].Cells[8].Value = PappyjoeMVC.Properties.Resources.blank;
        //                }
        //                i = i + 1;
        //                dgv_invoice.Rows.Add("0", "", "", "", "", "", "", "", "", "");
        //                dgv_invoice.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
        //                dgv_invoice.Rows[i].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
        //                i = i + 1;
        //                decimal gtotal = 0, paid = 0;
        //                //gtotal = Convert.ToDouble(total_cost) - (Convert.ToDouble(total_discount) + Convert.ToDouble(total_tax));
        //                gtotal = (Convert.ToDecimal(total_cost) - Convert.ToDecimal(total_discount)) + Convert.ToDecimal(total_tax);
        //                paid = gtotal - totalEst;
        //                dgv_invoice.Rows[row_no].Cells[2].Value = totalEst.ToString("0.00");
        //                dgv_invoice.Rows[row_no + 3].Cells[2].Value = gtotal.ToString("0.00") + "                  " + paid.ToString("0.00");
        //            }

        //        }
        //        if (dgv_invoice.Rows.Count <= 0)
        //        {
        //            int x = (panel6.Size.Width - Lab_Msg.Size.Width) / 2;
        //            Lab_Msg.Location = new Point(x, Lab_Msg.Location.Y);
        //            Lab_Msg.Show();
        //        }
        //        else
        //        {
        //            Lab_Msg.Hide();
        //        }
        //        btn_ADD.Show();
        //    }
        //}
        public void Load_MainTable_showmore(DataTable dt_invoice_main)
        {

            int i = 0;
            //dgv_invoice.Rows.Clear();
            if (dt_invoice_main.Rows.Count > 0)
            {
                int row = dgv_invoice.Rows.Count;
                foreach(DataRow dr in dt_invoice_main.Rows)// for (int j = 0; j < dt_invoice_main.Rows.Count; j++)
                {
                    dgv_invoice.Rows.Add("0", "", String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(dr["date"].ToString())), "", "", "", "", "", "0", "");
                    dgv_invoice.Rows[row].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Bold);
                    dgv_invoice.Rows[row].Cells[2].Style.ForeColor = Color.DarkGreen;
                    dgv_invoice.Rows[row].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                    dgv_invoice.Rows[row].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
                    row = row + 1;
                    dgv_invoice.Rows.Add(dr["id"].ToString(), "", "INVOICE NUMBER", "TREATMENT & PRODUCTS", "COST", "DISCOUNT", "TAX", "AMOUNT DUE", dr["status"].ToString(), "");
                    dgv_invoice.Rows[row].Cells[1].Value = PappyjoeMVC.Properties.Resources.gry;
                    dgv_invoice.Rows[row].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    dgv_invoice.Rows[row].Cells[2].Style.ForeColor = Color.White;
                    dgv_invoice.Rows[row].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    dgv_invoice.Rows[row].Cells[3].Style.ForeColor = Color.White;
                    dgv_invoice.Rows[row].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    dgv_invoice.Rows[row].Cells[4].Style.ForeColor = Color.White;
                    dgv_invoice.Rows[row].Cells[5].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    dgv_invoice.Rows[row].Cells[5].Style.ForeColor = Color.White;
                    dgv_invoice.Rows[row].Cells[6].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    dgv_invoice.Rows[row].Cells[6].Style.ForeColor = Color.White;
                    dgv_invoice.Rows[row].Cells[7].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    dgv_invoice.Rows[row].Cells[7].Style.ForeColor = Color.White;
                    dgv_invoice.Rows[row].Cells[8].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    dgv_invoice.Rows[row].Cells[8].Style.ForeColor = Color.White;
                    dgv_invoice.Rows[row].Cells[1].Style.BackColor = Color.DarkGray;
                    dgv_invoice.Rows[row].Cells[2].Style.BackColor = Color.DarkGray;
                    dgv_invoice.Rows[row].Cells[3].Style.BackColor = Color.DarkGray;
                    dgv_invoice.Rows[row].Cells[4].Style.BackColor = Color.DarkGray;
                    dgv_invoice.Rows[row].Cells[5].Style.BackColor = Color.DarkGray;
                    dgv_invoice.Rows[row].Cells[6].Style.BackColor = Color.DarkGray;
                    dgv_invoice.Rows[row].Cells[7].Style.BackColor = Color.DarkGray;
                    dgv_invoice.Rows[row].Cells[8].Style.BackColor = Color.DarkGray;
                    dgv_invoice.Rows[row].Cells[9].Value = PappyjoeMVC.Properties.Resources.Bill;
                    if (dr["status"].ToString() != "0")
                    {
                        dgv_invoice.Rows[row].Cells[1].Value = PappyjoeMVC.Properties.Resources.Borderblank;
                    }
                    else
                    {
                        dgv_invoice.Rows[row].Cells[1].Value = PappyjoeMVC.Properties.Resources.billed;
                    }
                    System.Data.DataTable dt_pt_sub = this.cntrl.get_invoiceDetails(dr["id"].ToString());
                    decimal totalEst = 0;
                    Decimal total_cost = 0;
                    Decimal total_discount = 0;
                    Decimal total_tax = 0;
                    int row_no = 0;
                    string discount_string = "";
                    string Dr_name = "";
                    if (dt_pt_sub.Rows.Count > 0)
                    {
                        for (int k = 0; k < dt_pt_sub.Rows.Count; k++)
                        {
                            System.Data.DataTable dt_dr = db.table("SELECT doctor_name FROM tbl_doctor where id='" + dt_pt_sub.Rows[k]["dr_id"].ToString() + "' ORDER BY id");
                            if (dt_dr.Rows.Count > 0)
                            {
                                Dr_name = dt_dr.Rows[0]["doctor_name"].ToString();
                            }
                            if (dt_pt_sub.Rows[k]["discount_type"].ToString() == "INR")
                            {
                                discount_string = "";
                            }
                            else
                            {
                                discount_string = "(" + dt_pt_sub.Rows[k]["discount"].ToString() + "%)";
                            }
                            Decimal totalcost = Convert.ToDecimal(dt_pt_sub.Rows[k]["cost"].ToString()) * Convert.ToDecimal(dt_pt_sub.Rows[k]["unit"].ToString());
                            total_cost = total_cost + Convert.ToDecimal(totalcost);
                            total_discount = total_discount + Convert.ToDecimal(dt_pt_sub.Rows[k]["discountin_rs"].ToString());
                            total_tax = total_tax + Convert.ToDecimal(dt_pt_sub.Rows[k]["tax_inrs"].ToString());
                            if (k == 0 || k > 2)
                            {
                                row = row + 1;
                                if (k == 0)
                                {
                                    dgv_invoice.Rows.Add(dt_pt_sub.Rows[k]["id"].ToString(), "", dr["invoice"].ToString(), dt_pt_sub.Rows[k]["services"].ToString(), String.Format("{0:C2}", Convert.ToDecimal(totalcost)), String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discountin_rs"].ToString())) + discount_string, String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["tax_inrs"].ToString())), String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString())), "");
                                    dgv_invoice.Rows[row].Height = 25;
                                    dgv_invoice.Rows[row].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
                                    dgv_invoice.Rows[row].Cells[2].Style.ForeColor = Color.DodgerBlue;
                                    dgv_invoice.Rows[row].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
                                    dgv_invoice.Rows[row].Cells[3].Style.ForeColor = Color.DodgerBlue;
                                    dgv_invoice.Rows[row].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                                    dgv_invoice.Rows[row].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
                                }
                                else
                                {
                                    dgv_invoice.Rows.Add(dt_pt_sub.Rows[k]["id"].ToString(), "", "", dt_pt_sub.Rows[k]["services"].ToString(), String.Format("{0:C2}", Convert.ToDecimal(totalcost)), String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discountin_rs"].ToString())) + discount_string, String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["tax_inrs"].ToString())), String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString())), "");
                                    dgv_invoice.Rows[row].Height = 25;
                                    dgv_invoice.Rows[row].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
                                    dgv_invoice.Rows[row].Cells[2].Style.ForeColor = Color.DodgerBlue;
                                    dgv_invoice.Rows[row].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
                                    dgv_invoice.Rows[row].Cells[3].Style.ForeColor = Color.DodgerBlue;
                                    dgv_invoice.Rows[row].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                                    dgv_invoice.Rows[row].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
                                    row = row + 1;
                                    dgv_invoice.Rows.Add("0", "", "", "Completed by " + Dr_name, "", "", "", "", "", "");
                                    dgv_invoice.Rows[row].Height = 25;
                                    dgv_invoice.Rows[row].Cells[2].Style.ForeColor = Color.DimGray;
                                    dgv_invoice.Rows[row].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                                    dgv_invoice.Rows[row].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
                                }
                            }
                            if (k == 0)
                            {
                                row = row + 1;
                                dgv_invoice.Rows.Add("0", "", "Balance", "Completed by " + Dr_name, "", "", "", "", "", "");
                                dgv_invoice.Rows[row].Height = 20;
                                dgv_invoice.Rows[row].Cells[2].Style.ForeColor = Color.DimGray;
                                dgv_invoice.Rows[row].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                                dgv_invoice.Rows[row].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
                                row = row + 1;
                                row_no = i;
                                dgv_invoice.Rows.Add("0", "", "0000.00", "", "", "", "", "", "", "");
                                dgv_invoice.Rows[row].Height = 20;
                                dgv_invoice.Rows[row].Cells[2].Style.ForeColor = Color.Red;
                                dgv_invoice.Rows[row].Cells[2].Style.Alignment = DataGridViewContentAlignment.TopLeft;
                                dgv_invoice.Rows[row].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                                dgv_invoice.Rows[row].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
                                dgv_invoice.Rows[row].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                                row = row + 1;
                                dgv_invoice.Rows.Add("0", "", "", "", "", "", "", "", "", "");
                                dgv_invoice.Rows[row].Height = 20;
                                dgv_invoice.Rows[row].Cells[2].Style.ForeColor = Color.DimGray;
                                dgv_invoice.Rows[row].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                                dgv_invoice.Rows[row].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
                                row = row + 1;
                                dgv_invoice.Rows.Add("0", "", "Total                   Paid", "", "", "", "", "", "", "");
                                dgv_invoice.Rows[row].Height = 20;
                                dgv_invoice.Rows[row].Cells[2].Style.ForeColor = Color.DimGray;
                                dgv_invoice.Rows[row].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                                dgv_invoice.Rows[row].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
                                row = row + 1;
                                dgv_invoice.Rows.Add("0", "", "0000.00        0000.00", "", "", "", "", "", "", "");
                                dgv_invoice.Rows[row].Height = 15;
                                dgv_invoice.Rows[row].Cells[2].Style.ForeColor = Color.DimGray;
                                dgv_invoice.Rows[row].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                                dgv_invoice.Rows[row].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
                                dgv_invoice.Rows[row].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                            }
                            if (k == 1)
                            {
                                dgv_invoice.Rows[row - 3].Cells[0].Value = dt_pt_sub.Rows[k]["id"].ToString();
                                dgv_invoice.Rows[row - 3].Cells[3].Value = dt_pt_sub.Rows[k]["services"].ToString();
                                dgv_invoice.Rows[row - 3].Cells[4].Value = String.Format("{0:C2}", Convert.ToDecimal(totalcost));
                                dgv_invoice.Rows[row - 3].Cells[5].Value = String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discountin_rs"].ToString())) + discount_string;
                                dgv_invoice.Rows[row - 3].Cells[6].Value = String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["tax_inrs"].ToString()));
                                dgv_invoice.Rows[row - 3].Cells[7].Value = String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString()));
                                dgv_invoice.Rows[row - 3].Cells[8].Value = "";
                                dgv_invoice.Rows[row - 3].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
                                dgv_invoice.Rows[row - 3].Cells[3].Style.ForeColor = Color.DodgerBlue;
                                dgv_invoice.Rows[row - 3].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                                dgv_invoice.Rows[row - 3].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
                                dgv_invoice.Rows[row - 2].Cells[3].Value = "Completed by " + Dr_name;
                            }
                            else if (k == 2)
                            {
                                dgv_invoice.Rows[row - 1].Cells[0].Value = dt_pt_sub.Rows[k]["id"].ToString();
                                dgv_invoice.Rows[row - 1].Cells[3].Value = dt_pt_sub.Rows[k]["services"].ToString();
                                dgv_invoice.Rows[row - 1].Cells[4].Value = String.Format("{0:C2}", Convert.ToDecimal(totalcost));
                                dgv_invoice.Rows[row - 1].Cells[5].Value = String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discountin_rs"].ToString())) + discount_string;
                                dgv_invoice.Rows[row - 1].Cells[6].Value = String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["tax_inrs"].ToString()));
                                dgv_invoice.Rows[row - 1].Cells[7].Value = String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString()));
                                dgv_invoice.Rows[row - 1].Cells[8].Value = "";
                                dgv_invoice.Rows[row - 1].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
                                dgv_invoice.Rows[row - 1].Cells[3].Style.ForeColor = Color.DodgerBlue;
                                dgv_invoice.Rows[row - 1].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                                dgv_invoice.Rows[row - 1].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
                                dgv_invoice.Rows[row].Cells[3].Value = "Completed by " + Dr_name;
                            }
                            dgv_invoice.Rows[row].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                            totalEst = totalEst + Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString());//Balance
                            dgv_invoice.Rows[i].Cells[8].Value = PappyjoeMVC.Properties.Resources.blank;
                        }
                        row = row + 1;
                        dgv_invoice.Rows.Add("0", "", "", "", "", "", "", "", "", "");
                        dgv_invoice.Rows[row].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                        dgv_invoice.Rows[row].Cells[9].Value = PappyjoeMVC.Properties.Resources.blank;
                        row = row + 1;
                        decimal gtotal = 0, paid = 0;
                        //gtotal = Convert.ToDouble(total_cost) - (Convert.ToDouble(total_discount) + Convert.ToDouble(total_tax));
                        gtotal = (Convert.ToDecimal(total_cost) - Convert.ToDecimal(total_discount)) + Convert.ToDecimal(total_tax);
                        paid = gtotal - totalEst;
                        dgv_invoice.Rows[row_no].Cells[2].Value = totalEst.ToString("0.00");
                        dgv_invoice.Rows[row_no + 3].Cells[2].Value = gtotal.ToString("0.00") + "                  " + paid.ToString("0.00");
                    }

                }
                if (dgv_invoice.Rows.Count <= 0)
                {
                    int x = (panel6.Size.Width - Lab_Msg.Size.Width) / 2;
                    Lab_Msg.Location = new Point(x, Lab_Msg.Location.Y);
                    Lab_Msg.Show();
                }
                else
                {
                    Lab_Msg.Hide();
                }
                btn_ADD.Show();
            }
        }
        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (doctor_id != "1")
                {
                    string id;
                    id = privi_mdl.invoice_delete(doctor_id);// db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category=' EMRI' and Permission='D'");
                    if (int.Parse(id) > 0)
                    {
                        dlt_privilage();
                        string dt_time = DateTime.Now.Date.ToString("yyyy-MM-dd");
                        DateTime Timeonly = DateTime.Now;
                        this.cntrl.save_log(doctor_id, "Invoice", "Deletes Invoice", dt_time, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Delete");
                    }
                    else
                    {
                        MessageBox.Show("There is No Privilege to Delete Invoice", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    }
                }
                else
                {
                    dlt_privilage();
                    string dt_time = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    DateTime Timeonly = DateTime.Now;
                    this.cntrl.save_log(doctor_id, "Invoice", "Deletes Invoice", dt_time, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Delete");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void dlt_privilage()
        {
            try
            {
                if (invoice_plan_id != "0")
                {
                    DialogResult res = MessageBox.Show("Are you sure you want to delete..?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (res == DialogResult.Yes)
                    {
                        string invoice_type = this.cntrl.Get_type(invoice_plan_id);
                        if (invoice_type != "")
                        {
                            if (invoice_type == "drug")
                            {
                                System.Data.DataTable dt_in_main = this.cntrl.InvoiceDetails(invoice_plan_id);
                                if (dt_in_main.Rows.Count > 0)
                                {
                                    for (int j = 0; j < dt_in_main.Rows.Count; j++)
                                    {
                                        decimal total_Stock = 0; decimal current_Stock = 0;
                                        System.Data.DataTable dt3 = inv_model.Get_Stock(dt_in_main.Rows[j]["Plan_id"].ToString());
                                        if (dt3.Rows.Count > 0)
                                        {
                                            total_Stock = decimal.Parse(dt3.Rows[0][0].ToString());
                                        }
                                        total_Stock = total_Stock + decimal.Parse(dt_in_main.Rows[j]["unit"].ToString());
                                        System.Data.DataTable dt33 = this.cntrl.geGet_quantiry_fromStockt(dt_in_main.Rows[j]["completed_id"].ToString());
                                        if (dt33.Rows.Count > 0)
                                        {
                                            current_Stock = decimal.Parse(dt33.Rows[0][0].ToString());
                                        }
                                        current_Stock = current_Stock + decimal.Parse(dt_in_main.Rows[j]["unit"].ToString());
                                        this.cntrl.update_addStock_qty(current_Stock, dt_in_main.Rows[j]["completed_id"].ToString());
                                        this.cntrl.update_inventoryStock(total_Stock, dt_in_main.Rows[j]["Plan_id"].ToString());
                                    }
                                }
                            }
                            else if (Convert.ToString(invoice_type) == "service")
                            {
                                System.Data.DataTable dt_in_main = this.cntrl.InvoiceDetails(invoice_plan_id);
                                if (dt_in_main.Rows.Count > 0)
                                {
                                    for (int j = 0; j < dt_in_main.Rows.Count; j++)
                                    {
                                        this.cntrl.set_completeProcedure_status1(dt_in_main.Rows[j]["completed_id"].ToString());
                                    }
                                }
                            }
                            this.cntrl.delete_from_invoice(invoice_plan_id);
                        }
                        this.cntrl.get_total_payment(patient_id);
                        DataTable dt_invoice_main = this.cntrl.Get_invoice_mainDetails(patient_id);
                        Load_MainTable(dt_invoice_main);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void emailToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            mprintosave();
        }
        public void mprintosave()
        {
            try
            {
                string doct = cmodel.Get_DoctorName(doctor_id);
                string doctor_name = "";
                if (doct != "")
                {
                    doctor_name = doct;
                }
                System.Data.DataTable patient = cmodel.Get_Patient_Details(patient_id);
                string Pname = "", Gender = "", address = "", age = "", Mobile = "";
                if (patient.Rows.Count > 0)
                {
                    Pname = patient.Rows[0]["pt_name"].ToString();
                    Gender = patient.Rows[0]["gender"].ToString();
                    address = patient.Rows[0]["street_address"].ToString() + " , " + patient.Rows[0]["city"].ToString();
                    Mobile = patient.Rows[0]["primary_mobile_number"].ToString();
                    if (patient.Rows[0]["age"].ToString() != "")
                    {
                        age = patient.Rows[0]["age"].ToString();
                    }
                }
                string contact_no = "";
                string clinic_name = "";
                System.Data.DataTable dtp = cmodel.Get_Practice_details();
                if (dtp.Rows.Count > 0)
                {
                    clinic_name = dtp.Rows[0]["name"].ToString();
                    contact_no = dtp.Rows[0]["contact_no"].ToString();
                }
                System.Data.DataTable dt_pt_sub = this.cntrl.get_invoiceDetails(invoice_plan_id);// System.Data.DataTable dt_invoice_main = this.cntrl.Load_invoice_mainDetails(patient_id);
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                StreamWriter sWrite = new StreamWriter(Apppath + "\\InvoicenPrint.html");
                sWrite.WriteLine("<html>");
                sWrite.WriteLine("<head>");
                sWrite.WriteLine("</head>");
                sWrite.WriteLine("<body >");
                sWrite.WriteLine("<br><br><br>");
                sWrite.WriteLine("<table align='center' style='width:900px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr><th align='left'><FONT COLOR=black FACE='Geneva, Arial' SIZE=4>" + clinic_name.ToString() + "</font></th></tr>");
                sWrite.WriteLine("<tr><th align='left'><FONT COLOR=black FACE='Geneva, Arial' SIZE=3>" + contact_no.ToString() + "</font></th></tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr><th align='center'><br><FONT COLOR=black FACE='Geneva, Arial' SIZE=5>INVOICE </font></th></tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<br>");
                sWrite.WriteLine("<table align='center' style='width:900px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("    <td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>Name :<b>" + Pname.ToString() + " </b></font></td>");
                sWrite.WriteLine("	<td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>Age : <b> " + age.ToString() + " </b></font></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine(" <tr>");
                sWrite.WriteLine("    <td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>Address :<b> " + address.ToString() + "</b></font></td>");
                sWrite.WriteLine("	<td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>Gender : <b>" + Gender.ToString() + " </b></font></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine(" <tr>");
                sWrite.WriteLine("    <td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>Mobile No:<b> " + Mobile.ToString() + "</b></font></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine(" <tr>");
                sWrite.WriteLine("    <td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>Invoice No:<b> " + dt_pt_sub.Rows[0]["invoice_no"].ToString() + "</b></font></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<br>");
                sWrite.WriteLine("<hr>");
                sWrite.WriteLine("<table align='center'  style='border: 1px ;border-collapse: collapse;' >");
                System.Data.DataTable dt_invoice_main = this.cntrl.Load_invoice_mainDetails(patient_id);
                if (dt_invoice_main.Rows.Count > 0)
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' colspan='2' >");
                    sWrite.WriteLine("<table align='center'  style='border: 1px ;border-collapse: collapse;' >");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<th align='left' width='350px' bgcolor=gray><FONT COLOR=black FACE='Geneva, Arial' SIZE=2><b>&nbsp;TREATMENT & PRODUCTS</b> </font></th>");
                    sWrite.WriteLine("<th align='right' width='100px' bgcolor=gray><FONT COLOR=black FACE='Geneva, Arial' SIZE=2><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;COST</b> </font></th>");
                    sWrite.WriteLine("<th align='right' width='150px' bgcolor=gray><FONT COLOR=black FACE='Geneva, Arial' SIZE=2><b>&nbsp;DISCOUNT</b> </font></th>");
                    sWrite.WriteLine("<th align='right' width='150px' bgcolor=gray><FONT COLOR=black FACE='Geneva, Arial' SIZE=2><b>&nbsp;&nbsp;TAX</b> </font></th>");
                    sWrite.WriteLine("<th align='right' width='200px' bgcolor=gray><FONT COLOR=black FACE='Geneva, Arial' SIZE=2><b>&nbsp;&nbsp;AMOUNT DUE</b> </font></th>");
                    sWrite.WriteLine("</tr>");
                    Double totalEst = 0;
                    Decimal total_cost = 0;
                    Decimal total_discount = 0;
                    Decimal total_tax = 0;
                    decimal due = 0;
                    string discount_string = "";
                    if (dt_pt_sub.Rows.Count > 0)
                    {
                        for (int k = 0; k < dt_pt_sub.Rows.Count; k++)
                        {
                            if (dt_pt_sub.Rows[k]["discount_type"].ToString() == "INR")
                            {
                                discount_string = "";
                            }
                            else
                            {
                                discount_string = "(" + dt_pt_sub.Rows[k]["discount"].ToString() + "%)";
                            }
                            Decimal totalcost = Convert.ToDecimal(dt_pt_sub.Rows[k]["cost"].ToString()) * Convert.ToDecimal(dt_pt_sub.Rows[k]["unit"].ToString());
                            total_cost = total_cost + Convert.ToDecimal(totalcost);
                            total_discount = total_discount + Convert.ToDecimal(dt_pt_sub.Rows[k]["discountin_rs"].ToString());
                            total_tax = total_tax + Convert.ToDecimal(dt_pt_sub.Rows[k]["tax_inrs"].ToString());
                            totalEst = totalEst + Convert.ToDouble(dt_pt_sub.Rows[k]["total"].ToString());
                            due = Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString());
                            sWrite.WriteLine("<tr>");
                            sWrite.WriteLine("    <td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dt_pt_sub.Rows[k]["services"].ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='right' width='230px' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;" + String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["cost"].ToString())) + "</font></th>");
                            sWrite.WriteLine("    <td align='right' width='230px' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;&nbsp;&nbsp;" + String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discountin_rs"].ToString())) + discount_string + " </font></th>");
                            sWrite.WriteLine("    <td align='right' width='230px' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;&nbsp;&nbsp; " + String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["tax_inrs"].ToString())) + "</font></th>");
                            sWrite.WriteLine("    <td align='right' width='230px' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;&nbsp;&nbsp;" + String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString())) + "</font></th>");
                            sWrite.WriteLine("</tr>");
                        }
                    }
                    Double gtotal = 0, paid = 0;
                    gtotal = Convert.ToDouble(total_cost) - (Convert.ToDouble(total_discount) + Convert.ToDouble(total_tax));
                    paid = gtotal - totalEst;
                    sWrite.WriteLine(" </table>");
                    sWrite.WriteLine(" </td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr >");
                    sWrite.WriteLine("    <td align='left' height='20'><FONT COLOR=black FACE='Geneva, Arial' SIZE=3></font></th>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<table align='center' style='width:900px;border: 1px;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("    <td align='right' width='230px' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;&nbsp;Total :" + String.Format("{0:C2}", Convert.ToDecimal(gtotal)) + "</font></th>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("</table >");
                    sWrite.WriteLine("<table align='center' style='width:900px;border: 1px;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("    <td align='right' width='230px' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;&nbsp;Paid :" + String.Format("{0:C2}", Convert.ToDecimal(paid)) + "</font></th>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("</table >");
                    sWrite.WriteLine("<table align='center' style='width:900px;border: 1px;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("    <td align='right' width='230px' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;&nbsp;Balance :" + String.Format("{0:C2}", Convert.ToDecimal(totalEst)) + "</font></th>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("</table>");
                }
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("</td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("</body>");
                sWrite.WriteLine("</html>");
                sWrite.Close();
                string email = "", emailName = "", emailPass = "";
                System.Data.DataTable sr = cmodel.getpatemail(patient_id);
                if (sr.Rows.Count > 0)
                {
                    email = sr.Rows[0]["email_address"].ToString();
                    if (email != "")
                    {
                        System.Data.DataTable sms = cmodel.send_email();
                        if (sms.Rows.Count > 0)
                        {
                            emailName = sms.Rows[0]["emailName"].ToString();
                            emailPass = sms.Rows[0]["emailPass"].ToString();
                            try
                            {
                                StreamReader reader = new StreamReader(Apppath + "\\InvoicenPrint.html");
                                string readFile = reader.ReadToEnd();
                                string StrContent = "";
                                StrContent = readFile;
                                MailMessage message = new MailMessage();
                                message.From = new MailAddress(email);
                                message.To.Add(email);
                                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                                message.Subject = "Pappyjoe Invoice";
                                message.Body = StrContent.ToString();
                                smtp.UseDefaultCredentials = false;
                                message.IsBodyHtml = true;
                                smtp.UseDefaultCredentials = false;
                                smtp.EnableSsl = true;
                                smtp.Port = 587;
                                smtp.Host = "smtp.gmail.com";
                                smtp.Credentials = new System.Net.NetworkCredential(emailName, emailPass);
                                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                                smtp.Send(message);
                                MessageBox.Show("Email is Sent To : " + email);
                                reader.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please Activate Email Configuration", "Configuration Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Add EmailId for Selected patient", "Email Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_invoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_invoice.Rows.Count > 0 && (dgv_invoice.Rows[e.RowIndex].Cells[8].Value.ToString() == "1" || dgv_invoice.Rows[e.RowIndex].Cells[8].Value.ToString() == "2") && dgv_invoice.Rows[e.RowIndex].Cells[2].Value.ToString() == "INVOICE NUMBER")
            {
                if (e.ColumnIndex == 1)
                {
                    if (dgv_invoice.Rows[e.RowIndex].Cells[8].Value.ToString() == "1")
                    {
                        button_value = button_value + 1;
                        dgv_invoice.Rows[e.RowIndex].Cells[8].Value = "2";
                        dgv_invoice.Rows[e.RowIndex].Cells[1].Value = PappyjoeMVC.Properties.Resources.Bordertick;
                    }
                    else if (dgv_invoice.Rows[e.RowIndex].Cells[8].Value.ToString() == "2")
                    {
                        button_value = button_value - 1;
                        dgv_invoice.Rows[e.RowIndex].Cells[8].Value = "1";
                        dgv_invoice.Rows[e.RowIndex].Cells[1].Value = PappyjoeMVC.Properties.Resources.Borderblank;
                    }
                    if (button_value > 0)
                    { btn_paySelectedInvoice.Enabled = true; }
                    else
                    { btn_paySelectedInvoice.Enabled = false; }
                }
            }
        }

        private void dgv_invoice_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int currentMouseOverRow = dgv_invoice.HitTest(e.X, e.Y).RowIndex;
                int currentMouseOverColumn = dgv_invoice.HitTest(e.X, e.Y).ColumnIndex;
                if (currentMouseOverRow >= 0)
                {
                    if (currentMouseOverColumn == 9)
                    {
                        if (dgv_invoice.Rows[currentMouseOverRow].Cells[8].Value.ToString() == "0" && dgv_invoice.Rows[currentMouseOverRow].Cells[2].Value.ToString() == "INVOICE NUMBER")
                        {
                            invoice_plan_id = dgv_invoice.Rows[currentMouseOverRow].Cells[0].Value.ToString();
                            deleteToolStripMenuItem1.Enabled = false;
                            contextMenuStrip1.Show(dgv_invoice, new System.Drawing.Point(915 - 120, e.Y));
                        }
                        else if (dgv_invoice.Rows[currentMouseOverRow].Cells[0].Value.ToString() != "0" && dgv_invoice.Rows[currentMouseOverRow].Cells[2].Value.ToString() == "INVOICE NUMBER")
                        {
                            invoice_plan_id = dgv_invoice.Rows[currentMouseOverRow].Cells[0].Value.ToString();
                            string dt_pay = this.cntrl.Get_paymentid(dgv_invoice.Rows[currentMouseOverRow + 1].Cells[2].Value.ToString());
                            if (Convert.ToInt32(dt_pay) > 0)
                            {
                                deleteToolStripMenuItem1.Enabled = false;
                            }
                            else
                            {
                                deleteToolStripMenuItem1.Enabled = true;
                            }
                            contextMenuStrip1.Show(dgv_invoice, new System.Drawing.Point(915 - 120, e.Y));
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error Data...!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Data.DataTable print = this.cntrl.Get_invoicePrintsettings();
            if (print.Rows.Count > 0)
            {
                combo_topmargin = print.Rows[0][4].ToString();
                combo_leftmargin = print.Rows[0][5].ToString();
                combo_bottommargin = print.Rows[0][6].ToString();
                combo_rightmargin = print.Rows[0][7].ToString();
                combo_paper_size = print.Rows[0][1].ToString();
                combo_footer_topmargin = print.Rows[0][22].ToString();
                rich_fullwidth = print.Rows[0][23].ToString();
                rich_leftsign = print.Rows[0][24].ToString();
                rich_rightsign = print.Rows[0][25].ToString();
                patient_details = print.Rows[0][14].ToString();
                med = print.Rows[0][15].ToString();
                patient = print.Rows[0][16].ToString();
                address = print.Rows[0][17].ToString();
                phone = print.Rows[0][18].ToString();
                blood = print.Rows[0][20].ToString();
                gender = print.Rows[0][21].ToString();
                orientation = print.Rows[0][2].ToString();
                includeheader = print.Rows[0]["include_header"].ToString();
                includelogo = print.Rows[0]["include_logo"].ToString();
                if(print.Rows[0][1].ToString()=="Thermal Print")
                {
                    printhtml_thermal();
                }
                else
                {
                    printhtml();
                }
            }
           
        }
        public void printhtml_thermal()
        {
            try
            {
               
                string str_druglicenseno = "";
                string str_taxno = "";
                System.Data.DataTable dtp = cmodel.Get_practiceDlNumber();
                if (dtp.Rows.Count > 0)
                {
                    str_druglicenseno = dtp.Rows[0]["Dl_Number"].ToString();
                    str_taxno = dtp.Rows[0]["Dl_Number2"].ToString();
                    logo_name = dtp.Rows[0]["path"].ToString();
                }
                string strfooter1 = "";
                string strfooter2 = "";
                string strfooter3 = "";
                string header1 = "";
                string header2 = "";
                string header3 = "";
                System.Data.DataTable print = this.cntrl.Get_invoicePrintsettings();
                if (print.Rows.Count > 0)
                {
                    header1 = print.Rows[0]["header"].ToString();
                    header2 = print.Rows[0]["left_text"].ToString();
                    header3 = print.Rows[0]["right_text"].ToString();
                    strfooter1 = print.Rows[0]["fullwidth_context"].ToString();
                    strfooter2 = print.Rows[0]["left_sign"].ToString();
                    strfooter3 = print.Rows[0]["right_sign"].ToString();
                }
                string doctorname = "";
                string strinvoice = "";
                string strdate = "";
                System.Data.DataTable dt_cf = this.cntrl.get_invoice_doctorname(patient_id);
                if (dt_cf.Rows.Count > 0)
                {
                    doctorname = Convert.ToString(dt_cf.Rows[0]["doctor_name"].ToString());
                }
                System.Data.DataTable dt_invo = this.cntrl.Get_date(invoice_plan_id);
                if (dt_invo.Rows.Count > 0)
                {
                    strinvoice = Convert.ToString(dt_invo.Rows[0]["invoice"].ToString());
                    strdate = Convert.ToString(dt_invo.Rows[0]["date"].ToString());
                }
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                StreamWriter sWrite = new StreamWriter(Apppath + "\\invoice_thermal_print.html");
                sWrite.WriteLine("<html>");
                sWrite.WriteLine("<head>");
                sWrite.WriteLine("</head>");
                sWrite.WriteLine("<body >");
                if (includelogo == "1")
                {
                    if (logo != null || logo_name != "")
                    {
                        string Appath = System.IO.Directory.GetCurrentDirectory();
                        if (File.Exists(Appath + "\\" + logo_name))
                        {
                            sWrite.WriteLine("<table align='center' style='width:270px;border: 1px ;border-collapse: collapse;'>");
                            sWrite.WriteLine("<tr>");
                            sWrite.WriteLine("<td width='100' height='75px' align='center' ><img src='" + Appath + "\\" + logo_name + "' width='77' height='25px' style='width:100px;height:100px;'></td></tr>  ");//rowspan='4'
                            sWrite.WriteLine("<tr><td  align='center' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=4><b>&nbsp;&nbsp;" + header1 + "</b></font></td></tr>");
                            sWrite.WriteLine("<tr><td  align='center' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>&nbsp;&nbsp;" + header2 + "</b></font></td></tr>");
                            sWrite.WriteLine("<tr><td align='center' height='20' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=1><b>&nbsp;&nbsp;" + header3 + "</b></font></td></tr>");
                            sWrite.WriteLine("<tr><td align='center' height='20' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=1><b>&nbsp;&nbsp;" + str_druglicenseno + "&nbsp;&nbsp; " + str_taxno + "</b></font></td></tr>");
                            sWrite.WriteLine("<tr><td align='center' colspan='2'><hr/></td></tr>");
                            sWrite.WriteLine("</table>");
                        }
                        else
                        {
                            sWrite.WriteLine("<table align='center' style='width:270px;border: 1px ;border-collapse: collapse;'>");
                            sWrite.WriteLine("<tr>");
                            sWrite.WriteLine("<td  align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + header1 + "</font></td></tr>");
                            sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;&nbsp;" + header2 + "</font></td></tr>");
                            sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + header3 + "</font></td></tr>");
                            sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + str_druglicenseno + "&nbsp;&nbsp; " + str_taxno + "</font></td></tr>");
                            sWrite.WriteLine("<tr><td align='left' colspan='2' border: 1px><hr/></td></tr>");
                            sWrite.WriteLine("</table>");
                        }
                    }
                    else
                    {
                        sWrite.WriteLine("<table align='center' style='width:270px;border: 1px ;border-collapse: collapse;'>");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td  align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + header1 + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;&nbsp;" + header2 + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + header3 + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + str_druglicenseno + "&nbsp;&nbsp; " + str_taxno + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                        sWrite.WriteLine("</table>");
                    }
                }
                else
                {
                    sWrite.WriteLine("<table align='center' style='width:270px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td  align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + header1 + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;&nbsp;" + header2 + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + header3 + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + str_druglicenseno + "&nbsp;&nbsp; " + str_taxno + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                    sWrite.WriteLine("</table>");
                }
                sWrite.WriteLine("<table align='center' style='width:270px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr><th align='center'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3><b>INVOICE </b></font></th></tr>");
                sWrite.WriteLine("</table>");
                string sexage = "", gen = "";
                System.Data.DataTable dt1 = cmodel.Get_Patient_Details(patient_id);
                if (dt1.Rows.Count > 0)
                {
                    sWrite.WriteLine("<table align='center' style='width:270px;border: 1px ;border-collapse: collapse;'>");
                    if (dt1.Rows[0]["gender"].ToString() != "")
                    {
                        gen = dt1.Rows[0]["gender"].ToString();
                    }
                    if (dt1.Rows[0]["age"].ToString() != "")
                    {
                        sexage = dt1.Rows[0]["age"].ToString() + " Years";
                    }
                    sWrite.WriteLine("<tr><td align='left' height='20px' valign='top' > <FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> <FONT COLOR=black>Inv No : </FONT>" + strinvoice + "</font> </td> <td align='right' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> <FONT COLOR=black>Date : </FONT>" + DateTime.Parse(strdate).ToString("dd MMM yyyy") + "</font></td></tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine(" <td align='left' height=20><FONT COLOR=black FACE='Segoe UI' SIZE=2>Name: <b>" + dt1.Rows[0]["pt_name"].ToString() + "</b></font></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' height=20><FONT COLOR=black FACE='Segoe UI' SIZE=2>Id:" + dt1.Rows[0]["pt_id"].ToString() + " </font></td>");
                    sWrite.WriteLine("<td align='right' height=20><FONT COLOR=black FACE='Segoe UI' SIZE=2>Age:" + sexage + " </font></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' height=20><FONT COLOR=black FACE='Segoe UI' SIZE=2>Sex:" + gen + " </font></td>");
                    sWrite.WriteLine("<td align='right' height=20><FONT COLOR=black FACE='Segoe UI' SIZE=2>Mobile:" + dt1.Rows[0]["primary_mobile_number"].ToString() + " </font></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine("<tr><td colspan=2><hr></td></tr>");
                    sWrite.WriteLine("</table>");
                }
                // Discount and Tax Check
                string discount_check = "";
                string tax_check = "";
                System.Data.DataTable dt_invoice_check = this.cntrl.get_invoiceDetails(invoice_plan_id);
                if (dt_invoice_check.Rows.Count > 0)
                {
                    k = 0;
                    while (k < dt_invoice_check.Rows.Count)
                    {
                        if (decimal.Parse(dt_invoice_check.Rows[k]["discountin_rs"].ToString()) > 0 && discount_check == "")
                        {
                            discount_check = "Discount";
                        }
                        if (decimal.Parse(dt_invoice_check.Rows[k]["total_tax"].ToString()) > 0 && tax_check == "")
                        {
                            tax_check = "Tax";
                        }
                        k = k + 1;
                    }
                }
                // Close
                sWrite.WriteLine("<table align='center'   style='width:270px;border: 1px ;border-collapse: collapse;' >");
                sWrite.WriteLine("<tr style='background:#999999'>");
                sWrite.WriteLine("<td align='left' width='100px' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Segoe UI' SIZE=2><b>&nbsp;Item</b></font></td>");
                sWrite.WriteLine("<td align='left' width='93px' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Arial' SIZE=2>&nbsp;SAC </font></td>");
                sWrite.WriteLine("<td align='left' width='25px' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Segoe UI' SIZE=2><b>&nbsp;Qty</b></font></td>");
                sWrite.WriteLine("<td align='right' width='80px' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Segoe UI' SIZE=2><b>&nbsp;Cost </b></font></td>");
             //   sWrite.WriteLine("<td align='right' width='93px' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Arial' SIZE=3>&nbsp;Cost </font></td>");
                sWrite.WriteLine("<td align='right' width='40px' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Segoe UI' SIZE=2><b>&nbsp;Tax </b></font></td>");
                sWrite.WriteLine("<td align='right' width='93px' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Segoe UI' SIZE=2><b>&nbsp;Total</b> </font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr><td colspan=6><hr></td></tr>");
                decimal cost = 0, tax = 0, discount = 0, total = 0, totalPaid = 0;
                System.Data.DataTable dt_prescription = this.cntrl.get_invoiceDetails(invoice_plan_id);
                Decimal ValBalance1 = 0;
                if (dt_prescription.Rows.Count > 0)
                {
                    k = 0;
                    while (k < dt_prescription.Rows.Count)
                    {
                        Decimal totalcost = Convert.ToDecimal(dt_prescription.Rows[k]["cost"].ToString()) * Convert.ToDecimal(dt_prescription.Rows[k]["unit"].ToString());
                        ValBalance1 = ValBalance1 + totalcost;
                        Decimal subtotalcost = (Convert.ToDecimal(totalcost.ToString()) - (Convert.ToDecimal(dt_prescription.Rows[k]["discountin_rs"].ToString())) + Convert.ToDecimal(dt_prescription.Rows[k]["total_tax"].ToString()));
                        Decimal qty = Convert.ToDecimal(dt_prescription.Rows[k]["unit"].ToString());
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;" + dt_prescription.Rows[k]["services"].ToString() + " </font></td>");
                        sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=1>&nbsp;" + dt_prescription.Rows[k]["sac"].ToString()+ " </font></td>");
                        sWrite.WriteLine("<td align='left' height='30'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;" + qty + " </font></td>");
                        sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;" + String.Format("{0:C2}", decimal.Parse(totalcost.ToString())) + " </font></td>");
                        if (tax_check == "Tax")
                        {
                            sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;" + String.Format("{0:C2}", decimal.Parse(dt_prescription.Rows[k]["total_tax"].ToString())) + " </font></td>");
                        }
                        else
                        {
                            sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp; </font></td>");
                        }
                        sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;" + String.Format("{0:C2}", decimal.Parse(subtotalcost.ToString())) + " </font></td>");
                        sWrite.WriteLine("</tr>");
                        cost = cost + decimal.Parse(totalcost.ToString());
                        tax = tax + decimal.Parse(dt_prescription.Rows[k]["total_tax"].ToString());
                        discount = discount + decimal.Parse(dt_prescription.Rows[k]["discountin_rs"].ToString());
                        total = total + decimal.Parse(subtotalcost.ToString());
                        k++;
                    }
                }
                string num;
                num = this.cntrl.Get_TotalSum(invoice_plan_id);
                decimal numValBalance = Convert.ToDecimal(num.ToString());
                totalPaid = ValBalance1 - (numValBalance + discount);
                sWrite.WriteLine("<table align='center'   style='width:270px;border: 1px ;border-collapse: collapse;' >");
                sWrite.WriteLine("<tr><td align='left' colspan=5><hr/></td></tr>");
                sWrite.WriteLine("<tr >");
                sWrite.WriteLine("<td  colspan=4  align='Right' width='100px' style='text-align:Right' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;" + "Total Cost :" + " </font></td>");
                sWrite.WriteLine("<td align='Right' width='100px'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1><b>&nbsp;" + String.Format("{0:C2}", decimal.Parse(cost.ToString())) + " </b></font></td>");
                sWrite.WriteLine("</tr>");
                if (decimal.Parse(discount.ToString()) > 0)
                {
                    sWrite.WriteLine("<tr >");
                    sWrite.WriteLine("<td colspan=4 align='Right' width='100px' style='text-align:Right'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;" + "Total Discount :" + " </font></td>");
                    sWrite.WriteLine("<td align='Right'   width='100px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1><b>&nbsp;" + String.Format("{0:C2}", decimal.Parse(discount.ToString())) + "</b> </font></td>");
                    sWrite.WriteLine("</tr>");
                }
                sWrite.WriteLine("<tr >");
                sWrite.WriteLine("<td colspan=4 align='Right' width='100px' style='text-align:Right'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;" + "Grand Total :" + " </font></td>");
                sWrite.WriteLine("<td align='Right'   width='100px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;<b>" + String.Format("{0:C2}", decimal.Parse(total.ToString())) + " </b></font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<table align='center' style='width:270px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr><th align='center'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>Thankyou</font></th></tr>");
                sWrite.WriteLine("</table>");
                //footer
                sWrite.WriteLine("<table align='center'   style='width:270px;border: 1px ;border-collapse: collapse;' >");
                sWrite.WriteLine("<tr><td align='left' ><hr/></td></tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='center' height='22'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + strfooter1 + "</font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='center' height='22'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + strfooter2 + "</font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='center' height='22'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + strfooter3 + "</font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<script>window.print();</script>");
                sWrite.WriteLine("</body >");
                sWrite.WriteLine("</html>");
                sWrite.Close();
                System.Diagnostics.Process.Start(Apppath + "\\invoice_thermal_print.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void printhtml()
        {
            try
            {
                string clinicn = "";
                string Clinic = "";
                string streetaddress = "";
                string contact_no = "";
                string str_locality = "";
                string str_pincode = "";
                string str_email = "";
                string str_website = "";
                string str_druglicenseno = "";
                string str_taxno = "";
                System.Data.DataTable dtp = cmodel.Get_practiceDlNumber();
                if (dtp.Rows.Count > 0)
                {
                    clinicn = dtp.Rows[0][0].ToString();
                    Clinic = clinicn.Replace("¤", "'");
                    streetaddress = dtp.Rows[0]["street_address"].ToString();
                    str_locality = dtp.Rows[0]["locality"].ToString();
                    str_pincode = dtp.Rows[0]["pincode"].ToString();
                    contact_no = dtp.Rows[0]["contact_no"].ToString();
                    str_email = dtp.Rows[0]["email"].ToString();
                    str_website = dtp.Rows[0]["website"].ToString();
                    str_druglicenseno = dtp.Rows[0]["Dl_Number"].ToString();
                    str_taxno = dtp.Rows[0]["Dl_Number2"].ToString();
                    logo_name = dtp.Rows[0]["path"].ToString();
                }
                string strfooter1 = "";
                string strfooter2 = "";
                string strfooter3 = "";
                string header1 = "";
                string header2 = "";
                string header3 = "";
                System.Data.DataTable print = this.cntrl.Get_invoicePrintsettings();
                if (print.Rows.Count > 0)
                {
                    header1 = print.Rows[0]["header"].ToString();
                    header2 = print.Rows[0]["left_text"].ToString();
                    header3 = print.Rows[0]["right_text"].ToString();
                    strfooter1 = print.Rows[0]["fullwidth_context"].ToString();
                    strfooter2 = print.Rows[0]["left_sign"].ToString();
                    strfooter3 = print.Rows[0]["right_sign"].ToString();
                }
                string doctorname = "";
                string strinvoice = "";
                string strdate = "";
                System.Data.DataTable dt_cf = this.cntrl.get_invoice_doctorname(patient_id);
                if (dt_cf.Rows.Count > 0)
                {
                    doctorname = Convert.ToString(dt_cf.Rows[0]["doctor_name"].ToString());
                }
                System.Data.DataTable dt_invo = this.cntrl.Get_date(invoice_plan_id);
                if (dt_invo.Rows.Count > 0)
                {
                    strinvoice = Convert.ToString(dt_invo.Rows[0]["invoice"].ToString());
                    strdate = Convert.ToString(dt_invo.Rows[0]["date"].ToString());
                }
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                StreamWriter sWrite = new StreamWriter(Apppath + "\\invoice_print.html");
                sWrite.WriteLine("<html>");
                sWrite.WriteLine("<head>");
                sWrite.WriteLine("</head>");
                sWrite.WriteLine("<body >");
                sWrite.WriteLine("<br>");
                sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr><th align='center'><br><FONT COLOR=black FACE='Geneva, Arial' SIZE=5>INVOICE </font></th></tr>");
                sWrite.WriteLine("</table>");
                if (includeheader == "1")
                {
                    if (includelogo == "1")
                    {
                        if (logo != null || logo_name != "")
                        {
                            string Appath = System.IO.Directory.GetCurrentDirectory();
                            if (File.Exists(Appath + "\\" + logo_name))
                            {
                                sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                                sWrite.WriteLine("<tr>");
                                sWrite.WriteLine("<td width='100' height='75px' align='left' rowspan='4'><img src='" + Appath + "\\" + logo_name + "' width='77' height='25px' style='width:100px;height:100px;'></td>  ");
                                sWrite.WriteLine("<td width='588' align='left' height='25px'><FONT  COLOR=black  face='Arial' SIZE=5>&nbsp;" + header1 + "</font></td></tr>");
                                sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Arial' SIZE=3>&nbsp;&nbsp;" + header2 + "</font></td></tr>");
                                sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Arial' SIZE=2>&nbsp;&nbsp;" + header3 + "</font></td></tr>");
                                sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Arial' SIZE=2>&nbsp;&nbsp;" + str_druglicenseno + "&nbsp;&nbsp; " + str_taxno + "</font></td></tr>");
                                sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                                sWrite.WriteLine("<tr><td align='left' height='20px' valign='top' > <FONT COLOR=black FACE='Geneva, Arial' SIZE=1> <FONT COLOR=black>Invoice No: </FONT>" + strinvoice + "</font> </td> <td align='right' ><FONT COLOR=black FACE=' Arial' SIZE=1> <FONT COLOR=black>Date : </FONT>" + DateTime.Parse(strdate).ToString("dd MMM yyyy") + "</font></td></tr>");
                                sWrite.WriteLine("</table>");
                            }
                            else
                            {
                                sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                                sWrite.WriteLine("<tr>");
                                sWrite.WriteLine("<td  align='left' height='25px'><FONT  COLOR=black  face='Arial' SIZE=5>&nbsp;" + header1 + "</font></td></tr>");
                                sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Arial' SIZE=3>&nbsp;&nbsp;" + header2 + "</font></td></tr>");
                                sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Arial' SIZE=2>&nbsp;&nbsp;" + header3 + "</font></td></tr>");
                                sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Arial' SIZE=2>&nbsp;&nbsp;" + str_druglicenseno + "&nbsp;&nbsp; " + str_taxno + "</font></td></tr>");
                                sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                                sWrite.WriteLine("<tr><td align='left' height='20px' valign='top' > <FONT COLOR=black FACE='Geneva, Arial' SIZE=1> <FONT COLOR=black>Invoice No: </FONT>" + strinvoice + "</font> </td> <td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=1> <FONT COLOR=black>Date : </FONT>" + DateTime.Parse(strdate).ToString("dd MMM yyyy") + "</font></td></tr>");
                                sWrite.WriteLine("</table>");
                            }
                        }
                        else
                        {
                            sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                            sWrite.WriteLine("<tr>");
                            sWrite.WriteLine("<td  align='left' height='25px'><FONT  COLOR=black  face='Arial' SIZE=5>&nbsp;" + header1 + "</font></td></tr>");
                            sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Arial' SIZE=3>&nbsp;&nbsp;" + header2 + "</font></td></tr>");
                            sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Arial' SIZE=2>&nbsp;&nbsp;" + header3 + "</font></td></tr>");
                            sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Arial' SIZE=2>&nbsp;&nbsp;" + str_druglicenseno + "&nbsp;&nbsp; " + str_taxno + "</font></td></tr>");
                            sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                            sWrite.WriteLine("<tr><td align='left' height='20px' valign='top' > <FONT COLOR=black FACE='Geneva, Arial' SIZE=1> <FONT COLOR=black>Invoice No: </FONT>" + strinvoice + "</font> </td> <td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=1> <FONT COLOR=black>Date : </FONT>" + DateTime.Parse(strdate).ToString("dd MMM yyyy") + "</font></td></tr>");
                            sWrite.WriteLine("</table>");
                        }
                    }
                    else
                    {
                        sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td  align='left' height='25px'><FONT  COLOR=black  face='Arial' SIZE=5>&nbsp;" + header1 + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Arial' SIZE=3>&nbsp;&nbsp;" + header2 + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Arial' SIZE=2>&nbsp;&nbsp;" + header3 + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Arial' SIZE=2>&nbsp;&nbsp;" + str_druglicenseno + "&nbsp;&nbsp; " + str_taxno + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                        sWrite.WriteLine("<tr><td align='left' height='20px' valign='top' > <FONT COLOR=black FACE='Geneva, Arial' SIZE=1> <FONT COLOR=black>Invoice No: </FONT>" + strinvoice + "</font> </td> <td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=1> <FONT COLOR=black>Date : </FONT>" + DateTime.Parse(strdate).ToString("dd MMM yyyy") + "</font></td></tr>");
                        sWrite.WriteLine("</table>");
                    }
                }
                else
                {
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");

                    sWrite.WriteLine("<td align='left' height='15px' > <FONT COLOR=black FACE='Geneva, Arial' SIZE=2> <FONT COLOR=black>Invoice No: </FONT>" + strinvoice + "</font> </td> <td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2> <FONT COLOR=black>Date : </FONT>" + DateTime.Parse(strdate).ToString("dd MMM yyyy") + "</font></td></tr>");
                    sWrite.WriteLine("</table>");
                }
                int Dexist = 0;
                string sexage = "";
                string address = "";
                address = "";
                System.Data.DataTable dt1 = cmodel.Get_Patient_Details(patient_id);
                if (dt1.Rows.Count > 0)
                {
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
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
                    sWrite.WriteLine(" <td align='left' height=20><FONT COLOR=black FACE='Arial' SIZE=2><b>" + dt1.Rows[0]["pt_name"].ToString() + "</b><i> (" + sexage + ")</i></font></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' height=20><FONT COLOR=black FACE='Arial' SIZE=2>Patient Id:" + dt1.Rows[0]["pt_id"].ToString() + " </font></td>");
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
                        sWrite.WriteLine("<td align='left' height=20><FONT COLOR=black FACE=' Arial' SIZE=2>" + address + " </font></td>");
                        sWrite.WriteLine(" </tr>");
                    }
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' height=20><FONT COLOR=black FACE=' Arial' SIZE=2>" + dt1.Rows[0]["primary_mobile_number"].ToString() + " </font></td>");
                    sWrite.WriteLine(" </tr>");
                    if (dt1.Rows[0]["email_address"].ToString() != "")
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='left' height=20><FONT COLOR=black FACE=' Arial' SIZE=2>" + dt1.Rows[0]["email_address"].ToString() + " </font></td>");
                        sWrite.WriteLine(" </tr>");
                    }
                    sWrite.WriteLine("<tr><td colspan=2><hr></td></tr>");
                    sWrite.WriteLine("</table>");
                }
                // Discount and Tax Check
                string discount_check = "";
                string tax_check = "";
                System.Data.DataTable dt_invoice_check = this.cntrl.get_invoiceDetails(invoice_plan_id);
                if (dt_invoice_check.Rows.Count > 0)
                {
                    k = 0;
                    while (k < dt_invoice_check.Rows.Count)
                    {
                        if (decimal.Parse(dt_invoice_check.Rows[k]["discountin_rs"].ToString()) > 0 && discount_check == "")
                        {
                            discount_check = "Discount";
                        }
                        if (decimal.Parse(dt_invoice_check.Rows[k]["total_tax"].ToString()) > 0 && tax_check == "")
                        {
                            tax_check = "Tax";
                        }
                        k = k + 1;
                    }
                }
                // Close
                sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");
                sWrite.WriteLine("<tr style='background:#999999'>");
                sWrite.WriteLine("<td align='left' width='35px' height='30' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Arial' SIZE=3>&nbsp;Sl.</font></td>");
                sWrite.WriteLine("<td align='left' width='316px' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Arial' SIZE=3>&nbsp;Services</font></td>");
                sWrite.WriteLine("<td align='left' width='93px' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Arial' SIZE=3>&nbsp;SAC </font></td>");
                sWrite.WriteLine("<td align='right' width='93px' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Arial' SIZE=3>&nbsp;Cost </font></td>");
                sWrite.WriteLine("<td align='right' width='78px' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Arial' SIZE=3>&nbsp;" + discount_check + "&nbsp;</font></td>");
                sWrite.WriteLine("<td align='right' width='52px' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Arial' SIZE=3>&nbsp;" + tax_check + "&nbsp;</font></td>");
                sWrite.WriteLine("<td align='right' width='91px' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Arial' SIZE=3>&nbsp;Total&nbsp;</font></td>");
                sWrite.WriteLine("</tr>");
                decimal cost = 0, tax = 0, discount = 0, total = 0, totalPaid = 0;
                System.Data.DataTable dt_prescription = this.cntrl.get_invoiceDetails(invoice_plan_id);
                Decimal ValBalance1 = 0;
                if (dt_prescription.Rows.Count > 0)
                {
                    k = 0;
                    while (k < dt_prescription.Rows.Count)
                    {
                        Decimal totalcost = Convert.ToDecimal(dt_prescription.Rows[k]["cost"].ToString()) * Convert.ToDecimal(dt_prescription.Rows[k]["unit"].ToString());
                        ValBalance1 = ValBalance1 + totalcost;
                        Decimal subtotalcost = (Convert.ToDecimal(totalcost.ToString()) - (Convert.ToDecimal(dt_prescription.Rows[k]["discountin_rs"].ToString())) + Convert.ToDecimal(dt_prescription.Rows[k]["total_tax"].ToString()));
                        Decimal qty = Convert.ToDecimal(dt_prescription.Rows[k]["unit"].ToString());
                        string str_qty = "";
                        if (qty > 1)
                        {
                            str_qty = " [Qty:" + Convert.ToDecimal(dt_prescription.Rows[k]["unit"].ToString()) + "]";
                        }
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='left' height='30'  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + Convert.ToString(k + 1) + " </font></td>");
                        sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + dt_prescription.Rows[k]["services"].ToString() + str_qty + " </font></td>");
                        sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + dt_prescription.Rows[k]["sac"].ToString() + str_qty + " </font></td>");
                        sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(totalcost.ToString())) + " </font></td>");
                        if (discount_check == "Discount")
                        {
                            sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(dt_prescription.Rows[k]["discountin_rs"].ToString())) + " </font></td>");
                        }
                        else
                        {
                            sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp; </font></td>");
                        }
                        if (tax_check == "Tax")
                        {
                            sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(dt_prescription.Rows[k]["total_tax"].ToString())) + " </font></td>");
                        }
                        else
                        {
                            sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp; </font></td>");
                        }

                        sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(subtotalcost.ToString())) + " </font></td>");
                        sWrite.WriteLine("</tr>");
                        if (dt_prescription.Rows[k]["notes"].ToString() != "")
                        {
                            sWrite.WriteLine("<tr>");
                            sWrite.WriteLine("<td align='left' height='10'  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;</font></td>");
                            sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=1><i>&nbsp;(" + dt_prescription.Rows[k]["notes"].ToString() + ")</i> </font></td>");
                            sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2> </font></td>");
                            sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2></font></td>");
                            sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2></font></td>");
                            sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2></font></td>");
                            sWrite.WriteLine("</tr>");
                            sWrite.WriteLine("</table>");
                        }
                        cost = cost + decimal.Parse(totalcost.ToString());
                        tax = tax + decimal.Parse(dt_prescription.Rows[k]["total_tax"].ToString());
                        discount = discount + decimal.Parse(dt_prescription.Rows[k]["discountin_rs"].ToString());
                        total = total + decimal.Parse(subtotalcost.ToString());
                        k++;
                    }
                }
                string num;
                num = this.cntrl.Get_TotalSum(invoice_plan_id);
                decimal numValBalance = Convert.ToDecimal(num.ToString());
                totalPaid = ValBalance1 - (numValBalance + discount);

                sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");

                sWrite.WriteLine("<tr><td align='left' colspan=6><hr/></td></tr>");
                sWrite.WriteLine("<tr >");
                sWrite.WriteLine("<td align='Right' width='500px'  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp; </font></td>");
                sWrite.WriteLine("<td align='Right' width='100px' style='text-align:left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + "Total Cost :" + " </font></td>");
                sWrite.WriteLine("<td align='Right' width='100px'  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(cost.ToString())) + " </font></td>");
                sWrite.WriteLine("</tr>");
                if (decimal.Parse(tax.ToString()) > 0)
                {
                    sWrite.WriteLine("<tr >");
                    sWrite.WriteLine("<td align='Right' width='500px'  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp; </font></td>");

                    sWrite.WriteLine("<td align='Right' width='100px' style='text-align:left'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + "Total Tax :" + " </font></td>");
                    sWrite.WriteLine("<td align='Right'   width='100px'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(tax.ToString())) + " </font></td>");
                    sWrite.WriteLine("</tr>");
                }
                if (decimal.Parse(discount.ToString()) > 0)
                {
                    sWrite.WriteLine("<tr >");
                    sWrite.WriteLine("<td align='Right' width='500px'  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp; </font></td>");

                    sWrite.WriteLine("<td align='Right' width='100px' style='text-align:left'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + "Total Discount :" + " </font></td>");
                    sWrite.WriteLine("<td align='Right'   width='100px'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(discount.ToString())) + " </font></td>");
                    sWrite.WriteLine("</tr>");
                }
                sWrite.WriteLine("<tr >");
                sWrite.WriteLine("<td align='Right' width='500px'  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp; </font></td>");

                sWrite.WriteLine("<td align='Right' width='100px' style='text-align:left'><FONT COLOR=black FACE='Geneva, Arial' SIZE=3>&nbsp;" + "Grand Total :" + " </font></td>");
                sWrite.WriteLine("<td align='Right'   width='100px'><FONT COLOR=black FACE='Geneva, Arial' SIZE=3>&nbsp;<b>" + String.Format("{0:C2}", decimal.Parse(total.ToString())) + " </b></font></td>");
                sWrite.WriteLine("</tr>");
                //sWrite.WriteLine("<tr >");
                //sWrite.WriteLine("<td align='Right' width='500px'  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp; </font></td>");

                //sWrite.WriteLine("<td align='Right' width='100px' style='text-align:left'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + "Total Paid :" + " </font></td>");
                //sWrite.WriteLine("<td align='Right'   width='100px'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(totalPaid.ToString())) + " </font></td>");
                //sWrite.WriteLine("</tr>");
                //sWrite.WriteLine("<tr >");
                //sWrite.WriteLine("<td align='Right' width='500px'  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp; </font></td>");

                //sWrite.WriteLine("<td align='Right' width='100px' style='text-align:left'><FONT COLOR=black FACE='Geneva, Arial' SIZE=3>&nbsp;" + "Balance :" + " </font></td>");
                //sWrite.WriteLine("<td align='Right'   width='100px'><FONT COLOR=black FACE='Geneva, Arial' SIZE=3>&nbsp;<b>" + String.Format("{0:C2}", decimal.Parse(numValBalance.ToString())) + "</b> </font></td>");
                //sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");
                //footer
                sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");
                sWrite.WriteLine("<tr><td align='left' ><hr/></td></tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='center' height='22'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + strfooter1 + "</font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='center' height='22'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + strfooter2 + "</font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='center' height='22'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + strfooter3 + "</font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<script>window.print();</script>");
                sWrite.WriteLine("</body >");
                sWrite.WriteLine("</html>");
                sWrite.Close();
                System.Diagnostics.Process.Start(Apppath + "\\invoice_print.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //public void printhtml()
        //{
        //    try
        //    {
        //        string clinicn = "";
        //        string Clinic = "";
        //        string streetaddress = "";
        //        string contact_no = "";
        //        string str_locality = "";
        //        string str_pincode = "";
        //        string str_email = "";
        //        string str_website = "";
        //        string str_druglicenseno = "";
        //        string str_taxno = "";
        //        System.Data.DataTable dtp = cmodel.Get_practiceDlNumber();
        //        if (dtp.Rows.Count > 0)
        //        {
        //            clinicn = dtp.Rows[0][0].ToString();
        //            Clinic = clinicn.Replace("¤", "'");
        //            streetaddress = dtp.Rows[0]["street_address"].ToString();
        //            str_locality = dtp.Rows[0]["locality"].ToString();
        //            str_pincode = dtp.Rows[0]["pincode"].ToString();
        //            contact_no = dtp.Rows[0]["contact_no"].ToString();
        //            str_email = dtp.Rows[0]["email"].ToString();
        //            str_website = dtp.Rows[0]["website"].ToString();
        //            str_druglicenseno = dtp.Rows[0]["Dl_Number"].ToString();
        //            str_taxno = dtp.Rows[0]["Dl_Number2"].ToString();
        //            logo_name = dtp.Rows[0]["path"].ToString();
        //        }
        //        string strfooter1 = "";
        //        string strfooter2 = "";
        //        string strfooter3 = "";
        //        string header1 = "";
        //        string header2 = "";
        //        string header3 = "";
        //        System.Data.DataTable print = this.cntrl.Get_invoicePrintsettings();
        //        if (print.Rows.Count > 0)
        //        {
        //            header1 = print.Rows[0]["header"].ToString();
        //            header2 = print.Rows[0]["left_text"].ToString();
        //            header3 = print.Rows[0]["right_text"].ToString();
        //            strfooter1 = print.Rows[0]["fullwidth_context"].ToString();
        //            strfooter2 = print.Rows[0]["left_sign"].ToString();
        //            strfooter3 = print.Rows[0]["right_sign"].ToString();
        //        }
        //        string doctorname = "";
        //        string strinvoice = "";
        //        string strdate = "";
        //        System.Data.DataTable dt_cf = this.cntrl.get_invoice_doctorname(patient_id);
        //        if (dt_cf.Rows.Count > 0)
        //        {
        //            doctorname = Convert.ToString(dt_cf.Rows[0]["doctor_name"].ToString());
        //        }
        //        System.Data.DataTable dt_invo = this.cntrl.Get_date(invoice_plan_id);
        //        if (dt_invo.Rows.Count > 0)
        //        {
        //            strinvoice = Convert.ToString(dt_invo.Rows[0]["invoice"].ToString());
        //            strdate = Convert.ToString(dt_invo.Rows[0]["date"].ToString());
        //        }
        //        string Apppath = System.IO.Directory.GetCurrentDirectory();
        //        StreamWriter sWrite = new StreamWriter(Apppath + "\\invoice.html");
        //        sWrite.WriteLine("<html>");
        //        sWrite.WriteLine("<head>");
        //        sWrite.WriteLine("</head>");
        //        sWrite.WriteLine("<body >");
        //        sWrite.WriteLine("<br>");
        //        sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
        //        sWrite.WriteLine("<tr><th align='center'><br><FONT COLOR=black FACE='Geneva, Arial' SIZE=5>INVOICE </font></th></tr>");
        //        sWrite.WriteLine("</table>");
        //        if (includeheader == "1")
        //        {
        //            if (includelogo == "1")
        //            {
        //                if (logo != null || logo_name != "")
        //                {
        //                    string Appath = System.IO.Directory.GetCurrentDirectory();
        //                    if (File.Exists(Appath + "\\" + logo_name))
        //                    {
        //                        sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
        //                        sWrite.WriteLine("<tr>");
        //                        sWrite.WriteLine("<td width='100' height='75px' align='left' rowspan='4'><img src='" + Appath + "\\" + logo_name + "' width='77' height='25px' style='width:100px;height:100px;'></td>  ");
        //                        sWrite.WriteLine("<td width='588' align='left' height='25px'><FONT  COLOR=black  face='Arial' SIZE=5>&nbsp;" + header1 + "</font></td></tr>");
        //                        sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Arial' SIZE=3>&nbsp;&nbsp;" + header2 + "</font></td></tr>");
        //                        sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Arial' SIZE=2>&nbsp;&nbsp;" + header3 + "</font></td></tr>");
        //                        sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Arial' SIZE=2>&nbsp;&nbsp;" + str_druglicenseno + "&nbsp;&nbsp; " + str_taxno + "</font></td></tr>");
        //                        sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
        //                        sWrite.WriteLine("<tr><td align='left' height='20px' valign='top' > <FONT COLOR=black FACE='Geneva, Arial' SIZE=1> <FONT COLOR=black>Invoice No: </FONT>" + strinvoice + "</font> </td> <td align='right' ><FONT COLOR=black FACE=' Arial' SIZE=1> <FONT COLOR=black>Date : </FONT>" + DateTime.Parse(strdate).ToString("dd MMM yyyy") + "</font></td></tr>");
        //                        sWrite.WriteLine("</table>");
        //                    }
        //                    else
        //                    {
        //                        sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
        //                        sWrite.WriteLine("<tr>");
        //                        sWrite.WriteLine("<td  align='left' height='25px'><FONT  COLOR=black  face='Arial' SIZE=5>&nbsp;" + header1 + "</font></td></tr>");
        //                        sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Arial' SIZE=3>&nbsp;&nbsp;" + header2 + "</font></td></tr>");
        //                        sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Arial' SIZE=2>&nbsp;&nbsp;" + header3 + "</font></td></tr>");
        //                        sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Arial' SIZE=2>&nbsp;&nbsp;" + str_druglicenseno + "&nbsp;&nbsp; " + str_taxno + "</font></td></tr>");
        //                        sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
        //                        sWrite.WriteLine("<tr><td align='left' height='20px' valign='top' > <FONT COLOR=black FACE='Geneva, Arial' SIZE=1> <FONT COLOR=black>Invoice No: </FONT>" + strinvoice + "</font> </td> <td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=1> <FONT COLOR=black>Date : </FONT>" + DateTime.Parse(strdate).ToString("dd MMM yyyy") + "</font></td></tr>");
        //                        sWrite.WriteLine("</table>");
        //                    }
        //                }
        //                else
        //                {
        //                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
        //                    sWrite.WriteLine("<tr>");
        //                    sWrite.WriteLine("<td  align='left' height='25px'><FONT  COLOR=black  face='Arial' SIZE=5>&nbsp;" + header1 + "</font></td></tr>");
        //                    sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Arial' SIZE=3>&nbsp;&nbsp;" + header2 + "</font></td></tr>");
        //                    sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Arial' SIZE=2>&nbsp;&nbsp;" + header3 + "</font></td></tr>");
        //                    sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Arial' SIZE=2>&nbsp;&nbsp;" + str_druglicenseno + "&nbsp;&nbsp; " + str_taxno + "</font></td></tr>");
        //                    sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
        //                    sWrite.WriteLine("<tr><td align='left' height='20px' valign='top' > <FONT COLOR=black FACE='Geneva, Arial' SIZE=1> <FONT COLOR=black>Invoice No: </FONT>" + strinvoice + "</font> </td> <td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=1> <FONT COLOR=black>Date : </FONT>" + DateTime.Parse(strdate).ToString("dd MMM yyyy") + "</font></td></tr>");
        //                    sWrite.WriteLine("</table>");
        //                }
        //            }
        //            else
        //            {
        //                sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
        //                sWrite.WriteLine("<tr>");
        //                sWrite.WriteLine("<td  align='left' height='25px'><FONT  COLOR=black  face='Arial' SIZE=5>&nbsp;" + header1 + "</font></td></tr>");
        //                sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Arial' SIZE=3>&nbsp;&nbsp;" + header2 + "</font></td></tr>");
        //                sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Arial' SIZE=2>&nbsp;&nbsp;" + header3 + "</font></td></tr>");
        //                sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Arial' SIZE=2>&nbsp;&nbsp;" + str_druglicenseno + "&nbsp;&nbsp; " + str_taxno + "</font></td></tr>");
        //                sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
        //                sWrite.WriteLine("<tr><td align='left' height='20px' valign='top' > <FONT COLOR=black FACE='Geneva, Arial' SIZE=1> <FONT COLOR=black>Invoice No: </FONT>" + strinvoice + "</font> </td> <td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=1> <FONT COLOR=black>Date : </FONT>" + DateTime.Parse(strdate).ToString("dd MMM yyyy") + "</font></td></tr>");
        //                sWrite.WriteLine("</table>");
        //            }
        //        }
        //        else
        //        {
        //            sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
        //            sWrite.WriteLine("<tr>");

        //            sWrite.WriteLine("<td align='left' height='15px' > <FONT COLOR=black FACE='Geneva, Arial' SIZE=2> <FONT COLOR=black>Invoice No: </FONT>" + strinvoice + "</font> </td> <td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2> <FONT COLOR=black>Date : </FONT>" + DateTime.Parse(strdate).ToString("dd MMM yyyy") + "</font></td></tr>");
        //            sWrite.WriteLine("</table>");
        //        }
        //        int Dexist = 0;
        //        string sexage = "";
        //        string address = "";
        //        address = "";
        //        System.Data.DataTable dt1 = cmodel.Get_Patient_Details(patient_id);
        //        if (dt1.Rows.Count > 0)
        //        {
        //            sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
        //            sWrite.WriteLine("<tr>");
        //            if (dt1.Rows[0]["gender"].ToString() != "")
        //            {
        //                sexage = dt1.Rows[0]["gender"].ToString();
        //                Dexist = 1;
        //            }
        //            if (dt1.Rows[0]["age"].ToString() != "")
        //            {
        //                if (Dexist == 1)
        //                {
        //                    sexage = sexage + ", " + dt1.Rows[0]["age"].ToString() + " Years";
        //                }
        //                else
        //                {
        //                    sexage = dt1.Rows[0]["age"].ToString() + " Years";
        //                }
        //            }
        //            sWrite.WriteLine(" <td align='left' height=20><FONT COLOR=black FACE='Arial' SIZE=2><b>" + dt1.Rows[0]["pt_name"].ToString() + "</b><i> (" + sexage + ")</i></font></td>");
        //            sWrite.WriteLine(" </tr>");
        //            sWrite.WriteLine("<tr>");
        //            sWrite.WriteLine("<td align='left' height=20><FONT COLOR=black FACE='Arial' SIZE=2>Patient Id:" + dt1.Rows[0]["pt_id"].ToString() + " </font></td>");
        //            sWrite.WriteLine(" </tr>");
        //            Dexist = 0;
        //            if (dt1.Rows[0]["street_address"].ToString() != "")
        //            {
        //                address = dt1.Rows[0]["street_address"].ToString();
        //                Dexist = 1;
        //            }
        //            if (dt1.Rows[0]["locality"].ToString() != "")
        //            {
        //                if (Dexist == 1)
        //                {
        //                    address = address + ",";
        //                }
        //                address = address + dt1.Rows[0]["locality"].ToString();
        //                Dexist = 1;
        //            }
        //            if (dt1.Rows[0]["city"].ToString() != "")
        //            {
        //                if (Dexist == 1)
        //                {
        //                    address = address + ",";
        //                }
        //                address = address + dt1.Rows[0]["city"].ToString();
        //                Dexist = 1;
        //            }
        //            if (dt1.Rows[0]["pincode"].ToString() != "")
        //            {
        //                if (Dexist == 1)
        //                {
        //                    address = address + ",";
        //                }
        //                address = address + dt1.Rows[0]["pincode"].ToString();
        //                Dexist = 1;
        //            }
        //            if (address != "")
        //            {
        //                sWrite.WriteLine("<tr>");
        //                sWrite.WriteLine("<td align='left' height=20><FONT COLOR=black FACE=' Arial' SIZE=2>" + address + " </font></td>");
        //                sWrite.WriteLine(" </tr>");
        //            }
        //            sWrite.WriteLine("<tr>");
        //            sWrite.WriteLine("<td align='left' height=20><FONT COLOR=black FACE=' Arial' SIZE=2>" + dt1.Rows[0]["primary_mobile_number"].ToString() + " </font></td>");
        //            sWrite.WriteLine(" </tr>");
        //            if (dt1.Rows[0]["email_address"].ToString() != "")
        //            {
        //                sWrite.WriteLine("<tr>");
        //                sWrite.WriteLine("<td align='left' height=20><FONT COLOR=black FACE=' Arial' SIZE=2>" + dt1.Rows[0]["email_address"].ToString() + " </font></td>");
        //                sWrite.WriteLine(" </tr>");
        //            }
        //            sWrite.WriteLine("<tr><td colspan=2><hr></td></tr>");
        //            sWrite.WriteLine("</table>");
        //        }
        //        // Discount and Tax Check
        //        string discount_check = "";
        //        string tax_check = "";
        //        System.Data.DataTable dt_invoice_check = this.cntrl.get_invoiceDetails(invoice_plan_id);
        //        if (dt_invoice_check.Rows.Count > 0)
        //        {
        //            k = 0;
        //            while (k < dt_invoice_check.Rows.Count)
        //            {
        //                if (decimal.Parse(dt_invoice_check.Rows[k]["discountin_rs"].ToString()) > 0 && discount_check == "")
        //                {
        //                    discount_check = "Discount";
        //                }
        //                if (decimal.Parse(dt_invoice_check.Rows[k]["total_tax"].ToString()) > 0 && tax_check == "")
        //                {
        //                    tax_check = "Tax";
        //                }
        //                k = k + 1;
        //            }
        //        }
        //        // Close
        //        sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");
        //        sWrite.WriteLine("<tr style='background:#999999'>");
        //        sWrite.WriteLine("<td align='left' width='35px' height='30' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Arial' SIZE=3>&nbsp;Sl.</font></td>");
        //        sWrite.WriteLine("<td align='left' width='316px' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Arial' SIZE=3>&nbsp;Services</font></td>");
        //        sWrite.WriteLine("<td align='right' width='93px' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Arial' SIZE=3>&nbsp;Cost </font></td>");
        //        sWrite.WriteLine("<td align='right' width='78px' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Arial' SIZE=3>&nbsp;" + discount_check + "&nbsp;</font></td>");
        //        sWrite.WriteLine("<td align='right' width='52px' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Arial' SIZE=3>&nbsp;" + tax_check + "&nbsp;</font></td>");
        //        sWrite.WriteLine("<td align='right' width='91px' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Arial' SIZE=3>&nbsp;Total&nbsp;</font></td>");
        //        sWrite.WriteLine("</tr>");
        //        decimal cost = 0, tax = 0, discount = 0, total = 0, totalPaid = 0;
        //        System.Data.DataTable dt_prescription = this.cntrl.get_invoiceDetails(invoice_plan_id);
        //        Decimal ValBalance1 = 0;
        //        if (dt_prescription.Rows.Count > 0)
        //        {
        //            k = 0;
        //            while (k < dt_prescription.Rows.Count)
        //            {
        //                Decimal totalcost = Convert.ToDecimal(dt_prescription.Rows[k]["cost"].ToString()) * Convert.ToDecimal(dt_prescription.Rows[k]["unit"].ToString());
        //                ValBalance1 = ValBalance1 + totalcost;
        //                Decimal subtotalcost =( Convert.ToDecimal(totalcost.ToString()) - Convert.ToDecimal(dt_prescription.Rows[k]["discountin_rs"].ToString())) + Convert.ToDecimal(dt_prescription.Rows[k]["total_tax"].ToString());
        //                Decimal qty = Convert.ToDecimal(dt_prescription.Rows[k]["unit"].ToString());
        //                string str_qty = "";
        //                if (qty > 1)
        //                {
        //                    str_qty = " [Qty:" + Convert.ToDecimal(dt_prescription.Rows[k]["unit"].ToString()) + "]";
        //                }
        //                sWrite.WriteLine("<tr>");
        //                sWrite.WriteLine("<td align='left' height='30'  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + Convert.ToString(k + 1) + " </font></td>");
        //                sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + dt_prescription.Rows[k]["services"].ToString() + str_qty + " </font></td>");
        //                sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(totalcost.ToString())) + " </font></td>");
        //                if (discount_check == "Discount")
        //                {
        //                    sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(dt_prescription.Rows[k]["discountin_rs"].ToString())) + " </font></td>");
        //                }
        //                else
        //                {
        //                    sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp; </font></td>");
        //                }
        //                if (tax_check == "Tax")
        //                {
        //                    sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(dt_prescription.Rows[k]["total_tax"].ToString())) + " </font></td>");
        //                }
        //                else
        //                {
        //                    sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp; </font></td>");
        //                }

        //                sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(subtotalcost.ToString())) + " </font></td>");
        //                sWrite.WriteLine("</tr>");
        //                if (dt_prescription.Rows[k]["notes"].ToString() != "")
        //                {
        //                    sWrite.WriteLine("<tr>");
        //                    sWrite.WriteLine("<td align='left' height='10'  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;</font></td>");
        //                    sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=1><i>&nbsp;(" + dt_prescription.Rows[k]["notes"].ToString() + ")</i> </font></td>");
        //                    sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2> </font></td>");
        //                    sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2></font></td>");
        //                    sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2></font></td>");
        //                    sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2></font></td>");
        //                    sWrite.WriteLine("</tr>");
        //                    sWrite.WriteLine("</table>");
        //                }
        //                cost = cost + decimal.Parse(totalcost.ToString());
        //                tax = tax + decimal.Parse(dt_prescription.Rows[k]["total_tax"].ToString());
        //                discount = discount + decimal.Parse(dt_prescription.Rows[k]["discountin_rs"].ToString());
        //                total = total + decimal.Parse(subtotalcost.ToString());
        //                k++;
        //            }
        //        }
        //        string num;
        //        num = this.cntrl.Get_TotalSum(invoice_plan_id);
        //        decimal numValBalance = Convert.ToDecimal(num.ToString());
        //        totalPaid = ValBalance1 - (numValBalance + discount);

        //        sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");

        //        sWrite.WriteLine("<tr><td align='left' colspan=6><hr/></td></tr>");
        //        sWrite.WriteLine("<tr >");
        //        sWrite.WriteLine("<td align='Right' width='500px'  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp; </font></td>");
        //        sWrite.WriteLine("<td align='Right' width='100px' style='text-align:left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + "Total Cost :" + " </font></td>");
        //        sWrite.WriteLine("<td align='Right' width='100px'  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(cost.ToString())) + " </font></td>");
        //        sWrite.WriteLine("</tr>");
        //        if (decimal.Parse(tax.ToString()) > 0)
        //        {
        //            sWrite.WriteLine("<tr >");
        //            sWrite.WriteLine("<td align='Right' width='500px'  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp; </font></td>");

        //            sWrite.WriteLine("<td align='Right' width='100px' style='text-align:left'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + "Total Tax :" + " </font></td>");
        //            sWrite.WriteLine("<td align='Right'   width='100px'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2", Convert.ToDecimal(tax.ToString())) + " </font></td>");
        //            sWrite.WriteLine("</tr>");
        //        }
        //        if (decimal.Parse(discount.ToString()) > 0)
        //        {
        //            sWrite.WriteLine("<tr >");
        //            sWrite.WriteLine("<td align='Right' width='500px'  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp; </font></td>");

        //            sWrite.WriteLine("<td align='Right' width='100px' style='text-align:left'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + "Total Discount :" + " </font></td>");
        //            sWrite.WriteLine("<td align='Right'   width='100px'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(discount.ToString())) + " </font></td>");
        //            sWrite.WriteLine("</tr>");
        //        }
        //        sWrite.WriteLine("<tr >");
        //        sWrite.WriteLine("<td align='Right' width='500px'  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp; </font></td>");

        //        sWrite.WriteLine("<td align='Right' width='100px' style='text-align:left'><FONT COLOR=black FACE='Geneva, Arial' SIZE=3>&nbsp;" + "Grand Total :" + " </font></td>");
        //        sWrite.WriteLine("<td align='Right'   width='100px'><FONT COLOR=black FACE='Geneva, Arial' SIZE=3>&nbsp;<b>" + String.Format("{0:C2}", decimal.Parse(total.ToString())) + " </b></font></td>");
        //        sWrite.WriteLine("</tr>");
        //        //sWrite.WriteLine("<tr >");
        //        //sWrite.WriteLine("<td align='Right' width='500px'  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp; </font></td>");

        //        ////sWrite.WriteLine("<td align='Right' width='100px' style='text-align:left'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + "Total Paid :" + " </font></td>");
        //        ////sWrite.WriteLine("<td align='Right'   width='100px'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C3}", decimal.Parse(totalPaid.ToString())) + " </font></td>");
        //        ////sWrite.WriteLine("</tr>");
        //        //sWrite.WriteLine("<tr >");
        //        //sWrite.WriteLine("<td align='Right' width='500px'  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp; </font></td>");

        //        ////sWrite.WriteLine("<td align='Right' width='100px' style='text-align:left'><FONT COLOR=black FACE='Geneva, Arial' SIZE=3>&nbsp;" + "Balance :" + " </font></td>");
        //        ////sWrite.WriteLine("<td align='Right'   width='100px'><FONT COLOR=black FACE='Geneva, Arial' SIZE=3>&nbsp;<b>" + String.Format("{0:C3}", decimal.Parse(numValBalance.ToString())) + "</b> </font></td>");
        //        //sWrite.WriteLine("</tr>");
        //        sWrite.WriteLine("</table>");
        //        //footer
        //        sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");
        //        sWrite.WriteLine("<tr><td align='left' ><hr/></td></tr>");
        //        sWrite.WriteLine("<tr>");
        //        sWrite.WriteLine("<td align='center' height='22'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + strfooter1 + "</font></td>");
        //        sWrite.WriteLine("</tr>");
        //        sWrite.WriteLine("<tr>");
        //        sWrite.WriteLine("<td align='center' height='22'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + strfooter2 + "</font></td>");
        //        sWrite.WriteLine("</tr>");
        //        sWrite.WriteLine("<tr>");
        //        sWrite.WriteLine("<td align='center' height='22'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + strfooter3 + "</font></td>");
        //        sWrite.WriteLine("</tr>");
        //        sWrite.WriteLine("</table>");
        //        sWrite.WriteLine("<script>window.print();</script>");
        //        sWrite.WriteLine("</body >");
        //        sWrite.WriteLine("</html>");
        //        sWrite.Close();
        //        System.Diagnostics.Process.Start(Apppath + "\\invoice.html");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

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
        private void labelprofile_Click(object sender, EventArgs e)
        {
            var form2 = new PappyjoeMVC.View.Patient_Profile_Details();
            form2.doctor_id = doctor_id;
            form2.patient_id = patient_id;
            openform(form2);
        }

        private void btn_paySelectedInvoice_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string privid;
                privid = this.cntrl.check_addprivillege(doctor_id);
                if (int.Parse(privid) > 0)
                {
                    var form2 = new Add_Receipt();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    form2.status = 1;
                    int i = 0; int j = 0;
                    while (i < dgv_invoice.Rows.Count)
                    {
                        try
                        {
                            if (dgv_invoice.Rows[i].Cells[8].Value.ToString() == "2")
                            {
                                form2.invoices[j] = dgv_invoice.Rows[i + 1].Cells[2].Value.ToString();
                                j++;
                            }
                        }
                        catch
                        {
                        }
                        i++;
                    }
                    if (form2.invoices == null)
                    {
                        MessageBox.Show("Choose an INVOICE first...", "Invalid Selection", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        openform(form2);
                    }
                }
                else
                {
                    MessageBox.Show("There is No Privilege to Add Invoice", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                var form2 = new Add_Receipt();
                form2.doctor_id = doctor_id;
                form2.patient_id = patient_id;
                form2.status = 1;
                int i = 0; int j = 0;
                while (i < dgv_invoice.Rows.Count)
                {
                    try
                    {
                        if (dgv_invoice.Rows[i].Cells[8].Value.ToString() == "2")
                        {
                            form2.invoices[j] = dgv_invoice.Rows[i + 1].Cells[2].Value.ToString();
                            j++;
                        }
                    }
                    catch
                    {
                    }
                    i++;
                }
                if (form2.invoices == null)
                {
                    MessageBox.Show("Choose an INVOICE first...", "Invalid Selection", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }
                else
                {
                    openform(form2);
                }
            }
            //var form2 = new Add_Receipt();
            //form2.doctor_id = doctor_id;
            //form2.patient_id = patient_id;
            //form2.status = 1;
            //int i = 0; int j = 0;
            //while (i < dgv_invoice.Rows.Count)
            //{
            //    try
            //    {
            //        if (dgv_invoice.Rows[i].Cells[8].Value.ToString() == "2")
            //        {
            //            form2.invoices[j] = dgv_invoice.Rows[i + 1].Cells[2].Value.ToString();
            //            j++;
            //        }
            //    }
            //    catch
            //    {
            //    }
            //    i++;
            //}
            //if (form2.invoices == null)
            //{
            //    MessageBox.Show("Choose an INVOICE first...", "Invalid Selection", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            //}
            //else
            //{
            //    openform(form2);
            //}
        }

        private void btn_ADD_Click(object sender, EventArgs e)
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
        GlobalVariables gv = new GlobalVariables();
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
                string id = privi_mdl.Show_attachment(doctor_id);
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

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id = privi_mdl.invoice_edit(doctor_id);
                if (int.Parse(id) > 0)
                {
                    if (invoice_plan_id != "0")
                    {
                        var form2 = new Add__invoice(invoice_plan_id);
                        form2.doctor_id = doctor_id;
                        form2.patient_id = patient_id;
                        openform(form2);
                    }
                }
                else
                {
                    MessageBox.Show("There is No Privilege to Edit Invoice", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                if (invoice_plan_id != "0")
                {
                    var form2 = new Add__invoice(invoice_plan_id);
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
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
                        DataTable dt = this.Apptmt_cntrl.show(patient_id);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            apntid = dt.Rows[i]["a_id"].ToString();
                        }
                        var form2 = new Add_Appointment();
                        form2.patient_id = patient_id;
                        form2.doctor_id = doctor_id;
                        form2.appointment_id = apntid;
                        openform(form2);
                    }
                    else
                    {
                        MessageBox.Show("There is No Privilege to Add Appointments", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    }
                }
                else
                {
                    DataTable dt = this.Apptmt_cntrl.show(patient_id);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        apntid = dt.Rows[i]["a_id"].ToString();
                    }
                    var form2 = new Add_Appointment();
                    form2.patient_id = patient_id;
                    form2.doctor_id = doctor_id;
                    form2.appointment_id = apntid;
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
                    if (gv.project_type == "Dental")
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
                if (gv.project_type == "Dental")
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

        private void whatsappToolStripMenuItem_Click(object sender, EventArgs e)
        {try
            {
                DataTable dtb_patient = this.cntrl.Get_patient_id_name_gender(patient_id);
                if (dtb_patient.Rows.Count > 0)
                {
                    string mob = "", numb = "";
                    mob = dtb_patient.Rows[0]["primary_mobile_number"].ToString();
                    numb = "+91" + mob;
                    numb = numb.Replace(" ", "");

                    //strPriscription = strPriscription + " [" + shortname + "]" + dt_prescription.Rows[p]["drug_name"].ToString() + "" + dt_prescription.Rows[p]["strength"].ToString() + " " + dt_prescription.Rows[p]["strength_gr"].ToString() + ", Duration: " + morning + "-" + noon + "-" + night + " for " + dt_prescription.Rows[p]["duration_unit"].ToString() + " " + dt_prescription.Rows[p]["duration_period"].ToString() + "-" + dt_prescription.Rows[p]["add_instruction"].ToString() + "\n\n";
                    System.Diagnostics.Process.Start("http://api.whatsapp.com/send?phone=" + numb + "&text=" + "its from pappyjoe");
                    MessageBox.Show("sucdess");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
        Communication_Setting_controller com_cntrl = new Communication_Setting_controller();
        Booking_controller b_cntrl = new Booking_controller();
        private void sMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string scount = "";
                DataTable smscount = this.com_cntrl.getsmscnt();
                if (smscount.Rows[0]["sms"].ToString() != "")
                {
                    scount = PappyjoeMVC.Model.EncryptionDecryption.Decrypt(smscount.Rows[0]["sms"].ToString(), "ch3lSeAW0n2o2!C1");
                    if (Convert.ToInt32(scount) > 5)
                    {
                        int p = 0;
                        string clinic = "", cname = "";
                        string contact_no = "";
                        string text = "";
                        string smsName = "", smsPass = "";
                        string strPriscription = "";
                        DataTable sms = this.b_cntrl.smsdetails();
                        if (sms.Rows.Count > 0)
                        {
                            smsName = sms.Rows[0]["smsName"].ToString();
                            smsPass = sms.Rows[0]["smsPass"].ToString();
                        }
                        System.Data.DataTable clinicname = this.cntrl.Get_companynameNo();
                        if (clinicname.Rows.Count > 0)
                        {
                            clinic = clinicname.Rows[0][0].ToString();
                            //clinic = cname.Replace("¤", "'");
                            contact_no = clinicname.Rows[0][1].ToString();
                        }
                        SMS_model a = new SMS_model();
                        System.Data.DataTable pat = this.cntrl.Get_patient_id_name_gender(patient_id);
                        System.Data.DataTable smsreminder = this.cntrl.remindersms();

                        System.Data.DataTable dt_pt_sub = this.cntrl.get_invoiceDetails(invoice_plan_id);
                       if(pat.Rows.Count>0)
                        {
                            if (dt_pt_sub.Rows.Count > 0)
                            {
                                System.Data.DataTable dt_invoice_main = this.cntrl.Load_invoice_mainDetails(patient_id);
                                Double totalEst = 0;
                                Decimal total_cost = 0;
                                Decimal total_discount = 0;
                                Decimal total_tax = 0;
                                decimal due = 0;
                                string discount_string = "";
                                while (p < dt_invoice_main.Rows.Count)
                                {

                                    Decimal totalcost = Convert.ToDecimal(dt_pt_sub.Rows[k]["cost"].ToString()) * Convert.ToDecimal(dt_pt_sub.Rows[k]["unit"].ToString());
                                    total_cost = total_cost + Convert.ToDecimal(totalcost);
                                    total_discount = total_discount + Convert.ToDecimal(dt_pt_sub.Rows[k]["discountin_rs"].ToString());
                                    total_tax = total_tax + Convert.ToDecimal(dt_pt_sub.Rows[k]["tax_inrs"].ToString());
                                    totalEst = totalEst + Convert.ToDouble(dt_pt_sub.Rows[k]["total"].ToString());
                                    due = Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString());
                                    strPriscription = strPriscription + " [" + dt_pt_sub.Rows[k]["services"].ToString() + "]" + "(" + dt_pt_sub.Rows[k]["unit"].ToString() + ")" + ", Cost: " + String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["cost"].ToString())) + ", Discount: " + String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discountin_rs"].ToString())) + ", Tax: " + String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["tax_inrs"].ToString())) + ", Total: " + String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString())) + "\n\n";
                                    p++;
                                }
                                Double gtotal = 0, paid = 0;
                                gtotal = Convert.ToDouble(total_cost) - (Convert.ToDouble(total_discount) + Convert.ToDouble(total_tax));
                                paid = gtotal - totalEst;
                                strPriscription = strPriscription + "Grand Total:" + gtotal + "Paid:" + "\n\n";
                                string type = "LNG";
                                text = "Dear " + pat.Rows[0]["pt_name"].ToString() + "," + "\n" + "Invoice:" + "\n" + "\n" + "Treatment:" + strPriscription + "Regards With " + clinic + "," + contact_no + "CLIINI";
                                string number = "91" + pat.Rows[0]["primary_mobile_number"].ToString();
                                a.SendSMS(smsName, smsPass, number, text, type);
                                //this.cntrl.savecommunication(patient_id, text);
                                MessageBox.Show("The Prescription Details Containing  Message Sent Successfully to " + pat.Rows[0]["pt_name"].ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    
                        scount = Convert.ToString(Convert.ToInt32(scount) - 1);
                        string Encrypt = PappyjoeMVC.Model.EncryptionDecryption.Encrypt(scount, "ch3lSeAW0n2o2!C1");
                        //this.ccntrl.smsCount(Encrypt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                string clinicn = "";
                string Clinic = "";
                string streetaddress = "";
                string contact_no = "";
                string str_locality = "";
                string str_pincode = "";
                string str_email = "";
                string str_website = "";
                string str_druglicenseno = "";
                string str_taxno = "";
                System.Data.DataTable dtp = cmodel.Get_practiceDlNumber();
                if (dtp.Rows.Count > 0)
                {
                    clinicn = dtp.Rows[0][0].ToString();
                    Clinic = clinicn.Replace("¤", "'");
                    streetaddress = dtp.Rows[0]["street_address"].ToString();
                    str_locality = dtp.Rows[0]["locality"].ToString();
                    str_pincode = dtp.Rows[0]["pincode"].ToString();
                    contact_no = dtp.Rows[0]["contact_no"].ToString();
                    str_email = dtp.Rows[0]["email"].ToString();
                    str_website = dtp.Rows[0]["website"].ToString();
                    str_druglicenseno = dtp.Rows[0]["Dl_Number"].ToString();
                    str_taxno = dtp.Rows[0]["Dl_Number2"].ToString();
                    logo_name = dtp.Rows[0]["path"].ToString();
                }
                string strfooter1 = "";
                string strfooter2 = "";
                string strfooter3 = "";
                string header1 = "";
                string header2 = "";
                string header3 = "";
                System.Data.DataTable print = this.cntrl.Get_invoicePrintsettings();
                if (print.Rows.Count > 0)
                {
                    header1 = print.Rows[0]["header"].ToString();
                    header2 = print.Rows[0]["left_text"].ToString();
                    header3 = print.Rows[0]["right_text"].ToString();
                    strfooter1 = print.Rows[0]["fullwidth_context"].ToString();
                    strfooter2 = print.Rows[0]["left_sign"].ToString();
                    strfooter3 = print.Rows[0]["right_sign"].ToString();
                }
                string PatName = "", patiID = "", strage = "", mobile = "", strgener="", strinvoice="", strdate="";
                DataTable dt_patent = new DataTable();
                dt_patent = cmodel.Get_Patient_Details(patient_id);//aadhar_id
                if (dt_patent.Rows.Count > 0)
                {

                    patiID = dt_patent.Rows[0]["pt_id"].ToString();
                    if (dt_patent.Rows[0]["age"].ToString() != "")
                    {
                        strage = dt_patent.Rows[0]["age"].ToString();
                    }
                    if (dt_patent.Rows[0]["gender"].ToString() != "")
                    {
                        strgener = dt_patent.Rows[0]["gender"].ToString();
                        //}
                    }
                    PatName = dt_patent.Rows[0]["pt_name"].ToString();
                    mobile = dt_patent.Rows[0]["primary_mobile_number"].ToString();
                }
        
                float x = 31;
                float y = 250;
                float width = 270.0F; // max width I found through trial and error  270.0F
                float height = 0F;
                SolidBrush drawBrush = new SolidBrush(Color.Black);

                // Set format of string.
                StringFormat drawFormatCenter = new StringFormat();
                drawFormatCenter.Alignment = StringAlignment.Center;
                StringFormat drawFormatLeft = new StringFormat();
                drawFormatLeft.Alignment = StringAlignment.Near;
                StringFormat drawFormatRight = new StringFormat();
                drawFormatRight.Alignment = StringAlignment.Far;

    System.Data.DataTable dt_invo = this.cntrl.Get_date(invoice_plan_id);
                if (dt_invo.Rows.Count > 0)
                {
                    strinvoice = Convert.ToString(dt_invo.Rows[0]["invoice"].ToString());
                    strdate = Convert.ToString(dt_invo.Rows[0]["date"].ToString());
                }
                var dateTimeNow = DateTime.Now;
                var tdate = dateTimeNow.ToShortDateString();
                if (dt_invo.Rows.Count > 0)
                {
                    if (dt_invo.Rows[0]["date"].ToString() != null)
                    { tdate = Convert.ToDateTime(dt_invo.Rows[0]["date"].ToString()).ToString("dd-MM-yyyy"); }
                    else
                        tdate = null;
                }
                ///}
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                if (File.Exists(Apppath + "\\" + logo_name))
                {
                    Image newImage = Image.FromFile(Apppath + "\\" + logo_name);
                    // Create coordinates for upper-left corner of image.
                    float x1 = 75.0F;//100.0F;
                    float y1 = 75.0F;

                    // Create rectangle for source image.
                    RectangleF srcRect = new RectangleF(50.0F, 50.0F, 150, 150);//(50.0F, 50.0F, 150.0F, 150.0F);
                    GraphicsUnit units = GraphicsUnit.Pixel;

                    // Draw image to screen.
                    e.Graphics.DrawImage(newImage, x1, y1, srcRect, units);
                }

                string text = Clinic;
                e.Graphics.DrawString(text, new Font("Arial", 12, FontStyle.Bold), drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, new Font("Arial", 12, FontStyle.Bold)).Height;

                text = streetaddress;
                e.Graphics.DrawString(text, new Font("Arial", 10, FontStyle.Regular), drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, new Font("Arial", 10, FontStyle.Regular)).Height;

                text = str_locality;
                e.Graphics.DrawString(text, new Font("Arial", 10, FontStyle.Regular), drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, new Font("Arial", 10, FontStyle.Regular)).Height;
                text = str_pincode;
                e.Graphics.DrawString(text, new Font("Arial", 10, FontStyle.Regular), drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, new Font("Arial", 10, FontStyle.Regular)).Height;

                y = y + 17;
                text = "BILL";
                e.Graphics.DrawString(text, new Font("Arial", 10, FontStyle.Underline), drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, new Font("Arial", 10, FontStyle.Underline)).Height;

                //y = y + 18;
                //text = "Date:" + " " + tdate;// +"   "+ "AdharID:"+" " + aadhar_id;
                e.Graphics.DrawString("Inv No: "+ strinvoice, new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(31, 373), drawFormatLeft);
                //y += e.Graphics.MeasureString(text, drawFontArial8Regular).Height;
                e.Graphics.DrawString("Date: " + tdate, new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(159, 373));

                text = "Name:" + "" + PatName;
                e.Graphics.DrawString(text, new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(31, 391), drawFormatLeft);

                //y += e.Graphics.MeasureString(text, drawFontArial8Regular).Height;
                //y = y + 5;
                //text = "Age:" + strage;
                //e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                //y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;
                text = "Mobile: " + mobile;// + "  " + "Sex:"+" " + strgener + " " + "Age:"+" " + strage;
                e.Graphics.DrawString(text, new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(31, 412), drawFormatLeft);
                //y += e.Graphics.MeasureString(text, drawFontArial8Regular).Height;
                e.Graphics.DrawString("Sex: " + strgener, new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(153, 412), drawFormatLeft);
                e.Graphics.DrawString("Age:" + " " + strage, new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(234, 412), drawFormatLeft);
                e.Graphics.DrawString("ID:" + " " + patiID, new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(31, 428), drawFormatLeft);

                System.Data.DataTable dt_payment = this.cntrl.get_invoiceDetails(invoice_plan_id);
                //System.Data.DataTable dt_payment = db.table(strsql);
                //decimal total = 0;
                //y = y + 20;

                text = "***************************************************";
                e.Graphics.DrawString(text, new Font("Arial", 10, FontStyle.Regular), drawBrush, new Point(31, 450), drawFormatLeft);
                //y += e.Graphics.MeasureString(text, new Font("Arial", 10, FontStyle.Regular)).Height;
                text = "Item";//       AMOUNT      INV NO";
                e.Graphics.DrawString(text, new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(31, 458), drawFormatLeft);
                e.Graphics.DrawString("Qty", new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(140, 458));
                e.Graphics.DrawString("Cost", new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(180, 458));
                e.Graphics.DrawString("Tax", new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(225, 458));
                e.Graphics.DrawString("Total", new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(253, 458));

                //y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;
                text = "***************************************************";
                e.Graphics.DrawString(text, new Font("Arial", 10, FontStyle.Regular), drawBrush, new Point(31, 470), drawFormatLeft);
                ////y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;
                //y = y + 10;
                int yy = 490; string mode = "";
                System.Data.DataTable dt_prescription = this.cntrl.get_invoiceDetails(invoice_plan_id);
                decimal ValBalance1 = 0; decimal cost = 0, tax = 0, discount = 0, total = 0, totalPaid = 0;
                for (int k = 0; k< dt_payment.Rows.Count; k++)
                {
                    Decimal totalcost = Convert.ToDecimal(dt_prescription.Rows[k]["cost"].ToString()) * Convert.ToDecimal(dt_prescription.Rows[k]["unit"].ToString());
                    ValBalance1 = ValBalance1 + totalcost;
                    Decimal subtotalcost = (Convert.ToDecimal(totalcost.ToString()) - (Convert.ToDecimal(dt_prescription.Rows[k]["discountin_rs"].ToString())) + Convert.ToDecimal(dt_prescription.Rows[k]["total_tax"].ToString()));
                    Decimal qty = Convert.ToDecimal(dt_prescription.Rows[k]["unit"].ToString());
                    string str_qty = "";
                    if (qty > 1)
                    {
                        str_qty =dt_prescription.Rows[k]["unit"].ToString();
                    }
                    e.Graphics.DrawString(dt_prescription.Rows[k]["services"].ToString(), new Font("Arial", 7, FontStyle.Regular), drawBrush, new Point(31, yy));

                    e.Graphics.DrawString(dt_prescription.Rows[k]["unit"].ToString(), new Font("Arial", 7, FontStyle.Regular), drawBrush, new Point(140, yy));

                    e.Graphics.DrawString(String.Format("{0:C2}", decimal.Parse(totalcost.ToString())), new Font("Arial", 7, FontStyle.Regular), drawBrush, new Point(180, yy));
                    //if (tax_check == "Tax")
                    //{
                        e.Graphics.DrawString(dt_prescription.Rows[k]["total_tax"].ToString(), new Font("Arial", 7, FontStyle.Regular), drawBrush, new Point(225, yy));
                    //}
                    //else
                    //{
                    //    e.Graphics.DrawString("", new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(31, yy));
                    //}

                    e.Graphics.DrawString(String.Format("{0:C2}", decimal.Parse(subtotalcost.ToString())), new Font("Arial", 7, FontStyle.Regular), drawBrush, new Point(253, yy));

                    cost = cost + decimal.Parse(totalcost.ToString());
                    tax = tax + decimal.Parse(dt_prescription.Rows[k]["total_tax"].ToString());
                    discount = discount + decimal.Parse(dt_prescription.Rows[k]["discountin_rs"].ToString());
                    total = total + decimal.Parse(subtotalcost.ToString());
                    //e.Graphics.DrawString(dt_payment.Rows[i]["receipt_no"].ToString(), new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(31, yy));
                    //e.Graphics.DrawString(String.Format("{0:C2}", decimal.Parse(dt_payment.Rows[i]["amount_paid"].ToString())), new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(135, yy));
                    //e.Graphics.DrawString(dt_payment.Rows[i]["invoice_no"].ToString(), new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(216, yy));
                    yy = yy + 20;
                    //total = total + decimal.Parse(dt_payment.Rows[i]["amount_paid"].ToString());
                    //mode = dt_payment.Rows[i]["mode_of_payment"].ToString();
                }
                yy = yy + 10;
                text = "***************************************************";
                e.Graphics.DrawString(text, new Font("Arial", 10, FontStyle.Regular), drawBrush, new Point(31, yy), drawFormatLeft);
                //e.Graphics.DrawString(text, new Font("Arial", 10, FontStyle.Regular), drawBrush, new Point(31, yy), drawFormatLeft);

                //y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;
                //y = y + 10;
                e.Graphics.DrawString("TOTAL: ", new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(129, yy + 19));
                e.Graphics.DrawString(String.Format("{0:C2}", decimal.Parse(cost.ToString())) , new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(248, yy + 19));

                yy = yy + 45;
                e.Graphics.DrawString("TOTAL DISC:", new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(129, yy), drawFormatLeft);
                e.Graphics.DrawString( discount.ToString("0.00"), new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(248, yy), drawFormatLeft);

                yy = yy + 25;
                text = "GRAND TOTAL:  " ;
                e.Graphics.DrawString(text, new Font("Arial", 10, FontStyle.Bold), drawBrush, new Point(129, yy));
                e.Graphics.DrawString(String.Format("{0:C2}", decimal.Parse(total.ToString())), new Font("Arial", 10, FontStyle.Bold), drawBrush, new Point(248, yy));

                //y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;
                //y = y + 20;
                yy = yy + 25;
                text = "THANK YOU";
                e.Graphics.DrawString(text, new Font("Arial", 8, FontStyle.Regular), drawBrush, new Point(119, yy));
                //y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;



            }
            catch (Exception ex)
            {

            }
        }
        public int rowcount = 0;
        private void label1_Click(object sender, EventArgs e)
        {
            // dgv_invoice.Rows.Count;// lb_Invoice_cnt.Text;// 
            //if(Convert.ToInt32( lb_Invoice_cnt.Text) >50)
            //{
                int count = rowcount + 5;
                DataTable dt_invoice_main = this.cntrl.Load_invoice_mainDetails_count(patient_id, count);

            if( dt_invoice_main.Rows.Count>0)
            {
                Load_MainTable_showmore(dt_invoice_main);// Load_MainTable(dt_invoice_main);
                rowcount = count;
            }
            else
            {
                label1.Visible = false;
            }
               
            //}
          
        }
    }
}
