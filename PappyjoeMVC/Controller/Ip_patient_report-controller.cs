using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PappyjoeMVC.Model;
using System.Data;

namespace PappyjoeMVC.Controller
{
  public  class Ip_patient_report_controller
    {
        Patients_Report_model model = new Patients_Report_model();
        public DataTable load_ippatient(string dtp1, string dtp2)
        {
            DataTable dtb = model.load_ippatient(dtp1, dtp2);
            return dtb;
        }
        public DataTable Get_practiceDlNumber()
        {
            DataTable d = model.Get_practiceDlNumber();
            return d;
        }
        public DataTable dt_ip_only(string dtp1, string dtp2)
        {
            DataTable dtb = model.dt_ip_only(dtp1, dtp2);
            return dtb;
        }
    }
}
