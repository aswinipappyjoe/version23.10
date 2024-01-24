using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

using MySql.Data.MySqlClient;
using PappyjoeMVC.Controller;
using PappyjoeMVC.Model;
namespace PappyjoeMVC.View
{
    public partial class Fasttrack_window_New : Form
    {
        public event child_form Appointment_list;
        public delegate void child_form(string strappointment);
        fasttrack_model cntrl = new fasttrack_model();
        Add_Vital_Signs_model vmdl = new Add_Vital_Signs_model();
        Clinical_Notes_Add_model cmdl = new Clinical_Notes_Add_model();
        Common_model model = new Common_model();
        Add_Treatmentplan_model _Model = new Add_Treatmentplan_model();
        Invoice_controller invntrl = new Invoice_controller();
        Add_Finished_Treatment_model fmodel = new Add_Finished_Treatment_model();
        Add_Invoice_model amodel = new Add_Invoice_model();
        Prescription_Add_controller pcntrl = new Prescription_Add_controller();
        public string doctor_id = "0", patient_id = "",patname="", loginid = "0",clinic_id="",vital_id="",treamt_id,pres_id="";
        public static bool flag = false; double weight;
        double height;
        string gender; public bool load_flag = false;
        public Fasttrack_window_New()
        {
            InitializeComponent();
            g = pb_main.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pen = new Pen(Color.Black, 2);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;//
        }

        public Fasttrack_window_New(bool clinicprint1, bool vitalprint1, bool presprint1, bool treatprint1)
        {
            InitializeComponent();
            clinic_print = clinicprint1;
           vital_print = vitalprint1;
            pres_print = presprint1;
            treat_print = treatprint1;
        }

        public Fasttrack_window_New(string text, string id)
        {
            InitializeComponent();
            patname = text;
            newptid = id;
        }

        //private void btn_change_patients_Click(object sender, EventArgs e)
        //{

        //}

