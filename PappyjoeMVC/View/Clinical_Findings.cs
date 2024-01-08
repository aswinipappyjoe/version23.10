using PappyjoeMVC.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Net.Mail;
using System.Windows.Forms;
using PappyjoeMVC.Model;

namespace PappyjoeMVC.View
{
    public partial class Clinical_Findings : Form
    {
        Clinical_Findings_controller cntrl=new Clinical_Findings_controller();
        Show_Appointment_controller Apptmt_cntrl = new Show_Appointment_controller();
        Vital_Signs_controller vital_cntrl = new Vital_Signs_controller();
        Treatment_controller treatmnt_cntrl = new Treatment_controller();
        Finished_Procedre_controller fnsdtrt_cntrl = new Finished_Procedre_controller();
        Prescription_Show_controller prescr_cntrl = new Prescription_Show_controller();
        LabWorks_controller lab_cntrl = new LabWorks_controller();
        Attachments_controller attach_cntrl = new Attachments_controller();
        Invoice_controller invo_cntrl = new Invoice_controller();
        Receipt_controller recpt_cntrl = new Receipt_controller();
        Nurses_Notes_controller nurse_cntrl = new Nurses_Notes_controller();
        Connection db = new Connection(); user_privillage_model privi_mdl = new user_privillage_model();
        GlobalVariables gv = new GlobalVariables();
        public string doctor_id = "";
        public string patient_id = "";
        public string staff_id = "";
        public string clinic_id = "0";

