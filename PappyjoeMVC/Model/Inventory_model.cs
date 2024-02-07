using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace PappyjoeMVC.Model
{
    public class Inventory_model
    {
        Connection db = new Connection();
        
        public DataTable Get_Stock(string id)
        {
          DataTable dt3 = db.table("Select stock from tbl_inventory_item where id='" + id + "'");
          return dt3;
        }                            
        public void update_addStock_qty(decimal current_Stock, string completedid)
        {
            db.execute("update tbl_add_stock set quantity='" + current_Stock + "'where id='" + completedid + "'");
        }
        public void update_inventoryStock(decimal total_Stock,string Plan_id)
        {
            db.execute("update tbl_inventory_item set stock='" + total_Stock + "'where id='" + Plan_id + "'");
        }
        //sales batch load
        public DataTable Load_batch(string ItemCode)
        {
            DataTable dtb = db.table("select b.BatchNumber,b.Qty,CONVERT(b.ExpDate , date) 'ExpDate' ,CONVERT(b.PrdDate ,date) 'PrdDate',b.PurchNumber,CONVERT(b.PurchDate,date)'PurchDate' ,b.Sup_Code,b.batch_sales_rate from tbl_BatchNumber b  where b.Item_Code='" + ItemCode + "'and b.Qty>0 and b.ExpDate>=(SELECT CONVERT(CURDATE(), date))");
            return dtb;
        }
       
        public DataTable dt_batch_wise_rate(string purno,string itemid)
        {
            DataTable dtitems = db.table("SELECT Rate,Qty FROM tbl_purchit WHERE PurchNumber='" + purno + "'and Item_Code='" + itemid + "' ");
            return dtitems;
        }
        public DataTable Get_consume_tick()
        {
            DataTable dtitems = db.table("select * from tbl_choose_consumables ");
            return dtitems;
        }
        public string get_itemid(string Item_Code,string type)
        {
            string str = db.scalar("select id from tbl_ITEMS where item_code='" + Item_Code + "' and Product_type='"+type+"'");
            return str;
        }
        public DataTable get_itemName(string Item_Code, string type) ////////////////////////////asddfsdg
        {
            DataTable str = db.table("select id,item_code,item_name from tbl_ITEMS where    item_name like '%" + Item_Code + "%' and Product_type='" + type + "' ");
            return str;
        }
        public string check_batch(string item_code)
        {
            string dt = db.scalar("select ISBatch from tbl_ITEMS where id='" + item_code + "'");
            return dt;
        }
        public DataTable Get_unites(string Item_Id, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dtitems = db.trans_table("select Unit1,Unit2,UnitMF,packing from tbl_ITEMS where id='" + Item_Id + "'", con, trans);
            return dtitems;
        }
        public DataTable Get_unites(string Item_Id)
        {
            DataTable dtitems = db.table("select Unit1,Unit2,UnitMF,packing from tbl_ITEMS where id='" + Item_Id + "'");
            return dtitems;
        }
        public DataTable Get_itemdetails_from_purchaseit(string Itemid, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dtb_Avg = db.trans_table("select Qty,Rate,Unit,UNIT2,UnitMF from tbl_PURCHIT where Item_Code='" + Itemid + "'",con,trans);
            return dtb_Avg;
        }
        public DataTable get_companydetails()
        {
            System.Data.DataTable dtp = db.table("select name,contact_no,street_address,path,email,website  from tbl_practice_details");
            return dtp;
        }
        public DataTable get_item_unitmf(string Itemid)
        {
            DataTable dt_unit1 = db.table("select Unit2,Unit1,UnitMF,OneUnitOnly,HSN_Number from tbl_ITEMS where id='" + Itemid + "'");
            return dt_unit1;
        }
        //sales item list
        public DataTable Load_items_wit_itemcode(string Item_Code)
        {
            DataTable dt = db.table("select t.id,t.item_code,item_name,c.Name,(select SUM(qty) from tbl_BatchNumber where item_code=t.id) as 'Current_Stock',(CONVERT( Sales_Rate_Max,  CHAR(50))+'/'+Unit1) as 'Cost (Unit1)', (CONVERT(Sales_Rate_Max2, CHAR(50))+'/'+t.Unit2) as 'Cost (Unit2)' from tbl_ITEMS t left join tbl_Category C on C.id=t.Cat_Number  left join tbl_BatchNumber B on b.Item_Code=t.id where B.Qty <> -1 and B.Qty <> 0 and t.id='" + Item_Code + "' group by t.item_code,t.item_name,c.Name,Sales_Rate_Max,Unit1,Sales_Rate_Max2,t.Unit2 limit 200");
            return dt;
        }
        public DataTable Load_items_qty(string item)
        {
            //DataTable dt = db.table("select  t.id, t.item_code,item_name,c.Name,(select SUM(qty) from tbl_BatchNumber where item_code=t.id) as 'Current_Stock',(CONVERT( Sales_Rate_Max, CHAR(50))+'/'+Unit1) as 'Cost (Unit1)', (CONVERT(Sales_Rate_Max2 ,CHAR(50))+'/'+t.Unit2) as 'Cost (Unit2)' from tbl_ITEMS t left join tbl_Category C on C.id=t.Cat_Number  left join tbl_BatchNumber B on b.Item_Code=t.id where B.Qty <> -1 and B.Qty <> 0 and  t.Product_type='" + type + "'  group by t.item_code,t.item_name,c.Name,Sales_Rate_Max,Unit1,Sales_Rate_Max2,t.Unit2 ");
            DataTable dt = db.table("select distinct B.item_code,(select SUM(qty) from tbl_BatchNumber where item_code=B.item_code) as 'Current_Stock' from  tbl_BatchNumber B  where B.item_code='"+ item + "' ");
            return dt;
        }
       
        public DataTable Load_items(string type)
        {
            //DataTable dt = db.table("select  t.id, t.item_code,item_name,c.Name,(select SUM(qty) from tbl_BatchNumber where item_code=t.id) as 'Current_Stock',(CONVERT( Sales_Rate_Max, CHAR(50))+'/'+Unit1) as 'Cost (Unit1)', (CONVERT(Sales_Rate_Max2 ,CHAR(50))+'/'+t.Unit2) as 'Cost (Unit2)' from tbl_ITEMS t left join tbl_Category C on C.id=t.Cat_Number  left join tbl_BatchNumber B on b.Item_Code=t.id where B.Qty <> -1 and B.Qty <> 0 and  t.Product_type='" + type + "'  group by t.item_code,t.item_name,c.Name,Sales_Rate_Max,Unit1,Sales_Rate_Max2,t.Unit2 ");
            DataTable dt = db.table("select distinct B.item_code,(select SUM(qty) from tbl_BatchNumber where item_code=B.item_code) as 'Current_Stock' from  tbl_BatchNumber B  where B.Qty <> -1 and B.Qty <> 0 ");
            return dt;
        }
        public DataTable each_item_details(string item_code,string type)
        {
            DataTable dt = db.table("select  t.id, t.item_code,t.item_name,t.Cat_Number,(CONVERT( Sales_Rate_Max, CHAR(50))+'/'+Unit1) as 'Cost (Unit1)', (CONVERT(Sales_Rate_Max2 ,CHAR(50))+'/'+t.Unit2) as 'Cost (Unit2)' from tbl_ITEMS t where  t.Product_type='" + type + "' and t.id='" + item_code + "'  ");
            return dt;
        }
        public DataTable dt_cat(string Cat_Number)
        {
            DataTable dt = db.table("select Name from  tbl_Category  where id='" + Cat_Number + "'  ");
            return dt;
        }
        public DataTable search_wit_itemcode(string name)
        {
            //DataTable dtb = db.table("select t.id,t.item_code,item_name,c.Name,(select SUM(qty) from tbl_BatchNumber where item_code=t.id) as 'Current_Stock',(CONVERT( Sales_Rate_Max, CHAR(50))+'/'+Unit1) as 'Cost (Unit1)', (CONVERT(Sales_Rate_Max2 ,CHAR(50))+'/'+t.Unit2) as 'Cost (Unit2)' from tbl_ITEMS t left join tbl_Category C on C.id=t.Cat_Number  left join tbl_BatchNumber B on b.Item_Code=t.id where (t.item_code Like '%" + name + "%' or item_name like '%" + name + "%' or c.Name like '%" + name + "%') and B.Qty <> -1 and B.Qty <> 0  group by t.item_code,t.item_name,c.Name,Sales_Rate_Max,Unit1,Sales_Rate_Max2,t.Unit2 ");
            DataTable dtb = db.table("select t.id,t.item_code,item_name,t.Cat_Number,(CONVERT( Sales_Rate_Max, CHAR(50))+'/'+Unit1) as 'Cost (Unit1)', (CONVERT(Sales_Rate_Max2 ,CHAR(50))+'/'+t.Unit2) as 'Cost (Unit2)' from tbl_ITEMS t where (t.item_code Like '%" + name + "%' or item_name like '%" + name + "%')   limit 200");

            return dtb;
        }
        public DataTable dt_minimum_stock(string itemid)
        {
            DataTable dtb = db.table("select t.id,t.item_code,MinimumStock from tbl_ITEMS t where  t.id = '" + itemid + "' ");

            return dtb;
        }
        //batch sale
        public DataTable get_item_frm_purchase(string itemcode)
        {
            DataTable dtb = db.table("select Item_Code from  TBL_PURCHIT  where  Item_Code='" + itemcode + "'");
            return dtb;
        }
        public DataTable get_batchdetails(string ItemCode)
        {
            //DataTable dtb = db.table("select distinct b.Entry_No,b.BatchNumber,b.Qty,cast(b.PrdDate as date) PrdDate,cast(b.ExpDate as date) ExpDate, b.Unit2,b.UnitMF,p.rate,b.sales_default_qty  from tbl_BatchNumber b INNER JOIN TBL_PURCHIT P ON P.Item_Code = b.Item_Code   where  b.Item_Code='" + ItemCode + "'and b.Qty>0 GROUP BY p.Unit  order by b.ExpDate");
            DataTable dtb = db.table("select distinct b.Entry_No,b.BatchNumber,b.Qty,cast(b.PrdDate as date) PrdDate,cast(b.ExpDate as date) ExpDate,b.Unit2,b.UnitMF,b.sales_default_qty,b.batch_sales_rate,purch_unit2 from tbl_BatchNumber b  where  b.Item_Code='" + ItemCode + "'and b.Qty>0   order by b.ExpDate ");//GROUP BY b.BatchNumber
            return dtb;
        }
        public DataTable get_batch_wise_rate(string batch,string itemcode)
        {
            DataTable dt = db.table("select Item_Code,BatchNumber,Entry_No,Qty,Unit2,UnitMF,sales_default_qty,purch_unit2,batch_sales_rate,batch_rate from tbl_BatchNumber where  Item_Code='" + itemcode + "' and BatchNumber ='" + batch + "' ");
            return dt;
        }
        public DataTable batch_wise_rate(string batch, string itemcode)
        {
            DataTable dt = db.table("select Item_Code,BatchNumber,Entry_No,batch_rate from tbl_BatchNumber where  Item_Code='" + itemcode + "' and BatchNumber ='" + batch + "' ");
            return dt;
        }
        //public DataTable get_batch_wise_rate(string ItemCode)
        //{
        //    DataTable dtb = db.table("select distinct ,b.BatchNumber,b.Qty, b.Unit2,b.UnitMF ,p.rate from tbl_BatchNumber b INNER JOIN TBL_PURCHIT P ON P.PurchNumber= b.PurchNumber  where  b.Item_Code='" + ItemCode + "'and b.Qty>0 and b.BatchNumber='" + ItemCode + "'  ");
        //    return dtb;
        //}    
        public DataTable get_batchdetails_opening(string ItemCode)
        {
            DataTable dtb = db.table("select distinct b.Entry_No,b.BatchNumber,b.Qty,cast(b.PrdDate as date) PrdDate,cast(b.ExpDate as date) ExpDate, b.Unit2,b.UnitMF,b.sales_default_qty,b.batch_sales_rate,purch_unit2  from tbl_BatchNumber b  where  b.Item_Code='" + ItemCode + "'and b.Qty>0  ");
            return dtb;
        }
        public DataTable itemdetails(string itemid)
        {
            DataTable dtb = db.table("select Sales_Rate_Max,Unit1,Unit2,OneUnitOnly,UnitMF from tbl_ITEMS where id='" + itemid + "' ");
            return dtb;
        }
        public DataTable get_unit_wise_rate(string itemid, string unit)
        {
            DataTable dtb = db.table("select p.rate,p.Unit from TBL_PURCHIT p where Item_Code='" + itemid + "' and unit='" + unit + "'");
            return dtb;
        }
        public DataTable get_item_salesrate_minimun(string itemid)
        {
            DataTable dtb = db.table("select Sales_Rate_Max2,Purch_Rate2 from tbl_items where id='" + itemid + "'");
            return dtb;
        }
        public DataTable get_item_salesrate(string itemid)
        {
            DataTable dtb = db.table("select Sales_Rate_Max,Purch_Rate from tbl_items where id='" + itemid + "'");
            return dtb;
        }
        //purchase itemist
        public DataTable  LoadItems(string type)
        {
            //DataTable dt = db.table("select t.id,t.item_code,t.item_name,m.manufacturer,(select sum(Qty) from tbl_BatchNumber where Item_Code=t.id) as 'Current_Stock',(CONVERT(Purch_Rate,CHAR(50))+'/'+t.Unit1) as 'Cost(Unit1)',(CONVERT(Purch_Rate2,CHAR(50))+'/'+t.Unit2) as 'Cost(Unit2)' from tbl_ITEMS t inner  join  tbl_manufacturer m on m.id=t.manufacturer left join tbl_BatchNumber b on b.Item_Code=t.id where t.Product_type='"+type+"' group by t.item_code,t.item_name,m.manufacturer,t.Purch_Rate,t.Unit1,t.Purch_Rate2,t.Unit2,b.Item_Code Order by t.item_name");
            DataTable dt = db.table("select t.id,t.item_code,t.item_name,t.manufacturer,(CONVERT(Purch_Rate,CHAR(50))+'/'+t.Unit1) as 'Cost(Unit1)',(CONVERT(Purch_Rate2,CHAR(50))+'/'+t.Unit2) as 'Cost(Unit2)' from tbl_ITEMS t where t.Product_type='" + type + "' order by t.item_name limit 200");

            return dt;
        }
        public DataTable Load_itemcode_details(string item_,string type)
        {
            //DataTable dt = db.table("select t.id,t.item_code,t.item_name,m.manufacturer,(select sum(Qty) from tbl_BatchNumber where Item_Code=t.id) as 'Current_Stock',(CONVERT(Purch_Rate,CHAR(50))+'/'+t.Unit1) as 'Cost(Unit1)',(CONVERT(Purch_Rate2,CHAR(50))+'/'+t.Unit2) as 'Cost(Unit2)' from tbl_ITEMS t inner  join  tbl_manufacturer m on m.id=t.manufacturer left join tbl_BatchNumber b on b.Item_Code=t.id where t.id='" + item_ + "'  group by t.item_code,t.item_name,m.manufacturer,t.Purch_Rate,t.Unit1,t.Purch_Rate2,t.Unit2,b.Item_Code Order by t.item_name");
            DataTable dt = db.table("select t.id,t.item_code,t.item_name,t.manufacturer,(CONVERT(Purch_Rate,CHAR(50))+'/'+t.Unit1) as 'Cost(Unit1)',(CONVERT(Purch_Rate2,CHAR(50))+'/'+t.Unit2) as 'Cost(Unit2)' from tbl_ITEMS t where t.Product_type='" + type + "' and  t.id='" + item_ + "' order by t.item_name");

            return dt;
        }
        public DataTable Search(string item,string type)
        {
            DataTable dt = db.table("select t.id,t.item_code,t.item_name,t.manufacturer,(CONVERT(Purch_Rate,CHAR(50))+'/'+t.Unit1) as 'Cost(Unit1)',(CONVERT(Purch_Rate2,CHAR(50))+'/'+t.Unit2) as 'Cost(Unit2)' from tbl_ITEMS t where (t.item_code like'%" + item + "%' or t.item_name like '%" + item + "%' ) and  t.Product_type='" + type + "' order by t.item_name limit 200");

            //DataTable dtb = db.table("select t.id,t.item_code,t.item_name,m.manufacturer,(select sum(Qty) from tbl_BatchNumber where Item_Code=t.id) as 'Current_Stock',(CONVERT(Purch_Rate,CHAR(50))+'/'+t.Unit1) as 'Cost(Unit1)',(CONVERT(Purch_Rate2,CHAR(50))+'/'+t.Unit2) as 'Cost(Unit2)' from tbl_ITEMS t inner  join  tbl_manufacturer m on m.id=t.manufacturer left join tbl_BatchNumber b on b.Item_Code=t.id where (t.item_code like'%" + item + "%' or t.item_name like '%" + item + "%' or m.manufacturer like '%" + item + "%')  and t.Product_type='" + type + "'  group by t.item_code,t.item_name,m.manufacturer,t.Purch_Rate,t.Unit1,t.Purch_Rate2,t.Unit2,b.Item_Code");
            return dt;
        }
        //sales+_return_batch
        public DataTable dtb_load(string InvNum,string ItemCode)
        {
            DataTable dt_batchSale = db.table("select S.BatchNumber,S.Entry_No,S.Qty,BatchEntry,S.RetQty,S.BatchEntry,N.Qty Stock from tbl_BatchSale S inner join  tbl_BatchNumber N on N.Entry_No=S.BatchEntry where InvNumber='" + Convert.ToInt32(InvNum) + "' and S.Item_Code='" + ItemCode + "' ");
            return dt_batchSale;
        }
        public DataTable Load_manufactr(string item)
        {
            DataTable dt = db.table("select manufacturer  from tbl_manufacturer where id='" + item + "' ");
            return dt;

        }
    }
}
