using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PappyjoeMVC.View
{
    public partial class Stock_Out_Report : Form
    {
        public Stock_Out_Report()
        {
            InitializeComponent();
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                //if (DGV_SALES.Rows.Count > 0)
                //{
                //    string frdate = dptMonthly_From.Value.Day.ToString();
                //    string frmonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dptMonthly_From.Value.Month);
                //    string fryear = dptMonthly_From.Value.Year.ToString();
                //    string todate = dptMonthly_To.Value.Day.ToString();
                //    string tomonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dptMonthly_To.Value.Month);
                //    string toyear = dptMonthly_To.Value.Year.ToString();
                //    string today = DateTime.Now.ToString("dd/MM/yyyy");
                //    string message = "Did you want Header on Print?";
                //    string caption = "Verification";
                //    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                //    DialogResult result;
                //    result = MessageBox.Show(message, caption, buttons);
                //    if (result == System.Windows.Forms.DialogResult.Yes)
                //    {
                //        DataTable dtp = this.ctrlr.practicedetails();
                //        if (dtp.Rows.Count > 0)
                //        {
                //            clinicn = dtp.Rows[0]["name"].ToString();
                //            strclinicname = clinicn.Replace("¤", "'");
                //            strphone = dtp.Rows[0]["contact_no"].ToString();
                //            strStreet = dtp.Rows[0]["street_address"].ToString();
                //            stremail = dtp.Rows[0]["email"].ToString();
                //            strwebsite = dtp.Rows[0]["website"].ToString();
                //            logo_name = dtp.Rows[0]["path"].ToString();
                //        }
                //    }
                //    string Apppath = System.IO.Directory.GetCurrentDirectory();
                //    System.IO.StreamWriter sWrite = new System.IO.StreamWriter(Apppath + "\\SalesReport.html");
                //    sWrite.WriteLine("<html>");
                //    sWrite.WriteLine("<head>");
                //    sWrite.WriteLine("<style>");
                //    sWrite.WriteLine("table { border-collapse: collapse;}");
                //    sWrite.WriteLine("p.big {line-height: 400%;}");
                //    sWrite.WriteLine("</style>");
                //    sWrite.WriteLine("</head>");
                //    sWrite.WriteLine("<body >");
                //    sWrite.WriteLine("<div>");
                //    sWrite.WriteLine("<table align=center width=900 >");
                //    sWrite.WriteLine("<col >");
                //    sWrite.WriteLine("<br>");
                //    string Appath = System.IO.Directory.GetCurrentDirectory();
                //    if (File.Exists(Appath + "\\" + logo_name))
                //    {
                //        sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                //        sWrite.WriteLine("<tr>");
                //        sWrite.WriteLine("<td width='30px' height='50px' align='left' rowspan='3'><img src='" + Appath + "\\" + logo_name + "'style='width:70px;height:70px;' ></td>  ");
                //        sWrite.WriteLine("<td width='870px' align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=4><b>&nbsp;" + strclinicname + "</font> <br><FONT  COLOR=black  face='Segoe UI' SIZE=2>&nbsp;" + strStreet + "<br>&nbsp;" + strphone + " </b></td></tr>");
                //        sWrite.WriteLine("</table>");
                //    }
                //    else
                //    {
                //        sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                //        sWrite.WriteLine("<tr>");
                //        sWrite.WriteLine("<td  align='left' height='20px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + strclinicname + "</font></td></tr>");
                //        sWrite.WriteLine("<tr><td  align='left' height='20px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;" + strStreet + "</font></td></tr>");
                //        sWrite.WriteLine("<tr><td align='left' height='20px' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + strphone + "</font></td></tr>");

                //        sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");

                //        sWrite.WriteLine("</table>");
                //    }
                //    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                //    sWrite.WriteLine("<tr><td align='left'  ><hr/></td></tr>");
                //    sWrite.WriteLine("</table>");
                //    sWrite.WriteLine("<tr>");
                //    sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3> SALES REPORT </font></center></b></td>");
                //    sWrite.WriteLine("</tr>");
                //    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                //    sWrite.WriteLine("<tr>");
                //    sWrite.WriteLine("<td colspan=7 align=left><FONT COLOR=black FACE='Segoe UI' SIZE=2>  " + "<b>From :</b>" + " " + dptMonthly_From.Value.ToString("dd/MM/yyyy") + " </font></left></td>");
                //    sWrite.WriteLine("</tr>");
                //    sWrite.WriteLine("<tr>");
                //    sWrite.WriteLine("<td colspan=7 align=left><FONT COLOR=black FACE='Segoe UI' SIZE=2>  " + "<b>To :</b>" + "   " + dptMonthly_To.Value.ToString("dd/MM/yyyy") + "</font></left></td>");
                //    sWrite.WriteLine("</tr>");
                //    sWrite.WriteLine("<tr>");
                //    sWrite.WriteLine("<td align='left' colspan=7><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>Printed Date:</b>" + " " + today + "" + "</font><left></td>");
                //    sWrite.WriteLine("</tr>");
                //    if (DGV_SALES.Rows.Count > 0)
                //    {
                //        sWrite.WriteLine("<tr>");
                //        sWrite.WriteLine("    <td align='left' width='6%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3 ><b>&nbsp;Slno.</b></font></th>");
                //        sWrite.WriteLine("    <td align='left' width='17%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> <b>Invoice No</b></font></th>");
                //        sWrite.WriteLine("    <td align='left' width='16%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Invoice Date</b></font></th>");
                //        sWrite.WriteLine("    <td align='left' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Customer Id</b></font></th>");
                //        sWrite.WriteLine("    <td align='left' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Customer Name</b></font></th>");
                //        sWrite.WriteLine("    <td align='left' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Mode of Payment</b></font></th>");
                //        sWrite.WriteLine("    <td align='right' width='15%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b> Amount</b></font></th>");
                //        sWrite.WriteLine("    <td align='right' width='15%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b> Discount (%)</b></font></th>");
                //        sWrite.WriteLine("    <td align='right' width='25%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> <b>Total Amount</b></font></th>");
                //        sWrite.WriteLine("</tr>");
                //        for (int c = 0; c < DGV_SALES.Rows.Count; c++)
                //        {
                //            sWrite.WriteLine("<tr>");
                //            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + DGV_SALES.Rows[c].Cells["SLNO"].Value.ToString() + "</font></th>");
                //            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + DGV_SALES.Rows[c].Cells["InvNumber"].Value.ToString() + "</font></th>");
                //            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + DGV_SALES.Rows[c].Cells["InvDate"].Value.ToString() + "</font></th>");
                //            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + DGV_SALES.Rows[c].Cells["cust_number"].Value.ToString() + "</font></th>");
                //            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + DGV_SALES.Rows[c].Cells["cust_name"].Value.ToString() + "</font></th>");
                //            {
                //                sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + DGV_SALES.Rows[c].Cells["modeofpayment"].Value.ToString() + "</font></th>");
                //                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + DGV_SALES.Rows[c].Cells["amount"].Value.ToString() + "&nbsp</font></th>");
                //                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + DGV_SALES.Rows[c].Cells["clDiscount"].Value.ToString() + "&nbsp</font></th>");
                //            }
                //            sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + DGV_SALES.Rows[c].Cells["TotalAmount"].Value.ToString() + "&nbsp;</font></th>");
                //        }
                //        sWrite.WriteLine("</tr >");
                //        sWrite.WriteLine("<tr>");
                //        sWrite.WriteLine("<td align='right'  colspan=6 ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b> Total Items :</b></font><right'></td>");
                //        sWrite.WriteLine("<td align='right'  colspan=7 ><FONT COLOR=black FACE='Segoe UI' SIZE=3> " + Txt_totalInvoice.Text + " </font><right'></td>");
                //        sWrite.WriteLine("</tr>");
                //        sWrite.WriteLine("<tr>");
                //        sWrite.WriteLine("<td align='right'  colspan=6 ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b> Grand Total :</b></font><right'></td>");
                //        sWrite.WriteLine("<td align='right'  colspan=7 ><FONT COLOR=black FACE='Segoe UI' SIZE=3>  " + Txtgrandtotal.Text + " </font><right'></td>");
                //        sWrite.WriteLine("</tr>");
                //        sWrite.WriteLine("</table>");
                //        sWrite.WriteLine("</div>");
                //        sWrite.WriteLine("<script>window.print();</script>");
                //        sWrite.WriteLine("</body>");
                //        sWrite.WriteLine("</html>");
                //        sWrite.Close();
                //        System.Diagnostics.Process.Start(Apppath + "\\SalesReport.html");
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("No records found, Please change the date and try again!..", "Failed ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btn_show_Click(object sender, EventArgs e)
        {

        }
    }
}
