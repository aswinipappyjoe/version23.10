using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PappyjoeMVC.Model;
using System.Data;

namespace PappyjoeMVC.Controller
{
    class NursesDashboard_controller
    {
        NursesDashboard_model model = new NursesDashboard_model();
        public DataTable get_patient_notification(DateTime startdate,DateTime enddate)
        {
            DataTable gn = model.get_patient_notification(startdate, enddate);
                return gn;
        }
        public DataTable get_tonurse_notify(string plan_main_id)
        {
            DataTable dt = model.get_tonurse_notify(plan_main_id);
            return dt;
        }
        public DataTable total_tonurse_notify(DateTime startdate,DateTime enddate)
        {
            DataTable dt = model.total_tonurse_notify(startdate,enddate);
            return dt;
        }
        public DataTable get_status(string pt_id,string pr_id,DateTime startdate,DateTime enddate)
        {
            DataTable gs = model.get_nstatus(pt_id, pr_id, startdate, enddate);
            return (gs);
        }
        public DataTable get_inv(string pid, string pr_id, DateTime startdate, DateTime enddate)
        {
            DataTable gs = model.get_inv(pid, pr_id, startdate, enddate);
            return gs;
        }
        public DataTable cmplt_nnotification(DateTime startdate, DateTime enddate)
        {
            DataTable dt = model.cmplt_nnotification(startdate, enddate);
            return dt;
        }
        public DataTable get_invoiceid(DateTime startdate,DateTime enddate)
        {
            DataTable dt = model.get_invoiceid(startdate,enddate);
            return dt;
        }
        public DataTable get_patient_invoice_notification(string pt_id, string mainid)
        {
            DataTable dt = model.get_patient_invoice_notification(pt_id, mainid);
            return dt;
        }
        public DataTable docname(string id)
        {
            DataTable dt = model.docname(id);
            return dt;
        }
        public DataTable get_statuslab(string pt_id, string dr_id, DateTime startdate,DateTime enddate)
        {
            DataTable dt = model.get_status(pt_id, dr_id, startdate,enddate);
            return dt;
        }
        //lab
        public DataTable get_lab_notify(DateTime startdate, DateTime enddate)
        {
            DataTable dtl = model.get_lab_notify(startdate,enddate);
            return dtl;
        }
        public DataTable pending_lab_notify(DateTime startdate, DateTime enddate)
        {
            DataTable dtl = model.pending_lab_notify(startdate, enddate);
            return dtl;
        }

        public DataTable completed_lab_notify(DateTime startdate, DateTime enddate)
        {
            DataTable dtl = model.completed_lab_notify(startdate, enddate);
            return dtl;
        }
       
        //nnotifu
        public DataTable get_nnotify(DateTime startdate, DateTime enddate)
        {
            DataTable dtl = model.get_nnotify(startdate, enddate);
            return dtl;
        }
        public DataTable get_nstatus(string id, string pdid, DateTime startdate, DateTime enddate)
        {
            DataTable dtl = model.get_nstatus(id, pdid, startdate, enddate);
            return dtl;
        }
    }
    
}
