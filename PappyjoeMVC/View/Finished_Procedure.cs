using PappyjoeMVC.Controller;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using PappyjoeMVC.Model;

namespace PappyjoeMVC.View
{
    public partial class Finished_Procedure : Form
    {
        public string patient_id = "0";
        public string doctor_id = "";
        int btnEnabled = 0;
        string treatment_complete_id = "0";
        Finished_Procedre_controller cntrl=new Finished_Procedre_controller();
        Show_Appointment_controller Apptmt_cntrl = new Show_Appointment_controller();
        Vital_Signs_controller vital_cntrl = new Vital_Signs_controller();
        Clinical_Findings_controller clinical_cntrl = new Clinical_Findings_controller();
        Treatment_controller treatmnt_cntrl = new Treatment_controller();
        Prescription_Show_controller prescr_cntrl = new Prescription_Show_controller();
        LabWorks_controller lab_cntrl = new LabWorks_controller();
        Attachments_controller attach_cntrl = new Attachments_controller();
        Invoice_controller invo_cntrl = new Invoice_controller();
        Receipt_controller recpt_cntrl = new Receipt_controller();
        Nurses_Notes_controller nurse_cntrl = new Nurses_Notes_controller(); user_privillage_model privi_mdl = new user_privillage_model();
        Connection db = new Connection();
        public Finished_Procedure()
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

