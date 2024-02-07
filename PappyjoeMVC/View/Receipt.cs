using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using PappyjoeMVC.Controller;
using PappyjoeMVC.Model;

namespace PappyjoeMVC.View
{
    public partial class Receipt : Form
    {
        public string doctor_id = "";
        public string patient_id = "0", staff_id="";
        string logo_name = "";
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
        string printall = "NO"; string payment_date = "", receipt = "";
        System.Drawing.Image logo = null;
        Receipt_controller cntrl=new Receipt_controller();
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
        Connection db = new Connection();
        string path = "";
        public Receipt()
        {
            InitializeComponent();
        }
        public void SetController(Receipt_controller Controller )
        {
            cntrl = Controller;
        }

        private void Receipt_Load(object sender, EventArgs e)
        {
            try
            {
                //if (doctor_id != "1")
                //{
                //    string id;
                //    id = this.cntrl.check_add_privillege(doctor_id);
                //    if (int.Parse(id) > 0)
                //    {
                //        btn_Add.Enabled = false;
                //    }
                //    else
                //    {
                //        btn_Add.Enabled = true;
                //    }
                //}
                System.Data.DataTable clinicname = this.cntrl.Get_CompanyNAme();
                if (clinicname.Rows.Count > 0)
                {
                    string clinicn = "";
                    clinicn = clinicname.Rows[0][0].ToString();
                    path = clinicname.Rows[0]["path"].ToString();
                    string docnam = this.cntrl.Get_DoctorName(doctor_id);
                    try
                    {
                        if (path != "")
                        {
                            string curFile = this.cntrl.server() + "\\Pappyjoe_utilities\\Logo\\" + path;

                            if (File.Exists(curFile))
                            {
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
                    catch (Exception ex)
                    {
                    }
                }
                Dgv_payment.Location = new System.Drawing.Point(10, 10);
                Dgv_payment.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom);
                System.Data.DataTable pay = this.cntrl.get_total_payment(patient_id);
                if (pay.Rows.Count > 0)
                {
                    //Lab_Due.Visible = true;
                    if(pay.Rows[0][0].ToString()!="")
                       Lab_Due.Text =Convert.ToDecimal( pay.Rows[0]["total_payment"].ToString()).ToString("0.00") + " due";
                }
                DataTable dtadvance = this.cntrl.Get_Advance(patient_id);
                if (dtadvance.Rows.Count > 0)
                {
                    //adv_refund.Visible = true;
                    lblAdvance.Show(); Adv_details.Visible = true; //adv_refund.Visible = true;
                    lblAdvance.Text = "Available advance: " + string.Format("{0:C2}", decimal.Parse(dtadvance.Rows[0][0].ToString()));
                }
                else
                {
                    Adv_details.Visible = false; //adv_refund.Visible = false;
                    lblAdvance.Text = "Available advance: " + string.Format("{0:C2}", 0);
                }
                Dgv_payment.Rows.Clear();
                System.Data.DataTable pat = this.cntrl.Get_pat_iDName(patient_id);
                linkLabel_id.Text = pat.Rows[0]["pt_id"].ToString();
                linkLabel_Name.Text = pat.Rows[0]["pt_name"].ToString();
                Dgv_payment.ColumnCount = 8;
                Dgv_payment.RowCount = 0;
                Dgv_payment.ColumnHeadersVisible = false;
                Dgv_payment.RowHeadersVisible = false;
                Dgv_payment.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Dgv_payment.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                Dgv_payment.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                Dgv_payment.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                Dgv_payment.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                Dgv_payment.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                Dgv_payment.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                Dgv_payment.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                Dgv_payment.Columns[0].Width = 150;
                Dgv_payment.Columns[1].Width = 150;
                Dgv_payment.Columns[2].Width = 250;
                Dgv_payment.Columns[3].Width = 182;
                Dgv_payment.Columns[4].Width = 110;
                //Dgv_payment.Columns[4].Width = 120;
                Dgv_payment.Columns[5].Width = 120;
                Dgv_payment.Columns[6].Width = 80;
                Dgv_payment.Columns[6].Visible = false;
                Dgv_payment.Columns[5].Visible = true;
                int j = 0;
                System.Data.DataTable Payment = this.cntrl.get_paymentDate(patient_id);
                load_grid(Payment);
                //if(Payment.Rows.Count>25)
                //{
                //    label11.Visible = true;
                //}
                //else
                //{
                //    label11.Visible = false;
                //}
                //for (int i = 0; i < Payment.Rows.Count; i++)
                //{
                //    int l = 0;
                //    Dgv_payment.Rows.Add(String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(Payment.Rows[i]["payment_date"].ToString())), "","", "", "", "0");
                //    Dgv_payment.Rows[j].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                //    Dgv_payment.Rows[j].Cells[0].Style.ForeColor = Color.DarkGreen;
                //    Dgv_payment.Rows[j].Cells[6].Value = PappyjoeMVC.Properties.Resources.blank;
                //    j = j + 1;
                //    Dgv_payment.Rows.Add("RECEIPT NUMBER", "INVOICES", "TREATMENTS","MODE OF PAYMENT", "AMOUNT PAID", "", "");
                //    Dgv_payment.Rows[j].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                //    Dgv_payment.Rows[j].Cells[0].Style.ForeColor = Color.White;
                //    Dgv_payment.Rows[j].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                //    Dgv_payment.Rows[j].Cells[1].Style.ForeColor = Color.White;
                //    Dgv_payment.Rows[j].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                //    Dgv_payment.Rows[j].Cells[2].Style.ForeColor = Color.White;
                //    Dgv_payment.Rows[j].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                //    Dgv_payment.Rows[j].Cells[3].Style.ForeColor = Color.White;
                //    Dgv_payment.Rows[j].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                //    Dgv_payment.Rows[j].Cells[4].Style.ForeColor = Color.White;
                //    Dgv_payment.Rows[j].Cells[0].Style.BackColor = Color.DarkGray;
                //    Dgv_payment.Rows[j].Cells[1].Style.BackColor = Color.DarkGray;
                //    Dgv_payment.Rows[j].Cells[2].Style.BackColor = Color.DarkGray;
                //    Dgv_payment.Rows[j].Cells[3].Style.BackColor = Color.DarkGray;
                //    Dgv_payment.Rows[j].Cells[4].Style.BackColor = Color.DarkGray;
                //    Dgv_payment.Rows[j].Cells[6].Value = PappyjoeMVC.Properties.Resources.blank;
                //    string receipt = "";
                //    System.Data.DataTable Payments = this.cntrl.payment_details(Convert.ToDateTime (Payment.Rows[i]["payment_date"].ToString()).ToString("yyyy-MM-dd"), patient_id);
                //    for (int k = 0; k < Payments.Rows.Count; k++)
                //    {
                //        if (l >= 1)
                //        {
                //            receipt = Payments.Rows[k]["receipt_no"].ToString();
                //            if (receipt == Payments.Rows[k - 1]["receipt_no"].ToString())
                //            {
                //                Dgv_payment.Rows.Add("", Payments.Rows[k]["invoice_no"].ToString(), Payments.Rows[k]["procedure_name"].ToString(), Payments.Rows[k]["mode_of_payment"].ToString(), String.Format("{0:C2}", Convert.ToDecimal(Payments.Rows[k]["amount_paid"].ToString())), "", "");
                //                j = j + 1;
                //                l = l + 1;
                //                Dgv_payment.Rows[j].Cells[4].Style.ForeColor = Color.Red;
                //                Dgv_payment.Rows[j].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                //                Dgv_payment.Rows[j].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
                //                Dgv_payment.Rows[j].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                //                Dgv_payment.Rows[j].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                //                Dgv_payment.Rows[j].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                //                Dgv_payment.Rows[j].Cells[2].Style.ForeColor = Color.Black;
                //                Dgv_payment.Rows[j].Cells[0].Style.ForeColor = Color.Red;
                //                Dgv_payment.Rows[j].Cells[1].Style.ForeColor = Color.DodgerBlue;
                //                Dgv_payment.Rows[j].Cells[6].Value = PappyjoeMVC.Properties.Resources.blank;
                //            }
                //            else
                //            {
                //                Dgv_payment.Rows.Add("", "", "", "", "", "","");
                //                j = j + 1;
                //                Dgv_payment.Rows.Add(Payments.Rows[k]["receipt_no"].ToString(), Payments.Rows[k]["invoice_no"].ToString(), Payments.Rows[k]["procedure_name"].ToString(), Payments.Rows[k]["mode_of_payment"].ToString(), String.Format("{0:C2}", Convert.ToDecimal(Payments.Rows[k]["amount_paid"].ToString())), String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(Payment.Rows[i]["payment_date"].ToString())), "");
                //                Dgv_payment.Rows[j].Cells[6].Value = PappyjoeMVC.Properties.Resources.blank;
                //                j = j + 1;
                //                l = l + 1;
                //                Dgv_payment.Rows[j].Cells[4].Style.ForeColor = Color.Red;
                //                Dgv_payment.Rows[j].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                //                Dgv_payment.Rows[j].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
                //                Dgv_payment.Rows[j].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                //                Dgv_payment.Rows[j].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                //                Dgv_payment.Rows[j].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                //                Dgv_payment.Rows[j].Cells[2].Style.ForeColor = Color.Black;
                //                Dgv_payment.Rows[j].Cells[0].Style.ForeColor = Color.Red;
                //                Dgv_payment.Rows[j].Cells[1].Style.ForeColor = Color.DodgerBlue;
                //                Dgv_payment.Rows[j].Cells[6].Value = PappyjoeMVC.Properties.Resources.Bill;
                //            }
                //        }
                //        else
                //        {
                //            Dgv_payment.Rows.Add(Payments.Rows[k]["receipt_no"].ToString(), Payments.Rows[k]["invoice_no"].ToString(), Payments.Rows[k]["procedure_name"].ToString(), Payments.Rows[k]["mode_of_payment"].ToString(), String.Format("{0:C2}", Convert.ToDecimal(Payments.Rows[k]["amount_paid"].ToString())), String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(Payment.Rows[i]["payment_date"].ToString())), "");
                //            j = j + 1;
                //            l = l + 1;
                //            Dgv_payment.Rows[j].Cells[4].Style.ForeColor = Color.Red;
                //            Dgv_payment.Rows[j].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                //            Dgv_payment.Rows[j].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
                //            Dgv_payment.Rows[j].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                //            Dgv_payment.Rows[j].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                //            Dgv_payment.Rows[j].Cells[2].Style.ForeColor = Color.Black;
                //            Dgv_payment.Rows[j].Cells[0].Style.ForeColor = Color.Red;
                //            Dgv_payment.Rows[j].Cells[1].Style.ForeColor = Color.DodgerBlue;
                //            Dgv_payment.Rows[j].Cells[6].Value = PappyjoeMVC.Properties.Resources.Bill;
                //        }
                //    }
                //    Dgv_payment.Rows.Add("", "", "", "", "", "","");
                //    Dgv_payment.Rows[j + 1].Cells[6].Value = PappyjoeMVC.Properties.Resources.blank;
                //    j = j + 2;
                //}

                if (Dgv_payment.Rows.Count <= 0)
                {
                    int x = (panel9.Size.Width - Lab_Msg.Size.Width) / 2;
                    Lab_Msg.Location = new Point(x, Lab_Msg.Location.Y);
                    Lab_Msg.Show();
                }
                else
                {
                    Lab_Msg.Hide();
                    //Lab_Msg.Location = new System.Drawing.Point(165, 165);
                }
                Dgv_payment.Show();
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
        int j = 0;
        public void load_grid(DataTable Payment)
        {
            if(Payment.Rows.Count>0)
            {
                for (int i = 0; i < Payment.Rows.Count; i++)
                {
                    int l = 0;
                    Dgv_payment.Rows.Add(String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(Payment.Rows[i]["payment_date"].ToString())), "", "", "", "", "", "", PappyjoeMVC.Properties.Resources.blank);
                    Dgv_payment.Rows[j].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                    Dgv_payment.Rows[j].Cells[0].Style.ForeColor = Color.DarkGreen;
                    //Dgv_payment.Rows[j].Cells[7].Value = PappyjoeMVC.Properties.Resources.blank;
                    j = j + 1;
                    Dgv_payment.Rows.Add("RECEIPT NUMBER", "INVOICES", "TREATMENTS", "MODE OF PAYMENT", "AMOUNT PAID", "REFUND AMOUNT", "Date", "");
                    Dgv_payment.Rows[j].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    Dgv_payment.Rows[j].Cells[0].Style.ForeColor = Color.White;
                    Dgv_payment.Rows[j].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    Dgv_payment.Rows[j].Cells[1].Style.ForeColor = Color.White;
                    Dgv_payment.Rows[j].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    Dgv_payment.Rows[j].Cells[2].Style.ForeColor = Color.White;
                    Dgv_payment.Rows[j].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    Dgv_payment.Rows[j].Cells[3].Style.ForeColor = Color.White;
                    Dgv_payment.Rows[j].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    Dgv_payment.Rows[j].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    Dgv_payment.Rows[j].Cells[5].Style.ForeColor = Color.White;
                    Dgv_payment.Rows[j].Cells[5].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    Dgv_payment.Rows[j].Cells[6].Style.ForeColor = Color.White;
                    Dgv_payment.Rows[j].Cells[6].Style.ForeColor = Color.White;
                    Dgv_payment.Rows[j].Cells[0].Style.BackColor = Color.DarkGray;
                    Dgv_payment.Rows[j].Cells[1].Style.BackColor = Color.DarkGray;
                    Dgv_payment.Rows[j].Cells[2].Style.BackColor = Color.DarkGray;
                    Dgv_payment.Rows[j].Cells[3].Style.BackColor = Color.DarkGray;
                    Dgv_payment.Rows[j].Cells[4].Style.BackColor = Color.DarkGray;
                    Dgv_payment.Rows[j].Cells[5].Style.BackColor = Color.DarkGray;
                    Dgv_payment.Rows[j].Cells[6].Style.BackColor = Color.DarkGray;
                    Dgv_payment.Rows[j].Cells[7].Value = PappyjoeMVC.Properties.Resources.blank;
                    //Dgv_payment.Rows[j].Cells[7].Visible = false;
                    string receipt = "";
                    System.Data.DataTable Payments = this.cntrl.payment_details(Convert.ToDateTime(Payment.Rows[i]["payment_date"].ToString()).ToString("yyyy-MM-dd"), patient_id);
                    for (int k = 0; k < Payments.Rows.Count; k++)
                    {
                        string refund = "";
                        if (l >= 1)
                        {
                            receipt = Payments.Rows[k]["receipt_no"].ToString();
                            if (receipt == Payments.Rows[k - 1]["receipt_no"].ToString())
                            {
                                if (Convert.ToDecimal(Payments.Rows[k]["refund_amount"].ToString()) > 0)
                                {
                                    refund = Payments.Rows[k]["refund_amount"].ToString();
                                }
                                Dgv_payment.Rows.Add("", Payments.Rows[k]["invoice_no"].ToString(), Payments.Rows[k]["procedure_name"].ToString(), Payments.Rows[k]["mode_of_payment"].ToString(), String.Format("{0:C3}", Convert.ToDecimal(Payments.Rows[k]["amount_paid"].ToString())), refund, String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(Payment.Rows[i]["payment_date"].ToString())), "");
                                j = j + 1;
                                l = l + 1;
                                Dgv_payment.Rows[j].Cells[4].Style.ForeColor = Color.Red;
                                Dgv_payment.Rows[j].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                                Dgv_payment.Rows[j].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
                                Dgv_payment.Rows[j].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                                Dgv_payment.Rows[j].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                                Dgv_payment.Rows[j].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                                Dgv_payment.Rows[j].Cells[2].Style.ForeColor = Color.Black;
                                Dgv_payment.Rows[j].Cells[0].Style.ForeColor = Color.Red;
                                Dgv_payment.Rows[j].Cells[1].Style.ForeColor = Color.DodgerBlue;
                                Dgv_payment.Rows[j].Cells[7].Value = PappyjoeMVC.Properties.Resources.Bill;
                            }
                            else
                            {
                                if (Convert.ToDecimal(Payments.Rows[k]["refund_amount"].ToString()) > 0)
                                {
                                    refund = Payments.Rows[k]["refund_amount"].ToString();
                                }
                                Dgv_payment.Rows.Add("", "", "", "", "", "", "", "");
                                j = j + 1;
                                Dgv_payment.Rows.Add(Payments.Rows[k]["receipt_no"].ToString(), Payments.Rows[k]["invoice_no"].ToString(), Payments.Rows[k]["procedure_name"].ToString(), Payments.Rows[k]["mode_of_payment"].ToString(), String.Format("{0:C3}", Convert.ToDecimal(Payments.Rows[k]["amount_paid"].ToString())), refund, String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(Payment.Rows[i]["payment_date"].ToString())), "");
                                Dgv_payment.Rows[j].Cells[7].Value = PappyjoeMVC.Properties.Resources.blank;
                                j = j + 1;
                                l = l + 1;
                                Dgv_payment.Rows[j].Cells[4].Style.ForeColor = Color.Red;
                                Dgv_payment.Rows[j].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                                Dgv_payment.Rows[j].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
                                Dgv_payment.Rows[j].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                                Dgv_payment.Rows[j].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                                Dgv_payment.Rows[j].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                                Dgv_payment.Rows[j].Cells[2].Style.ForeColor = Color.Black;
                                Dgv_payment.Rows[j].Cells[0].Style.ForeColor = Color.Red;
                                Dgv_payment.Rows[j].Cells[1].Style.ForeColor = Color.DodgerBlue;
                                Dgv_payment.Rows[j].Cells[7].Value = PappyjoeMVC.Properties.Resources.Bill;
                            }
                        }
                        else
                        {
                            if (Convert.ToDecimal(Payments.Rows[k]["refund_amount"].ToString()) > 0)
                            {
                                refund = Payments.Rows[k]["refund_amount"].ToString();
                            }
                            Dgv_payment.Rows.Add(Payments.Rows[k]["receipt_no"].ToString(), Payments.Rows[k]["invoice_no"].ToString(), Payments.Rows[k]["procedure_name"].ToString(), Payments.Rows[k]["mode_of_payment"].ToString(), String.Format("{0:C3}", Convert.ToDecimal(Payments.Rows[k]["amount_paid"].ToString())), refund, String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(Payment.Rows[i]["payment_date"].ToString())), "");
                            j = j + 1;
                            l = l + 1;
                            Dgv_payment.Rows[j].Cells[4].Style.ForeColor = Color.Red;
                            Dgv_payment.Rows[j].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                            Dgv_payment.Rows[j].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
                            Dgv_payment.Rows[j].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                            Dgv_payment.Rows[j].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                            Dgv_payment.Rows[j].Cells[2].Style.ForeColor = Color.Black;
                            Dgv_payment.Rows[j].Cells[0].Style.ForeColor = Color.Red;
                            Dgv_payment.Rows[j].Cells[1].Style.ForeColor = Color.DodgerBlue;
                            Dgv_payment.Rows[j].Cells[7].Value = PappyjoeMVC.Properties.Resources.Bill;
                        }
                    }
                    Dgv_payment.Rows.Add("", "", "", "", "", "", "", "");
                    Dgv_payment.Rows[j + 1].Cells[7].Value = PappyjoeMVC.Properties.Resources.blank;
                    j = j + 2;
                }
            }
            
        }
        public void load_grid_showmore(DataTable Payment)
        {
            if (Payment.Rows.Count > 0)
            {
                int row = Dgv_payment.Rows.Count;
                foreach(DataRow dr in Payment.Rows)// for (int i = 0; i < Payment.Rows.Count; i++)
                {
                    int l = 0;
                    Dgv_payment.Rows.Add(String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(dr["payment_date"].ToString())), "", "", "", "", "", "", PappyjoeMVC.Properties.Resources.blank);
                    Dgv_payment.Rows[row].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                    Dgv_payment.Rows[row].Cells[0].Style.ForeColor = Color.DarkGreen;
                    //Dgv_payment.Rows[j].Cells[7].Value = PappyjoeMVC.Properties.Resources.blank;
                    row = row + 1;
                    Dgv_payment.Rows.Add("RECEIPT NUMBER", "INVOICES", "TREATMENTS", "MODE OF PAYMENT", "AMOUNT PAID", "REFUND AMOUNT", "Date", "");
                    Dgv_payment.Rows[row].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    Dgv_payment.Rows[row].Cells[0].Style.ForeColor = Color.White;
                    Dgv_payment.Rows[row].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    Dgv_payment.Rows[row].Cells[1].Style.ForeColor = Color.White;
                    Dgv_payment.Rows[row].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    Dgv_payment.Rows[row].Cells[2].Style.ForeColor = Color.White;
                    Dgv_payment.Rows[row].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    Dgv_payment.Rows[row].Cells[3].Style.ForeColor = Color.White;
                    Dgv_payment.Rows[row].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    Dgv_payment.Rows[row].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    Dgv_payment.Rows[row].Cells[5].Style.ForeColor = Color.White;
                    Dgv_payment.Rows[row].Cells[5].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    Dgv_payment.Rows[row].Cells[6].Style.ForeColor = Color.White;
                    Dgv_payment.Rows[row].Cells[6].Style.ForeColor = Color.White;
                    Dgv_payment.Rows[row].Cells[0].Style.BackColor = Color.DarkGray;
                    Dgv_payment.Rows[row].Cells[1].Style.BackColor = Color.DarkGray;
                    Dgv_payment.Rows[row].Cells[2].Style.BackColor = Color.DarkGray;
                    Dgv_payment.Rows[row].Cells[3].Style.BackColor = Color.DarkGray;
                    Dgv_payment.Rows[row].Cells[4].Style.BackColor = Color.DarkGray;
                    Dgv_payment.Rows[row].Cells[5].Style.BackColor = Color.DarkGray;
                    Dgv_payment.Rows[row].Cells[6].Style.BackColor = Color.DarkGray;
                    Dgv_payment.Rows[row].Cells[7].Value = PappyjoeMVC.Properties.Resources.blank;
                    //Dgv_payment.Rows[j].Cells[7].Visible = false;
                    string receipt = "";
                    System.Data.DataTable Payments = this.cntrl.payment_details(Convert.ToDateTime(dr["payment_date"].ToString()).ToString("yyyy-MM-dd"), patient_id);
                    for (int k = 0; k < Payments.Rows.Count; k++)
                    {
                        string refund = "";
                        if (l >= 1)
                        {
                            receipt = Payments.Rows[k]["receipt_no"].ToString();
                            if (receipt == Payments.Rows[k - 1]["receipt_no"].ToString())
                            {
                                if (Convert.ToDecimal(Payments.Rows[k]["refund_amount"].ToString()) > 0)
                                {
                                    refund = Payments.Rows[k]["refund_amount"].ToString();
                                }
                                Dgv_payment.Rows.Add("", Payments.Rows[k]["invoice_no"].ToString(), Payments.Rows[k]["procedure_name"].ToString(), Payments.Rows[k]["mode_of_payment"].ToString(), String.Format("{0:C3}", Convert.ToDecimal(Payments.Rows[k]["amount_paid"].ToString())), refund, String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(dr["payment_date"].ToString())), "");
                                row = row + 1;
                                l = l + 1;
                                Dgv_payment.Rows[row].Cells[4].Style.ForeColor = Color.Red;
                                Dgv_payment.Rows[row].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                                Dgv_payment.Rows[row].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
                                Dgv_payment.Rows[row].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                                Dgv_payment.Rows[row].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                                Dgv_payment.Rows[row].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                                Dgv_payment.Rows[row].Cells[2].Style.ForeColor = Color.Black;
                                Dgv_payment.Rows[row].Cells[0].Style.ForeColor = Color.Red;
                                Dgv_payment.Rows[row].Cells[1].Style.ForeColor = Color.DodgerBlue;
                                Dgv_payment.Rows[row].Cells[7].Value = PappyjoeMVC.Properties.Resources.Bill;
                            }
                            else
                            {
                                if (Convert.ToDecimal(Payments.Rows[k]["refund_amount"].ToString()) > 0)
                                {
                                    refund = Payments.Rows[k]["refund_amount"].ToString();
                                }
                                Dgv_payment.Rows.Add("", "", "", "", "", "", "", "");
                                row = row + 1;
                                Dgv_payment.Rows.Add(Payments.Rows[k]["receipt_no"].ToString(), Payments.Rows[k]["invoice_no"].ToString(), Payments.Rows[k]["procedure_name"].ToString(), Payments.Rows[k]["mode_of_payment"].ToString(), String.Format("{0:C3}", Convert.ToDecimal(Payments.Rows[k]["amount_paid"].ToString())), refund, String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(dr["payment_date"].ToString())), "");
                                Dgv_payment.Rows[row].Cells[7].Value = PappyjoeMVC.Properties.Resources.blank;
                                row = row + 1;
                                l = l + 1;
                                Dgv_payment.Rows[row].Cells[4].Style.ForeColor = Color.Red;
                                Dgv_payment.Rows[row].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                                Dgv_payment.Rows[row].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
                                Dgv_payment.Rows[row].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                                Dgv_payment.Rows[row].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                                Dgv_payment.Rows[row].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                                Dgv_payment.Rows[row].Cells[2].Style.ForeColor = Color.Black;
                                Dgv_payment.Rows[row].Cells[0].Style.ForeColor = Color.Red;
                                Dgv_payment.Rows[row].Cells[1].Style.ForeColor = Color.DodgerBlue;
                                Dgv_payment.Rows[row].Cells[7].Value = PappyjoeMVC.Properties.Resources.Bill;
                            }
                        }
                        else
                        {
                            if (Convert.ToDecimal(Payments.Rows[k]["refund_amount"].ToString()) > 0)
                            {
                                refund = Payments.Rows[k]["refund_amount"].ToString();
                            }
                            Dgv_payment.Rows.Add(Payments.Rows[k]["receipt_no"].ToString(), Payments.Rows[k]["invoice_no"].ToString(), Payments.Rows[k]["procedure_name"].ToString(), Payments.Rows[k]["mode_of_payment"].ToString(), String.Format("{0:C3}", Convert.ToDecimal(Payments.Rows[k]["amount_paid"].ToString())), refund, String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(dr["payment_date"].ToString())), "");
                            row = row + 1;
                            l = l + 1;
                            Dgv_payment.Rows[row].Cells[4].Style.ForeColor = Color.Red;
                            Dgv_payment.Rows[row].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                            Dgv_payment.Rows[row].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
                            Dgv_payment.Rows[row].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                            Dgv_payment.Rows[row].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                            Dgv_payment.Rows[row].Cells[2].Style.ForeColor = Color.Black;
                            Dgv_payment.Rows[row].Cells[0].Style.ForeColor = Color.Red;
                            Dgv_payment.Rows[row].Cells[1].Style.ForeColor = Color.DodgerBlue;
                            Dgv_payment.Rows[row].Cells[7].Value = PappyjoeMVC.Properties.Resources.Bill;
                        }
                    }
                    Dgv_payment.Rows.Add("", "", "", "", "", "", "", "");
                    Dgv_payment.Rows[row + 1].Cells[7].Value = PappyjoeMVC.Properties.Resources.blank;
                    row = row + 2;
                }
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
        public void reciept_count()
        {
            DataTable Payments = db.table("select receipt_no,amount_paid,invoice_no,procedure_name,mode_of_payment from tbl_payment where pt_id='" + patient_id + "'");
            if (Payments.Rows.Count > 0)
            {
                lb_Reciepts_cnt.Text = Payments.Rows.Count.ToString();
            }
            if (Payments.Rows.Count > 15)
            {
                label11.Visible = true;
            }
            else
            {
                label11.Visible = false;
            }
        }
        private void printToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataTable print = this .cntrl.get_printSettings();
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
                        print_html_thermal();
                    }
                    else
                    {
                        printall = "NO";
                        print_html();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Printing Error..", "Print error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void print_html_thermal()
        {
            try
            {
                System.Data.DataTable dtp = this.cntrl.get_company_details();
                System.Data.DataTable dt1 = this.cntrl.get_patientDetails(patient_id);
                string str_druglicenseno = "";
                string str_taxno = "";
                string doctor = this.cntrl.Get_DoctorName(doctor_id);
                if (doctor != "")
                {
                    str_druglicenseno = dtp.Rows[0]["Dl_Number"].ToString();
                    str_taxno = dtp.Rows[0]["Dl_Number2"].ToString();
                }
                string strfooter1 = "";
                string strfooter2 = "";
                string strfooter3 = "";
                string header1 = "";
                string header2 = "";
                string header3 = "";
                System.Data.DataTable print = this.cntrl.receipt_printSettings();
                if (print.Rows.Count > 0)
                {
                    header1 = print.Rows[0]["header"].ToString();
                    header2 = print.Rows[0]["left_text"].ToString();
                    header3 = print.Rows[0]["right_text"].ToString();
                    strfooter1 = print.Rows[0]["fullwidth_context"].ToString();
                    strfooter2 = print.Rows[0]["left_sign"].ToString();
                    strfooter3 = print.Rows[0]["right_sign"].ToString();
                }
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                StreamWriter sWrite = new StreamWriter(Apppath + "\\Receipt_thermal_print.html");
                sWrite.WriteLine("<html>");
                sWrite.WriteLine("<head>");
                sWrite.WriteLine("</head>");
                sWrite.WriteLine("<body >");
                sWrite.WriteLine("<br>");
                if (includeheader == "1")
                {
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
                        sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                        sWrite.WriteLine("</table>");
                    }
                }
                else
                {
                    sWrite.WriteLine("<table align='center' style='width:270px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td  align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=5></font></td></tr>");
                    sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3></font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                    sWrite.WriteLine("</table>");
                }
                sWrite.WriteLine("<table align='center' style='width:270px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr><th align='center'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3><b>RECEIPT</b></font></th></tr>");
                sWrite.WriteLine("</table>");
                string sexage = "", age = "";
                sWrite.WriteLine("<table align='center' style='width:270px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr><td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> <FONT COLOR=black>Date: </FONT>" + DateTime.Now.ToString("dd MMM yyyy") + "</font></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine("<tr>");
                if (dt1.Rows[0]["gender"].ToString() != "")
                {
                    sexage = dt1.Rows[0]["gender"].ToString();
                }
                if (dt1.Rows[0]["age"].ToString() != "")
                {
                    age = dt1.Rows[0]["age"].ToString() + " Years";
                }

                sWrite.WriteLine(" <td align='left' height=25><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Name :<b>" + dt1.Rows[0]["pt_name"].ToString() + "</b></font></td>");
                sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Sex:" + sexage + " </font></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Mobile :" + dt1.Rows[0]["primary_mobile_number"].ToString() + " </font></td>");
                sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>Age:" + age + " </font></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine("<tr><td colspan=2><hr></td></tr>");
                sWrite.WriteLine("</table>");
                string strsql = "";
                strsql = "select * from tbl_payment where payment_date='" + payment_date + "' and pt_id='" + patient_id + "' and receipt_no='" + receipt + "'";
                System.Data.DataTable dt_cf = db.table(strsql);
                var dateTimeNow = DateTime.Now;
                var tdate = dateTimeNow.ToShortDateString();
                if (dt_cf.Rows.Count > 0)
                {
                    sWrite.WriteLine("<table   align='center' style='width:270px;border: 1px ;border-collapse: collapse;' >");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td width='125' align='left'  bgcolor='#dcdcdc' height='30'><FONT COLOR=black FACE=' Segoe UI' SIZE=2><b>Receipt No</b></font></td>");
                    sWrite.WriteLine("<td width='127' align='center' bgcolor='#dcdcdc'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>Invoice No</b></font></td>");
                    sWrite.WriteLine("<td width='200' align='right' bgcolor='#dcdcdc'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>Amount</b></font></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr><td colspan=3><hr></td></tr>");
                    strsql = "select receipt_no,amount_paid,invoice_no,procedure_name,payment_date,mode_of_payment from tbl_payment where payment_date='" + payment_date + "' and pt_id='" + patient_id + "' and receipt_no='" + receipt + "' order by payment_date";
                    System.Data.DataTable dt_payment = db.table(strsql);
                    decimal total = 0; string mode = "";
                    for (int i = 0; i < dt_payment.Rows.Count; i++)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='left'   width='135'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;" + dt_payment.Rows[i]["receipt_no"].ToString() + " </font></td>");
                        sWrite.WriteLine("<td align='center'   width='147'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;" + dt_payment.Rows[i]["invoice_no"].ToString() + " </font></td>");
                        sWrite.WriteLine("<td align='right'   width='99'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;" + String.Format("{0:C2}", decimal.Parse(dt_payment.Rows[i]["amount_paid"].ToString())) + " </font></td>");
                        sWrite.WriteLine("</tr>");
                        total = total + decimal.Parse(dt_payment.Rows[i]["amount_paid"].ToString());
                        mode = dt_payment.Rows[i]["mode_of_payment"].ToString();
                    }
                    sWrite.WriteLine("<tr><td align='left' colspan=3><hr/></td></tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=3 align='right' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>Mode of Payment :<b>" + mode + " </b></font></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=3 align='right' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Grand Total :<b>" + String.Format("{0:C2}", decimal.Parse(total.ToString())) + " </b></font></td>");
                    sWrite.WriteLine("</tr>");
                    NumberToWords(Convert.ToInt32(total));
                    sWrite.WriteLine("<tr><td align='right' colspan=3><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1><b>(<i>" + words + "</i>)</b></fount> </td></tr>");
                    sWrite.WriteLine("</table>");
                }

                //footer
                sWrite.WriteLine("<table align='center'   style='width:270px;border: 1px ;border-collapse: collapse;' >");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='center' height='22'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;" + strfooter1 + "</font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='center' height='22'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;" + strfooter2 + "</font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='center' height='22'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;" + strfooter3 + "</font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<script>window.print();</script>");
                sWrite.WriteLine("</body>");
                sWrite.WriteLine("</html>");
                sWrite.Close();
                System.Diagnostics.Process.Start(Apppath + "\\Receipt_thermal_print.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Printing Error..", "Print error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void print_html()
        {
            try
            {
                System.Data.DataTable dtp = this.cntrl.get_company_details();
                System.Data.DataTable dt1 = this.cntrl.Get_Patient_Details(patient_id);
                string clinicn = "";
                string Clinic = "";
                clinicn = dtp.Rows[0][1].ToString();
                Clinic = clinicn.Replace("¤", "'");
                string doctorName = "";
                string streetaddress = "";
                string str_locality = "";
                string contact_no = "";
                string str_pincode = "";
                string str_email = "";
                string str_website = "";
                string str_druglicenseno = "";
                string str_taxno = "";
                string check = "";
                string header_image = "";
                string doctor = this.cntrl.Get_DoctorName(doctor_id);
                if (doctor!="")
                {
                    doctorName = doctor;
                    streetaddress = dtp.Rows[0]["street_address"].ToString();
                    contact_no = dtp.Rows[0]["contact_no"].ToString();
                    str_locality = dtp.Rows[0]["locality"].ToString();
                    str_pincode = dtp.Rows[0]["pincode"].ToString();
                    str_email = dtp.Rows[0]["email"].ToString();
                    str_website = dtp.Rows[0]["website"].ToString();
                    logo_name= dtp.Rows[0]["path"].ToString();
                    str_druglicenseno = dtp.Rows[0]["Dl_Number"].ToString();
                    str_taxno = dtp.Rows[0]["Dl_Number2"].ToString();
                  
                }
                string strfooter1 = "";
                string strfooter2 = "";
                string strfooter3 = "";
                string header1 = "";
                string header2 = "";
                string header3 = "";
                System.Data.DataTable print = this.cntrl.receipt_printSettings();
                if (print.Rows.Count > 0)
                {
                    header1 = print.Rows[0]["header"].ToString();
                    header2 = print.Rows[0]["left_text"].ToString();
                    header3 = print.Rows[0]["right_text"].ToString();
                    strfooter1 = print.Rows[0]["fullwidth_context"].ToString();
                    strfooter2 = print.Rows[0]["left_sign"].ToString();
                    strfooter3 = print.Rows[0]["right_sign"].ToString();
                    check = print.Rows[0]["set_as_default_header"].ToString();
                    header_image = print.Rows[0]["header_path"].ToString();
                }
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                StreamWriter sWrite = new StreamWriter(Apppath + "\\Receipt_print.html");
                sWrite.WriteLine("<html>");
                sWrite.WriteLine("<head>");
                sWrite.WriteLine("</head>");
                sWrite.WriteLine("<body >");
                sWrite.WriteLine("<br>");
                if (check =="Yes")
                {
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
                                sWrite.WriteLine("<td width='588' align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + header1 + "</font></td></tr>");
                                sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;&nbsp;" + header2 + "</font></td></tr>");
                                sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + header3 + "</font></td></tr>");
                                sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + str_druglicenseno + "&nbsp;&nbsp; " + str_taxno + "</font></td></tr>");
                                sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                                sWrite.WriteLine("</table>");
                            }
                            else
                            {
                                sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
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
                            sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                            sWrite.WriteLine("<tr>");
                            sWrite.WriteLine("<td  align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + header1 + "</font></td></tr>");
                            sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;&nbsp;" + header2 + "</font></td></tr>");
                            sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + header3 + "</font></td></tr>");
                            sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                            sWrite.WriteLine("</table>");
                        }
                    }
                    else
                    {
                        sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td  align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + header1 + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;&nbsp;" + header2 + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + header3 + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                        sWrite.WriteLine("</table>");
                    }
                }
                else
                {
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td  align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=5></font></td></tr>");
                    sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3></font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                    sWrite.WriteLine("</table>");
                }
                }
                else
                {
                    string paths = this.db.server();
                    if (File.Exists(paths + "\\" + header_image))
                    {
                        sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td  align='left'><img src='" + paths + "\\" + header_image + "' width='700' height='80';'></td>  ");
                        sWrite.WriteLine("</tr>");
                        sWrite.WriteLine("</table>");

                    }
                }

                string sexage = "";
                int Dexist = 0;
                string address = "";
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
                sWrite.WriteLine(" <td align='left' height=25><FONT COLOR=black FACE='Geneva, Arial' SIZE=2><b>" + dt1.Rows[0]["pt_name"].ToString() + "</b><i> (" + sexage + ")</i></font></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>Patient Id:" + dt1.Rows[0]["pt_id"].ToString() + " </font></td>");
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
                    sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>" + address + " </font></td>");
                    sWrite.WriteLine(" </tr>");
                }
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>" + dt1.Rows[0]["primary_mobile_number"].ToString() + " </font></td>");
                sWrite.WriteLine(" </tr>");
                if (dt1.Rows[0]["email_address"].ToString() != "")
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>" + dt1.Rows[0]["email_address"].ToString() + " </font></td>");
                    sWrite.WriteLine(" </tr>");
                }
                sWrite.WriteLine("<tr><td colspan=2><hr></td></tr>");
                sWrite.WriteLine("</table>");
                string strsql = "";
                if (printall == "YES")
                {
                    strsql = "select * from tbl_payment where pt_id='" + patient_id + "'";
                }
                else
                {
                    strsql = "select * from tbl_payment where payment_date='" + payment_date + "' and pt_id='" + patient_id + "' and receipt_no='" + receipt + "'";
                }
                System.Data.DataTable dt_cf = db.table(strsql);
                var dateTimeNow = DateTime.Now;
                var tdate = dateTimeNow.ToShortDateString();
                if (dt_cf.Rows.Count > 0)
                {
                    if (dt_cf.Rows[0]["payment_date"].ToString() != null)
                    { tdate = dt_cf.Rows[0]["payment_date"].ToString(); }
                    else
                        tdate = null;
                    sWrite.WriteLine("<br>");
                    sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td><FONT COLOR=black FACE='Geneva, Arial' SIZE=5>Payment</FONT></td>");
                    sWrite.WriteLine("<td width=450px></td>");
                    if (printall == "YES")
                    {
                        sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2> <FONT COLOR=black>Date : </FONT>" + DateTime.Now.ToString("dd MMM yyyy") + "</font></td>");
                    }
                    else
                    {
                        sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2> <FONT COLOR=black>Date : </FONT>" + Convert.ToDateTime(tdate).ToString("dd MMM yyyy") + "</font></td>");
                    }
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("</table>");
                    sWrite.WriteLine("<table   align='center' style='width:700px;border: 1px ;border-collapse: collapse;' >");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td width='36' height='30' align='center'  bgcolor='#dcdcdc'><FONT COLOR=black FACE=' Arial' SIZE=3>Sl</font></td>");
                    sWrite.WriteLine("<td width='125' align='center'  bgcolor='#dcdcdc' height='30'><FONT COLOR=black FACE=' Arial' SIZE=3>Receipt Number</font></td>");
                    sWrite.WriteLine("<td width='127' align='center' bgcolor='#dcdcdc'><FONT COLOR=black FACE='Geneva, Arial' SIZE=3>Invoice Number</font></td>");
                    sWrite.WriteLine("<td width='200' align='left' bgcolor='#dcdcdc'><FONT COLOR=black FACE='Geneva, Arial' SIZE=3>Procedure Name</font></td>");

                    //sWrite.WriteLine("<td width='259' align='left' bgcolor='#dcdcdc'><FONT COLOR=black FACE='Geneva, Arial' SIZE=3>Procedure Name</font></td>");
                    sWrite.WriteLine("<td width='200' align='left' bgcolor='#dcdcdc'><FONT COLOR=black FACE='Geneva, Arial' SIZE=3>Mode of Payment</font></td>");
                    sWrite.WriteLine("<td width='99' align='right' bgcolor='#dcdcdc'><FONT COLOR=black FACE='Geneva, Arial' SIZE=3>Amount Paid</font></td>");
                    sWrite.WriteLine("</tr>");
                    strsql = "";
                    if (printall == "YES")
                    {
                        strsql = "select receipt_no,amount_paid,invoice_no,procedure_name,payment_date,mode_of_payment from tbl_payment where pt_id='" + patient_id + "' order by payment_date";
                    }
                    else
                    {
                        strsql = "select receipt_no,amount_paid,invoice_no,procedure_name,payment_date,mode_of_payment from tbl_payment where payment_date='" + payment_date + "' and pt_id='" + patient_id + "' and receipt_no='" + receipt + "' order by payment_date";
                    }
                    System.Data.DataTable dt_payment = db.table(strsql);
                    decimal total = 0;
                    for (int i = 0; i < dt_payment.Rows.Count; i++)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='center'  height='30'><FONT COLOR=black FACE=' Arial' SIZE=2>" + (i + 1) + " </font></td>");
                        sWrite.WriteLine("<td align='center'   width='135'  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + dt_payment.Rows[i]["receipt_no"].ToString() + " </font></td>");
                        sWrite.WriteLine("<td align='center'   width='147'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + dt_payment.Rows[i]["invoice_no"].ToString() + " </font></td>");
                        sWrite.WriteLine("<td align='left'   width='259'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + dt_payment.Rows[i]["procedure_name"].ToString() + " </font></td>");
                        sWrite.WriteLine("<td align='left'   width='259'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + dt_payment.Rows[i]["mode_of_payment"].ToString() + " </font></td>");
                        sWrite.WriteLine("<td align='right'   width='99'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(dt_payment.Rows[i]["amount_paid"].ToString())) + " </font></td>");
                        sWrite.WriteLine("</tr>");
                        total = total + decimal.Parse(dt_payment.Rows[i]["amount_paid"].ToString());
                    }
                    sWrite.WriteLine("<tr><td align='left' colspan=6><hr/></td></tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td></td>");
                    sWrite.WriteLine("<td></td>");
                    sWrite.WriteLine("<td></td>");
                    sWrite.WriteLine("<td></td>");
                    sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=3><b>" + String.Format("{0:C2}", decimal.Parse(total.ToString())) + " </b></font></td>");
                    sWrite.WriteLine("</tr>");
                    //sWrite.WriteLine("<tr><td align='right' colspan=6><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>(<i>" + NumWordsWrapper(double.Parse(total.ToString())) + "</i>)</fount> </td></tr>");
                    NumberToWords(Convert.ToInt32(total));
                    sWrite.WriteLine("<tr><td align='right' colspan=6><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>(<i>" + words + "</i>)</fount> </td></tr>");
                    sWrite.WriteLine("</table>");
                }

                //footer
                sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");
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
                sWrite.WriteLine("</body>");
                sWrite.WriteLine("</html>");
                sWrite.Close();
                System.Diagnostics.Process.Start(Apppath + "\\Receipt_print.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Printing Error..", "Print error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        public string words = "";
        public string NumberToWords(int number)//////to convert amount to correct denomination in words
        {
            if (number == 0) { return "zero"; }
            if (number < 0) { return "minus " + NumberToWords(Math.Abs(number)); }
            words = "";
            if ((number / 10000000) > 0) { words += NumberToWords(number / 10000000) + " Crore "; number %= 10000000; }
            if ((number / 100000) > 0) { words += NumberToWords(number / 100000) + " Lakh "; number %= 100000; }
            if ((number / 1000) > 0) { words += NumberToWords(number / 1000) + " Thousand "; number %= 1000; }
            if ((number / 100) > 0) { words += NumberToWords(number / 100) + " Hundred "; number %= 100; }
            if (number > 0)
            {
                if (words != "") { words += "and "; }
                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "seventy", "Eighty", "Ninety" };
                if (number < 20) { words += unitsMap[number]; }
                else { words += tensMap[number / 10]; if ((number % 10) > 0) { words += "-" + unitsMap[number % 10]; } }
            }
            return words;
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

        private void btn_Add_Click(object sender, EventArgs e)
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
        public string proname = "";
        private void Dgv_payment_MouseClick(object sender, MouseEventArgs e)
        {
            int currentMouseOverRow = Dgv_payment.HitTest(e.X, e.Y).RowIndex;
            int currentMouseOverColumn = Dgv_payment.HitTest(e.X, e.Y).ColumnIndex;
            if (currentMouseOverRow >= 0)
            {
                if (currentMouseOverColumn == 7)
                {
                    if (Dgv_payment.Rows[currentMouseOverRow].Cells[6].Value.ToString() != "" && Dgv_payment.Rows[currentMouseOverRow].Cells[5].Value.ToString() != "0")
                    {
                        payment_date = Convert.ToDateTime(Dgv_payment.Rows[currentMouseOverRow].Cells[6].Value.ToString()).ToString("yyyy-MM-dd");
                        receipt = Dgv_payment.Rows[currentMouseOverRow].Cells[0].Value.ToString();
                        invno = Dgv_payment.Rows[currentMouseOverRow].Cells[1].Value.ToString();
                        proname = Dgv_payment.Rows[currentMouseOverRow].Cells[2].Value.ToString();
                        contextMenuStrip1.Show(Dgv_payment, new System.Drawing.Point(915 - 120, e.Y));
                    }
                }
            }
        }
        private void Lab_AllPatients_Click(object sender, EventArgs e)
        {
            var form2 = new Patients();
            form2.doctor_id = doctor_id;
            openform(form2);
        }

        private void Lab_Appointment_Click(object sender, EventArgs e)
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
        private void Lab_Profile_Click(object sender, EventArgs e)
        {
            var form2 = new PappyjoeMVC.View.Patient_Profile_Details();
            form2.doctor_id = doctor_id;
            form2.patient_id = patient_id;
            openform(form2);
        }

        private void Lab_VitalSigns_Click(object sender, EventArgs e)
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
        private void Lab_Treatment_Click(object sender, EventArgs e)
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

        private void Lab_Finished_Click(object sender, EventArgs e)
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

        private void Lab_Attachmnt_Click(object sender, EventArgs e)
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

        private void Lab_Invoice_Click(object sender, EventArgs e)
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

        private void Lab_Payment_Click(object sender, EventArgs e)
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

        private void Lab_Ledger_Click(object sender, EventArgs e)
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

        private void Lab_Clinical_Click(object sender, EventArgs e)
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

        private void Lab_Prescription_Click(object sender, EventArgs e)
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

        private void printAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                    System.Data.DataTable print = this.cntrl.get_printSettings();
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
                    }
                    printall = "YES";
                    print_html();
            }
            catch
            {
                MessageBox.Show("Printing Error..", "Print error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void showAdvanceDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panl_advance.Visible = true;
            DataTable dtb = this.cntrl.getall_advance(patient_id);
            int k =1;
            if(dtb.Rows.Count>0)
            {
                dgv_advance.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_advance.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_advance.Rows.Clear();
                for (int i = 0; i < dtb.Rows.Count; i++)
                {

                    dgv_advance.Rows.Add(k, Convert.ToDateTime(dtb.Rows[i]["Date"].ToString()).ToString("MM/dd/yyy"), dtb.Rows[i]["PaymentMethod"].ToString(), dtb.Rows[i]["form"].ToString(), string.Format("{0:C2}", decimal.Parse(dtb.Rows[i]["Amount"].ToString())));
                    k++;
                }
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            panl_advance.Visible = false;
        }

        private void refundAdvanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panl_refund.Visible = true;
            panl_refund.Location = new Point(299, 81);
        }

        private void btn_refund_amount_Click(object sender, EventArgs e)
        {
            DataTable dtb = this.cntrl.gt_pt_advance(patient_id);
            
            if (txt_adv_refund.Text!="")
            {
                if(dtb.Rows.Count>0)
                {
                    decimal adv =Convert.ToDecimal( dtb.Rows[0][0].ToString());
                    if(Convert.ToDecimal( txt_adv_refund.Text)<=adv)
                    {
                        decimal refund = 0;
                        refund = adv - Convert.ToDecimal(txt_adv_refund.Text);
                        this.cntrl.update_advance(refund, patient_id);
                        this.cntrl.Save_advancetable(patient_id, DateTime.Now.Date.ToString("yyyy-MM-dd"), txt_adv_refund.Text, "cash", "credit","Advance Amount Returned");
                        //lblAdvance.Show(); adv_refund.Visible = true;
                        lblAdvance.Text = "Available advance: " + string.Format("{0:C3}", decimal.Parse(refund.ToString()));
                        txt_adv_refund.Text = "";
                        MessageBox.Show("Advance is refunded..","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Entered amount is greater than advance amount...","Invalid",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {

        }

        private void btn_refund_calcel_Click(object sender, EventArgs e)
        {
            panl_refund.Visible = false;
        }

        private void Adv_details_Click(object sender, EventArgs e)
        {
            contextMenuStrip2.Show(new System.Drawing.Point(866, 83));
        }

        public static string apntid = "";
        private void pb_AppntmntAdd_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id = privi_mdl.Add_privillege(doctor_id);
                if (int.Parse(id) > 0)
                {
                    var form2 = new Add_Appointment();
                    form2.patient_id = patient_id;
                    form2.doctor_id = doctor_id;
                    openform(form2);
                }
                else
                {

                    MessageBox.Show("There is No Privilege to add appointment", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                var form2 = new Add_Appointment();
                form2.patient_id = patient_id;
                form2.doctor_id = doctor_id;
                openform(form2);
            }
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
        Communication_Setting_controller com_cntrl = new Communication_Setting_controller();
        Booking_controller b_cntrl = new Booking_controller();
        Invoice_controller incntrl = new Invoice_controller();
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
                        System.Data.DataTable clinicname = this.incntrl.Get_companynameNo();
                        if (clinicname.Rows.Count > 0)
                        {
                            clinic = clinicname.Rows[0][0].ToString();
                            //clinic = cname.Replace("¤", "'");
                            contact_no = clinicname.Rows[0][1].ToString();
                        }
                        SMS_model a = new SMS_model();
                        System.Data.DataTable pat = this.incntrl.Get_patient_id_name_gender(patient_id);
                        System.Data.DataTable smsreminder = this.incntrl.remindersms();


                       string strsql = "";
                        if (printall == "YES")
                        {
                            strsql = "select receipt_no,amount_paid,invoice_no,procedure_name,payment_date,mode_of_payment from tbl_payment where pt_id='" + patient_id + "' order by payment_date";
                        }
                        else
                        {
                            strsql = "select receipt_no,amount_paid,invoice_no,procedure_name,payment_date,mode_of_payment from tbl_payment where payment_date='" + payment_date + "' and pt_id='" + patient_id + "' and receipt_no='" + receipt + "' order by payment_date";
                        }
                        System.Data.DataTable dt_pt_sub = db.table(strsql);
                        decimal total = 0;


                        //System.Data.DataTable dt_pt_sub = this.cntrl.get_invoiceDetails(invoice_plan_id);
                        if (pat.Rows.Count > 0)
                        {
                            if (dt_pt_sub.Rows.Count > 0)
                            {
                                //System.Data.DataTable dt_invoice_main = this.cntrl.Load_invoice_mainDetails(patient_id);
                                Double totalEst = 0;
                                Decimal total_cost = 0;
                                Decimal total_discount = 0;
                                Decimal total_tax = 0;
                                decimal due = 0;
                                string discount_string = "";
                                while (p < dt_pt_sub.Rows.Count)
                                {

                                    Decimal totalcost = Convert.ToDecimal(dt_pt_sub.Rows[p]["cost"].ToString()) * Convert.ToDecimal(dt_pt_sub.Rows[p]["unit"].ToString());
                                    total_cost = total_cost + Convert.ToDecimal(totalcost);
                                    total_discount = total_discount + Convert.ToDecimal(dt_pt_sub.Rows[p]["discountin_rs"].ToString());
                                    total_tax = total_tax + Convert.ToDecimal(dt_pt_sub.Rows[p]["tax_inrs"].ToString());
                                    totalEst = totalEst + Convert.ToDouble(dt_pt_sub.Rows[p]["total"].ToString());
                                    due = Convert.ToDecimal(dt_pt_sub.Rows[p]["total"].ToString());
                                    strPriscription = strPriscription + " [" + dt_pt_sub.Rows[p]["services"].ToString() + "]" + "(" + dt_pt_sub.Rows[p]["unit"].ToString() + ")" + ", Cost: " + String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[p]["cost"].ToString())) + ", Discount: " + String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[p]["discountin_rs"].ToString())) + ", Tax: " + String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[p]["tax_inrs"].ToString())) + ", Total: " + String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[p]["total"].ToString())) + "\n\n";



                                    //sWrite.WriteLine("<td align='center'  height='30'><FONT COLOR=black FACE=' Arial' SIZE=2>" + (i + 1) + " </font></td>");
                                    //sWrite.WriteLine("<td align='center'   width='135'  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + dt_payment.Rows[i]["receipt_no"].ToString() + " </font></td>");
                                    //sWrite.WriteLine("<td align='center'   width='147'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + dt_payment.Rows[i]["invoice_no"].ToString() + " </font></td>");
                                    //sWrite.WriteLine("<td align='left'   width='259'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + dt_payment.Rows[i]["procedure_name"].ToString() + " </font></td>");
                                    //sWrite.WriteLine("<td align='left'   width='259'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + dt_payment.Rows[i]["mode_of_payment"].ToString() + " </font></td>");
                                    //sWrite.WriteLine("<td align='right'   width='99'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(dt_payment.Rows[i]["amount_paid"].ToString())) + " </font></td>");
                                    //sWrite.WriteLine("</tr>");
                                    //total = total + decimal.Parse(dt_payment.Rows[i]["amount_paid"].ToString());
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
        Common_model cmodel = new Common_model();
        private void emailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataTable dtp = this.cntrl.get_company_details();
                System.Data.DataTable dt1 = this.cntrl.Get_Patient_Details(patient_id);
                string clinicn = "";
                string Clinic = "";
                clinicn = dtp.Rows[0][1].ToString();
                Clinic = clinicn.Replace("¤", "'");
                string doctorName = "";
                string streetaddress = "";
                string str_locality = "";
                string contact_no = "";
                string str_pincode = "";
                string str_email = "";
                string str_website = "";
                string str_druglicenseno = "";
                string str_taxno = "";
                string doctor = this.cntrl.Get_DoctorName(doctor_id);
                if (doctor != "")
                {
                    doctorName = doctor;
                    streetaddress = dtp.Rows[0]["street_address"].ToString();
                    contact_no = dtp.Rows[0]["contact_no"].ToString();
                    str_locality = dtp.Rows[0]["locality"].ToString();
                    str_pincode = dtp.Rows[0]["pincode"].ToString();
                    str_email = dtp.Rows[0]["email"].ToString();
                    str_website = dtp.Rows[0]["website"].ToString();
                    logo_name = dtp.Rows[0]["path"].ToString();
                    str_druglicenseno = dtp.Rows[0]["Dl_Number"].ToString();
                    str_taxno = dtp.Rows[0]["Dl_Number2"].ToString();
                }
                string strfooter1 = "";
                string strfooter2 = "";
                string strfooter3 = "";
                string header1 = "";
                string header2 = "";
                string header3 = "";
                System.Data.DataTable print = this.cntrl.receipt_printSettings();
                if (print.Rows.Count > 0)
                {
                    header1 = print.Rows[0]["header"].ToString();
                    header2 = print.Rows[0]["left_text"].ToString();
                    header3 = print.Rows[0]["right_text"].ToString();
                    strfooter1 = print.Rows[0]["fullwidth_context"].ToString();
                    strfooter2 = print.Rows[0]["left_sign"].ToString();
                    strfooter3 = print.Rows[0]["right_sign"].ToString();
                }
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                StreamWriter sWrite = new StreamWriter(Apppath + "\\Receipt.html");
                sWrite.WriteLine("<html>");
                sWrite.WriteLine("<head>");
                sWrite.WriteLine("</head>");
                sWrite.WriteLine("<body >");
                sWrite.WriteLine("<br>");
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
                                sWrite.WriteLine("<td width='588' align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + header1 + "</font></td></tr>");
                                sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;&nbsp;" + header2 + "</font></td></tr>");
                                sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + header3 + "</font></td></tr>");
                                sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + str_druglicenseno + "&nbsp;&nbsp; " + str_taxno + "</font></td></tr>");
                                sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                                sWrite.WriteLine("</table>");
                            }
                            else
                            {
                                sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
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
                            sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                            sWrite.WriteLine("<tr>");
                            sWrite.WriteLine("<td  align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + header1 + "</font></td></tr>");
                            sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;&nbsp;" + header2 + "</font></td></tr>");
                            sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + header3 + "</font></td></tr>");
                            sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                            sWrite.WriteLine("</table>");
                        }
                    }
                    else
                    {
                        sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td  align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + header1 + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;&nbsp;" + header2 + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + header3 + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                        sWrite.WriteLine("</table>");
                    }
                }
                else
                {
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td  align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=5></font></td></tr>");
                    sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3></font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                    sWrite.WriteLine("</table>");
                }
                string sexage = "";
                int Dexist = 0;
                string address = "";
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
                sWrite.WriteLine(" <td align='left' height=25><FONT COLOR=black FACE='Geneva, Arial' SIZE=2><b>" + dt1.Rows[0]["pt_name"].ToString() + "</b><i> (" + sexage + ")</i></font></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>Patient Id:" + dt1.Rows[0]["pt_id"].ToString() + " </font></td>");
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
                    sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>" + address + " </font></td>");
                    sWrite.WriteLine(" </tr>");
                }
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>" + dt1.Rows[0]["primary_mobile_number"].ToString() + " </font></td>");
                sWrite.WriteLine(" </tr>");
                if (dt1.Rows[0]["email_address"].ToString() != "")
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>" + dt1.Rows[0]["email_address"].ToString() + " </font></td>");
                    sWrite.WriteLine(" </tr>");
                }
                sWrite.WriteLine("<tr><td colspan=2><hr></td></tr>");
                sWrite.WriteLine("</table>");
                string strsql = "";
                if (printall == "YES")
                {
                    strsql = "select * from tbl_payment where pt_id='" + patient_id + "'";
                }
                else
                {
                    strsql = "select * from tbl_payment where payment_date='" + payment_date + "' and pt_id='" + patient_id + "' and receipt_no='" + receipt + "'";
                }
                System.Data.DataTable dt_cf = db.table(strsql);
                var dateTimeNow = DateTime.Now;
                var tdate = dateTimeNow.ToShortDateString();
                if (dt_cf.Rows.Count > 0)
                {
                    if (dt_cf.Rows[0]["payment_date"].ToString() != null)
                    { tdate = dt_cf.Rows[0]["payment_date"].ToString(); }
                    else
                        tdate = null;
                    sWrite.WriteLine("<br>");
                    sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td><FONT COLOR=black FACE='Geneva, Arial' SIZE=5>Payment</FONT></td>");
                    sWrite.WriteLine("<td width=450px></td>");
                    if (printall == "YES")
                    {
                        sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2> <FONT COLOR=black>Date : </FONT>" + DateTime.Now.ToString("dd MMM yyyy") + "</font></td>");
                    }
                    else
                    {
                        sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2> <FONT COLOR=black>Date : </FONT>" + Convert.ToDateTime(tdate).ToString("dd MMM yyyy") + "</font></td>");
                    }
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("</table>");
                    sWrite.WriteLine("<table   align='center' style='width:700px;border: 1px ;border-collapse: collapse;' >");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td width='36' height='30' align='center'  bgcolor='#dcdcdc'><FONT COLOR=black FACE=' Arial' SIZE=3>Sl</font></td>");
                    sWrite.WriteLine("<td width='125' align='center'  bgcolor='#dcdcdc' height='30'><FONT COLOR=black FACE=' Arial' SIZE=3>Receipt Number</font></td>");
                    sWrite.WriteLine("<td width='127' align='center' bgcolor='#dcdcdc'><FONT COLOR=black FACE='Geneva, Arial' SIZE=3>Invoice Number</font></td>");
                    sWrite.WriteLine("<td width='200' align='left' bgcolor='#dcdcdc'><FONT COLOR=black FACE='Geneva, Arial' SIZE=3>Procedure Name</font></td>");

                    //sWrite.WriteLine("<td width='259' align='left' bgcolor='#dcdcdc'><FONT COLOR=black FACE='Geneva, Arial' SIZE=3>Procedure Name</font></td>");
                    sWrite.WriteLine("<td width='200' align='left' bgcolor='#dcdcdc'><FONT COLOR=black FACE='Geneva, Arial' SIZE=3>Mode of Payment</font></td>");
                    sWrite.WriteLine("<td width='99' align='right' bgcolor='#dcdcdc'><FONT COLOR=black FACE='Geneva, Arial' SIZE=3>Amount Paid</font></td>");
                    sWrite.WriteLine("</tr>");
                    strsql = "";
                    if (printall == "YES")
                    {
                        strsql = "select receipt_no,amount_paid,invoice_no,procedure_name,payment_date,mode_of_payment from tbl_payment where pt_id='" + patient_id + "' order by payment_date";
                    }
                    else
                    {
                        strsql = "select receipt_no,amount_paid,invoice_no,procedure_name,payment_date,mode_of_payment from tbl_payment where payment_date='" + payment_date + "' and pt_id='" + patient_id + "' and receipt_no='" + receipt + "' order by payment_date";
                    }
                    System.Data.DataTable dt_payment = db.table(strsql);
                    decimal total = 0;
                    for (int i = 0; i < dt_payment.Rows.Count; i++)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='center'  height='30'><FONT COLOR=black FACE=' Arial' SIZE=2>" + (i + 1) + " </font></td>");
                        sWrite.WriteLine("<td align='center'   width='135'  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + dt_payment.Rows[i]["receipt_no"].ToString() + " </font></td>");
                        sWrite.WriteLine("<td align='center'   width='147'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + dt_payment.Rows[i]["invoice_no"].ToString() + " </font></td>");
                        sWrite.WriteLine("<td align='left'   width='259'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + dt_payment.Rows[i]["procedure_name"].ToString() + " </font></td>");
                        sWrite.WriteLine("<td align='left'   width='259'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + dt_payment.Rows[i]["mode_of_payment"].ToString() + " </font></td>");
                        sWrite.WriteLine("<td align='right'   width='99'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(dt_payment.Rows[i]["amount_paid"].ToString())) + " </font></td>");
                        sWrite.WriteLine("</tr>");
                        total = total + decimal.Parse(dt_payment.Rows[i]["amount_paid"].ToString());
                    }
                    sWrite.WriteLine("<tr><td align='left' colspan=6><hr/></td></tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td></td>");
                    sWrite.WriteLine("<td></td>");
                    sWrite.WriteLine("<td></td>");
                    sWrite.WriteLine("<td></td>");
                    sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=3><b>" + String.Format("{0:C2}", decimal.Parse(total.ToString())) + " </b></font></td>");
                    sWrite.WriteLine("</tr>");
                    //sWrite.WriteLine("<tr><td align='right' colspan=6><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>(<i>" + NumWordsWrapper(double.Parse(total.ToString())) + "</i>)</fount> </td></tr>");
                    NumberToWords(Convert.ToInt32(total));
                    sWrite.WriteLine("<tr><td align='right' colspan=6><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>(<i>" + words + "</i>)</fount> </td></tr>");
                    sWrite.WriteLine("</table>");
                }

                //footer
                sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");
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
                                StreamReader reader = new StreamReader(Apppath + "\\Receipt.html");
                                string readFile = reader.ReadToEnd();
                                string StrContent = "";
                                StrContent = readFile;
                                MailMessage message = new MailMessage();
                                message.From = new MailAddress(email);
                                message.To.Add(email);
                                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                                message.Subject = "Pappyjoe Receipt";
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
                //System.Diagnostics.Process.Start(Apppath + "\\Receipt.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Printing Error..", "Print error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        public string invno = "";
        user_privillage_model privi_mdl = new user_privillage_model();
        private void refundAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (invno != "")  //receipt
            {
                decimal a = 0; lbl_amount.Text = "";
                if (doctor_id != "1")
                {
                    string id = privi_mdl.refund_add(doctor_id);
                    if (int.Parse(id) > 0)
                    {
                        System.Data.DataTable Payments = this.cntrl.payment_details_re(Convert.ToDateTime(payment_date).ToString("yyyy-MM-dd"), patient_id, invno, proname);
                        if (Payments.Rows.Count > 0)
                        {
                            if (Convert.ToDecimal(Payments.Rows[0]["refund_amount"].ToString()) > 0)
                            {
                                if (Convert.ToDecimal(Payments.Rows[0]["refund_amount"].ToString()) >= Convert.ToDecimal(Payments.Rows[0]["amount_paid"].ToString()))
                                {
                                    MessageBox.Show("This receipt can't refund", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    panl_amount_refund.Visible = false;
                                    return;
                                }
                                else
                                {
                                    txt_rece.Text = Payments.Rows[0]["receipt_no"].ToString();
                                    txt_inv.Text = Payments.Rows[0]["invoice_no"].ToString();
                                    txt_total.Text = Payments.Rows[0]["amount_paid"].ToString();
                                    a = Convert.ToDecimal(Payments.Rows[0]["amount_paid"].ToString()) - Convert.ToDecimal(Payments.Rows[0]["refund_amount"].ToString());
                                    txt_total.Text = a.ToString();// Payments.Rows[0]["amount_paid"].ToString();
                                    lbl_amount.Text = Convert.ToDecimal(Convert.ToDecimal(Payments.Rows[0]["refund_amount"].ToString()) + a).ToString();
                                    panl_amount_refund.Visible = true;
                                    panl_amount_refund.Location = new Point(299, 129);
                                }
                            }
                            else
                            {
                                txt_rece.Text = Payments.Rows[0]["receipt_no"].ToString();
                                txt_inv.Text = Payments.Rows[0]["invoice_no"].ToString();
                                txt_total.Text = Payments.Rows[0]["amount_paid"].ToString();
                                panl_amount_refund.Visible = true;
                                panl_amount_refund.Location = new Point(299, 129);

                            }

                        }

                    }
                    else
                    {
                        MessageBox.Show("There is No Privilege to Add Payment Refund", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;

                    }
                }
                else
                {
                    System.Data.DataTable Payments = this.cntrl.payment_details_re(Convert.ToDateTime(payment_date).ToString("yyyy-MM-dd"), patient_id, invno, proname);
                    if (Payments.Rows.Count > 0)
                    {
                        if (Convert.ToDecimal(Payments.Rows[0]["refund_amount"].ToString()) > 0)
                        {
                            if (Convert.ToDecimal(Payments.Rows[0]["refund_amount"].ToString()) >= Convert.ToDecimal(Payments.Rows[0]["amount_paid"].ToString()))
                            {
                                MessageBox.Show("This receipt can't refund", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                panl_amount_refund.Visible = false;
                                return;
                            }
                            else
                            {
                                txt_rece.Text = Payments.Rows[0]["receipt_no"].ToString();
                                txt_inv.Text = Payments.Rows[0]["invoice_no"].ToString();
                                txt_total.Text = Payments.Rows[0]["amount_paid"].ToString();
                                a = Convert.ToDecimal(Payments.Rows[0]["amount_paid"].ToString()) - Convert.ToDecimal(Payments.Rows[0]["refund_amount"].ToString());
                                txt_total.Text = a.ToString();// Payments.Rows[0]["amount_paid"].ToString();
                                lbl_amount.Text = Convert.ToDecimal(Convert.ToDecimal(Payments.Rows[0]["refund_amount"].ToString()) + a).ToString();
                                panl_amount_refund.Visible = true;
                                panl_amount_refund.Location = new Point(299, 129);
                            }
                        }
                        else
                        {
                            txt_rece.Text = Payments.Rows[0]["receipt_no"].ToString();
                            txt_inv.Text = Payments.Rows[0]["invoice_no"].ToString();
                            txt_total.Text = Payments.Rows[0]["amount_paid"].ToString();
                            panl_amount_refund.Visible = true;
                            panl_amount_refund.Location = new Point(299, 129);

                        }

                    }

                }



            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panl_amount_refund.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txt_amont_refund.Text) > 0)
            {
                if (Convert.ToDecimal(txt_amont_refund.Text) <= Convert.ToDecimal(txt_total.Text))
                {
                    if (lbl_amount.Text != "")
                    {
                        this.cntrl.update_refund("Refund", lbl_amount.Text, txt_rece.Text, proname);

                    }
                    else
                    {
                        this.cntrl.update_refund("Refund", txt_amont_refund.Text, txt_rece.Text, proname);
                    }
                    DialogResult rslt = MessageBox.Show("Amount Refund Saved...! Do you want print receipt...? ", "Print As...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rslt == DialogResult.Yes)
                    {
                        refund_print();
                    }

                }
                else
                {
                    MessageBox.Show("Amount greater than total amount ", "Can't Refund", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }

                //MessageBox.Show("Amount Refund Successfully ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panl_amount_refund.Visible = false;
            }
        }
        public void refund_print()
        {
            try
            {
                string Logopath = "";
                string str_name = "";
                string str_street_address = "";
                string str_locality = "";
                string str_pincode = "";
                string str_contact_no = "";
                string str_email = "";
                string str_website = "";
                string patiID = "";
                string PatName = "";
                string address = "";
                DataTable dt_patent = new DataTable();
                dt_patent = this.cntrl.get_patientDetails(patient_id);
                if (dt_patent.Rows.Count > 0)
                {
                    string strage = "", strgener = "";
                    patiID = dt_patent.Rows[0]["pt_id"].ToString();
                    if (dt_patent.Rows[0]["age"].ToString() != "")
                    {
                        if (dt_patent.Rows[0]["gender"].ToString() != "")
                        {
                            strage = " (" + dt_patent.Rows[0]["age"].ToString();
                        }
                        else
                        { strage = " (" + dt_patent.Rows[0]["age"].ToString() + ")"; }
                    }
                    if (dt_patent.Rows[0]["gender"].ToString() != "")
                    {
                        if (strage != "")
                        {
                            strgener = "/" + dt_patent.Rows[0]["gender"].ToString() + ")";
                        }
                        else
                        {
                            strgener = " (" + dt_patent.Rows[0]["gender"].ToString() + ")";
                        }
                    }
                    PatName = dt_patent.Rows[0]["pt_name"].ToString() + strage + strgener;
                    address = dt_patent.Rows[0]["street_address"].ToString();
                }
                System.Data.DataTable dtp = this.cntrl.get_company_details();
                if (dtp.Rows.Count > 0)
                {
                    string clinicn = "";
                    clinicn = dtp.Rows[0]["name"].ToString();
                    str_name = clinicn.Replace("¤", "'");
                    str_street_address = dtp.Rows[0]["street_address"].ToString();
                    str_locality = dtp.Rows[0]["locality"].ToString();
                    str_pincode = dtp.Rows[0]["pincode"].ToString();
                    str_contact_no = dtp.Rows[0]["contact_no"].ToString();
                    str_email = dtp.Rows[0]["email"].ToString();
                    str_website = dtp.Rows[0]["website"].ToString();
                    Logopath = dtp.Rows[0]["path"].ToString();
                }
                System.Data.DataTable Payments = this.cntrl.payment_details_re(Convert.ToDateTime(payment_date).ToString("yyyy-MM-dd"), patient_id, invno, proname);
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                StreamWriter swriter = new StreamWriter(Apppath + "\\Receiptrefund.html");
                swriter.WriteLine("<Html>");
                swriter.WriteLine("<head>");
                swriter.WriteLine("<style>");
                swriter.WriteLine("table { border-collapse: collapse;}");
                swriter.WriteLine("p.big {line-height: 400%;}");
                swriter.WriteLine("</style>");
                swriter.WriteLine("</head>");
                swriter.WriteLine("<body>");
                if (File.Exists(Apppath + "\\" + Logopath))
                {
                    swriter.WriteLine("<table align='center' width=500px;border:1px;border-collapse:collapse;'>");
                    swriter.WriteLine("<tr>");
                    swriter.WriteLine("<td width=100px; height=85px; rowSpan=4><img src='" + Apppath + "\\" + Logopath + "' width='77' height='78' style='width:100px; hwight:100px;' ></td>");
                    swriter.WriteLine("<td align='left' width='488' height='25px';><FONT COLOR='black' FACE='segoe UI' SIZE=4><b>" + str_name + "</b></FONT></td> ");
                    swriter.WriteLine("<tr><td align='left' ><FONT COLOR='black' FACE='segoe UI' SIZE=2>" + str_street_address + "</FONT></td></tr>");
                    swriter.WriteLine("<tr><td align='left' ><FONT COLOR='black' FACE='segoe UI' SIZE=2>" + str_email + "</FONT></td>");
                    swriter.WriteLine("<tr><td align='left' ><FONT COLOR='black' FACE='segoe UI' SIZE=2>" + str_contact_no + "</FONT></td>");
                    swriter.WriteLine("<tr><td colspan='2'; align='left'><hr></td></tr>");
                    swriter.WriteLine("</table>");
                }
                else
                {
                    swriter.WriteLine("<table align='center' style='width:500px;border:1px;border-collapse:collapse;'>");
                    swriter.WriteLine("<tr>");
                    swriter.WriteLine("<td align='left'> <FONT COLOR='black' FACE='segoe UI' SIZE=4><b>" + str_name + "</b></FONT></td>");
                    swriter.WriteLine("</tr>");
                    swriter.WriteLine("<tr>");
                    swriter.WriteLine("<td align='left'> <FONT COLOR='black' FACE='segoe UI' SIZE=2>" + str_street_address + "</FONT></td>");
                    swriter.WriteLine("</tr>");
                    swriter.WriteLine("<tr>");
                    swriter.WriteLine("<td align='left'> <FONT COLOR='black' FACE='segoe UI' SIZE=2>" + str_locality + "</FONT></td>");
                    swriter.WriteLine("</tr>");
                    swriter.WriteLine("<tr>");
                    swriter.WriteLine("<td align='left'> <FONT COLOR='black' FACE='segoe UI' SIZE=2>" + str_email + "</FONT></td>");
                    swriter.WriteLine("</tr>");
                    swriter.WriteLine("<tr>");
                    swriter.WriteLine("<td align='left'> <FONT COLOR='black' FACE='segoe UI' SIZE=2>" + str_contact_no + "</FONT></td>");
                    swriter.WriteLine("</tr>");
                    swriter.WriteLine("<tr><td colspan='2'; align='left'><hr></td></tr>");
                    swriter.WriteLine("</table>");
                }
                swriter.WriteLine("<table align='center' width='500'>");
                swriter.WriteLine("<tr>");
                swriter.WriteLine("<td colspan=2> <center> <FONT Color=black face='Segoe UI' SIZE=3> <b>RECEIPT REFUND VOUCHER</b></FONT></center></td>");
                swriter.WriteLine("</tr>");
                swriter.WriteLine("</table>");//
                swriter.WriteLine("<br>");
                swriter.WriteLine("<table align='center' width='500'>");
                swriter.WriteLine("<tr>");
                swriter.WriteLine("<td align='left'><FONT COLOR ='black' FACE='segoe UI' SIZE=2>Patient Name : <b>" + PatName + "</b></FONT></td>");
                swriter.WriteLine("</tr>");
                swriter.WriteLine("<tr>");
                swriter.WriteLine("<td align='left'><FONT COLOR='black' FACE='segoe UI' SIZE=2>Patient Id : <b>" + patiID + "</b></FONT></td>");
                swriter.WriteLine("</tr>");
                swriter.WriteLine("<tr>");
                swriter.WriteLine("<td align='left'><FONT COLOR='black' FACE='segoe UI' SIZE=2>Address : <b>" + address + "</b></FONT></td>");
                swriter.WriteLine("<td align='right'><FONT COLOR ='black' FACE='segoe UI' SIZE=2>Date: " + DateTime.Now.Date.ToShortDateString() + "</FONT></td>");
                swriter.WriteLine("</tr >");
                swriter.WriteLine("<tr>");
                swriter.WriteLine("<td align='left'><FONT COLOR='black' FACE='segoe UI' SIZE=2>Receipt No : <b>" + Payments.Rows[0]["receipt_no"].ToString() + "</b></FONT></td>");
                swriter.WriteLine("<td align='right'><FONT COLOR ='black' FACE='segoe UI' SIZE=2>Invoice No: " + Payments.Rows[0]["invoice_no"].ToString() + "</FONT></td>");
                swriter.WriteLine("</tr >");
                swriter.WriteLine("<tr><td colspan='2'; align='left'><hr></td></tr>");//
                swriter.WriteLine("</table>");
                swriter.WriteLine("<table align='center' border='1' width='500' >");
                swriter.WriteLine("<tr>");
                swriter.WriteLine("<th width='44' ><FONT COLOR ='black' FACE='segoe UI' SIZE=3>SlNO</FONT></th>");
                swriter.WriteLine("<th width='329'><FONT COLOR ='black' FACE='segoe UI' SIZE=3>Details</FONT></th>");
                swriter.WriteLine("<th width='105'><FONT COLOR ='black' FACE='segoe UI' SIZE=3>Amount</FONT></th>");
                swriter.WriteLine("</tr>");
                swriter.WriteLine("<tr>");
                swriter.WriteLine("<td height='200' valign='top'Halign=><FONT COLOR ='black' FACE='segoe UI' SIZE=2>  <span style='float:left'>" + 1 + "</span></FONT></td>");
                swriter.WriteLine("<td height='200'valign='top' Halign='center'><FONT COLOR ='black' FACE='segoe UI' SIZE=2> <span style='float:left'> " + proname + "</span></FONT></td>");
                swriter.WriteLine("<td height='200'valign='top' ><FONT COLOR ='black' FACE='segoe UI' SIZE=2> <span style='float:right'>INR." + Convert.ToDecimal(Payments.Rows[0]["refund_amount"].ToString()).ToString("#0.00") + "</span></FONT></td>");
                swriter.WriteLine("</tr>");
                swriter.WriteLine("<tr>");
                swriter.WriteLine("<td colspan=2 align='right'><FONT COLOR ='black' FACE='segoe UI' SIZE=2>Total:</FONT></td>");
                swriter.WriteLine("<td colspan=1 align='right'><FONT COLOR ='black' FACE='segoe UI' SIZE=2><b>" + Convert.ToDecimal(Payments.Rows[0]["refund_amount"].ToString()).ToString("#0.00") + "</b></FONT></td>");
                swriter.WriteLine("</tr>");
                swriter.WriteLine("<tr><td colspan='3'  align='right'><FONT COLOR ='black' FACE='segoe UI' SIZE=2>(" + NumWordsWrapper(double.Parse(Payments.Rows[0]["refund_amount"].ToString().ToString())) + ")</FONT></td></tr>");
                swriter.WriteLine("</table>");
                swriter.WriteLine("<br>");
                swriter.WriteLine("<table align='center'  style='width:500px; border:1px;border-collaspe:collapse;'>");
                swriter.WriteLine("<tr>");
                swriter.WriteLine("<td colspan='2'  align='right'><FONT COLOR ='black' FACE='segoe UI' SIZE=2><b>Signature</b></FONT></td>");
                swriter.WriteLine("</tr>");
                swriter.WriteLine("</table>");
                swriter.WriteLine("</body>");
                swriter.WriteLine("</Html>");
                swriter.Close();
                System.Diagnostics.Process.Start(Apppath + "\\Receiptrefund.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Some error occured!..please try again later..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void print_receipt_document_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            try
            {
                string Logopath = "";
                string str_name = "";
                string str_street_address = "";
                string str_locality = "";
                string str_pincode = "";
                string str_contact_no = "";
                string str_email = "";
                string str_website = "";
                string patiID = "";
                string PatName = "";
                string address = "", aadhar_id=""; string strage = "", strgener = "";
                DataTable dt_patent = new DataTable();
                dt_patent = this.cntrl.get_patientDetails(patient_id);//aadhar_id
                if (dt_patent.Rows.Count > 0)
                {
                  
                    patiID = dt_patent.Rows[0]["pt_id"].ToString();
                    if (dt_patent.Rows[0]["age"].ToString() != "")
                    {
                        //if (dt_patent.Rows[0]["gender"].ToString() != "")
                        //{
                        strage = dt_patent.Rows[0]["age"].ToString();
                        //}
                        //else
                        //{ strage = dt_patent.Rows[0]["age"].ToString() + ")"; }
                    }
                    if (dt_patent.Rows[0]["gender"].ToString() != "")
                    {
                        //if (strage != "")
                        //{
                        //    strgener = "/" + dt_patent.Rows[0]["gender"].ToString() + ")";
                        //}
                        //else
                        //{
                        //strgener = " " + dt_patent.Rows[0]["gender"].ToString() + ")";
                        strgener =  dt_patent.Rows[0]["gender"].ToString();
                        //}
                    }
                    PatName = dt_patent.Rows[0]["pt_name"].ToString() ;
                    address = dt_patent.Rows[0]["primary_mobile_number"].ToString();
                    aadhar_id = dt_patent.Rows[0]["aadhar_id"].ToString();
                }
                System.Data.DataTable dtp = this.cntrl.get_company_details();
                if (dtp.Rows.Count > 0)
                {
                    string clinicn = "";
                    clinicn = dtp.Rows[0]["name"].ToString();
                    str_name = clinicn.Replace("¤", "'");
                    str_street_address = dtp.Rows[0]["street_address"].ToString();
                    str_locality = dtp.Rows[0]["locality"].ToString();
                    str_pincode = dtp.Rows[0]["pincode"].ToString();
                    str_contact_no = dtp.Rows[0]["contact_no"].ToString();
                    str_email = dtp.Rows[0]["email"].ToString();
                    str_website = dtp.Rows[0]["website"].ToString();
                    Logopath = dtp.Rows[0]["path"].ToString();
                }

                float x =31;
                float y = 250;
                float width = 270.0F; // max width I found through trial and error  270.0F
                float height = 0F;

                Font drawFontArial12Bold = new Font("Arial", 12, FontStyle.Bold);
                Font drawFontArial10Regular = new Font("Arial", 10, FontStyle.Regular);
                Font drawFontArial10Bold = new Font("Arial", 12, FontStyle.Bold);
                Font drawFontArial8Regular = new Font("Arial", 8, FontStyle.Regular);
                Font drawFontArial10Regular_underline = new Font("Arial", 10, FontStyle.Underline);
                SolidBrush drawBrush = new SolidBrush(Color.Black);

                // Set format of string.
                StringFormat drawFormatCenter = new StringFormat();
                drawFormatCenter.Alignment = StringAlignment.Center;
                StringFormat drawFormatLeft = new StringFormat();
                drawFormatLeft.Alignment = StringAlignment.Near;
                StringFormat drawFormatRight = new StringFormat();
                drawFormatRight.Alignment = StringAlignment.Far;

                // Draw string to screen.

             string   strsql = "select * from tbl_payment where payment_date='" + payment_date + "' and pt_id='" + patient_id + "' and receipt_no='" + receipt + "'";
         
                System.Data.DataTable dt_cf = db.table(strsql);


                var dateTimeNow = DateTime.Now;
            var tdate = dateTimeNow.ToShortDateString();
            if (dt_cf.Rows.Count > 0)
            {
                if (dt_cf.Rows[0]["payment_date"].ToString() != null)
                { tdate =Convert.ToDateTime( dt_cf.Rows[0]["payment_date"].ToString()).ToString("dd-MM-yyyy"); }
                else
                    tdate = null; }
                ///}
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                if (File.Exists(Apppath + "\\" + Logopath))
                {
                    Image newImage = Image.FromFile(Apppath + "\\" + Logopath);
                    // Create coordinates for upper-left corner of image.
                    float x11 = 75.0F;//100.0F;
                    float y11 = 75.0F;

                    // Create rectangle for source image.
                    RectangleF srcRect = new RectangleF(50.0F, 50.0F, 150, 150);//(50.0F, 50.0F, 150.0F, 150.0F);
                    GraphicsUnit units = GraphicsUnit.Pixel;

                    // Draw image to screen.
                    e.Graphics.DrawImage(newImage, x11, y11, srcRect, units);
                }
                //PdfPTable table = new PdfPTable(9);
                //table.TotalWidth = 595;
                string text = str_name;
                e.Graphics.DrawString(text, drawFontArial12Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial12Bold).Height;

                text = str_street_address;
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = str_locality;
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;
                text = str_pincode;
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                y = y + 17;
                text = "RECEIPT";
                e.Graphics.DrawString(text, drawFontArial10Regular_underline, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial12Bold).Height;

                //y = y + 18;
                text = "Date:" + " " + tdate;// +"   "+ "AdharID:"+" " + aadhar_id;
                e.Graphics.DrawString(text, new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point (31, 373), drawFormatLeft);
                //y += e.Graphics.MeasureString(text, drawFontArial8Regular).Height;
                e.Graphics.DrawString("AdharID: " + aadhar_id, new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(140, 373));

                //y = y + 5;
                //text = "Adhar ID :" + aadhar_id;
                //e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                ////y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;
                text = "Name:"+"" + PatName ;
                e.Graphics.DrawString(text, new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(31, 391), drawFormatLeft);

                //y += e.Graphics.MeasureString(text, drawFontArial8Regular).Height;
                //y = y + 5;
                //text = "Age:" + strage;
                //e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                //y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;
                text = "Mobile: "+ address;// + "  " + "Sex:"+" " + strgener + " " + "Age:"+" " + strage;
                e.Graphics.DrawString(text, new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(31, 412), drawFormatLeft);
                //y += e.Graphics.MeasureString(text, drawFontArial8Regular).Height;
                e.Graphics.DrawString("Sex: "+ strgener, new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(153, 412), drawFormatLeft);
                e.Graphics.DrawString("Age:" + " " + strage, new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(234, 412), drawFormatLeft);



                //text = "Sex :" + strgener;
                //e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                //y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;
                strsql = "select receipt_no,amount_paid,invoice_no,procedure_name,payment_date,mode_of_payment from tbl_payment where payment_date='" + payment_date + "' and pt_id='" + patient_id + "' and receipt_no='" + receipt + "' order by payment_date";
           
            System.Data.DataTable dt_payment = db.table(strsql);
                decimal total = 0;
                string mode = "";
                ///////////////////table
                ///
                //DataTable dt_pay = new DataTable();
                //dt_pay.Columns.Clear();
                //dt_pay.Columns.Add("RECEIPT NO");
                //dt_pay.Columns.Add("AMOUNT");
                //dt_pay.Columns.Add("INV NO");
                //DataRow row;
                //foreach (DataRow r in dt_payment.Rows)
                //{
                //    //Add new row and assign values to columns, no need to add columns again and again in loop which will throw exception
                //    row = dt_pay.NewRow();

                //    //Map all the values in the columns
                //    row["RECEIPT NO"] = r["receipt_no"].ToString();
                //    row["AMOUNT"] = r["amount_paid"].ToString();
                //    row["INV NO"] = r["invoice_no"].ToString();
                //    //At the end just add that row in datatable
                //    dt_pay.Rows.Add(row);
                //    total = total + decimal.Parse(r["amount_paid"].ToString());
                //    mode = r["mode_of_payment"].ToString();
                //}

                //Bitmap bm = new Bitmap(this.dt_pay.Width, this.dt_pay.Height);
                //dt_pay.DrawToBitmap(bm, new Rectangle(0, 0, this.dt_pay.Width, this.dt_pay.Height));
                //e.Graphics.DrawImage(bm, 0, 0);
                //int columnCount = 3;
                //int maxRows = dt_payment.Rows.Count;
                //using (Graphics g = e.Graphics)
                //{
                //    Brush brush = new SolidBrush(Color.Black);
                //    Pen pen = new Pen(brush);
                //    Font font = new Font("Arial", 10);
                //    SizeF size;
                //    int x1 = 0, y1 = 0, width1 = 130;
                //    float xPadding;

                //    foreach (DataColumn column in dt_pay.Columns)
                //    {
                //        size = g.MeasureString(column.ColumnName, font);
                //        xPadding = (width1 - size.Width) / 2;
                //        g.DrawString(column.ColumnName, font, brush, x1 + xPadding, y1 + 5);
                //        x1 += width1;
                //    }
                //    x = 0;
                //    y += 30;
                //    int rowcount = 0;
                //    foreach (DataRow row1 in dt_pay.Rows)
                //    {
                //        rowcount++;

                //        for (int i = 0; i < columnCount; i++)
                //        {
                //            size = g.MeasureString(row1[i].ToString(), font);
                //            xPadding = (width - size.Width) / 2;

                //            g.DrawString(row1[i].ToString(), font, brush, x + xPadding, y + 5);
                //            x += width;
                //        }

                //        e.HasMorePages = rowcount - 1 < maxRows;

                //        x = 0;
                //        y += 30;
                //    }


                //}












                //y = y + 10;

                //    text = "***************************************************";
                //    e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new Point(31, 438), drawFormatLeft);
                //    y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;
                //    text = "RECEIPT NO";//       AMOUNT      INV NO";
                //    e.Graphics.DrawString(text, new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(31, 450), drawFormatLeft);
                //    e.Graphics.DrawString("AMOUNT", new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(135,450));
                //    e.Graphics.DrawString("INV NO", new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(216, 450));

                //    //y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;
                //    text = "***************************************************";
                //    e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new Point(31, 465), drawFormatLeft);
                //    ////y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;
                //    //y = y + 10;
                //   int  yy = 480;
                //    for (int i = 0; i < dt_payment.Rows.Count; i++)
                //    {
                //        //text = dt_payment.Rows[i]["receipt_no"].ToString() + "      " + String.Format("{0:C2}", decimal.Parse(dt_payment.Rows[i]["amount_paid"].ToString())) + "      " + dt_payment.Rows[i]["invoice_no"].ToString();
                //        //e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                //        e.Graphics.DrawString(dt_payment.Rows[i]["receipt_no"].ToString(), new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(31, yy));
                //        e.Graphics.DrawString(String.Format("{0:C2}", decimal.Parse(dt_payment.Rows[i]["amount_paid"].ToString())), new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(135, yy));
                //        e.Graphics.DrawString(dt_payment.Rows[i]["invoice_no"].ToString(), new Font("Arial", 9, FontStyle.Regular), drawBrush, new Point(216, yy));
                //        yy = yy +20;
                //        total = total + decimal.Parse(dt_payment.Rows[i]["amount_paid"].ToString());
                //        mode = dt_payment.Rows[i]["mode_of_payment"].ToString();
                //    }
                //    yy = yy +10;
                //    text = "***************************************************";
                //    e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new Point(31, yy), drawFormatLeft);
                //    //y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;
                //    //y = y + 10;
                //    e.Graphics.DrawString("PAYMENT MODE "+mode, new Font("Arial",8, FontStyle.Regular), drawBrush, new Point(129, yy+19));
                //    yy = yy + 39;
                //    text = "GRAND TOTAL:  "  + String.Format("{0:C2}", decimal.Parse(total.ToString())) ;
                //    e.Graphics.DrawString(text, new Font("Arial", 10, FontStyle.Bold), drawBrush, new Point(129, yy));
                //    //y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;
                //    //y = y + 20;
                //    yy = yy + 25;
                //    text = "THANK YOU";
                //    e.Graphics.DrawString(text, new Font("Arial", 8, FontStyle.Regular), drawBrush, new Point(119, yy));
                //    //y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;



            }
            catch (Exception ex)
            {

            }
        }
        public int rowcount = 0;
        private void label11_Click(object sender, EventArgs e)
        {
            int count = rowcount + 15;
            DataTable dt_invoice_main = this.cntrl.get_paymentDate_count(patient_id, count);
            load_grid_showmore(dt_invoice_main);// Load_MainTable(dt_invoice_main);
            rowcount = count;

            //rowcount = Dgv_payment.Rows.Count;
            //int count = rowcount + 20;
            //System.Data.DataTable Payment = this.cntrl.get_paymentDate_count(patient_id,count);
            //load_grid(Payment);
        }

        static String NumWordsWrapper(double n)
        {
            string words = "";
            double intPart;
            double decPart = 0;
            if (n == 0)
                return "zero";
            try
            {
                string[] splitter = n.ToString().Split('.');
                intPart = double.Parse(splitter[0]);
                decPart = double.Parse(splitter[1]);
            }
            catch
            {
                intPart = n;
            }
            words = NumWords(intPart);
            if (decPart > 0)
            {
                if (words != "")
                    words += " and ";
                int counter = decPart.ToString().Length;
                switch (counter)
                {
                    case 1: words += NumWords(decPart) + " tenths"; break;
                    case 2: words += NumWords(decPart) + " hundredths"; break;
                    case 3: words += NumWords(decPart) + " thousandths"; break;
                    case 4: words += NumWords(decPart) + " ten-thousandths"; break;
                    case 5: words += NumWords(decPart) + " hundred-thousandths"; break;
                    case 6: words += NumWords(decPart) + " millionths"; break;
                    case 7: words += NumWords(decPart) + " ten-millionths"; break;
                }
            }
            return words;
        }
        static String NumWords(double n)
        {
            string[] numbersArr = new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            string[] tensArr = new string[] { "twenty", "thirty", "fourty", "fifty", "sixty", "seventy", "eighty", "ninty" };
            string[] suffixesArr = new string[] { "lakhs", "Crore", "billion", "trillion", "quadrillion", "quintillion", "sextillion", "septillion", "octillion", "nonillion", "decillion", "undecillion", "duodecillion", "tredecillion", "Quattuordecillion", "Quindecillion", "Sexdecillion", "Septdecillion", "Octodecillion", "Novemdecillion", "Vigintillion" };
            string words = "";
            bool tens = false;
            if (n < 0)
            {
                words += "negative ";
                n *= -1;
            }
            int power = (suffixesArr.Length + 1) * 3;
            while (power > 3)
            {
                double pow = Math.Pow(10, power);
                if (n >= pow)
                {
                    if (n % pow > 0)
                    {
                        words += NumWords(Math.Floor(n / pow)) + " " + suffixesArr[(power / 3) - 1] + ", ";
                    }
                    else if (n % pow == 0)
                    {
                        words += NumWords(Math.Floor(n / pow)) + " " + suffixesArr[(power / 3) - 1];
                    }
                    n %= pow;
                }
                power -= 3;
            }
            if (n >= 1000)
            {
                if (n % 1000 > 0) words += NumWords(Math.Floor(n / 1000)) + " thousand, ";
                else words += NumWords(Math.Floor(n / 1000)) + " thousand";
                n %= 1000;
            }
            if (0 <= n && n <= 999)
            {
                if ((int)n / 100 > 0)
                {
                    words += NumWords(Math.Floor(n / 100)) + " hundred";
                    n %= 100;
                }
                if ((int)n / 10 > 1)
                {
                    if (words != "")
                        words += " ";
                    words += tensArr[(int)n / 10 - 2];
                    tens = true;
                    n %= 10;
                }
                if (n < 20 && n > 0)
                {
                    if (words != "" && tens == false)
                        words += " ";
                    words += (tens ? "-" + numbersArr[(int)n - 1] : numbersArr[(int)n - 1]);
                    n -= Math.Floor(n);
                }
            }
            return words;
        }
    }
}
