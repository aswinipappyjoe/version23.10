using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PappyjoeMVC.Model
{
    class TagnameCredit_model
    {
        Connection db = new Connection();
        public DataTable submit(string tagname)
        {
            DataTable i = db.table("Select * from tbl_expense_tag where tag ='" + tagname + "'");
            return i;
        }
        public int insert(string tagname)
        {
            int i = db.execute("insert into tbl_expense_tag (tag,exp_type) values('" + tagname + "','Cr')");
            return i;
        }
    }
}
