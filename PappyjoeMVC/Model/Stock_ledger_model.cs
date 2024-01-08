using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace PappyjoeMVC.Model
{
  public  class Stock_ledger_model
    {
        Connection db = new Connection();
        public DataTable load_itemcode(string itemcode)
        {
            DataTable dtb = db.table("select item_code,item_name,id from tbl_items where (item_code  like '" + itemcode + "%' or item_name like '" + itemcode + "%') ");//and Product_type='Pharmacy'
            return dtb;
        }
      public DataTable get_items_from_batch(string item)
        {
            DataTable dtb = db.table("SELECT Entry_No,Item_Code,BatchNumber ,Qty,PurchNumber,Product_type,PurchDateFROM tbl_batchnumber WHERE Item_Code='"+item+ "' and Product_type='Pharmacy' ");
            return dtb;
        }
        public DataTable get_opening_stock(string item)
        {
            DataTable dtb =db.table("select id,item_code,item_name,Unit1,Unit2,UnitMF,opening_stock_date,Open_Stock from tbl_items where id='" + item + "'");
            return dtb;
        }

        public DataTable get_purchase_from_to(string from, string to,string item)
        {
            DataTable dtb = db.table("select i.PurchNumber,i.PurchDate,Item_Code,Desccription,Qty,Unit,FreeQty,Rate,Amount,UnitMF from tbl_purchit i inner join tbl_purchase p on i. PurchNumber=p.PurchNumber where i.PurchDate between '" + from + "' and '" + to + "'  and Item_Code='" + item + "' and p.Memo='Purchase'");
            return dtb;
        }
       
        public DataTable get_sales_from_to(string from, string to, string item)
        {
            DataTable dtb = db.table("select InvNumber,InvDate,Item_Code,Description,Qty,Unit,FreeQty,Rate,TotalAmount,UnitMF from tbl_saleit where InvDate between '" + from + "' and '" + to + "'  and Item_Code='" + item + "'");
            return dtb;
        }
        public DataTable get_purchase_return_from_to(string from, string to, string item)
        {
            DataTable dtb = db.table("select i.RetNumber,p.ReturnDate,p.PurchNumber,i.item_code,i.Qty,i.Unit,i.FreeQty,i.Rate,i.Gst from tbl_pretit i inner join tbl_preturn p on i. RetNumber=p.RetNumber where p.ReturnDate between '" + from + "' and '" + to + "'  and item_code='" + item + "'");
            return dtb;
        }
        public DataTable get_sales_return_from_to(string from, string to, string item)
        {
            DataTable dtb = db.table("select i.RetNumber,s.ReturnDate,s.InvNumber,Item_Code,Qty,Unit,FreeQty,Rate from tbl_retit i inner join tbl_return s on i.RetNumber=s.RetNumber  where s.ReturnDate between '" + from + "' and '" + to + "'  and Item_Code='" + item + "'");
            return dtb;
        }
        public DataTable get_stock_adjuctment_from_to(string from, string to, string item)
        {
            DataTable dtb = db.table("select id,updation_date,Action,ItemId,upate_qty,Unit,Cost from tbl_stock_updation  where updation_date between '" + from + "' and '" + to + "'  and ItemId='" + item + "'");
            return dtb;
        }
        public DataTable get_stock_transfer_from_to(string from, string to, string item)
        {
            DataTable dtb = db.table("select stock_date,Action,m.RefNo,ItemId,Given_Qty ,unit,Cost from  tbl_stock_out s inner join tbl_stock_out_main m on s.RefNo=m.RefNo where stock_date between '" + from + "' and '" + to + "'  and ItemId='" + item + "'");
            return dtb;
        }
        public DataTable get_stock_in_from_to(string from, string to, string item)
        {
            DataTable dtb = db.table("select stock_date,Action,m.RefNo,ItemId,buy_Qty,unit,Cost from  tbl_stock_in s inner join tbl_stock_in_main m on s.RefNo=m.RefNo where stock_date between '" + from + "' and '" + to + "'  and ItemId='" + item + "'");
            return dtb;
        }
        public DataTable get_purchase_before(string from, string item)
        {
            DataTable dtb = db.table("select i.PurchNumber,i.PurchDate,Item_Code,Desccription,Qty,Unit,FreeQty,Rate,Amount,UnitMF from tbl_purchit i inner join tbl_purchase p on i. PurchNumber=p.PurchNumber where i.PurchDate < '" + from + "'  and Item_Code='" + item + "' and p.Memo='Purchase'");
            return dtb;
        }
        public DataTable get_sales_before(string from, string item)
        {
            DataTable dtb = db.table("select InvNumber,InvDate,Item_Code,Description,Qty,Unit,FreeQty,Rate,TotalAmount,UnitMF from tbl_saleit where InvDate < '" + from + "'  and Item_Code='" + item + "'");
            return dtb;
        }
        public DataTable get_purchase_return_before(string from,string item)
        {
            DataTable dtb = db.table("select i.RetNumber,p.ReturnDate,i.item_code,i.Qty,i.Unit,i.FreeQty,i.Rate,i.Gst from tbl_pretit i inner join tbl_preturn p on i. RetNumber=p.RetNumber where p.ReturnDate < '" + from + "'  and item_code='" + item + "'");
            return dtb;
        }
        public DataTable get_sales_return_before(string from, string item)
        {
            DataTable dtb = db.table("select i.RetNumber,s.ReturnDate,Item_Code,Qty,Unit,FreeQty,Rate from tbl_retit i inner join tbl_return s on i.RetNumber=s.RetNumber  where s.ReturnDate < '" + from + "'  and Item_Code='" + item + "'");
            return dtb;
        }

        //all stock
        public DataTable get_allitems_from_batch(string from, string to,string stock_type)
        {
            DataTable dtb = db.table("SELECT distinct Item_Code FROM tbl_batchnumber WHERE PurchDate between '" + from + "'and '" + to + "' and  Product_type='" + stock_type + "' ");
            return dtb;
        }
        public DataTable get_stock_adjuctment_before(string from,string item)
        {
            DataTable dtb = db.table("select id,updation_date,Action,ItemId,upate_qty,Unit,Cost from tbl_stock_updation  where updation_date < '" + from + "'  and ItemId='" + item + "'");
            return dtb;
        }
        public DataTable get_stock_transfer_before(string from,string item)
        {
            DataTable dtb = db.table("select stock_date,Action,m.RefNo,ItemId,Given_Qty ,unit,Cost from  tbl_stock_out s inner join tbl_stock_out_main m on s.RefNo=m.RefNo where stock_date < '" + from + "'   and ItemId='" + item + "'");
            return dtb;
        }
        public DataTable get_stock_in_before(string from,  string item)
        {
            DataTable dtb = db.table("select stock_date,Action,m.RefNo,ItemId,buy_Qty,unit,Cost from  tbl_stock_in s inner join tbl_stock_in_main m on s.RefNo=m.RefNo where stock_date < '" + from + "'   and ItemId='" + item + "'");
            return dtb;
        }
        public DataTable companydetails()
        {
            DataTable dtp = db.table("select name,contact_no,street_address,email,website,path  from tbl_practice_details");
            return dtp;
        }
    }
}
