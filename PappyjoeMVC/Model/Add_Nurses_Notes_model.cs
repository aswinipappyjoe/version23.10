using System.Data;


namespace PappyjoeMVC.Model
{
  public  class Add_Nurses_Notes_model
    {
        Connection db = new Connection();

        public DataTable load_staffname()
        {
            DataTable staffname = db.table("SELECT id,doctor_name,login_type  FROM tbl_doctor WHERE login_type= 'NURSE' OR 'HEAD_NURSE'");
            return staffname;
        }

        public void save_main(string pt_id, string dr_id, string staff_name, string date, string plan_id,string proid,string proname)
        {
            db.execute("insert into tbl_nursenote_main (pt_id,dr_id,staff_name,status,date,plan_id,proid,pro_name) values('" + pt_id + "','" + dr_id + "','" + staff_name + "','Completed','" + date + "','" + plan_id + "','" + proid + "','" + proname + "')");
        }
        public void update_main(string pt_id, string dr_id, string staff_name, string date, string plan_id,string main_id)
        {
            db.execute("update tbl_nursenote_main set pt_id ='" + pt_id + "',dr_id ='" + dr_id + "',staff_name ='" + staff_name + "',status ='Completed', date ='" + date + "',plan_id ='" + plan_id + "' where id ='" + main_id + "'");
        }
        public DataTable load_doctorname()
        {
            DataTable doctorname = db.table("SELECT id,doctor_name FROM tbl_doctor WHERE login_type= 'DOCTOR'");
            return doctorname;
        }
        public void delete_notes(string main_id)
        {
            db.execute("delete from tbl_nurses_notes where nurseid='"+ main_id + "' ");
        }
        public void delete_main(string main_id)
        {
            db.execute("delete from tbl_nursenote_main where id='" + main_id + "' ");
        }
        public void delete_remarks(string main_id)
        {
            db.execute("delete from tbl_nursenote_remarks where nurseid='" + main_id + "' ");
        }
        // nurse notification
        public DataTable get_patient_notification(string date)
        {
            DataTable dtb = db.table("select  t.id as mainid,t.date,t.dr_name,t.pt_id,t.pt_name,p.pt_id as  patientid,p.primary_mobile_number,t.dr_id from   tbl_treatment_plan_main t inner join tbl_patient p on p.id=t.pt_id  where  t.date='" + date + "'");
            return dtb;
        }
        public DataTable get_tonurse_treatment(string plan_main_id)
        {
            DataTable dtb = db.table("select a.id as treatid,a.pt_id,a.procedure_id,a.procedure_name  from tbl_treatment_plan a   where a.ToNurse ='Yes' and a.plan_main_id='" + plan_main_id + "'");
            return dtb;

        }
        public DataTable get_invoiceid(string pt_id, string date,string treatid)
        {
            //DataTable dtb = db.table("select id,date,pt_id,invoice,status,Tonurse_paid  from tbl_invoices_main  where pt_id ='" + pt_id + "' and  date='" + date + "' ");
            DataTable dtb = db.table("SELECT m.id, m.date, m.pt_id, m.invoice, m.status, m.Tonurse_paid  from tbl_invoices_main m INNER JOIN tbl_invoices i ON m.id = i.invoice_main_id  where m.pt_id ='" + pt_id + "' and  m.date='" + date + "' and  i.service_id = '" + treatid + "' ");
            return dtb;
        }
        public DataTable get_patient_treatments(string pt_id, string date,string mainid )
        {
            DataTable dtb = db.table("select t.id,t.procedure_id,t.procedure_name,t.pt_id  from tbl_treatment_plan t inner join tbl_treatment_plan_main m on t.plan_main_id=m.id  where t.pt_id ='" + pt_id + "' and  m.date='" + date + "' and  m.id ='" + mainid + "' and t.ToNurse='Yes'");//t.plan_main_id
            return dtb;
        }
        public void update_patient_treatments(string pt_id,string plan_id,string date)
        {
            db.execute("update  tbl_treatment_plan_main set date ='" + date + "'  where pt_id ='" + pt_id + "' and id='" + plan_id + "'");//t.plan_main_id
           
            
          /*  db.execute("update  tbl_treatment_plan set procedure_id='" + pro_id + "',procedure_name ='" + name + "',plan_main_id='" + mainid + "'  where t.pt_id ='" + pt_id + "' and plan_main_id='" + plan_id + "' and t.ToNurse='Yes'");//t.plan_ma*///in_id
            
        }
      
        public void save_main(string pt_id,string dr_id,string staff_name,string date)
        {
            db.execute("insert into tbl_nursenote_main (pt_id,dr_id,staff_name,status,date) values('" + pt_id + "','" + dr_id + "','" + staff_name + "','Completed','" + date + "')");
        }
        public void save_nursenote(string nurseid, string pt_id, string treatment_id, string procedure)
        {
            db.execute("insert into tbl_nurses_notes (nurseid,pt_id,treatment_id,procedure_notes ) values('" + nurseid + "','" + pt_id + "','" + treatment_id + "','" + procedure + "')");
        }
        public string  get_maxid()
        {
            string maxid = db.scalar("select max(id) from tbl_nursenote_main");
            return maxid;
        }
        public void save_nursenote_remark(string nurseid, string pt_id, string treatment_id, string remark)
        {
            db.execute("insert into tbl_nursenote_remarks (nurseid,pt_id,treatment_id,Remarks) values('" + nurseid + "','" + pt_id + "','" + treatment_id + "','" + remark + "')");
        }
        public DataTable get_nurse_note_status(string pt_id, string date,string proid)
        {
            DataTable dtb = db.table("select id,pt_id,status,date,Invoice_no from tbl_nursenote_main  where pt_id ='" + pt_id + "' and  date='" + date + "' and proid='" + proid + "'");
            return dtb;

        }
        public DataTable main_edit(string main_id)
        {
            DataTable dtb = db.table("select *  from tbl_nursenote_main  where id ='" + main_id + "'");
            return dtb;
        }
        public DataTable note_edit(string main_id)
        {
            DataTable dtb = db.table("select *  from tbl_nurses_notes  where nurseid ='" + main_id + "'");
            return dtb;
        }
        public DataTable note_remarks_edit(string main_id)
        {
            DataTable dtb = db.table("select *  from tbl_nursenote_remarks  where nurseid ='" + main_id + "'");
            return dtb;
        }
        public DataTable get_invno(string pt_id, string date, string treatid)
        {
            DataTable dtb = db.table("select id,date,pt_id,invoice_no from tbl_invoices  where pt_id ='" + pt_id + "' and  date='" + date + "' and service_id = '" + treatid + "'");
            return dtb;
        }
    }


}
