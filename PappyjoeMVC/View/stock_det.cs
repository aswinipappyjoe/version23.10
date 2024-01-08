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


namespace PappyjoeMVC.View
{
    public partial class stock_det : Form
    {

        Reports_controller cntrl = new Reports_controller();
        
        public int bno;
        public int stock;
        public string id;
      
        public stock_det()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void stock_det_Load(object sender, EventArgs e)
        {
           
            string expdate = "";
            string IsExpDate = "";
            DataTable dtb = this.cntrl.get_batch_stock(id);
            if (dtb.Rows.Count > 0)
            {
                int i = 0;
                int j = 1;
              
                foreach (DataRow dr in dtb.Rows)
                {
                    dgv_stockdetials.Rows.Add();
                    dgv_stockdetials.Rows[i].Cells["sno"].Value = j;
                    dgv_stockdetials.Rows[i].Cells["c_batch"].Value = dtb.Rows[i]["BatchNumber"].ToString();
                    dgv_stockdetials.Rows[i].Cells["c_stock"].Value = dtb.Rows[i]["Qty"].ToString();
                    dgv_stockdetials.Rows[i].Cells["rate"].Value = dtb.Rows[i]["batch_sales_rate"].ToString();
                    if (dtb.Rows[i]["ExpDate"].ToString() != "" && dtb.Rows[i]["ExpDate"].ToString() != null)
                    {
                        IsExpDate = "Yes";
                        expdate = Convert.ToDateTime(dtb.Rows[i]["ExpDate"].ToString()).ToString("dd-MM-yyyy");

                       dgv_stockdetials.Rows[i].Cells["c_exp"].Value = expdate;
                    }
                    else
                    {
                        IsExpDate = "NO";
                         expdate = "";
                    }

                    i =i+1;
                   
                    j++;
                }


            }
        }
    }
}
