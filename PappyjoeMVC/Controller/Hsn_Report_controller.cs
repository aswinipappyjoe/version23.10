using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PappyjoeMVC.Model;

namespace PappyjoeMVC.Controller
{
    class Hsn_Report_controller
    {
        Hsn_Report_model model = new Hsn_Report_model();
      
        public DataTable select_gst()//gst from practise_details
        {
            DataTable dtb = model.select_gst();
            return dtb;
        }
        public DataTable Monthly_Get_gstrate_sale(string hsn,string from, string to)
        {
            DataTable dtb = model.Monthly_Get_gstrate_sale(hsn,from, to);
            return dtb;
        }
        public DataTable Monthly_Get_gstrate_purchase(string hsn, string from, string to)
        {
            DataTable dtb = model.Monthly_Get_gstrate_purchase(hsn,from,to);
            return dtb;
        }
        //public DataTable Monthly_pur_qty(string from, string to, string gst)
        //{
        //    DataTable dtb = model.Monthly_pur_qty(from,to,gst);
        //    return dtb;
        //}
        public DataTable Monthly_pur_hsn(string from, string to)
        {
            DataTable dtb = model.Monthly_pur_hsn(from, to);
            return dtb;
        }
        public DataTable Monthly_sale_hsn(string from, string to)
        {
            DataTable dtb = model.Monthly_sale_hsn(from, to);
            return dtb;
        }
        //print
        public DataTable practicedetails()
        {
            DataTable dt = model.practicedetails();
            return dt;
        }
    }
}
