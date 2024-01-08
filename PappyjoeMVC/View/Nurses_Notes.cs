using PappyjoeMVC.Controller;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using PappyjoeMVC.Model;
using System.Net.Mail;

namespace PappyjoeMVC.View
{

    public partial class Nurses_Notes : Form
    {
        Show_Appointment_controller Apptmt_cntrl = new Show_Appointment_controller();
        Vital_Signs_controller vital_cntrl = new Vital_Signs_controller();
        Treatment_controller treatmnt_cntrl = new Treatment_controller();
        Finished_Procedre_controller fnsdtrt_cntrl = new Finished_Procedre_controller();
        Prescription_Show_controller prescr_cntrl = new Prescription_Show_controller();
        LabWorks_controller lab_cntrl = new LabWorks_controller();
        Attachments_controller attach_cntrl = new Attachments_controller();
        Invoice_controller invo_cntrl = new Invoice_controller();
        Receipt_controller recpt_cntrl = new Receipt_controller();
        Nurses_Notes_controller cntrl = new Nurses_Notes_controller();
        Clinical_Findings_controller clinic_cntrl = new Clinical_Findings_controller();
        Connection db = new Connection();
        public string doctor_id = "0";
        public string patient_id = "0";
        public string staff_id = "";
        public string clinic_id = "0";
        
