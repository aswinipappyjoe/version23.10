using MySql.Data.MySqlClient;
using System.Data;
namespace PappyjoeMVC.Model
{
    public class Purchase_model
    {
        Connection db = new Connection();
        //load purchase items
        public DataTable Get_itemdetails(string itemCode)
        {
            DataTable dtitems = db.table("select id,Item_Code,item_name,Packing,Purch_Rate,Unit1,Unit2,UnitMF,Barcode,GstVat from tbl_ITEMS where id='" + itemCode + "'");
            return dtitems;
        }
        public DataTable Get_itemdetails_itemname(string itemname)
        {
            DataTable dtitems = db.table("select id,Item_Code,item_name,Packing,Purch_Rate,Unit1,Unit2,UnitMF,Barcode,GstVat from tbl_ITEMS where item_name='" + itemname + "'");
            return dtitems;
        }
        public DataTable Search_itemdetails_itemname(string itemname)
        {
            DataTable dtitems = db.table("select id,Item_Code,item_name,Packing,Purch_Rate,Unit1,Unit2,UnitMF,Barcode from tbl_ITEMS where item_name like '" + itemname + "%'");
            return dtitems;
        }
        public DataTable get_itemdetails_Barcode(string name)
        {
            DataTable dt = new DataTable();
            dt = db.table(" select i.id,i.item_code,i.Sales_Rate_Max,i.Packing,i.Unit1,i.Unit2,i.OneUnitOnly,Barcode from tbl_ITEMS i where i.Barcode='" + name + "'");// inner join tbl_PURCHIT p on p.Item_Code = i.id  where i.id='" +name + "'");
            //if (dt.Rows.Count == 0)
            //{
            //    dt = db.table(" select i.id,i.item_code,i.Sales_Rate_Max,i.Packing,i.Unit1,i.Unit2,i.OneUnitOnly from tbl_ITEMS i where i.Barcode='" + name + "'");
            //}
            return dt;
        }
        public DataTable get_maxpurNumber(string itemCode)
        {
            DataTable dt_PurNum = db.table("select MAX(PurchNumber)from tbl_PURCHIT where Item_Code='" + itemCode + "'");
            return dt_PurNum;
        }
        public DataTable Load_Suplier()
        {
            DataTable dt = db.table("select Supplier_Code,Supplier_Name from tbl_Supplier where EntryNo='Supplier' order by Supplier_Code ");
            return dt;//where  Supplier_Name != 'stockIn_updation'
        }
        public DataTable LoadSuplier_wit_supname(string name)
        {
            DataTable dt = db.table("select Supplier_Code,Supplier_Name  from tbl_Supplier where Supplier_Name like '" + name + "%'  or Supplier_Code like '" + name + "%' or Phone1 like '" + name + "%'  and EntryNo='Supplier'");//Supplier_Name != 'stockIn_updation'
            return dt;
        }
        public DataTable get_suppcode(string name)
        {
            DataTable dt = db.table("select Supplier_Code,Supplier_Name  from tbl_Supplier where Supplier_Name = '" + name + "' and EntryNo='Supplier'");//Supplier_Name != 'stockIn_updation'
            return dt;

        }
        public DataTable check_batch(string item_code)
        {
            DataTable dt = db.table("select ISBatch,item_name,Purch_Rate,Sales_Rate_Max from tbl_ITEMS where id='" + item_code + "'");
            return dt;
        }
        public int save_purchase(string PurchNumber, string InvNumber, string PurchDate, string Sup_Code, string TotalAmount, string GrandTotal, string DiscPercentage, string DiscAmount, string TotalCost, string status, string type,string purch_type, MySqlConnection con, MySqlTransaction trans)
        {
            int i = db.trans_execute("insert into tbl_PURCHASE (PurchNumber,InvNumber,PurchDate,Sup_Code,TotalAmount,DiscPercent,DiscAmount,GrandTotal,Memo,PurchType,TotalCost,Product_type) values('" + PurchNumber + "','" + InvNumber + "','" + PurchDate + "','" + Sup_Code + "','" + TotalAmount + "','" + DiscPercentage + "','" + DiscAmount + "','" + GrandTotal + "','" + status + "','"+ purch_type + "','" + TotalCost + "','" + type + "')", con, trans);
            return i;
        }
        //public int save_purchase(string PurchNumber, string InvNumber, string PurchDate, string Sup_Code, string TotalAmount, string GrandTotal, string DiscPercentage, string DiscAmount, string TotalCost, string status, string type)
        //{
        //    int i = db.execute("insert into tbl_PURCHASE (PurchNumber,InvNumber,PurchDate,Sup_Code,TotalAmount,DiscPercent,DiscAmount,GrandTotal,Memo,PurchType,TotalCost,Product_type) values('" + PurchNumber + "','" + InvNumber + "','" + PurchDate + "','" + Sup_Code + "','" + TotalAmount + "','" + DiscPercentage + "','" + DiscAmount + "','" + GrandTotal + "','" + status + "','cash','" + TotalCost + "','" + type + "')");
        //    return i;
        //}
        public int update_purchase(string PurchNumber, string InvNumber, string PurchDate, string Sup_Code, string TotalAmount, string GrandTotal, string DiscPercentage, string DiscAmount, string TotalCost, string status, string type, string purch_type, MySqlConnection con, MySqlTransaction trans)
        {
            int i = db.trans_execute("update tbl_PURCHASE  set PurchNumber='" + PurchNumber + "' ,InvNumber='" + InvNumber + "',PurchDate='" + PurchDate + "',Sup_Code='" + Sup_Code + "',TotalAmount='" + TotalAmount + "',DiscPercent='" + DiscPercentage + "',DiscAmount='" + DiscAmount + "',GrandTotal='" + GrandTotal + "',Memo='" + status + "',PurchType='"+purch_type+"',TotalCost='" + TotalCost + "', Product_type='" + type + "' where PurchNumber= '" + PurchNumber + "' ", con, trans);
            return i;
        }
        public DataTable  check_have_same_batch(string item,string batch, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable bat = db.trans_table("select BatchNumber,Entry_No,Qty from tbl_BatchNumber where Item_Code='" + item + "'  and BatchNumber= '" + batch + "' ", con
                , trans);
            return bat;

        }
        public int save_batchNumber(string Item_Code, string BatchNumber, int Qty,decimal rate,decimal sales_rate, string Unit2, string pur_unit2, string UnitMF, string PurchNumber, string PrdDate, string ExpDate, string Period, string Sup_Code, string PurchDate, string IsExpDate, string product_type, MySqlConnection con, MySqlTransaction trans)
        {
            int a = db.trans_execute("insert into tbl_BatchNumber (Item_Code,BatchNumber,Qty,batch_rate,batch_sales_rate,Unit2,purch_unit2,UnitMF,PurchNumber,PrdDate,ExpDate,Period,Sup_Code,PurchDate,IsExpDate,Product_type) values ('" + Item_Code + "','" + BatchNumber + "','" + Qty + "','" + rate + "','" + sales_rate + "','" + Unit2 + "','" + pur_unit2 + "','" + UnitMF + "','" + PurchNumber + "','" + PrdDate + "','" + ExpDate + "','" + Period + "','" + Sup_Code + "','" + PurchDate + "','" + IsExpDate + "','" + product_type + "')", con, trans);
            return a;
        }
        public int save_batchNumber(string Item_Code, string BatchNumber, int Qty,decimal rate, decimal sales_rate, string Unit2,string pur_unit2, string UnitMF, string PurchNumber, string PrdDate, string ExpDate, string Period, string Sup_Code, string PurchDate, string IsExpDate, string product_type)
        {
            int a = db.execute("insert into tbl_BatchNumber (Item_Code,BatchNumber,Qty,batch_rate,batch_sales_rate,Unit2,purch_unit2,UnitMF,PurchNumber,PrdDate,ExpDate,Period,Sup_Code,PurchDate,IsExpDate,Product_type) values ('" + Item_Code + "','" + BatchNumber + "','" + Qty + "','" + rate + "','" + sales_rate + "','" + Unit2 + "','" + pur_unit2 + "','" + UnitMF + "','" + PurchNumber + "','" + PrdDate + "','" + ExpDate + "','" + Period + "','" + Sup_Code + "','" + PurchDate + "','" + IsExpDate + "','" + product_type + "')");
            return a;
        }
        public int update_same_batchNumber(string Item_Code, string BatchNumber, int Qty,  string Unit2, string UnitMF, string PurchNumber, string PrdDate, string ExpDate, string Period, string Sup_Code, string PurchDate, string IsExpDate, string product_type, string Entry_No, MySqlConnection con, MySqlTransaction trans)
        {
            int a = db.trans_execute("update tbl_BatchNumber set  Item_Code='" + Item_Code + "',BatchNumber='" + BatchNumber + "',Qty='" + Qty + "',  Unit2='" + Unit2 + "',UnitMF='" + UnitMF + "',PurchNumber='" + PurchNumber + "',PrdDate='" + PrdDate + "',ExpDate='" + ExpDate + "',Period='" + Period + "',Sup_Code='" + Sup_Code + "',PurchDate='" + PurchDate + "',IsExpDate='" + IsExpDate + "',Product_type='" + product_type + "' where Item_Code='" + Item_Code + "' and  Entry_No	='" + Entry_No + "'", con, trans);
            return a;
        }
        public int update_batchNumber(string Item_Code, string BatchNumber, int Qty,decimal rate,decimal sales_rate, string Unit2, string purch_unit2, string UnitMF, string PurchNumber, string PrdDate, string ExpDate, string Period, string Sup_Code, string PurchDate, string IsExpDate, string product_type, string Entry_No, MySqlConnection con, MySqlTransaction trans)
        {
            int a = db.trans_execute("update tbl_BatchNumber set  Item_Code='" + Item_Code + "',BatchNumber='" + BatchNumber + "',Qty='" + Qty + "',batch_rate='" + rate + "',batch_sales_rate='" + sales_rate + "',  Unit2='" + Unit2 + "',purch_unit2='" + purch_unit2 + "',UnitMF='" + UnitMF + "',PurchNumber='" + PurchNumber + "',PrdDate='" + PrdDate + "',ExpDate='" + ExpDate + "',Period='" + Period + "',Sup_Code='" + Sup_Code + "',PurchDate='" + PurchDate + "',IsExpDate='" + IsExpDate + "',Product_type='" + product_type + "' where PurchNumber='" + PurchNumber + "' and  Entry_No	='" + Entry_No + "'", con, trans);
            return a;
        }
        public int update_batchNumber(string Item_Code, string BatchNumber, int Qty,decimal rate, decimal sales_rate, string Unit2,string purch_unit2, string UnitMF, string PurchNumber, string PrdDate, string ExpDate, string Period, string Sup_Code, string PurchDate, string IsExpDate, string Entry_No, string product_type)
        {
            int a = db.execute("update tbl_BatchNumber set  Item_Code='" + Item_Code + "',BatchNumber='" + BatchNumber + "',Qty='" + Qty + "',batch_rate='" + rate + "',batch_sales_rate='" + sales_rate + "',Unit2='" + Unit2 + "',purch_unit2='" + purch_unit2 + "', UnitMF='" + UnitMF + "',PurchNumber='" + PurchNumber + "',PrdDate='" + PrdDate + "',ExpDate='" + ExpDate + "',Period='" + Period + "',Sup_Code='" + Sup_Code + "',PurchDate='" + PurchDate + "',IsExpDate='" + IsExpDate + "',Product_type='" + product_type + "' where PurchNumber='" + PurchNumber + "' and  Entry_No	='" + Entry_No + "'");
            return a;
        }

