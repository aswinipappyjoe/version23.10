using System.Data;
using System;

namespace PappyjoeMVC.Model
{
    public class Add_Vital_Signs_model
    {
        Connection db = new Connection();
        public int submit(string patient_id, string dr_id, string doctor, string temp_type, string bp_type, string pulse, string txttemp, string text_Bp_Syst, string text_Bp_Dias, string text_Weight, string text_Resp, string dtp_date, string Txtheight,string spo2,string maxid,string time)
        {
            int i = db.execute("insert into tbl_Vital_Signs (pt_id,dr_id,dr_name,pulse,temp,temp_type,bp_syst,bp_dia,bp_type,weight,resp,date,Height,spo,main_id,time) values ('" + patient_id + "','" + dr_id + "','" + doctor + "','" + pulse + "','" + txttemp + "','" + temp_type + "','" + text_Bp_Syst + "','" + text_Bp_Dias + "','" + bp_type + "','" + text_Weight + "','" + text_Resp + "','" + Convert.ToDateTime(dtp_date).ToString("yyyy/MM/dd") + "','" + Txtheight + "','" + spo2 + "','" + maxid + "','" + time + "')");
            return i;
        }
        public int submit1(string patient_id, string dr_id, string doctor, string temp_type, string bp_type, string pulse, string txttemp, string text_Bp_Syst, string text_Bp_Dias, string text_Weight, string text_Resp, string dtp_date, string Txtheight, string spo2, string maxid, string time)
        {
            int i = db.execute("insert into tbl_Vital_Signs (pt_id,dr_id,dr_name,pulse,temp,temp_type,bp_syst,bp_dia,bp_type,weight,resp,date,Height,spo,main_id,time) values ('" + patient_id + "','" + dr_id + "','" + doctor + "','" + pulse + "','" + txttemp + "','" + temp_type + "','" + text_Bp_Syst + "','" + text_Bp_Dias + "','" + bp_type + "','" + text_Weight + "','" + text_Resp + "','" + Convert.ToDateTime(dtp_date).ToString("yyyy/MM/dd") + "','" + Txtheight + "','" + spo2 + "','" + maxid + "','" + time + "')");
            return i;
        }
        public int update(string patient_id, string dr_id, string doctor, string temp_type, string bp_type, string pulse, string txttemp, string text_Bp_Syst, string text_Bp_Dias, string text_Weight, string text_Resp, string dtp_date, string Txtheight, string spo2,string id)
        {
            int i = db.execute("update tbl_Vital_Signs set pt_id='" + patient_id + "',dr_id='" + dr_id + "',dr_name='" + doctor + "',pulse='" + pulse + "',temp='" + txttemp + "',temp_type='" + temp_type + "',bp_syst='" + text_Bp_Syst + "',bp_dia='" + text_Bp_Dias + "',bp_type='" + bp_type + "',weight='" + text_Weight + "',resp='" + text_Resp + "',date='" + Convert.ToDateTime(dtp_date).ToString("yyyy/MM/dd") + "',Height='" + Txtheight + "',spo='" + spo2 + "' where main_id= '" + id + "'");
            return i;
        }
        //bhj 
        public int updates(string patient_id, string dr_id, string doctor, string temp_type, string bp_type, string pulse, string txttemp, string text_Bp_Syst, string text_Bp_Dias, string text_Weight, string text_Resp, string dtp_date, string Txtheight, string spo2,string time, string id)
        {
            int i = db.execute("update tbl_Vital_Signs set pt_id='" + patient_id + "',dr_id='" + dr_id + "',dr_name='" + doctor + "',pulse='" + pulse + "',temp='" + txttemp + "',temp_type='" + temp_type + "',bp_syst='" + text_Bp_Syst + "',bp_dia='" + text_Bp_Dias + "',bp_type='" + bp_type + "',weight='" + text_Weight + "',resp='" + text_Resp + "',date='" + Convert.ToDateTime(dtp_date).ToString("yyyy/MM/dd") + "',Height='" + Txtheight + "',spo='" + spo2 + "',time='"+time+"' where main_id= '" + id + "'");
            return i;
        }
        public void update_vital_main(string patient_id, string dr_id, string dtp_date,string mainid)
        {
            db.execute("update tbl_vital_main set dr_id='" + dr_id + "', date='" + Convert.ToDateTime(dtp_date).ToString("yyyy/MM/dd") + "' where pt_id='" + patient_id + "' and id= '" + mainid + "'");
        }
        public DataTable dt_load(string id)
        {
            DataTable dt = db.table("select * from tbl_Vital_Signs where main_id='" + id + "'");
            return dt;
        }
        public void save_vital_main(string patient_id, string dr_id,string dtp_date)
        {
            db.execute("insert into tbl_vital_main (pt_id,dr_id,date) values('" + patient_id + "','" + dr_id + "','" + Convert.ToDateTime(dtp_date).ToString("yyyy/MM/dd") + "')");
        }
        public DataTable dt_get_maxid()
        {
            DataTable dt = db.table("select max(id) from tbl_vital_main");
            return dt;
        }
    }
}