        public Nurses_Notes()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        
        {

            try
            {
                
                //var form2 = new Add_Nurses_Notes();
                //form2.doctor_id = doctor_id;
                //form2.patient_id = patient_id;
               // openform(form2);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void labelallpatient_Click(object sender, EventArgs e)
        {
            var form2 = new Patients();
            form2.doctor_id = doctor_id;
            openform(form2);
        }

        private void Nurses_Notes_Load(object sender, EventArgs e)
        {
            //string docnam = this.cntrl.Get_DoctorName(doctor_id);
            //dataGridView1.Rows.Clear();
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
            System.Data.DataTable dt_cf_main = this.clinic_cntrl.dt_cf_main(patient_id);
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
        public void reciept_count()
        {
            DataTable Payments = db.table("select receipt_no,amount_paid,invoice_no,procedure_name,mode_of_payment from tbl_payment where pt_id='" + patient_id + "'");
            if (Payments.Rows.Count > 0)
            {
                label1.Text = Payments.Rows.Count.ToString();
            }
        }
        public void nurse_count()
        {
            System.Data.DataTable dt_cf_main = this.cntrl.dt_cf_main(patient_id);
            if (dt_cf_main.Rows.Count > 0)
            {
                lb_Nurses_Notes.Text = dt_cf_main.Rows.Count.ToString();
            }
        }
        public int nurse_note_id = 0;
        public string staff = "",pro_id="",pro_name="";
        public void fillgrid()
        {
            dataGridView1.Show();
            dataGridView1.ColumnCount = 7;
            dataGridView1.RowCount = 0;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            string heading = "";
            System.Data.DataTable dt_cf_main = this.cntrl.dt_cf_main(patient_id);
            int i = 0;
            if(dt_cf_main.Rows.Count>0)
            {
                for (int j = 0; j < dt_cf_main.Rows.Count; j++)
                {
                    DataTable dt_staffname = this.cntrl.dt_staffname(dt_cf_main.Rows[j]["staff_name"].ToString());//staff_name
                   
                    dataGridView1.Rows.Add(dt_cf_main.Rows[j]["id"].ToString(), String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(dt_cf_main.Rows[j]["date"].ToString())), "", "TREATMENT :" + dt_cf_main.Rows[j]["pro_name"].ToString(), dt_cf_main.Rows[j]["proid"].ToString(), dt_cf_main.Rows[j]["pro_name"].ToString(), "");
                    dataGridView1.Rows[i].Cells[6].Value = PappyjoeMVC.Properties.Resources.Bill;
                    dataGridView1.Columns[4].Visible = false;
                    dataGridView1.Columns[5].Visible = false;
                    dataGridView1.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                    dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.DarkGreen;
                    dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.LightGray;
                    dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.LightGray;
                    dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.LightGray;
                    dataGridView1.Rows[i].Cells[5].Style.BackColor = Color.LightGray;
                    dataGridView1.Rows[i].Cells[3].Style.ForeColor = Color.DarkGreen;
                    //dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.LightGray;
                    System.Data.DataTable dt_cf_Complaints = this.cntrl.dt_cf_Complaints(dt_cf_main.Rows[j]["id"].ToString());
                    if (dt_cf_Complaints.Rows.Count > 0)
                    {
                        int partLength = 90;
                        string sentence = dt_cf_Complaints.Rows[0]["procedure_notes"].ToString();
                        string[] words = sentence.Split(' ');
                        var parts = new System.Collections.Generic.Dictionary<int, string>();
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
                        heading = "Procedure";
                        foreach (var item in parts)
                        {
                            i = i + 1;
                            dataGridView1.Rows.Add("0", heading, "", item.Value, "","","");
                            heading = "";
                            dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                            dataGridView1.Rows[i].Cells[6].Value = PappyjoeMVC.Properties.Resources.blank;
                        }
                        for (int k = 1; k < dt_cf_Complaints.Rows.Count; k++)
                        {
                            partLength = 90;
                            sentence = dt_cf_Complaints.Rows[k]["procedure_notes"].ToString();
                            words = sentence.Split(' ');
                            parts = new System.Collections.Generic.Dictionary<int, string>();
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
                                dataGridView1.Rows.Add("0", "", "", item.Value, "","","");
                                dataGridView1.Rows[i].Cells[6].Value = PappyjoeMVC.Properties.Resources.blank;
                                dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                            }
                        }
                    }
                    System.Data.DataTable dt_cf_remarks = this.cntrl.remarks(dt_cf_main.Rows[j]["id"].ToString());
                    if (dt_cf_remarks.Rows.Count > 0)
                    {
                        int partLength = 90;
                        string sentence = dt_cf_remarks.Rows[0]["Remarks"].ToString();
                        string[] words = sentence.Split(' ');
                        var parts = new System.Collections.Generic.Dictionary<int, string>();
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
                        heading = "Remarks";
                        foreach (var item in parts)
                        {
                            i = i + 1;
                            dataGridView1.Rows.Add("0", heading, "", item.Value, "","","");
                            heading = "";
                            dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                            dataGridView1.Rows[i].Cells[6].Value = PappyjoeMVC.Properties.Resources.blank;
                        }
                        for (int k = 1; k < dt_cf_remarks.Rows.Count; k++)
                        {
                            partLength = 90;
                            sentence = dt_cf_remarks.Rows[k]["Remarks"].ToString();
                            words = sentence.Split(' ');
                            parts = new System.Collections.Generic.Dictionary<int, string>();
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
                                dataGridView1.Rows.Add("0", "", "", item.Value, "","","");
                                dataGridView1.Rows[i].Cells[6].Value = PappyjoeMVC.Properties.Resources.blank;
                                dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                            }
                        }
                    }

                    i = i + 1;
                    dataGridView1.Rows.Add("0", "Note by " + dt_staffname.Rows[0]["doctor_name"].ToString(), "", "", "","","");
                    dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                    dataGridView1.Rows[i].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 7, FontStyle.Bold);
                    dataGridView1.Rows[i].Cells[6].Value = PappyjoeMVC.Properties.Resources.blank;
                    i = i + 1;
                    dataGridView1.Rows.Add("0", "", "", "", "","","");
                    dataGridView1.Rows[i].Cells[6].Value = PappyjoeMVC.Properties.Resources.blank;
                    i = i + 1;
                }
            }

            if (dataGridView1.Rows.Count <= 0)
            {
                //int x = (panel3.Size.Width - Lbl_NoRecordFound_Alert.Size.Width) / 2;
                Lbl_NoRecordFound_Alert.Location = new Point(92, 267);//   x, Lbl_NoRecordFound_Alert.Location.Y);
                Lbl_NoRecordFound_Alert.Show();
                dataGridView1.Hide();
            }
            else
            {
                Lbl_NoRecordFound_Alert.Hide();
                dataGridView1.Show();
            }
            //BtnAdd.Show();
            //dataGr/*i*/dView1.Show();


        }

