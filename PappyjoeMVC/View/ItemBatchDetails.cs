using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PappyjoeMVC.Model;
namespace PappyjoeMVC.View
{
    public partial class ItemBatchDetails : Form
    {
        public static string ItemCode;
        Inventory_model _model = new Inventory_model();
        public ItemBatchDetails()
        {
            InitializeComponent();
        }

        public ItemBatchDetails(string itemcode)
        {
            InitializeComponent();
            ItemCode = itemcode;
        }

        private void FrmItemBatchDetails_Load(object sender, EventArgs e)
        {
            try
            {
                Lab_Msg.Visible = false; DataTable dt_salesrate = new DataTable();
                decimal percnt_amt = 0, total_cost = 0, value1 = 0, value2 = 0, percentage = 0, unitMf = 0, cost = 0;
                if (ItemCode != "")
                {
                    DataTable dtb = this._model.Load_batch(ItemCode);
                    if (dtb.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtb.Rows)
                        {
                            // batch rate calculations
                            //DataTable dtb_item = this._model.itemdetails(ItemCode);
                            //DataTable dt_rate = this._model.dt_batch_wise_rate(dr["PurchNumber"].ToString(), ItemCode);
                            //if (dt_rate.Rows.Count > 0)
                            //{
                            //    cost = Convert.ToDecimal(dt_rate.Rows[0]["rate"].ToString());
                            //}
                            //else
                            //{
                            //    //cost
                            //}
                            //if (dtb_item.Rows.Count > 0)
                            //{
                            //    //unitMf = Convert.ToDecimal(dtb_item.Rows[0]["UnitMF"].ToString());
                            //    //if (Unit == dtb_item.Rows[0]["Unit2"].ToString())
                            //    //{
                            //    //    {
                            //            //dt_salesrate = this._model.get_item_salesrate_minimun(ItemCode);
                            //    //    }
                            //    //}
                            //    //else
                            //    //{
                            //    dt_salesrate = this._model.get_item_salesrate(ItemCode);
                            //    //    //}
                            //    //}
                            //}
                            //value1 = Convert.ToDecimal(dt_salesrate.Rows[0][0].ToString()) - Convert.ToDecimal(dt_salesrate.Rows[0][1].ToString());
                            //value2 = (value1 / Convert.ToDecimal(dt_salesrate.Rows[0][1].ToString())) * 100;
                            //percentage = Convert.ToDecimal(value2.ToString("##.00"));

                            ////if (Unit == dtb_item.Rows[0]["Unit1"].ToString())
                            ////{
                            //    percnt_amt = (Convert.ToDecimal(dr["rate"].ToString()) * percentage) / 100;
                            //    total_cost = percnt_amt + Convert.ToDecimal(dr["rate"].ToString());
                            //cost =Convert.ToDecimal( total_cost.ToString("##.00"));
                            //}
                            //else //if(cmb_Unit.Text == dtb.Rows[0]["Unit"].ToString())
                            //{
                            //    if (Unit == dtb_item.Rows[0]["Unit2"].ToString())
                            //    {
                            //        cost = Convert.ToDecimal(dtb.Rows[i]["rate"].ToString()) / unitMf;
                            //        //txt_UnitCost.Text = percnt_amt.ToString("##.00");

                            //        //percnt_amt = (Convert.ToDecimal(dtb.Rows[0]["rate"].ToString()) * percentage) / 100;// percnt_amt = (Convert.ToDecimal(dt_salesrate.Rows[0][1].ToString()) * percentage) / 100;
                            //        percnt_amt = (Convert.ToDecimal(cost) * percentage) / 100;
                            //        total_cost = cost + percnt_amt;// percnt_amt + Convert.ToDecimal(dt_salesrate.Rows[0][1].ToString());
                            //        dgv_batchSale.Rows[i].Cells["rate"].Value = total_cost.ToString("##.00");
                            //    }
                            //    else
                            //    {
                            //        percnt_amt = (Convert.ToDecimal(dt_salesrate.Rows[0][1].ToString()) * percentage) / 100;
                            //        //percnt_amt = (Convert.ToDecimal(cost) * percentage) / 100;
                            //        total_cost = percnt_amt + Convert.ToDecimal(dt_salesrate.Rows[0][1].ToString());
                            //        dgv_batchSale.Rows[i].Cells["rate"].Value = total_cost.ToString("##.00");
                            //    }
                            //}
                            ////
                            ///
                           // batch_sales_rate
                            dgv_batch.Rows.Add(dr["BatchNumber"].ToString(), dr["Qty"].ToString(), dr["batch_sales_rate"].ToString(), Convert.ToDateTime(dr["ExpDate"].ToString()).ToString("MM/dd/yyyy"), Convert.ToDateTime(dr["PrdDate"].ToString()).ToString("MM/dd/yyyy"), dr["PurchNumber"].ToString(), Convert.ToDateTime(dr["PurchDate"].ToString()).ToString("MM/dd/yyyy"), dr["Sup_Code"].ToString());
                        }
                    }
                    else
                        Lab_Msg.Visible = true;
                }
                dgv_batch.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
                dgv_batch.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgv_batch.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 9, FontStyle.Regular);
                dgv_batch.EnableHeadersVisualStyles = false;
                foreach (DataGridViewColumn cl in dgv_batch.Columns)
                {
                    cl.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
