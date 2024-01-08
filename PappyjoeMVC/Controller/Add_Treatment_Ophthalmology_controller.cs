using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PappyjoeMVC.Model;

namespace PappyjoeMVC.Controller
{
    class Add_Treatment_Ophthalmology_controller
    {
        Add_Treatment_Ophthalmology_model model = new Add_Treatment_Ophthalmology_model();
        public DataTable cornsearch(string corn)
        {
            DataTable dt = model.cornsearch(corn);
            return dt;
        }
        public DataTable corntxt(string crntxt)
        {
            DataTable dt = model.checkdataAcc(crntxt);
            return dt;
        }
        public int insert_corneal(string corntextbox, string cost)
        {
            int s = model.insert_corneal(corntextbox, cost);
            return s;
        }
        public DataTable get_All_CorneaTreatment()
        {
            DataTable dt = model.get_All_CorneaTreatment();
            return dt;
        }
        public DataTable get_All_EyemusTreatment()
        {
            DataTable dt = model.get_All_EyemusTreatment();
            return dt;
        }
        public DataTable get_All_eyelidTreatment()
        {
            DataTable dt = model.get_All_eyelidTreatment();
            return dt;
        }
        public DataTable get_All_vitretTreatment()
        {
            DataTable dt = model.get_All_vitretTreatment();
            return dt;
        }
        public DataTable get_All_vitvitTreatment()
        {
            DataTable dt = model.get_All_vitvitTreatment();
            return dt;
        }
        public DataTable checkeyemus(string eyemustext)
        {
            DataTable dt = model.checkeyemus(eyemustext);
            return dt;
        }
        public DataTable eyemussearch(string eyemus)
        {
            DataTable dt = model.eyemussearch(eyemus);
            return dt;
        }
        public DataTable checkeyelid(string eyelidtext)
        {
            DataTable dt = model.checkeyelid(eyelidtext);
            return dt;
        }
        public int insert_eyemus(string eyemustextbox, string cost)
        {
            int dt = model.insert_eyemus(eyemustextbox, cost);
            return dt;
        }
        public int insert_eyelid(string eyelidtextbox, string cost)
        {
            int dt = model.insert_eyelid(eyelidtextbox, cost);
            return dt;
        }
        public DataTable eyelidsearch(string eyelidsearchtext)
        {
            DataTable dt = model.eyelidsearch(eyelidsearchtext);
            return dt;
        }
        public DataTable checkvitret(string vitrettext)
        {
            DataTable dt = model.checkvitret(vitrettext);
            return dt;
        }
        public int insert_vitret(string txtvitret, string cost)
        {
            int dt = model.insert_vitret(txtvitret, cost);
            return dt;
        }
        public DataTable vitretsearch(string vitretsearchtext)
        {
            DataTable dt = model.vitretsearch(vitretsearchtext);
            return dt;
        }
        public DataTable checkvitvit(string vitvittext)
        {
            DataTable dt = model.checkvitvit(vitvittext);
            return dt;
        }
        public int insert_vitvit(string txtvitvit, string cost)
        {
            int dt = model.insert_vitvit(txtvitvit, cost);
            return dt;
        }
        public DataTable vitvitsearch(string vitvitsearchtext)
        {
            DataTable dt = model.vitvitsearch(vitvitsearchtext);
            return dt;
        }
        public DataTable get_corneaTreatment(string id)
        {
            DataTable dt = model.get_ProcedureTreatment(id);
            return dt;
        }
        public void Save_treatment(string dr_id, string patient_id, string _date, string _doctor, string _patientname, string _totalcost, string _totaldiscount, string _grandtotal)
        {
            model.Save_treatment(dr_id, patient_id, _date, _doctor, _patientname, _totalcost, _totaldiscount, _grandtotal);
        }
        public string get_treatmentmaxid()
        {
            string dt = model.get_treatmentmaxid();
            return dt;
        }
        public void Save_treatmentgrid(int j, string procedure_id, string pt_id, string procedure_name, string quantity, string cost, string discount_type, string discount, string total, string discount_inrs, string note)
        {
            model.Save_treatmentgrid(j, procedure_id, pt_id, procedure_name, quantity, cost, discount_type, discount, total, discount_inrs, note);
        }
        public DataTable get_all_doctorname()
        {
            DataTable dtb = model.get_all_doctorname();
            return dtb;
        }
        public DataTable Get_Patient_Details(string id)
        {
            DataTable dtb = model.Get_Patient_Details(id);
            return dtb;
        }
    }
}
