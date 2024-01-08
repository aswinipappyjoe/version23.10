using System.Data;
namespace PappyjoeMVC.Model
{
    public class Procedure_Catalog_model
    {
        Connection db = new Connection();
        public DataTable FormLoad()
        {
            DataTable dt = db.table(" select A.id,A.name,A.cost,A.category,A.notes,T.tax_name,A.sac from tbl_addproceduresettings A left join tbl_proceduretax  P ON P.procedure_id = A.id left join tbl_tax T on T.id= P.tax_id ORDER BY A.id Desc limit 40");
            return dt;
        }
        public DataTable FormLoad_ShMre(int limit)
        {
            DataTable dt = db.table(" select A.id,A.name,A.cost,A.category,A.notes,T.tax_name,A.sac from tbl_addproceduresettings A left join tbl_proceduretax  P ON P.procedure_id = A.id left join tbl_tax T on T.id= P.tax_id ORDER BY A.id Desc limit " + limit + ",10");
            return dt;
        }
        public DataTable get_procedure_category_value()
        {
            DataTable dt = db.table("select * from tbl_procedure_category");
            return dt;
        }
        public DataTable Get_category_name(string TextCategory)
        {
            DataTable dt = db.table("select * from tbl_procedure_category where name='" + TextCategory + "'");
            return dt;
        }
        public void save(string TextCategory)
        {
            db.execute("insert into tbl_procedure_category (name) values('" + TextCategory + "')");
        }
        public DataTable get_procedureName(string _procName)
        {
            DataTable checkdatacc = db.table("Select id,name from tbl_addproceduresettings where name ='" + _procName + "'");
            return checkdatacc;
        }
        public int save_addprocedure(string _procName, string _procost,string _sac, string _comboCategory, string _notes)
        {
            int i = db.execute("insert into tbl_addproceduresettings (name,cost,sac,category,notes) values('" + _procName + "','" + _procost + "','"+_sac+"','" + _comboCategory + "','" + _notes + "')");
            return i;
        }
        public int update_addprocedure(string _procName, string _procost,string _sac, string _comboCategory, string _notes,string pro_id)
        {
            int i = db.execute("update  tbl_addproceduresettings set name='" + _procName + "',cost='" + _procost + "',sac='"+_sac+"',category='" + _comboCategory + "',notes='" + _notes + "' where id='"+pro_id+"'");
            return i;
        }
        public string Get_GST_id()
        {
            string s = db.scalar("select id from tbl_tax where tax_name= 'GST' ");
            return s;
        }
        public void save_proceduretax(int id1, int pid)
        {
            int j = db.execute("insert into tbl_proceduretax (tax_id,procedure_id) values('" + id1 + "','" + pid + "')");
        }
        public void update_proceduretax(int id1, int pid)
        {
            int j = db.execute("update  tbl_proceduretax set tax_id='" + id1 + "' where procedure_id='" + pid + "'");
        }
        public string Get_IGST_id()
        {
            string s = db.scalar("select id from tbl_tax where tax_name= 'IGST'");
            return s;
        }
        public DataTable srchprocedure(string name)
        {
            DataTable dtb = db.table(" select A.id,A.name,A.cost,A.category,A.notes,T.tax_name from tbl_addproceduresettings A left join tbl_proceduretax  P ON P.procedure_id = A.id left join tbl_tax T on T.id= P.tax_id where name like '" + name + "%'  ORDER BY A.id Desc");
            return dtb;
        }
        public int delproceduretax(int procid)
        {
            int i = db.execute("delete from tbl_proceduretax where procedure_id='" + procid + "'");
            return i;
        }
        public int delprocdresetngs(int procid)
        {
            int ii = db.execute("delete from tbl_addproceduresettings where id='" + procid + "'");
            return ii;
        }
        public DataTable check_procedureid(string procid)
        {
            DataTable dtb = db.table("select id,procedure_id from tbl_completed_procedures where procedure_id='" + procid + "'");
            return dtb;
        }
        public void save_enablecost(string cost)
        {
            db.execute("insert into tbl_enable_cost (enable_cost) values('"+ cost + "')");
        }
        public DataTable  get_enablecost()
        {
            DataTable dt= db.table("select * from  tbl_enable_cost");
            return dt;
        }
        public void update_enablecost(string cost)
        {
            db.execute("update tbl_enable_cost set enable_cost ='" + cost + "' ");
        }
    }
}
