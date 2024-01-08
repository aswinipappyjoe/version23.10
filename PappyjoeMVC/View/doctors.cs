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
using Microsoft.Win32;
using System.IO;
using PappyjoeMVC.Model;

namespace PappyjoeMVC.View
{
    public partial class doctors : Form
    {


        Doctor_Controller cntrl = new Doctor_Controller();
        public string doctor_id = "";
        string calendrcolor = "0";
        public string status = "No";
        public string doc = "0";
        public string type, strlogin_type = "";
        string tempId = "0";
        String gender = "";

        Connection db = new Connection();
        //private string type;

        public doctors()
        {
            InitializeComponent();
        }

        public doctors(string type)
        {
            InitializeComponent();
           
        }

        private void r_male_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void text_reg_no_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txt_fee_TextChanged(object sender, EventArgs e)
        {

        }

        private void radio_login_yes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radio_login_no_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void r_yes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void text_phone_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void button_add_service_Click(object sender, EventArgs e)
        {
            Doctors_Practice_Details frm = new Doctors_Practice_Details();
            frm.frameid = "1";
            frm.ShowDialog();
            frm.Dispose();
        }

        private void button_save_service_Click(object sender, EventArgs e)
        {
            try 
            {

                int flagService = 0;
            if (dataGridView_services.Rows.Count != 0)
            {
                for (int i = 0; i < dataGridView_services.Rows.Count; i++)
                {
                        if (dataGridView_services.Rows[i].Cells["Col_service"].Value.ToString().Trim() == combo_service.Text.Trim())
                        {
                            flagService = 1;
                        }
                    }
            }
            if (flagService == 0)
            {
                if (combo_service.Text != "")
                {
                    string c = this.cntrl.get_servicecount(doctor_id);
                    string ser_id = this.cntrl.getserviceid(combo_service.Text);
                    this.cntrl.save_drservice(doctor_id, ser_id);

                }
                DataTable service1 = this.cntrl.load_servicegrid(doctor_id);
                dataGridView_services.DataSource = service1;
            }
            else
            {
                MessageBox.Show("Service with same name already exists..", "Duplication encountered", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
         catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

        private void cmbDrProfileSelectedIndexChanged(object sender, EventArgs e)
        {
              if (cmbDrProfile.Text == "SERVICES")
            {
                panel_DoctorProfile_Add.Visible = true;
                panel_service.Location = new Point(20, 7);
                panel_service.Visible = true;
                panel_specialization.Visible = false;
                panel_education.Visible = false;
                panel_experience.Visible = false;
                panel_recognition.Visible = false;
                panel_membership.Visible = false;
                panel_register.Visible = false;
            }
            else if (cmbDrProfile.Text == "SPECIALIZATION")
            {
                panel_DoctorProfile_Add.Visible = true;
                
                panel_specialization.Visible = true;
                panel_specialization.Location = new Point(20, 7);
               
                panel_service.Visible = false;
                panel_education.Visible = false;
                panel_experience.Visible = false;
                panel_recognition.Visible = false;
                panel_membership.Visible = false;
                panel_register.Visible = false;
            }
            else if (cmbDrProfile.Text == "EDUCATION")
            {
                panel_DoctorProfile_Add.Visible = true;
                panel_education.Visible = true;
                panel_education.Location = new Point(20, 7);
                panel_service.Visible = false;
                panel_specialization.Visible = false;
                panel_experience.Visible = false;
                panel_recognition.Visible = false;
                panel_membership.Visible = false;
                panel_register.Visible = false;
            }
            else if (cmbDrProfile.Text == "AWARDS")
            {
                panel_DoctorProfile_Add.Visible = true;
                panel_recognition.Visible = true;
                panel_recognition.Location = new Point(20, 7);
                panel_service.Visible = false;
                panel_specialization.Visible = false;
                panel_education.Visible = false;
                panel_experience.Visible = false;
                panel_membership.Visible = false;
                panel_register.Visible = false;
            }
            else if (cmbDrProfile.Text == "MEMBERSHIPS")
            {
                panel_DoctorProfile_Add.Visible = true;
                panel_membership.Visible = true;
                panel_membership.Location = new Point(20, 7);
                panel_service.Visible = false;
                panel_specialization.Visible = false;
                panel_education.Visible = false;
                panel_experience.Visible = false;
                panel_recognition.Visible = false;
                panel_register.Visible = false;
            }
            else if (cmbDrProfile.Text == "REGISTRATION")
            {
                panel_DoctorProfile_Add.Visible = true;
                panel_register.Visible = true;
                panel_register.Location = new Point(20, 7);
                panel_service.Visible = false;
                panel_specialization.Visible = false;
                panel_education.Visible = false;
                panel_experience.Visible = false;
                panel_recognition.Visible = false;
                panel_membership.Visible = false;
            }
            else if (cmbDrProfile.Text == "EXPERIENCE")
            {
                panel_DoctorProfile_Add.Visible = true;
                panel_experience.Visible = true;
                panel_experience.Location = new Point(20, 7);
                panel_service.Visible = false;
                panel_specialization.Visible = false;
                panel_education.Visible = false;
                panel_recognition.Visible = false;
                panel_membership.Visible = false;
                panel_register.Visible = false;
                DataTable dt11 = this.cntrl.load_city();
                combo_experience_city.DisplayMember = "city";
                combo_experience_city.ValueMember = "id";
                combo_experience_city.DataSource = dt11;
            }
        }

        private void combo_service_Click(object sender, EventArgs e)
        {
            DataTable service = this.cntrl.load_serviceCombo();
            combo_service.DisplayMember = "service";
            combo_service.ValueMember = "id";
            combo_service.DataSource = service;
            combo_service.Text = "";

        }

        private void combo_special_Click(object sender, EventArgs e)
        {
            DataTable special = this.cntrl.load_cmbspecilization();
            combo_special.DisplayMember = "name";
            combo_special.ValueMember = "id";
            combo_special.DataSource = special;
            combo_special.Text = "";
        }

        private void button_add_special_Click(object sender, EventArgs e)
        {
            Doctors_Practice_Details frm = new Doctors_Practice_Details();
            frm.frameid = "6";
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void button_save_special_Click(object sender, EventArgs e)
        {
            try
            {
                int flagspecial = 0;
                if (dataGridView_specialization.Rows.Count != 0)
                {
                    for (int i = 0; i < dataGridView_specialization.Rows.Count; i++)
                    {
                        if (dataGridView_specialization.Rows[i].Cells["Col_Col_special"].Value.ToString().Trim() == combo_special.Text.Trim())
                        {
                            flagspecial = 1;
                        }
                    }
                }
                if (flagspecial == 0)
                {
                    if (combo_special.Text != "")
                    {
                        string ser_id = this.cntrl.get_specilizationid(combo_special.Text);
                        this.cntrl.dr_savespecilization(doctor_id, ser_id);
                    }
                    DataTable special = this.cntrl.load_dr_specilizaion(doctor_id);
                    dataGridView_specialization.DataSource = special;
                }
                else
                {
                    MessageBox.Show("Specialization with same name already exists..", "Duplication encountered", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void combo_education_degree_Click(object sender, EventArgs e)
        {
            DataTable degree = this.cntrl.load_degreecombo();
            combo_education_degree.DisplayMember = "degree";
            combo_education_degree.ValueMember = "id";
            combo_education_degree.DataSource = degree;
            combo_education_degree.Text = "";
        }

        private void combo_education_college_Click(object sender, EventArgs e)
        {
            DataTable college = this.cntrl.load_collegecombo();
            combo_education_college.DisplayMember = "college";
            combo_education_college.ValueMember = "id";
            combo_education_college.DataSource = college;
            combo_education_college.Text = "";
        }

        private void combo_education_year_Click(object sender, EventArgs e)
        {

        }
        public void fill_all_combo()
        {
            combo_reg_regcouncil_Click(null, null);
            combo_membership_Click(null, null);
            combo_service_Click(null, null);
            combo_special_Click(null, null);
            combo_education_college_Click(null, null);
            combo_education_degree_Click(null, null);
        }

        private void doctors_Load(object sender, EventArgs e)
        {
            for (int i = 1914; i <= DateTime.Now.Year; i++)
            {
                combo_education_year.Items.Add(i.ToString());
                combo_award_year.Items.Add(i.ToString());
                combo_reg_year.Items.Add(i.ToString());
                combo_eperience_from.Items.Add(i.ToString());
                combo_experience_to.Items.Add(i.ToString());
            }
            fill_all_combo();
            DataTable staff1 = this.cntrl.load_staffcombo();
            cmbStaffType.DisplayMember = "st_type";
            cmbStaffType.ValueMember = "id";
            cmbStaffType.DataSource = staff1;
            cmbStaffType.Text = "";
            cmbDrProfile.Text = "Select values";
            button1.Visible = true;

            if (PappyjoeMVC.Model.Connection.MyGlobals.loginType != "staff")
            {
                DataTable docnam = this.cntrl.Load_Logintype(doctor_id);
                if (docnam.Rows.Count > 0)
                {
                    strlogin_type = docnam.Rows[0]["login_type"].ToString();
                }
                if (doc != "0")
                {
                    this.FormBorderStyle = FormBorderStyle.None;
                    button2.Show();
                    tempId = doctor_id;
                    doctor_id = doc;
                }
                else
                {
                    tempId = doctor_id;
                }
                panel_education.Hide();
                panel_experience.Hide();
                panel_membership.Hide();
                panel_recognition.Hide();
                panel_register.Hide();
                panel_service.Hide();
                panel_specialization.Hide();
                panel_edit_dr.Show();

                DataTable name = this.cntrl.get_doctor_details(doctor_id);
                text_drname.Text = name.Rows[0]["doctor_name"].ToString();
                //string gen = name.Rows[0]["gender"].ToString();
                /*if (gen == "Male")
                 {
                     chk_male.Checked = true;
                 }
                else
                 {
                  chk_female.Checked = true;
                 }*/
                if (name.Rows[0]["gender"].ToString()=="male")
                 {
                    r_male.Checked = true;
                 }
                 else
                 {
                     r_female.Checked = true;
                 }

                if (name.Rows[0]["activate_login"].ToString() == "Yes")
                {
                    radio_login_yes.Checked = true;
                }
                else
                {
                    radio_login_no.Checked = true;
                }
                panel_color.Visible = false;
                combo_year.Text = name.Rows[0]["experience"].ToString();
                rich_about.Text = name.Rows[0]["about"].ToString();
                text_phone.Text = name.Rows[0]["mobile_number"].ToString();
                text_email.Text = name.Rows[0]["email_id"].ToString();
                textPassword.Text = name.Rows[0]["password"].ToString();
                text_reg_no.Text = name.Rows[0]["registration_number"].ToString();
                txt_fee.Text = name.Rows[0]["fee"].ToString();
                txt_f_fee.Text = name.Rows[0]["followup_fee"].ToString();
                txt_f_period.Text = name.Rows[0]["followup_period"].ToString();
                string colo = name.Rows[0]["calendar_color"].ToString();
                cmbStaffType.Text = name.Rows[0]["login_type"].ToString();

                if (colo == "0")
                {
                    button1.BackColor = Color.FromArgb(197, 214, 236);// Color.LightCoral;// Color.FromArgb(0, 192, 192, 255);
                }
                else if (colo == "1")
                {
                    button1.BackColor = Color.FromArgb(236, 141, 128);// Color.Orchid;//   Color.FromArgb(0, 255, 128, 128);
                }
                else if (colo == "2")
                {
                    button1.BackColor = Color.FromArgb(128, 149, 215);// Color.MediumSlateBlue;// Color.FromArgb(0, 128, 128, 255);
                }
                else if (colo == "3")
                {
                    button1.BackColor = Color.FromArgb(162, 211, 106);// Color.PaleGreen;// Color.FromArgb(0, 128, 255, 128);
                }
                else if (colo == "4")
                {
                    button1.BackColor = Color.FromArgb(218, 218, 203);// Color.Silver;// Color.FromArgb(0, 224, 224, 224);
                }
                else if (colo == "5")
                {
                    button1.BackColor = Color.FromArgb(238, 167, 120);// Color.Orange;// Color.FromArgb(0, 255, 128, 0);
                }
                else if (colo == "6")
                {
                    button1.BackColor = Color.FromArgb(134, 225, 232);// Color.Cyan;// Color.FromArgb(0, 0, 255, 255);
                }
                else if (colo == "7")
                {
                    button1.BackColor = Color.FromArgb(200, 193, 128);// Color.Goldenrod;// Color.FromArgb(0, 218, 165, 32);
                }
                else if (colo == "8")
                {
                    button1.BackColor = Color.FromArgb(190, 162, 232);// Color.PaleV

                }
                else if (colo == "9")
                {
                    button1.BackColor = Color.FromArgb(159, 195, 188);// Color.CadetBlue;// Color.FromArgb(0, 95, 158, 160);
                }
                else if (colo == "10")
                {
                    button1.BackColor = Color.FromArgb(238, 218, 120);// Color.Yellow;//Color.FromArgb(0, 255, 255, 128);
                }
                else
                {
                    button1.BackColor = Color.FromArgb(0, 0, 0);// Color.Black;//Color.FromArgb(0, 0, 0, 0);
                }



                //if ((name.Rows[0]["login_type"].ToString() != "admin") && strlogin_type == "admin")
                //{
                //    cmbStaffType.Visible = true;
                //    lblStaffType.Visible = true;
                //    if (name.Rows[0]["login_type"].ToString() == "doctor" || name.Rows[0]["login_type"].ToString() == "DOCTOR" || name.Rows[0]["login_type"].ToString() == "Doctor")
                //    {
                //        cmbStaffType.SelectedIndex = 0;

                //    }
                //    else
                //    { cmbStaffType.SelectedIndex = 1; }
                //}

               






            }
        }

        private void button_education_adddegree_Click(object sender, EventArgs e)
        {
            Doctors_Practice_Details frm = new Doctors_Practice_Details();
            frm.frameid = "2";
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void button_education_add_college_Click(object sender, EventArgs e)
        {
            Doctors_Practice_Details frm = new Doctors_Practice_Details();
            frm.frameid = "3";
            frm.ShowDialog();
            frm.Dispose();
        }

        private void button_save_education_Click(object sender, EventArgs e)
        {
            try
            {
                int flageducation = 0;
                if (dataGridView_education.Rows.Count != 0)
                {

                    for (int i = 0; i < dataGridView_education.Rows.Count; i++)
                    {
                        if (dataGridView_education.Rows[i].Cells["Col_degree"].Value.ToString().Trim() == combo_education_degree.Text.Trim() && dataGridView_education.Rows[i].Cells["Col_college"].Value.ToString().Trim() == combo_education_college.Text.Trim() && dataGridView_education.Rows[i].Cells["Col_year"].Value.ToString().Trim() == combo_education_year.Text.Trim())
                        {
                            flageducation = 1;
                        }
                    }
                }
                if (flageducation == 0)
                {
                    string degree = "", college = "", year = "", eDegree = "0", eCollege = "0", eYear = "0";
                    if (combo_education_degree.Text != "" && combo_education_degree.Visible == true)
                    {
                        errorProvider1.Dispose();
                        degree = combo_education_degree.Text;
                        eDegree = "0";
                        string serviceCheck = this.cntrl.check_degreeexists(combo_education_degree.SelectedValue.ToString(), doctor_id);
                        if (serviceCheck != "")
                        {
                            int eduid = Convert.ToInt32(serviceCheck);
                            if (eduid > 0)
                            {
                                MessageBox.Show("Degree with same name already exists..", "Duplication encountered", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                    else
                    {
                        errorProvider1.SetError(combo_education_degree, "can't be empty");
                        eDegree = "1";
                    }
                    if (combo_education_college.Text != "" && combo_education_college.Visible == true)
                    {
                        errorProvider2.Dispose();
                        college = combo_education_college.Text;
                        eCollege = "0";
                    }
                    else
                    {
                        errorProvider2.SetError(combo_education_college, "can't be empty");
                        eCollege = "1";
                    }
                    if (combo_education_year.Text != "")
                    {
                        errorProvider3.Dispose();
                        year = combo_education_year.Text;
                        eYear = "0";
                        string year_ = this.cntrl.check_yearexists(year, doctor_id);
                        if (year_ != "")
                        {
                            int checkyear = Convert.ToInt32(year_);
                            if (checkyear > 0)
                            {
                                MessageBox.Show("Same year already exists..", "Duplication encountered", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                    else
                    {
                        errorProvider3.SetError(combo_education_year, "can't be empty");
                        eYear = "1";
                    }

                    if (eCollege == "0" && eDegree == "0" && eYear == "0")
                    {
                        string degId = "", colgId = "";
                        DataTable deg_id = this.cntrl.get_degreeid(degree);
                        if (deg_id.Rows.Count > 0)
                        {
                            degId = deg_id.Rows[0]["id"].ToString();
                        }
                        DataTable colg_id = this.cntrl.get_collegeid(college);
                        if (colg_id.Rows.Count > 0)
                        {
                            colgId = colg_id.Rows[0]["id"].ToString();
                        }
                        this.cntrl.save_dr_education(doctor_id, degId, colgId, combo_education_year.Text);
                        DataTable education1 = this.cntrl.load_educationgrid(doctor_id);
                        dataGridView_education.DataSource = education1;
                    }
                }
                else
                {
                    MessageBox.Show("Education details with same name already exists..", "Duplication encountered", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void combo_membership_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void combo_membership_Click(object sender, EventArgs e)
        {
            DataTable member = this.cntrl.load_membercombo();
            combo_membership.DisplayMember = "name";
            combo_membership.ValueMember = "id";
            combo_membership.DataSource = member;
            combo_membership.Text = "";

        }

        private void button_add_membership_Click(object sender, EventArgs e)
        {
            Doctors_Practice_Details frm = new Doctors_Practice_Details();
            frm.frameid = "4";
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void button_save_membership_Click(object sender, EventArgs e)
        {
            try
            {
                int flagmember = 0;
                if (dataGridView_member.Rows.Count != 0)
                {

                    for (int i = 0; i < dataGridView_member.Rows.Count; i++)
                    {
                        if (dataGridView_member.Rows[i].Cells["col_member"].Value.ToString().Trim() == combo_membership.Text.Trim())
                        {
                            flagmember = 1;
                        }
                    }
                }
                if (flagmember == 0)
                {
                    string member = "";
                    member = combo_membership.Text;
                    string mem_id = this.cntrl.check_membership(member);
                    this.cntrl.save_member(doctor_id, mem_id);
                }
                else
                {
                    MessageBox.Show("Membership with same name already exists..", "Duplication encountered", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                DataTable member1 = this.cntrl.load_member(doctor_id);
                dataGridView_member.DataSource = member1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_save_awards_Click(object sender, EventArgs e)
        {
            try { 
            int flagaward = 0;
            if (dataGridView_awards.Rows.Count !=0)
            {
                for(int i =0;i<dataGridView_awards.Rows.Count;i++)
                {
                    if (dataGridView_awards.Rows[i].Cells["Award_name"].Value.ToString() ==text_awardname.Text.Trim()&&dataGridView_awards.Rows[i].Cells["year"].Value.ToString() == text_awardname.Text.Trim())
                    {
                        flagaward = 1;
                    }
                }
            }
            if(flagaward ==0)
            {
                this.cntrl.save_awards(doctor_id, text_awardname.Text, combo_award_year.Text);
                text_awardname.Text = "";
                combo_award_year.SelectedIndex = 0;
                DataTable awards1 = this.cntrl.load_awards(doctor_id);
                dataGridView_awards.DataSource = awards1;
            }
            else
            {
                MessageBox.Show("Award with same name already exists..", "Duplication encountered", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!...", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }



        }

        private void combo_reg_regcouncil_Click(object sender, EventArgs e)
        {
           

            DataTable reg = this.cntrl.load_councilcombo();
            combo_reg_regcouncil.DisplayMember = "name";
            combo_reg_regcouncil.ValueMember = "id";
            combo_reg_regcouncil.DataSource = reg;
            combo_reg_regcouncil.Text = "";

        }

        private void button_reg_add_Click(object sender, EventArgs e)
        {
            Doctors_Practice_Details frm = new Doctors_Practice_Details();
            frm.frameid = "5";
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void button_reg_save_Click(object sender, EventArgs e)
        {
            try
            {


                int flagReg = 0;
                if (dataGridView_reg.Rows.Count != 0)
                {
                    for (int i = 0; i < dataGridView_reg.Rows.Count; i++)
                    {
                        if (dataGridView_reg.Rows[i].Cells["col_reg"].Value.ToString().Trim() == combo_reg_regcouncil.Text.Trim() && dataGridView_reg.Rows[i].Cells["column_year"].Value.ToString().Trim() == combo_reg_year.Text.Trim() && dataGridView_reg.Rows[i].Cells["col_regNo"].Value.ToString().Trim() == text_reg_number.Text.Trim())
                        {
                            flagReg = 1;
                        }



                    }
                }
                if (flagReg == 0)
                {

                    string regCouncil = "";
                    regCouncil = combo_reg_regcouncil.Text;
                    string mem_id = this.cntrl.check_council(regCouncil);
                    int mem = int.Parse(mem_id);
                    this.cntrl.save_council(doctor_id, text_reg_number.Text, combo_reg_year.Text, mem_id);
                    text_reg_number.Text = "";
                }
                else
                {
                    MessageBox.Show("Registration Details with same name already exists..", "Duplication encountered", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                DataTable register = this.cntrl.load_council(doctor_id);
                dataGridView_reg.DataSource = register;
                combo_reg_regcouncil.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void combo_experience_city_Click(object sender, EventArgs e)
        {
           
            }
        private void button_experience_save_Click(object sender, EventArgs e)
        {
            try { 

            int flagExp = 0;
            if (dataGridView_experience.Rows.Count !=0)
            {
                for(int i =0;i<dataGridView_experience.Rows.Count;i++)
                {
                    if(dataGridView_experience.Rows[i].Cells["Col_from"].Value.ToString().Trim() == combo_eperience_from.Text.Trim() && dataGridView_experience.Rows[i].Cells["Col_To"].Value.ToString().Trim() == combo_experience_to.Text.Trim() && dataGridView_experience.Rows[i].Cells["Col_Role"].Value.ToString().Trim() == text_experience_role.Text.Trim() && dataGridView_experience.Rows[i].Cells["Exp_Company"].Value.ToString().Trim() == text_experience_company.Text.Trim() && dataGridView_experience.Rows[i].Cells["Col_city"].Value.ToString().Trim() == combo_experience_city.Text.Trim())
                    {
                        flagExp = 1;
                    }
                }
            }
            if(flagExp == 0)
            {
                int fromYear = 0, toYear = 0;

                if (combo_eperience_from.Text != "" && combo_experience_to.Text != "" && text_experience_role.Text != "" && text_experience_company.Text != "" && combo_experience_city.Text != "")
                {
                    fromYear = int.Parse(combo_eperience_from.Text);
                    toYear = int.Parse(combo_experience_to.Text);
                    if (toYear < fromYear)
                    {
                        errorProvider1.SetError(combo_experience_to, "Choose a higher Year");
                        combo_eperience_from.Focus();
                        return;
                    }
                    else
                    {
                        errorProvider1.Dispose();
                        this.cntrl.save_dr_experiences(doctor_id, combo_eperience_from.Text, combo_experience_to.Text, text_experience_role.Text, text_experience_company.Text, combo_experience_city.Text);
                        DataTable experience1 = this.cntrl.load_experiecncegrid(doctor_id);
                        dataGridView_experience.DataSource = experience1;
                        text_experience_role.Text = "";
                        text_experience_company.Text = "";
                        combo_experience_city.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Check if all values are filled", "empty fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            else
            {
                MessageBox.Show("Experiance details with same name already exists..", "Duplication encountered", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


}

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel_color.Show();
            choosecolor.Show();
        }

        private void label57_Click(object sender, EventArgs e)
        {
            calendrcolor = "0";
            Color a = label57.ForeColor;
            button1.BackColor = a;
            panel_color.Hide();
            choosecolor.Hide();
        }

        private void label58_Click(object sender, EventArgs e)
        {
            calendrcolor = "1";
            Color a = label58.ForeColor;
            button1.BackColor = a;
            panel_color.Hide();
            choosecolor.Hide();
        }

        private void label59_Click(object sender, EventArgs e)
        {
            calendrcolor = "2";
            Color a = label59.ForeColor;
            button1.BackColor = a;
            panel_color.Hide();
            choosecolor.Hide();
        }

        private void label60_Click(object sender, EventArgs e)
        {
            calendrcolor = "3";
            Color a = label60.ForeColor;
            button1.BackColor = a;
            panel_color.Hide();
            choosecolor.Hide();
        }

        private void label61_Click(object sender, EventArgs e)
        {
            calendrcolor = "4";
            Color a = label61.ForeColor;
            button1.BackColor = a;
            panel_color.Hide();
            choosecolor.Hide();
        }

        private void label62_Click(object sender, EventArgs e)
        {
            calendrcolor = "5";
            Color a = label62.ForeColor;
            button1.BackColor = a;
            panel_color.Hide();
            choosecolor.Hide();
        }

        private void label56_Click(object sender, EventArgs e)
        {
            calendrcolor = "6";
            Color a = label56.ForeColor;
            button1.BackColor = a;
            panel_color.Hide();
            choosecolor.Hide();
        }

        private void label63_Click(object sender, EventArgs e)
        {
            calendrcolor = "7";
            Color a = label63.ForeColor;
            button1.BackColor = a;
            panel_color.Hide();
            choosecolor.Hide();
        }

        private void label64_Click(object sender, EventArgs e)
        {
            calendrcolor = "8";
            Color a = label64.ForeColor;
            button1.BackColor = a;
            panel_color.Hide();
            choosecolor.Hide();
        }

        private void label65_Click(object sender, EventArgs e)
        {
            calendrcolor = "9";
            Color a = label65.ForeColor;
            button1.BackColor = a;
            panel_color.Hide();
            choosecolor.Hide();
        }

        private void label66_Click(object sender, EventArgs e)
        {
            calendrcolor = "10";
            Color a = label66.ForeColor;
            button1.BackColor = a;
            panel_color.Hide();
            choosecolor.Hide();
        }

        private void text_phone_Leave(object sender, EventArgs e)
        {
            if(text_phone.TextLength==10)
            {

            }
            else
            {
                MessageBox.Show("Invalied Mobile No","Invalied",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        /*  private void checkbox_checked_changed(object sender, EventArgs e)
          {
              gender = "male";  
          }

          private void chk_female_checkedchanged(object sender, EventArgs e)
          {
              gender = "female";
          }*/

        private void button_edit_drname_Click(object sender, EventArgs e)
        {
            if (text_drname.Text == "")
            {
                errorProvider1.SetError(text_drname, "can't be empty");
            }


            else if (text_phone.TextLength < 10)
            {
                errorProvider2.SetError(text_phone, "invalied phone!!");
                MessageBox.Show("Invalied Phone Number !!", "Invalied", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                errorProvider1.Dispose();
                String gender = "";
               if (r_male.Checked == true)
                {
                    gender = "male";

                }
                else
                    gender = "female";

                DataTable dt = this.cntrl.get_doctor_details(doctor_id);
                String mobile = text_phone.Text;
                string email = dt.Rows[0]["email_id"].ToString();
                string lpogintype = "";

                if (radio_login_no.Checked == true)
                {
                    status = "No";

                }
                
                if(radio_login_yes.Checked==true)
                {
                    status = "Yes";
                }

              



                if (dt.Rows[0]["login_type"].ToString().TrimEnd() != "admin" && dt.Rows[0]["login_type"].ToString().TrimEnd() != "ADMIN" && dt.Rows[0]["login_type"].ToString().TrimEnd() != "Admin")//
                {
                    lpogintype = cmbStaffType.Text;
                }
                else
                {
                    lpogintype = dt.Rows[0]["login_type"].ToString().TrimEnd();
                }
               if (txtPic.Text == openFileDialog1.FileName || txtPic.Text == "Image")
                {
                    RegistryKey regKeyAppRoot = Registry.CurrentUser.CreateSubKey("pappyjoe");
                    string strWindowsState = (string)regKeyAppRoot.GetValue("Server");
                    if (txtPic.Text != "")
                    {
                        try
                        {
                            if (File.Exists(@"\\" + strWindowsState + "\\Pappyjoe_utilities\\doctor_image\\" + doctor_id))
                            {
                            }
                            else
                            {
                                System.IO.File.Copy(txtPic.Text, @"\\" + strWindowsState + "\\Pappyjoe_utilities\\doctor_image\\" + doctor_id);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error!...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                  this.cntrl.update_doctor(doctor_id, text_drname.Text, text_phone.Text, text_email.Text, gender, combo_year.Text, rich_about.Text, txtPic.Text, lpogintype, textPassword.Text, calendrcolor, text_reg_no.Text, status, txt_fee.Text, txt_f_fee.Text, txt_f_period.Text);
                    this.cntrl.update_logintable(doctor_id, text_email.Text, textPassword.Text, lpogintype);
               }
             else
            {
                this.cntrl.update_doctor(doctor_id, text_drname.Text, text_phone.Text, text_email.Text, gender, combo_year.Text, rich_about.Text, txtPic.Text, lpogintype, textPassword.Text, calendrcolor, text_reg_no.Text, status, txt_fee.Text, txt_f_fee.Text, txt_f_period.Text);
                this.cntrl.update_logintable(doctor_id, text_email.Text, textPassword.Text, lpogintype);
                MessageBox.Show("Successfully Updated !!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



        }


        }
       
    }
}

