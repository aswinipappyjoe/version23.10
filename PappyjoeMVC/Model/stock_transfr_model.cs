using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PappyjoeMVC.Controller;
using PappyjoeMVC.Model;
using MySql.Data.MySqlClient;
using System.Data;

namespace PappyjoeMVC.Model
{
   public  class stock_transfr_model
    {
        Connection db = new Connection();
        //load item details
        public DataTable get_itemdata(string type)
        {
            DataTable dtb = db.table("select i.id,i.item_code ,i.item_name ,(select sum(qty) from tbl_batchnumber where Item_Code = i.id) stock from tbl_batchnumber B right join tbl_items i on b.item_code=i.id where i.product_type='"+type+"'  group by i.item_code order by i.item_code ");
            //DataTable dtb = db.table("select item_code as id ,(select item_code from tbl_items where id = B.Item_Code) as itemCode,(select  item_name from tbl_items where id=b.Item_Code) itemName,(select sum(qty) from tbl_batchnumber where Item_Code = B.Item_Code) stock from tbl_batchnumber B group by item_code order by B.item_code");
            return dtb;
        }
        public DataTable get_itemdata_Stock_updation(string type)
        {
            DataTable dtb = db.table("select i.id,i.item_code ,i.item_name,B.Entry_No ,B.BatchNumber, B.Qty,B.batch_rate from tbl_batchnumber B right join tbl_items i on b.item_code=i.id where i.Purch_Rate <> 0 and i.product_type='" + type + "' order by i.item_code ");
            //db.execute("drop index author_index on tbl_items");
            //db.execute("drop index author_index1 on tbl_batchnumber");
            return dtb;
        }
        public DataTable stock_adjustmnt_showmore(string type, int count)
        {
            DataTable dtb = db.table("select i.id,i.item_code ,i.item_name,B.Entry_No ,B.BatchNumber, B.Qty,B.batch_rate,B.batch_sales_rate from tbl_batchnumber B right join tbl_items i on b.item_code=i.id where i.Purch_Rate <> 0 and i.product_type='" + type + "' order by i.item_code DESC limit " + count + ",50");
            return dtb;
        }
       
