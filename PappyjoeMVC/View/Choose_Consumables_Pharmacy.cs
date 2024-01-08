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
    public partial class Choose_Consumables_Pharmacy : Form
    {
        Item_List_Controller cntrl = new Item_List_Controller();
        public Choose_Consumables_Pharmacy()
        {
            InitializeComponent();
        }

        private void Choose_Consumables_Pharmacy_Load(object sender, EventArgs e)
        {
            DataTable dt_value = this.cntrl.get_consume_data();
            if (dt_value.Rows.Count > 0)
            {
                if(dt_value.Rows[0]["consumables"].ToString()=="Yes")
                {
                    chk_consume.Checked = true;
                }
                else
                {
                    chk_consume.Checked = false;
                }
            }
            else
                chk_consume.Checked = false;
        }

        private void chk_consume_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            string consume = "";
            try
            {
                if (chk_consume.Checked == true)
                {
                    consume = "Yes";
                }
                else
                    consume = "No";
                DataTable dt_value = this.cntrl.get_consume_data();
                if (dt_value.Rows.Count > 0)
                {
                    this.cntrl.update_consume(consume);
                    MessageBox.Show("Successfully Saved !!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    this.cntrl.save_consume(consume);
                    MessageBox.Show("Successfully Saved !!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            catch (Exception ex)
            {

            }
           

        }
    }
}
