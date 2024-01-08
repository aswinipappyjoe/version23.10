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
    public partial class stock_adjustment_report : Form
    {
        stock_updation_controller cntrl = new stock_updation_controller();
        public string dateFrom = "", dateTo = "", strclinicname = "", clinicn = "", strStreet = "", stremail = "", strwebsite = "", strphone = "", checkStr = "0", PathName = "", logo_name = "";

        private void btnEXPORT_Click(object sender, EventArgs e)
        {
            try
            {
                if (Grvsummary.Rows.Count != 0)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Excel Files (*.xls)|*.xls";
                    saveFileDialog1.FileName = "Stock Adjustment Report(" + DateTime.Now.ToString("dd-MM-yy h.mm.ss tt") + ").xls";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        PathName = saveFileDialog1.FileName;
                        Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                        ExcelApp.Application.Workbooks.Add(Type.Missing);
                        ExcelApp.Columns.ColumnWidth = 20;
                        int count = Grvsummary.Columns.Count;

                        ExcelApp.Range[ExcelApp.Cells[1, 1], ExcelApp.Cells[1, count]].Merge();
                        ExcelApp.Cells[1, 1] = "STOCK ADJUSTMENT REPORT";
                        ExcelApp.Cells[1, 1].HorizontalAlignment = HorizontalAlignment.Center;

                        ExcelApp.Cells[1, 1].Font.Size = 12;
                        ExcelApp.Cells[1, 1].Interior.Color = Color.FromArgb(153, 204, 255);
                        ExcelApp.Columns.ColumnWidth = 20;
                        ExcelApp.Cells[2, 1] = "From Date";
                        ExcelApp.Cells[2, 1].Font.Size = 10;
                        ExcelApp.Cells[2, 2] = dtp1ReceptReceivedPerMonth1.Value.ToString("dd-MM-yyyy");
                        ExcelApp.Cells[2, 2].Font.Size = 10;
                        ExcelApp.Cells[3, 1] = "To Date";
                        ExcelApp.Cells[3, 1].Font.Size = 10;
                        ExcelApp.Cells[3, 2] = dateTimePickerdaily1.Value.ToString("dd-MM-yyyy");
                        ExcelApp.Cells[3, 2].Font.Size = 10;
                        ExcelApp.Cells[4, 1] = "Running Date";
                        ExcelApp.Cells[4, 1].Font.Size = 10;
                        ExcelApp.Cells[4, 2] = DateTime.Now.ToString("dd-MM-yyyy");
                        ExcelApp.Cells[4, 2].Font.Size = 10;
                        ExcelApp.Cells[4, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        ExcelApp.Cells[3, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        ExcelApp.Cells[2, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        for (int i = 1; i < Grvsummary.Columns.Count + 1; i++)
                        {
                            ExcelApp.Cells[5, i] = Grvsummary.Columns[i - 1].HeaderText;
                            ExcelApp.Cells[5, i].ColumnWidth = 25;
                            ExcelApp.Cells[5, i].EntireRow.Font.Bold = true;
                            ExcelApp.Cells[5, i].Interior.Color = Color.FromArgb(0, 102, 204);
                            ExcelApp.Cells[5, i].Font.Size = 10;
                            ExcelApp.Cells[5, i].Font.Name = "Arial";
                            ExcelApp.Cells[5, i].Font.Color = Color.FromArgb(255, 255, 255);
                            ExcelApp.Cells[5, i].Interior.Color = Color.FromArgb(0, 102, 204);
                        }
                        for (int i = 0; i <= Grvsummary.Rows.Count; i++)
                        {
                            try
                            {
                                for (int j = 0; j < Grvsummary.Columns.Count; j++)
                                {
                                    ExcelApp.Cells[i + 6, j + 1] = Grvsummary.Rows[i].Cells[j].Value.ToString();
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

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (Grvsummary.Rows.Count > 0)
                {
                    string frdate = dtp1ReceptReceivedPerMonth1.Value.Day.ToString();
                    string frmonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dtp1ReceptReceivedPerMonth1.Value.Month);
                    string fryear = dtp1ReceptReceivedPerMonth1.Value.Year.ToString();
                    string todate = dateTimePickerdaily1.Value.Day.ToString();
                    string tomonth = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTimePickerdaily1.Value.Month);
                    string toyear = dateTimePickerdaily1.Value.Year.ToString();
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
                    System.IO.StreamWriter sWrite = new System.IO.StreamWriter(Apppath + "\\StockAdjustmentReport.html");
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
                    //if (cmb_stckINorOUT.Text == "Stock Out")
                    //{
                    //    sWrite.WriteLine("<tr>");
                    //    sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3> STOCK OUT REPORT </font></center></b></td>");
                    //}
                    //else
                    //{
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3> STOCK ADJUSTMENT REPORT </font></center></b></td>");
                    //}

                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=7 align=left><FONT COLOR=black FACE='Segoe UI' SIZE=2>  " + "<b>From :</b>" + " " + dtp1ReceptReceivedPerMonth1.Value.ToString("dd/MM/yyyy") + " </font></left></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=7 align=left><FONT COLOR=black FACE='Segoe UI' SIZE=2>  " + "<b>To :</b>" + "   " + dateTimePickerdaily1.Value.ToString("dd/MM/yyyy") + "</font></left></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' colspan=7><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>Printed Date:</b>" + " " + today + "" + "</font><left></td>");
                    sWrite.WriteLine("</tr>");
                    if (Grvsummary.Rows.Count > 0)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("    <td align='left' width='6%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3 ><b>&nbsp;Date.</b></font></th>");
                       
                        sWrite.WriteLine("    <td align='left' width='16%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Item Code</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Item Name</b></font></th>");
                        //sWrite.WriteLine("    <td align='left' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Lab Name</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Batch</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='17%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> <b>Action</b></font></th>");
                        sWrite.WriteLine("    <td align='right' width='15%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Update Qty</b></font></th>");
                        sWrite.WriteLine("    <td align='right' width='15%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b> Previous Stock</b></font></th>");
                        sWrite.WriteLine("    <td align='right' width='15%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b> Current Stock</b></font></th>");
                        sWrite.WriteLine("    <td align='right' width='25%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> <b>Unit</b></font></th>");
                       
                        sWrite.WriteLine("    <td align='right' width='25%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> <b>Rate</b></font></th>");
                        sWrite.WriteLine("    <td align='right' width='25%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> <b>GST</b></font></th>");
                      
                        sWrite.WriteLine("    <td align='right' width='25%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> <b>Total Amount</b></font></th>");
                        sWrite.WriteLine("</tr>");
                        for (int c = 0; c < Grvsummary.Rows.Count; c++)
                        {
                            sWrite.WriteLine("<tr>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells["date"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells["itemcode"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells["itemname"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells["batch"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells["action"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells["updatedqty"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + Grvsummary.Rows[c].Cells["previous_stock"].Value.ToString() + "&nbsp</font></th>");
                            sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + Grvsummary.Rows[c].Cells["current_stock"].Value.ToString() + "&nbsp</font></th>");
                            sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + Grvsummary.Rows[c].Cells["unit"].Value.ToString() + "&nbsp;</font></th>");
                            sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + Grvsummary.Rows[c].Cells["rate"].Value.ToString() + "&nbsp;</font></th>");
                            sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + Grvsummary.Rows[c].Cells["gst"].Value.ToString() + "&nbsp;</font></th>");
                            sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + Grvsummary.Rows[c].Cells["total"].Value.ToString() + "&nbsp;</font></th>");
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
                        sWrite.WriteLine("<td align='right'   ><FONT COLOR=black FACE='Segoe UI' SIZE=3>INR " + Lab_AmountPaid.Text + "  </font><right'></td>");
                        sWrite.WriteLine("<td align='right'   ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        sWrite.WriteLine("<td align='right'   ><FONT COLOR=black FACE='Segoe UI' SIZE=3>INR " + label5.Text + "  </font><right'></td>");
                        sWrite.WriteLine("</tr>");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("</tr>");
                        sWrite.WriteLine("</table>");
                        sWrite.WriteLine("</div>");
                        sWrite.WriteLine("<script>window.print();</script>");
                        sWrite.WriteLine("</body>");
                        sWrite.WriteLine("</html>");
                        sWrite.Close();
                        System.Diagnostics.Process.Start(Apppath + "\\StockAdjustmentReport.html");
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

        public stock_adjustment_report()
        {
            InitializeComponent();
        }

        private void stock_adjustment_report_Load(object sender, EventArgs e)
        {
            dtp1ReceptReceivedPerMonth1.Value = DateTime.Now.AddMonths(-1).Date;
            dateTimePickerdaily1.Value = DateTime.Now.Date;
            DataTable dt_items = this.cntrl.stock_updation_dataload(dtp1ReceptReceivedPerMonth1.Value.ToString("yyyy-MM-dd"), dateTimePickerdaily1.Value.ToString("yyyy-MM-dd"));
              fill_grid(dt_items);
            Grvsummary.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            Grvsummary.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            Grvsummary.EnableHeadersVisualStyles = false;
            Grvsummary.Columns["gst"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns["updatedqty"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns["rate"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns["previous_stock"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Grvsummary.Columns["current_stock"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Grvsummary.Columns["total"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns["gst"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns["updatedqty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns["rate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns["previous_stock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns["current_stock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns["total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.Grvsummary.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            Grvsummary.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            Grvsummary.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 9, FontStyle.Regular);
            foreach (DataGridViewColumn cl in Grvsummary.Columns)
            {
                cl.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        public void fill_grid(DataTable dtb)
        {
            Lab_Msg.Visible = false;
            Grvsummary.Rows.Clear();
            if (dtb.Rows.Count>0)
            {
                decimal totalAmount = 0, totalCost = 0;
                for (int i=0;i< dtb.Rows.Count;i++)
                {
                    decimal value=0,value1=0;
                    int qty = 0, Stock = 0,lstock=0, current_stk__unit1 = 0, prev_stk_unit1 = 0, Remainder = 0, Remainder_last = 0;
                    string  last_stock="",new_stock="";
                    Decimal gst = 0, gst_Amount = 0, unitcost = 0, Amount = 0, TotalAmount = 0;
                    DataTable dt_itemcode = this.cntrl.get_itemcode_frm_items(dtb.Rows[i]["ItemId"].ToString());
                    DataTable dt_stock = this.cntrl.get_stock_of_items(dtb.Rows[i]["ItemId"].ToString());
                    DataTable dt_itemdetails = this.cntrl.itemdetails(dtb.Rows[i]["ItemId"].ToString());
                    Grvsummary.Rows.Add();
                    Grvsummary.Rows[i].Cells["id"].Value = dtb.Rows[i]["ItemId"].ToString();
                    Grvsummary.Rows[i].Cells["date"].Value =Convert.ToDateTime( dtb.Rows[i]["updation_date"].ToString()).ToString("dd/MM/yyy");
                    Grvsummary.Rows[i].Cells["action"].Value = dtb.Rows[i]["Action"].ToString();
                    Grvsummary.Rows[i].Cells["itemcode"].Value = dt_itemcode.Rows[0]["item_code"].ToString();
                    Grvsummary.Rows[i].Cells["itemname"].Value = dtb.Rows[i]["ItemName"].ToString();
                    Grvsummary.Rows[i].Cells["batch"].Value = dtb.Rows[i]["batch"].ToString();
                    Grvsummary.Rows[i].Cells["unit"].Value = dtb.Rows[i]["unit"].ToString();
                    DataTable dt = this.cntrl.get_pur_gst(dtb.Rows[i]["ItemId"].ToString());
                    if (dt.Rows.Count>0)
                    {
                        Grvsummary.Rows[i].Cells["gst"].Value = dt.Rows[0]["GST"].ToString();
                        string c = dt.Rows[0]["GST"].ToString();
                        if (c!="")
                        {
                            gst = Convert.ToDecimal(c);
                        }
                    }
                    
                    Grvsummary.Rows[i].Cells["updatedqty"].Value = dtb.Rows[i]["upate_qty"].ToString();
                    Grvsummary.Rows[i].Cells["rate"].Value = dtb.Rows[i]["Cost"].ToString();
                    int unitmf = Convert.ToInt32(dt_itemdetails.Rows[0]["UnitMF"].ToString());
                    qty = Convert.ToInt32(dtb.Rows[i]["upate_qty"].ToString());
                    
                    unitcost = Convert.ToDecimal(dtb.Rows[i]["Cost"].ToString());
                    value = Convert.ToDecimal(dt_stock.Rows[0]["stock"].ToString());
                    value1 = Convert.ToDecimal(dtb.Rows[i]["last_qty"].ToString());
                    if (unitmf > 0)
                    {
                        if (dt_itemdetails.Rows[0]["Unit1"].ToString() == dtb.Rows[i]["unit"].ToString())
                        {
                            Stock = Convert.ToInt32(value);
                            lstock = Convert.ToInt32(value1);
                            current_stk__unit1 =Convert.ToInt32(Stock / unitmf);
                            prev_stk_unit1= Convert.ToInt32(lstock / unitmf);
                            Remainder = Convert.ToInt32(Stock % unitmf);
                            Remainder_last= Convert.ToInt32(lstock % unitmf);
                            last_stock= dt_itemdetails.Rows[0]["Unit1"].ToString() + " " + "=" + " " + prev_stk_unit1 + " " + "," + " " + dt_itemdetails.Rows[0]["Unit2"].ToString() + " " + "=" + Remainder_last;
                            new_stock = dt_itemdetails.Rows[0]["Unit1"].ToString() + " " + "=" + " " + current_stk__unit1 + " " + "," + " " + dt_itemdetails.Rows[0]["Unit2"].ToString() + " " + "=" + Remainder;
                            //Stock = qty;//*unitmf;
                        }
                        else
                        {
                            //current_stk__unit1 = Convert.ToDecimal(dt_stock.Rows[0]["stock"].ToString());
                            //prev_stk_unit1 = Convert.ToDecimal(dtb.Rows[i]["last_qty"].ToString());
                            //new_stock = dt_itemdetails.Rows[0]["Unit1"].ToString() + " " + "=" + " " + current_stk__unit1;
                            //last_stock = dt_itemdetails.Rows[0]["Unit1"].ToString() + " " + "=" + " " + prev_stk_unit1 ;
                            Stock = Convert.ToInt32(value);
                            lstock = Convert.ToInt32(value1);
                            current_stk__unit1 = Convert.ToInt32(Stock / unitmf);
                            prev_stk_unit1 = Convert.ToInt32(lstock / unitmf);
                            Remainder = Convert.ToInt32(Stock % unitmf);
                            Remainder_last = Convert.ToInt32(lstock % unitmf);
                            last_stock = dt_itemdetails.Rows[0]["Unit1"].ToString() + " " + "=" + " " + prev_stk_unit1 + " " + "," + " " + dt_itemdetails.Rows[0]["Unit2"].ToString() + " " + "=" + Remainder_last;
                            new_stock = dt_itemdetails.Rows[0]["Unit1"].ToString() + " " + "=" + " " + current_stk__unit1 + " " + "," + " " + dt_itemdetails.Rows[0]["Unit2"].ToString() + " " + "=" + Remainder;
                            //Stock = qty;
                        }
                    }
                    else
                    {
                        Stock = Convert.ToInt32(value);
                        lstock = Convert.ToInt32(value1);
                        current_stk__unit1 = Stock;
                        prev_stk_unit1 = lstock;
                        new_stock = dt_itemdetails.Rows[0]["Unit1"].ToString() + " " + "=" + " " + current_stk__unit1;
                        last_stock = dt_itemdetails.Rows[0]["Unit1"].ToString() + " " + "=" + " " + prev_stk_unit1;
                        //Stock = qty;
                    }
                    Grvsummary.Rows[i].Cells["previous_stock"].Value = last_stock ;// prev_stk_unit1;
                    Grvsummary.Rows[i].Cells["current_stock"].Value = new_stock;// current_stk__unit1;
                    if (gst > 0)
                    {
                        Amount = qty * unitcost;
                        gst_Amount = (Amount * gst) / 100;
                        TotalAmount = Amount + gst_Amount;
                    }
                    else
                    {
                        TotalAmount = (qty * unitcost);
                    }
                    Grvsummary.Rows[i].Cells["total"].Value = TotalAmount.ToString("##.00");
                    totalCost = totalCost + Convert.ToDecimal(dtb.Rows[i]["Cost"].ToString());
                    totalAmount = totalAmount + Convert.ToDecimal(TotalAmount);
                }
                Lab_AmountPaid.Text = totalCost.ToString("#.00");
                label5.Text = totalAmount.ToString("#.00");
            }
            else
            {
                Lab_Msg.Visible = true;
            }
        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            if (cmb_action.SelectedIndex == 0)
            {
                DataTable dt_items = this.cntrl.stock_updation_dataload(dtp1ReceptReceivedPerMonth1.Value.ToString("yyyy-MM-dd"), dateTimePickerdaily1.Value.ToString("yyyy-MM-dd"));
                fill_grid(dt_items);
            }
            else if (cmb_action.SelectedIndex > 0)
            {
                string action = cmb_action.Text;
                DataTable dt_items = this.cntrl.stock_updation_dataload_on_action(dtp1ReceptReceivedPerMonth1.Value.ToString("yyyy-MM-dd"), dateTimePickerdaily1.Value.ToString("yyyy-MM-dd"), action);
                fill_grid(dt_items);
            }
            else
            {
                DataTable dt_items = this.cntrl.stock_updation_dataload(dtp1ReceptReceivedPerMonth1.Value.ToString("yyyy-MM-dd"), dateTimePickerdaily1.Value.ToString("yyyy-MM-dd"));
                fill_grid(dt_items);
            }
        }

        private void cmb_action_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cmb_action.SelectedIndex==0)
            {
                DataTable dt_items = this.cntrl.stock_updation_dataload(dtp1ReceptReceivedPerMonth1.Value.ToString("yyyy-MM-dd"), dateTimePickerdaily1.Value.ToString("yyyy-MM-dd"));
                fill_grid(dt_items);
            }
            else if(cmb_action.SelectedIndex>0)
            {
                string action = cmb_action.Text;
                DataTable dt_items = this.cntrl.stock_updation_dataload_on_action(dtp1ReceptReceivedPerMonth1.Value.ToString("yyyy-MM-dd"), dateTimePickerdaily1.Value.ToString("yyyy-MM-dd"),action);
                fill_grid(dt_items);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
    }
}
