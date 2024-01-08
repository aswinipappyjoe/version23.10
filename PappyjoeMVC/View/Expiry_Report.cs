using PappyjoeMVC.Controller;
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

namespace PappyjoeMVC.View
{
    public partial class Expiry_Report : Form
    {
        int flag;
        
        public Expiry_Report()
        {
            
            InitializeComponent();
        }
        stock_controller ct = new stock_controller();
        Expiry_Report_controller ctrlr = new Expiry_Report_controller();
        int grosspurchaserate = 0, grossstockvalue = 0;
        public string dateFrom = "", dateTo = "", checkStr = "0", PathName = "", strclinicname = "", strStreet = "", stremail = "", strwebsite = "", strphone = "", clinicn = "", logo_name = "";

        private void comboBox1r_SelectedIndexChanged(object sender, EventArgs e)//sanoop start
        {
            flag = 0;
        }//sanoop end

        private void dgvExpiry_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void fill_90days()
        {
            DateTime to = DateTime.Now.Date;
            DataTable dtf;
            int slno, m = 0;
            Lab_Msg.Visible = false;
            dgvExpiry.Rows.Clear();
             DataTable dtb = ctrlr.getbatch( comboBox1.Text, flag, to, dptTo.Value);
            if(dtb.Rows.Count>0)
            {
                for (int j = 0; j < dtb.Rows.Count; j++)
                {
                    DataTable dti = ctrlr.getitems(dtb.Rows[j]["Item_Code"].ToString());
                    DateTime from = Convert.ToDateTime(dtb.Rows[j]["ExpDate"].ToString());
                    double rr;
                    rr = (from - to).TotalDays;
                    dgvExpiry.Rows.Add();
                    slno = m + 1;
                    dgvExpiry.Rows[m].Cells["col_slno"].Value = slno;
                    dgvExpiry.Rows[m].Cells["ItemCode"].Value = dti.Rows[0]["item_code"].ToString();
                    dgvExpiry.Rows[m].Cells["ItemName"].Value = dti.Rows[0]["item_name"].ToString();
                    dgvExpiry.Rows[m].Cells["PurchNumber"].Value = dtb.Rows[j]["PurchNumber"].ToString();
                    dgvExpiry.Rows[m].Cells["PurchaseDate"].Value = Convert.ToDateTime(dtb.Rows[j]["PurchDate"].ToString()).ToString("dd/MM/yyyy");
                    dgvExpiry.Rows[m].Cells["BatchNumber"].Value = dtb.Rows[j]["BatchNumber"].ToString();
                    dgvExpiry.Rows[m].Cells["Quantity"].Value = dtb.Rows[j]["Qty"].ToString();
                    dgvExpiry.Rows[m].Cells["ExpiryDate"].Value = Convert.ToDateTime(dtb.Rows[j]["ExpDate"].ToString()).ToString("dd/MM/yyyy");
                    dtf = ctrlr.findsupplier(Convert.ToInt32(dtb.Rows[j]["Sup_Code"].ToString()));
                    dgvExpiry.Rows[m].Cells["ColSup"].Value = dtf.Rows[0]["Supplier_Name"].ToString();
                    dgvExpiry.Rows[m].Cells["stock"].Value = Convert.ToInt32(dtb.Rows[j]["Qty"]) * Convert.ToInt32(dti.Rows[0]["Purch_Rate"]);
                    dgvExpiry.Rows[m].Cells["purchaserate"].Value = dti.Rows[0]["Purch_Rate"].ToString();//sanoop
                    grossstockvalue = grossstockvalue + (Convert.ToInt32(dtb.Rows[j]["Qty"]) * Convert.ToInt32(dti.Rows[0]["Purch_Rate"]));
                    grosspurchaserate = grosspurchaserate + Convert.ToInt32(dti.Rows[0]["Purch_Rate"]);
                    TimeSpan s = Convert.ToDateTime(dtb.Rows[j]["ExpDate"]) - DateTime.Now;
                    dgvExpiry.Rows[m].Cells["daystoexpire"].Value = s.Days;
                    m = m + 1;
                }
                ls_amount.Text = grosspurchaserate.ToString("0.00");
            }
            else
            {
                ls_amount.Text = "0.00"; Lab_Msg.Visible = true;
            }
                   
        }

     
        private void btnselect_Click(object sender, EventArgs e)
        {

            flag = 1;
            fill_90days();
        }
        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                int c = 0;
                if (dgvExpiry.Rows.Count > 0)
                {
                    DataTable dt1 = new DataTable();
                    string message = "Did you want Header on Print?";
                    string caption = "Verification";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        DataTable dtp = this.ctrlr.practicedetails();
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
                    StreamWriter sWrite = new StreamWriter(Apppath + "\\ExpiryDatewiseReport.html");
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
                    sWrite.WriteLine("<tr><td align='left'  ><hr/></td></tr>");
                    sWrite.WriteLine("</table>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3> EXPIRY DATE WISE  REPORT </font></center></b></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=7 align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>From:</b>  " + dptFrom.Value.ToString("dd/MM/yyyy") + " </font></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=7 align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>To:</b>  " + dptTo.Value.ToString("dd/MM/yyyy") + " </font></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=7 align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2> <b>Printed Date :</b>  " + DateTime.Now.ToString("dd/MM/yyyy") + " </font></td>");
                    sWrite.WriteLine("</tr>");
                    if (dgvExpiry.Rows.Count > 0)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("    <td align='left' width='4%'style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3 ><b>&nbsp;Slno.</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='25%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3 ><b>Item code</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='24%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Item Name</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='25%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Purchase No</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='34%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b> Purch Date</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='25%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b> Batch Number</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='25%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Quantity</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='70%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Expiry Date</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='50%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b> Supplier Name</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='25%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Purchase Rate</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='70%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Stock Value</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='50%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b> Days To Expiry</b></font></td>");

                        sWrite.WriteLine("</tr>");
                        while (c < dgvExpiry.Rows.Count)
                        {
                            sWrite.WriteLine("<tr>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvExpiry.Rows[c].Cells["col_slno"].Value.ToString() + "</font></td>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvExpiry.Rows[c].Cells["ItemCode"].Value.ToString() + "</font></td>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvExpiry.Rows[c].Cells["ItemName"].Value.ToString() + "</font></td>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvExpiry.Rows[c].Cells["PurchNumber"].Value.ToString() + "</font></td>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvExpiry.Rows[c].Cells["PurchaseDate"].Value.ToString() + "</font></td>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvExpiry.Rows[c].Cells["BatchNumber"].Value.ToString() + "&nbsp;</font></td>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvExpiry.Rows[c].Cells["Quantity"].Value.ToString() + "&nbsp;</font></td>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvExpiry.Rows[c].Cells["ExpiryDate"].Value.ToString() + "&nbsp;</font></td>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvExpiry.Rows[c].Cells["ColSup"].Value.ToString() + "&nbsp;</font></td>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvExpiry.Rows[c].Cells["purchaserate"].Value.ToString() + "&nbsp;</font></td>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvExpiry.Rows[c].Cells["stock"].Value.ToString() + "&nbsp;</font></td>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvExpiry.Rows[c].Cells["daystoexpire"].Value.ToString() + "&nbsp;</font></td>");
                            sWrite.WriteLine("</tr>");
                            c++;
                        }
                    }
                    sWrite.WriteLine("</table>");
                    sWrite.WriteLine("</div>");
                    sWrite.WriteLine("<script>window.print();</script>");
                    sWrite.WriteLine("</body>");
                    sWrite.WriteLine("</html>");
                    sWrite.Close();
                    System.Diagnostics.Process.Start(Apppath + "\\ExpiryDatewiseReport.html");
                }
                else
                {
                    MessageBox.Show("Record Not Found,please change the date and try again !..", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error!...", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvExpiry.Rows.Count != 0)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Excel Files (*.xls)|*.xls";
                    saveFileDialog1.FileName = "Expiry Date Wise Report(" + DateTime.Now.ToString("dd-MM-yy h.mm.ss tt") + ").xls";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        PathName = saveFileDialog1.FileName;
                        Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                        ExcelApp.Application.Workbooks.Add(Type.Missing);
                        ExcelApp.Columns.ColumnWidth = 20;
                        int count = dgvExpiry.Columns.Count;
                        ExcelApp.Range[ExcelApp.Cells[1, 1], ExcelApp.Cells[1, count]].Merge();
                        ExcelApp.Cells[1, 1] = "Expiry Date Wise Report";
                        ExcelApp.Cells[1, 1].HorizontalAlignment = HorizontalAlignment.Center;
                        ExcelApp.Cells[1, 1].Font.Size = 12;
                        ExcelApp.Cells[1, 1].Interior.Color = Color.FromArgb(153, 204, 255);
                        ExcelApp.Columns.ColumnWidth = 20;
                        ExcelApp.Cells[2, 1] = "From Date";
                        ExcelApp.Cells[2, 1].Font.Size = 10;
                        ExcelApp.Cells[2, 2] = dptFrom.Value.ToString("dd-MM-yyyy");
                        ExcelApp.Cells[2, 2].Font.Size = 10;
                        ExcelApp.Cells[3, 1] = "To Date";
                        ExcelApp.Cells[3, 1].Font.Size = 10;
                        ExcelApp.Cells[3, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        ExcelApp.Cells[3, 2] = dptTo.Value.ToString("dd-MM-yyyy");
                        ExcelApp.Cells[3, 2].Font.Size = 10;
                        ExcelApp.Cells[4, 1] = "Running Date";
                        ExcelApp.Cells[4, 1].Font.Size = 10;
                        ExcelApp.Cells[4, 2] = DateTime.Now.ToString("dd-MM-yyyy");
                        ExcelApp.Cells[4, 2].Font.Size = 10;
                        ExcelApp.Cells[4, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        for (int i = 1; i < dgvExpiry.Columns.Count + 1; i++)
                        {
                            ExcelApp.Cells[5, i] = dgvExpiry.Columns[i - 1].HeaderText;
                            ExcelApp.Cells[5, i].ColumnWidth = 25;
                            ExcelApp.Cells[5, i].EntireRow.Font.Bold = true;
                            ExcelApp.Cells[5, i].Interior.Color = Color.FromArgb(0, 102, 204);
                            ExcelApp.Cells[5, i].Font.Size = 10;
                            ExcelApp.Cells[5, i].Font.Name = "Segoe UI";
                            ExcelApp.Cells[5, i].Font.Color = Color.FromArgb(255, 255, 255);
                            ExcelApp.Cells[5, i].Interior.Color = Color.FromArgb(0, 102, 204);
                        }
                        for (int i = 0; i <= dgvExpiry.Rows.Count; i++)
                        {
                            try
                            {
                                for (int j = 0; j < dgvExpiry.Columns.Count; j++)
                                {
                                    ExcelApp.Cells[i + 6, j + 1] = dgvExpiry.Rows[i].Cells[j].Value.ToString();
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
            var form2 = new Expiry_Report();
            form2.FormClosed += (sender1, args) => this.Close();
            this.Hide();
        }
        private void Expiry_Report_Load(object sender, EventArgs e)
        {
            try
            {
                grossstockvalue = 0;
                grosspurchaserate = 0;
                DataTable dts = ct.LoadSupplier();
                comboBox1.DisplayMember = "Supplier_Name";
                comboBox1.ValueMember = "Supplier_Code";
                DataRow row = dts.NewRow();
                row["Supplier_Name"] = "All";
                dts.Rows.InsertAt(row, 0);
                comboBox1.DataSource = dts;
                dptTo.Format = DateTimePickerFormat.Short;
                dptTo.MinDate = DateTime.Today.Date;
                flag = 0;
                fill_90days();
                dgvExpiry.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
                dgvExpiry.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvExpiry.EnableHeadersVisualStyles = false;
                dgvExpiry.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 8, FontStyle.Regular);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error!...", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
