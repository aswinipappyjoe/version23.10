using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PappyjoeMVC.Model
{
    class Add_Treatment_Ophthalmology_model
    {
        Connection db = new Connection();
        public DataTable cornsearch(string cornsearchtext)
        {
            DataTable dt = db.table("select id,name from tbl_addproceduresettings where name like '" + cornsearchtext + "%' and Department='Ophthalmology' and Partstype='Corneal'");
            return dt;
        }
        public DataTable checkdataAcc(string corntext)
        {
            DataTable checkdatacc = db.table("Select * from tbl_addproceduresettings where name ='" + corntext + "'");
            return checkdatacc;
        }
        public int insert_corneal(string corntextbox,string cost)
        {
            int i = db.execute("insert into tbl_addproceduresettings(name,cost,Department,Partstype) values('" + corntextbox + "','" + cost + "','Ophthalmology','Corneal')");
            return i;
        }
        public DataTable get_All_CorneaTreatment()
        {
            DataTable dt3 = db.table("select * from  tbl_addproceduresettings where Department='Ophthalmology' and Partstype='Corneal' order by id");
            return dt3;
        }
        public DataTable get_All_EyemusTreatment()
        {
            DataTable dt3 = db.table("select * from  tbl_addproceduresettings where Department='Ophthalmology' and Partstype='Eye Muscle' order by id");
            return dt3;
        }
        public DataTable get_All_eyelidTreatment()
        {
            DataTable dt3 = db.table("select * from  tbl_addproceduresettings where Department='Ophthalmology' and Partstype='Eyelid' order by id");
            return dt3;
        }
        public DataTable get_All_vitretTreatment()
        {
            DataTable dt3 = db.table("select * from  tbl_addproceduresettings where Department='Ophthalmology' and Partstype='Vitero-Retinal(Retina)' order by id");
            return dt3;
        }
        public DataTable get_All_vitvitTreatment()
        {
            DataTable dt3 = db.table("select * from  tbl_addproceduresettings where Department='Ophthalmology' and Partstype='Vitero-Retinal(Viterous Body)' order by id");
            return dt3;
        }
        public DataTable checkeyemus(string eyemustext)
        {
            DataTable checkdatacc = db.table("Select * from tbl_addproceduresettings where name ='" + eyemustext + "'");
            return checkdatacc;
        }
        public DataTable eyemussearch(string eyemussearchtext)
        {
            DataTable dt = db.table("select id,name from tbl_addproceduresettings where name like '" + eyemussearchtext + "%' and Department='Ophthalmology' and Partstype='Eye Muscle'");
            return dt;
        }
        public DataTable checkeyelid(string eyelidtext)
        {
            DataTable checkdatacc = db.table("Select * from tbl_addproceduresettings where name ='" + eyelidtext + "'");
            return checkdatacc;
        }
        public int insert_eyemus(string eyemustextbox, string cost)
        {
            int i = db.execute("insert into tbl_addproceduresettings(name,cost,Department,Partstype) values('" + eyemustextbox + "','" + cost + "','Ophthalmology','Eye Muscle')");
            return i;
        }
        public int insert_eyelid(string eyelidtextbox, string cost)
        {
            int i = db.execute("insert into tbl_addproceduresettings(name,cost,Department,Partstype) values('" + eyelidtextbox + "','" + cost + "','Ophthalmology','Eyelid')");
            return i;
        }
        public DataTable eyelidsearch(string eyelidsearchtext)
        {
            DataTable dt = db.table("select id,name from tbl_addproceduresettings where name like '" + eyelidsearchtext + "%' and Department='Ophthalmology' and Partstype='Eyelid'");
            return dt;
        }
        public DataTable checkvitret(string vitrettext)
        {
            DataTable checkdatacc = db.table("Select * from tbl_addproceduresettings where name ='" + vitrettext + "'");
            return checkdatacc;
        }
        public int insert_vitret(string txtvitret, string cost)
        {
            int i = db.execute("insert into tbl_addproceduresettings(name,cost,Department,Partstype) values('" + txtvitret + "','" + cost + "','Ophthalmology','Vitero-Retinal(Retina)')");
            return i;
        }
        public DataTable vitretsearch(string vitretsearchtext)
        {
            DataTable dt = db.table("select id,name from tbl_addproceduresettings where name like '" + vitretsearchtext + "%' and Department='Ophthalmology' and Partstype='Vitero-Retinal(Retina)'");
            return dt;
        }
        public DataTable checkvitvit(string vitvittext)
        {
            DataTable checkdatacc = db.table("Select * from tbl_addproceduresettings where name ='" + vitvittext + "'");
            return checkdatacc;
        }
        public int insert_vitvit(string txtvitvit, string cost)
        {
            int i = db.execute("insert into tbl_addproceduresettings(name,cost,Department,Partstype) values('" + txtvitvit + "','" + cost + "','Ophthalmology','Vitero-Retinal(Viterous Body)')");
            return i;
        }
        public DataTable vitvitsearch(string vitvitsearchtext)
        {
            DataTable dt = db.table("select id,name from tbl_addproceduresettings where name like '" + vitvitsearchtext + "%' and Department='Ophthalmology' and Partstype='Vitero-Retinal(Viterous Body)'");
            return dt;
        }
        public DataTable get_ProcedureTreatment(string id)
        {
            DataTable dtb = db.table("select name,cost from tbl_addproceduresettings where id ='" + id + "'");
            return dtb;
        }
        public void Save_treatment(string dr_id, string patient_id, string _date, string _doctor, string _patientname, string _totalcost, string _totaldiscount, string _grandtotal)
        {
            int i = db.execute("insert into tbl_treatment_plan_main (date,dr_id,dr_name,pt_id,pt_name,total_cost,total_discount,grand_total) values('" + _date + "','" + dr_id + "','" + _doctor + "','" + patient_id + "','" + _patientname + "','" + _totalcost + "','" + _totaldiscount + "','" + _grandtotal + "')");
        }
        public string get_treatmentmaxid()
        {
            string dt = db.scalar("select MAX(id) from tbl_treatment_plan_main");
            return dt;
        }
        public void Save_treatmentgrid(int j, string procedure_id, string pt_id, string procedure_name, string quantity, string cost, string discount_type, string discount, string total, string discount_inrs, string note)
        {
            int t_p = db.execute("insert into tbl_treatment_plan (plan_main_id,procedure_id,pt_id,procedure_name,quantity,cost,discount_type,discount,total,discount_inrs,note,status) values('" + j + "','" + procedure_id + "','" + pt_id + "','" + procedure_name + "','" + quantity + "','" + cost + "','" + discount_type + "','" + discount + "','" + total + "','" + discount_inrs + "','" + note + "','1')");
        }
        public DataTable get_all_doctorname()
        {
            DataTable dt = db.table("select DISTINCT id,doctor_name from tbl_doctor  where login_type='doctor' or login_type ='admin' and activate_login='yes' order by doctor_name");
            return dt;
        }
        public DataTable Get_Patient_Details(string id)
        {
            DataTable clinicname = db.table("select * from tbl_patient where id='" + id + "'");
            return clinicname;
        }
    }
}
