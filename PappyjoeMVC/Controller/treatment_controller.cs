using PappyjoeMVC.Model;
using System.Data;

namespace PappyjoeMVC.Controller
{
    public class Treatment_controller
    {
        Treatment_model _model = new Treatment_model();
        Common_model cmodel = new Common_model();
        Connection db = new Connection();
        Procedure_Catalog_model pmodel = new Procedure_Catalog_model();
        public string check_privillege(string doctor_id)
        {
            string a = _model.check_privillege(doctor_id);
            return a;
        }
        public string privilge_for_inventory(string doctor_id)
        {
            string s = cmodel.privilge_for_inventory(doctor_id);
            return s;
        }
        public string check_edit_privillege(string doctor_id)
        {
            string a = _model.check_edit_privillege(doctor_id);
            return a;
        }
        public string delete_privillege(string doctor_id)
        {
            string a = _model.delete_privillege(doctor_id);
            return a;
        }
        public DataTable Get_CompanyNAme()
        {
            DataTable clinicname = cmodel.Get_CompanyNAme();
            return clinicname;
        }
        public string Get_DoctorName(string doctor_id)
        {
            string docnam = cmodel.Get_DoctorName(doctor_id);
            return docnam;
        }
        public string server()
        {
            string server = db.server();
            return server;
        }
        public DataTable Get_Patient_Details(string patient_id)
        {
            DataTable rs_patients = cmodel.Get_Patient_Details(patient_id);
            return rs_patients;
        }
        public DataTable get_treatments(string patient_id)
        {
            DataTable dtb = _model.Load_treatments(patient_id);
            return dtb;// intr.load_treatment(dtb);
        }
        public DataTable Load_treatments_sidelabel_count(string patient_id)
        {
            DataTable dtb = _model.Load_treatments_sidelabel_count(patient_id);
            return dtb;
        }
        public DataTable treatment_sub_details(string id)
        {
            DataTable dtb = _model.treatment_sub_details(id);
            return dtb;
        }
        public DataTable get_plan_id(string treatment_plan_id)
        {
            DataTable dtb = _model.get_plan_id(treatment_plan_id);
            return dtb;
        }
        public void delete_treatment(string treatment_plan_id)
        {
            _model.delete_treatment(treatment_plan_id);
        }
        public DataTable Get_treatment_details(string treatment_plan_id)
        {
            DataTable dtb = _model.get_treatments(treatment_plan_id);
            return dtb;
        }
        public DataTable Patient_search(string patid)
        {
            DataTable dtb = cmodel.Patient_search(patid);
            return dtb;
        }
        public string doctr_privillage_for_addnewPatient(string doctor_id)
        {
            string dtb = cmodel.doctr_privillage_for_addnewPatient(doctor_id);
            return dtb;
        }
        public string permission_for_settings(string doctor_id)
        {
            string dtb = cmodel.permission_for_settings(doctor_id);
            return dtb;
        }
        public DataTable getpatemail(string patient_id)
        {
            DataTable sr = cmodel.getpatemail(patient_id);
            return sr;
        }
        public DataTable send_email()
        {
            DataTable sms = cmodel.send_email();
            return sms;
        }
        public DataTable get_company_details()
        {
            DataTable dtp = cmodel.get_company_details();
            return dtp;
        }
        public DataTable Load_treatments_count(string patient_id, int count)
        {
            DataTable dtb = _model.Load_treatments_count(patient_id, count);
            return dtb;
        }
        public DataTable get_enablecost()
        {
            DataTable dtb = pmodel.get_enablecost();
            return dtb;
        }
        /////
        ///dental
        ///
        public DataTable get_procedure_tooth(string treatmentid)
        {
            DataTable dt = _model.get_procedure_tooth(treatmentid);
            return dt;
        }
        public DataTable tooth_relation(string patient_id, string treatment_plan_id, string procedureid)
        {
            DataTable dt = _model.tooth_relation(patient_id, treatment_plan_id, procedureid);
            return dt;
        }
        public DataTable tooth_relation_tooth(string patient_id, string treatment_plan_id, string procedureid, string toothno)
        {
            DataTable dt = _model.tooth_relation_tooth(patient_id, treatment_plan_id, procedureid, toothno);
            return dt;
        }
        public DataTable get_procedureid(string pro_name)
        {
            DataTable num = _model.get_procedureid(pro_name);
            return num;
        }
        public DataTable get_planid(string pro_name, string id, string patid)
        {
            DataTable dt = _model.get_planid(pro_name, id, patid);
            return dt;
        }
        public DataTable Get_tooth_relation(string patient_id, string treatment_plan_id)
        {
            DataTable dt = _model.Get_tooth_relation(patient_id, treatment_plan_id);
            return dt;
        }
        public void delete_tooth(string treatment_plan_id)
        {
            _model.delete_tooth(treatment_plan_id);
        }
    }
}
