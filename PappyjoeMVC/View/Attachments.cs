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
    public partial class Attachments : Form
    {
        public int attach_id;
        public bool APTDelete = false;
        public string doctor_id = "", privid = "", dctr = "", File_Type = "", patient_id = "";
        Attachments_controller ctrlr = new Attachments_controller();
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
        user_privillage_model privi_mdl = new user_privillage_model();
        Connection db = new Connection(); GlobalVariables gv = new GlobalVariables();
        public Attachments()
        {
            InitializeComponent();
        }
        public void privillage_A(string id)
        {
            if (doctor_id != "1")
            {
                string privid;
                privid = id;
                if (int.Parse(privid) > 0)
                {
                    button1.Enabled = false;
                }
                else
                {
                    button1.Enabled = true;
                }
            }
            else
            {
                string previd = this.ctrlr.getprevid(doctor_id);
                getprevid(previd);
            }
        }
        public void getprevid(string id)
        {
            privid = id;
            if (int.Parse(privid) > 0)
            {
                APTDelete = false;
            }
            else
            {
                APTDelete = true;
            }
        }
        public void GetCategory(DataTable dtb)
        {
            try
            {
                if (dtb.Rows.Count > 0)
                {
                    Dgv_Category.Rows.Add();
                    Dgv_Category.Rows[0].Cells["colid"].Value = 0;
                    Dgv_Category.Rows[0].Cells["ColCategory"].Value = "All Category";
                    Dgv_Category.Rows[0].Cells["ColEdit"].Value = PappyjoeMVC.Properties.Resources.editicon;
                    Dgv_Category.Rows[0].Cells["ColDelete"].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                    for (int i = 1; i < dtb.Rows.Count + 1; i++)
                    {
                        Dgv_Category.Rows.Add();
                        Dgv_Category.Rows[i].Cells["colid"].Value = dtb.Rows[i - 1]["id"].ToString();
                        Dgv_Category.Rows[i].Cells["ColCategory"].Value = dtb.Rows[i - 1]["CategoryName"].ToString();
                        Dgv_Category.Rows[i].Cells["ColEdit"].Value = PappyjoeMVC.Properties.Resources.editicon;
                        Dgv_Category.Rows[i].Cells["ColDelete"].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        public void GetPatientDetails(DataTable dt)
        {
            if (patient_id != "0")
            {
                if (dt.Rows.Count > 0)
                {
                    linkLabel_id.Text = dt.Rows[0]["pt_id"].ToString();
                    linkLabel_Name.Text = dt.Rows[0]["pt_name"].ToString();
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                string id = this.ctrlr.privillage_A(doctor_id);
                if (id != "")
                {
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
            }
            else
            {
                var form2 = new Add_Attachments();
                form2.doctor_id = doctor_id;
                form2.patient_id = patient_id;
                openform(form2);
            }
        }
        public void getattachment(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                FillAttachmentGrid(dt);
            }
            else
            {
                if (Dgv_Attachment.Rows.Count <= 0)
                {
                    int x = (panel7.Size.Width - Lab_Msg.Size.Width) / 2;
                    Lab_Msg.Location = new Point(x, Lab_Msg.Location.Y);
                    Lab_Msg.Show();
                    //Lab_Msg.Location = new System.Drawing.Point(134, 177);
                }
                else
                {
                    Lab_Msg.Hide();
                    //Lab_Msg.Location = new System.Drawing.Point(134, 177);
                }
            }
        }
        public void getattachment2(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                Lab_Msg.Visible = false;
                FillAttachmentGrid(dt);
            }
            else
            {
                int x = (panel7.Size.Width - Lab_Msg.Size.Width) / 2;
                Lab_Msg.Location = new Point(x, Lab_Msg.Location.Y);
                Lab_Msg.Visible = true;
            }
        }
        public void FillAttachmentGrid(DataTable attach)
        {
            if (attach.Rows.Count > 0)
            {
                string doctor = "";
                for (int i = 0; i < attach.Rows.Count; i++)
                {
                    string paths = this.ctrlr.getserver();
                    string path = attach.Rows[i]["path"].ToString();
                    if (decimal.Parse(attach.Rows[i]["dr_id"].ToString()) > 0)
                    {
                        this.ctrlr.Get_DoctorName(attach.Rows[i]["dr_id"].ToString());
                        doctor = dctr;
                    }
                    string ext = Path.GetExtension(attach.Rows[i]["Path"].ToString());
                    //                     0                          1                                                   2                                             3                       4                            5                    6  
                    Dgv_Attachment.Rows.Add("", "Date : " + String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(attach.Rows[i]["date"].ToString())), "Category:" + attach.Rows[i]["CategoryName"].ToString(), "Added By : Dr." + doctor, attach.Rows[i]["photo_name"].ToString(), attach.Rows[i]["id"].ToString(), ext,PappyjoeMVC.Properties.Resources.mail_icon);
                    Dgv_Attachment.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    Dgv_Attachment.Columns[1].DefaultCellStyle.Font = new System.Drawing.Font("Seigo", 8, FontStyle.Regular);
                    Dgv_Attachment.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    Dgv_Attachment.Columns[3].DefaultCellStyle.Font = new System.Drawing.Font("Seigo", 9, FontStyle.Bold);
                    Dgv_Attachment.Columns[3].DefaultCellStyle.ForeColor = Color.Green;
                    Dgv_Attachment.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    Dgv_Attachment.Columns[2].DefaultCellStyle.Font = new System.Drawing.Font("Seigo", 7, FontStyle.Bold);
                    Dgv_Attachment.Columns[5].Visible = false;
                    Dgv_Attachment.Columns[0].Width = 50;
                    Dgv_Attachment.Columns[1].Width = 210;
                    Dgv_Attachment.Columns[2].Width = 210;
                    Dgv_Attachment.Columns[3].Width = 150;
                    Dgv_Attachment.Columns[4].Width = 150;
                    Dgv_Attachment.Columns[5].Width = 10;
                    Dgv_Attachment.Columns[7].Width = 50;
                    Dgv_Attachment.Rows[i].Cells[0].Value = PappyjoeMVC.Properties.Resources.erase_icon;
                    Dgv_Attachment.Rows[i].Height = 85;
                    if (File.Exists(paths + path) == true)
                    {
                        if (ext.ToLower() == ".jpeg" || ext.ToLower() == ".jpg" || ext.ToLower() == ".gif" || ext.ToLower() == ".png")
                        {
                            try
                            {
                                using (FileStream stream = new FileStream(paths + path, FileMode.Open, FileAccess.Read))
                                {
                                    Image image = Image.FromStream(stream);
                                    stream.Dispose();
                                    Dgv_Attachment.Rows[i].Cells[6].Value = image;
                                }
                            }
                            catch (Exception ex)
                            { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                        else if (ext.ToLower() == ".docx" || ext.ToLower() == ".doc")
                        {
                            Dgv_Attachment.Rows[i].Cells[6].Value = PappyjoeMVC.Properties.Resources.word_doc_icon;
                        }
                        else if (ext.ToLower() == ".xls" || ext.ToLower() == ".xlsx")
                        {
                            Dgv_Attachment.Rows[i].Cells[6].Value = PappyjoeMVC.Properties.Resources.excel_doc_icon;
                        }
                        else if (ext.ToLower() == ".pdf")
                        {
                            Dgv_Attachment.Rows[i].Cells[6].Value = PappyjoeMVC.Properties.Resources.pdf;
                        }
                    }
                    else
                    {
                        Dgv_Attachment.Rows[i].Cells[6].Value = PappyjoeMVC.Properties.Resources.no_image_icon_6;
                    }
                }
            }
            else { }
        }

        private void btn_CategoryAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_Category.Text != "")
                {
                    Lab_Msg1.Visible = false;
                    if (btn_CategoryAdd.Text == "Save")
                    {
                        int i = this.ctrlr.inscatgry(txt_Category.Text);
                        if (i > 0)
                        {
                            setcontrolls_aftersave();
                        }
                        else
                        {
                            MessageBox.Show("Insertion failed !..", "Failed ! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        btn_CategoryCancel.Visible = true;
                        int i = this.ctrlr.update(txt_Category.Text, Dgv_Category.CurrentRow.Cells["colid"].Value.ToString());
                        if (i > 0)
                        {
                            setcontrolls_aftersave();
                            btn_CategoryAdd.Text = "Save";
                        }
                        else
                        {
                            MessageBox.Show("Updation failed !..", "Failed ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    int x = (panel7.Size.Width - Lab_Msg.Size.Width) / 2;
                    Lab_Msg.Location = new Point(x, Lab_Msg.Location.Y);
                    Lab_Msg1.Visible = true;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void btn_AddCategory_Click(object sender, EventArgs e)
        {
            txt_Category.Visible = true;
            lab_Category.Visible = true;
            btn_CategoryAdd.Visible = true;
            Btn_CategoryClose.Visible = true;
            Dgv_Category.Location = new Point(0, 100);
        }
        private void Btn_CategoryClose_Click(object sender, EventArgs e)
        {
            controll_visibility();
        }
        private void btn_CategoryCancel_Click(object sender, EventArgs e)
        {
            btn_CategoryAdd.Text = "Save";
            txt_Category.Text = "";
            Lab_Msg1.Visible = false;
            btn_CategoryCancel.Visible = false;
            btn_CategoryAdd.Location = new Point(124, 70);
            btn_CategoryAdd.Visible = true;
        }
        private void Dgv_Category_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string cid;
                if (e.RowIndex > -1)
                {
                    Lab_Msg.Visible = false;
                    if (Dgv_Category.CurrentCell.OwningColumn.Name == "ColEdit")//&& 
                    {
                        cid = Dgv_Category.CurrentRow.Cells["colid"].Value.ToString();
                        if (cid != "")
                        {
                            if (Dgv_Category.CurrentRow.Cells["ColCategory"].Value.ToString() != "General" && Dgv_Category.CurrentRow.Cells["ColCategory"].Value.ToString() != "All Category")
                            {
                                txt_Category.Text = Dgv_Category.CurrentRow.Cells["ColCategory"].Value.ToString();
                                btn_CategoryAdd.Text = "Update";
                                btn_CategoryCancel.Visible = true;
                                btn_CategoryCancel.Location = new Point(138, 70);
                                btn_CategoryAdd.Location = new Point(74, 70);
                                Dgv_Category.Location = new Point(0, 108);
                                txt_Category.Visible = true;
                                lab_Category.Visible = true;
                                btn_CategoryAdd.Visible = true;
                                Btn_CategoryClose.Visible = true;
                            }
                            else
                            {
                                MessageBox.Show("Can't update this category !...", "Can't update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else if (Dgv_Category.CurrentCell.OwningColumn.Name == "ColDelete")
                    {
                        cid = Dgv_Category.CurrentRow.Cells["colid"].Value.ToString();
                        string name = Dgv_Category.CurrentRow.Cells["ColCategory"].Value.ToString();
                        int index = Dgv_Category.CurrentRow.Index;
                        DialogResult res = MessageBox.Show("Are you sure you want to delete?", "Delete confirmation",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (res == DialogResult.No)
                        {
                        }
                        else
                        {
                            if (name != "General" && name != "All Category")
                            {
                                this.ctrlr.delete(cid);
                                Dgv_Category.Rows.RemoveAt(index);
                                Dgv_Category.Rows.Clear();
                                DataTable dt = this.ctrlr.GetCategory();
                                GetCategory(dt);
                            }
                            else
                            {
                                MessageBox.Show("Can't delete this category  !...", "Can't delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else if (Dgv_Category.CurrentCell.OwningColumn.Name == "ColCategory")
                    {
                        string name = Dgv_Category.CurrentRow.Cells["ColCategory"].Value.ToString();
                        {
                            if (name == "All Category")
                            {
                                Dgv_Attachment.Rows.Clear();
                                DataTable dt = this.ctrlr.getattachment(patient_id);
                                getattachment(dt);
                            }
                            else
                            {
                                Dgv_Attachment.Rows.Clear();
                                DataTable dt = this.ctrlr.getattachment2(patient_id, name);
                                getattachment2(dt);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private void Attachments_Load(object sender, EventArgs e)
        {
            if (doctor_id != "1")
            {
                //string EMRFadd = privi_mdl.add_attachments(doctor_id);// db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRF' and Permission='A'");
                //if (int.Parse(EMRFadd) > 0)
                //{
                //    pb_attchmntAdd.Enabled = true;
                //    button1.Enabled = true;
                //}
                //else
                //{
                //    pb_attchmntAdd.Enabled = false;
                //    button1.Enabled = false;
                //}
                dctr = this.ctrlr.Get_DoctorName(doctor_id);
                txt_Category.Visible = false;
                lab_Category.Visible = false;
                btn_CategoryAdd.Visible = false;
                Btn_CategoryClose.Visible = false;
                btn_CategoryAdd.Location = new Point(138, 70);
                Dgv_Category.Location = new Point(0, 45);
                DataTable catgry = this.ctrlr.GetCategory();
                GetCategory(catgry);
                DataTable patnt = this.ctrlr.GetPatientDetails(patient_id);
                GetPatientDetails(patnt);
                string pay = this.ctrlr.GetPayment(patient_id);
                if (pay != "")
                {
                    label8.Text = pay + " due";
                }
                DataTable attach = this.ctrlr.getattachment(patient_id);
                getattachment(attach);
                appointment_count();
                vitals_count();
                clinical_count();
                treatment_count();
                fnsdtrt_count(); nurse_count();
                prescr_count();
                lab_count();
                attach_count();
                invoice_count();
                reciept_count();
            } 
            else
            {
                //string id = this.ctrlr.privillage_A(doctor_id);
                //privillage_A(id);
                dctr = this.ctrlr.Get_DoctorName(doctor_id);
                txt_Category.Visible = false;
                lab_Category.Visible = false;
                btn_CategoryAdd.Visible = false;
                Btn_CategoryClose.Visible = false;
                btn_CategoryAdd.Location = new Point(138, 70);
                Dgv_Category.Location = new Point(0, 45);
                DataTable catgry = this.ctrlr.GetCategory();
                GetCategory(catgry);
                DataTable patnt = this.ctrlr.GetPatientDetails(patient_id);
                GetPatientDetails(patnt);
                string pay = this.ctrlr.GetPayment(patient_id);
                if (pay != "")
                {
                    label8.Text = pay + " due";
                }
                DataTable attach = this.ctrlr.getattachment(patient_id);
                getattachment(attach);
                appointment_count();
                vitals_count();
                clinical_count();
                treatment_count();
                fnsdtrt_count();
                nurse_count();
                prescr_count();
                lab_count();
                attach_count();
                invoice_count();
                reciept_count();
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
        public void setcontrolls_aftersave()
        {
            Dgv_Category.Rows.Clear();
            DataTable catgry = this.ctrlr.GetCategory();
            GetCategory(catgry);
            txt_Category.Text = "";
            controll_visibility();
            Dgv_Category.Location = new Point(0, 45);
        }
        public void controll_visibility()
        {
            Lab_Msg1.Visible = false;
            btn_CategoryCancel.Visible = false;
            txt_Category.Visible = false;
            lab_Category.Visible = false;
            btn_CategoryAdd.Visible = false;
            Dgv_Category.Location = new Point(0, 45);
            Btn_CategoryClose.Visible = false;
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
            var form2 = new Nurses_Notes();
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

        private void Dgv_Attachment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ext1 = "";
            attach_id = int.Parse(Dgv_Attachment.Rows[e.RowIndex].Cells[5].Value.ToString());
            ext1 = Dgv_Attachment.Rows[e.RowIndex].Cells[4].Value.ToString();
            File_Type = Path.GetExtension(ext1);
            if (e.ColumnIndex == 0)
            {
                if (doctor_id != "1")
                {
                    string id = privi_mdl.delete_attachment(doctor_id);
                    if (int.Parse(id) > 0)
                    {
                        DialogResult res = new System.Windows.Forms.DialogResult();
                        res = MessageBox.Show("Are you Sure You Want To Delete it?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (res == DialogResult.Yes)
                        {
                            this.ctrlr.delattach(attach_id);
                            Dgv_Attachment.Rows.Clear();
                            DataTable dtb = this.ctrlr.getattachment(patient_id);
                            getattachment(dtb);
                        }

                    }
                    else
                    {
                        MessageBox.Show("There is No Privilege to Delete Attachment", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
                else
                {
                    DialogResult res = new System.Windows.Forms.DialogResult();
                    res = MessageBox.Show("Are you Sure You Want To Delete it?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (res == DialogResult.Yes)
                    {
                        this.ctrlr.delattach(attach_id);
                        Dgv_Attachment.Rows.Clear();
                        DataTable dtb = this.ctrlr.getattachment(patient_id);
                        getattachment(dtb);
                    }
                }
            }
            else if (e.ColumnIndex == 6)
            {
                try
                {
                    string path = this.ctrlr.getpath(attach_id);
                    getpath(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if(e.ColumnIndex==7)
            {
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                string email = "", emailName = "", emailPass = "";
                string query = "select email_address,pt_name from tbl_patient where id='" + patient_id + "'";
                System.Data.DataTable sr = db.table(query);
                if (sr.Rows.Count > 0)
                {
                    //DialogResult res = MessageBox.Show("Data updated Successfully, Do you want to print...?", "Success",
                    //              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //if (res == DialogResult.Yes)
                    //{
                        email = sr.Rows[0]["email_address"].ToString();
                        string paths = this.ctrlr.getserver();
                        string path = this.ctrlr.getpath(attach_id);
                        if (email != "")
                        {
                            System.Data.DataTable sms = db.table("select emailName,emailPass from tbl_SmsEmailConfig");
                            if (sms.Rows.Count > 0)
                            {
                                emailName = sms.Rows[0]["emailName"].ToString();
                                emailPass = sms.Rows[0]["emailPass"].ToString();
                                try
                                {
                                    MailMessage mail = new MailMessage();
                                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                                    mail.From = new MailAddress(emailName);//"your mail@gmail.com"
                                    mail.To.Add(email);//"to_mail@gmail.com"
                                    mail.Subject = "Mail from Pappyjoe";
                                    mail.Body = "mail with attachment";

                                    System.Net.Mail.Attachment attachment;
                                    if (File.Exists(paths + path) == true)
                                    {
                                        attachment = new System.Net.Mail.Attachment(paths + path);
                                        mail.Attachments.Add(attachment);
                                    }


                                    SmtpServer.Port = 587;
                                    SmtpServer.Credentials = new System.Net.NetworkCredential(emailName, emailPass);
                                    SmtpServer.EnableSsl = true;

                                    SmtpServer.Send(mail);
                                    MessageBox.Show("Mail send successfully","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);

                                    
                                }
                                catch (Exception ex)
                                {
                                // support@pappyjoe.com
                                //supportingteam

                                }
                            }
                            else
                            {
                                MessageBox.Show("Please Activate Email Configuration", "Activation Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please Add EmailId for Selected patient", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    //}
                    //else
                    //{

                    //}
                       
                }
            }
        }
        public void getpath(string path1)
        {
            try
            {
                string realfile = "";
                string paths = this.ctrlr.getserver();
                if (path1 != "")
                {
                    realfile = paths + path1;
                    if (File.Exists(realfile) == true)
                    {
                        if (File_Type.ToLower() == ".jpeg" || File_Type.ToLower() == ".jpg" || File_Type.ToLower() == ".gif" || File_Type.ToLower() == ".png")
                        {
                            Image_Zoom frm = new Image_Zoom();
                            frm.attach_id = attach_id;
                            frm.ShowDialog(this);
                            frm.Dispose();
                        }
                        else if (File_Type.ToLower() == ".docx" || File_Type.ToLower() == ".doc")
                        {
                            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                            saveFileDialog1.Filter = "Word Documents|*.doc | Word Documents 2010|*.docx";
                            saveFileDialog1.Title = "Save an Image File";
                            saveFileDialog1.ShowDialog();
                            if (saveFileDialog1.FileName != "")
                            {
                                if (attach_id != 0)
                                {
                                    if (File.Exists(realfile))
                                    {
                                        System.IO.File.Copy(realfile, saveFileDialog1.FileName);
                                        MessageBox.Show("Successfully Saved !!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("File Not Found", "Not Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                        }
                        else if (File_Type.ToLower() == ".xls" || File_Type.ToLower() == ".xlsx")
                        {
                            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                            saveFileDialog1.Filter = "Excel Worksheets|*.xls | Excel Worksheets 2010|*.xlsx";
                            saveFileDialog1.Title = "Save an Image File";
                            saveFileDialog1.ShowDialog();
                            if (saveFileDialog1.FileName != "")
                            {
                                if (attach_id != 0)
                                {
                                    if (File.Exists(realfile))
                                    {
                                        System.IO.File.Copy(realfile, saveFileDialog1.FileName);
                                        MessageBox.Show("Successfully Saved !!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("File Not Found", "Not Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                        }
                        else if (File_Type.ToLower() == ".pdf")
                        {
                            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                            saveFileDialog1.Filter = "Pdf Files|*.pdf";
                            saveFileDialog1.Title = "Save an Image File";
                            saveFileDialog1.ShowDialog();
                            if (saveFileDialog1.FileName != "")
                            {
                                if (attach_id != 0)
                                {
                                    if (File.Exists(realfile))
                                    {
                                        System.IO.File.Copy(realfile, saveFileDialog1.FileName);
                                        MessageBox.Show("Successfully Saved !!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("File Not Found", "Not Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Image Not Found", "Not Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}