        //public int update_batchNumber(string Item_Code, string BatchNumber, int Qty, string Unit2, string UnitMF, string PurchNumber, string PrdDate, string ExpDate, string Period, string Sup_Code, string PurchDate, string IsExpDate,string entryno, string product_type)
        //{
        //    int a = db.execute("update  tbl_BatchNumber set Item_Code='" + Item_Code + "',BatchNumber='" + BatchNumber + "',Qty='" + Qty + "',Unit2='" + Unit2 + "',UnitMF='" + UnitMF + "',PurchNumber='" + PurchNumber + "',PrdDate='" + PrdDate + "',ExpDate='" + ExpDate + "',Period='" + Period + "',Sup_Code='" + Sup_Code + "',PurchDate='" + PurchDate + "',IsExpDate='" + IsExpDate + "',Product_type='" + product_type + "' where Entry_No='" + entryno + "'");
        //    return a;
        //}
        public void save_purchaseit(string PurchNo, string Purc_Date, string Item_Code, string Desccription, string barcode, string Packing, string discount, string Unit, string Qty, int FreeQty, string Rate, string Amount, string UNIT2, string UnitMF, decimal GST, decimal IGST, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("insert into tbl_PURCHIT (PurchNumber,PurchDate,Item_Code,Desccription,Barcode,Packing,Discount,Unit,Qty,FreeQty,Rate,Amount,UNIT2,UnitMF,RetQty,GST,IGST) values ('" + PurchNo + "','" + Purc_Date + "','" + Item_Code + "','" + Desccription + "','" + barcode + "','" + Packing + "','" + discount + "','" + Unit + "','" + Qty + "','" + FreeQty + "','" + Rate + "','" + Amount + "','" + UNIT2 + "','" + UnitMF + "','0','" + GST + "','" + IGST + "')", con, trans);
        }
        //public void save_purchaseit(string PurchNo, string Purc_Date, string Item_Code, string Desccription, string barcode, string Packing, string Unit, string Qty, int FreeQty, string Rate, string Amount, string UNIT2, string UnitMF, decimal GST, decimal IGST)
        //{
        //    db.execute("insert into tbl_PURCHIT (PurchNumber,PurchDate,Item_Code,Desccription,Barcode,Packing,Unit,Qty,FreeQty,Rate,Amount,UNIT2,UnitMF,RetQty,GST,IGST) values ('" + PurchNo + "','" + Purc_Date + "','" + Item_Code + "','" + Desccription + "','" + barcode + "','" + Packing + "','" + Unit + "','" + Qty + "','" + FreeQty + "','" + Rate + "','" + Amount + "','" + UNIT2 + "','" + UnitMF + "','0','" + GST + "','" + IGST + "')");
        //}

