using MySql.Data.MySqlClient;
using PappyjoeMVC.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PappyjoeMVC.Model;
namespace PappyjoeMVC.View
{
    public partial class Add_Dental_Treatment_Plan : Form
    {
        Add_Treatmentplan_controller cntrl = new Add_Treatmentplan_controller();
        public string doctor_id = ""; public string patient_id = "";
        public static DataTable dtb_toothtable = new DataTable();
        string id;
        public string tooth_imgno = "";
        public string surface_imgno;
        Decimal discounttotal = 0;
        public int rowindex;
        public string[] tooth = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        public Add_Dental_Treatment_Plan()
        {
            InitializeComponent();
        }
        private void AddTreatmentPlan_Load(object sender, EventArgs e)
        {
        
            try
            {
                string docnam = this.cntrl.Get_DoctorName(doctor_id);
                Lab_msg.Top = 300;
                create_datatable();
                panl_TreatmentAdd.Hide();
                disclick.Visible = false;
                Cmb_Discount.Hide();
                DGV_Procedure.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGV_Procedure.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DGV_Procedure.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DGV_Procedure.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                proceduretreatgrid.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                System.Data.DataTable rs_patients = this.cntrl.Get_Patient_Details(patient_id);
                if (rs_patients.Rows[0]["pt_name"].ToString() != "")
                {
                    linkLabel_Name.Text = rs_patients.Rows[0]["pt_name"].ToString();
                }
                if (rs_patients.Rows[0]["pt_id"].ToString() != "")
                {
                    linkLabel_id.Text = rs_patients.Rows[0]["pt_id"].ToString();
                }

                DataTable dt_pt = this.cntrl.Get_all_procedures();
                fill_proceduregrid(dt_pt);
                DataTable dtdr = this.cntrl.get_all_doctorname();
                Cmb_Doctor.DisplayMember = "doctor_name";
                Cmb_Doctor.ValueMember = "id";
                Cmb_Doctor.DataSource = dtdr;
                Cmb_Doctor.SelectedIndex = 0;
                if (doctor_id != "0")
                {
                    int dr_index = 0;
                    if (dtdr.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtdr.Rows.Count; j++)
                        {
                            if (dtdr.Rows[j]["id"].ToString() == doctor_id)
                            {
                                dr_index = j;
                            }
                        }
                        Cmb_Doctor.SelectedIndex = dr_index;
                    }
                }
                foreach (DataGridViewColumn column in DGV_Procedure.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                foreach (DataGridViewColumn column in proceduretreatgrid.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                txt_procedure.Visible = false;
                txt_Cost.Visible = false;
                addbut.Visible = true;
                btn_ProcedureClose.Visible = false;
                btn_SaveProcedure.Visible = false;
                disclick.Visible = false;
                Lab_Procedure.Hide();
                lab_Cost.Hide();
                panl_teeth.Hide();
                //updation 15/12/2020
                DataTable dt_enablecost = this.cntrl.get_enablecost();
                if (dt_enablecost.Rows.Count > 0)
                {
                    if (dt_enablecost.Rows[0][1].ToString().Trim() == "Yes")
                    {
                        costtextbox.Visible = true;
                        label4.Visible = true;
                        label3.Visible = true;
                        label7.Visible = true;
                        label5.Visible = true;
                        dislabel.Visible = true;
                        //Cmb_Discount.Visible = true;
                        //disclick.Visible = true;
                        label6.Visible = true;
                        tottext.Visible = true;//
                        proceduretreatgrid.Columns[2].Visible = true;
                        DGV_Procedure.Columns["Column11"].Visible = true;
                        DGV_Procedure.Columns["Column12"].Visible = true;
                        DGV_Procedure.Columns["Column14"].Visible = true;
                        DGV_Procedure.Columns["Column13"].Visible = true;
                    }
                    else
                    {
                        costtextbox.Visible = false;
                        label4.Visible = false;
                        label3.Visible = false;
                        label7.Visible = false;
                        label5.Visible = false;
                        dislabel.Visible = false;
                        Cmb_Discount.Visible = false;
                        disclick.Visible = false;
                        label6.Visible = false;
                        tottext.Visible = false;
                        proceduretreatgrid.Columns[2].Visible = false;
                        DGV_Procedure.Columns["Column11"].Visible = false;
                        DGV_Procedure.Columns["Column12"].Visible = false;
                        DGV_Procedure.Columns["Column14"].Visible = false;
                        DGV_Procedure.Columns["Column13"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !.....", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btn_SaveProcedure_Click(object sender, EventArgs e)
        {
            DataTable dtb = this.cntrl.check_procedurename(txt_procedure.Text);
            insertTreatment(dtb);

        }
        public void insertTreatment(DataTable checkdatacc)
        {
            try
            {
               
                if (checkdatacc.Rows.Count > 0)
                {
                    MessageBox.Show("Procedure " + txt_procedure.Text + " already exist");
                }
                else
                {
                    if (txt_Cost.Text != "" && txt_procedure.Text != "")
                    {
                        this.cntrl.save_Procedure(txt_procedure.Text, txt_Cost.Text);
                        string p = this.cntrl.Procedure_maxid();
                        int pid = int.Parse(p);
                        DataTable dt_pt = this.cntrl.Get_all_procedures();
                        fill_proceduregrid(dt_pt);
                    }
                    else
                    {
                        MessageBox.Show("Please Enter the Procedure and Cost...", "Data Required..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    txt_Cost.Clear();
                    txt_procedure.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !.....", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void fill_proceduregrid(DataTable dt_pt)
        {
            if (dt_pt.Rows.Count > 0)
            {
                proceduretreatgrid.Rows.Clear();
                for (int j = 0; j < dt_pt.Rows.Count; j++)
                {
                    proceduretreatgrid.Rows.Add(dt_pt.Rows[j]["id"].ToString(), dt_pt.Rows[j]["name"].ToString(), Convert.ToDecimal(dt_pt.Rows[j]["cost"].ToString()).ToString("##.##"));
                }
            }
           
        }

        private void addbut_Click(object sender, EventArgs e)
        {
            txt_procedure.Visible = true;
            txt_Cost.Visible = true;
            addbut.Visible = false;
            btn_ProcedureClose.Visible = true;
            btn_SaveProcedure.Visible = true;
            Lab_Procedure.Show();
            lab_Cost.Show();

           
        }

        private void btn_ProcedureClose_Click(object sender, EventArgs e)
        {
            txt_procedure.Visible = false;
            txt_Cost.Visible = false;
            lab_Cost.Hide();
            Lab_Procedure.Hide();
            addbut.Visible = true;
            btn_ProcedureClose.Visible = false;
            btn_SaveProcedure.Visible = false;
            searchtextbox.Visible = true;
        }

        private void proceduretreatgrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RTB_AddNotes.Visible = false;
            panl_teeth.Hide();
            Lab_msg.Hide();
            try
            {
                //if (tooth_imgno != "" && tooth_imgno != "0")
                //{
                    int r = e.RowIndex;
                    id = proceduretreatgrid.Rows[r].Cells[0].Value.ToString();
                    int idd = Int32.Parse(id);
                    DataTable dt = this.cntrl.get_ProcedureTreatment(id);
                    servicetext.Text = dt.Rows[0][0].ToString();
                    costtextbox.Text = dt.Rows[0][1].ToString();
                tottext.Text = Convert.ToString(Convert.ToDecimal(dt.Rows[0][1].ToString()));
                //var str = tooth_imgno.Split(',');
                    
                //if(str[] =="")
                    qty1.Text = "1";
                //else
                  //  qty1.Text = (str.Length - 1).ToString();
                Cmb_Discount.Text = "INR";
                    disclick.Text = "";
                    RTB_AddNotes.Text = "";
                    RTB_AddNotes.Width = 500;
                    lab_SelectedTeeth.Text = "";
                    lab_teeth.Text = "Teeth";
                    checkBox1.Checked = false;
                    chk_fullmouth.Checked = false;

                    chk11.Checked = false; chk12.Checked = false;
                    chk13.Checked = false; chk14.Checked = false;
                    chk15.Checked = false; chk16.Checked = false;
                    chk17.Checked = false; chk18.Checked = false;

                    chk21.Checked = false; chk22.Checked = false;
                    chk23.Checked = false; chk24.Checked = false;
                    chk25.Checked = false; chk26.Checked = false;
                    chk27.Checked = false; chk28.Checked = false;

                    chk31.Checked = false; chk32.Checked = false;
                    chk33.Checked = false; chk34.Checked = false;
                    chk35.Checked = false; chk36.Checked = false;
                    chk37.Checked = false; chk38.Checked = false;

                    chk41.Checked = false; chk42.Checked = false;
                    chk43.Checked = false; chk44.Checked = false;
                    chk45.Checked = false; chk46.Checked = false;
                    chk47.Checked = false; chk48.Checked = false;
                    chk51.Checked = false; chk52.Checked = false;
                    chk53.Checked = false; chk54.Checked = false;
                    chk55.Checked = false; chk61.Checked = false;
                    chk62.Checked = false; chk63.Checked = false;
                    chk64.Checked = false; chk65.Checked = false;
                    chk71.Checked = false; chk72.Checked = false;
                    chk73.Checked = false; chk74.Checked = false;
                    chk75.Checked = false; chk81.Checked = false;
                    chk82.Checked = false; chk83.Checked = false;
                    chk84.Checked = false; chk85.Checked = false;
                    panl_TreatmentAdd.Show();
                //}
                //else
                //{
                //    MessageBox.Show("Please choose tooth before selecting the treatment", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch
            {
            }
        }

        private void qty1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void qty1_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }
        public void Calculations()
        {
            try
            {
                decimal qty = 0;
                decimal cost = 0;
                decimal discount = 0;
                if (qty1.Text != "")
                {
                    qty = Convert.ToDecimal(qty1.Text);
                }
                if (costtextbox.Text != "")
                {
                    cost = Convert.ToDecimal(costtextbox.Text);
                }
                if (disclick.Text != "")
                {
                    if (Cmb_Discount.Text == "INR")
                    {
                        discount = Convert.ToDecimal(disclick.Text);
                    }
                    else
                    {
                        discount = ((qty * cost) * Convert.ToDecimal(disclick.Text)) / 100;
                    }
                }
                discounttotal = discount;

                tottext.Text = Convert.ToString(decimal.Round(((qty * cost) - discount), 2, MidpointRounding.AwayFromZero));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Not a valid number. Please try again.", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void costtextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.'))
            { e.Handled = true; }
            TextBox txtDecimal = sender as TextBox;
            if (e.KeyChar == '.' && txtDecimal.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void costtextbox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                int a;
                if (costtextbox.Text == "")
                {
                    a = 0;
                }
                else
                {
                    a = Convert.ToInt32(costtextbox.Text);
                }
                //int a = Convert.ToInt32(costtextbox.Text);
                int b = Convert.ToInt32(qty1.Text);
                int c = a * b;
                cqgrant.Text = c.ToString();
                totgrant.Text = c.ToString();
                tottext.Text = c.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !.....", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void costtextbox_TextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        private void dislabel_Click(object sender, EventArgs e)
        {
            dislabel.Visible = false;
            disclick.Visible = true;
            Cmb_Discount.Show();
        }

        private void disclick_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal qty = 0;
                decimal cost = 0;
                decimal discount = 0;
                if (qty1.Text != "")
                {
                    qty = Convert.ToDecimal(qty1.Text);
                }
                if (costtextbox.Text != "")
                {
                    cost = Convert.ToDecimal(costtextbox.Text);
                }
                if (disclick.Text != "")
                {
                    if (Cmb_Discount.Text == "INR")
                    {
                        discount = Convert.ToDecimal(disclick.Text);
                    }
                    else
                    {
                        discount = ((qty * cost) * Convert.ToDecimal(disclick.Text)) / 100;
                    }
                }
                discounttotal = discount;
                tottext.Text = Convert.ToString((qty * cost) - discount);
                decimal dis1 = Convert.ToDecimal(qty * cost);
                decimal dis2 = discounttotal;
                if (dis1 < dis2)
                {
                    MessageBox.Show("Discount Cost is greater than Actual Cost");
                    disclick.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Not a valid number. Please try again.", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cmb_Discount_SelectedIndexChanged(object sender, EventArgs e)
        {
            Calculations();
        }
        public int rowcnt = 0;
        private void btn_TreatmentAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (qty1.Text == "" || servicetext.Text == "" || costtextbox.Text == "")
                {
                    MessageBox.Show("Fill the Mandatory field(s)...", "Empty field(s)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Decimal dis = 0;
                    string tooth = "";
                    if (disclick.Text == "")
                    {
                        dis = 0;
                    }
                    else
                    {
                        dis = Convert.ToDecimal(disclick.Text);
                    }
                    if (tooth_imgno.Length>0)
                    {
                         tooth = tooth_imgno.Remove(tooth_imgno.Length - 1);
                    }
                    if(tooth !="")
                    {
                        DGV_Procedure.Rows.Add(id, servicetext.Text, qty1.Text, costtextbox.Text, dis, Cmb_Discount.Text, tottext.Text, discounttotal, "", "dt.Rows[0][0].ToString()", Cmb_Doctor.Text, RTB_AddNotes.Text, tooth, "TO Nurse", "No", "DEL", lab_SelectedTeeth.Text);
                        //DGV_Procedure.Rows.Add(id, servicetext.Text, qty1.Text, costtextbox.Text, dis, Cmb_Discount.Text, tottext.Text, discounttotal, "", "dt.Rows[0][0].ToString()", Cmb_Doctor.Text, RTB_AddNotes.Text, "TO Nurse", "No", "DEL", "");

                    }
                    else
                    {
                        MessageBox.Show("Please choose tooth", "Error !.....", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    Decimal totalcost = 0;
                    Decimal totaldiscount = 0;
                    Decimal totalgrand = 0;
                    for (int i = 0; i < DGV_Procedure.Rows.Count; i++)
                    {
                        totalcost = totalcost + (Convert.ToDecimal(DGV_Procedure.Rows[i].Cells[3].Value.ToString()) * Convert.ToDecimal(DGV_Procedure.Rows[i].Cells[2].Value.ToString()));
                        totaldiscount = totaldiscount + Convert.ToDecimal(DGV_Procedure.Rows[i].Cells[7].Value.ToString());
                        totalgrand = totalgrand + Convert.ToDecimal(DGV_Procedure.Rows[i].Cells[6].Value.ToString());
                    }
                   
                    cqgrant.Text = String.Format("{0:C0}", totalcost);
                    text_discounttotal.Text = String.Format("{0:C0}", totaldiscount);
                    totgrant.Text = String.Format("{0:C0}", totalgrand);
                    if (dtb_toothtable.Rows.Count > 0)
                    {
                        var items = tooth_imgno.Split(',');
                        for (int k = 1; k < items.Length; k++)
                        {
                            dtb_toothtable.Rows[rowcnt]["Pt_id"] = patient_id;
                            dtb_toothtable.Rows[rowcnt]["procedure_id"] = id.ToString();
                            if (dtb_toothtable.Rows[rowcnt]["Occlual"].ToString()=="")
                            {
                                dtb_toothtable.Rows[rowcnt]["Occlual"] = "No";
                            }
                            if (dtb_toothtable.Rows[rowcnt]["Mesial"].ToString() == "")
                            {
                                dtb_toothtable.Rows[rowcnt]["Mesial"] = "No";
                            }
                            if (dtb_toothtable.Rows[rowcnt]["Distal"].ToString() == "")
                            {
                                dtb_toothtable.Rows[rowcnt]["Distal"] = "No";
                            }
                            if (dtb_toothtable.Rows[rowcnt]["Buccal"].ToString() == "")
                            {
                                dtb_toothtable.Rows[rowcnt]["Buccal"] = "No";
                            }
                            if (dtb_toothtable.Rows[rowcnt]["Lingual"].ToString() == "")
                            {
                                dtb_toothtable.Rows[rowcnt]["Lingual"] = "No";
                            }
                           
                            rowcnt++;
                        }
                    }
                    panl_teeth.Visible = false;
                    clear();
                    remove_border(tooth_imgno);
                    tooth_imgno = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !.....", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void clear()
        {
            servicetext.Text = "";
            qty1.Text = "";
            costtextbox.Text = "";
            disclick.Text = "";
            Cmb_Discount.Text = "";
            tottext.Text = "";
            Cmb_Doctor.Text = "";
            RTB_AddNotes.Text = "";
            lab_SelectedTeeth.Text = "";
        }

        private void lab_AddNotes_Click(object sender, EventArgs e)
        {
            RTB_AddNotes.Visible = true;
        }

        private void lab_teeth_Click(object sender, EventArgs e)
        {
            panl_teeth.Visible = true;
            panel4.Visible = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                decimal q = Convert.ToDecimal(qty1.Text.ToString());
                qty1.Text = Convert.ToString(q);

                int qty = 0;
                for (int i = 0; i < 51; i++)
                { if (tooth[i] != "") { qty++; } }
                if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            }
            else
            {
                qty1.Text = "1";
            }
        }

        private void chk_fullmouth_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_fullmouth.Checked)
            {
                chk11.Checked = true;
                chk12.Checked = true;
                chk13.Checked = true;
                chk14.Checked = true;
                chk15.Checked = true;
                chk16.Checked = true;
                chk17.Checked = true;
                chk18.Checked = true;
                chk21.Checked = true;
                chk22.Checked = true;
                chk23.Checked = true;
                chk24.Checked = true;
                chk25.Checked = true;
                chk26.Checked = true;
                chk27.Checked = true;
                chk28.Checked = true;
                chk31.Checked = true;
                chk32.Checked = true;
                chk33.Checked = true;
                chk34.Checked = true;
                chk35.Checked = true;
                chk36.Checked = true;
                chk37.Checked = true;
                chk38.Checked = true;
                chk41.Checked = true;
                chk42.Checked = true;
                chk43.Checked = true;
                chk44.Checked = true;
                chk45.Checked = true;
                chk46.Checked = true;
                chk47.Checked = true;
                chk48.Checked = true;
                lab_SelectedTeeth.Text = "Full Mouth";
            }
            else
            {

                lab_SelectedTeeth.Text = "";
                check_checkbox();
            }
        }

        private void chk18_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk18.Checked) { tooth[0] = "18"; } else { tooth[0] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk17_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk17.Checked) { tooth[1] = "17"; } else { tooth[1] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk16_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk16.Checked)
            {
                tooth[2] = "16";
            }
            else
            {
                tooth[2] = "";
            }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            {
                if (tooth[i] != "")
                {
                    toothno = toothno + '|' + tooth[i]; qty++;
                }
            }
            if (checkBox1.Checked)
            {
                qty1.Text = qty.ToString();
            }
            if (toothno.Length > 2)
            {
                lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1);
            }
            else
            {
                lab_SelectedTeeth.Text = "";
            }
        }

        private void chk15_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk15.Checked) { tooth[3] = "15"; } else { tooth[3] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk14_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk14.Checked) { tooth[4] = "14"; } else { tooth[4] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk13_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk13.Checked) { tooth[5] = "13"; } else { tooth[5] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk12_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk12.Checked) { tooth[6] = "12"; } else { tooth[6] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk11_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk11.Checked) { tooth[7] = "11"; } else { tooth[7] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk21_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk21.Checked) { tooth[8] = "21"; } else { tooth[8] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk22_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk22.Checked) { tooth[9] = "22"; } else { tooth[9] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk23_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk23.Checked) { tooth[10] = "23"; } else { tooth[10] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk24_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk24.Checked) { tooth[11] = "24"; } else { tooth[11] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk25_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk25.Checked) { tooth[12] = "25"; } else { tooth[12] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk26_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk26.Checked) { tooth[13] = "26"; } else { tooth[13] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk27_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk27.Checked) { tooth[14] = "27"; } else { tooth[14] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk28_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk28.Checked) { tooth[15] = "28"; } else { tooth[15] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk48_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk48.Checked) { tooth[31] = "48"; } else { tooth[31] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk47_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk47.Checked) { tooth[30] = "47"; } else { tooth[30] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk46_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk46.Checked) { tooth[29] = "46"; } else { tooth[29] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk45_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk45.Checked) { tooth[28] = "45"; } else { tooth[28] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk44_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk44.Checked) { tooth[27] = "44"; } else { tooth[27] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk43_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk43.Checked) { tooth[26] = "43"; } else { tooth[26] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk42_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk42.Checked) { tooth[25] = "42"; } else { tooth[25] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk41_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk41.Checked) { tooth[24] = "41"; } else { tooth[24] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk31_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk31.Checked) { tooth[23] = "31"; } else { tooth[23] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk32_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk32.Checked) { tooth[22] = "32"; } else { tooth[22] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk33_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk33.Checked) { tooth[21] = "33"; } else { tooth[21] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk34_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk34.Checked) { tooth[20] = "34"; } else { tooth[20] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk35_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk35.Checked) { tooth[19] = "35"; } else { tooth[19] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk36_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk36.Checked) { tooth[18] = "36"; } else { tooth[18] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk37_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk37.Checked) { tooth[17] = "37"; } else { tooth[17] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk38_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk38.Checked) { tooth[16] = "38"; } else { tooth[16] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk55_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk55.Checked) { tooth[32] = "55"; } else { tooth[32] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk54_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk54.Checked) { tooth[33] = "54"; } else { tooth[33] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk53_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk53.Checked) { tooth[34] = "53"; } else { tooth[34] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk52_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk52.Checked) { tooth[35] = "52"; } else { tooth[35] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk51_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk51.Checked) { tooth[36] = "51"; } else { tooth[36] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk61_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk61.Checked) { tooth[37] = "61"; } else { tooth[37] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk62_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk62.Checked) { tooth[38] = "62"; } else { tooth[38] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk63_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk63.Checked) { tooth[39] = "63"; } else { tooth[39] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk64_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk64.Checked) { tooth[40] = "64"; } else { tooth[40] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk65_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk65.Checked) { tooth[41] = "65"; } else { tooth[41] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk85_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk85.Checked)
            {
                tooth[51] = "85";
            }
            else
            {
                tooth[51] = "";
            }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i <= 51; i++)
            {
                if (tooth[i] != "")
                {
                    toothno = toothno + '|' + tooth[i]; qty++;
                }
            }
            if (checkBox1.Checked)
            {
                qty1.Text = qty.ToString();
            }
            if (toothno.Length > 2)
            {
                lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1);
            }
            else
            {
                lab_SelectedTeeth.Text = "";
            }
        }

        private void chk84_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk84.Checked)
            {
                tooth[50] = "84";
            }
            else
            {
                tooth[50] = "";
            }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            {
                if (tooth[i] != "")
                {
                    toothno = toothno + '|' + tooth[i]; qty++;
                }
            }
            if (checkBox1.Checked)
            {
                qty1.Text = qty.ToString();
            }
            if (toothno.Length > 2)
            {
                lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1);
            }
            else
            {
                lab_SelectedTeeth.Text = "";
            }
        }

        private void chk83_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk83.Checked) { tooth[49] = "83"; } else { tooth[49] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk82_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk82.Checked) { tooth[48] = "82"; } else { tooth[48] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk81_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk81.Checked) { tooth[47] = "81"; } else { tooth[47] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk72_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk72.Checked) { tooth[45] = "72"; } else { tooth[45] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk71_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk71.Checked) { tooth[46] = "71"; } else { tooth[46] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk73_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk73.Checked) { tooth[44] = "73"; } else { tooth[44] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk74_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk74.Checked) { tooth[43] = "74"; } else { tooth[43] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void chk75_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk75.Checked) { tooth[42] = "75"; } else { tooth[42] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (tooth[i] != "") { toothno = toothno + '|' + tooth[i]; qty++; } }
            if (checkBox1.Checked) { qty1.Text = qty.ToString(); }
            if (toothno.Length > 2) { lab_SelectedTeeth.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { lab_SelectedTeeth.Text = ""; }
        }

        private void label19_Click(object sender, EventArgs e)
        {
            if (label19.Text == "Show ChildTeeth")
            {
                panel4.Show();
                label19.Text = "Hide ChildTeeth";
            }
            else
            {
                panel4.Hide();
                label19.Text = "Show ChildTeeth";
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            panl_teeth.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            lab_SelectedTeeth.Text = "";
            check_checkbox();
            panl_teeth.Visible = false;
        }
        public void check_checkbox()
        {
            chk11.Enabled = true; chk12.Enabled = true;
            chk13.Enabled = true; chk14.Enabled = true;
            chk15.Enabled = true; chk16.Enabled = true;
            chk17.Enabled = true; chk18.Enabled = true;
            chk21.Enabled = true; chk22.Enabled = true;
            chk23.Enabled = true; chk24.Enabled = true;
            chk25.Enabled = true; chk26.Enabled = true;
            chk27.Enabled = true; chk28.Enabled = true;
            chk31.Enabled = true; chk32.Enabled = true;
            chk33.Enabled = true; chk34.Enabled = true;
            chk35.Enabled = true; chk36.Enabled = true;
            chk37.Enabled = true; chk38.Enabled = true;
            chk41.Enabled = true; chk42.Enabled = true;
            chk43.Enabled = true; chk44.Enabled = true;
            chk45.Enabled = true; chk46.Enabled = true;
            chk47.Enabled = true; chk48.Enabled = true;
            chk51.Enabled = true; chk52.Enabled = true;
            chk53.Enabled = true; chk54.Enabled = true;
            chk55.Enabled = true; chk61.Enabled = true;
            chk62.Enabled = true; chk63.Enabled = true;
            chk64.Enabled = true; chk65.Enabled = true;
            chk71.Enabled = true; chk72.Enabled = true;
            chk73.Enabled = true; chk74.Enabled = true;
            chk75.Enabled = true; chk81.Enabled = true;
            chk82.Enabled = true; chk83.Enabled = true;
            chk84.Enabled = true; chk85.Enabled = true;
            chk11.Checked = false; chk12.Checked = false;
            chk13.Checked = false; chk14.Checked = false;
            chk15.Checked = false; chk16.Checked = false;
            chk17.Checked = false; chk18.Checked = false;
            chk21.Checked = false; chk22.Checked = false;
            chk23.Checked = false; chk24.Checked = false;
            chk25.Checked = false; chk26.Checked = false;
            chk27.Checked = false; chk28.Checked = false;
            chk31.Checked = false; chk32.Checked = false;
            chk33.Checked = false; chk34.Checked = false;
            chk35.Checked = false; chk36.Checked = false;
            chk37.Checked = false; chk38.Checked = false;
            chk41.Checked = false; chk42.Checked = false;
            chk43.Checked = false; chk44.Checked = false;
            chk45.Checked = false; chk46.Checked = false;
            chk47.Checked = false; chk48.Checked = false;
            chk51.Checked = false; chk52.Checked = false;
            chk53.Checked = false; chk54.Checked = false;
            chk55.Checked = false; chk61.Checked = false;
            chk62.Checked = false; chk63.Checked = false;
            chk64.Checked = false; chk65.Checked = false;
            chk71.Checked = false; chk72.Checked = false;
            chk73.Checked = false; chk74.Checked = false;
            chk75.Checked = false; chk81.Checked = false;
            chk82.Checked = false; chk83.Checked = false;
            chk84.Checked = false; chk85.Checked = false;
        }

        private void chkfulch_CheckedChanged(object sender, EventArgs e)
        {
            if (chkfulch.Checked)
            {
                chk51.Checked = true;
                chk52.Checked = true;
                chk53.Checked = true;
                chk54.Checked = true;
                chk55.Checked = true;
                chk61.Checked = true;
                chk62.Checked = true;
                chk63.Checked = true;
                chk64.Checked = true;
                chk65.Checked = true;
                chk71.Checked = true;
                chk72.Checked = true;
                chk73.Checked = true;
                chk74.Checked = true;
                chk75.Checked = true;
                chk81.Checked = true;
                chk82.Checked = true;
                chk83.Checked = true;
                chk84.Checked = true;
                chk85.Checked = true;
            }
            else
            {
                chk51.Checked = false;
                chk52.Checked = false;
                chk53.Checked = false;
                chk54.Checked = false;
                chk55.Checked = false;
                chk61.Checked = false;
                chk62.Checked = false;
                chk63.Checked = false;
                chk64.Checked = false;
                chk65.Checked = false;
                chk71.Checked = false;
                chk72.Checked = false;
                chk73.Checked = false;
                chk74.Checked = false;
                chk75.Checked = false;
                chk81.Checked = false;
                chk82.Checked = false;
                chk83.Checked = false;
                chk84.Checked = false;
                chk85.Checked = false;
            }
        }

        private void DGV_Procedure_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 15 && e.RowIndex > -1)
                {
                    if (MessageBox.Show("Delete this Treatment.. Confirm?", "Remove Treatment", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int r = e.RowIndex;
                        delete_tooth_frm_toothtable(DGV_Procedure.Rows[r].Cells[12].Value.ToString());
                        this.DGV_Procedure.Rows.RemoveAt(e.RowIndex);
                        Decimal totalcost = 0;
                        Decimal totaldiscount = 0;
                        Decimal totalgrand = 0;
                        for (int i = 0; i < DGV_Procedure.Rows.Count; i++)
                        {
                            totalcost = totalcost + (Convert.ToDecimal(DGV_Procedure.Rows[i].Cells[3].Value.ToString()) * Convert.ToDecimal(DGV_Procedure.Rows[i].Cells[2].Value.ToString()));
                            totaldiscount = totaldiscount + Convert.ToDecimal(DGV_Procedure.Rows[i].Cells[7].Value.ToString());
                            totalgrand = totalgrand + Convert.ToDecimal(DGV_Procedure.Rows[i].Cells[6].Value.ToString());
                        }
                      
                        cqgrant.Text = String.Format("{0:C0}", totalcost);
                        text_discounttotal.Text = String.Format("{0:C0}", totaldiscount);
                        totgrant.Text = String.Format("{0:C0}", totalgrand);
                    }
                }
                else if (DGV_Procedure.CurrentCell.OwningColumn.Name == "tonurse")
                {
                    DGV_Procedure.CurrentRow.Cells["nursenote"].Value = "Yes";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !.....", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void savebut_Click(object sender, EventArgs e)
        {
            if (DGV_Procedure.Rows.Count > 0)
            {
                string cs = PappyjoeMVC.Model.Connection.MyGlobals.trans_connectionstring;
                using (MySqlConnection con = new MySqlConnection(cs))
                {
                    con.Open();
                    MySqlTransaction trans = con.BeginTransaction();
                    try
                    {
                        int j, k1=0; bool flag = false;
                        string dr_id = Cmb_Doctor.SelectedValue.ToString();
                        this.cntrl.Save_treatment(dr_id, patient_id, DTP_Date.Value.ToString("yyyy-MM-dd"), Cmb_Doctor.Text, linkLabel_Name.Text, cqgrant.Text, text_discounttotal.Text, totgrant.Text, con, trans);

                        //this.cntrl.Save_treatment(dr_id, patient_id, DTP_Date.Value.ToString("yyyy-MM-dd"), Cmb_Doctor.Text, linkLabel_Name.Text, cqgrant.Text, text_discounttotal.Text, totgrant.Text);
                        string dt = this.cntrl.get_treatmentmaxid(con, trans);
                        int planid;
                        //int j;
                        string tooth_value = tooth_imgno.Replace(",", "");
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
                        for (int ii = 0; ii < DGV_Procedure.Rows.Count; ii++)
                        {
                            if (DGV_Procedure.Rows[ii].Cells["nursenote"].Value.ToString() == "Yes")
                            {
                                flag = true;
                                this.cntrl.Save_treatmentgrid_set_ststus(j, DGV_Procedure[0, ii].Value.ToString(), patient_id, DGV_Procedure[1, ii].Value.ToString(), DGV_Procedure[2, ii].Value.ToString(), DGV_Procedure[3, ii].Value.ToString(), DGV_Procedure[5, ii].Value.ToString(), DGV_Procedure[4, ii].Value.ToString(), DGV_Procedure[6, ii].Value.ToString(), DGV_Procedure[7, ii].Value.ToString(), DGV_Procedure[11, ii].Value.ToString(), DGV_Procedure[12, ii].Value.ToString(), con, trans);
                                this.cntrl.save_completed_id_trans(DTP_Date.Value.ToString("yyyy-MM-dd"), patient_id, con, trans);
                                string maxid = this.cntrl.get_completedMaxid_trans(con, trans);
                                string treat_id = this.cntrl.get_treatmentplan_id(Int32.Parse(maxid), DGV_Procedure[0, ii].Value.ToString(), patient_id, con, trans);//
                                int completed_id;
                                if (Int32.Parse(maxid) == 0)//dt
                                {
                                    k1 = 1;
                                    completed_id = 0;
                                }
                                else
                                {
                                    completed_id = Int32.Parse(maxid);
                                }
                                k1 = completed_id;
                                this.cntrl.save_completed_items_trans(k1, patient_id, DGV_Procedure[0, ii].Value.ToString(), DGV_Procedure[1, ii].Value.ToString(), DGV_Procedure[2, ii].Value.ToString(), DGV_Procedure[3, ii].Value.ToString(), DGV_Procedure[5, ii].Value.ToString(), DGV_Procedure[4, ii].Value.ToString(), DGV_Procedure[6, ii].Value.ToString(), DGV_Procedure[7, ii].Value.ToString(), DGV_Procedure[11, ii].Value.ToString(), DTP_Date.Value.ToString("yyyy-MM-dd"), Cmb_Doctor.SelectedValue.ToString(), j.ToString(), DGV_Procedure[12, ii].Value.ToString(), "Yes", con, trans);
                                //                                string dt1 = DateTime.Now.Date.ToString("yyyy-MM-dd");
                                //                              plan_main_id, patient_id, procedure_id,                      procedure_name,                           quantity,                              cost,                               discount_type,                             discount,                        total,                                    discount_inrs,                           note,                                date,                                     dr_id,                             completed_id,                            tooth

                            }
                            else
                            {
                                this.cntrl.Save_treatmentgrid(j, DGV_Procedure[0, ii].Value.ToString(), patient_id, DGV_Procedure[1, ii].Value.ToString(), DGV_Procedure[2, ii].Value.ToString(), DGV_Procedure[3, ii].Value.ToString(), DGV_Procedure[5, ii].Value.ToString(), DGV_Procedure[4, ii].Value.ToString(), DGV_Procedure[6, ii].Value.ToString(), DGV_Procedure[7, ii].Value.ToString(), DGV_Procedure[11, ii].Value.ToString(), DGV_Procedure[12, ii].Value.ToString(), con, trans);
                                //this.cntrl.Save_treatmentgrid(j, DGV_Procedure[0, ii].Value.ToString(), patient_id, DGV_Procedure[1, ii].Value.ToString(), DGV_Procedure[2, ii].Value.ToString(), DGV_Procedure[3, ii].Value.ToString(), DGV_Procedure[5, ii].Value.ToString(), DGV_Procedure[4, ii].Value.ToString(), DGV_Procedure[6, ii].Value.ToString(), DGV_Procedure[7, ii].Value.ToString(), DGV_Procedure[11, ii].Value.ToString(), DGV_Procedure[12, ii].Value.ToString());

                            }
                        }
                        string tooth = "";
                        if (dtb_toothtable.Rows.Count > 0)
                        {

                            for (int k = 0; k < dtb_toothtable.Rows.Count; k++)
                            {
                                if (dtb_toothtable.Rows[k]["Tooth_Number"].ToString() != "")
                                {
                                    tooth = "1";
                                }
                                else
                                { tooth = "0"; }
                                this.cntrl.save_tooth(patient_id, j, dtb_toothtable.Rows[k]["procedure_id"].ToString(), tooth, dtb_toothtable.Rows[k]["Tooth_Number"].ToString(), dtb_toothtable.Rows[k]["Surface_Number"].ToString(), dtb_toothtable.Rows[k]["Occlual"].ToString(), dtb_toothtable.Rows[k]["Mesial"].ToString(), dtb_toothtable.Rows[k]["Distal"].ToString(), dtb_toothtable.Rows[k]["Buccal"].ToString(), dtb_toothtable.Rows[k]["Lingual"].ToString(), con, trans);
                            }
                        }
                        //for (int ii = 0; ii < DGV_Procedure.Rows.Count; ii++)
                        //{
                        //    this.cntrl.Save_treatmentgrid(j, DGV_Procedure[0, ii].Value.ToString(), patient_id, DGV_Procedure[1, ii].Value.ToString(), DGV_Procedure[2, ii].Value.ToString(), DGV_Procedure[3, ii].Value.ToString(), DGV_Procedure[5, ii].Value.ToString(), DGV_Procedure[4, ii].Value.ToString(), DGV_Procedure[6, ii].Value.ToString(), DGV_Procedure[7, ii].Value.ToString(), DGV_Procedure[11, ii].Value.ToString(), DGV_Procedure[12, ii].Value.ToString());
                        //}

                        trans.Commit();
                        con.Close();
                        string dt1 = DateTime.Now.Date.ToString("yyyy-MM-dd");
                        DateTime Timeonly = DateTime.Now;
                        this.cntrl.save_log(doctor_id, "Treatment Plan", " Add Treatment Plan", dt1, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Add", j.ToString());
                        if (flag == true)
                        {
                            string dt11 = DateTime.Now.Date.ToString("yyyy-MM-dd");
                            DateTime Timeonl = DateTime.Now;
                            this.cntrl.save_log(doctor_id, "Finished Procedure", " Add Finished Procedure", dt11, Convert.ToString(Timeonl.ToString("hh:mm tt")), "Add", k1.ToString());

                        }
                        var form2 = new Dental_Treatment_Plans();
                        form2.doctor_id = doctor_id;
                        form2.patient_id = patient_id;
                        openform(form2);


                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        con.Close();
                        MessageBox.Show(ex.Message, "SAVE / UPDATE function is failed !..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btn_TreatmentAdd.Focus();
                        timer1.Start();
                        timer1.Enabled = true;

                        return;

                    }
                }
            }



            //try
            //{
            //    if (DGV_Procedure.Rows.Count > 0)
            //    {
            //        int j;
            //        string dr_id = Cmb_Doctor.SelectedValue.ToString();
            //        this.cntrl.Save_treatment(dr_id, patient_id, DTP_Date.Value.ToString("yyyy-MM-dd"), Cmb_Doctor.Text, linkLabel_Name.Text, cqgrant.Text, text_discounttotal.Text, totgrant.Text);
            //        string dt = this.cntrl.get_treatmentmaxid();
            //        int planid;
            //        string tooth_value = tooth_imgno.Replace(",", "");
            //        try
            //        {
            //            if (Int32.Parse(dt) == 0)
            //            {
            //                j = 1;
            //                planid = 1;
            //            }
            //            else
            //            {
            //                planid = Int32.Parse(dt);
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            j = 1;
            //            planid = 1;
            //        }
            //        j = planid;
            //        for (int ii = 0; ii < DGV_Procedure.Rows.Count; ii++)
            //        {
            //            this.cntrl.Save_treatmentgrid(j, DGV_Procedure[0, ii].Value.ToString(), patient_id, DGV_Procedure[1, ii].Value.ToString(), DGV_Procedure[2, ii].Value.ToString(), DGV_Procedure[3, ii].Value.ToString(), DGV_Procedure[5, ii].Value.ToString(), DGV_Procedure[4, ii].Value.ToString(), DGV_Procedure[6, ii].Value.ToString(), DGV_Procedure[7, ii].Value.ToString(), DGV_Procedure[11, ii].Value.ToString(), DGV_Procedure[12, ii].Value.ToString());
            //        }
            //        string tooth = "";
            //        if (dtb_toothtable.Rows.Count > 0)
            //        {

            //            for (int k = 0; k < dtb_toothtable.Rows.Count; k++)
            //            {
            //                if (dtb_toothtable.Rows[k]["Tooth_Number"].ToString() != "")
            //                {
            //                    tooth = "1";
            //                }
            //                else
            //                { tooth = "0"; }
            //                this.cntrl.save_tooth(patient_id, j, dtb_toothtable.Rows[k]["procedure_id"].ToString(), tooth, dtb_toothtable.Rows[k]["Tooth_Number"].ToString(), dtb_toothtable.Rows[k]["Surface_Number"].ToString(), dtb_toothtable.Rows[k]["Occlual"].ToString(), dtb_toothtable.Rows[k]["Mesial"].ToString(), dtb_toothtable.Rows[k]["Distal"].ToString(), dtb_toothtable.Rows[k]["Buccal"].ToString(), dtb_toothtable.Rows[k]["Lingual"].ToString());
            //            }
            //        }
            //        var form2 = new Treatment_Plans();
            //        form2.doctor_id = doctor_id;
            //        form2.patient_id = patient_id;
            //        openform(form2);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Select a Treatment...., Click 'Add' Button . and try again..", "Treatment Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        btn_TreatmentAdd.Focus();
            //        timer1.Start();
            //        timer1.Enabled = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error !.....", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void linkLabel_id_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Patient_Profile_Edit a = new Patient_Profile_Edit();
            a.patient_id = linkLabel_id.Text.ToString();
            a.Show();
        }

        private void btn_TreatmentAdd_MouseEnter(object sender, EventArgs e)
        {
            btn_TreatmentAdd.BackColor = Color.LimeGreen;
            timer1.Stop();
            timer1.Enabled = false;
        }

        private void btnaCancel_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.Module == "Ophthalmology")
            {
                var form = new Dental_Treatment_Plans();
                form.doctor_id = doctor_id;
                form.patient_id = patient_id;
                openform(form);
            }
            if (GlobalVariables.Module == "Main")
            {
                var form2 = new Dental_Treatment_Plans();
                form2.doctor_id = doctor_id;
                form2.patient_id = patient_id;
                openform(form2);
            }
        }

        private void linkLabel_Name_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Patient_Profile_Edit a = new Patient_Profile_Edit();
            a.patient_id = linkLabel_id.Text.ToString();
            a.Show();
        }

        private void searchtextbox_Click(object sender, EventArgs e)
        {
            searchtextbox.Text = "";
        }

        private void searchtextbox_KeyUp(object sender, KeyEventArgs e)
        {
            DataTable dt = this.cntrl.Search_procedure(searchtextbox.Text);
            fill_proceduregrid(dt);
        }

        private void txt_Cost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == (char)Keys.Back || e.KeyChar == Char.Parse("."))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Please Enter The Correct cost", "Invalid cost", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void fill_toothTable(string re_move)
        {

            if (dtb_toothtable.Rows.Count > 0)
            {
                if (check_tooth_surface(re_move) == 1)
                {
                    if (tooth_flag == true)
                    {
                        dtb_toothtable.Rows[rowindex]["Tooth_Number"] = re_move;
                        dtb_toothtable.Rows[rowindex]["Surface_Number"] = re_move;
                        tooth_flag = false;

                    }
                    else
                    {
                        dtb_toothtable.Rows[rowindex]["Occlual"] = O;
                        dtb_toothtable.Rows[rowindex]["Mesial"] = M;
                        dtb_toothtable.Rows[rowindex]["Distal"] = D;
                        dtb_toothtable.Rows[rowindex]["Buccal"] = B;
                        dtb_toothtable.Rows[rowindex]["Lingual"] = L;
                    }
                }
                else
                {
                    dtb_toothtable.Rows.Add("", "", re_move, re_move);
                    tooth_flag = false;
                }
            }
            else
            {

                dtb_toothtable.Rows.Add("", "", re_move, re_move);
                tooth_flag = false;
            }
        }
        public void delete_tooth_frm_toothtable(string remove)
        {
            string tooth = remove;

            if (dtb_toothtable.Rows.Count > 0)
            {
                var items = tooth.Split(',');
                for (int i = 0; i < items.Length; i++)
                {
                    for (int j = 0; j < dtb_toothtable.Rows.Count; j++)
                    {
                        if (dtb_toothtable.Rows[j]["Tooth_Number"].ToString() == items[i].ToString())
                        {
                            dtb_toothtable.Rows.RemoveAt(j);
                        }
                    }
                }

                rowcnt = dtb_toothtable.Rows.Count;
            }
        }
        private void p23_Click(object sender, EventArgs e)
        {
            PictureBox pbox = (PictureBox)sender;
            Graphics g = pbox.CreateGraphics();
            string remove_tooth = "";
            string str = pbox.Name;
            string re_move = "",lat_remove="",s_remove="";// = str.Remove(0, 1);
            int r = 0;
            r = str.Count();
            if (chk_child_tooth.Checked==true)
            {
                 re_move = str.Remove(0, 3);
                if (re_move == "")
                {
                    MessageBox.Show("Please choose child tooth..", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                lat_remove = str.Remove(0, 1);
                s_remove = lat_remove.Remove(1,2);
            }
            else
            {
               
                if (r==5)
                {
                    lat_remove = str.Remove(0, 1);
                    re_move = lat_remove.Remove(1, 2);
                    s_remove = re_move;

                }
                else
                {
                    re_move = str.Remove(0, 1);
                    s_remove = re_move;
                }
                   
            }
            
            tooth_flag = true;
            if (pbox.Tag == null)//Sets a default color
            {
                if (chk_child_tooth.Checked == true)
                {
                    if (r == 5)
                    {
                        tooth_imgno += re_move + ",";
                        fill_toothTable(re_move);
                        pbox.Tag = Color.Red;
                        ControlPaint.DrawBorder(g, pbox.ClientRectangle, (Color)pbox.Tag, ButtonBorderStyle.Solid);
                    }
                        
                }
                else
                {
                    tooth_imgno += re_move + ",";
                    fill_toothTable(re_move);
                    pbox.Tag = Color.Red;
                    ControlPaint.DrawBorder(g, pbox.ClientRectangle, (Color)pbox.Tag, ButtonBorderStyle.Solid);
                }
                   
            }
            else
            {
                remove_tooth += tooth_imgno.Replace(re_move + ",", "");//deleting tooth
                tooth_imgno = remove_tooth;
                pbox.Tag = Color.White;
                ControlPaint.DrawBorder(g, pbox.ClientRectangle, (Color)pbox.Tag, ButtonBorderStyle.Solid);
                pbox.Tag = null;
                delete_tooth_frm_toothtable(re_move);
                Control[] contrl = this.Controls.Find("_" + re_move, true);
                {
                    if (contrl != null && contrl.Length > 0)
                    {
                        foreach (Control control in contrl)
                        {
                            if (control.GetType() == typeof(PictureBox))
                            {
                                PictureBox pictureBox = control as PictureBox;
                                pictureBox.Image = PappyjoeMVC.Properties.Resources.New_Image;
                                pictureBox.Tag = null;
                            }
                        }
                    }
                }
            }
            Control[] controls = this.Controls.Find("s" + s_remove.ToString(), true);
            if (controls != null && controls.Length > 0)
            {
                foreach (Control control in controls)
                {
                    if (control.GetType() == typeof(PictureBox))
                    {
                        PictureBox pictureBox = control as PictureBox;
                        Graphics g1 = pictureBox.CreateGraphics();
                        if (pictureBox.Tag == null)
                        {
                            pictureBox.Tag = Color.Red;
                            ControlPaint.DrawBorder(g1, pictureBox.ClientRectangle, (Color)pictureBox.Tag, ButtonBorderStyle.Solid);
                        }
                        else
                        {
                            pictureBox.Tag = Color.White;
                            ControlPaint.DrawBorder(g1, pictureBox.ClientRectangle, (Color)pictureBox.Tag, ButtonBorderStyle.Solid);
                            pictureBox.Tag = null;
                        }
                    }
                }
            }
        }
        public void remove_border(string tooth)
        {
            string str = tooth;
            var items = str.Split(',');
            for (int i = 0; i < items.Length; i++)
            {
                Control[] controls = this.Controls.Find("s" + items[i], true);

                if (controls != null && controls.Length > 0)
                {
                    foreach (Control control in controls)
                    {
                        if (control.GetType() == typeof(PictureBox))
                        {
                            PictureBox pictureBox = control as PictureBox;
                            Graphics g1 = pictureBox.CreateGraphics();
                            pictureBox.Tag = Color.White;
                            ControlPaint.DrawBorder(g1, pictureBox.ClientRectangle, (Color)pictureBox.Tag, ButtonBorderStyle.Solid);
                            pictureBox.Tag = null;
                        }
                    }
                }
                Control[] controls1 = this.Controls.Find("p" + items[i], true);
                if (controls1 != null && controls1.Length > 0)
                {
                    foreach (Control control in controls1)
                    {
                        if (control.GetType() == typeof(PictureBox))
                        {
                            PictureBox pictureBox = control as PictureBox;
                            Graphics g1 = pictureBox.CreateGraphics();
                            pictureBox.Tag = Color.White;
                            ControlPaint.DrawBorder(g1, pictureBox.ClientRectangle, (Color)pictureBox.Tag, ButtonBorderStyle.Solid);
                            pictureBox.Tag = null;
                        }
                    }
                }
                Control[] contrl = this.Controls.Find("_" + items[i], true);
                {
                    if (contrl != null && contrl.Length > 0)
                    {
                        foreach (Control control in contrl)
                        {
                            if (control.GetType() == typeof(PictureBox))
                            {
                                PictureBox pictureBox = control as PictureBox;
                                pictureBox.Image = PappyjoeMVC.Properties.Resources.New_Image;
                                pictureBox.Tag = null;
                            }
                        }
                    }
                }
            }
        }
        public void create_datatable()
        {
            dtb_toothtable.Columns.Clear();
            dtb_toothtable.Rows.Clear();
            dtb_toothtable.Columns.Add("Pt_id");
            dtb_toothtable.Columns.Add("procedure_id");
            dtb_toothtable.Columns.Add("Tooth_Number");
            dtb_toothtable.Columns.Add("Surface_Number");
            dtb_toothtable.Columns.Add("Occlual");
            dtb_toothtable.Columns.Add("Mesial");
            dtb_toothtable.Columns.Add("Distal");
            dtb_toothtable.Columns.Add("Buccal");
            dtb_toothtable.Columns.Add("Lingual");
        }

        public bool tooth_flag = false;
        public int found_tooth;
        private void spl_27_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox pbox = (PictureBox)sender;
            Point sPt = scaledPoint(pbox, e.Location);
            Bitmap bmp = (Bitmap)pbox.Image;
            string str = pbox.Name;
            int r = 0;
            //string re_move = str.Remove(0, 1);
            string re_move = "", lat_remove = "";// = str.Remove(0, 1);
            if (chk_child_tooth.Checked == true)
            {
                re_move = str.Remove(0, 3);
                if (re_move == "")
                {
                    MessageBox.Show("Please choose child tooth..", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                r = str.Count();
                if (r == 5)
                {
                    lat_remove = str.Remove(0, 1);
                    re_move = lat_remove.Remove(1, 2);

                }
                else
                {
                    re_move = str.Remove(0, 1);
                }
            }
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, pbox.Width - 0, pbox.Height - 0);
            Region rg = new Region(gp);
            pbox.Region = rg;
            if (check_surface(re_move) == 1)
            {
                found_tooth = 1;
                if (dtb_toothtable.Rows[rowindex]["Occlual"].ToString() == "" && dtb_toothtable.Rows[rowindex]["Mesial"].ToString() == "" && dtb_toothtable.Rows[rowindex]["Distal"].ToString() == "" && dtb_toothtable.Rows[rowindex]["Buccal"].ToString() == "" && dtb_toothtable.Rows[rowindex]["Lingual"].ToString() == "")
                {
                    B = "No";
                    M = "No";
                    L = "No";
                    D = "No";
                    O = "No";
                }
                Color c0 = bmp.GetPixel(sPt.X, sPt.Y);
                Fill4(bmp, sPt, c0, Color.CadetBlue, re_move);
                pbox.Image = bmp;
            }
            else
            {
                found_tooth = 0;
                MessageBox.Show("Please choose surface tooth", "Data missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public int check_surface(string num)
        {
            int found = 0;
            if (dtb_toothtable.Rows.Count > 0)
            {
                for (int i = 0; i < dtb_toothtable.Rows.Count; i++)
                {
                    if (dtb_toothtable.Rows[i]["Surface_Number"].ToString() == num && dtb_toothtable.Rows[i]["procedure_id"].ToString() == "")
                    {
                        found = 1;
                        rowindex = i;
                    }
                }
            }
            return found;
        }
        public int check_tooth_surface(string num)
        {
            int found = 0;
            for (int i = 0; i < dtb_toothtable.Rows.Count; i++)
            {
                if (dtb_toothtable.Rows[i]["Surface_Number"].ToString() == num && dtb_toothtable.Rows[i]["procedure_id"].ToString() == "")
                {
                    found = 1;
                    rowindex = i;
                }
            }
            return found;
        }
        public Point scaledPoint(PictureBox pbox, Point pt)
        {
            Size si = pbox.Image.Size;
            Size sp = pbox.ClientSize;             
            int left = 0;
            int top = 0;
            if (pbox.SizeMode == PictureBoxSizeMode.Normal ||
                pbox.SizeMode == PictureBoxSizeMode.AutoSize) return pt;
            if (pbox.SizeMode == PictureBoxSizeMode.CenterImage)
            {
                left = (sp.Width - si.Width) / 2;
                top = (sp.Height - si.Height) / 2;
                return new Point(pt.X - left, pt.Y - top);
            }
            if (pbox.SizeMode == PictureBoxSizeMode.Zoom)
            {
                if (1f * si.Width / si.Height < 1f * sp.Width / sp.Height)
                    left = (sp.Width - si.Width * sp.Height / si.Height) / 2;
                else
                    top = (sp.Height - si.Height * sp.Width / si.Width) / 2;
            }
            pt = new Point(pt.X - left, pt.Y - top);
            float scaleX = 1f * pbox.Image.Width / (pbox.ClientSize.Width - 2 * left);
            float scaleY = 1f * pbox.Image.Height / (pbox.ClientSize.Height - 2 * top);
            return new Point((int)(pt.X * scaleX), (int)(pt.Y * scaleY));
        }
        public string  B = "No", M = "No", L = "No", D = "No", O = "No", pb_box_name = "";

        private void _26_Click(object sender, EventArgs e)
        {

        }

        public bool flag_b = false, flag_m = false, flag_l = false, flag_d = false, flag_o = false;
        public void Fill4(Bitmap bmp, Point pt, Color c0, Color c1, string re_move)
        {
            string reference = "";
            Color cx = bmp.GetPixel(pt.X, pt.Y);
            Rectangle bmpRect = new Rectangle(Point.Empty, bmp.Size);
            Stack<Point> stack = new Stack<Point>();
            Stack<Point> stack1 = new Stack<Point>();
            int x0 = pt.X;
            int y0 = pt.Y;
            if (cx.GetBrightness() < 0.01f)
                return;
            else if (cx.GetBrightness() == 1.0f)
            {
                stack.Push(new Point(x0, y0));
                while (stack.Any())
                {
                    Point p = stack.Pop();
                    if (!bmpRect.Contains(p)) continue;
                    cx = bmp.GetPixel(p.X, p.Y);
                    if (cx == c0)
                    {
                        if (x0 >= 38 && x0 <= 164 && y0 >= 17 && y0 <= 61)   //(x0 >= 54 && x0 <= 172 && y0 >= 17 && y0 <= 61)
                        {
                            if (x0 == 157 && y0 == 61 || x0 == 39 && y0 == 61 || x0 == 39 && y0 == 52 || x0 == 47 && y0 == 61 || x0 == 102 && y0 == 61 || x0 == 110 && y0 == 61 || x0 == 94 && y0 == 61 || x0 == 117 && y0 == 61 || x0 == 125 && y0 == 61)
                            { }
                            else
                                B = "Yes";
                        }
                        if (x0 >= 157 && x0 <= 196 && y0 >= 43 && y0 <= 148)// (x0 >= 157 && x0 <= 180 && y0 >= 43 && y0 <= 148) 
                        {
                            if (x0 == 157 && y0 == 105 || x0 == 157 && y0 == 113 || x0 == 157 && y0 == 96) { }
                            else
                                D = "Yes";
                        }
                        if (x0 >= 47 && x0 <= 172 && y0 >= 148 && y0 <= 201)// (x0 >= 55 && x0 <= 165 && y0 >= 157 && y0 <= 192)
                        {
                            if (x0 == 55 && y0 == 148 || x0 == 62 && y0 == 148 || x0 == 55 && y0 == 157 || x0 == 125 && y0 == 148 || x0 == 110 && y0 == 148 || x0 == 94 && y0 == 148 || x0 == 117 && y0 == 148)
                            { }
                            else
                                L = "Yes";
                        }
                        if (x0 >= 15 && x0 <= 62 && y0 >= 52 && y0 <= 166)
                        {
                            if (x0 == 62 && y0 == 113 || x0 == 62 && y0 == 105 || x0 == 62 && y0 == 96 || x0 == 55 && y0 == 52)
                            { }
                            else
                                M = "Yes";
                        }
                        if (x0 >= 62 && x0 <= 157 && y0 >= 61 && y0 <= 153)
                        {
                            if (x0 == 62 && y0 == 78 || x0 == 62 && y0 == 70 || x0 == 62 && y0 == 131 || x0 == 62 && y0 == 140 || x0 == 157 && y0 == 140 || x0 == 78 && y0 == 61 || x0 == 157 && y0 == 70 || x0 == 157 && y0 == 61 || x0 == 149 && y0 == 148 || x0 == 78 && y0 == 148 || x0 == 141 && y0 == 148) { }
                            else
                                O = "Yes";
                        }
                        bmp.SetPixel(p.X, p.Y, c1);
                        stack.Push(new Point(p.X, p.Y + 1));
                        stack.Push(new Point(p.X, p.Y - 1));
                        stack.Push(new Point(p.X + 1, p.Y));
                        stack.Push(new Point(p.X - 1, p.Y));
                    }
                }
                fill_toothTable(re_move);
            }
            else
            {
                stack1.Push(new Point(x0, y0));
                while (stack1.Any())
                {
                    Point p = stack1.Pop();
                    if (!bmpRect.Contains(p)) continue;
                    cx = bmp.GetPixel(p.X, p.Y);
                    if (cx == c0)
                    {
                        if (x0 >= 55 && x0 <= 172 && y0 >= 17 && y0 <= 52)
                        {
                            B = "No";

                        }
                        else if (x0 >= 157 && x0 <= 180 && y0 >= 43 && y0 <= 148)
                        {
                            D = "No";
                        }
                        else if (x0 >= 55 && x0 <= 165 && y0 >= 157 && y0 <= 192)
                        {
                            L = "No";
                        }
                        else if (x0 >= 39 && x0 <= 62 && y0 >= 52 && y0 <= 166)
                        {
                            M = "No";
                        }
                        else if (x0 >= 70 && x0 <= 157 && y0 >= 78 && y0 <= 153)
                        {
                            O = "No";
                        }
                        bmp.SetPixel(p.X, p.Y, Color.White);
                        stack1.Push(new Point(p.X, p.Y + 1));
                        stack1.Push(new Point(p.X, p.Y - 1));
                        stack1.Push(new Point(p.X + 1, p.Y));
                        stack1.Push(new Point(p.X - 1, p.Y));
                    }
                }
                fill_toothTable(re_move);
            }
        }

        private void spl_17_Click(object sender, EventArgs e)
        {

        }
    }
}
