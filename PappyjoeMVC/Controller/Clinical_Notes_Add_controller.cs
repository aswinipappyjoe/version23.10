using PappyjoeMVC.Model;
using System.Data;

namespace PappyjoeMVC.Controller
{
    public class Clinical_Notes_Add_controller
    {
        Clinical_Notes_Add_model _model = new Clinical_Notes_Add_model();
        Common_model model = new Common_model();
        Add_New_Patient_model pmodel = new Add_New_Patient_model();
        Patient_Edit_model _emodel = new Patient_Edit_model();
        public DataTable investigation_cell(string idinv)
        {
            DataTable dt = _model.investigation_cell(idinv);
            return dt;
        }
        public string privilge_for_inventory(string doctor_id)
        {
            string s = model.privilge_for_inventory(doctor_id);
            return s;
        }
        public DataTable diagnose_cell(string iddiag)
        {
            DataTable dt = _model.diagnose_cell(iddiag);
            return dt;
        }
        public DataTable complaint_cell(string idcomp)
        {
            DataTable dt = _model.complaint_cell(idcomp);
            return dt;
        }
        public DataTable notes_cell(string idnote)
        {
            DataTable dt = _model.notes_cell(idnote);
            return dt;
        }
        public DataTable observation_cell(string idobs)
        {
            DataTable dt = _model.observation_cell(idobs);
            return dt;
        }
        public DataTable allergy_cell(string idobs)
        {
            DataTable dt = _model.allergy_cell(idobs);
            return dt;
        }
        public DataTable investsearchtxt(string investsearchtext)
        {
            DataTable dt = _model.investsearchtxt(investsearchtext);
            return dt;
        }
        public DataTable diagnosetxtsearch(string diagsearchtext)
        {
            DataTable dt = _model.diagnosetxtsearch(diagsearchtext);
            return dt;
        }
        public DataTable compsearch(string compsearchtext)
        {
            DataTable dt = _model.compsearch(compsearchtext);
            return dt;
        }
        public DataTable notesearch(string notesearchtext)
        {
            DataTable dt = _model.notesearch(notesearchtext);
            return dt;
        }
        public DataTable observsearch(string obsersearchtext)
        {
            DataTable dt = _model.observsearch(obsersearchtext);
            return dt;
        }
        public DataTable allergysearch(string allersearchtext)
        {
            DataTable dt1 = _model.allergysearch(allersearchtext);
            return dt1;
        }
        public DataTable CheckInvest(string investtextbox)
        {
            DataTable dt = _model.CheckInvest(investtextbox);
            return dt;
        }
        public int investigation_insert(string investtextbox)
        {
            int i = _model.investigation_insert(investtextbox);
            return i;
        }
        public DataTable Show_investigation()
        {
            DataTable dt = _model.Show_investigation();
            return dt;
        }
        public DataTable CheckdataDiag(string diagtext)
        {
            DataTable dt = _model.CheckdataDiag(diagtext);
            return dt;
        }
        public int Insert_diagno(string diagtext)
        {
            int i = _model.Insert_diagno(diagtext);
            return i;
        }
        public DataTable show_diagno()
        {
            DataTable dt = _model.show_diagno();
            return dt;
        }
        public DataTable checkdataAcc(string comptext)
        {
            DataTable dt = _model.checkdataAcc(comptext);
            return dt;
        }
        public int insert_compl(string comptextbox)
        {
            int i = _model.insert_compl(comptextbox);
            return i;
        }
        public DataTable show_compl()
        {
            DataTable dt = _model.show_compl();
            return dt;
        }
        public int Update_compl(string comptextbox, int rowvalue)
        {
            int i = _model.Update_compl(comptextbox,rowvalue);
            return i;
        }
        public DataTable checkdataNote(string notetextbox)
        {
            DataTable dt = _model.checkdataNote(notetextbox);
            return dt;
        }
        public int insert_note(string notetextbox)
        {
            int i = _model.insert_note(notetextbox);
            return i;
        }
        public DataTable show_note()
        {
            DataTable d = _model.show_note();
            return d;
        }
        public DataTable show_allerg()
        {
            DataTable dt = _model.show_allerg();
            return dt;
        }
        public DataTable checkdataOB(string obsertextbox)
        {
            DataTable d = _model.checkdataOB(obsertextbox);
            return d;
        }
        public DataTable checkdataAllerg(string allr)
        {
            DataTable d = _model.checkdataAllerg(allr);
            return d;
        }
        public int insert_Observ(string obsertextbox)
        {
            int i = _model.insert_Observ(obsertextbox);
            return i;
        }
        public int insert_Allergy(string allergtextbox)
        {
            int i = _model.insert_Allergy(allergtextbox);
            return i;
        }
        public DataTable show_observation()
        {
            DataTable d = _model.show_observation();
            return d;
        }
        public DataTable patient_search(string _Patientid)
        {
            DataTable dt = model.Patient_search(_Patientid);
            return dt;
        }
        public string doctr_privillage_for_addnewPatient(string doctor_id)
        {
            string s = model.doctr_privillage_for_addnewPatient(doctor_id);
            return s;
        }
        public string permission_for_settings(string doctor_id)
        {
            string s = model.permission_for_settings(doctor_id);
            return s;
        }
        public string userPrivilege_for_ClinicalNotes_Add(string doctor_id)
        {
            string s = _model.userPrivilege_for_ClinicalNotes_Add(doctor_id);
            return s;
        }
        public DataTable Get_CompanyNAme()
        {
            DataTable dt = model.Get_CompanyNAme();
            return dt;
        }
        public string Get_DoctorName(string doctor_id)
        {
            string dt = model.Get_DoctorName(doctor_id);
            return dt;
        }
        public DataTable get_total_payment(string ptid)
        {
            DataTable d = model.get_total_payment(ptid);
            return d;
        }
        public DataTable get_all_doctorname()
        {
            DataTable d = model.get_all_doctorname();
            return d;
        }
        public DataTable Get_patient_id_name_gender(string patient_id)
        {
            DataTable d = model.Get_patient_id_name_gender(patient_id);
            return d;
        }
        public DataTable getdatafrom_clinicalFindings(string id, string ptid)
        {
            DataTable d = _model.getdatafrom_clinicalFindings(id, ptid);
            return d;
        }
        public string get_clinicId(string ptid)
        {
            string cliniid = _model.get_clinicId(ptid);
            return cliniid;
        }
        public DataTable getComplaints(string id)
        {
            DataTable d = _model.getComplaints(id);
            return d;
        }
        public DataTable get_observation(string id)
        {
            DataTable dt = _model.get_observation(id);
            return dt;
        }
        public DataTable get_invest(string id)
        {
            DataTable dt = _model.get_invest(id);
            return dt;
        }
        public DataTable get_diagno(string id)
        {
            DataTable d = _model.get_diagno(id);
            return d;
        }
        public DataTable get_note(string id)
        {
            DataTable d = _model.get_note(id);
            return d;
        }
        public DataTable get_allergy(string id)
        {
            DataTable d = _model.get_allergy(id);
            return d;
        }
        public string Get_DoctorId(string name)
        {
            string d = model.Get_DoctorId(name);
            return d;
        }
        public void insertInto_clinical_findings(string ptid, string dt, string date)
        {
            _model.insertInto_clinical_findings(ptid, dt, date);
        }
        public string MaxId_clinic_findings()
        {
            string dt = _model.MaxId_clinic_findings();
            return dt;
        }
        public void insrtto_investi(int treat, string one)
        {
             _model.insrtto_investi(treat, one);
        }
        public void insrtto_diagno(int treat, string one)
        {
            _model.insrtto_diagno(treat, one);
        }
        public void insrtto_note(int treat, string one)
        {
             _model.insrtto_note(treat, one);
        }
        public void insrtto_obser(int treat, string one)
        {
            _model.insrtto_obser(treat, one);
        }
        public void insrtto_allergy(int treat, string one)
        {
            _model.insrtto_allergy(treat, one);
        }
        public void insrtto_chief_comp(int treat, string one)
        {
          _model.insrtto_chief_comp(treat, one);
        }
        public void insrtto_nurseNote(int treat, string one)
        {
            _model.insrtto_nurseNote(treat, one);
        }
        public int Update_clinical_findings(string ptid, string dt, string id)
        {
            int i = _model.Update_clinical_findings(ptid, dt, id);
            return i;
        }
        public int Update_date_of_clinical(string date, string ptid, string id)
        {
            int i = _model.Update_date_of_clinical(date, ptid, id);
            return i;
        }
        public int del_investi(string id)
        {
            int i = _model.del_investi(id);
            return i;
        }
        public int del_diagno(string id)
        {
            int i = _model.del_diagno(id);
            return i;
        }
        public int del_note(string id)
        {
            int i = _model.del_note(id);
            return i;
        }
        public int del_obser(string id)
        {
            int i = _model.del_obser(id);
            return i;
        }
        public int del_allergy(string id)
        {
            int i = _model.del_allergy(id);
            return i;
        }
        public int del_chiefComp(string id)
        {
            int i = _model.del_chiefComp(id);
            return i;
        }
        public int Add_investi(string id, string grid)
        {
            int i = _model.Add_investi(id, grid);
            return i;
        }
        public int Add_diagno(string id, string grid)
        {
            int i = _model.Add_diagno(id, grid);
            return i;
        }
        //public void save_details(string clinic, string advice, string discharge)
        //{
        //    _model.save_details(clinic, advice, discharge);
        //}
        public int Add_note(string id, string grid)
        {
            int i = _model.Add_note(id, grid);
            return i;
        }
        public int Add_observ(string id, string grid)
        {
            int i = _model.Add_observ(id, grid);
            return i;
        }
        public int Add_allergy(string id, string grid)
        {
            int i = _model.Add_allergy(id, grid);
            return i;
        }
        public int Add_cheifComp(string id, string grid)
        {
            int i = _model.Add_cheifComp(id, grid);
            return i;
        }
        public int Del_Complaints(int row)
        {
            int i = _model.Del_Complaints(row);
            return i;
        }
        public DataTable COMP(int rowvalue)
        {
            DataTable dt = _model.COMP(rowvalue);
            return dt;
        }
        public DataTable load_medical()
        {
            DataTable dtb = pmodel.load_medical();
            return dtb;
        }
        public DataTable check_medical(string name)
        {
            DataTable dtb = pmodel.check_medical(name);
            return dtb;
        }
        public DataTable check_medical_(string pat_id, string medical)
        {
            DataTable dt = _model.check_medical_(pat_id, medical);
            return dt;
        }
        public void save_medical(string pat_id, string medical,string clinic_id)
        {
            _model.save_medical(pat_id, medical, clinic_id);
        }
        public void insert_medical(string Medical)
        {
            pmodel.insert_medical(Medical);
        }
        public DataTable get_medicalId(string pt_id)
        {
            DataTable dtb = _emodel.get_medicalId(pt_id);
            return dtb;
        }
        public DataTable Get_medicalname()
        {
            DataTable dtb = _emodel.Get_medicalname();
            return dtb;
        }
        public string patient_medical(string idd, string medid)
        {
            string dtb = _emodel.patient_medical(idd, medid);
            return dtb;
        }
        //public DataTable get_details(string id)
        //{
        //    DataTable dtb = _model.get_details(id);
        //    return dtb;
        //}
        public void insrtto_pt_meditation(int treat, string one)
        {
            _model.insrtto_pt_meditation(treat, one);
        }
        public DataTable medical_cell(string idobs)
        {
            DataTable dtb = _model.medical_cell(idobs);
            return dtb;
        }
        public void insrtto_pt_advice(int treat, string one)
        {
            _model.insrtto_pt_advice(treat, one);
        }
        public DataTable get_pt_meditation(string id)
        {
            DataTable dtb = _model.get_pt_meditation(id);
            return dtb;
        }
        public DataTable get_pt_advice(string id)
        {
            DataTable dtb = _model.get_pt_advice(id);
            return dtb;
        }
        public DataTable get_nursenotes(string id)
        {
            DataTable dtb = _model.get_nursenotes(id);
            return dtb;
        }
        public int del_meditation(string id)
        {
            int i = _model.del_meditation(id);
            return i;
        }
        public int del_advice(string id)
        {
            int i = _model.del_advice(id);
            return i;
        }
        public int del_medialhistory(string id)
        {
            int i = _model.del_medialhistory(id);
            return i;
        }
        public int save_log(string log_usrid, string log_type, string log_descriptn, string logdate, string logtime, string log_stage,string typeid)
        {
            int j = model.save_log(log_usrid, log_type, log_descriptn, logdate, logtime, log_stage, typeid);
            return j;
        }
    }
}
