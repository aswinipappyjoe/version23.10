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
    public partial class Account_Statement : Form
    {
        purchase_controller cntrl = new purchase_controller();
        PurchaseList_controller ctrl = new PurchaseList_controller();
        public string doctor_id = "", select_dr_id = "0", strclinicname = "", strStreet = "", stremail = "", strwebsite = "", strphone = "", clinicn = "", PathName = "";

        private void dgv_Stock_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            {
                string pur = "";int retno=0; PappyjoeMVC.Model.Connection.MyGlobals.global_Flag = true;
                if (dgv_Stock.CurrentCell.OwningColumn.Name=="purno")
                {
                    pur = dgv_Stock.CurrentRow.Cells["purno"].Value.ToString();
                    if(pur!="")
                    {
                        DataTable data_from_Pur_Master = this.ctrl.data_from_Pur_Master(pur);
                        DataTable data_from_purchase = this.ctrl.data_from_purchase(pur);
                        var form2 = new Purchase(data_from_Pur_Master, data_from_purchase);
                        form2.ShowDialog();
                    }
                }
                else if (dgv_Stock.CurrentCell.OwningColumn.Name == "return_no")
                {
                    if(dgv_Stock.CurrentRow.Cells["return_no"].Value !=null && dgv_Stock.CurrentRow.Cells["return_no"].Value.ToString()!="")
                    {
                        retno = Convert.ToInt32(dgv_Stock.CurrentRow.Cells["return_no"].Value.ToString());
                        if (retno > 0)
                        {
                            //    DataTable data_from_Pur_Master = this.ctrl.data_from_Pur_Master(pur);
                            //    DataTable data_from_purchase = this.ctrl.data_from_purchase(pur);
                            var form2 = new Purchase_Return(retno);
                            form2.ShowDialog();
                        }
                    }
                }
            }
        }

        public Account_Statement()
        {
            InitializeComponent();
        }

        private void Account_Statement_Load(object sender, EventArgs e)
        {
            dgv_Stock.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            dgv_Stock.EnableHeadersVisualStyles = false;
            this.dgv_Stock.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgv_Stock.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            foreach (DataGridViewColumn cl in dgv_Stock.Columns)
            {
                cl.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dtp_from.Value = DateTime.Now.AddMonths(-1).Date;
            dtp_to.Value = DateTime.Now.Date;
            DataTable dtb = this.cntrl.Load_Suplier();
            if(dtb.Rows.Count>0)
            {
                cmbUnit.Items.Clear();
                cmbUnit.Items.Add("All Supplier");
                cmbUnit.ValueMember = "0";
                cmbUnit.DisplayMember = "All Supplier";
                foreach (DataRow dr in dtb.Rows)
                {
                    cmbUnit.Items.Add(dr["Supplier_Name"].ToString());
                    cmbUnit.ValueMember = dr["Supplier_Code"].ToString();
                    cmbUnit.DisplayMember = dr["Supplier_Name"].ToString();
                }
                cmbUnit.SelectedIndex = 0;
            }
            string date1="", date2="";// string supplier
            date1 = dtp_from.Value.ToString("yyyy-MM-dd");
            date2 = dtp_to.Value.ToString("yyyy-MM-dd");
            DataTable dtb_load = new DataTable();
            if(cmbUnit.Text=="All Supplier")
            {
                dtb_load = this.cntrl.dtload(date1, date2, "All Supplier");

            }
            else if(cmbUnit.Text!="")
            {
                dtb_load = this.cntrl.dtload(date1, date2, cmbUnit.SelectedValue.ToString());
            }

            load(dtb_load);
            //decimal totao_amot = 0, total_paid = 0, total_due = 0;
            //if(dtb_load.Rows.Count>0)
            //{
            //    dgv_Stock.Rows.Clear();
            //    foreach (DataRow dr in dtb_load.Rows)
            //    {//voucherno,purchno,date,supplierid,amount,paymethod,advance,opening_balance,Amount_paid,Due,Applied_to
            //        dgv_Stock.Rows.Add(dr["id"].ToString(), dr["date"].ToString(), dr["voucherno"].ToString(), dr["purchno"].ToString(), dr["supplierid"].ToString(), dr["amount"].ToString(), dr["Amount_paid"].ToString(), dr["Due"].ToString());
            //        totao_amot = totao_amot + Convert.ToDecimal(dr["amount"].ToString());
            //        total_paid = total_paid + Convert.ToDecimal(dr["Amount_paid"].ToString());
            //        total_due = total_due + Convert.ToDecimal(dr["Due"].ToString());
            //    }
            //    dgv_Stock.Rows.Add("","","","","Total ",totao_amot.ToString("0.00"),total_paid.ToString("0.00"),total_due.ToString("0.00"));
            //}
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            string date1 = "", date2 = "";// string supplier
            date1 = dtp_from.Value.ToString("yyyy-MM-dd");
            date2 = dtp_to.Value.ToString("yyyy-MM-dd");
            DataTable dtb_load = new DataTable();
            if (cmbUnit.Text == "All Supplier")
            {
                dtb_load = this.cntrl.dtload(date1, date2, "All Supplier");

            }
            else
            {
                DataTable dt = this.cntrl.get_suppcode(cmbUnit.Text);
                dtb_load = this.cntrl.dtload(date1, date2, dt.Rows[0][0].ToString());
            }
            load(dtb_load);
        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            string date1 = "", date2 = "";// string supplier
            date1 = dtp_from.Value.ToString("yyyy-MM-dd");
            date2 = dtp_to.Value.ToString("yyyy-MM-dd");
            DataTable dtb_load = new DataTable();
            if (cmbUnit.Text == "All Supplier")
            {
                dtb_load = this.cntrl.dtload(date1, date2, "All Supplier");

            }
            else
            {
                DataTable  dt = this.cntrl.get_suppcode(cmbUnit.Text);
                dtb_load = this.cntrl.dtload(date1, date2, dt.Rows[0][0].ToString());
            }
            load(dtb_load);
        }
        public void load(DataTable dtb_load)
        {
            dgv_Stock.Rows.Clear();
            decimal totao_amot = 0, total_paid = 0, total_due = 0,total_adv=0,total_ret=0,due=0;
            if (dtb_load.Rows.Count > 0)
            {
               

                foreach (DataRow dr in dtb_load.Rows)
                {//voucherno,purchno,date,supplierid,amount,paymethod,advance,opening_balance,Amount_paid,Due,Applied_to
                    DataTable dt_return = this.cntrl.return_details(dr["purchno"].ToString());
                    string dt_sup = this.cntrl.get_suppliercode(dr["supplierid"].ToString());
                    string retno = "";decimal retAmount =0;
                    if(dt_return.Rows.Count>0)
                    {
                        retno = dt_return.Rows[0]["RetNumber"].ToString();
                        retAmount =Convert.ToDecimal( dt_return.Rows[0]["TotalAmount"].ToString());
                    }
                    dgv_Stock.Rows.Add(dr["id"].ToString(),Convert.ToDateTime( dr["date"].ToString()).ToString("dd-MM-yyyy"), dr["voucherno"].ToString(), dr["purchno"].ToString(), retno, dt_sup, dr["amount"].ToString(), dr["Amount_paid"].ToString(), dr["advance"].ToString(), dr["Due"].ToString(), retAmount);
                    totao_amot = totao_amot + Convert.ToDecimal(dr["amount"].ToString());
                    total_paid = total_paid + Convert.ToDecimal(dr["Amount_paid"].ToString());
                    if (cmbUnit.Text != "All Supplier")
                        due = Convert.ToDecimal(dr["Due"].ToString());//total_due + Convert.ToDecimal(dr["Due"].ToString());
                    else
                        

                    total_adv = total_adv + Convert.ToDecimal(dr["advance"].ToString());
                    total_ret = total_ret + Convert.ToDecimal(retAmount);

                }
                if (cmbUnit.Text == "All Supplier")
                    dgv_Stock.Rows.Add("", "", "", "","", "Total ", totao_amot.ToString("0.00"), total_paid.ToString("0.00"), total_adv.ToString("0.00"), "", total_ret.ToString("0.00"));
                else
                    dgv_Stock.Rows.Add("", "", "", "", "", "Total ", totao_amot.ToString("0.00"), total_paid.ToString("0.00"), total_adv.ToString("0.00"), due.ToString("0.00"), total_ret.ToString("0.00"));
                int row = dgv_Stock.Rows.Count;

                dgv_Stock.Rows[row - 1].Cells[5].Style.ForeColor = Color.Red;
                dgv_Stock.Rows[row - 1].Cells[6].Style.ForeColor = Color.Red;
                dgv_Stock.Rows[row - 1].Cells[7].Style.ForeColor = Color.Red;
                dgv_Stock.Rows[row - 1].Cells[8].Style.ForeColor = Color.Red;
                dgv_Stock.Rows[row - 1].Cells[9].Style.ForeColor = Color.Red;
                dgv_Stock.Rows[row - 1].Cells[10].Style.ForeColor = Color.Red;

                dgv_Stock.Rows[row - 1].Cells[5].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                dgv_Stock.Rows[row - 1].Cells[6].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                dgv_Stock.Rows[row - 1].Cells[7].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                dgv_Stock.Rows[row - 1].Cells[8].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                dgv_Stock.Rows[row - 1].Cells[9].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                dgv_Stock.Rows[row - 1].Cells[10].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                //dgv_Stock.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgv_Stock.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgv_Stock.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_Stock.Rows.Count > 0)
                {
                    string message = "Did you want Header on Print?";
                    string caption = "Verification";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons);
                    int c = 0;
                    int sl = 0;
                    string logo_name = "";
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        DataTable dt = this.cntrl.get_company_details();
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
                    System.IO.StreamWriter sWrite = new System.IO.StreamWriter(Apppath + "\\accountstatement.html");
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

                        //sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");

                        sWrite.WriteLine("</table>");
                    }
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr><td align='left'  colspan='2'><hr/></td></tr>");
                    sWrite.WriteLine("</table>");
                    //if (combodoctors.SelectedIndex == 0)
                    //{
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3> ACCOUNT STATEMENT  </font></center></b></td>");
                    sWrite.WriteLine("</tr>");
                    //}
                    //if (combodoctors.SelectedIndex != 0)
                    //{
                    //sWrite.WriteLine("<tr>");
                    //sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3> MONTHLY INVOICE REPORT (" + combodoctors.Text + ")  </font></center></b></td>");
                    //sWrite.WriteLine("</tr>");
                    //}
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=10 align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>From:</b>  " + dtp_from.Value.ToString("dd/MM/yyyy") + " </font></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=10 align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>To: </b> " + dtp_to.Value.ToString("dd/MM/yyyy") + " </font></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=10 align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2><b> Printed Date:</b>  " + DateTime.Now.ToString("dd/MM/yyyy") + " </font></td>");
                    sWrite.WriteLine("</tr>");
                    //if (combodoctors.SelectedIndex != 0)
                    //{
                    //    sWrite.WriteLine("<tr>");
                    //    sWrite.WriteLine("<td colspan=10 align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2> <b>Doctor: </b> " + combodoctors.Text + " </font></td>");
                    //    sWrite.WriteLine("</tr>");
                    //}
                    if (dgv_Stock.Rows.Count > 0)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("    <td align='left' width='6%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3 ><b>&nbsp;Slno. </b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='15%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3 ><b>&nbsp;Date </b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;Voucher No</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='24%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Purchase No</b></font></td>");
                        sWrite.WriteLine("    <td align='right' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;Return No</b></font></td>");

                        sWrite.WriteLine("    <td align='left' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;Supplier</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;Total Amount(Rs)</b></font></td>");
                        sWrite.WriteLine("    <td align='right' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Amount Paid (Rs)</b></font></td>");
                        sWrite.WriteLine("    <td align='right' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Advance(Rs)</b></font></td>");
                        sWrite.WriteLine("    <td align='right' width='13%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Due</b></font></td>");
                        sWrite.WriteLine("    <td align='right' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;Return Amount</b></font></td>");
                        //sWrite.WriteLine("    <td align='right' width='30%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>Total Payment</b></font></td>");
                        //sWrite.WriteLine("    <td align='right' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;Amount Due</b></font></td>");
                        sWrite.WriteLine("</tr>");
                        while (c < dgv_Stock.Rows.Count)
                        {
                            sl = c + 1;
                            sWrite.WriteLine("<tr>");
                            if(dgv_Stock.Rows[c].Cells[5].Value.ToString()=="Total ")
                            {
                                sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;</font></td>");
                            }
                            else
                            {
                                sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + sl + "</font></td>");
                            }
                           
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgv_Stock.Rows[c].Cells[1].Value.ToString() + "</font></td>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgv_Stock.Rows[c].Cells[2].Value.ToString() + "</font></td>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgv_Stock.Rows[c].Cells[3].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='center' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgv_Stock.Rows[c].Cells[4].Value.ToString() + "</font></td>");
                            if (dgv_Stock.Rows[c].Cells[5].Value.ToString() == "Total ")
                            {
                                sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;" + dgv_Stock.Rows[c].Cells[5].Value.ToString() + "</b></font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;" + Convert.ToDecimal(dgv_Stock.Rows[c].Cells[6].Value.ToString()).ToString("#0.00") + "&nbsp;</b></font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;" + dgv_Stock.Rows[c].Cells[7].Value.ToString() + "</b></font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;" + dgv_Stock.Rows[c].Cells[8].Value.ToString() + "</b></font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;" + dgv_Stock.Rows[c].Cells[9].Value.ToString() + "</b></font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;" + dgv_Stock.Rows[c].Cells[10].Value.ToString() + "&nbsp;</b></font></td>");
                            }
                            else
                            {
                                sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgv_Stock.Rows[c].Cells[5].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + Convert.ToDecimal(dgv_Stock.Rows[c].Cells[6].Value.ToString()).ToString("#0.00") + "&nbsp;</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgv_Stock.Rows[c].Cells[7].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgv_Stock.Rows[c].Cells[8].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgv_Stock.Rows[c].Cells[9].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgv_Stock.Rows[c].Cells[10].Value.ToString() + "&nbsp;</font></td>");
                            }
                                
                            //sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvMonthlyReports.Rows[c].Cells["due"].Value.ToString() + "&nbsp;</font></td>");
                            sWrite.WriteLine("</tr>");
                            c++;
                        }
                        int row = dgv_Stock.Rows.Count;
                        //sWrite.WriteLine("<tr>");
                        //sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        //sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        //sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        //sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></th>");
                        //sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></th>");
                        //sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        //sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>Total&nbsp;</font></td>");
                        ////sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>" + lbltotal2.Text + "</b></font></td>");
                        ////sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2></font></td>");
                        ////sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>" + lbltotal1.Text + "</b></font></td>");
                        ////sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>" + LabTotal3.Text + "</b>&nbsp;</font></td>");
                        ////sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>" + lbltotal3.Text + "</b>&nbsp;</font></td>");
                        //sWrite.WriteLine("</tr>");
                        sWrite.WriteLine("</table>");
                    }
                    sWrite.WriteLine("</div>");
                    sWrite.WriteLine("<script>window.print();</script>");
                    sWrite.WriteLine("</body>");
                    sWrite.WriteLine("</html>");
                    sWrite.Close();
                    System.Diagnostics.Process.Start(Apppath + "\\accountstatement.html");
                }
                else
                { MessageBox.Show("No Record Found,please change the date and try again !..", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error!...", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
