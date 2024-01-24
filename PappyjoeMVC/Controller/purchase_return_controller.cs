using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PappyjoeMVC.Model;
using MySql.Data.MySqlClient;

namespace PappyjoeMVC.Controller
{
  public  class purchase_return_controller
    {
        purchase_return_model model = new purchase_return_model();
        Inventory_model inv_model = new Inventory_model();
        Common_model cmodel = new Common_model();
        public DataTable load_return_details(int purchRetNo1)
        {
            DataTable dtb = model.load_return_details(purchRetNo1);
            return dtb;
        }
        public int save_log(string log_usrid, string log_type, string log_descriptn, string logdate, string logtime, string log_stage,string typeid)
        {
            int j = cmodel.save_log(log_usrid, log_type, log_descriptn, logdate, logtime, log_stage, typeid);
            return j;
        }
        public string get_packing(string item_code, string PurchNumber)
        {
            string packing = model.get_packing(item_code,PurchNumber);
            return packing;
        }
        public DataTable Get_unites(string Item_Id, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dtb = inv_model.Get_unites(Item_Id, con, trans);
            return dtb;
        }
        public DataTable Get_unites(string Item_Id)
        {
            DataTable dtb = inv_model.Get_unites(Item_Id);
            return dtb;
        }
        public DataTable load_purchaenum(string purchaseno)
        {
            DataTable dtb = model.load_purchaenum(purchaseno);
            return dtb;
        }
        public DataTable load_purch_invno_aenum(string purchaseno)
        {
            DataTable dtb = model.load_purch_invno_aenum(purchaseno);
            return dtb;
        }
        public DataTable Load_pur_details(string pur_no)
        {
            DataTable dtb = model.Load_pur_details(pur_no);
            return dtb;
        }
        public DataTable Load_pur_details_on_invoice(string pur_no)
        {
            DataTable dtb = model.Load_pur_details_on_invoice(pur_no);
            return dtb;
        }
        public DataTable get_Loaditems_details(string itemcd1, string pur_no)
        {
            DataTable dtb = model.get_Loaditems_details(itemcd1, pur_no);
            return dtb;
        }
        public DataTable get_unit_details(string dtitems)
        {
            DataTable dtb = model.get_unit_details(dtitems);
            return dtb;
        }
        public DataTable dt_get_stock(string itemcode, string pur_no)
        {
            DataTable dtb = model.dt_get_stock(itemcode, pur_no);
            return dtb;
        }
        public DataTable dt_get_batchpurchase(string itemcode, string pur_no)
        {
            DataTable dtb = model.dt_get_batchpurchase(itemcode, pur_no);
            return dtb;
        }
        public DataTable get_batchentry_qty(string pur_no, string batch1)
        {
            DataTable dtb = model.get_batchentry_qty(pur_no, batch1);
            return dtb;
        }
        public void save_purReturn(string ReturnDate, string PurchNumber, string PurchDate, string Sup_Code, string TotalAmount, string TotalCost, MySqlConnection con, MySqlTransaction trans)
        {
            model.save_purReturn(ReturnDate, PurchNumber, PurchDate, Sup_Code, TotalAmount, TotalCost, con, trans);
        }
        public string get_maxReturnNo(MySqlConnection con, MySqlTransaction trans)
        {
            string maxno = model.get_maxReturnNo(con, trans);
            return maxno;
        }
        public string get_unit2(string itemid, MySqlConnection con, MySqlTransaction trans)
        {
            string unit = model.get_unit2(itemid, con, trans);
            return unit;
        }
        //discount........bhj
        public void save_returnItems(string RetNumber, string item_code, string Qty,string unit, string Rate, string UNIT2, string FreeQty, string Gst, string Igst,string Discount, MySqlConnection con, MySqlTransaction trans)
        {
            model.save_returnItems(RetNumber, item_code, Qty,unit, Rate, UNIT2, FreeQty, Gst, Igst,Discount, con, trans);
        }
        public void upadte_batchnumber(decimal QTY, string itemcode, string batchnumbr, MySqlConnection con, MySqlTransaction trans)
        {
            model.upadte_batchnumber(QTY, itemcode, batchnumbr, con, trans);
        }
        public string get_returnqty(string pur_no, string itemcode, MySqlConnection con, MySqlTransaction trans)
        {
            string retqty = model.get_returnqty(pur_no, itemcode, con, trans);
            return retqty;
        }
        public void update_purchit(decimal retnew, string pur_no, string itemcode, MySqlConnection con, MySqlTransaction trans)
        {
            model.update_purchit(retnew, pur_no, itemcode, con, trans);
        }
        public string get_dte(string PurchNumber, string Item_Code, string batch, MySqlConnection con, MySqlTransaction trans)
        {
            string retqty = model.get_dte(PurchNumber, Item_Code, batch, con, trans);
            return retqty;
        }
        public void update_batchpurchase(decimal batchNewQty, string PurchNumber, string Item_Code, string BatchNumber, MySqlConnection con, MySqlTransaction trans)
        {
            model.update_batchpurchase(batchNewQty,  PurchNumber,  Item_Code,  BatchNumber,con,trans);
        }
        public DataTable get_companydetails()
        {
            DataTable dtb = inv_model.get_companydetails();
            return dtb;
        }
    }
}
