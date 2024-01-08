using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PappyjoeMVC.Model;
using System.Data;

namespace PappyjoeMVC.Controller
{
    public class Sales_List_controller
    {
        sales_model _model = new sales_model();

        Inventory_model inv_model = new Inventory_model();

        public DataTable Get_consume_tick()
        {
            DataTable dt = inv_model.Get_consume_tick();
            return dt;
        }
        public DataTable supname()
        {
            DataTable dt = _model.supname();
            return dt;
        }
        public DataTable get_salesDetails(string dateTo, string dateFrom,string type)
        {
            DataTable dt = _model.get_salesDetails(dateTo, dateFrom,type);
            return dt;
        }
        public DataTable get_salesDetails(string dateTo, string dateFrom, string type, string pat)
        {
            DataTable dt = _model.get_salesDetails(dateTo, dateFrom, type, pat);
            return dt;
        }
        public DataTable invDetailsbyDate(string date, string type)
        {
            DataTable dt = _model.invDetailsbyDate(date,type);
            return dt;
        }
        public DataTable invDetailsbyDateBtwn(string from, string to, string type)
        {
            DataTable dt = _model.invDetailsbyDateBtwn(from, to,type);
            return dt;
        }
        public DataTable InvDetailsBtwnDates(string from, string to, string type)
        {
            DataTable dt = _model.InvDetailsBtwnDates(from, to,type);
            return dt;
        }
        public DataTable data_from_sales(string dt)
        {
            DataTable dtb = _model.data_from_sales(dt);
            return dtb;
        }
        public DataTable data_from_sales_igst(string dt)
        {
            DataTable dtb = _model.data_from_sales_igst(dt);
            return dtb;
        }

    }
}
