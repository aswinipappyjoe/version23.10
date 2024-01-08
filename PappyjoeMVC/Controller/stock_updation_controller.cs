using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PappyjoeMVC.Model;
using System.Data;
using MySql.Data.MySqlClient;

namespace PappyjoeMVC.Controller
{
    class stock_updation_controller
    {
        stock_transfr_model mdl = new stock_transfr_model(); 
        sales_model smodel = new sales_model();
        Purchase_model pmodel = new Purchase_model();
        Supplier_model sup_model = new Supplier_model();
        Inventory_model inv_model = new Inventory_model();
        Daily_Invoice_Report_model dm = new Daily_Invoice_Report_model(); public DataTable Get_consume_tick()
        {
            DataTable dt = inv_model.Get_consume_tick();
            return dt;
        }
        public DataTable get_itemdata(string type)
        {
            DataTable dtb = mdl.get_itemdata(type);
            return dtb;
        }
        public DataTable get_itemdata_Stock_updation(string type)
        {
            DataTable dtb = mdl.get_itemdata_Stock_updation(type);
            return dtb;
        }
        public DataTable stock_adjustmnt_showmore(string type, int count)
        {
            DataTable dtb = mdl.stock_adjustmnt_showmore(type, count);
            return dtb;
        }
        public DataTable Stock_updation_todays_adjustmnt(string date)
        {
            DataTable dtb = mdl.Stock_updation_todays_adjustmnt(date);
            return dtb;
        }
        public DataTable get_itemdata_Stock_updation_limit(string type)
        {
            DataTable dtb = mdl.get_itemdata_Stock_updation_limit(type);
            return dtb;
        }
        public DataTable get_itemdata_Stock_updation_all_dataLoad(string type)
        {
            DataTable dtb = mdl.get_itemdata_Stock_updation_all_dataLoad(type);
            return dtb;
        }
        public DataTable Stock_updation_search_wit_itemcode(string name,string type)
        {
            DataTable dtb = mdl.Stock_updation_search_wit_itemcode(name,type);
            return dtb;
        }
        public DataTable dt_units(string id)
        {
            DataTable dtb = mdl.dt_units(id);
            return dtb;
        }
        public DataTable itemdetails(string itemid)
        {
            DataTable dtb = mdl.itemdetails(itemid);
            return dtb;
        }
        public DataTable batch_stock(string ItemId, string batch)
        {
            DataTable dtb = mdl.batch_stock(ItemId, batch);
            return dtb;
        }
        public DataTable get_pur_gst(string name)
        {
            DataTable dtb = smodel.get_pur_gst(name);
            return dtb;
        }
        public DataTable stock_updation_docnumber()
        {
            DataTable dtb = mdl.stock_updation_docnumber();
            return dtb;
        }
        public DataTable Get_stock(string itemid)
        {
            DataTable dtb = smodel.Get_stock(itemid);
            return dtb;
        }
        public void stock_update_main_save(string refno, string date,  string action, decimal amount)
        {
            mdl.stock_update_main_save(refno, date, action, amount);
        }
        public string maxid_stockupdation(MySqlConnection con, MySqlTransaction trans)
        {
            string id = mdl.maxid_stockupdation(con,trans);
            return id;
        }
        public int update_stockOut(string qty, string entryno,MySqlConnection con,MySqlTransaction trans)
        {
            int i = mdl.update_stockOut(qty, entryno,con,trans);
            return i;
        }
        public void save_stockupdate(string date, string Action, string ItemId, string ItemName, string Batch, string Unit, string Given_Qty, string Cost, string entry, string last_qty,MySqlConnection con,MySqlTransaction trans )
        {
            mdl.save_stockupdate(date, Action, ItemId, ItemName, Batch, Unit, Given_Qty, Cost, entry, last_qty,con,trans);
        }
        public DataTable get_Supliermaxid(MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dtb = mdl.get_Supliermaxid(con,trans);
            return dtb;
        }
        public DataTable get_suppliername(string name, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dtb = mdl.get_suppliername(name,con,trans);
            return dtb;
        }
        public int Save(string _code, string _name, string _Cname, string _phone, string _phone2, string _email, string _fax, string _web, string _address1, string _address2, string _address3, string _balance,string status)
        {
            int i = sup_model.save(_code, _name, _Cname, _phone, _phone2, _email, _fax, _web, _address1, _address2, _address3, _balance,status,"0","0");
            return i;
        }
        public DataTable trans_incrementDocnumber(MySqlConnection con,MySqlTransaction trans)
        {
            DataTable dtb = pmodel.trans_incrementDocnumber(con,trans);
            return dtb;
            //intr.DocNumber_increment(dtb);
        }
        public int save_purchase(string PurchNumber, string InvNumber, string PurchDate, string Sup_Code, string TotalAmount, string GrandTotal, string DiscPercentage, string DiscAmount, string TotalCost, string status,string type,MySqlConnection con,MySqlTransaction trans )
        {
            int i = pmodel.save_purchase(PurchNumber, InvNumber, PurchDate, Sup_Code, TotalAmount, GrandTotal, DiscPercentage, DiscAmount, TotalCost, status,type,"Cash",con,trans );
            return i;
        }
        public DataTable get_item_unitmf(string id, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dtb = mdl.get_item_unitmf(id,con,trans);
            return dtb;
        }
        public void save_purchaseit(string purno, string date, string Item_Code, string Desccription, string barcode, string Packing, string Unit, string Qty, int FreeQty, string Rate, string Amount, string UNIT2, string UnitMF, decimal GST, decimal IGST,MySqlConnection con,MySqlTransaction trans )
        {
            pmodel.save_purchaseit(purno, date, Item_Code, Desccription, barcode, Packing,"0", Unit, Qty, FreeQty, Rate, Amount, UNIT2, UnitMF, GST, IGST,con,trans );
        }
        public int save_batchNumber(string Item_Code, string BatchNumber, int Qty, string Unit2, string UnitMF, string PurchNumber, string PrdDate, string ExpDate, string Period, string Sup_Code, string PurchDate, string IsExpDate,string product_type,MySqlConnection con,MySqlTransaction trans)
        {
            int i = pmodel.save_batchNumber(Item_Code, BatchNumber, Qty,0,0, Unit2, Unit2, UnitMF, PurchNumber, PrdDate, ExpDate, Period, Sup_Code, PurchDate, IsExpDate, product_type,con,trans);
            return i;
        }
        public DataTable get_maxEntryNo(MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dtb = pmodel.get_maxEntryNo(con,trans);
            return dtb;
        }
        public void save_batchpurchase(string PurchNo, string Purc_Date, string Sup_Code, string Item_Code, string BatchNumber, decimal Qty, string Unit2, string UnitMF, string PrdDate, string ExpDate, string IsExpDate, string BatchEntry,MySqlConnection con,MySqlTransaction trans)
        {
            pmodel.save_batchpurchase(PurchNo, Purc_Date, Sup_Code, Item_Code, BatchNumber, Qty,0, Unit2, UnitMF, PrdDate, ExpDate, IsExpDate, BatchEntry,con,trans );
        }
        public DataTable get_itemName_cost(string refno, string purno)
        {
            DataTable dtb = mdl.get_itemName_cost(refno, purno);
            return dtb;
        }
        public DataTable get_batchEntry(string refno, string purno)
        {
            DataTable dtb = mdl.get_batchEntry(refno, purno);
            return dtb;
        }
        public DataTable check_purno(string purno, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dtb = mdl.check_purno(purno,con,trans);
            return dtb;
        }
        //report
        public DataTable stock_updation_dataload(string date1,string date2)
        {
            DataTable dtb = mdl.stock_updation_dataload(date1, date2);
            return dtb;
        }
        public DataTable stock_updation_dataload_on_action(string date1, string date2, string action)
        {
            DataTable dtb = mdl.stock_updation_dataload_on_action(date1, date2, action);
            return dtb;
        }
        //list
        public DataTable get_stockupdate_details(string date1, string date2)
        {
            DataTable dtb = mdl.get_stockupdate_details(date1, date2);
            return dtb;
        }
        public DataTable get_stockupdation_main_details(string refno)
        {
            DataTable dtb = mdl.get_stockupdation_main_details(refno);
            return dtb;
        }
        public DataTable get_stockupdate_details(string refno)
        {
            DataTable dtb = mdl.get_stockupdate_details(refno);
            return dtb;
        }
        public DataTable get_itemcode(string refno)
        {
            DataTable dtb = mdl.get_itemcode(refno);
            return dtb;
        }
        public DataTable get_itemcode_frm_items(string ItemId)
        {
            DataTable dtb = mdl.get_itemcode_frm_items(ItemId);
            return dtb;
        }
        public DataTable trans_get_batchdetails(string ItemCode,MySqlConnection con,MySqlTransaction trans)
        {
            DataTable dtb = mdl.trans_get_batchdetails(ItemCode,con,trans);
            return dtb;
        }
        
        public DataTable get_stock_of_items(string ItemId, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dtb = mdl.get_stock_of_items(ItemId,con,trans);
            return dtb;
        }
        public DataTable get_stock_of_items(string ItemId)
        {
            DataTable dtb = mdl.get_stock_of_items(ItemId);
            return dtb;
        }
        public DataTable practicedetails()
        {
            DataTable dt = dm.practicedetails();
            return dt; ;
        }
    }
}
