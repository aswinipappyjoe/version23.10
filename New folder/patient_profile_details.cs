using PappyjoeMVC.Controller;
using PappyjoeMVC.Model;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
namespace PappyjoeMVC.View
{
    public partial class Patient_Profile_Details : Form
    {
        public string patient_id = "0";
        public string doctor_id = "0", admin_id = "0";
        Profile_Details_controller cntrl = new Profile_Details_controller();
        Show_Appointment_controller Apptmt_cntrl = new Show_Appointment_controller();
        Vital_Signs_controller vital_cntrl = new Vital_Signs_controller();
        Clinical_Findings_controller clinical_cntrl = new Clinical_Findings_controller();
        Treatment_controller treatmnt_cntrl = new Treatment_controller();
        Finished_Procedre_controller fnsdtrt_cntrl = new Finished_Procedre_controller();
        Prescription_Show_controller prescr_cntrl=new Prescription_Show_controller();
        LabWorks_controller lab_cntrl = new LabWorks_controller();
        Attachments_controller attach_cntrl = new Attachments_controller();
        Invoice_controller invo_cntrl = new Invoice_controller();
        Receipt_controller recpt_cntrl = new Receipt_controller();
        Nurses_Notes_controller nurse_cntrl = new Nurses_Notes_controller();

        Connection db = new Connection();
        public string ptid { get; set; }
        public Patient_Profile_Details()
        {
            InitializeComponent();

        }

