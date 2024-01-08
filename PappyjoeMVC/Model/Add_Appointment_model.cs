using System.Data;
namespace PappyjoeMVC.Model
{
    public class Add_Appointment_model
    {
        Connection db = new Connection();
        public DataTable smsreminder()
        {
            DataTable dt = db.table("select * from tbl_appt_reminder_sms");
            return dt;
        }
        public DataTable getpatdetails(string patid)
        {
            DataTable dt = db.table("select pt_name,pt_id,gender,primary_mobile_number,email_address from tbl_patient where id='" + patid + "'");
            return dt;
        }
        public DataTable get_All_procedure()
        {
            DataTable dt3 = db.table("select DISTINCT id,name from tbl_addproceduresettings");
            return dt3;
        }
        public DataTable dt_appointment(string appointmntid)
        {
            DataTable dt3 = db.table("select  start_datetime,duration,note,dr_id,plan_new_procedure from tbl_appointment  where id='" + appointmntid + "'");
            return dt3;
        }
        public string getdoctrname(string doctrid)
        {
            string t3 = db.scalar("select doctor_name from tbl_doctor where id='" + doctrid + "' and login_type='doctor' and activate_login='yes' ");
            return t3;
        }
        public DataTable getappointment(string doctrid)
        {
            DataTable dt = db.table("select calendar_color,doctor_name,mobile_number,email_id from tbl_doctor where  id=" + doctrid + " ORDER BY id");
            return dt;
        }
        public int apntupdate(string strtdatetime, string duratn, string note, string dr_id, string procedure, string bookedby, string appointmentid)
        {
            int i = db.execute("update  tbl_appointment SET start_datetime='" + strtdatetime + "',duration='" + duratn + "',note='" + note + "',dr_id ='" + dr_id + "',plan_new_procedure='" + procedure + "',booked_by='" + bookedby + "' WHERE id='" + appointmentid + "'");
            return i;
        }
        //public int save_log(string log_userid, string log_type, string log_descriptn, string log_stage)
        //{
        //    int j = db.execute("insert into tbl_log(log_user_id,log_type,log_description,log_stage)values('" + log_userid + "','" + log_type + "','" + log_descriptn + "','" + log_stage + "')");
        //    return j;
        //}
        public int insappointment(string book_datetime, string start_datetime, string duration, string note, string pt_id, string pt_name, string dr_id, string mob_no, string email_id, string procedure, string booked_by)
        {
            int i = db.execute("insert into tbl_appointment (book_datetime,start_datetime,duration,note,pt_id,pt_name,dr_id,mobile_no,email_id,notify_patient,notify_doctor,plan_new_procedure,status,booked_by ) values('" + book_datetime + "','" + start_datetime + "','" + duration + "','" + note + "','" + pt_id + "','" + pt_name + "','" + dr_id + "','" + mob_no + "','" + email_id + "','yes','yes','" + procedure + "','scheduled','" + booked_by + "')");
            return i;
        }
        public int inssms(string pt_id, string send_datetime, string message)
        {
            int i = db.execute("insert into tbl_pt_sms_communication (pt_id,send_datetime,type,message_status,message) values('" + pt_id + "','" + send_datetime + "','patient','sent','" + message + "' )");
            return i;
        }
        public string settingsprivilage(string doctrid)
        {
            string b = db.scalar("select id from tbl_User_Privilege where UserID=" + doctrid + " and Category='CLMS' and Permission='A'");
            return b;
        }
        //public DataTable smsdetails()
        //{
        //    DataTable dt = db.table("select smsName,smsPass from tbl_SmsEmailConfig");
        //    return dt;
        //}
        //public DataTable clinicdetails()
        //{
        //    DataTable dtb = db.table("select name,locality,contact_no from tbl_practice_details");
        //    return dtb;
        //}

        public DataTable search_procedure(string proc)
        {
            DataTable dt3 = db.table("select DISTINCT id,name from tbl_addproceduresettings where (name like '" + proc + "%' )");
            return dt3;
        }
        public DataTable Get_procedure(string proc)
        {
            DataTable dt3 = db.table("select DISTINCT id,name from tbl_addproceduresettings where id='" + proc + "'");
            return dt3;
        }
        public DataTable getPatRemTime()
        {
            DataTable dt = db.table("select * from tbl_smsemailconfig ");
            return dt;
        }
        public DataTable get_DocRemsmsTime()
        {
            DataTable dt = db.table("select doctorApptCountsmsTime from tbl_smsemailconfig where id=1");
            return dt;
        }
        public DataTable getDocAppntmnt(string drid,string date,string date2)
        {
            DataTable dt = db.table("select * from tbl_appointment where dr_id='" + drid + "' and start_datetime between '"+date+"' and '"+date2+"'");
            return dt;
        }
        public void insert_dataToDocNotif(string drid, string webresp)
        {
            db.execute("insert into tbl_doctors_notification(dr_id,webRespo)values('" + drid + "','" + webresp + "')");
        }
        public void updateWebresp(string drid, string webresp, string date)
        {
            db.execute("update tbl_doctors_notification set webRespo='" + webresp + "', date='" + date + "' where dr_id='" + drid + "'");
        }
        public DataTable get_docNotifInfor(string drid)
        {
            DataTable dt = db.table("select * from tbl_doctors_notification where dr_id='" + drid + "'");
            return dt;
        }
        public void insert_DocId(string drid)
        {
            db.execute("insert into tbl_doctors_notification(dr_id)values('" + drid + "')");
        }
    }
}