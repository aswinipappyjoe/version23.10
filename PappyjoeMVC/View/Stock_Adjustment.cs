using PappyjoeMVC.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using PappyjoeMVC.Model;

namespace PappyjoeMVC.View
{
    public partial class Stock_Adjustment : Form
    {

        Inventory_model inv = new Inventory_model();
        stock_updation_controller cntrl = new stock_updation_controller(); string unit2Is;
        public string rowindex = "";
        public decimal quantity = 0, qty = 0;
        public string Action = "";
        string type = "";

        public Stock_Adjustment()
        {
            InitializeComponent();
        }

        private void Stock_Adjustment_Load(object sender, EventArgs e)
        {
            DataTable dt_consume = this.cntrl.Get_consume_tick();
            if (dt_consume.Rows.Count > 0)
            {
                if (dt_consume.Rows[0]["consumables"].ToString() == "Yes")
                {
                    type = "Consumable";
                }
                else
                    type = "Pharmacy";
            }
            else
                type = "Pharmacy";
            DataTable dt_items = this.cntrl.get_itemdata_Stock_updation_limit(type);
            if(dt_items.Rows.Count>0)
            {
                fill_Grid(dt_items);
            }
            DataTable dt_it = this.cntrl.get_itemdata_Stock_updation_all_dataLoad(type);
            if (dt_it.Rows.Count > 25)
            {
                linkLabel1.Visible = true;
            }
            else
            {
                linkLabel1.Visible = false;
            }
            dgvstockOut.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            dgvstockOut.EnableHeadersVisualStyles = false;
            this.dgvstockOut.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvstockOut.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvstockOut.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 9, FontStyle.Regular);
        }
        public void fill_Grid(DataTable dtb)
        {
            dgvstockOut.Rows.Clear();
            if (dtb.Rows.Count > 0)
            {
                int k = 1; int i = 0;
                Lab_Msg.Visible = false;
                foreach (DataRow drow in dtb.Rows)
                {
                    //DataTable dt = this.cntrl.get_pur_gst(drow["id"].ToString());
                    DataTable dt_unit = this.cntrl.dt_units(drow["id"].ToString());
                     int n = dgvstockOut.Rows.Add();
                    dgvstockOut.Rows[i].Cells["id"].Value = drow["id"].ToString();
                    dgvstockOut.Rows[i].Cells["batchid"].Value = drow["Entry_No"].ToString();
                    dgvstockOut.Rows[i].Cells["slno"].Value = k;
                    dgvstockOut.Rows[i].Cells["itemcode"].Value = drow["item_code"].ToString();
                    dgvstockOut.Rows[i].Cells["description"].Value = drow["item_name"].ToString();
                    if (drow["BatchNumber"].ToString() != "")
                    {
                        dgvstockOut.Rows[i].Cells["Unit_Cost"].Value = drow["batch_rate"].ToString();// dt_unit.Rows[0]["batch_rate"].ToString();
                        dgvstockOut.Rows[i].Cells["batch"].Value = drow["BatchNumber"].ToString();
                        dgvstockOut.Rows[i].Cells["batch"].ReadOnly = true;
                    }
                    else
                    {
                        dgvstockOut.Rows[i].Cells["Unit_Cost"].Value = dt_unit.Rows[0]["Purch_Rate"].ToString();
                        dgvstockOut.Rows[i].Cells["batch"].Value = "";
                        dgvstockOut.Rows[i].Cells["batch"].ReadOnly = false;
                    }
                    dgvstockOut.Rows[i].Cells["unit"].Value = "";//GstVat
                                                                 //if (dt.Rows.Count > 0)
                                                                 //{
                                                                 //    if (dt.Rows[0]["GST"].ToString() != "")
                                                                 //    {
                                                                 //        dgvstockOut.Rows[i].Cells["gst"].Value = Convert.ToDecimal(dt.Rows[0]["GST"].ToString()).ToString();
                                                                 //    }
                                                                 //    else
                                                                 //        dgvstockOut.Rows[i].Cells["gst"].Value = 0;
                                                                 //}
                    if (dt_unit.Rows.Count > 0)
                    {
                        if (dt_unit.Rows[0]["GstVat"].ToString() != "")
                        {
                            dgvstockOut.Rows[i].Cells["gst"].Value = Convert.ToDecimal(dt_unit.Rows[0]["GstVat"].ToString()).ToString();
                        }
                        else
                            dgvstockOut.Rows[i].Cells["gst"].Value = 0;
                    }
                    if (dt_unit.Rows.Count > 0) //adding combobox column
                    {
                       
                        foreach (DataRow dr in dt_unit.Rows)
                        {
                            if (dt_unit.Rows[0]["Unit2"].ToString() != "null" && dt_unit.Rows[0]["Unit2"].ToString() != "")
                            {
                                DataGridViewComboBoxCell CellColumn1;
                                string unit1 = dt_unit.Rows[0]["Unit1"].ToString();
                                string unit2 = dt_unit.Rows[0]["Unit2"].ToString();
                                var list1 = new List<string>() { unit1, unit2 };
                                CellColumn1 = (DataGridViewComboBoxCell)this.dgvstockOut.Rows[i].Cells["unit"];
                                CellColumn1.DataSource = list1;
                            }
                            else
                            {
                                DataGridViewComboBoxCell CellColumn1;
                                string unit1 = dt_unit.Rows[0]["Unit1"].ToString();
                                var list1 = new List<string>() { unit1 };
                                CellColumn1 = (DataGridViewComboBoxCell)this.dgvstockOut.Rows[i].Cells["unit"];
                                CellColumn1.DataSource = list1;
                            }
                        }
                    }
                    if (drow["Qty"].ToString() != "")
                    {
                        dgvstockOut.Rows[i].Cells["currentStock"].Value = Convert.ToDecimal(drow["Qty"].ToString()).ToString("0.00");
                    }
                    else
                        dgvstockOut.Rows[i].Cells["currentStock"].Value = 0;
                    dgvstockOut.Rows[i].Cells["excess"].Value = 0;
                    dgvstockOut.Rows[i].Cells["Shortage"].Value = 0;
                    dgvstockOut.Rows[i].Cells["newstock"].Value = 0;
                    dgvstockOut.Rows[i].Cells["total"].Value = "0.00";
                    k++;i++;
                }
            }
            else
            {
                Lab_Msg.Visible = true;
                Lab_Msg.Location = new Point(194, 282);
                dgvstockOut.RowCount = 0;
            }
        }


