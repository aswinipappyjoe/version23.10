using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PappyjoeMVC.Model;
using System.Data;

namespace PappyjoeMVC.Controller
{
   public class stocktransfer_batch_controller
    {
        stock_transfr_model mdl = new stock_transfr_model();
        sales_model smodel = new sales_model();
        public DataTable get_batchdetails(string ItemCode)
        {
            DataTable dtb = mdl.get_batchdetails(ItemCode);
            return dtb;
        }
        public DataTable get_batchdetails_stock_out(string ItemCode)
        {
            DataTable dtb = mdl.get_batchdetails_stock_out(ItemCode);
            return dtb;
        }
        public DataTable get_item_unitmf(string Itemid)
        {
            DataTable dtb = mdl.get_item_unitmf(Itemid);
            return dtb;
        }

        //stock in
        public DataTable check_batch(string item_code)
        {
            DataTable dtb = mdl.check_batch(item_code);
            return dtb;
        }
    }
}
