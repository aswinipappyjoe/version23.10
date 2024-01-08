using MySql.Data.MySqlClient;
using PappyjoeMVC.Model;
using System.Data;

namespace PappyjoeMVC.Controller
{
    public class Purchase_order_controller
    {
        purchase_order_model _model = new purchase_order_model();
        Inventory_model imodel = new Inventory_model();
        Common_model cmodel = new Common_model();
        public DataTable getrate(string ar)
        {
            DataTable dt= cmodel.getrate(ar);
            return dt;
        }
        public int save_log(string log_usrid, string log_type, string log_descriptn, string logdate, string logtime, string log_stage,string typeid)
        {
            int j = cmodel.save_log(log_usrid, log_type, log_descriptn, logdate, logtime, log_stage,typeid);
            return j;
        }
        public DataTable master_details(int purch_id1)
        {
            DataTable dtb = _model.master_details(purch_id1);
            return dtb;
        }
        public DataTable Load_item_details(int purch_id1)
        {
            DataTable dtb = _model.Load_item_details(purch_id1);
            return dtb;
        }
        public DataTable Doc_number()
        {
            DataTable dtb = _model.Doc_number();
            return dtb;
        }
        public DataTable Load_supplier_details(string name)
        {
            DataTable dtb = _model.Load_supplier_details(name);
            return dtb;
        }
        public DataTable Load_all_supplier()
        {
            DataTable dtb = _model.Load_all_supplier();
            return dtb;
        }
        public DataTable get_supplier_name(string sup_code)
        {
            DataTable dtb = _model.get_supplier_name(sup_code);
            return dtb;
        }
        public DataTable get_itemname(string Itemid)
        {
            DataTable dtb = _model.get_itemname(Itemid);
            return dtb;
        }
        public string max_purNo(string Itemid)
        {
            string maxid = _model.max_purNo(Itemid);
            return maxid;
        }
        public string purchit_details(string itemid)
        {
            string value = _model.purchit_details(itemid);
            return value;
        }
        public void save_master(string OrerNo, string OrderDate, string Supplierid, MySqlConnection con, MySqlTransaction trans)
        {
            _model.save_master(OrerNo, OrderDate, Supplierid, con, trans);
        }
        public void save_items(string OrerNo, string Item_Id, string description, string col_qty, string Unit_Cost, string Amount, MySqlConnection con, MySqlTransaction trans)
        {
            //_model.OrerNo = intr.OrerNo;
            _model.save_items(OrerNo, Item_Id, description, col_qty, Unit_Cost, Amount, con, trans);
        }
        public void delete_order(string orderno, MySqlConnection con, MySqlTransaction trans)
        {
            _model.delete_order(orderno, con, trans);
        }
        //public string max_pur_orderno()
        //{
        //    string pur_no = _model.max_pur_orderno();
        //    return pur_no;
        //}
        public DataTable companydetails()
        {
            DataTable dtb = imodel.get_companydetails();
            return dtb;
        }

    }
}
