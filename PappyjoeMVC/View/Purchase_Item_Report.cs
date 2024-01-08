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
    public partial class Purchase_Item_Report : Form
    {
        int pur_id1 = 0;
        DateTime from1, to1;
        public string checkStr = "0",PathName = "";
        Purchase_Item_Report_controller ctrlr = new Purchase_Item_Report_controller();
        public decimal dis=0,grand;
        //private string v2;

        public Purchase_Item_Report(int pur_id, DateTime from, DateTime to)
        {
            InitializeComponent();
            pur_id1 = pur_id;
            from1 = from;
            to1 = to;
        }

        //public Purchase_Item_Report(int pur_id, DateTime from, DateTime to, string v1, string v2) : this(pur_id, from, to)
        //{
        //   dis = v1;
        //    grand = v2;
        //}

        //private void print()
        //{
        //    string message = "Do you want Header on Print?";
        //    string caption = "Verification";
        //    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
        //    DialogResult result;
        //    result = MessageBox.Show(message, caption, buttons);
        //    int c = 0;
        //    string strclinicname = "";
        //    string strStreet = "";
        //    string stremail = "";
        //    string strwebsite = "";
        //    string strphone = "";
        //    string path = "";
        //    string logo_name = "";
        //    try
        //    {
        //        if (result == System.Windows.Forms.DialogResult.Yes)
        //        {
        //            DataTable print_settng = this.prnt_ctrl.Get_inventory_id();
        //            System.Data.DataTable dtp = this.cntrl.get_companydetails();
        //            if (dtp.Rows.Count > 0)
        //            {

        //                if (dtp.Rows.Count > 0)
        //                {
        //                    string clinicn = "";
        //                    //clinicn = dtp.Rows[0]["name"].ToString();
        //                    if (print_settng.Rows.Count > 0)
        //                    {
        //                        clinicn = print_settng.Rows[0]["header"].ToString();
        //                        strclinicname = clinicn.Replace("¤", "'");
        //                        strStreet = print_settng.Rows[0]["left_text"].ToString();
        //                        strphone = print_settng.Rows[0]["right_text"].ToString();
        //                    }
        //                    stremail = dtp.Rows[0]["email"].ToString();
        //                    strwebsite = dtp.Rows[0]["website"].ToString();
        //                    path = dtp.Rows[0]["path"].ToString();
        //                    logo_name = path;
        //                }
        //            }
        //        }
        //        string Apppath = System.IO.Directory.GetCurrentDirectory();
        //        StreamWriter sWrite = new StreamWriter(Apppath + "\\Purchase.html");
        //        sWrite.WriteLine("<html>");
        //        sWrite.WriteLine("<head>");
        //        sWrite.WriteLine("<style>");
        //        sWrite.WriteLine("table { border-collapse: collapse;}");
        //        sWrite.WriteLine("p.big {line-height: 400%;}");
        //        sWrite.WriteLine("</style>");
        //        sWrite.WriteLine("</head>");
        //        sWrite.WriteLine("<body >");
        //        sWrite.WriteLine("<div>");
        //        sWrite.WriteLine("<table align=center width=900>");
        //        sWrite.WriteLine("<col >");
        //        sWrite.WriteLine("<br>");
        //        string Appath = System.IO.Directory.GetCurrentDirectory();
        //        if (File.Exists(Appath + "\\" + logo_name))
        //        {
        //            sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
        //            sWrite.WriteLine("<tr>");
        //            sWrite.WriteLine("<td width='30px' height='50px' align='left' rowspan='3'><img src='" + Appath + "\\" + logo_name + "'style='width:70px;height:70px;' ></td>  ");
        //            sWrite.WriteLine("<td width='870px' align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=4><b>&nbsp;" + strclinicname + "</font> <br><FONT  COLOR=black  face='Segoe UI' SIZE=2>&nbsp;" + strStreet + "<br>&nbsp;" + strphone + " </b></td></tr>");
        //            sWrite.WriteLine("</table>");
        //        }
        //        else
        //        {
        //            sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
        //            sWrite.WriteLine("<tr>");
        //            sWrite.WriteLine("<td  align='left' height='20px'><FONT  COLOR=black  face='Segoe UI' SIZE=5>&nbsp;" + strclinicname + "</font></td></tr>");
        //            sWrite.WriteLine("<tr><td  align='left' height='20px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;" + strStreet + "</font></td></tr>");
        //            sWrite.WriteLine("<tr><td align='left' height='20px' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + strphone + "</font></td></tr>");
        //            sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
        //            sWrite.WriteLine("</table>");
        //        }
        //        sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");

        //        sWrite.WriteLine("<tr>");
        //        sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3> PURCHASE INVOICE </font></center></b></td>");
        //        sWrite.WriteLine("</tr>");
        //        sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
        //        sWrite.WriteLine("<tr>");
        //        sWrite.WriteLine("<td  colspan=11  align='left'><left><FONT COLOR=black FACE='Segoe UI'  SIZE=2> Printed Date : " + dtpPurchDate.Value.ToString("d/MM/yyyy") + " </font></left></td>");
        //        sWrite.WriteLine("</tr>");
        //        sWrite.WriteLine("<tr>");
        //        sWrite.WriteLine("<td colspan=11  align='left'><left><FONT COLOR=black FACE='Segoe UI'  SIZE=2>   Purchase No :" + txtPurchInvNumber.Text + " </font></left></td>");
        //        sWrite.WriteLine("</tr>");
        //        sWrite.WriteLine("<tr>");
        //        sWrite.WriteLine("<td colspan=11  align='left'><left><FONT COLOR=black FACE='Segoe UI'  SIZE=2>   Invoice No :" + txtinvoiceno.Text + " </font></left></td>");
        //        sWrite.WriteLine("</tr>");
        //        sWrite.WriteLine("<tr>");
        //        sWrite.WriteLine("<td colspan=11 align='left'><left><FONT COLOR=black FACE='Segoe UI'  SIZE=2>   Supplier Name : " + txtSupplierName.Text + " </font></left></td>");
        //        sWrite.WriteLine("</tr>");
        //        if (dgvItemData.Rows.Count > 0)
        //        {
        //            sWrite.WriteLine("<tr>");
        //            sWrite.WriteLine("    <td align='center' width='230px' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Sl.No.</font></th>");
        //            sWrite.WriteLine("    <td align='left' width='230px' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3>Item Code</font></th>");
        //            sWrite.WriteLine("    <td align='left' width='230px' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Item Name</font></th>");
        //            sWrite.WriteLine("    <td align='left' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Barcode</font></th>");
        //            sWrite.WriteLine("    <td align='left' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Packing</font></th>");
        //            sWrite.WriteLine("    <td align='left' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Unit</font></th>");
        //            //sWrite.WriteLine("    <td align='left' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Batch</font></th>");
        //            sWrite.WriteLine("    <td align='Right' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Quantity</font></th>");
        //            sWrite.WriteLine("    <td align='Right' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Free</font></th>");
        //            sWrite.WriteLine("    <td align='Right' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3>Unit Cost</font></th>");
        //            sWrite.WriteLine("    <td align='Right' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>GST(%)</font></th>");
        //            sWrite.WriteLine("    <td align='Right' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>IGST(%)</font></th>");
        //            //sWrite.WriteLine("    <td align='Right' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>SGST(%)</font></th>");
        //            //sWrite.WriteLine("    <td align='Right' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>CGST(%)</font></th>");
        //            sWrite.WriteLine("    <td align='Right' width='230px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI'  SIZE=3>Amount</font></th>");
        //            sWrite.WriteLine("</tr>");
        //            int k = 1; int m = 0;

        //            while (c < dgvItemData.Rows.Count)
        //            {
        //                decimal s = Convert.ToDecimal(dgvItemData.Rows[c].Cells["gst"].Value.ToString());
        //                decimal sg = s / 2;
        //                sWrite.WriteLine("<tr>");
        //                sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + k + "</font></th>");
        //                sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["itemid"].Value.ToString() + "</font></th>");
        //                sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["description"].Value.ToString() + "</font></th>");
        //                sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["barcode"].Value.ToString() + "</font></th>");
        //                sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["Packing"].Value.ToString() + "</font></th>");
        //                sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["note"].Value.ToString() + "</font></th>");//
        //                DataTable dtb_batch = this.cntrl.purchase_batch_data(dgvItemData.Rows[c].Cells["id"].Value.ToString(), txtPurchInvNumber.Text);

        //                if (dtb_batch.Rows.Count > 0)
        //                {
        //                    if (m == 2)
        //                    {
        //                        m = 0;
        //                    }
        //                    //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000'  ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dtb_batch.Rows[m]["BatchNumber"].ToString() + "</font></th>");
        //                    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000'  ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["col_qty"].Value.ToString() + "</font></th>");

        //                    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["free"].Value.ToString() + "</font></th>");
        //                    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["Unit_Cost"].Value.ToString() + "</font></th>");
        //                    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["gst"].Value.ToString() + "</font></th>");
        //                    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["igst"].Value.ToString() + "</font></th>");
        //                    //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + sg + "</font></th>");
        //                    //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + sg + "</font></th>");
        //                    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["Amount"].Value.ToString() + "</font></th>");
        //                    //for (int l = 1; l < dtb_batch.Rows.Count; l++)
        //                    //{
        //                    //    sWrite.WriteLine("<tr>");
        //                    //    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
        //                    //    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
        //                    //    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
        //                    //    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
        //                    //    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
        //                    //    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");

        //                    //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000'  ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dtb_batch.Rows[l]["BatchNumber"].ToString() + "</font></th>");
        //                    //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000'  ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dtb_batch.Rows[0]["Qty"].ToString() + "</font></th>");

        //                    //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
        //                    //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
        //                    //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
        //                    //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
        //                    //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
        //                    //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");
        //                    //    sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2></font></th>");


        //                    //    sWrite.WriteLine("</tr>");
        //                    //}


        //                }
        //                //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000'  ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["col_qty"].Value.ToString() + "</font></th>");
        //                //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["free"].Value.ToString() + "</font></th>");
        //                //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["Unit_Cost"].Value.ToString() + "</font></th>");
        //                //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["igst"].Value.ToString() + "</font></th>");
        //                //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + sg + "</font></th>");
        //                //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + sg + "</font></th>");
        //                //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["gst"].Value.ToString() + "</font></th>");
        //                //sWrite.WriteLine("    <td align='Right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI'  SIZE=2>" + dgvItemData.Rows[c].Cells["Amount"].Value.ToString() + "</font></th>");
        //                sWrite.WriteLine("</tr>");
        //                k = k + 1;
        //                if (dtb_batch.Rows.Count > 1)
        //                {
        //                    m = m + 1;
        //                }
        //                int row = dtb_batch.Rows.Count;
        //                c++;
        //            }
        //            sWrite.WriteLine("<tr >");
        //            sWrite.WriteLine("<td colspan=11 align='right'><right><FONT COLOR=black FACE='Segoe UI' SIZE=2>   &nbsp;Total Amount  &nbsp;:</td><td align='right'><b> " + String.Format("{0:C}", Convert.ToDecimal(txt_TotalAmount.Text)) + "</b></td>");
        //            sWrite.WriteLine("</tr >");
        //            if (txtDic.Text != "0.00")
        //            {
        //                sWrite.WriteLine("<tr >");
        //                sWrite.WriteLine("<td colspan=11 align='right'><right><FONT COLOR=black FACE='Segoe UI' SIZE=2>   &nbsp;Total Discount Percent &nbsp;:</td><td align='right'><b>" + txtDic.Text + "</b></td>");
        //                sWrite.WriteLine("</tr >");
        //            }
        //            if (txt_Discount.Text != "0.00")
        //            {
        //                sWrite.WriteLine("<tr >");
        //                sWrite.WriteLine("<td colspan=11 align='right'><right><FONT COLOR=black FACE='Segoe UI' SIZE=2>   &nbsp;Discount Amount  &nbsp;:</td><td align='right'><b>" + String.Format("{0:C}", Convert.ToDecimal(txt_Discount.Text)) + "</b></td>");
        //                sWrite.WriteLine("</tr >");
        //            }
        //            sWrite.WriteLine("<tr >");
        //            sWrite.WriteLine("<td colspan=11 align='right'><right><FONT COLOR=black FACE='Segoe UI' SIZE=2>   &nbsp;Grand Total  &nbsp;:</td><td align='right'><b> " + String.Format("{0:C}", Convert.ToDecimal(txtGrandTotal.Text)) + "</b></td>");
        //            sWrite.WriteLine("</tr>");
        //        }
        //        sWrite.WriteLine("</table>");
        //        sWrite.WriteLine("</div>");
        //        sWrite.WriteLine("<script>window.print();</script>");
        //        sWrite.WriteLine("</body>");
        //        sWrite.WriteLine("</html>");
        //        sWrite.Close();
        //        System.Diagnostics.Process.Start(Apppath + "\\Purchase.html");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //}
        private void btnprint_Click(object sender, EventArgs e)
        {
            string message = "Did you want Header on Print?";
            string caption = "Verification";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons);
            int c = 0;
            string strclinicname = "";
            string strStreet = "";
            string stremail = "";
            string strwebsite = "";
            string strphone = "";
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                DataTable dtp = this.ctrlr.practicedetails();
                if (dtp.Rows.Count > 0)
                {
                    strclinicname = dtp.Rows[0]["name"].ToString();
                    strphone = dtp.Rows[0]["contact_no"].ToString();
                    strStreet = dtp.Rows[0]["street_address"].ToString();
                    stremail = dtp.Rows[0]["email"].ToString();
                    strwebsite = dtp.Rows[0]["website"].ToString();
                }
            }
            string Apppath = System.IO.Directory.GetCurrentDirectory();
            StreamWriter sWrite = new StreamWriter(Apppath + "\\PurchaseItemReport.html");
            sWrite.WriteLine("<html>");
            sWrite.WriteLine("<head>");
            sWrite.WriteLine("<style>");
            sWrite.WriteLine("table { border-collapse: collapse;}");
            sWrite.WriteLine("p.big {line-height: 400%;}");
            sWrite.WriteLine("</style>");
            sWrite.WriteLine("</head>");
            sWrite.WriteLine("<body >");
            sWrite.WriteLine("<br><br><br>");
            sWrite.WriteLine("<div>");
            sWrite.WriteLine("<table align=center width=900>");
            sWrite.WriteLine("<col width=500>");
            sWrite.WriteLine("<tr>");
            sWrite.WriteLine("<th colspan=7> <center><FONT COLOR=black FACE='Segoe UI' SIZE=3>  <b> PURCHASE ITEM REPORT </b> </font></center></th>");
            sWrite.WriteLine("</tr>");
            sWrite.WriteLine("<tr>");
            sWrite.WriteLine("<th colspan=7 align='left'><FONT COLOR=black FACE=' Segoe UI' SIZE=3>  <b> " + strclinicname + "</b> </font></th>");
            sWrite.WriteLine("</tr>");
            sWrite.WriteLine("<tr>");
            sWrite.WriteLine("<th colspan=7 align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=1>  <b> " + strphone + "</b> </font></th>");
            sWrite.WriteLine("</tr>");
            sWrite.WriteLine("<tr>");
            sWrite.WriteLine("<td colspan=7 align='left'><FONT COLOR=black FACE=' Segoe UI' SIZE=2> From :  " + dptMonthly_From.Value.ToString("dd/MM/yyyy") + " </font></td>");
            sWrite.WriteLine("</tr>");
            sWrite.WriteLine("<tr>");
            sWrite.WriteLine("<td colspan=7 align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2> To :  " + dptMonthly_To.Value.ToString("dd/MM/yyyy") + " </font></td>");
            sWrite.WriteLine("</tr>");
            sWrite.WriteLine("<tr>");
            sWrite.WriteLine("<td colspan=7 align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2> Printed Date :  " + DateTime.Now.ToString("dd/MM/yyyy") + " </font></td>");
            sWrite.WriteLine("</tr>");
            sWrite.WriteLine("</table>");
            if (dgvPurchase.Rows.Count > 0)
            {
                sWrite.WriteLine("<table align=center width=900>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("    <td align='left' width='15' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> Sl</font></th>");
                sWrite.WriteLine("    <td align='center' width='100' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> Purchase Date</font></th>");
                sWrite.WriteLine("    <td align='left' width='100' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> Item Code</font></th>");
                sWrite.WriteLine("    <td align='left' width='100' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE=' Segoe UI' SIZE=3> Description</font></th>");
                sWrite.WriteLine("    <td align='left' width='100' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> Packing</font></th>");
                sWrite.WriteLine("    <td align='left' width='100' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> Unit</font></th>");
                sWrite.WriteLine("    <td align='right' width='100' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> Quantity</font></th>");
                sWrite.WriteLine("    <td align='right' width='100' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> Free</font></th>");
                sWrite.WriteLine("    <td align='right' width='100' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> Unit Cost</font></th>");
                sWrite.WriteLine("    <td align='right' width='100' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> Discount</font></th>");
                sWrite.WriteLine("    <td align='right' width='100' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> GST</font></th>");
                sWrite.WriteLine("    <td align='right' width='100' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> IGST</font></th>");
                sWrite.WriteLine("    <td align='right' width='100' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> Amount</font></th>");
                sWrite.WriteLine("</tr>");
                while (c < dgvPurchase.Rows.Count)
                {
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + dgvPurchase.Rows[c].Cells["SL_NO"].Value.ToString() + "</font></th>");
                    sWrite.WriteLine("    <td align='center' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + dgvPurchase.Rows[c].Cells["PurchDate"].Value.ToString() + "</font></th>");
                    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + dgvPurchase.Rows[c].Cells["Item_id"].Value.ToString() + "</font></th>");
                    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + dgvPurchase.Rows[c].Cells["Desccription"].Value.ToString() + "</font></th>");
                    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + dgvPurchase.Rows[c].Cells["PACKING"].Value.ToString() + "</font></th>");
                    sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + dgvPurchase.Rows[c].Cells["UNIT"].Value.ToString() + "</font></th>");
                    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + dgvPurchase.Rows[c].Cells["QTY"].Value.ToString() + "&nbsp;</font></th>");
                    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + dgvPurchase.Rows[c].Cells["FREE"].Value.ToString() + "&nbsp;</font></th>");
                    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + dgvPurchase.Rows[c].Cells["UNIT_COST"].Value.ToString() + "&nbsp;</font></th>");
                    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + dgvPurchase.Rows[c].Cells["c_disc"].Value.ToString() + "</font></th>");
                    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + dgvPurchase.Rows[c].Cells["GST"].Value.ToString() + "&nbsp;</font></th>");
                    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + dgvPurchase.Rows[c].Cells["IGST"].Value.ToString() + "&nbsp;</font></th>");
                    sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>" + dgvPurchase.Rows[c].Cells["AMOUNT"].Value.ToString() + "&nbsp;</font></th>");
                    sWrite.WriteLine("</tr>");
                    c++;
                }
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<table align=center width=900>");
                sWrite.WriteLine("<tr >");
                sWrite.WriteLine("<td align='right'width=19000><FONT COLOR=black FACE='Segoe UI' SIZE=2>   TOTAL ITEM</font></right><td><td width=192>:&nbsp;&nbsp;</td><td align ='right'><b> " + txtTotalItem.Text + " </b></td>");
                sWrite.WriteLine("</tr >");
                sWrite.WriteLine("<tr >");
                sWrite.WriteLine("<td align='right'width=19000><FONT COLOR=black FACE='Segoe UI' SIZE=2> Discount</font></right><td><td width=192>:&nbsp;&nbsp;</td><td align ='right'><b> " + dis + " </b></td>");
                sWrite.WriteLine("</tr >");
                sWrite.WriteLine("<tr >");
                sWrite.WriteLine("<td align='right'width=19000><FONT COLOR=black FACE='Segoe UI' SIZE=2>  Grand Total</font></right><td><td width=192>:&nbsp;</td><td align ='right'><b> " + String.Format("{0:C}",grand)  + " </b></td>");
                sWrite.WriteLine("</tr >");
                sWrite.WriteLine("</table>");
            }
            sWrite.WriteLine("</div>");
            sWrite.WriteLine("<script>window.print();</script>");
            sWrite.WriteLine("</body>");
            sWrite.WriteLine("</html>");
            sWrite.Close();
            System.Diagnostics.Process.Start(Apppath + "\\PurchaseItemReport.html");
        }
        private void BTNClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btn_Export_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPurchase.Rows.Count != 0)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Excel Files (*.xls)|*.xls";
                    saveFileDialog1.FileName = "PurchaseItemReport(" + DateTime.Now.ToString("dd-MM-yy h.mm.ss tt") + ").xls";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        PathName = saveFileDialog1.FileName;
                        Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                        ExcelApp.Application.Workbooks.Add(Type.Missing);
                        ExcelApp.Columns.ColumnWidth = 20;
                        ExcelApp.Range[ExcelApp.Cells[1, 1], ExcelApp.Cells[1, 3]].Merge();
                        ExcelApp.Cells[1, 1] = "PURCHASE ITEMS REPORT";
                        ExcelApp.Cells[1, 1].HorizontalAlignment = HorizontalAlignment.Center;
                        ExcelApp.Cells[1, 1].Font.Size = 12;
                        ExcelApp.Cells[1, 1].Interior.Color = Color.FromArgb(153, 204, 255);
                        ExcelApp.Columns.ColumnWidth = 20;
                        ExcelApp.Cells[2, 1] = "From Date";
                        ExcelApp.Cells[2, 1].Font.Size = 10;
                        ExcelApp.Cells[2, 2] = dptMonthly_From.Value;
                        ExcelApp.Cells[2, 2].Font.Size = 10;
                        ExcelApp.Cells[3, 1] = "To Date";
                        ExcelApp.Cells[3, 1].Font.Size = 10;
                        ExcelApp.Cells[3, 2] = dptMonthly_To.Value;
                        ExcelApp.Cells[3, 2].Font.Size = 10; ExcelApp.Cells[4, 1] = "Running Date";
                        ExcelApp.Cells[4, 1].Font.Size = 10;
                        ExcelApp.Cells[4, 2] = DateTime.Now.ToString("dd-MM-yyyy");
                        ExcelApp.Cells[4, 2].Font.Size = 10;
                        ExcelApp.Cells[4, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        ExcelApp.Cells[3, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        ExcelApp.Cells[2, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        for (int i = 1; i < dgvPurchase.Columns.Count + 1; i++)
                        {
                            ExcelApp.Cells[5, i] = dgvPurchase.Columns[i - 1].HeaderText;
                            ExcelApp.Cells[5, i].ColumnWidth = 25;
                            ExcelApp.Cells[5, i].EntireRow.Font.Bold = true;
                            ExcelApp.Cells[5, i].Interior.Color = Color.FromArgb(0, 102, 204);
                            ExcelApp.Cells[5, i].Font.Size = 10;
                            ExcelApp.Cells[5, i].Font.Name = "Arial";
                            ExcelApp.Cells[5, i].Font.Color = Color.FromArgb(255, 255, 255);
                            ExcelApp.Cells[5, i].Interior.Color = Color.FromArgb(0, 102, 204);
                        }
                        for (int i = 0; i <= dgvPurchase.Rows.Count; i++)
                        {
                            try
                            {
                                for (int j = 0; j < dgvPurchase.Columns.Count; j++)
                                {
                                    ExcelApp.Cells[i + 6, j + 1] = dgvPurchase.Rows[i].Cells[j].Value.ToString();
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
        private void Purchase_Item_Report_Load(object sender, EventArgs e)
        {
            dptMonthly_From.Value = from1;
            dptMonthly_To.Value = to1;
            txtPurch_no.Text = pur_id1.ToString();
            decimal total = 0;
            decimal total1 = 0;
            DataTable dt = this.ctrlr.purchitem(pur_id1.ToString());//add discount in model....bahja
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgvPurchase.Rows.Add();
                    dgvPurchase.Rows[i].Cells["SL_NO"].Value = i + 1;
                    dgvPurchase.Rows[i].Cells["PurchDate"].Value = Convert.ToDateTime(dt.Rows[i]["PurchDate"].ToString()).ToString("yyyy-MM-dd");
                    dgvPurchase.Rows[i].Cells["Item_id"].Value = dt.Rows[i]["Item_Code"].ToString();
                    dgvPurchase.Rows[i].Cells["Desccription"].Value = dt.Rows[i]["Desccription"].ToString();
                    dgvPurchase.Rows[i].Cells["PACKING"].Value = dt.Rows[i]["Packing"].ToString();
                    dgvPurchase.Rows[i].Cells["UNIT"].Value = dt.Rows[i]["Unit"].ToString();
                    dgvPurchase.Rows[i].Cells["QTY"].Value = dt.Rows[i]["Qty"].ToString();
                    dgvPurchase.Rows[i].Cells["FREE"].Value = dt.Rows[i]["FreeQty"].ToString();
                    dgvPurchase.Rows[i].Cells["c_disc"].Value = dt.Rows[i]["Discount"].ToString();//bahja
                    dgvPurchase.Rows[i].Cells["UNIT_COST"].Value =Convert.ToDecimal( dt.Rows[i]["Rate"].ToString()).ToString("##.00");
                    dgvPurchase.Rows[i].Cells["GST"].Value =Convert.ToDecimal( dt.Rows[i]["GST"].ToString()).ToString("##.00");
                    dgvPurchase.Rows[i].Cells["IGST"].Value = dt.Rows[i]["IGST"].ToString();
                    dgvPurchase.Rows[i].Cells["AMOUNT"].Value =Convert.ToDecimal(  dt.Rows[i]["Amount"].ToString()).ToString("##.00");
                    total = Convert.ToDecimal(dt.Rows[i]["Amount"].ToString());
                    total1 = total + total1;
                }
                txtTotalItem.Text = dgvPurchase.Rows.Count.ToString();
                txtGrandTotal.Text = total1.ToString("##.00");
            }
            dgvPurchase.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            dgvPurchase.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPurchase.EnableHeadersVisualStyles = false;
            dgvPurchase.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;//MiddleRight
            dgvPurchase.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPurchase.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPurchase.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPurchase.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPurchase.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPurchase.Columns[9].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPurchase.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPurchase.Columns[10].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPurchase.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPurchase.Columns[11].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPurchase.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
    }
}