        public Clinical_Findings()
        {
            InitializeComponent();
        }
        private void Clinical_Findings_Load(object sender, EventArgs e)
        {
            try
            {

                System.Data.DataTable clinicname = this.cntrl.Get_CompanyNAme();
                string docnam = this.cntrl.Get_DoctorName(doctor_id);
                dataGridView1.Rows.Clear();
                DataTable rs_patients = this.cntrl.Get_Patient_Details(patient_id);
                if (rs_patients.Rows[0]["pt_name"].ToString() != "")
                {
                    linkLabel_Name.Text = rs_patients.Rows[0]["pt_name"].ToString();
                }
                if (rs_patients.Rows[0]["pt_id"].ToString() != "")
                {
                    linkLabel_id.Text = rs_patients.Rows[0]["pt_id"].ToString();
                }
                fillgrid();
                DataTable dt8 = this.cntrl.get_medicalId(patient_id);
                appointment_count();
                vitals_count();
                clinical_count();
                treatment_count();
                fnsdtrt_count();
                prescr_count();
                lab_count();
                attach_count();
                invoice_count();
                reciept_count(); nurse_count();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void fillgrid()
        {
            dataGridView1.ColumnCount = 5;
            dataGridView1.RowCount = 0;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            string heading = "";
            System.Data.DataTable dt_cf_main = new DataTable();
                dt_cf_main = this.cntrl.dt_cf_main(patient_id);
            //if(dt_cf_main.Rows.Count>25)
            //{
            //    label1.Visible = true;
            //}
            //else
            //{
            //    label1.Visible = false;
            //}
            int i = 0;
            for (int j = 0; j < dt_cf_main.Rows.Count; j++)
            {
                dataGridView1.Rows.Add(dt_cf_main.Rows[j]["id"].ToString(), String.Format("{0:dddd,d MMMM , yyyy}", Convert.ToDateTime(dt_cf_main.Rows[j]["date"].ToString())), "", "", "");
                dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.Bill;
                dataGridView1.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.DarkGreen;
                dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.LightGray;
                dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.LightGray;
                dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.LightGray;
                dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.LightGray;
                System.Data.DataTable dt_cf_Complaints = this.cntrl.dt_cf_Complaints(dt_cf_main.Rows[j]["id"].ToString());
                if (dt_cf_Complaints.Rows.Count > 0)
                {
                    int partLength = 90;
                    string sentence = dt_cf_Complaints.Rows[0]["complaint_id"].ToString();
                    string[] words = sentence.Split(' ');
                    var parts = new Dictionary<int, string>();
                    string part = string.Empty;
                    int partCounter = 0;
                    foreach (var word in words)
                    {
                        if (part.Length + word.Length < partLength)
                        {
                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                        }
                        else
                        {
                            parts.Add(partCounter, part);
                            part = word;
                            partCounter++;
                        }
                    }
                    parts.Add(partCounter, part);
                    heading = "Complaints";
                    foreach (var item in parts)
                    {
                        i = i + 1;
                        dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                        heading = "";
                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                        dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    }
                    for (int k = 1; k < dt_cf_Complaints.Rows.Count; k++)
                    {
                        partLength = 90;
                        sentence = dt_cf_Complaints.Rows[k]["complaint_id"].ToString();
                        words = sentence.Split(' ');
                        parts = new Dictionary<int, string>();
                        part = string.Empty;
                        partCounter = 0;
                        foreach (var word in words)
                        {
                            if (part.Length + word.Length < partLength)
                            {
                                part += string.IsNullOrEmpty(part) ? word : " " + word;
                            }
                            else
                            {
                                parts.Add(partCounter, part);
                                part = word;
                                partCounter++;
                            }
                        }
                        parts.Add(partCounter, part);
                        foreach (var item in parts)
                        {
                            i = i + 1;
                            dataGridView1.Rows.Add("0", "", "", item.Value, "");
                            dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                            dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                        }
                    }
                }
                System.Data.DataTable dt_cf_observe = this.cntrl.dt_cf_observe(dt_cf_main.Rows[j]["id"].ToString());
                if (dt_cf_observe.Rows.Count > 0)
                {
                    i = i + 1;
                    dataGridView1.Rows.Add("0", "", "", "", "");
                    dataGridView1.Rows[i].Height = 1;
                    dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Gainsboro;
                    dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    int partLength = 90;
                    string sentence = dt_cf_observe.Rows[0]["observation_id"].ToString();
                    string[] words = sentence.Split(' ');
                    var parts = new Dictionary<int, string>();
                    string part = string.Empty;
                    int partCounter = 0;
                    foreach (var word in words)
                    {
                        if (part.Length + word.Length < partLength)
                        {
                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                        }
                        else
                        {
                            parts.Add(partCounter, part);
                            part = word;
                            partCounter++;
                        }
                    }
                    parts.Add(partCounter, part);
                    heading = "Observation";
                    foreach (var item in parts)
                    {
                        i = i + 1;
                        dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                        heading = "";
                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                        dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    }
                    for (int k = 1; k < dt_cf_observe.Rows.Count; k++)
                    {
                        partLength = 90;
                        sentence = dt_cf_observe.Rows[k]["observation_id"].ToString();
                        words = sentence.Split(' ');
                        parts = new Dictionary<int, string>();
                        part = string.Empty;
                        partCounter = 0;
                        foreach (var word in words)
                        {
                            if (part.Length + word.Length < partLength)
                            {
                                part += string.IsNullOrEmpty(part) ? word : " " + word;
                            }
                            else
                            {
                                parts.Add(partCounter, part);
                                part = word;
                                partCounter++;
                            }
                        }
                        parts.Add(partCounter, part);
                        foreach (var item in parts)
                        {
                            i = i + 1;
                            dataGridView1.Rows.Add("0", "", "", item.Value, "");
                            dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                            dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                        }
                    }
                }
                System.Data.DataTable dt_cf_investigation = this.cntrl.dt_cf_investigation(dt_cf_main.Rows[j]["id"].ToString());
                if (dt_cf_investigation.Rows.Count > 0)
                {
                    i = i + 1;
                    dataGridView1.Rows.Add("0", "", "", "", "");
                    dataGridView1.Rows[i].Height = 1;
                    dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Gainsboro;
                    dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    int partLength = 90;
                    string sentence = dt_cf_investigation.Rows[0]["investigation_id"].ToString();
                    string[] words = sentence.Split(' ');
                    var parts = new Dictionary<int, string>();
                    string part = string.Empty;
                    int partCounter = 0;
                    foreach (var word in words)
                    {
                        if (part.Length + word.Length < partLength)
                        {
                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                        }
                        else
                        {
                            parts.Add(partCounter, part);
                            part = word;
                            partCounter++;
                        }
                    }
                    parts.Add(partCounter, part);
                    heading = "Investigation";
                    foreach (var item in parts)
                    {
                        i = i + 1;
                        dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                        heading = "";
                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                        dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    }
                    for (int k = 1; k < dt_cf_investigation.Rows.Count; k++)
                    {
                        partLength = 90;
                        sentence = dt_cf_investigation.Rows[k]["investigation_id"].ToString();
                        words = sentence.Split(' ');
                        parts = new Dictionary<int, string>();
                        part = string.Empty;
                        partCounter = 0;
                        foreach (var word in words)
                        {
                            if (part.Length + word.Length < partLength)
                            {
                                part += string.IsNullOrEmpty(part) ? word : " " + word;
                            }
                            else
                            {
                                parts.Add(partCounter, part);
                                part = word;
                                partCounter++;
                            }
                        }
                        parts.Add(partCounter, part);
                        foreach (var item in parts)
                        {
                            i = i + 1;
                            dataGridView1.Rows.Add("0", "", "", item.Value, "");
                            dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                            dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                        }
                    }
                }
                System.Data.DataTable dt_cf_diagnosis = this.cntrl.dt_cf_diagnosis(dt_cf_main.Rows[j]["id"].ToString());
                if (dt_cf_diagnosis.Rows.Count > 0)
                {
                    i = i + 1;
                    dataGridView1.Rows.Add("0", "", "", "", "");
                    dataGridView1.Rows[i].Height = 1;
                    dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Gainsboro;
                    dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    int partLength = 90;
                    string sentence = dt_cf_diagnosis.Rows[0]["diagnosis_id"].ToString();
                    string[] words = sentence.Split(' ');
                    var parts = new Dictionary<int, string>();
                    string part = string.Empty;
                    int partCounter = 0;
                    foreach (var word in words)
                    {
                        if (part.Length + word.Length < partLength)
                        {
                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                        }
                        else
                        {
                            parts.Add(partCounter, part);
                            part = word;
                            partCounter++;
                        }
                    }
                    parts.Add(partCounter, part);
                    heading = "Diagnosis";
                    foreach (var item in parts)
                    {
                        i = i + 1;
                        dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                        heading = "";
                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                        dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    }
                    for (int k = 1; k < dt_cf_diagnosis.Rows.Count; k++)
                    {
                        partLength = 90;
                        sentence = dt_cf_diagnosis.Rows[k]["diagnosis_id"].ToString();
                        words = sentence.Split(' ');
                        parts = new Dictionary<int, string>();
                        part = string.Empty;
                        partCounter = 0;
                        foreach (var word in words)
                        {
                            if (part.Length + word.Length < partLength)
                            {
                                part += string.IsNullOrEmpty(part) ? word : " " + word;
                            }
                            else
                            {
                                parts.Add(partCounter, part);
                                part = word;
                                partCounter++;
                            }
                        }
                        parts.Add(partCounter, part);
                        foreach (var item in parts)
                        {
                            i = i + 1;
                            dataGridView1.Rows.Add("0", "", "", item.Value, "");
                            dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                            dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                        }
                    }
                }
                System.Data.DataTable dt_cf_note = this.cntrl.dt_cf_note(dt_cf_main.Rows[j]["id"].ToString());
                if (dt_cf_note.Rows.Count > 0)
                {
                    i = i + 1;
                    dataGridView1.Rows.Add("0", "", "", "", "");
                    dataGridView1.Rows[i].Height = 1;
                    dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Gainsboro;
                    dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    int partLength = 90;
                    string sentence = dt_cf_note.Rows[0]["note_name"].ToString();
                    string[] words = sentence.Split(' ');
                    var parts = new Dictionary<int, string>();
                    string part = string.Empty;
                    int partCounter = 0;
                    foreach (var word in words)
                    {
                        if (part.Length + word.Length < partLength)
                        {
                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                        }
                        else
                        {
                            parts.Add(partCounter, part);
                            part = word;
                            partCounter++;
                        }
                    }
                    parts.Add(partCounter, part);
                    heading = "Note";
                    foreach (var item in parts)
                    {
                        i = i + 1;
                        dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                        heading = "";
                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                        dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    }
                    for (int k = 1; k < dt_cf_note.Rows.Count; k++)
                    {
                        partLength = 90;
                        sentence = dt_cf_note.Rows[k]["note_name"].ToString();
                        words = sentence.Split(' ');
                        parts = new Dictionary<int, string>();
                        part = string.Empty;
                        partCounter = 0;
                        foreach (var word in words)
                        {
                            if (part.Length + word.Length < partLength)
                            {
                                part += string.IsNullOrEmpty(part) ? word : " " + word;
                            }
                            else
                            {
                                parts.Add(partCounter, part);
                                part = word;
                                partCounter++;
                            }
                        }
                        parts.Add(partCounter, part);
                        foreach (var item in parts)
                        {
                            i = i + 1;
                            dataGridView1.Rows.Add("0", "", "", item.Value, "");
                            dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                            dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                        }
                    }
                }
                System.Data.DataTable dt_cf_allergy = this.cntrl.dt_cf_allergy(dt_cf_main.Rows[j]["id"].ToString());
                if (dt_cf_allergy.Rows.Count > 0)
                {
                    i = i + 1;
                    dataGridView1.Rows.Add("0", "", "", "", "");
                    dataGridView1.Rows[i].Height = 1;
                    dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Gainsboro;
                    dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    int partLength = 90;
                    string sentence = dt_cf_allergy.Rows[0]["allergy_name"].ToString();
                    string[] words = sentence.Split(' ');
                    var parts = new Dictionary<int, string>();
                    string part = string.Empty;
                    int partCounter = 0;
                    foreach (var word in words)
                    {
                        if (part.Length + word.Length < partLength)
                        {
                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                        }
                        else
                        {
                            parts.Add(partCounter, part);
                            part = word;
                            partCounter++;
                        }
                    }
                    parts.Add(partCounter, part);
                    heading = "Allergies";
                    foreach (var item in parts)
                    {
                        i = i + 1;
                        dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                        heading = "";
                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                        dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    }
                    for (int k = 1; k < dt_cf_allergy.Rows.Count; k++)
                    {
                        partLength = 90;
                        sentence = dt_cf_allergy.Rows[k]["allergy_name"].ToString();
                        words = sentence.Split(' ');
                        parts = new Dictionary<int, string>();
                        part = string.Empty;
                        partCounter = 0;
                        foreach (var word in words)
                        {
                            if (part.Length + word.Length < partLength)
                            {
                                part += string.IsNullOrEmpty(part) ? word : " " + word;
                            }
                            else
                            {
                                parts.Add(partCounter, part);
                                part = word;
                                partCounter++;
                            }
                        }
                        parts.Add(partCounter, part);
                        foreach (var item in parts)
                        {
                            i = i + 1;
                            dataGridView1.Rows.Add("0", "", "", item.Value, "");
                            dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                            dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                        }
                    }
                }
                System.Data.DataTable dt_cf_Nursenote = this.cntrl.dt_cf_Nursenote(dt_cf_main.Rows[j]["id"].ToString());
                if (dt_cf_Nursenote.Rows.Count > 0)
                {
                    i = i + 1;
                    dataGridView1.Rows.Add("0", "", "", "", "");
                    dataGridView1.Rows[i].Height = 1;
                    dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Gainsboro;
                    dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    int partLength = 90;
                    string sentence = dt_cf_Nursenote.Rows[0]["nurses_note"].ToString();
                    string[] words = sentence.Split(' ');
                    var parts = new Dictionary<int, string>();
                    string part = string.Empty;
                    int partCounter = 0;
                    foreach (var word in words)
                    {
                        if (part.Length + word.Length < partLength)
                        {
                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                        }
                        else
                        {
                            parts.Add(partCounter, part);
                            part = word;
                            partCounter++;
                        }
                    }
                    parts.Add(partCounter, part);
                    heading = "Nurses Notes";
                    foreach (var item in parts)
                    {
                        i = i + 1;
                        dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                        heading = "";
                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                        dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    }
                    for (int k = 1; k < dt_cf_Nursenote.Rows.Count; k++)
                    {
                        partLength = 90;
                        sentence = dt_cf_Nursenote.Rows[k]["nurses_note"].ToString();
                        words = sentence.Split(' ');
                        parts = new Dictionary<int, string>();
                        part = string.Empty;
                        partCounter = 0;
                        foreach (var word in words)
                        {
                            if (part.Length + word.Length < partLength)
                            {
                                part += string.IsNullOrEmpty(part) ? word : " " + word;
                            }
                            else
                            {
                                parts.Add(partCounter, part);
                                part = word;
                                partCounter++;
                            }
                        }
                        parts.Add(partCounter, part);
                        foreach (var item in parts)
                        {
                            i = i + 1;
                            dataGridView1.Rows.Add("0", "", "", item.Value, "");
                            dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                            dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                        }
                    }
                }
                System.Data.DataTable dt_cf_medical = this.cntrl.get_medicalId(dt_cf_main.Rows[j]["id"].ToString());
                if (dt_cf_medical.Rows.Count > 0)
                {
                    i = i + 1;
                    dataGridView1.Rows.Add("0", "", "", "", "");
                    dataGridView1.Rows[i].Height = 1;
                    dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Gainsboro;
                    dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    int partLength = 90;
                    string sentence = dt_cf_medical.Rows[0]["med_id"].ToString();
                    string[] words = sentence.Split(' ');
                    var parts = new Dictionary<int, string>();
                    string part = string.Empty;
                    int partCounter = 0;
                    foreach (var word in words)
                    {
                        if (part.Length + word.Length < partLength)
                        {
                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                        }
                        else
                        {
                            parts.Add(partCounter, part);
                            part = word;
                            partCounter++;
                        }
                    }
                    parts.Add(partCounter, part);
                    heading = "Medical History";
                    foreach (var item in parts)
                    {
                        i = i + 1;
                        dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                        heading = "";
                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                        dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    }
                    for (int k = 1; k < dt_cf_medical.Rows.Count; k++)
                    {
                        partLength = 90;
                        sentence = dt_cf_medical.Rows[k]["med_id"].ToString();
                        words = sentence.Split(' ');
                        parts = new Dictionary<int, string>();
                        part = string.Empty;
                        partCounter = 0;
                        foreach (var word in words)
                        {
                            if (part.Length + word.Length < partLength)
                            {
                                part += string.IsNullOrEmpty(part) ? word : " " + word;
                            }
                            else
                            {
                                parts.Add(partCounter, part);
                                part = word;
                                partCounter++;
                            }
                        }
                        parts.Add(partCounter, part);
                        foreach (var item in parts)
                        {
                            i = i + 1;
                            dataGridView1.Rows.Add("0", "", "", item.Value, "");
                            dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                            dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                        }
                    }
                }
                System.Data.DataTable dt_cf_medication = this.cntrl.dt_cf_meditation(dt_cf_main.Rows[j]["id"].ToString());
                if (dt_cf_medication.Rows.Count > 0)
                {
                    i = i + 1;
                    dataGridView1.Rows.Add("0", "", "", "", "");
                    dataGridView1.Rows[i].Height = 1;
                    dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Gainsboro;
                    dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    int partLength = 90;
                    string sentence = dt_cf_medication.Rows[0]["current_meditation"].ToString();
                    string[] words = sentence.Split(' ');
                    var parts = new Dictionary<int, string>();
                    string part = string.Empty;
                    int partCounter = 0;
                    foreach (var word in words)
                    {
                        if (part.Length + word.Length < partLength)
                        {
                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                        }
                        else
                        {
                            parts.Add(partCounter, part);
                            part = word;
                            partCounter++;
                        }
                    }
                    parts.Add(partCounter, part);
                    heading = "Current Medication";
                    foreach (var item in parts)
                    {
                        i = i + 1;
                        dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                        heading = "";
                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                        dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    }
                    for (int k = 1; k < dt_cf_medication.Rows.Count; k++)
                    {
                        partLength = 90;
                        sentence = dt_cf_medication.Rows[k]["current_meditation"].ToString();
                        words = sentence.Split(' ');
                        parts = new Dictionary<int, string>();
                        part = string.Empty;
                        partCounter = 0;
                        foreach (var word in words)
                        {
                            if (part.Length + word.Length < partLength)
                            {
                                part += string.IsNullOrEmpty(part) ? word : " " + word;
                            }
                            else
                            {
                                parts.Add(partCounter, part);
                                part = word;
                                partCounter++;
                            }
                        }
                        parts.Add(partCounter, part);
                        foreach (var item in parts)
                        {
                            i = i + 1;
                            dataGridView1.Rows.Add("0", "", "", item.Value, "");
                            dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                            dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                        }
                    }
                }
                System.Data.DataTable dt_cf_advice = this.cntrl.dt_cf_advice(dt_cf_main.Rows[j]["id"].ToString());
                if (dt_cf_advice.Rows.Count > 0)
                {
                    i = i + 1;
                    dataGridView1.Rows.Add("0", "", "", "", "");
                    dataGridView1.Rows[i].Height = 1;
                    dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Gainsboro;
                    dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    int partLength = 90;
                    string sentence = dt_cf_advice.Rows[0]["discharge_advice"].ToString();
                    string[] words = sentence.Split(' ');
                    var parts = new Dictionary<int, string>();
                    string part = string.Empty;
                    int partCounter = 0;
                    foreach (var word in words)
                    {
                        if (part.Length + word.Length < partLength)
                        {
                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                        }
                        else
                        {
                            parts.Add(partCounter, part);
                            part = word;
                            partCounter++;
                        }
                    }
                    parts.Add(partCounter, part);
                    heading = "Advice On Completion";
                    foreach (var item in parts)
                    {
                        i = i + 1;
                        dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                        heading = "";
                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                        dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    }
                    for (int k = 1; k < dt_cf_advice.Rows.Count; k++)
                    {
                        partLength = 90;
                        sentence = dt_cf_advice.Rows[k]["discharge_advice"].ToString();
                        words = sentence.Split(' ');
                        parts = new Dictionary<int, string>();
                        part = string.Empty;
                        partCounter = 0;
                        foreach (var word in words)
                        {
                            if (part.Length + word.Length < partLength)
                            {
                                part += string.IsNullOrEmpty(part) ? word : " " + word;
                            }
                            else
                            {
                                parts.Add(partCounter, part);
                                part = word;
                                partCounter++;
                            }
                        }
                        parts.Add(partCounter, part);
                        foreach (var item in parts)
                        {
                            i = i + 1;
                            dataGridView1.Rows.Add("0", "", "", item.Value, "");
                            dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                            dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                        }
                    }
                }
                i = i + 1;
                dataGridView1.Rows.Add("0", "Note by Dr." + dt_cf_main.Rows[j]["doctor_name"].ToString(), "", "", "");
                dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                dataGridView1.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                i = i + 1;
                dataGridView1.Rows.Add("0", "", "", "", "");
                dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                i = i + 1;
            }
            if (dataGridView1.Rows.Count <= 0)
            {
                int x = (panel3.Size.Width - Lbl_NoRecordFound_Alert.Size.Width) / 2;
                Lbl_NoRecordFound_Alert.Location = new Point(x, Lbl_NoRecordFound_Alert.Location.Y);
                Lbl_NoRecordFound_Alert.Show();
            }
            else
            {
                Lbl_NoRecordFound_Alert.Hide();
            }
            BtnAdd.Show();
            dataGridView1.Show();
        }
        public void fillgrid_count(int count)
        {
            //dataGridView1.ColumnCount = 5;
            //dataGridView1.RowCount = 0;
            //dataGridView1.ColumnHeadersVisible = false;
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            string heading = "";
            System.Data.DataTable dt_cf_main = new DataTable();
            dt_cf_main = this.cntrl.dt_cf_main_count(patient_id, count);
            int i = 0; int row = dataGridView1.Rows.Count;
            foreach (DataRow dr in dt_cf_main.Rows)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[row].Cells[0].Value = dr["id"].ToString();
                dataGridView1.Rows[row].Cells[1].Value = String.Format("{0:dddd,d MMMM , yyyy}", Convert.ToDateTime(dr["date"].ToString()));
                dataGridView1.Rows[row].Cells[2].Value = "";
                dataGridView1.Rows[row].Cells[3].Value = "";
                dataGridView1.Rows[row].Cells[4].Value = "";
                dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.Bill;
                dataGridView1.Rows[row].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                dataGridView1.Rows[row].Cells[1].Style.ForeColor = Color.DarkGreen;
                dataGridView1.Rows[row].Cells[1].Style.BackColor = Color.LightGray;
                dataGridView1.Rows[row].Cells[2].Style.BackColor = Color.LightGray;
                dataGridView1.Rows[row].Cells[3].Style.BackColor = Color.LightGray;
                dataGridView1.Rows[row].Cells[4].Style.BackColor = Color.LightGray;
                System.Data.DataTable dt_cf_Complaints = this.cntrl.dt_cf_Complaints(dr["id"].ToString());
                if (dt_cf_Complaints.Rows.Count > 0)
                {
                    int partLength = 90;
                    string sentence = dt_cf_Complaints.Rows[0]["complaint_id"].ToString();
                    string[] words = sentence.Split(' ');
                    var parts = new Dictionary<int, string>();
                    string part = string.Empty;
                    int partCounter = 0;
                    foreach (var word in words)
                    {
                        if (part.Length + word.Length < partLength)
                        {
                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                        }
                        else
                        {
                            parts.Add(partCounter, part);
                            part = word;
                            partCounter++;
                        }
                    }
                    parts.Add(partCounter, part);
                    heading = "Complaints";
                    foreach (var item in parts)
                    {
                        row = row + 1;
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[row].Cells[0].Value ="0";
                        dataGridView1.Rows[row].Cells[1].Value = heading;
                        dataGridView1.Rows[row].Cells[2].Value = "";
                        dataGridView1.Rows[row].Cells[3].Value = item.Value;
                        dataGridView1.Rows[row].Cells[4].Value = "";
                        heading = "";
                        dataGridView1.Rows[row].Cells[2].Style.BackColor = Color.Gainsboro;
                        dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    }
                    for (int k = 1; k < dt_cf_Complaints.Rows.Count; k++)
                    {
                        partLength = 90;
                        sentence = dt_cf_Complaints.Rows[k]["complaint_id"].ToString();
                        words = sentence.Split(' ');
                        parts = new Dictionary<int, string>();
                        part = string.Empty;
                        partCounter = 0;
                        foreach (var word in words)
                        {
                            if (part.Length + word.Length < partLength)
                            {
                                part += string.IsNullOrEmpty(part) ? word : " " + word;
                            }
                            else
                            {
                                parts.Add(partCounter, part);
                                part = word;
                                partCounter++;
                            }
                        }
                        parts.Add(partCounter, part);
                        foreach (var item in parts)
                        {
                            row = row  + 1;
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[row].Cells[0].Value = "0";
                            dataGridView1.Rows[row].Cells[1].Value = "";
                            dataGridView1.Rows[row].Cells[2].Value = "";
                            dataGridView1.Rows[row].Cells[3].Value = item.Value;
                            dataGridView1.Rows[row].Cells[4].Value = "";
                            dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                            dataGridView1.Rows[row].Cells[2].Style.BackColor = Color.Gainsboro;
                        }
                    }
                }
                System.Data.DataTable dt_cf_observe = this.cntrl.dt_cf_observe(dr["id"].ToString());
                if (dt_cf_observe.Rows.Count > 0)
                {
                    row = row + 1;
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[row].Cells[0].Value = "0";
                    dataGridView1.Rows[row].Cells[1].Value = "";
                    dataGridView1.Rows[row].Cells[2].Value = "";
                    dataGridView1.Rows[row].Cells[3].Value = "";
                    dataGridView1.Rows[row].Cells[4].Value = "";
                    dataGridView1.Rows[row].Height = 1;
                    dataGridView1.Rows[row].Cells[3].Style.BackColor = Color.Gainsboro;
                    dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    int partLength = 90;
                    string sentence = dt_cf_observe.Rows[0]["observation_id"].ToString();
                    string[] words = sentence.Split(' ');
                    var parts = new Dictionary<int, string>();
                    string part = string.Empty;
                    int partCounter = 0;
                    foreach (var word in words)
                    {
                        if (part.Length + word.Length < partLength)
                        {
                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                        }
                        else
                        {
                            parts.Add(partCounter, part);
                            part = word;
                            partCounter++;
                        }
                    }
                    parts.Add(partCounter, part);
                    heading = "Observation";
                    foreach (var item in parts)
                    {
                        row = row + 1;
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[row].Cells[0].Value = "0";
                        dataGridView1.Rows[row].Cells[1].Value = heading;
                        dataGridView1.Rows[row].Cells[2].Value = "";
                        dataGridView1.Rows[row].Cells[3].Value = item.Value;
                        dataGridView1.Rows[row].Cells[4].Value = "";
                        heading = "";
                        dataGridView1.Rows[row].Cells[2].Style.BackColor = Color.Gainsboro;
                        dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    }
                    for (int k = 1; k < dt_cf_observe.Rows.Count; k++)
                    {
                        partLength = 90;
                        sentence = dt_cf_observe.Rows[k]["observation_id"].ToString();
                        words = sentence.Split(' ');
                        parts = new Dictionary<int, string>();
                        part = string.Empty;
                        partCounter = 0;
                        foreach (var word in words)
                        {
                            if (part.Length + word.Length < partLength)
                            {
                                part += string.IsNullOrEmpty(part) ? word : " " + word;
                            }
                            else
                            {
                                parts.Add(partCounter, part);
                                part = word;
                                partCounter++;
                            }
                        }
                        parts.Add(partCounter, part);
                        foreach (var item in parts)
                        {
                            row = row + 1;
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[row].Cells[0].Value = "0";
                            dataGridView1.Rows[row].Cells[1].Value = "";
                            dataGridView1.Rows[row].Cells[2].Value = "";
                            dataGridView1.Rows[row].Cells[3].Value = item.Value;
                            dataGridView1.Rows[row].Cells[4].Value = "";
                            dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                            dataGridView1.Rows[row].Cells[2].Style.BackColor = Color.Gainsboro;
                        }
                    }
                }
                System.Data.DataTable dt_cf_investigation = this.cntrl.dt_cf_investigation(dr["id"].ToString());
                if (dt_cf_investigation.Rows.Count > 0)
                {
                    row = row + 1;
                    //dataGridView1.Rows.Add("0", "", "", "", "");
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[row].Cells[0].Value = "0";
                    dataGridView1.Rows[row].Cells[1].Value = "";
                    dataGridView1.Rows[row].Cells[2].Value = "";
                    dataGridView1.Rows[row].Cells[3].Value = "";
                    dataGridView1.Rows[row].Cells[4].Value = "";
                    dataGridView1.Rows[row].Height = 1;
                    dataGridView1.Rows[row].Cells[3].Style.BackColor = Color.Gainsboro;
                    dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    int partLength = 90;
                    string sentence = dt_cf_investigation.Rows[0]["investigation_id"].ToString();
                    string[] words = sentence.Split(' ');
                    var parts = new Dictionary<int, string>();
                    string part = string.Empty;
                    int partCounter = 0;
                    foreach (var word in words)
                    {
                        if (part.Length + word.Length < partLength)
                        {
                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                        }
                        else
                        {
                            parts.Add(partCounter, part);
                            part = word;
                            partCounter++;
                        }
                    }
                    parts.Add(partCounter, part);
                    heading = "Investigation";
                    foreach (var item in parts)
                    {
                        row = row + 1;
                        //dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[row].Cells[0].Value = "0";
                        dataGridView1.Rows[row].Cells[1].Value = heading;
                        dataGridView1.Rows[row].Cells[2].Value = "";
                        dataGridView1.Rows[row].Cells[3].Value = item.Value;
                        dataGridView1.Rows[row].Cells[4].Value = "";
                        heading = "";
                        dataGridView1.Rows[row].Cells[2].Style.BackColor = Color.Gainsboro;
                        dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    }
                    for (int k = 1; k < dt_cf_investigation.Rows.Count; k++)
                    {
                        partLength = 90;
                        sentence = dt_cf_investigation.Rows[k]["investigation_id"].ToString();
                        words = sentence.Split(' ');
                        parts = new Dictionary<int, string>();
                        part = string.Empty;
                        partCounter = 0;
                        foreach (var word in words)
                        {
                            if (part.Length + word.Length < partLength)
                            {
                                part += string.IsNullOrEmpty(part) ? word : " " + word;
                            }
                            else
                            {
                                parts.Add(partCounter, part);
                                part = word;
                                partCounter++;
                            }
                        }
                        parts.Add(partCounter, part);
                        foreach (var item in parts)
                        {
                            row = row + 1;
                            //dataGridView1.Rows.Add("0", "", "", item.Value, "");
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[row].Cells[0].Value = "0";
                            dataGridView1.Rows[row].Cells[1].Value = "";
                            dataGridView1.Rows[row].Cells[2].Value = "";
                            dataGridView1.Rows[row].Cells[3].Value = item.Value;
                            dataGridView1.Rows[row].Cells[4].Value = "";
                            dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                            dataGridView1.Rows[row].Cells[2].Style.BackColor = Color.Gainsboro;
                        }
                    }
                }
                System.Data.DataTable dt_cf_diagnosis = this.cntrl.dt_cf_diagnosis(dr["id"].ToString());
                if (dt_cf_diagnosis.Rows.Count > 0)
                {
                    row = row + 1;
                    //dataGridView1.Rows.Add("0", "", "", "", "");
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[row].Cells[0].Value = "0";
                    dataGridView1.Rows[row].Cells[1].Value = "";
                    dataGridView1.Rows[row].Cells[2].Value = "";
                    dataGridView1.Rows[row].Cells[3].Value = "";
                    dataGridView1.Rows[row].Cells[4].Value = "";
                    dataGridView1.Rows[row].Height = 1;
                    dataGridView1.Rows[row].Cells[3].Style.BackColor = Color.Gainsboro;
                    dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    int partLength = 90;
                    string sentence = dt_cf_diagnosis.Rows[0]["diagnosis_id"].ToString();
                    string[] words = sentence.Split(' ');
                    var parts = new Dictionary<int, string>();
                    string part = string.Empty;
                    int partCounter = 0;
                    foreach (var word in words)
                    {
                        if (part.Length + word.Length < partLength)
                        {
                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                        }
                        else
                        {
                            parts.Add(partCounter, part);
                            part = word;
                            partCounter++;
                        }
                    }
                    parts.Add(partCounter, part);
                    heading = "Diagnosis";
                    foreach (var item in parts)
                    {
                        row = row + 1;
                        //dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[row].Cells[0].Value = "0";
                        dataGridView1.Rows[row].Cells[1].Value = heading;
                        dataGridView1.Rows[row].Cells[2].Value = "";
                        dataGridView1.Rows[row].Cells[3].Value = item.Value;
                        dataGridView1.Rows[row].Cells[4].Value = "";
                        heading = "";
                        dataGridView1.Rows[row].Cells[2].Style.BackColor = Color.Gainsboro;
                        dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    }
                    for (int k = 1; k < dt_cf_diagnosis.Rows.Count; k++)
                    {
                        partLength = 90;
                        sentence = dt_cf_diagnosis.Rows[k]["diagnosis_id"].ToString();
                        words = sentence.Split(' ');
                        parts = new Dictionary<int, string>();
                        part = string.Empty;
                        partCounter = 0;
                        foreach (var word in words)
                        {
                            if (part.Length + word.Length < partLength)
                            {
                                part += string.IsNullOrEmpty(part) ? word : " " + word;
                            }
                            else
                            {
                                parts.Add(partCounter, part);
                                part = word;
                                partCounter++;
                            }
                        }
                        parts.Add(partCounter, part);
                        foreach (var item in parts)
                        {
                            row = row + 1;
                            //dataGridView1.Rows.Add("0", "", "", item.Value, "");
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[row].Cells[0].Value = "0";
                            dataGridView1.Rows[row].Cells[1].Value = "";
                            dataGridView1.Rows[row].Cells[2].Value = "";
                            dataGridView1.Rows[row].Cells[3].Value = item.Value;
                            dataGridView1.Rows[row].Cells[4].Value = "";
                            dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                            dataGridView1.Rows[row].Cells[2].Style.BackColor = Color.Gainsboro;
                        }
                    }
                }
                System.Data.DataTable dt_cf_note = this.cntrl.dt_cf_note(dr["id"].ToString());
                if (dt_cf_note.Rows.Count > 0)
                {
                    row = row + 1;
                    //dataGridView1.Rows.Add("0", "", "", "", "");
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[row].Cells[0].Value = "0";
                    dataGridView1.Rows[row].Cells[1].Value = "";
                    dataGridView1.Rows[row].Cells[2].Value = "";
                    dataGridView1.Rows[row].Cells[3].Value = "";
                    dataGridView1.Rows[row].Cells[4].Value = "";
                    dataGridView1.Rows[row].Height = 1;
                    dataGridView1.Rows[row].Cells[3].Style.BackColor = Color.Gainsboro;
                    dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    int partLength = 90;
                    string sentence = dt_cf_note.Rows[0]["note_name"].ToString();
                    string[] words = sentence.Split(' ');
                    var parts = new Dictionary<int, string>();
                    string part = string.Empty;
                    int partCounter = 0;
                    foreach (var word in words)
                    {
                        if (part.Length + word.Length < partLength)
                        {
                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                        }
                        else
                        {
                            parts.Add(partCounter, part);
                            part = word;
                            partCounter++;
                        }
                    }
                    parts.Add(partCounter, part);
                    heading = "Note";
                    foreach (var item in parts)
                    {
                        row = row + 1;
                        //dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[row].Cells[0].Value = "0";
                        dataGridView1.Rows[row].Cells[1].Value = heading;
                        dataGridView1.Rows[row].Cells[2].Value = "";
                        dataGridView1.Rows[row].Cells[3].Value = item.Value;
                        dataGridView1.Rows[row].Cells[4].Value = "";
                        heading = "";
                        dataGridView1.Rows[row].Cells[2].Style.BackColor = Color.Gainsboro;
                        dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    }
                    for (int k = 1; k < dt_cf_note.Rows.Count; k++)
                    {
                        partLength = 90;
                        sentence = dt_cf_note.Rows[k]["note_name"].ToString();
                        words = sentence.Split(' ');
                        parts = new Dictionary<int, string>();
                        part = string.Empty;
                        partCounter = 0;
                        foreach (var word in words)
                        {
                            if (part.Length + word.Length < partLength)
                            {
                                part += string.IsNullOrEmpty(part) ? word : " " + word;
                            }
                            else
                            {
                                parts.Add(partCounter, part);
                                part = word;
                                partCounter++;
                            }
                        }
                        parts.Add(partCounter, part);
                        foreach (var item in parts)
                        {
                            row = row + 1;
                            //dataGridView1.Rows.Add("0", "", "", item.Value, "");
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[row].Cells[0].Value = "0";
                            dataGridView1.Rows[row].Cells[1].Value = "";
                            dataGridView1.Rows[row].Cells[2].Value = "";
                            dataGridView1.Rows[row].Cells[3].Value = item.Value;
                            dataGridView1.Rows[row].Cells[4].Value = "";
                            dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                            dataGridView1.Rows[row].Cells[2].Style.BackColor = Color.Gainsboro;
                        }
                    }
                }
                System.Data.DataTable dt_cf_allergy = this.cntrl.dt_cf_allergy(dr["id"].ToString());
                if (dt_cf_allergy.Rows.Count > 0)
                {
                    row = row + 1;
                    //dataGridView1.Rows.Add("0", "", "", "", "");
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[row].Cells[0].Value = "0";
                    dataGridView1.Rows[row].Cells[1].Value = "";
                    dataGridView1.Rows[row].Cells[2].Value = "";
                    dataGridView1.Rows[row].Cells[3].Value = "";
                    dataGridView1.Rows[row].Cells[4].Value = "";
                    dataGridView1.Rows[row].Height = 1;
                    dataGridView1.Rows[row].Cells[3].Style.BackColor = Color.Gainsboro;
                    dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    int partLength = 90;
                    string sentence = dt_cf_allergy.Rows[0]["allergy_name"].ToString();
                    string[] words = sentence.Split(' ');
                    var parts = new Dictionary<int, string>();
                    string part = string.Empty;
                    int partCounter = 0;
                    foreach (var word in words)
                    {
                        if (part.Length + word.Length < partLength)
                        {
                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                        }
                        else
                        {
                            parts.Add(partCounter, part);
                            part = word;
                            partCounter++;
                        }
                    }
                    parts.Add(partCounter, part);
                    heading = "Allergies";
                    foreach (var item in parts)
                    {
                        row = row + 1;
                        //dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[row].Cells[0].Value = "0";
                        dataGridView1.Rows[row].Cells[1].Value = heading;
                        dataGridView1.Rows[row].Cells[2].Value = "";
                        dataGridView1.Rows[row].Cells[3].Value = item.Value;
                        dataGridView1.Rows[row].Cells[4].Value = "";
                        heading = "";
                        dataGridView1.Rows[row].Cells[2].Style.BackColor = Color.Gainsboro;
                        dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    }
                    for (int k = 1; k < dt_cf_allergy.Rows.Count; k++)
                    {
                        partLength = 90;
                        sentence = dt_cf_allergy.Rows[k]["allergy_name"].ToString();
                        words = sentence.Split(' ');
                        parts = new Dictionary<int, string>();
                        part = string.Empty;
                        partCounter = 0;
                        foreach (var word in words)
                        {
                            if (part.Length + word.Length < partLength)
                            {
                                part += string.IsNullOrEmpty(part) ? word : " " + word;
                            }
                            else
                            {
                                parts.Add(partCounter, part);
                                part = word;
                                partCounter++;
                            }
                        }
                        parts.Add(partCounter, part);
                        foreach (var item in parts)
                        {
                            row = row + 1;
                            //dataGridView1.Rows.Add("0", "", "", item.Value, "");
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[row].Cells[0].Value = "0";
                            dataGridView1.Rows[row].Cells[1].Value = "";
                            dataGridView1.Rows[row].Cells[2].Value = "";
                            dataGridView1.Rows[row].Cells[3].Value = item.Value;
                            dataGridView1.Rows[row].Cells[4].Value = "";
                            dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                            dataGridView1.Rows[row].Cells[2].Style.BackColor = Color.Gainsboro;
                        }
                    }
                }
                System.Data.DataTable dt_cf_Nursenote = this.cntrl.dt_cf_Nursenote(dr["id"].ToString());
                if (dt_cf_Nursenote.Rows.Count > 0)
                {
                    row = row + 1;
                    //dataGridView1.Rows.Add("0", "", "", "", "");
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[row].Cells[0].Value = "0";
                    dataGridView1.Rows[row].Cells[1].Value = "";
                    dataGridView1.Rows[row].Cells[2].Value = "";
                    dataGridView1.Rows[row].Cells[3].Value = "";
                    dataGridView1.Rows[row].Cells[4].Value = "";
                    dataGridView1.Rows[row].Height = 1;
                    dataGridView1.Rows[row].Cells[3].Style.BackColor = Color.Gainsboro;
                    dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    int partLength = 90;
                    string sentence = dt_cf_Nursenote.Rows[0]["nurses_note"].ToString();
                    string[] words = sentence.Split(' ');
                    var parts = new Dictionary<int, string>();
                    string part = string.Empty;
                    int partCounter = 0;
                    foreach (var word in words)
                    {
                        if (part.Length + word.Length < partLength)
                        {
                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                        }
                        else
                        {
                            parts.Add(partCounter, part);
                            part = word;
                            partCounter++;
                        }
                    }
                    parts.Add(partCounter, part);
                    heading = "Nurses Notes";
                    foreach (var item in parts)
                    {
                        row = row + 1;
                        //dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[row].Cells[0].Value = "0";
                        dataGridView1.Rows[row].Cells[1].Value = heading;
                        dataGridView1.Rows[row].Cells[2].Value = "";
                        dataGridView1.Rows[row].Cells[3].Value = item.Value;
                        dataGridView1.Rows[row].Cells[4].Value = "";
                        heading = "";
                        dataGridView1.Rows[row].Cells[2].Style.BackColor = Color.Gainsboro;
                        dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    }
                    for (int k = 1; k < dt_cf_Nursenote.Rows.Count; k++)
                    {
                        partLength = 90;
                        sentence = dt_cf_Nursenote.Rows[k]["nurses_note"].ToString();
                        words = sentence.Split(' ');
                        parts = new Dictionary<int, string>();
                        part = string.Empty;
                        partCounter = 0;
                        foreach (var word in words)
                        {
                            if (part.Length + word.Length < partLength)
                            {
                                part += string.IsNullOrEmpty(part) ? word : " " + word;
                            }
                            else
                            {
                                parts.Add(partCounter, part);
                                part = word;
                                partCounter++;
                            }
                        }
                        parts.Add(partCounter, part);
                        foreach (var item in parts)
                        {
                            row = row + 1;
                            //dataGridView1.Rows.Add("0", "", "", item.Value, "");
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[row].Cells[0].Value = "0";
                            dataGridView1.Rows[row].Cells[1].Value = "";
                            dataGridView1.Rows[row].Cells[2].Value = "";
                            dataGridView1.Rows[row].Cells[3].Value = item.Value;
                            dataGridView1.Rows[row].Cells[4].Value = "";
                            dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                            dataGridView1.Rows[row].Cells[2].Style.BackColor = Color.Gainsboro;
                        }
                    }
                }
                System.Data.DataTable dt_cf_medical = this.cntrl.get_medicalId(dr["id"].ToString());
                if (dt_cf_medical.Rows.Count > 0)
                {
                    row = row + 1;
                    //dataGridView1.Rows.Add("0", "", "", "", "");
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[row].Cells[0].Value = "0";
                    dataGridView1.Rows[row].Cells[1].Value = "";
                    dataGridView1.Rows[row].Cells[2].Value = "";
                    dataGridView1.Rows[row].Cells[3].Value = "";
                    dataGridView1.Rows[row].Cells[4].Value = "";
                    dataGridView1.Rows[row].Height = 1;
                    dataGridView1.Rows[row].Cells[3].Style.BackColor = Color.Gainsboro;
                    dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    int partLength = 90;
                    string sentence = dt_cf_medical.Rows[0]["med_id"].ToString();
                    string[] words = sentence.Split(' ');
                    var parts = new Dictionary<int, string>();
                    string part = string.Empty;
                    int partCounter = 0;
                    foreach (var word in words)
                    {
                        if (part.Length + word.Length < partLength)
                        {
                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                        }
                        else
                        {
                            parts.Add(partCounter, part);
                            part = word;
                            partCounter++;
                        }
                    }
                    parts.Add(partCounter, part);
                    heading = "Medical History";
                    foreach (var item in parts)
                    {
                        row = row + 1;
                        //dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[row].Cells[0].Value = "0";
                        dataGridView1.Rows[row].Cells[1].Value = heading;
                        dataGridView1.Rows[row].Cells[2].Value = "";
                        dataGridView1.Rows[row].Cells[3].Value = item.Value;
                        dataGridView1.Rows[row].Cells[4].Value = "";
                        heading = "";
                        dataGridView1.Rows[row].Cells[2].Style.BackColor = Color.Gainsboro;
                        dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    }
                    for (int k = 1; k < dt_cf_medical.Rows.Count; k++)
                    {
                        partLength = 90;
                        sentence = dt_cf_medical.Rows[k]["med_id"].ToString();
                        words = sentence.Split(' ');
                        parts = new Dictionary<int, string>();
                        part = string.Empty;
                        partCounter = 0;
                        foreach (var word in words)
                        {
                            if (part.Length + word.Length < partLength)
                            {
                                part += string.IsNullOrEmpty(part) ? word : " " + word;
                            }
                            else
                            {
                                parts.Add(partCounter, part);
                                part = word;
                                partCounter++;
                            }
                        }
                        parts.Add(partCounter, part);
                        foreach (var item in parts)
                        {
                            row = row + 1;
                            //dataGridView1.Rows.Add("0", "", "", item.Value, "");
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[row].Cells[0].Value = "0";
                            dataGridView1.Rows[row].Cells[1].Value = "";
                            dataGridView1.Rows[row].Cells[2].Value = "";
                            dataGridView1.Rows[row].Cells[3].Value = item.Value;
                            dataGridView1.Rows[row].Cells[4].Value = "";
                            dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                            dataGridView1.Rows[row].Cells[2].Style.BackColor = Color.Gainsboro;
                        }
                    }
                }
                System.Data.DataTable dt_cf_medication = this.cntrl.dt_cf_meditation(dr["id"].ToString());
                if (dt_cf_medication.Rows.Count > 0)
                {
                    row = row + 1;
                    //dataGridView1.Rows.Add("0", "", "", "", "");
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[row].Cells[0].Value = "0";
                    dataGridView1.Rows[row].Cells[1].Value = "";
                    dataGridView1.Rows[row].Cells[2].Value = "";
                    dataGridView1.Rows[row].Cells[3].Value = "";
                    dataGridView1.Rows[row].Cells[4].Value = "";
                    dataGridView1.Rows[row].Height = 1;
                    dataGridView1.Rows[row].Cells[3].Style.BackColor = Color.Gainsboro;
                    dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    int partLength = 90;
                    string sentence = dt_cf_medication.Rows[0]["current_meditation"].ToString();
                    string[] words = sentence.Split(' ');
                    var parts = new Dictionary<int, string>();
                    string part = string.Empty;
                    int partCounter = 0;
                    foreach (var word in words)
                    {
                        if (part.Length + word.Length < partLength)
                        {
                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                        }
                        else
                        {
                            parts.Add(partCounter, part);
                            part = word;
                            partCounter++;
                        }
                    }
                    parts.Add(partCounter, part);
                    heading = "Current Medication";
                    foreach (var item in parts)
                    {
                        row = row + 1;
                        //dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[row].Cells[0].Value = "0";
                        dataGridView1.Rows[row].Cells[1].Value = heading;
                        dataGridView1.Rows[row].Cells[2].Value = "";
                        dataGridView1.Rows[row].Cells[3].Value = item.Value;
                        dataGridView1.Rows[row].Cells[4].Value = "";
                        heading = "";
                        dataGridView1.Rows[row].Cells[2].Style.BackColor = Color.Gainsboro;
                        dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    }
                    for (int k = 1; k < dt_cf_medication.Rows.Count; k++)
                    {
                        partLength = 90;
                        sentence = dt_cf_medication.Rows[k]["current_meditation"].ToString();
                        words = sentence.Split(' ');
                        parts = new Dictionary<int, string>();
                        part = string.Empty;
                        partCounter = 0;
                        foreach (var word in words)
                        {
                            if (part.Length + word.Length < partLength)
                            {
                                part += string.IsNullOrEmpty(part) ? word : " " + word;
                            }
                            else
                            {
                                parts.Add(partCounter, part);
                                part = word;
                                partCounter++;
                            }
                        }
                        parts.Add(partCounter, part);
                        foreach (var item in parts)
                        {
                            row = row + 1;
                            //dataGridView1.Rows.Add("0", "", "", item.Value, "");
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[row].Cells[0].Value = "0";
                            dataGridView1.Rows[row].Cells[1].Value = "";
                            dataGridView1.Rows[row].Cells[2].Value = "";
                            dataGridView1.Rows[row].Cells[3].Value = item.Value;
                            dataGridView1.Rows[row].Cells[4].Value = "";
                            dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                            dataGridView1.Rows[row].Cells[2].Style.BackColor = Color.Gainsboro;
                        }
                    }
                }
                System.Data.DataTable dt_cf_advice = this.cntrl.dt_cf_advice(dr["id"].ToString());
                if (dt_cf_advice.Rows.Count > 0)
                {
                    row = row + 1;
                    //dataGridView1.Rows.Add("0", "", "", "", "");
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[row].Cells[0].Value = "0";
                    dataGridView1.Rows[row].Cells[1].Value = "";
                    dataGridView1.Rows[row].Cells[2].Value = "";
                    dataGridView1.Rows[row].Cells[3].Value = "";
                    dataGridView1.Rows[row].Cells[4].Value = "";
                    dataGridView1.Rows[row].Height = 1;
                    dataGridView1.Rows[row].Cells[3].Style.BackColor = Color.Gainsboro;
                    dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    int partLength = 90;
                    string sentence = dt_cf_advice.Rows[0]["discharge_advice"].ToString();
                    string[] words = sentence.Split(' ');
                    var parts = new Dictionary<int, string>();
                    string part = string.Empty;
                    int partCounter = 0;
                    foreach (var word in words)
                    {
                        if (part.Length + word.Length < partLength)
                        {
                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                        }
                        else
                        {
                            parts.Add(partCounter, part);
                            part = word;
                            partCounter++;
                        }
                    }
                    parts.Add(partCounter, part);
                    heading = "Advice On Completion";
                    foreach (var item in parts)
                    {
                        row = row + 1;
                        //dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[row].Cells[0].Value = "0";
                        dataGridView1.Rows[row].Cells[1].Value = heading;
                        dataGridView1.Rows[row].Cells[2].Value = "";
                        dataGridView1.Rows[row].Cells[3].Value = item.Value;
                        dataGridView1.Rows[row].Cells[4].Value = "";
                        heading = "";
                        dataGridView1.Rows[row].Cells[2].Style.BackColor = Color.Gainsboro;
                        dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                    }
                    for (int k = 1; k < dt_cf_advice.Rows.Count; k++)
                    {
                        partLength = 90;
                        sentence = dt_cf_advice.Rows[k]["discharge_advice"].ToString();
                        words = sentence.Split(' ');
                        parts = new Dictionary<int, string>();
                        part = string.Empty;
                        partCounter = 0;
                        foreach (var word in words)
                        {
                            if (part.Length + word.Length < partLength)
                            {
                                part += string.IsNullOrEmpty(part) ? word : " " + word;
                            }
                            else
                            {
                                parts.Add(partCounter, part);
                                part = word;
                                partCounter++;
                            }
                        }
                        parts.Add(partCounter, part);
                        foreach (var item in parts)
                        {
                            row = row + 1;
                            //dataGridView1.Rows.Add("0", "", "", item.Value, "");
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[row].Cells[0].Value = "0";
                            dataGridView1.Rows[row].Cells[1].Value = "";
                            dataGridView1.Rows[row].Cells[2].Value = "";
                            dataGridView1.Rows[row].Cells[3].Value = item.Value;
                            dataGridView1.Rows[row].Cells[4].Value = "";
                            dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                            dataGridView1.Rows[row].Cells[2].Style.BackColor = Color.Gainsboro;
                        }
                    }
                }
                row = row + 1;
                //dataGridView1.Rows.Add("0", "Note by Dr." + dr["doctor_name"].ToString(), "", "", "");
                dataGridView1.Rows.Add();
                dataGridView1.Rows[row].Cells[0].Value = "0";
                dataGridView1.Rows[row].Cells[1].Value = "Note by Dr." + dr["doctor_name"].ToString();
                dataGridView1.Rows[row].Cells[2].Value = "";
                dataGridView1.Rows[row].Cells[3].Value = "";
                dataGridView1.Rows[row].Cells[4].Value = "";
                dataGridView1.Rows[row].Cells[1].Style.ForeColor = Color.Red;
                dataGridView1.Rows[row].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                row = row + 1;
                //dataGridView1.Rows.Add("0", "", "", "", "");
                dataGridView1.Rows.Add();
                dataGridView1.Rows[row].Cells[0].Value = "0";
                dataGridView1.Rows[row].Cells[1].Value = "";
                dataGridView1.Rows[row].Cells[2].Value = "";
                dataGridView1.Rows[row].Cells[3].Value = "";
                dataGridView1.Rows[row].Cells[4].Value = "";
                dataGridView1.Rows[row].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                row = row + 1;
            }
            if (dataGridView1.Rows.Count <= 0)
            {
                int x = (panel3.Size.Width - Lbl_NoRecordFound_Alert.Size.Width) / 2;
                Lbl_NoRecordFound_Alert.Location = new Point(x, Lbl_NoRecordFound_Alert.Location.Y);
                Lbl_NoRecordFound_Alert.Show();
            }
            else
            {
                Lbl_NoRecordFound_Alert.Hide();
            }
            BtnAdd.Show();
            dataGridView1.Show();
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

        private void BtnAdd_Click(object sender, EventArgs e)
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

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                {
                    System.Data.DataTable dtp = this.cntrl.get_company_details();
                    System.Data.DataTable dt1 = this.cntrl.Get_Patient_Details(patient_id);
                    using (System.Drawing.Font printFont = new System.Drawing.Font("Segoe UI", 18.0f))
                    {
                        string clinicn = "";
                        string clinic = "";
                        clinicn = dtp.Rows[0][1].ToString();
                        clinic = clinicn.Replace("¤", "'");
                        e.Graphics.DrawString(clinic, printFont, Brushes.Gray, 30, 100);
                    }
                    using (System.Drawing.Font printFont = new System.Drawing.Font("Segoe UI", 13.0f))
                    {
                        e.Graphics.DrawString(dtp.Rows[0][9].ToString(), printFont, Brushes.Gray, 30, 130);
                    }
                    using (System.Drawing.Font printFont = new System.Drawing.Font("Segoe UI", 14.0f))
                    {
                        e.Graphics.DrawString("CLINICAL FINDINGS", printFont, Brushes.Gray, 320, 175);
                    }
                    Graphics g = e.Graphics;
                    Pen pen = new Pen(Color.Gray);
                    g.DrawLine(pen, new System.Drawing.Point(20, 200), new System.Drawing.Point(800, 200));
                    int yy = 205;
                    string sexage = ""; string age = ""; string days = "";
                    int Dexist = 0;
                    string address = "";
                    using (System.Drawing.Font printFont = new System.Drawing.Font("Segoe UI", 9.0f))
                    {
                        using (System.Drawing.Font printFont1 = new System.Drawing.Font("Segoe UI", 10.0f))
                        {
                            if (dt1.Rows[0]["gender"].ToString() != "")
                            {
                                sexage = dt1.Rows[0]["gender"].ToString();
                                Dexist = 1;
                            }
                            days = dt1.Rows[0]["days"].ToString();

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
                            e.Graphics.DrawString(dt1.Rows[0]["pt_name"].ToString() + " (" + sexage +age+ ")", printFont1, Brushes.Black, 20, yy);
                        }
                        yy = yy + 22;
                        e.Graphics.DrawString("Patient id:" + dt1.Rows[0]["pt_id"].ToString(), printFont, Brushes.Gray, 20, yy);
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
                        e.Graphics.DrawString(address, printFont, Brushes.Gray, 300, yy);
                        if (dt1.Rows[0]["aadhar_id"].ToString() != "")
                        {
                            yy = yy + 20;
                            e.Graphics.DrawString("Aadhaar ID:" + dt1.Rows[0]["aadhar_id"].ToString(), printFont, Brushes.Gray, 20, yy);
                        }
                        if (dt1.Rows[0]["primary_mobile_number"].ToString() != "")
                        {
                            yy = yy + 20;
                            e.Graphics.DrawString(dt1.Rows[0]["primary_mobile_number"].ToString(), printFont, Brushes.Gray, 20, yy);
                        }
                        if (dt1.Rows[0]["email_address"].ToString() != "")
                        {
                            yy = yy + 20;
                            e.Graphics.DrawString(dt1.Rows[0]["email_address"].ToString(), printFont, Brushes.Gray, 20, yy);
                        }
                        yy = yy + 20;
                        using (System.Drawing.Font printFontm = new System.Drawing.Font("Segoe UI", 8.0f))
                        {
                            DataTable dt_medical = this.cntrl.dt_medical(patient_id);
                            if (dt_medical.Rows.Count > 0)
                            {
                                int k = 0;
                                string medical_history = "";
                                while (k < dt_medical.Rows.Count)
                                {
                                    if (medical_history != "")
                                    {
                                        medical_history = medical_history + ",";
                                    }
                                    medical_history = medical_history + dt_medical.Rows[k]["med_id"].ToString();
                                    k++;
                                }
                                e.Graphics.DrawString("Medical History:" + medical_history, printFont, Brushes.Gray, 20, yy);
                                yy = yy + 20;
                            }
                        }
                        yy = yy + 20;
                        g.DrawLine(pen, new System.Drawing.Point(20, yy), new System.Drawing.Point(800, yy));
                        yy = yy + 30;
                        Dexist = 0;
                        System.Data.DataTable dt_cf = this.cntrl.dt_cf(clinic_id, patient_id);
                        if (dt_cf.Rows.Count > 0)
                        {
                            e.Graphics.DrawString("By:", printFont, Brushes.Gray, 20, yy);
                            using (System.Drawing.Font printFont1 = new System.Drawing.Font("Segoe UI", 10.0f))
                            {
                                e.Graphics.DrawString("Dr." + Convert.ToString(dt_cf.Rows[0]["doctor_name"].ToString()), printFont1, Brushes.Black, 40, yy);
                            }
                            e.Graphics.DrawString("Date: ", printFont, Brushes.Gray, 690, yy);
                            using (System.Drawing.Font printFont1 = new System.Drawing.Font("Segoe UI", 10.0f))
                            {
                                DateTime tdate = Convert.ToDateTime(dt_cf.Rows[0]["date"].ToString());
                                e.Graphics.DrawString(tdate.ToString("dd MMM yyyy"), printFont1, Brushes.Black, 720, yy);
                            }
                        }
                        Graphics g2 = e.Graphics;
                        Pen pen11 = new Pen(Color.Gray);
                        g2.DrawLine(pen11, new System.Drawing.Point(20, 200), new System.Drawing.Point(800, 200));
                        using (System.Drawing.Font printFont2 = new System.Drawing.Font("Segoe UI", 11.0f))
                        {
                            System.Data.DataTable dt_cf_Complaints = this.cntrl.dt_cf_Complaints_clinic_id(clinic_id);
                            if (dt_cf_Complaints.Rows.Count > 0)
                            {
                                yy = yy + 40;
                                Dexist = 1;
                                e.Graphics.DrawString("Complaints", printFont2, Brushes.Gray, 20, yy);
                                e.Graphics.DrawString("o", printFont2, Brushes.Gray, 200, yy);
                                int partLength = 80;
                                string sentence = dt_cf_Complaints.Rows[0]["complaint_id"].ToString();
                                string[] words = sentence.Split(' ');
                                var parts = new Dictionary<int, string>();
                                string part = string.Empty;
                                int partCounter = 0;
                                foreach (var word in words)
                                {
                                    if (part.Length + word.Length < partLength)
                                    {
                                        part += string.IsNullOrEmpty(part) ? word : " " + word;
                                    }
                                    else
                                    {
                                        parts.Add(partCounter, part);
                                        part = word;
                                        partCounter++;
                                    }
                                }
                                parts.Add(partCounter, part);
                                foreach (var item in parts)
                                {
                                    e.Graphics.DrawString(item.Value, printFont2, Brushes.Gray, 220, yy);
                                    yy = yy + 20;
                                }
                                for (int k = 1; k < dt_cf_Complaints.Rows.Count; k++)
                                {
                                    e.Graphics.DrawString("o", printFont2, Brushes.Gray, 200, yy);
                                    partLength = 80;
                                    sentence = dt_cf_Complaints.Rows[k]["complaint_id"].ToString();
                                    words = sentence.Split(' ');
                                    parts = new Dictionary<int, string>();
                                    part = string.Empty;
                                    partCounter = 0;
                                    foreach (var word in words)
                                    {
                                        if (part.Length + word.Length < partLength)
                                        {
                                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                                        }
                                        else
                                        {
                                            parts.Add(partCounter, part);
                                            part = word;
                                            partCounter++;
                                        }
                                    }
                                    parts.Add(partCounter, part);
                                    foreach (var item in parts)
                                    {
                                        e.Graphics.DrawString(item.Value, printFont2, Brushes.Gray, 220, yy);
                                        yy = yy + 20;
                                    }
                                }
                            }
                            System.Data.DataTable dt_cf_observe = this.cntrl.dt_cf_observe_clinicid(clinic_id);
                            if (dt_cf_observe.Rows.Count > 0)
                            {
                                if (Dexist == 1)
                                {
                                    yy = yy + 10;
                                    g.DrawLine(pen, new System.Drawing.Point(20, yy), new System.Drawing.Point(800, yy));
                                    yy = yy + 10;
                                }
                                else
                                {
                                    yy = yy + 40;
                                }
                                Dexist = 1;
                                e.Graphics.DrawString("Observations", printFont2, Brushes.Gray, 20, yy);
                                e.Graphics.DrawString("o", printFont2, Brushes.Gray, 200, yy);
                                int partLength = 80;
                                string sentence = dt_cf_observe.Rows[0]["observation_id"].ToString();
                                string[] words = sentence.Split(' ');
                                var parts = new Dictionary<int, string>();
                                string part = string.Empty;
                                int partCounter = 0;
                                foreach (var word in words)
                                {
                                    if (part.Length + word.Length < partLength)
                                    {
                                        part += string.IsNullOrEmpty(part) ? word : " " + word;
                                    }
                                    else
                                    {
                                        parts.Add(partCounter, part);
                                        part = word;
                                        partCounter++;
                                    }
                                }
                                parts.Add(partCounter, part);
                                foreach (var item in parts)
                                {
                                    e.Graphics.DrawString(item.Value, printFont2, Brushes.Gray, 220, yy);
                                    yy = yy + 20;
                                }
                                for (int k = 1; k < dt_cf_observe.Rows.Count; k++)
                                {
                                    e.Graphics.DrawString("o", printFont2, Brushes.Gray, 200, yy);
                                    partLength = 80;
                                    sentence = dt_cf_observe.Rows[k]["observation_id"].ToString();
                                    words = sentence.Split(' ');
                                    parts = new Dictionary<int, string>();
                                    part = string.Empty;
                                    partCounter = 0;
                                    foreach (var word in words)
                                    {
                                        if (part.Length + word.Length < partLength)
                                        {
                                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                                        }
                                        else
                                        {
                                            parts.Add(partCounter, part);
                                            part = word;
                                            partCounter++;
                                        }
                                    }
                                    parts.Add(partCounter, part);
                                    foreach (var item in parts)
                                    {
                                        e.Graphics.DrawString(item.Value, printFont2, Brushes.Gray, 220, yy);
                                        yy = yy + 20;
                                    }
                                }
                            }
                            System.Data.DataTable dt_cf_investigation = this.cntrl.dt_cf_investigation_clinicid(clinic_id);
                            if (dt_cf_investigation.Rows.Count > 0)
                            {
                                if (Dexist == 1)
                                {
                                    yy = yy + 10;
                                    g.DrawLine(pen, new System.Drawing.Point(20, yy), new System.Drawing.Point(800, yy));
                                    yy = yy + 10;
                                }
                                else
                                {
                                    yy = yy + 40;
                                }
                                Dexist = 1;
                                e.Graphics.DrawString("Investigation", printFont2, Brushes.Gray, 20, yy);
                                e.Graphics.DrawString("o", printFont2, Brushes.Gray, 200, yy);
                                int partLength = 80;
                                string sentence = dt_cf_investigation.Rows[0]["investigation_id"].ToString();
                                string[] words = sentence.Split(' ');
                                var parts = new Dictionary<int, string>();
                                string part = string.Empty;
                                int partCounter = 0;
                                foreach (var word in words)
                                {
                                    if (part.Length + word.Length < partLength)
                                    {
                                        part += string.IsNullOrEmpty(part) ? word : " " + word;
                                    }
                                    else
                                    {
                                        parts.Add(partCounter, part);
                                        part = word;
                                        partCounter++;
                                    }
                                }
                                parts.Add(partCounter, part);
                                foreach (var item in parts)
                                {
                                    e.Graphics.DrawString(item.Value, printFont2, Brushes.Gray, 220, yy);
                                    yy = yy + 20;
                                }

                                for (int k = 1; k < dt_cf_investigation.Rows.Count; k++)
                                {
                                    e.Graphics.DrawString("o", printFont2, Brushes.Gray, 200, yy);
                                    partLength = 80;
                                    sentence = dt_cf_investigation.Rows[k]["investigation_id"].ToString();
                                    words = sentence.Split(' ');
                                    parts = new Dictionary<int, string>();
                                    part = string.Empty;
                                    partCounter = 0;
                                    foreach (var word in words)
                                    {
                                        if (part.Length + word.Length < partLength)
                                        {
                                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                                        }
                                        else
                                        {
                                            parts.Add(partCounter, part);
                                            part = word;
                                            partCounter++;
                                        }
                                    }
                                    parts.Add(partCounter, part);
                                    foreach (var item in parts)
                                    {
                                        e.Graphics.DrawString(item.Value, printFont2, Brushes.Gray, 220, yy);
                                        yy = yy + 20;
                                    }
                                }
                            }
                            System.Data.DataTable dt_cf_diagnosis = this.cntrl.dt_cf_diagnosis_clinicid(clinic_id);
                            if (dt_cf_diagnosis.Rows.Count > 0)
                            {
                                if (Dexist == 1)
                                {
                                    yy = yy + 10;
                                    g.DrawLine(pen, new System.Drawing.Point(20, yy), new System.Drawing.Point(800, yy));
                                    yy = yy + 10;
                                }
                                else
                                {
                                    yy = yy + 40;
                                }
                                Dexist = 1;
                                Dexist = 1;
                                e.Graphics.DrawString("Diagnosis", printFont2, Brushes.Gray, 20, yy);
                                e.Graphics.DrawString("o", printFont2, Brushes.Gray, 200, yy);
                                int partLength = 80;
                                string sentence = dt_cf_diagnosis.Rows[0]["diagnosis_id"].ToString();
                                string[] words = sentence.Split(' ');
                                var parts = new Dictionary<int, string>();
                                string part = string.Empty;
                                int partCounter = 0;
                                foreach (var word in words)
                                {
                                    if (part.Length + word.Length < partLength)
                                    {
                                        part += string.IsNullOrEmpty(part) ? word : " " + word;
                                    }
                                    else
                                    {
                                        parts.Add(partCounter, part);
                                        part = word;
                                        partCounter++;
                                    }
                                }
                                parts.Add(partCounter, part);
                                foreach (var item in parts)
                                {
                                    e.Graphics.DrawString(item.Value, printFont2, Brushes.Gray, 220, yy);
                                    yy = yy + 20;
                                }
                                for (int k = 1; k < dt_cf_diagnosis.Rows.Count; k++)
                                {
                                    e.Graphics.DrawString("o", printFont2, Brushes.Gray, 200, yy);
                                    partLength = 80;
                                    sentence = dt_cf_diagnosis.Rows[k]["diagnosis_id"].ToString();
                                    words = sentence.Split(' ');
                                    parts = new Dictionary<int, string>();
                                    part = string.Empty;
                                    partCounter = 0;
                                    foreach (var word in words)
                                    {
                                        if (part.Length + word.Length < partLength)
                                        {
                                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                                        }
                                        else
                                        {
                                            parts.Add(partCounter, part);
                                            part = word;
                                            partCounter++;
                                        }
                                    }
                                    parts.Add(partCounter, part);
                                    foreach (var item in parts)
                                    {
                                        e.Graphics.DrawString(item.Value, printFont2, Brushes.Gray, 220, yy);
                                        yy = yy + 20;
                                    }
                                }
                            }
                            System.Data.DataTable dt_cf_note = this.cntrl.dt_cf_note_clincid(clinic_id);
                            if (dt_cf_note.Rows.Count > 0)
                            {
                                if (Dexist == 1)
                                {
                                    yy = yy + 10;
                                    g.DrawLine(pen, new System.Drawing.Point(20, yy), new System.Drawing.Point(800, yy));
                                    yy = yy + 10;
                                }
                                else
                                {
                                    yy = yy + 40;
                                }
                                Dexist = 1;
                                e.Graphics.DrawString("Notes", printFont2, Brushes.Gray, 20, yy);
                                e.Graphics.DrawString("o", printFont2, Brushes.Gray, 200, yy);
                                int partLength = 80;
                                string sentence = dt_cf_note.Rows[0]["note_name"].ToString();
                                string[] words = sentence.Split(' ');
                                var parts = new Dictionary<int, string>();
                                string part = string.Empty;
                                int partCounter = 0;
                                foreach (var word in words)
                                {
                                    if (part.Length + word.Length < partLength)
                                    {
                                        part += string.IsNullOrEmpty(part) ? word : " " + word;
                                    }
                                    else
                                    {
                                        parts.Add(partCounter, part);
                                        part = word;
                                        partCounter++;
                                    }
                                }
                                parts.Add(partCounter, part);
                                foreach (var item in parts)
                                {
                                    e.Graphics.DrawString(item.Value, printFont2, Brushes.Gray, 220, yy);
                                    yy = yy + 20;
                                }
                                for (int k = 1; k < dt_cf_note.Rows.Count; k++)
                                {
                                    e.Graphics.DrawString("o", printFont2, Brushes.Gray, 200, yy);
                                    partLength = 80;
                                    sentence = dt_cf_note.Rows[k]["note_name"].ToString();
                                    words = sentence.Split(' ');
                                    parts = new Dictionary<int, string>();
                                    part = string.Empty;
                                    partCounter = 0;
                                    foreach (var word in words)
                                    {
                                        if (part.Length + word.Length < partLength)
                                        {
                                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                                        }
                                        else
                                        {
                                            parts.Add(partCounter, part);
                                            part = word;
                                            partCounter++;
                                        }
                                    }
                                    parts.Add(partCounter, part);
                                    foreach (var item in parts)
                                    {
                                        e.Graphics.DrawString(item.Value, printFont2, Brushes.Gray, 220, yy);
                                        yy = yy + 20;
                                    }
                                }
                            }
                            System.Data.DataTable dt_cf_allergy = this.cntrl.dt_cf_allergy(clinic_id);
                            if (dt_cf_allergy.Rows.Count > 0)
                            {
                                if (Dexist == 1)
                                {
                                    yy = yy + 10;
                                    g.DrawLine(pen, new System.Drawing.Point(20, yy), new System.Drawing.Point(800, yy));
                                    yy = yy + 10;
                                }
                                else
                                {
                                    yy = yy + 40;
                                }
                                Dexist = 1;
                                e.Graphics.DrawString("Allergies", printFont2, Brushes.Gray, 20, yy);
                                e.Graphics.DrawString("o", printFont2, Brushes.Gray, 200, yy);
                                int partLength = 80;
                                string sentence = dt_cf_allergy.Rows[0]["allergy_name"].ToString();
                                string[] words = sentence.Split(' ');
                                var parts = new Dictionary<int, string>();
                                string part = string.Empty;
                                int partCounter = 0;
                                foreach (var word in words)
                                {
                                    if (part.Length + word.Length < partLength)
                                    {
                                        part += string.IsNullOrEmpty(part) ? word : " " + word;
                                    }
                                    else
                                    {
                                        parts.Add(partCounter, part);
                                        part = word;
                                        partCounter++;
                                    }
                                }
                                parts.Add(partCounter, part);
                                foreach (var item in parts)
                                {
                                    e.Graphics.DrawString(item.Value, printFont2, Brushes.Gray, 220, yy);
                                    yy = yy + 20;
                                }
                                for (int k = 1; k < dt_cf_allergy.Rows.Count; k++)
                                {
                                    e.Graphics.DrawString("o", printFont2, Brushes.Gray, 200, yy);
                                    partLength = 80;
                                    sentence = dt_cf_allergy.Rows[k]["allergy_name"].ToString();
                                    words = sentence.Split(' ');
                                    parts = new Dictionary<int, string>();
                                    part = string.Empty;
                                    partCounter = 0;
                                    foreach (var word in words)
                                    {
                                        if (part.Length + word.Length < partLength)
                                        {
                                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                                        }
                                        else
                                        {
                                            parts.Add(partCounter, part);
                                            part = word;
                                            partCounter++;
                                        }
                                    }
                                    parts.Add(partCounter, part);
                                    foreach (var item in parts)
                                    {
                                        e.Graphics.DrawString(item.Value, printFont2, Brushes.Gray, 220, yy);
                                        yy = yy + 20;
                                    }
                                }
                            }
                            System.Data.DataTable dt_cf_medical = this.cntrl.get_medicalId(patient_id);
                            if (dt_cf_medical.Rows.Count > 0)
                            {
                                if (Dexist == 1)
                                {
                                    yy = yy + 10;
                                    g.DrawLine(pen, new System.Drawing.Point(20, yy), new System.Drawing.Point(800, yy));
                                    yy = yy + 10;
                                }
                                else
                                {
                                    yy = yy + 40;
                                }
                                Dexist = 1;
                                e.Graphics.DrawString("Medical History", printFont2, Brushes.Gray, 20, yy);
                                e.Graphics.DrawString("o", printFont2, Brushes.Gray, 200, yy);
                                int partLength = 80;
                                string sentence = dt_cf_medical.Rows[0]["med_id"].ToString();
                                string[] words = sentence.Split(' ');
                                var parts = new Dictionary<int, string>();
                                string part = string.Empty;
                                int partCounter = 0;
                                foreach (var word in words)
                                {
                                    if (part.Length + word.Length < partLength)
                                    {
                                        part += string.IsNullOrEmpty(part) ? word : " " + word;
                                    }
                                    else
                                    {
                                        parts.Add(partCounter, part);
                                        part = word;
                                        partCounter++;
                                    }
                                }
                                parts.Add(partCounter, part);
                                foreach (var item in parts)
                                {
                                    e.Graphics.DrawString(item.Value, printFont2, Brushes.Gray, 220, yy);
                                    yy = yy + 20;
                                }
                                for (int k = 1; k < dt_cf_medical.Rows.Count; k++)
                                {
                                    e.Graphics.DrawString("o", printFont2, Brushes.Gray, 200, yy);
                                    partLength = 80;
                                    sentence = dt_cf_medical.Rows[k]["med_id"].ToString();
                                    words = sentence.Split(' ');
                                    parts = new Dictionary<int, string>();
                                    part = string.Empty;
                                    partCounter = 0;
                                    foreach (var word in words)
                                    {
                                        if (part.Length + word.Length < partLength)
                                        {
                                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                                        }
                                        else
                                        {
                                            parts.Add(partCounter, part);
                                            part = word;
                                            partCounter++;
                                        }
                                    }
                                    parts.Add(partCounter, part);
                                    foreach (var item in parts)
                                    {
                                        e.Graphics.DrawString(item.Value, printFont2, Brushes.Gray, 220, yy);
                                        yy = yy + 20;
                                    }
                                }
                            }
                            System.Data.DataTable dt_cf_medication = this.cntrl.dt_cf_meditation(clinic_id);
                            if (dt_cf_medication.Rows.Count > 0)
                            {
                                if (Dexist == 1)
                                {
                                    yy = yy + 10;
                                    g.DrawLine(pen, new System.Drawing.Point(20, yy), new System.Drawing.Point(800, yy));
                                    yy = yy + 10;
                                }
                                else
                                {
                                    yy = yy + 40;
                                }
                                Dexist = 1;
                                e.Graphics.DrawString("Current Medication", printFont2, Brushes.Gray, 20, yy);
                                e.Graphics.DrawString("o", printFont2, Brushes.Gray, 200, yy);
                                int partLength = 80;
                                string sentence = dt_cf_medication.Rows[0]["current_meditation"].ToString();
                                string[] words = sentence.Split(' ');
                                var parts = new Dictionary<int, string>();
                                string part = string.Empty;
                                int partCounter = 0;
                                foreach (var word in words)
                                {
                                    if (part.Length + word.Length < partLength)
                                    {
                                        part += string.IsNullOrEmpty(part) ? word : " " + word;
                                    }
                                    else
                                    {
                                        parts.Add(partCounter, part);
                                        part = word;
                                        partCounter++;
                                    }
                                }
                                parts.Add(partCounter, part);
                                foreach (var item in parts)
                                {
                                    e.Graphics.DrawString(item.Value, printFont2, Brushes.Gray, 220, yy);
                                    yy = yy + 20;
                                }
                                for (int k = 1; k < dt_cf_medication.Rows.Count; k++)
                                {
                                    e.Graphics.DrawString("o", printFont2, Brushes.Gray, 200, yy);
                                    partLength = 80;
                                    sentence = dt_cf_medication.Rows[k]["current_meditation"].ToString();
                                    words = sentence.Split(' ');
                                    parts = new Dictionary<int, string>();
                                    part = string.Empty;
                                    partCounter = 0;
                                    foreach (var word in words)
                                    {
                                        if (part.Length + word.Length < partLength)
                                        {
                                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                                        }
                                        else
                                        {
                                            parts.Add(partCounter, part);
                                            part = word;
                                            partCounter++;
                                        }
                                    }
                                    parts.Add(partCounter, part);
                                    foreach (var item in parts)
                                    {
                                        e.Graphics.DrawString(item.Value, printFont2, Brushes.Gray, 220, yy);
                                        yy = yy + 20;
                                    }
                                }
                            }
                            System.Data.DataTable dt_cf_advice = this.cntrl.dt_cf_advice(clinic_id);
                            if (dt_cf_advice.Rows.Count > 0)
                            {
                                if (Dexist == 1)
                                {
                                    yy = yy + 10;
                                    g.DrawLine(pen, new System.Drawing.Point(20, yy), new System.Drawing.Point(800, yy));
                                    yy = yy + 10;
                                }
                                else
                                {
                                    yy = yy + 40;
                                }
                                Dexist = 1;
                                e.Graphics.DrawString("Advice On Completion", printFont2, Brushes.Gray, 20, yy);
                                e.Graphics.DrawString("o", printFont2, Brushes.Gray, 200, yy);
                                int partLength = 80;
                                string sentence = dt_cf_advice.Rows[0]["discharge_advice"].ToString();
                                string[] words = sentence.Split(' ');
                                var parts = new Dictionary<int, string>();
                                string part = string.Empty;
                                int partCounter = 0;
                                foreach (var word in words)
                                {
                                    if (part.Length + word.Length < partLength)
                                    {
                                        part += string.IsNullOrEmpty(part) ? word : " " + word;
                                    }
                                    else
                                    {
                                        parts.Add(partCounter, part);
                                        part = word;
                                        partCounter++;
                                    }
                                }
                                parts.Add(partCounter, part);
                                foreach (var item in parts)
                                {
                                    e.Graphics.DrawString(item.Value, printFont2, Brushes.Gray, 220, yy);
                                    yy = yy + 20;
                                }
                                for (int k = 1; k < dt_cf_advice.Rows.Count; k++)
                                {
                                    e.Graphics.DrawString("o", printFont2, Brushes.Gray, 200, yy);
                                    partLength = 80;
                                    sentence = dt_cf_advice.Rows[k]["discharge_advice"].ToString();
                                    words = sentence.Split(' ');
                                    parts = new Dictionary<int, string>();
                                    part = string.Empty;
                                    partCounter = 0;
                                    foreach (var word in words)
                                    {
                                        if (part.Length + word.Length < partLength)
                                        {
                                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                                        }
                                        else
                                        {
                                            parts.Add(partCounter, part);
                                            part = word;
                                            partCounter++;
                                        }
                                    }
                                    parts.Add(partCounter, part);
                                    foreach (var item in parts)
                                    {
                                        e.Graphics.DrawString(item.Value, printFont2, Brushes.Gray, 220, yy);
                                        yy = yy + 20;
                                    }
                                }
                            }
                            System.Data.DataTable dt_cf_Nursenote = this.cntrl.dt_cf_Nursenote(clinic_id);
                            if (dt_cf_Nursenote.Rows.Count > 0)
                            {
                                if (Dexist == 1)
                                {
                                    yy = yy + 10;
                                    g.DrawLine(pen, new System.Drawing.Point(20, yy), new System.Drawing.Point(800, yy));
                                    yy = yy + 10;
                                }
                                else
                                {
                                    yy = yy + 40;
                                }
                                Dexist = 1;
                                e.Graphics.DrawString("Nurses Notes", printFont2, Brushes.Gray, 20, yy);
                                e.Graphics.DrawString("o", printFont2, Brushes.Gray, 200, yy);
                                int partLength = 80;
                                string sentence = dt_cf_Nursenote.Rows[0]["nurses_note"].ToString();
                                string[] words = sentence.Split(' ');
                                var parts = new Dictionary<int, string>();
                                string part = string.Empty;
                                int partCounter = 0;
                                foreach (var word in words)
                                {
                                    if (part.Length + word.Length < partLength)
                                    {
                                        part += string.IsNullOrEmpty(part) ? word : " " + word;
                                    }
                                    else
                                    {
                                        parts.Add(partCounter, part);
                                        part = word;
                                        partCounter++;
                                    }
                                }
                                parts.Add(partCounter, part);
                                foreach (var item in parts)
                                {
                                    e.Graphics.DrawString(item.Value, printFont2, Brushes.Gray, 220, yy);
                                    yy = yy + 20;
                                }
                                for (int k = 1; k < dt_cf_Nursenote.Rows.Count; k++)
                                {
                                    e.Graphics.DrawString("o", printFont2, Brushes.Gray, 200, yy);
                                    partLength = 80;
                                    sentence = dt_cf_Nursenote.Rows[k]["nurses_note"].ToString();
                                    words = sentence.Split(' ');
                                    parts = new Dictionary<int, string>();
                                    part = string.Empty;
                                    partCounter = 0;
                                    foreach (var word in words)
                                    {
                                        if (part.Length + word.Length < partLength)
                                        {
                                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                                        }
                                        else
                                        {
                                            parts.Add(partCounter, part);
                                            part = word;
                                            partCounter++;
                                        }
                                    }
                                    parts.Add(partCounter, part);
                                    foreach (var item in parts)
                                    {
                                        e.Graphics.DrawString(item.Value, printFont2, Brushes.Gray, 220, yy);
                                        yy = yy + 20;
                                    }
                                }
                            }
                        }
                        
                        e.Graphics.DrawString("Generated On: " + DateTime.Today.ToString("dd MMM yyyy"), printFont, Brushes.Gray, 20, 1000);
                        e.Graphics.DrawString("Page 1 of 1 ", printFont, Brushes.Gray, 300, 1000);
                        e.Graphics.DrawString("Powered by Pappyjoe", printFont, Brushes.Gray, 670, 1000);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            int currentMouseOverColumn = dataGridView1.HitTest(e.X, e.Y).ColumnIndex;
            if (currentMouseOverRow >= 0)
            {
                if (currentMouseOverColumn == 4)
                {
                    if (dataGridView1.Rows[currentMouseOverRow].Cells[0].Value.ToString() != "0")
                    {
                        clinic_id = dataGridView1.Rows[currentMouseOverRow].Cells[0].Value.ToString();
                        contextMenuStrip1.Show(dataGridView1, new System.Drawing.Point(930 - 120, e.Y));
                    }
                }
            }
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (doctor_id != "1")
                {
                    string id = privi_mdl.usr_priv_EMRCF_D(doctor_id);
                    if (int.Parse(id) > 0)
                    {
                        del_privilage();
                    }
                    else
                    {
                        MessageBox.Show("There is No Privilege to Delete Clinical findings", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    }
                }
                else
                {
                    del_privilage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void del_privilage()
        {
            try
            {
                if (clinic_id != "0")
                {
                    DialogResult res = MessageBox.Show("Are you sure you want to delete..?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (res == DialogResult.Yes)
                    {
                        this.cntrl.del_clinic_findings(clinic_id);
                        this.cntrl.del_cheif_comp(clinic_id);
                        this.cntrl.del_observ(clinic_id);
                        this.cntrl.del_invest(clinic_id);
                        this.cntrl.del_diagno(clinic_id);
                        this.cntrl.del_note(clinic_id);
                        //
                        this.cntrl.del_alergy(clinic_id);
                        this.cntrl.del_medialhistory(clinic_id);
                        this.cntrl.del_advice(clinic_id);
                        this.cntrl.del_meditation(clinic_id);
                        this.cntrl.del_nursesnotes(clinic_id);
                        dataGridView1.Rows.Clear();
                        dataGridView1.ColumnCount = 5;
                        dataGridView1.RowCount = 0;
                        dataGridView1.ColumnHeadersVisible = false;
                        dataGridView1.RowHeadersVisible = false;
                        dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        string heading = "";
                        System.Data.DataTable dt_cf_main = this.cntrl.dt_cf_main(patient_id);
                        int i = 0;
                        for (int j = 0; j < dt_cf_main.Rows.Count; j++)
                        {
                            dataGridView1.Rows.Add(dt_cf_main.Rows[j]["id"].ToString(), String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(dt_cf_main.Rows[j]["date"].ToString())), "", "", "");
                            dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.Bill;
                            dataGridView1.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                            dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.DarkGreen;
                            dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.LightGray;
                            dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.LightGray;
                            dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.LightGray;
                            dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.LightGray;
                            System.Data.DataTable dt_cf_Complaints = this.cntrl.dt_cf_Complaints(dt_cf_main.Rows[j]["id"].ToString());
                            if (dt_cf_Complaints.Rows.Count > 0)
                            {
                                int partLength = 90;
                                string sentence = dt_cf_Complaints.Rows[0]["complaint_id"].ToString();
                                string[] words = sentence.Split(' ');
                                var parts = new Dictionary<int, string>();
                                string part = string.Empty;
                                int partCounter = 0;
                                foreach (var word in words)
                                {
                                    if (part.Length + word.Length < partLength)
                                    {
                                        part += string.IsNullOrEmpty(part) ? word : " " + word;
                                    }
                                    else
                                    {
                                        parts.Add(partCounter, part);
                                        part = word;
                                        partCounter++;
                                    }
                                }
                                parts.Add(partCounter, part);
                                heading = "Complaints";
                                foreach (var item in parts)
                                {
                                    i = i + 1;
                                    dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                                    heading = "";
                                    dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                    dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                }

                                for (int k = 1; k < dt_cf_Complaints.Rows.Count; k++)
                                {
                                    partLength = 90;
                                    sentence = dt_cf_Complaints.Rows[k]["complaint_id"].ToString();
                                    words = sentence.Split(' ');
                                    parts = new Dictionary<int, string>();
                                    part = string.Empty;
                                    partCounter = 0;
                                    foreach (var word in words)
                                    {
                                        if (part.Length + word.Length < partLength)
                                        {
                                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                                        }
                                        else
                                        {
                                            parts.Add(partCounter, part);
                                            part = word;
                                            partCounter++;
                                        }
                                    }
                                    parts.Add(partCounter, part);
                                    foreach (var item in parts)
                                    {
                                        i = i + 1;
                                        dataGridView1.Rows.Add("0", "", "", item.Value, "");
                                        dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                    }
                                }
                            }
                            System.Data.DataTable dt_cf_observe = this.cntrl.dt_cf_observe(dt_cf_main.Rows[j]["id"].ToString());
                            if (dt_cf_observe.Rows.Count > 0)
                            {
                                i = i + 1;
                                dataGridView1.Rows.Add("0", "", "", "", "");
                                dataGridView1.Rows[i].Height = 1;
                                dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Gainsboro;
                                dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;

                                int partLength = 90;
                                string sentence = dt_cf_observe.Rows[0]["observation_id"].ToString();
                                string[] words = sentence.Split(' ');
                                var parts = new Dictionary<int, string>();
                                string part = string.Empty;
                                int partCounter = 0;
                                foreach (var word in words)
                                {
                                    if (part.Length + word.Length < partLength)
                                    {
                                        part += string.IsNullOrEmpty(part) ? word : " " + word;
                                    }
                                    else
                                    {
                                        parts.Add(partCounter, part);
                                        part = word;
                                        partCounter++;
                                    }
                                }
                                parts.Add(partCounter, part);
                                heading = "Observation";
                                foreach (var item in parts)
                                {
                                    i = i + 1;
                                    dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                                    heading = "";
                                    dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                    dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                }
                                for (int k = 1; k < dt_cf_observe.Rows.Count; k++)
                                {
                                    partLength = 90;
                                    sentence = dt_cf_observe.Rows[k]["observation_id"].ToString();
                                    words = sentence.Split(' ');
                                    parts = new Dictionary<int, string>();
                                    part = string.Empty;
                                    partCounter = 0;
                                    foreach (var word in words)
                                    {
                                        if (part.Length + word.Length < partLength)
                                        {
                                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                                        }
                                        else
                                        {
                                            parts.Add(partCounter, part);
                                            part = word;
                                            partCounter++;
                                        }
                                    }
                                    parts.Add(partCounter, part);
                                    foreach (var item in parts)
                                    {
                                        i = i + 1;
                                        dataGridView1.Rows.Add("0", "", "", item.Value, "");
                                        dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                    }
                                }
                            }
                            System.Data.DataTable dt_cf_investigation = this.cntrl.dt_cf_investigation(dt_cf_main.Rows[j]["id"].ToString());
                            if (dt_cf_investigation.Rows.Count > 0)
                            {
                                i = i + 1;
                                dataGridView1.Rows.Add("0", "", "", "", "");
                                dataGridView1.Rows[i].Height = 1;
                                dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Gainsboro;
                                dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;

                                int partLength = 90;
                                string sentence = dt_cf_investigation.Rows[0]["investigation_id"].ToString();
                                string[] words = sentence.Split(' ');
                                var parts = new Dictionary<int, string>();
                                string part = string.Empty;
                                int partCounter = 0;
                                foreach (var word in words)
                                {
                                    if (part.Length + word.Length < partLength)
                                    {
                                        part += string.IsNullOrEmpty(part) ? word : " " + word;
                                    }
                                    else
                                    {
                                        parts.Add(partCounter, part);
                                        part = word;
                                        partCounter++;
                                    }
                                }
                                parts.Add(partCounter, part);
                                heading = "Investigation";
                                foreach (var item in parts)
                                {
                                    i = i + 1;
                                    dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                                    heading = "";
                                    dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                    dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                }

                                for (int k = 1; k < dt_cf_investigation.Rows.Count; k++)
                                {
                                    partLength = 90;
                                    sentence = dt_cf_investigation.Rows[k]["investigation_id"].ToString();
                                    words = sentence.Split(' ');
                                    parts = new Dictionary<int, string>();
                                    part = string.Empty;
                                    partCounter = 0;
                                    foreach (var word in words)
                                    {
                                        if (part.Length + word.Length < partLength)
                                        {
                                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                                        }
                                        else
                                        {
                                            parts.Add(partCounter, part);
                                            part = word;
                                            partCounter++;
                                        }
                                    }
                                    parts.Add(partCounter, part);
                                    foreach (var item in parts)
                                    {
                                        i = i + 1;
                                        dataGridView1.Rows.Add("0", "", "", item.Value, "");
                                        dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                    }
                                }
                            }
                            System.Data.DataTable dt_cf_diagnosis = this.cntrl.dt_cf_diagnosis(dt_cf_main.Rows[j]["id"].ToString());
                            if (dt_cf_diagnosis.Rows.Count > 0)
                            {
                                i = i + 1;
                                dataGridView1.Rows.Add("0", "", "", "", "");
                                dataGridView1.Rows[i].Height = 1;
                                dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Gainsboro;
                                dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                int partLength = 90;
                                string sentence = dt_cf_diagnosis.Rows[0]["diagnosis_id"].ToString();
                                string[] words = sentence.Split(' ');
                                var parts = new Dictionary<int, string>();
                                string part = string.Empty;
                                int partCounter = 0;
                                foreach (var word in words)
                                {
                                    if (part.Length + word.Length < partLength)
                                    {
                                        part += string.IsNullOrEmpty(part) ? word : " " + word;
                                    }
                                    else
                                    {
                                        parts.Add(partCounter, part);
                                        part = word;
                                        partCounter++;
                                    }
                                }
                                parts.Add(partCounter, part);
                                heading = "Diagnosis";
                                foreach (var item in parts)
                                {
                                    i = i + 1;
                                    dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                                    heading = "";
                                    dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                    dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                }
                                for (int k = 1; k < dt_cf_diagnosis.Rows.Count; k++)
                                {
                                    partLength = 90;
                                    sentence = dt_cf_diagnosis.Rows[k]["diagnosis_id"].ToString();
                                    words = sentence.Split(' ');
                                    parts = new Dictionary<int, string>();
                                    part = string.Empty;
                                    partCounter = 0;
                                    foreach (var word in words)
                                    {
                                        if (part.Length + word.Length < partLength)
                                        {
                                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                                        }
                                        else
                                        {
                                            parts.Add(partCounter, part);
                                            part = word;
                                            partCounter++;
                                        }
                                    }
                                    parts.Add(partCounter, part);
                                    foreach (var item in parts)
                                    {
                                        i = i + 1;
                                        dataGridView1.Rows.Add("0", "", "", item.Value, "");
                                        dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                    }
                                }
                            }
                            System.Data.DataTable dt_cf_note = this.cntrl.dt_cf_note(dt_cf_main.Rows[j]["id"].ToString());
                            if (dt_cf_note.Rows.Count > 0)
                            {
                                i = i + 1;
                                dataGridView1.Rows.Add("0", "", "", "", "");
                                dataGridView1.Rows[i].Height = 1;
                                dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Gainsboro;
                                dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;

                                int partLength = 90;
                                string sentence = dt_cf_note.Rows[0]["note_name"].ToString();
                                string[] words = sentence.Split(' ');
                                var parts = new Dictionary<int, string>();
                                string part = string.Empty;
                                int partCounter = 0;
                                foreach (var word in words)
                                {
                                    if (part.Length + word.Length < partLength)
                                    {
                                        part += string.IsNullOrEmpty(part) ? word : " " + word;
                                    }
                                    else
                                    {
                                        parts.Add(partCounter, part);
                                        part = word;
                                        partCounter++;
                                    }
                                }
                                parts.Add(partCounter, part);
                                heading = "Note";
                                foreach (var item in parts)
                                {
                                    i = i + 1;
                                    dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                                    heading = "";
                                    dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                    dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                }
                                for (int k = 1; k < dt_cf_note.Rows.Count; k++)
                                {
                                    partLength = 90;
                                    sentence = dt_cf_note.Rows[k]["note_name"].ToString();
                                    words = sentence.Split(' ');
                                    parts = new Dictionary<int, string>();
                                    part = string.Empty;
                                    partCounter = 0;
                                    foreach (var word in words)
                                    {
                                        if (part.Length + word.Length < partLength)
                                        {
                                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                                        }
                                        else
                                        {
                                            parts.Add(partCounter, part);
                                            part = word;
                                            partCounter++;
                                        }
                                    }
                                    parts.Add(partCounter, part);
                                    foreach (var item in parts)
                                    {
                                        i = i + 1;
                                        dataGridView1.Rows.Add("0", "", "", item.Value, "");
                                        dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                    }
                                }
                            }
                            System.Data.DataTable dt_cf_allergy = this.cntrl.dt_cf_allergy(dt_cf_main.Rows[j]["id"].ToString());
                            if (dt_cf_allergy.Rows.Count > 0)
                            {
                                i = i + 1;
                                dataGridView1.Rows.Add("0", "", "", "", "");
                                dataGridView1.Rows[i].Height = 1;
                                dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Gainsboro;
                                dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                int partLength = 90;
                                string sentence = dt_cf_allergy.Rows[0]["allergy_name"].ToString();
                                string[] words = sentence.Split(' ');
                                var parts = new Dictionary<int, string>();
                                string part = string.Empty;
                                int partCounter = 0;
                                foreach (var word in words)
                                {
                                    if (part.Length + word.Length < partLength)
                                    {
                                        part += string.IsNullOrEmpty(part) ? word : " " + word;
                                    }
                                    else
                                    {
                                        parts.Add(partCounter, part);
                                        part = word;
                                        partCounter++;
                                    }
                                }
                                parts.Add(partCounter, part);
                                heading = "Allergies";
                                foreach (var item in parts)
                                {
                                    i = i + 1;
                                    dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                                    heading = "";
                                    dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                    dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                }
                                for (int k = 1; k < dt_cf_allergy.Rows.Count; k++)
                                {
                                    partLength = 90;
                                    sentence = dt_cf_allergy.Rows[k]["allergy_name"].ToString();
                                    words = sentence.Split(' ');
                                    parts = new Dictionary<int, string>();
                                    part = string.Empty;
                                    partCounter = 0;
                                    foreach (var word in words)
                                    {
                                        if (part.Length + word.Length < partLength)
                                        {
                                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                                        }
                                        else
                                        {
                                            parts.Add(partCounter, part);
                                            part = word;
                                            partCounter++;
                                        }
                                    }
                                    parts.Add(partCounter, part);
                                    foreach (var item in parts)
                                    {
                                        i = i + 1;
                                        dataGridView1.Rows.Add("0", "", "", item.Value, "");
                                        dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                    }
                                }
                            }
                            System.Data.DataTable dt_cf_Nursenote = this.cntrl.dt_cf_Nursenote(dt_cf_main.Rows[j]["id"].ToString());
                            if (dt_cf_Nursenote.Rows.Count > 0)
                            {
                                i = i + 1;
                                dataGridView1.Rows.Add("0", "", "", "", "");
                                dataGridView1.Rows[i].Height = 1;
                                dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Gainsboro;
                                dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                int partLength = 90;
                                string sentence = dt_cf_Nursenote.Rows[0]["nurses_note"].ToString();
                                string[] words = sentence.Split(' ');
                                var parts = new Dictionary<int, string>();
                                string part = string.Empty;
                                int partCounter = 0;
                                foreach (var word in words)
                                {
                                    if (part.Length + word.Length < partLength)
                                    {
                                        part += string.IsNullOrEmpty(part) ? word : " " + word;
                                    }
                                    else
                                    {
                                        parts.Add(partCounter, part);
                                        part = word;
                                        partCounter++;
                                    }
                                }
                                parts.Add(partCounter, part);
                                heading = "Nurses Notes";
                                foreach (var item in parts)
                                {
                                    i = i + 1;
                                    dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                                    heading = "";
                                    dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                    dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                }
                                for (int k = 1; k < dt_cf_Nursenote.Rows.Count; k++)
                                {
                                    partLength = 90;
                                    sentence = dt_cf_Nursenote.Rows[k]["nurses_note"].ToString();
                                    words = sentence.Split(' ');
                                    parts = new Dictionary<int, string>();
                                    part = string.Empty;
                                    partCounter = 0;
                                    foreach (var word in words)
                                    {
                                        if (part.Length + word.Length < partLength)
                                        {
                                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                                        }
                                        else
                                        {
                                            parts.Add(partCounter, part);
                                            part = word;
                                            partCounter++;
                                        }
                                    }
                                    parts.Add(partCounter, part);
                                    foreach (var item in parts)
                                    {
                                        i = i + 1;
                                        dataGridView1.Rows.Add("0", "", "", item.Value, "");
                                        dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                    }
                                }
                            }
                            System.Data.DataTable dt_cf_medical = this.cntrl.get_medicalId(patient_id);
                            if (dt_cf_medical.Rows.Count > 0)
                            {
                                i = i + 1;
                                dataGridView1.Rows.Add("0", "", "", "", "");
                                dataGridView1.Rows[i].Height = 1;
                                dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Gainsboro;
                                dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                int partLength = 90;
                                string sentence = dt_cf_medical.Rows[0]["med_id"].ToString();
                                string[] words = sentence.Split(' ');
                                var parts = new Dictionary<int, string>();
                                string part = string.Empty;
                                int partCounter = 0;
                                foreach (var word in words)
                                {
                                    if (part.Length + word.Length < partLength)
                                    {
                                        part += string.IsNullOrEmpty(part) ? word : " " + word;
                                    }
                                    else
                                    {
                                        parts.Add(partCounter, part);
                                        part = word;
                                        partCounter++;
                                    }
                                }
                                parts.Add(partCounter, part);
                                heading = "Medical History";
                                foreach (var item in parts)
                                {
                                    i = i + 1;
                                    dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                                    heading = "";
                                    dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                    dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                }
                                for (int k = 1; k < dt_cf_medical.Rows.Count; k++)
                                {
                                    partLength = 90;
                                    sentence = dt_cf_medical.Rows[k]["med_id"].ToString();
                                    words = sentence.Split(' ');
                                    parts = new Dictionary<int, string>();
                                    part = string.Empty;
                                    partCounter = 0;
                                    foreach (var word in words)
                                    {
                                        if (part.Length + word.Length < partLength)
                                        {
                                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                                        }
                                        else
                                        {
                                            parts.Add(partCounter, part);
                                            part = word;
                                            partCounter++;
                                        }
                                    }
                                    parts.Add(partCounter, part);
                                    foreach (var item in parts)
                                    {
                                        i = i + 1;
                                        dataGridView1.Rows.Add("0", "", "", item.Value, "");
                                        dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                    }
                                }
                            }
                            System.Data.DataTable dt_cf_medication = this.cntrl.dt_cf_meditation(dt_cf_main.Rows[j]["id"].ToString());
                            if (dt_cf_medication.Rows.Count > 0)
                            {
                                i = i + 1;
                                dataGridView1.Rows.Add("0", "", "", "", "");
                                dataGridView1.Rows[i].Height = 1;
                                dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Gainsboro;
                                dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                int partLength = 90;
                                string sentence = dt_cf_medication.Rows[0]["current_meditation"].ToString();
                                string[] words = sentence.Split(' ');
                                var parts = new Dictionary<int, string>();
                                string part = string.Empty;
                                int partCounter = 0;
                                foreach (var word in words)
                                {
                                    if (part.Length + word.Length < partLength)
                                    {
                                        part += string.IsNullOrEmpty(part) ? word : " " + word;
                                    }
                                    else
                                    {
                                        parts.Add(partCounter, part);
                                        part = word;
                                        partCounter++;
                                    }
                                }
                                parts.Add(partCounter, part);
                                heading = "Current Medication";
                                foreach (var item in parts)
                                {
                                    i = i + 1;
                                    dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                                    heading = "";
                                    dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                    dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                }
                                for (int k = 1; k < dt_cf_medication.Rows.Count; k++)
                                {
                                    partLength = 90;
                                    sentence = dt_cf_medication.Rows[k]["current_meditation"].ToString();
                                    words = sentence.Split(' ');
                                    parts = new Dictionary<int, string>();
                                    part = string.Empty;
                                    partCounter = 0;
                                    foreach (var word in words)
                                    {
                                        if (part.Length + word.Length < partLength)
                                        {
                                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                                        }
                                        else
                                        {
                                            parts.Add(partCounter, part);
                                            part = word;
                                            partCounter++;
                                        }
                                    }
                                    parts.Add(partCounter, part);
                                    foreach (var item in parts)
                                    {
                                        i = i + 1;
                                        dataGridView1.Rows.Add("0", "", "", item.Value, "");
                                        dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                    }
                                }
                            }
                            System.Data.DataTable dt_cf_advice = this.cntrl.dt_cf_advice(dt_cf_main.Rows[j]["id"].ToString());
                            if (dt_cf_advice.Rows.Count > 0)
                            {
                                i = i + 1;
                                dataGridView1.Rows.Add("0", "", "", "", "");
                                dataGridView1.Rows[i].Height = 1;
                                dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Gainsboro;
                                dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                int partLength = 90;
                                string sentence = dt_cf_advice.Rows[0]["discharge_advice"].ToString();
                                string[] words = sentence.Split(' ');
                                var parts = new Dictionary<int, string>();
                                string part = string.Empty;
                                int partCounter = 0;
                                foreach (var word in words)
                                {
                                    if (part.Length + word.Length < partLength)
                                    {
                                        part += string.IsNullOrEmpty(part) ? word : " " + word;
                                    }
                                    else
                                    {
                                        parts.Add(partCounter, part);
                                        part = word;
                                        partCounter++;
                                    }
                                }
                                parts.Add(partCounter, part);
                                heading = "Advice On Completion";
                                foreach (var item in parts)
                                {
                                    i = i + 1;
                                    dataGridView1.Rows.Add("0", heading, "", item.Value, "");
                                    heading = "";
                                    dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                    dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                }
                                for (int k = 1; k < dt_cf_advice.Rows.Count; k++)
                                {
                                    partLength = 90;
                                    sentence = dt_cf_advice.Rows[k]["discharge_advice"].ToString();
                                    words = sentence.Split(' ');
                                    parts = new Dictionary<int, string>();
                                    part = string.Empty;
                                    partCounter = 0;
                                    foreach (var word in words)
                                    {
                                        if (part.Length + word.Length < partLength)
                                        {
                                            part += string.IsNullOrEmpty(part) ? word : " " + word;
                                        }
                                        else
                                        {
                                            parts.Add(partCounter, part);
                                            part = word;
                                            partCounter++;
                                        }
                                    }
                                    parts.Add(partCounter, part);
                                    foreach (var item in parts)
                                    {
                                        i = i + 1;
                                        dataGridView1.Rows.Add("0", "", "", item.Value, "");
                                        dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                    }
                                }
                            }

                            i = i + 1;
                            dataGridView1.Rows.Add("0", "Note by Dr." + dt_cf_main.Rows[j]["doctor_name"].ToString(), "", "", "");
                            dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                            dataGridView1.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                            dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                            i = i + 1;
                            dataGridView1.Rows.Add("0", "", "", "", "");
                            dataGridView1.Rows[i].Cells[4].Value = PappyjoeMVC.Properties.Resources.blank;
                            i = i + 1;
                        }
                        string dt = DateTime.Now.Date.ToString("yyyy-MM-dd");
                        DateTime Timeonly = DateTime.Now;
                        this.cntrl.save_log(doctor_id, "Clinical Findings", " Delete Clinical Findings", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Delete", clinic_id);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (doctor_id != "1")
                {
                    string id = privi_mdl.user_priv_EMRC_E(doctor_id);
                    if (int.Parse(id) > 0)
                    {
                        if (clinic_id != "0")
                        {
                            var form2 = new PappyjoeMVC.View.Add_Clinical_Notes();
                            //ClinicalNotesAdd_controller cnt = new ClinicalNotesAdd_controller(form2);
                            form2.clinic_id = clinic_id;
                            form2.patient_id = patient_id;
                            form2.doctor_id = doctor_id;
                            openform(form2);
                        }
                    }
                    else
                    {
                        MessageBox.Show("There is No Privilege to Edit Clinical findings", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    }
                }
                else
                {
                    if (clinic_id != "0")
                    {
                        var form2 = new PappyjoeMVC.View.Add_Clinical_Notes();
                        //ClinicalNotesAdd_controller cnt = new ClinicalNotesAdd_controller(form2);
                        form2.clinic_id = clinic_id;
                        form2.patient_id = patient_id;
                        form2.doctor_id = doctor_id;
                        openform(form2);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void emailToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                string doct = this.cntrl.Get_DoctorName(doctor_id);
                string doctor_name = "";
                if (doct != "")
                {
                    doctor_name = doct;
                }
                System.Data.DataTable patient = this.cntrl.patient_information(patient_id);
                string Pname = "", Gender = "", address = "", DOA = "", age = "", Mobile = "", DOB = "";
                if (patient.Rows.Count > 0)
                {
                    Pname = patient.Rows[0]["pt_name"].ToString();
                    Gender = patient.Rows[0]["gender"].ToString();
                    address = patient.Rows[0]["street_address"].ToString() + " , " + patient.Rows[0]["city"].ToString();
                    Mobile = patient.Rows[0]["primary_mobile_number"].ToString();
                    DOA = DateTime.Parse(patient.Rows[0]["date"].ToString()).ToString("dd/MM/yyyy");
                    age = patient.Rows[0]["age"].ToString();
                    DOB = patient.Rows[0]["date_of_birth"].ToString();
                }
                string contact_no = "";
                string clinic_name = "";
                System.Data.DataTable dtp = this.cntrl.Get_Practice_details();
                if (dtp.Rows.Count > 0)
                {
                    clinic_name = dtp.Rows[0]["name"].ToString();
                    contact_no = dtp.Rows[0]["contact_no"].ToString();
                }
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                StreamWriter sWrite = new StreamWriter(Apppath + "\\Clinic_findings.html");
                sWrite.WriteLine("<html>");
                sWrite.WriteLine("<head>");
                sWrite.WriteLine("</head>");
                sWrite.WriteLine("<body >");
                sWrite.WriteLine("<br><br><br>");
                sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr><th align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=4>" + clinic_name.ToString() + "</font></th></tr>");
                sWrite.WriteLine("<tr><th align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>" + contact_no.ToString() + "</font></th></tr>");
                sWrite.WriteLine("<tr><th align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3><hr></font></th></tr>");
                sWrite.WriteLine("</table>");

                sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine(" <tr height='40px'>");
                sWrite.WriteLine("    <td align='left' width='400px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Consulted by : <b> " + doctor_name.ToString() + " </b></font></td>");
                sWrite.WriteLine("	<td align='left' width='170px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2></font></td>");
                sWrite.WriteLine("	<td align='left' width='130px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2></font></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("    <td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Name :<b>" + Pname.ToString() + " </b></font></td>");
                sWrite.WriteLine("	<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>DOB : <b> " + DOB.ToString() + " </b></font></td>");
                sWrite.WriteLine("	<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Gender : <b>" + Gender.ToString() + " </b></font></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine(" <tr>");
                sWrite.WriteLine("    <td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Address :<b> " + address.ToString() + "</b></font></td>");
                sWrite.WriteLine("	<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>DOA : <b> " + DOA + "</b></font></td>");
                sWrite.WriteLine("	<td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Age : <b> " + age + "</b></font></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine(" <tr>");
                sWrite.WriteLine("    <td align='left' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Mobile No:<b> " + Mobile.ToString() + "</b></font></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine("</table>");

                System.Data.DataTable dt_clinical_Findings = this.cntrl.dt_cf_id_date(clinic_id);
                int i = 0;
                sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                System.Data.DataTable dt_cf_Complaints = this.cntrl.dt_cf_Complaints(dt_clinical_Findings.Rows[0]["id"].ToString());
                if (dt_cf_Complaints.Rows.Count > 0)
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<th align='left' width='230px' bgcolor='#0099FF' ><FONT COLOR=white FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;COMPLINTS</font></th>");
                    sWrite.WriteLine("</tr>");
                    for (i = 0; i < dt_cf_Complaints.Rows.Count; i++)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;" + " " + "" + dt_cf_Complaints.Rows[i]["complaint_id"].ToString() + "</font></td>");
                        sWrite.WriteLine("</tr>");
                    }
                }
                System.Data.DataTable dt_cf_observe = this.cntrl.dt_cf_observe(dt_clinical_Findings.Rows[0]["id"].ToString());
                if (dt_cf_observe.Rows.Count > 0)
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<th align='left' width='230px' bgcolor='#0099FF' ><FONT COLOR=white FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;OBSERVATION</font></th>");
                    sWrite.WriteLine("</tr>");

                    for (i = 0; i < dt_cf_observe.Rows.Count; i++)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;" + " " + "" + dt_cf_observe.Rows[i]["observation_id"].ToString() + "</font></td>");
                        sWrite.WriteLine("</tr>");
                    }
                }
                System.Data.DataTable dt_cf_investigation = this.cntrl.dt_cf_investigation(dt_clinical_Findings.Rows[0]["id"].ToString());
                if (dt_cf_investigation.Rows.Count > 0)
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<th align='left' width='230px' bgcolor='#0099FF' ><FONT COLOR=white FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;INVESTIGATION</font></th>");
                    sWrite.WriteLine("</tr>");
                    for (i = 0; i < dt_cf_investigation.Rows.Count; i++)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;" + dt_cf_investigation.Rows[i]["investigation_id"].ToString() + "</font></td>");
                        sWrite.WriteLine("</tr>");
                    }
                }
                System.Data.DataTable dt_cf_diagnosis = this.cntrl.dt_cf_diagnosis(dt_clinical_Findings.Rows[0]["id"].ToString());
                if (dt_cf_diagnosis.Rows.Count > 0)
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<th align='left' width='230px' bgcolor='#0099FF' ><FONT COLOR=white FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;DIAGNOSIS</font></th>");
                    sWrite.WriteLine("</tr>");
                    for (i = 0; i < dt_cf_diagnosis.Rows.Count; i++)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;" + dt_cf_diagnosis.Rows[i]["diagnosis_id"].ToString() + "</font></td>");
                        sWrite.WriteLine("</tr>");
                    }
                }
                System.Data.DataTable dt_cf_note = this.cntrl.dt_cf_note(dt_clinical_Findings.Rows[0]["id"].ToString());
                if (dt_cf_note.Rows.Count > 0)
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<th align='left' width='230px' bgcolor='#0099FF' ><FONT COLOR=white FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;NOTE</font></th>");
                    sWrite.WriteLine("</tr>");
                    for (i = 0; i < dt_cf_note.Rows.Count; i++)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;" + dt_cf_note.Rows[i]["note_name"].ToString() + "</font></td>");
                        sWrite.WriteLine("</tr>");
                    }
                    
                }
                System.Data.DataTable dt_cf_allergy = this.cntrl.dt_cf_allergy(dt_clinical_Findings.Rows[0]["id"].ToString());
                if (dt_cf_allergy.Rows.Count > 0)
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<th align='left' width='230px' bgcolor='#0099FF' ><FONT COLOR=white FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;Allergies</font></th>");
                    sWrite.WriteLine("</tr>");
                    for (i = 0; i < dt_cf_allergy.Rows.Count; i++)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;" + dt_cf_allergy.Rows[i]["allergy_name"].ToString() + "</font></td>");
                        sWrite.WriteLine("</tr>");
                    }
                    sWrite.WriteLine(" </table>");
                    sWrite.WriteLine(" </td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr >");
                    sWrite.WriteLine("    <td align='center' height='20'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3></font></td>");
                    sWrite.WriteLine("</tr>");
                }
                sWrite.WriteLine("<tr >");
                sWrite.WriteLine("    <td align='right' height='20' width='700px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>powered by</font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr >");
                sWrite.WriteLine("    <td align='right' height='81' width='700px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1><img src='http://pappyjoe.com/assets/images/pappyjoe-logo.PNG' alt='pappyjoe official logo'></font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("</td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("</body>");
                sWrite.WriteLine("</html>");
                sWrite.Close();
                // mail senting...
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

                            StreamReader reader = new StreamReader(Apppath + "\\Clinic_findings.html");
                            string readFile = reader.ReadToEnd();
                            string StrContent = "";
                            StrContent = readFile;
                            MailMessage message = new MailMessage();
                            message.From = new MailAddress(email);
                            message.To.Add(email);
                            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                            message.Subject = "Clinical Findings";
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
                            MessageBox.Show("Email is Sent To : " + email, "Success ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            reader.Close();

                        }
                        else
                        {
                            MessageBox.Show("Please Activate Email Configuration", "Activate Email", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Add EmailId for Selected patient", "Add Email Id", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dotmatrixPrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDocument printdocument = new PrintDocument();
                printdocument.PrintPage += printDocument1_PrintPagedotMatrix;
                printdocument.Print();
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void printDocument1_PrintPagedotMatrix(object sender, PrintPageEventArgs e)
        {
            try
            {
                System.Data.DataTable dtp = this.cntrl.get_company_details();
                System.Data.DataTable dt1 = this.cntrl.Get_Patient_Details(patient_id);
                using (System.Drawing.Font printFont = new System.Drawing.Font("Times New Roman", 16))
                {
                    e.Graphics.DrawString(dtp.Rows[0][1].ToString(), printFont, Brushes.Black, 16, 100);
                }
                using (System.Drawing.Font printFont = new System.Drawing.Font("Times New Roman", 12))
                {
                    e.Graphics.DrawString(dtp.Rows[0][9].ToString(), printFont, Brushes.Black, 16, 130);
                }
                Graphics g = e.Graphics;
                Pen pen = new Pen(Color.Gray);
                int yy = 205;
                string sexage = "";
                int Dexist = 0;
                string address = "";
                using (System.Drawing.Font printFont = new System.Drawing.Font("Times New Roman", 10))
                {
                    using (System.Drawing.Font printFont1 = new System.Drawing.Font("Times New Roman", 10))
                    {
                        e.Graphics.DrawString(dt1.Rows[0]["pt_name"].ToString(), printFont1, Brushes.Black, 14, yy);
                        if (dt1.Rows[0]["gender"].ToString() != "")
                        {
                            sexage = "Gender :" + dt1.Rows[0]["gender"].ToString();
                            Dexist = 1;
                        }
                        if (dt1.Rows[0]["age"].ToString() != "")
                        {
                            if (Dexist == 1)
                            {
                                sexage = sexage + ", " + dt1.Rows[0]["age"].ToString() + dt1.Rows[0]["days"].ToString();
                            }
                            else
                            {
                                sexage = dt1.Rows[0]["age"].ToString() + dt1.Rows[0]["days"].ToString();
                            }
                        }
                        e.Graphics.DrawString(sexage, printFont1, Brushes.Black, 300, yy);
                    }
                    yy = yy + 22;
                    e.Graphics.DrawString("Patient id:" + dt1.Rows[0]["pt_id"].ToString(), printFont, Brushes.Black, 14, yy);
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
                    e.Graphics.DrawString(address, printFont, Brushes.Black, 300, yy);
                    if (dt1.Rows[0]["aadhar_id"].ToString() != "")
                    {
                        yy = yy + 20;
                        e.Graphics.DrawString("Aadhaar ID:" + dt1.Rows[0]["aadhar_id"].ToString(), printFont, Brushes.Black, 14, yy);
                    }
                    if (dt1.Rows[0]["primary_mobile_number"].ToString() != "")
                    {
                        yy = yy + 20;
                        e.Graphics.DrawString(dt1.Rows[0]["primary_mobile_number"].ToString(), printFont, Brushes.Black, 14, yy);
                    }
                    if (dt1.Rows[0]["email_address"].ToString() != "")
                    {
                        yy = yy + 20;
                        e.Graphics.DrawString(dt1.Rows[0]["email_address"].ToString(), printFont, Brushes.Black, 14, yy);
                    }
                    yy = yy + 20;
                    e.Graphics.DrawString("Medical History:", printFont, Brushes.Black, 14, yy);
                    yy = yy + 20;
                    yy = yy + 30;
                    Dexist = 0;
                    System.Data.DataTable dt_cf = this.cntrl.dt_cf(clinic_id, patient_id);
                    if (dt_cf.Rows.Count > 0)
                    {
                        e.Graphics.DrawString("By:", printFont, Brushes.Black, 14, yy);
                        using (System.Drawing.Font printFont1 = new System.Drawing.Font("Times New Roman", 10))
                        {
                            e.Graphics.DrawString("Dr." + Convert.ToString(dt_cf.Rows[0]["doctor_name"].ToString()), printFont1, Brushes.Black, 40, yy);
                        }
                        e.Graphics.DrawString("Date: ", printFont, Brushes.Black, 690, yy);
                        using (System.Drawing.Font printFont1 = new System.Drawing.Font("Times New Roman", 10))
                        {
                            DateTime tdate = Convert.ToDateTime(dt_cf.Rows[0]["date"].ToString());
                            e.Graphics.DrawString(tdate.ToString("dd MMM yyyy"), printFont1, Brushes.Black, 720, yy);
                        }
                    }
                    using (System.Drawing.Font printFont2 = new System.Drawing.Font("Times New Roman", 11))
                    {
                        System.Data.DataTable dt_cf_Complaints = this.cntrl.dt_cf_Complaints(clinic_id);
                        if (dt_cf_Complaints.Rows.Count > 0)
                        {
                            yy = yy + 40;
                            Dexist = 1;
                            e.Graphics.DrawString("Complaints", printFont2, Brushes.Black, 14, yy);
                            e.Graphics.DrawString("o", printFont2, Brushes.Black, 200, yy);
                            e.Graphics.DrawString(dt_cf_Complaints.Rows[0]["complaint_id"].ToString(), printFont2, Brushes.Black, 220, yy);
                            yy = yy + 20;
                            for (int k = 1; k < dt_cf_Complaints.Rows.Count; k++)
                            {
                                e.Graphics.DrawString("o", printFont2, Brushes.Black, 200, yy);
                                e.Graphics.DrawString(dt_cf_Complaints.Rows[k]["complaint_id"].ToString(), printFont2, Brushes.Black, 220, yy);
                                yy = yy + 20;
                            }
                        }
                        // Observation
                        System.Data.DataTable dt_cf_observe = this.cntrl.dt_cf_observe(clinic_id);
                        if (dt_cf_observe.Rows.Count > 0)
                        {
                            if (Dexist == 1)
                            {
                                yy = yy + 10;
                                g.DrawLine(pen, new System.Drawing.Point(20, yy), new System.Drawing.Point(800, yy));
                                yy = yy + 10;
                            }
                            else
                            {
                                yy = yy + 40;
                            }
                            Dexist = 1;
                            e.Graphics.DrawString("Observations", printFont2, Brushes.Black, 14, yy);
                            e.Graphics.DrawString("o", printFont2, Brushes.Black, 200, yy);
                            e.Graphics.DrawString(dt_cf_observe.Rows[0]["observation_id"].ToString(), printFont2, Brushes.Black, 220, yy);
                            yy = yy + 20;
                            for (int k = 1; k < dt_cf_observe.Rows.Count; k++)
                            {
                                e.Graphics.DrawString("o", printFont2, Brushes.Black, 200, yy);
                                e.Graphics.DrawString(dt_cf_observe.Rows[k]["observation_id"].ToString(), printFont2, Brushes.Black, 220, yy);
                                yy = yy + 20;
                            }
                        }
                        // Investigation
                        System.Data.DataTable dt_cf_investigation = this.cntrl.dt_cf_investigation(clinic_id);
                        if (dt_cf_investigation.Rows.Count > 0)
                        {
                            if (Dexist == 1)
                            {
                                yy = yy + 10;
                                g.DrawLine(pen, new System.Drawing.Point(20, yy), new System.Drawing.Point(800, yy));
                                yy = yy + 10;
                            }
                            else
                            {
                                yy = yy + 40;
                            }
                            Dexist = 1;
                            e.Graphics.DrawString("Investigation", printFont2, Brushes.Black, 14, yy);
                            e.Graphics.DrawString("o", printFont2, Brushes.Black, 200, yy);
                            e.Graphics.DrawString(dt_cf_investigation.Rows[0]["investigation_id"].ToString(), printFont2, Brushes.Black, 220, yy);
                            yy = yy + 20;
                            for (int k = 1; k < dt_cf_investigation.Rows.Count; k++)
                            {
                                e.Graphics.DrawString("o", printFont2, Brushes.Black, 200, yy);
                                e.Graphics.DrawString(dt_cf_investigation.Rows[k]["investigation_id"].ToString(), printFont2, Brushes.Black, 220, yy);
                                yy = yy + 20;
                            }
                        }
                        // diagnosis
                        System.Data.DataTable dt_cf_diagnosis = this.cntrl.dt_cf_diagnosis(clinic_id);
                        if (dt_cf_diagnosis.Rows.Count > 0)
                        {
                            if (Dexist == 1)
                            {
                                yy = yy + 10;
                                g.DrawLine(pen, new System.Drawing.Point(20, yy), new System.Drawing.Point(800, yy));
                                yy = yy + 10;
                            }
                            else
                            {
                                yy = yy + 40;
                            }
                            Dexist = 1;
                            Dexist = 1;
                            e.Graphics.DrawString("Diagnosis", printFont2, Brushes.Black, 14, yy);
                            e.Graphics.DrawString("o", printFont2, Brushes.Black, 200, yy);
                            e.Graphics.DrawString(dt_cf_diagnosis.Rows[0]["diagnosis_id"].ToString(), printFont2, Brushes.Black, 220, yy);
                            yy = yy + 20;
                            for (int k = 1; k < dt_cf_diagnosis.Rows.Count; k++)
                            {
                                e.Graphics.DrawString("o", printFont2, Brushes.Black, 200, yy);
                                e.Graphics.DrawString(dt_cf_diagnosis.Rows[k]["diagnosis_id"].ToString(), printFont2, Brushes.Black, 220, yy);
                                yy = yy + 20;
                            }
                        }
                        //Note
                        System.Data.DataTable dt_cf_note = this.cntrl.dt_cf_note(clinic_id);
                        if (dt_cf_note.Rows.Count > 0)
                        {
                            if (Dexist == 1)
                            {
                                yy = yy + 10;
                                g.DrawLine(pen, new System.Drawing.Point(20, yy), new System.Drawing.Point(800, yy));
                                yy = yy + 10;
                            }
                            else
                            {
                                yy = yy + 40;
                            }
                            Dexist = 1;
                            e.Graphics.DrawString("Notes", printFont2, Brushes.Black, 14, yy);
                            e.Graphics.DrawString("o", printFont2, Brushes.Black, 200, yy);
                            e.Graphics.DrawString(dt_cf_note.Rows[0]["note_name"].ToString(), printFont2, Brushes.Black, 220, yy);
                            yy = yy + 20;
                            for (int k = 0; k < dt_cf_note.Rows.Count; k++)
                            {
                                e.Graphics.DrawString("o", printFont2, Brushes.Black, 200, yy);
                                e.Graphics.DrawString(dt_cf_note.Rows[k]["note_name"].ToString(), printFont2, Brushes.Black, 220, yy);
                                yy = yy + 20;
                            }
                        }
                    }
                    e.Graphics.DrawString("Generated On: " + DateTime.Today.ToString("dd MMM yyyy"), printFont, Brushes.Black, 14, 1000);
                    e.Graphics.DrawString("Page 1 of 1 ", printFont, Brushes.Black, 300, 1000);
                    e.Graphics.DrawString("Powered by Pappyjoe", printFont, Brushes.Black, 670, 1000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure you want copy this clinical findings..??", "Copy...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res==DialogResult.Yes)
            {
                int treat;
                if (clinic_id!="0")
                {
                    DataTable dt_clncl_findings = this.cntrl.dt_cf(clinic_id);
                    if (dt_clncl_findings.Rows.Count>0)
                    {
                        this.cntrl.insertInto_clinical_findings(dt_clncl_findings.Rows[0]["pt_id"].ToString(), dt_clncl_findings.Rows[0]["dr_id"].ToString(), DateTime.Now.ToString("yyyy-MM-dd")); //dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                        string treatment = this.cntrl.MaxId_clinic_findings();
                        if (Convert.ToInt32(treatment) > 0)
                        {
                            treat = int.Parse(treatment);
                        }
                        else
                        {
                            treat = 1;
                        }
                        System.Data.DataTable dt_cf_Complaints = this.cntrl.dt_cf_Complaints(clinic_id);
                        if (dt_cf_Complaints.Rows.Count>0)
                        {
                            for (int i = 0; i < dt_cf_Complaints.Rows.Count; i++)
                            {
                                this.cntrl.insrtto_chief_comp(treat,dt_cf_Complaints.Rows[i]["complaint_id"].ToString());
                            }
                        }
                        System.Data.DataTable dt_cf_observe = this.cntrl.dt_cf_observe(clinic_id);
                        if (dt_cf_observe.Rows.Count>0)
                        {
                            for (int i = 0; i < dt_cf_observe.Rows.Count; i++)
                            {
                                this.cntrl.insrtto_obser(treat, dt_cf_observe.Rows[i]["observation_id"].ToString());
                            }
                        }
                        System.Data.DataTable dt_cf_investigation = this.cntrl.dt_cf_investigation(clinic_id);
                        if (dt_cf_investigation.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt_cf_investigation.Rows.Count; i++)
                            {
                                this.cntrl.insrtto_investi(treat, dt_cf_investigation.Rows[i]["investigation_id"].ToString());
                            }
                        }
                        System.Data.DataTable dt_cf_diagnosis = this.cntrl.dt_cf_diagnosis(clinic_id);
                        if (dt_cf_diagnosis.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt_cf_diagnosis.Rows.Count; i++)
                            {
                                this.cntrl.insrtto_diagno(treat, dt_cf_diagnosis.Rows[i]["diagnosis_id"].ToString());
                            }
                        }
                        System.Data.DataTable dt_cf_note = this.cntrl.dt_cf_note(clinic_id);
                        if (dt_cf_note.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt_cf_note.Rows.Count; i++)
                            {
                                this.cntrl.insrtto_note(treat, dt_cf_note.Rows[i]["note_name"].ToString());
                            }
                        }
                        ///coprrr
                        System.Data.DataTable dt_cf_allergy = this.cntrl.get_allergy(clinic_id);
                        if (dt_cf_allergy.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt_cf_allergy.Rows.Count; i++)
                            {
                                this.cntrl.insrtto_allergy(treat, dt_cf_allergy.Rows[i]["allergy_name"].ToString());
                            }
                        }
                        DataTable dt_details = this.cntrl.get_pt_meditation(clinic_id);
                        if (dt_details.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt_details.Rows.Count; i++)
                            {
                                //this.cntrl.insrtto_allergy(treat, dt_cf_note.Rows[i]["note_name"].ToString());
                                this.cntrl.insrtto_pt_meditation(treat, dt_details.Rows[i]["current_meditation"].ToString());
                            }
                        }
                        DataTable dt_advice = this.cntrl.get_pt_advice(clinic_id);
                        if (dt_advice.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt_advice.Rows.Count; i++)
                            {
                                //this.cntrl.insrtto_allergy(treat, dt_cf_note.Rows[i]["note_name"].ToString());
                                this.cntrl.insrtto_pt_advice(treat, dt_advice.Rows[i]["discharge_advice"].ToString());
                            }
                        }
                        DataTable dt_nurses_note = this.cntrl.get_nursenotes(clinic_id);
                        if (dt_nurses_note.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt_nurses_note.Rows.Count; i++)
                            {
                                //this.cntrl.insrtto_allergy(treat, dt_cf_note.Rows[i]["note_name"].ToString());
                                this.cntrl.insrtto_nurseNote(treat, dt_nurses_note.Rows[i]["nurses_note"].ToString());
                            }
                        }
                        //this.cntrl.save_medical(patient_id, dt_cf_allergy.Rows[i]["note_name"].ToString());
                    }
                }
            }
            fillgrid();
        }

        private void btn_SpecialCases_Click(object sender, EventArgs e)
        {
            var form2 = new PappyjoeMVC.View.Special_Cases();
            form2.doctor_id = doctor_id;
            form2.patient_id = patient_id;
            form2.ShowDialog();
            form2.Dispose();
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
            System.Data.DataTable dt_cf_main = this.cntrl.dt_cf_main_load_count(patient_id);
            if (dt_cf_main.Rows.Count > 0)
            {
                lb_clinical_cnt.Text = dt_cf_main.Rows.Count.ToString();
            }
            if(dt_cf_main.Rows.Count>10)
            {
                label1.Visible = true;
            }
            else
            {
                label1.Visible = false;
            }
        }
        public void treatment_count()
        {
            DataTable dtb = this.treatmnt_cntrl.Load_treatments_sidelabel_count(patient_id);
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
        public void nurse_count()
        {
            System.Data.DataTable dt_cf_main = this.nurse_cntrl.dt_cf_main(patient_id);
            if (dt_cf_main.Rows.Count > 0)
            {
                lb_Nurses_Notes.Text = dt_cf_main.Rows.Count.ToString();
            }
        }

        private void lbl_NursesNotes_Click(object sender, EventArgs e)
        {
            var form2 = new Nurses_Notes();
            form2.doctor_id = doctor_id;
            form2.patient_id = patient_id;
            openform(form2);

        }
        public int rowcount = 0;
        private void label1_Click(object sender, EventArgs e)
        {
            //rowcount = dataGridView1.Rows.Count;
            //if(Convert.ToInt32(lb_clinical_cnt.Text)>10)
            //{
                int count = rowcount + 10;
            fillgrid_count(count);// fillgrid(count);
                rowcount = count;
            //}
           
        }
    }
}
