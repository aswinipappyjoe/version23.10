using PappyjoeMVC.Controller;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
namespace PappyjoeMVC.View
{
    public partial class Add_Drug : Form
    {
        Add_Item_controller cntrl = new Add_Item_controller();
        public bool editFlag = false;
        DataTable dt_ForEditItems = new DataTable();
        public static int Item_Id;
        public bool cal_flag = false, load_flag = false; string isOneUnitOnly = "", unit2 = "", _isbatch = "", _istax = "", _sUnit = "";
        int UnitMF = 0;
        public static string batch_id = "";
        public string doctor_id = "1";
        public Add_Drug()
        {
            InitializeComponent();
        }

        public Add_Drug(DataTable dtb)
        {
            InitializeComponent();
            dt_ForEditItems = dtb;
            editFlag = true;
        }
        private void frmAddDrug_Load(object sender, EventArgs e)//complete
        {
            BTnClose.Visible = false;
            txt_ItemCode.Enabled = true;
            Fill_combo(); load_flag = true;
            DataTable dt_consume_check = this.cntrl.Get_consume_tick();
            if (dt_consume_check.Rows.Count > 0)
            {
                if (dt_consume_check.Rows[0]["consumables"].ToString() == "Yes")
                {
                    chk_consume.Checked = true;
                }
                else
                    chk_consume.Checked = false;
            }
            else
                chk_consume.Checked = false;
            if (editFlag == true)
            {
                decimal Unit2_Avg = 0;
                DataTable dt = this.cntrl.Get_Item_unitmf(dt_ForEditItems.Rows[0]["id"].ToString());
                string bt_purNo = this.cntrl.get_PurchNumber(dt_ForEditItems.Rows[0]["id"].ToString());
                DataTable dt_purch = this.cntrl.get_tbl_PURCHIT_details(dt_ForEditItems.Rows[0]["id"].ToString());
                //DataTable dtb_Avg = this.cntrl.Get_PURCHIT_wit_itemid(dt_ForEditItems.Rows[0]["id"].ToString());
                DataTable dt_batch = this.cntrl.get_batch_details(dt_ForEditItems.Rows[0]["id"].ToString());
                Item_Id = Convert.ToInt32(dt.Rows[0]["id"].ToString());
                if (dt_ForEditItems.Rows.Count > 0)
                {
                    txt_ItemCode.Enabled = false;
                    txt_ItemName.Text = dt_ForEditItems.Rows[0]["item_name"].ToString();
                    txt_ItemCode.Text = dt_ForEditItems.Rows[0]["item_code"].ToString();
                    Cmb_Manufacture.SelectedValue = Convert.ToInt32(dt_ForEditItems.Rows[0]["manufacturer"].ToString());
                    cmb_supplier.SelectedValue = Convert.ToInt32(dt_ForEditItems.Rows[0]["Sup_code"].ToString());
                    if (dt_ForEditItems.Rows[0]["Cat_Number"].ToString() != "")
                        Cmb_Category.SelectedValue = Convert.ToInt32(dt_ForEditItems.Rows[0]["Cat_Number"].ToString());
                    if (dt_batch.Rows.Count > 0)
                    {
                        txt_batch.Text = dt_batch.Rows[0]["BatchNumber"].ToString();
                        if (dt_batch.Rows[0]["ExpDate"].ToString() != "")
                            dtpExp.Value = Convert.ToDateTime(dt_batch.Rows[0]["ExpDate"].ToString());
                    }
                    if (dt_ForEditItems.Rows[0]["Product_type"].ToString() == "Consumable")
                    {
                        chk_consume.Checked = true;
                    }
                    else
                        chk_consume.Checked = false;
                    txt_Location.Text = dt_ForEditItems.Rows[0]["Location"].ToString();
                    txt_Packing.Text = dt_ForEditItems.Rows[0]["Packing"].ToString();
                    txt_HSN.Text = dt_ForEditItems.Rows[0]["HSN_Number"].ToString();
                    cmb_Unit1.Text = dt_ForEditItems.Rows[0]["Unit1"].ToString();
                    txtShelfNo.Text = dt_ForEditItems.Rows[0]["Shelf_No"].ToString();
                    txtBarcode.Text = dt_ForEditItems.Rows[0]["Barcode"].ToString();
                    txt_op.Text = dt_ForEditItems.Rows[0]["Open_Stock"].ToString();
                    txtVAT.Text = dt_ForEditItems.Rows[0]["GstVat"].ToString();
                    if (txt_op.Text != "" && txt_op.Text != "0")
                    {
                        batch_id = dt_batch.Rows[0]["Entry_No"].ToString();
                        txt_batch.Enabled = false;
                        dtpExp.Enabled = false;
                        lab_batch.Enabled = false;
                        lab_exp.Enabled = false; txt_op.Enabled = false;
                    }
                    else
                    {
                        txt_op.Enabled = true;
                        txt_batch.Enabled = false;
                        dtpExp.Enabled = false;
                        lab_batch.Enabled = false;
                        lab_exp.Enabled = false;
                    }
                    if (dt_ForEditItems.Rows[0]["OneUnitOnly"].ToString() == "True")
                    {
                        chk_OneUnitOnly.Checked = true;
                        inVisible_Controls();
                    }
                    else
                    {
                        chk_OneUnitOnly.Checked = false;
                        Visible_Controls();
                        cmb_unit2.Text = dt_ForEditItems.Rows[0]["Unit2"].ToString();
                    }
                    if (dt_ForEditItems.Rows[0]["ISBatch"].ToString() == "true")
                    {
                        Chk_HavebatchNo.Checked = true;
                    }
                    else
                    {
                        Chk_HavebatchNo.Checked = false;
                    }
                    if (dt_ForEditItems.Rows[0]["Taxable"].ToString() == "true")
                    {
                        Chk_Taxable.Checked = true;
                    }
                    else
                    {
                        Chk_Taxable.Checked = false;
                    }
                    txt_unit2Retailmin2.Visible = false; textBox9.Visible = false; txt_Unit2Dealer2.Visible = false;
                    textBox6.Visible = false; txt_SalesRate2.Visible = false; txt_SalesRateMin.Visible = false;
                    txt_SalesRateMin2.Visible = false;
                    txt_reorderStockQty.Text = dt_ForEditItems.Rows[0]["ReorderQty"].ToString();
                    txt_minimumStockforSale.Text = dt_ForEditItems.Rows[0]["MinimumStock"].ToString();
                    txt_PurchaseRate.Text = Convert.ToDecimal(dt_ForEditItems.Rows[0]["Purch_Rate"].ToString()).ToString("##.00");
                    txt_PurchRate2.Text = Convert.ToDecimal(dt_ForEditItems.Rows[0]["Purch_Rate2"].ToString()).ToString("##.00");
                    txt_SalesRate2.Text = Convert.ToDecimal(dt_ForEditItems.Rows[0]["Sales_Rate2"].ToString()).ToString("##.00");
                    txt_SalesRateMin2.Text = Convert.ToDecimal(dt_ForEditItems.Rows[0]["Sales_Rate_min2"].ToString()).ToString("##.00");
                    txt_SalesRateMax2.Text = Convert.ToDecimal(dt_ForEditItems.Rows[0]["Sales_Rate_Max2"].ToString()).ToString("##.00");
                    txt_UnitMF.Text = Convert.ToInt32(dt_ForEditItems.Rows[0]["UnitMF"].ToString()).ToString();
                    txt_CostBase.Text = Convert.ToDecimal(dt_ForEditItems.Rows[0]["CostBase"].ToString()).ToString("##.00");
                    txt_SalesRate.Text = Convert.ToDecimal(dt_ForEditItems.Rows[0]["Sales_Rate"].ToString()).ToString("##.00");
                    txt_SalesRateMin.Text = Convert.ToDecimal(dt_ForEditItems.Rows[0]["Sales_Rate_min"].ToString()).ToString("##.00");
                    txt_SalesRateMax.Text = Convert.ToDecimal(dt_ForEditItems.Rows[0]["Sales_Rate_Max"].ToString()).ToString("##.00");
                    if (Convert.ToDecimal(dt_ForEditItems.Rows[0]["UnitMF"].ToString()) > 0)
                    {
                        Unit2_Avg = Convert.ToDecimal(txt_CostBase.Text) * Convert.ToDecimal(dt_ForEditItems.Rows[0]["UnitMF"].ToString());
                        txt_unit2Avg_PurchRate.Text = Unit2_Avg.ToString("#.0");
                    }
                    DataTable dtb_item = this.cntrl.get_drugdetails(Item_Id);
                    if (dtb_item.Rows.Count > 0)
                    {
                        chkprescription.Checked = true;
                        int inttype = -1;
                        if (dtb_item.Rows[0]["type"].ToString() != "")
                        {
                            inttype = cmbdrugtype.FindStringExact(dtb_item.Rows[0]["type"].ToString());
                            if (inttype >= 0)
                            {
                                cmbdrugtype.SelectedIndex = inttype;
                            }
                        }
                        inttype = -1;
                        if (dtb_item.Rows[0]["strength"].ToString() != "")
                        {
                            inttype = cmbstrength.FindStringExact(dtb_item.Rows[0]["strength_gr"].ToString());
                            if (inttype >= 0)
                            {
                                cmbstrength.SelectedIndex = inttype;
                            }
                        }
                        txtstrength.Text = dtb_item.Rows[0]["strength"].ToString();
                        txtinstructions.Text = dtb_item.Rows[0]["instructions"].ToString();
                    }
                    save.Text = "UPDATE";
                    if (save.Text == "UPDATE")
                    {
                        if (dt_purch.Rows.Count > 0)
                        {
                            cmb_Unit1.Enabled = false;
                            cmb_unit2.Enabled = false;
                        }
                        else
                        {
                            cmb_Unit1.Enabled = true;
                            cmb_unit2.Enabled = true;
                        }
                    }
                    BTnClose.Visible = true;
                }
            }
            else
            {
                load_itemcode();
                chk_OneUnitOnly.Checked = false;
                cbOneunitOnly();

            }
            load_flag = false;
            txtBarcode.Select();
        }
        public void load_itemcode()
        {
            double item_id = 0;
            DataTable dt_item_code = this.cntrl.max_itemid();
            if (dt_item_code.Rows.Count > 0)
            {
                if (dt_item_code.Rows[0][0].ToString() != "")
                {
                    item_id = Convert.ToDouble(dt_item_code.Rows[0][0].ToString());
                }
            }
            txt_ItemCode.Text = "ITEM" + (item_id + 1);
        }
        public void Fill_combo()
        {
            Cmb_Category.DataSource = null;
            cmb_Unit1.DataSource = null;
            cmb_unit2.DataSource = null;
            Cmb_Manufacture.DataSource = null;
            cmb_supplier.DataSource = null;
            DataTable dt_supplier = this.cntrl.fill_supplier();
            if (dt_supplier.Rows.Count > 0)
            {
                cmb_supplier.DisplayMember = "Supplier_Name";
                cmb_supplier.ValueMember = "Supplier_Code";
                cmb_supplier.DataSource = dt_supplier;
                cmb_supplier.SelectedIndex = 0;
            }
            DataTable dt_Category = this.cntrl.fill_category();
            if (dt_Category.Rows.Count > 0)
            {
                Cmb_Category.DisplayMember = "Name";
                Cmb_Category.ValueMember = "id";
                Cmb_Category.DataSource = dt_Category;
                Cmb_Category.SelectedIndex = 0;
            }
            DataTable dt_Manufacture = this.cntrl.fill_manufacture();
            if (dt_Manufacture.Rows.Count > 0)
            {
                Cmb_Manufacture.DisplayMember = "manufacturer";
                Cmb_Manufacture.ValueMember = "id";
                Cmb_Manufacture.DataSource = dt_Manufacture;
                Cmb_Manufacture.SelectedIndex = 0;
            }
            DataTable dt_Units = this.cntrl.fill_unit();
            if (dt_Units.Rows.Count > 0)
            {
                cmb_Unit1.DisplayMember = "Name";
                cmb_Unit1.ValueMember = "id";
                cmb_Unit1.DataSource = dt_Units;
                cmb_Unit1.SelectedIndex = 0;
            }
            DataTable dt_Units2 = this.cntrl.fill_unit();
            if (dt_Units2.Rows.Count > 0)
            {
                cmb_unit2.DisplayMember = "Name";
                cmb_unit2.ValueMember = "id";
                cmb_unit2.DataSource = dt_Units2;
                cmb_unit2.SelectedIndex = 0;
            }
            DataTable dt1 = this.cntrl.fill_unit();
            cmbstrength.DisplayMember = "name";
            cmbstrength.ValueMember = "id";
            cmbstrength.DataSource = dt1;
            DataTable dt2 = this.cntrl.fill_drugtype();
            cmbdrugtype.DisplayMember = "dr_type";
            cmbdrugtype.ValueMember = "id";
            cmbdrugtype.DataSource = dt2;
        }
        public void inVisible_Controls()
        {
            Lab_Unit2.Visible = false;
            cmb_unit2.Visible = false;
            Lab_whereunit2.Visible = false;
            txt_UnitMF.Visible = false;
            Lab_isunit1.Visible = false;
            Lab_Unit2_1.Visible = false;
            Lab_Unit2LastPurch.Visible = false;
            LabAvg.Visible = false;
            txt_unit2Avg_PurchRate.Visible = false;
            txt_PurchRate2.Visible = false;
            txt_SalesRate2.Visible = false;
            txt_Unit2Dealer2.Visible = false;
            txt_SalesRateMin2.Visible = false;
            txt_unit2Retailmin2.Visible = false;
            txt_SalesRateMax2.Visible = false;
            txt_unit2Retailmax2.Visible = false;
        }
        public void Visible_Controls()
        {
            Lab_Unit2.Visible = true;
            cmb_unit2.Visible = true;
            Lab_whereunit2.Visible = true;
            txt_UnitMF.Visible = true;
            Lab_isunit1.Visible = true;
            Lab_Unit2_1.Visible = true;
            Lab_Unit2LastPurch.Visible = true;
            LabAvg.Visible = true;
            txt_unit2Avg_PurchRate.Visible = true;
            txt_PurchRate2.Visible = true;
            txt_SalesRate2.Visible = true;
            txt_Unit2Dealer2.Visible = true;
            txt_SalesRateMin2.Visible = true;
            txt_unit2Retailmin2.Visible = true;
            txt_SalesRateMax2.Visible = true;
            txt_unit2Retailmax2.Visible = true;
        }
        public void clear()
        {
            txt_ItemName.Text = "";
            txt_ItemCode.Text = "";
            Cmb_Manufacture.Text = "";
            Cmb_Category.Text = "";
            txt_Location.Text = "";
            txt_Packing.Text = "";
            chk_OneUnitOnly.Checked = true;
            Chk_HavebatchNo.Checked = true;
            Chk_Taxable.Checked = false;
            txt_reorderStockQty.Text = "";
            txt_minimumStockforSale.Text = "";
            txt_CostBase.Text = "";
            txt_SalesRate.Text = "";
            txt_SalesRateMin.Text = "";
            txt_SalesRateMax.Text = "";
            txt_PurchaseRate.Text = "";
            txt_SalesRate2.Text = "";
            txt_SalesRateMin2.Text = "";
            txt_SalesRateMax2.Text = "";
            txt_UnitMF.Text = "";
            txt_PurchRate2.Text = "";
            txt_Unit1Dealer2.Text = "";
            txt_unit1Retailmin2.Text = "";
            txt_unit1Retailmax2.Text = "";
            txt_Unit2Dealer2.Text = "";
            txt_unit2Retailmin2.Text = "";
            txt_unit2Retailmax2.Text = "";
            txt_unit2Avg_PurchRate.Text = "";
            txtShelfNo.Text = "";
            txtBarcode.Text = "";
            txt_op.Text = "";
            txt_batch.Text = ""; txtVAT.Text = ""; chkprescription.Checked = false;
            dtpExp.Value = DateTime.Now.Date;
        }

