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
using PappyjoeMVC.Model;

namespace PappyjoeMVC.View
{
    public partial class DayWiseInvoice_Report : Form
    {
        Daily_Invoice_Report_model cntrl = new Daily_Invoice_Report_model();
        Common_model cmdl = new Common_model();
        public DateTime date;
        public int invcount, slno = 1, c = 0;
        public string drid, patient_id = "", select_dr_id = "", dte;
        string strclinicname = "", strStreet = "", stremail = "", strwebsite = "", strphone = "", clinicn = "", PathName = "", logo_name = "", DrctName = "";

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGV_Receipt.Rows.Count != 0)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Excel Files (*.xls)|*.xls";
                    saveFileDialog1.FileName = "Day Wise Receipt Report(" + DateTime.Now.ToString("dd-MM-yy h.mm.ss tt") + ").xls";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        PathName = saveFileDialog1.FileName;
                        Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                        ExcelApp.Application.Workbooks.Add(Type.Missing);
                        ExcelApp.Columns.ColumnWidth = 20;
                        if (Chk_RemoveAmountDue.Checked == true)
                        {
                            int count = 13;
                            ExcelApp.Range[ExcelApp.Cells[1, 1], ExcelApp.Cells[1, count]].Merge();
                        }
                        else if (Chk_RemoveAmountDue.Checked == false)
                        {
                            int count = 14;
                            ExcelApp.Range[ExcelApp.Cells[1, 1], ExcelApp.Cells[1, count]].Merge();
                        }
                        if (cmb_doctor.SelectedIndex == 0)
                        {
                            ExcelApp.Cells[1, 1] = "DAY WISE RECEIPT (All DOCTOR)";
                        }
                        else if (cmb_doctor.SelectedIndex > 0)
                        {
                            ExcelApp.Cells[1, 1] = " DAY WISE RECEIPT OF DR." + DrctName + "";
                        }
                        ExcelApp.Cells[1, 1].HorizontalAlignment = HorizontalAlignment.Center;
                        ExcelApp.Cells[1, 1].Font.Size = 12;
                        ExcelApp.Cells[1, 1].Interior.Color = Color.FromArgb(153, 204, 255);
                        ExcelApp.Columns.ColumnWidth = 20;
                        ExcelApp.Cells[2, 1] = "Date";
                        ExcelApp.Cells[2, 1].Font.Size = 10;
                        ExcelApp.Cells[2, 2] = DTP_From.Value.ToString("dd-MM-yyyy");
                        ExcelApp.Cells[2, 2].Font.Size = 10;
                        ExcelApp.Cells[4, 1] = "Running Date";
                        ExcelApp.Cells[4, 1].Font.Size = 10;
                        ExcelApp.Cells[4, 2] = DateTime.Now.ToString("dd-MM-yyyy");
                        ExcelApp.Cells[4, 2].Font.Size = 10;
                        ExcelApp.Cells[4, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        ExcelApp.Cells[3, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        ExcelApp.Cells[2, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        for (int i = 1; i < DGV_Receipt.Columns.Count + 1; i++)
                        {
                            if (i == 14)
                            {
                                if (Chk_RemoveAmountDue.Checked == false)
                                {
                                    ExcelApp.Cells[5, i] = DGV_Receipt.Columns[i - 1].HeaderText;
                                    ExcelApp.Cells[5, i].ColumnWidth = 25;
                                    ExcelApp.Cells[5, i].EntireRow.Font.Bold = true;
                                    ExcelApp.Cells[5, i].Interior.Color = Color.FromArgb(0, 102, 204);
                                    ExcelApp.Cells[5, i].Font.Size = 10;
                                    ExcelApp.Cells[5, i].Font.Name = "Arial";
                                    ExcelApp.Cells[5, i].Font.Color = Color.FromArgb(255, 255, 255);
                                    ExcelApp.Cells[5, i].Interior.Color = Color.FromArgb(0, 102, 204);
                                }
                            }
                            else
                            {
                                ExcelApp.Cells[5, i] = DGV_Receipt.Columns[i - 1].HeaderText;
                                ExcelApp.Cells[5, i].ColumnWidth = 25;
                                ExcelApp.Cells[5, i].EntireRow.Font.Bold = true;
                                ExcelApp.Cells[5, i].Interior.Color = Color.FromArgb(0, 102, 204);
                                ExcelApp.Cells[5, i].Font.Size = 10;
                                ExcelApp.Cells[5, i].Font.Name = "Arial";
                                ExcelApp.Cells[5, i].Font.Color = Color.FromArgb(255, 255, 255);
                                ExcelApp.Cells[5, i].Interior.Color = Color.FromArgb(0, 102, 204);
                            }
                        }
                        int j1 = 5;
                        for (int i = 0; i <= DGV_Receipt.Rows.Count; i++)
                        {
                            try
                            {
                                for (int j = 0; j < DGV_Receipt.Columns.Count; j++)
                                {
                                    if (j == 13)
                                    {
                                        if (Chk_RemoveAmountDue.Checked == false)
                                        {
                                            ExcelApp.Cells[i + 6, j + 1] = DGV_Receipt.Rows[i].Cells[j].Value.ToString();
                                            ExcelApp.Cells[i + 6, j + 1].BorderAround(true);
                                            ExcelApp.Cells[i + 6, j + 1].Borders.Color = Color.FromArgb(0, 102, 204);
                                            ExcelApp.Cells[i + 6, j + 1].Font.Size = 8;
                                        }
                                    }
                                    else
                                    {
                                        ExcelApp.Cells[i + 6, j + 1] = DGV_Receipt.Rows[i].Cells[j].Value.ToString();
                                        ExcelApp.Cells[i + 6, j + 1].BorderAround(true);
                                        ExcelApp.Cells[i + 6, j + 1].Borders.Color = Color.FromArgb(0, 102, 204);
                                        ExcelApp.Cells[i + 6, j + 1].Font.Size = 8;
                                    }
                                }
                                j1 = j1 + 1;
                            }
                            catch { }
                        }
                        ExcelApp.ActiveWorkbook.SaveAs(PathName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                        ExcelApp.ActiveWorkbook.Saved = true;
                        ExcelApp.Quit();
                        //checkStr = "1";
                        MessageBox.Show("Successfully Exported to Excel", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                    MessageBox.Show("No records found,please change the date and try again!..", "Failed ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error!...", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGV_Receipt.Rows.Count > 0)
                {
                    DataTable tbl = DGV_Receipt.DataSource as DataTable;
                    string frdate = DTP_From.Value.Day.ToString();
                    string frmonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DTP_From.Value.Month);
                    string fryear = DTP_From.Value.Year.ToString();
                    string today = DateTime.Now.ToString("dd/MM/yyyy");
                    string message = "Did you want Header on Print?";
                    string caption = "Verification";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        DataTable dt = this.cntrl.practicedetails();
                        if (dt.Rows.Count > 0)
                        {
                            clinicn = dt.Rows[0]["name"].ToString();
                            strclinicname = clinicn.Replace("¤", "'");
                            strphone = dt.Rows[0]["contact_no"].ToString();
                            strStreet = dt.Rows[0]["street_address"].ToString();
                            stremail = dt.Rows[0]["email"].ToString();
                            strwebsite = dt.Rows[0]["website"].ToString();
                            logo_name = dt.Rows[0]["path"].ToString();
                        }
                    }
                    string Apppath = System.IO.Directory.GetCurrentDirectory();
                    StreamWriter sWrite = new StreamWriter(Apppath + "\\ReceiptReceivedPerDay.html");
                    sWrite.WriteLine("<html>");
                    sWrite.WriteLine("<head>");
                    sWrite.WriteLine("<style>");
                    sWrite.WriteLine("table { border-collapse: collapse;}");
                    sWrite.WriteLine("p.big {line-height: 400%;}");
                    sWrite.WriteLine("</style>");
                    sWrite.WriteLine("</head>");
                    sWrite.WriteLine("<body >");
                    sWrite.WriteLine("<div>");
                    sWrite.WriteLine("<table align=center width=900>");
                    sWrite.WriteLine("<col >");
                    sWrite.WriteLine("<br>");
                    string Appath = System.IO.Directory.GetCurrentDirectory();
                    if (File.Exists(Appath + "\\" + logo_name))
                    {
                        sWrite.WriteLine("<table align='center' style='width:800px;border: 1px ;border-collapse: collapse;'>");
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
                    sWrite.WriteLine("<table align='center' style='width:800px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr><td align='left'  colspan='8'><hr/></td></tr>");
                    sWrite.WriteLine("</table>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3> DAY WISE RECEIPT  </font></center></b></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=9 align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>From:</b>  " + DTP_From.Value.ToString("dd/MM/yyyy") + " </font></td>");
                    sWrite.WriteLine("</tr>");
                    //sWrite.WriteLine("<tr>");
                    //sWrite.WriteLine("<td colspan=9 align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>To:</b>  " + Dtp_ReceiptTO.Value.ToString("dd/MM/yyyy") + " </font></td>");
                    //sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=9 align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>Printed Date :</b>  " + DateTime.Now.ToString("dd/MM/yyyy") + " </font></td>");
                    sWrite.WriteLine("</tr>");
                    if (DGV_Receipt.Rows.Count > 0)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("    <td align='left' width='7' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3 ><b>&nbsp;Slno.</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='70' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Patient</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='60' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Invoice</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='70' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Receipt</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='70' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Doctor</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='60' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Procedure</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='10' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Date</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='100' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Mode of Payment</b></font></td>");
                        sWrite.WriteLine("    <td align='right' width='30' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Cost</b>&nbsp;</font></td>");
                        sWrite.WriteLine("    <td align='right' width='60' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b> Discount</b>&nbsp;</font></td>");
                        sWrite.WriteLine("    <td align='right' width='30' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> <b>Tax</b>&nbsp;</font></td>");
                        //sWrite.WriteLine("    <td align='right' width='70' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Total Amount</b></font></td>");
                        sWrite.WriteLine("    <td align='right' width='70' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Amount Paid</b>&nbsp;</font></td>");
                        if (Chk_RemoveAmountDue.Checked)
                        { }
                        else
                        {
                            sWrite.WriteLine("    <td align='right' width='60' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Amount Due</b>&nbsp;</font></td>");
                        }
                        sWrite.WriteLine("</tr>");
                        try
                        {
                            for (int j = 0; j < DGV_Receipt.Rows.Count; j++)
                            {
                                sWrite.WriteLine("<tr>");
                                sWrite.WriteLine("<td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + DGV_Receipt.Rows[j].Cells["ColSLNo"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("<td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + DGV_Receipt.Rows[j].Cells["ColPtName"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("<td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + DGV_Receipt.Rows[j].Cells["ColInv"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("<td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + DGV_Receipt.Rows[j].Cells["ColReceipt"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("<td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + DGV_Receipt.Rows[j].Cells["ColDrName"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("<td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + DGV_Receipt.Rows[j].Cells["ColProcedure"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("<td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + DGV_Receipt.Rows[j].Cells["date_"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("<td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + DGV_Receipt.Rows[j].Cells["ColModeofpayment"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + DGV_Receipt.Rows[j].Cells["ColTotao_Cost"].Value.ToString() + "&nbsp;</font></td>");
                                sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + DGV_Receipt.Rows[j].Cells["COlDIS"].Value.ToString() + "&nbsp;</font></td>");
                                sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + DGV_Receipt.Rows[j].Cells["ColTax"].Value.ToString() + "&nbsp;</font></td>");
                                //sWrite.WriteLine("<td/* align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + DGV_Receipt.Rows[j].Cells["ColTotalIncome"].Value.ToString() + "&nbsp;</font></td>");*/
                                sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + Convert.ToDecimal(DGV_Receipt.Rows[j].Cells["ColAmountPaid"].Value.ToString()).ToString("#0.00") + "&nbsp;</font></td>");
                                if (Chk_RemoveAmountDue.Checked)
                                { }
                                else
                                {
                                    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Sego UI' SIZE=2>" + DGV_Receipt.Rows[j].Cells["ColTotalDue"].Value.ToString() + "&nbsp;</font></td>");
                                }
                                sWrite.WriteLine("</tr>");
                            }
                        }
                        catch { }
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        sWrite.WriteLine("<td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        sWrite.WriteLine("<td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        sWrite.WriteLine("<td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        sWrite.WriteLine("<td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        sWrite.WriteLine("<td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        sWrite.WriteLine("<td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        sWrite.WriteLine("<td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font><b>Total</b></td>");
                        sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>" + Lab_Amount.Text + "</font></td>");
                        sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>" + Lab_Discount.Text + "</b></font></td>");
                        sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>" + Lab_tax.Text + "</b></font></td>");
                        //sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>" + Lab_Amount.Text + "</b></font></td>");
                        sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>" + Lab_Paid.Text + "</b></font></td>");
                        if (Chk_RemoveAmountDue.Checked)
                        { }
                        else
                        {
                            sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>" + Lab_Due.Text + "</b></font></td>");
                        }
                        sWrite.WriteLine("</tr>");
                    }
                    sWrite.WriteLine("</table>");
                    sWrite.WriteLine("</div>");
                    sWrite.WriteLine("<script>window.print();</script>");
                    sWrite.WriteLine("</body>");
                    sWrite.WriteLine("</html>");
                    sWrite.Close();
                    System.Diagnostics.Process.Start(Apppath + "\\ReceiptReceivedPerDay.html");
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error!...", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        decimal due = 0;
        private void cmb_doctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGV_Receipt.Rows.Clear();
            select_dr_id = "0";
            if (cmb_doctor.SelectedIndex == -1) { }
            else
            {
                drid = cmb_doctor.SelectedItem.ToString();
                select_dr_id = this.cmdl.Get_DoctorId(drid);
               
            }
            string date = Convert.ToDateTime(DTP_From.Value).ToString("yyyy-MM-dd");
            if (select_dr_id == "0")
            {
                DataTable inv = this.cntrl.getinvoice(date);
                getinvoice(inv);
                DataTable pay = this.cntrl.getpay(date);
                getpay(pay);
            } 
            else
            {
                DataTable inv2 = this.cntrl.getinvoice2(date, select_dr_id);
                getinvoice(inv2);
                DataTable pay2 = this.cntrl.getpay2(date, select_dr_id);
                getpay(pay2);
            }
            if (DGV_Receipt.Rows.Count == 0)
            {
                int x = (panel3.Size.Width - Lab_Msg.Size.Width) / 2;
                Lab_Msg.Location = new Point(x, Lab_Msg.Location.Y);
                Lab_Msg.Show();
                Lab_Amount.Text = "00.00";
                Lab_tax.Text = "00.00";
                Lab_Discount.Text = "00.00"; 
                Lab_Paid.Text = "00.00";
                Lab_Due.Text = "00.00"; lb_Totalcost.Text = "00.00";
                totalpayment = 0;
                totalcredit = 0;
                balance = 0; due = 0;
                totalinvoice = 0; total_cost = 0;
            }
            else
            {
                Lab_Msg.Hide(); 
                Lab_Amount.Text = Convert.ToDecimal(totalcredit).ToString("#0.00");
                Lab_tax.Text = Convert.ToDecimal(total_tax).ToString("#0.00");
                Lab_Discount.Text = Convert.ToDecimal(total_dis).ToString("#0.00");//balance
                Lab_Paid.Text = Convert.ToDecimal(totalpayment).ToString("#0.00"); 
                Lab_Due.Text = Convert.ToDecimal(due).ToString("#0.00"); 
                lb_Totalcost.Text = Convert.ToDecimal(total_cost).ToString("#0.00");
            }
            totalpayment = 0;
            totalcredit = 0;
            balance = 0; due = 0;
            totalinvoice = 0; total_dis = 0; total_tax = 0; total_cost = 0;
        }

        private void btn_Show_Click(object sender, EventArgs e)
        {
            string date = Convert.ToDateTime(DTP_From.Value).ToString("yyyy-MM-dd");
            DataTable inv = this.cntrl.getinvoice(date);
            getinvoice(inv);
            DataTable pay = this.cntrl.getpay(date);
            getpay(pay);
            if (DGV_Receipt.Rows.Count == 0)
            {
                int x = (panel3.Size.Width - Lab_Msg.Size.Width) / 2;
                Lab_Msg.Location = new Point(x, Lab_Msg.Location.Y);
                Lab_Msg.Show();
                Lab_Amount.Text = "00.00";
                Lab_tax.Text = "00.00";
                Lab_Discount.Text = "00.00";
                Lab_Paid.Text = "00.00";
                Lab_Due.Text = "00.00"; lb_Totalcost.Text= "00.00";
                 totalpayment = 0;
                totalcredit = 0;
                balance = 0; due = 0;
                totalinvoice = 0; total_cost = 0;
            }
            else
            {
                Lab_Msg.Hide();
                Lab_Amount.Text = Convert.ToDecimal(totalcredit).ToString("#0.00");
                Lab_tax.Text = Convert.ToDecimal(total_tax).ToString("#0.00");
                Lab_Discount.Text = Convert.ToDecimal(total_dis).ToString("#0.00");//balance
                Lab_Paid.Text = Convert.ToDecimal(totalpayment).ToString("#0.00");
                Lab_Due.Text = Convert.ToDecimal(due).ToString("#0.00");
                lb_Totalcost.Text= Convert.ToDecimal(total_cost).ToString("#0.00");
            }
            totalpayment = 0;
            totalcredit = 0;
            balance = 0; due = 0;
            totalinvoice = 0; total_dis = 0; total_tax = 0; total_cost = 0;
        }

        public decimal totalcost, totalpay, totalpaid, totalinvoice = 0, credit, balance, balance1, totalcredit = 0, totalpayment = 0, total_tax =0,total_dis=0,total_cost=0;      public DayWiseInvoice_Report()
        {
            InitializeComponent();
        }

        private void DayWiseInvoice_Report_Load(object sender, EventArgs e)
        {
            DGV_Receipt.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            DGV_Receipt.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DGV_Receipt.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 8, FontStyle.Regular);
            DGV_Receipt.EnableHeadersVisualStyles = false;
            cmb_doctor.Items.Add("All Doctors");
            cmb_doctor.ValueMember = "0";
            cmb_doctor.DisplayMember = "All Doctors";
            DataTable doctor_rs = this.cntrl.getdocname();
            if (doctor_rs.Rows.Count > 0)
            {
                for (int i = 0; i < doctor_rs.Rows.Count; i++)
                {
                    cmb_doctor.Items.Add(doctor_rs.Rows[i]["doctor_name"].ToString());
                    cmb_doctor.ValueMember = doctor_rs.Rows[i]["id"].ToString();
                    cmb_doctor.DisplayMember = doctor_rs.Rows[i]["doctor_name"].ToString();
                }
            }
            cmb_doctor.SelectedIndex = 0;
            select_dr_id = "0";
        }
        
        public void getinvoice(DataTable invMain)
        {
            try
            {
                if (invMain.Rows.Count == 0)
                {
                    int x = (panel3.Size.Width - Lab_Msg.Size.Width) / 2;
                    Lab_Msg.Location = new Point(x, Lab_Msg.Location.Y);
                    Lab_Msg.Show();
                }
                else
                {
                    DGV_Receipt.Rows.Clear();
                    Lab_Msg.Hide();
                    for (int z = 0; z < invMain.Rows.Count; z++)
                    {
                        decimal amdue = 0;
                        string Patient = invMain.Rows[z]["pt_name"].ToString();
                        string date = invMain.Rows[z]["date"].ToString();
                        string invoiceno = invMain.Rows[z]["invoice_no"].ToString();
                        string details = invMain.Rows[z]["services"].ToString() + " (Qty:" + invMain.Rows[z]["unit"].ToString() + ")";
                        decimal cost = decimal.Parse(invMain.Rows[z]["cost"].ToString());
                        decimal unit = decimal.Parse(invMain.Rows[z]["unit"].ToString());
                        decimal discount = decimal.Parse(invMain.Rows[z]["discountin_rs"].ToString());
                        decimal tax = decimal.Parse(invMain.Rows[z]["tax_inrs"].ToString());
                        string doctor = invMain.Rows[z]["doctor_name"].ToString();
                        credit = (((cost * unit) + tax) - discount);// (cost * unit) - (tax + discount);//Convert.ToDecimal(((totalCost + totalTax) - totalDiscount)).ToString("#0.00")
                        totalcredit = totalcredit + credit;
                        //totalinvoice = totalinvoice + credit;
                        total_cost = total_cost + cost;
                        amdue = decimal.Parse(invMain.Rows[z]["total"].ToString());
                        total_tax = total_tax + tax;
                        total_dis = total_dis + discount;
                        //due = due + amdue;                                                     //mode of payment                              //Amount Received
                        DGV_Receipt.Rows.Add(slno, Patient, invoiceno, " ", doctor, details, date, "", cost, tax, discount, String.Format("{0:C}", credit), "", String.Format("{0:C}", credit));//totalcredit  , String.Format("{0:C}", credit), "0.00", String.Format("{0:C}", amdue)
                        //Grvsummary.Rows[z].Cells[1].Style.ForeColor = Color.Blue;
                        //Grvsummary.Rows[z].Cells[6].Style.ForeColor = Color.Blue;
                        //Grvsummary.Rows[z].Cells[0].Style.ForeColor = Color.Blue;
                        //Grvsummary.Rows[z].Cells[2].Style.ForeColor = Color.Blue;
                        //Grvsummary.Rows[z].Cells[3].Style.ForeColor = Color.Blue;
                        //Grvsummary.Rows[z].Cells[4].Style.ForeColor = Color.Blue;
                        //Grvsummary.Rows[z].Cells[5].Style.ForeColor = Color.Blue;
                        //Grvsummary.Rows[z].Cells[6].Style.ForeColor = Color.Blue;
                        //Grvsummary.Rows[z].Cells[7].Style.ForeColor = Color.Blue;
                        //Grvsummary.Rows[z].Cells[8].Style.ForeColor = Color.Blue;
                        //Grvsummary.Rows[z].Cells[9].Style.ForeColor = Color.Blue;
                        //Grvsummary.Rows[z].Cells[10].Style.ForeColor = Color.Blue;
                        //Grvsummary.Rows[z].Cells[11].Style.ForeColor = Color.Red;
                        DGV_Receipt.Rows[z].Cells[8].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                        slno = slno + 1;
                        invcount = invMain.Rows.Count;
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error!...", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        public void getpay(DataTable payMain)
        {
            try
            {
                //DGV_Receipt.Rows.Clear();
                int sl = 1;
                decimal cash = 0, cheque = 0, card = 0, dd = 0, PayTM = 0, paypal = 0, Tez = 0, UPI = 0, NEFT = 0, IMPS = 0, Netbanking = 0, Wallets = 0, CCAvenue = 0;
                for (int u = 0; u < payMain.Rows.Count; u++)
                {

                    string Patient = payMain.Rows[u]["pt_name"].ToString();
                    string Patient_id = payMain.Rows[u]["pt_id"].ToString();
                    string recpno = payMain.Rows[u]["receipt_no"].ToString();
                    string date = payMain.Rows[u]["payment_date"].ToString();
                    string details = payMain.Rows[u]["procedure_name"].ToString();// +" (Qty:" + invMain.Rows[u]["unit"].ToString() + ")";
                    string doctor = payMain.Rows[u]["doctor_name"].ToString();
                    string mode = payMain.Rows[u]["mode_of_payment"].ToString();
                    //DateTime d = Convert.ToDateTime(date);
                    //string day = d.Day.ToString();
                    //string month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(d.Month);
                    //string year = d.Year.ToString();
                    string invoiceno = payMain.Rows[u]["invoice_no"].ToString();
                    string[] invoice1 = new string[100];
                    invoice1 = invoiceno.Split(',');
                    decimal total = Convert.ToDecimal(payMain.Rows[u]["amount_paid"].ToString());
                    totalpayment = totalpayment + total;
                    balance1 = Convert.ToDecimal(payMain.Rows[u]["total"].ToString());
                    balance = balance1 - total; // balance = totalinvoice - totalpayment;
                    //due = due + balance;
                    string tax_inrs = "";// payMain.Rows[u]["tax_inrs"].ToString();                   //cost    //tax    dis   totalamount 
                    DGV_Receipt.Rows.Add(slno, Patient, invoiceno, recpno, doctor, details, date, mode, "0.00", "03.00", "0.00", "0.00", total, balance);
                    sl = slno;
                    slno = slno + 1;
                }
                due = totalcredit - totalpayment;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
