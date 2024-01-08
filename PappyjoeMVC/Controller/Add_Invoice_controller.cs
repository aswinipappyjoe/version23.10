using PappyjoeMVC.Model;
using System;
using System.Data;
using MySql.Data.MySqlClient;
namespace PappyjoeMVC.Controller
{
    public class Add_Invoice_controller
    {
        Connection db = new Connection();
        Add_Invoice_model _model = new Add_Invoice_model();
        Add_Finished_Treatment_model fmodel = new Add_Finished_Treatment_model();
        Common_model cmodel = new Common_model();
        Receipt_Model rcmd = new Receipt_Model();
        public string get_adminid()
        {
            string admin = cmodel.Get_adminId();
            return admin;
        }
        public int updatetotal(decimal total, string invoice_no, string patient_id, string services, MySqlConnection con, MySqlTransaction trans)
        {
          int i=  rcmd.updatetotal(total, invoice_no, patient_id,services, con, trans);
            return i;
        }
        public void update_invoice_status0(decimal invoice_main_id, MySqlConnection con, MySqlTransaction trans)
        {
            rcmd.update_invoice_status0(invoice_main_id,con,trans);
        }
        public int save_payment(string advance, string receipt_no, decimal amount_paid, string invoice_no, string procedure_name, string mode_of_payment, string pt_id, string payment_date, string dr_id, string payment_due, string total, string cost, string pt_name, MySqlConnection con, MySqlTransaction trans)
        {
            int inv = rcmd.save_payment(advance, receipt_no, amount_paid, invoice_no, procedure_name, mode_of_payment, pt_id, payment_date, dr_id, payment_due, total, cost, pt_name,con,trans );
            return inv;
        }
        public DataTable get_receipt_details(string patient_id, string invno,MySqlConnection con,MySqlTransaction trans)
        {
            DataTable dt = _model.get_receipt_details(patient_id, invno,con,trans);
            return dt;
        }
        public string privilge_for_inventory(string doctor_id)
        {
            string s = cmodel.privilge_for_inventory(doctor_id);
            return s;
        }
        public string Load_CompanyName()
        {
            string dtb = cmodel.Load_CompanyName();
            return dtb;
        }
        public DataTable Get_invoice_prefix()
        {
            DataTable invno = _model.Get_invoice_prefix();
            return invno;
        }
        public DataTable load_tax()
        {
            DataTable dtb = _model.load_tax();
            return dtb;
        }
        public DataTable get_completed_procedure_details(string patient_id)
        {
            DataTable dtb = _model.get_completed_procedure_details(patient_id);
            return dtb;
        }
        public DataTable get_patient_lab_details(string patient_id, string date)
        {
            DataTable dtb = _model.get_patient_lab_details(patient_id, date);
            return dtb;
        }
        public DataTable get_template_result(string patient_id, string Resultmain_id)
        {
            DataTable dtb = _model.get_template_result(patient_id, Resultmain_id);
            return dtb;
        }
        public DataTable get_test_result(string patient_id, string Resultmain_id)
        {
            DataTable dtb = _model.get_test_result(patient_id, Resultmain_id);
            return dtb;

        }
        public DataTable dt_tempname(string tempid)
        {
            DataTable dtb = _model.dt_tempname(tempid);
            return dtb;
        }
        public DataTable dt_testname(string tempid)
        {
            DataTable dtb = _model.dt_testname(tempid);
            return dtb;
        }
        public DataTable get_planed_procedure(string patient_id)
        {
            DataTable dtb = _model.get_planed_procedure(patient_id);
            return dtb;
        }
        public DataTable Get_invoice_deatils(string invoiceid)
        {
            DataTable dtb = _model.Get_invoice_deatils(invoiceid);
            return dtb;
        }
        public DataTable load_completed_procedure(string id)
        {
            DataTable dtb = _model.load_completed_procedure(id);
            return dtb;
        }
        public DataTable search_procedure_completed(string patient_id, string search)
        {
            DataTable dt_tp1 = _model.search_procedure_completed(patient_id, search);
            return dt_tp1;
        }
        public DataTable Search_procedure_planed(string patient_id, string search)
        {
            DataTable dt_tp = _model.Search_procedure_planed(patient_id, search);
            return dt_tp;
        }
        public DataTable search_procedures(string search)
        {
            DataTable dt_pt = _model.search_procedures(search);
            return dt_pt;
        }
        public DataTable load_AllProcedures()
        {
            DataTable dtb = fmodel.load_proceduresgrid();
            return dtb;
        }
        public DataTable get_procedure_Name(string id)
        {
            DataTable dtb = fmodel.get_procedure_Name(id);
            return dtb;
        }
        public DataTable Get_treatment_details(string plan_p_id)
        {
            DataTable dtb = fmodel.Get_treatment_details(plan_p_id);
            return dtb;
        }
        public DataTable Get_completed_table_details(string P_Completed_id)
        {
            DataTable dtb = _model.Get_completed_table_details(P_Completed_id);
            return dtb;
        }
        public DataTable Get_quantiry_fromStock(string stock_id)
        {
            DataTable testQty = _model.Get_quantiry_fromStock(stock_id);

            return testQty;
        }
        public DataTable select_All_invoicedata(string billno,MySqlConnection con,MySqlTransaction trans)
        {
            DataTable dtb = _model.select_All_invoicedata(billno, con, trans);
            return dtb;
        }
        public void save_invoice_main(string patient_id, string name, string billno, MySqlConnection con, MySqlTransaction trans)
        {
            _model.save_invoice_main(patient_id, name, billno, con, trans);
        }
        //bh1
        public void save_invoice_main1(string date,string patient_id, string name, string billno, MySqlConnection con, MySqlTransaction trans)
        {
            _model.save_invoice_main1(date,patient_id, name, billno, con, trans);
        }
        public int save_log(string log_usrid, string log_type, string log_descriptn, string dt, string ti, string log_stage)
        {
            int j = _model.save_log(log_usrid, log_type, log_descriptn, dt, ti, log_stage);
            return j;
        }
        public string get_invoiceMain_maxid(MySqlConnection con, MySqlTransaction trans)
        {
            string dt1 = _model.get_invoiceMain_maxid(con, trans);
            return dt1;
        }
        public void save_completedid(string date, string patient_id, MySqlConnection con, MySqlTransaction trans)
        {
            _model.save_completedid(date, patient_id,con,trans);
        }
        public string get_completed_procedure_maxid(MySqlConnection con, MySqlTransaction trans)
        {
            string dt_procedure = _model.get_completed_procedure_maxid(con, trans);
            return dt_procedure;
        }
        public void save_invoice_details(string invoice_no, string pt_name, string patient_id, string service_id, string services, string unit, string cost, string discount, string discount_type, string tax, string total, string notes, string total_discount, string total_tax, string dr_id, string discountin_rs, string tax_inrs, int invoice_main_id, int completed_id,string lab, MySqlConnection con, MySqlTransaction trans)
        {
            _model.save_invoice_details(invoice_no, pt_name, patient_id, service_id, services, unit, cost, discount, discount_type, tax, total, notes, total_discount, total_tax, dr_id, discountin_rs, tax_inrs, invoice_main_id, completed_id,lab,con,trans);
        }
        public void update_tbl_completed_procedures(int plan_main_id, string patient_id, string procedure_id, string procedure_name, string quantity, string cost, string discount_type, string discount, string total, string discount_inrs, string note, string date, string dr_id, string completed_id, string tooth,string lab,MySqlConnection con,MySqlTransaction trans)
        {
            //_model.Save_tbl_completed_procedures(plan_main_id, patient_id, procedure_id, procedure_name, quantity, cost, discount_type, discount, total, discount_inrs, note, dr_id, completed_id, tooth,lab,con ,trans);
            _model.update_tbl_completed_procedures(plan_main_id, patient_id, procedure_id, procedure_name, quantity, cost, discount_type, discount, total, discount_inrs, note,date, dr_id, completed_id, tooth, lab, con, trans);
        }
        public void Save_tbl_completed_procedures(int plan_main_id, string patient_id, string procedure_id, string procedure_name, string quantity, string cost, string discount_type, string discount, string total, string discount_inrs, string note, string dr_id, string completed_id, string tooth, string lab, MySqlConnection con, MySqlTransaction trans)
        {
            _model.Save_tbl_completed_procedures(plan_main_id, patient_id, procedure_id, procedure_name, quantity, cost, discount_type, discount, total, discount_inrs, note, dr_id, completed_id, tooth, lab, con, trans);
            //_model.update_tbl_completed_procedures(plan_main_id, patient_id, procedure_id, procedure_name, quantity, cost, discount_type, discount, total, discount_inrs, note, dr_id, completed_id, tooth, lab, con, trans);
        }
        public void save_incove_items1(string invoice_no, string pt_name, string pt_id, string service_id, string services, string unit, string cost, string discount, string discount_type, string tax, string total, string date, string notes, string total_cost, string total_discount, string total_tax, string grant_total, string dr_id, string discountin_rs, string tax_inrs, string total_payment, string main_id, string completed_id, string ToNurse, string lab, MySqlConnection con, MySqlTransaction trans)
        {
            _model.save_incove_items1(invoice_no, pt_name, pt_id, service_id, services, unit, cost, discount, discount_type, tax, total, date, notes, total_cost, total_discount, total_tax, grant_total, dr_id, discountin_rs, tax_inrs, total_payment, main_id, completed_id, ToNurse, lab, con, trans);
        }
        public void save_invoice_items(string invoice_no, string pt_name, string patient_id, string service_id, string services, string unit, string cost, string discount, string discount_type, string tax, string total, string notes, string total_discount, string total_tax, string dr_id, string discountin_rs, string tax_inrs, int invoice_main_id, string plan_id, int completed_id,string tonurse,string lab,MySqlConnection con,MySqlTransaction trans)
        {
            _model.save_invoice_items(invoice_no, pt_name, patient_id, service_id, services, unit, cost, discount, discount_type, tax, total, notes, total_discount, total_tax, dr_id, discountin_rs, tax_inrs, invoice_main_id, plan_id, completed_id,tonurse,lab,con,trans);
        }
        public void Set_completed_status0(string id, MySqlConnection con, MySqlTransaction trans)
        {
            _model.Set_completed_status0(id,con,trans);
        }
        public void update_invoice_autoid(int a, MySqlConnection con, MySqlTransaction trans)
        {
            _model.update_invoice_autoid(a, con, trans);
        }
        public string Get_invoiceDate(string invno, MySqlConnection con, MySqlTransaction trans)
        {
            string dt0 = _model.Get_invoiceDate(invno,con,trans);
            return dt0;
        }
        public DataTable get_comleted_id(string invoice_plan_id_, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dt = _model.get_comleted_id(invoice_plan_id_, con, trans);
            return dt;
        }
        public DataTable get_cmple_main_id(string invoice_plan_id_, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dt = _model.get_cmple_main_id(invoice_plan_id_, con, trans);
            return dt;
        }
        public void delete_invoice(string billno, MySqlConnection con, MySqlTransaction trans)
        {
            _model.delete_invoice(billno, con, trans);
        }
        public void delete_receipt(string billno,string pt_id, MySqlConnection con, MySqlTransaction trans)
        {
            _model.delete_receipt(billno, pt_id,con,trans);
        }
        public DataTable get_taxValue(string name, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dtb = _model.get_taxValue(name, con, trans);
            return dtb;
        }
        public void save_incove_items(string invoice_no, string pt_name, string pt_id, string service_id, string services, string unit, string cost, string discount, string discount_type, string tax, string total, string date, string notes, string total_cost, string total_discount, string total_tax, string grant_total, string dr_id, string discountin_rs, string tax_inrs, string total_payment, string main_id, string ToNurse, string lab, MySqlConnection con, MySqlTransaction trans)
        {
            _model.save_incove_items(invoice_no, pt_name, pt_id, service_id, services, unit, cost, discount, discount_type, tax, total, date, notes, total_cost, total_discount, total_tax, grant_total, dr_id, discountin_rs, tax_inrs, total_payment, main_id, ToNurse, lab, con, trans);
        }
        public string select_taxValue(string name)
        {
            string dt = _model.select_taxValue(name);
            return dt;
        }
        public DataTable Patient_search(string patid)
        {
            DataTable dtb = cmodel.Patient_search(patid);
            return dtb;
        }
        public string permission_for_settings(string doctor_id)
        {
            string dtb = cmodel.permission_for_settings(doctor_id);
            return dtb;
        }
        public string doctr_privillage_for_addnewPatient(string doctor_id)
        {
            string dtb = cmodel.doctr_privillage_for_addnewPatient(doctor_id);
            return dtb;
        }
        public DataTable get_completed_id(string patient_id, string DTP_Date,MySqlConnection con,MySqlTransaction trans)
        {
            DataTable dt_pt = fmodel.get_completed_id(patient_id, DTP_Date, con,trans);
            return dt_pt;
        }
        public string get_completedMaxid_trans(MySqlConnection con, MySqlTransaction trans)
        {
            string dt = fmodel.get_completedMaxid_trans(con,trans);
            return dt;
        }
        public void update_treatment_status(string id, MySqlConnection con, MySqlTransaction trans)
        {
            fmodel.update_treatment_status(id,con,trans);
        }
        public DataTable getlabprocedure()
        {
            DataTable dt = _model.getlabprocedure();
            return dt;
        }
        public DataTable searchlabprocedure(string qry)
        {
            DataTable dt = _model.searchlabprocedure(qry);
            return dt;
        }
        public DataTable get_labprocedure_Name(string id)
        {
            DataTable dt = _model.get_labprocedure_Name(id);
            return dt;
        }
        public void update_invoice_nurse_notify(string check, string id, MySqlConnection con, MySqlTransaction trans)
        {
            _model.update_invoice_nurse_notify(check, id, con, trans);
        }
        //bhj
        public void update_invoice_main(string date, string patient_id, string name, string billno, MySqlConnection con, MySqlTransaction trans)
        {
            _model.update_invoice_main(date, patient_id, name, billno, con, trans);
        }
        public void delete_completed(string billno, MySqlConnection con, MySqlTransaction trans)
        {
            _model.delete_completed(billno, con, trans);
        }
        public void update_completed_main(string date, string patient_id, string billno, MySqlConnection con, MySqlTransaction trans)
        {
            _model.update_completed_main(date, patient_id, billno, con, trans);
        }

        public DataTable get_proce_wise_cmple_id(string plan_main_id, string procedure_id, MySqlConnection con, MySqlTransaction trans)
        {
          DataTable dt=  _model.get_proce_wise_cmple_id(plan_main_id, procedure_id, con, trans);
            return dt;
        }
    }
}
