  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PappyjoeMVC.Model;
using MySql.Data.MySqlClient;

namespace PappyjoeMVC.Controller
{
    public class purchase_controller
    {
        //purchase_interface intr;
        Purchase_model _model = new Purchase_model();
        Inventory_model inv_model = new Inventory_model();
        Common_model cmodel = new Common_model();
        //public purchase_controller()
        //{
        //    //intr = inttr;
        //    //intr.setcontroller(this);
        //}
        public DataTable Get_itemdetails(string itemCode)
        {
            DataTable dtb = _model.Get_itemdetails(itemCode);
            return dtb;
            //intr.Load_item_in_textbox(dtb);
        }
        public DataTable  Get_item_units(string itemCode)
        {
            DataTable dtb = _model.Get_itemdetails(itemCode);
            return dtb;
        }
        public DataTable get_maxpurNumber(string itemCode)
        {
            DataTable dtb = _model.get_maxpurNumber(itemCode);
            return dtb;
        }
        public DataTable Load_Suplier()
        {
            DataTable dtb = _model.Load_Suplier();
            return dtb;
            //intr.Load_Suplier(dtb);
        }
        public DataTable LoadSuplier_wit_supname(string name)
        {
            DataTable dtb = _model.LoadSuplier_wit_supname(name);
            return dtb;
           //intr./*Load_Suplier*/(dtb);
        }
        public DataTable check_batch(string item_code)
        {
            DataTable dtb = _model.check_batch(item_code);
            return dtb;
        }
        public DataTable Get_unites(string Item_Id, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dtb = inv_model.Get_unites(Item_Id, con, trans);
            return dtb;
        }
        public DataTable Get_unites(string Item_Id)
        {
            DataTable dtb = inv_model.Get_unites(Item_Id);
            return dtb;
        }
        public int save_purchase(string PurchNumber, string InvNumber, string PurchDate, string Sup_Code, string TotalAmount, string GrandTotal, string DiscPercentage, string DiscAmount, string TotalCost,string status,string type, string purch_type, MySqlConnection con, MySqlTransaction trans)
        {
            int i = _model.save_purchase(PurchNumber, InvNumber, PurchDate, Sup_Code, TotalAmount, GrandTotal, DiscPercentage, DiscAmount, TotalCost, status, type, purch_type, con, trans);
            return i;
        }
        public int update_purchase(string PurchNumber, string InvNumber, string PurchDate, string Sup_Code, string TotalAmount, string GrandTotal, string DiscPercentage, string DiscAmount, string TotalCost, string status, string type, string purch_type, MySqlConnection con, MySqlTransaction trans)

        {
            int i = _model.update_purchase(PurchNumber, InvNumber, PurchDate, Sup_Code, TotalAmount, GrandTotal, DiscPercentage, DiscAmount, TotalCost, status, type,purch_type, con, trans);
            return i;
        }
        public DataTable check_have_same_batch(string item, string batch, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable bat = _model.check_have_same_batch(item, batch, con,trans);
            return bat;
        }
        public int save_batchNumber(string Item_Code, string BatchNumber, int Qty,decimal rate,decimal sales_rate, string Unit2,string purch_uit2, string UnitMF, string PurchNumber, string PrdDate, string ExpDate, string Period, string Sup_Code, string PurchDate, string IsExpDate,string product_type, MySqlConnection con, MySqlTransaction trans)
        {
            int i = _model.save_batchNumber(Item_Code, BatchNumber, Qty, rate, sales_rate, Unit2, purch_uit2, UnitMF, PurchNumber, PrdDate, ExpDate, Period, Sup_Code, PurchDate, IsExpDate, product_type, con, trans);
            return i;
        }
        public int update_batchNumber(string Item_Code, string BatchNumber, int Qty,decimal rate, decimal sales_rate, string Unit2, string purch_unit2, string UnitMF, string PurchNumber, string PrdDate, string ExpDate, string Period, string Sup_Code, string PurchDate, string IsExpDate, string product_type,string Entry_No, MySqlConnection con, MySqlTransaction trans)

