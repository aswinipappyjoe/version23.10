using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PappyjoeMVC.View;
using PappyjoeMVC.Model;

namespace PappyjoeMVC.View
{
    public partial class MainForm : Form
    {
        public string doctor_id = "";
        public string patient_id { get; set; }
        Connection db = new Connection();
        user_privillage_model privi_mdl = new user_privillage_model();
        public MainForm()
        {
            InitializeComponent();
        }

        private Form activeform = null;
        private void openChildForm(Form childform)
        {
            if (activeform != null)
                activeform.Close();
            activeform = childform;
            childform.TopLevel = false;
            childform.FormBorderStyle = FormBorderStyle.None;
            childform.Dock = DockStyle.Fill;
            panelChildFormMain.Controls.Add(childform);
            panelChildFormMain.Tag = childform;
            childform.BringToFront();
            childform.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Buttoncolor();
            toolStripButton3.BackColor = Color.SkyBlue;
            var form2 = new Patients();
            form2.doctor_id = doctor_id;
            openChildForm(form2);
            hide_PatSearchList();
        }

        private void AAA_Load(object sender, EventArgs e)
        {
            if (PappyjoeMVC.Model.Connection.MyGlobals.project_type == "Pharmacy")
            {
                toolStripButton7.Visible = false;//profile
                toolStripButton2.Visible = false;//calender
                toolStripButton13.Visible = false;//consultation
                toolStripButton15.Visible = false;//dashboard
                if (doctor_id == "1")
                {
                    toolStripButton3.Visible = true;//records
                    toolStripButton4.Visible = true;//communication
                    toolStripButton5.Visible = true;//inventory
                    toolStripButton6.Visible = true;//reports
                    toolStripButton10.Visible = true;//expense
                    toolStripButton14.Visible = true;//salse
                }
                else
                {
                    string emr = privi_mdl.emr_main(doctor_id);
                    if (int.Parse(emr) > 0)
                    {
                        toolStripButton3.Visible = true;
                    }
                    else
                    {
                        toolStripButton3.Visible = false;
                    }
                    string cmnctn = privi_mdl.Communication_main(doctor_id);
                    if (int.Parse(cmnctn) > 0)
                    {
                        toolStripButton4.Visible = true;
                    }
                    else
                    {
                        toolStripButton4.Visible = false;
                    }
                    string id = privi_mdl.inventry_main(doctor_id);
                    if (int.Parse(id) > 0)
                    {
                        toolStripButton5.Visible = true;
                        toolStripButton14.Visible = true;
                    }
                    else
                    {
                        toolStripButton5.Visible = false;
                        toolStripButton14.Visible = false;
                    }
                    string report = privi_mdl.reports_main(doctor_id);
                    if (int.Parse(report) > 0)
                    {
                        toolStripButton6.Visible = true;
                    }
                    else
                    {
                        toolStripButton6.Visible = false;
                    }
                    string expnse = privi_mdl.expense_main(doctor_id);
                    if (int.Parse(expnse) > 0)
                    {
                        toolStripButton10.Visible = true;
                    }
                    else
                    {
                        toolStripButton10.Visible = false;
                    }
                }
                var form2 = new   StockReport(); //Patients(); Pharmacy_dashboard();
                form2.doctor_id = doctor_id;
                openChildForm(form2);
                toolStripButton5.BackColor = Color.SkyBlue;
                toolStripButton9.ToolTipText = PappyjoeMVC.Model.GlobalVariables.Version;
                string docnam = db.scalar("select doctor_name from tbl_doctor Where id='" + doctor_id + "'");
                if (docnam != "")
                {
                    toolStripTextDoctor.Text = "Logged In As : " + docnam;
                }
                DataTable clinicname = db.table("select name,id,path,Prescription_lang  from tbl_practice_details");
                if (clinicname.Rows.Count > 0)
                {
                    string clinicn = "";
                    clinicn = clinicname.Rows[0][0].ToString();
                    toolStripButton1.Text = clinicn.Replace("¤", "'");
                }
            }
            else
            {
                if (doctor_id != "1")
                {
                    //toolStripTextBox1
                    string user = privi_mdl.user_exsists(doctor_id);
                    if(int.Parse(user) >0)
                    {
                        toolStripTextBox1.Visible = true;
                    }
                    else
                    {
                        toolStripTextBox1.Visible = false;
                    }
                    string calndr = privi_mdl.Calender_main(doctor_id);
                    if (int.Parse(calndr) > 0)
                    {
                        toolStripButton2.Visible = true;
                    }
                    else
                    {
                        toolStripButton2.Visible = false;
                    }
                    string id = privi_mdl.inventry_main(doctor_id);
                    if (int.Parse(id) > 0)
                    {
                        toolStripButton5.Visible = true;
                        toolStripButton14.Visible = true;
                    }
                    else
                    {
                        toolStripButton5.Visible = false;
                        toolStripButton14.Visible = false;
                    }
                    string emr = privi_mdl.emr_main(doctor_id);
                    if (int.Parse(emr) > 0)
                    {
                        toolStripButton3.Visible = true;
                    }
                    else
                    {
                        toolStripButton3.Visible = false;
                    }
                    string report = privi_mdl.reports_main(doctor_id);
                    if (int.Parse(report) > 0)
                    {
                        toolStripButton6.Visible = true;
                    }
                    else
                    {
                        toolStripButton6.Visible = false;
                    }
                    string cnsltn = privi_mdl.Consultation_main(doctor_id);
                    if (int.Parse(cnsltn) > 0)
                    {
                        toolStripButton13.Visible = true;
                    }
                    else
                    {
                        toolStripButton13.Visible = false;
                    }
                    string cmnctn = privi_mdl.Communication_main(doctor_id);
                    if (int.Parse(cmnctn) > 0)
                    {
                        toolStripButton4.Visible = true;
                    }
                    else
                    {
                        toolStripButton4.Visible = false;
                    }
                    string expnse = privi_mdl.expense_main(doctor_id);
                    if (int.Parse(expnse) > 0)
                    {
                        toolStripButton10.Visible = true;
                    }
                    else
                    {
                        toolStripButton10.Visible = false;
                    }
                    string lbtrack = privi_mdl.labtrackinf_main(doctor_id);
                    if (int.Parse(lbtrack) > 0)
                    {
                        toolStripButton11.Visible = true;
                    }
                    else
                    {
                        toolStripButton11.Visible = false;
                    }
                    string patadd = privi_mdl.add_patients(doctor_id);
                    if (int.Parse(patadd) > 0)
                    {
                        toolStripDropDownButton1.Visible = true;
                    }
                    else
                    {
                        toolStripDropDownButton1.Visible = false;

                    }
                    string profile = privi_mdl.profile_main(doctor_id);
                    if (int.Parse(profile) > 0)
                    {
                        toolStripButton7.Visible = true;
                    }
                    else
                    {
                        toolStripButton7.Visible = false;

                    }
                }
                toolStripButton15.Visible = true;
                toolStripButton15.BackColor = Color.SkyBlue;
                if (PappyjoeMVC.Model.Connection.MyGlobals.loginType.TrimEnd() == "ADMIN")
                {
                    var form2 = new PappyjoeMVC.View.Dashboard();
                    form2.doctor_id = doctor_id;
                    openChildForm(form2);
                }
                else if (PappyjoeMVC.Model.Connection.MyGlobals.loginType.TrimEnd() == "DOCTOR")
                {
                    var form2 = new PappyjoeMVC.View.Dashboard_Doctor();
                    form2.doctor_id = doctor_id;
                    openChildForm(form2);
                }
                else if (PappyjoeMVC.Model.Connection.MyGlobals.loginType.TrimEnd() == "RECEPTIONIST")
                {
                    var form2 = new PappyjoeMVC.View.Dashboard_Reception();
                    form2.doctor_id = doctor_id;
                    openChildForm(form2);
                    //toolStripButton13.Visible = false;
                }
                else if (PappyjoeMVC.Model.Connection.MyGlobals.loginType.TrimEnd() == "NURSE")
                {
                    toolStripButton15.Visible = false;
                    toolStripButton13.Visible = false;
                    var form2 = new Main_Calendar();
                    form2.doctor_id = doctor_id;
                    openChildForm(form2);
                    toolStripButton2.BackColor = Color.SkyBlue;
                }
                //var form2 = new Main_Calendar();
                //form2.doctor_id = doctor_id;
                //openChildForm(form2);
                //toolStripButton2.BackColor = Color.SkyBlue;
                toolStripButton9.ToolTipText = PappyjoeMVC.Model.GlobalVariables.Version;
                string docnam = db.scalar("select doctor_name from tbl_doctor Where id='" + doctor_id + "'");
                if (docnam != "")
                {
                    toolStripTextDoctor.Text = "Logged In As : " + docnam;
                }
                DataTable clinicname = db.table("select name,id,path,Prescription_lang  from tbl_practice_details");
                if (clinicname.Rows.Count > 0)
                {
                    string clinicn = "";
                    clinicn = clinicname.Rows[0][0].ToString();
                    toolStripButton1.Text = clinicn.Replace("¤", "'");
                }
            }
            //if (PappyjoeMVC.Model.GlobalVariables.Subscription=="Platinum")
            //{
            //    if (doctor_id != "1")
            //    {

            //        string calndr = privi_mdl.Calender_main(doctor_id);// scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='CALENDAR' and Permission='A'");
            //        if (int.Parse(calndr) > 0)
            //        {
            //            toolStripButton2.Visible = true;
            //        }
            //        else
            //        {
            //            toolStripButton2.Visible = false;
            //        }
            //        string id = privi_mdl.inventry_main(doctor_id);// scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='INVENTORY' and Permission='A'");
            //        if (int.Parse(id) > 0)
            //        {
            //            toolStripButton5.Visible = true;
            //            toolStripButton14.Visible = true;
            //        }
            //        else
            //        {
            //            toolStripButton5.Visible = false;
            //            toolStripButton14.Visible = false;
            //        }
            //        string emr = privi_mdl.emr_main(doctor_id);// db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMR' and Permission='A'");
            //        if (int.Parse(emr) > 0)
            //        {
            //            toolStripButton3.Visible = true;
            //        }
            //        else
            //        {
            //            toolStripButton3.Visible = false;
            //        }
            //        string report = privi_mdl.reports_main(doctor_id);// db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='RPT' and Permission='A'");
            //        if (int.Parse(report) > 0)
            //        {
            //            toolStripButton6.Visible = true;
            //        }
            //        else
            //        {
            //            toolStripButton6.Visible = false;
            //        }
            //        string cnsltn = privi_mdl.Consultation_main(doctor_id);// db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='CONSULTATION' and Permission='A'");
            //        if (int.Parse(cnsltn) > 0)
            //        {
            //            toolStripButton13.Visible = true;
            //        }
            //        else
            //        {
            //            toolStripButton13.Visible = false;
            //        }
            //        string cmnctn = privi_mdl.Communication_main(doctor_id);// db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='COMMUNICATION' and Permission='A'");
            //        if (int.Parse(cmnctn) > 0)
            //        {
            //            toolStripButton4.Visible = true;
            //        }
            //        else
            //        {
            //            toolStripButton4.Visible = false;
            //        }
            //        string expnse = privi_mdl.expense_main(doctor_id);// db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EXPENSE' and Permission='A'");
            //        if (int.Parse(expnse) > 0)
            //        {
            //            toolStripButton10.Visible = true;
            //        }
            //        else
            //        {
            //            toolStripButton10.Visible = false;
            //        }
            //        string lbtrack = privi_mdl.labtrackinf_main(doctor_id);// db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='LABTRACKING' and Permission='A'");
            //        if (int.Parse(lbtrack) > 0)
            //        {
            //            toolStripButton11.Visible = true;
            //        }
            //        else
            //        {
            //            toolStripButton11.Visible = false;
            //        }
            //        string patadd = privi_mdl.add_patients(doctor_id);//.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='PAT' and Permission='A'");
            //        if (int.Parse(patadd) > 0)
            //        {
            //            toolStripDropDownButton1.Visible = true;
            //        }
            //        else
            //        {
            //            toolStripDropDownButton1.Visible = false;

            //        }
            //        string profile = privi_mdl.profile_main(doctor_id);
            //        if (int.Parse(profile) > 0)
            //        {
            //            toolStripButton7.Visible = true;
            //        }
            //        else
            //        {
            //            toolStripButton7.Visible = false;

            //        }
            //        //else
            //        //{
            //        //    var form2 = new PappyjoeMVC.View.StockReport();
            //        //    form2.doctor_id = doctor_id;
            //        //    openChildForm(form2);
            //        //}
            //    }

            //    string type = db.scalar("select login_type from tbl_doctor Where id='" + doctor_id + "'");
            //    if (type == "PHARMACY")
            //    {
            //        var form2 = new Sales();
            //        form2.doctor_id = doctor_id;
            //        openChildForm(form2);
            //        toolStripButton14.BackColor = Color.SkyBlue;
            //        toolStripButton9.ToolTipText = PappyjoeMVC.Model.GlobalVariables.Version;
            //        string docnam = db.scalar("select doctor_name from tbl_doctor Where id='" + doctor_id + "'");
            //        if (docnam != "")
            //        {
            //            toolStripTextDoctor.Text = "Logged In As : " + docnam;
            //        }
            //        DataTable clinicname = db.table("select name,id,path,Prescription_lang  from tbl_practice_details");
            //        if (clinicname.Rows.Count > 0)
            //        {
            //            string clinicn = "";
            //            clinicn = clinicname.Rows[0][0].ToString();
            //            toolStripButton1.Text = clinicn.Replace("¤", "'");
            //        }
            //    }
            //    else
            //    {
            //        var form2 = new Main_Calendar();
            //        form2.doctor_id = doctor_id;
            //        openChildForm(form2);
            //        toolStripButton2.BackColor = Color.SkyBlue;
            //        toolStripButton9.ToolTipText = PappyjoeMVC.Model.GlobalVariables.Version;
            //        string docnam = db.scalar("select doctor_name from tbl_doctor Where id='" + doctor_id + "'");
            //        if (docnam != "")
            //        {
            //            toolStripTextDoctor.Text = "Logged In As : " + docnam;
            //        }
            //        DataTable clinicname = db.table("select name,id,path,Prescription_lang  from tbl_practice_details");
            //        if (clinicname.Rows.Count > 0)
            //        {
            //            string clinicn = "";
            //            clinicn = clinicname.Rows[0][0].ToString();
            //            toolStripButton1.Text = clinicn.Replace("¤", "'");
            //        }
            //    }
              
            //}
            //else if (PappyjoeMVC.Model.GlobalVariables.Subscription == "Gold")
            //{
            //    toolStripButton5.Visible = false;
            //    var form2 = new Main_Calendar();
            //    form2.doctor_id = doctor_id;
            //    openChildForm(form2);
            //    toolStripButton2.BackColor = Color.SkyBlue;
            //    toolStripButton9.ToolTipText = PappyjoeMVC.Model.GlobalVariables.Version;
            //    string docnam = db.scalar("select doctor_name from tbl_doctor Where id='" + doctor_id + "'");
            //    if (docnam != "")
            //    {
            //        toolStripTextDoctor.Text = "Logged In As : " + docnam;
            //    }
            //    DataTable clinicname = db.table("select name,id,path,Prescription_lang  from tbl_practice_details");
            //    if (clinicname.Rows.Count > 0)
            //    {
            //        string clinicn = "";
            //        clinicn = clinicname.Rows[0][0].ToString();
            //        toolStripButton1.Text = clinicn.Replace("¤", "'");
            //    }
            //}
            //else if (PappyjoeMVC.Model.GlobalVariables.Subscription == "Silver")
            //{
            //    toolStripButton5.Visible = false;
            //    toolStripButton13.Visible = false;
            //    toolStripButton10.Visible = false;
            //    toolStripButton11.Visible = false;
            //    var form2 = new Main_Calendar();
            //    form2.doctor_id = doctor_id;
            //    openChildForm(form2);
            //    toolStripButton2.BackColor = Color.SkyBlue;
            //    toolStripButton9.ToolTipText = PappyjoeMVC.Model.GlobalVariables.Version;
            //    string docnam = db.scalar("select doctor_name from tbl_doctor Where id='" + doctor_id + "'");
            //    if (docnam != "")
            //    {
            //        toolStripTextDoctor.Text = "Logged In As : " + docnam;
            //    }
            //    DataTable clinicname = db.table("select name,id,path,Prescription_lang  from tbl_practice_details");
            //    if (clinicname.Rows.Count > 0)
            //    {
            //        string clinicn = "";
            //        clinicn = clinicname.Rows[0][0].ToString();
            //        toolStripButton1.Text = clinicn.Replace("¤", "'");
            //    }
            //}
        }

