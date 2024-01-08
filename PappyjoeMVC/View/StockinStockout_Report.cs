using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PappyjoeMVC.Controller;

namespace PappyjoeMVC.View
{
    public partial class StockTransfer_Report : Form
    {
        stock_transfer_controller cntrl = new stock_transfer_controller();
        public string  strclinicname = "", clinicn = "", strStreet = "", stremail = "", strwebsite = "", strphone = "", checkStr = "0", PathName = "", logo_name = "";

        private void btnEXPORT_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_Sockdetails.Rows.Count != 0)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Excel Files (*.xls)|*.xls";
                    saveFileDialog1.FileName = "Stock Transfer Report(" + DateTime.Now.ToString("dd-MM-yy h.mm.ss tt") + ").xls";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        PathName = saveFileDialog1.FileName;
                        Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                        ExcelApp.Application.Workbooks.Add(Type.Missing);
                        ExcelApp.Columns.ColumnWidth = 20;
                        int count = dgv_Sockdetails.Columns.Count;

                        ExcelApp.Range[ExcelApp.Cells[1, 1], ExcelApp.Cells[1, count]].Merge();
                        if(cmb_stckINorOUT.Text== "Stock Out")
                            ExcelApp.Cells[1, 1] = "STOCK OUT REPORT";
                        else
                            ExcelApp.Cells[1, 1] = "STOCK IN REPORT";
                        ExcelApp.Cells[1, 1].HorizontalAlignment = HorizontalAlignment.Center;

