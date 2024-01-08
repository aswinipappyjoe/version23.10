using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PappyjoeMVC.Model
{
    public class LabResultEntry_model
    {
        Connection db = new Connection();
        public DataTable LoadResult(string patid,string wrkid)
        {
            DataTable dt = db.table("SELECT distinct '' as 'SlNo', B.Main_test as'Main Test',D.Name as 'Test Type',C.Name as 'Test',A.results as 'Result',F.Units,F.Normalvalue as 'Normal Value',C.id as 'Typeid',A.id as 'id',A.Resultmain_id FROM Lab_Medi_LabResult A INNER JOIN tbl_Lab_Medi_MainTest B ON A.maintest_id=B.id INNER JOIN Lab_Medi_Test C ON A.test_id=C.id INNER JOIN tbl_Lab_Medi_TestType D On D.id=C.TestTypeID  inner join tbl_lab_main E ON E.id=A.Resultmain_id inner join Lab_Medi_Template F on F.TestId=C.id WHERE E.pt_id='" + patid+ "' and E.id='" + wrkid + "'");
            return dt;
        }
        public int rsltupdate(string rslts, string testid, string id, string rsltmainid)
        {
            int j = db.execute("UPDATE Lab_Medi_LabResult SET results='" + rslts + "' WHERE test_id='" + testid + "' and id='" + id + "' and Resultmain_id='" + rsltmainid + "'");
            return j;
        }

        public void update_lab_status(string id ,string ptid)
        {
            db.execute("update tbl_lab_main set status='Completed' where id='" + id+"' and pt_id='"+ptid+"'");
        }
        public DataTable load_main_value(string patid, string wrkid)
        {
            DataTable dt = db.table(" SELECT * FROM tbl_lab_main E  WHERE E.pt_id = '" + patid + "' and E.id='" + wrkid + "'");
            return dt;
        }
        public DataTable load_result_value(string patid, string wrkid)
        {
            DataTable dt = db.table("SELECT  A.results , A.id , A.Resultmain_id,A.maintest_id, A.test_id,A.Units,A.Normalvalue FROM Lab_Medi_LabResult A  where pt_id = '" + patid + "' and A.Resultmain_id ='" + wrkid + "' ");
            return dt;
        }

        public DataTable get_maintest(string id)
        {
            DataTable dt = db.table("select * from tbl_Lab_Medi_MainTest where id='" + id + "'");
            return dt;
                
        }
        public DataTable get_testid(string id)
        {
            DataTable dt=db.table("select * from Lab_Medi_Test where id='" + id + "' ");
            return dt;
        }
        public DataTable get_testtype(string id)
        {
            DataTable dt = db.table("select * from tbl_Lab_Medi_TestType where id='" + id + "'");
                return dt;
        }
        public DataTable get_testemplate(string id)
        {
            DataTable dt = db.table("select * from Lab_Medi_Template  where TestId ='" + id + "'");
            return dt;
        }

    }
}
