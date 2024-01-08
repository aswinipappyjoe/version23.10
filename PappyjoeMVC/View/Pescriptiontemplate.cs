using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PappyjoeMVC.Model;

namespace PappyjoeMVC.View
{
    public partial class Pescriptiontemplate : Form
    {
        public Pescriptiontemplate()
        {
            InitializeComponent();
        }
        Connection db = new Connection();
        public string doctor_id = "", patient_id = "0";
        string doctorname = "", patientname = "";
        string id1, drug_type = "";
        string idtemp = "0";
        private void Pescriptiontemplate_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1068, 733); 
            DataTable dt = db.table("select * from tbl_unit");
            cmbStrungthTemp.DataSource = dt;
            cmbStrungthTemp.DisplayMember = "name";
            cmbStrungthTemp.ValueMember = "id";
            if (cmbStrungthTemp.Items.Count > 1)
            {
                cmbStrungthTemp.SelectedIndex = 0;
            }

            cmbTempDuration.SelectedIndex = 0;
            DataTable dt2 = db.table("select id,templates from tbl_templates_main ORDER BY id DESC");
            //dataGridView2.DataSource = dt2;
            fill_Prescrptntemplate(dt2);
            DataTable dt4 = db.table("select id,CONCAT(name,' ', type ) as name, CONCAT(strength_gr ,' ' , strength ) as type,inventory_id from tbl_adddrug ORDER BY id DESC limit 50");
            string strstock = "";
            presdruggrid.Columns.Add("id", "xt");
            presdruggrid.Columns.Add("drug", "xt");
            presdruggrid.Columns.Add("stock", "xt");
            presdruggrid.Columns[0].Visible = false;
            presdruggrid.Columns[1].Width = 200;
            presdruggrid.Columns[2].Width = 150;
            presdruggrid.Columns[3].Visible = false;
            for (int j = 0; j < dt4.Rows.Count; j++)
                presdruggrid.Columns[4].Visible = false;
            for (int j = 0; j < dt4.Rows.Count; j++)
            {
                if (dt4.Rows[j]["inventory_id"].ToString() == "0")
                {
                    strstock = "(Not sold)";
                }
                else
                {
                    strstock = "(Not sold)";
                    //DataTable dtstock = db.table("select stock from tbl_inventory_item where item_code='" + dt4.Rows[j]["inventory_id"].ToString() + "' ORDER BY id DESC");
                    DataTable dtstock = db.table("Select A.item_code,A.item_name,(select sum(Qty) from tbl_BatchNumber where item_code= A.item_code) 'Stock' from tbl_ITEMS A WHERE A.ID='" + dt4.Rows[j]["inventory_id"].ToString() + "' order by item_name");
                    if (dtstock.Rows.Count > 0)
                    {
                        string dou_stock = dtstock.Rows[0]["Stock"].ToString();
                        // Convert.ToInt16(dtstock.Rows[0]["Stock"].ToString()
                        if (dou_stock != "")
                        {
                            strstock = "(In stock)";
                        }
                        else
                        {
                            strstock = "(Out-of-stock)";
                        }
                    }
                }
                presdruggrid.Rows.Add(dt4.Rows[j]["id"].ToString(), dt4.Rows[j]["name"].ToString(), dt4.Rows[j]["type"].ToString() + "  " + strstock);
                if (strstock == "(Not sold)")
                { presdruggrid.Rows[j].Cells[2].Style.ForeColor = Color.Red; }
                else if (strstock == "(Out-of-stock)")
                { presdruggrid.Rows[j].Cells[2].Style.ForeColor = Color.Blue; }
                presdruggrid.Rows[j].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            DataTable patient = db.table("select pt_name from tbl_patient where id='" + patient_id + "'");
            if (patient.Rows.Count > 0)
            {
                patientname= patient.Rows[0][0].ToString();
            }
            DataTable docnam = db.table("select doctor_name from tbl_doctor Where id='" + doctor_id + "'");
            if (docnam.Rows.Count > 0)
            {
              doctorname= docnam.Rows[0][0].ToString();
            }
            btnupdate.Location = savebut.Location;
            dataGridView_templatenew.Location = new Point(340, 160);

        }

        private void searchtext2_TextChanged(object sender, EventArgs e)
        {
            if (searchtext2.Text != "")
            {
                DataTable dtb = db.table("select id,templates from tbl_templates_main  where templates like '%" + searchtext2.Text + "%'  ORDER BY id DESC");
                if (dtb.Rows.Count > 0)
                {
                    dataGridView2.Rows.Clear();
                    fill_Prescrptntemplate(dtb);
                                    }
                else
                {
                    dataGridView2.Rows.Clear();
                }
            }
            else
            {
                DataTable dtb1 = db.table("select id,templates from tbl_templates_main ORDER BY id DESC");
                dataGridView2.Rows.Clear();
                fill_Prescrptntemplate(dtb1);
            }
            
        }

