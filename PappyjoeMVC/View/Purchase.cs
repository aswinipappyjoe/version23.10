using MySql.Data.MySqlClient;
using PappyjoeMVC.Controller;
using PappyjoeMVC.Model;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
namespace PappyjoeMVC.View
{
    public partial class Purchase : Form
    {
        purchase_controller cntrl = new purchase_controller();
        Printout_controller prnt_ctrl = new Printout_controller();
        DataTable editgrid = new DataTable();
        GeneralFunctions GF = new GeneralFunctions();
        public DataTable data_from_Pur_Master1 = new DataTable();
        public DataTable data_from_purchase1 = new DataTable();
        Connection db = new Connection();
        decimal amtUntchange = 0;
        decimal ttlamt = 0;
        public string doctor_id = "0";
        public static string itemCode;
        public static string Item_id;
        public static bool Item_flag = false;
        bool flagSup = false;
        public bool flagedit = false;
        bool purchOrder_flag = false;
        bool flag_save = false;
        bool flagcheck = false;
        public string form_name;
        public static int freeQty;
        bool Pur_List_flag = false;
        public static int qty;
        string unit2Is;
        public int Rowindex = 0;
        int Pur_order_no1 = 0;
        public static DataTable dt_forBatch;
        public static decimal total_batch_rate = 0;
        public string edit;
        public bool load_flag = false;
        public  bool supplier_flag=false; public bool credit_flag = false;
        public Purchase()
        {
            InitializeComponent();
        }
        public Purchase(int purch_id)
        {
            InitializeComponent();
            Pur_order_no1 = purch_id;
            purchOrder_flag = true;
        }
        public Purchase(string item_code)
        {
            InitializeComponent();
            itemCode = item_code;
            Item_flag = true;
        }
        public Purchase(DataTable gridData)
        {
            InitializeComponent();
            dt_forBatch = gridData;
        }
        public Purchase(string item_code, string item_id) : this(item_code)
        {
            Item_id = item_id;
            itemCode = item_code;
        }
        public Purchase(DataTable data_from_Pur_Master, DataTable data_from_purchase)
        {
            InitializeComponent();
            data_from_Pur_Master1 = data_from_Pur_Master;
            data_from_purchase1 = data_from_purchase;
            Pur_List_flag = true;
        }

        public Purchase(bool flag)
        {
            InitializeComponent();
            supplier_flag = flag;
        }

        public Purchase(DataTable gridData, decimal total_rate) : this(gridData)
        {
            InitializeComponent();
            dt_forBatch = gridData;
            total_batch_rate = total_rate;
        }

