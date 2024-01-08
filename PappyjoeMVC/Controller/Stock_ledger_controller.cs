using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PappyjoeMVC.Model;
using System.Data;

namespace PappyjoeMVC.Controller
{
   public  class Stock_ledger_controller
    {
        Stock_ledger_model model = new Stock_ledger_model();
        public DataTable load_itemcode(string itemcode)
        {
            DataTable dtb = model.load_itemcode(itemcode);
            return dtb;
        }
        public DataTable get_items_from_batch(string item)
        {
            DataTable dtb = model.get_items_from_batch(item);
            return dtb;
        }
        public DataTable  get_opening_stock(string item)
        {
            DataTable dtb = model.get_opening_stock(item);
            return dtb;
        }
        public DataTable get_purchase_from_to(string from, string to, string item)
        {
            DataTable dtb = model.get_purchase_from_to(from,to,item);
            return dtb;
        }
        public DataTable get_sales_from_to(string from, string to, string item)
        {
            DataTable dtb = model.get_sales_from_to(from, to, item);
            return dtb;
        }
        public DataTable get_purchase_return_from_to(string from, string to, string item)
        {
            DataTable dtb = model.get_purchase_return_from_to(from, to, item);
            return dtb;
        }
        public DataTable get_sales_return_from_to(string from, string to, string item)
        {
            DataTable dtb = model.get_sales_return_from_to(from, to, item);
            return dtb;
        }
        public DataTable get_stock_adjuctment_from_to(string from, string to, string item)
        {
            DataTable dtb = model.get_stock_adjuctment_from_to(from, to, item);
            return dtb;
        }
        public DataTable get_stock_transfer_from_to(string from, string to, string item)
        {
            DataTable dtb = model.get_stock_transfer_from_to(from, to, item);
            return dtb;
        }
        public DataTable get_stock_in_from_to(string from, string to, string item)
        {
            DataTable dtb = model.get_stock_in_from_to(from, to, item);
            return dtb;
        }
        public DataTable get_purchase_before(string from, string item)
        {
            DataTable dtb = model.get_purchase_before(from, item);
            return dtb;
        }
        public DataTable get_sales_before(string from, string item)
        {
            DataTable dtb = model.get_sales_before(from, item);
            return dtb;
        }
        public DataTable get_purchase_return_before(string from, string item)
        {
            DataTable dtb = model.get_purchase_return_before(from, item);
            return dtb;
        }
        public DataTable get_sales_return_before(string from, string item)
        {
            DataTable dtb = model.get_sales_return_before(from, item);
            return dtb;
        }
        public DataTable get_stock_adjuctment_before(string from, string item)
        {
            DataTable dtb = model.get_stock_adjuctment_before(from, item);
            return dtb;
        }
        public DataTable get_stock_transfer_before(string from, string item)
        {
            DataTable dtb = model.get_stock_transfer_before(from, item);
            return dtb;
        }
        public DataTable get_stock_in_before(string from, string item)
        {
            DataTable dtb = model.get_stock_in_before(from, item);
            return dtb;
        }
        // all items
        public DataTable get_allitems_from_batch(string from, string to,string stock_type)
        {
            DataTable dtb = model.get_allitems_from_batch(from,to, stock_type);
            return dtb;
        }
        public DataTable companydetails()
        {
            DataTable dtb = model.companydetails();
            return dtb;
        }
    }

}
