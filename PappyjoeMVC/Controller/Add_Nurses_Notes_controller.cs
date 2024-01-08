using PappyjoeMVC.Model;
using System.Data;

namespace PappyjoeMVC.Controller
{
  public  class Add_Nurses_Notes_controller
    {
        Add_Nurses_Notes_model model = new Add_Nurses_Notes_model();
        Common_model cmodel = new Common_model();
        public DataTable load_staffname()
        {
            DataTable dt = model.load_staffname();
            return (dt);
        }

        public DataTable load_doctorname()
        {
            DataTable dt = model.load_doctorname();
            return (dt);
        }
        public void save_main(string pt_id, string dr_id, string staff_name, string date, string plan_id,string proid,string proname)
        {
            model.save_main(pt_id, dr_id, staff_name, date, plan_id, proid, proname);
        }
        public void update_main(string pt_id, string dr_id, string staff_name, string date, string plan_id, string main_id)
        {
            model.update_main(pt_id, dr_id, staff_name, date, plan_id, main_id);
        }
        public void delete_notes(string main_id)
        {
            model.delete_notes(main_id);
        }
        public void delete_main(string main_id)
        {
            model.delete_main(main_id);
        }
        public void delete_remarks(string main_id)
        {
            model.delete_remarks(main_id);
        }
        //nurse notification
        public DataTable get_patient_notification(string date)
        {
            DataTable dt = model.get_patient_notification(date);
            return (dt);
        }
        public DataTable get_tonurse_treatment(string plan_main_id)
        {
            DataTable dt = model.get_tonurse_treatment(plan_main_id);
            return (dt);
        }
        public DataTable get_invoiceid(string pt_id, string date, string treatid)
        {
            DataTable dt = model.get_invoiceid(pt_id, date, treatid);
            return (dt);
        }
        //public DataTable get_invoiceid(string pt_id, string date,string treatid)
        //{
        //    DataTable dt = model.get_invoiceid(pt_id,date, treatid);
        //    return (dt);
        //}
        public DataTable get_patient_treatments(string pt_id, string date, string mainid)
        {
            DataTable dt = model.get_patient_treatments(pt_id, date, mainid);
            return (dt);
        }
        public void save_main(string pt_id, string dr_id, string staff_name, string date)
        {
            model.save_main(pt_id, dr_id, staff_name, date);
        }
        public void save_nursenote(string nurseid, string pt_id, string treatment_id, string procedure)
        {
            model.save_nursenote(nurseid, pt_id, treatment_id, procedure);
        }
        public string get_maxid()
        {
            string dt = model.get_maxid();
            return (dt);
        }
        public void save_nursenote_remark(string nurseid, string pt_id, string treatment_id, string remark)
        {
            model.save_nursenote_remark(nurseid, pt_id, treatment_id, remark);

        }
        public int save_log(string log_usrid, string log_type, string log_descriptn, string logdate, string logtime, string log_stage,string typeid)
        {
            int j = cmodel.save_log(log_usrid, log_type, log_descriptn, logdate, logtime, log_stage, typeid);
            return j;
        }
        public DataTable get_nurse_note_status(string pt_id, string date, string proid)
        {
            DataTable dt = model.get_nurse_note_status(pt_id, date, proid);
            return (dt);
        }
        public DataTable main_edit(string main_id)
        {
            DataTable dt = model.main_edit(main_id);
            return (dt);
        }
        public DataTable note_edit(string main_id)
        {
            DataTable dt = model.note_edit(main_id);
            return (dt);
        }
        public DataTable note_remarks_edit(string main_id)
        {
            DataTable dt = model.note_remarks_edit(main_id);
            return (dt);
        }
        public void update_patient_treatments(string pt_id,  string plan_id,string date)//patient_id, plan_id,dateTimePicker1.Value.ToString("yyyy-MM-dd")
        {
            model.update_patient_treatments( pt_id,  plan_id, date);
        }
        public DataTable get_invno(string pt_id, string date, string treatid)
        {
            DataTable dt = model.get_invno(pt_id, date, treatid);
            return (dt);
        }
    }
}