        public void update_purchaseit(string PurchNo, string Purc_Date, string Item_Code, string Desccription, string barcode, string Packing, string discount, string Unit, string Qty, int FreeQty, string Rate, string Amount, string UNIT2, string UnitMF, decimal GST, decimal IGST, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("update tbl_PURCHIT set  PurchNumber='" + PurchNo + "',PurchDate='" + Purc_Date + "',Item_Code='" + Item_Code + "',Desccription ='" + Desccription + "',Barcode='" + barcode + "',Packing='" + Packing + "',Discount='" + discount + "',Unit='" + Unit + "',Qty='" + Qty + "',FreeQty='" + FreeQty + "',Rate='" + Rate + "',Amount='" + Amount + "',UNIT2='" + UNIT2 + "',UnitMF='" + UnitMF + "',RetQty='0',GST='" + GST + "',IGST='" + IGST + "' where PurchNumber='" + PurchNo + "' and Item_Code='" + Item_Code + "' ", con, trans);//and Unit='" + Unit + "'
        }
        public DataTable get_maxEntryNo(MySqlConnection con, MySqlTransaction trans)
        {
            DataTable batch_entry = db.trans_table("select max( Entry_No) from tbl_BatchNumber order by Entry_No desc",con,trans);
            return batch_entry;
        }
        public DataTable get_maxEntryNo()
        {
            DataTable batch_entry = db.table("select max( Entry_No) from tbl_BatchNumber order by Entry_No desc");
            return batch_entry;
        }
        public void save_batchpurchase(string PurchNo, string Purc_Date, string Sup_Code, string Item_Code, string BatchNumber, decimal Qty,decimal rate, string Unit2, string UnitMF, string PrdDate, string ExpDate, string IsExpDate, string BatchEntry,MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("insert into tbl_BatchPurchase (PurchNumber,PurchDate,Sup_Code,Item_Code,BatchNumber,Qty,batch_rate,Unit2,UnitMF,PrdDate,ExpDate,IsExpDate,BatchEntry,RetQty) values ('" + PurchNo + "','" + Purc_Date + "','" + Sup_Code + "','" + Item_Code + "','" + BatchNumber + "','" + Qty + "','" + rate + "','" + Unit2 + "','" + UnitMF + "','" + PrdDate + "','" + ExpDate + "','" + IsExpDate + "','" + BatchEntry + "','0')",con, trans);
        }
        public void save_batchpurchase(string PurchNo, string Purc_Date, string Sup_Code, string Item_Code, string BatchNumber, decimal Qty, string Unit2, string UnitMF, string PrdDate, string ExpDate, string IsExpDate, string BatchEntry)
        {
            db.execute("insert into tbl_BatchPurchase (PurchNumber,PurchDate,Sup_Code,Item_Code,BatchNumber,Qty,Unit2,UnitMF,PrdDate,ExpDate,IsExpDate,BatchEntry,RetQty) values ('" + PurchNo + "','" + Purc_Date + "','" + Sup_Code + "','" + Item_Code + "','" + BatchNumber + "','" + Qty + "','" + Unit2 + "','" + UnitMF + "','" + PrdDate + "','" + ExpDate + "','" + IsExpDate + "','" + BatchEntry + "','0')");
        }
        public void update_batchpurchase(string PurchNo, string Purc_Date, string Sup_Code, string Item_Code, string BatchNumber, decimal Qty,decimal rate, string Unit2, string UnitMF, string PrdDate, string ExpDate, string IsExpDate, string BatchEntry, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("update tbl_BatchPurchase set PurchNumber='" + PurchNo + "',PurchDate='" + Purc_Date + "',Sup_Code='" + Sup_Code + "',Item_Code='" + Item_Code + "',BatchNumber='" + BatchNumber + "',Qty='" + Qty + "',batch_rate='" + rate + "',Unit2='" + Unit2 + "',UnitMF='" + UnitMF + "',PrdDate='" + PrdDate + "',ExpDate='" + ExpDate + "',IsExpDate='" + IsExpDate + "',BatchEntry='" + BatchEntry + "',RetQty='0' where PurchNumber='" + PurchNo + "' and Item_Code='" + Item_Code + "' and BatchEntry='" + BatchEntry + "'", con, trans);
        }
        public void update_batchpurchase(string PurchNo, string Purc_Date, string Sup_Code, string Item_Code, string BatchNumber, decimal Qty, string Unit2, string UnitMF, string PrdDate, string ExpDate, string IsExpDate, string BatchEntry)
        {
            db.execute("update tbl_BatchPurchase set PurchNumber='" + PurchNo + "',PurchDate='" + Purc_Date + "',Sup_Code='" + Sup_Code + "',Item_Code='" + Item_Code + "',BatchNumber='" + BatchNumber + "',Qty='" + Qty + "',Unit2='" + Unit2 + "',UnitMF='" + UnitMF + "',PrdDate='" + PrdDate + "',ExpDate='" + ExpDate + "',IsExpDate='" + IsExpDate + "',BatchEntry='" + BatchEntry + "',RetQty='0' where PurchNumber='" + PurchNo + "' and Item_Code='" + Item_Code + "' and BatchEntry='" + BatchEntry + "'");
        }
        public DataTable get_itemdetails(string Itemid, MySqlConnection con, MySqlTransaction trans)
        {
            DataTable dtb_cost = db.trans_table("select * from tbl_ITEMS where id='" + Itemid + "'",con,trans);
            return dtb_cost;
        }

