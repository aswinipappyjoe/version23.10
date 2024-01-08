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
    public partial class daily_GST_report : Form
    {
        gst_controller cntrl = new gst_controller();
        public string dateFrom = "", dateTo = "", checkStr = "", PathName = "", strclinicname = "", clinicn = "", strStreet = "", stremail = "", strwebsite = "", strphone = "", logo_name = "";

        private void rad_pur_Click(object sender, EventArgs e)
        {
            rad_pur.Checked = true;
            rad_sales.Checked = false;
            //string date = Convert.ToDateTime(dateTimePickerdaily1.Value).ToString("yyyy-MM-dd");
            //DataTable dt = this.cntrl.Purchase_Get_AllData(date);
            fill_data_purchase();
        }

        private void rad_sales_Click(object sender, EventArgs e)
        {
            rad_pur.Checked = false;
            rad_sales.Checked = true;
            //string date = Convert.ToDateTime(dateTimePickerdaily1.Value).ToString("yyyy-MM-dd");
            //DataTable dt = this.cntrl.Get_AllData(date);
            fill_data();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rad_sales_CheckedChanged(object sender, EventArgs e)
        {
            //rad_pur.Checked = false;
            //rad_sales.Checke/*d*/ = true;
            //string date = Convert.ToDateTime(dateTimePickerdaily1.Value).ToString("yyyy-MM-dd");
            //DataTable dt = this.cntrl.Get_AllData(date);
            //fill_data();
        }

        private void rad_pur_CheckedChanged(object sender, EventArgs e)
        {
            //rad_pur.Checked = true;
            ////rad_sales.Ch/*e*/cked = false;
            ////string date = Convert.ToDateTime(dateTimePickerdaily1.Value).ToString("yyyy-MM-dd");
            ////DataTable dt = this.cntrl.Purchase_Get_AllData(date);
            //fill_data_purchase();
        }

        private void buttonClose_Click(object sender, EventArgs e)
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
                        ExcelApp.Cells[4, 1] = "GST";
                        ExcelApp.Cells[4, 1].Font.Size = 10;
                        ExcelApp.Cells[4, 2] = txt_gst.Text;
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
                    MessageBox.Show("No records found,please change the date and try again!..", "Failed ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error!...", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
                    DialogResult result; string  gst = "";
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
                            gst = dtp.Rows[0]["Dl_Number2"].ToString();
                        }
                    }
                    string Apppath = System.IO.Directory.GetCurrentDirectory();
                    System.IO.StreamWriter sWrite = new System.IO.StreamWriter(Apppath + "\\daily_gst_report.html");
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
                    sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3>DAILY GST REPORT  </font></center></b></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=7 align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> <b>Date: </b>" + dateTimePickerdaily1.Value.ToString("dd/MM/yyyy") + " </font></td> ");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=7 align='left' colspan=7><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>Printed Date: </b>" + today + "</font></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=7 align='left' colspan=7><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b> Gst: </b>" + gst + "</font></td>");
                    sWrite.WriteLine("</tr>");
                    if (Grvsummary.Rows.Count > 0)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("    <td align='left' width='12%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3 ><b>&nbsp;Date</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='12%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;Bill No</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='14%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;Name</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='18%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;Taxable Amount</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;Tax</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='12%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;Total</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='18%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;Mode Of Payment</b></font></td>");
                        sWrite.WriteLine("</tr>");
                        for (int c = 0; c < Grvsummary.Rows.Count; c++)
                        {
                                sWrite.WriteLine("<tr>");
                            if(Grvsummary.Rows.Count==0)
                            {
                                sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>&nbsp;" + Grvsummary.Rows[c].Cells["DATE"].Value.ToString() + "</b></font></td>");
                            }
                            else
                            {
                                if (Grvsummary.Rows[c].Cells[2].Value.ToString()=="TOTAL")
                                {
                                    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' colspan=3 ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>" + Grvsummary.Rows[c].Cells[2].Value.ToString() + "</b></font></td>");
                                    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>" + Grvsummary.Rows[c].Cells["TAXAMOUNT"].Value.ToString() + "</b></font></td>");
                                    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>" + Grvsummary.Rows[c].Cells["TAX"].Value.ToString() + "</b></font></td>");
                                    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>" + Grvsummary.Rows[c].Cells["TOTAL"].Value.ToString() + "</b></font></td>");
                                    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells["MODE"].Value.ToString() + "</font></td>");
                                }
                                else if(Grvsummary.Rows[c].Cells[2].Value.ToString() == "Summery")
                                {
                                    sWrite.WriteLine("    <td align='left' style='border:1px solid #000'  ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>" + Grvsummary.Rows[c].Cells["DATE"].Value.ToString() + "</b></font></td>");
                                    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>" + Grvsummary.Rows[c].Cells["BILLNO"].Value.ToString() + ">/b></font></td>");
                                    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells["CUSTOMERNAME"].Value.ToString() + "</font></td>");
                                }
                                else
                                {
                                    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells["DATE"].Value.ToString() + "</font></td>");
                                    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells["BILLNO"].Value.ToString() + "</font></td>");
                                    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells[2].Value.ToString() + "</font></td>");
                                    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells["TAXAMOUNT"].Value.ToString() + "</font></td>");
                                    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells["TAX"].Value.ToString() + "</font></td>");
                                    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;INR " +
                                        "" +
                                        "" + Grvsummary.Rows[c].Cells["TOTAL"].Value.ToString() + "</font></td>");
                                    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells["MODE"].Value.ToString() + "</font></td>");
                                }
                            }
                            sWrite.WriteLine("</tr>");
                        }
                        sWrite.WriteLine("</table>");
                        sWrite.WriteLine("</div>");
                        sWrite.WriteLine("<script>window.print();</script>");
                        sWrite.WriteLine("</body>");
                        sWrite.WriteLine("</html>");
                        sWrite.Close();
                        System.Diagnostics.Process.Start(Apppath + "\\daily_gst_report.html");
                    }
                }
                else
                {
                    MessageBox.Show("No records found, Please change the date and try again!..", "No Records Found ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            if(rad_pur.Checked==true)
            {
                fill_data_purchase();
            }
            else
            {
                fill_data();
            }
           
        }

        public daily_GST_report()
        {
            InitializeComponent();
        }

        private void daily_GST_report_Load(object sender, EventArgs e)
        {
            try
            {
                Grvsummary.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
                Grvsummary.EnableHeadersVisualStyles = false;
                this.Grvsummary.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
                Grvsummary.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                Grvsummary.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 9, FontStyle.Regular);
                Lab_Msg.Visible = false;
                DataTable dt4 = this.cntrl.select_gst();
                if (dt4.Rows.Count > 0)
                {
                    txt_gst.Text = dt4.Rows[0]["Dl_Number2"].ToString();
                }
                fill_data();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!...", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
        }

        public void fill_data()
        {
            string date = Convert.ToDateTime(dateTimePickerdaily1.Value).ToString("yyyy-MM-dd");
            DataTable dtb_data = this.cntrl.DAILY_Get_gstrate(date);
            Grvsummary.Columns[2].Name = "CUSTOMER NAME";//CUSTOMER NAME
            decimal rate = 0, qty = 0, taxbleamount = 0, tax = 0, totalamount = 0, cash = 0, cheque = 0, card = 0, dd = 0, PayTM = 0, paypal = 0, Tez = 0, UPI = 0, NEFT = 0, IMPS = 0, Netbanking = 0, Wallets = 0, CCAvenue = 0,a=0,b=0,c=0;
            Grvsummary.Rows.Clear();
            if (dtb_data.Rows.Count>0)
            {
                Lab_Msg.Visible = false;
                Grvsummary.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Grvsummary.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Grvsummary.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Grvsummary.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //Grvsummary.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //Grvsummary.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //Grvsummary.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //Grvsummary.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                for (int i=0;i<dtb_data.Rows.Count;i++)
                {
                    decimal total_taxable = 0, total_tax = 0, total_amount = 0;
                       DataTable dt_gst_data = this.cntrl.DAILY_GST_AllData( dtb_data.Rows[i][0].ToString(), date);
                    if(dt_gst_data.Rows.Count>0)
                    {
                        Grvsummary.Rows.Add("Sales "+ dtb_data.Rows[i][0].ToString()+"%", "", "", "", "", "", "");
                        int rowcoount = Grvsummary.Rows.Count;
                        Grvsummary.Rows[rowcoount - 1].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                        Grvsummary.Rows[rowcoount - 1].DefaultCellStyle.BackColor = Color.Bisque;

                        for (int j = 0; j < dt_gst_data.Rows.Count; j++)
                        {
                                rate = Convert.ToDecimal(dt_gst_data.Rows[j]["rate"].ToString());
                                qty = Convert.ToDecimal(dt_gst_data.Rows[j]["Qty"].ToString());
                                taxbleamount = rate * qty;
                                tax= (taxbleamount* Convert.ToDecimal(dtb_data.Rows[i][0].ToString()))/ 100;
                                totalamount = taxbleamount + tax;
                                total_taxable = total_taxable + taxbleamount;
                                total_tax = total_tax + tax;
                                total_amount = total_amount + totalamount;
                                Grvsummary.Rows.Add(Convert.ToDateTime(dt_gst_data.Rows[j]["InvDate"].ToString()).ToString("dd/MM/yyyy"), dt_gst_data.Rows[j]["InvNumber"].ToString(), dt_gst_data.Rows[j]["cust_name"].ToString(), taxbleamount.ToString("##0.00"), tax.ToString("##0.00"), totalamount.ToString("##0.00"), dt_gst_data.Rows[j]["PayMethod"].ToString());
                            a = a + taxbleamount;
                            b = b + tax;
                            c = c + totalamount;
                            Grvsummary.Rows[j].DefaultCellStyle.BackColor = Color.Bisque;
                            if (dt_gst_data.Rows[j]["PayMethod"].ToString()== "Cash Sale")
                            {
                                cash = cash + Convert.ToDecimal(totalamount);
                            }
                            else if(dt_gst_data.Rows[j]["PayMethod"].ToString() == "Cheque")
                            {
                                cheque = cheque + Convert.ToDecimal(totalamount);
                            }
                            else if (dt_gst_data.Rows[j]["PayMethod"].ToString() == "Card")
                            {
                                card = card + Convert.ToDecimal(totalamount);
                            }
                            else if (dt_gst_data.Rows[j]["PayMethod"].ToString() == "Demand Draft")
                            {
                                dd = dd + Convert.ToDecimal(totalamount);
                            }
                            else if (dt_gst_data.Rows[j]["PayMethod"].ToString() == "PayTM")
                            {
                                PayTM = PayTM + Convert.ToDecimal(totalamount);
                            }
                            else if (dt_gst_data.Rows[j]["PayMethod"].ToString() == "Tez")
                            {
                                Tez = Tez + Convert.ToDecimal(totalamount);
                            }
                            else if (dt_gst_data.Rows[j]["PayMethod"].ToString() == "UPI")
                            {
                                UPI = UPI + Convert.ToDecimal(totalamount);
                            }
                            else if (dt_gst_data.Rows[j]["PayMethod"].ToString() == "NEFT/RTGS/IMPS")
                            {
                                NEFT = NEFT + Convert.ToDecimal(totalamount);
                            }
                            else if (dt_gst_data.Rows[j]["PayMethod"].ToString() == "Netbanking")
                            {
                                Netbanking = Netbanking + Convert.ToDecimal(totalamount);
                            }
                            else if (dt_gst_data.Rows[j]["PayMethod"].ToString() == "Wallets")
                            {
                                Wallets = Wallets + Convert.ToDecimal(totalamount);
                            }
                            else if (dt_gst_data.Rows[j]["PayMethod"].ToString() == "CCAvenue")
                            {
                                CCAvenue = CCAvenue + Convert.ToDecimal(totalamount);
                            }
                            else if (dt_gst_data.Rows[j]["PayMethod"].ToString() == "PayPal")
                            {
                                paypal = paypal + Convert.ToDecimal(totalamount);
                            }
                        }
                        //Grvsummary.Rows.Add("", "", "TOTAL", total_taxable.ToString("##0.00"), total_tax.ToString("##0.00"), total_amount.ToString("##0.00"),"");
                        //int row = Grvsummary.Rows.Count;
                        //Grvsummary.Rows[row - 1].Cells[2].Style.ForeColor = Color.Red;
                        //Grvsummary.Rows[row - 1].Cells[3].Style.ForeColor = Color.Red;
                        //Grvsummary.Rows[row - 1].Cells[4].Style.ForeColor = Color.Red;
                        //Grvsummary.Rows[row - 1].Cells[5].Style.ForeColor = Color.Red;
                        //Grvsummary.Rows[row-1].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                        //Grvsummary.Rows[row - 1].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                        //Grvsummary.Rows[row - 1].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                        //Grvsummary.Rows[row - 1].Cells[5].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                        ls_amount.Text = a.ToString("##0.00");
                        ls_gst.Text = b.ToString("##0.00");
                        label3.Text = c.ToString("##0.00");
                    }
                }
            }
            else
            {
                Lab_Msg.Visible = true;
                ls_amount.Text ="0.00";
                ls_gst.Text = "0.00";
                label3.Text = "0.00";
            }
            
            Grvsummary.Rows.Add("Summa" +
                "ry","Amount","","","","","");
            int row_count = Grvsummary.Rows.Count;
            Grvsummary.Rows[row_count - 1].Cells[0].Style.ForeColor = Color.Blue;
            Grvsummary.Rows[row_count - 1].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Bold);
            Grvsummary.Rows[row_count - 1].Cells[1].Style.ForeColor = Color.Blue;
            Grvsummary.Rows[row_count - 1].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Bold);

            Grvsummary.Rows[row_count - 1].DefaultCellStyle.BackColor = Color.Bisque;
            DataTable dt_paymethod = this.cntrl.DAILY_paymethod(date);
            if (dt_paymethod.Rows.Count > 0)
            {
                if (cash > 0)
                {
                    Grvsummary.Rows.Add("Cash Sale :", cash.ToString("##0.00"), "", "", "", "", "");
                }
                if (cheque > 0)
                {
                    Grvsummary.Rows.Add("Cheque :", cheque.ToString("##0.00"), "", "", "", "", "");
                }
                if (card > 0)
                {
                    Grvsummary.Rows.Add("Card :", card.ToString("##0.00"), "", "", "", "", "");
                }
                if (dd > 0)
                {
                    Grvsummary.Rows.Add("Demand Draft :", dd.ToString("##0.00"), "", "", "", "", "");
                }
                if (PayTM > 0)
                {
                    Grvsummary.Rows.Add("PayTM : ", PayTM.ToString("##0.00"), "", "", "", "", "");
                }
                if (Tez > 0)
                {
                    Grvsummary.Rows.Add("Tez :", Tez.ToString("##0.00"), "", "", "", "", "");
                }
                if (UPI > 0)
                {
                    Grvsummary.Rows.Add("UPI :", UPI.ToString("##0.00"), "", "", "", "", "");
                }
                if (NEFT > 0)
                {
                    Grvsummary.Rows.Add("NEFT/RTGS/IMPS :", NEFT.ToString("##0.00"), "", "", "", "", "");
                }
                if (Netbanking > 0)
                {
                    Grvsummary.Rows.Add("Netbanking :", Netbanking.ToString("##0.00"), "", "", "", "", "");
                }
                if (Wallets > 0)
                {
                    Grvsummary.Rows.Add("Wallets :", Wallets.ToString("##0.00"), "", "", "", "", "");
                }
                if (CCAvenue > 0)
                {
                    Grvsummary.Rows.Add("CCAvenue :", CCAvenue.ToString("##0.00"), "", "", "", "", "");
                }
                if (paypal > 0)
                {
                    Grvsummary.Rows.Add("PayPal :", paypal.ToString("##0.00"), "", "", "", "", "");
                }
            }
        }

        public void fill_data_purchase()///////purchase
        {
            ls_amount.Text = "0.00";
            ls_gst.Text = "0.00";
            label3.Text = "0.00";
            string date = Convert.ToDateTime(dateTimePickerdaily1.Value).ToString("yyyy-MM-dd");
            DataTable dtb_data = this.cntrl.DAILY_Get_gstrate_purchase(date);
            Grvsummary.Rows.Clear();
            Grvsummary.Columns[2].Name = "Supplier Name";//CUSTOMER NAME
            decimal rate = 0, qty = 0, taxbleamount = 0, tax = 0, totalamount = 0, cash = 0, cheque = 0, card = 0, dd = 0, PayTM = 0, paypal = 0, Tez = 0, UPI = 0, NEFT = 0, IMPS = 0, Netbanking = 0, Wallets = 0, CCAvenue = 0,a=0,b=0,c=0;
            if (dtb_data.Rows.Count > 0)
            {
                Lab_Msg.Visible = false;
                Grvsummary.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Grvsummary.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Grvsummary.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Grvsummary.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //Grvsummary.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //Grvsummary.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //Grvsummary.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //Grvsummary.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                for (int i = 0; i < dtb_data.Rows.Count; i++)
                {
                    decimal total_taxable = 0, total_tax = 0, total_amount = 0;
                    DataTable dt_gst_data = this.cntrl.DAILY_GST_AllData_purchase(dtb_data.Rows[i][0].ToString(), date);
                    if (dt_gst_data.Rows.Count > 0)
                    {
                        Grvsummary.Rows.Add("Purchase " + dtb_data.Rows[i][0].ToString() + "%", "", "", "", "", "", "");
                        int rowcoount = Grvsummary.Rows.Count;
                        Grvsummary.Rows[rowcoount - 1].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                        Grvsummary.Rows[rowcoount - 1].DefaultCellStyle.BackColor = Color.Bisque;
                        for (int j = 0; j < dt_gst_data.Rows.Count; j++)
                        {
                            string sup_name = "",paymethod="";
                            DataTable dt_supplier = this.cntrl.get_supplier_name(dt_gst_data.Rows[j]["Sup_Code"].ToString());
                            if(dt_supplier.Rows.Count>0)
                            {
                                sup_name = dt_supplier.Rows[0][1].ToString();
                            }
                            else
                            {
                                sup_name = "";
                            }
                            DataTable dt_pay = this.cntrl.get_paymethod(dt_gst_data.Rows[j]["PurchNumber"].ToString());
                            if(dt_pay.Rows.Count>0)
                            {
                                paymethod = dt_pay.Rows[0][0].ToString();
                            }
                            rate = Convert.ToDecimal(dt_gst_data.Rows[j]["rate"].ToString());
                            qty = Convert.ToDecimal(dt_gst_data.Rows[j]["Qty"].ToString());
                            taxbleamount = rate * qty;
                            tax = (taxbleamount * Convert.ToDecimal(dtb_data.Rows[i][0].ToString())) / 100;
                            totalamount = taxbleamount + tax;
                            total_taxable = total_taxable + taxbleamount;
                            total_tax = total_tax + tax;
                            total_amount = total_amount + totalamount;
                            Grvsummary.Rows.Add(Convert.ToDateTime(dt_gst_data.Rows[j]["PurchDate"].ToString()).ToString("dd/MM/yyyy"), dt_gst_data.Rows[j]["PurchNumber"].ToString(), sup_name, taxbleamount.ToString("##0.00"), tax.ToString("##0.00"), totalamount.ToString("##0.00"), paymethod);
                            a = a + taxbleamount;
                            b = b + tax;
                            c = c + totalamount;
                            Grvsummary.Rows[j].DefaultCellStyle.BackColor = Color.Bisque;
                            if (paymethod == "Cash")
                            {
                                cash = cash + Convert.ToDecimal(totalamount);
                            }
                            else if (paymethod == "Cheque")
                            {
                                cheque = cheque + Convert.ToDecimal(totalamount);
                            }
                            else if (paymethod == "Card")
                            {
                                card = card + Convert.ToDecimal(totalamount);
                            }
                            else if (paymethod == "Demand Draft")
                            {
                                dd = dd + Convert.ToDecimal(totalamount);
                            }
                            else if (paymethod == "PayTM")
                            {
                                PayTM = PayTM + Convert.ToDecimal(totalamount);
                            }
                            else if (paymethod == "Tez")
                            {
                                Tez = Tez + Convert.ToDecimal(totalamount);
                            }
                            else if (paymethod == "UPI")
                            {
                                UPI = UPI + Convert.ToDecimal(totalamount);
                            }
                            else if (paymethod == "NEFT/RTGS/IMPS")
                            {
                                NEFT = NEFT + Convert.ToDecimal(totalamount);
                            }
                            else if (paymethod == "Netbanking")
                            {
                                Netbanking = Netbanking + Convert.ToDecimal(totalamount);
                            }
                            else if (paymethod == "Wallets")
                            {
                                Wallets = Wallets + Convert.ToDecimal(totalamount);
                            }
                            else if (paymethod == "CCAvenue")
                            {
                                CCAvenue = CCAvenue + Convert.ToDecimal(totalamount);
                            }
                            else if (paymethod == "PayPal")
                            {
                                paypal = paypal + Convert.ToDecimal(totalamount);
                            }
                        }
                        //Grvsummary.Rows.Add("", "", "TOTAL", total_taxable.ToString("##0.00"), total_tax.ToString("##0.00"), total_amount.ToString("##0.00"), "");
                        //int row = Grvsummary.Rows.Count;
                        //Grvsummary.Rows[row - 1].Cells[2].Style.ForeColor = Color.Red;
                        //Grvsummary.Rows[row - 1].Cells[3].Style.ForeColor = Color.Red;
                        //Grvsummary.Rows[row - 1].Cells[4].Style.ForeColor = Color.Red;
                        //Grvsummary.Rows[row - 1].Cells[5].Style.ForeColor = Color.Red;
                        //Grvsummary.Rows[row - 1].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                        //Grvsummary.Rows[row - 1].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                        ////Grvsummary.Rows[row - 1].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                        //Grvsummary.Rows[row - 1].Cells[5].Style.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
                        ls_amount.Text = a.ToString("##0.00");
                        ls_gst.Text = b.ToString("##0.00");
                        label3.Text = c.ToString("##0.00");
                    }
                }
            }
            else
            {
                Lab_Msg.Visible = true;
                ls_amount.Text = "0.00";
                ls_gst.Text = "0.00";
                label3.Text = "0.00";
            }

            Grvsummary.Rows.Add("Summa" +
                "ry", "Amount", "", "", "", "", "");
            int row_count = Grvsummary.Rows.Count;
            Grvsummary.Rows[row_count - 1].Cells[0].Style.ForeColor = Color.Blue;
            Grvsummary.Rows[row_count - 1].Cells[0].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Bold);
            Grvsummary.Rows[row_count - 1].Cells[1].Style.ForeColor = Color.Blue;
            Grvsummary.Rows[row_count - 1].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Bold);
            Grvsummary.Rows[row_count - 1].DefaultCellStyle.BackColor = Color.Bisque;
            DataTable dt_paymethod = this.cntrl.DAILY_paymethod(date);
            if (dt_paymethod.Rows.Count > 0)
            {
                if (cash > 0)
                {
                    Grvsummary.Rows.Add("Cash Sale :", cash.ToString("##0.00"), "", "", "", "", "");
                }
                if (cheque > 0)
                {
                    Grvsummary.Rows.Add("Cheque :", cheque.ToString("##0.00"), "", "", "", "", "");
                }
                if (card > 0)
                {
                    Grvsummary.Rows.Add("Card :", card.ToString("##0.00"), "", "", "", "", "");
                }
                if (dd > 0)
                {
                    Grvsummary.Rows.Add("Demand Draft :", dd.ToString("##0.00"), "", "", "", "", "");
                }
                if (PayTM > 0)
                {
                    Grvsummary.Rows.Add("PayTM : ", PayTM.ToString("##0.00"), "", "", "", "", "");
                }
                if (Tez > 0)
                {
                    Grvsummary.Rows.Add("Tez :", Tez.ToString("##0.00"), "", "", "", "", "");
                }
                if (UPI > 0)
                {
                    Grvsummary.Rows.Add("UPI :", UPI.ToString("##0.00"), "", "", "", "", "");
                }
                if (NEFT > 0)
                {
                    Grvsummary.Rows.Add("NEFT/RTGS/IMPS :", NEFT.ToString("##0.00"), "", "", "", "", "");
                }
                if (Netbanking > 0)
                {
                    Grvsummary.Rows.Add("Netbanking :", Netbanking.ToString("##0.00"), "", "", "", "", "");
                }
                if (Wallets > 0)
                {
                    Grvsummary.Rows.Add("Wallets :", Wallets.ToString("##0.00"), "", "", "", "", "");
                }
                if (CCAvenue > 0)
                {
                    Grvsummary.Rows.Add("CCAvenue :", CCAvenue.ToString("##0.00"), "", "", "", "", "");
                }
                if (paypal > 0)
                {
                    Grvsummary.Rows.Add("PayPal :", paypal.ToString("##0.00"), "", "", "", "", "");
                }
            }
        }
    }
}
