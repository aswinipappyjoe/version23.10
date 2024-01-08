using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PappyjoeMVC.Controller;
using System.Data;

namespace PappyjoeMVC.View
{
    public partial class Stocktransfer_itemlist : Form
    {
        stock_transfer_controller cntrl = new stock_transfer_controller();
        public string formname = "",Action="",type="";
        public Stocktransfer_itemlist()
        {
            InitializeComponent();
        }

        private void Stocktransfer_itemlist_Load(object sender, EventArgs e)
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
            DataTable dt_items = new DataTable();
            if (formname=="Stock Transfer")
            {
                if (Action == "Stock Out")
                {
                    dt_items = this.cntrl.get_itemdata_nonzero_stock(type);
                   fill_Grid_stockout(dt_items);
                }
                else if (Action == "Stock In")
                {
                    dt_items = this.cntrl.get_itemdata(type);
                    fill_Grid(dt_items);
                }
            }
            else
            {
                dt_items = this.cntrl.get_itemdata(type);
                fill_Grid(dt_items);
            }
            dgv_item_details.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            dgv_item_details.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv_item_details.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 9, FontStyle.Regular);
            dgv_item_details.EnableHeadersVisualStyles = false;
           
        }
        
       
        private void Btn_Add_Click(object sender, EventArgs e)
        {
            if(dgv_item_details.Rows.Count>=0)
            {
                DataTable dtb_items = new DataTable();
                foreach (DataGridViewColumn col in dgv_item_details.Columns)
                {
                    dtb_items.Columns.Add(col.Name);
                }
                foreach (DataGridViewRow row in dgv_item_details.Rows)
                {
                    if (row.Selected == true)
                    {
                        DataRow dRow = dtb_items.NewRow();
                        foreach (DataGridViewCell cell in row.Cells)
                        {

                            dRow[cell.ColumnIndex] = cell.Value;
                        }
                        dtb_items.Rows.Add(dRow);
                    }
                        
                }
                if (dtb_items.Rows.Count > 0)
                {
                    if(formname == "Stock Transfer")
                    {
                        var form2 = new stock_transfer(dtb_items);
                        form2.Closed += (sender1, args) => this.Close();
                        this.Close();
                    }
                    else
                    {
                       
                    }
                   
                }
            }
        }

        private void txt_ItemCode_Click(object sender, EventArgs e)
        {
            txt_ItemCode.Text = "";
        }

        private void txt_ItemCode_TextChanged(object sender, EventArgs e)
        {
            try//
            {
                if (txt_ItemCode.Text != "")
                {
                    DataTable dtb = new DataTable();
                    if (formname=="Stock Transfer")
                    {
                        if(Action== "Stock Out")
                        {
                            dtb = this.cntrl.search_wit_itemcode_stock_out(txt_ItemCode.Text,type);
                            fill_Grid_stockout(dtb);
                        }
                        else
                        {
                            dtb = this.cntrl.search_wit_itemcode(txt_ItemCode.Text,type);
                            fill_Grid(dtb);
                        }
                    }
                    else
                    {
                        dtb = this.cntrl.search_wit_itemcode(txt_ItemCode.Text,type);
                        fill_Grid(dtb);
                    }
                  
                   
                }
                else
                {
                    Lab_Msg.Visible = false;
                    DataTable dt_load = new DataTable();
                    if (formname == "Stock Transfer")
                    {
                        if (Action == "Stock Out")
                        {
                            dt_load = this.cntrl.get_itemdata_nonzero_stock(type);
                           fill_Grid_stockout(dt_load);
                        }
                        else
                        {
                            dt_load = this.cntrl.get_itemdata(type); fill_Grid(dt_load);
                        }
                    }
                    else
                    {
                        dt_load = this.cntrl.get_itemdata(type); fill_Grid(dt_load);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void fill_Grid(DataTable dtb)
        {
            dgv_item_details.Rows.Clear();
            if (dtb.Rows.Count > 0)
            {
                int k = 1;
                Lab_Msg.Visible = false;
                for (int i = 0; i < dtb.Rows.Count; i++)
                {
                    dgv_item_details.Rows.Add();
                    dgv_item_details.Rows[i].Cells["id"].Value = dtb.Rows[i]["id"].ToString();
                    dgv_item_details.Rows[i].Cells["slno"].Value = k;
                    dgv_item_details.Rows[i].Cells["itemcode"].Value = dtb.Rows[i]["item_code"].ToString();
                    dgv_item_details.Rows[i].Cells["name"].Value = dtb.Rows[i]["item_name"].ToString();
                    if (dtb.Rows[i]["stock"].ToString() != "")
                    {
                        dgv_item_details.Rows[i].Cells["stock"].Value = Convert.ToDecimal(dtb.Rows[i]["stock"].ToString()).ToString("#0");
                    }
                    else
                        dgv_item_details.Rows[i].Cells["stock"].Value = 0;


                    k++;
                }
            }
            else
            {
                Lab_Msg.Visible = true;
                Lab_Msg.Location = new Point(194, 282);
                dgv_item_details.RowCount = 0;
            }
        }

        public void fill_Grid_stockout(DataTable dtb)
        {
            dgv_item_details.Rows.Clear();
            if (dtb.Rows.Count > 0)
            {
                int k = 1;
                Lab_Msg.Visible = false;
                for (int i = 0; i < dtb.Rows.Count; i++)
                {
                    dgv_item_details.Rows.Add();
                    dgv_item_details.Rows[i].Cells["id"].Value = dtb.Rows[i]["id"].ToString();
                    dgv_item_details.Rows[i].Cells["slno"].Value = k;
                    dgv_item_details.Rows[i].Cells["itemcode"].Value = dtb.Rows[i]["itemCode"].ToString();
                    dgv_item_details.Rows[i].Cells["name"].Value = dtb.Rows[i]["itemname"].ToString();
                    if (dtb.Rows[i]["stock"].ToString() != "")
                    {
                        dgv_item_details.Rows[i].Cells["stock"].Value = Convert.ToDecimal(dtb.Rows[i]["stock"].ToString()).ToString("#0");
                    }
                    else
                        dgv_item_details.Rows[i].Cells["stock"].Value = 0;


                    k++;
                }
            }
            else
            {
                Lab_Msg.Visible = true;
                Lab_Msg.Location = new Point(194, 282);
                dgv_item_details.RowCount = 0;
            }
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
