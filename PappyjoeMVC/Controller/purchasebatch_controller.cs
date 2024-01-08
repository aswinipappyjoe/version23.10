using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PappyjoeMVC.Model;
namespace PappyjoeMVC.Controller
{
    public class purchasebatch_controller
    {
        //purchasebatch_interface intr;
        Connection db = new Connection();
        //public purchasebatch_controller(purchasebatch_interface inttr)
        //{
        //    intr = inttr;
        //    intr.setcontroller(this);
        //}
        public DataTable check_batch(string item_code)
        {
            DataTable dt = db.table("select ISBatch,item_name from tbl_ITEMS where id='" + item_code + "'");
            return dt;
        }
        public DataTable get_batchrate_exp(string item,string batch)
        {
            DataTable dtb = db.table("select Item_Code,BatchNumber,batch_rate,batch_sales_rate,ExpDate,PrdDate from tbl_batchnumber where Item_Code= '" + item + "' and BatchNumber='" + batch + "'");
            return dtb;
        }
        public DataTable itemdetails(string itemid)
        {
            DataTable dtb = db.table("select Sales_Rate_Max,Unit1,Unit2,OneUnitOnly,UnitMF from tbl_ITEMS where id='" + itemid + "' ");
            return dtb;
        }
        public DataTable batchdetails(string batch, string itemid)
        {
            DataTable dtb = db.table("select Item_Code,BatchNumber,Unit2,batch_rate,batch_sales_rate,purch_unit2 from tbl_batchnumber where BatchNumber='" + batch + "' and Item_Code='" + itemid + "' ");
            return dtb;
        }
    }
}
