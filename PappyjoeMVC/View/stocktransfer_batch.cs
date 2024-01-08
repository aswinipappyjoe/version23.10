using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PappyjoeMVC.Controller;
using PappyjoeMVC.Model;
using System.Data;

namespace PappyjoeMVC.View
{
    public partial class stocktransfer_batch : Form
    {
        batchsale_controller bcntrl = new batchsale_controller();
           stocktransfer_batch_controller cntrl = new stocktransfer_batch_controller();
        public static string ItemCode;
        public static decimal Quantity;
        public static string Unit;
        public decimal TotalStock = 0;
        public decimal TotalQty = 0;
        decimal curent_Stock;
        DataTable dtb_Sales = new DataTable();
        DataTable dt_batchtransfer_edit = new DataTable();
        public static bool edit_flag = false;
        public string Action = "",formname="",updation_action="";
        public stocktransfer_batch()
        {
            InitializeComponent();
        }

        public stocktransfer_batch(string itemId, decimal qty, string unit)
        {
            InitializeComponent();
            ItemCode = itemId;
            Quantity = qty;
            Unit = unit;
        }

        public stocktransfer_batch(string itemId, decimal qty, DataTable frmBatchsale_edit, string unit)
        {
            InitializeComponent();
            ItemCode = itemId;
            Quantity = qty;
            dt_batchtransfer_edit = frmBatchsale_edit;
            Unit = unit;
            edit_flag = true;
        }
        public int rowindex = 0;
        private void stocktransfer_batch_Load(object sender, EventArgs e)
        {
            try
            {
                dgv_batchSale.AllowUserToAddRows = false;
                dgv_batchSale.Columns["colbatchNo"].ReadOnly = true;
                dgv_batchSale.Columns["ColStock"].ReadOnly = true;
                dgv_batchSale.Columns["clUnit"].ReadOnly = true;//
                dgv_batchSale.Columns["colCurrentStock"].ReadOnly = true;
                //dgv_batchSale.Columns["ColQty"].ReadOnly = true;
                dgv_batchSale.Columns["ColPrd_Date"].ReadOnly = true;
                dgv_batchSale.Columns["colExpDate"].ReadOnly = true;
                dgv_batchSale.Columns["rate"].ReadOnly = true;
                DataTable dt_opening = new DataTable();
                DataTable dt_batch = new DataTable();
                if (Action == "Stock Out")
                {
                    if (edit_flag == false)
                    {
                        dt_opening = this.bcntrl.get_item_frm_purchase(ItemCode);
                        dt_batch = this.cntrl.get_batchdetails_stock_out(ItemCode);
                    }
                        dt_opening = this.bcntrl.get_item_frm_purchase(ItemCode);
                    //if (dt_opening.Rows.Count == 0)
                    //{
                    //    DataTable dt_op = this.cntrl.get_batchdetails_opening(ItemCode);
                    //    opening_flag = true;
                    //    fill_grid(dt_op);
                    //    opening_flag = false;
                    //}
                    if (dt_batch.Rows.Count > 0)
                    {
                        fill_grid(dt_batch);
                    }
                    //fill_grid(dt_batch);
                    //if (dt_opening.Rows.Count == 0)
                    //{
                    //    DataTable dt_op = this.cntrl.get_batchdetails_opening(ItemCode);
                    //    opening_flag = true;
                    //    fill_grid(dt_op);
                    //    opening_flag = false;
                    //}
                    //else if (dtb.Rows.Count > 0)
                    //{
                    //    fill_grid(dtb);
                    //}
                    if (edit_flag == true)
                        {
                       if (dt_batchtransfer_edit.Rows.Count > 0)
                        {
                            if (dt_batchtransfer_edit.Rows.Count == 1)
                            {
                                dgv_batchSale.Rows.Clear();
                                for (int i = 0; i < dt_batchtransfer_edit.Rows.Count; i++)
                                {
                                    decimal edit_curent_Stock = 0;

                                    dgv_batchSale.Rows.Add();
                                    dgv_batchSale.Rows[i].Cells["colentryNo"].Value = dt_batchtransfer_edit.Rows[0]["colBatchEntry"].ToString();
                                    dgv_batchSale.Rows[i].Cells["colbatchNo"].Value = dt_batchtransfer_edit.Rows[0]["colBatchnumber"].ToString();
                                    dgv_batchSale.Rows[0].Cells["clUnit"].Value = Unit;
                                    dgv_batchSale.Rows[0].Cells["ColQty"].Value = Quantity;
                                    dgv_batchSale.Rows[i].Cells["rate"].Value = dt_batchtransfer_edit.Rows[0]["Cost"].ToString();// dtbBatchsale.Rows[i]["colQuantity"].ToString();// Quantity.ToString();
                                    dgv_batchSale.Rows[0].Cells["ColStock"].Value = dt_batchtransfer_edit.Rows[0]["stock"].ToString();//colStock
                                    edit_curent_Stock = Convert.ToDecimal(dgv_batchSale.Rows[0].Cells["ColStock"].Value.ToString()) - Convert.ToDecimal(Quantity);
                                    dgv_batchSale.Rows[0].Cells["colCurrentStock"].Value = edit_curent_Stock.ToString();
                                    TotalQty = Quantity;
                                    dgv_batchSale.Rows[0].Cells["ColPrd_Date"].Value = dt_batchtransfer_edit.Rows[0]["prddate"].ToString();//).ToString("MM/dd/yyyy")
                                    if (dt_batchtransfer_edit.Rows[0]["expdate"].ToString() != "")
                                    {
                                        dgv_batchSale.Rows[0].Cells["colExpDate"].Value = dt_batchtransfer_edit.Rows[0]["expdate"].ToString();//).ToString("MM/dd/yyyy").TrimStart();
                                                                                                                                     //dgv_batchSale.Rows[i].Cells["exp"].Value = Convert.ToDateTime(dtb.Rows[i]["ExpDate"].ToString()).ToString("MM/dd/yyyy").TrimStart();
                                    }
                                    else
                                        dgv_batchSale.Rows[0].Cells["colExpDate"].Value = "";


                                    //dgv_batchSale.Rows[i].Cells["ColStock"].Value = dtbBatchsale.Rows[i]["colStock"].ToString();
                                    //TotalStock = TotalStock + Convert.ToDecimal(dtbBatchsale.Rows[i]["colStock"].ToString());
                                    //if (itemcheck_(dt_batchtransfer_edit.Rows[i]["colBatchnumber"].ToString()) == 1)
                                    //{
                                    //    dgv_batchSale.Rows[rowindex].Cells["clUnit"].Value = Unit;
                                    //    dgv_batchSale.Rows[rowindex].Cells["ColQty"].Value = Quantity;// dtbBatchsale.Rows[i]["colQuantity"].ToString();// Quantity.ToString();

                                    //    edit_curent_Stock = Convert.ToDecimal(dgv_batchSale.Rows[rowindex].Cells["ColStock"].Value.ToString()) - Convert.ToDecimal(dt_batchtransfer_edit.Rows[i]["colQuantity"].ToString());
                                    //    dgv_batchSale.Rows[i].Cells["colCurrentStock"].Value = edit_curent_Stock.ToString();
                                    //    TotalQty = Quantity;
                                    //}
                                }
                            }
                            else
                            {
                                calc_new();// calc();
                            }
                            //calc();
                            
                        }
                            else
                            {
                                MessageBox.Show("No Records Found..", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        else
                        {
                        calc_new();// calc();
                        }
                    rate_calculation();
                    }
                    else if (Action == "Stock In")
                    {
                        DataTable dt_batch1 = this.cntrl.get_batchdetails(ItemCode);
                        fill_grid(dt_batch1);
                        StockIn_calc();
                    }
                dgv_batchSale.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
                dgv_batchSale.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgv_batchSale.EnableHeadersVisualStyles = false;
                dgv_batchSale.ColumnHeadersDefaultCellStyle.Font = new Font("Sego UI", 9, FontStyle.Regular);
                dgv_batchSale.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_batchSale.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                foreach (DataGridViewColumn cl in dgv_batchSale.Columns)
                {
                    cl.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public int itemcheck_(string batch)
        {
            int affected = 0;
            for (int i = 0; i < dgv_batchSale.Rows.Count; i++)
            {
                //for (int j = 0; j < dgv_SalesItem.Columns.Count; j++)
                //{
                if (dgv_batchSale.Rows[i].Cells["colbatchNo"].Value != null && batch == dgv_batchSale.Rows[i].Cells["colbatchNo"].Value.ToString())
                {
                    affected = 1; rowindex = i;
                }
                //}
            }
            return affected;
        }
        public static decimal total_rate = 0;
        Inventory_model inv_model = new Inventory_model();
        public void rate_calculation()
        {
            decimal percnt_amt = 0, total_cost = 0, value1 = 0, value2 = 0, percentage = 0, unitMf = 0, cost = 0;
            batchentry = 0;
            total_rate = 0;
            for (int i = 0; i < dgv_batchSale.Rows.Count; i++)
            {
                DataTable dtb_item = this.inv_model.itemdetails(ItemCode);
                DataTable dt = this.inv_model.get_batch_wise_rate(dgv_batchSale.Rows[i].Cells["colbatchNo"].Value.ToString(), ItemCode);
                if (dgv_batchSale.Rows[i].Cells["ColQty"].Value.ToString() != "0")
                {
                    total_rate = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["rate"].Value.ToString());
                    batchentry = Convert.ToInt32(dgv_batchSale.Rows[i].Cells["colentryNo"].Value.ToString());
                    //if(dt.Rows.Count>0)
                    //{
                    //    unitMf = Convert.ToDecimal(dtb_item.Rows[0]["UnitMF"].ToString());
                    //    if (dtb_item.Rows[0]["OneUnitOnly"].ToString() == "False")
                    //    {
                    //        if (dt.Rows[0]["purch_unit2"].ToString() == "No")
                    //        {
                    //            if (Unit == dtb_item.Rows[0]["Unit2"].ToString())
                    //            {
                    //                cost = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["rate"].Value.ToString()) / unitMf;

                    //            }
                    //            else
                    //            {
                    //                cost = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["rate"].Value.ToString());
                    //            }
                    //        }
                    //        if (Unit == dtb_item.Rows[0]["Unit2"].ToString())
                    //        {
                    //            cost = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["rate"].Value.ToString());
                    //        }
                    //        else
                    //        {
                    //            value1 = unitMf * Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["rate"].Value.ToString());
                    //            cost = value1;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        cost = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["rate"].Value.ToString());
                    //    }
                    //    total_rate = cost;
                    //}

                }
            }
        }
        //public void rate_calculation()
        //{
        //    total_rate = 0;
        //    for (int i = 0; i < dgv_batchSale.Rows.Count; i++)
        //    {
        //        if (dgv_batchSale.Rows[i].Cells["ColQty"].Value.ToString() != "0")
        //        {
        //            total_rate = total_rate + Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["rate"].Value.ToString());
        //        }
        //    }
        //}
        public void fill_grid(DataTable dtb)
        {
            {
                DataTable dt_salesrate = new DataTable();
                decimal percnt_amt = 0, total_cost = 0, value1 = 0, value2 = 0, percentage = 0, unitMf = 0, cost = 0;

                for (int i = 0; i < dtb.Rows.Count; i++)
                {
                    dgv_batchSale.Rows.Add();
                    dgv_batchSale.Rows[i].Cells["colentryNo"].Value = dtb.Rows[i]["Entry_No"].ToString();
                    dgv_batchSale.Rows[i].Cells["colbatchNo"].Value = dtb.Rows[i]["BatchNumber"].ToString();


                    // batch rate calculations
                    DataTable dtb_item = this.bcntrl.itemdetails(ItemCode);
                    if (dtb_item.Rows.Count > 0)
                    {
                        unitMf = Convert.ToDecimal(dtb_item.Rows[0]["UnitMF"].ToString());
                        if (dtb_item.Rows[0]["OneUnitOnly"].ToString() == "False")
                        {
                            if (dtb.Rows[i]["purch_unit2"].ToString() == "No")//Unit2
                            {
                                if (Unit == dtb_item.Rows[0]["Unit2"].ToString())
                                {
                                    cost = Convert.ToDecimal(dtb.Rows[i]["batch_sales_rate"].ToString()) / unitMf;

                                }
                                else
                                {
                                    cost = Convert.ToDecimal(dtb.Rows[i]["batch_sales_rate"].ToString());
                                }
                            }
                            else
                            {
                                if (Unit == dtb_item.Rows[0]["Unit2"].ToString())
                                {
                                    //value1 = unitMf * Convert.ToDecimal(dtb.Rows[i]["batch_sales_rate"].ToString());
                                    cost = Convert.ToDecimal(dtb.Rows[i]["batch_sales_rate"].ToString());// / unitMf;
                                    //cost = value1;
                                    ///*dt_salesrate*/ = this.cntrl.get_item_salesrate_minimun(ItemCode);
                                    //cost = Convert.ToDecimal(dtb.Rows[i]["batch_sales_rate"].ToString());


                                }
                                else
                                {
                                    //cost = Convert.ToDecimal(dtb.Rows[i]["batch_sales_rate"].ToString());
                                    //dt_salesrate = this.cntrl.get_item_salesrate(ItemCode);
                                    value1 = unitMf * Convert.ToDecimal(dtb.Rows[i]["batch_sales_rate"].ToString());
                                    cost = value1;
                                }
                            }

                        }
                        else
                        {
                            cost = Convert.ToDecimal(dtb.Rows[i]["batch_sales_rate"].ToString());
                        }

                    }
                    //if(opening_flag==false)
                    //{
                    //    dgv_batchSale.Rows[i].Cells["rate"].Value= cost;
                    //}
                    //else
                    //{
                    dgv_batchSale.Rows[i].Cells["rate"].Value = cost;// "0.000";
                                                                     //}


                    dgv_batchSale.Rows[i].Cells["ColStock"].Value = dtb.Rows[i]["Qty"].ToString();// default_qty
                    TotalStock = TotalStock + Convert.ToDecimal(dtb.Rows[i]["Qty"].ToString()); //default_qty;
                                                                                                //if(dgv_batchSale.Rows[i].Cells["ColStock"].Value =="0")

                    dgv_batchSale.Rows[i].Cells["ColPrd_Date"].Value = Convert.ToDateTime(dtb.Rows[i]["PrdDate"].ToString()).ToString("MM/dd/yyyy");
                    if (dtb.Rows[i]["ExpDate"].ToString() != "")
                    {
                        dgv_batchSale.Rows[i].Cells["colExpDate"].Value = dtb.Rows[i]["ExpDate"].ToString();//).ToString("MM/dd/yyyy").TrimStart();
                        //dgv_batchSale.Rows[i].Cells["exp"].Value = Convert.ToDateTime(dtb.Rows[i]["ExpDate"].ToString()).ToString("MM/dd/yyyy").TrimStart();
                    }
                    else
                        dgv_batchSale.Rows[i].Cells["colExpDate"].Value = "";
                    dgv_batchSale.Rows[i].Cells["clUnit"].Value = Unit;
                    dgv_batchSale.Rows[i].Cells["ColQty"].Value = 0;
                }
            }
        }
        //public void fill_grid(DataTable dtb)
        //{
        //    if (dtb.Rows.Count > 0)
        //    {
        //        DataTable dt_salesrate = new DataTable();
        //        for (int i = 0; i < dtb.Rows.Count; i++)
        //        {
        //            dgv_batchSale.Rows.Add();
        //            dgv_batchSale.Rows[i].Cells["colentryNo"].Value = dtb.Rows[i]["Entry_No"].ToString();
        //            dgv_batchSale.Rows[i].Cells["colbatchNo"].Value = dtb.Rows[i]["BatchNumber"].ToString();
        //            dgv_batchSale.Rows[i].Cells["ColStock"].Value = dtb.Rows[i]["Qty"].ToString();
        //            TotalStock = TotalStock + Convert.ToDecimal(dtb.Rows[i]["Qty"].ToString());
        //        }
        //    }
        //}
        public void fill_grid_stock_updation(DataTable dtb)
        {
            if (dtb.Rows.Count > 0)
            {
                DataTable dt_salesrate = new DataTable();
                TotalQty = Quantity;
                for (int i = 0; i < dtb.Rows.Count; i++)
                {
                    dgv_batchSale.Rows.Add();
                    dgv_batchSale.Rows[i].Cells["colentryNo"].Value = dtb.Rows[i]["Entry_No"].ToString();
                    dgv_batchSale.Rows[i].Cells["colbatchNo"].Value = dtb.Rows[i]["BatchNumber"].ToString();
                    dgv_batchSale.Rows[i].Cells["ColStock"].Value =dtb.Rows[i]["Qty"].ToString();
                    dgv_batchSale.Rows[i].Cells["clUnit"].Value = Unit;
                    dgv_batchSale.Rows[i].Cells["ColQty"].Value = 0;
                    dgv_batchSale.Rows[i].Cells["colCurrentStock"].Value = 0;
                    TotalStock = TotalStock + Convert.ToDecimal(dtb.Rows[i]["Qty"].ToString());
                }
            }
        }
        public void calc_new()
        {
            try
            {
                decimal Remaning_qty = 0;
                DataTable dt_unit1 = this.cntrl.get_item_unitmf(ItemCode);
                Remaning_qty = Quantity; TotalQty = Quantity;
                if (dt_unit1.Rows[0]["OneUnitOnly"].ToString() == "False")
                {
                    decimal unitmf = Convert.ToDecimal(dt_unit1.Rows[0]["UnitMF"].ToString());
                    if (dt_unit1.Rows[0]["Unit1"].ToString() == Unit)
                    {
                        Remaning_qty = Quantity * unitmf;
                        TotalQty = Quantity * unitmf;
                        foreach (DataGridViewRow dr in dgv_batchSale.Rows)
                        {
                            if (Convert.ToDecimal(dr.Cells["ColStock"].Value.ToString()) >= Remaning_qty)
                            {
                                dr.Cells["clUnit"].Value = Unit;
                                dr.Cells["ColQty"].Value = Remaning_qty.ToString();
                                curent_Stock = Convert.ToDecimal(dr.Cells["ColStock"].Value.ToString()) - Remaning_qty;// Convert.ToDecimal(dgv_batchSale.Rows[0].Cells["ColQty"].Value.ToString());
                                dr.Cells["colCurrentStock"].Value = curent_Stock.ToString();
                                break;
                            }
                        }

                    }
                    else
                    {
                        foreach (DataGridViewRow dr in dgv_batchSale.Rows)
                        {
                            if (Convert.ToDecimal(dr.Cells["ColStock"].Value.ToString()) >= TotalQty)
                            {
                                dr.Cells["clUnit"].Value = Unit;
                                dr.Cells["ColQty"].Value = TotalQty.ToString();
                                curent_Stock = Convert.ToDecimal(dr.Cells["ColStock"].Value.ToString()) - TotalQty;// Convert.ToDecimal(dgv_batchSale.Rows[0].Cells["ColQty"].Value.ToString());
                                dr.Cells["colCurrentStock"].Value = curent_Stock.ToString();
                                break;
                            }
                        }
                        //dgv_batchSale.Rows[0].Cells["clUnit"].Value = Unit;
                        //dgv_batchSale.Rows[0].Cells["ColQty"].Value = Remaning_qty.ToString();
                        //curent_Stock = Convert.ToDecimal(dgv_batchSale.Rows[0].Cells["ColStock"].Value.ToString()) - Convert.ToDecimal(dgv_batchSale.Rows[0].Cells["ColQty"].Value.ToString());
                        //dgv_batchSale.Rows[0].Cells["colCurrentStock"].Value = curent_Stock.ToString();
                    }
                }
                else
                {
                    foreach (DataGridViewRow dr in dgv_batchSale.Rows)
                    {
                        if (Convert.ToDecimal(dr.Cells["ColStock"].Value.ToString()) >= TotalQty)
                        {
                            dr.Cells["clUnit"].Value = Unit;
                            dr.Cells["ColQty"].Value = TotalQty.ToString();
                            curent_Stock = Convert.ToDecimal(dr.Cells["ColStock"].Value.ToString()) - TotalQty;// Convert.ToDecimal(dgv_batchSale.Rows[0].Cells["ColQty"].Value.ToString());
                            dr.Cells["colCurrentStock"].Value = curent_Stock.ToString();
                            break;
                        }
                    }
                    //dgv_batchSale.Rows[0].Cells["clUnit"].Value = Unit;
                    //dgv_batchSale.Rows[0].Cells["ColQty"].Value = Remaning_qty.ToString();
                    //curent_Stock = Convert.ToDecimal(dgv_batchSale.Rows[0].Cells["ColStock"].Value.ToString()) - Convert.ToDecimal(dgv_batchSale.Rows[0].Cells["ColQty"].Value.ToString());
                    //dgv_batchSale.Rows[0].Cells["colCurrentStock"].Value = curent_Stock.ToString();
                }
            }
            catch (Exception ex)
            {

            }

        }
        //public void calc()
        //{
        //    try
        //    {
        //        DataTable dt_unit1 = this.cntrl.get_item_unitmf(ItemCode);
        //        decimal Remaning_qty = 0;
        //        Remaning_qty = Quantity;
        //        TotalQty = Quantity;
        //        decimal stk_value = 0;
        //        if (dt_unit1.Rows[0]["OneUnitOnly"].ToString() == "False")
        //        {
        //            decimal unitmf = Convert.ToDecimal(dt_unit1.Rows[0]["UnitMF"].ToString());
        //            if (dt_unit1.Rows[0]["Unit1"].ToString() == Unit)
        //            {
        //                Remaning_qty = Quantity * unitmf;
        //                TotalQty = Quantity * unitmf;
        //                if (TotalQty <= TotalStock)
        //                {
        //                    for (int i = 0; i < dgv_batchSale.Rows.Count; i++)
        //                    {
        //                        if (Remaning_qty <= Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value))
        //                        {
        //                            dgv_batchSale.Rows[i].Cells["clUnit"].Value = Unit;
        //                            dgv_batchSale.Rows[i].Cells["ColQty"].Value = Remaning_qty.ToString();
        //                            curent_Stock = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value.ToString()) - Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColQty"].Value.ToString());
        //                            dgv_batchSale.Rows[i].Cells["colCurrentStock"].Value = curent_Stock.ToString();
        //                            break;
        //                        }
        //                        else
        //                        {
        //                            stk_value = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value.ToString());
        //                            dgv_batchSale.Rows[i].Cells["clUnit"].Value = Unit;
        //                            dgv_batchSale.Rows[i].Cells["ColQty"].Value = stk_value;
        //                            curent_Stock = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value.ToString()) - Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColQty"].Value.ToString());
        //                            dgv_batchSale.Rows[i].Cells["colCurrentStock"].Value = curent_Stock.ToString();
        //                            Remaning_qty = Remaning_qty - stk_value;
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if (TotalQty <= TotalStock)
        //                {
        //                    for (int i = 0; i < dgv_batchSale.Rows.Count; i++)
        //                    {
        //                        if (Remaning_qty <= Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value))
        //                        {
        //                            dgv_batchSale.Rows[i].Cells["clUnit"].Value = Unit;
        //                            dgv_batchSale.Rows[i].Cells["ColQty"].Value = Remaning_qty.ToString();
        //                            curent_Stock = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value.ToString()) - Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColQty"].Value.ToString());
        //                            dgv_batchSale.Rows[i].Cells["colCurrentStock"].Value = curent_Stock.ToString();
        //                            break;
        //                        }
        //                        else
        //                        {
        //                            stk_value = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value.ToString());
        //                            dgv_batchSale.Rows[i].Cells["clUnit"].Value = Unit;
        //                            dgv_batchSale.Rows[i].Cells["ColQty"].Value = stk_value;
        //                            curent_Stock = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value.ToString()) - Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColQty"].Value.ToString());
        //                            dgv_batchSale.Rows[i].Cells["colCurrentStock"].Value = curent_Stock.ToString();
        //                            Remaning_qty = Remaning_qty - stk_value;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (TotalQty <= TotalStock)
        //            {
        //                for (int i = 0; i < dgv_batchSale.Rows.Count; i++)
        //                {
        //                    if (Remaning_qty <= Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value))
        //                    {
        //                        dgv_batchSale.Rows[i].Cells["clUnit"].Value = Unit;
        //                        dgv_batchSale.Rows[i].Cells["ColQty"].Value = Remaning_qty.ToString();
        //                        curent_Stock = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value.ToString()) - Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColQty"].Value.ToString());
        //                        dgv_batchSale.Rows[i].Cells["colCurrentStock"].Value = curent_Stock.ToString();
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        stk_value = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value.ToString());
        //                        dgv_batchSale.Rows[i].Cells["clUnit"].Value = Unit;
        //                        dgv_batchSale.Rows[i].Cells["ColQty"].Value = stk_value;
        //                        curent_Stock = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value.ToString()) - Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColQty"].Value.ToString());
        //                        dgv_batchSale.Rows[i].Cells["colCurrentStock"].Value = curent_Stock.ToString();
        //                        Remaning_qty = Remaning_qty - stk_value;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                MessageBox.Show("Quantity is greater than the stock", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        //public void calc()
        //{
        //    try
        //    {
        //        DataTable dt_unit1 = this.cntrl.get_item_unitmf(ItemCode);
        //        decimal Remaning_qty = 0;
        //        Remaning_qty = Quantity;
        //        TotalQty = Quantity;
        //        decimal stk_value = 0;
        //        if (dt_unit1.Rows[0]["OneUnitOnly"].ToString() == "False")
        //        {
        //            decimal unitmf = Convert.ToDecimal(dt_unit1.Rows[0]["UnitMF"].ToString());
        //            if (dt_unit1.Rows[0]["Unit1"].ToString() == Unit)
        //            {
        //                Remaning_qty = Quantity * unitmf;
        //                TotalQty = Quantity * unitmf;
        //                if (TotalQty <= TotalStock)
        //                {
        //                    for (int i = 0; i < dgv_batchSale.Rows.Count; i++)
        //                    {
        //                        if (Remaning_qty <= Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value))
        //                        {
        //                            dgv_batchSale.Rows[i].Cells["clUnit"].Value = Unit;
        //                            dgv_batchSale.Rows[i].Cells["ColQty"].Value = Remaning_qty.ToString();
        //                            curent_Stock = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value.ToString()) - Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColQty"].Value.ToString());
        //                            dgv_batchSale.Rows[i].Cells["colCurrentStock"].Value = curent_Stock.ToString();
        //                            break;
        //                        }
        //                        else
        //                        {
        //                            stk_value = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value.ToString());
        //                            dgv_batchSale.Rows[i].Cells["clUnit"].Value = Unit;
        //                            dgv_batchSale.Rows[i].Cells["ColQty"].Value = stk_value;
        //                            curent_Stock = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value.ToString()) - Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColQty"].Value.ToString());
        //                            dgv_batchSale.Rows[i].Cells["colCurrentStock"].Value = curent_Stock.ToString();
        //                            Remaning_qty = Remaning_qty - stk_value;
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if (TotalQty <= TotalStock)
        //                {
        //                    for (int i = 0; i < dgv_batchSale.Rows.Count; i++)
        //                    {
        //                        if (Remaning_qty <= Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value))
        //                        {
        //                            dgv_batchSale.Rows[i].Cells["clUnit"].Value = Unit;
        //                            dgv_batchSale.Rows[i].Cells["ColQty"].Value = Remaning_qty.ToString();
        //                            curent_Stock = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value.ToString()) - Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColQty"].Value.ToString());
        //                            dgv_batchSale.Rows[i].Cells["colCurrentStock"].Value = curent_Stock.ToString();
        //                            break;
        //                        }
        //                        else
        //                        {
        //                            stk_value = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value.ToString());
        //                            dgv_batchSale.Rows[i].Cells["clUnit"].Value = Unit;
        //                            dgv_batchSale.Rows[i].Cells["ColQty"].Value = stk_value;
        //                            curent_Stock = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value.ToString()) - Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColQty"].Value.ToString());
        //                            dgv_batchSale.Rows[i].Cells["colCurrentStock"].Value = curent_Stock.ToString();
        //                            Remaning_qty = Remaning_qty - stk_value;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (TotalQty <= TotalStock)
        //            {
        //                for (int i = 0; i < dgv_batchSale.Rows.Count; i++)
        //                {
        //                    if (Remaning_qty <= Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value))
        //                    {
        //                        dgv_batchSale.Rows[i].Cells["clUnit"].Value = Unit;
        //                        dgv_batchSale.Rows[i].Cells["ColQty"].Value = Remaning_qty.ToString();
        //                        curent_Stock = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value.ToString()) - Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColQty"].Value.ToString());
        //                        dgv_batchSale.Rows[i].Cells["colCurrentStock"].Value = curent_Stock.ToString();
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        stk_value = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value.ToString());
        //                        dgv_batchSale.Rows[i].Cells["clUnit"].Value = Unit;
        //                        dgv_batchSale.Rows[i].Cells["ColQty"].Value = stk_value;
        //                        curent_Stock = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value.ToString()) - Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColQty"].Value.ToString());
        //                        dgv_batchSale.Rows[i].Cells["colCurrentStock"].Value = curent_Stock.ToString();
        //                        Remaning_qty = Remaning_qty - stk_value;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                MessageBox.Show("Quantity is greater than the stock", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        public void StockIn_calc()
        {
            try
            {
                DataTable dt_unit1 = this.cntrl.get_item_unitmf(ItemCode);
                TotalQty = Quantity;
                if (dt_unit1.Rows[0]["OneUnitOnly"].ToString() == "False")
                {
                    decimal unitmf = Convert.ToDecimal(dt_unit1.Rows[0]["UnitMF"].ToString());
                    if (dt_unit1.Rows[0]["Unit1"].ToString() == Unit)
                    {
                        TotalQty = Quantity * unitmf;
                        for (int i = 0; i < dgv_batchSale.Rows.Count; i++)
                        {
                            dgv_batchSale.Rows[i].Cells["clUnit"].Value = Unit;
                            if(i==0)
                            {
                                dgv_batchSale.Rows[i].Cells["ColQty"].Value = TotalQty.ToString();
                            }
                            else
                            dgv_batchSale.Rows[i].Cells["ColQty"].Value = 0;

                            curent_Stock = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value.ToString()) + Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColQty"].Value.ToString());
                            if(i==0)
                            {
                                dgv_batchSale.Rows[i].Cells["colCurrentStock"].Value = curent_Stock.ToString();
                            }
                            else
                            dgv_batchSale.Rows[i].Cells["colCurrentStock"].Value = 0;
                            //break;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dgv_batchSale.Rows.Count; i++)
                        {
                            dgv_batchSale.Rows[i].Cells["clUnit"].Value = Unit;
                            if (i == 0)
                            {
                                dgv_batchSale.Rows[i].Cells["ColQty"].Value = TotalQty.ToString();
                            }
                            else
                                dgv_batchSale.Rows[i].Cells["ColQty"].Value = 0;
                            curent_Stock = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value.ToString()) + Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColQty"].Value.ToString());
                            if (i == 0)
                            {
                                dgv_batchSale.Rows[i].Cells["colCurrentStock"].Value = curent_Stock.ToString();
                            }
                            else
                                dgv_batchSale.Rows[i].Cells["colCurrentStock"].Value = 0;
                            //break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dgv_batchSale.Rows.Count; i++)
                    {

                        dgv_batchSale.Rows[i].Cells["clUnit"].Value = Unit;
                        if (i == 0)
                        {
                            dgv_batchSale.Rows[i].Cells["ColQty"].Value = TotalQty.ToString();
                        }
                        else
                            dgv_batchSale.Rows[i].Cells["ColQty"].Value = 0;
                        curent_Stock = Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColStock"].Value.ToString()) + Convert.ToDecimal(dgv_batchSale.Rows[i].Cells["ColQty"].Value.ToString());
                        if (i == 0)
                        {
                            dgv_batchSale.Rows[i].Cells["colCurrentStock"].Value = curent_Stock.ToString();
                        }
                        else
                            dgv_batchSale.Rows[i].Cells["colCurrentStock"].Value = 0;
                        //break;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static int batchentry = 0;
        private void btn_OK_Click(object sender, EventArgs e)
        {
            try
            {
                decimal qty = 0;
                dtb_Sales.Columns.Clear();
                dtb_Sales.Rows.Clear(); rate_calculation();
                foreach (DataGridViewRow dr in dgv_batchSale.Rows)
                {
                    if (dr.Cells["ColQty"].Value != null && dr.Cells["ColQty"].Value.ToString() != "")
                    {
                        qty = qty + Convert.ToDecimal(dr.Cells["ColQty"].Value.ToString());
                    }
                    else
                    {
                        dr.Cells["colCurrentStock"].Value = dr.Cells["ColStock"].Value.ToString();
                    }
                }
                if (qty == TotalQty)
                {
                    foreach (DataGridViewColumn col in dgv_batchSale.Columns)
                    {
                        dtb_Sales.Columns.Add(col.Name);
                    }
                    foreach (DataGridViewRow row in dgv_batchSale.Rows)
                    {
                        DataRow dRow = dtb_Sales.NewRow();
                        foreach (DataGridViewCell cell in row.Cells)
                        {

                            dRow[cell.ColumnIndex] = cell.Value;
                        }
                        dtb_Sales.Rows.Add(dRow);
                    }
                }
                else
                {
                    MessageBox.Show("Please make sure the quanties are equall ", "Not Equall", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                    if (Action == "Stock Out")
                    {
                        if (edit_flag == true)
                        {
                            if (dtb_Sales.Rows.Count > 0)
                            {
                                var form2 = new stock_transfer(dtb_Sales,"", total_rate, batchentry);
                                form2.Closed += (sender1, args) => this.Close();
                                edit_flag = false;
                                this.Close();
                            }
                        }
                        else
                        {
                            if (dtb_Sales.Rows.Count > 0)
                            {
                                string str = "Stock Out"; //var form2 = new Sales(dtb_Sales, total_rate, batchentry);
                            var form2 = new stock_transfer(dtb_Sales, str, total_rate, batchentry);
                                form2.Closed += (sender1, args) => this.Close();
                                this.Close();
                            }
                        }
                    }
                    else if (Action == "Stock In")
                    {
                        if (edit_flag == true)
                        {
                            if (dtb_Sales.Rows.Count > 0)
                            {
                                var form2 = new stock_transfer(dtb_Sales);
                                form2.Closed += (sender1, args) => this.Close();
                                edit_flag = false;
                                this.Close();
                            }
                        }
                        else
                        {
                            if (dtb_Sales.Rows.Count > 0)
                            {
                                string str = "Stock In";
                                var form2 = new stock_transfer(dtb_Sales, str);
                                form2.Closed += (sender1, args) => this.Close();
                                this.Close();
                            }
                        }

                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_batchSale_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dgv_batchSale.CurrentCell.OwningColumn.Name == "ColQty")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void dgv_batchSale_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //decimal curent_Stock = 0, prev_stock = 0;
                //if (dgv_batchSale.CurrentCell.OwningColumn.Name == "ColQty")
                //{
                //    if (dgv_batchSale.CurrentRow.Cells["ColQty"].Value != null && dgv_batchSale.CurrentRow.Cells["ColQty"].Value.ToString() != "")
                //    {
                //        curent_Stock = Convert.ToDecimal(dgv_batchSale.CurrentRow.Cells["ColStock"].Value.ToString()) - Convert.ToDecimal(dgv_batchSale.CurrentRow.Cells["ColQty"].Value.ToString());
                //        dgv_batchSale.CurrentRow.Cells["colCurrentStock"].Value = curent_Stock.ToString();
                //        //dgv_batchSale.CurrentRow.Cells["clUnit"].Value = Unit;
                //    }
                //    else
                //    {
                //        //prev_stock = Convert.ToDecimal(dgv_batchSale.CurrentRow.Cells["colCurrentStock"].Value) + Convert.ToDecimal(dgv_batchSale.CurrentRow.Cells["ColStock"].Value);
                //        //dgv_batchSale.CurrentRow.Cells["ColStock"].Value = prev_stock.ToString();
                //        dgv_batchSale.CurrentRow.Cells["colCurrentStock"].Value = "";
                //        //dgv_batchSale.CurrentRow.Cells["clUnit"].Value = "";
                //    }
                //}
                if (e.RowIndex >= 0)
                {
                    decimal curent_Stock = 0, prev_stock = 0; int k = 0;
                    if (dgv_batchSale.CurrentCell.OwningColumn.Name == "ColQty")
                    {
                        if (dgv_batchSale.CurrentRow.Cells["ColQty"].Value != null && dgv_batchSale.CurrentRow.Cells["ColQty"].Value.ToString() != "")
                        {
                            if (Convert.ToDecimal(dgv_batchSale.CurrentRow.Cells["ColStock"].Value.ToString()) > Convert.ToDecimal(dgv_batchSale.CurrentRow.Cells["ColQty"].Value.ToString()))
                            {
                                curent_Stock = Convert.ToDecimal(dgv_batchSale.CurrentRow.Cells["ColStock"].Value.ToString()) - Convert.ToDecimal(dgv_batchSale.CurrentRow.Cells["ColQty"].Value.ToString());
                                dgv_batchSale.CurrentRow.Cells["colCurrentStock"].Value = curent_Stock.ToString();
                                k = dgv_batchSale.CurrentRow.Index;
                                for (int i = 0; i < dgv_batchSale.Rows.Count; i++)
                                {
                                    if (i != k)
                                    {
                                        dgv_batchSale.Rows[i].Cells["ColQty"].Value = "0";
                                        dgv_batchSale.Rows[i].Cells["colCurrentStock"].Value = "";
                                    }
                                }

                            }
                            else
                            {
                                dgv_batchSale.CurrentRow.Cells["ColQty"].Value = "0";
                                MessageBox.Show("Quantity is greater than the stock", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            //dgv_batchSale.CurrentRow.Cells["clUnit"].Value = Unit;
                        }
                        else
                        {
                            //prev_stock = Convert.ToDecimal(dgv_batchSale.CurrentRow.Cells["colCurrentStock"].Value) + Convert.ToDecimal(dgv_batchSale.CurrentRow.Cells["ColStock"].Value);
                            //dgv_batchSale.CurrentRow.Cells["ColStock"].Value = prev_stock.ToString();
                            dgv_batchSale.CurrentRow.Cells["colCurrentStock"].Value = "";
                            //dgv_batchSale.CurrentRow.Cells["clUnit"].Value = "";
                        }
                    }
                }
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

        private void dgv_batchSale_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
          
                if(Action=="Stock In")
                {
                    if (dgv_batchSale.CurrentCell.OwningColumn.Name == "ColQty")
                    {
                        decimal curent_Stock = 0;
                        int index = dgv_batchSale.CurrentRow.Index;
                        string qty = dgv_batchSale.CurrentRow.Cells["ColQty"].Value.ToString();
                        curent_Stock = Convert.ToDecimal(dgv_batchSale.CurrentRow.Cells["ColStock"].Value.ToString()) + Convert.ToDecimal(dgv_batchSale.CurrentRow.Cells["ColQty"].Value.ToString());
                        dgv_batchSale.CurrentRow.Cells["colCurrentStock"].Value = curent_Stock.ToString();
                        curent_Stock = 0;
                    }
                }
        }
    }
}
