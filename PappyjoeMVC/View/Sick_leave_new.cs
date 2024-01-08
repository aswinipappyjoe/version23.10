using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Aspose.Pdf;
using PappyjoeMVC.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;
//using Org.BouncyCastle.Asn1.Ocsp;

namespace PappyjoeMVC.View
{
    public partial class Sick_leave_new : Form
    {
        public string patient_id = "0",doctor_id="0";
        Common_model mdl = new Common_model();
        Connection db = new Connection();
        public Sick_leave_new()
        {
            InitializeComponent();
        }

        private void Sick_leave_new_Load(object sender, EventArgs e)
        {
            DataTable rs_patients = mdl.Get_Patient_Details(patient_id);
            if (rs_patients.Rows[0]["pt_name"].ToString() != "")
            {
                txt_patientname.Text = rs_patients.Rows[0]["pt_name"].ToString();//date_of_birth
            }
            if (rs_patients.Rows[0]["pt_id"].ToString() != "")
            {
                txt_fileno.Text = rs_patients.Rows[0]["pt_id"].ToString();
            }
            if (rs_patients.Rows[0]["date_of_birth"].ToString() != "")
            {
                txt_dob.Text = Convert.ToDateTime(rs_patients.Rows[0]["date_of_birth"].ToString()).ToString("dd-MMM-yyyy");
            }
            System.Data.DataTable dtdr = mdl.get_all_doctorname();// db.table("select DISTINCT id,doctor_name from tbl_doctor where login_type='doctor' or login_type ='admin' and activate_login='yes' order by doctor_name");
            cmbDoctor.DisplayMember = "doctor_name";
            cmbDoctor.ValueMember = "id";
            cmbDoctor.DataSource = dtdr;
            dateTimePicker1.Value = DateTime.Today;
            dtp_deliveredon.Value = DateTime.Today;
            dtp_inpatient_from.Value = DateTime.Today;
            dtp_inpatient_to.Value = DateTime.Today;
            dtp_mleave_from.Value = DateTime.Today;
            dtp_mleave_to.Value = DateTime.Today;
            //dtp_issuse.Value = DateTime.Today;
            //dtb_to.Value = DateTime.Today;
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            print();
        }
        public void print()
        {
            try
            {
                DateTime SelDateFrom = dtp_inpatient_from.Value;
                DateTime SelDateTo = dtp_inpatient_to.Value;
                //DateTime SelDateFrom1 = dtp_admiton.Value;
                DateTime SelDateTo1 = dtp_deliveredon.Value;
                DateTime SelDateFrom2 = dtp_mleave_from.Value;
                DateTime SelDateTo2 = dtp_mleave_to.Value;
                //if (SelDateTo >= SelDateFrom && SelDateTo1 >= SelDateFrom1 && SelDateTo2 >= SelDateFrom2)
                //{
                string strclinicname = "";
                string logo_name = "";
                string str_address = "";
                string str_contact = "";
                System.Data.DataTable dtp = mdl.Get_Practice_details();// db.table("select name,contact_no,path,street_address,contact_no from tbl_practice_details");
                if (dtp.Rows.Count > 0)
                {
                    string clinicn = "";
                    clinicn = dtp.Rows[0]["Name"].ToString();
                    strclinicname = clinicn.Replace("¤", "'");
                    str_address = dtp.Rows[0]["street_address"].ToString();
                    str_contact = dtp.Rows[0]["contact_no"].ToString();
                    string path = dtp.Rows[0]["path"].ToString();
                    logo_name = path;

                    double Days = (dtp_mleave_to.Value - dtp_mleave_from.Value).TotalDays;
                    txt_days.Text = (Days+1).ToString();

                }
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                System.IO.StreamWriter sWrite = new StreamWriter(Apppath + "\\new_sickLeave.html");
                sWrite.WriteLine("<html>");
                sWrite.WriteLine("<head>");
                sWrite.WriteLine("<style>");
                sWrite.WriteLine("<table, th, td {border: 1px }>");

                sWrite.WriteLine("p.big {line-height: 400%;}");
                sWrite.WriteLine("</style>");
                sWrite.WriteLine("</head>");
                sWrite.WriteLine("<body >");
                sWrite.WriteLine("<br>");
                if (logo_name != "")
                {
                    string Appath = System.IO.Directory.GetCurrentDirectory();
                    if (File.Exists(Appath + "\\" + logo_name))
                    {
                        sWrite.WriteLine("<table align='center' style='width:500px;border: 1px ;border-collapse: collapse;'>");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td width='30px' height='50px' align='left' rowspan='2'><img src='" + Appath + "\\" + logo_name + "' style='width:70px;height:70px;></td>  ");
                        sWrite.WriteLine("<td width='770px' align='right' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=4><b>&nbsp;" + strclinicname.ToString() + "</font> <br><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + str_address.ToString() + "<br>&nbsp;" + str_contact.ToString() + " </b></td></tr>");
                        sWrite.WriteLine("</tr>");
                        sWrite.WriteLine("</table>");
                    }
                    else
                    {
                        sWrite.WriteLine("<table align='center' style='width:500px;border: 1px ;border-collapse: collapse;'>");
                        sWrite.WriteLine("<tr>");
                        sWrite.WriteLine("<td  align='center' height='25px'><FONT  color=black  face='Segoe UI' SIZE=5>" + strclinicname + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td  align='center' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>" + str_address + "</font></td></tr>");
                        sWrite.WriteLine("<tr><td align='center' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>" + str_contact + "</font></td></tr>");
                        // sWrite.WriteLine("<tr><td align='center' colspan='1'><hr/></td></tr>");
                        sWrite.WriteLine("</table>");
                    }
                }
                else
                {

                    sWrite.WriteLine("<table align='center' style='width:500px;border: 1px ;border-collapse: collapse;'>");
                    sWrite.WriteLine("<tr><th align='center'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=4></font></th></tr>");
                    sWrite.WriteLine("<tr><th align='center'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=3></font></th></tr>");
                    sWrite.WriteLine("<tr><th align='center'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=3></font></th></tr>");
                    sWrite.WriteLine("</table>");
                }

                sWrite.WriteLine("<table align=center> ");
                sWrite.WriteLine("<col width=500>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<th align=center><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=5>Sick Leave  Certificate </font></th>");

                sWrite.WriteLine("</tr>");
                //sWrite.WriteLine("<tr>");
                //sWrite.WriteLine("<td colspan='1' align=left><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=2>Date of Issuse :" + dtp_issuse.Value.ToString("dd-MMM-yyyy") + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;REF NO :" + textBox1.Text + "</font></th>");
                ////sWrite.WriteLine("<td colspan='1' align=left><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=1>REF NO :" + textBox1.Text + "</font></th>");
                //sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("<br>");
                sWrite.WriteLine("<br>");
                sWrite.WriteLine("<table align='center' style='width:800px;border: 1px ;border-collapse: collapse;'>");
                sWrite.WriteLine("<col >");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td colspan='1' align=left><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=2>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Date of Issuse :" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "</FONT>  </td>");
                sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=2>REF NO : " + textBox1.Text + "</font></td>");
                //sWrite.WriteLine("<td colspan='1' align=left><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=2>Date of Issuse :" + dtp_issuse.Value.ToString("dd-MMM-yyyy") + "</font><align=right><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=2>REF NO :" + textBox1.Text + "</FONT>  </td>");
                //sWrite.WriteLine("<td colspan='1' align=left><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=1>REF NO :" + textBox1.Text + "</font></th>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");



                sWrite.WriteLine("<div>");
                sWrite.WriteLine("<table align='center' style='width:800px;border: 1px ;border-collapse: collapse;'>");

                sWrite.WriteLine("<col >");
                sWrite.WriteLine("<br>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("    <td align='center' width='200px' Height='10px' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3 >&nbsp;<b>Name Of the Patient</b></font></td>");

                sWrite.WriteLine("    <td align='center' width='200px' Height='10px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3><b>Designation / Place of Work</b></font></td>");
                sWrite.WriteLine("    <td align='center' width='200px' Height='10px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3><b>File No</b></font></td>");
                sWrite.WriteLine("    <td align='center' width='200px' Height='10px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>&nbsp;<b>Resident Card</b></font></td>");
                sWrite.WriteLine("    <td align='center' width='200px' Height='10px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>&nbsp;<b>DOB Card</b></font></td>");
                sWrite.WriteLine("</tr>");

                sWrite.WriteLine("<tr>");

                sWrite.WriteLine("    <td align='center' width='200px' Height='10px' style='border:1px solid #000' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + txt_patientname.Text + "</font></td>");
                sWrite.WriteLine("    <td align='center' width='200px' Height='10px' style='border:1px solid #000' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + txt_designation.Text + "</font></td>");
                sWrite.WriteLine("    <td align='center' width='200px' Height='10px' style='border:1px solid #000' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + txt_fileno.Text + "</font></td>");
                sWrite.WriteLine("    <td align='center' width='200px' Height='10px' style='border:1px solid #000' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + txt_residentcard.Text + "</font></td>");
                sWrite.WriteLine("    <td align='center' width='200px' Height='10px' style='border:1px solid #000' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + txt_dob.Text + "</font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align=right colspan=7><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3 ><br><br><b> </b>&nbsp;&nbsp;  </font> </td> ");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");
                sWrite.WriteLine("</div>");

                sWrite.WriteLine("<table align='center' style='width:740px;border: 1px  ;border-collapse: collapse;'>");
                sWrite.WriteLine("<td align='left'><p style='width:740px;line-height:3;><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=7 >");
                sWrite.WriteLine("This is To certify that the above mentioned person was examined/treated by me and found to suffer from " + "<b><u>" + txt_procedure.Text + "</u></b> " + "  " + "" + "as an Out Patient on" + "  " + "<b><u>" + dtp_deliveredon.Value.ToString("dd/MM/yyyy") + "</u></b>." + "  " + "" + "and an inpatient from" + " &nbsp; " + "<u><b>" + dtp_inpatient_from.Value.ToString("dd/MM/yyyy") + "</u></b>" + " &nbsp; " + "  to   " + "<u><b>" + dtp_inpatient_to.Value.ToString("dd/MM/yyyy") + "</u></b>." + "&nbsp;" + "<br>He/She is authorized sick leave from " + " &nbsp;" + "<b><u>" + dtp_mleave_from.Value.ToString("dd/MM/yyyy") + "</b></u>" + " &nbsp; to &nbsp;" + " <b><u>" + dtp_mleave_to.Value.ToString("dd/MM/yyyy") + "</b></u> " + "&nbsp;for" + " &nbsp; " + "<b><u>" + txt_days.Text + "</b></u>" + "  " + "&nbsp;days." + "  " + "<br><br><b>Remarks:</b>" + txt_remarks.Text + "<br> To be countersigned by Consultant if sick leave exceeds  " + textBox4.Text + " days <br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;This certificate is not valid without doctor's signature and stamp of Clinic/Polyclinic/Hospital<br><br>"); ;

                sWrite.WriteLine("</font> </p></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=3>Patient Signature</font></td>");
                sWrite.WriteLine("<col width=800px>");
                sWrite.WriteLine("<td align='right' ><FONT  COLOR=black FACE='Geneva, segoe UI' SIZE=3, > Doctor's Signature with Seal  </font></td>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("<tr>");
                sWrite.WriteLine("<td align='left' ><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=3>........................... </font></td>");
                sWrite.WriteLine("<col width=270>");
                sWrite.WriteLine("<td align='right' ><FONT COLOR=black font_align='center' FACE='Geneva, segoe UI' SIZE=3><b>" + cmbDoctor.Text + " </b></font></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine(" <tr>");
                sWrite.WriteLine("<td align='right' ><FONT COLOR=black font_align='center' FACE='Geneva, segoe UI' SIZE=3><br>...........................  </font></td>");
                sWrite.WriteLine(" </tr>");

                sWrite.WriteLine(" <tr>");
                sWrite.WriteLine("<td align=center><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=2><br><br>Countersigned by Directorate of Private Health Establishment, Affairs </font></td>");
                sWrite.WriteLine(" </tr>");
                sWrite.WriteLine("</table>");

                sWrite.WriteLine("<script>window.print();</script>");
                sWrite.WriteLine("</body>");
                sWrite.WriteLine("</html>");
                sWrite.Close();
                System.Diagnostics.Process.Start(Apppath + "\\new_sickLeave.html");
                //string html = "new_sickLeave.html";
                //string folderPath = "E:\\PDFs\\";
                //if (!Directory.Exists(folderPath))
                //{
                //    Directory.CreateDirectory(folderPath);
                //}

                ////Exporting HTML to PDF file.
                //using (FileStream stream = new FileStream(folderPath + "sickleave.pdf", FileMode.Create))
                //{
                //    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                //    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                //    pdfDoc.Open();
                //    StringReader sr = new StringReader("new_sickLeave.html");

                //    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                //    pdfDoc.Close();
                //    stream.Close();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            string server = db.server();
            string realfile = "";
            var url = @"\\" + server + "\\Pappyjoe_utilities\\Attachments\\" + realfile;// https://stackoverflow.com/questions/564650/convert-html-to-pdf-in-net";

            //Document doc = new Document();
            //PdfWriter.GetInstance(doc, new FileStream("E://createpdf.pdf", FileMode.Create));
            //doc.Open();
            //Paragraph pl = new Paragraph("");
            //doc.Add(pl);
            //doc.Close();
            //MessageBox.Show("successfully created");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string strclinicname = "";
            string logo_name = "";
            string str_address = "";
            string str_contact = "";
            System.Data.DataTable dtp = mdl.Get_Practice_details();// db.table("select name,contact_no,path,street_address,contact_no from tbl_practice_details");
            if (dtp.Rows.Count > 0)
            {
                string clinicn = "";
                clinicn = dtp.Rows[0]["Name"].ToString();
                strclinicname = clinicn.Replace("¤", "'");
                str_address = dtp.Rows[0]["street_address"].ToString();
                str_contact = dtp.Rows[0]["contact_no"].ToString();
                string path = dtp.Rows[0]["path"].ToString();
                logo_name = path;

                double Days = (dtp_mleave_to.Value - dtp_mleave_from.Value).TotalDays;
                txt_days.Text =(Days+1).ToString();
            }
            string html=("<html>");

            html+=("<head>");
            //html += ("<style>");
            //html += ("<table, th, td {border: 1px }>");

            //html += ("p.big {line-height: 400%;}");
            //html += ("</table>");
            //html += ("</style>");
            html += ("</head>");
            html += ("<body >");
            //html += ("<br>");
            //html += ("</br>");
            if (logo_name != "")
            {
                string Appath = System.IO.Directory.GetCurrentDirectory();
                //if (File.Exists(Appath + "\\" + logo_name))
                //{
                //    //html += ("<table align='center' style='width:500px;border: 1px ;border-collapse: collapse;'>");
                //    //html += ("<tr>");
                //    //html += ("<td width='30px' height='50px' align='left' rowspan='3' > <img src='" + Appath + "\\" + logo_name + "'style='width:70px;height:70px; > </td>");
                //    ////html += ("<td width='770px' align='right' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=4 ><b>&nbsp;" + strclinicname.ToString() + "</b></font> <br> </br><FONT COLOR=black FACE='Segoe UI' SIZE=2 >&nbsp;" + str_address.ToString() + "<br> </br>&nbsp;" + str_contact.ToString() + " </font></td>");
                //    //html += ("</tr>");
                //    //html += ("</table>");
                //    html += ("<table align='center' style='width:500px;border: 1px ;border-collapse: collapse;'>");
                //    html += ("<tr>");
                //    html += ("<td width='30px' height='50px' align='left' rowspan='3'><img src='" + Appath + "\\" + logo_name + "' style='width:70px;height:70px;></td>  ");
                //    html += ("<td width='770px' align='right' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=4><b>&nbsp;" + strclinicname.ToString() + "</b></font> <br></br><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + str_address.ToString() + "<br></br><b>&nbsp;" + str_contact.ToString() + " </b></font></td></tr>");
                //    html += ("</tr>");
                //    html += ("</table>");

                //}
                //else
                //{
                    html += ("<table align='center' style='width:500px;border: 0px ;border-collapse: collapse;'>");
                    html += ("<tr>");
                    html += ("<td  align='center' height='25px'><FONT  color=black  face='Segoe UI' SIZE=5 >" + strclinicname + "</font></td></tr>");
                    html += ("<tr><td  align='center' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3 >" + str_address + "</font></td></tr>");
                    html += ("<tr><td align='center' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2 >" + str_contact + "</font></td></tr>");
                    // sWrite.WriteLine("<tr><td align='center' colspan='1'><hr/></td></tr>");
                    html += ("</table>");

             
                //}
            }
            else
            {

                html += ("<table align='center' style='width:500px;border: 1px ;border-collapse: collapse;'>");
                html += ("<tr><th align='center'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=4 ></font></th></tr>");
                html += ("<tr><th align='center'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=3 ></font></th></tr>");
                html += ("<tr><th align='center'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=3 ></font></th></tr>");
                html += ("</table>");
            }

            html += ("<table align=center> ");
            html += ("<col width=500>");
            html += ("<tr>");
            html += ("<th align='center'><FONT COLOR=black FACE='Geneva, segoe UI'SIZE=6 ><b><u> Sick Leave  Certificate </u></b></font></th>");

            html += ("</tr>");
            html += ("</col >");
            html += ("</table>");
            html += ("<br>");
            html += ("</br>");
            html += ("<br>");
            html += ("</br>");
            html += ("<table align='center' style='width:800px;border: 0px ;border-collapse: collapse;'>");
            html += ("<col >");
            html += ("<tr>");
            html += ("<td colspan='1' align='left'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=3 >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Date of Issuse :" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "</FONT></td>");
            html += ("<td align='right' ><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=3 > REF NO : " + textBox1.Text + "</font></td>");
            html += ("</tr>");
            html += ("<tr>");
            html += ("</tr>");
            html += ("</col >");
            html += ("</table>");
            html += ("<div>");
            html += ("<table align='center' style='width:800px;border: 1px ;border-collapse: collapse;'>");

            html += ("<col >");
            html += ("<br>");
            html += ("</br>");
            html += ("<tr>");
            html += ("<td align='center' width='200px' Height='10px' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=4 >&nbsp;<b>Name Of the Patient </b></font></td>");

            html += ("<td align='center' width='200px' Height='10px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=4 ><b> Designation / Place of Work </b></font></td>");
            html += (" <td align='center' width='200px' Height='10px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=4 ><b>File No </b></font></td>");
            html += ("    <td align='center' width='200px' Height='10px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=4 >&nbsp;<b>Resident Card</b></font></td>");
            html += ("    <td align='center' width='200px' Height='10px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=4 >&nbsp;<b>DOB Card</b></font></td>");
            html += ("</tr>");

            html += ("<tr>");

            html += ("    <td align='center' width='200px' Height='25px' style='border:1px solid #000' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3 >&nbsp;" + txt_patientname.Text + "</font></td>");
            html += ("    <td align='center' width='200px' Height='25px' style='border:1px solid #000' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3 >&nbsp;" + txt_designation.Text + "</font></td>");
            html += ("    <td align='center' width='200px' Height='25px' style='border:1px solid #000' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3 >&nbsp;" + txt_fileno.Text + "</font></td>");
            html += ("    <td align='center' width='200px' Height='25px' style='border:1px solid #000' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3 >&nbsp;" + txt_residentcard.Text + "</font></td>");
            html += ("    <td align='center' width='200px' Height='25px' style='border:1px solid #000' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3 >&nbsp;" + txt_dob.Text + "</font></td>");
            html += ("</tr>");
            html += ("<tr>");
            //html += ("<td align=right colspan=7><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3 >&nbsp;&nbsp;</font> </td> ");
            html += ("</tr>");
            html += ("</col >");
            html += ("</table>");
            html += ("<br>");
            html += ("</br>");
            html += ("<br>");
            html += ("</br>");
            html += ("</div>");
            html += ("<div>");
            html += ("<table align='center' style='width:800px;border: 0px  ;border-collapse: collapse;'>");
            //html += ("<p class='big'>");
            html += ("<tr>");
            html += ("<td  ><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=4 >");
            html += ("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;This is To certify that the above mentioned person was examined/treated by me and found to suffer from " + "<b><u>" + txt_procedure.Text + "</u></b> " + "  " + "" + "<br></br> <br></br>as an Out Patient on" + "  " + "<b><u>" + dtp_deliveredon.Value.ToString("dd/MM/yyyy") + "</u></b>." + "  " + "" + "and an inpatient from" + " &nbsp; " + "<b><u>" + dtp_inpatient_from.Value.ToString("dd/MM/yyyy") + "</u></b>" + " &nbsp; " + "  to   " + "<b><u>" + dtp_inpatient_to.Value.ToString("dd/MM/yyyy") + "</u></b>." + "&nbsp;" + "<br></br><br></br>&nbsp;&nbsp;&nbsp; He/She is authorized sick leave from " + " &nbsp;" + "<b><u>" + dtp_mleave_from.Value.ToString("dd/MM/yyyy") + "</u></b>" + " &nbsp; to &nbsp;" + " <b><u>" + dtp_mleave_to.Value.ToString("dd/MM/yyyy") + "</u></b> " + "&nbsp;for" + " &nbsp; " + "<b><u>" + txt_days.Text + "</u></b>" + "  " + "&nbsp;days." + "  " + "<br></br> <br></br> Remarks:" + txt_remarks.Text + "<br></br> <br></br> To be countersigned by Consultant if sick leave exceeds  " + textBox4.Text + " days<br></br> <br></br> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;This certificate is not valid without doctor's signature and stamp of Clinic/Polyclinic/Hospital <br></br> <br></br> ");

            html += ("</font></td>");
            html += ("</tr>");
            //html += ("</p>");
            html += ("<tr>");
            html += ("<td align='left' ><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=3 >Patient Signature</font></td>");
            //html += ("<col width=800px>");
            html += ("<td align='right' ><FONT  COLOR=black FACE='Geneva, segoe UI' SIZE=3 >&nbsp;&nbsp;Doctor's Signature with Seal</font></td>");
            //html += ("</col >");
            html += ("</tr>");
            html += ("<tr>");
            //html += ("<td align='left' ><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=1 >........................... </font></td>");
            //html += ("<col width=270>");
            html += ("<td align='right' ><FONT COLOR=black font_align='center' FACE='Geneva, segoe UI' SIZE=3 ><b>" + cmbDoctor.Text + " </b></font></td>");
            //html += ("</col >");
            html += (" </tr>");
            html += (" <tr>");
            //html += ("<td align='right' ><FONT COLOR=black font_align='center' FACE='Geneva, segoe UI' SIZE=1 ><b></b>...........................  </font></td>");
            html += (" </tr>");

            html += (" <tr>");
            html += ("<td align='center'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=2 >Countersigned by Directorate of Private Health Establishment, Affairs </font></td>");
            html += (" </tr>");
            html += ("</table>");
            html += ("</div>");


            html +=("</body>");
            html += ("</html>");

            //string folderPath = "E:\\PDFs\\";
            //if (!Directory.Exists(folderPath))
            //{
            //    Directory.CreateDirectory(folderPath);
            //}
            string name = "Sickleave" + patient_id + ".pdf";
            string server = db.server();
            string realfile = @"\\" + server + "\\Pappyjoe_utilities\\Attachments\\";// System.IO.Path.GetFileName(name);
            //if (File.Exists(@"\\" + server + "\\Pappyjoe_utilities\\Attachments\\" + realfile))
            //{
            //}
            //else
            //{
            //    System.IO.File.Copy(filename, @"\\" + server + "\\Pappyjoe_utilities\\Attachments\\" + realfile);
            //}

            //Exporting HTML to PDF file.
            using (FileStream stream = new FileStream(realfile + name, FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                StringReader sr = new StringReader(html);
                
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                stream.Close();
            }
            string pathimage = "\\" + "\\Pappyjoe_utilities" + "\\" + "\\Attachments\\" + "\\" +name;
            //txt_path3.Text = pathimage;
           mdl.insattach(patient_id, name, pathimage, cmbDoctor.Text, "Certificate");
            MessageBox.Show("Pdf successfully saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
