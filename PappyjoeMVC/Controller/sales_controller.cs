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
   public class sales_controller
    {
        sales_model _model = new sales_model();
        Common_model cmodel = new Common_model();
        Inventory_model inv_model = new Inventory_model();  
        Connection db = new Connection(); Prescription_model pmodel = new Prescription_model();
        public DataTable get_itemdetails(string name)
        {
            DataTable dtb = _model.get_itemdetails(name);
            return dtb;
        }
        public DataTable Get_itemdetails_itemname(string itemname)
        {
            DataTable dtb = _model.Get_itemdetails_itemname(itemname);
            return dtb;
        }
        public DataTable Search_itemdetails_itemname(string itemname)
        {
            DataTable dtitems = _model.Search_itemdetails_itemname(itemname);
            return dtitems;
        }
        public DataTable get_itemdetails_BArcode(string name)
        {
            DataTable dtb = _model.get_itemdetails_Barcode(name);
            return dtb;
        }
        public DataTable get_pur_gst(string name)
        {
            DataTable dtb = _model.get_pur_gst(name);
            return dtb;
        }
        public DataTable GetDoctorName(string name)
        {
            DataTable dtb = _model.GetDoctorName(name);
            return dtb;
        }
        public DataTable get_doctorname_by_id(string value)
        {
            DataTable dtb = _model.get_doctorname_by_id(value);
            return dtb;
        }
        public DataTable patients(string name)
        {
            DataTable dtb = _model.patients(name);
            return dtb;
        }
        public DataTable Get_Advance(string patient_id)
        {
            DataTable dtb = _model.Get_Advance(patient_id);
            return dtb;
        }
        public DataTable patient_keydown(string value)
        {
            DataTable dtb = _model.patient_keydown(value);
            return dtb;
        }
        public DataTable itemdetails(string itemid)
        {
            DataTable dtb = _model.itemdetails(itemid);
            return dtb;
        }
        public DataTable Get_stock(string itemid)
        {
            DataTable dtb = _model.Get_stock(itemid);
            return dtb;
        }
        public DataTable get_sales_default_qty(string itemid)
        {
            DataTable dtb = _model.get_sales_default_qty(itemid);
            return dtb;
        }
        public void save_default_qty(string qty, string batchentry)
        {
            _model.save_default_qty(qty, batchentry);
        }
        public DataTable check_sales_defaut_qty(string batchentry)
        {
           DataTable dtb= _model.check_sales_defaut_qty( batchentry);
            return dtb;

        }
        public DataTable get_item_unitmf(string Itemid)
        {
            DataTable dtb = inv_model.get_item_unitmf(Itemid);
            return dtb;
        }
        public DataTable get_salesrate_unit(string itemid)
        {
            DataTable dtb = _model.get_salesrate_unit(itemid);
            return dtb;
        }
        public DataTable dt_itemdetails(string itemid)
        {
            DataTable dtb = _model.dt_itemdetails(itemid);
            return dtb;
        }
        public DataTable sales_details(int invnum_Edit)
        {
            DataTable dtb = _model.sales_details(invnum_Edit);
            return dtb;
        }
        public DataTable get_hsn(string id)
        {
            DataTable dtb = _model.get_hsn(id);
            return dtb;
        }
        public DataTable sales_items_details(int invnum_Edit)
        {
            DataTable dtb = _model.sales_items_details(invnum_Edit);
            return dtb;
        }
        public DataTable get_batchsale(int invnum_Edit)
        {
            DataTable dtb = _model.get_batchsale(invnum_Edit);
            return dtb;
        }
        public DataTable batcdetails(int invnum_Edit, string unit2)
        {
            DataTable dtb = _model.batcdetails(invnum_Edit, unit2);
            return dtb;
        }
        public DataTable batchnumbetails(string invnum_Edit)
        {
            DataTable dtb = _model.batchnumbetails(invnum_Edit);
            return dtb;
        }
        public DataTable load_prescription()
        {
            DataTable dtb = _model.load_prescription();
            return dtb;
        }
        public void change_pres_status(string id)
        {
            _model.change_pres_status(id);
        }
        public DataTable docnumber()
        {
            DataTable dtb = _model.docnumber();
            return dtb;
        }
        public int delete_sales_main(string inv)
        {
            int i = _model.delete_sales_main(inv);
            return i;
        }
        public int delete_sales_it(string inv)
        {
            int i = _model.delete_sales_it(inv);
            return i;
        }
        public DataTable get_sales_items_details(string inv)
        {
            DataTable dtb = _model.get_sales_items_details(inv);
            return dtb;
        }
        public DataTable get_batchsales_items_details(string inv)
        {
            DataTable dtb = _model.get_batchsales_items_details(inv);
            return dtb;
        }
        public DataTable salesOrder_master(int invnum_order)
        {
            DataTable dtb = _model.salesOrder_master(invnum_order);
            return dtb;
        }
        public DataTable get_doctor(string doctor_id)
        {
            DataTable dtb = _model.get_doctor(doctor_id);
            return dtb;
        }
        public DataTable Get_CompanyNAme()
        {
            DataTable dtb = cmodel.Get_CompanyNAme();
            return dtb;
        }
        public string server()
        {
            string server = db.server();
            return server;
        }
        public DataTable order_itemsDtails(int invnum_order)
        {
            DataTable dtb = _model.order_itemsDtails(invnum_order);
            return dtb;
        }
        public int Save_salesMaster_cheque(int DocNo, string Docdate, string sales_, string ordeNo, string Date_, string doctor_, string lrno, string lr_date, string throuhg, string ptid, string ptname, string street__, string locality, string city, string phone, string payMethod, string Bank, string Number, decimal totalAmnt, decimal disount, decimal gst, decimal igst, decimal gTotal,string type)
        {
            int i = _model.Save_salesMaster_cheque(DocNo, Docdate, sales_, ordeNo, Date_, doctor_, lrno, lr_date, throuhg, ptid, ptname, street__, locality, city, phone, payMethod, Bank, Number, totalAmnt, disount, gst, igst, gTotal, type);
            return i;
        }
        public int Save_salesMaster_card(int DocNo, string Docdate, string sales_, string ordeNo, string Date_, string doctor_, string lrno, string lr_date, string throuhg, string ptid, string ptname, string street__, string locality, string city, string phone, string payMethod, string cardnumber, string fourdigitnumber, decimal totalAmnt, decimal disount, decimal gst, decimal igst, decimal gTotal, string type)
        {
            int i = _model.Save_salesMaster_card(DocNo, Docdate, sales_, ordeNo, Date_, doctor_, lrno, lr_date, throuhg, ptid, ptname, street__, locality, city, phone, payMethod, cardnumber, fourdigitnumber, totalAmnt, disount, gst, igst, gTotal, type);
            return i;
        }
        public int Save_salesMaster_DD(int DocNo, string Docdate, string sales_, string ordeNo, string Date_, string doctor_, string lrno, string lr_date, string throuhg, string ptid, string ptname, string street__, string locality, string city, string phone, string payMethod, string Bank, string DDnumber, decimal totalAmnt, decimal disount, decimal gst, decimal igst, decimal gTotal, string type)
        {
            int i = _model.Save_salesMaster_DD(DocNo, Docdate, sales_, ordeNo, Date_, doctor_, lrno, lr_date, throuhg, ptid, ptname, street__, locality, city, phone, payMethod, Bank, DDnumber, totalAmnt, disount, gst, igst, gTotal, type);
            return i;
        }
        public int Save_salesMaster(int DocNo, string Docdate, string sales_, string ordeNo, string Date_, string doctor_, string lrno, string lr_date, string throuhg, string ptid, string ptname, string street__, string locality, string city, string phone, string payMethod, decimal totalAmnt, decimal disount, decimal gst, decimal igst, decimal gTotal, string type,MySqlConnection con,MySqlTransaction trans)
        {
          int i=   _model.Save_salesMaster(DocNo, Docdate, sales_, ordeNo, Date_, doctor_, lrno, lr_date, throuhg, ptid, ptname, street__, locality, city, phone, payMethod, totalAmnt, disount, gst, igst, gTotal, type,con,trans);
            return i;
        }
        public int save_log(string log_usrid, string log_type, string log_descriptn, string logdate, string logtime, string log_stage,string typeid)
        {
            int j = cmodel.save_log(log_usrid, log_type, log_descriptn, logdate, logtime, log_stage, typeid);
            return j;
        }
        public int update_salesMaster(int DocNo, string Docdate, string sales_, string ordeNo, string Date_, string doctor_, string lrno, string lr_date, string throuhg, string ptid, string ptname, string street__, string locality, string city, string phone, string payMethod, decimal totalAmnt, decimal disount, decimal gst, decimal igst, decimal gTotal, string type,MySqlConnection con,MySqlTransaction trans)
        {
            int i = _model.update_salesMaster(DocNo, Docdate, sales_, ordeNo, Date_, doctor_, lrno, lr_date, throuhg, ptid, ptname, street__, locality, city, phone, payMethod, totalAmnt, disount, gst, igst, gTotal, type,con,trans);
            return i;
        }
        public int update_salesMaster_cheque(int DocNo, string Docdate, string sales_, string ordeNo, string Date_, string doctor_, string lrno, string lr_date, string throuhg, string ptid, string ptname, string street__, string locality, string city, string phone, string payMethod, string Bank, string Number, decimal totalAmnt, decimal disount, decimal gst, decimal igst, decimal gTotal, string type)
        {
            int i = _model.update_salesMaster_cheque(DocNo, Docdate, sales_, ordeNo, Date_, doctor_, lrno, lr_date, throuhg, ptid, ptname, street__, locality, city, phone, payMethod, Bank, Number, totalAmnt, disount, gst, igst, gTotal, type);
            return i;
        }
        public int update_salesMaster_card(int DocNo, string Docdate, string sales_, string ordeNo, string Date_, string doctor_, string lrno, string lr_date, string throuhg, string ptid, string ptname, string street__, string locality, string city, string phone, string payMethod, string cardnumber, string fourdigitnumber, decimal totalAmnt, decimal disount, decimal gst, decimal igst, decimal gTotal, string type)
        {
            int i = _model.update_salesMaster_card(DocNo, Docdate, sales_, ordeNo, Date_, doctor_, lrno, lr_date, throuhg, ptid, ptname, street__, locality, city, phone, payMethod, cardnumber, fourdigitnumber, totalAmnt, disount, gst, igst, gTotal, type);
            return i;
        }
        public int update_salesMaster_DD(int DocNo, string Docdate, string sales_, string ordeNo, string Date_, string doctor_, string lrno, string lr_date, string throuhg, string ptid, string ptname, string street__, string locality, string city, string phone, string payMethod, string Bank, string DDnumber, decimal totalAmnt, decimal disount, decimal gst, decimal igst, decimal gTotal, string type)
        {
            int i = _model.update_salesMaster_DD(DocNo, Docdate, sales_, ordeNo, Date_, doctor_, lrno, lr_date, throuhg, ptid, ptname, street__, locality, city, phone, payMethod, Bank, DDnumber, totalAmnt, disount, gst, igst, gTotal, type);
            return i;
        }
        public DataTable get_costbase(string itemcode,MySqlConnection con,MySqlTransaction trans)
        {
            DataTable dtb = _model.get_costbase(itemcode,con,trans);
            return dtb;
        }
        public DataTable get_batch_wise_stock(string entryno)
        {
            DataTable dtb = _model.get_batch_wise_stock(entryno);
            return dtb;
        }
        //public int Save_itemdetails(int DocNo, string Docdate, string Item_Code, string Description, string Packing, string Unit, decimal GST, decimal IGST, decimal Qty, decimal  FreeQty, decimal Rate, decimal TotalAmount, string UNIT2, decimal UnitMF, decimal CostBase,MySqlConnection con,MySqlTransaction trans)
        //{
        //  int i= _model.Save_itemdetails(DocNo, Docdate,Item_Code, Description, Packing, Unit, GST, IGST, Qty, FreeQty, Rate, TotalAmount, UNIT2, UnitMF, CostBase,con,trans);
        //    return i;
        //}
        public int Save_itemdetails(int DocNo, string Docdate, string Item_Code, string Description, string Packing,string batch,string b_entry, string Unit, decimal GST, decimal IGST, decimal Discount, decimal Qty, decimal FreeQty, decimal Rate, decimal TotalAmount, string UNIT2, decimal UnitMF, decimal CostBase, MySqlConnection con, MySqlTransaction trans)
        {
            int i = _model.Save_itemdetails(DocNo, Docdate, Item_Code, Description, Packing, batch,b_entry, Unit, GST, IGST, Discount, Qty, FreeQty, Rate, TotalAmount, UNIT2, UnitMF, CostBase, con, trans);
            return i;
        }
        //public int update_itemdetails(int DocNo, string Docdate, string Item_Code, string Packing, string Unit, decimal GST, decimal IGST, decimal Qty, decimal FreeQty, decimal Rate, decimal TotalAmount, string UNIT2, decimal UnitMF, decimal CostBase,MySqlConnection con,MySqlTransaction trans)
        //{
        //    int i = _model.update_itemdetails(DocNo, Docdate, Item_Code,Packing, Unit, GST, IGST, Qty, FreeQty, Rate, TotalAmount, UNIT2, UnitMF, CostBase,con,trans);
        //    return i;
        //}
        public int update_itemdetails(int DocNo, string Docdate, string Item_Code, string Packing, string batch, string b_entry, string Unit, decimal GST, decimal IGST, decimal Discount, decimal Qty, decimal FreeQty, decimal Rate, decimal TotalAmount, string UNIT2, decimal UnitMF, decimal CostBase, MySqlConnection con, MySqlTransaction trans)
        {
            int i = _model.update_itemdetails(DocNo, Docdate, Item_Code, Packing, batch, b_entry, Unit, GST, IGST, Discount, Qty, FreeQty, Rate, TotalAmount, UNIT2, UnitMF, CostBase, con, trans);
            return i;
        }
        public void update_batchnumber(decimal currentStock, string BatchEntry, MySqlConnection con, MySqlTransaction trans)
        {
            _model.update_batchnumber(currentStock, BatchEntry, con, trans);
        }
        public void update_batchnumber_(decimal currentStock, string BatchEntry)
        {
            _model.update_batchnumber_(currentStock, BatchEntry);
        }
        public void update_default_qty(string BatchEntry)
        {
            _model.update_default_qty(BatchEntry);
        }
        public void save_batchsale(int InvNumber, string InvDate, string Item_Code, string BatchNumber, decimal Qty, decimal rate, string BatchEntry, string unit2, string unitmf, MySqlConnection con, MySqlTransaction trans)
        {
            _model.save_batchsale(InvNumber, InvDate, Item_Code, BatchNumber, Qty, rate, BatchEntry, unit2, unitmf, con, trans);
        }
        public void update_batchsale(int InvNumber, string InvDate, string Item_Code, string BatchNumber, decimal Qty,decimal rate, string BatchEntry, string unit2, string unitmf, MySqlConnection con, MySqlTransaction trans)
        {
            _model.update_batchsale(InvNumber, InvDate,Item_Code, BatchNumber, Qty,rate, BatchEntry, unit2, unitmf, con, trans);
        }
        public void update_salesorder(string invnum_order, MySqlConnection con, MySqlTransaction trans)
        {
            _model.update_salesorder(invnum_order, con, trans);
        }
        public DataTable Get_companydetails()
        {
            DataTable dtb = _model.Get_companydetails();
            return dtb;
        }
        public DataTable prescription_main(string PrescritionMain_id)
        {
            DataTable dtb = _model.prescription_main(PrescritionMain_id);
            return dtb;
        }
        public DataTable prescription_dteails(string PrescritionMain_id)
        {
            DataTable dtb = _model.prescription_dteails(PrescritionMain_id);
            return dtb;
        }
        public DataTable drug_instock(string id)
        {
            DataTable dtb = pmodel.drug_instock(id);
            return dtb;
        }
        public DataTable get_inventoryid(string drug_id)
        {
            DataTable dtb = _model.get_inventoryid(drug_id);
            return dtb;
        }
        public DataTable get_inventoryname(string drug_id)
        {
            DataTable dtb = _model.get_inventoryname(drug_id);
            return dtb;

        }
        public DataTable Get_itemdetails(string inventory_id)
        {
            DataTable dtb = _model.Get_itemdetails(inventory_id);
            return dtb;
        }
        public DataTable get_batchdetails(string item_Code)
        {
            DataTable dtb = _model.get_batchdetails(item_Code);
            return dtb;
        }
        public DataTable batchrate(string itemid, string batch, string Unit)
        {
            DataTable dtb = _model.batchrate(itemid, batch, Unit);
            return dtb;
        }
        public DataTable get_item_salesrate(string itemid)
        {
            DataTable dtb = _model.get_item_salesrate(itemid);
            return dtb;
        }
        public DataTable get_item_salesrate_minimun(string itemid)
        {
            DataTable dtb = _model.get_item_salesrate_minimun(itemid);
            return dtb;
        }
        public void Save_advancetable(string Pt_id, string Date, string Amount, string PaymentMethod, string Credit_Debit, string form, MySqlConnection con, MySqlTransaction trans)
        {
            _model.Save_advancetable(Pt_id, Date, Amount, PaymentMethod, Credit_Debit, form, con, trans);
        }
        public void update_advance(decimal adv, string patient_id, MySqlConnection con, MySqlTransaction trans)
        {
            _model.update_advance(adv, patient_id, con, trans);
        }
        public DataTable _trans_docnumber(MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dtb = _model._trans_docnumber(con, trans);
            return dtb;
        }
        public DataTable getShelfno(string id)
        {
            DataTable dt = _model.getShelfno(id);
            return dt;
        }
        public DataTable Get_consume_tick()
        {
            DataTable dt = inv_model.Get_consume_tick();
            return dt;
        }
    }
}
