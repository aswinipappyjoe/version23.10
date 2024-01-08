using System;
using System.Data;
using MySql.Data.MySqlClient;
namespace PappyjoeMVC.Model
{
    public class Common_model
    {
        Connection db = new Connection();
        public string Load_CompanyName()
        {
            string clinicn = "";
            string name = db.scalar("select name from tbl_practice_details");
            clinicn = name.Replace("¤", "'");
            return clinicn;
        }
        public DataTable clinicdetails()
        {
            DataTable dtb = db.table("select name,locality,contact_no from tbl_practice_details");
            return dtb;
        }
        public DataTable getrate(string ar)//start sanoop
        {
            string qry = "select * from tbl_items where id='" + ar + "'";
            DataTable dt = db.table(qry);
            return dt;
        }//bhj
        public int save_log(string log_userid, string log_type, string log_descriptn, string logdate, string logtime, string log_stage,string log_type_id)
        {
            int j = db.execute("insert into tbl_log(log_user_id,log_type,log_description,date,time,log_stage,log_type_id)values('" + log_userid + "','" + log_type + "','" + log_descriptn + "','" + logdate + "','" + logtime + "','" + log_stage + "','"+log_type_id+"')");
            return j;
        }
        //public int save_log(string log_userid, string log_type, string log_descriptn, string logdate, string logtime, string log_stage, string log_type_id)
        //{
        //    int j = db.execute("insert into tbl_log(log_user_id,log_type,log_description,date,time,log_stage,log_type_id)values('" + log_userid + "','" + log_type + "','" + log_descriptn + "','" + logdate + "','" + logtime + "','" + log_stage + "','" + log_type_id + "')");
        //    return j;
        //}
        public int save_log(string log_userid, string log_type, string log_descriptn, string logdate, string logtime, string log_stage, string log_type_id, MySqlConnection con, MySqlTransaction trans)//consultation
        {
            int j = db.trans_execute("insert into tbl_log(log_user_id,log_type,log_description,date,time,log_stage,log_type_id)values('" + log_userid + "','" + log_type + "','" + log_descriptn + "','" + logdate + "','" + logtime + "','" + log_stage + "','" + log_type_id + "')", con,trans);
            return j;
        }
        public DataTable Get_CompanyNAme()
        {
            DataTable clinicname = db.table("select name,id,path,Prescription_lang  from tbl_practice_details");
            return clinicname;
        }
        public DataTable practicedetails()
        {
            DataTable dt = db.table("select name,path,contact_no from tbl_practice_details");
            return dt;
        }
        public string Get_DoctorName(string doctor_id)
        {
            string docnam = db.scalar("select doctor_name from tbl_doctor Where id='" + doctor_id + "'");
            return docnam;
        }
        public DataTable Load_Logintype(string doctor_id)
        {
            DataTable lgtype = db.table("select doctor_name,login_type from tbl_doctor Where id='" + doctor_id + "'");
            return lgtype;
        }
        public DataTable get_company_details()
        {
            DataTable docnam = db.table("select * from tbl_practice_details");
            return docnam;
        }
        public string Get_DoctorId(string name)
        {
            string docnam = db.scalar("select id from tbl_doctor Where doctor_name='" + name + "'");
            return docnam;
        }
        public string Get_adminId()
        {
            string dtb = db.scalar("select id from tbl_doctor where login_type='admin'");
            return dtb;
        }
        public DataTable get_all_doctorname()
        {
            DataTable dt = db.table("select DISTINCT id,doctor_name from tbl_doctor  where (login_type='doctor' or login_type ='admin') and activate_login='yes'and availability <> 'Unavailabile' order by doctor_name");
            return dt;
        }
        public DataTable Get_Patient_Details(string id)
        {
            DataTable clinicname = db.table("select * from tbl_patient where id='" + id + "'");
            return clinicname;
        }
        public DataTable Get_Patient_id_NAme(string id)
        {
            DataTable clinicname = db.table("select  pt_id,pt_name from tbl_patient where id='" + id + "'");
            return clinicname;
        }
        public DataTable Get_Advance(string patient_id)
        {
            DataTable dtadvance = db.table("select distinct(advance) from tbl_payment where pt_id='" + patient_id + "'");
            return dtadvance;
        }
        public DataTable Get_Advance(string patient_id, MySqlConnection con, MySqlTransaction trans)//consultation
        {
            DataTable dtadvance = db.trans_table("select distinct(advance) from tbl_payment where pt_id='" + patient_id + "'",con,trans);
            return dtadvance;
        }
        public string doctr_privillage_for_addnewPatient(string doctor_id)
        {
            string id = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='PAT' and Permission='A'");
            return id;
        }
        public string privillage_E(string doctor_id)
        {
            string id = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='PAT' and Permission='E'");
            return id;
        }
        public string privillage_D(string doctor_id)
        {
            string id = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='PAT' and Permission='D'");
            return id;
        }
        public string privilge_for_inventory(string doctor_id)
        {
            string id = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='INVENTORY' and Permission='A'");
            return id;
        }
        public DataTable get_patient_date(string patient_id)
        {
            DataTable dtb = db.table("select date from tbl_patient where id='" + patient_id + "'");
            return dtb;
        }
        public DataTable Get_Practice_details()
        {
            DataTable dtb = db.table("select name,contact_no,path,street_address,locality from tbl_practice_details");
            return dtb;
        }
        public DataTable Get_practiceDlNumber()
        {
            DataTable dtp = db.table("select name,street_address,locality,pincode,contact_no,path,email,website,Dl_Number,Dl_Number2 from tbl_practice_details");
            return dtp;
        }
        public DataTable send_email()
        {
            DataTable dtb = db.table("select emailName,emailPass from tbl_smsemailconfig");
            return dtb;
        }
        public DataTable smsdetails()
        {
            DataTable dt = db.table("select * from tbl_smsemailconfig");
            return dt;
        }
        public DataTable getpatemail(string patient_id)
        {
            DataTable query = db.table("select email_address,pt_name from tbl_patient where id='" + patient_id + "'");
            return query;
        }
        public DataTable get_All_proceure()
        {
            DataTable dt3 = db.table("select * from  tbl_addproceduresettings order by id limit 200");
            return dt3;
        }
        public DataTable Patient_search(string _Patientid)//////fdgdg
        {
            DataTable dtdr = db.table("select id, CONCAT(pt_name, ', ', substring(gender,1,1), ', ',primary_mobile_number) as patient from tbl_patient where (pt_name like '" + _Patientid + "%'   or pt_id like '" + _Patientid + "%' or primary_mobile_number like '" + _Patientid + "%') and Profile_Status = 'Active' ");//order by pt_id
            return dtdr;
        }
        
