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
    public partial class Patient_consultation_report : Form
    {
        patient_consultation_report_controller cntrl = new patient_consultation_report_controller();
        public Patient_consultation_report()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Patient_consultation_report_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime date = new DateTime(now.Year, now.Month, 1);
            dateTimePickerdailytreatment1.Value = date;
            load_doctor();
            load_all_doctor_data();
            dgvPatient.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            dgvPatient.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPatient.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 8, FontStyle.Regular);
            dgvPatient.EnableHeadersVisualStyles = false;
            dgvPatient.Location = new System.Drawing.Point(5, 5);
            foreach (DataGridViewColumn cl in dgvPatient.Columns)
            {
                cl.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            //dgvPatient.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvPatient.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvPatient.Columns[9].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvPatient.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvPatient.Columns[10].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvPatient.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
        public void load_doctor()
        {
            cmb_doctor.Items.Clear();
            cmb_doctor.Items.Add("All Doctor");
            cmb_doctor.ValueMember = "0";
            cmb_doctor.DisplayMember = "All Doctor";
            DataTable dtb = this.cntrl.Load_doctor();
            if(dtb.Rows.Count>0)
            {
                for (int i = 0; i < dtb.Rows.Count; i++)
                {
                    cmb_doctor.Items.Add(dtb.Rows[i]["doctor_name"].ToString());
                    cmb_doctor.ValueMember = dtb.Rows[i]["id"].ToString();
                    cmb_doctor.DisplayMember = dtb.Rows[i]["doctor_name"].ToString();
                }
                cmb_doctor.SelectedIndex = 0;
            }
        }
        public void load_all_doctor_data()
        {
            var dateFrom = dateTimePickerdailytreatment1.Value.ToString("yyyy-MM-dd");
            var dateTo = dateTimePickerdailytreatment2.Value.ToString("yyyy-MM-dd");
            if (Convert.ToDateTime(dateFrom).Date > Convert.ToDateTime(dateTo).Date)
            {
                MessageBox.Show("From date should be less than To date", "From Date is grater ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateTimePickerdailytreatment1.Value = DateTime.Today;
                return;
            }
            DataTable dtb_load = new DataTable();
            if (cmb_doctor.Text== "All Doctor")
            {
                dtb_load = this.cntrl.load_data(dateFrom, dateTo);

            }
            else
            {
                dtb_load = this.cntrl.load_data_doctor_wisw(dateFrom, dateTo, cmb_doctor.Text);
            }
            if (dtb_load.Rows.Count>0)
            {
                int k = 1;decimal total = 0;
                Lab_Msg.Visible = false;
                dgvPatient.Rows.Clear();
                for(int i=0;i< dtb_load.Rows.Count;i++)
                {
                    DataTable dt_patient = this.cntrl.get_patient_details(dtb_load.Rows[i]["Patientid"].ToString());
                    DataTable dt_doctor = this.cntrl.get_doctor_fee(dtb_load.Rows[i]["dr_id"].ToString());
                    dgvPatient.Rows.Add();
                    dgvPatient.Rows[i].Cells["sl_no"].Value = k;
                    dgvPatient.Rows[i].Cells["patient_name"].Value = dtb_load.Rows[i]["patient_name"].ToString();
                    if(dt_patient.Rows.Count>0)
                    {
                        dgvPatient.Rows[i].Cells["Sex"].Value = dt_patient.Rows[0]["gender"].ToString();
                        dgvPatient.Rows[i].Cells["age"].Value = dt_patient.Rows[0]["age"].ToString();
                        dgvPatient.Rows[i].Cells["patient_id"].Value = dt_patient.Rows[0]["pt_id"].ToString();
                    }
                    dgvPatient.Rows[i].Cells["Doctor"].Value = dtb_load.Rows[i]["doctorname"].ToString();
                    dgvPatient.Rows[i].Cells["date"].Value = dtb_load.Rows[i]["date"].ToString();
                    if (dt_doctor.Rows.Count > 0)
                    {
                        dgvPatient.Rows[i].Cells["fee"].Value =Convert.ToDecimal( dt_doctor.Rows[0]["fee"].ToString()).ToString("0.00");
                        dgvPatient.Rows[i].Cells["f_fee"].Value =Convert.ToDecimal( dt_doctor.Rows[0]["followup_fee"].ToString()).ToString("0.00");
                    }
                    else
                    {
                        dgvPatient.Rows[i].Cells["fee"].Value = "0.00";
                        dgvPatient.Rows[i].Cells["f_fee"].Value = "0.00";
                    }
                    dgvPatient.Rows[i].Cells["total"].Value =Convert.ToDecimal( dtb_load.Rows[i]["amount"].ToString()).ToString("0.00");
                    total = total + Convert.ToDecimal(dtb_load.Rows[i]["amount"].ToString());
                    k++;
                }
                Lab_Total.Text = dgvPatient.Rows.Count.ToString();
                dgvPatient.Rows.Add("","","","","","","","","TOTAL", total.ToString("0.00"));
            }
            else
            {
                Lab_Total.Text = "0";
                Lab_Msg.Visible = true;
            }
        }

        private void btnselect_Click(object sender, EventArgs e)
        {
            if(cmb_doctor.Text!= "")
            {
                load_all_doctor_data();
            }
        }

        private void cmb_doctor_Click(object sender, EventArgs e)
        {
            load_doctor();
        }
        //string PathName = "";
        private void btn_Export_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPatient.Rows.Count != 0)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Excel Files (*.xls)|*.xls";
                    saveFileDialog1.FileName = "Patient Consultation Report(" + DateTime.Now.ToString("dd-MM-yy h.mm.ss tt") + ").xls";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        PathName = saveFileDialog1.FileName;
                        Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                        ExcelApp.Application.Workbooks.Add(Type.Missing);
                        ExcelApp.Columns.ColumnWidth = 20;
                        int count = dgvPatient.Columns.Count;
                        ExcelApp.Range[ExcelApp.Cells[1, 1], ExcelApp.Cells[1, count]].Merge();
                        ExcelApp.Cells[1, 1] = "PATIENT CONSULTATION REPORT";
                        ExcelApp.Cells[1, 1].HorizontalAlignment = HorizontalAlignment.Center;
                        ExcelApp.Cells[1, 1].Font.Size = 12;
                        ExcelApp.Cells[1, 1].Interior.Color = Color.FromArgb(153, 204, 255);
                        ExcelApp.Columns.ColumnWidth = 20;
                        ExcelApp.Cells[2, 1] = "Date";
                        ExcelApp.Cells[2, 1].Font.Size = 10;
                        ExcelApp.Cells[2, 2] = DateTime.Now.Date.ToString("dd-MM-yyyy");
                        ExcelApp.Cells[2, 2].Font.Size = 10;
                        ExcelApp.Cells[3, 1] = "Running Date";
                        ExcelApp.Cells[3, 1].Font.Size = 10;
                        ExcelApp.Cells[3, 2] = DateTime.Now.ToString("dd-MM-yyyy");
                        ExcelApp.Cells[3, 2].Font.Size = 10;
                        ExcelApp.Cells[3, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        ExcelApp.Cells[2, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        for (int i = 1; i < dgvPatient.Columns.Count + 1; i++)
                        {
                            ExcelApp.Cells[5, i] = dgvPatient.Columns[i - 1].HeaderText;
                            ExcelApp.Cells[5, i].ColumnWidth = 25;
                            ExcelApp.Cells[5, i].EntireRow.Font.Bold = true;
                            ExcelApp.Cells[5, i].Interior.Color = Color.FromArgb(0, 102, 204);
                            ExcelApp.Cells[5, i].Font.Size = 10;
                            ExcelApp.Cells[5, i].Font.Name = "Arial";
                            ExcelApp.Cells[5, i].Font.Color = Color.FromArgb(255, 255, 255);
                            ExcelApp.Cells[5, i].Interior.Color = Color.FromArgb(0, 102, 204);
                        }
                        for (int i = 0; i <= dgvPatient.Rows.Count; i++)
                        {
                            try
                            {
                                for (int j = 0; j < dgvPatient.Columns.Count; j++)
                                {
                                    ExcelApp.Cells[i + 6, j + 1] = dgvPatient.Rows[i].Cells[j].Value.ToString();
                                    ExcelApp.Cells[i + 6, j + 1].BorderAround(true);
                                    ExcelApp.Cells[i + 6, j + 1].Borders.Color = Color.FromArgb(0, 102, 204);
                                    ExcelApp.Cells[i + 6, j + 1].Font.Size = 8;
                                }
                            }
                            catch
                            {

                            }
                        }
                        ExcelApp.ActiveWorkbook.SaveAs(PathName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                        ExcelApp.ActiveWorkbook.Saved = true;
                        ExcelApp.Quit();
                        //checkStr = "1";
                        MessageBox.Show("Successfully Exported to Excel", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                    MessageBox.Show("No records found,please change the date and try again!..", "Failed ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error!...", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        public string dateFrom = "", dateTo = "", checkStr = "", PathName = "", strclinicname = "", clinicn = "", strStreet = "", stremail = "", strwebsite = "", strphone = "", logo_name = "";

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPatient.Rows.Count > 0)
                {
                    string today = DateTime.Now.ToString("dd/MM/yyyy");
                    string message = "Did you want Header on Print?";
                    string caption = "Verification";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        DataTable dtp = this.cntrl.get_company_details();
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
                    System.IO.StreamWriter sWrite = new System.IO.StreamWriter(Apppath + "\\patient_consultation.html");
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
                    sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3> PATIENT CONSULTATION REPOET  </font></center></b></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=7 align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> <b>From: </b>" + dateTimePickerdailytreatment1.Value.ToString("dd/MM/yyyy") + " </font></td> ");
                    sWrite.WriteLine("<td colspan=9 align='left'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2> <b>To: </b>" + dateTimePickerdailytreatment2.Value.ToString("dd/MM/yyyy") + " </font></td> ");

                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=7 align='left' colspan=7><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>Doctor: </b>" + cmb_doctor.Text + "</font></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=7 align='left' colspan=7><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>Printed Date: </b>" + today + "</font></td>");
                    sWrite.WriteLine("</tr>");
                    int rownum = 0;
                    if (dgvPatient.Rows.Count > 0)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("    <td align='left' width='7%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3 ><b>&nbsp;SLNO.</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='14%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;PATIENT ID</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;PATIENT NAME</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='7%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;GENDER</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='15%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;AGE</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;DOCTOR</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;LAST VISITED DATE</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;FEE</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='18%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;FOLLOWUP FEE</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='18%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;TOTAL AMOUNT PAID</b></font></td>");
                        sWrite.WriteLine("</tr>");
                        for (int c = 0; c < dgvPatient.Rows.Count; c++)
                        {
                            if (dgvPatient.Rows[c].Cells["SL_NO"].Value.ToString() != "")
                            {
                                sWrite.WriteLine("<tr>");
                                sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvPatient.Rows[c].Cells["sl_no"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvPatient.Rows[c].Cells["patient_id"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvPatient.Rows[c].Cells["patient_name"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvPatient.Rows[c].Cells["Sex"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvPatient.Rows[c].Cells["age"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvPatient.Rows[c].Cells["doctor"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvPatient.Rows[c].Cells["date"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvPatient.Rows[c].Cells["fee"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvPatient.Rows[c].Cells["f_fee"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvPatient.Rows[c].Cells["total"].Value.ToString() + "</font></td>");
                                rownum = c;
                            }
                        }
                        sWrite.WriteLine("</tr>");
                        //sWrite.WriteLine("<tr>");
                        //sWrite.WriteLine("<td align='center'  style='border:1px solid #000' colspan=4 ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b> TOTAL</b></font></td>");
                        //sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=3>  " + Grvsummary.Rows[rownum + 1].Cells["TAXABLEVAMOUNT"].Value.ToString() + " </font></td>");
                        //sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=3>   </font></td>");
                        //sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=3>  " + Grvsummary.Rows[rownum + 1].Cells["SGST"].Value.ToString() + " </font></td>");
                        //sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=3>  " + Grvsummary.Rows[rownum + 1].Cells["CGST"].Value.ToString() + " </font></td>");
                        //sWrite.WriteLine("<td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=3>  " + Grvsummary.Rows[rownum + 1].Cells["TOTAL"].Value.ToString() + " </font></td>");
                        //sWrite.WriteLine("</tr>");

                        //sWrite.WriteLine("<tr>");
                        //sWrite.WriteLine("<td align='right'  colspan=8 ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b> Discount :</b></font></td>");
                        //sWrite.WriteLine("<td align='right'  colspan=5 ><FONT COLOR=black FACE='Segoe UI' SIZE=3>  " + Grvsummary.Rows[rownum + 2].Cells["TOTAL"].Value.ToString() + " </font></td>");
                        //sWrite.WriteLine("</tr>");

                        //sWrite.WriteLine("<tr>");
                        //sWrite.WriteLine("<td align='right'  colspan=8 ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b> Round OF :</b></font></td>");
                        //sWrite.WriteLine("<td align='right'  colspan=5 ><FONT COLOR=black FACE='Segoe UI' SIZE=3>  " + Grvsummary.Rows[rownum + 4].Cells["TOTAL"].Value.ToString() + " </font></td>");
                        //sWrite.WriteLine("</tr>");

                        //sWrite.WriteLine("<tr>");
                        //sWrite.WriteLine("<td align='right'  colspan=8 ><FONT COLOR=black FACE='Segoe UI' SIZE=2><b> Net Amount :</b></font></td>");
                        //sWrite.WriteLine("<td align='right'  colspan=5 ><FONT COLOR=black FACE='Segoe UI' SIZE=3>  " + Grvsummary.Rows[rownum + 3].Cells["TOTAL"].Value.ToString() + " </font></td>");
                        //sWrite.WriteLine("</tr>");

                        sWrite.WriteLine("</table>");
                        sWrite.WriteLine("</div>");
                        sWrite.WriteLine("<script>window.print();</script>");
                        sWrite.WriteLine("</body>");
                        sWrite.WriteLine("</html>");
                        sWrite.Close();
                        System.Diagnostics.Process.Start(Apppath + "\\patient_consultation.html");
                    }
                }
                else
                {
                    MessageBox.Show("No records found, Please change the date and try again!..", "No Records Found ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
