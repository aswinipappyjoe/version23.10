using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PappyjoeMVC.Model;
using System.Data;

namespace PappyjoeMVC.Controller
{
    class finished_procedure_opthalmology_controller
    {
        finished_procedure_opthalmology_model model = new finished_procedure_opthalmology_model();
        Common_model cmodel = new Common_model();
        Treatment_model _model = new Treatment_model();
        Add_Finished_Treatment_model fmodel = new Add_Finished_Treatment_model();
        Daily_Invoice_Report_model dm = new Daily_Invoice_Report_model();
        Treatment_model tmodel = new Treatment_model();
        public void SaveDate(string glass_prescription_date, string patient_id, string Patient_name, string doctor_id, string doctor_name, string L_dist_spherical, string L_dist_cylindrical, string L_dist_axis, string l_near_spherical, string l_near_cylindrical, string l_near_axis, string l_computer_spherical, string l_computer_cylindrical, string l_computer_axis, string r_dist_spherical, string r_dist_cylindrical, string r_dist_axis, string r_near_spherical, string r_near_cylindrical, string r_near_axis, string r_computer_spherical, string r_computer_cylindrical, string r_computer_axis)
        {
            model.SaveDate(glass_prescription_date,  patient_id,  Patient_name,  doctor_id,  doctor_name,  L_dist_spherical,  L_dist_cylindrical,  L_dist_axis,  l_near_spherical,  l_near_cylindrical,  l_near_axis,  l_computer_spherical,  l_computer_cylindrical,  l_computer_axis,  r_dist_spherical,  r_dist_cylindrical,  r_dist_axis,  r_near_spherical, r_near_cylindrical,  r_near_axis,  r_computer_spherical,  r_computer_cylindrical,  r_computer_axis);
        }
        public DataTable Get_Patient_id_NAme(string id)
        {
            DataTable dtb = cmodel.Get_Patient_id_NAme(id);
            return dtb;
        }
        public string Add_privilliege(string doctor_id)
        {
            string privid = fmodel.Add_privilliege(doctor_id);
            return privid;
        }
        public string edit_privillege(string doctor_id)
        {
            string privid = fmodel.edit_privillege(doctor_id);
            return privid;
        }
        public string delete_privillage(string doctor_id)
        {
            string privid = fmodel.delete_privillage(doctor_id);
            return privid;
        }
        public string Get_DoctorName(string doctor_id)
        {
            string dtb = cmodel.Get_DoctorName(doctor_id);
            return dtb;
        }
        public void updateData(string glassid,string glass_prescription_date, string patient_id, string Patient_name, string doctor_id, string doctor_name, string L_dist_spherical, string L_dist_cylindrical, string L_dist_axis, string l_near_spherical, string l_near_cylindrical, string l_near_axis, string l_computer_spherical, string l_computer_cylindrical, string l_computer_axis, string r_dist_spherical, string r_dist_cylindrical, string r_dist_axis, string r_near_spherical, string r_near_cylindrical, string r_near_axis, string r_computer_spherical, string r_computer_cylindrical, string r_computer_axis)
        {
            model.updateData(glassid,glass_prescription_date, patient_id, Patient_name, doctor_id, doctor_name, L_dist_spherical, L_dist_cylindrical, L_dist_axis, l_near_spherical, l_near_cylindrical, l_near_axis, l_computer_spherical, l_computer_cylindrical, l_computer_axis, r_dist_spherical, r_dist_cylindrical, r_dist_axis, r_near_spherical, r_near_cylindrical, r_near_axis, r_computer_spherical, r_computer_cylindrical, r_computer_axis);
        }
        public void delete(string patient_id,string glassid, string glass_prescription_date)
        {
            model.delete(patient_id,glassid, glass_prescription_date);
        }
        public string check_privillege(string doctor_id)
        {
            string a = _model.check_privillege(doctor_id);
            return a;
        }
        public DataTable get_details(string patient)
        {
            DataTable dtb = model.get_details(patient);
            return dtb;
        }
        public DataTable practicedetails()
        {
            DataTable dt = dm.practicedetails();
            return dt;
        }
        public DataTable treatment_sub_details(string id)
        {
            DataTable dtb = tmodel.treatment_sub_details(id);
            return dtb;
        }
        public DataTable get_treatments(string patient_id)
        {
            DataTable dtb = tmodel.Load_treatments(patient_id);
            return dtb;// intr.load_treatment(dtb);
        }
        public string delete_privillege(string doctor_id)
        {
            string a = tmodel.delete_privillege(doctor_id);
            return a;
        }
        public void delete_treatment(string treatment_plan_id)
        {
            tmodel.delete_treatment(treatment_plan_id);
        }
        public DataTable get_plan_id(string treatment_plan_id)
        {
            DataTable dtb = tmodel.get_plan_id(treatment_plan_id);
            return dtb;
        }
        public DataTable Get_Patient_Details(string patient_id)
        {
            DataTable rs_patients = cmodel.Get_Patient_Details(patient_id);
            return rs_patients;
        }
        public DataTable Get_treatment_details(string treatment_plan_id)
        {
            DataTable dtb = tmodel.get_treatments(treatment_plan_id);
            return dtb;
        }
        public DataTable get_company_details()
        {
            DataTable dtp = cmodel.get_company_details();
            return dtp;
        }
        public DataTable getpatemail(string patient_id)
        {
            DataTable sr = cmodel.getpatemail(patient_id);
            return sr;
        }
        public DataTable send_email()
        {
            DataTable sms = cmodel.send_email();
            return sms;
        }
    }
    }
