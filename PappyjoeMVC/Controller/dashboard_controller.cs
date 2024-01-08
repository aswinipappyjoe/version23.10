using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PappyjoeMVC.Model;
using System.Data;



namespace PappyjoeMVC.Controller
{
    class dashboard_controller
    {
        dashboard_model db_model = new dashboard_model();

        public DataTable get_all_dctr()
        {
            DataTable dt = db_model.get_all_dctr();
            return dt;
        }

        public DataTable cname()
        {
            DataTable dt = db_model.cname();
            return dt;
        }
        public DataTable uname(string id)
        {
            DataTable dt = db_model.uname(id);
            return dt;
        }
        public DataTable new_patient_count(string id, DateTime startdate, DateTime enddate)
        {
            DataTable tpc = db_model.new_patient_count(id, startdate, enddate);
            return tpc;
        }

        public DataTable get_all_aponmt(DateTime startDateTime, DateTime startDateTime1)
        {
            DataTable dtb = db_model.get_all_aponmt(startDateTime, startDateTime1);
            return dtb;
        }
        public DataTable get_all_dctrtoday(DateTime startDateTime, DateTime startDateTime1)
        {
            DataTable dtb = db_model.get_all_dctrtoday(startDateTime, startDateTime1);
            return dtb;
        }
        public DataTable get_all_dctrtoday()
        {
            DataTable dtb = db_model.get_all_dctrtoday();
            return dtb;
        }
        public DataTable get_tdy_dctr(DateTime startDateTime, DateTime startDateTime1)
        {
            DataTable dtb = db_model.get_tdy_dctr(startDateTime, startDateTime1);
            return dtb;
        }
        public DataTable procedure_count_receptn(DateTime startDateTime, DateTime startDateTime1)//prcd count
        {
            DataTable dtpr = db_model.procedure_count_receptn(startDateTime, startDateTime1);
            return dtpr;
        }
        public DataTable procedure()
        {
            DataTable dtpr = db_model.procedure_all();
            return dtpr;
        }
        public DataTable get_all_rcptoday()
        {
            DataTable dtb = db_model.get_all_rcptoday();
            return dtb;
        }
        public DataTable get_all_nrstoday()
        {
            DataTable dtb = db_model.get_all_nrstoday();
            return dtb;
        }
        public DataTable get_lab_notify(DateTime startdate, DateTime enddate)
        {
            DataTable dtl = db_model.get_lab_notify(startdate, enddate);
            return dtl;
        }

        public DataTable get_specific_doctr(string id,DateTime startDateTime, DateTime startDateTime1)
        {
            DataTable dtb = db_model.get_specific_doctr(id,startDateTime, startDateTime1);
            return dtb;
        }

        public DataTable all_Patient_count(DateTime startDateTime, DateTime startDateTime1)
        {
            DataTable dtpc = db_model.all_Patient_count(startDateTime,startDateTime1);
            return dtpc;
        }
        public DataTable new_Patient_count(DateTime startDateTime, DateTime startDateTime1)
        {
            DataTable dtpc = db_model.new_patient_count(startDateTime, startDateTime1);
            return dtpc;
        }
        public DataTable procedure(DateTime startDateTime,DateTime startDateTime1)
        {
            DataTable dtpr = db_model.procedure(startDateTime, startDateTime1);
            return dtpr;

        }
        public DataTable procedure_count(DateTime startDateTime, DateTime startDateTime1)
        {
            DataTable dtpr = db_model.procedure_count(startDateTime, startDateTime1);
            return dtpr;
        }
        public DataTable get_tdyprc(string id, DateTime startdate, DateTime enddate)
        {
            DataTable gtp = db_model.get_tdyprc(id, startdate, enddate);
            return gtp;

        }
        public DataTable today_procedure_count(string id,DateTime startDateTime,DateTime startDateTime1)
        {
            DataTable tpc = db_model.today_procedure_count(id, startDateTime, startDateTime1);
            return tpc;
        }
        public DataTable rec_amt1(DateTime startDateTime, DateTime startDateTime1)
        {
            DataTable ra = db_model.rec_amt1(startDateTime, startDateTime1);
            return ra;
        }
        public DataTable sale_amt1(DateTime startDateTime, DateTime startDateTime1)
        {
            DataTable sa = db_model.sale_amt1(startDateTime, startDateTime1);
            return sa;
        }

        public DataTable totalRec_rcptnst(string id,DateTime startDateTime, DateTime startDateTime1)
        {
            DataTable ra = db_model.totalRec_rcptnst(id,startDateTime, startDateTime1);
            return ra;
        }
        public DataTable weeklyappointcount()
        {
            DataTable dt = db_model.weeklyappointcount();
            return dt;
        }
        public DataTable Monthlyappointcount()
        {
            DataTable dt = db_model.Monthlyappointcount();
            return dt;
        }
        public DataTable weeklyprocedure()
        {
            DataTable dt = db_model.weeklyprocedure();
            return dt;
        }
        public DataTable weekly_procedure_count(string id)
        {
            DataTable tpc = db_model.weekly_procedure_count(id);
            return tpc;
        }
        public DataTable weeklydctr()
        {
            DataTable dt = db_model.weeklydctr();
            return dt;
        }
        public DataTable get_specific_doctr_weekly(string id)
        {
            DataTable dtb = db_model.get_specific_doctr_weekly(id);
            return dtb;
        }
        public DataTable get_specific_doctr_monthly(string id)
        {
            DataTable dtb = db_model.get_specific_doctr_monthly(id);
            return dtb;
        }
        public DataTable monthlydctr()
        {
            DataTable dt = db_model.monthlydctr();
            return dt;
        }
        public DataTable monthlyprocedure()
        {
            DataTable dt = db_model.monthlyprocedure();
            return dt;
        }
        public DataTable monthly_procedure_count(string id)
        {
            DataTable tpc = db_model.monthly_procedure_count(id);
            return tpc;
        }

    }
}