        public void update_itemtable(decimal unitcost, decimal Sales1_, decimal SalesMin_, decimal SalesMax_, decimal costbase1, decimal purchaserate2, decimal Sales2_, decimal SalesMin1_, decimal SalesMax1_, string Item_Id, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("update tbl_ITEMS set Purch_Rate='" + unitcost + "', Sales_Rate='" + Sales1_ + "',Sales_Rate_min='" + SalesMin_ + "',Sales_Rate_Max='" + SalesMax_ + "',CostBase='" + costbase1 + "',Purch_Rate2='" + purchaserate2 + "',Sales_Rate2='" + Sales2_ + "',Sales_Rate_min2='" + SalesMin1_ + "',Sales_Rate_Max2='" + SalesMax1_ + "' where id='" + Item_Id + "'",con,trans);
        }
        public void update_purchaseorder(int Pur_order_no1, MySqlConnection con, MySqlTransaction trans)
        {
            db.trans_execute("Update tbl_Purchase_order_master set status='P' where Pur_order_no= '" + Pur_order_no1 + "'",con,trans);
        }
        public int save_log(string log_userid, string log_type, string log_descriptn, string log_stage)
        {
            int j = db.execute("insert into tbl_log(log_user_id,log_type,log_description,log_stage)values('" + log_userid + "','" + log_type + "','" + log_descriptn + "','" + log_stage + "')");
            return j;
        }
        public DataTable trans_incrementDocnumber(MySqlConnection con, MySqlTransaction trans)
        {
            DataTable Document_Number = db.trans_table("SELECT max(cast(PurchNumber as UNSIGNED))AS 'Doc_Number' FROM tbl_PURCHASE", con, trans);
            return Document_Number;
        }
        public DataTable incrementDocnumber()
        {
            DataTable Document_Number = db.table("SELECT max(cast(PurchNumber as UNSIGNED))AS 'Doc_Number' FROM tbl_PURCHASE where memo='Purchase'");
            return Document_Number;
        }
        public DataTable load_purchase_order_details(int Pur_order_no1)
        {  
            DataTable dt = db.table("select M.Suppleir_id,S.Supplier_Name,P.Item_code as id,P.Description,P.Qty,P.UnitCost,P.Amount,i.Item_code, i.barcode, i.packing, i.gstvat from tbl_PurchaseOrder P inner join tbl_Purchase_order_master M on M.Pur_order_no=P.Pur_order_no inner join tbl_Supplier S on S.Supplier_Code=M.Suppleir_id left join tbl_items i on i.id=p.Item_code where P.Pur_order_no='" + Pur_order_no1 + "' and M.status='O' group by id");
            return dt;
        }
        public DataTable get_batchpurchase(int invnum_Edit)
        {
            //DataTable dtb_BatchSale = db.table("select  p.PurchNumber,p.PurchDate,b.Item_Code,b.BatchNumber,b.Qty,BatchEntry,b.PrdDate,b.ExpDate,b.Unit2,b.Period,p.rate,p.Unit from tbl_Batchpurchase s inner join tbl_batchnumber b on b.BatchNumber=s.BatchNumber inner join tbl_purchit p on p.Item_Code=b.Item_Code where b.PurchNumber='" + invnum_Edit + "' GROUP BY p.Unit");//(select Unit from tbl_purchit p where p.PurchNumber= s.PurchNumber and p.Item_Code=s.Item_Code ) Unit
            DataTable dtb_BatchSale = db.table("SELECT  p.PurchNumber,p.PurchDate,p.Item_Code,p.Qty ,p.rate,p.Unit,UNIT2 FROM tbl_purchit p WHERE PurchNumber='" + invnum_Edit + "'");
            return dtb_BatchSale;
        } 
        public DataTable get_batch_sales_rate(string batch, string item)
        {
            DataTable dtb = db.table("select batch_sales_rate from tbl_batchnumber p where p.BatchNumber= '" + batch + "' and p.Item_Code='" + item + "' ");
            return dtb;
        }
        public DataTable get_batch(int invnum_Edit,string itemcode)//,string unit2
        {
            //DataTable dtb_BatchSale = ("SELECT BatchNumber,Item_Code,BatchEntry,PrdDate,ExpDate,Unit2 FROM tbl_batchpurchase  WHERE PurchNumber ='" + invnum_Edit + "'  and Item_Code ='" + itemcode + "' and Unit2='" + unit2 + "' );
            DataTable dtb_BatchSale = db.table("SELECT  BatchNumber,Item_Code,BatchEntry,PrdDate,ExpDate,Unit2,batch_rate FROM tbl_batchpurchase WHERE PurchNumber='" + invnum_Edit + "' and  Item_Code ='" + itemcode + "'  ");//and  Unit2 = '" + unit2 + "'
            //DataTable dtb_BatchSale = db.table("select b.BatchNumber,b.Item_Code,b.BatchEntry,b.PrdDate,b.ExpDate,b.Unit2,b.batch_rate,b.qty,p.unit FROM tbl_batchpurchase b  inner join tbl_purchit p on p.PurchNumber= b.PurchNumber WHERE PurchNumber='" + invnum_Edit + "' and  Item_Code ='" + itemcode + "'  ");
            return dtb_BatchSale;
        }
        public DataTable get_purchase_unit(int invnum_Edit, string item_code, string qty)
        {
            DataTable dtb = db.table("select Unit from tbl_purchit p where p.PurchNumber= '" + invnum_Edit + "' and p.Item_Code='" + item_code + "' and Qty='" + qty + "'");
            return dtb;
        }