        private void btn_Add_Click(object sender, EventArgs e)
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
        private void FinishedProcedure_Load(object sender, EventArgs e)
        {
            try
            {
                //if (doctor_id != "1")
                // {
                //    string privid;
                //    //privid = this.cntrl.Add_privilliege(doctor_id);
                //    //if (int.Parse(privid) > 0)
                //    //{
                //    //    btn_Add.Enabled = false ;
                //    //    pb_fnsdtrtAdd.Enabled = false;
                //    //}
                //    //else
                //    //{
                //    //    btn_Add.Enabled = true;
                //    //    pb_fnsdtrtAdd.Enabled = true;
                //    //}
                //    //privid = this.cntrl.edit_privillege(doctor_id);
                //    //if (int.Parse(privid) > 0)
                //    //{
                //    //    editToolStripMenuItem1.Enabled = false;
                //    //}
                //    //else
                //    //{
                //    //    editToolStripMenuItem1.Enabled = true;
                //    //}
                //    //privid = this.cntrl.delete_privillage(doctor_id);
                //    //if (int.Parse(privid) > 0)
                //    //{
                //    //    deleteToolStripMenuItem1.Enabled = false;
                //    //}
                //    //else
                //    //{
                //    //    deleteToolStripMenuItem1.Enabled = true;
                //    //}
                //}
                string docnam = this.cntrl.Get_DoctorName(doctor_id);
                System.Data.DataTable rs_patients = this.cntrl.Get_Patient_id_NAme(patient_id);
                if (rs_patients.Rows[0]["pt_name"].ToString() != "")
                {
                    linkLabel_Name.Text = rs_patients.Rows[0]["pt_name"].ToString();
                }
                if (rs_patients.Rows[0]["pt_id"].ToString() != "")
                {
                    linkLabel_id.Text = rs_patients.Rows[0]["pt_id"].ToString();
                }
                dgv_treatment_paln.DefaultCellStyle.SelectionBackColor = Color.White;
                dgv_treatment_paln.ColumnHeadersVisible = false;
                dgv_treatment_paln.RowHeadersVisible = false;
                dgv_treatment_paln.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv_treatment_paln.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv_treatment_paln.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DataTable dtb = this.cntrl.get_completed_id_date(patient_id);
                Load_Data(dtb);
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            DataTable dtb = this.cntrl.get_completed_id_date(patient_id);
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
        public void reciept_count()
        {
            DataTable Payments = db.table("select receipt_no,amount_paid,invoice_no,procedure_name,mode_of_payment from tbl_payment where pt_id='" + patient_id + "'");
            if (Payments.Rows.Count > 0)
            {
                lb_Reciepts_cnt.Text = Payments.Rows.Count.ToString();
            }
        }
        public void Load_Data(DataTable dt_pt_main)
        {
            try
            {
                int i = 0;
                if (dt_pt_main.Rows.Count > 0)
                {
                    for (int j = 0; j < dt_pt_main.Rows.Count; j++)
                    {
                        dgv_treatment_paln.Rows.Add("", "", String.Format("{0:dddd, d MMMM , yyyy}", Convert.ToDateTime(dt_pt_main.Rows[j]["completed_date"].ToString())), "", "", "", "", "0");
                        dgv_treatment_paln.Rows[i].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Bold);
                        dgv_treatment_paln.Rows[i].Cells[2].Style.ForeColor = Color.DarkGreen;
                        dgv_treatment_paln.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                        dgv_treatment_paln.Rows[i].Cells[8].Value = PappyjoeMVC.Properties.Resources.blank;
                        i = i + 1;
                        DataTable dt_enablecost = this.cntrl.get_enablecost();
                        if (dt_enablecost.Rows.Count > 0)
                        {
                            if (dt_enablecost.Rows[0][1].ToString() == "Yes")
                            {
                                dgv_treatment_paln.Rows.Add("", "", "TREATMENTS", "COST", "DISCOUNT", "TOTAL", "NOTE", "0");

                            }
                            else
                            {
                                dgv_treatment_paln.Rows.Add("", "", "TREATMENTS", "", "", "", "NOTE", "0");

                            }
                        }
                        else
                        {
                            dgv_treatment_paln.Rows.Add("", "", "TREATMENTS", "COST", "DISCOUNT", "TOTAL", "NOTE", "0");
                        }
                        dgv_treatment_paln.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.gry;
                        dgv_treatment_paln.Rows[i].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                        dgv_treatment_paln.Rows[i].Cells[2].Style.ForeColor = Color.White;
                        dgv_treatment_paln.Rows[i].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                        dgv_treatment_paln.Rows[i].Cells[3].Style.ForeColor = Color.White;
                        dgv_treatment_paln.Rows[i].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                        dgv_treatment_paln.Rows[i].Cells[4].Style.ForeColor = Color.White;
                        dgv_treatment_paln.Rows[i].Cells[5].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                        dgv_treatment_paln.Rows[i].Cells[5].Style.ForeColor = Color.White;
                        dgv_treatment_paln.Rows[i].Cells[6].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                        dgv_treatment_paln.Rows[i].Cells[6].Style.ForeColor = Color.White;
                        dgv_treatment_paln.Rows[i].Cells[1].Style.BackColor = Color.DarkGray;
                        dgv_treatment_paln.Rows[i].Cells[2].Style.BackColor = Color.DarkGray;
                        dgv_treatment_paln.Rows[i].Cells[3].Style.BackColor = Color.DarkGray;
                        dgv_treatment_paln.Rows[i].Cells[4].Style.BackColor = Color.DarkGray;
                        dgv_treatment_paln.Rows[i].Cells[5].Style.BackColor = Color.DarkGray; 
                        dgv_treatment_paln.Rows[i].Cells[6].Style.BackColor = Color.DarkGray;
                        dgv_treatment_paln.Rows[i].Cells[8].Value = PappyjoeMVC.Properties.Resources.gry;
                        dgv_treatment_paln.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgv_treatment_paln.Rows[i].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgv_treatment_paln.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        foreach (DataGridViewColumn column in dgv_treatment_paln.Columns)
                        {
                            column.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                        System.Data.DataTable dt_pt_sub = this.cntrl.get_completed_details(dt_pt_main.Rows[j]["id"].ToString());
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
                            i = i + 1;
                            if (dt_enablecost.Rows.Count > 0)
                            {
                                if (dt_enablecost.Rows[0][1].ToString() == "Yes")
                                {
                                    dgv_treatment_paln.Rows.Add(dt_pt_sub.Rows[k]["id"].ToString(), "", dt_pt_sub.Rows[k]["procedure_name"].ToString(), String.Format("{0:C2}", Convert.ToDecimal(totalcost)), String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discount_inrs"].ToString())) + discount_string, String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString())), dt_pt_sub.Rows[k]["note"].ToString() + " " + dt_pt_sub.Rows[k]["tooth"].ToString() + "(Finished by- Dr." + dt_pt_sub.Rows[k]["doctor_name"].ToString() + ")", dt_pt_sub.Rows[k]["status"].ToString());

                                }
                                else
                                {
                                    dgv_treatment_paln.Rows.Add(dt_pt_sub.Rows[k]["id"].ToString(), "", dt_pt_sub.Rows[k]["procedure_name"].ToString(), "", "", "", dt_pt_sub.Rows[k]["note"].ToString() + " " + dt_pt_sub.Rows[k]["tooth"].ToString() + "(Finished by- Dr." + dt_pt_sub.Rows[k]["doctor_name"].ToString() + ")", dt_pt_sub.Rows[k]["status"].ToString());

                                }
                            }
                            else
                            {
                                dgv_treatment_paln.Rows.Add(dt_pt_sub.Rows[k]["id"].ToString(), "", dt_pt_sub.Rows[k]["procedure_name"].ToString(), String.Format("{0:C2}", Convert.ToDecimal(totalcost)), String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["discount_inrs"].ToString())) + discount_string, String.Format("{0:C2}", Convert.ToDecimal(dt_pt_sub.Rows[k]["total"].ToString())), dt_pt_sub.Rows[k]["note"].ToString() + " " + dt_pt_sub.Rows[k]["tooth"].ToString() + "(Finished by- Dr." + dt_pt_sub.Rows[k]["doctor_name"].ToString() + ")", dt_pt_sub.Rows[k]["status"].ToString());
                            }
                            dgv_treatment_paln.Rows[i].Height = 30;
                            if (dt_pt_sub.Rows[k]["status"].ToString() == "0")
                            {
                                dgv_treatment_paln.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.billed;
                            }
                            else
                            {
                                dgv_treatment_paln.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.Borderblank;
                            }
                            totalEst = totalEst + Convert.ToDouble(dt_pt_sub.Rows[k]["total"].ToString());
                            dgv_treatment_paln.Rows[i].Cells[8].Value = PappyjoeMVC.Properties.Resources.Bill;

                            dgv_treatment_paln.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dgv_treatment_paln.Rows[i].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dgv_treatment_paln.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }
                        i = i + 1;
                        dgv_treatment_paln.Rows.Add("", "", "", "", "", "", "", "0");
                        dgv_treatment_paln.Rows[i].Cells[1].Value = PappyjoeMVC.Properties.Resources.blank;
                        dgv_treatment_paln.Rows[i].Cells[8].Value = PappyjoeMVC.Properties.Resources.blank;
                        i = i + 1;
                    }
                }
                if (dgv_treatment_paln.Rows.Count <= 0)
                {
                    int x = (panel7.Size.Width - lab_Msg.Size.Width) / 2;
                    lab_Msg.Location = new Point(x, lab_Msg.Location.Y);
                    lab_Msg.Show();
                }
                else
                {
                    lab_Msg.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_treatment_paln_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_treatment_paln.Rows.Count > 0 && (dgv_treatment_paln.Rows[e.RowIndex].Cells[7].Value.ToString() == "1" || dgv_treatment_paln.Rows[e.RowIndex].Cells[7].Value.ToString() == "2"))
                {
                    if (e.ColumnIndex == 1)
                    {
                        if (dgv_treatment_paln.Rows[e.RowIndex].Cells[7].Value.ToString() == "1")
                        {
                            btnEnabled = btnEnabled + 1;
                            dgv_treatment_paln.Rows[e.RowIndex].Cells[7].Value = "2";
                            dgv_treatment_paln.Rows[e.RowIndex].Cells[1].Value = PappyjoeMVC.Properties.Resources.Bordertick;
                        }
                        else if (dgv_treatment_paln.Rows[e.RowIndex].Cells[7].Value.ToString() == "2")
                        {
                            btnEnabled = btnEnabled - 1;
                            dgv_treatment_paln.Rows[e.RowIndex].Cells[7].Value = "1";
                            dgv_treatment_paln.Rows[e.RowIndex].Cells[1].Value = PappyjoeMVC.Properties.Resources.Borderblank;
                        }
                        if (btnEnabled > 0)
                        {
                            btn_InvoiceSelectTemnt.Enabled = true;
                        }
                        else
                        {
                            btn_InvoiceSelectTemnt.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_treatment_paln_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int currentMouseOverRow = dgv_treatment_paln.HitTest(e.X, e.Y).RowIndex;
                int currentMouseOverColumn = dgv_treatment_paln.HitTest(e.X, e.Y).ColumnIndex;
                if (currentMouseOverRow >= 0)
                {
                    if (currentMouseOverColumn == 8)
                    {
                        if (dgv_treatment_paln.Rows[currentMouseOverRow].Cells[0].Value.ToString() != "")
                        {
                            if (dgv_treatment_paln.Rows[currentMouseOverRow].Cells[7].Value.ToString() == "0")
                            {
                                treatment_complete_id = dgv_treatment_paln.Rows[currentMouseOverRow].Cells[0].Value.ToString();
                                deleteToolStripMenuItem1.Enabled = false;
                                contextMenuStrip1.Show(dgv_treatment_paln, new System.Drawing.Point(881 - 120, e.Y));
                            }
                            else if (dgv_treatment_paln.Rows[currentMouseOverRow].Cells[7].Value.ToString() == "1" || dgv_treatment_paln.Rows[currentMouseOverRow].Cells[7].Value.ToString() == "2")
                            {
                                treatment_complete_id = dgv_treatment_paln.Rows[currentMouseOverRow].Cells[0].Value.ToString();
                                contextMenuStrip1.Show(dgv_treatment_paln, new System.Drawing.Point(881 - 120, e.Y));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (doctor_id != "1")
                {
                    string id;
                    id = privi_mdl.delete_privillage(doctor_id);
                    if (int.Parse(id) > 0)
                    {
                        dlt_privilage();
                    }
                    else
                    {
                        MessageBox.Show("There is No Privilege to Delete Finished Procedure", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    }
                }
                else
                {
                    dlt_privilage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        public void dlt_privilage()
        {
            if (treatment_complete_id != "0")
            {
                DialogResult res = MessageBox.Show("Are you sure you want to delete..?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    System.Data.DataTable dt_pt_complete = this.cntrl.get_plamManin_id(treatment_complete_id);
                    string completed_main_id = "0";
                    string plan_main_id = "0";
                    if (dt_pt_complete.Rows.Count > 0)
                    {
                        completed_main_id = dt_pt_complete.Rows[0]["plan_main_id"].ToString();
                        plan_main_id = dt_pt_complete.Rows[0]["completed_id"].ToString();
                    }
                    this.cntrl.delete(treatment_complete_id);
                    this.cntrl.update_status1(plan_main_id);
                    System.Data.DataTable dt_pt_complete2 = this.cntrl.chek_planmain_id(completed_main_id);
                    if (dt_pt_complete2.Rows.Count == 0)
                    {
                        this.cntrl.delete_completedid(completed_main_id);
                    }
                    //dgv_treatment_paln.RowCount = 0;
                    dgv_treatment_paln.Rows.RemoveAt(dgv_treatment_paln.CurrentRow.Index);
                    dgv_treatment_paln.ColumnHeadersVisible = false;
                    dgv_treatment_paln.RowHeadersVisible = false;
                    dgv_treatment_paln.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv_treatment_paln.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv_treatment_paln.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.cntrl.get_completed_id_date(patient_id);
                    string dt = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    DateTime Timeonly = DateTime.Now;
                    this.cntrl.save_log(doctor_id, "Finished Procedure", " Delete Finished Procedure", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Delete", treatment_complete_id);
                }
            }
        }

        private void btn_InvoiceSelectTemnt_Click(object sender, EventArgs e)
        {
            try
            {
                int rec_count = 0;
                string a_plan_id = "";
                if (doctor_id != "1")
                {
                    string id = this.privi_mdl.invoice_add(doctor_id);
                    //if (id != "")
                    //{
                    if (int.Parse(id) > 0)
                    {
                        for (int i = 0; i < dgv_treatment_paln.Rows.Count; i++)
                        {
                            if (dgv_treatment_paln.Rows[i].Cells[7].Value.ToString() == "2")
                            {
                                rec_count = rec_count + 1;
                                a_plan_id = a_plan_id + "," + dgv_treatment_paln.Rows[i].Cells[0].Value.ToString();
                            }
                        }
                        if (rec_count != 0)
                        {
                            a_plan_id = a_plan_id.Substring(1, a_plan_id.Length - 1);
                            var form2 = new Add__invoice();
                            form2.patient_id = patient_id;
                            form2.doctor_id = doctor_id;
                            form2.complete_proce_id = a_plan_id;
                            openform(form2);
                        }
                        else
                        {
                            MessageBox.Show("Please select the Treatment..", "No Data To add !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("There is No Privilege to Add Invoice", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
                else
                {
                    for (int i = 0; i < dgv_treatment_paln.Rows.Count; i++)
                    {
                        if (dgv_treatment_paln.Rows[i].Cells[7].Value.ToString() == "2")
                        {
                            rec_count = rec_count + 1;
                            a_plan_id = a_plan_id + "," + dgv_treatment_paln.Rows[i].Cells[0].Value.ToString();
                        }
                    }
                    if (rec_count != 0)
                    {
                        a_plan_id = a_plan_id.Substring(1, a_plan_id.Length - 1);
                        var form2 = new Add__invoice();
                        form2.patient_id = patient_id;
                        form2.doctor_id = doctor_id;
                        form2.complete_proce_id = a_plan_id;
                        openform(form2);
                    }
                    else
                    {
                        MessageBox.Show("Please select the Treatment..", "No Data To add !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void labelprofile_Click(object sender, EventArgs e)
        {
            var form2 = new PappyjoeMVC.View.Patient_Profile_Details();
            form2.doctor_id = doctor_id;
            form2.patient_id = patient_id;
            openform(form2);
        }

        private void labelallpatient_Click(object sender, EventArgs e)
        {
            var form2 = new Patients();
            form2.doctor_id = doctor_id;
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

        private void lab_Vitalsigns_Click(object sender, EventArgs e)
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
                    MessageBox.Show("There is No Privilege to Show Treatment plan", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

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

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {

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
    }
}
