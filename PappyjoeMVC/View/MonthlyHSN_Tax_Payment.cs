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
using System.IO;

namespace PappyjoeMVC.View
{
    public partial class MonthlyHSN_Tax_Payment : Form
    {
        Hsn_Report_controller cntrl = new Hsn_Report_controller();
        public string dateFrom = "", dateTo = "", checkStr = "", gst = "", PathName = "", strclinicname = "", clinicn = "", strStreet = "", stremail = "", strwebsite = "", strphone = "", logo_name = "";

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEXPORT_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvhsn.Rows.Count != 0)
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
                        int count = dgvhsn.Columns.Count;
                        ExcelApp.Range[ExcelApp.Cells[1, 1], ExcelApp.Cells[1, count]].Merge();

                        ExcelApp.Cells[1, 1] = "HSN Wise Monthly Tax Payment ";

                        ExcelApp.Cells[1, 1].HorizontalAlignment = HorizontalAlignment.Center;
                        ExcelApp.Cells[1, 1].Font.Size = 12;
                        ExcelApp.Cells[1, 1].Interior.Color = Color.FromArgb(153, 204, 255);
                        ExcelApp.Columns.ColumnWidth = 20;
                        ExcelApp.Cells[2, 1] = "Date";
                        ExcelApp.Cells[2, 1].Font.Size = 10;
                        ExcelApp.Cells[2, 2] = dateTimePickerTo.Value.ToString("dd-MM-yyyy");
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
                        for (int i = 1; i < dgvhsn.Columns.Count + 1; i++)
                        {
                            ExcelApp.Cells[5, i] = dgvhsn.Columns[i - 1].HeaderText;
                            ExcelApp.Cells[5, i].ColumnWidth = 25;
                            ExcelApp.Cells[5, i].EntireRow.Font.Bold = true;
                            ExcelApp.Cells[5, i].Interior.Color = Color.FromArgb(0, 102, 204);
                            ExcelApp.Cells[5, i].Font.Size = 10;
                            ExcelApp.Cells[5, i].Font.Name = "Arial";
                            ExcelApp.Cells[5, i].Font.Color = Color.FromArgb(255, 255, 255);
                            ExcelApp.Cells[5, i].Interior.Color = Color.FromArgb(0, 102, 204);
                        }
                        for (int i = 0; i <= dgvhsn.Rows.Count; i++)
                        {
                            try
                            {
                                for (int j = 0; j < dgvhsn.Columns.Count; j++)
                                {
                                    ExcelApp.Cells[i + 6, j + 1] = dgvhsn.Rows[i].Cells[j].Value.ToString();
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

        private void rad_pur_Click(object sender, EventArgs e)
        {
            rad_pur.Checked = true;
            rad_sales.Checked = false;
            fillgrid();
        }

        private void rad_sales_Click(object sender, EventArgs e)
        {
            rad_pur.Checked = false;
            rad_sales.Checked = true;
            fillgrid();
        }

        private void rad_sales_CheckedChanged(object sender, EventArgs e)
        {

        }

        public MonthlyHSN_Tax_Payment()
        {
            InitializeComponent();
        }

        private void MonthlyHSN_Tax_Payment_Load(object sender, EventArgs e)
        {
            //Lab_Msg.Visible =true;
            DataTable dt4 = this.cntrl.select_gst();
            if (dt4.Rows.Count > 0)
            {
                txt_gst.Text = dt4.Rows[0]["Dl_Number2"].ToString();
            }
            
            fillgrid(); 
        }
        public void fillgrid()
        {
            string from = Convert.ToDateTime(dateTimePickerFrom.Value).ToString("yyyy-MM-dd");
            string date = Convert.ToDateTime(dateTimePickerTo.Value).ToString("yyyy-MM-dd");
            DataTable dtb_data = new DataTable();
            if (rad_pur.Checked == true)
            {
                dtb_data = this.cntrl.Monthly_pur_hsn(from, date);
                //Lab_Msg.Visible = false;



            }
            else
            {
                dtb_data = this.cntrl.Monthly_sale_hsn(from, date);
                //dtb_data = this.cntrl.Monthly_Get_gstrate(from, date);
               // Lab_Msg.Visible = false;

            }
            if (dtb_data.Rows.Count > 0)
            {
                dgvhsn.Rows.Clear();

                for (int i = 0; i < dtb_data.Rows.Count; i++)
                {
                    decimal gst5 = 0, t_gst5 = 0, gst_12 = 0, t_gst12 = 0, gst_18 = 0, t_gst_18 = 0, gst_28 = 0, t_gst_28 = 0, tax_amount = 0, totat_tax_amount = 0, net_amount = 0;
                    dgvhsn.Rows.Add();
                    dgvhsn.Rows[i].Cells["colhsn"].Value = dtb_data.Rows[i]["HSN_Number"].ToString();
                    DataTable dtb_gst = new DataTable();
                    if (rad_pur.Checked == true)
                    {
                        dtb_gst = this.cntrl.Monthly_Get_gstrate_purchase(dtb_data.Rows[i]["HSN_Number"].ToString(), from, date);
                    }
                    else
                    {
                        dtb_gst = this.cntrl.Monthly_Get_gstrate_sale(dtb_data.Rows[i]["HSN_Number"].ToString(), from, date);

                    }
                    for (int l = 0; l < dtb_gst.Rows.Count; l++)
                    {
                        tax_amount = Convert.ToDecimal(dtb_gst.Rows[l]["Qty"].ToString()) * Convert.ToDecimal(dtb_gst.Rows[l]["Rate"].ToString());
                        totat_tax_amount = totat_tax_amount + tax_amount;
                        //dgvhsn.Rows[i].Cells["coltaxamount"].Value = totat_tax_amount;
                        if (Convert.ToDecimal(dtb_gst.Rows[l]["GST"].ToString()) == 5)
                        {
                            gst5 = (tax_amount * Convert.ToDecimal(dtb_gst.Rows[l]["GST"].ToString())) / 100;
                            t_gst5 = t_gst5 + gst5;
                            //dgvhsn.Rows[i].Cells["colrate5"].Value = t_gst5.ToString("##0.00"); 

                        }
                        else if (Convert.ToDecimal(dtb_gst.Rows[l]["GST"].ToString()) == 12)
                        {
                            gst_12 = (tax_amount * Convert.ToDecimal(dtb_gst.Rows[l]["GST"].ToString())) / 100;
                            t_gst12 = t_gst12 + gst_12;
                           // dgvhsn.Rows[i].Cells["colrate12"].Value = t_gst12.ToString("##0.00");
                        }
                        else if (Convert.ToDecimal(dtb_gst.Rows[l]["GST"].ToString()) == 18)
                        {
                            gst_18 = (tax_amount * Convert.ToDecimal(dtb_gst.Rows[l]["GST"].ToString())) / 100;
                            t_gst_18 = t_gst_18 + gst_18;
                           // dgvhsn.Rows[i].Cells["colrate18"].Value = t_gst_18.ToString("##0.00");
                        }
                        else if (Convert.ToDecimal(dtb_gst.Rows[l]["GST"].ToString()) == 28)
                        {
                            gst_28 = (tax_amount * Convert.ToDecimal(dtb_gst.Rows[l]["GST"].ToString())) / 100;
                            t_gst_28 = t_gst_28 + gst_28;
                            //dgvhsn.Rows[i].Cells["colrate28"].Value = t_gst_28.ToString("##0.00");
                        }

                    }
                    net_amount = totat_tax_amount + t_gst5 + t_gst12 + t_gst_18 + t_gst_28;
                    dgvhsn.Rows[i].Cells["coltaxamount"].Value = totat_tax_amount.ToString("##0.00");
                    dgvhsn.Rows[i].Cells["colrate5"].Value = t_gst5.ToString("##0.00");
                    dgvhsn.Rows[i].Cells["colrate12"].Value = t_gst12.ToString("##0.00");
                    dgvhsn.Rows[i].Cells["colrate18"].Value = t_gst_18.ToString("##0.00");
                    dgvhsn.Rows[i].Cells["colrate28"].Value = t_gst_28.ToString("##0.00");
                    dgvhsn.Rows[i].Cells["coltotalamount"].Value = net_amount.ToString("##0.00"); 
                }

            }
          //  Lab_Msg.Visible =false;
        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            fillgrid();
            Lab_Msg.Visible = false;

        }

        private void btnprint_Click(object sender, EventArgs e)
        {
          try
          {

           
             if (dgvhsn.Rows.Count > 0)
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
                        gst = dtp.Rows[0]["Dl_Number2"].ToString();
                    }
                }
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                System.IO.StreamWriter sWrite = new System.IO.StreamWriter(Apppath + "\\MonthlyHSN_Tax_Payment.html");
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
                if (rad_pur.Checked == true)
                    sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3>MONTHLY GST REPORT (PURCHASE)  </font></center></b></td>");
                else
                    sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3>MONTHLY GST REPORT (SALES) </font></center></b></td>");

                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan=7 align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> <b>Date:</b>" + dateTimePickerFrom.Value.ToString("dd/MM/yyyy") + " </font></td> ");
                sWrite.WriteLine("</tr>");

                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan=7 align='left' colspan=7><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>Printed Date:</b>" + today + "</font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan=7 align='left' colspan=7><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>Gst:</b>" + gst + "</font></td>");
                sWrite.WriteLine("</tr>");
                    if (dgvhsn.Rows.Count > 0)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("    <td align='left' width='12%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3 ><b>&nbsp;HSN</b></font></td>");
                       // sWrite.WriteLine("    <td align='left' width='12%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;HSN</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='18%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;Non Taxable Amount(INR)</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;5% Taxable Amount(INR)</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='12%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;12% Taxable Amount(INR)</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='18%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;18% Taxable Amount(INR)</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp; 28% Taxable Amount(INR)</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='40%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Total(INR)</b></font></td>");
                        sWrite.WriteLine("</tr>");
                        for ( int c = 0; c < dgvhsn.Rows.Count; c++)
                        {
                            sWrite.WriteLine("<tr>");
                            //if (dgvhsn.Rows[c].Cells[0].Value.ToString() != "" && dgvhsn.Rows[c].Cells[0].Value.ToString() !=null)
                            {
                                sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvhsn.Rows[c].Cells[0].Value.ToString() + "</font></td>");
                                //if(dgvhsn.Rows[c].Cells[1].Value!=null && dgvhsn.Rows[c].Cells[1].Value.ToString()!="")
                                //   sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvhsn.Rows[c].Cells[1].Value.ToString() + "</font></td>");
                                //else
                                //    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvhsn.Rows[c].Cells[2].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvhsn.Rows[c].Cells[3].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvhsn.Rows[c].Cells[4].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvhsn.Rows[c].Cells[5].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvhsn.Rows[c].Cells[6].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvhsn.Rows[c].Cells[7].Value.ToString() + "</font></td>");

                            }
                            //else
                            //{
                            //    sWrite.WriteLine("    <td align='right' style='border:1px solid #000'colspan=2 ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>" + dgvhsn.Rows[c].Cells[0].Value.ToString() + "</b></font></td>");
                            //    //if (dgvhsn.Rows[c].Cells[1].Value != null && dgvhsn.Rows[c].Cells[1].Value.ToString() != "")
                            //    //    sWrite.WriteLine("    <td align='right' style='border:1px solid #000'colspan=2 ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>" + dgvhsn.Rows[c].Cells[1].Value.ToString() + "</b></font></td>");
                            //    //else
                            //    //    sWrite.WriteLine("    <td align='right' style='border:1px solid #000'colspan=2 ><FONT COLOR=black FACE='Segoe UI' SIZE=2></b></font></td>");
                            //    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>" + dgvhsn.Rows[c].Cells[2].Value.ToString() + "</b></font></td>");
                            //    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>" + dgvhsn.Rows[c].Cells[3].Value.ToString() + "</b></font></td>");
                            //    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>" + dgvhsn.Rows[c].Cells[4].Value.ToString() + "</b></font></td>");
                            //    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>" + dgvhsn.Rows[c].Cells[5].Value.ToString() + "</b></font></td>");
                            //    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>" + dgvhsn.Rows[c].Cells[6].Value.ToString() + "</b></font></td>");
                            //    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;<b>" + dgvhsn.Rows[c].Cells[7].Value.ToString() + "</b></font></td>");
                            //}
                            sWrite.WriteLine("</tr>");
                        }
                        sWrite.WriteLine("</table>");
                        sWrite.WriteLine("</div>");
                        sWrite.WriteLine("<script>window.print();</script>");
                        sWrite.WriteLine("</body>");
                        sWrite.WriteLine("</html>");
                        sWrite.Close();
                        System.Diagnostics.Process.Start(Apppath + "\\MonthlyHSN_Tax_Payment.html");
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

    }
}