                        ExcelApp.Cells[1, 1].Font.Size = 12;
                        ExcelApp.Cells[1, 1].Interior.Color = Color.FromArgb(153, 204, 255);
                        ExcelApp.Columns.ColumnWidth = 20;
                        ExcelApp.Cells[2, 1] = "From Date";
                        ExcelApp.Cells[2, 1].Font.Size = 10;
                        ExcelApp.Cells[2, 2] = dateFrom.Value.ToString("dd-MM-yyyy");
                        ExcelApp.Cells[2, 2].Font.Size = 10;
                        ExcelApp.Cells[3, 1] = "To Date";
                        ExcelApp.Cells[3, 1].Font.Size = 10;
                        ExcelApp.Cells[3, 2] = dateTo.Value.ToString("dd-MM-yyyy");
                        ExcelApp.Cells[3, 2].Font.Size = 10;
                        ExcelApp.Cells[4, 1] = "Running Date";
                        ExcelApp.Cells[4, 1].Font.Size = 10;
                        ExcelApp.Cells[4, 2] = DateTime.Now.ToString("dd-MM-yyyy");
                        ExcelApp.Cells[4, 2].Font.Size = 10;
                        ExcelApp.Cells[4, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        ExcelApp.Cells[3, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        ExcelApp.Cells[2, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        for (int i = 1; i < dgv_Sockdetails.Columns.Count + 1; i++)
                        {
                            ExcelApp.Cells[5, i] = dgv_Sockdetails.Columns[i - 1].HeaderText;
                            ExcelApp.Cells[5, i].ColumnWidth = 25;
                            ExcelApp.Cells[5, i].EntireRow.Font.Bold = true;
                            ExcelApp.Cells[5, i].Interior.Color = Color.FromArgb(0, 102, 204);
                            ExcelApp.Cells[5, i].Font.Size = 10;
                            ExcelApp.Cells[5, i].Font.Name = "Arial";
                            ExcelApp.Cells[5, i].Font.Color = Color.FromArgb(255, 255, 255);
                            ExcelApp.Cells[5, i].Interior.Color = Color.FromArgb(0, 102, 204);
                        }
                        for (int i = 0; i <= dgv_Sockdetails.Rows.Count; i++)
                        {
                            try
                            {
                                for (int j = 0; j < dgv_Sockdetails.Columns.Count; j++)
                                {
                                    ExcelApp.Cells[i + 6, j + 1] = dgv_Sockdetails.Rows[i].Cells[j].Value.ToString();
                                    ExcelApp.Cells[i + 6, j + 1].BorderAround(true);
                                    ExcelApp.Cells[i + 6, j + 1].Borders.Color = Color.FromArgb(0, 102, 204);
                                    ExcelApp.Cells[i + 6, j + 1].Font.Size = 8;
                                }
                            }
                            catch { }
                        }
                        ExcelApp.ActiveWorkbook.SaveAs(PathName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                        ExcelApp.ActiveWorkbook.Saved = true;
                        ExcelApp.Quit();
                        checkStr = "1";
                        MessageBox.Show("Successfully Exported to Excel", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                    MessageBox.Show("No records found, Please change the date and try again!..", "No Records Found ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void cmb_stckINorOUT_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (cmb_stckINorOUT.Text == "Stock Out")
            {
                dt = this.cntrl.get_stockout_details(dateFrom.Value.ToString("yyyy-MM-dd"), dateTo.Value.ToString("yyyy-MM-dd"));
                Load_Grid_StockOut(dt);
            }
            else if (cmb_stckINorOUT.Text == "Stock In")
            {
                dt = this.cntrl.get_stockin_details(dateFrom.Value.ToString("yyyy-MM-dd"), dateTo.Value.ToString("yyyy-MM-dd"));
                Load_Grid_StockIn(dt);
            }
        }

        public StockTransfer_Report()
        {
            InitializeComponent();
        }

        private void StockTransfer_Report_Load(object sender, EventArgs e)
        {
            //string datefrom = dateFrom.ToString();
            //string dateto = dateTo.ToString();
            cmb_stckINorOUT.Text = "Stock In";
            DataTable dt = this.cntrl.get_stockin_details(dateFrom.Value.ToString("yyyy-MM-dd"), dateTo.Value.ToString("yyyy-MM-dd"));
            Load_Grid_StockIn(dt);
            dgv_Sockdetails.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            dgv_Sockdetails.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv_Sockdetails.EnableHeadersVisualStyles = false;
            dgv_Sockdetails.ColumnHeadersDefaultCellStyle.Font = new Font("Sego UI", 9, FontStyle.Regular);
            dgv_Sockdetails.Columns["SlNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_Sockdetails.Columns["date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_Sockdetails.Columns["itemcode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv_Sockdetails.Columns["itemname"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv_Sockdetails.Columns["labname"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv_Sockdetails.Columns["batch"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_Sockdetails.Columns["stockinout"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_Sockdetails.Columns["unit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_Sockdetails.Columns["gst"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_Sockdetails.Columns["rate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_Sockdetails.Columns["currentstock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_Sockdetails.Columns["totalamount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            foreach (DataGridViewColumn cl in dgv_Sockdetails.Columns)
            {
                cl.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public void Load_Grid_StockOut(DataTable dt)
        {
            dgv_Sockdetails.Rows.Clear();
            dgv_Sockdetails.Columns["stockinout"].HeaderText = "STOCK OUT QTY";
            dgv_Sockdetails.Columns["labname"].HeaderText = "TO";
            int slno = 1; string itemname = ""; string batch = ""; string GST = ""; string Itemcode = ""; string date = ""; string Quantity = "";string unit = ""; string rate = ""; string Stock = "";
            decimal totalAmount = 0, totalCost = 0, Amount = 0, cost = 0, total = 0;
            if (dt.Rows.Count>0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable itemDetails = this.cntrl.get_itemcode_stock_out(dt.Rows[i]["id"].ToString());
                    if (itemDetails.Rows.Count>0)
                    {
                        for (int j = 0; j < itemDetails.Rows.Count; j++)
                        {
                            itemname = itemDetails.Rows[j]["ItemName"].ToString();
                            batch = itemDetails.Rows[j]["Batch"].ToString();
                            Quantity =Convert.ToDecimal( itemDetails.Rows[j]["qty"].ToString()).ToString("##0.00");
                            unit = itemDetails.Rows[j]["Unit"].ToString();
                            rate = itemDetails.Rows[j]["Cost"].ToString();

                            //DataTable gst = this.cntrl.get_pur_gst(itemDetails.Rows[j]["ItemId"].ToString());
                            //if (gst.Rows.Count > 0)
                            //{
                            //    GST = gst.Rows[0]["GST"].ToString();
                            //}
                            DataTable itemcode = this.cntrl.get_itemcode_frm_items(itemDetails.Rows[j]["ItemId"].ToString());
                            if (itemcode.Rows.Count > 0)
                            {
                                Itemcode = itemcode.Rows[0]["item_code"].ToString();
                                GST = itemcode.Rows[0]["GstVat"].ToString();
                            }
                            DataTable currentStock = this.cntrl.get_stock_of_items(itemDetails.Rows[j]["ItemId"].ToString());
                            if (currentStock.Rows.Count > 0)
                            {
                                Stock = currentStock.Rows[0]["stock"].ToString();
                            }
                            cost = Convert.ToDecimal(Quantity) * Convert.ToDecimal(rate);
                            Amount = cost * Convert.ToDecimal(GST) / 100;
                            total = Amount + cost;
                            totalCost = totalCost + Convert.ToDecimal(rate);
                            totalAmount = totalAmount + Convert.ToDecimal(total);

                            //totalCost = totalCost + Convert.ToDecimal(rate);
                            //totalAmount = totalAmount + Convert.ToDecimal(dt.Rows[i]["TotalAmount"].ToString());
                            dgv_Sockdetails.Rows.Add(slno, Convert.ToDateTime(dt.Rows[i]["stock_date"].ToString()).ToString("dd/MM/yyyy"), Itemcode, itemname, dt.Rows[i]["Name"].ToString(), batch, Quantity, unit,Convert.ToDecimal( GST).ToString("##0.00"),Convert.ToDecimal( rate).ToString("##0.00"), Stock,Convert.ToDecimal(total).ToString("##0.00"));
                            slno = slno + 1;
                        }
                        
                    }
                    
                }
                Lab_AmountPaid.Text = totalCost.ToString("##0.00");
                label2.Text = totalAmount.ToString("##0.00");
            }
        }
        public void Load_Grid_StockIn(DataTable dt)
        {
            dgv_Sockdetails.Rows.Clear();
            dgv_Sockdetails.Columns["stockinout"].HeaderText = "STOCK IN QTY";
            dgv_Sockdetails.Columns["labname"].HeaderText = "FROM";
            int slno = 1; string itemname = ""; string batch = ""; string GST = "0"; string Itemcode = ""; string date = ""; string Quantity = ""; string unit = ""; string rate = ""; string Stock = "";
            decimal totalAmount = 0, totalCost = 0,Amount=0,cost=0,total=0;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable itemDetails = this.cntrl.get_itemcode_stock_in(dt.Rows[i]["id"].ToString());
                    if (itemDetails.Rows.Count > 0)
                    {
                        for (int j = 0; j < itemDetails.Rows.Count; j++)
                        {
                            itemname = itemDetails.Rows[j]["ItemName"].ToString();
                            batch = itemDetails.Rows[j]["Batch"].ToString();
                            Quantity = itemDetails.Rows[j]["qty"].ToString();
                            unit = itemDetails.Rows[j]["Unit"].ToString();
                            rate = itemDetails.Rows[j]["Cost"].ToString();
                            //DataTable gst = this.cntrl.get_pur_gst(itemDetails.Rows[0]["ItemId"].ToString());
                            //if (gst.Rows.Count > 0)
                            //{
                            //    GST = gst.Rows[0]["GST"].ToString();
                            //}
                            DataTable itemcode = this.cntrl.get_itemcode_frm_items(itemDetails.Rows[j]["ItemId"].ToString());
                            if (itemcode.Rows.Count > 0)
                            {
                                Itemcode = itemcode.Rows[0]["item_code"].ToString();
                                GST = itemcode.Rows[0]["GstVat"].ToString();
                            }
                            DataTable currentStock = this.cntrl.get_stock_of_items(itemDetails.Rows[j]["ItemId"].ToString());
                            if (currentStock.Rows.Count > 0)
                            {
                                Stock = currentStock.Rows[0]["stock"].ToString();
                            }
                            cost = Convert.ToDecimal(Quantity) * Convert.ToDecimal(rate);
                            Amount = cost * Convert.ToDecimal(GST) / 100;
                            total = Amount + cost;
                            totalCost = totalCost + Convert.ToDecimal(rate);
                            totalAmount = totalAmount + Convert.ToDecimal(total);
                            dgv_Sockdetails.Rows.Add(slno, Convert.ToDateTime(dt.Rows[i]["stock_date"].ToString()).ToString("dd/MM/yyyy"), Itemcode, itemname, dt.Rows[i]["Name"].ToString(), batch, Convert.ToDecimal(Quantity).ToString("##0.00"), unit, Convert.ToDecimal(GST).ToString("##0.00"), Convert.ToDecimal(rate).ToString("##0.00"), Convert.ToDecimal(Stock).ToString("##0.00"), Convert.ToDecimal(total).ToString("##0.00"));
                            slno = slno + 1;
                        }
                        
                    }
                    //totalCost = totalCost + Convert.ToDecimal(rate);
                    //totalAmount = totalAmount + Convert.ToDecimal(total);
                }
                Lab_AmountPaid.Text = totalCost.ToString("##0.00");
                label2.Text= totalAmount.ToString("##0.00");
            }
        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (cmb_stckINorOUT.Text == "Stock Out")
            {
                dt = this.cntrl.get_stockout_details(dateFrom.Value.ToString("yyyy-MM-dd"), dateTo.Value.ToString("yyyy-MM-dd"));
                Load_Grid_StockOut(dt);
            }
            else if (cmb_stckINorOUT.Text == "Stock In")
            {
                dt = this.cntrl.get_stockin_details(dateFrom.Value.ToString("yyyy-MM-dd"), dateTo.Value.ToString("yyyy-MM-dd"));
                Load_Grid_StockIn(dt);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_Sockdetails.Rows.Count > 0)
                {
                    string frdate = dateFrom.Value.Day.ToString();
                    string frmonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateFrom.Value.Month);
                    string fryear = dateFrom.Value.Year.ToString();
                    string todate = dateTo.Value.Day.ToString();
                    string tomonth = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTo.Value.Month);
                    string toyear = dateTo.Value.Year.ToString();
                    string today = DateTime.Now.ToString("dd/MM/yyyy");
                    string message = "Did you want Header on Print?";
                    string caption = "Verification";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        DataTable dtp = this.cntrl.practicedetails();
                        if (dtp.Rows.Count > 0)
                        {
                            clinicn = dtp.Rows[0]["name"].ToString();
                            strclinicname = clinicn.Replace("¤", "'");
                            strphone = dtp.Rows[0]["contact_no"].ToString();
                            strStreet = dtp.Rows[0]["street_address"].ToString();
                            stremail = dtp.Rows[0]["email"].ToString();
                            strwebsite = dtp.Rows[0]["website"].ToString();
                            logo_name = dtp.Rows[0]["path"].ToString();
                        }
                    }
                    string Apppath = System.IO.Directory.GetCurrentDirectory();
                    System.IO.StreamWriter sWrite = new System.IO.StreamWriter(Apppath + "\\SalesReport.html");
                    sWrite.WriteLine("<html>");
                    sWrite.WriteLine("<head>");
                    sWrite.WriteLine("<style>");
                    sWrite.WriteLine("table { border-collapse: collapse;}");
                    sWrite.WriteLine("p.big {line-height: 400%;}");
                    sWrite.WriteLine("</style>");
                    sWrite.WriteLine("</head>");
                    sWrite.WriteLine("<body >");
                    sWrite.WriteLine("<div>");
                    sWrite.WriteLine("<table align=center width=900 >");
                    sWrite.WriteLine("<col >");
                    sWrite.WriteLine("<br>");
                    string Appath = System.IO.Directory.GetCurrentDirectory();
                    if (File.Exists(Appath + "\\" + logo_name))
                    {
                        sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td width='30px' height='50px' align='left' rowspan='3'><img src='" + Appath + "\\" + logo_name + "'style='width:70px;height:70px;' ></td>  ");
                        sWrite.WriteLine("<td width='870px' align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=4><b>&nbsp;" + strclinicname + "</font> <br><FONT  COLOR=black  face='Segoe UI' SIZE=2>&nbsp;" + strStreet + "<br>&nbsp;" + strphone + " </b></td></tr>");
                        sWrite.WriteLine("</table>");
                    }
                    else
                    {
                        sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td  align='left' height='20px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + strclinicname + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td  align='left' height='20px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;" + strStreet + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td align='left' height='20px' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + strphone + "</font></td></tr>");

                        sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");

                        sWrite.WriteLine("</table>");
                    }
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    //sWrite.WriteLine("<tr><td align='left'  ><hr/></td></tr>");
                    sWrite.WriteLine("</table>");
                    if(cmb_stckINorOUT.Text== "Stock Out")
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3> STOCK OUT REPORT </font></center></b></td>");
                    }
                    else
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3> STOCK IN REPORT </font></center></b></td>");
                    }
                   
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=7 align=left><FONT COLOR=black FACE='Segoe UI' SIZE=2>  " + "<b>From :</b>" + " " + dateFrom.Value.ToString("dd/MM/yyyy") + " </font></left></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=7 align=left><FONT COLOR=black FACE='Segoe UI' SIZE=2>  " + "<b>To :</b>" + "   " + dateTo.Value.ToString("dd/MM/yyyy") + "</font></left></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' colspan=7><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>Printed Date:</b>" + " " + today + "" + "</font><left></td>");
                    sWrite.WriteLine("</tr>");
                    if (dgv_Sockdetails.Rows.Count > 0)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("    <td align='left' width='6%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3 ><b>&nbsp;Slno.</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='17%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> <b>Date</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='16%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Item Code</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Item Name</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Lab Name</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Batch</b></font></th>");
                        sWrite.WriteLine("    <td align='right' width='15%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b> Stock Qty</b></font></th>");
                        sWrite.WriteLine("    <td align='right' width='15%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Unit</b></font></th>");
                        sWrite.WriteLine("    <td align='right' width='25%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> <b>GST</b></font></th>");
                        sWrite.WriteLine("    <td align='right' width='25%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> <b>Rate</b></font></th>");
                        sWrite.WriteLine("    <td align='right' width='25%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> <b>Current Stock</b></font></th>");
                        sWrite.WriteLine("    <td align='right' width='25%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> <b>Total Amount</b></font></th>");
                        sWrite.WriteLine("</tr>");
                        for (int c = 0; c < dgv_Sockdetails.Rows.Count; c++)
                        {
                            sWrite.WriteLine("<tr>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgv_Sockdetails.Rows[c].Cells["SlNo"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgv_Sockdetails.Rows[c].Cells["date"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgv_Sockdetails.Rows[c].Cells["itemcode"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgv_Sockdetails.Rows[c].Cells["itemname"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgv_Sockdetails.Rows[c].Cells["labname"].Value.ToString() + "</font></th>");
                                sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgv_Sockdetails.Rows[c].Cells["batch"].Value.ToString() + "</font></th>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + dgv_Sockdetails.Rows[c].Cells["stockinout"].Value.ToString() + "&nbsp</font></th>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + dgv_Sockdetails.Rows[c].Cells["unit"].Value.ToString() + "&nbsp</font></th>");
                            sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + dgv_Sockdetails.Rows[c].Cells["gst"].Value.ToString() + "&nbsp;</font></th>");
                            sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + dgv_Sockdetails.Rows[c].Cells["rate"].Value.ToString() + "&nbsp;</font></th>");
                            sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + dgv_Sockdetails.Rows[c].Cells["currentstock"].Value.ToString() + "&nbsp;</font></th>");
                            sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + dgv_Sockdetails.Rows[c].Cells["totalamount"].Value.ToString() + "&nbsp;</font></th>");
                        }
                        sWrite.WriteLine("</tr >");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='right'  ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        sWrite.WriteLine("<td align='right'   ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        sWrite.WriteLine("<td align='right'  ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        sWrite.WriteLine("<td align='right'   ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        sWrite.WriteLine("<td align='right'  ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        sWrite.WriteLine("<td align='right'   ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        sWrite.WriteLine("<td align='right'   ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        sWrite.WriteLine("<td align='right'  ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        
                        sWrite.WriteLine("<td align='right'  ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b> Total:</b></font></td>");
                        sWrite.WriteLine("<td align='right'   ><FONT COLOR=black FACE='Segoe UI' SIZE=3> INR :"+Lab_AmountPaid.Text+"  </font><right'></td>");
                        sWrite.WriteLine("<td align='right'   ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        sWrite.WriteLine("<td align='right'   ><FONT COLOR=black FACE='Segoe UI' SIZE=3> INR :" + label2.Text + "  </font><right'></td>");
                        sWrite.WriteLine("</tr>");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("</tr>");
                        sWrite.WriteLine("</table>");
                        sWrite.WriteLine("</div>");
                        sWrite.WriteLine("<script>window.print();</script>");
                        sWrite.WriteLine("</body>");
                        sWrite.WriteLine("</html>");
                        sWrite.Close();
                        System.Diagnostics.Process.Start(Apppath + "\\SalesReport.html");
                    }
                }
                else
                {
                    MessageBox.Show("No records found, Please change the date and try again!..", "Failed ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
