using PappyjoeMVC.Controller;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
namespace PappyjoeMVC.View
{
    public partial class Add_Nurses_Notes : Form
    {

        Add_Nurses_Notes_controller cntrl = new Add_Nurses_Notes_controller();
      public  string plan_id = "", patient_id, Pt_name, doctor = "", pid = "",date="",procedurename="",pro_id="",dr_id="";
        public Boolean flag_notify = false;//bhj

        private void dgvNursesNote_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

        }

        private void dgv_remarks_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgv_remarks.Rows[e.RowIndex].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
            }
        }

        private void dgv_remarks_Leave(object sender, EventArgs e)
        {
            if (dgv_remarks.Rows.Count > 0)
            {
                int k = dgv_remarks.Rows.Count - 1;
                dgv_remarks.Rows[k].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
            }
        }

        private void dgvNursesNote_Leave(object sender, EventArgs e)
        {
            if (dgvNursesNote.Rows.Count > 0)
            {
                int k = dgvNursesNote.Rows.Count - 1;
                dgvNursesNote.Rows[k].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
            }
        }

        private void dgvNursesNote_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvNursesNote.Rows.Count > 1)
                {
                    if (e.ColumnIndex == 2)
                    {
                        if (dgvNursesNote.CurrentRow.Cells[1].Value != null && dgvNursesNote.CurrentRow.Cells[1].Value.ToString() != "")
                            dgvNursesNote.Rows.RemoveAt(this.dgvNursesNote.SelectedRows[0].Index);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_remarks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_remarks.Rows.Count > 1)
                {
                    if (e.ColumnIndex == 2)
                    {
                        if (dgv_remarks.CurrentRow.Cells[1].Value != null && dgv_remarks.CurrentRow.Cells[1].Value.ToString() != "")
                            dgv_remarks.Rows.RemoveAt(this.dgv_remarks.SelectedRows[0].Index);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable dt_tonurse = new DataTable();
        public int nurse_note_id = 0;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "SAVE")
            {
                if (cmb_staffname.Text != "")//pt_id,string dr_id,string staff_name,string date
                {
                    if (dgvNursesNote.Rows[0].Cells[1].Value != null && dgvNursesNote.Rows[0].Cells[1].Value.ToString() != "" || dgv_remarks.Rows[0].Cells[1].Value != null && dgv_remarks.Rows[0].Cells[1].Value.ToString() != "")
                    {
                        this.cntrl.save_main(patient_id, Cmb_doctor.SelectedValue.ToString(), cmb_staffname.SelectedValue.ToString(), dateTimePicker1.Value.ToString("yyyy-MM-dd"), plan_id,DGV_Procedure.Rows[0].Cells[0].Value.ToString(),DGV_Procedure.Rows[0].Cells[1].Value.ToString());
                        string maxid = this.cntrl.get_maxid();
                        if (dgvNursesNote.Rows.Count > 0)
                        {
                            for (int i = 0; i < dgvNursesNote.Rows.Count; i++)//string nurseid, string pt_id, string treatment_id, string procedure
                            {
                                if (dgvNursesNote[1, i].Value != null)
                                {
                                    string one = dgvNursesNote[1, i].Value.ToString();
                                    this.cntrl.save_nursenote(maxid, patient_id, plan_id, one);
                                }
                            }
                        }
                        if (dgv_remarks.Rows.Count > 0)
                        {
                            for (int i = 0; i < dgv_remarks.Rows.Count; i++)//string nurseid, string pt_id, string treatment_id, string procedure
                            {
                                if (dgv_remarks[1, i].Value != null)
                                {
                                    string one = dgv_remarks[1, i].Value.ToString();
                                    this.cntrl.save_nursenote_remark(maxid, patient_id, plan_id, one);
                                }
                            }
                        }
                        MessageBox.Show("Nurses note saved successfully", "Add Nurses Notes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string dt1 = DateTime.Now.Date.ToString("yyyy-MM-dd");
                        DateTime Timeonly = DateTime.Now;
                        this.cntrl.save_log(doctor, "Nurses Notes", "Adds Nurses Notes", dt1, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Add", maxid);
                        dgv_remarks.Rows.Clear();
                        dgvNursesNote.Rows.Clear();
                        DGV_Procedure.Rows.Clear();
                        this.Close();
                        //var form2 = new PappyjoeMVC.View.Clinical_Findings();
                        //form2.doctor_id = doctor_id;
                        //form2.patient_id = patient_id;
                        //openform(form2);
                    }
                    else
                    {
                        MessageBox.Show("Click Some Nurses Notes  And Then try again...", "Add Clinical Notes", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }
            else
            {
                if (cmb_staffname.Text != "")//pt_id,string dr_id,string staff_name,string date
                {
                    if (dgvNursesNote.Rows[0].Cells[1].Value != null && dgvNursesNote.Rows[0].Cells[1].Value.ToString() != "" || dgv_remarks.Rows[0].Cells[1].Value != null && dgv_remarks.Rows[0].Cells[1].Value.ToString() != "")
                    {
                        this.cntrl.update_main(patient_id, Cmb_doctor.SelectedValue.ToString(), cmb_staffname.SelectedValue.ToString(), dateTimePicker1.Value.ToString("yyyy-MM-dd"), plan_id, nurse_note_id.ToString());
                        //string maxid = this.cntrl.get_maxid();
                        if (dgvNursesNote.Rows.Count > 0)
                        {
                            this.cntrl.delete_notes(nurse_note_id.ToString());
                            for (int i = 0; i < dgvNursesNote.Rows.Count; i++)//string nurseid, string pt_id, string treatment_id, string procedure
                            {
                                if (dgvNursesNote[1, i].Value != null)
                                {
                                    string one = dgvNursesNote[1, i].Value.ToString();
                                    this.cntrl.save_nursenote(nurse_note_id.ToString(), patient_id, plan_id, one);
                                }
                            }
                        }
                        if (dgv_remarks.Rows.Count > 0)
                        {
                            this.cntrl.delete_remarks(nurse_note_id.ToString());
                            for (int i = 0; i < dgv_remarks.Rows.Count; i++)//string nurseid, string pt_id, string treatment_id, string procedure
                            {
                                if (dgv_remarks[1, i].Value != null)
                                {
                                    string one = dgv_remarks[1, i].Value.ToString();
                                    this.cntrl.save_nursenote_remark(nurse_note_id.ToString(), patient_id, plan_id, one);
                                }
                            }
                        }//pro_id, pt_id, name, plan_id, mainid, id,date)
                        this.cntrl.update_patient_treatments(patient_id, plan_id,dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                        MessageBox.Show("Nurses note updated successfully", "Update Nurses Notes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string dt1 = DateTime.Now.Date.ToString("yyyy-MM-dd");
                        DateTime Timeonly = DateTime.Now;
                        this.cntrl.save_log(doctor, "Nurses Notes", "Edit Nurses Notes", dt1, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Edit", nurse_note_id.ToString());
                        dgv_remarks.Rows.Clear();
                        dgvNursesNote.Rows.Clear();
                        DGV_Procedure.Rows.Clear();
                        btnSave.Text = "SAVE";
                        this.Close();

                    }
                }
            }
            //if(btnSave.Text== "SAVE")
            //{
            //    if(cmb_staffname.Text!="")//pt_id,string dr_id,string staff_name,string date
            //    {
            //        if(dgvNursesNote.Rows[0].Cells[1].Value != null && dgvNursesNote.Rows[0].Cells[1].Value.ToString() != "" || dgv_remarks.Rows[0].Cells[1].Value != null && dgv_remarks.Rows[0].Cells[1].Value.ToString() != "")
            //        {
            //            this.cntrl.save_main(patient_id,Cmb_doctor.SelectedValue.ToString(),cmb_staffname.SelectedValue.ToString(), dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            //            string maxid = this.cntrl.get_maxid();
            //            if (dgvNursesNote.Rows.Count > 0)
            //            {
            //                for (int i = 0; i < dgvNursesNote.Rows.Count; i++)//string nurseid, string pt_id, string treatment_id, string procedure
            //                {
            //                    if (dgvNursesNote[1, i].Value != null)
            //                    {
            //                        string one = dgvNursesNote[1, i].Value.ToString();
            //                        this.cntrl.save_nursenote(maxid, patient_id, plan_id, one);
            //                    }
            //                }
            //            }
            //            if (dgv_remarks.Rows.Count > 0)
            //            {
            //                for (int i = 0; i < dgv_remarks.Rows.Count; i++)//string nurseid, string pt_id, string treatment_id, string procedure
            //                {
            //                    if (dgv_remarks[1, i].Value != null)
            //                    {
            //                        string one = dgv_remarks[1, i].Value.ToString();
            //                        this.cntrl.save_nursenote_remark(maxid, patient_id, plan_id, one);
            //                    }
            //                }
            //            }
            //            MessageBox.Show("Nurses note saved successfully", "Add Nurses Notes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            string dt1 = DateTime.Now.Date.ToString("yyyy-MM-dd");
            //            DateTime Timeonly = DateTime.Now;
            //            this.cntrl.save_log(doctor, "Nurses Notes", "Adds Nurses Notes", dt1, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Add");
            //            dgv_remarks.Rows.Clear();
            //            dgvNursesNote.Rows.Clear();
            //            DGV_Procedure.Rows.Clear();
            //            this.Close();
            //            //var form2 = new PappyjoeMVC.View.Clinical_Findings();
            //            //form2.doctor_id = doctor_id;
            //            //form2.patient_id = patient_id;
            //            //openform(form2);
            //        }
            //        else
            //        {
            //            MessageBox.Show("Click Some Nurses Notes  And Then try again...", "Add Clinical Notes", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //        }
            //    }
            //}
        }

        public Add_Nurses_Notes()
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
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
           // var form2 = new PappyjoeMVC.View.Nurses_Notes();
           //form2.doctor_id = doctor_id;
           //form2.patient_id = patient_id;
           //openform(form2);
        }

        private void Add_Nurses_Notes_Load(object sender, EventArgs e)
        {
            
            DataTable staffname = this.cntrl.load_staffname();
            cmb_staffname.DataSource = null;
            if (staffname.Rows.Count > 0)
            {
                cmb_staffname.DisplayMember = "doctor_name";
                cmb_staffname.ValueMember = "id";
                cmb_staffname.DataSource = staffname;
            }

            DataTable doctorname = this.cntrl.load_doctorname();
            Cmb_doctor.DataSource = null;
            if (doctorname.Rows.Count > 0)
            {
                Cmb_doctor.DisplayMember = "doctor_name";
                Cmb_doctor.ValueMember = "id";
                Cmb_doctor.DataSource = doctorname;
            }
            if (flag_notify == true)//bhj
            {

                btnSave.Text = "UPDATE";
            }

                //DataTable dt_treatment = this.cntrl.get_patient_treatments(patient_id, date, plan_id);

                if (nurse_note_id>0)
            {
                linkLabel_Name.Text = Pt_name;
                linkLabel_id.Text = pid;
                DataTable dtb_main = this.cntrl.main_edit(nurse_note_id.ToString());
                if (cmb_staffname.Items.Count > 0)
                {
                    if (dtb_main.Rows[0]["staff_name"].ToString() != "")
                    {
                        cmb_staffname.SelectedValue = dtb_main.Rows[0]["staff_name"].ToString();
                    }
                }
                dateTimePicker1.Value = Convert.ToDateTime(dtb_main.Rows[0]["date"].ToString());

                DataTable dt_treatments = this.cntrl.get_patient_treatments(patient_id, Convert.ToDateTime(dtb_main.Rows[0]["date"].ToString()).ToString("yyyy-MM-dd"), dtb_main.Rows[0]["plan_id"].ToString());
                if (dt_treatments.Rows.Count > 0)
                {
                    plan_id = dtb_main.Rows[0]["plan_id"].ToString();
                    DGV_Procedure.Rows.Clear();
                    for (int i = 0; i < dt_treatments.Rows.Count; i++)
                    {
                        DGV_Procedure.Rows.Add();
                        DGV_Procedure.Rows[0].Cells["t_id"].Value = pro_id;
                        DGV_Procedure.Rows[0].Cells["pname"].Value = procedurename;
                    }
                }
                if (dtb_main.Rows.Count>0)
                {
                    DataTable dtb_notes = this.cntrl.note_edit(nurse_note_id.ToString());
                    if(dtb_notes.Rows.Count>0)
                    {
                        dgvNursesNote.Rows.Clear();
                        foreach(DataRow dr in dtb_notes.Rows)
                        {
                            dgvNursesNote.Rows.Add(dr["id"].ToString(),dr["procedure_notes"].ToString(),PappyjoeMVC.Properties.Resources.deleteicon);
                        }
                    }
                    DataTable dtb_remarks = this.cntrl.note_remarks_edit(nurse_note_id.ToString());
                    if (dtb_remarks.Rows.Count > 0)
                    {
                        dgv_remarks.Rows.Clear();
                        foreach (DataRow dr in dtb_remarks.Rows)
                        {
                            dgv_remarks.Rows.Add(dr["id"].ToString(), dr["Remarks"].ToString(), PappyjoeMVC.Properties.Resources.deleteicon);
                        }
                    }
                }
                btnSave.Text = "UPDATE";
            }
            else
            {
                if (Cmb_doctor.Items.Count > 0)
                {
                    if (doctor != "")
                    {
                        Cmb_doctor.Text = doctor;
                    }
                }

                linkLabel_Name.Text = Pt_name;
                linkLabel_id.Text = pid;
                // dateTimePicker1.Value = Convert.ToDateTime(date);
                dateTimePicker1.Value.ToString("yyyy-MM-dd");//bhj

                //DataTable dt_treatment = this.cntrl.get_patient_treatments(patient_id, Convert.ToDateTime(date).ToString("yyyy-MM-dd"), plan_id);
                //if (dt_treatment.Rows.Count > 0)
                //{
                DGV_Procedure.Rows.Clear();
                    //for (int i = 0; i < dt_tonurse.Rows.Count; i++)
                    //{
                        DGV_Procedure.Rows.Add();
                        DGV_Procedure.Rows[0].Cells["t_id"].Value = pro_id;
                        DGV_Procedure.Rows[0].Cells["pname"].Value = procedurename;
                    //}

                //}
            }
            dgvNursesNote.Rows[0].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
            dgv_remarks.Rows[0].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
        }

       
    }
}
