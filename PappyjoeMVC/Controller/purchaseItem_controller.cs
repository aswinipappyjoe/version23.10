using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PappyjoeMVC.Model;
using System.Data;
namespace PappyjoeMVC.Controller
{
    public class purchaseItem_controller
    {
        Inventory_model imodel = new Inventory_model();
        public DataTable LoadItems(string type)
        {
            DataTable dt = imodel.LoadItems(type);
            return dt;
        }
        public DataTable Load_itemcode_details(string item_,string type)
        {
            DataTable dt = imodel.Load_itemcode_details(item_,type);
            return dt;
        }

        public DataTable Search(string item, string type)
        {
            DataTable dtb = imodel.Search(item, type);
            return dtb;
        }
        public string get_itemid(string Item_Code, string type)
        {
            string id = imodel.get_itemid(Item_Code, type);
            return id;
        }
        public DataTable Get_consume_tick()
        {
            DataTable dt = imodel.Get_consume_tick();
            return dt;
        }
        public DataTable Load_items_qty(string item)
        {
            DataTable dt = imodel.Load_items_qty(item);
            return dt;
        }
        public DataTable Load_manufactr(string item)
        {
            DataTable dt = imodel.Load_manufactr(item);
            return dt;
        }
    }
}
