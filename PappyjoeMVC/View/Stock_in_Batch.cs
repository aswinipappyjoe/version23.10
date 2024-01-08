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
    public partial class Stock_in_Batch : Form
    {
        stocktransfer_batch_controller cntrl = new stocktransfer_batch_controller();
        public bool editflag = false;
        public string Action = "";
        public string unit1,cost;
        public decimal qtyE;
        int Count;
        string itemid1;
        string Itemcode = "", purNo = "";
        bool ermsg = false, flagbatchError = false, flagdateError = false;
        public static DataTable gridData = new DataTable();
        DataTable editgrid1 = new DataTable();
        int flag = 0;
        private string v;

        public Stock_in_Batch()
        {
            InitializeComponent();
        }

        //public Stock_in_Batch(string itemId, decimal qty, string unit)
        //{
        //    InitializeComponent();
        //    itemid1 = itemId;
        //    qtyE = qty;
        //    unit1 = unit;
        //}

        public Stock_in_Batch(string itemId, decimal qty, DataTable frmBatchsale_edit, string unit)
        {
            InitializeComponent();
            itemid1 = itemId;
            qtyE = qty;
            editgrid1 = frmBatchsale_edit;
            unit1 = unit; editflag = true;
        }

        public Stock_in_Batch(string itemId, decimal qty, string unit, string text, string v) //: this(itemId, qty, unit)
        {
            InitializeComponent();
            itemid1 = itemId;
            qtyE = qty;
            unit1 = unit; cost = text;
        }

        private void dgvPurchaseBatch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgvPurchaseBatch_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(dgvPurchaseBatch_KeyPress);
            if (dgvPurchaseBatch.CurrentCell.ColumnIndex == 2)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(dgvPurchaseBatch_KeyPress);
                }
            }
            if (dgvPurchaseBatch.CurrentCell.ColumnIndex == 3)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(dgvPurchaseBatch_KeyPress);
                }
            }
        }

        private void dtpExp_CloseUp(object sender, EventArgs e)
        {
            dtpExp.Visible = false;
        }
        public static decimal total_rate = 0;
        private void dtpExp_ValueChanged(object sender, EventArgs e)
        {
            dgvPurchaseBatch.CurrentRow.Cells["Exp_Date"].Value = dtpExp.Text.ToString();
        }
        purchasebatch_controller cnt = new purchasebatch_controller();
        private void dgvPurchaseBatch_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgvPurchaseBatch.CurrentCell.OwningColumn.Name == "Branch_No")
                {
                    if (dgvPurchaseBatch.Rows[dgvPurchaseBatch.CurrentRow.Index].Cells["Branch_No"].Value != null && dgvPurchaseBatch.Rows[dgvPurchaseBatch.CurrentRow.Index].Cells["Branch_No"].Value.ToString() != "")
                    {
                        string unit = ""; decimal unitMf = 0, cost = 0, sales = 0;
                        string batch = dgvPurchaseBatch.Rows[dgvPurchaseBatch.CurrentRow.Index].Cells["Branch_No"].Value.ToString();    //CurrentRow.Cells["Branch_No"].Value.ToString();
                        DataTable dt_batch = this.cnt.get_batchrate_exp(itemid1, batch);
                        if (dt_batch.Rows.Count > 0)//
                        {
                            unit = dgvPurchaseBatch.Rows[dgvPurchaseBatch.CurrentRow.Index].Cells["col_Unit"].Value.ToString();
                            DataTable dtb_item = this.cnt.itemdetails(itemid1);
                            DataTable dtb = this.cnt.batchdetails(batch, itemid1);
                            if (dtb_item.Rows.Count > 0)
                            {
                                unitMf = Convert.ToDecimal(dtb_item.Rows[0]["UnitMF"].ToString());
                                if (dtb_item.Rows[0]["OneUnitOnly"].ToString() == "False")
                                {
                                    if (dtb.Rows[0]["purch_unit2"].ToString() == "No")//Unit2
                                    {
                                        if (unit == dtb_item.Rows[0]["Unit2"].ToString())
                                        {
                                            cost = Convert.ToDecimal(dtb.Rows[0]["batch_rate"].ToString()) / unitMf;
                                            sales = Convert.ToDecimal(dtb.Rows[0]["batch_sales_rate"].ToString()) / unitMf;
                                        }
                                        else
                                        {
                                            cost = Convert.ToDecimal(dtb.Rows[0]["batch_rate"].ToString());
                                            sales = Convert.ToDecimal(dtb.Rows[0]["batch_sales_rate"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        if (unit == dtb_item.Rows[0]["Unit2"].ToString())
                                        {

                                            cost = Convert.ToDecimal(dtb.Rows[0]["batch_rate"].ToString());
                                            sales = Convert.ToDecimal(dtb.Rows[0]["batch_sales_rate"].ToString());

                                        }
                                        else
                                        {

                                            cost = unitMf * Convert.ToDecimal(dtb.Rows[0]["batch_rate"].ToString());
                                            sales = unitMf * Convert.ToDecimal(dtb.Rows[0]["batch_sales_rate"].ToString());
                                        }
                                    }
                                }
                                else
                                {
                                    cost = Convert.ToDecimal(dtb.Rows[0]["batch_rate"].ToString());
                                    sales = Convert.ToDecimal(dtb.Rows[0]["batch_sales_rate"].ToString());
                                }
                            }
                            dgvPurchaseBatch.CurrentRow.Cells["rate"].Value = cost;// dt_batch.Rows[0]["batch_rate"].ToString();
                            dgvPurchaseBatch.CurrentRow.Cells["sales_rate"].Value = sales;// dt_batch.Rows[0]["batch_sales_rate"].ToString();
                            dgvPurchaseBatch.CurrentRow.Cells["Prd_Date"].Value = dt_batch.Rows[0]["PrdDate"].ToString();
                            dgvPurchaseBatch.CurrentRow.Cells["Exp_Date"].Value = dt_batch.Rows[0]["ExpDate"].ToString();
                            dgvPurchaseBatch.CurrentRow.Cells["rate"].ReadOnly = true;// dt_batch.Rows[0]["batch_rate"].ToString();
                            dgvPurchaseBatch.CurrentRow.Cells["sales_rate"].ReadOnly = true;// dt_batch.Rows[0]["batch_sales_rate"].ToString();
                            dgvPurchaseBatch.CurrentRow.Cells["Prd_Date"].ReadOnly = true;
                            dgvPurchaseBatch.CurrentRow.Cells["Exp_Date"].ReadOnly = true;
                            batch_date_flag = true;
                        }
                        else
                        {
                            dgvPurchaseBatch.Rows[dgvPurchaseBatch.CurrentRow.Index].Cells["col_Unit"].Value = unit1;
                            dgvPurchaseBatch.CurrentRow.Cells["rate"].Value = "0";// dt_batch.Rows[0]["batch_rate"].ToString();
                            dgvPurchaseBatch.CurrentRow.Cells["sales_rate"].Value = "0";// dt_batch.Rows[0]["batch_sales_rate"].ToString();
                            dgvPurchaseBatch.CurrentRow.Cells["Prd_Date"].Value = "";
                            dgvPurchaseBatch.CurrentRow.Cells["Exp_Date"].Value = "";
                            dgvPurchaseBatch.CurrentRow.Cells["rate"].ReadOnly = false;// dt_batch.Rows[0]["batch_rate"].ToString();
                            dgvPurchaseBatch.CurrentRow.Cells["sales_rate"].ReadOnly = false;// dt_batch.Rows[0]["batch_sales_rate"].ToString();
                            dgvPurchaseBatch.CurrentRow.Cells["Prd_Date"].ReadOnly = false;
                            dgvPurchaseBatch.CurrentRow.Cells["Exp_Date"].ReadOnly = false;
                            batch_date_flag = false;
                        }
                    }


                }




                //dgvPurchaseBatch.CurrentRow.Cells["col_Unit"].Value = unit1;
                //if (dgvPurchaseBatch.CurrentCell.OwningColumn.Name == "Branch_No")
                //{
                //    if (dgvPurchaseBatch.Rows[dgvPurchaseBatch.CurrentRow.Index].Cells["Branch_No"].Value != null && dgvPurchaseBatch.Rows[dgvPurchaseBatch.CurrentRow.Index].Cells["Branch_No"].Value.ToString() != "")
                //    {
                //        string unit = ""; decimal unitMf = 0, cost = 0, sales = 0;
                //        string batch = dgvPurchaseBatch.Rows[dgvPurchaseBatch.CurrentRow.Index].Cells["Branch_No"].Value.ToString();    //CurrentRow.Cells["Branch_No"].Value.ToString();
                //        DataTable dt_batch = this.cnt.get_batchrate_exp(itemid1, batch);
                //        if (dt_batch.Rows.Count > 0)//
                //        {
                //            unit = dgvPurchaseBatch.Rows[dgvPurchaseBatch.CurrentRow.Index].Cells["col_Unit"].Value.ToString();
                //            DataTable dtb_item = this.cnt.itemdetails(itemid1);
                //            DataTable dtb = this.cnt.batchdetails(batch, itemid1);
                //            if (dtb_item.Rows.Count > 0)
                //            {
                //                unitMf = Convert.ToDecimal(dtb_item.Rows[0]["UnitMF"].ToString());
                //                if (dtb_item.Rows[0]["OneUnitOnly"].ToString() == "False")
                //                {
                //                    if (dtb.Rows[0]["purch_unit2"].ToString() == "No")//Unit2
                //                    {
                //                        if (unit == dtb_item.Rows[0]["Unit2"].ToString())
                //                        {
                //                            cost = Convert.ToDecimal(dtb.Rows[0]["batch_rate"].ToString()) / unitMf;
                //                            sales = Convert.ToDecimal(dtb.Rows[0]["batch_sales_rate"].ToString()) / unitMf;
                //                        }
                //                        else
                //                        {
                //                            cost = Convert.ToDecimal(dtb.Rows[0]["batch_rate"].ToString());
                //                            sales = Convert.ToDecimal(dtb.Rows[0]["batch_sales_rate"].ToString());
                //                        }
                //                    }
                //                    else
                //                    {
                //                        if (unit == dtb_item.Rows[0]["Unit2"].ToString())
                //                        {

                //                            cost = Convert.ToDecimal(dtb.Rows[0]["batch_rate"].ToString());
                //                            sales = Convert.ToDecimal(dtb.Rows[0]["batch_sales_rate"].ToString());

                //                        }
                //                        else
                //                        {

                //                            cost = unitMf * Convert.ToDecimal(dtb.Rows[0]["batch_rate"].ToString());
                //                            sales = unitMf * Convert.ToDecimal(dtb.Rows[0]["batch_sales_rate"].ToString());
                //                        }
                //                    }
                //                }
                //                else
                //                {
                //                    cost = Convert.ToDecimal(dtb.Rows[0]["batch_rate"].ToString());
                //                    sales = Convert.ToDecimal(dtb.Rows[0]["batch_sales_rate"].ToString());
                //                }
                //            }
                //            dgvPurchaseBatch.CurrentRow.Cells["rate"].Value = cost;// dt_batch.Rows[0]["batch_rate"].ToString();
                //            dgvPurchaseBatch.CurrentRow.Cells["sales_rate"].Value = sales;// dt_batch.Rows[0]["batch_sales_rate"].ToString();
                //            dgvPurchaseBatch.CurrentRow.Cells["Prd_Date"].Value = dt_batch.Rows[0]["PrdDate"].ToString();
                //            dgvPurchaseBatch.CurrentRow.Cells["Exp_Date"].Value = dt_batch.Rows[0]["ExpDate"].ToString();
                //        }
                //    }

                //}
            }
            //if (e.RowIndex >= 0)
            //{
            //    dgvPurchaseBatch.CurrentRow.Cells["col_Unit"].Value = unit1;

            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (dgvPurchaseBatch.Rows.Count > 0)
            {
                this.Close();
            }
        }

        private void dtp_ValueChanged(object sender, EventArgs e)
        {
            dgvPurchaseBatch.CurrentRow.Cells["Prd_Date"].Value = dtp.Text.ToString();
        }
        public bool batch_date_flag = false;
        private void dgvPurchaseBatch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (batch_date_flag == false)
            {
                if (e.ColumnIndex == 5 && e.RowIndex > -1)
                {
                    dtp = new DateTimePicker();
                    dgvPurchaseBatch.Controls.Add(dtp);
                    dtp.Format = DateTimePickerFormat.Short;
                    dtp.MaxDate = DateTime.Today.Date;
                    Rectangle oRectangle = dgvPurchaseBatch.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    dtp.Size = new Size(oRectangle.Width, oRectangle.Height);
                    dtp.Location = new Point(oRectangle.X, oRectangle.Y);
                    dtp.CloseUp += new EventHandler(dtp_CloseUp);
                    dtp.TextChanged += new EventHandler(dtp_ValueChanged);
                    dtp.Visible = true;
                }
                if (e.ColumnIndex == 6 && e.RowIndex > -1)
                {
                    dtpExp = new DateTimePicker();
                    dgvPurchaseBatch.Controls.Add(dtpExp);
                    dtpExp.Format = DateTimePickerFormat.Short;
                    dtpExp.MinDate = DateTime.Today.Date;
                    Rectangle oRectangle = dgvPurchaseBatch.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    dtpExp.Size = new Size(oRectangle.Width, oRectangle.Height);
                    dtpExp.Location = new Point(oRectangle.X, oRectangle.Y);
                    dtpExp.CloseUp += new EventHandler(dtpExp_CloseUp);
                    dtpExp.TextChanged += new EventHandler(dtpExp_ValueChanged);
                    dtpExp.Visible = true;
                }
            }
               
        }

        private void dtp_CloseUp(object sender, EventArgs e)
        {
            dtp.Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPurchaseBatch.Rows.Count > 1)
                {
                    Count = dgvPurchaseBatch.Rows.Count - 1;
                }
                else
                {
                    Count = dgvPurchaseBatch.Rows.Count;
                }
                for (int i = 0; i < Count; i++)
                {
                    if (dgvPurchaseBatch.Rows[i].Cells["Branch_No"].Value == null || dgvPurchaseBatch.Rows[i].Cells["Branch_No"].Value.ToString().Trim() == "")
                    {
                        flagbatchError = true;
                    }
                    else
                    {
                        flagbatchError = false;
                    }
                    if (dgvPurchaseBatch.Rows[i].Cells["Prd_Date"].Value == null)
                    {
                        flagdateError = true;
                    }
                    else
                    {
                        flagdateError = false;
                    }
                }
                int sum = 0;decimal  total_rate = 0;
                if (editflag == true)
                {
                    if (flagbatchError != true && flagdateError != true)
                    {
                        foreach (DataGridViewRow r in dgvPurchaseBatch.Rows)
                        {
                            if (r.Cells["Branch_No"].Value != null && r.Cells["Branch_No"].Value.ToString().Trim() != "" && r.Cells["Prd_Date"].Value != null && r.Cells["Prd_Date"].Value.ToString() != "")
                            {
                                sum += Convert.ToInt32(r.Cells["Quantity"].Value);
                            }
                        }
                        if (qtyE == sum)
                        {
                            flag = 1;
                        }
                        else
                        {
                            flag = 0;
                            ermsg = true;
                        }
                        if (flag == 1)
                        {
                            gridData.Columns.Clear();
                            gridData.Rows.Clear();
                            foreach (DataGridViewColumn col in dgvPurchaseBatch.Columns)
                            {
                                gridData.Columns.Add(col.Name);
                            }
                            foreach (DataGridViewRow row in dgvPurchaseBatch.Rows)
                            {
                                DataRow dRow = gridData.NewRow();
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    dRow[cell.ColumnIndex] = cell.Value;
                                    if (cell.ColumnIndex == 3)
                                        total_rate = total_rate + Convert.ToDecimal(cell.Value);
                                }
                                gridData.Rows.Add(dRow);
                            }
                            if (gridData.Rows.Count > 0)
                            {
                                if(Action=="Stock Updation")
                                {
                                }
                                else
                                {
                                    var frm = new stock_transfer(gridData, total_rate.ToString());
                                    editflag = false;
                                    frm.Closed += (sender1, args) => this.Close();
                                    this.Close();
                                }
                            }
                        }
                        else if (flag == 0)
                        {
                            if (ermsg == true)
                            {
                                MessageBox.Show("the entered quantity is mismatch!.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            ermsg = false;
                        }
                    }
                    else if (flagbatchError == true && flagdateError == true)
                    {
                        MessageBox.Show("Enter the Batch Number and Manufacture Date!.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (flagbatchError == true)
                    {
                        MessageBox.Show("Enter the Batch number!.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (flagdateError == true)
                    {
                        MessageBox.Show("Enter the Manufacture Date!.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (editflag == false)
                {
                    if (flagbatchError != true && flagdateError != true)
                    {
                        foreach (DataGridViewRow r in dgvPurchaseBatch.Rows)
                        {
                            if (r.Cells["Branch_No"].Value != null && r.Cells["Branch_No"].Value.ToString().Trim() != "")
                                if (r.Cells["Prd_Date"].Value != null && r.Cells["Prd_Date"].Value.ToString() != "")
                                {
                                    sum += Convert.ToInt32(r.Cells["Quantity"].Value);
                                }
                        }
                        if (qtyE == sum)
                        {
                            flag = 1;
                        }
                        else
                            flag = 0;
                        ermsg = true;
                        if (flag == 1)
                        {
                            gridData.Columns.Clear();
                            gridData.Rows.Clear();
                            foreach (DataGridViewColumn col in dgvPurchaseBatch.Columns)
                            {
                                gridData.Columns.Add(col.Name);
                            }
                            foreach (DataGridViewRow row in dgvPurchaseBatch.Rows)
                            {
                                DataRow dRow = gridData.NewRow();
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    dRow[cell.ColumnIndex] = cell.Value;
                                    if (cell.ColumnIndex == 3)
                                        total_rate = total_rate + Convert.ToDecimal(cell.Value);
                                }
                                gridData.Rows.Add(dRow);
                            }
                            if (gridData.Rows.Count > 0)
                            {
                                if (Action == "Stock Updation")
                                {
                                }
                                else
                                {
                                    var frm = new stock_transfer(gridData, total_rate,0);
                                    frm.Closed += (sender1, args) => this.Close();
                                    this.Close();
                                }
                            }
                        }
                        else if (flag == 0)
                        {
                            if (ermsg == true)
                            {
                                MessageBox.Show("the entered quantity is mismatch!.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            ermsg = false;
                        }
                    }
                    else if (flagbatchError == true && flagdateError == true)
                    {
                        MessageBox.Show("Enter the Batch Number and Manufacturing Date!.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (flagbatchError == true)
                    {
                        MessageBox.Show("Enter the Batch number!.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (flagdateError == true)
                    {
                        MessageBox.Show("Enter the Manufacture Date!.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public bool loadflag = true;
        private void Stock_in_Batch_Load(object sender, EventArgs e)
        {
            try
            {
                if(loadflag==true)
                {
                    DataTable dtb = this.cntrl.check_batch(itemid1);
                    if (editflag == true)
                    {
                        dgvPurchaseBatch.Rows.Clear();
                        for (int i = 0; i < editgrid1.Rows.Count; i++)
                        {
                            dgvPurchaseBatch.Rows.Add();
                            dgvPurchaseBatch.Rows[i].Cells["Branch_No"].Value = editgrid1.Rows[i]["Branch_No"].ToString();
                            dgvPurchaseBatch.Rows[i].Cells["Quantity"].Value = qtyE;// editgrid1.Rows[i]["col_temp_qty"].ToString();
                            dgvPurchaseBatch.Rows[i].Cells["rate"].Value = editgrid1.Rows[i]["rate"].ToString();//qtyE.ToString();
                            dgvPurchaseBatch.Rows[i].Cells["sales_rate"].Value = editgrid1.Rows[i]["sales_rate"].ToString();
                            dgvPurchaseBatch.Rows[i].Cells["Prd_Date"].Value = editgrid1.Rows[i]["Prd_Date"].ToString();
                            dgvPurchaseBatch.Rows[i].Cells["Exp_Date"].Value = editgrid1.Rows[i]["Exp_Date"].ToString();
                            dgvPurchaseBatch.Rows[i].Cells["col_Unit"].Value = unit1.ToString();
                        }
                    }
                    else
                    {
                        //dgvPurchaseBatch.Rows.Clear();

                        dgvPurchaseBatch.Rows[0].Cells["col_Unit"].Value = unit1.ToString();
                        dgvPurchaseBatch.Rows[0].Cells["Quantity"].Value = qtyE.ToString();
                        dgvPurchaseBatch.Rows[0].Cells["rate"].Value = cost;
                        dgvPurchaseBatch.Rows[0].Cells["sales_rate"].Value = "0";
                    }
                    if (dtb.Rows[0]["ISBatch"].ToString() != "true")
                    {
                        dgvPurchaseBatch.Rows[0].Cells["Branch_No"].Value = purNo + "_" + Itemcode;
                        dgvPurchaseBatch.Columns[0].ReadOnly = true;
                    }
                    else
                    {
                        dgvPurchaseBatch.Columns[0].ReadOnly = false;
                    }
                    dgvPurchaseBatch.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
                    dgvPurchaseBatch.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dgvPurchaseBatch.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 9, FontStyle.Regular);
                    dgvPurchaseBatch.EnableHeadersVisualStyles = false;
                    loadflag = false;
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
