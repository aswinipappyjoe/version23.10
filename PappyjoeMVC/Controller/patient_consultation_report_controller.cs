using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PappyjoeMVC.Model;
namespace PappyjoeMVC.Controller
{
   public  class patient_consultation_report_controller
    {
        patient_consultation_report_model mdl = new patient_consultation_report_model();
        Common_model cmodel = new Common_model();
        public DataTable Load_doctor()
        {
            DataTable dtb = mdl.Load_doctor();
            return dtb;
        }
        public DataTable load_data(string from, string to)
        {
            DataTable dtb = mdl.load_data(from ,to);
            return dtb;
        }
        public DataTable get_patient_details(string pt_id)
        {
            DataTable dtb = mdl.get_patient_details(pt_id);
            return dtb;
        }
        public DataTable get_doctor_fee(string dr_id)
        {
            DataTable dtb = mdl.get_doctor_fee(dr_id);
            return dtb;
        }
        public DataTable load_data_doctor_wisw(string from, string to, string dr_id)
        {
            DataTable dtb = mdl.load_data_doctor_wisw(from,to,dr_id);
            return dtb;
        }
        public DataTable get_patients(string newptid)
        {
            DataTable dtb = mdl.get_patients(newptid);
            return dtb;
        }
        public DataTable srch_patient(string ptname/*, string mobno*/)
        {
            DataTable dtb = cmodel.Patient_search(ptname);//model.srch_patient(ptname, mobno);
            return dtb;
        }
        public DataTable patient_wise_consultation(string pt_id)
        {
            DataTable dtb = mdl.patient_wise_consultation(pt_id);
            return dtb;
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
    }
}