        public string get_suppliercode(string sup_id)
        {
            string supplier = db.scalar("select Supplier_Name from tbl_Supplier where Supplier_Code='" + sup_id + "'");
            return supplier;
        }
        //purchase list 
        public DataTable getPurchase_btwndates(string fromdate, string todate, string type,string purtype)//ffytfytfyt
        {
            DataTable dt = db.table("select p.PurchNumber,p.PurchDate,p.Sup_Code,s.Supplier_Name,p.GrandTotal,p.PurchType,p.Amount_Status from tbl_PURCHASE as p inner join tbl_Supplier as s on p.Sup_Code = s.Supplier_Code  where  PurchDate between '" + fromdate + "' and '" + todate + "' and p.memo='Purchase' and p.Product_type='" + type + "' and PurchType='" + purtype + "'"); //,v.voucherno,v.Partial_amount  inner join tbl_voucher v on v.purchno =p.PurchNumber
            return dt;
        }
        public DataTable get_due_from_voucher(string purno)
        {
            DataTable dt = db.table("select voucherno,purchno,Partial_amount from tbl_voucher where purchno='" + purno + "' and Partial_amount>0 " +
                ""); 
            return dt;
        }
        public DataTable getPurchase_btwndates(string fromdate, string todate, string type, string sup,string purtype)
        {
            DataTable dt = db.table("select p.PurchNumber,p.PurchDate,p.Sup_Code,s.Supplier_Name,p.GrandTotal,p.PurchType,p.Amount_Status from tbl_PURCHASE as p inner join tbl_Supplier as s on p.Sup_Code = s.Supplier_Code  where  PurchDate between '" + fromdate + "' and '" + todate + "' and Sup_Code='"+sup+"' and p.memo='Purchase' and p.Product_type='" + type + "' and PurchType='" + purtype + "'");
            return dt;
        }
        public DataTable data_from_Pur_Master(object dgv_Purchase)
        {
            DataTable data_from_Pur_Master = db.table("select PurchNumber,PurchDate,Sup_Code,InvNumber,TotalAmount,DiscPercent,DiscAmount,GrandTotal,PurchType,TotalCost,Product_type from tbl_PURCHASE where PurchNumber = '" + dgv_Purchase + "'");
            return data_from_Pur_Master;
        }
        public DataTable data_from_purchase(object dgv_Purchase)
        {
            DataTable data_from_purchase = db.table("select p.PurchDate,p.Item_Code as id,s.Item_Code,p.Desccription,p.Barcode,p.Packing,p.Unit,p.Qty,p.FreeQty,p.Rate,p.Amount,p.GST,p.IGST,p.Discount from tbl_PURCHIT p inner join tbl_ITEMS s on s.id= p.Item_Code where PurchNumber='" + dgv_Purchase + "'");
            return data_from_purchase;
        }
        public DataTable dt(string fromdate, string todate)
        {
            DataTable dt = db.table("select p.PurchNumber,p.PurchDate,p.Sup_Code,s.Supplier_Name,p.GrandTotal,p.PurchType,s.Address1 from tbl_PURCHASE as p inner join tbl_Supplier as s on p.Sup_Code = s.Supplier_Code  where  PurchDate between '" + fromdate + "' and '" + todate + "'");
            return dt;
        }
        public DataTable purchase_batch_data(string itemcode, string purno)
        {
            DataTable data_from_purchase = db.table("select Item_Code,BatchNumber,Qty from tbl_batchnumber where Item_Code='" + itemcode + "' and PurchNumber='" + purno + "'");
            return data_from_purchase;
        }
        public DataTable supname()
        {
            DataTable dt = db.table("select Supplier_Code,Supplier_Name from tbl_supplier");
            return dt;
        }

