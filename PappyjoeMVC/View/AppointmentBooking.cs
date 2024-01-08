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
using XtremeCalendarControl;
using System.Net.Mail;
using PappyjoeMVC.Model;
using System.Globalization;

namespace PappyjoeMVC.View
{
    public partial class AppointmentBooking : Form
    {
        Connection db = new Connection();
        public string doctor_id = "0";
        public string gpl_app_id = "0";
        string app_Doctor_id = "0";
        public string patient_id = "0";
        public string patient_name;
        public string gender;
        public int status1 = 0;
        Booking_controller cntrl = new Booking_controller();
        Communication_Setting_controller ccntrl = new Communication_Setting_controller();
        user_privillage_model privi_mdl = new user_privillage_model();
        private System.Windows.Forms.Label lblShowTimeAs;
        private System.Windows.Forms.Button cmdRecurrence;
        public CalendarEvent EditingEvent;
        public Boolean IsNewEvent = true;
        internal System.Windows.Forms.ComboBox cmbReminder;
        public bool dashboard_flag = false;
        static public AppointmentBooking Instance;
        public string send_on_day { get; set; }
        public string send_before_day { get; set; }
        public string day_time { get; set; }
        public string before_time { get; set; }
        public string schedTime = "";
        public string ApptRemndrSMStime { get; set; }
        public bool editflag = false; MainCalendar_Controller mcntrl = new MainCalendar_Controller();
        public AppointmentBooking()
        {
            Instance = this;
            InitializeComponent();
            InitializeControls();
        }

        public AppointmentBooking(bool cal)
        {
            Instance = this;
            InitializeComponent();
             InitializeControl_dashboard();
        }
        private void InitializeControl_dashboard()
        {
            cmbLabel.Items.Insert(0, "None");
            cmbLabel.Items.Insert(1, "Important");
            cmbLabel.Items.Insert(2, "Business");
            cmbLabel.Items.Insert(3, "Personal");
            cmbLabel.Items.Insert(4, "Vacation");
            cmbLabel.Items.Insert(5, "Must Attend");
            cmbLabel.Items.Insert(6, "Travel Required");
            cmbLabel.Items.Insert(7, "Needs Preparation");
            cmbLabel.Items.Insert(8, "Birthday");
            cmbLabel.Items.Insert(9, "Anniverserary");
            cmbLabel.Items.Insert(10, "Phone Call");
            cmbLabel.SelectedIndex = 0;
            cmbShowTimeAs.Items.Insert(0, "Free");
            cmbShowTimeAs.Items.Insert(1, "Tentative");
            cmbShowTimeAs.Items.Insert(2, "Busy");
            cmbShowTimeAs.Items.Insert(3, "Out of Office");
            cmbShowTimeAs.SelectedIndex = 0;
                string strslot = "";
                strslot = this.mcntrl.get_slot();
                DateTime dtHours = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                for (int cnt = 0; cnt < 288; cnt++)
                {
                    cmbStartTime.Items.Insert(cnt, dtHours.ToString("h:mm tt"));
                    cmbEndTime.Items.Insert(cnt, dtHours.ToString("h:mm tt"));
                    dtHours = dtHours.AddMinutes(Convert.ToDouble(strslot));
                }
                cmbStartTime.SelectedIndex = 10;
                cmbEndTime.SelectedIndex = 11;
        }
        private void InitializeControls()
        {
            cmbLabel.Items.Insert(0, "None");
            cmbLabel.Items.Insert(1, "Important");
            cmbLabel.Items.Insert(2, "Business");
            cmbLabel.Items.Insert(3, "Personal");
            cmbLabel.Items.Insert(4, "Vacation");
            cmbLabel.Items.Insert(5, "Must Attend");
            cmbLabel.Items.Insert(6, "Travel Required");
            cmbLabel.Items.Insert(7, "Needs Preparation");
            cmbLabel.Items.Insert(8, "Birthday");
            cmbLabel.Items.Insert(9, "Anniverserary");
            cmbLabel.Items.Insert(10, "Phone Call");
            cmbLabel.SelectedIndex = 0;
            cmbShowTimeAs.Items.Insert(0, "Free");
            cmbShowTimeAs.Items.Insert(1, "Tentative");
            cmbShowTimeAs.Items.Insert(2, "Busy");
            cmbShowTimeAs.Items.Insert(3, "Out of Office");
            cmbShowTimeAs.SelectedIndex = 0;
             DateTime dtHours = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                for (int cnt = 0; cnt < 288; cnt++)
                {
                    cmbStartTime.Items.Insert(cnt, dtHours.ToString("h:mm tt"));
                    cmbEndTime.Items.Insert(cnt, dtHours.ToString("h:mm tt"));
                    dtHours = dtHours.AddMinutes(5);
                }
            cmbStartTime.SelectedIndex =  11;
                cmbEndTime.SelectedIndex = 11;
        }
        private class CalReminderMinutes
        {
            public Int32 m_nMinutes;
            public String m_strMinutes;
            public CalReminderMinutes(Int32 nMinutes, String strMinutes)
            {
                m_nMinutes = nMinutes;
                m_strMinutes = strMinutes;
            }
            public CalReminderMinutes()
            {
                m_nMinutes = 0;
                m_strMinutes = "";
            }
            public override String ToString()
            {
                return m_strMinutes;
            }
        }
        private void AppointmentBooking_Load(object sender, EventArgs e)
        {
            try
            {
                checkBox2.Refresh();
                if (doctor_id != "1")
                {

                    string i;
                    i = privi_mdl.Add_privillege(doctor_id);// add_patients(doctor_id);
                    if (int.Parse(i) > 0)
                    {
                        DataTable dt_ = this.cntrl.doctor_name(doctor_id);
                        if (dt_.Rows.Count > 0)
                        {
                            combodoctor.DisplayMember = "doctor_name";
                            combodoctor.ValueMember = "id";
                            combodoctor.DataSource = dt_;

                        }
                        else
                        {
                            DataTable dt = this.cntrl.get_all_doctorname();
                            combodoctor.DisplayMember = "doctor_name";
                            combodoctor.ValueMember = "id";
                            combodoctor.DataSource = dt;
                        }
                    }
                }
                else
                {
                    DataTable dt = this.cntrl.get_all_doctorname();
                    combodoctor.DisplayMember = "doctor_name";
                    combodoctor.ValueMember = "id";
                    combodoctor.DataSource = dt;
                }
                if(editflag==true)
                {
                    DataTable dt_ = this.cntrl.doctor_name(app_Doctor_id);

                    combodoctor.Text = dt_.Rows[0]["doctor_name"].ToString();
                    editflag = false;
                }
                ///new code
                if (dashboard_flag == true)
                {
                    panel2.Visible = true;
                    lab_edit.Visible = false;
                }
                else
                {
                    panel2.Visible = false;
                    lab_edit.Visible = true;
                }
                listProcedure.Visible = false;
            }
            catch (Exception ex)
            {

            }
        }

        private void lab_edit_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void lab_edit_MouseEnter(object sender, EventArgs e)
        {
            lab_edit.ForeColor = Color.FromArgb(5, 32, 59);
        }

        private void lab_edit_MouseLeave(object sender, EventArgs e)
        {
            lab_edit.ForeColor = Color.FromArgb(0, 102, 204);
        }

        private void txt_p_name_KeyUp(object sender, KeyEventArgs e)
        {
            label14.Visible = false;
            DataTable dt = this.cntrl.search_patient(txt_p_name.Text);
            list_p_details.DisplayMember = "pt_name";
            list_p_details.ValueMember = "id";
            list_p_details.DataSource = dt;
            list_p_details.Show();
            patient_id = "0";
            if (e.KeyCode == Keys.Down && list_p_details.Items.Count > 0)
            {
                list_p_details.Focus();
            }
            else if (e.KeyCode == Keys.Enter && list_p_details.Items.Count > 0)
            {
                patient_id = list_p_details.GetItemText(list_p_details.SelectedValue);
                DataTable dt_p = this.cntrl.Getpat_MobNamme(patient_id);
                Fill_search_patient(dt_p);
                list_p_details.Hide();
            }
            if (dt.Rows.Count <= 0)
            {
                list_p_details.Hide();
            }
        }

        private void txt_p_id_KeyUp(object sender, KeyEventArgs e)
        {
        }

        public void Fill_search_patient(DataTable dt_p)
        {
            if (dt_p.Rows.Count > 0)
            {
                txt_p_name.Text = dt_p.Rows[0][0].ToString();
                lab_p_name.Text = dt_p.Rows[0][0].ToString() + "(" + dt_p.Rows[0][1].ToString() + ")";
                lab_p_gndr.Text = dt_p.Rows[0][2].ToString();
                lab_p_ph.Text = dt_p.Rows[0][3].ToString();
                patient_name = dt_p.Rows[0][0].ToString();
                lab_p_email.Text = dt_p.Rows[0][4].ToString();
                panel1.Hide();
            }
        }

        private void list_p_details_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void list_p_details_Click(object sender, EventArgs e)
        {
            patient_id = "0";
            if (list_p_details.Items.Count > 0)
            {
                patient_id = list_p_details.GetItemText(list_p_details.SelectedValue);
                DataTable dt_p = this.cntrl.Getpat_MobNamme(patient_id);
                Fill_search_patient(dt_p);
                list_p_details.Hide();
            }
        }
       
