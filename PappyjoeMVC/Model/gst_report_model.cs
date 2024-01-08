using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace PappyjoeMVC.Model
{
   public  class gst_report_model
    {
        Connection db = new Connection();
        public DataTable Get_AllData(string date)
        {
            DataTable dt_dr = db.table("select s.Discount,s.InvNumber,i.Item_Code,i.GST,i.Qty,i.Rate,i.TotalAmount from tbl_sales s  inner join  tbl_saleit i on s.InvNumber= i.InvNumber where s.InvDate='" + date+ "'  order by s.InvNumber");
            return dt_dr;
        }
        public DataTable Purchase_Get_AllData(string date)
        {
            DataTable dt_dr = db.table("select s.DiscPercent,s.PurchNumber,i.Item_Code,i.GST,i.Qty,i.Rate,i.Amount from tbl_purchase s  inner join  tbl_purchit i on s.PurchNumber= i.PurchNumber where s.PurchDate='" + date + "'  order by s.PurchNumber");
            return dt_dr;
        }
        //public DataTable DAILY_GST_AllData(string date)
        //{
        //    DataTable dt_dr = db.table("select s.Discount,s.InvNumber,i.Item_Code,i.GST,i.Qty,i.Rate,i.TotalAmount from tbl_sales s  inner join  tbl_saleit i on s.InvNumber= i.InvNumber where s.InvDate='" + date + "'  order by s.InvNumber");
        //    return dt_dr;
        //}
        public DataTable get_paymethod(string purno)
        {
            DataTable dtb = db.table("select paymethod from tbl_voucher where purchno='" + purno + "'");
            return dtb;
        }
        public DataTable DAILY_Get_gstrate(string date)
        {
            DataTable dt_dr = db.table("select distinct GST  from  tbl_saleit where InvDate = '" + date + "'  order by GST ");
            return dt_dr;
        }
        public DataTable DAILY_Get_gstrate_purchase(string date)
        {
            DataTable dt_dr = db.table("select distinct GST  from  tbl_purchit where PurchDate = '" + date + "'  order by GST ");
            return dt_dr;
        }
        public DataTable select_gst()
        {
            DataTable dt1 = db.table("select  Dl_Number2 from tbl_practice_details ");
            return dt1;
        }

        public DataTable DAILY_GST_AllData(string gst,string date)
        {
            DataTable dt_dr = db.table("select s.InvNumber,s.InvDate,s.cust_name,s.PayMethod,i.Item_Code,i.GST,i.Qty,i.Rate,i.TotalAmount from tbl_sales s  inner join  tbl_saleit i on s.InvNumber= i.InvNumber where  i.GST='" + gst + "' and s.InvDate = '" + date + "' order by s.InvNumber");
            return dt_dr;
        }
        public DataTable DAILY_GST_AllData_purchase(string gst, string date)
        {
            DataTable dt_dr = db.table("select s.PurchNumber,s.PurchDate,s.Sup_Code,i.Item_Code,i.GST,i.Qty,i.Rate,i.Amount from tbl_purchase s  inner join  tbl_purchit i on s.PurchNumber= i.PurchNumber where  i.GST='" + gst + "' and s.PurchDate = '" + date + "' order by s.PurchNumber");
            return dt_dr;
        }
        public DataTable DAILY_paymethod(string date)
        {
            DataTable dt_dr = db.table("select distinct PayMethod  from  tbl_sales where InvDate = '" + date + "' ");
            return dt_dr;
        }
        // monthly gst report
        public DataTable Monthly_Get_gstrate(string from, string to)
        {
            DataTable dt_dr = db.table("select distinct GST  from  tbl_saleit where InvDate between '" + from + "' and '" + to + "' ");
            return dt_dr;
        }
        public DataTable Monthly_Get_gstrate_purchase(string from, string to)
        {
            DataTable dt_dr = db.table("select distinct GST  from  tbl_purchit where PurchDate between '" + from + "' and '" + to + "' ");
            return dt_dr;
        }
        public DataTable Monthly_GST_AllData(string from, string to)
        {
            DataTable dt_dr = db.table("select distinct s.InvDate from tbl_sales s  where   s.InvDate between '" + from + "' and '" + to + "'  order by s.InvDate");
            return dt_dr;//s.InvNumber,,s.cust_name,s.PayMethod Monthly_GST_AllData_purchase
        }
        public DataTable Monthly_GST_AllData_purchase(string from, string to)
        {
            DataTable dt_dr = db.table("select distinct s.PurchDate from tbl_purchase s  where   s.PurchDate between '" + from + "' and '" + to + "'  order by s.PurchDate");
            return dt_dr;//s.InvNumber,,s.cust_name,s.PayMethod Monthly_GST_AllData_purchase
        }

        public DataTable Monthly_GST_count(string to)
        {
            DataTable dt_dr = db.table("select distinct (SELECT invnumber FROM tbl_sales WHERE invdate = '" + to + "' ORDER BY invnumber LIMIT 1) as 'first',(SELECT invnumber  FROM tbl_sales WHERE invdate = '" + to + "' ORDER BY invnumber DESC LIMIT 1) as 'last' from tbl_sales  where InvDate = '" + to + "'");
            return dt_dr;
        }
        public DataTable Monthly_GST_count_purchase(string to)
        {
            DataTable dt_dr = db.table("select distinct (SELECT PurchNumber FROM tbl_purchase WHERE PurchDate = '" + to + "' ORDER BY PurchNumber LIMIT 1) as 'first',(SELECT PurchNumber  FROM tbl_purchase WHERE PurchDate = '" + to + "' ORDER BY PurchNumber DESC LIMIT 1) as 'last' from tbl_purchase  where PurchDate = '" + to + "'");
            return dt_dr;
        }
        public DataTable Monthly_GST_rate(string from, string gst)
        {
            DataTable dt_dr = db.table("select i.Item_Code,i.GST,i.Qty,i.Rate,i.TotalAmount from  tbl_saleit i  where   i.InvDate = '" + from + "'  and i.GST='" + gst + "'");
            return dt_dr;
        }
        public DataTable Monthly_GST_rate_purchase(string from, string gst)
        {
            DataTable dt_dr = db.table("select i.Item_Code,i.GST,i.Qty,i.Rate,i.Amount from  tbl_purchit i  where   i.PurchDate = '" + from + "'  and i.GST='" + gst + "'");
            return dt_dr;
        }
        // gst summery report

        public DataTable get_all_data_purchase(string item,string month, string _year)
        {
            DataTable dt_dr = db.table("select i.Item_Code,i.Desccription,i.GST,i.Rate,i.UnitMF,i.Qty,i.Unit,p.GrandTotal from  tbl_purchit i inner join tbl_purchase p on p.PurchNumber=i.PurchNumber where YEAR(i.PurchDate)= '" + _year + "'and  MONTH( i.PurchDate )='" + month+ "' and i.Item_Code='"+item+"' ");
            return dt_dr;
        }
        public DataTable get_all_data_purchase_year(string item,  string _year)
        {
            DataTable dt_dr = db.table("select i.Item_Code,i.Desccription,i.GST,i.Rate,i.UnitMF,i.Qty,i.Unit,p.GrandTotal from  tbl_purchit i inner join tbl_purchase p on p.PurchNumber=i.PurchNumber where YEAR(i.PurchDate)= '" + _year + "' and i.Item_Code='" + item + "' ");
            return dt_dr;
        }
        public DataTable get_month_wise_items(string month,string _year)
        {
            DataTable dt_dr = db.table("select distinct Item_Code from  tbl_purchit  where MONTH( PurchDate )='" + month + "' and YEAR(PurchDate)= '" + _year + "' order by Item_Code");
            return dt_dr;
        }
        public DataTable get_year_wise_items( string _year)
        {
            DataTable dt_dr = db.table("select distinct Item_Code from  tbl_purchit  where  YEAR(PurchDate)= '" + _year + "' order by Item_Code");
            return dt_dr;
        }
        public DataTable get_all_data_sales(string item,string month, string _year)
        {
            DataTable dt_dr = db.table("select i.Item_Code,i.GST,i.Rate,i.Qty,s.TotalAmount from  tbl_saleit i inner join tbl_sales s on s.InvNumber=i.InvNumber where YEAR(i. InvDate )='" + _year + "' and  MONTH(i. InvDate )='" + month + "' and i.Item_Code='" + item + "'");//where   i.InvDate = '" + from + "'  and i.GST='" + gst + "'
            return dt_dr;
        }
        public DataTable get_all_data_sales_year(string item, string _year)
        {
            DataTable dt_dr = db.table("select i.Item_Code,i.GST,i.Rate,i.Qty,s.TotalAmount from  tbl_saleit i inner join tbl_sales s on s.InvNumber=i.InvNumber where YEAR(i. InvDate )='" + _year + "'  and i.Item_Code='" + item + "'");//where   i.InvDate = '" + from + "'  and i.GST='" + gst + "'
            return dt_dr;
        }
        ////

    }
}
