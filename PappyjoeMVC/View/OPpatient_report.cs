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
    public partial class OPpatient_report : Form
    {
        Daily_NewPatients_controller cntrl = new Daily_NewPatients_controller();
        public OPpatient_report()
        {
            InitializeComponent();
        }

        private void btnselect_Click(object sender, EventArgs e)
        {
            try
            {
                var dateFrom = dateTimePickerdailytreatment1.Value.ToShortDateString();
                var dateTo = dateTimePickerdailytreatment2.Value.ToShortDateString();
                if (Convert.ToDateTime(dateFrom).Date > Convert.ToDateTime(dateTo).Date)
                {
                    MessageBox.Show("From date should be less than to date", "From Date is grater ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dateTimePickerdailytreatment1.Value = DateTime.Today;
                }
                string date1 = dateTimePickerdailytreatment1.Value.ToString("yyyy-MM-dd");
                string date2 = dateTimePickerdailytreatment2.Value.ToString("yyyy-MM-dd");
                Grid_load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Grid_load()
        {
            try
            {
                dgvDailyNewPatient.Rows.Clear();
                dgvDailyNewPatient.Visible = true;
                dgvDailyNewPatient.EnableHeadersVisualStyles = false;
                dgvDailyNewPatient.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
                dgvDailyNewPatient.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                string date1 = dateTimePickerdailytreatment1.Value.ToString("yyyy-MM-dd");
                string date2 = dateTimePickerdailytreatment2.Value.ToString("yyyy-MM-dd");
                System.Data.DataTable griddailytrreatmenttable;
                griddailytrreatmenttable = this.cntrl.opPatientlist(date1, date2);
                
                int sl = 0;
                for (int z = 0; z < griddailytrreatmenttable.Rows.Count; z++)
                {
                    sl = z + 1;
                    string sl_no = sl.ToString();
                    string date = griddailytrreatmenttable.Rows[z]["date"].ToString();
                    string patient_id = griddailytrreatmenttable.Rows[z]["pt_id"].ToString();
                    string Patient_Name = griddailytrreatmenttable.Rows[z]["pt_name"].ToString();
                    string mobile = griddailytrreatmenttable.Rows[z]["primary_mobile_number"].ToString();
                    string email = griddailytrreatmenttable.Rows[z]["email_address"].ToString();
                    string doctor_name = griddailytrreatmenttable.Rows[z]["doctorname"].ToString();
                    string nationality = griddailytrreatmenttable.Rows[z]["nationality"].ToString();
                    string passportno = griddailytrreatmenttable.Rows[z]["passport_no"].ToString();
                    dgvDailyNewPatient.Columns["date"].DefaultCellStyle.Format = "dd-MM-yyyy";
                    dgvDailyNewPatient.Rows.Add(sl_no, date, patient_id, Patient_Name, mobile, email, doctor_name, nationality, passportno);
                }
               
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OPpatient_report_Load(object sender, EventArgs e)
        {
            Grid_load();
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            string checkStr = "0"; string PathName = "";
            try
            {
                if (dgvDailyNewPatient.Rows.Count != 0)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Excel Files (*.xls)|*.xls";
                    saveFileDialog1.FileName = "Daily New Patient Report(" + DateTime.Now.ToString("dd-MM-yy h.mm.ss tt") + ").xls";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        PathName = saveFileDialog1.FileName;
                        Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                        ExcelApp.Application.Workbooks.Add(Type.Missing);
                        ExcelApp.Columns.ColumnWidth = 20;
                        int count = dgvDailyNewPatient.Columns.Count;
                        ExcelApp.Range[ExcelApp.Cells[1, 1], ExcelApp.Cells[1, count]].Merge();
                        ExcelApp.Cells[1, 1] = "DAILY NEW PATIENTS";
                        ExcelApp.Cells[1, 1].HorizontalAlignment = HorizontalAlignment.Center;
                        ExcelApp.Cells[1, 1].Font.Size = 12;
                        ExcelApp.Cells[1, 1].Interior.Color = Color.FromArgb(153, 204, 255);
                        ExcelApp.Columns.ColumnWidth = 20;
                        ExcelApp.Cells[2, 1] = "From Date";
                        ExcelApp.Cells[2, 1].Font.Size = 10;
                        ExcelApp.Cells[3, 1] = "To Date";
                        ExcelApp.Cells[3, 1].Font.Size = 10;
                        ExcelApp.Cells[2, 2] = dateTimePickerdailytreatment1.Value.ToString("dd-MM-yyyy");
                        ExcelApp.Cells[2, 2].Font.Size = 10;
                        ExcelApp.Cells[3, 2] = dateTimePickerdailytreatment2.Value.ToString("dd-MM-yyyy");
                        ExcelApp.Cells[3, 2].Font.Size = 10; ExcelApp.Cells[4, 1] = "Running Date";
                        ExcelApp.Cells[4, 1].Font.Size = 10;
                        ExcelApp.Cells[4, 2] = DateTime.Now.ToString("dd-MM-yyyy");
                        ExcelApp.Cells[4, 2].Font.Size = 10;
                        ExcelApp.Cells[4, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        ExcelApp.Cells[3, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        ExcelApp.Cells[2, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        for (int i = 1; i < dgvDailyNewPatient.Columns.Count + 1; i++)
                        {
                            ExcelApp.Cells[5, i] = dgvDailyNewPatient.Columns[i - 1].HeaderText;
                            ExcelApp.Cells[5, i].ColumnWidth = 25;
                            ExcelApp.Cells[5, i].EntireRow.Font.Bold = true;
                            ExcelApp.Cells[5, i].Interior.Color = Color.FromArgb(0, 102, 204);
                            ExcelApp.Cells[5, i].Font.Size = 10;
                            ExcelApp.Cells[5, i].Font.Name = "Arial";
                            ExcelApp.Cells[5, i].Font.Color = Color.FromArgb(255, 255, 255);
                            ExcelApp.Cells[5, i].Interior.Color = Color.FromArgb(0, 102, 204);
                        }
                        for (int i = 0; i <= dgvDailyNewPatient.Rows.Count; i++)
                        {
                            try
                            {
                                for (int j = 0; j < dgvDailyNewPatient.Columns.Count; j++)
                                {
                                    ExcelApp.Cells[i + 6, j + 1] = dgvDailyNewPatient.Rows[i].Cells[j].Value.ToString();
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
                    MessageBox.Show("No records found, Please change the date and try again!..", "Failed ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                //if (flag == 1)
                //{
                //    Grid_load();
                //}
                if (dgvDailyNewPatient.Rows.Count > 0)
                {
                    string message = "Did you want Header on Print?";
                    string caption = "Verification";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons);
                    int c = 0;
                    string today = DateTime.Now.ToString("d/M/yyyy");
                    string strclinicname = "";
                    string clinicn = "";
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
                    StreamWriter sWrite = new StreamWriter(Apppath + "\\DailyNewPatientReport.html");
                    sWrite.WriteLine("<html>");
                    sWrite.WriteLine("<head>");
                    sWrite.WriteLine("<style>");
                    sWrite.WriteLine("table { border-collapse: collapse;}");
                    sWrite.WriteLine("p.big {line-height: 400%;}");
                    sWrite.WriteLine("</style>");
                    sWrite.WriteLine("</head>");
                    sWrite.WriteLine("<body >");
                    sWrite.WriteLine("<div>");
                    sWrite.WriteLine("<table align=center width =900>");
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
                    sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3> DAILY NEW PATIENT  </font></center></b></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=7 align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2> <b>From :</b>  " + dateTimePickerdailytreatment1.Value.ToString("dd/MM/yyyy") + " </font></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=7 align='left'><FONT COLOR=black FACE='Segoe UI' SIZE=2> <b>To :</b>  " + dateTimePickerdailytreatment2.Value.ToString("dd/MM/yyyy") + " </font></td>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=7 align='left'><FONT COLOR=black FACE='segoe UI' SIZE=2><b>Printed Date:</b>" + " " + today + "" + "</font></center></td>");
                    sWrite.WriteLine("</tr>");
                    if (dgvDailyNewPatient.Rows.Count > 0)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("    <td align='left' width='6%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3 >&nbsp;<b>Slno.</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='25%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;<b> Date</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b> Patient Id</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='33%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b> Patient Name</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='12%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3>&nbsp;<b> Mobile</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='23%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> &nbsp;<b>Email</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='17%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> &nbsp;<b>Doctor</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='17%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> &nbsp;<b>Nationality</b></font></th>");
                        sWrite.WriteLine("    <td align='left' width='20%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3> &nbsp;<b>Passport</b></font></th>");
                        sWrite.WriteLine("</tr>");
                        while (c < dgvDailyNewPatient.Rows.Count)
                        {
                            sWrite.WriteLine("<tr>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvDailyNewPatient.Rows[c].Cells[0].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgvDailyNewPatient.Rows[c].Cells[1].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgvDailyNewPatient.Rows[c].Cells[2].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvDailyNewPatient.Rows[c].Cells[3].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgvDailyNewPatient.Rows[c].Cells[4].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvDailyNewPatient.Rows[c].Cells[5].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgvDailyNewPatient.Rows[c].Cells[6].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgvDailyNewPatient.Rows[c].Cells[7].Value.ToString() + "</font></th>");
                            sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp; " + dgvDailyNewPatient.Rows[c].Cells[8].Value.ToString() + "</font></th>");
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
                    System.Diagnostics.Process.Start(Apppath + "\\DailyNewPatientReport.html");
                }
                else
                {
                    MessageBox.Show("No Record Found,Please change the date and try again!..", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
