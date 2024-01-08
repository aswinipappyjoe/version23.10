using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PappyjoeMVC.Model;
using MySql.Data.MySqlClient;
namespace PappyjoeMVC.Controller
{
    public class Consultation_controller
    {
        Consultation_model model = new Consultation_model();
        Add_Finished_Treatment_model fmodel = new Add_Finished_Treatment_model();
        Prescription_model pmodel = new Prescription_model();
        Common_model cmodel = new Common_model();
        Add_Invoice_model imodel = new Add_Invoice_model();
        SimpleAppointment_model smodel = new SimpleAppointment_model();
        Receipt_Model rmodel = new Receipt_Model();
        Connection db = new Connection();
        public int save_log(string log_usrid, string log_type, string log_descriptn, string logdate, string logtime, string log_stage,string typeid, MySqlConnection con, MySqlTransaction trans)
        {
            int j = cmodel.save_log(log_usrid, log_type, log_descriptn, logdate, logtime, log_stage, typeid, con, trans);
            return j;
        }
        public DataTable search_patient(string search)
        {
            DataTable dtb = cmodel.Patient_search(search); //model.search_patient(search);
            return dtb;
        }
        public DataTable get_prescriptn()
        {
            DataTable dtb = model.get_prescriptn();
            return dtb;
        }
        public DataTable get_tmplates()
        {
            DataTable dtb = model.get_tmplates();
            return dtb;
        }
        public DataTable get_unit()
        {
            DataTable dtb = model.get_unit();
            return dtb;
        }
        public DataTable srch_patient(string ptname/*, string mobno*/)
        {
            DataTable dtb = cmodel.Patient_search(ptname);//model.srch_patient(ptname, mobno);
            return dtb;
        }
        public DataTable patient_details(string value)
        {
            DataTable dtb = model.patient_details(value);
            return dtb;
        }
        public DataTable search_procedure(string search)
        {
            DataTable dtb = model.search_procedure(search);
            return dtb;
        }
        public DataTable procedure_details(string value)
        {
            DataTable dtb = model.procedure_details(value);
            return dtb;
        }
        public DataTable Load_temlate()
        {
            DataTable dtb = model.Load_temlate();
            return dtb;
        }
        public DataTable Load_doctor()
        {
            DataTable dtb = model.Load_doctor();
            return dtb;
        }
        public DataTable Load_dctrname(string doctor_id)
        {
            DataTable dtb = model.Load_dctrname(doctor_id);
            return dtb;
        }
        public DataTable get_procedure()
        {
            DataTable dtb = model.get_procedure();
            return dtb;
        }
        public DataTable get_tempid(string pres_id)
        {
            DataTable dtb = model.get_tempid(pres_id);
            return dtb;
        }
        public DataTable get_templateid(string presid)
        {
            DataTable dtb = model.get_templateid(presid);
            return dtb;
        }
        public DataTable get_invid(string drugid,MySqlConnection con,MySqlTransaction trans )
        {
            DataTable dtb = model.get_invid(drugid, con, trans);
            return dtb;
        }
        public void save_prescriptionMain(string patient_id, int d_id, string Prescription_bill_status, string notes, MySqlConnection con, MySqlTransaction trans)
        {
            model.save_prescriptionMain(patient_id, d_id, Prescription_bill_status, notes, con, trans);
        }
        public string max_presid(MySqlConnection con, MySqlTransaction trans)
        {
            string id = model.max_presid(con, trans);
            return id;
        }
        public void save_prescription(int presid, string patient_id, string dr_name, string dr_id, string drug_name, string strength, string strength_gr,string duration_unit, string duration_period, string morning, string noon, string night, string food, string add_instruction, string drug_type, string status, string drug_id, MySqlConnection con, MySqlTransaction trans)
        {
            model.save_prescription(presid, patient_id, dr_name, dr_id, drug_name, strength, strength_gr, duration_unit, duration_period, morning, noon, night, food, add_instruction, drug_type, status, drug_id, con, trans);
        }
        public void save_completedid(string patient_id, MySqlConnection con, MySqlTransaction trans)
        {
            model.save_completedid(patient_id, con, trans);
        }
        public string max_completedid(MySqlConnection con, MySqlTransaction trans)
        {
            string com_id = model.max_completedid(con, trans);
            return com_id;
        }
        public void save_completed_details(int plan_main_id, string pt_id, string procedure_id, string procedure_name, string cost, string total, string note, string dr_id, MySqlConnection con, MySqlTransaction trans)
        {
            model.save_completed_details(plan_main_id, pt_id, procedure_id, procedure_name, cost, total, note, dr_id,con,trans);
        }
        public string max_completeProcedure(MySqlConnection con, MySqlTransaction trans)
        {
            string max_id = model.max_completeProcedure(con, trans);
            return max_id;
        }
        public void update_review(string date, int j_Review, MySqlConnection con, MySqlTransaction trans)
        {
            fmodel.update_review(date, j_Review,con,trans);
        }
        public void update_prescription_review(string date, int presid, MySqlConnection con, MySqlTransaction trans)
        {
            pmodel.update_prescription_review(date, presid, con, trans);
        }
        public DataTable get_reviewId(string patient_id, string reviewdate, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dtb = cmodel.get_reviewId(patient_id, reviewdate, con, trans);
            return dtb;
        }
        public void save_review(string date, string patient_id, MySqlConnection con, MySqlTransaction trans)
        {
            cmodel.save_review(date, patient_id,con,trans);
        }
        public void update_review_No(string date, int j_Review)
        {
            fmodel.update_review_No(date, j_Review);
        }
        public void update_prescription_review_NO(string date, int presid,MySqlConnection con, MySqlTransaction trans)
        {
            pmodel.update_prescription_review_NO(date, presid,con,trans);
        }
        public DataTable Get_invoice_prefix()
        {
            DataTable dtb = imodel.Get_invoice_prefix();
            return dtb;
        }
        public void save_invoice_main(string patient_id, string pt_name, string invoice, MySqlConnection con, MySqlTransaction trans)
        {
            model.save_invoice_main(patient_id, pt_name, invoice,con,trans);
        }
        public void save_appointment(string date, string patient_id, string pat_name, string dr_id, string Patient_mobile, string dr_name,MySqlConnection con,MySqlTransaction trans)
        {
            cmodel.save_appointment(date, patient_id, pat_name, dr_id, Patient_mobile, dr_name,con,trans);
        }
        public string get_invoiceMain_maxid(MySqlConnection con, MySqlTransaction trans)
        {
            string max_id = imodel.get_invoiceMain_maxid(con, trans);
            return max_id;
        }
        public void save_invoice_details(string invoice, string pt_name, string patient_id, string service_id, string services, string cost, string total, string dr_id, long Invoice_main_id, long completed_procedures_id,MySqlConnection con, MySqlTransaction trans)
        {
            model.save_invoice_details(invoice, pt_name, patient_id, service_id, services, cost, total, dr_id, Invoice_main_id, completed_procedures_id,con,trans);
        }
        public string get_invoicenumber(MySqlConnection con, MySqlTransaction trans)
        {
            string numbr = smodel.get_invoicenumber(con, trans);
            return numbr;
        }
        public void update_invnumber(int invoautoup, MySqlConnection con, MySqlTransaction trans)
        {
            smodel.update_invnumber(invoautoup.ToString(), con, trans);
        }
        public DataTable receipt_number(MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dtb = rmodel.receipt_number(con, trans);
            return dtb;
        }
        public DataTable Get_Advance(string patient_id, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dtb = cmodel.Get_Advance(patient_id, con, trans);
            return dtb;
        }
        public void save_receipt(string receipt, decimal advance, string amount_paid, string invoice, string procedure_name, string patient_id, string dr_id, string total, string cost, string pt_name, long Invoice_main_id, MySqlConnection con, MySqlTransaction trans)
        {
            model.save_receipt(receipt, advance, amount_paid, invoice, procedure_name, patient_id, dr_id, total, cost, pt_name, Invoice_main_id,con,trans);
        }
        public string receipt_autoid(MySqlConnection con, MySqlTransaction trans)
        {
            string numbr = rmodel.receipt_autoid(con, trans);
            return numbr;
        }
        public void update_receiptAutoid(int receip, MySqlConnection con, MySqlTransaction trans)
        {
            rmodel.update_receiptAutoid(receip, con, trans);
        }
        public DataTable get_receipt_details(string payment_date, string patient_id, string receipt)
        {
            DataTable dtb = model.get_receipt_details(payment_date, patient_id, receipt);
            return dtb;
        }
        public DataTable get_payment_details(string payment_date, string patient_id, string receipt)
        {
            DataTable dtb = model.get_payment_details(payment_date, patient_id, receipt);
            return dtb;
        }
        public DataTable get_receipt_print_setting()
        {
            DataTable dtb = model.get_receipt_print_setting();
            return dtb;
        }
        public DataTable printsettings()
        {
            DataTable dtb = pmodel.printsettings();
            return dtb;
        }
        public DataTable get_invoice_data()
        {
            DataTable dtb = model.get_invoice_data();
            return dtb;
        }
        public DataTable get_practicedtls()
        {
            DataTable dt = model.get_practicedtls();
            return dt;
        }
        public DataTable printsettings_details()
        {
            DataTable print = pmodel.printsettings_details();
            return print;
        }
        public string server()
        {
            string server = db.server();
            return server;
        }
        public string Get_DoctorName(string doctor_id)
        {
            string name = cmodel.Get_DoctorName(doctor_id);
            return name;
        }
        public DataTable get_company_details()
        {
            DataTable dtb = cmodel.get_company_details();
            return dtb;
        }
        public DataTable Get_Patient_Details(string id)
        {
            DataTable dtb = cmodel.Get_Patient_Details(id);
            return dtb;
        }
        public DataTable get_patient_details(string newptid)
        {
            DataTable dtb = model.get_patient_details(newptid);
            return dtb;
        }
        public DataTable patient_data(string newptid)
        {
            DataTable dtb = model.patient_data(newptid);
            return dtb;
        }
        public DataTable get_inventoryid(string id)
        {
            DataTable dtb = pmodel.get_inventoryid(id);
            return dtb;
        }
        public DataTable table_details(string prescription_id, string patient_id)
        {
            DataTable dtb = pmodel.table_details(prescription_id, patient_id);
            return dtb;
        }
        public DataTable prescription_details(string id)
        {
            DataTable dt_prescription = pmodel.prescription_detoails(id);
            return dt_prescription;
        }
        public DataTable pt_details(string ptid)
        {
            DataTable dt = model.pt_details(ptid);
            return dt;
        }
        public DataTable drug_instock(string id)
        {
            DataTable dtstock = pmodel.drug_instock(id);
            return dtstock;
        }
        public DataTable get_prescriptnwthname(string pescrptn)
        {
            DataTable dt_prescription = model.get_prescriptnwthname(pescrptn);
            return dt_prescription;
        }
        public DataTable get_doctorname(string id)
        {
            DataTable doctor = model.get_doctorname(id);
            return doctor;
        }
        public DataTable drug_dtls(string id)
        {
            DataTable dt = model.drug_dtls(id);
            return dt;
        }
        public DataTable get_template(string idtemp)
        {
            DataTable dt = pmodel.get_template(idtemp);
            return dt;
        }
        public DataTable get_followup(string id)
        {
            DataTable dt = model.get_followup(id);
            return dt;
        }
        public void save_followup(string Patientid, string patient_name, string visited_date, string payment_date, string doctor, string fee,string payment_status, MySqlConnection con, MySqlTransaction trans)
        {
            model.save_followup(Patientid, patient_name, visited_date, payment_date, doctor, fee, payment_status,con,trans);
        }
        public string get_consultation()
        {
            string str = model.get_consultation();
            return str;
        }
        public DataTable get_last_paid(string id)
        {
            DataTable dt = model.get_last_paid(id);
            return dt;
        }
        public DataTable get_patient_doctor(string dr_id, string pt_id)
        {
            DataTable dt = model.get_patient_doctor(dr_id, pt_id);
            return dt;
        }
    }
}
