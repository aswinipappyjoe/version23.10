using PappyjoeMVC.Controller;
using PappyjoeMVC.Model;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;


namespace PappyjoeMVC.View
{
    public partial class LabWorks : Form
    {
        public static LabWorks form;
        public LabWorks()
        {
            InitializeComponent();
            form = this;
        }
        StreamWriter sWrite;
        public int k, Dexist = 0;
        string logo_name = "";
        string path = "";
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
        LabWorks_controller ctrlr = new LabWorks_controller();
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
        public static string strphone = "";
        public string name = "", result = "", units = "", text = "", smsName = "", smsPass = "";
        public string addr = "", loc = "", gen = "", patient_id = "", age = "", sexage = "", Apppath = "", doctor_id = "", typ = "", n = "", workiddental = "", workname = "", strPatientName = "", mob_number = "",  contact_no = "",  clinicn = "", strclinicname = "", strStreet = "", stremail = "", strwebsite = "";

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
        user_privillage_model privi_mdl = new user_privillage_model();
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
        private void labelprofile_Click(object sender, EventArgs e)
        {
            var form2 = new Patient_Profile_Details();
            form2.doctor_id = doctor_id;
            form2.patient_id = patient_id;
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

        private void labelallpatient_Click(object sender, EventArgs e)
        {
            var form2 = new Patients();
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

        public void reciept_count()
        {
            DataTable Payments = db.table("select receipt_no,amount_paid,invoice_no,procedure_name,mode_of_payment from tbl_payment where pt_id='" + patient_id + "'");
            if (Payments.Rows.Count > 0)
            {
                lb_Reciepts_cnt.Text = Payments.Rows.Count.ToString();
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
        public void practicedetails(DataTable dtp)
        {
            if (dtp.Rows.Count > 0)
            {
                clinicn = dtp.Rows[0]["name"].ToString();
                strclinicname = clinicn.Replace("¤", "'");
                strphone = dtp.Rows[0]["contact_no"].ToString();
                strStreet = dtp.Rows[0]["street_address"].ToString();
                stremail = dtp.Rows[0]["email"].ToString();
                strwebsite = dtp.Rows[0]["website"].ToString();
            }
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
           DataTable dtp = this.ctrlr.practicedetails();
            practicedetails(dtp);
           
            DataTable print = this.ctrlr.printsettings_details();
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
                printdetails();
            }
        }

        private void pb_AppntmntAdd_Click(object sender, EventArgs e)
        {
            try
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
                        MessageBox.Show("There is No Privilege to Add Appointments", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

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

        private void lbl_NursesNotes_Click(object sender, EventArgs e)
        {
            //var form2 = new Nurses_Notes();
            //form2.doctor_id = doctor_id;
            //form2.patient_id = patient_id;
            //openform(form2);

        }
        public void nurse_count()
        {
            System.Data.DataTable dt_cf_main = this.nurse_cntrl.dt_cf_main(patient_id);
            if (dt_cf_main.Rows.Count > 0)
            {
                lb_Nurses_Notes.Text = dt_cf_main.Rows.Count.ToString();
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            var form = new PappyjoeMVC.View.Dentalwork();
            form.patient_id = patient_id;
            form.doctor_id = doctor_id;
            form.workid = workiddental;
            form.ShowDialog();
            DataTable dt = this.ctrlr.Getdata(patient_id);
            Getdata(dt);
        }
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DataTable sm = this.ctrlr.smsinfo();
            smsinfo(sm);     
        }
        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            var form = new PappyjoeMVC.View.LabResultEntry();
            form.patient_id = patient_id;
            form.doctor_id = doctor_id;
            form.workid = workiddental;
            form.flagup = "0";
            form.flag = "1";
            form.Show();
        }
        public void tbmain(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    string type = "LNG";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataTable tbshade = this.ctrlr.tbshade_(patient_id, dt.Rows[i][0].ToString(), workiddental);
                        text = text + " [" + dt.Rows[i][1].ToString() + "]  TEST --  RESULT/ UNITS --  NV --";
                        for (int j = 0; j < tbshade.Rows.Count; j++)
                        {
                          
                            if (dt.Rows[i][1].ToString() == tbshade.Rows[j][0].ToString())
                            {

                                text = text + " (" + (j + 1) + ")" + tbshade.Rows[j][1].ToString() + " :" + tbshade.Rows[j][2].ToString() + " " + tbshade.Rows[j][5].ToString();
                            }
                           
                        }
                    }
                    DataTable dtp = this.ctrlr.practicedetails();
                    practicedetails(dtp);
                    string res = this.ctrlr.SendSMS(smsName, smsPass, mob_number, "Dear " + strPatientName + ",  Your Lab Test Result : " + text + "--- Regards " + strclinicname + "," + strphone, type);
                    if (res == "SMS message(s) sent")
                    { MessageBox.Show("Laboratory Result  send successfully", "success", MessageBoxButtons.OK, MessageBoxIcon.None); }
                    else
                    { MessageBox.Show("Laboratory Result  sending failed,Please try again later !", "Failed", MessageBoxButtons.OK, MessageBoxIcon.None); }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        public void smsinfo(DataTable sms)
        {
            try
            {
                if (sms.Rows.Count > 0)
                {
                    smsName = sms.Rows[0]["smsName"].ToString();
                    smsPass = sms.Rows[0]["smsPass"].ToString();
                }
                if (smsName != "" && smsPass != "")
                {
                    DataTable dt = this.ctrlr.tbmain(patient_id, workiddental);
                    tbmain(dt);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        public void seltype(string type)
        {
            try
            {
                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                if (type == "Dental")
                {
                    printtoolStripMenuItem2.Visible = false;
                    toolStripMenuItem1.Visible = false;
                    addLabOrderToolStripMenuItem.Visible = true;
                    sendSMSToolStripMenuItem.Visible = false;
                    workiddental = dataGridView1_treatment_paln.Rows[k].Cells[2].Value.ToString();
                }
                else
                {
                    printtoolStripMenuItem2.Visible = true;
                    toolStripMenuItem1.Visible = true;
                    sendSMSToolStripMenuItem.Visible = true;
                    workiddental = dataGridView1_treatment_paln.Rows[k].Cells[2].Value.ToString();
                    addLabOrderToolStripMenuItem.Visible = false;
                }
                contextMenuStrip1.Tag = k;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void dataGridView1_treatment_paln_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex > -1)
                {
                    if (dataGridView1_treatment_paln.Rows[e.RowIndex].Cells[5].Value.ToString() == "Medical")
                    {
                        var form2 = new LabResultEntry();
                        form2.patient_id = patient_id;
                        form2.doctor_id = doctor_id;
                        form2.Text = "Result View";
                        form2.label41.Text = " RESULT VIEW";
                        string workid = dataGridView1_treatment_paln.Rows[e.RowIndex].Cells[2].Value.ToString();
                        form2.workid = workid;
                        form2.flagup = "1";
                        form2.flag = "0";
                        form2.Show();
                    }
                    else if (dataGridView1_treatment_paln.Rows[e.RowIndex].Cells[5].Value.ToString() == "Dental")
                    {
                        workiddental = dataGridView1_treatment_paln.Rows[e.RowIndex].Cells[2].Value.ToString();
                        if (workiddental != "")
                        {
                            var form2 = new PappyjoeMVC.View.Dentalwork();
                            form2.patient_id = patient_id;
                            form2.doctor_id = doctor_id;
                            string workid = dataGridView1_treatment_paln.Rows[e.RowIndex].Cells[2].Value.ToString();
                            form2.workid = workid;
                            form2.flag = "1";
                            form2.Show();
                        }
                        else
                        {
                            MessageBox.Show("Details of dental lab work is avialable in lab tracking", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                if (e.ColumnIndex == 1 && e.RowIndex > -1)
                {
                    k = e.RowIndex;
                    //label1.Text = dataGridView1_treatment_paln.Rows[e.RowIndex].Cells[4].Value.ToString();
                    string type = this.ctrlr.seltype(patient_id, dataGridView1_treatment_paln.Rows[e.RowIndex].Cells[2].Value.ToString());
                    seltype(type);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
        private void button1_Click(object sender, EventArgs e)
        {
            var form2 = new PappyjoeMVC.View.Add_Labwork();
            form2.doctor_id = doctor_id;
            form2.patient_id = patient_id;
            openform(form2);
        }
        public void printdetails()
        {
            try
            {
                //string clinicn = "";
                //string Clinic = "";
                //string streetaddress = "";
                //string contact_no = "";
                //string str_locality = "";
                //string str_pincode = "";
                //string str_email = "";
                //string str_website = "";

                string strfooter1 = "";
                string strfooter2 = "";
                string strfooter3 = "";
                string header1 = "";
                string header2 = "";
                string header3 = "";
                string check = "";
                string header_image = "";
                string message = "Did you want Header on Print?";
                string caption = "Verification";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Data.DataTable dtp = this.ctrlr.Get_Practice_details();
                    if (dtp.Rows.Count > 0)
                    {
                        //clinicn = dtp.Rows[0]["name"].ToString();
                        //Clinic = clinicn.Replace("¤", "'");
                        //streetaddress = dtp.Rows[0]["street_address"].ToString();
                        //str_locality = dtp.Rows[0]["locality"].ToString();
                        ////str_pincode = dtp.Rows[0]["pincode"].ToString();
                        //contact_no = dtp.Rows[0]["contact_no"].ToString();
                        ////str_email = dtp.Rows[0]["email"].ToString();
                        //str_website = dtp.Rows[0]["website"].ToString();
                        path = dtp.Rows[0]["path"].ToString();
                        logo_name = path;
                    }
                    System.Data.DataTable print = this.ctrlr.printsettings();
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
                }
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                System.IO.StreamWriter sWrite = new System.IO.StreamWriter(Apppath + "\\RESULT_print.html");
                sWrite.WriteLine("<html>");
                sWrite.WriteLine("<head>");
                sWrite.WriteLine("</head>");
                sWrite.WriteLine("<body>");
                sWrite.WriteLine("<br>");
                if(check=="Yes")
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
                                sWrite.WriteLine("<table align='center' style='width:900px;border: 1px ;border-collapse: collapse;'>");
                                sWrite.WriteLine("<tr>");
                                sWrite.WriteLine("<td width='30px' height='50px' align='left' rowspan='3'><img src='" + Appath + "\\" + logo_name + "'  style='width:70px;height:70px;'></td>  ");
                                sWrite.WriteLine("<td width='870px' align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=4><b>&nbsp;" + header1 + " </font><br><FONT  COLOR=black  face='Segoe UI' SIZE=2>&nbsp;" + header2 + "<br>&nbsp;" + header3 + "  </b></font></td>");
                                sWrite.WriteLine("</tr>");
                                sWrite.WriteLine("</table>");
                            }
                            else
                            {
                                sWrite.WriteLine("<table align='center' style='width:900px;border: 1px ;border-collapse: collapse;'>");
                                sWrite.WriteLine("<tr>");
                                sWrite.WriteLine("<td  align='left' height='20px'><FONT  COLOR=black  face='Segoe UI' SIZE=7><b>&nbsp;" + header1 + "</b></font></td></tr>");
                                sWrite.WriteLine("<tr><td  align='left' height='20px'><FONT COLOR=black FACE='Segoe UI' SIZE=4><b>&nbsp;" + header2 + "</b></font></td></tr>");
                                sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;" + header3 + "</b></font></td></tr>");
                                sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                                sWrite.WriteLine("</table>");
                            }
                        }
                        else
                        {
                            sWrite.WriteLine("<table align='center' style='width:900px;border: 1px ;border-collapse: collapse;'>");
                            sWrite.WriteLine("<tr>");
                            sWrite.WriteLine("<td  align='left' height='20px'><FONT  COLOR=black  face='Segoe UI' SIZE=7><b>&nbsp;" + header1 + "</b></font></td></tr>");
                            sWrite.WriteLine("<tr><td  align='left' height='20px'><FONT COLOR=black FACE='Segoe UI' SIZE=4><b>&nbsp;" + header2 + "</b></font></td></tr>");
                            sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;" + header3 + "</b></font></td></tr>");
                            sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                            sWrite.WriteLine("</table>");
                        }
                    }
                    else
                    {
                        sWrite.WriteLine("<table align='center' style='width:900px;border: 1px ;border-collapse: collapse;'>");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td  align='left' height='20px'><FONT  COLOR=black  face='Segoe UI' SIZE=7><b>&nbsp;" + header1 + "</b></font></td></tr>");
                        sWrite.WriteLine("<tr><td  align='left' height='20px'><FONT COLOR=black FACE='Segoe UI' SIZE=4><b>&nbsp;" + header2 + "</b></font></td></tr>");
                        sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;" + header3 + "</b></font></td></tr>");
                        sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                        sWrite.WriteLine("</table>");
                    }
                 }
                else
                {
                    sWrite.WriteLine("<table align='center' style='width:900px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td  align='left' height='20px'><FONT  COLOR=black  face='Segoe UI' SIZE=5></font></td></tr>");
                    sWrite.WriteLine("<tr><td  align='left' height='20px'><FONT COLOR=black FACE='Segoe UI' SIZE=3></font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                    sWrite.WriteLine("</table>");
                }
                }
                else
                {
                    string path1 = this.db.server();
                    if (File.Exists(path1 + "\\" + header_image))
                    {
                        sWrite.WriteLine("<table align='center'   style='width:9000px;border: 1px ;border-collapse: collapse;' >");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td  align='left'><img src='" + path1 + "\\" + header_image + "' width='1200' height='80';'></td>  ");
                        sWrite.WriteLine("</tr>");
                        sWrite.WriteLine("</table>");
                    }
                }
                sWrite.WriteLine("<table align='center' style='width:900px;border: 1px ;border-collapse: collapse;'>");
                //sWrite.WriteLine("<tr><td align='left'  ><hr/></td></tr>");
                sWrite.WriteLine("</table>");
                int Dexist = 0;
                string sexage = ""; string age = ""; string age2 = "";
                string address = "";
                

                string strNote = "";
                string strreview = "NO";
                string strreview_date = "";
                DataTable tbmain_doctr = this.ctrlr.get_test_doctor(patient_id, workiddental);//     tbmain(patient_id, workiddental);
                DataTable docname = this.ctrlr.docname(tbmain_doctr.Rows[0]["dr_id"].ToString());
                string doctorname = "";
                if (docname.Rows.Count > 0)
                {
                    doctorname = Convert.ToString(docname.Rows[0]["doctor_name"].ToString());
                }
                System.Data.DataTable dt1 = this.ctrlr.patient_details(patient_id);
                if (dt1.Rows.Count > 0)
                {
                    sWrite.WriteLine("<table align='center' style='width:900px;border: 1px ;border-collapse: collapse;'>");
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
                    sWrite.WriteLine(" <td align='left' height=25><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3><b>" + dt1.Rows[0]["pt_name"].ToString() + "</b><i> (" + sexage + age+")</i></font></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>Patient Id:" + dt1.Rows[0]["pt_id"].ToString() + " </font></td>");
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
                        sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>" + address + " </font></td>");
                        sWrite.WriteLine(" </tr>");
                    }
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>" + dt1.Rows[0]["primary_mobile_number"].ToString() + " </font></td>");
                    sWrite.WriteLine(" </tr>");
                    if (dt1.Rows[0]["email_address"].ToString() != "")
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>" + dt1.Rows[0]["email_address"].ToString() + " </font></td>");
                        sWrite.WriteLine(" </tr>");
                    }
                    sWrite.WriteLine("<tr><td colspan=2><hr></td></tr>");

                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' width='400px' height='30px'><FONT FACE='Geneva, Segoe UI' SIZE=3><FONT COLOR=black >By</FONT> :Dr. <b>" + doctorname + " </b></font></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("</table>");
                    sWrite.WriteLine("<table align='center'   style='width:900px;border: 1px ;border-collapse: collapse;' >");
                    sWrite.WriteLine("<tr>");
                    //sWrite.WriteLine("<td><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=5>R</font><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>x&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</font><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=5>Lab</FONT></td>");
                    sWrite.WriteLine("<td width=250px></td>");
                    DataTable tb_doctor = this.ctrlr.get_test_doctor(patient_id, workiddental);// tbmain(patient_id, workiddental);
                    //DataTable tbshade = this.ctrlr.tbshade_(patient_id, tbmai.Rows[0][0].ToString(), workiddental);// '" + Convert.ToDateTime(date).ToString("yyyy-MM-dd") + "'DateTime.Parse(strdate).ToString("dd MMM yyyy")
                    sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3> <FONT COLOR=black>Date : </FONT>" + Convert.ToDateTime(tb_doctor.Rows[0]["date"].ToString()).ToString("dd/MM/yyyy") + "</font></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("</table>");


                }//
                DataTable tbmain  = this.ctrlr.tbmain(patient_id, workiddental);
                DataTable tbmain_1 = this.ctrlr.get_test_not_main(patient_id, workiddental);
                sWrite.WriteLine("<table align='center'style='width:900px;border: 1px ;border-collapse: collapse;' >");
                sWrite.WriteLine("<tr >");
                sWrite.WriteLine("<td align='left' width='200px' height='30'><FONT COLOR=black FACE=' Segoe UI' SIZE=3><b>&nbsp;TEST</b></font></td>");
                sWrite.WriteLine("<td align='left' width='200px' ><FONT COLOR=black FACE=' Segoe UI' SIZE=3><b>&nbsp;RESULT</b></font></td>");
                //sWrite.WriteLine("<td align='left' width='200px' ><FONT COLOR=black FACE=' Segoe UI' SIZE=3><b>&nbsp;UNITS</b> </font></td>");
                sWrite.WriteLine("<td align='left' width='300px' colspan='3' ><FONT COLOR=black FACE=' Segoe UI' SIZE=3><b>&nbsp;NORMAL VALUE</b></font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>"); string tm = "",t="";
                sWrite.WriteLine("<table  align='center' width=900px>");

                if (tbmain.Rows.Count>0)
                {
                    for (int i = 0; i < tbmain.Rows.Count; i++)
                    {
                        DataTable tbshade = this.ctrlr.tbshade(patient_id, tbmain.Rows[i][0].ToString(), workiddental, tbmain.Rows[i]["template_id"].ToString());
                        DataTable tempname = this.ctrlr.dt(tbmain.Rows[i]["template_id"].ToString());//tbmain.Rows[i][1].ToString()
                        if (tempname.Rows.Count > 0)
                        {
                            tm = tempname.Rows[0][0].ToString();
                        }
                        if (tm == t && t != "")
                        {

                        }
                        else
                        {

                            sWrite.WriteLine("<tr>");
                            sWrite.WriteLine("<td align='center' colspan='4' ><b><Font COLOR=black FACE='Segoe UI' SIZE=4><u>" + tm + "</u></font></b></td>");
                            t = tm;
                            sWrite.WriteLine("</tr>");
                        }
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='left' colspan='4' ><b><Font COLOR=black FACE='Segoe UI' SIZE=4><u>" + tbmain.Rows[i][1].ToString() + "</u></font></b></td>");
                        sWrite.WriteLine("</tr>");
                        sWrite.WriteLine("</table>");
                        sWrite.WriteLine("<table align='center' style='width:900px;border: 1px ;border-collapse: collapse;' >");
                        DataTable dt_type = this.ctrlr.test_type(tbmain.Rows[i]["maintest_id"].ToString(), tempname.Rows[0][1].ToString());//tbshade.Rows[j][3].ToString()

                        for (int j = 0; j < tbshade.Rows.Count; j++)
                        {

                            sWrite.WriteLine("<tr>");
                            if (tbmain.Rows[i][1].ToString() == tbshade.Rows[j][0].ToString())
                            {
                                //sWrite.WriteLine("<td align='left' width='200px' height='30'><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + tbshade.Rows[j][1].ToString() + " </font></b></td>");
                                sWrite.WriteLine("<td align='left' width='200px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;" + tbshade.Rows[j][2].ToString() + " </font></b></td>");
                                if (tbshade.Rows[j]["Units"].ToString() == "NIL")
                                {
                                    sWrite.WriteLine("<td align='left' width='200px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>&nbsp;" + tbshade.Rows[j]["results"].ToString() + " </font></b></td>");
                                }
                                else
                                {
                                    sWrite.WriteLine("<td align='left' width='200px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>&nbsp;" + tbshade.Rows[j]["results"].ToString() + tbshade.Rows[j]["Units"].ToString() + " </font></b></td>");

                                }
                                //if (tbshade.Rows[j]["NormalValueM"].ToString() != "" || tbshade.Rows[j]["NormalValueF"].ToString() != "")
                                //{
                                //    sWrite.WriteLine("<td align='left' width='300px' colspan='3' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; [ M(" + tbshade.Rows[j]["NormalValueM"].ToString() + ") F('" + tbshade.Rows[j]["NormalValueF"].ToString() + "')]</font></b></td>");
                                //}
                                //else
                                //{
                                //    sWrite.WriteLine("<td align='left' valign='top'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> </font></b></td>");
                                //}


                                if (tbshade.Rows[j]["NormalValueM"].ToString() != "" && tbshade.Rows[j]["NormalValueF"].ToString() != "")
                                {
                                    sWrite.WriteLine("<td align='left' width='300px' colspan='3' ><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;  " + tbshade.Rows[j]["NormalValueM"].ToString() + " , " + tbshade.Rows[j]["NormalValueF"].ToString() + "</font></b></td>");
                                }
                                else if (tbshade.Rows[j]["NormalValueM"].ToString() == "" && tbshade.Rows[j]["NormalValueF"].ToString() == "")

                                {
                                    sWrite.WriteLine("<td align='left' valign='top'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3> </font></b></td>");
                                }
                                else
                                {
                                    if (tbshade.Rows[j]["NormalValueM"].ToString() != "")
                                    {
                                        sWrite.WriteLine("<td align='left' width='300px' colspan='3' ><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp; " + tbshade.Rows[j]["NormalValueM"].ToString() + "</font></b></td>");

                                    }
                                    else if (tbshade.Rows[j]["NormalValueF"].ToString() != "")
                                    {
                                        sWrite.WriteLine("<td align='left' width='300px' colspan='3' ><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp; " + tbshade.Rows[j]["NormalValueF"].ToString() + "</font></b></td>");

                                    }
                                }

                            }
                        }
                    }
                    //if (tbmain.Rows[i]["Template_Type"].ToString() == "Template")
                    //{
                    //DataTable dt_main = this.ctrlr.tbmain(patient_id, workiddental);

                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='center' colspan='4' ><b><Font COLOR=black FACE='Segoe UI' SIZE=3><u></u></font></b></td>");
                    //t = tm;
                    sWrite.WriteLine("</tr>");


                    //}
                }
                if (tbmain_1.Rows.Count>0)
                {
                  
                    //if (tbmain.Rows[i][1].ToString() == tbshade.Rows[j][0].ToString())
                    //{
                    for(int i=0;i< tbmain_1.Rows.Count;i++)
                    {
                        sWrite.WriteLine("<tr>");
                        DataTable dt_test = this.ctrlr.tb_shade_test(patient_id, tbmain_1.Rows[i]["test_id"].ToString(), tbmain_1.Rows[i]["id"].ToString());
                        if (dt_test.Rows.Count > 0)
                        {
                            sWrite.WriteLine("<td align='left' width='200px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;" + dt_test.Rows[0][0].ToString() + " </font></b></td>");
                            if (dt_test.Rows[0]["Units"].ToString() == "NIL")
                            {
                                sWrite.WriteLine("<td align='left' width='200px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>&nbsp;" + dt_test.Rows[0]["results"].ToString() + " </font></b></td>");
                            }
                            else
                            {
                                sWrite.WriteLine("<td align='left' width='200px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>&nbsp;" + dt_test.Rows[0]["results"].ToString() + dt_test.Rows[0]["Units"].ToString() + " </font></b></td>");

                            }
                            if (dt_test.Rows[0]["NormalValueM"].ToString() != "" && dt_test.Rows[0]["NormalValueF"].ToString() != "")
                            {
                                sWrite.WriteLine("<td align='left' width='300px' colspan='3' ><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;" + dt_test.Rows[0]["NormalValueM"].ToString() + " ,"+ dt_test.Rows[0]["NormalValueF"].ToString() + "</font></b></td>");
                            }
                            else if (dt_test.Rows[0]["NormalValueM"].ToString() == "" && dt_test.Rows[0]["NormalValueF"].ToString() == "")

                            {
                                sWrite.WriteLine("<td align='left' valign='top'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> </font></b></td>");
                            }
                            else
                            {
                                if(dt_test.Rows[0]["NormalValueM"].ToString() != "" )
                                {
                                    sWrite.WriteLine("<td align='left' width='300px' colspan='3' ><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp; " + dt_test.Rows[0]["NormalValueM"].ToString() + "</font></b></td>");

                                }
                                else if(dt_test.Rows[0]["NormalValueF"].ToString() != "")
                                {
                                    sWrite.WriteLine("<td align='left' width='300px' colspan='3' ><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp; " + dt_test.Rows[0]["NormalValueF"].ToString() + "</font></b></td>");

                                }
                            }

                        }
                        sWrite.WriteLine("</tr>");
                    }
                  
                }   
                sWrite.WriteLine("<tr><td align='left' colspan=8><hr/></td></tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<table align='center'   style='width:900px;border: 1px ;border-collapse: collapse;' >");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='right' height='22'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>&nbsp;</font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='right' height='22'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>&nbsp;</font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='right' height='22'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>&nbsp;Lab Technician</font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='center' height='22'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>&nbsp;" + strfooter1 + "</font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='center' height='22'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>&nbsp;" + strfooter2 + "</font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='center' height='22'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>&nbsp;" + strfooter3 + "</font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<script>window.print();</script>");
                sWrite.WriteLine("</body >");
                sWrite.WriteLine("</html>");
                sWrite.Close();
                System.Diagnostics.Process.Start(Apppath + "\\RESULT_print.html");
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        //public void printdetails()
        //{
        //    try
        //    {
        //        //string clinicn = "";
        //        //string Clinic = "";
        //        //string streetaddress = "";
        //        //string contact_no = "";
        //        //string str_locality = "";
        //        //string str_pincode = "";
        //        //string str_email = "";
        //        //string str_website = "";

        //        string strfooter1 = "";
        //        string strfooter2 = "";
        //        string strfooter3 = "";
        //        string header1 = "";
        //        string header2 = "";
        //        string header3 = "";
        //        string message = "Did you want Header on Print?";
        //        string caption = "Verification";
        //        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
        //        DialogResult result;
        //        result = MessageBox.Show(message, caption, buttons);
        //        if (result == System.Windows.Forms.DialogResult.Yes)
        //        {
        //            System.Data.DataTable dtp = this.ctrlr.Get_Practice_details();
        //            if (dtp.Rows.Count > 0)
        //            {
        //                //clinicn = dtp.Rows[0]["name"].ToString();
        //                //Clinic = clinicn.Replace("¤", "'");
        //                //streetaddress = dtp.Rows[0]["street_address"].ToString();
        //                //str_locality = dtp.Rows[0]["locality"].ToString();
        //                ////str_pincode = dtp.Rows[0]["pincode"].ToString();
        //                //contact_no = dtp.Rows[0]["contact_no"].ToString();
        //                ////str_email = dtp.Rows[0]["email"].ToString();
        //                //str_website = dtp.Rows[0]["website"].ToString();
        //                path = dtp.Rows[0]["path"].ToString();
        //                logo_name = path;
        //            }
        //            System.Data.DataTable print = this.ctrlr.printsettings();
        //            if (print.Rows.Count > 0)
        //            {
        //                header1 = print.Rows[0]["header"].ToString();
        //                header2 = print.Rows[0]["left_text"].ToString();
        //                header3 = print.Rows[0]["right_text"].ToString();
        //                strfooter1 = print.Rows[0]["fullwidth_context"].ToString();
        //                strfooter2 = print.Rows[0]["left_sign"].ToString();
        //                strfooter3 = print.Rows[0]["right_sign"].ToString();
        //            }
        //        }



        //        string Apppath = System.IO.Directory.GetCurrentDirectory();
        //        System.IO.StreamWriter sWrite = new System.IO.StreamWriter(Apppath + "\\RESULT.html");
        //        sWrite.WriteLine("<html>");
        //        sWrite.WriteLine("<head>");
        //        sWrite.WriteLine("</head>");
        //        sWrite.WriteLine("<body>");
        //        sWrite.WriteLine("<br>");
        //        if (includeheader == "1")
        //        {
        //            if (includelogo == "1")
        //            {
        //                if (logo != null || logo_name != "")
        //                {
        //                    string Appath = System.IO.Directory.GetCurrentDirectory();
        //                    if (File.Exists(Appath + "\\" + logo_name))
        //                    {
        //                        sWrite.WriteLine("<table align='center' style='width:900px;border: 1px ;border-collapse: collapse;'>");
        //                        sWrite.WriteLine("<tr>");
        //                        sWrite.WriteLine("<td width='30px' height='50px' align='left' rowspan='3'><img src='" + Appath + "\\" + logo_name + "'  style='width:70px;height:70px;'></td>  ");
        //                        sWrite.WriteLine("<td width='870px' align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=4><b>&nbsp;" + header1 + " </font><br><FONT  COLOR=black  face='Segoe UI' SIZE=2>&nbsp;" + header2 + "<br>&nbsp;" + header3 + "  </b></font></td>");
        //                        sWrite.WriteLine("</tr>");
        //                        sWrite.WriteLine("</table>");
        //                    }
        //                    else
        //                    {
        //                        sWrite.WriteLine("<table align='center' style='width:900px;border: 1px ;border-collapse: collapse;'>");
        //                        sWrite.WriteLine("<tr>");
        //                        sWrite.WriteLine("<td  align='left' height='20px'><FONT  COLOR=black  face='Segoe UI' SIZE=5><b>&nbsp;" + header1 + "</b></font></td></tr>");
        //                        sWrite.WriteLine("<tr><td  align='left' height='20px'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;" + header2 + "</b></font></td></tr>");
        //                        sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2><b>&nbsp;" + header3 + "</b></font></td></tr>");
        //                        sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
        //                        sWrite.WriteLine("</table>");
        //                    }
        //                }
        //                else
        //                {
        //                    sWrite.WriteLine("<table align='center' style='width:900px;border: 1px ;border-collapse: collapse;'>");
        //                    sWrite.WriteLine("<tr>");
        //                    sWrite.WriteLine("<td  align='left' height='20px'><FONT  COLOR=black  face='Segoe UI' SIZE=5><b>&nbsp;" + header1 + "</b></font></td></tr>");
        //                    sWrite.WriteLine("<tr><td  align='left' height='20px'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;" + header2 + "</b></font></td></tr>");
        //                    sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2><b>&nbsp;" + header3 + "</b></font></td></tr>");
        //                    sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
        //                    sWrite.WriteLine("</table>");
        //                }
        //            }
        //            else
        //            {
        //                sWrite.WriteLine("<table align='center' style='width:900px;border: 1px ;border-collapse: collapse;'>");
        //                sWrite.WriteLine("<tr>");
        //                sWrite.WriteLine("<td  align='left' height='20px'><FONT  COLOR=black  face='Segoe UI' SIZE=5><b>&nbsp;" + header1 + "</b></font></td></tr>");
        //                sWrite.WriteLine("<tr><td  align='left' height='20px'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;" + header2 + "</b></font></td></tr>");
        //                sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2><b>&nbsp;" + header3 + "</b></font></td></tr>");
        //                sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
        //                sWrite.WriteLine("</table>");
        //            }
        //        }
        //        else
        //        {
        //            sWrite.WriteLine("<table align='center' style='width:900px;border: 1px ;border-collapse: collapse;'>");
        //            sWrite.WriteLine("<tr>");
        //            sWrite.WriteLine("<td  align='left' height='20px'><FONT  COLOR=black  face='Segoe UI' SIZE=5></font></td></tr>");
        //            sWrite.WriteLine("<tr><td  align='left' height='20px'><FONT COLOR=black FACE='Segoe UI' SIZE=3></font></td></tr>");
        //            sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td></tr>");
        //            sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
        //            sWrite.WriteLine("</table>");
        //        }
        //        sWrite.WriteLine("<table align='center' style='width:900px;border: 1px ;border-collapse: collapse;'>");
        //        //sWrite.WriteLine("<tr><td align='left'  ><hr/></td></tr>");
        //        sWrite.WriteLine("</table>");
        //        int Dexist = 0;
        //        string sexage = "";
        //        string address = "";
        //        address = "";
        //        string strNote = "";
        //        string strreview = "NO";
        //        string strreview_date = "";
        //        DataTable tbmain = this.ctrlr.tbmain(patient_id, workiddental);
        //        DataTable docname = this.ctrlr.docname(tbmain.Rows[0]["dr_id"].ToString());
        //        string doctorname = "";
        //        if (docname.Rows.Count>0)
        //        {
        //            doctorname = Convert.ToString(docname.Rows[0]["doctor_name"].ToString());
        //        }
        //        System.Data.DataTable dt1 = this.ctrlr.patient_details(patient_id);
        //        if (dt1.Rows.Count > 0)
        //        {
        //            sWrite.WriteLine("<table align='center' style='width:900px;border: 1px ;border-collapse: collapse;'>");
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
        //            sWrite.WriteLine(" <td align='left' height=25><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>" + dt1.Rows[0]["pt_name"].ToString() + "</b><i> (" + sexage + ")</i></font></td>");
        //            sWrite.WriteLine(" </tr>");
        //            sWrite.WriteLine("<tr>");
        //            sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Patient Id:" + dt1.Rows[0]["pt_id"].ToString() + " </font></td>");
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
        //                sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + address + " </font></td>");
        //                sWrite.WriteLine(" </tr>");
        //            }
        //            sWrite.WriteLine("<tr>");
        //            sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + dt1.Rows[0]["primary_mobile_number"].ToString() + " </font></td>");
        //            sWrite.WriteLine(" </tr>");
        //            if (dt1.Rows[0]["email_address"].ToString() != "")
        //            {
        //                sWrite.WriteLine("<tr>");
        //                sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + dt1.Rows[0]["email_address"].ToString() + " </font></td>");
        //                sWrite.WriteLine(" </tr>");
        //            }
        //            sWrite.WriteLine("<tr><td colspan=2><hr></td></tr>");

        //            sWrite.WriteLine("<tr>");
        //            sWrite.WriteLine("<td align='left' width='400px' height='30px'><FONT FACE='Geneva, Segoe UI' SIZE=2><FONT COLOR=black >By</FONT> :Dr. <b>" + doctorname + " </b></font></td>");
        //            sWrite.WriteLine("</tr>");
        //            sWrite.WriteLine("</table>");
        //            sWrite.WriteLine("<table align='center'   style='width:900px;border: 1px ;border-collapse: collapse;' >");
        //            sWrite.WriteLine("<tr>");
        //            //sWrite.WriteLine("<td><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=5>R</font><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>x&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</font><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=5>Lab</FONT></td>");
        //            sWrite.WriteLine("<td width=250px></td>");
        //            DataTable tbmai = this.ctrlr.tbmain(patient_id, workiddental);
        //            DataTable tbshade = this.ctrlr.tbshade(patient_id, tbmai.Rows[0][0].ToString(), workiddental);
        //            sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> <FONT COLOR=black>Date : </FONT>" + tbshade.Rows[0]["date"].ToString() + "</font></td>");
        //            sWrite.WriteLine("</tr>");
        //            sWrite.WriteLine("</table>");


        //        }//
        //        //DataTable tbmain = this.ctrlr.tbmain(patient_id, workiddental);
        //            for (int i = 0; i < tbmain.Rows.Count; i++)
        //            {
        //            string tm = "";
        //            DataTable tbshade = this.ctrlr.tbshade(patient_id, tbmain.Rows[i][0].ToString(), workiddental);
        //            DataTable tempname = this.ctrlr.dt(tbmain.Rows[i][1].ToString());
        //            if (tempname.Rows.Count>0)
        //            {
        //                tm = tempname.Rows[0][0].ToString();
        //            }
        //            sWrite.WriteLine("<table  align='center' width=900px>");
        //            sWrite.WriteLine("<tr>");
        //            sWrite.WriteLine("<td align='left' colspan='4' ><b><Font COLOR=black FACE='Segoe UI' SIZE=3><u>" + tm + "</u></font></b></td>");
        //            sWrite.WriteLine("</tr>");
        //            sWrite.WriteLine("<tr>");
        //            sWrite.WriteLine("<td align='left' colspan='4' ><b><Font COLOR=black FACE='Segoe UI' SIZE=4><u>" + tbmain.Rows[i][1].ToString() + "</u></font></b></td>");
        //            sWrite.WriteLine("</tr>");
        //            sWrite.WriteLine("</table>");
        //            sWrite.WriteLine("<table align='center'style='width:900px;border: 1px ;border-collapse: collapse;' >");
        //            sWrite.WriteLine("<tr >");
        //                sWrite.WriteLine("<td align='left' width='200px' height='30'><FONT COLOR=black FACE=' Segoe UI' SIZE=3><b>&nbsp;TEST</b></font></td>");
        //                sWrite.WriteLine("<td align='left' width='200px' ><FONT COLOR=black FACE=' Segoe UI' SIZE=3><b>&nbsp;RESULT</b></font></td>");
        //                sWrite.WriteLine("<td align='left' width='200px' ><FONT COLOR=black FACE=' Segoe UI' SIZE=3><b>&nbsp;UNITS</b> </font></td>");
        //                sWrite.WriteLine("<td align='left' width='300px' colspan='3' ><FONT COLOR=black FACE=' Segoe UI' SIZE=3><b>&nbsp;NORMAL VALUE</b></font></td>");
        //                sWrite.WriteLine("</tr>");
        //                sWrite.WriteLine("<table align='center' style='width:900px;border: 1px ;border-collapse: collapse;' >");
        //            for (int j = 0; j < tbshade.Rows.Count; j++)
        //            {
        //                sWrite.WriteLine("<tr>");
        //                if (tbmain.Rows[i][1].ToString() == tbshade.Rows[j][0].ToString())
        //                {
        //                    sWrite.WriteLine("<td align='left' width='200px' height='30'><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + tbshade.Rows[j][1].ToString() + " </font></b></td>");
        //                    sWrite.WriteLine("<td align='left' width='200px'><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + tbshade.Rows[j][2].ToString() + " </font></b></td>");
        //                    sWrite.WriteLine("<td align='left' width='200px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + tbshade.Rows[j][5].ToString() + " </font></b></td>");
        //                    if (tbshade.Rows[j][3].ToString() != "")
        //                    {
        //                        sWrite.WriteLine("<td align='left' width='300px' colspan='3' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; [NV: M(" + tbshade.Rows[j][3].ToString() + ") F('" + tbshade.Rows[j][4].ToString() + "')]</font></b></td>");
        //                    }
        //                    else
        //                    {
        //                        sWrite.WriteLine("<td align='left' valign='top'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> </font></b></td>");
        //                    }
        //                }
        //            }
        //        }

        //        sWrite.WriteLine("<tr><td align='left' colspan=8><hr/></td></tr>");
        //        sWrite.WriteLine("</table>");
        //        sWrite.WriteLine("<table align='center'   style='width:900px;border: 1px ;border-collapse: collapse;' >");
        //        sWrite.WriteLine("<tr>");
        //        sWrite.WriteLine("<td align='right' height='22'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>&nbsp;</font></td>");
        //        sWrite.WriteLine("</tr>");
        //        sWrite.WriteLine("<tr>");
        //        sWrite.WriteLine("<td align='right' height='22'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>&nbsp;</font></td>");
        //        sWrite.WriteLine("</tr>");
        //        sWrite.WriteLine("<tr>");
        //        sWrite.WriteLine("<td align='right' height='22'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>&nbsp;Lab Technician</font></td>");
        //        sWrite.WriteLine("</tr>");
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
        //        System.Diagnostics.Process.Start(Apppath + "\\RESULT.html");
        //    }
        //    catch (Exception ex)
        //    { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        //}
        private void LabWorks_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable rs_patients = this.ctrlr.Get_Patient_Details(patient_id);
                Get_Patient_Details(rs_patients);
                DataTable clinicname = this.ctrlr.Get_Practice_details();
                if (clinicname.Rows.Count > 0) 
                {
                    string clinicn = "";
                    clinicn = clinicname.Rows[0][0].ToString();
                    path = clinicname.Rows[0]["path"].ToString();
                    contact_no = clinicname.Rows[0][2].ToString();
                    string dr = this.ctrlr.Get_DoctorName(doctor_id);
                    Get_DoctorName(dr);
                }
                DataTable dt = this.ctrlr.Getdata(patient_id);
                Getdata(dt);
                dataGridView1_treatment_paln.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
                dataGridView1_treatment_paln.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridView1_treatment_paln.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 10, FontStyle.Regular);
                dataGridView1_treatment_paln.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView1_treatment_paln.EnableHeadersVisualStyles = false;
                foreach (DataGridViewColumn cl in dataGridView1_treatment_paln.Columns)
                {
                    cl.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView1_treatment_paln.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView1_treatment_paln.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1_treatment_paln.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1_treatment_paln.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1_treatment_paln.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1_treatment_paln.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1_treatment_paln.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView1_treatment_paln.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1_treatment_paln.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1_treatment_paln.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1_treatment_paln.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1_treatment_paln.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                foreach (DataGridViewColumn cl in dataGridView1_treatment_paln.Columns)
                {
                    cl.SortMode = DataGridViewColumnSortMode.NotSortable;
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
            { MessageBox.Show(ex.Message, "Error!...", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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

        public static string apntid = "";
        public void Get_Patient_Details(DataTable rs_patients)
        {
            try
            {
                if (rs_patients.Rows[0]["pt_name"].ToString() != "")
                {
                    linkLabel_Name.Text = rs_patients.Rows[0]["pt_name"].ToString();
                    strPatientName = rs_patients.Rows[0]["pt_name"].ToString();
                    mob_number = rs_patients.Rows[0]["primary_mobile_number"].ToString();
                }
                if (rs_patients.Rows[0]["pt_id"].ToString() != "")
                {
                    linkLabel_id.Text = rs_patients.Rows[0]["pt_id"].ToString();
                }
                gen = rs_patients.Rows[0]["gender"].ToString();
                age = rs_patients.Rows[0]["age"].ToString();
                addr = rs_patients.Rows[0]["street_address"].ToString();
                loc = rs_patients.Rows[0]["locality"].ToString();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        public void Get_DoctorName(string docnam)
        {
            try
            {
                if (path != "")
                {
                    string curFile = this.ctrlr.server() + "\\Pappyjoe_utilities\\Logo\\" + path;
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
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        public void Getdata(DataTable tbShade)
        {
            try
            {
                dataGridView1_treatment_paln.DataSource = tbShade;
                if (dataGridView1_treatment_paln.Rows.Count <= 0)
                {
                    int x = (panel4.Size.Width - label1.Size.Width) / 2;
                    label1.Location = new Point(x, label1.Location.Y);
                    label1.Show();
                }
                else
                {
                    label1.Hide();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
