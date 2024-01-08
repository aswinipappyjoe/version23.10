using PappyjoeMVC.Controller;
using PappyjoeMVC.Model;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PappyjoeMVC.View
{
    public partial class Show_Appointment : Form
    {
        bool dltflag = false;
        bool Editflag = false;
        public static string apntid = "";
        public string patient_id = "", doctor_id = "", appointment_id = "";
        Connection db = new Connection();
        Show_Appointment_controller ctrlr = new Show_Appointment_controller();
        Vital_Signs_controller vital_cntrl = new Vital_Signs_controller();
        Clinical_Findings_controller clinical_cntrl = new Clinical_Findings_controller();
        Treatment_controller treatmnt_cntrl = new Treatment_controller();
        Finished_Procedre_controller fnsdtrt_cntrl = new Finished_Procedre_controller();
        Prescription_Show_controller prescr_cntrl = new Prescription_Show_controller();
        LabWorks_controller lab_cntrl = new LabWorks_controller();
        Attachments_controller attach_cntrl = new Attachments_controller();
        Invoice_controller invo_cntrl = new Invoice_controller();
        Receipt_controller recpt_cntrl = new Receipt_controller();
        public Show_Appointment()
        {
            InitializeComponent();
        }
        public void Get_Patient_Details(DataTable dt)
        {
            try
            {
                if (dt.Rows[0]["pt_name"].ToString() != "")
                {
                    linkLabel_Name.Text = dt.Rows[0]["pt_name"].ToString();
                }
                if (dt.Rows[0]["pt_id"].ToString() != "")
                {
                    linkLabel_id.Text = dt.Rows[0]["pt_id"].ToString();
                }
            }
            catch (Exception ex)
            { MessageBox.Show("Error!..", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public void show(DataTable dt)
        {
            try
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string reason = "";
                    if (dt.Rows[j]["status"].ToString() == "CANCELLED")
                    {
                        reason ="Reason = "+ "(" + dt.Rows[j]["cancelation_reason"].ToString() + ")";
                    }
                    else
                        reason = "";
                    dataGridView2.Rows.Add(dt.Rows[j]["id"].ToString(), dt.Rows[j]["a_id"].ToString(), "", "", dt.Rows[j]["pt_name"].ToString() + "\r\n" + "Patient Id  : " + dt.Rows[j]["pt_id"].ToString() + "\r\n" + "Mobile No:  " + dt.Rows[j]["primary_mobile_number"].ToString(), "DOCTOR" + "\r\n" + dt.Rows[j]["doctor_name"].ToString(), "APPOINTMENT " + "\r\n............................\r\n" + dt.Rows[j]["start_datetime"].ToString(),
                        "STATUS " + "\r\n....................\r\n" + dt.Rows[j]["status"].ToString()+ "\r\n"+ reason, "SCHEDULE " + "\r\n....................\r\n" + dt.Rows[j]["schedule"].ToString(), "WAITING  " + "\r\n....................\r\n" + dt.Rows[j]["waiting"].ToString(),
                        "ENGAGED " + "\r\n....................\r\n" + dt.Rows[j]["engaged"].ToString(), "CHECK OUT " + "\r\n....................\r\n" + dt.Rows[j]["checkout"].ToString(), "");
                    dataGridView2.Rows[j].Height = 70;
                    dataGridView2.Rows[j].Cells[2].Style.ForeColor = Color.FromArgb(34, 139, 34);
                    dataGridView2.Rows[j].Cells[2].Value = "Edit";
                    dataGridView2.Rows[j].Cells[3].Style.ForeColor = Color.FromArgb(255, 69, 0);
                    dataGridView2.Rows[j].Cells[3].Value = "Delete";
                }
            }
            catch (Exception ex)
            { MessageBox.Show("Error!..", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void Show_Appointment_Load(object sender, EventArgs e)
        {
            try
            {
                        this.Size = Screen.PrimaryScreen.WorkingArea.Size;
                        DataTable ptntdtls = this.ctrlr.Get_Patient_Details(patient_id);
                        Get_Patient_Details(ptntdtls);
                        string docname = this.ctrlr.Get_DoctorName(doctor_id);
                        panelmain.Show();
                        label41.Text = "APPOINTMENTS";
                        dataGridView2.Width = 1040;
                        dataGridView2.Height = 510;
                        dataGridView2.Visible = true;
                        dataGridView2.RowCount = 0;
                        dataGridView2.Height = 404;
                        dataGridView2.ColumnCount = 12;
                        dataGridView2.ColumnHeadersVisible = false;
                        dataGridView2.RowHeadersVisible = false;
                        dataGridView2.Columns[0].Name = "id";
                        dataGridView2.Columns[0].Width = 0;
                        dataGridView2.Columns[1].Name = "id";
                        dataGridView2.Columns[1].Width = 0;
                        dataGridView2.Columns[2].Name = "edit";
                        dataGridView2.Columns[2].Width = 40;
                        dataGridView2.Columns[3].Name = "del";
                        dataGridView2.Columns[3].Width = 40;
                        dataGridView2.Columns[4].Name = "Name";
                        dataGridView2.Columns[4].Width = 200;
                        dataGridView2.Columns[5].Name = "payment";
                        dataGridView2.Columns[5].Width = 150;
                        dataGridView2.Columns[6].Name = "sum";
                        dataGridView2.Columns[6].Width = 150;
                        dataGridView2.Columns[7].Name = "sum";
                        dataGridView2.Columns[7].Width = 130;
                        dataGridView2.Columns[8].Name = "sum";
                        dataGridView2.Columns[8].Width = 75;
                        dataGridView2.Columns[9].Name = "sum";
                        dataGridView2.Columns[9].Width = 75;
                        dataGridView2.Columns[10].Name = "sum";
                        dataGridView2.Columns[10].Width = 75;
                        dataGridView2.Columns[11].Name = "sum";
                        dataGridView2.Columns[11].Width = 75;
                        dataGridView2.Columns[0].Visible = false;
                        dataGridView2.Columns[1].Visible = false;
                        DataTable dt = this.ctrlr.show(patient_id);
                        show(dt);
                        if (dataGridView2.Rows.Count <= 0)
                        {
                            Lab_Msg.Show();
                            Lab_Msg.Location = new System.Drawing.Point(146, 233);
                        }
                        else
                        {
                            Lab_Msg.Hide();
                            Lab_Msg.Location = new System.Drawing.Point(146, 233);
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
            catch (Exception ex)
            { MessageBox.Show("Error!..", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
        private void labeltreatment_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id = privi_mdl.treatment_show(doctor_id);
                if (int.Parse(id) > 0)
                {
                    if (PappyjoeMVC.Model.Connection.MyGlobals.project_type == "Dental")
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
                if (PappyjoeMVC.Model.Connection.MyGlobals.project_type == "Dental")
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

        private void linkLabel_Name_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form2 = new PappyjoeMVC.View.Patient_Profile_Details();
            form2.doctor_id = doctor_id;
            form2.patient_id = patient_id;
            openform(form2);
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

        public void appointment_count()
        {
            DataTable dt = this.ctrlr.show(patient_id);
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
        GlobalVariables gv = new GlobalVariables();
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
        public int rowcount = 0;
        private void label4_Click(object sender, EventArgs e)
        {
            rowcount = dataGridView2.Rows.Count;
            int count = rowcount + 20;
            DataTable dt = this.ctrlr.show_count(patient_id, count);
            show(dt);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void linkLabel_id_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form2 = new PappyjoeMVC.View.Patient_Profile_Details();
            form2.doctor_id = doctor_id;
            form2.patient_id = patient_id;
            openform(form2);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count > 0 && e.ColumnIndex == 3)
                {
                    if (doctor_id != "1")
                    {
                        dltflag = false;
                    }
                    else
                    {
                        dltflag = true;
                    }
                    if (dltflag == false)
                    {
                        string doctr_id = this.ctrlr.privilege_D(doctor_id);
                        if (int.Parse(doctr_id) > 0)
                        {
                            dltflag = true;
                        }
                        else
                        {
                            dltflag = false;
                        }
                    }
                    if (dltflag == true)
                    {
                        DialogResult res = MessageBox.Show("Are you sure you want to delete..?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (res == DialogResult.Yes)
                        {
                            string app_id = "0";
                            app_id = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                            this.ctrlr.delete(app_id);
                            string dt1 = DateTime.Now.Date.ToString("yyyy-MM-dd");
                            DateTime Timeonly = DateTime.Now;
                            this.ctrlr.save_log(doctor_id, "Appointment", "logged user deletes appointment", dt1, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Delete", app_id);
                            dataGridView2.RowCount = 0;
                            dataGridView2.Height = 404;
                            dataGridView2.ColumnCount = 12;
                            dataGridView2.ColumnHeadersVisible = false;
                            dataGridView2.RowHeadersVisible = false;
                            dataGridView2.Columns[0].Name = "id";
                            dataGridView2.Columns[0].Width = 0;
                            dataGridView2.Columns[1].Name = "id";
                            dataGridView2.Columns[1].Width = 0;
                            dataGridView2.Columns[2].Name = "edit";
                            dataGridView2.Columns[2].Width = 40;
                            dataGridView2.Columns[3].Name = "del";
                            dataGridView2.Columns[3].Width = 40;
                            dataGridView2.Columns[4].Name = "Name";
                            dataGridView2.Columns[4].Width = 200;
                            dataGridView2.Columns[5].Name = "payment";
                            dataGridView2.Columns[5].Width = 200;
                            dataGridView2.Columns[6].Name = "sum";
                            dataGridView2.Columns[6].Width = 150;
                            dataGridView2.Columns[7].Name = "sum";
                            dataGridView2.Columns[7].Width = 75;
                            dataGridView2.Columns[8].Name = "sum";
                            dataGridView2.Columns[8].Width = 75;
                            dataGridView2.Columns[9].Name = "sum";
                            dataGridView2.Columns[9].Width = 75;
                            dataGridView2.Columns[10].Name = "sum";
                            dataGridView2.Columns[10].Width = 75;
                            dataGridView2.Columns[11].Name = "sum";
                            dataGridView2.Columns[11].Width = 75;
                            dataGridView2.Columns[0].Visible = false;
                            dataGridView2.Columns[1].Visible = false;
                            DataTable dt = this.ctrlr.show(patient_id);
                            show(dt);
                        }
                    }
                    else
                    {
                        MessageBox.Show("There is No Privilege to Delete Appointment", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else if (dataGridView2.Rows.Count > 0 && e.ColumnIndex == 2)
                {
                    if (doctor_id != "1")
                    {
                        Editflag = false;
                    }
                    else
                    {
                        Editflag = true;
                    }
                    if (Editflag == false)
                    {
                        string id;
                        id = this.ctrlr.privilege_E(doctor_id);
                        if (int.Parse(id) > 0)
                        {
                            Editflag = true;
                        }
                        else
                        {
                            Editflag = false;
                        }
                    }
                    if (Editflag == true)
                    {
                        var form2 = new PappyjoeMVC.View.Add_Appointment();
                        form2.patient_id = patient_id;
                        form2.doctor_id = doctor_id;
                        form2.appointment_id = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                        openform(form2);
                    }
                    else
                    {
                        MessageBox.Show("There is No Privilege to Edit Appointment", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show("Error!..", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