        private void chk_OneUnitOnly_CheckedChanged(object sender, EventArgs e)
        {
            cbOneunitOnly();
        }
        private void cbOneunitOnly()//comple
        {
            if (chk_OneUnitOnly.Checked == true)
            {
                label32.Visible = false;
                Lab_Unit2.Visible = false;
                cmb_unit2.Visible = false;
                Lab_whereunit2.Visible = false;
                txt_UnitMF.Visible = false;
                Lab_isunit1.Visible = false;
                Lab_Unit2_1.Visible = false;
                Lab_Unit2LastPurch.Visible = false;
                LabAvg.Visible = false;
                txt_unit2Avg_PurchRate.Visible = false;
                txt_PurchRate2.Visible = false;
                txt_SalesRate2.Visible = false;
                txt_Unit2Dealer2.Visible = false;
                txt_SalesRateMin2.Visible = false;
                txt_unit2Retailmin2.Visible = false;
                txt_SalesRateMax2.Visible = false;
                txt_unit2Retailmax2.Visible = false;
                btn_Addunit1.Visible = false;
                textBox6.Visible = false;
                textBox9.Visible = false;
                textBox10.Visible = false;
                groupBox1.Size = new Size(795, 138);
                groupBox2.Size = new Size(792, 124);
                groupBox2.Location = new Point(6, 142);
                pnlbottom.Location = new Point(9, 269);
            }
            else
            {
                groupBox1.Size = new Size(795, 167);
                groupBox2.Size = new Size(792, 195);
                groupBox2.Location = new Point(6, 178);
                pnlbottom.Location = new Point(9, 375);
                label32.Visible = true;
                Lab_Unit2.Visible = true;
                cmb_unit2.Visible = true;
                Lab_whereunit2.Visible = true;
                txt_UnitMF.Visible = true;
                Lab_isunit1.Visible = true;
                Lab_Unit2LastPurch.Visible = true;
                LabAvg.Visible = true;
                txt_unit2Avg_PurchRate.Visible = true;
                txt_PurchRate2.Visible = true;
                txt_SalesRateMax2.Visible = true;
                txt_unit2Retailmax2.Visible = true;
                btn_Addunit1.Visible = true;
                textBox10.Visible = true;
            }
        }
        private void btn_Addunit_Click(object sender, EventArgs e)
        {
            Add_Units frm = new Add_Units();
            frm.ShowDialog();
            DataTable dtb = this.cntrl.fill_unit();
            if (dtb.Rows.Count > 0)
            {
                cmb_Unit1.DataSource = dtb;
                cmb_Unit1.SelectedIndex = 0;
            }
        }
        private void btn_Addunit1_Click(object sender, EventArgs e)
        {
            Add_Units frm = new Add_Units();
            frm.ShowDialog();
            DataTable dtb = this.cntrl.fill_unit();
            if (dtb.Rows.Count > 0)
            {
                cmb_Unit1.DataSource = dtb;
                cmb_Unit1.SelectedIndex = 0;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Add_Categeory fr = new Add_Categeory();
            fr.ShowDialog();
            DataTable dt_Category = this.cntrl.fill_category();
            if (dt_Category.Rows.Count > 0)
            {
                Cmb_Category.DisplayMember = "Name";
                Cmb_Category.ValueMember = "id";
                Cmb_Category.DataSource = dt_Category;
                Cmb_Category.SelectedIndex = 0;
            }
        }
        public void calculations(string unit2, DataTable dtb_Avg)
        {
            cal_flag = true; decimal Sales1 = 0; decimal SalesMin = 0; decimal SalesMax = 0; decimal Sales2 = 0; decimal SalesMin1 = 0; decimal SalesMax1 = 0;
            decimal Sales1_ = 0; decimal SalesMin_; decimal SalesMax_ = 0; decimal Sales2_ = 0; decimal SalesMin1_ = 0; decimal SalesMax1_ = 0;
            if (Convert.ToDecimal(txt_PurchaseRate.Text) > 0)
            {
                Sales1 = (Convert.ToDecimal(txt_PurchaseRate.Text) * Convert.ToDecimal(txt_Unit1Dealer2.Text)) / 100;
                Sales1_ = Convert.ToDecimal(txt_PurchaseRate.Text) + Sales1;
                txt_SalesRate.Text = Sales1_.ToString("##.00");
                SalesMin = (Convert.ToDecimal(txt_PurchaseRate.Text) * Convert.ToDecimal(txt_unit1Retailmin2.Text)) / 100;
                SalesMin_ = Convert.ToDecimal(txt_PurchaseRate.Text) + SalesMin;
                txt_SalesRateMin.Text = SalesMin_.ToString("##.00");
                SalesMax = (Convert.ToDecimal(txt_PurchaseRate.Text) * Convert.ToDecimal(txt_unit1Retailmax2.Text)) / 100;
                SalesMax_ = Convert.ToDecimal(txt_PurchaseRate.Text) + SalesMax;
                txt_SalesRateMax.Text = SalesMax_.ToString("##.00");
            }
            if (Convert.ToDecimal(txt_PurchRate2.Text) > 0)
            {
                Sales2 = (Convert.ToDecimal(txt_PurchRate2.Text) * Convert.ToDecimal(txt_Unit2Dealer2.Text)) / 100;
                Sales2_ = Convert.ToDecimal(txt_PurchRate2.Text) + Sales2;
                txt_SalesRate2.Text = Sales2_.ToString("##.00");
                SalesMin1 = (Convert.ToDecimal(txt_PurchRate2.Text) * Convert.ToDecimal(txt_unit2Retailmin2.Text)) / 100;
                SalesMin1_ = Convert.ToDecimal(txt_PurchRate2.Text) + SalesMin1;
                txt_SalesRateMin2.Text = SalesMin1_.ToString("##.00");
                SalesMax1 = (Convert.ToDecimal(txt_PurchRate2.Text) * Convert.ToDecimal(txt_unit2Retailmax2.Text)) / 100;
                SalesMax1_ = Convert.ToDecimal(txt_PurchRate2.Text) + SalesMax1;
                txt_SalesRateMax2.Text = SalesMax1_.ToString("##.00");
            }
            if (dtb_Avg.Rows.Count > 0)
            {
                decimal total = 0, Cost = 0, qty = 0, qty_ = 0, avg = 0, Unit2_Avg = 0;
                foreach (DataRow dr in dtb_Avg.Rows)
                {
                    if (dr["Qty"].ToString() == "Yes")
                    {
                        qty_ = Convert.ToDecimal(dr["Qty"].ToString()) * Convert.ToDecimal(dr["UnitMF"].ToString());
                        Cost = qty_ * Convert.ToDecimal(dr["Rate"].ToString());
                        qty = qty + qty_;
                    }
                    else
                    {
                        Cost = (Convert.ToDecimal(dr["Qty"].ToString()) * Convert.ToDecimal(dr["Rate"].ToString()));
                        qty = qty + Convert.ToDecimal(dr["Qty"].ToString());
                    }
                    total = total + Cost;
                }
                avg = total / qty;
                txt_CostBase.Text = avg.ToString("#.00");
                if (Convert.ToDecimal(dtb_Avg.Rows[0]["UnitMF"].ToString()) > 0)
                {
                    Unit2_Avg = Convert.ToDecimal(txt_CostBase.Text) * Convert.ToDecimal(dtb_Avg.Rows[0]["UnitMF"].ToString());
                    txt_unit2Avg_PurchRate.Text = Unit2_Avg.ToString("#.00");
                }
            }
        }

        private void txt_UnitMF_Click(object sender, EventArgs e)
        {
            if (txt_UnitMF.Text == "0")
            {
                txt_UnitMF.Text = "";
            }
        }

        private void txt_UnitMF_Leave(object sender, EventArgs e)
        {
            if (txt_UnitMF.Text == "")
            {
                txt_UnitMF.Text = "0";
            }
        }

        private void txt_CostBase_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumwithsinglepoint(sender, e);
        }
        public void onlynumwithsinglepoint(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.'))
            { e.Handled = true; }
            TextBox txtDecimal = sender as TextBox;
            if (e.KeyChar == '.' && txtDecimal.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void txt_PurchaseRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumwithsinglepoint(sender, e);
        }

        private void txt_SalesRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumwithsinglepoint(sender, e);
        }

