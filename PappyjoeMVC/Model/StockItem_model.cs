using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace PappyjoeMVC.Model
{
    public class StockItem_model
    {
        Connection db = new Connection();
        public DataTable LoadSupplier()
        {
            DataTable gp_rs = db.table("select Supplier_Code,Supplier_Name from tbl_supplier order by Supplier_Code");
            return gp_rs;
        }
        public DataTable dt_get_count(string type)
        {
            DataTable dt_stock = db.table("select item_code as id ,(select item_code from tbl_items where id=B.Item_Code) as item_code,(select sum( qty) from tbl_batchnumber where Item_Code=B.Item_Code) qty from tbl_batchnumber B where b.Product_type='" + type + "' group by item_code order by B.item_code  ");
            return dt_stock;
        }
        public DataTable load_stock(string type)
        {
            //db.execute("ALTER TABLE tbl_items ADD INDEX author_index( Item_Code,item_name,Unit1)");
            //db.execute("ALTER TABLE tbl_batchnumber ADD INDEX author_index1( Item_Code )");
            DataTable dt_stock = db.table("select item_code as id ,(select item_code from tbl_items where id=B.Item_Code) as item_code,(select sum( qty) from tbl_batchnumber where Item_Code=B.Item_Code) qty from tbl_batchnumber B where b.Product_type='"+type+"' group by item_code order by B.item_code limit 25 ");//limit 50
            //db.execute("drop index author_index1 on tbl_batchnumber");
            //db.execute("drop index author_index on tbl_items");
            return dt_stock;
        }
        public DataTable load_fullstock(int count)
        {
            //db.execute("ALTER TABLE tbl_items ADD INDEX author_index( Item_Code,item_name,Unit1)");
            //db.execute("ALTER TABLE tbl_batchnumber ADD INDEX author_index1( Item_Code )");
            DataTable dt_stock = db.table("select item_code as id ,(select item_code from tbl_items where id=B.Item_Code) as item_code,(select sum( qty) from tbl_batchnumber where Item_Code=B.Item_Code) qty from tbl_batchnumber B   group by item_code order by B.item_code limit " + count + ",25");
            //db.execute("drop index author_index1 on tbl_batchnumber");
            //db.execute("drop index author_index on tbl_items");
            return dt_stock;
        }
        public DataTable minimumStock(string item_code)
        {
            DataTable dtb_Min = db.table(" select MinimumStock from tbl_items where id='" + item_code + "'");//item_code
            return dtb_Min;
        }
        public DataTable getsupplierid(string supplier)//start sanoop
        {
            string qry = "select Supplier_Code from tbl_supplier where Supplier_Name='" + supplier + "'";
            DataTable dt = new DataTable();
            dt = db.table(qry);
            return dt;
        }//end sanoop
        public DataTable itemdetails(string item_code)
        {
            DataTable dtunit = db.table(" select item_name,OneUnitOnly,UnitMF,Unit1,Unit2,purch_rate,purch_rate2,Sales_Rate_Max,Sales_Rate_Max2,Shelf_No,GstVat from tbl_items where id='" + item_code + "'");//item_code                      
            return dtunit;
        }
        public DataTable get_batch_rate(string item_code)
        {
            DataTable dtb_Min = db.table(" select Item_Code,BatchNumber,batch_rate,batch_sales_rate,purch_unit2,Qty,Unit2 from tbl_batchnumber where Item_Code='" + item_code + "'");//item_code
            return dtb_Min;
        }
        public DataTable get_batch_sale_rate(string item_code)
        {
            DataTable dtb_Min = db.table(" select Item_Code,BatchNumber,Qty,BatchEntry,Unit2 from tbl_batchsale where Item_Code='" + item_code + "'");//item_code
            return dtb_Min;
        }
        public DataTable get_sales_rate(string item_code,string batch)
        {
            DataTable dtb_Min = db.table(" select Item_Code,BatchNumber,batch_sales_rate,purch_unit2,Qty from tbl_batchnumber where Item_Code='" + item_code + "' and BatchNumber='" + batch + "' ");//item_code
            return dtb_Min;
        }
        public DataTable get_lst_pur_rate(string item_code)
        {
            DataTable dtb_Min = db.table(" SELECT Rate FROM tbl_purchit WHERE PurchNumber =(select max(PurchNumber) from tbl_purchit WHERE Item_Code='" + item_code + "')  ");//item_code
            return dtb_Min;
        }
        public DataTable get_lst_sales_rate(string item_code)
        {
            DataTable dtb_Min = db.table(" SELECT Rate FROM tbl_saleit WHERE InvNumber =(select max(InvNumber) from tbl_saleit where Item_Code='" + item_code + "' ) ");//item_code
            return dtb_Min;
        }
        public DataTable search_minimum(string type)
        {
            //db.execute("ALTER TABLE tbl_batchnumber ADD INDEX author_index1( Item_Code )");
            DataTable sqlstr = db.table("select item_code as id ,(select item_code from tbl_items where id=B.Item_Code) as item_code,(select sum( qty) from tbl_batchnumber where Item_Code=B.Item_Code) qty from tbl_batchnumber B where b.Product_type='" + type + "'   group by item_code order by B.item_code limit 25");
            //db.execute("drop index author_index1 on tbl_batchnumber");
            return sqlstr;
        }
        public DataTable search_minium_wit_itemname(string search)
        {
            //db.execute("ALTER TABLE tbl_batchnumber ADD INDEX author_index1( Item_Code )");
            DataTable sqlstr = db.table("select B.item_code as id, tbl_ITEMS.item_code, (select sum(qty) from tbl_batchnumber where Item_Code= B.Item_Code) qty,tbl_ITEMS.item_name from tbl_batchnumber B left join tbl_ITEMS on tbl_ITEMS.id=b.item_code where tbl_ITEMS.item_code like '" + search + "%' OR tbl_ITEMS.item_name like'%" + search + "%' group by B.item_code order by B.item_code limit 50");
            //db.execute("drop index author_index1 on tbl_batchnumber");
            return sqlstr;
        }
        public DataTable get_supcode(string sup)
        {
            DataTable dt_sup = db.table(" select Supplier_Code from tbl_Supplier where Supplier_Name='" + sup + "'");
            return dt_sup;
        }
        public DataTable Load_supplier_items(string Sup_Code, string type)
        {
            //db.execute("ALTER TABLE tbl_batchnumber ADD INDEX author_index1( Item_Code,Sup_Code )");
            //db.execute("ALTER TABLE tbl_batchnumber ADD INDEX author_index1( Item_Code )");
            DataTable dtb = db.table("select Item_Code as id, (select item_code from tbl_items where id=B.Item_Code) as item_code,sum(qty) qty  from tbl_BatchNumber B where Sup_Code='" + Sup_Code + "' and b.Product_type='" + type + "' group by B.Item_Code ");
            //db.execute("drop index author_index1 on tbl_batchnumber");
            return dtb;
        }
        public DataTable get_company_details()
        {
           DataTable dtp = db.table("select name,contact_no,street_address,email,website from tbl_practice_details");
            return dtp;
        }
        public DataTable load_stock_lil(int count)
        {
            //db.execute("ALTER TABLE tbl_items ADD INDEX author_index( Item_Code,item_name,Unit1)");
            //db.execute("ALTER TABLE tbl_batchnumber ADD INDEX author_index1( Item_Code )");
            DataTable dt_stock = db.table("select item_code as id ,(select item_code from tbl_items where id=B.Item_Code) as item_code,(select sum( qty) from tbl_batchnumber where Item_Code=B.Item_Code) qty from tbl_batchnumber B group by item_code order by B.item_code limit "+count+"");
            //db.execute("drop index author_index1 on tbl_batchnumber");
            //db.execute("drop index author_index on tbl_items");
            return dt_stock;
        }
        //bhj
        public DataTable exp_prd()
        {
            DataTable dt = db.table("select item_code,BatchNumber,Qty,batch_sales_rate,PurchDate,ExpDate from tbl_batchnumber where ExpDate <= '"+DateTime.Now.Date.ToString("yyy-MM-dd")+"' order by ExpDate ASC");
            //DataTable dt = db.table("select item_code,BatchNumber,Qty,batch_sales_rate,PurchDate,ExpDate from tbl_batchnumber where year(ExpDate)=year(now()) and month(ExpDate)<month(now()) order by ExpDate ASC");

            return dt;
        }
        public DataTable get_itemname(string id)
        {
            DataTable dt = db.table("select item_code,item_name,Unit1 from tbl_items where id='" + id + "' ");
            return dt;
        }
        public DataTable consumables()
        {
            DataTable dt = db.table("select item_code as id, (select item_code from tbl_items where id = B.Item_Code) as item_code,(select sum(qty) from tbl_batchnumber where Item_Code = B.Item_Code) qty from tbl_batchnumber B where B.Product_type='Consumable' order by B.item_code ASC");
            return dt;
        }
    }
}
