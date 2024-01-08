using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PappyjoeMVC.Model
{
    public class LabWorks_model
    {
        Connection db = new Connection();
        public DataTable Getdata(string patid)
        {
            
            DataTable docnam = db.table("SELECT distinct A.id as'Work ID',DATE_FORMAT(A.date,'%d/%m/%Y') as'Date',A.work_name as'Work Name',A.type as 'Work Type',A.trackingstatus as'Status' from tbl_lab_main A inner join tbl_patient B on A.pt_id=B.id inner join tbl_doctor C on A.dr_id=C.id WHERE A.pt_id='" + patid + "' order by A.ID DESC ");
            return docnam;
        }
        public DataTable printdetails(string patid,string workname,string wrkiddental)
        {
            DataTable tbmain = db.table(" SELECT distinct B.work_name,D.Main_test,C.NormalValueM,C.Name,C.NormalValueF,B.id,A.results,A.Units FROM lab_medi_labresult A INNER JOIN tbl_lab_main B ON A.Resultmain_id=B.id INNER JOIN lab_medi_test C ON A.test_id=C.id inner join tbl_lab_medi_maintest D on D.id=A.maintest_id WHERE B.pt_id='" + patid + "' and B.id='" + wrkiddental + "'");
            return tbmain;
        }
        public string seltype(string patid, string id)
        {
            string type = db.scalar("SELECT distinct A.type from tbl_lab_main A inner join tbl_patient B on A.pt_id=B.id inner join tbl_doctor C on A.dr_id=C.id WHERE A.pt_id='" + patid + "'and A.id='" + id + "'");
            return type;
        }
        public string getprev(string doctrid)
        {
            string e = db.scalar("select id from tbl_User_Privilege where UserID=" + doctrid + " and Category='INVENTORY' and Permission='A'");
            return e;
        }
        public DataTable smsinfo()
        {
            DataTable dt = db.table("select smsName,smsPass,emailName,emailPass from tbl_SmsEmailConfig");
            return dt;
        }
        public DataTable get_test_doctor(string patid, string workid)
        {
            DataTable dt = db.table("SELECT DISTINCT B.work_name, B.id, B.dr_id,B.date, A.maintest_id,A.test_id, A.template_id, A.Template_Type FROM Lab_Medi_LabResult A INNER JOIN tbl_lab_main B ON A.Resultmain_id = B.id WHERE B.pt_id = '" + patid + "' AND B.id = '" + workid + "'");
            return dt;
           
        }
        public DataTable get_test_not_main(string patid, string workid)
        {
            DataTable dt = db.table("SELECT DISTINCT B.work_name, B.id, B.dr_id,B.date, A.maintest_id,A.test_id, A.template_id, A.Template_Type FROM Lab_Medi_LabResult A INNER JOIN tbl_lab_main B ON A.Resultmain_id = B.id WHERE B.pt_id = '" + patid + "' AND B.id = '" + workid + "' and A.maintest_id='0'");
            return dt;
        }
        public DataTable tbmain(string patid,string workid)
        {
            DataTable dt = db.table("SELECT distinct B.work_name,D.Main_test,B.id,B.dr_id ,A.maintest_id,A.template_id FROM Lab_Medi_LabResult A INNER JOIN tbl_lab_main B ON A.Resultmain_id=B.id INNER JOIN Lab_Medi_Test C ON A.test_id=C.id inner join tbl_Lab_Medi_MainTest D on D.id=A.maintest_id WHERE B.pt_id='" + patid + "'and B.id='" + workid + "'");
            return dt;
        }
        public DataTable tbshade(string patid,string wrkname,string workid,string tempid)
        {
            DataTable dt = db.table("SELECT D.Main_test,D.id,C.Name,c.id,C.TestTypeID,A.results,C.NormalValueM,C.NormalValueF,A.Units,B.date FROM Lab_Medi_LabResult A INNER JOIN tbl_lab_main B ON A.Resultmain_id=B.id INNER JOIN Lab_Medi_Test C ON A.test_id=C.id inner join tbl_Lab_Medi_MainTest D on D.id=A.maintest_id WHERE B.pt_id='" + patid + "' and B.work_name='" + wrkname + "' and B.id='" + workid+ "' and A.template_id='" + tempid + "' ");
            return dt;
        }
        public DataTable tbshade_(string patid, string wrkname, string workid)
        {
            DataTable dt = db.table("SELECT D.Main_test,D.id,C.Name,c.id,C.TestTypeID,A.results,C.NormalValueM,C.NormalValueF,A.Units,B.date FROM Lab_Medi_LabResult A INNER JOIN tbl_lab_main B ON A.Resultmain_id=B.id INNER JOIN Lab_Medi_Test C ON A.test_id=C.id inner join tbl_Lab_Medi_MainTest D on D.id=A.maintest_id WHERE B.pt_id='" + patid + "' and B.work_name='" + wrkname + "' and B.id='" + workid + "' ");
            return dt;
        }
        public DataTable tb_shade_test(string patid, string workid,string id)
        {
            DataTable dt = db.table("SELECT C.Name,c.id,C.TestTypeID,A.results,C.NormalValueM,C.NormalValueF,A.Units,B.date FROM Lab_Medi_LabResult A INNER JOIN tbl_lab_main B ON A.Resultmain_id=B.id INNER JOIN Lab_Medi_Test C ON A.test_id=C.id WHERE B.pt_id='" + patid + "'  and A.test_id='" + workid + "' and B.id= '" + id + "' ");
            return dt;
        }
        public DataTable tbshade_type(string patid, string wrkname, string workid,string typeid)
        {
            DataTable dt = db.table("SELECT D.Main_test,D.id,C.Name,c.id,C.TestTypeID,A.results,C.NormalValueM,C.NormalValueF,A.Units,B.date FROM Lab_Medi_LabResult A INNER JOIN tbl_lab_main B ON A.Resultmain_id=B.id INNER JOIN Lab_Medi_Test C ON A.test_id=C.id inner join tbl_Lab_Medi_MainTest D on D.id=A.maintest_id WHERE B.pt_id='" + patid + "' and B.work_name='" + wrkname + "' and B.id='" + workid + "' and  C.TestTypeID='"+typeid+"'");
            return dt;
        }
        public DataTable test_type(string MainTestId,string id)
        {
            DataTable dt = db.table("SELECT distinct TestTypeID FROM  lab_medi_template where  MainTestId='" + MainTestId + "' and  Id='" + id + "' ");//and TestId ='" + testid + "'
            return dt;
        }
        public DataTable test_type_name(string id)
        {
            DataTable dt = db.table("select * from tbl_lab_medi_testtype where id='" + id + "'");
            return dt;
        }
        public DataTable dt (string wrkname)
        {
            DataTable dt = new DataTable();
            // dt = db.table("select id from tbl_lab_medi_maintest where Main_test='"+ wrkname + "'");
            //if (dt.Rows.Count>0)
            //{
                // dt = db.table("select distinct id from lab_medi_template where MainTestId='" + dt.Rows[0][0] + "'");
                //if (dt.Rows.Count > 0)
                //{
                    dt = db.table("select TemplateName,id from lab_medi_templatemain where id='"+ wrkname + "'"); //dt.Rows[0][0] 
                                                                                                                        //}
                                                                                                                        //}
            return dt;
        }
        public DataTable docname(string id)
        {
            DataTable dt = db.table("select doctor_name from tbl_doctor where id='" + id + "'");
            return dt;
        }

        //lab notification 
        public DataTable get_patient_notification(string date)
        {
            DataTable dt = db.table("select t.id,t.dr_id,t.pt_id,t.work_name,t.work_id,t.date,p.pt_id as  patientid,p.primary_mobile_number,p.pt_name  from tbl_lab_main t  inner join tbl_patient p on p.id=t.pt_id where t.date='" + date + "'");
            return dt;
        }
        public DataTable get_status(string pt_id,string dr_id,string date)
        {
            DataTable dtb = db.table("select status from tbl_lab_main where pt_id='" + pt_id + "' and dr_id='" + dr_id + "' and date='" + date + "'");
            return dtb;
        }
        public DataTable get_patient_invoice_notification(string pt_id, string invoice_main_id)
        {
            DataTable dt = db.table("select t.id,t.pt_name,t.pt_id,t.service_id,t.services,t.date,t.invoice_main_id,t.dr_id from tbl_invoices t   where t.pt_id='" + pt_id + "' and t.invoice_main_id='" + invoice_main_id + "' and t.Lab_service='Yes'");
            return dt;
        }
        public DataTable get_invoiceid( string date)
        {
            DataTable dtb = db.table("select t.id,t.date,t.pt_id,t.invoice,t.status,p.pt_id as patientid,p.primary_mobile_number,p.pt_name  from tbl_invoices_main t  inner join tbl_patient p on p.id=t.pt_id  where   t.date='" + date + "'");
            return dtb;
        }

    }//select  t.id as mainid,t.date,t.dr_name,t.pt_id,t.pt_name,p.pt_id as  patientid,p.primary_mobile_number from   tbl_treatment_plan_main t inner join tbl_patient p on p.id=t.pt_id  where  t.date='" + date + "'");
}
