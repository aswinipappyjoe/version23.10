using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PappyjoeMVC.Model;
using System.Data;

namespace PappyjoeMVC.Controller
{
    
    class DoctorDashboard_controller
    {
        DoctorDashboard_model model = new DoctorDashboard_model();

        public DataTable get_tdy_apnmt(string id,DateTime startdate,DateTime enddate)
        {
            DataTable gta = model.get_tdy_apnmt(id, startdate, enddate);
            return gta;

        }
        public DataTable get_tdy_prc(string id, DateTime startdate, DateTime enddate)
        {
            DataTable gtp = model.get_tdy_prc(id, startdate, enddate);
            return gtp;

        }
        public DataTable get_tdyprc(string id, DateTime startdate, DateTime enddate)
        {
            DataTable gtp = model.get_tdyprc(id, startdate, enddate);
            return gtp;

        }
        public DataTable today_procedure_count(string id, DateTime startDateTime, DateTime startDateTime1)
        {
            DataTable tpc = model.today_procedure_count(id, startDateTime, startDateTime1);
            return tpc;
        }
        public DataTable procedure()
        {
            DataTable dtpr = model.procedure_all();
            return dtpr;

        }
        public DataTable today_procedure_count(string id,string prc, DateTime startdate, DateTime enddate)
        {
            DataTable tpc = model.today_procedure_count(id,prc, startdate, enddate);
            return tpc;
        }

        public DataTable new_patient_count(string id,DateTime startdate, DateTime enddate)
        {
            DataTable tpc = model.new_patient_count(id,startdate, enddate);
            return tpc;
        }
        public DataTable totalRec_rcptnst(string id, DateTime startDateTime, DateTime startDateTime1)
        {
            DataTable ra = model.totalRec_rcptnst(id, startDateTime, startDateTime1);
            return ra;
        }
        public DataTable cname()
        {
            DataTable dt = model.cname();
            return dt;
        }
        public DataTable uname(string id)
        {
            DataTable dt = model.uname(id);
            return dt;
        }
        public DataTable get_all_rcptoday()
        {
            DataTable dtb = model.get_all_rcptoday();
            return dtb;
        }
        public DataTable get_all_nrstoday()
        {
            DataTable dtb = model.get_all_nrstoday();
            return dtb;
        }
        public DataTable get_lab_notify(DateTime startdate, DateTime enddate)
        {
            DataTable dtl = model.get_lab_notify(startdate, enddate);
            return dtl;

        }

        public DataTable weeklyappointcount (string id)
        {
            DataTable dtl = model.weeklyappointcount(id);
            return dtl;

        }
   
             public DataTable weeklyprocedure(string id)
        {
            DataTable dtl = model.weeklyprocedure(id);
            return dtl;

        }
     

             public DataTable weekly_procedure_count(string id,string dr)
        {
            DataTable dtl = model.weekly_procedure_count(id,dr);
            return dtl;

        }
        public DataTable Monthlyappointcount(string id)
        {
            DataTable dt = model.Monthlyappointcount(id);
            return dt;
        }
        public DataTable monthlyprocedure(string id)
        {
            DataTable dt = model.monthlyprocedure(id);
            return dt;
        }
        public DataTable monthly_procedure_count(string id)
        {
            DataTable tpc = model.monthly_procedure_count(id);
            return tpc;
        }

    }
}