        public DataTable get_itemdata_Stock_updation_limit(string type)/////dffdsfds
        {
          
            DataTable dtb = db.table("select i.id,i.item_code ,i.item_name,B.Entry_No ,B.BatchNumber, B.Qty,B.batch_rate,B.batch_sales_rate from tbl_batchnumber B right join tbl_items i on b.item_code=i.id where i.Purch_Rate <> 0 and i.product_type='" + type+"' order by i.item_code limit 25");
            //db.execute("drop index author_index on tbl_items");
            //db.execute("drop index author_index1 on tbl_batchnumber");
            return dtb;
        }
        public DataTable get_itemdata_Stock_updation_all_dataLoad(string type)/////dffdsfds
        {
           
            DataTable dtb = db.table("select i.id,i.item_code ,i.item_name,B.Entry_No ,B.BatchNumber, B.Qty from tbl_batchnumber B right join tbl_items i on b.item_code=i.id where i.Purch_Rate <> 0 and i.product_type='" + type + "' order by i.item_code ");
            //db.execute("drop index author_index on tbl_items");
            //db.execute("drop index author_index1 on tbl_batchnumber");
            return dtb;
        }
        public DataTable Stock_updation_search_wit_itemcode(string name,string type)
        {
           
            DataTable dtb = db.table("select i.id,i.item_code ,i.item_name,B.Entry_No ,B.BatchNumber, B.Qty,B.batch_rate from tbl_batchnumber B right join tbl_items i on b.item_code=i.id where (i.item_code Like '" + name + "%' or i.item_name like '" + name + "%' or i.Barcode Like '" + name + "%') and  i.product_type='" + type + "' order by i.item_code ");
            return dtb;
        }
        public DataTable get_itemdata_load()
        {
            DataTable dtb = db.table("select item_code as id ,(select item_code from tbl_items where id = B.Item_Code) as itemCode,(select  item_name from tbl_items where id=b.Item_Code) itemName,(select sum(qty) from tbl_batchnumber where Item_Code = B.Item_Code) stock from tbl_batchnumber B group by item_code order by B.item_code");//limit 50
            return dtb;
        }
        public DataTable get_itemdata_nonzero_stock(string type)
        {
         DataTable dtb = db.table("select item_code as id ,(select item_code from tbl_items where id = B.Item_Code) as itemCode,(select  item_name from tbl_items where id=b.Item_Code) itemName,(select sum(qty) from tbl_batchnumber where Item_Code = B.Item_Code) stock from tbl_batchnumber B where B.Qty <> -1 and B.Qty <> 0 and b.product_type='"+type+"' group by item_code order by B.item_code");
         return dtb;
        }
        public DataTable search_wit_itemcode(string name,string type)
        {
            DataTable dtb = db.table("select i.id,i.item_code ,i.item_name ,(select sum(qty) from tbl_batchnumber where Item_Code = i.id) stock from tbl_batchnumber B right join tbl_items i on b.item_code=i.id  where (i.item_code Like '" + name + "%' or i.item_name like '" + name + "%')and i.product_type='" + type + "' group by i.item_code order by i.item_code ");
            //DataTable dtb = db.table(" select B.item_code as id,i.item_code ,i.item_name ,(select sum(qty) from tbl_batchnumber where Item_Code = B.Item_Code) stock from tbl_batchnumber B inner join tbl_items i on B.item_code = i.id where (i.item_code Like '" + name + "%' or i.item_name like '" + name + "%')  group by B.item_code");
            return dtb;
        }
        public DataTable search_wit_itemcode_stock_out(string name,string type)
        {
            DataTable dtb = db.table(" select B.item_code as id,i.item_code as itemCode,i.item_name itemName,(select sum(qty) from tbl_batchnumber where Item_Code = B.Item_Code) stock from tbl_batchnumber B inner join tbl_items i on B.item_code = i.id where (i.item_code Like '" + name + "%' or i.item_name like '" + name + "%') and B.Qty <> -1 and B.Qty <> 0 and i.product_type='" + type + "' group by B.item_code");
            return dtb;
        }
        //stock out
        public DataTable dt_units(string id)
        {
            DataTable dtb = db.table("select Unit1,Unit2,Purch_Rate,GstVat from tbl_items where id='" + id+"'");
            return dtb;
        }
        public DataTable itemdetails(string itemid)
        {
            DataTable dtb = db.table("select Purch_Rate,Purch_Rate2,Unit1,Unit2,OneUnitOnly,UnitMF from tbl_ITEMS where id='" + itemid + "' ");
            return dtb;
        }
        public void save_stockout(string id,string refno,string ItemId, string ItemName, string Batch, string Unit, string Given_Qty, string Cost,string entry, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("insert into tbl_stock_out (Tbl_Main_id,RefNo,ItemId,ItemName,Batch,Unit,Given_Qty,Cost,BatchEntry)values('"+id+"','" + refno + "','" + ItemId + "','" + ItemName + "','" + Batch + "','" + Unit + "','" + Given_Qty + "','" + Cost + "','" + entry + "')",con,trans);
        }
        public void stock_out_main_save(string refno, string date,string name,string action,decimal amount,MySqlConnection con,MySqlTransaction trans)
        {
            db.trans_execute("insert into tbl_stock_out_main (RefNo,stock_date,LabName,Action,TotalAmount)values('" + refno + "','" + date + "','" + name + "','" + action + "','"+amount+"')",con,trans);
        }
        public string maxid_stockout( MySqlConnection con, MySqlTransaction trans)
        {
            string id = db.trans_scalar("select max(id) as id from tbl_stock_out_main",con,trans);
            return id;
        }
        public DataTable get_stockout_details(string date1, string date2)
        {
            DataTable dtb = db.table("select id,RefNo,stock_date,LabName,(select name from tbl_stocktransfer_partner where id=LabName) as Name, TotalAmount from tbl_stock_out_main where stock_date between '" + date1 + "' and '" + date2 + "' ");
            return dtb;
        }
        public DataTable get_main_details(string refno)
        {
            DataTable dt = db.table("select * from tbl_stock_out_main where id='"+refno+"'");
            return dt;
        }
        public DataTable get_stock_details(string refno)
        {
            DataTable dt = db.table("select Tbl_Main_id,RefNo,ItemId,(select item_code from tbl_items where id = ItemId) as itemCode,ItemName,Batch,Unit,Given_Qty,Cost,BatchEntry from tbl_stock_out where Tbl_Main_id='" + refno + "'");//(select GST from tbl_purchit where item_code=ItemId)as gst
            return dt;
        }
        public DataTable get_itemcode_stock_out(string refno)
        {
            DataTable dt = db.table("SELECT  Tbl_Main_id,RefNo,ItemId,ItemName,Batch,Unit,Cost, Given_Qty as qty from tbl_stock_out where Tbl_Main_id='" + refno + "' ");
            return dt;
            //DataTable dt = db.table("SELECT  Tbl_Main_id,RefNo,ItemId,ItemName,Batch,Unit,Cost, sum(Given_Qty) as qty from tbl_stock_out where Tbl_Main_id='" + refno + "' group by ItemId");
        }