        private void labelprofile_Click(object sender, EventArgs e)
        {
            Patient_Profile_Details frm = new Patient_Profile_Details();
            frm.doctor_id = doctor_id;
            frm.patient_id = patient_id;
            openform(frm);
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

        private void label26_Click(object sender, EventArgs e)
        {
            //var form2 = new Show_Appointment();
            //form2.doctor_id = doctor_id;
            //form2.patient_id = patient_id;
            //openform(form2);
        }

        private void label16_Click(object sender, EventArgs e)
        {
            //var form2 = new Vital_Signs();
            //form2.doctor_id = doctor_id;
            //form2.patient_id = patient_id;
            //openform(form2);
        }

        private void label25_Click(object sender, EventArgs e)
        {
            //var form2 = new Clinical_Findings();
            //form2.doctor_id = doctor_id;
            //form2.patient_id = patient_id;
            //openform(form2);
        }

        private void label24_Click(object sender, EventArgs e)
        {
            //var form2 = new Treatment_Plans();
            //form2.doctor_id = doctor_id;
            //form2.patient_id = patient_id;
            //openform(form2);
        }

        private void label23_Click(object sender, EventArgs e)
        {
            //var form2 = new Finished_Procedure();
            //form2.doctor_id = doctor_id;
            //form2.patient_id = patient_id;
            //openform(form2);
        }

        private void label22_Click(object sender, EventArgs e)
        {
            //var form2 = new Prescription_Show();
            //form2.doctor_id = doctor_id;
            //form2.patient_id = patient_id;
            //openform(form2);
        }

        private void label15_Click(object sender, EventArgs e)
        {
        //    var form2 = new LabWorks();
        //    form2.doctor_id = doctor_id;
        //    form2.patient_id = patient_id;
        //    openform(form2);
        }

        private void label20_Click(object sender, EventArgs e)
        {
        //    var form2 = new Attachments();
        //    form2.doctor_id = doctor_id;
        //    form2.patient_id = patient_id;
        //    openform(form2);
        }

        private void label18_Click(object sender, EventArgs e)
        {
            //var form2 = new Invoice();
            //form2.doctor_id = doctor_id;
            //form2.patient_id = patient_id;
            //openform(form2);
        }

        private void lb_Reciepts_cnt_Click(object sender, EventArgs e)
        {
            //Receipt myForm = new Receipt();
            //myForm.doctor_id = doctor_id;
            //myForm.patient_id = patient_id;
            //openform(myForm);
        }

        private void label17_Click(object sender, EventArgs e)
        {
            //var form2 = new Ledger();
            //form2.patient_id = patient_id;
            //form2.doctor_id = doctor_id;
            //openform(form2);
        }

        private void lbl_NursesNotes_Click(object sender, EventArgs e)
        {
            //var form2 = new Nurses_Notes();
            //form2.doctor_id = doctor_id;
            //form2.patient_id = patient_id;
            //openform(form2);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nurse_note_id > 0)
            {
                DialogResult res = MessageBox.Show("Are you sure you want to delete?", "Delete confirmation",
                             MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.No)
                {
                }
                else
                {
                    DataTable dt_main = this.cntrl.notes_main(nurse_note_id.ToString());
                    if(dt_main.Rows.Count>0)
                    {
                        string id = dt_main.Rows[0]["plan_id"].ToString();
                        this.cntrl.delete_main(nurse_note_id.ToString());
                        this.cntrl.delete_notes(nurse_note_id.ToString());
                        this.cntrl.delete_remarks(nurse_note_id.ToString());
                        this.cntrl.update_tonurse_status(patient_id,id);
                        MessageBox.Show("Data deleted successfully ","Data Deleted",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                   
                }
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            int currentMouseOverColumn = dataGridView1.HitTest(e.X, e.Y).ColumnIndex;
            if (currentMouseOverRow >= 0)
            {
                if (currentMouseOverColumn == 6)
                {
                    if (dataGridView1.Rows[currentMouseOverRow].Cells[0].Value.ToString() != "0")
                    {
                        nurse_note_id =Convert.ToInt32( dataGridView1.Rows[currentMouseOverRow].Cells[0].Value.ToString());
                        pro_id = dataGridView1.Rows[currentMouseOverRow].Cells[4].Value.ToString();
                        pro_name = dataGridView1.Rows[currentMouseOverRow].Cells[5].Value.ToString();
                        contextMenuStrip1.Show(dataGridView1, new System.Drawing.Point(930 - 120, e.Y));//(930 - 120, e.Y
                    }
                }

            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
           if(nurse_note_id>0)
            {
                var form2 = new Add_Nurses_Notes();
                form2.nurse_note_id = nurse_note_id;
                form2.doctor = doctor_id;
                form2.pro_id = pro_id;
                form2.procedurename = pro_name;
                form2.patient_id = patient_id;
                form2.ShowDialog();
                fillgrid();
            }
        }

        private void emailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string doctor_name = "", staff = "", doct="";
                DataTable dt_main = this.cntrl.notes_main(nurse_note_id.ToString());
                if(dt_main.Rows.Count>0)
                {
                    staff =  this.cntrl.Get_DoctorName(dt_main.Rows[0]["staff_name"].ToString());
                     doct = this.cntrl.Get_DoctorName(dt_main.Rows[0]["dr_id"].ToString());

                }

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
                StreamWriter sWrite = new StreamWriter(Apppath + "\\Nurse_Note.html");
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
                sWrite.WriteLine("<table align=center> ");
                sWrite.WriteLine("<col width=700px>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<th align='center'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=5>Nurses Note </font></th>");

                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine(" <tr height='40px'>");
                sWrite.WriteLine("    <td align='left' width='400px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>Consulted by : <b> " + doctor_name.ToString() + " </b></font></td>");
                sWrite.WriteLine("<td align='right' width='400px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Staff Name : <b> " + staff + " </b></font></td>");

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
                sWrite.WriteLine("<tr><th align='left' colspan=3><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3><hr></font></th></tr>");
                sWrite.WriteLine("</table>");
                
                //sWrite.WriteLine("<tr><th align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3><hr></font></th></tr>");
                System.Data.DataTable dt_clinical_Findings = this.cntrl.dt_cf_Complaints(nurse_note_id.ToString());
                int i = 0;
                sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                System.Data.DataTable dt_cf_Complaints = this.cntrl.dt_cf_Complaints(nurse_note_id.ToString());
                sWrite.WriteLine("<br>");
                sWrite.WriteLine("<tr>");
                //sWrite.WriteLine("<th align='left' width='230px' bgcolor='#0099FF' ><FONT COLOR=white FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;</font></th>");
                sWrite.WriteLine("<th align='left' width='230px' bgcolor='#00000' ><FONT COLOR=white FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;TREATMENT NAME : "+dt_main.Rows[0]["pro_name"].ToString() +"</font></th>");
                sWrite.WriteLine("</tr>");
                if (dt_cf_Complaints.Rows.Count > 0)
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<th align='left' width='230px' bgcolor='#0099FF' ><FONT COLOR=white FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;PROCEDURE</font></th>");
                    sWrite.WriteLine("</tr>");
                    for (i = 0; i < dt_cf_Complaints.Rows.Count; i++)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;" + " " + "" + dt_cf_Complaints.Rows[i]["procedure_notes"].ToString() + "</font></td>");
                        sWrite.WriteLine("</tr>");
                    }
                }
                System.Data.DataTable dt_cf_observe = this.cntrl.remarks(nurse_note_id.ToString());
                if (dt_cf_observe.Rows.Count > 0)
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<th align='left' width='230px' bgcolor='#0099FF' ><FONT COLOR=white FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;OBSERVATION</font></th>");
                    sWrite.WriteLine("</tr>");

                    for (i = 0; i < dt_cf_observe.Rows.Count; i++)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;" + " " + "" + dt_cf_observe.Rows[i]["Remarks"].ToString() + "</font></td>");
                        sWrite.WriteLine("</tr>");
                    }
                }
                //System.Data.DataTable dt_cf_investigation = this.cntrl.dt_cf_investigation(dt_clinical_Findings.Rows[0]["id"].ToString());
                //if (dt_cf_investigation.Rows.Count > 0)
                //{
                //    sWrite.WriteLine("<tr>");
                //    sWrite.WriteLine("<th align='left' width='230px' bgcolor='#0099FF' ><FONT COLOR=white FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;INVESTIGATION</font></th>");
                //    sWrite.WriteLine("</tr>");
                //    for (i = 0; i < dt_cf_investigation.Rows.Count; i++)
                //    {
                //        sWrite.WriteLine("<tr>");
                //        sWrite.WriteLine("<td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;" + dt_cf_investigation.Rows[i]["investigation_id"].ToString() + "</font></td>");
                //        sWrite.WriteLine("</tr>");
                //    }
                //}
                //System.Data.DataTable dt_cf_diagnosis = this.cntrl.dt_cf_diagnosis(dt_clinical_Findings.Rows[0]["id"].ToString());
                //if (dt_cf_diagnosis.Rows.Count > 0)
                //{
                //    sWrite.WriteLine("<tr>");
                //    sWrite.WriteLine("<th align='left' width='230px' bgcolor='#0099FF' ><FONT COLOR=white FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;DIAGNOSIS</font></th>");
                //    sWrite.WriteLine("</tr>");
                //    for (i = 0; i < dt_cf_diagnosis.Rows.Count; i++)
                //    {
                //        sWrite.WriteLine("<tr>");
                //        sWrite.WriteLine("<td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;" + dt_cf_diagnosis.Rows[i]["diagnosis_id"].ToString() + "</font></td>");
                //        sWrite.WriteLine("</tr>");
                //    }
                //}
                //System.Data.DataTable dt_cf_note = this.cntrl.dt_cf_note(dt_clinical_Findings.Rows[0]["id"].ToString());
                //if (dt_cf_note.Rows.Count > 0)
                //{
                //    sWrite.WriteLine("<tr>");
                //    sWrite.WriteLine("<th align='left' width='230px' bgcolor='#0099FF' ><FONT COLOR=white FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;NOTE</font></th>");
                //    sWrite.WriteLine("</tr>");
                //    for (i = 0; i < dt_cf_note.Rows.Count; i++)
                //    {
                //        sWrite.WriteLine("<tr>");
                //        sWrite.WriteLine("<td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;" + dt_cf_note.Rows[i]["note_name"].ToString() + "</font></td>");
                //        sWrite.WriteLine("</tr>");
                //    }

                //}
                //System.Data.DataTable dt_cf_allergy = this.cntrl.dt_cf_allergy(dt_clinical_Findings.Rows[0]["id"].ToString());
                //if (dt_cf_allergy.Rows.Count > 0)
                //{
                //    sWrite.WriteLine("<tr>");
                //    sWrite.WriteLine("<th align='left' width='230px' bgcolor='#0099FF' ><FONT COLOR=white FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;Allergies</font></th>");
                //    sWrite.WriteLine("</tr>");
                //    for (i = 0; i < dt_cf_allergy.Rows.Count; i++)
                //    {
                //        sWrite.WriteLine("<tr>");
                //        sWrite.WriteLine("<td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;" + dt_cf_allergy.Rows[i]["allergy_name"].ToString() + "</font></td>");
                //        sWrite.WriteLine("</tr>");
                //    }
                //    sWrite.WriteLine(" </table>");
                //    sWrite.WriteLine(" </td>");
                //    sWrite.WriteLine("</tr>");
                //    sWrite.WriteLine("<tr >");
                //    sWrite.WriteLine("    <td align='center' height='20'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3></font></td>");
                //    sWrite.WriteLine("</tr>");
                //}
                //sWrite.WriteLine("<tr >");
                //sWrite.WriteLine("    <td align='right' height='20' width='700px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1>powered by</font></td>");
                //sWrite.WriteLine("</tr>");
                //sWrite.WriteLine("<tr >");
                //sWrite.WriteLine("    <td align='right' height='81' width='700px'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=1><img src='http://pappyjoe.com/assets/images/pappyjoe-logo.PNG' alt='pappyjoe official logo'></font></td>");
                //sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");
                //sWrite.WriteLine("</td>");
                //sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("</body>");
                sWrite.WriteLine("</html>");
                sWrite.Close();
                System.Diagnostics.Process.Start(Apppath + "\\Nurse_Note.html");
                // mail senting...
                //string email = "", emailName = "", emailPass = "";
                //System.Data.DataTable sr = this.cntrl.getpatemail(patient_id);
                //if (sr.Rows.Count > 0)
                //{
                //    email = sr.Rows[0]["email_address"].ToString();
                //    if (email != "")
                //    {
                //        System.Data.DataTable sms = this.cntrl.send_email();
                //        if (sms.Rows.Count > 0)
                //        {
                //            emailName = sms.Rows[0]["emailName"].ToString();
                //            emailPass = sms.Rows[0]["emailPass"].ToString();

                //            StreamReader reader = new StreamReader(Apppath + "\\Nurse_Note.html");
                //            string readFile = reader.ReadToEnd();
                //            string StrContent = "";
                //            StrContent = readFile;
                //            MailMessage message = new MailMessage();
                //            message.From = new MailAddress(email);
                //            message.To.Add(email);
                //            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                //            message.Subject = "Nurses Note";
                //            message.Body = StrContent.ToString();
                //            message.IsBodyHtml = true;
                //            smtp.Port = 587;
                //            smtp.Host = "smtp.gmail.com";
                //            smtp.EnableSsl = true;
                //            smtp.UseDefaultCredentials = false;
                //            smtp.Credentials = new System.Net.NetworkCredential(emailName, emailPass);
                //            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                //            smtp.Send(message);
                //            MessageBox.Show("Email is Sent To : " + email, "Success ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //            reader.Close();

                //        }
                //        else
                //        {
                //            MessageBox.Show("Please Activate Email Configuration", "Activate Email", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        }
                //    }
                //    else
                //    {
                //        MessageBox.Show("Please Add EmailId for Selected patient", "Add Email Id", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                StreamWriter sWrite = new StreamWriter(Apppath + "\\Nurse_Note.html");
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

                System.Data.DataTable dt_clinical_Findings = this.cntrl.dt_cf_Complaints(nurse_note_id.ToString());
                int i = 0;
                sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                System.Data.DataTable dt_cf_Complaints = this.cntrl.dt_cf_Complaints(nurse_note_id.ToString());
                if (dt_cf_Complaints.Rows.Count > 0)
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<th align='left' width='230px' bgcolor='#0099FF' ><FONT COLOR=white FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;PROCEDURE</font></th>");
                    sWrite.WriteLine("</tr>");
                    for (i = 0; i < dt_cf_Complaints.Rows.Count; i++)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;" + " " + "" + dt_cf_Complaints.Rows[i]["procedure_notes"].ToString() + "</font></td>");
                        sWrite.WriteLine("</tr>");
                    }
                }
                System.Data.DataTable dt_cf_observe = this.cntrl.remarks(nurse_note_id.ToString());
                if (dt_cf_observe.Rows.Count > 0)
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<th align='left' width='230px' bgcolor='#0099FF' ><FONT COLOR=white FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;OBSERVATION</font></th>");
                    sWrite.WriteLine("</tr>");

                    for (i = 0; i < dt_cf_observe.Rows.Count; i++)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;" + " " + "" + dt_cf_observe.Rows[i]["Remarks"].ToString() + "</font></td>");
                        sWrite.WriteLine("</tr>");
                    }
                }
                //System.Data.DataTable dt_cf_investigation = this.cntrl.dt_cf_investigation(dt_clinical_Findings.Rows[0]["id"].ToString());
                //if (dt_cf_investigation.Rows.Count > 0)
                //{
                //    sWrite.WriteLine("<tr>");
                //    sWrite.WriteLine("<th align='left' width='230px' bgcolor='#0099FF' ><FONT COLOR=white FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;INVESTIGATION</font></th>");
                //    sWrite.WriteLine("</tr>");
                //    for (i = 0; i < dt_cf_investigation.Rows.Count; i++)
                //    {
                //        sWrite.WriteLine("<tr>");
                //        sWrite.WriteLine("<td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;" + dt_cf_investigation.Rows[i]["investigation_id"].ToString() + "</font></td>");
                //        sWrite.WriteLine("</tr>");
                //    }
                //}
                //System.Data.DataTable dt_cf_diagnosis = this.cntrl.dt_cf_diagnosis(dt_clinical_Findings.Rows[0]["id"].ToString());
                //if (dt_cf_diagnosis.Rows.Count > 0)
                //{
                //    sWrite.WriteLine("<tr>");
                //    sWrite.WriteLine("<th align='left' width='230px' bgcolor='#0099FF' ><FONT COLOR=white FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;DIAGNOSIS</font></th>");
                //    sWrite.WriteLine("</tr>");
                //    for (i = 0; i < dt_cf_diagnosis.Rows.Count; i++)
                //    {
                //        sWrite.WriteLine("<tr>");
                //        sWrite.WriteLine("<td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;" + dt_cf_diagnosis.Rows[i]["diagnosis_id"].ToString() + "</font></td>");
                //        sWrite.WriteLine("</tr>");
                //    }
                //}
                //System.Data.DataTable dt_cf_note = this.cntrl.dt_cf_note(dt_clinical_Findings.Rows[0]["id"].ToString());
                //if (dt_cf_note.Rows.Count > 0)
                //{
                //    sWrite.WriteLine("<tr>");
                //    sWrite.WriteLine("<th align='left' width='230px' bgcolor='#0099FF' ><FONT COLOR=white FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;NOTE</font></th>");
                //    sWrite.WriteLine("</tr>");
                //    for (i = 0; i < dt_cf_note.Rows.Count; i++)
                //    {
                //        sWrite.WriteLine("<tr>");
                //        sWrite.WriteLine("<td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;" + dt_cf_note.Rows[i]["note_name"].ToString() + "</font></td>");
                //        sWrite.WriteLine("</tr>");
                //    }

                //}
                //System.Data.DataTable dt_cf_allergy = this.cntrl.dt_cf_allergy(dt_clinical_Findings.Rows[0]["id"].ToString());
                //if (dt_cf_allergy.Rows.Count > 0)
                //{
                //    sWrite.WriteLine("<tr>");
                //    sWrite.WriteLine("<th align='left' width='230px' bgcolor='#0099FF' ><FONT COLOR=white FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;Allergies</font></th>");
                //    sWrite.WriteLine("</tr>");
                //    for (i = 0; i < dt_cf_allergy.Rows.Count; i++)
                //    {
                //        sWrite.WriteLine("<tr>");
                //        sWrite.WriteLine("<td align='left' width='230px' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;" + dt_cf_allergy.Rows[i]["allergy_name"].ToString() + "</font></td>");
                //        sWrite.WriteLine("</tr>");
                //    }
                //    sWrite.WriteLine(" </table>");
                //    sWrite.WriteLine(" </td>");
                //    sWrite.WriteLine("</tr>");
                //    sWrite.WriteLine("<tr >");
                //    sWrite.WriteLine("    <td align='center' height='20'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3></font></td>");
                //    sWrite.WriteLine("</tr>");
                //}
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

                            StreamReader reader = new StreamReader(Apppath + "\\Nurse_Note.html");
                            string readFile = reader.ReadToEnd();
                            string StrContent = "";
                            StrContent = readFile;
                            MailMessage message = new MailMessage();
                            message.From = new MailAddress(email);
                            message.To.Add(email);
                            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                            message.Subject = "Nurses Note";
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
    }

}
