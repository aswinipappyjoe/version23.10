using System;
using System.Data;
namespace PappyjoeMVC.Model
{
    public class Item_List_Model
    {
        Connection db = new Connection();
        public DataTable Fill_manufactureCombo()
        {
            DataTable gp_rs = db.table("SELECT id,manufacturer FROM tbl_manufacturer WHERE id IN (SELECT MAX(id) FROM tbl_manufacturer GROUP BY id) limit 10");
            return gp_rs;
        }

        public DataTable Fill_Grid(string type)
        {
            DataTable dtItems = db.table("Select A.id, A.item_code,A.item_name,(select sum(Qty) from tbl_BatchNumber where item_code= A.id) 'Stock' from tbl_ITEMS A where A.Product_type='"+ type + "' order by id limit 50");
            return dtItems;
        }
        public DataTable Fill_Grid_totalcount(string type)
        {
            DataTable dtItems = db.table("Select  A.id, A.item_code,A.item_name,(select sum(Qty) from tbl_BatchNumber where item_code= A.id) 'Stock' from tbl_ITEMS A where A.Product_type='" + type + "' order by id ");
            return dtItems;
        }
        public DataTable Get_manufacturename(string selectedValue)
        {
            DataTable dt_manu = db.table("SELECT id,manufacturer FROM tbl_manufacturer where manufacturer='" + selectedValue + "' ");
            return dt_manu;
        }
        public DataTable get_items_with_manufacture(int manufacture)
        {
            DataTable dtb = db.table("Select A.id,A.item_code,A.item_name,(select sum(Qty) 'Stock' from tbl_BatchNumber where item_code= A.id) 'Stock' from tbl_ITEMS A  where manufacturer='" + Convert.ToInt32(manufacture) + "'");
            return dtb;
        }
        public DataTable Search(string itemname,string type)
        {
           DataTable dtb_items = db.table("Select A.id, A.item_code,A.item_name,(select sum(Qty) from tbl_BatchNumber where item_code= A.id) Stock from tbl_ITEMS A  where A.item_code like '%" + itemname + "%' or A.item_name like '" + itemname + "%' and Product_type='"+type+"' order by A.id");
            return dtb_items;
        }
        public DataTable Search_wit_manufacture(string name, string manufacture,string type)
        {
            DataTable dtb_items = db.table("Select A.id,A.item_code,A.item_name,(select sum(Qty) from tbl_BatchNumber where item_code= A.id) Stock from tbl_ITEMS A  where (A.item_code like '%" + name + "%' or A.item_name like '" + name + "%') and A.manufacturer='" + manufacture + "' and  Product_type='" + type + "'  order by A.id");
            return dtb_items;
        }
        public DataTable Get_itemDetails(string itemcode)
        {
            DataTable dtb = db.table("select * from tbl_ITEMS where id='" + itemcode + "' ");
            return dtb;
        }
        public DataTable get_stock(string itemcode)
        {
            DataTable dtb = db.table("select sum(qty) as Stock from tbl_BatchNumber where Item_Code='" + itemcode + "'");
            return dtb;
        }
        public int delete(string itemcode)
        {
            int i = db.execute("delete from tbl_ITEMS where id='" + itemcode + "' ");
            return i;
        }
        public int delete_batchwithNoitem(string itemcode)
        {
            int i = db.execute("delete from tbl_batchnumber where Item_Code='" + itemcode + "' ");
            return i;
        }
        public DataTable Fill_manufacture()
        {
            DataTable gp_rs = db.table("SELECT id,manufacturer FROM tbl_manufacturer WHERE id IN (SELECT MAX(id) FROM tbl_manufacturer GROUP BY id) ");
            return gp_rs;
        }
        public DataTable Fill_Grid_scroll(int count)
        {
            DataTable dtItems = db.table("Select A.id, A.item_code,A.item_name,(select sum(Qty) from tbl_BatchNumber where item_code= A.id) 'Stock' from tbl_ITEMS A order by id limit "+count+",20");
            return dtItems;
        }

        // Consumables

        public DataTable get_consume_data()
        {
            DataTable dtItems = db.table("Select * from tbl_choose_consumables");
            return dtItems;
        }
        public void save_consume(string consume)
        {
            db.execute("insert into tbl_choose_consumables (consumables) values ('"+consume+"')");
        }
        public void update_consume(string consume)
        {
            db.execute("update tbl_choose_consumables set consumables ='" + consume + "'");
        }
    }
}
