using PappyjoeMVC.Controller;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PappyjoeMVC.View
{
    public partial class Add_Labwork : Form
    {
        public Add_Labwork()
        {
            InitializeComponent();
        }
        bool flag;
        public static string ID = "";
        Add_Labwork_controller ctrlr = new Add_Labwork_controller();
        public string patient_id = "", doctor_id = "", checkvalue = "", ids = "", r = "", f = "";
        public string[] teeth = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        public DataTable dt_test_for_invoice = new DataTable();
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                if (MessageBox.Show("Delete this Item.. Confirm?", "Remove Item", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.dataGridView3.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        public void maxid(string iddt)
        {
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                int a = i + 1;
                int k = 0;
                if (iddt != "")
                {
                    k = Convert.ToInt32(iddt);
                }
                else
                {
                    k = 1;
                }
                this.ctrlr.labresult(k.ToString(), a.ToString(), dataGridView3.Rows[i].Cells["mainid"].Value.ToString(), dataGridView3.Rows[i].Cells["testid"].Value.ToString(), Convert.ToInt32(patient_id).ToString(), dataGridView3.Rows[i].Cells["units"].Value.ToString(), dataGridView3.Rows[i].Cells["normalvalue"].Value.ToString(), dataGridView3.Rows[i].Cells["temp_id"].Value.ToString(), dataGridView3.Rows[i].Cells["template_type"].Value.ToString());

            }
        }
        //dental rb
        public void dentallab(DataTable dt)
        {
            cmbShade.DisplayMember = "shade";
            cmbShade.ValueMember = "id";
            cmbShade.DataSource = dt;
            cmbAlloytype.DisplayMember = "aloytype";
            cmbAlloytype.ValueMember = "id";
            cmbAlloytype.DataSource = dt;
            dgvdentalwork.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvdentalwork.Rows.Add();
                dgvdentalwork.Rows[i].Cells["id"].Value = dt.Rows[i]["id"].ToString();
                dgvdentalwork.Rows[i].Cells["WorkType"].Value = dt.Rows[i]["work_type"].ToString();
                dgvdentalwork.Rows[i].Cells["Work"].Value = dt.Rows[i]["work_name"].ToString();
                dgvdentalwork.Rows[i].Cells["shade"].Value = dt.Rows[i]["shade"].ToString();
                dgvdentalwork.Rows[i].Cells["aloytype"].Value = dt.Rows[i]["aloytype"].ToString();
            }
            //dgvdentalwork.DataSource = dt;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButton2.Checked == true)
                {
                    radioButton1.Checked = false;
                    pnlMedlab.Hide();
                    pnlDental.Show();
                    pnladddental.Show();
                    //pnladddental.Location = new Point(1022, 137);
                    c.Hide();
                    DataTable dt = this.ctrlr.dentallab();
                    dentallab(dt);
                }
                else
                {
                    radioButton2.Checked = false;
                    pnlDental.Hide();
                    pnladddental.Hide();
                    //pnlMedlab.Show();
                    pnlMedlab.Visible = true;
                    //pnlMedlab.Location = new Point(2, 137);
                    pnlMedlab.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
                    //c.Show();
                    c.Visible = true;
                    //c.Location = new Point(1022, 137);
                    c.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
                    DataTable tbshade = this.ctrlr.Lab_Medi_TemplateMain();
                    dataGridView2.DataSource = tbshade;
                    checkvalue = "1";
                    DataTable tblab = this.ctrlr.getLabdata();
                    combolab.DisplayMember = "labname";
                    combolab.ValueMember = "id";
                    combolab.DataSource = tblab;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked == true)
                {
                    radioButton2.Checked = false;
                    //pnlDental.Show();
                    pnladddental.Hide();
                    pnlMedlab.Show();
                    //pnlMedlab.Visible = true;
                    pnlMedlab.Location = new Point(4, 90);
                    //pnlMedlab.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right |AnchorStyles.Left);
                    c.Show();
                    c.Visible = true;
                    //c.Location = new Point(1028, 141);
                    //c.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
                    DataTable tbshade = this.ctrlr.Lab_Medi_TemplateMain();
                    dataGridView2.DataSource = tbshade;
                    checkvalue = "1";
                    DataTable tblab = this.ctrlr.getLabdata();
                    combolab.DisplayMember = "labname";
                    combolab.ValueMember = "id";
                    combolab.DataSource = tblab;
                }
                else
                {
                    radioButton1.Checked = false;
                    pnlMedlab.Hide();
                    pnlDental.Show();
                    pnladddental.Show(); 
                    //pnladddental.Location= new Point(1022, 137);
                    c.Hide();
                    DataTable dt = this.ctrlr.dentallab();
                    dentallab(dt);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
        private void button2_Click(object sender, EventArgs e)
        {
            var form2 = new PappyjoeMVC.View.LabWorks();
            form2.doctor_id = doctor_id;
            form2.patient_id = patient_id;
            openform(form2);
        }

        public void listeeth()
        {
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (toothno.Length > 2) { label15.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label15.Text = ""; }
        }
        private void chk18_CheckedChanged(object sender, EventArgs e)
        {
            if (chk18.Checked) { teeth[0] = "18"; } else { teeth[0] = ""; }
            listeeth();
        }
        private void chk17_CheckedChanged(object sender, EventArgs e)
        {
            if (chk17.Checked) { teeth[1] = "17"; } else { teeth[1] = ""; }
            listeeth();
        }
        private void chk16_CheckedChanged(object sender, EventArgs e)
        {
            if (chk16.Checked) { teeth[2] = "16"; } else { teeth[2] = ""; }
            listeeth();
        }
        private void chk15_CheckedChanged(object sender, EventArgs e)
        {
            if (chk15.Checked) { teeth[3] = "15"; } else { teeth[3] = ""; }
            listeeth();
        }
        private void chk14_CheckedChanged(object sender, EventArgs e)
        {
            if (chk14.Checked) { teeth[4] = "14"; } else { teeth[4] = ""; }
            listeeth();
        }
        private void chk13_CheckedChanged(object sender, EventArgs e)
        {
            if (chk13.Checked) { teeth[5] = "13"; } else { teeth[5] = ""; }
            listeeth();
        }
        private void chk12_CheckedChanged(object sender, EventArgs e)
        {
            if (chk12.Checked) { teeth[6] = "12"; } else { teeth[6] = ""; }
            listeeth();
        }
        private void chk11_CheckedChanged(object sender, EventArgs e)
        {
            if (chk11.Checked) { teeth[7] = "11"; } else { teeth[7] = ""; }
            listeeth();
        }
        private void chk21_CheckedChanged(object sender, EventArgs e)
        {
            if (chk21.Checked) { teeth[8] = "21"; } else { teeth[8] = ""; }
            listeeth();
        }
        private void chk22_CheckedChanged(object sender, EventArgs e)
        {
            if (chk22.Checked) { teeth[9] = "22"; } else { teeth[9] = ""; }
            listeeth();
        }
        private void chk23_CheckedChanged(object sender, EventArgs e)
        {
            if (chk23.Checked) { teeth[10] = "23"; } else { teeth[10] = ""; }
            listeeth();
        }
        private void chk24_CheckedChanged(object sender, EventArgs e)
        {
            if (chk24.Checked) { teeth[11] = "24"; } else { teeth[11] = ""; }
            listeeth();
        }
        private void chk25_CheckedChanged(object sender, EventArgs e)
        {
            if (chk25.Checked) { teeth[12] = "25"; } else { teeth[12] = ""; }
            listeeth();
        }
        private void chk26_CheckedChanged(object sender, EventArgs e)
        {
            if (chk26.Checked) { teeth[13] = "26"; } else { teeth[13] = ""; }
            listeeth();
        }
        private void chk27_CheckedChanged(object sender, EventArgs e)
        {
            if (chk27.Checked) { teeth[14] = "27"; } else { teeth[14] = ""; }
            listeeth();
        }
        private void chk28_CheckedChanged(object sender, EventArgs e)
        {
            if (chk28.Checked) { teeth[15] = "28"; } else { teeth[15] = ""; }
            listeeth();
        }
        private void chk48_CheckedChanged(object sender, EventArgs e)
        {
            if (chk48.Checked) { teeth[31] = "48"; } else { teeth[31] = ""; }
            listeeth();
        }
        private void chk47_CheckedChanged(object sender, EventArgs e)
        {
            if (chk47.Checked) { teeth[30] = "47"; } else { teeth[30] = ""; }
            listeeth();
        }
        private void chk46_CheckedChanged(object sender, EventArgs e)
        {
            if (chk46.Checked) { teeth[29] = "46"; } else { teeth[29] = ""; }
            listeeth();
        }
        private void chk45_CheckedChanged(object sender, EventArgs e)
        {
            if (chk45.Checked) { teeth[28] = "45"; } else { teeth[28] = ""; }
            listeeth();
        }
        private void chk44_CheckedChanged(object sender, EventArgs e)
        {
            if (chk44.Checked) { teeth[27] = "44"; } else { teeth[27] = ""; }
            listeeth();
        }
        private void chk43_CheckedChanged(object sender, EventArgs e)
        {
            if (chk43.Checked) { teeth[26] = "43"; } else { teeth[26] = ""; }
            listeeth();
        }
        private void chk42_CheckedChanged(object sender, EventArgs e)
        {
            if (chk42.Checked) { teeth[25] = "42"; } else { teeth[25] = ""; }
            listeeth();
        }
        private void chk41_CheckedChanged(object sender, EventArgs e)
        {
            if (chk41.Checked) { teeth[24] = "41"; } else { teeth[24] = ""; }
            listeeth();
        }
        private void chk31_CheckedChanged(object sender, EventArgs e)
        {
            if (chk31.Checked) { teeth[23] = "31"; } else { teeth[23] = ""; }
            listeeth();
        }
        private void chk32_CheckedChanged(object sender, EventArgs e)
        {
            if (chk32.Checked) { teeth[22] = "32"; } else { teeth[22] = ""; }
            listeeth();
        }
        private void chk33_CheckedChanged(object sender, EventArgs e)
        {
            if (chk33.Checked) { teeth[21] = "33"; } else { teeth[21] = ""; }
            listeeth();
        }
        private void chk34_CheckedChanged(object sender, EventArgs e)
        {
            if (chk34.Checked) { teeth[20] = "34"; } else { teeth[20] = ""; }
            listeeth();
        }
        private void chk35_CheckedChanged(object sender, EventArgs e)
        {
            if (chk35.Checked) { teeth[19] = "35"; } else { teeth[19] = ""; }
            listeeth();
        }
        private void chk36_CheckedChanged(object sender, EventArgs e)
        {
            if (chk36.Checked) { teeth[18] = "36"; } else { teeth[18] = ""; }
            listeeth();
        }
        private void chk37_CheckedChanged(object sender, EventArgs e)
        {
            if (chk37.Checked) { teeth[17] = "37"; } else { teeth[17] = ""; }
            listeeth();
        }
        private void chk38_CheckedChanged(object sender, EventArgs e)
        {
            if (chk38.Checked) { teeth[16] = "38"; } else { teeth[16] = ""; }
            listeeth();
        }
        private void chk55_CheckedChanged(object sender, EventArgs e)
        {
            if (chk55.Checked) { teeth[32] = "55"; } else { teeth[32] = ""; }
            listeeth();
        }
        private void chk54_CheckedChanged(object sender, EventArgs e)
        {
            if (chk54.Checked) { teeth[33] = "54"; } else { teeth[33] = ""; }
            listeeth();
        }
        private void chk53_CheckedChanged(object sender, EventArgs e)
        {
            if (chk53.Checked) { teeth[34] = "53"; } else { teeth[34] = ""; }
            listeeth();
        }
        private void chk52_CheckedChanged(object sender, EventArgs e)
        {
            if (chk52.Checked) { teeth[35] = "52"; } else { teeth[35] = ""; }
            listeeth();
        }
        private void chk51_CheckedChanged(object sender, EventArgs e)
        {
            if (chk51.Checked) { teeth[36] = "51"; } else { teeth[36] = ""; }
            listeeth();
        }
        private void chk61_CheckedChanged(object sender, EventArgs e)
        {
            if (chk61.Checked) { teeth[37] = "61"; } else { teeth[37] = ""; }
            listeeth();
        }
        private void chk62_CheckedChanged(object sender, EventArgs e)
        {
            if (chk62.Checked) { teeth[38] = "62"; } else { teeth[38] = ""; }
            listeeth();
        }
        private void chk63_CheckedChanged(object sender, EventArgs e)
        {
            if (chk63.Checked) { teeth[39] = "63"; } else { teeth[39] = ""; }
            listeeth();
        }
        private void chk64_CheckedChanged(object sender, EventArgs e)
        {
            if (chk64.Checked) { teeth[40] = "64"; } else { teeth[40] = ""; }
            listeeth();
        }
        private void chk65_CheckedChanged(object sender, EventArgs e)
        {
            if (chk65.Checked) { teeth[41] = "65"; } else { teeth[41] = ""; }
            listeeth();
        }
        private void chk85_CheckedChanged(object sender, EventArgs e)
        {
            if (chk85.Checked) { teeth[51] = "85"; } else { teeth[51] = ""; }
            listeeth();
        }
        private void chk84_CheckedChanged(object sender, EventArgs e)
        {
            if (chk84.Checked) { teeth[50] = "84"; } else { teeth[50] = ""; }
            listeeth();
        }
        private void chk83_CheckedChanged(object sender, EventArgs e)
        {
            if (chk83.Checked) { teeth[49] = "83"; } else { teeth[49] = ""; }
            listeeth();
        }
        private void chk82_CheckedChanged(object sender, EventArgs e)
        {
            if (chk82.Checked) { teeth[48] = "82"; } else { teeth[48] = ""; }
            listeeth();
        }
        private void chk81_CheckedChanged(object sender, EventArgs e)
        {
            if (chk81.Checked) { teeth[47] = "81"; } else { teeth[47] = ""; }
            listeeth();
        }
        private void chk71_CheckedChanged(object sender, EventArgs e)
        {
            if (chk71.Checked) { teeth[46] = "71"; } else { teeth[46] = ""; }
            listeeth();
        }
        private void chk72_CheckedChanged(object sender, EventArgs e)
        {
            if (chk72.Checked) { teeth[45] = "72"; } else { teeth[45] = ""; }
            listeeth();
        }
        private void chk73_CheckedChanged(object sender, EventArgs e)
        {
            if (chk73.Checked) { teeth[44] = "73"; } else { teeth[44] = ""; }
            listeeth();
        }
        private void chk74_CheckedChanged(object sender, EventArgs e)
        {
            if (chk74.Checked) { teeth[43] = "74"; } else { teeth[43] = ""; }
            listeeth();
        }
        private void chk75_CheckedChanged(object sender, EventArgs e)
        {
            if (chk75.Checked) { teeth[42] = "75"; } else { teeth[42] = ""; }
            listeeth();
        }
        public void fill_test_grid(DataTable dt,string q)
        {
            try
            {
                if (dataGridView3.Rows.Count == 0)
                {
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    string norm = "";
                    DataTable dt_test_type = new DataTable();
                        DataTable dt_test = this.ctrlr.dt_test(q);
                        if (dt_test.Rows.Count > 0)
                        {
                            dt_test_type = this.ctrlr.dt_test_type(dt_test.Rows[0]["TestTypeID"].ToString());

                        string Normal = "";
                        string Mnorm = ""; string Fnorm = "";
                        if (dt.Rows[0]["NormalValueM"].ToString() != "")
                        {
                            Mnorm = dt.Rows[0]["NormalValueM"].ToString();
                        }
                        if (dt.Rows[0]["NormalValueF"].ToString() != "")
                        {
                            Fnorm = dt.Rows[0]["NormalValueF"].ToString();
                        }
                        string NormalValue = "";
                        if (Mnorm!="" && Fnorm !="")
                        {
                            NormalValue = Mnorm + "," + Fnorm;
                        }
                        else if(Mnorm != "")
                        {
                            NormalValue = Mnorm;
                        }
                        else if(Fnorm != "")
                        {
                            NormalValue = Fnorm;
                        }
                        else
                        {
                            NormalValue = "";
                        }
                        dataGridView3.Rows.Add("", "", dt_test_type.Rows[0]["Name"].ToString(), dt.Rows[0]["Name"].ToString(), dt.Rows[0]["unit"].ToString(), NormalValue, PappyjoeMVC.Properties.Resources.deleteicon, dt.Rows[0]["id"].ToString(), "", "Lab");

                    }


                    //if (dgvtempitem.Rows[r].Cells[10].Value.ToString() == ",")
                    //{
                    //    norm = "";
                    //}
                    //else
                    //{
                    //    norm = dgvtempitem.Rows[r].Cells[10].Value.ToString();
                    //}



                    //dataGridView3.Rows[i].Cells["temp_id"].Value = "";// q;// dt_maintest.Rows[0]["Main_test"].ToString();
                    //dataGridView3.Rows[i].Cells["maintest"].Value = "";// dt_maintest.Rows[0]["Main_test"].ToString();
                    //dataGridView3.Rows[i].Cells["testtype"].Value = dt_test_type.Rows[0]["Name"].ToString();
                    //dataGridView3.Rows[i].Cells["test"].Value = dt.Rows[0]["Name"].ToString();
                    //dataGridView3.Rows[i].Cells["units"].Value = dt.Rows[]["Units"].ToString();
                    //dataGridView3.Rows[i].Cells["normalvalue"].Value = dt.Rows[0]["NormalValue"].ToString();
                    //dataGridView3.Rows[i].Cells["testid"].Value = dt.Rows[0]["id"].ToString();
                    //dataGridView3.Rows[i].Cells["mainid"].Value = "";// dt_maintest.Rows[0]["id"].ToString();
                    //dataGridView3.Rows[i].Cells["delete"].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                    //}
                }
                else
                {
                    DataTable dt_test_type = new DataTable();
                    DataTable dt_test = this.ctrlr.dt_test(q);
                    if (dt_test.Rows.Count > 0)
                    {
                        dt_test_type = this.ctrlr.dt_test_type(dt_test.Rows[0]["TestTypeID"].ToString());
                    }
                    string Mnorm = ""; string Fnorm = "";
                    if (dt.Rows[0]["NormalValueM"].ToString() != "")
                    {
                        Mnorm = dt.Rows[0]["NormalValueM"].ToString();
                    }
                    if (dt.Rows[0]["NormalValueF"].ToString() != "")
                    {
                        Fnorm = dt.Rows[0]["NormalValueF"].ToString();
                    }
                    string NormalValue = "";
                    if (Mnorm != "" && Fnorm != "")
                    {
                        NormalValue = Mnorm + "," + Fnorm;
                    }
                    else if (Mnorm != "")
                    {
                        NormalValue = Mnorm;
                    }
                    else if (Fnorm != "")
                    {
                        NormalValue = Fnorm;
                    }
                    else
                    {
                        NormalValue = "";
                    }
                    int row = dataGridView3.Rows.Count;
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                        dataGridView3.Rows.Add();
                        dataGridView3.Rows[row].Cells["temp_id"].Value = "";// q;// dt_maintest.Rows[0]["Main_test"].ToString();
                        dataGridView3.Rows[row].Cells["maintest"].Value = "";// dt_maintest.Rows[0]["Main_test"].ToString();
                        dataGridView3.Rows[row].Cells["testtype"].Value = dt_test_type.Rows[0]["Name"].ToString();
                        dataGridView3.Rows[row].Cells["test"].Value = dt.Rows[0]["Name"].ToString();
                        dataGridView3.Rows[row].Cells["units"].Value = dt.Rows[0]["unit"].ToString();
                        dataGridView3.Rows[row].Cells["normalvalue"].Value = NormalValue;// dt.Rows[]["NormalValue"].ToString();
                        dataGridView3.Rows[row].Cells["testid"].Value = dt.Rows[0]["id"].ToString();
                        dataGridView3.Rows[row].Cells["mainid"].Value = "";// dt_maintest.Rows[0]["id"].ToString();
                        dataGridView3.Rows[row].Cells["template_type"].Value = "Lab";
                    dataGridView3.Rows[row].Cells["delete"].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                    //}
                }
            }
            catch(Exception ex)
            {

            }
        }
        public void testrslt(DataTable tbshade,string q)
        {
            try
            {
                if (dataGridView3.Rows.Count == 0)
                {
                    for (int i = 0; i < tbshade.Rows.Count; i++)
                    {
                        DataTable dt_test_type = new DataTable();

                        DataTable dt_test = this.ctrlr.dt_test(tbshade.Rows[i]["TestId"].ToString());
                        if (dt_test.Rows.Count > 0)
                        {
                            dt_test_type = this.ctrlr.dt_test_type(dt_test.Rows[0]["TestTypeID"].ToString());
                        }
                        DataTable dt_maintest = this.ctrlr.dt_maintest(tbshade.Rows[i]["MainTestId"].ToString());

                        labelmaintest.Text = dt_maintest.Rows[0]["Main_test"].ToString();// tbshade.Rows[i]["Test Name"].ToString();
                        labeltesttype.Text = dt_test_type.Rows[0]["Name"].ToString(); //tbshade.Rows[i]["SampleType"].ToString();
                        txttype.Text = dt_test_type.Rows[0]["Name"].ToString();// tbshade.Rows[i]["SampleType"].ToString();

                        dataGridView3.Rows.Add();
                        dataGridView3.Rows[i].Cells["temp_id"].Value = q;// dt_maintest.Rows[0]["Main_test"].ToString();
                        dataGridView3.Rows[i].Cells["maintest"].Value = dt_maintest.Rows[0]["Main_test"].ToString();
                        dataGridView3.Rows[i].Cells["testtype"].Value = dt_test_type.Rows[0]["Name"].ToString();
                        dataGridView3.Rows[i].Cells["test"].Value = dt_test.Rows[0]["Name"].ToString();
                        dataGridView3.Rows[i].Cells["units"].Value = tbshade.Rows[i]["Units"].ToString();
                        dataGridView3.Rows[i].Cells["normalvalue"].Value = tbshade.Rows[i]["NormalValue"].ToString();
                        dataGridView3.Rows[i].Cells["testid"].Value = dt_test.Rows[0]["id"].ToString();
                        dataGridView3.Rows[i].Cells["mainid"].Value = dt_maintest.Rows[0]["id"].ToString();
                        dataGridView3.Rows[i].Cells["template_type"].Value = "Template";
                        dataGridView3.Rows[i].Cells["delete"].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                        checkvalue = "1";
                    }
                }
                else
                {
                    int row = dataGridView3.Rows.Count;
                    for (int i = 0; i < tbshade.Rows.Count; i++)
                    {
                        DataTable dt_test_type = new DataTable();

                        DataTable dt_test = this.ctrlr.dt_test(tbshade.Rows[i]["TestId"].ToString());
                        if (dt_test.Rows.Count > 0)
                        {
                            dt_test_type = this.ctrlr.dt_test_type(dt_test.Rows[0]["TestTypeID"].ToString());
                        }
                        DataTable dt_maintest = this.ctrlr.dt_maintest(tbshade.Rows[i]["MainTestId"].ToString());

                        labelmaintest.Text = dt_maintest.Rows[0]["Main_test"].ToString();// tbshade.Rows[i]["Test Name"].ToString();
                        labeltesttype.Text = dt_test_type.Rows[0]["Name"].ToString(); //tbshade.Rows[i]["SampleType"].ToString();
                        txttype.Text = dt_test_type.Rows[0]["Name"].ToString();// tbshade.Rows[i]["SampleType"].ToString();


                        dataGridView3.Rows.Add(); 
                        dataGridView3.Rows[row].Cells["temp_id"].Value = q;// dt_maintest.Rows[0]["Main_test"].ToString();

                        dataGridView3.Rows[row].Cells["maintest"].Value = dt_maintest.Rows[0]["Main_test"].ToString();
                        dataGridView3.Rows[row].Cells["testtype"].Value = dt_test_type.Rows[0]["Name"].ToString();
                        dataGridView3.Rows[row].Cells["test"].Value = dt_test.Rows[0]["Name"].ToString();
                        dataGridView3.Rows[row].Cells["units"].Value = tbshade.Rows[i]["Units"].ToString();
                        dataGridView3.Rows[row].Cells["normalvalue"].Value = tbshade.Rows[i]["NormalValue"].ToString();
                        dataGridView3.Rows[row].Cells["testid"].Value = dt_test.Rows[0]["id"].ToString();
                        dataGridView3.Rows[row].Cells["mainid"].Value = dt_maintest.Rows[0]["id"].ToString();
                        dataGridView3.Rows[row].Cells["template_type"].Value = "Template";
                        dataGridView3.Rows[row].Cells["delete"].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                        row++;
                        checkvalue = "1";
                    }
                }
                //for (int i = 0; i < tbshade.Rows.Count; i++)
                //{
                //    DataTable dt_test_type = new DataTable();

                //    DataTable dt_test = this.ctrlr.dt_test(tbshade.Rows[i]["TestId"].ToString());
                //    if (dt_test.Rows.Count > 0)
                //    {
                //        dt_test_type = this.ctrlr.dt_test_type(dt_test.Rows[0]["TestTypeID"].ToString());
                //    }
                //    DataTable dt_maintest = this.ctrlr.dt_maintest(tbshade.Rows[i]["MainTestId"].ToString());
                //    labelmaintest.Text = dt_maintest.Rows[0]["Main_test"].ToString();// tbshade.Rows[i]["Test Name"].ToString();
                //    labeltesttype.Text = dt_test_type.Rows[0]["Name"].ToString(); //tbshade.Rows[i]["SampleType"].ToString();
                //    txttype.Text = dt_test_type.Rows[0]["Name"].ToString();// tbshade.Rows[i]["SampleType"].ToString();
                //    //dataGridView3.Rows.Add(tbshade.Rows[i]["Test Name"].ToString(), tbshade.Rows[i]["SampleType"].ToString(), tbshade.Rows[i]["Speciality"].ToString(), tbshade.Rows[i]["Units"].ToString(), tbshade.Rows[i]["Normal Value"].ToString(), PappyjoeMVC.Properties.Resources.deleteicon, tbshade.Rows[i]["Test_id"].ToString(), tbshade.Rows[i]["Main id"].ToString());
                //    //dataGridView3.DataSource = tbshade;
                //    //this.dataGridView3.Columns[6].Visible = false;
                //    //this.dataGridView3.Columns[7].Visible = false;
                //    if(dataGridView3.Rows.Count==0)
                //    {
                //        dataGridView3.Rows.Add();
                //        dataGridView3.Rows[i].Cells["maintest"].Value = dt_maintest.Rows[0]["Main_test"].ToString();
                //        dataGridView3.Rows[i].Cells["testtype"].Value = dt_test_type.Rows[0]["Name"].ToString();
                //        dataGridView3.Rows[i].Cells["test"].Value = dt_test.Rows[0]["Name"].ToString();
                //        dataGridView3.Rows[i].Cells["units"].Value = tbshade.Rows[i]["Units"].ToString();
                //        dataGridView3.Rows[i].Cells["normalvalue"].Value = tbshade.Rows[i]["NormalValue"].ToString();
                //        dataGridView3.Rows[i].Cells["testid"].Value = dt_test.Rows[0]["id"].ToString();
                //        dataGridView3.Rows[i].Cells["mainid"].Value = dt_maintest.Rows[0]["id"].ToString();
                //        dataGridView3.Rows[i].Cells["delete"].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                //    }



                checkvalue = "1";
                //}

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form2 = new PappyjoeMVC.View.Patient_Profile_Details();
            form2.doctor_id = doctor_id;
            form2.patient_id = patient_id;
            openform(form2);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form2 = new PappyjoeMVC.View.Patient_Profile_Details();
            form2.doctor_id = doctor_id;
            form2.patient_id = patient_id;
            openform(form2);
        }

        private void txtSearchMedLab_TextChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedTab.Text=="Templates")
            {
                DataTable tbshade = this.ctrlr.Tempsearch(txtSearchMedLab.Text);
                 if(tbshade.Rows.Count>0)
                {
                    dataGridView2.Rows.Clear();
                    for (int i = 0; i < tbshade.Rows.Count; i++)
                    {
                        dataGridView2.Rows.Add();
                        dataGridView2.Rows[i].Cells[0].Value = tbshade.Rows[i][0].ToString();
                        dataGridView2.Rows[i].Cells[1].Value = tbshade.Rows[i][1].ToString();
                    }
                }

                //dataGridView2.DataSource = tbshade;
            }
            else
            {
                DataTable tbshade = this.ctrlr.lab_Test_search(txtSearchMedLab.Text);
                if(tbshade.Rows.Count>0)
                {
                    dgv_test.Rows.Clear();
                    for (int i = 0; i < tbshade.Rows.Count; i++)
                    {
                        dgv_test.Rows.Add();
                        dgv_test.Rows[i].Cells["test_id"].Value = tbshade.Rows[i][0].ToString();
                        dgv_test.Rows[i].Cells[1].Value = tbshade.Rows[i][1].ToString();
                    }
                }
               //ghgdtteyeeght //dataGridView2.DataSource = tbshade;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedTab.Text== "Templates")
            {
                //dataGridView2.DataSource = null;
                DataTable tbshade = this.ctrlr.Lab_Medi_TemplateMain();
                if (tbshade.Rows.Count > 0)
                {
                    dataGridView2.Rows.Clear();
                    for (int i = 0; i < tbshade.Rows.Count; i++)
                    {
                        dataGridView2.Rows.Add();
                        dataGridView2.Rows[i].Cells[0].Value = tbshade.Rows[i][0].ToString();
                        dataGridView2.Rows[i].Cells[1].Value = tbshade.Rows[i][1].ToString();
                    }
                }
                //if (tbshade.Rows.Count>0)
                //{
                //    //dataGridView2.DataSource = tbshade;
                //}
               
            }
            else
            {
                DataTable tbshade = this.ctrlr.Lab_load_test();
                if(tbshade.Rows.Count>0)
                {
                    dgv_test.Rows.Clear();
                    for(int i=0;i<tbshade.Rows.Count;i++)
                    {
                        dgv_test.Rows.Add();
                        dgv_test.Rows[i].Cells["test_id"].Value= tbshade.Rows[i][0].ToString();
                        dgv_test.Rows[i].Cells[1].Value = tbshade.Rows[i][1].ToString();
                    }
                }
                //dgv_test.DataSource = tbshade;
            }
        }

        private void dgv_test_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //string k = dgv_test.Rows[e.RowIndex].Cells[1].Value.ToString();
            string p = dgv_test.Rows[e.RowIndex].Cells[1].Value.ToString();
            string q = dgv_test.Rows[e.RowIndex].Cells["test_id"].Value.ToString();//0
            txtname.Text = dgv_test.Rows[e.RowIndex].Cells[1].Value.ToString();//1
            DataTable dt = this.ctrlr.test_details(q);
            //fill_test_grid(dt,q);
            if (dup_check(p) == 0)
            {
                fill_test_grid(dt, q);
            }
        }
        
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string k = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            string p = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            string q = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();//0
            txtname.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();//1
            DataTable dt = this.ctrlr.testrslt(q);
            if (dup_check_template(q) == 0)
            {
                testrslt(dt, q);
            }
           
        }
        public int dup_check_template(string p)
        {
            int affected = 0;
            if (dataGridView3.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {

                    if (dataGridView3.Rows[i].Cells["temp_id"].Value.ToString() == p)
                    {
                        // MessageBox.Show("duplication");
                        affected = 1;

                    }
                }

            }
            return affected;
        }
        public int dup_check(string p)
        {
            int affected = 0;
            if (dataGridView3.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {

                    if (dataGridView3.Rows[i].Cells["test"].Value.ToString() == p)
                    {
                        // MessageBox.Show("duplication");
                        affected = 1;

                    }
                }

            }
            return affected;
        }

        private void dgvdentalwork_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtwork_id.Text = dgvdentalwork.Rows[e.RowIndex].Cells["id"].Value.ToString();
            txtWorktype.Text = dgvdentalwork.Rows[e.RowIndex].Cells["WorkType"].Value.ToString();
            txtworkname.Text = dgvdentalwork.Rows[e.RowIndex].Cells["Work"].Value.ToString();
            cmbAlloytype.Text = dgvdentalwork.Rows[e.RowIndex].Cells["aloytype"].Value.ToString();
            cmbShade.Text = dgvdentalwork.Rows[e.RowIndex].Cells["shade"].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = this.ctrlr.grid3data(linkLabel1.Text);
            dataGridView3.DataSource = dt;
        }
      
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked == true)
                {
                    if (dataGridView3.Rows.Count > 0)
                    {
                        ID = this.ctrlr.selectid(labelmaintest.Text);
                        this.ctrlr.inslabmain(patient_id, comboBox6.SelectedValue.ToString(), labelmaintest.Text, ID, Convert.ToDateTime(dateTimePicker1.Text).ToString("yyyy-MM-dd"), dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                        //foreach (DataGridViewRow dr in dataGridView3.Rows)
                        //{
                        //    ID = this.ctrlr.selectid(dr.Cells["maintest"].Value.ToString());
                        //    this.ctrlr.inslabmain(patient_id, comboBox6.SelectedValue.ToString(), dr.Cells["maintest"].Value.ToString(), ID, Convert.ToDateTime(dateTimePicker1.Text).ToString("yyyy-MM-dd"), dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                        //}
                        
                        
                        string id = this.ctrlr.maxid();
                        maxid(id);
                        string dt = DateTime.Now.Date.ToString("yyyy-MM-dd");
                        DateTime Timeonly = DateTime.Now;
                        this.ctrlr.save_log(doctor_id, "Lab Work", " Add Lab Work", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Add", id);
                        var form2 = new PappyjoeMVC.View.LabWorks();
                        form2.doctor_id = doctor_id;
                        form2.patient_id = patient_id;
                        openform(form2);
                    }
                    else
                    {
                        MessageBox.Show(" Please Add Lab Work", "Data Missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (txtworkname.Text.Trim() != "" && txtwork_id.Text.Trim() != "")
                    {
                        this.ctrlr.inslabmain2(patient_id, comboBox6.SelectedValue.ToString(), txtworkname.Text, txtwork_id.Text, dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                        string id = this.ctrlr.maxid();
                        this.ctrlr.insdentlab(id, txtworkname.Text, txtWorktype.Text, cmbAlloytype.Text, cmbShade.Text, patient_id, label15.Text);
                        var form2 = new PappyjoeMVC.View.LabWorks();
                        form2.doctor_id = doctor_id;
                        form2.patient_id = patient_id;
                        openform(form2);
                    }
                    else
                    {
                        MessageBox.Show(" Please Add Lab Work Or select work name in left side..", "Data Missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void Add_Labwork_Load(object sender, EventArgs e)
        {
            try
            {
                cmbShade.Enabled = false;
                radioButton2.Checked = false;
                radioButton1.Checked = true;
                pnlDental.Hide();
                //pnlMedlab.Hide();
                pnladddental.Hide();
                //c.Hide();
                panel13.Visible = true;
                //panel13.Location = new Point(4, 678);
                DataTable rs_patients = this.ctrlr.Get_Patient_Details(patient_id);
                if (rs_patients.Rows[0]["pt_name"].ToString() != "")
                {
                    linkLabel1.Text = rs_patients.Rows[0]["pt_name"].ToString();
                }
                if (rs_patients.Rows[0]["pt_id"].ToString() != "")
                {
                    linkLabel2.Text = rs_patients.Rows[0]["pt_id"].ToString();
                }
                DataTable doctorcombo = this.ctrlr.getdoctrdetails();
                comboBox6.DisplayMember = "doctor_name";
                comboBox6.ValueMember = "id";
                comboBox6.DataSource = doctorcombo;
                label15.Text = "";
                DataTable tbshade = this.ctrlr.Lab_Medi_TemplateMain();
                if (tbshade.Rows.Count > 0)
                {
                    dataGridView2.Rows.Clear();
                    for (int i = 0; i < tbshade.Rows.Count; i++)
                    {
                        dataGridView2.Rows.Add();
                        dataGridView2.Rows[i].Cells[0].Value = tbshade.Rows[i][0].ToString();
                        dataGridView2.Rows[i].Cells[1].Value = tbshade.Rows[i][1].ToString();
                    }
                }
                //dataGridView2.DataSource = tbshade;
                checkvalue = "1";
                DataTable tblab = this.ctrlr.getLabdata();
                combolab.DisplayMember = "labname";
                combolab.ValueMember = "id";
                combolab.DataSource = tblab;
                //if(PappyjoeMVC.Model.Connection.MyGlobals.project_type=="Dental")
                //{
                //    //radioButton2.Visible = true; radioButton1.Visible = true;
                //}
                //else
                //{
                //    radioButton2.Visible = false; radioButton1.Visible = false;
                //}
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
