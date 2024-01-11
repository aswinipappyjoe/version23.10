using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using PappyjoeMVC.Controller;
using PappyjoeMVC.Model;
using System.IO;
namespace PappyjoeMVC.View
{
    public partial class Stock_Ledger : Form
    {
        Stock_ledger_controller cntrl = new Stock_ledger_controller();
        Printout_controller prnt_ctrl = new Printout_controller();

        Inventory_model inv_model = new Inventory_model();
        public static string itemid = "";
        public static string stock_type = "";
        public Stock_Ledger()
        {
            InitializeComponent();
        }

        private void Stock_Ledger_Load(object sender, EventArgs e)
        {
            dgv_Stock.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            dgv_Stock.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv_Stock.EnableHeadersVisualStyles = false;
            dgv_Stock.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 9, FontStyle.Regular);
            DataTable dt_consume_check = this.inv_model.Get_consume_tick();
            if (dt_consume_check.Rows.Count > 0)
            {
                if (dt_consume_check.Rows[0]["consumables"].ToString() == "Yes")
                {
                    stock_type = "Consumable";
                }
                else
                    stock_type = "Pharmacy";
            }
            else
                stock_type = "Pharmacy";
        }

        private void txtPatient_KeyUp(object sender, KeyEventArgs e)
       {
            DataTable dtb = this.cntrl.load_itemcode(txtitemcode.Text);
            Fill_litbox(dtb);
            if (txtitemcode.Text == "")
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
        public void Fill_litbox(DataTable dtdr)
        {
            if (dtdr.Rows.Count > 0)
            {
                lbPatient.Show();
                lbPatient.Location = new Point(txtitemcode.Location.X, 27);
                lbPatient.DisplayMember = "item_name";
                lbPatient.ValueMember = "id";
                lbPatient.DataSource = dtdr;
            }
            else
            {
                lbPatient.Hide();
            }
        }

        private void lbPatient_MouseClick(object sender, MouseEventArgs e)
        {
            if (lbPatient.SelectedItems.Count > 0)
            {
                itemid = lbPatient.SelectedValue.ToString();
                txtitemcode.Text = lbPatient.Text;
                lbPatient.Visible = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtitemcode.Text != "")
            {
                dgv_Stock.Rows.Clear();
                if (itemid != "")
                {
                    string date_from = "", date_To = "";
                    date_from = dtp_from.Value.ToString("yyyy-MM-dd");
                    date_To = dtp_to.Value.ToString("yyyy-MM-dd");
                    decimal total_stock = 0, Total_receipt_stk = 0, Total_issuse_stk = 0, Total_receipt_rate = 0, Total_issuse_rate = 0;
                    DataTable dt_item_details = this.cntrl.get_opening_stock(itemid);
                    string unit = "";
                    if (Convert.ToDecimal(dt_item_details.Rows[0]["UnitMF"].ToString()) > 0)
                    {
                        dgv_Stock.Rows.Add("", "", "", dt_item_details.Rows[0]["item_name"].ToString(), "( " + " where 1 " + dt_item_details.Rows[0]["Unit1"].ToString() + " have " + dt_item_details.Rows[0]["UnitMF"].ToString() + "" + dt_item_details.Rows[0]["Unit2"].ToString() + " )", "", "", "", "", "");
                        unit = dt_item_details.Rows[0]["Unit2"].ToString().Trim();
                    }
                    else
                    {
                        dgv_Stock.Rows.Add("", "", "", dt_item_details.Rows[0]["item_name"].ToString(), "", "", "", "", "", "");
                        unit = dt_item_details.Rows[0]["Unit1"].ToString().Trim();
                    }
                    dgv_Stock.Rows[dgv_Stock.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.BlueViolet;
                    dgv_Stock.Rows[dgv_Stock.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 8, FontStyle.Bold);

                    string date = "";
                    if (Convert.ToDecimal(dt_item_details.Rows[0]["Open_Stock"].ToString()) > 0)
                    {
                        decimal open_qty = 0;
                        if (Convert.ToInt32(dt_item_details.Rows[0]["UnitMF"].ToString()) > 0)
                        {
                            open_qty = Convert.ToDecimal(dt_item_details.Rows[0]["Open_Stock"].ToString()) * Convert.ToDecimal(dt_item_details.Rows[0]["UnitMF"].ToString());
                        }
                        else
                        {
                            open_qty = Convert.ToDecimal(dt_item_details.Rows[0]["Open_Stock"].ToString());
                        }
                        total_stock = open_qty;// Convert.ToDecimal(dt_item_details.Rows[0]["Open_Stock"].ToString());
                        //date = "Opening Stock Date : " + open_qty;
                        //total_stock = Convert.ToDecimal(dt_item_details.Rows[0]["Open_Stock"].ToString());
                        date = "Opening Stock Date : " + dt_item_details.Rows[0]["opening_stock_date"].ToString();
                    }
                    else
                        date = "";
                    dgv_Stock.Rows.Add("", "", "", "Opening Balance", date, "", "", "", "", total_stock);
                    ////// end opening balance
                    DataTable dt_purchase_from = this.cntrl.get_purchase_from_to(date_from, date_To, itemid);
                    if (dt_purchase_from.Rows.Count > 0)
                    {
                        decimal total_Pstock = 0, receipt_stk = 0, receipt_rate = 0;
                        total_Pstock = total_Pstock + total_stock;
                        for (int i = 0; i < dt_purchase_from.Rows.Count; i++)
                        {
                            decimal unitmf = 0, qty = 0, stock = 0;
                            unitmf = Convert.ToDecimal(dt_item_details.Rows[0]["UnitMF"].ToString());
                            if (Convert.ToDecimal(dt_purchase_from.Rows[i]["FreeQty"].ToString()) > 0)
                            {
                                qty = Convert.ToDecimal(dt_purchase_from.Rows[i]["Qty"].ToString()) + Convert.ToDecimal(dt_purchase_from.Rows[i]["FreeQty"].ToString());
                            }
                            else
                                qty = Convert.ToDecimal(dt_purchase_from.Rows[i]["Qty"].ToString());

                            if (unitmf > 0)
                            {
                                if (dt_item_details.Rows[0]["Unit1"].ToString().Trim() == dt_purchase_from.Rows[i]["Unit"].ToString().Trim())
                                {
                                    stock = qty * unitmf;
                                }
                                else
                                {
                                    stock = qty;
                                }
                            }
                            else
                            {
                                stock = qty;
                            }
                            total_Pstock = total_Pstock + stock;
                            receipt_stk = receipt_stk + stock;
                            receipt_rate = receipt_rate + Convert.ToDecimal(dt_purchase_from.Rows[i]["Amount"].ToString());
                            dgv_Stock.Rows.Add(itemid, Convert.ToDateTime(dt_purchase_from.Rows[i]["PurchDate"].ToString()).ToString("dd-MM-yyyy"), dt_purchase_from.Rows[i]["PurchNumber"].ToString(), "Cash", "Purchase", dt_purchase_from.Rows[i]["Qty"].ToString() + " " + dt_purchase_from.Rows[i]["Unit"].ToString(), Convert.ToDecimal(dt_purchase_from.Rows[i]["Amount"].ToString()).ToString("0.00"), "", "", total_Pstock.ToString("0.00"));

                        }
                        Total_receipt_stk = Total_receipt_stk + receipt_stk;
                        Total_receipt_rate = Total_receipt_rate + receipt_rate;
                    }
                    DataTable dt_purchase_return_from = this.cntrl.get_purchase_return_from_to(date_from, date_To, itemid);
                    if (dt_purchase_return_from.Rows.Count > 0)
                    {
                        decimal issuse_stk = 0, total_Pstock = 0, last_stk = 0, issuse_rate = 0;
                        total_Pstock = total_Pstock + total_stock;
                        last_stk = Convert.ToDecimal(dgv_Stock.Rows[dgv_Stock.Rows.Count - 1].Cells["balance"].Value.ToString());
                        for (int i = 0; i < dt_purchase_return_from.Rows.Count; i++)
                        {
                            decimal unitmf = 0, qty = 0, stock = 0;
                            unitmf = Convert.ToDecimal(dt_item_details.Rows[0]["UnitMF"].ToString());
                            if (Convert.ToDecimal(dt_purchase_return_from.Rows[i]["FreeQty"].ToString()) > 0)
                            {
                                qty = Convert.ToDecimal(dt_purchase_return_from.Rows[i]["Qty"].ToString()) + Convert.ToDecimal(dt_purchase_return_from.Rows[i]["FreeQty"].ToString());
                            }
                            else
                                qty = Convert.ToDecimal(dt_purchase_return_from.Rows[i]["Qty"].ToString());

                            if (unitmf > 0)
                            {
                                if (dt_item_details.Rows[0]["Unit1"].ToString().Trim() == dt_purchase_return_from.Rows[i]["Unit"].ToString().Trim())
                                {
                                    stock = qty * unitmf;
                                }
                                else
                                {
                                    stock = qty;
                                }
                            }
                            else
                            {
                                stock = qty;
                            }
                            last_stk = last_stk - stock;
                            issuse_stk = issuse_stk + stock;
                            issuse_rate = issuse_rate + Convert.ToDecimal(dt_purchase_return_from.Rows[i]["Rate"].ToString());
                            dgv_Stock.Rows.Add(itemid, Convert.ToDateTime(dt_purchase_return_from.Rows[i]["ReturnDate"].ToString()).ToString("dd-MM-yyyy"), dt_purchase_return_from.Rows[i]["RetNumber"].ToString() + " , Purchase No :" + dt_purchase_return_from.Rows[i]["PurchNumber"].ToString(), "Cash", "Purchase Return", "", "", dt_purchase_return_from.Rows[i]["Qty"].ToString() + " " + dt_purchase_return_from.Rows[i]["Unit"].ToString(), Convert.ToDecimal(dt_purchase_return_from.Rows[i]["Rate"].ToString()).ToString("0.00"), last_stk.ToString("0.00"));
                        }
                        Total_issuse_stk = Total_issuse_stk + issuse_stk;
                        Total_issuse_rate = Total_issuse_rate + issuse_rate;
                    }

                    DataTable dt_sales_from = this.cntrl.get_sales_from_to(date_from, date_To, itemid);
                    if (dt_sales_from.Rows.Count > 0)
                    {
                        decimal last_stk = 0, issuse_stk = 0, issuse_rate = 0;
                        last_stk = Convert.ToDecimal(dgv_Stock.Rows[dgv_Stock.Rows.Count - 1].Cells["balance"].Value.ToString());
                        for (int i = 0; i < dt_sales_from.Rows.Count; i++)
                        {
                            decimal unitmf = 0, qty = 0, stock = 0;
                            unitmf = Convert.ToDecimal(dt_item_details.Rows[0]["UnitMF"].ToString());
                            if (Convert.ToDecimal(dt_sales_from.Rows[i]["FreeQty"].ToString()) > 0)
                            {
                                qty = Convert.ToDecimal(dt_sales_from.Rows[i]["Qty"].ToString()) + Convert.ToDecimal(dt_sales_from.Rows[i]["FreeQty"].ToString());
                            }
                            else
                                qty = Convert.ToDecimal(dt_sales_from.Rows[i]["Qty"].ToString());

                            if (unitmf > 0)
                            {
                                if (dt_item_details.Rows[0]["Unit1"].ToString().Trim() == dt_sales_from.Rows[i]["Unit"].ToString().Trim())
                                {
                                    stock = qty * unitmf;
                                }
                                else
                                {
                                    stock = qty;
                                }
                            }
                            else
                            {
                                stock = qty;
                            }
                            issuse_stk = issuse_stk + stock;
                            last_stk = last_stk - stock;
                            issuse_rate = issuse_rate + Convert.ToDecimal(dt_sales_from.Rows[i]["TotalAmount"].ToString());
                            dgv_Stock.Rows.Add(itemid, Convert.ToDateTime(dt_sales_from.Rows[i]["InvDate"].ToString()).ToString("dd-MM-yyyy"), dt_sales_from.Rows[i]["InvNumber"].ToString(), "Cash", "Sales", "", "", dt_sales_from.Rows[i]["Qty"].ToString() + " " + dt_sales_from.Rows[i]["Unit"].ToString(), Convert.ToDecimal(dt_sales_from.Rows[i]["TotalAmount"].ToString()).ToString("0.00"), last_stk.ToString("0.00"));
                        }
                        Total_issuse_stk = Total_issuse_stk + issuse_stk;
                        Total_issuse_rate = Total_issuse_rate + issuse_rate;
                    }
                    DataTable dt_sales_return_from = this.cntrl.get_sales_return_from_to(date_from, date_To, itemid);
                    if (dt_sales_return_from.Rows.Count > 0)
                    {
                        decimal last_stk = 0, receipt_stk = 0, receipt_rate = 0;
                        last_stk = Convert.ToDecimal(dgv_Stock.Rows[dgv_Stock.Rows.Count - 1].Cells["balance"].Value.ToString());
                        for (int i = 0; i < dt_sales_return_from.Rows.Count; i++)
                        {
                            decimal unitmf = 0, qty = 0, stock = 0;
                            unitmf = Convert.ToDecimal(dt_item_details.Rows[0]["UnitMF"].ToString());
                            if (Convert.ToDecimal(dt_sales_return_from.Rows[i]["FreeQty"].ToString()) > 0)
                            {
                                qty = Convert.ToDecimal(dt_sales_return_from.Rows[i]["Qty"].ToString()) + Convert.ToDecimal(dt_sales_return_from.Rows[i]["FreeQty"].ToString());
                            }
                            else
                                qty = Convert.ToDecimal(dt_sales_return_from.Rows[i]["Qty"].ToString());

                            if (unitmf > 0)
                            {
                                if (dt_item_details.Rows[0]["Unit1"].ToString().Trim() == dt_sales_return_from.Rows[i]["Unit"].ToString().Trim())
                                {
                                    stock = qty * unitmf;
                                }
                                else
                                {
                                    stock = qty;
                                }
                            }
                            else
                            {
                                stock = qty;
                            }
                            last_stk = last_stk + stock;
                            receipt_stk = receipt_stk + stock;
                            receipt_rate = receipt_rate + Convert.ToDecimal(dt_sales_return_from.Rows[i]["Rate"].ToString());
                            dgv_Stock.Rows.Add(itemid, Convert.ToDateTime(dt_sales_return_from.Rows[i]["ReturnDate"].ToString()).ToString("dd-MM-yyyy"), dt_sales_return_from.Rows[i]["RetNumber"].ToString() + ",  InvoiceNo :" + dt_sales_return_from.Rows[i]["InvNumber"].ToString(), "Cash", "Sales Return", dt_sales_return_from.Rows[i]["Qty"].ToString() + " " + dt_sales_return_from.Rows[i]["Unit"].ToString(), Convert.ToDecimal(dt_sales_return_from.Rows[i]["Rate"].ToString()).ToString("0.00"), "", "", last_stk.ToString("0.00"));
                        }
                        Total_receipt_stk = Total_receipt_stk + receipt_stk;
                        Total_receipt_rate = Total_receipt_rate + receipt_rate;
                    }
                    DataTable dt_stock_updation_from_to = this.cntrl.get_stock_adjuctment_from_to(date_from, date_To, itemid);
                    if (dt_stock_updation_from_to.Rows.Count > 0)
                    {
                        decimal last_stk = 0, receipt_stk = 0, receipt_rate = 0, issuse_stk = 0, issuse_rate = 0;
                        last_stk = Convert.ToDecimal(dgv_Stock.Rows[dgv_Stock.Rows.Count - 1].Cells["balance"].Value.ToString());
                        for (int i = 0; i < dt_stock_updation_from_to.Rows.Count; i++)
                        {
                            decimal unitmf = 0, qty = 0, stock = 0;
                            unitmf = Convert.ToDecimal(dt_item_details.Rows[0]["UnitMF"].ToString());
                            qty = Convert.ToDecimal(dt_stock_updation_from_to.Rows[i]["upate_qty"].ToString());

                            if (unitmf > 0)
                            {
                                if (dt_item_details.Rows[0]["Unit1"].ToString().Trim() == dt_stock_updation_from_to.Rows[i]["unit"].ToString().Trim())
                                {
                                    stock = qty * unitmf;
                                }
                                else
                                {
                                    stock = qty;
                                }
                            }
                            else
                            {
                                stock = qty;
                            }
                            if (dt_stock_updation_from_to.Rows[i]["Action"].ToString().Trim() == "Adjust to Excess")
                            {
                                last_stk = last_stk + stock;
                                receipt_stk = receipt_stk + stock;
                            }
                            else if (dt_stock_updation_from_to.Rows[i]["Action"].ToString().Trim() == "Adjust to Shortage")
                            {
                                last_stk = last_stk - stock;
                                issuse_stk = issuse_stk + stock;
                            }
                            else if (dt_stock_updation_from_to.Rows[i]["Action"].ToString().Trim() == "Adjust to Damage")
                            {
                                last_stk = last_stk - stock;
                                issuse_stk = issuse_stk + stock;
                            }
                            if (dt_stock_updation_from_to.Rows[i]["Action"].ToString().Trim() == "Adjust to Excess")
                            {
                                dgv_Stock.Rows.Add(itemid, Convert.ToDateTime(dt_stock_updation_from_to.Rows[i]["updation_date"].ToString()).ToString("dd-MM-yyyy"), dt_stock_updation_from_to.Rows[i]["id"].ToString(), "", "Stock Adjustment", dt_stock_updation_from_to.Rows[i]["upate_qty"].ToString() + " " + dt_stock_updation_from_to.Rows[i]["unit"].ToString(), "0.00", "", "", last_stk.ToString("0.00"));

                            }
                            else
                            {
                                dgv_Stock.Rows.Add(itemid, Convert.ToDateTime(dt_stock_updation_from_to.Rows[i]["updation_date"].ToString()).ToString("dd-MM-yyyy"), dt_stock_updation_from_to.Rows[i]["id"].ToString(), "", "Stock Adjustment", "", "", dt_stock_updation_from_to.Rows[i]["upate_qty"].ToString() + " " + dt_stock_updation_from_to.Rows[i]["unit"].ToString(), "0.00", last_stk.ToString("0.00"));

                            }
                        }
                        Total_issuse_stk = Total_issuse_stk + issuse_stk;
                        Total_receipt_stk = Total_receipt_stk + receipt_stk;
                    }
                    DataTable dt_stock_out = this.cntrl.get_stock_transfer_from_to(date_from, date_To, itemid);
                    if (dt_stock_out.Rows.Count > 0)
                    {
                        decimal last_stk = 0, issuse_stk = 0;
                        last_stk = Convert.ToDecimal(dgv_Stock.Rows[dgv_Stock.Rows.Count - 1].Cells["balance"].Value.ToString());
                        foreach (DataRow dr in dt_stock_out.Rows)
                        {
                            decimal unitmf = 0, qty = 0, stock = 0;
                            unitmf = Convert.ToDecimal(dt_item_details.Rows[0]["UnitMF"].ToString());
                            qty = Convert.ToDecimal(dr["Given_Qty"].ToString());

                            if (unitmf > 0)
                            {
                                if (dt_item_details.Rows[0]["Unit1"].ToString().Trim() == dr["unit"].ToString().Trim())
                                {
                                    stock = qty * unitmf;
                                }
                                else
                                {
                                    stock = qty;
                                }
                            }
                            else
                            {
                                stock = qty;
                            }
                            issuse_stk = issuse_stk + stock;
                            last_stk = last_stk - stock;
                            dgv_Stock.Rows.Add(itemid, Convert.ToDateTime(dr["stock_date"].ToString()).ToString("dd-MM-yyyy"), dr["RefNo"].ToString(), "", "Stock Out", "", "", dr["Given_Qty"].ToString() + " " + dr["unit"].ToString(), "0.00", last_stk.ToString("0.00"));
                        }

                        Total_issuse_stk = Total_issuse_stk + issuse_stk;
                    }
                    DataTable dt_stock_in = this.cntrl.get_stock_in_from_to(date_from, date_To, itemid);
                    if (dt_stock_in.Rows.Count > 0)
                    {
                        decimal last_stk = 0, receipt_stk = 0;
                        last_stk = Convert.ToDecimal(dgv_Stock.Rows[dgv_Stock.Rows.Count - 1].Cells["balance"].Value.ToString());
                        for (int m = 0; m < dt_purchase_from.Rows.Count; m++)
                        {
                            decimal unitmf = 0, qty = 0, stock = 0;
                            unitmf = Convert.ToDecimal(dt_item_details.Rows[0]["UnitMF"].ToString());
                            qty = Convert.ToDecimal(dt_stock_in.Rows[m]["buy_Qty"].ToString());

                            if (unitmf > 0)
                            {
                                if (dt_item_details.Rows[0]["Unit1"].ToString().Trim() == dt_stock_in.Rows[m]["Unit"].ToString().Trim())
                                {
                                    stock = qty * unitmf;
                                }
                                else
                                {
                                    stock = qty;

                                }
                            }
                            else
                            {
                                stock = qty;

                            }
                            last_stk = last_stk + stock;
                            receipt_stk = receipt_stk + stock;
                            dgv_Stock.Rows.Add(itemid, Convert.ToDateTime(dt_stock_in.Rows[m]["Stock_date"].ToString()).ToString("dd-MM-yyyy"), dt_stock_in.Rows[m]["RefNo"].ToString(), "", "Stock In", dt_stock_in.Rows[m]["buy_Qty"].ToString() + " " + dt_stock_in.Rows[m]["Unit"].ToString(), "0.00", "", "", last_stk.ToString("0.00"));
                        }
                        Total_receipt_stk = Total_receipt_stk + receipt_stk;
                    }
                    decimal total_stk = 0;
                    total_stk = Convert.ToDecimal(dgv_Stock.Rows[dgv_Stock.Rows.Count - 1].Cells["balance"].Value.ToString());
                    dgv_Stock.Rows.Add("", "", "", "", "TOTAL :", Total_receipt_stk.ToString("0.00") + " " + unit, Total_receipt_rate.ToString("0.00"), Total_issuse_stk.ToString("0.00") + " " + unit, Total_issuse_rate.ToString("0.00"), total_stk.ToString("0.00") + " " + unit);
                    dgv_Stock.Rows[dgv_Stock.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Black;
                    dgv_Stock.Rows[dgv_Stock.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 8, FontStyle.Bold);
                }

            }
        }

        private void btn_all_stk_Click(object sender, EventArgs e)
        {
            dgv_Stock.Rows.Clear();
            txtitemcode.Text = "";
            string date_from = "", date_To = "",item_id="";
            date_from = dtp_from.Value.ToString("yyyy-MM-dd");
            date_To = dtp_to.Value.ToString("yyyy-MM-dd");
            decimal total_stock = 0, Total_receipt_stk = 0, Total_issuse_stk = 0, Total_receipt_rate = 0, Total_issuse_rate = 0;
            DataTable dt_all_items = this.cntrl.get_allitems_from_batch(date_from, date_To, stock_type);
            if(dt_all_items.Rows.Count>0)
            {
                for(int p=0;p< dt_all_items.Rows.Count;p++)
                {
                    string unit = "";
                    item_id = dt_all_items.Rows[p]["Item_Code"].ToString(); 
                    DataTable dt_item_details = this.cntrl.get_opening_stock(item_id);
                    if(Convert.ToDecimal(dt_item_details.Rows[0]["UnitMF"].ToString())>0)
                    {
                        dgv_Stock.Rows.Add("", "", "", dt_item_details.Rows[0]["item_name"].ToString(), "( " + " where 1 " + dt_item_details.Rows[0]["Unit1"].ToString() + " have " + dt_item_details.Rows[0]["UnitMF"].ToString() + "" + dt_item_details.Rows[0]["Unit2"].ToString() + " )", "", "", "", "", "");
                        unit = dt_item_details.Rows[0]["Unit2"].ToString().Trim();
                    }
                    else
                    {
                        dgv_Stock.Rows.Add("", "", "", dt_item_details.Rows[0]["item_name"].ToString(), "", "", "", "", "", "");
                        unit = dt_item_details.Rows[0]["Unit1"].ToString().Trim();
                    }
                    dgv_Stock.Rows[dgv_Stock.Rows.Count-1].DefaultCellStyle.ForeColor = Color.BlueViolet;
                    dgv_Stock.Rows[dgv_Stock.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 8, FontStyle.Bold);

                    string date = "";
                    if (Convert.ToDecimal(dt_item_details.Rows[0]["Open_Stock"].ToString()) > 0)
                    {
                        decimal open_qty = 0;
                        if(Convert.ToInt32(dt_item_details.Rows[0]["UnitMF"].ToString()) >0)
                        {
                            open_qty = Convert.ToDecimal(dt_item_details.Rows[0]["Open_Stock"].ToString()) * Convert.ToDecimal(dt_item_details.Rows[0]["UnitMF"].ToString());
                        }
                        else
                        {
                            open_qty = Convert.ToDecimal(dt_item_details.Rows[0]["Open_Stock"].ToString());
                        }
                        total_stock = open_qty;// Convert.ToDecimal(dt_item_details.Rows[0]["Open_Stock"].ToString());
                        date = "Opening Stock Date : " + dt_item_details.Rows[0]["opening_stock_date"].ToString();
                    }
                    else
                        date = "";
                        dgv_Stock.Rows.Add("", "", "", "Opening Balance", date, "", "", "","" , total_stock.ToString("0.00"));
                   //purchase
                    DataTable dt_purchase_from = this.cntrl.get_purchase_from_to(date_from, date_To, item_id);
                    if (dt_purchase_from.Rows.Count > 0)
                    {
                        decimal total_Pstock = 0, receipt_stk = 0, receipt_rate = 0;

                        total_Pstock = total_Pstock + total_stock;
                        for (int m = 0; m < dt_purchase_from.Rows.Count; m++)
                        {
                            decimal unitmf = 0, qty = 0, stock = 0;
                            unitmf = Convert.ToDecimal(dt_purchase_from.Rows[0]["UnitMF"].ToString());
                            if (Convert.ToDecimal(dt_purchase_from.Rows[m]["FreeQty"].ToString()) > 0)
                            {
                                qty = Convert.ToDecimal(dt_purchase_from.Rows[m]["Qty"].ToString()) + Convert.ToDecimal(dt_purchase_from.Rows[m]["FreeQty"].ToString());
                            }
                            else
                                qty = Convert.ToDecimal(dt_purchase_from.Rows[m]["Qty"].ToString());

                            if (unitmf > 0)
                            {
                                if (dt_item_details.Rows[0]["Unit1"].ToString().Trim() == dt_purchase_from.Rows[m]["Unit"].ToString().Trim())
                                {
                                    stock = qty * unitmf;
                                }
                                else
                                {
                                    stock = qty;

                                }
                            }
                            else
                            {
                                stock = qty;

                            }
                            total_Pstock = total_Pstock + stock;
                            receipt_stk = receipt_stk + stock;
                            receipt_rate = receipt_rate + Convert.ToDecimal(dt_purchase_from.Rows[m]["Amount"].ToString());
                            dgv_Stock.Rows.Add(item_id, Convert.ToDateTime(dt_purchase_from.Rows[m]["PurchDate"].ToString()).ToString("dd-MM-yyyy"), dt_purchase_from.Rows[m]["PurchNumber"].ToString(), "Cash", "Purchase", dt_purchase_from.Rows[m]["Qty"].ToString() + " " + dt_purchase_from.Rows[m]["Unit"].ToString(), Convert.ToDecimal(dt_purchase_from.Rows[m]["Amount"].ToString()).ToString("0.00"), "", "", total_Pstock.ToString("0.00"));
                        }
                        Total_receipt_stk = Total_receipt_stk + receipt_stk;
                        Total_receipt_rate = Total_receipt_rate + receipt_rate;
                    }
                    DataTable dt_purchase_return_from = this.cntrl.get_purchase_return_from_to(date_from, date_To, item_id);
                    if (dt_purchase_return_from.Rows.Count > 0)
                    {
                        decimal issuse_stk = 0, total_Pstock = 0, last_stk = 0, issuse_rate = 0;
                        total_Pstock = total_Pstock + total_stock;
                        last_stk = Convert.ToDecimal(dgv_Stock.Rows[dgv_Stock.Rows.Count - 1].Cells["balance"].Value.ToString());
                        for (int m = 0; m < dt_purchase_return_from.Rows.Count; m++)
                        {
                            decimal unitmf = 0, qty = 0, stock = 0;
                            unitmf = Convert.ToDecimal(dt_item_details.Rows[0]["UnitMF"].ToString());
                            if (Convert.ToDecimal(dt_purchase_return_from.Rows[m]["FreeQty"].ToString()) > 0)
                            {
                                qty = Convert.ToDecimal(dt_purchase_return_from.Rows[m]["Qty"].ToString()) + Convert.ToDecimal(dt_purchase_return_from.Rows[m]["FreeQty"].ToString());
                            }
                            else
                                qty = Convert.ToDecimal(dt_purchase_return_from.Rows[m]["Qty"].ToString());

                            if (unitmf > 0)
                            {
                                if (dt_item_details.Rows[0]["Unit1"].ToString().Trim() == dt_purchase_return_from.Rows[m]["Unit"].ToString().Trim())
                                {
                                    stock = qty * unitmf;
                                }
                                else
                                {
                                    stock = qty;
                                }
                            }
                            else
                            {
                                stock = qty;
                            }
                            last_stk = last_stk - stock;
                            issuse_stk = issuse_stk + stock;
                            issuse_rate = issuse_rate + Convert.ToDecimal(dt_purchase_return_from.Rows[m]["Rate"].ToString());
                            dgv_Stock.Rows.Add(item_id, Convert.ToDateTime(dt_purchase_return_from.Rows[m]["ReturnDate"].ToString()).ToString("dd-MM-yyyy"), dt_purchase_return_from.Rows[m]["RetNumber"].ToString() + " , Purchase No :" + dt_purchase_return_from.Rows[m]["PurchNumber"].ToString(), "Cash", "Purchase Return", "", "", dt_purchase_return_from.Rows[m]["Qty"].ToString()+" " + dt_purchase_return_from.Rows[m]["Unit"].ToString(),Convert.ToDecimal( dt_purchase_return_from.Rows[m]["Rate"].ToString()).ToString("0.00"), last_stk.ToString("0.00"));
                        }
                        Total_issuse_stk = Total_issuse_stk + issuse_stk;
                        Total_issuse_rate = Total_issuse_rate + issuse_rate;
                    }

                    DataTable dt_sales_from = this.cntrl.get_sales_from_to(date_from, date_To, item_id);
                    if (dt_sales_from.Rows.Count > 0)
                    {
                        decimal last_stk = 0, issuse_stk = 0, issuse_rate = 0;
                        last_stk = Convert.ToDecimal(dgv_Stock.Rows[dgv_Stock.Rows.Count - 1].Cells["balance"].Value.ToString());
                        for (int i = 0; i < dt_sales_from.Rows.Count; i++)
                        {
                            decimal unitmf = 0, qty = 0, stock = 0;
                            unitmf = Convert.ToDecimal(dt_sales_from.Rows[0]["UnitMF"].ToString());
                            if (Convert.ToDecimal(dt_sales_from.Rows[i]["FreeQty"].ToString()) > 0)
                            {
                                qty = Convert.ToDecimal(dt_sales_from.Rows[i]["Qty"].ToString()) + Convert.ToDecimal(dt_sales_from.Rows[i]["FreeQty"].ToString());
                            }
                            else
                                qty = Convert.ToDecimal(dt_sales_from.Rows[i]["Qty"].ToString());

                            if (unitmf > 0)
                            {
                                if (dt_item_details.Rows[0]["Unit1"].ToString().Trim() == dt_sales_from.Rows[i]["Unit"].ToString().Trim())
                                {
                                    stock = qty * unitmf;
                                }
                                else
                                {
                                    stock = qty;
                                }
                            }
                            else
                            {
                                stock = qty;
                            }
                            issuse_stk = issuse_stk + stock;
                            last_stk = last_stk - stock;
                            issuse_rate = issuse_rate + Convert.ToDecimal(dt_sales_from.Rows[i]["TotalAmount"].ToString());
                            dgv_Stock.Rows.Add(item_id, Convert.ToDateTime(dt_sales_from.Rows[i]["InvDate"].ToString()).ToString("dd-MM-yyyy"), dt_sales_from.Rows[i]["InvNumber"].ToString(), "Cash", "Sales", "", "", dt_sales_from.Rows[i]["Qty"].ToString() + " " + dt_sales_from.Rows[i]["Unit"].ToString(), Convert.ToDecimal(dt_sales_from.Rows[i]["TotalAmount"].ToString()).ToString("0.00"), last_stk.ToString("0.00"));
                        }
                        Total_issuse_stk = Total_issuse_stk + issuse_stk;
                        Total_issuse_rate = Total_issuse_rate + issuse_rate;
                    }
                    DataTable dt_sales_return_from = this.cntrl.get_sales_return_from_to(date_from, date_To, item_id);
                    if (dt_sales_return_from.Rows.Count > 0)
                    {
                        decimal last_stk = 0, receipt_stk = 0, receipt_rate = 0;
                        last_stk = Convert.ToDecimal(dgv_Stock.Rows[dgv_Stock.Rows.Count - 1].Cells["balance"].Value.ToString());
                        for (int i = 0; i < dt_sales_return_from.Rows.Count; i++)
                        {
                            decimal unitmf = 0, qty = 0, stock = 0;
                            unitmf = Convert.ToDecimal(dt_item_details.Rows[0]["UnitMF"].ToString());
                            if (Convert.ToDecimal(dt_sales_return_from.Rows[i]["FreeQty"].ToString()) > 0)
                            {
                                qty = Convert.ToDecimal(dt_sales_return_from.Rows[i]["Qty"].ToString()) + Convert.ToDecimal(dt_sales_return_from.Rows[i]["FreeQty"].ToString());
                            }
                            else
                                qty = Convert.ToDecimal(dt_sales_return_from.Rows[i]["Qty"].ToString());

                            if (unitmf > 0)
                            {
                                if (dt_item_details.Rows[0]["Unit1"].ToString().Trim() == dt_sales_return_from.Rows[i]["Unit"].ToString().Trim())
                                {
                                    stock = qty * unitmf;
                                }
                                else
                                {
                                    stock = qty;
                                }
                            }
                            else
                            {
                                stock = qty;
                            }
                            last_stk = last_stk + stock;
                            receipt_stk = receipt_stk + stock;
                            receipt_rate = receipt_rate + Convert.ToDecimal(dt_sales_return_from.Rows[i]["Rate"].ToString());
                            dgv_Stock.Rows.Add(item_id, Convert.ToDateTime(dt_sales_return_from.Rows[i]["ReturnDate"].ToString()).ToString("dd-MM-yyyy"), dt_sales_return_from.Rows[i]["RetNumber"].ToString() + ",  InvoiceNo :" + dt_sales_return_from.Rows[i]["InvNumber"].ToString(), "Cash", "Sales Return", dt_sales_return_from.Rows[i]["Qty"].ToString()+" " + dt_sales_return_from.Rows[i]["Unit"].ToString(),Convert.ToDecimal( dt_sales_return_from.Rows[i]["Rate"].ToString()).ToString("0.00"), "", "", last_stk.ToString("0.00"));
                        }
                        Total_receipt_stk = Total_receipt_stk + receipt_stk;
                        Total_receipt_rate = Total_receipt_rate + receipt_rate;
                    }
                    //stock updation
                    //DataTable dt_stock_updation_from_to = this.cntrl.get_stock_adjuctment_from_to(date_from, date_To, item_id);
                    //if(dt_stock_updation_from_to.Rows.Count>0)
                    //{
                    //    decimal last_stk = 0, receipt_stk = 0, receipt_rate = 0, issuse_stk = 0, issuse_rate = 0; 
                    //    last_stk = Convert.ToDecimal(dgv_Stock.Rows[dgv_Stock.Rows.Count - 1].Cells["balance"].Value.ToString());
                    //    for (int i = 0; i < dt_stock_updation_from_to.Rows.Count; i++)
                    //    {
                    //        decimal unitmf = 0, qty = 0, stock = 0;
                    //        unitmf = Convert.ToDecimal(dt_item_details.Rows[0]["UnitMF"].ToString());
                    //            qty = Convert.ToDecimal(dt_stock_updation_from_to.Rows[i]["upate_qty"].ToString());

                    //        if (unitmf > 0)
                    //        {
                    //            if (dt_item_details.Rows[0]["Unit1"].ToString().Trim() == dt_stock_updation_from_to.Rows[i]["unit"].ToString().Trim())
                    //            {
                    //                stock = qty * unitmf;
                    //            }
                    //            else
                    //            {
                    //                stock = qty;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            stock = qty;
                    //        }
                    //        if(dt_stock_updation_from_to.Rows[i]["Action"].ToString().Trim()== "Adjust to Excess")
                    //        {
                    //             last_stk = last_stk + stock;
                    //            receipt_stk = receipt_stk + stock;
                    //        }
                    //        else if(dt_stock_updation_from_to.Rows[i]["Action"].ToString().Trim() == "Adjust to Shortage")
                    //        {
                    //            last_stk = last_stk - stock;
                    //            issuse_stk = issuse_stk + stock;
                    //        }
                    //        else if(dt_stock_updation_from_to.Rows[i]["Action"].ToString().Trim() == "Adjust to Damage")
                    //        {
                    //            last_stk = last_stk - stock;
                    //            issuse_stk = issuse_stk + stock;
                    //        }
                    //        if (dt_stock_updation_from_to.Rows[i]["Action"].ToString().Trim() == "Adjust to Excess")
                    //        {
                    //            dgv_Stock.Rows.Add(item_id, Convert.ToDateTime(dt_stock_updation_from_to.Rows[i]["updation_date"].ToString()).ToString("dd-MM-yyyy"), dt_stock_updation_from_to.Rows[i]["id"].ToString(), "", "Stock Adjustment", dt_stock_updation_from_to.Rows[i]["upate_qty"].ToString() + " " + dt_stock_updation_from_to.Rows[i]["unit"].ToString(), "0.00", "", "", last_stk.ToString("0.00"));

                    //        }
                    //        else
                    //        {
                    //            dgv_Stock.Rows.Add(item_id, Convert.ToDateTime(dt_stock_updation_from_to.Rows[i]["updation_date"].ToString()).ToString("dd-MM-yyyy"), dt_stock_updation_from_to.Rows[i]["id"].ToString(), "", "Stock Adjustment", "", "", dt_stock_updation_from_to.Rows[i]["upate_qty"].ToString() + " " + dt_stock_updation_from_to.Rows[i]["unit"].ToString(), "0.00",  last_stk.ToString("0.00"));

                    //        }
                    //    }
                    //    Total_issuse_stk = Total_issuse_stk + issuse_stk;
                    //    Total_receipt_stk = Total_receipt_stk + receipt_stk;
                    //}
                    DataTable dt_stock_out = this.cntrl.get_stock_transfer_from_to(date_from, date_To, item_id);
                    if(dt_stock_out.Rows.Count>0)
                    {
                        decimal last_stk = 0, issuse_stk = 0;
                        last_stk = Convert.ToDecimal(dgv_Stock.Rows[dgv_Stock.Rows.Count - 1].Cells["balance"].Value.ToString());
                        for (int i = 0; i < dt_stock_out.Rows.Count; i++)
                        {
                            decimal unitmf = 0, qty = 0, stock = 0;
                            unitmf = Convert.ToDecimal(dt_item_details.Rows[0]["UnitMF"].ToString());
                                qty = Convert.ToDecimal(dt_stock_out.Rows[i]["Given_Qty"].ToString());

                            if (unitmf > 0)
                            {
                                if (dt_item_details.Rows[0]["Unit1"].ToString().Trim() == dt_stock_out.Rows[i]["unit"].ToString().Trim())
                                {
                                    stock = qty * unitmf;
                                }
                                else
                                {
                                    stock = qty;
                                }
                            }
                            else
                            {
                                stock = qty;
                            }
                            issuse_stk = issuse_stk + stock;
                            last_stk = last_stk - stock;
                            dgv_Stock.Rows.Add(item_id, Convert.ToDateTime(dt_stock_out.Rows[i]["stock_date"].ToString()).ToString("dd-MM-yyyy"), dt_stock_out.Rows[i]["RefNo"].ToString(), "", "Stock Out", "", "", dt_stock_out.Rows[i]["Given_Qty"].ToString() + " " + dt_stock_out.Rows[i]["unit"].ToString(), "0.00", last_stk.ToString("0.00"));
                        }
                        Total_issuse_stk = Total_issuse_stk + issuse_stk;
                    }
                    DataTable dt_stock_in = this.cntrl.get_stock_in_from_to(date_from, date_To, item_id);
                    if(dt_stock_in.Rows.Count>0)
                    {
                        decimal last_stk = 0, receipt_stk = 0;
                        last_stk = Convert.ToDecimal(dgv_Stock.Rows[dgv_Stock.Rows.Count - 1].Cells["balance"].Value.ToString());
                        for (int m = 0; m < dt_stock_in.Rows.Count; m++)
                        {
                            decimal unitmf = 0, qty = 0, stock = 0;
                            unitmf = Convert.ToDecimal(dt_item_details.Rows[0]["UnitMF"].ToString());
                                qty = Convert.ToDecimal(dt_stock_in.Rows[m]["buy_Qty"].ToString());

                            if (unitmf > 0)
                            {
                                if (dt_item_details.Rows[0]["Unit1"].ToString().Trim() == dt_stock_in.Rows[m]["Unit"].ToString().Trim())
                                {
                                    stock = qty * unitmf;
                                }
                                else
                                {
                                    stock = qty;

                                }
                            }
                            else
                            {
                                stock = qty;

                            }
                            last_stk = last_stk + stock;
                            receipt_stk = receipt_stk + stock;
                            dgv_Stock.Rows.Add(item_id, Convert.ToDateTime(dt_stock_in.Rows[m]["Stock_date"].ToString()).ToString("dd-MM-yyyy"), dt_stock_in.Rows[m]["RefNo"].ToString(), "", "Stock In", dt_stock_in.Rows[m]["buy_Qty"].ToString() + " " + dt_stock_in.Rows[m]["Unit"].ToString(),"0.00", "", "", last_stk.ToString("0.00"));
                        }
                        Total_receipt_stk = Total_receipt_stk + receipt_stk;
                    }
                    decimal total_stk = 0;
                    total_stk = Convert.ToDecimal(dgv_Stock.Rows[dgv_Stock.Rows.Count - 1].Cells["balance"].Value.ToString());
                    dgv_Stock.Rows.Add("", "", "", "", "TOTAL :", Total_receipt_stk.ToString("0.00")+" "+ unit, Total_receipt_rate.ToString("0.00"), Total_issuse_stk.ToString("0.00")+" "+ unit, Total_issuse_rate.ToString("0.00"), total_stk.ToString("0.00")+" "+ unit);
                    dgv_Stock.Rows[dgv_Stock.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Black;
                    dgv_Stock.Rows[dgv_Stock.Rows.Count - 1].DefaultCellStyle.Font= new System.Drawing.Font("Sego UI", 8, FontStyle.Bold);
                    total_stock = 0; Total_receipt_stk = 0; Total_receipt_rate = 0; Total_issuse_stk = 0; Total_issuse_rate = 0; total_stk = 0;
                }
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (dgv_Stock.Rows.Count > 0)
            {
                string strclinicname = "", strphone = "", DlNumber = "", DlNumber2 = "", website = "", logo_name = "";
                DataTable print_settng = this.prnt_ctrl.Get_inventory_id();
                System.Data.DataTable dtp = this.cntrl.companydetails();
                if (dtp.Rows.Count > 0)
                {
                    if (dtp.Rows.Count > 0)
                    {
                        string clinicn = "";
                        if (print_settng.Rows.Count > 0)
                        {
                            clinicn = print_settng.Rows[0]["header"].ToString();
                            strclinicname = clinicn.Replace("¤", "'");
                            DlNumber = print_settng.Rows[0]["left_text"].ToString();
                            strphone = print_settng.Rows[0]["right_text"].ToString();
                        }
                        DlNumber2 = dtp.Rows[0]["email"].ToString();
                        website = dtp.Rows[0]["website"].ToString();
                        logo_name = dtp.Rows[0]["path"].ToString();
                    }
                }
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                StreamWriter sWrite = new StreamWriter(Apppath + "\\stockledger_print.html");
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
                sWrite.WriteLine("<br>");
                string Appath = System.IO.Directory.GetCurrentDirectory();
                if (File.Exists(Appath + "\\" + logo_name))
                {
                    sWrite.WriteLine("<table align='center' style='width:900px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td width='30px' height='50px' align='left' rowspan='3'><img src='" + Appath + "\\" + logo_name + "'style='width:70px;height:70px;' ></td>  ");
                    sWrite.WriteLine("<td width='870px' align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=4><b>&nbsp;" + strclinicname + "</font> <br><FONT  COLOR=black  face='Segoe UI' SIZE=2>&nbsp;" + DlNumber + "<br>&nbsp;" + strphone + " </b></td></tr>");
                    sWrite.WriteLine("</table>");
                }
                else
                {
                    sWrite.WriteLine("<table align='center' style='width:900px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td  align='left' height='20px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + strclinicname + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td  align='left' height='20px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;" + DlNumber + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' height='20px' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + strphone + "</font></td></tr>");
                    sWrite.WriteLine("</table>");
                }
                sWrite.WriteLine("<table align='center' style='width:900px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr><td align='left'  ><hr/></td></tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3> STOCK LEDGER </font></center></b></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<table align='center' style='width:900px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td  colspan=4  a lign='left' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>From Date  :" + dtp_from.Value.ToString("dd-MM-yyyy") + " </font></td>");
                sWrite.WriteLine("<td colspan=4  align='right' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>To Date:" + dtp_to.Value.ToString("dd-MM-yyyy") + " </font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr><td align='left' colspan=9><hr/></td></tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='left'  width='70'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=3><b>Date</b></font></td>");
                sWrite.WriteLine("<td align='left'  width='70'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=3><b>Voucher No</b></font></td>");
                sWrite.WriteLine("<td align='left'  width='200'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=3><b>Particular</b></font></td>");
                sWrite.WriteLine("<td align='left'  width='60'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=3><b>Type</b></font></td>");
                sWrite.WriteLine("<td align='right'  width='100'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=3><b>Receipt</b></font></td>");
                sWrite.WriteLine("<td align='right'  width='100'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=3><b>Receipt Rate (INR)</b></font></td>");
                sWrite.WriteLine("<td align='right'  width='100'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=3><b>Issuse</b></font></td>");
                sWrite.WriteLine("<td align='right'  width='100'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=3><b>Issuse Rate (INR)</b></font></td>");
                sWrite.WriteLine("<td align='right'  width='100'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=3><b>Balance</b></font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr><td align='left' colspan=9><hr/></td></tr>");
                for (int i = 0; i < dgv_Stock.Rows.Count; i++)
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + dgv_Stock.Rows[i].Cells["date"].Value.ToString() + "</font></td>");
                    sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + dgv_Stock.Rows[i].Cells["voucher"].Value.ToString() + "</font></td>");
                    sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + dgv_Stock.Rows[i].Cells["particulars"].Value.ToString() + "</font></td>");
                    if (dgv_Stock.Rows[i].Cells["type"].Value.ToString()=="TOTAL")
                    {
                        sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=3><b>" + dgv_Stock.Rows[i].Cells["type"].Value.ToString() + "</b></font></td>");

                    }
                    else
                    sWrite.WriteLine("<td align='left'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + dgv_Stock.Rows[i].Cells["type"].Value.ToString() + "</font></td>");
                    
                    sWrite.WriteLine("<td align='right'><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + dgv_Stock.Rows[i].Cells["receipts"].Value.ToString() + "</font></td>");
                    sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + dgv_Stock.Rows[i].Cells["receipt_rate"].Value.ToString() + "</font></td>");
                    sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + dgv_Stock.Rows[i].Cells["issuse"].Value.ToString() + "</font></td>");
                    sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + dgv_Stock.Rows[i].Cells["issuse_rate"].Value.ToString() + "</font></td>");
                    sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva,  Sego UI' SIZE=2>" + dgv_Stock.Rows[i].Cells["balance"].Value.ToString() + "</font></td>");
                    sWrite.WriteLine("</tr>");
                }
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<script>window.print();</script>");
                sWrite.WriteLine("</body >");
                sWrite.WriteLine("</html>");
                sWrite.Close();
                System.Diagnostics.Process.Start(Apppath + "\\stockledger_print.html");

            }
        }
    }
}
