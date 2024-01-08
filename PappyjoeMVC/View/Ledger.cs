using PappyjoeMVC.Controller;
using PappyjoeMVC.Model;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Windows.Forms;
namespace PappyjoeMVC.View
{
    public partial class Ledger : Form
    {
        public Ledger()
        {
            InitializeComponent();
        }
        public string patient_id = "0";
        public string doctor_id = "0";
        string logo_name = "";
        string path = "";
        public decimal balance;
        public decimal a;
        public decimal b;
        public decimal credit;
        string totalamt;
        Ledger_controller cntrl = new Ledger_controller();
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
        Nurses_Notes_controller nurse_cntrl = new Nurses_Notes_controller();
        Connection db = new Connection();
        private void Ledger_Load(object sender, EventArgs e)
        {
            string docnam = this.cntrl.Get_DoctorName(doctor_id);
            DataTable clinicname = this.cntrl.Get_CompanyNAme();
            if (clinicname.Rows.Count > 0)
            {
                string clinicn = "";
                clinicn = clinicname.Rows[0][0].ToString();
                path = clinicname.Rows[0]["path"].ToString();
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
            DataTable dtadvance = this.cntrl.Get_Advance(patient_id);
            if (dtadvance.Rows.Count > 0)
            {
                label31.Text = "Available advance: " + string.Format("{0:C2}", decimal.Parse(dtadvance.Rows[0][0].ToString()));
            }
            else
            {
                label31.Text = "Available advance: " + string.Format("{0:C2}", 0);
            }
             System.Data.DataTable pat = this.cntrl.Get_pat_iDName(patient_id);
            linkLabel_id.Text = pat.Rows[0]["pt_id"].ToString();
            linkLabel_Name.Text = pat.Rows[0]["pt_name"].ToString();
            DataTable dt_invoice = this.cntrl.LedgerInvoice(patient_id);
            DGV_Transaction.Size = new System.Drawing.Size(1038, 500);
            DGV_Transaction.Columns.Add("abc", "DATE"); 
            DGV_Transaction.Columns["abc"].Width = 170;
            DGV_Transaction.Columns.Add("ac", "INVOICE NO");
            DGV_Transaction.Columns["ac"].Width = 170;
            this.DGV_Transaction.Columns["ac"].SortMode =
    DataGridViewColumnSortMode.NotSortable;
            DGV_Transaction.Columns.Add("dds", "DETAILS");
            DGV_Transaction.Columns["dds"].Width = 170;
            this.DGV_Transaction.Columns["dds"].SortMode =
    DataGridViewColumnSortMode.NotSortable;
            DGV_Transaction.Columns.Add("sdfd", "CREDIT");
            DGV_Transaction.Columns["sdfd"].Width = 100;
            this.DGV_Transaction.Columns["sdfd"].SortMode =
    DataGridViewColumnSortMode.NotSortable;
            DGV_Transaction.Columns["sdfd"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGV_Transaction.Columns["sdfd"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGV_Transaction.Columns.Add("sde", "DEBIT");
            DGV_Transaction.Columns["sde"].Width = 100;
            this.DGV_Transaction.Columns["sde"].SortMode =
    DataGridViewColumnSortMode.NotSortable;
            DGV_Transaction.Columns["sde"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGV_Transaction.Columns["sde"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGV_Transaction.Columns.Add("q", "REFUND AMOUNT");
            DGV_Transaction.Columns["q"].Width = 100;
            this.DGV_Transaction.Columns["q"].SortMode =
    DataGridViewColumnSortMode.NotSortable;
            DGV_Transaction.Columns["q"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGV_Transaction.Columns["q"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGV_Transaction.Columns["q"].Visible = true;// false;
            DGV_Transaction.Columns.Add("p", "  BALANCE");
            DGV_Transaction.Columns["p"].Width = 100;
            this.DGV_Transaction.Columns["p"].SortMode =
    DataGridViewColumnSortMode.NotSortable;
            DGV_Transaction.Columns["p"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGV_Transaction.Columns["p"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGV_Transaction.Columns["p"].Visible = true;// false;now
            DGV_Transaction.Columns.Add("q1", "BALANCE");
            DGV_Transaction.Columns["q1"].Width = 150;
            this.DGV_Transaction.Columns["q1"].SortMode =
    DataGridViewColumnSortMode.NotSortable;
            DGV_Transaction.Columns["q1"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGV_Transaction.Columns["q1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGV_Transaction.Columns["q1"].Visible = false;
            DGV_Transaction.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 10, FontStyle.Bold);
            DGV_Transaction.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            DGV_Transaction.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DGV_Transaction.EnableHeadersVisualStyles = false;

            decimal totalcredit = 0; decimal tot_bal = 0;
            if (dt_invoice.Rows.Count > 0)
            {
                for (int z = 0; z < dt_invoice.Rows.Count; z++)
                {
                    string date = dt_invoice.Rows[z]["date"].ToString();
                    DateTime d = Convert.ToDateTime(date);
                    string day = d.Day.ToString();
                    string year = d.Year.ToString();
                    string strMonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(d.Month);
                    string sr = d.Month.ToString();
                    string invoiceno = dt_invoice.Rows[z]["invoice_no"].ToString();
                    string details = dt_invoice.Rows[z]["services"].ToString();
                    decimal cost = decimal.Parse(dt_invoice.Rows[z]["cost"].ToString());
                    decimal unit = decimal.Parse(dt_invoice.Rows[z]["unit"].ToString());
                    decimal discount = decimal.Parse(dt_invoice.Rows[z]["discountin_rs"].ToString());
                    decimal tax = decimal.Parse(dt_invoice.Rows[z]["tax_inrs"].ToString());
                    //credit = (cost * unit) - (tax + discount);
                    credit =((cost * unit) + tax ) - discount;
                    totalcredit = totalcredit + credit;
                    DGV_Transaction.Rows.Add(day + ' ' + strMonthName + ' ' + year, invoiceno, details, String.Format("{0:C2}", credit), String.Format("{0:C2}", 0),"0", String.Format("{0:C2}", credit), String.Format("{0:C2}", credit));
                    DGV_Transaction.Rows[z].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                    DGV_Transaction.Rows[z].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                    DGV_Transaction.Rows[z].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                    DGV_Transaction.Rows[z].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                    DGV_Transaction.Rows[z].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                    DGV_Transaction.Rows[z].Cells[5].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                    DGV_Transaction.Rows[z].Cells[0].Style.ForeColor = Color.Green;
                }
            }
            DataTable dtb = this.cntrl.LedgerPay(patient_id);
            if (dtb.Rows.Count > 0)
            {
                
                for (int u = 0; u < dtb.Rows.Count; u++)
                {
                    string recpno = dtb.Rows[u]["receipt_no"].ToString();
                    string date = dtb.Rows[u]["payment_date"].ToString();
                    DateTime d = Convert.ToDateTime(date);
                    string day = d.Day.ToString();
                    string month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(d.Month);
                    string year = d.Year.ToString();
                    string invoiceno = dtb.Rows[u]["invoice_no"].ToString();
                    string[] invoice1 = new string[100];
                    invoice1 = invoiceno.Split(',');
                    decimal total = 0,refund_amount=0; decimal balance1 = 0; 
                    string invAmount = dtb.Rows[u]["total"].ToString();// DGV_Transaction.Rows[DGV_Transaction.Rows.Count - 1].Cells[6].Value.ToString();//6
                    balance = decimal.Parse(invAmount, NumberStyles.Currency);
                    if (dtb.Rows[u]["refund_status"].ToString()=="Refund")
                    {
                        refund_amount = Convert.ToDecimal(dtb.Rows[u]["refund_amount"].ToString());
                           total = Convert.ToDecimal(dtb.Rows[u]["amount_paid"].ToString()) - Convert.ToDecimal(dtb.Rows[u]["refund_amount"].ToString());
                        balance1 = balance - refund_amount;
                    }
                    else
                    {
                        total = Convert.ToDecimal(dtb.Rows[u]["amount_paid"].ToString());
                        balance1 = balance - total;
                    }
                    tot_bal = tot_bal + balance1;
                    DGV_Transaction.Rows.Add(day + ' ' + month + ' ' + year, recpno, invoiceno, String.Format("{0:C2}", 0), String.Format("{0:C2}", total), refund_amount, balance1, balance1);//String.Format("{0:C2}", balance1)
                    //DGV_Transaction.Rows.Add(day + ' ' + strMonthName + ' ' + year, invoiceno, details, String.Format("{0:C2}", credit), String.Format("{0:C2}", 0), "0", String.Format("{0:C2}", totalcredit), "0");
                    int a = DGV_Transaction.Rows.Count;
                    //DGV_Transaction.Rows[dtb.Rows.Count + u].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                    //DGV_Transaction.Rows[dtb.Rows.Count + u].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                    //DGV_Transaction.Rows[dtb.Rows.Count + u].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                    //DGV_Transaction.Rows[dtb.Rows.Count + u].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                    //DGV_Transaction.Rows[dtb.Rows.Count + u].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                    //DGV_Transaction.Rows[dtb.Rows.Count + u].Cells[5].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                    //DGV_Transaction.Rows[dtb.Rows.Count + u].Cells[0].Style.ForeColor = Color.Blue;
                }
            }
            decimal credittot = 0,bal=0;
            decimal debittot = 0,refund_total=0;
            if (DGV_Transaction.Rows.Count > 0)
            {
                for (int j = 0; j < DGV_Transaction.Rows.Count; j++)
                {
                    decimal credit = decimal.Parse(DGV_Transaction.Rows[j].Cells[3].Value.ToString(), NumberStyles.Currency);
                    decimal debit = decimal.Parse(DGV_Transaction.Rows[j].Cells[4].Value.ToString(), NumberStyles.Currency);
                    refund_total= refund_total+ decimal.Parse(DGV_Transaction.Rows[j].Cells[5
                        ].Value.ToString(), NumberStyles.Currency);
                    credittot = credittot + credit;
                    debittot = debittot + debit;
                    string credittot1 = String.Format("{0:C2}", credittot);
                    Lab_Due.Text = credittot1.ToString();
                    string debittot1 = String.Format("{0:C2}", debittot);
                    Lab_DebitTotal.Text = debittot1.ToString();
                    decimal tot = credittot - debittot;
                    totalamt = tot.ToString();
                    string tot1 = String.Format("{0:C2}", tot);
                    Lab_BalanTotal.Text = tot1;
                    string rr = DGV_Transaction.Rows[j].Cells[1].Value.ToString();
                    lb_total_balance.Text = tot1;
                }
            }
            decimal c = a - b; //lb_total_balance.Text = tot1.ToString("0.00");//tot_bal
            DGV_Transaction.Rows.Add("", "", "TOTAL", Lab_Due.Text, Lab_DebitTotal.Text, refund_total, Lab_BalanTotal.Text, tot_bal);
            DGV_Transaction.Rows[DGV_Transaction.Rows.Count - 1].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            DGV_Transaction.Rows[DGV_Transaction.Rows.Count - 1].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            DGV_Transaction.Rows[DGV_Transaction.Rows.Count - 1].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            DGV_Transaction.Rows[DGV_Transaction.Rows.Count - 1].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            DGV_Transaction.Rows[DGV_Transaction.Rows.Count - 1].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            DGV_Transaction.Rows[DGV_Transaction.Rows.Count - 1].Cells[5].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            DGV_Transaction.Rows[DGV_Transaction.Rows.Count - 1].Cells[6].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            DGV_Transaction.Rows[DGV_Transaction.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(224, 224, 224);
            DGV_Transaction.Rows[DGV_Transaction.Rows.Count - 1].Cells[6].Style.ForeColor = Color.Red;
            DGV_Transaction.AllowUserToAddRows = false;
            DataTable All_advance = this.cntrl.getall_advance(patient_id);
            DataTable Adv_return = this.cntrl.Adv_return(patient_id);
            {
                if (All_advance.Rows.Count>0)
                {
                    DGV_Transaction.Rows.Add("DATE", "ADVANCE", "ADVANCE RETURN", "", "", "", "","");
                    DGV_Transaction.Rows[DGV_Transaction.Rows.Count - 1].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                    DGV_Transaction.Rows[DGV_Transaction.Rows.Count - 1].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                    DGV_Transaction.Rows[DGV_Transaction.Rows.Count - 1].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                    DGV_Transaction.Rows[DGV_Transaction.Rows.Count - 1].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                    DGV_Transaction.Rows[DGV_Transaction.Rows.Count - 1].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                    DGV_Transaction.Rows[DGV_Transaction.Rows.Count - 1].Cells[5].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                    DGV_Transaction.Rows[DGV_Transaction.Rows.Count - 1].Cells[6].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);

                    for (int i = 0; i < All_advance.Rows.Count; i++)
                    {
                        string date = All_advance.Rows[i]["Date"].ToString();
                        DateTime d = Convert.ToDateTime(date);
                        string day = d.Day.ToString();
                        string month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(d.Month);
                        string year = d.Year.ToString();
                        decimal advance= Convert.ToDecimal(All_advance.Rows[i]["Amount"].ToString());
                        DGV_Transaction.Rows.Add(day + ' ' + month + ' ' + year, String.Format("{0:C2}", advance), String.Format("{0:C2}", 0), "", "","", "");
                        DGV_Transaction.Rows[All_advance.Rows.Count + i].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                        DGV_Transaction.Rows[All_advance.Rows.Count + i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                        DGV_Transaction.Rows[All_advance.Rows.Count + i].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                        DGV_Transaction.Rows[All_advance.Rows.Count + i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                        DGV_Transaction.Rows[All_advance.Rows.Count + i].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                        DGV_Transaction.Rows[All_advance.Rows.Count + i].Cells[5].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);

                    }
                    for (int i = 0; i < Adv_return.Rows.Count; i++)
                    {
                        string date = Adv_return.Rows[i]["Date"].ToString();
                        DateTime d = Convert.ToDateTime(date);
                        string day = d.Day.ToString();
                        string month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(d.Month);
                        string year = d.Year.ToString();
                        decimal advance = Convert.ToDecimal(Adv_return.Rows[i]["Amount"].ToString());
                        DGV_Transaction.Rows.Add(day + ' ' + month + ' ' + year, String.Format("{0:C2}", 0), String.Format("{0:C2}", advance), "", "","", "");
                        DGV_Transaction.Rows[Adv_return.Rows.Count + i].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                        DGV_Transaction.Rows[Adv_return.Rows.Count + i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                        DGV_Transaction.Rows[Adv_return.Rows.Count + i].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                        DGV_Transaction.Rows[Adv_return.Rows.Count + i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                        DGV_Transaction.Rows[Adv_return.Rows.Count + i].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                        DGV_Transaction.Rows[Adv_return.Rows.Count + i].Cells[5].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                    }
                }
            }

            if (DGV_Transaction.Rows.Count < 2)
            {
                DGV_Transaction.CurrentCell.Selected = false;
                DGV_Transaction.Visible = false;
                btnprint.Hide();
                btnpayreminder.Hide();
            }
            else
            {
                btnprint.Show();
                DGV_Transaction.CurrentCell.Selected = false;
            }
            if (DGV_Transaction.Rows.Count <= 1)
            {
                int x = (panel9.Size.Width - Lab_Msg.Size.Width) / 2;
                Lab_Msg.Location = new Point(x, Lab_Msg.Location.Y);
                Lab_Msg.Show();
            }
            else
            {
                Lab_Msg.Hide();
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
        public void SetController(Ledger_controller controller)
        {
            cntrl = controller;
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
        }

        private void btnpayreminder_Click(object sender, EventArgs e)
        {
            try
            {
                string doctor_name = "";
                string doct = this.cntrl.Get_DoctorName(doctor_id);
                if (doct != "")
                {
                    doctor_name = doct;
                }
                System.Data.DataTable patient = this.cntrl.Get_Patient_Details(patient_id);
                string Pname = "", Gender = "", address = "", DOA = "", age = "", Mobile = "";
                if (patient.Rows.Count > 0)
                {
                    Pname = patient.Rows[0]["pt_name"].ToString();
                    Gender = patient.Rows[0]["gender"].ToString();
                    address = patient.Rows[0]["street_address"].ToString() + " , " + patient.Rows[0]["city"].ToString();
                    Mobile = patient.Rows[0]["primary_mobile_number"].ToString();
                    DOA = DateTime.Parse(patient.Rows[0]["date"].ToString()).ToString("dd/MM/yyyy");
                    if (patient.Rows[0]["date_of_birth"].ToString() != "")
                    {
                        age = DateTime.Parse(patient.Rows[0]["date_of_birth"].ToString()).ToString("dd/MM/yyyy");
                    }
                }
                string contact_no = "";
                string clinic_name = "";
                System.Data.DataTable dtp = this.cntrl.get_company_details();
                if (dtp.Rows.Count > 0)
                {
                    string clinicn = "";
                    clinicn = dtp.Rows[0]["name"].ToString();
                    clinic_name = clinicn.Replace("¤", "'");
                    contact_no = dtp.Rows[0]["contact_no"].ToString();
                }
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
                                string payment = "<html><body><p>Dear <b>" + Pname.ToString() + "</b>, your " + clinic_name.ToString() + " account summary is here.</p><h3><u>Account Details</u></h3><div style=border:1px solid #ededed;color:#999999;><p>" + Pname.ToString() + "</p><p>" + Gender.ToString() + "</p><p>" + address.ToString() + "</p><p>" + Mobile.ToString() + "</p></div><h3><u>Clinic Details</u></h3><div style=border:1px solid #ededed;color:#999999;><p>" + clinic_name.ToString() + "</p><p>" + doctor_name.ToString() + "</p><p>" + contact_no.ToString() + "</p><table style=color:#F0605D><tr><td width=200px><b>DUE AMOUNT :</b></td><td width=200px style=font-size:20px;><b> INR " + Lab_BalanTotal.Text + "/- </b><//td></tr></table></div><p>This letter is to formally notify you that to pay the amount due within 15 working days.If your payment has been already sent or remitted by credit card, please disregard this letter. </p><p> However,if you have not yet made payment, kindely do so immediately.Thank you for attending to this matter as soon as possible. </p><p><b>Regards,</b></p><p>" + clinic_name.ToString() + "</p></body></html>";
                                double j = Convert.ToDouble(totalamt);
                                if (j > 0)
                                {
                                    MailMessage message = new MailMessage();
                                    message.From = new MailAddress(email);
                                    message.To.Add(email);
                                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                                    message.Subject = "Ledger";
                                    message.Body = payment;
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
                                }
                                else
                                {
                                    MessageBox.Show("There is No Dues for this patient:" + Pname.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invali Email Id", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please add  Email Id To this patient", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                printhtml();
            }
            catch
            {
                MessageBox.Show("Report error..", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void printhtml()
        {
            try
            {
                string clinicn = "";
                string Clinic = "";
                string streetaddress = "";
                string str_locality = "";
                string contact_no = "";
                string str_pincode = "";
                string str_email = "";
                string str_website = "";
                string path = "";
                System.Data.DataTable dtp = db.table("select * from tbl_practice_details");
                if (dtp.Rows.Count > 0)
                {
                    clinicn = dtp.Rows[0]["name"].ToString();
                    Clinic = clinicn.Replace("¤", "'");
                    streetaddress = dtp.Rows[0]["street_address"].ToString();
                    contact_no = dtp.Rows[0]["contact_no"].ToString();
                    str_locality = dtp.Rows[0]["locality"].ToString();
                    str_pincode = dtp.Rows[0]["pincode"].ToString();
                    str_email = dtp.Rows[0]["email"].ToString();
                    str_website = dtp.Rows[0]["website"].ToString();
                    path= dtp.Rows[0]["path"].ToString();
                    logo_name = path;
                }
                string day = DateTime.Now.DayOfWeek.ToString();
                string date = DateTime.Now.Day.ToString();
                string month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
                string year = DateTime.Now.Year.ToString();
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                StreamWriter sWrite = new StreamWriter(Apppath + "\\ledger_.html");
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
                        sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td width='30px' height='50px' align='left' rowspan='3'><img src='" + Appath + "\\" + logo_name + "'style='width:70px;height:70px;' ></td>  ");
                        sWrite.WriteLine("<td width='870px' align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=4><b>&nbsp" + clinicn + "</font> <br><FONT  COLOR=black  face='Segoe UI' SIZE=2>&nbsp;" + streetaddress + "<br>&nbsp;" + contact_no + " </b></td></tr>");
                       
                        sWrite.WriteLine("</table>");
                    }
                    else
                    {
                        sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td  align='left' height='20px'><FONT  COLOR=black  face='Arial' SIZE=5>&nbsp;" + Clinic + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td  align='left' height='20px'><FONT COLOR=black FACE='Arial' SIZE=3>&nbsp;" + streetaddress + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td align='left' height='20px' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + str_locality + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td align='left' height='20px' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;Pincode :" + str_pincode + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td align='left' height='20px' valign='top'> <FONT COLOR=black FACE='Arial' SIZE=2>&nbsp;Phone No :" + contact_no + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td align='left' height='20px' valign='top'> <FONT COLOR=black FACE='Arial' SIZE=2>&nbsp;Email :" + str_email + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td align='left' height='20px' valign='top'> <FONT COLOR=black FACE='Arial' SIZE=2>&nbsp;Website :" + str_website + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                        sWrite.WriteLine("</table>");
                    }
                }
                else
                {
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td  align='left' height='20px'><FONT  COLOR=black  face='Arial' SIZE=5>&nbsp;" + Clinic + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td  align='left' height='20px'><FONT COLOR=black FACE='Arial' SIZE=3>&nbsp;" + streetaddress + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' height='20px' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + str_locality + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' height='20px' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;Pincode :" + str_pincode + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' height='20' valign='top'> <FONT COLOR=black FACE='Arial' SIZE=2>&nbsp;Phone No :" + contact_no + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' height='20px' valign='top'> <FONT COLOR=black FACE='Arial' SIZE=2>&nbsp;Email :" + str_email + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' height='20px' valign='top'> <FONT COLOR=black FACE='Arial' SIZE=2>&nbsp;Website :" + str_website + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                    sWrite.WriteLine("</table>");
                }
                int Dexist = 0;
                string sexage = "";
                string address = "";
                address = "";
                System.Data.DataTable dt1 = this.cntrl.Get_Patient_Details(patient_id);// db.table("select * from tbl_patient where id='" + patient_id + "'");
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
                    if (dt1.Rows[0]["aadhar_id"].ToString() != "")
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>" + "ID:" + dt1.Rows[0]["aadhar_id"].ToString() + " </font></td>");
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
                }
                sWrite.WriteLine("<br>");
                sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td><FONT COLOR=black FACE='Geneva, Arial' SIZE=6>Ledger</FONT></td>");
                sWrite.WriteLine("<td width=350px></td>");
                sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2> <FONT COLOR=black>Date : </FONT>" + DateTime.Now.ToString("dd MMM yyyy") + "</font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");
                sWrite.WriteLine("<tr style='background:#999999'>");
                sWrite.WriteLine("<td align='left' width='35px' height='30' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Arial' SIZE=3>Sl.</font></td>");
                sWrite.WriteLine("<td align='left' width='113px' height='30' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Arial' SIZE=3>&nbsp;Date</font></td>");
                sWrite.WriteLine("<td align='left' width='87px' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Arial' SIZE=3>&nbsp;Invoice No. </font></td>");
                sWrite.WriteLine("<td align='left' width='195px' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Arial' SIZE=3>&nbsp;Details </font></td>");
                sWrite.WriteLine("<td align='right' width='72px' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Arial' SIZE=3>&nbsp;Credit&nbsp;</font></td>");
                sWrite.WriteLine("<td align='right' width='75px' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Arial' SIZE=3>&nbsp;Debit&nbsp;</font></td>");
                sWrite.WriteLine("<td align='right' width='75px' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Arial' SIZE=3>&nbsp;Refund&nbsp;</font></td>");
                sWrite.WriteLine("<td align='right' width='91px' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Arial' SIZE=3>&nbsp;Balance&nbsp;</font></td>");
                sWrite.WriteLine("</tr>");
                for (int i = 0; i < DGV_Transaction.Rows.Count ; i++)
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' height='30'  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + (i + 1) + " </font></td>");
                    sWrite.WriteLine("<td align='left'   ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + DGV_Transaction.Rows[i].Cells[0].FormattedValue.ToString() + " </font></td>");
                    sWrite.WriteLine("<td align='left'   ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + DGV_Transaction.Rows[i].Cells[1].FormattedValue.ToString() + " </font></td>");
                    sWrite.WriteLine("<td align='left'   ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + DGV_Transaction.Rows[i].Cells[2].FormattedValue.ToString() + " </font></td>");
                    sWrite.WriteLine("<td align='right'   ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + DGV_Transaction.Rows[i].Cells[3].FormattedValue.ToString() + " </font></td>");
                    sWrite.WriteLine("<td align='right'   ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + DGV_Transaction.Rows[i].Cells[4].FormattedValue.ToString() + " </font></td>");
                    sWrite.WriteLine("<td align='right'   ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + DGV_Transaction.Rows[i].Cells[5].FormattedValue.ToString() + " </font></td>");
                    sWrite.WriteLine("<td align='right'   ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + DGV_Transaction.Rows[i].Cells[7].FormattedValue.ToString() + " </font></td>");
                    sWrite.WriteLine("</tr>");
                }
                sWrite.WriteLine("<tr><td align='left' colspan=8><hr/></td></tr>");
                sWrite.WriteLine("<tr >");
                sWrite.WriteLine("<td align='Right' colspan=6 width='80px'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>" + " TOTAL CREDIT :" + " </font></td>");
                sWrite.WriteLine("<td align='Right'   colspan=2><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>" + this.Lab_Due.Text + " </font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr >");
                sWrite.WriteLine("<td align='Right'    colspan=6 width='80px'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>" + "TOTAL DEBIT : " + " </font></td>");
                sWrite.WriteLine("<td align='Right'   colspan=2 width='40px'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>" + this.Lab_DebitTotal.Text + " </font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr >");
                sWrite.WriteLine("<b><td align='Right'   colspan=6 width='80px' style='font:Arial;font-size:10'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>" + "TOTAL BALANCE : " + " </font></td></b>");
                sWrite.WriteLine("<b><td align='Right'   colspan=2 width='40px'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>"+lb_total_balance.Text + "</font></td></b>");// 
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<script>window.print();</script>");
                sWrite.WriteLine("</body >");
                sWrite.WriteLine("</html>");
                sWrite.Close();
                System.Diagnostics.Process.Start(Apppath + "\\ledger_.html");
            }
            catch (Exception ex)
            {
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
        user_privillage_model privi_mdl = new user_privillage_model();
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
       

        private void DGV_Transaction_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }
}
