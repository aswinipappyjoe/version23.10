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
//using PappyjoeMVC.Model;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;
using PappyjoeMVC.Model;
namespace PappyjoeMVC.View
{
    public partial class Attendant_Certificate : Form
    {
        public string patient_id = "0";
        Common_model mdl = new Common_model();
        Connection db = new Connection();
        public Attendant_Certificate()
        {
            InitializeComponent();
        }

        private void Attendant_Certificate_Load(object sender, EventArgs e)
        {
            DataTable rs_patients = mdl.Get_Patient_Details(patient_id);
            if (rs_patients.Rows[0]["pt_name"].ToString() != "")
            {
                txt_patientname.Text = rs_patients.Rows[0]["pt_name"].ToString();
            }
            if (rs_patients.Rows[0]["pt_id"].ToString() != "")
            {
                txt_fileno.Text = rs_patients.Rows[0]["pt_id"].ToString();
            }
            System.Data.DataTable dtdr = mdl.get_all_doctorname();// db.table("select DISTINCT id,doctor_name from tbl_doctor where login_type='doctor' or login_type ='admin' and activate_login='yes' order by doctor_name");
            cmbDoctor.DisplayMember = "doctor_name";
            cmbDoctor.ValueMember = "id";
            cmbDoctor.DataSource = dtdr;
            dtp_issuse.Value = DateTime.Today;
            dtb_to.Value = DateTime.Today;
            //dtp_mleave_to.Value = DateTime.Today;
            //dtp_mleave_from.Value = DateTime.Today;
            fromdate();
        }
        private void fromdate()
        {
            System.Data.DataTable dt7 = mdl.get_patient_date(patient_id);
            string aa = dt7.Rows[0]["date"].ToString();
            if (dt7.Rows[0]["date"].ToString() != "")
            {
                dtb_admited.Show();
                dtb_admited.Value = DateTime.Parse(DateTime.Parse(dt7.Rows[0]["date"].ToString()).ToString());
                dtb_from.Show();
                dtb_from.Value = DateTime.Parse(DateTime.Parse(dt7.Rows[0]["date"].ToString()).ToString());
                //dtp_mleave_from.Show();
                //dtp_mleave_from.Value = DateTime.Parse(DateTime.Parse(dt7.Rows[0]["date"].ToString()).ToString());
                //dtp_mleave_to.Show();
                //dtp_mleave_to.Value = DateTime.Parse(DateTime.Parse(dt7.Rows[0]["date"].ToString()).ToString());
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            print();
        }
        //public void print()
        //{
        //    try
        //    {
        //        DateTime SelDateFrom = dtb_from.Value;
        //        DateTime SelDateTo = dtb_to.Value;
        //        if (SelDateTo >= SelDateFrom)
        //        {
        //            string strclinicname = "";
        //            string logo_name = "";
        //            string str_address = "";
        //            string str_contact = "";
        //            System.Data.DataTable dtp = mdl.Get_Practice_details();// db.table("select name,contact_no,path,street_address,contact_no from tbl_practice_details");
        //            if (dtp.Rows.Count > 0)
        //            {
        //                string clinicn = "";
        //                clinicn = dtp.Rows[0]["Name"].ToString();
        //                strclinicname = clinicn.Replace("¤", "'");
        //                str_address = dtp.Rows[0]["street_address"].ToString();
        //                str_contact = dtp.Rows[0]["contact_no"].ToString();
        //                string path = dtp.Rows[0]["path"].ToString();
        //                logo_name = path;
        //            }
        //            string Apppath = System.IO.Directory.GetCurrentDirectory();
        //            StreamWriter sWrite = new StreamWriter(Apppath + "\\Attendant.html");
        //            sWrite.WriteLine("<html>");
        //            sWrite.WriteLine("<head>");
        //            sWrite.WriteLine("<style>");
        //            sWrite.WriteLine("table, th, td {border: 2px ;}");
        //            sWrite.WriteLine("p.big {line-height: 400%;}");
        //            sWrite.WriteLine("</style>");
        //            sWrite.WriteLine("</head>");
        //            sWrite.WriteLine("<body >");
        //            sWrite.WriteLine("<br>");
        //            if (logo_name != "")
        //            {
        //                string Appath = System.IO.Directory.GetCurrentDirectory();
        //                if (File.Exists(Appath + "\\" + logo_name))
        //                {
        //                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
        //                    sWrite.WriteLine("<tr>");
        //                    sWrite.WriteLine("<td width='30px' height='50px' align='left' rowspan='3'><img src='" + Appath + "\\" + logo_name + "' style='width:70px;height:70px;></td>  ");
        //                    sWrite.WriteLine("<td width='870px' align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=4><b>&nbsp;" + strclinicname.ToString() + "</font> <br><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + str_address.ToString() + "<br>&nbsp;" + str_contact.ToString() + " </b></td></tr>");
        //                    sWrite.WriteLine("</table>");
        //                }
        //                else
        //                {
        //                    sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
        //                    sWrite.WriteLine("<tr>");
        //                    sWrite.WriteLine("<td  align='left' height='25px'><FONT  color=black  face='Segoe UI' SIZE=5>" + strclinicname + "</font></td></tr>");
        //                    sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>" + str_address + "</font></td></tr>");
        //                    sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>" + str_contact + "</font></td></tr>");
        //                    sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
        //                    sWrite.WriteLine("</table>");
        //                }
        //            }
        //            else
        //            {
        //                sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
        //                sWrite.WriteLine("<tr><th align='left'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=4></font></th></tr>");
        //                sWrite.WriteLine("<tr><th align='left'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=3></font></th></tr>");
        //                sWrite.WriteLine("<tr><th align='left'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=3></font></th></tr>");
        //                sWrite.WriteLine("</table>");
        //            }
        //            sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
        //            sWrite.WriteLine("<tr><td align='left'  ><hr/></td></tr>");
        //            sWrite.WriteLine("</table>");
        //            sWrite.WriteLine("<table align=center>");
        //            sWrite.WriteLine("<col width=500>");
        //            sWrite.WriteLine("<tr>");
        //            sWrite.WriteLine("<th align=center><FONT COLOR=black FACE=' segoe UI' SIZE=5>Attendant Certificate </font></th>");
        //            sWrite.WriteLine("</tr>");
        //            sWrite.WriteLine("<tr>");
        //            sWrite.WriteLine("<td align=left><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=3 > <br><b>Date:</b>&nbsp;&nbsp;  </font> <FONT COLOR=black FACE='Geneva, segoe UI' SIZE=2 > " + dtp_issuse.Value.ToString("dd-MMM-yyyy") + "</th></tr> " + "</font>");
        //            sWrite.WriteLine("<td align=right><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=3 > <br><b>REF NO:</b>&nbsp;&nbsp;  </font> <FONT COLOR=black FACE='Geneva, segoe UI' SIZE=2 > " + txt_designation.Text + "</th></tr> " + "</font>");
        //            sWrite.WriteLine("</tr>");
        //            sWrite.WriteLine("<tr>");

        //            sWrite.WriteLine("<td align=center><p style='width:700px;line-height:3;><br><FONT COLOR=black FACE=' segoe UI' SIZE=7 >&nbsp;&nbsp;&nbsp; ");
        //            sWrite.WriteLine(" This is To certify that <b>" + txt_attandant_name.Text + " </b> was an attendant to patient <b> " + txt_patientname.Text + "</b> his/her File No <b>" + txt_fileno.Text + "</b> who was seen in the hospital on  <b> " + dtb_admited.Value + "  </b>This certificate has been issused at the request of the patient attendant. ");
        //            sWrite.WriteLine("</tr>");
        //            sWrite.WriteLine("<tr>");
        //            sWrite.WriteLine("<td align=left><p style='width:700px;line-height:3;><br><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=7 >");
        //            sWrite.WriteLine("Remark : <b>"+ txt_remark.Text+ " ");
        //            sWrite.WriteLine("<td align=left><p style='width:700px;line-height:3;><br><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=7 > ");
        //            sWrite.WriteLine("He/she may be granted leave from  <b>" + dtb_from.Value+ "</b> To <b>"+ dtb_to.Value+ "</b>for <b>"+ txt_days.Text+ "days ");
        //            sWrite.WriteLine("</font> </p></td>");
        //            sWrite.WriteLine("</tr>");
        //            sWrite.WriteLine("<tr>");
        //            sWrite.WriteLine("<td align=right><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=2 ><br><br><b>Doctor's Name & Signature</b>&nbsp;&nbsp;  </font> </td> ");
        //            sWrite.WriteLine("</tr>");
        //            sWrite.WriteLine("<tr>");
        //            sWrite.WriteLine("<td align=right><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=3 ><br><br><b> " + cmbDoctor.Text + "</b>&nbsp;&nbsp;  </font> </td> ");
        //            sWrite.WriteLine("</tr>");
        //            sWrite.WriteLine("</table>");
        //            sWrite.WriteLine("<script>window.print();</script>");
        //            sWrite.WriteLine("</body>");
        //            sWrite.WriteLine("</html>");
        //            sWrite.Close();
        //            System.Diagnostics.Process.Start(Apppath + "\\Attendant.html");
        //        }
        //        else
        //        {
        //            MessageBox.Show("From date has to be lesser than To date. Please Check and try again..", "Invalid Dates", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        public void print()
        {
            try
            {
                DateTime SelDateFrom = dtb_from.Value;
                DateTime SelDateTo = dtb_to.Value;
                //DateTime SelDateFrom1 = dtp_admiton.Value;
                //DateTime SelDateTo1 = dtp_deliveredon.Value;
                //DateTime SelDateFrom2 = dtp_mleave_from.Value;
                //DateTime SelDateTo2 = dtp_mleave_to.Value;
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

                    double Days = (dtb_to.Value - dtb_from.Value).TotalDays;
                    txt_days.Text = (Days + 1).ToString();


                }
                string Apppath = System.IO.Directory.GetCurrentDirectory();
                System.IO.StreamWriter sWrite = new StreamWriter(Apppath + "\\Attendant.html");
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
                        sWrite.WriteLine("<td width='30px' height='50px' align='left' rowspan='3'><img src='" + Appath + "\\" + logo_name + "' style='width:70px;height:70px;></td>  ");
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
                sWrite.WriteLine("<th align=center><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=5>Attendant Leave  Certificate </font></th>");

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
                sWrite.WriteLine("<td align=left><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=2>&nbsp;Date of Issuse :" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "</FONT>  </td>");
                sWrite.WriteLine("<td align='right' ><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=2>REF NO : " + txt_designation.Text + "</font></td>");
                //sWrite.WriteLine("<td colspan='1' align=left><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=1>REF NO :" + textBox1.Text + "</font></th>");
                sWrite.WriteLine("</tr>");
                sWrite.WriteLine("</table>");

                sWrite.WriteLine("<br>");
                sWrite.WriteLine("<br>");
                //sWrite.WriteLine("<div>");
                ////sWrite.WriteLine("<table align='center' style='width:800px;border: 1px ;border-collapse: collapse;'>");

                //sWrite.WriteLine("<col >");
                //sWrite.WriteLine("<br>");
                //sWrite.WriteLine("<tr>");
                //sWrite.WriteLine("    <td align='center' width='200px' Height='10px' style='border:1px solid #000;background-color:#999999'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3 >&nbsp;<b>Name Of the Patient</b></font></td>");

                //sWrite.WriteLine("    <td align='center' width='200px' Height='10px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3><b>Designation / Place of Work</b></font></td>");
                //sWrite.WriteLine("    <td align='center' width='200px' Height='10px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3><b>File No</b></font></td>");
                //sWrite.WriteLine("    <td align='center' width='200px' Height='10px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>&nbsp;<b>Resident Card</b></font></td>");
                //sWrite.WriteLine("    <td align='center' width='200px' Height='10px' style='border:1px solid #000;background:#999999'><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3>&nbsp;<b>DOB Card</b></font></td>");
                //sWrite.WriteLine("</tr>");

                //sWrite.WriteLine("<tr>");

                //sWrite.WriteLine("    <td align='center' width='200px' Height='10px' style='border:1px solid #000' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + txt_patientname.Text + "</font></td>");
                //sWrite.WriteLine("    <td align='center' width='200px' Height='10px' style='border:1px solid #000' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + txt_designation.Text + "</font></td>");
                //sWrite.WriteLine("    <td align='center' width='200px' Height='10px' style='border:1px solid #000' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + txt_fileno.Text + "</font></td>");
                //sWrite.WriteLine("    <td align='center' width='200px' Height='10px' style='border:1px solid #000' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + txt_residentcard.Text + "</font></td>");
                //sWrite.WriteLine("    <td align='center' width='200px' Height='10px' style='border:1px solid #000' ><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=2>&nbsp;" + txt_dob.Text + "</font></td>");
                //sWrite.WriteLine("</tr>");
                //sWrite.WriteLine("<tr>");
                //sWrite.WriteLine("<td align=right colspan=7><FONT COLOR=black FACE='Geneva, Segoe UI' SIZE=3 ><br><br><b> </b>&nbsp;&nbsp;  </font> </td> ");
                //sWrite.WriteLine("</tr>");
                //sWrite.WriteLine("</table>");
                //sWrite.WriteLine("</div>");

                //sWrite.WriteLine(" This is To certify that <b>" + txt_attandant_name.Text + " </b> was an attendant to patient <b> " + txt_patientname.Text + "</b> his/her File No <b>" + txt_fileno.Text + "</b> who was seen in the hospital on  <b> " + dtb_admited.Value + "  </b>This certificate has been issused at the request of the patient attendant. ");
                //            sWrite.WriteLine("</tr>");
                //            sWrite.WriteLine("<tr>");
                //            sWrite.WriteLine("<td align=left><p style='width:700px;line-height:3;><br><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=7 >");
                //            sWrite.WriteLine("Remark : <b>"+ txt_remark.Text+ " ");
                //            sWrite.WriteLine("<td align=left><p style='width:700px;line-height:3;><br><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=7 > ");
                //            sWrite.WriteLine("He/she may be granted leave from  <b>" + dtb_from.Value+ "</b> To <b>"+ dtb_to.Value+ "</b>for <b>"+ txt_days.Text+ "days ");
                //            sWrite.WriteLine("</font> </p></td>");


                sWrite.WriteLine("<table align='center' style='width:740px;border: 1px  ;border-collapse: collapse;'>");
                sWrite.WriteLine("<td align='left'><p style='width:740px;line-height:3;><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=7 >");
                sWrite.WriteLine("This is To certify that &nbsp;" + "<b><u>" + txt_attandant_name.Text + "</u></b> " + "  " + "" + "was an attendant to patient &nbsp;" + "  " + "<b><u>" + txt_patientname.Text + "</u></b>." + "  " + "" + "His/her File No" + " &nbsp; " + "<u><b>" + txt_fileno.Text + "</u></b>" + " &nbsp;" + "who was seen in the hospital on " + " &nbsp;" + "<b><u>" + dtb_admited.Value.ToString("dd/MM/yyyy") + "</b></u>" + " &nbsp; his certificate has been issused at the request of the patient attendant.  &nbsp; <br><b>Remarks:</b>" + " " + "<br> He/she may be granted leave from " + "<b><u>" + dtb_from.Value.ToString("dd/MM/yyyy") + "</b></u>" + " TO " + "<b><u>" + dtb_to.Value.ToString("dd/MM/yyyy") + "</b></u>" + "For " + txt_days.Text + " days <br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;This certificate is not valid without doctor's signature and stamp of Clinic/Polyclinic/Hospital<br><br>"); ;

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
                System.Diagnostics.Process.Start(Apppath + "\\Attendant.html");
                //}
                //else
                //{
                //    MessageBox.Show("From date has to be lesser than To date. Please Check and try again..", "Invalid Dates", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
                DateTime SelDateFrom = dtb_from.Value;
                DateTime SelDateTo = dtb_to.Value;
            //DateTime SelDateFrom1 = dtp_admiton.Value;
            //DateTime SelDateTo1 = dtp_deliveredon.Value;
            //DateTime SelDateFrom2 = dtp_mleave_from.Value;
            //DateTime SelDateTo2 = dtp_mleave_to.Value;
            //if (SelDateTo >= SelDateFrom && SelDateTo1 >= SelDateFrom1 && SelDateTo2 >= SelDateFrom2)
            //{
            txt_days.Text = "";
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
                //if(Convert.ToDateTime(dtb_from.Value)> Convert.ToDateTime(dtb_to.Value))
                //{

                //}
                //else
                //{

                //}
                    double Days = (dtb_to.Value - dtb_from.Value).TotalDays;
                    txt_days.Text = (Days + 1).ToString();


                }
                string html = "";
                //string Apppath = System.IO.Directory.GetCurrentDirectory();
                //System.IO.StreamWriter sWrite = new StreamWriter(Apppath + "\\Attendant.html");
               html=("<html>");
              html +=("<head>");
                //html += ("<style>");
                //html += ("<table, th, td {border: 1px }>");

                //html += ("p.big {line-height: 400%;}");
                //html += ("</style>");
                html += ("</head>");
                html += ("<body >");
                html += ("<br>"); html += ("</br>");
                if (logo_name != "")
                {
                //string Appath = System.IO.Directory.GetCurrentDirectory();
                //if (File.Exists(Appath + "\\" + logo_name))
                //{
                //    sWrite.WriteLine("<table align='center' style='width:500px;border: 1px ;border-collapse: collapse;'>");
                //    sWrite.WriteLine("<tr>");
                //    sWrite.WriteLine("<td width='30px' height='50px' align='left' rowspan='3'><img src='" + Appath + "\\" + logo_name + "' style='width:70px;height:70px;></td>  ");
                //    sWrite.WriteLine("<td width='770px' align='right' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=4><b>&nbsp;" + strclinicname.ToString() + "</font> <br><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + str_address.ToString() + "<br>&nbsp;" + str_contact.ToString() + " </b></td></tr>");
                //    sWrite.WriteLine("</tr>");
                //    sWrite.WriteLine("</table>");
                //}
                //else
                //{
                //html += ("<table align='center' style='width:500px;border: 0px ;border-collapse: collapse;'>");
                //html += ("<tr>");
                //html += ("<td  align='center' height='25px'><FONT  color=black  face='Segoe UI' SIZE=5 >" + strclinicname + "</font></td></tr>");
                //html += ("<tr><td  align='center' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>" + str_address + "</font></td></tr>");
                //html += ("<tr><td align='center' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2 >" + str_contact + "</font></td></tr>");
                //// sWrite.WriteLine("<tr><td align='center' colspan='1'><hr/></td></tr>");
                //html += ("</table>");

                html += ("<table align='center' style='width:700px;border: 0px ;border-collapse: collapse;'>");
                html += ("<tr>");
                html += ("<td  align='left' height='25px'><FONT  color=black  face='Segoe UI' SIZE=5 >" + strclinicname + "</font></td></tr>");
                html += ("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3 >" + str_address + "</font></td></tr>");
                html += ("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2 >" + str_contact + "</font></td></tr>");
                //html += ("<tr><td align='left' colspan='2'><hr/></td></tr>");
                html += ("</table>");
                //}
            }
                else
                {

                    html += ("<table align='center' style='width:500px;border: 0px ;border-collapse: collapse;'>");
                    html += ("<tr><th align='center'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=4></font></th></tr>");
                    html += ("<tr><th align='center'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=3></font></th></tr>");
                    html += ("<tr><th align='center'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=3></font></th></tr>");
                    html += ("</table>");
                }

                html += ("<table align='center'> ");
                html += ("<col width=500>");
                html += ("<tr>");
                html += ("<th align='center'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=6 ><b><u>Attendant Leave Certificate </u></b></font></th>");
            html += ("</tr>");
            //html += ("<br>");
            //html += ("</br>");
            html += ("<tr>");
            html += ("<td align='center'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=5 ><br></br></font></td>");

            html += ("</tr>");
            html += ("</col>");
           
            html += ("</table>");

            // html += ("<br>"); 
            //html += ("</br>");
            //html += ("<br>");
            //html += ("</br>");

            html += ("<table align='center' style='width:700px;border: 0px ;border-collapse: collapse;'>");
            html += ("<col >");
            html += ("<tr>");
            html += ("<td align='left'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=3 >&nbsp;Date of Issuse :" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "</FONT>  </td>");
            html += ("<td align='right' ><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=3 >REF NO : " + txt_designation.Text + "</font></td>");
            //sWrite.WriteLine("<td colspan='1' align=left><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=1>REF NO :" + textBox1.Text + "</font></th>");
   
            html += ("</tr>");
            html += ("<tr>");
            html += ("<td align='center'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=5 ><br></br></font></td>");
            html += ("</tr>");
            html += ("<tr>");
            html += ("<td align='center'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=5 ><br></br></font></td>");
            html += ("</tr>");
            html += ("</col >"); 
            html += ("</table>");

          


            html += ("<table align='center' style='width:700px;border: 0px  ;border-collapse: collapse;'>");
            html += ("<tr>");
            html += ("<br>"); html += ("</br>");
            //html += ("<br>"); html += ("</br>");
            html += ("<td align='left'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=4 >");
            html += ("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;This is To certify that &nbsp;" + "<b><u>" + txt_attandant_name.Text + "</u></b> " + "  " + "" + "was an attendant to patient &nbsp;" + "  " + "<b><u>" + txt_patientname.Text + "</u></b>." + "  " + "" + "His/her File No" + " &nbsp; " + "<b><u>" + txt_fileno.Text + "</u></b>" + " &nbsp;" + "who was seen in the hospital on " + " &nbsp;" + "<b><u>" + dtb_admited.Value.ToString("dd/MM/yyyy") + "</u></b>" + " &nbsp; his certificate has been issused at the request of the patient attendant.  &nbsp; <br></br><br></br><b>Remarks:</b><br></br>" + " " + "<br></br> He/she may be granted leave from " + "<b><u>" + dtb_from.Value.ToString("dd/MM/yyyy") + "</u></b>" + " TO " + "<b><u>" + dtb_to.Value.ToString("dd/MM/yyyy") + "</u></b>" + "For " + txt_days.Text + " days. <br></br><br></br>This certificate is not valid without doctor's signature and stamp of Clinic/Polyclinic/Hospital<br></br><br></br>"); 

            html += ("</font></td>");
            html += ("</tr>"); 
            html += ("<br>"); html += ("</br>");
            html += ("<tr>");
            html += ("<td align='left' ><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=3 >Patient Signature</font></td>");
            html += ("<col width=700px>");
            html += ("<td align='right' ><FONT  COLOR=black FACE='Geneva, segoe UI' SIZE=3 > Doctor's Signature with Seal  </font></td>");
            html += ("</col>");
            html += ("</tr>");
            html += ("<tr>");
            //html += ("<td align='left' ><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=3 >........................... </font></td>");
            //html += ("<col width=270>");
            html += ("<td align='right' ><FONT COLOR=black font_align='center' FACE='Geneva, segoe UI' SIZE=3 ><b>" + cmbDoctor.Text + " </b></font></td>");
            html += (" </tr>");
            html += (" <tr>");
            //html += ("<td align='right' ><FONT COLOR=black font_align='center' FACE='Geneva, segoe UI' SIZE=3 ><br></br>...........................  </font></td>");
            html += (" </tr>");

            html += (" <tr>");
            html += ("<td align='center'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=2 ><br></br><br></br>Countersigned by Directorate of Private Health Establishment, Affairs </font></td>");
            html += (" </tr>");
            html += ("</table>");

            //html += ("<script>window.print();</script>");
            html += ("</body>");
                html += ("</html>");
                string name = "Attentant" + patient_id + ".pdf";
                string server = db.server();
                string realfile = @"\\" + server + "\\Pappyjoe_utilities\\Attachments\\";
                using (FileStream stream = new FileStream(realfile + name, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 15f, 15f, 15f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    StringReader sr = new StringReader(html);

                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    stream.Close();
                }
                string pathimage = "\\" + "\\Pappyjoe_utilities" + "\\" + "\\Attachments\\" + "\\" + name;
                //txt_path3.Text = pathimage;
                mdl.insattach(patient_id, name, pathimage, cmbDoctor.Text, "Certificate");
                MessageBox.Show("Pdf successfully saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //sWrite.Close();
                //System.Diagnostics.Process.Start(Apppath + "\\Attendant.html");
                //}
                //else
                //{
                //    MessageBox.Show("From date has to be lesser than To date. Please Check and try again..", "Invalid Dates", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }
    }
}
