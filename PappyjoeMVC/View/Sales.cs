using PappyjoeMVC.Controller;
using MySql.Data.MySqlClient;
using PappyjoeMVC.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PappyjoeMVC.View
{
    public partial class Sales : Form
    {
        sales_controller cntrl = new sales_controller();
        Printout_controller prnt_ctrl = new Printout_controller();
        string FormName = "";
        GeneralFunctions GF = new GeneralFunctions(); public static string Stock_From_List = "";
        public static string ItemCode_From_List = "";
        public static string ItemName_From_List = "";
        public bool salesOrder_flag = false; public bool salesListFlag = false,editflag=false;
        public static string itemId = "";
        bool flagSup = false;
        public decimal TotalStock;
        public int rowindex;
        public bool sales_Edit = false;
        decimal GST = 0, Cgst = 0, Sgst = 0;
        public static int invnum_Edit;
        string Payment_method = ""; private string p1;
        public static DataTable dtFor_CurrentStockUpdate_Bill = new DataTable();
        public static DataTable dtFor_CurrentStockUpdate;
        DataTable FrmBatchsale_edit = new DataTable();
        public static bool batch_flag = false; public string doctor_id = "1";
        public static string invnum_order; string path = ""; string logo_name = "";
        public static string type = "";public static  decimal totalbatchrate = 0;public static int batch_entry = 0;
        public Sales()
        {
            InitializeComponent();
        }
        public Sales(int invnum)
        {
            InitializeComponent();
            invnum_Edit = invnum;
            sales_Edit = true;
            salesListFlag = true;
        }
        public Sales(string invNumOrder, string p1)
        {
            InitializeComponent();
            invnum_order = invNumOrder;
            this.p1 = p1;
            salesOrder_flag = true;
        }
        public Sales(string item_code, string item_Name, string stock, string itemID)
        {
            InitializeComponent();
            ItemCode_From_List = item_code;
            ItemName_From_List = item_Name;
            Stock_From_List = stock;
            itemId = itemID;
        }
        public Sales(DataTable dtb_Sales)
        {
            InitializeComponent();
            dtFor_CurrentStockUpdate = dtb_Sales;
            batch_flag = true;
        }

        public Sales(DataTable dtb_Sales, decimal total_rate, int v) : this(dtb_Sales)
        {
            InitializeComponent();
            dtFor_CurrentStockUpdate = dtb_Sales;
            batch_flag = true;

            totalbatchrate = total_rate;
            batch_entry = v;
        }

        private void btn_item_Choose_Click(object sender, EventArgs e)
        {
            FormName = "Sales";
            cmb_Unit.Items.Clear();
            lst_Itemname.Visible = false;
            if (txt_ItemCode.Text != "")
            {
                ItemCode_From_List = "";
                   var form2 = new ItemListForSales(FormName, txt_ItemCode.Text);
                form2.ShowDialog();
                form2.Dispose();
                if (ItemCode_From_List != "")
                {
                    if (btn_AddtoGrid.Text == "Add")
                    {
                        txt_ItemCode.Text = ItemCode_From_List;
                        //if (itemcheck() == 0)
                        //{
                            txt_Discription.Text = ItemName_From_List;
                            DataTable dtb = this.cntrl.get_itemdetails(itemId);
                            Load_itemdetails(dtb);
                            cmb_Unit.Focus();
                        //}
                        //else
                        //{
                        //    txt_ItemCode.Text = "";
                        //    txt_ItemCode.Focus();
                        //}
                        lst_Itemname.Visible = false;
                    }
                }
            }
            else
            {
                ItemCode_From_List = "";
                    var form2 = new ItemListForSales(FormName, txt_ItemCode.Text);
                //form2.Item_Code = "";
                form2.ShowDialog();
                form2.Dispose();
                if (ItemCode_From_List != "")
                {
                    if (btn_AddtoGrid.Text == "Add")
                    {
                        txt_ItemCode.Text = ItemCode_From_List;
                        //if (itemcheck() == 0)
                        //{
                            txt_Discription.Text = ItemName_From_List;
                            DataTable dtb = this.cntrl.get_itemdetails(itemId);
                            Load_itemdetails(dtb);
                        //}
                        //else
                        //{
                        //    txt_ItemCode.Text = "";
                        //    txt_ItemCode.Focus();
                        //}
                        lst_Itemname.Visible = false;
                    }
                }
            }
        }

        public void fill_item_code()
        {
            if (btn_AddtoGrid.Text == "Add")
            {
                txt_ItemCode.Text = ItemCode_From_List;
                if (itemcheck() == 0)
                {
                    txt_Discription.Text = ItemName_From_List;
                    DataTable dtb = this.cntrl.get_itemdetails(itemId);
                    Load_itemdetails(dtb);
                }
                else
                {
                    txt_ItemCode.Text = "";
                    txt_ItemCode.Focus();
                }
            }
        }
        public int itemcheck()
        {
            int affected = 0;
            for (int i = 0; i < dgv_SalesItem.Rows.Count; i++)
            {
                //for (int j = 0; j < dgv_SalesItem.Columns.Count; j++)
                //{
                if (dgv_SalesItem.Rows[i].Cells["colItemCode"].Value != null && txt_ItemCode.Text == dgv_SalesItem.Rows[i].Cells["colItemCode"].Value.ToString() &&Convert.ToInt32( dgv_SalesItem.Rows[i].Cells["batchentry"].Value)== batch_entry)//&& cmb_Unit.Text== dgv_SalesItem.Rows[i].Cells["ColUnit"].Value.ToString()
                {
                    MessageBox.Show("There is a repetitive sale, check the row above ", "Duplication Encountered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    affected = 1;
                }
                //}
            }
            return affected;
        }
        //public int itemcheck_()
        //{
        //    int affected = 0;
        //    for (int i = 0; i < dgv_SalesItem.Rows.Count; i++)
        //    {
        //        //for (int j = 0; j < dgv_SalesItem.Columns.Count; j++)
        //        //{
        //        if (dgv_SalesItem.Rows[i].Cells["colItemCode"].Value != null && txt_ItemCode.Text == dgv_SalesItem.Rows[i].Cells["colItemCode"].Value.ToString() )
        //        {
        //            //MessageBox.Show("The ItemCode already existed ", "Duplication Encountered", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            affected = 1;
        //        }
        //        //}
        //    }
        //    return affected;
        //}
        public void Load_itemdetails(DataTable dtb)
        {
            string unit1 = "", unit2 = "";
            if (dtb.Rows.Count > 0)
            {
                txt_ItemCode.Text = dtb.Rows[0]["item_code"].ToString();
                //if (itemcheck() == 0) Barcode
                //{
                DataTable dt_gst = this.cntrl.get_pur_gst(itemId);
                txt_UnitCost.Text = Convert.ToDecimal(dtb.Rows[0]["Sales_Rate_Max"].ToString()).ToString("##.00");
                txt_Packing.Text = dtb.Rows[0]["Packing"].ToString();
                unit1 = dtb.Rows[0]["Unit1"].ToString();
                txt_IGST.Text = dt_gst.Rows[0]["IGST"].ToString();
                txtBarcode.Text = dtb.Rows[0]["Barcode"].ToString();
                txt_Discription.Text = dtb.Rows[0]["item_name"].ToString();
                if (dtb.Rows[0]["GstVat"].ToString() != "")
                    txt_GST.Text = dtb.Rows[0]["GstVat"].ToString();
                else
                    txt_GST.Text = "0";

                txt_Qty.Text = "1";
                cmb_Unit.Items.Clear();// cmb_unit2.Items.Clear();
                                       //if ( dtb.Rows[0]["OneUnitOnly"].ToString()=="True")
                                       //{
                if (unit1 != "")
                {
                    cmb_Unit.Items.Add(unit1);
                }
                if (dtb.Rows[0]["Unit2"].ToString() != "null" && dtb.Rows[0]["Unit2"].ToString() != "")
                {
                    unit2 = dtb.Rows[0]["Unit2"].ToString();
                    cmb_Unit.Items.Add(unit2);
                }
                if (cmb_Unit.Items.Count > 0)//error correction
                {
                    cmb_Unit.SelectedIndex = 0;
                }
            }
            else
                txt_UnitCost.Text = "0.00";
        }
        private void txtBdoctor_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtBdoctor.Text != "")
            {
                DataTable dtdr = this.cntrl.GetDoctorName(txtBdoctor.Text);
                lbIdoctor.DisplayMember = "doctor_name";
                lbIdoctor.ValueMember = "id";
                lbIdoctor.DataSource = dtdr;
                if (e.KeyCode == Keys.Enter && lbIdoctor.Items.Count > 0)
                {
                    var value = lbIdoctor.GetItemText(lbIdoctor.SelectedValue);
                    System.Data.DataTable supplier = this.cntrl.get_doctorname_by_id(value);
                    if (supplier.Rows.Count > 0)
                    {
                        txtBdoctor.Text = supplier.Rows[0]["doctor_name"].ToString();
                    }
                    lbIdoctor.Visible = false;
                    txtBdoctor.Focus();
                }
                else if (e.KeyCode == Keys.Down && lbIdoctor.Items.Count > 0)
                {
                    lbIdoctor.Focus();
                }
                else if (lbIdoctor.Items.Count > 0)
                {
                    lbIdoctor.Location = new Point(138, 100);
                    lbIdoctor.Visible = true;
                }
                else
                {
                    lbIdoctor.Visible = false;
                }
            }
            else
            {
                lbIdoctor.Visible = false;
            }
        }
        private void lbIdoctor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (lbIdoctor.SelectedItems.Count > 0)
                {
                    var value = lbIdoctor.SelectedValue.ToString();
                    System.Data.DataTable doctor = this.cntrl.get_doctorname_by_id(value);
                    if (doctor.Rows.Count > 0)
                    {
                        txtBdoctor.Text = doctor.Rows[0]["doctor_name"].ToString();
                        lbIdoctor.Visible = false;
                    }
                }
            }
        }
        private void lbIdoctor_MouseClick(object sender, MouseEventArgs e)
        {
            if (lbIdoctor.SelectedItems.Count > 0)
            {
                var value = lbIdoctor.SelectedValue.ToString();
                System.Data.DataTable doctor = this.cntrl.get_doctorname_by_id(value);
                if (doctor.Rows.Count > 0)
                {
                    txtBdoctor.Text = doctor.Rows[0]["doctor_name"].ToString();
                    lbIdoctor.Visible = false;
                }
            }
        }
        private void txtPatient_KeyUp(object sender, KeyEventArgs e)
        {
            if (flagSup == false)
            {
                DataTable dtb = this.cntrl.patient_keydown(txtPatient.Text);
                Fill_litbox(dtb);
                if (txtPatient.Text == "")
                {
                    lbPatient.Focus();
                    lbPatient.SelectedIndex = 0;
                }
                if (e.KeyCode == Keys.Down)
                {
                    if (lbPatient.Items.Count == 1)
                    {
                        lbPatient.SelectedIndex = 0;
                        lbPatient.Focus();
                    }
                    else if (lbPatient.Items.Count > 1)
                    {
                        lbPatient.SelectedIndex = 0;
                        lbPatient.Focus();
                    }
                }
            }
            flagSup = false;
        }
        private void lbPatient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                flagSup = true;
                string value = lbPatient.SelectedValue.ToString();
                DataTable dtb = this.cntrl.patients(value);
                fill_patientdetails(dtb);
                lbPatient.Hide();
            }
            else if (e.KeyCode == Keys.Up)
            {
                lbPatient.Focus();
                int indicee = lbPatient.SelectedIndex;
                indicee++;
            }
            else if (e.KeyCode == Keys.Down)
            {
                lbPatient.Focus();
                int indicee = lbPatient.SelectedIndex;
                indicee++;
            }
        }
        public void Fill_litbox(DataTable dtdr)
        {
            if (dtdr.Rows.Count > 0)
            {
                lbPatient.Show();
                lbPatient.Location = new Point(txtPatient.Location.X, 27);
                lbPatient.DisplayMember = "pt_name";
                lbPatient.ValueMember = "pt_id";
                lbPatient.DataSource = dtdr;
            }
            else
            {
                txtPatientID.Text = "0";
                lbPatient.Hide();
            }
        }
        public void fill_patientdetails(DataTable dtdr)
        {
            if (dtdr.Rows.Count > 0)
            {
                txtPatient.Text = dtdr.Rows[0]["pt_name"].ToString();
                txtPatientID.Text = dtdr.Rows[0]["pt_id"].ToString();
                txt_Street.Text = dtdr.Rows[0]["street_address"].ToString();
                txt_Locality.Text = dtdr.Rows[0]["locality"].ToString();
                txt_City.Text = dtdr.Rows[0]["city"].ToString();
                txt_PhoneNo.Text = dtdr.Rows[0]["primary_mobile_number"].ToString();
                lbPatient.Hide();
            }
            else
            {
                lbPatient.Hide();
            }
        }
        public string patient_id = "";
        private void lbPatient_MouseClick(object sender, MouseEventArgs e)
        {
            if (lbPatient.SelectedItems.Count > 0)
            {
                string value = lbPatient.SelectedValue.ToString();
                DataTable dtb = this.cntrl.patients(value);
                fill_patientdetails(dtb);
                lbPatient.Visible = false;
                patient_id = dtb.Rows[0]["id"].ToString();
                DataTable dtadvance = this.cntrl.Get_Advance(dtb.Rows[0]["id"].ToString());
                if (dtadvance.Rows.Count > 0)
                {
                    label23.Visible = true;
                    lblAdvance.Show(); //adv_refund.Visible = true;
                    lblAdvance.Text = Convert.ToDecimal(dtadvance.Rows[0][0].ToString()).ToString("##.00");
                              // decimal.Parse(dtadvance.Rows[0][0].ToString()).ToString("##.00");//string.Format("{0:C}",
                }
                else
                {
                    label23.Visible = true;
                    lblAdvance.Text = "0";// string.Format("{0:C}", 0);
                }
            }
        }
        private void cmb_Unit_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void cmb_Unit_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal unitcost = 0, unitMf = 0; decimal d;
            if (cmb_Unit.SelectedIndex >= 0)
            {
                if (btn_AddtoGrid.Text == "Update" && salesOrder_flag == false)
                {
                    txt_Qty.Focus();
                }
                DataTable dtb = this.cntrl.itemdetails(itemId);
                if (dtb.Rows.Count > 0)
                {
                    unitMf = Convert.ToDecimal(dtb.Rows[0]["UnitMF"].ToString());
                    if (cmb_Unit.Text == dtb.Rows[0]["Unit2"].ToString())
                    {
                        {
                            unitcost = Convert.ToDecimal(dtb.Rows[0]["Sales_Rate_Max"].ToString()) / unitMf;
                            txt_UnitCost.Text = unitcost.ToString("##.00");
                        }
                    }
                    else
                    {
                        txt_UnitCost.Text = dtb.Rows[0]["Sales_Rate_Max"].ToString();
                        if (decimal.TryParse(txt_UnitCost.Text, out d))
                        {
                            unitcost = Convert.ToDecimal(txt_UnitCost.Text);
                        }
                    }
                }
                //batchrate();
                TotalAmount_Calculation();
            }
        }

        public void batchrate()
        {
            DataTable dt_salesrate = new DataTable();
            decimal percnt_amt = 0, total_cost = 0, value1 = 0, value2 = 0, percentage = 0, unitMf = 0, cost = 0, unitcost = 0;
            if (cmb_Unit.SelectedIndex >= 0)
            {
                if (btn_AddtoGrid.Text == "Update" && salesOrder_flag == false)
                {
                    txt_Qty.Focus();
                }
                DataTable dtb = this.cntrl.itemdetails(itemId);
                DataTable dt_gst = this.cntrl.get_pur_gst(itemId);
                if (dtb.Rows.Count > 0)
                {
                    unitMf = Convert.ToDecimal(dtb.Rows[0]["UnitMF"].ToString());
                    if (cmb_Unit.Text == dtb.Rows[0]["Unit2"].ToString())
                    {
                        {
                            dt_salesrate = this.cntrl.get_item_salesrate_minimun(itemId);
                            unitcost = Convert.ToDecimal(dtb.Rows[0]["Sales_Rate_Max"].ToString()) / unitMf;
                            txt_UnitCost.Text = unitcost.ToString("##.00");
                        }
                    }
                    else
                    {
                        dt_salesrate = this.cntrl.get_item_salesrate(itemId);
                        //txt_UnitCost.Text = dtb.Rows[0]["Sales_Rate_Max"].ToString();
                        //if (decimal.TryParse(txt_UnitCost.Text, out d))
                        //{
                        //    unitcost = Convert.ToDecimal(txt_UnitCost.Text);
                        //}
                    }
                    value1 = Convert.ToDecimal(dt_salesrate.Rows[0][0].ToString()) - Convert.ToDecimal(dt_salesrate.Rows[0][1].ToString());
                    value2 = (value1 / Convert.ToDecimal(dt_salesrate.Rows[0][1].ToString())) * 100;
                    percentage = Convert.ToDecimal(value2.ToString("##.00"));

                    if (cmb_Unit.Text == dtb.Rows[0]["Unit1"].ToString())
                    {
                        percnt_amt = (Convert.ToDecimal(dt_gst.Rows[0]["rate"].ToString()) * percentage) / 100;
                        total_cost = percnt_amt + Convert.ToDecimal(dt_gst.Rows[0]["rate"].ToString());

                        txt_UnitCost.Text = total_cost.ToString("##.00");
                    }
                    else //if(cmb_Unit.Text == dtb.Rows[0]["Unit"].ToString())
                    {
                        if (cmb_Unit.Text == dtb.Rows[0]["Unit2"].ToString())
                        {
                            cost = Convert.ToDecimal(dt_gst.Rows[0]["rate"].ToString()) / unitMf;
                            txt_UnitCost.Text = percnt_amt.ToString("##.00");
                            //percnt_amt = (Convert.ToDecimal(dtb.Rows[0]["rate"].ToString()) * percentage) / 100;// percnt_amt = (Convert.ToDecimal(dt_salesrate.Rows[0][1].ToString()) * percentage) / 100;
                            //percnt_amt = (Convert.ToDecimal(dt_gst.Rows[0]["rate"].ToString()) * percentage) / 100;
                            total_cost = cost + percnt_amt;// percnt_amt + Convert.ToDecimal(dt_salesrate.Rows[0][1].ToString());
                            //total_cost = Convert.ToDecimal(dt_gst.Rows[0]["rate"].ToString()) + percnt_amt;
                            txt_UnitCost.Text = total_cost.ToString("##.00");
                        }
                        else
                        {
                            percnt_amt = (Convert.ToDecimal(dt_salesrate.Rows[0][1].ToString()) * percentage) / 100;
                            //percnt_amt = (Convert.ToDecimal(cost) * percentage) / 100;
                            total_cost = percnt_amt + Convert.ToDecimal(dt_salesrate.Rows[0][1].ToString());
                            txt_UnitCost.Text = total_cost.ToString("##.00");
                        }
                    }
                }

            }

        }
        public void TotalAmount_Calculation()
        {
            Decimal gst = 0, gst_Amount = 0, igst_Amount = 0, igst = 0, qty = 0, unitcost = 0, Amount = 0, TotalAmount = 0;
            decimal d; decimal disc = 0;
            //int qty = 0;
            //decimal gst = 0;
            decimal discount = 0;
            unitcost = Convert.ToDecimal(txt_UnitCost.Text);
            txt_Amount.Text = unitcost.ToString("##.00");
            if (decimal.TryParse(txt_GST.Text, out d))
            {
                gst = Convert.ToDecimal(txt_GST.Text);
            }
            if (decimal.TryParse(txt_IGST.Text, out d))
            {
                igst = Convert.ToDecimal(txt_IGST.Text);
            }
            if (decimal.TryParse(txt_Qty.Text, out d))
            {
                qty = Convert.ToDecimal(txt_Qty.Text);
            }
            if (Convert.ToDecimal(txt_GST.Text) > 0)
            {
                Amount = qty * unitcost;
                gst_Amount = (Amount * gst) / 100;
                TotalAmount = Amount + gst_Amount;
                txt_Amount.Text = TotalAmount.ToString("##.00");
            }
            else
            {
                Amount = qty * unitcost;
                txt_Amount.Text = Amount.ToString("##.00");
            }
            if (decimal.TryParse(txtdisc.Text, out d))
            {
                discount = Convert.ToDecimal(txtdisc.Text);
            }
            //else if (Convert.ToDecimal(txt_IGST.Text) > 0)
            //{
            //    Amount = qty * unitcost;
            //    igst_Amount = (Amount * igst) / 100;
            //    TotalAmount = Amount + igst_Amount;
            //    txt_Amount.Text = TotalAmount.ToString("##.00");
            //}
            //else
            //{
            //    TotalAmount = (qty * unitcost);
            //    txt_Amount.Text = TotalAmount.ToString("##.00");
            //}
            if (txtdisc.Text != "0")
            {
                if (TotalAmount > 0)
                {
                    disc = (TotalAmount - ((TotalAmount * discount) / 100));

                    //disc=((discount * gstamt) / 100);

                    txt_Amount.Text = disc.ToString("##.00");
                }
                else
                {

                    disc = (((Amount) * discount) / 100) + (Amount);
                    txt_Amount.Text = disc.ToString("##.00");
                }

            }
           
        }
        //public void TotalAmount_Calculation()
        //{
        //    Decimal gst = 0, gst_Amount = 0, igst_Amount = 0, igst = 0, qty = 0,  unitcost = 0, Amount = 0, TotalAmount = 0;
        //    decimal d; decimal disc = 0;
        //    //int qty = 0;
        //    //decimal gst = 0;
        //    decimal discount = 0;
        //    unitcost = Convert.ToDecimal(txt_UnitCost.Text);
        //    if (decimal.TryParse(txt_GST.Text, out d))
        //    {
        //        gst = Convert.ToDecimal(txt_GST.Text);
        //    }
        //    if (decimal.TryParse(txt_IGST.Text, out d))
        //    {
        //        igst = Convert.ToDecimal(txt_IGST.Text);
        //    }
        //    if (decimal.TryParse(txt_Qty.Text, out d))
        //    {
        //        qty = Convert.ToDecimal(txt_Qty.Text);
        //    }
        //    if (Convert.ToDecimal(txt_GST.Text) > 0)
        //    {
        //        Amount = qty * unitcost;
        //        gst_Amount = (Amount * gst) / 100;
        //        TotalAmount = Amount + gst_Amount;
        //        txt_Amount.Text = TotalAmount.ToString("##.00");
        //    }
        //    //else if (Convert.ToDecimal(txt_IGST.Text) > 0)
        //    //{
        //    //    Amount = qty * unitcost;
        //    //    igst_Amount = (Amount * igst) / 100;
        //    //    TotalAmount = Amount + igst_Amount;
        //    //    txt_Amount.Text = TotalAmount.ToString("##.00");
        //    //}
        //    else
        //    {
        //        TotalAmount = (qty * unitcost);
        //        txt_Amount.Text = TotalAmount.ToString("##.00");
        //    }
        //    if (txtdisc.Text != "0.0")
        //    {
        //        if (TotalAmount > 0)
        //        {
        //            disc = (TotalAmount - ((TotalAmount * discount) / 100));

        //            //disc=((discount * gstamt) / 100);

        //            txt_Amount.Text = disc.ToString("##.00");
        //        }
        //        else
        //        {

        //            disc = (((Amount) * discount) / 100) + (Amount);
        //            txt_Amount.Text = disc.ToString("##.00");
        //        }

        //    }
        //}
        private void txt_GST_Click(object sender, EventArgs e)
        {
            if (txt_GST.Text == "0")
            {
                txt_GST.Text = "";
            }
        }
        private void txt_GST_Leave(object sender, EventArgs e)
        {
            if (txt_GST.Text == "")
            {
                txt_GST.Text = "0";
            }
            else if (Convert.ToDecimal(txt_GST.Text) > 0)
            {
                txt_IGST.Text = "0";
            }
        }
        private void txt_GST_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumwithsinglepoint(sender, e);
            string a = txt_GST.Text;
            string b = a.TrimStart('0');
            txt_GST.Text = b;
        }
        private void txt_GST_KeyUp(object sender, KeyEventArgs e)
        {
            decimal d;
            if (decimal.TryParse(txt_GST.Text, out d))
            {
                TotalAmount_Calculation();
            }
            else
            {
                txt_GST.Text = "0";
                TotalAmount_Calculation();
            }
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
        private void txt_GST_TextChanged(object sender, EventArgs e)
        {
            if (txt_GST.Text != "" && txt_GST.Text != ".")
            {
                if (Convert.ToDecimal(txt_GST.Text) > 0)
                {
                    txt_IGST.Text = "0";
                    TotalAmount_Calculation();
                }
                else
                {
                    txt_GST.Text = "0";
                    TotalAmount_Calculation();
                }
            }
        }
        private void txt_IGST_Click(object sender, EventArgs e)
        {
            if (txt_IGST.Text == "0")
            {
                txt_IGST.Text = "";
            }
        }
        private void txt_IGST_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumwithsinglepoint(sender, e);
            string a = txt_IGST.Text;
            string b = a.TrimStart('0');
            txt_IGST.Text = b;
        }
        private void txt_IGST_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_IGST.Text))
            {
                TotalAmount_Calculation();
            }
        }
        private void txt_IGST_Leave(object sender, EventArgs e)
        {
            if (txt_IGST.Text == "")
            {
                txt_IGST.Text = "0";
            }
            else if (Convert.ToDecimal(txt_IGST.Text) > 0)
            {
                txt_GST.Text = "0";
            }
        }
        private void txt_IGST_TextChanged(object sender, EventArgs e)
        {
            if (txt_IGST.Text != "" && txt_IGST.Text != ".")
            {
                if (Convert.ToDecimal(txt_IGST.Text) > 0)
                {
                    txt_GST.Text = "0";
                }
                else
                {
                    txt_IGST.Text = "0";
                    TotalAmount_Calculation();
                }
            }
        }
        private void txt_Qty_Click(object sender, EventArgs e)
        {
            if (txt_Qty.Text == "0")
            {
                txt_Qty.Text = "";
            }
        }
        private void txt_Qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            string a = txt_Qty.Text;
            string b = a.TrimStart('0');
            txt_Qty.Text = b;
        }
        private void txt_Qty_KeyUp(object sender, KeyEventArgs e)
        {
            //if (!string.IsNullOrWhiteSpace(txt_ItemCode.Text) && !string.IsNullOrWhiteSpace(txt_Qty.Text))
            //{
            //    DataTable dtb_qty = this.cntrl.Get_stock(itemId);
            //    if (dtb_qty.Rows[0][0].ToString() != "")
            //    {
            //        if (itemcheck() == 1)
            //        {
            //            decimal qty = 0;
            //            DataTable dtb_default_qty = this.cntrl.get_sales_default_qty(itemId);
            //            if(dtb_default_qty.Rows.Count>0)
            //            {
            //                qty = Convert.ToDecimal(dtb_qty.Rows[0][0].ToString()) - Convert.ToDecimal(dtb_default_qty.Rows[0]["sales_default_qty"].ToString());
            //                if(qty < Convert.ToDecimal(txt_Qty.Text))
            //                {
            //                    MessageBox.Show("You do Not have enough Stock. Available Stock = " + dtb_qty.Rows[0][0].ToString(), "limited Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                    txt_Qty.Clear();

            //                }
            //            }
            //        }
            //        else
            //        {
            //            if (Convert.ToDecimal(dtb_qty.Rows[0][0].ToString()) < Convert.ToDecimal(txt_Qty.Text))
            //            {
            //                MessageBox.Show("You do Not have enough Stock. Available Stock = " + dtb_qty.Rows[0][0].ToString(), "limited Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                txt_Qty.Clear();
            //            }
            //        }
            //    }
            //}
            if (txt_UnitCost.Text != "")
            {
                TotalAmount_Calculation();
            }
        }
        
        private void txt_Qty_Leave(object sender, EventArgs e)
        {
            string a = txt_Qty.Text;
            string b = a.TrimStart('0');
            txt_Qty.Text = b;
        }
        private void txt_Free_Click(object sender, EventArgs e)
        {
            if (txt_Free.Text == "0")
            {
                txt_Free.Text = "";
            }
        }
        private void txt_Free_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            string a = txt_Free.Text;
            string b = a.TrimStart('0');
            txt_Free.Text = b;
        }
        private void txt_Free_Leave(object sender, EventArgs e)
        {
            if (txt_Free.Text == "")
            {
                txt_Free.Text = "0";
            }
            else
            {
                string a = txt_Free.Text;
                string b = a.TrimStart('0');
                txt_Free.Text = b;
            }
        }
        private void txt_UnitCost_Click(object sender, EventArgs e)
        {
            //if (txt_UnitCost.Text == "0.00")
            //{
            //    txt_UnitCost.Text = "";
            //}
        }
        private void txt_UnitCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            //onlynumwithsinglepoint(sender, e);
        }
        private void txt_UnitCost_KeyUp(object sender, KeyEventArgs e)
        {

            decimal d;
            if (decimal.TryParse(txt_UnitCost.Text, out d))
            {
                TotalAmount_Calculation();
            }
            else
            {
                txt_UnitCost.Text = "0";
                TotalAmount_Calculation();
            }
        }
        private void txt_UnitCost_Leave(object sender, EventArgs e)
        {
            //if (txt_UnitCost.Text == "")
            //{
            //    txt_UnitCost.Text = "0.00"; 
            //}
        }
        private void txt_Amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumwithsinglepoint(sender, e);
        }
        private void btn_AddtoGrid_Click(object sender, EventArgs e)
        {
            try
            {
                txt_Discount.Text = "0";txt_DiscAmount.Text = "0.00";
                Decimal TotalGst = 0;
                Decimal Total_Igst = 0; decimal Stock = 0,edit_stck=0;
                int Totalqty = 0; decimal qty = 0;
                Decimal TotalAmount = 0;
                
                Decimal TotalCost = 0; decimal gstAmount = 0; decimal igstAmount = 0;
                if (!string.IsNullOrWhiteSpace(txt_ItemCode.Text) && !string.IsNullOrWhiteSpace(txt_Discription.Text) && !string.IsNullOrWhiteSpace(txt_Qty.Text)&& cmb_Unit.Text!="")
                {
                    if (Convert.ToDecimal(txt_Qty.Text) > 0 && Convert.ToDecimal(txt_UnitCost.Text) > 0)
                    {
                       
                        string item_Code = itemId;
                        if (txt_Free.Text == "") { txt_Free.Text = "0"; }
                        if (Convert.ToInt32(txt_Free.Text) > 0)
                        {
                            qty = Convert.ToDecimal(txt_Qty.Text) + Convert.ToDecimal(txt_Free.Text);
                        }
                        else
                            qty = Convert.ToDecimal(txt_Qty.Text);
                        string unit = cmb_Unit.Text;
                        DataTable dt_unit1 = this.cntrl.get_item_unitmf(itemId);
                        DataTable Dt_updateStock = this.cntrl.Get_stock(itemId); 
                        decimal def_qty = 0;
                        if (Dt_updateStock.Rows[0][0].ToString() != "")//else
                        {
                            TotalStock = Convert.ToDecimal(Dt_updateStock.Rows[0][0].ToString());
                        }
                        if (dt_unit1.Rows.Count > 0)
                        {
                            decimal unitmf = Convert.ToDecimal(dt_unit1.Rows[0]["UnitMF"].ToString());
                            if (unitmf > 0)
                            {
                                if (dt_unit1.Rows[0]["Unit1"].ToString() == unit)
                                {
                                    Stock = qty * unitmf;
                                }
                                else
                                {
                                    Stock = qty;
                                }
                            }
                            else
                            {
                                Stock = qty;
                            }
                        }
                        if (btn_AddtoGrid.Text == "Add")
                        {
                           
                            if (Stock <= TotalStock)
                            {
                                var form2 = new Batch_Sale(item_Code, qty, unit);
                                form2.ShowDialog();
                                if(totalbatchrate != 0)
                                {
                                    txt_UnitCost.Text = totalbatchrate.ToString();
                                }
                                if (dtFor_CurrentStockUpdate != null)
                                { 
                                    if (dgv_SalesItem.Rows.Count > 0)
                                    {
                                        if (itemcheck() == 1)
                                        {
                                            return;
                                        }
                                    }
                                    Fiil_BatchSale_Grid();
                                    foreach(DataRow dr in dtFor_CurrentStockUpdate.Rows)
                                    {
                                        if (dr["ColQty"].ToString() != "" && dr["ColQty"].ToString() != "0")
                                        {
                                            dgv_SalesItem.Rows.Add(itemId, txt_ItemCode.Text, txt_Discription.Text, txt_Packing.Text, dr["colbatchNo"].ToString(), dr["colentryNo"].ToString(), dt_unit1.Rows[0]["HSN_Number"].ToString(), cmb_Unit.Text, txt_GST.Text, txt_IGST.Text, txt_Qty.Text, txt_Free.Text, txtdisc.Text, txt_UnitCost.Text, txt_Amount.Text, PappyjoeMVC.Properties.Resources.editicon, PappyjoeMVC.Properties.Resources.deleteicon);
                                        }
                                    }
                                   
                                    DataTable dt = this.cntrl.getShelfno(itemId);
                                    if (dt.Rows.Count > 0)
                                    {
                                        dgvShelfNo.Rows.Add(dt.Rows[0]["item_name"].ToString(), dt.Rows[0]["Shelf_No"].ToString());
                                    }
                                    clear_itemdetails();
                                }
                                else
                                {
                                    MessageBox.Show("Did not add batch!..", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Quantity is greater than the stock", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        else
                        {
                            editflag = false;
                            if (salesOrder_flag == true)
                            {
                                if (itemcheck_Batchgrid() == 0)
                                {
                                    if (Stock <= TotalStock)
                                    {
                                        var form2 = new Batch_Sale(item_Code, qty, unit);
                                        form2.ShowDialog();
                                        form2.Dispose();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Quantity is greater than the stock", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }
                                else
                                {
                                    FrmBatchsale_edit.Rows.Clear();
                                    FrmBatchsale_edit.Columns.Clear();
                                    createTempTable();
                                    if (FrmBatchsale_edit.Rows.Count > 0)
                                    {
                                        if (Stock <= TotalStock)
                                        {
                                            var form2 = new Batch_Sale(item_Code, qty, FrmBatchsale_edit, unit);
                                            form2.ShowDialog();
                                            form2.Dispose();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Quantity is greater than the stock", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }
                                    }
                                }// dr["colbatchNo"].ToString(), dr["colentryNo"].ToString(),
                                if (totalbatchrate != 0)
                                {
                                    txt_UnitCost.Text = totalbatchrate.ToString();
                                }
                                if (dtFor_CurrentStockUpdate != null)
                                {
                                    foreach(DataRow drr in dtFor_CurrentStockUpdate.Rows)
                                    {
                                        if (drr["ColQty"].ToString() != "" && drr["ColQty"].ToString() != "0")
                                        {
                                            dgv_SalesItem.Rows[rowindex].Cells["id"].Value = itemId;
                                            dgv_SalesItem.Rows[rowindex].Cells["colItemCode"].Value = txt_ItemCode.Text;
                                            dgv_SalesItem.Rows[rowindex].Cells["colDiscription"].Value = txt_Discription.Text;
                                            dgv_SalesItem.Rows[rowindex].Cells["ColPacking"].Value = txt_Packing.Text;
                                            dgv_SalesItem.Rows[rowindex].Cells["batch"].Value = drr["colbatchNo"].ToString();
                                            dgv_SalesItem.Rows[rowindex].Cells["batchentry"].Value = drr["colentryNo"].ToString();
                                            dgv_SalesItem.Rows[rowindex].Cells["ColUnit"].Value = cmb_Unit.Text;
                                            dgv_SalesItem.Rows[rowindex].Cells["ColGST"].Value = txt_GST.Text;
                                            dgv_SalesItem.Rows[rowindex].Cells["colIGST"].Value = txt_IGST.Text;
                                            dgv_SalesItem.Rows[rowindex].Cells["ColQty"].Value = txt_Qty.Text;
                                            dgv_SalesItem.Rows[rowindex].Cells["ColFree"].Value = txt_Free.Text;
                                            dgv_SalesItem.Rows[rowindex].Cells["c_disc"].Value = txtdisc.Text;
                                            dgv_SalesItem.Rows[rowindex].Cells["colUnitcost"].Value = txt_UnitCost.Text;
                                            dgv_SalesItem.Rows[rowindex].Cells["colAmount"].Value = txt_Amount.Text;
                                        }
                                            
                                    }
                                }
                                    
                                if (itemcheck_Batchgrid() == 0)
                                {
                                    if (dtFor_CurrentStockUpdate != null)
                                    {
                                        Fiil_BatchSale_Grid();
                                        clear_itemdetails();
                                        dtFor_CurrentStockUpdate = null;
                                        batch_flag = false;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Did not add batch!..", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    if (dtFor_CurrentStockUpdate != null)
                                    {
                                        if (dgv_SalesItem.Rows.Count == 1)
                                        {
                                            dgv_BatchSale.Rows.Clear();
                                            fill_batch_onerow();
                                        }
                                        else
                                            fill_batch_sales();// update_Grid();
                                        //fill_Updategrid();
                                        dtFor_CurrentStockUpdate = null; txt_ItemCode.Enabled = true;
                                        batch_flag = false;
                                    }
                                    clear_itemdetails();
                                }

                                btn_AddtoGrid.Text = "Add"; btn_cancel.Visible = false;
                                txt_Discription.Enabled = true;
                                btn_item_Choose.Enabled = true; txtBarcode.Enabled = true;
                            }
                            else
                            {
                                //if(prescrption_sale==true)
                                //{
                                //    if (Stock <= TotalStock)
                                //    {
                                //        var form2 = new Batch_Sale(item_Code, qty, unit);
                                //        form2.ShowDialog();
                                //        if (totalbatchrate != 0)
                                //        {
                                //            txt_UnitCost.Text = totalbatchrate.ToString();
                                //        }
                                //        if (dtFor_CurrentStockUpdate != null)
                                //        {
                                //            if (dgv_SalesItem.Rows.Count > 0)
                                //            {
                                //                if (itemcheck() == 1)
                                //                {
                                //                    return;
                                //                }
                                //            }
                                //            Fiil_BatchSale_Grid();
                                //            foreach (DataRow dr in dtFor_CurrentStockUpdate.Rows)
                                //            {
                                //                if (dr["ColQty"].ToString() != "" && dr["ColQty"].ToString() != "0")
                                //                {
                                //                    dgv_SalesItem.Rows.Add(itemId, txt_ItemCode.Text, txt_Discription.Text, txt_Packing.Text, dr["colbatchNo"].ToString(), dr["colentryNo"].ToString(), dt_unit1.Rows[0]["HSN_Number"].ToString(), cmb_Unit.Text, txt_GST.Text, txt_IGST.Text, txt_Qty.Text, txt_Free.Text, txtdisc.Text, txt_UnitCost.Text, txt_Amount.Text, PappyjoeMVC.Properties.Resources.editicon, PappyjoeMVC.Properties.Resources.deleteicon);
                                //                }
                                //            }

                                //            DataTable dt = this.cntrl.getShelfno(itemId);
                                //            if (dt.Rows.Count > 0)
                                //            {
                                //                dgvShelfNo.Rows.Add(dt.Rows[0]["item_name"].ToString(), dt.Rows[0]["Shelf_No"].ToString());
                                //            }
                                //            clear_itemdetails();
                                //        }
                                //        else
                                //        {
                                //            MessageBox.Show("Did not add batch!..", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //            return;
                                //        }
                                //    }
                                //}
                                //else
                                //{
                                    FrmBatchsale_edit.Rows.Clear();
                                    FrmBatchsale_edit.Columns.Clear();
                                    createTempTable();
                                    if (FrmBatchsale_edit.Rows.Count > 0)
                                    {

                                        if (Stock <= TotalStock)
                                        {
                                            if (FrmBatchsale_edit.Rows.Count == 1)
                                            {
                                                edit_stck = Stock;
                                            }
                                            else
                                            {
                                                edit_stck = qty;
                                            }
                                            var form2 = new Batch_Sale(item_Code, edit_stck, FrmBatchsale_edit, unit);
                                            form2.ShowDialog();
                                            form2.Dispose();
                                            if (rowindex >= 0)
                                            {
                                                dgv_SalesItem.Rows[rowindex].Cells["id"].Value = itemId;
                                                dgv_SalesItem.Rows[rowindex].Cells["colItemCode"].Value = txt_ItemCode.Text;
                                                dgv_SalesItem.Rows[rowindex].Cells["colDiscription"].Value = txt_Discription.Text;
                                                dgv_SalesItem.Rows[rowindex].Cells["ColPacking"].Value = txt_Packing.Text;
                                                dgv_SalesItem.Rows[rowindex].Cells["ColUnit"].Value = cmb_Unit.Text;
                                                dgv_SalesItem.Rows[rowindex].Cells["ColGST"].Value = txt_GST.Text;
                                                dgv_SalesItem.Rows[rowindex].Cells["colIGST"].Value = txt_IGST.Text;
                                                dgv_SalesItem.Rows[rowindex].Cells["ColQty"].Value = txt_Qty.Text;
                                                dgv_SalesItem.Rows[rowindex].Cells["ColFree"].Value = txt_Free.Text;
                                                dgv_SalesItem.Rows[rowindex].Cells["c_disc"].Value = txtdisc.Text;
                                                dgv_SalesItem.Rows[rowindex].Cells["colUnitcost"].Value = txt_UnitCost.Text;
                                                dgv_SalesItem.Rows[rowindex].Cells["colAmount"].Value = txt_Amount.Text;
                                                if (dtFor_CurrentStockUpdate != null)
                                                {
                                                    if (dgv_SalesItem.Rows.Count == 1)
                                                    {
                                                        dgv_BatchSale.Rows.Clear();
                                                        fill_batch_onerow();
                                                    }
                                                    else
                                                        fill_batch_sales();// update_Grid();
                                                                           //fill_Updategrid();
                                                    clear_itemdetails(); txt_ItemCode.Enabled = true;
                                                    btn_AddtoGrid.Text = "Add"; btn_cancel.Visible = false;
                                                    dtFor_CurrentStockUpdate = null; txt_Discription.Enabled = true;
                                                    btn_item_Choose.Enabled = true; txtBarcode.Enabled = true;
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Did not add batch!..", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Quantity is greater than the stock", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }
                                    }
                                //}
                                
                            }
                        }
                        foreach (DataGridViewRow dr in dgv_SalesItem.Rows)
                        {
                            if (dr.Cells["ColGST"].Value != null && dr.Cells["ColGST"].Value.ToString() != "")
                            {
                                gstAmount = ((Convert.ToDecimal(dr.Cells["ColQty"].Value.ToString()) * Convert.ToDecimal(dr.Cells["colUnitcost"].Value.ToString())) * Convert.ToDecimal(dr.Cells["ColGST"].Value.ToString())) / 100;
                                TotalGst = TotalGst + gstAmount;
                            }
                            if (dr.Cells["colIGST"].Value != null && dr.Cells["colIGST"].Value.ToString() != "")
                            {
                                igstAmount = ((Convert.ToDecimal(dr.Cells["ColQty"].Value.ToString()) * Convert.ToDecimal(dr.Cells["colUnitcost"].Value.ToString())) * Convert.ToDecimal(dr.Cells["colIGST"].Value.ToString())) / 100;
                                Total_Igst = Total_Igst + igstAmount;
                            }
                            if (dr.Cells["colAmount"].Value != null && dr.Cells["colAmount"].Value.ToString() != "")
                            {
                                TotalAmount = TotalAmount + Convert.ToDecimal(dr.Cells["colAmount"].Value.ToString());
                            }
                            if (dr.Cells["colUnitcost"].Value != null && dr.Cells["colUnitcost"].Value.ToString() != "")
                            {
                                TotalCost = TotalCost + (Convert.ToDecimal(dr.Cells["ColQty"].Value.ToString()) * Convert.ToDecimal(dr.Cells["colUnitcost"].Value.ToString()));
                            }
                        }
                        Totalqty = dgv_SalesItem.Rows.Count; batch_entry = 0;
                        if (Totalqty > 0)
                        {
                            txt_totalItems.Text = Totalqty.ToString();
                        }
                        if (TotalGst > 0)
                        {
                            decimal cgst = TotalGst / 2;
                            txt_SGST.Text = Convert.ToDecimal(cgst).ToString("##0.00");
                            txt_CGST.Text = Convert.ToDecimal(cgst).ToString("##0.00");
                        }
                        if (Total_Igst > 0)
                        {
                            Txt_TotalIGST.Text = Convert.ToDecimal(Total_Igst).ToString("##0.00");
                        }
                        if (TotalAmount > 0)
                        {
                            Txt_TotalAmount.Text = Convert.ToDecimal(TotalAmount).ToString("##0.00");
                            txt_GrandTotal.Text = Convert.ToDecimal(TotalAmount).ToString("##0.00");// String.Format("{0:C}", Convert.ToDecimal(TotalAmount));
//  
                        }
                        if (TotalCost > 0)
                        {
                            txt_TotalCost.Text = Convert.ToDecimal(TotalCost).ToString("##0.00");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mandatory fields should not be empty.", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_ItemCode.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Mandatory fields should not be empty.", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                txtBarcode.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public int itemcheck_Batchgrid()
        {
            int affected = 0;//&& Convert.ToInt32(dgv_SalesItem.Rows[i].Cells["batchentry"].Value) == batch_entry  txt_ItemCode.Text
            for (int i = 0; i < dgv_BatchSale.Rows.Count; i++)
            {
                if (dgv_BatchSale.Rows[i].Cells["coiltem_code"].Value != null && itemId  == dgv_BatchSale.Rows[i].Cells["coiltem_code"].Value.ToString() )
                {
                    affected = 1;
                }
            }
            return affected;
        }
        public void createTempTable()
        {
            foreach (DataGridViewColumn col in dgv_BatchSale.Columns)
            {
                FrmBatchsale_edit.Columns.Add(col.Name);
            }
            foreach (DataGridViewRow row in dgv_BatchSale.Rows)
            {//                                if (row.Cells["tempItem_code"].Value.ToString() == Item_id && row.Cells["Item_unit"].Value.ToString()==cmbUnit.Text)

                DataRow dRow = FrmBatchsale_edit.NewRow();
                if (row.Cells["coiltem_code"].Value.ToString() == itemId && row.Cells["unit"].Value.ToString() == cmb_Unit.Text && row.Cells["colBatchEntry"].Value.ToString()== bath_entry)//
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dRow[cell.ColumnIndex] = cell.Value;
                    }
                    FrmBatchsale_edit.Rows.Add(dRow);
                }
            }
        }
        string bath_entry = "";
        public void fill_batch_onerow()
        {
            dgv_BatchSale.Rows.Add(txtDocumentNumber.Text, dtpDocumentDate.Value.ToShortDateString(), itemId, dtFor_CurrentStockUpdate.Rows[0]["colbatchNo"].ToString(), dtFor_CurrentStockUpdate.Rows[0]["ColQty"].ToString(), dtFor_CurrentStockUpdate.Rows[0]["rate"].ToString(), dtFor_CurrentStockUpdate.Rows[0]["ColStock"].ToString(),"", txt_Qty.Text, dtFor_CurrentStockUpdate.Rows[0]["colentryNo"].ToString(),  dtFor_CurrentStockUpdate.Rows[0]["ColPrd_Date"].ToString(), dtFor_CurrentStockUpdate.Rows[0]["colExpDate"].ToString(),  dtFor_CurrentStockUpdate.Rows[0]["clUnit"].ToString());
            if (dtFor_CurrentStockUpdate.Rows[0]["colCurrentStock"].ToString() != "")
            {
                dgv_BatchSale.Rows[0].Cells["currentStock"].Value = dtFor_CurrentStockUpdate.Rows[0]["colCurrentStock"].ToString();
            }
            else
                dgv_BatchSale.Rows[0].Cells["currentStock"].Value = dtFor_CurrentStockUpdate.Rows[0]["ColStock"].ToString();
        }
        public void fill_batch_sales()
        {
            for (int j = 0; j < dgv_BatchSale.Rows.Count; j++)
            {
                if (dgv_BatchSale.Rows[j].Cells["colBatchEntry"].Value.ToString() == dtFor_CurrentStockUpdate.Rows[0]["colentryNo"].ToString())
                {
                    dgv_BatchSale.Rows[j].Cells["ColinvNum"].Value = txtDocumentNumber.Text;
                    dgv_BatchSale.Rows[j].Cells["ColInvDate"].Value = dtpDocumentDate.Value.ToShortDateString();
                    dgv_BatchSale.Rows[j].Cells["coiltem_code"].Value = itemId;
                    dgv_BatchSale.Rows[j].Cells["colQuantity"].Value = dtFor_CurrentStockUpdate.Rows[0]["ColQty"].ToString();
                    dgv_BatchSale.Rows[j].Cells["rate"].Value = dtFor_CurrentStockUpdate.Rows[0]["rate"].ToString();
                    dgv_BatchSale.Rows[j].Cells["colBatchnumber"].Value = dtFor_CurrentStockUpdate.Rows[0]["colbatchNo"].ToString();
                    dgv_BatchSale.Rows[j].Cells["colBatchEntry"].Value = dtFor_CurrentStockUpdate.Rows[0]["colentryNo"].ToString();
                    dgv_BatchSale.Rows[j].Cells["colStock"].Value = dtFor_CurrentStockUpdate.Rows[0]["ColStock"].ToString();
                    dgv_BatchSale.Rows[j].Cells["colIsExp"].Value = txt_Qty.Text;
                    dgv_BatchSale.Rows[j].Cells["prddate"].Value = dtFor_CurrentStockUpdate.Rows[0]["ColPrd_Date"].ToString();
                    dgv_BatchSale.Rows[j].Cells["expdate"].Value = dtFor_CurrentStockUpdate.Rows[0]["colExpDate"].ToString();
                    dgv_BatchSale.Rows[j].Cells["colQuantity"].Value = dtFor_CurrentStockUpdate.Rows[0]["ColQty"].ToString();
                    dgv_BatchSale.Rows[j].Cells["unit"].Value = dtFor_CurrentStockUpdate.Rows[0]["clUnit"].ToString();
                    if (dtFor_CurrentStockUpdate.Rows[0]["colCurrentStock"].ToString() != "")
                    {
                        dgv_BatchSale.Rows[j].Cells["currentStock"].Value = dtFor_CurrentStockUpdate.Rows[0]["colCurrentStock"].ToString();
                    }
                    else
                        dgv_BatchSale.Rows[j].Cells["currentStock"].Value = dtFor_CurrentStockUpdate.Rows[0]["ColStock"].ToString();
                }
            }

        }
        public void fill_Updategrid()
        {
            int rowindex = dgv_BatchSale.Rows.Count;
            for (int j = 0; j < dtFor_CurrentStockUpdate.Rows.Count; j++)
            {
                if (dtFor_CurrentStockUpdate.Rows[j]["ColQty"].ToString() != "")
                {
                    dgv_BatchSale.Rows.Add();
                    dgv_BatchSale.Rows[rowindex].Cells["ColinvNum"].Value = txtDocumentNumber.Text;
                    dgv_BatchSale.Rows[rowindex].Cells["ColInvDate"].Value = dtpDocumentDate.Value.ToShortDateString();
                    dgv_BatchSale.Rows[rowindex].Cells["coiltem_code"].Value = itemId;
                    dgv_BatchSale.Rows[rowindex].Cells["colQuantity"].Value = dtFor_CurrentStockUpdate.Rows[j]["ColQty"].ToString();
                    dgv_BatchSale.Rows[rowindex].Cells["rate"].Value = dtFor_CurrentStockUpdate.Rows[j]["rate"].ToString();
                    dgv_BatchSale.Rows[rowindex].Cells["colBatchnumber"].Value = dtFor_CurrentStockUpdate.Rows[j]["colbatchNo"].ToString();
                    dgv_BatchSale.Rows[rowindex].Cells["colBatchEntry"].Value = dtFor_CurrentStockUpdate.Rows[j]["colentryNo"].ToString();
                    dgv_BatchSale.Rows[rowindex].Cells["colStock"].Value = dtFor_CurrentStockUpdate.Rows[j]["ColStock"].ToString();
                    dgv_BatchSale.Rows[rowindex].Cells["colIsExp"].Value = txt_Qty.Text;
                    dgv_BatchSale.Rows[rowindex].Cells["prddate"].Value = dtFor_CurrentStockUpdate.Rows[j]["ColPrd_Date"].ToString();
                    dgv_BatchSale.Rows[rowindex].Cells["expdate"].Value = dtFor_CurrentStockUpdate.Rows[j]["colExpDate"].ToString();
                    dgv_BatchSale.Rows[rowindex].Cells["colQuantity"].Value = dtFor_CurrentStockUpdate.Rows[j]["ColQty"].ToString();
                    dgv_BatchSale.Rows[rowindex].Cells["unit"].Value = dtFor_CurrentStockUpdate.Rows[j]["clUnit"].ToString();
                    if (dtFor_CurrentStockUpdate.Rows[j]["colCurrentStock"].ToString() != "")
                    {
                        dgv_BatchSale.Rows[rowindex].Cells["currentStock"].Value = dtFor_CurrentStockUpdate.Rows[j]["colCurrentStock"].ToString();
                    }
                    else
                        dgv_BatchSale.Rows[rowindex].Cells["currentStock"].Value = dtFor_CurrentStockUpdate.Rows[j]["ColStock"].ToString();
                    rowindex++;

                }
            }
        }
        public void update_Grid()//update casae, create table for remaining items in the batch sale grid except the updated item 
        {
            DataTable dt_Update = new DataTable();
            dt_Update.Columns.Clear();
            dt_Update.Rows.Clear();
            dt_Update.Columns.Add("invNo");
            dt_Update.Columns.Add("invDate");
            dt_Update.Columns.Add("ItemCode");
            dt_Update.Columns.Add("Batchno");
            dt_Update.Columns.Add("qty");
            dt_Update.Columns.Add("Cost");
            dt_Update.Columns.Add("Stock");
            dt_Update.Columns.Add("CurrentStock");
            dt_Update.Columns.Add("IsexpDate");
            dt_Update.Columns.Add("BatchEntry");
            dt_Update.Columns.Add("Prd.date");
            dt_Update.Columns.Add("Exp.date");
            dt_Update.Columns.Add("unit");
            if (dt_Update.Columns.Count > 0)
            {
                for (int i = 0; i < dgv_BatchSale.Rows.Count; i++)
                {
                    if (dgv_BatchSale.Rows[i].Cells["coiltem_code"].Value.ToString() != itemId)
                    {
                        dt_Update.Rows.Add(dgv_BatchSale.Rows[i].Cells["ColinvNum"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["ColInvDate"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["coiltem_code"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colBatchnumber"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colQuantity"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["rate"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colStock"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["currentStock"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colIsExp"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colBatchEntry"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["prddate"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["expdate"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["unit"].Value.ToString());
                    }
                    else if (dgv_BatchSale.Rows[i].Cells["coiltem_code"].Value.ToString() == itemId)
                    {
                        if (dgv_BatchSale.Rows[i].Cells["unit"].Value.ToString() != cmb_Unit.Text)
                        {
                            dt_Update.Rows.Add(dgv_BatchSale.Rows[i].Cells["ColinvNum"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["ColInvDate"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["coiltem_code"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colBatchnumber"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colQuantity"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["rate"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colStock"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["currentStock"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colIsExp"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colBatchEntry"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["prddate"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["expdate"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["unit"].Value.ToString());
                        }
                    }
                }
            }
            if (dt_Update.Rows.Count > 0)//refill the batch sale grid only the remaining items in the table except the updated item 
            {
                dgv_BatchSale.Rows.Clear();
                foreach (DataRow dr in dt_Update.Rows)
                {
                    dgv_BatchSale.Rows.Add(dr["invNo"].ToString(), dr["invDate"].ToString(), dr["ItemCode"].ToString(), dr["Batchno"].ToString(), dr["qty"].ToString(), dr["rate"].ToString(), dr["Stock"].ToString(), dr["CurrentStock"].ToString(), dr["IsexpDate"].ToString(), dr["BatchEntry"].ToString(), dr["Prd.date"].ToString(), dr["Exp.date"].ToString(), dr["unit"].ToString());
                }
            }
        }
        public void Fiil_BatchSale_Grid()// fill the update item to the batch sale grid
        {
            int row = dgv_BatchSale.Rows.Count; string batchno = "", unit = "";
            if (dtFor_CurrentStockUpdate.Rows.Count > 0)
            {
                var myList = new List<decimal>();
                decimal max = decimal.MinValue;
                foreach (DataRow dr in dtFor_CurrentStockUpdate.Rows)
                {
                    if (dr["ColQty"].ToString() != "" && dr["ColQty"].ToString() != "0")
                    {

                        dgv_BatchSale.Rows.Add();
                        dgv_BatchSale.Rows[row].Cells["ColinvNum"].Value = txtDocumentNumber.Text;
                        dgv_BatchSale.Rows[row].Cells["ColInvDate"].Value = dtpDocumentDate.Value.ToShortDateString();
                        dgv_BatchSale.Rows[row].Cells["coiltem_code"].Value = itemId;
                        dgv_BatchSale.Rows[row].Cells["colBatchnumber"].Value = dr["colbatchNo"].ToString();
                        dgv_BatchSale.Rows[row].Cells["colQuantity"].Value = dr["ColQty"].ToString();
                        dgv_BatchSale.Rows[row].Cells["rate"].Value = dr["rate"].ToString();
                        dgv_BatchSale.Rows[row].Cells["colStock"].Value = dr["ColStock"].ToString();
                        dgv_BatchSale.Rows[row].Cells["colBatchEntry"].Value = dr["colentryNo"].ToString();
                        dgv_BatchSale.Rows[row].Cells["colIsExp"].Value = txt_Qty.Text;
                        dgv_BatchSale.Rows[row].Cells["prddate"].Value = dr["ColPrd_Date"].ToString();
                        dgv_BatchSale.Rows[row].Cells["expdate"].Value =dr["colExpDate"].ToString(); //
                        dgv_BatchSale.Rows[row].Cells["unit"].Value = dr["clUnit"].ToString();

                        if (dr["colCurrentStock"].ToString() != "")
                        {
                            dgv_BatchSale.Rows[row].Cells["currentStock"].Value = dr["colCurrentStock"].ToString();
                        }
                        else
                            dgv_BatchSale.Rows[row].Cells["currentStock"].Value = dr["ColStock"].ToString();
                        //decimal rate = Convert.ToDecimal(dr["rate"].ToString());
                        //myList.Add(rate);

                        //int highestNumber = dtFor_CurrentStockUpdate.AsEnumerable().Max(x => int.Parse(x.Field<string>("rate")));
                        //}

                        //decimal de_sales_qty = 0;
                        //DataTable dt_sales_qty = this.cntrl.check_sales_defaut_qty(dr["colentryNo"].ToString());
                        //if(dt_sales_qty.Rows[0][0].ToString()!="0")
                        //{
                        //    de_sales_qty = Convert.ToDecimal(dt_sales_qty.Rows[0][0].ToString()) + Convert.ToDecimal(dr["ColQty"].ToString());
                        //    this.cntrl.save_default_qty(de_sales_qty.ToString(), dr["colentryNo"].ToString());

                        //}
                        //else
                        //{
                        //    this.cntrl.save_default_qty(dr["ColQty"].ToString(), dr["colentryNo"].ToString());

                        //}
                        //foreach (var type in myList)
                        //{
                        //    if (type > max)
                        //    {
                        //        max = type;
                        //        batchno = dr["colbatchNo"].ToString();
                        //        unit = dr["clUnit"].ToString();
                        //    }
                        //}
                        //decimal maxvalue = max;

                        row++;
                    }

                }
                TotalAmount_Calculation();
                ////
                //DataTable dtb1 = this.cntrl.itemdetails(itemId);
                //DataTable dt_salesrate = new DataTable();// this.cntrl.get_item_salesrate(itemId);
                //DataTable dtb = this.cntrl.batchrate(itemId, batchno, unit);
                ////batch wise selling rate
                ////if(dgv_BatchSale.Rows.Count==1)
                ////{
                //if (dtb.Rows.Count > 0)
                //{
                //    if (Convert.ToDecimal(max) > 0)//dtb.Rows[0]["rate"].ToString()
                //    {
                //        decimal percnt_amt = 0, total_cost = 0, value1 = 0, value2 = 0, percentage = 0, unitMf = 0, cost = 0;
                //        //     unitMf = Convert.ToDecimal(dtb1.Rows[0]["UnitMF"].ToString());
                //        //     if (cmb_Unit.Text == dtb1.Rows[0]["Unit2"].ToString())
                //        //     {
                //        //         dt_salesrate = this.cntrl.get_item_salesrate_minimun(itemId);
                //        //     }
                //        //     else
                //        //     {
                //        //         dt_salesrate = this.cntrl.get_item_salesrate(itemId);
                //        //     }
                //        //     value1 = Convert.ToDecimal(dt_salesrate.Rows[0][0].ToString()) - Convert.ToDecimal(dt_salesrate.Rows[0][1].ToString());
                //        //     value2 = (value1 / Convert.ToDecimal(dt_salesrate.Rows[0][1].ToString())) * 100;
                //        //     percentage = Convert.ToDecimal(value2.ToString("##.00"));
                //        //     if (cmb_Unit.Text == dtb.Rows[0]["Unit"].ToString())
                //        //     {
                //        //         percnt_amt = (Convert.ToDecimal(max) * percentage) / 100;
                //        //         total_cost = percnt_amt + Convert.ToDecimal(max);
                //        //     }
                //        //     else //if(cmb_Unit.Text == dtb.Rows[0]["Unit"].ToString())
                //        //     {
                //        //         if (cmb_Unit.Text == dtb1.Rows[0]["Unit2"].ToString())
                //        //         {
                //        //             cost = Convert.ToDecimal(max) / unitMf;
                //        //             //txt_UnitCost.Text = percnt_amt.ToString("##.00");

                //        //             //percnt_amt = (Convert.ToDecimal(dtb.Rows[0]["rate"].ToString()) * percentage) / 100;// percnt_amt = (Convert.ToDecimal(dt_salesrate.Rows[0][1].ToString()) * percentage) / 100;
                //        //             percnt_amt = (Convert.ToDecimal(cost) * percentage) / 100;
                //        //             total_cost = cost + percnt_amt;// percnt_amt + Convert.ToDecimal(dt_salesrate.Rows[0][1].ToString());
                //        //         }
                //        //         else
                //        //         {
                //        //             percnt_amt = (Convert.ToDecimal(dt_salesrate.Rows[0][1].ToString()) * percentage) / 100;
                //        //             //percnt_amt = (Convert.ToDecimal(cost) * percentage) / 100;
                //        //             total_cost = percnt_amt + Convert.ToDecimal(dt_salesrate.Rows[0][1].ToString());
                //        //         }
                //        //     }
                //        ////     if (Convert.ToDecimal(txt_UnitCost.Text) == total_cost)
                //        ////     {

                //        //     }
                //        //     else
                //        //     {
                //        //         DialogResult res = MessageBox.Show("Do you want to change the cost ?", "Cost Changed",
                //        //MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //        //         if (res == DialogResult.Yes)
                //        //         {


                //        //txt_UnitCost.Text = max.ToString("##.00");//put last batch rate as unit cost here
                        


                //        //}
                //        //else
                //        //{
                //        //    //txt_UnitCost.Text = total_cost.ToString("##.00");
                //        //}
                //        //}
                //    }
                //}
            }
        }
        public void clear_itemdetails()
        {
            txt_ItemCode.Text = "";
            txt_Discription.Text = "";
            txt_Packing.Text = "";
            txt_GST.Text = "0";
            txt_IGST.Text = "0";
            txt_Qty.Text = "0";
            txt_Free.Text = "0";
            txt_UnitCost.Text = "0.00";
            txt_Amount.Text = "0.00";
            cmb_Unit.Items.Clear();
            cmb_Unit.Text = "";
            txtBarcode.Text = "";
            lst_Itemname.Hide();txtdisc.Text = "0";
            //prescrption_sale = false;
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            clear_itemdetails();
            btn_AddtoGrid.Text = "Add";
            btn_cancel.Visible = false; 
            txt_Discription.Enabled = true;
            txt_ItemCode.Enabled = true;
            txtBarcode.Enabled = true; btn_item_Choose.Enabled = true;
        }
        private void dgv_SalesItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (salesListFlag != true)
                {
                    if (dgv_SalesItem.Rows.Count > 0 && e.RowIndex >= 0)
                    {
                        Decimal Qty = 0, IGST = 0, GST = 0, Cost = 0, TotalItems = 0, TotalCost = 0, TotalGst = 0, TotalAmount = 0, igstAmount = 0, gstAmount = 0, DiscAmount = 0;
                        string itmCode = "", quantity = "",unit="", itemname = "";  txt_ItemCode.Enabled = true;
                        if (dgv_SalesItem.CurrentCell.OwningColumn.Name == "colEdit")
                        {
                            editflag = true;
                               rowindex = dgv_SalesItem.CurrentRow.Index;
                            itemId = dgv_SalesItem.Rows[rowindex].Cells["id"].Value.ToString();
                            txt_ItemCode.Text = dgv_SalesItem.Rows[rowindex].Cells["colItemCode"].Value.ToString();
                            txt_Discription.Text = dgv_SalesItem.Rows[rowindex].Cells["colDiscription"].Value.ToString();
                            txt_GST.Text = dgv_SalesItem.Rows[rowindex].Cells["ColGST"].Value.ToString();
                            txt_IGST.Text = dgv_SalesItem.Rows[rowindex].Cells["colIGST"].Value.ToString();
                            txt_Qty.Text = dgv_SalesItem.Rows[rowindex].Cells["ColQty"].Value.ToString();
                            txt_Free.Text = dgv_SalesItem.Rows[rowindex].Cells["ColFree"].Value.ToString();
                            txt_Packing.Text = dgv_SalesItem.Rows[rowindex].Cells["ColPacking"].Value.ToString();
                            txt_UnitCost.Text = Convert.ToDecimal(dgv_SalesItem.Rows[rowindex].Cells["colUnitcost"].Value.ToString()).ToString("##.00");
                            txt_Amount.Text = Convert.ToDecimal(dgv_SalesItem.Rows[rowindex].Cells["colAmount"].Value.ToString()).ToString("##.00");
                            if(dgv_SalesItem.Rows[rowindex].Cells["batchentry"].Value !=null)
                                 bath_entry = dgv_SalesItem.Rows[rowindex].Cells["batchentry"].Value.ToString();// dgv_SalesItem.Rows[rowindex].Cells["batchentry"].Value.ToString();
                            string unit1 = "", unit2 = "";
                            DataTable dt_itemdetails = this.cntrl.dt_itemdetails(itemId);
                            //unit1 = dt_itemdetails.Rows[0]["Unit1"].ToString();
                            cmb_Unit.Text = dgv_SalesItem.Rows[rowindex].Cells["ColUnit"].Value.ToString();

                            if (dt_itemdetails.Rows.Count>0)
                            {
                                //if (unit1 != "")
                                //{
                                //    cmb_Unit.Items.Add(unit1);
                                //}
                                //if (dt_itemdetails.Rows[0]["Unit2"].ToString() != "null" && dt_itemdetails.Rows[0]["Unit2"].ToString() != "")
                                //{
                                //    unit2 = dt_itemdetails.Rows[0]["Unit2"].ToString();
                                //    cmb_Unit.Items.Add(unit2);
                                //}
                               
                                txtBarcode.Text = dt_itemdetails.Rows[0]["Barcode"].ToString();
                                if (salesOrder_flag == true)
                                {
                                    txt_GST.Text= dt_itemdetails.Rows[0]["GstVat"].ToString();
                                    txt_Packing.Text= dt_itemdetails.Rows[0]["Packing"].ToString();
                                }
                            }
                           
                            btn_AddtoGrid.Text = "Update"; btn_cancel.Visible = true;
                            txt_ItemCode.Enabled = false; txt_Discription.Enabled = false; 
                            btn_item_Choose.Enabled = false; txtBarcode.Enabled = false;

                        }
                        if (dgv_SalesItem.CurrentCell.OwningColumn.Name == "colDelete")
                        {
                            int index = dgv_SalesItem.CurrentRow.Index;
                            string entery = dgv_SalesItem.CurrentRow.Cells["batchentry"].Value.ToString();
                            itmCode = dgv_SalesItem.CurrentRow.Cells["id"].Value.ToString();
                            itemname= dgv_SalesItem.CurrentRow.Cells["colDiscription"].Value.ToString();
                            quantity = dgv_SalesItem.CurrentRow.Cells["ColQty"].Value.ToString();
                            unit = dgv_SalesItem.CurrentRow.Cells["ColUnit"].Value.ToString();//batchentry
                            DialogResult res = MessageBox.Show("Are you sure you want to delete?", "Delete confirmation",
                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (res == DialogResult.No)
                            {
                            }
                            else
                            {
                                Qty = Convert.ToDecimal(dgv_SalesItem.Rows[index].Cells["ColQty"].Value.ToString());
                                if (Txt_TotalIGST.Text != "")
                                {
                                    if (dgv_SalesItem.CurrentRow.Cells["colIGST"].Value != null && dgv_SalesItem.Rows[index].Cells["colIGST"].Value.ToString() != "")
                                    {
                                        igstAmount = ((Convert.ToDecimal(dgv_SalesItem.CurrentRow.Cells["ColQty"].Value.ToString()) * Convert.ToDecimal(dgv_SalesItem.CurrentRow.Cells["colUnitcost"].Value.ToString())) * Convert.ToDecimal(dgv_SalesItem.CurrentRow.Cells["colIGST"].Value.ToString())) / 100;
                                        IGST = Convert.ToDecimal(Txt_TotalIGST.Text) - igstAmount;
                                        Txt_TotalIGST.Text = Convert.ToDecimal(IGST).ToString("##0.00");
                                    }
                                }
                                if (txt_TotalCost.Text != "")
                                {
                                    Cost = (Qty * Convert.ToDecimal(dgv_SalesItem.Rows[index].Cells["colUnitcost"].Value.ToString()));
                                    TotalCost = Convert.ToDecimal(txt_TotalCost.Text) - Cost;
                                    txt_TotalCost.Text = Convert.ToDecimal(TotalCost).ToString("##0.00");
                                }
                                if (txt_totalItems.Text != "")
                                {
                                    TotalItems = Convert.ToDecimal(txt_totalItems.Text) - 1;
                                    txt_totalItems.Text = Convert.ToDecimal(TotalItems).ToString();
                                }
                                if (Txt_TotalAmount.Text != "")
                                {
                                    TotalAmount = Convert.ToDecimal(Txt_TotalAmount.Text) - Convert.ToDecimal(dgv_SalesItem.Rows[index].Cells["colAmount"].Value.ToString());
                                    Txt_TotalAmount.Text = Convert.ToDecimal(TotalAmount).ToString("##0.00");
                                }
                                if (txt_CGST.Text != "" && txt_SGST.Text != "")
                                {
                                    if (dgv_SalesItem.CurrentRow.Cells["ColGST"].Value != null && dgv_SalesItem.Rows[index].Cells["ColGST"].Value.ToString() != "")
                                    {
                                        TotalGst = Convert.ToDecimal(txt_CGST.Text) + Convert.ToDecimal(txt_SGST.Text);
                                        gstAmount = ((Convert.ToDecimal(dgv_SalesItem.CurrentRow.Cells["ColQty"].Value.ToString()) * Convert.ToDecimal(dgv_SalesItem.CurrentRow.Cells["colUnitcost"].Value.ToString())) * Convert.ToDecimal(dgv_SalesItem.CurrentRow.Cells["ColGST"].Value.ToString())) / 100;
                                        GST = TotalGst - gstAmount;
                                        decimal gst_ = GST / 2;
                                        txt_CGST.Text = Convert.ToDecimal(gst_).ToString("##0.00");
                                        txt_SGST.Text = Convert.ToDecimal(gst_).ToString("##0.00");
                                    }
                                }
                                //if (txt_SGST.Text != "")
                                //{
                                //    TotalGst = Convert.ToDecimal(txt_SGST.Text);
                                //    gstAmount = ((Convert.ToDecimal(dgv_SalesItem.CurrentRow.Cells["ColQty"].Value.ToString()) * Convert.ToDecimal(dgv_SalesItem.CurrentRow.Cells["colUnitcost"].Value.ToString())) * Convert.ToDecimal(dgv_SalesItem.CurrentRow.Cells["ColGST"].Value.ToString())) / 100;
                                //    GST = TotalGst - gstAmount;
                                //    txt_SGST.Text = GST.ToString("##0.000");
                                //}
                                if (txt_DiscAmount.Text != "0.00")
                                {
                                    DiscAmount = ((Convert.ToDecimal(Txt_TotalAmount.Text) * Convert.ToDecimal(txt_Discount.Text)) / 100);
                                    txt_DiscAmount.Text = Convert.ToDecimal(DiscAmount).ToString("##0.00");
                                    txt_GrandTotal.Text = Convert.ToDecimal(Convert.ToDecimal(Txt_TotalAmount.Text) - Convert.ToDecimal(txt_DiscAmount.Text)).ToString("##.000");
                                }
                                else
                                {
                                    txt_GrandTotal.Text = Convert.ToDecimal(Txt_TotalAmount.Text).ToString("##0.00");// String.Format("{0:C}", Convert.ToDecimal(Txt_TotalAmount.Text));
// 
                                }
                                dgv_SalesItem.Rows.RemoveAt(index);
                                if(dgvShelfNo.Rows.Count>0)
                                {
                                    dgvShelfNo.Rows.RemoveAt(index);
                                }
                                if (dgrid_prescription.Rows.Count>0)
                                {
                                    fill_prescription_delete(itemname);
                                }
                                delete_batch(entery);
                                //fill_Batch_delete(itmCode, quantity, unit);        
                            }
                        }
                    }
                }
            else
                {
                    if (dgv_SalesItem.Rows.Count > 0 && e.RowIndex >= 0)
                    {
                        //Decimal Qty = 0, IGST = 0, GST = 0, Cost = 0, TotalItems = 0, TotalCost = 0, TotalGst = 0, TotalAmount = 0, igstAmount = 0, gstAmount = 0, DiscAmount = 0;
                        //string itmCode = "", quantity = ""; 
                        txt_ItemCode.Enabled = true;
                        if (dgv_SalesItem.CurrentCell.OwningColumn.Name == "colEdit")
                        {
                            editflag = true;
                            rowindex = dgv_SalesItem.CurrentRow.Index;
                            txt_ItemCode.Text = dgv_SalesItem.Rows[rowindex].Cells["colItemCode"].Value.ToString();
                            txt_Discription.Text = dgv_SalesItem.Rows[rowindex].Cells["colDiscription"].Value.ToString();
                            cmb_Unit.Text = dgv_SalesItem.Rows[rowindex].Cells["ColUnit"].Value.ToString();
                            txt_GST.Text = dgv_SalesItem.Rows[rowindex].Cells["ColGST"].Value.ToString();
                            txt_IGST.Text = "0";// dgv_SalesItem.Rows[rowindex].Cells["colIGST"].Value.ToString();
                            txt_Qty.Text = dgv_SalesItem.Rows[rowindex].Cells["ColQty"].Value.ToString();
                            txt_Free.Text = dgv_SalesItem.Rows[rowindex].Cells["ColFree"].Value.ToString();
                            itemId = dgv_SalesItem.Rows[rowindex].Cells["id"].Value.ToString();
                            if (dgv_SalesItem.Rows[rowindex].Cells["batchentry"].Value != null)
                                bath_entry = dgv_SalesItem.Rows[rowindex].Cells["batchentry"].Value.ToString();
                            if (salesOrder_flag == true)
                            {
                                cmb_Unit.Items.Clear();
                                DataTable dtb = this.cntrl.get_salesrate_unit(itemId);
                                update_itemload(dtb);
                            }
                            else
                            {
                                txt_Packing.Text = dgv_SalesItem.Rows[rowindex].Cells["ColPacking"].Value.ToString();
                                txt_UnitCost.Text = Convert.ToDecimal(dgv_SalesItem.Rows[rowindex].Cells["colUnitcost"].Value.ToString()).ToString("##.00");
                                txt_Amount.Text = Convert.ToDecimal(dgv_SalesItem.Rows[rowindex].Cells["colAmount"].Value.ToString()).ToString("##.00");
                            }
                            btn_AddtoGrid.Text = "Update"; btn_cancel.Visible = true;
                            txt_ItemCode.Enabled = false; txt_Discription.Enabled = false;txtBarcode.Enabled = false;
                            btn_item_Choose.Enabled = false;
                        }
                        //if(btnSave.Text!="UPDATE")
                        //{
                        //    if (dgv_SalesItem.CurrentCell.OwningColumn.Name == "colDelete")
                        //    {
                        //        int index = dgv_SalesItem.CurrentRow.Index;
                        //        itmCode = dgv_SalesItem.CurrentRow.Cells["id"].Value.ToString();
                        //        quantity = dgv_SalesItem.CurrentRow.Cells["ColQty"].Value.ToString();
                        //        DialogResult res = MessageBox.Show("Are you sure you want to delete?", "Delete confirmation",
                        //          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        //        if (res == DialogResult.No)
                        //        {
                        //        }
                        //        else
                        //        {
                        //            Qty = Convert.ToDecimal(dgv_SalesItem.Rows[index].Cells["ColQty"].Value.ToString());
                        //            //if (Txt_TotalIGST.Text != "")
                        //            //{
                        //            //    if (dgv_SalesItem.CurrentRow.Cells["colIGST"].Value != null && dgv_SalesItem.Rows[index].Cells["colIGST"].Value.ToString() != "")
                        //            //    {
                        //            //        igstAmount = ((Convert.ToDecimal(dgv_SalesItem.CurrentRow.Cells["ColQty"].Value.ToString()) * Convert.ToDecimal(dgv_SalesItem.CurrentRow.Cells["colUnitcost"].Value.ToString())) * Convert.ToDecimal(dgv_SalesItem.CurrentRow.Cells["colIGST"].Value.ToString())) / 100;
                        //            //        IGST = Convert.ToDecimal(Txt_TotalIGST.Text) - igstAmount;
                        //            //        Txt_TotalIGST.Text = Convert.ToDecimal(IGST).ToString("##0.00");
                        //            //    }
                        //            //}
                        //            if (txt_TotalCost.Text != "")
                        //            {
                        //                Cost = (Qty * Convert.ToDecimal(dgv_SalesItem.Rows[index].Cells["colUnitcost"].Value.ToString()));
                        //                TotalCost = Convert.ToDecimal(txt_TotalCost.Text) - Cost;
                        //                txt_TotalCost.Text = Convert.ToDecimal(TotalCost).ToString("##0.000");
                        //            }
                        //            if (txt_totalItems.Text != "")
                        //            {
                        //                TotalItems = Convert.ToDecimal(txt_totalItems.Text) - 1;
                        //                txt_totalItems.Text = Convert.ToDecimal(TotalItems).ToString();
                        //            }
                        //            if (Txt_TotalAmount.Text != "")
                        //            {
                        //                TotalAmount = Convert.ToDecimal(Txt_TotalAmount.Text) - Convert.ToDecimal(dgv_SalesItem.Rows[index].Cells["colAmount"].Value.ToString());
                        //                Txt_TotalAmount.Text = Convert.ToDecimal(TotalAmount).ToString("##0.000");
                        //            }
                        //            //if (txt_CGST.Text != "" && txt_SGST.Text != "")
                        //            //{
                        //            //    if (dgv_SalesItem.CurrentRow.Cells["ColGST"].Value != null && dgv_SalesItem.Rows[index].Cells["ColGST"].Value.ToString() != "")
                        //            //    {
                        //            //        TotalGst = Convert.ToDecimal(txt_CGST.Text) + Convert.ToDecimal(txt_SGST.Text);
                        //            //        gstAmount = ((Convert.ToDecimal(dgv_SalesItem.CurrentRow.Cells["ColQty"].Value.ToString()) * Convert.ToDecimal(dgv_SalesItem.CurrentRow.Cells["colUnitcost"].Value.ToString())) * Convert.ToDecimal(dgv_SalesItem.CurrentRow.Cells["ColGST"].Value.ToString())) / 100;
                        //            //        GST = TotalGst - gstAmount;
                        //            //        decimal gst_ = GST / 2;
                        //            //        txt_CGST.Text = Convert.ToDecimal(gst_).ToString("##0.00");
                        //            //        txt_SGST.Text = Convert.ToDecimal(gst_).ToString("##0.00");
                        //            //    }
                        //            //}
                        //            if (txt_SGST.Text != "")
                        //            {
                        //                TotalGst = Convert.ToDecimal(txt_SGST.Text);
                        //                gstAmount = ((Convert.ToDecimal(dgv_SalesItem.CurrentRow.Cells["ColQty"].Value.ToString()) * Convert.ToDecimal(dgv_SalesItem.CurrentRow.Cells["colUnitcost"].Value.ToString())) * Convert.ToDecimal(dgv_SalesItem.CurrentRow.Cells["ColGST"].Value.ToString())) / 100;
                        //                GST = TotalGst - gstAmount;
                        //                txt_SGST.Text = GST.ToString("##0.000");
                        //            }
                        //            if (txt_DiscAmount.Text != "0.000")
                        //            {
                        //                DiscAmount = ((Convert.ToDecimal(Txt_TotalAmount.Text) * Convert.ToDecimal(txt_Discount.Text)) / 100);
                        //                txt_DiscAmount.Text = Convert.ToDecimal(DiscAmount).ToString("##0.000");
                        //                txt_GrandTotal.Text = Convert.ToDecimal(Convert.ToDecimal(Txt_TotalAmount.Text) - Convert.ToDecimal(txt_DiscAmount.Text)).ToString("##.000");
                        //            }
                        //            else
                        //            {
                        //                txt_GrandTotal.Text = Convert.ToDecimal(Txt_TotalAmount.Text).ToString("##0.000");
                        //            }
                        //            dgv_SalesItem.Rows.RemoveAt(index);
                        //            dgvShelfNo.Rows.RemoveAt(index);
                        //            fill_Batch_delete(itmCode, quantity);
                        //            //string dt = DateTime.Now.Date.ToString("yyyy-MM-dd");
                        //            //DateTime Timeonly = DateTime.Now;
                        //            //this.cntrl.save_log(doctor_id, "Sales", "logged user deletes sales", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Delete");
                        //        }
                        //    }
                        //}

                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void delete_batch(string entery)
        {
            for (int i = 0; i < dgv_BatchSale.Rows.Count; i++)
            {
                if (dgv_BatchSale.Rows[i].Cells["colBatchEntry"].Value.ToString() == entery)
                {
                    dgv_BatchSale.Rows.RemoveAt(i);
                }
            }
        }
        public void update_itemload(DataTable dt_cost)
        {
            string unit1 = "", unit2 = ""; decimal Amount = 0, gst_Amount = 0, Total_Amount = 0, gst = 0, igst_Amount = 0, igst = 0;
            unit1 = dt_cost.Rows[0]["Unit1"].ToString();
            if (unit1 != "")
            {
                cmb_Unit.Items.Add(unit1);
            }
            if (dt_cost.Rows[0]["Unit2"].ToString() != "null" && dt_cost.Rows[0]["Unit2"].ToString() != "")
            {
                unit2 = dt_cost.Rows[0]["Unit2"].ToString();
                cmb_Unit.Items.Add(unit2);
            }
            cmb_Unit.SelectedIndex = 0;
            if (dt_cost.Rows.Count > 0)
            {
                txt_Packing.Text = dt_cost.Rows[0][0].ToString();
            }
            else
                txt_Packing.Text = "";
            txt_UnitCost.Text = dt_cost.Rows[0][1].ToString();
            if (Convert.ToDecimal(txt_GST.Text) > 0)
            {
                Amount = Convert.ToDecimal(txt_Qty.Text) * Convert.ToDecimal(txt_UnitCost.Text);
                gst_Amount = (Amount * Convert.ToDecimal(txt_GST.Text)) / 100;
                Total_Amount = Amount + gst_Amount;
                txt_Amount.Text = Total_Amount.ToString("##.00");
            }
            else if (Convert.ToDecimal(txt_IGST.Text) > 0)
            {
                Amount = Convert.ToDecimal(txt_Qty.Text) * Convert.ToDecimal(txt_UnitCost.Text);
                igst_Amount = (Amount * igst) / 100;
                Total_Amount = Amount + igst_Amount;
                txt_Amount.Text = Total_Amount.ToString("##.00");
            }
            else
            {
                txt_Amount.Text = Convert.ToDecimal(Convert.ToDecimal(txt_Qty.Text) * Convert.ToDecimal(dt_cost.Rows[0][1].ToString())).ToString();
            }
        }
        public void fill_prescription_delete(string itemcode)
        {
            DataTable dtrow = new DataTable();
            dtrow.Columns.Clear();
            dtrow.Rows.Clear();
            dtrow.Columns.Add("colDrug");
            dtrow.Columns.Add("Colduration");
            dtrow.Columns.Add("coldirection");
            dtrow.Columns.Add("colremarks");
            if (dtrow.Columns.Count > 0)
            {
                for (int i = 0; i < dgrid_prescription.Rows.Count; i++)
                {
                    if (dgrid_prescription.Rows[i].Cells["colDrug"].Value.ToString() == itemcode)
                    {
                        dtrow.Rows.Add(dgrid_prescription.Rows[i].Cells["colDrug"].Value.ToString(), dgrid_prescription.Rows[i].Cells["Colduration"].Value.ToString(), dgrid_prescription.Rows[i].Cells["coldirection"].Value.ToString(), dgrid_prescription.Rows[i].Cells["colremarks"].Value.ToString());
                    }
                }
            }
            if (dtrow.Rows.Count > 0)
            {
                dgrid_prescription.Rows.Clear();
                for (int i = 0; i < dtrow.Rows.Count; i++)
                {
                    dgrid_prescription.Rows.Add(dtrow.Rows[i][0].ToString(), dtrow.Rows[i][1].ToString(), dtrow.Rows[i][2].ToString(), dtrow.Rows[i][3].ToString());
                }
            }
            else
            {
                dgrid_prescription.Rows.Clear();
            }
        }
        //public void fill_Batch_delete(string itemcode, string quantity,string unit)
        //{
        //    DataTable dtrow = new DataTable();
        //    dtrow.Columns.Clear();
        //    dtrow.Rows.Clear();
        //    dtrow.Columns.Add("invNo");
        //    dtrow.Columns.Add("invDate");
        //    dtrow.Columns.Add("ItemCode");
        //    dtrow.Columns.Add("Batchno");
        //    dtrow.Columns.Add("qty");
        //    dtrow.Columns.Add("Cost");
        //    dtrow.Columns.Add("Stock");
        //    dtrow.Columns.Add("CurrentStock");
        //    dtrow.Columns.Add("IsexpDate");
        //    dtrow.Columns.Add("BatchEntry");
        //    dtrow.Columns.Add("Prd.date");
        //    dtrow.Columns.Add("Exp.date");
        //    dtrow.Columns.Add("unit");
        //    if (dtrow.Columns.Count > 0)
        //    {
        //        for (int i = 0; i < dgv_BatchSale.Rows.Count; i++)
        //        {
        //            if (dgv_BatchSale.Rows[i].Cells["coiltem_code"].Value.ToString() != itemcode)
        //            {
        //                dtrow.Rows.Add(dgv_BatchSale.Rows[i].Cells["ColinvNum"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["ColInvDate"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["coiltem_code"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colBatchnumber"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colQuantity"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["rate"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colStock"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["currentStock"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colIsExp"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colBatchEntry"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["prddate"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["expdate"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["unit"].Value.ToString());
        //            }
        //            else if (dgv_BatchSale.Rows[i].Cells["coiltem_code"].Value.ToString() == itemcode)
        //            {
        //                if (dgv_BatchSale.Rows[i].Cells["unit"].Value.ToString() != unit)
        //                {
        //                    dtrow.Rows.Add(dgv_BatchSale.Rows[i].Cells["ColinvNum"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["ColInvDate"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["coiltem_code"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colBatchnumber"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colQuantity"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["rate"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colStock"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["currentStock"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colIsExp"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colBatchEntry"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["prddate"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["expdate"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["unit"].Value.ToString());
        //                }

        //            }
        //        }
        //    }
        //    if (dtrow.Rows.Count > 0)
        //    {
        //        dgv_BatchSale.Rows.Clear();
        //        for (int i = 0; i < dtrow.Rows.Count; i++)
        //        {
        //            dgv_BatchSale.Rows.Add(dtrow.Rows[i][0].ToString(), dtrow.Rows[i][1].ToString(), dtrow.Rows[i][2].ToString(), dtrow.Rows[i][3].ToString(), dtrow.Rows[i][4].ToString(), dtrow.Rows[i][5].ToString(), dtrow.Rows[i][6].ToString(), dtrow.Rows[i][7].ToString(), dtrow.Rows[i][8].ToString(), dtrow.Rows[i][9].ToString(), dtrow.Rows[i][10].ToString(), dtrow.Rows[i][11].ToString(), dtrow.Rows[i][12].ToString());
        //        }
        //    }
        //    else
        //    {
        //        dgv_BatchSale.Rows.Clear();
        //    }
        //}
        private void frmSales_Load(object sender, EventArgs e)
        {
            try
            {
                txtBarcode.Focus();
                pnlShelf.Hide(); 
                Cmb_ModeOfPaymnt.SelectedIndex = 0;
                dgv_SalesItem.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv_SalesItem.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv_SalesItem.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv_SalesItem.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv_SalesItem.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv_SalesItem.Columns[9].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                lbIdoctor.Location = new Point(131, 103);
                lbIdoctor.Visible = false;
                btnReport.Visible = false; dgv_SalesItem.Enabled = true;
                lbPatient.Visible = false;// lblAdvance.Text = "0";
                DataTable dt_consume_check = this.cntrl.Get_consume_tick();
                if (dt_consume_check.Rows.Count > 0)
                {
                    if (dt_consume_check.Rows[0]["consumables"].ToString() == "Yes")
                    {
                        type = "Consumable";
                    }
                    else
                        type = "Pharmacy";
                }
                else
                    type = "Pharmacy";
                if (salesListFlag == true)
                {
                    btnSave.Enabled = true;
                    BTN_CLEAR.Enabled = true;
                    btn_cancel.Enabled = true;
                    btn_AddtoGrid.Enabled = true;
                    dgv_SalesItem.Enabled = true;
                }
                if (sales_Edit == true)
                {
                    clear_All_Data();
                    //DisabledAllControlls();
                    btnReport.Visible = true;
                    if (invnum_Edit > 0)
                    {
                        DataTable dtb = this.cntrl.sales_details(invnum_Edit);
                        Load_masterdetails(dtb);
                        btnSave.Text = "SAVE"; btnSave.Enabled = false; btn_AddtoGrid.Enabled = false;
                        lblAdvance.Text = "0"; dgv_SalesItem.Enabled = false;
                        btn_item_Choose.Enabled = false;
                        txtBarcode.Enabled = false;
                        txt_Discription.Enabled = false;
                        txt_ItemCode.Enabled = false;
                        //this.cntrl.sales_items_details(invnum_Edit);
                    }
                }
                else
                {
                    if (salesOrder_flag == true)
                    {
                        if (invnum_order != "")
                        {
                            DataTable dtb = this.cntrl.salesOrder_master(Convert.ToInt32(invnum_order));
                            Load_order_master(dtb);
                            DataTable dtb1 = this.cntrl.order_itemsDtails(Convert.ToInt32(invnum_order));
                            Load_orderitems(dtb1);
                            lblAdvance.Text = "0";
                        }
                    }
                    else
                    {
                        load_prescription();
                        EnabledAllControlls();
                        btnReport.Visible = false;
                        lblAdvance.Text = "0";
                    }
                    DataTable Document_Number = this.cntrl.docnumber();
                    if (String.IsNullOrWhiteSpace(Document_Number.Rows[0][0].ToString()))
                    {
                        txtDocumentNumber.Text = "1";
                    }
                    else
                    {
                        int Count = Convert.ToInt32(Document_Number.Rows[0][0]);
                        int incrValue = Convert.ToInt32(Count);
                        incrValue += 1;
                        txtDocumentNumber.Text = incrValue.ToString();
                    }
                    System.Data.DataTable tb_doctor = this.cntrl.get_doctor(doctor_id);
                    if (tb_doctor.Rows.Count > 0)
                    {
                        txtBdoctor.Text = tb_doctor.Rows[0]["doctor_name"].ToString();

                    }
                }
                System.Data.DataTable clinicname = this.cntrl.Get_CompanyNAme();
                if (path != "")
                {
                    string curFile = this.cntrl.server() + "\\Pappyjoe_utilities\\Logo\\" + path;

                    if (File.Exists(curFile))
                    {
                        logo_name = "";
                        logo_name = path;
                        string Apppath = System.IO.Directory.GetCurrentDirectory();
                        if (!File.Exists(Apppath + "\\" + logo_name))
                        {
                            System.IO.File.Copy(curFile, Apppath + "\\" + logo_name);
                        }
                    }
                    else
                    {
                        logo_name = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void clear_All_Data()
        {
            dtpDocumentDate.Text = DateTime.Now.ToString();
            txtBdoctor.Text = "";
            lbIdoctor.Visible = false;
            lbPatient.Visible = false;
            txt_LRNO.Text = "";
            DTP_LRDate.Text = DateTime.Now.ToString();
            txt_OrderNo.Text = "";
            DTP_OrderDate.Text = DateTime.Now.ToString();
            txt_Through.Text = "";
            txtPatient.Text = "";
            txtPatientID.Text = "";
            txt_Street.Text = "";
            txt_Locality.Text = "";
            txt_City.Text = "";
            txt_PhoneNo.Text = "";
            txt_totalItems.Text = "0";
            Txt_TotalAmount.Text = "0.00";
            txt_DiscAmount.Text = "0.00";
            txt_Discount.Text = "0";
            txt_GrandTotal.Text = "00.00";
            txt_GrandTotal.ForeColor = Color.Red;
            txt_TotalCost.Text = "0.00";
            Txt_TotalIGST.Text = "0";
            txt_SGST.Text = "0";
            txt_CGST.Text = "0";
            txt_4Digit.Text = "";
            txt_BankNAme.Text = "";
            txt_Number.Text = "";
            dgv_SalesItem.Rows.Clear();
            dgv_BatchSale.Rows.Clear();
            txtAmntRcvd.Text = "0.00";
            txtBalncToGve.Text = "0.00";
        }
        public void DisabledAllControlls()
        {
            dtpDocumentDate.Enabled = false;
            txtSales.Enabled = false;
            txtBdoctor.Enabled = false;
            lbIdoctor.Visible = false;
            lbPatient.Visible = false;
            DTP_LRDate.Enabled = false;
            txt_OrderNo.Enabled = false;
            DTP_OrderDate.Enabled = false;
            txt_Through.Enabled = false;
            txtPatient.Enabled = false;
            txtPatientID.Enabled = false;
            txt_Street.Enabled = false;
            txt_Locality.Enabled = false;
            txt_City.Enabled = false;
            txt_PhoneNo.Enabled = false;
            txt_totalItems.Enabled = false;
            Txt_TotalAmount.Enabled = false;
            txt_DiscAmount.Enabled = false;
            txt_Discount.Enabled = false;
            txt_GrandTotal.Enabled = false;
            txt_GrandTotal.ForeColor = Color.Red;
            txt_TotalCost.Enabled = false;
            Txt_TotalIGST.Enabled = false;
            txt_SGST.Enabled = false;
            txt_CGST.Enabled = false;
            txt_ItemCode.Enabled = false;
            txt_Discription.Enabled = false;
            txt_Packing.Enabled = false;
            cmb_Unit.Enabled = false;
            txt_GST.Enabled = false;
            txt_IGST.Enabled = false;
            txt_Qty.Enabled = false;
            txt_Free.Enabled = false;
            txtBarcode.Enabled = false;
            txt_UnitCost.Enabled = false;
            btn_AddtoGrid.Enabled = false;
            btnSave.Enabled = false;
            dgv_SalesItem.Enabled = false;
            txt_LRNO.Enabled = false;
            BTN_CLEAR.Enabled = false;
        }
        public void Load_masterdetails(DataTable dtb_master)
        {
            try
            {
                if (dtb_master.Rows.Count > 0)
                {
                    txtDocumentNumber.Text = dtb_master.Rows[0]["InvNumber"].ToString();
                    dtpDocumentDate.Text = dtb_master.Rows[0]["InvDate"].ToString();
                    txtSales.Text = dtb_master.Rows[0]["SalesmanCode"].ToString();
                    txtBdoctor.Text = dtb_master.Rows[0]["Prescribedby"].ToString();
                    txt_LRNO.Text = dtb_master.Rows[0]["LRNo"].ToString();
                    DTP_LRDate.Text = dtb_master.Rows[0]["LRDate"].ToString();
                    txt_OrderNo.Text = dtb_master.Rows[0]["OrderNumber"].ToString();
                    DTP_OrderDate.Text = dtb_master.Rows[0]["Orderdate"].ToString();
                    txt_Through.Text = dtb_master.Rows[0]["Through"].ToString();
                    txtPatient.Text = dtb_master.Rows[0]["cust_name"].ToString();
                    txtPatientID.Text = dtb_master.Rows[0]["cust_number"].ToString();
                    txt_Street.Text = dtb_master.Rows[0]["adr1"].ToString();
                    txt_Locality.Text = dtb_master.Rows[0]["adr2"].ToString();
                    txt_City.Text = dtb_master.Rows[0]["adr3"].ToString();
                    txt_PhoneNo.Text = dtb_master.Rows[0]["phone1"].ToString();
                    if (dtb_master.Rows[0]["PayMethod"].ToString()== "Credit Sale")
                    {
                        rad_CreditSale.Checked = true;
                    }
                    else
                    {
                        rad_CashSale.Checked = true;
                    }
                }
                DataTable dtb_sales = this.cntrl.sales_items_details(invnum_Edit);
                decimal gstAmount = 0, igstAmount = 0;
                decimal TotalAmount = 0, DisAmount = 0, TotalCost = 0, ToatalGST = 0, TotalIGST = 0;
                int qty = 0;
                for (int i = 0; i < dtb_sales.Rows.Count; i++)
                {
                    dgv_SalesItem.Rows.Add();
                    dgv_SalesItem.Rows[i].Cells["id"].Value = dtb_sales.Rows[i]["InvNumber"].ToString();
                    dgv_SalesItem.Rows[i].Cells["colItemCode"].Value = dtb_sales.Rows[i]["Item_Code"].ToString();
                    DataTable dtb_hsn = this.cntrl.get_hsn(dtb_sales.Rows[i]["Item_Code"].ToString());
                    dgv_SalesItem.Rows[i].Cells["id"].Value = dtb_hsn.Rows[0]["id"].ToString();
                    dgv_SalesItem.Rows[i].Cells["colItemCode"].Value = dtb_hsn.Rows[0]["Item_Code"].ToString();
                    dgv_SalesItem.Rows[i].Cells["colDiscription"].Value = dtb_sales.Rows[i]["Description"].ToString();
                    dgv_SalesItem.Rows[i].Cells["ColPacking"].Value = dtb_sales.Rows[i]["Packing"].ToString();
                    dgv_SalesItem.Rows[i].Cells["batch"].Value = dtb_sales.Rows[i]["batch_number"].ToString();
                    dgv_SalesItem.Rows[i].Cells["batchentry"].Value = dtb_sales.Rows[i]["batch_entry"].ToString();
                    dgv_SalesItem.Rows[i].Cells["c_disc"].Value = dtb_sales.Rows[i]["Discount"].ToString();//bahja
                    dgv_SalesItem.Rows[i].Cells["hsn"].Value = dtb_hsn.Rows[0]["HSN_Number"].ToString();
                    dgv_SalesItem.Rows[i].Cells["ColUnit"].Value = dtb_sales.Rows[i]["Unit"].ToString();
                    dgv_SalesItem.Rows[i].Cells["ColQty"].Value = dtb_sales.Rows[i]["Qty"].ToString();
                    dgv_SalesItem.Rows[i].Cells["ColFree"].Value = dtb_sales.Rows[i]["FreeQty"].ToString();
                    dgv_SalesItem.Rows[i].Cells["colUnitcost"].Value = Convert.ToDecimal(dtb_sales.Rows[i]["Rate"].ToString()).ToString("##.00");
                    //dgv_SalesItem.Rows[i].Cells["hsn"].Value = Convert.ToDecimal(dtb_sales.Rows[i]["Rate"].ToString()).ToString("##.00");

                    if (Convert.ToInt32(dtb_sales.Rows[i]["GST"].ToString()) > 0)
                    {

                        dgv_SalesItem.Rows[i].Cells["ColGST"].Value = dtb_sales.Rows[i]["GST"].ToString();
                        gstAmount = ((Convert.ToDecimal(dtb_sales.Rows[i]["Qty"].ToString()) * Convert.ToDecimal(dtb_sales.Rows[i]["Rate"].ToString())) * Convert.ToDecimal(dtb_sales.Rows[i]["GST"].ToString())) / 100;
                        ToatalGST = ToatalGST + gstAmount;
                    }
                    else
                        dgv_SalesItem.Rows[i].Cells["ColGST"].Value = "0";

                    dgv_SalesItem.Rows[i].Cells["colAmount"].Value = Convert.ToDecimal(dtb_sales.Rows[i]["TotalAmount"].ToString()).ToString("##.00");
                    TotalAmount = TotalAmount + Convert.ToDecimal(dtb_sales.Rows[i]["TotalAmount"].ToString());  //).ToString("##.000"); 
                    TotalCost = TotalCost + (Convert.ToInt32(dtb_sales.Rows[i]["Qty"].ToString()) * Convert.ToDecimal(dtb_sales.Rows[i]["Rate"].ToString()));
                    dgv_SalesItem.Rows[i].Cells["colEdit"].Value = PappyjoeMVC.Properties.Resources.editicon;
                    dgv_SalesItem.Rows[i].Cells["colDelete"].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                }
                qty = dtb_sales.Rows.Count;
                txt_totalItems.Text = qty.ToString();
                Txt_TotalAmount.Text = TotalAmount.ToString("##.00");
                if (Convert.ToDecimal(dtb_master.Rows[0]["Discount"].ToString()) > 0)
                {
                    txt_Discount.Text = dtb_master.Rows[0]["Discount"].ToString();
                }
                else
                {
                    txt_Discount.Text = "0";
                }
                if (Convert.ToDecimal(txt_Discount.Text) > 0)
                {
                    DisAmount = Convert.ToDecimal((TotalAmount * Convert.ToDecimal(txt_Discount.Text)) / 100);
                    txt_DiscAmount.Text = DisAmount.ToString("##.00"); 
                    txt_GrandTotal.Text = Convert.ToDecimal(Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(DisAmount)).ToString("##.00");
                }
                else
                {
                    txt_DiscAmount.Text = "0.00";
                    txt_GrandTotal.Text = TotalAmount.ToString("##.00");// String.Format("{0:C}", Convert.ToDecimal(TotalAmount));
//  
                }
                txt_GrandTotal.ForeColor = Color.Red;
                if (ToatalGST > 0)
                {
                    decimal sgst = ToatalGST / 2;
                    txt_SGST.Text = sgst.ToString("0.00");
                    txt_CGST.Text = sgst.ToString("0.00");
                }
                else
                {
                    txt_SGST.Text = "0";
                    txt_CGST.Text = "0";
                }
                if (TotalIGST > 0)
                {
                    Txt_TotalIGST.Text = TotalIGST.ToString("0.00");
                }
                else
                    Txt_TotalIGST.Text = "0";
                txt_TotalCost.Text = TotalCost.ToString("0.00");
                DataTable dtb_BatchSale = this.cntrl.get_batchsale(invnum_Edit);
                if (dtb_BatchSale.Rows.Count > 0)
                {
                    dgv_BatchSale.Rows.Clear();
                    for (int i = 0; i < dtb_BatchSale.Rows.Count; i++)
                    {
                        DataTable dt_batch = this.cntrl.batcdetails(invnum_Edit, dtb_BatchSale.Rows[i]["Unit2"].ToString());
                        //if(dt_batch.Rows.Count>1)
                        //{
                        //for (int j = 0; j< dt_batch.Rows.Count; j++)
                        //{
                        DataTable dt_batchnumb = this.cntrl.batchnumbetails(dtb_BatchSale.Rows[i]["batch_entry"].ToString());//
                        dgv_BatchSale.Rows.Add(dtb_BatchSale.Rows[i]["InvNumber"].ToString(), dtb_BatchSale.Rows[i]["InvDate"].ToString(), dtb_BatchSale.Rows[i]["Item_Code"].ToString(), dt_batchnumb.Rows[0]["BatchNumber"].ToString(), dtb_BatchSale.Rows[i]["Qty"].ToString(), dt_batchnumb.Rows[0]["batch_sales_rate"].ToString(), dt_batchnumb.Rows[0]["Qty"].ToString(),"", dt_batchnumb.Rows[0]["IsExpDate"].ToString(), dt_batchnumb.Rows[0]["Entry_No"].ToString(), dt_batchnumb.Rows[0]["prddate"].ToString(), dt_batchnumb.Rows[0]["expdate"].ToString(), dtb_BatchSale.Rows[i]["Unit"].ToString());

                        //    }
                        //}
                        //else
                        //{
                        //    DataTable dt_batchnumb = this.cntrl.batchnumbetails(dt_batch.Rows[0]["BatchEntry"].ToString());
                        //    dgv_BatchSale.Rows.Add(dtb_BatchSale.Rows[i]["InvNumber"].ToString(), dtb_BatchSale.Rows[i]["InvDate"].ToString(), dtb_BatchSale.Rows[i]["Item_Code"].ToString(), dt_batch.Rows[0]["BatchNumber"].ToString(), dtb_BatchSale.Rows[i]["Qty"].ToString(), "0", "0", dt_batch.Rows[0]["IsExpDate"].ToString(), dt_batch.Rows[0]["BatchEntry"].ToString(), dt_batchnumb.Rows[0]["prddate"].ToString(), dt_batchnumb.Rows[0]["expdate"].ToString(), dtb_BatchSale.Rows[i]["Unit"].ToString());

                        //}
                    }
                }// dtb_BatchSale.Rows[i]["stock"].ToString()
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void load_prescription()
        {
            System.Data.DataTable dt_pre_main = this.cntrl.load_prescription();
            int i = 0;
            for (int j = 0; j < dt_pre_main.Rows.Count; j++)
            {
                presgrid.Rows.Add(dt_pre_main.Rows[j]["id"].ToString(), dt_pre_main.Rows[j]["pt_name"].ToString() + " (" + dt_pre_main.Rows[j]["pt_id"].ToString() + ")", String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dt_pre_main.Rows[j]["date"].ToString())));

                i = i + 1;
            }
        }
        public void EnabledAllControlls()
        {
            dtpDocumentDate.Enabled = true;
            txtSales.Enabled = true;
            txtBdoctor.Enabled = true;
            lbIdoctor.Visible = false;
            lbPatient.Visible = false;
            DTP_LRDate.Enabled = true;
            txt_OrderNo.Enabled = true;
            DTP_OrderDate.Enabled = true;
            txt_LRNO.Enabled = true;
            txt_Through.Enabled = true;
            txtPatient.Enabled = true;
            txt_totalItems.Enabled = true;
            Txt_TotalAmount.Enabled = true;
            txt_DiscAmount.Enabled = true;
            txt_Discount.Enabled = true;
            txt_GrandTotal.Enabled = true;
            txt_GrandTotal.ForeColor = Color.Red;
            txt_TotalCost.Enabled = true;
            Txt_TotalIGST.Enabled = true;
            txt_SGST.Enabled = true;
            txt_CGST.Enabled = true;
            txt_ItemCode.Enabled = true;
            txt_Discription.Enabled = true;
            txt_Packing.Enabled = true;
            cmb_Unit.Enabled = true;
            txt_GST.Enabled = true;
            txt_IGST.Enabled = true;
            txt_Qty.Enabled = true;
            txt_Free.Enabled = true;
            txtBarcode.Enabled = true;
            txt_UnitCost.Enabled = true;
            btn_AddtoGrid.Enabled = true;
            btnSave.Enabled = true;
            BTN_CLEAR.Enabled = true;
            dgv_SalesItem.Enabled = true;
        }
        public void Load_order_master(DataTable dtb_orderMaster)
        {
            if (dtb_orderMaster.Rows.Count > 0)
            {
                txt_OrderNo.Text = dtb_orderMaster.Rows[0]["OrderNo"].ToString();
                DTP_OrderDate.Text = dtb_orderMaster.Rows[0]["OrderDate"].ToString();
                txtPatient.Text = dtb_orderMaster.Rows[0]["CustomerName"].ToString();
                txtPatientID.Text = dtb_orderMaster.Rows[0]["Cus_Id"].ToString();
                txt_Street.Text = dtb_orderMaster.Rows[0]["Address1"].ToString();
                txt_Locality.Text = dtb_orderMaster.Rows[0]["adr2"].ToString();
                txt_City.Text = dtb_orderMaster.Rows[0]["adr3"].ToString();
                txt_PhoneNo.Text = dtb_orderMaster.Rows[0]["Phone"].ToString();
                Txt_TotalAmount.Text= dtb_orderMaster.Rows[0]["TotalAmount"].ToString();
                txt_Discount.Text = dtb_orderMaster.Rows[0]["Discount_inr"].ToString();
                txt_DiscAmount.Text = dtb_orderMaster.Rows[0]["Discount_Amount"].ToString();
                txt_GrandTotal.Text = dtb_orderMaster.Rows[0]["GrandTotal"].ToString();
            }
        }
        public void Load_orderitems(DataTable dtb_order)
        {
            dgvShelfNo.Rows.Clear();
            if (dtb_order.Rows.Count > 0)
            {
                for (int i = 0; i < dtb_order.Rows.Count; i++)
                {
                    dgv_SalesItem.Rows.Add();
                    dgv_SalesItem.Rows[i].Cells["id"].Value = dtb_order.Rows[i]["id"].ToString();
                    dgv_SalesItem.Rows[i].Cells["colItemCode"].Value = dtb_order.Rows[i]["item_code"].ToString();
                    dgv_SalesItem.Rows[i].Cells["colDiscription"].Value = dtb_order.Rows[i]["Discription"].ToString();
                    dgv_SalesItem.Rows[i].Cells["ColQty"].Value = dtb_order.Rows[i]["Qty"].ToString();
                    dgv_SalesItem.Rows[i].Cells["colUnitcost"].Value = dtb_order.Rows[i]["Cost"].ToString();
                    dgv_SalesItem.Rows[i].Cells["hsn"].Value = dtb_order.Rows[i]["HSN_Number"].ToString();
                    dgv_SalesItem.Rows[i].Cells["ColPacking"].Value = "";
                    dgv_SalesItem.Rows[i].Cells["ColUnit"].Value = dtb_order.Rows[i]["Unit"].ToString();
                    dgv_SalesItem.Rows[i].Cells["ColGST"].Value = "0";
                    dgv_SalesItem.Rows[i].Cells["colIGST"].Value = "0";
                    dgv_SalesItem.Rows[i].Cells["ColFree"].Value = "0";
                    dgv_SalesItem.Rows[i].Cells["colUnitcost"].Value = dtb_order.Rows[i]["Cost"].ToString();
                    dgv_SalesItem.Rows[i].Cells["colAmount"].Value = dtb_order.Rows[i]["TotalAmount"].ToString();
                    dgv_SalesItem.Rows[i].Cells["colEdit"].Value = PappyjoeMVC.Properties.Resources.editicon;
                    dgv_SalesItem.Rows[i].Cells["colDelete"].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                    dgvShelfNo.Rows.Add(dtb_order.Rows[i]["item_code"].ToString(), dtb_order.Rows[i]["Shelf_No"].ToString());

                }
            }
        }
        Connection con = new Connection();// OpenConnection()
      
        public int Check_OrderItems()
        {
            int affect = 0;
            for (int i = 0; i < dgv_SalesItem.Rows.Count; i++)
            {
                for (int j = 0; j < dgv_BatchSale.Rows.Count; j++)
                {
                    if (dgv_SalesItem.Rows[i].Cells["id"].Value.ToString() == dgv_BatchSale.Rows[j].Cells["coiltem_code"].Value.ToString())
                    {
                        affect = 0;
                    }
                    else
                    {
                        affect = 1;
                    }
                }
            }
            return affect;
        }
        public static bool prescrption_sale = false;
        public static string pres_index = "";
      public static string PrescritionMain_id = "0";
        private int v;

        private void presgrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (presgrid.Rows.Count > 0)
                {
                    clear_itemdetails();
                  
                    clear_All_Data();
                    prescrption_sale = true;
                    int r = e.RowIndex; pres_index = e.RowIndex.ToString();
                    dgrid_prescription.Rows.Clear();
                    dgv_SalesItem.Rows.Clear(); dgv_BatchSale.Rows.Clear();
                     PrescritionMain_id = presgrid.Rows[r].Cells[0].Value.ToString();
                    System.Data.DataTable dt_prescriptionDetails = this.cntrl.prescription_main(PrescritionMain_id);
                    DataTable dtadvance = this.cntrl.Get_Advance(dt_prescriptionDetails.Rows[0]["id1"].ToString());
                    if (dtadvance.Rows.Count > 0)
                    {
                        label23.Visible = true;
                        lblAdvance.Show(); //adv_refund.Visible = true;
                        lblAdvance.Text = decimal.Parse(dtadvance.Rows[0][0].ToString()).ToString();//string.Format("{0:C}",
                    }
                    else
                    {
                        label23.Visible = true;
                        lblAdvance.Text = "0"; //string.Format("{0:C}", 0);
                    }
                    if (dt_prescriptionDetails.Rows.Count > 0)
                    {
                        txtBdoctor.Text = dt_prescriptionDetails.Rows[0]["doctor_name"].ToString();
                        txtPatient.Text = dt_prescriptionDetails.Rows[0]["pt_name"].ToString();
                        txtPatientID.Text = dt_prescriptionDetails.Rows[0]["pt_id"].ToString();
                        System.Data.DataTable dt_drugDetails = this.cntrl.prescription_dteails(PrescritionMain_id);
                        if (dt_drugDetails.Rows.Count > 0)
                        {
                            int i = 0; dgrid_prescription.Rows.Clear();
                            pnlprescription.Visible = true; string exp = "0",exp_items="",remov_expe="";
                            for (int j = 0; j < dt_drugDetails.Rows.Count; j++)
                            {
                                DataTable dt_drug = this.cntrl.get_inventoryname(dt_drugDetails.Rows[j]["drug_id"].ToString());
                                if(dt_drug.Rows.Count>0)
                                 {
                                    decimal duration_unit = 0;
                                    decimal duration = 0;
                                    decimal drug_qty = 0;
                                    int drug_qty1 = 0;
                                    duration_unit = Convert.ToDecimal(dt_drugDetails.Rows[j]["duration_unit"].ToString());
                                    drug_qty = Convert.ToDecimal(dt_drugDetails.Rows[j]["morning"].ToString()) + Convert.ToDecimal(dt_drugDetails.Rows[j]["noon"].ToString()) + Convert.ToDecimal(dt_drugDetails.Rows[j]["night"].ToString());
                                    if (duration_unit > 0 && drug_qty > 0)
                                    {
                                        if (dt_drugDetails.Rows[j]["duration_period"].ToString() == "year(s)")
                                        { duration = duration_unit * 365; }
                                        else if (dt_drugDetails.Rows[j]["duration_period"].ToString() == "month(s)")
                                        { duration = duration_unit * 30; }
                                        else if (dt_drugDetails.Rows[j]["duration_period"].ToString() == "week(s)")
                                        { duration = duration_unit * 7; }
                                        else
                                        { duration = duration_unit; }
                                        drug_qty1 = Convert.ToInt16(drug_qty);
                                        txt_Qty.Text = Convert.ToString(duration * drug_qty1).ToString();
                                        double Org_Qty = Convert.ToDouble(duration * drug_qty1);
                                        double decPart = Convert.ToDouble(Org_Qty) - Math.Truncate(Org_Qty);
                                        if (decPart > 0)
                                        {
                                            Org_Qty = Org_Qty + .5;
                                            //txt_Qty.Text = Convert.ToString(Org_Qty).ToString(); drug_id
                                        }
                                        dgrid_prescription.Rows.Add(dt_drugDetails.Rows[j]["drug_id"].ToString(),dt_drugDetails.Rows[j]["drug_name"].ToString() + " " + dt_drugDetails.Rows[j]["strength"].ToString(), dt_drugDetails.Rows[j]["duration_unit"].ToString() + " " + dt_drugDetails.Rows[j]["duration_period"].ToString(), dt_drugDetails.Rows[j]["morning"].ToString() + "-" + dt_drugDetails.Rows[j]["noon"].ToString() + "-" + dt_drugDetails.Rows[j]["night"].ToString(), dt_drugDetails.Rows[j]["add_instruction"].ToString(), Org_Qty);
                                    }
                                       
                                    
                                   
                                    //if (duration_unit > 0 && drug_qty > 0)
                                    //{
                                    //    if (dt_drugDetails.Rows[j]["duration_period"].ToString() == "year(s)")
                                    //    { duration = duration_unit * 365; }
                                    //    else if (dt_drugDetails.Rows[j]["duration_period"].ToString() == "month(s)")
                                    //    { duration = duration_unit * 30; }
                                    //    else if (dt_drugDetails.Rows[j]["duration_period"].ToString() == "week(s)")
                                    //    { duration = duration_unit * 7; }
                                    //    else
                                    //    { duration = duration_unit; }
                                    //    drug_qty1 = Convert.ToInt16(drug_qty);
                                    //    txt_Qty.Text = Convert.ToString(duration * drug_qty1).ToString();
                                    //    double Org_Qty = Convert.ToDouble(duration * drug_qty1);
                                    //    double decPart = Convert.ToDouble(Org_Qty) - Math.Truncate(Org_Qty);
                                    //    if (decPart > 0)
                                    //    {
                                    //        Org_Qty = Org_Qty + .5;
                                    //        txt_Qty.Text = Convert.ToString(Org_Qty).ToString();
                                    //    }
                                        //System.Data.DataTable dt_drug_inv_Details = this.cntrl.get_inventoryid(dt_drugDetails.Rows[j]["drug_id"].ToString());
                                        //if (dt_drug_inv_Details.Rows.Count > 0)
                                        //{
                                            //DataTable dt_exp = this.cntrl.drug_instock(dt_drug_inv_Details.Rows[0]["inventory_id"].ToString());
                                            //if (dt_exp.Rows.Count > 0 && dt_exp.Rows[0][0].ToString() != "")
                                            //{
                                                //DataTable dtb = this.cntrl.Get_itemdetails(dt_drug_inv_Details.Rows[0]["inventory_id"].ToString());
                                                //if (dtb.Rows.Count > 0)
                                                //{
                                                    //itemId = dt_drug_inv_Details.Rows[0]["inventory_id"].ToString();
                                                    //txt_ItemCode.Text = dtb.Rows[0]["id"].ToString();
                                                    //txt_Discription.Text = dtb.Rows[0]["item_name"].ToString();
                                                    //txt_UnitCost.Text = dtb.Rows[0]["Sales_Rate_Max"].ToString();
                                                    //txt_GST.Text = dtb.Rows[0]["GstVat"].ToString();
                                                    //txt_IGST.Text = "0";// dtb.Rows[0]["IGST"].ToString();
                                                    //txtdisc.Text = "0";
                                                    //cmb_Unit.Items.Clear();
                                                    //if (dtb.Rows[0]["OneUnitOnly"].ToString() == "True")
                                                    //{
                                                    //    cmb_Unit.Items.Add(dtb.Rows[0]["Unit1"].ToString());
                                                    //}
                                                    //else
                                                    //{
                                                    //    cmb_Unit.Items.Add(dtb.Rows[0]["Unit2"].ToString());
                                                    //    cmb_Unit.Items.Add(dtb.Rows[0]["Unit1"].ToString());
                                                    //}
                                                    //cmb_Unit.SelectedIndex = 0;
                                                    //decimal qty = 0;
                                                    //qty = Convert.ToDecimal(txt_Qty.Text);
                                                    //string unit = cmb_Unit.Text;
                                                    //call_Item_Batch(dtb.Rows[0]["id"].ToString(), qty, unit);
                                                    //if (dtFor_CurrentStockUpdate_Bill != null)
                                                    //{
                                                        //Fiil_BatchSale_Grid_Prescription_bill();//HSN_Number  dtFor_CurrentStockUpdate_Bill.Rows[0]["colentryNo"].ToString()
                                                        //TotalAmount_Calculation();//dtFor_CurrentStockUpdate_Bill.Rows[0]["colbatchNo"].ToString()
                                                        //dgv_SalesItem.Rows.Add(txt_ItemCode.Text, dtb.Rows[0]["item_code"].ToString(), txt_Discription.Text, txt_Packing.Text,"" ,"" , dtb.Rows[0]["HSN_Number"].ToString(), cmb_Unit.Text, txt_GST.Text, txt_IGST.Text, txt_Qty.Text, txt_Free.Text, txtdisc.Text, txt_UnitCost.Text, txt_Amount.Text, PappyjoeMVC.Properties.Resources.editicon, PappyjoeMVC.Properties.Resources.deleteicon);
                                                        //dgvShelfNo.Rows.Add(dtb.Rows[0]["item_code"].ToString(), dtb.Rows[0]["Shelf_No"].ToString());

                                                        //clear_itemdetails();
                                                        //Decimal TotalGst = 0;
                                                        //Decimal Total_Igst = 0;
                                                        //int Totalqty = 0;
                                                        //Decimal TotalAmount = 0;
                                                        //Decimal TotalCost = 0; decimal gstAmount = 0; decimal igstAmount = 0;
                                                        //foreach (DataGridViewRow dr in dgv_SalesItem.Rows)
                                                        //{
                                                        //    if (dr.Cells["ColGST"].Value != null && dr.Cells["ColGST"].Value.ToString() != "")
                                                        //    {
                                                        //        gstAmount = ((Convert.ToDecimal(dr.Cells["ColQty"].Value.ToString()) * Convert.ToDecimal(dr.Cells["colUnitcost"].Value.ToString())) * Convert.ToDecimal(dr.Cells["ColGST"].Value.ToString())) / 100;
                                                        //        TotalGst = TotalGst + gstAmount;
                                                        //    }
                                                        //    if (dr.Cells["colIGST"].Value != null && dr.Cells["colIGST"].Value.ToString() != "")
                                                        //    {
                                                        //        igstAmount = ((Convert.ToDecimal(dr.Cells["ColQty"].Value.ToString()) * Convert.ToDecimal(dr.Cells["colUnitcost"].Value.ToString())) * Convert.ToDecimal(dr.Cells["colIGST"].Value.ToString())) / 100;
                                                        //        Total_Igst = Total_Igst + igstAmount;
                                                        //    }
                                                        //    if (dr.Cells["colAmount"].Value != null && dr.Cells["colAmount"].Value.ToString() != "")
                                                        //    {
                                                        //        TotalAmount = TotalAmount + Convert.ToDecimal(dr.Cells["colAmount"].Value.ToString());
                                                        //    }
                                                        //    if (dr.Cells["colUnitcost"].Value != null && dr.Cells["colUnitcost"].Value.ToString() != "")
                                                        //    {
                                                        //        TotalCost = TotalCost + (Convert.ToDecimal(dr.Cells["ColQty"].Value.ToString()) * Convert.ToDecimal(dr.Cells["colUnitcost"].Value.ToString()));
                                                        //    }
                                                        //}
                                                        //Totalqty = dgv_SalesItem.Rows.Count;
                                                        //if (Totalqty > 0)
                                                        //{
                                                        //    txt_totalItems.Text = Totalqty.ToString();
                                                        //}
                                                        //if (TotalGst > 0)
                                                        //{
                                                        //    decimal cgst = TotalGst / 2;
                                                        //    txt_SGST.Text = Convert.ToDecimal(cgst).ToString("##0.00");
                                                        //    txt_CGST.Text = Convert.ToDecimal(cgst).ToString("##0.00");
                                                        //}
                                                        //if (Total_Igst > 0)
                                                        //{
                                                        //    Txt_TotalIGST.Text = Convert.ToDecimal(Total_Igst).ToString("##0.00");
                                                        //}
                                                        //if (TotalAmount > 0)
                                                        //{
                                                        //    Txt_TotalAmount.Text = Convert.ToDecimal(TotalAmount).ToString("##0.00");
                                                        //    txt_GrandTotal.Text = Convert.ToDecimal(TotalAmount).ToString("##0.00");// String.Format("{0:C}", Convert.ToDecimal(TotalAmount));
                                                        //                                                                            // 
                                                        //}
                                                        //if (TotalCost > 0)
                                                        //{
                                                        //    txt_TotalCost.Text = Convert.ToDecimal(TotalCost).ToString("##0.00");
                                                        //}
                                                    //}
                                                //}
                                            //}
                                            //else
                                            //{
                                            //    exp = "1";
                                            //    exp_items = dt_drugDetails.Rows[j]["drug_name"].ToString() + ",";
                                            //    if (exp_items != "")
                                            //        remov_expe = exp_items.Remove(exp_items.Length - 1);
                                            //}
                                        //}

                                    //}
                                }
                              
                            }
                            if(exp=="1")
                            {
                                MessageBox.Show(remov_expe+"  batch expired!..", "Expired", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("You can't add Duplicate Entry (" + ex.Message + ")", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void DocNumber_increment()
        {
            DataTable Document_Number = new DataTable();
            Document_Number = this.cntrl.docnumber();
            if (String.IsNullOrWhiteSpace(Document_Number.Rows[0][0].ToString()))
            {
                txtDocumentNumber.Text = "1";
            }
            else
            {
                int Count = Convert.ToInt32(Document_Number.Rows[0][0]);
                int incrValue = Convert.ToInt32(Count);
                incrValue += 1;
                txtDocumentNumber.Text = incrValue.ToString();
            }
        }
        public void printhtml()
        {
            try
            {
                string strclinicname = "", strphone = "", DlNumber = "", DlNumber2 = "", website = "", path = "";
                decimal totalamount = 0;
                string str_druglicenseno = "";
                string str_taxno = "";
                System.Data.DataTable dtp = this.cntrl.Get_companydetails();
                DataTable print_settng = this.prnt_ctrl.Get_inventory_id();
                if (dtp.Rows.Count > 0)
                {
                    string clinicn = "";
                    //clinicn = dtp.Rows[0]["name"].ToString();
                    if (print_settng.Rows.Count>0)
                    {
                        clinicn = print_settng.Rows[0]["header"].ToString();
                        strclinicname = clinicn.Replace("¤", "'");
                        strphone = print_settng.Rows[0]["right_text"].ToString();
                        DlNumber = print_settng.Rows[0]["left_text"].ToString();
                    }
                    DlNumber2 = dtp.Rows[0]["email"].ToString();
                    website = dtp.Rows[0]["website"].ToString();
                    str_druglicenseno = dtp.Rows[0]["Dl_Number"].ToString();
                    str_taxno = dtp.Rows[0]["Dl_Number2"].ToString();
                    path = dtp.Rows[0]["path"].ToString();
                    logo_name = path;
                }
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                StreamWriter sWrite = new StreamWriter(Apppath + "\\sales_Invoice.html");
                sWrite.WriteLine("<html>");
                sWrite.WriteLine("<head>");
                sWrite.WriteLine("<style>");
                sWrite.WriteLine("table { border-collapse: collapse;}");
                sWrite.WriteLine("p.big {line-height: 400%;}");
                sWrite.WriteLine("</style>");
                sWrite.WriteLine("</head>");
                sWrite.WriteLine("<body >");
                sWrite.WriteLine("<div>");
                sWrite.WriteLine("<table align=center width='100%'>");
                sWrite.WriteLine("<col >");
                sWrite.WriteLine("<br>");
                string Appath = System.IO.Directory.GetCurrentDirectory();
                if (File.Exists(Appath + "\\" + logo_name))
                {
                    sWrite.WriteLine("<table align='center' style='width:100%;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td width='4.29%' height='50px' align='left' rowspan='3'><img src='" + Appath + "\\" + logo_name + "'style='width:70px;height:70px;' ></td>  ");//30px
                    sWrite.WriteLine("<td width='100%' align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=4><b>" + strclinicname + "</font> <br><FONT  COLOR=black  face='Segoe UI' SIZE=2>&nbsp;" + DlNumber + "<br>&nbsp;" + strphone + " </b></td></tr>");//820px
                    sWrite.WriteLine("</table>");
                }
                else
                {
                    sWrite.WriteLine("<table align='center' style='width:100%;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' height='20px'><FONT  COLOR=black  face='Segoe UI' SIZE=3><b>" + strclinicname + "</b></font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' height='20px'><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + DlNumber + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' height='20px' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>" + strphone + "</font></td></tr>");
                    sWrite.WriteLine("</table>");
                }
                sWrite.WriteLine("<table width='100%' align='center' style='width:100%;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr><td align='left' width='100%'><hr/></td></tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<th ><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3> SALES INVOICE  </font></center></b></td>");
                sWrite.WriteLine("</tr>");

                sWrite.WriteLine("<table width='100%' align='center' style='width:100%;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan='1' align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2>Drug License:" + str_druglicenseno + "</font></left></td>");
                sWrite.WriteLine("<td colspan='1' align='right'>  <FONT COLOR=black FACE='Segoe UI'   SIZE=2>Tax No:&nbsp;" + str_taxno + " &nbsp;</font></right></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan='1' align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2> Sold To : &nbsp" + txtPatient.Text + "</font></left></td>");
                sWrite.WriteLine("<td colspan='1' align='right'><FONT COLOR=black FACE='Segoe UI'   SIZE=2>Invoice No:&nbsp;" + txtDocumentNumber.Text + "&nbsp; </font></right></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan='1' align='left'><FONT COLOR=black FACE='Segoe UI'   SIZE=2> Prescribed by : Dr." + txtBdoctor.Text + "</font></left></td>");
                sWrite.WriteLine("<td colspan='1' align='right'><FONT COLOR=black FACE='Segoe UI'   SIZE=2> Date : " + dtpDocumentDate.Value.ToString("dd-MM-yyyy") + "</font></right></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");

                if (rad_CashSale.Checked == true)
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td width='100%' align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2> Mode of payment: &nbsp" + Cmb_ModeOfPaymnt.Text + "</font></left></td>");
                    sWrite.WriteLine("</tr>");
                }
                else
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td width='100%' align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2> Mode of payment: &nbsp Credit Sale</font></left></td>");
                    sWrite.WriteLine("</tr>");
                   
                }

                sWrite.WriteLine("<tr><td colspan='100%'><hr/></td></tr>");

                sWrite.WriteLine("<table width='100%' align='center' style='width:100%;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='left'  width='20'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3><b>Sl</b></font></td>");
                sWrite.WriteLine("<td align='left'  width='180'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3><b>Item</b></font></td>");
                sWrite.WriteLine("<td align='left'  width='130'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>Batch</b></font></td>");
                sWrite.WriteLine("<td align='left'  width='90'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3><b>Expiry</b></font></td>");
                sWrite.WriteLine("<td align='left'  width='70'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3><b>Unit</b></font></td>");
                sWrite.WriteLine("<td align='left'  width='80'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3><b>Qty</b></font></td>");
                sWrite.WriteLine("<td align='left'  width='100'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3><b>Cost</b></font></td>");
                sWrite.WriteLine("<td align='left'  width='50'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3><b>GST%</b></font></td>");
                sWrite.WriteLine("<td align='right'  width='100'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3><b>Amount</b></font></td>");

                //sWrite.WriteLine("<td align='left'  colspan='1'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>Sl.</b></font></td>");
                //sWrite.WriteLine("<td align='left'  ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>Item</b></font></td>");
                ////sWrite.WriteLine("<td align='left'  colspan='1'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>Batch/Expiry</b></font></td>");
                ////sWrite.WriteLine("<td align='right'  colspan='1' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>HSN Number</b></font></td>");
                //sWrite.WriteLine("<td align='right'  colspan='1' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>Unit</b></font></td>");
                //sWrite.WriteLine("<td align='right'  colspan='1'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>Qty</b></font></td>");
                //sWrite.WriteLine("<td align='right'  colspan='1'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>Cost</b></font></td>");
                ////sWrite.WriteLine("<td align='right'  colspan='1'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>Taxable Amount</b></font></td>");
                //sWrite.WriteLine("<td align='right'  colspan='1'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>VAT%</b></font></td>");
                ////sWrite.WriteLine("<td align='right'  colspan='1'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>CGST</b></font></td>");
                ////sWrite.WriteLine("<td align='right'  colspan='1'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>SGST</b></font></td>");
                //sWrite.WriteLine("<td align='right'  colspan='1'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>Amount</b></font></td>");
                //sWrite.WriteLine("</tr>");
                //sWrite.WriteLine("</table>");
                sWrite.WriteLine("<tr><td align='left' colspan='100%'><hr/></td></tr>");
                string removecomma = "",remoexp="";

                int k = 1; decimal tax_amt = 0, total_cgst = 0, total_sgst = 0;
                for (int i = 0; i < dgv_SalesItem.Rows.Count; i++)
                {
                    decimal gst_Amount = 0, Amount = 0, cgst = 0;
                    //sWrite.WriteLine("<table width='100%' align='center' style='width:100%;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + k + "</font></td>");
                    sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + dgv_SalesItem.Rows[i].Cells["colDiscription"].Value.ToString() + "</font></td>");
                    string strBatch = "",strexp="",expirydate="";
                    string mon="" ;
                    for (int ii = 0; ii < dgv_BatchSale.Rows.Count; ii++)
                    {
                        if (dgv_BatchSale.Rows[ii].Cells["coiltem_code"].Value.ToString() == dgv_SalesItem.Rows[i].Cells["id"].Value.ToString())
                        {
                            strBatch = strBatch + dgv_BatchSale.Rows[ii].Cells["colBatchnumber"].Value.ToString() + ",";
                            if (dgv_BatchSale.Rows[ii].Cells["expdate"].Value != null && dgv_BatchSale.Rows[ii].Cells["expdate"].Value.ToString() != "")
                            {
                                strexp = strexp+ dgv_BatchSale.Rows[ii].Cells["expdate"].Value.ToString().TrimEnd() + ",";//).ToString("dd-MM-yyyy").Trim();
                                //mon = Convert.ToDateTime(strexp);
                                //string ed = mon.Month.ToString();//ToString("MM-yy");
                                //string ye = mon.Year.ToString();
                                //expirydate = expirydate + ed + "/" + ye+",";
                            }
                            else
                            {
                                //strBatch = strBatch + dgv_BatchSale.Rows[ii].Cells["colBatchnumber"].Value.ToString() + ",";
                            }
                        }
                    }
                    if (strBatch != "")
                        removecomma = strBatch.Remove(strBatch.Length - 1);     
                    if (strexp != "")
                    {
                        remoexp = strexp.Remove(strexp.Length - 1);
                        mon = Convert.ToDateTime(remoexp).ToString("dd-MM-yyyy");
                        //string ed = mon.Month.ToString();//ToString("MM-yy");
                        //string ye = mon.Year.ToString();
                        //expirydate = ed + "/" + ye;
                    }
                       
                    sWrite.WriteLine("<td align='left' colspan='1'><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + removecomma + "</font></td>");
                    sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + mon + "</font></td>");
                    sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + dgv_SalesItem.Rows[i].Cells["ColUnit"].Value.ToString() + "</font></td>");
                    sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + dgv_SalesItem.Rows[i].Cells["ColQty"].Value.ToString() + "</font></td>");
                    sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + Convert.ToDecimal(dgv_SalesItem.Rows[i].Cells["colUnitcost"].Value.ToString()).ToString("##0.00") + "</font></td>");
                    Amount = Convert.ToDecimal(dgv_SalesItem.Rows[i].Cells["ColQty"].Value.ToString()) * Convert.ToDecimal(dgv_SalesItem.Rows[i].Cells["colUnitcost"].Value.ToString());

                    sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + Convert.ToDecimal(dgv_SalesItem.Rows[i].Cells["ColGST"].Value.ToString()) + "</font></td>");
                    cgst = Convert.ToDecimal(dgv_SalesItem.Rows[i].Cells["ColGST"].Value.ToString()) / 2;
                    gst_Amount = (Amount * cgst) / 100;
                    tax_amt = Amount + gst_Amount + gst_Amount;
                    sWrite.WriteLine("<td align='right'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + tax_amt.ToString("##0.00") + "</font></td>");

                    totalamount = totalamount + tax_amt;//   totalamount + Convert.ToDecimal(dgv_SalesItem.Rows[i].Cells["colAmount"].Value.ToString());
                    total_cgst = total_cgst + gst_Amount;
                    k = k + 1;

                  
                    sWrite.WriteLine("</tr>");

                }
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<table width='100%' align='center' style='width:100%;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr><td align='left'  colspan='100%'><hr/></td></tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan='1' align='right'><FONT COLOR=black FACE='Segoe UI'   SIZE=2> Total Items : " + txt_totalItems.Text + "</font></right></td>");

                sWrite.WriteLine("</tr>");
                if (txt_SGST.Text != "")
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan='1' align='right'><FONT COLOR=black FACE='Segoe UI'   SIZE=2> CGST : " + total_cgst.ToString("##0.00") + "</font></right></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan='1' align='right'><FONT COLOR=black FACE='Segoe UI'   SIZE=2> SGST : " + total_cgst.ToString("##0.00") + "</font></right></td>");
                    sWrite.WriteLine("</tr>");
                }
                //else if (Txt_TotalIGST.Text != "")
                //{
                //    sWrite.WriteLine("<tr>");
                //    sWrite.WriteLine("<td colspan='1' align='right'><FONT COLOR=black FACE='Segoe UI'   SIZE=2> Total IGST : " + Txt_TotalIGST.Text + "</font></right></td>");
                //    sWrite.WriteLine("</tr>");
                //}
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan='1' align='right'><FONT COLOR=black FACE='Segoe UI'   SIZE=2> Total Amount : " + String.Format("{0:C}", Convert.ToDecimal(Txt_TotalAmount.Text))  + "</font></right></td>");//decimal.Parse(Txt_TotalAmount.Text).ToString("##0.000")
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan='1' align='right'><FONT COLOR=black FACE='Segoe UI'   SIZE=2> Discount Amount: " + String.Format("{0:C}", Convert.ToDecimal(txt_DiscAmount.Text))  + "</font></right></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan='1' align='right'><FONT COLOR=black FACE='Segoe UI'   SIZE=2><b> Grand Total :  " + String.Format("{0:C}", Convert.ToDecimal(txt_GrandTotal.Text))+ "</b></font></right></td>");// decimal.Parse(txt_GrandTotal.Text).ToString("##0.000")
                //sWrite.WriteLine(" <div class='footer'>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan='1' align='right'><FONT COLOR=black FACE='Segoe UI'   SIZE=2><b> Round Off : " + Math.Round(decimal.Parse(txt_GrandTotal.Text)).ToString("##0.00") + "</b></font></right></td>");
                sWrite.WriteLine(" <div class='footer'>");
                sWrite.WriteLine("</tr>");

                sWrite.WriteLine("<tr><td align='left' width='100%' > <FONT COLOR=black FACE='Segoe UI' SIZE=2>Pharmacist Signature</font></td></tr>");
                sWrite.WriteLine("<tr><td align='left'  width='100%'><hr/></td></tr>");
                sWrite.WriteLine("<tr><td align='left' width='100%' > <FONT COLOR=black FACE='Segoe UI' SIZE=1><i>Goods once sold cannot be taken back or exchanged</i></font></td></tr>");
                sWrite.WriteLine("</div>");
                sWrite.WriteLine("<tr><td align='left' width='100%' > <FONT COLOR=black FACE='Segoe UI' SIZE=2>E&OE</font></td></tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<script>window.print();</script>");
                sWrite.WriteLine("</body >");
                sWrite.WriteLine("</html>");
                sWrite.Close();
                System.Diagnostics.Process.Start(Apppath + "\\sales_Invoice.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Printer not ready...." + ex.Message, "Printer error.. ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
     
        private void txtPatientID_TextChanged(object sender, EventArgs e)
        {
            if (txtPatient.Text != "")
            {
                string id = txtPatientID.Text;
                DataTable dtb = this.cntrl.patients(id);
                fill_patientdetails(dtb);
            }
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Do you want to print?", "Question ?",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                // printhtml();
                printhtml_thermal();
            }
        }
        private void BTN_CLEAR_Click(object sender, EventArgs e)
        {
            //if(dgv_BatchSale.Rows.Count>0)
            //{
            //    for(int i=0; i<dgv_BatchSale.Rows.Count;i++)
            //    {
            //        this.cntrl.update_default_qty(dgv_BatchSale.Rows[i].Cells["colBatchEntry"].Value.ToString());
            //    }
            //}
            prescrption_sale = false;
            clear_itemdetails();
            clear_All_Data();//btn_item_Choose.Enabled = true;
            dgvShelfNo.Rows.Clear(); //txtBdoctor.Text = lbIdoctor.Items[0].ToString();
        }
        private void txt_Discount_TextAlignChanged(object sender, EventArgs e)
        {
        }
        private void txt_Discount_Leave(object sender, EventArgs e)
        {
            if (txt_Discount.Text == "")
            {
                txt_Discount.Text = "0";
            }
        }
        private void txt_Discount_Click(object sender, EventArgs e)
        {
            if (txt_Discount.Text == "0")
            {
                txt_Discount.Text = "";
            }
        }
        private void txt_Discount_KeyUp(object sender, KeyEventArgs e)
        {
            decimal totaldis = 0, totalamount = 0, totaldisper = 0;
            decimal d;
            if (decimal.TryParse(txt_DiscAmount.Text, out d))
            {
                if(cmb_disc.Text=="INR")
                {
                    if (decimal.TryParse(txt_Discount.Text, out d))
                    {
                        totalamount = Convert.ToDecimal(Txt_TotalAmount.Text);
                        totaldisper = Convert.ToDecimal(txt_Discount.Text);
                        totaldis = totalamount - totaldisper;// Convert.ToDecimal((totalamount * totaldisper) / 100);
                        txt_DiscAmount.Text = Convert.ToDecimal(totaldis).ToString("##.00");
                        txt_GrandTotal.Text = Convert.ToDecimal(totaldis).ToString("##.00");// Convert.ToDecimal(Convert.ToDecimal(Txt_TotalAmount.Text) - Convert.ToDecimal(totaldis)).ToString("##.00");
                    }
                    else
                    {
                        txt_DiscAmount.Text = "00.00";
                        txt_GrandTotal.Text = Txt_TotalAmount.Text;
                    }
                }
                else
                {
                    if (decimal.TryParse(txt_Discount.Text, out d))
                    {
                        totalamount = Convert.ToDecimal(Txt_TotalAmount.Text);
                        totaldisper = Convert.ToDecimal(txt_Discount.Text);
                        totaldis = Convert.ToDecimal((totalamount * totaldisper) / 100);
                        txt_DiscAmount.Text = Convert.ToDecimal(totaldis).ToString("##.00");
                        txt_GrandTotal.Text = Convert.ToDecimal(Convert.ToDecimal(Txt_TotalAmount.Text) - Convert.ToDecimal(totaldis)).ToString("##.00");
                    }
                    else
                    {
                        txt_DiscAmount.Text = "00.00";
                        txt_GrandTotal.Text = Txt_TotalAmount.Text;
                    }
                }
                
            }
        }

        private void rad_CashSale_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_CashSale.Checked == true)
            {
                rad_CreditSale.Checked = false;
                label22.Visible = true;
                ////Lab_CardNo.Visible = true;
                //Lab_Last4Digit.Visible = true;
                //Lab_Numbr.Visible = true;
                //txt_4Digit.Visible = true;
                //txt_Number.Visible = true;
                //txt_BankNAme.Visible = true;

                Cmb_ModeOfPaymnt.Visible = true;
                //panl_mode_payment.Visible = true;
            }
        }

        private void rad_CreditSale_CheckedChanged(object sender, EventArgs e)
        {
            //rad_CreditSale.Checked = true;
            if (rad_CreditSale.Checked == true)
            {
                rad_CashSale.Checked = false;
                //panl_mode_payment.Visible = false;
                label22.Visible = false;
                Lab_CardNo.Visible = false;
                Lab_Last4Digit.Visible = false;
                Lab_Numbr.Visible = false;
                txt_4Digit.Visible = false;
                txt_Number.Visible = false;
                txt_BankNAme.Visible = false;

                Cmb_ModeOfPaymnt.Visible = false;
            }
        }

        private void Cmb_ModeOfPaymnt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb_ModeOfPaymnt.SelectedIndex == 0)//cash
            {
                payment_show(false, false, false, false, false, false, false);
                //payment_show(true, true, true, true, false, false, false);
            }
            else if (Cmb_ModeOfPaymnt.SelectedIndex == 1)//cheque
            {
                payment_show(true, true, true, true, false, false, false);
                //payment_show(true, false, false, false, true, true, true);
            }
            else if (Cmb_ModeOfPaymnt.SelectedIndex == 2)//card
            {
                payment_show(true, false, false, false, true, true, true);
                //payment_show(true, true, true, true, false, false, false);
            }
            else if (Cmb_ModeOfPaymnt.SelectedIndex == 3)//dd
            {
                payment_show(true, true, true, true, false, false, false);
            }
            else
            { payment_show(false, false, false, false, false, false, false); }
        }
        public void payment_show(Boolean BankName, Boolean Number, Boolean bank, Boolean lab_number, Boolean last4digit, Boolean cardno, Boolean t4digit)
        {
            txt_BankNAme.Visible = BankName;
            txt_Number.Visible = Number;
            Bank.Visible = bank;
            Lab_Numbr.Visible = lab_number;
            Lab_Last4Digit.Visible = last4digit;
            Lab_CardNo.Visible = cardno;
            txt_4Digit.Visible = t4digit;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txt_4Digit.Text = "";
            txt_BankNAme.Text = "";
            txt_Number.Text = "";
            //panl_mode_payment.Visible = false;
            rad_CreditSale.Checked = false;
            rad_CashSale.Checked = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //panl_mode_payment.Visible = false;
        }

        private void txt_ItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string item = txt_ItemCode.Text;
                if (item != "")
                {
                    FormName = "Sales";
                    var form2 = new ItemListForSales(FormName, txt_ItemCode.Text);
                    form2.ShowDialog();
                    form2.Dispose();
                    if (ItemCode_From_List != "")
                    {
                        DataTable dtb = this.cntrl.get_itemdetails(itemId);
                        Load_itemdetails(dtb);
                    }
                }
            }
        }

        private void txt_Discription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string item = txt_Discription.Text;
                if (item != "")
                {
                    //FormName = "Sales";
                    //var form2 = new ItemListForSales(FormName, txt_Discription.Text);
                    //form2.ShowDialog();
                    //form2.Dispose();

                    //if (ItemCode_From_List != "")
                    //{
                    DataTable dtb = this.cntrl.Get_itemdetails_itemname(item);
                    if (dtb.Rows.Count > 0)
                    {
                        itemId = dtb.Rows[0]["id"].ToString();
                        txt_ItemCode.Text = dtb.Rows[0]["Item_Code"].ToString();
                        txt_Discription.Text = dtb.Rows[0]["item_name"].ToString();
                        Load_itemdetails(dtb);
                    }
                }
                else
                {
                    btnSave.Select();
                    e.SuppressKeyPress = true;
            }
                }
        }

        private void btnCloseShelfpnl_Click(object sender, EventArgs e)
        {
            pnlShelf.Hide();
        }

        private void btnShelfNo_Click(object sender, EventArgs e)
        {
            pnlShelf.Show();
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string item = txtBarcode.Text;
                if (item != "")
                {
                    DataTable dtbt = this.cntrl.get_itemdetails_BArcode(item);
                    if (dtbt.Rows.Count > 0)
                    {
                        FormName = "Sales";
                        itemId = dtbt.Rows[0]["id"].ToString();
                        {
                            DataTable dtb = this.cntrl.get_itemdetails(itemId);
                            //if (itemcheck() == 0)
                            //{

                            //}
                                Load_itemdetails(dtb);
                            cmb_Unit.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Item doesn't have stock..!", "No Stock", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    txt_Discription.Focus();
                }
                e.SuppressKeyPress = true;
            }
        }

        private void cmb_Unit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Qty.Text = "1";
                txt_Qty.Focus();
            }
        }

        private void txt_Qty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_AddtoGrid.Select();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlShelf_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt_Discription_TextChanged(object sender, EventArgs e)
        {
            if (editflag != true) 
            {
                if (txt_Discription.Text != "" && txtBarcode.Text == "")
                {
                    DataTable dtb = this.cntrl.Search_itemdetails_itemname(txt_Discription.Text);
                    if (dtb.Rows.Count > 0)
                    {
                        lst_Itemname.DisplayMember = "item_name";
                        lst_Itemname.ValueMember = "item_code";
                        lst_Itemname.DataSource = dtb;
                        lst_Itemname.Show();
                    }
                }
                else
                {
                    lst_Itemname.Visible = false;
                }
            }
        }

        private void lst_Itemname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //flagSup = true;
                    txt_Discription.Text = lst_Itemname.Text;
                    txt_ItemCode.Text = lst_Itemname.SelectedValue.ToString();
                    lst_Itemname.Hide();
                    string name = txt_Discription.Text;
                    if (name != "")
                    {
                        DataTable dtb = this.cntrl.Get_itemdetails_itemname(name);
                        if (dtb.Rows.Count > 0)
                        {
                            itemId = dtb.Rows[0]["id"].ToString();
                            txt_ItemCode.Text = dtb.Rows[0]["Item_Code"].ToString();
                            txt_Discription.Text = dtb.Rows[0]["item_name"].ToString();
                            Load_itemdetails(dtb);
                        }
                    }
                    cmb_Unit.Focus();
                    e.SuppressKeyPress = true;
                }
                else if (e.KeyCode == Keys.Up)
                {
                    lst_Itemname.Focus();
                    int indicee = lst_Itemname.SelectedIndex;
                    indicee++;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    lst_Itemname.Focus();
                    int indicee = lst_Itemname.SelectedIndex;
                    indicee++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_Discription_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lst_Itemname.Visible != false)
                {
                    lst_Itemname.Focus();
                }
            }
        }

        private void lst_Itemname_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                //flagSup = true;
                txt_Discription.Text = lst_Itemname.Text;
                txt_ItemCode.Text = lst_Itemname.SelectedValue.ToString();
                lst_Itemname.Hide();
                string name = txt_Discription.Text;
                if (name != "")
                {
                    DataTable dtb = this.cntrl.Get_itemdetails_itemname(name);
                    if (dtb.Rows.Count > 0)
                    {
                        itemId = dtb.Rows[0]["id"].ToString();
                        txt_ItemCode.Text = dtb.Rows[0]["Item_Code"].ToString();
                        txt_Discription.Text = dtb.Rows[0]["item_name"].ToString();
                        Load_itemdetails(dtb);
                    }
                }
                cmb_Unit.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAmntRcvd_TextChanged(object sender, EventArgs e)
        {
            decimal amtrecvd = 0, balance = 0, grandTotal=0;
            decimal d;
            if (txtAmntRcvd.Text != "0.00" && txtAmntRcvd.Text != "")
            {
                if (txt_GrandTotal.Text != "0.00")
                {
                    amtrecvd = Convert.ToDecimal(txtAmntRcvd.Text);
                    grandTotal = Convert.ToDecimal(txt_GrandTotal.Text);
                    balance = Convert.ToDecimal((amtrecvd - grandTotal));
                    txtBalncToGve.Text = String.Format("{0:C}", Convert.ToDecimal(balance));
// Convert.ToDecimal(balance).ToString("##.00");
                    //txt_GrandTotal.Text = Convert.ToDecimal(Convert.ToDecimal(Txt_TotalAmount.Text) - Convert.ToDecimal(totaldis)).ToString("##.000");
                }
                else
                {
                    txtBalncToGve.Text = "0.00";
                    //txt_GrandTotal.Text = Txt_TotalAmount.Text;
                }
            }
            else
            {
                txtBalncToGve.Text = "0.00";
            }
        }

        private void txtAmntRcvd_Click(object sender, EventArgs e)
        {
            txtAmntRcvd.Text = "";
        }

        private void lbPatient_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgv_BatchSale_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if(dgv_BatchSale.Rows.Count>1)
            {
               if( dtFor_CurrentStockUpdate !=null)
                {

                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textdisc_KeyUp(object sender, KeyEventArgs e)
        {
            TotalAmount_Calculation();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (rad_CashSale.Checked == true)
            {
                //panl_mode_payment.Visible = true;
                Payment_method = Cmb_ModeOfPaymnt.Text;

            }
            else if (rad_CreditSale.Checked == true)
            {
                //panl_mode_payment.Visible = false;
                Payment_method = "Credit Sale"; 
            }
            if (string.IsNullOrWhiteSpace(txtPatient.Text))
            {
                txtPatient.Text = ".";
                txtPatient.Focus();
            }
            if (string.IsNullOrWhiteSpace(txtBdoctor.Text))
            {
                MessageBox.Show("Doctor name could not be found....", "Data Required..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBdoctor.Focus();
                return;
            }
            if (dgv_SalesItem.Rows.Count == 0)
            {
                MessageBox.Show("Products not found....", "Data Required..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBarcode.Focus();
                return;
            }
            if (salesOrder_flag == true)
            {

                //if (Check_OrderItems() != 0)
                //{
                //    MessageBox.Show("Products not found....", "Data Required..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txt_ItemCode.Focus();
                //    return;
                //}
                if (dgv_BatchSale.Rows.Count == 0)
                {
                    MessageBox.Show("Batch not found....,Please add batch", "Data Required..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //txt_ItemCode.Focus();
                    return;
                }
            }
            decimal adv_amt = 0, new_adv = 0, adv = 0;

            string cs = PappyjoeMVC.Model.Connection.MyGlobals.trans_connectionstring;
            using (MySqlConnection con = new MySqlConnection(cs))
            {
                con.Open();
                MySqlTransaction trans = con.BeginTransaction();
                if (Convert.ToDecimal(lblAdvance.Text) > 0)
                {
                    DialogResult res = MessageBox.Show("Do you want to pay from the advance payment...?", "",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        if (Convert.ToDecimal(txt_GrandTotal.Text) > Convert.ToDecimal(lblAdvance.Text))
                        {
                            adv_amt = Convert.ToDecimal(txt_GrandTotal.Text) - Convert.ToDecimal(lblAdvance.Text);
                            new_adv = 0; adv = Convert.ToDecimal(lblAdvance.Text);
                        }
                        else
                        {
                            adv_amt = Convert.ToDecimal(lblAdvance.Text) - Convert.ToDecimal(txt_GrandTotal.Text);
                            new_adv = adv_amt;
                            adv = Convert.ToDecimal(txt_GrandTotal.Text);

                        }
                        this.cntrl.update_advance(new_adv, patient_id, con, trans);
                        this.cntrl.Save_advancetable(patient_id, DateTime.Now.Date.ToString("yyyy-MM-dd"), adv.ToString(), "CASH", "credit", "Pharmarcy", con, trans);

                    }
                    else
                    {
                    }
                }
                Cgst = Convert.ToDecimal(txt_CGST.Text);
                Sgst = Convert.ToDecimal(txt_SGST.Text);
                GST = Convert.ToDecimal(Cgst + Sgst);
                int i = 0, j = 0;
                string invnumber = "";
                try
                {
                    DataTable Document_Number = this.cntrl._trans_docnumber(con, trans);
                    if (String.IsNullOrWhiteSpace(Document_Number.Rows[0][0].ToString()))
                    {
                        invnumber = "1";
                    }
                    else
                    {
                        int Count = Convert.ToInt32(Document_Number.Rows[0][0]);
                        int incrValue = Convert.ToInt32(Count);
                        incrValue += 1;
                        invnumber = incrValue.ToString();
                    }
                    if (btnSave.Text == "SAVE")
                    {

                        dgvShelfNo.Rows.Clear();
                        //if (Cmb_ModeOfPaymnt.SelectedIndex == 0)
                        //{
                        //    i = this.cntrl.Save_salesMaster_cheque(Convert.ToInt32(txtDocumentNumber.Text), dtpDocumentDate.Value.ToString("yyyy-MM-dd"), txtSales.Text, txt_OrderNo.Text, DTP_OrderDate.Value.ToString("yyyy-MM-dd"), txtBdoctor.Text, txt_LRNO.Text, DTP_LRDate.Value.ToString("yyyy-MM-dd"), txt_Through.Text, txtPatientID.Text, txtPatient.Text, txt_Street.Text, txt_Locality.Text, txt_City.Text, txt_PhoneNo.Text, Payment_method, txt_BankNAme.Text, txt_Number.Text, Convert.ToDecimal(Txt_TotalAmount.Text), Convert.ToDecimal(txt_Discount.Text), GST, Convert.ToDecimal(Txt_TotalIGST.Text), Convert.ToDecimal(txt_GrandTotal.Text), type);
                        //}
                        //else if (Cmb_ModeOfPaymnt.SelectedIndex == 1)
                        //{
                        //    i = this.cntrl.Save_salesMaster_card(Convert.ToInt32(txtDocumentNumber.Text), dtpDocumentDate.Value.ToString("yyyy-MM-dd"), txtSales.Text, txt_OrderNo.Text, DTP_OrderDate.Value.ToString("yyyy-MM-dd"), txtBdoctor.Text, txt_LRNO.Text, DTP_LRDate.Value.ToString("yyyy-MM-dd"), txt_Through.Text, txtPatientID.Text, txtPatient.Text, txt_Street.Text, txt_Locality.Text, txt_City.Text, txt_PhoneNo.Text, Payment_method, txt_BankNAme.Text, txt_4Digit.Text, Convert.ToDecimal(Txt_TotalAmount.Text), Convert.ToDecimal(txt_Discount.Text), GST, Convert.ToDecimal(Txt_TotalIGST.Text), Convert.ToDecimal(txt_GrandTotal.Text), type);
                        //}
                        //else if (Cmb_ModeOfPaymnt.SelectedIndex == 2)
                        //{
                        //    i = this.cntrl.Save_salesMaster_DD(Convert.ToInt32(txtDocumentNumber.Text), dtpDocumentDate.Value.ToString("yyyy-MM-dd"), txtSales.Text, txt_OrderNo.Text, DTP_OrderDate.Value.ToString("yyyy-MM-dd"), txtBdoctor.Text, txt_LRNO.Text, DTP_LRDate.Value.ToString("yyyy-MM-dd"), txt_Through.Text, txtPatientID.Text, txtPatient.Text, txt_Street.Text, txt_Locality.Text, txt_City.Text, txt_PhoneNo.Text, Payment_method, txt_BankNAme.Text, txt_Number.Text, Convert.ToDecimal(Txt_TotalAmount.Text), Convert.ToDecimal(txt_Discount.Text), GST, Convert.ToDecimal(Txt_TotalIGST.Text), Convert.ToDecimal(txt_GrandTotal.Text), type);
                        //}
                        //else
                        //{
                        i = this.cntrl.Save_salesMaster(Convert.ToInt32(txtDocumentNumber.Text), dtpDocumentDate.Value.ToString("yyyy-MM-dd"), txtSales.Text, txt_OrderNo.Text, DTP_OrderDate.Value.ToString("yyyy-MM-dd"), txtBdoctor.Text, txt_LRNO.Text, DTP_LRDate.Value.ToString("yyyy-MM-dd"), txt_Through.Text, txtPatientID.Text, txtPatient.Text, txt_Street.Text, txt_Locality.Text, txt_City.Text, txt_PhoneNo.Text, Payment_method, Convert.ToDecimal(Txt_TotalAmount.Text), Convert.ToDecimal(txt_Discount.Text), GST, Convert.ToDecimal(Txt_TotalIGST.Text), Convert.ToDecimal(txt_GrandTotal.Text), type,  con, trans);//cmb_disc.Text, txt_DiscAmount.Text,
                        //}
                        //string dt = DateTime.Now.Date.ToString("yyyy-MM-dd");
                        //DateTime Timeonly = DateTime.Now;
                        //this.cntrl.save_log(doctor_id, "Sales", " Adds Sales", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Add");
                    }
                    else if (btnSave.Text == "UPDATE")
                    {
                        dgvShelfNo.Rows.Clear();
                        //if (Cmb_ModeOfPaymnt.SelectedIndex == 0)
                        //{
                        //    i = this.cntrl.update_salesMaster_cheque(Convert.ToInt32(txtDocumentNumber.Text), dtpDocumentDate.Value.ToString("yyyy-MM-dd"), txtSales.Text, txt_OrderNo.Text, DTP_OrderDate.Value.ToString("yyyy-MM-dd"), txtBdoctor.Text, txt_LRNO.Text, DTP_LRDate.Value.ToString("yyyy-MM-dd"), txt_Through.Text, txtPatientID.Text, txtPatient.Text, txt_Street.Text, txt_Locality.Text, txt_City.Text, txt_PhoneNo.Text, Payment_method, txt_BankNAme.Text, txt_Number.Text, Convert.ToDecimal(Txt_TotalAmount.Text), Convert.ToDecimal(txt_Discount.Text), GST, Convert.ToDecimal(Txt_TotalIGST.Text), Convert.ToDecimal(txt_GrandTotal.Text), type);
                        //}
                        //else if (Cmb_ModeOfPaymnt.SelectedIndex == 1)
                        //{
                        //    i = this.cntrl.update_salesMaster_card(Convert.ToInt32(txtDocumentNumber.Text), dtpDocumentDate.Value.ToString("yyyy-MM-dd"), txtSales.Text, txt_OrderNo.Text, DTP_OrderDate.Value.ToString("yyyy-MM-dd"), txtBdoctor.Text, txt_LRNO.Text, DTP_LRDate.Value.ToString("yyyy-MM-dd"), txt_Through.Text, txtPatientID.Text, txtPatient.Text, txt_Street.Text, txt_Locality.Text, txt_City.Text, txt_PhoneNo.Text, Payment_method, txt_BankNAme.Text, txt_4Digit.Text, Convert.ToDecimal(Txt_TotalAmount.Text), Convert.ToDecimal(txt_Discount.Text), GST, Convert.ToDecimal(Txt_TotalIGST.Text), Convert.ToDecimal(txt_GrandTotal.Text), type);
                        //}
                        //else if (Cmb_ModeOfPaymnt.SelectedIndex == 2)
                        //{
                        //    i = this.cntrl.update_salesMaster_DD(Convert.ToInt32(txtDocumentNumber.Text), dtpDocumentDate.Value.ToString("yyyy-MM-dd"), txtSales.Text, txt_OrderNo.Text, DTP_OrderDate.Value.ToString("yyyy-MM-dd"), txtBdoctor.Text, txt_LRNO.Text, DTP_LRDate.Value.ToString("yyyy-MM-dd"), txt_Through.Text, txtPatientID.Text, txtPatient.Text, txt_Street.Text, txt_Locality.Text, txt_City.Text, txt_PhoneNo.Text, Payment_method, txt_BankNAme.Text, txt_Number.Text, Convert.ToDecimal(Txt_TotalAmount.Text), Convert.ToDecimal(txt_Discount.Text), GST, Convert.ToDecimal(Txt_TotalIGST.Text), Convert.ToDecimal(txt_GrandTotal.Text), type);
                        //}
                        //else
                        //{
                        i = this.cntrl.update_salesMaster(Convert.ToInt32(txtDocumentNumber.Text), dtpDocumentDate.Value.ToString("yyyy-MM-dd"), txtSales.Text, txt_OrderNo.Text, DTP_OrderDate.Value.ToString("yyyy-MM-dd"), txtBdoctor.Text, txt_LRNO.Text, DTP_LRDate.Value.ToString("yyyy-MM-dd"), txt_Through.Text, txtPatientID.Text, txtPatient.Text, txt_Street.Text, txt_Locality.Text, txt_City.Text, txt_PhoneNo.Text, Payment_method, Convert.ToDecimal(Txt_TotalAmount.Text), Convert.ToDecimal(txt_Discount.Text), GST, Convert.ToDecimal(Txt_TotalIGST.Text), Convert.ToDecimal(txt_GrandTotal.Text), type, con, trans);
                        //}
                        //    string dt = DateTime.Now.Date.ToString("yyyy-MM-dd");
                        //    DateTime Timeonly = DateTime.Now;
                        //    this.cntrl.save_log(doctor_id, "Sales", "logged User Edit Sales", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Edit");
                    }
                    if (i > 0)
                    {
                        string unit2;
                        decimal unitMf;
                        if (dgv_SalesItem.Rows.Count > 0)
                        {
                            foreach (DataGridViewRow dr in dgv_SalesItem.Rows)
                            {
                                DataTable dt_Unit2 = this.cntrl.get_costbase(dr.Cells["id"].Value.ToString(), con, trans);
                               if(dt_Unit2.Rows[0]["Unit2"].ToString()!="")
                                {
                                    if (dr.Cells["ColUnit"].Value.ToString() == dt_Unit2.Rows[0]["Unit2"].ToString())
                                    {
                                        unit2 = "Yes";
                                        unitMf = Convert.ToDecimal(dt_Unit2.Rows[0]["UnitMF"].ToString());
                                    }
                                    else
                                    {
                                        unit2 = "No";
                                        unitMf = Convert.ToDecimal(dt_Unit2.Rows[0]["UnitMF"].ToString());
                                    }
                                }
                               else
                                {
                                    unit2 = "No";
                                    unitMf = 0;
                                }
                                
                                if (btnSave.Text == "SAVE")
                                {
                                    dgvShelfNo.Rows.Clear();
                                    j = this.cntrl.Save_itemdetails(Convert.ToInt32(invnumber), dtpDocumentDate.Value.ToString("yyyy-MM-dd"), dr.Cells["id"].Value.ToString(), dr.Cells["colDiscription"].Value.ToString(), dr.Cells["ColPacking"].Value.ToString(),dr.Cells["batch"].Value.ToString(),dr.Cells["batchentry"].Value.ToString(), dr.Cells["ColUnit"].Value.ToString(), Convert.ToDecimal(dr.Cells["ColGST"].Value.ToString()),0, Convert.ToDecimal(dr.Cells["c_disc"].Value.ToString()), Convert.ToDecimal(dr.Cells["ColQty"].Value.ToString()), Convert.ToDecimal(dr.Cells["ColFree"].Value.ToString()), Convert.ToDecimal(dr.Cells["colUnitcost"].Value.ToString()), Convert.ToDecimal(dr.Cells["colAmount"].Value.ToString()), unit2, unitMf, Convert.ToDecimal(dt_Unit2.Rows[0]["CostBase"].ToString()), con, trans);
                                }
                                else if (btnSave.Text == "UPDATE")
                                {
                                    dgvShelfNo.Rows.Clear();
                                    //j = this.cntrl.update_itemdetails(Convert.ToInt32(txtDocumentNumber.Text), dtpDocumentDate.Value.ToString("yyyy-MM-dd"), dr.Cells["id"].Value.ToString(), dr.Cells["ColPacking"].Value.ToString(), dr.Cells["ColUnit"].Value.ToString(), Convert.ToDecimal(dr.Cells["ColGST"].Value.ToString()), 0, Convert.ToDecimal(dr.Cells["ColQty"].Value.ToString()), Convert.ToDecimal(dr.Cells["ColFree"].Value.ToString()), Convert.ToDecimal(dr.Cells["colUnitcost"].Value.ToString()), Convert.ToDecimal(dr.Cells["colAmount"].Value.ToString()), unit2, unitMf, Convert.ToDecimal(dt_Unit2.Rows[0]["CostBase"].ToString()), con, trans);
                                    j = this.cntrl.update_itemdetails(Convert.ToInt32(txtDocumentNumber.Text), dtpDocumentDate.Value.ToString("yyyy-MM-dd"), dr.Cells["id"].Value.ToString(), dr.Cells["ColPacking"].Value.ToString(), dr.Cells["batch"].Value.ToString(), dr.Cells["batchentry"].Value.ToString(), dr.Cells["ColUnit"].Value.ToString(), Convert.ToDecimal(dr.Cells["ColGST"].Value.ToString()), 0, Convert.ToDecimal(dr.Cells["c_disc"].Value.ToString()), Convert.ToDecimal(dr.Cells["ColQty"].Value.ToString()), Convert.ToDecimal(dr.Cells["ColFree"].Value.ToString()), Convert.ToDecimal(dr.Cells["colUnitcost"].Value.ToString()), Convert.ToDecimal(dr.Cells["colAmount"].Value.ToString()), unit2, unitMf, Convert.ToDecimal(dt_Unit2.Rows[0]["CostBase"].ToString()), con, trans);

                                }

                            }
                        }
                    }
                    if (i > 0 && j > 0)
                    {
                        if (dgv_BatchSale.Rows.Count > 0)
                        {
                            string unit2;
                            decimal unitMf;
                            foreach (DataGridViewRow dr in dgv_BatchSale.Rows)
                            {
                                decimal batchNumber_qty = 0;
                                DataTable dt_Unit2 = this.cntrl.get_costbase(dr.Cells["coiltem_code"].Value.ToString(), con, trans);
                                if (dt_Unit2.Rows[0]["Unit2"].ToString() != "")
                                {
                                    if (dr.Cells["unit"].Value.ToString() == dt_Unit2.Rows[0]["Unit2"].ToString())
                                    {
                                        unit2 = "Yes";
                                        unitMf = Convert.ToDecimal(dt_Unit2.Rows[0]["UnitMF"].ToString());
                                    }
                                    else
                                    {
                                        unit2 = "No";
                                        unitMf = Convert.ToDecimal(dt_Unit2.Rows[0]["UnitMF"].ToString());
                                    }
                                }
                                else
                                {
                                    unit2 = "No";
                                    unitMf = 0;
                                }
                                //DataTable dt_batch_wise_stock = this.cntrl.get_batch_wise_stock(dr.Cells["colBatchEntry"].Value.ToString());
                                //batchNumber_qty = Convert.ToDecimal(dt_batch_wise_stock.Rows[0]["Qty"].ToString()) - Convert.ToDecimal(dr.Cells["colQuantity"].Value.ToString());
                                ////this.cntrl.update_batchnumber(Convert.ToDecimal(dr.Cells["currentStock"].Value.ToString()),
                                ////    dr.Cells["colBatchEntry"].Value.ToString(), con, trans);
                                //this.cntrl.update_batchnumber(batchNumber_qty,
                                //   dr.Cells["colBatchEntry"].Value.ToString(), con, trans);
                                if (btnSave.Text == "UPDATE")
                                {
                                    this.cntrl.update_batchsale(Convert.ToInt32(dr.Cells["ColinvNum"].Value.ToString()), Convert.ToDateTime(dr.Cells["ColInvDate"].Value.ToString()).ToString("yyyy-MM-dd"), dr.Cells["coiltem_code"].Value.ToString(), dr.Cells["colBatchnumber"].Value.ToString(), Convert.ToDecimal(dr.Cells["colQuantity"].Value.ToString()), Convert.ToDecimal(dr.Cells["rate"].Value.ToString()), dr.Cells["colBatchEntry"].Value.ToString(), unit2, unitMf.ToString(), con, trans);
                                }
                                else
                                {
                                    this.cntrl.save_batchsale(Convert.ToInt32(dr.Cells["ColinvNum"].Value.ToString()), Convert.ToDateTime(dr.Cells["ColInvDate"].Value.ToString()).ToString("yyyy-MM-dd"), dr.Cells["coiltem_code"].Value.ToString(), dr.Cells["colBatchnumber"].Value.ToString(), Convert.ToDecimal(dr.Cells["colQuantity"].Value.ToString()), Convert.ToDecimal(dr.Cells["rate"].Value.ToString()), dr.Cells["colBatchEntry"].Value.ToString(), unit2, unitMf.ToString(), con, trans);
                                }

                            }//dr.Cells["ColinvNum"].Value.ToString()
                            if (salesOrder_flag == true)
                            {
                                this.cntrl.update_salesorder(invnum_order, con, trans);
                            }
                            //trans.Commit();
                            //con.Close();
                        }
                        
                    }
                    trans.Commit();
                    con.Close();
                    if (btnSave.Text != "UPDATE")
                    {
                        foreach (DataGridViewRow dr in dgv_BatchSale.Rows)
                        {
                            decimal batchNumber_qty = 0;
                            DataTable dt_batch_wise_stock = this.cntrl.get_batch_wise_stock(dr.Cells["colBatchEntry"].Value.ToString());
                            batchNumber_qty = Convert.ToDecimal(dt_batch_wise_stock.Rows[0]["Qty"].ToString()) - Convert.ToDecimal(dr.Cells["colQuantity"].Value.ToString());
                            //this.cntrl.update_batchnumber(Convert.ToDecimal(dr.Cells["currentStock"].Value.ToString()),
                            //    dr.Cells["colBatchEntry"].Value.ToString(), con, trans);
                            this.cntrl.update_batchnumber_(batchNumber_qty,
                               dr.Cells["colBatchEntry"].Value.ToString());
                        }
                    }
                    if(prescrption_sale==true)
                    {
                        this.cntrl.change_pres_status(PrescritionMain_id);
                        presgrid.Rows.RemoveAt(Convert.ToInt32( pres_index));
                        prescrption_sale = false;
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    con.Close();
                    MessageBox.Show(ex.Message, "SAVE / UPDATE function is failed !..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  
                    return;
                }
                try
                {
                   
                    string dt = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    DateTime Timeonly = DateTime.Now;
                    if (btnSave.Text == "SAVE")
                    {
                        this.cntrl.save_log(doctor_id, "Sales", " Adds Sales", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Add", invnumber.ToString());

                    }
                    else
                    {
                        this.cntrl.save_log(doctor_id, "Sales", "Edit Sales", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Edit", invnumber);
                    }
                    if (btnSave.Text == "UPDATE")
                    {
                        try
                        {
                            DialogResult res = MessageBox.Show("Data updated Successfully, Do you want to print...?", "Success",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (res == DialogResult.Yes)
                            {
                                printhtml();
                                txtBarcode.Focus();
                            }
                            else
                            {
                                txtBarcode.Focus();
                            }
                        }
                        catch (Exception ex)
                        {
                        }

                    }
                    else
                    {
                        DialogResult res = MessageBox.Show("Data saved Successfully, Do you want to print...?", "Success",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (res == DialogResult.Yes)
                        {
                            printhtml();
                            txtBarcode.Focus();
                        }
                        else
                        {
                            txtBarcode.Focus();
                        }
                    }
                    DocNumber_increment();
                    clear_All_Data();
                    clear_itemdetails();
                    //panl_mode_payment.Visible = false;
                    rad_CreditSale.Checked = false;
                    rad_CashSale.Checked = true;
                    btnSave.Text = "SAVE";
                    label23.Visible = false;
                    lblAdvance.Visible = false;
                    System.Data.DataTable tb_doctor = this.cntrl.get_doctor(doctor_id);
                    if (tb_doctor.Rows.Count > 0)
                    {
                        txtBdoctor.Text = tb_doctor.Rows[0]["doctor_name"].ToString();
                    }
                    if(salesOrder_flag==true)
                    {
                        this.Close();
                    }

                }
                catch(Exception ex)
                {
                }

            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dgv_SalesItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Txt_TotalAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_UnitCost_TextChanged(object sender, EventArgs e)
        {
            decimal d;
            if (decimal.TryParse(txt_UnitCost.Text, out d))
            {
                TotalAmount_Calculation();
            }
            else
            {
                txt_UnitCost.Text = "0";
                TotalAmount_Calculation();
            }
        }

        private void txtdisc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            string a = txtdisc.Text;
            string b = a.TrimStart('0');
            txtdisc.Text = b;
        }

        private void txtdisc_Leave(object sender, EventArgs e)
        {
            if (txtdisc.Text == "")
            {
                txtdisc.Text = "0";
            }
            else
            {
                string a = txtdisc.Text;
                string b = a.TrimStart('0');
                txtdisc.Text = b;
            }
        }

        private void txtdisc_Click(object sender, EventArgs e)
        {
            if (txtdisc.Text == "0")
            {
                txtdisc.Text = "";
            }
        }

        private void dgrid_prescription_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            {
                System.Data.DataTable dt_drug_inv_Details = this.cntrl.get_inventoryid(dgrid_prescription.CurrentRow.Cells["drug_id"].Value.ToString());
                if(dt_drug_inv_Details.Rows.Count>0)
                {
                    DataTable dt_exp = this.cntrl.drug_instock(dt_drug_inv_Details.Rows[0]["inventory_id"].ToString());
                    if (dt_exp.Rows.Count > 0 && dt_exp.Rows[0][0].ToString() != "")
                    {
                        DataTable dtb = this.cntrl.Get_itemdetails(dt_drug_inv_Details.Rows[0]["inventory_id"].ToString());
                        if (dtb.Rows.Count > 0)
                        {
                            txt_ItemCode.Text = dtb.Rows[0]["id"].ToString();
                            txt_Discription.Text = dtb.Rows[0]["item_name"].ToString();
                            txt_UnitCost.Text = dtb.Rows[0]["Sales_Rate_Max"].ToString();
                            txt_GST.Text = dtb.Rows[0]["GstVat"].ToString();
                            txt_IGST.Text = "0";// dtb.Rows[0]["IGST"].ToString();
                            txtdisc.Text = "0";
                            cmb_Unit.Items.Clear();
                            if (dtb.Rows[0]["OneUnitOnly"].ToString() == "True")
                            {
                                cmb_Unit.Items.Add(dtb.Rows[0]["Unit1"].ToString());
                            }
                            else
                            {
                                cmb_Unit.Items.Add(dtb.Rows[0]["Unit2"].ToString());
                                cmb_Unit.Items.Add(dtb.Rows[0]["Unit1"].ToString());
                            }
                            cmb_Unit.SelectedIndex = 0;
                            txt_Qty.Text = dgrid_prescription.CurrentRow.Cells["drug_qty"].Value.ToString ();
                            TotalAmount_Calculation();
                        }
                    }

                }
            }
        }

        private void cmb_disc_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal totaldis = 0, totalamount = 0, totaldisper = 0;
            decimal d;
            if (decimal.TryParse(txt_DiscAmount.Text, out d))
            {
                if (cmb_disc.Text == "INR")
                {
                    if (decimal.TryParse(txt_Discount.Text, out d))
                    {
                        totalamount = Convert.ToDecimal(Txt_TotalAmount.Text);
                        totaldisper = Convert.ToDecimal(txt_Discount.Text);
                        totaldis = totalamount - totaldisper;// Convert.ToDecimal((totalamount * totaldisper) / 100);
                        txt_DiscAmount.Text = Convert.ToDecimal(totaldis).ToString("##.00");
                        txt_GrandTotal.Text = Convert.ToDecimal(totaldis).ToString("##.00");// Convert.ToDecimal(Convert.ToDecimal(Txt_TotalAmount.Text) - Convert.ToDecimal(totaldis)).ToString("##.00");
                    }
                    else
                    {
                        txt_DiscAmount.Text = "00.00";
                        txt_GrandTotal.Text = Txt_TotalAmount.Text;
                    }
                }
                else
                {
                    if (decimal.TryParse(txt_Discount.Text, out d))
                    {
                        totalamount = Convert.ToDecimal(Txt_TotalAmount.Text);
                        totaldisper = Convert.ToDecimal(txt_Discount.Text);
                        totaldis = Convert.ToDecimal((totalamount * totaldisper) / 100);
                        txt_DiscAmount.Text = Convert.ToDecimal(totaldis).ToString("##.00");
                        txt_GrandTotal.Text = Convert.ToDecimal(Convert.ToDecimal(Txt_TotalAmount.Text) - Convert.ToDecimal(totaldis)).ToString("##.00");
                    }
                    else
                    {
                        txt_DiscAmount.Text = "00.00";
                        txt_GrandTotal.Text = Txt_TotalAmount.Text;
                    }
                }

            }
        }

        public void call_Item_Batch(string item_Code, decimal qty, string unit)
        {
            dtFor_CurrentStockUpdate_Bill.Columns.Clear();
            dtFor_CurrentStockUpdate_Bill.Rows.Clear();
            decimal Stock = 0;
            decimal curent_Stock = 0;
            TotalStock = 0;
            {
            DataTable Dt_updateStock = this.cntrl.Get_stock(item_Code);
            if (Dt_updateStock.Rows[0][0].ToString() != "")
                TotalStock = Convert.ToDecimal(Dt_updateStock.Rows[0][0].ToString());
            }
            Stock = qty;
            decimal Remaning_qty = 0;
            decimal stk_value = 0;
            Remaning_qty = qty;
            if (Stock <= TotalStock)
            {
                DataTable dtb = this.cntrl.get_batchdetails(item_Code);
                if (dtb.Rows.Count > 0)
                {
                    dtFor_CurrentStockUpdate_Bill.Columns.Add("colentryNo");
                    dtFor_CurrentStockUpdate_Bill.Columns.Add("colbatchNo");
                    dtFor_CurrentStockUpdate_Bill.Columns.Add("ColStock");
                    dtFor_CurrentStockUpdate_Bill.Columns.Add("ColPrd_Date");
                    dtFor_CurrentStockUpdate_Bill.Columns.Add("colExpDate");
                    dtFor_CurrentStockUpdate_Bill.Columns.Add("clUnit");
                    dtFor_CurrentStockUpdate_Bill.Columns.Add("ColQty");
                    dtFor_CurrentStockUpdate_Bill.Columns.Add("Colrate");
                    dtFor_CurrentStockUpdate_Bill.Columns.Add("colCurrentStock");
                    for (int i = 0; i < dtb.Rows.Count; i++)//
                    {
                        if (Remaning_qty <= Convert.ToDecimal(dtb.Rows[i]["Qty"].ToString()))
                        {
                            DataRow dRow = dtFor_CurrentStockUpdate_Bill.NewRow();
                            dRow[0] = dtb.Rows[i]["Entry_No"].ToString();
                            dRow[1] = dtb.Rows[i]["BatchNumber"].ToString();
                            dRow[2] = dtb.Rows[i]["Qty"].ToString();
                            dRow[3] = dtb.Rows[i]["PrdDate"].ToString();
                            dRow[4] = dtb.Rows[i]["ExpDate"].ToString();
                            dRow[5] = unit;
                            dRow[6] = Remaning_qty.ToString();
                            dRow[7] = dtb.Rows[i]["batch_sales_rate"].ToString();
                            curent_Stock = Convert.ToDecimal(dtb.Rows[i]["Qty"].ToString()) - Convert.ToDecimal(Remaning_qty);
                            dRow[8] = curent_Stock.ToString();
                            dtFor_CurrentStockUpdate_Bill.Rows.Add(dRow);
                            break;
                        }
                        else
                        {
                            stk_value = Convert.ToDecimal(dtb.Rows[i]["Qty"].ToString());
                            DataRow dRow = dtFor_CurrentStockUpdate_Bill.NewRow();
                            dRow[0] = dtb.Rows[i]["Entry_No"].ToString();
                            dRow[1] = dtb.Rows[i]["BatchNumber"].ToString();
                            dRow[2] = dtb.Rows[i]["Qty"].ToString();
                            dRow[3] = dtb.Rows[i]["PrdDate"].ToString();
                            dRow[4] = dtb.Rows[i]["ExpDate"].ToString();
                            dRow[5] = unit;
                            dRow[6] = stk_value;
                            dRow[7] = dtb.Rows[i]["batch_sales_rate"].ToString();
                            curent_Stock = Convert.ToDecimal(dtb.Rows[i]["Qty"].ToString()) - Convert.ToDecimal(stk_value);
                            dRow[8] = curent_Stock.ToString();
                            Remaning_qty = Remaning_qty - stk_value;
                        }
                    }
                }
            }
        }
        public void Fiil_BatchSale_Grid_Prescription_bill()
        {
            int row = dgv_BatchSale.Rows.Count;
            if (dtFor_CurrentStockUpdate_Bill.Rows.Count > 0)
            {
                foreach (DataRow dr in dtFor_CurrentStockUpdate_Bill.Rows)
                {
                    if (dr["ColQty"].ToString() != "")
                    {
                        dgv_BatchSale.Rows.Add();
                        dgv_BatchSale.Rows[row].Cells["ColinvNum"].Value = txtDocumentNumber.Text;
                        dgv_BatchSale.Rows[row].Cells["ColInvDate"].Value = dtpDocumentDate.Value.ToShortDateString();
                        dgv_BatchSale.Rows[row].Cells["coiltem_code"].Value = txt_ItemCode.Text;
                        dgv_BatchSale.Rows[row].Cells["colBatchnumber"].Value = dr["colbatchNo"].ToString();
                        dgv_BatchSale.Rows[row].Cells["colQuantity"].Value = dr["ColQty"].ToString();
                        dgv_BatchSale.Rows[row].Cells["rate"].Value = dr["Colrate"].ToString();
                        dgv_BatchSale.Rows[row].Cells["colStock"].Value = dr["ColStock"].ToString();
                        dgv_BatchSale.Rows[row].Cells["colBatchEntry"].Value = dr["colentryNo"].ToString();
                        dgv_BatchSale.Rows[row].Cells["colIsExp"].Value = txt_Qty.Text;
                        dgv_BatchSale.Rows[row].Cells["prddate"].Value = dr["ColPrd_Date"].ToString();
                        dgv_BatchSale.Rows[row].Cells["expdate"].Value = dr["colExpDate"].ToString();
                        dgv_BatchSale.Rows[row].Cells["unit"].Value = dr["clUnit"].ToString();
                        if (dr["colCurrentStock"].ToString() != "")
                        {
                            dgv_BatchSale.Rows[row].Cells["currentStock"].Value = dr["colCurrentStock"].ToString();
                        }
                        else
                            dgv_BatchSale.Rows[row].Cells["currentStock"].Value = dr["ColStock"].ToString();
                    }
                    row++;
                }
            }
        }

        //thermal......bhj
        public void printhtml_thermal()
        {
            try
            {
                string strclinicname = "", strphone = "", DlNumber = "", DlNumber2 = "", website = "", path = "";
                decimal totalamount = 0;
                string str_druglicenseno = "";
                string str_taxno = "";
                System.Data.DataTable dtp = this.cntrl.Get_companydetails();
                DataTable print_settng = this.prnt_ctrl.Get_inventory_id();
                if (dtp.Rows.Count > 0)
                {
                    string clinicn = "";
                    //clinicn = dtp.Rows[0]["name"].ToString();
                    if (print_settng.Rows.Count > 0)
                    {
                        clinicn = print_settng.Rows[0]["header"].ToString();
                        strclinicname = clinicn.Replace("¤", "'");
                        strphone = print_settng.Rows[0]["right_text"].ToString();
                        DlNumber = print_settng.Rows[0]["left_text"].ToString();
                    }
                    DlNumber2 = dtp.Rows[0]["email"].ToString();
                    website = dtp.Rows[0]["website"].ToString();
                    str_druglicenseno = dtp.Rows[0]["Dl_Number"].ToString();
                    str_taxno = dtp.Rows[0]["Dl_Number2"].ToString();
                    path = dtp.Rows[0]["path"].ToString();
                    logo_name = path;
                }
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                StreamWriter sWrite = new StreamWriter(Apppath + "\\sales_Invoice_print.html");
                sWrite.WriteLine("<html>");
                sWrite.WriteLine("<head>");
                sWrite.WriteLine("<style>");
                sWrite.WriteLine("table { border-collapse: collapse;}");
                sWrite.WriteLine("p.big {line-height: 400%;}");
                sWrite.WriteLine("</style>");
                sWrite.WriteLine("</head>");
                sWrite.WriteLine("<body >");
                sWrite.WriteLine("<div>");
                sWrite.WriteLine("<table align=center width='100%'>");
                sWrite.WriteLine("<col >");
                sWrite.WriteLine("<br>");
                string Appath = System.IO.Directory.GetCurrentDirectory();
                if (File.Exists(Appath + "\\" + logo_name))
                {
                    sWrite.WriteLine("<table align='center' style='width:270px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td width='4.29%' height='50px' align='left' rowspan='3'><img src='" + Appath + "\\" + logo_name + "'style='width:70px;height:70px;' ></td>  ");//30px
                    sWrite.WriteLine("<td width='100%' align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=4><b>" + strclinicname + "</font> <br><FONT  COLOR=black  face='Segoe UI' SIZE=2>&nbsp;" + DlNumber + "<br>&nbsp;" + strphone + " </b></td></tr>");//820px
                    sWrite.WriteLine("</table>");
                }
                else
                {
                    sWrite.WriteLine("<table align='center' style='width:270px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' height='20px'><FONT  COLOR=black  face='Segoe UI' SIZE=3><b>" + strclinicname + "</b></font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' height='20px'><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + DlNumber + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' height='20px' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>" + strphone + "</font></td></tr>");
                    sWrite.WriteLine("</table>");
                }
                sWrite.WriteLine("<table width='100%' align='center' style='width:270px ;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr><td align='left' width='100%'><hr/></td></tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<th ><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3> SALES INVOICE  </font></center></b></td>");
                sWrite.WriteLine("</tr>");

                sWrite.WriteLine("<table width='100%' align='center' style='width:270px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan='1' align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2>Drug License:" + str_druglicenseno + "</font></left></td>");
                sWrite.WriteLine("<td colspan='1' align='right'>  <FONT COLOR=black FACE='Segoe UI'   SIZE=2>Tax No:&nbsp;" + str_taxno + " &nbsp;</font></right></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan='1' align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2> Sold To : &nbsp" + txtPatient.Text + "</font></left></td>");
                sWrite.WriteLine("<td colspan='1' align='right'><FONT COLOR=black FACE='Segoe UI'   SIZE=2>Invoice No:&nbsp;" + txtDocumentNumber.Text + "&nbsp; </font></right></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan='1' align='left'><FONT COLOR=black FACE='Segoe UI'   SIZE=2> Prescribed by : Dr." + txtBdoctor.Text + "</font></left></td>");
                sWrite.WriteLine("<td colspan='1' align='right'><FONT COLOR=black FACE='Segoe UI'   SIZE=2> Date : " + dtpDocumentDate.Value.ToString("dd-MM-yyyy") + "</font></right></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<table width='100%' align='center' style='width:270px;border: 1px ;border-collapse: collapse;'>");
                if (rad_CashSale.Checked == true)
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td width='100%' align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2> Mode of payment: &nbsp Cash</font></left></td>");
                    sWrite.WriteLine("</tr>");
                }
                else
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td width='100%' align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2> Mode of payment: &nbsp" + Cmb_ModeOfPaymnt.Text + "</font></left></td>");
                    sWrite.WriteLine("</tr>");
                }

                sWrite.WriteLine("<tr><td colspan='100%'><hr/></td></tr></table>");

                sWrite.WriteLine("<table width='100%' align='center' style='width:270px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='left'  width='20'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3><b>Sl</b></font></td>");
                sWrite.WriteLine("<td align='left'  width='180'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3><b>Item</b></font></td>");
                // sWrite.WriteLine("<td align='left'  width='130'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>Batch</b></font></td>");
                //sWrite.WriteLine("<td align='left'  width='90'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3><b>Expiry</b></font></td>");
                // sWrite.WriteLine("<td align='left'  width='70'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3><b>Unit</b></font></td>");
                sWrite.WriteLine("<td align='left'  width='80'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3><b>Qty</b></font></td>");
                sWrite.WriteLine("<td align='left'  width='100'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3><b>Cost</b></font></td>");
                // sWrite.WriteLine("<td align='left'  width='100'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3><b>Discount</b></font></td>");
                //sWrite.WriteLine("<td align='left'  width='50'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3><b>GST%</b></font></td>");
                sWrite.WriteLine("<td align='right'  width='100'><FONT COLOR=black FACE='Geneva,Sego UI' SIZE=3><b>Amount</b></font></td>");


                sWrite.WriteLine("<tr><td align='left' colspan='100%'><hr/></td></tr>");
                string removecomma = "", remoexp = "";

                int k = 1; decimal tax_amt = 0, total_cgst = 0, total_sgst = 0;
                for (int i = 0; i < dgv_SalesItem.Rows.Count; i++)
                {
                    decimal gst_Amount = 0, Amount = 0, cgst = 0;
                    //sWrite.WriteLine("<table width='100%' align='center' style='width:100%;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + k + "</font></td>");
                    sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + dgv_SalesItem.Rows[i].Cells["colDiscription"].Value.ToString() + "</font></td>");
                    string strBatch = "", strexp = "", expirydate = "";
                    string mon = "";
                    for (int ii = 0; ii < dgv_BatchSale.Rows.Count; ii++)
                    {
                        if (dgv_BatchSale.Rows[ii].Cells["coiltem_code"].Value.ToString() == dgv_SalesItem.Rows[i].Cells["id"].Value.ToString())
                        {
                            strBatch = strBatch + dgv_BatchSale.Rows[ii].Cells["colBatchnumber"].Value.ToString() + ",";
                            if (dgv_BatchSale.Rows[ii].Cells["expdate"].Value != null && dgv_BatchSale.Rows[ii].Cells["expdate"].Value.ToString() != "")
                            {
                                strexp = strexp + dgv_BatchSale.Rows[ii].Cells["expdate"].Value.ToString().TrimEnd() + ",";//).ToString("dd-MM-yyyy").Trim();
                                //mon = Convert.ToDateTime(strexp);
                                //string ed = mon.Month.ToString();//ToString("MM-yy");
                                //string ye = mon.Year.ToString();
                                //expirydate = expirydate + ed + "/" + ye+",";
                            }
                            else
                            {
                                //strBatch = strBatch + dgv_BatchSale.Rows[ii].Cells["colBatchnumber"].Value.ToString() + ",";
                            }
                        }
                    }
                    if (strBatch != "")
                        removecomma = strBatch.Remove(strBatch.Length - 1);
                    if (strexp != "")
                    {
                        remoexp = strexp.Remove(strexp.Length - 1);
                        mon = Convert.ToDateTime(remoexp).ToString("dd-MM-yyyy");
                        //string ed = mon.Month.ToString();//ToString("MM-yy");
                        //string ye = mon.Year.ToString();
                        //expirydate = ed + "/" + ye;
                    }

                    //sWrite.WriteLine("<td align='left' colspan='1'><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + removecomma + "</font></td>");
                    //sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + mon + "</font></td>");
                    //sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + dgv_SalesItem.Rows[i].Cells["ColUnit"].Value.ToString() + "</font></td>");
                    sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + dgv_SalesItem.Rows[i].Cells["ColQty"].Value.ToString() + "</font></td>");
                    // sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + dgv_SalesItem.Rows[i].Cells["c_disc"].Value.ToString() + "</font></td>");
                    sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + Convert.ToDecimal(dgv_SalesItem.Rows[i].Cells["colUnitcost"].Value.ToString()).ToString("##0.00") + "</font></td>");
                    Amount = Convert.ToDecimal(dgv_SalesItem.Rows[i].Cells["ColQty"].Value.ToString()) * Convert.ToDecimal(dgv_SalesItem.Rows[i].Cells["colUnitcost"].Value.ToString());

                    // sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + Convert.ToDecimal(dgv_SalesItem.Rows[i].Cells["ColGST"].Value.ToString()) + "</font></td>");
                    cgst = Convert.ToDecimal(dgv_SalesItem.Rows[i].Cells["ColGST"].Value.ToString()) / 2;
                    gst_Amount = (Amount * cgst) / 100;
                    tax_amt = Amount + gst_Amount + gst_Amount;
                    sWrite.WriteLine("<td align='right'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + tax_amt.ToString("##0.00") + "</font></td>");

                    totalamount = totalamount + tax_amt;//   totalamount + Convert.ToDecimal(dgv_SalesItem.Rows[i].Cells["colAmount"].Value.ToString());
                    total_cgst = total_cgst + gst_Amount;
                    k = k + 1;


                    sWrite.WriteLine("</tr>");

                }
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<table width='100%' align='center' style='width:270px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr><td align='left'  colspan='100%'><hr/></td></tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan='1' align='right'><FONT COLOR=black FACE='Segoe UI'   SIZE=2> Total Items : " + txt_totalItems.Text + "</font></right></td>");

                sWrite.WriteLine("</tr>");
                if (txt_SGST.Text != "")
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan='1' align='right'><FONT COLOR=black FACE='Segoe UI'   SIZE=2> CGST : " + total_cgst.ToString("##0.00") + "</font></right></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan='1' align='right'><FONT COLOR=black FACE='Segoe UI'   SIZE=2> SGST : " + total_cgst.ToString("##0.00") + "</font></right></td>");
                    sWrite.WriteLine("</tr>");
                }
                //else if (Txt_TotalIGST.Text != "")
                //{
                //    sWrite.WriteLine("<tr>");
                //    sWrite.WriteLine("<td colspan='1' align='right'><FONT COLOR=black FACE='Segoe UI'   SIZE=2> Total IGST : " + Txt_TotalIGST.Text + "</font></right></td>");
                //    sWrite.WriteLine("</tr>");
                //}
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan='1' align='right'><FONT COLOR=black FACE='Segoe UI'   SIZE=2> Total Amount : " + String.Format("{0:C}", Convert.ToDecimal(Txt_TotalAmount.Text)) + "</font></right></td>");//decimal.Parse(Txt_TotalAmount.Text).ToString("##0.000")
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan='1' align='right'><FONT COLOR=black FACE='Segoe UI'   SIZE=2> Discount Amount: " + String.Format("{0:C}", Convert.ToDecimal(txt_DiscAmount.Text)) + "</font></right></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan='1' align='right'><FONT COLOR=black FACE='Segoe UI'   SIZE=2><b> Grand Total :  " + String.Format("{0:C}", Convert.ToDecimal(txt_GrandTotal.Text)) + "</b></font></right></td>");// decimal.Parse(txt_GrandTotal.Text).ToString("##0.000")
                //sWrite.WriteLine(" <div class='footer'>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan='1' align='right'><FONT COLOR=black FACE='Segoe UI'   SIZE=2><b> Round Off : " + Math.Round(decimal.Parse(txt_GrandTotal.Text)).ToString("##0.00") + "</b></font></right></td>");
                sWrite.WriteLine(" <div class='footer'>");
                sWrite.WriteLine("</tr>");

                sWrite.WriteLine("<tr><td align='left' width='100%' > <FONT COLOR=black FACE='Segoe UI' SIZE=2>Pharmacist Signature</font></td></tr>");
                sWrite.WriteLine("<tr><td align='left'  width='100%'><hr/></td></tr>");
                sWrite.WriteLine("<tr><td align='left' width='100%' > <FONT COLOR=black FACE='Segoe UI' SIZE=1><i>Goods once sold cannot be taken back or exchanged</i></font></td></tr>");
                sWrite.WriteLine("</div>");
                sWrite.WriteLine("<tr><td align='left' width='100%' > <FONT COLOR=black FACE='Segoe UI' SIZE=2>E&OE</font></td></tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<script>window.print();</script>");
                sWrite.WriteLine("</body >");
                sWrite.WriteLine("</html>");
                sWrite.Close();
                System.Diagnostics.Process.Start(Apppath + "\\sales_Invoice_print.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Printer not ready...." + ex.Message, "Printer error.. ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
    }
}