        private void txt_Prescrptn_TextChanged(object sender, EventArgs e)
        {
            if (txt_Prescrptn.Text != "")
            {
                DataTable dtb = db.table("select id,CONCAT(name,' ', type ) as name, CONCAT(strength_gr ,' ' , strength ) as type,inventory_id from tbl_adddrug where name like '%" + txt_Prescrptn.Text + "%'  ORDER BY id DESC Limit 30");
                if (dtb.Rows.Count > 0)
                {
                    presdruggrid.Rows.Clear();
                    fill_DrugPrescrptn(dtb);

                }
                else
                {
                    presdruggrid.Rows.Clear();
                }
            }
            else
            {
                DataTable dtb1 = db.table("select id,CONCAT(name,' ', type ) as name, CONCAT(strength_gr ,' ' , strength ) as type,inventory_id from tbl_adddrug ORDER BY id DESC Limit 30");
                presdruggrid.Rows.Clear();
                fill_DrugPrescrptn(dtb1);
            }
        }
        public void fill_Prescrptntemplate(DataTable dt5)
        {
            for (int jj = 0; jj < dt5.Rows.Count; jj++)
            {
                dataGridView2.Rows.Add(dt5.Rows[jj]["id"].ToString(), dt5.Rows[jj]["templates"].ToString());
            }
        }
        public void fill_DrugPrescrptn(DataTable dt4)
        {

            string strstock = "";
            for (int j = 0; j < dt4.Rows.Count; j++)
                presdruggrid.Columns[4].Visible = false;
            for (int j = 0; j < dt4.Rows.Count; j++)
            {
                if (dt4.Rows[j]["inventory_id"].ToString() == "0")
                {
                    strstock = "(Not sold)";
                }
                else
                {
                    strstock = "(Not sold)";

                    DataTable dtstock = db.table("Select A.item_code,A.item_name,(select sum(Qty) from tbl_BatchNumber where item_code= A.item_code) 'Stock' from tbl_ITEMS A WHERE A.ID='" + dt4.Rows[j]["inventory_id"].ToString() + "' order by item_name");
                    if (dtstock.Rows.Count > 0)
                    {
                        string dou_stock = dtstock.Rows[0]["Stock"].ToString();
                        if (dou_stock != "")
                        {
                            strstock = "(In stock)";
                        }
                        else
                        {
                            strstock = "(Out-of-stock)";
                        }
                    }
                }
                presdruggrid.Rows.Add(dt4.Rows[j]["id"].ToString(), dt4.Rows[j]["name"].ToString(), dt4.Rows[j]["type"].ToString() + "  " + strstock);
                if (strstock == "(Not sold)")
                { presdruggrid.Rows[j].Cells[2].Style.ForeColor = Color.Red; }
                else if (strstock == "(Out-of-stock)")
                { presdruggrid.Rows[j].Cells[2].Style.ForeColor = Color.Blue; }
                presdruggrid.Rows[j].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Rows.Count > 0 && e.RowIndex >= 0)
            {
                //try
                //{
                dataGridView_templatenew.Rows.Clear();
                int r = e.RowIndex;
                idtemp = "0";
                idtemp = dataGridView2.Rows[r].Cells[0].Value.ToString();
                tempnametext.Text = dataGridView2.Rows[r].Cells[1].Value.ToString();
               if(e.ColumnIndex.ToString()=="2")
               {

                   DataTable dt = db.table("select drug_name,strength,strength_gr,duration,duration_period,morning,noon,night,add_instruction,food,drug_id,drug_type,status from tbl_template where temp_id ='" + idtemp + "'");
                   for (int i = 0; i < dt.Rows.Count; i++)
                   {

                       dataGridView_templatenew.Rows.Add(dt.Rows[i]["drug_name"].ToString(), dt.Rows[i]["strength"].ToString(), dt.Rows[i]["strength_gr"].ToString(), dt.Rows[i]["duration"].ToString(), dt.Rows[i]["duration_period"].ToString(), dt.Rows[i]["morning"].ToString(), dt.Rows[i]["noon"].ToString(), dt.Rows[i]["night"].ToString(), dt.Rows[i]["food"].ToString(), dt.Rows[i]["add_instruction"].ToString(), dt.Rows[i]["drug_id"].ToString(), dt.Rows[i]["drug_type"].ToString());
                       dataGridView_templatenew.Rows[dataGridView_templatenew.Rows.Count - 1].Cells[12].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                       dataGridView_templatenew.Rows[dataGridView_templatenew.Rows.Count - 1].Height = 30;
                       img1.ImageLayout = DataGridViewImageCellLayout.Normal;
                       dataGridView_templatenew.Rows[dataGridView_templatenew.Rows.Count - 1].Cells[13].Value = dt.Rows[i]["status"].ToString();
                       dataGridView_templatenew.Columns[12].Visible = false;
                   }
                   lbltemplatename.Text = "TEMPLATE NAME";
                   lbltemlateviewname.Text = "TEMPLATE NAME :" + tempnametext.Text;
                   lbltemlateviewname.Visible = true;
                   templatepanel1.Visible = false;
                   dataGridView_templatenew.Location = new Point(340, 160);
                   savebut.Visible = false;
                   btnupdate.Visible = false;
                   this.Size = new Size(1068, 733); 

               }
               if (e.ColumnIndex.ToString() == "3")
               {

                   DataTable dt = db.table("select drug_name,strength,strength_gr,duration,duration_period,morning,noon,night,add_instruction,food,drug_id,drug_type,status from tbl_template where temp_id ='" + idtemp + "'");
                   for (int i = 0; i < dt.Rows.Count; i++)
                   {

                       dataGridView_templatenew.Rows.Add(dt.Rows[i]["drug_name"].ToString(), dt.Rows[i]["strength"].ToString(), dt.Rows[i]["strength_gr"].ToString(), dt.Rows[i]["duration"].ToString(), dt.Rows[i]["duration_period"].ToString(), dt.Rows[i]["morning"].ToString(), dt.Rows[i]["noon"].ToString(), dt.Rows[i]["night"].ToString(), dt.Rows[i]["food"].ToString(), dt.Rows[i]["add_instruction"].ToString(), dt.Rows[i]["drug_id"].ToString(), dt.Rows[i]["drug_type"].ToString());
                       dataGridView_templatenew.Rows[dataGridView_templatenew.Rows.Count - 1].Cells[12].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                       dataGridView_templatenew.Rows[dataGridView_templatenew.Rows.Count - 1].Height = 30;
                       img1.ImageLayout = DataGridViewImageCellLayout.Normal;
                       dataGridView_templatenew.Rows[dataGridView_templatenew.Rows.Count - 1].Cells[13].Value = dt.Rows[i]["status"].ToString();
                       dataGridView_templatenew.Columns[12].Visible = true;
                   }
                   lbltemplatename.Text = "TEMPLATE NAME";
                   lbltemlateviewname.Visible = false;
                   templatepanel1.Visible = true;
                   dataGridView_templatenew.Location = new Point(340, 307);
                   savebut.Visible = false;
                   btnupdate.Visible = true;
                   this.Size = new Size(1416, 733);
               }
               if (e.ColumnIndex.ToString() == "4")
               {
                   DialogResult yesno=MessageBox.Show("Are you sure you wnat to delete this Prescription Template..?","DELETE",MessageBoxButtons.YesNo,MessageBoxIcon.Stop);
                   if (yesno == DialogResult.Yes)
                   {

                       int i = db.execute("delete FROM tbl_templates_main where id='" + idtemp + "'");
                       int ii = db.execute("delete FROM tbl_template where temp_id='" + idtemp + "'");
                       if (i > 0)
                       {
                           DataTable dtb1 = db.table("select id,templates from tbl_templates_main ORDER BY id DESC");
                           dataGridView2.Rows.Clear();
                           fill_Prescrptntemplate(dtb1);
                       }
                       lbltemlateviewname.Visible = false;
                       templatepanel1.Visible = false;
                       savebut.Visible = false;
                       btnupdate.Visible = false;
                   }
               }

                   
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
        }

        private void BtnAddInstruction_Click(object sender, EventArgs e)
        {
            richTxtTempInstruction.Visible = true;
        }

        private void presdruggrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            id1 = presdruggrid.Rows[r].Cells[0].Value.ToString();
            DataTable dt = db.table("select name,strength,strength_gr,type from tbl_adddrug where id='" + id1 + "'");
            if (dt.Rows.Count > 0)
            {
                txtDrugName.Text = dt.Rows[0][0].ToString();
                txttemplteStrugthNo.Text = dt.Rows[0][2].ToString();
                cmbStrungthTemp.Text = dt.Rows[0][1].ToString();
                drug_type = dt.Rows[0][3].ToString();
            }
            richTxtTempInstruction.Hide();
            richTxtTempInstruction.Text = "";
            radioButtonTempAftrFood.Checked = false;
            radioButtonTempBfrFood.Checked = false;
        }

