using PappyjoeMVC.Controller;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PappyjoeMVC.View
{
    public partial class Add_Clinical_Notes : Form
    {
        Clinical_Notes_Add_controller cntrl = new Clinical_Notes_Add_controller();
        public string doctor_id = "";
        public string staff_id = "";
        public string clinic_id = "";
        string idcomp, iddiag, idobs, idinv, idnote = "";
        public string patient_id = "";
        static int rowvalue; int medhisStatus = 0;
        public bool caledr_edit_flag = false;
        public Add_Clinical_Notes()
        {
            InitializeComponent();
        }

        private void investigationgrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = e.RowIndex;
                idinv = investigationgrid.Rows[r].Cells[0].Value.ToString();
                DataTable dt2 = this.cntrl.investigation_cell(idinv);
                bool entryFound = false;
                if (dt2.Rows.Count > 0)
                {
                    var value = dt2.Rows[0][1].ToString().Trim();
                    foreach (DataGridViewRow row in investigationgrid1.Rows)
                    {
                        object val1 = row.Cells[0].Value;
                        object val2 = row.Cells[1].Value;
                        if (val2 != null && val2.ToString() == value)
                        {
                            entryFound = true;
                            break;
                        }
                    }
                    if (!entryFound)
                    {
                        investigationgrid1.Rows.Add(dt2.Rows[0][0].ToString(), dt2.Rows[0][1].ToString());
                        investigationgrid1.Rows[investigationgrid1.Rows.Count - 1].Height = 30;
                        investigationgrid1.Rows[investigationgrid1.Rows.Count - 1].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                        del2.ImageLayout = DataGridViewImageCellLayout.Normal;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void diagnosisgrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = e.RowIndex;
                iddiag = diagnosisgrid.Rows[r].Cells[0].Value.ToString();
                DataTable dt3 = this.cntrl.diagnose_cell(iddiag);
                bool entryFound = false;
                if (dt3.Rows.Count > 0)
                {
                    var value = dt3.Rows[0][1].ToString().Trim();
                    foreach (DataGridViewRow row in diagnosisgrid1.Rows)
                    {
                        object val1 = row.Cells[0].Value;
                        object val2 = row.Cells[1].Value;
                        if (val2 != null && val2.ToString() == value)
                        {
                            entryFound = true;
                            break;
                        }
                    }
                    if (!entryFound)
                    {
                        diagnosisgrid1.Rows.Add(dt3.Rows[0][0].ToString(), dt3.Rows[0][1].ToString());
                        diagnosisgrid1.Rows[diagnosisgrid1.Rows.Count - 1].Height = 30;
                        diagnosisgrid1.Rows[diagnosisgrid1.Rows.Count - 1].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                        del3.ImageLayout = DataGridViewImageCellLayout.Normal;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void complaintgrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = e.RowIndex;
                idcomp = complaintgrid.Rows[r].Cells[0].Value.ToString();
                DataTable dt = this.cntrl.complaint_cell(idcomp);
                bool entryFound = false;
                if (dt.Rows.Count > 0)
                {
                    var value = dt.Rows[0][1].ToString().Trim();
                    foreach (DataGridViewRow row in complaintgrid1.Rows)
                    {
                        object val1 = row.Cells[0].Value;
                        object val2 = row.Cells[1].Value;
                        if (val2 != null && val2.ToString() == value)
                        {
                            entryFound = true;
                            break;
                        }
                    }
                    if (!entryFound)
                    {
                        complaintgrid1.Rows.Add(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString());
                        complaintgrid1.Rows[complaintgrid1.Rows.Count - 1].Height = 30;
                        complaintgrid1.Rows[complaintgrid1.Rows.Count - 1].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                        del.ImageLayout = DataGridViewImageCellLayout.Normal;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void notegrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = e.RowIndex;
                idnote = notegrid.Rows[r].Cells[0].Value.ToString();
                DataTable dt4 = this.cntrl.notes_cell(idnote);
                bool entryFound = false;
                if (dt4.Rows.Count > 0)
                {
                    var value = dt4.Rows[0][1].ToString().Trim();
                    foreach (DataGridViewRow row in notesgrid1.Rows)
                    {
                        object val1 = row.Cells[0].Value;
                        object val2 = row.Cells[1].Value;
                        if (val2 != null && val2.ToString() == value)
                        {
                            entryFound = true;
                            break;
                        }
                    }
                    if (!entryFound)
                    {
                        notesgrid1.Rows.Add(dt4.Rows[0][0].ToString(), dt4.Rows[0][1].ToString());
                        notesgrid1.Rows[notesgrid1.Rows.Count - 1].Height = 30;
                        notesgrid1.Rows[notesgrid1.Rows.Count - 1].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                        del4.ImageLayout = DataGridViewImageCellLayout.Normal;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void observationgrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = e.RowIndex;
                idobs = observationgrid.Rows[r].Cells[0].Value.ToString();
                DataTable dt1 = this.cntrl.observation_cell(idobs);
                bool entryFound = false;
                if (dt1.Rows.Count > 0)
                {
                    var value = dt1.Rows[0][1].ToString().Trim();
                    foreach (DataGridViewRow row in observationgrid1.Rows)
                    {
                        object val1 = row.Cells[0].Value;
                        object val2 = row.Cells[1].Value;
                        if (val2 != null && val2.ToString() == value)
                        {
                            this.observationgrid1.Rows[e.RowIndex].Selected = true;
                            entryFound = true;
                            break;
                        }
                    }
                    if (!entryFound)
                    {
                        observationgrid1.Rows.Add(dt1.Rows[0][0].ToString(), dt1.Rows[0][1].ToString());
                        observationgrid1.Rows[observationgrid1.Rows.Count - 1].Height = 30;
                        observationgrid1.Rows[observationgrid1.Rows.Count - 1].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                        del1.ImageLayout = DataGridViewImageCellLayout.Normal;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void compsearchtext_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                DataTable dt = this.cntrl.compsearch(compsearchtext.Text);
                complaintgrid.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void compsearchtext_MouseClick(object sender, MouseEventArgs e)
        {
            compsearchtext.Text = null;
        }

        private void notesearchtext_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                DataTable dt4 = this.cntrl.notesearch(notesearchtext.Text);
                notegrid.DataSource = dt4;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void obsersearchtext_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                DataTable dt1 = this.cntrl.observsearch(obsersearchtext.Text);
                observationgrid.DataSource = dt1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void notesearchtext_MouseClick(object sender, MouseEventArgs e)
        {
            notesearchtext.Text = null;
        }

        private void obsersearchtext_MouseClick(object sender, MouseEventArgs e)
        {
            obsersearchtext.Text = null;
        }
        private void diagsearchtext_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                DataTable dt3 = this.cntrl.diagnosetxtsearch(diagsearchtext.Text);
                diagnosisgrid.DataSource = dt3;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void investsearchtext_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                DataTable dt2 = this.cntrl.investsearchtxt(investsearchtext.Text);
                investigationgrid.DataSource = dt2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void investsearchtext_MouseClick(object sender, MouseEventArgs e)
        {
            investsearchtext.Text = null;
        }

        private void investsavebut_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable checkdataINVEST = this.cntrl.CheckInvest(investtextbox.Text.Replace("'", ""));
                if (checkdataINVEST.Rows.Count > 0)
                {
                    MessageBox.Show("Record " + investtextbox.Text + " already exist", "Duplication Encountered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (investtextbox.Text != "")
                    {
                        this.cntrl.investigation_insert(investtextbox.Text);
                        DataTable dt2 = this.cntrl.Show_investigation();
                        investigationgrid.DataSource = dt2;
                        label17.Hide();
                        investtextbox.Hide();
                        investsavebut.Hide();
                        investcancel.Hide();
                        investtextbox.Text = "";
                        lab_investSearch.Show();
                        investsearchtext.Show();
                        investadd.Show();
                        btn_imprt_investgtn.Show();
                        lab_investSearch.Location = new Point(6, 13);
                        investsearchtext.Location = new Point(62, 8);
                        investigationgrid.Location = new Point(3, 37);
                    }
                    else
                    {
                        MessageBox.Show("Enter the data..!!", "Data Required..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        investtextbox.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void diagsavebut_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable checkdataDIAG = this.cntrl.CheckdataDiag(diagtext.Text.Replace("'", ""));
                if (checkdataDIAG.Rows.Count > 0)
                {
                    MessageBox.Show("Record " + diagtext.Text + " already exist", "Duplication Encountered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (diagtext.Text != "")
                    {
                        this.cntrl.Insert_diagno(diagtext.Text);
                        DataTable dt3 = this.cntrl.show_diagno();
                        diagnosisgrid.DataSource = dt3;
                        label11.Hide();
                        diagtext.Hide();
                        diagsavebut.Hide();
                        diagcancel.Hide();
                        diagtext.Text = "";
                        Lab_Diag_Search.Show();
                        diagsearchtext.Show();
                        diagadd.Show();
                        btn_imprt_diagno.Show();
                        Lab_Diag_Search.Location = new Point(6, 13);
                        diagsearchtext.Location = new Point(62, 8);
                        diagnosisgrid.Location = new Point(3, 37);
                    }
                    else
                    {
                        MessageBox.Show("Enter the data..!!", "Data Required..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        diagtext.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void compsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (compsave.Text == "Save")
                {
                    DataTable checkdatacc = this.cntrl.checkdataAcc(comptextbox.Text.Replace("'", ""));
                    if (checkdatacc.Rows.Count > 0)
                    {
                        MessageBox.Show("Record " + comptextbox.Text + " already exist", "Duplication Encountered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (comptextbox.Text != "")
                        {
                            this.cntrl.insert_compl(comptextbox.Text);
                            DataTable dt = this.cntrl.show_compl();
                            complaintgrid.DataSource = dt;
                            lad_compAddNew.Hide();
                            comptextbox.Hide();
                            compsave.Hide();
                            compcancel.Hide();
                            lab_compSearch.Show();
                            compsearchtext.Show();
                            compadd.Show();
                            btn_imprt_complnt.Show();
                            lab_compSearch.Location = new Point(6, 13);
                            compsearchtext.Location = new Point(62, 8);
                            complaintgrid.Location = new Point(3, 37);
                        }
                        else
                        {
                            MessageBox.Show("Enter the data..!!", "Data Required..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            comptextbox.Focus();
                        }
                    }
                }
                else if (compsave.Text == "Update")
                {
                    if (comptextbox.Text != "")
                    {
                        int i = 0;
                        this.cntrl.Update_compl(comptextbox.Text, rowvalue);
                        if (i > 0)
                        {
                            DataTable dt = this.cntrl.show_compl();
                            complaintgrid.DataSource = dt;
                            compsave.Text = "Save";
                            compadd.Visible = true;
                            btn_imprt_complnt.Visible = true;
                            compsearchtext.Visible = true;
                            compcancel.Visible = false;
                            compsave.Visible = false;
                            comptextbox.Visible = false;
                            lab_compSearch.Location = new Point(6, 13);
                            compsearchtext.Location = new Point(62, 8);
                            complaintgrid.Location = new Point(3, 37);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void notesavebut_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable checkdataNOTE = this.cntrl.checkdataNote(notetextbox.Text.Replace("'", ""));
                if (checkdataNOTE.Rows.Count > 0)
                {
                    MessageBox.Show("Record " + notetextbox.Text + " already exist", "Duplication Encountered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (notetextbox.Text != "")
                    {
                        this.cntrl.insert_note(notetextbox.Text);
                        DataTable dt4 = this.cntrl.show_note();
                        notegrid.DataSource = dt4;
                        label14.Hide();
                        notetextbox.Hide();
                        notesavebut.Hide();
                        notecancel.Hide();
                        notetextbox.Text = "";
                        lab_NotesSearch.Show();
                        notesearchtext.Show();
                        noteadd.Show();
                        btn_imprt_notes.Show();
                        lab_NotesSearch.Location = new Point(6, 13);
                        notesearchtext.Location = new Point(62, 8);
                        notegrid.Location = new Point(3, 37);
                    }
                    else
                    {
                        MessageBox.Show("Enter the data..!!", "Data Required..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        notetextbox.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void obsersavbut_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable checkdataOB = this.cntrl.checkdataOB(obsertextbox.Text.Replace("'", ""));
                if (checkdataOB.Rows.Count > 0)
                {
                    MessageBox.Show("Record " + obsertextbox.Text + "  already exists..", "Duplication encountered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (obsertextbox.Text != "")
                    {
                        this.cntrl.insert_Observ(obsertextbox.Text);
                        DataTable dt1 = this.cntrl.show_observation();
                        observationgrid.DataSource = dt1;
                        label12.Hide();
                        obsertextbox.Hide();
                        obsersavbut.Hide();
                        obsercancel.Hide();
                        obsertextbox.Text = "";
                        lab_observeSearch.Show();
                        obsersearchtext.Show();
                        obseradd.Show();
                        btn_imprt_obsrvtn.Show();
                        lab_observeSearch.Location = new Point(6, 13);
                        obsersearchtext.Location = new Point(62, 8);
                        observationgrid.Location = new Point(3, 37);
                    }
                    else
                    {
                        MessageBox.Show("Enter the data..!!", "Data Required..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        obsertextbox.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void complaintgrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (complaintgrid1.Rows.Count > 1)
                {
                    if (e.ColumnIndex == 2)
                    {
                        if (complaintgrid1.CurrentRow.Cells[1].Value != null && complaintgrid1.CurrentRow.Cells[1].Value.ToString() != "")
                            complaintgrid1.Rows.RemoveAt(this.complaintgrid1.SelectedRows[0].Index);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void observationgrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (observationgrid1.Rows.Count > 1)
                {
                    if (e.ColumnIndex == 2)
                    {
                        if (observationgrid1.CurrentRow.Cells[1].Value != null && observationgrid1.CurrentRow.Cells[1].Value.ToString() != "")
                            observationgrid1.Rows.RemoveAt(this.observationgrid1.SelectedRows[0].Index);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void diagnosisgrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (diagnosisgrid1.Rows.Count > 1)
                {
                    if (e.ColumnIndex == 2)
                    {
                        if (diagnosisgrid1.CurrentRow.Cells[1].Value != null && diagnosisgrid1.CurrentRow.Cells[1].Value.ToString() != "")
                            diagnosisgrid1.Rows.RemoveAt(this.diagnosisgrid1.SelectedRows[0].Index);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void investigationgrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (investigationgrid1.Rows.Count > 1)
                {
                    if (e.ColumnIndex == 2)
                    {
                        if (investigationgrid1.CurrentRow.Cells[1].Value != null && investigationgrid1.CurrentRow.Cells[1].Value.ToString() != "")
                            investigationgrid1.Rows.RemoveAt(this.investigationgrid1.SelectedRows[0].Index);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void notesgrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (notesgrid1.Rows.Count > 1)
                {
                    if (e.ColumnIndex == 2)
                    {
                        if (notesgrid1.CurrentRow.Cells[1].Value != null && notesgrid1.CurrentRow.Cells[1].Value.ToString() != "")
                            notesgrid1.Rows.RemoveAt(this.notesgrid1.SelectedRows[0].Index);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        
        private void compsearchtext_Click(object sender, EventArgs e)
        {
            compsearchtext.Clear();
        }

        private void obsersearchtext_Click(object sender, EventArgs e)
        {
            obsersearchtext.Clear();
        }

        private void investsearchtext_Click(object sender, EventArgs e)
        {
            investsearchtext.Clear();
        }

        private void diagsearchtext_Click(object sender, EventArgs e)
        {
            diagsearchtext.Clear();
        }

        private void notesearchtext_Click(object sender, EventArgs e)
        {
            notesearchtext.Clear();
        }

        private void notesgrid1_Click(object sender, EventArgs e)
        {
            cmb_clinicalfinding.SelectedIndex = 4;
        }

        private void diagnosisgrid1_Click(object sender, EventArgs e)
        {
            cmb_clinicalfinding.SelectedIndex = 3;
        }

        private void investigationgrid1_Click(object sender, EventArgs e)
        {
            cmb_clinicalfinding.SelectedIndex = 2;
        }

        private void observationgrid1_Click(object sender, EventArgs e)
        {
            cmb_clinicalfinding.SelectedIndex = 1;
        }

        private void complaintgrid1_Click(object sender, EventArgs e)
        {
            cmb_clinicalfinding.SelectedIndex = 0;
        }

        private void compadd_Click(object sender, EventArgs e)
        {
            lad_compAddNew.Show();
            comptextbox.Show();
            compsave.Show();
            compcancel.Show();
            comptextbox.Text = "";
            lab_compSearch.Hide();
            compsearchtext.Hide();
            compadd.Hide();
            btn_imprt_complnt.Hide();
            complaintgrid.Location = new Point(3, 65);
        }

        private void compcancel_Click(object sender, EventArgs e)
        {
            lad_compAddNew.Hide();
            comptextbox.Hide();
            compsave.Hide();
            compcancel.Hide();
            lab_compSearch.Show();
            compsearchtext.Show();
            compadd.Show();
            btn_imprt_complnt.Show();
            lab_compSearch.Location = new Point(6, 13);
            compsearchtext.Location = new Point(62, 8);
            complaintgrid.Location = new Point(3, 37);
        }

        private void obseradd_Click(object sender, EventArgs e)
        {
            label12.Show();
            obsertextbox.Show();
            obsersavbut.Show();
            obsercancel.Show();
            obsertextbox.Text = "";
            lab_observeSearch.Hide();
            obsersearchtext.Hide();
            obseradd.Hide();
            btn_imprt_obsrvtn.Hide();
            observationgrid.Location = new Point(3, 65);
        }

        private void diagadd_Click(object sender, EventArgs e)
        {
            label11.Show();
            diagtext.Show();
            diagsavebut.Show();
            diagcancel.Show();
            diagtext.Text = "";
            Lab_Diag_Search.Hide();
            diagsearchtext.Hide();
            diagadd.Hide();
            btn_imprt_diagno.Hide();
            diagnosisgrid.Location = new Point(3, 65);
        }

        private void noteadd_Click(object sender, EventArgs e)
        {
            label14.Show();
            notetextbox.Show();
            notesavebut.Show();
            notecancel.Show();
            notetextbox.Text = "";
            lab_NotesSearch.Hide();
            notesearchtext.Hide();
            noteadd.Hide();
            btn_imprt_notes.Hide();
            notegrid.Location = new Point(3, 65);
        }

        private void investadd_Click(object sender, EventArgs e)
        {
            label17.Show();
            investtextbox.Show();
            investsavebut.Show();
            investcancel.Show();
            investtextbox.Text = "";
            lab_investSearch.Hide();
            investsearchtext.Hide();
            investadd.Hide();
            btn_imprt_investgtn.Hide();
            investigationgrid.Location = new Point(3, 65);
        }

        private void obsercancel_Click(object sender, EventArgs e)
        {
            label12.Hide();
            obsertextbox.Hide();
            obsersavbut.Hide();
            obsercancel.Hide();
            obsertextbox.Text = "";
            lab_observeSearch.Show();
            obsersearchtext.Show();
            obseradd.Show();
            btn_imprt_obsrvtn.Show();
            lab_observeSearch.Location = new Point(6, 13);
            obsersearchtext.Location = new Point(62, 8);
            observationgrid.Location = new Point(3, 37);
        }

        private void investcancel_Click(object sender, EventArgs e)
        {
            label17.Hide();
            investtextbox.Hide();
            investsavebut.Hide();
            investcancel.Hide();
            investtextbox.Text = "";
            lab_investSearch.Show();
            investsearchtext.Show();
            investadd.Show();
            btn_imprt_investgtn.Show();
            lab_investSearch.Location = new Point(6, 13);
            investsearchtext.Location = new Point(62, 8);
            investigationgrid.Location = new Point(3, 37);
        }

        private void diagcancel_Click(object sender, EventArgs e)
        {
            label11.Hide();
            diagtext.Hide();
            diagsavebut.Hide();
            diagcancel.Hide();
            diagtext.Text = "";
            Lab_Diag_Search.Show();
            diagsearchtext.Show();
            diagadd.Show();
            btn_imprt_diagno.Show();
            Lab_Diag_Search.Location = new Point(6, 13);
            diagsearchtext.Location = new Point(62, 8);
            diagnosisgrid.Location = new Point(3, 37);
        }

        private void notecancel_Click(object sender, EventArgs e)
        {
            label14.Hide();
            notetextbox.Hide();
            notesavebut.Hide();
            notecancel.Hide();
            notetextbox.Text = "";
            lab_NotesSearch.Show();
            notesearchtext.Show();
            noteadd.Show();
            btn_imprt_notes.Show();
            lab_NotesSearch.Location = new Point(6, 13);
            notesearchtext.Location = new Point(62, 8);
            notegrid.Location = new Point(3, 37);
        }

        private void ClinicalNotesAdd_Load(object sender, EventArgs e)
        {
            try
            {
                //if (doctor_id != "1")
                //{
                //    string privid = this.cntrl.userPrivilege_for_ClinicalNotes_Add(doctor_id);
                //    if (int.Parse(privid) < 0)
                //    {
                //        btnSave.Enabled = false;
                //    }
                //    else
                //    {
                //        btnSave.Enabled = true;
                //    }
                //}
                dgv_medical.Rows.Clear();
                 DataTable clinicname = this.cntrl.Get_CompanyNAme();
                if (clinicname.Rows.Count > 0)
                {
                    string clinicn = "";
                    clinicn = clinicname.Rows[0][0].ToString();
                }
                string docnam = this.cntrl.Get_DoctorName(doctor_id);
                System.Data.DataTable pay = this.cntrl.get_total_payment(patient_id);
                if (pay.Rows.Count > 0)
                {
                    Lab_Nodue.Text = pay.Rows[0]["total_payment"].ToString() + " due";
                }
                DataTable dt_dr = this.cntrl.get_all_doctorname();
                Cmb_doctor.DisplayMember = "doctor_name";
                Cmb_doctor.ValueMember = "id";
                Cmb_doctor.DataSource = dt_dr;
                Cmb_doctor.SelectedIndex = 0;
                cmb_clinicalfinding.SelectedIndex = 0;
                DataTable dt = this.cntrl.Get_patient_id_name_gender(patient_id);
                if (dt.Rows[0]["pt_name"].ToString() != "")
                {
                    linkLabel_Name.Text = dt.Rows[0]["pt_name"].ToString();
                }
                if (dt.Rows[0]["pt_id"].ToString() != "")
                {
                    linkLabel_id.Text = dt.Rows[0]["pt_id"].ToString();
                }
                if (caledr_edit_flag == true)
                {
                    string clin_id = this.cntrl.get_clinicId(patient_id);
                    if (clin_id != "")
                    {
                        clinic_id = clin_id;
                    }
                }
                panl_content.Location = new Point(1, 92);
                if (clinic_id != "" && clinic_id!="0")
                {
                    btnSave.Text = "Update";
                    DataTable dt_cf = this.cntrl.getdatafrom_clinicalFindings(clinic_id, patient_id);
                    if (dt_cf.Rows.Count > 0)
                    {
                        dateTimePicker1.Value = Convert.ToDateTime(dt_cf.Rows[0][1].ToString());
                        int index = Cmb_doctor.FindString(Convert.ToString(dt_cf.Rows[0][2].ToString()));
                        if (index >= 0)
                        {
                            Cmb_doctor.SelectedIndex = index;
                        }
                        else
                        {
                            Cmb_doctor.SelectedIndex = 0;
                        }
                        System.Data.DataTable dt_cf_Complaints = this.cntrl.getComplaints(clinic_id);
                        if (dt_cf_Complaints.Rows.Count > 0)
                        {
                            for (int k = 0; k < dt_cf_Complaints.Rows.Count; k++)
                            {
                                complaintgrid1.Rows.Add(dt_cf_Complaints.Rows[k]["id"].ToString(), dt_cf_Complaints.Rows[k]["complaint_id"].ToString());
                                complaintgrid1.Rows[complaintgrid1.Rows.Count - 1].Height = 30;
                                complaintgrid1.Rows[complaintgrid1.Rows.Count - 1].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                                del.ImageLayout = DataGridViewImageCellLayout.Normal;
                            }
                        }
                        System.Data.DataTable dt_cf_observe = this.cntrl.get_observation(clinic_id);
                        if (dt_cf_observe.Rows.Count > 0)
                        {
                            for (int k = 0; k < dt_cf_observe.Rows.Count; k++)
                            {
                                observationgrid1.Rows.Add(dt_cf_observe.Rows[k]["id"].ToString(), dt_cf_observe.Rows[k]["observation_id"].ToString());
                                observationgrid1.Rows[observationgrid1.Rows.Count - 1].Height = 30;
                                observationgrid1.Rows[observationgrid1.Rows.Count - 1].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                                del1.ImageLayout = DataGridViewImageCellLayout.Normal;
                            }
                        }
                        System.Data.DataTable dt_cf_investigation = this.cntrl.get_invest(clinic_id);
                        if (dt_cf_investigation.Rows.Count > 0)
                        {
                            for (int k = 0; k < dt_cf_investigation.Rows.Count; k++)
                            {
                                investigationgrid1.Rows.Add(dt_cf_investigation.Rows[k]["id"].ToString(), dt_cf_investigation.Rows[k]["investigation_id"].ToString());
                                investigationgrid1.Rows[investigationgrid1.Rows.Count - 1].Height = 30;
                                investigationgrid1.Rows[investigationgrid1.Rows.Count - 1].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                                del2.ImageLayout = DataGridViewImageCellLayout.Normal;
                            }
                        }
                        System.Data.DataTable dt_cf_diagnosis = this.cntrl.get_diagno(clinic_id);
                        if (dt_cf_diagnosis.Rows.Count > 0)
                        {
                            for (int k = 0; k < dt_cf_diagnosis.Rows.Count; k++)
                            {
                                diagnosisgrid1.Rows.Add(dt_cf_diagnosis.Rows[k]["id"].ToString(), dt_cf_diagnosis.Rows[k]["diagnosis_id"].ToString());
                                diagnosisgrid1.Rows[diagnosisgrid1.Rows.Count - 1].Height = 30;
                                diagnosisgrid1.Rows[diagnosisgrid1.Rows.Count - 1].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                                del3.ImageLayout = DataGridViewImageCellLayout.Normal;
                            }
                        }
                        System.Data.DataTable dt_cf_note = this.cntrl.get_note(clinic_id);
                        if (dt_cf_note.Rows.Count > 0)
                        {
                            for (int k = 0; k < dt_cf_note.Rows.Count; k++)
                            {
                                notesgrid1.Rows.Add(dt_cf_note.Rows[k]["id"].ToString(), dt_cf_note.Rows[k]["note_name"].ToString());
                                notesgrid1.Rows[notesgrid1.Rows.Count - 1].Height = 30;
                                notesgrid1.Rows[notesgrid1.Rows.Count - 1].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                                del4.ImageLayout = DataGridViewImageCellLayout.Normal;
                            }
                        }
                        System.Data.DataTable dt_cf_allergy = this.cntrl.get_allergy(clinic_id);
                        if (dt_cf_allergy.Rows.Count > 0)
                        {
                            for (int k = 0; k < dt_cf_allergy.Rows.Count; k++)
                            {
                                dgvAllerg.Rows.Add(dt_cf_allergy.Rows[k]["id"].ToString(), dt_cf_allergy.Rows[k]["allergy_name"].ToString());
                                dgvAllerg.Rows[dgvAllerg.Rows.Count - 1].Height = 30;
                                dgvAllerg.Rows[dgvAllerg.Rows.Count - 1].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                                del5.ImageLayout = DataGridViewImageCellLayout.Normal;
                            }
                        }
                        DataTable dt_details = this.cntrl.get_pt_meditation(clinic_id);
                        if (dt_details.Rows.Count > 0)
                        {
                            for (int k = 0; k < dt_details.Rows.Count; k++)
                            {
                                dgv_meditation.Rows.Add(dt_details.Rows[k]["id"].ToString(), dt_details.Rows[k]["current_meditation"].ToString());
                                dgv_meditation.Rows[dgv_meditation.Rows.Count - 1].Height = 30;
                                dgv_meditation.Rows[dgv_meditation.Rows.Count - 1].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                                del4.ImageLayout = DataGridViewImageCellLayout.Normal;
                            }
                        }
                        DataTable dt_advice = this.cntrl.get_pt_advice(clinic_id);
                        if (dt_advice.Rows.Count > 0)
                        {
                            for (int k = 0; k < dt_advice.Rows.Count; k++)
                            {
                                dgv_discharge_advice.Rows.Add(dt_advice.Rows[k]["id"].ToString(), dt_advice.Rows[k]["discharge_advice"].ToString());
                                dgv_discharge_advice.Rows[dgv_discharge_advice.Rows.Count - 1].Height = 30;
                                dgv_discharge_advice.Rows[dgv_discharge_advice.Rows.Count - 1].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                                del4.ImageLayout = DataGridViewImageCellLayout.Normal;
                            }
                        }
                        DataTable dt_nurses_note = this.cntrl.get_nursenotes(clinic_id);
                        if(dt_nurses_note.Rows.Count > 0)
                        {
                            for (int k = 0; k < dt_nurses_note.Rows.Count; k++)
                            {
                                dgvNursesNote.Rows.Add(dt_nurses_note.Rows[k]["id"].ToString(), dt_nurses_note.Rows[k]["nurses_note"].ToString());
                                dgvNursesNote.Rows[dgvNursesNote.Rows.Count - 1].Height = 30;
                                dgvNursesNote.Rows[dgvNursesNote.Rows.Count - 1].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                                del4.ImageLayout = DataGridViewImageCellLayout.Normal;
                            }
                        }
                    }
                    DataGridViewCheckBoxColumn check = new DataGridViewCheckBoxColumn()
                    {
                        Name = "Check"
                    };
                    grmedical.Columns.Add(check);
                    check.Width = 100;
                    check.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DataTable dt8 = this.cntrl.get_medicalId(patient_id);
                    for (int j = 0; j < dt8.Rows.Count; j++)
                    {
                        grmedical.Rows.Add(dt8.Rows[j][0].ToString());
                        grmedical.Rows[j].Cells[1].Value = true;
                        grmedical.Rows[j].Cells[0].Style.BackColor = Color.FromArgb(62, 165, 195);
                        grmedical.Rows[j].Cells[1].Style.BackColor = Color.FromArgb(62, 165, 195);

                        dgv_medical.Rows.Add("", dt8.Rows[j][0].ToString());
                        dgv_medical.Rows[dgv_medical.Rows.Count - 1].Height = 30;
                        dgv_medical.Rows[dgv_medical.Rows.Count - 1].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                        del4.ImageLayout = DataGridViewImageCellLayout.Normal;
                    }
                   
                    DataTable dt35 = this.cntrl.Get_medicalname();
                    for (int j = 0; j < dt35.Rows.Count; j++)
                    {
                        string mht = this.cntrl.patient_medical(patient_id, dt35.Rows[j][0].ToString());
                        if (mht == "0")
                        {
                            grmedical.Rows.Add(dt35.Rows[j][0].ToString());
                        }
                    }
                }
                else
                {
                    grmedical.Visible = true;
                    DataGridViewCheckBoxColumn check = new DataGridViewCheckBoxColumn()
                    {
                        Name = "Check"
                    };
                    grmedical.Columns.Add(check);
                    check.Width = 100;
                    check.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //DataTable dt8 = this.cntrl.get_medicalId(patient_id);
                    //for (int j = 0; j < dt8.Rows.Count; j++)
                    //{
                    //    grmedical.Rows.Add(dt8.Rows[j][0].ToString());
                    //    grmedical.Rows[j].Cells[1].Value = true;
                    //    grmedical.Rows[j].Cells[0].Style.BackColor = Color.FromArgb(62, 165, 195);
                    //    grmedical.Rows[j].Cells[1].Style.BackColor = Color.FromArgb(62, 165, 195);

                    //    //dgv_medical.Rows.Add("", dt8.Rows[j][0].ToString());
                    //    //dgv_medical.Rows[dgv_medical.Rows.Count - 1].Height = 30;
                    //    //dgv_medical.Rows[dgv_medical.Rows.Count - 1].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                    //    //del4.ImageLayout = DataGridViewImageCellLayout.Normal;
                    //}

                    DataTable dt35 = this.cntrl.Get_medicalname();
                    for (int j = 0; j < dt35.Rows.Count; j++)
                    {
                        string mht = this.cntrl.patient_medical(patient_id, dt35.Rows[j][0].ToString());
                        if (mht == "0")
                        {
                            grmedical.Rows.Add(dt35.Rows[j][0].ToString());
                        }
                    }
                   
                }
                DataTable dt1 = this.cntrl.show_compl();
                complaintgrid.DataSource = dt1;
                lad_compAddNew.Hide();
                comptextbox.Hide();
                compsave.Hide();
                compcancel.Hide();
                lab_compSearch.Show();
                compsearchtext.Show();
                compadd.Show();
                btn_imprt_complnt.Show();
                lab_compSearch.Location = new Point(6, 13);
                compsearchtext.Location = new Point(62, 8);
                complaintgrid.Location = new Point(3, 37);
                complaintpanel.Height = 549;
                complaintpanel.Location = new Point(10, 34);
                complaintpanel.Visible = true;
                investigationpanel.Visible = false;
                diagnosispanel.Visible = false;
                observationpanel.Visible = false;
                notespanel.Visible = false;
                pnl_Allergies.Visible = false;
                complaintgrid1.Rows[0].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                observationgrid1.Rows[0].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                investigationgrid1.Rows[0].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                diagnosisgrid1.Rows[0].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                notesgrid1.Rows[0].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                dgvAllerg.Rows[0].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                dgv_medical.Rows[0].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                dgv_meditation.Rows[0].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                dgv_discharge_advice.Rows[0].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                dgvNursesNote.Rows[0].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                // 


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void fill_grid_medical(DataTable dtb)
        {
            if(dtb.Rows.Count>0)
            {
                grmedical.Rows.Clear();
                for (int j = 0; j < dtb.Rows.Count; j++)
                {
                    grmedical.Rows.Add(dtb.Rows[j][0].ToString());
                    //grmedical.Rows[j].Cells[1].Value = true;
                    //grmedical.Rows[j].Cells[0].Style.BackColor = Color.FromArgb(62, 165, 195);
                    //grmedical.Rows[j].Cells[1].Style.BackColor = Color.FromArgb(62, 165, 195);
                }
            }
        }
        private void cmb_clinicalfinding_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                investigationpanel.Hide();
                observationpanel.Hide();
                diagnosispanel.Hide();
                notespanel.Hide();
                pnl_Allergies.Hide();
                panel15.Size = new Size(406, 308);
                complaintpanel.Visible = false;
                if (cmb_clinicalfinding.Text == "Investigations")
                {
                    investigationpanel.Visible = true;
                    DataTable dt2 = this.cntrl.Show_investigation();
                    investigationgrid.DataSource = dt2;
                    label17.Hide();
                    investtextbox.Hide();
                    investsavebut.Hide();
                    investcancel.Hide();
                    investtextbox.Text = "";
                    lab_investSearch.Show();
                    investsearchtext.Show();
                    investadd.Show();
                    btn_imprt_investgtn.Show();
                    lab_investSearch.Location = new Point(6, 13);
                    investsearchtext.Location = new Point(62, 8);
                    investigationgrid.Location = new Point(3, 37);
                    investigationpanel.Location = new Point(14, 29);
                    //investigationpanel.Height = 200;// 549
                }
                else if (cmb_clinicalfinding.Text == "Observations")
                {
                    observationpanel.Visible = true;
                    label12.Hide();
                    obsertextbox.Hide();
                    obsersavbut.Hide();
                    obsercancel.Hide();
                    obsertextbox.Text = "";
                    lab_observeSearch.Show();
                    obsersearchtext.Show();
                    obseradd.Show();
                    btn_imprt_obsrvtn.Show();
                    lab_observeSearch.Location = new Point(6, 13);
                    obsersearchtext.Location = new Point(62, 8);
                    observationgrid.Location = new Point(3, 37);
                    observationpanel.Location = new Point(14, 29);
                    //observationpanel.Height = 200;
                    DataTable dt1 = this.cntrl.show_observation();
                    observationgrid.DataSource = dt1;
                }
                else if (cmb_clinicalfinding.Text == "Diagnosis")
                {
                    diagnosispanel.Visible = true;
                    DataTable dt3 = this.cntrl.show_diagno();
                    diagnosisgrid.DataSource = dt3;
                    label11.Hide();
                    diagtext.Hide();
                    diagsavebut.Hide();
                    diagcancel.Hide();
                    diagtext.Text = "";
                    Lab_Diag_Search.Show();
                    diagsearchtext.Show();
                    diagadd.Show();
                    btn_imprt_diagno.Show();
                    Lab_Diag_Search.Location = new Point(6, 13);
                    diagsearchtext.Location = new Point(62, 8);
                    diagnosisgrid.Location = new Point(3, 37);
                    diagnosispanel.Location = new Point(14, 29);
                    //diagnosispanel.Height = 200;
                }
                else if (cmb_clinicalfinding.Text == "Complaints")
                {
                    complaintpanel.Visible = true;
                    compsave.Visible = true;
                    DataTable dt = this.cntrl.show_compl();
                    complaintgrid.DataSource = dt;
                    lad_compAddNew.Hide();
                    comptextbox.Hide();
                    compsave.Hide();
                    compcancel.Hide();
                    lab_compSearch.Show();
                    compsearchtext.Show();
                    compadd.Show();
                    btn_imprt_complnt.Show();
                    lab_compSearch.Location = new Point(6, 13);
                    compsearchtext.Location = new Point(62, 8);
                    complaintgrid.Location = new Point(3, 37);
                    complaintpanel.Location = new Point(14, 29);
                    //complaintpanel.Height = 200;
                }
                else if (cmb_clinicalfinding.Text == "Notes")
                {
                    notespanel.Visible = true; 
                    DataTable dt4 = this.cntrl.show_note();
                    notegrid.DataSource = dt4;
                    label14.Hide();
                    notetextbox.Hide();
                    notesavebut.Hide();
                    notecancel.Hide();
                    notetextbox.Text = "";
                    lab_NotesSearch.Show();
                    notesearchtext.Show();
                    noteadd.Show();
                    btn_imprt_notes.Show();
                    lab_NotesSearch.Location = new Point(6, 13);
                    notesearchtext.Location = new Point(62, 8);
                    notegrid.Location = new Point(3, 37);
                    notespanel.Location = new Point(14, 29);
                    //notespanel.Height = 200;
                }
                else if (cmb_clinicalfinding.Text == "Allergies")
                {
                    pnl_Allergies.Visible = true;
                    DataTable dt4 = this.cntrl.show_allerg();
                    AllergyGrid.DataSource = dt4;
                    label1.Hide();
                    txtAllergy.Hide();
                    btnSaveAllerg.Hide();
                    btnCancelAllrg.Hide();
                    txtAllergy.Text = "";
                    lab_AllergySearch.Show();
                    txtSearchAllergy.Show();
                    btnAddAllerg.Show();
                    btnImprtAllerg.Show();
                    lab_AllergySearch.Location = new Point(6, 13);
                    txtSearchAllergy.Location = new Point(62, 8);
                    AllergyGrid.Location = new Point(3, 37);
                    pnl_Allergies.Location = new Point(14, 29);
                    //notespanel.Height = 200;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var form2 = new PappyjoeMVC.View.Clinical_Findings();
            form2.doctor_id = doctor_id;
            form2.patient_id = patient_id;
            openform(form2);
        }

        Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
        string FileName;
        private void btn_imprt_complnt_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Excel File to Import";
                ofd.FileName = "";
                ofd.Filter = "Excel File|*.xlsx;*.xls";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FileName = ofd.FileName;
                    if (FileName.Trim() != "")
                    {
                        xlApp = new Microsoft.Office.Interop.Excel.Application();
                        xlWorkBook = xlApp.Workbooks.Open(FileName);
                        xlWorkSheet = xlWorkBook.Worksheets["Sheet1"];
                        int iRow;
                        if (xlWorkSheet.Cells[1, 1].value == "Complaints")
                        {
                            for (iRow = 2; iRow <= xlWorkSheet.Rows.Count; iRow++)
                            {
                                if (xlWorkSheet.Cells[iRow, 1].value == null)
                                {
                                    break;
                                }
                                else
                                {
                                    string complaints = "";
                                    complaints = Convert.ToString(xlWorkSheet.Cells[iRow, 1].value);
                                    DataTable checkdatacc = this.cntrl.checkdataAcc(complaints);
                                    if (checkdatacc.Rows.Count == 0)
                                    {
                                        this.cntrl.insert_compl(complaints);
                                    }
                                }
                            }
                            DataTable dt = this.cntrl.show_compl();
                            complaintgrid.DataSource = dt;
                            xlWorkBook.Close();
                            xlApp.Quit();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet);
                            MessageBox.Show("Successfully Imported !!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("The Excel sheet data is not in the standard format", "Format mismatch", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_imprt_obsrvtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Excel File to Import";
                ofd.FileName = "";
                ofd.Filter = "Excel File|*.xlsx;*.xls";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FileName = ofd.FileName;
                    if (FileName.Trim() != "")
                    {
                        xlApp = new Microsoft.Office.Interop.Excel.Application();
                        xlWorkBook = xlApp.Workbooks.Open(FileName);
                        xlWorkSheet = xlWorkBook.Worksheets["Sheet1"];
                        int iRow;
                        if (xlWorkSheet.Cells[1, 1].value == "Observations")
                        {
                            for (iRow = 2; iRow <= xlWorkSheet.Rows.Count; iRow++)
                            {
                                if (xlWorkSheet.Cells[iRow, 1].value == null)
                                {
                                    break;
                                }
                                else
                                {
                                    string obser = "";
                                    obser = Convert.ToString(xlWorkSheet.Cells[iRow, 1].value);
                                    DataTable checkdataOB = this.cntrl.checkdataOB(obser);
                                    if (checkdataOB.Rows.Count == 0)
                                    {
                                        this.cntrl.insert_Observ(obser);
                                    }
                                }
                            }
                            DataTable dt1 = this.cntrl.show_observation();
                            observationgrid.DataSource = dt1;
                            xlWorkBook.Close();
                            xlApp.Quit();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet);
                            MessageBox.Show("Successfully Imported !!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("The Excel sheet data is not in the standard format", "Format mismatch", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_imprt_investgtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Excel File to Import";
                ofd.FileName = "";
                ofd.Filter = "Excel File|*.xlsx;*.xls";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FileName = ofd.FileName;
                    if (FileName.Trim() != "")
                    {
                        xlApp = new Microsoft.Office.Interop.Excel.Application();
                        xlWorkBook = xlApp.Workbooks.Open(FileName);
                        xlWorkSheet = xlWorkBook.Worksheets["Sheet1"];
                        int iRow;
                        if (xlWorkSheet.Cells[1, 1].value == "Investigations")
                        {
                            for (iRow = 2; iRow <= xlWorkSheet.Rows.Count; iRow++)
                            {
                                if (xlWorkSheet.Cells[iRow, 1].value == null)
                                {
                                    break;
                                }
                                else
                                {
                                    string invest = "";
                                    invest = Convert.ToString(xlWorkSheet.Cells[iRow, 1].value);
                                    DataTable checkdataINVEST = this.cntrl.CheckInvest(invest);
                                    if (checkdataINVEST.Rows.Count == 0)
                                    {
                                        this.cntrl.investigation_insert(invest);
                                    }
                                }
                            }
                            DataTable dt2 = this.cntrl.Show_investigation();
                            investigationgrid.DataSource = dt2;
                            xlWorkBook.Close();
                            xlApp.Quit();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet);
                            MessageBox.Show("Successfully Imported !!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("The Excel sheet data is not in the standard format", "Format mismatch", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_imprt_diagno_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Excel File to Import";
                ofd.FileName = "";
                ofd.Filter = "Excel File|*.xlsx;*.xls";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FileName = ofd.FileName;
                    if (FileName.Trim() != "")
                    {
                        xlApp = new Microsoft.Office.Interop.Excel.Application();
                        xlWorkBook = xlApp.Workbooks.Open(FileName);
                        xlWorkSheet = xlWorkBook.Worksheets["Sheet1"];
                        int iRow;
                        if (xlWorkSheet.Cells[1, 1].value == "Diagnosis")
                        {
                            for (iRow = 2; iRow <= xlWorkSheet.Rows.Count; iRow++)
                            {
                                if (xlWorkSheet.Cells[iRow, 1].value == null)
                                {
                                    break;
                                }
                                else
                                {
                                    string diag = "";
                                    diag = Convert.ToString(xlWorkSheet.Cells[iRow, 1].value);
                                    DataTable checkdataDIAG = this.cntrl.CheckdataDiag(diag);
                                    if (checkdataDIAG.Rows.Count == 0)
                                    {
                                        this.cntrl.Insert_diagno(diag);
                                    }
                                }
                            }
                            DataTable dt3 = this.cntrl.show_diagno();
                            diagnosisgrid.DataSource = dt3;
                            xlWorkBook.Close();
                            xlApp.Quit();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet);
                            MessageBox.Show("Successfully Imported !!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("The Excel sheet data is not in the standard format", "Format mismatch", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_imprt_notes_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Excel File to Import";
                ofd.FileName = "";
                ofd.Filter = "Excel File|*.xlsx;*.xls";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FileName = ofd.FileName;
                    if (FileName.Trim() != "")
                    {
                        xlApp = new Microsoft.Office.Interop.Excel.Application();
                        xlWorkBook = xlApp.Workbooks.Open(FileName);
                        xlWorkSheet = xlWorkBook.Worksheets["Sheet1"];
                        int iRow;
                        if (xlWorkSheet.Cells[1, 1].value == "Notes")
                        {
                            for (iRow = 2; iRow <= xlWorkSheet.Rows.Count; iRow++)
                            {
                                if (xlWorkSheet.Cells[iRow, 1].value == null)
                                {
                                    break;
                                }
                                else
                                {
                                    string note = "";
                                    note = Convert.ToString(xlWorkSheet.Cells[iRow, 1].value);
                                    DataTable checkdataNOTE = this.cntrl.checkdataNote(note);
                                    if (checkdataNOTE.Rows.Count == 0)
                                    {
                                        this.cntrl.insert_note(note);
                                    }
                                }
                            }
                            DataTable dt4 = this.cntrl.show_note();
                            notegrid.DataSource = dt4;
                            xlWorkBook.Close();
                            xlApp.Quit();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet);
                            MessageBox.Show("Successfully Imported !!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("The Excel sheet data is not in the standard format", "Format mismatch", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            grmedical.Visible = false;
            txtMedHistory.Visible = true;
            dataGridViewmedical.Visible = true;
            btnMED_Add.Visible = true;
            btnMED_Cancle.Visible = true;
            DataTable dt = this.cntrl.load_medical();
            dataGridViewmedical.DataSource = dt;
            btnAddNew.Visible = false;
        }

        private void btnMED_Add_Click(object sender, EventArgs e)
        {
            //medhisStatus = 1;
            DataTable dtb = this.cntrl.check_medical(txtMedHistory.Text);
            insertMED(dtb);
            DataTable dt = this.cntrl.load_medical();
            fill_grid_medical(dt);
            //grmedical.DataSource = dt;
        }
        public void insertMED(DataTable checkdatacc)
        {
            try
            {
                if (checkdatacc.Rows.Count > 0)
                {
                    MessageBox.Show("Medical History " + txtMedHistory.Text + " already exist");
                }
                else
                {
                    if (txtMedHistory.Text != "")
                    {
                        this.cntrl.insert_medical(txtMedHistory.Text);
                        txtMedHistory.Text = "";
                        btnAddNew.Visible = true;
                        grmedical.Visible = true;
                        txtMedHistory.Visible = false;
                        dataGridViewmedical.Visible = false;
                        btnMED_Add.Visible = false;
                        btnMED_Cancle.Visible = false;
                    }
                    else
                    { }
                }
            }
            catch { }
        }

        private void btnMED_Cancle_Click(object sender, EventArgs e)
        {
            DataTable dt = this.cntrl.load_medical();
            fill_grid_medical(dt);
            // grmedical.DataSource = dt;
            btnAddNew.Visible = true;
            grmedical.Visible = true;
            txtMedHistory.Visible = false;
            dataGridViewmedical.Visible = false;
            btnMED_Add.Visible = false;
            btnMED_Cancle.Visible = false;
        }

        private void grmedical_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            grmedical.CurrentCell.Selected = true;
            int r = e.RowIndex;
            // if(grmedical.CurrentCell.Selected==true)
            //{
                idobs = grmedical.Rows[r].Cells[0].Value.ToString();
                DataTable dt1 = this.cntrl.medical_cell(idobs);
                bool entryFound = false;
                if (dt1.Rows.Count > 0)
                {
                    var value = dt1.Rows[0][1].ToString().Trim();
                    foreach (DataGridViewRow row1 in dgv_medical.Rows)
                    {
                        object val1 = row1.Cells[0].Value;
                        object val2 = row1.Cells[1].Value;
                        if (val2 != null && val2.ToString() == value)
                        {
                            this.dgv_medical.Rows[e.RowIndex].Selected = true;
                            entryFound = true;
                            break;
                        }
                    }
                    if (!entryFound)
                    {
                        dgv_medical.Rows.Add(dt1.Rows[0][0].ToString(), dt1.Rows[0][1].ToString());
                        dgv_medical.Rows[dgv_medical.Rows.Count - 1].Height = 30;
                        dgv_medical.Rows[dgv_medical.Rows.Count - 1].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                        del1.ImageLayout = DataGridViewImageCellLayout.Normal;
                    }
                else
                {
                    if (grmedical.CurrentCell.Selected == false)
                    {
                        dgv_medical.Rows.RemoveAt(e.RowIndex);

                    }
                }
                }
            //}
            //else
            //{
            //    dgv_medical.Rows.RemoveAt(r);
            //}





                   
            //    }
            //    else
            //    {
            //        dgv_medical.Rows.RemoveAt(r);
            //    }
            //}


            //int r = e.RowIndex;
            

        }

        private void grmedical_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //grmedical.CurrentCell.Selected = false;
            //int r = grmedical.CurrentRow.Index;
            //bool entryFound = false;
            //idobs = grmedical.Rows[r].Cells[0].Value.ToString();
            //var value = idobs.Trim();
            //foreach (DataGridViewRow row in dgv_medical.Rows)
            //{
            //    object val1 = row.Cells[0].Value;
            //    object val2 = row.Cells[1].Value;
            //    if (val2 != null && val2.ToString() == value)
            //    {
            //        this.dgv_medical.Rows[r].Selected = true;
            //        entryFound = true;
            //        break;
            //    }
            //}
            //if (entryFound)
            //{
            //    dgv_medical.Rows.RemoveAt(r);
            //    //dgv_medical.Rows[dgv_medical.Rows.Count - 1].Height = 30;
            //    //dgv_medical.Rows[dgv_medical.Rows.Count - 1].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
            //    //del1.ImageLayout = DataGridViewImageCellLayout.Normal;
            //}
        }

        private void panl_heading_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgv_medical_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_medical.Rows.Count > 1)
                {
                    if (e.ColumnIndex == 2)
                    {
                        if (dgv_medical.CurrentRow.Cells[1].Value != null && dgv_medical.CurrentRow.Cells[1].Value.ToString() != "")
                               dgv_medical.Rows.RemoveAt(this.dgv_medical.SelectedRows[0].Index);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_meditation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_meditation.Rows.Count > 1)
                {
                    if (e.ColumnIndex == 2)
                    {
                        if(dgv_meditation.CurrentRow.Cells[1].Value!=null && dgv_meditation.CurrentRow.Cells[1].Value.ToString()!="")
                             dgv_meditation.Rows.RemoveAt(this.dgv_meditation.SelectedRows[0].Index);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_discharge_advice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_discharge_advice.Rows.Count > 1)
                {
                    if (e.ColumnIndex == 2)
                    {
                        if (dgv_discharge_advice.CurrentRow.Cells[1].Value != null && dgv_discharge_advice.CurrentRow.Cells[1].Value.ToString() != "")
                                dgv_discharge_advice.Rows.RemoveAt(this.dgv_discharge_advice.SelectedRows[0].Index);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void notesgrid1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

        }

        private void dgv_meditation_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgv_meditation.Rows[e.RowIndex].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
            }
            //dgv_meditation.Rows[dgv_meditation.CurrentRow.Index].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
        }

        private void grmedical_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void grmedical_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //idobs = grmedical.Rows[e.RowIndex].Cells[0].Value.ToString();
            //DataTable dt1 = this.cntrl.medical_cell(idobs);
            //bool entryFound = false;
            //if (dt1.Rows.Count > 0)
            //{
            //    var value = dt1.Rows[0][1].ToString().Trim();
            //    foreach (DataGridViewRow row1 in dgv_medical.Rows)
            //    {
            //        object val1 = row1.Cells[0].Value;
            //        object val2 = row1.Cells[1].Value;
            //        if (val2 != null && val2.ToString() == value)
            //        {
            //            this.dgv_medical.Rows[e.RowIndex].Selected = true;
            //            entryFound = true;
            //            break;
            //        }
            //    }
            //    if (!entryFound)
            //    {
            //        dgv_medical.Rows.Add(dt1.Rows[0][0].ToString(), dt1.Rows[0][1].ToString());
            //        dgv_medical.Rows[dgv_medical.Rows.Count - 1].Height = 30;
            //        dgv_medical.Rows[dgv_medical.Rows.Count - 1].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
            //        del1.ImageLayout = DataGridViewImageCellLayout.Normal;
            //    }
            //    else
            //    {
            //if (grmedical.CurrentCell.Selected == false)
            //{
            //            dgv_medical.Rows.RemoveAt(e.RowIndex);

            //        }
                //}
            //}
        }

        private void btnAddAllerg_Click(object sender, EventArgs e)
        {
            label1.Show();
            txtAllergy.Show();
            btnSaveAllerg.Show();
            btnCancelAllrg.Show();
            txtAllergy.Text = "";
            lab_AllergySearch.Hide();
            txtSearchAllergy.Hide();
            btnAddAllerg.Hide();
            btnImprtAllerg.Hide();
            AllergyGrid.Location = new Point(3, 65);
        }

        private void btnSaveAllerg_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable checkdataAllerg = this.cntrl.checkdataAllerg(txtAllergy.Text.Replace("'", ""));
                if (checkdataAllerg.Rows.Count > 0)
                {
                    MessageBox.Show("Record " + txtAllergy.Text + "  already exists..", "Duplication encountered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (txtAllergy.Text != "")
                    {
                        this.cntrl.insert_Allergy(txtAllergy.Text);
                        DataTable dt1 = this.cntrl.show_allerg();
                        AllergyGrid.DataSource = dt1;
                        label1.Hide();
                        txtAllergy.Hide();
                        btnSaveAllerg.Hide();
                        btnCancelAllrg.Hide();
                        txtAllergy.Text = "";
                        lab_AllergySearch.Show();
                        txtSearchAllergy.Show();
                        btnAddAllerg.Show();
                        btnImprtAllerg.Show();
                        lab_AllergySearch.Location = new Point(6, 13);
                        txtSearchAllergy.Location = new Point(62, 8);
                        AllergyGrid.Location = new Point(3, 37);
                    }
                    else
                    {
                        MessageBox.Show("Enter the data..!!", "Data Required..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        obsertextbox.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelAllrg_Click(object sender, EventArgs e)
        {
            label1.Hide();
            txtAllergy.Hide();
            btnSaveAllerg.Hide();
            btnCancelAllrg.Hide();
            txtAllergy.Text = "";
            lab_AllergySearch.Show();
            txtSearchAllergy.Show();
            btnAddAllerg.Show();
            btnImprtAllerg.Show();
            lab_AllergySearch.Location = new Point(6, 13);
            txtSearchAllergy.Location = new Point(62, 8);
            AllergyGrid.Location = new Point(3, 37);
        }

        private void txtSearchAllergy_MouseClick(object sender, MouseEventArgs e)
        {
            txtSearchAllergy.Text = null;
        }

        private void txtSearchAllergy_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                DataTable dt1 = this.cntrl.allergysearch(txtSearchAllergy.Text);
                AllergyGrid.DataSource = dt1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearchAllergy_Click(object sender, EventArgs e)
        {
            txtSearchAllergy.Clear();
        }

        private void AllergyGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = e.RowIndex;
                idobs = AllergyGrid.Rows[r].Cells[0].Value.ToString();
                DataTable dt1 = this.cntrl.allergy_cell(idobs);
                bool entryFound = false;
                if (dt1.Rows.Count > 0)
                {
                    var value = dt1.Rows[0][1].ToString().Trim();
                    foreach (DataGridViewRow row in dgvAllerg.Rows)
                    {
                        object val1 = row.Cells[0].Value;
                        object val2 = row.Cells[1].Value;
                        if (val2 != null && val2.ToString() == value)
                        {
                            this.dgvAllerg.Rows[e.RowIndex].Selected = true;
                            entryFound = true;
                            break;
                        }
                    }
                    if (!entryFound)
                    {
                        dgvAllerg.Rows.Add(dt1.Rows[0][0].ToString(), dt1.Rows[0][1].ToString());
                        dgvAllerg.Rows[dgvAllerg.Rows.Count - 1].Height = 30;
                        dgvAllerg.Rows[dgvAllerg.Rows.Count - 1].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                        del5.ImageLayout = DataGridViewImageCellLayout.Normal;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvAllerg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvAllerg.Rows.Count > 1)
                {
                    if (e.ColumnIndex == 2)
                    {
                        if (dgvAllerg.CurrentRow.Cells[1].Value != null && dgvAllerg.CurrentRow.Cells[1].Value.ToString() != "")
                            dgvAllerg.Rows.RemoveAt(this.dgvAllerg.SelectedRows[0].Index);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvAllerg_Click(object sender, EventArgs e)
        {
            cmb_clinicalfinding.SelectedIndex = 5;
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

        private void dgvNursesNote_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if(e.RowIndex>=0)
            //{
            //    dgvNursesNote.Rows[e.RowIndex+1].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
            //}
        }

        private void dgvNursesNote_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvNursesNote.Rows[e.RowIndex ].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
            }
        }

        private void dgv_discharge_advice_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgv_discharge_advice.Rows[e.RowIndex].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
            }
        }

        private void dgv_meditation_Leave(object sender, EventArgs e)
        {
            if (dgv_meditation.Rows.Count> 0)
            {
                int k = dgv_meditation.Rows.Count-1;
                dgv_meditation.Rows[k].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
            }
        }

        private void dgv_discharge_advice_Leave(object sender, EventArgs e)
        {
            if (dgv_discharge_advice.Rows.Count > 0)
            {
                int k = dgv_discharge_advice.Rows.Count-1;
                dgv_discharge_advice.Rows[k].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
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

        private void dgv_medical_Leave(object sender, EventArgs e)
        {
            //if (dgv_medical.Rows.Count > 0)
            //{
            //    int k = dgv_medical.Rows.Count - 1;
            //    dgv_medical.Rows[k].Cells[2].Value = PappyjoeMVC.Properties.Resources.deleteicon;
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "SAVE CLINICAL FINDINGS")
                {
                    if (investigationgrid1.Rows[0].Cells[1].Value != null && investigationgrid1.Rows[0].Cells[1].Value.ToString() != "" || diagnosisgrid1.Rows[0].Cells[1].Value != null && diagnosisgrid1.Rows[0].Cells[1].Value.ToString() != "" ||
                        notesgrid1.Rows[0].Cells[1].Value != null && notesgrid1.Rows[0].Cells[1].Value.ToString() != "" || observationgrid1.Rows[0].Cells[1].Value != null && observationgrid1.Rows[0].Cells[1].Value.ToString() != ""
                         || complaintgrid1.Rows[0].Cells[1].Value != null && complaintgrid1.Rows[0].Cells[1].Value.ToString() != "" ||
                        dgvAllerg.Rows[0].Cells[1].Value != null && dgvAllerg.Rows[0].Cells[1].Value.ToString() != "" ||
                        dgvNursesNote.Rows[0].Cells[1].Value != null && dgvNursesNote.Rows[0].Cells[1].Value.ToString() != "")
                    {
                        int treat = 0;
                        string dt = this.cntrl.Get_DoctorId(Cmb_doctor.Text);
                        if (dt != "")
                        {
                            this.cntrl.insertInto_clinical_findings(patient_id, dt.ToString(), dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                            string treatment = this.cntrl.MaxId_clinic_findings();
                            if (Convert.ToInt32(treatment) > 0)
                            {
                                treat = int.Parse(treatment);
                            }
                            else
                            {
                                treat = 1;
                            }
                            try
                            {
                                if (investigationgrid1.Rows.Count > 0)
                                {
                                    for (int i = 0; i < investigationgrid1.Rows.Count; i++)
                                    {
                                        if (investigationgrid1[1, i].Value != null)
                                        {
                                            string one = investigationgrid1[1, i].Value.ToString();
                                            this.cntrl.insrtto_investi(treat, one);
                                        }
                                    }
                                }
                                if (diagnosisgrid1.Rows.Count > 0)
                                {
                                    for (int i = 0; i < diagnosisgrid1.Rows.Count; i++)
                                    {
                                        if (diagnosisgrid1[1, i].Value != null)
                                        {
                                            string one = diagnosisgrid1[1, i].Value.ToString();
                                            this.cntrl.insrtto_diagno(treat, one);

                                        }
                                    }
                                }
                                if (notesgrid1.Rows.Count > 0)
                                {
                                    for (int i = 0; i < notesgrid1.Rows.Count; i++)
                                    {
                                        if (notesgrid1[1, i].Value != null)
                                        {
                                            string one = notesgrid1[1, i].Value.ToString();
                                            this.cntrl.insrtto_note(treat, one);
                                        }
                                    }
                                }
                                if (observationgrid1.Rows.Count > 0)
                                {
                                    for (int i = 0; i < observationgrid1.Rows.Count; i++)
                                    {
                                        if (observationgrid1[1, i].Value != null)
                                        {
                                            string one = observationgrid1[1, i].Value.ToString();
                                            this.cntrl.insrtto_obser(treat, one);
                                        }
                                    }
                                }
                                if (dgvAllerg.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dgvAllerg.Rows.Count; i++)
                                    {
                                        if (dgvAllerg[1, i].Value != null)
                                        {
                                            string one = dgvAllerg[1, i].Value.ToString();
                                            this.cntrl.insrtto_allergy(treat, one);
                                        }
                                    }
                                }
                                if (complaintgrid1.Rows.Count > 0)
                                {
                                    for (int i = 0; i < complaintgrid1.Rows.Count; i++)
                                    {
                                        if (complaintgrid1[1, i].Value != null)
                                        {
                                            string one = complaintgrid1[1, i].Value.ToString();
                                            this.cntrl.insrtto_chief_comp(treat, one);
                                        }
                                    }
                                }
                                if (dgvNursesNote.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dgvNursesNote.Rows.Count; i++)
                                    {
                                        if (dgvNursesNote[1, i].Value != null)
                                        {

                                            string one = dgvNursesNote[1, i].Value.ToString();
                                            this.cntrl.insrtto_nurseNote(treat, one);
                                        }
                                    }
                                }
                                //if (medhisStatus == 0)
                                //{
                                for (int i = 0; i < dgv_medical.Rows.Count; i++)
                                {
                                    if (dgv_medical[1, i].Value != null)
                                    {
                                        string one = dgv_medical[1, i].Value.ToString();
                                        DataTable dtr = this.cntrl.check_medical_(patient_id, one);
                                        if (dtr.Rows.Count > 0)
                                        {

                                        }
                                        else
                                        {
                                            this.cntrl.save_medical(patient_id, dgv_medical.Rows[i].Cells[1].Value.ToString(), treat.ToString());
                                            //this.cntrl.save_medical(patient_id, dgv_medical.Rows[i].Cells[1].Value.ToString());
                                        }
                                        //string one = dgv_medical[1, i].Value.ToString();
                                        //this.cntrl.save_medical(patient_id, dgv_medical.Rows[i].Cells[1].Value.ToString(), treat.ToString());
                                    }
                                }

                                //foreach (DataGridViewRow row in dgv_medical.Rows)
                                //    {
                                //        string d = row.Cells[1].Value.ToString();
                                //        //DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[1];
                                //        //if (chk.Selected == true)
                                //        //{
                                //            this.cntrl.save_medical(patient_id, row.Cells[1].Value.ToString());
                                //        //}
                                //    }
                                ////}
                                //else
                                //{
                                //    foreach (DataGridViewRow row in dgv_medical.Rows)
                                //    {
                                //        string d = row.Cells[1].Value.ToString();
                                //        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                                //        if (chk.Selected == true)
                                //        {
                                //            this.cntrl.save_medical(patient_id, row.Cells[0].Value.ToString());
                                //        }
                                //    }
                                    
                                //}
                                if (dgv_meditation.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dgv_meditation.Rows.Count; i++)
                                    {
                                        if (dgv_meditation[1, i].Value != null)
                                        {
                                            string one = dgv_meditation[1, i].Value.ToString();
                                            this.cntrl.insrtto_pt_meditation(treat, one);
                                        }
                                    }
                                }
                                if (dgv_discharge_advice.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dgv_discharge_advice.Rows.Count; i++)
                                    {
                                        if (dgv_discharge_advice[1, i].Value != null)
                                        {
                                            string one = dgv_discharge_advice[1, i].Value.ToString();
                                            this.cntrl.insrtto_pt_advice(treat, one);
                                        }
                                    }
                                }
                                //this.cntrl.save_details(treat.ToString(), txt_meditation.Text, txt_discharge_adv.Text);
                                string dt1 = DateTime.Now.Date.ToString("yyyy-MM-dd");
                                DateTime Timeonly = DateTime.Now;
                                this.cntrl.save_log(doctor_id, "Clinical Findings", "Adds Clinical Findings", dt1, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Add", treat.ToString());
                                var form2 = new PappyjoeMVC.View.Clinical_Findings();
                                form2.doctor_id = doctor_id;
                                form2.patient_id = patient_id;
                                openform(form2);
                            }
                            catch { }
                        }
                        else
                        {
                            MessageBox.Show("Choose a doctor first", "Doctor missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Click Some Clinical Notes From Right First And Then try again...", "Add Clinical Notes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (investigationgrid1.Rows[0].Cells[1].Value != null && investigationgrid1.Rows[0].Cells[1].Value.ToString() != "" || diagnosisgrid1.Rows[0].Cells[1].Value != null && diagnosisgrid1.Rows[0].Cells[1].Value.ToString() != "" ||
                        notesgrid1.Rows[0].Cells[1].Value != null && notesgrid1.Rows[0].Cells[1].Value.ToString() != "" || observationgrid1.Rows[0].Cells[1].Value != null && observationgrid1.Rows[0].Cells[1].Value.ToString() != ""
                         || complaintgrid1.Rows[0].Cells[1].Value != null && complaintgrid1.Rows[0].Cells[1].Value.ToString() != "" || dgvAllerg.Rows[0].Cells[1].Value != null && dgvAllerg.Rows[0].Cells[1].Value.ToString() != "" || dgvNursesNote.Rows[0].Cells[1].Value != null && dgvNursesNote.Rows[0].Cells[1].Value.ToString() != "")
                    {
                        string dt = this.cntrl.Get_DoctorId(Cmb_doctor.Text);
                        if (dt != "")
                        {
                            this.cntrl.Update_clinical_findings(patient_id, dt.ToString(), clinic_id);
                        }
                        else
                        {
                            MessageBox.Show("Choose a doctor first", "Doctor missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        this.cntrl.Update_date_of_clinical(dateTimePicker1.Value.ToString("yyyy-MM-dd"), patient_id, clinic_id);
                        this.cntrl.del_investi(clinic_id);
                        this.cntrl.del_diagno(clinic_id);
                        this.cntrl.del_note(clinic_id);
                        this.cntrl.del_obser(clinic_id);
                        this.cntrl.del_allergy(clinic_id);
                        this.cntrl.del_chiefComp(clinic_id);
                        this.cntrl.del_meditation(clinic_id);
                        this.cntrl.del_advice(clinic_id);
                        //this.cntrl.del_medialhistory(patient_id);

                        this.cntrl.del_medialhistory(clinic_id);
                        for (int i = 0; i < investigationgrid1.Rows.Count; i++)
                        {
                            if (investigationgrid1[1, i].Value != null)
                            {
                                this.cntrl.Add_investi(clinic_id, investigationgrid1[1, i].Value.ToString());
                            }
                        }
                        for (int i = 0; i < diagnosisgrid1.Rows.Count; i++)
                        {

                            if (diagnosisgrid1[1, i].Value != null)
                            {
                                this.cntrl.Add_diagno(clinic_id, diagnosisgrid1[1, i].Value.ToString());
                            }
                        }
                        for (int i = 0; i < notesgrid1.Rows.Count; i++)
                        {
                            if (notesgrid1[1, i].Value != null)
                            {
                                this.cntrl.Add_note(clinic_id, notesgrid1[1, i].Value.ToString());
                            }
                        }
                        for (int i = 0; i < observationgrid1.Rows.Count; i++)
                        {
                            if (observationgrid1[1, i].Value != null)
                            {
                                this.cntrl.Add_observ(clinic_id, observationgrid1[1, i].Value.ToString());
                            }
                        }
                        for (int i = 0; i < dgvAllerg.Rows.Count; i++)
                        {
                            if (dgvAllerg[1, i].Value != null)
                            {
                                this.cntrl.Add_allergy(clinic_id, dgvAllerg[1, i].Value.ToString());
                            }
                        }
                        for (int i = 0; i < complaintgrid1.Rows.Count; i++)
                        {
                            if (complaintgrid1[1, i].Value != null)
                            {
                                this.cntrl.Add_cheifComp(clinic_id, complaintgrid1[1, i].Value.ToString());
                            }
                        }

                        for (int i = 0; i < dgv_medical.Rows.Count; i++)
                        {
                            if (dgv_medical[1, i].Value != null)
                            {
                                this.cntrl.save_medical(patient_id, dgv_medical[1, i].Value.ToString(), clinic_id);
                            }
                        }

                        for (int i = 0; i < dgv_meditation.Rows.Count; i++)
                        {
                            if (dgv_meditation[1, i].Value != null)
                            {
                                this.cntrl.insrtto_pt_meditation(Convert.ToInt32( clinic_id), dgv_meditation[1, i].Value.ToString());
                            }
                        }

                        for (int i = 0; i < dgv_discharge_advice.Rows.Count; i++)
                        {
                            if (dgv_discharge_advice[1, i].Value != null)
                            {
                                this.cntrl.insrtto_pt_advice(Convert.ToInt32(clinic_id), dgv_discharge_advice[1, i].Value.ToString());
                            }
                        }
                        string dt1 = DateTime.Now.Date.ToString("yyyy-MM-dd");
                        DateTime Timeonly = DateTime.Now;
                        this.cntrl.save_log(doctor_id, "Clinical Findings", " Edit Clinical Findings", dt1, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Edit", clinic_id);
                        var form2 = new PappyjoeMVC.View.Clinical_Findings();
                        form2.patient_id = patient_id;
                        form2.doctor_id = doctor_id;
                        openform(form2);
                    }
                    else
                    {
                        MessageBox.Show("Select Clinical Notes From Right Side....", "Clinical Notes Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void complaintgrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                complaintgrid.ClearSelection();
                var relativeMousePosition = complaintgrid.PointToClient(Cursor.Position);
                this.ConMSp_gridDelete_.Show(complaintgrid, relativeMousePosition);
                complaintgrid.Rows[e.RowIndex].Selected = true;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (complaintgrid.Rows.Count > 0)
                {
                    int i = 0;
                    int row = Convert.ToInt32(complaintgrid.Rows[complaintgrid.CurrentRow.Index].Cells["Column1"].Value);
                    int rowindex = complaintgrid.SelectedCells[0].RowIndex;
                    if (row != null && rowindex >= 0)
                    {
                        complaintgrid.Rows.RemoveAt(rowindex);
                        i = this.cntrl.Del_Complaints(row);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (complaintgrid.Rows.Count > 0)
                {
                    rowvalue = Convert.ToInt32(complaintgrid.Rows[complaintgrid.CurrentRow.Index].Cells["Column1"].Value);
                    int rowindex = complaintgrid.SelectedCells[0].RowIndex;
                    DataTable dtb = new DataTable();
                    if (rowvalue != null && rowindex >= 0)
                    {
                        dtb = this.cntrl.COMP(rowvalue);
                        if (dtb.Rows.Count > 0)
                        {
                            compsave.Visible = true;
                            compcancel.Visible = true;
                            comptextbox.Visible = true;
                            compadd.Visible = false;
                            btn_imprt_complnt.Visible = false;
                            compsearchtext.Visible = false;
                            lab_compSearch.Visible = false;
                            compsave.Text = "Update";
                            comptextbox.Text = dtb.Rows[0]["name"].ToString();
                            complaintgrid.Location = new Point(3, 65);
                        }
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
