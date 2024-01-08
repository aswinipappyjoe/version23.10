using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PappyjoeMVC.Model;
using System.Data;

namespace PappyjoeMVC.Controller
{
    class TagnameCredit_controller
    {
        TagnameCredit_model model = new TagnameCredit_model();
        public DataTable submit(string tagname)
        {
            DataTable dtb = model.submit(tagname);
            return dtb;
        }
        public int insert(string tagname)
        {
            int i = model.insert(tagname);
            return i;
        }
    }
}
