using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PappyjoeMVC.Model;
using System.Data;
namespace PappyjoeMVC.Controller
{
   public class stock_controller
    {
        Common_model cmodel = new Common_model();
        Connection db = new Connection();
        Receipt_Model Model = new Receipt_Model();
        StockItem_model model = new StockItem_model(); 
        Inventory_model inv_model = new Inventory_model();
        Reports_model rmodel = new Reports_model();
        public DataTable getsupplierid(string supplier)//start sanoop
        {
            DataTable dt = new DataTable();
            DataTable clinicname = model.getsupplierid(supplier);
            return clinicname;
        }//end sanoop
        public string Load_CompanyName()
        {
            string clinicname = cmodel.Load_CompanyName();
            return clinicname;
        }
        public DataTable get_printSettings()
        {
            DataTable print = Model.get_printSettings();
            return print;
        }
        public DataTable Get_practiceDlNumber()
        {
            DataTable dt = cmodel.Get_practiceDlNumber();
            return dt;
        }
        public string Get_DoctorName(string doctor_id)
        {
            string dtb = cmodel.Get_DoctorName(doctor_id);
            return dtb;
        }
        public DataTable LoadSupplier()
        {
            DataTable gp_rs = model.LoadSupplier();
            return gp_rs;
        }
        public DataTable load_stock(string type)
        {
            DataTable dt_stock = model.load_stock(type);
            return dt_stock;
        }
        public DataTable load_fullstock(int count)
        {
            DataTable dt_stock = model.load_fullstock(count);
            return dt_stock;
        }
        public DataTable dt_get_count(string type)
        {
            DataTable dt_stock = model.dt_get_count(type);
            return dt_stock;
        }
        public DataTable minimumStock(string item_code)
        {
            DataTable dtb_Min = model. minimumStock(item_code);
            return dtb_Min;
        }
        public DataTable itemdetails(string item_code)
        {
            DataTable dtunit = model.itemdetails(item_code);
            return dtunit;
        }
        public DataTable get_batch_rate(string item_code)
        {
            DataTable dtunit = model.get_batch_rate(item_code);
            return dtunit;
        }
        public DataTable get_batch_sale_rate(string item_code)
        {
            DataTable dtunit = model.get_batch_sale_rate(item_code);
            return dtunit;
        }
        public DataTable get_sales_rate(string item_code, string batch)
        {
            DataTable dtunit = model.get_sales_rate(item_code, batch);
            return dtunit;
        }
        public DataTable get_lst_pur_rate(string item_code)
        {
            DataTable dtunit = model.get_lst_pur_rate(item_code);
            return dtunit;
        }
        public DataTable get_lst_sales_rate(string item_code)
        {
            DataTable dtunit = model.get_lst_sales_rate(item_code);
            return dtunit;
        }
        public DataTable search_minimum(string type)
        {
            DataTable sqlstr = model.search_minimum(type);
            return sqlstr;
        }
        public DataTable search_minium_wit_itemname(string search)
        {
            DataTable sqlstr = model.search_minium_wit_itemname(search);
            return sqlstr;
        }
        public DataTable get_supcode(string sup)
        {
            DataTable dt_sup = model.get_supcode(sup);
            return dt_sup;
        }
        public DataTable Load_supplier_items(string Sup_Code, string type)
        {
            DataTable dtb = model.Load_supplier_items(Sup_Code,type);
            return dtb;
        }
        public DataTable Patient_search(string patid)
        {
            DataTable dtb = cmodel.Patient_search(patid);
            return dtb;
        }
        public string doctr_privillage_for_addnewPatient(string doctor_id)
        {
            string dtb = cmodel.doctr_privillage_for_addnewPatient(doctor_id);
            return dtb;
        }
        public string permission_for_settings(string doctor_id)
        {
            string dtb = cmodel.permission_for_settings(doctor_id);
            return dtb;
        }
        public DataTable get_company_details()
        {
            DataTable dtb = cmodel.get_company_details();
            return dtb;
        }
        public DataTable Get_CompanyNAme()
        {
            DataTable dtb = cmodel.Get_CompanyNAme();
            return dtb;
        }
        public DataTable receipt_printSettings()
        {
            DataTable print = Model.receipt_printSettings();
            return print;
        }
        public string server()
        {
            string server = db.server();
            return server;
        }
        public DataTable load_stock_lil(int count)
        {
            DataTable dt_stock = model.load_stock_lil(count);
            return dt_stock;
        }
        public DataTable Get_consume_tick()
        {
            DataTable dt = inv_model.Get_consume_tick();
            return dt;
        }
        public DataTable get_batch_stock(string itemcode)
        {
            DataTable dt = rmodel.get_batch_stock(itemcode);
            return dt;
        }
        //bhj
        public DataTable exp_prd()
        {
            DataTable dt = model.exp_prd();
            return dt;

        }
        public DataTable consumables()
        {
            DataTable dt = model.consumables();
            return dt;

        }
    }
}
