using PappyjoeMVC.Model;
using System.Data;

using MySql.Data.MySqlClient;
namespace PappyjoeMVC.Controller
{
    public class Add_Treatmentplan_controller
    {
        
        Add_Treatmentplan_model _Model = new Add_Treatmentplan_model();
        Common_model cmodel = new Common_model();
        Connection db = new Connection();
        Add_Finished_Treatment_model fmodel = new Add_Finished_Treatment_model();
        Procedure_Catalog_model pmodel = new Procedure_Catalog_model();
        public int save_log(string log_usrid, string log_type, string log_descriptn, string logdate, string logtime, string log_stage,string typeid)
        {
            int j = cmodel.save_log(log_usrid, log_type, log_descriptn, logdate, logtime, log_stage, typeid);
            return j;
        }
        public DataTable get_ProcedureTreatment(string id)
        {
            DataTable dtb = _Model.get_ProcedureTreatment(id);
            return dtb;
        }
        public string privilge_for_inventory(string doctor_id)
        {
            string s = cmodel.privilge_for_inventory(doctor_id);
            return s;
        }
        public DataTable check_procedurename(string AddProcedureName)
        {
            DataTable dtb = _Model.check_procedurename(AddProcedureName);
            return dtb;
        }
        public void save_Procedure(string _addProcedurename, string _procedurecost)
        {
            _Model.save_Procedure( _addProcedurename,  _procedurecost);
        }
        public string Procedure_maxid()
        {
            string s = _Model.Procedure_maxid();
            return s;
        }
        public DataTable Get_all_procedures()
        {
            DataTable dtb = cmodel.get_All_proceure();
            return dtb;
        }
        public void Save_treatment(string dr_id, string patient_id, string _date, string _doctor, string _patientname, string _totalcost, string _totaldiscount, string _grandtotal, MySqlConnection con, MySqlTransaction trans)
        {
            _Model.Save_treatment( dr_id,  patient_id,  _date,  _doctor,  _patientname,  _totalcost,  _totaldiscount,  _grandtotal,con,trans);
        }
        public void Save_treatment(string dr_id, string patient_id, string _date, string _doctor, string _patientname, string _totalcost, string _totaldiscount, string _grandtotal)
        {
            _Model.Save_treatment(dr_id, patient_id, _date, _doctor, _patientname, _totalcost, _totaldiscount, _grandtotal);
        }
        public string get_treatmentmaxid(MySqlConnection con, MySqlTransaction trans)
        {
            string dt = _Model.get_treatmentmaxid(con,trans);
            return dt;
        }
        public string get_treatmentmaxid()
        {
            string dt = _Model.get_treatmentmaxid();
            return dt;
        }
        public void Save_treatmentgrid(int j, string procedure_id, string pt_id, string procedure_name, string quantity, string cost, string discount_type, string discount, string total, string discount_inrs, string note, string tooth, MySqlConnection con, MySqlTransaction trans)
        {
            _Model.Save_treatmentgrid(j, procedure_id, pt_id, procedure_name, quantity, cost, discount_type, discount, total, discount_inrs, note, tooth,con,trans);
        }
        public void Save_treatmentgrid(int j, string procedure_id, string pt_id, string procedure_name, string quantity, string cost, string discount_type, string discount, string total, string discount_inrs, string note, string tooth)
        {
            _Model.Save_treatmentgrid(j, procedure_id, pt_id, procedure_name, quantity, cost, discount_type, discount, total, discount_inrs, note, tooth);
        }
        public string Load_CompanyName()
        {
            string dtb = cmodel.Load_CompanyName();
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
        public string Get_DoctorName(string doctor_id)
        {
            string dtb = cmodel.Get_DoctorName(doctor_id);
            return dtb;
        }
        public DataTable get_all_doctorname()
        {
            DataTable dtb = cmodel.get_all_doctorname();
            return dtb;
        }
        public DataTable Get_Patient_Details(string id)
        {
            DataTable dtb = cmodel.Get_Patient_Details(id);
            return dtb;
        }
        public DataTable Search_procedure(string _search)
        {
            DataTable dtb = fmodel.Search_procedure(_search);
            return dtb;
        }
        public DataTable get_enablecost()
        {
            DataTable dtb = pmodel.get_enablecost();
            return dtb;
        }
        public void Save_treatmentgrid_set_ststus(int j, string procedure_id, string pt_id, string procedure_name, string quantity, string cost, string discount_type, string discount, string total, string discount_inrs, string note, string tooth, MySqlConnection con, MySqlTransaction trans)

        {
            _Model.Save_treatmentgrid_set_ststus_fromtreatmnt(j, procedure_id, pt_id, procedure_name, quantity, cost, discount_type, discount, total, discount_inrs, note, tooth, con, trans);

        }
        public void save_completed_id_trans(string date, string patient_id, MySqlConnection con, MySqlTransaction trans)

        {
            fmodel.save_completed_id_trans(date, patient_id, con, trans);
        }
        public string get_completedMaxid_trans(MySqlConnection con, MySqlTransaction trans)
        {
            string dtb = fmodel.get_completedMaxid_trans(con, trans);
            return dtb;
        }
        public void save_completed_items_trans(int plan_main_id, string patient_id, string procedure_id, string procedure_name, string quantity, string cost, string discount_type, string discount, string total, string discount_inrs, string note, string date, string dr_id, string completed_id, string tooth,string nurse, MySqlConnection con, MySqlTransaction trans)
        {
            //fmodel.save_completed_items_trans(plan_main_id, patient_id, procedure_id, procedure_name, quantity, cost, discount_type, discount, total, discount_inrs, note, date, dr_id, completed_id, tooth,nurse, con, trans);
            fmodel.save_completed_items_trans_stats0(plan_main_id, patient_id, procedure_id, procedure_name, quantity, cost, discount_type, discount, total, discount_inrs, note, date, dr_id, completed_id, tooth, nurse, con, trans);
        }
        public string get_treatmentplan_id(int j, string procedure_id, string pt_id, MySqlConnection con, MySqlTransaction trans)
        {
            string dtb = _Model.get_treatmentplan_id(j, procedure_id, pt_id,con, trans);
            return dtb;

        }
        //dental

        public void save_tooth(string pt_id, int plan_main_id, string procedure_id, string tooth_img, string tooth_no, string surface_no, string occlusal, string mesial, string distal, string buccal, string lingual, MySqlConnection con, MySqlTransaction trans)
        {
            _Model.save_tooth(pt_id, plan_main_id, procedure_id, tooth_img, tooth_no, surface_no, occlusal, mesial, distal, buccal, lingual);
        }
    }
}
