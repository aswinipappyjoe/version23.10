using PappyjoeMVC.Controller;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PappyjoeMVC.View
{
    public partial class PurchaseList : Form
    {
        PurchaseList_controller cntrl=new PurchaseList_controller();
        public static string type = "";
        public PurchaseList()
        {
            InitializeComponent();
        }
        public void setcontroller(PurchaseList_controller controller)
        {
            cntrl = controller;
        }
        public PurchaseList(string date1, string date2)
        {
            InitializeComponent();
            dateTo = date1;
            dateFrom = date2;
            flag_fromInventory = true;
        }
        string check = "cash"; bool flag_fromInventory = false;
        public string dateTo;
        public string dateFrom;
        public void load(string purtype)
        {
            try
            {
                dgv_Purchase.Rows.Clear();
                string fromdate = DTP_From.Value.ToString("yyyy-MM-dd");
                string todate = DTP_To.Value.ToString("yyyy-MM-dd");
                if (Convert.ToDateTime(fromdate).Date > Convert.ToDateTime(todate).Date)
                {
                    MessageBox.Show("From date should be less than To date", "From Date is grater ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DTP_From.Value = DateTime.Today;
                }
                else
                {
                    int slno = 0;
                    DataTable dt = new DataTable();
                  
                    if (cmbSupname.Text!= "All Supplier")
                    {
                        DataTable dt_sup = this.cntrl.get_suppcode(cmbSupname.Text);
                        if(dt_sup.Rows.Count>0)
                        {
                            string code = dt_sup.Rows[0][0].ToString();
                            dt = this.cntrl.getPurchase_btwndates(fromdate, todate, type, code, purtype);
                        }

                    }
                    else
                    {
                        dt = this.cntrl.getPurchase_btwndates(fromdate, todate, type, purtype);

                    }
                    if (dt.Rows.Count != 0)
                    {
                        for (int i = 0; dt.Rows.Count > i; i++)
                        {
                            dgv_Purchase.Rows.Add();
                            slno = i + 1;//InvNumber
                            DataTable dt_due = this.cntrl.get_due_from_voucher(dt.Rows[i]["PurchNumber"].ToString());
                            dgv_Purchase.Rows[i].Cells["colslNo"].Value = slno;
                            dgv_Purchase.Rows[i].Cells["colPurNum"].Value = dt.Rows[i]["PurchNumber"].ToString(); 
                            dgv_Purchase.Rows[i].Cells["voucherno"].Value = dt.Rows[i]["Amount_Status"].ToString();
                            dgv_Purchase.Rows[i].Cells["colPurchDate"].Value = Convert.ToDateTime(dt.Rows[i]["PurchDate"].ToString()).ToString("dd/MM/yyyy");
                            dgv_Purchase.Rows[i].Cells["invno"].Value = dt.Rows[i]["InvNumber"].ToString();
                            dgv_Purchase.Rows[i].Cells["SupplierId"].Value = dt.Rows[i]["Sup_Code"].ToString();
                            dgv_Purchase.Rows[i].Cells["colName"].Value = dt.Rows[i]["Supplier_Name"].ToString();
                            dgv_Purchase.Rows[i].Cells["colTotalAmount"].Value =Convert.ToDecimal( dt.Rows[i]["GrandTotal"].ToString()).ToString("0.00");
                            dgv_Purchase.Rows[i].Cells["colPayment"].Value = dt.Rows[i]["PurchType"].ToString();
                            if (dt.Rows[i]["PurchType"].ToString() == "Credit")
                            {
                                dgv_Purchase.Rows[i].Cells["due"].Value= Convert.ToDecimal(dt.Rows[i]["GrandTotal"].ToString()).ToString("0.00");

                                dgv_Purchase.Rows[i].Cells["voucherno"].Value = "Pending";
                            }
                            else
                            {
                                if (dt_due.Rows.Count > 0)
                                {
                                    dgv_Purchase.Rows[i].Cells["due"].Value = dt_due.Rows[0]["Partial_amount"].ToString();
                                }
                                else
                                    dgv_Purchase.Rows[i].Cells["due"].Value = "0.00";
                            }
                            
                            if (dt.Rows[i]["Amount_Status"].ToString() == "Completed")
                            {
                                dgv_Purchase.Rows[i].DefaultCellStyle.BackColor = Color.Coral;
                                dgv_Purchase.Rows[i].Cells["due"].Value = "0.00";
                            }
                            dgv_Purchase.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            
                        }
                        Lab_Msg.Visible = false; 
                        dgv_Purchase.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                    else
                    {
                        int x = (panel2.Size.Width - Lab_Msg.Size.Width) / 2;
                        Lab_Msg.Location = new Point(x, Lab_Msg.Location.Y);
                        Lab_Msg.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void loadSup()
        {
            cmbSupname.Items.Clear();
            cmbSupname.Items.Add("All Supplier");
            cmbSupname.ValueMember = "0";
            cmbSupname.DisplayMember = "All Supplier";
            System.Data.DataTable sup = this.cntrl.supname();
            if (sup.Rows.Count > 0)
            {
                foreach (DataRow dr in sup.Rows)
                {
                    cmbSupname.Items.Add(dr["Supplier_Name"].ToString());
                    cmbSupname.ValueMember = dr["Supplier_Code"].ToString();
                    cmbSupname.DisplayMember = dr["Supplier_Name"].ToString();
                }
            }
            cmbSupname.SelectedIndex = 0;
        }
        private void frmPurchaseList_Load(object sender, EventArgs e)
        {
            try
            {
                loadSup();
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
                if (flag_fromInventory == true)
                {
                    DTP_From.Value = Convert.ToDateTime(dateTo);
                    DTP_To.Value = Convert.ToDateTime(dateFrom);
                    int slno = 0;
                    DataTable dt = this.cntrl.getPurchase_btwndates(Convert.ToDateTime(dateTo).ToString("yyyy-MM-dd"), Convert.ToDateTime(dateFrom).ToString("yyyy-MM-dd"), type,rad_Credit.Text);
                    if (dt.Rows.Count != 0)
                    {//InvNumber
                     // if()
                        for (int i = 0; dt.Rows.Count > i; i++)
                        {
                            dgv_Purchase.Rows.Add();
                            slno = i + 1;
                            dgv_Purchase.Rows[i].Cells["colslNo"].Value = slno;
                            dgv_Purchase.Rows[i].Cells["voucherno"].Value = "Pending";// dt.Rows[i]["Amount_Status"].ToString();
                            dgv_Purchase.Rows[i].Cells["colPurNum"].Value = dt.Rows[i]["PurchNumber"].ToString();
                            dgv_Purchase.Rows[i].Cells["colPurchDate"].Value = Convert.ToDateTime(dt.Rows[i]["PurchDate"].ToString()).ToString("dd/MM/yyyy");
                            dgv_Purchase.Rows[i].Cells["invno"].Value = dt.Rows[i]["InvNumber"].ToString();
                            dgv_Purchase.Rows[i].Cells["SupplierId"].Value = dt.Rows[i]["Sup_Code"].ToString();
                            dgv_Purchase.Rows[i].Cells["colName"].Value = dt.Rows[i]["Supplier_Name"].ToString();
                            dgv_Purchase.Rows[i].Cells["colTotalAmount"].Value =Convert.ToDecimal( dt.Rows[i]["GrandTotal"].ToString()).ToString("0.00");
                            dgv_Purchase.Rows[i].Cells["colPayment"].Value = dt.Rows[i]["PurchType"].ToString();
                            if (dt.Rows[i]["PurchType"].ToString() == "Credit")
                            {
                                dgv_Purchase.Rows[i].Cells["due"].Value = Convert.ToDecimal(dt.Rows[i]["GrandTotal"].ToString()).ToString("0.00");
                            }
                            //else
                           if (dt.Rows[i]["Amount_Status"].ToString()=="Completed")
                            {
                                dgv_Purchase.Rows[i].DefaultCellStyle.BackColor = Color.Coral;
                               
                            }
                            dgv_Purchase.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


                        }
                        Lab_Msg.Visible = false;
                        dgv_Purchase.ColumnHeadersDefaultCellStyle.Alignment= DataGridViewContentAlignment.MiddleRight;
                    }
                }
                else
                {
                    load(rad_Credit.Text);
                }
                dgv_Purchase.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
                dgv_Purchase.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgv_Purchase.EnableHeadersVisualStyles = false;
                dgv_Purchase.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv_Purchase.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                PappyjoeMVC.Model.Connection.MyGlobals.Date_From = DTP_From.Value.ToString("yyyy-MM-dd");
                PappyjoeMVC.Model.Connection.MyGlobals.Date_To = DTP_To.Value.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void rad_Cash_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_Cash.Checked == true)
            {
                rad_Credit.Checked = false;
                check = "cash";
            }
            else
            {
                rad_Credit.Checked = true;
                rad_Cash.Checked = false;
                check = "credit";
            }
            load(rad_Cash.Text);
        }
        private void rad_Credit_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_Credit.Checked == true)
            {
                rad_Cash.Checked = false;
                check = "credit";
            }
            else
            {
                rad_Cash.Checked = true;
                rad_Credit.Checked = false;
                check = "cash";
            }
            load(rad_Credit.Text);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_Purchase_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PappyjoeMVC.Model.Connection.MyGlobals.global_Flag = true;
            if(rad_Credit.Checked==true)
            {
                DataTable data_from_Pur_Master = this.cntrl.data_from_Pur_Master(dgv_Purchase.CurrentRow.Cells["colPurNum"].Value);
                DataTable data_from_purchase = this.cntrl.data_from_purchase(dgv_Purchase.CurrentRow.Cells["colPurNum"].Value);
                var form2 = new PappyjoeMVC.View.Purchase(data_from_Pur_Master, data_from_purchase);
                form2.credit_flag = true;
                form2.ShowDialog();
            }
            else
            {
                DataTable data_from_Pur_Master = this.cntrl.data_from_Pur_Master(dgv_Purchase.CurrentRow.Cells["colPurNum"].Value);
                DataTable data_from_purchase = this.cntrl.data_from_purchase(dgv_Purchase.CurrentRow.Cells["colPurNum"].Value);
                var form2 = new PappyjoeMVC.View.Purchase(data_from_Pur_Master, data_from_purchase);
                form2.credit_flag = false;
                form2.ShowDialog();
            }
           
        }
        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            //load();
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            PappyjoeMVC.Model.Connection.MyGlobals.Date_From = DTP_From.Value.ToString("yyyy-MM-dd");
            PappyjoeMVC.Model.Connection.MyGlobals.Date_To = DTP_To.Value.ToString("yyyy-MM-dd");
            if(rad_Credit.Checked==true)
            {
                load(rad_Credit.Text);

            }
            else
            {
                load(rad_Cash.Text);
            }
            
        }
        private void btnexport_Click(object sender, EventArgs e)
        {
            string PathName = "";
            string fromdate = DTP_From.Value.ToString("yyyy-MM-dd");
            string todate = DTP_To.Value.ToString("yyyy-MM-dd");
            string[] strarray;
            strarray = new string[] { "Date", "Particulars", "Supplier", "Address", "Voucher Type", "Vch No.", "Quantity", "Rate", "Gross Total(Include VAT)" };
            int[] intarray;
            intarray = new int[] { 10, 25, 25, 25, 14, 10, 8, 8, 25 };
            if (Convert.ToDateTime(fromdate).Date > Convert.ToDateTime(todate).Date)
            {
                MessageBox.Show("From date should be less than To date", "From Date is grater ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DTP_From.Value = DateTime.Today;
            }
            else
            {
                DataTable dt = this.cntrl.dt(fromdate,todate);
                if (dt.Rows.Count != 0)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Excel Files (*.xls)|*.xls";
                    saveFileDialog1.FileName = "Purchase Report(" + DateTime.Now.ToString("dd-MM-yy h.mm.ss tt") + ").xls";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        PathName = saveFileDialog1.FileName;
                        Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                        ExcelApp.Application.Workbooks.Add(Type.Missing);
                        ExcelApp.Columns.ColumnWidth = 20;
                        int count = dt.Columns.Count;
                        ExcelApp.Range[ExcelApp.Cells[1, 1], ExcelApp.Cells[1, 15]].Merge();
                        ExcelApp.Cells[1, 1] = "Purchase Register";
                        ExcelApp.Cells[1, 1].HorizontalAlignment = HorizontalAlignment.Center;
                        ExcelApp.Cells[1, 1].Font.Size = 12;
                        ExcelApp.Cells[1, 1].Interior.Color = Color.FromArgb(153, 204, 255);
                        ExcelApp.Columns.ColumnWidth = 20;
                        ExcelApp.Cells[2, 1] = "From Date";
                        ExcelApp.Cells[2, 1].Font.Size = 10;
                        ExcelApp.Cells[3, 1] = "To Date";
                        ExcelApp.Cells[3, 1].Font.Size = 10;
                        ExcelApp.Cells[2, 2] = DTP_From.Value.ToString("dd-MM-yyyy");
                        ExcelApp.Cells[2, 2].Font.Size = 10;
                        ExcelApp.Cells[3, 2] = DTP_To.Value.ToString("dd-MM-yyyy");
                        ExcelApp.Cells[3, 2].Font.Size = 10;
                        ExcelApp.Cells[4, 1] = "Running Date";
                        ExcelApp.Cells[4, 1].Font.Size = 10;
                        ExcelApp.Cells[4, 2] = DateTime.Now.ToString("dd-MM-yyyy");
                        ExcelApp.Cells[4, 2].Font.Size = 10;
                        ExcelApp.Cells[4, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        ExcelApp.Cells[3, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        ExcelApp.Cells[2, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        for (int i = 0; i < strarray.Length; i++)
                        {
                            ExcelApp.Range[ExcelApp.Cells[5, i + 1], ExcelApp.Cells[6, i + 1]].Merge();
                            ExcelApp.Cells[5, i + 1] = strarray[i];
                            ExcelApp.Cells[5, i + 1].ColumnWidth = intarray[i];
                            ExcelApp.Cells[5, i + 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            ExcelApp.Cells[5, i + 1].VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            ExcelApp.Cells[5, i + 1].EntireRow.Font.Bold = true;
                            ExcelApp.Cells[5, i + 1].Interior.Color = Color.FromArgb(0, 102, 204);
                            ExcelApp.Cells[5, i + 1].Font.Size = 10;
                            ExcelApp.Cells[5, i + 1].Font.Name = "Arial";
                            ExcelApp.Cells[5, i + 1].Font.Color = Color.FromArgb(255, 255, 255);
                            ExcelApp.Cells[5, i + 1].Interior.Color = Color.FromArgb(0, 102, 204);
                            ExcelApp.Range[ExcelApp.Cells[5, i + 1], ExcelApp.Cells[6, i + 1]].BorderAround(true);
                            ExcelApp.Range[ExcelApp.Cells[5, i + 1], ExcelApp.Cells[6, i + 1]].Borders.Color = Color.FromArgb(0, 0, 0);
                        }
                        int row = 7;
                        double totalamount = 0;
                        for (int i = 0; dt.Rows.Count > i; i++)
                        {
                            DataTable data_from_purchase = this.cntrl.data_from_purchase(dt.Rows[i]["PurchNumber"].ToString());
                            if (data_from_purchase.Rows.Count > 0)
                            {
                                for (int j = 0; data_from_purchase.Rows.Count > j; j++)
                                {
                                    ExcelApp.Cells[row, 1] = Convert.ToDateTime(data_from_purchase.Rows[j]["PurchDate"].ToString()).ToString("dd/MM/yyy");
                                    ExcelApp.Cells[row, 1].BorderAround(true);
                                    ExcelApp.Cells[row, 1].Borders.Color = Color.FromArgb(0, 0, 0);
                                    ExcelApp.Cells[row, 1].Font.Size = 9;
                                    ExcelApp.Cells[row, 2] = data_from_purchase.Rows[j]["Desccription"].ToString();
                                    ExcelApp.Cells[row, 2].BorderAround(true);
                                    ExcelApp.Cells[row, 2].Borders.Color = Color.FromArgb(0, 0, 0);
                                    ExcelApp.Cells[row, 2].Font.Size = 9;
                                    ExcelApp.Cells[row, 3] = dt.Rows[i]["Supplier_Name"].ToString();
                                    ExcelApp.Cells[row, 3].BorderAround(true);
                                    ExcelApp.Cells[row, 3].Borders.Color = Color.FromArgb(0, 0, 0);
                                    ExcelApp.Cells[row, 3].Font.Size = 9;
                                    ExcelApp.Cells[row, 4] = dt.Rows[i]["Address1"].ToString();
                                    ExcelApp.Cells[row, 4].BorderAround(true);
                                    ExcelApp.Cells[row, 4].Borders.Color = Color.FromArgb(0, 0, 0);
                                    ExcelApp.Cells[row, 4].Font.Size = 9;
                                    ExcelApp.Cells[row, 5] = "Purchase";
                                    ExcelApp.Cells[row, 5].BorderAround(true);
                                    ExcelApp.Cells[row, 5].Borders.Color = Color.FromArgb(0, 0, 0);
                                    ExcelApp.Cells[row, 5].Font.Size = 9;
                                    ExcelApp.Cells[row, 6] = dt.Rows[i]["PurchNumber"].ToString();
                                    ExcelApp.Cells[row, 6].BorderAround(true);
                                    ExcelApp.Cells[row, 6].Borders.Color = Color.FromArgb(0, 0, 0);
                                    ExcelApp.Cells[row, 6].Font.Size = 9;
                                    ExcelApp.Cells[row, 7] = data_from_purchase.Rows[j]["Qty"].ToString();
                                    ExcelApp.Cells[row, 7].BorderAround(true);
                                    ExcelApp.Cells[row, 7].Borders.Color = Color.FromArgb(0, 0, 0);
                                    ExcelApp.Cells[row, 7].Font.Size = 9;
                                    ExcelApp.Cells[row, 8] = data_from_purchase.Rows[j]["Rate"].ToString();
                                    ExcelApp.Cells[row, 8].BorderAround(true);
                                    ExcelApp.Cells[row, 8].Borders.Color = Color.FromArgb(0, 0, 0);
                                    ExcelApp.Cells[row, 8].Font.Size = 9;
                                    ExcelApp.Cells[row, 9] = data_from_purchase.Rows[j]["Amount"].ToString();
                                    ExcelApp.Cells[row, 9].BorderAround(true);
                                    ExcelApp.Cells[row, 9].Borders.Color = Color.FromArgb(0, 0, 0);
                                    ExcelApp.Cells[row, 9].Font.Size = 9;
                                    totalamount = totalamount + Convert.ToDouble(data_from_purchase.Rows[j]["Amount"].ToString());
                                    row = row + 1;
                                }
                            }
                            data_from_purchase.Clear();
                        }
                        for (int i = 1; i < 10; i++)
                        {
                            ExcelApp.Cells[row, i].BorderAround(true);
                            ExcelApp.Cells[row, i].Font.Size = 10;
                            ExcelApp.Cells[row, i].EntireRow.Font.Bold = true;
                        }
                        ExcelApp.Cells[row, 9] = totalamount;
                        Lab_Msg.Visible = false;
                        ExcelApp.ActiveWorkbook.SaveAs(PathName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                        ExcelApp.ActiveWorkbook.Saved = true;
                        ExcelApp.Quit();
                        MessageBox.Show("Successfully Exported to Excel", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    int x = (panel2.Size.Width - Lab_Msg.Size.Width) / 2;
                    Lab_Msg.Location = new Point(x, Lab_Msg.Location.Y);
                    Lab_Msg.Visible = true;
                }
            }
        }
        private void dgv_Purchase_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                PappyjoeMVC.Model.Connection.MyGlobals.global_Flag = true;
                if (dgv_Purchase.CurrentCell.OwningColumn.Name == "colmore")
                {
                    if(rad_Credit.Checked==true)
                    {
                        DataTable data_from_Pur_Master = this.cntrl.data_from_Pur_Master(dgv_Purchase.CurrentRow.Cells["colPurNum"].Value);
                        DataTable data_from_purchase = this.cntrl.data_from_purchase(dgv_Purchase.CurrentRow.Cells["colPurNum"].Value.ToString());
                        var form2 = new Purchase(data_from_Pur_Master, data_from_purchase);
                        form2.credit_flag = true;
                        form2.ShowDialog();
                    }
                  
                }
            }
        }

        private void btn_voucher_Click(object sender, EventArgs e)
        {
            if(dgv_Purchase.Rows.Count>0)
            {
                string purno = "";
                purno = dgv_Purchase.CurrentRow.Cells["colPurNum"].Value.ToString();
                if(purno !="")
                {
                    if (dgv_Purchase.CurrentRow.Cells["voucherno"].Value.ToString() != "Completed")
                    {
                        var form2 = new Supplier_invoice();
                        form2.purno = purno;
                        form2.purch_list_flag = true;
                        form2.supname = dgv_Purchase.CurrentRow.Cells["colName"].Value.ToString();
                        form2.amount = Convert.ToDecimal(dgv_Purchase.CurrentRow.Cells["colTotalAmount"].Value.ToString());
                        form2.supcode = dgv_Purchase.CurrentRow.Cells["SupplierId"].Value.ToString();
                        form2.ShowDialog();
                        BtnShow_Click(null,null);
                    }
                    else
                    {
                        MessageBox.Show("This Purchase bill is closed !..","Completed",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                   
                }
            }
        }
    }
}