        {
            int i = _model.update_batchNumber(Item_Code, BatchNumber, Qty,rate, sales_rate, Unit2, purch_unit2, UnitMF, PurchNumber, PrdDate, ExpDate, Period, Sup_Code, PurchDate, IsExpDate, product_type, Entry_No, con, trans);
            return i;
        }
        public int update_same_batchNumber(string Item_Code, string BatchNumber, int Qty,  string Unit2, string UnitMF, string PurchNumber, string PrdDate, string ExpDate, string Period, string Sup_Code, string PurchDate, string IsExpDate, string product_type, string Entry_No, MySqlConnection con, MySqlTransaction trans)
        {
            int i = _model.update_same_batchNumber(Item_Code, BatchNumber, Qty,  Unit2, UnitMF, PurchNumber, PrdDate, ExpDate, Period, Sup_Code, PurchDate, IsExpDate, product_type, Entry_No, con, trans);
            return i;
        }
        public void save_purchaseit(string purno, string date, string Item_Code, string Desccription, string barcode, string Packing, string discount, string Unit, string Qty, int FreeQty, string Rate, string Amount, string UNIT2, string UnitMF, decimal GST, decimal IGST, MySqlConnection con, MySqlTransaction trans)
        {
            _model.save_purchaseit(purno, date, Item_Code, Desccription, barcode, Packing, discount, Unit, Qty, FreeQty, Rate, Amount, UNIT2, UnitMF, GST, IGST, con, trans);
        }
        public void update_purchaseit(string PurchNo, string Purc_Date, string Item_Code, string Desccription, string barcode, string Packing, string discount, string Unit, string Qty, int FreeQty, string Rate, string Amount, string UNIT2, string UnitMF, decimal GST, decimal IGST, MySqlConnection con, MySqlTransaction trans)
        {
            _model.update_purchaseit(PurchNo, Purc_Date, Item_Code, Desccription, barcode, Packing, discount, Unit, Qty, FreeQty, Rate, Amount, UNIT2, UnitMF, GST, IGST, con, trans);

        }
        public DataTable get_maxEntryNo(MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dtb = _model.get_maxEntryNo(con, trans);
            return dtb;
        }
        public void save_batchpurchase(string PurchNo, string Purc_Date, string Sup_Code, string Item_Code, string BatchNumber, decimal Qty,decimal rate, string Unit2, string UnitMF, string PrdDate, string ExpDate, string IsExpDate, string BatchEntry, MySqlConnection con, MySqlTransaction trans)
        {
            _model.save_batchpurchase(PurchNo, Purc_Date, Sup_Code,Item_Code, BatchNumber, Qty,rate, Unit2, UnitMF, PrdDate, ExpDate, IsExpDate, BatchEntry,con,trans);
        }
        public void update_batchpurchase(string PurchNo, string Purc_Date, string Sup_Code, string Item_Code, string BatchNumber, decimal Qty,decimal rate, string Unit2, string UnitMF, string PrdDate, string ExpDate, string IsExpDate, string BatchEntry, MySqlConnection con, MySqlTransaction trans)
        {
            _model.update_batchpurchase(PurchNo, Purc_Date, Sup_Code, Item_Code, BatchNumber, Qty,rate, Unit2, UnitMF, PrdDate, ExpDate, IsExpDate, BatchEntry, con,trans);

        }
        public DataTable get_itemdetails(string Itemid, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dtb = _model.get_itemdetails(Itemid,con,trans);
            return dtb;
        }
        public DataTable Get_itemdetails_from_purchaseit(string Itemid, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dtb = inv_model.Get_itemdetails_from_purchaseit(Itemid,con,trans);
            return dtb;
        }
        public void update_itemtable(decimal unitcost, decimal Sales1_, decimal SalesMin_, decimal SalesMax_, decimal costbase1, decimal purchaserate2, decimal Sales2_, decimal SalesMin1_, decimal SalesMax1_, string Item_Id, MySqlConnection con, MySqlTransaction trans)
        {
            _model.update_itemtable(unitcost, Sales1_, SalesMin_, SalesMax_, costbase1, purchaserate2, Sales2_, SalesMin1_, SalesMax1_, Item_Id,con,trans);
        }
        public void update_purchaseorder(int Pur_order_no1, MySqlConnection con, MySqlTransaction trans) 
        {
            _model.update_purchaseorder(Pur_order_no1,con,trans);
        }
        //bhj
        public int save_log(string log_usrid, string log_type, string log_descriptn, string logdate, string logtime, string log_stage,string log_type_id)
        {
            int j = cmodel.save_log(log_usrid, log_type, log_descriptn, logdate,logtime, log_stage,log_type_id);
            return j;
        }
        //public int save_log(string log_usrid, string log_type, string log_descriptn, string logdate, string logtime, string log_stage, string log_type_id)
        //{
        //    int j = cmodel.save_log(log_usrid, log_type, log_descriptn, logdate, logtime, log_stage, log_type_id);
        //    return j;
        //}
        public DataTable incrementDocnumber(MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dtb = _model.trans_incrementDocnumber(con, trans);
            return dtb;
            //intr.DocNumber_increment(dtb);
        }
        public DataTable incrementDocnumber()
        {
            DataTable dtb = _model.incrementDocnumber();
            return dtb;
            //intr.DocNumber_increment(dtb);
        }
        public DataTable get_companydetails()
        {
            DataTable dtb = inv_model.get_companydetails();
            return dtb;
        }
        public DataTable load_purchase_order_details(int Pur_order_no1)
        {
            DataTable dtb = _model.load_purchase_order_details(Pur_order_no1);
            return dtb;
        }
        public DataTable get_batch_sales_rate(string batch, string item)
        {
            DataTable dtb = _model.get_batch_sales_rate(batch, item);
            return dtb;
        }
        public DataTable get_batchpurchase(int invnum_Edit)
        {
            DataTable dtb = _model.get_batchpurchase(invnum_Edit);
            return dtb;
        }
        public DataTable get_batch(int invnum_Edit, string itemcode)//, string unit2
        {
            DataTable dtb = _model.get_batch(invnum_Edit, itemcode);// unit2;
            return dtb;
        }
        public DataTable get_purchase_unit(int invnum_Edit, string item_code, string qty)
        {
            DataTable dtb = _model.get_purchase_unit(invnum_Edit, item_code,qty);
            return dtb;
        }
        public string get_suppliercode(string sup_id)
        {
            string supplier = _model.get_suppliercode(sup_id);
            return supplier;
        }
        public DataTable purchase_batch_data(string itemcode, string purno)
        {
            DataTable dtb = _model.purchase_batch_data(itemcode, purno);
            return dtb;
        }

