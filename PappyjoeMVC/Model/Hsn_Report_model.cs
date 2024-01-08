using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PappyjoeMVC.Model
{
    class Hsn_Report_model
    {
        Connection db = new Connection();
        public DataTable select_gst()
        {
            DataTable dt1 = db.table("select  Dl_Number2 from tbl_practice_details ");
            return dt1;
        }

        public DataTable Monthly_Get_gstrate_sale(string hsn,string from, string to)
        {
            DataTable dt_dr = db.table("select s.GST,s.Qty,s.Rate,s.TotalAmount from tbl_saleit  s inner join tbl_items i on i.id=s.Item_Code where i.HSN_Number='" + hsn + "'and s.InvDate between '" + from + "' and '" + to + "'");
            
            //DataTable dt_dr = db.table("select distinct s.GST,s.Item_Code,i.HSN_Number from  tbl_saleit s inner join tbl_items i on i.id=s.Item_Code  where s.InvDate between '" + from + "' and '" + to + "' ");

            return dt_dr;
        }
        public DataTable Monthly_Get_gstrate_purchase(string hsn, string from, string to)
        {
            // DataTable dt_dr = db.table("select distinct i.HSN_Number,p.GST,p.Item_Code,p.Qty,p.Rate,p.Amount from  tbl_purchit p inner join tbl_items i on i.id=p.Item_Code where p.PurchDate between '" + from + "' and '" + to + "' ");
            DataTable dt_dr = db.table("select p.GST,p.Qty,p.Rate,p.Amount from tbl_purchit  p inner join tbl_items i on i.id=p.Item_Code where i.HSN_Number='" + hsn + "'and  p.PurchDate between '" + from + "' and '" + to + "'");
            return dt_dr;
        }
        //public DataTable Monthly_pur_qty(string from,string to,string gst)
        //{
        //    DataTable dt_dr = db.table("select i.Item_Code,i.GST,i.Qty,i.Rate,i.Amount from  tbl_purchit i  where   i.PurchDate = '" + from + "'  and i.GST='" + gst + "'");
        //    return dt_dr;

        //}
        public DataTable Monthly_pur_hsn(string from, string to)
        {
            DataTable dt_dr = db.table("select distinct i.HSN_Number from tbl_items i left join tbl_purchit p on p.Item_Code=i.id  where p.PurchDate between '" + from + "' and '" + to + "'");
            return dt_dr;

        }
        public DataTable Monthly_sale_hsn(string from, string to)
        {
            DataTable dt_dr = db.table("select distinct i.HSN_Number from tbl_items i left join tbl_saleit s on s.Item_Code=i.id  where  s.InvDate between '" + from + "' and '" + to + "'");
            return dt_dr;

        }
        //print
        public DataTable practicedetails()
        {
            DataTable dt = db.table("select name,contact_no,street_address,email,website,path,Dl_Number2  from tbl_practice_details");
            return dt;
        }
    }
}
