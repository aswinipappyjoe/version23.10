using PappyjoeMVC.Model;
using System.Data;

namespace PappyjoeMVC.Controller
{
    public class Communication_Setting_controller
    {
        Communication_Setting_model _model = new Communication_Setting_model();
        public DataTable getsmstabledata()
        {
            DataTable sms = _model.getsmstabledata();
            return sms;
        }
        public DataTable Getsmaname()
        {
            DataTable sms = _model.Getsmaname();
            return sms;
        }
        public void update_sms(string _sms_uname, string _smspassword)
        {
            _model.update_sms(_sms_uname, _smspassword);
        }

        public void save_sms(string _sms_uname, string _smspassword)
        {
            _model.save_sms(_sms_uname, _smspassword);
        }
        public void update_email(string _emailuname, string _emailpaswsword)
        {
            _model.update_email(_emailuname, _emailpaswsword);
        }

        public void Save_email(string _emailuname, string _emailpaswsword)
        {
            _model.Save_email(_emailuname, _emailpaswsword);
        }
        public void save_ScheduleTime(string time)
        {
            _model.save_ScheduleTime(time);
        }
        public DataTable ReminderTime()
        {
            DataTable dt = _model.ReminderTime();
            return dt;
        }
        public void updateDocRemTme(string tme)
        {
            _model.updateDocRemTme(tme);
        }
        public void WelcSms(int a)
        {
            this._model.WelcSms(a);
        }
        public void patAppRem(int a)
        {
            this._model.patAppRem(a);
        }
        public void doc_appoCntSMS(int a)
        {
            this._model.doc_appoCntSMS(a);
        }
        public void smsCount(string cnt)
        {
            this._model.smsCount(cnt);
        }
        public DataTable getsmscnt()
        {
            DataTable gt = _model.getsmscnt();
            return gt;
        }
    }
}