        private void patient_profile_details_Load(object sender, EventArgs e)
        {
            DataTable dtb = this.cntrl.Get_Patient_details(patient_id);
            patientload(dtb);
            if (PappyjoeMVC.Model.Connection.MyGlobals.project_type.TrimEnd() == "Pharmacy")
            {
                lab_visa_msg.Visible = false;
                labelfinished.Visible = false;
                labelprescription.Visible = false;
                labl_Lab.Visible = false;
                labelattachment.Visible = false;
                labelinvoice.Visible = false;
                labelpayment.Visible = false;
                labelledger.Visible = false;

                labelappointment.Visible = false;
                label44.Visible = false;
                labelclinical.Visible = false;

                lb_appmnt_cnt.Visible = false;
                lb_Vitals_cnt.Visible = false;
                lb_Attchmnt_cnt.Visible = false;
                lb_clinical_cnt.Visible = false;

                lb_finisdtrt_cnt.Visible = false;
                lb_trtment_cnt.Visible = false;
                lb_Lac_cnt.Visible = false;
                lb_prescr_cnt.Visible = false;
                lb_Invoice_cnt.Visible = false;
                lb_Reciepts_cnt.Visible = false;

                pb_AppntmntAdd.Visible = false;
                pb_attchmntAdd.Visible = false;
                pb_ClinicalAdd.Visible = false;
                pb_fnsdtrtAdd.Visible = false;
                pb_invoiceAdd.Visible = false;
                pb_labAdd.Visible = false;
                pb_recieptsAdd.Visible = false;


                pb_recieptsAdd.Visible = false;
                pb_trtmntAdd.Visible = false;
                pb_vitalsAdd.Visible = false;
                label2.Visible = false;
                label3.Visible = false;

                panel7.Visible = false;
                panel10.Visible = false;
                lbl_NursesNotes.Visible = false;
                BtnCard.Visible = false;
                labeltreatment.Visible = false;
                pb_prescrptAdd.Visible = false;
                lab_passport_expi.Visible = false;
                txt_passport_expi.Visible = false;
                lab_visa_expiry_date.Visible = false;
                txt__visa_expiry_date.Visible = false;
            }
            else
            {
                DataTable dtadvance = this.cntrl.Get_Advance(patient_id);
                if (dtadvance.Rows.Count > 0)
                {
                    lblAdvance.Show();
                    lblAdvance.Text = "Available advance: " + string.Format("{0:C}", decimal.Parse(dtadvance.Rows[0][0].ToString()));
                }
                else
                {
                    lblAdvance.Text = "Available advance: " + string.Format("{0:C}", 0);
                }
               
                //visa expiry check
                lab_visa_msg.Visible = false;
                if(dtb.Rows.Count>0)
                {
                    if (dtb.Rows[0]["latest_visa_expiry_date"].ToString() != "")
                    {
                        int diff = 0;
                        DateTime expiry = Convert.ToDateTime(dtb.Rows[0]["latest_visa_expiry_date"].ToString());
                        DateTime to_date = DateTime.Now.Date;
                        if (expiry > to_date)
                        {
                            double Days = (expiry - to_date).TotalDays;
                            diff = Convert.ToInt32(Days);
                            if (diff > 12)
                            {
                                //lab_visa_msg.Visible = true;
                                //lab_visa_msg.Text = "Visa Expired";
                                //lab_visa_msg.ForeColor = Color.Red;
                            }
                            else
                            {
                                lab_visa_msg.Visible = true;
                                lab_visa_msg.Text = "Your visa validity will expire on date";
                                lab_visa_msg.ForeColor = Color.Red;
                            }

                        }
                        else
                        {
                            lab_visa_msg.Visible = true;
                            lab_visa_msg.Text = "Visa Expired";
                            lab_visa_msg.ForeColor = Color.Red;
                        }
                    }
                }
               
                
                appointment_count();
                nurse_count();
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

            //    if (PappyjoeMVC.Model.GlobalVariables.Subscription == "Silver")
            //{
            //    //this.Size = Screen.PrimaryScreen.WorkingArea.Size;//to set to the screen size
            //    labelfinished.Enabled = false;
            //    labelprescription.Enabled = false;
            //    labl_Lab.Enabled = false;
            //    labelattachment.Enabled = false;
            //    labelinvoice.Enabled = false;
            //    labelpayment.Enabled = false;
            //    labelledger.Enabled = false;
            //    //if (doctor_id != "1")
            //    //{
            //    //    string id;
            //    //    id = this.cntrl.get_dctr_privillage(doctor_id);
            //    //    if (int.Parse(id) > 0)
            //    //    {
            //    //        editpatient.Enabled = false;
            //    //    }
            //    //    else
            //    //    {
            //    //        editpatient.Enabled = true;
            //    //    }
            //    //}

            //    DataTable dtadvance = this.cntrl.Get_Advance(patient_id);
            //    if (dtadvance.Rows.Count > 0)
            //    {
            //        lblAdvance.Show();
            //        lblAdvance.Text = "Available advance: " + string.Format("{0:C}", decimal.Parse(dtadvance.Rows[0][0].ToString()));
            //    }
            //    else
            //    {
            //        lblAdvance.Text = "Available advance: " + string.Format("{0:C}", 0);
            //    }
            //    //BTNursenote.Hide();


            //    DataTable dtb = this.cntrl.Get_Patient_details(patient_id);
            //    patientload(dtb);
            //    appointment_count();
            //    vitals_count();
            //    clinical_count();
            //    treatment_count();
            //    nurse_count();
            //}
            //else
            //{
            //if (doctor_id != "1")
            //{
            string id;
                    //id = this.cntrl.get_dctr_privillage(doctor_id);
                    //if (int.Parse(id) > 0)
                    //{
                    //    editpatient.Enabled = false;
                    //}
                    //else
                    //{
                    //    editpatient.Enabled = true;
                    //}
                    //string EMRCFadd = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRCF' and Permission='A'");
                    //if (int.Parse(EMRCFadd) == 0)
                    //{
                    //    pb_ClinicalAdd.Enabled = false;
                    //}
                    //string EMRTPadd = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRTP' and Permission='A'");
                    //if (int.Parse(EMRTPadd) == 0)
                    //{
                    //    pb_trtmntAdd.Enabled = false;
                    //}
                    //string EMRPadd = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRP' and Permission='A'");
                    //if (int.Parse(EMRPadd) == 0)
                    //{
                    //    pb_prescrptAdd.Enabled = false;
                    //}
                    //string EMRFPadd = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRFP' and Permission='A'");
                    //if (int.Parse(EMRFPadd) == 0)
                    //{
                    //    pb_fnsdtrtAdd.Enabled = false;
                    //}
                    //string EMRFadd = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRF' and Permission='A'");
                    //if (int.Parse(EMRFadd) == 0)
                    //{
                    //    pb_attchmntAdd.Enabled = false;
                    //}
                    //string EMRIadd = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRI' and Permission='A'");
                    //if (int.Parse(EMRIadd) == 0)
                    //{
                    //    pb_invoiceAdd.Enabled = false;
                    //}
                    //string PATedit = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='PAT' and Permission='A'");
                    //if (int.Parse(PATedit) == 0)
                    //{
                    //    editpatient.Enabled = false;
                    //}
                    //string APTadd = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='APT' and Permission='A'");
                    //if (int.Parse(APTadd) == 0)
                    //{
                    //    pb_AppntmntAdd.Enabled = false;
                    //}
                //}

               
                //BTNursenote.Hide();
               
                ////
              
            //}
        }
        public void patientload(DataTable rs_patients)
        {
            if (rs_patients.Rows.Count > 0)
            {
                int YX1 = 141;
                if (rs_patients.Rows[0]["pt_name"].ToString() != "")
                {
                     lab_PatientName.Show();
                    linkLabel_Name.Text = rs_patients.Rows[0]["pt_name"].ToString();
                }
                else
                {
                    txtPatientName.Hide();
                     lab_PatientName.Hide();
                }
                if (rs_patients.Rows[0]["pt_id"].ToString() != "")
                {
                    labPatientId.Show();
                    labPatientId.Location = new Point(415, YX1);
                    labPatientId.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                    linkLabel_id.Text = rs_patients.Rows[0]["pt_id"].ToString();
                }
                //DateTime date_of_submission = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
                //DateTime date_of_submission = DateTime.Now;
                //DateTime _effective_date = Convert.ToDateTime(rs_patients.Rows[0]["Visited"].ToString());
                //TimeSpan difference = date_of_submission - _effective_date;
                try
                {
                    int YX = 20;
                    if (rs_patients.Rows[0]["pt_name"].ToString() != "")
                    {
                        txtPatientName.Text = rs_patients.Rows[0]["pt_name"].ToString();
                        txtPatientName.BackColor = Color.White;
                        lab_PatientName.Visible = true;  txtPatientName.Visible = true; 
                        linkLabel_Name.Text = rs_patients.Rows[0]["pt_name"].ToString();
                    }
                    else
                    {
                        txtPatientName.Visible = false;
                        lab_PatientName.Visible = false;
                    }
                    if (rs_patients.Rows[0]["pt_id"].ToString() != "")
                    {
                        txtPatientId.Text = rs_patients.Rows[0]["pt_id"].ToString();
                        txtPatientId.BackColor = Color.White;
                        YX = YX + 30;
                        txtPatientId.Location = new Point(303, YX);
                        txtPatientId.Visible = true;
                        labPatientId.Visible = true;
                        labPatientId.Location = new Point(201, YX);
                        linkLabel_id.Text = rs_patients.Rows[0]["pt_id"].ToString();
                    }
                    else
                    {
                        txtPatientId.Visible = false;
                        labPatientId.Visible = false;
                    }
                    if (rs_patients.Rows[0]["aadhar_id"].ToString() != "")
                    {
                        txtAdhaarId.Text = rs_patients.Rows[0]["aadhar_id"].ToString();
                        txtAdhaarId.BackColor = Color.White;
                        txtAdhaarId.Visible = true;
                        YX = YX + 30;
                        txtAdhaarId.Location = new Point(303, YX);
                        labAdhaarId.Visible = true;
                        labAdhaarId.Location = new Point(253, YX);
                    }
                    else
                    {
                        txtAdhaarId.Visible = false;
                        labAdhaarId.Visible = false;
                    }
                    //bh1
                    if (rs_patients.Rows[0]["Room_no"].ToString() != "")
                    {
                        txtrno.Text = rs_patients.Rows[0]["Room_no"].ToString();
                        txtrno.BackColor = Color.White;
                        txtrno.Show();
                        YX = YX + 30;
                        txtrno.Location = new Point(303, YX);
                        labrno.Show();
                        labrno.Location = new Point(201, YX);
                    }
                    else
                    {
                        txtrno.Hide();
                        labrno.Hide();
                    }
                    if (rs_patients.Rows[0]["gender"].ToString() != "")
                    {
                        txtGender.Text = rs_patients.Rows[0]["gender"].ToString();
                        YX = YX + 30;
                        txtGender.Location = new Point(303, YX);
                        txtGender.BackColor = Color.White;
                        txtGender.Show();
                        labGender.Show();
                        labGender.Location = new Point(217, YX);
                    }
                    else
                    {
                        txtGender.Hide();
                        labGender.Hide();
                    }
                    int a = 0;
                    if (rs_patients.Rows[0]["date_of_birth"].ToString() != "")
                    {
                        txtDob.Text = DateTime.Parse(rs_patients.Rows[0]["date_of_birth"].ToString()).ToString("dd/MM/yyyy");
                        txtDob.BackColor = Color.White;
                        YX = YX + 30;
                        txtDob.Location = new Point(303, YX);
                        txtDob.Show();
                        labDob.Show();
                        labDob.Location = new Point(182, YX);
                        a = 1;
                    }
                    else
                    {
                        txtDob.Hide();
                        labDob.Hide();
                    }
                    if (rs_patients.Rows[0]["refferedby"].ToString() != "")
                    {
                        txtRefferedBy.Text = rs_patients.Rows[0]["refferedby"].ToString();
                        txtRefferedBy.BackColor = Color.White;
                        YX = YX + 30;
                        txtRefferedBy.Location = new Point(303, YX);
                        txtRefferedBy.Show();
                        labRefferedBy.Show();
                        labRefferedBy.Location = new Point(188, YX);
                    }
                    else
                    {
                        txtRefferedBy.Hide();
                        labRefferedBy.Hide();
                    }
                    if (rs_patients.Rows[0]["blood_group"].ToString() != "")
                    {
                        txtBloodGroup.Text = rs_patients.Rows[0]["blood_group"].ToString();
                        txtBloodGroup.BackColor = Color.White;
                        YX = YX + 30;
                        txtBloodGroup.Location = new Point(303, YX);
                        txtBloodGroup.Show();
                        labBloodGroup.Show();
                        labBloodGroup.Location = new Point(180, YX);
                    }
                    else
                    {
                        txtBloodGroup.Hide();
                        labBloodGroup.Hide();
                    }
                    if (rs_patients.Rows[0]["family"].ToString() != "")
                    {
                        txtAccompainedBy.Text = rs_patients.Rows[0]["family"].ToString();
                        txtAccompainedBy.BackColor = Color.White;
                        YX = YX + 30;
                        txtAccompainedBy.Location = new Point(303, YX);
                        txtAccompainedBy.Show();
                        labAccompainedBy.Show();
                        labAccompainedBy.Location = new Point(153, YX);
                    }
                    else
                    {
                        txtAccompainedBy.Hide();
                        labAccompainedBy.Hide();
                    }
                    YX = YX + 30;
                    labhead.Location = new Point(180, YX);
                    labhead.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                    YX = YX + 10;
                    if (rs_patients.Rows[0]["primary_mobile_number"].ToString() != "")
                    {
                        txtPrimaryMobNo.Text = rs_patients.Rows[0]["primary_mobile_number"].ToString();
                        txtPrimaryMobNo.BackColor = Color.White;
                        YX = YX + 30;
                        txtPrimaryMobNo.Location = new Point(303, YX);
                        txtPrimaryMobNo.Show();
                        labPrimaryMobileNumber.Show();
                        labPrimaryMobileNumber.Location = new Point(100, YX);
                    }
                    else
                    {
                        txtPrimaryMobNo.Hide();
                        labPrimaryMobileNumber.Hide();
                    }
                    if (rs_patients.Rows[0]["secondary_mobile_number"].ToString() != "")
                    {
                        txtSecondaryMobNo.Text = rs_patients.Rows[0]["secondary_mobile_number"].ToString();
                        txtSecondaryMobNo.BackColor = Color.White;
                        YX = YX + 30;
                        txtSecondaryMobNo.Location = new Point(305, YX);
                        txtSecondaryMobNo.Show();
                        labSecondaryMobileNumbr.Show();
                        labSecondaryMobileNumbr.Location = new Point(120, YX);
                    }
                    else
                    {
                        txtSecondaryMobNo.Hide();
                        labSecondaryMobileNumbr.Hide();
                    }
                    if (rs_patients.Rows[0]["landline_number"].ToString() != "")
                    {
                        txtLandLineNo.Text = rs_patients.Rows[0]["landline_number"].ToString();
                        txtLandLineNo.BackColor = Color.White;
                        YX = YX + 30;
                        txtLandLineNo.Location = new Point(305, YX);
                        txtLandLineNo.Show();
                        labLandLineNo.Show();
                        labLandLineNo.Location = new Point(178, YX);
                    }
                    else
                    {
                        txtLandLineNo.Hide();
                        labLandLineNo.Hide();
                    }
                    if (rs_patients.Rows[0]["email_address"].ToString() != "")
                    {
                        txtEmailAddress.Text = rs_patients.Rows[0]["email_address"].ToString();
                        txtEmailAddress.BackColor = Color.White;
                        YX = YX + 30;
                        txtEmailAddress.Location = new Point(305, YX);
                        txtEmailAddress.Show();
                        labEmailAddress.Show();
                        labEmailAddress.Location = new Point(172, YX);
                    }
                    else
                    {
                        txtEmailAddress.Hide();
                        labEmailAddress.Hide();
                    }
                    if (rs_patients.Rows[0]["street_address"].ToString() != "")
                    {
                        txtStreetAddress.Text = rs_patients.Rows[0]["street_address"].ToString();
                        txtStreetAddress.BackColor = Color.White;
                        YX = YX + 30;
                        txtStreetAddress.Location = new Point(305, YX);
                        txtStreetAddress.Show();
                        labStreetAddress.Show();
                        labStreetAddress.Location = new Point(170, YX);
                    }
                    else
                    {
                        txtStreetAddress.Hide();
                        labStreetAddress.Hide();
                    }
                    if (rs_patients.Rows[0]["locality"].ToString() != "")
                    {
                        txtLocality.Text = rs_patients.Rows[0]["locality"].ToString();
                        txtLocality.BackColor = Color.White;
                        YX = YX + 30;
                        txtLocality.Location = new Point(305, YX);
                        txtLocality.Show();
                        labLocality.Show();
                        labLocality.Location = new Point(217, YX);
                    }
                    else
                    {
                        txtLocality.Hide();
                        labLocality.Hide();
                    }
                    if (rs_patients.Rows[0]["city"].ToString() != "")
                    {
                        txtCity.Text = rs_patients.Rows[0]["city"].ToString();
                        txtCity.BackColor = Color.White;
                        YX = YX + 30;
                        txtCity.Location = new Point(305, YX);
                        txtCity.Show();
                        labCity.Show();
                        labCity.Location = new Point(243, YX);
                    }
                    else
                    {
                        txtCity.Hide();
                        labCity.Hide();
                    }
                    if (rs_patients.Rows[0]["pincode"].ToString() != "")
                    {
                        txtPinCode.Text = rs_patients.Rows[0]["pincode"].ToString();
                        txtPinCode.BackColor = Color.White;
                        YX = YX + 30;
                        txtPinCode.Location = new Point(305, YX);
                        txtPinCode.Show();
                        labPinCode.Show();
                        labPinCode.Location = new Point(216, YX);
                    }
                    else
                    {
                        txtPinCode.Hide();
                        labPinCode.Hide();
                    }
                    if (rs_patients.Rows[0]["nationality"].ToString() != "")
                    {
                        txtnationality.Text = rs_patients.Rows[0]["nationality"].ToString();
                        txtnationality.BackColor = Color.White;
                        YX = YX + 30;
                        txtnationality.Location = new Point(305, YX);
                        txtnationality.Show();
                        labnationality.Show();
                        labnationality.Location = new Point(200, YX);
                    }
                    else
                    {
                        txtnationality.Hide();
                        labnationality.Hide();
                    }
                    if (rs_patients.Rows[0]["passport_no"].ToString() != "")
                    {
                        txtpassport.Text = rs_patients.Rows[0]["passport_no"].ToString();
                        txtpassport.BackColor = Color.White;
                        YX = YX + 30;
                        txtpassport.Location = new Point(305, YX);
                        txtpassport.Show();
                        labpassport.Show();
                        labpassport.Location = new Point(200, YX);
                    }
                    else
                    {
                        txtpassport.Hide();
                        labpassport.Hide();
                    }
                    YX =   50;
                    if (rs_patients.Rows[0]["Opticket"].ToString() != "")
                    {
                        txtopticket.Text = rs_patients.Rows[0]["Opticket"].ToString();
                        txtopticket.BackColor = Color.White;
                        txtopticket.Location = new Point(634, YX);
                        txtopticket.Show();
                        lblopti.Show();
                        txtopticket.Show();
                        lblopti.Location = new Point(553, YX);
                    }
                    else
                    {
                        txtopticket.Hide();
                        lblopti.Hide();
                    }
                    if (rs_patients.Rows[0]["care_of"].ToString() != "")
                    {
                        txt_c_o.Text = rs_patients.Rows[0]["care_of"].ToString();
                        txt_c_o.BackColor = Color.White;
                        YX = YX + 30;
                        txt_c_o.Location = new Point(634, YX);
                        txt_c_o.Show();
                        lb_C_o.Show();
                        txt_c_o.Show();
                        lb_C_o.Location = new Point(575, YX);
                    }
                    else
                    {
                        txt_c_o.Hide();
                        lb_C_o.Hide();
                    }
                    if (a == 1)
                    {

                        if (rs_patients.Rows[0]["age"].ToString() != "")
                        {
                            if (rs_patients.Rows[0]["age2"].ToString() != "")
                            {
                                if (Convert.ToDecimal(rs_patients.Rows[0]["age2"].ToString()) != 0)
                                {
                                    txtAge.Text = rs_patients.Rows[0]["age"].ToString() + " " + rs_patients.Rows[0]["days"].ToString()+ " "+rs_patients.Rows[0]["age2"].ToString() + "Months";
                                    //txtAge.Text = rs_patients.Rows[0]["age"].ToString() + " " + rs_patients.Rows[0]["days"].ToString();
                                    txtAge.Show();
                                    txtAge.BackColor = Color.White;
                                    YX = YX + 30;
                                    txtAge.Location = new Point(634, YX);
                                    txtAge.Show();
                                    labAge.Show();
                                    labAge.Location = new Point(579, YX);
                                }
                            }
                            else
                            {
                                if (Convert.ToDecimal(rs_patients.Rows[0]["age"].ToString()) != 0)
                                {
                                    txtAge.Text = rs_patients.Rows[0]["age"].ToString() + " " + rs_patients.Rows[0]["days"].ToString();
                                    //txtAge.Text = rs_patients.Rows[0]["age"].ToString() + " " + rs_patients.Rows[0]["age2"].ToString()+"" + rs_patients.Rows[0]["days"].ToString();
                                    txtAge.Show();
                                    txtAge.BackColor = Color.White;
                                    YX = YX + 30;
                                    txtAge.Location = new Point(634, YX);
                                    txtAge.Show();
                                    labAge.Show();
                                    labAge.Location = new Point(579, YX);
                                }

                            }
                        }
                        else
                        {
                           txtAge.Hide();
                           labAge.Hide();
                        }
                        //if (rs_patients.Rows[0]["age2"].ToString() != "")
                        //{

                        //    if (Convert.ToDecimal(rs_patients.Rows[0]["age2"].ToString()) != 0)
                        //    {
                        //        txt_age2.Text = rs_patients.Rows[0]["age2"].ToString() + " Months";
                        //        txt_age2.Show();
                        //        txt_age2.BackColor = Color.White;
                        //        YX = YX + 30;
                        //        txt_age2.Location = new Point(634, YX);
                        //        txt_age2.Show();
                        //        labAge.Show();
                        //        labAge.Location = new Point(579, YX);
                        //    }
                        //}
                        //else
                        //{
                        //    //txt_age2.Hide();
                        //    //labAge.Hide();
                        //}


                    }
                    else
                    {
                        if (rs_patients.Rows[0]["age"].ToString() != "")
                        {
                            if (rs_patients.Rows[0]["age2"].ToString() != "")
                            {
                                if (Convert.ToDecimal(rs_patients.Rows[0]["age2"].ToString()) != 0)
                                {
                                    txtAge.Text = rs_patients.Rows[0]["age"].ToString() + " " + rs_patients.Rows[0]["days"].ToString() + " " + rs_patients.Rows[0]["age2"].ToString() + "Months";
                                   // txtAge.Text = rs_patients.Rows[0]["age"].ToString() + " " + rs_patients.Rows[0]["age2"].ToString() + "" + rs_patients.Rows[0]["days"].ToString();
                                    //txtAge.Text = rs_patients.Rows[0]["age"].ToString() + " " + rs_patients.Rows[0]["days"].ToString();
                                    txtAge.Show();
                                    txtAge.BackColor = Color.White;
                                    YX = YX + 30;
                                    txtAge.Location = new Point(634, YX);
                                    txtAge.Show();
                                    labAge.Show();
                                    labAge.Location = new Point(579, YX);
                                }
                            }
                            else
                            {
                                if (Convert.ToDecimal(rs_patients.Rows[0]["age"].ToString()) != 0)
                                {
                                    txtAge.Text = rs_patients.Rows[0]["age"].ToString() + " " + rs_patients.Rows[0]["days"].ToString();
                                    //txtAge.Text = rs_patients.Rows[0]["age"].ToString() + " " + rs_patients.Rows[0]["age2"].ToString()+"" + rs_patients.Rows[0]["days"].ToString();
                                    txtAge.Show();
                                    txtAge.BackColor = Color.White;
                                    YX = YX + 30;
                                    txtAge.Location = new Point(634, YX);
                                    txtAge.Show();
                                    labAge.Show();
                                    labAge.Location = new Point(579, YX);
                                }

                            }
                        }
                        else
                        {
                            txtAge.Hide();
                            labAge.Hide();
                        }
                        //if (rs_patients.Rows[0]["age"].ToString() != "")
                        //{
                        //    if (Convert.ToDecimal(rs_patients.Rows[0]["age"].ToString()) != 0)
                        //    {
                        //        txtAge.Text = rs_patients.Rows[0]["age"].ToString() + " " + rs_patients.Rows[0]["days"].ToString();
                        //        txtAge.Show();
                        //        txtAge.BackColor = Color.White;
                        //        YX = YX + 30;
                        //        txtAge.Location = new Point(634, YX);
                        //        txtAge.Show();
                        //        labAge.Show();
                        //        labAge.Location = new Point(579, YX);
                        //    }
                        //}
                        //else
                        //{
                        //    txtAge.Hide();
                        //    labAge.Hide();
                        //}
                        //    if (rs_patients.Rows[0]["age2"].ToString() != "")
                        //{

                        //    if (Convert.ToDecimal(rs_patients.Rows[0]["age2"].ToString()) != 0)
                        //    {
                        //        txt_age2.Text = rs_patients.Rows[0]["age2"].ToString() + " Months";
                        //        txt_age2.Show();
                        //        txt_age2.BackColor = Color.White;
                        //        YX = YX + 30;
                        //        txt_age2.Location = new Point(634, YX);
                        //        txt_age2.Show();
                        //        labAge.Show();
                        //        labAge.Location = new Point(579, YX);
                        //    }
                        //}
                        //else
                        //{
                        //    //txt_age2.Hide();
                        //    //labAge.Hide();
                        //}

                    }
                    if (txtAge.Text == "")
                    {
                        if (rs_patients.Rows[0]["age2"].ToString() != "")
                        {
                            txt_age2.Text = rs_patients.Rows[0]["age2"].ToString() + " " + rs_patients.Rows[0]["days"].ToString();
                            txt_age2.Show();
                            txt_age2.BackColor = Color.White;
                            txt_age2.Location = new Point(634, YX);
                            txt_age2.Show();
                            labAge.Show();
                            labAge.Location = new Point(579, YX);
                        }
                        else
                        {
                            //labAge.Hide();
                            //txt_age2.Hide();
                        }
                    }


                    int ba = 0;
                    if (rs_patients.Rows[0]["Visited"].ToString() != "")
                    {
                        if (DateTime.Parse(rs_patients.Rows[0]["Visited"].ToString()).ToString("dd/MM/yyyy") != "")
                        {
                            txtvisiteddate.Text = DateTime.Parse(rs_patients.Rows[0]["Visited"].ToString()).ToString("dd/MM/yyyy");
                            txtvisiteddate.BackColor = Color.White;
                            YX = YX + 30;
                            txtvisiteddate.Location = new Point(634, YX);
                            txtvisiteddate.Show();
                            LabDateOfAdm.Show();
                            LabDateOfAdm.Location = new Point(510, YX);
                            ba = 1;
                        }
                    }
                    else
                    {
                        txtvisiteddate.Hide();
                        LabDateOfAdm.Hide();
                    }
                    if (rs_patients.Rows[0]["doctorname"].ToString() != "")
                    {
                        YX = YX + 30;
                        txtDoctor.Text = rs_patients.Rows[0]["doctorname"].ToString();
                        txtDoctor.BackColor = Color.White;
                        txtDoctor.Location = new Point(634, YX);
                        txtDoctor.Show();
                        LabDoctorName.Show();
                        LabDoctorName.Location = new Point(512, YX);
                    }
                    else
                    {
                        txtDoctor.Hide();
                        LabDoctorName.Hide();
                    }
                    if (rs_patients.Rows[0]["Occupation"].ToString() != "")
                    {
                        YX = YX + 30;
                        txtOccupation.Text = rs_patients.Rows[0]["Occupation"].ToString();
                        txtOccupation.BackColor = Color.White;
                        txtOccupation.Location = new Point(634, YX);
                        txtOccupation.Show();
                        LabOccupation.Show();
                        LabOccupation.Location = new Point(525, YX);
                    }
                    else
                    {
                        txtOccupation.Hide();
                        LabOccupation.Hide();
                    }
                    if (rs_patients.Rows[0]["passport_expiry_date"].ToString() != "")
                    {
                        YX = YX + 70;
                        txt_passport_expi.Text = rs_patients.Rows[0]["passport_expiry_date"].ToString();
                        txt_passport_expi.BackColor = Color.White;
                        txt_passport_expi.Location = new Point(685, YX);
                        txt_passport_expi.Show();
                        lab_passport_expi.Show();
                        lab_passport_expi.Location = new Point(525, YX);
                    }
                    else
                    {
                        txt_passport_expi.Hide();
                        lab_passport_expi.Hide();
                    }
                    if (rs_patients.Rows[0]["country_issuing_passport"].ToString() != "")
                    {
                        YX = YX + 30;
                        txt__country_issu_pass.Text = rs_patients.Rows[0]["country_issuing_passport"].ToString();
                        txt__country_issu_pass.BackColor = Color.White;
                        txt__country_issu_pass.Location = new Point(685, YX);
                        txt__country_issu_pass.Show();
                        lab_country_issu_pass.Show();
                        lab_country_issu_pass.Location = new Point(490, YX);
                    }
                    else
                    {
                        txt__country_issu_pass.Hide();
                        lab_country_issu_pass.Hide();
                    }
                    if (rs_patients.Rows[0]["latest_visa_no"].ToString() != "")
                    {
                        YX = YX + 30;
                        _latest_visa_no.Text = rs_patients.Rows[0]["latest_visa_no"].ToString();
                        _latest_visa_no.BackColor = Color.White;
                        _latest_visa_no.Location = new Point(685, YX);
                        _latest_visa_no.Show();
                        lab_latest_visa_no.Show();
                        lab_latest_visa_no.Location = new Point(530, YX);
                    }
                    else
                    {
                        _latest_visa_no.Hide();
                        lab_latest_visa_no.Hide();
                    }
                    if (rs_patients.Rows[0]["visa_issuing_country"].ToString() != "")
                    {
                        YX = YX + 30;
                        txt__visa_issu_country.Text = rs_patients.Rows[0]["visa_issuing_country"].ToString();
                        txt__visa_issu_country.BackColor = Color.White;
                        txt__visa_issu_country.Location = new Point(685, YX);
                        txt__visa_issu_country.Show();
                        lab_visa_issu_country.Show();
                        lab_visa_issu_country.Location = new Point(524, YX);
                    }
                    else
                    {
                        txt__visa_issu_country.Hide();
                        lab_visa_issu_country.Hide();
                    }
                    if (rs_patients.Rows[0]["latest_visa_expiry_date"].ToString() != "")
                    {
                        YX = YX + 30;
                        txt__visa_expiry_date.Text = rs_patients.Rows[0]["latest_visa_expiry_date"].ToString();
                        txt__visa_expiry_date.BackColor = Color.White;
                        txt__visa_expiry_date.Location = new Point(685, YX);
                        txt__visa_expiry_date.Show();
                        lab_visa_expiry_date.Show();
                        lab_visa_expiry_date.Location = new Point(505, YX);
                    }
                    else
                    {
                        txt__visa_expiry_date.Hide();
                        lab_visa_expiry_date.Hide();
                    }
                    if(lab_visa_msg.Visible==true)
                    {
                        YX = YX + 30;
                        lab_visa_msg.Location = new Point(544, YX);
                    }
                    try
                    {
                        string curFile = this.cntrl.getserver() + "\\Pappyjoe_utilities\\patient_image\\" + patient_id;
                        if (System.IO.File.Exists(curFile))
                        {
                            pictureBox_PatientPhoto.Image = Image.FromFile(curFile);
                        }
                        else
                        {
                            pictureBox_PatientPhoto.Image = PappyjoeMVC.Properties.Resources.nophoto;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    DataTable dt8 = this.cntrl.pt_medical(patient_id);
                    grmedical.Visible = true;
                    grmedical.RowCount = 0;
                    grmedical.ColumnCount = 1;
                    grmedical.ColumnHeadersHeight = 10;
                    grmedical.RowHeadersVisible = false;
                    grmedical.ColumnHeadersVisible = false;
                    grmedical.Columns[0].Name = "preid";
                    grmedical.Columns[0].Width = 200;
                    for (int j = 0; j < dt8.Rows.Count; j++)
                    {
                        grmedical.Rows.Add(dt8.Rows[j][0].ToString());
                    }
                    DataTable dt9 = this.cntrl.patrint_goup(patient_id);
                    gridgroups.Visible = true;
                    gridgroups.RowCount = 0;
                    gridgroups.ColumnCount = 1;
                    gridgroups.ColumnHeadersHeight = 10;
                    gridgroups.RowHeadersVisible = false;
                    gridgroups.ColumnHeadersVisible = false;
                    gridgroups.Columns[0].Name = "preid";
                    gridgroups.Columns[0].Width = 200;
                    for (int j = 0; j < dt9.Rows.Count; j++)
                    {
                        gridgroups.Rows.Add(dt9.Rows[j][0].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        public void Setcontroller(Profile_Details_controller controller)
        {
            cntrl = controller;
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
                this.Dispose();
                myForm.Show();
            }
        }

        private void medicalcertificate_Click(object sender, EventArgs e)
        {
            var dlg = new Medical_Certificate();
            dlg.patient_id = patient_id;
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void BtnConsent_Click(object sender, EventArgs e)
        {
            var dlg = new Consent();
            dlg.patient_id = patient_id;
            dlg.doctor_id = doctor_id;
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void BtnCaseSheet_Click(object sender, EventArgs e)
        {
            DataTable dt_ip = this.cntrl.get_opid(patient_id);
            if (dt_ip.Rows[0]["op_id"].ToString() == "" || dt_ip.Rows[0]["op_id"].ToString() == null)
            {
                var dlg = new CaseSheet();
                dlg.patient_id = patient_id;
                dlg.doctor_id = doctor_id;
                dlg.ShowDialog();
                dlg.Dispose();
            }
            else
            {
                var dlg = new CaseSheet_Of_IP_Patients();
                dlg.patient_id = patient_id;
                dlg.doctor_id = doctor_id;
                dlg.ShowDialog();
                dlg.Dispose();
                DataTable dt = this.cntrl.get_opid(patient_id);
                if (dt.Rows.Count > 0)
                {
                    txtPatientId.Text = dt.Rows[0]["pt_id"].ToString();
                }

                //dlg.Visible = false;
                //patient_profile_details_Load(null, null);
            }
            //var dlg = new CaseSheet();
            //dlg.patient_id = patient_id;
            //dlg.doctor_id = doctor_id;
            //dlg.ShowDialog();
            //dlg.Dispose();
        }

        private void editpatient_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string PATedit = privi_mdl.edit_patients(doctor_id);// db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='PAT' and Permission='E'");
                if (int.Parse(PATedit) > 0)
                {
                    var form2 = new Patient_Profile_Edit();
                    form2.doctor_id = doctor_id;
                    form2.patient_id = patient_id;
                    openform(form2);
                }
                else
                {
                    MessageBox.Show("There is No Privilege to edit Patient", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                var form2 = new Patient_Profile_Edit();
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
        private void labelprofile_Click(object sender, EventArgs e)
        {
            Patient_Profile_Details form = new Patient_Profile_Details();
            form.doctor_id = doctor_id;
            form.patient_id = patient_id;
            openform(form);
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

        private void labelallpatient_Click(object sender, EventArgs e)
        {
            var form2 = new Patients();
            form2.doctor_id = doctor_id;
            openform(form2);
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

        private void BtnCard_Click(object sender, EventArgs e)
        {
            try
            {
                //string barCode = txtPatientId.Text;
                //Bitmap bitMap = new Bitmap(barCode.Length * 40, 20);
                //using (Graphics graphics = Graphics.FromImage(bitMap))
                //{
                //    Font oFont = new Font("IDAutomationHC39M", 16);
                //    PointF point = new PointF(2f, 2f);
                //    SolidBrush blackBrush = new SolidBrush(Color.Black);
                //    SolidBrush whiteBrush = new SolidBrush(Color.White);
                //    graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                //    graphics.DrawString(barCode, oFont, blackBrush, point);
                //}
                //using (MemoryStream ms = new MemoryStream())
                //{
                //    bitMap.Save(ms, ImageFormat.Png);
                //    pictureBox_barcode.Image = bitMap;
                //    pictureBox_barcode.Height = bitMap.Height;
                //    pictureBox_barcode.Width = bitMap.Width;
                //}
                Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                pictureBox_barcode.Image = barcode.Draw(txtPatientId.Text, 20);

                PrintDocument printdocument = new PrintDocument();
                printdocument.PrintPage += print_Patient_Card_PrintPage;
                printdocument.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please check your printer...!!", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void print_Patient_Card_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int yy = 2;
            string sexage = ""; string age = ""; string days = "";
            int Dexist = 0;
            System.Data.DataTable dt1 = this.cntrl.Get_Patient_details(patient_id);
            if (dt1.Rows.Count > 0)
            {
                e.Graphics.DrawImage(pictureBox_Logo.Image, 10, yy);
                yy = yy + 160;
                e.Graphics.DrawImage(pictureBox_barcode.Image, 80, yy);
                using (System.Drawing.Font printFont = new System.Drawing.Font("Segoe UI", 15.0f))
                {
                    yy = yy + 30;
                    e.Graphics.DrawString(dt1.Rows[0]["pt_name"].ToString(), printFont, Brushes.Black, 10, yy);
                }
                using (System.Drawing.Font printFont2 = new System.Drawing.Font("Segoe UI", 15.0f))
                {
                    yy = yy + 20;
                    e.Graphics.DrawString("PID: " + dt1.Rows[0]["pt_Id"].ToString(), printFont2, Brushes.Black, 10, yy);
                }
                using (System.Drawing.Font printFont1 = new System.Drawing.Font("Segoe UI", 10.0f, FontStyle.Bold))
                {
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
                            age =   dt1.Rows[0]["age2"].ToString() + "Months";
                        }

                    }
                   
                    yy = yy + 40;
                    e.Graphics.DrawString("Date :" + DateTime.Today.ToString("dd-MM-yyyy")+ "        " +"Age:"+age, printFont1, Brushes.Black, 10, yy);

                    if (dt1.Rows[0]["Street_address"].ToString() != "")
                    {
                        yy = yy + 20;
                        e.Graphics.DrawString(dt1.Rows[0]["Street_address"].ToString(), printFont1, Brushes.Black, 10, yy);
                    }
                    else if (dt1.Rows[0]["locality"].ToString() != "")
                    {
                        yy = yy + 20;
                        e.Graphics.DrawString(dt1.Rows[0]["locality"].ToString(), printFont1, Brushes.Black, 10, yy);
                    }
                    else if (dt1.Rows[0]["primary_mobile_number"].ToString() != "")
                    {
                        yy = yy + 20;
                        e.Graphics.DrawString("Ph :" + dt1.Rows[0]["primary_mobile_number"].ToString(), printFont1, Brushes.Black, 10, yy);
                    }
                    else if (dt1.Rows[0]["nationality"].ToString() != "")
                    {
                        yy = yy + 20;
                        e.Graphics.DrawString("Nationality :" + dt1.Rows[0]["nationality"].ToString(), printFont1, Brushes.Black, 10, yy);
                    }
                    else if (dt1.Rows[0]["passport_no"].ToString() != "")
                    {
                        yy = yy + 20;
                        e.Graphics.DrawString("Passport No :" + dt1.Rows[0]["passport_no"].ToString(), printFont1, Brushes.Black, 10, yy);
                    }
                }

            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            PrintDocument printdocument = new PrintDocument();
            printdocument.PrintPage += printDocument1_PrintPage;
            printdocument.Print();
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.Show();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Font printFont = new Font("Segoe UI", 10);
            string tabDataText = this.cntrl.Load_CompanyName();
            var tabDataForeColor = Color.Blue;
            var txtDataWidth = e.Graphics.MeasureString(tabDataText, printFont).Width;
            using (var sf = new StringFormat())
            {
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                e.Graphics.DrawString(tabDataText, new Font(this.Font.Name, 18),
                     new SolidBrush(tabDataForeColor),
                     e.MarginBounds.Left + (e.MarginBounds.Width / 2),
                     e.MarginBounds.Top - 55,
                     sf);
            }
            e.HasMorePages = false;
            int iLeftMargin = e.MarginBounds.Left;
            string date = System.DateTime.Now.ToShortDateString();
            e.Graphics.DrawString("Patient Registration Form", new Font("Segoe UI", 16, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 200, 75);
            string underLine = "--------------------------------------------";
            e.Graphics.DrawString(underLine, new Font("Segoe UI", 14, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 200, 95);
            e.Graphics.DrawString("Printed By:", new Font("Segoe UI", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 1, 130);
            e.Graphics.DrawString("Admin", new Font("Segoe UI", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 75, 130);
            e.Graphics.DrawString("Date:", new Font("Segoe UI", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 150, 130);
            e.Graphics.DrawString(date, new Font("Segoe UI", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 195, 130);
            e.Graphics.DrawString("No: " + patient_id, new Font("Segoe UI", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 370, 130);
            e.Graphics.DrawString("Personal Details", new Font("Segoe UI", 12, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 10, 160);

            int xyy = 190;
            if (txtPatientName.Visible == true)
            {
                e.Graphics.DrawString(this.lab_PatientName.Text, new Font("Segoe UI", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 25, xyy);
                e.Graphics.DrawString(this.txtPatientName.Text, new Font("Segoe UI", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 230, xyy);
                xyy = xyy + 30;
            }
            if (txtPatientId.Visible == true)
            {
                e.Graphics.DrawString(this.labPatientId.Text, new Font("Segoe UI", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 25, xyy);
                e.Graphics.DrawString(this.txtPatientId.Text, new Font("Segoe UI", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 230, xyy);
                xyy = xyy + 30;
            }
            if (txtAdhaarId.Visible == true)
            {
                e.Graphics.DrawString(this.labAdhaarId.Text, new Font("Segoe UI", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 25, xyy);
                e.Graphics.DrawString(this.txtAdhaarId.Text, new Font("Segoe UI", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 230, xyy);
                xyy = xyy + 30;
            }
            if (txtGender.Visible == true)
            {
                e.Graphics.DrawString(this.labGender.Text, new Font("Segoe UI", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 25, xyy);
                e.Graphics.DrawString(this.txtGender.Text, new Font("Segoe UI", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 230, xyy);
                xyy = xyy + 30;
            }
            if (txtDob.Visible == true)
            {
                e.Graphics.DrawString(this.labDob.Text, new Font("Segoe UI", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 25, xyy);
                e.Graphics.DrawString(this.txtDob.Text, new Font("Segoe UI", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 230, xyy);
                xyy = xyy + 30;
            }
            if (txtRefferedBy.Visible == true)
            {
                e.Graphics.DrawString(this.labRefferedBy.Text, new Font("Segoe UI", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 25, xyy);
                e.Graphics.DrawString(this.txtRefferedBy.Text, new Font("Segoe UI", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 230, xyy);
                xyy = xyy + 30;
            }
            if (txtBloodGroup.Visible == true)
            {
                e.Graphics.DrawString(this.labAdhaarId.Text, new Font("Segoe UI", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 25, xyy);
                e.Graphics.DrawString(this.txtAdhaarId.Text, new Font("Segoe UI", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 230, xyy);
                txtBloodGroup.Hide();
                labBloodGroup.Hide();
                xyy = xyy + 30;
            }
            if (txtAccompainedBy.Visible == true)
            {
                e.Graphics.DrawString(this.labAccompainedBy.Text, new Font("Segoe UI", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 25, xyy);
                e.Graphics.DrawString(this.txtAccompainedBy.Text, new Font("Segoe UI", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 230, xyy);
                xyy = xyy + 30;
            }
            if (txtPrimaryMobNo.Visible == true)
            {
                e.Graphics.DrawString(this.labPrimaryMobileNumber.Text, new Font("Segoe UI", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 25, xyy);
                e.Graphics.DrawString(this.txtPrimaryMobNo.Text, new Font("Segoe UI", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 230, xyy);

                xyy = xyy + 30;
            }
            if (txtSecondaryMobNo.Visible == true)
            {
                e.Graphics.DrawString(this.labSecondaryMobileNumbr.Text, new Font("Segoe UI", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 25, xyy);
                e.Graphics.DrawString(this.txtSecondaryMobNo.Text, new Font("Segoe UI", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 230, xyy);

                xyy = xyy + 30;
            }
            if (txtLandLineNo.Visible == true)
            {
                e.Graphics.DrawString(this.labLandLineNo.Text, new Font("Segoe UI", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 25, xyy);
                e.Graphics.DrawString(this.txtLandLineNo.Text, new Font("Segoe UI", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 230, xyy);

                xyy = xyy + 30;
            }
            if (txtEmailAddress.Visible == true)
            {
                e.Graphics.DrawString(this.labEmailAddress.Text, new Font("Segoe UI", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 25, xyy);
                e.Graphics.DrawString(this.txtEmailAddress.Text, new Font("Segoe UI", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 230, xyy);
                xyy = xyy + 30;
            }
            if (txtStreetAddress.Visible == true)
            {
                e.Graphics.DrawString(this.labStreetAddress.Text, new Font("Arial", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 25, xyy);
                e.Graphics.DrawString(this.txtStreetAddress.Text, new Font("Arial", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 230, xyy);

                xyy = xyy + 30;
            }
            if (txtLocality.Visible == true)
            {
                e.Graphics.DrawString(this.labLocality.Text, new Font("Segoe UI", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 25, xyy);
                e.Graphics.DrawString(this.txtLocality.Text, new Font("Segoe UI", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 230, xyy);
                xyy = xyy + 30;
            }
            if (txtCity.Visible == true)
            {
                e.Graphics.DrawString(this.labCity.Text, new Font("Segoe UI", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 25, xyy);
                e.Graphics.DrawString(this.txtCity.Text, new Font("Segoe UI", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 230, xyy);
                xyy = xyy + 30;
            }
            if (txtPinCode.Visible == true)
            {
                e.Graphics.DrawString(this.labPinCode.Text, new Font("Segoe UI", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 25, xyy);
                e.Graphics.DrawString(this.txtPinCode.Text, new Font("Segoe UI", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 230, xyy);
                xyy = xyy + 30;
            }
            if (txtnationality.Visible == true)
            {
                e.Graphics.DrawString(this.labPinCode.Text, new Font("Segoe UI", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 25, xyy);
                e.Graphics.DrawString(this.txtPinCode.Text, new Font("Segoe UI", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 230, xyy);
                xyy = xyy + 30;
            }
            if (txtpassport.Visible == true)
            {
                e.Graphics.DrawString(this.labPinCode.Text, new Font("Segoe UI", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 25, xyy);
                e.Graphics.DrawString(this.txtPinCode.Text, new Font("Segoe UI", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 230, xyy);
                xyy = xyy + 30;
            }

            if (txtopticket.Visible == true)
            {
                e.Graphics.DrawString(this.lblopti.Text, new Font("Segoe UI", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 25, xyy);
                e.Graphics.DrawString(this.txtopticket.Text, new Font("Segoe UI", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 230, xyy);
                xyy = xyy + 30;
            }
            if (txtAge.Visible == true)
            {
                e.Graphics.DrawString(this.labAge.Text, new Font("Segoe UI", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 25, xyy);
                e.Graphics.DrawString(this.txtAge.Text+ txt_age2.Text, new Font("Segoe UI", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 230, xyy);
                xyy = xyy + 30;
            }

            if (txtvisiteddate.Visible == true)
            {
                e.Graphics.DrawString(this.LabDateOfAdm.Text, new Font("Segoe UI", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 25, xyy);
                e.Graphics.DrawString(this.txtvisiteddate.Text, new Font("Segoe UI", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 230, xyy);
                xyy = xyy + 30;
            }
            if (txtDoctor.Visible == true)
            {
                e.Graphics.DrawString(this.LabDoctorName.Text, new Font("Segoe UI", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 25, xyy);
                e.Graphics.DrawString(this.txtDoctor.Text, new Font("Segoe UI", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 230, xyy);
                xyy = xyy + 30;
            }
            if (txtOccupation.Visible == true)
            {
                e.Graphics.DrawString(this.LabOccupation.Text, new Font("Segoe UI", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 25, xyy);
                e.Graphics.DrawString(this.txtOccupation.Text, new Font("Segoe UI", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 230, xyy);
                xyy = xyy + 30;
            }

            if (grmedical.Rows.Count > 0)
            {
                xyy = xyy + 60;
                e.Graphics.DrawString("Medical History", new Font("Segoe UI", 12, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point), Brushes.Black, 10, xyy);
                xyy = xyy + 30;
                for (int j = 0; j < grmedical.Rows.Count; j++)
                {
                    e.Graphics.DrawString(grmedical.Rows[j].Cells[0].Value.ToString(), new Font("Segoe UI", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), Brushes.Black, 25, xyy);
                    xyy = xyy + 20;
                }
            }

        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            var form2 = new Consultation();
            form2.doctor_id = doctor_id;
            form2.patient_id = patient_id;
            form2.ShowDialog();
            form2.Dispose();
        }

        private void btn_convertip_Click(object sender, EventArgs e)
        {
            panl_IP_Patient.Visible = true;
            panl_IP_Patient.Location = new Point(316, 17);
            DataTable dtb = this.cntrl.IP_patentid();
            if (dtb.Rows.Count > 0)
            {
                if (dtb.Rows[0]["IP_automation"].ToString() == "Yes")
                {
                    txt_ipId.Text = dtb.Rows[0]["IP_prefix"].ToString() + dtb.Rows[0]["IP_number"].ToString();
                    txt_ipId.ReadOnly = true;
                    txt_IPname.Text = txtPatientName.Text;

                }
                else
                {
                    txt_IPname.Text = txtPatientName.Text;
                    txt_ipId.ReadOnly = false;
                }
                //txt_ipId.Text = dtb.Rows[0]["IP_number"].ToString();
                //txt_IPname.Text = txtPatientName.Text;// linkLabel_Name.Text;
            }

            //DataTable auto = this.cntrl.data_from_automaticid();   IP_number,IP_prefix,IP_automation
            //if (auto.Rows.Count > 0)
            //{
            //    if (auto.Rows[0]["patient_automation"].ToString() == "Yes")
            //    {
            //        txtPatientId.Text = auto.Rows[0]["patient_prefix"].ToString() + auto.Rows[0]["patient_number"].ToString();
            //        txtPatientId.ReadOnly = true;
            //    }
            //}
        }

        private void button_IPsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (button_IPsave.Text == "Save")
                {
                    //DataTable dt_pat_id=
                    if (txt_ipId.Text != "")
                    {
                        DataTable dtb = this.cntrl.get_opid(patient_id);
                        if (dtb.Rows[0]["op_id"].ToString() != "")
                        {
                            MessageBox.Show("IP ID already existed", "Exist", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            this.cntrl.save_ippatient(patient_id, txtPatientId.Text, txt_ipId.Text);
                            int j = this.cntrl.update_ipPatient(txtPatientId.Text, txt_ipId.Text, patient_id, txt_room.Text);
                            if (j > 0)
                            {
                                DataTable cmd = this.cntrl.automaticIPid();
                                if (cmd.Rows.Count > 0)
                                {
                                    int n = 0;
                                    n = int.Parse(cmd.Rows[0]["IP_number"].ToString()) + 1;
                                    if (n != 0)
                                    {
                                        this.cntrl.update_autogenerateIPid(n);
                                    }
                                }

                                txt_ipId.Text = "";
                                txt_IPname.Text = "";
                                txt_room.Text = "";
                                MessageBox.Show("IP ID saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                panl_IP_Patient.Visible = false;
                                patient_profile_details_Load(null, null);
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Patient IP ID is missing", "Data not found ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            catch (Exception ex)
            {
                
            }

        }
        private void BtnCaseSheetIP_Click(object sender, EventArgs e)
        {
            var dlg = new CaseSheet();
            dlg.patient_id = patient_id;
            dlg.doctor_id = doctor_id;
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void BTnClose_Click(object sender, EventArgs e)
        {
            panl_IP_Patient.Visible = false;
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
        public void nurse_count()
        {
            System.Data.DataTable dt_cf_main = this.nurse_cntrl.dt_cf_main(patient_id);
            if (dt_cf_main.Rows.Count > 0)
            {
                lb_Nurses_Notes.Text = dt_cf_main.Rows.Count.ToString();
            }
        }

        public void appointment_count()
        {
            DataTable dt = this.Apptmt_cntrl.show(patient_id);
            if (dt.Rows.Count>0)
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
        //Connection db = new Connection();
        private void pb_trtmntAdd_Click(object sender, EventArgs e)
        {
            //string type = PappyjoeMVC.Model.Connection.MyGlobals.project_type;
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

        private void linklbl_patientHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form2 = new Patient_Previous_History();
            form2.doctor_id = doctor_id;
            form2.patient_id = patient_id;
            form2.ShowDialog();
            form2.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new Clinical_Porforma();
            frm.doctor_id = doctor_id;
            frm.patient_id = patient_id;
            frm.ShowDialog();
            frm.Dispose();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var dlg = new Maternity_leave_certificate ();
            dlg.patient_id = patient_id;
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dlg = new Sick_leave_new();//  MedicalLeave();
            dlg.patient_id = patient_id;
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void lbl_NursesNotes_Click(object sender, EventArgs e)
        {
            var dlg = new certificate_found();
            //dlg.doctor_id = doctor_id;
            dlg.patient_id = patient_id;
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void BTNursenote_Click(object sender, EventArgs e)
        {
            var dlg = new Nurses_Notes();
            dlg.doctor_id = doctor_id;
            dlg.patient_id = patient_id;
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void labAdhaarId_Click(object sender, EventArgs e)
        {


        }

        private void button3_Click(object sender, EventArgs e)
        {
            var dlg = new Attendant_Certificate();
            //dlg.doctor_id = doctor_id;
            dlg.patient_id = patient_id;
            dlg.ShowDialog();
            dlg.Dispose();
           
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
         {

        }

        public void reciept_count()
        {
            DataTable Payments = db.table("select receipt_no,amount_paid,invoice_no,procedure_name,mode_of_payment from tbl_payment where pt_id='" + patient_id + "'");
            if (Payments.Rows.Count > 0)
            {
                lb_Reciepts_cnt.Text = Payments.Rows.Count.ToString();
            }
        }
    }
}