        private bool cal;

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if(dashboard_flag==true)
            { 
                DateTime _dash_StartT;
                try
                {
                    string DatetimeNow = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                    int pos1 = 2;
                    char replacement1 = '/';
                    DatetimeNow = DatetimeNow.Remove(pos1, 1).Insert(pos1, replacement1.ToString());
                    int po1 = 5;
                    char replacemen1 = '/';
                    DatetimeNow = DatetimeNow.Remove(po1, 1).Insert(po1, replacemen1.ToString());

                    string neworold = "0"; DataTable dtpSearch = new DataTable();
                    DataTable dtb = this.cntrl.patient_details(txt_p_name.Text);
                    Appointment_for_newPAtient(dtb);
                    if (cmbStartTime.SelectedIndex <= cmbEndTime.SelectedIndex && combodoctor.Text != "")
                    {
                        if (patient_id != "0")
                        {
                            string dr_id = combodoctor.GetItemText(combodoctor.SelectedValue);
                            string dr_color = "0";
                            long app_id = 0;
                            DateTime Dateonly = DateTime.Now.Date;
                            string dr_mobile = "0";
                            string dr_email = "";
                            DataTable dt_d = this.cntrl.Get_calenderColor(dr_id);
                            if (dt_d.Rows.Count > 0)
                            {
                                dr_color = dt_d.Rows[0][0].ToString();
                                dr_mobile = dt_d.Rows[0][1].ToString();
                                dr_email = dt_d.Rows[0][2].ToString();
                            }
                            string diff1 = "0";
                            DateTime StartTime1 = DateTime.Now.Date;
                            ////
                            DateTime StartTime, EndTime;
                            Dateonly = Convert.ToDateTime(DateTime.Now.ToLocalTime());
                            string start_time = "", end_time = "";
                            var dt = dpStartTimeDate.Value.Date.ToString("yyyy-MM-dd");
                            _dash_StartT = Convert.ToDateTime(dt);
                            EndTime = Convert.ToDateTime(dt);
                            start_time = Convert.ToDateTime(cmbStartTime.SelectedItem.ToString()).ToString("HH: mm:ss");
                            end_time = Convert.ToDateTime(cmbEndTime.SelectedItem.ToString()).ToString("HH: mm:ss");
                            var start_date_time = string.Format("{0} {1}", dt, start_time);
                            var end_date_time = string.Format("{0} {1}", dt, end_time);
                            _dash_StartT = Convert.ToDateTime(start_time);
                            EndTime = Convert.ToDateTime(end_time);
                            var diff = EndTime.Subtract(_dash_StartT);
                            diff1 = Convert.ToString(diff.Minutes);
                            if (diff.Hours.ToString() != "")
                            {
                                int valh = diff.Hours;
                                valh = valh * 60;
                                int valm = diff.Minutes;
                                diff1 = Convert.ToString(valh + valm);
                            }
                            else
                            {
                                diff1 = Convert.ToString(diff.Minutes);
                            }
                            string Name = "";
                            DataTable doctor = this.cntrl.get_doctor_login(PappyjoeMVC.Model.Connection.MyGlobals.Doctor_id);
                            if (doctor.Rows.Count > 0)
                            {
                                if (doctor.Rows[0]["login_type"].ToString() == "doctor")
                                {
                                    Name = "Dr ";
                                }

                                Name = Name + doctor.Rows[0]["doctor_name"].ToString();
                            }
                            dtpSearch = this.cntrl.patient_details_byname(txt_p_name.Text);
                            this.cntrl.insappointment(Convert.ToDateTime(Dateonly).ToString("yyyy-MM-dd"), Convert.ToDateTime(start_date_time).ToString("yyyy-MM-dd HH:mm"), diff1, txtDescription.Text, patient_id, txt_p_name.Text, dr_id, txt_p_mobile.Text, txt_p_email.Text, txtProcedure.Text, Name);
                                DateTime Timeonly = DateTime.Now;
                              MessageBox.Show("Appointment saved successfully", "Appointment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DataTable dt_a = this.cntrl.appointmentId();
                            if (dt_a.Rows.Count > 0)
                            {
                                app_id = Convert.ToInt32(dt_a.Rows[dt_a.Rows.Count - 1][0].ToString());
                            }
                            ///
                            DateTime Dateonly1 = DateTime.Now;
                            if (IsNewEvent)
                            {
                                string clinic = "", locality = "", contact_no = "";
                                System.Data.DataTable clinicname = this.cntrl.clinicdetails();
                                clinic = clinicname.Rows[0]["name"].ToString();
                                contact_no = clinicname.Rows[0]["contact_no"].ToString();
                                string smsName = "", smsPass = "", scount = "";
                                DataTable sms = this.cntrl.smsdetails();
                                DataTable smscount = this.ccntrl.getsmscnt();
                                if (sms.Rows.Count > 0)
                                {
                                    smsName = sms.Rows[0]["smsName"].ToString();
                                    smsPass = sms.Rows[0]["smsPass"].ToString();
                                }
                                if (smscount.Rows[0]["sms"].ToString() != "")
                                {
                                    scount = PappyjoeMVC.Model.EncryptionDecryption.Decrypt(smscount.Rows[0]["sms"].ToString(), "ch3lSeAW0n2o2!C1");
                                    if (Convert.ToInt32(scount) > 5)
                                    {
                                        //---------------Appoitment count SMS for Doctor-------------------
                                        if (sms.Rows[0]["doc_appoCntSMS"].ToString() == "1")
                                        {
                                            DataTable DocRemTme = this.cntrl.get_DocRemsmsTime();
                                            if (DocRemTme.Rows.Count > 0)
                                            {
                                                SMS_model a = new SMS_model();
                                                string ApptCount = ""; string SendTime = "";
                                                string number = "91" + dr_mobile;
                                                string DocRem = DocRemTme.Rows[0]["doctorApptCountsmsTime"].ToString();
                                                string ST = DateTime.Today.AddDays(1).ToShortDateString() + " " + DocRem;
                                                DateTime DT = DateTime.Parse(ST);
                                                SendTime = DT.ToString("dd/MM/yyyy hh:mm:ss tt");
                                                int pos = 2;
                                                char replacement = '/';
                                                SendTime = SendTime.Remove(pos, 1).Insert(pos, replacement.ToString());
                                                int po = 5;
                                                char replacemen = '/';
                                                SendTime = SendTime.Remove(po, 1).Insert(po, replacemen.ToString());

                                                DataTable doc = this.cntrl.getDocAppntmnt(dr_id, DateTime.Today.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Today.AddDays(1).AddHours(23).AddMinutes(59).AddSeconds(59).ToString("yyyy-MM-dd HH:mm:ss"));
                                                if (doc.Rows.Count > 0)
                                                {
                                                    ApptCount = doc.Rows.Count.ToString();
                                                }
                                                string text = "Dear " + combodoctor.Text + ", today you have " + ApptCount + " appointment in" + clinic + "CLIINI";
                                                DataTable RIDa = this.cntrl.get_docNotifInfor(dr_id);
                                                if (RIDa.Rows.Count == 0)
                                                {
                                                    this.cntrl.insert_DocId(dr_id);
                                                }
                                                DataTable RID = this.cntrl.get_docNotifInfor(dr_id);
                                                if (RID.Rows.Count > 0)
                                                {
                                                    string webrespo = RID.Rows[0]["webRespo"].ToString();
                                                    if (webrespo != "")
                                                    {
                                                        string deleDocrem = this.cntrl.DeleteDocRem(smsName, smsPass, webrespo);
                                                        if (deleDocrem != "")
                                                        {
                                                            string smspatnt3 = this.cntrl.SendSMS(smsName, smsPass, number, text, "DRTOMS", dr_id.ToString(), SendTime, DatetimeNow);
                                                            if (smspatnt3 != "")
                                                            {
                                                                this.cntrl.updateWebresp(dr_id, smspatnt3, Dateonly.ToString("yyyy-MM-dd"));
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        string smspatnt3 = this.cntrl.SendSMS(smsName, smsPass, number, text, "DRTOMS", dr_id.ToString(), SendTime, DatetimeNow);
                                                        if (smspatnt3 != "")
                                                        {
                                                            {
                                                                this.cntrl.updateWebresp(dr_id, smspatnt3, Dateonly.ToString("yyyy-MM-dd"));
                                                            }
                                                        }
                                                    }
                                                }
                                                DataTable RIDb = this.cntrl.get_docNotifInfor(dr_id);
                                                if (RID.Rows[0]["date"].ToString() != Dateonly.ToString())
                                                {
                                                    scount = Convert.ToString(Convert.ToInt32(scount) - 1);
                                                }
                                            }
                                        }
                                        else if (sms.Rows[0]["doc_appoCntSMS"].ToString() == "0")
                                        {
                                            DataTable RIDa = this.cntrl.get_docNotifInfor(dr_id);
                                            if (RIDa.Rows.Count == 0)
                                            {
                                                this.cntrl.insert_DocId(dr_id);
                                            }
                                            DataTable RID = this.cntrl.get_docNotifInfor(dr_id);
                                            if (RID.Rows.Count > 0)
                                            {
                                                string webrespo = RID.Rows[0]["webRespo"].ToString();
                                                if (webrespo != "")
                                                {
                                                    string smspatnt3 = ""; string date = "";
                                                    string deleDocrem = this.cntrl.DeleteDocRem(smsName, smsPass, webrespo);
                                                    this.cntrl.updateWebresp(dr_id, smspatnt3, date);
                                                }
                                            }
                                        }
                                        string ApptDate1 = ""; string ApptDate2 = "";
                                        if (checkBox1.Checked)
                                        {
                                            string text = "";
                                            DataTable pat = this.cntrl.Get_Patient_Details(patient_id);
                                            DataTable smslanguage = this.cntrl.sms_lang();
                                            DataTable grtpatremt = this.cntrl.getPatRemTime();
                                            if (grtpatremt.Rows.Count > 0)
                                            {
                                                schedTime = grtpatremt.Rows[0]["patientRemsmsTime"].ToString();
                                                ApptDate1 = _dash_StartT.ToShortDateString() + " " + schedTime;
                                                ApptDate2 = _dash_StartT.ToShortDateString() + " " + cmbStartTime.Text;
                                                System.DateTime date1 = DateTime.Parse(ApptDate1);
                                                DateTime date2 = DateTime.Parse(ApptDate2);
                                                System.TimeSpan time = date2.Subtract(date1);
                                                string TT = _dash_StartT.ToShortDateString() + " " + time;
                                                DateTime DT = DateTime.Parse(TT);
                                                ApptRemndrSMStime = DT.ToString("dd/MM/yyyy hh:mm:ss tt");
                                                int pos = 2;
                                                char replacement = '/';
                                                ApptRemndrSMStime = ApptRemndrSMStime.Remove(pos, 1).Insert(pos, replacement.ToString());
                                                int po = 5;
                                                char replacemen = '/';
                                                ApptRemndrSMStime = ApptRemndrSMStime.Remove(po, 1).Insert(po, replacemen.ToString());
                                            }
                                            if (pat.Rows.Count > 0)
                                            {
                                                SMS_model a = new SMS_model();
                                                string number = "91" + pat.Rows[0]["primary_mobile_number"].ToString();
                                                string type = "LNG";
                                                if (neworold == "1")
                                                {
                                                    if (sms.Rows[0]["pat_welcSMS"].ToString() == "1")
                                                    {
                                                        a.SendSMS(smsName, smsPass, number, "Dear " + pat.Rows[0]["pt_name"].ToString() + " welcome to " + clinic + "," + contact_no, type);
                                                        scount = Convert.ToString(Convert.ToInt32(scount) - 1);
                                                    }
                                                }
                                                if (smslanguage.Rows.Count > 0)
                                                {
                                                    string smslang = smslanguage.Rows[0]["Prescription_lang"].ToString();
                                                    DataTable smstemplate = this.cntrl.smstemplate(smslang, pat.Rows[0]["pt_name"].ToString(), txtProcedure.Text, _dash_StartT.ToString("dd/MM/yyyy"), cmbStartTime.Text, combodoctor.Text, clinic, contact_no);
                                                    if (smstemplate.Rows.Count > 0)
                                                    {
                                                        text = smstemplate.Rows[0]["Template"].ToString();
                                                        a.SendSMS(smsName, smsPass, number, text, type);
                                                        this.cntrl.save_Pt_SMS(patient_id, pat.Rows[0]["pt_name"].ToString(), txtProcedure.Text, _dash_StartT.ToString("dd/MM/yyyy"), cmbStartTime.Text, combodoctor.Text);
                                                        scount = Convert.ToString(Convert.ToInt32(scount) - 1);
                                                    }//
                                                    else
                                                    {
                                                        text = "Dear " + pat.Rows[0]["pt_name"].ToString() + " " + "Your appointment for " + txtProcedure.Text + " has been confirmed at " + _dash_StartT.ToString("dd/MM/yyyy") + " " + cmbStartTime.Text + " with " + "Dr " + combodoctor.Text + " Regards " + clinic + "," + contact_no;
                                                        a.SendSMS(smsName, smsPass, number, text, type);
                                                        this.cntrl.save_Pt_SMS(patient_id, pat.Rows[0]["pt_name"].ToString(), txtProcedure.Text, _dash_StartT.ToString("dd/MM/yyyy"), cmbStartTime.Text, combodoctor.Text);
                                                        scount = Convert.ToString(Convert.ToInt32(scount) - 1);
                                                    }
                                                }
                                                string sddsds = "";
                                                ////-------------For Reminder SMS patient----------
                                                if (day_time != null)
                                                {
                                                    if (dpStartTimeDate.Value > DateTime.Now.Date)
                                                    {
                                                       
                                                    }
                                                }
                                                if (schedTime != "")
                                                {
                                                    if (sms.Rows[0]["pat_appoRemSMS"].ToString() == "1")
                                                    {
                                                       
                                                        text = "Dear " + pat.Rows[0]["pt_name"].ToString() + ", " + "Today you have an appointment at " + clinic + " on " + _dash_StartT.ToString("dd/MM/yyyy") + " " + cmbStartTime.Text + " for " + txtProcedure.Text + " .Regards  " + clinic + "," + contact_no + "CLIINI";
                                                        string smspatnt3 = this.cntrl.SendSMS(smsName, smsPass, number, text, "DRTOMS", patient_id.ToString(), ApptRemndrSMStime, DatetimeNow);
                                                        //a.SendSMS(smsName, smsPass, number, text, "DRTOMS", patient_id.ToString(), ApptRemndrSMStime, DatetimeNow);
                                                        scount = Convert.ToString(Convert.ToInt32(scount) - 1);

                                                        //}
                                                    }
                                                }

                                            }
                                        }
                                        if (checkBox3.Checked)
                                        {
                                            if (dr_mobile != "0")
                                            {
                                                string text = "";
                                                //string smsName = "", smsPass = "";
                                                SMS_model a = new SMS_model();
                                                string number = "91" + dr_mobile;
                                                string type = "LNG";
                                                text = "You have an appointment on " + dpStartTimeDate.Value.ToShortDateString() + " " + cmbStartTime.Text + " With " + txt_p_name.Text + " for " + txtProcedure.Text + " at " + clinic + "," + contact_no;
                                                a.SendSMS(smsName, smsPass, number, text, type);
                                                scount = Convert.ToString(Convert.ToInt32(scount) - 1);
                                                //--------For Remainder SMS doctor-----------
                                                if (day_time != null)
                                                {
                                                    if (dpStartTimeDate.Value > DateTime.Now.Date)
                                                    {
                                                      
                                                    }
                                                }
                                                if (before_time != null)
                                                {
                                                    if (dpStartTimeDate.Value > DateTime.Now.Date)
                                                    {
                                                       
                                                    }
                                                }
                                            }
                                        }
                                        string Encrypt = PappyjoeMVC.Model.EncryptionDecryption.Encrypt(scount, "ch3lSeAW0n2o2!C1");
                                        this.ccntrl.smsCount(Encrypt);
                                        panel1.Visible = true;
                                        panel2.Visible = true;
                                        combodoctor.SelectedIndex = 0;
                                        combocategory.Text = "";
                                        txtDescription.Text = "";
                                        checkBox2.Checked = false; checkBox4.Checked = false;
                                        checkBox1.Checked = true; checkBox3.Checked = true;
                                        dpStartTimeDate.Value = DateTime.Now.Date; txtProcedure.Text = "";
                                        txt_p_name.Text = "";
                                        cmbStartTime.SelectedIndex = 0;
                                        cmbEndTime.SelectedIndex = 0;
                                        list_p_details.Visible = false;
                                        listProcedure.Visible = false;
                                        combocategory.SelectedIndex = 0;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Insufficient balance to send SMS. Please contact Pappyjoe to add SMS Balance", "SMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                //if (checkBox2.Checked)
                                //{
                                    string email1 = "", emailName1 = "", emailPass1 = "";
                                    string query = "select email_address,pt_name from tbl_patient where id='" + patient_id + "'";

                                    DataTable sr = this.cntrl.getpatemail(patient_id);
                                    if (sr.Rows.Count > 0)
                                    {
                                    email1 = sr.Rows[0]["email_address"].ToString();
                                        DataTable mail = this.cntrl.send_email();
                                        if (mail.Rows.Count > 0)
                                        {
                                        emailName1 = mail.Rows[0]["emailName"].ToString();
                                        emailPass1 = mail.Rows[0]["emailPass"].ToString();

                                            try
                                            {
                                                string sr1 = "<table align='center' style='width:700px;border: 1px solid ;border-collapse: collapse; background: #EAEAEA; height:500px'><tr><td  align='left' height='27'><FONT  color='#666666'  face='Arial' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Appointment Reminder:" + dpStartTimeDate.Value.ToString("ddd, dd MMM yyyy") + ' ' + cmbStartTime.Text + " @ " + clinic + "</font></td></tr><tr><td  align='left' height='400px'><table  height='423' align='center' style='width:600px; background: #FFFFFF; height:400px'><tr><td  align='left' height='6px'><FONT  color='#000000'  face='Arial' SIZE=6>" + clinic + "</font></td></tr><tr><td  align='left' height='1px' bgcolor='#666666'></td></tr><tr><td  align='left' height='62' valign='bottom'><FONT  color='#000000'  face='Arial' SIZE=3>Good morning <b>" + sr.Rows[0]["pt_name"].ToString() + "</b></font></td></tr> <tr><td align='left' height='197' valign='top'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Just to remind you about your appointment at " + clinic + ".<table><tbody><tr><td width='188' height='31' valign='bottom' align='right'>WHEN :</td><td width='30' height='31' valign='bottom' align='right'></td><td width='358' valign='bottom'> <strong>" + dpStartTimeDate.Value.ToString("ddd, dd MMM yyyy") + ' ' + cmbStartTime.Text + "</strong></td>  </tr><tr><td height='76' valign='top'  align='right'>WHERE :</td><td width='30' height='31' valign='bottom' align='right'></td><td  valign='top'>" + locality + "</td></tr></tbody></table> For any queries, contact us at : " + contact_no + "</td>  </tr><tr><td  align='left' height='1px' bgcolor='#666666'></td></tr> <tr><td height='25'  align='right' valign='bottom'>Powered by&nbsp;&nbsp; </td></tr> <tr><td height='81'  align='right' valign='top'><img src='http://pappyjoe.com/assets/images/pappyjoe-logo.PNG' alt='pappyjoe official logo'>&nbsp;&nbsp;</td></tr></table></td></tr></table>";
                                                MailMessage message = new MailMessage();
                                                message.From = new MailAddress(email1);
                                                message.To.Add(email1);
                                                message.BodyEncoding = System.Text.Encoding.GetEncoding(1252); //bijeesh
                                                message.IsBodyHtml = true; //bijeesh
                                                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                                                message.Subject = "Appointment Reminder: " + dpStartTimeDate.Value.ToString("ddd, dd MMM yyyy") + ' ' + cmbStartTime.Text + " @ " + clinic;
                                                message.Body = sr1.ToString();
                                                smtp.Port = 587;
                                                smtp.Host = "smtp.gmail.com";
                                                smtp.EnableSsl = true;
                                                smtp.UseDefaultCredentials = false;
                                                smtp.Credentials = new System.Net.NetworkCredential(emailName1, emailPass1);
                                                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                                smtp.Send(message);
                                            }
                                            catch (Exception ex)
                                            {
                                            }
                                        }
                                    }
                                //}
                                if (checkBox4.Checked) // Doctor Email
                                {
                                    string emailName = "", emailPass = "";
                                    if (dr_email != "")
                                    {
                                        DataTable mail = this.cntrl.send_email();
                                        if (mail.Rows.Count > 0)
                                        {
                                            emailName = mail.Rows[0]["emailName"].ToString();
                                            emailPass = mail.Rows[0]["emailPass"].ToString();
                                            try
                                            {
                                                string sr1 = "<table align='center' style='width:700px;border: 1px solid ;border-collapse: collapse; background: #EAEAEA; height:500px'><tr><td  align='left' height='27'><FONT  color='#666666'  face='Arial' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Appointment Reminder:" + dpStartTimeDate.Value.ToString("ddd, dd MMM yyyy") + ' ' + cmbStartTime.Text + " @ " + clinic + "</font></td></tr><tr><td  align='left' height='400px'><table  height='423' align='center' style='width:600px; background: #FFFFFF; height:400px'><tr><td  align='left' height='6px'><FONT  color='#000000'  face='Arial' SIZE=6>" + clinic + "</font></td></tr><tr><td  align='left' height='1px' bgcolor='#666666'></td></tr><tr><td  align='left' height='62' valign='bottom'><FONT  color='#000000'  face='Arial' SIZE=3>Dear <b>" + combodoctor.Text + "</b></font></td></tr> <tr><td align='left' height='197' valign='top'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Just to remind you about your appointment at " + clinic + ".<table><tbody><tr><td width='188' height='31' valign='bottom' align='right'>WHEN :</td><td width='30' height='31' valign='bottom' align='right'></td><td width='358' valign='bottom'> <strong>" + dpStartTimeDate.Value.ToString("ddd, dd MMM yyyy") + ' ' + cmbStartTime.Text + "</strong></td>  </tr><tr><td height='31' valign='top'  align='right'>PATIENT :</td><td width='30' height='31' valign='bottom' align='right'></td><td  valign='top'>" + txt_p_name.Text + "</td></tr><tr><td height='76' valign='top'  align='right'>FOR :</td><td width='30' height='31' valign='bottom' align='right'></td><td  valign='top'>" + txtProcedure.Text + "</td></tr></tbody></table> For any queries, contact us at : " + contact_no + "</td>  </tr><tr><td  align='left' height='1px' bgcolor='#666666'></td></tr> <tr><td height='25'  align='right' valign='bottom'>Powered by&nbsp;&nbsp; </td></tr> <tr><td height='81'  align='right' valign='top'><img src='http://pappyjoe.com/assets/images/pappyjoe-logo.PNG' alt='pappyjoe official logo'>&nbsp;&nbsp;</td></tr></table></td></tr></table>";
                                                MailMessage message = new MailMessage();
                                                message.From = new MailAddress(dr_email);
                                                message.To.Add(dr_email);
                                                message.BodyEncoding = System.Text.Encoding.GetEncoding(1252); //bijeesh
                                                message.IsBodyHtml = true; //bijeesh
                                                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                                                message.Subject = "Appointment(s) scheduled for Today: " + dpStartTimeDate.Value.ToString("ddd, dd MMM yyyy") + ' ' + cmbStartTime.Text + " @ " + clinic;
                                                message.Body = sr1.ToString();
                                                smtp.Port = 587;
                                                smtp.Host = "smtp.gmail.com";
                                                smtp.EnableSsl = true;
                                                smtp.UseDefaultCredentials = false;
                                                smtp.Credentials = new System.Net.NetworkCredential(emailName, emailPass);
                                                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                                smtp.Send(message);
                                            }
                                            catch (Exception ex)
                                            {

                                            }
                                        }
                                    }
                                }// Doctor Email end
                            }
                           
                        }
                        else { }
                    }
                    else
                    {
                        if (combodoctor.Text == "")
                        {
                            MessageBox.Show("Please select a Doctor...", "Appointment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Appointment Date should be greater than Current Date...", "Appointment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    if (dtpSearch.Rows.Count <= 0)
                    {
                        status1 = 1;
                    }
                    //bh1
                    txt_p_name.Clear();
                    txt_p_id.Clear();
                    txt_p_email.Clear();
                    txt_p_mobile.Clear();
                    combodoctor.Text = "";
                    radfemale.Text = "";
                    radMale.Text = "";
                    list_p_details.Text = "";
                    txtProcedure.Text = "";
                    combocategory.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    string DatetimeNow = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                    int pos1 = 2;
                    char replacement1 = '/';
                    DatetimeNow = DatetimeNow.Remove(pos1, 1).Insert(pos1, replacement1.ToString());
                    int po1 = 5;
                    char replacemen1 = '/';
                    DatetimeNow = DatetimeNow.Remove(po1, 1).Insert(po1, replacemen1.ToString());

                    string neworold = "0"; DataTable dtpSearch = new DataTable();
                    DataTable dtb = this.cntrl.patient_details(txt_p_name.Text);
                    Appointment_for_newPAtient(dtb);
                    if (cmbStartTime.SelectedIndex <= cmbEndTime.SelectedIndex && combodoctor.Text != "")
                    {
                        if (patient_id != "0")
                        {
                            string dr_id = combodoctor.GetItemText(combodoctor.SelectedValue);
                            string dr_color = "0";
                            long app_id = 0;
                            DateTime Dateonly = DateTime.Now.Date;
                            string dr_mobile = "0";
                            string dr_email = "";
                            DataTable dt_d = this.cntrl.Get_calenderColor(dr_id);
                            if (dt_d.Rows.Count > 0)
                            {
                                dr_color = dt_d.Rows[0][0].ToString();
                                dr_mobile = dt_d.Rows[0][1].ToString();
                                dr_email = dt_d.Rows[0][2].ToString();
                            }
                            string diff1 = "0";
                            DateTime StartTime1 = DateTime.Now.Date;
                            // string Stime; 
                            if (EditingEvent.RecurrenceState == CalendarEventRecurrenceState.xtpCalendarRecurrenceNotRecurring ||
                                EditingEvent.RecurrenceState == CalendarEventRecurrenceState.xtpCalendarRecurrenceException)
                            {
                                DateTime StartTime, EndTime;
                                StartTime = dpStartTimeDate.Value;
                                EndTime = dpStartTimeDate.Value;
                                StartTime = StartTime.AddHours(cmbStartTime.SelectedIndex / 12);
                                EndTime = EndTime.AddHours(cmbEndTime.SelectedIndex / 12);
                                int st_ms = 0;
                                st_ms = cmbStartTime.SelectedIndex % 12;
                                StartTime = StartTime.AddMinutes(st_ms * 5);
                                st_ms = cmbEndTime.SelectedIndex % 12;
                                EndTime = EndTime.AddMinutes(st_ms * 5);
                                EditingEvent.StartTime = StartTime;
                                EditingEvent.EndTime = EndTime;
                                var diff = EndTime.Subtract(StartTime);
                                if (diff.Hours.ToString() != "")
                                {
                                    int valh = diff.Hours;
                                    valh = valh * 60;
                                    int valm = diff.Minutes;
                                    diff1 = Convert.ToString(valh + valm);
                                }
                                else
                                {
                                    diff1 = Convert.ToString(diff.Minutes);
                                }
                                StartTime1 = StartTime;
                            }

                            DateTime Dateonly1 = DateTime.Now;
                            if (IsNewEvent)
                            {
                                DateTime StartT;
                                StartT = dpStartTimeDate.Value;
                                StartT = StartT.AddHours(cmbStartTime.SelectedIndex / 12);
                                int md = cmbStartTime.SelectedIndex % 12;
                                StartT = StartT.AddMinutes(md * 5);
                                //*********Booking Name***********//
                                string Name = "";
                                DataTable doctor = this.cntrl.get_doctor_login(PappyjoeMVC.Model.Connection.MyGlobals.Doctor_id);
                                if (doctor.Rows.Count > 0)
                                {
                                    if (doctor.Rows[0]["login_type"].ToString() == "doctor")
                                    {
                                        Name = "Dr ";
                                    }

                                    Name = Name + doctor.Rows[0]["doctor_name"].ToString();
                                }
                                dtpSearch = this.cntrl.patient_details_byname(txt_p_name.Text);
                                if (dtpSearch.Rows.Count <= 0)
                                {
                                    this.cntrl.insappointment(Convert.ToDateTime(Dateonly).ToString("yyyy-MM-dd"), Convert.ToDateTime(StartT).ToString("yyyy-MM-dd HH:mm"), diff1, txtDescription.Text, patient_id, txt_p_name.Text, dr_id, txt_p_mobile.Text, txt_p_email.Text, txtProcedure.Text, Name);
                                    string dt = DateTime.Now.Date.ToString("yyyy-MM-dd");
                                    DateTime Timeonly = DateTime.Now;
                                }
                                else
                                {
                                    this.cntrl.insappointment(Convert.ToDateTime(Dateonly).ToString("yyyy-MM-dd"), Convert.ToDateTime(StartT).ToString("yyyy-MM-dd HH:mm"), diff1, txtDescription.Text, patient_id, patient_name, dr_id, lab_p_ph.Text, lab_p_email.Text, txtProcedure.Text, Name);
                                    string dt = DateTime.Now.Date.ToString("yyyy-MM-dd");
                                    DateTime Timeonly = DateTime.Now;
                                }
                                DataTable dt_a = this.cntrl.appointmentId();
                                if (dt_a.Rows.Count > 0)
                                {
                                    app_id = Convert.ToInt32(dt_a.Rows[dt_a.Rows.Count - 1][0].ToString());
                                }
                                EditingEvent.Subject = patient_name;
                                EditingEvent.Location = Convert.ToString(cmbStartTime.GetItemText(cmbStartTime.SelectedItem)) + "-" + Convert.ToString(cmbEndTime.GetItemText(cmbEndTime.SelectedItem));
                                EditingEvent.Body = Convert.ToString(app_id);
                                EditingEvent.Label = Convert.ToInt32(dr_color);
                                EditingEvent.AllDayEvent = (chkAllDayEvent.Checked ? true : false);
                                EditingEvent.BusyStatus = (CalendarEventBusyStatus)cmbShowTimeAs.SelectedIndex;
                                EditingEvent.PrivateFlag = (chkPrivate.Checked ? true : false);
                                EditingEvent.MeetingFlag = (chkMeeting.Checked ? true : false);
                                EditingEvent.Reminder = chkReminder.Checked;
                                if (EditingEvent.Reminder)
                                {
                                    CalReminderMinutes pRmd = (CalReminderMinutes)cmbReminder.SelectedItem;
                                    if (pRmd != null)
                                    {
                                        EditingEvent.ReminderMinutesBeforeStart = pRmd.m_nMinutes;
                                    }
                                }
                                Main_Calendar.Instance.wndCalendarControl.DataProvider.AddEvent(EditingEvent);
                                string clinic = "", locality = "", contact_no = "";
                                System.Data.DataTable clinicname = this.cntrl.clinicdetails();
                                clinic = clinicname.Rows[0]["name"].ToString();
                                contact_no = clinicname.Rows[0]["contact_no"].ToString();
                                string smsName = "", smsPass = "", scount = "";
                                DataTable sms = this.cntrl.smsdetails();
                                DataTable smscount = this.ccntrl.getsmscnt();
                                if (sms.Rows.Count > 0)
                                {
                                    smsName = sms.Rows[0]["smsName"].ToString();
                                    smsPass = sms.Rows[0]["smsPass"].ToString();
                                }
                                if (smscount.Rows[0]["sms"].ToString() != "")
                                {
                                    scount = PappyjoeMVC.Model.EncryptionDecryption.Decrypt(smscount.Rows[0]["sms"].ToString(), "ch3lSeAW0n2o2!C1");
                                    if (Convert.ToInt32(scount) > 5)
                                    {
                                        //---------------Appoitment count SMS for Doctor-------------------
                                        if (sms.Rows[0]["doc_appoCntSMS"].ToString() == "1")
                                        {
                                            DataTable DocRemTme = this.cntrl.get_DocRemsmsTime();
                                            if (DocRemTme.Rows.Count > 0)
                                            {
                                                SMS_model a = new SMS_model();
                                                string ApptCount = ""; string SendTime = "";
                                                string number = "91" + dr_mobile;
                                                string DocRem = DocRemTme.Rows[0]["doctorApptCountsmsTime"].ToString();
                                                string ST = DateTime.Today.AddDays(1).ToShortDateString() + " " + DocRem;
                                                DateTime DT = DateTime.Parse(ST);
                                                SendTime = DT.ToString("dd/MM/yyyy hh:mm:ss tt");
                                                int pos = 2;
                                                char replacement = '/';
                                                SendTime = SendTime.Remove(pos, 1).Insert(pos, replacement.ToString());
                                                int po = 5;
                                                char replacemen = '/';
                                                SendTime = SendTime.Remove(po, 1).Insert(po, replacemen.ToString());

                                                DataTable doc = this.cntrl.getDocAppntmnt(dr_id, DateTime.Today.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Today.AddDays(1).AddHours(23).AddMinutes(59).AddSeconds(59).ToString("yyyy-MM-dd HH:mm:ss"));
                                                if (doc.Rows.Count > 0)
                                                {
                                                    ApptCount = doc.Rows.Count.ToString();
                                                }
                                                string text = "Dear " + combodoctor.Text + ", today you have " + ApptCount + " appointment in" + clinic + "CLIINI";
                                                DataTable RIDa = this.cntrl.get_docNotifInfor(dr_id);
                                                if (RIDa.Rows.Count == 0)
                                                {
                                                    this.cntrl.insert_DocId(dr_id);
                                                }
                                                DataTable RID = this.cntrl.get_docNotifInfor(dr_id);
                                                if (RID.Rows.Count > 0)
                                                {
                                                    string webrespo = RID.Rows[0]["webRespo"].ToString();
                                                    if (webrespo != "")
                                                    {
                                                        string deleDocrem = this.cntrl.DeleteDocRem(smsName, smsPass, webrespo);
                                                        if (deleDocrem != "")
                                                        {
                                                            string smspatnt3 = this.cntrl.SendSMS(smsName, smsPass, number, text, "DRTOMS", dr_id.ToString(), SendTime, DatetimeNow);
                                                            if (smspatnt3 != "")
                                                            {
                                                                this.cntrl.updateWebresp(dr_id, smspatnt3, Dateonly.ToString("yyyy-MM-dd"));
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        string smspatnt3 = this.cntrl.SendSMS(smsName, smsPass, number, text, "DRTOMS", dr_id.ToString(), SendTime, DatetimeNow);
                                                        if (smspatnt3 != "")
                                                        {
                                                            {
                                                                this.cntrl.updateWebresp(dr_id, smspatnt3, Dateonly.ToString("yyyy-MM-dd"));
                                                            }
                                                        }
                                                    }
                                                }
                                                DataTable RIDb = this.cntrl.get_docNotifInfor(dr_id);
                                                if (RID.Rows[0]["date"].ToString() != Dateonly.ToString())
                                                {
                                                    scount = Convert.ToString(Convert.ToInt32(scount) - 1);
                                                }
                                            }
                                        }
                                        else if (sms.Rows[0]["doc_appoCntSMS"].ToString() == "0")
                                        {
                                            DataTable RIDa = this.cntrl.get_docNotifInfor(dr_id);
                                            if (RIDa.Rows.Count == 0)
                                            {
                                                this.cntrl.insert_DocId(dr_id);
                                            }
                                            DataTable RID = this.cntrl.get_docNotifInfor(dr_id);
                                            if (RID.Rows.Count > 0)
                                            {
                                                string webrespo = RID.Rows[0]["webRespo"].ToString();
                                                if (webrespo != "")
                                                {
                                                    string smspatnt3 = ""; string date = "";
                                                    string deleDocrem = this.cntrl.DeleteDocRem(smsName, smsPass, webrespo);
                                                    this.cntrl.updateWebresp(dr_id, smspatnt3, date);
                                                }
                                            }
                                        }
                                        string ApptDate1 = ""; string ApptDate2 = "";
                                        if (checkBox1.Checked)
                                        {
                                            string text = "";
                                            DataTable pat = this.cntrl.Get_Patient_Details(patient_id);
                                            //DataTable smsreminder = this.cntrl.Get_reminderSmS();
                                            DataTable smslanguage = this.cntrl.sms_lang();
                                            DataTable grtpatremt = this.cntrl.getPatRemTime();
                                            if (grtpatremt.Rows.Count > 0)
                                            {
                                                schedTime = grtpatremt.Rows[0]["patientRemsmsTime"].ToString();
                                                ApptDate1 = StartT.ToShortDateString() + " " + schedTime;
                                                ApptDate2 = StartT.ToShortDateString() + " " + cmbStartTime.Text;
                                                System.DateTime date1 = DateTime.Parse(ApptDate1);
                                                DateTime date2 = DateTime.Parse(ApptDate2);
                                                System.TimeSpan time = date2.Subtract(date1);
                                                string TT = StartT.ToShortDateString() + " " + time;
                                                DateTime DT = DateTime.Parse(TT);
                                                ApptRemndrSMStime = DT.ToString("dd/MM/yyyy hh:mm:ss tt");
                                                int pos = 2;
                                                char replacement = '/';
                                                ApptRemndrSMStime = ApptRemndrSMStime.Remove(pos, 1).Insert(pos, replacement.ToString());
                                                int po = 5;
                                                char replacemen = '/';
                                                ApptRemndrSMStime = ApptRemndrSMStime.Remove(po, 1).Insert(po, replacemen.ToString());
                                            }
                                            if (pat.Rows.Count > 0)
                                            {
                                                SMS_model a = new SMS_model();
                                                string number = "91" + pat.Rows[0]["primary_mobile_number"].ToString();
                                                string type = "LNG";
                                                if (neworold == "1")
                                                {
                                                    if (sms.Rows[0]["pat_welcSMS"].ToString() == "1")
                                                    {
                                                        a.SendSMS(smsName, smsPass, number, "Dear " + pat.Rows[0]["pt_name"].ToString() + " welcome to " + clinic + "," + contact_no, type);
                                                        scount = Convert.ToString(Convert.ToInt32(scount) - 1);
                                                    }
                                                }
                                                if (smslanguage.Rows.Count > 0)
                                                {
                                                    string smslang = smslanguage.Rows[0]["Prescription_lang"].ToString();
                                                    DataTable smstemplate = this.cntrl.smstemplate(smslang, pat.Rows[0]["pt_name"].ToString(), txtProcedure.Text, StartT.ToString("dd/MM/yyyy"), cmbStartTime.Text, combodoctor.Text, clinic, contact_no);
                                                    if (smstemplate.Rows.Count > 0)
                                                    {
                                                        text = smstemplate.Rows[0]["Template"].ToString();
                                                        a.SendSMS(smsName, smsPass, number, text, type);
                                                        this.cntrl.save_Pt_SMS(patient_id, pat.Rows[0]["pt_name"].ToString(), txtProcedure.Text, StartT.ToString("dd/MM/yyyy"), cmbStartTime.Text, combodoctor.Text);
                                                        scount = Convert.ToString(Convert.ToInt32(scount) - 1);
                                                    }//
                                                    else
                                                    {
                                                        text = "Dear " + pat.Rows[0]["pt_name"].ToString() + " " + "Your appointment for " + txtProcedure.Text + " has been confirmed at " + StartT.ToString("dd/MM/yyyy") + " " + cmbStartTime.Text + " with " + "Dr " + combodoctor.Text + " Regards " + clinic + "," + contact_no;
                                                        a.SendSMS(smsName, smsPass, number, text, type);
                                                        this.cntrl.save_Pt_SMS(patient_id, pat.Rows[0]["pt_name"].ToString(), txtProcedure.Text, StartT.ToString("dd/MM/yyyy"), cmbStartTime.Text, combodoctor.Text);
                                                        scount = Convert.ToString(Convert.ToInt32(scount) - 1);
                                                    }
                                                }
                                                string sddsds = "";
                                                //text = "Dear " + pat.Rows[0]["pt_name"].ToString() + " " + "Your appointment for " + compoprocedure.Text + " has been confirmed at " + StartT.ToString("dd/MM/yyyy") + " " + cmbStartTime.Text + " with " + "Dr " + combodoctor.Text + " Regards " + clinic + "," + contact_no;
                                                //a.SendSMS(smsName, smsPass, number, text);
                                                //this.cntrl.save_Pt_SMS(patient_id, pat.Rows[0]["pt_name"].ToString(), compoprocedure.Text, StartT.ToString("dd/MM/yyyy"), cmbStartTime.Text, combodoctor.Text);
                                                ////-------------For Reminder SMS patient----------
                                                if (day_time != null)
                                                {
                                                    if (dpStartTimeDate.Value > DateTime.Now.Date)
                                                    {
                                                        //text = "Dear " + pat.Rows[0]["pt_name"].ToString() + ", " + "Today you have an appointment at " + clinic + " on " + StartT.ToString("dd/MM/yyyy") + " " + cmbStartTime.Text + " for " + txtProcedure.Text + " .Regards  " + clinic + "," + contact_no;
                                                        //a.SendSMS(smsName, smsPass, number, text, "DRTOMS", patient_id.ToString(), StartT.ToString("dd/MM/yyyy") + " 09:10:00 am", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                                                    }
                                                }
                                                if (schedTime != "")
                                                {
                                                    if (sms.Rows[0]["pat_appoRemSMS"].ToString() == "1")
                                                    {
                                                        //if (dpStartTimeDate.Value > DateTime.Now.Date)
                                                        //{
                                                        text = "Dear " + pat.Rows[0]["pt_name"].ToString() + ", " + "Today you have an appointment at " + clinic + " on " + StartT.ToString("dd/MM/yyyy") + " " + cmbStartTime.Text + " for " + txtProcedure.Text + " .Regards  " + clinic + "," + contact_no + "CLIINI";
                                                        string smspatnt3 = this.cntrl.SendSMS(smsName, smsPass, number, text, "DRTOMS", patient_id.ToString(), ApptRemndrSMStime, DatetimeNow);
                                                        //a.SendSMS(smsName, smsPass, number, text, "DRTOMS", patient_id.ToString(), ApptRemndrSMStime, DatetimeNow);
                                                        scount = Convert.ToString(Convert.ToInt32(scount) - 1);

                                                        //}
                                                    }
                                                }

                                            }
                                        }
                                        if (checkBox3.Checked)
                                        {
                                            if (dr_mobile != "0")
                                            {
                                                string text = "";
                                                //string smsName = "", smsPass = "";
                                                SMS_model a = new SMS_model();
                                                string number = "91" + dr_mobile;
                                                string type = "LNG";
                                                text = "You have an appointment on " + dpStartTimeDate.Value.ToShortDateString() + " " + cmbStartTime.Text + " With " + txt_p_name.Text + " for " + txtProcedure.Text + " at " + clinic + "," + contact_no;
                                                a.SendSMS(smsName, smsPass, number, text, type);
                                                scount = Convert.ToString(Convert.ToInt32(scount) - 1);
                                                //--------For Remainder SMS doctor-----------
                                                if (day_time != null)
                                                {
                                                    if (dpStartTimeDate.Value > DateTime.Now.Date)
                                                    {
                                                        
                                                    }
                                                }
                                                if (before_time != null)
                                                {
                                                    if (dpStartTimeDate.Value > DateTime.Now.Date)
                                                    {
                                                       
                                                    }
                                                }
                                            }
                                        }
                                        string Encrypt = PappyjoeMVC.Model.EncryptionDecryption.Encrypt(scount, "ch3lSeAW0n2o2!C1");
                                        this.ccntrl.smsCount(Encrypt);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Insufficient balance to send SMS. Please contact Pappyjoe to add SMS Balance", "SMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                if (checkBox2.Checked)
                                {
                                    string email = "", emailName = "", emailPass = "";
                                    string query = "select email_address,pt_name from tbl_patient where id='" + patient_id + "'";

                                    DataTable sr = this.cntrl.getpatemail(patient_id);
                                    if (sr.Rows.Count > 0)
                                    {
                                        email = sr.Rows[0]["email_address"].ToString();
                                        DataTable mail = this.cntrl.send_email();
                                        if (mail.Rows.Count > 0)
                                        {
                                            emailName = mail.Rows[0]["emailName"].ToString();
                                            emailPass = mail.Rows[0]["emailPass"].ToString();

                                            try
                                            {
                                                string sr1 = "<table align='center' style='width:700px;border: 1px solid ;border-collapse: collapse; background: #EAEAEA; height:500px'><tr><td  align='left' height='27'><FONT  color='#666666'  face='Arial' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Appointment Reminder:" + dpStartTimeDate.Value.ToString("ddd, dd MMM yyyy") + ' ' + cmbStartTime.Text + " @ " + clinic + "</font></td></tr><tr><td  align='left' height='400px'><table  height='423' align='center' style='width:600px; background: #FFFFFF; height:400px'><tr><td  align='left' height='6px'><FONT  color='#000000'  face='Arial' SIZE=6>" + clinic + "</font></td></tr><tr><td  align='left' height='1px' bgcolor='#666666'></td></tr><tr><td  align='left' height='62' valign='bottom'><FONT  color='#000000'  face='Arial' SIZE=3>Good morning <b>" + sr.Rows[0]["pt_name"].ToString() + "</b></font></td></tr> <tr><td align='left' height='197' valign='top'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Just to remind you about your appointment at " + clinic + ".<table><tbody><tr><td width='188' height='31' valign='bottom' align='right'>WHEN :</td><td width='30' height='31' valign='bottom' align='right'></td><td width='358' valign='bottom'> <strong>" + dpStartTimeDate.Value.ToString("ddd, dd MMM yyyy") + ' ' + cmbStartTime.Text + "</strong></td>  </tr><tr><td height='76' valign='top'  align='right'>WHERE :</td><td width='30' height='31' valign='bottom' align='right'></td><td  valign='top'>" + locality + "</td></tr></tbody></table> For any queries, contact us at : " + contact_no + "</td>  </tr><tr><td  align='left' height='1px' bgcolor='#666666'></td></tr> <tr><td height='25'  align='right' valign='bottom'>Powered by&nbsp;&nbsp; </td></tr> <tr><td height='81'  align='right' valign='top'><img src='http://pappyjoe.com/assets/images/pappyjoe-logo.PNG' alt='pappyjoe official logo'>&nbsp;&nbsp;</td></tr></table></td></tr></table>";
                                                MailMessage message = new MailMessage();
                                                message.From = new MailAddress(email);
                                                message.To.Add(email);
                                                message.BodyEncoding = System.Text.Encoding.GetEncoding(1252); //bijeesh
                                                message.IsBodyHtml = true; //bijeesh
                                                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                                                message.Subject = "Appointment Reminder: " + dpStartTimeDate.Value.ToString("ddd, dd MMM yyyy") + ' ' + cmbStartTime.Text + " @ " + clinic;
                                                message.Body = sr1.ToString();
                                                smtp.Port = 587;
                                                smtp.Host = "smtp.gmail.com";
                                                smtp.EnableSsl = true;
                                                smtp.UseDefaultCredentials = false;
                                                smtp.Credentials = new System.Net.NetworkCredential(emailName, emailPass);
                                                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                                smtp.Send(message);
                                            }
                                            catch (Exception ex)
                                            {
                                            }
                                        }
                                    }
                                }
                                if (checkBox4.Checked) // Doctor Email
                                {
                                    string emailName = "", emailPass = "";
                                    if (dr_email != "")
                                    {
                                        DataTable mail = this.cntrl.send_email();
                                        if (mail.Rows.Count > 0)
                                        {
                                            emailName = mail.Rows[0]["emailName"].ToString();
                                            emailPass = mail.Rows[0]["emailPass"].ToString();
                                            try
                                            {
                                                string sr1 = "<table align='center' style='width:700px;border: 1px solid ;border-collapse: collapse; background: #EAEAEA; height:500px'><tr><td  align='left' height='27'><FONT  color='#666666'  face='Arial' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Appointment Reminder:" + dpStartTimeDate.Value.ToString("ddd, dd MMM yyyy") + ' ' + cmbStartTime.Text + " @ " + clinic + "</font></td></tr><tr><td  align='left' height='400px'><table  height='423' align='center' style='width:600px; background: #FFFFFF; height:400px'><tr><td  align='left' height='6px'><FONT  color='#000000'  face='Arial' SIZE=6>" + clinic + "</font></td></tr><tr><td  align='left' height='1px' bgcolor='#666666'></td></tr><tr><td  align='left' height='62' valign='bottom'><FONT  color='#000000'  face='Arial' SIZE=3>Dear <b>" + combodoctor.Text + "</b></font></td></tr> <tr><td align='left' height='197' valign='top'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Just to remind you about your appointment at " + clinic + ".<table><tbody><tr><td width='188' height='31' valign='bottom' align='right'>WHEN :</td><td width='30' height='31' valign='bottom' align='right'></td><td width='358' valign='bottom'> <strong>" + dpStartTimeDate.Value.ToString("ddd, dd MMM yyyy") + ' ' + cmbStartTime.Text + "</strong></td>  </tr><tr><td height='31' valign='top'  align='right'>PATIENT :</td><td width='30' height='31' valign='bottom' align='right'></td><td  valign='top'>" + txt_p_name.Text + "</td></tr><tr><td height='76' valign='top'  align='right'>FOR :</td><td width='30' height='31' valign='bottom' align='right'></td><td  valign='top'>" + txtProcedure.Text + "</td></tr></tbody></table> For any queries, contact us at : " + contact_no + "</td>  </tr><tr><td  align='left' height='1px' bgcolor='#666666'></td></tr> <tr><td height='25'  align='right' valign='bottom'>Powered by&nbsp;&nbsp; </td></tr> <tr><td height='81'  align='right' valign='top'><img src='http://pappyjoe.com/assets/images/pappyjoe-logo.PNG' alt='pappyjoe official logo'>&nbsp;&nbsp;</td></tr></table></td></tr></table>";
                                                MailMessage message = new MailMessage();
                                                message.From = new MailAddress(dr_email);
                                                message.To.Add(dr_email);
                                                message.BodyEncoding = System.Text.Encoding.GetEncoding(1252); //bijeesh
                                                message.IsBodyHtml = true; //bijeesh
                                                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                                                message.Subject = "Appointment(s) scheduled for Today: " + dpStartTimeDate.Value.ToString("ddd, dd MMM yyyy") + ' ' + cmbStartTime.Text + " @ " + clinic;
                                                message.Body = sr1.ToString();
                                                smtp.Port = 587;
                                                smtp.Host = "smtp.gmail.com";
                                                smtp.EnableSsl = true;
                                                smtp.UseDefaultCredentials = false;
                                                smtp.Credentials = new System.Net.NetworkCredential(emailName, emailPass);
                                                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                                smtp.Send(message);
                                            }
                                            catch (Exception ex)
                                            {

                                            }
                                        }
                                    }
                                }// Doctor Email end
                            }
                            else
                            {
                                DateTime StartT;
                                StartT = dpStartTimeDate.Value;
                                StartT = StartT.AddHours(cmbStartTime.SelectedIndex / 12);
                                int md = cmbStartTime.SelectedIndex % 12;
                                StartT = StartT.AddMinutes(md * 5);
                                EditingEvent.Subject = patient_name;
                                EditingEvent.Location = Convert.ToString(cmbStartTime.GetItemText(cmbStartTime.SelectedItem)) + "-" + Convert.ToString(cmbEndTime.GetItemText(cmbEndTime.SelectedItem));
                                EditingEvent.Body = Convert.ToString(gpl_app_id);
                                EditingEvent.Label = Convert.ToInt32(dr_color);
                                if (cmbStartTime.SelectedIndex % 2 > 0)
                                {
                                    StartT = StartT.AddMinutes(30);
                                }
                                this.cntrl.update_appointment(Convert.ToDateTime(StartT), diff1, txtDescription.Text, txtProcedure.Text, patient_id, patient_name, dr_id, lab_p_ph.Text, lab_p_email.Text, gpl_app_id);
                            }
                            EditingEvent = null;
                            Main_Calendar.Instance.ContextEvent = null;
                            Main_Calendar.Instance.wndCalendarControl.Populate();
                            Main_Calendar.Instance.wndCalendarControl.RedrawControl();
                            if (Convert.ToDateTime(DateTime.Today.ToString("d")) == dpStartTimeDate.Value)
                            {
                                Main_Calendar.Instance.listAppointment("0");
                            }
                            this.Close();
                            Main_Calendar.Instance.Activate();
                        }
                        else { }
                    }
                    else
                    {
                        if (combodoctor.Text == "")
                        {
                            MessageBox.Show("Please select a Doctor...", "Appointment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Appointment Date should be greater than Current Date...", "Appointment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    if (dtpSearch.Rows.Count <= 0)
                    {
                        status1 = 1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }
        public void ModifyEvent(CalendarEvent ModEvent)
        {
            gpl_app_id = "0";
            gpl_app_id = ModEvent.Body;
            IsNewEvent = false;
            editflag = true;
            txtSubject.Text = ModEvent.Subject;
            txtLocation.Text = ModEvent.Location;
            chkAllDayEvent.Checked = (ModEvent.AllDayEvent ? true : false);
            cmbLabel.SelectedIndex = ModEvent.Label;
            cmbShowTimeAs.SelectedIndex = (int)ModEvent.BusyStatus;
            chkPrivate.Checked = (ModEvent.PrivateFlag ? true : false);
            chkMeeting.Checked = (ModEvent.MeetingFlag ? true : false);
            DataTable dt_a = this.cntrl.get_appointment_procedure(ModEvent.Body);
            if (ModEvent.RecurrenceState == CalendarEventRecurrenceState.xtpCalendarRecurrenceNotRecurring ||
                ModEvent.RecurrenceState == CalendarEventRecurrenceState.xtpCalendarRecurrenceException)
            {
                if (editflag == false)
                    SetStartEnd(ModEvent.StartTime, ModEvent.EndTime, ModEvent.AllDayEvent);
                else
                {
                    DateTime dtstart = new DateTime(DateTime.Now.Ticks);
                    dtstart = Convert.ToDateTime(dt_a.Rows[0]["start_datetime"].ToString());
                    //lbl_scheduledtime.Text = dtstart.ToString("t") + " On " + dtstart.ToString("dd MMMM") + " for " + dt_a.Rows[0]["duration"].ToString() + " mins";
                    lbl_scheduledtime.Text = dtstart.ToString("dd MMM yyyy") + " at " + dtstart.ToString("t") + " for " + dt_a.Rows[0]["duration"].ToString() + " mins";

                }
            }
            EditingEvent = ModEvent;
            panel1.Hide();
            list_p_details.Visible = false;
            if (dt_a.Rows.Count > 0)
            {
                patient_id = dt_a.Rows[0][0].ToString();
                app_Doctor_id = dt_a.Rows[0]["dr_id"].ToString();
                txtDescription.Text = dt_a.Rows[0]["note"].ToString();
                txtProcedure.Text = dt_a.Rows[0]["plan_new_procedure"].ToString();
            }
            else
            {
                patient_id = "0";
            }
            //combodoctor.SelectedIndex = 1;
            txt_p_name.Text = ModEvent.Subject;
            patient_name = ModEvent.Subject;
            DataTable dt_p = this.cntrl.Getpat_MobNamme(patient_id);
            if (dt_p.Rows.Count > 0)
            {
                lab_p_name.Text = dt_p.Rows[0][0].ToString() + "(" + dt_p.Rows[0][1].ToString() + ")";
                lab_p_gndr.Text = dt_p.Rows[0][2].ToString();
                lab_p_ph.Text = dt_p.Rows[0][3].ToString();
                lab_p_email.Text = dt_p.Rows[0][4].ToString();
            }
        }
        public void SetStartEnd(DateTime BeginSelection, DateTime EndSelection, Boolean AllDay)
        {
            DateTime StartDate, StartTime, EndDate, EndTime;

            StartDate = BeginSelection.Date;
            StartTime = BeginSelection;

            EndDate = EndSelection.Date;
            EndTime = EndSelection;

            if (AllDay)
            {
                cmbStartTime.Visible = false;
                cmbEndTime.Visible = false;
            }

            dpStartTimeDate.Value = StartDate;

            int mis = 0;
             mis = StartTime.Minute / 5;
            cmbStartTime.SelectedIndex = (int)(StartTime.Hour * 12 + mis);
            //UpdateEndTimeCombo();
            dpEndTimeDate.Value = EndDate;
            mis = EndTime.Minute / 5;
            cmbEndTime.SelectedIndex = (int)(EndTime.Hour * 12 + mis);
            var diff = EndTime.Subtract(StartTime);
            lbl_scheduledtime.Text = dpStartTimeDate.Value.ToString("dd MMM yyyy") + " at " + cmbStartTime.Text + " for " + Convert.ToString(diff.Minutes) + " mins";

        }
        public void Appointment_for_newPAtient(DataTable dtpSearch)
        {
            list_p_details.DataSource = dtpSearch;
            string neworold = "0";
            if (dtpSearch.Rows.Count <= 0)
            {
                if (!String.IsNullOrWhiteSpace(txt_p_name.Text))
                {
                    if (txt_p_mobile.TextLength >= 10)
                    {
                        string patid_ = "";
                        DataTable patidGeneration = this.cntrl.get_patientPrefix();
                        if (patidGeneration.Rows.Count > 0)
                        {
                            patid_ = patidGeneration.Rows[0]["patient_prefix"].ToString() + patidGeneration.Rows[0]["patient_number"].ToString();
                        }
                        else
                        {
                            patid_ = txt_p_id.Text;
                        }
                        if (!PappyjoeMVC.Model.Connection.checkforemail(txt_p_email.Text.ToString()) && txt_p_email.Text != "")
                        {
                            MessageBox.Show("invalid Email address", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_p_email.Focus();
                            txt_p_email.BackColor = Color.Coral;
                        }
                        else if (radMale.Checked != true && radfemale.Checked != true)
                        {
                            MessageBox.Show("Please select gender", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (radMale.Checked == true)
                            {
                                gender = "Male";
                            }
                            else if (radfemale.Checked == true)
                            {
                                gender = "Female";
                            }
                            this.cntrl.Save_patient(txt_p_name.Text, patid_, "", gender, "", "","","", "", "", txt_p_mobile.Text, "", "", txt_p_email.Text, "", "", "", "", "", "", DateTime.Now.ToString("yyyy-MM-dd"), combodoctor.Text, "", "", "");
                            this.cntrl.save_group( patid_,combocategory.Text);
                            DataTable rs_patient = this.cntrl.get_max_patid();
                            patient_name = txt_p_name.Text;
                            patient_id = rs_patient.Rows[0][0].ToString();
                            neworold = "1";
                            DataTable cmd = this.cntrl.automaticid();
                            if (cmd.Rows.Count > 0)
                            {
                                int n = 0;
                                n = int.Parse(cmd.Rows[0]["patient_number"].ToString()) + 1;
                                if (n != 0)
                                {
                                    this.cntrl.update_autogenerateid(n);
                                }
                            }
                        }
                    }
                    else
                    {
                        label14.Text = " * You must enter a valid mobile number..!!";
                        label14.Visible = true;
                    }
                }
                else
                {
                    label14.Text = "* You must enter a value in the  Patient Name/Mobile field..!!";
                    label14.Visible = true;
                }
            }
        }
        public void CreateNewEvent()
        {
            try
            {
                EditingEvent = PappyjoeMVC.View.Main_Calendar.Instance.wndCalendarControl.DataProvider.CreateEvent();
                IsNewEvent = true;
                DateTime BeginSelection, EndSelection;
                Boolean AllDay;
                BeginSelection = EndSelection = DateTime.Now;
                AllDay = false;
                PappyjoeMVC.View.Main_Calendar.Instance.wndCalendarControl.ActiveView.GetSelection(ref BeginSelection, ref EndSelection, ref AllDay);
                AllDay = false;
                SetStartEnd(BeginSelection, EndSelection, AllDay);
                if (AllDay)
                {
                    PappyjoeMVC.View.Main_Calendar.Instance.ContextEvent = null;
                    this.Close();
                }
                //Bijeesh chkAllDayEvent.Checked = (AllDay ? true : false);
                txtSubject.Text = "New Event";
                cmbShowTimeAs.SelectedIndex = (AllDay ? 0 : 2);
                cmbLabel.SelectedIndex = 0;

                PappyjoeMVC.View.Main_Calendar.Instance.ContextEvent = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AppointmentBooking_Click(object sender, EventArgs e)
        {
            list_p_details.Hide();
            listProcedure.Hide();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            if(dashboard_flag==true)
            {
                this.Close();
            }
            else
            {
                EditingEvent = null;
                Main_Calendar.Instance.ContextEvent = null;
                this.Close();
            }
           
        }

        private void txt_p_mobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void txt_p_name_Click(object sender, EventArgs e)
        {
            label14.Visible = false;
            DataTable dt = this.cntrl.search_patient(txt_p_name.Text);
            list_p_details.DisplayMember = "pt_name";
            list_p_details.ValueMember = "id";
            list_p_details.DataSource = dt;
            //list_p_details.Location = new Point(146, 45);
            list_p_details.Show();
        }

        private void txt_p_name_TextChanged(object sender, EventArgs e)
        {
            if(editflag==false)
            {
                label14.Visible = false;
                DataTable dt = this.cntrl.search_patient(txt_p_name.Text);
                list_p_details.DisplayMember = "pt_name";
                list_p_details.ValueMember = "id";
                list_p_details.DataSource = dt;
                list_p_details.Show();
                if (dt.Rows.Count <= 0)
                {
                    list_p_details.Hide();
                }
            }
          
        }

        private void lab_change_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            editflag = false;
        }

        private void txtProcedure_TextChanged(object sender, EventArgs e)
        {
            if (txtProcedure.Text != "")
            {
                DataTable dt = this.cntrl.search_procedure(txtProcedure.Text);
                listProcedure.DisplayMember = "name";
                listProcedure.ValueMember = "id";
                listProcedure.DataSource = dt;
            }
            else
            {
                DataTable dt = this.cntrl.search_procedure(txtProcedure.Text);
                listProcedure.DisplayMember = "name";
                listProcedure.ValueMember = "id";
                listProcedure.DataSource = dt;
            }
            if (listProcedure.Items.Count > 0)
            {
                listProcedure.Visible = true;
            }
            else
            { listProcedure.Visible = false; }
        }

        private void listProcedure_MouseClick(object sender, MouseEventArgs e)
        {
            if (listProcedure.SelectedItems.Count > 0)
            {
                string value = listProcedure.SelectedValue.ToString();
                DataTable pr = this.cntrl.Get_procedure(value);
                if (pr.Rows.Count > 0)
                {
                    txtProcedure.Text = pr.Rows[0]["name"].ToString();
                    listProcedure.Visible = false;
                }
                else
                {
                    MessageBox.Show("Please choose Correct procedure....", "Data Not Found..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listProcedure_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && listProcedure.Items.Count > 0)
            {
                if (listProcedure.SelectedItems.Count > 0)
                {
                    string value = listProcedure.SelectedValue.ToString();
                    DataTable pr = this.cntrl.Get_procedure(value);
                    if (pr.Rows.Count > 0)
                    {
                        txtProcedure.Text = pr.Rows[0]["name"].ToString();
                        listProcedure.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Please choose Correct procedure....", "Data Not Found..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void txtProcedure_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && listProcedure.Items.Count > 0)
            {
                listProcedure.Focus();
            }
            else if (e.KeyCode == Keys.Enter && listProcedure.Items.Count > 0)
            {

                if (listProcedure.SelectedItems.Count > 0)
                {
                    string value = listProcedure.SelectedValue.ToString();
                    DataTable pr = this.cntrl.Get_procedure(value);
                    if (pr.Rows.Count > 0)
                    {
                        txtProcedure.Text = pr.Rows[0]["name"].ToString();
                        listProcedure.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Please choose Correct procedure....", "Data Not Found..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void txtProcedure_Click(object sender, EventArgs e)
        {
            txtProcedure.Text = "";
            if (txtProcedure.Text != "")
            {
                DataTable dt = this.cntrl.search_procedure(txtProcedure.Text);
                listProcedure.DisplayMember = "name";
                listProcedure.ValueMember = "id";
                listProcedure.DataSource = dt;
            }
            else
            {
                DataTable dt = this.cntrl.search_procedure(txtProcedure.Text);
                listProcedure.DisplayMember = "name";
                listProcedure.ValueMember = "id";
                listProcedure.DataSource = dt;
            }
            if (listProcedure.Items.Count > 0)
            {
                listProcedure.Visible = true;
            }
            else
            { listProcedure.Visible = false; }
        }

        private void cmbStartTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStartTime.SelectedIndex < 287)
            {
                cmbEndTime.SelectedIndex = cmbStartTime.SelectedIndex + 1;
            }
            else
            {
                cmbEndTime.SelectedIndex = cmbStartTime.SelectedIndex;
            }
        }

        private void listProcedure_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
