using PappyjoeMVC.Model;
using System.Data;

namespace PappyjoeMVC.Controller
{
    public class Add_Vital_Signs_controller
    {
        Add_Vital_Signs_model _model = new Add_Vital_Signs_model();
        Common_model model = new Common_model();
        public string doctor_id = "0";
        public string staff_id = "0";
        public string patient_id = "0";
        public DataTable Get_CompanyNAme()
        {
            DataTable dt = model.Get_CompanyNAme();
            return dt;
        }
        public string Get_DoctorName(string doctor_id)
        {
            string d = model.Get_DoctorName(doctor_id);
            return d;
        }
        public DataTable Get_patient_id_name_gender(string patient_id)
        {
            DataTable d = model.Get_patient_id_name_gender(patient_id);
            return d;
        }
        public DataTable get_all_doctorname()
        {
            DataTable d = model.get_all_doctorname();
            return d;
        }
        public string Load_CompanyName()
        {
            string dtb = model.Load_CompanyName();
            return dtb;
        }
        public string doctr_privillage_for_addnewPatient(string doctor_id)
        {
            string d = model.doctr_privillage_for_addnewPatient(doctor_id);
            return d;
        }
        public string permission_for_settings(string doctor_id)
        {
            string d = model.permission_for_settings(doctor_id);
            return d;
        }
        public DataTable Patient_search(string _Patientid)
        {
            DataTable d = model.Patient_search(_Patientid);
            return d;
        }
        public string privilge_for_inventory(string doctor_id)
        {
            string s = model.privilge_for_inventory(doctor_id);
            return s;
        }
        //public int submit(string patient_id, string dr_id, string doctor, string temp_type, string bp_type, string pulse, string txttemp, string text_Bp_Syst, string text_Bp_Dias, string text_Weight, string text_Resp, string dtp_date, string Txtheight,string spo2,string maxid)
        //{
        //    int i = _model.submit(patient_id, dr_id, doctor, temp_type, bp_type, pulse, txttemp, text_Bp_Syst, text_Bp_Dias, text_Weight, text_Resp, dtp_date, Txtheight,spo2, maxid);
        //    return i;
        //}
        public int submit1(string patient_id, string dr_id, string doctor, string temp_type, string bp_type, string pulse, string txttemp, string text_Bp_Syst, string text_Bp_Dias, string text_Weight, string text_Resp, string dtp_date, string Txtheight, string spo2, string maxid, string time)
        {
            int i = _model.submit1(patient_id, dr_id, doctor, temp_type, bp_type, pulse, txttemp, text_Bp_Syst, text_Bp_Dias, text_Weight, text_Resp, dtp_date, Txtheight, spo2, maxid, time);
            return i;
        }
        public void save_vital_main(string patient_id, string dr_id, string dtp_date)
        {
            _model.save_vital_main(patient_id, dr_id, dtp_date);
        }
        public void update_vital_main(string patient_id, string dr_id, string dtp_date, string mainid)
        {
            _model.update_vital_main(patient_id, dr_id, dtp_date, mainid);
        }
        public int save_log(string log_usrid, string log_type, string log_descriptn, string logdate, string logtime, string log_stage,string typeid)
        {
            int j = model.save_log(log_usrid, log_type, log_descriptn, logdate, logtime, log_stage, typeid);
            return j;
        }
        public DataTable dt_load(string id)
        {
            DataTable d = _model.dt_load(id);
            return d;
        }
        public int update(string patient_id, string dr_id, string doctor, string temp_type, string bp_type, string pulse, string txttemp, string text_Bp_Syst, string text_Bp_Dias, string text_Weight, string text_Resp, string dtp_date, string Txtheight, string spo2, string id)
        {
            int i = _model.update(patient_id, dr_id, doctor, temp_type, bp_type, pulse, txttemp, text_Bp_Syst, text_Bp_Dias, text_Weight, text_Resp, dtp_date, Txtheight, spo2,id);
            return i;
        }
        public int updates(string patient_id, string dr_id, string doctor, string temp_type, string bp_type, string pulse, string txttemp, string text_Bp_Syst, string text_Bp_Dias, string text_Weight, string text_Resp, string dtp_date, string Txtheight, string spo2,string time, string id)
        {
            int i = _model.updates(patient_id, dr_id, doctor, temp_type, bp_type, pulse, txttemp, text_Bp_Syst, text_Bp_Dias, text_Weight, text_Resp, dtp_date, Txtheight, spo2,time, id);
            return i;
        }
        public DataTable dt_get_maxid()
        {
            DataTable d = _model.dt_get_maxid();
            return d;
        }
    }
}
