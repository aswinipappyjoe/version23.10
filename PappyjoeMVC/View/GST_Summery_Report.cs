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

namespace PappyjoeMVC.View
{
    public partial class GST_Summery_Report : Form
    {
        gst_controller cntrl = new gst_controller();
      public  string str_month = "1";
        public GST_Summery_Report()
        {
            InitializeComponent();
        }
        bool log_flag = false;
        private void GST_Summery_Report_Load(object sender, EventArgs e)
        {
            log_flag = true;
            //    cmb_year.Items.Add("All");
            //    cmb_year.ValueMember = "0";
            //    cmb_year.DisplayMember = "All";
            //    //DataTable doctor_rs = this.cntrl.doctor_rs();
            //    //if (doctor_rs.Rows.Count > 0)
            //    //{
            //        for (int i = 1950; i <= 2050; i++)
            //    {
            //        cmb_year.Items.Add(i);
            //        //cmb_year.Items.Add(doctor_rs.Rows[i]["doctor_name"].ToString());
            //        //    cmb_year.ValueMember = doctor_rs.Rows[i]["id"].ToString();
            //        //    cmb_year.DisplayMember = doctor_rs.Rows[i]["doctor_name"].ToString();
            //   }
            //    //}
            //    cmb_year.SelectedIndex = 0;
            //comb_month.Items.Add("All Month");
            //comb_month.ValueMember = "0";
            //comb_month.DisplayMember = "All Month";
            //int mont = DateTime.Now.Month;
            comb_month.SelectedIndex = 0;
            //string mon = comb_month.Text;
            //get_month(mon);
            DataTable dt4 = this.cntrl.select_gst();
            if (dt4.Rows.Count > 0)
            {
                drugnametextbox.Text = dt4.Rows[0]["Dl_Number2"].ToString();
            }
            BindDropdown();
            //public DataTable get_year_wise_items(string month, string _year)
            DataTable dtb= this.cntrl.get_year_wise_items( cmb_year.Text);
            //DataTable dtb = this.cntrl.get_month_wise_items(str_month,cmb_year.Text);
            load_data(dtb,comb_month.Text);
            dgv_gst.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            dgv_gst.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv_gst.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 9, FontStyle.Regular);
            dgv_gst.EnableHeadersVisualStyles = false;
            foreach (DataGridViewColumn cl in dgv_gst.Columns)
            {
                cl.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            log_flag = false;
        }
        public void load_data(DataTable dtb_items,string mon)
        {
            dgv_gst.Rows.Clear();
            if(dtb_items.Rows.Count>0)
            {
                decimal p_amount = 0, s_amount = 0, pgst = 0, sgst = 0, p_total = 0, s_total = 0, s_p = 0,p_gst_am=0;
               int k = 1;
                foreach(DataRow dr in dtb_items.Rows)
                {
                    decimal gst_Amount = 0, Amount = 0, total = 0, sa_gstamount = 0, sa_amount = 0, sa_total = 0, diff = 0, p_gst = 0, s_gst = 0;
                    string name = "";
                    DataTable dt_purchase = new DataTable();
                    if (mon=="All")
                    {
                         dt_purchase = this.cntrl.get_all_data_purchase_year(dr["Item_Code"].ToString(), cmb_year.Text);

                    }
                    else
                    {
                         dt_purchase = this.cntrl.get_all_data_purchase(dr["Item_Code"].ToString(), str_month, cmb_year.Text);

                    }
                    if (dt_purchase.Rows.Count>0)
                    {
                        foreach (DataRow drr in dt_purchase.Rows)//
                        {
                            name = drr["Desccription"].ToString();
                               p_gst = Convert.ToDecimal(drr["GST"].ToString());
                            Amount +=Convert.ToDecimal( drr["Qty"].ToString()) *Convert.ToDecimal( drr["Rate"].ToString());
                            gst_Amount += (Amount * Convert.ToDecimal(drr["GST"].ToString())) / 100;
                            total += Convert.ToDecimal(drr["GrandTotal"].ToString());
                        }
                    }
                    DataTable dt_sales = new DataTable();
                    if (mon == "All")
                    {
                        //public DataTable get_all_data_sales_year(string item, string _year)
                        dt_sales = this.cntrl.get_all_data_sales_year(dr["Item_Code"].ToString(),  cmb_year.Text);

                    }
                    else
                    {
                        dt_sales = this.cntrl.get_all_data_sales(dr["Item_Code"].ToString(), str_month, cmb_year.Text);

                    }
                    if (dt_sales.Rows.Count>0)
                    {
                        foreach (DataRow drs in dt_sales.Rows)
                        {
                            s_gst = Convert.ToDecimal(drs["GST"].ToString());
                            sa_amount += Convert.ToDecimal(drs["Qty"].ToString()) * Convert.ToDecimal(drs["Rate"].ToString());
                            sa_gstamount += (sa_amount * Convert.ToDecimal(drs["GST"].ToString())) / 100;
                            sa_total += Convert.ToDecimal(drs["TotalAmount"].ToString());
                        }
                    }
                    diff = sa_gstamount - gst_Amount;
                    if(dt_purchase.Rows.Count > 0 || dt_sales.Rows.Count > 0)
                    {
                        dgv_gst.Rows.Add(k, name, Amount.ToString("0.00"), gst_Amount.ToString("0.00"), total.ToString("0.00"), name, sa_amount.ToString("0.00"), sa_gstamount.ToString("0.00"), sa_total.ToString("0.00"), diff.ToString("0.00"));
                    }
                    p_amount = p_amount + Amount;//gst_Amount;
                    pgst = pgst + gst_Amount;// p_gst;
                    p_total = p_total + total;
                    s_amount = s_amount + sa_amount;// sa_gstamount;
                    sgst = sgst + sa_gstamount;// s_gst;
                    s_total = s_total + sa_total;
                    s_p = s_p + diff;

                    k++;
                }
                lp_amount.Text = p_amount.ToString("0.00");
                lp_gst.Text = pgst.ToString("0.00");
                lp_total.Text = p_total.ToString("0.00");
                ls_amount.Text = s_amount.ToString("0.00");
                ls_gst.Text = sgst.ToString("0.00");
                ls_total.Text = s_total.ToString("0.00");
                ls_p.Text = s_p.ToString("0.00");


            }
            else
            {
                MessageBox.Show("There is no data...","Data Not Found",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
        public void get_month(string mon)
        {
            switch (mon)
            {
                case "January":
                    str_month="1";
                    break;
                case "February":
                    str_month = "2";
                    break;
                case "March":
                    str_month = "3";
                    break;
                case "April":
                    str_month = "4";
                    break;
                case "May":
                    str_month = "5";
                    break;
                case "June":
                    str_month = "6";
                    break;
                case "July":
                    str_month = "7";
                    break;
                case "August":
                    str_month = "8";
                    break;
                case "September":
                    str_month = "9";
                    break;
                case "October":
                    str_month = "10";
                    break;
                case "November":
                    str_month = "11";
                    break;
                case "December":
                    str_month = "12";
                    break;
                default:
                    str_month = "0";
                    break;

            }
        }
        void BindDropdown()
        {
            for (int Year = 1950; Year <= DateTime.Now.Year; Year++)
            {
                cmb_year.Items.Add(Year.ToString());
            }
            var year = DateTime.Now.Year;
            cmb_year.Text=year.ToString();
          
        }
        private void comb_month_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(log_flag==false)
            {
                DataTable dtb = new DataTable();
                string mon = comb_month.Text;
                get_month(mon);
                if(mon=="All")
                {
                     dtb = this.cntrl.get_year_wise_items( cmb_year.Text);
                }
                else
                {
                     dtb = this.cntrl.get_month_wise_items(str_month, cmb_year.Text);
                }
                load_data(dtb, mon);
            }
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            string checkStr = "0"; string PathName = "";
            try
            {
                if (dgv_gst.Rows.Count != 0)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Excel Files (*.xls)|*.xls";
                    saveFileDialog1.FileName = "GSTSummeryReport(" + DateTime.Now.ToString("MM-dd-yy h.mm.ss tt") + ").xls";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        PathName = saveFileDialog1.FileName;
                        Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                        ExcelApp.Application.Workbooks.Add(Type.Missing);
                        ExcelApp.Columns.ColumnWidth = 20;
                        int count = dgv_gst.Columns.Count;
                        ExcelApp.Range[ExcelApp.Cells[1, 1], ExcelApp.Cells[1, count]].Merge();
                        ExcelApp.Cells[1, 1] = "GST SUMMARY REPORT";
                        ExcelApp.Cells[1, 1].HorizontalAlignment = HorizontalAlignment.Center;
                        ExcelApp.Cells[1, 1].Font.Size = 12;
                        ExcelApp.Cells[1, 1].Interior.Color = Color.FromArgb(153, 204, 255);
                        ExcelApp.Columns.ColumnWidth = 20;
                        ExcelApp.Cells[2, 1] = "Year";
                        ExcelApp.Cells[2, 1].Font.Size = 10;
                        ExcelApp.Cells[3, 1] = "Month";
                        ExcelApp.Cells[3, 1].Font.Size = 10;
                        ExcelApp.Cells[2, 2] = cmb_year.Text;
                        ExcelApp.Cells[2, 2].Font.Size = 10;
                        ExcelApp.Cells[3, 2] = comb_month.Text;
                        ExcelApp.Cells[3, 2].Font.Size = 10;
                        ExcelApp.Cells[5, 1] = "GST";
                        ExcelApp.Cells[5, 1].Font.Size = 10;
                        ExcelApp.Cells[5, 2] = drugnametextbox.Text;
                        ExcelApp.Cells[5, 2].Font.Size = 10;
                        ExcelApp.Cells[4, 1] = "Running Date";
                        ExcelApp.Cells[4, 1].Font.Size = 10;
                        ExcelApp.Cells[4, 2] = DateTime.Now.ToString("MM-dd-yyyy");
                        ExcelApp.Cells[4, 2].Font.Size = 10;
                        ExcelApp.Cells[4, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        ExcelApp.Cells[3, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        ExcelApp.Cells[2, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        for (int i = 1; i < dgv_gst.Columns.Count + 1; i++)
                        {
                            ExcelApp.Cells[5, i] = dgv_gst.Columns[i - 1].HeaderText;
                            ExcelApp.Cells[5, i].ColumnWidth = 25;
                            ExcelApp.Cells[5, i].EntireRow.Font.Bold = true;
                            ExcelApp.Cells[5, i].Interior.Color = Color.FromArgb(0, 102, 204);
                            ExcelApp.Cells[5, i].Font.Size = 10;
                            ExcelApp.Cells[5, i].Font.Name = "Arial";
                            ExcelApp.Cells[5, i].Font.Color = Color.FromArgb(255, 255, 255);
                            ExcelApp.Cells[5, i].Interior.Color = Color.FromArgb(0, 102, 204);
                        }
                        for (int i = 0; i <= dgv_gst.Rows.Count - 1; i++)
                        {
                            try
                            {
                                for (int j = 0; j < dgv_gst.Columns.Count; j++)
                                {
                                    if (dgv_gst.Rows[i].Cells[j].Value != null && dgv_gst.Rows[i].Cells[j].Value.ToString() != "")
                                        ExcelApp.Cells[i + 7, j + 1] = dgv_gst.Rows[i].Cells[j].Value.ToString();
                                    ExcelApp.Cells[i + 7, j + 1].BorderAround(true);
                                    ExcelApp.Cells[i + 7, j + 1].Borders.Color = Color.FromArgb(0, 102, 204);
                                    ExcelApp.Cells[i + 7, j + 1].Font.Size = 8;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
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
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_gst.Rows.Count > 0)
                {
                    //string frdate = dateTimePickerfirstappoint1.Value.Day.ToString();
                    //string frmonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTimePickerfirstappoint1.Value.Month);
                    //string fryear = dateTimePickerfirstappoint1.Value.Year.ToString();
                    //string todate = dateTimePickerfirstappoint2.Value.Day.ToString();
                    //string tomonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTimePickerfirstappoint2.Value.Month);
                    //string toyear = dateTimePickerfirstappoint2.Value.Year.ToString();
                    string today = DateTime.Now.ToString("dd-MM-yyyy");
                    string message = "Did you want Header on Print?";
                    string caption = "Verification";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons);
                    int c = 0;
                    string strclinicname = ""; string clinicn = "";
                    string strStreet = "";
                    string stremail = "";
                    string strwebsite = "";
                    string strphone = "", logo_name = "",gst="";
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Data.DataTable dtp = this.cntrl.Get_practiceDlNumber();
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
                    StreamWriter sWrite = new StreamWriter(Apppath + "\\GSTSummeryreport.html");
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
                    sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3>GST SUMMARY REPORT </font></center></b></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=3><FONT COLOR=black FACE='Segoe UI' SIZE=2><b> Year :</b> " + cmb_year.Text + " </font></td> ");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=3><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>Month : </b>" + comb_month.Text + " </font></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=3><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>GST  : </b>" + gst + " </font></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=3><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>Printed Date:</b>" + " " + DateTime.Now.ToString("dd-MM-yyyy") + "" + "</font></center></td>");
                    sWrite.WriteLine("</tr>");
                    if (dgv_gst.Rows.Count > 0)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("    <td colspan=5 align='center' width='10%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>Purchase</b></font></th>");
                        //sWrite.WriteLine("<td colspan=6><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>Purchase</b></font></center></td>");
                        sWrite.WriteLine("    <td colspan=5 align='center' width='10%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>Sales</b></font></th>");
                        //sWrite.WriteLine("<td colspan=5  align='right'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>Sales</b></font></center></td>");

                        sWrite.WriteLine("</tr>");

                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>Slno.</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>Particular.</b></font></th>");

                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>Amount.</b></font></th>");

                        sWrite.WriteLine("    <td align='left' width='60%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>GST</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>Total Amount(Amount+GST).</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>Particular.</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>Amount.</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>GST.</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>Total Amount(Amount+GST).</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>Sales GST-Purchase GST.</b></font></th>");
                        //sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>Date of Admission.</b></font></th>");

                        //sWrite.WriteLine("    <td align='left' width='30%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2> &nbsp;<b>No of Days on Bed</b></font></th>");
                        sWrite.WriteLine("</tr>");
                        for (int j = 0; j < dgv_gst.Rows.Count; j++)
                        {
                            sWrite.WriteLine("<tr>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgv_gst.Rows[j].Cells["slno"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgv_gst.Rows[j].Cells["particular_pur"].Value.ToString() + " </font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgv_gst.Rows[j].Cells["amount_pur"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgv_gst.Rows[j].Cells["gst_pur"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgv_gst.Rows[j].Cells["pur_total"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgv_gst.Rows[j].Cells["particular_sales"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgv_gst.Rows[j].Cells["amount_sales"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgv_gst.Rows[j].Cells["gst_sales"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgv_gst.Rows[j].Cells["sales_total"].Value.ToString() + "</font></th>");
                            //sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgv_gst.Rows[j].Cells["roomno"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgv_gst.Rows[j].Cells["summery"].Value.ToString() + "</font></th>");
                            //sWrite.WriteLine("    <td align='left' style='border:1px so/*lid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgv_ip.Rows[j].Cells["noofdays"].Value.ToString() + "</font></th>");*/
                            sWrite.WriteLine("</tr>");
                        }
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>Total(INR)</font></td>");
                        sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>" + lp_amount.Text + "</b>&nbsp;</font></td>");
                        sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>" + lp_gst.Text + "</b>&nbsp;</font></td>");
                        sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>" + lp_total.Text + "</b>&nbsp;</font></td>");

                        sWrite.WriteLine("    <td align='center' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>" + ls_amount.Text + "</b>&nbsp;</font></td>");
                        sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>" + ls_gst.Text + "</b>&nbsp;</font></td>");
                        sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>" + ls_total.Text + "</b>&nbsp;</font></td>");
                        sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>" + ls_p.Text + "</b>&nbsp;</font></td>");
                        //sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>" + lp_amount.Text + "</b>&nbsp;</font></td>");

                        ////sWrite.WriteLine("    <td align='center' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        //sWrite.WriteLine("    <td align='center' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        //sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        //sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        //sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        //sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        //sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        //sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>Total</b></font></td>");
                        //sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>" + lp_amount.Text + "</b>&nbsp;</font></td>");
                        //sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>" + payment + "</b>&nbsp;</font></td>");
                        //sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>" + due + "</b>&nbsp;</font></td>");
                        sWrite.WriteLine("</tr>");
                        sWrite.WriteLine("</table>");
                        sWrite.WriteLine("</div>");
                        sWrite.WriteLine("<script>window.print();</script>");
                        sWrite.WriteLine("</body>");
                        sWrite.WriteLine("</html>");
                        sWrite.Close();
                        System.Diagnostics.Process.Start(Apppath + "\\GSTSummeryreport.html");
                    }
                }
                else
                {
                    MessageBox.Show("No Record found, Please change the date and try again!..", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmb_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (log_flag == false)
            {
                DataTable dtb = new DataTable();
                string mon = comb_month.Text;
                get_month(mon);
                if (mon == "All")
                {
                    dtb = this.cntrl.get_year_wise_items(cmb_year.Text);
                }
                else
                {
                    dtb = this.cntrl.get_month_wise_items(str_month, cmb_year.Text);
                }
                load_data(dtb, mon);
                //string mon = comb_month.Text;
                //get_month(mon);
                //DataTable dtb = this.cntrl.get_month_wise_items(str_month, cmb_year.Text);
                //load_data(dtb, mon);
            }
        }

        private void ls_amount_Click(object sender, EventArgs e)
        {

        }
    }
}
