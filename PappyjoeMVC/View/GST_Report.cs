using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PappyjoeMVC.Controller;
using PappyjoeMVC.Model;

namespace PappyjoeMVC.View
{
    public partial class GST_Report : Form
    {
        gst_controller cntrl = new gst_controller();
        public string dateFrom = "", dateTo = "", checkStr = "", PathName = "", strclinicname = "", clinicn = "", strStreet = "", stremail = "", strwebsite = "", strphone = "", logo_name = "";

        private void buttonClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void btnEXPORT_Click(object sender, EventArgs e)
        {
            try
            {
                if (Grvsummary.Rows.Count != 0)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Excel Files (*.xls)|*.xls";
                    saveFileDialog1.FileName = "Daily GST Report(" + DateTime.Now.ToString("dd-MM-yy h.mm.ss tt") + ").xls";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        PathName = saveFileDialog1.FileName;
                        Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                        ExcelApp.Application.Workbooks.Add(Type.Missing);
                        ExcelApp.Columns.ColumnWidth = 20;
                        int count = Grvsummary.Columns.Count;
                        ExcelApp.Range[ExcelApp.Cells[1, 1], ExcelApp.Cells[1, count]].Merge();

                        ExcelApp.Cells[1, 1] = "DAILY GST REPORT ";

                        ExcelApp.Cells[1, 1].HorizontalAlignment = HorizontalAlignment.Center;
                        ExcelApp.Cells[1, 1].Font.Size = 12;
                        ExcelApp.Cells[1, 1].Interior.Color = Color.FromArgb(153, 204, 255);
                        ExcelApp.Columns.ColumnWidth = 20;
                        ExcelApp.Cells[2, 1] = "Date";
                        ExcelApp.Cells[2, 1].Font.Size = 10;
                        ExcelApp.Cells[2, 2] = dateTimePickerdaily1.Value.ToString("dd-MM-yyyy");
                        ExcelApp.Cells[2, 2].Font.Size = 10;
                        ExcelApp.Cells[3, 1] = "Running Date";
                        ExcelApp.Cells[3, 1].Font.Size = 10;
                        ExcelApp.Cells[3, 2] = DateTime.Now.ToString("dd-MM-yyyy");
                        ExcelApp.Cells[3, 2].Font.Size = 10;
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
                    MessageBox.Show("No records found,please change the date and try again!..", "Failed ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error!...", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public GST_Report()
        {
            InitializeComponent();
        }

        private void GST_Report_Load(object sender, EventArgs e)
        {
            try
            {
                Grvsummary.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
                Grvsummary.EnableHeadersVisualStyles = false;
                this.Grvsummary.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
                Grvsummary.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                Grvsummary.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 9, FontStyle.Regular);
                Lab_Msg.Visible = false;
                string date = Convert.ToDateTime(dateTimePickerdaily1.Value).ToString("yyyy-MM-dd");
                DataTable dtb_data = this.cntrl.Get_AllData(date);
                fill_data(dtb_data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void fill_data(DataTable dtb)
        {
            Grvsummary.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            Grvsummary.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            if (dtb.Rows.Count > 0)
            {
                Lab_Msg.Visible = false;
                int k = 1;
                Grvsummary.Rows.Clear();
                decimal total_cgst = 0, total_sgst = 0, totolamount = 0,t_tax=0, total_dis = 0, tax_amt = 0, totaldis = 0, DiscAmount = 0, GrandTotal = 0;
                for (int i = 0; i < dtb.Rows.Count; i++)
                {
                    decimal gst_Amount = 0, Amount = 0, qty = 0, gst = 0, rate = 0, cgst = 0;
                    Grvsummary.Rows.Add();
                    Grvsummary.Rows[i].Cells["SL_NO"].Value = k;
                    Grvsummary.Rows[i].Cells["NAME"].Value = dtb.Rows[i]["Item_Code"].ToString();
                    Grvsummary.Rows[i].Cells["rate"].Value = dtb.Rows[i]["Rate"].ToString();
                    Grvsummary.Rows[i].Cells["qty"].Value = dtb.Rows[i]["Qty"].ToString();
                    rate = Convert.ToDecimal(dtb.Rows[i]["Rate"].ToString());
                    qty = Convert.ToDecimal(dtb.Rows[i]["Qty"].ToString());
                    gst = Convert.ToDecimal(dtb.Rows[i]["GST"].ToString());
                    Amount = qty * rate;
                    cgst = gst / 2;
                    //gst_Amount = Amount * (gst / 100);
                    //gst_Amount = (cgst / Amount) * 2;
                    //90 x(100 / 125) = 72 %

                    Grvsummary.Rows[i].Cells["TAXABLEVAMOUNT"].Value = Amount.ToString("##0.00");
                    t_tax = t_tax + Amount;
                    gst_Amount = (Amount * cgst) / 100;
                    Grvsummary.Rows[i].Cells["GSTRATE"].Value = dtb.Rows[i]["GST"].ToString();// gst_Amount.ToString("##0.00");// dtb.Rows[i]["GST"].ToString();

                    Grvsummary.Rows[i].Cells["SGST"].Value = Convert.ToDecimal(gst_Amount).ToString("##0.00");
                    total_cgst = total_cgst + gst_Amount;
                    total_sgst = total_sgst + gst_Amount;
                    tax_amt = Amount + gst_Amount + gst_Amount;
                    Grvsummary.Rows[i].Cells["CGST"].Value = Convert.ToDecimal(gst_Amount).ToString("##0.00");
                    Grvsummary.Rows[i].Cells["TOTAL"].Value = tax_amt.ToString("##0.00");// dtb.Rows[i]["TotalAmount"].ToString();
                    totolamount = totolamount + tax_amt;
                    total_dis = total_dis + Convert.ToDecimal(dtb.Rows[i]["Discount"].ToString());
                    k++;
                }
                Grvsummary.Rows.Add("", "", "", "TOTAL:", t_tax.ToString("##0.00"), "", total_sgst.ToString("##0.00"), total_cgst.ToString("##0.00"), totolamount.ToString("##0.00"));

                int row = Grvsummary.Rows.Count;
                Grvsummary.Rows[row - 1].Cells[3].Style.ForeColor = Color.Red;
                Grvsummary.Rows[row - 1].Cells[4].Style.ForeColor = Color.Red;
                Grvsummary.Rows[row - 1].Cells[6].Style.ForeColor = Color.Red;
                Grvsummary.Rows[row - 1].Cells[7].Style.ForeColor = Color.Red;
                Grvsummary.Rows[row - 1].Cells[8].Style.ForeColor = Color.Red;
                Grvsummary.Rows[row - 1].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                Grvsummary.Rows[row - 1].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                Grvsummary.Rows[row - 1].Cells[6].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                Grvsummary.Rows[row - 1].Cells[7].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                Grvsummary.Rows[row - 1].Cells[8].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);


                totaldis = Convert.ToDecimal((totolamount * total_dis) / 100);
                DiscAmount = Convert.ToDecimal(totaldis);
                if (DiscAmount > 0)
                {
                    GrandTotal = Convert.ToDecimal(Convert.ToDecimal(totolamount) - Convert.ToDecimal(totaldis));

                }
                else
                {
                    GrandTotal = totolamount;
                }
                Grvsummary.Rows.Add("", "", "", "", "", "", "", "Discount", DiscAmount.ToString("##0.00"));
               
                int row_ = Grvsummary.Rows.Count;
                Grvsummary.Rows[row_ - 1].Cells[7].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                Grvsummary.Rows[row_ - 1].Cells[8].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                Grvsummary.Rows.Add("", "", "", "", "", "", "", "Net Amount", GrandTotal.ToString("##.00"));
                int row_1 = Grvsummary.Rows.Count;
                Grvsummary.Rows[row_1 - 1].Cells[7].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                Grvsummary.Rows[row_1 - 1].Cells[8].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                Grvsummary.Rows.Add("", "", "", "", "", "", "", "Round OF", Math.Round(GrandTotal).ToString("##0.00"));
                int row_2 = Grvsummary.Rows.Count;
                Grvsummary.Rows[row_2 - 1].Cells[7].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                Grvsummary.Rows[row_2 - 1].Cells[8].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);

            }
            else
            {
                Lab_Msg.Visible = true;
            }
        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            string date = Convert.ToDateTime(dateTimePickerdaily1.Value).ToString("yyyy-MM-dd");
            DataTable dtb_data = this.cntrl.Get_AllData(date);
            fill_data(dtb_data);
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (Grvsummary.Rows.Count > 0)
                {
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
                    System.IO.StreamWriter sWrite = new System.IO.StreamWriter(Apppath + "\\gst_bill.html");
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
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3> GST BILL  </font></center></b></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=7 align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> <b>Date: </b>" + dateTimePickerdaily1.Value.ToString("dd/MM/yyyy") + " </font></td> ");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=7 align='left' colspan=7><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>Printed Date: </b>" + today + "</font></td>");
                    sWrite.WriteLine("</tr>");
                    int rownum = 0;
                    if (Grvsummary.Rows.Count > 0)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("    <td align='left' width='7%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3 ><b>&nbsp;Slno.</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='14%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;Name</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;Rate</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='7%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;Qty</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='15%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;Taxable Amount</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;GST Rate</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;SGST</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;CGST</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='18%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;TOTAL</b></font></td>");
                        sWrite.WriteLine("</tr>");
                        for (int c = 0; c < Grvsummary.Rows.Count; c++)
                        {
                            if (Grvsummary.Rows[c].Cells["SL_NO"].Value.ToString() != "")
                            {
                                sWrite.WriteLine("<tr>");
                                sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells["SL_NO"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells["NAME"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells["rate"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells["qty"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells["TAXABLEVAMOUNT"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells["GSTRATE"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells["SGST"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells["CGST"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells["TOTAL"].Value.ToString() + "</font></td>");
                                rownum = c;
                            }
                        }
                        sWrite.WriteLine("</tr>");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='center'  style='border:1px solid #000' colspan=4 ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b> TOTAL</b></font></td>");
                        sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=3>  " + Grvsummary.Rows[rownum + 1].Cells["TAXABLEVAMOUNT"].Value.ToString() + " </font></td>");
                        sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=3>   </font></td>");
                        sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=3>  " + Grvsummary.Rows[rownum + 1].Cells["SGST"].Value.ToString() + " </font></td>");
                        sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=3>  " + Grvsummary.Rows[rownum + 1].Cells["CGST"].Value.ToString() + " </font></td>");
                        sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=3>  " + Grvsummary.Rows[rownum + 1].Cells["TOTAL"].Value.ToString() + " </font></td>");
                        sWrite.WriteLine("</tr>");

                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='right'  colspan=8 ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b> Discount :</b></font></td>");
                        sWrite.WriteLine("<td align='right'  colspan=5 ><FONT COLOR=black FACE='Segoe UI' SIZE=3>  " + Grvsummary.Rows[rownum + 2].Cells["TOTAL"].Value.ToString() + " </font></td>");
                        sWrite.WriteLine("</tr>");

                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='right'  colspan=8 ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b> Round OF :</b></font></td>");
                        sWrite.WriteLine("<td align='right'  colspan=5 ><FONT COLOR=black FACE='Segoe UI' SIZE=3>  " + Grvsummary.Rows[rownum + 4].Cells["TOTAL"].Value.ToString() + " </font></td>");
                        sWrite.WriteLine("</tr>");

                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='right'  colspan=8 ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b> Net Amount :</b></font></td>");
                        sWrite.WriteLine("<td align='right'  colspan=5 ><FONT COLOR=black FACE='Segoe UI' SIZE=3>  " + Grvsummary.Rows[rownum + 3].Cells["TOTAL"].Value.ToString() + " </font></td>");
                        sWrite.WriteLine("</tr>");

                        sWrite.WriteLine("</table>");
                        sWrite.WriteLine("</div>");
                        sWrite.WriteLine("<script>window.print();</script>");
                        sWrite.WriteLine("</body>");
                        sWrite.WriteLine("</html>");
                        sWrite.Close();
                        System.Diagnostics.Process.Start(Apppath + "\\gst_bill.html");
                    }
                }
                else
                {
                    MessageBox.Show("No records found, Please change the date and try again!..", "No Records Found ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }
    }
}
