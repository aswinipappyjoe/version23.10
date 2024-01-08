using System.Data;

namespace PappyjoeMVC.Model
{
    public class Vital_Signs_model
    {
        Connection db = new Connection();
        public DataTable vital(string patient_id)
        {
            DataTable dt = db.table("select * from tbl_Vital_Signs where pt_id='" + patient_id + "' order by date DESC limit 20");
            return dt;
        }
        //bhj
        public DataTable vitals(string patient_id)
        {
            DataTable dt = db.table("select * from tbl_Vital_Signs where pt_id='" + patient_id + "' order by date DESC ");
            return dt;
        }
        public DataTable vitals_print(string patient_id,string id)
        {
            DataTable dt = db.table("select * from tbl_Vital_Signs where pt_id='" + patient_id + "'and main_id='" + id + "' order by date DESC ");
            return dt;
        }
    }
}
