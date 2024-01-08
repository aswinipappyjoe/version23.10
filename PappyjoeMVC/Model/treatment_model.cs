using System.Data;
namespace PappyjoeMVC.Model
{
    public class Treatment_model
    {
        Connection db = new Connection();
        public string check_privillege(string doctor_id)
        {
            string privid = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRTP' and Permission='A'");
            return privid;
        }
        public string check_edit_privillege(string doctor_id)
        {
            string privid = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRTP' and Permission='E'");
            return privid;
        }
        public string delete_privillege(string doctor_id)
        {
            string privid = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRTP' and Permission='D'");
            return privid;
        }
        public DataTable get_treatments(string treatment_plan_id)
        {
            DataTable dt_pt_main = db.table("SELECT id,date,dr_name FROM tbl_treatment_plan_main where id='" + treatment_plan_id + "' ORDER BY date DESC");
            return dt_pt_main;
        }
        public DataTable treatment_sub_details(string id)
        {
            DataTable dt_pt_sub = db.table("SELECT id,procedure_id,procedure_name,cost,discount_inrs,discount_type,discount,total,note,status,tooth,quantity FROM tbl_treatment_plan where plan_main_id='" + id + "' ORDER BY id");
            return dt_pt_sub;
        }
        public DataTable Load_treatments(string patient_id)
        {
            System.Data.DataTable dt_pt_main = db.table("SELECT id,date,dr_name FROM tbl_treatment_plan_main where pt_id='" + patient_id + "' ORDER BY date DESC limit 10 ");
            return dt_pt_main;
        }
        public DataTable Load_treatments_sidelabel_count(string patient_id)
        {
            System.Data.DataTable dt_pt_main = db.table("SELECT id,date,dr_name FROM tbl_treatment_plan_main where pt_id='" + patient_id + "' ORDER BY date DESC  ");
            return dt_pt_main;
        }
        public DataTable Load_treatments_count(string patient_id, int count)
        {
            System.Data.DataTable dt_pt_main = db.table("SELECT id,date,dr_name FROM tbl_treatment_plan_main where pt_id='" + patient_id + "' ORDER BY date DESC limit " + count + ",10 ");
            return dt_pt_main;
        }
        //public DataTable dt_cf_main_count(string patient_id, int count)
        //{
        //    System.Data.DataTable dt_cf_main = db.table("SELECT tbl_clinical_findings.id,tbl_clinical_findings.date,tbl_doctor.doctor_name FROM tbl_clinical_findings join tbl_doctor on tbl_clinical_findings.dr_id=tbl_doctor.id  where tbl_clinical_findings.pt_id='" + patient_id + "' ORDER BY tbl_clinical_findings.date DESC limit " + count + "");
        //    return dt_cf_main;
        //}
        public DataTable get_plan_id(string treatment_plan_id)
        {
            DataTable dt_pt_check = db.table("SELECT id FROM tbl_treatment_plan  where plan_main_id='" + treatment_plan_id + "' and status='0'");
            return dt_pt_check;
        }
        public void delete_treatment(string treatment_plan_id)
        {
            db.execute("delete from tbl_treatment_plan_main where id='" + treatment_plan_id + "'");
            db.execute("delete from tbl_treatment_plan where plan_main_id='" + treatment_plan_id + "'");
        }
        ///dental
        ///
        public DataTable get_procedure_tooth(string treatmentid)
        {
            DataTable dt = db.table("select id,name,Tooth_Image,Surface_Image from tbl_addproceduresettings where id='" + treatmentid + "' ");
            return dt;
        }
        public DataTable tooth_relation(string patient_id, string treatment_plan_id, string procedureid)
        {
            DataTable dt = db.table("select * from tbl_tooth_relation where Pt_id='" + patient_id + "' and plan_main_id='" + treatment_plan_id + "' and procedure_id ='" + procedureid + "' ");
            return dt;
        }
        public DataTable tooth_relation_tooth(string patient_id, string treatment_plan_id, string procedureid, string toothno)
        {
            DataTable dt = db.table("select * from tbl_tooth_relation where Pt_id='" + patient_id + "' and plan_main_id='" + treatment_plan_id + "' and procedure_id ='" + procedureid + "' and Tooth_Number='" + toothno + "' ");
            return dt;
        }
        public DataTable get_procedureid(string pro_name)
        {
            DataTable no = db.table("select id,name,Tooth_Image,Surface_Image from tbl_addproceduresettings where name='" + pro_name + "' ");
            return no;
        }
        public DataTable get_planid(string pro_name, string id, string patid)
        {
            DataTable no = db.table("select id,plan_main_id,procedure_name from tbl_treatment_plan where procedure_name='" + pro_name + "' and id='" + id + "' and  pt_id='" + patid + "'");
            return no;
        }
        public DataTable Get_tooth_relation(string patient_id, string treatment_plan_id)
        {
            DataTable dt = db.table("select * from tbl_tooth_relation where Pt_id='" + patient_id + "' and plan_main_id='" + treatment_plan_id + "' ");
            return dt;
        }
        public void delete_tooth(string treatment_plan_id)
        {
            db.execute("delete from tbl_tooth_relation where plan_main_id='" + treatment_plan_id + "'");
            //db.execute("delete from tbl_treatment_plan where plan_main_id='" + treatment_plan_id + "'");
        }

    }
}