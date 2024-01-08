using PappyjoeMVC.Model;
using PappyjoeMVC.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PappyjoeMVC.Controller
{
    public class LabResultEntry_controller
    {
        Common_model cmdl = new Common_model();
        LabResultEntry_model mdl = new LabResultEntry_model();
        public DataTable LoadResult(string patid,string wrkid)
        {
            DataTable dt = mdl.LoadResult(patid,wrkid);
            return dt;
        }
        public int rsltupdate(string rslts,string testid,string id,string rsltmainid)
        {
            int j = mdl.rsltupdate(rslts,testid,id,rsltmainid);
            return j;
        }
        public void update_lab_status(string id, string ptid)
        {
            mdl.update_lab_status(id,ptid);
        }
        public DataTable load_main_value(string patid, string wrkid)
        {
            DataTable dt = mdl.load_main_value(patid, wrkid);
            return dt;
        }
        public DataTable load_result_value(string patid, string wrkid)
        {
            DataTable dt = mdl.load_result_value(patid, wrkid);
            return dt;
        }
        public DataTable get_maintest(string id)
        {
            DataTable dt = mdl.get_maintest(id);
            return dt;
        }
        public DataTable get_testid(string id)
        {
            DataTable dt = mdl.get_testid(id);
            return dt;
        }
        public DataTable get_testtype(string id)
        {
            DataTable dt = mdl.get_testtype(id);
            return dt;
        }
        public DataTable get_testemplate(string id)
        {
            DataTable dt = mdl.get_testemplate(id);
            return dt;
        }
    }
}
