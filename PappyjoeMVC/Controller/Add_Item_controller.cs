﻿using PappyjoeMVC.Model;
using System.Data;
namespace PappyjoeMVC.Controller
{
    public class Add_Item_controller
    {
        Add_Item_model _Model = new Add_Item_model();
        Common_model cmodel = new Common_model(); Purchase_model pmodel = new Purchase_model();
        Inventory_model inv_model = new Inventory_model();
        public DataTable fill_category()
        {
            DataTable dtb = _Model.fill_category();
            return dtb;
        }
        public DataTable Get_consume_tick()
        {
            DataTable dt = inv_model.Get_consume_tick();
            return dt;
        }
        public DataTable fill_supplier()
        {
            DataTable dtb = _Model.fill_supplier();
            return dtb;

        }
        public void save(string name)
        {
            _Model.save(name);
        }
        public DataTable fill_manufacture()
        {
            DataTable dtb = _Model.fill_manufacture();
            return dtb;
        }
        public DataTable fill_unit()
        {
            DataTable dtb = cmodel.Fill_unit_combo();
            return dtb;
        }
        public int Save_data(string _itemname, string _itemcode, string _manufacture, string _category, string HSN_Number, string _location, string _packing, string _isbatch, string _Sales1, string _SalesMin, string _SalesMax, string _Purch_Rate, string _punit, string _sUnit, int _unitmf, string _Purch_Rate2, string _Sales2, string _SalesMin1, string _SalesMax1, string _isOneUnitOnly, string _ReorderQty, string _CostBase, string _istax, string _MinimumStock,string shelfno, string barcode,string op_stck,string sup_code,string product_type,string gstvat,string op_date)
        {
            int i = _Model.Save_data(_itemname, _itemcode, _manufacture, _category, HSN_Number, _location, _packing, _isbatch, _Sales1, _SalesMin, _SalesMax, _Purch_Rate, _punit, _sUnit, _unitmf, _Purch_Rate2, _Sales2, _SalesMin1, _SalesMax1, _isOneUnitOnly, _ReorderQty, _CostBase, _istax, _MinimumStock, shelfno, barcode, op_stck,sup_code, product_type, gstvat, op_date);
            return i;
        }
        public int save_batchNumber(string Item_Code, string BatchNumber, int Qty,decimal rate,decimal sales_rate, string Unit2, string pur_unit2, string UnitMF, string PurchNumber, string PrdDate, string ExpDate, string Period, string Sup_Code, string PurchDate, string IsExpDate,string product_type)
        {
            int i = pmodel.save_batchNumber(Item_Code, BatchNumber, Qty,rate, sales_rate, Unit2, pur_unit2, UnitMF, PurchNumber, PrdDate, ExpDate, Period, Sup_Code, PurchDate, IsExpDate, product_type);
            return i;
        }
        public int update_batchNumber(string Item_Code, string BatchNumber, int Qty,decimal rate,decimal sales_rate, string Unit2, string pur_unit2, string UnitMF, string PurchNumber, string PrdDate, string ExpDate, string Period, string Sup_Code, string PurchDate, string IsExpDate, string entryno, string product_type)

        {
            int i = pmodel.update_batchNumber(Item_Code, BatchNumber, Qty,rate, sales_rate, Unit2, pur_unit2, UnitMF, PurchNumber, PrdDate, ExpDate, Period, Sup_Code, PurchDate, IsExpDate, entryno, product_type);
            return i;
        }
        public int update_data(int id, string _itemname, string _itemcode, string _manufacture, string _category, string _location, string _packing, string _isbatch, string _Sales1, string _SalesMin, string _SalesMax, string _Purch_Rate, string _punit, string _sUnit, int _unitmf, string _Purch_Rate2, string _Sales2, string _SalesMin1, string _SalesMax1, string _isOneUnitOnly, string _ReorderQty, string _CostBase, string _istax, string _MinimumStock, string shelfno, string barcode,string open_stock,string sup_code,string product_type,string gstvat,string op_date)
        {
            int i = _Model.update_data(id, _itemname, _itemcode, _manufacture, _category, _location, _packing, _isbatch, _Sales1, _SalesMin, _SalesMax, _Purch_Rate, _punit, _sUnit, _unitmf, _Purch_Rate2, _Sales2, _SalesMin1, _SalesMax1, _isOneUnitOnly, _ReorderQty, _CostBase, _istax, _MinimumStock, shelfno, barcode, open_stock,sup_code, product_type, gstvat, op_date);
            return i;
        }
        public DataTable check_have_batch(string Item_Code, string BatchNumber)
        {
            DataTable dtb = _Model.check_have_batch(Item_Code, BatchNumber);
            return dtb;
        }
        public DataTable load_itemcode()
        {
            DataTable dtb = _Model.load_itemcode();
            return dtb;
        }
        public DataTable Get_Item_unitmf(string id)
        {
            DataTable dtb = _Model.Get_Item_unitmf(id);
            return dtb;
        }
        public string get_PurchNumber(string id)
        {
            string dtb = _Model.get_PurchNumber(id);
            return dtb;
        }
        public DataTable get_tbl_PURCHIT_details(string id)
        {
            DataTable dtb = _Model.get_tbl_PURCHIT_details(id);
            return dtb;
        }
        public DataTable Get_PURCHIT_wit_itemid(string id)
        {
            DataTable dtb = _Model.get_tbl_PURCHIT_details(id);
            return dtb;
        }
        public DataTable get_batch_details(string itemid)
        {
            DataTable dtb = _Model.get_batch_details(itemid);
            return dtb;
        }
        public string get_max_itemid()
        {
            string maxId = _Model.get_max_itemid();
            return maxId;
        }
        public void savedrugtable(string itemid, string _itemname, string _type, string _strength, string _strength_gr, string _instructions)
        {
            _Model.savedrugtable(itemid, _itemname, _type, _strength, _strength_gr, _instructions);
        }
        public string get_drugid(string Item_Id)
        {
            string dtb = _Model.get_drugid(Item_Id);
            return dtb;
        }
        public void update_drug(string itemid, string _itemname, string _type, string _strength, string _strength_gr, string _instructions)
        {
            _Model.update_drug(itemid, _itemname, _type, _strength, _strength_gr, _instructions);
        }
        public DataTable select_id_drug(string _itemname, string _type, string _strength, string _strength_gr, string _instructions)
        {
            DataTable dt = _Model.select_id_drug(_itemname, _type, _strength, _strength_gr, _instructions);
            return dt;
        }
        public void update(string Item_Id, string id, string _itemname, string _type, string _strength, string _strength_gr, string _instructions)
        {
            _Model.update(Item_Id, id, _itemname, _type, _strength, _strength_gr, _instructions);
        }
        public void update_inventryid(string Item_Id)
        {
            _Model.update_inventryid(Item_Id);
        }
        public DataTable get_drugdetails(int Item_Id)
        {
            DataTable dtb = _Model.get_drugdetails(Item_Id);
            return dtb;
        }
        public DataTable fill_drugtype()
        {
            DataTable dtb = _Model.fill_drugtype();
            return dtb;
        } 
        public DataTable max_itemid()
        {
            DataTable dtb = _Model.max_itemid();
            return dtb;
        }
        public int save_log(string log_usrid, string log_type, string log_descriptn,string logdate, string logtime, string log_stage,string typeid)
        {
            int j = cmodel.save_log(log_usrid, log_type, log_descriptn, logdate, logtime,log_stage, typeid);
            return j;
        }
    }
}
