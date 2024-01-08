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
{//sanoop start
    public partial class AlreadyExpired : Form
    {
        int flag;
        int grosspurchaserate;
        int grossstockvalue;
        stock_controller ct = new stock_controller();
        Expiry_Report_controller ctrlr = new Expiry_Report_controller();
        public string fdate = "", tdate = "", strclinicname = "", clinicn = "", strStreet = "", stremail = "", strwebsite = "", strphone = "", checkStr = "0", PathName = "", logo_name = "";

        

        public AlreadyExpired()
        {
            InitializeComponent();
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                //int c = 0;
                if (dataGridView1.Rows.Count > 0)
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
                    StreamWriter sWrite = new StreamWriter(Apppath + "\\AlreadyExpiryReport.html");
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
                    if (!System.IO.File.Exists(Appath + "\\" + logo_name))
                    {
                        sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td  align='left' height='20px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + strclinicname + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td  align='left' height='20px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;" + strStreet + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td align='left' height='20px' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + strphone + "</font></td></tr>");

                        sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");

                        sWrite.WriteLine("</table>");
                    }
                    else
                    {
                        sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td width='30px' height='50px' align='left' rowspan='3'><img src='" + Appath + "\\" + logo_name + "'style='width:70px;height:70px;' ></td>  ");
                        sWrite.WriteLine("<td width='870px' align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=4><b>&nbsp;" + strclinicname + "</font> <br><FONT  COLOR=black  face='Segoe UI' SIZE=2>&nbsp;" + strStreet + "<br>&nbsp;" + strphone + " </b></td></tr>");
                        sWrite.WriteLine("</table>");
                    }
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr><td align='left'  ><hr/></td></tr>");
                    sWrite.WriteLine("</table>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3> ALREADY EXPIRED REPORT </font></center></b></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<table align='center' style='width:800px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=7 align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>From:</b>  " + dptFrom.Value.ToString("dd/MM/yyyy") + " </font></td>");
                    sWrite.WriteLine("</tr>");
                    //sWrite.WriteLine("<tr>");
                    //sWrite.WriteLine("<td colspan=7 align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>To:</b>  " + dptTo.Value.ToString("dd/MM/yyyy") + " </font></td>");
                    //sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=7 align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2> <b>Printed Date :</b>  " + DateTime.Now.ToString("dd/MM/yyyy") + " </font></td>");
                    sWrite.WriteLine("</tr>");
                    if (dataGridView1.Rows.Count > 0)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("    <td align='left' width='4%'style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3 ><b>&nbsp;Slno.</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='20%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3 ><b>Item Code</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Item Name</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Purchase No</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b> Purchase Date</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b> Batch Number</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Quantity</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Expiry Date</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b> Supplier Name</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b> Purchase Rate</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b> Stock Value </b></font></td>");
                        sWrite.WriteLine("</tr>");
                        for(int c=0;c < dataGridView1.Rows.Count; c++)
                        {
                            sWrite.WriteLine("<tr>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dataGridView1.Rows[c].Cells["col_sln"].Value.ToString() + "</font></td>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dataGridView1.Rows[c].Cells["ItemCod"].Value.ToString() + "</font></td>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dataGridView1.Rows[c].Cells["ItemNam"].Value.ToString() + "</font></td>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dataGridView1.Rows[c].Cells["PurchNumbe"].Value.ToString() + "</font></td>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dataGridView1.Rows[c].Cells["PurchaseDat"].Value.ToString() + "</font></td>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dataGridView1.Rows[c].Cells["BatchNumbe"].Value.ToString() + "&nbsp;</font></td>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dataGridView1.Rows[c].Cells["Quantit"].Value.ToString() + "&nbsp;</font></td>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dataGridView1.Rows[c].Cells["ExpiryDat"].Value.ToString() + "&nbsp;</font></td>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dataGridView1.Rows[c].Cells["ColSu"].Value.ToString() + "&nbsp;</font></td>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dataGridView1.Rows[c].Cells["purchaserate"].Value.ToString() + "&nbsp;</font></td>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dataGridView1.Rows[c].Cells["stock"].Value.ToString() + "&nbsp;</font></td>");
                            sWrite.WriteLine("</tr>");
                           // c++;
                        }
                    }
                    sWrite.WriteLine("</table>");
                    sWrite.WriteLine("</div>");
                    sWrite.WriteLine("<script>window.print();</script>");
                    sWrite.WriteLine("</body>");
                    sWrite.WriteLine("</html>");
                    sWrite.Close();
                    System.Diagnostics.Process.Start(Apppath + "\\AlreadyExpiryReport.html");
                }
                else
                {
                    MessageBox.Show("Record Not Found,please change the date and try again !..", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error!...", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void dptFrom_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnExport_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count != 0)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Excel Files (*.xls)|*.xls";
                    saveFileDialog1.FileName = "Already Expired Report(" + DateTime.Now.ToString("dd-MM-yy h.mm.ss tt") + ").xls";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        PathName = saveFileDialog1.FileName;
                        Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                        ExcelApp.Application.Workbooks.Add(Type.Missing);
                        ExcelApp.Columns.ColumnWidth = 20;
                        ExcelApp.Range[ExcelApp.Cells[1, 1], ExcelApp.Cells[1, 3]].Merge();
                        ExcelApp.Cells[1, 1] = "ALREADY EXPIRED REPORT";
                        ExcelApp.Cells[1, 1].HorizontalAlignment = HorizontalAlignment.Center;
                        ExcelApp.Cells[1, 1].Font.Size = 12;
                        ExcelApp.Cells[1, 1].Interior.Color = Color.FromArgb(153, 204, 255);
                        ExcelApp.Columns.ColumnWidth = 20;
                        ExcelApp.Cells[2, 1] = "From Date";
                        ExcelApp.Cells[2, 1].Font.Size = 10;
                        ExcelApp.Cells[2, 2] = dptFrom.Value;
                        ExcelApp.Cells[2, 2].Font.Size = 10;
                        //ExcelApp.Cells[3, 1] = "To Date";
                        //ExcelApp.Cells[3, 1].Font.Size = 10;
                        //ExcelApp.Cells[3, 2] = dptTo.Value;
                        //ExcelApp.Cells[3, 2].Font.Size = 10;
                        ExcelApp.Cells[3, 1] = "Running Date";
                        ExcelApp.Cells[3, 1].Font.Size = 10;
                        ExcelApp.Cells[3, 2] = DateTime.Now.ToString("dd-MM-yyyy");
                        ExcelApp.Cells[3, 2].Font.Size = 10;
                        ExcelApp.Cells[3, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        //ExcelApp.Cells[3, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)

                        {
                            ExcelApp.Cells[4, i] = dataGridView1.Columns[i - 1].HeaderText;
                            ExcelApp.Cells[4, i].ColumnWidth = 25;
                            ExcelApp.Cells[4, i].EntireRow.Font.Bold = true;
                            ExcelApp.Cells[4, i].Interior.Color = Color.FromArgb(0, 102, 204);
                            ExcelApp.Cells[4, i].Font.Size = 10;
                            ExcelApp.Cells[4, i].Font.Name = "Arial";
                            ExcelApp.Cells[4, i].Font.Color = Color.FromArgb(255, 255, 255);
                            ExcelApp.Cells[4, i].Interior.Color = Color.FromArgb(0, 102, 204);
                        }
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            try
                            {
                                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                                {
                                    ExcelApp.Cells[i + 5, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                                    ExcelApp.Cells[i + 5, j + 1].BorderAround(true);
                                    ExcelApp.Cells[i + 5, j + 1].Borders.Color = Color.FromArgb(0, 102, 204);
                                    ExcelApp.Cells[i + 5, j + 1].Font.Size = 8;
                                }
                            }
                            catch (Exception ex) { }
                        }
                        ExcelApp.ActiveWorkbook.SaveAs(PathName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                        ExcelApp.ActiveWorkbook.Saved = true;
                        ExcelApp.Quit();
                        checkStr = "1";
                        MessageBox.Show("Successfully Exported to Excel", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                    MessageBox.Show("No records found,please change the date and try again!..", "No Records Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        public void datewiseexpiry(string supplier)
        {
            dataGridView1.Enabled = true;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            int m = 0;
            try
            {
                DataTable dtf;
                int slno = 0;
                int j = 0;
                grosspurchaserate = 0;
                grossstockvalue = 0;
                DataTable dtb = ctrlr.getbatchforalreadyexpired(supplier, dptFrom.Value.ToString("yyyy/MM/dd"));//, dptTo.Value.ToString("yyyy/MM/dd")
                if (dtb.Rows.Count > 0)
                {
                    for (j = 0; j < dtb.Rows.Count; j++)
                    {
                        dataGridView1.Rows.Add();
                        // Lab_Msg.Visible = false;
                        slno = m + 1;
                        DataTable dti = ctrlr.getitems(dtb.Rows[j]["item_code"].ToString());
                        if (dti.Rows.Count > 0)
                        {
                            dataGridView1.Rows[m].Cells["col_sln"].Value = slno;
                            dataGridView1.Rows[m].Cells["ItemCod"].Value = dti.Rows[0]["item_code"].ToString();
                            dataGridView1.Rows[m].Cells["ItemNam"].Value = dti.Rows[0]["item_name"].ToString();
                            dataGridView1.Rows[m].Cells["PurchNumbe"].Value = dtb.Rows[j]["PurchNumber"].ToString();
                            dataGridView1.Rows[m].Cells["PurchaseDat"].Value = Convert.ToDateTime(dtb.Rows[j]["PurchDate"].ToString()).ToString("dd/MM/yyyy");
                            dataGridView1.Rows[m].Cells["BatchNumbe"].Value = dtb.Rows[j]["BatchNumber"].ToString();
                            dataGridView1.Rows[m].Cells["Quantit"].Value = dtb.Rows[j]["Qty"].ToString();
                            dataGridView1.Rows[m].Cells["ExpiryDat"].Value = Convert.ToDateTime(dtb.Rows[j]["ExpDate"].ToString()).ToString("dd/MM/yyyy");
                            dtf = ctrlr.findsupplier(Convert.ToInt32(dtb.Rows[j]["Sup_Code"].ToString()));
                            dataGridView1.Rows[m].Cells["ColSu"].Value = dtf.Rows[0]["Supplier_Name"].ToString();
                            dataGridView1.Rows[m].Cells["stock"].Value = Convert.ToInt32(dtb.Rows[j]["Qty"]) * Convert.ToInt32(dti.Rows[0]["Purch_Rate"]);//sanoop 
                            dataGridView1.Rows[m].Cells["purchaserate"].Value = dti.Rows[0]["Purch_Rate"].ToString();//sanoop
                            grossstockvalue = grossstockvalue + (Convert.ToInt32(dtb.Rows[j]["Qty"]) * Convert.ToInt32(dti.Rows[0]["Purch_Rate"]));
                            grosspurchaserate = grosspurchaserate + Convert.ToInt32(dti.Rows[0]["Purch_Rate"]);
                            m = m + 1;
                        }
                    }
                    ls_gst.Text = grossstockvalue.ToString();
                    ls_amount.Text = grosspurchaserate.ToString();
                }
                else
                {
                    ls_amount.Text = "0.00";
                    ls_gst.Text = "0.00";
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AlreadyExpired_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Enabled = true;
                //dptTo.Format = DateTimePickerFormat.Short;
                //dptTo.MaxDate = DateTime.Today.Date;
                //dptTo.Visible = true;
                DataTable dts = ct.LoadSupplier();
                cmbSupplier.DisplayMember = "Supplier_Name";
                cmbSupplier.ValueMember = "Supplier_Code";
                DataRow row = dts.NewRow();
                row["Supplier_Name"] = "All Supplier";
                dts.Rows.InsertAt(row, 0);
                cmbSupplier.DataSource = dts;
                dataGridView1.Enabled = true;
                datewiseexpiry(cmbSupplier.Text);
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                //dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 9, FontStyle.Bold);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error!...", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSupplier.SelectedIndex==-1)
            {

            }
           else if(cmbSupplier.SelectedIndex>=0)
            {
                if(cmbSupplier.Text== "All Supplier")
                {
                    datewiseexpiry("All Supplier");
                }
                else
                {
                    datewiseexpiry(cmbSupplier.Text);
                }
            }
        }

        private void btnselect_Click(object sender, EventArgs e)
        {
            
        }

        private void btnselect_Click_1(object sender, EventArgs e)
        {
            if(cmbSupplier.SelectedIndex>=0)
            {
                datewiseexpiry(cmbSupplier.Text);
            }
            
        }

        private void cmbSupplier_Click(object sender, EventArgs e)
        {
            if(cmbSupplier.Items.Count==0)
            {
                DataTable dts = ct.LoadSupplier();
                cmbSupplier.DisplayMember = "Supplier_Name";
                cmbSupplier.ValueMember = "Supplier_Code";
                DataRow row = dts.NewRow();
                row["Supplier_Name"] = "All Supplier";
                dts.Rows.InsertAt(row, 0);
                cmbSupplier.DataSource = dts;
            }
            else
            {

            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        
    }

}

        
    
