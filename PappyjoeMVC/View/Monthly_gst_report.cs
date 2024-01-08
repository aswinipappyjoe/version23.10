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
    public partial class Monthly_gst_report : Form
    {
        gst_controller cntrl = new gst_controller();
        public string dateFrom = "", dateTo = "", checkStr = "", PathName = "", strclinicname = "", clinicn = "", strStreet = "", stremail = "", strwebsite = "", strphone = "", logo_name = "";

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Grvsummary_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void rad_sales_Click(object sender, EventArgs e)
        {
            rad_pur.Checked = false;
            rad_sales.Checked = true;
            fill_data();
        }

        private void rad_pur_Click(object sender, EventArgs e)
        {
            rad_pur.Checked = true;
            rad_sales.Checked = false;
            fill_data();
        }

        private void rad_pur_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnEXPORT_Click(object sender, EventArgs e)
        {
            try
            {
                if (Grvsummary.Rows.Count != 0)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Excel Files (*.xls)|*.xls";
                    saveFileDialog1.FileName = "Monthl GST Report(" + DateTime.Now.ToString("dd-MM-yy h.mm.ss tt") + ").xls";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        PathName = saveFileDialog1.FileName;
                        Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                        ExcelApp.Application.Workbooks.Add(Type.Missing);
                        ExcelApp.Columns.ColumnWidth = 20;
                        int count = Grvsummary.Columns.Count;
                        ExcelApp.Range[ExcelApp.Cells[1, 1], ExcelApp.Cells[1, count]].Merge();

                        ExcelApp.Cells[1, 1] = "MONTHLY GST REPORT ";

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

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (Grvsummary.Rows.Count > 0)
                {
                    string gst = "";
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
                            logo_name = dtp.Rows[0]["path"].ToString(); gst = dtp.Rows[0]["Dl_Number2"].ToString();
                        }
                    }
                    string Apppath = System.IO.Directory.GetCurrentDirectory();
                    System.IO.StreamWriter sWrite = new System.IO.StreamWriter(Apppath + "\\monthly_gst_report.html");
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
                        sWrite.WriteLine("</table>");
                    }
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr><td align='left'  ><hr/></td></tr>");
                    sWrite.WriteLine("</table>");
                    sWrite.WriteLine("<tr>");
                    if(rad_pur.Checked==true)
                       sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3>MONTHLY GST REPORT (PURCHASE)  </font></center></b></td>");
                    else
                        sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3>MONTHLY GST REPORT (SALES) </font></center></b></td>");

                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=7 align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> <b>Date:</b>" + dateTimePickerdaily1.Value.ToString("dd/MM/yyyy") + " </font></td> ");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=7 align='left' colspan=7><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>Printed Date:</b>" + today + "</font></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=7 align='left' colspan=7><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>Gst:</b>" + gst + "</font></td>");
                    sWrite.WriteLine("</tr>");
                    if (Grvsummary.Rows.Count > 0)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("    <td align='left' width='12%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3 ><b>&nbsp;Date</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='12%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp; Bill No</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='18%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;Non Taxable Amount(INR)</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;5% Taxable Amount(INR)</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='12%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;12% Taxable Amount(INR)</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='18%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;18% Taxable Amount(INR)</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp; 28% Taxable Amount(INR)</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='40%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Total(INR)</b></font></td>");
                        sWrite.WriteLine("</tr>");
                        for (int c = 0; c < Grvsummary.Rows.Count; c++)
                        {
                            sWrite.WriteLine("<tr>");
                            if (Grvsummary.Rows[c].Cells[0].Value.ToString() != "")
                            {
                                sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells[0].Value.ToString() + "</font></td>");
                                    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells[1].Value.ToString() + "</font></td>");
                                    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells[2].Value.ToString() + "</font></td>");
                                    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells[3].Value.ToString() + "</font></td>");
                                    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells[4].Value.ToString() + "</font></td>");
                                    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells[5].Value.ToString() + "</font></td>");
                                    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells[6].Value.ToString() + "</font></td>");
                            sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Grvsummary.Rows[c].Cells[7].Value.ToString() + "</font></td>");
                            }
                            else
                            {
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000'colspan=2 ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>" + Grvsummary.Rows[c].Cells[1].Value.ToString() + "</b></font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>" + Grvsummary.Rows[c].Cells[2].Value.ToString() + "</b></font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>" + Grvsummary.Rows[c].Cells[3].Value.ToString() + "</b></font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>" + Grvsummary.Rows[c].Cells[4].Value.ToString() + "</b></font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>" + Grvsummary.Rows[c].Cells[5].Value.ToString() + "</b></font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>" + Grvsummary.Rows[c].Cells[6].Value.ToString() + "</b></font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>" + Grvsummary.Rows[c].Cells[7].Value.ToString() + "</b></font></td>");
                            }
                            sWrite.WriteLine("</tr>");
                        }
                        sWrite.WriteLine("</table>");
                        sWrite.WriteLine("</div>");
                        sWrite.WriteLine("<script>window.print();</script>");
                        sWrite.WriteLine("</body>");
                        sWrite.WriteLine("</html>");
                        sWrite.Close();
                        System.Diagnostics.Process.Start(Apppath + "\\monthly_gst_report.html");
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
            fill_data();
        }

        public Monthly_gst_report()
        {
            InitializeComponent();
        }

        private void Monthly_gst_report_Load(object sender, EventArgs e)
        {
            try
            {
                Lab_Msg.Hide();
                DateTime now = DateTime.Now;
                DateTime date = new DateTime(now.Year, now.Month, 1);
                dtp1ReceptReceivedPerMonth1.Value = date;
                Grvsummary.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
                Grvsummary.EnableHeadersVisualStyles = false;
                this.Grvsummary.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
                Grvsummary.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                Grvsummary.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 9, FontStyle.Regular);
                //Grvsummary.Rows[].DefaultCellStyle.BackColor = Color.Bisque;
                //Grvsummary.Rows[].DefaultCellStyle.ForeColor = Color.Black;
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
            string from = Convert.ToDateTime(dtp1ReceptReceivedPerMonth1.Value).ToString("yyyy-MM-dd");
            string date = Convert.ToDateTime(dateTimePickerdaily1.Value).ToString("yyyy-MM-dd");
            DataTable dtb_data = new DataTable();
            if (rad_pur.Checked==true)
            {
                dtb_data = this.cntrl.Monthly_Get_gstrate_purchase(from, date);

            }
            else
            {
                 dtb_data = this.cntrl.Monthly_Get_gstrate(from, date);

            }
            Grvsummary.Rows.Clear();
            Grvsummary.Columns.Clear();
            Grvsummary.ColumnCount = 8;
            Grvsummary.Columns[0].Name = "Date";
            Grvsummary.Columns[1].Name = "Bill No";
            Grvsummary.Columns[2].Name = " Non Taxable Amount";
            Grvsummary.Columns[3].Name = "5% Taxable Amount";
            Grvsummary.Columns[4].Name = "12% Taxable Amount";
            Grvsummary.Columns[5].Name = "18% Taxable Amount";
            Grvsummary.Columns[6].Name = "28% Taxable Amount";
            Grvsummary.Columns[7].Name = "Total";
            Grvsummary.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            Grvsummary.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grvsummary.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //int k = 3;

            //if (dtb_data.Rows.Count > 0)
            //{

            //    for (int i = 0; i < dtb_data.Rows.Count; i++)
            //    {
            //        Grvsummary.Columns[k].Name = dtb_data.Rows[i][0].ToString()+ "% TAXABLE AMOUNT ";
            //        k++;
            //    }

            //}
            DataTable dt_gst_data = new DataTable();
            if (rad_pur.Checked == true)
            {
                dt_gst_data = this.cntrl.Monthly_GST_AllData_purchase(from, date);

            }
            else
                 dt_gst_data = this.cntrl.Monthly_GST_AllData(from, date);
            if (dt_gst_data.Rows.Count > 0)
            {
                Lab_Msg.Visible = false;
                decimal total_tax = 0, total2 = 0, total3 = 0, total4 = 0, total5 = 0, total6 = 0;
                for (int j = 0; j < dt_gst_data.Rows.Count; j++)
                {
                    decimal gst5 = 0, t_gst5 = 0, gst_12 = 0, t_gst12 = 0, gst_18 = 0, t_gst_18 = 0, gst_28 = 0, t_gst_28 = 0, tax_amount = 0, totat_tax_amount = 0, net_amount = 0; DataTable dt_salecount = new DataTable();
                    if (rad_pur.Checked == true)
                         dt_salecount = this.cntrl.Monthly_GST_count_purchase(Convert.ToDateTime(dt_gst_data.Rows[j]["PurchDate"].ToString()).ToString("yyyy-MM-dd"));
                    else
                        dt_salecount = this.cntrl.Monthly_GST_count(Convert.ToDateTime(dt_gst_data.Rows[j]["InvDate"].ToString()).ToString("yyyy-MM-dd"));

                    for (int i = 0; i < dtb_data.Rows.Count; i++)
                    {
                        DataTable dt_gst_rate = new DataTable();
                         
                        if (rad_pur.Checked == true)
                            dt_gst_rate = this.cntrl.Monthly_GST_rate_purchase(Convert.ToDateTime(dt_gst_data.Rows[j]["PurchDate"].ToString()).ToString("yyyy-MM-dd"), dtb_data.Rows[i][0].ToString());
                        else
                            dt_gst_rate = this.cntrl.Monthly_GST_rate(Convert.ToDateTime(dt_gst_data.Rows[j]["InvDate"].ToString()).ToString("yyyy-MM-dd"), dtb_data.Rows[i][0].ToString());
                        for (int l = 0; l < dt_gst_rate.Rows.Count; l++)
                            {
                                tax_amount = Convert.ToDecimal(dt_gst_rate.Rows[l]["Qty"].ToString()) * Convert.ToDecimal(dt_gst_rate.Rows[l]["Rate"].ToString());
                                totat_tax_amount = totat_tax_amount + tax_amount;
                                if (Convert.ToDecimal(dt_gst_rate.Rows[l]["GST"].ToString()) == 5)
                                {
                                    gst5 = (tax_amount * Convert.ToDecimal(dt_gst_rate.Rows[l]["GST"].ToString())) / 100;
                                    t_gst5 = t_gst5 + gst5;
                                }
                                else if (Convert.ToDecimal(dt_gst_rate.Rows[l]["GST"].ToString()) == 12)
                                {
                                    gst_12 = (tax_amount * Convert.ToDecimal(dt_gst_rate.Rows[l]["GST"].ToString())) / 100;
                                    t_gst12 = t_gst12 + gst_12;
                                }
                                else if (Convert.ToDecimal(dt_gst_rate.Rows[l]["GST"].ToString()) == 18)
                                {
                                    gst_18 = (tax_amount * Convert.ToDecimal(dt_gst_rate.Rows[l]["GST"].ToString())) / 100;
                                    t_gst_18 = t_gst_18 + gst_18;
                                }
                                else if (Convert.ToDecimal(dt_gst_rate.Rows[l]["GST"].ToString()) == 28)
                                {
                                    gst_28 = (tax_amount * Convert.ToDecimal(dt_gst_rate.Rows[l]["GST"].ToString())) / 100;
                                    t_gst_28 = t_gst_28 + gst_28;
                                }
                            }
                        //}
                    }
                    net_amount = totat_tax_amount + t_gst5 + t_gst12 + t_gst_18 + t_gst_28;
                    Grvsummary.Rows.Add();
                    if (rad_pur.Checked == true)
                        Grvsummary.Rows[j].Cells[0].Value = Convert.ToDateTime(dt_gst_data.Rows[j]["PurchDate"].ToString()).ToString("dd/MM/yyyy");
                    else
                        Grvsummary.Rows[j].Cells[0].Value = Convert.ToDateTime(dt_gst_data.Rows[j]["InvDate"].ToString()).ToString("dd/MM/yyyy");
                    Grvsummary.Rows[j].Cells[1].Value = dt_salecount.Rows[0]["first"].ToString() + " to " + dt_salecount.Rows[0]["last"].ToString();
                    Grvsummary.Rows[j].Cells[2].Value = totat_tax_amount.ToString("##0.00");
                    Grvsummary.Rows[j].Cells[3].Value = t_gst5.ToString("##0.00");
                    Grvsummary.Rows[j].Cells[4].Value = t_gst12.ToString("##0.00");
                    Grvsummary.Rows[j].Cells[5].Value = t_gst_18.ToString("##0.00");
                    Grvsummary.Rows[j].Cells[6].Value = t_gst_28.ToString("##0.00");
                    Grvsummary.Rows[j].Cells[7].Value = net_amount.ToString("##0.00");
                    Grvsummary.Rows[j].DefaultCellStyle.BackColor = Color.White;
                    Grvsummary.Rows[j].DefaultCellStyle.ForeColor = Color.Black;
                    //Grvsummary.Rows.Add(Convert.ToDateTime(dt_gst_data.Rows[j]["InvDate"].ToString()).ToString("dd/MM/yyyy"), dt_salecount.Rows[j]["first"].ToString() + " to " + dt_salecount.Rows[j]["last"].ToString(), totat_tax_amount.ToString("##0.00"), t_gst5.ToString("##0.00"), t_gst12.ToString("##0.00"), t_gst_18.ToString("##0.00"), t_gst_28.ToString("##0.00"), net_amount.ToString("##0.00"));


                    total_tax = total_tax + totat_tax_amount;
                    total2 = total2 + t_gst5;
                    total3 = total3 + t_gst12;
                    total4 = total4 + t_gst_18;
                    total5 = total5 + t_gst_28;
                    total6 = total6 + net_amount;
                }
                //Grvsummary.Rows.Add("", "TOTAL :", total_tax.ToString("##0.00"), total2.ToString("##0.00"), total3.ToString("##0.00"), total4.ToString("##0.00"), total5.ToString("##0.00"), total6.ToString("##0.00"));
                label5.Text = total_tax.ToString("##0.00");
                label4.Text = total2.ToString("##0.00");
                ls_amount.Text = total3.ToString("##0.00");
                ls_gst.Text = total4.ToString("##0.00");
                label7.Text = total5.ToString("##0.00");
                label6.Text = total6.ToString("##0.00");
                //int row = Grvsummary.Rows.Count;
                //Grvsummary.Rows[row - 1].Cells[1].Style.ForeColor = Color.Red;
                //Grvsummary.Rows[row - 1].Cells[2].Style.ForeColor = Color.Red;
                //Grvsummary.Rows[row - 1].Cells[3].Style.ForeColor = Color.Red;
                //Grvsummary.Rows[row - 1].Cells[4].Style.ForeColor = Color.Red;
                //Grvsummary.Rows[row - 1].Cells[5].Style.ForeColor = Color.Red;
                //Grvsummary.Rows[row - 1].Cells[6].Style.ForeColor = Color.Red;
                //Grvsummary.Rows[row - 1].Cells[7].Style.ForeColor = Color.Red;

                //Grvsummary.Rows[row - 1].Cells[1].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                //Grvsummary.Rows[row - 1].Cells[2].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                //Grvsummary.Rows[row - 1].Cells[3].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                //Grvsummary.Rows[row - 1].Cells[4].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                //Grvsummary.Rows[row - 1].Cells[5].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                //Grvsummary.Rows[row - 1].Cells[6].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                //Grvsummary.Rows[row - 1].Cells[7].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
            }
            else
            {
                Lab_Msg.Visible = true;
            }
        }
    }
}
