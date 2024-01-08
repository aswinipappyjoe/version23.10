using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using PappyjoeMVC.Model;

namespace PappyjoeMVC.View
{
    public partial class Fast_Track : Form
    {
        public event child_form Appointment_list;
        public delegate void child_form(string strappointment);
        public string doctor_id = "0", patient_id="",loginid="0";
        public string payment_status = ""; public static string newptid = "", ptname = "";
        public string strApp_id = "";
        string Prescription_bill_status = "No";
        string idcomp, iddiag, idnote, idtret, idpres;
        public static bool flag = false;
        fasttrack_model cntrl = new fasttrack_model();
        Prescription_model pmodel = new Prescription_model();
        Common_model cmodel = new Common_model();
        Connection db = new Connection(); 
        Add_Invoice_model amodel = new Add_Invoice_model();
        Add_Treatmentplan_model TModel = new Add_Treatmentplan_model();
        Add_Finished_Treatment_model fmodel = new Add_Finished_Treatment_model();
        public Fast_Track()
        {
            InitializeComponent();
            
        }

        public Fast_Track(string text, string id2)
        {
            InitializeComponent();
            ptname = text;
            newptid = id2;
        }

        private void Clinical_Click(object sender, EventArgs e)
        {

        }

        private void Fast_Track_Load(object sender, EventArgs e)
        {
            try
            {
                load_flag = true;
                //vitals  txt_temp  cmb_temp txt_blood1 txt_blood2 cmb_blood txt_height  txt_weight txt_resp txt_spo txt_bmi
                //userControl_label17.Text = "VITALS";
                //userControl_label1.Text = "Pulse";
                //userControl_label4.Text = "Temperature(C)";
                //userControl_label2.Text = "Blood Pressure(mm Hg)";
                //userControl_label3.Text = "Height(Cm)";
                //userControl_label7.Text = "Weight(Kg)";
                //userControl_label6.Text = "Resp.Rate(Breaths/min)";
                //userControl_label8.Text = "SPO2(%)";
                //userControl_label5.Text = "BMI";
                ///
                //dgv_prescrptn.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
                //dgv_prescrptn.EnableHeadersVisualStyles = false;
                cmb_blood.SelectedIndex = 0; cmb_temp.SelectedIndex = 0;
                btn_clinical_findings.BackColor = Color.White;
                btn_clinical_findings.ForeColor = Color.DarkBlue;
                userControl_groupbox1.Text = "Patient Details";
                //clinical findings
                pnal_previous_prescription.Visible = false;
                pnal_prescription.Visible = false;
                pnal_clinicalfinding.Visible = true;
                pnal_clinicalfinding.Size = new Size(1008, 275);
                pnal_clinicalfinding.Location = new Point(0, 30);
                //userControl_txt_complaints_search.Text = "Complaints Search";
                //userControl_txt_Diagnosis_search.Text = "Diagnosis Search";
                //userControl_txt_notes_search.Text = "Notes Search";
                //rjBtn_previous_clinical_findings.Text = "Previous Clinical Findings";
                pat_name.Visible = false;
                label13.Visible = false;
                rjcmb_clinic_date.Visible = false;
                label29.Visible = false;
                label7.Visible = false;
                lb_gender.Visible = false;
                label14.Visible = false;
                lb_mobile.Visible = false;
                label15.Visible = false;
                pat_name.Visible = false;
                ///Doctor
                //userControl_label9.Text = "Fee";
                //userControl_label10.Text = "Last Visited Date";
                //userControl_label15.Text = "Doctor";

                dateTime_review.Value = DateTime.Today.AddMonths(+1);
               
                //from calender
                if(strApp_id!="")
                {
                    btn_newPatient.Enabled = false;
                    DataTable dtb = this.cntrl.get_pt_details(patient_id);
                    if (dtb.Rows.Count > 0)
                    {

                        pat_name.Visible = true;
                        patient_id = dtb.Rows[0]["id"].ToString();
                        if (dtb.Rows[0]["pt_id"].ToString() != "")
                        {
                            label15.Text = dtb.Rows[0]["pt_id"].ToString();
                            label15.Visible = true; label7.Visible = true;
                        }
                        else
                        {
                            label15.Visible = false; label7.Visible = false;
                        }


                        if (dtb.Rows[0]["pt_name"].ToString() != "")
                            pat_name.Text = dtb.Rows[0]["pt_name"].ToString();
                        if (dtb.Rows[0]["primary_mobile_number"].ToString() != "")
                        {
                            label29.Visible = true;
                            lb_mobile.Visible = true;
                            lb_mobile.Text = dtb.Rows[0]["primary_mobile_number"].ToString();
                        }
                        else
                        {
                            lb_mobile.Visible = false;
                            label29.Visible = false;
                        }


                        if (dtb.Rows[0]["age"].ToString() != "")
                        {
                            label14.Visible = true;
                            label14.Text = dtb.Rows[0]["age"].ToString() + dtb.Rows[0]["days"].ToString();
                        }
                        else
                        {
                            label14.Visible = false;
                        }
                        if (dtb.Rows[0]["gender"].ToString().Trim() != "")
                        {
                            lb_gender.Visible = true;
                            lb_gender.Text = dtb.Rows[0]["gender"].ToString().Trim();
                        }
                        else
                        {
                            lb_gender.Visible = false;
                        }

                        txt_search.Text = "Search by Patient Id, Name";
                        txt_search.ForeColor = Color.LightSlateGray;
                        txt_search.Visible = false;

                    }
                    DataTable dt_doctr = this.cntrl.Load_dctrname(doctor_id);
                    if(dt_doctr.Rows.Count>0)
                    {
                        string[] items = new string[dt_doctr.Rows.Count];
                        for (int i = 0; i < dt_doctr.Rows.Count; i++)
                        {
                            items[i] = dt_doctr.Rows[i]["doctor_name"].ToString();
                        }
                        rjCmb_doctor.DataSource = items;
                    }
                }
                else
                {
                    btn_newPatient.Enabled = true;
                    DataTable dt = this.cntrl.Load_doctor();
                    if (dt.Rows.Count > 0)
                    {
                        string[] items = new string[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            items[i] = dt.Rows[i]["doctor_name"].ToString();
                        }
                        rjCmb_doctor.DataSource = items;

                    }
                }
                //invoice
                cmb_discount.SelectedIndex = 0;
                DataTable dt5 = this.amodel.load_tax();
                if (dt5.Rows.Count > 0)
                {
                    string[] items = new string[dt5.Rows.Count];
                    for (int i = 0; i < dt5.Rows.Count; i++)
                    {
                           items[i] = dt5.Rows[i]["tax_name"].ToString();
                    }
                    cmb_tax.DataSource = items;

                   
                }
                DataTable invno = null;
                invno = this.amodel.Get_invoice_prefix();
                if (invno.Rows.Count == 0)
                {
                    txt_invoiceno.Enabled = true;
                }
                else
                {
                    txt_invoiceno.Text = invno.Rows[0]["invoice_prefix"].ToString() + invno.Rows[0]["invoice_number"].ToString();
                }
                lb_add_disc.Visible = true;
                lb_add_tax.Visible = true;
                load_flag = false;
            }
            catch(Exception ex)
            {

            }
        }

        private void txt_search_Click(object sender, EventArgs e)
        {
            if (txt_search.Text == "Search by Patient Id, Name")
            {
                txt_search.Text = "";
                txt_search.ForeColor = Color.DarkBlue;
            }
        }

       

