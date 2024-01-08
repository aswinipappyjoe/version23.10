using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PappyjoeMVC.Model;
using System.Data;

namespace PappyjoeMVC.Controller
{
    class TagnameDebit_controller
    {
        TagnameDebit_model model = new TagnameDebit_model();
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
