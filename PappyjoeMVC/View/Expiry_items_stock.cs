using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PappyjoeMVC.Model;
using System.Windows.Forms;

namespace PappyjoeMVC.View
{
    public partial class Expiry_items_stock : Form
    {
        StockItem_model cntrl = new StockItem_model();
        public Expiry_items_stock()
        {
            InitializeComponent();
        }

        private void Expiry_items_stock_Load(object sender, EventArgs e)
        {
            DGV_Stock.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            DGV_Stock.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DGV_Stock.EnableHeadersVisualStyles = false;
            DGV_Stock.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 9, FontStyle.Regular);
            DataTable dtitems = this.cntrl.exp_prd();
            if(dtitems.Rows.Count>0)
            {
                int i = 1;
                DGV_Stock.Rows.Clear();
                foreach(DataRow dr in dtitems.Rows)
                {
                    DateTime exp = Convert.ToDateTime(dr["ExpDate"].ToString());
                    DateTime todate = DateTime.Now.Date;
                    if(exp < todate)
                    {
                        DataTable get_itemnae= this.cntrl.get_itemname(dr["item_code"].ToString());
                        if(get_itemnae.Rows.Count>0)
                        {
                            DGV_Stock.Rows.Add(dr["item_code"].ToString(), i, get_itemnae.Rows[0]["item_code"].ToString(), get_itemnae.Rows[0]["item_name"].ToString(), dr["BatchNumber"].ToString(), dr["Qty"].ToString(), dr["ExpDate"].ToString(), dr["ExpDate"].ToString());
                        }
                      
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
