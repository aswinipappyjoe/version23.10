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
using MySql.Data.MySqlClient;

namespace PappyjoeMVC.View
{
    public partial class stock_transfer : Form
    {
        public bool flag_comboload = false;
        stock_transfer_controller cntrl = new stock_transfer_controller();
        public static DataTable dtb_items_load;
        public static DataTable dtb_batch_load;
        public static string itemId = "";
        public string Action="";
        public decimal TotalStock;
        public int rowindex;
        public Decimal TotalAmount = 0;
        public int stock_out_id = 0;
        public bool batch_flag=false; public static int batch_entry = 0;
        public stock_transfer()
        {
            InitializeComponent();
        }

        public stock_transfer(DataTable dtb_items)
        {
            InitializeComponent();
            dtb_items_load = dtb_items;
        }

        public static decimal total_batch_rate = 0;
        public stock_transfer(DataTable dtb_items, string str) : this(dtb_items)
        {
            InitializeComponent();
            dtb_batch_load = dtb_items;
            total_batch_rate = total_rate;
        }

        public stock_transfer(DataTable dtb_items, decimal total_rate, int v) : this(dtb_items)
        {
            InitializeComponent();
            dtb_items_load = dtb_items;
            total_batch_rate = total_rate;
        }

        public stock_transfer(DataTable dtb_items, string str, decimal total_rate, int v) : this(dtb_items, str)
        {
            InitializeComponent();
            dtb_batch_load = dtb_items;
            totalbatchrate = total_rate;
            batch_entry = v;
        }

