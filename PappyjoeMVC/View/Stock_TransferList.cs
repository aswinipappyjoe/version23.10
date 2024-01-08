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
    public partial class Stock_TransferList : Form
    {
        stock_transfer_controller cntrl = new stock_transfer_controller();
        public Stock_TransferList()
        {
            InitializeComponent();
        }

        private void Stock_TransferList_Load(object sender, EventArgs e)
        {
            try
            {
                Lab_Msg.Visible = false;
                DataTable dt = new DataTable();
                if (cmb_action.Text=="Stock Out")
                {
                   dt = this.cntrl.get_stockout_details(Convert.ToDateTime(DTP_From.Value).ToString("yyyy-MM-dd"), Convert.ToDateTime(DTP_To.Value).ToString("yyyy-MM-dd"));
                }
                else if(cmb_action.Text=="Stock In")
                {
                    dt = this.cntrl.get_stockin_details(Convert.ToDateTime(DTP_From.Value).ToString("yyyy-MM-dd"), Convert.ToDateTime(DTP_To.Value).ToString("yyyy-MM-dd"));
                }
                 Fill_dgvSale(dt);
                dgv_sales.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
                dgv_sales.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgv_sales.EnableHeadersVisualStyles = false;
                dgv_sales.ColumnHeadersDefaultCellStyle.Font = new Font("Sego UI", 9, FontStyle.Regular);
                dgv_sales.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv_sales.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv_sales.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv_sales.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv_sales.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv_sales.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                foreach (DataGridViewColumn cl in dgv_sales.Columns)
                {
                    cl.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                PappyjoeMVC.Model.Connection.MyGlobals.Date_From = DTP_From.Value.ToString("yyyy-MM-dd");
                PappyjoeMVC.Model.Connection.MyGlobals.Date_To = DTP_To.Value.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Fill_dgvSale(DataTable dtb)
        {
            dgv_sales.Rows.Clear(); Lab_Msg.Visible = false;
            if (dtb.Rows.Count > 0)
            {
                int num = 1;
                for (int i = 0; i < dtb.Rows.Count; i++)// RefNo,stock_date,LabName,Action,TotalAmount
                {
                    dgv_sales.Rows.Add();
                    dgv_sales.Rows[i].Cells["slno"].Value = num;
                    dgv_sales.Rows[i].Cells["id"].Value = dtb.Rows[i]["id"].ToString();
                    dgv_sales.Rows[i].Cells["RefNo"].Value = dtb.Rows[i]["RefNo"].ToString();
                    dgv_sales.Rows[i].Cells["date"].Value = Convert.ToDateTime(dtb.Rows[i]["stock_date"].ToString()).ToString("MM/dd/yyyy");
                    dgv_sales.Rows[i].Cells["labname"].Value = dtb.Rows[i]["Name"].ToString();
                    dgv_sales.Rows[i].Cells["cost"].Value = Convert.ToDecimal(dtb.Rows[i]["TotalAmount"].ToString()).ToString("0.00");
                    num = num + 1;
                }
            }
            else
            {
                int x = (panel2.Size.Width - Lab_Msg.Size.Width) / 2;
                Lab_Msg.Location = new Point(x, Lab_Msg.Location.Y);
                Lab_Msg.Visible = true;
            }
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (cmb_action.Text=="Stock Out")
            {
                dt = this.cntrl.get_stockout_details(Convert.ToDateTime(DTP_From.Value).ToString("yyyy-MM-dd"), Convert.ToDateTime(DTP_To.Value).ToString("yyyy-MM-dd"));
            }
            else if (cmb_action.Text == "Stock In")
            {
                dt = this.cntrl.get_stockin_details(Convert.ToDateTime(DTP_From.Value).ToString("yyyy-MM-dd"), Convert.ToDateTime(DTP_To.Value).ToString("yyyy-MM-dd"));
            }
            Fill_dgvSale(dt);
        }

        private void dgv_sales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                PappyjoeMVC.Model.Connection.MyGlobals.global_Flag = true;
                int invnum = Convert.ToInt32(dgv_sales.CurrentRow.Cells["id"].Value.ToString());
                int rowindex = dgv_sales.CurrentRow.Index;
                if (dgv_sales.CurrentCell.OwningColumn.Name == "colmore")
                {
                    if (cmb_action.Text == "Stock Out")
                    {
                        var form2 = new stock_transfer();
                        form2.stock_out_id = invnum;
                        form2.Action = "Stock Out";
                        form2.ShowDialog();
                        form2.Dispose();
                    }
                    else if (cmb_action.Text == "Stock In")
                    {
                        var form2 = new stock_transfer();
                        form2.stock_out_id = invnum;
                        form2.Action = "Stock In";
                        form2.ShowDialog();
                        form2.Dispose();

                    }
                }
            }
        }

        private void cmb_action_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (cmb_action.Text == "Stock Out")
            {
                dt = this.cntrl.get_stockout_details(Convert.ToDateTime(DTP_From.Value).ToString("yyyy-MM-dd"), Convert.ToDateTime(DTP_To.Value).ToString("yyyy-MM-dd"));
            }
            else if (cmb_action.Text == "Stock In")
            {
                dt = this.cntrl.get_stockin_details(Convert.ToDateTime(DTP_From.Value).ToString("yyyy-MM-dd"), Convert.ToDateTime(DTP_To.Value).ToString("yyyy-MM-dd"));
            }
            Fill_dgvSale(dt);
        }
    }
}
