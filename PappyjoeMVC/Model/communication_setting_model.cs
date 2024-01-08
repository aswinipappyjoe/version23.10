using System.Data;

namespace PappyjoeMVC.Model
{
    public class Communication_Setting_model
    {
        Connection db = new Connection();
        public DataTable getsmstabledata()
        {
            DataTable sms = db.table("select smsName,smsPass,emailName,emailPass from tbl_smsemailconfig");
            return sms;
        }
        public DataTable Getsmaname()
        {
            DataTable sms = db.table("select smsName,emailName from tbl_smsemailconfig");
            return sms;
        }
        public void update_sms(string _sms_uname, string _smspassword)
        {
            db.execute("update tbl_smsemailconfig set smsName='" + _sms_uname + "',smsPass='" + _smspassword+ "'");
        }
        public void save_sms(string _sms_uname, string _smspassword )
        {
            db.execute("insert into tbl_smsemailconfig (smsName,smsPass) values ('" + _sms_uname + "','" + _smspassword + "')");
        }
        public void update_email (string _emailuname, string _emailpaswsword)
        {
            db.execute("update tbl_smsemailconfig set emailName='" + _emailuname + "',emailPass='" + _emailpaswsword + "'");
        }
        public void Save_email(string _emailuname, string _emailpaswsword)
        {
            db.execute("insert into tbl_smsemailconfig (emailName,emailPass) values ('" + _emailuname + "','" + _emailpaswsword + "')");
        }
        public void save_ScheduleTime(string time)
        {
            db.execute("update tbl_smsemailconfig set patientRemsmsTime='" + time + "' where id=1");
        }
        public DataTable ReminderTime()
        {
            DataTable dt = db.table("select * from tbl_smsemailconfig where id=1");
            return dt;
        }
        public void updateDocRemTme(string tme)
        {
            db.execute("update tbl_smsemailconfig set doctorApptCountsmsTime='" + tme + "' where id=1");
        }
        public void WelcSms(int a)
        {
            db.execute("update tbl_smsemailconfig set pat_welcSMS='" + a + "' where id=1");
        }
        public void patAppRem(int a)
        {
            db.execute("update tbl_smsemailconfig set pat_appoRemSMS='" + a + "' where id=1");
        }
        public void doc_appoCntSMS(int a)
        {
            db.execute("update tbl_smsemailconfig set doc_appoCntSMS='" + a + "' where id=1");
        }
        public void smsCount(string cnt)
        {
            db.execute("update tbl_activation set sms='" + cnt + "'");
        }
        public DataTable getsmscnt()
        {
            DataTable gt = db.table("select sms from tbl_activation");
            return gt;
        }
    }
}
