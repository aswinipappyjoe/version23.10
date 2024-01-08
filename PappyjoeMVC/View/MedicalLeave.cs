using PappyjoeMVC.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace PappyjoeMVC
{
    public partial class MedicalLeave : Form
    {
        public string patient_id = "0";
        Common_model mdl = new Common_model();
        Connection db = new Connection();
        string gender = ""; string age = ""; string days = "";
        public MedicalLeave()
        {
            InitializeComponent();
        }

        private void MedicalLeave_Load(object sender, EventArgs e)
        {
            DataTable rs_patients = mdl.Get_Patient_Details(patient_id);
            age = rs_patients.Rows[0]["age"].ToString();
            lblid.Text = rs_patients.Rows[0]["pt_id"].ToString();
            lbl_ID.Text = rs_patients.Rows[0]["aadhar_id"].ToString();
            gender = rs_patients.Rows[0]["gender"].ToString();
            
            if (rs_patients.Rows[0]["pt_name"].ToString() != "")
            {
                lblname.Text = rs_patients.Rows[0]["pt_name"].ToString();
                lblname1.Text= rs_patients.Rows[0]["pt_name"].ToString();
                if (rs_patients.Rows[0]["gender"].ToString() == "Female")
                {
                    lblgender.Text = "She";
                    lblname1.Text = "She";
                }
                else
                {
                    lblgender.Text = "He";
                    lblname1.Text = "He";
                }
            }


            days = rs_patients.Rows[0]["days"].ToString();

            if (rs_patients.Rows[0]["age"].ToString() != "")
            {
                age =  rs_patients.Rows[0]["age"].ToString() + rs_patients.Rows[0]["days"].ToString();
            }
            if (rs_patients.Rows[0]["age2"].ToString() != "")
            {
                if (!string.IsNullOrEmpty(age))
                {
                    age = age + " " + rs_patients.Rows[0]["age2"].ToString() + "Months";
                }
                else
                {
                    age =  rs_patients.Rows[0]["age2"].ToString() + "Months";
                }

            }
            lblage.Text = age;
            //comboBox2.SelectedIndex = 0;
            //comboBox5.SelectedIndex = 0;
            //comboBox7.SelectedIndex = 0;
            System.Data.DataTable dtdr = mdl.get_all_doctorname();// db.table("select DISTINCT id,doctor_name from tbl_doctor where login_type='doctor' or login_type ='admin' and activate_login='yes' order by doctor_name");
            cmbDoctor.DisplayMember = "doctor_name";
            cmbDoctor.ValueMember = "id";
            cmbDoctor.DataSource = dtdr;
            panel8.Width = this.Width;
            dateTimePickerTo.Value = DateTime.Today;
            fromdate();
        }
        private void fromdate()
        {
            System.Data.DataTable dt7 = mdl.get_patient_date(patient_id);
            string aa = dt7.Rows[0]["date"].ToString();
            if (dt7.Rows[0]["date"].ToString() != "")
            {
                dateTimePickefrom.Show();
                dateTimePickefrom.Value = DateTime.Parse(DateTime.Parse(dt7.Rows[0]["date"].ToString()).ToString());
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
              print();
        }
        public void print()
        {
            try
            {
                DateTime inpdatefrom = dateTimePicker2.Value;
                DateTime inpdateto = dateTimePicker3.Value;
                DateTime SelDateFrom = dateTimePickefrom.Value;
                DateTime SelDateTo = dateTimePickerTo.Value;
                double i = (SelDateTo - SelDateFrom).TotalDays;
                if (SelDateTo >= SelDateFrom && inpdateto>=inpdatefrom)
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
                    }
                    string Apppath = System.IO.Directory.GetCurrentDirectory();
                    StreamWriter sWrite = new StreamWriter(Apppath + "\\MedicalHistory.html");
                    sWrite.WriteLine("<html>");
                    sWrite.WriteLine("<head>");
                    sWrite.WriteLine("<style>");
                    sWrite.WriteLine("table, th, td {border: 2px ;}");
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
                            sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                            sWrite.WriteLine("<tr>");
                            sWrite.WriteLine("<td width='30px' height='50px' align='left' rowspan='3'><img src='" + Appath + "\\" + logo_name + "' style='width:70px;height:70px;></td>  ");
                           sWrite.WriteLine("<td width='870px' align='left' height='25px'><FONT  COLOR=black  face='Segoe UI' SIZE=4><b>&nbsp;" + strclinicname.ToString() + "</font> <br><FONT COLOR=black FACE='Segoe UI' SIZE=2>&nbsp;" + str_address.ToString() + "<br>&nbsp;" + str_contact.ToString() + " </b></td></tr>");


                            sWrite.WriteLine("</table>");
                        }
                        else
                        {
                            sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                            sWrite.WriteLine("<tr>");
                            sWrite.WriteLine("<td  align='left' height='25px'><FONT  color=black  face='Segoe UI' SIZE=5>" + strclinicname + "</font></td></tr>");
                            sWrite.WriteLine("<tr><td  align='left' height='25px'><FONT COLOR=black FACE='Segoe UI' SIZE=3>" + str_address + "</font></td></tr>");
                            sWrite.WriteLine("<tr><td align='left' height='40' valign='top'> <FONT COLOR=black FACE='Segoe UI' SIZE=2>" + str_contact + "</font></td></tr>");
                            sWrite.WriteLine("<tr><td align='left' colspan='2'><hr/></td></tr>");
                            sWrite.WriteLine("</table>");
                        }
                    }
                    else
                    {
                        sWrite.WriteLine("<table align='center' style='width:700px;border: 1px ;border-collapse: collapse;'>");
                        sWrite.WriteLine("<tr><th align='left'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=4></font></th></tr>");
                        sWrite.WriteLine("<tr><th align='left'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=3></font></th></tr>");
                        sWrite.WriteLine("<tr><th align='left'><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=3></font></th></tr>");
                        sWrite.WriteLine("</table>");
                    }
                    sWrite.WriteLine("<table align=center>");
                    sWrite.WriteLine("<col width=500>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<th align=center><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=5>Leave Certificate </font></th>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("</table>");
                    sWrite.WriteLine("<table align='center' style='width:700px;border-top: 1px solid black;border-bottom: 1px solid black;border-collapse: separate;'>");
                    sWrite.WriteLine("<tr><td align='left'>Date</td><td>");
                    sWrite.WriteLine(":" + " " + datepicker1.Value.ToString("dd/MM/yyyy"));
                    sWrite.WriteLine("</td></tr>");
                    sWrite.WriteLine("<tr><td align='left'>Name</td><td>");
                    sWrite.WriteLine(":" + " " + lblname.Text);
                    sWrite.WriteLine("</td></tr>");
                    sWrite.WriteLine("<tr><td align='left'>Age</td><td>");
                    sWrite.WriteLine(":" + " " + age);
                   

                    sWrite.WriteLine("<td align='right'>Sex </td>");
                     sWrite.WriteLine("<td> " + ":"+" "  + gender);
                    sWrite.WriteLine("</td></tr>");
                    sWrite.WriteLine("<tr><td align='left'>PatientId</td><td>");
                    sWrite.WriteLine(":" + " " + lblid.Text);
                    sWrite.WriteLine("</td></tr>");
                    sWrite.WriteLine("<tr><td align='left'>Civil ID</td><td>");
                    sWrite.WriteLine(":" + " " + lbl_ID.Text);
                    sWrite.WriteLine("</td></tr>");
                    sWrite.WriteLine("<tr><td align='left'>Department</td><td>");
                    sWrite.WriteLine(":" + " " + txtdept.Text);
                    sWrite.WriteLine("</td></tr>");
                    sWrite.WriteLine("<tr><td align='left'>Unit</td><td>");
                    sWrite.WriteLine(":" + " " + txtunit.Text);
                    sWrite.WriteLine("</td></tr>");
                    sWrite.WriteLine("</table>");












                    sWrite.WriteLine("</br></br> " + "This is to certify that " + lblname.Text +"("+ lblid.Text+")"+" was examined/treated by me/my department and found to suffer from " +txtdisease.Text+".");
                    sWrite.WriteLine("</br> "+lblname1.Text+" was admitted as an Inpatient from "+dateTimePicker2.Value.ToString("dd/MM/yyyy")+" to "+dateTimePicker3.Value.ToString("dd/MM/yyyy"));
                    sWrite.WriteLine("</br> " + lblname1.Text + " is authorised to get a sick leave from " +dateTimePickefrom.Value.ToString("dd/MM/yyyy") + " to " + dateTimePickerTo.Value.ToString("dd/MM/yyyy")+" for "+i+" Days.");
                   sWrite.WriteLine("</br></br></br></br></br></br>To be countersigned by medical office incharge or his deputy or the concerned consultant, if leave exceeds 3 days.");
                    sWrite.WriteLine("</br></br></br>HOSPITAL STAMP");
                    //sWrite.WriteLine("</br></br></br>This certificate is not valid without a hospital stamp.");
                    //sWrite.WriteLine("</br>HOSPITAL STAMP");
                    sWrite.WriteLine("<table align=center>");
                    sWrite.WriteLine("<col width=500>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<th align=center><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=5> </font></th>");
                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("<tr>");
                    //sWrite.WriteLine("<td align=right><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=3 > <br><b>Date:</b>&nbsp;&nbsp;  </font> <FONT COLOR=black FACE='Geneva, segoe UI' SIZE=2 > " + datePick.Value.ToString("dd-MMM-yyyy") + "</th></tr> " + "</font>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td align=left><p style='width:700px;line-height:3;><br><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=7 >&nbsp;&nbsp;&nbsp; ");
                    //sWrite.WriteLine("<b>" + comboBox1.Text + " " + txtname.Text + "</b> " + comboBox2.Text + " under my treatment for <b> " + txtfor.Text + "  </b>from <b>" + dateTimePickefrom.Value.ToString("dd/MM/yyyy") + "</b> to <b>" + dateTimePickerTo.Value.ToString("dd/MM/yyyy") + "</b>. " + txt_he.Text + " " + comboBox5.Text + " advised complete rest for this period. " + txt_she.Text + " " + comboBox7.Text + " medically fit to resume duty from <b> " + dateTimePickerfrmlast.Value.ToString("dd/MM/yyyy") + " " + txtMore.Text + "</b>");
                    sWrite.WriteLine("</font> </p></td>");
                    sWrite.WriteLine("</tr>");
                    //sWrite.WriteLine("<tr>");
                    ////sWrite.WriteLine("<td width=50%>");

                    ////sWrite.WriteLine("This certificate is not valid without a hospital stamp.");
                    ////sWrite.WriteLine("</br>HOSPITAL STAMP");
                    ////sWrite.WriteLine("</td>");
                    //sWrite.WriteLine("<td align=right width=30%><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=2 ><br><br><b>Doctor's Name & Signature</b>&nbsp;&nbsp;  </font> </td> ");
                    //sWrite.WriteLine("</tr>");
                    //sWrite.WriteLine("<tr>");
                    //sWrite.WriteLine("<td align=right><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=3 ><br><br><b> " + cmbDoctor.Text + "</b>&nbsp;&nbsp;  </font> </td> ");
                    //sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("</table>");
                    sWrite.WriteLine("<table>");
                    sWrite.WriteLine("<tr>");
                    sWrite.WriteLine("<td width=50%>");

                    sWrite.WriteLine("This certificate is not valid without a HOSPITAL STAMP.");
                    //sWrite.WriteLine("</br>HOSPITAL STAMP");
                    sWrite.WriteLine("</td>");
                     sWrite.WriteLine("<td align=center width=30%><FONT COLOR=black FACE='Geneva, segoe UI' SIZE=2 ><br><br><b>Doctor's Name & Signature</b>&nbsp;&nbsp;  </font> ");
                   
                    sWrite.WriteLine( "</br>"+cmbDoctor.Text + "</br></br></br></br></br></br>" + "Signature and Rubber Stamp Of Doctor" + " </ td > ");

                    sWrite.WriteLine("</tr>");
                    sWrite.WriteLine("</table>");
                    sWrite.WriteLine("<script>window.print();</script>");
                    sWrite.WriteLine("</body>");
                    sWrite.WriteLine("</html>");
                    sWrite.Close();
                    System.Diagnostics.Process.Start(Apppath + "\\MedicalHistory.html");
                }
                else
                {
                    MessageBox.Show("From date has to be lesser than To date. Please Check and try again..", "Invalid Dates", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