        private void Btn_itemCode_Click(object sender, EventArgs e)
        {
            try
            {
                if (type == "Consumable")
                {
                    Item_id = "";
                    var form = new PurchaseItemLIst("Purchase", txt_Itemcode.Text);
                    form.type = "Consumable";
                    form.ShowDialog();
                }
                else
                {
                    Item_id = "";
                    var form = new PurchaseItemLIst("Purchase", txt_Itemcode.Text);
                    form.type = "Pharmacy";
                    form.ShowDialog();
                }
                txt_Itemcode.Text = itemCode;
                if (checkRep() == 1)
                {
                    MessageBox.Show("Item already existed", "Duplication Encountered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DataTable dtb = this.cntrl.Get_itemdetails(Item_id);
                    Load_item_in_textbox(dtb);
                }
                itemCode = "";
                lst_Description.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public int checkRep()
        {
            int affecte = 0;
            for (int i = 0; i < dgvItemData.Rows.Count; i++)
            {
                if (dgvItemData.Rows[i].Cells["itemid"].Value.ToString() == txt_Itemcode.Text)
                {
                    affecte = 1;
                    clear();
                }
            }
            return affecte;
        }

        private void txt_Itemcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string item = txt_Itemcode.Text;
                    if (item != "")
                    {
                        string formname = "Purchase";
                        var form = new PurchaseItemLIst(formname, item);
                        form.ShowDialog();
                        txt_Itemcode.Text = itemCode;
                        if (checkRep() == 1)
                        {
                            MessageBox.Show("This item already exists", "Duplication Encountered", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        else
                        {
                            DataTable dtb = this.cntrl.Get_itemdetails(Item_id);
                            Load_item_in_textbox(dtb);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Load_item_in_textbox(DataTable dtitems)
        {
            if (dtitems.Rows.Count > 0)
            {
                unitload(dtitems);
                txtDescription.Text = dtitems.Rows[0]["item_name"].ToString();
                txtPacking.Text = dtitems.Rows[0]["Packing"].ToString();
                txtUnitCost.Text = Convert.ToDecimal(dtitems.Rows[0]["Purch_Rate"].ToString()).ToString("0.00");
                txtbarcode.Text = dtitems.Rows[0]["Barcode"].ToString();
                txtGst.Text =Convert.ToDecimal( dtitems.Rows[0]["GstVat"].ToString()).ToString("0.00");
                txtAmount.Text = "0.00";
                txt_qty.Text = "1";
                calculaton();

            }
        }

        private void cmbUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbUnit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (load_flag == false)
                {
                    if (flagedit == false)
                    {
                        if (cmbUnit.Text != "")
                        {
                            decimal gstamt = 0;
                            decimal disc = 0;
                            DataTable dtitems = this.cntrl.Get_item_units(Item_id);
                            decimal gst = Convert.ToDecimal(txtGst.Text);
                            decimal igst = Convert.ToDecimal(txtIgst.Text);
                            decimal discount = Convert.ToDecimal(textdisc.Text);
                            string unit2 = dtitems.Rows[0]["Unit2"].ToString();
                            string unit1 = dtitems.Rows[0]["Unit1"].ToString();
                            string unit;
                            int UnitMf = Convert.ToInt32(dtitems.Rows[0]["UnitMF"].ToString());
                            if (txt_qty.Text != "" && txtUnitCost.Text != "")
                            {
                                unit = cmbUnit.Text;
                                if (unit == unit2)
                                {
                                    int qty = int.Parse(txt_qty.Text);
                                    DataTable dtb = this.cntrl.Get_itemdetails_itemname(txtDescription.Text);
                                    if (dtb.Rows.Count > 0)
                                    {

                                        txtUnitCost.Text = dtb.Rows[0]["Purch_Rate"].ToString();

                                    }
                                    decimal unitcost = Convert.ToDecimal(txtUnitCost.Text) / UnitMf;
                                    if (txtGst.Text != "0")
                                    {
                                        gstamt = (((unitcost * qty) * gst) / 100) + (unitcost * qty);
                                        txtAmount.Text = gstamt.ToString("##.00");
                                        txtUnitCost.Text = unitcost.ToString("##.00");
                                    }
                                    else if (txtIgst.Text != "0")
                                    {
                                        gstamt = (((unitcost * qty) * igst) / 100) + (unitcost * qty);
                                        txtAmount.Text = gstamt.ToString("##.00");
                                        txtUnitCost.Text = unitcost.ToString("##.00");
                                    }
                                    else if (txtGst.Text == "0" && txtIgst.Text == "0")
                                    {
                                        txtUnitCost.Text = unitcost.ToString("##.00");
                                        amtUntchange = unitcost * qty;
                                        txtAmount.Text = amtUntchange.ToString("##.00");
                                        txtUnitCost.Text = unitcost.ToString("##.00");
                                    }
                                    amtUntchange = (unitcost * qty);
                                }
                                if (unit == unit1)
                                {
                                    unit = cmbUnit.Text;
                                    int qty = int.Parse(txt_qty.Text);
                                    DataTable dtb = this.cntrl.Get_itemdetails_itemname(txtDescription.Text);
                                    if (dtb.Rows.Count > 0)
                                    {

                                        txtUnitCost.Text = dtb.Rows[0]["Purch_Rate"].ToString();

                                    }
                                    decimal unitcost = Convert.ToDecimal(txtUnitCost.Text);// * UnitMf;
                                    if (txtGst.Text != "0")
                                    {
                                        gstamt = (((unitcost * qty) * gst) / 100) + (unitcost * qty);
                                        txtAmount.Text = gstamt.ToString("##.00");
                                        txtUnitCost.Text = unitcost.ToString("##.00");
                                    }
                                    else if (txtIgst.Text != "0")
                                    {
                                        gstamt = (((unitcost * qty) * igst) / 100) + (unitcost * qty);
                                        txtAmount.Text = gstamt.ToString("##.00");
                                        txtUnitCost.Text = unitcost.ToString("##.00");
                                    }
                                    else if (txtGst.Text == "0" && txtIgst.Text == "0")
                                    {
                                        txtUnitCost.Text = unitcost.ToString("##.00");
                                        amtUntchange = unitcost * qty;
                                        txtAmount.Text = amtUntchange.ToString("##.00");
                                        txtUnitCost.Text = unitcost.ToString("##.00");
                                    }
                                    amtUntchange = (unitcost * qty);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtGst_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            if (txtGst.Text != "0")
            {
                string a = txtGst.Text;
                string b = a.TrimStart('0');
                txtGst.Text = b;
            }
        }

        private void txtGst_Click(object sender, EventArgs e)
        {
            if (txtGst.Text == "0")
            {
                txtGst.Text = "";
            }
        }

        private void txtGst_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtGst.Text == "" || txtGst.Text == ".")
            {
                txtGst.Text = "0";
            }
        }

        private void txtGst_Leave(object sender, EventArgs e)
        {
            if (txtGst.Text == "")
            {
                txtGst.Text = "0";
            }
            else if (Convert.ToDecimal(txtGst.Text) > 0)
            {
                txtIgst.Text = "0";
            }
        }

        private void txtGst_TextChanged(object sender, EventArgs e)
        {
            calculaton();
        }
        public void calculaton()
        {
            try
            {
                if (txtGst.Text.Trim() != "" && txtIgst.Text.Trim() != "" && txtGst.Text != "." && txtIgst.Text != ".")
                {
                    if (txt_qty.Text != "" && txtUnitCost.Text != "")
                    {
                        decimal gstamt = 0;
                        decimal igstamt = 0;
                        decimal unitcost = 0;
                        decimal igst = 0;
                        decimal disc= 0;
                        int qty = 0;
                        decimal gst = 0;
                        decimal discount = 0;
                        unitcost = Convert.ToDecimal(txtUnitCost.Text);
                        qty = Convert.ToInt32(txt_qty.Text);
                        gst = Convert.ToDecimal(txtGst.Text);
                        igst = Convert.ToDecimal(txtIgst.Text);
                       discount = Convert.ToDecimal(textdisc.Text);//bahja
                       
                        if (txtGst.Text != "0.0")
                        {
                            gstamt = (((unitcost * qty) * gst) / 100) + (unitcost * qty);
                            txtAmount.Text = gstamt.ToString("##.00");
                        }
                        else if (txtIgst.Text != "0.0")
                        {
                            igstamt = (((unitcost * qty) * igst) / 100) + (unitcost * qty);
                            txtAmount.Text = igstamt.ToString("##.00");
                        }
                        else if (txtGst.Text == "0.0" && txtIgst.Text == "0.0")
                        {
                            txtAmount.Text = (unitcost * qty).ToString("##.00");
                        }
                        //bahja......if cond
                        if (textdisc.Text != "0.00")
                        {
                            if(gstamt>0)
                            {
                                disc = (gstamt-((gstamt * discount) / 100) );
                                txtAmount.Text = disc.ToString("##.00");
                            }
                            else
                            {
                               disc = (((unitcost * qty) * discount) / 100) + (unitcost * qty);
                                txtAmount.Text = disc.ToString("##.00");
                            }
                           
                        }
                        ttlamt = (unitcost * qty);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtIgst_Click(object sender, EventArgs e)
        {
            if (txtIgst.Text == "0")
            {
                txtIgst.Text = "";
            }
        }

        private void txtIgst_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
       (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            if (txtIgst.Text != "0")
            {
                string a = txtIgst.Text;
                string b = a.TrimStart('0');
                txtIgst.Text = b;
            }
        }

        private void txtIgst_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtIgst.Text == "" || txtIgst.Text == ".")
            {
                txtIgst.Text = "0";
            }
        }

        private void txtIgst_Leave(object sender, EventArgs e)
        {
            if (txtIgst.Text == "")
            {
                txtIgst.Text = "0";
            }
            else if (Convert.ToDecimal(txtIgst.Text) > 0)
            {
                txtGst.Text = "0";
            }
        }

        private void txtIgst_TextChanged(object sender, EventArgs e)
        {
            calculaton();
        }

        private void txt_qty_Click(object sender, EventArgs e)
        {
            if (txt_qty.Text == "0")
                txt_qty.Text = "";
        }

        private void txt_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            string a = txt_qty.Text;
            string b = a.TrimStart('0');
            txt_qty.Text = b;
        }

        private void txt_qty_KeyUp(object sender, KeyEventArgs e)
        {
            if (txt_qty.Text == "")
            {
                txt_qty.Text = "0";
            }
            calculaton();
        }

        private void txt_qty_Leave(object sender, EventArgs e)
        {
            if (txt_qty.Text == "")
            {
                txt_qty.Text = "0";
            }
        }

        private void txt_qty_TextChanged(object sender, EventArgs e)
        {
            if (txt_qty.Text != "0" && txt_qty.Text != "")
            {
                calculaton();
            }
        }

        private void txt_free_Click(object sender, EventArgs e)
        {
            if (txt_free.Text == "0")
            {
                txt_free.Text = "";
            }
        }

        private void txt_free_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            string a = txt_free.Text;
            string b = a.TrimStart('0');
            txt_free.Text = b;
        }

        private void txt_free_Leave(object sender, EventArgs e)
        {
            if (txt_free.Text == "")
            {
                txt_free.Text = "0";
            }
        }

        private void txtUnitCost_Click(object sender, EventArgs e)
        {
            if (txtUnitCost.Text == "0.00")
                txtUnitCost.Text = "";
        }

        private void txtUnitCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
       (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            string a = txtUnitCost.Text;
            string b = a.TrimStart('0');
            txtUnitCost.Text = Convert.ToDecimal(b).ToString();
        }

        private void txtUnitCost_KeyUp(object sender, KeyEventArgs e)
        {
            calculaton();
        }

        private void txtSupplierName_Click(object sender, EventArgs e)
        {
            DataTable dtb = this.cntrl.Load_Suplier();

            Load_Suplier(dtb);
        }

        public void Load_Suplier(DataTable dt)
        {
            lstbox_Supplier.DisplayMember = "Supplier_Name";
            lstbox_Supplier.ValueMember = "Supplier_Code";
            lstbox_Supplier.DataSource = dt;
            lstbox_Supplier.Show();
        }

        private void txtSupplierName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lstbox_Supplier.Visible != false)
                {
                    lstbox_Supplier.Focus();
                }
            }
        }

        private void txtSupplierName_TextChanged(object sender, EventArgs e)
        {
            if (flagSup == false)
            {
                if (txtSupplierName.Text != "")
                {
                    DataTable dtb = this.cntrl.LoadSuplier_wit_supname(txtSupplierName.Text);
                    Load_Suplier(dtb);
                }
            }
            flagSup = false;
        }

        private void lstbox_Supplier_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    flagSup = true;
                    txtSupplierName.Text = lstbox_Supplier.Text;
                    txt_SupplierId.Text = lstbox_Supplier.SelectedValue.ToString();
                    lstbox_Supplier.Hide();
                    txtinvoiceno.Focus();
                    e.SuppressKeyPress = true;
                }
                else if (e.KeyCode == Keys.Up)
                {
                    lstbox_Supplier.Focus();
                    int indicee = lstbox_Supplier.SelectedIndex;
                    indicee++;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    lstbox_Supplier.Focus();
                    int indicee = lstbox_Supplier.SelectedIndex;
                    indicee++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstbox_Supplier_MouseClick(object sender, MouseEventArgs e)
        {
            string sup_code = lstbox_Supplier.SelectedValue.ToString();
            txtSupplierName.Text = lstbox_Supplier.Text;
            txt_SupplierId.Text = sup_code;
            lstbox_Supplier.Hide();
        }
        public int itemcheck()
        {
            int affected = 0;
            for (int i = 0; i < dgvItemData.Rows.Count; i++)
            {
                if (dgvItemData.Rows[i].Cells["itemid"].Value != null && txt_Itemcode.Text == dgvItemData.Rows[i].Cells["itemid"].Value.ToString() && dgvItemData.Rows[i].Cells["note"].Value != null && cmbUnit.Text == dgvItemData.Rows[i].Cells["note"].Value.ToString())//
                {
                    MessageBox.Show("The ItemCode already existed ", "Duplication Encountered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    affected = 1;
                }
            }
            return affected;
        }
        private void Btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_Itemcode.Text != "" && txtDescription.Text != "" && txt_qty.Text != "0" && txtUnitCost.Text != "0.000" && txtAmount.Text != "0.000")
                {
                    if (Btn_Add.Text == "Add")
                    {
                        if (dgvItemData.Rows.Count > 0)
                        {
                            if (itemcheck() == 1)
                            {
                                return;
                            }
                        }
                    }
                    decimal amount1 = 0;
                    decimal amount = 0;
                    decimal amt = 0;
                    decimal amt1 = 0;
                    decimal igstcal = 0;
                    decimal igstcalc1 = 0;
                    decimal igstcalc = 0;
                    decimal cgstcal = 0;
                    decimal cgstcalc = 0;
                    decimal cgstcalc1 = 0;
                    decimal cgstcalc2 = 0;
                    decimal unitcost = 0;
                    int Qty = 0;
                    int qtycal = 0;
                    decimal unitcostcal = 0;
                    decimal gst = Convert.ToDecimal(txtGst.Text);
                    decimal igst = Convert.ToDecimal(txtIgst.Text);
                    decimal ucost = Convert.ToDecimal(txtUnitCost.Text);
                    //bahja
                    int qtty = Convert.ToInt32(txt_qty.Text);
                    decimal gstcalc = 0;
                    decimal igstcalcc = 0;
                    if (ttlamt == 0)
                    {
                        ttlamt = Convert.ToDecimal(txtUnitCost.Text) * Convert.ToInt32(txt_qty.Text);
                    }
                    if (txt_free.Text == "")
                    {
                        freeQty = 0;
                    }
                    else
                    {
                        freeQty = int.Parse(txt_free.Text);
                    }
                    qty = freeQty + int.Parse(txt_qty.Text);
                    DataTable dtb = this.cntrl.check_batch(Item_id);//Sales_Rate_Max
                    if (Btn_Add.Text == "Update")
                    {
                      
                        if (purchOrder_flag == false)
                        {
                            editgrid.Columns.Clear();
                            editgrid.Rows.Clear();
                            foreach (DataGridViewColumn col in dgvGridData.Columns)
                            {
                                editgrid.Columns.Add(col.Name);
                            }
                            foreach (DataGridViewRow row in dgvGridData.Rows)
                            {
                                DataRow dRow = editgrid.NewRow();
                                if (row.Cells["tempItem_code"].Value.ToString() == Item_id && row.Cells["Item_unit"].Value.ToString() == cmbUnit.Text)//
                                {
                                    foreach (DataGridViewCell cell in row.Cells)
                                    {
                                        dRow[cell.ColumnIndex] = cell.Value;
                                    }
                                    editgrid.Rows.Add(dRow);
                                }
                            }
                            if(editgrid.Rows.Count>0)
                            {
                                form_name = "purchase";
                                var form = new Purchase_Batch(cmbUnit.Text, txt_Itemcode.Text, Item_id, editgrid, qty, form_name, txtPurchInvNumber.Text);
                                form.ShowDialog();
                            }
                            //return;
                        }
                        if (purchOrder_flag == true)
                        {
                            var form = new Purchase_Batch(cmbUnit.Text, qty, txt_Itemcode.Text, Item_id, txtPurchInvNumber.Text);
                            form.ShowDialog();
                        }
                        txt_Itemcode.Enabled = true; txtDescription.Enabled = true; txtbarcode.Enabled = true;
                    }
                    if (Btn_Add.Text == "Add")
                    {
                        if (chk_consume.Checked == true)
                        {
                            var form = new purchase_batch_for_consumables(cmbUnit.Text, qty, txt_Itemcode.Text, Item_id, txtPurchInvNumber.Text);
                            form.ShowDialog();
                        }
                        else
                        {
                            var form = new Purchase_Batch(cmbUnit.Text, qty, txt_Itemcode.Text, Item_id, txtPurchInvNumber.Text, txtUnitCost.Text) ;
                            form.ShowDialog();
                        }
                    }
                    if (txt_qty.Text == "")
                    {
                        txt_qty.Text = "0";
                    }
                    if (Btn_Add.Text == "Update")
                    {
                        if (dt_forBatch != null)
                        {
                            decimal newcost = 0;
                            if (total_batch_rate != 0)
                            {
                                txtUnitCost.Text = total_batch_rate.ToString("0.00");
                                newcost = total_batch_rate;
                            }
                            cmbUnit.Enabled = true;
                            amtUntchange = Convert.ToDecimal(txtUnitCost.Text) * Convert.ToDecimal(txt_qty.Text);
                            dgvItemData.Rows[Rowindex].Cells["id"].Value = Item_id;
                            dgvItemData.Rows[Rowindex].Cells["itemid"].Value = txt_Itemcode.Text;
                            dgvItemData.Rows[Rowindex].Cells["description"].Value = txtDescription.Text;
                            dgvItemData.Rows[Rowindex].Cells["barcode"].Value = txtbarcode.Text;
                            dgvItemData.Rows[Rowindex].Cells["Packing"].Value = txtPacking.Text; 
                            dgvItemData.Rows[Rowindex].Cells["note"].Value = cmbUnit.Text;
                            dgvItemData.Rows[Rowindex].Cells["GST"].Value = txtGst.Text;
                            dgvItemData.Rows[Rowindex].Cells["Cdiscount"].Value = textdisc.Text;//bahja
                            dgvItemData.Rows[Rowindex].Cells["IGST"].Value = txtIgst.Text;
                            dgvItemData.Rows[Rowindex].Cells["col_qty"].Value = txt_qty.Text;
                            dgvItemData.Rows[Rowindex].Cells["free"].Value = txt_free.Text;
                            dgvItemData.Rows[Rowindex].Cells["Unit_Cost"].Value = txtUnitCost.Text;
                            dgvItemData.Rows[Rowindex].Cells["Amount"].Value = txtAmount.Text;
                            dgvItemData.Rows[Rowindex].Cells["amt"].Value = amtUntchange.ToString();
                            dgvItemData.Rows[Rowindex].Cells["gstCal"].Value = ((Convert.ToDecimal(txtUnitCost.Text) * Convert.ToDecimal(txt_qty.Text) * Convert.ToDecimal(txtGst.Text)) / 100);
                            dgvItemData.Rows[Rowindex].Cells["igstCal"].Value = ((Convert.ToDecimal(txtUnitCost.Text) * Convert.ToDecimal(txt_qty.Text) * Convert.ToDecimal(txtIgst.Text)) / 100);
                            if (dtb.Rows[0]["ISBatch"].ToString() == "True")
                            {
                                if (dgvItemData.Rows.Count == 1)
                                {
                                    dgvGridData.Rows.Clear(); fill_Updategrid(); dt_forBatch.Clear();
                                }
                                else
                                {
                                    update_Grid(); fill_Updategrid();
                                    dt_forBatch.Clear();
                                }
                            }
                            else
                            {
                                if (dgvItemData.Rows.Count == 1)
                                {
                                    dgvGridData.Rows.Clear();
                                    fill_Updategrid();
                                }
                                else
                                {
                                    update_Grid(); fill_Updategrid();//clear();
                                }
                            }

                            foreach (DataGridViewRow row1 in dgvItemData.Rows)
                            {
                                if (row1.Cells["Amount"].Value != null && row1.Cells["Amount"].Value.ToString() != "")
                                {
                                     amount = Convert.ToDecimal(row1.Cells["Amount"].Value.ToString());
                                    amount1 = amount1 + amount;
                                    amt = Convert.ToDecimal(row1.Cells["amt"].Value.ToString());
                                    amt1 = amt1 + amt;
                                    if (row1.Cells["igst"].Value != null)
                                    {
                                        igstcal = Convert.ToDecimal(row1.Cells["igst"].Value.ToString());
                                        unitcostcal = Convert.ToDecimal(row1.Cells["Unit_Cost"].Value.ToString());
                                        qtycal = Convert.ToInt32(row1.Cells["col_qty"].Value.ToString());
                                        igstcalc = (((unitcostcal * qtycal) * igstcal) / 100);
                                        igstcalc1 = igstcalc1 + igstcalc;
                                    }
                                    if (row1.Cells["gst"].Value != null)
                                    {
                                        cgstcal = Convert.ToDecimal(row1.Cells["gst"].Value.ToString());
                                        cgstcalc = (((unitcostcal * qtycal) * cgstcal) / 100);
                                        cgstcalc1 = cgstcalc1 + cgstcalc;
                                        cgstcalc2 = cgstcalc1 / 2;
                                    }
                                }
                            }
                            txtTotal_item.Text = dgvItemData.Rows.Count.ToString();
                            txtIgstResult.Text = igstcalc1.ToString("#0.00");
                            txt_TotalAmount.Text = amount1.ToString("#0.00");
                            txtTotalCost.Text = amt1.ToString("#0.00");
                            txtCgst.Text = cgstcalc2.ToString("#0.00"); 
                            txtSgst.Text = cgstcalc2.ToString("#0.00");
                            clear();
                            Btn_itemCode.Enabled = true;
                            Btn_Add.Text = "Add";
                            BtnCancel.Visible = false;
                            txt_Itemcode.Enabled = true; total_batch_rate = 0;
                        }
                        else
                        {
                            MessageBox.Show("Does not add batch", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;

                        }
                    }
                    else if (Btn_Add.Text == "Add")
                    {
                        int qty = 0;decimal newcost = 0;
                        if (total_batch_rate != 0)
                        {
                            txtUnitCost.Text = total_batch_rate.ToString("0.00");
                            newcost = total_batch_rate;
                        }
                            
                        if (txtGst.Text != "0.0")
                        {
                            qtycal = Convert.ToInt32(txt_qty.Text);
                            gstcalc = (((newcost * qtycal) * gst) / 100);
                        }
                        else if (txtIgst.Text != "0.0")
                        {
                            qtycal = Convert.ToInt32(txt_qty.Text);
                            igstcalcc = (((newcost * qtycal) * igst) / 100);
                        }
                        if (dtb.Rows[0]["ISBatch"].ToString() == "true")
                        {
                            if (dt_forBatch != null)
                            {
                                //bahja.....add discount
                                dgvItemData.Rows.Add(Item_id, txt_Itemcode.Text, txtDescription.Text, txtbarcode.Text, txtPacking.Text, cmbUnit.Text, txtGst.Text, textdisc.Text, txtIgst.Text, txt_qty.Text, txt_free.Text, txtUnitCost.Text, txtAmount.Text, ttlamt, gstcalc, igstcalcc);
                               // MessageBox.Show("added");
                                //if (chk_consume.Checked == true)
                                //{
                                //    for (int i = 0; i < dt_forBatch.Rows.Count; i++)
                                //    {
                                //        if (dt_forBatch.Rows[i][0].ToString() != "")
                                //        {
                                //            dgvGridData.Rows.Add(Item_id, dt_forBatch.Rows[i]["Branch_No"].ToString(), dt_forBatch.Rows[i]["col_Unit"].ToString(), dt_forBatch.Rows[i]["Quantity"].ToString(), DateTime.Now.Date.ToString("yyyy-MM-dd"), dt_forBatch.Rows[i]["Exp_Date"].ToString(), dt_forBatch.Rows[i]["prd"].ToString());
                                //        }
                                //    }
                                //}
                                //else
                                //{
                                for (int i = 0; i < dt_forBatch.Rows.Count; i++)
                                {
                                    if (dt_forBatch.Rows[i][0].ToString() != "")//Purch_Rate
                                    {
                                        if(dt_forBatch.Rows[i]["sales_rate"].ToString()=="0" && dt_forBatch.Rows[i]["rate"].ToString() == "0")
                                        {
                                            DataTable dt_salesrate = this.cntrl.check_batch(Item_id);//Sales_Rate_Max
                                            dgvGridData.Rows.Add(Item_id, dt_forBatch.Rows[i]["Branch_No"].ToString(), dt_forBatch.Rows[i]["col_Unit"].ToString(), dt_forBatch.Rows[i]["Quantity"].ToString(), dt_salesrate.Rows[0]["Purch_Rate"].ToString(), dt_salesrate.Rows[0]["Sales_Rate_Max"].ToString(), dt_forBatch.Rows[i]["Prd_Date"].ToString(), dt_forBatch.Rows[i]["Exp_Date"].ToString(), dt_forBatch.Rows[i]["prd"].ToString(), "0");
                                        }
                                        else if(dt_forBatch.Rows[i]["rate"].ToString() == "0")
                                        {
                                            DataTable dt_salesrate = this.cntrl.check_batch(Item_id);//Sales_Rate_Max
                                            dgvGridData.Rows.Add(Item_id, dt_forBatch.Rows[i]["Branch_No"].ToString(), dt_forBatch.Rows[i]["col_Unit"].ToString(), dt_forBatch.Rows[i]["Quantity"].ToString(), dt_salesrate.Rows[0]["Purch_Rate"].ToString(), dt_salesrate.Rows[0]["sales_rate"].ToString(), dt_forBatch.Rows[i]["Prd_Date"].ToString(), dt_forBatch.Rows[i]["Exp_Date"].ToString(), dt_forBatch.Rows[i]["prd"].ToString(), "0");
                                        }
                                        else if (dt_forBatch.Rows[i]["sales_rate"].ToString() == "0")
                                        {
                                            DataTable dt_salesrate = this.cntrl.check_batch(Item_id);//Sales_Rate_Max
                                            dgvGridData.Rows.Add(Item_id, dt_forBatch.Rows[i]["Branch_No"].ToString(), dt_forBatch.Rows[i]["col_Unit"].ToString(), dt_forBatch.Rows[i]["Quantity"].ToString(), dt_forBatch.Rows[i]["rate"].ToString(), dt_salesrate.Rows[0]["Sales_Rate_Max"].ToString(), dt_forBatch.Rows[i]["Prd_Date"].ToString(), dt_forBatch.Rows[i]["Exp_Date"].ToString(), dt_forBatch.Rows[i]["prd"].ToString(), "0");
                                        }
                                        else
                                        {
                                            dgvGridData.Rows.Add(Item_id, dt_forBatch.Rows[i]["Branch_No"].ToString(), dt_forBatch.Rows[i]["col_Unit"].ToString(), dt_forBatch.Rows[i]["Quantity"].ToString(), dt_forBatch.Rows[i]["rate"].ToString(), dt_forBatch.Rows[i]["sales_rate"].ToString(), dt_forBatch.Rows[i]["Prd_Date"].ToString(), dt_forBatch.Rows[i]["Exp_Date"].ToString(), dt_forBatch.Rows[i]["prd"].ToString(), "0");
                                        }
                                        
                                    }
                                }
                                //}
                            }
                            else
                            {
                                MessageBox.Show("Does not add batch", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        else if (dtb.Rows[0]["ISBatch"].ToString() == "false")
                        {
                            if (dt_forBatch != null)
                            {
                                string Quantity = "";
                                //bahja.....add discount
                                dgvItemData.Rows.Add(Item_id, txt_Itemcode.Text, txtDescription.Text, txtbarcode.Text, txtPacking.Text, cmbUnit.Text, txtGst.Text,  textdisc.Text, txtIgst.Text, txt_qty.Text, txt_free.Text, txtUnitCost.Text, txtAmount.Text, ttlamt, gstcalc, igstcalcc);
                                if (txt_free.Text.ToString() != "")
                                {
                                    Quantity = (Convert.ToInt32(txt_free.Text) + Convert.ToInt32(txt_qty.Text)).ToString();//to find quantity + free quantity
                                }
                                //if (chk_consume.Checked == true)
                                //{
                                //    dgvGridData.Rows.Add(Item_id, dt_forBatch.Rows[0]["Branch_No"].ToString(), cmbUnit.Text, Quantity, DateTime.Now.Date.ToString("yyyy-MM-dd"), "","");

                                //}
                                //else
                                //{
                                dgvGridData.Rows.Add(Item_id, dt_forBatch.Rows[0]["Branch_No"].ToString(), cmbUnit.Text, Quantity, dt_forBatch.Rows[0]["rate"].ToString(), dt_forBatch.Rows[0]["sales_rate"].ToString(), dt_forBatch.Rows[0]["Prd_Date"].ToString(), dt_forBatch.Rows[0]["Exp_Date"].ToString(), dt_forBatch.Rows[0]["prd"].ToString(), "0");
                                //}
                            }
                            else
                            {
                                MessageBox.Show("Does not add batch", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        clear();
                        qty = dgvItemData.Rows.Count;
                        txtTotal_item.Text = qty.ToString();
                        foreach (DataGridViewRow row1 in dgvItemData.Rows)
                        {
                            if (row1.Cells["Amount"].Value != null && row1.Cells["Amount"].Value.ToString() != "")
                            {
                                amount = Convert.ToDecimal(row1.Cells["Amount"].Value.ToString());
                                amount1 = amount1 + amount;
                                if (Convert.ToDecimal(row1.Cells["amt"].Value) == 0)
                                {
                                    amt = unitcost * Qty;
                                }
                                else
                                {
                                    amt = Convert.ToDecimal(row1.Cells["amt"].Value.ToString());
                                }
                                amt1 = amt1 + amt;
                                if (row1.Cells["gstCal"].Value != null)
                                {
                                    cgstcalc = Convert.ToDecimal(row1.Cells["gstCal"].Value.ToString());
                                    cgstcalc1 = cgstcalc + cgstcalc1;
                                    cgstcalc2 = cgstcalc1 / 2;
                                }
                                else if (row1.Cells["gstCal"].Value == null)
                                {
                                    cgstcalc = gstcalc;
                                    cgstcalc1 = cgstcalc + cgstcalc1;
                                    cgstcalc2 = cgstcalc1 / 2;
                                }
                                if (row1.Cells["igstCal"].Value != null)
                                {
                                    igstcalc = Convert.ToDecimal(row1.Cells["igstCal"].Value.ToString());
                                    igstcalc1 = igstcalc + igstcalc1;
                                }
                                else if (row1.Cells["igstCal"].Value == null)
                                {
                                    igstcalc = igstcalcc;
                                    igstcalc1 = igstcalc + igstcalc1;
                                }
                            }
                        }
                        Btn_itemCode.Enabled = true;
                        txt_TotalAmount.Text = amount1.ToString("#0.00");
                        txtTotalCost.Text = amt1.ToString("#0.00");
                        txtIgstResult.Text = igstcalc1.ToString("#0.00");
                        txt_TotalAmount.Text = amount1.ToString("#0.00");
                        txtCgst.Text = cgstcalc2.ToString("#0.00");
                        txtSgst.Text = cgstcalc2.ToString("#0.00");
                        txtGrandTotal.Text = amount1.ToString("#0.00");
                        dt_forBatch = null; total_batch_rate = 0;
                    }
                }
                else if (txt_Itemcode.Text == "")
                {
                    MessageBox.Show("Item not found..", "Item Data Not Found...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (txt_qty.Text == "0")
                {
                    MessageBox.Show("Quantity not found", "Quantity Not Found..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (txtUnitCost.Text == "0.00")
                {
                    MessageBox.Show("Unit Cost not found..", "Cost Not Found..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (txtAmount.Text == "0.00")
                {
                    MessageBox.Show("Amount not found..", "Amount Not Found..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                txtbarcode.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void update_Grid()
        {
            DataTable dt_Update = new DataTable();
            dt_Update.Columns.Clear();
            dt_Update.Rows.Clear();
            dt_Update.Columns.Add("tempItem_code");
            dt_Update.Columns.Add("Branch_No");
            dt_Update.Columns.Add("Item_unit");
            dt_Update.Columns.Add("col_temp_qty");
            dt_Update.Columns.Add("rate");
            dt_Update.Columns.Add("sales_rate");
            dt_Update.Columns.Add("Prd_Date");
            dt_Update.Columns.Add("Exp_Date");
            dt_Update.Columns.Add("period");
            dt_Update.Columns.Add("batchentry");
            if (dt_Update.Columns.Count > 0)
            {
                //if (chk_consume.Checked == true)
                //{
                //    for (int i = 0; i < dgvGridData.Rows.Count; i++)
                //    {
                //        if (dgvGridData.Rows[i].Cells["tempItem_code"].Value.ToString() != Item_id)
                //        {
                //            dt_Update.Rows.Add(dgvGridData.Rows[i].Cells["tempItem_code"].Value.ToString(), dgvGridData.Rows[i].Cells["Branch_No"].Value.ToString(), dgvGridData.Rows[i].Cells["Item_unit"].Value.ToString(), dgvGridData.Rows[i].Cells["col_temp_qty"].Value.ToString(), DateTime.Now.Date.ToString("yyyy-MM-dd"), "", "");
                //        }
                //    }
                //}
                //else
                //{
                for (int i = 0; i < dgvGridData.Rows.Count; i++)
                {
                    if (dgvGridData.Rows[i].Cells["tempItem_code"].Value.ToString() != Item_id)//&& dgvGridData.Rows[i].Cells["Item_unit"].Value.ToString() != cmbUnit.Text
                    {
                        dt_Update.Rows.Add(dgvGridData.Rows[i].Cells["tempItem_code"].Value.ToString(), dgvGridData.Rows[i].Cells["Branch_No"].Value.ToString(), dgvGridData.Rows[i].Cells["Item_unit"].Value.ToString(), dgvGridData.Rows[i].Cells["col_temp_qty"].Value.ToString(), dgvGridData.Rows[i].Cells["rate"].Value.ToString(), dgvGridData.Rows[i].Cells["sales_rate"].Value.ToString(), dgvGridData.Rows[i].Cells["Prd_Date"].Value.ToString(), dgvGridData.Rows[i].Cells["Exp_Date"].Value.ToString(), dgvGridData.Rows[i].Cells["period"].Value.ToString(), dgvGridData.Rows[i].Cells["batchentry"].Value.ToString());
                    }
                    else if (dgvGridData.Rows[i].Cells["tempItem_code"].Value.ToString() == Item_id)
                    {
                        if (dgvGridData.Rows[i].Cells["Item_unit"].Value.ToString() != cmbUnit.Text)
                        {
                            dt_Update.Rows.Add(dgvGridData.Rows[i].Cells["tempItem_code"].Value.ToString(), dgvGridData.Rows[i].Cells["Branch_No"].Value.ToString(), dgvGridData.Rows[i].Cells["Item_unit"].Value.ToString(), dgvGridData.Rows[i].Cells["col_temp_qty"].Value.ToString(), dgvGridData.Rows[i].Cells["rate"].Value.ToString(), dgvGridData.Rows[i].Cells["sales_rate"].Value.ToString(), dgvGridData.Rows[i].Cells["Prd_Date"].Value.ToString(), dgvGridData.Rows[i].Cells["Exp_Date"].Value.ToString(), dgvGridData.Rows[i].Cells["period"].Value.ToString(), dgvGridData.Rows[i].Cells["batchentry"].Value.ToString());
                        }

                    }
                }
            }
            if (dt_Update.Rows.Count > 0)
            {
                dgvGridData.Rows.Clear();
                foreach (DataRow dr in dt_Update.Rows)
                {
                    dgvGridData.Rows.Add(dr["tempItem_code"].ToString(), dr["Branch_No"].ToString(), dr["Item_unit"].ToString(), Convert.ToInt32(dr["col_temp_qty"].ToString()), dr["rate"].ToString(), dr["sales_rate"].ToString(), dr["Prd_Date"].ToString(), dr["Exp_Date"].ToString(), dr["period"].ToString(), dr["batchentry"].ToString());
                }
            }
        }
        public void fill_Updategrid()
        {
            int rowindex = dgvGridData.Rows.Count;
            //if (chk_consume.Checked == true)
            //{
            //    for (int j = 0; j < dt_forBatch.Rows.Count; j++)
            //    {
            //        if (dt_forBatch.Rows[j]["Quantity"].ToString() != "")
            //        {
            //            dgvGridData.Rows.Add();
            //            dgvGridData.Rows[rowindex].Cells["tempItem_code"].Value = Item_id;
            //            dgvGridData.Rows[rowindex].Cells["Branch_No"].Value = dt_forBatch.Rows[j]["Branch_No"].ToString();
            //            dgvGridData.Rows[rowindex].Cells["Item_unit"].Value = dt_forBatch.Rows[j]["col_Unit"].ToString();
            //            dgvGridData.Rows[rowindex].Cells["col_temp_qty"].Value = dt_forBatch.Rows[j]["Quantity"].ToString();
            //            dgvGridData.Rows[rowindex].Cells["Prd_Date"].Value = DateTime.Now.Date.ToString("yyyy-MM-dd");
            //            dgvGridData.Rows[rowindex].Cells["Exp_Date"].Value = "";
            //            dgvGridData.Rows[rowindex].Cells["period"].Value = "";
            //        }
            //        rowindex++;
            //    }
            //}
            //else
            //{
            for (int j = 0; j < dt_forBatch.Rows.Count; j++)
            {
                if (dt_forBatch.Rows[j]["Quantity"].ToString() != "")
                {
                    dgvGridData.Rows.Add();
                    dgvGridData.Rows[rowindex].Cells["tempItem_code"].Value = Item_id;
                    dgvGridData.Rows[rowindex].Cells["Branch_No"].Value = dt_forBatch.Rows[j]["Branch_No"].ToString();
                    dgvGridData.Rows[rowindex].Cells["Item_unit"].Value = dt_forBatch.Rows[j]["col_Unit"].ToString();
                    dgvGridData.Rows[rowindex].Cells["col_temp_qty"].Value = dt_forBatch.Rows[j]["Quantity"].ToString();
                    dgvGridData.Rows[rowindex].Cells["rate"].Value = dt_forBatch.Rows[j]["rate"].ToString();
                    dgvGridData.Rows[rowindex].Cells["sales_rate"].Value = dt_forBatch.Rows[j]["sales_rate"].ToString();
                    dgvGridData.Rows[rowindex].Cells["Prd_Date"].Value = dt_forBatch.Rows[j]["Prd_Date"].ToString();
                    dgvGridData.Rows[rowindex].Cells["Exp_Date"].Value = dt_forBatch.Rows[j]["Exp_Date"].ToString();
                    dgvGridData.Rows[rowindex].Cells["period"].Value = dt_forBatch.Rows[j]["prd"].ToString();
                    if (dt_forBatch.Rows[j]["entryno"].ToString() == "")
                    {
                        dgvGridData.Rows[rowindex].Cells["batchentry"].Value = "0";
                    }
                    else
                        dgvGridData.Rows[rowindex].Cells["batchentry"].Value = dt_forBatch.Rows[j]["entryno"].ToString();
                }
                rowindex++;
            }
            //}
        }
        public void clear()
        {
            txt_Itemcode.Clear();
            txtDescription.Clear();
            txtPacking.Clear();
            txt_qty.Text = "0";
            txt_free.Text = "0";
            txtUnitCost.Text = "0.00";
            txtAmount.Text = "0.00";
            txtGst.Text = "0";
            txtIgst.Text = "0";
            cmbUnit.Text = "";
            txtbarcode.Text = "";
           textdisc.Text = "0";//bahja
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            txt_Itemcode.Clear();
            txtDescription.Clear();
            txtGst.Text = "0";
            txtIgst.Text = "0";
            txtPacking.Clear();
            txt_qty.Text = "0";
            txt_free.Clear();
            txtUnitCost.Text = "0.00";
            txtAmount.Text = "0.00";
            textdisc.Text = "0";//bahja
            cmbUnit.Text = ""; cmbUnit.Enabled = true;
            Btn_Add.Text = "Add"; BtnCancel.Visible = false;
            Btn_itemCode.Enabled = true; txt_Itemcode.Enabled = true;
            cmbUnit.Items.Clear(); txtbarcode.Enabled = true;
            txtDescription.Enabled = true;
            txtbarcode.Text = "";
        }

        private void dgvItemData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (Pur_List_flag != true)
                //{
                    if (e.RowIndex >= 0)
                    {
                        bool flagDel = false;
                        Rowindex = dgvItemData.CurrentRow.Index;
                        if (dgvItemData.Rows.Count > 0)
                        {
                        //decimal Qty = 0, IGST = 0, GST = 0, Cost = 0, TotalItems = 0, TotalCost = 0, TotalGst = 0, TotalAmount = 0, igstAmount = 0, gstAmount = 0, DiscAmount = 0;
                        if (dgvItemData.CurrentCell.OwningColumn.Name == "ItemEdit")
                            {
                                flagedit = true; 
                                txt_Itemcode.Enabled = false; txtbarcode.Enabled = false;
                                Btn_itemCode.Enabled = false; txtDescription.Enabled = false;
                                BtnCancel.Visible = true;
                                if (txt_Itemcode.Text == "")
                                {
                                    txt_Itemcode.Text = dgvItemData.Rows[Rowindex].Cells["itemid"].Value.ToString();
                                    txtDescription.Text = dgvItemData.Rows[Rowindex].Cells["description"].Value.ToString();
                                    txtbarcode.Text = dgvItemData.Rows[Rowindex].Cells["barcode"].Value.ToString();
                                if(dgvItemData.Rows[Rowindex].Cells["Cdiscount"].Value !=null && dgvItemData.Rows[Rowindex].Cells["Cdiscount"].Value.ToString()!="")
                                    textdisc.Text = dgvItemData.Rows[Rowindex].Cells["Cdiscount"].Value.ToString();
                                else
                                    textdisc.Text = "0.00";

                                txt_qty.Text = dgvItemData.Rows[Rowindex].Cells["col_qty"].Value.ToString();
                                    txt_free.Text = dgvItemData.Rows[Rowindex].Cells["free"].Value.ToString();
                                    txtUnitCost.Text = dgvItemData.Rows[Rowindex].Cells["Unit_Cost"].Value.ToString();
                                    txtAmount.Text = dgvItemData.Rows[Rowindex].Cells["Amount"].Value.ToString();
                                    txtGst.Text = dgvItemData.Rows[Rowindex].Cells["gst"].Value.ToString();
                                    txtIgst.Text = dgvItemData.Rows[Rowindex].Cells["igst"].Value.ToString();
                                if(dgvItemData.Rows[Rowindex].Cells["note"].Value.ToString()!=null)
                                      cmbUnit.Text = dgvItemData.Rows[Rowindex].Cells["note"].Value.ToString();
                                //bahja
                                //if(purchOrder_flag==true)
                                //   Btn_Add.Text = "Add";
                                //else
                                Btn_Add.Text = "Update";
                                txt_qty.Focus();
                                    //string dt = DateTime.Now.Date.ToString("yyyy-MM-dd");
                                    //DateTime Timeonly = DateTime.Now;
                                    //this.cntrl.save_log(doctor_id, "Purchase", "logged user edits purchase", dt, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Edit");
                                   
                                if(purchOrder_flag == true)
                                {
                                    cmbUnit.Enabled = true;
                                    Item_id = dgvItemData.Rows[Rowindex].Cells["id"].Value.ToString();
                                    DataTable dtitems = this.cntrl.Get_unites(Item_id);
                                    unitload(dtitems);
                                }
                                else
                                {
                                    cmbUnit.Text = dgvItemData.Rows[Rowindex].Cells["note"].Value.ToString();
                                    cmbUnit.Enabled = false;
                                }
                                    
                               
                                flagedit = false;
                            }
                        }
                        else if (dgvItemData.CurrentCell.OwningColumn.Name == "Del")
                        {
                            DialogResult res = MessageBox.Show("Are you sure you want to delete?", "Delete confirmation",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (res == DialogResult.No)
                            {
                            }
                            else
                            {
                                if (purchOrder_flag == true)
                                {
                                    if (txt_Itemcode.Text == "")
                                    {
                                        flagDel = true;
                                    }
                                    else
                                    {
                                        flagDel = false;
                                    }
                                }
                                else
                                {
                                    flagDel = true;
                                }
                                if (flagDel == true)
                                {
                                    DataTable dt_for_delete = new DataTable();
                                    dt_for_delete.Columns.Clear();
                                    dt_for_delete.Rows.Clear();
                                    dt_for_delete.Columns.Add("tempItem_code");
                                    dt_for_delete.Columns.Add("Branch_No");
                                    dt_for_delete.Columns.Add("Item_unit");
                                    dt_for_delete.Columns.Add("col_temp_qty");
                                    dt_for_delete.Columns.Add("Prd_Date");
                                    dt_for_delete.Columns.Add("Exp_Date");
                                    dt_for_delete.Columns.Add("period");
                                    dt_for_delete.Columns.Add("batchentry");
                                    //this.cntrl.save_log(doctor_id, "Purchase", "logged user deletes purchase", "Delete");
                                    string itemid = dgvItemData.CurrentRow.Cells["id"].Value.ToString();
                                    string itemcode = dgvItemData.CurrentRow.Cells["itemid"].Value.ToString();
                                    string unit = dgvItemData.CurrentRow.Cells["note"].Value.ToString();
                                    decimal amount = Convert.ToDecimal(dgvItemData.CurrentRow.Cells["Amount"].Value.ToString());
                                    decimal amt = Convert.ToDecimal(dgvItemData.CurrentRow.Cells["amt"].Value.ToString());
                                    decimal totalamount = Convert.ToDecimal(txtTotalCost.Text);
                                    decimal totalamt = Convert.ToDecimal(txt_TotalAmount.Text);
                                    decimal total = (totalamount - amt);
                                    txtTotalCost.Text = total.ToString("##0.00");
                                    txt_TotalAmount.Text = (totalamt - amount).ToString("##0.00");
                                    decimal gst = Convert.ToDecimal(dgvItemData.CurrentRow.Cells["gst"].Value.ToString());
                                    decimal igst = Convert.ToDecimal(dgvItemData.CurrentRow.Cells["igst"].Value.ToString());
                                    decimal unitcostDl = Convert.ToDecimal(dgvItemData.CurrentRow.Cells["Unit_Cost"].Value.ToString());
                                    int qtyDl = Convert.ToInt32(dgvItemData.CurrentRow.Cells["col_qty"].Value.ToString());
                                    decimal cgstDl = Convert.ToDecimal(txtSgst.Text);
                                    decimal newcgst = (((unitcostDl * qtyDl) * gst) / 100) / 2;
                                    txtCgst.Text = (cgstDl - newcgst).ToString("##0.00");
                                    txtSgst.Text = (cgstDl - newcgst).ToString("##0.00");
                                    decimal igstDL = Convert.ToDecimal(txtIgstResult.Text);
                                    decimal Igstnew = (((unitcostDl * qtyDl) * igst) / 100);
                                    txtIgstResult.Text = (igstDL - Igstnew).ToString();
                                    if (txt_TotalAmount.Text == "0.00")
                                    {
                                        txtDic.Text = "0.00";
                                    }
                                    dgvItemData.Rows.RemoveAt(Rowindex);
                                    foreach (DataGridViewRow row in dgvGridData.Rows)
                                    {
                                        if (row.Cells["tempItem_code"].Value.ToString() != itemid)//&& row.Cells["Item_unit"].Value.ToString() != unit
                                        {
                                            dt_for_delete.Rows.Add(row.Cells["tempItem_code"].Value, row.Cells["Branch_No"].Value, row.Cells["Item_unit"].Value, row.Cells["col_temp_qty"].Value, row.Cells["Prd_Date"].Value, row.Cells["Exp_Date"].Value, row.Cells["period"].Value, "0");
                                        }
                                        else if (row.Cells["tempItem_code"].Value.ToString() == itemid)
                                        {
                                            if (row.Cells["Item_unit"].Value.ToString() != unit)
                                            {
                                                dt_for_delete.Rows.Add(row.Cells["tempItem_code"].Value, row.Cells["Branch_No"].Value, row.Cells["Item_unit"].Value, row.Cells["col_temp_qty"].Value, row.Cells["Prd_Date"].Value, row.Cells["Exp_Date"].Value, row.Cells["period"].Value, "0");

                                            }
                                        }
                                    }
                                    if (dt_for_delete.Rows.Count > 0)
                                    {
                                        dgvGridData.Rows.Clear();
                                        for (int i = 0; i < dt_for_delete.Rows.Count; i++)
                                        {
                                            dgvGridData.Rows.Add();
                                            dgvGridData.Rows[i].Cells[0].Value = dt_for_delete.Rows[i]["tempItem_code"].ToString();
                                            dgvGridData.Rows[i].Cells[1].Value = dt_for_delete.Rows[i]["Branch_No"].ToString();
                                            dgvGridData.Rows[i].Cells[2].Value = dt_for_delete.Rows[i]["Item_unit"].ToString();
                                            dgvGridData.Rows[i].Cells[3].Value = dt_for_delete.Rows[i]["col_temp_qty"].ToString();
                                            dgvGridData.Rows[i].Cells[4].Value = dt_for_delete.Rows[i]["Prd_Date"].ToString();
                                            dgvGridData.Rows[i].Cells[5].Value = dt_for_delete.Rows[i]["Exp_Date"].ToString();
                                            dgvGridData.Rows[i].Cells[6].Value = dt_for_delete.Rows[i]["period"].ToString();
                                            dgvGridData.Rows[i].Cells[7].Value = dt_for_delete.Rows[i]["batchentry"].ToString();
                                        }
                                    }
                                    else
                                    {
                                        dgvGridData.Rows.Clear();
                                    }
                                }
                            }
                        }
                    }
                    if (dgvItemData.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow row in dgvItemData.Rows)
                        {
                            if (row.Cells["col_qty"].Value != null && row.Cells["col_qty"].Value.ToString() != "")
                            {
                                qty = dgvItemData.Rows.Count;
                            }
                        }
                        txtTotal_item.Text = qty.ToString();
                    }
                    else
                    {
                        txtTotal_item.Text = "0";
                        txtCgst.Text = "0.00";
                        txtSgst.Text = "0.00";
                        txtIgstResult.Text = "0.00";

                    }
                }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void unitload(DataTable dtitems)
        {
            if (dtitems.Rows.Count > 0)
            {
                txtPacking.Text = dtitems.Rows[0]["packing"].ToString();
                cmbUnit.Items.Clear();
                if (Convert.ToInt32(dtitems.Rows[0]["UnitMF"].ToString()) == 0)
                {
                    cmbUnit.Items.Add(dtitems.Rows[0]["Unit1"].ToString());
                    //cmbUnit.SelectedIndex = 0;
                }
                else
                {
                    cmbUnit.Items.Add(dtitems.Rows[0]["Unit1"].ToString());
                    cmbUnit.Items.Add(dtitems.Rows[0]["Unit2"].ToString());
                    //cmbUnit.SelectedIndex = 0;
                }
                if (cmbUnit.Items.Count >= 1)
                {
                    cmbUnit.SelectedIndex = 0;
                }
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (txtSupplierName.Text == "")
            {
                MessageBox.Show("Please select supplier", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSupplierName.Focus();
                return;
            }
            if (dgvItemData.Rows.Count == 0 || dgvGridData.Rows.Count==0)
            {
                MessageBox.Show("Please select Items and add batch for items", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string cs = PappyjoeMVC.Model.Connection.MyGlobals.trans_connectionstring;
            using (MySqlConnection con = new MySqlConnection(cs))
            {
                con.Open();
                MySqlTransaction trans = con.BeginTransaction();


                try
                {

                    //if (flag_save == true)
                    if (btn_Save.Text == "SAVE")
                    {
                        DataTable dtb = this.cntrl.incrementDocnumber(con, trans);
                        DocNumber_increment(dtb);
                    }
                    int newQty = 0;
                    int qty = 0;
                    int mf = 0; string expdate = "";
                    DataTable dtunit2 = new DataTable();
                    //if (btn_Save.Text == "SAVE")
                    //    check();
                    //else
                    //    flagcheck = true;
                    //if (flagcheck == true)
                    //{
                    if (txt_SupplierId.Text != "" && txtSupplierName.Text != "")
                    {
                        string IsExpDate = ""; 
                        DataTable dt = new DataTable();
                        DataTable batch_entry = new DataTable();
                        int i = 0;
                        if (btn_Save.Text == "SAVE")
                        {
                          if( rad_CashSale.Checked==true)
                            {
                                i = this.cntrl.save_purchase(txtPurchInvNumber.Text, txtinvoiceno.Text, dtpPurchDate.Value.ToString("yyyy-MM-dd"), txt_SupplierId.Text, txt_TotalAmount.Text, txtGrandTotal.Text, txtDic.Text, txt_Discount.Text, txtTotalCost.Text, "Purchase", type,"Cash" ,con, trans);

                            }
                            else
                            {
                                i = this.cntrl.save_purchase(txtPurchInvNumber.Text, txtinvoiceno.Text, dtpPurchDate.Value.ToString("yyyy-MM-dd"), txt_SupplierId.Text, txt_TotalAmount.Text, txtGrandTotal.Text, txtDic.Text, txt_Discount.Text, txtTotalCost.Text, "Purchase", type,"Credit", con, trans);

                            }

                        }
                        else
                        {
                            //if (rad_CashSale.Checked == true)
                            //{
                            //    i = this.cntrl.update_purchase(txtPurchInvNumber.Text, txtinvoiceno.Text, dtpPurchDate.Value.ToString("yyyy-MM-dd"), txt_SupplierId.Text, txt_TotalAmount.Text, txtGrandTotal.Text, txtDic.Text, txt_Discount.Text, txtTotalCost.Text, "Purchase", type,"Cash", con, trans);

                            //}
                            //else
                            //{
                                i = this.cntrl.update_purchase(txtPurchInvNumber.Text, txtinvoiceno.Text, dtpPurchDate.Value.ToString("yyyy-MM-dd"), txt_SupplierId.Text, txt_TotalAmount.Text, txtGrandTotal.Text, txtDic.Text, txt_Discount.Text, txtTotalCost.Text, "Purchase", type,"Credit", con, trans);

                            //}

                        }
                        if (i > 0)
                        {
                            int freeQty;
                            string discnt;
                            string pac, not;
                            foreach (DataGridViewRow dr in dgvItemData.Rows)
                            {
                                dtunit2 = this.cntrl.Get_unites(dr.Cells["id"].Value.ToString(), con, trans);
                                if (dr.Cells["free"].Value == null || dr.Cells["free"].Value.ToString() == "")
                                {
                                    freeQty = 0;
                                }
                                else
                                {
                                    freeQty = Convert.ToInt32(dr.Cells["free"].Value.ToString());
                                }
                                if (dtunit2.Rows.Count > 0)
                                {
                                    if (dtunit2.Rows[0]["Unit2"].ToString()==null || dr.Cells["note"].Value==null)
                                    {
                                        unit2Is = "No";
                                    }
                                    else
                                    {
                                        if (dtunit2.Rows[0]["Unit2"].ToString()!=null)
                                        {
                                            unit2Is = "Yes";
                                        }
                                        else
                                        {
                                            unit2Is = "No";
                                        }
                                    }

                                }
                                //bhj......
                                if (dr.Cells["Packing"].Value == null || dr.Cells["Packing"].Value.ToString() == "")
                                {
                                    pac = "";

                                }
                                else
                                {
                                    pac = dr.Cells["Packing"].Value.ToString();
                                }
                                if (dr.Cells["Cdiscount"].Value == null || dr.Cells["Cdiscount"].Value.ToString() == "")
                                {
                                    discnt = "";
                                }
                                else
                                {
                                    discnt = dr.Cells["Cdiscount"].Value.ToString();
                                }
                                if (dr.Cells["note"].Value == null || dr.Cells["note"].Value.ToString() == "")
                                {
                                    not = "";

                                }
                                else
                                {
                                    not = dr.Cells["note"].Value.ToString();
                                }
                                //........bhj

                                if (btn_Save.Text == "SAVE")
                                {

                                    this.cntrl.save_purchaseit(txtPurchInvNumber.Text, dtpPurchDate.Value.ToString("yyyy-MM-dd"), dr.Cells["id"].Value.ToString(), dr.Cells["description"].Value.ToString(), dr.Cells["barcode"].Value.ToString(),pac,discnt,not, dr.Cells["col_qty"].Value.ToString(), freeQty, dr.Cells["Unit_Cost"].Value.ToString(), dr.Cells["Amount"].Value.ToString(), unit2Is, dtunit2.Rows[0]["UnitMF"].ToString(), Convert.ToDecimal(dr.Cells["gst"].Value.ToString()), Convert.ToDecimal(dr.Cells["igst"].Value.ToString()), con, trans); ;

                                }
                                else if (btn_Save.Text == "UPDATE")
                                {
                                    this.cntrl.update_purchaseit(txtPurchInvNumber.Text, dtpPurchDate.Value.ToString("yyyy-MM-dd"), dr.Cells["id"].Value.ToString(), dr.Cells["description"].Value.ToString(), dr.Cells["barcode"].Value.ToString(), dr.Cells["Packing"].Value.ToString(), dr.Cells["Cdiscount"].Value.ToString(), dr.Cells["note"].Value.ToString(), dr.Cells["col_qty"].Value.ToString(), freeQty, dr.Cells["Unit_Cost"].Value.ToString(), dr.Cells["Amount"].Value.ToString(), unit2Is, dtunit2.Rows[0]["UnitMF"].ToString(), Convert.ToDecimal(dr.Cells["gst"].Value.ToString()), Convert.ToDecimal(dr.Cells["igst"].Value.ToString()), con, trans);

                                }
                            }
                            int a = 0;
                            decimal tempqty;
                            for (int l = 0; l < dgvGridData.Rows.Count; l++)
                            {
                                if (dgvGridData.Rows[l].Cells["Exp_Date"].Value.ToString() != "" && dgvGridData.Rows[l].Cells["Exp_Date"].Value.ToString() != null)
                                {
                                    IsExpDate = "Yes";
                                    expdate = Convert.ToDateTime(dgvGridData.Rows[l].Cells["Exp_Date"].Value.ToString()).ToString("yyyy-MM-dd");
                                }
                                else
                                {
                                    IsExpDate = "NO";
                                    expdate = "";
                                }
                                dt = this.cntrl.Get_unites(dgvGridData.Rows[l].Cells["tempItem_code"].Value.ToString(), con, trans);
                                if (dt.Rows.Count > 0)
                                {
                                    if (dt.Rows[0]["Unit2"].ToString() == dgvGridData.Rows[l].Cells["Item_unit"].Value.ToString())
                                    {
                                        unit2Is = "Yes";
                                    }
                                    else
                                    {
                                        unit2Is = "No";
                                    }
                                }
                                if (dt.Rows[0]["Unit2"].ToString() != "null")
                                {
                                    if (unit2Is == "No")
                                    {
                                        mf = Convert.ToInt32(dt.Rows[0]["UnitMF"].ToString());
                                        qty = Convert.ToInt32(dgvGridData.Rows[l].Cells["col_temp_qty"].Value.ToString());
                                        newQty = Convert.ToInt32(mf * qty);
                                    }
                                    else
                                    {
                                        newQty = Convert.ToInt32(dgvGridData.Rows[l].Cells["col_temp_qty"].Value.ToString());
                                    }
                                }
                                else
                                {
                                    newQty = Convert.ToInt32(dgvGridData.Rows[l].Cells["col_temp_qty"].Value.ToString());
                                }
                                if (btn_Save.Text == "SAVE")
                                {
                                    decimal total_batch_qty = 0;
                                    DataTable batch = this.cntrl.check_have_same_batch(dgvGridData.Rows[l].Cells["tempItem_code"].Value.ToString(), dgvGridData.Rows[l].Cells["Branch_No"].Value.ToString(), con, trans);
                                    if(batch.Rows.Count>0)
                                    {
                                        total_batch_qty = newQty +Convert.ToDecimal( batch.Rows[0]["Qty"].ToString());
                                        a = this.cntrl.update_same_batchNumber(dgvGridData.Rows[l].Cells["tempItem_code"].Value.ToString(), dgvGridData.Rows[l].Cells["Branch_No"].Value.ToString(),Convert.ToInt32( total_batch_qty),  unit2Is, dt.Rows[0]["UnitMF"].ToString(), txtPurchInvNumber.Text, Convert.ToDateTime(dgvGridData.Rows[l].Cells["Prd_Date"].Value.ToString()).ToString("yyyy-MM-dd"), expdate, dgvGridData.Rows[l].Cells["period"].Value.ToString(), txt_SupplierId.Text, Convert.ToDateTime(dtpPurchDate.Value.ToString()).ToString("yyyy-MM-dd"), IsExpDate, type, batch.Rows[0]["Entry_No"].ToString(), con, trans);//Convert.ToDecimal(dgvGridData.Rows[l].Cells["rate"].Value.ToString()),
                                    }
                                    else
                                    {
                                        a = this.cntrl.save_batchNumber(dgvGridData.Rows[l].Cells["tempItem_code"].Value.ToString(), dgvGridData.Rows[l].Cells["Branch_No"].Value.ToString(), newQty, Convert.ToDecimal(dgvGridData.Rows[l].Cells["rate"].Value.ToString()), Convert.ToDecimal(dgvGridData.Rows[l].Cells["sales_rate"].Value.ToString()), unit2Is, unit2Is, dt.Rows[0]["UnitMF"].ToString(), txtPurchInvNumber.Text, Convert.ToDateTime(dgvGridData.Rows[l].Cells["Prd_Date"].Value.ToString()).ToString("yyyy-MM-dd"), expdate, dgvGridData.Rows[l].Cells["period"].Value.ToString(), txt_SupplierId.Text, dtpPurchDate.Value.ToString("yyyy-MM-dd"), IsExpDate, type, con, trans);//sales_rate
                                    }

                                   

                                }
                                else if (btn_Save.Text == "UPDATE")
                                {
                                    a = this.cntrl.update_batchNumber(dgvGridData.Rows[l].Cells["tempItem_code"].Value.ToString(), dgvGridData.Rows[l].Cells["Branch_No"].Value.ToString(), newQty, Convert.ToDecimal(dgvGridData.Rows[l].Cells["rate"].Value.ToString()), Convert.ToDecimal(dgvGridData.Rows[l].Cells["sales_rate"].Value.ToString()), unit2Is, unit2Is, dt.Rows[0]["UnitMF"].ToString(), txtPurchInvNumber.Text, dgvGridData.Rows[l].Cells["Prd_Date"].Value.ToString(), expdate, dgvGridData.Rows[l].Cells["period"].Value.ToString(), txt_SupplierId.Text, Convert.ToDateTime( dtpPurchDate.Value.ToString()).ToString("yyyy-MM-dd"), IsExpDate, type, dgvGridData.Rows[l].Cells["batchentry"].Value.ToString(), con, trans);

                                }
                                if (a > 0)
                                {
                                    batch_entry = this.cntrl.get_maxEntryNo(con, trans);
                                    if (batch_entry != null)
                                    {
                                        if (dt.Rows[0]["Unit2"].ToString() != "null")
                                        {
                                            if (unit2Is == "No")
                                            {
                                                tempqty = Convert.ToDecimal(dgvGridData.Rows[l].Cells["col_temp_qty"].Value.ToString());
                                                tempqty = tempqty * Convert.ToDecimal(dt.Rows[0]["UnitMF"].ToString());
                                            }
                                            else
                                            {
                                                tempqty = Convert.ToDecimal(dgvGridData.Rows[l].Cells["col_temp_qty"].Value.ToString());
                                            }
                                        }
                                        else
                                        {
                                            tempqty = Convert.ToDecimal(dgvGridData.Rows[l].Cells["col_temp_qty"].Value.ToString());
                                        }
                                        if (btn_Save.Text == "SAVE")
                                        {
                                            this.cntrl.save_batchpurchase(txtPurchInvNumber.Text, dtpPurchDate.Value.ToString("yyyy-MM-dd"), txt_SupplierId.Text, dgvGridData.Rows[l].Cells["tempItem_code"].Value.ToString(), dgvGridData.Rows[l].Cells["Branch_No"].Value.ToString(), tempqty, Convert.ToDecimal(dgvGridData.Rows[l].Cells["rate"].Value.ToString()), unit2Is, dt.Rows[0]["UnitMF"].ToString(), Convert.ToDateTime(dgvGridData.Rows[l].Cells["Prd_Date"].Value.ToString()).ToString("yyyy-MM-dd"), expdate, IsExpDate, batch_entry.Rows[0][0].ToString(), con, trans);

                                        }
                                        else if (btn_Save.Text == "UPDATE")
                                        {
                                            this.cntrl.update_batchpurchase(txtPurchInvNumber.Text, dtpPurchDate.Value.ToString("yyyy-MM-dd"), txt_SupplierId.Text, dgvGridData.Rows[l].Cells["tempItem_code"].Value.ToString(), dgvGridData.Rows[l].Cells["Branch_No"].Value.ToString(), tempqty, Convert.ToDecimal(dgvGridData.Rows[l].Cells["rate"].Value.ToString()), unit2Is, dt.Rows[0]["UnitMF"].ToString(), dgvGridData.Rows[l].Cells["Prd_Date"].Value.ToString(), expdate, IsExpDate, dgvGridData.Rows[l].Cells["batchentry"].Value.ToString(), con, trans);

                                        }
                                    }
                                }
                            }

                            //Update_Itemstable(con, trans);
                            if (purchOrder_flag == true)
                            {
                                this.cntrl.update_purchaseorder(Pur_order_no1, con, trans);
                            }
                            if (btn_Save.Text == "SAVE")
                            {
                                //this.cntrl.save_log(doctor_id, "Purchase", " Adds Purchase", dt1, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Add");
                                //DialogResult res = MessageBox.Show("Data inserted Successfully,Do you want to print ?", "Success ",
                                //MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                //if (res == DialogResult.Yes)
                                //{
                                //print();
                                if (rad_CashSale.Checked == true)
                                {
                                    var form2 = new Supplier_invoice(con, trans);
                                    form2.amount = Convert.ToDecimal(txtGrandTotal.Text);
                                    form2.supname = txtSupplierName.Text;
                                    form2.supcode = txt_SupplierId.Text;
                                    form2.purno = txtPurchInvNumber.Text;
                                    form2.doctor_id = doctor_id;
                                    form2.ShowDialog();
                                    //supplier_flag = true;

                                }
                                else
                                {
                                    //MessageBox.Show("Data inserted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    DialogResult res = MessageBox.Show("Data inserted Successfully,Do you want to print ?", "Success ",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (res == DialogResult.Yes)
                                    {
                                        print();
                                        //print_thermal();
                                    }
                                }
                            }
                        }
                        if (btn_Save.Text == "SAVE")
                        {
                            if (rad_CashSale.Checked == true)
                            {
                                if (PappyjoeMVC.Model.GlobalVariables.supplier_inv_flag == true)
                                {
                                    trans.Commit();
                                    con.Close(); PappyjoeMVC.Model.GlobalVariables.supplier_inv_flag = false;
                                }
                                else
                                {
                                    trans.Rollback(); PappyjoeMVC.Model.GlobalVariables.supplier_inv_flag = false;
                                    MessageBox.Show("Please add receipt!.. Please try again ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            else if (rad_CreditSale.Checked == true)
                            {
                                trans.Commit();
                                con.Close();
                            }
                        }
                        else if (btn_Save.Text == "UPDATE")
                        {
                            if (rad_CreditSale.Checked == true)
                            {
                                trans.Commit();
                                con.Close();
                            }
                        }

                        //else
                        //{

                            //}

                    }
                    else
                    {
                        txtSupplierName.Focus();
                        MessageBox.Show("Please select supplier", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    string dt1 = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    DateTime Timeonly = DateTime.Now;
                    if (btn_Save.Text == "SAVE")
                    {
                        this.cntrl.save_log(doctor_id, "Purchase", " Adds Purchase", dt1, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Add", txtPurchInvNumber.Text);
                      
                    }
                    else
                    {
                        this.cntrl.save_log(doctor_id, "Purchase", " EDit Purchase", dt1, Convert.ToString(Timeonly.ToString("hh:mm tt")), "Edit", txtPurchInvNumber.Text);
                        DialogResult res = MessageBox.Show("Data Updated Successfully,Do you want to print ?", "Success ",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (res == DialogResult.Yes)
                        {
                             print();
                           // print_thermal();
                        }
                        else
                        {
                        }

                    }
                    DataTable dtbt = this.cntrl.incrementDocnumber();
                    DocNumber_increment(dtbt);
                    clear();
                    dgvItemData.Rows.Clear();
                    dgvGridData.Rows.Clear();
                    GF.ResetAll_GroupBox(gbDocumentDetails);
                    txtDic.Text = "0.00";
                    txt_Discount.Text = "0";
                    txtGrandTotal.Text = "0.00";
                    txtTotalCost.Text = "0.00";
                    txtTotal_item.Text = "0";
                    txt_TotalAmount.Text = "0.00";
                    txtCgst.Text = "0";
                    txtSgst.Text = "0";
                    txtIgstResult.Text = "0.00";
                    txtSupplierName.Clear();
                    txt_SupplierId.Clear(); flag_save = false;
                    txtinvoiceno.Text = "";
                    if(purchOrder_flag==true)
                    {
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

        }
        public void Update_Itemstable(MySqlConnection con, MySqlTransaction trans)
        {
            //try
            //{
            string percen_SalesRate = "0", percen_SalesMin = "0", percen_SalesMax = "0", percen_SalesRate1 = "0", percen_SalesMin1 = "0", percen_SalesMax1 = "0"; decimal value1 = 0; decimal value2 = 0;
            decimal Sales1 = 0, purchaserate2 = 0; decimal SalesMin = 0; decimal SalesMax = 0; decimal Sales2 = 0; decimal SalesMin1 = 0; decimal SalesMax1 = 0;
            decimal Sales1_ = 0; decimal SalesMin_ = 0; decimal SalesMax_ = 0; decimal Sales2_ = 0; decimal SalesMin1_ = 0; decimal SalesMax1_ = 0; string note;
            for (int i = 0; i < dgvItemData.Rows.Count; i++)
            {
                DataTable dtb_cost = this.cntrl.get_itemdetails(dgvItemData.Rows[i].Cells["id"].Value.ToString(), con, trans);
                DataTable dtb_Avg = this.cntrl.Get_itemdetails_from_purchaseit(dgvItemData.Rows[i].Cells["id"].Value.ToString(), con, trans);
                if (dtb_cost.Rows.Count > 0)
                {
                    decimal costbase1 = 0, unitcost = 0;
                    unitcost = Convert.ToDecimal(dgvItemData.Rows[i].Cells["Unit_Cost"].Value.ToString());
                    if (Convert.ToDecimal(dtb_cost.Rows[0]["Purch_Rate"].ToString()) != unitcost)
                    {
                        value1 = Convert.ToDecimal(dtb_cost.Rows[0]["Sales_Rate"].ToString()) - Convert.ToDecimal(dtb_cost.Rows[0]["Purch_Rate"].ToString());
                        value2 = (value1 / Convert.ToDecimal(dtb_cost.Rows[0]["Purch_Rate"].ToString())) * 100;
                        percen_SalesRate = Convert.ToDecimal(value2).ToString("#0.00");
                        value1 = Convert.ToDecimal(dtb_cost.Rows[0]["Sales_Rate_min"].ToString()) - Convert.ToDecimal(dtb_cost.Rows[0]["Purch_Rate"].ToString());
                        value2 = (value1 / Convert.ToDecimal(dtb_cost.Rows[0]["Purch_Rate"].ToString())) * 100;
                        percen_SalesMin = Convert.ToDecimal(value2).ToString("#0.00");
                        value1 = Convert.ToDecimal(dtb_cost.Rows[0]["Sales_Rate_Max"].ToString()) - Convert.ToDecimal(dtb_cost.Rows[0]["Purch_Rate"].ToString());
                        value2 = (value1 / Convert.ToDecimal(dtb_cost.Rows[0]["Purch_Rate"].ToString())) * 100;
                        percen_SalesMax = Convert.ToDecimal(value2).ToString("#0.00");
                        //bhj
                        if (dgvItemData.Rows[i].Cells["note"].Value == null || dgvItemData.Rows[i].Cells["note"].Value.ToString() == "")
                        {
                            note = "";
                        }
                        else
                        {
                            note = dgvItemData.Rows[i].Cells["note"].Value.ToString();
                        }
                        if (dtb_cost.Rows[0]["Unit1"].ToString() == note)//bhj  note
                        {
                            Sales1 = (Convert.ToDecimal(unitcost) * Convert.ToDecimal(percen_SalesRate)) / 100;
                            Sales1_ = Convert.ToDecimal(unitcost) + Sales1;
                            SalesMin = (Convert.ToDecimal(unitcost) * Convert.ToDecimal(percen_SalesMin)) / 100;
                            SalesMin_ = Convert.ToDecimal(unitcost) + SalesMin;
                            SalesMax = (Convert.ToDecimal(unitcost) * Convert.ToDecimal(percen_SalesMax)) / 100;
                            SalesMax_ = Convert.ToDecimal(unitcost) + SalesMax;
                            costbase1 = Convert.ToDecimal(dgvItemData.Rows[i].Cells["col_qty"].Value.ToString());
                            if (Convert.ToDecimal(dtb_cost.Rows[0]["UnitMF"].ToString()) > 0)
                            {
                                purchaserate2 = Convert.ToDecimal(unitcost) / Convert.ToDecimal(dtb_cost.Rows[0]["UnitMF"].ToString());
                                value1 = Convert.ToDecimal(dtb_cost.Rows[0]["Sales_Rate2"].ToString()) - Convert.ToDecimal(dtb_cost.Rows[0]["Purch_Rate2"].ToString());
                                value2 = (value1 / Convert.ToDecimal(dtb_cost.Rows[0]["Purch_Rate2"].ToString())) * 100;
                                percen_SalesRate1 = Convert.ToDecimal(value2).ToString("#0.00");
                                value1 = Convert.ToDecimal(dtb_cost.Rows[0]["Sales_Rate_min2"].ToString()) - Convert.ToDecimal(dtb_cost.Rows[0]["Purch_Rate2"].ToString());
                                value2 = (value1 / Convert.ToDecimal(dtb_cost.Rows[0]["Purch_Rate2"].ToString())) * 100;
                                percen_SalesMin1 = Convert.ToDecimal(value2).ToString("#0.00");
                                value1 = Convert.ToDecimal(dtb_cost.Rows[0]["Sales_Rate_Max2"].ToString()) - Convert.ToDecimal(dtb_cost.Rows[0]["Purch_Rate2"].ToString());
                                value2 = (value1 / Convert.ToDecimal(dtb_cost.Rows[0]["Purch_Rate2"].ToString())) * 100;
                                percen_SalesMax1 = Convert.ToDecimal(value2).ToString("#0.00");
                                Sales2 = (Convert.ToDecimal(purchaserate2) * Convert.ToDecimal(percen_SalesRate1)) / 100;
                                Sales2_ = Convert.ToDecimal(purchaserate2) + Sales2;
                                SalesMin1 = (Convert.ToDecimal(purchaserate2) * Convert.ToDecimal(percen_SalesMin1)) / 100;
                                SalesMin1_ = Convert.ToDecimal(purchaserate2) + SalesMin1;
                                SalesMax1 = (Convert.ToDecimal(purchaserate2) * Convert.ToDecimal(percen_SalesMax1)) / 100;
                                SalesMax1_ = Convert.ToDecimal(purchaserate2) + SalesMax1;
                            }
                            this.cntrl.update_itemtable(unitcost, Sales1_, SalesMin_, SalesMax_, costbase1, purchaserate2, Sales2_, SalesMin1_, SalesMax1_, dgvItemData.Rows[i].Cells["id"].Value.ToString(), con, trans);
                        }
                        else if (dtb_cost.Rows[0]["Unit2"].ToString() == note)//bhj  note
                        {
                            value1 = Convert.ToDecimal(dtb_cost.Rows[0]["Sales_Rate2"].ToString()) - Convert.ToDecimal(dtb_cost.Rows[0]["Purch_Rate2"].ToString());
                            value2 = (value1 / Convert.ToDecimal(dtb_cost.Rows[0]["Purch_Rate2"].ToString())) * 100;
                            percen_SalesRate1 = Convert.ToDecimal(value2).ToString("#0.00");
                            value1 = Convert.ToDecimal(dtb_cost.Rows[0]["Sales_Rate_min2"].ToString()) - Convert.ToDecimal(dtb_cost.Rows[0]["Purch_Rate2"].ToString());
                            value2 = (value1 / Convert.ToDecimal(dtb_cost.Rows[0]["Purch_Rate2"].ToString())) * 100;
                            percen_SalesMin1 = Convert.ToDecimal(value2).ToString("#0.00");
                            value1 = Convert.ToDecimal(dtb_cost.Rows[0]["Sales_Rate_Max2"].ToString()) - Convert.ToDecimal(dtb_cost.Rows[0]["Purch_Rate2"].ToString());
                            value2 = (value1 / Convert.ToDecimal(dtb_cost.Rows[0]["Purch_Rate2"].ToString())) * 100;
                            percen_SalesMax1 = Convert.ToDecimal(value2).ToString("#0.00");
                            Sales2 = (Convert.ToDecimal(unitcost) * Convert.ToDecimal(percen_SalesRate1)) / 100;
                            Sales2_ = Convert.ToDecimal(unitcost) + Sales2;
                            SalesMin1 = (Convert.ToDecimal(unitcost) * Convert.ToDecimal(percen_SalesMin1)) / 100;
                            SalesMin1_ = Convert.ToDecimal(unitcost) + SalesMin1;
                            SalesMax1 = (Convert.ToDecimal(unitcost) * Convert.ToDecimal(percen_SalesMax1)) / 100;
                            SalesMax1_ = Convert.ToDecimal(unitcost) + SalesMax1;
                            purchaserate2 = Convert.ToDecimal(unitcost) * Convert.ToDecimal(dtb_cost.Rows[0]["UnitMF"].ToString());
                            Sales1 = (Convert.ToDecimal(purchaserate2) * Convert.ToDecimal(percen_SalesRate)) / 100;
                            Sales1_ = Convert.ToDecimal(purchaserate2) + Sales1;
                            SalesMin = (Convert.ToDecimal(purchaserate2) * Convert.ToDecimal(percen_SalesMin)) / 100;
                            SalesMin_ = Convert.ToDecimal(purchaserate2) + SalesMin;
                            SalesMax = (Convert.ToDecimal(purchaserate2) * Convert.ToDecimal(percen_SalesMax)) / 100;
                            SalesMax_ = Convert.ToDecimal(purchaserate2) + SalesMax;
                            costbase1 = Convert.ToDecimal(dgvItemData.Rows[i].Cells["col_qty"].Value.ToString()) / Convert.ToDecimal(dtb_cost.Rows[0]["UnitMF"].ToString());
                            this.cntrl.update_itemtable(purchaserate2, Sales1_, SalesMin_, SalesMax_, costbase1, unitcost, Sales2_, SalesMin1_, SalesMax1_, dgvItemData.Rows[i].Cells["id"].Value.ToString(), con, trans);
                        }
                    }
                    else
                    { }
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        public void check()
        {
            for (int j = 0; j < dgvItemData.Rows.Count; j++)
            {

                for (int k = 0; k < dgvGridData.Rows.Count; k++)
                {
                    if (dgvItemData.Rows[j].Cells["id"].Value.ToString() == dgvGridData.Rows[k].Cells["tempItem_code"].Value.ToString())
                    {
                        flagcheck = true;
                    }
                    else
                    {
                        flagcheck = false;
                    }
                }
            }
        }

        public void DocNumber_increment(DataTable dtb)
        {

            if (String.IsNullOrWhiteSpace(dtb.Rows[0][0].ToString()))
            {
                txtPurchInvNumber.Text = "1";
            }
            else
            {
                int Count = Convert.ToInt32(dtb.Rows[0][0]);
                int incrValue = Convert.ToInt32(Count);
                incrValue += 1;
                txtPurchInvNumber.Text = incrValue.ToString();
            }
        }
        private void print()
        {
            string message = "Do you want Header on Print?";
            string caption = "Verification";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons);
            int c = 0;
            string strclinicname = "";
            string strStreet = "";
            string stremail = "";
            string strwebsite = "";
            string strphone = "";
            string path = "";
            string logo_name = "";
            try
            {
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    DataTable print_settng = this.prnt_ctrl.Get_inventory_id();
                    System.Data.DataTable dtp = this.cntrl.get_companydetails();
                    if (dtp.Rows.Count > 0)
                    {

                        if (dtp.Rows.Count > 0)
                        {
                            string clinicn = "";
                            //clinicn = dtp.Rows[0]["name"].ToString();
                            if (print_settng.Rows.Count > 0)
                            {
                                clinicn = print_settng.Rows[0]["header"].ToString();
                                strclinicname = clinicn.Replace("¤", "'");
                                strStreet = print_settng.Rows[0]["left_text"].ToString(); 
                                strphone = print_settng.Rows[0]["right_text"].ToString();
                            }
                            stremail = dtp.Rows[0]["email"].ToString();
                            strwebsite = dtp.Rows[0]["website"].ToString();
                            path = dtp.Rows[0]["path"].ToString();
                            logo_name = path;
                        }
                    }
                }
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                StreamWriter sWrite = new StreamWriter(Apppath + "\\Purchase.html");
                sWrite.WriteLine("<html>");
                sWrite.WriteLine("<head>");
                sWrite.WriteLine("<style>");
                sWrite.WriteLine("table { border-collapse: collapse;}");
                sWrite.WriteLine("p.big {line-height: 400%;}");
                sWrite.WriteLine("</style>");
                sWrite.WriteLine("</head>");
                sWrite.WriteLine("<body >");
                sWrite.WriteLine("<div>");
                sWrite.WriteLine("<table align=center width=900>");
                sWrite.WriteLine("<col >");
                sWrite.WriteLine("<br>");
                string Appath = System.IO.Directory.GetCurrentDirectory();
                if (File.Exists(Appath + "\\" + logo_name))
                {
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td width='30px' height='50px' align='left' rowspan='3'><img src='" + Appath + "\\" + logo_name + "'style='width:70px;height:70px;' ></td>  ");
                    sWrite.WriteLine("<td width='870px' align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=4><b>&nbsp;" + strclinicname + "</font> <br><FONT  COLOR=black  face='Segoe UI' SIZE=2>&nbsp;" + strStreet + "<br>&nbsp;" + strphone + " </b></td></tr>");
                    sWrite.WriteLine("</table>");
                }
                else
                {
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td  align='left' height='20px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + strclinicname + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td  align='left' height='20px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;" + strStreet + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' height='20px' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + strphone + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                    sWrite.WriteLine("</table>");
                }
                sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                //sWrite.WriteLine("<tr><td align='left'  ><hr/></td></tr>");
                //sWrite.WriteLine("</table>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3> PURCHASE INVOICE </font></center></b></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td  colspan=11  align='left'><left><FONT COLOR=black FACE='Segoe UI'  SIZE=2> Printed Date : " + dtpPurchDate.Value.ToString("d/MM/yyyy") + " </font></left></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan=11  align='left'><left><FONT COLOR=black FACE='Segoe UI'  SIZE=2>   Purchase No :" + txtPurchInvNumber.Text + " </font></left></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan=11  align='left'><left><FONT COLOR=black FACE='Segoe UI'  SIZE=2>   Invoice No :" + txtinvoiceno.Text + " </font></left></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan=11 align='left'><left><FONT COLOR=black FACE='Segoe UI'  SIZE=2>   Supplier Name : " + txtSupplierName.Text + " </font></left></td>");
                sWrite.WriteLine("</tr>");
                if (dgvItemData.Rows.Count > 0)
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("    <td align='center' width='230px' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Sl.No.</font></th>");
                    sWrite.WriteLine("    <td align='left' width='230px' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3>Item Code</font></th>");
                    sWrite.WriteLine("    <td align='left' width='230px' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Item Name</font></th>");
                    sWrite.WriteLine("    <td align='left' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Barcode</font></th>");
                    sWrite.WriteLine("    <td align='left' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Packing</font></th>");
                    sWrite.WriteLine("    <td align='left' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Unit</font></th>");
                    //sWrite.WriteLine("    <td align='left' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Batch</font></th>");
                    sWrite.WriteLine("    <td align='Right' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Quantity</font></th>");
                    sWrite.WriteLine("    <td align='Right' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Free</font></th>");
                    sWrite.WriteLine("    <td align='Right' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3>Unit Cost</font></th>");
                    sWrite.WriteLine("    <td align='Right' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Discount</font></th>");
                    //sWrite.WriteLine("    <td align='Right' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>SGST(%)</font></th>");
                    //sWrite.WriteLine("    <td align='Right' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>CGST(%)</font></th>");
                    sWrite.WriteLine("    <td align='Right' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>GST(%)</font></th>");
                    sWrite.WriteLine("    <td align='Right' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Amount</font></th>");
                    sWrite.WriteLine("</tr>");
                    int k = 1; int m = 0;

                    while (c < dgvItemData.Rows.Count)
                    {
                        decimal s = Convert.ToDecimal(dgvItemData.Rows[c].Cells["gst"].Value.ToString());
                        decimal sg = s / 2;
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + k + "</font></th>");
                        sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["itemid"].Value.ToString() + "</font></th>");
                        sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["description"].Value.ToString() + "</font></th>");
                        sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["barcode"].Value.ToString() + "</font></th>");
                        sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["Packing"].Value.ToString() + "</font></th>");
                        sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["note"].Value.ToString() + "</font></th>");//
                        DataTable dtb_batch = this.cntrl.purchase_batch_data(dgvItemData.Rows[c].Cells["id"].Value.ToString(), txtPurchInvNumber.Text);

                        //if (dtb_batch.Rows.Count > 0)
                        //{
                            if (m == 2)
                            {
                                m = 0;
                            }
                            //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000'  ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dtb_batch.Rows[m]["BatchNumber"].ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='Right' style='border:1px solid #000'  ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["col_qty"].Value.ToString() + "</font></th>");

                            sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["free"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["Unit_Cost"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["Cdiscount"].Value.ToString() + "</font></th>");
                            //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + sg + "</font></th>");
                            //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + sg + "</font></th>");
                            sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["gst"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["Amount"].Value.ToString() + "</font></th>");
                            //for (int l = 1; l < dtb_batch.Rows.Count; l++)
                            //{
                            //    sWrite.WriteLine("<tr>");
                            //    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
                            //    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
                            //    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
                            //    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
                            //    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
                            //    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");

                            //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000'  ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dtb_batch.Rows[l]["BatchNumber"].ToString() + "</font></th>");
                            //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000'  ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dtb_batch.Rows[0]["Qty"].ToString() + "</font></th>");

                            //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
                            //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
                            //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
                            //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
                            //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
                            //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
                            //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");


                            //    sWrite.WriteLine("</tr>");
                            //}


                        //}
                        //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000'  ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["col_qty"].Value.ToString() + "</font></th>");
                        //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["free"].Value.ToString() + "</font></th>");
                        //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["Unit_Cost"].Value.ToString() + "</font></th>");
                        //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["igst"].Value.ToString() + "</font></th>");
                        //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + sg + "</font></th>");
                        //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + sg + "</font></th>");
                        //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["gst"].Value.ToString() + "</font></th>");
                        //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["Amount"].Value.ToString() + "</font></th>");
                        sWrite.WriteLine("</tr>");
                        k = k + 1;
                        if (dtb_batch.Rows.Count > 1)
                        {
                            m = m + 1;
                        }
                        int row = dtb_batch.Rows.Count;
                        c++;
                    }
                    sWrite.WriteLine("<tr >");
                    sWrite.WriteLine("<td colspan=10 align='right'><right><FONT COLOR=black FACE='Segoe UI' SIZE=2>   &nbsp;Total Amount  &nbsp;:</td><td align='right'><b>INR " + txt_TotalAmount.Text + "</b></td>");
                    sWrite.WriteLine("</tr >");
                    if (txtDic.Text != "0.00")
                    {
                        sWrite.WriteLine("<tr >");
                        sWrite.WriteLine("<td colspan=10 align='right'><right><FONT COLOR=black FACE='Segoe UI' SIZE=2>   &nbsp;Total Discount Percent &nbsp;:</td><td align='right'><b>" + txtDic.Text + "</b></td>");
                        sWrite.WriteLine("</tr >");
                    }
                    if (txt_Discount.Text != "0.00")
                    {
                        sWrite.WriteLine("<tr >");
                        sWrite.WriteLine("<td colspan=10 align='right'><right><FONT COLOR=black FACE='Segoe UI' SIZE=2>   &nbsp;Discount Amount  &nbsp;:</td><td align='right'><b>INR " + txt_Discount.Text + "</b></td>");
                        sWrite.WriteLine("</tr >");
                    }
                    sWrite.WriteLine("<tr >");
                    sWrite.WriteLine("<td colspan=10 align='right'><right><FONT COLOR=black FACE='Segoe UI' SIZE=2>   &nbsp;Grand Total  &nbsp;:</td><td align='right'><b>INR " + txtGrandTotal.Text + "</b></td>");
                    sWrite.WriteLine("</tr>");
                }
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("</div>");
                sWrite.WriteLine("<script>window.print();</script>");
                sWrite.WriteLine("</body>");
                sWrite.WriteLine("</html>");
                sWrite.Close();
                System.Diagnostics.Process.Start(Apppath + "\\Purchase.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtDic_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumwithsinglepoint(sender, e);
        }
        public void onlynumwithsinglepoint(object sender, KeyPressEventArgs e)
        {

        }
        private void txtDic_KeyUp(object sender, KeyEventArgs e)
        {
            decimal dis;
            decimal totalAmount;
            decimal Discount;
            if (txtDic.Text != "" || txtDic.Text == ".")
            {
                dis = Convert.ToDecimal(txtDic.Text);
            }
            else
            {
                dis = 0;
            }
            if (disoncost == "S")
            {
                if (cmb_disc.Text == "INR")
                {
                    totalAmount = Convert.ToDecimal(txtTotalCost.Text);
                    Discount = Convert.ToDecimal(totalAmount - dis);//(dis * totalAmount) / 100
                    txt_Discount.Text = Discount.ToString("##.00");
                    txtGrandTotal.Focus();
                }
                else
                {
                    totalAmount = Convert.ToDecimal(txtTotalCost.Text);
                    Discount = Convert.ToDecimal(totalAmount - dis);
                    txt_Discount.Text = Discount.ToString("##.00");
                    txtGrandTotal.Focus();
                }

            }
            else
            {
                if (cmb_disc.Text == "INR")
                {
                    totalAmount = Convert.ToDecimal(txt_TotalAmount.Text);
                    Discount = Convert.ToDecimal(totalAmount - dis);
                    txt_Discount.Text = Discount.ToString("##.00");
                    txtGrandTotal.Text = Discount.ToString("##.00");
                }
                else
                {
                    totalAmount = Convert.ToDecimal(txt_TotalAmount.Text);
                    Discount = Convert.ToDecimal((dis * totalAmount) / 100);
                    txt_Discount.Text = Discount.ToString("##.00");
                    txtGrandTotal.Text = Discount.ToString("##.00");
                }

            }
        }

        private void txtDic_Leave(object sender, EventArgs e)
        {
            if (txtDic.Text == "")
            {
                txtDic.Text = "0.00";
            }
        }

        private void txtDic_Click(object sender, EventArgs e)
        {
            if (txtDic.Text == "0.00")
            {
                txtDic.Text = "";
            }
        }
        string disoncost = "q";
        private void txt_Discount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (disoncost == "S")
                {
                    decimal totalamount = 0;
                    decimal dic = 0;
                    decimal grandtotal = 0;
                    decimal Alltotal = 0;
                    if (txt_Discount.Text == "" || txt_Discount.Text == ".")
                    {
                        dic = 0;
                    }
                    else
                    {
                        dic = Convert.ToDecimal(txt_Discount.Text);
                    }
                    totalamount = Convert.ToDecimal(txtTotalCost.Text);
                    if (txt_Discount.Text != "0.00")
                    {
                        grandtotal = totalamount - dic;
                        txtTotalCost.Text = grandtotal.ToString("##.00");
                        //txt_TotalAmount.Text= grandtotal.ToString("##.00");
                        Alltotal = Convert.ToDecimal(grandtotal.ToString("##.00")) + Convert.ToDecimal(txtIgstResult.Text) + Convert.ToDecimal(txtCgst.Text) + Convert.ToDecimal(txtSgst.Text);
                        txtGrandTotal.Text =  Alltotal.ToString("##.00");
                    }
                    else if (txt_Discount.Text == "0.00")
                    {
                        txtGrandTotal.Text = Convert.ToDecimal(txt_TotalAmount.Text).ToString("0.00");
                    }
                }
                else
                {
                    decimal totalamount = 0;
                    decimal dic = 0;
                    decimal grandtotal = 0;
                    if (txt_Discount.Text == "" || txt_Discount.Text == ".")
                    {
                        dic = 0;
                    }
                    else
                    {
                        dic = Convert.ToDecimal(txt_Discount.Text);
                    }
                    totalamount = Convert.ToDecimal(txt_TotalAmount.Text);
                    if (txt_Discount.Text != "0.00")
                    {
                        grandtotal = totalamount - dic;
                        txtGrandTotal.Text =  grandtotal.ToString("##.00");
                    }
                    else if (txt_Discount.Text == "0.00")
                    {
                        txtGrandTotal.Text = Convert.ToDecimal(txt_TotalAmount.Text).ToString("0.00");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_Discount_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumwithsinglepoint(sender, e);
        }

        private void txt_TotalAmount_TextChanged(object sender, EventArgs e)
        {
            decimal total = Convert.ToDecimal(txt_TotalAmount.Text);
            decimal dic = Convert.ToDecimal(txtDic.Text);
            txt_Discount.Text = (total * dic / 100).ToString("##.00");
            decimal discount = Convert.ToDecimal(txt_Discount.Text);
            txtGrandTotal.Text = (total - discount).ToString();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string message1 = "Do you want Print?";
            string caption1 = "Verification";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;
            DialogResult result1;
            result1 = MessageBox.Show(message1, caption1, buttons1);
            if (result1 == System.Windows.Forms.DialogResult.Yes)
            {
                // print();
                print_thermal();
            }
        }
        public static string type = "";
        private bool flag;
        private decimal total_rate;
        private void frmPurchase_Load(object sender, EventArgs e)
        {
            try
            {
                txtSupplierName.Focus();
                load_flag = true;
                dgvItemData.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvItemData.Columns[9].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

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
                if (purchOrder_flag == true)
                {
                    if (Pur_order_no1 > 0)
                    {
                        flagSup = true;
                        decimal total = 0;
                        decimal total1 = 0;
                        DataTable dt = this.cntrl.load_purchase_order_details(Pur_order_no1);
                        if(dt.Rows.Count>0)
                        {
                            txtSupplierName.Text = dt.Rows[0]["Supplier_Name"].ToString();
                            txt_SupplierId.Text = dt.Rows[0]["Suppleir_id"].ToString();
                            flagSup = false;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                dgvItemData.Rows.Add();
                                dgvItemData.Rows[i].Cells["id"].Value = dt.Rows[i]["id"].ToString();
                                dgvItemData.Rows[i].Cells["itemid"].Value = dt.Rows[i]["Item_code"].ToString();
                                dgvItemData.Rows[i].Cells["description"].Value = dt.Rows[i]["Description"].ToString();
                                dgvItemData.Rows[i].Cells["barcode"].Value = dt.Rows[i]["Barcode"].ToString();
                                dgvItemData.Rows[i].Cells["col_qty"].Value = dt.Rows[i]["Qty"].ToString();
                                dgvItemData.Rows[i].Cells["gst"].Value = dt.Rows[i]["gstvat"].ToString();
                                dgvItemData.Rows[i].Cells["igst"].Value = "0";
                                dgvItemData.Rows[i].Cells["free"].Value = "0";
                                dgvItemData.Rows[i].Cells["note"].Value = "";
                                // dgvItemData.Rows[i].Cells["Cdiscount"].Value = "0";//bahja
                                dgvItemData.Rows[i].Cells["Unit_Cost"].Value = dt.Rows[i]["UnitCost"].ToString();
                                dgvItemData.Rows[i].Cells["Amount"].Value = dt.Rows[i]["Amount"].ToString();
                                dgvItemData.Rows[i].Cells["amt"].Value = dt.Rows[i]["Amount"].ToString();

                                total = Convert.ToDecimal(dt.Rows[i]["Amount"].ToString());
                                total1 = total1 + total;
                            }
                            txtTotal_item.Text = dgvItemData.Rows.Count.ToString();
                        }
                        txtTotalCost.Text = total1.ToString();
                        txt_TotalAmount.Text = total1.ToString();
                        txtGrandTotal.Text = total1.ToString();
                    }
                }
                if (Pur_List_flag == true)
                {
                    flagSup = true;
                   
                    Btn_itemCode.Enabled = false;
                    txtDescription.Enabled = false;
                    
                    txtbarcode.Enabled = false;
                    
                    txtPurchInvNumber.Enabled = false;
                   
                    if(credit_flag==true)
                    {
                        dgvItemData.Enabled = true;
                        btn_Save.Enabled = true; Btn_Add.Enabled = true;
                        txt_SupplierId.Enabled = true;
                        txtDic.Enabled = true;
                        btn_Save.Enabled = true;
                        Btn_Add.Enabled = true;
                        btnPrint.Visible = true;
                        BtnCancel.Enabled = true;
                        cmbUnit.Enabled = true;
                        txt_qty.Enabled = true;
                        txt_free.Enabled = true;
                        txtUnitCost.Enabled = true;
                        txtSupplierName.Enabled = true;
                        txtPacking.Enabled = true;
                        dtpPurchDate.Enabled = true;
                        Btn_Add.Text = "Update";
                        btn_Save.Text = "UPDATE";
                    }
                    else
                    {
                        //dgvItemData.Enabled = false;
                        Btn_Add.Text = "Save";
                        btn_Save.Text = "SAVE";
                        btn_Save.Enabled = false; Btn_Add.Enabled = false; txt_SupplierId.Enabled = false;
                        txtDic.Enabled = false;
                        btn_Save.Enabled = false;
                        Btn_Add.Enabled = false;
                        btnPrint.Visible = false;
                        BtnCancel.Enabled = false;
                        cmbUnit.Enabled = false;
                        txt_qty.Enabled = false;
                        txt_free.Enabled = false;
                        txtUnitCost.Enabled = false;
                        txtSupplierName.Enabled = false;
                        txtPacking.Enabled = false;
                        dtpPurchDate.Enabled = false;
                    }
                   
                    
                    btnClear.Enabled = true;
                    decimal gstcal = 0, cgstcal = 0, cgstcalc = 0, cgstcalc1 = 0, cgstcalc2 = 0, unitcostcal=0, qtycal=0;
                    decimal gstcal1 = 0;
                    decimal igstcal = 0;
                    decimal igstcal1 = 0;
                    if (data_from_Pur_Master1.Rows.Count > 0)
                    {//Product_type
                        txtPurchInvNumber.Text = data_from_Pur_Master1.Rows[0]["PurchNumber"].ToString();
                        dtpPurchDate.Text = data_from_Pur_Master1.Rows[0]["PurchDate"].ToString();
                        txt_SupplierId.Text = data_from_Pur_Master1.Rows[0]["Sup_Code"].ToString();
                        txtinvoiceno.Text = data_from_Pur_Master1.Rows[0]["InvNumber"].ToString();
                        string supplier = this.cntrl.get_suppliercode(txt_SupplierId.Text);
                        txtSupplierName.Text = supplier;
                        txtTotalCost.Text = data_from_Pur_Master1.Rows[0]["TotalCost"].ToString();
                        txt_TotalAmount.Text = data_from_Pur_Master1.Rows[0]["TotalAmount"].ToString();
                        txtDic.Text = data_from_Pur_Master1.Rows[0]["DiscPercent"].ToString();
                        txt_Discount.Text = data_from_Pur_Master1.Rows[0]["DiscAmount"].ToString();
                        string pur_type = data_from_Pur_Master1.Rows[0]["PurchType"].ToString();
                        if (data_from_Pur_Master1.Rows[0]["Product_type"].ToString() == "Consumable")
                        {
                            chk_consume.Checked = true;
                        }
                        else
                            chk_consume.Checked = false;
                        if (data_from_Pur_Master1.Rows[0]["Product_type"].ToString() == "Cash")
                        {
                            rad_CashSale.Checked = true;
                          

                        }
                        else
                        {
                            rad_CreditSale.Checked = true;
                        }

                    }
                    flagSup = false;
                    if (data_from_purchase1.Rows.Count > 0)
                    {
                        int totalitem = 0;
                        for (int i = 0; i < data_from_purchase1.Rows.Count; i++)
                        {
                            decimal amtUntchange_ = 0;
                            dgvItemData.Rows.Add();
                            amtUntchange_ = Convert.ToDecimal(data_from_purchase1.Rows[i]["Rate"].ToString()) * Convert.ToDecimal(data_from_purchase1.Rows[i]["Qty"].ToString());
                            dgvItemData.Rows[i].Cells["id"].Value = data_from_purchase1.Rows[i]["id"].ToString();
                            dgvItemData.Rows[i].Cells["itemid"].Value = data_from_purchase1.Rows[i]["Item_Code"].ToString();
                            dgvItemData.Rows[i].Cells["description"].Value = data_from_purchase1.Rows[i]["Desccription"].ToString();
                            dgvItemData.Rows[i].Cells["barcode"].Value = data_from_purchase1.Rows[i]["Barcode"].ToString();
                            dgvItemData.Rows[i].Cells["Packing"].Value = data_from_purchase1.Rows[i]["Packing"].ToString();
                            dgvItemData.Rows[i].Cells["note"].Value = data_from_purchase1.Rows[i]["Unit"].ToString();
                            dgvItemData.Rows[i].Cells["col_qty"].Value = data_from_purchase1.Rows[i]["Qty"].ToString();
                            dgvItemData.Rows[i].Cells["free"].Value = data_from_purchase1.Rows[i]["FreeQty"].ToString();
                            dgvItemData.Rows[i].Cells["Unit_Cost"].Value = data_from_purchase1.Rows[i]["Rate"].ToString();
                            dgvItemData.Rows[i].Cells["Amount"].Value = data_from_purchase1.Rows[i]["Amount"].ToString();
                            dgvItemData.Rows[i].Cells["amt"].Value = amtUntchange.ToString();
                            dgvItemData.Rows[i].Cells["gst"].Value = data_from_purchase1.Rows[i]["GST"].ToString();
                            dgvItemData.Rows[i].Cells["igst"].Value = data_from_purchase1.Rows[i]["IGST"].ToString();
                            if(data_from_purchase1.Rows[i]["Discount"].ToString()=="")
                              dgvItemData.Rows[i].Cells["Cdiscount"].Value = "0";//bhj
                            else
                                dgvItemData.Rows[i].Cells["Cdiscount"].Value = data_from_purchase1.Rows[i]["Discount"].ToString();//bhj

                            totalitem = data_from_purchase1.Rows.Count;
                            //gstcal = Convert.ToDecimal(data_from_purchase1.Rows[i]["GST"].ToString());
                            //gstcal1 = gstcal1 + gstcal;
                            igstcal = Convert.ToDecimal(data_from_purchase1.Rows[i]["IGST"].ToString());
                            igstcal1 = igstcal1 + igstcal;

                            unitcostcal = Convert.ToDecimal(data_from_purchase1.Rows[i]["Rate"].ToString());
                            qtycal = Convert.ToDecimal(data_from_purchase1.Rows[i]["Qty"].ToString());
                            cgstcal = Convert.ToDecimal(data_from_purchase1.Rows[i]["GST"].ToString());
                            cgstcalc = (((unitcostcal * qtycal) * cgstcal) / 100);
                            cgstcalc1 = cgstcalc1 + cgstcalc;
                            cgstcalc2 = cgstcalc1 / 2;
                        }
                        DataTable dt_batch_purchase = this.cntrl.get_batchpurchase(Convert.ToInt32(data_from_Pur_Master1.Rows[0]["PurchNumber"].ToString()));
                        if (dt_batch_purchase.Rows.Count > 0)
                        {
                            dgvGridData.Rows.Clear();
                            for (int i = 0; i < dt_batch_purchase.Rows.Count; i++)
                            {
                                DataTable dtb_batch = this.cntrl.get_batch(Convert.ToInt32(data_from_Pur_Master1.Rows[0]["PurchNumber"].ToString()), dt_batch_purchase.Rows[i]["Item_Code"].ToString());//, dt_batch_purchase.Rows[i]["UNIT2"].ToString()
                                
                                if (dtb_batch.Rows.Count > 0)
                                {
                                    DataTable dt_sale_rate = this.cntrl.get_batch_sales_rate(dtb_batch.Rows[0]["BatchNumber"].ToString(), dt_batch_purchase.Rows[i]["Item_Code"].ToString());
                                    //DataTable dt_unit = this.cntrl.get_purchase_unit(Convert.ToInt32(data_from_Pur_Master1.Rows[0]["PurchNumber"].ToString()), dt_batch_purchase.Rows[i]["Item_Code"].ToString(), dt_batch_purchase.Rows[i]["Qty"].ToString());
                                    dgvGridData.Rows.Add(dt_batch_purchase.Rows[i]["Item_Code"].ToString(), dtb_batch.Rows[0]["BatchNumber"].ToString(), dt_batch_purchase.Rows[i]["Unit"].ToString(), dt_batch_purchase.Rows[i]["Qty"].ToString(), dtb_batch.Rows[0]["batch_rate"].ToString(), dt_sale_rate.Rows[0]["batch_sales_rate"].ToString(), Convert.ToDateTime(dtb_batch.Rows[0]["PrdDate"].ToString()).ToString("MM-dd-yyy"), dtb_batch.Rows[0]["ExpDate"].ToString(), "", dtb_batch.Rows[0]["BatchEntry"].ToString());
                                }
                                
                            }
                        }

                        txtTotal_item.Text = totalitem.ToString();
                        txtCgst.Text= cgstcalc2.ToString("0.00");
                        txtSgst.Text = cgstcalc2.ToString("0.00");// gstcal1.ToString();
                        txtIgstResult.Text = igstcal1.ToString();
                    }
                }
                else
                {
                    this.dgvItemData.ColumnHeadersHeight = 25;
                    DataTable dtb = this.cntrl.incrementDocnumber();
                    DocNumber_increment(dtb);
                    dtpPurchDate.Format = DateTimePickerFormat.Short;
                    dtpPurchDate.Value = DateTime.Today;

                }
                load_flag = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        public void Clear()
        {
            txt_Itemcode.Clear();
            txtDescription.Clear();
            txtPacking.Clear();
            txtbarcode.Clear();
            txt_qty.Text = "0";
            txt_free.Text = "0";
            txtUnitCost.Text = "0.00";
            txtAmount.Text = "0.00";
            txtGst.Text = "0";
            txtIgst.Text = "0";
            cmbUnit.Text = "";
            textdisc.Text = "0.00";//
            dgvItemData.Rows.Clear();
          
            dgvGridData.Rows.Clear();
            txtTotalCost.Text = "0.00";
            txtIgstResult.Text = "0.00";
            txtCgst.Text = "0.00";
            txtSgst.Text = "0.00";
            txtTotal_item.Clear();
            txt_TotalAmount.Text = "0.00";
            txt_Discount.Text = "0.00";
            txtGrandTotal.Text = "0.00";
            txtSupplierName.Focus();
            txtSupplierName.Text = "";
            txtinvoiceno.Text = "";
            txt_SupplierId.Text = ""; cmbUnit.Enabled = true;
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {//,,,,Unit1,Unit2,UnitMF,
                string name = txtDescription.Text;
                if (name != "")
                {
                    DataTable dtb = this.cntrl.Get_itemdetails_itemname(name);
                    if (dtb.Rows.Count > 0)
                    {
                        Item_id = dtb.Rows[0]["id"].ToString();
                        txt_Itemcode.Text = dtb.Rows[0]["Item_Code"].ToString();
                        txtDescription.Text = dtb.Rows[0]["item_name"].ToString();

                        txtPacking.Text = dtb.Rows[0]["Packing"].ToString();
                        //txt_qty.Text = "1";
                        txtUnitCost.Text = dtb.Rows[0]["Purch_Rate"].ToString();
                        txtbarcode.Text = dtb.Rows[0]["Barcode"].ToString();
                        unitload(dtb);
                        cmbUnit.Focus();
                    }
                }
                else
                {
                    btn_Save.Select();
                }
                e.SuppressKeyPress = true;
            }
        }

        private void txtbarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string item = txtbarcode.Text; string unit1 = "", unit2 = "";
                if (item != "")
                {
                    DataTable dtbt = this.cntrl.get_itemdetails_Barcode(item);
                    if (dtbt.Rows.Count > 0)
                    {
                        //FormName = "Sales";
                        Item_id = dtbt.Rows[0]["id"].ToString();
                        DataTable dtb = this.cntrl.Get_itemdetails(Item_id);
                        if (dtb.Rows.Count > 0)
                        {
                            txt_Itemcode.Text = dtb.Rows[0]["Item_Code"].ToString();
                            //if (checkRep() == 1)
                            //{
                            //    MessageBox.Show("Item already existed", "Duplication Encountered", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //}
                            //else
                            {
                                txtDescription.Text = dtb.Rows[0]["item_name"].ToString();
                                txtPacking.Text = dtb.Rows[0]["Packing"].ToString();
                                //txt_qty.Text = "1";
                                txtUnitCost.Text = dtb.Rows[0]["Purch_Rate"].ToString();
                                txtbarcode.Text = dtbt.Rows[0]["Barcode"].ToString();
                                if (dtb.Rows[0]["GstVat"].ToString() != "")
                                    txtGst.Text = dtb.Rows[0]["GstVat"].ToString();
                                else
                                    txtGst.Text = "0";
                                unit1 = dtb.Rows[0]["Unit1"].ToString();
                                cmbUnit.Items.Clear();
                                if (unit1 != "")
                                {
                                    cmbUnit.Items.Add(unit1);
                                    cmbUnit.Text = unit1;
                                }
                                if (dtb.Rows[0]["Unit2"].ToString() != "null" && dtb.Rows[0]["Unit2"].ToString() != "")
                                {
                                    unit2 = dtb.Rows[0]["Unit2"].ToString();
                                    cmbUnit.Items.Add(unit2);
                                    cmbUnit.Text = unit1;
                                }
                                if (cmbUnit.Items.Count > 0)
                                {
                                    cmbUnit.SelectedIndex = 0;
                                }
                            }
                        }
                        cmbUnit.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Item doesn't exist..!", "No Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    txtDescription.Focus();
                }
                e.SuppressKeyPress = true;
            }
        }

        private void txt_qty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Btn_Add.Select();
                e.SuppressKeyPress = true;
            }
        }

        private void txtSupplierName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                DataTable dtb = this.cntrl.Load_Suplier();
                Load_Suplier(dtb);
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtinvoiceno.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtinvoiceno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtbarcode.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            if (flagedit == false)
            {
                if (txtDescription.Text != "" && txtbarcode.Text == "")
                {
                    DataTable dtb = this.cntrl.Search_itemdetails_itemname(txtDescription.Text);
                    if (dtb.Rows.Count > 0)
                    {
                        lst_Description.DisplayMember = "item_name";
                        lst_Description.ValueMember = "item_code";
                        lst_Description.DataSource = dtb;
                        lst_Description.Show();
                    }
                }
                else
                {
                    lst_Description.Visible = false;
                }
            }
        }

        private void txtDescription_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lst_Description.Visible != false)
                {
                    lst_Description.Focus();
                }
            }
        }

        private void lst_Description_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //flagSup = true;
                    txtDescription.Text = lst_Description.Text;
                    txt_Itemcode.Text = lst_Description.SelectedValue.ToString();
                    lst_Description.Hide();
                    string name = txtDescription.Text;
                    if (name != "")
                    {
                        DataTable dtb = this.cntrl.Get_itemdetails_itemname(name);
                        if (dtb.Rows.Count > 0)
                        {
                            Item_id = dtb.Rows[0]["id"].ToString();
                            txt_Itemcode.Text = dtb.Rows[0]["Item_Code"].ToString();
                            txtDescription.Text = dtb.Rows[0]["item_name"].ToString();

                            txtPacking.Text = dtb.Rows[0]["Packing"].ToString();
                            //txt_qty.Text = "1";
                            txtUnitCost.Text = dtb.Rows[0]["Purch_Rate"].ToString();
                            txtbarcode.Text = dtb.Rows[0]["Barcode"].ToString();
                            unitload(dtb);
                            cmbUnit.Focus();
                        }
                    }
                    cmbUnit.Focus();
                    e.SuppressKeyPress = true;
                }
                else if (e.KeyCode == Keys.Up)
                {
                    lst_Description.Focus();
                    int indicee = lst_Description.SelectedIndex;
                    indicee++;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    lst_Description.Focus();
                    int indicee = lst_Description.SelectedIndex;
                    indicee++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lst_Description_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                //flagSup = true;
                txtDescription.Text = lst_Description.Text;
                txt_Itemcode.Text = lst_Description.SelectedValue.ToString();
                lst_Description.Hide();
                string name = txtDescription.Text;
                if (name != "")
                {
                    DataTable dtb = this.cntrl.Get_itemdetails_itemname(name);
                    if (dtb.Rows.Count > 0)
                    {
                        Item_id = dtb.Rows[0]["id"].ToString();
                        txt_Itemcode.Text = dtb.Rows[0]["Item_Code"].ToString();
                        txtDescription.Text = dtb.Rows[0]["item_name"].ToString();

                        txtPacking.Text = dtb.Rows[0]["Packing"].ToString();
                        //txt_qty.Text = "1";
                        txtUnitCost.Text = dtb.Rows[0]["Purch_Rate"].ToString();
                        txtbarcode.Text = dtb.Rows[0]["Barcode"].ToString();
                        txtGst.Text = dtb.Rows[0]["GstVat"].ToString();
                        unitload(dtb);
                        cmbUnit.Focus();
                    }
                }
                cmbUnit.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvItemData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtUnitCost_TextChanged(object sender, EventArgs e)
        {
            calculaton();

        }

        private void txtUnitCost_Leave(object sender, EventArgs e)
        {
            //DataTable dtb = this.cntrl.check_batch(Item_id);
            //if (dtb.Rows.Count > 0)
            //{
            //    string rate = Convert.ToDecimal(txtUnitCost.Text).ToString("##.00");
            //    if (dtb.Rows[0]["Purch_Rate"].ToString() == rate)
            //    {

            //    }
            //    else
            //    {
            //        DialogResult res = MessageBox.Show("If you change purchase rate existing Batch's purchase rate will get updated", "Warning",
            //  MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rad_CashSale_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_CashSale.Checked == true)
            {
                rad_CreditSale.Checked = false;
                //panl_mode_payment.Visible = false;
            }
        }

        private void rad_CreditSale_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_CreditSale.Checked == true)
            {
                rad_CashSale.Checked = false;
                //panl_mode_payment.Visible = true;
            }
        }

        private void cmbUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_qty.Focus();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        //bahja
        private void textdisc_click(object sender, EventArgs e)
        {
            //if (textdisc.Text == "0.00")
            //{
            //    textdisc.Text = "";
            ////}
        }

        //private void textdisc(object sender, EventArgs e)
        //{

        //}
        //bahja
        private void textdisc_leave(object sender, EventArgs e)
        {
            if (textdisc.Text == "")
            {
                textdisc.Text = "0";
            }
            else
            {
                //string a = textdisc.Text;
                //string b = a.TrimStart('0');
                //textdisc.Text = b;
            }
        }
        
        private void textdisc_TextChanged(object sender, EventArgs e)
        {
           }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
           
        }
        //bahja....call calc()
        private void textdisc_Keyup(object sender, KeyEventArgs e)
        {
            if (textdisc.Text == "")
            {
                textdisc.Text = "0";
            }
            calculaton();

        }

        //thermal.......bhj
        private void print_thermal()
        {
            string message = "Do you want Header on Print?";
            string caption = "Verification";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons);
            int c = 0;
            string strclinicname = "";
            string strStreet = "";
            string stremail = "";
            string strwebsite = "";
            string strphone = "";
            string path = "";
            string logo_name = "";
            DataTable print_settng = this.prnt_ctrl.Get_inventory_id();
            try
            {
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    
                    System.Data.DataTable dtp = this.cntrl.get_companydetails();
                    if (dtp.Rows.Count > 0)
                    {

                        if (dtp.Rows.Count > 0)
                        {
                            string clinicn = "";
                            //clinicn = dtp.Rows[0]["name"].ToString();
                            if (print_settng.Rows.Count > 0)
                            {
                                clinicn = print_settng.Rows[0]["header"].ToString();
                                strclinicname = clinicn.Replace("¤", "'");
                                strStreet = print_settng.Rows[0]["left_text"].ToString();
                                strphone = print_settng.Rows[0]["right_text"].ToString();
                            }
                            stremail = dtp.Rows[0]["email"].ToString();
                            strwebsite = dtp.Rows[0]["website"].ToString();
                            path = dtp.Rows[0]["path"].ToString();
                            logo_name = path;
                        }
                    }
                }
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                StreamWriter sWrite = new StreamWriter(Apppath + "\\Purchase_print.html");
                sWrite.WriteLine("<html>");
                sWrite.WriteLine("<head>");
                sWrite.WriteLine("<style>");
                sWrite.WriteLine("table { border-collapse: collapse;}");
                sWrite.WriteLine("p.big {line-height: 400%;}");
                sWrite.WriteLine("</style>");
                sWrite.WriteLine("</head>");
                sWrite.WriteLine("<body >");
                sWrite.WriteLine("<div>");
                sWrite.WriteLine("<table align=center width=100%>");//900
                sWrite.WriteLine("<col >");
                sWrite.WriteLine("<br>");
                string Appath = System.IO.Directory.GetCurrentDirectory();
                string check = print_settng.Rows[0]["set_as_default_header"].ToString();
                string header_image = print_settng.Rows[0]["header_path"].ToString();
                if (check == "Yes")
                {
                    if (File.Exists(Appath + "\\" + logo_name))
                    {
                        sWrite.WriteLine("<table align='center' style='width:270px;border: 1px ;border-collapse: collapse;'>");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td width='30px' height='50px' align='left' rowspan='3'><img src='" + Appath + "\\" + logo_name + "'style='width:70px;height:70px;' ></td>  ");
                        sWrite.WriteLine("<td width='870px' align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=4><b>&nbsp;" + strclinicname + "</font> <br><FONT  COLOR=black  face='Segoe UI' SIZE=2>&nbsp;" + strStreet + "<br>&nbsp;" + strphone + " </b></td></tr>");
                        sWrite.WriteLine("</table>");
                    }
                    else
                    {
                        sWrite.WriteLine("<table align='center' style='width:270px;border: 1px ;border-collapse: collapse;'>");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td  align='left' height='20px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + strclinicname + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td  align='left' height='20px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;" + strStreet + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td align='left' height='20px' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + strphone + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                        sWrite.WriteLine("</table>");
                    }
                }
                else
                {
                    string paths = this.db.server();
                    if (File.Exists(paths + "\\" + header_image))
                    {
                        sWrite.WriteLine("<table align='center'   style='width:270px;border: 1px ;border-collapse: collapse;' >");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td  align='left'><img src='" + paths + "\\" + header_image + "' width='270' height='80';'></td>  ");
                        sWrite.WriteLine("</tr>");
                        sWrite.WriteLine("</table>");

                    }
                }
                sWrite.WriteLine("<table align='center' style='width:270px;border: 1px ;border-collapse: collapse;'>");
                //sWrite.WriteLine("<tr><td align='left'  ><hr/></td></tr>");
                //sWrite.WriteLine("</table>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3> PURCHASE INVOICE </font></center></b></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<table align='center' style='width:270px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td  colspan=11  align='center'><left><FONT COLOR=black FACE='Segoe UI'  SIZE=2> Printed Date : " + dtpPurchDate.Value.ToString("d/MM/yyyy") + " </font></left></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan=11  align='center'><left><FONT COLOR=black FACE='Segoe UI'  SIZE=2>   Purchase No :" + txtPurchInvNumber.Text + " </font></left></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan=11  align='center'><left><FONT COLOR=black FACE='Segoe UI'  SIZE=2>   Invoice No :" + txtinvoiceno.Text + " </font></left></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan=11 align='center'><left><FONT COLOR=black FACE='Segoe UI'  SIZE=2>   Supplier Name : " + txtSupplierName.Text + " </font></left></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<table align='center' style='width:270px;border: 1px ;border-collapse: collapse;'>");
                if (dgvItemData.Rows.Count > 0)
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("    <td align='center' width='250px' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=2>SNO.</font></th>");
                    // sWrite.WriteLine("    <td align='left' width='230px' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3>Item Code</font></th>");
                    // sWrite.WriteLine("    <td align='left' width='230px' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Item Name</font></th>");
                    //sWrite.WriteLine("    <td align='left' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Barcode</font></th>");
                    //sWrite.WriteLine("    <td align='left' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Packing</font></th>");
                    //sWrite.WriteLine("    <td align='left' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Unit</font></th>");
                    //sWrite.WriteLine("    <td align='left' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Batch</font></th>");
                    sWrite.WriteLine("    <td align='Right' width='250px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=2>Quantity</font></th>");
                    //sWrite.WriteLine("    <td align='Right' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Free</font></th>");
                    sWrite.WriteLine("    <td align='Right' width='250px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2>Unit Cost</font></th>");
                    sWrite.WriteLine("    <td align='Right' width='250px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=2>Discount</font></th>");
                    //sWrite.WriteLine("    <td align='Right' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>SGST(%)</font></th>");
                    //sWrite.WriteLine("    <td align='Right' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>CGST(%)</font></th>");
                    // sWrite.WriteLine("    <td align='Right' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>GST(%)</font></th>");
                    sWrite.WriteLine("    <td align='Right' width='250px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=2>Amount</font></th>");
                    sWrite.WriteLine("</tr>");
                    int k = 1; int m = 0;

                    while (c < dgvItemData.Rows.Count)
                    {
                        decimal s = Convert.ToDecimal(dgvItemData.Rows[c].Cells["gst"].Value.ToString());
                        decimal sg = s / 2;
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + k + "</font></th>");
                        // sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["itemid"].Value.ToString() + "</font></th>");
                        sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["description"].Value.ToString() + "</font></th>");
                        // sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["barcode"].Value.ToString() + "</font></th>");
                        //sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["Packing"].Value.ToString() + "</font></th>");
                        //sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["note"].Value.ToString() + "</font></th>");//
                        DataTable dtb_batch = this.cntrl.purchase_batch_data(dgvItemData.Rows[c].Cells["id"].Value.ToString(), txtPurchInvNumber.Text);

                        //if (dtb_batch.Rows.Count > 0)
                        //{
                        if (m == 2)
                        {
                            m = 0;
                        }
                        //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000'  ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dtb_batch.Rows[m]["BatchNumber"].ToString() + "</font></th>");
                        // sWrite.WriteLine("    <td align='Right' style='border:1px solid #000'  ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["col_qty"].Value.ToString() + "</font></th>");

                        // sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["free"].Value.ToString() + "</font></th>");
                        sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["Unit_Cost"].Value.ToString() + "</font></th>");
                        sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["Cdiscount"].Value.ToString() + "</font></th>");
                        //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + sg + "</font></th>");
                        //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + sg + "</font></th>");
                        // sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["gst"].Value.ToString() + "</font></th>");
                        sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["Amount"].Value.ToString() + "</font></th>");
                        //for (int l = 1; l < dtb_batch.Rows.Count; l++)
                        //{
                        //    sWrite.WriteLine("<tr>");
                        //    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
                        //    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
                        //    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
                        //    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
                        //    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
                        //    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");

                        //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000'  ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dtb_batch.Rows[l]["BatchNumber"].ToString() + "</font></th>");
                        //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000'  ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dtb_batch.Rows[0]["Qty"].ToString() + "</font></th>");

                        //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
                        //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
                        //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
                        //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
                        //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
                        //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
                        //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");


                        //    sWrite.WriteLine("</tr>");
                        //}


                        //}
                        //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000'  ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["col_qty"].Value.ToString() + "</font></th>");
                        //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["free"].Value.ToString() + "</font></th>");
                        //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["Unit_Cost"].Value.ToString() + "</font></th>");
                        //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["igst"].Value.ToString() + "</font></th>");
                        //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + sg + "</font></th>");
                        //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + sg + "</font></th>");
                        //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["gst"].Value.ToString() + "</font></th>");
                        //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["Amount"].Value.ToString() + "</font></th>");
                        sWrite.WriteLine("</tr>");
                        k = k + 1;
                        if (dtb_batch.Rows.Count > 1)
                        {
                            m = m + 1;
                        }
                        int row = dtb_batch.Rows.Count;
                        c++;
                    }
                    sWrite.WriteLine("<tr >");
                    sWrite.WriteLine("<td colspan=10 align='right'><left><FONT COLOR=black FACE='Segoe UI' SIZE=2>   &nbsp;Total Amount : INR " + txt_TotalAmount.Text + "</td>");
                    sWrite.WriteLine("</tr >");
                    if (txtDic.Text != "0.00")
                    {
                        sWrite.WriteLine("<tr >");
                        sWrite.WriteLine("<td colspan=10 align='right'><right><FONT COLOR=black FACE='Segoe UI' SIZE=2>Total Discount Percent : " + txtDic.Text + "</td>");
                        sWrite.WriteLine("</tr >");
                    }
                    if (txt_Discount.Text != "0.00")
                    {
                        sWrite.WriteLine("<tr >");
                        sWrite.WriteLine("<td colspan=10 align='right'><right><FONT COLOR=black FACE='Segoe UI' SIZE=2>Discount Amount : INR " + txt_Discount.Text + " </td>");
                        sWrite.WriteLine("</tr >");
                    }
                    sWrite.WriteLine("<tr >");
                    sWrite.WriteLine("<td colspan=10 align='right'><left><FONT COLOR=black FACE='Segoe UI' SIZE=2> Grand Total : INR " + txtGrandTotal.Text + " </td>");
                    sWrite.WriteLine("</tr>");
                    //sWrite.WriteLine("<td colspan=11 align='center'><left><FONT COLOR=black FACE='Segoe UI'  SIZE=2>   Supplier Name : " + txtSupplierName.Text + " </font></left></td>");
                    //sWrite.WriteLine("</tr>");
                }
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("</div>");
                sWrite.WriteLine("<script>window.print();</script>");
                sWrite.WriteLine("</body>");
                sWrite.WriteLine("</html>");
                sWrite.Close();
                System.Diagnostics.Process.Start(Apppath + "\\Purchase.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void textdisc_Click_1(object sender, EventArgs e)
        {
            if (textdisc.Text == "0")
            {
                textdisc.Text = "";
            }
        }

        private void textdisc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            string a = textdisc.Text;
            string b = a.TrimStart('0');
            textdisc.Text = b;
        }

        private void txt_free_KeyUp(object sender, KeyEventArgs e)
        {
            if (txt_free.Text == "")
            {
                txt_free.Text = "0";
            }
            //calculaton();

        }

        private void cmb_disc_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal dis;
            decimal totalAmount;
            decimal Discount;
            if (txtDic.Text != "" || txtDic.Text == ".")
            {
                dis = Convert.ToDecimal(txtDic.Text);
            }
            else
            {
                dis = 0;
            }
            if (disoncost == "S")
            {
                if (cmb_disc.Text == "INR")
                {
                    totalAmount = Convert.ToDecimal(txtTotalCost.Text);
                    Discount = Convert.ToDecimal(totalAmount - dis);//(dis * totalAmount) / 100
                    txt_Discount.Text = Discount.ToString("##.00");
                    txtGrandTotal.Focus();
                }
                else
                {
                    totalAmount = Convert.ToDecimal(txtTotalCost.Text);
                    Discount = Convert.ToDecimal(totalAmount - dis);
                    txt_Discount.Text = Discount.ToString("##.00");
                    txtGrandTotal.Focus();
                }

            }
            else
            {
                if (cmb_disc.Text == "INR")
                {
                    totalAmount = Convert.ToDecimal(txt_TotalAmount.Text);
                    Discount = Convert.ToDecimal(totalAmount - dis);
                    txt_Discount.Text = Discount.ToString("##.00");
                }
                else
                {
                    totalAmount = Convert.ToDecimal(txt_TotalAmount.Text);
                    Discount = Convert.ToDecimal((dis * totalAmount) / 100);
                    txt_Discount.Text = Discount.ToString("##.00");
                }

            }
        }
    }
}
