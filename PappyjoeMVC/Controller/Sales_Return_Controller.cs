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
   public class Sales_Return_Controller
    {
        Sales_Return_Model model = new Sales_Return_Model();
        Inventory_model invmodel = new Inventory_model();
        Common_model cmodel = new Common_model();
        public DataTable get_all_invnumbr(string invnumbr)
        {
            DataTable dtb = model. get_all_invnumbr(invnumbr);
            return dtb; 
        }
        public DataTable get_master_sales_details(string value)
        {
            DataTable dtb = model. get_master_sales_details(value);
            return dtb;
        }
        public DataTable itemdetails_from_salesit(string InvNumber)
        {
            DataTable dtb = model.itemdetails_from_salesit(InvNumber);
            return dtb;
        }
        public DataTable itemdetails_from_items(string ItemCode_From_List)
        {
            DataTable dtb = model.itemdetails_from_items(ItemCode_From_List);
            return dtb;
        }
        public DataTable get_sales_items_details(string value, string ItemCode)
        {
            DataTable dtb = model.get_sales_items_details(value, ItemCode);
            return dtb;
        }
        public string get_maxRetnumber(string invnumbr, string itemcode)
        {
            string dtb = model.get_maxRetnumber(invnumbr, itemcode);
            return dtb;
        }
        public string get_totalqty(string InvoiceNum, string ItemCode)
        {
            string dtb = model.get_totalqty(InvoiceNum, ItemCode);
            return dtb;
        }
        public string ubit2_from_retit(string ItemCode, string retnumber)
        {
            string dtb = model.ubit2_from_retit(ItemCode, retnumber);
            return dtb;
        }
        public DataTable get_salesdetails(string InvNumber,MySqlConnection con,MySqlTransaction trans)
        {
            DataTable dtb = model.get_salesdetails(InvNumber,con,trans);
            return dtb;
        }
        public int save_master(string ReturnDate, string InvNumber, string InvDate, string cust_number, string cust_name, string TotalAmount, string User_Name, string Paid, decimal GST, string IGST, MySqlConnection con, MySqlTransaction trans)
        {
            int i = model.save_master(ReturnDate,  InvNumber,  InvDate,  cust_number,  cust_name,  TotalAmount,  User_Name,  Paid,  GST,  IGST, con, trans);
            return i;
        }
        public string max_retnumber(MySqlConnection con, MySqlTransaction trans)
        {
            string maxretunmber = model.max_retnumber( con, trans);
            return maxretunmber;
        }
        public DataTable Get_unites(string Item_Id, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dtb = invmodel.Get_unites(Item_Id, con, trans);
            return dtb;
        }
        public DataTable Get_unites(string Item_Id)
        {
            DataTable dtb = invmodel.Get_unites(Item_Id);
            return dtb;
        }
        public void save_returnitems(string RetNumber, string Item_Code, string Qty,string unit, string Rate, string UNIT2, string FreeQty, MySqlConnection con, MySqlTransaction trans)
        {
            model.save_returnitems(RetNumber,  Item_Code,  Qty,unit,  Rate,  UNIT2,  FreeQty, con, trans);
        }
        public string get_sales_retqty(string invoicenumbr, string itemcode, MySqlConnection con, MySqlTransaction trans)
        {
            string dtb = model.get_sales_retqty(invoicenumbr, itemcode,con, trans);
            return dtb;
        }
        public DataTable get_sales_qty(string returnnumber, string itemcode, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dtb = model.get_sales_qty(returnnumber, itemcode,con,trans);
            return dtb;
        }
        public void update_sales_retqty(decimal RetQty, string InvNumber, string itemcode, MySqlConnection con, MySqlTransaction trans)
        {
            model.update_sales_retqty(RetQty,InvNumber,itemcode, con, trans);
        }
        public string retqty_from_batchsale(string entryno, MySqlConnection con, MySqlTransaction trans)
        {
            string retqty = model.retqty_from_batchsale(entryno, con, trans);
            return retqty;
        }
        public void update_batchsale(decimal bat_RetQty, string Entry_No, MySqlConnection con, MySqlTransaction trans)
        {
            model.update_batchsale(bat_RetQty,Entry_No, con, trans);
        }
        public int update_batchnumber(string qty, string Entry_No, MySqlConnection con, MySqlTransaction trans)
        {
            int i = model.update_batchnumber(qty,Entry_No, con, trans);
            return i;
        }
        public int save_log(string log_usrid, string log_type, string log_descriptn, string logdate, string logtime, string log_stage,string typeid)
        {
            int j = cmodel.save_log(log_usrid, log_type, log_descriptn, logdate, logtime, log_stage, typeid);
            return j;
        }
        public DataTable get_details_from_items(string itemid)
        {
            DataTable dtb = model.get_details_from_items(itemid);
            return dtb;
        }
        public DataTable get_companydetails()
        {
            DataTable dtb = invmodel.get_companydetails();
            return dtb;
        }
        public string sum_qty_from_sales(string InvNumber, string Item_Code)
        {
            string qty = model.sum_qty_from_sales(InvNumber,Item_Code);
            return qty;
        }
        public DataTable load_return_master(int Ret_Numbr)
        {
            DataTable dtb = model.load_return_master(Ret_Numbr);
            return dtb;
        }
        public DataTable load_return_items(int Ret_Numbr)
        {
            DataTable dtb = model.load_return_items(Ret_Numbr);
            return dtb;
        }
    }
}
