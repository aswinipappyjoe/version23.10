using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PappyjoeMVC.Model;
namespace PappyjoeMVC.Controller
{
   public class batchsale_controller
    {
        Connection db = new Connection();
        Inventory_model inv_model = new Inventory_model();
        public DataTable get_batchdetails(string ItemCode)
        {
            DataTable dtb = inv_model.get_batchdetails(ItemCode);
            return dtb;
        }
        public DataTable get_item_frm_purchase(string itemcode)
        {
            DataTable dtb = inv_model.get_item_frm_purchase(itemcode);
            return dtb;

        }
        public DataTable get_batch_wise_rate(string batch, string itemcode)
        {
            DataTable dtb = inv_model.get_batch_wise_rate(batch,itemcode);
            return dtb;
        }
        public DataTable get_batchdetails_opening(string ItemCode)
        {
            DataTable dtb = inv_model.get_batchdetails_opening(ItemCode);
            return dtb;

        }
        public DataTable get_item_unitmf(string Itemid)
        {
            DataTable dtb = inv_model.get_item_unitmf(Itemid);
            return dtb;
        }
        public DataTable itemdetails(string itemid)
        {
            DataTable dtb = inv_model.itemdetails(itemid);
            return dtb;
        }
        public DataTable get_unit_wise_rate(string itemid, string unit)
        {
            DataTable dtb = inv_model.get_unit_wise_rate(itemid,unit);
            return dtb;
        }
        public DataTable get_item_salesrate_minimun(string itemid)
        {
            DataTable dtb = inv_model.get_item_salesrate_minimun(itemid);
            return dtb;
        }
        public DataTable get_item_salesrate(string itemid)
        {
            DataTable dtb = inv_model.get_item_salesrate(itemid);
            return dtb;
        }

    }
    
}
