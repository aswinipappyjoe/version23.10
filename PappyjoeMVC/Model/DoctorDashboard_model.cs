using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PappyjoeMVC.Model
{
    
    class DoctorDashboard_model
    {
        Connection db = new Connection();
        
        public DataTable get_tdy_apnmt(string id,DateTime startdate,DateTime enddate)
        {
            DataTable gta=db.table("select d.doctor_name,p.id,p.pt_id,p.pt_name,a.start_datetime,a.status,a.plan_new_procedure,a.booked_by FROM  tbl_appointment a LEFT JOIN tbl_patient p on a.pt_id=p.id LEFT JOIN tbl_doctor d ON a.dr_id=d.id WHERE a.start_datetime between  '" + startdate.ToString("yyyy-MM-dd HH:mm") + "' AND '" + enddate.ToString("yyyy-MM-dd HH:mm") + "'and a.dr_id='"+id+"' and p. Profile_Status='Active' order by a.id DESC");
            return gta;
        }
        public DataTable get_tdy_prc(string id, DateTime startdate, DateTime enddate)
        {
            DataTable gtp = db.table("select distinct a.plan_new_procedure FROM  tbl_appointment a LEFT JOIN tbl_patient p on a.pt_id=p.id LEFT JOIN tbl_doctor d ON a.dr_id=d.id WHERE a.start_datetime between  '" + startdate.ToString("yyyy-MM-dd HH:mm") + "' AND '" + enddate.ToString("yyyy-MM-dd HH:mm") + "'and a.dr_id='" + id + "' and p. Profile_Status='Active' order by a.id DESC");
            return gtp;
        }
        public DataTable get_tdyprc(string id, DateTime startdate, DateTime enddate)
        {
            DataTable gtp = db.table("select a.plan_new_procedure FROM  tbl_appointment a LEFT JOIN tbl_patient p on a.pt_id=p.id LEFT JOIN tbl_doctor d ON a.dr_id=d.id WHERE a.start_datetime between  '" + startdate.ToString("yyyy-MM-dd HH:mm") + "' AND '" + enddate.ToString("yyyy-MM-dd HH:mm") + "'and a.dr_id='" + id + "' and p. Profile_Status='Active' order by a.id DESC");
            return gtp;
        }
        public DataTable today_procedure_count(string id,string prc,DateTime startdate, DateTime enddate)
        {
            DataTable tpc = db.table("select a.plan_new_procedure,p.pt_id,d.doctor_name,a.start_datetime,a.id from tbl_appointment a left join tbl_patient p on a.pt_id=p.id left join tbl_doctor d on a.dr_id=d.id where a.dr_id='"+id+"'and plan_new_procedure='" + prc + "' and a.start_datetime between'" + startdate.ToString("yyyy-MM-dd HH:mm") + "' and '" + enddate.ToString("yyyy-MM-dd HH:mm") + "'and p.Profile_Status='Active' order by a.id DESC ");
            return tpc;
        }
        public DataTable procedure_all()
        {
            DataTable dtpr = db.table("select name from tbl_addproceduresettings order by id");
            return dtpr;
        }
        public DataTable today_procedure_count(string id, DateTime startDateTime, DateTime startDateTime1)
        {
            DataTable tpc = db.table("select a.plan_new_procedure,p.pt_id,d.doctor_name,a.start_datetime,a.id from tbl_appointment a left join tbl_patient p on a.pt_id=p.id left join tbl_doctor d on a.dr_id=d.id where plan_new_procedure='" + id + "' and a.start_datetime between'" + startDateTime.ToString("yyyy-MM-dd HH:mm") + "' and '" + startDateTime1.ToString("yyyy-MM-dd HH:mm") + "'and p.Profile_Status='Active' order by a.id DESC ");
            return tpc;
        }
        public DataTable new_patient_count(string id,DateTime startdate, DateTime enddate)
        {
            //DataTable dt_new = db.table("select p.pt_id from tbl_appointment a left join tbl_patient p  on a.pt_id=p.id left join tbl_doctor d on a.dr_id=d.id where doctorname='" + id +"'and date  between '" + startdate.ToString("yyyy-MM-dd") + "'and '" + enddate.ToString("yyyy-MM-dd") + "'");
            //return dt_new;
            DataTable dt_new = db.table("select p.pt_id from tbl_patient P inner JOIN tbl_doctor d on d.doctor_name=p.doctorname where d.id='"+id+"'and p.date between '" + startdate.ToString("yyyy-MM-dd") + "'and '" + enddate.ToString("yyyy-MM-dd") + "'");
            return dt_new;
        }
        public DataTable totalRec_rcptnst(string id, DateTime startDateTime, DateTime startDateTime1)
        {
            DataTable dtrc = db.table("select SUM(amount_paid)from tbl_payment where payment_date between '" + startDateTime.ToString("yyyy-MM-dd") + "'and '" + startDateTime1.ToString("yyyy-MM-dd") + "' and dr_id='" + id + "'");
            return dtrc;
        }
        public DataTable cname()
        {
            DataTable cname = db.table("select name from tbl_practice_details ");
            return cname;
        }
        public DataTable uname(string id)
        {
            DataTable dt = db.table("select doctor_name,login_type from tbl_doctor where id='" + id + "' ");
            return dt;
        }
        public DataTable get_all_nrstoday()
        {
            DataTable dname = db.table("select doctor_name from tbl_doctor where login_type='NURSE' and availability<>'Unavailabile'");
            return dname;
        }
        public DataTable get_all_rcptoday()
        {
            DataTable dname = db.table("select doctor_name from tbl_doctor where login_type='RECEPTIONIST' and availability<>'Unavailabile'");
            return dname;
        }
        public DataTable get_lab_notify(DateTime startdate, DateTime enddate)
        {
            DataTable dtl = db.table("select tl.pt_id,tl.dr_id,tl.work_id,tl.status,p.pt_name,d.doctor_name from tbl_lab_main tl inner join tbl_patient p on p.id=tl.pt_id left join tbl_doctor d on d.id=tl.dr_id where tl.date between '" + startdate.ToString("yyyy-MM-dd") + "' and '" + enddate.ToString("yyyy-MM-dd") + "' ");
            return dtl;
        }

        public DataTable weeklyappointcount(string id)
        {
            DataTable dfd = db.table("SELECT tbl_doctor.doctor_name,tbl_patient.id,tbl_patient.pt_id,tbl_patient.pt_name,tbl_appointment.start_datetime,tbl_appointment.status,tbl_appointment.plan_new_procedure,tbl_appointment.schedule,tbl_appointment.id as a_id,tbl_appointment.booked_by FROM  tbl_appointment LEFT JOIN tbl_patient on tbl_appointment.pt_id=tbl_patient.ID LEFT JOIN tbl_doctor ON tbl_appointment.dr_id=tbl_doctor.id WHERE  MONTH(start_datetime)=MONTH(now())  and YEAR(start_datetime) = YEAR(now()) and  week(start_datetime)=week(now()) and tbl_appointment.dr_id='" + id + "' and tbl_patient. Profile_Status='Active' order by tbl_appointment.id DESC");
               return dfd;
        }
        public DataTable weeklyprocedure(string id)
        {
            // DataTable dtpr = db.table("SELECT distinct tbl_appointment.plan_new_procedure  FROM  tbl_appointment LEFT JOIN tbl_patient on tbl_appointment.pt_id=tbl_patient.ID LEFT JOIN tbl_addproceduresettings ON tbl_appointment.plan_new_procedure=tbl_addproceduresettings.name WHERE week(start_datetime)=week(now()) and  and tbl_patient. Profile_Status='Active' order by tbl_appointment.id DESC");
            DataTable gtp = db.table("select distinct a.plan_new_procedure FROM  tbl_appointment a LEFT JOIN tbl_patient p on a.pt_id=p.id LEFT JOIN tbl_doctor d ON a.dr_id=d.id WHERE MONTH(start_datetime)=MONTH(now())  and YEAR(start_datetime) = YEAR(now()) and  week(start_datetime)=week(now()) and a.dr_id='" + id + "' and p. Profile_Status='Active' order by a.id DESC");
            //DataTable dfd = db.table("SELECT distinct tbl_appointment.plan_new_procedure FROM  tbl_appointment LEFT JOIN tbl_patient on tbl_appointment.pt_id=tbl_patient.ID LEFT JOIN tbl_doctor ON tbl_appointment.dr_id=tbl_doctor.id WHERE  MONTH(start_datetime)=MONTH(now())  and YEAR(start_datetime) = YEAR(now()) and  week(start_datetime)=week(now()) and tbl_appointment.dr_id='" + id + "' and tbl_patient. Profile_Status='Active' order by tbl_appointment.id DESC");
            //return dfd;
            return gtp;
        }
        public DataTable weekly_procedure_count(string id,string dr)
        {
            DataTable tpc = db.table("SELECT tbl_addproceduresettings.name  FROM  tbl_appointment LEFT JOIN tbl_doctor on tbl_appointment.dr_id=tbl_doctor.id LEFT JOIN tbl_addproceduresettings ON tbl_appointment.plan_new_procedure=tbl_addproceduresettings.name WHERE week(start_datetime)=week(now()) and  tbl_appointment.plan_new_procedure='" + id + "' and tbl_doctor.id='" + dr + "' order by tbl_appointment.id DESC");
            return tpc;
           
        }

        public DataTable Monthlyappointcount(string id)
        {
            DataTable dfd = db.table("SELECT tbl_doctor.doctor_name,tbl_patient.id,tbl_patient.pt_id,tbl_patient.pt_name,tbl_appointment.start_datetime,tbl_appointment.status,tbl_appointment.plan_new_procedure,tbl_appointment.schedule,tbl_appointment.id as a_id,tbl_appointment.booked_by FROM  tbl_appointment LEFT JOIN tbl_patient on tbl_appointment.pt_id=tbl_patient.ID LEFT JOIN tbl_doctor ON tbl_appointment.dr_id=tbl_doctor.id  where MONTH(start_datetime)=MONTH(now())  and YEAR(start_datetime) = YEAR(now()) and tbl_doctor.id='"+id+"'and tbl_patient. Profile_Status='Active' order by tbl_appointment.id DESC");
            /* DataTable dfd = db.table("select date_format(start_datetime,'%b %Y') AS 'MONTH', COUNT(*) AS 'APPOINTMENT' from tbl_appointment right join tbl_patient  on tbl_patient.id = tbl_appointment.pt_id  where start_datetime ='"+date+"' and tbl_patient.Profile_Status !='Cancelled' GROUP BY date_format(start_datetime,'%b %Y')")*/
            ;
            return dfd;
        }
        public DataTable monthlyprocedure(string id)
        {
            DataTable dtpr = db.table("select DISTINCT a.name from tbl_addproceduresettings a left join tbl_appointment ap on a.name=ap.plan_new_procedure where MONTH(ap.start_datetime)=MONTH(now()) and ap.dr_id='"+id+"' order by ap.id");

            //DataTable dtpr = db.table("SELECT distinct tbl_appointment.plan_new_procedure  FROM  tbl_appointment LEFT JOIN tbl_patient on tbl_appointment.pt_id=tbl_patient.ID LEFT JOIN tbl_addproceduresettings ON tbl_appointment.plan_new_procedure=tbl_addproceduresettings.name WHERE MONTH(start_datetime)=MONTH(now()) and tbl_patient. Profile_Status='Active' order by tbl_appointment.id DESC");
            //DataTable dtpr = db.table("select distinct plan_new_procedure from tbl_appointment where MONTH(start_datetime)=MONTH(now()) order by id");
            return dtpr;
        }
        public DataTable monthly_procedure_count(string id)
        {
            //DataTable tpc = db.table("SELECT tbl_appointment.plan_new_procedure  FROM  tbl_appointment LEFT JOIN tbl_patient on tbl_appointment.pt_id=tbl_patient.ID LEFT JOIN tbl_addproceduresettings ON tbl_appointment.plan_new_procedure=tbl_addproceduresettings.name WHERE MONTH(start_datetime)=MONTH(now()) and  tbl_appointment.plan_new_procedure='" + id + "' and tbl_patient. Profile_Status='Active' order by tbl_appointment.id DESC");
            DataTable tpc = db.table("select a.plan_new_procedure,p.pt_id,d.doctor_name,a.start_datetime,a.id from tbl_appointment a left join tbl_patient p on a.pt_id=p.id left join tbl_doctor d on a.dr_id=d.id where plan_new_procedure='" + id + "' and MONTH(start_datetime)=MONTH(now()) and p.Profile_Status='Active' order by a.id DESC ");
            return tpc;
        }

    }
}
