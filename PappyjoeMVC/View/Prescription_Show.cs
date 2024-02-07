using PappyjoeMVC.Controller;
using PappyjoeMVC.Model;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Net.Mail;
using System.Windows.Forms;
using PappyjoeMVC.Model;

namespace PappyjoeMVC.View
{
    public partial class Prescription_Show : Form
    {

        public Prescription_Show()
        {
            InitializeComponent();
        }
        public string doctor_id = "", staff_id = "", patient_id = "";
        string logo_name = "";
        string path = "",age="";
        string includeheader = "0";
        string includelogo = "0";
        string paperSize_print = "";
        string prescription_id = "0";
        int topmargin1 = 0;
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
        PaperSize paperSize; System.Drawing.Image logo = null;
        Connection db = new Connection();
        Prescription_Show_controller cntrl=new Prescription_Show_controller();
        Show_Appointment_controller Apptmt_cntrl = new Show_Appointment_controller();
        Vital_Signs_controller vital_cntrl = new Vital_Signs_controller();
        Clinical_Findings_controller clinical_cntrl = new Clinical_Findings_controller();
        Treatment_controller treatmnt_cntrl = new Treatment_controller();
        Finished_Procedre_controller fnsdtrt_cntrl = new Finished_Procedre_controller();
        LabWorks_controller lab_cntrl = new LabWorks_controller();
        Attachments_controller attach_cntrl = new Attachments_controller();
        Invoice_controller invo_cntrl = new Invoice_controller();
        Receipt_controller recpt_cntrl = new Receipt_controller();
        Booking_controller b_cntrl = new Booking_controller();
        Communication_Setting_controller ccntrl = new Communication_Setting_controller();
        Nurses_Notes_controller nurse_cntrl = new Nurses_Notes_controller();
        private void prescriptionShow_Load(object sender, EventArgs e)
        {
            try
            {
                if (doctor_id != "1")
                {
                    // add
                    string privid;
                    //privid = this.cntrl.add_privillege(doctor_id);                                                  
                    //if (int.Parse(privid) > 0)
                    //{
                    //    BtnAdd.Enabled = false;
                    //    pb_prescrptAdd.Enabled = false;
                    //}
                    //else
                    //{
                    //    BtnAdd.Enabled = true;
                    //    pb_prescrptAdd.Enabled = true;
                    //}
                    //edit
                    //privid = this.cntrl.edit_privillege(doctor_id);
                    //if (int.Parse(privid) > 0)
                    //{
                    //    editToolStripMenuItem1.Enabled = false;
                    //}
                    //else
                    //{                                           
                    //    editToolStripMenuItem1.Enabled = true;
                    //}
                    ////Delete
                    //privid = this.cntrl.delete_privillege(doctor_id);
                    //if (int.Parse(privid) > 0)
                    //{
                    //    deleteToolStripMenuItem1.Enabled = false;
                    //}
                    //else
                    //{
                    //    deleteToolStripMenuItem1.Enabled = true;
                    //}
                }
                //Privilege set ends
                dataGridView1.Size = new System.Drawing.Size(this.Width - 312, 617);
                System.Data.DataTable clinicname = this.cntrl.Get_CompanyNAme();
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
                            //logo_name = "";
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
                System.Data.DataTable pat = this.cntrl.Get_pat_iDName(patient_id);
                if (pat.Rows.Count != 0)
                {
                    linkLabel_id.Text = pat.Rows[0]["pt_id"].ToString();
                    linkLabel_Name.Text = pat.Rows[0]["pt_name"].ToString();
                }
                dataGridView1.ColumnCount = 6;
                dataGridView1.RowCount = 0;
                dataGridView1.ColumnHeadersVisible = false;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DataTable dt_pre_main = this.cntrl.Get_maindtails(patient_id); //Get_maindtails_count
                Load_MainGrid(dt_pre_main);
                if(dt_pre_main.Rows.Count>25)
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
                nurse_count();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            DataTable dt_pre_main = this.cntrl.Get_maindtails(patient_id);
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
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                string strfooter1 = "";
                string strfooter2 = "";
                string strfooter3 = "";
                string header1 = "";
                string header2 = "";
                string header3 = "";
                System.Data.DataTable print = this.cntrl.printsettings();
                if (print.Rows.Count > 0)
                {
                    header1 = print.Rows[0]["header"].ToString();
                    header2 = print.Rows[0]["left_text"].ToString();
                    header3 = print.Rows[0]["right_text"].ToString();
                    strfooter1 = print.Rows[0]["fullwidth_context"].ToString();
                    strfooter2 = print.Rows[0]["left_sign"].ToString();
                    strfooter3 = print.Rows[0]["right_sign"].ToString();
                }
                int xx = 30;
                int yy = 185;
                if (includeheader == "1")
                {
                    if (includelogo == "1")
                    {
                        string pathimage = db.server();
                        System.Data.DataTable dtp = this.cntrl.clinicpath();
                        System.Drawing.Image logo = null;
                        try
                        {
                            if (dtp.Rows.Count > 0)
                            {
                                string path = dtp.Rows[0]["path"].ToString();
                                logo = System.Drawing.Image.FromFile(pathimage + path);
                                e.Graphics.DrawImage(logo, 30, 60, 75, 75);
                                xx = 120;
                            }
                        }
                        catch { }
                    } //logo
                    using (System.Drawing.Font printFont = new System.Drawing.Font("Segoe UI", 16))
                    {
                        e.Graphics.DrawString(header1, printFont, Brushes.Black, xx, 70);
                    }
                    using (System.Drawing.Font printFont = new System.Drawing.Font("Segoe UI", 12))
                    {
                        e.Graphics.DrawString(header2, printFont, Brushes.Black, xx, 100);
                    }
                    using (System.Drawing.Font printFont = new System.Drawing.Font("Segoe UI", 10))
                    {
                        e.Graphics.DrawString(header3, printFont, Brushes.Black, xx, 120);
                    }
                }//header
                yy = 185;
                System.Data.DataTable dt1 = this.cntrl.patient_details(patient_id);
                if (dt1.Rows.Count > 0)
                {
                    Graphics g = e.Graphics;
                    Pen pen = new Pen(Color.Gray);
                    if (paperSize_print == "A5")
                    {
                        g.DrawLine(pen, new System.Drawing.Point(20, 175), new System.Drawing.Point(500, 175));
                    }
                    else if (paperSize_print == "A4")
                    {
                        g.DrawLine(pen, new System.Drawing.Point(20, 175), new System.Drawing.Point(800, 175));
                    }
                    else if (paperSize_print == "A3")
                    {
                        g.DrawLine(pen, new System.Drawing.Point(20, 175), new System.Drawing.Point(1150, 175));
                    }
                    string sexage = ""; int Dexist = 0; string address = "";
                    using (System.Drawing.Font printFont = new System.Drawing.Font("Segoe UI", 9))
                    {
                        string doctor_name = "";
                        using (System.Drawing.Font printFont1 = new System.Drawing.Font("Segoe UI", 10))
                        {
                            e.Graphics.DrawString(dt1.Rows[0]["pt_name"].ToString(), printFont1, Brushes.Black, 20, yy);
                            if (dt1.Rows[0]["gender"].ToString() != "")
                            {
                                sexage = dt1.Rows[0]["gender"].ToString();
                                Dexist = 1;
                            }
                            if (dt1.Rows[0]["age"].ToString() != "")
                            {
                                age = "/" + dt1.Rows[0]["age"].ToString() + dt1.Rows[0]["days"].ToString();
                            }
                            if (dt1.Rows[0]["age2"].ToString() != "")
                            {
                                if (!string.IsNullOrEmpty(age))
                                {
                                    age = age + " " + dt1.Rows[0]["age2"].ToString() + "Months";
                                }
                                else
                                {
                                    age = "/" + dt1.Rows[0]["age2"].ToString() + "Months";
                                }

                            }
                            e.Graphics.DrawString(age, printFont1, Brushes.Black, 300, yy);
                        }
                        yy = yy + 22;
                        e.Graphics.DrawString("Patient id:" + dt1.Rows[0]["pt_id"].ToString(), printFont, Brushes.Black, 20, yy);
                        Dexist = 0;
                        if (dt1.Rows[0]["street_address"].ToString() != "")
                        {
                            address = dt1.Rows[0]["street_address"].ToString();
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
                        e.Graphics.DrawString(address, printFont, Brushes.Black, 300, yy);
                        if (dt1.Rows[0]["primary_mobile_number"].ToString() != "")
                        {
                            yy = yy + 20;
                            e.Graphics.DrawString("Contact:" + dt1.Rows[0]["primary_mobile_number"].ToString(), printFont, Brushes.Black, 20, yy);
                        }
                        yy = yy + 20;
                        yy = yy + 20;
                        if (paperSize_print == "A5")
                        {
                            g.DrawLine(pen, new System.Drawing.Point(20, yy), new System.Drawing.Point(500, yy));
                        }
                        else if (paperSize_print == "A4")
                        {
                            g.DrawLine(pen, new System.Drawing.Point(20, yy), new System.Drawing.Point(800, yy));
                        }
                        else if (paperSize_print == "A3")
                        {
                            g.DrawLine(pen, new System.Drawing.Point(20, yy), new System.Drawing.Point(1150, yy));
                        }

                        yy = yy + 30;
                        Dexist = 0;
                        System.Data.DataTable dt_cf = this.cntrl.patient_prescptn(prescription_id, patient_id);
                        if (dt_cf.Rows.Count > 0)
                        {

                            e.Graphics.DrawString("By:", printFont, Brushes.Black, 20, yy);
                            using (System.Drawing.Font printFont1 = new System.Drawing.Font("Segoe UI", 10))
                            {
                                e.Graphics.DrawString("Dr." + Convert.ToString(dt_cf.Rows[0]["doctor_name"].ToString()), printFont1, Brushes.Black, 45, yy);
                                doctor_name = Convert.ToString(dt_cf.Rows[0]["doctor_name"].ToString());
                            }
                            yy = yy + 30;
                            if (paperSize_print == "A5")
                            {
                                e.Graphics.DrawString("Date: ", printFont, Brushes.Black, 400, yy);
                                using (System.Drawing.Font printFont1 = new System.Drawing.Font("Segoe UI", 10))
                                {
                                    DateTime tdate = Convert.ToDateTime(dt_cf.Rows[0]["date"].ToString());
                                    e.Graphics.DrawString(tdate.ToString("dd MMM yyyy"), printFont1, Brushes.Black, 430, yy);
                                }
                            }
                            else if (paperSize_print == "A4")
                            {
                                e.Graphics.DrawString("Date: ", printFont, Brushes.Black, 690, yy);
                                using (System.Drawing.Font printFont1 = new System.Drawing.Font("Segoe UI", 10))
                                {
                                    DateTime tdate = Convert.ToDateTime(dt_cf.Rows[0]["date"].ToString());
                                    e.Graphics.DrawString(tdate.ToString("dd MMM yyyy"), printFont1, Brushes.Black, 720, yy);
                                }
                            }
                            else if (paperSize_print == "A3")
                            {
                                e.Graphics.DrawString("Date: ", printFont, Brushes.Black, 1040, yy);
                                using (System.Drawing.Font printFont1 = new System.Drawing.Font("Segoe UI", 10))
                                {
                                    DateTime tdate = Convert.ToDateTime(dt_cf.Rows[0]["date"].ToString());
                                    e.Graphics.DrawString(tdate.ToString("dd MMM yyyy"), printFont1, Brushes.Black, 1070, yy);
                                }
                            }
                            using (System.Drawing.Font printFont1 = new System.Drawing.Font("Segoe UI", 14))
                            {
                                e.Graphics.DrawString("P", printFont1, Brushes.Black, 20, yy);
                                using (System.Drawing.Font printFont2 = new System.Drawing.Font("Segoe UI", 12))
                                {
                                    e.Graphics.DrawString("x", printFont2, Brushes.Black, 26, yy + 5);
                                }
                                e.Graphics.DrawString("Prescriptions", printFont1, Brushes.Black, 51, yy);
                            }
                            yy = yy + 30;
                            if (paperSize_print == "A5")
                            {
                                yy = yy + 20;
                                using (System.Drawing.Font printFont2 = new System.Drawing.Font("Segoe UI", 11))
                                {
                                    e.Graphics.DrawString("Drug Name", printFont2, Brushes.Black, 51, yy - 15);
                                    e.Graphics.DrawString("Strength", printFont2, Brushes.Black, 200, yy - 15);
                                    e.Graphics.DrawString("Frequency", printFont2, Brushes.Black, 300, yy - 15);
                                    e.Graphics.DrawString("Instructions", printFont2, Brushes.Black, 400, yy - 15);
                                }
                            }
                            else if (paperSize_print == "A4")
                            {
                                yy = yy + 20;
                                using (System.Drawing.Font printFont2 = new System.Drawing.Font("Segoe UI", 11))
                                {
                                    e.Graphics.DrawString("Drug Name", printFont2, Brushes.Black, 51, yy - 15);
                                    e.Graphics.DrawString("Strength", printFont2, Brushes.Black, 300, yy - 15);
                                    e.Graphics.DrawString("Frequency", printFont2, Brushes.Black, 420, yy - 15);
                                    e.Graphics.DrawString("Instructions", printFont2, Brushes.Black, 600, yy - 15);
                                }
                            }
                            else if (paperSize_print == "A3")
                            {
                                yy = yy + 20;
                                using (System.Drawing.Font printFont2 = new System.Drawing.Font("Segoe UI", 11))
                                {
                                    e.Graphics.DrawString("Drug Name", printFont2, Brushes.Black, 51, yy - 15);
                                    e.Graphics.DrawString("Strength", printFont2, Brushes.Black, 300, yy - 15);
                                    e.Graphics.DrawString("Frequency", printFont2, Brushes.Black, 420, yy - 15);
                                    e.Graphics.DrawString("Instructions", printFont2, Brushes.Black, 600, yy - 15);
                                }
                            }
                            yy = yy - 10;
                            System.Data.DataTable dt_prescription = this.cntrl.prescription_details(prescription_id);
                            if (dt_prescription.Rows.Count > 0)
                            {
                                yy = yy + 10;
                                for (int k = 0; k < dt_prescription.Rows.Count; k++)
                                {
                                    yy = yy + 40;
                                    string morning = "";
                                    string noon = "";
                                    string night = "";
                                    string a1 = dt_prescription.Rows[k]["morning"].ToString();
                                    string[] b1 = a1.Split('.');
                                    int right1 = int.Parse(b1[1]);
                                    morning = Convert.ToString(int.Parse(b1[0]));
                                    if (right1 != 0) { morning = morning + "." + Convert.ToString(int.Parse(b1[1])); }
                                    string a2 = dt_prescription.Rows[k]["noon"].ToString();
                                    string[] b2 = a2.Split('.');
                                    int right2 = int.Parse(b2[1]);
                                    noon = Convert.ToString(int.Parse(b2[0]));
                                    if (right2 != 0) { noon = noon + "." + Convert.ToString(int.Parse(b2[1])); }
                                    string a3 = dt_prescription.Rows[k]["night"].ToString();
                                    string[] b3 = a3.Split('.');
                                    int right3 = int.Parse(b3[1]);
                                    night = Convert.ToString(int.Parse(b3[0]));
                                    if (right3 != 0) { night = night + "." + Convert.ToString(int.Parse(b3[1])); }
                                    if (paperSize_print == "A5")
                                    {
                                        e.Graphics.DrawString(Convert.ToString(k + 1), printFont, Brushes.Black, 20, yy);
                                        e.Graphics.DrawString(dt_prescription.Rows[k]["drug_type"].ToString() + " " + dt_prescription.Rows[k]["drug_name"].ToString(), printFont, Brushes.Black, 51, yy);
                                        e.Graphics.DrawString(dt_prescription.Rows[k]["strength"].ToString() + " " + dt_prescription.Rows[k]["strength_gr"].ToString(), printFont, Brushes.Black, 200, yy);
                                        e.Graphics.DrawString(morning, printFont, Brushes.Black, 275, yy);
                                        e.Graphics.DrawString(noon, printFont, Brushes.Black, 305, yy);
                                        e.Graphics.DrawString(night, printFont, Brushes.Black, 335, yy);
                                        using (System.Drawing.Font printFont2 = new System.Drawing.Font("Segoe UI", 6))
                                        {
                                            if (dt_prescription.Rows[k]["status"].ToString() == "1")
                                            {
                                                e.Graphics.DrawString("  Morning", printFont2, Brushes.Black, 260, yy - 10);
                                                e.Graphics.DrawString("    Noon", printFont2, Brushes.Black, 290, yy - 10);
                                                e.Graphics.DrawString("    Night", printFont2, Brushes.Black, 325, yy - 10);
                                            }
                                            e.Graphics.DrawString(dt_prescription.Rows[k]["duration_unit"].ToString() + " " + dt_prescription.Rows[k]["duration_period"].ToString(), printFont2, Brushes.Black, 400, yy - 10);
                                            e.Graphics.DrawString(dt_prescription.Rows[k]["food"].ToString(), printFont2, Brushes.Black, 400, yy);
                                        }
                                        using (System.Drawing.Font printFon2 = new System.Drawing.Font("Segoe UI", 6))
                                        {
                                            e.Graphics.DrawString(dt_prescription.Rows[k]["add_instruction"].ToString(), printFon2, Brushes.Black, 51, yy + 15);
                                        }
                                    }
                                    else if (paperSize_print == "A4")
                                    {
                                        e.Graphics.DrawString(Convert.ToString(k + 1), printFont, Brushes.Black, 20, yy);
                                        e.Graphics.DrawString(dt_prescription.Rows[k]["drug_type"].ToString() + " " + dt_prescription.Rows[k]["drug_name"].ToString(), printFont, Brushes.Black, 51, yy);
                                        e.Graphics.DrawString(dt_prescription.Rows[k]["strength"].ToString() + " " + dt_prescription.Rows[k]["strength_gr"].ToString(), printFont, Brushes.Black, 300, yy);
                                        e.Graphics.DrawString(morning, printFont, Brushes.Black, 430, yy);
                                        e.Graphics.DrawString(noon, printFont, Brushes.Black, 460, yy);
                                        e.Graphics.DrawString(night, printFont, Brushes.Black, 490, yy);
                                        using (System.Drawing.Font printFont2 = new System.Drawing.Font("Segoe UI", 6.0f))
                                        {
                                            if (dt_prescription.Rows[k]["status"].ToString() == "1")
                                            {
                                                e.Graphics.DrawString("  Morning", printFont2, Brushes.Black, 415, yy - 10);
                                                e.Graphics.DrawString("    Noon", printFont2, Brushes.Black, 445, yy - 10);
                                                e.Graphics.DrawString("    Night", printFont2, Brushes.Black, 475, yy - 10);
                                            }
                                            e.Graphics.DrawString(dt_prescription.Rows[k]["duration_unit"].ToString() + " " + dt_prescription.Rows[k]["duration_period"].ToString(), printFont2, Brushes.Black, 600, yy - 10);
                                            e.Graphics.DrawString(dt_prescription.Rows[k]["food"].ToString(), printFont2, Brushes.Black, 600, yy);
                                        }

                                        using (System.Drawing.Font printFon2 = new System.Drawing.Font("Segoe UI", 6))
                                        {
                                            e.Graphics.DrawString(dt_prescription.Rows[k]["add_instruction"].ToString(), printFon2, Brushes.Black, 51, yy + 15);
                                        }
                                    }
                                    else if (paperSize_print == "A3")
                                    {
                                        e.Graphics.DrawString(Convert.ToString(k + 1), printFont, Brushes.Black, 20, yy);
                                        e.Graphics.DrawString(dt_prescription.Rows[k]["drug_type"].ToString() + " " + dt_prescription.Rows[k]["drug_name"].ToString(), printFont, Brushes.Black, 51, yy);
                                        e.Graphics.DrawString(dt_prescription.Rows[k]["strength"].ToString() + " " + dt_prescription.Rows[k]["strength_gr"].ToString(), printFont, Brushes.Black, 300, yy);
                                        e.Graphics.DrawString(morning, printFont, Brushes.Black, 430, yy);
                                        e.Graphics.DrawString(noon, printFont, Brushes.Black, 460, yy);
                                        e.Graphics.DrawString(night, printFont, Brushes.Black, 490, yy);
                                        using (System.Drawing.Font printFont2 = new System.Drawing.Font("Segoe UI", 6))
                                        {
                                            if (dt_prescription.Rows[k]["status"].ToString() == "1")
                                            {
                                                e.Graphics.DrawString("  Morning", printFont2, Brushes.Black, 415, yy - 10);
                                                e.Graphics.DrawString("    Noon", printFont2, Brushes.Black, 445, yy - 10);
                                                e.Graphics.DrawString("    Night", printFont2, Brushes.Black, 475, yy - 10);
                                            }
                                            e.Graphics.DrawString(dt_prescription.Rows[k]["duration_unit"].ToString() + " " + dt_prescription.Rows[k]["duration_period"].ToString(), printFont2, Brushes.Black, 600, yy - 10);
                                            e.Graphics.DrawString(dt_prescription.Rows[k]["food"].ToString(), printFont2, Brushes.Black, 600, yy);
                                        }

                                        using (System.Drawing.Font printFon2 = new System.Drawing.Font("Segoe UI", 6))
                                        {
                                            e.Graphics.DrawString(dt_prescription.Rows[k]["add_instruction"].ToString(), printFon2, Brushes.Black, 51, yy + 15);
                                        }
                                    }
                                }
                            } 
                            if (paperSize_print == "A5")
                            {
                                e.Graphics.DrawString(Convert.ToString(dt_cf.Rows[0]["notes"].ToString()), printFont, Brushes.Black, 45, 745 - topmargin1);
                                g.DrawLine(pen, new System.Drawing.Point(20, 760 - topmargin1), new System.Drawing.Point(500, 760 - topmargin1));
                            }
                            else if (paperSize_print == "A4")
                            {
                                e.Graphics.DrawString(Convert.ToString(dt_cf.Rows[0]["notes"].ToString()), printFont, Brushes.Black, 45, 1045 - topmargin1);
                                g.DrawLine(pen, new System.Drawing.Point(20, 1060 - topmargin1), new System.Drawing.Point(800, 1060 - topmargin1));
                            }
                            else if (paperSize_print == "A3")
                            {
                                e.Graphics.DrawString(Convert.ToString(dt_cf.Rows[0]["notes"].ToString()), printFont, Brushes.Black, 45, 1520 - topmargin1);
                                g.DrawLine(pen, new System.Drawing.Point(20, 1540 - topmargin1), new System.Drawing.Point(1150, 1540 - topmargin1));
                            }
                        }// Prescription main Record Count
                        System.Drawing.Font printFt = new System.Drawing.Font("Segoe UI", 9);
                        var txtDataWidth1 = e.Graphics.MeasureString(strfooter1, printFt).Width;
                        var txtDataWidth2 = e.Graphics.MeasureString(strfooter2, printFt).Width;
                        var txtDataWidth3 = e.Graphics.MeasureString(strfooter3, printFt).Width;
                        if (paperSize_print == "A5")
                        {
                            e.Graphics.DrawString(strfooter1, printFont, Brushes.Gray, 280 - (txtDataWidth1 / 2), 765 - topmargin1);
                            e.Graphics.DrawString(strfooter2, printFont, Brushes.Gray, 280 - (txtDataWidth2 / 2), 785 - topmargin1);
                            e.Graphics.DrawString(strfooter3, printFont, Brushes.Gray, 280 - (txtDataWidth3 / 2), 800 - topmargin1);
                        }
                        else if (paperSize_print == "A4")
                        {
                            e.Graphics.DrawString(strfooter1, printFont, Brushes.Gray, 400 - (txtDataWidth1 / 2), 1065 - topmargin1);
                            e.Graphics.DrawString(strfooter2, printFont, Brushes.Gray, 400 - (txtDataWidth2 / 2), 1085 - topmargin1);
                            e.Graphics.DrawString(strfooter3, printFont, Brushes.Gray, 400 - (txtDataWidth3 / 2), 1100 - topmargin1);
                        }
                        else if (paperSize_print == "A3")
                        {
                            e.Graphics.DrawString(strfooter1, printFont, Brushes.Gray, 550 - (txtDataWidth1 / 2), 1560 - topmargin1);
                            e.Graphics.DrawString(strfooter2, printFont, Brushes.Gray, 550 - (txtDataWidth2 / 2), 1580 - topmargin1);
                            e.Graphics.DrawString(strfooter3, printFont, Brushes.Gray, 550 - (txtDataWidth3 / 2), 1600 - topmargin1);
                        }
                    } // main printFont Stop
                } // Patient Details record Count Stop
            }
            catch (Exception ex)
            {
                MessageBox.Show("Printer Error..!!! Please check printer cable connection....");
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id;
                id = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRP' and Permission='A'");
                if (int.Parse(id) > 0)
                {
                    var form2 = new Prescription_Add();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
                else
                {
                    MessageBox.Show("There is No Privilege to Prescription", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                int currentMouseOverColumn = dataGridView1.HitTest(e.X, e.Y).ColumnIndex;
                if (currentMouseOverRow >= 0)
                {
                    if (currentMouseOverColumn == 5)
                    {
                        if (dataGridView1.Rows[currentMouseOverRow].Cells[0].Value.ToString() != "0")
                        {
                            prescription_id = dataGridView1.Rows[currentMouseOverRow].Cells[0].Value.ToString();
                            contextMenuStrip1.Show(dataGridView1, new System.Drawing.Point(890 - 120, e.Y));//925
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (doctor_id != "1")
                {
                    string id;
                    id = this.cntrl.edit_privillege(doctor_id);
                    if (int.Parse(id) > 0)
                    {
                        if (prescription_id != "0")
                        {
                            var form2 = new Prescription_Add();
                            form2.doctor_id = doctor_id;
                            form2.prescription_id = prescription_id;
                            form2.patient_id = patient_id;
                            openform(form2);
                        }

                        
                    }
                    else
                    {
                        MessageBox.Show("There is No Privilege to Edit Prescription", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    if (prescription_id != "0")
                    {
                        var form2 = new Prescription_Add();
                        form2.doctor_id = doctor_id;
                        form2.prescription_id = prescription_id;
                        form2.patient_id = patient_id;
                        openform(form2);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        dlt_privilige();
                    }
                    else
                    {
                        MessageBox.Show("There is No Privilege to delete Prescription", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    dlt_privilige();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void dlt_privilige()
        {
            try
            {
                if (prescription_id != "0")
                {
                    DialogResult res = MessageBox.Show("Are you sure you want to delete..?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (res == DialogResult.Yes)
                    {
                        this.cntrl.delete_prescription(prescription_id);
                        db.execute("delete from tbl_prescription_main where id='" + prescription_id + "'");
                        db.execute("delete from tbl_prescription where pres_id='" + prescription_id + "'");
                        this.cntrl.Get_maindtails(patient_id);
                        dataGridView1.Rows.Clear();
                        dataGridView1.ColumnCount = 6;
                        dataGridView1.RowCount = 0;
                        dataGridView1.ColumnHeadersVisible = false;
                        dataGridView1.RowHeadersVisible = false;
                        dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        System.Data.DataTable dt_pre_main = this.cntrl.Get_maindta(patient_id);
                        int i = 0;
                        for (int j = 0; j < dt_pre_main.Rows.Count; j++)
                        {
                            dataGridView1.Rows.Add("0", String.Format("{0:dddd,d MMMM , yyyy}", Convert.ToDateTime(dt_pre_main.Rows[j]["date"].ToString())), "", "", "");
                            dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.blank;
                            dataGridView1.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                            dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.DarkGreen;
                            i = i + 1;
                            dataGridView1.Rows.Add(dt_pre_main.Rows[j]["id"].ToString(), "Drug", "Frequency", "Duration", "Instruction", "");
                            dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.LightGray;
                            dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.LightGray;
                            dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.LightGray;
                            dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.LightGray;
                            dataGridView1.Rows[i].Cells[5].Style.BackColor = Color.LightGray;
                            dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.Black;
                            dataGridView1.Rows[i].Cells[2].Style.ForeColor = Color.Black;
                            dataGridView1.Rows[i].Cells[3].Style.ForeColor = Color.Black;
                            dataGridView1.Rows[i].Cells[4].Style.ForeColor = Color.Black;
                            dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.Bill;
                            System.Data.DataTable dt_prescription = this.cntrl.prescription_details((dt_pre_main.Rows[j]["id"].ToString()));
                            if (dt_prescription.Rows.Count > 0)
                            {
                                for (int k = 0; k < dt_prescription.Rows.Count; k++)
                                {
                                    i = i + 1;
                                    string morning = "";
                                    string noon = "";
                                    string night = "";
                                    string a1 = dt_prescription.Rows[k]["morning"].ToString();
                                    string[] b1 = a1.Split('.');
                                    int right1 = int.Parse(b1[1]);
                                    morning = Convert.ToString(int.Parse(b1[0]));
                                    if (right1 != 0) { morning = morning + "." + Convert.ToString(int.Parse(b1[1])); }
                                    string a2 = dt_prescription.Rows[k]["noon"].ToString();
                                    string[] b2 = a2.Split('.');
                                    int right2 = int.Parse(b2[1]);
                                    noon = Convert.ToString(int.Parse(b2[0]));
                                    if (right2 != 0) { noon = noon + "." + Convert.ToString(int.Parse(b2[1])); }
                                    string a3 = dt_prescription.Rows[k]["night"].ToString();
                                    string[] b3 = a3.Split('.');
                                    int right3 = int.Parse(b3[1]);
                                    night = Convert.ToString(int.Parse(b3[0]));
                                    if (right3 != 0) { night = night + "." + Convert.ToString(int.Parse(b3[1])); }
                                    dataGridView1.Rows.Add("0", dt_prescription.Rows[k]["drug_type"].ToString() + " " + dt_prescription.Rows[k]["drug_name"].ToString() + " " + dt_prescription.Rows[k]["strength"].ToString() + " " + dt_prescription.Rows[k]["strength_gr"].ToString(), morning + " - " + noon + " - " + night, dt_prescription.Rows[k]["duration_unit"].ToString() + " " + dt_prescription.Rows[k]["duration_period"].ToString(), dt_prescription.Rows[k]["add_instruction"].ToString(), "");
                                    dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.blank;
                                    dataGridView1.Rows[i].Height = 30;
                                }
                            }
                            i = i + 1;
                            dataGridView1.Rows.Add("0", "Prescribed by Dr." + dt_pre_main.Rows[j]["doctor_name"].ToString(), "", "", "");
                            dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                            dataGridView1.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                            dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.blank;
                            i = i + 1;
                            dataGridView1.Rows.Add("0", "", "", "", "");
                            dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.blank;
                            i = i + 1;
                        }
                        string dt = DateTime.Now.Date.ToString("yyyy-MM-dd");
                        DateTime Timeonly = DateTime.Now;
                        this.cntrl.save_log(doctor_id, "Prescription", " Delete Prescription", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Delete", prescription_id);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult yesno = MessageBox.Show("Are you sure you want copy this prescription..??", "Copy...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (yesno == DialogResult.Yes)
                {
                    int presid;
                    if (prescription_id != "0")
                    {
                        System.Data.DataTable td_prescription_main = this.cntrl.get_presctnMain(prescription_id);
                        if (td_prescription_main.Rows.Count > 0)
                        {
                            this.cntrl.save_prescriptionmain(td_prescription_main.Rows[0]["pt_id"].ToString(), td_prescription_main.Rows[0]["dr_id"].ToString(), td_prescription_main.Rows[0]["notes"].ToString());
                            string dt = this.cntrl.Maxid();
                            if (Convert.ToInt32(dt) > 0)
                            {
                                presid = Int32.Parse(dt);
                            }
                            else
                            {
                                presid = 1;
                            }
                            System.Data.DataTable td_prescription_Sub = this.cntrl.get_allprescription(prescription_id);
                            if (td_prescription_Sub.Rows.Count > 0)
                            {
                                for (int k = 0; k < td_prescription_Sub.Rows.Count; k++)
                                {
                                    this.cntrl.save_prescription(presid, td_prescription_Sub.Rows[k]["pt_id"].ToString(), td_prescription_Sub.Rows[k]["dr_name"].ToString(), td_prescription_Sub.Rows[k]["dr_id"].ToString(), DateTime.Now.ToString("yyyy-MM-dd"), td_prescription_Sub.Rows[k]["drug_name"].ToString(), td_prescription_Sub.Rows[k]["strength"].ToString(), td_prescription_Sub.Rows[k]["strength_gr"].ToString(), td_prescription_Sub.Rows[k]["duration_unit"].ToString(), td_prescription_Sub.Rows[k]["duration_period"].ToString(), td_prescription_Sub.Rows[k]["morning"].ToString(), td_prescription_Sub.Rows[k]["noon"].ToString(), td_prescription_Sub.Rows[k]["night"].ToString(), td_prescription_Sub.Rows[k]["food"].ToString(), td_prescription_Sub.Rows[k]["add_instruction"].ToString(), td_prescription_Sub.Rows[k]["drug_type"].ToString(), td_prescription_Sub.Rows[k]["status"].ToString(), td_prescription_Sub.Rows[k]["drug_id"].ToString());
                                }
                            }
                        }
                        // List Prescriptions..
                        dataGridView1.Rows.Clear();
                        dataGridView1.ColumnCount = 6;
                        dataGridView1.RowCount = 0;
                        dataGridView1.ColumnHeadersVisible = false;
                        dataGridView1.RowHeadersVisible = false;
                        dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        //this.cntrl.Get_maindtails(patient_id);
                        DataTable dt_pre_main = this.cntrl.Get_maindtails(patient_id); //Get_maindtails_count
                        Load_MainGrid(dt_pre_main);
                        // Show Prescription
                    }
                }//end
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
                int p = 0;
                string doct = this.cntrl.Get_DoctorName(doctor_id);
                string doctor_name = "";
                if (doct != "")
                {
                    doctor_name = doct;
                }
                System.Data.DataTable patient = this.cntrl.get_emailpatientdetails(patient_id);
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
                int Dexist = 0;
                string contact_no = "";
                string clinic_name = "";
                System.Data.DataTable dtp = this.cntrl.Get_companynameNo();
                if (dtp.Rows.Count > 0)
                {
                    clinic_name = dtp.Rows[0]["name"].ToString();
                    contact_no = dtp.Rows[0]["contact_no"].ToString();
                }

                string Apppath = System.IO.Directory.GetCurrentDirectory();
                StreamWriter sWrite = new StreamWriter(Apppath + "\\PrescriptionPrint.html");
                sWrite.WriteLine("<html>");
                sWrite.WriteLine("<head>");
                sWrite.WriteLine("</head>");
                sWrite.WriteLine("<body >");
                sWrite.WriteLine("<br><br><br>");

                sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr><th align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=4>" + clinic_name.ToString() + "</font></th></tr>");
                sWrite.WriteLine("<tr><th align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>" + contact_no.ToString() + "</font></th></tr>");
                sWrite.WriteLine("</table>");

                sWrite.WriteLine("<br>");
                sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");

                sWrite.WriteLine(" <tr height='40px'>");
                sWrite.WriteLine("    <td align='left' width='400px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Consulted by : <b> " + doctor_name.ToString() + " </b></font></td>");
                sWrite.WriteLine("	<td align='left' width='170px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2></font></td>");
                sWrite.WriteLine("	<td align='left' width='130px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2></font></td>");
                sWrite.WriteLine(" </tr>");

                sWrite.WriteLine("<tr>");

                sWrite.WriteLine("    <td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Name :<b>" + Pname.ToString() + " </b></font></td>");
                sWrite.WriteLine("	<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>DOB : <b> " + age.ToString() + " </b></font></td>");
                sWrite.WriteLine("	<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Gender : <b>" + Gender.ToString() + " </b></font></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine(" <tr>");
                sWrite.WriteLine("    <td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Address :<b> " + address.ToString() + "</b></font></td>");
                sWrite.WriteLine("	<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>DOA : <b> " + DOA + "</b></font></td>");

                sWrite.WriteLine(" </tr>");

                sWrite.WriteLine(" <tr>");
                sWrite.WriteLine("    <td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Mobile No:<b> " + Mobile.ToString() + "</b></font></td>");
                sWrite.WriteLine(" </tr>");

                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<br>");

                sWrite.WriteLine("<table align='center' style='width:700px;border: 1px;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td>");
                sWrite.WriteLine("<hr>");
                sWrite.WriteLine("<table align='center'  style='border: 1px ;border-collapse: collapse;' >");

                // Prescription
                System.Data.DataTable dt_prescription = this.cntrl.prescription_details(prescription_id);
                if (dt_prescription.Rows.Count > 0)
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("    <td align='left' width='230px' bgcolor=black><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> &nbsp;PRESCRIPTIONS&nbsp;&nbsp;&nbsp;</font></th>");
                    sWrite.WriteLine("    <td align='left' width='230px' bgcolor='#999999'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> &nbsp;&nbsp;</font></th>");
                    sWrite.WriteLine("	<td align='left'  width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2></font></th>");
                    sWrite.WriteLine("</tr>");

                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' colspan='2' >");

                    sWrite.WriteLine("<table align='center'  style='border: 1px ;border-collapse: collapse;' >");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' width='230px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>&nbsp;&nbsp;DRUGNAME</b> </font></td>");
                    sWrite.WriteLine("<td align='left' width='230px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;STRENGTH</b> </font></td>");
                    sWrite.WriteLine("<td align='left'width='230px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>&nbsp;&nbsp;&nbsp;FREEQUENCY</b> </font></td>");
                    sWrite.WriteLine("<td align='left'width='230px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>&nbsp;&nbsp;&nbsp;INSTRUCTIONS</b> </font></td>");
                    sWrite.WriteLine("</tr>");

                    while (p < dt_prescription.Rows.Count)
                    {

                        string morning = "";
                        string noon = "";
                        string night = "";
                        string a1 = dt_prescription.Rows[p]["morning"].ToString();
                        string[] b1 = a1.Split('.');
                        int right1 = int.Parse(b1[1]);
                        morning = Convert.ToString(int.Parse(b1[0]));
                        if (right1 != 0) { morning = morning + "." + Convert.ToString(int.Parse(b1[1])); }

                        string a2 = dt_prescription.Rows[p]["noon"].ToString();
                        string[] b2 = a2.Split('.');
                        int right2 = int.Parse(b2[1]);
                        noon = Convert.ToString(int.Parse(b2[0]));
                        if (right2 != 0) { noon = noon + "." + Convert.ToString(int.Parse(b2[1])); }

                        string a3 = dt_prescription.Rows[p]["night"].ToString();
                        string[] b3 = a3.Split('.');
                        int right3 = int.Parse(b3[1]);
                        night = Convert.ToString(int.Parse(b3[0]));
                        if (right3 != 0) { night = night + "." + Convert.ToString(int.Parse(b3[1])); }

                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("    <td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;&nbsp;&nbsp;&nbsp;" + dt_prescription.Rows[p]["drug_type"].ToString() + "" + dt_prescription.Rows[p]["drug_name"].ToString() + "</font></th>");
                        sWrite.WriteLine("    <td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dt_prescription.Rows[p]["strength"].ToString() + "" + dt_prescription.Rows[p]["strength_gr"].ToString() + "</font></th>");
                        sWrite.WriteLine("    <td align='left' width='230px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;" + morning + "-" + noon + "-" + night + "</font></th>");
                        sWrite.WriteLine("    <td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;" + dt_prescription.Rows[p]["duration_unit"].ToString() + " " + dt_prescription.Rows[p]["duration_period"].ToString() + "-" + dt_prescription.Rows[p]["add_instruction"].ToString() + "</font></th>");
                        sWrite.WriteLine("</tr>");
                        p++;
                    }

                    sWrite.WriteLine(" </table>");
                    sWrite.WriteLine(" </td>");
                    sWrite.WriteLine("</tr>");

                    sWrite.WriteLine("<tr >");
                    sWrite.WriteLine("    <td align='left' height='20'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3></font></th>");
                    sWrite.WriteLine("</tr>");

                }

                sWrite.WriteLine("</table>");
                sWrite.WriteLine("</td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");

                sWrite.WriteLine("</body>");
                sWrite.WriteLine("</html>");
                sWrite.Close();

                // mail senting...

                string email = "", emailName = "", emailPass = "";
                string query = "select email_address,pt_name from tbl_patient where id='" + patient_id + "'";
                System.Data.DataTable sr = db.table(query);
                if (sr.Rows.Count > 0)
                {
                    email = sr.Rows[0]["email_address"].ToString();
                    if (email != "")
                    {
                        System.Data.DataTable sms = db.table("select emailName,emailPass from tbl_SmsEmailConfig");
                        if (sms.Rows.Count > 0)
                        {
                            emailName = sms.Rows[0]["emailName"].ToString();
                            emailPass = sms.Rows[0]["emailPass"].ToString();
                            try
                            {
                                StreamReader reader = new StreamReader(Apppath + "\\PrescriptionPrint.html");
                                string readFile = reader.ReadToEnd();
                                string StrContent = "";
                                StrContent = readFile;
                                MailMessage message = new MailMessage();
                                message.From = new MailAddress(email);
                                message.To.Add(email);
                                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                                message.Subject = "Prescription";
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
                                MessageBox.Show("Email is Sent To : " + email);
                                reader.Close();
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please Activate Email Configuration");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Add EmailId for Selected patient");
                    }
                }
                //...end
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataTable print = this.cntrl.printsettings_details();
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

                if (print.Rows[0][1].ToString() == "Thermal Print")
                {
                    printhtml_thermal();
                }
                else
                {
                    printhtml();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void printhtml()
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
                System.Data.DataTable dtp = this.cntrl.Get_practiceDlNumber();
                if (dtp.Rows.Count > 0)
                {
                    clinicn = dtp.Rows[0]["name"].ToString();
                    Clinic = clinicn.Replace("¤", "'");
                    streetaddress = dtp.Rows[0]["street_address"].ToString();
                    str_locality = dtp.Rows[0]["locality"].ToString();
                    str_pincode = dtp.Rows[0]["pincode"].ToString();
                    contact_no = dtp.Rows[0]["contact_no"].ToString();
                    str_email = dtp.Rows[0]["email"].ToString();
                    str_website = dtp.Rows[0]["website"].ToString();
                    logo_name= dtp.Rows[0]["path"].ToString();
                }
                string strfooter1 = "";
                string strfooter2 = "";
                string strfooter3 = "";
                string header1 = "";
                string header2 = "";
                string header3 = "";
                string check = "";
                string header_image = "";
                System.Data.DataTable print = this.cntrl.printsettings();
                if (print.Rows.Count > 0)
                {
                    header1 = print.Rows[0]["header"].ToString();
                    header2 = print.Rows[0]["left_text"].ToString();
                    header3 = print.Rows[0]["right_text"].ToString();
                    strfooter1 = print.Rows[0]["fullwidth_context"].ToString();
                    strfooter2 = print.Rows[0]["left_sign"].ToString();
                    strfooter3 = print.Rows[0]["right_sign"].ToString();
                    header_image = print.Rows[0]["header_path"].ToString();
                    check = print.Rows[0]["set_as_default_header"].ToString();
                }
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                System.IO.StreamWriter sWrite = new System.IO.StreamWriter(Apppath + "\\Prescription_Print.html");
                sWrite.WriteLine("<html>");
                sWrite.WriteLine("<head>");
                sWrite.WriteLine("</head>");
                sWrite.WriteLine("<body >");
                sWrite.WriteLine("<br>");
                
                if (check == "Yes")
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
                                    sWrite.WriteLine("<td width='100' height='75px' align='left' rowspan='3'><img src='" + Appath + "\\" + logo_name + "' width='77' height='78' style='width:100px;height:100px;'></td>  ");
                                    sWrite.WriteLine("<td width='588' align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + header1 + "</font></td></tr>");
                                    sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;&nbsp;" + header2 + "</font></td></tr>");
                                    sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + header3 + "</font></td></tr>");
                                    sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                                    sWrite.WriteLine("</table>");
                                }
                                else
                                {
                                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                                    sWrite.WriteLine("<tr>");
                                    sWrite.WriteLine("<td  align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + header1 + "</font></td></tr>");
                                    sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;&nbsp;" + header2 + "</font></td></tr>");
                                    sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + header3 + "</font></td></tr>");
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
                                sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + header3 + "</font></td></tr>");
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
                            sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + header3 + "</font></td></tr>");
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
                    string path = this.db.server();
                    if (File.Exists(path + "\\" + header_image))
                    {
                        sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td  align='left'><img src='" + path + "\\" + header_image + "' width='700' height='80';'></td>  ");
                        sWrite.WriteLine("</tr>");
                        sWrite.WriteLine("</table>");

                    }
                }
                int Dexist = 0;
                string sexage = "";
                string address = "";
                address = "";
                string strNote = "";
                string strreview = "NO";
                string strreview_date = "";
                System.Data.DataTable dt1 = this.cntrl.patient_details(patient_id);
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
                        age = "/" + dt1.Rows[0]["age"].ToString() + dt1.Rows[0]["days"].ToString();
                    }
                    if (dt1.Rows[0]["age2"].ToString() != "")
                    {
                        if (!string.IsNullOrEmpty(age))
                        {
                            age = age + " " + dt1.Rows[0]["age2"].ToString() + "Months";
                        }
                        else
                        {
                            age = "/" + dt1.Rows[0]["age2"].ToString() + "Months";
                        }

                    }
                    sWrite.WriteLine(" <td align='left' height=25><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>" + dt1.Rows[0]["pt_name"].ToString() + "</b><i> (" + sexage +age+ ")</i></font></td>");
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

                    string doctorname = "";
                    System.Data.DataTable dt_cf = this.cntrl.table_details(prescription_id, patient_id);
                    if (dt_cf.Rows.Count > 0)
                    {
                        doctorname = Convert.ToString(dt_cf.Rows[0]["doctor_name"].ToString());
                        strNote = dt_cf.Rows[0]["notes"].ToString();
                        if (dt_cf.Rows[0]["review"].ToString() == "YES")
                        {
                            strreview = "YES";
                            strreview_date = Convert.ToDateTime(dt_cf.Rows[0]["Review_date"].ToString()).ToString("dd-MM-yyyy hh:mm tt");
                        }
                        else
                        {
                            strreview = "NO";
                        }
                    }
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' width='400px' height='30px'><FONT FACE='Geneva, Segoe UI' SIZE=2><FONT COLOR=black >By</FONT> :Dr. <b>" + doctorname + " </b></font></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("</table>");
                    sWrite.WriteLine("<br>");
                    sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=5>R</font><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>x&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</font><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=5>Prescription</FONT></td>");
                    sWrite.WriteLine("<td width=250px></td>");
                    if (dt_cf.Rows.Count > 0)
                    {
                        sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> <FONT COLOR=black>Date : </FONT>" + DateTime.Parse(dt_cf.Rows[0]["date"].ToString()).ToString("dd MMM yyyy") + "</font></td>");
                    }
                    else
                    {
                        sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> <FONT COLOR=black>Date : </FONT>" + DateTime.Now.ToString("dd MMM yyyy") + "</font></td>");
                    }
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("</table>");
                }

                sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");
                sWrite.WriteLine("<tr >");
                sWrite.WriteLine("<td align='left' width='10%' height='30'><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;Sl.</font></td>");
                sWrite.WriteLine("<td align='left' width='20%' ><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;Drug Name</font></td>");
                sWrite.WriteLine("<td align='center' width='15%' ><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;Strength </font></td>");
                sWrite.WriteLine("<td align='center' width='30%' colspan='3' ><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;Frequency</font></td>");
                sWrite.WriteLine("<td align='center' width='10%' ><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;</font></td>");
                sWrite.WriteLine("<td align='left' width='15%'><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;Instructions</font></td>");
                sWrite.WriteLine("</tr>");
                System.Data.DataTable dt_prescription =this.cntrl.prescription_details(prescription_id);
                if (dt_prescription.Rows.Count > 0)
                {
                    for (int k = 0; k < dt_prescription.Rows.Count; k++)
                    {
                        string morning = "", duration="";
                        string noon = "";
                        string night = "";
                        string a1 = dt_prescription.Rows[k]["morning"].ToString();
                        string[] b1 = a1.Split('.');
                        int right1 = int.Parse(b1[1]);
                        morning = Convert.ToString(int.Parse(b1[0]));
                        if (right1 != 0) { morning = morning + "." + Convert.ToString(int.Parse(b1[1])); }

                        string a2 = dt_prescription.Rows[k]["noon"].ToString();
                        string[] b2 = a2.Split('.');
                        int right2 = int.Parse(b2[1]);
                        noon = Convert.ToString(int.Parse(b2[0]));
                        if (right2 != 0) { noon = noon + "." + Convert.ToString(int.Parse(b2[1])); }

                        string a3 = dt_prescription.Rows[k]["night"].ToString();
                        string[] b3 = a3.Split('.');
                        int right3 = int.Parse(b3[1]);
                        night = Convert.ToString(int.Parse(b3[0]));
                        if (right3 != 0) { night = night + "." + Convert.ToString(int.Parse(b3[1])); }
                        //}
                        duration = dt_prescription.Rows[k]["duration_unit"].ToString();
                        if (duration.Trim() == "0" || duration=="")
                        {
                            duration = "";

                        }
                        else
                        {
                            duration = dt_prescription.Rows[k]["duration_unit"].ToString() + "" + dt_prescription.Rows[k]["duration_period"].ToString();
                        }
                        if (morning == "0" && noon == "0" && night == "0")
                        {
                            morning = ""; noon = ""; night = "";
                        }

                        if (dt_prescription.Rows[k]["status"].ToString() == "1")
                        {
                            sWrite.WriteLine("<tr>");
                            sWrite.WriteLine("<td align='left' height='7'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1></font></td>");
                            sWrite.WriteLine("<td align='left' height='7'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1></font></td>");
                            sWrite.WriteLine("<td align='left' height='7'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1></font></td>");
                            if (morning == "" && noon == "" && night == "")
                            {

                            }
                            else
                            {
                                sWrite.WriteLine("<td align='center' height='7' valign='bottom'  width='50px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;Morning </font></td>");
                                sWrite.WriteLine("<td align='center' height='7'  valign='bottom' width='50px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;Noon </font></td>");
                                sWrite.WriteLine("<td align='center' height='7' valign='bottom'  width='50px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;Night </font></td>");

                            }
                            sWrite.WriteLine("</tr>");
                        }
                        sWrite.WriteLine("<tr>");
                        if (dt_prescription.Rows[k]["add_instruction"].ToString() != "")
                        {
                            sWrite.WriteLine("<td align='left' height='20' valign='top'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + Convert.ToString(k + 1) + " </font></td>");
                        }
                        else
                        {
                            sWrite.WriteLine("<td align='left' height='30' valign='top'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + Convert.ToString(k + 1) + " </font></td>");
                        }
                        sWrite.WriteLine("<td align='left'   valign='top' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + dt_prescription.Rows[k]["drug_type"].ToString() + " " + dt_prescription.Rows[k]["drug_name"].ToString() + " </font></td>");
                        //sWrite.WriteLine("<td align='center' valign='top' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + dt_prescription.Rows[k]["strength"].ToString() + " " + dt_prescription.Rows[k]["strength_gr"].ToString() + " </font></td>");
                        if (morning == "" && noon == "" && night == "")
                       {
                            //sWrite.WriteLine("<td align='center' valign='top' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" " </font></td>");
                            //sWrite.WriteLine("<td align='center' valign='top' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" " </font></td>");
                            //sWrite.WriteLine("<td align='center' valign='top' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" " </font></td>");

                        }
                        else
                        {
                            sWrite.WriteLine("<td align='center' valign='top' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + dt_prescription.Rows[k]["strength"].ToString() + " " + dt_prescription.Rows[k]["strength_gr"].ToString() + " </font></td>");
                            sWrite.WriteLine("<td align='center' valign='top' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + morning + " </font></td>");
                            sWrite.WriteLine("<td align='center' valign='top' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + noon + " </font></td>");
                            sWrite.WriteLine("<td align='center' valign='top' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + night + " </font></td>");
                           

                        }
                        if (dt_prescription.Rows[k]["duration_unit"].ToString() == "0" )
                        {
                            sWrite.WriteLine("<td align='left'   valign='top'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;" + dt_prescription.Rows[k]["food"].ToString() + " </font></td>");
                        }
                        else
                        {
                            sWrite.WriteLine("<td align='left'   valign='top'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;" + duration  + "</br>" + dt_prescription.Rows[k]["food"].ToString() + " </font></td>");// dt_prescription.Rows[k]["duration_unit"].ToString() + " " + dt_prescription.Rows[k]["duration_period"].ToString()
                        }
                        
                        //sWrite.WriteLine("</tr>");
                        if (dt_prescription.Rows[k]["add_instruction"].ToString() != "")
                        {
                            //sWrite.WriteLine("<tr>");
                            //sWrite.WriteLine("<td ></td>");
                            sWrite.WriteLine("<td align='left' valign='top' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1.5>&nbsp;" + dt_prescription.Rows[k]["add_instruction"].ToString() + " </font></td>");
                            sWrite.WriteLine("</tr>");
                        }
                        
                    } // Presription Sub(Drug Details) Record Count
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' height='30' colspan='8'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + strNote.ToString() + " </font></td>");
                    sWrite.WriteLine("</tr>");
                    if (strreview == "YES")
                    {
                        sWrite.WriteLine("<tr><td align='left' colspan=8 ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;Next Review Date : " + strreview_date + " </font></td></tr>");
                    }
                    sWrite.WriteLine("<tr><td align='left' colspan=8><hr/></td></tr>");
                }
                sWrite.WriteLine("</table>");
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
                sWrite.WriteLine("</body >");
                sWrite.WriteLine("</html>");
                sWrite.Close();
                System.Diagnostics.Process.Start(Apppath + "\\Prescription_Print.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Printing Error..", "Print error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void printhtml_thermal()
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
                System.Data.DataTable dtp = this.cntrl.Get_practiceDlNumber();
                if (dtp.Rows.Count > 0)
                {
                    clinicn = dtp.Rows[0]["name"].ToString();
                    Clinic = clinicn.Replace("¤", "'");
                    streetaddress = dtp.Rows[0]["street_address"].ToString();
                    str_locality = dtp.Rows[0]["locality"].ToString();
                    str_pincode = dtp.Rows[0]["pincode"].ToString();
                    contact_no = dtp.Rows[0]["contact_no"].ToString();
                    str_email = dtp.Rows[0]["email"].ToString();
                    str_website = dtp.Rows[0]["website"].ToString();
                    logo_name = dtp.Rows[0]["path"].ToString();
                }
                string strfooter1 = "";
                string strfooter2 = "";
                string strfooter3 = "";
                string header1 = "";
                string header2 = "";
                string header3 = "";
                System.Data.DataTable print = this.cntrl.printsettings();
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
                System.IO.StreamWriter sWrite = new System.IO.StreamWriter(Apppath + "\\PrescriptionPrint.html");
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
                                sWrite.WriteLine("<td width='100' height='75px' align='center' ><img src='" + Appath + "\\" + logo_name + "' width='77' height='25px' style='width:100px;height:100px;'></td></tr>  ");
                                sWrite.WriteLine("<tr><td  align='center' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=4><b>&nbsp;&nbsp;" + header1 + "</b></font></td></tr>");
                                sWrite.WriteLine("<tr><td  align='center' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>&nbsp;&nbsp;" + header2 + "</b></font></td></tr>");
                                sWrite.WriteLine("<tr><td align='center' height='20' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=1><b>&nbsp;&nbsp;" + header3 + "</b></font></td></tr>");
                                sWrite.WriteLine("<tr><td align='center' colspan='2'><hr/></td></tr>");
                                sWrite.WriteLine("</table>");
                            }
                            else
                            {
                                sWrite.WriteLine("<table align='center' style='width:270px;border: 1px ;border-collapse: collapse;'>");
                                sWrite.WriteLine("<tr>");
                                sWrite.WriteLine("<td  align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + header1 + "</font></td></tr>");
                                sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;&nbsp;" + header2 + "</font></td></tr>");
                                sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + header3 + "</font></td></tr>");
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
                            sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + header3 + "</font></td></tr>");
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
                        sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + header3 + "</font></td></tr>");
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
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<table align='center' style='width:270px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr><th align='center'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3><b>Prescription </b></font></th></tr>");
                sWrite.WriteLine("</table>");
                string sexage = "";
                address = "";
                System.Data.DataTable dt1 = this.cntrl.patient_details(patient_id);
                System.Data.DataTable dt_cf = this.cntrl.table_details(prescription_id, patient_id);

                if (dt1.Rows.Count > 0)
                {
                    sWrite.WriteLine("<table align='center' style='width:270px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    if (dt1.Rows[0]["gender"].ToString() != "")
                    {
                        sexage = dt1.Rows[0]["gender"].ToString();
                        //Dexist = 1;
                    }
                    if (dt1.Rows[0]["age"].ToString() != "")
                    {
                        age = "/" + dt1.Rows[0]["age"].ToString() + dt1.Rows[0]["days"].ToString();
                    }
                    if (dt1.Rows[0]["age2"].ToString() != "")
                    {
                        if (!string.IsNullOrEmpty(age))
                        {
                            age = age + " " + dt1.Rows[0]["age2"].ToString() + "Months";
                        }
                        else
                        {
                            age = "/" + dt1.Rows[0]["age2"].ToString() + "Months";
                        }
                    }
                    sWrite.WriteLine(" <td align='left' height=25><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Name:<b>" + dt1.Rows[0]["pt_name"].ToString() + "</b></font></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine(" </tr>");
                    //Dexist = 0;
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Age :" + age + " </font></td>");

                    sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><FONT COLOR=black>Sex : </FONT>" + sexage + "</font></td>");
                    sWrite.WriteLine(" </tr>");

                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Mobile :" + dt1.Rows[0]["primary_mobile_number"].ToString() + " </font></td>");
                    sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> <FONT COLOR=black>Date : </FONT>" + DateTime.Parse(dt_cf.Rows[0]["date"].ToString()).ToString("dd MMM yyyy") + "</font></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine("<tr><td colspan=2><hr></td></tr>");

                    string doctorname = "";
                    sWrite.WriteLine("<table align='center'   style='width:270px;border: 1px ;border-collapse: collapse;' >");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td width=250px></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("</table>");
                }

                sWrite.WriteLine("<table align='center'   style='width:270px;border: 1px ;border-collapse: collapse;' >");
                sWrite.WriteLine("<tr >");
                sWrite.WriteLine("<td align='left' width='50%' ><FONT COLOR=black FACE=' Segoe UI' SIZE=2><b>Medicine</b></font></td>");
                sWrite.WriteLine("<td align='center' width='30%' ><FONT COLOR=black FACE=' Segoe UI' SIZE=2><b>Frequency</b></font></td>");
                sWrite.WriteLine("<td align='right' width='10%' ><FONT COLOR=black FACE=' Segoe UI' SIZE=2><b>Duration</b></font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr><td colspan=3><hr></td></tr>");
                System.Data.DataTable dt_prescription = this.cntrl.prescription_details(prescription_id);
                if (dt_prescription.Rows.Count > 0)
                {
                    for (int k = 0; k < dt_prescription.Rows.Count; k++)
                    {
                        string morning = "", duration = "";
                        string noon = "";
                        string night = "";
                        string a1 = dt_prescription.Rows[k]["morning"].ToString();
                        string[] b1 = a1.Split('.');
                        int right1 = int.Parse(b1[1]);
                        morning = Convert.ToString(int.Parse(b1[0]));
                        if (right1 != 0) { morning = morning + "." + Convert.ToString(int.Parse(b1[1])); }
                        string a2 = dt_prescription.Rows[k]["noon"].ToString();
                        string[] b2 = a2.Split('.');
                        int right2 = int.Parse(b2[1]);
                        noon = Convert.ToString(int.Parse(b2[0]));
                        if (right2 != 0) { noon = noon + "." + Convert.ToString(int.Parse(b2[1])); }
                        string a3 = dt_prescription.Rows[k]["night"].ToString();
                        string[] b3 = a3.Split('.');
                        int right3 = int.Parse(b3[1]);
                        night = Convert.ToString(int.Parse(b3[0]));
                        if (right3 != 0) { night = night + "." + Convert.ToString(int.Parse(b3[1])); }
                        //}
                        duration = dt_prescription.Rows[k]["duration_unit"].ToString();
                        if (duration.Trim() == "0" || duration == "")
                        {
                            duration = "";

                        }
                        else
                        {
                            duration = dt_prescription.Rows[k]["duration_unit"].ToString() + "" + dt_prescription.Rows[k]["duration_period"].ToString();
                        }
                        if (morning == "0" && noon == "0" && night == "0")
                        {
                            morning = ""; noon = ""; night = "";
                        }

                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='left'   valign='top' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + dt_prescription.Rows[k]["drug_name"].ToString() + " </font></td>");
                        sWrite.WriteLine("<td align='center' valign='top' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + morning + "-" + noon + "-" + night + " </font></td>");
                        sWrite.WriteLine("<td align='right'   valign='top'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>" + duration + " </font></td>");
                    } 
                    sWrite.WriteLine("<tr><td align='left' colspan=3><hr/></td></tr>");
                }
                sWrite.WriteLine("</table>");
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
                sWrite.WriteLine("</body >");
                sWrite.WriteLine("</html>");
                sWrite.Close();
                System.Diagnostics.Process.Start(Apppath + "\\PrescriptionPrint.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Printing Error..", "Print error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        public DataTable dt_shortname = new DataTable();

        public void fill_presctn_shortname(string type)
        {
            if(type=="capsule")
            {

            }
        }
        private void sentSMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string scount = "";
                DataTable smscount = this.ccntrl.getsmscnt();
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
                        System.Data.DataTable pat = this.cntrl.get_patientnumber(patient_id);
                        System.Data.DataTable smsreminder = this.cntrl.remindersms();
                        if (pat.Rows.Count > 0)
                        {
                            //prescription msg
                            System.Data.DataTable dt_prescription = this.cntrl.prescription_details(prescription_id);
                            if (dt_prescription.Rows.Count > 0)
                            {
                                while (p < dt_prescription.Rows.Count)
                                {
                                    string morning = "", shortname = "", upper_string = "";
                                    string noon = "";
                                    string night = "";
                                    string a1 = dt_prescription.Rows[p]["morning"].ToString();
                                    string[] b1 = a1.Split('.');
                                    int right1 = int.Parse(b1[1]);
                                    morning = Convert.ToString(int.Parse(b1[0]));
                                    if (right1 != 0) { morning = morning + "." + Convert.ToString(int.Parse(b1[1])); }
                                    string a2 = dt_prescription.Rows[p]["noon"].ToString();
                                    string[] b2 = a2.Split('.');
                                    int right2 = int.Parse(b2[1]);
                                    noon = Convert.ToString(int.Parse(b2[0]));
                                    if (right2 != 0) { noon = noon + "." + Convert.ToString(int.Parse(b2[1])); }
                                    string a3 = dt_prescription.Rows[p]["night"].ToString();
                                    string[] b3 = a3.Split('.');
                                    int right3 = int.Parse(b3[1]);
                                    night = Convert.ToString(int.Parse(b3[0]));
                                    if (right3 != 0) { night = night + "." + Convert.ToString(int.Parse(b3[1])); }
                                    upper_string = dt_prescription.Rows[p]["drug_type"].ToString().ToUpper();
                                    if (upper_string == "CAPSULE")
                                    {
                                        shortname = "Cap";
                                    }
                                    else if (upper_string == "SYRUP")
                                    {
                                        shortname = "SYR";
                                    }
                                    else if (upper_string == "TABLET")
                                    {
                                        shortname = "tab";
                                    }
                                    else if (upper_string == "INJECTION")
                                    {
                                        shortname = "inj";
                                    }
                                    else if (upper_string == "LOTION")
                                    {
                                        shortname = "lot";
                                    }
                                    else if (upper_string == "OINTMENT")
                                    {
                                        shortname = "ung";
                                    }
                                    else if (upper_string == "CREAM")
                                    {
                                        shortname = "crm";
                                    }
                                    else if (upper_string == "POWDER")
                                    {
                                        shortname = "pulv";
                                    }
                                    strPriscription = strPriscription + " [" + shortname + "]" + dt_prescription.Rows[p]["drug_name"].ToString() + "" + dt_prescription.Rows[p]["strength"].ToString() + " " + dt_prescription.Rows[p]["strength_gr"].ToString() + ", Duration: " + morning + "-" + noon + "-" + night + " for " + dt_prescription.Rows[p]["duration_unit"].ToString() + " " + dt_prescription.Rows[p]["duration_period"].ToString() + "-" + dt_prescription.Rows[p]["add_instruction"].ToString() + "\n\n";
                                    p++;
                                }
                                //end prescription msg
                                string type = "LNG";
                                text = "Dear " + pat.Rows[0]["pt_name"].ToString() + "," + "\n" + "Prescription:" + "\n" + "\n" + "Drug Name:" + strPriscription + "Regards With " + clinic + "," + contact_no +"CLIINI";
                                string number = "91" + pat.Rows[0]["primary_mobile_number"].ToString();
                                a.SendSMS(smsName, smsPass, number, text, type);
                                this.cntrl.savecommunication(patient_id, text);
                                MessageBox.Show("The Prescription Details Containing  Message Sent Successfully to " + pat.Rows[0]["pt_name"].ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        scount = Convert.ToString(Convert.ToInt32(scount) - 1);
                        string Encrypt = PappyjoeMVC.Model.EncryptionDecryption.Encrypt(scount, "ch3lSeAW0n2o2!C1");
                        this.ccntrl.smsCount(Encrypt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void labelprofile_Click(object sender, EventArgs e)
        {
            var form2 = new PappyjoeMVC.View.Patient_Profile_Details();
            form2.doctor_id = doctor_id;
            form2.patient_id = patient_id;
            openform(form2);
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

        private void label14_Click(object sender, EventArgs e)
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
        private void LabelVitalSigns_Click(object sender, EventArgs e)
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

        private void lbl_NursesNotes_Click(object sender, EventArgs e)
        {
            //var form2 = new Nurses_Notes();
            //form2.doctor_id = doctor_id;
            //form2.patient_id = patient_id;
            //openform(form2);

        }
        public int rowcount = 25;
        private void label1_Click(object sender, EventArgs e)
        {
            //rowcount = dataGridView1.Rows.Count;
            if(Convert.ToInt32(lb_prescr_cnt.Text)>50)
            {
                int count = rowcount + 25;
                DataTable dt_pre_main = this.cntrl.Get_maindtails_count(patient_id, count);
                if (dt_pre_main.Rows.Count > rowcount)
                    Load_MainGrid(dt_pre_main);
                rowcount = count;
            }
           
        }

        public void nurse_count()
        {
            System.Data.DataTable dt_cf_main = this.nurse_cntrl.dt_cf_main(patient_id);
            if (dt_cf_main.Rows.Count > 0)
            {
                lb_Nurses_Notes.Text = dt_cf_main.Rows.Count.ToString();
            }
        }

        private void whatsappToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string recipient = "";

            System.Data.DataTable pat = this.cntrl.get_patientnumber(patient_id);
            if (pat.Rows.Count > 0)
            {
                //recipient = "+919846263694";
                string number = "+91" + pat.Rows[0]["primary_mobile_number"].ToString();
                recipient = number.Replace(" ", "");

            }
            try
            {


                int p = 0;
                string clinic = "", cname = "", numb = "";
                string contact_no = "";
                string text = "";
                string smsName = "", smsPass = "";
                string strPriscription = "";
                //System.Data.DataTable pat = this.cntrl.get_patientnumber(patient_id);
                if (pat.Rows.Count > 0)
                {
                    System.Data.DataTable clinicname = this.cntrl.Get_companynameNo();
                    if (clinicname.Rows.Count > 0)
                    {
                        clinic = clinicname.Rows[0][0].ToString();
                        //clinic = cname.Replace("¤", "'");
                        contact_no = clinicname.Rows[0][1].ToString();
                    }
                    //prescription msg
                    System.Data.DataTable dt_prescription = this.cntrl.prescription_details(prescription_id);
                    if (dt_prescription.Rows.Count > 0)
                    {
                        while (p < dt_prescription.Rows.Count)
                        {
                            string morning = "", shortname = "", upper_string = "";
                            string noon = "";
                            string night = "";
                            string a1 = dt_prescription.Rows[p]["morning"].ToString();
                            string[] b1 = a1.Split('.');
                            int right1 = int.Parse(b1[1]);
                            morning = Convert.ToString(int.Parse(b1[0]));
                            if (right1 != 0) { morning = morning + "." + Convert.ToString(int.Parse(b1[1])); }
                            string a2 = dt_prescription.Rows[p]["noon"].ToString();
                            string[] b2 = a2.Split('.');
                            int right2 = int.Parse(b2[1]);
                            noon = Convert.ToString(int.Parse(b2[0]));
                            if (right2 != 0) { noon = noon + "." + Convert.ToString(int.Parse(b2[1])); }
                            string a3 = dt_prescription.Rows[p]["night"].ToString();
                            string[] b3 = a3.Split('.');
                            int right3 = int.Parse(b3[1]);
                            night = Convert.ToString(int.Parse(b3[0]));
                            if (right3 != 0) { night = night + "." + Convert.ToString(int.Parse(b3[1])); }
                            upper_string = dt_prescription.Rows[p]["drug_type"].ToString().ToUpper();
                            if (upper_string == "CAPSULE")
                            {
                                shortname = "Cap";
                            }
                            else if (upper_string == "SYRUP")
                            {
                                shortname = "SYR";
                            }
                            else if (upper_string == "TABLET")
                            {
                                shortname = "tab";
                            }
                            else if (upper_string == "INJECTION")
                            {
                                shortname = "inj";
                            }
                            else if (upper_string == "LOTION")
                            {
                                shortname = "lot";
                            }
                            else if (upper_string == "OINTMENT")
                            {
                                shortname = "ung";
                            }
                            else if (upper_string == "CREAM")
                            {
                                shortname = "crm";
                            }
                            else if (upper_string == "POWDER")
                            {
                                shortname = "pulv";
                            }
                            strPriscription = strPriscription + " [" + shortname + "]" + dt_prescription.Rows[p]["drug_name"].ToString() + "" + dt_prescription.Rows[p]["strength"].ToString() + " " + dt_prescription.Rows[p]["strength_gr"].ToString() + ", Duration: " + morning + "-" + noon + "-" + night + " for " + dt_prescription.Rows[p]["duration_unit"].ToString() + " " + dt_prescription.Rows[p]["duration_period"].ToString() + "-" + dt_prescription.Rows[p]["add_instruction"].ToString() + "\n\n";
                            p++;
                        }
                        //end prescription msg
                        string type = "LNG";
                        text = "Dear " + pat.Rows[0]["pt_name"].ToString() + "," + "\n" + "Prescription:" + "\n" + "\n" + "Drug Name:" + strPriscription + "Regards With " + clinic + "," + contact_no + "CLIINI";
                        string number = "+91" + pat.Rows[0]["primary_mobile_number"].ToString();
                        numb = number.Replace(" ", "");
                        System.Diagnostics.Process.Start("http://api.whatsapp.com/send?phone=" + numb + "&text=" + text);
                    http://api.whatsmate.net/v3/whatsapp/single/document/message/
                        MessageBox.Show("The Prescription Details Containing  Message Sent Successfully to " + pat.Rows[0]["pt_name"].ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SetController(Prescription_Show_controller controller)
        {
            cntrl = controller;
        }
        public void Load_MainGrid(DataTable dt_pre_main)
        {
            try
            {
                int i = 0;
                if (dt_pre_main.Rows.Count > 0)
                {
                    for (int j = 0; j < dt_pre_main.Rows.Count; j++)
                    {
                        dataGridView1.Rows.Add("0", String.Format("{0:dddd,d MMMM , yyyy}", Convert.ToDateTime(dt_pre_main.Rows[j]["date"].ToString())), "", "", "");
                        dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.blank;
                        dataGridView1.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                        dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.DarkGreen;
                        i = i + 1;
                        dataGridView1.Rows.Add(dt_pre_main.Rows[j]["id"].ToString(), "Drug", "Frequency", "Duration", "Instruction", "");
                        dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.LightGray;
                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.LightGray;
                        dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.LightGray;
                        dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.LightGray;
                        dataGridView1.Rows[i].Cells[5].Style.BackColor = Color.LightGray;
                        dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.Black;
                        dataGridView1.Rows[i].Cells[2].Style.ForeColor = Color.Black;
                        dataGridView1.Rows[i].Cells[3].Style.ForeColor = Color.Black;
                        dataGridView1.Rows[i].Cells[4].Style.ForeColor = Color.Black;
                        dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.Bill;
                        System.Data.DataTable dt_prescription = this.cntrl.prescription_details(dt_pre_main.Rows[j]["id"].ToString());
                        if (dt_prescription.Rows.Count > 0)
                        {
                            for (int k = 0; k < dt_prescription.Rows.Count; k++)
                            {
                                i = i + 1;
                                string morning = "", duration = "";
                                string noon = "";
                                string night = "";
                                string a1 = dt_prescription.Rows[k]["morning"].ToString();
                                string[] b1 = a1.Split('.');
                                int right1 = int.Parse(b1[1]);
                                morning = Convert.ToString(int.Parse(b1[0]));
                                if (right1 != 0) { morning = morning + "." + Convert.ToString(int.Parse(b1[1])); }
                                string a2 = dt_prescription.Rows[k]["noon"].ToString();
                                string[] b2 = a2.Split('.');
                                int right2 = int.Parse(b2[1]);
                                noon = Convert.ToString(int.Parse(b2[0]));
                                if (right2 != 0) { noon = noon + "." + Convert.ToString(int.Parse(b2[1])); }
                                string a3 = dt_prescription.Rows[k]["night"].ToString();
                                string[] b3 = a3.Split('.');
                                int right3 = int.Parse(b3[1]);
                                night = Convert.ToString(int.Parse(b3[0]));
                                if (right3 != 0) { night = night + "." + Convert.ToString(int.Parse(b3[1])); }
                                duration = dt_prescription.Rows[k]["duration_unit"].ToString();
                                //updation
                                //if (morning == "0" && noon =="0" && night == "0"  )
                                //{
                                //    morning = "";
                                //    noon = "";
                                //    night = "";
                                //}
                                //duration = dt_prescription.Rows[k]["duration_unit"].ToString();
                                if (duration.Trim() == "0" || duration.Trim() == "")
                                {
                                    duration = "";

                                }
                                else
                                {
                                    duration = dt_prescription.Rows[k]["duration_unit"].ToString() + "" + dt_prescription.Rows[k]["duration_period"].ToString();
                                }
                                if (morning == "0" && noon == "0" && night == "0")
                                {
                                    dataGridView1.Rows.Add("0", dt_prescription.Rows[k]["drug_type"].ToString() + " " + dt_prescription.Rows[k]["drug_name"].ToString() + " " + dt_prescription.Rows[k]["strength"].ToString() + " " + "", "", duration, dt_prescription.Rows[k]["add_instruction"].ToString(), "");

                                }
                                else
                                {
                                    dataGridView1.Rows.Add("0", dt_prescription.Rows[k]["drug_type"].ToString() + " " + dt_prescription.Rows[k]["drug_name"].ToString() + " " + dt_prescription.Rows[k]["strength"].ToString() + " " + dt_prescription.Rows[k]["strength_gr"].ToString(), morning + " - " + noon + " - " + night, duration, dt_prescription.Rows[k]["add_instruction"].ToString(), "");

                                }
                                //dataGridView1.Rows[k].Cells[].Value = "0";





                                dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.blank;
                                dataGridView1.Rows[i].Height = 30;
                            }
                        }
                        i = i + 1;
                        dataGridView1.Rows.Add("0", "Prescribed by Dr." + dt_pre_main.Rows[j]["doctor_name"].ToString(), "", "", "");
                        dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                        dataGridView1.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                        dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.blank;
                        i = i + 1;
                        dataGridView1.Rows.Add("0", "", "", "", "");
                        dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.blank;
                        i = i + 1;
                    }
                }
                else
                {
                    dataGridView1.Rows.Clear();
                }
                if (dataGridView1.Rows.Count <= 0)
                {
                    int x = (panel3.Size.Width - Label_NORecordFound.Size.Width) / 2;
                    Label_NORecordFound.Location = new Point(x, Label_NORecordFound.Location.Y);
                    Label_NORecordFound.Show();
                    dataGridView1.Hide();
                }
                else
                {
                    Label_NORecordFound.Hide();
                    dataGridView1.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Load_MainGrid_showmore(DataTable dt_pre_main)
        {
            try
            {
                int i = 0;
                int row = dataGridView1.Rows.Count;
                if (dt_pre_main.Rows.Count > 0)
                {
                    for (int j = 0; j < dt_pre_main.Rows.Count; j++)
                    {
                        dataGridView1.Rows.Add("0", String.Format("{0:dddd,d MMMM , yyyy}", Convert.ToDateTime(dt_pre_main.Rows[j]["date"].ToString())), "", "", "");
                        dataGridView1.Rows[row].Cells[5].Value = PappyjoeMVC.Properties.Resources.blank;
                        dataGridView1.Rows[row].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                        dataGridView1.Rows[row].Cells[1].Style.ForeColor = Color.DarkGreen;
                        row = row + 1;
                        dataGridView1.Rows.Add(dt_pre_main.Rows[j]["id"].ToString(), "Drug", "Frequency", "Duration", "Instruction", "");
                        dataGridView1.Rows[row].Cells[1].Style.BackColor = Color.LightGray;
                        dataGridView1.Rows[row].Cells[2].Style.BackColor = Color.LightGray;
                        dataGridView1.Rows[row].Cells[3].Style.BackColor = Color.LightGray;
                        dataGridView1.Rows[row].Cells[4].Style.BackColor = Color.LightGray;
                        dataGridView1.Rows[row].Cells[5].Style.BackColor = Color.LightGray;
                        dataGridView1.Rows[row].Cells[1].Style.ForeColor = Color.Black;
                        dataGridView1.Rows[row].Cells[2].Style.ForeColor = Color.Black;
                        dataGridView1.Rows[row].Cells[3].Style.ForeColor = Color.Black;
                        dataGridView1.Rows[row].Cells[4].Style.ForeColor = Color.Black;
                        dataGridView1.Rows[row].Cells[5].Value = PappyjoeMVC.Properties.Resources.Bill;
                        System.Data.DataTable dt_prescription = this.cntrl.prescription_details(dt_pre_main.Rows[j]["id"].ToString());
                        if (dt_prescription.Rows.Count > 0)
                        {
                            for (int k = 0; k < dt_prescription.Rows.Count; k++)
                            {
                                i = i + 1;
                                string morning = "", duration = "";
                                string noon = "";
                                string night = "";
                                string a1 = dt_prescription.Rows[k]["morning"].ToString();
                                string[] b1 = a1.Split('.');
                                int right1 = int.Parse(b1[1]);
                                morning = Convert.ToString(int.Parse(b1[0]));
                                if (right1 != 0) { morning = morning + "." + Convert.ToString(int.Parse(b1[1])); }
                                string a2 = dt_prescription.Rows[k]["noon"].ToString();
                                string[] b2 = a2.Split('.');
                                int right2 = int.Parse(b2[1]);
                                noon = Convert.ToString(int.Parse(b2[0]));
                                if (right2 != 0) { noon = noon + "." + Convert.ToString(int.Parse(b2[1])); }
                                string a3 = dt_prescription.Rows[k]["night"].ToString();
                                string[] b3 = a3.Split('.');
                                int right3 = int.Parse(b3[1]);
                                night = Convert.ToString(int.Parse(b3[0]));
                                if (right3 != 0) { night = night + "." + Convert.ToString(int.Parse(b3[1])); }
                                duration = dt_prescription.Rows[k]["duration_unit"].ToString();
                                //updation
                                //if (morning == "0" && noon =="0" && night == "0"  )
                                //{
                                //    morning = "";
                                //    noon = "";
                                //    night = "";
                                //}
                                //duration = dt_prescription.Rows[k]["duration_unit"].ToString();
                                if (duration.Trim() == "0" || duration.Trim() == "")
                                {
                                    duration = "";

                                }
                                else
                                {
                                    duration = dt_prescription.Rows[k]["duration_unit"].ToString() + "" + dt_prescription.Rows[k]["duration_period"].ToString();
                                }
                                if (morning == "0" && noon == "0" && night == "0")
                                {
                                    dataGridView1.Rows.Add("0", dt_prescription.Rows[k]["drug_type"].ToString() + " " + dt_prescription.Rows[k]["drug_name"].ToString() + " " + dt_prescription.Rows[k]["strength"].ToString() + " " + "", "", duration, dt_prescription.Rows[k]["add_instruction"].ToString(), "");

                                }
                                else
                                {
                                    dataGridView1.Rows.Add("0", dt_prescription.Rows[k]["drug_type"].ToString() + " " + dt_prescription.Rows[k]["drug_name"].ToString() + " " + dt_prescription.Rows[k]["strength"].ToString() + " " + dt_prescription.Rows[k]["strength_gr"].ToString(), morning + " - " + noon + " - " + night, duration, dt_prescription.Rows[k]["add_instruction"].ToString(), "");

                                }
                                //dataGridView1.Rows[k].Cells[].Value = "0";





                                dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.blank;
                                dataGridView1.Rows[i].Height = 30;
                            }
                        }
                        i = i + 1;
                        dataGridView1.Rows.Add("0", "Prescribed by Dr." + dt_pre_main.Rows[j]["doctor_name"].ToString(), "", "", "");
                        dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                        dataGridView1.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                        dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.blank;
                        i = i + 1;
                        dataGridView1.Rows.Add("0", "", "", "", "");
                        dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.blank;
                        i = i + 1;
                    }
                }
                else
                {
                    dataGridView1.Rows.Clear();
                }
                if (dataGridView1.Rows.Count <= 0)
                {
                    int x = (panel3.Size.Width - Label_NORecordFound.Size.Width) / 2;
                    Label_NORecordFound.Location = new Point(x, Label_NORecordFound.Location.Y);
                    Label_NORecordFound.Show();
                    dataGridView1.Hide();
                }
                else
                {
                    Label_NORecordFound.Hide();
                    dataGridView1.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
