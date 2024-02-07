using PappyjoeMVC.Controller;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
namespace PappyjoeMVC.View
{
    public partial class ItemListForSales : Form
    {
        public static string Form_Name = "";
        public static string Item_Code = ""; public static string Item_Name = "";
       public string item_code = ""; string Stock = ""; string ItemID = "";public string item_Name = "";
        ItemListForSales_controller cntrl=new ItemListForSales_controller();
        string type = "";
        public ItemListForSales()
        {
            InitializeComponent();
        }

        public ItemListForSales(string formName)
        {
            InitializeComponent();
            Form_Name = formName;
            Item_Code = "";
        }

        public ItemListForSales(string formName, string text)
        {
            InitializeComponent();
            Form_Name = formName;
            Item_Code = text;
        }
       
        private void ItemListForSales_Load(object sender, EventArgs e)
        {
            try
            {
               
                btn_ok.Select();
                DataTable dt_consume = this.cntrl.Get_consume_tick();
                if (dt_consume.Rows.Count > 0)
                {
                    if (dt_consume.Rows[0]["consumables"].ToString() == "Yes")
                    {
                        type = "Consumable";
                    }
                    else
                        type = "Pharmacy";
                }
                else
                    type = "Pharmacy";

                if (Form_Name == "Sales")
                {
                    if (Item_Code != "")
                    {
                        string itemid = this.cntrl.get_itemid(Item_Code, type);
                        DataTable dtb = this.cntrl.each_item_details(itemid, type);// Load_items_wit_itemcode(itemid);
                        fill_grid_itemcode(dtb, itemid);// fill_Grid(dtb);
                    }
                    else if(item_Name != "")
                    {
                        DataTable itemid = this.cntrl.get_itemName(item_Name,type);
                        DataTable dtb = this.cntrl.each_item_details(itemid.Rows[0][0].ToString(), type);// Load_items_wit_itemcode(itemid);
                        fill_grid_itemcode(dtb, itemid.Rows[0][0].ToString());
                        //dgv_item.RowCount = 0;
                        //if(itemid.Rows.Count>0)
                        //{
                        //    for (int i = 0; i < itemid.Rows.Count; i++)
                        //    {
                        //        DataTable dtb1 = this.cntrl.Load_items_wit_itemcode(itemid.Rows[i][0].ToString());
                        //        if (dtb1.Rows.Count > 0)
                        //        {
                        //            Lab_Msg.Visible = false;
                        //            //for (int i = 0; i < dtb1.Rows.Count; i++)
                        //            //{
                        //                dgv_item.Rows.Add();
                        //                dgv_item.Rows[i].Cells["id"].Value = dtb1.Rows[0]["id"].ToString();
                        //                dgv_item.Rows[i].Cells["ColItemCode"].Value = dtb1.Rows[0]["item_code"].ToString();
                        //                dgv_item.Rows[i].Cells["colItemName"].Value = dtb1.Rows[0]["item_name"].ToString();
                        //                dgv_item.Rows[i].Cells["colCategory"].Value = dtb1.Rows[0]["Name"].ToString();
                        //                dgv_item.Rows[i].Cells["colStock"].Value = Convert.ToDecimal(dtb1.Rows[0]["Current_Stock"].ToString()).ToString("##");
                        //                dgv_item.Rows[i].Cells["ColPrize"].Value = Convert.ToDecimal(dtb1.Rows[0]["Cost (Unit1)"].ToString()).ToString("#0.000");
                        //                dgv_item.Rows[i].Cells["colCost2"].Value = Convert.ToDecimal(dtb1.Rows[0]["Cost (Unit2)"].ToString()).ToString("#0.000");
                        //            //}
                        //        }

                        //    }
                        //}
                        //else
                        //{
                        //    Lab_Msg.Visible = true;
                        //    Lab_Msg.Location = new Point(194, 282);
                        //    dgv_item.RowCount = 0;
                        //}
                        
                        //DataTable dtb = this.cntrl.Load_items_wit_itemcode(itemid);
                        //fill_Grid(dtb);
                    }
                    else 
                    {
                      DataTable dt_load=this.cntrl.Load_items(type);
                      fill_Grid(dt_load);
                    }
                }
                if (Form_Name == "Sales Order")
                {
                    if (Item_Code != "")
                    {
                        string itemid = this.cntrl.get_itemid(Item_Code, type);
                        DataTable dtb = this.cntrl.each_item_details(itemid, type);// Load_items_wit_itemcode(itemid);
                        fill_grid_itemcode(dtb, itemid);
                        //DataTable dtb = this.cntrl.Load_items_wit_itemcode(itemid);
                        //fill_Grid(dtb);
                    }
                    else
                    {
                     DataTable dt_load=this.cntrl.Load_items(type);
                        fill_Grid(dt_load);
                    }
                }
                dgv_item.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
                dgv_item.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgv_item.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 9, FontStyle.Regular);
                dgv_item.EnableHeadersVisualStyles = false;
                foreach (DataGridViewColumn cl in dgv_item.Columns)
                {
                    cl.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //public void fill_Grid(DataTable dtb)
        //{
        //    dgv_item.RowCount = 0;
        //    if (dtb.Rows.Count > 0)
        //    {
        //        Lab_Msg.Visible = false;
        //        for (int i = 0; i < dtb.Rows.Count; i++)
        //        {
        //            dgv_item.Rows.Add();
        //            dgv_item.Rows[i].Cells["id"].Value = dtb.Rows[i]["id"].ToString();
        //            dgv_item.Rows[i].Cells["ColItemCode"].Value = dtb.Rows[i]["item_code"].ToString();
        //            dgv_item.Rows[i].Cells["colItemName"].Value = dtb.Rows[i]["item_name"].ToString();
        //            dgv_item.Rows[i].Cells["colCategory"].Value = dtb.Rows[i]["Name"].ToString();
        //            dgv_item.Rows[i].Cells["colStock"].Value = Convert.ToDecimal(dtb.Rows[i]["Current_Stock"].ToString()).ToString("##");
        //            dgv_item.Rows[i].Cells["ColPrize"].Value = Convert.ToDecimal(dtb.Rows[i]["Cost (Unit1)"].ToString()).ToString("#0.000");
        //            dgv_item.Rows[i].Cells["colCost2"].Value = Convert.ToDecimal(dtb.Rows[i]["Cost (Unit2)"].ToString()).ToString("#0.000");
        //        }
        //    }
        //    else
        //    {
        //        Lab_Msg.Visible = true;
        //        Lab_Msg.Location = new Point(194, 282);
        //        dgv_item.RowCount = 0;
        //    }
        //}
        public void fill_grid_itemcode(DataTable dtb,string id)
        {
            dgv_item.RowCount = 0;
            if (dtb.Rows.Count > 0)
            {
                //int i = 0;
                Lab_Msg.Visible = false;
                //foreach (DataRow dr in dtb.Rows)
                //{
                    
                DataTable dt_item = this.cntrl.Load_items_qty(id);
                if (dt_item.Rows.Count > 0)
                {
                    dgv_item.Rows.Add();
                    DataTable dt_cat = this.cntrl.dt_cat(dtb.Rows[0]["Cat_Number"].ToString());
                    dgv_item.Rows[0].Cells["id"].Value = dtb.Rows[0]["id"].ToString();
                    dgv_item.Rows[0].Cells["ColItemCode"].Value = dtb.Rows[0]["item_code"].ToString();
                    dgv_item.Rows[0].Cells["colItemName"].Value = dtb.Rows[0]["item_name"].ToString();
                    dgv_item.Rows[0].Cells["colCategory"].Value = dt_cat.Rows[0]["Name"].ToString();
                    dgv_item.Rows[0].Cells["colStock"].Value = Convert.ToDecimal(dt_item.Rows[0]["Current_Stock"].ToString()).ToString("##");
                    dgv_item.Rows[0].Cells["ColPrize"].Value = Convert.ToDecimal(dtb.Rows[0]["Cost (Unit1)"].ToString()).ToString("#0.000");
                    dgv_item.Rows[0].Cells["colCost2"].Value = Convert.ToDecimal(dtb.Rows[0]["Cost (Unit2)"].ToString()).ToString("#0.000");
                    //i++;
                }


                //}

            }
            else
            {
                Lab_Msg.Visible = true;
                Lab_Msg.Location = new Point(194, 282);
                dgv_item.RowCount = 0;
            }
        }
        public void fill_grid_itemcode_search(DataTable dtb, string id)
        {
            dgv_item.RowCount = 0;
            if (dtb.Rows.Count > 0)
            {
                int i = 0;
                Lab_Msg.Visible = false;
                foreach (DataRow dr in dtb.Rows)
                {

                    DataTable dt_item = this.cntrl.Load_items_qty(dr["id"].ToString());
                    if (dt_item.Rows.Count > 0)
                    {
                        dgv_item.Rows.Add();
                        DataTable dt_cat = this.cntrl.dt_cat(dr["Cat_Number"].ToString());
                        dgv_item.Rows[i].Cells["id"].Value = dr["id"].ToString();
                        dgv_item.Rows[i].Cells["ColItemCode"].Value = dr["item_code"].ToString();
                        dgv_item.Rows[i].Cells["colItemName"].Value = dr["item_name"].ToString();
                        if(dt_cat.Rows.Count>0)
                        {
                            dgv_item.Rows[i].Cells["colCategory"].Value = dt_cat.Rows[0]["Name"].ToString();
                        }
                        else
                        dgv_item.Rows[i].Cells["colCategory"].Value = "";
                        dgv_item.Rows[i].Cells["colStock"].Value = Convert.ToDecimal(dt_item.Rows[0]["Current_Stock"].ToString()).ToString("##");
                        dgv_item.Rows[i].Cells["ColPrize"].Value = Convert.ToDecimal(dr["Cost (Unit1)"].ToString()).ToString("#0.000");
                        dgv_item.Rows[i].Cells["colCost2"].Value = Convert.ToDecimal(dr["Cost (Unit2)"].ToString()).ToString("#0.000");
                        i++;
                    }


                }

            }
            else
            {
                Lab_Msg.Visible = true;
                Lab_Msg.Location = new Point(194, 282);
                dgv_item.RowCount = 0;
            }
        }
        public void fill_Grid(DataTable dtb)
        {
            dgv_item.RowCount = 0;
            if (dtb.Rows.Count > 0)
            {
                int i = 0;
                Lab_Msg.Visible = false;
                foreach(DataRow dr in dtb.Rows)
                {
                    dgv_item.Rows.Add();
                    DataTable dt_item = this.cntrl.each_item_details(dr["item_code"].ToString(),type);
                    if(dt_item.Rows.Count>0)
                    {
                        DataTable dt_cat = this.cntrl.dt_cat(dt_item.Rows[0]["Cat_Number"].ToString());
                        dgv_item.Rows[i].Cells["id"].Value = dr["item_code"].ToString();
                        dgv_item.Rows[i].Cells["ColItemCode"].Value = dt_item.Rows[0]["item_code"].ToString();
                        dgv_item.Rows[i].Cells["colItemName"].Value = dt_item.Rows[0]["item_name"].ToString();
                        if(dt_cat.Rows.Count>0)
                        {
                            dgv_item.Rows[i].Cells["colCategory"].Value = dt_cat.Rows[0]["Name"].ToString();
                        }
                        else
                        {
                            dgv_item.Rows[i].Cells["colCategory"].Value ="";
                        }
                     
                        dgv_item.Rows[i].Cells["colStock"].Value = Convert.ToDecimal(dr["Current_Stock"].ToString()).ToString("##");
                        dgv_item.Rows[i].Cells["ColPrize"].Value = Convert.ToDecimal(dt_item.Rows[0]["Cost (Unit1)"].ToString()).ToString("#0.000");
                        dgv_item.Rows[i].Cells["colCost2"].Value = Convert.ToDecimal(dt_item.Rows[0]["Cost (Unit2)"].ToString()).ToString("#0.000");
                        i++;
                    }
                   

                }
               
            }
            else
            {
                Lab_Msg.Visible = true;
                Lab_Msg.Location = new Point(194, 282);
                dgv_item.RowCount = 0;
            }
        }
        private void txt_ItemCode_Click(object sender, EventArgs e)
        {
            txt_ItemCode.Text = "";
        }

        private void txt_ItemCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_ItemCode.Text != "")
                {
                  DataTable dtb= this.cntrl.search_wit_itemcode(txt_ItemCode.Text);
                    if(dtb.Rows.Count>0)
                    {
                        string id = dtb.Rows[0]["id"].ToString();
                        fill_grid_itemcode_search(dtb, id);
                        //fill_Grid(dtb);
                    }
                    else
                    {
                        dgv_item.Rows.Clear();
                    }
                   
                }
                else
                {
                    Lab_Msg.Visible = false;
                   DataTable dt_load=this.cntrl.Load_items(type);
                    fill_Grid(dt_load);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_item_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_item.Rows.Count > 0)
                {
                    if (Form_Name == "Sales")
                    {
                        ItemID= dgv_item.CurrentRow.Cells["id"].Value.ToString();
                        item_code = dgv_item.CurrentRow.Cells["ColItemCode"].Value.ToString();
                        item_Name = dgv_item.CurrentRow.Cells["colItemName"].Value.ToString();
                        Stock = dgv_item.CurrentRow.Cells["colStock"].Value.ToString();
                    }
                    if (Form_Name == "Sales Order")
                    {
                        ItemID= dgv_item.CurrentRow.Cells["id"].Value.ToString();
                        item_code = dgv_item.CurrentRow.Cells["ColItemCode"].Value.ToString();
                        item_Name = dgv_item.CurrentRow.Cells["colItemName"].Value.ToString();
                    }
                }
                btn_ok.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            try
            {
                if (item_code != "")
                {
                    if (Form_Name == "Sales")
                    {
                        DataTable dt_min = this.cntrl.dt_minimum_stock(ItemID);
                        if (dt_min.Rows.Count > 0)
                        {
                            if (Convert.ToDecimal(Stock) > Convert.ToDecimal(dt_min.Rows[0]["MinimumStock"].ToString()))
                            {
                                var form2 = new Sales(item_code, item_Name, Stock, ItemID);
                                //form2.Closed += (sender1, args) => this.Close();
                                //Item_Code = "";
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Can't sell, the quantity is equall to minimum quantity", "Can't sell", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }

                    }
                    else if (Form_Name == "Sales Order")
                    {
                        var form2 = new SalesOrder(item_code, item_Name, Form_Name, ItemID);
                        //form2.Closed += (sender1, args) => this.Close();
                        this.Close();
                    }
                }
                else
                    dgv_item.Rows.Clear();
                    this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Batches_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_item.Rows.Count > 0)
                {
                    int rowindex = dgv_item.CurrentRow.Index;
                    if (rowindex >= 0)
                    {
                        string itemcode = dgv_item.CurrentRow.Cells["id"].Value.ToString();
                        string isbatch = this.cntrl.check_batch(itemcode);
                        if (isbatch == "true")
                        {
                            var form2 = new ItemBatchDetails(itemcode);
                            form2.ShowDialog();
                            form2.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("This item does not have batch...", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_item_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (item_code != "")
                {
                    if (Form_Name == "Sales")
                    {
                        DataTable dt_min = this.cntrl.dt_minimum_stock(ItemID);
                        if (dt_min.Rows.Count > 0)
                        {
                            if (Convert.ToDecimal(Stock) > Convert.ToDecimal(dt_min.Rows[0]["MinimumStock"].ToString()))
                            {
                                var form2 = new Sales(item_code, item_Name, Stock, ItemID);
                                //form2.Closed += (sender1, args) => this.Close();
                                //Item_Code = "";
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Can't sell, the quantity is equall to minimum quantity", "Can't sell", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        //var form2 = new Sales(item_code, item_Name, Stock, ItemID);
                        ////form2.Closed += (sender1, args) => this.Close();
                        //this.Close();
                    }
                    else if (Form_Name == "Sales Order")
                    {
                        var form2 = new SalesOrder(item_code, item_Name, Form_Name, ItemID);
                        form2.Closed += (sender1, args) => this.Close();
                        this.Close();
                    }
                }
                else
                    this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
 