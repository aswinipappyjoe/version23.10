using PappyjoeMVC.Controller;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using PappyjoeMVC.View;
using PappyjoeMVC.Model;
using System.IO;

namespace PappyjoeMVC.View
{
    public partial class Vital_Signs : Form
    {
        Vital_Signs_controller cntrl=new Vital_Signs_controller();
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
        public string doctor_id = "0";
        public string staff_id = "0";
        public string patient_id = "0";
        double weight;
        double height;
        string gender;
        double BMI;
        public Vital_Signs()
        {
            InitializeComponent();
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
        private void btn_ADD_Click(object sender, EventArgs e)
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

        private void Vital_Signs_Load(object sender, EventArgs e)
        {
            try
            {
               
                    DataTable clinicname = this.cntrl.Get_CompanyNAme();
                    if (clinicname.Rows.Count > 0)
                    {
                        string clinicn = "";
                        clinicn = clinicname.Rows[0]["name"].ToString();
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
                    dataGridView_invoice.Show();
                    dataGridView_invoice.Columns[0].Width = 30;
                    dataGridView_invoice.Columns[1].Width = 225;
                    dataGridView_invoice.Columns[2].Width = 15;
                    dataGridView_invoice.Columns[3].Width = 250; dataGridView_invoice.Columns[4].Width = 200;
                    dataGridView_invoice.Columns[5].Width = 28;
                    dataGridView_invoice.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dataGridView_invoice.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    DataTable vital = this.cntrl.vital(patient_id);
                    if (vital.Rows.Count > 25)
                    {
                        label4.Visible = true;
                    }
                    else
                    {
                        label4.Visible = false;
                    }
                    if (vital.Rows.Count > 0)
                    {
                        int i = 0;
                        for (int j = 0; j < vital.Rows.Count; j++)
                        {
                            dataGridView_invoice.Rows.Add(vital.Rows[j]["main_id"].ToString(), String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(vital.Rows[j]["date"].ToString())), "", vital.Rows[j]["time"].ToString(), "", PappyjoeMVC.Properties.Resources.Bill);//add time bhj
                            dataGridView_invoice.Rows.Add("", "", "", "", "", PappyjoeMVC.Properties.Resources.blank);
                            dataGridView_invoice.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                        dataGridView_invoice.Rows[i].DefaultCellStyle.BackColor = Color.CadetBlue;
                            i = i + 1;
                            if (vital.Rows[j]["pulse"].ToString() != "")
                            {
                                dataGridView_invoice.Rows.Add("", "PULSE ", ":", vital.Rows[j]["pulse"].ToString(), "", PappyjoeMVC.Properties.Resources.blank);
                                i = i + 1;
                                dataGridView_invoice.Rows[i].Cells[1].Style.ForeColor = Color.DimGray;
                                dataGridView_invoice.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                                dataGridView_invoice.Rows[i].Cells[3].Style.ForeColor = Color.DarkGreen;
                                dataGridView_invoice.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                            }
                            if (vital.Rows[j]["temp"].ToString() != "")
                            {
                                dataGridView_invoice.Rows.Add("", "TEMPERATURE ", ":", vital.Rows[j]["temp"].ToString() + " ( " + vital.Rows[j]["temp_type"].ToString() + " ) ", "", PappyjoeMVC.Properties.Resources.blank);
                                i = i + 1;
                                dataGridView_invoice.Rows[i].Cells[1].Style.ForeColor = Color.DimGray;
                                dataGridView_invoice.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                                dataGridView_invoice.Rows[i].Cells[3].Style.ForeColor = Color.DarkGreen;
                                dataGridView_invoice.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                            }
                            if (vital.Rows[j]["bp_syst"].ToString() != "")
                            {
                                dataGridView_invoice.Rows.Add("", "BLOOD PRESSURE ( SYSTOLIC ) ", ":", vital.Rows[j]["bp_syst"].ToString() + " ( " + vital.Rows[j]["bp_type"].ToString() + " ) ", "", PappyjoeMVC.Properties.Resources.blank);
                                i = i + 1;
                                dataGridView_invoice.Rows[i].Cells[1].Style.ForeColor = Color.DimGray;
                                dataGridView_invoice.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                                dataGridView_invoice.Rows[i].Cells[3].Style.ForeColor = Color.DarkGreen;
                                dataGridView_invoice.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                            }
                            if (vital.Rows[j]["bp_dia"].ToString() != "")
                            {
                                dataGridView_invoice.Rows.Add("", "BLOOD PRESSURE ( DIASTOLIC )  ", ":", vital.Rows[j]["bp_dia"].ToString() + " ( " + vital.Rows[j]["bp_type"].ToString() + " ) ", "", PappyjoeMVC.Properties.Resources.blank);
                                i = i + 1;
                                dataGridView_invoice.Rows[i].Cells[1].Style.ForeColor = Color.DimGray;
                                dataGridView_invoice.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                                dataGridView_invoice.Rows[i].Cells[3].Style.ForeColor = Color.DarkGreen;
                                dataGridView_invoice.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                            }
                            if (vital.Rows[j]["Height"].ToString() != "")
                            {
                                dataGridView_invoice.Rows.Add("", "HEIGHT  ", ":", vital.Rows[j]["Height"].ToString() + "(Cm)", "", PappyjoeMVC.Properties.Resources.blank);
                                i = i + 1;
                                dataGridView_invoice.Rows[i].Cells[1].Style.ForeColor = Color.DimGray;
                                dataGridView_invoice.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                                dataGridView_invoice.Rows[i].Cells[3].Style.ForeColor = Color.DarkGreen;
                                dataGridView_invoice.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                            }
                            if (vital.Rows[j]["weight"].ToString() != "")
                            {
                                dataGridView_invoice.Rows.Add("", "WEIGHT  ", ":", vital.Rows[j]["weight"].ToString() + "(Kg)", "", PappyjoeMVC.Properties.Resources.blank);
                                i = i + 1;
                                dataGridView_invoice.Rows[i].Cells[1].Style.ForeColor = Color.DimGray;
                                dataGridView_invoice.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                                dataGridView_invoice.Rows[i].Cells[3].Style.ForeColor = Color.DarkGreen;
                                dataGridView_invoice.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                            }
                            if (vital.Rows[j]["resp"].ToString() != "")
                            {
                                dataGridView_invoice.Rows.Add("", "RESPIRATORY RATE ", ":", vital.Rows[j]["resp"].ToString(), "", PappyjoeMVC.Properties.Resources.blank);
                                i = i + 1;
                                dataGridView_invoice.Rows[i].Cells[1].Style.ForeColor = Color.DimGray;
                                dataGridView_invoice.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                                dataGridView_invoice.Rows[i].Cells[3].Style.ForeColor = Color.DarkGreen;
                                dataGridView_invoice.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                            }
                            if (vital.Rows[j]["weight"].ToString() != null && vital.Rows[j]["weight"].ToString() != "" && vital.Rows[j]["Height"].ToString() != null && vital.Rows[j]["Height"].ToString() != "")
                            {
                                weight = Convert.ToDouble(vital.Rows[j]["weight"].ToString());
                                height = Convert.ToDouble(vital.Rows[j]["Height"].ToString());
                            }
                            else
                            {
                                weight = Convert.ToDouble("0.00");
                                height = Convert.ToDouble("0.00");
                            }
                            gender = rs_patients.Rows[0]["gender"].ToString();
                            string msg = "";
                            if (weight > 0 && height > 0)
                            {
                                BMI = Math.Round((weight / (height * height)) * 10000, 1);
                                if (BMI != null)
                                {
                                    if (BMI < 19 && gender == "Female")
                                    {
                                        msg = "BMI is low";
                                    }
                                    if (BMI >= 19 & BMI <= 24 & gender == "Female")
                                    {
                                        msg = "Normal";
                                    }
                                    if (BMI > 24 & gender == "Female")
                                    {
                                        msg = "BMI is High";
                                    }
                                    if (BMI < 20 & gender == "Male")
                                    {
                                        msg = "BMI is low";
                                    }
                                    if (BMI >= 20 & BMI <= 25 & gender == "Male")
                                    {
                                        msg = "Normal";
                                    }
                                    if (BMI > 25 & gender == "Male")
                                    {
                                        msg = "BMI is High";
                                    }
                                }
                                if (BMI > 0)
                                {
                                    dataGridView_invoice.Rows.Add("", "BMI(BOADY MASS INDEX) ", ":", BMI + "  ,   " + msg, "", PappyjoeMVC.Properties.Resources.blank);
                                    i = i + 1;
                                    if (msg == "BMI is low")
                                        dataGridView_invoice.Rows[i].Cells[4].Style.ForeColor = Color.Red;
                                    else if (msg == "Normal")
                                        dataGridView_invoice.Rows[i].Cells[4].Style.ForeColor = Color.DarkGreen;
                                    else if (msg == "BMI is High")
                                        dataGridView_invoice.Rows[i].Cells[4].Style.ForeColor = Color.Red;
                                    dataGridView_invoice.Rows[i].Cells[1].Style.ForeColor = Color.DimGray;
                                    dataGridView_invoice.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                                    dataGridView_invoice.Rows[i].Cells[3].Style.ForeColor = Color.DarkGreen;
                                    dataGridView_invoice.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                                }
                            }
                            if (vital.Rows[j]["spo"].ToString() != "")
                            {
                                dataGridView_invoice.Rows.Add("", "SPO2 ", ":", vital.Rows[j]["spo"].ToString(), "", PappyjoeMVC.Properties.Resources.blank);
                                i = i + 1;
                                dataGridView_invoice.Rows[i].Cells[1].Style.ForeColor = Color.DimGray;
                                dataGridView_invoice.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                                dataGridView_invoice.Rows[i].Cells[3].Style.ForeColor = Color.DarkGreen;
                                dataGridView_invoice.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                            }
                            dataGridView_invoice.Rows.Add("", "Recorded By : Dr." + vital.Rows[j]["dr_name"].ToString(), "", "", "", PappyjoeMVC.Properties.Resources.blank);
                            dataGridView_invoice.Rows[i + 1].Cells[1].Style.ForeColor = Color.Red;
                            dataGridView_invoice.Rows[i + 1].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Italic);
                            i = i + 2;
                        }
                    }
                    if (dataGridView_invoice.Rows.Count <= 0)
                    {
                        int x = (panel1.Size.Width - Lab_Msg.Size.Width) / 2;
                        Lab_Msg.Location = new Point(x, Lab_Msg.Location.Y);
                        Lab_Msg.Show();
                    }
                    else
                    {
                        Lab_Msg.Hide();
                        Lab_Msg.Location = new System.Drawing.Point(165, 165);
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
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void labelallpatient_Click(object sender, EventArgs e)
        {
            var form2 = new Patients();
            form2.doctor_id = doctor_id;
            openform(form2);
        }

        private void labelprofile_Click(object sender, EventArgs e)
        {
            var form2 = new PappyjoeMVC.View.Patient_Profile_Details();
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
        user_privillage_model privi_mdl = new user_privillage_model();
        private void labelsms_Click(object sender, EventArgs e)
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
                    string id = privi_mdl.findings_Add(doctor_id);
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
        GlobalVariables gv = new GlobalVariables();
        private void pb_trtmntAdd_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id;
                id = privi_mdl.treatment_add(doctor_id);
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
                id = privi_mdl.finishedtreatment_add(doctor_id);
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
        public void reciept_count()
        {
            DataTable Payments = db.table("select receipt_no,amount_paid,invoice_no,procedure_name,mode_of_payment from tbl_payment where pt_id='" + patient_id + "'");
            if (Payments.Rows.Count > 0)
            {
                lb_Reciepts_cnt.Text = Payments.Rows.Count.ToString();
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
        public int vital_id = 0;
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vital_id > 0)
            {
                var form2 = new PappyjoeMVC.View.Add_Vital_Signs();
                form2.doctor_id = doctor_id;
                form2.patient_id = patient_id;
                form2.vital_id = vital_id;
                openform(form2);
            }
        }

        private void dataGridView_invoice_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
        }

       //bhj
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
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
                        MessageBox.Show("There is No Privilege to delete vital signs", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                if (vital_id > 0)
                {
                    DialogResult res = MessageBox.Show("Are you sure you want to delete..?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (res == DialogResult.Yes)
                    {
                        this.cntrl.delete_vital(vital_id);
                        db.execute("delete from tbl_vital_signs where id='" + vital_id + "'");
                        this.cntrl.vital(patient_id);
                        dataGridView_invoice.Show();
                        dataGridView_invoice.Columns[0].Width = 30;
                        dataGridView_invoice.Columns[1].Width = 225;
                        dataGridView_invoice.Columns[2].Width = 15;
                        dataGridView_invoice.Columns[3].Width = 250; dataGridView_invoice.Columns[4].Width = 200;
                        dataGridView_invoice.Columns[5].Width = 28;
                        dataGridView_invoice.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_invoice.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        DataTable vital = this.cntrl.vital(patient_id);
                        if (vital.Rows.Count > 25)
                        {
                            label4.Visible = true;
                        }
                        else
                        {
                            label4.Visible = false;
                        }
                        if (vital.Rows.Count > 0)
                        {
                            int i = 0;
                            for (int j = 0; j < vital.Rows.Count; j++)
                            {
                                dataGridView_invoice.Rows.Add(vital.Rows[j]["main_id"].ToString(), String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(vital.Rows[j]["date"].ToString())), "", "", "", PappyjoeMVC.Properties.Resources.Bill);
                                dataGridView_invoice.Rows.Add("", "", "", "", "", PappyjoeMVC.Properties.Resources.blank);
                                dataGridView_invoice.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                                i = i + 1;
                                if (vital.Rows[j]["pulse"].ToString() != "")
                                {
                                    dataGridView_invoice.Rows.Add("", "PULSE ", ":", vital.Rows[j]["pulse"].ToString(), "", PappyjoeMVC.Properties.Resources.blank);
                                    i = i + 1;
                                    dataGridView_invoice.Rows[i].Cells[1].Style.ForeColor = Color.DimGray;
                                    dataGridView_invoice.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                                    dataGridView_invoice.Rows[i].Cells[3].Style.ForeColor = Color.DarkGreen;
                                    dataGridView_invoice.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                                }
                                if (vital.Rows[j]["temp"].ToString() != "")
                                {
                                    dataGridView_invoice.Rows.Add("", "TEMPERATURE ", ":", vital.Rows[j]["temp"].ToString() + " ( " + vital.Rows[j]["temp_type"].ToString() + " ) ", "", PappyjoeMVC.Properties.Resources.blank);
                                    i = i + 1;
                                    dataGridView_invoice.Rows[i].Cells[1].Style.ForeColor = Color.DimGray;
                                    dataGridView_invoice.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                                    dataGridView_invoice.Rows[i].Cells[3].Style.ForeColor = Color.DarkGreen;
                                    dataGridView_invoice.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                                }
                                if (vital.Rows[j]["bp_syst"].ToString() != "")
                                {
                                    dataGridView_invoice.Rows.Add("", "BLOOD PRESSURE ( SYSTOLIC ) ", ":", vital.Rows[j]["bp_syst"].ToString() + " ( " + vital.Rows[j]["bp_type"].ToString() + " ) ", "", PappyjoeMVC.Properties.Resources.blank);
                                    i = i + 1;
                                    dataGridView_invoice.Rows[i].Cells[1].Style.ForeColor = Color.DimGray;
                                    dataGridView_invoice.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                                    dataGridView_invoice.Rows[i].Cells[3].Style.ForeColor = Color.DarkGreen;
                                    dataGridView_invoice.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                                }
                                if (vital.Rows[j]["bp_dia"].ToString() != "")
                                {
                                    dataGridView_invoice.Rows.Add("", "BLOOD PRESSURE ( DIASTOLIC )  ", ":", vital.Rows[j]["bp_dia"].ToString() + " ( " + vital.Rows[j]["bp_type"].ToString() + " ) ", "", PappyjoeMVC.Properties.Resources.blank);
                                    i = i + 1;
                                    dataGridView_invoice.Rows[i].Cells[1].Style.ForeColor = Color.DimGray;
                                    dataGridView_invoice.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                                    dataGridView_invoice.Rows[i].Cells[3].Style.ForeColor = Color.DarkGreen;
                                    dataGridView_invoice.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                                }
                                if (vital.Rows[j]["Height"].ToString() != "")
                                {
                                    dataGridView_invoice.Rows.Add("", "HEIGHT  ", ":", vital.Rows[j]["Height"].ToString() + "(Cm)", "", PappyjoeMVC.Properties.Resources.blank);
                                    i = i + 1;
                                    dataGridView_invoice.Rows[i].Cells[1].Style.ForeColor = Color.DimGray;
                                    dataGridView_invoice.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                                    dataGridView_invoice.Rows[i].Cells[3].Style.ForeColor = Color.DarkGreen;
                                    dataGridView_invoice.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                                }
                                if (vital.Rows[j]["weight"].ToString() != "")
                                {
                                    dataGridView_invoice.Rows.Add("", "WEIGHT  ", ":", vital.Rows[j]["weight"].ToString() + "(Kg)", "", PappyjoeMVC.Properties.Resources.blank);
                                    i = i + 1;
                                    dataGridView_invoice.Rows[i].Cells[1].Style.ForeColor = Color.DimGray;
                                    dataGridView_invoice.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                                    dataGridView_invoice.Rows[i].Cells[3].Style.ForeColor = Color.DarkGreen;
                                    dataGridView_invoice.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                                }
                                if (vital.Rows[j]["resp"].ToString() != "")
                                {
                                    dataGridView_invoice.Rows.Add("", "RESPIRATORY RATE ", ":", vital.Rows[j]["resp"].ToString(), "", PappyjoeMVC.Properties.Resources.blank);
                                    i = i + 1;
                                    dataGridView_invoice.Rows[i].Cells[1].Style.ForeColor = Color.DimGray;
                                    dataGridView_invoice.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                                    dataGridView_invoice.Rows[i].Cells[3].Style.ForeColor = Color.DarkGreen;
                                    dataGridView_invoice.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                                }
                                if (vital.Rows[j]["weight"].ToString() != null && vital.Rows[j]["weight"].ToString() != "" && vital.Rows[j]["Height"].ToString() != null && vital.Rows[j]["Height"].ToString() != "")
                                {
                                    weight = Convert.ToDouble(vital.Rows[j]["weight"].ToString());
                                    height = Convert.ToDouble(vital.Rows[j]["Height"].ToString());
                                }
                                else
                                {
                                    weight = Convert.ToDouble("0.00");
                                    height = Convert.ToDouble("0.00");
                                }
                                DataTable rs_patients = this.cntrl.Get_patient_id_name_gender(patient_id);
                                gender = rs_patients.Rows[0]["gender"].ToString();
                                string msg = "";
                                if (weight > 0 && height > 0)
                                {
                                    BMI = Math.Round((weight / (height * height)) * 10000, 1);
                                    if (BMI != null)
                                    {
                                        if (BMI < 19 && gender == "Female")
                                        {
                                            msg = "BMI is low";
                                        }
                                        if (BMI >= 19 & BMI <= 24 & gender == "Female")
                                        {
                                            msg = "Normal";
                                        }
                                        if (BMI > 24 & gender == "Female")
                                        {
                                            msg = "BMI is High";
                                        }
                                        if (BMI < 20 & gender == "Male")
                                        {
                                            msg = "BMI is low";
                                        }
                                        if (BMI >= 20 & BMI <= 25 & gender == "Male")
                                        {
                                            msg = "Normal";
                                        }
                                        if (BMI > 25 & gender == "Male")
                                        {
                                            msg = "BMI is High";
                                        }
                                    }
                                    if (BMI > 0)
                                    {
                                        dataGridView_invoice.Rows.Add("", "BMI(BOADY MASS INDEX) ", ":", BMI + "  ,   " + msg, "", PappyjoeMVC.Properties.Resources.blank);
                                        i = i + 1;
                                        if (msg == "BMI is low")
                                            dataGridView_invoice.Rows[i].Cells[4].Style.ForeColor = Color.Red;
                                        else if (msg == "Normal")
                                            dataGridView_invoice.Rows[i].Cells[4].Style.ForeColor = Color.DarkGreen;
                                        else if (msg == "BMI is High")
                                            dataGridView_invoice.Rows[i].Cells[4].Style.ForeColor = Color.Red;
                                        dataGridView_invoice.Rows[i].Cells[1].Style.ForeColor = Color.DimGray;
                                        dataGridView_invoice.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                                        dataGridView_invoice.Rows[i].Cells[3].Style.ForeColor = Color.DarkGreen;
                                        dataGridView_invoice.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                                    }
                                }
                                if (vital.Rows[j]["spo"].ToString() != "")
                                {
                                    dataGridView_invoice.Rows.Add("", "SPO2 ", ":", vital.Rows[j]["spo"].ToString(), "", PappyjoeMVC.Properties.Resources.blank);
                                    i = i + 1;
                                    dataGridView_invoice.Rows[i].Cells[1].Style.ForeColor = Color.DimGray;
                                    dataGridView_invoice.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                                    dataGridView_invoice.Rows[i].Cells[3].Style.ForeColor = Color.DarkGreen;
                                    dataGridView_invoice.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                                }
                                dataGridView_invoice.Rows.Add("", "Recorded By : Dr." + vital.Rows[j]["dr_name"].ToString(), "", "", "", PappyjoeMVC.Properties.Resources.blank);
                                dataGridView_invoice.Rows[i + 1].Cells[1].Style.ForeColor = Color.Red;
                                dataGridView_invoice.Rows[i + 1].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Italic);
                                i = i + 2;
                            }
                        }
                        if (dataGridView_invoice.Rows.Count <= 0)
                        {
                            int x = (panel1.Size.Width - Lab_Msg.Size.Width) / 2;
                            Lab_Msg.Location = new Point(x, Lab_Msg.Location.Y);
                            Lab_Msg.Show();
                        }
                        else
                        {
                            Lab_Msg.Hide();
                            Lab_Msg.Location = new System.Drawing.Point(165, 165);
                        }
                        string dt = DateTime.Now.Date.ToString("yyyy-MM-dd");
                        DateTime Timeonly = DateTime.Now;
                        this.cntrl.save_log(doctor_id, "Vital Signs", " Delete vital signs", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Delete", vital_id);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void dataGridView_invoice_MouseClick(object sender, MouseEventArgs e)//bhj
        {
            try
            {
                int currentMouseOverRow = dataGridView_invoice.HitTest(e.X, e.Y).RowIndex;
                int currentMouseOverColumn = dataGridView_invoice.HitTest(e.X, e.Y).ColumnIndex;
                if (currentMouseOverRow >= 0)
                {
                    if (currentMouseOverColumn == 5)// if (e.ColumnIndex == 5)//currentMouseOverColumn
                    {
                        if (dataGridView_invoice.Rows[currentMouseOverRow].Cells[0].Value.ToString() != "0")
                        {
                            vital_id = Convert.ToInt32(dataGridView_invoice.Rows[currentMouseOverRow].Cells[0].Value
                                .ToString());
                            contextMenuStrip1.Show(dataGridView_invoice, new System.Drawing.Point(860 - 120, e.Y));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printhtml();
        }
        string logo_name = "";
        public void printhtml()
        {
            int kw = 0;
            System.Data.DataTable dt1 = this.cntrl.Get_Patient_Details(patient_id);
            System.Data.DataTable dt_vital = this.cntrl.vitals_print(patient_id,vital_id.ToString());
            string doctor_name = "";
            string tr_date = "";
            if (dt_vital.Rows.Count > 0)
            {
                doctor_name = dt_vital.Rows[0]["dr_name"].ToString();
                tr_date = dt_vital.Rows[0]["date"].ToString();
            }
            string Pname = "", Gender = "", address = "", DOA = "", age = "", Mobile = "";
            if (dt1.Rows.Count > 0)
            {
                Pname = dt1.Rows[0]["pt_name"].ToString();
                Gender = dt1.Rows[0]["gender"].ToString();
                Mobile = dt1.Rows[0]["primary_mobile_number"].ToString();
                DOA = DateTime.Parse(dt1.Rows[0]["date"].ToString()).ToString("dd/MM/yyyy");
                if (dt1.Rows[0]["date_of_birth"].ToString() != "")
                {
                    age = DateTime.Parse(dt1.Rows[0]["date_of_birth"].ToString()).ToString("dd/MM/yyyy");
                }
            }
            int Dexist = 0;
            string clinicn = "";
            string Clinic = "";
            System.Data.DataTable dtp = this.cntrl.get_company_details();
            if (dtp.Rows.Count > 0)
            {
                clinicn = dtp.Rows[0]["name"].ToString();
                Clinic = clinicn.Replace("¤", "'");
            }
            string Apppath = System.IO.Directory.GetCurrentDirectory();
            System.IO.StreamWriter sWrite = new StreamWriter(Apppath + "\\Treatment_print.html");
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
                    sWrite.WriteLine("<td width='100' height='75px' align='left' rowspan='3'><img src='" + Appath + "\\" + logo_name + "' width='77' height='78' style='width:100px;height:100px;'></td>  ");
                    sWrite.WriteLine("<td width='588' align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + Clinic + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;&nbsp;" + dtp.Rows[0]["street_address"].ToString() + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + dtp.Rows[0]["contact_no"].ToString() + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                    sWrite.WriteLine("</table>");
                }
                else
                {
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td  align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + Clinic + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;&nbsp;" + dtp.Rows[0]["street_address"].ToString() + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + dtp.Rows[0]["contact_no"].ToString() + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                    sWrite.WriteLine("</table>");
                }
            }
            else
            {
                sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td  align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + Clinic + "</font></td></tr>");
                sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;&nbsp;" + dtp.Rows[0]["street_address"].ToString() + "</font></td></tr>");
                sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;&nbsp;" + dtp.Rows[0]["contact_no"].ToString() + "</font></td></tr>");
                sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                sWrite.WriteLine("</table>");
            }
            string sexage = "";
            address = "";
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
            sWrite.WriteLine(" <td align='left' height=25><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>" + dt1.Rows[0]["pt_name"].ToString() + "</b><i> (" + sexage + ")</i></font></td>");
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
            if (dt1.Rows[0]["aadhar_id"].ToString() != "")
            {
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + "Aadhaar ID:" + dt1.Rows[0]["aadhar_id"].ToString() + " </font></td>");
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
            sWrite.WriteLine("<tr>");
            sWrite.WriteLine("<td align='left' width='400px' height='30px'><FONT FACE='Geneva, Segoe UI' SIZE=2><FONT COLOR=black >By</FONT> :Dr. <b>" + doctor_name + " </b></font></td>");
            sWrite.WriteLine("</tr>");
            sWrite.WriteLine("</table>");
            sWrite.WriteLine("<br>");

            sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");
            sWrite.WriteLine("<tr>");
            sWrite.WriteLine("<tr><td colspan=10><center><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=5>Vital Signs</FONT></center></td></tr>");
            sWrite.WriteLine("<td width=250px></td>");
            if (dt_vital.Rows.Count > 0)
            {
                sWrite.WriteLine("<td align='right'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> <FONT COLOR=black>Date : </FONT>" + DateTime.Parse(dt_vital.Rows[0]["date"].ToString()).ToString("dd MMM yyyy") + "</font></td>");
            }
            else
            {
                sWrite.WriteLine("<td align='right'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> <FONT COLOR=black>Date : </FONT>" + DateTime.Now.ToString("dd MMM yyyy") + "</font></td>");
            }
            sWrite.WriteLine("</tr>");
            sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
            sWrite.WriteLine("</table>");
           

            sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");

            sWrite.WriteLine("<tr>");
            sWrite.WriteLine("<td><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3><b>Vitals Signs</b></FONT></td>");
            sWrite.WriteLine(" </tr>");

            sWrite.WriteLine("<tr >");
            sWrite.WriteLine("<td align='left' width='20px' height='8'><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;</font></td>");
            sWrite.WriteLine("<td align='left' width='160px' ><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;</font></td>");
            sWrite.WriteLine(" </tr>");
            if (dt_vital.Rows.Count > 0)
            {
                sWrite.WriteLine(" <tr>");
                sWrite.WriteLine("<td align='left'  width='20px' height='30' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Plus( Heart Beats Per Minute )  </font></td>");
                sWrite.WriteLine("<td align='left'  width='160px' height='30' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + dt_vital.Rows[0]["pulse"].ToString() + " </font></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine(" <tr>");
                sWrite.WriteLine("<td align='left' width='20px' height='30' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Temperature (C)      </font></td>");
                sWrite.WriteLine("<td align='left'width='160px' height='30'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + dt_vital.Rows[0]["temp"].ToString() + " </font></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine(" <tr>");
                sWrite.WriteLine(" <td align='left' width='20px' height='30'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Blood Pressure(mm Hg)    </font></td>");
                sWrite.WriteLine("<td align='left'width='160px' height='30'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + dt_vital.Rows[0]["bp_syst"].ToString() + " /" + dt_vital.Rows[0]["bp_dia"].ToString() + " </font></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine(" <tr>");
                sWrite.WriteLine("<td align='left'width='20px' height='30' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Height(Cm)  </font></td>");
                sWrite.WriteLine("<td align='left' width='160px' height='30'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + dt_vital.Rows[0]["Height"].ToString() + " </font></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine(" <tr>");
                sWrite.WriteLine("<td align='left'width='20px' height='30' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Weight(Kg)</font></td>");
                sWrite.WriteLine("<td align='left' width='160px' height='30'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + dt_vital.Rows[0]["weight"].ToString() + " </font></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine(" <tr>");
                sWrite.WriteLine("<td align='left'width='20px' height='30' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Resp.Rate((Breaths/min) )   </font></td>");
                sWrite.WriteLine("<td align='left' width='160px' height='30' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> " + dt_vital.Rows[0]["resp"].ToString() + " </font></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine(" <tr>");
                sWrite.WriteLine("<td align='left'width='20px' height='30' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>SPO2 (%)</font></td>");
                sWrite.WriteLine("<td align='left'width='160px' height='30' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + dt_vital.Rows[0]["spo"].ToString() + " </font></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine(" <tr>");
                sWrite.WriteLine("<td align='left'width='20px' height='30' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>BMI</font></td>");
                sWrite.WriteLine(" </tr>");
            }
            sWrite.WriteLine("<tr><td colspan=2><hr></td></tr>");
            sWrite.WriteLine("</table>");
            sWrite.WriteLine("<script>window.print();</script>");
                sWrite.WriteLine("</body>");
                sWrite.WriteLine("</html>");
                sWrite.Close();
                System.Diagnostics.Process.Start(Apppath + "\\Treatment_print.html");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
