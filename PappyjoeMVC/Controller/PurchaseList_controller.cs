using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PappyjoeMVC.Model;

namespace PappyjoeMVC.Controller
{
    public class PurchaseList_controller
    {
        //PurchaseList_interface intr;
        Purchase_model _model = new Purchase_model();
        Inventory_model inv_model = new Inventory_model();
      
        public DataTable Get_consume_tick()
        {
            DataTable dt = inv_model.Get_consume_tick();
            return dt;
        }
        public DataTable getPurchase_btwndates(string fromdate, string todate,string type, string purtype)
        {
            DataTable dt = _model.getPurchase_btwndates(fromdate,todate,type,purtype);
            return dt;
        }
        public DataTable get_due_from_voucher(string purno)
        {
            DataTable dt = _model.get_due_from_voucher(purno);
            return dt;
        }
        public DataTable get_suppcode(string name)
        {
            DataTable dt = _model.get_suppcode(name);
            return dt;
        }
        public DataTable getPurchase_btwndates(string fromdate, string todate, string type, string sup, string purtype)
        {
            DataTable dt = _model.getPurchase_btwndates(fromdate, todate, type, sup,purtype);
            return dt;
        }
        public DataTable data_from_Pur_Master(object dgv_Purchase)
        {
            DataTable d = _model.data_from_Pur_Master(dgv_Purchase);
            return d;
        }
        public DataTable data_from_purchase(object dgv_Purchase)
        {
            DataTable d = _model.data_from_purchase(dgv_Purchase);
            return d;
        }
        public DataTable dt(string fromdate, string todate)
        {
            DataTable d = _model.dt(fromdate,todate);
            return d;
        }
        public DataTable supname()
        {
            DataTable dt = _model.supname();
            return dt;
        }
    }
}
