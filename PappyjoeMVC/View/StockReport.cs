using PappyjoeMVC.Controller;

using PappyjoeMVC.Model;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Threading; 
namespace PappyjoeMVC.View

{
    public partial class StockReport : Form
    {
        Purchase_order_controller cntrl1 = new Purchase_order_controller();
        Connection db = new Connection();
        public static int j = 0;
        DataTable dtb=new DataTable();
        string combo_topmargin = "";
        string combo_leftmargin = "";
        string combo_bottommargin = "";
        string combo_rightmargin = "";
        string combo_paper_size = "";
        string combo_footer_topmargin = "";
        string rich_fullwidth = "";
        string rich_leftsign = "";
        string rich_rightsign = "";
        string patient_details = "";
        string med = "";
        string patient = "";
        string address = "";
        string phone = "";
        string blood = "";
        string gender = "";
        string logo_name = "";
        string orientation = "", path = "";
        string includeheader = "0";
        string includelogo = "0";
        System.Drawing.Image logo = null;
        stock_controller cntrl = new stock_controller();
        Printout_controller prnt_ctrl = new Printout_controller();
        public string doctor_id = "";
        Purchase purchase = new Purchase();
        Sales sales = new Sales();
        SalesOrder sales_order = new SalesOrder();
        PurchaseOrder pur_order = new PurchaseOrder();
        Purchase_Return pur_return = new Purchase_Return();
        Sales_Return salesreturn = new Sales_Return();
        public bool Pur_orderFlag = false, sales_orderFlag = false, Pur_ListFlag = false, sales_ListFlag = false, Pur_retnFlag = false;
        PurchaseList purchase_LIst = new PurchaseList();
        Purchase_order_list PU_orderList = new Purchase_order_list();
        Sales_List salesList = new Sales_List();
        Sales_order_list s_orderList = new Sales_order_list();
        Sales_ReturnList S_ReturnList = new Sales_ReturnList();
        Purchase_Return_List Pu_returnList = new Purchase_Return_List();
        stock_transfer transfer = new stock_transfer();
        Stock_TransferList transList = new Stock_TransferList();
        Stock_Adjustment stck_update = new Stock_Adjustment();
        Account_Statement uplist = new Account_Statement();
        Stock_Ledger stk_ledger = new Stock_Ledger();
        public static string type = ""; public bool load_flag = false;
        public StockReport()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void btn_purchase_Click(object sender, EventArgs e)
        {
            sales_collaps.Visible = false;
            Purchase_menu.Visible = false;
            stock_transfer_menu.Visible = false;
            backColor_Change();
            btn_purchase.BackColor = Color.SteelBlue;
            FormHide();
            panel_main.Show();
            purchase.Close();
            if (purchase == null || purchase.IsDisposed)
                purchase = new Purchase();
            purchase.TopLevel = false;
            purchase.doctor_id = doctor_id;
            panel_main.Controls.Add(purchase);
            purchase.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            purchase.Show();
        }
        public void backColor_Change()
        {
            btn_Stock.BackColor = Color.DodgerBlue;
            btn_purchase.BackColor = Color.DodgerBlue;
            btnSales.BackColor = Color.DodgerBlue;
            btn_SalesOrder.BackColor = Color.DodgerBlue;
            btn_stocktransfer.BackColor = Color.DodgerBlue;
            btn_stock_updation.BackColor = Color.DodgerBlue;
            btn_updatiobnlist.BackColor = Color.DodgerBlue;
            btn_Stockledger.BackColor = Color.DodgerBlue;
            btn_Manufacture.BackColor = Color.DodgerBlue;
            btnSuplier.BackColor = Color.DodgerBlue; button3.BackColor = Color.DodgerBlue;
            btn_ItemList.BackColor = Color.DodgerBlue;
            btn_SalesOrder.BackColor = Color.DodgerBlue;
        }
        public void FormHide()
        {
            salesList.Hide();
            purchase_LIst.Hide();
            Pu_returnList.Hide();
            s_orderList.Hide();
            PU_orderList.Hide();
            pur_order.Hide();
            pur_return.Hide();
            S_ReturnList.Hide();
            purchase.Hide();
            sales_order.Hide();
            Pur_ListFlag = false;
            Pur_retnFlag = false;
            sales_ListFlag = false;
            Pur_ListFlag = false;
            sales_ListFlag = false;
            Pur_orderFlag = false;
            sales_orderFlag = false;
            salesreturn.Hide();
            sales.Hide();
            transfer.Hide();
            transList.Hide();
            stck_update.Hide();
            uplist.Hide();
            stk_ledger.Hide();
            manufacture.Hide();
            suplier.Hide(); consume.Hide(); item_list.Hide();
            drug.Hide();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            sales_collaps.Visible = false;
            Purchase_menu.Visible = false;
            stock_transfer_menu.Visible = false;
            backColor_Change();
            btnSales.BackColor = Color.SteelBlue;
            FormHide();
            panel_main.Show();
            sales.Close();
            if (sales == null || sales.IsDisposed)
                sales = new Sales();
            sales.TopLevel = false;
            sales.doctor_id = doctor_id;
            panel_main.Controls.Add(sales);
            sales.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            sales.Show();
        }
        Add_Drug drug = new Add_Drug();
        private void btn_SalesOrder_Click(object sender, EventArgs e)
        {
            sales_collaps.Visible = false;
            Purchase_menu.Visible = false;
            stock_transfer_menu.Visible = false;
            backColor_Change();
            btn_SalesOrder.BackColor = Color.SteelBlue;
            panel_main.Show();
            var form2 = new Add_Drug();
            form2.ShowDialog();
            form2.Dispose();
        }
        private void frmstockReport_Load(object sender, EventArgs e)
        {
            load_flag = true;
               j = 0;
            panel_main.Hide(); Lab_Msg.Visible = false;
            btn_Stock.BackColor = Color.SteelBlue;
            System.Data.DataTable clinicname = this.cntrl.Get_CompanyNAme();
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
            if (clinicname.Rows.Count > 0)
            {
                string clinicn = "";
                clinicn = clinicname.Rows[0][0].ToString();
                path = clinicname.Rows[0]["path"].ToString();
                string docnam = this.cntrl.Get_DoctorName(doctor_id);
            }
            try
            {
                if (path != "")
                {
                    string curFile = this.cntrl.server() + "\\Pappyjoe_utilities\\Logo\\" + path;

                    if (File.Exists(curFile))
                    {
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
            }
            if (doctor_id != "1")
            {
                string id;
                id = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='Sales' and Permission='A'");
                if (int.Parse(id) > 0)
                {
                    btnSales.Enabled = true;
                    btn_SalesOrder.Enabled = true;
                }
                else
                {
                    btnSales.Enabled = false;
                    btn_SalesOrder.Enabled = false;
                }
                string id1;
                id1 = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='Purchase' and Permission='A'");
                if (int.Parse(id1) > 0)
                {
                    btn_purchase.Enabled = true;
                }
                else
                {
                    btn_purchase.Enabled = false;
                }
                string id11;
                id11 = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='StockAdjustment' and Permission='A'");
                if (int.Parse(id11) > 0)
                {
                    btn_stock_updation.Enabled = true;
                }
                else
                {
                    btn_stock_updation.Enabled = false;
                }
                string id111;
                id111 = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='StockTransfer' and Permission='A'");
                if (int.Parse(id111) > 0)
                {
                    btn_stocktransfer.Enabled = true;
                }
                else
                {
                    btn_stocktransfer.Enabled = false;
                }
                string id1111;
                id1111 = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='StockLedger' and Permission='S'");
                if (int.Parse(id1111) > 0)
                {
                    btn_Stockledger.Enabled = true;
                }
                else
                {
                    btn_Stockledger.Enabled = false;
                }
            }
            else
            { 
                btnSales.Enabled = true;
                btn_SalesOrder.Enabled = true;
                btn_stocktransfer.Enabled = true;
                btn_stock_updation.Enabled = true; btn_Stockledger.Enabled = true;
            }
            DataTable dt_supplier = this.cntrl.LoadSupplier();
            Load_Supplier(dt_supplier);
            DataTable dt_load = this.cntrl.load_stock(type);
            load_stock(dt_load);
            DGV_Stock.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            DGV_Stock.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DGV_Stock.EnableHeadersVisualStyles = false;
            DGV_Stock.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 9, FontStyle.Regular);
            DGV_Stock.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DGV_Stock.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DGV_Stock.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DGV_Stock.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            foreach (DataGridViewColumn cl in DGV_Stock.Columns)
            {
                cl.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            DataTable dt_count = this.cntrl.dt_get_count(type);
            if (dt_count.Rows.Count >25)
            {
                label1.Visible = true;
            }
            else
                label1.Visible = false;
            load_flag = false;
        }

        public void Load_Supplier(DataTable gp_rs)
        {
            if (gp_rs.Rows.Count > 0)
            {
                Cmb_Suplier.Items.Clear();

                foreach (DataRow dr in gp_rs.Rows)
                {
                    Cmb_Suplier.Items.Add(dr["Supplier_Name"].ToString());
                    Cmb_Suplier.ValueMember = dr["Supplier_Code"].ToString();
                    Cmb_Suplier.DisplayMember = dr["Supplier_Name"].ToString();
                }
                Cmb_Suplier.SelectedIndex = 0;
            }
        }
        public void load_stock(DataTable dtb)
        {
            try
            {
                Lab_Msg.Visible = false;
                    string current_Stock = "", WSP = "", RSP = ""; int unitmf = 0, qty = 0, quotient = 0, Remainder = 0;
                decimal value = 0,  total_wsp = 0, total_rsp = 0, total_stock = 0, sales_rate = 0;
                int num = 1;
                DGV_Stock.Rows.Clear();
                if (dtb.Rows.Count > 0)
                {
                    for (int i = 0; i < dtb.Rows.Count; i++)  
                    {
                        decimal stock = 0;
                        DataTable dtb_Min = this.cntrl.minimumStock(dtb.Rows[i]["id"].ToString());
                        DataTable dtunit = this.cntrl.itemdetails(dtb.Rows[i]["id"].ToString());//
                        DataTable dt_batch_wise_rate = this.cntrl.get_batch_rate(dtb.Rows[i]["id"].ToString());
                        DataTable dt_batch_sale_rate = this.cntrl.get_batch_sale_rate(dtb.Rows[i]["id"].ToString());
                        bool mini_flag = false;
                        if (dtunit.Rows.Count > 0)
                        {
                            decimal item_rate1 = 0, sales_rate1 = 0,bat_unt1_rate=0,sales_unit1_rate=0;
                            decimal item_rate2 = 0, sales_rate2 = 0, gst = 0,pur_wsp=0,sal_rsp=0, bat_wsp = 0, bat_rsp = 0,value1=0,value2=0;
                            int bat_qty = 0;
                            gst = Convert.ToDecimal(dtunit.Rows[0]["GstVat"].ToString());
                            if (dtunit.Rows[0]["OneUnitOnly"].ToString() == "False")
                            {
                                unitmf = Convert.ToInt32(dtunit.Rows[0]["UnitMF"].ToString());
                                value = Convert.ToDecimal(dtb.Rows[i]["qty"].ToString());
                                qty = Convert.ToInt32(value);
                                quotient = Convert.ToInt32(qty / unitmf);
                                Remainder = Convert.ToInt32(qty % unitmf);
                                ///
                                if(dt_batch_wise_rate.Rows.Count>0)
                                {           
                                    foreach (DataRow dr in dt_batch_wise_rate.Rows)
                                    {
                                        value1 = Convert.ToDecimal(dr["Qty"].ToString());
                                        if (Convert.ToDecimal(dr["Qty"].ToString()) <= Convert.ToDecimal(dtb_Min.Rows[0][0].ToString()))
                                        {
                                            mini_flag = true;
                                        }
                                        else
                                        {
                                            mini_flag = false;
                                        }
                                        if (dr["purch_unit2"].ToString() == "No")
                                        {
                                            if (dr["Unit2"].ToString() == "No")
                                            {
                                                bat_unt1_rate = Convert.ToDecimal(dr["batch_rate"].ToString())/ unitmf;
                                                sales_unit1_rate = Convert.ToDecimal(dr["batch_sales_rate"].ToString()) / unitmf;
                                            }
                                            else
                                            {
                                                bat_unt1_rate = Convert.ToDecimal(dr["batch_rate"].ToString()) / unitmf;
                                                sales_unit1_rate = Convert.ToDecimal(dr["batch_sales_rate"].ToString()) / unitmf;
                                            }

                                        }
                                        else
                                        {
                                            if (dr["Unit2"].ToString() == "No")
                                            {
                                                bat_unt1_rate = Convert.ToDecimal(dr["batch_rate"].ToString()) ;
                                                sales_unit1_rate = Convert.ToDecimal(dr["batch_sales_rate"].ToString()) ;
                                            }
                                            else
                                            {
                                                bat_unt1_rate = Convert.ToDecimal(dr["batch_rate"].ToString());
                                                sales_unit1_rate = Convert.ToDecimal(dr["batch_sales_rate"].ToString());
                                            }
                                        }
                                         bat_qty = Convert.ToInt32(value1);
                                        item_rate1 = bat_qty * bat_unt1_rate;
                                        bat_wsp = ((item_rate1 * gst / 100) + item_rate1);
                                        pur_wsp = pur_wsp + bat_wsp;
                                        sales_rate1 = bat_qty * sales_unit1_rate;
                                        bat_rsp = ((sales_rate1 * gst / 100) + sales_rate1);
                                        sal_rsp = sal_rsp + bat_rsp;
                                    }       
                                }
                             
                                WSP = pur_wsp.ToString("0.##");
                                RSP= sal_rsp.ToString("0.##");
                                if (dtb_Min.Rows.Count > 0)
                                {
                                    if (mini_flag==true)
                                    {
                                        current_Stock = "qty (" + dtunit.Rows[0]["Unit1"].ToString() + ")" + "=" + " " + quotient + " " + "," + " " + dtunit.Rows[0]["Unit2"].ToString() + " " + "=" + Remainder;//+ " Value:" + (item_rate1 + item_rate2).ToString("0.##")
                                        DGV_Stock.Rows.Add(dtb.Rows[i]["id"].ToString(), num, dtb.Rows[i]["item_code"].ToString(), dtunit.Rows[0]["item_name"].ToString(), dtunit.Rows[0]["Unit1"].ToString(), current_Stock, WSP, RSP, dtunit.Rows[0]["Shelf_No"].ToString());
                                        int s = DGV_Stock.Rows.Count;
                                        DGV_Stock.Rows[s - 1].DefaultCellStyle.ForeColor = Color.Red;
                                    }
                                    else
                                    {
                                        current_Stock = "qty (" + dtunit.Rows[0]["Unit1"].ToString() + ")" + "=" + " " + quotient + " " + "," + " " + dtunit.Rows[0]["Unit2"].ToString() + " " + "=" + Remainder;
                                        DGV_Stock.Rows.Add(dtb.Rows[i]["id"].ToString(), num, dtb.Rows[i]["item_code"].ToString(), dtunit.Rows[0]["item_name"].ToString(), dtunit.Rows[0]["Unit1"].ToString(), current_Stock, WSP, RSP, dtunit.Rows[0]["Shelf_No"].ToString());
                                    }
                                }

                            }
                            else
                            {
                                decimal tot_qty = 0;
                                if (dt_batch_wise_rate.Rows.Count > 0)
                                {
                                   
                                    foreach (DataRow dr in dt_batch_wise_rate.Rows)
                                    {
                                        tot_qty = tot_qty+ Convert.ToDecimal(dr["Qty"].ToString());
                                           stock = Convert.ToDecimal(dr["Qty"].ToString());
                                        if (Convert.ToDecimal(dr["Qty"].ToString()) <= Convert.ToDecimal(dtb_Min.Rows[0][0].ToString()))
                                        {
                                            mini_flag = true;
                                        }
                                        else
                                        {
                                            mini_flag = false;
                                        }
                                            bat_unt1_rate = Convert.ToDecimal(dr["batch_rate"].ToString());
                                        sales_unit1_rate = Convert.ToDecimal(dr["batch_sales_rate"].ToString());
                                        item_rate2 = stock * bat_unt1_rate;
                                        item_rate1 = (item_rate2 * gst / 100) + item_rate2;
                                        pur_wsp = pur_wsp + item_rate1;
                                        sales_rate2 = (stock * sales_unit1_rate);
                                        sales_rate1 = (sales_rate2 * gst / 100) + sales_rate2;
                                        sal_rsp = sal_rsp + sales_rate1;
                                    }
                                }
                                if (dtunit.Rows[0]["Unit1"].ToString() == null || dtunit.Rows[0]["Unit1"].ToString() == "")
                                    {
                                        current_Stock = Math.Floor(tot_qty).ToString("0.##");// + "," + " Value:" + item_rate1.ToString("0.##");
                                        DGV_Stock.Rows.Add(dtb.Rows[i]["id"].ToString(), num, dtb.Rows[i]["item_code"].ToString(), dtunit.Rows[0]["item_name"].ToString(), dtunit.Rows[0]["Unit1"].ToString(), current_Stock, pur_wsp.ToString("0.##"), sal_rsp.ToString("0.##"), dtunit.Rows[0]["Shelf_No"].ToString());
                                }
                                    else
                                    {
                                        current_Stock = "qty (" + dtunit.Rows[0]["Unit1"].ToString() + ") " + "=" + " " + Math.Floor(tot_qty).ToString("0.##");
                                        DGV_Stock.Rows.Add(dtb.Rows[i]["id"].ToString(), num, dtb.Rows[i]["item_code"].ToString(), dtunit.Rows[0]["item_name"].ToString(), dtunit.Rows[0]["Unit1"].ToString(), current_Stock, pur_wsp.ToString("0.##"), sal_rsp.ToString("0.##"), dtunit.Rows[0]["Shelf_No"].ToString());
                                    }
                                int s = DGV_Stock.Rows.Count;
                                if (mini_flag == true)
                                {
                                    DGV_Stock.Rows[s - 1].DefaultCellStyle.ForeColor = Color.Red;

                                }
                            }

                            total_wsp = total_wsp + Convert.ToDecimal(DGV_Stock.Rows[i].Cells["WSP"].Value.ToString());
                            total_rsp = total_rsp + Convert.ToDecimal(DGV_Stock.Rows[i].Cells["RSP"].Value.ToString());
                        }
                        num = num + 1;
                    }
                    ls_amount.Text = total_wsp.ToString();
                    ls_gst.Text = total_rsp.ToString();
                }
                else
                {
                    Lab_Msg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      
        public void load_stock_showmore(DataTable dtb)
        {
            try
            {
                string current_Stock = "", WSP = "", RSP = ""; int unitmf = 0, qty = 0, quotient = 0, Remainder = 0;
                decimal value = 0, stock = 0, total_wsp = 0, total_rsp = 0, total_stock = 0;
                if (dtb.Rows.Count > 0)
                {
                    int row = DGV_Stock.Rows.Count ;
                    int i = row;
                    int num =Convert.ToInt32( DGV_Stock.Rows[row-1].Cells["SLNO"].Value)+1;
                    foreach (DataRow dr in dtb.Rows)
                    {
                        DataTable dtb_Min = this.cntrl.minimumStock(dr["id"].ToString());
                        DataTable dtunit = this.cntrl.itemdetails(dr["id"].ToString());
                        if (dtunit.Rows.Count > 0)
                        {
                            decimal item_rate1 = 0, sales_rate1 = 0;
                            decimal item_rate2 = 0, sales_rate2 = 0, gst = 0;
                            gst = Convert.ToDecimal(dtunit.Rows[0]["GstVat"].ToString());
                            if (dtunit.Rows[0]["OneUnitOnly"].ToString() == "False")
                            {
                                unitmf = Convert.ToInt32(dtunit.Rows[0]["UnitMF"].ToString());
                                value = Convert.ToDecimal(dr["qty"].ToString());
                                qty = Convert.ToInt32(value);
                                quotient = Convert.ToInt32(qty / unitmf);
                                Remainder = Convert.ToInt32(qty % unitmf);
                                item_rate1 = quotient * Convert.ToDecimal(dtunit.Rows[0]["purch_rate"].ToString());
                                WSP = ((item_rate1 * gst / 100) + item_rate1).ToString("0.##");
                                sales_rate1 = quotient * Convert.ToDecimal(dtunit.Rows[0]["Sales_Rate_Max"].ToString());
                                RSP = ((sales_rate1 * gst / 100) + sales_rate1).ToString("0.##");
                                if (dtb_Min.Rows.Count > 0)
                                {
                                    if (quotient <= Convert.ToDecimal(dtb_Min.Rows[0][0].ToString()))
                                    {
                                        current_Stock = "qty (" + dtunit.Rows[0]["Unit1"].ToString() + ")" + "=" + " " + quotient + " " + "," + " " + dtunit.Rows[0]["Unit2"].ToString() + " " + "=" + Remainder;
                                        DGV_Stock.Rows.Add();
                                        DGV_Stock.Rows[row].Cells["id"].Value = dr["id"].ToString();
                                        DGV_Stock.Rows[row].Cells["SLNO"].Value = num;
                                        DGV_Stock.Rows[row].Cells["item_code"].Value = dr["item_code"].ToString();
                                        DGV_Stock.Rows[row].Cells["item_name"].Value = dtunit.Rows[0]["item_name"].ToString();
                                        DGV_Stock.Rows[row].Cells["clunit"].Value = dtunit.Rows[0]["Unit1"].ToString();
                                        DGV_Stock.Rows[row].Cells["qty"].Value = current_Stock;
                                        DGV_Stock.Rows[row].Cells["WSP"].Value = WSP;
                                        DGV_Stock.Rows[row].Cells["RSP"].Value = RSP; 
                                        DGV_Stock.Rows[row].Cells["shelfno"].Value = dtunit.Rows[0]["Shelf_No"].ToString();
                                        i++;
                                    }
                                    else
                                    {
                                        current_Stock = "qty (" + dtunit.Rows[0]["Unit1"].ToString() + ")" + "=" + " " + quotient + " " + "," + " " + dtunit.Rows[0]["Unit2"].ToString() + " " + "=" + Remainder;
                                        DGV_Stock.Rows.Add();
                                        DGV_Stock.Rows[row].Cells["id"].Value = dr["id"].ToString();
                                        DGV_Stock.Rows[row].Cells["SLNO"].Value = num;
                                        DGV_Stock.Rows[row].Cells["item_code"].Value = dr["item_code"].ToString();
                                        DGV_Stock.Rows[row].Cells["item_name"].Value = dtunit.Rows[0]["item_name"].ToString();
                                        DGV_Stock.Rows[row].Cells["clunit"].Value = dtunit.Rows[0]["Unit1"].ToString();
                                        DGV_Stock.Rows[row].Cells["qty"].Value = current_Stock;
                                        DGV_Stock.Rows[row].Cells["WSP"].Value = WSP;
                                        DGV_Stock.Rows[row].Cells["RSP"].Value = RSP;
                                        DGV_Stock.Rows[row].Cells["shelfno"].Value = dtunit.Rows[0]["Shelf_No"].ToString();
                                        i++;
                                    }
                                }
                            }
                            else
                            {
                                if (Convert.ToDecimal(dr["qty"].ToString()) <= Convert.ToDecimal(dtb_Min.Rows[0][0].ToString()))
                                {
                                    stock = Convert.ToDecimal(dr["qty"].ToString());
                                    item_rate2 = stock * Convert.ToDecimal(dtunit.Rows[0]["purch_rate"].ToString());
                                    item_rate1 = (item_rate2 * gst / 100) + item_rate2;
                                    sales_rate2 = (stock * Convert.ToDecimal(dtunit.Rows[0]["Sales_Rate_Max"].ToString()));
                                    sales_rate1 = (sales_rate2 * gst / 100) + sales_rate2;
                                    if (dtunit.Rows[0]["Unit1"].ToString() == null || dtunit.Rows[0]["Unit1"].ToString() == "")
                                    {
                                        current_Stock = Math.Floor(stock).ToString("0.##");
                                        DGV_Stock.Rows.Add();
                                        DGV_Stock.Rows[row].Cells["id"].Value = dr["id"].ToString();
                                        DGV_Stock.Rows[row].Cells["SLNO"].Value = num;
                                        DGV_Stock.Rows[row].Cells["item_code"].Value = dr["item_code"].ToString();
                                        DGV_Stock.Rows[row].Cells["item_name"].Value = dtunit.Rows[0]["item_name"].ToString();
                                        DGV_Stock.Rows[row].Cells["clunit"].Value = dtunit.Rows[0]["Unit1"].ToString();
                                        DGV_Stock.Rows[row].Cells["qty"].Value = current_Stock;
                                        DGV_Stock.Rows[row].Cells["WSP"].Value = item_rate1.ToString("0.##");
                                        DGV_Stock.Rows[row].Cells["RSP"].Value = sales_rate1.ToString("0.##");
                                        DGV_Stock.Rows[row].Cells["shelfno"].Value = dtunit.Rows[0]["Shelf_No"].ToString();
                                        int s = DGV_Stock.Rows.Count;
                                        DGV_Stock.Rows[s - 1].DefaultCellStyle.ForeColor = Color.Red;
                                        i++;
                                    }
                                    else
                                    {
                                        current_Stock = "qty (" + dtunit.Rows[0]["Unit1"].ToString() + ") " + "=" + " " + Math.Floor(stock).ToString("0.##");
                                        DGV_Stock.Rows.Add();
                                        DGV_Stock.Rows[row].Cells["id"].Value = dr["id"].ToString();
                                        DGV_Stock.Rows[row].Cells["SLNO"].Value = num;
                                        DGV_Stock.Rows[row].Cells["item_code"].Value = dr["item_code"].ToString();
                                        DGV_Stock.Rows[row].Cells["item_name"].Value = dtunit.Rows[0]["item_name"].ToString();
                                        DGV_Stock.Rows[row].Cells["clunit"].Value = dtunit.Rows[0]["Unit1"].ToString();
                                        DGV_Stock.Rows[row].Cells["qty"].Value = current_Stock;
                                        DGV_Stock.Rows[row].Cells["WSP"].Value = item_rate1.ToString("0.##");
                                        DGV_Stock.Rows[row].Cells["RSP"].Value = sales_rate1.ToString("0.##");
                                        DGV_Stock.Rows[row].Cells["shelfno"].Value = dtunit.Rows[0]["Shelf_No"].ToString();
                                        int s = DGV_Stock.Rows.Count;
                                        DGV_Stock.Rows[s - 1].DefaultCellStyle.ForeColor = Color.Red;
                                        i++;
                                    }
                                }
                                else
                                {
                                    stock = Convert.ToDecimal(dr["qty"].ToString());
                                    item_rate2 = stock * Convert.ToDecimal(dtunit.Rows[0]["purch_rate"].ToString());
                                    sales_rate2 = (stock * Convert.ToDecimal(dtunit.Rows[0]["Sales_Rate_Max"].ToString()));
                                    item_rate1 = (item_rate2 * gst / 100) + item_rate2; ;
                                    sales_rate1 = (sales_rate2 * gst / 100) + sales_rate2; ;
                                    if (dtunit.Rows[0]["Unit1"].ToString() == null || dtunit.Rows[0]["Unit1"].ToString() == "")
                                    {
                                        current_Stock = Math.Floor(stock).ToString("0.##");
                                        DGV_Stock.Rows.Add();
                                        DGV_Stock.Rows[row].Cells["id"].Value = dr["id"].ToString();
                                        DGV_Stock.Rows[row].Cells["SLNO"].Value = num;
                                        DGV_Stock.Rows[row].Cells["item_code"].Value = dr["item_code"].ToString();
                                        DGV_Stock.Rows[row].Cells["item_name"].Value = dtunit.Rows[0]["item_name"].ToString();
                                        DGV_Stock.Rows[row].Cells["clunit"].Value = dtunit.Rows[0]["Unit1"].ToString();
                                        DGV_Stock.Rows[row].Cells["qty"].Value = current_Stock;
                                        DGV_Stock.Rows[row].Cells["WSP"].Value = item_rate1.ToString("0.##");
                                        DGV_Stock.Rows[row].Cells["RSP"].Value = sales_rate1.ToString("0.##");
                                        DGV_Stock.Rows[row].Cells["shelfno"].Value = dtunit.Rows[0]["Shelf_No"].ToString();
                                        i++;
                                    }
                                    else
                                    {
                                        current_Stock = "qty (" + dtunit.Rows[0]["Unit1"].ToString() + ")" + "=" + " " + Math.Floor(stock);
                                        DGV_Stock.Rows.Add();
                                        DGV_Stock.Rows[row].Cells["id"].Value = dr["id"].ToString();
                                        DGV_Stock.Rows[row].Cells["SLNO"].Value = num;
                                        DGV_Stock.Rows[row].Cells["item_code"].Value = dr["item_code"].ToString();
                                        DGV_Stock.Rows[row].Cells["item_name"].Value = dtunit.Rows[0]["item_name"].ToString();
                                        DGV_Stock.Rows[row].Cells["clunit"].Value = dtunit.Rows[0]["Unit1"].ToString();
                                        DGV_Stock.Rows[row].Cells["qty"].Value = current_Stock;
                                        DGV_Stock.Rows[row].Cells["WSP"].Value = item_rate1.ToString("0.##");
                                        DGV_Stock.Rows[row].Cells["RSP"].Value = sales_rate1.ToString("0.##");
                                        DGV_Stock.Rows[row].Cells["shelfno"].Value = dtunit.Rows[0]["Shelf_No"].ToString();
                                        i++;
                                    }
                                }
                            }
                            total_wsp = total_wsp + Convert.ToDecimal(DGV_Stock.Rows[row].Cells["WSP"].Value.ToString());
                            total_rsp = total_rsp + Convert.ToDecimal(DGV_Stock.Rows[row].Cells["RSP"].Value.ToString());
                            row = row + 1;
                        }
                        num = num + 1;
                    }
                    ls_amount.Text =Convert.ToDecimal( Convert.ToDecimal( ls_amount.Text )+ total_wsp).ToString();
                    ls_gst.Text = Convert.ToDecimal(Convert.ToDecimal(ls_gst.Text)+ total_rsp).ToString();
                }
                else
                {
                    label1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void txt_search_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Chk_Minimum.Checked)
                {
                    Lab_Msg.Visible = false;
                    DataTable dtb = new DataTable();
                    if (String.IsNullOrWhiteSpace(txt_search.Text))
                    {
                        dtb = this.cntrl.search_minimum(type);
                    }
                    else
                    {
                        dtb = this.cntrl.search_minium_wit_itemname(txt_search.Text);
                    }
                    Lab_Msg.Visible = false;
                    if (dtb.Rows.Count > 0)
                    {
                        Fill_Mingrid(dtb);
                    }
                    else
                    {
                        DGV_Stock.Rows.Clear();
                        Lab_Msg.Visible = true;
                    }
                }
                else
                {
                    DataTable dtb = new DataTable();
                    if (String.IsNullOrWhiteSpace(txt_search.Text))
                    {
                        dtb = this.cntrl.search_minimum(type);
                    }
                    else
                    {
                        dtb = this.cntrl.search_minium_wit_itemname(txt_search.Text);
                    }
                    Lab_Msg.Visible = false;
                    if (dtb.Rows.Count > 0)
                    {
                        load_stock(dtb);
                    }
                    else
                    {
                        DGV_Stock.Rows.Clear();
                        Lab_Msg.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Fill_Mingrid(DataTable dtb)
        {
            try
            {
                Lab_Msg.Visible = false;
                string current_Stock = "", WSP = "", RSP = ""; int unitmf = 0; int qty = 0; int quotient = 0; int Remainder = 0; decimal value = 0, stock = 0;
                int num = 1; DGV_Stock.Rows.Clear();
                if (dtb.Rows.Count > 0)
                {
                    for (int i = 0; i < dtb.Rows.Count; i++)
                    {
                        DataTable dtb_Min = this.cntrl.minimumStock(dtb.Rows[i]["id"].ToString());
                        DataTable dtunit = this.cntrl.itemdetails(dtb.Rows[i]["id"].ToString());
                        if (dtunit.Rows.Count > 0)
                        {
                            if (dtunit.Rows[0]["OneUnitOnly"].ToString() == "False")
                            {
                                unitmf = Convert.ToInt32(dtunit.Rows[0]["UnitMF"].ToString());
                                value = Convert.ToDecimal(dtb.Rows[i]["qty"].ToString());
                                qty = Convert.ToInt32(value);
                                quotient = Convert.ToInt32(qty / unitmf);
                                Remainder = Convert.ToInt32(qty % unitmf);
                                if (quotient < Convert.ToDecimal(dtb_Min.Rows[0][0].ToString()))
                                {
                                    current_Stock = "qty (" + dtunit.Rows[0]["Unit1"].ToString() + ") " + "=" + " " + quotient + " " + "," + " " + dtunit.Rows[0]["Unit2"].ToString() + " " + "=" + Remainder;
                                    DGV_Stock.Rows.Add(dtb.Rows[i]["id"].ToString(),num, dtb.Rows[i]["item_code"].ToString(), dtunit.Rows[0]["item_name"].ToString(), dtunit.Rows[0]["unit"].ToString(), current_Stock);
                                    DGV_Stock.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                                }
                                else
                                {
                                    current_Stock = "qty (" + dtunit.Rows[0]["Unit1"].ToString() + " )" + "=" + " " + quotient + " " + "," + " " + dtunit.Rows[0]["Unit2"].ToString() + " " + "=" + Remainder;
                                    DGV_Stock.Rows.Add(dtb.Rows[i]["id"].ToString(),num, dtb.Rows[i]["item_code"].ToString(), dtunit.Rows[0]["item_name"].ToString(), dtunit.Rows[0]["unit"].ToString(), current_Stock);
                                    DGV_Stock.Rows[i].Visible = false;
                                }
                            }
                            else
                            {
                                if (Convert.ToDecimal(dtb.Rows[i]["qty"].ToString()) < Convert.ToDecimal(dtb_Min.Rows[0][0].ToString()))
                                {
                                    stock = Convert.ToDecimal(dtb.Rows[i]["qty"].ToString());
                                    current_Stock = "qty (" + dtunit.Rows[0]["Unit1"].ToString() + ") " + "=" + " " + Math.Floor(stock);
                                    DGV_Stock.Rows.Add(dtb.Rows[i]["id"].ToString(),num, dtb.Rows[i]["item_code"].ToString(), dtunit.Rows[0]["item_name"].ToString(), dtunit.Rows[0]["unit"].ToString(), current_Stock);
                                    DGV_Stock.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                                }
                                else
                                {
                                    stock = Convert.ToDecimal(dtb.Rows[i]["qty"].ToString());
                                    current_Stock = "qty (" + dtunit.Rows[0]["Unit1"].ToString() + ") " + "=" + " " + Math.Floor(stock);
                                    DGV_Stock.Rows.Add(dtb.Rows[i]["id"].ToString(),num, dtb.Rows[i]["item_code"].ToString(), dtunit.Rows[0]["item_name"].ToString(), dtunit.Rows[0]["unit"].ToString(), current_Stock);
                                    DGV_Stock.Rows[i].Visible = false;
                                }
                            }
                        }
                        num = num + 1;
                    }
                }
                else
                {
                    Lab_Msg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_search_Click(object sender, EventArgs e)
        {
            txt_search.Text = "";
        }

        private void Chk_Suplier_CheckedChanged(object sender, EventArgs e)
        {
            
             if (Chk_Suplier.Checked)
            {
                DGV_Stock.Rows.Clear();
                Chk_Minimum.Checked = false;
                Cmb_Suplier.Visible = true;
                Lab_Suplier.Visible = true;
                txt_search.Visible = false;
                Lab_search.Visible = false;
            }
            else
            {
                DataTable dt_stock = this.cntrl.search_minimum(type);
                load_stock(dt_stock);
                Chk_Minimum.Checked = false;
                Cmb_Suplier.Visible = false;
                Lab_Suplier.Visible = false;
                txt_search.Visible = true;
                Lab_search.Visible = true;
            }
        }

        private void Cmb_Suplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
       
        private void Cmb_Suplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(load_flag==false)
            {
                label1.Visible = false;
                Chk_Minimum.Checked = false;
                if (Cmb_Suplier.Visible == true)
                {
                    if (Cmb_Suplier.SelectedIndex >= 0)
                    {
                        Lab_Msg.Visible = false;
                        string sup = Cmb_Suplier.SelectedItem.ToString();
                        DataTable dt_sup = this.cntrl.get_supcode(sup);
                        dtb = this.cntrl.Load_supplier_items(dt_sup.Rows[0][0].ToString(), type);
                        if (dtb.Rows.Count > 0)
                        {
                            load_stock(dtb);
                            if (Chk_Minimum.Checked)
                            {
                                fill_Minimun();
                            }
                        }
                        else
                        {
                            DGV_Stock.Rows.Clear();
                        }
                    }
                }
            }
            
        }
        public void fill_Minimun()
        {
            dtb = new DataTable();
            foreach (DataGridViewColumn col in DGV_Stock.Columns)
            {
                dtb.Columns.Add(col.Name);
            }
            foreach (DataGridViewRow row in DGV_Stock.Rows)
            {
                DataRow dRow = dtb.NewRow();
                if (row.DefaultCellStyle.ForeColor == Color.Red)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dRow[cell.ColumnIndex] = cell.Value;
                    }
                    dtb.Rows.Add(dRow);
                }
            }
            if (dtb.Rows.Count > 0)
            {
                DGV_Stock.Rows.Clear();
                int num = 1;
                for (int i = 0; i < dtb.Rows.Count; i++)
                {
                    DGV_Stock.Rows.Add(dtb.Rows[i][0].ToString(),num, dtb.Rows[i][2].ToString(), dtb.Rows[i][3].ToString(), dtb.Rows[i][4].ToString(), dtb.Rows[i][5].ToString(), dtb.Rows[i][6].ToString(), dtb.Rows[i][7].ToString(), dtb.Rows[i][8].ToString());
                    num++;
                    DGV_Stock.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }
            }
            else
            {
                MessageBox.Show("There is no minimum stock items.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Chk_Minimum.Checked = false;
            }
        }

     

        private void frmstockReport_Activated(object sender, EventArgs e)
        {
            if (Connection.MyGlobals.global_Flag == true)
            {
                if (Pur_orderFlag == true)
                {
                    backColor_Change();
                    salesList.Hide();
                    purchase_LIst.Hide();
                    Pu_returnList.Hide();
                    s_orderList.Hide();
                    pur_order.Hide();
                    pur_return.Hide();
                    S_ReturnList.Hide();
                    purchase.Hide();
                    sales_order.Hide();
                    salesreturn.Hide();
                    sales.Hide();
                    Pur_orderFlag = true;
                    sales_orderFlag = false;
                    Pur_ListFlag = false;
                    sales_ListFlag = false;
                    Pur_retnFlag = false;
                    Connection.MyGlobals.global_Flag = false;
                    string date1 = Connection.MyGlobals.Date_From;
                    string date2 = Connection.MyGlobals.Date_To;
                    panel_main.Show();
                    PU_orderList.Close();
                    if (PU_orderList.IsDisposed)
                        PU_orderList = new Purchase_order_list(date1, date2);
                    PU_orderList.TopLevel = false;
                    panel_main.Controls.Add(PU_orderList);
                    PU_orderList.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    PU_orderList.Show();
                }
                else if (sales_orderFlag == true)
                {
                    backColor_Change();
                    salesreturn.Hide();
                    purchase.Hide();
                    pur_return.Hide();
                    Pu_returnList.Hide();
                    sales.Hide();
                    sales_order.Hide();
                    salesList.Hide();
                    purchase_LIst.Hide();
                    S_ReturnList.Hide();
                    pur_order.Hide();
                    PU_orderList.Hide();
                    Pur_orderFlag = false;
                    Pur_ListFlag = false;
                    sales_ListFlag = false;
                    sales_orderFlag = true;
                    Pur_retnFlag = false;
                    Connection.MyGlobals.global_Flag = false;
                    string date1 = Connection.MyGlobals.Date_From;
                    string date2 = Connection.MyGlobals.Date_To;
                    panel_main.Show();
                    s_orderList.Close();
                    if (s_orderList == null || s_orderList.IsDisposed)
                        s_orderList = new Sales_order_list(date1, date2);
                    s_orderList.TopLevel = false;
                    panel_main.Controls.Add(s_orderList);
                    s_orderList.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    s_orderList.Show();
                }
                else if (Pur_ListFlag == true)
                {
                    backColor_Change();
                    salesreturn.Hide();
                    PU_orderList.Hide();
                    Pu_returnList.Hide();
                    S_ReturnList.Hide();
                    pur_return.Hide();
                    salesList.Hide();
                    s_orderList.Hide();
                    purchase.Hide();
                    pur_order.Hide();
                    sales.Hide();
                    sales_order.Hide();
                    Pur_ListFlag = true;
                    sales_ListFlag = false;
                    Pur_orderFlag = false;
                    sales_orderFlag = false;
                    Pur_retnFlag = false;
                    Connection.MyGlobals.global_Flag = false;
                    string date1 = Connection.MyGlobals.Date_From;
                    string date2 = Connection.MyGlobals.Date_To;
                    panel_main.Show();
                    purchase_LIst.Close();
                    if (purchase_LIst == null || purchase_LIst.IsDisposed)
                        purchase_LIst = new PurchaseList(date1, date2);
                    purchase_LIst.TopLevel = false;
                    panel_main.Controls.Add(purchase_LIst);
                    purchase_LIst.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    purchase_LIst.Show();
                }
                else if (sales_ListFlag == true)
                {
                    backColor_Change();
                    purchase_LIst.Hide();
                    salesreturn.Hide(); PU_orderList.Hide();
                    Pu_returnList.Hide();
                    S_ReturnList.Hide();
                    pur_return.Hide();
                    s_orderList.Hide();
                    purchase.Hide(); pur_order.Hide();
                    sales.Hide(); sales_order.Hide();
                    Pur_ListFlag = false;
                    sales_ListFlag = true;
                    Pur_orderFlag = false;
                    sales_orderFlag = false;
                    Pur_retnFlag = false;
                    Connection.MyGlobals.global_Flag = false;
                    string date1 = Connection.MyGlobals.Date_From;
                    string date2 = Connection.MyGlobals.Date_To;
                    panel_main.Show();
                    salesList.Close();
                    if (salesList.IsDisposed)
                        salesList = new Sales_List(date1, date2);
                    salesList.TopLevel = false;
                    panel_main.Controls.Add(salesList);
                    salesList.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    salesList.Show();
                }
                else if (Pur_retnFlag == true)
                {
                    backColor_Change();
                    salesreturn.Hide();
                    salesList.Hide();
                    purchase_LIst.Hide();
                    s_orderList.Hide();
                    pur_return.Hide();
                    PU_orderList.Hide();
                    S_ReturnList.Hide();
                    sales.Hide();
                    sales_order.Hide();
                    purchase.Hide();
                    pur_order.Hide();
                    Pur_ListFlag = false;
                    Pur_retnFlag = true;
                    sales_ListFlag = false;
                    Pur_orderFlag = false;
                    sales_orderFlag = false;
                    Connection.MyGlobals.global_Flag = false;
                    string date1 = Connection.MyGlobals.Date_From;
                    string date2 = Connection.MyGlobals.Date_To;
                    panel_main.Show();
                    Pu_returnList.Close();
                    if (Pu_returnList == null || Pu_returnList.IsDisposed)
                        Pu_returnList = new Purchase_Return_List(date1, date2);
                    Pu_returnList.TopLevel = false;
                    panel_main.Controls.Add(Pu_returnList);
                    Pu_returnList.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    Pu_returnList.Show();
                }
            }
        }

        private void btn_Stock_Click(object sender, EventArgs e)
        { 
            sales_collaps.Visible = false;
            Purchase_menu.Visible = false;
            stock_transfer_menu.Visible = false;
            FormHide();
            panel_main.Hide();
            backColor_Change();
            btn_Stock.BackColor = Color.SteelBlue;
            DataTable dt_stock = this.cntrl.search_minimum(type);
            load_stock(dt_stock);
            Chk_Minimum.Checked = false;
        }

        private void btn_orderList_Click(object sender, EventArgs e)
        {
           
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                print_setting();
                print();
            }
            catch
            {
                MessageBox.Show("Printing Error..", "Print error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        public void print_setting()
        {
            System.Data.DataTable print = this.cntrl.get_printSettings();
            if (print.Rows.Count > 0)
            {
                combo_topmargin = print.Rows[0][4].ToString();
                combo_leftmargin = print.Rows[0][5].ToString();
                combo_bottommargin = print.Rows[0][6].ToString();
                combo_rightmargin = print.Rows[0][7].ToString();
                combo_paper_size = print.Rows[0][1].ToString();
                combo_footer_topmargin = print.Rows[0][22].ToString();
                rich_fullwidth = print.Rows[0][23].ToString();
                rich_leftsign = print.Rows[0][24].ToString();
                rich_rightsign = print.Rows[0][25].ToString();
                patient_details = print.Rows[0][14].ToString();
                med = print.Rows[0][15].ToString();
                patient = print.Rows[0][16].ToString();
                address = print.Rows[0][17].ToString();
                phone = print.Rows[0][18].ToString();
                blood = print.Rows[0][20].ToString();
                gender = print.Rows[0][21].ToString();
                orientation = print.Rows[0][2].ToString();
                includeheader = print.Rows[0]["include_header"].ToString();
                includelogo = print.Rows[0]["include_logo"].ToString();
            }
        }
        int rowcount;
        private void btn_stocktransfer_Click(object sender, EventArgs e)
        {
            sales_collaps.Visible = false;
            Purchase_menu.Visible = false;
            stock_transfer_menu.Visible = false;
            backColor_Change();
            btn_stocktransfer.BackColor = Color.SteelBlue;
            FormHide();
            panel_main.Show();
            transfer.Close();
            if (transfer.IsDisposed)
                transfer = new stock_transfer();
            transfer.TopLevel = false;
            sales.doctor_id = doctor_id;
            panel_main.Controls.Add(transfer);
            transfer.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            transfer.Show();
        }
        private void btn_stock_updation_Click(object sender, EventArgs e)
        {
            backColor_Change();
            btn_stock_updation.BackColor = Color.SteelBlue;
            FormHide();
            panel_main.Show();
            stck_update.Close();
            if (stck_update.IsDisposed)
                stck_update = new Stock_Adjustment();
            stck_update.TopLevel = false;
            panel_main.Controls.Add(stck_update);
            stck_update.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            stck_update.Show();
            sales_collaps.Visible = false;
            Purchase_menu.Visible = false;
            stock_transfer_menu.Visible = false;
        }

        private void btn_updatiobnlist_Click(object sender, EventArgs e)
        {
            sales_collaps.Visible = false;
            Purchase_menu.Visible = false;
            stock_transfer_menu.Visible = false;
            backColor_Change();
            btn_updatiobnlist.BackColor = Color.SteelBlue;
            FormHide();
            panel_main.Show();
            uplist.Close();
            if (uplist.IsDisposed)
                uplist = new Account_Statement();
            uplist.TopLevel = false;
            panel_main.Controls.Add(uplist);
            uplist.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            uplist.Show();
        }

        private void btn_Stockledger_Click(object sender, EventArgs e)
        {
            sales_collaps.Visible = false;
            Purchase_menu.Visible = false;
            stock_transfer_menu.Visible = false;
            backColor_Change();
            btn_Stockledger.BackColor = Color.SteelBlue;
            FormHide();
            panel_main.Show();
            stk_ledger.Close();
            if (stk_ledger.IsDisposed)
                stk_ledger = new Stock_Ledger();
            stk_ledger.TopLevel = false;
            panel_main.Controls.Add(stk_ledger);
            stk_ledger.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            stk_ledger.Show();
        }

        private void button2_Click(object sender, EventArgs e)//start  sanoop
        {
            if (doctor_id != "1")
            {
                string id1;
                id1 = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='Purchase' and Permission='A'");
                if (int.Parse(id1) > 0)
                {
                    if (j == 1)
                    {
                        int i = 0;
                        dtb.Columns.Clear();

                        dtb.AcceptChanges();
                        foreach (DataRow row in dtb.Rows)
                        {
                            row.Delete();
                        }
                        dtb.AcceptChanges();
                        DataColumn dc = new DataColumn("id", typeof(String));
                        dtb.Columns.Add(dc);
                        dc = new DataColumn("item_code", typeof(String));
                        dtb.Columns.Add(dc);
                        dc = new DataColumn("item_name", typeof(String));
                        dtb.Columns.Add(dc);
                        foreach (DataGridViewRow row in DGV_Stock.SelectedRows)
                        {
                            DataRow rows = dtb.NewRow();
                            dtb.Rows.Add(rows);
                            dtb.Rows[i]["id"] = row.Cells[0].Value;
                            dtb.Rows[i]["item_code"] = row.Cells[2].Value;
                            dtb.Rows[i]["item_name"] = row.Cells[3].Value;
                            i++;
                        }
                        PurchaseOrder frm2 = new PurchaseOrder();
                        string supplier = Cmb_Suplier.SelectedItem.ToString();
                        frm2.supplier = supplier;
                        DataTable dt = this.cntrl.getsupplierid(supplier);
                        frm2.supplierid = Convert.ToInt32(dt.Rows[0]["Supplier_Code"]);
                        frm2.flag1 = 1;
                        frm2.dts = dtb;
                        frm2.ShowDialog();
                        j = 0;
                        Chk_Minimum.Checked = false;
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
                        DataTable dt_load = this.cntrl.load_stock(type);
                        load_stock(dt_load);
                    }
                    else
                    {
                        MessageBox.Show("Please select items", "Item not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("There is No Privilege to Add Purchase Order", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                if (j == 1)
                {
                    int i = 0;
                    dtb.Columns.Clear();

                    dtb.AcceptChanges();
                    foreach (DataRow row in dtb.Rows)
                    {
                        row.Delete();
                    }
                    dtb.AcceptChanges();
                    DataColumn dc = new DataColumn("id", typeof(String));
                    dtb.Columns.Add(dc);
                    dc = new DataColumn("item_code", typeof(String));
                    dtb.Columns.Add(dc);
                    dc = new DataColumn("item_name", typeof(String));
                    dtb.Columns.Add(dc);
                    foreach (DataGridViewRow row in DGV_Stock.SelectedRows)
                    {
                        DataRow rows = dtb.NewRow();
                        dtb.Rows.Add(rows);
                        dtb.Rows[i]["id"] = row.Cells[0].Value;
                        dtb.Rows[i]["item_code"] = row.Cells[2].Value;
                        dtb.Rows[i]["item_name"] = row.Cells[3].Value;
                        i++;
                    }
                    PurchaseOrder frm2 = new PurchaseOrder();
                    string supplier = Cmb_Suplier.SelectedItem.ToString();
                    frm2.supplier = supplier;
                    DataTable dt = this.cntrl.getsupplierid(supplier);
                    frm2.supplierid = Convert.ToInt32(dt.Rows[0]["Supplier_Code"]);
                    frm2.flag1 = 1;
                    frm2.dts = dtb;
                    frm2.ShowDialog();
                    j = 0;
                    Chk_Minimum.Checked = false;
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
                    DataTable dt_load = this.cntrl.load_stock(type);
                    load_stock(dt_load);
                }
                else
                {
                    MessageBox.Show("please select one item","Items Not Found ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
        }
        //end sanoop

        private void DGV_Stock_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void DGV_Stock_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            j =1;
            if(e.RowIndex>=0)
            {
                if(DGV_Stock.CurrentCell.OwningColumn.Name== "item_name")
                {
                    var frm = new stock_det();
                    frm.id = DGV_Stock.Rows[e.RowIndex].Cells["id"].Value.ToString();//item_code
                    frm.ShowDialog();
                    frm.Dispose();
                }
            }
        }

        struct DataParameter
        {
            public int process;
            public int delay;
        }
        public int row_count = 0;
        private DataParameter _inputparameter;
        private void label1_Click(object sender, EventArgs e)
        {
            Chk_Minimum.Checked = false;
            Chk_Suplier.Checked = false;
            int count = row_count + 25;
            DataTable dt_load = this.cntrl.load_fullstock(count);
            load_stock_showmore(dt_load);
            row_count = count;
            
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            string checkStr = "0"; string PathName = "";
            try
            {
                if (DGV_Stock.Rows.Count != 0)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Excel Files (*.xls)|*.xls";
                    saveFileDialog1.FileName = "Stock Report(" + DateTime.Now.ToString("dd-MM-yy h.mm.ss tt") + ").xls";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        PathName = saveFileDialog1.FileName;
                        Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                        ExcelApp.Application.Workbooks.Add(Type.Missing);
                        ExcelApp.Columns.ColumnWidth = 20;
                        int count = DGV_Stock.Columns.Count;
                        ExcelApp.Range[ExcelApp.Cells[1, 1], ExcelApp.Cells[1, count]].Merge();
                        ExcelApp.Cells[1, 1] = "STOCK REPORT";
                        ExcelApp.Cells[1, 1].HorizontalAlignment = HorizontalAlignment.Center;
                        ExcelApp.Cells[1, 1].Font.Size = 12;
                        ExcelApp.Cells[1, 1].Interior.Color = Color.FromArgb(153, 204, 255);
                        ExcelApp.Columns.ColumnWidth = 20;
                        ExcelApp.Cells[2, 1] = "Running Date";
                        ExcelApp.Cells[2, 1].Font.Size = 10;
                        ExcelApp.Cells[2, 2] = DateTime.Now.ToString("dd-MM-yyyy");
                        ExcelApp.Cells[2, 2].Font.Size = 10;
                        ExcelApp.Cells[2, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        ExcelApp.Cells[3, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        ExcelApp.Cells[2, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        for (int i = 1; i < DGV_Stock.Columns.Count + 1; i++)
                        {
                            ExcelApp.Cells[4, i] = DGV_Stock.Columns[i - 1].HeaderText;
                            ExcelApp.Cells[4, i].ColumnWidth = 25;
                            ExcelApp.Cells[4, i].EntireRow.Font.Bold = true;
                            ExcelApp.Cells[4, i].Interior.Color = Color.FromArgb(0, 102, 204);
                            ExcelApp.Cells[4, i].Font.Size = 10;
                            ExcelApp.Cells[4, i].Font.Name = "Arial";
                            ExcelApp.Cells[4, i].Font.Color = Color.FromArgb(255, 255, 255);
                            ExcelApp.Cells[4, i].Interior.Color = Color.FromArgb(0, 102, 204);
                        }
                        for (int i = 0; i <= DGV_Stock.Rows.Count; i++)
                        {
                            try
                            {
                                for (int j = 0; j < DGV_Stock.Columns.Count; j++)
                                {
                                    ExcelApp.Cells[i + 5, j + 1] = DGV_Stock.Rows[i].Cells[j].Value.ToString();
                                    ExcelApp.Cells[i + 5, j + 1].BorderAround(true);
                                    ExcelApp.Cells[i + 5, j + 1].Borders.Color = Color.FromArgb(0, 102, 204);
                                    ExcelApp.Cells[i + 5, j + 1].Font.Size = 8;
                                }
                            }
                            catch
                            {
                            }
                        }
                        ExcelApp.ActiveWorkbook.SaveAs(PathName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                        ExcelApp.ActiveWorkbook.Saved = true;
                        ExcelApp.Quit();
                        checkStr = "1";
                        MessageBox.Show("Successfully Exported to Excel", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                    MessageBox.Show("No records found,Please change the date and try again!..", "Failed ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        DataTable dt_load = new DataTable();
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            dt_load = null;
            Chk_Minimum.Checked = false;
            Chk_Suplier.Checked = false;
        }

        private void pb_Sales_sub_Click(object sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            Point ptLowerLeft = new Point(0, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
            Sales_CMenuStrip.Show(ptLowerLeft);
        }
    
        private void btn_sales_sub_Click(object sender, EventArgs e)
        {
           
            Purchase_menu.Visible = false;
            stock_transfer_menu.Visible = false;
            if (sales_collaps.Visible== true )
            {
                sales_collaps.Visible = false;
                sales_collaps.Height = 46;
                sales_collaps.Location = new Point(167, 69);
            }
            else
            {
                sales_collaps.Visible = true;
                sales_collaps.Height = 168;
                sales_collaps.Location = new Point(166, 62);
            }
        }

        private void salesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backColor_Change();
            FormHide();
            sales_ListFlag = true;
            panel_main.Show();
            salesList.Close();
            if (salesList.IsDisposed)
                salesList = new Sales_List();
            salesList.TopLevel = false;
            sales.doctor_id = doctor_id;
            panel_main.Controls.Add(salesList);
            salesList.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            salesList.Show();
        }

        private void salesOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backColor_Change();
            btn_SalesOrder.BackColor = Color.SteelBlue;
            FormHide();
            panel_main.Show();
            sales_order.Close();
            if (sales_order == null || sales_order.IsDisposed)
                sales_order = new SalesOrder();
            sales_order.TopLevel = false;
            sales_order.doctor_id = doctor_id;
            panel_main.Controls.Add(sales_order);
            sales_order.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            sales_order.Show();
        }

        private void salesOrderListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backColor_Change();
            FormHide();
            panel_main.Show();
            s_orderList.Close();
            if (s_orderList == null || s_orderList.IsDisposed)
                s_orderList = new Sales_order_list();
            s_orderList.TopLevel = false;
            sales.doctor_id = doctor_id;
            panel_main.Controls.Add(s_orderList);
            s_orderList.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            s_orderList.Show();
        }

        private void salesReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backColor_Change();
            FormHide();
            panel_main.Show();
            pur_return.Close();
            if (salesreturn == null || salesreturn.IsDisposed)
                salesreturn = new Sales_Return();
            salesreturn.TopLevel = false;
            salesreturn.doctor_id = doctor_id;
            panel_main.Controls.Add(salesreturn);
            salesreturn.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            salesreturn.Show();
        }

        private void salesReturnListToolStripMenuItem_Click(object sender, EventArgs e)
        {

            backColor_Change();
            FormHide();
            panel_main.Show();
            S_ReturnList.Close();
            if (S_ReturnList.IsDisposed)
                S_ReturnList = new Sales_ReturnList();
            S_ReturnList.TopLevel = false;
            sales.doctor_id = doctor_id;
            panel_main.Controls.Add(S_ReturnList);
            S_ReturnList.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            S_ReturnList.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

            backColor_Change();
            FormHide();
            panel_main.Show();
            pur_order.Close();
            if (pur_order == null || pur_order.IsDisposed)
                pur_order = new PurchaseOrder();
            pur_order.TopLevel = false;
            pur_order.doctor_id = doctor_id;
            panel_main.Controls.Add(pur_order);
            pur_order.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            pur_order.Show();
        }

        private void btn_pur_sub_Click(object sender, EventArgs e)
        {
         
            sales_collaps.Visible = false;
            stock_transfer_menu.Visible = false;
            if (Purchase_menu.Visible == true)
            {
                Purchase_menu.Visible = false;
                Purchase_menu.Height = 46;  
                Purchase_menu.Location = new Point(170, 112);
            }
            else
            {
                Purchase_menu.Visible = true;
                Purchase_menu.Height = 170;
                Purchase_menu.Location = new Point(165, 98);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            backColor_Change();
            FormHide();
            Pur_ListFlag = true;
            panel_main.Show();
            purchase_LIst.Close();
            if (purchase_LIst.IsDisposed)
                purchase_LIst = new PurchaseList();
            purchase_LIst.TopLevel = false;
            sales.doctor_id = doctor_id;
            panel_main.Controls.Add(purchase_LIst);
            purchase_LIst.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            purchase_LIst.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            backColor_Change();
            FormHide();
            Pur_orderFlag = true;
            panel_main.Show();
            PU_orderList.Close();
            if (PU_orderList.IsDisposed)
                PU_orderList = new Purchase_order_list();
            PU_orderList.TopLevel = false;
            sales.doctor_id = doctor_id;
            panel_main.Controls.Add(PU_orderList);
            PU_orderList.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            PU_orderList.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            backColor_Change();
            FormHide();
            panel_main.Show();
            pur_return.Close();
            if (pur_return == null || pur_return.IsDisposed)
                pur_return = new Purchase_Return();
            pur_return.TopLevel = false;
            pur_return.doctor_id = doctor_id;
            panel_main.Controls.Add(pur_return);
            pur_return.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            pur_return.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            backColor_Change();
            FormHide();
            Pur_retnFlag = true;
            panel_main.Show();
            Pu_returnList.Close();
            if (Pu_returnList == null || Pu_returnList.IsDisposed)
                Pu_returnList = new Purchase_Return_List();
            Pu_returnList.TopLevel = false;
            sales.doctor_id = doctor_id;
            panel_main.Controls.Add(Pu_returnList);
            Pu_returnList.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Pu_returnList.Show();
        }

        private void btn_stock_sub_Click(object sender, EventArgs e)
        {
            sales_collaps.Visible = false;
            Purchase_menu.Visible = false;
            if (stock_transfer_menu.Visible == true)
            {
                stock_transfer_menu.Visible = false;
                stock_transfer_menu.Height = 39;    
                stock_transfer_menu.Location = new Point(171, 146);
            }
            else
            {
                stock_transfer_menu.Visible = true;
                stock_transfer_menu.Height = 39;
                stock_transfer_menu.Location = new Point(165, 140);
            }
        }

        private void stockTransferListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backColor_Change();
            FormHide();
            panel_main.Show();
            transList.Close();
            if (transList.IsDisposed)
                transList = new Stock_TransferList();
            transList.TopLevel = false;
            panel_main.Controls.Add(transList);
            transList.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            transList.Show();
        }
        bool sliderExpand;
        private void slider_timer_Tick(object sender, EventArgs e)
        {
            if(sliderExpand)
            {
                sales_collaps.Height -= 10;
                if (sales_collaps.Height == sales_collaps.MaximumSize.Height)
                {
                    sliderExpand = false;
                    slider_timer.Stop();
                }
            }
            else
            {
                sales_collaps.Height += 10;
                if (sales_collaps.Height == sales_collaps.MinimumSize.Height)
                {
                    sliderExpand = true;
                    slider_timer.Stop();
                }
            }
        }

        private void btn_st_list_Click(object sender, EventArgs e)
        {
            backColor_Change();
            FormHide(); stock_transfer_menu.Visible = false;
            panel_main.Show();
            transList.Close();
            if (transList.IsDisposed)
                transList = new Stock_TransferList();
            transList.TopLevel = false;
            panel_main.Controls.Add(transList);
            transList.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            transList.Show();
        }

        private void btn_p_return_Click(object sender, EventArgs e)
        {
            backColor_Change();
            FormHide(); Purchase_menu.Visible = false;
            panel_main.Show();
            pur_return.Close();
            if (pur_return == null || pur_return.IsDisposed)
                pur_return = new Purchase_Return();
            pur_return.TopLevel = false;
            pur_return.doctor_id = doctor_id;
            panel_main.Controls.Add(pur_return);
            pur_return.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            pur_return.Show();
        }

        private void btn_p_list_Click(object sender, EventArgs e)
        {
            backColor_Change();
            FormHide(); Purchase_menu.Visible = false;
            Pur_ListFlag = true;
            panel_main.Show();
            purchase_LIst.Close();
            if (purchase_LIst.IsDisposed)
                purchase_LIst = new PurchaseList();
            purchase_LIst.TopLevel = false;
            sales.doctor_id = doctor_id;
            panel_main.Controls.Add(purchase_LIst);
            purchase_LIst.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            purchase_LIst.Show();
        }

        private void btn_p_order_Click(object sender, EventArgs e)
        {
            backColor_Change();
            FormHide(); Purchase_menu.Visible = false;
            panel_main.Show();
            pur_order.Close();
            if (pur_order == null || pur_order.IsDisposed)
                pur_order = new PurchaseOrder();
            pur_order.TopLevel = false;
            pur_order.doctor_id = doctor_id;
            panel_main.Controls.Add(pur_order);
            pur_order.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            pur_order.Show();
        }

        private void btn_p_o_list_Click(object sender, EventArgs e)
        {
            backColor_Change();
            FormHide(); Purchase_menu.Visible = false;
            Pur_orderFlag = true;
            panel_main.Show();
            PU_orderList.Close();
            if (PU_orderList.IsDisposed)
                PU_orderList = new Purchase_order_list();
            PU_orderList.TopLevel = false;
            sales.doctor_id = doctor_id;
            panel_main.Controls.Add(PU_orderList);
            PU_orderList.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            PU_orderList.Show();
        }

        private void btn_p_r_list_Click(object sender, EventArgs e)
        {
            backColor_Change();
            FormHide();
            Pur_retnFlag = true; Purchase_menu.Visible = false;
            panel_main.Show();
            Pu_returnList.Close();
            if (Pu_returnList == null || Pu_returnList.IsDisposed)
                Pu_returnList = new Purchase_Return_List();
            Pu_returnList.TopLevel = false;
            sales.doctor_id = doctor_id;
            panel_main.Controls.Add(Pu_returnList);
            Pu_returnList.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Pu_returnList.Show();
        }

        private void btn_s_o_list_Click(object sender, EventArgs e)
        {
            backColor_Change();
            FormHide(); sales_collaps.Visible = false;
            panel_main.Show();
            s_orderList.Close();
            if (s_orderList == null || s_orderList.IsDisposed)
                s_orderList = new Sales_order_list();
            s_orderList.TopLevel = false;
            sales.doctor_id = doctor_id;
            panel_main.Controls.Add(s_orderList);
            s_orderList.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            s_orderList.Show();
        }

        private void btn_s_order_Click(object sender, EventArgs e)
        {
            backColor_Change();
            btn_SalesOrder.BackColor = Color.SteelBlue;
            FormHide(); sales_collaps.Visible = false;
            panel_main.Show();
            sales_order.Close();
            if (sales_order == null || sales_order.IsDisposed)
                sales_order = new SalesOrder();
            sales_order.TopLevel = false;
            sales_order.doctor_id = doctor_id;
            panel_main.Controls.Add(sales_order);
            sales_order.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            sales_order.Show();
        }

        private void btn_S_list_Click(object sender, EventArgs e)
        {
            backColor_Change();
            FormHide();
            sales_ListFlag = true;
            sales_collaps.Visible = false;
            panel_main.Show();
            salesList.Close();
            if (salesList.IsDisposed)
                salesList = new Sales_List();
            salesList.TopLevel = false;
            sales.doctor_id = doctor_id;
            panel_main.Controls.Add(salesList);
            salesList.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            salesList.Show();
        }

        private void btn_s_return_Click(object sender, EventArgs e)
        {
            sales_collaps.Visible = false;
            backColor_Change();
            FormHide();
            panel_main.Show();
            pur_return.Close();
            if (salesreturn == null || salesreturn.IsDisposed)
                salesreturn = new Sales_Return();
            salesreturn.TopLevel = false;
            salesreturn.doctor_id = doctor_id;
            panel_main.Controls.Add(salesreturn);
            salesreturn.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            salesreturn.Show();
        }

        private void btn_s_r_list_Click(object sender, EventArgs e)
        {
            backColor_Change();
            FormHide(); sales_collaps.Visible = false;
            panel_main.Show();
            S_ReturnList.Close();
            if (S_ReturnList.IsDisposed)
                S_ReturnList = new Sales_ReturnList();
            S_ReturnList.TopLevel = false;
            sales.doctor_id = doctor_id;
            panel_main.Controls.Add(S_ReturnList);
            S_ReturnList.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            S_ReturnList.Show();
        }

       
        private void StockReport_MouseMove(object sender, MouseEventArgs e)
        {
            if (sales_collaps.ClientRectangle.Contains(e.Location))
            {
                sales_collaps.Visible = true;
            }
            else
            {
                sales_collaps.Visible = false;
            }
            if (Purchase_menu.ClientRectangle.Contains(e.Location))
            {
                Purchase_menu.Visible = true;
            }
            else
            {
                Purchase_menu.Visible = false;
            }
            if (stock_transfer_menu.ClientRectangle.Contains(e.Location))
            {
                stock_transfer_menu.Visible = true;
            }
            else
            {
                stock_transfer_menu.Visible = false;
            }
        }

        private void btn_ItemList_Click(object sender, EventArgs e)
        {
            sales_collaps.Visible = false;
            Purchase_menu.Visible = false;
            stock_transfer_menu.Visible = false;
            backColor_Change();
            btn_ItemList.BackColor = Color.SteelBlue;
            FormHide();
            panel_main.Show();
            item_list.Close();
            if (item_list.IsDisposed)
                item_list = new Item_List();
            item_list.TopLevel = false;
            panel_main.Controls.Add(item_list);
            item_list.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            item_list.Show();
        }
        Item_List item_list = new Item_List();
        Supplier suplier = new Supplier();
        Manufacture manufacture = new Manufacture();
        Choose_Consumables_Pharmacy consume = new Choose_Consumables_Pharmacy();
        private void btn_Manufacture_Click(object sender, EventArgs e)
        {
            sales_collaps.Visible = false;
            Purchase_menu.Visible = false;
            stock_transfer_menu.Visible = false;
            backColor_Change();
            btn_Manufacture.BackColor = Color.SteelBlue;
            FormHide();
            panel_main.Show();
            manufacture.Close();
            if (manufacture.IsDisposed)
                manufacture = new Manufacture();
            manufacture.TopLevel = false;
            panel_main.Controls.Add(manufacture);
            manufacture.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            manufacture.Show();
        }

        private void btnSuplier_Click(object sender, EventArgs e)
        {
            sales_collaps.Visible = false;
            Purchase_menu.Visible = false;
            stock_transfer_menu.Visible = false;
            backColor_Change();
            btnSuplier.BackColor = Color.SteelBlue;
            FormHide();
            panel_main.Show();
            suplier.Close();
            if (suplier.IsDisposed)
                suplier = new Supplier();
            suplier.TopLevel = false;
            panel_main.Controls.Add(suplier);
            suplier.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            suplier.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sales_collaps.Visible = false;
            Purchase_menu.Visible = false;
            stock_transfer_menu.Visible = false;
            backColor_Change();
            button3.BackColor = Color.SteelBlue;
            FormHide();
            panel_main.Show();
            consume.Close();
            if (consume.IsDisposed)
                consume = new Choose_Consumables_Pharmacy();
            consume.TopLevel = false;
            panel_main.Controls.Add(consume);
            consume.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            consume.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var frm = new Expiry_items_stock();
            frm.ShowDialog();
            DataTable expired = this.cntrl.exp_prd();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGV_Stock.Rows.Count > 0)
                {
                    btn_back.Visible = true;
                    rowcount = DGV_Stock.Rows.Count;
                    int count = rowcount + 100;
                    DataTable dt_load = this.cntrl.load_stock_lil(count);
                    load_stock(dt_load);
                    fill_Minimun();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void chk_consume_CheckedChanged(object sender, EventArgs e)
        {
            if(chk_consume.Checked==true)
            {
                if (DGV_Stock.Rows.Count > 0)
                {
                    DGV_Stock.Rows.Clear();
                    DataTable dt = this.cntrl.consumables();
                    load_stock(dt);
                }
            }
            else
            {
                DataTable dt_load = this.cntrl.load_stock(type);
                load_stock(dt_load);
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {

        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            DataTable dt_load = this.cntrl.load_stock(type);
            load_stock(dt_load);
            btn_back.Visible = false;
        }

        public void print()
        {
            string today = DateTime.Now.ToString("d/M/yyyy");
            string message = "Did you want Header on Print?";
            string caption = "Verification";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons);
            string clinicn = "";
            string strclinicname = "";
            string strStreet = "";
            string stremail = "";
            string strwebsite = "";
            string strphone = "";
            string path = "";
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                DataTable print_settng = this.prnt_ctrl.Get_inventory_id();
                System.Data.DataTable dtp = this.cntrl.get_company_details();
                if (dtp.Rows.Count > 0)
                {
                    if (dtp.Rows.Count > 0)
                    {
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
            System.IO.StreamWriter sWrite = new System.IO.StreamWriter(Apppath + "\\StockReport_print.html");
            sWrite.WriteLine("<html>");
            sWrite.WriteLine("<head>");
            sWrite.WriteLine("<style>");
            sWrite.WriteLine("table { border-collapse: collapse;}");
            sWrite.WriteLine("p.big {line-height: 400%;}");
            sWrite.WriteLine("</style>");
            sWrite.WriteLine("</head>");
            sWrite.WriteLine("<body >");
            sWrite.WriteLine("<div>");
            sWrite.WriteLine("<table align=center width=900 >");
            sWrite.WriteLine("<br>");
            if (includeheader == "1")
            {
                if (includelogo == "1")
                {
                    if (logo != null || logo_name != "")
                    {
                        string Appath = System.IO.Directory.GetCurrentDirectory();
                        if (File.Exists(Appath + "\\" + logo_name))
                        {
                            sWrite.WriteLine("<table align='left' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                            sWrite.WriteLine("<tr>");
                            sWrite.WriteLine("<td width='30px' height='50px' align='left' rowspan='3'><img src='" + Appath + "\\" + logo_name + "'style='width:70px;height:70px;' ></td>  ");
                            sWrite.WriteLine("<td width='870px' align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=4><b>&nbsp;" + strclinicname + "</font> <br><FONT  COLOR=black  face='Segoe UI' SIZE=2>&nbsp;" + strStreet + "<br>&nbsp;" + strphone + " </b></td></tr>");
                            sWrite.WriteLine("</table>");
                        }
                        else
                        {
                            sWrite.WriteLine("<table align='left' style='border: 1px ;border-collapse: collapse;'>");
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
                        sWrite.WriteLine("<table align='left' style='border: 1px ;border-collapse: collapse;'>");
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
                    sWrite.WriteLine("<table align='left' style='border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td  align='left' height='20px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + strclinicname + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td  align='left' height='20px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;" + strStreet + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' height='20px' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + strphone + "</font></td></tr>");
                    sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                    sWrite.WriteLine("</table>");
                }
            }//
            else
            {
                sWrite.WriteLine("<table align='left' style='border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td  align='left' height='20px'><FONT  COLOR=black  face='Segoe UI' SIZE=5></font></td></tr>");
                sWrite.WriteLine("<tr><td  align='left' height='20px'><FONT COLOR=black FACE='Segoe UI' SIZE=3></font></td></tr>");
                sWrite.WriteLine("<tr><td align='left' height='20px' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td></tr>");
                sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                sWrite.WriteLine("</table>");
            }
            sWrite.WriteLine("<table align='center' style='border: 1px ;border-collapse: collapse;'>");
            sWrite.WriteLine("<tr><td align='left'  ><hr/></td></tr>");
            sWrite.WriteLine("</table>");
            sWrite.WriteLine("<tr>");
            if (Chk_Minimum.Checked)
            {
                sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3> MINIMUM STOCK REPORT </font></center></b></td>");
            }
            else
                sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3>STOCK REPORT </font></center></b></td>");
            sWrite.WriteLine("</tr>");
            sWrite.WriteLine("<br>");
            sWrite.WriteLine("<br>");
            sWrite.WriteLine("<table align='center' style='width:100%;border: 1px ;border-collapse: collapse;'>");
            sWrite.WriteLine("<tr>");
            if (DGV_Stock.Rows.Count > 0)
            {
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='left' width='8%' word-wrap: break-word; style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>Slno.</b></font></th>");
                sWrite.WriteLine("<td align='left' width='20%' word-wrap: break-word; style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>Item Code</b></font></th>");
                sWrite.WriteLine("<td align='left' width='30%' word-wrap: break-word; style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'SIZE=2>&nbsp;<b>Item Name</b></font></th>");
                sWrite.WriteLine("<td align='left' width='10%' word-wrap: break-word; style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>Purchase Unit</b></font></th>");
                sWrite.WriteLine("<td align='left' width='22%' word-wrap: break-word; style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>Stock</b></font></th>");
                sWrite.WriteLine("<td align='left' width='22%' word-wrap: break-word; style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>WSP</b></font></th>");
                sWrite.WriteLine("<td align='left' width='22%' word-wrap: break-word; style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>RSP</b></font></th>");
                sWrite.WriteLine("<td align='left' width='22%' word-wrap: break-word; style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>Shelf Number</b></font></th>");
                sWrite.WriteLine("</tr>");
                for (int c = 0; c < DGV_Stock.Rows.Count; c++)
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + DGV_Stock.Rows[c].Cells["SLNO"].Value.ToString() + "</font></th>");
                    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + DGV_Stock.Rows[c].Cells["item_code"].Value.ToString() + "</font></th>");
                    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + DGV_Stock.Rows[c].Cells["item_name"].Value.ToString() + "</font></th>");
                    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + DGV_Stock.Rows[c].Cells["clunit"].Value.ToString() + "</font></th>");
                    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'' SIZE=2>&nbsp;" + DGV_Stock.Rows[c].Cells["qty"].Value.ToString() + "</font></th>");
                    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'' SIZE=2>&nbsp;" + DGV_Stock.Rows[c].Cells["WSP"].Value.ToString() + "</font></th>");
                    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'' SIZE=2>&nbsp;" + DGV_Stock.Rows[c].Cells["RSP"].Value.ToString() + "</font></th>");
                    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'' SIZE=2>&nbsp;" + DGV_Stock.Rows[c].Cells["shelfno"].Value.ToString() + "</font></th>");
                }
                sWrite.WriteLine("</tr >");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='center'  style='border:1px solid #000'  ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=3>   </font></td>");
                sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=3><b> TOTAL</b></font></td>");
                sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=3>   </font></td>");
                sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=3>  </font></td>");
                sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=3>  " + ls_amount.Text + " </font></td>");
                sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=3>  " + ls_gst.Text + " </font></td>");
                sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=3>  </font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("</div>");
                sWrite.WriteLine("<script>window.print();</script>");
                sWrite.WriteLine("</body>");
                sWrite.WriteLine("</html>");
                sWrite.Close();
                System.Diagnostics.Process.Start(Apppath + "\\StockReport_print.html");
            }
        }
    

        private void btn_purOrder_Click(object sender, EventArgs e)
        {
            backColor_Change();
            FormHide();
            panel_main.Show();
            pur_order.Close();
            if (pur_order == null || pur_order.IsDisposed)
                pur_order = new PurchaseOrder();
            pur_order.TopLevel = false;
            pur_order.doctor_id = doctor_id;
            panel_main.Controls.Add(pur_order);
            pur_order.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            pur_order.Show();
        }

     
    }
}
