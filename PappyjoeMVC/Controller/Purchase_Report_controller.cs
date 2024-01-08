using PappyjoeMVC.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PappyjoeMVC.Controller
{
    public class Purchase_Report_controller
    {
        Purchase_Report_model mdl = new Purchase_Report_model();
        Daily_Invoice_Report_model dm = new Daily_Invoice_Report_model();
        Purchase_model _model = new Purchase_model();
        public DataTable get_suppcode(string name)
        {
            DataTable dt = _model.get_suppcode(name);
            return dt;
        }
        public DataTable purchdtls(string frmdte,string todte)
        {
            DataTable dt = mdl.purchdtls(frmdte, todte);
            return dt;
        }
        public DataTable purchdtls_PurcNo(string frmdte, string todte, string PurchNo)
        {
            DataTable dt = mdl.purchdtls_PurcNo(frmdte, todte, PurchNo);
            return dt;
        }
        public DataTable purchdtls_All(string frmdte, string todte)
        {
            DataTable dt = mdl.purchdtls_All(frmdte, todte);
            return dt;
        }
        public DataTable purchdtls_Sup(string frmdte, string todte, string PurchNo)
        {
            DataTable dt = mdl.purchdtls_Sup(frmdte, todte, PurchNo);
            return dt;
        }
        public DataTable purchdtls_Sup_purchno(string frmdte, string todte, string PurchN, string No)
        {
            DataTable dt = mdl.purchdtls_Sup_purchno(frmdte, todte, PurchN, No);
            return dt;
        }
        public DataTable practicedetails()
        {
            DataTable dt =dm.practicedetails();
            return dt;
        }
        public DataTable supname()
        {
            DataTable dt = mdl.supname();
            return dt;
        }
    }
}
