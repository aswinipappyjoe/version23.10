using System.Data;

namespace PappyjoeMVC.Model
{
  public  class Nurses_Notes_model
    {
        Connection db=new Connection();

        public string Get_DoctorName(string doctor_id)
        {
            string docnam = db.scalar("select doctor_name from tbl_doctor Where id='" + doctor_id + "'");
            return docnam;
        }

        public DataTable Get_Patient_Details(string id)
        {
            DataTable clinicname = db.table("select * from tbl_patient where id='" + id + "'");
            return clinicname;
        }
        public DataTable dt_cf_main(string patient_id)
        {
            System.Data.DataTable dt_cf_main = db.table("SELECT  * , d.doctor_name   from tbl_nursenote_main  n   join tbl_doctor d on n.dr_id=d.id  where n.pt_id='" + patient_id + "' ORDER BY n.date DESC");
            return dt_cf_main;
        }
        public DataTable dt_staffname(string patient_id)
        {
            System.Data.DataTable dt_cf_main = db.table("SELECT doctor_name   from tbl_doctor where id ='" + patient_id + "'");
            return dt_cf_main;
        }
        public DataTable dt_cf_Complaints(string dt_cf_main)
        {
            System.Data.DataTable dt_cf_Complaints = db.table("SELECT * FROM tbl_nurses_notes where nurseid='" + dt_cf_main + "' ORDER BY id");
            return dt_cf_Complaints;
        }
        public DataTable remarks(string dt_cf_main)
        {
            System.Data.DataTable dt_cf_Complaints = db.table("SELECT * FROM tbl_nursenote_remarks where nurseid='" + dt_cf_main + "' ORDER BY id");
            return dt_cf_Complaints;
        }
        public void update_tonurse_status(string pt_id, string plan)
        {
            db.execute("update tbl_treatment_plan set ToNurse='No' where plan_main_id='" + plan + "' and pt_id ='" + pt_id + "'");
        }
        public DataTable patient_information(string patient_id)
        {
            System.Data.DataTable patient = db.table("select pt_name,gender,street_address,city,primary_mobile_number,date,date_of_birth,age from tbl_patient where id='" + patient_id + "'");
            return patient;
        }
        public DataTable notes_main(string dt_cf_main)
        {
            System.Data.DataTable dt_cf_Complaints = db.table("SELECT * FROM tbl_nursenote_main where id='" + dt_cf_main + "' ");
            return dt_cf_Complaints;
        }
    }
}
