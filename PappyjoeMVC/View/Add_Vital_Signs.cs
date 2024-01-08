using PappyjoeMVC.Controller;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PappyjoeMVC.View
{
    public partial class Add_Vital_Signs : Form
    {
        Add_Vital_Signs_controller cntrl = new Add_Vital_Signs_controller();
        public string doctor_id = "0";
        public string staff_id = "0";
        public string patient_id = "0";
        public static string gender;
        double weight;
        double height;
        public int vital_id = 0;
        public Add_Vital_Signs()
        {
            InitializeComponent();
        }
        private void Add_Vital_Signs_Load(object sender, EventArgs e)
        {
            try
            {
                int dr_index = 0;
                DataTable dt = this.cntrl.get_all_doctorname();
                if (dt.Rows.Count > 0)
                {
                    Cmb_doctor.DisplayMember = "doctor_name";
                    Cmb_doctor.ValueMember = "id";
                    Cmb_doctor.DataSource = dt;
                    
                }
                if (vital_id>0)
                {
                    DataTable dt_main = this.cntrl.dt_load(vital_id.ToString());
                    if(dt_main.Rows.Count>0)
                    {
                        string docnam = this.cntrl.Get_DoctorName(dt_main.Rows[0]["dr_id"].ToString());
                        DataTable rs_patients = this.cntrl.Get_patient_id_name_gender(dt_main.Rows[0]["pt_id"].ToString());
                        if (rs_patients.Rows[0]["pt_name"].ToString() != "")
                        {
                            linkLabel_Name.Text = rs_patients.Rows[0]["pt_name"].ToString();
                        }
                        if (rs_patients.Rows[0]["pt_id"].ToString() != "")
                        {
                            linkLabel_id.Text = rs_patients.Rows[0]["pt_id"].ToString();
                        }
                        gender = rs_patients.Rows[0]["gender"].ToString();
                        //if (!String.IsNullOrWhiteSpace(text_Temp.Text))
                        //{
                        //    temp_type = combo_Te.Text;
                        //}
                        //if (!String.IsNullOrWhiteSpace(text_Bp_Syst.Text) || !String.IsNullOrWhiteSpace(text_Bp_Dias.Text))
                        //{
                        //    bp_type = combo_Bp.Text;
                        //}
                        combo_Te.Text= dt_main.Rows[0]["temp_type"].ToString();
                        combo_Bp.Text= dt_main.Rows[0]["bp_type"].ToString();
                        text_Pulse.Text = dt_main.Rows[0]["pulse"].ToString();
                        text_Temp.Text= dt_main.Rows[0]["temp"].ToString();
                        text_Bp_Syst.Text= dt_main.Rows[0]["bp_syst"].ToString();
                        text_Bp_Dias.Text= dt_main.Rows[0]["bp_dia"].ToString(); 
                        text_Weight.Text= dt_main.Rows[0]["weight"].ToString();
                        text_Resp.Text= dt_main.Rows[0]["resp"].ToString();
                        Txtheight.Text= dt_main.Rows[0]["Height"].ToString();
                        txt_spo2.Text = dt_main.Rows[0]["spo"].ToString();
                        dtp_date.Value = Convert.ToDateTime(dt_main.Rows[0]["date"].ToString());
                        Cmb_doctor.Text = docnam;
                        btn_Add.Text = "UPDATE";
                    }
                }
                else
                {
                    if (doctor_id == "0" || doctor_id == "")
                    {
                        doctor_id = "0";
                    }
                    string docnam = this.cntrl.Get_DoctorName(doctor_id);
                    DataTable rs_patients = this.cntrl.Get_patient_id_name_gender(patient_id);
                    if (rs_patients.Rows[0]["pt_name"].ToString() != "")
                    {
                        linkLabel_Name.Text = rs_patients.Rows[0]["pt_name"].ToString();
                    }
                    if (rs_patients.Rows[0]["pt_id"].ToString() != "")
                    {
                        linkLabel_id.Text = rs_patients.Rows[0]["pt_id"].ToString();
                    }
                    gender = rs_patients.Rows[0]["gender"].ToString();
                    if (doctor_id != "0")
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (dt.Rows[j]["id"].ToString() == doctor_id)
                            {
                                dr_index = j;
                            }
                        }
                        Cmb_doctor.SelectedIndex = dr_index;
                    }

                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void labelallpatient_Click(object sender, EventArgs e)
        {
            //var form2 = new PappyjoeMVC.View.Patients();
            //form2.doctor_id = doctor_id;
            //form2.ShowDialog();
            ////form2.Closed += (sender1, args) => this.Close();
            //this.Hide();
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
        private void labelallpatient_Click_1(object sender, EventArgs e)
        {
            var form2 = new PappyjoeMVC.View.Patients();
            form2.doctor_id = doctor_id;
            openform(form2);
        }

        private void text_Pulse_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch))
            {
                e.Handled = true;
            }
        }

        private void text_Pulse_KeyUp(object sender, KeyEventArgs e)
        {
            if (text_Pulse.Text != "")
            {
                if (decimal.Parse(text_Pulse.Text) < 10)
                {
                    label7.Show();
                    label7.Text = "Pulse can't be less than 10";
                }
                else if (decimal.Parse(text_Pulse.Text) > 200)
                {
                    label7.Show();
                    label7.Text = "Pulse can't be greater than 200";
                }
                else
                {
                    label7.Hide();
                }
            }
            else
            {
                label7.Hide();
            }
        }

        private void text_Temp_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void text_Temp_KeyUp(object sender, KeyEventArgs e)
        {
            if (text_Temp.Text != "")
            {
                if (decimal.Parse(text_Temp.Text) < 10)
                {
                    label7.Show();
                    label7.Text = "Temperature can't be less than 10";
                }
                else if (decimal.Parse(text_Temp.Text) > 111)
                {
                    label7.Show();
                    label7.Text = "Temperature can't be greater than 111";
                }
                else
                {
                    label7.Hide();
                }
            }
            else
            {
                label7.Hide();
            }
        }

        private void text_Bp_Syst_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void text_Bp_Syst_KeyUp(object sender, KeyEventArgs e)
        {
            if (text_Bp_Syst.Text != "")
            {
                if (decimal.Parse(text_Bp_Syst.Text) < 50)
                {
                    label7.Show();
                    label7.Text = "Systolic blood pressure can't be less than 50";
                }
                else if (decimal.Parse(text_Bp_Syst.Text) > 300)
                {
                    label7.Show();
                    label7.Text = "Systolic blood pressure can't be more than 300";
                }
                else
                {
                    label7.Hide();
                }
            }
            else
            {
                label7.Hide();
            }
        }

        private void text_Bp_Dias_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch))
            {
                e.Handled = true;
            }
        }

        private void text_Bp_Dias_KeyUp(object sender, KeyEventArgs e)
        {
            if (text_Bp_Dias.Text != "")
            {
                if (decimal.Parse(text_Bp_Dias.Text) < 25)
                {
                    label7.Show();
                    label7.Text = "Diastolic blood pressure can't be less than 25";
                }
                else if (decimal.Parse(text_Bp_Dias.Text) > 200)
                {
                    label7.Show();
                    label7.Text = "Diastolic blood pressure can't be more than 200";
                }
                else
                {
                    label7.Hide();
                }
            }
            else
            {
                label7.Hide();
            }
        }

        private void Txtheight_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void Txtheight_TextChanged(object sender, EventArgs e)
        {
            BMI_Calculate(gender);
        }

        private void text_Weight_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void text_Weight_TextChanged(object sender, EventArgs e)
        {
            BMI_Calculate(gender);
        }

        private void text_Resp_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void text_Resp_KeyUp(object sender, KeyEventArgs e)
        {
            if (text_Resp.Text != "")
            {
                if (decimal.Parse(text_Resp.Text) < 10)
                {
                    label7.Show();
                    label7.Text = "Respiratory rate can't be less than 10";
                }
                else if (decimal.Parse(text_Resp.Text) > 70)
                {
                    label7.Show();
                    label7.Text = "Respiratory rate can't be more than 70";
                }
                else
                {
                    label7.Hide();
                }
            }
            else
            {
                label7.Hide();
            }
        }

        private void txtBMI_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            var form2 = new PappyjoeMVC.View.Vital_Signs();
            if (doctor_id == "0" || doctor_id == "") { form2.staff_id = staff_id; } else { form2.doctor_id = doctor_id; }
            //Vital_Signs_controller controller = new Vital_Signs_controller(form2);
            form2.patient_id = patient_id;
            openform(form2);
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                string doctor = "", maxid = "";
                string temp_type = "", bp_type = ""; int i = 0;
                if (!String.IsNullOrWhiteSpace(text_Temp.Text))
                {
                    temp_type = combo_Te.Text;
                }
                if (!String.IsNullOrWhiteSpace(text_Bp_Syst.Text) || !String.IsNullOrWhiteSpace(text_Bp_Dias.Text))
                {
                    bp_type = combo_Bp.Text;
                }
                if (!String.IsNullOrWhiteSpace(text_Pulse.Text) || !String.IsNullOrWhiteSpace(text_Temp.Text) || !String.IsNullOrWhiteSpace(Txtheight.Text) || !String.IsNullOrWhiteSpace(text_Bp_Syst.Text) || !String.IsNullOrWhiteSpace(text_Bp_Dias.Text) || !String.IsNullOrWhiteSpace(text_Resp.Text) || !String.IsNullOrWhiteSpace(text_Weight.Text))
                { 
                    if(btn_Add.Text== "SAVE")
                    {
                       
                        string dr_id = doctor_id;
                        if (doctor_id != "0")
                        {
                            doctor = Cmb_doctor.Text;
                            dr_id = Cmb_doctor.SelectedValue.ToString();
                        }
                        string date = Convert.ToDateTime(dtp_date.Value).ToString("yyyy-MM-dd");
                        DateTime Timeonly1 = DateTime.Now;
                        this.cntrl.save_vital_main(patient_id, dr_id, date);
                        DataTable dt_max = this.cntrl.dt_get_maxid();
                        if(dt_max.Rows.Count>0)
                        {
                            maxid = dt_max.Rows[0][0].ToString();
                        }
                        //bhj add time
                        i = this.cntrl.submit1(patient_id, dr_id, doctor, temp_type, bp_type, text_Pulse.Text, text_Temp.Text, text_Bp_Syst.Text, text_Bp_Dias.Text, text_Weight.Text, text_Resp.Text, date, Txtheight.Text, txt_spo2.Text, maxid, Convert.ToString(Timeonly1.ToString("hh:mm tt")));
                      //  i = this.cntrl.submit(patient_id, dr_id, doctor, temp_type, bp_type, text_Pulse.Text, text_Temp.Text, text_Bp_Syst.Text, text_Bp_Dias.Text, text_Weight.Text, text_Resp.Text, date, Txtheight.Text, txt_spo2.Text, maxid));
                        if (i > 0)
                        {
                            string dt = DateTime.Now.Date.ToString("yyyy-MM-dd");
                            DateTime Timeonly = DateTime.Now;
                            this.cntrl.save_log(doctor_id, "Vital Sign", " Add Vital Sign", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Add", maxid);
                            var form2 = new PappyjoeMVC.View.Vital_Signs();
                            if (doctor_id == "0" || doctor_id == "")
                            { form2.staff_id = staff_id; }
                            else { form2.doctor_id = doctor_id; }
                            form2.patient_id = patient_id;
                            openform(form2);
                        }
                        else
                        {
                            MessageBox.Show("Inseration Failed!..", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        string doctor_ = "";
                        string dr_id = doctor_id;

                        doctor_ = Cmb_doctor.Text;
                            dr_id = Cmb_doctor.SelectedValue.ToString();
                        string date = Convert.ToDateTime(dtp_date.Value).ToString("yyyy-MM-dd");
                        DateTime Timeonly1 = DateTime.Now;
                        this.cntrl.update_vital_main(patient_id, dr_id, date, vital_id.ToString());
                       // i = this.cntrl.update(patient_id, dr_id, doctor_, temp_type, bp_type, text_Pulse.Text, text_Temp.Text, text_Bp_Syst.Text, text_Bp_Dias.Text, text_Weight.Text, text_Resp.Text, date, Txtheight.Text, txt_spo2.Text,vital_id.ToString());
                        i = this.cntrl.updates(patient_id, dr_id, doctor_, temp_type, bp_type, text_Pulse.Text, text_Temp.Text, text_Bp_Syst.Text, text_Bp_Dias.Text, text_Weight.Text, text_Resp.Text, date, Txtheight.Text, txt_spo2.Text, Convert.ToString(Timeonly1.ToString("hh:mm tt")), vital_id.ToString());
                        if (i > 0)
                        {
                            string dt = DateTime.Now.Date.ToString("yyyy-MM-dd");
                            DateTime Timeonly = DateTime.Now;
                            this.cntrl.save_log(doctor_id, "Vital Sign", " Edit Vital Sign", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Edit", maxid);
                            var form2 = new PappyjoeMVC.View.Vital_Signs();
                            if (doctor_id == "0" || doctor_id == "")
                            { form2.staff_id = staff_id; }
                            else { form2.doctor_id = doctor_id; }
                            form2.patient_id = patient_id;
                            openform(form2);
                        }
                        else
                        {
                            MessageBox.Show("Updation Failed!..", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                   
                }
                else
                {
                    label7.Show();
                    label7.Text = "Data not found...";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void BMI_Calculate(string gd)
        {
            try
            {
                if (text_Weight.Text != "" && Txtheight.Text != "")
                {
                    gender = gd;
                    weight = Double.Parse(text_Weight.Text);
                    height = Double.Parse(Txtheight.Text);
                    double BMI = Math.Round((weight / (height * height)) * 10000, 1);
                    if (BMI != null)
                    {
                        label10.Visible = true;
                        txtBMI.Text = BMI.ToString();
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
                    txtBMI.Text = "";
                    label10.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void txt_spo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
