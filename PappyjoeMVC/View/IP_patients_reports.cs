using PappyjoeMVC.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace PappyjoeMVC.View
{
    public partial class IP_patients_reports : Form
    {
        Ip_patient_report_controller cntrl = new Ip_patient_report_controller();
        public IP_patients_reports()
        {
            InitializeComponent();
        }

        private void IP_patients_reports_Load(object sender, EventArgs e)
        {
            dgv_ip.Rows.Clear();
            dgv_ip.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            dgv_ip.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv_ip.EnableHeadersVisualStyles = false;
            DateTime now = DateTime.Now;
            //DateTime todate = "", fromdate = "";
            dateTimePickerdailytreatment1.Value = new DateTime(now.Year, now.Month, 1);// DateTime.Today.AddDays(-1);

            DataTable dtb = this.cntrl.load_ippatient(dateTimePickerdailytreatment1.Value.ToString("yyyy-MM-dd"), dateTimePickerdailytreatment2.Value.ToString("yyyy-MM-dd"));
            DataTable dt_ip = this.cntrl.dt_ip_only(dateTimePickerdailytreatment1.Value.ToString("yyyy-MM-dd"), dateTimePickerdailytreatment2.Value.ToString("yyyy-MM-dd"));
            //if (dtb.Rows.Count>0)
            //{
                fill_grid(dtb, dt_ip);
            //}
        }

        public void fill_grid(DataTable dtb,DataTable dt_ip)
        {
            int k = 1; dgv_ip.Rows.Clear();
            if(dtb.Rows.Count>0)
            {
                for (int i = 0; i < dtb.Rows.Count; i++)
                {

                    dgv_ip.Rows.Add();
                    dgv_ip.Rows[i].Cells["slno"].Value = k;
                    dgv_ip.Rows[i].Cells["regno"].Value = dtb.Rows[i]["pt_id"].ToString();
                    dgv_ip.Rows[i].Cells["ipno"].Value = dtb.Rows[i]["ip_id"].ToString();
                    dgv_ip.Rows[i].Cells["name"].Value = dtb.Rows[i]["pt_name"].ToString();
                    dgv_ip.Rows[i].Cells["age"].Value = dtb.Rows[i]["age"].ToString();
                    dgv_ip.Rows[i].Cells["sex"].Value = dtb.Rows[i]["gender"].ToString();
                    dgv_ip.Rows[i].Cells["phone"].Value = dtb.Rows[i]["primary_mobile_number"].ToString();
                    dgv_ip.Rows[i].Cells["address"].Value = dtb.Rows[i]["street_address"].ToString();
                    dgv_ip.Rows[i].Cells["department"].Value = dtb.Rows[i]["department"].ToString();
                    dgv_ip.Rows[i].Cells["roomno"].Value = dtb.Rows[i]["Room_no"].ToString();
                    dgv_ip.Rows[i].Cells["dateofadmission"].Value = Convert.ToDateTime(dtb.Rows[i]["date"].ToString()).ToString("MM-dd-yyyy");
                dgv_ip.Rows[i].Cells["dischargedate"].Value = Convert.ToDateTime(dtb.Rows[i]["discharge_date"].ToString()).ToString("MM-dd-yyyy");
                    DateTime d1 = Convert.ToDateTime(dtb.Rows[i]["from_date"].ToString());
                    DateTime d2 = Convert.ToDateTime(dtb.Rows[i]["to_date"].ToString());
                    TimeSpan difference = d2 - d1;
                    var days = difference.TotalDays;
                    dgv_ip.Rows[i].Cells["noofdays"].Value = days;
                    k++;
                }
            }
           
            if(dt_ip.Rows.Count>0)
            {
                int p = 1;
                int count = dgv_ip.Rows.Count;
                for(int j= 0; j<dt_ip.Rows.Count;j++)
                {

                    dgv_ip.Rows.Add();
                    if (dgv_ip.Rows.Count == 0)
                    {
                        //dgv_ip.Rows[count].Cells["slno"].Value = p;
                        p =1;
                    }
                    else
                        p = count + 1;
                    dgv_ip.Rows[count].Cells["slno"].Value = p;
                    dgv_ip.Rows[count].Cells["regno"].Value = dt_ip.Rows[j]["op_id"].ToString();
                    dgv_ip.Rows[count].Cells["ipno"].Value = dt_ip.Rows[j]["pt_id"].ToString();
                    dgv_ip.Rows[count].Cells["name"].Value = dt_ip.Rows[j]["pt_name"].ToString();
                    dgv_ip.Rows[count].Cells["age"].Value = dt_ip.Rows[j]["age"].ToString();
                    dgv_ip.Rows[count].Cells["sex"].Value = dt_ip.Rows[j]["gender"].ToString();
                    dgv_ip.Rows[count].Cells["phone"].Value = dt_ip.Rows[j]["primary_mobile_number"].ToString();
                    dgv_ip.Rows[count].Cells["address"].Value = dt_ip.Rows[j]["street_address"].ToString();
                    dgv_ip.Rows[count].Cells["department"].Value = dt_ip.Rows[j]["group_id"].ToString();
                    dgv_ip.Rows[count].Cells["roomno"].Value = dt_ip.Rows[j]["Room_no"].ToString();
                    dgv_ip.Rows[count].Cells["dateofadmission"].Value =Convert.ToDateTime( dt_ip.Rows[j]["date"].ToString()).ToString("MM-dd-yyyy");
                    //dgv_ip.Rows[j].Cells["dischargedate"].Value = dtb.Rows[i]["discharge_date"].ToString();
                    DateTime d1 = Convert.ToDateTime(dt_ip.Rows[j]["date"].ToString());
                    DateTime d2 = DateTime.Now.Date;// Convert.ToDateTime(dtb.Rows[i]["to_date"].ToString());
                    TimeSpan difference = d2 - d1;
                    var days = difference.TotalDays;
                    dgv_ip.Rows[count].Cells["noofdays"].Value = days;
                    count++;
                    p++;
                }
            }
            
            
        }
        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_ip.Rows.Count > 0)
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
                    string strphone = "", logo_name = "";
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
                        }
                    }
                    string Apppath = System.IO.Directory.GetCurrentDirectory();
                    StreamWriter sWrite = new StreamWriter(Apppath + "\\ippatient.html");
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
                    sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3>IP PATIENTS REPORT </font></center></b></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=3><FONT COLOR=black FACE='Segoe UI' SIZE=2><b> From :</b> " + dateTimePickerdailytreatment1.Value.ToString("dd-MM-yyyy") + " </font></td> ");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=3><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>To : </b>" + dateTimePickerdailytreatment2.Value.ToString("dd-MM-yyyy") + " </font></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=3><FONT COLOR=black FACE='Segoe UI' SIZE=2><b>Printed Date:</b>" + " " + DateTime.Now.ToString("dd-MM-yyyy") + "" + "</font></center></td>");
                    sWrite.WriteLine("</tr>");
                    if (dgv_ip.Rows.Count > 0)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>Slno.</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>Reg No.</b></font></th>");

                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>Ip No.</b></font></th>");

                        sWrite.WriteLine("    <td align='left' width='60%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>Name</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>Age.</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>Sex.</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>Address.</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>Phone No.</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>Department.</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>Room No.</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;<b>Date of Admission.</b></font></th>");

                        sWrite.WriteLine("    <td align='left' width='30%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=2> &nbsp;<b>No of Days on Bed</b></font></th>");
                        sWrite.WriteLine("</tr>");
                        for (int j = 0; j < dgv_ip.Rows.Count; j++)
                        {
                            sWrite.WriteLine("<tr>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgv_ip.Rows[j].Cells["slno"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgv_ip.Rows[j].Cells["regno"].Value.ToString() + " </font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgv_ip.Rows[j].Cells["ipno"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgv_ip.Rows[j].Cells["name"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgv_ip.Rows[j].Cells["age"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgv_ip.Rows[j].Cells["sex"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgv_ip.Rows[j].Cells["address"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgv_ip.Rows[j].Cells["phone"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgv_ip.Rows[j].Cells["department"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgv_ip.Rows[j].Cells["roomno"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgv_ip.Rows[j].Cells["dateofadmission"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgv_ip.Rows[j].Cells["noofdays"].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("</tr>");
                        }
                        sWrite.WriteLine("</table>");
                        sWrite.WriteLine("</div>");
                        sWrite.WriteLine("<script>window.print();</script>");
                        sWrite.WriteLine("</body>");
                        sWrite.WriteLine("</html>");
                        sWrite.Close();
                        System.Diagnostics.Process.Start(Apppath + "\\ippatient.html");
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

        private void btn_Export_Click(object sender, EventArgs e)
        {
            string checkStr = "0"; string PathName = "";
            try
            {
                if (dgv_ip.Rows.Count != 0)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Excel Files (*.xls)|*.xls";
                    saveFileDialog1.FileName = "IPPatientReport(" + DateTime.Now.ToString("MM-dd-yy h.mm.ss tt") + ").xls";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        PathName = saveFileDialog1.FileName;
                        Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                        ExcelApp.Application.Workbooks.Add(Type.Missing);
                        ExcelApp.Columns.ColumnWidth = 20;
                        int count = dgv_ip.Columns.Count;
                        ExcelApp.Range[ExcelApp.Cells[1, 1], ExcelApp.Cells[1, count]].Merge();
                        ExcelApp.Cells[1, 1] = "IP PATIENTS REPORT";
                        ExcelApp.Cells[1, 1].HorizontalAlignment = HorizontalAlignment.Center;
                        ExcelApp.Cells[1, 1].Font.Size = 12;
                        ExcelApp.Cells[1, 1].Interior.Color = Color.FromArgb(153, 204, 255);
                        ExcelApp.Columns.ColumnWidth = 20;
                        ExcelApp.Cells[2, 1] = "From Date";
                        ExcelApp.Cells[2, 1].Font.Size = 10;
                        ExcelApp.Cells[3, 1] = "To Date";
                        ExcelApp.Cells[3, 1].Font.Size = 10;
                        ExcelApp.Cells[2, 2] = dateTimePickerdailytreatment1.Value.ToString("MM-dd-yyyy");
                        ExcelApp.Cells[2, 2].Font.Size = 10;
                        ExcelApp.Cells[3, 2] = dateTimePickerdailytreatment2.Value.ToString("MM-dd-yyyy");
                        ExcelApp.Cells[3, 2].Font.Size = 10;
                        ExcelApp.Cells[4, 1] = "Running Date";
                        ExcelApp.Cells[4, 1].Font.Size = 10;
                        ExcelApp.Cells[4, 2] = DateTime.Now.ToString("MM-dd-yyyy");
                        ExcelApp.Cells[4, 2].Font.Size = 10;
                        ExcelApp.Cells[4, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        ExcelApp.Cells[3, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        ExcelApp.Cells[2, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        for (int i = 1; i < dgv_ip.Columns.Count + 1; i++)
                        {
                            ExcelApp.Cells[5, i] = dgv_ip.Columns[i - 1].HeaderText;
                            ExcelApp.Cells[5, i].ColumnWidth = 25;
                            ExcelApp.Cells[5, i].EntireRow.Font.Bold = true;
                            ExcelApp.Cells[5, i].Interior.Color = Color.FromArgb(0, 102, 204);
                            ExcelApp.Cells[5, i].Font.Size = 10;
                            ExcelApp.Cells[5, i].Font.Name = "Arial";
                            ExcelApp.Cells[5, i].Font.Color = Color.FromArgb(255, 255, 255);
                            ExcelApp.Cells[5, i].Interior.Color = Color.FromArgb(0, 102, 204);
                        }
                        for (int i = 0; i <= dgv_ip.Rows.Count-1; i++)
                        {
                            try
                            {
                                for (int j = 0; j < dgv_ip.Columns.Count; j++)
                                {
                                    if(dgv_ip.Rows[i].Cells[j].Value !=null && dgv_ip.Rows[i].Cells[j].Value.ToString()!="")
                                      ExcelApp.Cells[i + 7, j + 1] = dgv_ip.Rows[i].Cells[j].Value.ToString();
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

        private void btnselect_Click(object sender, EventArgs e)
        {
            DataTable dtb = this.cntrl.load_ippatient(dateTimePickerdailytreatment1.Value.ToString("yyyy-MM-dd"), dateTimePickerdailytreatment2.Value.ToString("yyyy-MM-dd"));
            DataTable dt_ip = this.cntrl.dt_ip_only(dateTimePickerdailytreatment1.Value.ToString("yyyy-MM-dd"), dateTimePickerdailytreatment2.Value.ToString("yyyy-MM-dd"));

            if (dtb.Rows.Count > 0)
            {
                fill_grid(dtb, dt_ip);
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_ip_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