        private void listpatientsearch_MouseClick(object sender, MouseEventArgs e)
        {
            string pt_id = listpatientsearch.SelectedValue.ToString();
            listpatientsearch.Visible = false;
            DataTable dtb = this.cntrl.get_pt_details(pt_id);
            if (dtb.Rows.Count > 0)
            {

                pat_name.Visible = true;
                patient_id = dtb.Rows[0]["id"].ToString();
                if (dtb.Rows[0]["pt_id"].ToString() !="")
                {
                    label15.Text = dtb.Rows[0]["pt_id"].ToString();
                    label15.Visible = true; label7.Visible = true;
                }
                else
                {
                    label15.Visible = false; label7.Visible = false;
                }
                   

                if (dtb.Rows[0]["pt_name"].ToString()!="")
                    pat_name.Text = dtb.Rows[0]["pt_name"].ToString();
                if(dtb.Rows[0]["primary_mobile_number"].ToString()!="")
                {
                    label29.Visible = true;
                    lb_mobile.Visible = true;
                    lb_mobile.Text = dtb.Rows[0]["primary_mobile_number"].ToString();
                }
                else
                {
                    lb_mobile.Visible = false;
                    label29.Visible = false;
                }
                   

                if(dtb.Rows[0]["age"].ToString()!="")
                {
                    label14.Visible = true;
                    label14.Text = dtb.Rows[0]["age"].ToString() + dtb.Rows[0]["days"].ToString();
                }
                else
                {
                    label14.Visible = false;
                }
                if(dtb.Rows[0]["gender"].ToString().Trim()!="")
                {
                    lb_gender.Visible = true;
                    lb_gender.Text = dtb.Rows[0]["gender"].ToString().Trim();
                }
                else
                {
                    lb_gender.Visible = false;
                }
               
                txt_search.Text = "Search by Patient Id, Name";
                txt_search.ForeColor = Color.LightSlateGray;
                txt_search.Visible = false;
               
            }
            //rjCmb_doctor_onSelectedIndexChanged(null,null);

            //cons
            DateTime paid_date, todate; int f_days = 0; double days = 0;
            DataTable dtb_follow = this.cntrl.get_followup(patient_id);
            if(dtb_follow.Rows.Count>0)
            {
                doctor_id = dtb_follow.Rows[0]["dr_id"].ToString();

                rjCmb_doctor.Text = dtb_follow.Rows[0]["doctorname"].ToString();
                DataTable dt_doctor_id = this.cntrl.get_doctorname_(doctor_id);
                userControl_txt_last_visited_date.Visible = true; userControl_label10.Visible = true;
                todate = DateTime.Now.Date;
                userControl_txt_last_visited_date.Text = dtb_follow.Rows[0]["visited_date"].ToString();
                f_days = Convert.ToInt32(dt_doctor_id.Rows[0]["followup_period"].ToString());
                DataTable dt_last = this.cntrl.get_last_paid(pt_id);
                if (dt_last.Rows.Count > 0)//payment_status <>'Consultation'
                {
                    if (Convert.ToDecimal(dt_last.Rows[0]["fee"].ToString()) > 0)
                    {
                        paid_date = Convert.ToDateTime(dt_last.Rows[0]["payment_date"].ToString());
                        TimeSpan t = todate - paid_date;
                        days = t.TotalDays;
                    }
                    if (days < f_days)

                    {

                        userControl_txt_consultation.Text = "FOLLOWUP";
                        chk_followup.Visible = true;
                        lb_followup.Visible = true;
                        userControl_txt_fee.Text = dt_doctor_id.Rows[0]["followup_fee"].ToString();

                        payment_status = "Followup";
                    }
                    else
                    {
                        lb_followup.Visible = false; chk_followup.Visible = false;
                        userControl_txt_consultation.Text = "CONSULTATION";
                        payment_status = "Consultation";
                        if (rjCmb_doctor.SelectedItem.ToString() != "")
                        {
                            DataTable dt_doctorid = this.cntrl.get_doctorname(doctor_id);
                            if (dt_doctorid.Rows.Count > 0)
                            {
                                userControl_txt_fee.Text = dt_doctorid.Rows[0]["fee"].ToString();
                            }
                            else
                            {
                                userControl_txt_fee.Text = "0.00";

                            }
                        }
                    }
                }
               

            }
            else
            {
                userControl_txt_last_visited_date.Visible = false;  userControl_label10.Visible = false;
                lb_followup.Visible = false; chk_followup.Visible = false;
                userControl_txt_consultation.Text = "CONSULTATION";
                payment_status = "Consultation";
                if (rjCmb_doctor.SelectedItem.ToString() != "")
                {
                    DataTable dt_doctor_ = this.cntrl.get_doctorname(doctor_id);
                    if (dt_doctor_.Rows.Count > 0)
                    {
                        userControl_txt_fee.Text = dt_doctor_.Rows[0]["fee"].ToString();
                    }
                    else
                    {
                        userControl_txt_fee.Text = "0.00";

                    }
                }
            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (flag == false)
            {
                if (txt_search.Text != "")
                {
                    // lbPatient.Show();
                    //listpatientsearch.Location = new Point(45, 84);// txt_Pt_search.Location.X, 110);
                    DataTable dtdr = this.cntrl.Patient_search(txt_search.Text);//srch_patient(txt_Pt_search.Text, txt_Pt_search.Text);
                    listpatientsearch.DataSource = dtdr;
                    listpatientsearch.DisplayMember = "patient";
                    listpatientsearch.ValueMember = "id";

                }
                else
                {
                    DataTable dtdr = this.cntrl.Patient_search(txt_search.Text);
                    listpatientsearch.DataSource = dtdr;
                    listpatientsearch.DisplayMember = "patient";
                    listpatientsearch.ValueMember = "id";
                }
                if (listpatientsearch.Items.Count > 0)
                {
                    listpatientsearch.Show();
                }
                else
                {
                    listpatientsearch.Hide();
                }
            }
        }

       
        private void userControl_txt_complaints_search_Click(object sender, EventArgs e)
        {
            //if (userControl_txt_complaints_search.Text == "Complaints Search")
            //{
            //    userControl_txt_complaints_search.Text = "";
            //    userControl_txt_complaints_search.ForeColor = Color.DarkBlue;
            //}
        }

        private void userControl_txt_complaints_search_KeyUp(object sender, KeyEventArgs e)
        {
            if (userControl_txt_complaints_search.Text != "")
            {
                userControl_txt_complaints_search.ForeColor = Color.DarkBlue;
                DataTable dt = this.cntrl.complaint_cell_search(userControl_txt_complaints_search.Text);
                if(dt.Rows.Count>0)
                {
                    lst_cheif.Visible = true;
                    lst_cheif.DataSource = dt;
                    lst_cheif.DisplayMember = "name";
                    lst_cheif.ValueMember = "id";
                }
            }
        }

      

        private void lst_cheif_MouseClick(object sender, MouseEventArgs e)
        {
            //string pt_id = lst_cheif.Text.ToString();
            //lst_cheif.Visible = false;
            //if (userControl_text_complaints.Text == "")
            //    userControl_text_complaints.Text = pt_id;
            //else
            //    userControl_text_complaints.Text += "," + pt_id;

            //userControl_txt_complaints_search.Text = "Complaints";
            //userControl_txt_complaints_search.ForeColor = Color.FromArgb(194, 194, 163);
        }

        private void userControl_txt_Diagnosis_search_Click(object sender, EventArgs e)
        {
            if (userControl_txt_Diagnosis_search.Text == "Diagnosis Search")
            {
                userControl_txt_Diagnosis_search.Text = "";
                userControl_txt_Diagnosis_search.ForeColor = Color.DarkBlue;
            }
        }

        private void userControl_txt_notes_search_Click(object sender, EventArgs e)
        {
            if (userControl_txt_notes_search.Text == "Notes Search")
            {
                userControl_txt_notes_search.Text = "";
                userControl_txt_notes_search.ForeColor = Color.DarkBlue;
            }
        }

        private void lst_Diagnosis_MouseClick(object sender, MouseEventArgs e)
        {
            string pt_id = lst_Diagnosis.Text.ToString();
            lst_Diagnosis.Visible = false;
            if (userControl_text_Diagnosis.Text == "")
                userControl_text_Diagnosis.Text = pt_id;
            else
                userControl_text_Diagnosis.Text += "," + pt_id;

            userControl_txt_Diagnosis_search.Text = "Diagnosis Search";
            userControl_txt_Diagnosis_search.ForeColor = Color.FromArgb(194, 194, 163);
        }

        private void lst_notes_MouseClick(object sender, MouseEventArgs e)
        {
            string pt_id = lst_notes.Text.ToString();
            lst_notes.Visible = false;
            if (userControl_text_notes.Text == "")
                userControl_text_notes.Text = pt_id;
            else
                userControl_text_notes.Text += "," + pt_id;

            userControl_txt_notes_search.Text = "Notes Search";
            userControl_txt_notes_search.ForeColor = Color.FromArgb(194, 194, 163);
        }

        private void userControl_txt_Diagnosis_search_KeyUp(object sender, KeyEventArgs e)
        {
            if (userControl_txt_Diagnosis_search.Text != "")
            {
                userControl_txt_Diagnosis_search.ForeColor = Color.DarkBlue;
                DataTable dt = this.cntrl.diagnose_cell_search(userControl_txt_Diagnosis_search.Text);
                if (dt.Rows.Count > 0)
                {
                    lst_Diagnosis.Visible = true;
                    lst_Diagnosis.DataSource = dt;
                    lst_Diagnosis.DisplayMember = "diagnosis";
                    lst_Diagnosis.ValueMember = "id";
                }
                else
                {
                    lst_Diagnosis.Visible = false;
                    //userControl_txt_Diagnosis_search.Text = "Diagnosis Search";
                    //userControl_txt_Diagnosis_search.ForeColor = Color.FromArgb(194, 194, 163);
                }
            }
            else
            {
                lst_Diagnosis.Visible = false;
                //userControl_txt_Diagnosis_search.Text = "Diagnosis Search";
                //userControl_txt_Diagnosis_search.ForeColor = Color.FromArgb(194, 194, 163);
            }
        }

        private void userControl_txt_notes_search_KeyUp(object sender, KeyEventArgs e)
        {
            if (userControl_txt_notes_search.Text != "")
            {
                userControl_txt_notes_search.ForeColor = Color.DarkBlue;
                DataTable dt = this.cntrl.notes_cell_search(userControl_txt_notes_search.Text);
                if (dt.Rows.Count > 0)
                {
                    lst_notes.Visible = true;
                    lst_notes.DataSource = dt;
                    lst_notes.DisplayMember = "notes";
                    lst_notes.ValueMember = "id";
                }
                else
                {
                    lst_notes.Visible = false;
                    //userControl_txt_notes_search.Text = "Notes Search";
                    //userControl_txt_notes_search.ForeColor = Color.FromArgb(194, 194, 163);
                }
            }
            else
            {
                lst_notes.Visible = false;
                //userControl_txt_notes_search.Text = "Notes Search";
                //userControl_txt_notes_search.ForeColor = Color.FromArgb(194, 194, 163);
            }
        }
        public bool clinic_flag = false; 
        private void userControl_label_previous_clinic_Click(object sender, EventArgs e)
        {
            clinic_flag = false;
        }

      
        private void cmb_clinic_date_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(clinic_flag==false)
            //{
            //    if (cmb_clinic_date.Items.Count > 0)
            //    {
            //        userControl_label_complaints.Visible = true;
            //        userControl_label_diag.Visible = true;
            //        userControl_label_notes.Visible = true;
            //        userControl_textbox_compla.Visible = true;
            //        userControl_textbox_diagn.Visible = true;
            //        userControl_textbox_notes.Visible = true;

            //        DataTable dt_clinic_id = this.cntrl.get_clinic_id(patient_id,Convert.ToDateTime( cmb_clinic_date.Text).ToString("yyyy-MM-dd"));
            //        if (dt_clinic_id.Rows.Count > 0)
            //        {
            //            DataTable dt_complaints = this.cntrl.pt_complaints(dt_clinic_id.Rows[0][0].ToString());
            //            if (dt_complaints.Rows.Count > 0)
            //            {
            //                for (int i = 0; i < dt_complaints.Rows.Count; i++)
            //                {
            //                    userControl_textbox_compla.Text += dt_complaints.Rows[i]["complaint_id"].ToString() + "" + ",";
            //                }
            //            }
            //            DataTable dt_dignosis = this.cntrl.pt_diagnosis(dt_clinic_id.Rows[0][0].ToString());
            //            if (dt_dignosis.Rows.Count > 0)
            //            {
            //                for (int i = 0; i < dt_dignosis.Rows.Count; i++)
            //                {
            //                    userControl_textbox_diagn.Text += dt_dignosis.Rows[i]["diagnosis_id"].ToString() + "" + ",";
            //                }
            //            }
            //            DataTable dt_notes = this.cntrl.pt_notes(dt_clinic_id.Rows[0][0].ToString());
            //            if (dt_notes.Rows.Count > 0)
            //            {
            //                for (int i = 0; i < dt_notes.Rows.Count; i++)
            //                {
            //                    userControl_textbox_notes.Text += dt_notes.Rows[i]["note_name"].ToString() + "" + ",";
            //                }
            //            }
            //        }
            //    }
            //}
          
        }

        private void rjCombobox1_Click(object sender, EventArgs e)
        {
            //if (rjCombobox1.Text == "Complaints Search")
            //{
            //    rjCombobox1.Text = "";
            //    rjCombobox1.ForeColor = Color.DarkBlue;
            //    DataTable dt_cmp = this.cntrl.complaint_cell_search(rjCombobox1.Text);
           
           
            //    string[] items = new string[dt_cmp.Rows.Count];
            //    for (int i = 0; i < dt_cmp.Rows.Count; i++)
            //    {
            //        items[i] = dt_cmp.Rows[i][0].ToString();
            //    }
            //rjCombobox1.DataSource = items;
            
            //if (dt_cmp.Rows.Count > 0)
            //{
            //    //lst_cheif.Visible = true;
            //    rjCombobox1.DataSource = dt_cmp;
            //    //rjCombobox1.DisplayMember = "name";
            //    //rjCombobox1.ValueMember = "id";
            //}
            //}
        }

        private void btn_prescription_Click(object sender, EventArgs e)
        {
            btn_prescription.BackColor = Color.White;
            btn_prescription.ForeColor = Color.DarkBlue;
            //btn_clinical_findings.BackColor = Color.FromArgb(32, 43, 61);
            //btn_clinical_findings.ForeColor = Color.White;
            //panel12.BackColor = Color.Black;
            //panel13.BackColor = Color.White;
            pnal_previous_prescription.Visible = false;
            pnal_clinicalfinding.Visible = false;
            pnal_prescription.Visible = true;
            pnal_prescription.Size = new Size(1008, 275);
            pnal_prescription.Location = new Point(0, 30);
            btn_prescription.BackColor = Color.White;
            btn_prescription.ForeColor = Color.DarkBlue;
            btn_clinical_findings.BackColor = Color.FromArgb(32, 43, 61);
            btn_clinical_findings.ForeColor = Color.White;
            btn_previous_prescrption.BackColor = Color.FromArgb(32, 43, 61);
            btn_previous_prescrption.ForeColor = Color.White;
            if (rjCombo_strength.Items.Count == 0)
            {
                DataTable dt = cmodel.Fill_unit_combo();
                if (dt.Rows.Count > 0)
                {
                    string[] items = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        items[i] = dt.Rows[i][1].ToString();
                    }
                    rjCombo_strength.DataSource = items;
                    rjCombo_strength.SelectedIndex = 0;
                }
            }

            if (dataGridView2.Rows.Count==0)
            {
                DataTable dt2 = this.cntrl.get_tmplates();
                dataGridView2.DataSource = dt2;
                dataGridView2.Columns[0].Visible = false;
            }
          
        }

        private void rjBtn_previous_clinical_findings_Click(object sender, EventArgs e)
        {
            clinic_flag = true;
            label13.Visible = true;
            rjcmb_clinic_date.Visible = true;
            userControl_label_complaints.Text = "Cheif Complaints";
            userControl_label_diag.Text = "Diagnosis";
            userControl_label_notes.Text = "Notes";

            DataTable dt_clinic = this.cntrl.get_clinic(patient_id);
            if (dt_clinic.Rows.Count > 0)
            {
                string[] items = new string[dt_clinic.Rows.Count];
                for (int i = 0; i < dt_clinic.Rows.Count; i++)
                {
                    items[i] = Convert.ToDateTime(dt_clinic.Rows[i][1].ToString()).ToString("dd-MM-yyyy");// dt_clinic.Rows[i][1].ToString();
                }
                rjcmb_clinic_date.DataSource = items;


            }
            else
            {
                userControl_label_complaints.Visible = false;
                userControl_label_diag.Visible = false;
                userControl_label_notes.Visible = false;
                userControl_textbox_compla.Visible = false;
                userControl_textbox_diagn.Visible = false;
                userControl_textbox_notes.Visible = false;
            }

            clinic_flag = false;
        }

