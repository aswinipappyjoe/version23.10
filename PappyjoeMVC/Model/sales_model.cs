using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
namespace PappyjoeMVC.Model
{
  public class sales_model
    {
        Connection db = new Connection();
        public DataTable get_itemdetails(string name)//kggkgg
        {
            DataTable dtb = db.table(" select i.item_code,i.item_name,i.Sales_Rate_Max,i.Packing,i.Unit1,i.Unit2,i.OneUnitOnly,UnitMF,Barcode,Sales_Rate_Max,GstVat from tbl_ITEMS i where i.id='" + name + "'");// inner join tbl_PURCHIT p on p.Item_Code = i.id  where i.id='" +name + "'");
            return dtb;
        }
        public DataTable Get_itemdetails_itemname(string itemname)
        {
            DataTable dtitems = db.table("select id,Item_Code,item_name,Packing,Purch_Rate,Unit1,Unit2,UnitMF,Barcode,Sales_Rate_Max,GstVat from tbl_ITEMS where item_name='" + itemname + "'");
            return dtitems;
        }
        public DataTable Search_itemdetails_itemname(string itemname)
        {
            DataTable dtitems = db.table("select id,Item_Code,item_name,Packing,Purch_Rate,Unit1,Unit2,UnitMF,Barcode,Sales_Rate_Max from tbl_ITEMS where item_name like'" + itemname + "%'");
            return dtitems;
        }
        public DataTable get_itemdetails_Barcode(string name)
        {
            DataTable dt = new DataTable();
            dt = db.table(" select i.id,i.item_code,i.Sales_Rate_Max,i.Packing,i.Unit1,i.Unit2,i.OneUnitOnly from tbl_ITEMS i inner join tbl_PURCHIT p on p.Item_Code = i.id where p.Barcode='" + name + "'");// inner join tbl_PURCHIT p on p.Item_Code = i.id  where i.id='" +name + "'");
            if (dt.Rows.Count == 0)
            {
                dt = db.table(" select i.id,i.item_code,i.Sales_Rate_Max,i.Packing,i.Unit1,i.Unit2,i.OneUnitOnly from tbl_ITEMS i where i.Barcode='" + name + "'");
            }
            return dt;
        }
        public DataTable get_pur_gst(string name)
        {
            DataTable dtb = db.table("SELECT max(purchnumber),item_code,Rate,GST,IGST from tbl_purchit where item_code='" + name + "'");//,p.GST,p.IGST
            return dtb;
        }
        public DataTable GetDoctorName(string name)
        {
            DataTable dtdr = db.table("select id,doctor_name from tbl_doctor where (login_type='doctor' or login_type='admin') and activate_login='yes'and  doctor_name like '" + name + "%'");
            return dtdr;
        }
        public DataTable get_doctorname_by_id(string value)
        {
           DataTable supplier = db.table("select id,doctor_name from tbl_doctor where (login_type='doctor' or login_type='admin') and activate_login='yes'and  id='" + value + "'");
            return supplier;
        }
        public DataTable patients(string value)
        {
            DataTable dtdr = db.table("select id,pt_id,pt_name,street_address,locality,city,primary_mobile_number from tbl_patient where  pt_id='" + value + "' ");
            return dtdr;
        }
        public DataTable Get_Advance(string patient_id)
        {
            DataTable dtadvance = db.table("select distinct(advance) from tbl_payment where pt_id='" + patient_id + "'");
            return dtadvance;
        }
        public DataTable patient_keydown(string name)
        {
            DataTable supplier = db.table("select pt_name,pt_id,primary_mobile_number from tbl_patient where  pt_name like '%" + name + "%' or pt_id like '%" + name + "%' or primary_mobile_number like '%"+ name + "%'");
            return supplier;
        }
        public DataTable itemdetails(string itemid)//yuytutytuy
        {
            DataTable dtb = db.table("select Sales_Rate_Max,Packing,Unit1,Unit2,OneUnitOnly,UnitMF from tbl_ITEMS where id='" + itemid + "' ");
            return dtb;
        }
        public DataTable Get_stock(string itemid)
        {
            DataTable dtb_qty = db.table("select sum(qty) Stock from tbl_BatchNumber where item_code='" + itemid + "'");
            return dtb_qty;
        }
        public DataTable get_sales_default_qty(string itemid)
        {
            DataTable dtb = db.table("select b.Entry_No,b.BatchNumber,b.qty,b.sales_default_qty  from tbl_BatchNumber b  where  b.Item_Code='" + itemid + "'and b.sales_default_qty>0");
            return dtb;
        }
        public void save_default_qty(string qty,string batchentry)
        {
            db.execute("update tbl_BatchNumber set sales_default_qty ='" + qty + "' where Entry_No='" + batchentry + "' ");

        }
        public DataTable check_sales_defaut_qty( string batchentry)
        {
            DataTable dtb = db.table("select sales_default_qty from tbl_BatchNumber where  Entry_No='" + batchentry + "'");
            return dtb;
        }
        public DataTable get_salesrate_unit(string itemid)
        {
            DataTable dt_cost = db.table("select Packing,Sales_Rate,Unit1,Unit2 from tbl_ITEMS where id='" + itemid + "'");
            return dt_cost;
        }
        public DataTable dt_itemdetails(string itemid)
        {
            DataTable dt_cost = db.table("select Packing,Barcode,GstVat,Shelf_No,Unit1,Unit2 from tbl_ITEMS where id='" + itemid + "'");
            return dt_cost;
        }
        public DataTable  sales_details(int invnum_Edit)
        {
            DataTable dtb_master = db.table("select InvNumber,InvDate,SalesmanCode,OrderNumber,Orderdate,Prescribedby,LRNo,LRDate,Through,cust_name,cust_number,adr1,adr2,adr3,phone1,Discount,GST,IGST,TotalAmount,PayMethod from tbl_SALES where InvNumber='" + invnum_Edit + "'");
            return dtb_master;
        }
        public DataTable sales_items_details(int invnum_Edit)
        {
            DataTable dtb_sales = db.table("select InvNumber,Item_Code,Description,Packing,batch_entry,batch_number,Unit,GST,IGST,Qty,FreeQty,Rate,TotalAmount,Discount from tbl_SALEIT where InvNumber='" + invnum_Edit + "'");
            return dtb_sales;
        }
        public DataTable get_hsn(string id)
        {
            DataTable dtb = db.table("select id,item_name,Item_Code,HSN_Number from tbl_ITEMS where id='" + id + "'");
            return dtb;
        }
        public DataTable get_batchsale(int invnum_Edit)
        {
            //DataTable dtb_BatchSale = db.table("select InvNumber,InvDate,s.Item_Code,s.BatchNumber,s.Qty,(select sum(qty) Stock from tbl_BatchNumber where item_code=b.Item_Code) stock,s.IsExpDate,BatchEntry,PrdDate,ExpDate,b.Unit2	 from tbl_BatchSale s inner join tbl_batchnumber b on b.BatchNumber=s.BatchNumber where InvNumber='" + invnum_Edit + "'");
            DataTable dtb_BatchSale = db.table("select InvNumber,InvDate,Item_Code,Qty,Unit,UNIT2,batch_entry,batch_number from tbl_saleit where InvNumber='" + invnum_Edit + "'");
            return dtb_BatchSale;
        }
        public DataTable batcdetails(int invnum_Edit, string unit2)
        {
            DataTable dtb_BatchSale = db.table("select InvNumber,Item_Code,BatchNumber,IsExpDate,BatchEntry,Unit2 from tbl_batchsale where InvNumber='" + invnum_Edit + "' and UNIT2='" + unit2 + "' ");
            return dtb_BatchSale;

        }
        public DataTable batchnumbetails(string invnum_Edit)
        {
            DataTable dtb_BatchSale = db.table("select Entry_No,Item_Code,BatchNumber,Qty,batch_sales_rate,IsExpDate,PrdDate,ExpDate,Unit2 from tbl_BatchNumber where Entry_No='" + invnum_Edit + "'");
            return dtb_BatchSale;

        }
        public DataTable get_batch_wise_stock(string entryno)
        {
            DataTable dtb = db.table("select Qty from tbl_BatchNumber where Entry_No='" + entryno + "'");
            return dtb;
        }
        public DataTable load_prescription()
        {
            System.Data.DataTable dt_pre_main = db.table("SELECT tbl_prescription_main.id,tbl_prescription_main.date,tbl_patient.pt_name,tbl_patient.pt_id FROM tbl_prescription_main join tbl_patient on tbl_prescription_main.pt_id=tbl_patient.id where pay_status='Yes' AND tbl_prescription_main.date = '" + Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd") + "' ORDER BY tbl_prescription_main.date DESC");
            return dt_pre_main;
        }
        public void change_pres_status(string id)
        {
            db.execute(" update tbl_prescription_main set pay_status='NO' where id='" + id + "' ");
        }
        public DataTable docnumber()
        {
            DataTable Document_Number = db.table("SELECT max(cast(InvNumber as UNSIGNED)) As 'InvNumber' FROM tbl_SALES");
            return Document_Number;
        }
        //delete sales tables
       
        public int delete_sales_main(string inv)
        {
            int i = db.execute("delete from tbl_SALES where InvNumber='" + inv + "'");
            return i;
        }
        public int delete_sales_it(string inv)
        {
            int i = db.execute("delete from tbl_saleit where InvNumber='" + inv + "'");
            return i;
        }
        public int delete_batchsales(string inv)
        {
            int i = db.execute("delete from tbl_batchsales where InvNumber='" + inv + "'");
            return i;
        }
        public DataTable get_sales_items_details(string inv)
        {
            DataTable dtb = db.table("select * from tbl_saleit where InvNumber ='" + inv + "'");
            return dtb;
        }
        public DataTable get_batchsales_items_details(string inv)
        {
            DataTable dtb = db.table("select * from tbl_batchsales where InvNumber ='" + inv + "'");
            return dtb;
        }
        public DataTable salesOrder_master(int invnum_order)
        {
            DataTable dtb_orderMaster = db.table("select * from tbl_SalesOrder_Master where DocNumber='" + invnum_order + "'");
            return dtb_orderMaster;
        }
        public DataTable order_itemsDtails(int invnum_order)
        {
            DataTable dtb_order = db.table("select s.DocNumber,s.DocDate,s.ItemCode as id ,s.Discription,s.Qty,s.Cost,s.TotalAmount,s.Unit,i.item_code,i.HSN_Number,i.Shelf_No from tbl_SalesOrder s inner join tbl_ITEMS i on s.ItemCode=i.id where s.DocNumber='" + invnum_order + "'");
            return dtb_order;
        }
        public int Save_salesMaster(int DocNo,string Docdate, string sales_, string ordeNo, string Date_, string doctor_, string lrno, string lr_date, string throuhg, string ptid, string ptname, string street__, string locality, string city, string phone, string payMethod, decimal totalAmnt, decimal disount, decimal gst, decimal igst, decimal gTotal, string type,MySqlConnection con,MySqlTransaction trans)
        {
         int i= db.trans_execute("insert into tbl_SALES(InvNumber,InvDate,Dep_Number,SalesmanCode,OrderNumber,Orderdate,Prescribedby,LRNo,LRDate,Through,cust_number,cust_name,adr1,adr2,adr3,phone1,PayMethod," +
                               "Paid,Discount,UserName,JournalRefNo,SaleType,GST,IGST,TotalAmount,Product_type)" +
                               "values('" + DocNo + "','" + Docdate + "','1','" + sales_ + "','" + ordeNo + "','" + Date_ + "'," +
                               "'" + doctor_ + "','" + lrno + "','" + lr_date + "','" + throuhg + "','" + ptid + "','" + ptname + "','" + street__ + "'," +
                               "'" + locality + "','" + city + "','" + phone + "','" + payMethod + "','" + totalAmnt + "','" + disount + "','1','1','1','" + Convert.ToDecimal(gst) + "','" + igst + "','" + gTotal + "','" + type + "')",con,trans);
            return i;
        }
        //public int save_log(string log_userid, string log_type, string log_descriptn, string logdate, string logtime, string log_stage)
        //{
        //    int j = db.execute("insert into tbl_log(log_user_id,log_type,log_description,date,time,log_stage)values('" + log_userid + "','" + log_type + "','" + log_descriptn + "','" + logdate + "','" + logtime + "','" + log_stage + "')");
        //    return j;
        //}
        public int Save_salesMaster_cheque(int DocNo, string Docdate, string sales_, string ordeNo, string Date_, string doctor_, string lrno, string lr_date, string throuhg, string ptid, string ptname, string street__, string locality, string city, string phone, string payMethod,string Bank ,string Number,decimal totalAmnt, decimal disount, decimal gst, decimal igst, decimal gTotal,string type)
        {
            int i = db.execute("insert into tbl_SALES(InvNumber,InvDate,Dep_Number,SalesmanCode,OrderNumber,Orderdate,Prescribedby,LRNo,LRDate,Through,cust_number,cust_name,adr1,adr2,adr3,phone1,PayMethod,Number,Bank," +
                                  "Paid,Discount,UserName,JournalRefNo,SaleType,GST,IGST,TotalAmount,Product_type)" +
                                  "values('" + DocNo + "','" + Docdate + "','1','" + sales_ + "','" + ordeNo + "','" + Date_ + "'," +
                                  "'" + doctor_ + "','" + lrno + "','" + lr_date + "','" + throuhg + "','" + ptid + "','" + ptname + "','" + street__ + "'," +
                                  "'" + locality + "','" + city + "','" + phone + "','" + payMethod + "','" + Number + "','" + Bank + "','" + totalAmnt + "','" + disount + "','1','1','1','" + Convert.ToDecimal(gst) + "','" + igst + "','" + gTotal + "','" + type + "')");
            return i;
        }
        public int Save_salesMaster_card(int DocNo, string Docdate, string sales_, string ordeNo, string Date_, string doctor_, string lrno, string lr_date, string throuhg, string ptid, string ptname, string street__, string locality, string city, string phone, string payMethod, string cardnumber, string fourdigitnumber, decimal totalAmnt, decimal disount, decimal gst, decimal igst, decimal gTotal, string type)
        {
            int i = db.execute("insert into tbl_SALES(InvNumber,InvDate,Dep_Number,SalesmanCode,OrderNumber,Orderdate,Prescribedby,LRNo,LRDate,Through,cust_number,cust_name,adr1,adr2,adr3,phone1,PayMethod,cardnumber,fourdigitnumber," +
                                  "Paid,Discount,UserName,JournalRefNo,SaleType,GST,IGST,TotalAmount,Product_type)" +
                                  "values('" + DocNo + "','" + Docdate + "','1','" + sales_ + "','" + ordeNo + "','" + Date_ + "'," +
                                  "'" + doctor_ + "','" + lrno + "','" + lr_date + "','" + throuhg + "','" + ptid + "','" + ptname + "','" + street__ + "'," +
                                  "'" + locality + "','" + city + "','" + phone + "','" + payMethod + "','" + cardnumber + "','" + fourdigitnumber + "','" + totalAmnt + "','" + disount + "','1','1','1','" + Convert.ToDecimal(gst) + "','" + igst + "','" + gTotal + "','" + type + "')");
            return i;
        }
        public int Save_salesMaster_DD(int DocNo, string Docdate, string sales_, string ordeNo, string Date_, string doctor_, string lrno, string lr_date, string throuhg, string ptid, string ptname, string street__, string locality, string city, string phone, string payMethod, string Bank, string DDnumber, decimal totalAmnt, decimal disount, decimal gst, decimal igst, decimal gTotal, string type)
        {
            int i = db.execute("insert into tbl_SALES(InvNumber,InvDate,Dep_Number,SalesmanCode,OrderNumber,Orderdate,Prescribedby,LRNo,LRDate,Through,cust_number,cust_name,adr1,adr2,adr3,phone1,PayMethod,Bank,DDnumber," +
                                  "Paid,Discount,UserName,JournalRefNo,SaleType,GST,IGST,TotalAmount,Product_type)" +
                                  "values('" + DocNo + "','" + Docdate + "','1','" + sales_ + "','" + ordeNo + "','" + Date_ + "'," +
                                  "'" + doctor_ + "','" + lrno + "','" + lr_date + "','" + throuhg + "','" + ptid + "','" + ptname + "','" + street__ + "'," +
                                  "'" + locality + "','" + city + "','" + phone + "','" + payMethod + "','" + Bank + "','" + DDnumber + "','" + totalAmnt + "','" + disount + "','1','1','1','" + Convert.ToDecimal(gst) + "','" + igst + "','" + gTotal + "','" + type + "')");
            return i;
        }
        public int update_salesMaster(int DocNo, string Docdate, string sales_, string ordeNo, string Date_, string doctor_, string lrno, string lr_date, string throuhg, string ptid, string ptname, string street__, string locality, string city, string phone, string payMethod, decimal totalAmnt, decimal disount, decimal gst, decimal igst, decimal gTotal, string type,MySqlConnection con,MySqlTransaction trans)
        {
            int i = db.trans_execute("update  tbl_SALES set InvDate='" + Docdate + "',SalesmanCode='" + sales_ + "',OrderNumber='" + ordeNo + "',Orderdate='" + Date_ + "',Prescribedby='" + doctor_ + "',LRNo='" + lrno + "',LRDate='" + lr_date + "',Through='" + throuhg + "',cust_number='" + ptid + "',cust_name='" + ptname + "',adr1='" + locality + "',adr2='" + city + "',adr3='" + street__ + "',phone1='" + phone + "',PayMethod='" + payMethod + "'," +
                                  "Paid='" + totalAmnt + "',Discount='" + disount + "',GST='" + Convert.ToDecimal(gst) + "',IGST='" + igst + "',TotalAmount='" + gTotal + "'" +
                                  ",Product_type='" + type + "' where InvNumber='" + DocNo + "' ",con,trans);
            return i;
        }
        public int update_salesMaster_cheque(int DocNo, string Docdate, string sales_, string ordeNo, string Date_, string doctor_, string lrno, string lr_date, string throuhg, string ptid, string ptname, string street__, string locality, string city, string phone, string payMethod, string Bank, string Number, decimal totalAmnt, decimal disount, decimal gst, decimal igst, decimal gTotal, string type)
        {
            int i = db.execute("update into tbl_SALES  set InvDate='" + Docdate + "',SalesmanCode='" + sales_ + "',OrderNumber='" + ordeNo + "',Orderdate='" + Date_ + "',Prescribedby='" + doctor_ + "',LRNo='" + lrno + "',LRDate='" + lr_date + "',Through='" + throuhg + "',cust_number='" + ptid + "',cust_name='" + ptname + "',adr1='" + locality + "',adr2='" + city + "',adr3='" + street__ + "',phone1='" + phone + "',PayMethod='" + payMethod + "',Number='" + Number + "',Bank='" + Bank + "'," +
                                  "Paid='" + totalAmnt + "',Discount='" + disount + "',GST='" + Convert.ToDecimal(gst) + "',IGST='" + igst + "',TotalAmount='" + gTotal + "'" +
                                  ",Product_type='" + type + "'  where InvNumber='" + DocNo + "' "); 
            return i;
        }
        public int update_salesMaster_card(int DocNo, string Docdate, string sales_, string ordeNo, string Date_, string doctor_, string lrno, string lr_date, string throuhg, string ptid, string ptname, string street__, string locality, string city, string phone, string payMethod, string cardnumber, string fourdigitnumber, decimal totalAmnt, decimal disount, decimal gst, decimal igst, decimal gTotal, string type)
        {
            int i = db.execute("update tbl_SALES set InvDate='" + Docdate + "',SalesmanCode='" + sales_ + "',OrderNumber='" + ordeNo + "',Orderdate='" + Date_ + "',Prescribedby='" + doctor_ + "',LRNo='" + lrno + "',LRDate='" + lr_date + "',Through='" + throuhg + "',cust_number='" + ptid + "',cust_name='" + ptname + "',adr1='" + locality + "',adr2='" + city + "',adr3='" + street__ + "',phone1='" + phone + "',PayMethod='" + payMethod + "',cardnumber='" + cardnumber + "',fourdigitnumber='" + fourdigitnumber + "'," +
                                  "Paid='" + totalAmnt + "',Discount='" + disount + "',GST='" + Convert.ToDecimal(gst) + "',IGST='" + igst + "',TotalAmount='" + gTotal + "'" +
                                  ",Product_type='" + type + "'  where InvNumber='" + DocNo + "' ");
            return i;
        }
        public int update_salesMaster_DD(int DocNo, string Docdate, string sales_, string ordeNo, string Date_, string doctor_, string lrno, string lr_date, string throuhg, string ptid, string ptname, string street__, string locality, string city, string phone, string payMethod, string Bank, string DDnumber, decimal totalAmnt, decimal disount, decimal gst, decimal igst, decimal gTotal, string type)
        {
            int i = db.execute("update tbl_SALES set InvDate='" + Docdate + "',SalesmanCode='" + sales_ + "',OrderNumber='" + ordeNo + "',Orderdate='" + Date_ + "',Prescribedby='" + doctor_ + "',LRNo='" + lrno + "',LRDate='" + lr_date + "',Through='" + throuhg + "',cust_number='" + ptid + "',cust_name='" + ptname + "',adr1='" + locality + "',adr2='" + city + "',adr3='" + street__ + "',phone1='" + phone + "',PayMethod='" + payMethod + "',Bank='" + Bank + "',DDnumber='" + DDnumber + "'," +
                                  "Paid='" + totalAmnt + "',Discount='" + disount + "',GST='" + Convert.ToDecimal(gst) + "',IGST='" + igst + "',TotalAmount='" + gTotal + "'" +
                                  ",Product_type='" + type + "'  where InvNumber='" + DocNo + "' ");
            return i;
        }
        public DataTable get_costbase(string itemcode,MySqlConnection con,MySqlTransaction trans)
        {
            DataTable dt_Unit2 = db.trans_table("select Unit2,UnitMF,CostBase from tbl_ITEMS where id='" + itemcode + "' ",con,trans);
            return dt_Unit2;
        }
        //public int  Save_itemdetails(int DocNo,  string Docdate, string Item_Code, string Description, string Packing, string Unit, decimal GST, decimal IGST, decimal  Qty, decimal FreeQty,decimal Rate, decimal TotalAmount, string UNIT2, decimal UnitMF, decimal CostBase,MySqlConnection con,MySqlTransaction trans)
        //{
        //    int j = db.trans_execute("insert into tbl_SALEIT (InvNumber,InvDate,Item_Code,Description,Packing,Unit,GST,IGST,Qty,FreeQty,Rate,TotalAmount,UNIT2,UnitMF,CostBase,Taxable,RetQty) values('" + DocNo + "','" + Docdate + "','" + Item_Code + "','" + Description + "'," +
        //                     "'" + Packing + "','" + Unit + "','" + GST + "','" + IGST + "','" + Qty + "'," + "'" + FreeQty + "','" + Rate + "','" + TotalAmount + "','" + UNIT2 + "','" + UnitMF + "','" + CostBase + "','Yes','0')",con,trans);
        //    return j;
        //}
        public int Save_itemdetails(int DocNo, string Docdate, string Item_Code, string Description, string Packing,string batch,string b_entry, string Unit, decimal GST, decimal IGST, decimal Discount, decimal Qty, decimal FreeQty, decimal Rate, decimal TotalAmount, string UNIT2, decimal UnitMF, decimal CostBase, MySqlConnection con, MySqlTransaction trans)
        {
            int j = db.trans_execute("insert into tbl_SALEIT (InvNumber,InvDate,Item_Code,Description,Packing,batch_entry,batch_number,Unit,GST,IGST,Discount,Qty,FreeQty,Rate,TotalAmount,UNIT2,UnitMF,CostBase,Taxable,RetQty) values('" + DocNo + "','" + Docdate + "','" + Item_Code + "','" + Description + "'," +
                             "'" + Packing + "','" + b_entry + "','" + batch + "','" + Unit + "','" + GST + "','" + IGST + "','" + Discount + "','" + Qty + "'," + "'" + FreeQty + "','" + Rate + "','" + TotalAmount + "','" + UNIT2 + "','" + UnitMF + "','" + CostBase + "','Yes','0')", con, trans);
            return j;
        }
        //public int update_itemdetails(int DocNo, string Docdate, string Item_Code, string Packing, string Unit, decimal GST, decimal IGST, decimal Qty, decimal FreeQty, decimal Rate, decimal TotalAmount, string UNIT2, decimal UnitMF, decimal CostBase, MySqlConnection con, MySqlTransaction trans)
        //{
        //    int j = db.trans_execute("update  tbl_SALEIT set InvDate='" + Docdate + "',Packing='" + Packing + "',Unit='" + Unit + "',GST='" + GST + "',IGST='" + IGST + "',Qty='" + Qty + "',FreeQty='" + FreeQty + "',Rate='" + Rate + "',TotalAmount='" + TotalAmount + "',UNIT2='" + UNIT2 + "',UnitMF='" + UnitMF + "',CostBase='" + CostBase + "' where  InvNumber='" + DocNo + "' and Item_Code='" + Item_Code + "'",con,trans);
        //    return j;
        //}
        public int update_itemdetails(int DocNo, string Docdate, string Item_Code, string Packing, string batch, string b_entry, string Unit, decimal GST, decimal IGST, decimal Discount, decimal Qty, decimal FreeQty, decimal Rate, decimal TotalAmount, string UNIT2, decimal UnitMF, decimal CostBase, MySqlConnection con, MySqlTransaction trans)
        {
            int j = db.trans_execute("update  tbl_SALEIT set InvDate='" + Docdate + "',Packing='" + Packing + "', batch_entry ='" + b_entry + "',batch_number='" + batch + "',Unit='" + Unit + "',GST='" + GST + "',IGST='" + IGST + "',Discount='" + Discount + "',Qty='" + Qty + "',FreeQty='" + FreeQty + "',Rate='" + Rate + "',TotalAmount='" + TotalAmount + "',UNIT2='" + UNIT2 + "',UnitMF='" + UnitMF + "',CostBase='" + CostBase + "' where  InvNumber='" + DocNo + "' and Item_Code='" + Item_Code + "'", con, trans);
            return j;
        }
        public void update_batchnumber(decimal currentStock,string BatchEntry,MySqlConnection con,MySqlTransaction trans)
        {
            db.trans_execute("update tbl_BatchNumber set Qty='" +currentStock + "' ,sales_default_qty='0' where  Entry_No='" + BatchEntry + "'",con,trans);
        }
        public void update_batchnumber_(decimal currentStock, string BatchEntry)
        {
            db.execute("update tbl_BatchNumber set Qty='" + currentStock + "' ,sales_default_qty='0' where  Entry_No='" + BatchEntry + "'");
        }
        public void update_default_qty(string BatchEntry)
        {
            db.execute("update tbl_BatchNumber set sales_default_qty='0' where  Entry_No='" + BatchEntry + "'");

        }
        public void save_batchsale(int InvNumber, string InvDate, string Item_Code, string BatchNumber, decimal Qty,decimal rate, string BatchEntry,string unit2,string unitmf, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("insert into tbl_BatchSale (InvNumber,InvDate,Item_Code,BatchNumber,Qty,batch_rate, IsExpDate,BatchEntry,Unit2,UnitMF,WsInv,RetQty) values('" + InvNumber + "','" + InvDate + "','" + Item_Code + "','" + BatchNumber + "','" + Qty + "','" + rate + "','True','" + BatchEntry + "','" + unit2 + "','" + unitmf + "','1',0)", con, trans);
        }
        public void update_batchsale(int InvNumber, string InvDate, string Item_Code, string BatchNumber, decimal Qty,decimal rate, string BatchEntry, string unit2, string unitmf, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("update tbl_BatchSale set InvDate='" + InvDate + "',BatchNumber='" + BatchNumber + "',Qty='" + Qty + "',batch_rate='" + rate + "', BatchEntry='" + BatchEntry + "',Unit2='" + unit2 + "',UnitMF='" + unitmf + "' where InvNumber='" + InvNumber + "' and Item_Code='" + Item_Code + "'", con, trans);
        }
        public void update_salesorder(string invnum_order,MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("update tbl_SalesOrder_Master set Status='S' where DocNumber='" + invnum_order + "'",con,trans);
        }
        public DataTable Get_companydetails()
        {
          DataTable dtp = db.table("select name,contact_no,street_address,path,email,website,Dl_Number,Dl_Number2  from tbl_practice_details");
            return dtp;
        }
        public DataTable prescription_main(string PrescritionMain_id)
        {
            System.Data.DataTable dt_prescriptionDetails = db.table("SELECT  tbl_prescription_main.id,tbl_prescription_main.date,tbl_doctor.doctor_name,tbl_patient.pt_name,tbl_patient.id,tbl_patient.pt_id   FROM tbl_prescription_main join tbl_doctor on tbl_prescription_main.dr_id=tbl_doctor.id join tbl_patient on tbl_prescription_main.pt_id=tbl_patient.id  where tbl_prescription_main.id='" + PrescritionMain_id + "' ");
            return dt_prescriptionDetails;
        }
        public DataTable prescription_dteails(string PrescritionMain_id)
        {
            System.Data.DataTable dt_drugDetails = db.table("SELECT drug_name,strength,duration_unit,duration_period,morning,noon,night,food,add_instruction,drug_type,strength_gr,drug_id FROM tbl_prescription WHERE pres_id='" + PrescritionMain_id + "' ORDER BY id");
            return dt_drugDetails;
        }
        public DataTable get_inventoryid(string drug_id)
        {
            System.Data.DataTable dt_drug_inv_Details = db.table("SELECT id,inventory_id,name FROM tbl_adddrug WHERE id='" +drug_id + "'  and inventory_id<>'0' ORDER BY id");
            return dt_drug_inv_Details;
        }
        public DataTable get_inventoryname(string drug_id)
        {
            System.Data.DataTable dt_drug_inv_Details = db.table("SELECT id,inventory_id,name FROM tbl_adddrug WHERE id='" + drug_id + "'  ");//and inventory_id<>'0' ORDER BY id
            return dt_drug_inv_Details;
        }
        public DataTable Get_itemdetails(string inventory_id)
        {
            DataTable dtb = db.table(" select i.id,i.Sales_Rate_Max,i.Packing,i.Unit1,i.Unit2,i.OneUnitOnly,i.HSN_Number,i.item_name,i.item_code,i.OneUnitOnly,i.Unit1,i.Unit2,i.Shelf_No,i.GstVat from tbl_ITEMS i  where i.id='" + inventory_id + "'");
            return dtb;
        }
        public DataTable get_batchdetails(string item_Code)
        {
            DataTable dtb = db.table("select Entry_No,BatchNumber,Qty,cast(PrdDate as date) PrdDate,cast(ExpDate as date) ExpDate, Unit2,UnitMF,batch_sales_rate from tbl_BatchNumber where Item_Code='" + item_Code + "'and Qty>0 and `ExpDate`> CURDATE() order by ExpDate");
            return dtb;
        }
        //sales list
        public DataTable get_salesDetails(string dateTo, string dateFrom,string type, string pat)
        {
            DataTable dt = db.table("select InvNumber,cast(InvDate as date) 'InvDate' ,cust_name,cust_number,phone1,Paid,TotalAmount,PayMethod from tbl_SALES where InvDate between '" + Convert.ToDateTime(dateTo).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(dateFrom).ToString("yyyy-MM-dd") + "' and cust_name='"+pat+"' and Product_type='" + type + "'");
            return dt;
        }
        public DataTable get_salesDetails(string dateTo, string dateFrom, string type)
        {
            DataTable dt = db.table("select InvNumber,cast(InvDate as date) 'InvDate' ,cust_name,cust_number,phone1,Paid,TotalAmount,PayMethod from tbl_SALES where InvDate between '" + Convert.ToDateTime(dateTo).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(dateFrom).ToString("yyyy-MM-dd") + "' and Product_type='" + type + "'");
            return dt;
        }
        public DataTable supname()
        {
            DataTable dt = db.table("select id,pt_name from tbl_patient");
            return dt;
        }
        public DataTable invDetailsbyDate(string date, string type)
        {
            DataTable dtb = db.table("select InvNumber,cast(InvDate as date) 'InvDate' ,cust_name,cust_number,phone1,Paid,TotalAmount,PayMethod from tbl_SALES where InvDate='" + date + "'and Product_type='" + type + "'");
            return dtb;
        }
        public DataTable invDetailsbyDateBtwn(string from, string to,string type)
        {
            DataTable dt = db.table("select InvNumber,InvDate,cust_name,cust_number,phone1,Paid,TotalAmount,PayMethod from tbl_SALES where InvDate between '" + from + "' and '" + to + "' and PayMethod='Cash Sale' and Product_type='" + type + "'");
            return dt;
        }
        public DataTable InvDetailsBtwnDates(string from, string to, string type)
        {
            DataTable dt = db.table("select InvNumber,InvDate,cust_name,cust_number,phone1,Paid,TotalAmount,PayMethod from tbl_SALES where InvDate between '" + from + "' and '" + to + "' and PayMethod='Credit Sale' and Product_type='" + type + "'");
            return dt;
        }
        public DataTable data_from_sales(string dt)
        {
            DataTable data_from_sales = db.table("select Invdate,  FORMAT(SUM(Qty * Rate),2) total ,GST from tbl_saleit where InvNumber='" + dt + "' GROUP BY GST order by GST");
            return data_from_sales;
        }
        public DataTable data_from_sales_igst(string dt)
        {
            DataTable data_from_sales_igst = db.table("select Invdate,  FORMAT(SUM(Qty * Rate),2) total ,igst from tbl_saleit where InvNumber='" + dt + "'and igst<>0 GROUP BY IGST order by IGST");
            return data_from_sales_igst;
        }
        public DataTable get_doctor(string doctor_id)
        {
            System.Data.DataTable tb_doctor = db.table("select id,doctor_name from tbl_doctor where (login_type='doctor' or login_type='admin') and activate_login='yes'and  id='" + doctor_id + "'");
            return tb_doctor;
        }
        public DataTable batchrate(string itemid,string batch,string Unit)
        {
            DataTable dtb=db.table("SELECT i.item_code,i.rate,i.Unit,p.batchentry,b.entry_no,b.batchnumber FROM `tbl_purchit` i inner join tbl_batchpurchase p on p.item_code = i.item_code inner join tbl_batchpurchase b on p.batchentry = b.Entry_No WHERE i.item_code = '" + itemid+"'  and b.batchnumber = '"+batch+ "'");// and i.Unit='" + Unit + "'
            return dtb;
        }
        public DataTable get_item_salesrate(string itemid)
        {
            DataTable dtb = db.table("select Sales_Rate_Max,Purch_Rate from tbl_items where id='" + itemid + "'");
            return dtb;
        }
        public DataTable get_item_salesrate_minimun(string itemid)
        {
            DataTable dtb = db.table("select Sales_Rate_Max2,Purch_Rate2 from tbl_items where id='" + itemid + "'");
            return dtb;
        }
        public DataTable getShelfno(string id)
        {
            DataTable dt = db.table("select item_name,Shelf_No from tbl_items where id='" + id + "'");
            return dt;
        }
        public void Save_advancetable(string Pt_id, string Date, string Amount, string PaymentMethod, string Credit_Debit, string form, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("insert into tbl_advance(Pt_id,Date,Amount,PaymentMethod,Credit_Debit,form) values('" + Pt_id + "','" + Date + "','" + Amount + "','" + PaymentMethod + "','" + Credit_Debit + "','" + form + "')", con, trans);
        }
        public void update_advance(decimal adv, string patient_id, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("update tbl_payment set advance='" + adv + "' where pt_id='" + patient_id + "'", con, trans);
        }
        public DataTable _trans_docnumber(MySqlConnection con, MySqlTransaction trans)
        {
            DataTable Document_Number = db.trans_table("SELECT max(cast(InvNumber as UNSIGNED)) As 'InvNumber' FROM tbl_SALES", con, trans);
            return Document_Number;
        }
    } 
}
 