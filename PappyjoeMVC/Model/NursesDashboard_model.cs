using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PappyjoeMVC.Model
{
    
    class NursesDashboard_model
    {
        Connection db = new Connection();
        public DataTable get_patient_notification(DateTime startdate,DateTime enddate)
        {
            DataTable dtb = db.table("select  t.id as mainid,t.date,t.dr_name,t.pt_id,t.pt_name,p.pt_id as patientid,p.primary_mobile_number from   tbl_treatment_plan_main t inner join tbl_patient p on p.id=t.pt_id  where t.date between '" + startdate.ToString("yyyy-MM-dd") + "'and '" + enddate.ToString("yyyy-MM-dd") + "'");
            return dtb;
            //DataTable dtb = db.table("select t.pt_id,p.pt_id as pid,t.id as mainid,t.date,t.dr_name,t.pt_id,t.pt_name from tbl_treatment_plan_main t inner join tbl_patient p on p.id=t.pt_id  where  t.date between '" + startdate.ToString("yyyy-MM-dd")+"'and '" + enddate.ToString("yyyy-MM-dd")+"'");
            //return dtb;
        }
        
        public DataTable get_tonurse_notify(string plan_main_id)
        {
            DataTable dtb = db.table("select a.id as treatid ,a.pt_id,a.procedure_id,a.procedure_name from tbl_treatment_plan a where a.ToNurse ='Yes' and a.plan_main_id='" + plan_main_id + "'");
            return dtb;

        }
        public DataTable total_tonurse_notify(DateTime startdate,DateTime enddate)
        {
            DataTable dtb = db.table("select distinct t.pt_id from tbl_treatment_plan t left join tbl_treatment_plan_main m on m.pt_id=t.pt_id where m.date between '" + startdate.ToString("yyyy-MM-dd") + "'and '" + enddate.ToString("yyyy-MM-dd") + "'and t.ToNurse='Yes' ");
            return dtb;

        }
        //public DataTable get_nnotify_count()
        //{
        //    DataTable dtn = db.table("select DISTINCT tp.pt_id  from tbl_treatment_plan tp inner join tbl_patient p on p.id=tp.pt_id where tp.ToNurse='Yes' ");
        //    return dtn;
        //}
       public DataTable get_nristatus(string pt_id,string pr_id,DateTime startdate,DateTime enddate)
        {
            DataTable gs = db.table("select status from tbl_nursenote_main where pt_id='" + pt_id + "'and proid='" + pr_id + "'and date between '" + startdate.ToString("yyyy-MM-dd") + "'and '" + enddate.ToString("yyyy-MM-dd") + "'");
            return gs;
        }
        public DataTable get_inv(string pt_id, string pr_id,DateTime startdate,DateTime enddate)
        {
          
            DataTable dgi = db.table("SELECT m.id, m.date, m.pt_id, m.invoice, m.status, m.Tonurse_paid  from tbl_invoices_main m INNER JOIN tbl_invoices i ON m.id = i.invoice_main_id  where m.pt_id ='" + pt_id + "' and i.service_id = '" + pr_id + "'and m.date between'" + startdate.ToString("yyyy-MM-dd") + "' and '"+enddate.ToString("yyyy-MM-dd")+"'");
            return dgi;
        }
        public DataTable cmplt_nnotification(DateTime startdate,DateTime enddate)
        {
            DataTable dtn = db.table("select  status from tbl_nursenote_main where status='Completed'and date between '" + startdate.ToString("yyyy-MM-dd") + "' and '" + enddate.ToString("yyyy-MM-dd") + "' ");
            return dtn;
        }

        public DataTable get_invoiceid(DateTime startdate,DateTime enddate)
        {
            DataTable dtb = db.table("select t.id,t.date,t.pt_id,t.invoice,t.status,p.pt_id as patientid,p.primary_mobile_number,p.pt_name  from tbl_invoices_main t  inner join tbl_patient p on p.id=t.pt_id  where   t.date between'" + startdate.ToString("yyyy-MM-dd") + "' and '" + enddate.ToString("yyyy-MM-dd") + "'");
            return dtb;
        }
        public DataTable get_patient_invoice_notification(string pt_id, string invoice_main_id)
        {
            DataTable dt = db.table("select t.id,t.pt_name,t.pt_id,t.service_id,t.services,t.date,t.invoice_main_id,t.dr_id from tbl_invoices t   where t.pt_id='" + pt_id + "' and t.invoice_main_id='" + invoice_main_id + "' and t.Lab_service='Yes'");
            return dt;
        }
        public DataTable docname(string id)
        {
            DataTable dt = db.table("select doctor_name from tbl_doctor where id='" + id + "'");
            return dt;
        }
        public DataTable get_status(string pt_id, string dr_id,DateTime startdate,DateTime enddate)
        {
            DataTable dtb = db.table("select status from tbl_lab_main where pt_id='" + pt_id + "' and dr_id='" + dr_id + "' and date between '" + startdate.ToString("yyyy-MM-dd") + "' and '" + enddate.ToString("yyyy-MM-dd") + "'");
            return dtb;
        }
        //lab
        public DataTable get_lab_notify(DateTime startdate, DateTime enddate)
        {
            DataTable dtl = db.table("select tl.pt_id,tl.dr_id,tl.work_id,tl.status,p.pt_name,d.doctor_name from tbl_lab_main tl inner join tbl_patient p on p.id=tl.pt_id left join tbl_doctor d on d.id=tl.dr_id where tl.date between '" + startdate.ToString("yyyy-MM-dd") + "' and '" + enddate.ToString("yyyy-MM-dd") + "' ");
            return dtl;
        }
        public DataTable pending_lab_notify(DateTime startdate, DateTime enddate)
        {
            DataTable dtl = db.table("select tl.pt_id from tbl_lab_main tl inner join tbl_patient p on p.id=tl.pt_id where tl.date between '" + startdate.ToString("yyyy-MM-dd") + "' and '" + enddate.ToString("yyyy-MM-dd") + "'and tl.status='' ");
            return dtl;
        }
        public DataTable completed_lab_notify(DateTime startdate, DateTime enddate)
        {
            DataTable dtl = db.table("select tl.pt_id from tbl_lab_main tl inner join tbl_patient p on p.id=tl.pt_id where tl.date between '" + startdate.ToString("yyyy-MM-dd") + "' and '" + enddate.ToString("yyyy-MM-dd") + "'and tl.status='Completed' ");
            return dtl;
        }

        //nursenotif
        public DataTable get_nnotify(DateTime startdate,DateTime enddate)
        {
            DataTable dt = db.table("select tp.pt_id as pid,tp.procedure_id,tp.procedure_name,tpm.dr_name,tpm.pt_name from tbl_treatment_plan tp left join tbl_treatment_plan_main tpm on tpm.id=tp.plan_main_id where tpm.date between '" + startdate.ToString("yyyy-MM-dd") + "' and '" + enddate.ToString("yyyy-MM-dd") + "'");
            return dt;
        }
        public DataTable get_nstatus(string id,string pdid,DateTime startdate, DateTime enddate)
        {
           DataTable dt = db.table("select status from tbl_nursenote_main where pt_id='"+id+"'and  proid='"+pdid+ "'and date between '" + startdate.ToString("yyyy-MM-dd") + "' and '" + enddate.ToString("yyyy-MM-dd") + "' ");
           return dt;
        }
}
}