        public DataTable Get_itemdetails_itemname(string itemname)
        {
            DataTable dtb = _model.Get_itemdetails_itemname(itemname);
            return dtb;
        }
        public DataTable get_itemdetails_Barcode(string name)
        {
            DataTable dtb = _model.get_itemdetails_Barcode(name);
            return dtb;
        }
        public DataTable Search_itemdetails_itemname(string itemname)
        {
            DataTable dtitems = _model.Search_itemdetails_itemname(itemname);
            return dtitems;
        }
        public DataTable Get_consume_tick()
        {
            DataTable dt = inv_model.Get_consume_tick();
            return dt;
        }
        //

        public DataTable increment_Receipt()
        {
            DataTable dtb = _model.increment_Receipt();
            return dtb;
        }
        public DataTable get_openingbalance(string sup_code)
        {
            DataTable dtb = _model.get_openingbalance(sup_code);
            return dtb;
        }
        public void save_voucher(string voucherno, string purchno, string date, string supplierid, string amount, string paymethod, string advance, string opening_balance, string Amount_paid, string Due, string Applied_to, decimal partial_total, MySqlConnection con, MySqlTransaction tran)
        {
            _model.save_voucher( voucherno,  purchno,  date,  supplierid,  amount,  paymethod,  advance,  opening_balance,  Amount_paid,  Due,  Applied_to, partial_total, con, tran);
        }
        public void save_voucher_cheque(string voucherno, string purchno, string date, string supplierid, string amount, string paymethod, string advance, string opening_balance, string Amount_paid, string Due, string Applied_to, string bank, string num, decimal partial_total, MySqlConnection con,MySqlTransaction tran)
        {
            _model.save_voucher_cheque(voucherno, purchno, date, supplierid, amount, paymethod, advance, opening_balance, Amount_paid, Due, Applied_to,bank,num, partial_total,con, tran);
        }
        public void save_voucher_card(string voucherno, string purchno, string date, string supplierid, string amount, string paymethod, string advance, string opening_balance, string Amount_paid, string Due, string Applied_to, string card, string digitnum, decimal partial_total, MySqlConnection con, MySqlTransaction tran)
        {
            _model.save_voucher_card(voucherno, purchno, date, supplierid, amount, paymethod, advance, opening_balance, Amount_paid, Due, Applied_to, card, digitnum, partial_total, con, tran);

        }
        public void save_voucher_dd(string voucherno, string purchno, string date, string supplierid, string amount, string paymethod, string advance, string opening_balance, string Amount_paid, string Due, string Applied_to, string bank, string ddnum, decimal partial_total, MySqlConnection con, MySqlTransaction tran)
        {
            _model.save_voucher_dd(voucherno, purchno, date, supplierid, amount, paymethod, advance, opening_balance, Amount_paid, Due, Applied_to, bank, ddnum, partial_total, con, tran);

        }
        public void save_voucher(string voucherno, string purchno, string date, string supplierid, string amount, string paymethod, string advance, string opening_balance, string Amount_paid, string Due, string Applied_to, decimal partial_total)
        {
            _model.save_voucher(voucherno, purchno, date, supplierid, amount, paymethod, advance, opening_balance, Amount_paid, Due, Applied_to, partial_total);
        }
        public void save_voucher_cheque(string voucherno, string purchno, string date, string supplierid, string amount, string paymethod, string advance, string opening_balance, string Amount_paid, string Due, string Applied_to, string bank, string num, decimal partial_total)
        {
            _model.save_voucher_cheque(voucherno, purchno, date, supplierid, amount, paymethod, advance, opening_balance, Amount_paid, Due, Applied_to, bank, num, partial_total);
        }
        public void save_voucher_card(string voucherno, string purchno, string date, string supplierid, string amount, string paymethod, string advance, string opening_balance, string Amount_paid, string Due, string Applied_to, string card, string digitnum, decimal partial_total)
        {
            _model.save_voucher_card(voucherno, purchno, date, supplierid, amount, paymethod, advance, opening_balance, Amount_paid, Due, Applied_to, card, digitnum, partial_total);

        }
        public void save_voucher_dd(string voucherno, string purchno, string date, string supplierid, string amount, string paymethod, string advance, string opening_balance, string Amount_paid, string Due, string Applied_to, string bank, string ddnum, decimal partial_total)
        {
            _model.save_voucher_dd(voucherno, purchno, date, supplierid, amount, paymethod, advance, opening_balance, Amount_paid, Due, Applied_to, bank, ddnum, partial_total);

        }
        public void update_purchtype(string purno)
        {
            _model.update_purchtype(purno);
        }
        public void update_purch_Amout_status(string purno, string status)
        {
            _model.update_purch_Amout_status(purno, status);
        }
        public void update_purch_Amout_status(string purno, string status, MySqlConnection con, MySqlTransaction tran)
        {
            _model.update_purch_Amout_status(purno, status,con,tran);
        }
        public DataTable get_company_details()
        {
            DataTable dtb = cmodel.get_company_details();
            return dtb;
        }
        public string Get_DoctorName(string id)
        {
            string e = cmodel.Get_DoctorName(id);
            return e;
        }
        public DataTable dtload(string date1, string date2, string supplier)
        {
            DataTable dtb = _model.dtload(date1,date2,supplier);
            return dtb;
        }
        public DataTable get_suppcode(string name)
        {
            DataTable dtb = _model.get_suppcode(name);
            return dtb;
        }
        public DataTable return_details(string purno)
        {
            DataTable dtb = _model.return_details(purno);
            return dtb;
        }
        public void update_advance(string sup_code, string advance)
        {
           _model.update_advance(sup_code, advance);
        }
        public void update_advance(string sup_code, string advance, MySqlConnection con, MySqlTransaction tran)
        {
            _model.update_advance(sup_code, advance,con,tran);
        }
        public void update_due(string sup_code, string advance)
        {
            _model.update_due(sup_code, advance);
        }
        public void update_due(string sup_code, string advance, MySqlConnection con, MySqlTransaction tran)
        {
            _model.update_due(sup_code, advance, con, tran);
        }
        public DataTable get_voucher_details(string purno)
        {
           DataTable dtb=   _model.get_voucher_details(purno);
            return dtb;
        }
    }
}
