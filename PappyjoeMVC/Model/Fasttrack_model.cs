using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PappyjoeMVC.Model
{
    class fasttrack_model
    {
        Connection db = new Connection();
        public DataTable data_from_automaticid()
        {
            DataTable auto = db.table("select * from tbl_patient_automaticid ");
            return auto;
        }
        public DataTable Get_invoice_prefix()
        {
            DataTable invno = db.table("select invoice_prefix,invoice_number,invoive_automation from tbl_invoice_automaticid where invoive_automation='Yes'");
            return invno;
        }
        public DataTable Load_dctrname(string doctor_id)
        {
            DataTable dt_doctor = db.table("select id,doctor_name from tbl_doctor  where id='" + doctor_id + "'");
            return dt_doctor;
        }
        public DataTable Load_doctor()
        {
            DataTable dt = db.table("select DISTINCT id,doctor_name from tbl_doctor  where (login_type='doctor' or login_type='admin')and availability='Available'  order by doctor_name");
            return dt;
        }
        public DataTable get_pt_details(string patient_id)
        {
            DataTable dtb = db.table("select id, pt_id,pt_name,primary_mobile_number,gender,age,street_address,locality,city,pincode,days from tbl_patient where id='" + patient_id + "'");
            return dtb;
        }
        public DataTable get_followup(string id)
        {
            DataTable dt = db.table("SELECT Patientid,patient_name,payment_date,doctorname,dr_id,fee,visited_date FROM tbl_patient_followup WHERE Patientid ='" + id + "' order by visited_date desc limit 1");
            return dt;
        }
        public void save_patient(string pt_name, string pt_id, string primary_mobile_number, string gender, string age, string locality, string city, string Visited, string doctorname)
        {
            db.execute("insert into tbl_patient (pt_name,pt_id,primary_mobile_number,gender,age,locality,city,Visited,doctorname,Profile_Status) values('" + pt_name + "','" + pt_id + "','" + primary_mobile_number + "',' " + gender + " ','" + age + "','" + locality + "','" + city + "','" + Visited + "','" + doctorname + "','Active')");
        }
        public void update_autogenerateid(int n)
        {
            db.execute("update tbl_patient_automaticid set patient_number='" + n + "'");
        }

        public DataTable get_doctorname(string id)
        {
            DataTable doctor_id = db.table("select id,doctor_name,fee,followup_period,followup_fee from  tbl_doctor where doctor_name='" + id + "'");
            return doctor_id;
        }
        public DataTable get_doctorname_(string id)
        {
            DataTable doctor_id = db.table("select id,doctor_name,fee,followup_period,followup_fee from  tbl_doctor where id='" + id + "'");
            return doctor_id;
        }
        public DataTable get_patient_doctor(string dr_id, string pt_id) 
        {
            DataTable dtb = db.table("SELECT * FROM tbl_patient_followup WHERE Patientid='" + pt_id + "' and doctorname='" + dr_id + "' ");
            return dtb;
        }
        public DataTable show_compl()
        {
            DataTable dt = db.table("select id,name from tbl_complaints");
            return dt;
        }
        public DataTable show_diagno()
        {
            DataTable dt3 = db.table("select id,diagnosis from tbl_diagnosis");
            return dt3;
        }
        public DataTable show_note()
        {
            DataTable dt4 = db.table("select id,notes from tbl_notes");
            return dt4;
        }
        public DataTable complaint_cell_search(string idcomp)
        {
            DataTable dt = db.table("select id,name from tbl_complaints where name like '%" + idcomp + "%' order by name");

            //DataTable dt = db.table("select id,name from tbl_complaints order by name ");//where id='" + idcomp + "'
            return dt;
        }
        public DataTable diagnose_cell_search(string iddiag)
        {            //DataTable dt3 = db.table("select id,diagnosis from tbl_diagnosis where id='" + iddiag + "'");

            DataTable dt3 = db.table("select id,diagnosis from tbl_diagnosis where diagnosis like '%" + iddiag  + "%' order by diagnosis  ");
            return dt3;
        }
        public DataTable notes_cell_search(string idnote)
        {
            //DataTable dt4 = db.table("select id,notes from tbl_notes where id='" + idnote + "'");
            DataTable dt4 = db.table("select id,notes from tbl_notes where notes like '%" + idnote + "%' order by notes ");
            return dt4;
        }
        public DataTable get_clinic(string pt_id)
        {
            DataTable dt = db.table("SELECT id, CAST(Date AS DATE) Date FROM `tbl_clinical_findings` where pt_id ='" + pt_id + "' ");
            return dt;
        }
        public DataTable get_clinic_id(string pt_id,string date)
        {
            DataTable dt = db.table("SELECT * FROM `tbl_clinical_findings` where pt_id ='" + pt_id + "' and  date= '" + date + "' ");
            return dt;
        }
        public DataTable pt_diagnosis(string id)
        {
            DataTable dt = db.table("SELECT * FROM `tbl_pt_diagnosis` where clinical_finding_id ='" + id + "' ");
            return dt;
        }
        public DataTable pt_complaints(string id)
        {
            DataTable dt = db.table("SELECT * FROM `tbl_pt_chief_compaints` where clinical_finding_id ='" + id + "' ");
            return dt;
        }
        public DataTable pt_notes(string id)
        {
            DataTable dt = db.table("SELECT * FROM `tbl_pt_note` where clinical_findings_id ='" + id + "' ");
            return dt;
        }
        public DataTable Patient_search(string _Patientid)//////fdgdg
        {
            DataTable dtdr = db.table("select id, CONCAT(pt_name, ', ', substring(gender,1,1), ', ',primary_mobile_number) as patient from tbl_patient where (pt_name like '" + _Patientid + "%'   or pt_id like '" + _Patientid + "%' or primary_mobile_number like '" + _Patientid + "%') and Profile_Status = 'Active' ");//order by pt_id
            return dtdr;
        }
        public DataTable pt_details(string ptid)
        {
            DataTable dt = db.table("select id,pt_id,pt_name,doctorname,primary_mobile_number,age,gender,days from tbl_patient where id='" + ptid + "'"); ;
            return dt;
        }
        //prescription
        public DataTable get_tmplates()
        {
            DataTable dt2 = db.table("select id,templates from tbl_templates_main ORDER BY id DESC");
            return dt2;
        }
        public DataTable get_prescriptnwthname(string pescrptn)
        {
            DataTable dt_prescription = db.table("select id,CONCAT(name,' ', type ) as name, CONCAT(strength_gr ,' ' , strength ) as type,inventory_id from tbl_adddrug where name like '%" + pescrptn + "%'  ORDER BY id DESC Limit 30");
            return dt_prescription;
        }
        public DataTable previous_pres_date(string ptid)  
        {
             DataTable dt2 = db.table("select CAST(date AS DATE) date from tbl_prescription_main where pt_id='" + ptid + "' ORDER BY date DESC");
            return dt2;
        }
        public DataTable previous_pres_details(string ptid,string date)
        {
            DataTable dt2 = db.table("select * from tbl_prescription where pt_id='" + ptid + "' and date ='" + date + "' ");
            return dt2;
        }

        //vitals

        public DataTable previous_vital_date(string ptid)
        {
            DataTable dt2 = db.table("select CAST(date AS DATE) date from tbl_vital_main where pt_id='" + ptid + "' ORDER BY date DESC");
            return dt2;
        }
        public DataTable previous_vital_details(string ptid, string date)
        {
            DataTable dt2 = db.table("select * from tbl_vital_signs where pt_id='" + ptid + "' and date ='" +date + "' ");
            return dt2;
        }
        //invoice
        public DataTable search_procedures(string search)
        {
            DataTable dt_pt = db.table("SELECT id,name FROM tbl_addproceduresettings where name like'" + search + "%' ORDER BY id limit 250 ");
            return dt_pt;
        }
        public DataTable procedures_cost(string search)
        {
            DataTable dt_pt = db.table("SELECT id,name,cost FROM tbl_addproceduresettings where id ='" + search + "'  ");
            return dt_pt;
        }
        public string select_taxValue(string name)
        {
            string dt = db.scalar("select tax_value from tbl_tax where tax_name ='" + name + "'");
            return dt;
        }
        //save
        public void insertInto_clinical_findings(string ptid, string dt, string date, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("insert into tbl_clinical_findings (pt_id,dr_id,date) values ('" + ptid + "','" + dt + "','" + date + "')",con,trans);
        }
        public string MaxId_clinic_findings(MySqlConnection con, MySqlTransaction trans)
        {
            string treatment = db.trans_scalar("select MAX(id) from tbl_clinical_findings", con, trans);
            return treatment;
        }
        public void insrtto_chief_comp(int treat, string one, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("insert into tbl_pt_chief_compaints (clinical_finding_id,complaint_id) values('" + treat + "','" + one.Replace("'", " ") + "')", con, trans);
        }

        public void insrtto_diagno(int treat, string one, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("insert into tbl_pt_diagnosis (clinical_finding_id,diagnosis_id) values('" + treat + "','" + one.Replace("'", " ") + "')", con, trans);
        }
        public void insrtto_note(int treat, string one, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("insert into tbl_pt_note (clinical_findings_id,note_name) values('" + treat + "','" + one.Replace("'", " ") + "')", con, trans);
        }
        public void save_vital_main(string patient_id, string dr_id, string dtp_date, MySqlConnection con, MySqlTransaction trans)//vitals
        {
            db.trans_execute("insert into tbl_vital_main (pt_id,dr_id,date) values('" + patient_id + "','" + dr_id + "','" + Convert.ToDateTime(dtp_date).ToString("yyyy/MM/dd") + "')", con, trans);
        }
        public DataTable dt_get_maxid(MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dt = db.trans_table("select max(id) from tbl_vital_main", con, trans);
            return dt;
        }
        public int submit(string patient_id, string dr_id, string doctor, string temp_type, string bp_type, string pulse, string txttemp, string text_Bp_Syst, string text_Bp_Dias, string text_Weight, string text_Resp, string dtp_date, string Txtheight, string spo2, string maxid, MySqlConnection con, MySqlTransaction trans)
        {
            int i = db.trans_execute("insert into tbl_Vital_Signs (pt_id,dr_id,dr_name,pulse,temp,temp_type,bp_syst,bp_dia,bp_type,weight,resp,date,Height,spo,main_id) values ('" + patient_id + "','" + dr_id + "','" + doctor + "','" + pulse + "','" + txttemp + "','" + temp_type + "','" + text_Bp_Syst + "','" + text_Bp_Dias + "','" + bp_type + "','" + text_Weight + "','" + text_Resp + "','" + Convert.ToDateTime(dtp_date).ToString("yyyy/MM/dd") + "','" + Txtheight + "','" + spo2 + "','" + maxid + "')", con, trans);
            return i;
        }
        public DataTable get_inventoryid(string id, MySqlConnection con, MySqlTransaction trans)//prescrptn
        {
            DataTable dt4 = db.trans_table("select id,inventory_id from tbl_adddrug where id='" + id + "' and inventory_id<>0 ORDER BY id DESC", con, trans);
            return dt4;
        }
        public string get_sac(string pro_id)
        {
            string dt4 = db.scalar("select sac from tbl_addproceduresettings where id='" + pro_id + "' ");
            return dt4;
        }
        public DataTable get_inventoryid(string id)
        {
            DataTable dt4 = db.table("select id,inventory_id from tbl_adddrug where id='" + id + "' and inventory_id<>0 ORDER BY id DESC");
            return dt4;
        }
        public void save_prescriptionmain(string ptid, string d_id, string date, string prescription_bill_status, string note, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_table("insert into tbl_prescription_main (pt_id,dr_id,date,pay_status,notes) values('" + ptid + "','" + d_id + "','" + date + "','" + prescription_bill_status + "','" + note + "')", con, trans);
        }
        public string Maxid(MySqlConnection con, MySqlTransaction trans)
        {
            string dt = db.trans_scalar("select MAX(id) from tbl_prescription_main", con, trans);
            return dt;
        }
        public void save_prescription(int pres_id, string pt_id, string dr_name, string dr_id, string date, string drug_name, string strength, string strength_gr, string duration_unit, string duration_period, string morning, string noon, string night, string food, string add_instruction, string drug_type, string status, string drug_id, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("insert into tbl_prescription (pres_id,pt_id,dr_name,dr_id,date,drug_name,strength,strength_gr,duration_unit,duration_period,morning,noon,night,food,add_instruction,drug_type,status,drug_id) values('" + pres_id + "','" + pt_id + "','" + dr_name + "','" + dr_id + "','" + date + "','" + drug_name + "','" + strength + "','" + strength_gr + "','  " + duration_unit + "','" + duration_period + "','" + morning + "','" + noon + "','" + night + "','" + food + "','" + add_instruction + "','" + drug_type + "'," + status + ",'" + drug_id + "')", con, trans);
        }
        public void save_completed_items(int plan_main_id, string patient_id, string procedure_id, string procedure_name, string quantity, string cost, string discount_type, string discount, string total, string discount_inrs, string note, string date, string dr_id, string completed_id, string tooth, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("insert into tbl_completed_procedures (plan_main_id,pt_id,procedure_id,procedure_name,quantity,cost,discount_type,discount,total,discount_inrs,note,status,date,dr_id,completed_id,tooth,ToNurse)" + " values('" + plan_main_id + "','" + patient_id + "','" + procedure_id + "','" + procedure_name + "','" + quantity + "','" + cost + "','" + discount_type + "','" + discount + "','" + total + "','" + discount_inrs + "','" + note + "','0','" + date + "','" + dr_id + "','" + completed_id + "','" + tooth + "','No')", con, trans);//,ToNurse
        }
        public void save_followup(string Patientid, string patient_name, string visited_date, string payment_date, string doctor,string dr_id, string fee, string payment_status, MySqlConnection con, MySqlTransaction tran)
        {
            db.trans_execute("insert into tbl_patient_followup (Patientid,patient_name,visited_date,payment_date,doctorname,dr_id,fee,payment_status )values('" + Patientid + "','" + patient_name + "','" + visited_date + "','" + payment_date + "','" + doctor + "','" + dr_id + "','" + fee + "','" + payment_status + "')", con, tran);
        }
        public string get_invoicenumber(MySqlConnection con, MySqlTransaction trans)//trans consultation
        {
            string invoauto = db.trans_scalar("select invoice_number from tbl_invoice_automaticid", con, trans);
            return invoauto;
        }
        public void update_invnumber(string invoautoup, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("update tbl_invoice_automaticid set invoice_number='" + invoautoup + "'", con, trans);
        }
        //public void Save_treatmentgrid(int j, string procedure_id, string pt_id, string procedure_name, string quantity, string cost, string discount_type, string discount, string total, string discount_inrs, string note, string tooth, MySqlConnection con, MySqlTransaction trans)
        //{
        //    int t_p = db.trans_execute("insert into tbl_treatment_plan (plan_main_id,procedure_id,pt_id,procedure_name,quantity,cost,discount_type,discount,total,discount_inrs,note,status,tooth,ToNurse) values('" + j + "','" + procedure_id + "','" + pt_id + "','" + procedure_name + "','" + quantity + "','" + cost + "','" + discount_type + "','" + discount + "','" + total + "','" + discount_inrs + "','" + note + "','0','" + tooth + "','No')", con, trans);
        //}
        //public void save_completed_items_trans(int plan_main_id, string patient_id, string procedure_id, string procedure_name, string quantity, string cost, string discount_type, string discount, string total, string discount_inrs, string note, string date, string dr_id, string completed_id, string tooth, string nurse, MySqlConnection con, MySqlTransaction trans)
        //{
        //    db.trans_execute("insert into tbl_completed_procedures (plan_main_id,pt_id,procedure_id,procedure_name,quantity,cost,discount_type,discount,total,discount_inrs,note,status,date,dr_id,completed_id,tooth,ToNurse)" + " values('" + plan_main_id + "','" + patient_id + "','" + procedure_id + "','" + procedure_name + "','" + quantity + "','" + cost + "','" + discount_type + "','" + discount + "','" + total + "','" + discount_inrs + "','" + note + "','0','" + date + "','" + dr_id + "','" + completed_id + "','" + tooth + "','" + nurse + "')", con, trans);
        //}
        public DataTable get_last_paid(string id)
        {
            DataTable dt = db.table("SELECT Patientid,payment_date,fee,visited_date,payment_status FROM tbl_patient_followup WHERE Patientid ='" + id + "' and fee <> 0  order by visited_date desc limit 1 ");//and payment_status <>'Consultation'
            return dt;
        }
        //public DataTable get_last_paid(string id)
        //{
        //    DataTable dt = db.table("SELECT Patientid,payment_date,fee,visited_date,payment_status FROM tbl_patient_followup WHERE Patientid ='" + id + "' and fee <> 0  order by visited_date desc limit 1 ");//and payment_status <>'Consultation'
        //    return dt;
        //}



        // new fasttrack
        public DataTable vitals_grp_visit_main(string patient_id,string date)
        {
            DataTable dt = db.table("select * from tbl_Vital_Signs where pt_id='" + patient_id + "' and date='"+date+"' ");
            return dt;
        }
        public DataTable vitals_grp_visit_para_showmore(string patient_id)
        {
            DataTable dt = db.table("select * from tbl_Vital_Signs where pt_id='" + patient_id + "'ORDER BY date desc  ");
            return dt;
        }
        public DataTable vitals_grp_visit_para_showmore_count(string patient_id,int count)
        {
            DataTable dt = db.table("select * from tbl_Vital_Signs where pt_id='" + patient_id + "'ORDER BY date desc limit " + count + ",2 ");
            return dt;
        }
        public DataTable vitals_grp_visit_para(string patient_id)
        {
            DataTable dt = db.table("select * from tbl_Vital_Signs where pt_id='" + patient_id + "'ORDER BY date desc limit 2 ");
            return dt;
        }
        //model
        public DataTable dt_cf_Complaints(string dt_cf_main)
        {
            System.Data.DataTable dt_cf_Complaints = db.table("SELECT complaint_id FROM tbl_pt_chief_compaints where tbl_pt_chief_compaints.clinical_finding_id='" + dt_cf_main + "' ORDER BY tbl_pt_chief_compaints.id");
            return dt_cf_Complaints;
        }
        public DataTable dt_appointments_showmore(string patient_id)
        {
            DataTable dt = db.table("select book_datetime,start_datetime,pt_id from tbl_appointment where pt_id='" + patient_id + "' order by start_datetime DESC ");
            return dt;
        }
        public DataTable dt_appointments(string patient_id)
        {
            DataTable dt = db.table("select book_datetime,start_datetime,pt_id from tbl_appointment where pt_id='" + patient_id + "' order by start_datetime DESC limit 2");
            return dt;
        }
        public DataTable dt_appointments_showmore(string patient_id, int count)
        {
            DataTable dt = db.table("select book_datetime,start_datetime,pt_id from tbl_appointment where pt_id='" + patient_id + "' order by start_datetime DESC limit " + count + ",2");
            return dt;
        }
        public DataTable dt_appointments_show_coun(string patient_id,int count)
        {
            DataTable dt = db.table("select book_datetime,start_datetime,pt_id from tbl_appointment where pt_id='" + patient_id + "' order by start_datetime DESC limit " + count + ",2");
            return dt;
        }
        public DataTable dt_clinic_main_grp_visi(string patient_id, string date)
        {
            DataTable dt = db.table("select * from tbl_clinical_findings where pt_id='" + patient_id + "' and date='" + date + "'");
            return dt;
        }
        public DataTable dt_clinic_main_grp_para_showmore(string patient_id)
        {
            DataTable dt = db.table("select * from tbl_clinical_findings where pt_id='" + patient_id + "' ORDER BY date desc  ");
            return dt;
        }
        public DataTable dt_clinic_main_grp_para(string patient_id)
        {
            DataTable dt = db.table("select * from tbl_clinical_findings where pt_id='" + patient_id + "' ORDER BY date desc limit 2 ");
            return dt;
        }
        public DataTable dt_clinic_main_grp_para_showcount(string patient_id,int count)
        {
            DataTable dt = db.table("select * from tbl_clinical_findings where pt_id='" + patient_id + "' ORDER BY date desc limit " + count + ",2");
            return dt;
        }
        public DataTable dt_cf_observe(string dt_cf_main)
        {
            System.Data.DataTable dt_cf_observe = db.table("SELECT observation_id FROM tbl_pt_observation where tbl_pt_observation.clinical_finding_id='" + dt_cf_main + "' ORDER BY tbl_pt_observation.id");
            return dt_cf_observe;
        }
        public DataTable dt_cf_investigation(string dt_cf_main)
        {
            System.Data.DataTable dt_cf_investigation = db.table("SELECT investigation_id FROM tbl_pt_investigations where tbl_pt_investigations.clinical_finding_id='" + dt_cf_main + "' ORDER BY tbl_pt_investigations.id");
            return dt_cf_investigation;
        }
        public DataTable dt_cf_diagnosis(string dt_cf_main)
        {
            System.Data.DataTable dt_cf_diagnosis = db.table("SELECT diagnosis_id FROM tbl_pt_diagnosis where tbl_pt_diagnosis.clinical_finding_id='" + dt_cf_main + "' ORDER BY tbl_pt_diagnosis.id");
            return dt_cf_diagnosis;
        }
        public DataTable dt_cf_note(string dt_cf_main)
        {
            System.Data.DataTable dt_cf_note = db.table("SELECT note_name FROM tbl_pt_note where tbl_pt_note.clinical_findings_id='" + dt_cf_main + "' ORDER BY tbl_pt_note.id");
            return dt_cf_note;
        }
        public DataTable dt_cf_allergy(string dt_cf_main)
        {
            System.Data.DataTable dt_cf_allergy = db.table("SELECT allergy_name FROM tbl_pt_allergy where tbl_pt_allergy.clinical_findings_id='" + dt_cf_main + "' ORDER BY tbl_pt_allergy.id");
            return dt_cf_allergy;
        }
        public DataTable dt_cf_Nursenote(string dt_cf_main)
        {
            System.Data.DataTable dt_cf_Nursenote = db.table("SELECT nurses_note FROM tbl_pt_nursesnote where tbl_pt_nursesnote.clinical_finding_id='" + dt_cf_main + "' ORDER BY tbl_pt_nursesnote.id");
            return dt_cf_Nursenote;
        }
        //
        //treatmnt
            public DataTable Load_treatments(string patient_id,string date)
        {
            System.Data.DataTable dt_pt_main = db.table("SELECT id,date,dr_name FROM tbl_treatment_plan_main where pt_id='" + patient_id + "' and date='" + date + "'  ");
            return dt_pt_main;
        }
        public DataTable Load_treatments_para(string patient_id)
        {
            System.Data.DataTable dt_pt_main = db.table("SELECT id,date,dr_name FROM tbl_treatment_plan_main where pt_id='" + patient_id + "' order by date desc  limit 2");
            return dt_pt_main;
        }
        public DataTable Load_treatments_para_showmore(string patient_id)
        {
            System.Data.DataTable dt_pt_main = db.table("SELECT id,date,dr_name FROM tbl_treatment_plan_main where pt_id='" + patient_id + "' order by date desc ");
            return dt_pt_main;
        }
        public DataTable Load_treatments_para_showmore(string patient_id,int count)
        {
            System.Data.DataTable dt_pt_main = db.table("SELECT id,date,dr_name FROM tbl_treatment_plan_main where pt_id='" + patient_id + "' order by date desc  limit " + count + ",2");
            return dt_pt_main;
        }
        public DataTable treatment_sub_details(string id)
        {
            DataTable dt_pt_sub = db.table("SELECT id,procedure_id,procedure_name,cost,discount_inrs,discount_type,discount,total,note,status,tooth,quantity FROM tbl_treatment_plan where plan_main_id='" + id + "' ORDER BY id");
            return dt_pt_sub;
        }
        public DataTable Get_maindtails(string patient_id, string date)
        {
            System.Data.DataTable dt_pre_main = db.table("SELECT tbl_prescription_main.id,tbl_prescription_main.date,tbl_doctor.doctor_name  FROM tbl_prescription_main join tbl_doctor on tbl_prescription_main.dr_id=tbl_doctor.id  where tbl_prescription_main.pt_id='" + patient_id + "' AND tbl_prescription_main.date='" + date + "' ");
            return dt_pre_main;
        }
        public DataTable Get_maindtails_para(string patient_id)
        {
            System.Data.DataTable dt_pre_main = db.table("SELECT tbl_prescription_main.id,tbl_prescription_main.date,tbl_doctor.doctor_name  FROM tbl_prescription_main join tbl_doctor on tbl_prescription_main.dr_id=tbl_doctor.id  where tbl_prescription_main.pt_id='" + patient_id + "' order by date desc limit 2");
            return dt_pre_main;
        }
        public DataTable Get_maindtails_para_showmore(string patient_id)
        {
            System.Data.DataTable dt_pre_main = db.table("SELECT tbl_prescription_main.id,tbl_prescription_main.date,tbl_doctor.doctor_name  FROM tbl_prescription_main join tbl_doctor on tbl_prescription_main.dr_id=tbl_doctor.id  where tbl_prescription_main.pt_id='" + patient_id + "' order by date desc ");
            return dt_pre_main;
        }
        public DataTable Get_maindtails_para_showmore(string patient_id,int count)
        {
            System.Data.DataTable dt_pre_main = db.table("SELECT tbl_prescription_main.id,tbl_prescription_main.date,tbl_doctor.doctor_name  FROM tbl_prescription_main join tbl_doctor on tbl_prescription_main.dr_id=tbl_doctor.id  where tbl_prescription_main.pt_id='" + patient_id + "' order by date desc limit " + count + ",2");
            return dt_pre_main;
        }
        public DataTable prescription_detoails(string id)
        {
            System.Data.DataTable dt_prescription = db.table("SELECT drug_name,strength,duration_unit,duration_period,morning,noon,night,food,add_instruction,drug_type,strength_gr,status FROM tbl_prescription WHERE pres_id='" + id + "' ORDER BY drug_name ASC");
            return dt_prescription;
        }
        public void save_completed_items_trans(int plan_main_id, string patient_id, string procedure_id, string procedure_name, string quantity, string cost, string discount_type, string discount, string total, string discount_inrs, string note, string date, string dr_id, string completed_id, string tooth, string nurse, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("insert into tbl_completed_procedures (plan_main_id,pt_id,procedure_id,procedure_name,quantity,cost,discount_type,discount,total,discount_inrs,note,status,date,dr_id,completed_id,tooth,ToNurse)" + " values('" + plan_main_id + "','" + patient_id + "','" + procedure_id + "','" + procedure_name + "','" + quantity + "','" + cost + "','" + discount_type + "','" + discount + "','" + total + "','" + discount_inrs + "','" + note + "','1','" + date + "','" + dr_id + "','" + completed_id + "','" + tooth + "','" + nurse + "')", con, trans);
        }
        public void save_completed_id_trans(string date, string patient_id, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("insert into tbl_completed_id (completed_date,patient_id,review) values('" + date + "','" + patient_id + "','NO')", con, trans);
        }
        public string get_completedMaxid_trans(MySqlConnection con, MySqlTransaction trans)
        {
            string dt = db.trans_scalar("select MAX(id) from tbl_completed_id", con, trans);
            return dt;
        }
        public string get_completedProcedureMaxid_trans(MySqlConnection con, MySqlTransaction trans)
        {
            string dt = db.trans_scalar("select MAX(id) from tbl_completed_procedures", con, trans);
            return dt;
        }
        public void Save_treatmentgrid(int j, string procedure_id, string pt_id, string procedure_name, string quantity, string cost, string discount_type, string discount, string total, string discount_inrs, string note, string tooth, MySqlConnection con, MySqlTransaction trans)
        {
            int t_p = db.trans_execute("insert into tbl_treatment_plan (plan_main_id,procedure_id,pt_id,procedure_name,quantity,cost,discount_type,discount,total,discount_inrs,note,status,tooth,ToNurse) values('" + j + "','" + procedure_id + "','" + pt_id + "','" + procedure_name + "','" + quantity + "','" + cost + "','" + discount_type + "','" + discount + "','" + total + "','" + discount_inrs + "','" + note + "','0','" + tooth + "','No')", con, trans);
        }
        public void Save_treatmentgrid_set_ststus(int j, string procedure_id, string pt_id, string procedure_name, string quantity, string cost, string discount_type, string discount, string total, string discount_inrs, string note, string tooth, MySqlConnection con, MySqlTransaction trans)
        {
            int t_p = db.trans_execute("insert into tbl_treatment_plan (plan_main_id,procedure_id,pt_id,procedure_name,quantity,cost,discount_type,discount,total,discount_inrs,note,status,tooth,ToNurse) values('" + j + "','" + procedure_id + "','" + pt_id + "','" + procedure_name + "','" + quantity + "','" + cost + "','" + discount_type + "','" + discount + "','" + total + "','" + discount_inrs + "','" + note + "','0','" + tooth + "','Yes')", con, trans);
        }
        public string get_trat_itme_id(MySqlConnection con, MySqlTransaction trans)
        {
            string id = db.trans_scalar("select max(id) from tbl_treatment_plan",con,trans);
            return id;
        }
        public string get_treatmentmaxid(MySqlConnection con, MySqlTransaction trans)
        {
            string dt = db.trans_scalar("select MAX(id) from tbl_treatment_plan_main", con, trans);
            return dt;
        }
        public void Save_treatment(string dr_id, string patient_id, string _date, string _doctor, string _patientname, string _totalcost, string _totaldiscount, string _grandtotal, MySqlConnection con, MySqlTransaction trans)
        {
            int i = db.trans_execute("insert into tbl_treatment_plan_main (date,dr_id,dr_name,pt_id,pt_name,total_cost,total_discount,grand_total) values('" + _date + "','" + dr_id + "','" + _doctor + "','" + patient_id + "','" + _patientname + "','" + _totalcost + "','" + _totaldiscount + "','" + _grandtotal + "')", con, trans);
        }
        //invoice
        public void save_invoice_main(string patient_id, string name, string billno, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("insert into tbl_invoices_main (date,pt_id,pt_name,invoice,status,type,Tonurse_paid) values('" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + patient_id + "','" + name + "','" + billno + "','1','service','False')", con, trans);
        }
        public string get_invoiceMain_maxid(MySqlConnection con, MySqlTransaction trans)//trans
        {
            string dt1 = db.trans_scalar("select MAX(id) from tbl_invoices_main", con, trans);
            return dt1;
        }
        public void save_incove_items(string invoice_no, string pt_name, string pt_id, string service_id, string services, string unit, string cost, string discount, string discount_type, string tax, string total, string date, string notes, string total_cost, string total_discount, string total_tax, string grant_total, string dr_id, string discountin_rs, string tax_inrs,  string main_id,string plan_id,string comple_id, string ToNurse, string lab, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("insert into tbl_invoices(invoice_no,pt_name,pt_id,service_id,services,unit,cost,discount,discount_type,tax,total,date,notes,total_cost,total_discount,total_tax,grant_total,dr_id,discountin_rs,tax_inrs,invoice_main_id,plan_id,completed_id,ToNurse,Lab_service) values('" + invoice_no + "','" + pt_name + "','" + pt_id + "','" + service_id + "','" + services + "','" + unit + "','" + cost + "','" + discount + "','" + discount_type + "','" + tax + "','" + total + "','" + date + "','" + notes + "','" + total_cost + "','" + total_discount + "','" + total_tax + "','" + grant_total + "','" + dr_id + "','" + discountin_rs + "','" + tax_inrs + "','" + main_id + "', '" + plan_id + "','" + comple_id + "','" + ToNurse + "','" + lab + "')", con, trans);
        }
        public void Set_completed_status0(string id, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("update  tbl_completed_procedures set status='0' where id= '" + id + "'", con, trans);
        }
        public void update_invoice_nurse_notify(string check, string id, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("update tbl_invoices_main set Tonurse_paid='" + check + "' where  id= '" + id + "' ", con, trans);
        }

      


    }
}