        private void txt_Unit1Dealer2_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumwithsinglepoint(sender, e);
        }

        private void txt_SalesRateMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumwithsinglepoint(sender, e);
        }

        private void txt_unit1Retailmin2_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumwithsinglepoint(sender, e);
        }

        private void txt_SalesRateMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumwithsinglepoint(sender, e);
        }

        private void txt_unit1Retailmax2_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumwithsinglepoint(sender, e);
        }

        private void txt_PurchaseRate_TextChanged(object sender, EventArgs e)
        {
            if (txt_PurchaseRate.Text == "")
            {
                txt_CostBase.Text = "";
                txt_SalesRate.Text = "";
                txt_SalesRateMin.Text = "";
                txt_SalesRateMax.Text = "";
                txt_Unit1Dealer2.Text = "";
                txt_unit1Retailmin2.Text = "";
                txt_unit1Retailmax2.Text = "";
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(txt_PurchaseRate.Text) && !string.IsNullOrWhiteSpace(txt_UnitMF.Text))
                {
                    decimal value1 = 0;
                    if (chk_OneUnitOnly.Checked == false && txt_UnitMF.Text != "0")
                    {
                        value1 = Convert.ToDecimal(txt_PurchaseRate.Text) / Convert.ToDecimal(txt_UnitMF.Text);
                        txt_PurchRate2.Text = value1.ToString("##.00");
                    }
                }
            }
        }

        private void txt_SalesRate_Leave(object sender, EventArgs e)
        {
            if (load_flag == false)
            {
                if (!string.IsNullOrWhiteSpace(txt_PurchaseRate.Text) && !string.IsNullOrWhiteSpace(txt_SalesRate.Text))
                {
                    if (Convert.ToDecimal(txt_SalesRate.Text) >= Convert.ToDecimal(txt_PurchaseRate.Text))
                    {
                    }
                    else
                    {
                        MessageBox.Show("Please enter dealer rate greater than the purchase rate ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_SalesRate.Text = "";
                        txt_SalesRate.Focus();
                    }
                }

            }
        }

        private void txt_SalesRate_TextChanged(object sender, EventArgs e)
        {
        }

        private void txt_SalesRateMin_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void txt_SalesRateMax_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_PurchaseRate.Text) && !string.IsNullOrWhiteSpace(txt_SalesRateMax.Text))
            {
                if (Convert.ToDecimal(txt_SalesRateMax.Text) >= Convert.ToDecimal(txt_PurchaseRate.Text))
                {

                }
                else
                {
                    MessageBox.Show("Please enter maximum sales rate greater than the purchase rate ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_SalesRateMax.Text = "";
                    txt_SalesRateMax.Focus();
                }
            }
        }

        private void txt_SalesRateMax_TextChanged(object sender, EventArgs e)
        {
            if (load_flag == false)
            {

                if (txtSalespertoPurchse.Text != "")
                {
                    decimal value1 = 0, value2 = 0;
                    if (!string.IsNullOrWhiteSpace(txtSalespertoPurchse.Text) && !string.IsNullOrWhiteSpace(txt_SalesRateMax.Text))
                    {
                        value1 = (Convert.ToDecimal(txtSalespertoPurchse.Text) * Convert.ToDecimal(txt_SalesRateMax.Text)) / 100;
                        value2 = Convert.ToDecimal(txt_SalesRateMax.Text) - value1;
                        txt_PurchaseRate.Text = value2.ToString("##.00");
                    }
                    else
                    {
                        txt_PurchaseRate.Text = "";
                    }
                }
                if (txt_SalesRateMax.Text != ".")
                {
                    decimal value1 = 0, value2 = 0;
                    if (!string.IsNullOrWhiteSpace(txt_PurchaseRate.Text) && !string.IsNullOrWhiteSpace(txt_SalesRateMax.Text))
                    {
                        value1 = Convert.ToDecimal(txt_SalesRateMax.Text) - Convert.ToDecimal(txt_PurchaseRate.Text);
                        if (txt_PurchaseRate.Text != ".00")
                        {
                            value2 = (value1 / Convert.ToDecimal(txt_PurchaseRate.Text)) * 100;
                        }
                        txt_unit1Retailmax2.Text = value2.ToString("##.00");
                    }
                    else
                    {
                        txt_unit1Retailmax2.Text = "";
                    }
                }
                if (!string.IsNullOrWhiteSpace(txt_SalesRateMax.Text) && !string.IsNullOrWhiteSpace(txt_UnitMF.Text))
                {
                    decimal value1 = 0;
                    if (chk_OneUnitOnly.Checked == false && txt_UnitMF.Text != "0")
                    {
                        value1 = Convert.ToDecimal(txt_SalesRateMax.Text) / Convert.ToDecimal(txt_UnitMF.Text);
                        txt_SalesRateMax2.Text = value1.ToString("##.00");
                    }
                }
            }

        }

        private void txt_unit2Avg_PurchRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumwithsinglepoint(sender, e);
        }

        private void txt_PurchRate2_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumwithsinglepoint(sender, e);
        }

        private void txt_SalesRate2_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumwithsinglepoint(sender, e);
        }

        private void txt_Unit2Dealer2_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumwithsinglepoint(sender, e);
        }

        private void txt_SalesRateMin2_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumwithsinglepoint(sender, e);
        }

        private void txt_unit2Retailmin2_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumwithsinglepoint(sender, e);
        }

        private void txt_SalesRateMax2_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumwithsinglepoint(sender, e);
        }

        private void txt_unit2Retailmax2_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumwithsinglepoint(sender, e);
        }

        private void txt_SalesRateMin2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_PurchRate2.Text) && !string.IsNullOrWhiteSpace(txt_SalesRateMin2.Text))
            {
                if (Convert.ToDecimal(txt_PurchRate2.Text) > 0 && Convert.ToDecimal(txt_SalesRateMin2.Text) > 0)
                {
                    if (Convert.ToDecimal(txt_SalesRateMin2.Text) >= Convert.ToDecimal(txt_PurchRate2.Text))
                    {

                    }
                    else
                    {
                        MessageBox.Show("Please enter minimum sales rate  greater than the purchase rate ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_SalesRateMin2.Text = "";
                        txt_SalesRateMin2.Focus();
                    }
                }
            }
        }

        private void txt_SalesRateMin2_TextChanged(object sender, EventArgs e)
        {
            if (load_flag == false)
            {
                decimal value1 = 0, value2 = 0;
                if (!string.IsNullOrWhiteSpace(txt_PurchRate2.Text) && !string.IsNullOrWhiteSpace(txt_SalesRateMin2.Text))
                {
                    if (Convert.ToDecimal(txt_PurchRate2.Text) > 0 && Convert.ToDecimal(txt_SalesRateMin2.Text) > 0)
                    {
                        value1 = Convert.ToDecimal(txt_SalesRateMin2.Text) - Convert.ToDecimal(txt_PurchRate2.Text);
                        value2 = (value1 / Convert.ToDecimal(txt_PurchRate2.Text)) * 100;
                        txt_unit2Retailmin2.Text = value2.ToString("##.00");
                    }
                }
                else
                {
                    txt_unit2Retailmin2.Text = "";
                }
            }

        }

        private void txt_SalesRate2_TextChanged(object sender, EventArgs e)
        {
            if (load_flag == false)
            {
                decimal value1 = 0, value2 = 0;
                if (!string.IsNullOrWhiteSpace(txt_PurchRate2.Text) && !string.IsNullOrWhiteSpace(txt_SalesRate2.Text))
                {
                    if (Convert.ToDecimal(txt_PurchRate2.Text) > 0 && Convert.ToDecimal(txt_SalesRate2.Text) > 0)
                    {
                        value1 = Convert.ToDecimal(txt_SalesRate2.Text) - Convert.ToDecimal(txt_PurchRate2.Text);
                        value2 = (value1 / Convert.ToDecimal(txt_PurchRate2.Text)) * 100;
                        txt_Unit2Dealer2.Text = value2.ToString("##.00");
                    }
                }
                else
                {
                    txt_Unit2Dealer2.Text = "";
                }
            }
        }

        private void txt_SalesRate2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_PurchRate2.Text) && !string.IsNullOrWhiteSpace(txt_SalesRate2.Text))
            {
                if (Convert.ToDecimal(txt_PurchRate2.Text) > 0 && Convert.ToDecimal(txt_SalesRate2.Text) > 0)
                {
                    if (Convert.ToDecimal(txt_SalesRate2.Text) >= Convert.ToDecimal(txt_PurchRate2.Text))
                    {

                    }
                    else
                    {
                        MessageBox.Show("Please enter dealer rate  greater than the purchase rate ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_SalesRate2.Text = "";
                        txt_SalesRate2.Focus();
                    }
                }
            }
        }

        private void txt_PurchRate2_TextChanged(object sender, EventArgs e)
        {
            if (txt_PurchRate2.Text == "")
            {
                txt_SalesRate2.Text = "";
                txt_SalesRateMin2.Text = "";
                txt_SalesRateMax2.Text = "";
                txt_Unit2Dealer2.Text = "";
                txt_unit2Retailmin2.Text = "";
                txt_unit2Retailmax2.Text = "";
            }
        }

        private void txt_SalesRateMax2_TextChanged(object sender, EventArgs e)
        {
            if (load_flag == false)
            {
                decimal value1 = 0; decimal value2 = 0;
                if (!string.IsNullOrWhiteSpace(txt_PurchRate2.Text) && !string.IsNullOrWhiteSpace(txt_SalesRateMax2.Text))
                {
                    if (Convert.ToDecimal(txt_PurchRate2.Text) > 0 && Convert.ToDecimal(txt_SalesRateMax2.Text) > 0)
                    {
                        value1 = Convert.ToDecimal(txt_SalesRateMax2.Text) - Convert.ToDecimal(txt_PurchRate2.Text);
                        value2 = (value1 / Convert.ToDecimal(txt_PurchRate2.Text)) * 100;
                        txt_unit2Retailmax2.Text = value2.ToString("##.00");
                    }
                }
                else
                {
                    txt_unit2Retailmax2.Text = "";
                }
            }
        }

        private void txt_SalesRateMax2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_PurchRate2.Text) && !string.IsNullOrWhiteSpace(txt_SalesRateMax2.Text))
            {
                if (Convert.ToDecimal(txt_PurchRate2.Text) > 0 && Convert.ToDecimal(txt_SalesRateMax2.Text) > 0)
                {
                    if (Convert.ToDecimal(txt_SalesRateMax2.Text) >= Convert.ToDecimal(txt_PurchRate2.Text))
                    {

                    }
                    else
                    {
                        MessageBox.Show("Please enter maximum sales rate  greater than the purchase rate ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_SalesRateMax2.Text = "";
                        txt_SalesRateMax2.Focus();
                    }
                }
            }
        }

        private void label43_Click(object sender, EventArgs e)
        {
            pnltax.Visible = true;
            pnl_PurchRateFrmSales.Visible = false;
        }

        private void chkprescription_CheckedChanged(object sender, EventArgs e)
        {
            if (chkprescription.Checked == true)
            {
                pnlprescription.Enabled = true;
                label11.Visible = true;

            }
            else
            {

                pnlprescription.Enabled = false;
                label11.Visible = false;
            }
        }

        private void txtprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumwithsinglepoint(sender, e);
        }

        private void txttax_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumwithsinglepoint(sender, e);
        }

        private void btngenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtprice.Text) && !string.IsNullOrWhiteSpace(txttax.Text))
                {
                    decimal flprice = 0;
                    decimal fltax = 0;
                    decimal flmrp = 0;
                    decimal fltotal = 0;
                    flprice = Convert.ToDecimal(txtprice.Text);
                    fltax = Convert.ToDecimal(txttax.Text);
                    flmrp = flprice * fltax / (fltax + 100);
                    fltotal = flprice - flmrp;
                    txt_SalesRateMax.Text = fltotal.ToString("##.00");
                }
                pnltax.Visible = false;
                txtprice.Text = "";
                txttax.Text = "";
                if (chk_OneUnitOnly.Checked == true)
                {
                }
                else
                {
                    txt_PurchRate2.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            pnltax.Visible = false;
            checkNull(); string isunit2 = "False", IsExpDate = "NO", expdate = ""; int qty = 0;
            if (txt_ItemCode.Text != "" && txt_ItemName.Text != "" && txtVAT.Text != "" && Cmb_Manufacture.Items.Count != 0)
            {
                if (txt_PurchaseRate.Text == "" || txt_SalesRateMax.Text == "")// txt_SalesRate.Text == "" || txt_SalesRateMin.Text == "" ||
                {
                    MessageBox.Show("Must add Purchase/Sales rate...", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (chk_OneUnitOnly.Checked == false)
                {
                    if (txt_PurchRate2.Text == "" || txt_PurchRate2.Text == "0" || txt_SalesRateMax2.Text == "" || txt_SalesRateMax2.Text == "0")
                    {
                        MessageBox.Show("Must add Purchase/Sales rate...", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }
                }
                if (chk_OneUnitOnly.Checked == true)
                {
                    isOneUnitOnly = "True";
                    UnitMF = 0;
                    isunit2 = "False";
                    qty = Convert.ToInt32(txt_op.Text);
                }
                else
                {
                    isOneUnitOnly = "False";
                    isunit2 = "True";
                    if (Convert.ToInt32(txt_UnitMF.Text) == 0)
                    {
                        MessageBox.Show("Must add sales units", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_UnitMF.Focus();
                        return;
                    }
                    else
                        UnitMF = Convert.ToInt32(txt_UnitMF.Text);
                    if (txt_op.Text == "")
                    {
                        qty = 0;
                    }
                    else
                        qty = Convert.ToInt32(txt_op.Text) * UnitMF;
                    if (txt_PurchRate2.Text == "" || txt_SalesRate2.Text == "" || txt_SalesRateMin2.Text == "" || txt_SalesRateMax2.Text == "")
                    {
                        MessageBox.Show("Must add Sales units", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                expdate = Convert.ToDateTime(dtpExp.Value).ToString("yyyy-MM-dd");
                if (expdate != "")
                {
                    IsExpDate = "Yes";
                    expdate = Convert.ToDateTime(dtpExp.Value).ToString("yyyy-MM-dd");
                }
                else
                {
                    IsExpDate = "NO";
                    expdate = "";
                }
                if (Chk_HavebatchNo.Checked == true)
                {
                    _isbatch = "true";
                }
                else
                {
                    _isbatch = "false";
                }
                if (Chk_Taxable.Checked == true)
                {
                    _istax = "true";
                }
                else
                {
                    _istax = " false";
                }
                if (chk_OneUnitOnly.Checked == true)
                {
                    _sUnit = "null";
                }
                else
                {
                    _sUnit = cmb_unit2.Text;
                }
                string opening_date = "";
                if (Convert.ToInt32(txt_op.Text) > 0)
                {
                    opening_date = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    if (txt_batch.Text == "")
                    {
                        MessageBox.Show("Enter the Batch Number !.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_batch.Focus();
                        return;
                    }
                }
                else
                {
                    opening_date = "";
                }
                if (save.Text == "SAVE")
                {
                    if (itemcheck() == 1)
                    {
                        MessageBox.Show("The item code already exists !..", "Duplication encountered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    int i = 0;
                    if (chk_consume.Checked == true)
                    {
                        i = this.cntrl.Save_data(txt_ItemName.Text, txt_ItemCode.Text, Cmb_Manufacture.SelectedValue.ToString(), Cmb_Category.SelectedValue.ToString(), txt_HSN.Text, txt_Location.Text, txt_Packing.Text, _isbatch, txt_SalesRate.Text, txt_SalesRateMin.Text, txt_SalesRateMax.Text, txt_PurchaseRate.Text, cmb_Unit1.Text, _sUnit, UnitMF, txt_PurchRate2.Text, txt_SalesRate2.Text, txt_SalesRateMin2.Text, txt_SalesRateMax2.Text, isOneUnitOnly, txt_reorderStockQty.Text, txt_CostBase.Text, _istax, txt_minimumStockforSale.Text, txtShelfNo.Text, txtBarcode.Text, txt_op.Text, cmb_supplier.SelectedValue.ToString(), "Consumable", txtVAT.Text, opening_date);

                    }
                    else
                    {
                        i = this.cntrl.Save_data(txt_ItemName.Text, txt_ItemCode.Text, Cmb_Manufacture.SelectedValue.ToString(), Cmb_Category.SelectedValue.ToString(), txt_HSN.Text, txt_Location.Text, txt_Packing.Text, _isbatch, txt_SalesRate.Text, txt_SalesRateMin.Text, txt_SalesRateMax.Text, txt_PurchaseRate.Text, cmb_Unit1.Text, _sUnit, UnitMF, txt_PurchRate2.Text, txt_SalesRate2.Text, txt_SalesRateMin2.Text, txt_SalesRateMax2.Text, isOneUnitOnly, txt_reorderStockQty.Text, txt_CostBase.Text, _istax, txt_minimumStockforSale.Text, txtShelfNo.Text, txtBarcode.Text, txt_op.Text, cmb_supplier.SelectedValue.ToString(), "Pharmacy", txtVAT.Text, opening_date);

                    }
                    string dt = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    string dt_id = this.cntrl.get_max_itemid();
                    if (Convert.ToInt32(txt_op.Text) > 0)
                    {

                        if (chk_consume.Checked == true)
                        {
                            int a = this.cntrl.save_batchNumber(dt_id, txt_batch.Text, qty,Convert.ToDecimal( txt_PurchaseRate.Text), Convert.ToDecimal(txt_SalesRateMax.Text), "No", "No", UnitMF.ToString(), "0", DateTime.Now.Date.ToString("yyyy-MM-dd"), expdate, "", cmb_supplier.SelectedValue.ToString(), DateTime.Now.Date.ToString("yyyy-MM-dd"), IsExpDate, "Consumable");

                        }
                        else
                        {
                            int a = this.cntrl.save_batchNumber(dt_id, txt_batch.Text, qty, Convert.ToDecimal(txt_PurchaseRate.Text), Convert.ToDecimal(txt_SalesRateMax.Text), "No", "No", UnitMF.ToString(), "0", DateTime.Now.Date.ToString("yyyy-MM-dd"), expdate, "", cmb_supplier.SelectedValue.ToString(), DateTime.Now.Date.ToString("yyyy-MM-dd"), IsExpDate, "Pharmacy");

                        }

                    }
                    DateTime Timeonly = DateTime.Now;
                    this.cntrl.save_log(doctor_id, "Add Drug", "Add New Drugs", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Add", dt_id);
                    if (i > 0)
                    {
                        if (chkprescription.Checked == true)
                        {
                            string rs_item = this.cntrl.get_max_itemid();
                            string itemid = rs_item;
                            this.cntrl.savedrugtable(itemid, txt_ItemName.Text, cmbdrugtype.Text, txtstrength.Text, cmbstrength.Text, txtinstructions.Text);
                        }
                        MessageBox.Show("Item added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                    }
                }
                else
                {
                    int i = 0;
                    if (chk_consume.Checked == true)                                           
                    {
                        i = this.cntrl.update_data(Item_Id, txt_ItemName.Text, txt_ItemCode.Text, Cmb_Manufacture.SelectedValue.ToString(), Cmb_Category.SelectedValue.ToString(), txt_Location.Text, txt_Packing.Text, _isbatch, txt_SalesRate.Text, txt_SalesRateMin.Text, txt_SalesRateMax.Text, txt_PurchaseRate.Text, cmb_Unit1.Text, _sUnit, UnitMF, txt_PurchRate2.Text, txt_SalesRate2.Text, txt_SalesRateMin2.Text, txt_SalesRateMax2.Text, isOneUnitOnly, txt_reorderStockQty.Text, txt_CostBase.Text, _istax, txt_minimumStockforSale.Text, txtShelfNo.Text, txtBarcode.Text, txt_op.Text, cmb_supplier.SelectedValue.ToString(), "Consumable",txtVAT.Text, opening_date);

                    }
                    else
                    {
                        i = this.cntrl.update_data(Item_Id, txt_ItemName.Text, txt_ItemCode.Text, Cmb_Manufacture.SelectedValue.ToString(), Cmb_Category.SelectedValue.ToString(), txt_Location.Text, txt_Packing.Text, _isbatch, txt_SalesRate.Text, txt_SalesRateMin.Text, txt_SalesRateMax.Text, txt_PurchaseRate.Text, cmb_Unit1.Text, _sUnit, UnitMF, txt_PurchRate2.Text, txt_SalesRate2.Text, txt_SalesRateMin2.Text, txt_SalesRateMax2.Text, isOneUnitOnly, txt_reorderStockQty.Text, txt_CostBase.Text, _istax, txt_minimumStockforSale.Text, txtShelfNo.Text, txtBarcode.Text, txt_op.Text, cmb_supplier.SelectedValue.ToString(), "Pharmacy",txtVAT.Text, opening_date);
                    }

                    if (Convert.ToInt32(txt_op.Text) > 0)
                    {
                        DataTable dt_batch = this.cntrl.check_have_batch(Item_Id.ToString(),txt_batch.Text);
                        if(dt_batch.Rows.Count>0)
                        {
                            if (chk_consume.Checked == true)
                            {
                                int a = this.cntrl.update_batchNumber(Item_Id.ToString(), txt_batch.Text, qty, Convert.ToDecimal(txt_PurchaseRate.Text), Convert.ToDecimal(txt_SalesRateMax.Text), isunit2,"No", UnitMF.ToString(), "0", DateTime.Now.Date.ToString("yyyy-MM-dd"), expdate, "", cmb_supplier.SelectedValue.ToString(), DateTime.Now.Date.ToString("yyyy-MM-dd"), IsExpDate, batch_id, "Consumable");

                            }
                            else
                            {
                                int a = this.cntrl.update_batchNumber(Item_Id.ToString(), txt_batch.Text, qty, Convert.ToDecimal(txt_PurchaseRate.Text), Convert.ToDecimal(txt_SalesRateMax.Text), isunit2,"No", UnitMF.ToString(), "0", DateTime.Now.Date.ToString("yyyy-MM-dd"), expdate, "", cmb_supplier.SelectedValue.ToString(), DateTime.Now.Date.ToString("yyyy-MM-dd"), IsExpDate, batch_id, "Pharmacy");
                            }
                        }
                        else
                        {
                            if (chk_consume.Checked == true)
                            {
                                int a = this.cntrl.save_batchNumber(Item_Id.ToString(), txt_batch.Text, qty,Convert.ToDecimal(txt_PurchaseRate.Text), Convert.ToDecimal(txt_SalesRateMax.Text), isunit2,"No", UnitMF.ToString(), "0", DateTime.Now.Date.ToString("yyyy-MM-dd"), expdate, "", cmb_supplier.SelectedValue.ToString(), DateTime.Now.Date.ToString("yyyy-MM-dd"), IsExpDate, "Consumable");

                            }
                            else
                            {
                                int a = this.cntrl.save_batchNumber(Item_Id.ToString(), txt_batch.Text, qty, Convert.ToDecimal(txt_PurchaseRate.Text), Convert.ToDecimal(txt_SalesRateMax.Text), isunit2,"No", UnitMF.ToString(), "0", DateTime.Now.Date.ToString("yyyy-MM-dd"), expdate, "", cmb_supplier.SelectedValue.ToString(), DateTime.Now.Date.ToString("yyyy-MM-dd"), IsExpDate, "Pharmacy");

                            }

                        }
                    }
                    string dt = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    DateTime Timeonly = DateTime.Now;
                    this.cntrl.save_log(doctor_id, "Add Drug", " Edit Drugs", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Edit", Item_Id.ToString());
                    if (i > 0)
                    {
                        if (chkprescription.Checked == true)
                        {
                            string rs_item = this.cntrl.get_drugid(Item_Id.ToString());
                            if (rs_item != "0")
                            {
                                this.cntrl.update_drug(Item_Id.ToString(), txt_ItemName.Text, cmbdrugtype.Text, txtstrength.Text, cmbstrength.Text, txtinstructions.Text);
                            }
                            else
                            {
                                DataTable rs_additem = this.cntrl.select_id_drug(txt_ItemName.Text, cmbdrugtype.Text, txtstrength.Text, cmbstrength.Text, txtinstructions.Text);
                                if (rs_additem.Rows.Count > 0)
                                {
                                    this.cntrl.update(Item_Id.ToString(), rs_additem.Rows[0]["id"].ToString(), txt_ItemName.Text, cmbdrugtype.Text, txtstrength.Text, cmbstrength.Text, txtinstructions.Text);
                                }
                                else
                                {
                                    this.cntrl.savedrugtable(Item_Id.ToString(), txt_ItemName.Text, cmbdrugtype.Text, txtstrength.Text, cmbstrength.Text, txtinstructions.Text);
                                }
                            }
                        }
                        else
                        {
                            this.cntrl.update_inventryid(Item_Id.ToString());
                        }

                        MessageBox.Show("updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                Fill_combo();
                load_itemcode();
                if (chk_consume.Checked == true)
                {
                    chk_consume.Checked = true;
                }
                else
                    chk_consume.Checked = false;
            }
            else
            {
                if (txt_ItemCode.Text == "")
                {
                    txt_ItemCode.Focus();
                    MessageBox.Show("Enter the Item Code !", "Empty field occured", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (txt_ItemName.Text == "")
                {
                    txt_ItemName.Focus();
                    MessageBox.Show("Enter the  Item Name !", "Empty field occured", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Cmb_Manufacture.Items.Count == 0)
                {
                    MessageBox.Show("Please add Manufacture !", "Empty field occured", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (txtVAT.Text == "")
                {
                    MessageBox.Show("Please add Tax !", "Empty field occured", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            txtstrength.Text = "";
            txtinstructions.Text = "";
        }

        private void txt_ItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBarcode.Focus();
            }
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_ItemName.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txt_ItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Cmb_Category.DroppedDown = true;
                Cmb_Category.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void Cmb_Category_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Cmb_Manufacture.DroppedDown = true;
                Cmb_Manufacture.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void Cmb_Manufacture_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmb_supplier.DroppedDown = true;
                cmb_supplier.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void cmb_Unit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chk_OneUnitOnly.Focus();
                chk_OneUnitOnly.Checked = false;
                chk_OneUnitOnly.BackColor = Color.LightGray;
                e.SuppressKeyPress = true;
            }
        }

        private void chk_OneUnitOnly_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (chk_OneUnitOnly.Checked == true)
                {
                    txt_Packing.Focus();
                }
                else
                {
                    cmb_unit2.DroppedDown = true;
                    cmb_unit2.Focus();
                }
                e.SuppressKeyPress = true;
                chk_OneUnitOnly.BackColor = Color.White;
            }
        }

        private void cmb_unit2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_UnitMF.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txt_Packing_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtShelfNo.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtShelfNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_HSN.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txt_HSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Location.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txt_Location_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_PurchaseRate.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txt_PurchaseRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_SalesRateMax.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txt_UnitMF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Packing.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txt_SalesRateMax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (chk_OneUnitOnly.Checked == false)
                {
                    txt_PurchRate2.Focus();
                }
                else
                {
                    txt_op.Focus();
                }
                e.SuppressKeyPress = true;
            }
        }

        private void txt_PurchRate2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_SalesRateMax2.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txt_op_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumwithsinglepoint(sender, e);
        }

        private void cmb_supplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmb_Unit1.DroppedDown = true;
                cmb_Unit1.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txt_SalesRateMax2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_op.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txt_op_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_reorderStockQty.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txt_reorderStockQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_minimumStockforSale.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txt_minimumStockforSale_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_op.Text != "")
                {
                    txt_batch.Focus();
                    e.SuppressKeyPress = true;
                }
                else
                {
                    Chk_HavebatchNo.Focus();
                    Chk_HavebatchNo.Checked = true;
                    Chk_HavebatchNo.BackColor = Color.LightGray;
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void Chk_HavebatchNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Chk_Taxable.Focus();
                Chk_Taxable.Checked = false;
                Chk_Taxable.BackColor = Color.LightGray;
                Chk_HavebatchNo.BackColor = Color.White;
                e.SuppressKeyPress = true;
            }
        }

        private void Chk_Taxable_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkprescription.Focus();
                chkprescription.Checked = false;
                chkprescription.BackColor = Color.LightGray;
                Chk_Taxable.BackColor = Color.White;
                e.SuppressKeyPress = true;
            }
        }

        private void chkprescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (chkprescription.Checked == true)
                {
                    cmbdrugtype.DroppedDown = true;
                    cmbdrugtype.Focus();
                }
                else
                {
                    save.Select();
                }
                chkprescription.BackColor = Color.White;
                e.SuppressKeyPress = true;
            }
        }

        private void cmbdrugtype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtstrength.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtstrength_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbstrength.DroppedDown = true;
                cmbstrength.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void cmbstrength_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtinstructions.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtinstructions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                save.Select();
                e.SuppressKeyPress = true;
            }
        }
        public static string isunit2 = "False";
       
        public static string expdate = "";
        private void dtpExp_ValueChanged(object sender, EventArgs e)
        {
            expdate = dtpExp.Text.ToString();
        }

       

        private void dgvPurchaseBatch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

     
        private void txt_op_Leave(object sender, EventArgs e)
        {
            if (txt_op.Text != "" && txt_op.Text != "0")
            {
                lab_batch.Enabled = true;
                txt_batch.Enabled = true;
                lab_exp.Enabled = true;
                dtpExp.Enabled = true;
            }
            else
            {
                lab_batch.Enabled = false;
                txt_batch.Enabled = false;
                lab_exp.Enabled = false;
                dtpExp.Enabled = false;
            }
        }

        private void txtSalespertoPurchse_TextChanged(object sender, EventArgs e)
        {
            if (txtSalespertoPurchse.Text != "")
            {
                decimal value1 = 0, value2 = 0;
                if (!string.IsNullOrWhiteSpace(txtSalespertoPurchse.Text) && !string.IsNullOrWhiteSpace(txt_SalesRateMax.Text))
                {
                    value1 = (Convert.ToDecimal(txtSalespertoPurchse.Text) * Convert.ToDecimal(txt_SalesRateMax.Text)) / 100;
                    value2 = Convert.ToDecimal(txt_SalesRateMax.Text) - value1;
                    txt_PurchaseRate.Text = value2.ToString("##.00");
                }
                else
                {
                    txt_PurchaseRate.Text = "";
                }
            }
            if (txt_SalesRateMax.Text != ".")
            {
                decimal value1 = 0, value2 = 0;
                if (!string.IsNullOrWhiteSpace(txt_PurchaseRate.Text) && !string.IsNullOrWhiteSpace(txt_SalesRateMax.Text))
                {
                    value1 = Convert.ToDecimal(txt_SalesRateMax.Text) - Convert.ToDecimal(txt_PurchaseRate.Text);
                    if (txt_PurchaseRate.Text != ".00")
                    {
                        value2 = (value1 / Convert.ToDecimal(txt_PurchaseRate.Text)) * 100;
                    }
                    txt_unit1Retailmax2.Text = value2.ToString("##.00");
                }
                else
                {
                    txt_unit1Retailmax2.Text = "";
                }
            }
        }

        private void lbPurchRateFnder_Click(object sender, EventArgs e)
        {
            pnl_PurchRateFrmSales.Visible = true;
            pnltax.Visible = false;
        }

        private void txt_batch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpExp.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void dtpExp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Chk_HavebatchNo.Focus();
                Chk_HavebatchNo.Checked = true;
                Chk_HavebatchNo.BackColor = Color.LightGray;
                e.SuppressKeyPress = true;
            }
        }

       

        private void Chk_Taxable_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_Taxable.Checked == true)
            {
                lbVat.Visible = true;
                txtVAT.Visible = true;
            }
            else
            {
                lbVat.Visible = true;
                txtVAT.Visible = true;
                txtVAT.Text = "0";
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            btntypeadd.Visible = true;
            txt_newtype.Visible = true;
            linkLabel1.Visible= true;

        }

        private void btntypeadd_Click(object sender, EventArgs e)
        {
           if(txt_newtype.Text =="")
            {
                MessageBox.Show("please enter the type ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.cntrl.save(txt_newtype.Text);
                txt_newtype.Text = "";
                cmbdrugtype.DataSource = null;
               DataTable dt2 = this.cntrl.fill_drugtype();
                cmbdrugtype.DisplayMember = "dr_type";
                cmbdrugtype.ValueMember = "id";
                cmbdrugtype.DataSource = dt2;
                MessageBox.Show("Successfully saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            btntypeadd.Visible = false;
            txt_newtype.Visible = false;
            linkLabel1.Visible = false;
        }

        public void checkNull()
        {
            if (string.IsNullOrWhiteSpace(txt_op.Text))
            {
                txt_op.Text = "0";
            }
            if (string.IsNullOrWhiteSpace(txt_reorderStockQty.Text))
            {
                txt_reorderStockQty.Text = "0";
            }
            if (string.IsNullOrWhiteSpace(txt_minimumStockforSale.Text))
            {
                txt_minimumStockforSale.Text = "0";
            }
            if (string.IsNullOrWhiteSpace(txt_CostBase.Text))
            {
                txt_CostBase.Text = "0";
            }
            if (string.IsNullOrWhiteSpace(txt_SalesRateMin2.Text))
            {
                txt_SalesRateMin2.Text = "0";
            }
            if (string.IsNullOrWhiteSpace(txt_SalesRate2.Text))
            {
                txt_SalesRate2.Text = "0";
            }
            if (string.IsNullOrWhiteSpace(txt_SalesRateMax2.Text))
            {
                txt_SalesRateMax2.Text = "0";
            }
            if (string.IsNullOrWhiteSpace(txt_UnitMF.Text))
            {
                txt_UnitMF.Text = "0";
            }
            if (string.IsNullOrWhiteSpace(txt_PurchRate2.Text))
            {
                txt_PurchRate2.Text = "0";
            }
        }

        private void BTnClose_Click(object sender, EventArgs e)
        {
            pnltax.Visible = false;
            clear();
            save.Text = "SAVE";
            BTnClose.Visible = false;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            string formname = "ItemAdd";
            var form2 = new PappyjoeMVC.View.Item_List(formname);
            form2.Closed += (sender1, args) => this.Close();
            this.Hide();
        }

        private void txt_reorderStockQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public int itemcheck()
        {
            int affected = 0;
            DataTable dtb = this.cntrl.load_itemcode();
            if (dtb.Rows.Count > 0)
            {
                for (int i = 0; i < dtb.Rows.Count; i++)
                {
                    for (int j = 0; j < dtb.Columns.Count; j++)
                    {
                        if (dtb.Rows[i][j].ToString() != null && txt_ItemCode.Text == dtb.Rows[i][j].ToString())
                        {
                            affected = 1;
                        }
                    }
                }
            }
            return affected;
        }
    }
}
