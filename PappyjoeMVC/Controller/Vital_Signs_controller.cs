using PappyjoeMVC.Model;
using System.Data;

namespace PappyjoeMVC.Controller
{
    public class Vital_Signs_controller
    {
        Vital_Signs_model _model = new Vital_Signs_model();
        Common_model cmodel = new Common_model();
        public string doctor_id = "0";
        public string staff_id = "0";
        public string patient_id = "0";
        public DataTable Get_Patient_Details(string patient_id)
        {
            DataTable rs_patients = cmodel.Get_Patient_Details(patient_id);
            return rs_patients;
        }
        public DataTable vital(string patient_id)
        {
            DataTable dt = _model.vital(patient_id);
            return dt;
        }
        public string doctr_privillage_for_addnewPatient(string doctor_id)
        {
            string dtb = cmodel.doctr_privillage_for_addnewPatient(doctor_id);
            return dtb;
        }
        public string privilge_for_inventory(string doctor_id)
        {
            string s = cmodel.privilge_for_inventory(doctor_id);
            return s;
        }
        public string permission_for_settings(string doctor_id)
        {
            string s = cmodel.permission_for_settings(doctor_id);
            return s;
        }
        public DataTable Get_CompanyNAme()
        {
            DataTable dt = cmodel.Get_CompanyNAme();
            return dt;
        }
        public string Get_DoctorName(string doctor_id)
        {
            string dt = cmodel.Get_DoctorName(doctor_id);
            return dt;
        }
        public DataTable Get_patient_id_name_gender(string patient_id)
        {
            DataTable dt = cmodel.Get_patient_id_name_gender(patient_id);
            return dt;
        }
        public DataTable Patient_search(string _Patientid)
        {
            DataTable dt = cmodel.Patient_search(_Patientid);
            return dt;
        }
        //bhj...
        public string delete_privillege(string doctor_id)
        {
            string privid = cmodel.delete_privillege(doctor_id);
            return privid;
        }
        public void delete_vital(int vital_id)
        {
            cmodel.delete_vital(vital_id);
        }
        public int save_log(string log_usrid, string log_type, string log_descriptn, string logdate, string logtime, string log_stage, int log_type_id)
        {
            int j = cmodel.save_log(log_usrid, log_type, log_descriptn, logdate, logtime, log_stage, log_type_id);
            return j;
        }
        public DataTable vitals(string patient_id)
        {
            DataTable dt = _model.vitals(patient_id);
            return dt;
        }//bhj...
        public DataTable vitals_print(string patient_id, string id)
        {
            DataTable dt = _model.vitals_print(patient_id,id);
            return dt;

        }
        public DataTable get_company_details()
        {
            DataTable dtp = cmodel.get_company_details();
            return dtp;
        }
    }
}
