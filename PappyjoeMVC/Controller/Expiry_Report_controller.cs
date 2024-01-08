using PappyjoeMVC.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PappyjoeMVC.Controller
{
    public class Expiry_Report_controller
    {

        Reports_model mdl = new Reports_model();
        Daily_Invoice_Report_model dm = new Daily_Invoice_Report_model();
        public DataTable findsupplier(int supcode)
        {
            DataTable dt = mdl.findsupplier(supcode);
            return dt;

        }
        public DataTable getitems(string itemcode)
        {
            DataTable dt = mdl.getitems(itemcode);
            return dt;
        }
        public DataTable getbatch(string supplier, int flag, DateTime from, DateTime to)
        {
            DataTable dt = mdl.getbatch(supplier, flag, from, to);
            return dt;
        }
        public DataTable datewiseexpiry(string dateFrom, string dateTo)
        {
            DataTable dt = mdl.datewiseexpiry(dateFrom, dateTo);
            return dt;
        }
        public DataTable practicedetails()
        {
            DataTable dt = dm.practicedetails();
            return dt;
        }
        public DataTable alreadyexpired(string supplier)
        {
            DataTable dt = mdl.alreadyexpired(supplier);
            return dt;
        }
        public DataTable alreadyexpiredagainstsupplier(string supplier)
        {
            DataTable dt = mdl.alreadyexpiredagainstsupplier(supplier);
            return dt;
        }
        public DataTable getbatchforalreadyexpired( string supplier, string from)// string to
        {
            DataTable dt = mdl.getbatchforalreadyexpired( supplier, from);
            return dt;
        }
    }
}
