using PappyjoeMVC.Controller;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
namespace PappyjoeMVC.View
{
    public partial class PurchaseItemLIst : Form
    {
        public static string item_;
        public static string item_code,item_id;
        string PurchaseOrder1;public string type = "";
        purchaseItem_controller cntrl=new purchaseItem_controller();
        public PurchaseItemLIst()
        {
            InitializeComponent();
        }
        public PurchaseItemLIst(string PurchaseOrder, string item)
        {
            InitializeComponent();
            PurchaseOrder1 = PurchaseOrder;
            item_ = item;
        }
        public PurchaseItemLIst(string purchaseOrder)
        {
            InitializeComponent();
            PurchaseOrder1 = purchaseOrder;
        }
       
        private void frmPurchaseItemLIst_Load(object sender, EventArgs e)
        {
            DataTable dt_consume_check = this.cntrl.Get_consume_tick();
            if (dt_consume_check.Rows.Count > 0)
            {
                if (dt_consume_check.Rows[0]["consumables"].ToString() == "Yes")
                {
                    type = "Consumable";
                }
                else
                    type = "Pharmacy";
            }
            else
                type = "Pharmacy";
            if (item_ == "")
            {
               DataTable dtb= this.cntrl.LoadItems(type);
                Fill_Grid(dtb);
            }
            else
            {
                string id=this.cntrl.get_itemid(item_,type);
                DataTable dtb = this.cntrl.Load_itemcode_details(id, type);
                Fill_Grid(dtb);
                item_ = null;
            }
            dgvItemList.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            dgvItemList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvItemList.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 9, FontStyle.Regular);
            dgvItemList.EnableHeadersVisualStyles = false;
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dgvItemList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvItemList.Rows.Count > 0)
            {
                item_code = dgvItemList.CurrentRow.Cells["ItemCode"].Value.ToString();
                item_id= dgvItemList.CurrentRow.Cells["id"].Value.ToString();
                DataTable dt = new DataTable();
                if (item_code != null)
                {
                    if (PurchaseOrder1 == "Purchase_order")
                    {
                        var form2 = new PurchaseOrder(item_code, item_id);
                        form2.Closed += (sender1, args) => this.Close();
                        this.Hide();
                    }
                    else
                    {
                        var form1 = new Purchase(item_code, item_id);
                        form1.Closed += (sender1, args) => this.Close();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (dgvItemList.Rows.Count > 0)
            {
                item_code = dgvItemList.CurrentRow.Cells["ItemCode"].Value.ToString();
                item_id = dgvItemList.CurrentRow.Cells["id"].Value.ToString();
                DataTable dt = new DataTable();
                if (item_code != null)
                {
                    if (PurchaseOrder1 == "Purchase_order")
                    {
                        var form2 = new PurchaseOrder(item_code, item_id);
                        form2.Closed += (sender1, args) => this.Close();
                        this.Hide();
                    }
                    else
                    {
                        var form1 = new Purchase(item_code, item_id);
                        form1.Closed += (sender1, args) => this.Close();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            if (txt_Search.Text != "")
            {
                DataTable dtb = this.cntrl.Search(txt_Search.Text, type);
                Fill_Grid(dtb);
            }
            else
            {
                Lab_Msg.Visible = false;
                DataTable dtb=this.cntrl.LoadItems(type);
                Fill_Grid(dtb);
            }
        }
        //public void Fill_Grid(DataTable dt)
        //{
        //    if (dt.Rows.Count > 0)
        //    {
        //        dgvItemList.Rows.Clear();
        //        string stock;
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            if (dt.Rows[i]["Current_Stock"].ToString() != "")
        //            {
        //                stock = dt.Rows[i]["Current_Stock"].ToString();
        //            }
        //            else
        //            {
        //                stock = "0";
        //            }
        //            dgvItemList.Rows.Add(dt.Rows[i]["id"].ToString(),dt.Rows[i]["item_code"].ToString(), dt.Rows[i]["item_name"].ToString(), dt.Rows[i]["manufacturer"].ToString(), stock, dt.Rows[i]["Cost(Unit1)"].ToString(), dt.Rows[i]["Cost(Unit2)"].ToString());
        //        }
        //    }
        //    else
        //    {
        //        Lab_Msg.Visible = true;
        //    }
        //}
        public void Fill_Grid(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                dgvItemList.Rows.Clear();
                string stock;
                foreach (DataRow dr in dt.Rows)
                {
                    DataTable dt_stock = this.cntrl.Load_items_qty(dr["id"].ToString());
                    DataTable dt_man = this.cntrl.Load_manufactr(dr["manufacturer"].ToString());
                    if(dt_stock.Rows.Count>0)
                    {
                        if (dt_stock.Rows[0]["Current_Stock"].ToString() != "")
                        {
                            stock = dt_stock.Rows[0]["Current_Stock"].ToString();
                        }
                        else
                        {
                            stock = "0";
                        }
                    }
                    else
                    {
                        stock = "0";
                    }
                   
                    dgvItemList.Rows.Add(dr["id"].ToString(), dr["item_code"].ToString(), dr["item_name"].ToString(), dt_man.Rows[0]["manufacturer"].ToString(), stock, dr["Cost(Unit1)"].ToString(), dr["Cost(Unit2)"].ToString());
                }
            }
            else
            {
                Lab_Msg.Visible = true;
            }
        }
        private void txt_Search_Click(object sender, EventArgs e)
        {
            if (txt_Search.Text != "")
            {
                txt_Search.Text = "";
            }
        }

        private void dgvItemList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvItemList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgvItemList.Rows.Count > 0)
                {
                    item_code = dgvItemList.CurrentRow.Cells["ItemCode"].Value.ToString();
                    item_id = dgvItemList.CurrentRow.Cells["id"].Value.ToString();
                    DataTable dt = new DataTable();
                    if (item_code != null)
                    {
                        if (PurchaseOrder1 == "Purchase_order")
                        {
                            var form2 = new PurchaseOrder(item_code, item_id);
                            form2.Closed += (sender1, args) => this.Close();
                            this.Hide();
                        }
                        else
                        {
                            var form1 = new Purchase(item_code, item_id);
                            form1.Closed += (sender1, args) => this.Close();
                            this.Hide();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select an item", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