        public string permission_for_settings(string doctor_id)
        {
            string a = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='CLMS' and Permission='A'");
            return a;
        }
        public void save_review(string date, string patient_id)
        {
            int iii = db.execute("insert into  tbl_review(fix_datetime,review_datetime,pt_id,status) values('" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + date + "','" + patient_id + "','YES')");
        }
        public void save_review(string date, string patient_id, MySqlConnection con, MySqlTransaction trans)//trans consultation
        {
            int iii = db.trans_execute("insert into  tbl_review(fix_datetime,review_datetime,pt_id,status) values('" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + date + "','" + patient_id + "','YES')",con,trans);
        }
        public void save_appointment(string date, string patient_id, string pat_name, string dr_id, string Patient_mobile, string dr_name)
        {
            db.execute( "insert into tbl_appointment (book_datetime,start_datetime,duration,note,pt_id,pt_name,dr_id,mobile_no,email_id,notify_patient,notify_doctor,plan_new_procedure,status,booked_by ) values('" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + date + "','5','Review Appointment','" + patient_id + "','" + pat_name + "','" + dr_id + "','" + Patient_mobile + "','','yes','yes','','scheduled','" + dr_name + "')");
        }
        public void save_appointment(string date, string patient_id, string pat_name, string dr_id, string Patient_mobile, string dr_name, MySqlConnection con, MySqlTransaction trans)//trans
        {
            db.trans_execute("insert into tbl_appointment (book_datetime,start_datetime,duration,note,pt_id,pt_name,dr_id,mobile_no,email_id,notify_patient,notify_doctor,plan_new_procedure,status,booked_by ) values('" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + date + "','5','Review Appointment','" + patient_id + "','" + pat_name + "','" + dr_id + "','" + Patient_mobile + "','','yes','yes','','scheduled','" + dr_name + "')",con,trans);
        }
        public DataTable get_reviewId(string patient_id, string reviewdate)
        {
            DataTable dt_review = db.table("SELECT id FROM tbl_review where  pt_id='" + patient_id + "' and fix_datetime='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and review_datetime='" + reviewdate + "' ORDER BY id");
            return dt_review;
        }
        public DataTable get_reviewId(string patient_id, string reviewdate, MySqlConnection con, MySqlTransaction trans)// transaction
        {
            DataTable dt_review = db.trans_table("SELECT id FROM tbl_review where  pt_id='" + patient_id + "' and fix_datetime='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and review_datetime='" + reviewdate + "' ORDER BY id",con,trans);
            return dt_review;
        }
        public DataTable get_total_payment(string ptid)
        {
            DataTable pay = db.table("select total_payment from tbl_invoices where pt_id='" + ptid + "'");
            return pay;
        }
        public string privillage_A(string doctor_id)
        {
            string id = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRF' and Permission='A'");
            return id;
        }
        public DataTable Get_patient_id_name_gender(string patient_id)
        {
            DataTable dt = db.table("select pt_name,pt_id,gender,age,primary_mobile_number from tbl_patient where id='" + patient_id + "'");
            return dt;
        }
        public DataTable Get_pat_iDName(string patient_id)
        {
            DataTable pat = db.table("select pt_name,pt_id from tbl_patient where id='" + patient_id + "'");
            return pat;
        }
        public DataTable get_emailpatientdetails(string patient_id)
        {
            DataTable patient = db.table("select pt_name,gender,street_address,city,primary_mobile_number,date,date_of_birth from tbl_patient where id='" + patient_id + "'");
            return patient;
        }
        public DataTable Fill_unit_combo()
        {
            DataTable dt3 = db.table("select * from tbl_unit  order by id");
            return dt3;
        }
        public DataTable Fill_LoadTax()
        {
            DataTable dtb = db.table("select * from tbl_tax order by id");
            return dtb;
        }
        public DataTable sms_language()
        {
            DataTable d = db.table("select Prescription_lang from tbl_practice_details");
            return d;
        }
        public int toActivePatient(string id)
        {
            int dt = db.execute("update tbl_patient set Profile_Status='Active' where id='"+id+"'");
            return dt;
        }
        public string check_doctor(string doctor_id)
        {
            string dr_id = db.scalar("select login_type from tbl_doctor where id='" + doctor_id + "' and activate_login='Yes'");
            return dr_id;
        }
        public int insattach(string patientid, string imgname, string path, string doctrid, string catgryname)
        {
            int i = db.execute("insert into tbl_attachments(patient_id,photo_name,Path,dr_id,date,CategoryName)  values('" + patientid + "','" + imgname + "','" + path + "','" + doctrid + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + catgryname + "')");
            return i;
        }
        //bhj....
        public string delete_privillege(string doctor_id)
        {
            string privid = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRV' and Permission='D'");
            return privid;
        }
        public void delete_vital(int vital_id)
        {
            db.execute("delete from tbl_vital_signs where id='" + vital_id + "'");
            db.execute("delete from tbl_vital_signs where main_id='" + vital_id + "'");
        }
        public int save_log(string log_userid, string log_type, string log_descriptn, string logdate, string logtime, string log_stage, int log_type_id)
        {
            int j = db.execute("insert into tbl_log(log_user_id,log_type,log_description,date,time,log_stage,log_type_id)values('" + log_userid + "','" + log_type + "','" + log_descriptn + "','" + logdate + "','" + logtime + "','" + log_stage + "','" + log_type_id + "')");
            return j;
        }//bhj....
    }
}