        public void Buttoncolor()
        {
            toolStripButton2.BackColor = Color.DodgerBlue;
            toolStripButton3.BackColor = Color.DodgerBlue;
            toolStripButton4.BackColor = Color.DodgerBlue;
            toolStripButton5.BackColor = Color.DodgerBlue;
            toolStripButton6.BackColor = Color.DodgerBlue;
            toolStripButton7.BackColor = Color.DodgerBlue;
            toolStripButton11.BackColor = Color.DodgerBlue;
            toolStripButton8.BackColor = Color.DodgerBlue;
            toolStripDropDownButton1.BackColor = Color.DodgerBlue;
            toolStripButton14.BackColor = Color.DodgerBlue;
            toolStripButton13.BackColor = Color.DodgerBlue;
            toolStripButton15.BackColor = Color.DodgerBlue;
        }
        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            toolStripTextBox1.Clear();
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text != "")
            {
                DataTable dtdr = db.table("select id, CONCAT(pt_name, ', ', substring(gender,1,1), ', ',primary_mobile_number) as patient from tbl_patient where (pt_name like '" + toolStripTextBox1.Text + "%'   or pt_id like '" + toolStripTextBox1.Text + "%' or primary_mobile_number like '" + toolStripTextBox1.Text + "%') and Profile_Status = 'Active' ");
                listpatientsearch.DisplayMember = "patient";
                listpatientsearch.ValueMember = "id";
                listpatientsearch.DataSource = dtdr;
                if (listpatientsearch.Items.Count == 0)
                {
                    listpatientsearch.Visible = false;
                }
                else
                {
                    listpatientsearch.Visible = true;
                }
                listpatientsearch.Location = new Point(toolStrip1.Width -365, 37);
                listpatientsearch.BringToFront();
            }
            else
            {
                listpatientsearch.Visible = false;
            }
        }

        private void listpatientsearch_MouseClick(object sender, MouseEventArgs e)
        {
            var form2 = new Patient_Profile_Details();
            form2.doctor_id = doctor_id;
            form2.patient_id = listpatientsearch.SelectedValue.ToString();
            listpatientsearch.Visible = false;
            openChildForm(form2);
            hide_PatSearchList();
            Buttoncolor();
            toolStripButton3.BackColor = Color.SkyBlue;
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {
            Buttoncolor();
            toolStripDropDownButton1.BackColor = Color.SkyBlue;
            if (doctor_id != "1")
            {
                string id;
                id = privi_mdl.add_patients(doctor_id);// db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='PAT' and Permission='A'");
                if (int.Parse(id)> 0)
                {
                    var form2 = new Add_New_Patients();
                    form2.doctor_id = doctor_id;
                    openChildForm(form2);
                }
                else
                {
                    MessageBox.Show("There is No Privilege to Add Patient", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                var form2 = new Add_New_Patients();
                form2.doctor_id = doctor_id;
                openChildForm(form2);
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Buttoncolor();
            if (doctor_id != "1")
            {
                string id;
                id = privi_mdl.settings_add(doctor_id);// db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='CLMS' and Permission='A'");
                if (int.Parse(id) > 0)
                {
                    var form2 = new Practice_Details();
                    form2.doctor_id = doctor_id;
                    openChildForm(form2);
                }
                else
                {
                    MessageBox.Show("There is No Privilege to Clinic Settings", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                var form2 = new Practice_Details();
                form2.doctor_id = doctor_id;
                openChildForm(form2);
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form2 = new Login();
            form2.Closed += (sender1, args) => this.Close();
            this.Hide();
            form2.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Buttoncolor();
            toolStripButton2.BackColor = Color.SkyBlue;
            var form2 = new Main_Calendar();
            form2.doctor_id = doctor_id;
            openChildForm(form2);
            hide_PatSearchList();
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            Buttoncolor();
            toolStripButton13.BackColor = Color.SkyBlue;
            var form2 = new Fasttrack_window_New();  //Fast_Track();//   Consultation();
            form2.loginid = doctor_id;
            //form2.ShowDialog();
            //form2.Dispose();
            openChildForm(form2);
            hide_PatSearchList();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Buttoncolor();
            toolStripButton4.BackColor = Color.SkyBlue;
            var form2 = new Communication();
            form2.doctor_id = doctor_id;
            openChildForm(form2);
            hide_PatSearchList();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Buttoncolor();
            toolStripButton5.BackColor = Color.SkyBlue;
            if (doctor_id != "1")
            {
                string id = privi_mdl.inventry_main(doctor_id);// db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='INVENTORY' and Permission='A'");
                if (int.Parse(id) > 0)
                {
                    var form2 = new PappyjoeMVC.View.StockReport();
                    form2.doctor_id = doctor_id;
                    openChildForm(form2);
                }
                else
                {
                    MessageBox.Show("There is No Privilege to View the Inventory", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                var form2 = new PappyjoeMVC.View.StockReport();
                form2.doctor_id = doctor_id;
                openChildForm(form2);
            }
            hide_PatSearchList();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Buttoncolor();
            toolStripButton6.BackColor = Color.SkyBlue;
            var form2 = new Reports();
            form2.doctor_id = doctor_id;
            openChildForm(form2);
            hide_PatSearchList();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            var form2 = new PappyjoeMVC.View.Expense();
            form2.doctor_id = doctor_id;
            form2.ShowDialog();
            form2.Dispose();
            hide_PatSearchList();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Buttoncolor();
            toolStripButton7.BackColor = Color.SkyBlue;
            if (PappyjoeMVC.Model.Connection.MyGlobals.loginType != "staff")
            {
                var form2 = new PappyjoeMVC.View.doctors();//  Doctor_Profile();
                form2.doctor_id = doctor_id;
                openChildForm(form2);
            }
            hide_PatSearchList();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            Buttoncolor();
            toolStripButton11.BackColor = Color.SkyBlue;
            var form2 = new LabtrackingReport();
            form2.doctor_id = doctor_id;
            openChildForm(form2);
            hide_PatSearchList();
        }

        private void AAA_Shown(object sender, EventArgs e)
        {
            
        }

        private void listpatientsearch_Leave(object sender, EventArgs e)
        {
            //listpatientsearch.Hide();
            //toolStripTextBox1.Clear();
        }

        private void listpatientsearch_MouseLeave(object sender, EventArgs e)
        {
            //listpatientsearch.Hide();
            //toolStripTextBox1.Clear();
        }
        private void hide_PatSearchList()
        {
            listpatientsearch.Hide();
            toolStripTextBox1.Clear();
            toolStripTextBox1.Text = "Search by Patient Name, id, Mobile No";
        }
        //user_privillage_model privi_mdl = new user_privillage_model();

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            string id = privi_mdl.sales_add(doctor_id);
            if(doctor_id!="1")
            {
                if(int.Parse(id)>0)
                {
                    Buttoncolor();
                    toolStripButton14.BackColor = Color.SkyBlue;
                    var form2 = new Sales();
                    //form2.doctor_id = doctor_id;
                    openChildForm(form2);
                    hide_PatSearchList();
                }
                else
                {
                    MessageBox.Show("There is No Privilege to Add Sales", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                Buttoncolor();
                toolStripButton14.BackColor = Color.SkyBlue;
                var form2 = new Sales();
                //form2.doctor_id = doctor_id;
                openChildForm(form2);
                hide_PatSearchList();
            }
           
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            Buttoncolor();
            toolStripButton15.BackColor = Color.SkyBlue;
            if (PappyjoeMVC.Model.Connection.MyGlobals.loginType.Trim() == "ADMIN")
            {
                var form2 = new PappyjoeMVC.View.Dashboard();
                form2.doctor_id = doctor_id;
                openChildForm(form2);
            }
            else if (PappyjoeMVC.Model.Connection.MyGlobals.loginType.Trim() == "DOCTOR")
            {
                var form2 = new PappyjoeMVC.View.Dashboard_Doctor();
                form2.doctor_id = doctor_id;
                openChildForm(form2);
            }
            else if (PappyjoeMVC.Model.Connection.MyGlobals.loginType.Trim() == "RECEPTIONIST")
            {
                var form2 = new PappyjoeMVC.View.Dashboard_Reception();
                form2.doctor_id = doctor_id;
                openChildForm(form2);
            }
            else if (PappyjoeMVC.Model.Connection.MyGlobals.loginType.Trim() == "NURSE")
            {
                //var form2 = new PappyjoeMVC.View.NursesDashboard();
                //form2.doctor_id = doctor_id;
                //openChildForm(form2);
            }

        }
    }
}
