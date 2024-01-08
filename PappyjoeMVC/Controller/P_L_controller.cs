using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PappyjoeMVC.Model;
using System.Data;

namespace PappyjoeMVC.Controller
{
  public class P_L_controller
    {
        Expense_Report_model mdl = new Expense_Report_model();
        Common_model cmodel = new Common_model();
        public DataTable Daily_P_L_Account(string date1, string date2)
        {
            DataTable dtb = mdl.Daily_P_L_Account(date1, date2);
            return dtb;
        }
        public DataTable Daily_P_L_count(string d1, string d2)
        {
            DataTable dtb = mdl.Daily_P_L_count(d1, d2);
            return dtb;
        }
        public DataTable Get_practiceDlNumber()
        {
            DataTable dt = cmodel.Get_practiceDlNumber();
            return dt;
        }
    }
}
 