using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PappyjoeMVC.Controller;

namespace PappyjoeMVC.Model
{
   public class patient_consultation_report_model
    {
        Connection db = new Connection();
        public DataTable Load_doctor()
        {
            DataTable dt = db.table("select DISTINCT id,doctor_name from tbl_doctor  where login_type='doctor' or login_type='admin' order by doctor_name");
            return dt;
        }
        public DataTable load_data(string from, string to)
        {
            DataTable dtb = db.table(" SELECT p.id,p.Patientid,p.patient_name,p.dr_id,(select max(visited_date) from tbl_patient_followup where Patientid= p.Patientid and visited_date between '" + from + "' and '" + to + "') date ,p.payment_date,p.doctorname,(select sum(fee) from tbl_patient_followup where Patientid= p.Patientid and visited_date between '" + from + "'  and '" + to + "' ) amount ,p.payment_status FROM tbl_patient_followup p  WHERE p.visited_date between '" + from + "'  and '" + to + "'  group by p.Patientid");
            return dtb;
        }
         public DataTable get_patient_details(string pt_id)
         {
            DataTable dtb = db.table("select pt_name,pt_id,gender,age from tbl_patient where id='"+pt_id+"'");
            return dtb;
         }
        public DataTable get_doctor_fee(string dr_id)
        {
            DataTable dtb = db.table("select id,doctor_name,fee,followup_fee from  tbl_doctor where id='" + dr_id + "'");
            return dtb;
        }
        public DataTable load_data_doctor_wisw(string from, string to,string dr_id)
        {
            DataTable dtb = db.table(" SELECT p.id,p.Patientid,p.patient_name,p.dr_id,(select max(visited_date) from tbl_patient_followup where Patientid= p.Patientid and visited_date between '" + from + "' and '" + to + "') date ,p.payment_date,p.doctorname,(select sum(fee) from tbl_patient_followup where Patientid= p.Patientid and visited_date between '" + from + "'  and '" + to + "' ) amount ,p.payment_status FROM tbl_patient_followup p  WHERE p.visited_date between '" + from + "'  and '" + to + "' and p.doctorname='" + dr_id + "'  group by p.Patientid");
            return dtb;
        }
        public DataTable get_patients(string pt_id)
        {
            DataTable dtb = db.table("select id, pt_name,pt_id,gender,age,gender,primary_mobile_number,street_address from tbl_patient where id='" + pt_id + "'");
            return dtb;
        }
        public DataTable patient_wise_consultation(string pt_id)
        {
            DataTable dtb = db.table(" SELECT p.id,p.Patientid,p.patient_name,p.dr_id,p.visited_date , p.payment_date,p.doctorname,fee ,p.payment_status FROM tbl_patient_followup p  WHERE p.Patientid='" + pt_id + "' order by p.visited_date");
            return dtb;
        }
    }
}
