using PappyjoeMVC.Controller;
using PappyjoeMVC.Model;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace PappyjoeMVC.View
{
    public partial class Add__invoice : Form
    {
        public string doctor_id = "0"; public string staff_id = "";
        public string patient_id = "";
        string admin_id = "0";
        public string complete_proce_id = "";
        string id;
        string P_Plan_id = "0";
        string P_Completed_id = "0";
        Decimal discounttotal = 0;
        Decimal taxrstotal = 0;
        int status = 0;
        string invoicetype = "service";
        Decimal P_tax = 0;
        string stock_id = "0";
        public string[] teeth = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        Add_Invoice_controller cntrl = new Add_Invoice_controller();
        Common_model cmodel = new Common_model();
        public string invoiceid = "";
        public string jhjj;
        public bool editflag = false;
        public static string invoice_plan_id_;

        public Add__invoice()
        {
            InitializeComponent();
        }

        public Add__invoice(string invoice_plan_id)
        {
            InitializeComponent();
            invoice_plan_id_ = invoice_plan_id;
            editflag = true;
        }

        private void Add__invoice_Load(object sender, EventArgs e)
        {
            string admin = this.cntrl.get_adminid();
            if (admin != "")
            {
                if (admin != doctor_id)
                {
                    admin_id = admin;
                }
            }
            string docnam = cmodel.Get_DoctorName(doctor_id);
            panel3.Hide();
            panel6.Hide();
            DGV_Invoice.Hide();
            label14.Hide();
            Cmb_batch.Hide();
            foreach (DataGridViewColumn column in DGV_Invoice.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in DGV_Procedure.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            DataTable dt = cmodel.get_all_doctorname();
            if (dt.Rows.Count > 0)
            {
                Cmb_Doctor.DisplayMember = "doctor_name";
                Cmb_Doctor.ValueMember = "id";
                Cmb_Doctor.DataSource = dt;
                Cmb_Doctor.SelectedIndex = 0;
            }
            if (doctor_id != "0")
            {
                int dr_index = 0;
                if (dt.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j]["id"].ToString() == doctor_id)
                        {
                            dr_index = j;
                        }
                    }
                    Cmb_Doctor.SelectedIndex = dr_index;
                }
            }
            DataTable patient = cmodel.Get_Patient_id_NAme(patient_id);
            if (patient.Rows.Count > 0)
            {
                linkL_Name.Text = patient.Rows[0]["pt_name"].ToString();
                linkLabel_id.Text = patient.Rows[0]["pt_id"].ToString();
            }
            System.Data.DataTable pay = cmodel.get_total_payment(patient_id);
            if (pay.Rows.Count > 0)
            {
                lab_due.Text =Convert.ToDecimal( pay.Rows[0]["total_payment"].ToString()).ToString("0.00") + "due";
            }
            DataTable invno = null;
            invno = this.cntrl.Get_invoice_prefix();
            if (invno.Rows.Count == 0)
            {
                text_billno.ReadOnly = false;
            }
            else
            {
                text_billno.Text = invno.Rows[0]["invoice_prefix"].ToString() + invno.Rows[0]["invoice_number"].ToString();
            }
            Cmb_tax.Hide();
            txt_Discount.Hide();
            CMB_Discount.Hide();
            txt_SearchProcedure.Visible = true;
            DataTable dt5 = this.cntrl.load_tax();
            if (dt5.Rows.Count > 0)
            {
                Cmb_tax.DisplayMember = "tax_name";
                Cmb_tax.ValueMember = "id";
                Cmb_tax.DataSource = dt5;
                Cmb_tax.Text = "";
            }
            DataTable dtb = this.cntrl.get_completed_procedure_details(patient_id);
            Load_procedureGrid(dtb);
            DataTable dtb1 = this.cntrl.get_planed_procedure(patient_id);
            Load_planed_procedure(dtb1);
           DataTable dt_load= this.cntrl.load_AllProcedures();
            Load_searchProcedures(dt_load);
            //if (invoiceid != "")
            //{
            //    btn_SaveInvoice.Text = "Update";
            //    text_billno.Text = invoiceid;
            //    //DataTable dtb_1 = this.cntrl.Get_invoice_deatils(patient_id, invoiceid);
            //    //load_invoice_details(dtb);
            //}
            if(editflag==true)
            {
                btn_SaveInvoice.Text = "Update";
                //text_billno.Text = invoice_plan_id_;
                DGV_Invoice.Show(); panel3.Show(); panel6.Show();
                  DataTable dtb_1 = this.cntrl.Get_invoice_deatils(invoice_plan_id_);
                load_invoice_details(dtb_1);
                editflag = false;
            }
            if (complete_proce_id != "")
            {
                Decimal totalcost = 0;
                Decimal discounttotal = 0;
                Decimal taxtotalamount = 0;
                Decimal totalgrant = 0;
                DGV_Invoice.Show();
                string value = complete_proce_id;
                string[] parts = value.Split(new string[] { "," }, StringSplitOptions.None);
                for (int i = 0; i < parts.Length; i++)
                {
                    System.Data.DataTable rs_plan = this.cntrl.load_completed_procedure(parts[i]);
                    if (rs_plan.Rows.Count > 0)
                    {
                        // DGV_Invoice.Rows.Add(id, procedure_item.Text, txtCost.Text, NMUP_Unit.Text, txt_Discount.Text, CMB_Discount.Text, Cmb_tax.Text, lab_Total.Text, discounttotal, taxrstotal, Cmb_Doctor.SelectedValue.ToString(), Cmb_Doctor.Text, RTB_Notes.Text.Replace("'", " "), label33.Text, id, stock_id, DTP_Date.Value.ToString("MM/dd/yyyy"),lab_tonurse.Text,"",lab_service);
                        DGV_Invoice.Rows.Add(rs_plan.Rows[0]["procedure_id"].ToString(), rs_plan.Rows[0]["procedure_name"].ToString(), rs_plan.Rows[0]["cost"].ToString(), rs_plan.Rows[0]["quantity"].ToString(), rs_plan.Rows[0]["discount"].ToString(), rs_plan.Rows[0]["discount_type"].ToString(), "NA", rs_plan.Rows[0]["total"].ToString(), rs_plan.Rows[0]["discount_inrs"].ToString(), "0", rs_plan.Rows[0]["dr_id"].ToString(), rs_plan.Rows[0]["doctor_name"].ToString(), rs_plan.Rows[0]["note"].ToString(), rs_plan.Rows[0]["tooth"].ToString(), "0", parts[i], rs_plan.Rows[0]["date"].ToString(), lab_tonurse.Text, "", lab_service);

                        //DGV_Invoice.Rows.Add(rs_plan.Rows[0]["procedure_id"].ToString(), rs_plan.Rows[0]["procedure_name"].ToString(), rs_plan.Rows[0]["cost"].ToString(), rs_plan.Rows[0]["quantity"].ToString(), rs_plan.Rows[0]["discount"].ToString(), rs_plan.Rows[0]["discount_type"].ToString(), "NA", rs_plan.Rows[0]["total"].ToString(), rs_plan.Rows[0]["discount_inrs"].ToString(), "0", rs_plan.Rows[0]["dr_id"].ToString(), rs_plan.Rows[0]["doctor_name"].ToString(), rs_plan.Rows[0]["note"].ToString(), rs_plan.Rows[0]["tooth"].ToString(), "0", parts[i], rs_plan.Rows[0]["date"].ToString(), lab_tonurse.Text, "", lab_service);
                        DGV_Invoice.Rows[DGV_Invoice.Rows.Count - 1].Cells[18].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                        img.ImageLayout = DataGridViewImageCellLayout.Normal;
                        totalcost = totalcost + (decimal.Parse(rs_plan.Rows[0]["cost"].ToString()) * decimal.Parse(rs_plan.Rows[0]["quantity"].ToString()));
                        discounttotal = discounttotal + decimal.Parse(rs_plan.Rows[0]["discount_inrs"].ToString());
                        totalgrant = totalgrant + decimal.Parse(rs_plan.Rows[0]["total"].ToString());
                    }
                }
                taxtotalamount = 0;
                txt_TotalCost.Text = totalcost.ToString("0.00");
                txt_TotalDiscount.Text = discounttotal.ToString("0.00");
                txt_TotalTax.Text = taxtotalamount.ToString("0.00");
                txt_GrantTotal.Text = totalgrant.ToString("0.00");
                panel2.Show();
                panel3.Show();
                panel6.Show();
            }
            DGV_Invoice.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGV_Invoice.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGV_Invoice.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGV_Invoice.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGV_Invoice.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            //updation 15/12/2020
            //DataTable dt_enablecost = this.cntrl.get_enablecost();
            //if (dt_enablecost.Rows.Count > 0)
            //{
            //    if (dt_enablecost.Rows[0][0].ToString() == "Yes")
            //    {
            //        costtextbox.Visible = true;
            //        label4.Visible = true;
            //        label3.Visible = true;
            //        label7.Visible = true;
            //    }
            //    else
            //    {
            //        costtextbox.Visible = false;
            //        label4.Visible = false;
            //        label3.Visible = false;
            //        label7.Visible = false;
            //    }
            //}
        }
        public void Load_procedureGrid(DataTable dt_tp1)
        {
            if (dt_tp1.Rows.Count > 0)
            {
                for (int j = 0; j < dt_tp1.Rows.Count; j++)
                {
                    Double gtotal = 0;
                    gtotal = Convert.ToDouble(dt_tp1.Rows[j]["cost"].ToString());
                    if(dt_tp1.Rows[j]["ToNurse"].ToString()=="Yes")
                    {
                        DGV_Procedure.Rows.Add(dt_tp1.Rows[j]["procedure_id"].ToString(), dt_tp1.Rows[j]["procedure_name"].ToString(), "(Completed), To Nurse", gtotal.ToString("0.00"), "0", dt_tp1.Rows[j]["id"].ToString());

                    }
                    else
                    {
                        DGV_Procedure.Rows.Add(dt_tp1.Rows[j]["procedure_id"].ToString(), dt_tp1.Rows[j]["procedure_name"].ToString(), "(Completed)", gtotal.ToString("0.00"), "0", dt_tp1.Rows[j]["id"].ToString());

                    }
                    DGV_Procedure.Rows[j].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Bold);
                    DGV_Procedure.Rows[j].Cells[2].Style.ForeColor = Color.DarkGreen;
                }
            }
        }
        public void Load_planed_procedure(DataTable dt_tp)
        {
            if (dt_tp.Rows.Count > 0)
            {
                for (int j = 0; j < dt_tp.Rows.Count; j++)
                {
                    Double gtotal1 = 0;
                    gtotal1 = Convert.ToDouble(dt_tp.Rows[j]["cost"].ToString());
                    //if (dt_tp.Rows[j]["ToNurse"].ToString() == "Yes")
                    //{
                    //    DGV_Procedure.Rows.Add(dt_tp.Rows[j]["procedure_id"].ToString(), dt_tp.Rows[j]["procedure_name"].ToString(), "(Planned), (To Nurse)", gtotal1.ToString("0.00"), dt_tp.Rows[j]["id"].ToString(), "0");
                    //}
                    //else
                    //{
                        DGV_Procedure.Rows.Add(dt_tp.Rows[j]["procedure_id"].ToString(), dt_tp.Rows[j]["procedure_name"].ToString(), "(Planned)", gtotal1.ToString("0.00"), dt_tp.Rows[j]["id"].ToString(), "0");
                    //}
                        //DGV_Procedure.Rows.Add(dt_tp.Rows[j]["procedure_id"].ToString(), dt_tp.Rows[j]["procedure_name"].ToString(), "(Planned)", gtotal1.ToString("0.00"), dt_tp.Rows[j]["id"].ToString(), "0");
                    DGV_Procedure.Rows[DGV_Procedure.Rows.Count - 1].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Bold);
                    DGV_Procedure.Rows[DGV_Procedure.Rows.Count - 1].Cells[2].Style.ForeColor = Color.Blue;

                }
            }
        }
        public void Load_searchProcedures(DataTable dt_pt)
        {
            if (dt_pt.Rows.Count > 0)
            {
                for (int j = 0; j < dt_pt.Rows.Count; j++)
                {
                    Double gtotal2 = 0;
                    gtotal2 = Convert.ToDouble(dt_pt.Rows[j]["cost"].ToString());
                    DGV_Procedure.Rows.Add(dt_pt.Rows[j]["id"].ToString(), dt_pt.Rows[j]["name"].ToString(), "", gtotal2.ToString("0.00"), "0", "0");
                }
            }
        }
        public void load_invoice_details(DataTable dt2)
        {
            int i = 0;
            if (dt2.Rows.Count > 0)
            {
                decimal cost = 0, discound = 0, tax = 0, grand = 0;
                while (i < dt2.Rows.Count)
                {
                    string drid = dt2.Rows[i]["dr_id"].ToString();
                    try
                    {
                        string dr_Name = cmodel.Get_DoctorName(drid);
                        decimal total = 0, amount = 0,service_total=0;
                       
                        amount = (Convert.ToDecimal(dt2.Rows[i]["cost"].ToString()) * Convert.ToDecimal(dt2.Rows[i]["unit"].ToString()));
                        DGV_Invoice.Rows.Add(dt2.Rows[i]["service_id"].ToString(), dt2.Rows[i]["services"].ToString(), Convert.ToDecimal(dt2.Rows[i]["cost"].ToString()).ToString("F"), dt2.Rows[i]["unit"].ToString(), dt2.Rows[i]["discount"].ToString(), dt2.Rows[i]["discount_type"].ToString(), dt2.Rows[i]["tax"].ToString(), amount.ToString("F"), dt2.Rows[i]["discountin_rs"].ToString(), dt2.Rows[i]["tax_inrs"].ToString(), drid, dr_Name, dt2.Rows[i]["notes"].ToString(), "0", dt2.Rows[i]["plan_id"].ToString(), dt2.Rows[i]["completed_id"].ToString(), dt2.Rows[i]["date"].ToString(), lab_tonurse.Text, "", lab_service);
                        //DGV_Invoice.Rows.Add(dt2.Rows[i]["service_id"].ToString(), dt2.Rows[i]["services"].ToString(), Convert.ToDecimal( dt2.Rows[i]["cost"].ToString()).ToString("F"), dt2.Rows[i]["unit"].ToString(), dt2.Rows[i]["discount"].ToString(), dt2.Rows[i]["discount_type"].ToString(), dt2.Rows[i]["tax"].ToString(), amount.ToString("F"), dt2.Rows[i]["discountin_rs"].ToString(), dt2.Rows[i]["tax_inrs"].ToString(), drid, dr_Name, dt2.Rows[i]["notes"].ToString(),"0", dt2.Rows[i]["plan_id"].ToString(), dt2.Rows[i]["completed_id"].ToString(), dt2.Rows[i]["date"].ToString(), lab_tonurse.Text, "", lab_service);
                        //,lab_tonurse.Text,"",lab_service
                        cost = cost + amount;
                        discound = discound + Convert.ToDecimal(dt2.Rows[i]["discountin_rs"].ToString());
                        tax = tax + Convert.ToDecimal(dt2.Rows[i]["tax_inrs"].ToString());
                        total = (amount + Convert.ToDecimal(dt2.Rows[i]["tax_inrs"].ToString())) - Convert.ToDecimal(dt2.Rows[i]["discountin_rs"].ToString());

                        //txt_GrantTotal.Text = Convert.ToString((totcost + tax1) - discount1);
                        grand = grand + total;
                        DGV_Invoice.Rows[i].Cells[18].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                        img.ImageLayout = DataGridViewImageCellLayout.Normal;
                        Cmb_Doctor.Text = dr_Name;
                        DTP_Date.Value = Convert.ToDateTime(dt2.Rows[0]["date"].ToString());
                        RTB_Notes.Text = dt2.Rows[0][12].ToString();
                        text_billno.Text = dt2.Rows[0]["invoice_no"].ToString();
                        i++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                txt_TotalCost.Text = cost.ToString("0.00"); ;// dt2.Rows[0][""].ToString();
                txt_TotalDiscount.Text = discound.ToString("0.00"); ;
                txt_TotalTax.Text = tax.ToString("0.00"); ;
                txt_GrantTotal.Text = grand.ToString("0.00"); 

            }
        }

        private void txt_SearchProcedure_KeyUp(object sender, KeyEventArgs e)
        {
            if (tabControlprocedure.SelectedTab == tab_procedure)
            {
                DGV_Procedure.RowCount = 0;
                DataTable dt_tp1 = this.cntrl.search_procedure_completed(patient_id, txt_SearchProcedure.Text);
                Load_procedureGrid(dt_tp1);
                DataTable dt_planed = this.cntrl.Search_procedure_planed(patient_id, txt_SearchProcedure.Text);
                Load_planed_procedure(dt_planed);
                DataTable dtb_procedure = this.cntrl.search_procedures(txt_SearchProcedure.Text);
                Load_searchProcedures(dtb_procedure);
            }
            else if (tabControlprocedure.SelectedTab == tab_labprocedure)
            {
                //string qry = "select * from tbl_labprocedure where name like '" + txt_SearchProcedure.Text + "%'  ORDER BY id Desc limit 250";
                //DataTable dt = cntrl.searchlabprocedure(qry);
                //dgv_labprocedure.DataSource = dt;
                string qry = "select id,name,cost from tbl_labprocedure where name like '" + txt_SearchProcedure.Text + "%' ORDER BY id Desc limit 250";
                DataTable dt_testname = cntrl.searchlabprocedure(qry);
                dgv_labprocedure.Rows.Clear();// = null;
                if (dt_testname.Rows.Count > 0)
                {
                    for (int j = 0; j < dt_testname.Rows.Count; j++)
                    {
                        //DataTable dt_testname = this.cntrl.dt_testname(dt_test.Rows[j]["test_id"].ToString());
                        //if (dt_testname.Rows.Count > 0)
                        //{
                        Double gtotal = 0;
                        //gtotal = Convert.ToDouble(dt_testname.Rows[0]["cost"].ToString());
                        dgv_labprocedure.Rows.Add(dt_testname.Rows[j]["id"].ToString(), dt_testname.Rows[j]["name"].ToString(), "", Convert.ToDecimal(dt_testname.Rows[j]["cost"].ToString()).ToString("0.00"), "0", "");
                    }
                }
                // dgv_labprocedure.DataSource = dt;
            }
        }
      
        

        private void txt_SearchProcedure_Click(object sender, EventArgs e)
        {
            txt_SearchProcedure.Text = "";
        }

        private void DGV_Procedure_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panel3.Show();
            panel6.Show();
            DGV_Invoice.Show();
            panel1.Hide();
            txt_Discount.Hide();
            CMB_Discount.Hide();
            Cmb_tax.Hide();
            lab_AddDiscount.Show();
            lab_AddTax.Show();
            if (e.RowIndex >= 0)
            {
                int r = e.RowIndex; lab_service = "No";
                 id = DGV_Procedure.Rows[r].Cells[0].Value.ToString();
                P_Plan_id = DGV_Procedure.Rows[r].Cells[4].Value.ToString();
                P_Completed_id = DGV_Procedure.Rows[r].Cells[5].Value.ToString();
                string idd = id;
                panel2.Show();
                DataTable dt = this.cntrl.get_procedure_Name(id);
                if (dt.Rows.Count > 0)
                {
                    procedure_item.Text = dt.Rows[0]["name"].ToString();
                    NMUP_Unit.Text = "1";
                    txt_Discount.Text = "0";
                    CMB_Discount.Text = "INR";
                    Cmb_tax.Text = "NA";
                    RTB_Notes.Text = "";
                    RTB_Notes.Width = 500;
                    txtCost.Text =Convert.ToDecimal( dt.Rows[0]["cost"].ToString()).ToString("00.00");
                    //lab_Total.Text = Convert.ToString(decimal.Round(Convert.ToDecimal(dt.Rows[0]["cost"].ToString()), 2, MidpointRounding.AwayFromZero));
                    lab_Total.Text = Convert.ToDecimal(dt.Rows[0]["cost"].ToString()).ToString("00.00");

                }
                label33.Text = "";
                Lab_teeth.Text = "Teeth";
                if (P_Plan_id != "0")
                {
                    DataTable dt_tratmnt = this.cntrl.Get_treatment_details(P_Plan_id);
                    Fill_Alltreatment_deatils(dt_tratmnt);
                }
                if (P_Completed_id != "0")
                {
                    DataTable dtb = this.cntrl.Get_completed_table_details(P_Completed_id);
                    Fill_Alltreatment_deatils(dtb);
                }
                Chk_MultiplyCost.Checked = false;
                CHk_FullMouth.Checked = false;
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
                decimal qty = 0;
                decimal cost = 0;
                decimal discount = 0;
                if (NMUP_Unit.Text != "")
                {
                    qty = Convert.ToDecimal(NMUP_Unit.Text);
                }
                if (txtCost.Text != "")
                {
                    cost = Convert.ToDecimal(txtCost.Text);
                }
                if (txt_Discount.Text != "")
                {
                    if (CMB_Discount.Text == "INR")
                    {
                        discount = Convert.ToDecimal(txt_Discount.Text);
                    }
                    else
                    {
                        discount = ((qty * cost) * Convert.ToDecimal(txt_Discount.Text)) / 100;
                    }
                }
                lab_Total.Text = Convert.ToString((qty * cost) - discount);
                discounttotal = discount;
                taxrstotal = 0;
                if (P_tax > 0)
                {
                    lab_Total.Text = Convert.ToString(Convert.ToDecimal(lab_Total.Text) - ((qty * cost) * P_tax / 100));
                    taxrstotal = (qty * cost) * P_tax / 100;
                }
            }
        }

        public void Fill_Alltreatment_deatils(DataTable dt_treatment)
        {
            if (dt_treatment.Rows.Count > 0)
            {
                procedure_item.Text = dt_treatment.Rows[0]["procedure_name"].ToString();
                NMUP_Unit.Text = dt_treatment.Rows[0]["quantity"].ToString();
                txt_Discount.Text = dt_treatment.Rows[0]["discount"].ToString();
                CMB_Discount.Text = dt_treatment.Rows[0]["discount_type"].ToString();
                RTB_Notes.Text = dt_treatment.Rows[0]["note"].ToString();
                RTB_Notes.Width = 500;
                txtCost.Text = dt_treatment.Rows[0]["cost"].ToString();
                //lab_Total.Text = Convert.ToString(decimal.Round(Convert.ToDecimal(dt_treatment.Rows[0]["total"].ToString()), 2, MidpointRounding.AwayFromZero));
                lab_Total.Text = Convert.ToString(Convert.ToDecimal(dt_treatment.Rows[0]["total"].ToString()));
                if(dt_treatment.Rows[0]["ToNurse"].ToString()=="Yes")
                {
                    lab_tonurse.Text = "Yes";
                }
                else
                {
                    lab_tonurse.Text = "No";
                }
                label33.Text = dt_treatment.Rows[0]["tooth"].ToString();
                if (txt_Discount.Text != "" && txt_Discount.Text != "0")
                {
                    lab_AddDiscount.Hide();
                    txt_Discount.Show();
                    CMB_Discount.Show();
                }
            }
        }

        private void NMUP_Unit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void NMUP_Unit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (invoicetype == "drug")
                {
                    if (status == 1)
                    {
                        if (!String.IsNullOrWhiteSpace(NMUP_Unit.Text))
                        {
                            int qty = 0;
                            DataTable testQty = this.cntrl.Get_quantiry_fromStock(stock_id);
                            if (testQty.Rows.Count > 0)
                            {
                                qty = int.Parse(testQty.Rows[0][0].ToString());
                            }
                            if (int.Parse(NMUP_Unit.Text) > qty || int.Parse(NMUP_Unit.Text) == 0)
                            {
                                lab_Msg.Show();
                            }
                            else
                            {
                                lab_Msg.Hide();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Choose an Item From Left By Just clicking on an Item..", "No Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                calculations();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      
        public void calculations()
        {
            decimal qty1 = 0;
            decimal cost = 0;
            decimal discount = 0;
            if (!String.IsNullOrWhiteSpace(NMUP_Unit.Text))
            {
                qty1 = Convert.ToDecimal(NMUP_Unit.Text);
            }
            if (!String.IsNullOrWhiteSpace(txtCost.Text))
            {
                cost = Convert.ToDecimal(txtCost.Text);
            }
            if (!String.IsNullOrWhiteSpace(txt_Discount.Text))
            {
                if (CMB_Discount.Text == "INR")
                {
                    discount = Convert.ToDecimal(txt_Discount.Text);
                }
                else
                {
                    discount = ((qty1 * cost) * Convert.ToDecimal(txt_Discount.Text)) / 100;
                }
            }
            lab_Total.Text = Convert.ToString((qty1 * cost) - discount);
            discounttotal = discount;
            taxrstotal = 0;
            if (P_tax > 0)
            {
                lab_Total.Text = Convert.ToString(Convert.ToDecimal(lab_Total.Text) + ((qty1 * cost) * P_tax / 100));
                taxrstotal = (qty1 * cost) * P_tax / 100;
            }
        }

        private void lab_AddDiscount_Click(object sender, EventArgs e)
        {
            txt_Discount.Visible = true;
            CMB_Discount.Visible = true;
            lab_AddDiscount.Hide();
        }

        private void txt_Discount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.'))
            { e.Handled = true; }
            TextBox txtDecimal = sender as TextBox;
            if (e.KeyChar == '.' && txtDecimal.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void txt_Discount_TextChanged(object sender, EventArgs e)
        {
            calculations();
        }

        private void CMB_Discount_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal qty = 0;
            decimal cost = 0;
            decimal discount = 0;
            if (NMUP_Unit.Text != "")
            {
                qty = Convert.ToDecimal(NMUP_Unit.Text);
            }
            if (txtCost.Text != "")
            {
                cost = Convert.ToDecimal(txtCost.Text);
            }
            if (txt_Discount.Text != "")
            {
                if (CMB_Discount.Text == "INR")
                {
                    discount = Convert.ToDecimal(txt_Discount.Text);
                }
                else
                {
                    discount = ((qty * cost) * Convert.ToDecimal(txt_Discount.Text)) / 100;
                }
            }
            lab_Total.Text = Convert.ToString((qty * cost) - discount);
            discounttotal = discount;
            taxrstotal = 0;
            if (P_tax > 0)
            {
                lab_Total.Text = Convert.ToString(Convert.ToDecimal(lab_Total.Text) - ((qty * cost) * P_tax / 100));
                taxrstotal = (qty * cost) * P_tax / 100;
            }
        }

        private void lab_AddTax_Click(object sender, EventArgs e)
        {
            Cmb_tax.Show();
            lab_AddTax.Hide();
        }

        private void Cmb_tax_KeyPress(object sender, KeyPressEventArgs e)
        {
            Cmb_tax.SelectionStart = 0;
            Cmb_tax.SelectionLength = Cmb_tax.Text.Length;
            if (e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void Cmb_tax_SelectedIndexChanged(object sender, EventArgs e)
        {
            calculations();
        }

        private void Lab_teeth_Click(object sender, EventArgs e)
        {
            if (procedure_item.Text != "PRODUCTS AND SERVICES")
            {
                panel1.Visible = true;
                panel4.Visible = false;
            }
            else
            {
                MessageBox.Show("Services not found..!!");
            }
        }

        private void Chk_MultiplyCost_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_MultiplyCost.Checked == true)
            {
                decimal q = Convert.ToDecimal(NMUP_Unit.Text.ToString());
                NMUP_Unit.Text = Convert.ToString(q);
            }
        }

        private void CHk_FullMouth_CheckedChanged(object sender, EventArgs e)
        {
            if (CHk_FullMouth.Checked)
            {
                chk11.Checked = true; chk12.Checked = true;
                chk13.Checked = true; chk14.Checked = true;
                chk15.Checked = true; chk16.Checked = true;
                chk17.Checked = true; chk18.Checked = true;

                chk21.Checked = true; chk22.Checked = true;
                chk23.Checked = true; chk24.Checked = true;
                chk25.Checked = true; chk26.Checked = true;
                chk27.Checked = true; chk28.Checked = true;

                chk31.Checked = true; chk32.Checked = true;
                chk33.Checked = true; chk34.Checked = true;
                chk35.Checked = true; chk36.Checked = true;
                chk37.Checked = true; chk38.Checked = true;

                chk41.Checked = true; chk42.Checked = true;
                chk43.Checked = true; chk44.Checked = true;
                chk45.Checked = true; chk46.Checked = true;
                chk47.Checked = true; chk48.Checked = true;
                label33.Text = "Full Mouth";
            }
            else
            {

                label33.Text = "";
                checkBox_control();
            }
        }

        public void checkBox_control()
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
        private void chk18_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk18.Checked) { teeth[0] = "18"; } else { teeth[0] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk17_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk17.Checked) { teeth[1] = "17"; } else { teeth[1] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk16_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk16.Checked) { teeth[2] = "16"; } else { teeth[2] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk15_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk15.Checked) { teeth[3] = "15"; } else { teeth[3] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk14_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk14.Checked) { teeth[4] = "14"; } else { teeth[4] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk13_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk13.Checked) { teeth[5] = "13"; } else { teeth[5] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk12_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk12.Checked) { teeth[6] = "12"; } else { teeth[6] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk11_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk11.Checked) { teeth[7] = "11"; } else { teeth[7] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk21_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk21.Checked) { teeth[8] = "21"; } else { teeth[8] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk22_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk22.Checked) { teeth[9] = "22"; } else { teeth[9] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk23_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk23.Checked) { teeth[10] = "23"; } else { teeth[10] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk24_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk24.Checked) { teeth[11] = "24"; } else { teeth[11] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk25_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk25.Checked) { teeth[12] = "25"; } else { teeth[12] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk26_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk26.Checked) { teeth[13] = "26"; } else { teeth[13] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk27_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk27.Checked) { teeth[14] = "27"; } else { teeth[14] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk28_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk28.Checked) { teeth[15] = "28"; } else { teeth[15] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk48_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk48.Checked) { teeth[31] = "48"; } else { teeth[31] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk47_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk47.Checked) { teeth[30] = "47"; } else { teeth[30] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk46_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk46.Checked) { teeth[29] = "46"; } else { teeth[29] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk45_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk45.Checked) { teeth[28] = "45"; } else { teeth[28] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk44_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk44.Checked) { teeth[27] = "44"; } else { teeth[27] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk43_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk43.Checked) { teeth[26] = "43"; } else { teeth[26] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk42_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk42.Checked) { teeth[25] = "42"; } else { teeth[25] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk41_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk41.Checked) { teeth[24] = "41"; } else { teeth[24] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk31_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk31.Checked) { teeth[23] = "31"; } else { teeth[23] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk32_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk32.Checked) { teeth[22] = "32"; } else { teeth[22] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk33_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk33.Checked) { teeth[21] = "33"; } else { teeth[21] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk34_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk34.Checked) { teeth[20] = "34"; } else { teeth[20] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk35_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk35.Checked) { teeth[19] = "35"; } else { teeth[19] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk36_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk36.Checked) { teeth[18] = "36"; } else { teeth[18] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk37_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk37.Checked) { teeth[17] = "37"; } else { teeth[17] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk38_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk38.Checked) { teeth[16] = "38"; } else { teeth[16] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk55_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk55.Checked) { teeth[32] = "55"; } else { teeth[32] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk54_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk54.Checked) { teeth[33] = "54"; } else { teeth[33] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk53_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk53.Checked) { teeth[34] = "53"; } else { teeth[34] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk52_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk52.Checked) { teeth[35] = "52"; } else { teeth[35] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk51_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk51.Checked) { teeth[36] = "51"; } else { teeth[36] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk61_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk61.Checked) { teeth[37] = "61"; } else { teeth[37] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk62_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk62.Checked) { teeth[38] = "62"; } else { teeth[38] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk63_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk63.Checked) { teeth[39] = "63"; } else { teeth[39] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk64_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk64.Checked) { teeth[40] = "64"; } else { teeth[40] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk65_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk65.Checked) { teeth[41] = "65"; } else { teeth[41] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk85_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk85.Checked) { teeth[51] = "85"; } else { teeth[51] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk84_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk84.Checked) { teeth[50] = "84"; } else { teeth[50] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk83_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk83.Checked) { teeth[49] = "83"; } else { teeth[49] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk82_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk82.Checked) { teeth[48] = "82"; } else { teeth[48] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk81_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk81.Checked) { teeth[47] = "81"; } else { teeth[47] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk71_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk71.Checked) { teeth[46] = "71"; } else { teeth[46] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk72_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk72.Checked) { teeth[45] = "72"; } else { teeth[45] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk73_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk73.Checked) { teeth[44] = "73"; } else { teeth[44] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk74_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk74.Checked) { teeth[43] = "74"; } else { teeth[43] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void chk75_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk75.Checked) { teeth[42] = "75"; } else { teeth[42] = ""; }
            string toothno = "";
            int qty = 0;
            for (int i = 0; i < 51; i++)
            { if (teeth[i] != "") { toothno = toothno + '|' + teeth[i]; qty++; } }
            if (Chk_MultiplyCost.Checked)
            {
                if (qty.ToString() == "0")
                {
                    NMUP_Unit.Text = "1";
                }
                else
                {
                    NMUP_Unit.Text = qty.ToString();
                }
            }
            if (toothno.Length > 2) { label33.Text = toothno.Substring(1, (toothno.Length) - 1); }
            else { label33.Text = ""; }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            label33.Text = "";
            checkBox_control();
            panel1.Hide();
        }

        private void btn_Done_Click(object sender, EventArgs e)
        {
            panel1.Hide();
        }

        private void chk_fulmac_CheckedChanged(object sender, EventArgs e)
        {

            if (chk_fulmac.Checked)
            {
                chk51.Checked = true; chk52.Checked = true;
                chk53.Checked = true; chk54.Checked = true;
                chk55.Checked = true; chk61.Checked = true;
                chk62.Checked = true; chk63.Checked = true;
                chk64.Checked = true; chk65.Checked = true;

                chk71.Checked = true; chk72.Checked = true;
                chk73.Checked = true; chk74.Checked = true;
                chk75.Checked = true; chk81.Checked = true;
                chk82.Checked = true; chk83.Checked = true;
                chk84.Checked = true; chk85.Checked = true;
            }
            else
            {
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
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {

                if (String.IsNullOrWhiteSpace(NMUP_Unit.Text) || String.IsNullOrWhiteSpace(procedure_item.Text) || procedure_item.Text == "PRODUCTS AND SERVICES")
                {
                    MessageBox.Show("Please enter the Quantity and Cost...", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if(btn_SaveInvoice.Text!="Update")
                    {
                        if (invoicetype == "drug")
                        {
                            if (lab_Msg.Visible == false)
                            {
                                DGV_Invoice.Rows.Add(id, procedure_item.Text, txtCost.Text, NMUP_Unit.Text, txt_Discount.Text, CMB_Discount.Text, Cmb_tax.Text, lab_Total.Text, discounttotal, taxrstotal, Cmb_Doctor.SelectedValue.ToString(), Cmb_Doctor.Text, RTB_Notes.Text.Replace("'", " "), label33.Text, id, stock_id, DTP_Date.Value.ToString("MM/dd/yyyy"), lab_tonurse.Text, "", lab_service);

                                //DGV_Invoice.Rows.Add(id, procedure_item.Text, txtCost.Text, NMUP_Unit.Text, txt_Discount.Text, CMB_Discount.Text, Cmb_tax.Text, lab_Total.Text, discounttotal, taxrstotal, Cmb_Doctor.SelectedValue.ToString(), Cmb_Doctor.Text, RTB_Notes.Text.Replace("'", " "), label33.Text, id, stock_id, DTP_Date.Value.ToString("MM/dd/yyyy"),lab_tonurse.Text,"",lab_service);
                                DGV_Invoice.Rows[DGV_Invoice.Rows.Count - 1].Cells[18].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                                //DGV_Invoice.Rows[DGV_Invoice.Rows.Count - 1].Cells[17].Value = PappyjoeMVC.Properties.Resources.appnotfilled;// "To nif";

                                img.ImageLayout = DataGridViewImageCellLayout.Normal;
                                decimal totcost = 0;
                                decimal discount1 = 0, tax1 = 0;
                                for (int i = 0; i < DGV_Invoice.Rows.Count; i++)
                                {
                                    totcost = totcost + (decimal.Parse(DGV_Invoice.Rows[i].Cells[2].Value.ToString()) * decimal.Parse(DGV_Invoice.Rows[i].Cells[3].Value.ToString()));
                                    discount1 = discount1 + decimal.Parse(DGV_Invoice.Rows[i].Cells[8].Value.ToString());
                                    tax1 = tax1 + decimal.Parse(DGV_Invoice.Rows[i].Cells[9].Value.ToString());
                                }
                                txt_TotalCost.Text = totcost.ToString("F");
                                txt_TotalDiscount.Text = discount1.ToString("F");
                                txt_TotalTax.Text = tax1.ToString("F");
                                txt_GrantTotal.Text = Convert.ToString((totcost + tax1) - discount1);
                            }
                        }
                        else if (invoicetype == "service")
                        {
                            int check_planid = 0;
                            if (P_Plan_id != "0")
                            {
                                for (int i = 0; i < DGV_Invoice.Rows.Count; i++)
                                {
                                    if (P_Plan_id == DGV_Invoice.Rows[i].Cells[14].Value.ToString())
                                    {
                                        check_planid = check_planid + 1;
                                    }
                                }
                            }
                            if (P_Completed_id != "0")
                            {
                                for (int i = 0; i < DGV_Invoice.Rows.Count; i++)
                                {
                                    if (P_Completed_id == DGV_Invoice.Rows[i].Cells[15].Value.ToString())
                                    {
                                        check_planid = check_planid + 1;
                                    }
                                }
                            }
                            if (check_planid == 0)
                            {
                                DGV_Invoice.Rows.Add(id, procedure_item.Text, txtCost.Text, NMUP_Unit.Text, txt_Discount.Text, CMB_Discount.Text, Cmb_tax.Text, lab_Total.Text, discounttotal, taxrstotal, Cmb_Doctor.SelectedValue.ToString(), Cmb_Doctor.Text, RTB_Notes.Text.Replace("'", " "), label33.Text, P_Plan_id, P_Completed_id, DTP_Date.Value.ToString("MM/dd/yyyy"),lab_tonurse.Text,"", lab_service);
                                DGV_Invoice.Rows[DGV_Invoice.Rows.Count - 1].Cells[18].Value = PappyjoeMVC.Properties.Resources.deleteicon;

                                //DGV_Invoice.Rows[DGV_Invoice.Rows.Count - 1].Cells[17].Value = PappyjoeMVC.Properties.Resources.appnotfilled;// "To nif";
                                img.ImageLayout = DataGridViewImageCellLayout.Normal;
                                decimal totcost = 0;
                                decimal discount1 = 0, tax1 = 0;
                                for (int i = 0; i < DGV_Invoice.Rows.Count; i++)
                                {
                                    totcost = totcost + (decimal.Parse(DGV_Invoice.Rows[i].Cells[2].Value.ToString()) * decimal.Parse(DGV_Invoice.Rows[i].Cells[3].Value.ToString()));
                                    discount1 = discount1 + decimal.Parse(DGV_Invoice.Rows[i].Cells[8].Value.ToString());
                                    tax1 = tax1 + decimal.Parse(DGV_Invoice.Rows[i].Cells[9].Value.ToString());
                                }
                                txt_TotalCost.Text = totcost.ToString("F");
                                txt_TotalDiscount.Text = discount1.ToString("F");
                                txt_TotalTax.Text = tax1.ToString("F");
                                txt_GrantTotal.Text = Convert.ToString((totcost + tax1) - discount1);
                            }
                            else
                            {
                                MessageBox.Show("This Plan/Completed treatment already exist. Please select another treatment and try again...");
                                return;
                            }
                            txt_Discount.Clear();
                            CMB_Discount.Text = "";
                            Cmb_tax.Text = "";
                            procedure_item.Text = "";
                            txtCost.Text = "";
                            NMUP_Unit.Text = "";
                            txt_Discount.Hide();
                            CMB_Discount.Hide();
                            Cmb_tax.Hide();
                            lab_AddDiscount.Show();
                            lab_AddTax.Show();
                            CMB_Discount.Text = "INR";
                            txt_Discount.Text = "0";
                            Cmb_tax.Text = "NA";
                            lab_tonurse.Text = "No";
                            lab_service = "No";
                        }
                    }
                    else
                    {
                        DGV_Invoice.Rows.Add();
                        int row = DGV_Invoice.Rows.Count - 1;
                        DGV_Invoice.Rows[row].Cells["service_id"].Value = ser_id;
                        DGV_Invoice.Rows[row].Cells["service"].Value = procedure_item.Text;
                        DGV_Invoice.Rows[row].Cells["cost"].Value = txtCost.Text;
                        DGV_Invoice.Rows[row].Cells["unit"].Value = NMUP_Unit.Text;
                        DGV_Invoice.Rows[row].Cells["discount"].Value = txt_Discount.Text;
                        DGV_Invoice.Rows[row].Cells["disc_type"].Value = CMB_Discount.Text;
                        DGV_Invoice.Rows[row].Cells["tax"].Value = Cmb_tax.Text;
                        DGV_Invoice.Rows[row].Cells["total"].Value = lab_Total.Text;
                        DGV_Invoice.Rows[row].Cells["discount_in_rs"].Value = discounttotal;
                        DGV_Invoice.Rows[row].Cells["tax_in_rs"].Value = taxrstotal;
                        DGV_Invoice.Rows[row].Cells["dr_id"].Value = Cmb_Doctor.SelectedValue.ToString();
                        DGV_Invoice.Rows[row].Cells["doctor"].Value = Cmb_Doctor.SelectedValue.ToString();
                        DGV_Invoice.Rows[row].Cells["notes"].Value = RTB_Notes.Text.Replace("'", " ");
                        DGV_Invoice.Rows[row].Cells["Tooth1"].Value = label33.Text;
                        DGV_Invoice.Rows[row].Cells["pl_id"].Value = id;
                        DGV_Invoice.Rows[row].Cells["complete_id"].Value = stock_id;
                        DGV_Invoice.Rows[row].Cells["i_date"].Value = DTP_Date.Value.ToString("MM/dd/yyyy");
                        DGV_Invoice.Rows[row].Cells["Tonurse"].Value = lab_tonurse.Text;
                        DGV_Invoice.Rows[row].Cells["labservice"].Value = lab_service;
                        DGV_Invoice.Rows[row].Cells[18].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                        img.ImageLayout = DataGridViewImageCellLayout.Normal;
                        rowindex++;
                        //lab_tonurse.Text,"",lab_service
                        decimal totcost = 0;
                        decimal discount1 = 0, tax1 = 0;
                        for (int i = 0; i < DGV_Invoice.Rows.Count; i++)
                        {
                            totcost = totcost + (decimal.Parse(DGV_Invoice.Rows[i].Cells[2].Value.ToString()) * decimal.Parse(DGV_Invoice.Rows[i].Cells[3].Value.ToString()));
                            discount1 = discount1 + decimal.Parse(DGV_Invoice.Rows[i].Cells[8].Value.ToString());
                            tax1 = tax1 + decimal.Parse(DGV_Invoice.Rows[i].Cells[9].Value.ToString());
                        }
                        txt_TotalCost.Text = totcost.ToString("F");
                        txt_TotalDiscount.Text = discount1.ToString("F");
                        txt_TotalTax.Text = tax1.ToString("F");
                        txt_GrantTotal.Text = Convert.ToString((totcost + tax1) - discount1);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DGV_Invoice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 18 && e.RowIndex > -1)
                {
                    DialogResult res = MessageBox.Show("Are you sure you want to delete?", "Delete confirmation",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.No)
                    {
                    }
                    else
                    {
                        DGV_Invoice.Rows.RemoveAt(this.DGV_Invoice.SelectedRows[0].Index);
                        if (DGV_Invoice.Rows.Count == 0)
                        {
                            txt_TotalCost.Text = "Total cost";
                            txt_TotalDiscount.Text = "Total Discount";
                            txt_TotalTax.Text = "Total Tax";
                            txt_GrantTotal.Text = "Grant Total";
                        }
                        if (CMB_Discount.SelectedIndex == 0)
                        {
                            delete_gridrow_calculation();
                        }
                        else if (CMB_Discount.SelectedIndex == 1)
                        {
                            delete_gridrow_calculation();
                        }
                        txt_Discount.Hide();
                        CMB_Discount.Hide();
                        Cmb_tax.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void delete_gridrow_calculation()
        {
            decimal totcost = 0, total2 = 0;
            float discount1, dicount2 = 0, tax1 = 0;
            for (int i = 0; i < DGV_Invoice.Rows.Count; i++)
            {
                decimal unitcost = decimal.Parse(DGV_Invoice.Rows[i].Cells[2].Value.ToString());
                decimal quantity = decimal.Parse(DGV_Invoice.Rows[i].Cells[3].Value.ToString());
                decimal totalcost = unitcost * quantity;
                totcost = totcost + totalcost;
                txt_TotalCost.Text = totcost.ToString();
                decimal total1 = Convert.ToDecimal(DGV_Invoice.Rows[i].Cells[7].Value.ToString());
                total2 = total2 + total1;
                txt_GrantTotal.Text = total2.ToString();
                float dicount = float.Parse(DGV_Invoice.Rows[i].Cells[8].Value.ToString());
                discount1 = float.Parse(totalcost.ToString()) * (dicount / 100);
                dicount2 = dicount2 + dicount;
                txt_TotalDiscount.Text = dicount2.ToString();
                float tax = float.Parse(DGV_Invoice.Rows[i].Cells[9].Value.ToString());
                tax1 = tax1 + tax;
                txt_TotalTax.Text = tax1.ToString();
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
        private void btn_SaveInvoice_Click(object sender, EventArgs e)
        {
            if (btn_SaveInvoice.Text == "SAVE INVOICE")
            {
                string cs = PappyjoeMVC.Model.Connection.MyGlobals.trans_connectionstring;
                using (MySqlConnection con = new MySqlConnection(cs))
                {
                    con.Open();
                    MySqlTransaction trans = con.BeginTransaction();
                    try
                    {
                        DataTable receipt = this.cntrl.select_All_invoicedata(text_billno.Text, con, trans);
                        if (receipt.Rows.Count <= 0)
                        {
                            if (doctor_id == "0")
                            {
                                doctor_id = admin_id;
                            }
                            if (DGV_Invoice.RowCount > 0)
                            {
                                int j = 1;
                                //this.cntrl.save_invoice_main(patient_id, linkL_Name.Text.ToString(), text_billno.Text.ToString(), con, trans);
                                //bhj
                                this.cntrl.save_invoice_main1(DTP_Date.Value.ToString("yyyy-MM-dd"),patient_id, linkL_Name.Text.ToString(), text_billno.Text.ToString(), con, trans);
                                string dt1 = this.cntrl.get_invoiceMain_maxid(con, trans);
                                int invoice_main_id = 0;
                                try
                                {
                                    if (Int32.Parse(dt1) == 0)
                                    {
                                        j = 1;
                                        invoice_main_id = 0;
                                    }
                                    else
                                    {
                                        invoice_main_id = Int32.Parse(dt1);
                                    }
                                }
                                catch
                                {
                                    j = 1;
                                    invoice_main_id = 0;
                                }
                                j = invoice_main_id;
                                int jj = 0;
                                for (int i = 0; i < DGV_Invoice.Rows.Count; i++)
                                {
                                    if (DGV_Invoice[14, i].Value.ToString() == "0" && DGV_Invoice[15, i].Value.ToString() == "0")
                                    {
                                        if (jj == 0)
                                        {
                                            DataTable dt_pt = this.cntrl.get_completed_id(patient_id, DTP_Date.Value.ToString("yyyy-MM-dd"), con, trans);
                                            if (dt_pt.Rows.Count == 0)
                                            {
                                                this.cntrl.save_completedid(DTP_Date.Value.ToString("yyyy-MM-dd"), patient_id, con, trans);
                                                string dt = this.cntrl.get_completedMaxid_trans(con, trans);
                                                int completed_id;
                                                try
                                                {
                                                    if (Int32.Parse(dt) == 0)
                                                    {
                                                        jj = 1;
                                                        completed_id = 0;
                                                    }
                                                    else
                                                    {
                                                        completed_id = Int32.Parse(dt);
                                                    }
                                                }
                                                catch
                                                {
                                                    jj = 1;
                                                    completed_id = 0;
                                                }

                                                jj = completed_id;
                                            }
                                            else
                                            {
                                                jj = Int32.Parse(dt_pt.Rows[0][0].ToString());
                                            }
                                        }
                                       
                                        this.cntrl.Save_tbl_completed_procedures(jj, patient_id, DGV_Invoice[0, i].Value.ToString(), DGV_Invoice[1, i].Value.ToString(), DGV_Invoice[3, i].Value.ToString(), DGV_Invoice[2, i].Value.ToString(), DGV_Invoice[5, i].Value.ToString(), DGV_Invoice[4, i].Value.ToString(), DGV_Invoice[7, i].Value.ToString(), DGV_Invoice[8, i].Value.ToString(), DGV_Invoice[12, i].Value.ToString(), DGV_Invoice[10, i].Value.ToString(), "0", DGV_Invoice[13, i].Value.ToString(), DGV_Invoice.Rows[i].Cells["labservice"].Value.ToString(), con, trans);
                                        string dt_procedure = this.cntrl.get_completed_procedure_maxid(con, trans);
                                        int completed_max_id;
                                        if (Int32.Parse(dt_procedure) == 0)
                                        {
                                            completed_max_id = 0;
                                        }
                                        else
                                        {
                                            completed_max_id = Int32.Parse(dt_procedure);
                                        }
                                        this.cntrl.save_invoice_details(text_billno.Text.ToString(), linkL_Name.Text.ToString(), patient_id, DGV_Invoice[0, i].Value.ToString(), DGV_Invoice[1, i].Value.ToString(), DGV_Invoice[3, i].Value.ToString(), DGV_Invoice[2, i].Value.ToString(), DGV_Invoice[4, i].Value.ToString(), DGV_Invoice[5, i].Value.ToString(), DGV_Invoice[6, i].Value.ToString(), DGV_Invoice[7, i].Value.ToString(), DGV_Invoice[12, i].Value.ToString(), DGV_Invoice[8, i].Value.ToString(), DGV_Invoice[9, i].Value.ToString(), DGV_Invoice[10, i].Value.ToString(), DGV_Invoice[8, i].Value.ToString(), DGV_Invoice[9, i].Value.ToString(), j, completed_max_id, DGV_Invoice.Rows[i].Cells["labservice"].Value.ToString(), con, trans);
                                        this.cntrl.update_invoice_nurse_notify("False", dt1, con, trans);
                                    }
                                    if (DGV_Invoice[14, i].Value.ToString() != "0")
                                    {
                                        if (jj == 0)
                                        {
                                            DataTable dt_pt = this.cntrl.get_completed_id(patient_id, DTP_Date.Value.ToString("yyyy-MM-dd"), con, trans);
                                            if (dt_pt.Rows.Count == 0)
                                            {
                                                this.cntrl.save_completedid(DTP_Date.Value.ToString("yyyy-MM-dd"), patient_id, con, trans);
                                                string dt = this.cntrl.get_completedMaxid_trans(con, trans);//get_completedMaxid
                                                int completed_id;
                                                try
                                                {
                                                    if (Int32.Parse(dt) == 0)
                                                    {
                                                        jj = 1;
                                                        completed_id = 0;
                                                    }
                                                    else
                                                    {
                                                        completed_id = Int32.Parse(dt);
                                                    }
                                                }
                                                catch
                                                {
                                                    jj = 1;
                                                    completed_id = 0;
                                                }

                                                jj = completed_id;
                                            }
                                            else
                                            {
                                                jj = Int32.Parse(dt_pt.Rows[0][0].ToString());
                                            }
                                        }
                                        this.cntrl.Save_tbl_completed_procedures(jj, patient_id, DGV_Invoice[0, i].Value.ToString(), DGV_Invoice[1, i].Value.ToString(), DGV_Invoice[3, i].Value.ToString(), DGV_Invoice[2, i].Value.ToString(), DGV_Invoice[5, i].Value.ToString(), DGV_Invoice[4, i].Value.ToString(), DGV_Invoice[7, i].Value.ToString(), DGV_Invoice[8, i].Value.ToString(), DGV_Invoice[12, i].Value.ToString(), DGV_Invoice[10, i].Value.ToString(), DGV_Invoice[14, i].Value.ToString(), DGV_Invoice[13, i].Value.ToString(), DGV_Invoice.Rows[i].Cells["labservice"].Value.ToString(), con, trans);
                                        string dt_procedure = this.cntrl.get_completed_procedure_maxid(con,trans);
                                        int completed_max_id;
                                        if (Int32.Parse(dt_procedure) == 0)
                                        {
                                            completed_max_id = 0;
                                        }
                                        else
                                        {
                                            completed_max_id = Int32.Parse(dt_procedure);
                                        }
                                        this.cntrl.update_treatment_status(DGV_Invoice[14, i].Value.ToString(),con,trans);
                                        this.cntrl.save_invoice_items(text_billno.Text.ToString(), linkL_Name.Text.ToString(), patient_id, DGV_Invoice[0, i].Value.ToString(), DGV_Invoice[1, i].Value.ToString(), DGV_Invoice[3, i].Value.ToString(), DGV_Invoice[2, i].Value.ToString(), DGV_Invoice[4, i].Value.ToString(), DGV_Invoice[5, i].Value.ToString(), DGV_Invoice[6, i].Value.ToString(), DGV_Invoice[7, i].Value.ToString(), DGV_Invoice[12, i].Value.ToString(), DGV_Invoice[8, i].Value.ToString(), DGV_Invoice[9, i].Value.ToString(), DGV_Invoice[10, i].Value.ToString(), DGV_Invoice[8, i].Value.ToString(), DGV_Invoice[9, i].Value.ToString(), j, DGV_Invoice[14, i].Value.ToString(), completed_max_id, "No", DGV_Invoice.Rows[i].Cells["labservice"].Value.ToString(), con, trans);
                                        this.cntrl.update_invoice_nurse_notify("False", dt1, con, trans);//nurse notification

                                    }
                                    if (DGV_Invoice[15, i].Value.ToString() != "0")
                                    {
                                        this.cntrl.save_invoice_items(text_billno.Text.ToString(), linkL_Name.Text.ToString(), patient_id, DGV_Invoice[0, i].Value.ToString(), DGV_Invoice[1, i].Value.ToString(), DGV_Invoice[3, i].Value.ToString(), DGV_Invoice[2, i].Value.ToString(), DGV_Invoice[4, i].Value.ToString(), DGV_Invoice[5, i].Value.ToString(), DGV_Invoice[6, i].Value.ToString(), DGV_Invoice[7, i].Value.ToString(), DGV_Invoice[12, i].Value.ToString(), DGV_Invoice[8, i].Value.ToString(), DGV_Invoice[9, i].Value.ToString(), DGV_Invoice[10, i].Value.ToString(), DGV_Invoice[8, i].Value.ToString(), DGV_Invoice[9, i].Value.ToString(), j, DGV_Invoice[14, i].Value.ToString(), Convert.ToInt32(DGV_Invoice[15, i].Value.ToString()), DGV_Invoice.Rows[i].Cells["Tonurse"].Value.ToString(), DGV_Invoice.Rows[i].Cells["labservice"].Value.ToString(), con, trans);
                                        this.cntrl.Set_completed_status0(DGV_Invoice[15, i].Value.ToString(), con, trans);
                                        this.cntrl.update_invoice_nurse_notify("True", dt1, con, trans);////nurse notification
                                    }
                                }
                                DataTable invo = this.cntrl.Get_invoice_prefix();
                                int a = int.Parse(invo.Rows[0]["invoice_number"].ToString());
                                a = a + 1;
                                this.cntrl.update_invoice_autoid(a, con, trans);
                                if (doctor_id == admin_id)
                                {
                                    doctor_id = "0";
                                }
                                trans.Commit();
                                con.Close();
                                string dt_time = DateTime.Now.Date.ToString("yyyy-MM-dd");
                                DateTime Timeonly = DateTime.Now;
                                this.cntrl.save_log(doctor_id, "Invoice", "Add new invoice", dt_time, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Add");
                                var form2 = new Invoice();
                                form2.doctor_id = doctor_id;
                                form2.patient_id = patient_id;
                                openform(form2);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Bill Number already exists...!", "Duplication encountered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch(Exception ex)
                    {
                        trans.Rollback();
                        con.Close();
                        MessageBox.Show(ex.Message, "SAVE  function is failed !..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                   

                }
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
                        DateTime? date = null; string compl_id = "",main_id="";
                        string dt0 = this.cntrl.Get_invoiceDate(invoice_plan_id_, con, trans);// text_billno.Text.ToString());
                        if (dt0 != "0")
                        {
                            date = Convert.ToDateTime(dt0);
                        }
                        else
                            date = DateTime.Now.Date;
                        // get completed id
                        DataTable dt_completed_id = this.cntrl.get_comleted_id(invoice_plan_id_, con, trans);
                        if (dt_completed_id.Rows.Count > 0)
                        {
                            compl_id = dt_completed_id.Rows[0]["completed_id"].ToString();
                        }
                        //delete invoice item table
                        DataTable dt_main_id = this.cntrl.get_cmple_main_id(compl_id, con, trans);
                        if(dt_main_id.Rows.Count>0)
                        {
                            main_id= dt_main_id.Rows[0]["plan_main_id"].ToString();
                        }

                        //this.cntrl.delete_completed(main_id, con, trans);
                        this.cntrl.delete_invoice(invoice_plan_id_, con, trans);

                        this.cntrl.update_completed_main(Convert.ToDateTime(DTP_Date.Value).ToString("yyyy-MM-dd"), patient_id, main_id, con, trans);
                        this.cntrl.update_invoice_main(Convert.ToDateTime(DTP_Date.Value).ToString("yyyy-MM-dd"), patient_id, linkL_Name.Text.ToString(), text_billno.Text.ToString(), con, trans);



                        // DataTable dt_receipt = this.cntrl.get_receipt_details(patient_id, text_billno.Text.ToString(), con, trans);

                        //if(dt_receipt.Rows.Count>0)
                        //{
                        //    this.cntrl.delete_receipt(dt_receipt.Rows[0]["receipt_no"].ToString(), patient_id, con, trans);
                        //}
                        for (int i = 0; i < DGV_Invoice.Rows.Count; i++)
                        {
                            this.cntrl.get_taxValue(DGV_Invoice[5, i].Value.ToString(), con, trans);
                            string tax = DGV_Invoice[5, i].Value.ToString();
                            string discount = DGV_Invoice[3, i].Value.ToString();
                            //DataTable dt_receipt_date = this.cntrl.get_receipt_details1(patient_id, text_billno.Text.ToString());
                            // if (dt_receipt_date.Rows.Count > 0)
                            //{
                            //    this.cntrl.save_incove_items(text_billno.Text.ToString(), linkL_Name.Text.ToString(), patient_id, DGV_Invoice[0, i].Value.ToString(), DGV_Invoice[1, i].Value.ToString(), DGV_Invoice[3, i].Value.ToString(), DGV_Invoice[2, i].Value.ToString(), discount, DGV_Invoice[5, i].Value.ToString(), tax, DGV_Invoice[7, i].Value.ToString(), Convert.ToDateTime(date).ToString("yyyy-MM-dd"), RTB_Notes.Text.ToString(), txt_TotalCost.Text.ToString(), txt_TotalDiscount.Text.ToString(), txt_TotalTax.Text.ToString(), txt_GrantTotal.Text.ToString(), DGV_Invoice[10, i].Value.ToString(), DGV_Invoice[8, i].Value.ToString(), DGV_Invoice[9, i].Value.ToString(), DGV_Invoice[7, i].Value.ToString(), invoice_plan_id_, DGV_Invoice.Rows[i].Cells["Tonurse"].Value.ToString(), DGV_Invoice.Rows[i].Cells["labservice"].Value.ToString(), con, trans);
                            //    this.cntrl.update_invoice_main(Convert.ToDateTime(date).ToString("yyyy-MM-dd"), patient_id, linkL_Name.Text.ToString(), text_billno.Text.ToString(), con, trans);
                            //   // this.cntrl.Save_tbl_completed_procedures(0, patient_id, DGV_Invoice[0, i].Value.ToString(), DGV_Invoice[1, i].Value.ToString(), DGV_Invoice[3, i].Value.ToString(), DGV_Invoice[2, i].Value.ToString(), DGV_Invoice[5, i].Value.ToString(), DGV_Invoice[4, i].Value.ToString(), DGV_Invoice[7, i].Value.ToString(), DGV_Invoice[8, i].Value.ToString(), DGV_Invoice[12, i].Value.ToString(), DGV_Invoice[10, i].Value.ToString(), "0", DGV_Invoice[13, i].Value.ToString(), DGV_Invoice.Rows[i].Cells["labservice"].Value.ToString(), con, trans);

                            //}
                            //else {
                            //bhj   add update invoice main  //update_tbl_completed_procedures   Save_tbl_completed_procedures
                            this.cntrl.update_tbl_completed_procedures (Convert.ToInt32(main_id), patient_id, DGV_Invoice[0, i].Value.ToString(), DGV_Invoice[1, i].Value.ToString(), DGV_Invoice[3, i].Value.ToString(), DGV_Invoice[2, i].Value.ToString(), DGV_Invoice[5, i].Value.ToString(), DGV_Invoice[4, i].Value.ToString(), DGV_Invoice[7, i].Value.ToString(), DGV_Invoice[8, i].Value.ToString(), DGV_Invoice[12, i].Value.ToString(), Convert.ToDateTime(DTP_Date.Value).ToString("yyyy-MM-dd"),DGV_Invoice[10, i].Value.ToString(), "0", DGV_Invoice[13, i].Value.ToString(), DGV_Invoice.Rows[i].Cells["labservice"].Value.ToString(), con, trans);
                            DataTable dt_pi_id = this.cntrl.get_proce_wise_cmple_id(main_id, DGV_Invoice[0, i].Value.ToString(), con, trans);
                            //string dt_procedure = this.cntrl.get_completed_procedure_maxid(con, trans);
                            string  completed_max_id;
                            if (dt_pi_id.Rows.Count> 0)
                            {
                                completed_max_id = dt_pi_id.Rows[0][0].ToString();
                            }
                            else
                            {
                                completed_max_id = "0";
                            }
                            this.cntrl.save_incove_items1(text_billno.Text.ToString(), linkL_Name.Text.ToString(), patient_id, DGV_Invoice[0, i].Value.ToString(), DGV_Invoice[1, i].Value.ToString(), DGV_Invoice[3, i].Value.ToString(), DGV_Invoice[2, i].Value.ToString(), discount, DGV_Invoice[5, i].Value.ToString(), tax, DGV_Invoice[7, i].Value.ToString(), Convert.ToDateTime(DTP_Date.Value).ToString("yyyy-MM-dd"), RTB_Notes.Text.ToString(), txt_TotalCost.Text.ToString(), txt_TotalDiscount.Text.ToString(), txt_TotalTax.Text.ToString(), txt_GrantTotal.Text.ToString(), DGV_Invoice[10, i].Value.ToString(), DGV_Invoice[8, i].Value.ToString(), DGV_Invoice[9, i].Value.ToString(), DGV_Invoice[7, i].Value.ToString(), invoice_plan_id_, completed_max_id.ToString(), DGV_Invoice.Rows[i].Cells["Tonurse"].Value.ToString(), DGV_Invoice.Rows[i].Cells["labservice"].Value.ToString(), con, trans);

                            //}
                            if (DGV_Invoice[11, i].Value.ToString() != "0")
                            {
                                this.cntrl.Set_completed_status0(DGV_Invoice[11, i].Value.ToString(), con, trans);
                            }


                            //string dt_procedure = this.cntrl.get_completed_procedure_maxid();("yyyy-MM-dd")

                            //int updateinvoice = this.cntrl.updatetotal(0, text_billno.Text.ToString(), patient_id, DGV_Invoice[1, i].Value.ToString(), con, trans);
                            //if (dt_receipt.Rows.Count > 0)
                            //{
                            //    int inv1 = this.cntrl.save_payment(dt_receipt.Rows[i]["advance"].ToString(), dt_receipt.Rows[i]["receipt_no"].ToString(), Convert.ToDecimal(txt_GrantTotal.Text.ToString()), text_billno.Text.ToString(), DGV_Invoice[1, i].Value.ToString(), "Cash", patient_id, Convert.ToDateTime(dt_receipt.Rows[i]["payment_date"].ToString()).ToString("yyyy-MM-dd"), DGV_Invoice[10, i].Value.ToString(), "0", txt_GrantTotal.Text.ToString(), DGV_Invoice[2, i].Value.ToString(), dt_receipt.Rows[i]["pt_name"].ToString(), con, trans);
                            //    this.cntrl.update_invoice_status0(Convert.ToDecimal(invoice_plan_id_), con, trans);
                            //}
                        }
                        trans.Commit();
                        con.Close();
                        string dt_time = DateTime.Now.Date.ToString("yyyy-MM-dd");
                        DateTime Timeonly = DateTime.Now;
                        this.cntrl.save_log(doctor_id, "Invoice", "Edit new invoice", dt_time, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Edit");
                        //this.cntrl.save_log(doctor_id, "Invoice", "Edit new invoice", "Edit");
                        btn_SaveInvoice.Text = "SAVE";
                        var form1 = new Invoice();
                        form1.doctor_id = doctor_id;
                        form1.patient_id = patient_id;
                        openform(form1);
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        con.Close();
                        MessageBox.Show(ex.Message, " UPDATE function is failed !..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                }
            }

        }

        private void Cmb_tax_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string ctax = Cmb_tax.SelectedText.ToString();
            if (Cmb_tax.SelectedIndex == 4)
            {
                P_tax = 0;
            }
            else
            {
                string dt = this.cntrl.select_taxValue(Cmb_tax.SelectedValue.ToString());
                if (Convert.ToDecimal(dt) > 0)
                {
                    P_tax = Convert.ToDecimal(dt);
                }
                else
                {
                    P_tax = 0;
                }
            }
        }


        private void btn_MainCancel_Click(object sender, EventArgs e)
        {
            var form2 = new Invoice();
            form2.doctor_id = doctor_id;
            form2.patient_id = patient_id;
            openform(form2);
        }

        private void linkL_Name_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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

        private void lab_ShowChildteeth_Click(object sender, EventArgs e)
        {

            if (lab_ShowChildteeth.Text == "Show ChildTeeth")
            {
                panel4.Show();
                lab_ShowChildteeth.Text = "Hide ChildTeeth";
            }
            else
            {
                panel4.Hide();
                lab_ShowChildteeth.Text = "Show ChildTeeth";
            }
        }

        //private void panel9_Paint(object sender, PaintEventArgs e)
        //{

        //}
        public int rowindex = 0,ser_id=0;

        private void txtCost_TextChanged(object sender, EventArgs e)
        {
            calculations();
        }
        public void totalamt()
        {
            Decimal quant = 0;
            Decimal cost = 0;
            if (NMUP_Unit.Text != "")
            {
                quant = Convert.ToDecimal(NMUP_Unit.Text);
            }
            if (txtCost.Text != "")
            {
                cost = Convert.ToDecimal(txtCost.Text);
            }
            lab_Total.Text = Convert.ToString(quant * cost);
        }

        private void tabControlprocedure_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlprocedure.SelectedTab == tab_labprocedure)
            {
                dgv_labprocedure.Rows.Clear();
                dgv_labprocedure.ColumnHeadersVisible = false;
                DataTable dt_lab = this.cntrl.get_patient_lab_details(patient_id,DateTime.Now.Date.ToString("yyyy-MM-dd"));
                if(dt_lab.Rows.Count>0)
                {
                    for(int i=0;i<dt_lab.Rows.Count;i++)
                    {
                        DataTable dt_tempate = this.cntrl.get_template_result(patient_id, dt_lab.Rows[i]["id"].ToString());
                        DataTable dt_test = this.cntrl.get_test_result(patient_id , dt_lab.Rows[i]["id"].ToString()); 
                        if (dt_tempate.Rows.Count>0)
                        {
                            for(int j=0;j<dt_tempate.Rows.Count;j++)
                            {
                                DataTable dt_tempName = this.cntrl.dt_tempname(dt_tempate.Rows[j]["template_id"].ToString());
                                if(dt_tempName.Rows.Count>0)
                                {
                                    Double gtotal = 0;
                                    gtotal = Convert.ToDouble(dt_tempName.Rows[0]["cost"].ToString());
                                    dgv_labprocedure.Rows.Add(dt_tempName.Rows[0]["procedureid"].ToString(), dt_tempName.Rows[0]["TemplateName"].ToString(), "(Completed)", gtotal.ToString("0.00"), "0","" );//dt_tempName.Rows[j]["id"].ToString()
                                    //}
                                    dgv_labprocedure.Rows[j].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Bold);
                                    dgv_labprocedure.Rows[j].Cells[2].Style.ForeColor = Color.DarkGreen;

                                }


                            }
                        }
                        if(dt_test.Rows.Count>0)
                        {
                            for (int j = 0; j < dt_test.Rows.Count; j++)
                            {
                                DataTable dt_testname = this.cntrl.dt_testname(dt_test.Rows[j]["test_id"].ToString());
                                if(dt_testname.Rows.Count>0)
                                {
                                    Double gtotal = 0;
                                    gtotal = Convert.ToDouble(dt_testname.Rows[0]["cost"].ToString());
                                    dgv_labprocedure.Rows.Add(dt_testname.Rows[0]["procedureid"].ToString(), dt_testname.Rows[0]["testname"].ToString(), "(Completed)", gtotal.ToString("0.00"), "0", "");//dt_tempName.Rows[j]["id"].ToString()
                                    //}
                                    dgv_labprocedure.Rows[j].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Bold);
                                    dgv_labprocedure.Rows[j].Cells[2].Style.ForeColor = Color.DarkGreen;

                                }
                            }
                        }
                    }

                }
                DataTable dt = cntrl.getlabprocedure();
                if(dt.Rows.Count>0)
                {
                    for(int i=0;i<dt.Rows.Count;i++)
                    {
                        Double gtotal = 0;
                        gtotal = Convert.ToDouble(dt.Rows[i]["cost"].ToString());
                        dgv_labprocedure.Rows.Add(dt.Rows[i]["id"].ToString(), dt.Rows[i]["name"].ToString(), "", gtotal.ToString("0.00"), "0", "");
                    }
                }
            }
        }
        public string lab_service = "No";
        private void dgv_labprocedure_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panel3.Show();
            panel6.Show();
            DGV_Invoice.Show();
            panel1.Hide();
            txt_Discount.Hide();
            CMB_Discount.Hide();
            Cmb_tax.Hide();
            lab_AddDiscount.Show();
            lab_AddTax.Show();
            if (e.RowIndex >= 0)
            {
                lab_service = "Yes";
                int r = e.RowIndex;
                id = dgv_labprocedure.Rows[r].Cells[0].Value.ToString();
                P_Plan_id ="0" ;// dgv_labprocedure.Rows[r].Cells[4].Value.ToString();
                P_Completed_id = "0";// dgv_labprocedure.Rows[r].Cells[5].Value.ToString();
                string idd = id;
                panel2.Show();
                DataTable dt = this.cntrl.get_labprocedure_Name(id);
                if (dt.Rows.Count > 0)
                {
                    procedure_item.Text = dt.Rows[0]["name"].ToString();
                    NMUP_Unit.Text = "1";
                    txt_Discount.Text = "0";
                    CMB_Discount.Text = "INR";
                    Cmb_tax.Text = "NA";
                    RTB_Notes.Text = "";
                    RTB_Notes.Width = 500;
                    txtCost.Text = dt.Rows[0]["cost"].ToString();
                    //lab_Total.Text = Convert.ToString(decimal.Round(Convert.ToDecimal(dt.Rows[0]["cost"].ToString()), 2, MidpointRounding.AwayFromZero));
                    lab_Total.Text = Convert.ToString(dt.Rows[0]["cost"].ToString());

                }
                label33.Text = "";
                Lab_teeth.Text = "Teeth";
                if (P_Plan_id != "0")
                {
                    DataTable dt_tratmnt = this.cntrl.Get_treatment_details(P_Plan_id);
                    Fill_Alltreatment_deatils(dt_tratmnt);
                }
                if (P_Completed_id != "0")
                {
                    DataTable dtb = this.cntrl.Get_completed_table_details(P_Completed_id);
                    Fill_Alltreatment_deatils(dtb);
                }
                Chk_MultiplyCost.Checked = false;
                CHk_FullMouth.Checked = false;

                decimal qty = 0;
                decimal cost = 0;
                decimal discount = 0;
                if (NMUP_Unit.Text != "")
                {
                    qty = Convert.ToDecimal(NMUP_Unit.Text);
                }
                if (txtCost.Text != "")
                {
                    cost = Convert.ToDecimal(txtCost.Text);
                }
                if (txt_Discount.Text != "")
                {
                    if (CMB_Discount.Text == "INR")
                    {
                        discount = Convert.ToDecimal(txt_Discount.Text);
                    }
                    else
                    {
                        discount = ((qty * cost) * Convert.ToDecimal(txt_Discount.Text)) / 100;
                    }
                }
                lab_Total.Text = Convert.ToString((qty * cost) - discount);
                discounttotal = discount;
                taxrstotal = 0;
                if (P_tax > 0)
                {
                    lab_Total.Text = Convert.ToString(Convert.ToDecimal(lab_Total.Text) - ((qty * cost) * P_tax / 100));
                    taxrstotal = (qty * cost) * P_tax / 100;
                }
            }
        }

        private void DGV_Invoice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            {
                if (btn_SaveInvoice.Text == "Update")
                {
                    decimal tax = 0;
                    rowindex = e.RowIndex;
                    ser_id = Convert.ToInt32(DGV_Invoice.Rows[rowindex].Cells["service_id"].Value.ToString());

                    procedure_item.Text = DGV_Invoice.Rows[rowindex].Cells["service"].Value.ToString();

                    txtCost.Text = DGV_Invoice.Rows[rowindex].Cells["cost"].Value.ToString();

                    NMUP_Unit.Text = DGV_Invoice.Rows[rowindex].Cells["unit"].Value.ToString();
                    if (Convert.ToDecimal(DGV_Invoice.Rows[rowindex].Cells["discount_in_rs"].Value.ToString()) > 0)
                    {
                        lab_AddDiscount.Visible = false;
                        txt_Discount.Visible = true;
                        CMB_Discount.Visible = true;
                        txt_Discount.Text = DGV_Invoice.Rows[rowindex].Cells["discount_in_rs"].Value.ToString();
                        CMB_Discount.Text = DGV_Invoice.Rows[rowindex].Cells["disc_type"].Value.ToString();
                    }
                    else
                        txt_Discount.Text = "0";

                    if (Convert.ToDecimal(DGV_Invoice.Rows[rowindex].Cells["tax_in_rs"].Value.ToString()) > 0)
                    {
                        Cmb_tax.Text = DGV_Invoice.Rows[rowindex].Cells["tax"].Value.ToString();
                        tax = Convert.ToDecimal(DGV_Invoice.Rows[rowindex].Cells["tax_in_rs"].Value.ToString());
                    }
                    if (DGV_Invoice.Rows[rowindex].Cells["notes"].Value != null && DGV_Invoice.Rows[rowindex].Cells["notes"].Value.ToString() != "")
                        RTB_Notes.Text = DGV_Invoice.Rows[rowindex].Cells["notes"].Value.ToString();
                    else
                        RTB_Notes.Text = "";
                    Cmb_Doctor.Text = DGV_Invoice.Rows[rowindex].Cells["doctor"].Value.ToString();
                    //if (Convert.ToDateTime(DGV_Invoice.Rows[rowindex].Cells["i_date"].Value) != null && Convert.ToDateTime(DGV_Invoice.Rows[rowindex].Cells["i_date"].Value.ToString()).ToString()!="" )
                    //    DTP_Date.Value = Convert.ToDateTime(DGV_Invoice.Rows[rowindex].Cells["i_date"].Value.ToString());
                    //else
                    DTP_Date.Value = DateTime.Now.Date;
                    calculations();
                }
            }
           
        }
    }
}
