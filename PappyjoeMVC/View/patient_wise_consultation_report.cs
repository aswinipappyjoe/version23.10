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
    public partial class patient_wise_consultation_report : Form
    {
        patient_consultation_report_controller cntrl = new patient_consultation_report_controller();
        public string patient_id = ""; //string logo_name = "";
        public patient_wise_consultation_report()
        {
            InitializeComponent();
        }

        private void patient_wise_consultation_report_Load(object sender, EventArgs e)
        {
            dgvPatient.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            dgvPatient.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPatient.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 8, FontStyle.Regular);
            dgvPatient.EnableHeadersVisualStyles = false;
            dgvPatient.Location = new System.Drawing.Point(5, 5);
            foreach (DataGridViewColumn cl in dgvPatient.Columns)
            {
                cl.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
           
        }

        private void txt_Pt_search_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && lbPatient.Items.Count > 0)
            {
                lbPatient.Focus();
            }
            else if (e.KeyCode == Keys.Enter && lbPatient.Items.Count > 0)
            {
                if (lbPatient.SelectedItems.Count > 0)
                {
                    string value = lbPatient.SelectedValue.ToString();
                    DataTable patient = new DataTable();
                    patient = this.cntrl.get_patients(value);
                    if (patient.Rows.Count > 0)
                    {
                        txt_Pt_search.Text = patient.Rows[0]["pt_name"].ToString();
                        txtPatientID.Text = patient.Rows[0]["pt_id"].ToString();
                        patient_id = patient.Rows[0]["id"].ToString();
                        txt_gender.Text = patient.Rows[0]["gender"].ToString();
                        txt_age.Text= patient.Rows[0]["age"].ToString();
                        txt_mobile.Text= patient.Rows[0]["primary_mobile_number"].ToString();
                        txt_place.Text= patient.Rows[0]["street_address"].ToString();
                       
                        lbPatient.Visible = false;
                        txt_Pt_search.Focus();
                    }
                }
            }
        }

        private void txt_Pt_search_TextChanged(object sender, EventArgs e)
        {
            //if (flag == false)
            //{
                if (txt_Pt_search.Text != "")
                {
                    // lbPatient.Show();
                    lbPatient.Location = new Point(99, 35);// txt_Pt_search.Location.X, 110);
                    DataTable dtdr = this.cntrl.srch_patient(txt_Pt_search.Text);//srch_patient(txt_Pt_search.Text, txt_Pt_search.Text);
                    lbPatient.DataSource = dtdr;
                    lbPatient.DisplayMember = "patient";
                    lbPatient.ValueMember = "id";

                }
                else
                {
                    DataTable dtdr = this.cntrl.srch_patient(txt_Pt_search.Text);
                    lbPatient.DataSource = dtdr;
                    lbPatient.DisplayMember = "patient";
                    lbPatient.ValueMember = "id";
                }
                if (lbPatient.Items.Count > 0)
                {
                    lbPatient.Show();
                }
                else
                {
                    lbPatient.Hide();
                }
            //}
        }

        private void lbPatient_MouseClick(object sender, MouseEventArgs e)
        {
            if (lbPatient.SelectedItems.Count > 0)
            {
                DateTime paid_date, todate; int f_days = 0; double days = 0;
                string value = lbPatient.SelectedValue.ToString();
                DataTable patient = new DataTable();
                patient = this.cntrl.get_patients(value);
                if (patient.Rows.Count > 0)
                {
                    txt_Pt_search.Text = patient.Rows[0]["pt_name"].ToString();
                    txtPatientID.Text = patient.Rows[0]["pt_id"].ToString();
                    patient_id = patient.Rows[0]["id"].ToString();
                    txt_gender.Text = patient.Rows[0]["gender"].ToString();
                    txt_age.Text = patient.Rows[0]["age"].ToString();
                    txt_mobile.Text = patient.Rows[0]["primary_mobile_number"].ToString();
                    txt_place.Text = patient.Rows[0]["street_address"].ToString();
                    load_grid();
                    lbPatient.Visible = false;
                    txt_Pt_search.Focus();
                }
                else
                {
                    MessageBox.Show("Please choose  Correct patient....", "Data Not Found..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txt_Pt_search_Click(object sender, EventArgs e)
        {
            if (txt_Pt_search.Text == "Search by Patient Name")
            {
                txt_Pt_search.Text = "";
            }
        }
   
        public void load_grid()
        {
            DataTable dtb = this.cntrl.patient_wise_consultation(patient_id);
            if(dtb.Rows.Count>0)
            {
                decimal total = 0;
                int k = 1;
                dgvPatient.Rows.Clear();
                for(int i=0;i< dtb.Rows.Count;i++)
                {
                    dgvPatient.Rows.Add();
                    dgvPatient.Rows[i].Cells["sl_no"].Value = k;
                    dgvPatient.Rows[i].Cells["Doctor"].Value = dtb.Rows[i]["doctorname"].ToString();
                    dgvPatient.Rows[i].Cells["date"].Value = dtb.Rows[i]["visited_date"].ToString();
                    dgvPatient.Rows[i].Cells["fee"].Value =Convert.ToDecimal( dtb.Rows[i]["fee"].ToString()).ToString("0.00");
                    dgvPatient.Rows[i].Cells["status"].Value = dtb.Rows[i]["payment_status"].ToString();
                    total = total + Convert.ToDecimal(dtb.Rows[i]["fee"].ToString());
                    k++;
                    //dgvPatient.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    //dgvPatient.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                }
                dgvPatient.Rows.Add("","","","TOTAL :", total.ToString("0.00"));
            }
            else
            {

            }
        }
        public string dateFrom = "", dateTo = "", checkStr = "", PathName = "", strclinicname = "", clinicn = "", strStreet = "", stremail = "", strwebsite = "", strphone = "", logo_name = "";

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            //printhtml();
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

                    System.Data.DataTable dt1 = this.cntrl.Get_Patient_Details(patient_id);
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
                    sWrite.WriteLine("<th colspan=11><center><b><FONT COLOR=black FACE='Segoe UI'  SIZE=3> PATIENT WISE CONSULTATION REPOET  </font></center></b></td>");
                    sWrite.WriteLine("</tr>");
                    string sexage = "";
                    int Dexist = 0;
                    string address = "";
                    //sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr>");
                    if (dt1.Rows[0]["gender"].ToString() != "")
                    {
                        sexage = dt1.Rows[0]["gender"].ToString();
                        Dexist = 1;
                    }
                    if (dt1.Rows[0]["age"].ToString() != "")
                    {
                        if (Dexist == 1)
                        {
                            sexage = sexage + ", " + dt1.Rows[0]["age"].ToString() + " Years";
                        }
                        else
                        {
                            sexage = dt1.Rows[0]["age"].ToString() + " Years";
                        }
                    }
                    sWrite.WriteLine(" <td align='left' height=25><FONT COLOR=black FACE='Geneva, Arial' SIZE=2><b>" + dt1.Rows[0]["pt_name"].ToString() + "</b><i> (" + sexage + ")</i></font></td>");
                    sWrite.WriteLine(" </tr>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>Patient Id:&nbsp;" + dt1.Rows[0]["pt_id"].ToString() + " </font></td>");
                    sWrite.WriteLine(" </tr>");
                    Dexist = 0;
                    if (dt1.Rows[0]["street_address"].ToString() != "")
                    {
                        address = dt1.Rows[0]["street_address"].ToString();
                        Dexist = 1;
                    }
                    if (dt1.Rows[0]["locality"].ToString() != "")
                    {
                        if (Dexist == 1)
                        {
                            address = address + ",";
                        }
                        address = address + dt1.Rows[0]["locality"].ToString();
                        Dexist = 1;
                    }
                    if (dt1.Rows[0]["city"].ToString() != "")
                    {
                        if (Dexist == 1)
                        {
                            address = address + ",";
                        }
                        address = address + dt1.Rows[0]["city"].ToString();
                        Dexist = 1;
                    }
                    if (dt1.Rows[0]["pincode"].ToString() != "")
                    {
                        if (Dexist == 1)
                        {
                            address = address + ",";
                        }
                        address = address + dt1.Rows[0]["pincode"].ToString();
                        Dexist = 1;
                    }
                    if (address != "")
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>" + address + " </font></td>");
                        sWrite.WriteLine(" </tr>");
                    }

                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, Arial' SIZE=2>" + dt1.Rows[0]["primary_mobile_number"].ToString() + " </font></td>");
                    sWrite.WriteLine(" </tr>");

                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td colspan=7 align='left' colspan=7><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2><b>Printed Date: </b>" + today + "</font></td>");
                    sWrite.WriteLine("</tr>");
                    int rownum = 0;
                    if (dgvPatient.Rows.Count > 0)
                    {
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("    <td align='left' width='7%' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3 ><b>&nbsp;SLNO.</b></font></td>");
                        //sWrite.WriteLine("    <td align='left' width='14%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;PATIENT ID</b></font></td>");
                        //sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;PATIENT NAME</b></font></td>");
                        //sWrite.WriteLine("    <td align='left' width='7%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;GENDER</b></font></td>");
                        //sWrite.WriteLine("    <td align='left' width='15%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;AGE</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;DOCTOR</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp; VISITED DATE</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='10%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;Status</b></font></td>");
                        //sWrite.WriteLine("    <td align='left' width='18%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;FOLLOWUP FEE</b></font></td>");
                        sWrite.WriteLine("    <td align='left' width='18%' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Segoe UI' SIZE=3><b>&nbsp;AMOUNT PAID</b></font></td>");
                        sWrite.WriteLine("</tr>");
                        for (int c = 0; c < dgvPatient.Rows.Count; c++)
                        {
                            if (dgvPatient.Rows[c].Cells["SL_NO"].Value.ToString() != "")
                            {
                                sWrite.WriteLine("<tr>");
                                sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvPatient.Rows[c].Cells["sl_no"].Value.ToString() + "</font></td>");
                                //sWrite.WriteLine("    <td align='left' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvPatient.Rows[c].Cells["patient_id"].Value.ToString() + "</font></td>");
                                //sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvPatient.Rows[c].Cells["patient_name"].Value.ToString() + "</font></td>");
                                //sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvPatient.Rows[c].Cells["Sex"].Value.ToString() + "</font></td>");
                                //sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvPatient.Rows[c].Cells["age"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvPatient.Rows[c].Cells["doctor"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvPatient.Rows[c].Cells["date"].Value.ToString() + "</font></td>");
                                //sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvPatient.Rows[c].Cells["fee"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvPatient.Rows[c].Cells["status"].Value.ToString() + "</font></td>");
                                sWrite.WriteLine("    <td align='right' style='border:1px solid #000' ><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + dgvPatient.Rows[c].Cells["fee"].Value.ToString() + "</font></td>");
                                rownum = c;
                            }
                        }
                        sWrite.WriteLine("</tr>");
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
       
        private void btn_Export_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPatient.Rows.Count != 0)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Excel Files (*.xls)|*.xls";
                    saveFileDialog1.FileName = "Patient Wise Consultation Report(" + DateTime.Now.ToString("dd-MM-yy h.mm.ss tt") + ").xls";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        PathName = saveFileDialog1.FileName;
                        Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                        ExcelApp.Application.Workbooks.Add(Type.Missing);
                        ExcelApp.Columns.ColumnWidth = 20;
                        int count = dgvPatient.Columns.Count;
                        ExcelApp.Range[ExcelApp.Cells[1, 1], ExcelApp.Cells[1, count]].Merge();
                        ExcelApp.Cells[1, 1] = "PATIENT WISE CONSULTATION REPORT";
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
    }
}