        private void rjCombobox4_onSelectedIndexChanged(object sender, EventArgs e)
        {
            if (clinic_flag == false)
            {
                if (rjcmb_clinic_date.Items.Count > 0)
                {
                    userControl_label_complaints.Visible = true;
                    userControl_label_diag.Visible = true;
                    userControl_label_notes.Visible = true;
                    userControl_textbox_compla.Visible = true;
                    userControl_textbox_diagn.Visible = true;
                    userControl_textbox_notes.Visible = true;

                    DataTable dt_clinic_id = this.cntrl.get_clinic_id(patient_id, Convert.ToDateTime(rjcmb_clinic_date.SelectedItem.ToString()).ToString("yyyy-MM-dd"));
                    if (dt_clinic_id.Rows.Count > 0)
                    {
                        DataTable dt_complaints = this.cntrl.pt_complaints(dt_clinic_id.Rows[0][0].ToString());
                        if (dt_complaints.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt_complaints.Rows.Count; i++)
                            {
                                userControl_textbox_compla.Text += dt_complaints.Rows[i]["complaint_id"].ToString() + "" + ",";
                            }
                        }
                        DataTable dt_dignosis = this.cntrl.pt_diagnosis(dt_clinic_id.Rows[0][0].ToString());
                        if (dt_dignosis.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt_dignosis.Rows.Count; i++)
                            {
                                userControl_textbox_diagn.Text += dt_dignosis.Rows[i]["diagnosis_id"].ToString() + "" + ",";
                            }
                        }
                        DataTable dt_notes = this.cntrl.pt_notes(dt_clinic_id.Rows[0][0].ToString());
                        if (dt_notes.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt_notes.Rows.Count; i++)
                            {
                                userControl_textbox_notes.Text += dt_notes.Rows[i]["note_name"].ToString() + "" + ",";
                            }
                        }
                    }
                }
            }
        }

        private void userControl_txt_drug_KeyUp(object sender, KeyEventArgs e)
        {
            //if (flag == false)
            //{
                //if (userControl_txt_drug.Text != "")
                //{
                //    // lbPatient.Show();
                //    //listpatientsearch.Location = new Point(45, 84);// txt_Pt_search.Location.X, 110);
                //    DataTable dtdr = this.cntrl.get_prescriptnwthname(userControl_txt_drug.Text);//srch_patient(txt_Pt_search.Text, txt_Pt_search.Text);
                //lst_drug.DataSource = dtdr;
                //lst_drug.DisplayMember = "name";
                //lst_drug.ValueMember = "id";

                //}
                //else
                //{
                //    DataTable dtdr = this.cntrl.get_prescriptnwthname(userControl_txt_drug.Text);
                //lst_drug.DataSource = dtdr;
                //lst_drug.DisplayMember = "name";
                //lst_drug.ValueMember = "id";
                //}
                //if (lst_drug.Items.Count > 0)
                //{
                //lst_drug.Show();
                //}
                //else
                //{
                //lst_drug.Hide();
                //}
           
        }

       

        private void userControl_textbox10_KeyUp(object sender, KeyEventArgs e)
        {
            if (flag == false)
            {
                if (userControl_textbox10.Text != "")
                {
                    // lbPatient.Show();
                    //listpatientsearch.Location = new Point(45, 84);// txt_Pt_search.Location.X, 110);
                    DataTable dtdr = this.cntrl.get_prescriptnwthname(userControl_textbox10.Text);//srch_patient(txt_Pt_search.Text, txt_Pt_search.Text);
                    lst_drug.DataSource = dtdr;
                    lst_drug.DisplayMember = "name";
                    lst_drug.ValueMember = "id";

                }
                else
                {
                    DataTable dtdr = this.cntrl.get_prescriptnwthname(userControl_textbox10.Text);
                    lst_drug.DataSource = dtdr;
                    lst_drug.DisplayMember = "name";
                    lst_drug.ValueMember = "id";
                }
                if (lst_drug.Items.Count > 0)
                {
                    lst_drug.Show();
                }
                else
                {
                    lst_drug.Hide();
                }
            }
        }
        public string drug_type="", id1 = "";
        private void lst_drug_MouseClick(object sender, MouseEventArgs e)
        {
            id1 = lst_drug.SelectedValue.ToString();
            if (id1 != "")
            {
                DataTable dt = this.pmodel.ge_drug(id1);
                if(dt.Rows.Count>0)
                {
                    drug_type = dt.Rows[0][3].ToString();
                }
                else
                    drug_type = "";

                userControl_textbox10.Text = lst_drug.Text.ToString();
                userControl_textbox10.ForeColor = Color.DarkBlue;
            }
            lst_drug.Visible = false;
        }

        private void rjBtn_Prescriptn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                string mrng = "", noon = "", durat = "", night = "", food="";
                if (userControl_textbox10.Text != "" && userControl_textbox10.Text !="Drug Name")
                {
                    string dur = "";
                    food = "";
                    if (radioButtonBfrFood.Checked)
                    {
                        food = radioButtonBfrFood.Text.ToString();
                    }
                    else if (radioButtonAftrFood.Checked)
                    {
                        food = radioButtonAftrFood.Text.ToString();
                    }
                    if (rjCmb_duration.SelectedItem == "")
                    {
                        dur = "";
                    }
                    else
                    {
                        dur = rjCmb_duration.Text;
                    }
                    string strstatus = "1";
                    if (checkBoxShowTime.Checked == true)
                    {
                        strstatus = "1";
                    }
                    else
                    {
                        strstatus = "0";
                    }
                    string Note = "";
                    string NoteData = "";
                    NoteData = uc_txt_addinstruction.Text;
                    Note = NoteData.Replace("'", " ");
                    dgv_prescrptn.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv_prescrptn.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    if (uc_txt_morning.Text == "M")
                    {
                        mrng = "";
                    }
                    else
                    {
                        mrng = uc_txt_morning.Text.ToString();
                    }
                    if (uc_txt_noon.Text =="N")
                    {
                        noon = "";
                    }
                    else
                    {
                        noon = uc_txt_noon.Text.ToString();
                    }
                    if (uc_txt_night.Text == "N")
                    {
                        night = "";
                    }
                    else
                    {
                        night = uc_txt_night.Text.ToString();
                    }
                    if (uc_txt_duration.Text == "0" || uc_txt_duration.Text =="")
                    {
                        durat = "";
                    }
                    else
                    {
                        durat = uc_txt_duration.Text.ToString();
                    }

                    dgv_prescrptn.Rows.Add(userControl_textbox10.Text, uc_txt_strengthno.Text, rjCombo_strength.SelectedItem.ToString(), durat, dur, mrng, noon, night, food, Note, id1, drug_type);// , , numericUpDownNight.Value, 
                    dgv_prescrptn.Rows[dgv_prescrptn.Rows.Count - 1].Cells[12].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                    dgv_prescrptn.Rows[dgv_prescrptn.Rows.Count - 1].Height = 30;
                    img.ImageLayout = DataGridViewImageCellLayout.Normal;
                    dgv_prescrptn.Rows[dgv_prescrptn.Rows.Count - 1].Cells[13].Value = strstatus;
                    Presc_clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Presc_clear()
        {
            userControl_textbox10.Text = ""; 
            uc_txt_strengthno.Text = "";
            uc_txt_duration.Text = "";
            uc_txt_night.Text = ""; 
            uc_txt_noon.Text = ""; uc_txt_morning.Text = "";
            uc_txt_addinstruction.Text = "";
            radioButtonAftrFood.Checked = false;
            radioButtonBfrFood.Checked = false;
            rjCmb_duration.SelectedIndex = 0; rjCombo_strength.SelectedIndex = 0;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //richTxtInsrtuction.Visible = true;
        }

        private void uc_lb_addinstructn_Click(object sender, EventArgs e)
        {
            //uc_txt_addinstruction.Visible = true;
        }

        private void dgv_prescrptn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_prescrptn.Rows.Count > 0)
                {
                    if (e.ColumnIndex == 12)
                    {
                        DialogResult res = MessageBox.Show("Are you sure you want to delete..?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (res == DialogResult.Yes)
                        {
                            dgv_prescrptn.Rows.RemoveAt(this.dgv_prescrptn.SelectedRows[0].Index);
                        }
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        public bool pres_load_flag = false;
        private void rjCmb_prev_pres_date_onSelectedIndexChanged(object sender, EventArgs e)
        {
            if(pres_load_flag==false)
            {
                if (rjCmb_prev_pres_date.Items.Count > 0)
                {
                    DataTable dt_pres = this.cntrl.previous_pres_details(patient_id, Convert.ToDateTime(rjCmb_prev_pres_date.SelectedItem.ToString()).ToString("yyyy-MM-dd") );//Convert.ToDateTime().ToString("yyyy-MM-dd")rjCmb_prev_pres_date.SelectedItem.ToString()
                    if (dt_pres.Rows.Count > 0)
                    {
                        dgv_prev_prescription.Rows.Clear();
                        foreach (DataRow dr in dt_pres.Rows)
                        {
                            //dgv_prev_prescription.Rows.Add();strength  dr["add_instruction"].ToString()
                            dgv_prev_prescription.Rows.Add(dr["dr_name"].ToString(), dr["strength"].ToString(), dr["strength_gr"].ToString(), dr["duration_unit"].ToString(), dr["duration_period"].ToString(), dr["morning"].ToString(), dr["noon"].ToString(), dr["night"].ToString(), dr["food"].ToString(), dr["add_instruction"].ToString(), dr["drug_id"].ToString(), dr["drug_type"].ToString());// , , numericUpDownNight.Value, 
                            //dgv_prev_prescription.Rows[dgv_prev_prescription.Rows.Count - 1].Cells[12].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                            //dgv_prev_prescription.Rows[dgv_prev_prescription.Rows.Count - 1].Height = 30;
                            //img.ImageLayout = DataGridViewImageCellLayout.Normal;
                        }
                    }
                }
            }
            

        }

        private void btn_change_patients_Click(object sender, EventArgs e)
        {
            txt_search.Visible = true;
            label15.Visible = false; 
            label7.Visible = false; 
            pat_name.Visible = false;
            lb_mobile.Visible = false;
            label29.Visible = false;
            label14.Visible = false;
            lb_gender.Visible = false;



        }

        private void btn_newPatient_Click(object sender, EventArgs e)
        {
            //lbPatient.Visible = false;
            if (loginid != "1")
            {
                string id;
                id = db.scalar("select id from tbl_User_Privilege where UserID=" + loginid + " and Category='PAT' and Permission='A'");
                if (int.Parse(id) == 0)
                {
                    MessageBox.Show("There is No Privilege to Add Patient", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                else
                {
                    var form2 = new consultation_new_patient();
                    form2.ShowDialog();
                    form2.Dispose();
                    if (newptid != "")
                    {
                        //txt_procedure.Visible = true;
                        flag = true; string dr_id = "";
                        DataTable dtb = this.cntrl.pt_details(newptid);
                        txt_search.Visible = false;
                        if (dtb.Rows.Count > 0)
                        {

                            pat_name.Visible = true;
                            patient_id = dtb.Rows[0]["id"].ToString();
                            if (dtb.Rows[0]["pt_id"].ToString() != "")
                            {
                                label15.Text = dtb.Rows[0]["pt_id"].ToString();
                                label15.Visible = true; label7.Visible = true;
                            }
                            else
                            {
                                label15.Visible = false; label7.Visible = false;
                            }


                            if (dtb.Rows[0]["pt_name"].ToString() != "")
                                pat_name.Text = dtb.Rows[0]["pt_name"].ToString();
                            if (dtb.Rows[0]["primary_mobile_number"].ToString() != "")
                            {
                                label29.Visible = true;
                                lb_mobile.Visible = true;
                                lb_mobile.Text = dtb.Rows[0]["primary_mobile_number"].ToString();
                            }
                            else
                            {
                                lb_mobile.Visible = false;
                                label29.Visible = false;
                            }


                            if (dtb.Rows[0]["age"].ToString() != "")
                            {
                                label14.Visible = true;
                                label14.Text = dtb.Rows[0]["age"].ToString() + dtb.Rows[0]["days"].ToString();
                            }
                            else
                            {
                                label14.Visible = false;
                            }
                            if (dtb.Rows[0]["gender"].ToString().Trim() != "")
                            {
                                lb_gender.Visible = true;
                                lb_gender.Text = dtb.Rows[0]["gender"].ToString().Trim();
                            }
                            else
                            {
                                lb_gender.Visible = false;
                            }

                            txt_search.Text = "Search by Patient Id, Name";
                            txt_search.ForeColor = Color.LightSlateGray;
                            
                        }
                        string str_doctorname = dtb.Rows[0]["doctorname"].ToString();
                        //string str_doctorname = dtb.Rows[0]["doctorname"].ToString();
                        //int listindex = cmbdoctor.FindStringExact(str_doctorname);
                        //if (listindex > 0)
                        //{
                        rjCmb_doctor.SelectedItem = str_doctorname;
                        dr_id = rjCmb_doctor.SelectedItem.ToString();
                        DataTable dt_doctor_id = this.cntrl.get_doctorname(dr_id);
                        //string str = this.ctrlr.get_consultation();
                        //if(str!="")
                        //{
                        //    txt_procedure.Text = str;
                        //}
                        //else
                        //{
                        userControl_txt_consultation.Visible = true;
                        userControl_txt_consultation.Text = "CONSULTATION";
                        payment_status = "Consultation";
                        //}
                        if (dt_doctor_id.Rows.Count > 0)
                        {
                            userControl_txt_fee.Text = dt_doctor_id.Rows[0]["fee"].ToString();
                            //txtTotal.Text = Convert.ToDecimal( Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txt_cost.Text)).ToString("0.000");
                        }
                        else
                        {
                            userControl_txt_fee.Text = "0.00";
                            //txtTotal.Text = "0.000";
                        }
                        chk_followup.Visible = false;
                        lb_followup.Visible = false;
                        userControl_label10.Visible = false;
                        userControl_txt_last_visited_date.Visible = false;
                        userControl_label9.Visible = true;
                        userControl_txt_fee.Visible = true;
                    }
                    flag = false;
                }
            }
            else
            {
                var form2 = new consultation_new_patient();
                form2.ShowDialog();
                form2.Dispose();
                if (newptid != "")
                {
                    //txt_procedure.Visible = true;
                    flag = true; string dr_id = ""; txt_search.Visible = false;
                    DataTable dtb = this.cntrl.pt_details(newptid);
                    if (dtb.Rows.Count > 0)
                    {

                        pat_name.Visible = true;
                        patient_id = dtb.Rows[0]["id"].ToString();
                        if (dtb.Rows[0]["pt_id"].ToString() != "")
                        {
                            label15.Text = dtb.Rows[0]["pt_id"].ToString();
                            label15.Visible = true; label7.Visible = true;
                        }
                        else
                        {
                            label15.Visible = false; label7.Visible = false;
                        }


                        if (dtb.Rows[0]["pt_name"].ToString() != "")
                            pat_name.Text = dtb.Rows[0]["pt_name"].ToString();
                        if (dtb.Rows[0]["primary_mobile_number"].ToString() != "")
                        {
                            label29.Visible = true;
                            lb_mobile.Visible = true;
                            lb_mobile.Text = dtb.Rows[0]["primary_mobile_number"].ToString();
                        }
                        else
                        {
                            lb_mobile.Visible = false;
                            label29.Visible = false;
                        }


                        if (dtb.Rows[0]["age"].ToString() != "")
                        {
                            label14.Visible = true;
                            label14.Text = dtb.Rows[0]["age"].ToString() + dtb.Rows[0]["days"].ToString();
                        }
                        else
                        {
                            label14.Visible = false;
                        }
                        if (dtb.Rows[0]["gender"].ToString().Trim() != "")
                        {
                            lb_gender.Visible = true;
                            lb_gender.Text = dtb.Rows[0]["gender"].ToString().Trim();
                        }
                        else
                        {
                            lb_gender.Visible = false;
                        }

                        txt_search.Text = "Search by Patient Id, Name";
                        txt_search.ForeColor = Color.LightSlateGray;

                    }
                    string str_doctorname = dtb.Rows[0]["doctorname"].ToString();
                    //int listindex = cmbdoctor.FindStringExact(str_doctorname);
                    //if (listindex > 0)
                    //{
                        rjCmb_doctor.SelectedItem = str_doctorname;
                        dr_id = rjCmb_doctor.SelectedItem.ToString();
                    //}
                    DataTable dt_doctor_id = this.cntrl.get_doctorname(dr_id);
                    //string str = this.ctrlr.get_consultation();
                    //if(str!="")
                    //{
                    //    txt_procedure.Text = str;
                    //}
                    //else
                    //{
                    userControl_txt_consultation.Visible = true;
                    userControl_txt_consultation.Text = "CONSULTATION";
                    payment_status = "Consultation";
                    //}
                    if (dt_doctor_id.Rows.Count > 0)
                    {
                        userControl_txt_fee.Text = dt_doctor_id.Rows[0]["fee"].ToString();
                        //txtTotal.Text = Convert.ToDecimal( Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txt_cost.Text)).ToString("0.000");
                    }
                    else
                    {
                        userControl_txt_fee.Text = "0.00";
                        //txtTotal.Text = "0.000";
                    }
                    chk_followup.Visible = false;
                    lb_followup.Visible = false;
                    userControl_label10.Visible = false;
                    userControl_txt_last_visited_date.Visible = false;
                    userControl_label9.Visible = true;
                    userControl_txt_fee.Visible = true;
                }
                flag = false;
            }
        }

        private void uc_lb_addinstructn_Load(object sender, EventArgs e)
        {
           
        }
        public bool loadflag = false;
        private void cmb_vital_dates_onSelectedIndexChanged(object sender, EventArgs e)
        {
            if(loadflag==false)
            {
                if (cmb_vital_dates.Items.Count > 0)
                {
                    string date = cmb_vital_dates.SelectedItem.ToString();
                    if (date != "")
                        date = Convert.ToDateTime(date).ToString("yyyy-MM-dd");
                    DataTable dt_vital = this.cntrl.previous_vital_details(patient_id,date);
                    if(dt_vital.Rows.Count>0)
                    {
                        // txt_temp cmb_temp txt_blood1 txt_blood2 cmb_blood txt_height  txt_weight txt_resp txt_spo txt_bmi
                        txt_vital_pulse.Text = dt_vital.Rows[0]["pulse"].ToString();
                        txt_temp.Text = dt_vital.Rows[0]["temp"].ToString();
                        cmb_temp.Text = dt_vital.Rows[0]["temp_type"].ToString();
                        txt_blood1.Text = dt_vital.Rows[0]["bp_syst"].ToString();
                        txt_blood2.Text = dt_vital.Rows[0]["bp_dia"].ToString();
                        cmb_blood.Text = dt_vital.Rows[0]["bp_type"].ToString();
                        txt_height.Text = dt_vital.Rows[0]["Height"].ToString();
                        txt_weight.Text = dt_vital.Rows[0]["weight"].ToString();
                        txt_resp.Text = dt_vital.Rows[0]["resp"].ToString();
                        txt_spo.Text = dt_vital.Rows[0]["spo"].ToString();
                        BMI_Calculate(lb_gender.Text);
                        //txt_bmi.Text = dt_vital.Rows[0]["pulse"].ToString();
                    }
                }
            }
           
        }
        public void vitals_clear()
        {
            txt_vital_pulse.Text = "";
            txt_temp.Text = "";
            cmb_temp.SelectedIndex=0;
            txt_blood1.Text ="";
            txt_blood2.Text = "";
            cmb_blood.SelectedIndex = 0;
            txt_height.Text = "";
            txt_weight.Text ="";
            txt_resp.Text = "";
            txt_spo.Text = "";
            //BMI_Calculate(lb_gender.Text);
            txt_bmi.Text = "";
        }
        public string gender = "";double  weight=0, height=0;

        private void rjButtons1_Click_1(object sender, EventArgs e)
        {
            lb_vital_date.Visible = false;
            cmb_vital_dates.Visible = false;
            vitals_clear();
        }

        private void userControl_label21_Load(object sender, EventArgs e)
        {

        }

        private void txt_procedure_KeyUp(object sender, KeyEventArgs e)
        {
            //if(txt_procedure.Text!="")
            //{
                //DataTable dtb_procedure = this.amodel.search_procedures(txt_procedure.Text);
                if (txt_procedure.Text != "")
                {
                    // lbPatient.Show();
                    //listpatientsearch.Location = new Point(45, 84);// txt_Pt_search.Location.X, 110);
                    DataTable dtdr = this.cntrl.search_procedures(txt_procedure.Text);//srch_patient(txt_Pt_search.Text, txt_Pt_search.Text);
                lst_procedure.DataSource = dtdr;
                lst_procedure.DisplayMember = "name";
                lst_procedure.ValueMember = "id";

                }
                else
                {
                    DataTable dtdr = this.cntrl.search_procedures(userControl_textbox10.Text);
                lst_procedure.DataSource = dtdr;
                lst_procedure.DisplayMember = "name";
                lst_procedure.ValueMember = "id";
                }
                if (lst_procedure.Items.Count > 0)
                {
                lst_procedure.Show();
                }
                else
                {
                lst_procedure.Hide();
                }


                //if (dtb_procedure.Rows.Count>0)
                //{
                //    txt_unit.Text = "1";
                //    lbl_procedure_id.Text= dtb_procedure.Rows[0]["id"].ToString();
                //    txt_procedure.Text= dtb_procedure.Rows[0]["name"].ToString();
                //    txtCost.Text= dtb_procedure.Rows[0]["cost"].ToString();
                //}

            //}
        }

        private void lst_procedure_MouseClick(object sender, MouseEventArgs e)
        {
            string id1_ = lst_procedure.SelectedValue.ToString();
            if (id1_ != "")
            {
                DataTable dtb_procedure = this.cntrl.procedures_cost(id1_); ;
                txt_unit.Text = "1";
                lbl_procedure_id.Text = dtb_procedure.Rows[0]["id"].ToString();
                txt_procedure.Text = dtb_procedure.Rows[0]["name"].ToString();
                txtCost.Text = dtb_procedure.Rows[0]["cost"].ToString();
                txt_total.Text= dtb_procedure.Rows[0]["cost"].ToString();
                txt_procedure.ForeColor = Color.DarkBlue;
             }
            lst_procedure.Visible = false;
        }

        private void txt_unit_Load(object sender, EventArgs e)
        {

        }

        private void txt_unit_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                //if (invoicetype == "drug")
                //{
                //    if (status == 1)
                    //{
                        if (!String.IsNullOrWhiteSpace(txt_unit.Text))
                        {
                            int qty = 0;
                            //DataTable testQty = this.cntrl.Get_quantiry_fromStock(stock_id);
                            //if (testQty.Rows.Count > 0)
                            //{
                            //    qty = int.Parse(testQty.Rows[0][0].ToString());
                            //}
                            //if (int.Parse(txt_unit.Text) > qty || int.Parse(txt_unit.Text) == 0)
                            //{
                            //    lab_Msg.Show();
                            //}
                            //else
                            //{
                            //    lab_Msg.Hide();
                            //}
                        }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Choose an Item From Left By Just clicking on an Item..", "No Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                //}
                calculations();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        Decimal discounttotal = 0;
        Decimal taxrstotal = 0; Decimal P_tax = 0;
        public void calculations()
        {
            decimal qty1 = 0;
            decimal cost = 0;
            decimal discount = 0;
            if (!String.IsNullOrWhiteSpace(txt_unit.Text))
            {
                qty1 = Convert.ToDecimal(txt_unit.Text);
            }
            if (!String.IsNullOrWhiteSpace(txtCost.Text))
            {
                cost = Convert.ToDecimal(txtCost.Text);
            }
            if (!String.IsNullOrWhiteSpace(txt_disc.Text))
            {
                if (cmb_discount.SelectedItem.ToString() == "INR")
                {
                    discount = Convert.ToDecimal(txt_disc.Text);
                }
                else
                {
                    discount = ((qty1 * cost) * Convert.ToDecimal(txt_disc.Text)) / 100;
                }
            }
            txt_total.Text = Convert.ToString((qty1 * cost) - discount);
            discounttotal = discount;
            taxrstotal = 0;
            if (P_tax > 0)
            {
                txt_total.Text = Convert.ToString(Convert.ToDecimal(txt_total.Text) + ((qty1 * cost) * P_tax / 100));
                taxrstotal = (qty1 * cost) * P_tax / 100;
            }
        }

        private void txt_disc_KeyUp(object sender, KeyEventArgs e)
        {
            calculations();
        }

        private void cmb_discount_onSelectedIndexChanged(object sender, EventArgs e)
        {
            if(load_flag==false)
            {
                decimal qty = 0;
                decimal cost = 0;
                decimal discount = 0;
                if (txt_unit.Text != "")
                {
                    qty = Convert.ToDecimal(txt_unit.Text);
                }
                if (txtCost.Text != "")
                {
                    cost = Convert.ToDecimal(txtCost.Text);
                }
                if (txt_disc.Text != "")
                {
                    if (cmb_discount.SelectedItem.ToString() == "INR")
                    {
                        discount = Convert.ToDecimal(txt_disc.Text);
                    }
                    else
                    {
                        discount = ((qty * cost) * Convert.ToDecimal(txt_disc.Text)) / 100;
                    }
                }
                txt_total.Text = Convert.ToString((qty * cost) - discount);
                discounttotal = discount;
                taxrstotal = 0;
                if (P_tax > 0)
                {
                    txt_total.Text = Convert.ToString(Convert.ToDecimal(txt_total.Text) - ((qty * cost) * P_tax / 100));
                    taxrstotal = (qty * cost) * P_tax / 100;
                }
            }
           
        }

        private void txt_unit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCost_KeyUp(object sender, KeyEventArgs e)
        {
            calculations();
        }

        private void cmb_tax_onSelectedIndexChanged(object sender, EventArgs e)
        {
            if(load_flag==false)
            {
                string ctax = cmb_tax.SelectedItem.ToString();
                if (cmb_tax.SelectedIndex == 4)
                {
                    P_tax = 0;
                }
                else
                {
                    string dt = this.cntrl.select_taxValue(cmb_tax.SelectedItem.ToString());
                    if (Convert.ToDecimal(dt) > 0)
                    {
                        P_tax = Convert.ToDecimal(dt);
                    }
                    else
                    {
                        P_tax = 0;
                    }
                }
                calculations();
            }
          
        }
        public string lab_service = "No";

        private void lb_add_disc_Click(object sender, EventArgs e)
        {
            lb_add_disc.Visible = false;
            cmb_discount.Show();
            txt_disc.Show();
        }

        private void lb_add_tax_Click(object sender, EventArgs e)
        {
            lb_add_tax.Visible = false;
            cmb_tax.Show();
        }

        int presid;
        private void btn_save_Click(object sender, EventArgs e)
        {
            
            if(patient_id!="")
            {
                if(doctor_id !="")
                {
                    if ((userControl_txt_consultation.Text != "") && (userControl_txt_consultation.Text == "CONSULTATION"))
                    {
                        if (Convert.ToDecimal(userControl_txt_fee.Text) <= 0)
                        {
                            MessageBox.Show("Doctor Fee not found , please enter the Fee . ", "Doctor Fee Not Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            userControl_txt_fee.Focus();
                            return;
                        }

                    }
                    if (dgv_prescrptn.Rows.Count == 0)
                    {
                        DialogResult yesno = MessageBox.Show("You missed to Click on 'Add' button under prescription. Please click 'Add' button to proceed Or Do you want to save without prescription ?", "Prescription Not Added", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (yesno == DialogResult.No)
                        { return; }
                    }
                    if (DGV_Invoice.Rows.Count == 0)
                    {
                        DialogResult yesno = MessageBox.Show("You missed to Click on 'Add' button under Invoice. Please click 'Add' button to proceed Or Do you want to save without Invoice ?", "Invoice Not Added", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (yesno == DialogResult.No)
                        { return; }
                    }
                     string cs = PappyjoeMVC.Model.Connection.MyGlobals.trans_connectionstring;
                    using (MySqlConnection con = new MySqlConnection(cs))
                    {
                        con.Open();
                        MySqlTransaction trans = con.BeginTransaction();

                        try
                        {
                            int treat = 0;
                            string dt = DateTime.Now.Date.ToString("yyyy-MM-dd");
                            DateTime Timeonly = DateTime.Now;
                            if (userControl_text_complaints.Text != "" || userControl_text_Diagnosis.Text != "" || userControl_text_notes.Text != "")
                            {
                                
                                this.cntrl.insertInto_clinical_findings(patient_id, doctor_id, dateTimePicker2.Value.ToString("yyyy-MM-dd"), con, trans);
                                string treatment = this.cntrl.MaxId_clinic_findings(con, trans);
                                if (Convert.ToInt32(treatment) > 0)
                                {
                                    treat = int.Parse(treatment);
                                }
                                else
                                {
                                    treat = 1;
                                }
                                if (userControl_text_complaints.Text != "")
                                {
                                    string s = userControl_text_complaints.Text;
                                    string[] values = s.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                                    foreach (string items in values)
                                    {
                                        string one = items;
                                        this.cntrl.insrtto_chief_comp(treat, one, con, trans);
                                    }
                                }
                                if (userControl_text_Diagnosis.Text != "")
                                {
                                    string s = userControl_text_Diagnosis.Text;
                                    string[] values = s.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                                    foreach (string items in values)
                                    {
                                        string one = items;
                                        this.cntrl.insrtto_diagno(treat, one, con, trans);
                                    }
                                }
                                if (userControl_text_notes.Text != "")
                                {
                                    string s = userControl_text_notes.Text;
                                    string[] values = s.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                                    foreach (string items in values)
                                    {
                                        string one = items;
                                        this.cntrl.insrtto_note(treat, one, con, trans);
                                    }
                                }
                            }//clinical notes end
                             //vitals
                            string temp_type = "", bp_type = ""; int i = 0; string maxid = "";
                            if (!String.IsNullOrWhiteSpace(txt_temp.Text))
                            {
                                temp_type = cmb_temp.SelectedItem.ToString();
                            }
                            if (!String.IsNullOrWhiteSpace(txt_blood1.Text) || !String.IsNullOrWhiteSpace(txt_blood2.Text))
                            {
                                bp_type = cmb_blood.SelectedItem.ToString();
                            }
                            if (!String.IsNullOrWhiteSpace(txt_vital_pulse.Text)&& txt_vital_pulse.Text!= "( Heart Beats Per Minute )" || !String.IsNullOrWhiteSpace(txt_temp.Text)&& txt_temp.Text !="(C)" || !String.IsNullOrWhiteSpace(txt_height.Text) && txt_height.Text!="(Cm)" || !String.IsNullOrWhiteSpace(txt_blood1.Text)&& txt_blood1.Text != "(mm Hg)" || !String.IsNullOrWhiteSpace(txt_spo.Text) && txt_spo.Text !="(%)"|| !String.IsNullOrWhiteSpace(txt_resp.Text)&& txt_resp.Text != "(Breaths/min)" || !String.IsNullOrWhiteSpace(txt_weight.Text)&& txt_weight.Text !="(Kg)")
                            {
                                if (txt_weight.Text == "(Kg)")
                                    txt_weight.Text = "";
                                if (txt_resp.Text == "(Breaths/min)")
                                    txt_resp.Text = "";
                                if (txt_spo.Text == "(%)")
                                    txt_spo.Text = ""; 
                                if (txt_blood1.Text == "(mm Hg)")
                                    txt_blood1.Text = "";
                                if (txt_blood2.Text == "(mm Hg)")
                                    txt_blood2.Text = "";
                                if (txt_height.Text == "(Cm)")
                                    txt_height.Text = "";
                                if (txt_temp.Text == "(C)")
                                    txt_temp.Text = "";
                                if (txt_vital_pulse.Text == "( Heart Beats Per Minute )")
                                    txt_vital_pulse.Text = "";
                                    this.cntrl.save_vital_main(patient_id, doctor_id, dateTimePicker2.Value.ToString("yyyy-MM-dd"), con, trans);
                                DataTable dt_max = this.cntrl.dt_get_maxid(con, trans);
                                if (dt_max.Rows.Count > 0)
                                {
                                    maxid = dt_max.Rows[0][0].ToString();
                                }
                                i = this.cntrl.submit(patient_id, doctor_id, rjCmb_doctor.SelectedItem.ToString(), temp_type, bp_type, txt_vital_pulse.Text, txt_temp.Text, txt_blood1.Text, txt_blood2.Text, txt_weight.Text, txt_resp.Text, dateTimePicker2.Value.ToString("yyyy-MM-dd"), txt_height.Text, txt_spo.Text, maxid, con, trans);

                               
                            }//vitals end
                             //prescription
                            string strstatus = "1";
                            if (dgv_prescrptn.Rows.Count > 0)
                            {
                                this.cntrl.save_prescriptionmain(patient_id, doctor_id, dateTimePicker2.Value.ToString("yyyy-MM-dd"), Prescription_bill_status, uc_txt_addinstruction.Text, con, trans);
                                string dt1 = this.cntrl.Maxid(con, trans);
                                if (Convert.ToInt32(dt1) > 0)
                                {
                                    presid = Int32.Parse(dt1);
                                }
                                else
                                {
                                    presid = 1;
                                }
                              int count = dgv_prescrptn.Rows.Count;
                                for (int p = 0; p < count; p++)
                                {
                                    if (dgv_prescrptn[13, p].Value.ToString() != "")
                                    { strstatus = dgv_prescrptn[13, p].Value.ToString(); }
                                    this.cntrl.save_prescription(presid, patient_id, rjCmb_doctor.SelectedItem.ToString(), doctor_id, dateTimePicker2.Value.ToString("yyyy-MM-dd"), dgv_prescrptn[0, p].Value.ToString(), dgv_prescrptn[1, p].Value.ToString(), dgv_prescrptn[2, p].Value.ToString(), dgv_prescrptn[3, p].Value.ToString(), dgv_prescrptn[4, p].Value.ToString(), dgv_prescrptn[5, p].Value.ToString(), dgv_prescrptn[6, p].Value.ToString(), dgv_prescrptn[7, p].Value.ToString(), dgv_prescrptn[8, p].Value.ToString(), dgv_prescrptn[9, p].Value.ToString(), dgv_prescrptn[11, p].Value.ToString(), strstatus, dgv_prescrptn[10, p].Value.ToString(), con, trans);
                                }

                            }
                            //treatment and completed
                            int j_Review = 0;
                            this.TModel.Save_treatment(doctor_id, patient_id, dateTimePicker2.Value.ToString("yyyy-MM-dd"), rjCmb_doctor.SelectedItem.ToString(), pat_name.Text, txtTotalCost.Text, txtTotalDisc.Text, txt_grand_total.Text, con, trans);
                            string dti = this.TModel.get_treatmentmaxid(con, trans);
                            int planid; int j1,k=0;
                            if (Int32.Parse(dti) == 0)
                            {
                                j1 = 1;
                                planid = 1;
                            }
                            else
                            {
                                planid = Int32.Parse(dti);
                            }
                            j1 = planid;

                            for (int ii = 0; ii < DGV_Invoice.Rows.Count; ii++)
                            {
                                if (DGV_Invoice.Rows[ii].Cells["Tonurse"].Value.ToString() == "Yes")
                                {
                                    flag = true;
                                    this.TModel.Save_treatmentgrid_set_ststus(j1, DGV_Invoice.Rows[ii].Cells["service_id"].Value.ToString(), patient_id, DGV_Invoice.Rows[ii].Cells["service"].Value.ToString(), DGV_Invoice.Rows[ii].Cells[2].Value.ToString(), DGV_Invoice.Rows[ii].Cells["cost"].Value.ToString(), DGV_Invoice.Rows[ii].Cells["disc_type"].Value.ToString(), DGV_Invoice.Rows[ii].Cells["discount"].Value.ToString(), DGV_Invoice.Rows[ii].Cells["total"].Value.ToString(), DGV_Invoice.Rows[ii].Cells["discount_in_rs"].Value.ToString(), DGV_Invoice.Rows[ii].Cells[12].Value.ToString(), DGV_Invoice.Rows[ii].Cells["Tooth1"].Value.ToString(), con, trans);

                                    this.fmodel.save_completed_id_trans(dateTimePicker2.Value.ToString("yyyy-MM-dd"), patient_id, con, trans);
                                    string max_id = this.fmodel.get_completedMaxid_trans(con, trans);
                                    string treat_id = this.TModel.get_treatmentplan_id(Int32.Parse(maxid), DGV_Invoice.Rows[ii].Cells["service_id"].Value.ToString(), patient_id, con, trans);//
                                    int completed_id;
                                    if (Int32.Parse(max_id) == 0)//dt
                                    {
                                        k = 1;
                                        completed_id = 0;
                                    }
                                    else
                                    {
                                        completed_id = Int32.Parse(max_id);
                                    }
                                    k = completed_id;
                                    j_Review = completed_id;
                                    this.cntrl.save_completed_items_trans(k, patient_id, DGV_Invoice.Rows[ii].Cells["service_id"].Value.ToString(), DGV_Invoice.Rows[ii].Cells["service"].Value.ToString(), DGV_Invoice.Rows[ii].Cells[2].Value.ToString(), DGV_Invoice.Rows[ii].Cells["cost"].Value.ToString(), DGV_Invoice.Rows[ii].Cells["disc_type"].Value.ToString(), DGV_Invoice.Rows[ii].Cells["discount"].Value.ToString(), DGV_Invoice.Rows[ii].Cells["total"].Value.ToString(), DGV_Invoice.Rows[ii].Cells["discount_in_rs"].Value.ToString(), DGV_Invoice.Rows[ii].Cells[12].Value.ToString(), dateTimePicker2.Value.ToString("yyyy-MM-dd"),doctor_id, j1.ToString(), DGV_Invoice.Rows[ii].Cells["Tooth1"].Value.ToString(), "Yes", con, trans);
                                }
                                else
                                {
                                    this.fmodel.save_completed_id_trans(dateTimePicker2.Value.ToString("yyyy-MM-dd"), patient_id, con, trans);
                                    string max_id = this.fmodel.get_completedMaxid_trans(con, trans);
                                    int completed_id;
                                    if (Int32.Parse(max_id) == 0)//dt
                                    {
                                        k = 1;
                                        completed_id = 0;
                                    }
                                    else
                                    {
                                        completed_id = Int32.Parse(max_id);
                                    }
                                    k = completed_id;
                                    j_Review = completed_id;
                                    this.cntrl.Save_treatmentgrid(j1, DGV_Invoice.Rows[ii].Cells["service_id"].Value.ToString(), patient_id, DGV_Invoice.Rows[ii].Cells["service"].Value.ToString(), DGV_Invoice.Rows[ii].Cells[2].Value.ToString(), DGV_Invoice.Rows[ii].Cells["cost"].Value.ToString(), DGV_Invoice.Rows[ii].Cells["disc_type"].Value.ToString(), DGV_Invoice.Rows[ii].Cells["discount"].Value.ToString(), DGV_Invoice.Rows[ii].Cells["total"].Value.ToString(), DGV_Invoice.Rows[ii].Cells["discount_in_rs"].Value.ToString(), DGV_Invoice.Rows[ii].Cells[12].Value.ToString(), DGV_Invoice.Rows[ii].Cells["Tooth1"].Value.ToString(), con, trans);

                                    this.cntrl.save_completed_items(k, patient_id, DGV_Invoice.Rows[ii].Cells["service_id"].Value.ToString(), DGV_Invoice.Rows[ii].Cells["service"].Value.ToString(), DGV_Invoice.Rows[ii].Cells[2].Value.ToString(), DGV_Invoice.Rows[ii].Cells["cost"].Value.ToString(), DGV_Invoice.Rows[ii].Cells["disc_type"].Value.ToString(), DGV_Invoice.Rows[ii].Cells["discount"].Value.ToString(), DGV_Invoice.Rows[ii].Cells["total"].Value.ToString(), DGV_Invoice.Rows[ii].Cells["discount_in_rs"].Value.ToString(), DGV_Invoice.Rows[ii].Cells[12].Value.ToString(), dateTimePicker2.Value.ToString("yyyy-MM-dd"), doctor_id, j1.ToString(), DGV_Invoice.Rows[ii].Cells["Tooth1"].Value.ToString(), con, trans);

                                }
                               
                            }
                            //review date
                            if (checkBoxReview.Checked == true)
                            {
                                this.fmodel.update_review(dateTime_review.Value.ToString("yyyy-MM-dd HH:mm"), j_Review,con,trans);
                                DataTable dt_review = this.cmodel.get_reviewId(patient_id, dateTime_review.Value.ToString("yyyy-MM-dd HH:mm"),con,trans);//
                                if (dt_review.Rows.Count == 0)
                                {
                                    this.cmodel.save_review(dateTime_review.Value.ToString("yyyy-MM-dd HH:mm"), patient_id, con, trans);
                                    this.cmodel.save_appointment(dateTime_review.Value.ToString("yyyy-MM-dd HH:mm"), patient_id, pat_name.Text, doctor_id, lb_mobile.Text, rjCmb_doctor.SelectedItem.ToString(), con, trans);
                                }
                            }
                            else
                            {
                                this.fmodel.update_review(dateTime_review.Value.ToString("yyyy-MM-dd HH:mm"), j_Review, con, trans);
                            }

                            ///invoice
                            ///
                            int j = 1;
                            this.amodel.save_invoice_main(patient_id, pat_name.Text.ToString(), txt_invoiceno.Text.ToString(), con, trans);
                            string dt_1 = this.amodel.get_invoiceMain_maxid(con, trans);
                            int invoice_main_id = 0;
                            try
                            {
                                if (Int32.Parse(dt_1) == 0)
                                {
                                    j = 1;
                                    invoice_main_id = 0;
                                }
                                else
                                {
                                    invoice_main_id = Int32.Parse(dt_1);
                                }
                            }
                            catch
                            {
                                j = 1;
                                invoice_main_id = 0;
                            }
                            j = invoice_main_id;
                            for(int l=0;l<DGV_Invoice.Rows.Count;l++)
                            {
                                this.amodel.save_invoice_items(txt_invoiceno.Text.ToString(), pat_name.Text.ToString(), patient_id, DGV_Invoice[0, l].Value.ToString(), DGV_Invoice[1, l].Value.ToString(), DGV_Invoice[3, l].Value.ToString(), DGV_Invoice[2, l].Value.ToString(), DGV_Invoice[4, l].Value.ToString(), DGV_Invoice[5, l].Value.ToString(), DGV_Invoice[6, l].Value.ToString(), DGV_Invoice[7, l].Value.ToString(), DGV_Invoice[12, l].Value.ToString(), DGV_Invoice[8, l].Value.ToString(), DGV_Invoice[9, l].Value.ToString(), DGV_Invoice[10, l].Value.ToString(), DGV_Invoice[8, l].Value.ToString(), DGV_Invoice[9, l].Value.ToString(), j, DGV_Invoice[14, l].Value.ToString(), Convert.ToInt32(DGV_Invoice[15, l].Value.ToString()), DGV_Invoice.Rows[l].Cells["Tonurse"].Value.ToString(), DGV_Invoice.Rows[l].Cells["labservice"].Value.ToString(), con, trans);
                                this.amodel.Set_completed_status0(DGV_Invoice[15, l].Value.ToString(), con, trans);
                            }
                         
                            this.amodel.update_invoice_nurse_notify("True", dt_1, con, trans);////nurse notification

                            string invoauto = this.cntrl.get_invoicenumber(con, trans);
                            int invoautoup = int.Parse(invoauto) + 1;
                            this.cntrl.update_invnumber(invoautoup.ToString(), con, trans);
                            //followup
                            this.cntrl.save_followup(patient_id, pat_name.Text, DateTime.Now.Date.ToString("yyyy-MM-dd"), DateTime.Now.Date.ToString("yyyy-MM-dd"), rjCmb_doctor.SelectedItem.ToString(),doctor_id, userControl_txt_fee.Text, payment_status, con, trans);
                            //log
                            this.cmodel.save_log(loginid, "Treatment Plan", " Fast Track", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Add", j1.ToString(), con, trans);
                            this.cmodel.save_log(loginid, "Finished Procedure", " Fast Track", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Add", k.ToString(), con, trans);
                            this.cmodel.save_log(loginid, "Invoice", "Fast Track", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Add",j.ToString(), con, trans);
                            this.cmodel.save_log(loginid, "Prescription Add", "Fast Track", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Add", presid.ToString(), con, trans);
                            this.cmodel.save_log(loginid, "Vital Sign", " Fast Track", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Add", maxid, con, trans);
                            this.cmodel.save_log(loginid, "Clinical Findings", " Fast Track", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Add", treat.ToString(), con, trans);
                            trans.Commit();
                            con.Close();
                            DialogResult print_yesno = System.Windows.Forms.DialogResult.No;
                            print_yesno = MessageBox.Show("Data saved successfully, Do you want a print ?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (print_yesno == System.Windows.Forms.DialogResult.Yes)
                            {
                                printboth(presid, j, maxid, treat);

                            }
                            Allclear();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            con.Close();
                            MessageBox.Show(ex.Message, "SAVE function is failed !..", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return;
                        }
                       

                    }

                }
                else
                {
                    MessageBox.Show("Please Select a Doctor !.", "Doctor Missing", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                MessageBox.Show("Please Select a Patient First !.","Patient Missing",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
        string logo_name = ""; System.Drawing.Image logo = null;
        public void printboth(int Prescription_id, int invoice_id, string vital_id, int clinic_id)
        {
            try
            {
                System.Data.DataTable dtp = this.cmodel.get_company_details();
                System.Data.DataTable dt1 = this.cmodel.Get_Patient_Details(patient_id);
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
                string doctor = this.cmodel.Get_DoctorName(doctor_id.ToString());
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
                }
                string strfooter1 = "";
                string strfooter2 = "";
                string strfooter3 = "";
                string header1 = "";
                string header2 = "";
                string header3 = "";
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                StreamWriter sWrite = new StreamWriter(Apppath + "\\fasttrack.html");
                sWrite.WriteLine("<html>");
                sWrite.WriteLine("<head>");
                sWrite.WriteLine("</head>");
                sWrite.WriteLine("<body >");
                sWrite.WriteLine("<br>");
                if (logo != null || logo_name != "")
                {
                    string Appath = System.IO.Directory.GetCurrentDirectory();
                    if (File.Exists(Appath + "\\" + logo_name))
                    {
                        sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td width='30px' height='50px' align='left' rowspan='3'><img src='" + Appath + "\\" + logo_name + "'style='width:70px;height:70px;' ></td>  ");
                        sWrite.WriteLine("<td width='870px' align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=4><b>&nbsp;" + clinicn + "</font> <br><FONT  COLOR=black  face='Segoe UI' SIZE=2>&nbsp;" + streetaddress + "<br>Phone Number:&nbsp;" + contact_no + " </b></td></tr>");
                        sWrite.WriteLine("</table>");
                    }
                    else
                    {
                        sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td  align='center' height='20px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + clinicn + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td  align='center' height='20px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;" + streetaddress + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td align='center' height='20px' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>Phone Number:&nbsp;" + contact_no + "</font></td></tr>");
                        sWrite.WriteLine("</table>");
                    }
                }
                else
                {
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td  align='center' height='20px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + clinicn + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td  align='center' height='20px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;" + streetaddress + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='center' height='20px' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>Phone Number:&nbsp;" + contact_no + "</font></td></tr>");
                    sWrite.WriteLine("</table>");
                }
                sWrite.WriteLine("<tr><td align='left'  ><hr/></td></tr>");
                string sexage = ""; string age = ""; string days = "";
                int Dexist = 0;
                string strNote = "";
                string strreview = "NO";
                string strreview_date = "";
                if (chk_patients.Checked == true)
                {
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3><b>Patient Details<b></FONT></td>");
                    sWrite.WriteLine(" </tr>");
                    if (dt1.Rows[0]["gender"].ToString() != "")
                    {
                        sexage = dt1.Rows[0]["gender"].ToString();
                        Dexist = 1;
                    }
                    days = dt1.Rows[0]["days"].ToString();

                    if (dt1.Rows[0]["age"].ToString() != "")
                    {
                        age = dt1.Rows[0]["age"].ToString() + dt1.Rows[0]["days"].ToString();
                    }
                    if (dt1.Rows[0]["age2"].ToString() != "")
                    {
                        if (!string.IsNullOrEmpty(age))
                        {
                            age = age + " " + dt1.Rows[0]["age2"].ToString() + "Months";
                        }
                        else
                        {
                            age = dt1.Rows[0]["age2"].ToString() + "Months";
                        }
                    }
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine(" </br>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine(" <td align='left' height=25><FONT COLOR=black FACE='Geneva, Arial' SIZE=2><b>" + dt1.Rows[0]["pt_name"].ToString() + "</b><i> (" + sexage + "/" + age + ")</i></font></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>Patient Id:&nbsp;" + dt1.Rows[0]["pt_id"].ToString() + " </font></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>Phone Number:&nbsp;" + dt1.Rows[0]["primary_mobile_number"].ToString() + " </font></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine("<tr><td colspan=2><hr></td></tr>");
                    sWrite.WriteLine("</table>");
                }
                //doctor
                sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3><b>Doctor Details<b></FONT></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>Doctor Name:&nbsp;" + rjCmb_doctor.SelectedItem.ToString() + " </font></td>");
                sWrite.WriteLine("<tr><td colspan=2><hr></td></tr>");
                sWrite.WriteLine("</table>");
                if (chk_vitals.Checked == true)//vital
                {
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3><b>Vitals Signs</b></FONT></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine("<tr >");
                    sWrite.WriteLine("<td align='left' width='20px' height='8'><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;</font></td>");
                    sWrite.WriteLine("<td align='left' width='160px' ><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;</font></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine(" <tr>");
                    sWrite.WriteLine("<td align='left'  width='20px' height='30' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Plus( Heart Beats Per Minute )  </font></td>");
                    sWrite.WriteLine("<td align='left'  width='160px' height='30' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + txt_vital_pulse.Text + " </font></td>");
                    sWrite.WriteLine(" </tr>"); 
                    sWrite.WriteLine(" <tr>");
                    sWrite.WriteLine("<td align='left' width='20px' height='30' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Temperature (C)      </font></td>");
                    sWrite.WriteLine("<td align='left'width='160px' height='30'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + txt_temp.Text + " </font></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine(" <tr>");
                    sWrite.WriteLine(" <td align='left' width='20px' height='30'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Blood Pressure(mm Hg)    </font></td>");
                    sWrite.WriteLine("<td align='left'width='160px' height='30'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + txt_blood1.Text + " /" + txt_blood2.Text + " </font></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine(" <tr>");
                    sWrite.WriteLine("<td align='left'width='20px' height='30' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Height(Cm)  </font></td>");
                    sWrite.WriteLine("<td align='left' width='160px' height='30'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + txt_height.Text + " </font></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine(" <tr>");
                    sWrite.WriteLine("<td align='left'width='20px' height='30' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Weight(Kg)</font></td>");
                    sWrite.WriteLine("<td align='left' width='160px' height='30'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + txt_weight.Text + " </font></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine(" <tr>");
                    sWrite.WriteLine("<td align='left'width='20px' height='30' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Resp.Rate((Breaths/min) )   </font></td>");
                    sWrite.WriteLine("<td align='left' width='160px' height='30' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> " + txt_resp.Text + " </font></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine(" <tr>");
                    sWrite.WriteLine("<td align='left'width='20px' height='30' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>SPO2 (%)</font></td>");
                    sWrite.WriteLine("<td align='left'width='160px' height='30' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + txt_spo.Text + " </font></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine(" <tr>");
                    sWrite.WriteLine("<td align='left'width='20px' height='30' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>BMI</font></td>");
                    sWrite.WriteLine("<td align='left' width='160px' height='30'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> " + txt_bmi.Text + " " + label10.Text + "</font></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine("<tr><td colspan=2><hr></td></tr>");
                    sWrite.WriteLine("</table>");
                }
                if (chk_clinic.Checked == true)
                {
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>Clinical Findings</FONT></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine("<tr >");
                    sWrite.WriteLine("<td align='left' width='20px' height='8'><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;</font></td>");
                    sWrite.WriteLine("<td align='left' width='160px' ><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;</font></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine(" </br>");
                    sWrite.WriteLine(" <tr>");
                    sWrite.WriteLine("<td align='left'width='20px' height='50' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>Cheif Complaints</font></td>");
                    sWrite.WriteLine("<td align='left'width='400px' height='50' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>" + userControl_text_complaints.Text + " </font></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine(" <tr>");
                    sWrite.WriteLine("<td align='left' width='20px' height='50' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>Diagnosis </font></td>");//3333333
                    sWrite.WriteLine("<td align='left' width='400px' height='50'><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>" + userControl_text_Diagnosis.Text + " </font></td>");//3333333
                    sWrite.WriteLine(" <tr>");
                    sWrite.WriteLine("<td align='left' width='20px' height='50' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>Notes </font></td>");
                    sWrite.WriteLine("<td align='left'width='400px' height='50' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>" + userControl_text_notes.Text + " </font></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine("<tr><td colspan=2><hr></td></tr>");
                    sWrite.WriteLine("</table>");
                }
                if (chk_prescription.Checked == true)
                {
                    string doctorname = "";
                    System.Data.DataTable dtcf = this.pmodel.table_details(Prescription_id.ToString(), patient_id);
                    if (dtcf.Rows.Count > 0)
                    {
                        doctorname = Convert.ToString(dtcf.Rows[0]["doctor_name"].ToString());
                        strNote = dtcf.Rows[0]["notes"].ToString();
                        if (dtcf.Rows[0]["review"].ToString() == "YES")
                        {
                            strreview = "YES";
                            strreview_date = Convert.ToDateTime(dtcf.Rows[0]["Review_date"].ToString()).ToString("dd-MM-yyyy hh:mm tt");
                        }
                        else
                        {
                            strreview = "NO";
                        }
                    }
                    sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=5>Prescription</FONT></td>");
                    sWrite.WriteLine("<td width=250px></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("</table>");
                    sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");
                    sWrite.WriteLine("<tr >");
                    sWrite.WriteLine("<td align='left' width='35px' height='30'><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;Sl.</font></td>");
                    sWrite.WriteLine("<td align='left' width='160px' ><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;Drug Name</font></td>");
                    sWrite.WriteLine("<td align='center' width='105px' ><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;Strength </font></td>");
                    sWrite.WriteLine("<td align='center' width='114px' colspan='4' ><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;Frequency</font></td>");
                    sWrite.WriteLine("<td align='left' width='160px'><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;Instructions</font></td>");
                    sWrite.WriteLine("</tr>");
                    System.Data.DataTable dt_prescription = this.pmodel.prescription_detoails(Prescription_id.ToString());
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
                            duration = dt_prescription.Rows[k]["duration_unit"].ToString();
                            if (duration.Trim() == "0" || duration == "")
                            {
                                duration = "";
                            }
                            else
                            {
                                duration = dt_prescription.Rows[k]["duration_unit"].ToString() + "" + dt_prescription.Rows[k]["duration_period"].ToString();
                            }
                            if (dt_prescription.Rows[k]["status"].ToString() == "1")
                            {
                                sWrite.WriteLine("<tr>");
                                sWrite.WriteLine("<td align='left' height='7'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1></font></td>");
                                sWrite.WriteLine("<td align='left' height='7'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1></font></td>");
                                sWrite.WriteLine("<td align='left' height='7'  ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1></font></td>");
                                sWrite.WriteLine("<td align='center' height='7' valign='bottom'  width='50px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;Morning </font></td>");
                                sWrite.WriteLine("<td align='center' height='7'  valign='bottom' width='50px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;Noon </font></td>");
                                sWrite.WriteLine("<td align='center' height='7' valign='bottom'  width='50px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;Night </font></td>");
                                sWrite.WriteLine("<td align='center' height='7' valign='bottom'  width='50px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;Duration </font></td>");
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
                            sWrite.WriteLine("<td align='center' valign='top' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + dt_prescription.Rows[k]["strength"].ToString() + " " + dt_prescription.Rows[k]["strength_gr"].ToString() + " </font></td>");
                            sWrite.WriteLine("<td align='center' valign='top' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + morning + " </font></td>");
                            sWrite.WriteLine("<td align='center' valign='top' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + noon + " </font></td>");
                            sWrite.WriteLine("<td align='center' valign='top' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + night + " </font></td>");
                            sWrite.WriteLine("<td align='left'   valign='top'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>&nbsp;" + duration + "</br>" + dt_prescription.Rows[k]["food"].ToString() + " </font></td>");
                            sWrite.WriteLine("<td align='left' valign='top' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1.5>&nbsp;" + dt_prescription.Rows[k]["add_instruction"].ToString() + " </font></td>");
                            sWrite.WriteLine("</tr>");
                        }
                    }
                    sWrite.WriteLine("<tr><td colspan=8><hr></td></tr>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("</table>");
                }
                if (chk_invoice.Checked == true)
                {
                    sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=5  height='10'>R</font><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>x&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</font><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=5>Invoice</FONT></td>");
                    sWrite.WriteLine("<td width=250px></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("</table>");
                    sWrite.WriteLine("<table width='100%' align='center' style='width:100%;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left'  width='180'  height='14'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Procedure Name</b></font></td>");
                    sWrite.WriteLine("<td align='left' width='35' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Segoe UI' SIZE=3><b>SAC</b> </font></td>");
                    sWrite.WriteLine("<td align='left'  width='35' height='14'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Unit</b></font></td>");
                    sWrite.WriteLine("<td align='left'  width='90'height='14'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Cost</b></font></td>");
                    sWrite.WriteLine("<td align='left'  width='70'height='14'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3><b>Tax</b></font></td>");
                    sWrite.WriteLine("<td align='left'  width='80'height='14'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3><b>Discount</b></font></td>");
                    sWrite.WriteLine("<td align='right'  width='100'height='14'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3><b>Total</b></font></td>");
                    sWrite.WriteLine("</tr>");
                    int i = 1;
                    for (int k = 0; k < DGV_Invoice.Rows.Count; k++)
                    {
                        string sac = this.cntrl.get_sac(DGV_Invoice.Rows[k].Cells["service_id"].Value.ToString());

                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + DGV_Invoice.Rows[k].Cells["service"].Value.ToString() + " </font></td>");
                        if(sac !="")
                            sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + sac + " </font></td>");
                        else
                            sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> </font></td>");
                        sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + DGV_Invoice.Rows[k].Cells[3].Value.ToString() + " </font></td>");
                        sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + DGV_Invoice.Rows[k].Cells["cost"].Value.ToString() + " </font></td>");
                        sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + DGV_Invoice.Rows[k].Cells["tax"].Value.ToString() + " </font></td>");
                        sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + DGV_Invoice.Rows[k].Cells["discount"].Value.ToString() + " </font></td>");
                        sWrite.WriteLine("<td align='right'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + DGV_Invoice.Rows[k].Cells["total"].Value.ToString() + " </font></td>");
                        i = i + 1;
                        sWrite.WriteLine("</tr>");
                    }
                    sWrite.WriteLine("<tr><td align='left' colspan=7><hr/></td></tr>");
                    sWrite.WriteLine("<tr >");
                    sWrite.WriteLine("<td align='Right' colspan=6  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + "Total Cost :" + " </font></td>");
                    sWrite.WriteLine("<td align='Right' colspan=7  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(txtTotalCost.Text.ToString())) + " </font></td>");
                    sWrite.WriteLine("</tr>");

                    sWrite.WriteLine("<tr >");
                    sWrite.WriteLine("<td align='Right' colspan=6  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + "Total Discount :" + " </font></td>");
                    sWrite.WriteLine("<td align='Right' colspan=7  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(txtTotalDisc.Text.ToString())) + " </font></td>");
                    sWrite.WriteLine("</tr>");

                    sWrite.WriteLine("<tr >");
                    sWrite.WriteLine("<td align='Right' colspan=6  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + "Total Tax :" + " </font></td>");
                    sWrite.WriteLine("<td align='Right' colspan=7  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(txtTotaltax.Text.ToString())) + " </font></td>");
                    sWrite.WriteLine("</tr>");

                    sWrite.WriteLine("<tr >");
                    sWrite.WriteLine("<td align='Right' colspan=6  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + "Grand Total:" + " </font></td>");
                    sWrite.WriteLine("<td align='Right' colspan=7  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(txt_grand_total.Text.ToString())) + " </font></td>");
                    sWrite.WriteLine("</tr>");

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
                System.Diagnostics.Process.Start(Apppath + "\\fasttrack.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Printer not ready...." + ex.Message, "Printer error.. ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      
        public void Allclear()
        {
            //patient
            pat_name.Visible = false;
            label13.Visible = false;
            //rjcmb_clinic_date.Visible = false;
            label29.Visible = false;
            label7.Visible = false;
            lb_gender.Visible = false;
            label14.Visible = false;
            lb_mobile.Visible = false;
            label15.Visible = false;
            pat_name.Visible = false;
            txt_search.Visible = true;
            dateTimePicker2.Value = DateTime.Now.Date;
            //doctor
            userControl_txt_consultation.Text = "";
            userControl_txt_fee.Text = "";
            userControl_txt_last_visited_date.Text = "";
            userControl_label10.Visible = false;
            lb_followup.Visible = false;
            chk_followup.Visible = false;
            dateTime_review.Value = DateTime.Now.Date;
            //vitals
            lb_vital_date.Visible = false;
            cmb_vital_dates.Visible = false;
            txt_vital_pulse.Text = "( Heart Beats Per Minute ) ";
            txt_temp.Text = "(C)";
            txt_blood1.Text = "(mmHg)";
            txt_blood2.Text = "(mmHg)";
            txt_weight.Text = "(Kg)";
            txt_resp.Text = "(Breaths/min)";
            txt_height.Text = "(Cm)";
            txt_spo.Text = "(%)";
            txt_bmi.Text = "";
            cmb_blood.SelectedIndex = 0;
            cmb_temp.SelectedIndex = 0;
            //clinical
            userControl_text_notes.Text = "";
            userControl_text_Diagnosis.Text = "";
            userControl_text_complaints.Text = "";
            userControl_textbox_compla.Text = "";
            userControl_textbox_diagn.Text = "";
            userControl_textbox_notes.Text = "";
            rjcmb_clinic_date.Visible = false;
            label13.Visible = false;
            userControl_textbox_compla.Visible = false;
            userControl_textbox_diagn.Visible = false;
            userControl_textbox_notes.Visible = false;
            userControl_label_complaints.Visible = false;
            userControl_label_diag.Visible = false;
            userControl_label_notes.Visible = false;

            //prescription
            //label38.Visible = false;
            rjCmb_prev_pres_date.Items.Clear();
            dgv_prev_prescription.Rows.Clear();
            dgv_prescrptn.Rows.Clear();
            cmb_blood.SelectedIndex = 0; 
            cmb_temp.SelectedIndex = 0;
            //invoice
            DGV_Invoice.Rows.Clear();
            txtTotalCost.Text = "0.00";
            txtTotaltax.Text = "0.00";
            txtTotalDisc.Text = "0.00";
            txt_grand_total.Text = "0.00";
        }
        public void prescription_check(MySqlConnection con, MySqlTransaction trans)
        {
            try
            {
                if (dgv_prescrptn.Rows.Count > 0)
                {
                    int count = dgv_prescrptn.Rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        DataTable dt4 = this.cntrl.get_inventoryid(dgv_prescrptn[10, i].Value.ToString(),con,trans);
                        if (dt4.Rows.Count > 0)
                        {
                            Prescription_bill_status = "Yes";
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void userControl_txt_complaints_search_Click_1(object sender, EventArgs e)
        {
            if (userControl_txt_complaints_search.Text == "Complaints Search")
            {
                userControl_txt_complaints_search.Text = "";
                userControl_txt_complaints_search.ForeColor = Color.DarkBlue;
            }
        }

        private void userControl_txt_complaints_search_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (userControl_txt_complaints_search.Text != "")
            {
                userControl_txt_complaints_search.ForeColor = Color.DarkBlue;
                DataTable dt = this.cntrl.complaint_cell_search(userControl_txt_complaints_search.Text);
                if (dt.Rows.Count > 0)
                {
                    lst_cheif.Visible = true;
                    lst_cheif.DataSource = dt;
                    lst_cheif.DisplayMember = "name";
                    lst_cheif.ValueMember = "id";
                }
                else
                {
                    lst_cheif.Visible = false;
                }
            }
            else
            {
                lst_cheif.Visible = false;
            }
        }

        private void lst_cheif_Click(object sender, EventArgs e)
        {

        }

        private void lst_cheif_MouseClick_1(object sender, MouseEventArgs e)
        {
            string pt_id = lst_cheif.Text.ToString();
            lst_cheif.Visible = false;
            if (userControl_text_complaints.Text == "")
                userControl_text_complaints.Text = pt_id;
            else
                userControl_text_complaints.Text += "," + pt_id;

            userControl_txt_complaints_search.Text = "Complaints Search";
            userControl_txt_complaints_search.ForeColor = Color.FromArgb(194, 194, 163);
        }

        private void txt_height_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txt_disc_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void uc_txt_duration_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void uc_txt_strengthno_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txt_spo_Load(object sender, EventArgs e)
        {

        }

        private void txt_spo_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txt_resp_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txt_tab_clinical_Click(object sender, EventArgs e)
        {
            btn_clinical_findings.BackColor = Color.White;
            btn_clinical_findings.ForeColor = Color.DarkBlue;

            btn_prescription.BackColor = Color.FromArgb(32, 43, 61);
            btn_prescription.ForeColor = Color.White;
            btn_previous_prescrption.BackColor = Color.FromArgb(32, 43, 61);
            btn_previous_prescrption.ForeColor = Color.White;
            pnal_previous_prescription.Visible = false;
            pnal_prescription.Visible = false;
            pnal_clinicalfinding.Visible = true;
            pnal_clinicalfinding.Size = new Size(1008, 275);
            pnal_clinicalfinding.Location = new Point(0, 30);
        }

        private void txt_tab_prescription_Click(object sender, EventArgs e)
        {
            btn_prescription.BackColor = Color.White;
            pnal_previous_prescription.Visible = false;
            pnal_clinicalfinding.Visible = false;
            pnal_prescription.Visible = true;
            pnal_prescription.Size = new Size(1008, 275);
            pnal_prescription.Location = new Point(0, 30);
            btn_prescription.BackColor = Color.White;
            btn_prescription.ForeColor = Color.DarkBlue;
            btn_clinical_findings.BackColor = Color.FromArgb(32, 43, 61);
            btn_clinical_findings.ForeColor = Color.White;
            btn_previous_prescrption.BackColor = Color.FromArgb(32, 43, 61);
            btn_previous_prescrption.ForeColor = Color.White;
            if (rjCombo_strength.Items.Count == 0)
            {
                DataTable dt = cmodel.Fill_unit_combo();
                if (dt.Rows.Count > 0)
                {
                    string[] items = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        items[i] = dt.Rows[i][1].ToString();
                    }
                    rjCombo_strength.DataSource = items;
                    rjCombo_strength.SelectedIndex = 0;
                }
            }

            if (dataGridView2.Rows.Count == 0)
            {
                DataTable dt2 = this.cntrl.get_tmplates();
                dataGridView2.DataSource = dt2;
                dataGridView2.Columns[0].Visible = false;
            }

        }

        private void txt_tab_prev_prescription_Click(object sender, EventArgs e)
        {
            pres_load_flag = true;
            btn_previous_prescrption.BackColor = Color.FromArgb(32, 43, 61);
            btn_previous_prescrption.ForeColor = Color.White;
            pnal_prescription.Visible = false;
            pnal_clinicalfinding.Visible = false;
            pnal_previous_prescription.Visible = true;
            pnal_previous_prescription.Size = new Size(1008, 275);
            pnal_previous_prescription.Location = new Point(0, 30);
            btn_previous_prescrption.BackColor = Color.White;
            btn_previous_prescrption.ForeColor = Color.DarkBlue;
            btn_clinical_findings.BackColor = Color.FromArgb(32, 43, 61);
            btn_clinical_findings.ForeColor = Color.White;
            btn_prescription.BackColor = Color.FromArgb(32, 43, 61);
            btn_prescription.ForeColor = Color.White;
            if (patient_id != "")
            {
                DataTable dt = this.cntrl.previous_pres_date(patient_id);
                if (dt.Rows.Count > 0)
                {
                    string[] items = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        items[i] = Convert.ToDateTime(dt.Rows[i][0].ToString()).ToString("MM-dd-yyyy");
                    }
                    rjCmb_prev_pres_date.DataSource = items;
                }
            }
            pres_load_flag = false;
        }

        private void txt_tab_clinical_Load(object sender, EventArgs e)
        {

        }

        private void btn_vital_previous_Click(object sender, EventArgs e)
        {
            lb_vital_date.Visible = true;
            cmb_vital_dates.Visible = true;
            vitals_clear();
            if (patient_id != "")
            {
                DataTable dtb_dates = this.cntrl.previous_vital_date(patient_id);
                if (dtb_dates.Rows.Count > 0)
                {
                    string[] items = new string[dtb_dates.Rows.Count];
                    for (int i = 0; i < dtb_dates.Rows.Count; i++)
                    {
                        items[i] = Convert.ToDateTime(dtb_dates.Rows[i][0].ToString()).ToString("dd-MM-yyyy");
                    }
                    cmb_vital_dates.DataSource = items;
                }
            }
        }
        string idtemp = "";
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = e.RowIndex;
                idtemp = dataGridView2.Rows[r].Cells[0].Value.ToString();
                DataTable dt = this.pmodel.get_template(idtemp);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        dgv_prescrptn.Rows.Add(dt.Rows[i]["drug_name"].ToString(), dt.Rows[i]["strength"].ToString(), dt.Rows[i]["strength_gr"].ToString(), dt.Rows[i]["duration"].ToString(), dt.Rows[i]["duration_period"].ToString(), dt.Rows[i]["morning"].ToString(), dt.Rows[i]["noon"].ToString(), dt.Rows[i]["night"].ToString(), dt.Rows[i]["food"].ToString(), dt.Rows[i]["add_instruction"].ToString(), dt.Rows[i]["drug_id"].ToString(), dt.Rows[i]["drug_type"].ToString());
                        dgv_prescrptn.Rows[dgv_prescrptn.Rows.Count - 1].Cells[12].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                        dgv_prescrptn.Rows[dgv_prescrptn.Rows.Count - 1].Height = 30;
                        img.ImageLayout = DataGridViewImageCellLayout.Normal;
                        dgv_prescrptn.Rows[dgv_prescrptn.Rows.Count - 1].Cells[13].Value = dt.Rows[i]["status"].ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_weight_KeyUp(object sender, KeyEventArgs e)
        {
            if(txt_weight.Text!="")
            {
                BMI_Calculate(lb_gender.Text);
            }
        }//form.Show();
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
        private void rjButtons2_Click(object sender, EventArgs e)
        {
            //this.Close();
            if(strApp_id!="")
            {
                this.Close(); 
            }
            else
            {
                if (PappyjoeMVC.Model.Connection.MyGlobals.loginType.TrimEnd() == "ADMIN")
                {
                    var form2 = new PappyjoeMVC.View.Dashboard();
                    form2.doctor_id = doctor_id;
                    openform(form2);
                }
                else if (PappyjoeMVC.Model.Connection.MyGlobals.loginType.TrimEnd() == "DOCTOR")
                {
                    var form2 = new PappyjoeMVC.View.Dashboard_Doctor();
                    form2.doctor_id = doctor_id;
                    openform(form2);
                }
            }
        }

        private void rjCmb_doctor_Click(object sender, EventArgs e)
        {
            DataTable dt = this.cntrl.Load_doctor();
            if (dt.Rows.Count > 0)
            {
                string[] items = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    items[i] = dt.Rows[i]["doctor_name"].ToString();
                }
                rjCmb_doctor.DataSource = items;

            }
        }

        private void DGV_Invoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (DGV_Invoice.CurrentCell.OwningColumn.Name == "Tonurse")
                    {
                        DGV_Invoice.CurrentRow.Cells["Tonurse"].Value = "Yes";
                    }
                    if (DGV_Invoice.CurrentCell.OwningColumn.Name == "img")
                    {
                        DialogResult res = MessageBox.Show("Are you sure you want to delete..?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (res == DialogResult.Yes)
                        {
                            DGV_Invoice.Rows.RemoveAt(this.DGV_Invoice.SelectedRows[0].Index);
                            if (DGV_Invoice.Rows.Count == 0)
                            {
                                txtTotalCost.Text = "Total Cost";
                                txtTotalDisc.Text = "Total Discount";
                                txtTotaltax.Text = "Total Tax";
                                txt_grand_total.Text = "Grant Total";
                            }
                            if (cmb_discount.SelectedIndex == 0)
                            {
                                 delete_gridrow_calculation();
                            }
                            else if (cmb_discount.SelectedIndex == 1)
                            {
                                delete_gridrow_calculation();
                            }
                            cmb_discount.Hide();
                            cmb_tax.Hide();
                        }
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        public void delete_gridrow_calculation()
        {
            decimal totcost = 0, total2 = 0;
            float discount1, dicount2 = 0, tax1 = 0;
            for (int i = 0; i < DGV_Invoice.Rows.Count; i++)
            {
                decimal unitcost = decimal.Parse(DGV_Invoice.Rows[i].Cells["cost"].Value.ToString());
                decimal quantity = decimal.Parse(DGV_Invoice.Rows[i].Cells[3].Value.ToString());
                decimal totalcost = unitcost * quantity;
                totcost = totcost + totalcost;
                txtTotalCost.Text = totcost.ToString("0.00");
                //
                decimal total1 = Convert.ToDecimal(DGV_Invoice.Rows[i].Cells[8].Value.ToString());
                total2 = total2 + total1;
                txt_grand_total.Text = total2.ToString("0.00");
                float dicount = float.Parse(DGV_Invoice.Rows[i].Cells[9].Value.ToString());
                discount1 = float.Parse(totalcost.ToString()) * (dicount / 100);
                dicount2 = dicount2 + dicount;
                txtTotalDisc.Text = dicount2.ToString("0.00");
                float tax = float.Parse(DGV_Invoice.Rows[i].Cells[6].Value.ToString());
                tax1 = tax1 + tax;
                txtTotaltax.Text = tax1.ToString("0.00");
            }
        }

        private void listpatientsearch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lb_add_tax_Load(object sender, EventArgs e)
        {

        }
        
        private void btn_invoice_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(txt_unit.Text) || String.IsNullOrWhiteSpace(txt_procedure.Text) )
                {
                    MessageBox.Show("Please enter the Quantity and Cost...", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (btn_invoice_add.Text != "Update")
                    {
                       
                        DGV_Invoice.Rows.Add(lbl_procedure_id.Text, txt_procedure.Text, txtCost.Text, txt_unit.Text, txt_disc.Text, cmb_discount.SelectedItem.ToString(), taxrstotal, cmb_tax.SelectedItem.ToString(), txt_total.Text, discounttotal,  doctor_id, rjCmb_doctor.SelectedItem.ToString(),"", "", id, "1", dateTimePicker2.Value.ToString("MM/dd/yyyy"), lab_tonurse.Text, "", lab_service);
                                DGV_Invoice.Rows[DGV_Invoice.Rows.Count - 1].Cells[18].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                                img.ImageLayout = DataGridViewImageCellLayout.Normal;
                        decimal totcost = 0;
                        decimal discount1 = 0, tax1 = 0,grand=0;
                        for (int i = 0; i < DGV_Invoice.Rows.Count; i++)
                                {
                                    totcost = totcost + (decimal.Parse(DGV_Invoice.Rows[i].Cells[2].Value.ToString()) * decimal.Parse(DGV_Invoice.Rows[i].Cells[3].Value.ToString()));
                            discount1 = discount1 +  decimal.Parse(DGV_Invoice.Rows[i].Cells[9].Value.ToString());//discounttotal
                            tax1 = tax1 + decimal.Parse(DGV_Invoice.Rows[i].Cells[6].Value.ToString());//taxrstotal
                            grand= grand+ decimal.Parse(DGV_Invoice.Rows[i].Cells[8].Value.ToString());
                        }
                        txtTotalCost.Text = totcost.ToString("F");
                        txtTotalDisc.Text = discount1.ToString("F");
                        txtTotaltax.Text = tax1.ToString("F");
                        decimal d1 = Convert.ToDecimal((totcost + tax1) - discount1);
                        txt_grand_total.Text = grand.ToString("F");
                        txt_procedure.Text = "";
                        txtCost.Text = "";
                        txt_unit.Text = ""; txt_total.Text = "";
                        txt_disc.Hide(); cmb_discount.Hide();
                        cmb_discount.SelectedIndex = 0;
                        cmb_tax.Hide();
                        txt_disc.Text = "0"; cmb_tax.Hide();
                        lab_tonurse.Text = "No";
                        lab_service = "No";
                        lb_add_disc.Visible = true;
                        lb_add_tax.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void BMI_Calculate(string gd)
        {
            try
            {
                if (txt_weight.Text != "" && txt_height.Text != "")
                {
                    gender = gd;
                    weight = Double.Parse(txt_weight.Text);
                    height = Double.Parse(txt_height.Text);
                    double BMI = Math.Round((weight / (height * height)) * 10000, 1);
                    if (BMI != null)
                    {
                        label10.Visible = true;
                        txt_bmi.Text = BMI.ToString();
                        if (BMI < 19 && gender == "Female")
                        {
                            label10.Text = "BMI is Low";
                            label10.ForeColor = Color.Red;
                        }
                        if (BMI >= 19 & BMI <= 24 & gender == "Female")
                        {
                            label10.Text = "Normal";
                            label10.ForeColor = Color.LightGreen;
                        }
                        if (BMI > 24 & gender == "Female")
                        {
                            label10.Text = "BMI is High";
                            label10.ForeColor = Color.Red;
                        }

                        if (BMI < 20 & gender == "Male")
                        {
                            label10.Text = "BMI is Low";
                            label10.ForeColor = Color.Red;
                        }
                        if (BMI >= 20 & BMI <= 25 & gender == "Male")
                        {
                            label10.Text = "Normal";
                            label10.ForeColor = Color.LightGreen;
                        }
                        if (BMI > 25 & gender == "Male")
                        {
                            label10.Text = "BMI is High";
                            label10.ForeColor = Color.Red;
                        }
                    }
                }
                else
                {
                    txt_bmi.Text = "";
                    label10.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public bool load_flag = false;
        private string id2;

        private void btn_previous_prescrption_Click(object sender, EventArgs e)
        {
            pres_load_flag = true;
            btn_previous_prescrption.BackColor = Color.FromArgb(32, 43, 61);
            btn_previous_prescrption.ForeColor = Color.White;
            pnal_prescription.Visible = false;
            pnal_clinicalfinding.Visible = false;
            pnal_previous_prescription.Visible = true;
            pnal_previous_prescription.Size = new Size(1008, 275);
            pnal_previous_prescription.Location = new Point(0, 30);

            btn_previous_prescrption.BackColor = Color.White;
            btn_previous_prescrption.ForeColor = Color.DarkBlue;
            btn_clinical_findings.BackColor = Color.FromArgb(32, 43, 61);
            btn_clinical_findings.ForeColor = Color.White;
            btn_prescription.BackColor = Color.FromArgb(32, 43, 61);
            btn_prescription.ForeColor = Color.White;
            if (patient_id!="")
            {
                DataTable dt = this.cntrl.previous_pres_date(patient_id);
                if(dt.Rows.Count>0)
                {
                    string[] items = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        items[i] = items[i] = Convert.ToDateTime(dt.Rows[i][0].ToString()).ToString("dd-MM-yyyy");// Convert.ToDateTime( dt.Rows[i][0].ToString()).ToString("MM-dd-yyyy");
                    }
                    rjCmb_prev_pres_date.DataSource = items;
                }
            }
            pres_load_flag = false;
        }

        private void rjCmb_doctor_onSelectedIndexChanged(object sender, EventArgs e)
        {
            if (load_flag == false)
            {
                if (rjCmb_doctor.SelectedIndex >= 0)
                {
                    DataTable dt_doctor_id = this.cntrl.get_doctorname(rjCmb_doctor.SelectedItem.ToString());
                    DataTable dt_patient_doctor = this.cntrl.get_patient_doctor(rjCmb_doctor.SelectedItem.ToString(), patient_id);
                    doctor_id = dt_doctor_id.Rows[0]["id"].ToString(); ;
                    if (newptid == "")
                    {
                        if (patient_id != "")
                        {
                            if (dt_patient_doctor.Rows.Count == 0)
                            {
                                if (userControl_txt_consultation.Text == "FOLLOWUP")
                                {
                                    DialogResult yesno = MessageBox.Show("He/She is not a patient of Dr" + rjCmb_doctor.SelectedItem + " , Do you want to take an appointment with Dr" + rjCmb_doctor.SelectedItem + " ?", "Change Consultation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                    if (yesno == DialogResult.No)
                                    {
                                        payment_status = "Followup";
                                    }
                                    else
                                    {
                                        userControl_txt_consultation.Text = "CONSULTATION";
                                        payment_status = "Consultation";
                                        userControl_txt_fee.Text = dt_doctor_id.Rows[0]["fee"].ToString();
                                        chk_followup.Visible = false; lb_followup.Visible = false;
                                        userControl_txt_last_visited_date.Visible = false; userControl_label9.Visible = false;
                                    }
                                }
                                else
                                {
                                    userControl_txt_consultation.Text = "CONSULTATION";
                                    userControl_txt_fee.Text = dt_doctor_id.Rows[0]["fee"].ToString(); payment_status = "Consultation"; chk_followup.Visible = false; lb_followup.Visible = false;
                                    userControl_txt_last_visited_date.Visible = false; 
                                    userControl_label9.Visible = false;
                                    userControl_label10.Visible = false;
                                }
                            }
                            else
                            {
                                if (userControl_txt_consultation.Text != "")
                                {
                                    if (userControl_txt_consultation.Text == "FOLLOWUP")
                                    {
                                        userControl_txt_consultation.Text = "FOLLOWUP";
                                        userControl_txt_fee.Text = dt_doctor_id.Rows[0]["followup_fee"].ToString();
                                        payment_status = "Followup"; chk_followup.Visible = true; lb_followup.Visible = true;
                                        userControl_txt_last_visited_date.Visible = true; userControl_label9.Visible = true;
                                    }
                                    else if (userControl_txt_consultation.Text == "CONSULTATION")
                                    {
                                        if (rjCmb_doctor.SelectedItem == dt_patient_doctor.Rows[0]["doctorname"].ToString())
                                        {
                                            userControl_txt_consultation.Text = "FOLLOWUP";
                                            userControl_txt_fee.Text = dt_doctor_id.Rows[0]["followup_fee"].ToString();
                                            payment_status = "Followup"; chk_followup.Visible = true; lb_followup.Visible = true;
                                            userControl_txt_last_visited_date.Visible = true; userControl_label9.Visible = true;
                                        }
                                        else
                                        {
                                            userControl_txt_consultation.Text = "CONSULTATION";
                                            userControl_txt_fee.Text = dt_doctor_id.Rows[0]["fee"].ToString();
                                            payment_status = "Consultation"; chk_followup.Visible = false; lb_followup.Visible = false;
                                            userControl_txt_last_visited_date.Visible = false; userControl_label9.Visible = false;
                                        }
                                    }
                                    else
                                    {
                                        userControl_txt_fee.Text = "0.00";
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        userControl_txt_fee.Text = dt_doctor_id.Rows[0]["fee"].ToString();
                    }
                }
            }
           
        }

      

        private void rjButtons1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ahaiiiiiii");
        }

        private void btn_clinical_findings_Click(object sender, EventArgs e)
        {
            btn_clinical_findings.BackColor = Color.White;
            btn_clinical_findings.ForeColor = Color.DarkBlue;
            btn_prescription.BackColor = Color.FromArgb(32, 43, 61);
            btn_prescription.ForeColor = Color.White;
            btn_previous_prescrption.BackColor = Color.FromArgb(32, 43, 61);
            btn_previous_prescrption.ForeColor = Color.White;
            pnal_previous_prescription.Visible = false;
            pnal_prescription.Visible = false;
            pnal_clinicalfinding.Visible = true;
            pnal_clinicalfinding.Size = new Size(1008, 275);
            pnal_clinicalfinding.Location = new Point(0, 30);
        }

        private void rjCombobox1_onSelectedIndexChanged(object sender, EventArgs e)
        {
        }  

       

      
   

     

      

    
    }
}