        private void txt_search_Click(object sender, EventArgs e)
        {
            if (txt_search.Text == "Search by Patient Id, Name")
            {
                txt_search.Text = "";
                txt_search.ForeColor = Color.DarkBlue;
            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (flag == false)
            {
                if (txt_search.Text != "")
                {
                    DataTable dtdr = this.cntrl.Patient_search(txt_search.Text);
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

        private void listpatientsearch_MouseClick(object sender, MouseEventArgs e)
        {
            string pt_id = listpatientsearch.SelectedValue.ToString();
            listpatientsearch.Visible = false;
            DataTable dtb = this.cntrl.get_pt_details(pt_id);
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
                btn_grp_by_visit_Click(null,null);
            }
        }

        public string strApp_id = "";

        private void Fasttrack_window_New_Load(object sender, EventArgs e)
        {
           try
            {
                btn_newPatient.Enabled = true;
                btn_change_patients.Enabled = true;
                load_flag = true;
                cmb_temp.SelectedIndex = 0;
                pat_name.Visible = false;
                label13.Visible = false;
                label29.Visible = false;
                label7.Visible = false;
                lb_gender.Visible = false;
                label14.Visible = false;
                lb_mobile.Visible = false;
                label15.Visible = false;
                pat_name.Visible = false;
                DataTable catgry = this.cnt_atta.GetCategory();
                    GetCategory(catgry);
                if (rjCombo_strength.Items.Count == 0)
                {
                    DataTable dt = model.Fill_unit_combo();
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
                rjCmb_duration.SelectedIndex = 0;
                rjCombo_strength.SelectedIndex = 0;
                if (strApp_id != "")
                {
                    btn_newPatient.Enabled = false;
                    btn_change_patients.Enabled = false;
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
                    if (dt_doctr.Rows.Count > 0)
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
            catch (Exception ex)
            {

            }
        }

    
        public void fill_grp_by_visit(int count)
        {
            if (patient_id != "")
            {
              
                DataTable dt_appoint = this.cntrl.dt_appointments_showmore(patient_id, count);
                if (dt_appoint.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt_appoint.Rows)
                    {
                        Label lb = new Label();
                        lb.Text = String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(dr["start_datetime"].ToString()));
                        lb.BackColor = Color.FromArgb(51, 187, 255); ;// FromArgb(209, 226, 237);
                        lb.ForeColor = Color.White;
                        lb.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                        lb.Location = new Point(x, y);
                        lb.Width = 559;
                        lb.Height = 28;
                        this.flowLayoutPanel2.Controls.Add(lb);
                        string date = Convert.ToDateTime(dr["start_datetime"].ToString()).ToString("yyyy/MM/dd");
                        DataTable dt_vitals = this.cntrl.vitals_grp_visit_main(patient_id, date);
                        if (dt_vitals.Rows.Count > 0)
                        {
                            Label lbl = new Label();
                            lbl.Text = "Vital Signs";
                            lbl.BackColor = Color.White;
                            lbl.ForeColor = Color.Navy;
                            lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                            this.flowLayoutPanel2.Controls.Add(lbl);
                            DataGridView dgv_vitals = new DataGridView();
                            dgv_vitals.BackgroundColor = Color.White;
                            dgv_vitals.Width = 559;
                            dgv_vitals.ColumnCount = 3;
                            dgv_vitals.Columns[0].Width = 200;
                            dgv_vitals.Columns[1].Width = 10;
                            dgv_vitals.Columns[2].Width = 225;
                            dgv_vitals.ColumnHeadersVisible = false;
                            dgv_vitals.RowHeadersVisible = false;
                            dgv_vitals.EnableHeadersVisualStyles = false;
                            dgv_vitals.BorderStyle = BorderStyle.Fixed3D;
                            dgv_vitals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                            dgv_vitals.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                            dgv_vitals.CellBorderStyle = DataGridViewCellBorderStyle.None;
                            this.flowLayoutPanel2.Controls.Add(dgv_vitals);// panel6.Controls.Add(dgv_vitals);
                            dgv_vitals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                            dgv_vitals.AllowUserToAddRows = false;
                            dgv_vitals.AllowUserToResizeRows = false;
                            dgv_vitals.AllowUserToDeleteRows = false;
                            dgv_vitals.AllowUserToOrderColumns = false; dgv_vitals.ReadOnly = true;
                            dgv_vitals.AllowUserToResizeColumns = false;
                            dgv_vitals.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            dgv_vitals.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            int ri = 0;
                            for (int j1 = 0; j1 < dt_vitals.Rows.Count; j1++)
                            {

                                if (dt_vitals.Rows[j1]["pulse"].ToString() != "")
                                {
                                    dgv_vitals.Rows.Add("PULSE ", ":", dt_vitals.Rows[j1]["pulse"].ToString()+ "(Heart Beats Per Minute)");
                                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                    dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                    dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    ri = ri + 1;
                                }
                                if (dt_vitals.Rows[j1]["temp"].ToString() != "")
                                {
                                    dgv_vitals.Rows.Add("TEMPERATURE ", ":", dt_vitals.Rows[j1]["temp"].ToString() + " ( " + dt_vitals.Rows[j1]["temp_type"].ToString() + " ) ");
                                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                    dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                    dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    ri = ri + 1;
                                }
                                if (dt_vitals.Rows[j1]["bp_syst"].ToString() != "")
                                {
                                    dgv_vitals.Rows.Add("BLOOD PRESSURE ( SYSTOLIC ) ", ":", dt_vitals.Rows[j1]["bp_syst"].ToString() + " ( " + dt_vitals.Rows[j1]["bp_type"].ToString() + " ) ");
                                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                    dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                    dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    ri = ri + 1;
                                }
                                if (dt_vitals.Rows[j1]["bp_dia"].ToString() != "")
                                {
                                    dgv_vitals.Rows.Add("BLOOD PRESSURE ( DIASTOLIC )  ", ":", dt_vitals.Rows[j1]["bp_dia"].ToString() + " ( " + dt_vitals.Rows[j1]["bp_type"].ToString() + " ) ");
                                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                    dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                    dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    ri = ri + 1;
                                }
                                if (dt_vitals.Rows[j1]["Height"].ToString() != "")
                                {
                                    dgv_vitals.Rows.Add("HEIGHT  ", ":", dt_vitals.Rows[j1]["Height"].ToString() + "(Cm)");
                                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                    dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                    dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    ri = ri + 1;
                                }
                                if (dt_vitals.Rows[j1]["weight"].ToString() != "")
                                {
                                    dgv_vitals.Rows.Add("WEIGHT  ", ":", dt_vitals.Rows[j1]["weight"].ToString() + "(Kg)");
                                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                    dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                    dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    ri = ri + 1;
                                }
                                if (dt_vitals.Rows[j1]["resp"].ToString() != "")
                                {
                                    dgv_vitals.Rows.Add("RESPIRATORY RATE ", ":", dt_vitals.Rows[j1]["resp"].ToString()+ "(Breaths/min)");
                                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                    dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                    dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    ri = ri + 1;
                                }
                                if (dt_vitals.Rows[j1]["weight"].ToString() != null && dt_vitals.Rows[j1]["weight"].ToString() != "" && dt_vitals.Rows[j1]["Height"].ToString() != null && dt_vitals.Rows[j1]["Height"].ToString() != "")
                                {
                                    weight = Convert.ToDouble(dt_vitals.Rows[j1]["weight"].ToString());
                                    height = Convert.ToDouble(dt_vitals.Rows[j1]["Height"].ToString());
                                }
                                else
                                {
                                    weight = Convert.ToDouble("0.00");
                                    height = Convert.ToDouble("0.00");
                                }
                                gender = lb_gender.Text;
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
                                        dgv_vitals.Rows.Add("BMI(BOADY MASS INDEX) ", ":", BMI + "  ,   " + msg);
                                       
                                        if (msg == "BMI is low")
                                            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.Red;
                                        else if (msg == "Normal")
                                            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                        else if (msg == "BMI is High")
                                            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.Red;
                                        dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                        dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                        dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                        dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                        ri = ri + 1;
                                    }
                                }
                                if (dt_vitals.Rows[j1]["spo"].ToString() != "")
                                {
                                    dgv_vitals.Rows.Add("SPO2 ", ":", dt_vitals.Rows[j1]["spo"].ToString()+"%");
                                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                    dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                    dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    ri = ri + 1;
                                }
                                dgv_vitals.Rows.Add("", "", ""); ri = ri + 1;
                                dgv_vitals.Rows.Add("Recorded By : Dr." + dt_vitals.Rows[j1]["dr_name"].ToString(), "", "");
                                dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.Red;
                                dgv_vitals.Rows[ri ].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Italic);
                            }
                            int heignht = GetDataGridViewHeight(dgv_vitals);
                            dgv_vitals.Height = heignht;
                        }
                        else
                        {
                          
                        }
                        // clinic
                        DataTable dt_cf_main = this.cntrl.dt_clinic_main_grp_visi(patient_id, date);
                        int i = 0;
                        if (dt_cf_main.Rows.Count > 0)
                        {
                            Label lbl = new Label();
                            lbl.Text = "Clinical Findings";
                            lbl.Width = 250;
                            lbl.BackColor = Color.FromArgb(209, 226, 237);
                            lbl.ForeColor = Color.Navy;
                            lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                            this.flowLayoutPanel2.Controls.Add(lbl);
                            DataGridView dgv_clinic = new DataGridView();
                            dgv_clinic.Width = 559;
                            dgv_clinic.ColumnCount = 4;
                            dgv_clinic.Columns[0].Width = 20;
                            dgv_clinic.Columns[1].Width = 150;
                            dgv_clinic.Columns[2].Width = 1;
                            dgv_clinic.Columns[3].Width = 300;
                            dgv_clinic.BackgroundColor = Color.White;
                            dgv_clinic.EnableHeadersVisualStyles = false;
                            dgv_clinic.BorderStyle = BorderStyle.Fixed3D;
                            dgv_clinic.Enabled = false;
                            dgv_clinic.CellBorderStyle = DataGridViewCellBorderStyle.None;
                            this.flowLayoutPanel2.Controls.Add(dgv_clinic);
                            DataGridViewColumn column = dgv_clinic.Columns[3];
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            dgv_clinic.ReadOnly = true;                     
                            dgv_clinic.ColumnHeadersVisible = false;
                            dgv_clinic.RowHeadersVisible = false;
                            dgv_clinic.AllowUserToAddRows = false;
                            dgv_clinic.AllowUserToResizeRows = false;
                            dgv_clinic.AllowUserToDeleteRows = false;
                            dgv_clinic.AllowUserToOrderColumns = false;
                            dgv_clinic.AllowUserToResizeColumns = false;
                            for (int jt = 0; jt < dt_cf_main.Rows.Count; jt++)
                            {
                                string heading = "";
                                DataTable dt_clinic = this.cntrl.dt_cf_Complaints(dt_cf_main.Rows[0]["id"].ToString());
                                if (dt_clinic.Rows.Count > 0)
                                {
                                    heading = "Complaints";
                                    foreach (DataRow drr in dt_clinic.Rows)
                                    {
                                        dgv_clinic.Rows.Add("0", heading, "", drr["complaint_id"].ToString());
                                        heading = "";
                                        dgv_clinic.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                        dgv_clinic.Rows[i].Cells[1].Style.ForeColor = Color.DarkBlue;
                                        dgv_clinic.Rows[i].Cells[3].Style.ForeColor = Color.DarkBlue;
                                        dgv_clinic.Columns[0].Visible = false;
                                        i = i + 1;
                                    }
                                    dgv_clinic.Rows.Add("", "", "", "");
                                    dgv_clinic.Rows[i].Height = 1;
                                    dgv_clinic.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                                    i = i + 1;
                                }
                                System.Data.DataTable dt_cf_diagnosis = this.cntrl.dt_cf_diagnosis(dt_cf_main.Rows[jt]["id"].ToString());
                                if (dt_cf_diagnosis.Rows.Count > 0)
                                {
                                    heading = "Diagnosis";
                                    foreach (DataRow drr in dt_cf_diagnosis.Rows)
                                    {
                                        //i = i + 1;
                                        dgv_clinic.Rows.Add("0", heading, "", drr["diagnosis_id"].ToString());
                                        heading = "";
                                        dgv_clinic.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                        dgv_clinic.Rows[i].Cells[1].Style.ForeColor = Color.DarkBlue;
                                        dgv_clinic.Rows[i].Cells[3].Style.ForeColor = Color.DarkBlue;
                                        dgv_clinic.Columns[0].Visible = false;
                                        i = i + 1;
                                    }
                                    dgv_clinic.Rows.Add("", "", "", "");
                                    dgv_clinic.Rows[i].Height = 1;
                                    dgv_clinic.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                                    i = i + 1;
                                }
                                System.Data.DataTable dt_cf_note = this.cntrl.dt_cf_note(dt_cf_main.Rows[jt]["id"].ToString());
                                if (dt_cf_note.Rows.Count > 0)
                                {
                                    heading = "Notes";
                                    foreach (DataRow drr in dt_cf_note.Rows)
                                    {
                                        dgv_clinic.Rows.Add("0", heading, "", drr["note_name"].ToString());
                                        heading = "";
                                        dgv_clinic.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                        dgv_clinic.Rows[i].Cells[1].Style.ForeColor = Color.DarkBlue;
                                        dgv_clinic.Rows[i].Cells[3].Style.ForeColor = Color.DarkBlue;
                                        dgv_clinic.Columns[0].Visible = false;
                                        i = i + 1;
                                    }
                                }
                            }
                            int heignht = GetDataGridViewHeight(dgv_clinic);
                            dgv_clinic.Height = heignht;
                        }
                        // Treatment
                        DataTable dt_treatmnt = this.cntrl.Load_treatments(patient_id, date);
                        int j = 0;
                        if (dt_treatmnt.Rows.Count > 0)
                        {
                            Label lbl = new Label();
                            lbl.Text = "Treatments";
                            lbl.BackColor = Color.White;
                            lbl.ForeColor = Color.Navy;
                            lbl.Location = new Point(x, y);
                            lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                            this.flowLayoutPanel2.Controls.Add(lbl);
                            DataGridView dataGridView1_treatment_paln = new DataGridView();
                            dataGridView1_treatment_paln.BackgroundColor = Color.White;
                            dataGridView1_treatment_paln.Width = 559;
                            dataGridView1_treatment_paln.ColumnHeadersVisible = true;
                            dataGridView1_treatment_paln.Columns.Add("id", "ID");
                            dataGridView1_treatment_paln.Columns.Add("TREATMENTS", "TREATMENTS");
                            dataGridView1_treatment_paln.Columns.Add("COST", "COST");
                            dataGridView1_treatment_paln.Columns.Add("DISCOUNT", "DISCOUNT");
                            dataGridView1_treatment_paln.Columns.Add("TOTAL", "TOTAL");
                            dataGridView1_treatment_paln.Columns.Add("NOTE", "NOTE");
                            dataGridView1_treatment_paln.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 51, 102);
                            dataGridView1_treatment_paln.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                            dataGridView1_treatment_paln.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; ;
                            dataGridView1_treatment_paln.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dataGridView1_treatment_paln.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dataGridView1_treatment_paln.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                            dataGridView1_treatment_paln.Columns[0].Width = 5;
                            dataGridView1_treatment_paln.Columns[1].Width = 174;
                            dataGridView1_treatment_paln.Columns[2].Width = 81;
                            dataGridView1_treatment_paln.Columns[3].Width = 121;
                            dataGridView1_treatment_paln.Columns[4].Width = 90;
                            dataGridView1_treatment_paln.Columns[5].Width = 70;
                            dataGridView1_treatment_paln.Columns[0].Visible = false;
                            dataGridView1_treatment_paln.RowHeadersVisible = false;
                            dataGridView1_treatment_paln.EnableHeadersVisualStyles = false;
                            dataGridView1_treatment_paln.BorderStyle = BorderStyle.Fixed3D;
                            dataGridView1_treatment_paln.CellBorderStyle = DataGridViewCellBorderStyle.None;
                            dataGridView1_treatment_paln.Location = new Point(x, y);
                            this.flowLayoutPanel2.Controls.Add(dataGridView1_treatment_paln);
                            dataGridView1_treatment_paln.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                            dataGridView1_treatment_paln.ReadOnly = true;
                            dataGridView1_treatment_paln.RowHeadersVisible = false;
                            dataGridView1_treatment_paln.AllowUserToAddRows = false;
                            dataGridView1_treatment_paln.AllowUserToResizeRows = false;
                            dataGridView1_treatment_paln.AllowUserToDeleteRows = false;
                            dataGridView1_treatment_paln.AllowUserToOrderColumns = false;
                            dataGridView1_treatment_paln.AllowUserToResizeColumns = false;
                            DataTable dt_pt_sub = this.cntrl.treatment_sub_details(dt_treatmnt.Rows[0]["id"].ToString());
                            if (dt_pt_sub.Rows.Count > 0)
                            {
                                Double totalEst = 0;

                                for (int k = 0; k < dt_pt_sub.Rows.Count; k++)
                                {
                                    string discount_string = "";
                                    if (dt_pt_sub.Rows[k]["discount_type"].ToString() == "INR")
                                    {
                                        discount_string = "";
                                    }
                                    else
                                    {
                                        discount_string = "(" + dt_pt_sub.Rows[k]["discount"].ToString() + "%)";
                                    }
                                    Decimal totalcost = Convert.ToDecimal(dt_pt_sub.Rows[k]["cost"].ToString()) * Convert.ToDecimal(dt_pt_sub.Rows[k]["quantity"].ToString());
                                    dataGridView1_treatment_paln.Rows.Add(dt_pt_sub.Rows[k]["id"].ToString(), dt_pt_sub.Rows[k]["procedure_name"].ToString(), String.Format("{0:C2}", Convert.ToDecimal(totalcost)), String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discount_inrs"].ToString())) + discount_string, String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString())), dt_pt_sub.Rows[k]["note"].ToString() + " " + dt_pt_sub.Rows[k]["tooth"].ToString());
                                    dataGridView1_treatment_paln.Rows[j].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                    dataGridView1_treatment_paln.Rows[j].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                    dataGridView1_treatment_paln.Rows[j].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                    totalEst = totalEst + Convert.ToDouble(dt_pt_sub.Rows[k]["total"].ToString());
                                    j++;
                                }
                                    dataGridView1_treatment_paln.Rows.Add("", "Planned by " + dt_treatmnt.Rows[0]["dr_name"].ToString(), "", "Estimated amount:", String.Format("{0:C2}", Convert.ToDecimal(totalEst)));
                                    dataGridView1_treatment_paln.Rows[j].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dataGridView1_treatment_paln.Rows[j].Cells[3].Style.ForeColor = Color.Red;
                                dataGridView1_treatment_paln.Rows[j].Cells[4].Style.ForeColor = Color.Red;
                                dataGridView1_treatment_paln.Rows[j].Cells[1].Style.ForeColor = Color.Red;
                                dataGridView1_treatment_paln.Rows[j].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                dataGridView1_treatment_paln.Rows[j].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                dataGridView1_treatment_paln.Rows[j].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                    j = j + 1;
                                    dataGridView1_treatment_paln.Rows.Add("", "", "", "", "", "");
                                    j = j + 1;
                                int heignht = GetDataGridViewHeight(dataGridView1_treatment_paln);
                                dataGridView1_treatment_paln.Height = heignht;
                            }
                        }
                        //prescription
                        DataTable dt_pre_main = this.cntrl.Get_maindtails(patient_id, date);
                        int p = 0;
                        if (dt_pre_main.Rows.Count > 0)
                        {
                            Label lbl = new Label();
                            lbl.Text = "Prescription";
                            lbl.BackColor = Color.White;
                            lbl.ForeColor = Color.Navy;
                            lbl.Location = new Point(x, y);
                            lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                            this.flowLayoutPanel2.Controls.Add(lbl);
                            DataGridView dataGridView1 = new DataGridView();
                            dataGridView1.BackgroundColor = Color.White;
                            dataGridView1.Width = 559;
                            dataGridView1.ColumnHeadersVisible = true;
                            dataGridView1.Columns.Add("id", "ID");
                            dataGridView1.Columns.Add("DRUG", "DRUG");
                            dataGridView1.Columns.Add("FREQUENCY", "FREQUENCY");
                            dataGridView1.Columns.Add("DURATION", "DURATION");
                            dataGridView1.Columns.Add("INSTRUCTION", "INSTRUCTION");
                            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 51, 102);
                            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                            dataGridView1.Columns[0].Width = 10;
                            dataGridView1.Columns[1].Width = 225;
                            dataGridView1.Columns[2].Width = 100;
                            dataGridView1.Columns[3].Width = 100;
                            dataGridView1.Columns[4].Width = 120;
                            dataGridView1.RowHeadersVisible = false;
                            dataGridView1.EnableHeadersVisualStyles = false;
                            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
                            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
                            dataGridView1.Location = new Point(x, y);
                            dataGridView1.Columns[0].Visible = false;
                            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;//.None;
                            this.flowLayoutPanel2.Controls.Add(dataGridView1);
                            dataGridView1.ReadOnly = true;
                            dataGridView1.RowHeadersVisible = false;
                            dataGridView1.AllowUserToAddRows = false;
                            dataGridView1.AllowUserToResizeRows = false;
                            dataGridView1.AllowUserToDeleteRows = false;
                            dataGridView1.AllowUserToOrderColumns = false;
                            dataGridView1.AllowUserToResizeColumns = false;
                            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                            dataGridView1.Columns[0].Visible = false;
                            System.Data.DataTable dt_prescription = this.cntrl.prescription_detoails(dt_pre_main.Rows[0]["id"].ToString());
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
                                        dataGridView1.Rows.Add("0", dt_prescription.Rows[k]["drug_type"].ToString() + " " + dt_prescription.Rows[k]["drug_name"].ToString() + " " + dt_prescription.Rows[k]["strength"].ToString() + " " + "", "", duration, dt_prescription.Rows[k]["add_instruction"].ToString());
                                    }
                                    else
                                    {
                                        dataGridView1.Rows.Add("0", dt_prescription.Rows[k]["drug_type"].ToString() + " " + dt_prescription.Rows[k]["drug_name"].ToString() + " " + dt_prescription.Rows[k]["strength"].ToString() + " " + dt_prescription.Rows[k]["strength_gr"].ToString(), morning + " - " + noon + " - " + night, duration, dt_prescription.Rows[k]["add_instruction"].ToString());
                                    }
                                    dataGridView1.Rows[p].Height = 30;
                                    p++;
                                }
                                dataGridView1.Rows.Add("0", "Prescribed by Dr." + dt_pre_main.Rows[0]["doctor_name"].ToString(), "", "");
                                dataGridView1.Rows[p].Cells[1].Style.ForeColor = Color.Red;
                                dataGridView1.Rows[p].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                                p = p + 1;
                                dataGridView1.Rows.Add("0", "", "", "");
                                p = p + 1;
                            }

                            int heignht = GetDataGridViewHeight(dataGridView1);
                            dataGridView1.Height = heignht;
                        }
                    }
                }
                else
                {
                    lb_showmore.Visible = false;
                }

            }
        }
        public int v_hignt = 0, c_hight = 0, tre_hight = 0, pre_height = 0,vital=0,clinic=0,treat=0,pres=0;
        public void grp_by_para_showmore(int count)
        { 
            DataTable dt_vitals = new DataTable();
             dt_vitals = this.cntrl.vitals_grp_visit_para_showmore_count(patient_id, count);
            if (dt_vitals.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_vitals.Rows)
                {
                    Label lb = new Label();
                    lb.Text = String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(dr["date"].ToString()));
                    lb.BackColor = Color.White;// FromArgb(209, 226, 237);
                    lb.ForeColor = Color.Navy;
                    lb.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                    lb.Width = 260;
                    lb.Height = 30;
                    this.flowLayoutPanel2.Controls.Add(lb);// panel6.Controls.Add(lb);
                    this.flowLayoutPanel2.Controls.SetChildIndex(lb, v_hignt);
                    v_hignt = v_hignt + 1;
                    DataGridView dgv_vitals = new DataGridView();
                    dgv_vitals.BackgroundColor = Color.White;
                    dgv_vitals.Width = 559;
                    dgv_vitals.ColumnCount = 3;
                    dgv_vitals.Columns[0].Width = 200;
                    dgv_vitals.Columns[1].Width = 10;
                    dgv_vitals.Columns[2].Width = 225;
                    dgv_vitals.ColumnHeadersVisible = false;
                    dgv_vitals.RowHeadersVisible = false;
                    dgv_vitals.EnableHeadersVisualStyles = false;
                    dgv_vitals.BorderStyle = BorderStyle.Fixed3D;
                    dgv_vitals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    dgv_vitals.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                    dgv_vitals.CellBorderStyle = DataGridViewCellBorderStyle.None;
                    this.flowLayoutPanel2.Controls.Add(dgv_vitals);
                    this.flowLayoutPanel2.Controls.SetChildIndex(dgv_vitals, v_hignt);
                    v_hignt = v_hignt + 1;
                    dgv_vitals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgv_vitals.AllowUserToAddRows = false;
                    dgv_vitals.AllowUserToResizeRows = false;
                    dgv_vitals.AllowUserToDeleteRows = false;
                    dgv_vitals.AllowUserToOrderColumns = false; 
                    dgv_vitals.ReadOnly = true;
                    dgv_vitals.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv_vitals.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    int ri = 0;
                    if (dr["pulse"].ToString() != "")
                    {
                        dgv_vitals.Rows.Add("PULSE ", ":", dr["pulse"].ToString() + "(Heart Beats Per Minute)");
                        dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                        dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                        dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                        dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                        ri = ri + 1;
                    }
                    if (dr["temp"].ToString() != "")
                    {
                        dgv_vitals.Rows.Add("TEMPERATURE ", ":", dr["temp"].ToString() + " ( " + dr["temp_type"].ToString() + " ) ");
                        dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                        dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                        dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                        dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                        ri = ri + 1;
                    }
                    if (dr["bp_syst"].ToString() != "")
                    {
                        dgv_vitals.Rows.Add("BLOOD PRESSURE ( SYSTOLIC ) ", ":", dr["bp_syst"].ToString() + " ( " + dr["bp_type"].ToString() + " ) ");
                        dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                        dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                        dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                        dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                        ri = ri + 1;
                    }
                    if (dr["bp_dia"].ToString() != "")
                    {
                        dgv_vitals.Rows.Add("BLOOD PRESSURE ( DIASTOLIC )  ", ":", dr["bp_dia"].ToString() + " ( " + dr["bp_type"].ToString() + " ) ");
                        dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                        dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                        dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                        dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                        ri = ri + 1;
                    }
                    if (dr["Height"].ToString() != "")
                    {
                        dgv_vitals.Rows.Add("HEIGHT  ", ":", dr["Height"].ToString() + "(Cm)");
                        dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                        dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                        dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                        dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                        ri = ri + 1;
                    }
                    if (dr["weight"].ToString() != "")
                    {
                        dgv_vitals.Rows.Add("WEIGHT  ", ":", dr["weight"].ToString() + "(Kg)");
                        dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                        dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                        dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                        dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                        ri = ri + 1;
                    }
                    if (dr["resp"].ToString() != "")
                    {
                        dgv_vitals.Rows.Add("RESPIRATORY RATE ", ":", dr["resp"].ToString() + "(Breaths/min)");
                        dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                        dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                        dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                        dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                        ri = ri + 1;
                    }
                    if (dr["weight"].ToString() != null && dr["weight"].ToString() != "" && dr["Height"].ToString() != null && dr["Height"].ToString() != "")
                    {
                        weight = Convert.ToDouble(dr["weight"].ToString());
                        height = Convert.ToDouble(dr["Height"].ToString());
                    }
                    else
                    {
                        weight = Convert.ToDouble("0.00");
                        height = Convert.ToDouble("0.00");
                    }
                    gender = lb_gender.Text;
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
                            dgv_vitals.Rows.Add("BMI(BOADY MASS INDEX) ", ":", BMI + "  ,   " + msg);

                            if (msg == "BMI is low")
                                dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.Red;
                            else if (msg == "Normal")
                                dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                            else if (msg == "BMI is High")
                                dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.Red;
                            dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                            dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                            dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                            ri = ri + 1;
                        }
                    }
                    if (dr["spo"].ToString() != "")
                    {
                        dgv_vitals.Rows.Add("SPO2 ", ":", dr["spo"].ToString()+"%", "");
                        dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                        dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                        dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                        dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                        ri = ri + 1;
                    }
                    dgv_vitals.Rows.Add("","",""); ri = ri + 1;
                    dgv_vitals.Rows.Add("Recorded By : Dr." + dr["dr_name"].ToString(), "", "");
                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.Red;
                    dgv_vitals.Rows[ri].Cells[0]. Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Italic);
                    int heignht = GetDataGridViewHeight(dgv_vitals);
                    dgv_vitals.Height = heignht;
                }
                c_hight = v_hignt + 5;
            }
            //clinic
               DataTable dt_cf_main = this.cntrl.dt_clinic_main_grp_para_showcount(patient_id, count);
            if (dt_cf_main.Rows.Count > 0)
            {
            foreach (DataRow dtr in dt_cf_main.Rows)
            {
                int i = 0;
                Label lb = new Label();
                lb.Text = String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(dtr["date"].ToString()));
                lb.BackColor = Color.White;
                lb.ForeColor = Color.Navy;
                lb.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                lb.Width = 260;
                lb.Height = 30;
                    this.flowLayoutPanel2.Controls.Add(lb);
                    this.flowLayoutPanel2.Controls.SetChildIndex(lb, c_hight);
                    c_hight = c_hight + 1;
                    DataGridView dgv_clinic = new DataGridView();
                dgv_clinic.Width = 559;
                dgv_clinic.ColumnCount = 4;
                dgv_clinic.Columns[0].Width = 20;
                dgv_clinic.Columns[1].Width = 150;
                dgv_clinic.Columns[2].Width = 1; dgv_clinic.Columns[3].Width = 250;
                dgv_clinic.BackgroundColor = Color.White;
                dgv_clinic.EnableHeadersVisualStyles = false;
                dgv_clinic.BorderStyle = BorderStyle.Fixed3D;
                dgv_clinic.Enabled = false;
                dgv_clinic.CellBorderStyle = DataGridViewCellBorderStyle.None;
                    this.flowLayoutPanel2.Controls.Add(dgv_clinic);
                    this.flowLayoutPanel2.Controls.SetChildIndex(dgv_clinic, c_hight);
                    c_hight = c_hight + 1;
                    DataGridViewColumn column = dgv_clinic.Columns[3];
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv_clinic.ColumnHeadersVisible = false;
                dgv_clinic.RowHeadersVisible = false;
                    dgv_clinic.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgv_clinic.AllowUserToAddRows = false;
                    dgv_clinic.AllowUserToResizeRows = false;
                    dgv_clinic.AllowUserToDeleteRows = false;
                    dgv_clinic.AllowUserToOrderColumns = false; dgv_clinic.ReadOnly = true;
                    string heading = "";
                DataTable dt_clinic = this.cntrl.dt_cf_Complaints(dtr["id"].ToString());
                if (dt_clinic.Rows.Count > 0)
                {
                    heading = "Complaints";
                    foreach (DataRow drr in dt_clinic.Rows)
                    {
                             
                        dgv_clinic.Rows.Add("0", heading, "", drr["complaint_id"].ToString());
                        heading = "";
                        dgv_clinic.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                        dgv_clinic.Rows[i].Cells[1].Style.ForeColor = Color.DarkBlue;
                        dgv_clinic.Rows[i].Cells[3].Style.ForeColor = Color.DarkBlue;
                        dgv_clinic.Columns[0].Visible = false;
                        i = i + 1;
                    }
                    //i = i + 1;
                    dgv_clinic.Rows.Add("", "", "", "");
                    dgv_clinic.Rows[i].Height = 1;
                    dgv_clinic.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;

                    i = i + 1;
                }
                System.Data.DataTable dt_cf_diagnosis = this.cntrl.dt_cf_diagnosis(dtr["id"].ToString());
                if (dt_cf_diagnosis.Rows.Count > 0)
                {
                    heading = "Diagnosis";
                    foreach (DataRow drr in dt_cf_diagnosis.Rows)
                    {
                        dgv_clinic.Rows.Add("0", heading, "", drr["diagnosis_id"].ToString());
                        heading = "";
                        dgv_clinic.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                        dgv_clinic.Rows[i].Cells[1].Style.ForeColor = Color.DarkBlue;
                        dgv_clinic.Rows[i].Cells[3].Style.ForeColor = Color.DarkBlue;
                        dgv_clinic.Columns[0].Visible = false;
                        i = i + 1;
                    }
                    dgv_clinic.Rows.Add("", "", "", "");
                    dgv_clinic.Rows[i].Height = 1;
                    dgv_clinic.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                    i = i + 1;
                }
                System.Data.DataTable dt_cf_note = this.cntrl.dt_cf_note(dtr["id"].ToString());
                if (dt_cf_note.Rows.Count > 0)
                {
                    heading = "Notes";
                    foreach (DataRow drr in dt_cf_note.Rows)
                    {
                        dgv_clinic.Rows.Add("0", heading, "", drr["note_name"].ToString());
                        heading = "";
                        dgv_clinic.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                        dgv_clinic.Rows[i].Cells[1].Style.ForeColor = Color.DarkBlue;
                        dgv_clinic.Rows[i].Cells[3].Style.ForeColor = Color.DarkBlue;
                        dgv_clinic.Columns[0].Visible = false;
                        i = i + 1;
                    }

                }
                    int heignht = GetDataGridViewHeight(dgv_clinic);
                    dgv_clinic.Height = heignht;
                   
                }
                tre_hight = c_hight + 5;
            }
        // treatments
        DataTable dt_treatmnt = new DataTable();
                dt_treatmnt = this.cntrl.Load_treatments_para_showmore(patient_id, count);
            if (dt_treatmnt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_treatmnt.Rows)
                {
                  
                    int j = 0;
                    Label lb = new Label();
                    lb.Text = String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(dr["date"].ToString()));
                    lb.BackColor = Color.White;
                    lb.ForeColor = Color.Navy;
                    lb.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    lb.Width = 260;
                    lb.Height = 30;
                    this.flowLayoutPanel2.Controls.Add(lb);
                    this.flowLayoutPanel2.Controls.SetChildIndex(lb, tre_hight);
                    tre_hight = tre_hight + 1;
                    DataGridView dataGridView1_treatment_paln = new DataGridView();
                    dataGridView1_treatment_paln.BackgroundColor = Color.White;
                    dataGridView1_treatment_paln.Width = 559;
                    dataGridView1_treatment_paln.ColumnHeadersVisible = true;
                    dataGridView1_treatment_paln.Columns.Add("id", "ID");
                    dataGridView1_treatment_paln.Columns.Add("TREATMENTS", "TREATMENTS");
                    dataGridView1_treatment_paln.Columns.Add("COST", "COST");
                    dataGridView1_treatment_paln.Columns.Add("DISCOUNT", "DISCOUNT");
                    dataGridView1_treatment_paln.Columns.Add("TOTAL", "TOTAL");
                    dataGridView1_treatment_paln.Columns.Add("NOTE", "NOTE");
                    dataGridView1_treatment_paln.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 51, 102);
                    dataGridView1_treatment_paln.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dataGridView1_treatment_paln.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; ;
                    dataGridView1_treatment_paln.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1_treatment_paln.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1_treatment_paln.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                    dataGridView1_treatment_paln.Columns[0].Width = 5;
                    dataGridView1_treatment_paln.Columns[1].Width = 174;
                    dataGridView1_treatment_paln.Columns[2].Width = 81;
                    dataGridView1_treatment_paln.Columns[3].Width = 121;
                    dataGridView1_treatment_paln.Columns[4].Width = 90;
                    dataGridView1_treatment_paln.Columns[5].Width = 70;
                    dataGridView1_treatment_paln.Columns[0].Visible = false;
                    dataGridView1_treatment_paln.RowHeadersVisible = false;
                    dataGridView1_treatment_paln.EnableHeadersVisualStyles = false;
                    dataGridView1_treatment_paln.BorderStyle = BorderStyle.Fixed3D;
                    dataGridView1_treatment_paln.CellBorderStyle = DataGridViewCellBorderStyle.None;
                    this.flowLayoutPanel2.Controls.Add(dataGridView1_treatment_paln);
                    this.flowLayoutPanel2.Controls.SetChildIndex(dataGridView1_treatment_paln, tre_hight);
                    tre_hight = tre_hight + 1;
                    dataGridView1_treatment_paln.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1_treatment_paln.ReadOnly = true;
                    dataGridView1_treatment_paln.RowHeadersVisible = false;
                    dataGridView1_treatment_paln.AllowUserToAddRows = false;
                    dataGridView1_treatment_paln.AllowUserToResizeRows = false;
                    dataGridView1_treatment_paln.AllowUserToDeleteRows = false;
                    dataGridView1_treatment_paln.AllowUserToOrderColumns = false;
                    dataGridView1_treatment_paln.AllowUserToResizeColumns = false;
                    DataTable dt_pt_sub = this.cntrl.treatment_sub_details(dr["id"].ToString());
                    if (dt_pt_sub.Rows.Count > 0)
                    {
                        Double totalEst = 0;
                        for (int k = 0; k < dt_pt_sub.Rows.Count; k++)
                        {
                            string discount_string = "";
                            if (dt_pt_sub.Rows[k]["discount_type"].ToString() == "INR")
                            {
                                discount_string = "";
                            }
                            else
                            {
                                discount_string = "(" + dt_pt_sub.Rows[k]["discount"].ToString() + "%)";
                            }
                            Decimal totalcost = Convert.ToDecimal(dt_pt_sub.Rows[k]["cost"].ToString()) * Convert.ToDecimal(dt_pt_sub.Rows[k]["quantity"].ToString());
                            dataGridView1_treatment_paln.Rows.Add(dt_pt_sub.Rows[k]["id"].ToString(), dt_pt_sub.Rows[k]["procedure_name"].ToString(), String.Format("{0:C2}", Convert.ToDecimal(totalcost)), String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discount_inrs"].ToString())) + discount_string, String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString())), dt_pt_sub.Rows[k]["note"].ToString() + " " + dt_pt_sub.Rows[k]["tooth"].ToString());
                            dataGridView1_treatment_paln.Rows[j].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dataGridView1_treatment_paln.Rows[j].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dataGridView1_treatment_paln.Rows[j].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                            totalEst = totalEst + Convert.ToDouble(dt_pt_sub.Rows[k]["total"].ToString());
                        }
                        j = j + 1;
                        dataGridView1_treatment_paln.Rows.Add("", "Planned by " + dt_treatmnt.Rows[0]["dr_name"].ToString(), "", "Estimated amount:", String.Format("{0:C2}", Convert.ToDecimal(totalEst)));
                            dataGridView1_treatment_paln.Rows[j].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Regular);
                            dataGridView1_treatment_paln.Rows[j].Cells[1].Style.ForeColor = Color.Red;
                            dataGridView1_treatment_paln.Rows[j].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Regular);
                            dataGridView1_treatment_paln.Rows[j].Cells[3].Style.ForeColor = Color.Red;
                            dataGridView1_treatment_paln.Rows[j].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Regular);
                            dataGridView1_treatment_paln.Rows[j].Cells[4].Style.ForeColor = Color.Red;
                            dataGridView1_treatment_paln.Rows[j].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                            j = j + 1;
                            dataGridView1_treatment_paln.Rows.Add("", "", "", "", "", "");
                            j = j + 1;
                    }
                    int heignht = GetDataGridViewHeight(dataGridView1_treatment_paln);
                    dataGridView1_treatment_paln.Height = heignht;
                }
                pre_height = tre_hight +5;// pre_height + 2;
            }
            //prescription
            DataTable dt_pre_main = this.cntrl.Get_maindtails_para_showmore(patient_id, count);
            if (dt_pre_main.Rows.Count > 0)
            {
                foreach (DataRow dy in dt_pre_main.Rows)
                {
                    int p = 0;
                    Label lb = new Label();
                    lb.Text = String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(dy["date"].ToString()));
                    lb.BackColor = Color.White;
                    lb.ForeColor = Color.Navy;
                    lb.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    lb.Width = 260;
                    lb.Height = 30;
                    this.flowLayoutPanel2.Controls.Add(lb);
                    this.flowLayoutPanel2.Controls.SetChildIndex(lb, pre_height);
                    pre_height = pre_height + 1;
                    DataGridView dataGridView1 = new DataGridView();
                    dataGridView1.BackgroundColor = Color.White;
                    dataGridView1.Width = 559;
                    dataGridView1.ColumnCount = 5;
                    dataGridView1.Columns[0].Width = 10;
                    dataGridView1.Columns[1].Width = 225;
                    dataGridView1.Columns[2].Width = 100;
                    dataGridView1.Columns[3].Width = 100;
                    dataGridView1.Columns[4].Width = 120;
                    dataGridView1.ColumnHeadersVisible = false;
                    dataGridView1.RowHeadersVisible = false;
                    dataGridView1.EnableHeadersVisualStyles = false;
                    dataGridView1.BorderStyle = BorderStyle.Fixed3D;
                    dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.RowHeadersVisible = false;
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.AllowUserToResizeRows = false;
                    dataGridView1.AllowUserToDeleteRows = false;
                    dataGridView1.AllowUserToOrderColumns = false;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.AllowUserToResizeColumns = false;
                    dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                    this.flowLayoutPanel2.Controls.Add(dataGridView1);
                    this.flowLayoutPanel2.Controls.SetChildIndex(dataGridView1, pre_height);
                    pre_height = pre_height + 1;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                    dataGridView1.Rows.Add(dt_pre_main.Rows[0]["id"].ToString(), "Drug", "Frequency", "Duration", "Instruction");
                    dataGridView1.Rows[p].Cells[1].Style.BackColor = Color.FromArgb(0, 51, 102);
                    dataGridView1.Rows[p].Cells[2].Style.BackColor = Color.FromArgb(0, 51, 102);
                    dataGridView1.Rows[p].Cells[3].Style.BackColor = Color.FromArgb(0, 51, 102);
                    dataGridView1.Rows[p].Cells[4].Style.BackColor = Color.FromArgb(0, 51, 102);
                    dataGridView1.Rows[p].Cells[1].Style.ForeColor = Color.White;
                    dataGridView1.Rows[p].Cells[2].Style.ForeColor = Color.White;
                    dataGridView1.Rows[p].Cells[3].Style.ForeColor = Color.White;
                    dataGridView1.Rows[p].Cells[4].Style.ForeColor = Color.White;
                    System.Data.DataTable dt_prescription = this.cntrl.prescription_detoails(dy["id"].ToString());
                    if (dt_prescription.Rows.Count > 0)
                    {
                        for (int k = 0; k < dt_prescription.Rows.Count; k++)
                        {
                            p = p + 1;
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
                                dataGridView1.Rows.Add("0", dt_prescription.Rows[k]["drug_type"].ToString() + " " + dt_prescription.Rows[k]["drug_name"].ToString() + " " + dt_prescription.Rows[k]["strength"].ToString() + " " + "", "", duration, dt_prescription.Rows[k]["add_instruction"].ToString());

                            }
                            else
                            {
                                dataGridView1.Rows.Add("0", dt_prescription.Rows[k]["drug_type"].ToString() + " " + dt_prescription.Rows[k]["drug_name"].ToString() + " " + dt_prescription.Rows[k]["strength"].ToString() + " " + dt_prescription.Rows[k]["strength_gr"].ToString(), morning + " - " + noon + " - " + night, duration, dt_prescription.Rows[k]["add_instruction"].ToString());

                            }
                            dataGridView1.Rows[p].Height = 25;
                        }
                    }
                    p = p + 1;
                    dataGridView1.Rows.Add("0", "Prescribed by Dr." + dt_pre_main.Rows[0]["doctor_name"].ToString(), "", "");
                    dataGridView1.Rows[p].Cells[1].Style.ForeColor = Color.Red;
                    dataGridView1.Rows[p].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                    p = p + 1;
                    dataGridView1.Rows.Add("0", "", "", "");
                    p = p + 1;
                    int heignht = GetDataGridViewHeight(dataGridView1);
                    dataGridView1.Height = heignht;
                }

            }

        }
     
        public bool showmore_click_flag = false;
        private void btn_prescription_Click(object sender, EventArgs e)
        {
            show_flag = false;
            last_grid_height = 0;
            flowLayoutPanel2.Controls.Clear(); v_hignt = 0; c_hight = 0; tre_hight = 0; pre_height = 0;
            if (patient_id != "")
            {
                DataTable dt_bg = this.cntrl.vitals_grp_visit_para_showmore(patient_id);
                DataTable dt_tre = this.cntrl.Load_treatments_para_showmore(patient_id);
                DataTable dt_clic = this.cntrl.dt_clinic_main_grp_para_showmore(patient_id);
                DataTable dt_pres = this.cntrl.Get_maindtails_para_showmore(patient_id);
                if (dt_bg.Rows.Count > 2 || dt_tre.Rows.Count > 2 || dt_clic.Rows.Count>2 || dt_pres.Rows.Count>2)
                {
                    lb_showmore.Visible = true;
                }
                else
                    lb_showmore.Visible = false;

                //vitals
                DataTable dt_vitals = this.cntrl.vitals_grp_visit_para(patient_id);
                if (dt_vitals.Rows.Count > 0)
                {
                    Label lbl = new Label();
                    lbl.Text = "Vital Signs";
                    lbl.BackColor = Color.FromArgb (51, 187, 255);
                    lbl.ForeColor = Color.White;
                    lbl.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    lbl.Width = 559;
                    lbl.Height = 34;
                    this.flowLayoutPanel2.Controls.Add(lbl);
                    foreach (DataRow dr in dt_vitals.Rows)
                    {
                        Label lb = new Label();
                        lb.Text = String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(dr["date"].ToString()));
                        lb.BackColor = Color.White;// FromArgb(209, 226, 237);
                        lb.ForeColor = Color.Navy;
                        lb.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                        lb.Width = 260;
                        lb.Height = 30;
                        this.flowLayoutPanel2.Controls.Add(lb);
                        DataGridView dgv_vitals = new DataGridView();
                        dgv_vitals.BackgroundColor = Color.White;
                        dgv_vitals.Height = 230;
                        dgv_vitals.Width = 559;
                        dgv_vitals.ColumnCount = 3;
                        dgv_vitals.Columns[0].Width = 200;
                        dgv_vitals.Columns[1].Width = 10;
                        dgv_vitals.Columns[2].Width = 225;
                        dgv_vitals.ColumnHeadersVisible = false;
                        dgv_vitals.RowHeadersVisible = false;
                        dgv_vitals.EnableHeadersVisualStyles = false;
                        dgv_vitals.BorderStyle = BorderStyle.Fixed3D;
                        dgv_vitals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        dgv_vitals.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                        dgv_vitals.CellBorderStyle = DataGridViewCellBorderStyle.None;
                        this.flowLayoutPanel2.Controls.Add(dgv_vitals);// panel6.Controls.Add(dgv_vitals);
                        dgv_vitals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dgv_vitals.AllowUserToAddRows = false;
                        dgv_vitals.AllowUserToResizeRows = false;
                        dgv_vitals.AllowUserToDeleteRows = false;
                        dgv_vitals.AllowUserToOrderColumns = false; dgv_vitals.ReadOnly = true; 
                        dgv_vitals.AllowUserToResizeColumns = false;
                        dgv_vitals.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dgv_vitals.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        int ri = 0;
                        if (dr["pulse"].ToString() != "")
                        {
                            dgv_vitals.Rows.Add("PULSE ", ":", dr["pulse"].ToString()+ "(Heart Beats Per Minute)");
                            //
                            dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                            dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                            dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                            ri = ri + 1;
                        }
                        if (dr["temp"].ToString() != "")
                        {
                            dgv_vitals.Rows.Add("TEMPERATURE ", ":", dr["temp"].ToString() + " ( " + dr["temp_type"].ToString() + " ) " + " (C)");

                            dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                            dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                            dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                            ri = ri + 1;
                        }
                        if (dr["bp_syst"].ToString() != "")
                        {
                            dgv_vitals.Rows.Add("BLOOD PRESSURE ( SYSTOLIC ) ", ":", dr["bp_syst"].ToString() + " (mm Hg)");
                            dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                            dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                            dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                            ri = ri + 1;
                        }
                        if (dr["bp_dia"].ToString() != "")
                        {
                            dgv_vitals.Rows.Add("BLOOD PRESSURE ( DIASTOLIC )  ", ":", dr["bp_dia"].ToString() + " (mm Hg)");
                            dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                            dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                            dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                            ri = ri + 1;
                        }
                        if (dr["Height"].ToString() != "")
                        {
                            dgv_vitals.Rows.Add("HEIGHT  ", ":", dr["Height"].ToString() + "(Cm)");
                            dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                            dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                            dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                            ri = ri + 1;
                        }
                        if (dr["weight"].ToString() != "")
                        {
                            dgv_vitals.Rows.Add("WEIGHT  ", ":", dr["weight"].ToString() + "(Kg)");
                            dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                            dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                            dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                            ri = ri + 1;
                        }
                        if (dr["resp"].ToString() != "")
                        {
                            dgv_vitals.Rows.Add("RESPIRATORY RATE ", ":", dr["resp"].ToString()+ "(Breaths/min)");
                            dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                            dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                            dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                            ri = ri + 1;
                        }
                        if (dr["weight"].ToString() != null && dr["weight"].ToString() != "" && dr["Height"].ToString() != null && dr["Height"].ToString() != "")
                        {
                            weight = Convert.ToDouble(dr["weight"].ToString());
                            height = Convert.ToDouble(dr["Height"].ToString());
                        }
                        else
                        {
                            weight = Convert.ToDouble("0.00");
                            height = Convert.ToDouble("0.00");
                        }
                        gender = lb_gender.Text;
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
                                dgv_vitals.Rows.Add("BMI(BOADY MASS INDEX) ", ":", BMI + "  ,   " + msg);
                               
                                if (msg == "BMI is low")
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.Red;
                                else if (msg == "Normal")
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                else if (msg == "BMI is High")
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.Red;
                                dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                ri = ri + 1;
                            }
                        }
                        if (dr["spo"].ToString() != "")
                        {
                            dgv_vitals.Rows.Add("SPO2 ", ":", dr["spo"].ToString()+"%");
                            dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                            dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                            dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                            ri = ri + 1;
                        }
                        dgv_vitals.Rows.Add("", "", ""); ri = ri + 1;
                        dgv_vitals.Rows.Add("Recorded By : Dr." + dr["dr_name"].ToString(), "", "");
                        dgv_vitals.Rows[ri ].Cells[0].Style.ForeColor = Color.Red;
                        dgv_vitals.Rows[ri ].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Italic);
                        int heignht = GetDataGridViewHeight(dgv_vitals);
                        dgv_vitals.Height = heignht;
                        v_hignt = this.flowLayoutPanel2.Controls.GetChildIndex(dgv_vitals);
                    }
                    v_hignt = v_hignt + 1;
                }
                //ends
                //clinic
                   DataTable dt_cf_main = this.cntrl.dt_clinic_main_grp_para(patient_id);
                if (dt_cf_main.Rows.Count > 0)
                {

                    Label lbl1 = new Label();
                    lbl1.Text = "Clinical Findings";
                    lbl1.BackColor = Color.FromArgb(0, 128, 128);
                    lbl1.ForeColor = Color.White;
                    lbl1.Width = 559;
                    lbl1.Height = 34;
                    lbl1.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    this.flowLayoutPanel2.Controls.Add(lbl1);
                    foreach (DataRow dtr in dt_cf_main.Rows)
                    {
                        int i = 0;
                        Label lb = new Label();
                        lb.Text = String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(dtr["date"].ToString()));
                        lb.BackColor = Color.White;
                        lb.ForeColor = Color.Navy;
                        lb.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                        lb.Width = 260;
                        lb.Height = 30;
                        this.flowLayoutPanel2.Controls.Add(lb);
                        DataGridView dgv_clinic = new DataGridView();
                        dgv_clinic.Height = 150;
                        dgv_clinic.Width = 559;
                        dgv_clinic.ColumnCount = 4;
                        dgv_clinic.Columns[0].Width = 20;
                        dgv_clinic.Columns[1].Width = 150;
                        dgv_clinic.Columns[2].Width = 1; 
                        dgv_clinic.Columns[3].Width = 300;
                        dgv_clinic.BackgroundColor = Color.White;
                        dgv_clinic.EnableHeadersVisualStyles = false;
                        dgv_clinic.BorderStyle = BorderStyle.Fixed3D;
                        dgv_clinic.Enabled = false;
                        dgv_clinic.CellBorderStyle = DataGridViewCellBorderStyle.None;
                        this.flowLayoutPanel2.Controls.Add(dgv_clinic);
                        DataGridViewColumn column = dgv_clinic.Columns[3];
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgv_clinic.ReadOnly = true;                       
                        dgv_clinic.ColumnHeadersVisible = false;
                        dgv_clinic.RowHeadersVisible = false;
                        dgv_clinic.AllowUserToAddRows = false;
                        dgv_clinic.AllowUserToResizeRows = false;
                        dgv_clinic.AllowUserToDeleteRows = false;
                        dgv_clinic.AllowUserToOrderColumns = false;
                        dgv_clinic.AllowUserToResizeColumns = false;
                        string heading = "";
                        DataTable dt_clinic = this.cntrl.dt_cf_Complaints(dtr["id"].ToString());
                        if (dt_clinic.Rows.Count > 0)
                        {
                            heading = "Complaints";
                            foreach (DataRow drr in dt_clinic.Rows)
                            {
                                dgv_clinic.Rows.Add("0", heading, "", drr["complaint_id"].ToString());
                                heading = "";
                                dgv_clinic.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                dgv_clinic.Rows[i].Cells[1].Style.ForeColor = Color.DarkBlue;
                                dgv_clinic.Rows[i].Cells[3].Style.ForeColor = Color.DarkBlue;
                                dgv_clinic.Columns[0].Visible = false;
                                i = i + 1;
                            }
                            dgv_clinic.Rows.Add("", "", "", "");
                            dgv_clinic.Rows[i].Height = 1;
                            dgv_clinic.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                            i = i + 1;
                        }
                        System.Data.DataTable dt_cf_diagnosis = this.cntrl.dt_cf_diagnosis(dtr["id"].ToString());
                        if (dt_cf_diagnosis.Rows.Count > 0)
                        {
                            heading = "Diagnosis";
                            foreach (DataRow drr in dt_cf_diagnosis.Rows)
                            {
                                dgv_clinic.Rows.Add("0", heading, "", drr["diagnosis_id"].ToString());
                                heading = "";
                                dgv_clinic.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                dgv_clinic.Rows[i].Cells[1].Style.ForeColor = Color.DarkBlue;
                                dgv_clinic.Rows[i].Cells[3].Style.ForeColor = Color.DarkBlue;
                                dgv_clinic.Columns[0].Visible = false;
                                i = i + 1;
                            }
                            dgv_clinic.Rows.Add("", "", "", "");
                            dgv_clinic.Rows[i].Height = 1;
                            dgv_clinic.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                            i = i + 1;
                        }
                        System.Data.DataTable dt_cf_note = this.cntrl.dt_cf_note(dtr["id"].ToString());
                        if (dt_cf_note.Rows.Count > 0)
                        {
                            heading = "Notes";
                            foreach (DataRow drr in dt_cf_note.Rows)
                            {
                                dgv_clinic.Rows.Add("0", heading, "", drr["note_name"].ToString());
                                heading = "";
                                dgv_clinic.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                dgv_clinic.Rows[i].Cells[1].Style.ForeColor = Color.DarkBlue;
                                dgv_clinic.Rows[i].Cells[3].Style.ForeColor = Color.DarkBlue;
                                dgv_clinic.Columns[0].Visible = false;
                                i = i + 1;
                            }
                        }
                        int heignht = GetDataGridViewHeight(dgv_clinic);
                        dgv_clinic.Height = heignht;
                        c_hight = this.flowLayoutPanel2.Controls.GetChildIndex(dgv_clinic);
                    }
                    c_hight = c_hight + 1;
                }
                // treatments
                DataTable dt_treatmnt = new DataTable();
                dt_treatmnt = this.cntrl.Load_treatments_para(patient_id);
                if (dt_treatmnt.Rows.Count > 0)
                {
                    last_grid_height = 0;
                    Label lbl2 = new Label();
                    lbl2.Text = "Treatments";
                    lbl2.BackColor = Color.FromArgb(51, 187, 255);
                    lbl2.ForeColor = Color.White;
                    lbl2.Width = 559;
                    lbl2.Height = 34;
                    lbl2.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    this.flowLayoutPanel2.Controls.Add(lbl2);
                    foreach (DataRow dr in dt_treatmnt.Rows)
                    {
                        int j = 0;
                        Label lb = new Label();
                        lb.Text = String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(dr["date"].ToString()));
                        lb.BackColor = Color.White;
                        lb.ForeColor = Color.Navy;
                        lb.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        lb.Width = 260;
                        lb.Height = 30;
                        this.flowLayoutPanel2.Controls.Add(lb);
                        DataGridView dataGridView1_treatment_paln = new DataGridView();
                        dataGridView1_treatment_paln.BackgroundColor = Color.White;
                        dataGridView1_treatment_paln.Width = 559;
                        dataGridView1_treatment_paln.ColumnHeadersVisible = true;
                        dataGridView1_treatment_paln.Columns.Add("id", "ID");
                        dataGridView1_treatment_paln.Columns.Add("TREATMENTS", "TREATMENTS");
                        dataGridView1_treatment_paln.Columns.Add("COST", "COST");
                        dataGridView1_treatment_paln.Columns.Add("DISCOUNT", "DISCOUNT");
                        dataGridView1_treatment_paln.Columns.Add("TOTAL", "TOTAL");
                        dataGridView1_treatment_paln.Columns.Add("NOTE", "NOTE");
                        dataGridView1_treatment_paln.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 51, 102);
                        dataGridView1_treatment_paln.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                        dataGridView1_treatment_paln.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; 
                        dataGridView1_treatment_paln.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridView1_treatment_paln.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridView1_treatment_paln.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                        dataGridView1_treatment_paln.Columns[0].Width = 5;
                        dataGridView1_treatment_paln.Columns[1].Width = 174;
                        dataGridView1_treatment_paln.Columns[2].Width = 81;
                        dataGridView1_treatment_paln.Columns[3].Width = 121;
                        dataGridView1_treatment_paln.Columns[4].Width = 90;
                        dataGridView1_treatment_paln.Columns[5].Width = 70;
                        dataGridView1_treatment_paln.Columns[0].Visible = false;
                        dataGridView1_treatment_paln.RowHeadersVisible = false;
                        dataGridView1_treatment_paln.EnableHeadersVisualStyles = false;
                        dataGridView1_treatment_paln.BorderStyle = BorderStyle.Fixed3D;
                        dataGridView1_treatment_paln.CellBorderStyle = DataGridViewCellBorderStyle.None;
                        dataGridView1_treatment_paln.Location = new Point(x, y);
                        this.flowLayoutPanel2.Controls.Add(dataGridView1_treatment_paln);
                        dataGridView1_treatment_paln.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dataGridView1_treatment_paln.ReadOnly = true;
                        dataGridView1_treatment_paln.RowHeadersVisible = false;
                        dataGridView1_treatment_paln.AllowUserToAddRows = false;
                        dataGridView1_treatment_paln.AllowUserToResizeRows = false;
                        dataGridView1_treatment_paln.AllowUserToDeleteRows = false;
                        dataGridView1_treatment_paln.AllowUserToOrderColumns = false;
                        dataGridView1_treatment_paln.AllowUserToResizeColumns = false;
                        DataTable dt_pt_sub = this.cntrl.treatment_sub_details(dr["id"].ToString());
                        if (dt_pt_sub.Rows.Count > 0)
                        {
                            Double totalEst = 0;
                            for (int k = 0; k < dt_pt_sub.Rows.Count; k++)
                            {
                                string discount_string = "";
                                if (dt_pt_sub.Rows[k]["discount_type"].ToString() == "INR")
                                {
                                    discount_string = "";
                                }
                                else
                                {
                                    discount_string = "(" + dt_pt_sub.Rows[k]["discount"].ToString() + "%)";
                                }
                                Decimal totalcost = Convert.ToDecimal(dt_pt_sub.Rows[k]["cost"].ToString()) * Convert.ToDecimal(dt_pt_sub.Rows[k]["quantity"].ToString());
                                dataGridView1_treatment_paln.Rows.Add(dt_pt_sub.Rows[k]["id"].ToString(), dt_pt_sub.Rows[k]["procedure_name"].ToString(), String.Format("{0:C2}", Convert.ToDecimal(totalcost)), String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discount_inrs"].ToString())) + discount_string, String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString())), dt_pt_sub.Rows[k]["note"].ToString() + " " + dt_pt_sub.Rows[k]["tooth"].ToString());
                                dataGridView1_treatment_paln.Rows[j].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                dataGridView1_treatment_paln.Rows[j].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                dataGridView1_treatment_paln.Rows[j].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                totalEst = totalEst + Convert.ToDouble(dt_pt_sub.Rows[k]["total"].ToString());
                                j = j + 1;
                            }
                            
                            dataGridView1_treatment_paln.Rows.Add("", "Planned by " + dt_treatmnt.Rows[0]["dr_name"].ToString(), "", "Estimated amount:", String.Format("{0:C2}", Convert.ToDecimal(totalEst)));
                            dataGridView1_treatment_paln.Rows[j].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Regular);
                            dataGridView1_treatment_paln.Rows[j].Cells[1].Style.ForeColor = Color.Red;
                            dataGridView1_treatment_paln.Rows[j].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Regular);
                            dataGridView1_treatment_paln.Rows[j].Cells[3].Style.ForeColor = Color.Red;
                            dataGridView1_treatment_paln.Rows[j].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Regular);
                            dataGridView1_treatment_paln.Rows[j].Cells[4].Style.ForeColor = Color.Red;
                            dataGridView1_treatment_paln.Rows[j].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                            j = j + 1;
                            dataGridView1_treatment_paln.Rows.Add("", "", "", "", "", "");
                            j = j + 1;
                            int heignht = GetDataGridViewHeight(dataGridView1_treatment_paln);
                            dataGridView1_treatment_paln.Height = heignht;
                            tre_hight = this.flowLayoutPanel2.Controls.GetChildIndex(dataGridView1_treatment_paln);
                        }
                    }
                    tre_hight = tre_hight + 1;
                }
                //prescription
                DataTable dt_pre_main = this.cntrl.Get_maindtails_para(patient_id);
                if (dt_pre_main.Rows.Count > 0)
                {
                    Label lbl3 = new Label();
                    lbl3.Text = "Prescription";
                    lbl3.BackColor = Color.FromArgb(51, 187, 255);
                    lbl3.ForeColor = Color.White;
                    lbl3.Width = 559;
                    lbl3.Height = 34;
                    lbl3.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    this.flowLayoutPanel2.Controls.Add(lbl3);
                    foreach (DataRow dy in dt_pre_main.Rows)
                    {
                        int p = 0;
                        Label lb = new Label();
                        lb.Text = String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(dy["date"].ToString()));
                        lb.BackColor = Color.White;
                        lb.ForeColor = Color.Navy;
                        lb.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        lb.Width = 260;
                        lb.Height = 30;
                        this.flowLayoutPanel2.Controls.Add(lb);
                        DataGridView dataGridView1 = new DataGridView();
                        dataGridView1.BackgroundColor = Color.White;
                        dataGridView1.Width = 559;
                        dataGridView1.ColumnHeadersVisible = true;
                        dataGridView1.Columns.Add("id", "ID");
                        dataGridView1.Columns.Add("DRUG", "DRUG");
                        dataGridView1.Columns.Add("FREQUENCY", "FREQUENCY");
                        dataGridView1.Columns.Add("DURATION", "DURATION");
                        dataGridView1.Columns.Add("INSTRUCTION", "INSTRUCTION");
                        dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 51, 102);
                        dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                        dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                        dataGridView1.Columns[0].Width = 10;
                        dataGridView1.Columns[1].Width = 220;
                        dataGridView1.Columns[2].Width = 100;
                        dataGridView1.Columns[3].Width = 100;
                        dataGridView1.Columns[4].Width = 120;
                        dataGridView1.RowHeadersVisible = false;
                        dataGridView1.EnableHeadersVisualStyles = false;
                        dataGridView1.BorderStyle = BorderStyle.Fixed3D;
                        dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
                        dataGridView1.Location = new Point(x, y);
                        dataGridView1.Columns[0].Visible = false;
                        this.flowLayoutPanel2.Controls.Add(dataGridView1);
                        dataGridView1.ReadOnly = true;
                        dataGridView1.RowHeadersVisible = false;
                        dataGridView1.AllowUserToAddRows = false;
                        dataGridView1.AllowUserToResizeRows = false;
                        dataGridView1.AllowUserToDeleteRows = false;
                        dataGridView1.AllowUserToOrderColumns = false;
                        dataGridView1.AllowUserToResizeColumns = false;
                        dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        System.Data.DataTable dt_prescription = this.cntrl.prescription_detoails(dy["id"].ToString());
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
                                    dataGridView1.Rows.Add("0", dt_prescription.Rows[k]["drug_type"].ToString() + " " + dt_prescription.Rows[k]["drug_name"].ToString() + " " + dt_prescription.Rows[k]["strength"].ToString() + " " + "", "", duration, dt_prescription.Rows[k]["add_instruction"].ToString());
                                }
                                else
                                {
                                    dataGridView1.Rows.Add("0", dt_prescription.Rows[k]["drug_type"].ToString() + " " + dt_prescription.Rows[k]["drug_name"].ToString() + " " + dt_prescription.Rows[k]["strength"].ToString() + " " + dt_prescription.Rows[k]["strength_gr"].ToString(), morning + " - " + noon + " - " + night, duration, dt_prescription.Rows[k]["add_instruction"].ToString());
                                }
                            }
                        }
                        p = p + 1;
                        dataGridView1.Rows.Add("0", "Prescribed by Dr." + dt_pre_main.Rows[0]["doctor_name"].ToString(), "", "");
                        dataGridView1.Rows[p].Cells[1].Style.ForeColor = Color.Red;
                        dataGridView1.Rows[p].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                        p = p + 1;
                        dataGridView1.Rows.Add("0", "", "", "");
                        p = p + 1;
                        int heignht = GetDataGridViewHeight(dataGridView1);
                        dataGridView1.Height = heignht;
                        pre_height = this.flowLayoutPanel2.Controls.GetChildIndex(dataGridView1);
                    }
                    pre_height = pre_height + 1;
                }
                
            }
        }

        private void btn_vital_save_Click(object sender, EventArgs e)
        {
            try
            {
                string temp_type = "", bp_type = ""; int i = 0; string maxid = "";
                if(patient_id!="")
                {
                    if(doctor_id!="" && doctor_id !="0")
                    {
                        if ((txt_pluse.Text == "") && (txt_temp.Text == "") && (txt_height.Text == "") && (txt_blood1.Text == "") && (txt_spo.Text == "") && (txt_resp.Text == "") && (txt_weight.Text == ""))
                        {
                            MessageBox.Show("Data not found , Please add some vitals  !..", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            vitals_clear();
                            return;
                        }
                        else 
                        {
                            string dt = DateTime.Now.Date.ToString("yyyy-MM-dd");
                            DateTime Timeonly = DateTime.Now;
                            temp_type = cmb_temp.SelectedItem.ToString();
                                this.vmdl.save_vital_main(patient_id, doctor_id, dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                            DataTable dt_max = this.vmdl.dt_get_maxid();
                            if (dt_max.Rows.Count > 0)
                            {
                                maxid = dt_max.Rows[0][0].ToString();
                            }
                            if(txt_blood1.Text!="")
                            {
                                bp_type = "Sitting";
                            }
                            i = this.vmdl.submit(patient_id, doctor_id, rjCmb_doctor.SelectedItem.ToString(), temp_type, bp_type, txt_pluse.Text, txt_temp.Text, txt_blood1.Text, txt_blood2.Text, txt_weight.Text, txt_resp.Text, dateTimePicker2.Value.ToString("yyyy-MM-dd"), txt_height.Text, txt_spo.Text, maxid, Convert.ToString(Timeonly.ToString("hh:mm tt")));
                            if (i > 0)
                            {
                                this.model.save_log(doctor_id, "Vital Sign", " Add Vital Sign", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Add", maxid);
                            }

                            MessageBox.Show("Data saved successfully !..", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            vitals_clear();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Doctor not found !..", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Patient not found !..", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void vitals_clear()
        {
            txt_pluse.Text = "";
            txt_temp.Text = "";
            txt_blood1.Text = "";
            txt_blood2.Text = "";
            txt_weight.Text = "";
            txt_resp.Text = "";
            txt_height.Text = "";
            txt_spo.Text = "";
            txt_bmi.Text = "";
            cmb_temp.SelectedIndex = 0;
            label10.Visible = false;
        }
        public void Allclear()
        {
            //patient
            pat_name.Visible = false;
            label13.Visible = false;
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
            lb_followup.Visible = false;
            dateTime_review.Value = DateTime.Now.Date;
            ////clinical
            userControl_text_notes.Text = "";
            userControl_text_Diagnosis.Text = "";
            userControl_text_complaints.Text = "";
            label13.Visible = false;
            //prescription
            dgv_prescrptn.Rows.Clear();
            cmb_temp.SelectedIndex = 0;
            txtTotalCost.Text = "0.00";
            txtTotaltax.Text = "0.00";
            txtTotalDisc.Text = "0.00";
            txt_grand_total.Text = "0.00";
        }
        private void rjCmb_doctor_onSelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt_doctor_id = this.cntrl.get_doctorname(rjCmb_doctor.SelectedItem.ToString());
            doctor_id = dt_doctor_id.Rows[0]["id"].ToString();
        }

        private void btn_save_clinic_Click(object sender, EventArgs e)
        {
            try
            {
                if (patient_id != "")
                {
                    if (doctor_id != "")
                    {
                        int treat = 0;
                        string dt = DateTime.Now.Date.ToString("yyyy-MM-dd");
                        DateTime Timeonly = DateTime.Now;
                        if (userControl_text_complaints.Text != "" || userControl_text_Diagnosis.Text != "" || userControl_text_notes.Text != "")
                        {
                            this.cmdl.insertInto_clinical_findings(patient_id, doctor_id, dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                            string treatment = this.cmdl.MaxId_clinic_findings();
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
                                    this.cmdl.insrtto_chief_comp(treat, one);
                                }
                            }
                            if (userControl_text_Diagnosis.Text != "")
                            {
                                string s = userControl_text_Diagnosis.Text;
                                string[] values = s.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                                foreach (string items in values)
                                {
                                    string one = items;
                                    this.cmdl.insrtto_diagno(treat, one);
                                }
                            }
                            if (userControl_text_notes.Text != "")
                            {
                                string s = userControl_text_notes.Text;
                                string[] values = s.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                                foreach (string items in values)
                                {
                                    string one = items;
                                    this.cmdl.insrtto_note(treat, one);
                                }
                            }
                            clinic_id = treat.ToString();
                            this.model.save_log(loginid, "Clinical Findings", " Fast Track", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Add", treat.ToString());
                            MessageBox.Show("Data saved successfully !..", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            userControl_text_notes.Text = "";
                            userControl_text_Diagnosis.Text = "";
                            userControl_text_complaints.Text = "";
                        }//clinical notes end
                        else
                        {
                            MessageBox.Show("Data not found !..", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Doctor not found !..", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Patient not found !..", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {

            }
           
        }

        private void btn_treatmnt_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (patient_id != "")
                {
                    if (doctor_id != "")
                    {
                        if (DGV_Procedure.Rows.Count > 0)
                        {
                            int j, k = 0;string tret_item_id = "0";
                            string cs = PappyjoeMVC.Model.Connection.MyGlobals.trans_connectionstring;
                            using (MySqlConnection con = new MySqlConnection(cs))
                            {
                                con.Open();
                                MySqlTransaction trans = con.BeginTransaction();
                                try
                                {
                                    bool flag = false;
                                    this.cntrl.Save_treatment(doctor_id, patient_id, dateTimePicker2.Value.ToString("yyyy-MM-dd"), rjCmb_doctor.SelectedItem.ToString(), pat_name.Text, txtTotalCost.Text, txtTotalDisc.Text, txt_grand_total.Text, con, trans);
                                    string dt = this.cntrl.get_treatmentmaxid(con, trans);
                                    int planid;
                                    if (Int32.Parse(dt) == 0)
                                    {
                                        j = 1;
                                        planid = 1;
                                    }
                                    else
                                    {
                                        planid = Int32.Parse(dt);
                                    }
                                    j = planid;
                                    treamt_id = j.ToString();
                                    for (int ii = 0; ii < DGV_Procedure.Rows.Count; ii++)
                                    {
                                        tret_item_id = "0";
                                        if (DGV_Procedure.Rows[ii].Cells["nursenote"].Value.ToString() == "Yes")
                                        {
                                            flag = true;
                                            this.cntrl.Save_treatmentgrid_set_ststus(j, DGV_Procedure[0, ii].Value.ToString(), patient_id, DGV_Procedure[1, ii].Value.ToString(), DGV_Procedure[2, ii].Value.ToString(), DGV_Procedure[3, ii].Value.ToString(), DGV_Procedure[5, ii].Value.ToString(), DGV_Procedure[4, ii].Value.ToString(), DGV_Procedure[6, ii].Value.ToString(), DGV_Procedure[7, ii].Value.ToString(), DGV_Procedure[11, ii].Value.ToString(), DGV_Procedure[15, ii].Value.ToString(), con, trans);

                                            tret_item_id = this.cntrl.get_trat_itme_id(con, trans);

                                        }
                                        else
                                        {
                                            this.cntrl.Save_treatmentgrid(j, DGV_Procedure[0, ii].Value.ToString(), patient_id, DGV_Procedure[1, ii].Value.ToString(), DGV_Procedure[2, ii].Value.ToString(), DGV_Procedure[3, ii].Value.ToString(), DGV_Procedure[5, ii].Value.ToString(), DGV_Procedure[4, ii].Value.ToString(), DGV_Procedure[6, ii].Value.ToString(), DGV_Procedure[7, ii].Value.ToString(), DGV_Procedure[11, ii].Value.ToString(), DGV_Procedure[15, ii].Value.ToString(), con, trans);
                                            tret_item_id = this.cntrl.get_trat_itme_id(con, trans);

                                        }
                                        this.cntrl.save_completed_id_trans(dateTimePicker2.Value.ToString("yyyy-MM-dd"), patient_id, con, trans);
                                        string maxid = this.cntrl.get_completedMaxid_trans(con, trans);
                                        int completed_id;
                                        if (Int32.Parse(maxid) == 0)//dt
                                        {
                                            k = 1;
                                            completed_id = 0;
                                        }
                                        else
                                        {
                                            completed_id = Int32.Parse(maxid);
                                        }
                                        k = completed_id;
                                        this.cntrl.save_completed_items_trans(k, patient_id, DGV_Procedure[0, ii].Value.ToString(), DGV_Procedure[1, ii].Value.ToString(), DGV_Procedure[2, ii].Value.ToString(), DGV_Procedure[3, ii].Value.ToString(), DGV_Procedure[5, ii].Value.ToString(), DGV_Procedure[4, ii].Value.ToString(), DGV_Procedure[6, ii].Value.ToString(), DGV_Procedure[7, ii].Value.ToString(), DGV_Procedure[11, ii].Value.ToString(), dateTimePicker2.Value.ToString("yyyy-MM-dd"), doctor_id, (tret_item_id).ToString(), DGV_Procedure[15, ii].Value.ToString(), "Yes", con, trans);
                                        string max_pr_id = this.cntrl.get_completedProcedureMaxid_trans(con, trans);
                                        dgv_inv_procedure.Rows.Add(DGV_Procedure[0, ii].Value.ToString(), DGV_Procedure[1, ii].Value.ToString(), DGV_Procedure[2, ii].Value.ToString(), DGV_Procedure[3, ii].Value.ToString(),DGV_Procedure[4, ii].Value.ToString(), DGV_Procedure[5, ii].Value.ToString(), DGV_Procedure[6, ii].Value.ToString(), DGV_Procedure[7, ii].Value.ToString(), DGV_Procedure[9, ii].Value.ToString(), DGV_Procedure[10, ii].Value.ToString(), DGV_Procedure[11, ii].Value.ToString(), DGV_Procedure[12, ii].Value.ToString(), "", DGV_Procedure[14, ii].Value.ToString(), DGV_Procedure[8, ii].Value.ToString(), max_pr_id, j);//k

                                    }
                                    trans.Commit();
                                    con.Close();
                                    string dt1 = DateTime.Now.Date.ToString("yyyy-MM-dd");
                                    DateTime Timeonly = DateTime.Now;
                                    this.model.save_log(doctor_id, "Treatment Plan", " Add Treatment Plan", dt1, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Add", j.ToString());
                                    if (flag == true)
                                    {
                                        string dt11 = DateTime.Now.Date.ToString("yyyy-MM-dd");
                                        DateTime Timeonl = DateTime.Now;
                                        this.model.save_log(doctor_id, "Finished Procedure", " Add Finished Procedure", dt11, Convert.ToString(Timeonl.ToString("hh:mm tt")), "Add", k.ToString());
                                    }
                                    MessageBox.Show("Data saved successfully !..", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtTotalCost.Text = ""; 
                                    txtTotalDisc.Text = "";
                                    txt_grand_total.Text = "";
                                    DGV_Procedure.Rows.Clear();
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    con.Close();
                                    MessageBox.Show(ex.Message, "SAVE / UPDATE function is failed !..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please add treatment first !..", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Doctor not found !..", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Patient not found !..", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void userControl_txt_complaints_search_Click(object sender, EventArgs e)
        {
            if (userControl_txt_complaints_search.Text == "Complaints Search")
            {
                userControl_txt_complaints_search.Text = "";
                userControl_txt_complaints_search.ForeColor = Color.DarkBlue;
            }
        }

        private void userControl_txt_complaints_search_KeyUp(object sender, KeyEventArgs e)
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

        private void lst_cheif_MouseClick(object sender, MouseEventArgs e)
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
                }
            }
            else
            {
                lst_Diagnosis.Visible = false;
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
                }
            }
            else
            {
                lst_notes.Visible = false;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (patient_id != "")
                {
                    if (doctor_id != "")
                    {
                        if (dgv_invoice.Rows.Count == 0)
                        {
                            DialogResult yesno = MessageBox.Show("Please add Invoice", "Invoice Not Added", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (yesno == DialogResult.No)
                            { return; }
                        }
                        else
                        {
                            string cs = PappyjoeMVC.Model.Connection.MyGlobals.trans_connectionstring;
                            using (MySqlConnection con = new MySqlConnection(cs))
                            {
                                con.Open();
                                MySqlTransaction trans = con.BeginTransaction();

                                try
                                {
                                    int j = 1;
                                    this.cntrl.save_invoice_main(dateTimePicker2.Value.ToString("yyyy-MM-dd"),patient_id, pat_name.Text.ToString(), txt_invoiceno.Text.ToString(), con, trans);
                                    string dt_1 = this.cntrl.get_invoiceMain_maxid(con, trans);
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
                                    for (int c = 0; c < dgv_invoice.Rows.Count; c++) //disco
                                    {
                                        this.cntrl.save_incove_items(txt_invoiceno.Text.ToString(), pat_name.Text.ToString(), patient_id, dgv_invoice.Rows[c].Cells["serviceid"].Value.ToString(), dgv_invoice.Rows[c].Cells["procedure"].Value.ToString(), dgv_invoice.Rows[c].Cells["unit"].Value.ToString(), dgv_invoice.Rows[c].Cells["cost_"].Value.ToString(), dgv_invoice.Rows[c].Cells["disco"].Value.ToString(), dgv_invoice.Rows[c].Cells["dis_type"].Value.ToString(), dgv_invoice.Rows[c].Cells["tax_type"].Value.ToString(), dgv_invoice.Rows[c].Cells["total"].Value.ToString(), dateTimePicker2.Value.ToString("yyyy-MM-dd"),dgv_invoice.Rows[c].Cells["notes"].Value.ToString(), dgv_invoice.Rows[c].Cells["total"].Value.ToString(), dgv_invoice.Rows[c].Cells["disco"].Value.ToString(), dgv_invoice.Rows[c].Cells["disco"].Value.ToString(), dgv_invoice.Rows[c].Cells["total"].Value.ToString(), dgv_invoice.Rows[c].Cells["dr_id"].Value.ToString(), dgv_invoice.Rows[c].Cells["dis_ins"].Value.ToString(), dgv_invoice.Rows[c].Cells["tax"].Value.ToString(), j.ToString(), dgv_invoice.Rows[c].Cells["pl_id"].Value.ToString(), dgv_invoice.Rows[c].Cells["compl_id"].Value.ToString(), dgv_invoice.Rows[c].Cells["to_nurse"].Value.ToString(), dgv_invoice.Rows[c].Cells["lab"].Value.ToString(), con, trans);
                                        this.cntrl.Set_completed_status0(dgv_invoice[15, c].Value.ToString(), con, trans);
                                    }
                                    this.cntrl.update_invoice_nurse_notify("True", dt_1, con, trans);
                                    string invoauto = this.cntrl.get_invoicenumber(con, trans);
                                    int invoautoup = int.Parse(invoauto) + 1;
                                    this.cntrl.update_invnumber(invoautoup.ToString(), con, trans);
                                    string dt = DateTime.Now.Date.ToString("yyyy-MM-dd");
                                    DateTime Timeonly = DateTime.Now;
                                    this.model.save_log(loginid, "Invoice", "Fast Track", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Add", j.ToString(), con, trans);
                                    trans.Commit();
                                    con.Close();
                                    DialogResult print_yesno = System.Windows.Forms.DialogResult.No;
                                    print_yesno = MessageBox.Show("Data saved successfully, Do you want a print ?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (print_yesno == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        var frm = new fasttrack_print();
                                        frm.ShowDialog();
                                        if(vital_print==true || clinic_print==true || treat_print==true || pres_print==true)
                                            printboth(pres_id.ToString(), treamt_id, vital_id, clinic_id);

                                        if (doctor_id != "1")
                                        {
                                            string privid;
                                            privid = this.invntrl.check_addprivillege(doctor_id);
                                            if (int.Parse(privid) > 0)
                                            {
                                                var form2 = new Add_Receipt();
                                                form2.doctor_id = doctor_id;
                                                form2.patient_id = patient_id;
                                                form2.status = 1;
                                                form2.fasttrack_flag = true;
                                                form2.invoices[0] = txt_invoiceno.Text.ToString();
                                                form2.ShowDialog();
                                            }
                                            else
                                            {
                                                MessageBox.Show("There is No Privilege to Add Invoice", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                                            }
                                        }
                                        else
                                        {
                                            var form2 = new Add_Receipt();
                                            form2.doctor_id = doctor_id;
                                            form2.patient_id = patient_id;
                                            form2.status = 1; form2.fasttrack_flag = true;
                                            form2.invoices[0] = txt_invoiceno.Text.ToString();
                                            form2.ShowDialog();
                                        }
                                    }
                                    else
                                    {
                                        var form2 = new Add_Receipt();
                                        form2.doctor_id = doctor_id;
                                        form2.patient_id = patient_id;
                                        form2.status = 1; form2.fasttrack_flag = true;
                                        form2.invoices[0] = txt_invoiceno.Text.ToString();
                                        form2.ShowDialog();
                                    }
                                    clear();
                                    txt_search.Visible = true;
                                    DataTable invno = null;
                                    invno = this.cntrl.Get_invoice_prefix();
                                    if (invno.Rows.Count == 0)
                                    {
                                        txt_invoiceno.Enabled = false;
                                    }
                                    else
                                    {
                                        txt_invoiceno.Text = invno.Rows[0]["invoice_prefix"].ToString() + invno.Rows[0]["invoice_number"].ToString();
                                    }
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
                    }
                    else
                    {
                        MessageBox.Show("Doctor not found !..", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Patient not found !..", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            catch (Exception ex)
            {

            }
        }
        public static  bool vital_print = false;
        public static bool clinic_print = false;
        public static bool treat_print = false;
        public static bool pres_print = false;

        public void clear()
        {
            pat_name.Visible = false;
                label15.Visible = false; 
            label7.Visible = false;
            label29.Visible = false; 
            label14.Visible = false;
            lb_mobile.Visible = false;
            lb_gender.Visible = false;
            txt_search.Text = "Search by Patient Id, Name";
            txt_search.ForeColor = Color.LightSlateGray;
            txt_search.Visible = false; txtTotaltax.Text = "0.00"; txt_inv_t_cost.Text = "0.00";
            dgv_inv_procedure.Rows.Clear(); txt_inv_grand.Text = "0.00"; txt_inv_T_disc.Text = "0.00";
            dgv_inv_prescription.Rows.Clear(); dgv_invoice.Rows.Clear();
            flowLayoutPanel2.Controls.Clear();
        }
        private void txt_procedure_KeyUp(object sender, KeyEventArgs e)
        {
            if (txt_procedure.Text != "")
            {
                DataTable dtdr = this.cntrl.search_procedures(txt_procedure.Text);
                if(dtdr.Rows.Count>0)
                {
                    lst_procedure.DataSource = dtdr;
                    lst_procedure.DisplayMember = "name";
                    lst_procedure.ValueMember = "id";
                    lst_procedure.Show();
                }
                else
                {
                    txt_procedure.Text = ""; lst_procedure.Hide();
                }
            }
            else
            {
                DataTable dtdr = this.cntrl.search_procedures(userControl_textbox10.Text);
                if (dtdr.Rows.Count > 0)
                {
                    lst_procedure.DataSource = dtdr;
                    lst_procedure.DisplayMember = "name";
                    lst_procedure.ValueMember = "id"; lst_procedure.Show();
                }
                else 
                {
                    txt_procedure.Text = ""; lst_procedure.Hide();
                }
            }
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
                txt_total.Text = dtb_procedure.Rows[0]["cost"].ToString();
                txt_procedure.ForeColor = Color.DarkBlue;
            }
            lst_procedure.Visible = false;
        }

        private void lb_add_disc_Click(object sender, EventArgs e)
        {
            lb_add_disc.Visible = false;
            cmb_discount.Show();
            txt_disc.Show();
        }

        private void txtCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch) && (e.KeyChar != '.'))
            {
                e.Handled = true;
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
            calculations_disc();
        }

        Decimal discounttotal = 0;
        Decimal taxrstotal = 0; Decimal P_tax = 0;
        public void calculations_disc()
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
        }

        private void txt_unit_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(txt_unit.Text))
                {
                    int qty = 0;
                }
                calculations_disc();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmb_discount_onSelectedIndexChanged(object sender, EventArgs e)
        {
            if (load_flag == false)
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
        public string lab_service = "No"; string id;
        private void btn_treatmnt_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(txt_unit.Text)  || String.IsNullOrWhiteSpace(txt_procedure.Text) || String.IsNullOrWhiteSpace(txtCost.Text)|| txt_procedure.Text== "Search Procedure")
                {
                    MessageBox.Show("Fill the Mandatory field(s)...", "Empty field(s)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Decimal dis = 0;
                    if (cmb_discount.SelectedItem.ToString() == "")
                    {
                        dis = 0;
                    }
                    else
                    {
                        dis = Convert.ToDecimal(txt_disc.Text);
                    }
                    DGV_Procedure.Rows.Add(lbl_procedure_id.Text, txt_procedure.Text, txt_unit.Text, txtCost.Text, dis, cmb_discount.SelectedItem.ToString(), txt_total.Text, discounttotal, "", doctor_id, rjCmb_doctor.SelectedItem.ToString(), txt_notes.Text, "TO Nurse", "No", "DEL", "");
                    Decimal totalcost = 0;
                    Decimal totaldiscount = 0;
                    Decimal totalgrand = 0;
                    for (int i = 0; i < DGV_Procedure.Rows.Count; i++)
                    {
                        totalcost = totalcost + (Convert.ToDecimal(DGV_Procedure.Rows[i].Cells[3].Value.ToString()) * Convert.ToDecimal(DGV_Procedure.Rows[i].Cells[2].Value.ToString()));
                        totaldiscount = totaldiscount + Convert.ToDecimal(DGV_Procedure.Rows[i].Cells[7].Value.ToString());
                        totalgrand = totalgrand + Convert.ToDecimal(DGV_Procedure.Rows[i].Cells[6].Value.ToString());
                    }
                    txtTotalCost.Text = String.Format("{0:C0}", totalcost);
                    txtTotalDisc.Text = String.Format("{0:C0}", totaldiscount); 
                    txt_grand_total.Text = String.Format("{0:C0}", totalgrand);
                    txt_procedure.Text = "";
                    txtCost.Text = "";
                    txt_unit.Text = ""; txt_total.Text = "";
                    txt_disc.Hide(); cmb_discount.Hide();
                    cmb_discount.SelectedIndex = 0;
                    cmb_tax.Hide();
                    txt_disc.Text = "0"; cmb_tax.Hide();
                    lab_tonurse.Text = "No"; txt_notes.Text = "";
                     lab_service = "No";
                    lb_add_disc.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !.....", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        string Prescription_bill_status = "No"; int presid;
        private void btn_precription_Click(object sender, EventArgs e)
        {
            try
            {
                if(patient_id !="")
                {
                    if(doctor_id !="")
                    {
                        if (dgv_prescrptn.Rows.Count == 0)
                        {
                            MessageBox.Show("Data not found !..", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        { 
                            string cs = PappyjoeMVC.Model.Connection.MyGlobals.trans_connectionstring;
                            using (MySqlConnection con = new MySqlConnection(cs))
                            {
                                con.Open();
                                MySqlTransaction trans = con.BeginTransaction();
                                string strstatus = "1";
                                try
                                {
                                    if (dgv_prescrptn.Rows.Count > 0)
                                    {
                                        prescription_check();
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
                                        pres_id = presid.ToString();
                                        int count = dgv_prescrptn.Rows.Count;
                                        for (int p = 0; p < count; p++)
                                        {
                                            if (dgv_prescrptn[13, p].Value.ToString() != "")
                                            { strstatus = dgv_prescrptn[13, p].Value.ToString(); }
                                            this.cntrl.save_prescription(presid, patient_id, rjCmb_doctor.SelectedItem.ToString(), doctor_id, dateTimePicker2.Value.ToString("yyyy-MM-dd"), dgv_prescrptn[0, p].Value.ToString(), dgv_prescrptn[1, p].Value.ToString(), dgv_prescrptn[2, p].Value.ToString(), dgv_prescrptn[3, p].Value.ToString(), dgv_prescrptn[4, p].Value.ToString(), dgv_prescrptn[5, p].Value.ToString(), dgv_prescrptn[6, p].Value.ToString(), dgv_prescrptn[7, p].Value.ToString(), dgv_prescrptn[8, p].Value.ToString(), dgv_prescrptn[9, p].Value.ToString(), dgv_prescrptn[11, p].Value.ToString(), strstatus, dgv_prescrptn[10, p].Value.ToString(), con, trans);
                                            dgv_inv_prescription.Rows.Add(dgv_prescrptn[0, p].Value.ToString(), dgv_prescrptn[1, p].Value.ToString(), dgv_prescrptn[2, p].Value.ToString(), dgv_prescrptn[3, p].Value.ToString(), dgv_prescrptn[4, p].Value.ToString(), dgv_prescrptn[5, p].Value.ToString(), dgv_prescrptn[6, p].Value.ToString(), dgv_prescrptn[7, p].Value.ToString(), dgv_prescrptn[8, p].Value.ToString(), dgv_prescrptn[9, p].Value.ToString(), dgv_prescrptn[10, p].Value.ToString(), dgv_prescrptn[11, p].Value.ToString(), strstatus);
                                        }
                                        string dt11 = DateTime.Now.Date.ToString("yyyy-MM-dd");
                                        DateTime Timeonly = DateTime.Now;
                                        this.model.save_log(doctor_id, "Prescription Add", "Add Prescription Add", dt11, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Add", presid.ToString());
                                        trans.Commit();
                                        con.Close();
                                        MessageBox.Show("Data saved successfully !..", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        dgv_prescrptn.Rows.Clear();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    con.Close();
                                    MessageBox.Show(ex.Message, "SAVE / UPDATE function is failed !..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Doctor not found !..", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Patient not found !..", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch(Exception ex)
            {

            }
        }
        public void prescription_check()
        {
            try
            {
                if (dgv_prescrptn.Rows.Count > 0)
                {
                    int count = dgv_prescrptn.Rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        DataTable dt4 = this.cntrl.get_inventoryid(dgv_prescrptn[10, i].Value.ToString());
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
        public string drug_type = "", id1 = "";
        private void rjBtn_Prescriptn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                string mrng = "", noon = "", durat = "", night = "", food = "";
                if (userControl_textbox10.Text != "" && userControl_textbox10.Text != "Drug Search")
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
                        dur = rjCmb_duration.SelectedItem.ToString();
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
                    if (uc_txt_noon.Text == "N")
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
                    if (uc_txt_duration.Text == "0" || uc_txt_duration.Text == "")
                    {
                        durat = "";
                    }
                    else
                    {
                        durat = uc_txt_duration.Text.ToString();
                    }
                    if(durat=="" || uc_txt_strengthno.Text=="")
                    {
                        MessageBox.Show("Please add Duration and drug strength", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if(mrng==""&& noon=="" && night=="")
                    {
                        MessageBox.Show("Please add frequency", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    dgv_prescrptn.Rows.Add(userControl_textbox10.Text, uc_txt_strengthno.Text, rjCombo_strength.SelectedItem.ToString(), durat, dur, mrng, noon, night, food, Note, id1, drug_type);
                    dgv_prescrptn.Rows[dgv_prescrptn.Rows.Count - 1].Cells[12].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                    dgv_prescrptn.Rows[dgv_prescrptn.Rows.Count - 1].Height = 30;
                    dgv_prescrptn.Rows[dgv_prescrptn.Rows.Count - 1].Cells[13].Value = strstatus;
                    Presc_clear();
                }
                else
                {
                    MessageBox.Show("Please select drug", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
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

        private void userControl_textbox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        public void calculations()
        {
            decimal qty1 = 0; decimal d;
            decimal cost = 0;
            decimal discount = 0, TotalAmount = 0, Amount = 0, gst_Amount = 0;

            if (decimal.TryParse(txt_inv_unit.Text, out d))
            {
                qty1 = Convert.ToDecimal(txt_inv_unit.Text);
            }
            if (decimal.TryParse(txt_inv_cost.Text, out d)) 
            {
                cost = Convert.ToDecimal(txt_inv_cost.Text);
            }
            if (decimal.TryParse(txt_inv_disc.Text, out d))
            {
                if (cmb_inv_discound.SelectedItem.ToString() == "INR")
                {
                    discount = Convert.ToDecimal(txt_inv_disc.Text);
                }
                else
                {
                    discount = ((qty1 * cost) * Convert.ToDecimal(txt_inv_disc.Text)) / 100;
                }
            }
            Amount = (qty1 * cost);
            txt_inv_total.Text = Convert.ToString(Amount);
            taxrstotal = 0;
            if (P_tax > 0)
            {
                TotalAmount = Amount + ((qty1 * cost) * P_tax / 100);
                txt_inv_total.Text = Convert.ToString(TotalAmount);
                taxrstotal = (qty1 * cost) * P_tax / 100;
            }
            if (discount>0)
            {
               
                discounttotal = discount;
                if (TotalAmount>0)
                {
                    txt_inv_total.Text = Convert.ToString(TotalAmount- discount);
                }
                else
                {
                    TotalAmount = Amount - discount;
                    txt_inv_total.Text = Convert.ToString(TotalAmount);
                }
            }
        }
        private void userControl_textbox7_KeyUp(object sender, KeyEventArgs e)
        {
            calculations();
        }

        private void userControl_textbox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void userControl_textbox6_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(txt_unit.Text))
                {
                    int qty = 0;
                }
                calculations();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lab_inv_invoiceadd_Click(object sender, EventArgs e)
        {
            lab_inv_discadd.Visible = false;
            cmb_inv_discound.Show();
            txt_inv_disc.Show();
        }

        private void lb_add_tax_Click(object sender, EventArgs e)
        {
            lb_add_tax.Visible = false;
            cmb_tax.Show();
        }

        private void cmb_tax_onSelectedIndexChanged(object sender, EventArgs e)
        {
            if (load_flag == false)
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

        private void txt_disc_KeyUp(object sender, KeyEventArgs e)
        {
            calculations_disc();
        }

        private void txt_disc_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void DGV_Procedure_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (DGV_Procedure.CurrentCell.OwningColumn.Name == "tonurse")
                    {
                        if (DGV_Procedure.CurrentRow.Cells["Tonurse"].Value == "Yes")
                        {
                            DGV_Procedure.CurrentRow.Cells["Tonurse"].Value = "No";
                            DGV_Procedure.CurrentRow.Cells["nursenote"].Value = "No";
                        }
                        else
                        {
                            DGV_Procedure.CurrentRow.Cells["Tonurse"].Value = "Yes";
                            DGV_Procedure.CurrentRow.Cells["nursenote"].Value = "Yes";
                        }
                    }
                    if (DGV_Procedure.CurrentCell.OwningColumn.Name == "Column4")
                    {
                        DialogResult res = MessageBox.Show("Are you sure you want to delete..?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (res == DialogResult.Yes)
                        {
                            DGV_Procedure.Rows.RemoveAt(this.DGV_Procedure.SelectedRows[0].Index);
                            if (DGV_Procedure.Rows.Count == 0)
                            {
                                txtTotalCost.Text = "Total Cost";
                                txtTotalDisc.Text = "Total Discount";
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
            for (int i = 0; i < DGV_Procedure.Rows.Count; i++)
            {
                decimal unitcost = decimal.Parse(DGV_Procedure.Rows[i].Cells["cost"].Value.ToString());
                decimal quantity = decimal.Parse(DGV_Procedure.Rows[i].Cells[3].Value.ToString());
                decimal totalcost = unitcost * quantity;
                totcost = totcost + totalcost;
                txtTotalCost.Text = totcost.ToString("0.00");
                decimal total1 = Convert.ToDecimal(DGV_Procedure.Rows[i].Cells[6].Value.ToString());
                total2 = total2 + total1;
                txt_grand_total.Text = total2.ToString("0.00");
                float dicount = float.Parse(DGV_Procedure.Rows[i].Cells[7].Value.ToString());
                discount1 = float.Parse(totalcost.ToString()) * (dicount / 100);
                dicount2 = dicount2 + dicount;
                txtTotalDisc.Text = dicount2.ToString("0.00");
            }
        }

        private void dgv_inv_procedure_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            {
                cmb_inv_discound.SelectedIndex = 0;
                label19.Text= dgv_inv_procedure.CurrentRow.Cells[0].Value.ToString();
                txt_inv_procedure.Text = dgv_inv_procedure.CurrentRow.Cells[1].Value.ToString();
                txt_inv_cost.Text= dgv_inv_procedure.CurrentRow.Cells[3].Value.ToString();
                txt_inv_unit.Text= dgv_inv_procedure.CurrentRow.Cells[2].Value.ToString();
                if(dgv_inv_procedure.CurrentRow.Cells[4].Value.ToString()!="0")
                {
                    lab_inv_discadd.Visible = false;
                    txt_inv_disc.Text = dgv_inv_procedure.CurrentRow.Cells[4].Value.ToString();
                    if (dgv_inv_procedure.CurrentRow.Cells[5].Value.ToString()=="%")
                    {
                        cmb_inv_discound.SelectedIndex = 0;
                    }
                    else
                    {
                        cmb_inv_discound.SelectedIndex = 1;
                    }
                    calculations();
                }
                else
                {
                    lab_inv_discadd.Visible = true;
                    txt_inv_disc.Text = "0";
                    calculations();
                }
            }
        }

      
        private void rjButtons4_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_inv_procedure.Text!="")
                {
                    string tonurse = "";
                    if(dgv_inv_procedure.CurrentRow.Cells[11].Value.ToString()== "TO Nurse")
                    {
                        tonurse = "";
                    }
                    else
                    {
                        tonurse = dgv_inv_procedure.CurrentRow.Cells[11].Value.ToString();
                    }
                    dgv_invoice.Rows.Add(label19.Text, txt_inv_procedure.Text, txt_inv_cost.Text, txt_inv_unit.Text, txt_inv_disc.Text, discounttotal, cmb_inv_discound.SelectedItem.ToString(),taxrstotal,cmb_tax.SelectedItem.ToString(), txt_inv_total.Text,doctor_id,rjCmb_doctor.SelectedItem.ToString(),"","", dgv_inv_procedure.CurrentRow.Cells[14].Value.ToString(), dgv_inv_procedure.CurrentRow.Cells[15].Value.ToString(),dateTimePicker2.Value.ToString(), tonurse, PappyjoeMVC.Properties.Resources.deleteicon, "NO");
                    decimal totcost = 0;
                    decimal discount1 = 0, tax1 = 0, grand = 0;
                    for (int i = 0; i < dgv_invoice.Rows.Count; i++)
                    {
                        totcost = totcost + (decimal.Parse(dgv_invoice.Rows[i].Cells[2].Value.ToString()));
                        discount1 = discount1 + decimal.Parse(dgv_invoice.Rows[i].Cells["dis_ins"].Value.ToString());
                        tax1 = tax1 + decimal.Parse(dgv_invoice.Rows[i].Cells["tax"].Value.ToString());
                        grand = grand + decimal.Parse(dgv_invoice.Rows[i].Cells["total"].Value.ToString());//8
                    }
                    txt_inv_t_cost.Text = totcost.ToString("F");
                    txt_inv_T_disc.Text = discount1.ToString("F");
                    txtTotaltax.Text = tax1.ToString("F");
                    decimal d1 = Convert.ToDecimal((totcost + tax1) - discount1);
                    txt_inv_grand.Text = grand.ToString("F");
                    txt_inv_procedure.Text = "";
                    txt_inv_cost.Text = "";
                    txt_inv_unit.Text = ""; txt_inv_total.Text = "";
                    cmb_inv_discound.SelectedIndex = 0;
                    cmb_tax.Hide(); label19.Text = "0";
                    txt_inv_disc.Text = "0"; cmb_tax.Hide();
                    lab_tonurse.Text = "No";
                    lab_service = "No";
                    lab_inv_discadd.Visible = true;
                    lb_add_tax.Visible = true; discounttotal = 0;
                }
            }
            catch(Exception ex)
            {

            }
            
        }

        private void btn_inv_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (patient_id != "")
                {
                    if (doctor_id != "")
                    {
                        if (dgv_invoice.Rows.Count == 0)
                        {
                            DialogResult yesno = MessageBox.Show("You missed to Click on 'Add' button under Invoice. Please click 'Add' button to proceed Or Do you want to save without Invoice ?", "Invoice Not Added", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (yesno == DialogResult.No)
                            { return; }
                        }
                        else
                        {
                            string cs = PappyjoeMVC.Model.Connection.MyGlobals.trans_connectionstring;
                            using (MySqlConnection con = new MySqlConnection(cs))
                            {
                                con.Open();
                                MySqlTransaction trans = con.BeginTransaction();

                                try
                                {

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
                                    for (int l = 0; l < dgv_invoice.Rows.Count; l++)
                                    {
                                        this.amodel.save_invoice_items(txt_invoiceno.Text.ToString(), pat_name.Text.ToString(), patient_id, dgv_invoice[0, l].Value.ToString(), dgv_invoice[1, l].Value.ToString(), dgv_invoice[3, l].Value.ToString(), dgv_invoice[2, l].Value.ToString(), dgv_invoice[4, l].Value.ToString(), dgv_invoice[5, l].Value.ToString(), dgv_invoice[6, l].Value.ToString(), dgv_invoice[7, l].Value.ToString(), dgv_invoice[12, l].Value.ToString(), dgv_invoice[8, l].Value.ToString(), dgv_invoice[9, l].Value.ToString(), dgv_invoice[10, l].Value.ToString(), dgv_invoice[8, l].Value.ToString(), dgv_invoice[9, l].Value.ToString(), j, dgv_invoice[14, l].Value.ToString(), Convert.ToInt32(dgv_invoice[15, l].Value.ToString()), dgv_invoice.Rows[l].Cells[16].Value.ToString(), dgv_invoice.Rows[l].Cells[18].Value.ToString(), con, trans);
                                        this.amodel.Set_completed_status0(dgv_invoice[15, l].Value.ToString(), con, trans);
                                    }

                                    this.amodel.update_invoice_nurse_notify("True", dt_1, con, trans);////nurse notification

                                    string invoauto = this.cntrl.get_invoicenumber(con, trans);
                                    int invoautoup = int.Parse(invoauto) + 1;
                                    this.cntrl.update_invnumber(invoautoup.ToString(), con, trans);
                                    string dt = DateTime.Now.Date.ToString("yyyy-MM-dd");
                                    DateTime Timeonly = DateTime.Now;
                                    this.model.save_log(loginid, "Invoice", "Fast Track", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Add", j.ToString(), con, trans);
                                    trans.Commit();
                                    con.Close();
                                    MessageBox.Show( "Data saved successfully !..","Success", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    }
                    else
                    {
                        MessageBox.Show("Doctor not found !..", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Patient not found !..", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
               
            }
            catch(Exception ex)
            {

            }
        }

        private void txt_inv_disc_KeyUp(object sender, KeyEventArgs e)
        {
            calculations();
        }

        private void lst_drug_MouseClick(object sender, MouseEventArgs e)
        {
            id1 = lst_drug.SelectedValue.ToString();
            if (id1 != "")
            {
                DataTable dt = this.pmodel.ge_drug(id1);
                if (dt.Rows.Count > 0)
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

        private void pnl_right_Paint(object sender, PaintEventArgs e)
        {

        }
        public int special_flag = 0;
        private void pb_skin_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.skin;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                    special_flag = 1;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.skin;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                special_flag = 1;
            }
        }

        private void pb_eye_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.eye;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                    special_flag = 1;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.eye;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                special_flag = 1;
            }
        }

        private void pb_tooth_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.AdultTooth;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                    special_flag = 1;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.AdultTooth;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom; 
                special_flag = 1;
            }
        }

        private void pb_nose_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.nose;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                    special_flag = 1;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.nose;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                special_flag = 1;
            }
        }

        private void pb_tongue_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.Tongue;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                    special_flag = 1;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.Tongue;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                special_flag = 1;
            }
        }

        private void pb_ear_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.ear;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                    special_flag = 1;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.ear;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                special_flag = 1;
            }
        }

        private void pb_brain_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.brain;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                    special_flag = 1;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.brain;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                special_flag = 1;
            }
        }

        private void pb_lungs_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.lungs;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                    special_flag = 1;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.lungs;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                special_flag = 1;
            }
        }

        private void pb_liver_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.liver;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                    special_flag = 1;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.liver;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                special_flag = 1;
            }
        }

        private void pb_gallbladder_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.gallbladder;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                    special_flag = 1;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.gallbladder;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                special_flag = 1;
            }
        }

        private void pb_kidneys_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.kidneys;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                    special_flag = 1;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.kidneys;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                special_flag = 1;
            }
        }

        private void pb_femalers_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.female_reproductive_system;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                    special_flag = 1;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.female_reproductive_system;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                special_flag = 1;
            }
        }

        private void pb_intestine_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.instestinies;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                    special_flag = 1;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.instestinies;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                special_flag = 1;
            }
        }

        private void pb_malers_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.male_reproductive_system;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                    special_flag = 1;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.male_reproductive_system;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                special_flag = 1;
            }
        }

        private void pb_Bladder_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.bladder;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                    special_flag = 1;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.bladder;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                special_flag = 1;
            }
        }

        private void pb_pancreas_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.pancreas;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                    special_flag = 1;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.pancreas;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                special_flag = 1;
            }
        }

        private void pb_stomach_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.stomach;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                    special_flag = 1;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.stomach;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                special_flag = 1;
            }
        }

        private void pb_heart_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.heart;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                    special_flag = 1;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.heart;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                special_flag = 1;
            }
        }
        TextBox txt_panel = new TextBox();
        bool isDrag = false;
        int lastY = 0;
        int lastX = 0;
        void txt_panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y >= (txt_panel.ClientRectangle.Bottom - 5) &&
            e.Y <= (txt_panel.ClientRectangle.Bottom + 5))
            {
                ((TextBox)sender).Cursor = Cursors.Cross;

                isDrag = true;
                lastY = e.Y;
            }
            if (e.X >= (txt_panel.ClientRectangle.Right - 5) &&
            e.X <= (txt_panel.ClientRectangle.Right + 5))
            {
                ((TextBox)sender).Cursor = Cursors.Cross;

                isDrag = true;
                lastX = e.X;
            }
        }
        void txt_panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrag)
            {
                ((TextBox)sender).Cursor = Cursors.Cross;

                txt_panel.Height += (e.Y - lastY);
                lastY = e.Y;
            }
            if (isDrag)
            {
                ((TextBox)sender).Cursor = Cursors.Cross;

                txt_panel.Width += (e.X - lastX);
                lastX = e.X;
            }
        }
        void txt_panel_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrag)
            {
                isDrag = false;
                ((TextBox)sender).Cursor = Cursors.Default;

            }
        }

        private void pb_main_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Point s = e.Location;
            txt_panel.Location = s;
            pb_main.Controls.Add(txt_panel);
            txt_panel.Font = new Font("Segoe UI", 10);
            txt_panel.Width = 200;
            txt_panel.Multiline = true;
            txt_panel.MouseDown += new MouseEventHandler(txt_panel_MouseDown);
            txt_panel.MouseMove += new MouseEventHandler(txt_panel_MouseMove);
            txt_panel.MouseUp += new MouseEventHandler(txt_panel_MouseUp);
        }
        Pen pen;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            pen.Color = p.BackColor;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            pen.Color = p.BackColor;
        }
        bool moving = false;
        private void pb_main_MouseDown(object sender, MouseEventArgs e)
        {
            moving = true;
            x = e.X;
            y = e.Y;
        }
        Graphics g;
        int x = -1;
        int y = -1;
        private void pb_main_MouseMove(object sender, MouseEventArgs e)
        {
            {
                if (moving && x != -1 && y != -1)
                {
                    g.DrawLine(pen, new Point(x, y), e.Location);
                    x = e.X;
                    y = e.Y;
                }
            }
        }

        private void pb_main_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;
            x = -1;
            y = -1;
        }

        private void btn_special_save_Click(object sender, EventArgs e)
        {
            Save();
        }
        Add_Attachments_controller cnt_atta = new Add_Attachments_controller();
        public void Save()
        {
            if(patient_id!="")
            {
                if(doctor_id!="")
                {
                    if (special_flag == 0)
                    {
                        MessageBox.Show("Choose an Image First !!", "No Image Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        Bitmap bmp = new Bitmap(pb_main.ClientSize.Width, pb_main.ClientSize.Height, PixelFormat.Format32bppArgb);
                        Graphics g = Graphics.FromImage(bmp);
                        Rectangle rect = pb_main.RectangleToScreen(pb_main.ClientRectangle);
                        g.CopyFromScreen(rect.Location, Point.Empty, pb_main.Size);
                        string server = this.cnt_atta.getserver();
                        string outputFileName = @"\\" + server + "\\Pappyjoe_utilities\\Attachments\\Special_case" + DateTime.Now.ToString("dd - MM - yyyy_hh - mm - ss") + ".jpg";
                        //.............................
                        using (MemoryStream memory = new MemoryStream())
                        {
                            using (FileStream fs = new FileStream(outputFileName, FileMode.Create, FileAccess.ReadWrite))
                            {
                                bmp.Save(memory, ImageFormat.Jpeg);
                                byte[] bytes = memory.ToArray();
                                fs.Write(bytes, 0, bytes.Length);
                                bmp.Dispose();
                            }
                        }
                        string realfile = "Special_case" + DateTime.Now.ToString("dd - MM - yyyy_hh - mm - ss") + ".jpg";
                        string pathimage = "\\" + "\\Pappyjoe_utilities" + "\\" + "\\Attachments\\" + "\\" + realfile;
                        string catgry = "General";
                        this.cnt_atta.insattach(patient_id, realfile, pathimage, doctor_id, catgry);
                        MessageBox.Show("Image saved successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        pb_main.Controls.Remove(btn_spec_clear);
                        pb_main.Controls.Remove(txt_panel);
                        txt_panel.Clear();
                        pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.blank;
                        pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                        bmp = null; pb_main.Invalidate();
                        special_flag = 0;
                    }
                }
                else
                {
                    MessageBox.Show("Choose a doctor First !!", "No doctor Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Choose a patient First !!", "No patient Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btn_spec_clear_Click(object sender, EventArgs e)
        {
            pb_main.Invalidate();
            pb_main.Controls.Remove(btn_spec_clear);
            pb_main.Controls.Remove(txt_panel);
            txt_panel.Clear();
        }
        public int flag_a = 0, len, p2 = 0, p3 = 0, p4 = 0, p5 = 0, p6 = 0, p7 = 0, p8 = 0, p9 = 0;
        public string  pth = "";
        Image photo;
        PictureBox[] pics = new PictureBox[50];

        private void Pb_3_Click(object sender, EventArgs e)
        {
            try
            {
                open4.ShowDialog();
                string ext = Path.GetExtension(open4.FileName);
                if (ext.ToLower() == ".jpeg" || ext.ToLower() == ".jpg" || ext.ToLower() == ".gif" || ext.ToLower() == ".png")
                {
                    Pb_3.Image = Image.FromFile(open4.FileName);
                    Pb_3.BackgroundImageLayout = ImageLayout.Zoom;
                    pth = open4.FileName;
                    p4 = 0;
                    photo = Pb_3.Image;
                    txt_path3.Text = System.IO.Path.GetFileName(open4.FileName);
                }
                else if (ext.ToLower() == ".docx" || ext.ToLower() == ".doc")
                {
                    Pb_3.Image = PappyjoeMVC.Properties.Resources.word_doc_icon;
                    Pb_3.BackgroundImageLayout = ImageLayout.Zoom;
                    txt_path3.Text = System.IO.Path.GetFileName(open4.FileName);
                    p4 = 0;
                }
                else if (ext.ToLower() == ".xls" || ext.ToLower() == ".xlsx")
                {
                    Pb_3.Image = PappyjoeMVC.Properties.Resources.excel_doc_icon;
                    Pb_3.BackgroundImageLayout = ImageLayout.Zoom;
                    txt_path3.Text = System.IO.Path.GetFileName(open4.FileName);
                    p4 = 0;
                }
                else if (ext.ToLower() == ".pdf")
                {
                    Pb_3.Image = PappyjoeMVC.Properties.Resources.pdf;
                    Pb_3.BackgroundImageLayout = ImageLayout.Zoom;
                    txt_path3.Text = System.IO.Path.GetFileName(open4.FileName);
                    p4 = 0;
                }
                flag_a = 1;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Pb_2_Click(object sender, EventArgs e)
        {
            try
            {
                open3.ShowDialog();
                string ext = Path.GetExtension(open3.FileName);
                if (ext.ToLower() == ".jpeg" || ext.ToLower() == ".jpg" || ext.ToLower() == ".gif" || ext.ToLower() == ".png")
                {
                    Pb_2.Image = Image.FromFile(open3.FileName);
                    Pb_2.BackgroundImageLayout = ImageLayout.Zoom;
                    pth = open3.FileName;
                    p3 = 0;
                    photo = Pb_2.Image;
                    txt_Path2.Text = System.IO.Path.GetFileName(open3.FileName);
                }
                else if (ext.ToLower() == ".docx" || ext.ToLower() == ".doc")
                {
                    Pb_2.Image = PappyjoeMVC.Properties.Resources.word_doc_icon;
                    Pb_2.BackgroundImageLayout = ImageLayout.Zoom;
                    txt_Path2.Text = System.IO.Path.GetFileName(open3.FileName);
                    p3 = 0;
                }
                else if (ext.ToLower() == ".xls" || ext.ToLower() == ".xlsx")
                {
                    Pb_2.Image = PappyjoeMVC.Properties.Resources.excel_doc_icon;
                    Pb_2.BackgroundImageLayout = ImageLayout.Zoom;
                    txt_Path2.Text = System.IO.Path.GetFileName(open3.FileName);
                    p3 = 0;
                }
                else if (ext.ToLower() == ".pdf")
                {
                    Pb_2.Image = PappyjoeMVC.Properties.Resources.pdf;
                    Pb_2.BackgroundImageLayout = ImageLayout.Zoom;
                    txt_Path2.Text = System.IO.Path.GetFileName(open3.FileName);
                    p3 = 0;
                }
                flag_a = 1;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Pb_5_Click(object sender, EventArgs e)
        {
            try
            {
                open6.ShowDialog();
                string ext = Path.GetExtension(open5.FileName);
                if (ext.ToLower() == ".jpeg" || ext.ToLower() == ".jpg" || ext.ToLower() == ".gif" || ext.ToLower() == ".png")
                {
                    Pb_5.Image = Image.FromFile(open5.FileName);
                    Pb_5.BackgroundImageLayout = ImageLayout.Zoom;
                    pth = open6.FileName;
                    p6 = 0;
                    photo = Pb_5.Image;
                    txt_path5.Text = System.IO.Path.GetFileName(open6.FileName);
                }
                else if (ext.ToLower() == ".docx" || ext.ToLower() == ".doc")
                {
                    Pb_5.Image = PappyjoeMVC.Properties.Resources.word_doc_icon;
                    Pb_5.BackgroundImageLayout = ImageLayout.Zoom;
                    txt_path5.Text = System.IO.Path.GetFileName(open6.FileName);
                    p6 = 0;
                }
                else if (ext.ToLower() == ".xls" || ext.ToLower() == ".xlsx")
                {
                    Pb_5.Image = PappyjoeMVC.Properties.Resources.excel_doc_icon;
                    Pb_5.BackgroundImageLayout = ImageLayout.Zoom;
                    txt_path5.Text = System.IO.Path.GetFileName(open6.FileName);
                    p6 = 0;
                }
                else if (ext.ToLower() == ".pdf")
                {
                    Pb_5.Image = PappyjoeMVC.Properties.Resources.pdf;
                    Pb_5.BackgroundImageLayout = ImageLayout.Zoom;
                    txt_path5.Text = System.IO.Path.GetFileName(open6.FileName);
                    p6 = 0;
                }
                flag_a = 1;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void PB_6_Click(object sender, EventArgs e)
        {
            try
            {
                open7.ShowDialog();
                string ext = Path.GetExtension(open6.FileName);
                if (ext.ToLower() == ".jpeg" || ext.ToLower() == ".jpg" || ext.ToLower() == ".gif" || ext.ToLower() == ".png")
                {
                    PB_6.Image = Image.FromFile(open7.FileName);
                    PB_6.BackgroundImageLayout = ImageLayout.Zoom;
                    pth = open7.FileName;
                    p7 = 0;
                    photo = PB_6.Image;
                    txt_path6.Text = System.IO.Path.GetFileName(open7.FileName);
                }
                else if (ext.ToLower() == ".docx" || ext.ToLower() == ".doc")
                {
                    PB_6.Image = PappyjoeMVC.Properties.Resources.word_doc_icon;
                    PB_6.BackgroundImageLayout = ImageLayout.Zoom;
                    txt_path6.Text = System.IO.Path.GetFileName(open7.FileName);
                    p7 = 0;
                }
                else if (ext.ToLower() == ".xls" || ext.ToLower() == ".xlsx")
                {
                    PB_6.Image = PappyjoeMVC.Properties.Resources.excel_doc_icon;
                    PB_6.BackgroundImageLayout = ImageLayout.Zoom;
                    txt_path6.Text = System.IO.Path.GetFileName(open7.FileName);
                    p7 = 0;
                }
                else if (ext.ToLower() == ".pdf")
                {
                    PB_6.Image = PappyjoeMVC.Properties.Resources.pdf;
                    PB_6.BackgroundImageLayout = ImageLayout.Zoom;
                    txt_path6.Text = System.IO.Path.GetFileName(open7.FileName);
                    p7 = 0;
                }
                flag_a = 1;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btn_Delete1_Click(object sender, EventArgs e)
        {
            PB_1.Image = PappyjoeMVC.Properties.Resources.upload;
            txt_Path1.Clear();
            p2 = 1;
        }

        private void btn_Delete3_Click(object sender, EventArgs e)
        {
            Pb_3.Image = PappyjoeMVC.Properties.Resources.upload;
            txt_path3.Clear();
            p4 = 1;
        }

        private void btn_Delete2_Click(object sender, EventArgs e)
        {
            Pb_2.Image = PappyjoeMVC.Properties.Resources.upload;
            txt_Path2.Clear();
            p3 = 1;
        }

        private void btn_Delete4_Click(object sender, EventArgs e)
        {
            PB_4.Image = PappyjoeMVC.Properties.Resources.upload;
            txt_path4.Clear();
            p5 = 1;
        }

        private void btn_Delete5_Click(object sender, EventArgs e)
        {
            Pb_5.Image = PappyjoeMVC.Properties.Resources.upload;
            txt_path5.Clear();
            p6 = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PB_6.Image = PappyjoeMVC.Properties.Resources.upload;
            txt_path6.Clear();
            p7 = 1;
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.TabPages.Count== 6)
            {
                DataTable catgry = this.cnt_atta.GetCategory();
                GetCategory(catgry);
            }
          
        }
        public DataTable GetCategory(DataTable dtcatgry)
        {
            try
            {
                if (dtcatgry.Rows.Count > 0)
                {
                    Cmb_category1.DisplayMember = "CategoryName";
                    Cmb_category1.ValueMember = "id";
                    Cmb_category1.DataSource = dtcatgry;
                    Cmb_category1.SelectedIndex = 0;
                    Cmb_category2.BindingContext = new BindingContext();
                    Cmb_category2.DisplayMember = "CategoryName";
                    Cmb_category2.ValueMember = "id";
                    Cmb_category2.DataSource = dtcatgry;
                    Cmb_category2.SelectedIndex = 0;
                    Cmb_category3.BindingContext = new BindingContext();
                    Cmb_category3.DisplayMember = "CategoryName";
                    Cmb_category3.ValueMember = "id";
                    Cmb_category3.DataSource = dtcatgry;
                    Cmb_category3.SelectedIndex = 0;
                    Cmb_category4.BindingContext = new BindingContext();
                    Cmb_category4.DisplayMember = "CategoryName";
                    Cmb_category4.ValueMember = "id";
                    Cmb_category4.DataSource = dtcatgry;
                    Cmb_category4.SelectedIndex = 0;
                    Cmb_category5.BindingContext = new BindingContext();
                    Cmb_category5.DisplayMember = "CategoryName";
                    Cmb_category5.ValueMember = "id";
                    Cmb_category5.DataSource = dtcatgry;
                    Cmb_category5.SelectedIndex = 0;
                    Cmb_category6.BindingContext = new BindingContext();
                    Cmb_category6.DisplayMember = "CategoryName";
                    Cmb_category6.ValueMember = "id";
                    Cmb_category6.DataSource = dtcatgry;
                    Cmb_category6.SelectedIndex = 0;
                  
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            return dtcatgry;
        }

        private void bgn_attachmnt_save_Click(object sender, EventArgs e)
        {
            try
            {
              
                bool p2flg = false; bool p3flg = false; bool p4flg = false; bool p5flg = false; bool p6flg = false; bool p7flg = false;
                if (patient_id !="")
                {
                    if(doctor_id!="")
                    {
                        if (flag_a == 0)
                        {
                            MessageBox.Show("Choose an Image First !!", "No Image Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            string server = this.cnt_atta.getserver();

                            if(p2==0)
                            {
                                string realfile = System.IO.Path.GetFileName(open2.FileName);
                                string img_name = this.cnt_atta.get_imgname(realfile, patient_id);
                                if (img_name == "0")
                                {
                                   
                                }
                                else
                                {
                                    MessageBox.Show(realfile + "This image already exsist !..", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            if(p3==0)
                            {
                                string realfile = System.IO.Path.GetFileName(open3.FileName);
                                string img_name = this.cnt_atta.get_imgname(realfile, patient_id);
                                if (img_name == "0")
                                {
                                    
                                }
                                else
                                {
                                    MessageBox.Show(realfile + "This image already exsist !..", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            if(p4==0)
                            {
                                string realfile = System.IO.Path.GetFileName(open4.FileName);
                                string img_name = this.cnt_atta.get_imgname(realfile, patient_id);
                                if (img_name == "0")
                                {
                                   
                                }
                                else
                                {
                                    MessageBox.Show(realfile + "This image already exsist !..", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                           if(p5==0)
                            {
                                string realfile = System.IO.Path.GetFileName(open5.FileName);
                                string img_name = this.cnt_atta.get_imgname(realfile, patient_id);
                                if (img_name == "0")
                                {
                                    
                                }
                                else
                                {
                                    MessageBox.Show(realfile + "This image already exsist !..", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                            }
                           if(p6==0)
                            {
                                string realfile = System.IO.Path.GetFileName(open6.FileName);
                                string img_name = this.cnt_atta.get_imgname(realfile, patient_id);
                                if (img_name == "0")
                                {
                                   
                                }
                                else
                                {
                                    MessageBox.Show(realfile + "This image already exsist !..", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                           if(p7==0)
                            {
                                string realfile = System.IO.Path.GetFileName(open7.FileName);
                                string img_name = this.cnt_atta.get_imgname(realfile, patient_id);
                                if (img_name == "0")
                                {
                                    
                                }
                                else
                                {
                                    MessageBox.Show(realfile + "This image already exsist !..", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            if (p2 == 0)
                            {
                                string realfile = System.IO.Path.GetFileName(open2.FileName);
                                string catName = Cmb_category1.GetItemText(Cmb_category1.SelectedItem);
                                txt_Path1.Text = realfile;
                                txt_Path2.Text = catName;
                                save_image(realfile, server, open2.FileName, catName);
                            }
                            if (p3 == 0)
                            {
                                string realfile = System.IO.Path.GetFileName(open3.FileName);
                                string catName = Cmb_category2.GetItemText(Cmb_category2.SelectedItem);
                                txt_Path1.Text = realfile;
                                txt_Path2.Text = catName;
                                save_image(realfile, server, open3.FileName, catName);

                            }
                            if (p4 == 0)
                            {
                                string realfile = System.IO.Path.GetFileName(open4.FileName);
                                string catName = Cmb_category3.GetItemText(Cmb_category3.SelectedItem);
                                txt_Path1.Text = realfile;
                                txt_Path2.Text = catName;
                                save_image(realfile, server, open4.FileName, catName);
                            }
                            if (p5 == 0)
                            {
                                string realfile = System.IO.Path.GetFileName(open5.FileName);
                                string catName = Cmb_category4.GetItemText(Cmb_category4.SelectedItem);
                                txt_Path1.Text = realfile;
                                save_image(realfile, server, open5.FileName, catName);
                            }
                            if (p6 == 0)
                            {
                                string realfile = System.IO.Path.GetFileName(open6.FileName);
                                string catName = Cmb_category5.GetItemText(Cmb_category5.SelectedItem);
                                txt_Path1.Text = realfile;
                                txt_Path2.Text = catName;
                                save_image(realfile, server, open6.FileName, catName);
                            }
                            if (p7 == 0)
                            {
                                string realfile = System.IO.Path.GetFileName(open7.FileName);
                                string catName = Cmb_category6.GetItemText(Cmb_category6.SelectedItem);
                                txt_Path1.Text = realfile;
                                txt_Path2.Text = catName;
                                save_image(realfile, server, open7.FileName, catName);
                            }
                        }
                        if(img_flag==true)
                        {
                            MessageBox.Show("Image saved successfully !..", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Pb_2.Image = PappyjoeMVC.Properties.Resources.upload;
                            Pb_5.Image = PappyjoeMVC.Properties.Resources.upload;
                            PB_1.Image = PappyjoeMVC.Properties.Resources.upload;
                            Pb_3.Image = PappyjoeMVC.Properties.Resources.upload;
                            PB_4.Image = PappyjoeMVC.Properties.Resources.upload;
                            PB_6.Image = PappyjoeMVC.Properties.Resources.upload;
                            open2.FileName = "";
                            open3.FileName = "";
                            open4.FileName = "";
                            open5.FileName = "";
                            open6.FileName = "";
                            open7.FileName = "";
                            Cmb_category6.SelectedIndex = 0;
                            Cmb_category5.SelectedIndex = 0;
                            Cmb_category4.SelectedIndex = 0;
                            Cmb_category3.SelectedIndex = 0;
                            Cmb_category2.SelectedIndex = 0;
                            Cmb_category1.SelectedIndex = 0;
                            flag_a = 0;
                            img_flag = false;
                        }

                      
                    }
                    else
                    {
                        MessageBox.Show("Choose a doctor!!", "No doctor Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Choose a patient First !!", "No patient Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
               
               
               
            }
            catch (Exception ex)
            { MessageBox.Show("Error!..", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        public bool img_flag = false;
        public void save_image(string realfile, string server, string filename, string catgry)
        {
            if (realfile != "")
            {
              
                    try
                    {
                        if (File.Exists(@"\\" + server + "\\Pappyjoe_utilities\\Attachments\\" + realfile))
                        {
                        }
                        else
                        {
                            System.IO.File.Copy(filename, @"\\" + server + "\\Pappyjoe_utilities\\Attachments\\" + realfile);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    string pathimage = "\\" + "\\Pappyjoe_utilities" + "\\" + "\\Attachments\\" + "\\" + realfile;
                    txt_path3.Text = pathimage;
                    this.cnt_atta.insattach(patient_id, realfile, pathimage, doctor_id, catgry);
                img_flag = true;
            }
        }
      

        Connection db = new Connection(); string  ptname = "";
        public static string newptid = "";
        private void btn_change_patients_Click_1(object sender, EventArgs e)
        {
            txt_search.Visible = true;
            label15.Visible = false;
            label7.Visible = false;
            pat_name.Visible = false;
            lb_mobile.Visible = false;
            label29.Visible = false;
            label14.Visible = false;
            lb_gender.Visible = false; lb_showmore.Visible = false;
            flowLayoutPanel2.Controls.Clear();
            rowcount = 0; clear();

        }
        public int rowcount = 0;
        private void lb_showmore_Click(object sender, EventArgs e)
        {
            showmore_click_flag = true;
            if (show_flag==true)
            {
                int count = rowcount + 2;
                fill_grp_by_visit(count);// fillgrid(count);
                rowcount = count;
            }
            else
            {
                int count = rowcount + 2;
                grp_by_para_showmore(count);// fillgrid(count);
                rowcount = count;
            }
            
        }

        private void btn_newPatient_Click(object sender, EventArgs e)
        {
            listpatientsearch.Visible = false;
            txt_search.Visible = true;
            label15.Visible = false;
            label7.Visible = false;
            pat_name.Visible = false; lb_showmore.Visible = false;
            lb_mobile.Visible = false;
            label29.Visible = false;
            label14.Visible = false;
            lb_gender.Visible = false;
            flowLayoutPanel2.Controls.Clear();
            rowcount = 0;
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
                    if (newptid != "")
                    {
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
                        rjCmb_doctor.SelectedItem = str_doctorname;
                        dr_id = rjCmb_doctor.SelectedItem.ToString();
                        DataTable dt_doctor_id = this.cntrl.get_doctorname(dr_id);
                    }
                    flag = false;
                }
            }
            else
            {
                var form2 = new consultation_new_patient();
                form2.ShowDialog();
                //form2.Dispose();
                if (newptid != "")
                {
                    
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
                    rjCmb_doctor.SelectedItem = str_doctorname;
                    dr_id = rjCmb_doctor.SelectedItem.ToString();
                    DataTable dt_doctor_id = this.cntrl.get_doctorname(dr_id);
                }
                flag = false;
            }
        }

        OpenFileDialog open2 = new OpenFileDialog();

        private void txt_temp_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txt_pluse_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txt_blood1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txt_blood2_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txt_spo_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void dgv_prescrptn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
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
                                dgv_prescrptn.Rows.RemoveAt(this.dgv_prescrptn.CurrentRow.Index);
                            }
                        }
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dgv_invoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    if (dgv_invoice.Rows.Count > 0)
                    {
                        if (e.ColumnIndex == 18)
                        {
                            DialogResult res = MessageBox.Show("Are you sure you want to delete..?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (res == DialogResult.Yes)
                            {
                                dgv_invoice.Rows.RemoveAt(this.dgv_invoice.CurrentRow.Index);
                                if (dgv_invoice.Rows.Count == 0)
                                {
                                    txt_inv_t_cost.Text = "0.00";
                                    txtTotaltax.Text = "0.00";
                                    txt_inv_T_disc.Text = "0.00";
                                    txt_inv_grand.Text = "0.00";
                                }
                                else
                                {
                                    delete_invoice_calculation();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
        public void delete_invoice_calculation()
        {
            decimal totcost = 0;
            decimal discount1 = 0, tax1 = 0, grand = 0;
            for (int i = 0; i < dgv_invoice.Rows.Count; i++)
            {
                totcost = totcost + (decimal.Parse(dgv_invoice.Rows[i].Cells["cost_"].Value.ToString()));// * decimal.Parse(dgv_invoice.Rows[i].Cells[3].Value.ToString()));
                discount1 = discount1 + decimal.Parse(dgv_invoice.Rows[i].Cells["dis_ins"].Value.ToString());//discounttotal 9
                tax1 = tax1 + decimal.Parse(dgv_invoice.Rows[i].Cells["tax"].Value.ToString());//taxrstotal 6
                grand = grand + decimal.Parse(dgv_invoice.Rows[i].Cells["total"].Value.ToString());
            }
            txt_inv_t_cost.Text = totcost.ToString("F");
            txt_inv_T_disc.Text = discount1.ToString("F");
            txtTotaltax.Text = tax1.ToString("F");
            decimal d1 = Convert.ToDecimal((totcost + tax1) - discount1);
            txt_inv_grand.Text = grand.ToString("F");// Convert.ToString((totcost + tax1) - discount1);
            txt_inv_procedure.Text = "";
            txt_inv_cost.Text = "";
            txt_inv_unit.Text = ""; txt_inv_total.Text = "";
        }
        private void rjButtons2_Click(object sender, EventArgs e)
        {
            if (strApp_id != "")
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

        private void tab_clinical_Click(object sender, EventArgs e)
        {

        }

        private void dgv_prescription_search_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id1 = dgv_prescription_search.CurrentRow.Cells["prid"].Value.ToString();// lst_drug.SelectedValue.ToString();
            if (id1 != "")
            {
                DataTable dt = this.pmodel.ge_drug(id1);
                if (dt.Rows.Count > 0)
                {
                    drug_type = dt.Rows[0][3].ToString();
                }
                else
                    drug_type = "";

                userControl_textbox10.Text = dgv_prescription_search.CurrentRow.Cells[1].Value.ToString();// lst_drug.Text.ToString();
                userControl_textbox10.ForeColor = Color.DarkBlue;
            }
            dgv_prescription_search.Visible = false;
        }

        private void PB_4_Click(object sender, EventArgs e)
        {
            try
            {
                open8.ShowDialog();
                string ext = Path.GetExtension(open8.FileName);
                if (ext.ToLower() == ".jpeg" || ext.ToLower() == ".jpg" || ext.ToLower() == ".gif" || ext.ToLower() == ".png")
                {
                    PB_4.Image = Image.FromFile(open3.FileName);
                    PB_4 .BackgroundImageLayout = ImageLayout.Zoom;
                    pth = open8.FileName;
                    p8 = 0;
                    photo = PB_4.Image;
                    txt_path4.Text = System.IO.Path.GetFileName(open8.FileName);
                }
                else if (ext.ToLower() == ".docx" || ext.ToLower() == ".doc")
                {
                    PB_4.Image = PappyjoeMVC.Properties.Resources.word_doc_icon;
                    PB_4.BackgroundImageLayout = ImageLayout.Zoom;
                    txt_path4.Text = System.IO.Path.GetFileName(open8.FileName);
                    p8 = 0;
                }
                else if (ext.ToLower() == ".xls" || ext.ToLower() == ".xlsx")
                {
                    PB_4.Image = PappyjoeMVC.Properties.Resources.excel_doc_icon;
                    PB_4.BackgroundImageLayout = ImageLayout.Zoom;
                    txt_path4.Text = System.IO.Path.GetFileName(open8.FileName);
                    p8 = 0;
                }
                else if (ext.ToLower() == ".pdf")
                {
                    PB_4.Image = PappyjoeMVC.Properties.Resources.pdf;
                    PB_4.BackgroundImageLayout = ImageLayout.Zoom;
                    txt_path4.Text = System.IO.Path.GetFileName(open8.FileName);
                    p8 = 0;
                }
                flag_a = 1;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void cmb_inv_discound_onSelectedIndexChanged(object sender, EventArgs e)
        {
            if (load_flag == false)
            {
                calculations();
            }
        }

        private void txt_pluse_KeyUp(object sender, KeyEventArgs e)
        {
            if (txt_pluse.Text != "")
            {
                if (decimal.Parse(txt_pluse.Text) < 10)
                {
                    label20.Show();
                    label20.Text = "Pulse can't be less than 10";
                }
                else if (decimal.Parse(txt_pluse.Text) > 200)
                {
                    label20.Show();
                    label20.Text = "Pulse can't be greater than 200";
                }
                else
                {
                    label20.Hide();
                }
            }
            else
            {
                label20.Hide();
            }
        }

        private void txt_temp_KeyUp(object sender, KeyEventArgs e)
        {
            if (txt_temp.Text != "")
            {
                if (decimal.Parse(txt_temp.Text) < 10)
                {
                    label23.Show();
                    label23.Text = "Temperature can't be less than 10";
                }
                else if (decimal.Parse(txt_temp.Text) > 111)
                {
                    label23.Show();
                    label23.Text = "Temperature can't be greater than 111";
                }
                else
                {
                    label23.Hide();
                }
            }
            else
            {
                label23.Hide();
            }
        }

        private void txt_blood1_KeyUp(object sender, KeyEventArgs e)
        {
            if (txt_blood1.Text != "")
            {
                if (decimal.Parse(txt_blood1.Text) < 50)
                {
                    label22.Show();
                    label22.Text = "Systolic blood pressure can't be less than 50";
                }
                else if (decimal.Parse(txt_blood1.Text) > 300)
                {
                    label22.Show();
                    label22.Text = "Systolic blood pressure can't be more than 300";
                }
                else
                {
                    label22.Hide();
                }
            }
            else
            {
                label22.Hide();
            }
        }

        private void txt_resp_KeyUp(object sender, KeyEventArgs e)
        {
            if (txt_resp.Text != "")
            {
                if (decimal.Parse(txt_resp.Text) < 10)
                {
                    label26.Show();
                    label26.Text = "Respiratory rate can't be less than 10";
                }
                else if (decimal.Parse(txt_resp.Text) > 70)
                {
                    label26.Show();
                    label26.Text = "Respiratory rate can't be more than 70";
                }
                else
                {
                    label26.Hide();
                }
            }
            else
            {
                label26.Hide();
            }
        }

        OpenFileDialog open3 = new OpenFileDialog();

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt_height_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txt_weight_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 && !char.IsDigit(ch) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txt_weight_KeyUp(object sender, KeyEventArgs e)
        {
            if (txt_weight.Text != "")
            {
                BMI_Calculate(lb_gender.Text);
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
        OpenFileDialog open4 = new OpenFileDialog();
        OpenFileDialog open5 = new OpenFileDialog();
        OpenFileDialog open6 = new OpenFileDialog();
        OpenFileDialog open7 = new OpenFileDialog();
        OpenFileDialog open8 = new OpenFileDialog();
        OpenFileDialog open9 = new OpenFileDialog();
        private void PB_1_Click(object sender, EventArgs e)
        {
            try
            {
                open2.ShowDialog();
                string ext = Path.GetExtension(open2.FileName);
                if (ext.ToLower() == ".jpeg" || ext.ToLower() == ".jpg" || ext.ToLower() == ".gif" || ext.ToLower() == ".png")
                {
                    PB_1.Image = Image.FromFile(open2.FileName);
                    PB_1.BackgroundImageLayout = ImageLayout.Zoom;
                    pth = open2.FileName;
                    p2 = 0;
                    photo = PB_1.Image;
                    txt_Path1.Text = System.IO.Path.GetFileName(open2.FileName);
                }
                else if (ext.ToLower() == ".docx" || ext.ToLower() == ".doc")
                {
                    PB_1.Image = PappyjoeMVC.Properties.Resources.word_doc_icon;
                    PB_1.BackgroundImageLayout = ImageLayout.Zoom;
                    txt_Path1.Text = System.IO.Path.GetFileName(open2.FileName);
                    p2 = 0;
                }
                else if (ext.ToLower() == ".xls" || ext.ToLower() == ".xlsx")
                {
                    PB_1.Image = PappyjoeMVC.Properties.Resources.excel_doc_icon;
                    PB_1.BackgroundImageLayout = ImageLayout.Zoom;
                    txt_Path1.Text = System.IO.Path.GetFileName(open2.FileName);
                    p2 = 0;
                }
                else if (ext.ToLower() == ".pdf")
                {
                    PB_1.Image = PappyjoeMVC.Properties.Resources.pdf;
                    PB_1.BackgroundImageLayout = ImageLayout.Zoom;
                    txt_Path1.Text = System.IO.Path.GetFileName(open2.FileName);
                    p2 = 0;
                }
                flag_a = 1;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            printboth(pres_id.ToString(),treamt_id,vital_id,clinic_id);
        }
        string logo_name = "";
        Prescription_model pmodel = new Prescription_model();
        public void printboth(string Prescription_id, string invoice_id, string vital_id, string clinic_id)
        {
            try
            {
                System.Data.DataTable dtp = this.model.get_company_details();
                System.Data.DataTable dt1 = this.model.Get_Patient_Details(patient_id);
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
                string doctor = this.model.Get_DoctorName(doctor_id.ToString());
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
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                StreamWriter sWrite = new StreamWriter(Apppath + "\\fasttrack.html");
                sWrite.WriteLine("<html>");
                sWrite.WriteLine("<head>");
                sWrite.WriteLine("</head>");
                sWrite.WriteLine("<body >");
                sWrite.WriteLine("<br>");
                if ( logo_name != "")//logo != null ||
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
                sWrite.WriteLine("<tr><td colspan=2><hr></td></tr>");
                string sexage = ""; string age = ""; string days = "";
                int Dexist = 0;
                
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
                //doctor
                sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3><b>Doctor Details<b></FONT></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>Doctor Name:&nbsp;" + rjCmb_doctor.SelectedItem.ToString() + " </font></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine("<tr><td colspan=2><hr></td></tr>");
                sWrite.WriteLine("</table>");
                if (vital_print == true)//vital
                {
                    DataTable dt_vital = this.cntrl.previous_vital_details(patient_id,DateTime.Now.Date.ToString("yyyy-MM-dd"));
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");

                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3><b>Vitals Signs</b></FONT></td>");
                    sWrite.WriteLine(" </tr>");

                    sWrite.WriteLine("<tr >");
                    sWrite.WriteLine("<td align='left' width='20px' height='8'><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;</font></td>");
                    sWrite.WriteLine("<td align='left' width='160px' ><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;</font></td>");
                    sWrite.WriteLine(" </tr>");
                    if(dt_vital.Rows.Count>0)
                    {
                        sWrite.WriteLine(" <tr>");
                        sWrite.WriteLine("<td align='left'  width='20px' height='30' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Pulse( Heart Beats Per Minute )  </font></td>");
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
                        sWrite.WriteLine("<td align='left' width='160px' height='30'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> " + dt_vital.Rows[0]["pulse"].ToString() + " " + label10.Text + "</font></td>");
                        sWrite.WriteLine(" </tr>");
                    }

                    sWrite.WriteLine("<tr><td colspan=2><hr></td></tr>");
                    sWrite.WriteLine("</table>");
                }
                if (clinic_print == true)
                {
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3><b>Clinical Findings<b></FONT></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine("<tr >");
                    sWrite.WriteLine("<td align='left' width='20px' height='8'><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;</font></td>");
                    sWrite.WriteLine("<td align='left' width='160px' ><FONT COLOR=black FACE=' Segoe UI' SIZE=3>&nbsp;</font></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine(" </br>");
                    DataTable dt_clin = this.cntrl.get_clinic_id(patient_id, DateTime.Now.Date.ToString("yyyy-MM-dd"));
                    if(dt_clin.Rows.Count>0)
                    {
                        DataTable dt_cheif = this.cntrl.pt_complaints(dt_clin.Rows[0][0].ToString());
                        if (dt_cheif.Rows.Count > 0)
                        {
                            sWrite.WriteLine("<tr>");
                            sWrite.WriteLine("<th align='left' width='230px' bgcolor='#0099FF' ><FONT COLOR=white FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;<b>COMPLAINTS</b></font></th>");
                            sWrite.WriteLine(" </br>");
                            sWrite.WriteLine("</tr>");
                            for (int i = 0; i < dt_cheif.Rows.Count; i++)
                            {
                                sWrite.WriteLine("<tr>");
                                sWrite.WriteLine("<td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;" + " " + "" + dt_cheif.Rows[i]["complaint_id"].ToString() + "</font></td>");
                                sWrite.WriteLine("</tr>");
                            }
                        }
                        DataTable dt_dia = this.cntrl.pt_diagnosis(dt_clin.Rows[0][0].ToString());
                        if (dt_dia.Rows.Count > 0)
                        {
                            sWrite.WriteLine("<tr>");
                            sWrite.WriteLine("<th align='left' width='230px' bgcolor='#0099FF' ><FONT COLOR=white FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;DIAGNOSIS</font></th>");
                            sWrite.WriteLine("</tr>");
                            for (int i = 0; i < dt_dia.Rows.Count; i++)
                            {
                                sWrite.WriteLine("<tr>");
                                sWrite.WriteLine("<td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;" + dt_dia.Rows[i]["diagnosis_id"].ToString() + "</font></td>");
                                sWrite.WriteLine("</tr>");
                            }
                        }
                        DataTable dt_note = this.cntrl.pt_notes(dt_clin.Rows[0][0].ToString());
                        if (dt_note.Rows.Count > 0)
                        {
                            sWrite.WriteLine("<tr>");
                            sWrite.WriteLine("<th align='left' width='230px' bgcolor='#0099FF' ><FONT COLOR=white FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;NOTE</font></th>");
                            sWrite.WriteLine("</tr>");
                            for (int i = 0; i < dt_note.Rows.Count; i++)
                            {
                                sWrite.WriteLine("<tr>");
                                sWrite.WriteLine("<td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;" + dt_note.Rows[i]["note_name"].ToString() + "</font></td>");
                                sWrite.WriteLine("</tr>");
                            }
                        }
                    }
                    sWrite.WriteLine("<tr><td colspan=2><hr></td></tr>");
                    sWrite.WriteLine("</table>");
                }

                if (pres_print == true)
                {
                    sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3><b>Prescription</b></FONT></td>");
                    sWrite.WriteLine("<td width=250px></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("</table>");
                    sWrite.WriteLine(" </br>");
                    sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left'  width='50'  height='14'><FONT COLOR=black FACE='Segoe UI' SIZE=3>Slno</font></td>");
                    sWrite.WriteLine("<td align='left' width='150' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Segoe UI' SIZE=3>Drug Name</font></td>");
                    sWrite.WriteLine("<td align='left'  width='35' height='14'><FONT COLOR=black FACE='Segoe UI' SIZE=3>Strength</font></td>");
                    sWrite.WriteLine("<td align='left'  width='90'height='14'><FONT COLOR=black FACE='Segoe UI' SIZE=3>Frequency</font></td>");
                    sWrite.WriteLine("<td align='left'  width='70'height='14'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3>Duration</font></td>");
                    sWrite.WriteLine("<td align='left'  width='80'height='14'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3>Instruction</font></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine(" </br>");
                    int i = 1;
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
                            sWrite.WriteLine("<tr>");
                            sWrite.WriteLine("<td align='center'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + i + " </font></td>");
                            sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + dt_prescription.Rows[k]["drug_type"].ToString() + " " + dt_prescription.Rows[k]["drug_name"].ToString() + " </font></td>");
                            sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + dt_prescription.Rows[k]["strength"].ToString() + " " + dt_prescription.Rows[k]["strength_gr"].ToString() + "</font></td>");
                            sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> (" + morning + "- "+ noon + "- "+ night + ") </font></td>");
                            sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + duration + " </font></td>");
                            sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + dt_prescription.Rows[k]["add_instruction"].ToString() + " </font></td>");
                            i = i + 1;
                            sWrite.WriteLine("</tr>");
                        }
                    }
                    sWrite.WriteLine("<tr><td align='left' colspan=6><hr/></td></tr>");
                    sWrite.WriteLine("</table>");
                }
                if (treat_print == true)
                {
                    sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3><b>Invoice</b></FONT></td>");
                    sWrite.WriteLine("<td width=250px></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("</table>");
                    sWrite.WriteLine(" </br>");
                    sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>Invoice No :&nbsp; " + txt_invoiceno.Text + "</FONT></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("</table>");
                    sWrite.WriteLine(" </br>");
                    sWrite.WriteLine("<table align='center'   style='width:700px;border: 1px ;border-collapse: collapse;' >");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left'  width='150'  height='14'><FONT COLOR=black FACE='Segoe UI' SIZE=3>Procedure Name</font></td>");
                    sWrite.WriteLine("<td align='left' width='50' bgcolor='#CCCCCC'><FONT COLOR=black FACE=' Segoe UI' SIZE=3>SAC</font></td>");
                    sWrite.WriteLine("<td align='right'  width='35' height='14'><FONT COLOR=black FACE='Segoe UI' SIZE=3>Unit</font></td>");
                    sWrite.WriteLine("<td align='right'  width='90'height='14'><FONT COLOR=black FACE='Segoe UI' SIZE=3>Cost</font></td>");
                    sWrite.WriteLine("<td align='right'  width='70'height='14'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3>Tax</font></td>");
                    sWrite.WriteLine("<td align='right'  width='80'height='14'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3>Discount</font></td>");
                    sWrite.WriteLine("<td align='right'  width='80'height='14'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3>Total</font></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine(" </br>");
                    int i = 1;
                    for (int k = 0; k < dgv_invoice.Rows.Count; k++)
                    {
                        string sac = this.cntrl.get_sac(dgv_invoice.Rows[k].Cells[0].Value.ToString());
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + dgv_invoice.Rows[k].Cells[1].Value.ToString() + " </font></td>");
                        if (sac != "")
                            sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + sac + " </font></td>");
                        else
                            sWrite.WriteLine("<td align='center'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> </font></td>");
                        sWrite.WriteLine("<td align='right'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + dgv_invoice.Rows[k].Cells[3].Value.ToString() + " </font></td>");
                        sWrite.WriteLine("<td align='right'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + dgv_invoice.Rows[k].Cells[2].Value.ToString() + " </font></td>");
                        sWrite.WriteLine("<td align='right'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + dgv_invoice.Rows[k].Cells["tax"].Value.ToString() + " </font></td>");
                        sWrite.WriteLine("<td align='right'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + dgv_invoice.Rows[k].Cells["dis_ins"].Value.ToString() + " </font></td>");
                        sWrite.WriteLine("<td align='right'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>" + dgv_invoice.Rows[k].Cells["total"].Value.ToString() + " </font></td>");
                        i = i + 1;
                        sWrite.WriteLine("</tr>");
                    }
                    sWrite.WriteLine("<tr><td align='left' colspan=7><hr/></td></tr>");
                    sWrite.WriteLine("<tr >");
                    sWrite.WriteLine("<td align='Right' colspan=6  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + "Total Cost :" + " </font></td>");
                    sWrite.WriteLine("<td align='Right' colspan=7  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(txt_inv_t_cost.Text.ToString())) + " </font></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr >");
                    sWrite.WriteLine("<td align='Right' colspan=6  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + "Total Discount :" + " </font></td>");
                    sWrite.WriteLine("<td align='Right' colspan=7  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(txt_inv_T_disc.Text.ToString())) + " </font></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr >");
                    sWrite.WriteLine("<td align='Right' colspan=6  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + "Total Tax :" + " </font></td>");
                    sWrite.WriteLine("<td align='Right' colspan=7  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(txtTotaltax.Text.ToString())) + " </font></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr >");
                    sWrite.WriteLine("<td align='Right' colspan=6  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + "Grand Total:" + " </font></td>");
                    sWrite.WriteLine("<td align='Right' colspan=7  ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>&nbsp;" + String.Format("{0:C2}", decimal.Parse(txt_inv_grand.Text.ToString())) + " </font></td>");
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
        private void userControl_textbox10_KeyUp(object sender, KeyEventArgs e)
        {
            if (flag == false)
            {
                dgv_prescription_search.Rows.Clear();
                if (userControl_textbox10.Text != "")
                {
                    string strstock = "";
                    // lbPatient.Show();
                    //listpatientsearch.Location = new Point(45, 84);// txt_Pt_search.Location.X, 110);
                    DataTable dtdr = this.cntrl.get_prescriptnwthname(userControl_textbox10.Text);//srch_patient(txt_Pt_search.Text, txt_Pt_search.Text);
                    if(dtdr.Rows.Count>0)
                    {
                        dgv_prescription_search.Visible = true;
                        foreach (DataRow dr in dtdr.Rows)
                        {
                            DataTable dtstock = this.pcntrl.drug_instock(dr["inventory_id"].ToString());
                            if (dtstock.Rows.Count > 0)
                            {
                                if (dr["inventory_id"].ToString() == "0")
                                {
                                    //chk_exp = "0";
                                    strstock = "(Not sold)";
                                }
                                else
                                {
                                    strstock = "(Not sold)";
                                    //DataTable dt_exp = this.cntrl.check_expiry_date(dt4.Rows[j]["inventory_id"].ToString());
                                    //if(dt_exp.Rows.Count>0)
                                    //{
                                    DataTable dtsto = this.pcntrl.drug_instock(dr["inventory_id"].ToString());
                                    if (dtsto.Rows.Count > 0)
                                    {
                                        //chk_exp = "0";
                                        string dou_stock = dtsto.Rows[0]["Stock"].ToString();
                                        if (dou_stock == "" || dou_stock == "0.00")
                                        {
                                            strstock = "(Out-of-stock)";//(In stock)
                                        }
                                        else
                                        {
                                            strstock = "(In stock)";
                                        }
                                        //}
                                        //else
                                        //{
                                        //    chk_exp = "1";
                                        //}
                                    }

                                }
                                dgv_prescription_search.Rows.Add(dr["id"].ToString(), dr["name"].ToString(),strstock);
                            }
                                
                        }
                        
                    }

                    //else
                    //{
                    //    strstock = "(Not sold)";
                    //    //DataTable dt_exp = this.cntrl.check_expiry_date(dt4.Rows[j]["inventory_id"].ToString());
                    //    //if(dt_exp.Rows.Count>0)
                    //    //{
                    //   

                    //}


                    //lst_drug.DataSource = dtdr;
                    //lst_drug.DisplayMember = "name";
                    //lst_drug.ValueMember = "id";

                }
                else
                {
                    //string strstock = "";
                    //DataTable dtdr = this.cntrl.get_prescriptnwthname(userControl_textbox10.Text);
                    ////lst_drug.DataSource = dtdr;
                    ////lst_drug.DisplayMember = "name";
                    ////lst_drug.ValueMember = "id";
                    //if (dtdr.Rows.Count > 0)
                    //{
                    //    foreach (DataRow dr in dtdr.Rows)
                    //    {
                    //        DataTable dtstock = this.pcntrl.drug_instock(dr["inventory_id"].ToString());
                    //        if (dtstock.Rows.Count > 0)
                    //        {
                    //            if (dr["inventory_id"].ToString() == "0")
                    //            {
                    //                //chk_exp = "0";
                    //                strstock = "(Not sold)";
                    //            }
                    //            else
                    //            {
                    //                strstock = "(Not sold)";
                    //                //DataTable dt_exp = this.cntrl.check_expiry_date(dt4.Rows[j]["inventory_id"].ToString());
                    //                //if(dt_exp.Rows.Count>0)
                    //                //{
                    //                DataTable dtsto = this.pcntrl.drug_instock(dr["inventory_id"].ToString());
                    //                if (dtsto.Rows.Count > 0)
                    //                {
                    //                    //chk_exp = "0";
                    //                    string dou_stock = dtsto.Rows[0]["Stock"].ToString();
                    //                    if (dou_stock == "" || dou_stock == "0.00")
                    //                    {
                    //                        strstock = "(Out-of-stock)";//(In stock)
                    //                    }
                    //                    else
                    //                    {
                    //                        strstock = "(In stock)";
                    //                    }
                    //                    //}
                    //                    //else
                    //                    //{
                    //                    //    chk_exp = "1";
                    //                    //}
                    //                }

                    //            }
                    //            dgv_prescription_search.Rows.Add(dr["id"].ToString(), dr["name"].ToString(), strstock);
                    //        }

                    //    }

                    //}
                    //else
                       
                    dgv_prescription_search.Rows.Clear();
                    dgv_prescription_search.Hide();
                }
                if (dgv_prescription_search.Rows.Count > 0)
                {
                    dgv_prescription_search.Show();
                }
                else
                {
                    dgv_prescription_search.Hide();
                }
            }
        }
        //public void fill_grp_by_visit(int count)
        //{
        //    if (patient_id != "")
        //    {
        //        int x = 4;
        //        int y = last_grid_height;

        //        DataTable dt_appoint = this.cntrl.dt_appointments_show_coun(patient_id, count);
        //        if (dt_appoint.Rows.Count > 0)
        //        {
        //            int v_hignt = 0, c_hight = 0, tre_hight = 0, pre_height = 0;
        //            //x = 4;
        //            //y = 4;
        //            foreach (DataRow dr in dt_appoint.Rows)
        //            {
        //                Label lb = new Label();
        //                lb.Text = String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(dr["start_datetime"].ToString()));
        //                lb.BackColor = Color.White;// FromArgb(209, 226, 237);
        //                lb.ForeColor = Color.Navy;
        //                lb.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        //                lb.Location = new Point(x, y);
        //                lb.Width = 260;
        //                lb.Height = 30;
        //                panel6.Controls.Add(lb);
        //                y = y + (lb.Height + 10);
        //                string date = Convert.ToDateTime(dr["start_datetime"].ToString()).ToString("yyyy/MM/dd");
        //                //DataTable dt_vitals = this.cntrl.vitals_grp_visit_main(patient_id, date);
        //                //if (dt_vitals.Rows.Count > 0)
        //                //{
        //                //    Label lbl = new Label();
        //                //    lbl.Text = "Vital Signs";
        //                //    lbl.BackColor = Color.FromArgb(209, 226, 237);
        //                //    lbl.ForeColor = Color.Navy;
        //                //    lbl.Location = new Point(x, y);
        //                //    lbl.Font = new Font("Segoe UI", 10, FontStyle.Regular);
        //                //    panel6.Controls.Add(lbl);
        //                //    y = y + (lbl.Height + 10);
        //                //    DataGridView dgv_vitals = new DataGridView();
        //                //    dgv_vitals.BackgroundColor = Color.White;
        //                //    dgv_vitals.Height = 230;
        //                //    dgv_vitals.Width = 350;
        //                //    dgv_vitals.ColumnCount = 3;
        //                //    dgv_vitals.Columns[0].Width = 200;
        //                //    dgv_vitals.Columns[1].Width = 10;
        //                //    dgv_vitals.Columns[2].Width = 200;
        //                //    dgv_vitals.ColumnHeadersVisible = false;
        //                //    dgv_vitals.RowHeadersVisible = false;
        //                //    dgv_vitals.EnableHeadersVisualStyles = false;
        //                //    dgv_vitals.BorderStyle = BorderStyle.FixedSingle;
        //                //    dgv_vitals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        //                //    dgv_vitals.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        //                //    //dgv_vitals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //                //    dgv_vitals.CellBorderStyle = DataGridViewCellBorderStyle.None;
        //                //    dgv_vitals.Location = new Point(x, y);
        //                //    panel6.Controls.Add(dgv_vitals);
        //                //    dgv_vitals.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
        //                //    //dataGridView_invoice.Columns[4].Width = 28;
        //                //    dgv_vitals.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        //                //    dgv_vitals.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        //                //    int ri = 0;
        //                //    for (int j1 = 0; j1 < dt_vitals.Rows.Count; j1++)
        //                //    {

        //                //        if (dt_vitals.Rows[j1]["pulse"].ToString() != "")
        //                //        {
        //                //            dgv_vitals.Rows.Add("PULSE ", ":", dt_vitals.Rows[j1]["pulse"].ToString());
        //                //            //
        //                //            dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
        //                //            dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
        //                //            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
        //                //            dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
        //                //            ri = ri + 1;
        //                //        }
        //                //        if (dt_vitals.Rows[j1]["temp"].ToString() != "")
        //                //        {
        //                //            dgv_vitals.Rows.Add("TEMPERATURE ", ":", dt_vitals.Rows[j1]["temp"].ToString() + " ( " + dt_vitals.Rows[j1]["temp_type"].ToString() + " ) ");

        //                //            dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
        //                //            dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
        //                //            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
        //                //            dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
        //                //            ri = ri + 1;
        //                //        }
        //                //        if (dt_vitals.Rows[j1]["bp_syst"].ToString() != "")
        //                //        {
        //                //            dgv_vitals.Rows.Add("BLOOD PRESSURE ( SYSTOLIC ) ", ":", dt_vitals.Rows[j1]["bp_syst"].ToString() + " ( " + dt_vitals.Rows[j1]["bp_type"].ToString() + " ) ");

        //                //            dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
        //                //            dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
        //                //            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
        //                //            dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
        //                //            ri = ri + 1;
        //                //        }
        //                //        if (dt_vitals.Rows[j1]["bp_dia"].ToString() != "")
        //                //        {
        //                //            dgv_vitals.Rows.Add("BLOOD PRESSURE ( DIASTOLIC )  ", ":", dt_vitals.Rows[j1]["bp_dia"].ToString() + " ( " + dt_vitals.Rows[j1]["bp_type"].ToString() + " ) ");
        //                //            //ri = ri + 1;
        //                //            dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
        //                //            dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
        //                //            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
        //                //            dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
        //                //            ri = ri + 1;
        //                //        }
        //                //        if (dt_vitals.Rows[j1]["Height"].ToString() != "")
        //                //        {
        //                //            dgv_vitals.Rows.Add("HEIGHT  ", ":", dt_vitals.Rows[j1]["Height"].ToString() + "(Cm)", "", PappyjoeMVC.Properties.Resources.blank);
        //                //            //ri = ri + 1;
        //                //            dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
        //                //            dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
        //                //            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
        //                //            dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
        //                //            ri = ri + 1;
        //                //        }
        //                //        if (dt_vitals.Rows[j1]["weight"].ToString() != "")
        //                //        {
        //                //            dgv_vitals.Rows.Add("WEIGHT  ", ":", dt_vitals.Rows[j1]["weight"].ToString() + "(Kg)", "", PappyjoeMVC.Properties.Resources.blank);
        //                //            //ri = ri + 1;
        //                //            dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
        //                //            dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
        //                //            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
        //                //            dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
        //                //            ri = ri + 1;
        //                //        }
        //                //        if (dt_vitals.Rows[j1]["resp"].ToString() != "")
        //                //        {
        //                //            dgv_vitals.Rows.Add("RESPIRATORY RATE ", ":", dt_vitals.Rows[j1]["resp"].ToString(), "", PappyjoeMVC.Properties.Resources.blank);
        //                //            //ri = ri + 1;
        //                //            dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
        //                //            dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
        //                //            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
        //                //            dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
        //                //            ri = ri + 1;
        //                //        }
        //                //        if (dt_vitals.Rows[j1]["weight"].ToString() != null && dt_vitals.Rows[j1]["weight"].ToString() != "" && dt_vitals.Rows[j1]["Height"].ToString() != null && dt_vitals.Rows[j1]["Height"].ToString() != "")
        //                //        {
        //                //            weight = Convert.ToDouble(dt_vitals.Rows[j1]["weight"].ToString());
        //                //            height = Convert.ToDouble(dt_vitals.Rows[j1]["Height"].ToString());
        //                //        }
        //                //        else
        //                //        {
        //                //            weight = Convert.ToDouble("0.00");
        //                //            height = Convert.ToDouble("0.00");
        //                //        }
        //                //        gender = lb_gender.Text;
        //                //        string msg = "";
        //                //        if (weight > 0 && height > 0)
        //                //        {
        //                //            BMI = Math.Round((weight / (height * height)) * 10000, 1);
        //                //            if (BMI != null)
        //                //            {
        //                //                if (BMI < 19 && gender == "Female")
        //                //                {
        //                //                    msg = "BMI is low";
        //                //                }
        //                //                if (BMI >= 19 & BMI <= 24 & gender == "Female")
        //                //                {
        //                //                    msg = "Normal";
        //                //                }
        //                //                if (BMI > 24 & gender == "Female")
        //                //                {
        //                //                    msg = "BMI is High";
        //                //                }
        //                //                if (BMI < 20 & gender == "Male")
        //                //                {
        //                //                    msg = "BMI is low";
        //                //                }
        //                //                if (BMI >= 20 & BMI <= 25 & gender == "Male")
        //                //                {
        //                //                    msg = "Normal";
        //                //                }
        //                //                if (BMI > 25 & gender == "Male")
        //                //                {
        //                //                    msg = "BMI is High";
        //                //                }
        //                //            }
        //                //            if (BMI > 0)
        //                //            {
        //                //                dgv_vitals.Rows.Add("BMI(BOADY MASS INDEX) ", ":", BMI + "  ,   " + msg);
        //                //                ri = ri + 1;
        //                //                if (msg == "BMI is low")
        //                //                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.Red;
        //                //                else if (msg == "Normal")
        //                //                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
        //                //                else if (msg == "BMI is High")
        //                //                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.Red;
        //                //                dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
        //                //                dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
        //                //                dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
        //                //                dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
        //                //            }
        //                //        }
        //                //        if (dt_vitals.Rows[j1]["spo"].ToString() != "")
        //                //        {
        //                //            dgv_vitals.Rows.Add("SPO2 ", ":", dt_vitals.Rows[j1]["spo"].ToString(), "");
        //                //            //ri = ri + 1;
        //                //            dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
        //                //            dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
        //                //            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
        //                //            dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
        //                //            ri = ri + 1;
        //                //        }
        //                //        dgv_vitals.Rows.Add("Recorded By : Dr." + dt_vitals.Rows[j1]["dr_name"].ToString(), "", "");
        //                //        dgv_vitals.Rows[ri + 1].Cells[0].Style.ForeColor = Color.Red;
        //                //        dgv_vitals.Rows[ri + 1].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Italic);
        //                //        //ri = ri + 2;
        //                //    }
        //                //    v_hignt = dgv_vitals.Height;
        //                //    y = y + (v_hignt + 2);
        //                //}
        //                //else
        //                //{
        //                //    //y = y + (v_hignt + 2);
        //                //}
        //                //// clinic
        //                //DataTable dt_cf_main = this.cntrl.dt_clinic_main_grp_visi(patient_id, date);
        //                //int i = 0;
        //                //if (dt_cf_main.Rows.Count > 0)
        //                //{
        //                //    //x = 4;
        //                //    //y = y + (v_hignt +2);
        //                //    Label lbl = new Label();
        //                //    lbl.Text = "Clinical Findings";
        //                //    lbl.Width = 250;
        //                //    lbl.BackColor = Color.FromArgb(209, 226, 237);
        //                //    lbl.ForeColor = Color.Navy;
        //                //    lbl.Location = new Point(x, y);
        //                //    lbl.Font = new Font("Segoe UI", 10, FontStyle.Regular);

        //                //    //Label lbl = new Label();
        //                //    //lbl.Text = "Clinical Findings";
        //                //    //lbl.Location = new Point(x,y);
        //                //    panel6.Controls.Add(lbl);


        //                //    y = y + (lbl.Height + 5);
        //                //    DataGridView dgv_clinic = new DataGridView();
        //                //    dgv_clinic.Height = 260;
        //                //    dgv_clinic.Width = 460;
        //                //    dgv_clinic.ColumnCount = 4;
        //                //    dgv_clinic.Columns[0].Width = 20;
        //                //    dgv_clinic.Columns[1].Width = 150;
        //                //    dgv_clinic.Columns[2].Width = 1; dgv_clinic.Columns[3].Width = 250;
        //                //    dgv_clinic.BackgroundColor = Color.White;
        //                //    //dgv_clinic.ColumnHeadersVisible = false;
        //                //    //dgv_clinic.RowHeadersVisible = false;
        //                //    dgv_clinic.EnableHeadersVisualStyles = false;
        //                //    dgv_clinic.BorderStyle = BorderStyle.None;
        //                //    dgv_clinic.Enabled = false;
        //                //    //dgv_clinic.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //                //    dgv_clinic.CellBorderStyle = DataGridViewCellBorderStyle.None;
        //                //    dgv_clinic.Location = new Point(x, y);
        //                //    panel6.Controls.Add(dgv_clinic);
        //                //    DataGridViewColumn column = dgv_clinic.Columns[3];
        //                //    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        //                //    //dgv_clinic.Columns[3].AutoSizeMode =                        //dgv_clinic.RowCount = 0;
        //                //    dgv_clinic.ColumnHeadersVisible = false;
        //                //    dgv_clinic.RowHeadersVisible = false;
        //                //    //dgv_clinic.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //                //    for (int jt = 0; jt < dt_cf_main.Rows.Count; jt++)
        //                //    {

        //                //        string heading = "";
        //                //        DataTable dt_clinic = this.cntrl.dt_cf_Complaints(dt_cf_main.Rows[0]["id"].ToString());
        //                //        if (dt_clinic.Rows.Count > 0)
        //                //        {
        //                //            heading = "Complaints";
        //                //            foreach (DataRow drr in dt_clinic.Rows)
        //                //            {

        //                //                dgv_clinic.Rows.Add("0", heading, "", drr["complaint_id"].ToString());
        //                //                heading = "";
        //                //                dgv_clinic.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
        //                //                dgv_clinic.Rows[i].Cells[1].Style.ForeColor = Color.DarkBlue;
        //                //                dgv_clinic.Rows[i].Cells[3].Style.ForeColor = Color.DarkBlue;
        //                //                dgv_clinic.Columns[0].Visible = false;
        //                //                i = i + 1;
        //                //            }
        //                //            //i = i + 1;
        //                //            dgv_clinic.Rows.Add("", "", "", "");
        //                //            dgv_clinic.Rows[i].Height = 1;
        //                //            dgv_clinic.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;

        //                //            i = i + 1;
        //                //        }
        //                //        //System.Data.DataTable dt_cf_observe = this.cntrl.dt_cf_observe(dt_cf_main.Rows[0]["id"].ToString());
        //                //        //if (dt_cf_observe.Rows.Count > 0)
        //                //        //{
        //                //        //    heading = "Observations";
        //                //        //    foreach (DataRow drr in dt_cf_observe.Rows)
        //                //        //    {
        //                //        //        //i = i + 1;
        //                //        //        dgv_clinic.Rows.Add("0", heading,"", drr["observation_id"].ToString());
        //                //        //        heading = "";
        //                //        //        dgv_clinic.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
        //                //        //        dgv_clinic.Rows[i].Cells[1].Style.ForeColor = Color.DarkBlue;
        //                //        //        dgv_clinic.Rows[i].Cells[3].Style.ForeColor = Color.DarkBlue;
        //                //        //        dgv_clinic.Columns[0].Visible = false;
        //                //        //        i = i + 1;

        //                //        //    }

        //                //        //    dgv_clinic.Rows.Add("", "", "","");
        //                //        //    dgv_clinic.Rows[i].Height = 1;
        //                //        //    dgv_clinic.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
        //                //        //    i = i + 1;
        //                //        //}
        //                //        //System.Data.DataTable dt_cf_investigation = this.cntrl.dt_cf_investigation(dt_cf_main.Rows[jt]["id"].ToString());
        //                //        //if (dt_cf_investigation.Rows.Count > 0)
        //                //        //{
        //                //        //    heading = "Investigation";
        //                //        //    foreach (DataRow drr in dt_cf_investigation.Rows)
        //                //        //    {
        //                //        //        //i = i + 1;
        //                //        //        dgv_clinic.Rows.Add("0", heading,"", drr["investigation_id"].ToString());
        //                //        //        heading = ""; 
        //                //        //        dgv_clinic.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
        //                //        //        dgv_clinic.Rows[i].Cells[1].Style.ForeColor = Color.DarkBlue;
        //                //        //        dgv_clinic.Rows[i].Cells[3].Style.ForeColor = Color.DarkBlue;
        //                //        //        dgv_clinic.Columns[0].Visible = false;
        //                //        //        i = i + 1;
        //                //        //    }
        //                //        //    //i = i + 1;
        //                //        //    dgv_clinic.Rows.Add("", "", "", "");
        //                //        //    dgv_clinic.Rows[i].Height = 1;
        //                //        //    dgv_clinic.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
        //                //        //    i = i + 1;
        //                //        //}
        //                //        System.Data.DataTable dt_cf_diagnosis = this.cntrl.dt_cf_diagnosis(dt_cf_main.Rows[jt]["id"].ToString());
        //                //        if (dt_cf_diagnosis.Rows.Count > 0)
        //                //        {
        //                //            heading = "Diagnosis";
        //                //            foreach (DataRow drr in dt_cf_diagnosis.Rows)
        //                //            {
        //                //                //i = i + 1;
        //                //                dgv_clinic.Rows.Add("0", heading, "", drr["diagnosis_id"].ToString());
        //                //                heading = "";
        //                //                dgv_clinic.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
        //                //                dgv_clinic.Rows[i].Cells[1].Style.ForeColor = Color.DarkBlue;
        //                //                dgv_clinic.Rows[i].Cells[3].Style.ForeColor = Color.DarkBlue;
        //                //                dgv_clinic.Columns[0].Visible = false;
        //                //                i = i + 1;
        //                //            }
        //                //            dgv_clinic.Rows.Add("", "", "", "");
        //                //            dgv_clinic.Rows[i].Height = 1;
        //                //            dgv_clinic.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
        //                //            i = i + 1;
        //                //        }
        //                //        System.Data.DataTable dt_cf_note = this.cntrl.dt_cf_note(dt_cf_main.Rows[jt]["id"].ToString());
        //                //        if (dt_cf_note.Rows.Count > 0)
        //                //        {
        //                //            heading = "Notes";
        //                //            foreach (DataRow drr in dt_cf_note.Rows)
        //                //            {
        //                //                //i = i + 1;
        //                //                dgv_clinic.Rows.Add("0", heading, "", drr["note_name"].ToString());
        //                //                heading = "";
        //                //                dgv_clinic.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
        //                //                dgv_clinic.Rows[i].Cells[1].Style.ForeColor = Color.DarkBlue;
        //                //                dgv_clinic.Rows[i].Cells[3].Style.ForeColor = Color.DarkBlue;
        //                //                dgv_clinic.Columns[0].Visible = false;
        //                //                i = i + 1;
        //                //            }

        //                //        }
        //                //    }
        //                //    c_hight = dgv_clinic.Height;
        //                //    y = y + (c_hight + 2);
        //                //}
        //                // Treatment
        //                DataTable dt_treatmnt = this.cntrl.Load_treatments(patient_id, date);
        //                int j = 0;
        //                if (dt_treatmnt.Rows.Count > 0)
        //                {
        //                    //y = y + (c_hight + 2);
        //                    Label lbl = new Label();
        //                    lbl.Text = "Treatments";
        //                    lbl.BackColor = Color.FromArgb(209, 226, 237);
        //                    lbl.ForeColor = Color.Navy;
        //                    lbl.Location = new Point(x, y);
        //                    lbl.Font = new Font("Segoe UI", 10, FontStyle.Regular);
        //                    panel6.Controls.Add(lbl);
        //                    y = y + (lbl.Height + 10);
        //                    DataGridView dataGridView1_treatment_paln = new DataGridView();
        //                    dataGridView1_treatment_paln.BackgroundColor = Color.White;
        //                    dataGridView1_treatment_paln.Height = 180;
        //                    dataGridView1_treatment_paln.Width = 520;
        //                    dataGridView1_treatment_paln.ColumnCount = 6;
        //                    dataGridView1_treatment_paln.Columns[0].Width = 20;
        //                    dataGridView1_treatment_paln.Columns[1].Width = 150;
        //                    dataGridView1_treatment_paln.Columns[2].Width = 100;
        //                    dataGridView1_treatment_paln.Columns[3].Width = 100;
        //                    dataGridView1_treatment_paln.Columns[4].Width = 100;
        //                    dataGridView1_treatment_paln.Columns[5].Width = 100;
        //                    DataGridViewColumn column = dataGridView1_treatment_paln.Columns[5];
        //                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        //                    dataGridView1_treatment_paln.Columns[0].Visible = false;
        //                    dataGridView1_treatment_paln.ColumnHeadersVisible = false;
        //                    dataGridView1_treatment_paln.RowHeadersVisible = false;
        //                    dataGridView1_treatment_paln.EnableHeadersVisualStyles = false;
        //                    dataGridView1_treatment_paln.BorderStyle = BorderStyle.Fixed3D;
        //                    //dataGridView1_treatment_paln.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        //                    //dataGridView1_treatment_paln.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        //                    //dgv_vitals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //                    dataGridView1_treatment_paln.CellBorderStyle = DataGridViewCellBorderStyle.None;
        //                    dataGridView1_treatment_paln.Location = new Point(x, y);
        //                    panel6.Controls.Add(dataGridView1_treatment_paln);
        //                    dataGridView1_treatment_paln.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
        //                    DataTable dt_pt_sub = this.cntrl.treatment_sub_details(dt_treatmnt.Rows[0]["id"].ToString());
        //                    if (dt_pt_sub.Rows.Count > 0)
        //                    {
        //                        Double totalEst = 0;
        //                        dataGridView1_treatment_paln.Rows.Add(dt_treatmnt.Rows[0]["id"].ToString(), "TREATMENTS", "COST", "DISCOUNT", "TOTAL", "NOTE");
        //                        dataGridView1_treatment_paln.Rows[j].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
        //                        dataGridView1_treatment_paln.Rows[j].Cells[1].Style.ForeColor = Color.White;
        //                        dataGridView1_treatment_paln.Rows[j].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
        //                        dataGridView1_treatment_paln.Rows[j].Cells[2].Style.ForeColor = Color.White;
        //                        dataGridView1_treatment_paln.Rows[j].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
        //                        dataGridView1_treatment_paln.Rows[j].Cells[3].Style.ForeColor = Color.White;
        //                        dataGridView1_treatment_paln.Rows[j].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
        //                        dataGridView1_treatment_paln.Rows[j].Cells[4].Style.ForeColor = Color.White;
        //                        dataGridView1_treatment_paln.Rows[j].Cells[5].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
        //                        dataGridView1_treatment_paln.Rows[j].Cells[5].Style.ForeColor = Color.White;
        //                        dataGridView1_treatment_paln.Rows[j].Cells[0].Style.BackColor = Color.DarkGray;
        //                        dataGridView1_treatment_paln.Rows[j].Cells[1].Style.BackColor = Color.DarkGray;
        //                        dataGridView1_treatment_paln.Rows[j].Cells[2].Style.BackColor = Color.DarkGray;
        //                        dataGridView1_treatment_paln.Rows[j].Cells[3].Style.BackColor = Color.DarkGray;
        //                        dataGridView1_treatment_paln.Rows[j].Cells[4].Style.BackColor = Color.DarkGray;
        //                        dataGridView1_treatment_paln.Rows[j].Cells[5].Style.BackColor = Color.DarkGray;
        //                        dataGridView1_treatment_paln.Rows[j].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        //                        dataGridView1_treatment_paln.Rows[j].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        //                        dataGridView1_treatment_paln.Rows[j].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        //                        for (int k = 0; k < dt_pt_sub.Rows.Count; k++)
        //                        {
        //                            string discount_string = "";
        //                            if (dt_pt_sub.Rows[k]["discount_type"].ToString() == "INR")
        //                            {
        //                                discount_string = "";
        //                            }
        //                            else
        //                            {
        //                                discount_string = "(" + dt_pt_sub.Rows[k]["discount"].ToString() + "%)";
        //                            }
        //                            Decimal totalcost = Convert.ToDecimal(dt_pt_sub.Rows[k]["cost"].ToString()) * Convert.ToDecimal(dt_pt_sub.Rows[k]["quantity"].ToString());
        //                            j = j + 1;
        //                            dataGridView1_treatment_paln.Rows.Add(dt_pt_sub.Rows[k]["id"].ToString(), dt_pt_sub.Rows[k]["procedure_name"].ToString(), String.Format("{0:C2}", Convert.ToDecimal(totalcost)), String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discount_inrs"].ToString())) + discount_string, String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString())), dt_pt_sub.Rows[k]["note"].ToString() + " " + dt_pt_sub.Rows[k]["tooth"].ToString());
        //                            dataGridView1_treatment_paln.Rows[j].Height = 30;
        //                            //dataGridView1_treatment_paln.Columns[6].Width = 200;
        //                            dataGridView1_treatment_paln.Rows[j].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        //                            dataGridView1_treatment_paln.Rows[j].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        //                            dataGridView1_treatment_paln.Rows[j].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        //                            totalEst = totalEst + Convert.ToDouble(dt_pt_sub.Rows[k]["total"].ToString());
        //                            //dataGridView1_treatment_paln.Rows[j].Cells[8].Value = PappyjoeMVC.Properties.Resources.blank;

        //                            j = j + 1;
        //                            dataGridView1_treatment_paln.Rows.Add("", "Planned by " + dt_treatmnt.Rows[0]["dr_name"].ToString(), "", "Estimated amount:", String.Format("{0:C2}", Convert.ToDecimal(totalEst)));
        //                            dataGridView1_treatment_paln.Rows[j].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
        //                            dataGridView1_treatment_paln.Rows[j].Cells[1].Style.ForeColor = Color.Red;
        //                            dataGridView1_treatment_paln.Rows[j].Cells[5].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
        //                            //dataGridView1_treatment_paln.Rows[j].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
        //                            //dataGridView1_treatment_paln.Rows[j].Cells[8].Value = PappyjoeMVC.Properties.Resources.blank;
        //                            dataGridView1_treatment_paln.Rows[j].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        //                            j = j + 1;
        //                            dataGridView1_treatment_paln.Rows.Add("", "", "", "", "", "");
        //                            //dataGridView1_treatment_paln.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
        //                            //dataGridView1_treatment_paln.Rows[i].Cells[8].Value = PappyjoeMVC.Properties.Resources.blank;
        //                            j = j + 1;
        //                        }

        //                        tre_hight = dataGridView1_treatment_paln.Height;
        //                        y = y + (tre_hight + 5);
        //                    }



        //                }
        //                //prescription
        //                DataTable dt_pre_main = this.cntrl.Get_maindtails(patient_id, date);
        //                int p = 0;
        //                if (dt_pre_main.Rows.Count > 0)
        //                {
        //                    //y = y + (tre_hight + 2);
        //                    Label lbl = new Label();
        //                    lbl.Text = "Prescription";
        //                    lbl.BackColor = Color.FromArgb(209, 226, 237);
        //                    lbl.ForeColor = Color.Navy;
        //                    lbl.Location = new Point(x, y);
        //                    lbl.Font = new Font("Segoe UI", 10, FontStyle.Regular);
        //                    panel6.Controls.Add(lbl);
        //                    y = y + (lbl.Height + 10);
        //                    DataGridView dataGridView1 = new DataGridView();
        //                    dataGridView1.BackgroundColor = Color.White;
        //                    dataGridView1.Height = 180;
        //                    dataGridView1.Width = 520;
        //                    dataGridView1.ColumnCount = 5;
        //                    dataGridView1.Columns[0].Width = 10;

        //                    dataGridView1.Columns[1].Width = 190;
        //                    dataGridView1.Columns[2].Width = 100;
        //                    dataGridView1.Columns[3].Width = 100;
        //                    dataGridView1.Columns[4].Width = 120;
        //                    dataGridView1.ColumnHeadersVisible = false;
        //                    dataGridView1.RowHeadersVisible = false;
        //                    dataGridView1.EnableHeadersVisualStyles = false;
        //                    dataGridView1.BorderStyle = BorderStyle.Fixed3D;
        //                    //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        //                    //dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        //                    //dgv_vitals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //                    dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
        //                    dataGridView1.Location = new Point(x, y);
        //                    panel6.Controls.Add(dataGridView1);
        //                    dataGridView1.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
        //                    dataGridView1.Rows.Add(dt_pre_main.Rows[0]["id"].ToString(), "Drug", "Frequency", "Duration", "Instruction");
        //                    dataGridView1.Rows[p].Cells[1].Style.BackColor = Color.LightGray;
        //                    dataGridView1.Rows[p].Cells[2].Style.BackColor = Color.LightGray;
        //                    dataGridView1.Rows[p].Cells[3].Style.BackColor = Color.LightGray;
        //                    dataGridView1.Rows[p].Cells[4].Style.BackColor = Color.LightGray;
        //                    //dataGridView1.Rows[i].Cells[5].Style.BackColor = Color.LightGray;
        //                    dataGridView1.Rows[p].Cells[1].Style.ForeColor = Color.Black;
        //                    dataGridView1.Rows[p].Cells[2].Style.ForeColor = Color.Black;
        //                    dataGridView1.Rows[p].Cells[3].Style.ForeColor = Color.Black;
        //                    dataGridView1.Rows[p].Cells[4].Style.ForeColor = Color.Black;

        //                    dataGridView1.Columns[0].Visible = false;
        //                    //dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.Bill;
        //                    System.Data.DataTable dt_prescription = this.cntrl.prescription_detoails(dt_pre_main.Rows[0]["id"].ToString());
        //                    if (dt_prescription.Rows.Count > 0)
        //                    {
        //                        for (int k = 0; k < dt_prescription.Rows.Count; k++)
        //                        {
        //                            i = i + 1;
        //                            string morning = "", duration = "";
        //                            string noon = "";
        //                            string night = "";
        //                            string a1 = dt_prescription.Rows[k]["morning"].ToString();
        //                            string[] b1 = a1.Split('.');
        //                            int right1 = int.Parse(b1[1]);
        //                            morning = Convert.ToString(int.Parse(b1[0]));
        //                            if (right1 != 0) { morning = morning + "." + Convert.ToString(int.Parse(b1[1])); }
        //                            string a2 = dt_prescription.Rows[k]["noon"].ToString();
        //                            string[] b2 = a2.Split('.');
        //                            int right2 = int.Parse(b2[1]);
        //                            noon = Convert.ToString(int.Parse(b2[0]));
        //                            if (right2 != 0) { noon = noon + "." + Convert.ToString(int.Parse(b2[1])); }
        //                            string a3 = dt_prescription.Rows[k]["night"].ToString();
        //                            string[] b3 = a3.Split('.');
        //                            int right3 = int.Parse(b3[1]);
        //                            night = Convert.ToString(int.Parse(b3[0]));
        //                            if (right3 != 0) { night = night + "." + Convert.ToString(int.Parse(b3[1])); }
        //                            duration = dt_prescription.Rows[k]["duration_unit"].ToString();
        //                            //updation
        //                            //if (morning == "0" && noon =="0" && night == "0"  )
        //                            //{
        //                            //    morning = "";
        //                            //    noon = "";
        //                            //    night = "";
        //                            //}
        //                            //duration = dt_prescription.Rows[k]["duration_unit"].ToString();
        //                            if (duration.Trim() == "0" || duration.Trim() == "")
        //                            {
        //                                duration = "";

        //                            }
        //                            else
        //                            {
        //                                duration = dt_prescription.Rows[k]["duration_unit"].ToString() + "" + dt_prescription.Rows[k]["duration_period"].ToString();
        //                            }
        //                            if (morning == "0" && noon == "0" && night == "0")
        //                            {
        //                                dataGridView1.Rows.Add("0", dt_prescription.Rows[k]["drug_type"].ToString() + " " + dt_prescription.Rows[k]["drug_name"].ToString() + " " + dt_prescription.Rows[k]["strength"].ToString() + " " + "", "", duration, dt_prescription.Rows[k]["add_instruction"].ToString());

        //                            }
        //                            else
        //                            {
        //                                dataGridView1.Rows.Add("0", dt_prescription.Rows[k]["drug_type"].ToString() + " " + dt_prescription.Rows[k]["drug_name"].ToString() + " " + dt_prescription.Rows[k]["strength"].ToString() + " " + dt_prescription.Rows[k]["strength_gr"].ToString(), morning + " - " + noon + " - " + night, duration, dt_prescription.Rows[k]["add_instruction"].ToString());

        //                            }
        //                            //dataGridView1.Rows[k].Cells[].Value = "0";





        //                            //dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.blank;
        //                            dataGridView1.Rows[p].Height = 30;
        //                        }
        //                        p = p + 1;
        //                        dataGridView1.Rows.Add("0", "Prescribed by Dr." + dt_pre_main.Rows[0]["doctor_name"].ToString(), "", "");
        //                        dataGridView1.Rows[p].Cells[1].Style.ForeColor = Color.Red;
        //                        dataGridView1.Rows[p].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
        //                        //dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.blank;
        //                        p = p + 1;
        //                        dataGridView1.Rows.Add("0", "", "", "");
        //                        //dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.blank;
        //                        p = p + 1;
        //                    }

        //                    pre_height = dataGridView1.Height;
        //                    y = y + (pre_height + 2);
        //                }
        //            }
        //        }


        //    }
        //}
     
        public void grpbyvisit()
        {
            show_flag = true; last_grid_height = 0;
            panel6.Controls.Clear();
            if (patient_id != "")
            {
                int x = 4;
                int y = 4;
                DataTable dt_show = this.cntrl.dt_appointments_showmore(patient_id);
                if (dt_show.Rows.Count > 2)
                {
                    lb_showmore.Visible = true;
                }
                else
                {
                    lb_showmore.Visible = false;
                }
                DataTable dt_appoint = this.cntrl.dt_appointments(patient_id);
                if (dt_appoint.Rows.Count > 0)
                {
                    int v_hignt = 0, c_hight = 0, tre_hight = 0, pre_height = 0;
                    //x = 4;
                    //y = 4;
                    foreach (DataRow dr in dt_appoint.Rows)
                    {
                        Label lb = new Label();
                        lb.Text = String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(dr["start_datetime"].ToString()));
                        lb.BackColor = Color.White;// FromArgb(209, 226, 237);
                        lb.ForeColor = Color.Navy;
                        lb.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                        lb.Location = new Point(x, y);
                        lb.Width = 260;
                        lb.Height = 30;
                        panel6.Controls.Add(lb);
                        y = y + (lb.Height + 10);
                        string date = Convert.ToDateTime(dr["start_datetime"].ToString()).ToString("yyyy/MM/dd");
                        DataTable dt_vitals = this.cntrl.vitals_grp_visit_main(patient_id, date);
                        if (dt_vitals.Rows.Count > 0)
                        {
                            Label lbl = new Label();
                            lbl.Text = "Vital Signs";
                            lbl.BackColor = Color.FromArgb(209, 226, 237);
                            lbl.ForeColor = Color.Navy;
                            lbl.Location = new Point(x, y);
                            lbl.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                            panel6.Controls.Add(lbl);
                            y = y + (lbl.Height + 10);
                            DataGridView dgv_vitals = new DataGridView();
                            dgv_vitals.BackgroundColor = Color.White;
                            dgv_vitals.Height = 230;
                            dgv_vitals.Width = 520;
                            dgv_vitals.ColumnCount = 3;
                            dgv_vitals.Columns[0].Width = 200;
                            dgv_vitals.Columns[1].Width = 10;
                            dgv_vitals.Columns[2].Width = 200;
                            dgv_vitals.ColumnHeadersVisible = false;
                            dgv_vitals.RowHeadersVisible = false;
                            dgv_vitals.EnableHeadersVisualStyles = false;
                            dgv_vitals.BorderStyle = BorderStyle.FixedSingle;
                            dgv_vitals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                            dgv_vitals.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                            //dgv_vitals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            dgv_vitals.CellBorderStyle = DataGridViewCellBorderStyle.None;
                            dgv_vitals.Location = new Point(x, y);
                            panel6.Controls.Add(dgv_vitals);
                            dgv_vitals.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                            //dataGridView_invoice.Columns[4].Width = 28;
                            dgv_vitals.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            dgv_vitals.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            int ri = 0;
                            for (int j1 = 0; j1 < dt_vitals.Rows.Count; j1++)
                            {

                                if (dt_vitals.Rows[j1]["pulse"].ToString() != "")
                                {
                                    dgv_vitals.Rows.Add("PULSE ", ":", dt_vitals.Rows[j1]["pulse"].ToString());
                                    //
                                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                    dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                    dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    ri = ri + 1;
                                }
                                if (dt_vitals.Rows[j1]["temp"].ToString() != "")
                                {
                                    dgv_vitals.Rows.Add("TEMPERATURE ", ":", dt_vitals.Rows[j1]["temp"].ToString() + " ( " + dt_vitals.Rows[j1]["temp_type"].ToString() + " ) ");

                                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                    dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                    dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    ri = ri + 1;
                                }
                                if (dt_vitals.Rows[j1]["bp_syst"].ToString() != "")
                                {
                                    dgv_vitals.Rows.Add("BLOOD PRESSURE ( SYSTOLIC ) ", ":", dt_vitals.Rows[j1]["bp_syst"].ToString() + " ( " + dt_vitals.Rows[j1]["bp_type"].ToString() + " ) ");

                                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                    dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                    dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    ri = ri + 1;
                                }
                                if (dt_vitals.Rows[j1]["bp_dia"].ToString() != "")
                                {
                                    dgv_vitals.Rows.Add("BLOOD PRESSURE ( DIASTOLIC )  ", ":", dt_vitals.Rows[j1]["bp_dia"].ToString() + " ( " + dt_vitals.Rows[j1]["bp_type"].ToString() + " ) ");
                                    //ri = ri + 1;
                                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                    dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                    dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    ri = ri + 1;
                                }
                                if (dt_vitals.Rows[j1]["Height"].ToString() != "")
                                {
                                    dgv_vitals.Rows.Add("HEIGHT  ", ":", dt_vitals.Rows[j1]["Height"].ToString() + "(Cm)", "", PappyjoeMVC.Properties.Resources.blank);
                                    //ri = ri + 1;
                                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                    dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                    dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    ri = ri + 1;
                                }
                                if (dt_vitals.Rows[j1]["weight"].ToString() != "")
                                {
                                    dgv_vitals.Rows.Add("WEIGHT  ", ":", dt_vitals.Rows[j1]["weight"].ToString() + "(Kg)", "", PappyjoeMVC.Properties.Resources.blank);
                                    //ri = ri + 1;
                                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                    dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                    dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    ri = ri + 1;
                                }
                                if (dt_vitals.Rows[j1]["resp"].ToString() != "")
                                {
                                    dgv_vitals.Rows.Add("RESPIRATORY RATE ", ":", dt_vitals.Rows[j1]["resp"].ToString(), "", PappyjoeMVC.Properties.Resources.blank);
                                    //ri = ri + 1;
                                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                    dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                    dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    ri = ri + 1;
                                }
                                if (dt_vitals.Rows[j1]["weight"].ToString() != null && dt_vitals.Rows[j1]["weight"].ToString() != "" && dt_vitals.Rows[j1]["Height"].ToString() != null && dt_vitals.Rows[j1]["Height"].ToString() != "")
                                {
                                    weight = Convert.ToDouble(dt_vitals.Rows[j1]["weight"].ToString());
                                    height = Convert.ToDouble(dt_vitals.Rows[j1]["Height"].ToString());
                                }
                                else
                                {
                                    weight = Convert.ToDouble("0.00");
                                    height = Convert.ToDouble("0.00");
                                }
                                gender = lb_gender.Text;
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
                                        dgv_vitals.Rows.Add("BMI(BOADY MASS INDEX) ", ":", BMI + "  ,   " + msg);
                                        ri = ri + 1;
                                        if (msg == "BMI is low")
                                            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.Red;
                                        else if (msg == "Normal")
                                            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                        else if (msg == "BMI is High")
                                            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.Red;
                                        dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                        dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                        dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                        dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    }
                                }
                                if (dt_vitals.Rows[j1]["spo"].ToString() != "")
                                {
                                    dgv_vitals.Rows.Add("SPO2 ", ":", dt_vitals.Rows[j1]["spo"].ToString(), "");
                                    //ri = ri + 1;
                                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                    dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                    dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    ri = ri + 1;
                                }
                                dgv_vitals.Rows.Add("Recorded By : Dr." + dt_vitals.Rows[j1]["dr_name"].ToString(), "", "");
                                dgv_vitals.Rows[ri + 1].Cells[0].Style.ForeColor = Color.Red;
                                dgv_vitals.Rows[ri + 1].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Italic);
                                //ri = ri + 2;
                            }
                            v_hignt = dgv_vitals.Height;
                            last_grid_height = y;
                            y = y + (v_hignt + 2);
                        }
                        else
                        {
                            //y = y + (v_hignt + 2);
                        }
                        // clinic
                        DataTable dt_cf_main = this.cntrl.dt_clinic_main_grp_visi(patient_id, date);
                        int i = 0;
                        if (dt_cf_main.Rows.Count > 0)
                        {
                            //x = 4;
                            //y = y + (v_hignt +2);
                            Label lbl = new Label();
                            lbl.Text = "Clinical Findings";
                            lbl.Width = 250;
                            lbl.BackColor = Color.FromArgb(209, 226, 237);
                            lbl.ForeColor = Color.Navy;
                            lbl.Location = new Point(x, y);
                            lbl.Font = new Font("Segoe UI", 10, FontStyle.Regular);

                            //Label lbl = new Label();
                            //lbl.Text = "Clinical Findings";
                            //lbl.Location = new Point(x,y);
                            panel6.Controls.Add(lbl);


                            y = y + (lbl.Height + 5);
                            DataGridView dgv_clinic = new DataGridView();
                            dgv_clinic.Height = 260;
                            dgv_clinic.Width = 520;
                            dgv_clinic.ColumnCount = 4;
                            dgv_clinic.Columns[0].Width = 20;
                            dgv_clinic.Columns[1].Width = 150;
                            dgv_clinic.Columns[2].Width = 1; dgv_clinic.Columns[3].Width = 250;
                            dgv_clinic.BackgroundColor = Color.White;
                            //dgv_clinic.ColumnHeadersVisible = false;
                            //dgv_clinic.RowHeadersVisible = false;
                            dgv_clinic.EnableHeadersVisualStyles = false;
                            dgv_clinic.BorderStyle = BorderStyle.None;
                            dgv_clinic.Enabled = false;
                            //dgv_clinic.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            dgv_clinic.CellBorderStyle = DataGridViewCellBorderStyle.None;
                            dgv_clinic.Location = new Point(x, y);
                            panel6.Controls.Add(dgv_clinic);
                            DataGridViewColumn column = dgv_clinic.Columns[3];
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            //dgv_clinic.Columns[3].AutoSizeMode =                        //dgv_clinic.RowCount = 0;
                            dgv_clinic.ColumnHeadersVisible = false;
                            dgv_clinic.RowHeadersVisible = false;
                            //dgv_clinic.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            for (int jt = 0; jt < dt_cf_main.Rows.Count; jt++)
                            {

                                string heading = "";
                                DataTable dt_clinic = this.cntrl.dt_cf_Complaints(dt_cf_main.Rows[0]["id"].ToString());
                                if (dt_clinic.Rows.Count > 0)
                                {
                                    heading = "Complaints";
                                    foreach (DataRow drr in dt_clinic.Rows)
                                    {

                                        dgv_clinic.Rows.Add("0", heading, "", drr["complaint_id"].ToString());
                                        heading = "";
                                        dgv_clinic.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                        dgv_clinic.Rows[i].Cells[1].Style.ForeColor = Color.DarkBlue;
                                        dgv_clinic.Rows[i].Cells[3].Style.ForeColor = Color.DarkBlue;
                                        dgv_clinic.Columns[0].Visible = false;
                                        i = i + 1;
                                    }
                                    //i = i + 1;
                                    dgv_clinic.Rows.Add("", "", "", "");
                                    dgv_clinic.Rows[i].Height = 1;
                                    dgv_clinic.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;

                                    i = i + 1;
                                }
                                //System.Data.DataTable dt_cf_observe = this.cntrl.dt_cf_observe(dt_cf_main.Rows[0]["id"].ToString());
                                //if (dt_cf_observe.Rows.Count > 0)
                                //{
                                //    heading = "Observations";
                                //    foreach (DataRow drr in dt_cf_observe.Rows)
                                //    {
                                //        //i = i + 1;
                                //        dgv_clinic.Rows.Add("0", heading,"", drr["observation_id"].ToString());
                                //        heading = "";
                                //        dgv_clinic.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                //        dgv_clinic.Rows[i].Cells[1].Style.ForeColor = Color.DarkBlue;
                                //        dgv_clinic.Rows[i].Cells[3].Style.ForeColor = Color.DarkBlue;
                                //        dgv_clinic.Columns[0].Visible = false;
                                //        i = i + 1;

                                //    }

                                //    dgv_clinic.Rows.Add("", "", "","");
                                //    dgv_clinic.Rows[i].Height = 1;
                                //    dgv_clinic.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                                //    i = i + 1;
                                //}
                                //System.Data.DataTable dt_cf_investigation = this.cntrl.dt_cf_investigation(dt_cf_main.Rows[jt]["id"].ToString());
                                //if (dt_cf_investigation.Rows.Count > 0)
                                //{
                                //    heading = "Investigation";
                                //    foreach (DataRow drr in dt_cf_investigation.Rows)
                                //    {
                                //        //i = i + 1;
                                //        dgv_clinic.Rows.Add("0", heading,"", drr["investigation_id"].ToString());
                                //        heading = ""; 
                                //        dgv_clinic.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                //        dgv_clinic.Rows[i].Cells[1].Style.ForeColor = Color.DarkBlue;
                                //        dgv_clinic.Rows[i].Cells[3].Style.ForeColor = Color.DarkBlue;
                                //        dgv_clinic.Columns[0].Visible = false;
                                //        i = i + 1;
                                //    }
                                //    //i = i + 1;
                                //    dgv_clinic.Rows.Add("", "", "", "");
                                //    dgv_clinic.Rows[i].Height = 1;
                                //    dgv_clinic.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                                //    i = i + 1;
                                //}
                                System.Data.DataTable dt_cf_diagnosis = this.cntrl.dt_cf_diagnosis(dt_cf_main.Rows[jt]["id"].ToString());
                                if (dt_cf_diagnosis.Rows.Count > 0)
                                {
                                    heading = "Diagnosis";
                                    foreach (DataRow drr in dt_cf_diagnosis.Rows)
                                    {
                                        //i = i + 1;
                                        dgv_clinic.Rows.Add("0", heading, "", drr["diagnosis_id"].ToString());
                                        heading = "";
                                        dgv_clinic.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                        dgv_clinic.Rows[i].Cells[1].Style.ForeColor = Color.DarkBlue;
                                        dgv_clinic.Rows[i].Cells[3].Style.ForeColor = Color.DarkBlue;
                                        dgv_clinic.Columns[0].Visible = false;
                                        i = i + 1;
                                    }
                                    dgv_clinic.Rows.Add("", "", "", "");
                                    dgv_clinic.Rows[i].Height = 1;
                                    dgv_clinic.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                                    i = i + 1;
                                }
                                System.Data.DataTable dt_cf_note = this.cntrl.dt_cf_note(dt_cf_main.Rows[jt]["id"].ToString());
                                if (dt_cf_note.Rows.Count > 0)
                                {
                                    heading = "Notes";
                                    foreach (DataRow drr in dt_cf_note.Rows)
                                    {
                                        //i = i + 1;
                                        dgv_clinic.Rows.Add("0", heading, "", drr["note_name"].ToString());
                                        heading = "";
                                        dgv_clinic.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                        dgv_clinic.Rows[i].Cells[1].Style.ForeColor = Color.DarkBlue;
                                        dgv_clinic.Rows[i].Cells[3].Style.ForeColor = Color.DarkBlue;
                                        dgv_clinic.Columns[0].Visible = false;
                                        i = i + 1;
                                    }

                                }
                            }
                            c_hight = dgv_clinic.Height;
                            last_grid_height = y;
                            y = y + (c_hight + 2);
                        }
                        // Treatment
                        DataTable dt_treatmnt = this.cntrl.Load_treatments(patient_id, date);
                        int j = 0;
                        if (dt_treatmnt.Rows.Count > 0)
                        {
                            //y = y + (c_hight + 2);
                            Label lbl = new Label();
                            lbl.Text = "Treatments";
                            lbl.BackColor = Color.FromArgb(209, 226, 237);
                            lbl.ForeColor = Color.Navy;
                            lbl.Location = new Point(x, y);
                            lbl.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                            panel6.Controls.Add(lbl);
                            y = y + (lbl.Height + 10);
                            DataGridView dataGridView1_treatment_paln = new DataGridView();
                            dataGridView1_treatment_paln.BackgroundColor = Color.White;
                            dataGridView1_treatment_paln.Height = 180;
                            dataGridView1_treatment_paln.Width = 520;
                            dataGridView1_treatment_paln.ColumnCount = 6;
                            dataGridView1_treatment_paln.Columns[0].Width = 20;
                            dataGridView1_treatment_paln.Columns[1].Width = 150;
                            dataGridView1_treatment_paln.Columns[2].Width = 100;
                            dataGridView1_treatment_paln.Columns[3].Width = 100;
                            dataGridView1_treatment_paln.Columns[4].Width = 100;
                            dataGridView1_treatment_paln.Columns[5].Width = 100;
                            DataGridViewColumn column = dataGridView1_treatment_paln.Columns[5];
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            dataGridView1_treatment_paln.Columns[0].Visible = false;
                            dataGridView1_treatment_paln.ColumnHeadersVisible = false;
                            dataGridView1_treatment_paln.RowHeadersVisible = false;
                            dataGridView1_treatment_paln.EnableHeadersVisualStyles = false;
                            dataGridView1_treatment_paln.BorderStyle = BorderStyle.Fixed3D;
                            //dataGridView1_treatment_paln.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                            //dataGridView1_treatment_paln.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                            //dgv_vitals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            dataGridView1_treatment_paln.CellBorderStyle = DataGridViewCellBorderStyle.None;
                            dataGridView1_treatment_paln.Location = new Point(x, y);
                            panel6.Controls.Add(dataGridView1_treatment_paln);
                            dataGridView1_treatment_paln.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                            DataTable dt_pt_sub = this.cntrl.treatment_sub_details(dt_treatmnt.Rows[0]["id"].ToString());
                            if (dt_pt_sub.Rows.Count > 0)
                            {
                                Double totalEst = 0;
                                dataGridView1_treatment_paln.Rows.Add(dt_treatmnt.Rows[0]["id"].ToString(), "TREATMENTS", "COST", "DISCOUNT", "TOTAL", "NOTE");
                                dataGridView1_treatment_paln.Rows[j].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                                dataGridView1_treatment_paln.Rows[j].Cells[1].Style.ForeColor = Color.White;
                                dataGridView1_treatment_paln.Rows[j].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                                dataGridView1_treatment_paln.Rows[j].Cells[2].Style.ForeColor = Color.White;
                                dataGridView1_treatment_paln.Rows[j].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                                dataGridView1_treatment_paln.Rows[j].Cells[3].Style.ForeColor = Color.White;
                                dataGridView1_treatment_paln.Rows[j].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                                dataGridView1_treatment_paln.Rows[j].Cells[4].Style.ForeColor = Color.White;
                                dataGridView1_treatment_paln.Rows[j].Cells[5].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                                dataGridView1_treatment_paln.Rows[j].Cells[5].Style.ForeColor = Color.White;
                                dataGridView1_treatment_paln.Rows[j].Cells[0].Style.BackColor = Color.FromArgb(0, 51, 102);
                                dataGridView1_treatment_paln.Rows[j].Cells[1].Style.BackColor = Color.FromArgb(0, 51, 102);
                                dataGridView1_treatment_paln.Rows[j].Cells[2].Style.BackColor = Color.FromArgb(0, 51, 102);
                                dataGridView1_treatment_paln.Rows[j].Cells[3].Style.BackColor = Color.FromArgb(0, 51, 102);
                                dataGridView1_treatment_paln.Rows[j].Cells[4].Style.BackColor = Color.FromArgb(0, 51, 102);
                                dataGridView1_treatment_paln.Rows[j].Cells[5].Style.BackColor = Color.FromArgb(0, 51, 102);
                                dataGridView1_treatment_paln.Rows[j].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                dataGridView1_treatment_paln.Rows[j].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                dataGridView1_treatment_paln.Rows[j].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                for (int k = 0; k < dt_pt_sub.Rows.Count; k++)
                                {
                                    string discount_string = "";
                                    if (dt_pt_sub.Rows[k]["discount_type"].ToString() == "INR")
                                    {
                                        discount_string = "";
                                    }
                                    else
                                    {
                                        discount_string = "(" + dt_pt_sub.Rows[k]["discount"].ToString() + "%)";
                                    }
                                    Decimal totalcost = Convert.ToDecimal(dt_pt_sub.Rows[k]["cost"].ToString()) * Convert.ToDecimal(dt_pt_sub.Rows[k]["quantity"].ToString());
                                    j = j + 1;
                                    dataGridView1_treatment_paln.Rows.Add(dt_pt_sub.Rows[k]["id"].ToString(), dt_pt_sub.Rows[k]["procedure_name"].ToString(), String.Format("{0:C2}", Convert.ToDecimal(totalcost)), String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discount_inrs"].ToString())) + discount_string, String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString())), dt_pt_sub.Rows[k]["note"].ToString() + " " + dt_pt_sub.Rows[k]["tooth"].ToString());
                                    dataGridView1_treatment_paln.Rows[j].Height = 30;
                                    //dataGridView1_treatment_paln.Columns[6].Width = 200;
                                    dataGridView1_treatment_paln.Rows[j].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                    dataGridView1_treatment_paln.Rows[j].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                    dataGridView1_treatment_paln.Rows[j].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                    totalEst = totalEst + Convert.ToDouble(dt_pt_sub.Rows[k]["total"].ToString());
                                    //dataGridView1_treatment_paln.Rows[j].Cells[8].Value = PappyjoeMVC.Properties.Resources.blank;

                                    j = j + 1;
                                    dataGridView1_treatment_paln.Rows.Add("", "Planned by " + dt_treatmnt.Rows[0]["dr_name"].ToString(), "", "Estimated amount:", String.Format("{0:C2}", Convert.ToDecimal(totalEst)));
                                    dataGridView1_treatment_paln.Rows[j].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                                    dataGridView1_treatment_paln.Rows[j].Cells[1].Style.ForeColor = Color.Red;
                                    dataGridView1_treatment_paln.Rows[j].Cells[5].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                                    //dataGridView1_treatment_paln.Rows[j].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                                    //dataGridView1_treatment_paln.Rows[j].Cells[8].Value = PappyjoeMVC.Properties.Resources.blank;
                                    dataGridView1_treatment_paln.Rows[j].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                    j = j + 1;
                                    dataGridView1_treatment_paln.Rows.Add("", "", "", "", "", "");
                                    //dataGridView1_treatment_paln.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                                    //dataGridView1_treatment_paln.Rows[i].Cells[8].Value = PappyjoeMVC.Properties.Resources.blank;
                                    j = j + 1;
                                }

                                tre_hight = dataGridView1_treatment_paln.Height;
                                last_grid_height = y;
                                y = y + (tre_hight + 5);
                            }

                            Point locationOnForm = dataGridView1_treatment_paln.FindForm().PointToClient(
  dataGridView1_treatment_paln.Parent.PointToScreen(dataGridView1_treatment_paln.Location));
                            last_grid_height = Convert.ToInt32(locationOnForm.Y.ToString());

                        }

                        //prescription
                        DataTable dt_pre_main = this.cntrl.Get_maindtails(patient_id, date);
                        int p = 0;
                        if (dt_pre_main.Rows.Count > 0)
                        {
                            //y = y + (tre_hight + 2);
                            Label lbl = new Label();
                            lbl.Text = "Prescription";
                            lbl.BackColor = Color.FromArgb(209, 226, 237);
                            lbl.ForeColor = Color.Navy;
                            lbl.Location = new Point(x, y);
                            lbl.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                            panel6.Controls.Add(lbl);
                            y = y + (lbl.Height + 10);
                            DataGridView dataGridView1 = new DataGridView();
                            dataGridView1.BackgroundColor = Color.White;
                            dataGridView1.Height = 180;
                            dataGridView1.Width = 520;
                            dataGridView1.ColumnCount = 5;
                            dataGridView1.Columns[0].Width = 10;

                            dataGridView1.Columns[1].Width = 190;
                            dataGridView1.Columns[2].Width = 100;
                            dataGridView1.Columns[3].Width = 100;
                            dataGridView1.Columns[4].Width = 120;
                            dataGridView1.ColumnHeadersVisible = false;
                            dataGridView1.RowHeadersVisible = false;
                            dataGridView1.EnableHeadersVisualStyles = false;
                            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
                            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                            //dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                            //dgv_vitals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
                            dataGridView1.Location = new Point(x, y);
                            panel6.Controls.Add(dataGridView1);
                            dataGridView1.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                            dataGridView1.Rows.Add(dt_pre_main.Rows[0]["id"].ToString(), "Drug", "Frequency", "Duration", "Instruction");
                            dataGridView1.Rows[p].Cells[1].Style.BackColor = Color.FromArgb(0, 51, 102);
                            dataGridView1.Rows[p].Cells[2].Style.BackColor = Color.FromArgb(0, 51, 102);
                            dataGridView1.Rows[p].Cells[3].Style.BackColor = Color.FromArgb(0, 51, 102);
                            dataGridView1.Rows[p].Cells[4].Style.BackColor = Color.FromArgb(0, 51, 102);
                            //dataGridView1.Rows[i].Cells[5].Style.BackColor = Color.LightGray;
                            dataGridView1.Rows[p].Cells[1].Style.ForeColor = Color.White;
                            dataGridView1.Rows[p].Cells[2].Style.ForeColor = Color.White;
                            dataGridView1.Rows[p].Cells[3].Style.ForeColor = Color.White;
                            dataGridView1.Rows[p].Cells[4].Style.ForeColor = Color.White;

                            dataGridView1.Columns[0].Visible = false;
                            //dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.Bill;
                            System.Data.DataTable dt_prescription = this.cntrl.prescription_detoails(dt_pre_main.Rows[0]["id"].ToString());
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
                                        dataGridView1.Rows.Add("0", dt_prescription.Rows[k]["drug_type"].ToString() + " " + dt_prescription.Rows[k]["drug_name"].ToString() + " " + dt_prescription.Rows[k]["strength"].ToString() + " " + "", "", duration, dt_prescription.Rows[k]["add_instruction"].ToString());

                                    }
                                    else
                                    {
                                        dataGridView1.Rows.Add("0", dt_prescription.Rows[k]["drug_type"].ToString() + " " + dt_prescription.Rows[k]["drug_name"].ToString() + " " + dt_prescription.Rows[k]["strength"].ToString() + " " + dt_prescription.Rows[k]["strength_gr"].ToString(), morning + " - " + noon + " - " + night, duration, dt_prescription.Rows[k]["add_instruction"].ToString());

                                    }
                                    //dataGridView1.Rows[k].Cells[].Value = "0";





                                    //dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.blank;
                                    dataGridView1.Rows[p].Height = 30;
                                }
                                p = p + 1;
                                dataGridView1.Rows.Add("0", "Prescribed by Dr." + dt_pre_main.Rows[0]["doctor_name"].ToString(), "", "");
                                dataGridView1.Rows[p].Cells[1].Style.ForeColor = Color.Red;
                                dataGridView1.Rows[p].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                                //dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.blank;
                                p = p + 1;
                                dataGridView1.Rows.Add("0", "", "", "");
                                //dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.blank;
                                p = p + 1;
                            }

                            pre_height = dataGridView1.Height;
                            last_grid_height = y;

                            y = y + (pre_height + 2);
                            Point locationOnForm = dataGridView1.FindForm().PointToClient(
   dataGridView1.Parent.PointToScreen(dataGridView1.Location));
                            last_grid_height = Convert.ToInt32(locationOnForm.Y.ToString());
                        }

                    }

                    //last_grid_height = y- pre_height;
                }


            }
        }
        public bool show_flag = false;
        double BMI;
        public int last_grid_height = 0;
        //private bool clinic_print1;
        //private bool vital_print1;
        //private bool pres_print1;
        //private bool treat_print1;
        int GetDataGridViewHeight(DataGridView dataGridView)
        {
            var sum = (dataGridView.ColumnHeadersVisible ? dataGridView.ColumnHeadersHeight : 0) +
                      dataGridView.Rows.OfType<DataGridViewRow>().Where(r => r.Visible).Sum(r => r.Height);

            return sum;
        }
        private void btn_grp_by_visit_Click(object sender, EventArgs e)
        {
            show_flag = true; //last_grid_height = 0;
            flowLayoutPanel2.Controls.Clear();
            if (patient_id != "")
            {
                //int x = 4;
                //int y = 4;
                DataTable dt_show = this.cntrl.dt_appointments_showmore(patient_id);
                if(dt_show.Rows.Count>2)
                {
                    lb_showmore.Visible = true;
                }
                else
                {
                    lb_showmore.Visible = false;
                }
                DataTable dt_appoint = this.cntrl.dt_appointments(patient_id);
                if (dt_appoint.Rows.Count > 0)
                {
                    v_hignt = 0; c_hight = 0; tre_hight = 0; pre_height=0;
                    //x = 4;
                    //y = 4;
                    foreach (DataRow dr in dt_appoint.Rows)
                    {
                        Label lb = new Label();
                        lb.Text = String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(dr["start_datetime"].ToString()));
                        lb.BackColor = Color.FromArgb(51, 187, 255); ;// FromArgb(209, 226, 237);
                        lb.ForeColor = Color.White;
                        lb.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                        lb.Dock = DockStyle.Top;
                        //lb.Location = new Point(x, y);
                        lb.Width = 559;
                        lb.Height = 29;
                        this.flowLayoutPanel2.Controls.Add(lb);//  panel6.Controls.Add(lb);
                        //y = y + (lb.Height+ 10);
                        string date = Convert.ToDateTime(dr["start_datetime"].ToString()).ToString("yyyy/MM/dd");
                        DataTable dt_vitals = this.cntrl.vitals_grp_visit_main(patient_id, date);
                        if (dt_vitals.Rows.Count > 0)
                        {
                            //Label lbw = new Label();
                            //lbw.Text = "";
                            //lbw.BackColor = Color.White;// Color.FromArgb(209, 226, 237);
                            //lbw.ForeColor = Color.White;
                            //lbw.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                            //this.flowLayoutPanel2.Controls.Add(lbw);

                            Label lbl = new Label();
                            lbl.Text = "Vital Signs";
                            lbl.BackColor = Color.White;
                            lbl.ForeColor = Color.Navy;
                            //lbl.Location = new Point(x, y);
                            lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                           this. flowLayoutPanel2.Controls.Add(lbl);
                            //y = y + (lbl.Height + 10);
                            for (int j1 = 0; j1 < dt_vitals.Rows.Count; j1++)
                            {
                                DataGridView dgv_vitals = new DataGridView();
                            dgv_vitals.BackgroundColor = Color.White;
                            //dgv_vitals.Height = 230;
                            dgv_vitals.Width = 559;
                            dgv_vitals.ColumnCount = 3;
                            dgv_vitals.Columns[0].Width = 200;
                            dgv_vitals.Columns[1].Width = 10;
                            dgv_vitals.Columns[2].Width = 225;
                            dgv_vitals.ColumnHeadersVisible = false;
                            dgv_vitals.RowHeadersVisible = false;
                            dgv_vitals.EnableHeadersVisualStyles = false;
                            dgv_vitals.BorderStyle = BorderStyle.Fixed3D;
                            dgv_vitals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                            dgv_vitals.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                            //dgv_vitals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            dgv_vitals.CellBorderStyle = DataGridViewCellBorderStyle.None;
                            //dgv_vitals.Location = new Point(x, y);
                            this.flowLayoutPanel2.Controls.Add(dgv_vitals);// panel6.Controls.Add(dgv_vitals);
                            dgv_vitals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                            dgv_vitals.AllowUserToAddRows = false;
                            dgv_vitals.AllowUserToResizeRows = false;
                            dgv_vitals.AllowUserToDeleteRows = false;
                            dgv_vitals.AllowUserToOrderColumns = false; dgv_vitals.ReadOnly = true;
                            dgv_vitals.AllowUserToResizeColumns = false;
                            dgv_vitals.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            dgv_vitals.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            int ri = 0;
                           

                                if (dt_vitals.Rows[j1]["pulse"].ToString() != "")
                                {
                                    dgv_vitals.Rows.Add("PULSE ", ":", dt_vitals.Rows[j1]["pulse"].ToString()+ "(Heart Beats Per Minute)");
                                    //
                                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                    dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                    dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    ri = ri + 1;
                                }
                                if (dt_vitals.Rows[j1]["temp"].ToString() != "")
                                {
                                    dgv_vitals.Rows.Add("TEMPERATURE ", ":", dt_vitals.Rows[j1]["temp"].ToString() + " ( " + dt_vitals.Rows[j1]["temp_type"].ToString() + " ) "+" (C)");

                                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                    dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                    dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    ri = ri + 1;
                                }
                                if (dt_vitals.Rows[j1]["bp_syst"].ToString() != "")
                                {
                                    dgv_vitals.Rows.Add("BLOOD PRESSURE ( SYSTOLIC ) ", ":", dt_vitals.Rows[j1]["bp_syst"].ToString() + " (mm Hg)");//+ " ( " + dt_vitals.Rows[j1]["bp_type"].ToString() + " ) "

                                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                    dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                    dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    ri = ri + 1;
                                }
                                if (dt_vitals.Rows[j1]["bp_dia"].ToString() != "")
                                {
                                    dgv_vitals.Rows.Add("BLOOD PRESSURE ( DIASTOLIC )  ", ":", dt_vitals.Rows[j1]["bp_dia"].ToString() + " (mm Hg)");//+ " ( " + dt_vitals.Rows[j1]["bp_type"].ToString() + " ) "
                                    //ri = ri + 1;
                                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                    dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                    dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    ri = ri + 1;
                                }
                                if (dt_vitals.Rows[j1]["Height"].ToString() != "")
                                {
                                    dgv_vitals.Rows.Add("HEIGHT  ", ":", dt_vitals.Rows[j1]["Height"].ToString() + " (Cm)");
                                    //ri = ri + 1;
                                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                    dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                    dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    ri = ri + 1;
                                }
                                if (dt_vitals.Rows[j1]["weight"].ToString() != "")
                                {
                                    dgv_vitals.Rows.Add("WEIGHT  ", ":", dt_vitals.Rows[j1]["weight"].ToString() + " (Kg)");
                                    //ri = ri + 1;
                                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                    dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                    dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    ri = ri + 1;
                                }
                                if (dt_vitals.Rows[j1]["resp"].ToString() != "")
                                {
                                    dgv_vitals.Rows.Add("RESPIRATORY RATE ", ":", dt_vitals.Rows[j1]["resp"].ToString()+ " (Breaths/min)");
                                    //ri = ri + 1;
                                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                    dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                    dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    ri = ri + 1;
                                }
                                if (dt_vitals.Rows[j1]["weight"].ToString() != null && dt_vitals.Rows[j1]["weight"].ToString() != "" && dt_vitals.Rows[j1]["Height"].ToString() != null && dt_vitals.Rows[j1]["Height"].ToString() != "")
                                {
                                    weight = Convert.ToDouble(dt_vitals.Rows[j1]["weight"].ToString());
                                    height = Convert.ToDouble(dt_vitals.Rows[j1]["Height"].ToString());
                                }
                                else
                                {
                                    weight = Convert.ToDouble("0.00");
                                    height = Convert.ToDouble("0.00");
                                }
                                gender = lb_gender.Text;
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
                                        dgv_vitals.Rows.Add("BMI(BOADY MASS INDEX) ", ":", BMI + "  ,   " + msg);
                                       
                                        if (msg == "BMI is low")
                                            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.Red;
                                        else if (msg == "Normal")
                                            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                        else if (msg == "BMI is High")
                                            dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.Red;
                                        dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                        dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                        dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                        dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                        ri = ri + 1;
                                    }
                                }
                                if (dt_vitals.Rows[j1]["spo"].ToString() != "")
                                {
                                    dgv_vitals.Rows.Add("SPO2 ", ":", dt_vitals.Rows[j1]["spo"].ToString()+" (%)");
                                    //ri = ri + 1;
                                    dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.DarkBlue;
                                    dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dgv_vitals.Rows[ri].Cells[2].Style.ForeColor = Color.DarkGreen;
                                    dgv_vitals.Rows[ri].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    ri = ri + 1;
                                }
                                dgv_vitals.Rows.Add("", "", ""); ri = ri + 1;
                                dgv_vitals.Rows.Add("Recorded By : Dr." + dt_vitals.Rows[j1]["dr_name"].ToString(), "", "");
                                dgv_vitals.Rows[ri].Cells[0].Style.ForeColor = Color.Red;
                                dgv_vitals.Rows[ri].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Italic);
                                //ri = ri + 2;
                                int heignht = GetDataGridViewHeight(dgv_vitals);
                                dgv_vitals.Height = heignht;
                                v_hignt = this.flowLayoutPanel2.Controls.GetChildIndex(dgv_vitals);
                            }
                            
                            //last_grid_height = y;
                            //y = y + (v_hignt + 2);
                        }
                        //clinic
                        DataTable dt_cf_main = this.cntrl.dt_clinic_main_grp_visi(patient_id, date);
                       
                        if (dt_cf_main.Rows.Count > 0)
                        {
                            //Label lbw = new Label();
                            //lbw.Text = "";
                            //lbw.BackColor = Color.White;// Color.FromArgb(209, 226, 237);
                            //lbw.ForeColor = Color.White;
                            //lbw.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                            //this.flowLayoutPanel2.Controls.Add(lbw);

                            Label lbl = new Label();
                            lbl.Text = "Clinical Findings";
                            lbl.Width = 250;
                            lbl.BackColor = Color.White;
                            lbl.ForeColor = Color.Navy;
                            //lbl.Location = new Point(x, y);
                            lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                            //Label lbl = new Label();
                            //lbl.Text = "Clinical Findings";
                            //lbl.Location = new Point(x,y);
                            //panel6.Controls.Add(lbl);
                            this.flowLayoutPanel2.Controls.Add(lbl);

                            //y = y + (lbl.Height + 5);
                            for (int jt = 0; jt < dt_cf_main.Rows.Count; jt++)
                            {
                                int i = 0;
                                DataGridView dgv_clinic = new DataGridView();
                            //dgv_clinic.Height = 260;
                            dgv_clinic.Width = 559;
                            dgv_clinic.ColumnCount = 4;
                            dgv_clinic.Columns[0].Width = 20;
                            dgv_clinic.Columns[1].Width = 150;
                            dgv_clinic.Columns[2].Width = 1;
                            dgv_clinic.Columns[3].Width = 300;
                            dgv_clinic.BackgroundColor = Color.White;
                            //dgv_clinic.ColumnHeadersVisible = false;
                            //dgv_clinic.RowHeadersVisible = false;
                            dgv_clinic.EnableHeadersVisualStyles = false;
                            dgv_clinic.BorderStyle = BorderStyle.Fixed3D;
                            dgv_clinic.Enabled = false;
                            //dgv_clinic.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            dgv_clinic.CellBorderStyle = DataGridViewCellBorderStyle.None;
                            //dgv_clinic.Location = new Point(x, y);
                            this.flowLayoutPanel2.Controls.Add(dgv_clinic);
                            DataGridViewColumn column = dgv_clinic.Columns[3];
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            dgv_clinic.ReadOnly = true;                        //dgv_clinic.RowCount = 0;
                            dgv_clinic.ColumnHeadersVisible = false;
                            dgv_clinic.RowHeadersVisible = false;
                            dgv_clinic.AllowUserToAddRows = false;
                            dgv_clinic.AllowUserToResizeRows = false;
                            dgv_clinic.AllowUserToDeleteRows = false;
                            dgv_clinic.AllowUserToOrderColumns = false;
                            dgv_clinic.AllowUserToResizeColumns = false;
                            //dgv_clinic.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            

                                string heading = "";
                                DataTable dt_clinic = this.cntrl.dt_cf_Complaints(dt_cf_main.Rows[jt]["id"].ToString());
                                if (dt_clinic.Rows.Count > 0)
                                {
                                    heading = "Complaints";
                                    foreach (DataRow drr in dt_clinic.Rows)
                                    {

                                        dgv_clinic.Rows.Add("0", heading, "", drr["complaint_id"].ToString());
                                        heading = "";
                                        dgv_clinic.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                        dgv_clinic.Rows[i].Cells[1].Style.ForeColor = Color.DarkBlue;
                                        dgv_clinic.Rows[i].Cells[3].Style.ForeColor = Color.DarkBlue;
                                        dgv_clinic.Columns[0].Visible = false;
                                        i = i + 1;
                                    }
                                    //i = i + 1;
                                    dgv_clinic.Rows.Add("", "", "", "");
                                    dgv_clinic.Rows[i].Height = 1;
                                    dgv_clinic.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;

                                    i = i + 1;
                                }
                                //System.Data.DataTable dt_cf_observe = this.cntrl.dt_cf_observe(dt_cf_main.Rows[0]["id"].ToString());
                                //if (dt_cf_observe.Rows.Count > 0)
                                //{
                                //    heading = "Observations";
                                //    foreach (DataRow drr in dt_cf_observe.Rows)
                                //    {
                                //        //i = i + 1;
                                //        dgv_clinic.Rows.Add("0", heading,"", drr["observation_id"].ToString());
                                //        heading = "";
                                //        dgv_clinic.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                //        dgv_clinic.Rows[i].Cells[1].Style.ForeColor = Color.DarkBlue;
                                //        dgv_clinic.Rows[i].Cells[3].Style.ForeColor = Color.DarkBlue;
                                //        dgv_clinic.Columns[0].Visible = false;
                                //        i = i + 1;

                                //    }

                                //    dgv_clinic.Rows.Add("", "", "","");
                                //    dgv_clinic.Rows[i].Height = 1;
                                //    dgv_clinic.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                                //    i = i + 1;
                                //}
                                //System.Data.DataTable dt_cf_investigation = this.cntrl.dt_cf_investigation(dt_cf_main.Rows[jt]["id"].ToString());
                                //if (dt_cf_investigation.Rows.Count > 0)
                                //{
                                //    heading = "Investigation";
                                //    foreach (DataRow drr in dt_cf_investigation.Rows)
                                //    {
                                //        //i = i + 1;
                                //        dgv_clinic.Rows.Add("0", heading,"", drr["investigation_id"].ToString());
                                //        heading = ""; 
                                //        dgv_clinic.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                //        dgv_clinic.Rows[i].Cells[1].Style.ForeColor = Color.DarkBlue;
                                //        dgv_clinic.Rows[i].Cells[3].Style.ForeColor = Color.DarkBlue;
                                //        dgv_clinic.Columns[0].Visible = false;
                                //        i = i + 1;
                                //    }
                                //    //i = i + 1;
                                //    dgv_clinic.Rows.Add("", "", "", "");
                                //    dgv_clinic.Rows[i].Height = 1;
                                //    dgv_clinic.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                                //    i = i + 1;
                                //}
                                System.Data.DataTable dt_cf_diagnosis = this.cntrl.dt_cf_diagnosis(dt_cf_main.Rows[jt]["id"].ToString());
                                if (dt_cf_diagnosis.Rows.Count > 0)
                                {
                                    heading = "Diagnosis";
                                    foreach (DataRow drr in dt_cf_diagnosis.Rows)
                                    {
                                        //i = i + 1;
                                        dgv_clinic.Rows.Add("0", heading, "", drr["diagnosis_id"].ToString());
                                        heading = "";
                                        dgv_clinic.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                        dgv_clinic.Rows[i].Cells[1].Style.ForeColor = Color.DarkBlue;
                                        dgv_clinic.Rows[i].Cells[3].Style.ForeColor = Color.DarkBlue;
                                        dgv_clinic.Columns[0].Visible = false;
                                        i = i + 1;
                                    }
                                    dgv_clinic.Rows.Add("", "", "", "");
                                    dgv_clinic.Rows[i].Height = 1;
                                    dgv_clinic.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                                    i = i + 1;
                                }
                                System.Data.DataTable dt_cf_note = this.cntrl.dt_cf_note(dt_cf_main.Rows[jt]["id"].ToString());
                                if (dt_cf_note.Rows.Count > 0)
                                {
                                    heading = "Notes";
                                    foreach (DataRow drr in dt_cf_note.Rows)
                                    {
                                        //i = i + 1;
                                        dgv_clinic.Rows.Add("0", heading, "", drr["note_name"].ToString());
                                        heading = "";
                                        dgv_clinic.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                                        dgv_clinic.Rows[i].Cells[1].Style.ForeColor = Color.DarkBlue;
                                        dgv_clinic.Rows[i].Cells[3].Style.ForeColor = Color.DarkBlue;
                                        dgv_clinic.Columns[0].Visible = false;
                                        i = i + 1;
                                    }

                                }
                                int heignht = GetDataGridViewHeight(dgv_clinic);
                                dgv_clinic.Height = heignht;
                                c_hight = this.flowLayoutPanel2.Controls.GetChildIndex(dgv_clinic);
                            }
                            
                            
                        }

                        // Treatment
                        DataTable dt_treatmnt = this.cntrl.Load_treatments(patient_id, date);
                        int j = 0;
                        if (dt_treatmnt.Rows.Count > 0) 
                        {
                            //Label lbw = new Label();
                            //lbw.Text = "";
                            //lbw.BackColor = Color.White;// Color.FromArgb(209, 226, 237);
                            //lbw.ForeColor = Color.White;
                            //lbw.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                            //this.flowLayoutPanel2.Controls.Add(lbw);

                            Label lbl = new Label();
                            lbl.Text = "Treatments";
                            lbl.BackColor = Color.White;
                            lbl.ForeColor = Color.Navy;
                            lbl.Location = new Point(x, y);
                            lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                            this.flowLayoutPanel2.Controls.Add(lbl);

                            //
                            DataGridView dataGridView1_treatment_paln = new DataGridView();
                            dataGridView1_treatment_paln.BackgroundColor = Color.White;
                            dataGridView1_treatment_paln.Width = 559;
                            //dataGridView1_treatment_paln.ColumnCount = 6;
                            //
                            dataGridView1_treatment_paln.ColumnHeadersVisible = true;
                            dataGridView1_treatment_paln.Columns.Add("id", "ID");
                            dataGridView1_treatment_paln.Columns.Add("TREATMENTS", "TREATMENTS");
                            dataGridView1_treatment_paln.Columns.Add("COST", "COST");
                            dataGridView1_treatment_paln.Columns.Add("DISCOUNT", "DISCOUNT");
                            dataGridView1_treatment_paln.Columns.Add("TOTAL", "TOTAL");
                            dataGridView1_treatment_paln.Columns.Add("NOTE", "NOTE");
                            dataGridView1_treatment_paln.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 51, 102);
                            dataGridView1_treatment_paln.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                            //dataGridView1_treatment_paln.ColumnHeadersDefaultCellStyle.Alignment= DataGridViewContentAlignment.MiddleRight;
                            dataGridView1_treatment_paln.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; ;
                            dataGridView1_treatment_paln.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dataGridView1_treatment_paln.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dataGridView1_treatment_paln.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;//.None;
                                                                                                                       //
                            dataGridView1_treatment_paln.Columns[0].Width = 5;
                            dataGridView1_treatment_paln.Columns[1].Width = 174;
                            dataGridView1_treatment_paln.Columns[2].Width = 81;
                            dataGridView1_treatment_paln.Columns[3].Width = 121;
                            dataGridView1_treatment_paln.Columns[4].Width = 90;
                            dataGridView1_treatment_paln.Columns[5].Width = 70;
                            dataGridView1_treatment_paln.Columns[0].Visible = false;
                            dataGridView1_treatment_paln.RowHeadersVisible = false;
                            dataGridView1_treatment_paln.EnableHeadersVisualStyles = false;
                            dataGridView1_treatment_paln.BorderStyle = BorderStyle.Fixed3D;
                            dataGridView1_treatment_paln.CellBorderStyle = DataGridViewCellBorderStyle.None;
                            dataGridView1_treatment_paln.Location = new Point(x, y);
                            this.flowLayoutPanel2.Controls.Add(dataGridView1_treatment_paln);
                            dataGridView1_treatment_paln.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                            dataGridView1_treatment_paln.ReadOnly = true;
                            dataGridView1_treatment_paln.RowHeadersVisible = false;
                            dataGridView1_treatment_paln.AllowUserToAddRows = false;
                            dataGridView1_treatment_paln.AllowUserToResizeRows = false;
                            dataGridView1_treatment_paln.AllowUserToDeleteRows = false;
                            dataGridView1_treatment_paln.AllowUserToOrderColumns = false;
                            dataGridView1_treatment_paln.AllowUserToResizeColumns = false;
                            for( int n1=0;n1< dt_treatmnt.Rows.Count;n1++)
                            {
                                DataTable dt_pt_sub = this.cntrl.treatment_sub_details(dt_treatmnt.Rows[n1]["id"].ToString());
                                if (dt_pt_sub.Rows.Count > 0)
                                {
                                    Double totalEst = 0;

                                    for (int k = 0; k < dt_pt_sub.Rows.Count; k++)
                                    {
                                        string discount_string = "";
                                        if (dt_pt_sub.Rows[k]["discount_type"].ToString() == "INR")
                                        {
                                            discount_string = "";
                                        }
                                        else
                                        {
                                            discount_string = "(" + dt_pt_sub.Rows[k]["discount"].ToString() + "%)";
                                        }
                                        Decimal totalcost = Convert.ToDecimal(dt_pt_sub.Rows[k]["cost"].ToString()) * Convert.ToDecimal(dt_pt_sub.Rows[k]["quantity"].ToString());

                                        dataGridView1_treatment_paln.Rows.Add(dt_pt_sub.Rows[k]["id"].ToString(), dt_pt_sub.Rows[k]["procedure_name"].ToString(), String.Format("{0:C2}", Convert.ToDecimal(totalcost)), String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discount_inrs"].ToString())) + discount_string, String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString())), dt_pt_sub.Rows[k]["note"].ToString() + " " + dt_pt_sub.Rows[k]["tooth"].ToString());
                                        int h = dataGridView1_treatment_paln.Rows.Count;
                                        //dataGridView1_treatment_paln.Rows[j].Height = 25;
                                        //dataGridView1_treatment_paln.Columns[6].Width = 200;
                                        dataGridView1_treatment_paln.Rows[j].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                        dataGridView1_treatment_paln.Rows[j].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                        dataGridView1_treatment_paln.Rows[j].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                        totalEst = totalEst + Convert.ToDouble(dt_pt_sub.Rows[k]["total"].ToString());
                                        //dataGridView1_treatment_paln.Rows[j].Cells[8].Value = PappyjoeMVC.Properties.Resources.blank;
                                        j++;
                                    }
                                    //j = K + 1;
                                    dataGridView1_treatment_paln.Rows.Add("", "Planned by " + dt_treatmnt.Rows[0]["dr_name"].ToString(), "", "Estimated amount:", String.Format("{0:C2}", Convert.ToDecimal(totalEst)));
                                    dataGridView1_treatment_paln.Rows[j].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dataGridView1_treatment_paln.Rows[j].Cells[1].Style.ForeColor = Color.Red;
                                    dataGridView1_treatment_paln.Rows[j].Cells[3].Style.ForeColor = Color.Red;
                                    dataGridView1_treatment_paln.Rows[j].Cells[4].Style.ForeColor = Color.Red;
                                    dataGridView1_treatment_paln.Rows[j].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    dataGridView1_treatment_paln.Rows[j].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
                                    //dataGridView1_treatment_paln.Rows[j].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                                    //dataGridView1_treatment_paln.Rows[j].Cells[8].Value = PappyjoeMVC.Properties.Resources.blank;
                                    dataGridView1_treatment_paln.Rows[j].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                    j = j + 1;
                                    dataGridView1_treatment_paln.Rows.Add("", "", "", "", "", "");
                                    //dataGridView1_treatment_paln.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                                    //dataGridView1_treatment_paln.Rows[i].Cells[8].Value = PappyjoeMVC.Properties.Resources.blank;
                                    j = j + 1;

                                    int heignht = GetDataGridViewHeight(dataGridView1_treatment_paln);
                                    dataGridView1_treatment_paln.Height = heignht;
                                    tre_hight = this.flowLayoutPanel2.Controls.GetChildIndex(dataGridView1_treatment_paln);
                                    //tre_hight = dataGridView1_treatment_paln.Height;
                                    //last_grid_height = y;
                                    //y = y + (tre_hight + 5);
                                }
                            }
                           


                        }
                        //prescription
                        DataTable dt_pre_main = this.cntrl.Get_maindtails(patient_id, date);
                        int p = 0;
                        if (dt_pre_main.Rows.Count > 0)
                        {
                            //y = y + (tre_hight + 2);
                            Label lbl = new Label();
                            lbl.Text = "Prescription";
                            lbl.BackColor = Color.White;
                            lbl.ForeColor = Color.Navy;
                            lbl.Location = new Point(x, y);
                            lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                            //panel6.Controls.Add(lbl);
                            //y = y + (lbl.Height + 10);
                            this.flowLayoutPanel2.Controls.Add(lbl);


                            DataGridView dataGridView1 = new DataGridView();
                            dataGridView1.BackgroundColor = Color.White;
                            //dataGridView1.Height = 150;
                            dataGridView1.Width = 559;

                            dataGridView1.ColumnHeadersVisible = true;
                            dataGridView1.Columns.Add("id", "ID");
                            dataGridView1.Columns.Add("DRUG", "DRUG");
                            dataGridView1.Columns.Add("FREQUENCY", "FREQUENCY");
                            dataGridView1.Columns.Add("DURATION", "DURATION");
                            dataGridView1.Columns.Add("INSTRUCTION", "INSTRUCTION");
                            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 51, 102);
                            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                            //dataGridView1_treatment_paln.ColumnHeadersDefaultCellStyle.Alignment= DataGridViewContentAlignment.MiddleRight;
                            //dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; ;
                            //dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            //dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;//.None;


                            dataGridView1.Columns[0].Width = 10;

                            dataGridView1.Columns[1].Width = 225;
                            dataGridView1.Columns[2].Width = 100;
                            dataGridView1.Columns[3].Width = 100;
                            dataGridView1.Columns[4].Width = 120;
                            //dataGridView1.ColumnHeadersVisible = false;
                            dataGridView1.RowHeadersVisible = false;
                            dataGridView1.EnableHeadersVisualStyles = false;
                            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
                            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                            //dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                            //dgv_vitals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
                            dataGridView1.Location = new Point(x, y);
                            //dataGridView1.BackgroundColor = Color.White;
                            //dataGridView1.Height = 230;
                            //dataGridView1.Width = 490;
                            //dataGridView1.ColumnCount = 5;
                            dataGridView1.Columns[0].Visible = false;
                            int i = 0;
                            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;//.None;
                            this.flowLayoutPanel2.Controls.Add(dataGridView1);
                            dataGridView1.ReadOnly = true;
                            dataGridView1.RowHeadersVisible = false;
                            dataGridView1.AllowUserToAddRows = false;
                            dataGridView1.AllowUserToResizeRows = false;
                            dataGridView1.AllowUserToDeleteRows = false;
                            dataGridView1.AllowUserToOrderColumns = false;
                            dataGridView1.AllowUserToResizeColumns = false;
                            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                            //dataGridView1.Rows.Add(dt_pre_main.Rows[0]["id"].ToString(), "Drug", "Frequency", "Duration", "Instruction");
                            //dataGridView1.Rows[p].Cells[1].Style.BackColor = Color.FromArgb(0, 51, 102);
                            //dataGridView1.Rows[p].Cells[2].Style.BackColor = Color.FromArgb(0, 51, 102);
                            //dataGridView1.Rows[p].Cells[3].Style.BackColor = Color.FromArgb(0, 51, 102);
                            //dataGridView1.Rows[p].Cells[4].Style.BackColor = Color.FromArgb(0, 51, 102);
                            ////dataGridView1.Rows[i].Cells[5].Style.BackColor = Color.LightGray;
                            //dataGridView1.Rows[p].Cells[1].Style.ForeColor = Color.White;
                            //dataGridView1.Rows[p].Cells[2].Style.ForeColor = Color.White;
                            //dataGridView1.Rows[p].Cells[3].Style.ForeColor = Color.White;
                            //dataGridView1.Rows[p].Cells[4].Style.ForeColor = Color.White;

                            //dataGridView1.Columns[0].Visible = false;
                            //dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.Bill;
                            for(int pr=0;pr< dt_pre_main.Rows.Count;pr++)
                            {
                                System.Data.DataTable dt_prescription = this.cntrl.prescription_detoails(dt_pre_main.Rows[pr]["id"].ToString());

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
                                            dataGridView1.Rows.Add("0", dt_prescription.Rows[k]["drug_type"].ToString() + " " + dt_prescription.Rows[k]["drug_name"].ToString() + " " + dt_prescription.Rows[k]["strength"].ToString() + " " + "", "", duration, dt_prescription.Rows[k]["add_instruction"].ToString());

                                        }
                                        else
                                        {
                                            dataGridView1.Rows.Add("0", dt_prescription.Rows[k]["drug_type"].ToString() + " " + dt_prescription.Rows[k]["drug_name"].ToString() + " " + dt_prescription.Rows[k]["strength"].ToString() + " " + dt_prescription.Rows[k]["strength_gr"].ToString(), morning + " - " + noon + " - " + night, duration, dt_prescription.Rows[k]["add_instruction"].ToString());

                                        }
                                        //dataGridView1.Rows[k].Cells[].Value = "0";
                                        //dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.blank;
                                        dataGridView1.Rows[p].Height = 30;
                                    }
                                    p = p + 1;
                                    dataGridView1.Rows.Add("0", "", "", "");
                                    p = p + 1;
                                    dataGridView1.Rows.Add("0", "Prescribed by Dr." + dt_pre_main.Rows[0]["doctor_name"].ToString(), "", "");
                                    dataGridView1.Rows[p].Cells[1].Style.ForeColor = Color.Red;
                                    dataGridView1.Rows[p].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                                    //dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.blank;
                                    //dataGridView1.Rows.Add("0", "", "", "");
                                    //dataGridView1.Rows[i].Cells[5].Value = PappyjoeMVC.Properties.Resources.blank;
                                    p = p + 1;
                                }
                            }
                         
                            int heignht = GetDataGridViewHeight(dataGridView1);
                            dataGridView1.Height = heignht;
                            pre_height = this.flowLayoutPanel2.Controls.GetChildIndex(dataGridView1);
                            //pre_height = dataGridView1.Height;
   //                         last_grid_height = y;

   //                         y = y + (pre_height + 2);
   //                         Point locationOnForm = dataGridView1.FindForm().PointToClient(
   //dataGridView1.Parent.PointToScreen(dataGridView1.Location));
   //                         last_grid_height = Convert.ToInt32(locationOnForm.Y.ToString());
                        }

                    }
                   
                    //last_grid_height = y- pre_height;
                }


            }
        }
    }
}