        public DataTable docnumber()
        {
            DataTable Document_Number = db.table("SELECT max(cast(RefNo as UNSIGNED)) As 'RefNo' FROM tbl_stock_out");
            return Document_Number;
        }
        //stock out batch
        public DataTable get_batchdetails_stock_out(string ItemCode)
        {
            DataTable dtb = db.table("select distinct b.Entry_No,b.BatchNumber,b.Qty,cast(b.PrdDate as date) PrdDate,cast(b.ExpDate as date) ExpDate,b.Unit2,b.UnitMF,b.batch_sales_rate,purch_unit2 from tbl_BatchNumber b  where  b.Item_Code='" + ItemCode + "'and b.Qty>0 order by b.ExpDate");
            return dtb;
        }
        public DataTable trans_get_batchdetails(string ItemCode, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dtb = db.trans_table("select distinct b.Entry_No,b.BatchNumber,b.Qty,b.Unit2,b.UnitMF from tbl_BatchNumber b  where  b.Item_Code='" + ItemCode + "' order by b.ExpDate",con,trans);
            return dtb;
        }
        public DataTable get_batchdetails(string ItemCode)
        {
            DataTable dtb = db.table("select distinct b.Entry_No,b.BatchNumber,b.Qty,b.Unit2,b.UnitMF from tbl_BatchNumber b  where  b.Item_Code='" + ItemCode + "' order by b.ExpDate");
            return dtb;
        }
        //public DataTable get_batchdetails(string ItemCode)
        //{
        //    DataTable dtb = db.table("select distinct b.Entry_No,b.BatchNumber,b.Qty,b.Unit2,b.UnitMF from tbl_BatchNumber b  where  b.Item_Code='" + ItemCode + "' order by b.ExpDate");
        //    return dtb;
        //}
        public DataTable get_item_unitmf(string Itemid, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dt_unit1 = db.trans_table ("select Unit2,Unit1,UnitMF,OneUnitOnly,ISBatch from tbl_ITEMS where id='" + Itemid + "'",con,trans);
            return dt_unit1;
        }
        public DataTable get_item_unitmf(string Itemid)
        {
            DataTable dt_unit1 = db.table("select Unit2,Unit1,UnitMF,OneUnitOnly,ISBatch from tbl_ITEMS where id='" + Itemid + "'");
            return dt_unit1;
        }
        public int update_stockOut(string qty,string entryno, MySqlConnection con, MySqlTransaction trans)
        {
            int i = db.trans_execute("update tbl_batchnumber set Qty='" + qty + "' where Entry_No='" + entryno + "'",con,trans);
            return i;
        }
        //public int update_stockOut(string qty, string entryno)
        //{
        //    int i = db.execute("update tbl_batchnumber set Qty='" + qty + "' where Entry_No='" + entryno + "'");
        //    return i;
        //}
        public DataTable check_purno(string purno, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dt = db.trans_table("select PurchNumber from tbl_purchase where PurchNumber='" + purno + "'",con,trans);
            return dt;
        }
        //public DataTable check_purno(string purno, MySqlConnection con, MySqlTransaction trans)
        //{
        //    DataTable dt = db.trans_table("select PurchNumber from tbl_purchase where PurchNumber='" + purno + "'",con,trans);
        //    return dt;
        //}
        //Stock in batch
        public DataTable check_batch(string item_code)
        {
            DataTable dt = db.table("select ISBatch,item_name from tbl_ITEMS where id='" + item_code + "'");
            return dt;
        }