        private void BtnAddToList_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txtDrugName.Text != "DRUG NAME" && txtDrugName.Text != "")
                //{
                    string food = "",drugname="";
                string mrng = "", noon = "", durat = "", night = "",drg_id="",strength="";
                if (radioButtonTempBfrFood.Checked)
                    {
                        food = radioButtonTempBfrFood.Text.ToString();
                    }
                    else if (radioButtonTempAftrFood.Checked)
                    {
                        food = radioButtonTempAftrFood.Text.ToString();
                    }
                    string strstatus = "1";
                    string Note = "";
                    string NoteData = "";
                    NoteData = richTxtTempInstruction.Text;
                    Note = NoteData.Replace("'", " ");
                    dataGridView_templatenew.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView_templatenew.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if(txtDrugName.Text== "DRUG NAME")
                {
                    drugname = "";
                    strength = "";
                }
                else
                {
                    drugname = txtDrugName.Text;
                    strength = cmbStrungthTemp.Text;
                }
                if (numericUpDownTempMorning.Value == 0)
                {
                    mrng = "";
                }
                else
                {
                    mrng = numericUpDownTempMorning.Value.ToString();
                }
                if (numericUpDownTempNoon.Value == 0)
                {
                    noon = "";
                }
                else
                {
                    noon = numericUpDownTempNoon.Value.ToString();
                }
                if (numericUpDownTempNight.Value == 0)
                {
                    night = "";
                }
                else
                {
                    night = numericUpDownTempNight.Value.ToString();
                }
                if (numericUpDownTempDuration.Value == 0)
                {
                    durat = "";
                }
                else
                {
                    durat = numericUpDownTempDuration.Value.ToString();
                }
                if(id1!="" && id1 !=null)
                {
                    drg_id = id1;
                }
                else
                {
                    drg_id = "";
                }
                if (txttemplteStrugthNo.Text=="")
                { }
                dataGridView_templatenew.Rows.Add(drugname, txttemplteStrugthNo.Text, strength, durat, cmbTempDuration.Text, mrng, noon, night, food, Note, drg_id, drug_type);
                    dataGridView_templatenew.Rows[dataGridView_templatenew.Rows.Count - 1].Cells[12].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                    dataGridView_templatenew.Rows[dataGridView_templatenew.Rows.Count - 1].Height = 30;
                    img1.ImageLayout = DataGridViewImageCellLayout.Normal;
                    dataGridView_templatenew.Rows[dataGridView_templatenew.Rows.Count - 1].Cells[13].Value = strstatus;

                    radioButtonTempBfrFood.Checked = false;
                    radioButtonTempAftrFood.Checked = false;
                //}
            }
            catch
            {
            }
        }

        private void addtemplatebut_Click(object sender, EventArgs e)
        {
            dataGridView_templatenew.Columns[12].Visible = true;
            lbltemplatename.Text = "ENTER YOUR TEMPLATE NAME";
            tempnametext.Text = "";
            savebut.Visible = true;
            btnupdate.Visible = false;
            lbltemlateviewname.Visible = false;
            dataGridView_templatenew.Location = new Point(340, 307);
            templatepanel1.Visible = true;
            dataGridView_templatenew.Rows.Clear();
            this.Size = new Size(1416, 733);
           
        }

        private void dataGridView_templatenew_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView_templatenew.Rows.Count > 0)
                {
                    if (e.ColumnIndex == 12)
                    {
                        DialogResult res = MessageBox.Show("Are you sure you want to delete..?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (res == DialogResult.Yes)
                        {
                            dataGridView_templatenew.Rows.RemoveAt(this.dataGridView_templatenew.SelectedRows[0].Index);
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void savebut_Click(object sender, EventArgs e)
        {
            int count = dataGridView_templatenew.Rows.Count;
            if (tempnametext.Text != "" && count >= 1)
            {
                DataTable dt_check = db.table("select templates from tbl_templates_main where templates='" + tempnametext.Text + "'");
                if (dt_check.Rows.Count == 0)
                {
                    
                    db.execute("insert into tbl_templates_main(templates) values('" + tempnametext.Text + "')");
                    DataTable dt = db.table("select id from tbl_templates_main where templates='" + tempnametext.Text + "'");
                    for (int i = 0; i < count; i++)
                    {
                        db.execute("insert into tbl_template (temp_id,pt_id,pt_name,dr_id,dr_name,date,drug_name,strength,strength_gr,duration,morning,noon,night,food,add_instruction,drug_type,drug_id,pres_id,duration_period,status) values('" + dt.Rows[0][0].ToString() + "','" + patient_id + "','" + patientname + "','" + doctor_id + "','" + doctorname + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + dataGridView_templatenew[0, i].Value.ToString() + "','" + dataGridView_templatenew[1, i].Value.ToString() + "','" + dataGridView_templatenew[2, i].Value.ToString() + "','" + dataGridView_templatenew[3, i].Value.ToString() + "','" + dataGridView_templatenew[5, i].Value.ToString() + "','" + dataGridView_templatenew[6, i].Value.ToString() + "','" + dataGridView_templatenew[7, i].Value.ToString() + "','" + dataGridView_templatenew[8, i].Value.ToString() + "','" + dataGridView_templatenew[9, i].Value.ToString() + "','" + dataGridView_templatenew[11, i].Value.ToString() + "','" + dataGridView_templatenew[10, i].Value.ToString() + "','','" + dataGridView_templatenew[4, i].Value.ToString() + "','" + dataGridView_templatenew[13, i].Value.ToString() + "')");
                    }

                    DataTable dtb1 = db.table("select id,templates from tbl_templates_main ORDER BY id DESC");
                    dataGridView2.Rows.Clear();
                    fill_Prescrptntemplate(dtb1);
                    lbltemlateviewname.Text = "TEMPLATE NAME :" + tempnametext.Text;
                    lbltemlateviewname.Visible = true;
                    templatepanel1.Visible = false;
                    dataGridView_templatenew.Location = new Point(340, 160);
                    savebut.Visible = false;
                    btnupdate.Visible = false;
                    this.Size = new Size(1068, 733); 
                }
                else
                {
                    MessageBox.Show("Template name already exist, please enter template name ", "Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tempnametext.Text = "";
                    tempnametext.Focus();
                }
            }
            else
            {
                MessageBox.Show("Please Enter the Template name and Drug(s)..!", "Empty field", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            int count = dataGridView_templatenew.Rows.Count;
            if (tempnametext.Text != "" && count >= 1)
            {
                DataTable dt_check = db.table("select templates from tbl_templates_main where templates='" + tempnametext.Text + "' and id<>'" + idtemp +"'" );
                if (dt_check.Rows.Count == 0)
                {

                    db.execute("update tbl_templates_main set templates='" + tempnametext.Text + "' where id='" + idtemp + "'");
                    int ii = db.execute("delete FROM tbl_template where temp_id='" + idtemp + "'");
                   
                    for (int i = 0; i < count; i++)
                    {
                        db.execute("insert into tbl_template (temp_id,pt_id,pt_name,dr_id,dr_name,date,drug_name,strength,strength_gr,duration,morning,noon,night,food,add_instruction,drug_type,drug_id,pres_id,duration_period,status) values('" + idtemp + "','" + patient_id + "','" + patientname + "','" + doctor_id + "','" + doctorname + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + dataGridView_templatenew[0, i].Value.ToString() + "','" + dataGridView_templatenew[1, i].Value.ToString() + "','" + dataGridView_templatenew[2, i].Value.ToString() + "','" + dataGridView_templatenew[3, i].Value.ToString() + "','" + dataGridView_templatenew[5, i].Value.ToString() + "','" + dataGridView_templatenew[6, i].Value.ToString() + "','" + dataGridView_templatenew[7, i].Value.ToString() + "','" + dataGridView_templatenew[8, i].Value.ToString() + "','" + dataGridView_templatenew[9, i].Value.ToString() + "','" + dataGridView_templatenew[11, i].Value.ToString() + "','" + dataGridView_templatenew[10, i].Value.ToString() + "','','" + dataGridView_templatenew[4, i].Value.ToString() + "','" + dataGridView_templatenew[13, i].Value.ToString() + "')");
                    }
                    DataTable dtb1 = db.table("select id,templates from tbl_templates_main ORDER BY id DESC");
                    dataGridView2.Rows.Clear();
                    fill_Prescrptntemplate(dtb1);
                    lbltemlateviewname.Text = "TEMPLATE NAME :" + tempnametext.Text;
                    lbltemlateviewname.Visible = true;
                    templatepanel1.Visible = false;
                    dataGridView_templatenew.Location = new Point(340, 160);
                    savebut.Visible = false;
                    btnupdate.Visible = false;
                    this.Size = new Size(1068, 733); 
                }
                else
                {
                    MessageBox.Show("Template name already exist, please enter template name ", "Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tempnametext.Text = "";
                    tempnametext.Focus();
                }
            }
            else
            {
                MessageBox.Show("Please Enter the Template name and Drug(s)..!", "Empty field", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