        public void fill_Grid_showmore(DataTable dtb)
        {
            if (dtb.Rows.Count > 0)
            {
                int row = dgvstockOut.Rows.Count;
                int k =Convert.ToInt32( dgvstockOut.Rows[row-1].Cells["slno"].Value.ToString())+1; //int i = 0;
                Lab_Msg.Visible = false;
                foreach (DataRow drow in dtb.Rows)
                {
                    DataTable dt = this.cntrl.get_pur_gst(drow["id"].ToString());
                    DataTable dt_unit = this.cntrl.dt_units(drow["id"].ToString());
                    int n = dgvstockOut.Rows.Add();
                    dgvstockOut.Rows[row].Cells["id"].Value = drow["id"].ToString();
                    dgvstockOut.Rows[row].Cells["batchid"].Value = drow["Entry_No"].ToString();
                    dgvstockOut.Rows[row].Cells["slno"].Value = k;
                    dgvstockOut.Rows[row].Cells["itemcode"].Value = drow["item_code"].ToString();
                    dgvstockOut.Rows[row].Cells["description"].Value = drow["item_name"].ToString();
                    if (drow["BatchNumber"].ToString() != "")
                    {
                        dgvstockOut.Rows[row].Cells["Unit_Cost"].Value = drow["batch_rate"].ToString(); 
                        dgvstockOut.Rows[row].Cells["batch"].Value = drow["BatchNumber"].ToString();
                        dgvstockOut.Rows[row].Cells["batch"].ReadOnly = true;
                    }
                    else
                    {
                        dgvstockOut.Rows[row].Cells["Unit_Cost"].Value = dt_unit.Rows[0]["Purch_Rate"].ToString();
                        dgvstockOut.Rows[row].Cells["batch"].Value = "";
                        dgvstockOut.Rows[row].Cells["batch"].ReadOnly = false;
                    }
                    dgvstockOut.Rows[row].Cells["unit"].Value = "";
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["GST"].ToString() != "")
                        {
                            dgvstockOut.Rows[row].Cells["gst"].Value = Convert.ToDecimal(dt.Rows[0]["GST"].ToString()).ToString();
                        }
                        else
                            dgvstockOut.Rows[row].Cells["gst"].Value = 0;
                    }
                    if (dt_unit.Rows.Count > 0) //adding combobox column
                    {
                      
                        foreach (DataRow dr in dt_unit.Rows)
                        {
                            if (dt_unit.Rows[0]["Unit2"].ToString() != "null" && dt_unit.Rows[0]["Unit2"].ToString() != "")
                            {
                                DataGridViewComboBoxCell CellColumn1;
                                string unit1 = dt_unit.Rows[0]["Unit1"].ToString();
                                string unit2 = dt_unit.Rows[0]["Unit2"].ToString();
                                var list1 = new List<string>() { unit1, unit2 };
                                CellColumn1 = (DataGridViewComboBoxCell)this.dgvstockOut.Rows[row].Cells["unit"];
                                CellColumn1.DataSource = list1;
                            }
                            else
                            {
                                DataGridViewComboBoxCell CellColumn1;
                                string unit1 = dt_unit.Rows[0]["Unit1"].ToString();
                                var list1 = new List<string>() { unit1 };
                                CellColumn1 = (DataGridViewComboBoxCell)this.dgvstockOut.Rows[row].Cells["unit"];
                                CellColumn1.DataSource = list1;
                            }
                        }
                    }
                    if (drow["Qty"].ToString() != "")
                    {
                        dgvstockOut.Rows[row].Cells["currentStock"].Value = Convert.ToDecimal(drow["Qty"].ToString()).ToString("0.00");
                    }
                    else
                        dgvstockOut.Rows[row].Cells["currentStock"].Value = 0;
                    dgvstockOut.Rows[row].Cells["excess"].Value = 0;
                    dgvstockOut.Rows[row].Cells["Shortage"].Value = 0;
                    dgvstockOut.Rows[row].Cells["newstock"].Value = 0;
                    dgvstockOut.Rows[row].Cells["total"].Value = "0.00";
                    row++;k++;
                }
            }
            else
            {
                linkLabel1.Visible = false;
            }
        }
       

        private void dgvstockOut_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if(e.RowIndex>=0)
            {
                if (dgvstockOut.CurrentCell.OwningColumn.Name == "excess")
                {
                    if (dgvstockOut.CurrentRow.Cells["excess"].Value != null && dgvstockOut.CurrentRow.Cells["excess"].Value.ToString() != "")
                    {
                        if (Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["excess"].Value) == 0)
                        {
                            dgvstockOut.CurrentRow.Cells["excess"].Value = "";
                        }
                    }
                }
                else if (dgvstockOut.CurrentCell.OwningColumn.Name == "Shortage")
                {
                    if (dgvstockOut.CurrentRow.Cells["Shortage"].Value != null && dgvstockOut.CurrentRow.Cells["Shortage"].Value.ToString() != "")
                    {
                        if (Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["Shortage"].Value) == 0)
                        {
                            dgvstockOut.CurrentRow.Cells["Shortage"].Value = "";
                        }
                    }
                }
            }
           
        }

        private void btn_open_stk_excesss_Click(object sender, EventArgs e)
        {
            Action = "Adjust to Opening Stock Excess or Shortage";
            Save();
        }
      
        public void Save()
        {
            string cs = PappyjoeMVC.Model.Connection.MyGlobals.trans_connectionstring;
            using (MySqlConnection con = new MySqlConnection(cs))
            {
                con.Open();
                MySqlTransaction trans = con.BeginTransaction();
                try
                {
                    if (dgv_BatchSale.Rows.Count > 0)
                    {
                        string purnumber = "";
                        DataTable dtb = this.cntrl.trans_incrementDocnumber(con, trans); //incrementDocnumber();
                        if (String.IsNullOrWhiteSpace(dtb.Rows[0][0].ToString()))
                        {
                            purnumber = "1";
                        }
                        else
                        {
                            int Count = Convert.ToInt32(dtb.Rows[0][0]) + 1;
                            purnumber = Count.ToString();
                        }
                        for (int i = 0; i < dgv_BatchSale.Rows.Count; i++)
                        {
                            DataTable dt_batch = this.cntrl.trans_get_batchdetails(dgv_BatchSale.Rows[i].Cells["itemid"].Value.ToString(),con,trans);
                            if (dt_batch.Rows.Count > 0)
                            {
                                DataTable dt_stock = this.cntrl.get_stock_of_items(dgv_BatchSale.Rows[i].Cells["itemid"].Value.ToString(), con, trans);
                                this.cntrl.update_stockOut(Convert.ToDecimal(dgv_BatchSale.Rows[i].Cells["newstk"].Value.ToString()).ToString(), dgv_BatchSale.Rows[i].Cells["batid"].Value.ToString(), con, trans);//update stock
                                this.cntrl.save_stockupdate(Convert.ToDateTime(dtpentryDate.Value).ToString("yyyy-MM-dd"), Action, dgv_BatchSale.Rows[i].Cells["itemid"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["itemname"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colBatchnumber"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["b_unit"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["givenqty"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["unitcost"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["batid"].Value.ToString(), dt_stock.Rows[0][0].ToString(),con,trans);
                                dgvstockOut.CurrentRow.DefaultCellStyle.BackColor = Color.LightGray;
                            }
                            else
                            {
                                string supName = ""; int suplier_id = 1; string IsExpDate = "";
                                string expdate = "";
                                DataTable max_supNo = this.cntrl.get_Supliermaxid(con,trans);
                                if (max_supNo.Rows.Count > 0)
                                {
                                    suplier_id = Convert.ToInt32(max_supNo.Rows[0][0].ToString()) + 1;
                                }
                                else
                                    suplier_id = 1;
                                DataTable dt_suplier = this.cntrl.get_suppliername("stockIn_updation",con,trans);
                                if (dt_suplier.Rows.Count <= 0)
                                {
                                    this.cntrl.Save(suplier_id.ToString(), "stockIn_updation", "ct", "9999999999", "", "", "", "", "", "", "", "10", "StockUpdation");
                                    DataTable suppler_Name = this.cntrl.get_suppliername("stockIn_updation",con,trans);
                                    if (suppler_Name.Rows.Count > 0)
                                        supName = suppler_Name.Rows[0][0].ToString();
                                }
                                else
                                {
                                    supName = dt_suplier.Rows[0][0].ToString();
                                }
                                string maxid = this.cntrl.maxid_stockupdation(con,trans);
                                DataTable dt_purno = this.cntrl.check_purno(purnumber,con,trans);//int ii = 0;
                                if (dt_purno.Rows.Count <= 0)
                                {
                                    this.cntrl.save_purchase(purnumber, "", DateTime.Now.Date.ToString("yyyy-MM-dd"), supName, "0", dgv_BatchSale.Rows[i].Cells["totalamount"].Value.ToString(), "0", "0", "0", "StockUpdation", type,con,trans);
                                }
                                DataTable dtunit2 = this.cntrl.get_item_unitmf(dgv_BatchSale.Rows[i].Cells["itemid"].Value.ToString(),con,trans);
                                if (dtunit2.Rows.Count > 0)
                                {
                                    if (dgv_BatchSale.Rows[i].Cells["b_unit"].Value.ToString() == dtunit2.Rows[0]["Unit2"].ToString())
                                    {
                                        unit2Is = "Yes";
                                    }
                                    else
                                    {
                                        unit2Is = "No";
                                    }
                                    IsExpDate = "NO";
                                }
                                //purchit
                                this.cntrl.save_purchaseit(purnumber, DateTime.Now.Date.ToString("yyyy-MM-dd"), dgv_BatchSale.Rows[i].Cells["itemid"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["itemname"].Value.ToString(), "", "", dgv_BatchSale.Rows[i].Cells["b_unit"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["givenqty"].Value.ToString(), 0, dgv_BatchSale.Rows[i].Cells["unitcost"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["totalamount"].Value.ToString(), unit2Is, dtunit2.Rows[0]["UnitMF"].ToString(), Convert.ToDecimal(dgv_BatchSale.Rows[i].Cells["bgst"].Value.ToString()), 0,con,trans );
                                //batchnumber
                                if (dgv_BatchSale.Rows[i].Cells["colBatchnumber"].Value.ToString() == "")
                                {
                                    dgv_BatchSale.Rows[i].Cells["colBatchnumber"].Value = purnumber + "_" + dgv_BatchSale.Rows[i].Cells["coiltem_code"].Value.ToString();
                                }
                                int a = this.cntrl.save_batchNumber(dgv_BatchSale.Rows[i].Cells["itemid"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colBatchnumber"].Value.ToString(), Convert.ToInt32(dgv_BatchSale.Rows[i].Cells["newstk"].Value.ToString()), unit2Is, dtunit2.Rows[0]["UnitMF"].ToString(), purnumber, DateTime.Now.Date.ToString("yyyy-MM-dd"), expdate, "", supName, DateTime.Now.Date.ToString("yyyy-MM-dd"), IsExpDate, type,con,trans);
                                //batch purchase
                                DataTable batch_entry = this.cntrl.get_maxEntryNo(con,trans);
                                this.cntrl.save_batchpurchase(purnumber, DateTime.Now.Date.ToString("yyyy-MM-dd"), supName, dgv_BatchSale.Rows[i].Cells["itemid"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colBatchnumber"].Value.ToString(), Convert.ToInt32(dgv_BatchSale.Rows[i].Cells["givenqty"].Value.ToString()), unit2Is, dtunit2.Rows[0]["UnitMF"].ToString(), DateTime.Now.Date.ToString("yyyy-MM-dd"), expdate, IsExpDate, batch_entry.Rows[0][0].ToString(),con,trans);
                                //stock adjustment
                                DataTable dt_stock = this.cntrl.get_stock_of_items(dgv_BatchSale.Rows[i].Cells["itemid"].Value.ToString(),con,trans);
                                this.cntrl.save_stockupdate(Convert.ToDateTime(dtpentryDate.Value).ToString("yyyy-MM-dd"), Action, dgv_BatchSale.Rows[i].Cells["itemid"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["itemname"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colBatchnumber"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["b_unit"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["givenqty"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["unitcost"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["batid"].Value.ToString(), dt_stock.Rows[0][0].ToString(),con,trans);
                                dgvstockOut.CurrentRow.DefaultCellStyle.BackColor = Color.LightGray;
                            }
                        }
                        MessageBox.Show("Successfully Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgv_BatchSale.Rows.Clear();
                        Action = "";
                        trans.Commit();
                        con.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please edit items", "No data found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }

            }
        }
      
      
     
        private void btn_open_stk_short_Click(object sender, EventArgs e)
        {
            Action = "Adjust to Opening Stock Shortage";
            Save();
        }

        private void bnt_damage_Click(object sender, EventArgs e)
        {
            Action = "Adjust to Damage";
            Save();
        }

        private void btn_shortage_Click(object sender, EventArgs e)
        {
            Action = "Adjust to Shortage";
            Save();
        }

        private void btn_excess_Click(object sender, EventArgs e)
        {
            Action = "Adjust to Excess";
            Save();
        }

        private void txt_ItemCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_ItemCode.Text != "")
                {
                    DataTable dtb = this.cntrl.Stock_updation_search_wit_itemcode(txt_ItemCode.Text,type);
                    fill_Grid(dtb);
                }
                else
                {
                    DataTable dt_load = this.cntrl.get_itemdata_Stock_updation(type);
                    fill_Grid(dt_load);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_ItemCode_Click(object sender, EventArgs e)
        {
            if(txt_ItemCode.Text== "Search by Item Code,Item Name,Barcode")
            {
                txt_ItemCode.Text = "";
            }
        }
        public int rowcount = 0;
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int count = rowcount + 50;
            DataTable dt_items = this.cntrl.stock_adjustmnt_showmore(type, count);

            if (dt_items.Rows.Count > 0)
            {
                fill_Grid_showmore(dt_items);// Load_MainTable(dt_invoice_main);
                rowcount = count;
            }
            else
            {
                linkLabel1.Visible = false;
            }
        }

        private void dgvstockOut_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvstockOut.CurrentCell.ColumnIndex == 6 && e.Control is ComboBox)
            {
                ComboBox comboBox = e.Control as ComboBox;
                comboBox.SelectedIndexChanged += LastColumnComboSelectionChanged;
            }
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (dgvstockOut.CurrentCell.ColumnIndex == 6 || dgvstockOut.CurrentCell.ColumnIndex == 9 || dgvstockOut.CurrentCell.ColumnIndex == 10) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void LastColumnComboSelectionChanged(object sender, EventArgs e)
        {
            var currentcell = dgvstockOut.CurrentRow.Index;
            string value = ""; decimal unitmf = 0,batch_rate=0;
            DataTable dt_itemdetails = this.cntrl.itemdetails(dgvstockOut.CurrentRow.Cells["id"].Value.ToString());
            DataTable dt_batchrate = this.inv.batch_wise_rate(dgvstockOut.CurrentRow.Cells["batch"].Value.ToString(), dgvstockOut.CurrentRow.Cells["id"].Value.ToString());
            //batch_rate =Convert.ToDecimal( dgvstockOut.CurrentRow.Cells["Unit_Cost"].Value.ToString());
            unitmf = Convert.ToDecimal(dt_itemdetails.Rows[0]["UnitMF"].ToString());
            if (unitmf > 0)
            {
                ComboBox cb = (ComboBox)sender;
                value = cb.Text;
            }
            else
            {
                value = dgvstockOut.CurrentRow.Cells["unit"].Value.ToString();
            }
            Decimal gst = 0, gst_Amount = 0, Amount = 0, TotalAmount = 0, qty = 0;int batch_stk=0;
            decimal Stock = 0;
            if (dgvstockOut.CurrentRow.Cells["Shortage"].Value != null && dgvstockOut.CurrentRow.Cells["Shortage"].Value.ToString() != ""
                )
            {
                if (Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["Shortage"].Value) > 0)
                {
                    qty = Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["Shortage"].Value);
                }
            }
            if(dgvstockOut.CurrentRow.Cells["excess"].Value != null && dgvstockOut.CurrentRow.Cells["excess"].Value.ToString() != "")
            {
                if (Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["excess"].Value) > 0)
                {
                    qty = Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["excess"].Value);
                }
            }
            if (unitmf > 0)
            {
                DataTable dt_batchstk = this.cntrl.batch_stock(dgvstockOut.CurrentRow.Cells["id"].Value.ToString(), dgvstockOut.CurrentRow.Cells["batchid"].Value.ToString());
                if (dt_itemdetails.Rows[0]["Unit1"].ToString() == value)
                {
                  if(Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["currentStock"].Value)>0)
                    {
                        batch_stk=Convert.ToInt32(Convert.ToDecimal(dt_batchstk.Rows[0][0].ToString())/ unitmf);
                        dgvstockOut.CurrentRow.Cells["currentStock"].Value = batch_stk;
                    }
                    Stock = qty * unitmf;
                    //dgvstockOut.CurrentRow.Cells["Unit_Cost"].Value = Convert.ToDecimal(dt_itemdetails.Rows[0]["Purch_Rate"].ToString());
                    batch_rate= Convert.ToDecimal(dt_batchrate.Rows[0]["batch_rate"].ToString());
                    dgvstockOut.CurrentRow.Cells["Unit_Cost"].Value = batch_rate;// Convert.ToDecimal(dt_batchrate.Rows[0]["batch_rate"].ToString());

                }
                else
                {
                    if (Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["currentStock"].Value) > 0)
                    {
                        dgvstockOut.CurrentRow.Cells["currentStock"].Value = Convert.ToDecimal(dt_batchstk.Rows[0][0].ToString());

                    }
                    Stock = qty;

                    batch_rate = Convert.ToDecimal(dt_batchrate.Rows[0]["batch_rate"].ToString()) / unitmf;
                    dgvstockOut.CurrentRow.Cells["Unit_Cost"].Value = batch_rate;// Convert.ToDecimal(dt_batchrate.Rows[0]["batch_rate"].ToString());
                    //dgvstockOut.CurrentRow.Cells["Unit_Cost"].Value = Convert.ToDecimal(dt_itemdetails.Rows[0]["Purch_Rate2"].ToString());
                }
            }
            else
            {
                Stock = qty; 
                batch_rate = Convert.ToDecimal(dt_batchrate.Rows[0]["batch_rate"].ToString());
                dgvstockOut.CurrentRow.Cells["Unit_Cost"].Value = batch_rate;// Convert.ToDecimal(dt_batchrate.Rows[0]["batch_rate"].ToString());
                //dgvstockOut.CurrentRow.Cells["Unit_Cost"].Value = Convert.ToDecimal(dt_itemdetails.Rows[0]["Purch_Rate"].ToString());
            }
            gst = Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["gst"].Value);
            if (gst > 0)
            {
                Amount = qty * Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["Unit_Cost"].Value);
                gst_Amount = (Amount * gst) / 100;
                TotalAmount = Amount + gst_Amount;
                dgvstockOut.CurrentRow.Cells["total"].Value = TotalAmount.ToString("##.00");
            }
            else
            {
                TotalAmount = qty * Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["Unit_Cost"].Value);
                dgvstockOut.CurrentRow.Cells["total"].Value = TotalAmount.ToString("##.00");
            }
            if (Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["total"].Value.ToString()) > 0)
            {
                fill_batch_grid(dgvstockOut.CurrentRow.Index,value);
            }
        }
       
        private void dgvstockOut_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Decimal gst = 0, gst_Amount = 0, Amount = 0, TotalAmount = 0;
                decimal Stock = 0;
                if (dgvstockOut.CurrentCell.OwningColumn.Name == "excess")
                {
                    if (dgvstockOut.CurrentRow.Cells["excess"].Value != null && dgvstockOut.CurrentRow.Cells["excess"].Value.ToString() != "")
                    {
                        DataTable dt_itemdetails = this.cntrl.itemdetails(dgvstockOut.CurrentRow.Cells["id"].Value.ToString());
                        decimal unitmf = Convert.ToDecimal(dt_itemdetails.Rows[0]["UnitMF"].ToString());
                        quantity = Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["excess"].Value);
                        if (unitmf > 0)
                        {
                            if (dt_itemdetails.Rows[0]["Unit1"].ToString() == dgvstockOut.CurrentRow.Cells["unit"].Value.ToString())
                            {
                                Stock = quantity * unitmf;
                            }
                            else
                            {
                                Stock = quantity;
                            }
                        }
                        else
                        {
                            Stock = quantity;
                        }

                        DataTable dt_batchstk = this.cntrl.batch_stock(dgvstockOut.CurrentRow.Cells["id"].Value.ToString(), dgvstockOut.CurrentRow.Cells["batchid"].Value.ToString());
                        if (Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["currentStock"].Value) > 0)
                        {
                            dgvstockOut.CurrentRow.Cells["newstock"].Value = Stock + Convert.ToDecimal(dt_batchstk.Rows[0][0].ToString());
                        }
                        else
                        {
                            dgvstockOut.CurrentRow.Cells["newstock"].Value = Stock + Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["currentStock"].Value);
                        }

                        rowindex = dgvstockOut.CurrentRow.Index.ToString();
                        gst = Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["gst"].Value);
                        if (gst > 0)
                        {
                            Amount = Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["excess"].Value) * Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["Unit_Cost"].Value);
                            gst_Amount = (Amount * gst) / 100;
                            TotalAmount = Amount + gst_Amount;
                            dgvstockOut.CurrentRow.Cells["total"].Value = TotalAmount.ToString("##.00");
                        }
                        else
                        {
                            TotalAmount = Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["excess"].Value) * Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["Unit_Cost"].Value);
                            dgvstockOut.CurrentRow.Cells["total"].Value = TotalAmount.ToString("##.00");
                        }
                    }
                }
                else if (dgvstockOut.CurrentCell.OwningColumn.Name == "Shortage")
                {
                    if (dgvstockOut.CurrentRow.Cells["Shortage"].Value != null && dgvstockOut.CurrentRow.Cells["Shortage"].Value.ToString() != "")
                    {
                        if (Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["Shortage"].Value) <= Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["currentStock"].Value))
                        {
                            DataTable dt_itemdetails = this.cntrl.itemdetails(dgvstockOut.CurrentRow.Cells["id"].Value.ToString());
                            quantity = Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["Shortage"].Value);
                            decimal unitmf = Convert.ToDecimal(dt_itemdetails.Rows[0]["UnitMF"].ToString());
                            if (unitmf > 0)
                            {
                                if (dt_itemdetails.Rows[0]["Unit1"].ToString() == dgvstockOut.CurrentRow.Cells["unit"].Value.ToString())
                                {
                                    Stock = quantity * unitmf;
                                }
                                else
                                {
                                    Stock = quantity;
                                }
                            }
                            else
                            {
                                Stock = quantity;
                            }

                            DataTable dt_batchstk = this.cntrl.batch_stock(dgvstockOut.CurrentRow.Cells["id"].Value.ToString(), dgvstockOut.CurrentRow.Cells["batchid"].Value.ToString());
                            if (Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["currentStock"].Value) > 0)
                            {
                                dgvstockOut.CurrentRow.Cells["newstock"].Value = Convert.ToDecimal(dt_batchstk.Rows[0][0].ToString()) - Stock;
                            }
                            rowindex = dgvstockOut.CurrentRow.Index.ToString();
                            gst = Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["gst"].Value);
                            if (gst > 0)
                            {
                                Amount = Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["Shortage"].Value) * Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["Unit_Cost"].Value);
                                gst_Amount = (Amount * gst) / 100;
                                TotalAmount = Amount + gst_Amount;
                                dgvstockOut.CurrentRow.Cells["total"].Value = TotalAmount.ToString("##.00");
                            }
                            else
                            {
                                TotalAmount = Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["Shortage"].Value) * Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["Unit_Cost"].Value);
                                dgvstockOut.CurrentRow.Cells["total"].Value = TotalAmount.ToString("##.00");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Less stock / No stock, Can't do the operation!..", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgvstockOut.CurrentRow.Cells["Shortage"].Value = 0;
                            return;
                        }
                    }
                }
                else if (dgvstockOut.CurrentCell.OwningColumn.Name == "gst")
                {
                    DataTable dt_itemdetails = this.cntrl.itemdetails(dgvstockOut.CurrentRow.Cells["id"].Value.ToString());
                    if (Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["Shortage"].Value) > 0)
                    {
                        quantity = Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["Shortage"].Value);
                    }
                    else
                    {
                        quantity = Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["excess"].Value);
                    }

                    decimal unitmf = Convert.ToDecimal(dt_itemdetails.Rows[0]["UnitMF"].ToString());
                    if (unitmf > 0)
                    {
                        if (dt_itemdetails.Rows[0]["Unit1"].ToString() == dgvstockOut.CurrentRow.Cells["unit"].Value.ToString())
                        {
                            Stock = quantity * unitmf;
                        }
                        else
                        {
                            Stock = quantity;
                        }
                    }
                    else
                    {
                        Stock = quantity;
                    }
                    gst = Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["gst"].Value);
                    if (gst > 0)
                    {
                        Amount = quantity * Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["Unit_Cost"].Value);
                        gst_Amount = (Amount * gst) / 100;
                        TotalAmount = Amount + gst_Amount;
                        dgvstockOut.CurrentRow.Cells["total"].Value = TotalAmount.ToString("##.00");
                    }
                    else
                    {
                        TotalAmount = quantity * Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["Unit_Cost"].Value);
                        dgvstockOut.CurrentRow.Cells["total"].Value = TotalAmount.ToString("##.00");
                    }

                }
                if (Convert.ToDecimal(dgvstockOut.CurrentRow.Cells["total"].Value.ToString())>0)
                {
                    fill_batch_grid(dgvstockOut.CurrentRow.Index,"");
                }
            }
        }
        public void fill_batch_grid(int rowindex,string value)
        {
            string unit = "",batch=""; 
            if(value!="")
            {
                unit = value;
            }
            else if(dgvstockOut.Rows[rowindex].Cells["unit"].Value.ToString()!="")
            {
                unit = dgvstockOut.Rows[rowindex].Cells["unit"].Value.ToString();
            }
            if(dgv_BatchSale.Rows.Count==0)
            {
                dgv_BatchSale.Rows.Add(dgvstockOut.Rows[rowindex].Cells["id"].Value.ToString(), dgvstockOut.Rows[rowindex].Cells["batchid"].Value.ToString(), dgvstockOut.Rows[rowindex].Cells["itemcode"].Value.ToString(), dgvstockOut.Rows[rowindex].Cells["description"].Value.ToString(), dgvstockOut.Rows[rowindex].Cells["batch"].Value.ToString(), unit, dgvstockOut.Rows[rowindex].Cells["gst"].Value.ToString(), dgvstockOut.Rows[rowindex].Cells["Unit_Cost"].Value.ToString(), dgvstockOut.Rows[rowindex].Cells["currentstock"].Value.ToString(), quantity, dgvstockOut.Rows[rowindex].Cells["newstock"].Value.ToString(), dgvstockOut.Rows[rowindex].Cells["total"].Value.ToString());
            }
            else
            {
                    string id = dgvstockOut.Rows[rowindex].Cells["id"].Value.ToString();
                string batid = dgvstockOut.Rows[rowindex].Cells["batchid"].Value.ToString();
                if (itemcheck(id) == 0)
                {
                    dgv_BatchSale.Rows.Add(dgvstockOut.Rows[rowindex].Cells["id"].Value.ToString(), dgvstockOut.Rows[rowindex].Cells["batchid"].Value.ToString(), dgvstockOut.Rows[rowindex].Cells["itemcode"].Value.ToString(), dgvstockOut.Rows[rowindex].Cells["description"].Value.ToString(), dgvstockOut.Rows[rowindex].Cells["batch"].Value.ToString(), unit, dgvstockOut.Rows[rowindex].Cells["gst"].Value.ToString(), dgvstockOut.Rows[rowindex].Cells["Unit_Cost"].Value.ToString(), dgvstockOut.Rows[rowindex].Cells["currentstock"].Value.ToString(), quantity, dgvstockOut.Rows[rowindex].Cells["newstock"].Value.ToString(), dgvstockOut.Rows[rowindex].Cells["total"].Value.ToString());
                }
                else
                {
                    if (batchcheck(batid) == 0)
                    {
                        dgv_BatchSale.Rows.Add(dgvstockOut.Rows[rowindex].Cells["id"].Value.ToString(), dgvstockOut.Rows[rowindex].Cells["batchid"].Value.ToString(), dgvstockOut.Rows[rowindex].Cells["itemcode"].Value.ToString(), dgvstockOut.Rows[rowindex].Cells["description"].Value.ToString(), dgvstockOut.Rows[rowindex].Cells["batch"].Value.ToString(), unit, dgvstockOut.Rows[rowindex].Cells["gst"].Value.ToString(), dgvstockOut.Rows[rowindex].Cells["Unit_Cost"].Value.ToString(), dgvstockOut.Rows[rowindex].Cells["currentstock"].Value.ToString(), quantity, dgvstockOut.Rows[rowindex].Cells["newstock"].Value.ToString(), dgvstockOut.Rows[rowindex].Cells["total"].Value.ToString());
                    }
                    else
                    {
                        dgv_BatchSale.Rows[row_index].Cells["itemid"].Value = dgvstockOut.Rows[rowindex].Cells["id"].Value.ToString();
                        dgv_BatchSale.Rows[row_index].Cells["batid"].Value = dgvstockOut.Rows[rowindex].Cells["batchid"].Value.ToString();
                        dgv_BatchSale.Rows[row_index].Cells["coiltem_code"].Value = dgvstockOut.Rows[rowindex].Cells["itemcode"].Value.ToString();
                        dgv_BatchSale.Rows[row_index].Cells["itemname"].Value = dgvstockOut.Rows[rowindex].Cells["description"].Value.ToString();
                        dgv_BatchSale.Rows[row_index].Cells["colBatchnumber"].Value = dgvstockOut.Rows[rowindex].Cells["batch"].Value.ToString();
                        dgv_BatchSale.Rows[row_index].Cells["b_unit"].Value = unit;
                        dgv_BatchSale.Rows[row_index].Cells["bgst"].Value = dgvstockOut.Rows[rowindex].Cells["gst"].Value.ToString();
                        dgv_BatchSale.Rows[row_index].Cells["unitcost"].Value = dgvstockOut.Rows[rowindex].Cells["Unit_Cost"].Value.ToString();
                        dgv_BatchSale.Rows[row_index].Cells["current_stk"].Value = dgvstockOut.Rows[rowindex].Cells["currentstock"].Value.ToString();
                        dgv_BatchSale.Rows[row_index].Cells["givenqty"].Value = quantity;
                        dgv_BatchSale.Rows[row_index].Cells["newstk"].Value = dgvstockOut.Rows[rowindex].Cells["newstock"].Value.ToString();
                        dgv_BatchSale.Rows[row_index].Cells["totalamount"].Value = dgvstockOut.Rows[rowindex].Cells["total"].Value.ToString();
                    }
                }
            }
            row_index = 0;
        }
        public int affected = 0,bat_affected=0;public int row_index =0;
        public int itemcheck(string item)
        {
            for(int i=0;i< dgv_BatchSale.Rows.Count;i++)
            {
                if (dgv_BatchSale.Rows[i].Cells["itemid"].Value.ToString() == item)
                {
                    row_index = i;
                       affected = 1;
                    break;
                }
                else
                    affected = 0;
            }
            return affected;
        }

        private void dgvstockOut_KeyPress(object sender, KeyPressEventArgs e)
        {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
        }

        public int batchcheck(string item)
        {
            for (int i = 0; i < dgv_BatchSale.Rows.Count; i++)
            {
                if (dgv_BatchSale.Rows[i].Cells["batid"].Value.ToString() == item)
                {
                    row_index = i;
                    bat_affected = 1;
                    break;
                }
                else
                    bat_affected = 0;
            }
            return bat_affected;
        }
        private void lnk_todays_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DataTable dt_today = this.cntrl.Stock_updation_todays_adjustmnt(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            if(dt_today.Rows.Count>0)
            {
                if(dgvstockOut.Rows.Count>0)
                {
                    for(int i=0;i<dt_today.Rows.Count;i++)
                    {
                        for(int j=0;j<dgvstockOut.Rows.Count;j++)
                        {
                            if(dgvstockOut.Rows[j].Cells["id"].Value.ToString()==dt_today.Rows[i]["ItemId"].ToString())
                            {
                                dgvstockOut.Rows[j].DefaultCellStyle.BackColor = Color.LightGray;
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("NO items adjusted Today !", "No Records Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
