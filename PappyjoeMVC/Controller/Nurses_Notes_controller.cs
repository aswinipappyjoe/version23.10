using System.Data;
using PappyjoeMVC.Model;
   
namespace PappyjoeMVC.Controller
{
  public  class Nurses_Notes_controller
    {
        Nurses_Notes_model model = new Nurses_Notes_model(); 
        Add_Nurses_Notes_model amdl = new Add_Nurses_Notes_model();
        Common_model cmodl = new Common_model();
        public string Get_DoctorName(string doctor_id)
        {
            string dt = model.Get_DoctorName(doctor_id);
            return dt;
        }
        public DataTable Get_Patient_Details(string id)
        {
            DataTable dt = model.Get_Patient_Details(id);
            return dt;
        }
        public DataTable dt_cf_main(string patient_id)
        {
            DataTable dt = model.dt_cf_main(patient_id);
            return dt;
        }
        public DataTable dt_staffname(string patient_id)
        {
            DataTable dt = model.dt_staffname(patient_id);
            return dt;
        }
        public DataTable dt_cf_Complaints(string dt_cf_main)
        {
            DataTable dt = model.dt_cf_Complaints(dt_cf_main);
            return dt;
        }
        public DataTable remarks(string dt_cf_main)
        {
            DataTable dt = model.remarks(dt_cf_main);
            return dt;
        }
        public void delete_notes(string main_id)
        {
            amdl.delete_notes(main_id);
        }
        public void delete_main(string main_id)
        {
            amdl.delete_main(main_id);
        }
        public void delete_remarks(string main_id)
        {
            amdl.delete_remarks(main_id);
        }
        public void update_tonurse_status(string pt_id, string plan)
        {
            model.update_tonurse_status(pt_id, plan);
        }
        public DataTable notes_main(string dt_cf_main)
        {
            DataTable dt = model.notes_main(dt_cf_main);
            return dt;
        }
        public DataTable send_email()
        {
            DataTable dt = cmodl.send_email();
            return dt;
        }
        public DataTable getpatemail(string patient_id)
        {
            DataTable dt = cmodl.getpatemail(patient_id);
            return dt;
        }
        public DataTable patient_information(string patient_id)
        {
            DataTable dt = model.patient_information(patient_id);
            return dt;
        }
        public DataTable Get_Practice_details()
        {
            DataTable dt = cmodl.Get_Practice_details();
            return dt;
        }
    }
}
