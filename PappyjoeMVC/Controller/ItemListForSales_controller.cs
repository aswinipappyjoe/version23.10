using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PappyjoeMVC.Model;
namespace PappyjoeMVC.Controller
{
    public class ItemListForSales_controller
    {
        Connection db = new Connection();
        Inventory_model inv_model = new Inventory_model();
        public DataTable Load_items_wit_itemcode(string Item_Code)
        {
            DataTable dt = inv_model.Load_items_wit_itemcode(Item_Code);
            return dt;
        }
        public DataTable Get_consume_tick()
        {
            DataTable dt = inv_model.Get_consume_tick();
            return dt;
        }
        public string get_itemid(string Item_Code, string type)
        {
            string str = inv_model.get_itemid(Item_Code, type);
            return str;
        }
        public DataTable get_itemName(string Item_Code, string type)
        {
            DataTable dt = inv_model.get_itemName(Item_Code,type);
            return dt;
        }
        public DataTable Load_items(string type)
        {
            DataTable dt = inv_model.Load_items(type);
            return dt;
        }
        public DataTable Load_items_qty(string item)
        {
            DataTable dt = inv_model.Load_items_qty(item);
            return dt;
        }
        public DataTable dt_cat(string Cat_Number)
        {
            DataTable dt = inv_model.dt_cat(Cat_Number);
            return dt;
        }
        public DataTable each_item_details(string item_code, string type)
        {
            DataTable dt = inv_model.each_item_details(item_code,type);
            return dt;
        }
        public DataTable search_wit_itemcode(string name)
        {
            DataTable dtb = inv_model.search_wit_itemcode(name);
            return dtb;
        }
        public string check_batch(string item_code)
        {
            string dt = inv_model.check_batch(item_code);
            return dt;
        }
    }
}