        // Add Labs
        public DataTable Fill_lab_combo()
        {
            DataTable dt3 = db.table("select * from tbl_stocktransfer_partner  order by id");
            return dt3;
        }
        public int save(string _unit)
        {
            int i = db.execute("insert into tbl_stocktransfer_partner (name) values('" + _unit + "')");
            return i;
        }
        public int update(string unit_id, string _unit)
        {
            int j = db.execute("update tbl_stocktransfer_partner set name='" + _unit + "' where id='" + unit_id + "' ");
            return j;
        }
        public int delete(string unit_id)
        {
            int i = db.execute("delete from tbl_stocktransfer_partner where id='" + unit_id + "'");
            return i;
        }
        //stock in
        public DataTable stockin_docnumber()
        {
            DataTable Document_Number = db.table("SELECT max(cast(RefNo as UNSIGNED)) As 'RefNo' FROM tbl_stock_in_main");
            return Document_Number;
        }
        public void stock_in_main_save(string refno, string date, string name, string action, decimal amount,MySqlConnection con,MySqlTransaction trans)
        {
            db.trans_execute("insert into tbl_stock_in_main (RefNo,stock_date,LabName,Action,TotalAmount)values('" + refno + "','" + date + "','" + name + "','" + action + "','" + amount + "')",con,trans);
        }
        public string maxid_stockin(MySqlConnection con,MySqlTransaction trans)
        {
            string id = db.trans_scalar("select max(id) as id from tbl_stock_in_main",con,trans);
            return id;
        }
        public void save_stockin(string id, string refno, string ItemId, string ItemName, string Batch, string Unit, string buy_qty, string Cost, string entry, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("insert into tbl_stock_in (Tbl_Main_id,RefNo,ItemId,ItemName,Batch,Unit,buy_Qty,Cost,BatchEntry)values('" + id + "','" + refno + "','" + ItemId + "','" + ItemName + "','" + Batch + "','" + Unit + "','" + buy_qty + "','" + Cost + "','" + entry + "')",con,trans);
        }
        public DataTable get_stockin_details(string date1, string date2)
        {
            DataTable dtb = db.table("select id,RefNo,stock_date,LabName,(select name from tbl_stocktransfer_partner where id=LabName) as Name, TotalAmount from tbl_stock_in_main where stock_date between '" + date1 + "' and '" + date2 + "' ");
            return dtb;
        }
        public DataTable get_stockIn_main_details(string refno)
        {
            DataTable dt = db.table("select * from tbl_stock_in_main where id='" + refno + "'");
            return dt;
        }
        public DataTable get_stockIn_details(string refno)
        {
            DataTable dt = db.table("select Tbl_Main_id,RefNo,ItemId,(select item_code from tbl_items where id = ItemId) as itemCode,ItemName,Batch,Unit,buy_Qty,Cost,BatchEntry,(select GST from tbl_purchit where item_code=ItemId limit 1)as gst from tbl_stock_in where Tbl_Main_id='" + refno + "'");
            return dt;
        }
        public DataTable get_itemcode_stock_in(string refno)
        {
            DataTable dt = db.table("SELECT  Tbl_Main_id,RefNo,ItemId,ItemName,Batch,Unit,Cost, (buy_Qty) as qty from tbl_stock_in where Tbl_Main_id='" + refno + "' ");//group by ItemId
            return dt;
            //DataTable dt = db.table("SELECT  Tbl_Main_id,RefNo,ItemId,ItemName,Batch,Unit,Cost, sum(buy_Qty) as qty from tbl_stock_in where Tbl_Main_id='" + refno + "' group by ItemId");
        }
        public DataTable get_itemName_cost(string refno,string purno, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dt = db.trans_table("select Item_Code,Desccription,Rate from tbl_purchit where Item_Code='" + refno + "' and PurchNumber='"+ purno + "'",con,trans);
            return dt;
        }
        public DataTable get_itemName_cost(string refno, string purno)
        {
            DataTable dt = db.table("select Item_Code,Desccription,Rate from tbl_purchit where Item_Code='" + refno + "' and PurchNumber='" + purno + "'");
            return dt;
        }
        public DataTable get_batchEntry(string refno, string purno, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dt = db.trans_table("select Item_Code,Entry_No,BatchNumber from tbl_batchnumber where Item_Code='" + refno + "' and BatchNumber='" + purno + "'",con,trans);
            return dt;
        }
        public DataTable get_batchEntry(string refno, string purno)
        {
            DataTable dt = db.table("select Item_Code,Entry_No,BatchNumber from tbl_batchnumber where Item_Code='" + refno + "' and BatchNumber='" + purno + "'");
            return dt;
        }
        //suplier
        public DataTable get_suppliername(string name, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable suppler_Name = db.trans_table("SELECT * from tbl_Supplier where Supplier_Name='" + name + "' and EntryNo='StockUpdation'",con,trans);
            return suppler_Name;
        }
        public DataTable get_Supliermaxid(MySqlConnection con,MySqlTransaction trans)
        {
            DataTable dt = db.trans_table("select max( Supplier_Code)from tbl_Supplier",con,trans);
            return dt;
        }
        //public DataTable get_Supliermaxid()
        //{
        //    DataTable dt = db.table("select max( Supplier_Code)from tbl_Supplier");
        //    return dt;
        //}
        //stock updation
        public DataTable stock_updation_docnumber()
        {
            DataTable Document_Number = db.table("SELECT max(cast(RefNo as UNSIGNED)) As 'RefNo' FROM tbl_stock_updation");
            return Document_Number;
        }
        public void stock_update_main_save(string refno, string date,  string action, decimal amount)
        {
            db.execute("insert into tbl_stock_updation_main (RefNo,updation_date,Action,TotalAmount)values('" + refno + "','" + date + "','" + action + "','" + amount + "')");
        }
        public string maxid_stockupdation(MySqlConnection con, MySqlTransaction trans)
        {
            string id = db.trans_scalar("select max(id) as id from tbl_stock_updation_main",con,trans);
            return id;
        }
        public void save_stockupdate(string date, string Action, string ItemId, string ItemName, string Batch, string Unit, string Given_Qty, string Cost, string entry,string last_qty,MySqlConnection con,MySqlTransaction trans )
        {
            db.trans_execute("insert into tbl_stock_updation (updation_date,Action,ItemId,ItemName,BatchEntry,Batch,upate_qty,unit,Cost,last_qty)values('" + date + "','" + Action + "','" + ItemId + "','" + ItemName + "','" + entry + "','" + Batch + "','" + Given_Qty + "','" + Unit + "','" + Cost + "','" + last_qty + "' )",con,trans );
        }
        public DataTable Stock_updation_todays_adjustmnt(string date)
        {
            DataTable dtb = db.table("select ItemId,updation_date from tbl_stock_updation where  updation_date='"+date+"' ");
            return dtb;
        }
        //stock updation report
        public DataTable stock_updation_dataload(string date1, string date2)
        {
            DataTable Document_Number = db.table("Select updation_date,Action,ItemId,ItemName ,batch,unit,upate_qty,Cost,last_qty from tbl_stock_updation where updation_date between '" + date1 + "' and '"+ date2 + "'");
            return Document_Number;
        }
        public DataTable stock_updation_dataload_on_action(string date1, string date2,string action)
        {
            DataTable Document_Number = db.table("Select updation_date,Action,ItemId,ItemName ,batch,unit,upate_qty,Cost,last_qty from tbl_stock_updation where updation_date between '" + date1 + "' and '" + date2 + "' and Action='"+ action + "'");
            return Document_Number;
        }
        public DataTable get_stockupdate_details(string date1, string date2)
        {
            DataTable dtb = db.table("select id,RefNo,updation_date,Action,TotalAmount from tbl_stock_updation_main where updation_date between '" + date1 + "' and '" + date2 + "' ");
            return dtb;
        }
        public DataTable get_stockupdation_main_details(string refno)
        {
            DataTable dt = db.table("select * from tbl_stock_updation_main where id='" + refno + "'");
            return dt;
        }
        public DataTable get_stockupdate_details(string refno)
        {
            DataTable dt = db.table("select main_id,RefNo,ItemId,(select item_code from tbl_items where id = ItemId) as itemCode,ItemName,Batch,Unit,upate_qty,Cost,BatchEntry from tbl_stock_updation where main_id='" + refno + "'");//(select GST from tbl_purchit where item_code=ItemId)as gst
            return dt;
        }
        public DataTable get_itemcode(string refno)
        {
            DataTable dt = db.table("SELECT  main_id,RefNo,ItemId,ItemName,Unit,Cost, sum(upate_qty) as qty from tbl_stock_updation where main_id='" + refno + "' group by ItemId");
            return dt;
        }
       
        public DataTable get_itemcode_frm_items(string ItemId)
        {
            DataTable dt = db.table("select item_code,GstVat from tbl_items where id ='" + ItemId + "'");
            return dt;
        }

        public DataTable get_stock_of_items(string ItemId,MySqlConnection con,MySqlTransaction trans)
        {
            DataTable dt = db.trans_table("select sum(qty) as stock from tbl_batchnumber where Item_Code='" + ItemId + "'",con,trans);
             return dt;
        }
        public DataTable get_stock_of_items(string ItemId)
        {
            DataTable dt = db.table ("select sum(qty) as stock from tbl_batchnumber where Item_Code='" + ItemId + "'");
            return dt;
        }


        public DataTable get_stockoutDetails(string from, string to)
        {
            DataTable dt = db.table("select a.stock_date,(select item_code from tbl_items where )");
            return dt;
        }

        public DataTable batch_stock(string ItemId,string batch)
        {
            DataTable dt = db.table("select qty  from tbl_batchnumber where Item_Code='" + ItemId + "'and Entry_No='" + batch + "'");
            return dt;
        }
    }
}
