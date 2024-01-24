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
using MySql.Data.MySqlClient;
using PappyjoeMVC.Controller;

namespace PappyjoeMVC.View
{
    public partial class Supplier_invoice : Form
    {
        purchase_controller cntrl = new purchase_controller();
        public MySqlConnection ccon;
        public MySqlTransaction mtran;
        public Supplier_invoice()
        {
            InitializeComponent();
        }

        public Supplier_invoice(MySqlConnection con, MySqlTransaction trans)
        {
            InitializeComponent();
            ccon = con;
            mtran = trans;
        }

        public string purno = "", supname = "", paymethod = "",supcode="",doctor_id="1";
        public decimal amount = 0, due = 0, op_balance =0;
        public static decimal advance = 0,due_amnt=0;
        public  bool purch_list_flag = false;
        private MySqlConnection con;
        private MySqlTransaction trans;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt_amount_paid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            string a = txt_amount_paid.Text;
            string b = a.TrimStart('0');
            txt_amount_paid.Text = b;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Cmb_ModeOfPaymnt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb_ModeOfPaymnt.SelectedIndex == 1)
            {
                payment_show(true, true, true, true, false, false, false);
            }
            else if (Cmb_ModeOfPaymnt.SelectedIndex == 2)
            {
                payment_show(true, false, false, false, true, true, true);
            }
            else if (Cmb_ModeOfPaymnt.SelectedIndex == 3)
            {
                payment_show(true, true, true, true, false, false, false);
            }
            else
            { payment_show(false, false, false, false, false, false, false); }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void payment_show(Boolean BankName, Boolean Number, Boolean bank, Boolean lab_number, Boolean last4digit, Boolean cardno, Boolean t4digit)
        {
            txt_BankNAme.Visible = BankName;
            txt_Number.Visible = Number;
            Bank.Visible = bank;
            Lab_Numbr.Visible = lab_number;
            Lab_Last4Digit.Visible = last4digit;
            Lab_CardNo.Visible = cardno;
            txt_4Digit.Visible = t4digit;
        }
        private void btnSaveSupplier_Click(object sender, EventArgs e)
        {
            decimal partial_total = 0;string amount_status = "Completed";
            if (Convert.ToDecimal( txt_amount_paid.Text) >0)
            {
                if (Convert.ToDecimal(txt_amount.Text) > Convert.ToDecimal(txt_amount_paid.Text))
                {
                    partial_total = Convert.ToDecimal(txt_amount.Text) - Convert.ToDecimal(txt_amount_paid.Text);
                    amount_status = "Partial";
                }
                else if (Convert.ToDecimal(txt_amount.Text) == Convert.ToDecimal(txt_amount_paid.Text))
                {
                    partial_total = 0;
                    amount_status = "Completed";
                }
                else if (Convert.ToDecimal(txt_amount.Text) < Convert.ToDecimal(txt_amount_paid.Text))
                {
                    partial_total = 0;
                    amount_status = "Completed";
                }

                if (purch_list_flag == true)
                {
                   
                    if (Cmb_ModeOfPaymnt.Text == "Cheque")
                    {
                        //this.cntrl.save_voucher_cheque(txt_voucherno.Text, txt_purno.Text, dateTimePickerdailytreatment1.Value.ToString("yyyy-MM-dd"), supcode, txt_amount.Text, Cmb_ModeOfPaymnt.Text, txt_advance.Text, txt_opbalan.Text, txt_amount_paid.Text, txt_due.Text, doctor_id, txt_BankNAme.Text, txt_Number.Text, partial_total, txt_vouto_sup.Text);

                    }
                    else if (Cmb_ModeOfPaymnt.Text == "Card")
                    {

                        //this.cntrl.save_voucher_card(txt_voucherno.Text, txt_purno.Text, dateTimePickerdailytreatment1.Value.ToString("yyyy-MM-dd"), supcode, txt_amount.Text, Cmb_ModeOfPaymnt.Text, txt_advance.Text, txt_opbalan.Text, txt_amount_paid.Text, txt_due.Text, doctor_id, txt_BankNAme.Text, txt_4Digit.Text, partial_total, txt_vouto_sup.Text);
                    }
                    else if (Cmb_ModeOfPaymnt.Text == "Demand Draft")
                    {

                        //this.cntrl.save_voucher_dd(txt_voucherno.Text, txt_purno.Text, dateTimePickerdailytreatment1.Value.ToString("yyyy-MM-dd"), supcode, txt_amount.Text, Cmb_ModeOfPaymnt.Text, txt_advance.Text, txt_opbalan.Text, txt_amount_paid.Text, txt_due.Text, doctor_id, txt_BankNAme.Text, txt_Number.Text, partial_total, txt_vouto_sup.Text);
                    }
                    else
                    {
                        this.cntrl.save_voucher(txt_voucherno.Text, txt_purno.Text, dateTimePickerdailytreatment1.Value.ToString("yyyy-MM-dd"), supcode, txt_amount.Text, Cmb_ModeOfPaymnt.Text, txt_advance.Text, txt_opbalan.Text, txt_amount_paid.Text, txt_due.Text, doctor_id, partial_total, txt_vouto_sup.Text);
                    }
                    this.cntrl.update_purchtype(txt_purno.Text);
                    this.cntrl.update_purch_Amout_status(txt_purno.Text, amount_status);
                    purch_list_flag = false;
                    if (advance != Convert.ToDecimal(txt_advance.Text))
                    {
                        this.cntrl.update_advance(supcode, txt_advance.Text);
                    }
                    if (due_amnt != Convert.ToDecimal(txt_due.Text))
                    {
                        this.cntrl.update_due(supcode, txt_due.Text);
                    }
                }
                else
                {
                    if (Cmb_ModeOfPaymnt.Text == "Cheque")
                    {
                        this.cntrl.save_voucher_cheque(txt_voucherno.Text, txt_purno.Text, dateTimePickerdailytreatment1.Value.ToString("yyyy-MM-dd"), supcode, txt_amount.Text, Cmb_ModeOfPaymnt.Text, txt_advance.Text, txt_opbalan.Text, txt_amount_paid.Text, txt_due.Text, doctor_id, txt_BankNAme.Text, txt_Number.Text, partial_total, txt_vouto_sup.Text, ccon, trans);

                    }
                    else if (Cmb_ModeOfPaymnt.Text == "Card")
                    {

                        //this.cntrl.save_voucher_card(txt_voucherno.Text, txt_purno.Text, dateTimePickerdailytreatment1.Value.ToString("yyyy-MM-dd"), supcode, txt_amount.Text, Cmb_ModeOfPaymnt.Text, txt_advance.Text, txt_opbalan.Text, txt_amount_paid.Text, txt_due.Text, doctor_id, txt_BankNAme.Text, txt_4Digit.Text, partial_total, txt_vouto_sup.Text, ccon, trans);
                    }
                    else if (Cmb_ModeOfPaymnt.Text == "Demand Draft")
                    {

                        this.cntrl.save_voucher_dd(txt_voucherno.Text, txt_purno.Text, dateTimePickerdailytreatment1.Value.ToString("yyyy-MM-dd"), supcode, txt_amount.Text, Cmb_ModeOfPaymnt.Text, txt_advance.Text, txt_opbalan.Text, txt_amount_paid.Text, txt_due.Text, doctor_id, txt_BankNAme.Text, txt_Number.Text, partial_total, txt_vouto_sup.Text, ccon, trans);
                    }
                    else
                    {
                        this.cntrl.save_voucher(txt_voucherno.Text, txt_purno.Text, dateTimePickerdailytreatment1.Value.ToString("yyyy-MM-dd"), supcode, txt_amount.Text, Cmb_ModeOfPaymnt.Text, txt_advance.Text, txt_opbalan.Text, txt_amount_paid.Text, txt_due.Text, doctor_id, partial_total, txt_vouto_sup.Text, ccon, trans);
                    }
                    if (advance != Convert.ToDecimal(txt_advance.Text))
                    {
                        this.cntrl.update_advance(supcode, txt_advance.Text,ccon,trans);
                    }
                    if(due_amnt != Convert.ToDecimal(txt_due.Text))
                    {
                        this.cntrl.update_due(supcode, txt_due.Text, ccon, trans);
                    }
                    this.cntrl.update_purch_Amout_status(txt_purno.Text, amount_status, ccon, trans);

                }

                DialogResult res = MessageBox.Show("Data inserted Successfully,Do you want to print ?", "Success ",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    PappyjoeMVC.Model.GlobalVariables.supplier_inv_flag = true;
                    Advance_paymentPrint(0);
                    var frm = new Purchase();
                    frm.Closed += (sender1, args) => this.Close();
                    this.Close();
                }
                else
                {
                    PappyjoeMVC.Model.GlobalVariables.supplier_inv_flag = true;
                    var frm = new Purchase();
                    frm.Closed += (sender1, args) => this.Close();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Please add Amount Paid ","Data not found",MessageBoxButtons.OK,MessageBoxIcon.Information);
                txt_amount_paid.Focus();
            }
        }

        private void txt_amount_paid_Click(object sender, EventArgs e)
        {
            if(txt_amount_paid.Text=="0")
            {
                txt_amount_paid.Text = "";
            }
            else if (txt_amount_paid.Text == "")
            {
                txt_amount_paid.Text = "0";
            }
        }

        private void txt_amount_paid_KeyUp(object sender, KeyEventArgs e)
        {
            label16.Visible = false;
            decimal adv = 0,baln=0,due_balan=0;
          if(txt_amount_paid.Text!="")
            {
                if (Convert.ToDecimal(txt_amount_paid.Text) > 0)
                {
                    if (Convert.ToDecimal(txt_amount_paid.Text)> Convert.ToDecimal(txt_amount.Text))
                    {
                        if(advance>0)
                        {
                            baln = Convert.ToDecimal(txt_amount_paid.Text) - Convert.ToDecimal(txt_amount.Text);
                            adv = advance + baln;
                            txt_advance.Text = adv.ToString("0.00");
                            if (due_amnt > 0)
                            {
                                if (adv > 0)
                                {
                                    if (adv >= due_amnt)
                                    {
                                        due_balan = (adv - due_amnt);
                                        txt_due.Text = "0";
                                        txt_advance.Text = due_balan.ToString();
                                        label16.Visible = true;
                                        label16.Text = "The Balance Amount Rs " + baln + "  is Saves as Advance Amount";
                                    }
                                    else
                                    {
                                        due_balan = (due_amnt - adv);
                                        txt_due.Text = due_balan.ToString();
                                        txt_advance.Text = "0";
                                    }
                                       

                                }
                                else
                                {
                                    txt_due.Text = (due_amnt).ToString();

                                }
                            }
                            else
                                txt_due.Text = "0";
                        }
                        else
                        {
                            txt_advance.Text = "0";

                               baln = Convert.ToDecimal(txt_amount_paid.Text) - Convert.ToDecimal(txt_amount.Text);
                            adv = Convert.ToDecimal(txt_advance.Text) + baln;
                            txt_advance.Text = adv.ToString("0.00");
                            if (due_amnt > 0)
                            {
                                if(adv>0)
                                {
                                    if (adv >= due_amnt)
                                    {
                                        due_balan = (adv - due_amnt);
                                        txt_due.Text = "0";
                                        txt_advance.Text = due_balan.ToString(); label16.Visible = true;
                                        label16.Text = "The Balance Amount Rs " + baln + "  is Saves as Advance Amount";
                                    }
                                    else
                                    {
                                        due_balan = (due_amnt - adv);
                                        txt_due.Text = due_balan.ToString();
                                        txt_advance.Text = "0";
                                    }

                                }
                                else
                                {
                                    txt_due.Text = (due_amnt).ToString();

                                }
                                //txt_due.Text = (due_amnt).ToString();
                            }
                            else
                                txt_due.Text = "0";
                        }
                    }
                    else
                    {
                        due = Convert.ToDecimal(txt_amount.Text) - Convert.ToDecimal(txt_amount_paid.Text);
                        txt_due.Text = "0";
                        if (due_amnt>0)
                        {
                                txt_due.Text = (due + due_amnt).ToString();
                        }
                        else
                        {
                            txt_due.Text = due.ToString();
                        }
                      
                       
                        if (Convert.ToDecimal(txt_advance.Text)>0)
                        {
                            txt_advance.Text = advance.ToString("#.00"); 
                        }
                        else
                            txt_advance.Text = "0";

                    }

                }
                else
                {
                    decimal a = 0, b = 0;
                   if(due_amnt>0)
                    {
                        a = due_amnt + Convert.ToDecimal(txt_amount.Text);
                    }
                   else
                    {
                        a = Convert.ToDecimal(txt_amount.Text);
                    }
                   if(Convert.ToDecimal(txt_advance.Text)>a)
                    {
                        txt_due.Text = "0";
                        b = Convert.ToDecimal(txt_advance.Text) - a;
                        txt_advance.Text = b.ToString();
                    }
                   else
                    {
                        MessageBox.Show("Total amount is grater than advance amount . Please enter a valid amount","Invalid",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            else if (txt_amount_paid.Text == "")
            {
                txt_amount_paid.Text = "0";
                txt_advance.Text = advance.ToString("0.00");
                if (due_amnt > 0)
                {
                    txt_due.Text = (due_amnt).ToString();
                }
                else
                    txt_due.Text = "0";
            }

        }

      

        private void Supplier_invoice_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            txt_amount_paid.Focus();
            string doctor = this.cntrl.Get_DoctorName(doctor_id);
            if(doctor!="")
            {
                txt_approved.Text = doctor;
            }
            System.Data.DataTable dtp = this.cntrl.get_company_details();
            string str_name = "", str_street_address = "", str_locality = "", str_pincode = "";
            dateTimePickerdailytreatment1.Value = DateTime.Now.Date;
            if (dtp.Rows.Count > 0)
            {
                string clinicn = "";
                clinicn = dtp.Rows[0]["name"].ToString();
                str_name = clinicn.Replace("¤", "'");
                str_street_address = dtp.Rows[0]["street_address"].ToString();
                str_locality = dtp.Rows[0]["locality"].ToString();
                str_pincode = dtp.Rows[0]["pincode"].ToString();
                label12.Text = str_name;
                label6.Text = str_street_address + "," + str_locality + "," + str_pincode;
            }
            Cmb_ModeOfPaymnt.SelectedIndex = 0;
            DataTable dt_opening = this.cntrl.get_openingbalance(supcode);
            if (dt_opening.Rows.Count > 0)
            {
                txt_opbalan.Text = Convert.ToDecimal(dt_opening.Rows[0][0].ToString()).ToString();
                txt_advance.Text = Convert.ToDecimal(dt_opening.Rows[0]["advance"].ToString()).ToString();
                advance = Convert.ToDecimal(dt_opening.Rows[0]["advance"].ToString());
                txt_due.Text= Convert.ToDecimal(dt_opening.Rows[0]["Current_Balance"].ToString()).ToString();
                due_amnt = Convert.ToDecimal(dt_opening.Rows[0]["Current_Balance"].ToString());
            }
            else
            {
                txt_opbalan.Text = "0.00";
                txt_advance.Text = "0.00";
            }
            if(purno!="")
            {
                txt_purno.Text = purno;
            }
            if(supname!="")
            {
                txt_supname.Text = supname;
            }
            if (amount > 0)
            {
                txt_amount.Text = amount.ToString();
            }
            else
                txt_amount.Text = "0.00";
            if(purch_list_flag ==true)
            {
                DataTable dt = this.cntrl.get_voucher_details(purno);
                if(dt.Rows.Count>0 && dt.Rows[0]["voucherno"].ToString() !="")
                {
                    txt_amount.Text = dt.Rows[0]["Partial_amount"].ToString();
                    txt_voucherno.Text = dt.Rows[0]["voucherno"].ToString();
                }
                else
                {
                    DataTable dtb = this.cntrl.increment_Receipt();
                    DocNumber_increment(dtb);
                }
            }
            else
            {
                DataTable dtb = this.cntrl.increment_Receipt();
                DocNumber_increment(dtb);
            }
            //if(advance>)
        }
        public void DocNumber_increment(DataTable dtb)
        {

            if (String.IsNullOrWhiteSpace(dtb.Rows[0][0].ToString()))
            {
                txt_voucherno.Text = "1";
            }
            else
            {
                int Count = Convert.ToInt32(dtb.Rows[0][0]);
                int incrValue = Convert.ToInt32(Count);
                incrValue += 1;
                txt_voucherno.Text = incrValue.ToString();
            }
        }
        public void Advance_paymentPrint(decimal total)
        {
            try
            {
                string Logopath = "";
                string str_name = "";
                string str_street_address = "";
                string str_locality = "";
                string str_pincode = "";
                string str_contact_no = "";
                string str_email = "";
                string str_website = "";
                string patiID = "";
                string PatName = "";
                string address = "";
                DataTable dt_patent = new DataTable();
                System.Data.DataTable dtp = this.cntrl.get_company_details();
                if (dtp.Rows.Count > 0)
                {
                    string clinicn = "";
                    clinicn = dtp.Rows[0]["name"].ToString();
                    str_name = clinicn.Replace("¤", "'");
                    str_street_address = dtp.Rows[0]["street_address"].ToString();
                    str_locality = dtp.Rows[0]["locality"].ToString();
                    str_pincode = dtp.Rows[0]["pincode"].ToString();
                    str_contact_no = dtp.Rows[0]["contact_no"].ToString();
                    str_email = dtp.Rows[0]["email"].ToString();
                    str_website = dtp.Rows[0]["website"].ToString();
                    Logopath = dtp.Rows[0]["path"].ToString();
                }
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                StreamWriter swriter = new StreamWriter(Apppath + "\\AdvancePaymentReceipt_print.html");
                swriter.WriteLine("<Html>");
                swriter.WriteLine("<head>");
                swriter.WriteLine("<style>");
                swriter.WriteLine("table { border-collapse: collapse;}");
                swriter.WriteLine("p.big {line-height: 400%;}");
                swriter.WriteLine("</style>");
                swriter.WriteLine("</head>");
                swriter.WriteLine("<body>");
                if (File.Exists(Apppath + "\\" + Logopath))
                {
                    swriter.WriteLine("<table align='center' width=500px;border:1px;border-collapse:collapse;'>");
                    swriter.WriteLine("<tr>");
                    swriter.WriteLine("<td width=100px; height=85px; rowSpan=4><img src='" + Apppath + "\\" + Logopath + "' width='77' height='78' style='width:100px; hwight:100px;' ></td>");
                    swriter.WriteLine("<td align='left' width='488' height='25px';><FONT COLOR='black' FACE='segoe UI' SIZE=4><b>" + str_name + "</b></FONT></td> ");
                    swriter.WriteLine("<tr><td align='left' ><FONT COLOR='black' FACE='segoe UI' SIZE=2>" + str_street_address + "</FONT></td></tr>");
                    swriter.WriteLine("<tr><td align='left' ><FONT COLOR='black' FACE='segoe UI' SIZE=2>" + str_email + "</FONT></td>");
                    swriter.WriteLine("<tr><td align='left' ><FONT COLOR='black' FACE='segoe UI' SIZE=2>" + str_contact_no + "</FONT></td>");
                    swriter.WriteLine("<tr><td colspan='2'; align='left'><hr></td></tr>");
                    swriter.WriteLine("</table>");
                }
                else
                {
                    swriter.WriteLine("<table align='center' style='width:500px;border:1px;border-collapse:collapse;'>");
                    swriter.WriteLine("<tr>");
                    swriter.WriteLine("<td align='left'> <FONT COLOR='black' FACE='segoe UI' SIZE=4><b>" + str_name + "</b></FONT></td>");
                    swriter.WriteLine("</tr>");
                    swriter.WriteLine("<tr>");
                    swriter.WriteLine("<td align='left'> <FONT COLOR='black' FACE='segoe UI' SIZE=2>" + str_street_address + "</FONT></td>");
                    swriter.WriteLine("</tr>");
                    swriter.WriteLine("<tr>");
                    swriter.WriteLine("<td align='left'> <FONT COLOR='black' FACE='segoe UI' SIZE=2>" + str_locality + "</FONT></td>");
                    swriter.WriteLine("</tr>");
                    swriter.WriteLine("<tr>");
                    swriter.WriteLine("<td align='left'> <FONT COLOR='black' FACE='segoe UI' SIZE=2>" + str_email + "</FONT></td>");
                    swriter.WriteLine("</tr>");
                    swriter.WriteLine("<tr>");
                    swriter.WriteLine("<td align='left'> <FONT COLOR='black' FACE='segoe UI' SIZE=2>" + str_contact_no + "</FONT></td>");
                    swriter.WriteLine("</tr>");
                    swriter.WriteLine("<tr><td colspan='2'; align='left'><hr></td></tr>");
                    swriter.WriteLine("</table>");
                }
                swriter.WriteLine("<table align='center' width='500'>");
                swriter.WriteLine("<tr>");
                swriter.WriteLine("<td colspan=2> <center> <FONT Color=black face='Segoe UI' SIZE=3> <b> RECEIPT VOUCHER</b></FONT></center></td>");
                swriter.WriteLine("</tr>");
                swriter.WriteLine("</table>");
                swriter.WriteLine("<br>");
                swriter.WriteLine("<table align='center' width='500'>");
                swriter.WriteLine("<tr>");
                swriter.WriteLine("<td align='left'><FONT COLOR ='black' FACE='segoe UI' SIZE=2>Supplier Name : <b>" + txt_supname.Text + "</b></FONT></td>");
                swriter.WriteLine("<td align='right'><FONT COLOR ='black' FACE='segoe UI' SIZE=2>Date: " + DateTime.Now.Date.ToShortDateString() + "</FONT></td>");
                swriter.WriteLine("<tr><td colspan='2'; align='left'><hr></td></tr>");
                swriter.WriteLine("</table>");
                swriter.WriteLine("<table align='center' border='1' width='500' >");
                swriter.WriteLine("<tr>");
                swriter.WriteLine("<th width='329'><FONT COLOR ='black' FACE='segoe UI' SIZE=3>Details</FONT></th>");
                swriter.WriteLine("<th width='105'><FONT COLOR ='black' FACE='segoe UI' SIZE=3>Amount</FONT></th>");
                swriter.WriteLine("</tr>");
                swriter.WriteLine("<tr>");
                swriter.WriteLine("<td height='25'valign='top' Halign='center'><FONT COLOR ='black' FACE='segoe UI' SIZE=2> <span style='float:left'>Amount</span></FONT></td>");
                swriter.WriteLine("<td height='25'valign='top' ><FONT COLOR ='black' FACE='segoe UI' SIZE=2> <span style='float:right'>Rs." + Convert.ToDecimal(txt_amount.Text).ToString("#0.00") + "</span></FONT></td>");
                swriter.WriteLine("</tr>");
                swriter.WriteLine("<tr>");
                swriter.WriteLine("<td height='25'valign='top' Halign='center'><FONT COLOR ='black' FACE='segoe UI' SIZE=2> <span style='float:left'>Advance</span></FONT></td>");
                swriter.WriteLine("<td height='25'valign='top' ><FONT COLOR ='black' FACE='segoe UI' SIZE=2> <span style='float:right'>Rs." + Convert.ToDecimal(txt_advance.Text).ToString("#0.00") + "</span></FONT></td>");
                swriter.WriteLine("</tr>");

                swriter.WriteLine("<tr>");
                swriter.WriteLine("<td height='25'valign='top' Halign='center'><FONT COLOR ='black' FACE='segoe UI' SIZE=2> <span style='float:left'>Opening Balance</span></FONT></td>");
                swriter.WriteLine("<td height='25'valign='top' ><FONT COLOR ='black' FACE='segoe UI' SIZE=2> <span style='float:right'>Rs." + Convert.ToDecimal(txt_opbalan.Text).ToString("#0.00") + "</span></FONT></td>");
                swriter.WriteLine("</tr>");

                swriter.WriteLine("<tr>");
                swriter.WriteLine("<td height='25'valign='top' Halign='center'><FONT COLOR ='black' FACE='segoe UI' SIZE=2> <span style='float:left'>Amount Paid</span></FONT></td>");
                swriter.WriteLine("<td height='25'valign='top' ><FONT COLOR ='black' FACE='segoe UI' SIZE=2> <span style='float:right'>Rs." + Convert.ToDecimal(txt_amount_paid.Text).ToString("#0.00") + "</span></FONT></td>");
                swriter.WriteLine("</tr>");

                swriter.WriteLine("<tr color ='white'>");
                swriter.WriteLine("<td height='25'valign='top' Halign='center'><FONT COLOR ='black' FACE='segoe UI' SIZE=2> <span style='float:left'>Due</span></FONT></td>");
                swriter.WriteLine("<td height='25'valign='top' ><FONT COLOR ='black' FACE='segoe UI' SIZE=2> <span style='float:right'>Rs." + Convert.ToDecimal(txt_due.Text).ToString("#0.00") + "</span></FONT></td>");
                swriter.WriteLine("</tr>");
                swriter.WriteLine("</table>");
                swriter.WriteLine("<br>");
                swriter.WriteLine("<table align='center'  style='width:500px; border:1px;border-collaspe:collapse;'>");
                if(Convert.ToDecimal(txt_advance.Text)>0)
                {
                    swriter.WriteLine("<tr>");
                    swriter.WriteLine("<td colspan='2'  align='right'><FONT COLOR ='black' FACE='segoe UI' SIZE=2><b> "+ label16.Text+" </b></FONT></td>");
                    swriter.WriteLine("</tr>");
                }
                swriter.WriteLine("<tr>");
                swriter.WriteLine("<td colspan='2'  align='right'><FONT COLOR ='black' FACE='segoe UI' SIZE=2><b>Signature</b></FONT></td>");
                swriter.WriteLine("</tr>");
                swriter.WriteLine("</table>");
                swriter.WriteLine("</body>");
                swriter.WriteLine("</Html>");
                swriter.Close();
                System.Diagnostics.Process.Start(Apppath + "\\AdvancePaymentReceipt_print.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Some error occured!..please try again later..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