        //supplier receipt

        public DataTable increment_Receipt()
        {
            DataTable Document_Number = db.table("SELECT max(cast(voucherno as UNSIGNED))AS 'Doc_Number' FROM tbl_voucher ");
            return Document_Number;
        }
        public DataTable get_openingbalance(string sup_code)
        {
            DataTable dtb = db.table("select Opeinig_Balance,advance,Supplier_Name,Current_Balance from tbl_supplier where Supplier_Code='" + sup_code + "'");
            return dtb;
        }
        public void update_advance(string sup_code,string advance)
        {
            db.execute("update tbl_supplier set advance ='"+ advance + "' where  Supplier_Code='" + sup_code + "'");
        }
        public void update_due(string sup_code, string advance)
        {
            db.execute("update tbl_supplier set Current_Balance ='" + advance + "' where  Supplier_Code='" + sup_code + "'");
        }
        public void update_due(string sup_code, string advance, MySqlConnection con, MySqlTransaction tran)
        {
            db.trans_execute("update tbl_supplier set Current_Balance ='" + advance + "' where  Supplier_Code='" + sup_code + "'", con, tran);
        }
        public void update_advance(string sup_code, string advance, MySqlConnection con, MySqlTransaction tran)
        {
            db.trans_execute("update tbl_supplier set advance ='" + advance + "' where  Supplier_Code='" + sup_code + "'",con,tran);
        }
        public void save_voucher(string voucherno, string purchno, string date, string supplierid, string amount, string paymethod, string advance, string opening_balance, string Amount_paid, string Due, string Applied_to,decimal Partial_amount, MySqlConnection con, MySqlTransaction tran)
        {
            db.trans_execute("insert into tbl_voucher (voucherno,purchno,date,supplierid,amount,paymethod,advance,opening_balance,Amount_paid,Due,Applied_to,Partial_amount) values('" + voucherno + "','" + purchno + "','" + date + "','" + supplierid + "','" + amount + "','" + paymethod + "','" + advance + "','" + opening_balance + "','" + Amount_paid + "','" + Due + "','" + Applied_to + "','" + Partial_amount + "' )", con, tran);
        }
        public void save_voucher_cheque(string voucherno, string purchno, string date, string supplierid, string amount, string paymethod, string advance, string opening_balance, string Amount_paid, string Due, string Applied_to,string bank,string num, decimal Partial_amount, MySqlConnection con, MySqlTransaction tran)
        {
            db.trans_execute("insert into tbl_voucher (voucherno,purchno,date,supplierid,amount,paymethod,advance,opening_balance,Amount_paid,Due,Applied_to,Bank,Number,Partial_amount) values('" + voucherno + "','" + purchno + "','" + date + "','" + supplierid + "','" + amount + "','" + paymethod + "','" + advance + "','" + opening_balance + "','" + Amount_paid + "','" + Due + "','" + Applied_to + "','" + bank + "','" + num + "','" + Partial_amount + "')", con,tran);
        } 
        public void save_voucher_card(string voucherno, string purchno, string date, string supplierid, string amount, string paymethod, string advance, string opening_balance, string Amount_paid, string Due, string Applied_to, string card, string digitnum, decimal Partial_amount, MySqlConnection con, MySqlTransaction tran)
        {
            db.trans_execute("insert into tbl_voucher (voucherno,purchno,date,supplierid,amount,paymethod,advance,opening_balance,Amount_paid,Due,Applied_to,cardnumber,fourdigitnumber,Partial_amount) values('" + voucherno + "','" + purchno + "','" + date + "','" + supplierid + "','" + amount + "','" + paymethod + "','" + advance + "','" + opening_balance + "','" + Amount_paid + "','" + Due + "','" + Applied_to + "','" + card + "','" + digitnum + "','" + Partial_amount + "')", con, tran);
        }
        public void save_voucher_dd(string voucherno, string purchno, string date, string supplierid, string amount, string paymethod, string advance, string opening_balance, string Amount_paid, string Due, string Applied_to, string bank, string ddnum, decimal Partial_amount, MySqlConnection con, MySqlTransaction tran)
        {
            db.trans_execute("insert into tbl_voucher (voucherno,purchno,date,supplierid,amount,paymethod,advance,opening_balance,Amount_paid,Due,Applied_to,Bank,DDnumber,Partial_amount) values('" + voucherno + "','" + purchno + "','" + date + "','" + supplierid + "','" + amount + "','" + paymethod + "','" + advance + "','" + opening_balance + "','" + Amount_paid + "','" + Due + "','" + Applied_to + "','" + bank + "','" + ddnum + "','" + Partial_amount + "')", con, tran);
        }
        public void save_voucher(string voucherno, string purchno, string date, string supplierid, string amount, string paymethod, string advance, string opening_balance, string Amount_paid, string Due, string Applied_to,decimal Partial_amount)
        {
            db.execute("insert into tbl_voucher (voucherno,purchno,date,supplierid,amount,paymethod,advance,opening_balance,Amount_paid,Due,Applied_to,Partial_amount) values('" + voucherno + "','" + purchno + "','" + date + "','" + supplierid + "','" + amount + "','" + paymethod + "','" + advance + "','" + opening_balance + "','" + Amount_paid + "','" + Due + "','" + Applied_to + "','" + Partial_amount + "')");
        }
        public void save_voucher_cheque(string voucherno, string purchno, string date, string supplierid, string amount, string paymethod, string advance, string opening_balance, string Amount_paid, string Due, string Applied_to, string bank, string num, decimal Partial_amount)
        {
            db.execute("insert into tbl_voucher (voucherno,purchno,date,supplierid,amount,paymethod,advance,opening_balance,Amount_paid,Due,Applied_to,Bank,Number,Partial_amount) values('" + voucherno + "','" + purchno + "','" + date + "','" + supplierid + "','" + amount + "','" + paymethod + "','" + advance + "','" + opening_balance + "','" + Amount_paid + "','" + Due + "','" + Applied_to + "','" + bank + "','" + num + "','" + Partial_amount + "')");
        }
        public void save_voucher_card(string voucherno, string purchno, string date, string supplierid, string amount, string paymethod, string advance, string opening_balance, string Amount_paid, string Due, string Applied_to, string card, string digitnum, decimal Partial_amount)
        {
            db.execute("insert into tbl_voucher (voucherno,purchno,date,supplierid,amount,paymethod,advance,opening_balance,Amount_paid,Due,Applied_to,cardnumber,fourdigitnumber,Partial_amount) values('" + voucherno + "','" + purchno + "','" + date + "','" + supplierid + "','" + amount + "','" + paymethod + "','" + advance + "','" + opening_balance + "','" + Amount_paid + "','" + Due + "','" + Applied_to + "','" + card + "','" + digitnum + "','" + Partial_amount + "')");
        }
        public void save_voucher_dd(string voucherno, string purchno, string date, string supplierid, string amount, string paymethod, string advance, string opening_balance, string Amount_paid, string Due, string Applied_to, string bank, string ddnum, decimal Partial_amount)
        {
            db.execute("insert into tbl_voucher (voucherno,purchno,date,supplierid,amount,paymethod,advance,opening_balance,Amount_paid,Due,Applied_to,Bank,DDnumber,Partial_amount) values('" + voucherno + "','" + purchno + "','" + date + "','" + supplierid + "','" + amount + "','" + paymethod + "','" + advance + "','" + opening_balance + "','" + Amount_paid + "','" + Due + "','" + Applied_to + "','" + bank + "','" + ddnum + "','" + Partial_amount + "')");
        }
        public void update_purchtype(string purno)
        {
            db.execute("update tbl_PURCHASE set PurchType='Cash' where PurchNumber= '" + purno + "'");
        }
        public void update_purch_Amout_status(string purno,string status)
        {
            db.execute("update tbl_PURCHASE set Amount_Status= '" + status + "' where PurchNumber= '" + purno + "'");
        }
        public void update_purch_Amout_status(string purno, string status, MySqlConnection con, MySqlTransaction tran)
        {
            db.trans_execute("update tbl_PURCHASE set Amount_Status= '" + status + "' where PurchNumber= '" + purno + "'", con, tran);
        }
        public DataTable dtload(string date1 ,string date2,string supplier)
        {
            DataTable dtb = new DataTable();
            if(supplier == "All Supplier")
            {
                dtb = db.table("select * from tbl_voucher where date between'" + date1 + "'  and'" + date2 + "' order by  supplierid");
            }
            else
            {
              dtb = db.table("select * from tbl_voucher where date between'" + date1 + "'  and'" + date2 + "' and supplierid='" + supplier + "'");
            }
            return dtb;
        }
        public DataTable all_sup_due(string date1, string date2)
        {
            DataTable dt = new DataTable();
            DataTable dtb = db.table("select max(date) from tbl_voucher where date between'" + date1 + "'  and'" + date2 + "' ");
            if(dtb.Rows.Count>0)
            {
                dt= db.table("select distinct supplierid,max(due) from tbl_voucher where date ='" + dtb.Rows[0][0].ToString() + "' ");
            }
            return dtb;
        }
        public DataTable return_details(string purno)
        {
            DataTable dt = db.table("select TotalCost,TotalAmount,RetNumber,ReturnDate from  tbl_preturn where PurchNumber='" + purno + "'");
            return dt;
        }
        public DataTable get_voucher_details(string purno)
        {
            DataTable dtb = db.table("select *,max(voucherno) voucherno  from tbl_voucher where purchno ='" + purno + "'");
            return dtb;
        }
    }
}