        private void stock_transfer_Load(object sender, EventArgs e)
        {
            try
            {
                load_lab();
                cmb_action.SelectedIndex = 0;
                if (stock_out_id != 0)
                {
                    if (Action == "Stock Out")
                    {
                        lb_taken.Visible = false;
                        lb_givento.Visible = true;
                        disable_controlls(); decimal net = 0;
                        DataTable dt = this.cntrl.get_main_details(stock_out_id.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            txt_RefNo.Text = dt.Rows[0]["RefNo"].ToString();
                            dtpentryDate.Value = Convert.ToDateTime(dt.Rows[0]["stock_date"].ToString());
                            cmb_action.Text = dt.Rows[0]["Action"].ToString();
                            cmb_givento.SelectedValue = dt.Rows[0]["LabName"].ToString();
                            txt_netAmount.Text =Convert.ToDecimal( dt.Rows[0]["TotalAmount"].ToString()).ToString("#0.00");
                            DataTable dtb = this.cntrl.get_stock_details(stock_out_id.ToString());
                            DataTable dt_items = this.cntrl.get_itemcode_stock_out(stock_out_id.ToString()); dgvstockOut.Rows.Clear();
                            if (dt_items.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt_items.Rows.Count; i++)
                                {
                                    DataTable dt_gst = this.cntrl.get_pur_gst(dt_items.Rows[i]["ItemId"].ToString());
                                    decimal total = 0, Amount = 0, gst_Amount = 0, gst = 0;
                                    DataTable dt_itemcode = this.cntrl.get_itemcode_frm_items(dt_items.Rows[i]["ItemId"].ToString());
                                    dgvstockOut.Rows.Add();
                                    dgvstockOut.Rows[i].Cells["id"].Value = dt_items.Rows[i]["ItemId"].ToString();
                                    dgvstockOut.Rows[i].Cells["itemcode"].Value = dt_itemcode.Rows[0]["item_code"].ToString();
                                    dgvstockOut.Rows[i].Cells["description"].Value = dt_items.Rows[i]["ItemName"].ToString();
                                    dgvstockOut.Rows[i].Cells["unit"].Value = dt_items.Rows[i]["Unit"].ToString();
                                    dgvstockOut.Rows[i].Cells["gst"].Value = dt_gst.Rows[0]["GST"].ToString();
                                    dgvstockOut.Rows[i].Cells["Qty"].Value = dt_items.Rows[i]["qty"].ToString();
                                    dgvstockOut.Rows[i].Cells["unit_cost"].Value = Convert.ToDecimal(dt_items.Rows[i]["Cost"].ToString()).ToString("#0.00"); 

                                    if (dt_gst.Rows.Count > 0)
                                        gst = Convert.ToDecimal(dt_gst.Rows[0]["GST"].ToString());
                                    else
                                        gst = 0;
                                    if (gst > 0)
                                    {
                                        Amount = Convert.ToDecimal(dt_items.Rows[i]["qty"].ToString()) * Convert.ToDecimal(dt_items.Rows[i]["Cost"].ToString());
                                        gst_Amount = (Amount * gst) / 100;
                                        total = Amount + gst_Amount;
                                        dgvstockOut.Rows[i].Cells["Total"].Value = total.ToString("#0.00");
                                    }
                                    else
                                    {
                                        total = Convert.ToDecimal(dt_items.Rows[i]["qty"].ToString()) * Convert.ToDecimal(dt_items.Rows[i]["Cost"].ToString());
                                        dgvstockOut.Rows[i].Cells["Total"].Value = total.ToString("#0.00");
                                    }
                                    net = net + Convert.ToDecimal(dgvstockOut.Rows[i].Cells["Total"].Value);
                                    dgvstockOut.Rows[i].Cells["edit"].Value = PappyjoeMVC.Properties.Resources.editicon;
                                    dgvstockOut.Rows[i].Cells["Del"].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                                }
                                txt_netAmount.Text = net.ToString("#0.00");
                                txt_totalItems.Text = dgvstockOut.Rows.Count.ToString();
                            }
                            if (dtb.Rows.Count > 0)
                            {
                                for (int j = 0; j < dtb.Rows.Count; j++)
                                {
                                    dgv_BatchSale.Rows.Add();
                                    dgv_BatchSale.Rows[j].Cells["colBatchEntry"].Value = dtb.Rows[j]["BatchEntry"].ToString();
                                    dgv_BatchSale.Rows[j].Cells["colBatchnumber"].Value = dtb.Rows[j]["Batch"].ToString();
                                    dgv_BatchSale.Rows[j].Cells["coiltem_code"].Value = dtb.Rows[j]["ItemId"].ToString();
                                    dgv_BatchSale.Rows[j].Cells["itemname"].Value = dtb.Rows[j]["ItemName"].ToString();
                                    dgv_BatchSale.Rows[j].Cells["b_unit"].Value = dtb.Rows[j]["Unit"].ToString();
                                    dgv_BatchSale.Rows[j].Cells["givenqty"].Value = dtb.Rows[j]["Given_Qty"].ToString();
                                    dgv_BatchSale.Rows[j].Cells["cost"].Value = Convert.ToDecimal(dtb.Rows[j]["Cost"].ToString()).ToString("#0.00"); 
                            }
                            }
                        } 
                    }
                    else if (Action == "Stock In")
                    {
                        disable_controlls();
                        decimal net = 0;
                        lb_taken.Visible = true;
                        lb_givento.Visible = false;
                        DataTable dt = this.cntrl.get_stockIn_main_details(stock_out_id.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            txt_RefNo.Text = dt.Rows[0]["RefNo"].ToString();
                            dtpentryDate.Value = Convert.ToDateTime(dt.Rows[0]["stock_date"].ToString());
                            cmb_action.Text = dt.Rows[0]["Action"].ToString();
                            cmb_givento.SelectedValue = dt.Rows[0]["LabName"].ToString();
                            txt_netAmount.Text = dt.Rows[0]["TotalAmount"].ToString();
                            
                            DataTable dtb = this.cntrl.get_stockIn_details(stock_out_id.ToString());
                            DataTable dt_items = this.cntrl.get_itemcode_stock_in(stock_out_id.ToString());
                            if(dt_items.Rows.Count>0)
                            {
                                for (int i = 0; i < dt_items.Rows.Count; i++)
                                {
                                    DataTable dt_gst = this.cntrl.get_pur_gst(dt_items.Rows[i]["ItemId"].ToString());
                                    decimal total = 0, Amount = 0, gst_Amount = 0, gst = 0;
                                    DataTable dt_itemcode = this.cntrl.get_itemcode_frm_items(dt_items.Rows[i]["ItemId"].ToString());
                                    dgvstockOut.Rows.Add();
                                    dgvstockOut.Rows[i].Cells["id"].Value = dt_items.Rows[i]["ItemId"].ToString();
                                    dgvstockOut.Rows[i].Cells["itemcode"].Value = dt_itemcode.Rows[0]["item_code"].ToString();
                                    dgvstockOut.Rows[i].Cells["description"].Value = dt_items.Rows[i]["ItemName"].ToString();
                                    dgvstockOut.Rows[i].Cells["unit"].Value = dt_items.Rows[i]["Unit"].ToString();
                                    dgvstockOut.Rows[i].Cells["gst"].Value = dt_gst.Rows[0]["GST"].ToString();
                                    dgvstockOut.Rows[i].Cells["Qty"].Value = dt_items.Rows[i]["qty"].ToString();
                                    dgvstockOut.Rows[i].Cells["unit_cost"].Value = dt_items.Rows[i]["Cost"].ToString();
                                    if (dt_gst.Rows.Count > 0)
                                        if (dt_gst.Rows[0]["GST"].ToString()!="")
                                        {
                                            gst = Convert.ToDecimal(dt_gst.Rows[0]["GST"].ToString());
                                        }
                                    else
                                        gst = 0;
                                    if (gst > 0)
                                    {
                                        Amount = Convert.ToDecimal(dt_items.Rows[i]["qty"].ToString()) * Convert.ToDecimal(dt_items.Rows[i]["Cost"].ToString());
                                        gst_Amount = (Amount * gst) / 100;
                                        total = Amount + gst_Amount;
                                        dgvstockOut.Rows[i].Cells["Total"].Value = total.ToString("#0.00");
                                    }
                                    else
                                    {
                                        total = Convert.ToDecimal(dt_items.Rows[i]["qty"].ToString()) * Convert.ToDecimal(dt_items.Rows[i]["Cost"].ToString());
                                        dgvstockOut.Rows[i].Cells["Total"].Value = total.ToString("#0.00");
                                    }
                                    net = net + Convert.ToDecimal(dgvstockOut.Rows[i].Cells["Total"].Value);
                                    dgvstockOut.Rows[i].Cells["edit"].Value = PappyjoeMVC.Properties.Resources.editicon;
                                    dgvstockOut.Rows[i].Cells["Del"].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                                }
                                txt_netAmount.Text = net.ToString("#0.00");
                                txt_totalItems.Text = dgvstockOut.Rows.Count.ToString();
                            }
                            if (dtb.Rows.Count > 0)
                            {
                                for (int j = 0; j < dtb.Rows.Count; j++)
                                {
                                    dgv_BatchSale.Rows.Add();
                                    dgv_BatchSale.Rows[j].Cells["colBatchEntry"].Value = dtb.Rows[j]["BatchEntry"].ToString();
                                    dgv_BatchSale.Rows[j].Cells["colBatchnumber"].Value = dtb.Rows[j]["Batch"].ToString();
                                    dgv_BatchSale.Rows[j].Cells["coiltem_code"].Value = dtb.Rows[j]["ItemId"].ToString();
                                    dgv_BatchSale.Rows[j].Cells["itemname"].Value = dtb.Rows[j]["ItemName"].ToString();
                                    dgv_BatchSale.Rows[j].Cells["b_unit"].Value = dtb.Rows[j]["Unit"].ToString();
                                    dgv_BatchSale.Rows[j].Cells["givenqty"].Value = dtb.Rows[j]["buy_Qty"].ToString();
                                    dgv_BatchSale.Rows[j].Cells["cost"].Value = dtb.Rows[j]["Cost"].ToString();
                                }
                           }
                        }

                    }
                }
                else
                {
                    if (cmb_action.Text == "Stock Out")
                    {
                        lb_taken.Visible = false;
                        lb_givento.Visible = true;
                        Enable_controlls();
                        doc_num_incerment();
                    }
                    else if (cmb_action.Text == "Stock In")
                    {
                        lb_taken.Visible = true;
                        lb_givento.Visible = false;
                        Enable_controlls();
                        stockin_doc_num_incerment();
                    }
                }
                flag_comboload = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void disable_controlls()
        {
            dgvstockOut.Columns["edit"].Visible = false;
            dgvstockOut.Columns["Del"].Visible = false;
            txt_RefNo.Enabled = false;
            dtpentryDate.Enabled = false;
            cmb_action.Enabled = false;
            cmb_givento.Enabled = false;
            txt_netAmount.Enabled = false;
            txt_totalItems.Enabled = false;
            txt_Itemcode.Enabled = false;
            txtDescription.Enabled = false;
            txt_givenqty.Enabled = false;
            cmb_unit.Enabled = false;
            txtUnitCost.Enabled = false;
            txt_Amount.Enabled = false;
            btn_save.Enabled = false;
            Btn_Add.Enabled = false;
            button1.Enabled = false;
            Btn_itemselect.Enabled = false;
            BTN_CLEAR.Enabled = false;
        }
        public void Enable_controlls()
        {
            dgvstockOut.Columns["edit"].Visible = true;
            dgvstockOut.Columns["Del"].Visible = true;
            txt_RefNo.Enabled = true;
            dtpentryDate.Enabled = true;
            cmb_action.Enabled = true;
            cmb_givento.Enabled = true;
            txt_netAmount.Enabled = true;
            txt_totalItems.Enabled = true;
            txt_Itemcode.Enabled = true;
            txtDescription.Enabled = true;
            txt_givenqty.Enabled = true;
            cmb_unit.Enabled = true;
            txtUnitCost.Enabled = true;
            txt_Amount.Enabled = true;
            btn_save.Enabled = true;
            Btn_Add.Enabled = true;
            button1.Enabled = true;
            Btn_itemselect.Enabled = true; BTN_CLEAR.Enabled = true;
        }
        public void doc_num_incerment()
        {
            DataTable Document_Number = this.cntrl.docnumber();
            if (String.IsNullOrWhiteSpace(Document_Number.Rows[0][0].ToString()))
            {
                txt_RefNo.Text = "1";
            }
            else
            {
                int Count = Convert.ToInt32(Document_Number.Rows[0][0]);
                int incrValue = Convert.ToInt32(Count);
                incrValue += 1;
                txt_RefNo.Text = incrValue.ToString();
            }
        }
        public void stockin_doc_num_incerment()
        {
            DataTable Document_Number = this.cntrl.stockin_docnumber();
            if (String.IsNullOrWhiteSpace(Document_Number.Rows[0][0].ToString()))
            {
                txt_RefNo.Text = "1";
            }
            else
            {
                int Count = Convert.ToInt32(Document_Number.Rows[0][0]);
                int incrValue = Convert.ToInt32(Count);
                incrValue += 1;
                txt_RefNo.Text = incrValue.ToString();
            }
        }
        private void cmb_action_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (flag_comboload == true)
                {
                    if (cmb_action.SelectedIndex == 0)
                    {
                        lb_givento.Visible = true;
                        lb_taken.Visible = false; batch_flag = false;
                        doc_num_incerment();
                    }
                    else if (cmb_action.SelectedIndex == 1)
                    {
                        lb_taken.Visible = true;
                        lb_givento.Visible = false;
                        stockin_doc_num_incerment();
                    }
                    else if (cmb_action.SelectedIndex == 2)
                    {
                        lb_taken.Visible = false;
                        lb_givento.Visible = false;
                        lb_givrnqty.Visible = false;
                        txt_givenqty.Visible = false;
                        cmb_givento.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public int itemcheck_()
        {
            int affected = 0;
            for (int i = 0; i < dgvstockOut.Rows.Count; i++)
            {
                    if (dgvstockOut.Rows[i].Cells["itemcode"].Value != null && txt_Itemcode.Text == dgvstockOut.Rows[i].Cells["itemcode"].Value.ToString())
                    {
                        MessageBox.Show("The ItemCode already existed ", "Duplication Encountered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        affected = 1;
                    }
            }
            return affected;
        }
        private void Btn_itemselect_Click(object sender, EventArgs e)
        {
            try
            {
              
                dtb_items_load = null;
                var form2 = new Stocktransfer_itemlist();
                form2.formname = "Stock Transfer";
                if (cmb_action.Text == "Stock Out")
                    form2.Action = "Stock Out";
                else
                    form2.Action = "Stock In";
                form2.ShowDialog();
                form2.Dispose();
                if (dtb_items_load != null)
                {
                    DataTable dt = this.cntrl.dt_units(dtb_items_load.Rows[0]["id"].ToString());
                    itemId = dtb_items_load.Rows[0]["id"].ToString();
                    txt_Itemcode.Text = dtb_items_load.Rows[0]["itemcode"].ToString();
                    if (cmb_action.Text == "Stock In")
                    {
                        if (itemcheck_() == 0)
                        {
                            txtDescription.Text = dtb_items_load.Rows[0]["name"].ToString();
                            if (dt.Rows.Count > 0)
                            {
                                if (dt.Rows[0]["GstVat"].ToString() != "")
                                {
                                    txt_gst.Text = Convert.ToDecimal(dt.Rows[0]["GstVat"].ToString()).ToString();
                                }
                            }
                            if (cmb_action.Text == "Stock In")
                            {
                                DataTable dt_batch = this.cntrl.get_batchdetails(itemId);
                                if (dt_batch.Rows.Count > 0)
                                {
                                    txt_gst.Enabled = false;
                                    lb_gst.Enabled = false;
                                }
                                else
                                {
                                    txt_gst.Enabled = true;
                                    lb_gst.Enabled = true;
                                    batch_flag = true;
                                }
                            }
                            else
                            {
                                txt_gst.Enabled = false;
                                lb_gst.Enabled = false;
                            }
                            cmb_unit.Items.Clear();
                            if (dt.Rows.Count > 0)
                            {
                                txtUnitCost.Text = Convert.ToDecimal(dt.Rows[0]["Purch_Rate"].ToString()).ToString("0.00");
                                if (dt.Rows[0]["Unit1"].ToString() != "")
                                {
                                    cmb_unit.Items.Add(dt.Rows[0]["Unit1"].ToString());
                                }
                                if (dt.Rows[0]["Unit2"].ToString() != "null" && dt.Rows[0]["Unit2"].ToString() != "")
                                {
                                    cmb_unit.Items.Add(dt.Rows[0]["Unit2"].ToString());
                                }
                                if (cmb_unit.Items.Count > 0)//error correction
                                {
                                    cmb_unit.SelectedIndex = 0;
                                }
                            }
                            dtb_items_load = null;
                        }
                        else
                        {
                            txt_Itemcode.Text = "";
                            txt_Itemcode.Focus();
                        }
                    }
                    else
                    {
                        txtDescription.Text = dtb_items_load.Rows[0]["name"].ToString();
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["GstVat"].ToString() != "")
                            {
                                txt_gst.Text = Convert.ToDecimal(dt.Rows[0]["GstVat"].ToString()).ToString();
                            }
                        }
                        if (cmb_action.Text == "Stock In")
                        {
                            DataTable dt_batch = this.cntrl.get_batchdetails(itemId);
                            if (dt_batch.Rows.Count > 0)
                            {
                                txt_gst.Enabled = false;
                                lb_gst.Enabled = false;
                            }
                            else
                            {
                                txt_gst.Enabled = true;
                                lb_gst.Enabled = true;
                                batch_flag = true;
                            }
                        }
                        else
                        {
                            txt_gst.Enabled = false;
                            lb_gst.Enabled = false;
                        }
                        cmb_unit.Items.Clear();
                        if (dt.Rows.Count > 0)
                        {
                            txtUnitCost.Text = Convert.ToDecimal(dt.Rows[0]["Purch_Rate"].ToString()).ToString("0.00");
                            if (dt.Rows[0]["Unit1"].ToString() != "")
                            {
                                cmb_unit.Items.Add(dt.Rows[0]["Unit1"].ToString());
                            }
                            if (dt.Rows[0]["Unit2"].ToString() != "null" && dt.Rows[0]["Unit2"].ToString() != "")
                            {
                                cmb_unit.Items.Add(dt.Rows[0]["Unit2"].ToString());
                            }
                            if (cmb_unit.Items.Count > 0)//error correction
                            {
                                cmb_unit.SelectedIndex = 0;
                            }
                        }
                        dtb_items_load = null;

                    }
                        //if (itemcheck_() == 0)
                            //{
                           
                    //}
                    //else
                    //{
                    //    txt_Itemcode.Text = "";
                    //    txt_Itemcode.Focus();
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //public static int batch_entry = 0;
        private void dgvstockOut_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    Decimal TotalItems = 0, TotalAmount = 0;
                    string itmCode = "", quantity = "";
                    if (dgvstockOut.CurrentCell.OwningColumn.Name == "Del")
                    {
                        itemId = "0";
                        itemId= dgvstockOut.CurrentRow.Cells["id"].Value.ToString();
                        string entery = "0";
                        int index = dgvstockOut.CurrentRow.Index;
                        itmCode = dgvstockOut.CurrentRow.Cells["id"].Value.ToString();
                        DialogResult res = MessageBox.Show("Are you sure you want to delete?", "Delete confirmation",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (res == DialogResult.No)
                        {
                        }
                        else
                        {
                            if (cmb_action.Text == "Stock Out")
                            {
                                if (dgvstockOut.Rows[rowindex].Cells["batchentry"].Value != null)
                                    batch_entry = Convert.ToInt32(dgvstockOut.Rows[rowindex].Cells["batchentry"].Value.ToString());
                                 entery = dgvstockOut.CurrentRow.Cells["batchentry"].Value.ToString();
                            }
                               
                            //if (dgvstockOut.Rows[rowindex].Cells["batchentry"].Value != null)
                            //bath_entry = dgvstockOut.Rows[rowindex].Cells["batchentry"].Value.ToString();
                           
                         
                            if (txt_totalItems.Text != "")
                            {
                                TotalItems = Convert.ToDecimal(txt_totalItems.Text) - 1;
                                txt_totalItems.Text = Convert.ToDecimal(TotalItems).ToString();
                            }
                            if (txt_netAmount.Text != "")
                            {
                                TotalAmount = Convert.ToDecimal(txt_netAmount.Text) - Convert.ToDecimal(dgvstockOut.Rows[index].Cells["total"].Value.ToString());
                                txt_netAmount.Text = Convert.ToDecimal(TotalAmount).ToString("##0.00");
                            }
                           
                            if (cmb_action.Text == "Stock Out")
                                delete_batch(entery);// fill_Batch_delete(itmCode, quantity); 
                            else
                            {
                                if(dgvstockOut.Rows.Count==1)
                                {
                                    dgvGridData.Rows.Clear();
                                }
                                else
                                    update_Grid_stockin();
                            }
                            dgvstockOut.Rows.RemoveAt(index);
                        }
                    }
                    else if (dgvstockOut.CurrentCell.OwningColumn.Name == "edit")
                    {
                        itemId = "0";
                        if (cmb_action.Text == "Stock Out")
                            batch_entry = Convert.ToInt32(dgvstockOut.CurrentRow.Cells["batchentry"].Value.ToString());
                       
                        cmb_unit.Text = dgvstockOut.Rows[rowindex].Cells["unit"].Value.ToString();
                        rowindex = dgvstockOut.CurrentRow.Index;
                        itemId = dgvstockOut.CurrentRow.Cells["id"].Value.ToString();
                        txt_Itemcode.Text = dgvstockOut.CurrentRow.Cells["itemcode"].Value.ToString();
                        txtDescription.Text = dgvstockOut.CurrentRow.Cells["itemcode"].Value.ToString();
                        cmb_unit.Text = dgvstockOut.CurrentRow.Cells["unit"].Value.ToString();
                        txt_gst.Text = dgvstockOut.CurrentRow.Cells["gst"].Value.ToString();
                        txt_givenqty.Text = dgvstockOut.CurrentRow.Cells["Qty"].Value.ToString();
                        txtUnitCost.Text = dgvstockOut.CurrentRow.Cells["unit_cost"].Value.ToString();
                        txt_Amount.Text = dgvstockOut.CurrentRow.Cells["Total"].Value.ToString();
                        Btn_Add.Text = "Update";
                        BtnCancel.Visible = true; DataTable dt_batch = this.cntrl.get_batchdetails(itemId);
                        if (dt_batch.Rows.Count > 0)
                        {
                            batch_flag = false;
                        }
                        else
                        {
                            batch_flag = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void delete_batch(string entery)
        {
            for (int i = 0; i < dgv_BatchSale.Rows.Count; i++)
            {
                if (dgv_BatchSale.Rows[i].Cells["colBatchEntry"].Value.ToString() == entery)
                {
                    dgv_BatchSale.Rows.RemoveAt(i);
                }
            }
        }
        //public void fill_Batch_delete(string itemcode, string quantity)
        //{
        //    DataTable dtrow = new DataTable();
        //    dtrow.Columns.Clear();
        //    dtrow.Rows.Clear();
        //    dtrow.Columns.Add("BatchEntry");
        //    dtrow.Columns.Add("Batchno");
        //    dtrow.Columns.Add("ItemCode");
        //    dtrow.Columns.Add("itemname");
        //    dtrow.Columns.Add("unit");
        //    dtrow.Columns.Add("givenqty");
        //    dtrow.Columns.Add("cost");
        //    dtrow.Columns.Add("CurrentStock");

        //    dtrow.Columns.Add("stock");
        //    dtrow.Columns.Add("prddate");
        //    dtrow.Columns.Add("expdate");
        //    if (dtrow.Columns.Count > 0)
        //    {
        //        for (int i = 0; i < dgv_BatchSale.Rows.Count; i++)
        //        {
        //            if (dgv_BatchSale.Rows[i].Cells["coiltem_code"].Value.ToString() != itemcode)
        //            {
        //                dtrow.Rows.Add(dgv_BatchSale.Rows[i].Cells["colBatchEntry"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colBatchnumber"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["coiltem_code"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["itemname"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["b_unit"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["givenqty"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["Cost"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["stock"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colQuantity"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["prddate"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["expdate"].Value.ToString());
        //            }
        //        }
        //    }
        //    if (dtrow.Rows.Count > 0)
        //    {
        //        dgv_BatchSale.Rows.Clear();
        //        for (int i = 0; i < dtrow.Rows.Count; i++)
        //        {
        //            dgv_BatchSale.Rows.Add(dtrow.Rows[i][0].ToString(), dtrow.Rows[i][1].ToString(), dtrow.Rows[i][2].ToString(), dtrow.Rows[i][3].ToString(), dtrow.Rows[i][4].ToString(), dtrow.Rows[i][5].ToString(), dtrow.Rows[i][6].ToString(), dtrow.Rows[i]["stock"].ToString(), dtrow.Rows[i][7].ToString(), dtrow.Rows[i]["prddate"].ToString(), dtrow.Rows[i]["expdate"].ToString());
        //        }
        //    }
        //    else
        //    {
        //        dgv_BatchSale.Rows.Clear();
        //    }
        //}
        string unit2Is; purchase_controller pcntrl = new purchase_controller();
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (dgvstockOut.Rows.Count>0)
            {
                if (cmb_action.Text != "")
                {
                    if (cmb_action.Text == "Stock Out")
                    {
                        if (cmb_givento.Text != "")
                        {
                            string cs = PappyjoeMVC.Model.Connection.MyGlobals.trans_connectionstring;
                            using (MySqlConnection con = new MySqlConnection(cs))
                            {
                                con.Open();
                                MySqlTransaction trans = con.BeginTransaction();
                                try
                                {
                                    this.cntrl.stock_out_main_save(txt_RefNo.Text, Convert.ToDateTime(dtpentryDate.Value).ToString("yyyy-MM-dd"), cmb_givento.SelectedValue.ToString(), cmb_action.Text, Convert.ToDecimal(txt_netAmount.Text), con, trans);
                                    string maxid = this.cntrl.maxid_stockout(con, trans);
                                    for (int i = 0; i < dgv_BatchSale.Rows.Count; i++)
                                    {
                                        if (Convert.ToDecimal(dgv_BatchSale.Rows[i].Cells["colQuantity"].Value) >= 0)
                                        {
                                            int k = this.cntrl.update_stockOut(dgv_BatchSale.Rows[i].Cells["colQuantity"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colBatchEntry"].Value.ToString(), con, trans);
                                            if (k > 0)
                                            {
                                                this.cntrl.save_stockout(maxid, txt_RefNo.Text, dgv_BatchSale.Rows[i].Cells["coiltem_code"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["itemname"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colBatchnumber"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["b_unit"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["givenqty"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["cost"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colBatchEntry"].Value.ToString(), con, trans);
                                            }
                                        }
                                    }
                                    trans.Commit();
                                    con.Close();
                                    all_clear(); lb_taken.Visible = false;
                                    lb_givento.Visible = true;
                                    doc_num_incerment();
                                    MessageBox.Show("Data Saved Successfully", "Data Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    MessageBox.Show(ex.Message, "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Select the given lab", "Data Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else if (cmb_action.Text == "Stock In")
                    {
                        if (cmb_givento.Text != "")
                        {
                            string cs = PappyjoeMVC.Model.Connection.MyGlobals.trans_connectionstring;
                            using (MySqlConnection con = new MySqlConnection(cs))
                            {
                                con.Open();
                                MySqlTransaction trans = con.BeginTransaction();
                                try
                                {
                                    string purnumber = "";
                                    this.cntrl.stock_in_main_save(txt_RefNo.Text, Convert.ToDateTime(dtpentryDate.Value).ToString("yyyy-MM-dd"), cmb_givento.SelectedValue.ToString(), cmb_action.Text, Convert.ToDecimal(txt_netAmount.Text), con, trans);
                                    string maxid_id = this.cntrl.maxid_stockin(con, trans);
                                    DataTable dtb = this.cntrl.trans_incrementDocnumber(con, trans);
                                    if (String.IsNullOrWhiteSpace(dtb.Rows[0][0].ToString()))
                                    {
                                        purnumber = "1";
                                    }
                                    else
                                    {
                                        int Count = Convert.ToInt32(dtb.Rows[0][0]) + 1;
                                        purnumber = Count.ToString();
                                    }
                                    for (int k = 0; k < dgvstockOut.Rows.Count; k++)
                                    {

                                            string supName = ""; int suplier_id = 1; string IsExpDate = "";
                                            string expdate = "";
                                            DataTable max_supNo = this.cntrl.get_Supliermaxid(con, trans);
                                            if (max_supNo.Rows.Count > 0)
                                            {
                                                suplier_id = Convert.ToInt32(max_supNo.Rows[0][0].ToString()) + 1;
                                            }
                                            else
                                                suplier_id = 1;
                                            DataTable dt_suplier = this.cntrl.trans_get_suppliername("stockIn_updation", con, trans);
                                            if (dt_suplier.Rows.Count <= 0)
                                            {
                                                this.cntrl.Save(suplier_id.ToString(), "stockIn_updation", "ct", "9999999999", "", "", "", "", "", "", "", "10", "Stock_transfer");
                                                DataTable suppler_Name = this.cntrl.trans_get_suppliername("stockIn_updation", con, trans);
                                                if (suppler_Name.Rows.Count > 0)
                                                {
                                                    supName = suppler_Name.Rows[0][0].ToString();
                                                }
                                            }
                                            else
                                            {
                                                supName = dt_suplier.Rows[0][0].ToString();
                                            }
                                            DataTable dt_purno = this.cntrl.check_purno(purnumber, con, trans);//int ii = 0;
                                            if (dt_purno.Rows.Count <= 0)
                                            {
                                                this.cntrl.save_purchase(purnumber, "", DateTime.Now.Date.ToString("yyyy-MM-dd"), supName, "0", txt_netAmount.Text, "0", "0", "0", "StockTransfer", "Pharmacy", con, trans);
                                            }
                                            DataTable dtunit2 = this.cntrl.get_item_unitmf(dgvstockOut.Rows[k].Cells["id"].Value.ToString(), con, trans);
                                            if (dtunit2.Rows.Count > 0)
                                            {
                                                if (dgvstockOut.Rows[k].Cells["Unit"].Value.ToString() == dtunit2.Rows[0]["Unit2"].ToString())
                                                {
                                                    unit2Is = "Yes";
                                                }
                                                else
                                                {
                                                    unit2Is = "No";
                                                }
                                            }
                                            this.cntrl.save_purchaseit(purnumber, DateTime.Now.Date.ToString("yyyy-MM-dd"), dgvstockOut.Rows[k].Cells["id"].Value.ToString(), dgvstockOut.Rows[k].Cells["description"].Value.ToString(), "", "", dgvstockOut.Rows[k].Cells["Unit"].Value.ToString(), dgvstockOut.Rows[k].Cells["qty"].Value.ToString(), 0, dgvstockOut.Rows[k].Cells["Unit_Cost"].Value.ToString(), dgvstockOut.Rows[k].Cells["total"].Value.ToString(), unit2Is, dtunit2.Rows[0]["UnitMF"].ToString(), Convert.ToDecimal(dgvstockOut.Rows[k].Cells["gst"].Value.ToString()), 0, con, trans);

                                            for (int l = 0; l < dgvGridData.Rows.Count; l++)
                                            {
                                                if (dgvstockOut.Rows[k].Cells["id"].Value.ToString() == dgvGridData.Rows[l].Cells["tempItem_code"].Value.ToString())
                                                {
                                                    if (dgvGridData.Rows[l].Cells["Exp_Date"].Value.ToString() != "" && dgvGridData.Rows[l].Cells["Exp_Date"].Value.ToString() != null)
                                                    {
                                                        IsExpDate = "Yes";
                                                        expdate = Convert.ToDateTime(dgvGridData.Rows[l].Cells["Exp_Date"].Value.ToString()).ToString("yyyy-MM-dd");
                                                    }
                                                    else
                                                    {
                                                        IsExpDate = "NO";
                                                        expdate = "";
                                                    }
                                                decimal total_batch_qty = 0;
                                                DataTable batch = this.pcntrl.check_have_same_batch(dgvGridData.Rows[l].Cells["tempItem_code"].Value.ToString(), dgvGridData.Rows[l].Cells["Branch_No"].Value.ToString(), con, trans);
                                                if (batch.Rows.Count > 0)
                                                {
                                                    total_batch_qty = Convert.ToInt32(dgvGridData.Rows[l].Cells["col_temp_qty"].Value.ToString()) + Convert.ToDecimal(batch.Rows[0]["Qty"].ToString());
                                                    int a = this.pcntrl.update_same_batchNumber(dgvGridData.Rows[l].Cells["tempItem_code"].Value.ToString(), dgvGridData.Rows[l].Cells["Branch_No"].Value.ToString(), Convert.ToInt32(total_batch_qty), unit2Is, dtunit2.Rows[0]["UnitMF"].ToString(), purnumber, Convert.ToDateTime(dgvGridData.Rows[l].Cells["Prd_Date"].Value.ToString()).ToString("yyyy-MM-dd"), expdate, "", supName, DateTime.Now.Date.ToString("yyyy-MM-dd"), IsExpDate, "Pharmacy", batch.Rows[0]["Entry_No"].ToString(), con, trans);
                                                    //DataTable batch_entry = this.cntrl.get_maxEntryNo();
                                                }
                                                else
                                                {
                                                    int a = this.cntrl.save_batchNumber(dgvGridData.Rows[l].Cells["tempItem_code"].Value.ToString(), dgvGridData.Rows[l].Cells["Branch_No"].Value.ToString(), Convert.ToInt32(dgvGridData.Rows[l].Cells["col_temp_qty"].Value.ToString()), Convert.ToDecimal(dgvGridData.Rows[l].Cells["rate"].Value.ToString()), Convert.ToDecimal(dgvGridData.Rows[l].Cells["sales_rate"].Value.ToString()), unit2Is, unit2Is, dtunit2.Rows[0]["UnitMF"].ToString(), purnumber, Convert.ToDateTime(dgvGridData.Rows[l].Cells["Prd_Date"].Value.ToString()).ToString("yyyy-MM-dd"), expdate, "", supName, DateTime.Now.Date.ToString("yyyy-MM-dd"), IsExpDate, "Pharmacy", con, trans);
                                                    
                                                }
                                                DataTable batch_entry = this.cntrl.get_maxEntryNo();
                                                this.cntrl.save_batchpurchase(purnumber, DateTime.Now.Date.ToString("yyyy-MM-dd"), supName, dgvGridData.Rows[l].Cells["tempItem_code"].Value.ToString(), dgvGridData.Rows[l].Cells["Branch_No"].Value.ToString(), Convert.ToInt32(dgvGridData.Rows[l].Cells["col_temp_qty"].Value.ToString()), unit2Is, dtunit2.Rows[0]["UnitMF"].ToString(), Convert.ToDateTime(dgvGridData.Rows[l].Cells["Prd_Date"].Value.ToString()).ToString("yyyy-MM-dd"), expdate, IsExpDate, batch_entry.Rows[0][0].ToString(), con, trans);

                                                    DataTable dt_itemname = this.cntrl.get_itemName_cost(dgvGridData.Rows[l].Cells["tempItem_code"].Value.ToString(), purnumber, con, trans);
                                                    DataTable dt_batEntry = this.cntrl.get_batchEntry(dgvGridData.Rows[l].Cells["tempItem_code"].Value.ToString(), dgvGridData.Rows[l].Cells["Branch_No"].Value.ToString(), con, trans);
                                                    this.cntrl.save_stockin(maxid_id, txt_RefNo.Text, dgvGridData.Rows[l].Cells["tempItem_code"].Value.ToString(), dt_itemname.Rows[0]["Desccription"].ToString(), dgvGridData.Rows[l].Cells["Branch_No"].Value.ToString(), dgvGridData.Rows[l].Cells["Item_unit"].Value.ToString(), dgvGridData.Rows[l].Cells["col_temp_qty"].Value.ToString(), dt_itemname.Rows[0]["Rate"].ToString(), dt_batEntry.Rows[0]["Entry_No"].ToString(), con, trans);

                                                }

                                            }
                                    }
                                    trans.Commit();
                                    con.Close();
                                    all_clear();
                                    cmb_action.SelectedIndex = 1;
                                    lb_taken.Visible = true;
                                    lb_givento.Visible = false;
                                    stockin_doc_num_incerment();
                                    MessageBox.Show("Data Saved Successfully", "Data Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    MessageBox.Show(ex.Message, "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Select the taken lab", "Data Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Choose an action first", "Data Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please add data to the grid", "Data Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void all_clear()
        {
            txt_Itemcode.Text = "";
            txtDescription.Text = "";
            cmb_unit.Text = "";
            txt_givenqty.Text = "0";
            txt_gst.Text = "0";
            txtUnitCost.Text = "0.00";
            txt_Amount.Text = "0.00";
            cmb_action.SelectedIndex = 0;
            dgvstockOut.Rows.Clear();
            dgv_BatchSale.Rows.Clear();
            txt_netAmount.Text = "0.00";
            txt_totalItems.Text = "0"; 
            dgvGridData.Rows.Clear();
            cmb_action.Enabled = true;
        }

        private void txt_givenqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            string a = txt_givenqty.Text;
            string b = a.TrimStart('0');
            txt_givenqty.Text = b;
        }

        private void txt_givenqty_KeyUp(object sender, KeyEventArgs e)
        {
         
            if (txtUnitCost.Text != "")
            {
                TotalAmount_Calculation();
            }
        }

        public void TotalAmount_Calculation()
        {
            Decimal gst = 0, gst_Amount = 0, unitcost = 0, Amount = 0, qty=0, TotalAmount = 0;
            decimal d;
                if (txt_gst.Text != "")
                {
                    gst = Convert.ToDecimal(txt_gst.Text);
                }
            unitcost = Convert.ToDecimal(txtUnitCost.Text);
            if (decimal.TryParse(txt_givenqty.Text, out d))
            {
                qty = Convert.ToDecimal(txt_givenqty.Text);
            }
            if (gst > 0)
            {
                Amount = qty * unitcost;
                gst_Amount = (Amount * gst) / 100;
                TotalAmount = Amount + gst_Amount;
                txt_Amount.Text = TotalAmount.ToString("##.00");
            }
            else
            {
                TotalAmount = (qty * unitcost);
                txt_Amount.Text = TotalAmount.ToString("##.00");
            }
        }

        private void cmb_unit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmb_unit.SelectedIndex >= 0)
                {
                    DataTable dtb = this.cntrl.itemdetails(itemId);
                    if (dtb.Rows.Count > 0)
                    {
                        if (cmb_unit.Text == dtb.Rows[0]["Unit1"].ToString())
                        {
                            txtUnitCost.Text = Convert.ToDecimal(dtb.Rows[0]["Purch_Rate"].ToString()).ToString("##.00");
                        }
                        else if (cmb_unit.Text == dtb.Rows[0]["Unit2"].ToString())
                        {
                            txtUnitCost.Text = Convert.ToDecimal(dtb.Rows[0]["Purch_Rate2"].ToString()).ToString("##.00");
                        }
                        TotalAmount_Calculation();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        DataTable FrmBatchsale_edit = new DataTable();
        private decimal total_rate;
        private int v;
        public static decimal totalbatchrate = 0;
        private void Btn_Add_Click(object sender, EventArgs e)
        {
            try
            {

                decimal Stock = 0, qty = 0, edit_stck = 0; ;
                if (!string.IsNullOrWhiteSpace(txt_Itemcode.Text) && !string.IsNullOrWhiteSpace(txtDescription.Text) && !string.IsNullOrWhiteSpace(txt_givenqty.Text))
                {
                    if (Convert.ToDecimal(txt_givenqty.Text) > 0 && Convert.ToDecimal(txtUnitCost.Text) > 0)
                    {
                        DataTable dt_itemdetails = this.cntrl.itemdetails(itemId);
                        DataTable Dt_updateStock = this.cntrl.Get_stock(itemId);
                        qty = Convert.ToDecimal(txt_givenqty.Text);
                        string unit = cmb_unit.Text;
                        if (Dt_updateStock.Rows[0][0].ToString() != "")
                        {
                            TotalStock = Convert.ToDecimal(Dt_updateStock.Rows[0][0].ToString());
                        }
                        if (dt_itemdetails.Rows.Count > 0)
                        {
                            decimal unitmf = Convert.ToDecimal(dt_itemdetails.Rows[0]["UnitMF"].ToString());
                            if (unitmf > 0)
                            {
                                if (dt_itemdetails.Rows[0]["Unit1"].ToString() == unit)
                                {
                                    Stock = qty * unitmf;
                                }
                                else
                                {
                                    Stock = qty;
                                }
                            }
                            else
                            {
                                Stock = qty;
                            }
                        }
                        cmb_action.Enabled = false;
                        if (cmb_action.Text == "Stock Out")
                        {
                           
                            if (Btn_Add.Text == "Add")
                            {
                                if (Stock <= TotalStock)
                                {
                                    var form2 = new stocktransfer_batch(itemId, qty, unit);
                                    form2.Action = "Stock Out";
                                    form2.ShowDialog();
                                    if (dtb_batch_load != null)
                                    {
                                        if (dgvstockOut.Rows.Count > 0)
                                        {
                                            if (itemcheck() == 1)
                                            {
                                                return;
                                            }
                                        }
                                        decimal total = 0;
                                        if (totalbatchrate != 0)
                                        {
                                            txtUnitCost.Text = totalbatchrate.ToString();
                                        }
                                        TotalAmount_Calculation();
                                        Fiil_BatchSale_Grid(dtb_batch_load);
                                        foreach (DataRow dr in dtb_batch_load.Rows)
                                        {
                                            if (dr["ColQty"].ToString() != "" && dr["ColQty"].ToString() != "0")
                                            {
                                                dgvstockOut.Rows.Add(itemId, txt_Itemcode.Text, txtDescription.Text, dr["colbatchNo"].ToString(), dr["colentryNo"].ToString(), cmb_unit.Text, txt_gst.Text, txt_givenqty.Text, txtUnitCost.Text, txt_Amount.Text, PappyjoeMVC.Properties.Resources.editicon, PappyjoeMVC.Properties.Resources.deleteicon);
                                                //dgv_SalesItem.Rows.Add(itemId, txt_ItemCode.Text, txt_Discription.Text, txt_Packing.Text, dr["colbatchNo"].ToString(), dr["colentryNo"].ToString(), dt_unit1.Rows[0]["HSN_Number"].ToString(), cmb_Unit.Text, txt_GST.Text, txt_IGST.Text, txt_Qty.Text, txt_Free.Text, txtdisc.Text, txt_UnitCost.Text, txt_Amount.Text, PappyjoeMVC.Properties.Resources.editicon, PappyjoeMVC.Properties.Resources.deleteicon);
                                            }
                                        }





                                        
                                        for(int k=0;k<dgvstockOut.Rows.Count;k++)
                                        {
                                            //TotalAmount = Convert.ToDecimal(dgvstockOut.Rows[0].Cells["Total"].Value.ToString());
                                            total = total + Convert.ToDecimal(dgvstockOut.Rows[k].Cells["Total"].Value.ToString());// Convert.ToDecimal(dr.Cells["Total"].ToString());
                                        }
                                        //txt_netAmount
                                        txt_netAmount.Text = total.ToString("#.00");
                                        txt_totalItems.Text = dgvstockOut.Rows.Count.ToString();
                                        clear_itemdetails();
                                        dtb_batch_load = null;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Does not add batch!..", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Quantity is greater than the stock", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            else
                            {
                                FrmBatchsale_edit.Rows.Clear();
                                FrmBatchsale_edit.Columns.Clear();
                                createTempTable();
                                if (FrmBatchsale_edit.Rows.Count > 0)
                                {
                                    if (Stock <= TotalStock)
                                    {
                                        if (FrmBatchsale_edit.Rows.Count == 1)
                                        {
                                            edit_stck = Stock;
                                        }
                                        else
                                        {
                                            edit_stck = qty;
                                        }
                                        var form2 = new stocktransfer_batch(itemId, edit_stck, FrmBatchsale_edit, unit);
                                        form2.Action = "Stock Out";
                                        form2.ShowDialog();
                                        form2.Dispose();
                                        if (rowindex >= 0)
                                        {
                                            decimal total = 0;
                                            if (totalbatchrate != 0)
                                            {
                                                txtUnitCost.Text = totalbatchrate.ToString();
                                            }
                                            TotalAmount_Calculation();
                                            dgvstockOut.Rows[rowindex].Cells["id"].Value = itemId;
                                            dgvstockOut.Rows[rowindex].Cells["itemcode"].Value = txt_Itemcode.Text;
                                            dgvstockOut.Rows[rowindex].Cells["description"].Value = txtDescription.Text;
                                            dgvstockOut.Rows[rowindex].Cells["batch"].Value = dtb_items_load.Rows[0]["colbatchNo"].ToString();
                                            dgvstockOut.Rows[rowindex].Cells["batchentry"].Value = dtb_items_load.Rows[0]["colentryNo"].ToString();
                                            dgvstockOut.Rows[rowindex].Cells["unit"].Value = cmb_unit.Text;
                                            dgvstockOut.Rows[rowindex].Cells["gst"].Value = txt_gst.Text;
                                            dgvstockOut.Rows[rowindex].Cells["Qty"].Value = txt_givenqty.Text;
                                            dgvstockOut.Rows[rowindex].Cells["unit_cost"].Value = txtUnitCost.Text;
                                            dgvstockOut.Rows[rowindex].Cells["total"].Value = txt_Amount.Text;
                                            dgvstockOut.Rows[rowindex].Cells["edit"].Value = PappyjoeMVC.Properties.Resources.editicon;
                                            dgvstockOut.Rows[rowindex].Cells["Del"].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                                            if (dtb_items_load != null)
                                            {
                                                if (dgvstockOut.Rows.Count == 1)
                                                {
                                                    dgv_BatchSale.Rows.Clear();
                                                    fill_batch_onerow();
                                                }
                                                else
                                                    fill_batch_sales();
                                                //    update_Grid();
                                                //Fiil_BatchSale_Grid(dtb_items_load);// fill_Updategrid();
                                                for (int k = 0; k < dgvstockOut.Rows.Count; k++)
                                                {
                                                    //TotalAmount = Convert.ToDecimal(dgvstockOut.Rows[0].Cells["Total"].Value.ToString());
                                                    total = total + Convert.ToDecimal(dgvstockOut.Rows[k].Cells["Total"].Value.ToString());// Convert.ToDecimal(dr.Cells["Total"].ToString());
                                                }
                                                //txt_netAmount
                                                txt_netAmount.Text = total.ToString("#.00");
                                                clear_itemdetails(); txt_Itemcode.Enabled = true;
                                                Btn_Add.Text = "Add"; BtnCancel.Visible = false;
                                                dtb_items_load = null;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Does not add batch!..", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Quantity is greater than the stock", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }
                            }
                            totalbatchrate = 0;
                            batch_entry = 0;
                        }
                        else if (cmb_action.Text == "Stock In")
                        {
                          
                            if (Btn_Add.Text == "Add")
                            {
                                if (batch_flag == false)
                                {
                                    var form2 = new Stock_in_Batch(itemId, Stock, unit, txtUnitCost.Text,"");
                                    form2.Action = "Stock In";
                                    form2.ShowDialog();
                                    if (dtb_items_load != null)
                                    {
                                        decimal total = 0;
                                        decimal newcost = 0;
                                        if (total_batch_rate != 0)
                                        {
                                            txtUnitCost.Text = total_batch_rate.ToString("0.00");
                                            newcost = total_batch_rate;
                                        }
                                        TotalAmount_Calculation();
                                        fill_stock_in_batch_grid(dtb_items_load);// Fiil_BatchSale_Grid(dtb_batch_load);
                                        dgvstockOut.Rows.Add(itemId, txt_Itemcode.Text, txtDescription.Text,"","", cmb_unit.Text,txt_gst.Text, txt_givenqty.Text, txtUnitCost.Text, txt_Amount.Text, PappyjoeMVC.Properties.Resources.editicon, PappyjoeMVC.Properties.Resources.deleteicon);
                                        for (int k = 0; k < dgvstockOut.Rows.Count; k++)
                                        {
                                            //TotalAmount = Convert.ToDecimal(dgvstockOut.Rows[0].Cells["Total"].Value.ToString());
                                            total = total + Convert.ToDecimal(dgvstockOut.Rows[k].Cells["Total"].Value.ToString());// Convert.ToDecimal(dr.Cells["Total"].ToString());
                                        }
                                        //TotalAmount = TotalAmount + Convert.ToDecimal(txt_Amount.Text);
                                        txt_netAmount.Text = total.ToString("#.000");
                                        txt_totalItems.Text = dgvstockOut.Rows.Count.ToString();
                                        clear_itemdetails();
                                        dtb_items_load  = null;//dtb_batch_load
                                    }
                                    else
                                    {
                                        MessageBox.Show("Did not add batch!..", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    var form2 = new Stock_in_Batch(itemId, Stock, unit, txtUnitCost.Text, "");
                                    form2.Action = "Stock In";
                                    form2.ShowDialog();
                                    if (dtb_items_load != null)
                                    {
                                        decimal newcost = 0;
                                        if (total_batch_rate != 0)
                                        {
                                            txtUnitCost.Text = total_batch_rate.ToString("0.00");
                                            newcost = total_batch_rate;
                                        }
                                        fill_stock_in_batch_grid(dtb_items_load);
                                        dgvstockOut.Rows.Add(itemId, txt_Itemcode.Text, txtDescription.Text,"","", cmb_unit.Text,txt_gst.Text, txt_givenqty.Text, txtUnitCost.Text, txt_Amount.Text, PappyjoeMVC.Properties.Resources.editicon, PappyjoeMVC.Properties.Resources.deleteicon);
                                        foreach (DataGridViewRow dr in dgvstockOut.Rows)
                                        {
                                            //TotalAmount = Convert.ToDecimal(dr.Cells["Total"].ToString());
                                            TotalAmount = TotalAmount + Convert.ToDecimal(dr.Cells["Total"].Value.ToString());
                                        }
                                        //TotalAmount = TotalAmount + Convert.ToDecimal(txt_Amount.Text);
                                        txt_netAmount.Text = TotalAmount.ToString("#.00");
                                        txt_totalItems.Text = dgvstockOut.Rows.Count.ToString();
                                        clear_itemdetails();
                                        dtb_items_load = null;
                                        batch_flag = false;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Did not add batch!..", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                            else
                            {
                                FrmBatchsale_edit.Rows.Clear();
                                FrmBatchsale_edit.Columns.Clear();
                                if (batch_flag == false)
                                {
                                    createTempTable_stockin();// createTempTable();
                                    if (FrmBatchsale_edit.Rows.Count > 0)
                                    {
                                        //if (Stock <= TotalStock)
                                        //{
                                            var form2 = new Stock_in_Batch(itemId, Stock, FrmBatchsale_edit, unit);
                                            form2.Action = "Stock In";
                                            form2.ShowDialog();
                                            form2.Dispose();
                                            if (rowindex >= 0)
                                            {
                                                dgvstockOut.Rows[rowindex].Cells["id"].Value = itemId;
                                                dgvstockOut.Rows[rowindex].Cells["itemcode"].Value = txt_Itemcode.Text;
                                                dgvstockOut.Rows[rowindex].Cells["description"].Value = txtDescription.Text;
                                                dgvstockOut.Rows[rowindex].Cells["unit"].Value = cmb_unit.Text;
                                                dgvstockOut.Rows[rowindex].Cells["gst"].Value = txt_gst.Text;
                                                dgvstockOut.Rows[rowindex].Cells["Qty"].Value = txt_givenqty.Text;
                                                dgvstockOut.Rows[rowindex].Cells["gst"].Value = txt_gst.Text;
                                                dgvstockOut.Rows[rowindex].Cells["unit_cost"].Value = txtUnitCost.Text;
                                                dgvstockOut.Rows[rowindex].Cells["total"].Value = txt_Amount.Text;
                                                dgvstockOut.Rows[rowindex].Cells["edit"].Value = PappyjoeMVC.Properties.Resources.editicon;
                                                dgvstockOut.Rows[rowindex].Cells["Del"].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                                                if (dtb_items_load != null)
                                                {
                                                    if (dgvstockOut.Rows.Count == 1)
                                                    {
                                                    dgvGridData.Rows.Clear();
                                                    }
                                                    else
                                                        update_Grid_stockin();
                                                    fill_stock_in_batch_grid(dtb_items_load); 
                                                    clear_itemdetails(); txt_Itemcode.Enabled = true;
                                                    Btn_Add.Text = "Add"; BtnCancel.Visible = false;
                                                    dtb_items_load = null;
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Does not add batch!..", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }
                                            }
                                        //}
                                        //else
                                        //{
                                        //    MessageBox.Show("Quantity is greater than the stock", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        //    return;
                                        //}
                                    }
                                }
                                else
                                {
                                    createTempTable_stockin();
                                    if (FrmBatchsale_edit.Rows.Count > 0)
                                    {
                                            var form2 = new Stock_in_Batch(itemId, Stock, FrmBatchsale_edit, unit);
                                            form2.Action = "Stock In";
                                            form2.editflag = true;
                                            form2.ShowDialog();
                                            form2.Dispose();
                                            if (rowindex >= 0)
                                            {
                                                dgvstockOut.Rows[rowindex].Cells["id"].Value = itemId;
                                                dgvstockOut.Rows[rowindex].Cells["itemcode"].Value = txt_Itemcode.Text;
                                                dgvstockOut.Rows[rowindex].Cells["description"].Value = txtDescription.Text;
                                                dgvstockOut.Rows[rowindex].Cells["unit"].Value = cmb_unit.Text; ;
                                                dgvstockOut.Rows[rowindex].Cells["gst"].Value = txt_gst.Text;
                                                dgvstockOut.Rows[rowindex].Cells["Qty"].Value = txt_givenqty.Text;
                                                dgvstockOut.Rows[rowindex].Cells["unit_cost"].Value = txtUnitCost.Text;
                                                dgvstockOut.Rows[rowindex].Cells["total"].Value = txt_Amount.Text;
                                                dgvstockOut.Rows[rowindex].Cells["edit"].Value = PappyjoeMVC.Properties.Resources.editicon;
                                                dgvstockOut.Rows[rowindex].Cells["Del"].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                                                if (dtb_items_load != null)
                                                {
                                                    if (dgvstockOut.Rows.Count == 1)
                                                    {
                                                        dgvGridData.Rows.Clear();
                                                    }
                                                    else
                                                        update_Grid_stockin();
                                                    fill_stock_in_batch_grid(dtb_items_load);// fill_Updategrid();
                                                    clear_itemdetails(); txt_Itemcode.Enabled = true;
                                                    Btn_Add.Text = "Add"; BtnCancel.Visible = false;
                                                    dtb_items_load = null;
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Does not add batch!..", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }
                                            }
                                    }
                                }
                                   
                               
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Mandatory fields should not be empty.", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_Itemcode.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Mandatory fields should not be empty.", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message, "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void fill_batch_sales()
        {
            for (int j = 0; j < dgv_BatchSale.Rows.Count; j++)
            {
                if (dgv_BatchSale.Rows[j].Cells["colBatchEntry"].Value.ToString() == dtb_items_load.Rows[0]["colentryNo"].ToString())
                {
                    //dgv_BatchSale.Rows[j].Cells["ColinvNum"].Value = txtDocumentNumber.Text;
                    //dgv_BatchSale.Rows[j].Cells["ColInvDate"].Value = dtpDocumentDate.Value.ToShortDateString();
                    dgv_BatchSale.Rows[j].Cells["colBatchEntry"].Value = dtb_items_load.Rows[0]["colentryNo"].ToString();
                    dgv_BatchSale.Rows[j].Cells["colBatchnumber"].Value = dtb_items_load.Rows[0]["colbatchNo"].ToString();
                    dgv_BatchSale.Rows[j].Cells["coiltem_code"].Value = itemId; 
                    dgv_BatchSale.Rows[j].Cells["itemname"].Value = txtDescription.Text;// dtb_items_load.Rows[0]["clUnit"].ToString();
                    dgv_BatchSale.Rows[j].Cells["b_unit"].Value = dtb_items_load.Rows[0]["clUnit"].ToString();
                    dgv_BatchSale.Rows[j].Cells["givenqty"].Value = dtb_items_load.Rows[0]["ColQty"].ToString();
                    dgv_BatchSale.Rows[j].Cells["Cost"].Value = dtb_items_load.Rows[0]["rate"].ToString();
                
                 
                    dgv_BatchSale.Rows[j].Cells["stock"].Value = dtb_items_load.Rows[0]["ColStock"].ToString();
                    //dgv_BatchSale.Rows[j].Cells["colIsExp"].Value = txt_givenqty.Text;
                    dgv_BatchSale.Rows[j].Cells["prddate"].Value = dtb_items_load.Rows[0]["ColPrd_Date"].ToString();
                    dgv_BatchSale.Rows[j].Cells["expdate"].Value = dtb_items_load.Rows[0]["colExpDate"].ToString();
                    //dgv_BatchSale.Rows[j].Cells["colQuantity"].Value = dtb_items_load.Rows[0]["ColQty"].ToString();//current qty
                
                    if (dtb_items_load.Rows[0]["colCurrentStock"].ToString() != "")
                    {
                        dgv_BatchSale.Rows[j].Cells["colQuantity"].Value = dtb_items_load.Rows[0]["colCurrentStock"].ToString();
                    }
                    else
                        dgv_BatchSale.Rows[j].Cells["colQuantity"].Value = dtb_items_load.Rows[0]["ColStock"].ToString();
                }
            }

        }
        public int itemcheck()
        {
            int affected = 0;
            for (int i = 0; i < dgvstockOut.Rows.Count; i++)
            {
                //for (int j = 0; j < dgv_SalesItem.Columns.Count; j++)
                //{
                if (dgvstockOut.Rows[i].Cells["itemcode"].Value != null && txt_Itemcode.Text == dgvstockOut.Rows[i].Cells["itemcode"].Value.ToString() && Convert.ToInt32(dgvstockOut.Rows[i].Cells["batchentry"].Value) == batch_entry)//&& cmb_Unit.Text== dgv_SalesItem.Rows[i].Cells["ColUnit"].Value.ToString()
                {
                    MessageBox.Show("There is a repetitive sale, check the row above ", "Duplication Encountered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    affected = 1;
                }
                //}
            }
            return affected;
        }
        public void createTempTable()
        {
            foreach (DataGridViewColumn col in dgv_BatchSale.Columns)
            {
                FrmBatchsale_edit.Columns.Add(col.Name);
            }
            foreach (DataGridViewRow row in dgv_BatchSale.Rows)
            {
                DataRow dRow = FrmBatchsale_edit.NewRow();
                //if (row.Cells["coiltem_code"].Value.ToString() == itemId)
                    if (row.Cells["coiltem_code"].Value.ToString() == itemId && row.Cells["b_unit"].Value.ToString() == cmb_unit.Text && row.Cells["colBatchEntry"].Value.ToString() == batch_entry.ToString())//
                    {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dRow[cell.ColumnIndex] = cell.Value;
                    }
                    FrmBatchsale_edit.Rows.Add(dRow);
                }
            }
        }
        public void createTempTable_stockin()
        {
            foreach (DataGridViewColumn col in dgvGridData.Columns)
            {
                FrmBatchsale_edit.Columns.Add(col.Name);
            }
            foreach (DataGridViewRow row in dgvGridData.Rows)
            {
                DataRow dRow = FrmBatchsale_edit.NewRow();
                if (row.Cells["tempItem_code"].Value.ToString() == itemId)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dRow[cell.ColumnIndex] = cell.Value;
                    }
                    FrmBatchsale_edit.Rows.Add(dRow);
                }
            }
        }
        public void update_Grid_stockin()//update casae, create table for remaining items in the batch sale grid except the updated item 
        {
            DataTable dt_Update = new DataTable();
            dt_Update.Columns.Clear();
            dt_Update.Rows.Clear();
            dt_Update.Columns.Add("ItemCode");
            dt_Update.Columns.Add("Batchno");
            dt_Update.Columns.Add("unit");
            dt_Update.Columns.Add("qty"); ;
            dt_Update.Columns.Add("rate");
            dt_Update.Columns.Add("sales_rate");
            dt_Update.Columns.Add("prdate");
            dt_Update.Columns.Add("expdate");
            if (dt_Update.Columns.Count > 0)
            {
                for (int i = 0; i < dgvGridData.Rows.Count; i++)
                {
                    if (dgvGridData.Rows[i].Cells["tempItem_code"].Value.ToString() != itemId)
                    {
                        dt_Update.Rows.Add(dgvGridData.Rows[i].Cells["tempItem_code"].Value.ToString(), dgvGridData.Rows[i].Cells["Branch_No"].Value.ToString(), dgvGridData.Rows[i].Cells["Item_unit"].Value.ToString(), dgvGridData.Rows[i].Cells["col_temp_qty"].Value.ToString(), dgvGridData.Rows[i].Cells["rate"].Value.ToString(), dgvGridData.Rows[i].Cells["sales_rate"].Value.ToString(), dgvGridData.Rows[i].Cells["Prd_Date"].Value.ToString(), dgvGridData.Rows[i].Cells["Exp_Date"].Value.ToString());
                    }
                }
            }
            if (dt_Update.Rows.Count > 0)//refill the batch sale grid only the remaining items in the table except the updated item 
            {
                dgvGridData.Rows.Clear();
                foreach (DataRow dr in dt_Update.Rows)
                {
                    dgvGridData.Rows.Add(dr["ItemCode"].ToString(), dr["Batchno"].ToString(), dr["unit"].ToString(), dr["qty"].ToString(), dr["rate"].ToString(), dr["sales_rate"].ToString(), dr["prdate"].ToString(), dr["expdate"].ToString());
                }
            }
        }
        //string bath_entry = "";
        public void fill_batch_onerow()
        {
            dgv_BatchSale.Rows.Add(dtb_items_load.Rows[0]["colentryNo"].ToString(), dtb_items_load.Rows[0]["colbatchNo"].ToString(), itemId, txtDescription.Text, dtb_items_load.Rows[0]["clUnit"].ToString(), dtb_items_load.Rows[0]["ColQty"].ToString(), dtb_items_load.Rows[0]["rate"].ToString(), dtb_items_load.Rows[0]["ColStock"].ToString(), dtb_items_load.Rows[0]["ColPrd_Date"].ToString(), dtb_items_load.Rows[0]["colExpDate"].ToString());
            if (dtb_items_load.Rows[0]["colCurrentStock"].ToString() != "")
            {
                dgv_BatchSale.Rows[0].Cells["colQuantity"].Value = dtb_items_load.Rows[0]["colCurrentStock"].ToString();
            }
            else
                dgv_BatchSale.Rows[0].Cells["colQuantity"].Value = dtb_items_load.Rows[0]["ColStock"].ToString();
        }
        public void update_Grid()//update casae, create table for remaining items in the batch sale grid except the updated item 
        {
            DataTable dt_Update = new DataTable();
            dt_Update.Columns.Clear();
            dt_Update.Rows.Clear();
            dt_Update.Columns.Add("BatchEntry");
            dt_Update.Columns.Add("Batchno");
            dt_Update.Columns.Add("ItemCode");
            dt_Update.Columns.Add("ItemName");
            dt_Update.Columns.Add("unit");
            dt_Update.Columns.Add("Givenqty");
            dt_Update.Columns.Add("Cost");

            dt_Update.Columns.Add("stock");
            dt_Update.Columns.Add("CurrentStock");
            dt_Update.Columns.Add("prddate");
            dt_Update.Columns.Add("expdate");
            if (dt_Update.Columns.Count > 0)
            {
                for (int i = 0; i < dgv_BatchSale.Rows.Count; i++)
                {
                    if (dgv_BatchSale.Rows[i].Cells["coiltem_code"].Value.ToString() != itemId)
                    {
                        dt_Update.Rows.Add(dgv_BatchSale.Rows[i].Cells["colBatchEntry"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colBatchnumber"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["coiltem_code"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["itemname"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["b_unit"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["givenqty"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["Cost"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["stock"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["colQuantity"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["prddate"].Value.ToString(), dgv_BatchSale.Rows[i].Cells["expdate"].Value.ToString());
                    }
                }
            }
            if (dt_Update.Rows.Count > 0)//refill the batch sale grid only the remaining items in the table except the updated item 
            {
                dgv_BatchSale.Rows.Clear();
                foreach (DataRow dr in dt_Update.Rows)
                {
                    dgv_BatchSale.Rows.Add(dr["BatchEntry"].ToString(), dr["Batchno"].ToString(), dr["ItemCode"].ToString(), dr["ItemName"].ToString(), dr["unit"].ToString(), dr["Givenqty"].ToString(), dr["Cost"].ToString(), dr["stock"].ToString(), dr["CurrentStock"].ToString(), dr["prddate"].ToString(), dr["expdate"].ToString());
                }
            }
        }
        public void  fill_stock_in_batch_grid(DataTable dt)
        {
            int row = dgvGridData.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Quantity"].ToString() != "")
                    {

                        dgvGridData.Rows.Add();
                        dgvGridData.Rows[row].Cells["tempItem_code"].Value = itemId;
                        dgvGridData.Rows[row].Cells["Branch_No"].Value = dr["Branch_No"].ToString();
                        dgvGridData.Rows[row].Cells["Item_unit"].Value = dr["col_Unit"].ToString();
                        dgvGridData.Rows[row].Cells["col_temp_qty"].Value = dr["Quantity"].ToString();
                        dgvGridData.Rows[row].Cells["rate"].Value = dr["rate"].ToString(); //dt_forBatch.Rows[i]["rate"].ToString();
                        dgvGridData.Rows[row].Cells["sales_rate"].Value = dr["sales_rate"].ToString();// dt_forBatch.Rows[i]["sales_rate"].ToString();

                        dgvGridData.Rows[row].Cells["Prd_Date"].Value = dr["Prd_Date"].ToString(); 
                        dgvGridData.Rows[row].Cells["Exp_Date"].Value = dr["Exp_Date"].ToString();
                        row++;
                        //if (dr["entryno"].ToString() == "")
                        //{
                        //    dgvGridData.Rows[rowindex].Cells["batchentry_"].Value = "0";
                        //}
                        //else
                        //    dgvGridData.Rows[rowindex].Cells["batchentry_"].Value = dr["entryno"].ToString();
                    }
                   

                }
            }
        }
        public void Fiil_BatchSale_Grid(DataTable dt)// fill the update item to the batch sale grid
        {
            int row = dgv_BatchSale.Rows.Count; 
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ColQty"].ToString() != "" && dr["ColQty"].ToString() != "0") //if (dr["ColQty"].ToString() != "0")
                    {

                        dgv_BatchSale.Rows.Add();
                        dgv_BatchSale.Rows[row].Cells["colBatchEntry"].Value = dr["colentryNo"].ToString();
                        dgv_BatchSale.Rows[row].Cells["colBatchnumber"].Value = dr["colbatchNo"].ToString();
                        dgv_BatchSale.Rows[row].Cells["coiltem_code"].Value = itemId;
                        dgv_BatchSale.Rows[row].Cells["itemname"].Value = txtDescription.Text;
                        dgv_BatchSale.Rows[row].Cells["b_unit"].Value = cmb_unit.Text;
                        dgv_BatchSale.Rows[row].Cells["givenqty"].Value = txt_givenqty.Text;
                        dgv_BatchSale.Rows[row].Cells["cost"].Value = txtUnitCost.Text;
                        dgv_BatchSale.Rows[row].Cells["stock"].Value = dr["ColStock"].ToString();
                        dgv_BatchSale.Rows[row].Cells["prddate"].Value = dr["ColPrd_Date"].ToString();
                        dgv_BatchSale.Rows[row].Cells["expdate"].Value = dr["colExpDate"].ToString();
                        if (dr["colCurrentStock"].ToString() != "")
                        {
                            dgv_BatchSale.Rows[row].Cells["colQuantity"].Value = dr["colCurrentStock"].ToString();
                        }
                        else
                            dgv_BatchSale.Rows[row].Cells["colQuantity"].Value = dr["ColStock"].ToString();
                        row++;
                    }
                   

                }
            }
        }
        public void clear_itemdetails()
        {
            txt_Itemcode.Text = "";
            txtDescription.Text = "";
            cmb_unit.Items.Clear();//
            cmb_unit .Text= "";
            txt_givenqty.Text = "0";
            txtUnitCost.Text = "0.00";
            txt_Amount.Text = "0.00";
            //txt_netAmount.Text = "0.00";
        }
        public void load_lab()
        {
            DataTable dt_labload = this.cntrl.Fill_lab_combo();
            if (dt_labload.Rows.Count > 0)
            {
                cmb_givento.DataSource = null;
                cmb_givento.DataSource = dt_labload;
                cmb_givento.DisplayMember = dt_labload.Columns["name"].ToString();
                cmb_givento.ValueMember = dt_labload.Columns["id"].ToString();
                cmb_givento.SelectedIndex = 0;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var form2 = new stock_transfer_partner();
            form2.ShowDialog();
            form2.Dispose();
            load_lab();
        }

        private void txt_givenqty_Click(object sender, EventArgs e)
        {
            if(txt_givenqty.Text=="0")
            {
                txt_givenqty.Text = "";
            }
        }


        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Btn_Add.Text = "Add";
            BtnCancel.Visible = false;
            clear_itemdetails();
        }

        private void BTN_CLEAR_Click(object sender, EventArgs e)
        {
            all_clear();
        }

        private void txt_gst_Click(object sender, EventArgs e)
        {
            if(txt_gst.Text=="0")
            {
                txt_gst.Text = "";
            }
        }

        private void txtUnitCost_TextChanged(object sender, EventArgs e)
        {
            //if (txtUnitCost.Text == "")
            //{
            //    TotalAmount_Calculation();
            //}

        }
    }
}
