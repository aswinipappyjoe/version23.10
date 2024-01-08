using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PappyjoeMVC.Model;
using System.Data;
namespace PappyjoeMVC.Controller
{
   public  class gst_controller
    {
        gst_report_model model = new gst_report_model();
        Daily_Invoice_Report_model dm = new Daily_Invoice_Report_model();
        purchase_order_model pordr = new purchase_order_model();
        // Bill Report
        public DataTable Get_AllData(string date)
        {
            DataTable dtb = model.Get_AllData(date);
            return dtb;
        }
        public DataTable Purchase_Get_AllData(string date)
        {
            DataTable dtb = model.Purchase_Get_AllData(date);
            return dtb;
        }
        public DataTable practicedetails()
        {
            DataTable dt = dm.practicedetails();
            return dt;
        }

        //daily gst
        public DataTable get_paymethod(string purno)
        {
            DataTable dtb = model.get_paymethod(purno);
            return dtb;
        }
        public DataTable get_supplier_name(string sup_code)
        {
            DataTable dtb = pordr.get_supplier_name(sup_code);
            return dtb;
        }
        public DataTable DAILY_Get_gstrate(string date)
        {
            DataTable dtb = model.DAILY_Get_gstrate(date);
            return dtb;
        }
        public DataTable DAILY_Get_gstrate_purchase(string date)
        {
            DataTable dtb = model.DAILY_Get_gstrate_purchase(date);
            return dtb;
        }
        public DataTable select_gst()
        {
            DataTable dtb = model.select_gst();
            return dtb;
        }
        public DataTable DAILY_GST_AllData( string gst,string date)
        {
            DataTable dtb = model.DAILY_GST_AllData( gst, date);
            return dtb;
        }
        public DataTable DAILY_GST_AllData_purchase(string gst, string date)
        {
            DataTable dtb = model.DAILY_GST_AllData_purchase(gst, date);
            return dtb;
        }
        public DataTable DAILY_paymethod(string date)
        {
            DataTable dtb = model.DAILY_paymethod(date);
            return dtb;
        }
        //monthly gst
        public DataTable Monthly_Get_gstrate(string from, string to)
        {
            DataTable dtb = model.Monthly_Get_gstrate(from, to);
            return dtb;
        }
        public DataTable Monthly_Get_gstrate_purchase(string from, string to)
        {
            DataTable dtb = model.Monthly_Get_gstrate_purchase(from, to);
            return dtb;
        }
        public DataTable Monthly_GST_AllData( string from, string to)
        {
            DataTable dtb = model.Monthly_GST_AllData( from,to);
            return dtb;
        }
        public DataTable Monthly_GST_AllData_purchase(string from, string to)
        {
            DataTable dtb = model.Monthly_GST_AllData_purchase(from, to);
            return dtb;
        }
        public DataTable Monthly_GST_count(string to)
        {
            DataTable dtb = model.Monthly_GST_count(to);
            return dtb;
        }
        public DataTable Monthly_GST_count_purchase(string to)
        {
            DataTable dtb = model.Monthly_GST_count_purchase(to);
            return dtb;
        }
        public DataTable Monthly_GST_rate(string from,string gst)
        {
            DataTable dtb = model.Monthly_GST_rate(from, gst);
            return dtb;
        }
        public DataTable Monthly_GST_rate_purchase(string from, string gst)
        {
            DataTable dtb = model.Monthly_GST_rate_purchase(from, gst);
            return dtb;
        }
        //gst summery
        public DataTable get_all_data_purchase(string item,string month,string _year)
        {
            DataTable dtb = model.get_all_data_purchase(item,month, _year);
            return dtb;
        }
        public DataTable get_all_data_purchase_year(string item, string _year)
        {
            DataTable dtb = model.get_all_data_purchase_year(item,_year);
            return dtb;
        }
        public DataTable get_month_wise_items(string month,string _year)
        {
             DataTable dtb = model.get_month_wise_items(month, _year);
            return dtb;
        }
        public DataTable get_all_data_sales_year(string item, string _year)
        {
            DataTable dtb = model.get_all_data_sales_year(item, _year);
            return dtb;
        }
        public DataTable get_year_wise_items(string _year)
        {
            DataTable dtb = model.get_year_wise_items(_year);
            return dtb;
        }
        public DataTable get_all_data_sales(string item, string month, string _year)
        {
            DataTable dtb = model.get_all_data_sales(item,month, _year);
            return dtb;
        }
        Patients_Report_model pmodel = new Patients_Report_model();
        public DataTable Get_practiceDlNumber()
        {
            DataTable d = pmodel.Get_practiceDlNumber();
            return d;
        }
    }
}
