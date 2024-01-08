using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PappyjoeMVC.Controller;
using PappyjoeMVC.Model;
namespace PappyjoeMVC.View
{
    public partial class Dashboard_Doctor : Form
    {
        public Dashboard_Doctor()
        {
            InitializeComponent();
        }
        DoctorDashboard_controller cntrl = new DoctorDashboard_controller();
        user_privillage_model privi_mdl = new user_privillage_model();
        public string doctor_id;
        public int p_count = 0;
        private void Dashboard_Doctor_Load(object sender, EventArgs e)
        {
            DataTable cname = this.cntrl.cname();
            if (cname.Rows.Count > 0)
            {

                label2.Text = "Welcome to " +cname.Rows[0][0].ToString();
            }
            DataTable uname = this.cntrl.uname(doctor_id);
            if (uname.Rows.Count > 0)
            {
                label11.Text = uname.Rows[0][0].ToString();
                label10.Text = uname.Rows[0][1].ToString();
            }
            DateTime startdate = Convert.ToDateTime(dctr_dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
            DateTime enddate = Convert.ToDateTime(dctr_dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
            DataTable dt = this.cntrl.get_tdy_apnmt(doctor_id,startdate,enddate);
            today_app(dt);
            todayApp_count(dt);
            DataTable dt_prc = this.cntrl.get_tdy_prc(doctor_id,startdate,enddate);
            today_prcd(dt_prc);
            DataTable dtprc = this.cntrl.get_tdyprc(doctor_id, startdate, enddate);
            prcd_count(dtprc);
            DataTable new_patient = this.cntrl.new_patient_count(doctor_id,startdate,enddate);
            newPatient_count(new_patient);
            DataTable rec_amt = this.cntrl.totalRec_rcptnst(doctor_id, startdate, enddate);
            total_rec_amt(rec_amt);
            //DataTable dtn = this.cntrl.get_all_nrstoday();
            
            //all_nurses(dtn);

            DataTable dtn = this.cntrl.get_all_nrstoday();
            all_nurses(dtn);
            nurses_count(dtn);
            DataTable dtr = this.cntrl.get_all_rcptoday();
            receptn__count(dtr);
            DataTable dtl = this.cntrl.get_lab_notify(startdate, enddate);
            lab_ttcount(dtl);
            label8.BackColor = Color.White;
            //label8.BackColor = Color.FromArgb(204, 235, 255);
            label9.BackColor = Color.White;
            //label27.BackColor = Color.FromArgb(204, 235, 255);
            dgv_doctors.Rows.Clear();
            dctrcombo.SelectedIndex = 0;
        }

        public void nurses_count(DataTable dtp)
        {
            if (dtp.Rows.Count > 0)
            {
                label15.Text = dtp.Rows.Count.ToString();
            }
        }
        public void receptn__count(DataTable dtr)
        {
            if (dtr.Rows.Count > 0)
            {
                label17.Text = dtr.Rows.Count.ToString();
            }
        }
        public void lab_ttcount(DataTable dtl)
        {
            if (dtl.Rows.Count > 0)
            {
                label19.Text = dtl.Rows.Count.ToString();
            }
        }
        public void today_app(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                dgv_aptschedule.Rows.Clear();
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    dgv_aptschedule.Rows.Add();
                    dgv_aptschedule.Rows[i].Cells["c_name"].Value = dt.Rows[i]["pt_name"].ToString();
                    DateTime daytime = Convert.ToDateTime(dt.Rows[i]["start_datetime"].ToString());
                    string day = daytime.ToString("dd/MM/yyyy");
                    string time = daytime.ToString("hh:mm tt");
                    dgv_aptschedule.Rows[i].Cells["c_date"].Value = day;
                    dgv_aptschedule.Rows[i].Cells["c_vtime"].Value = time;
                    dgv_aptschedule.Rows[i].Cells["c_cond"].Value = dt.Rows[i]["plan_new_procedure"].ToString();
                    i++;
                    //SetFontAndColors();
                }
            }
        }
        public void todayApp_count(DataTable dt)
        {
            if(dt.Rows.Count>0)
            {
                labelapt_count.Text = dt.Rows.Count.ToString();
                label12.Text = dt.Rows.Count.ToString();
            }
        }
        public void today_prcd(DataTable dt_prc)
        {
            if (dt_prc.Rows.Count > 0)
            {
                dgv_procedure.Rows.Clear();
                int i = 0;
                foreach (DataRow dr in dt_prc.Rows)
                {
                    if(dt_prc.Rows[i]["plan_new_procedure"].ToString()!="")
                    {
                        dgv_procedure.Rows.Add();
                        string prcdr = dt_prc.Rows[i]["plan_new_procedure"].ToString();
                        dgv_procedure.Rows[i].Cells["c_procedures"].Value = prcdr;
                        DateTime startdate = Convert.ToDateTime(dctr_dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
                        DateTime startdate1 = Convert.ToDateTime(dctr_dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
                        DataTable pro_count = this.cntrl.today_procedure_count(doctor_id, prcdr, startdate, startdate1);
                        p_count = pro_count.Rows.Count;
                        if (p_count > 0)
                            dgv_procedure.Rows[i].Cells["c_count"].Value = p_count;
                    }
                   
                    i++;
                }
            }
        }
        public void prcd_count(DataTable dtprc)
        {
            if(dtprc.Rows.Count>0)
            {
                labelprc_count.Text = dtprc.Rows.Count.ToString();
            }
        }

        public void newPatient_count(DataTable new_patient)
        {
            if(new_patient.Rows.Count>0)
            {
                labelpat_count.Text = new_patient.Rows.Count.ToString();
            }
        }
        public void total_rec_amt(DataTable rec_amt)
        {
            if (rec_amt.Rows.Count > 0)
            {
                labeltotal_rec_count.Text = rec_amt.Rows[0][0].ToString();
            }
        }

        public void all_nurses(DataTable dtn)
        {
            if (dtn.Rows.Count > 0)
            {
                dgv_nurse.Rows.Clear();
                int i = 0;
                foreach (DataRow dr in dtn.Rows)
                {
                    dgv_nurse.Rows.Add();
                    dgv_nurse.Rows[i].Cells[0].Value = dtn.Rows[i]["doctor_name"].ToString();
                    i++;
                    //SetFontAndColors();
                }
            }
        }


        //private void SetFontAndColors()
        //{
        //    this.dgv_doctors.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Regular);
        //    this.dgv_doctors.DefaultCellStyle.ForeColor = Color.DimGray;
        //    this.dgv_doctors.DefaultCellStyle.SelectionForeColor = Color.Gray;
        //    this.dgv_doctors.DefaultCellStyle.SelectionBackColor = Color.White;
        //    this.dgv_doctors.RowsDefaultCellStyle.BackColor = Color.White;
        //    this.dgv_doctors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //    this.dgv_doctors.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        //    this.dgv_doctors.GridColor = Color.LightGray;
        //    this.dgv_aptschedule.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        //    this.dgv_aptschedule.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        //    this.dgv_aptschedule.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Regular);
        //    this.dgv_aptschedule.DefaultCellStyle.ForeColor = Color.DimGray;
        //    this.dgv_aptschedule.DefaultCellStyle.SelectionForeColor = Color.Black;
        //    this.dgv_aptschedule.DefaultCellStyle.SelectionBackColor = Color.White;
        //    this.dgv_aptschedule.RowsDefaultCellStyle.BackColor = Color.White;
        //    this.dgv_aptschedule.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        //    this.dgv_aptschedule.GridColor = Color.LightGray;
        //    this.dgv_aptschedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //    this.dgv_procedure.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Regular);
        //    this.dgv_procedure.DefaultCellStyle.ForeColor = Color.DimGray;
        //    this.dgv_procedure.DefaultCellStyle.SelectionForeColor = Color.Black;
        //    this.dgv_procedure.DefaultCellStyle.SelectionBackColor = Color.White;
        //    this.dgv_procedure.RowsDefaultCellStyle.BackColor = Color.White;
        //    this.dgv_procedure.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //    this.dgv_procedure.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        //    this.dgv_procedure.GridColor = Color.LightGray;

        //}

        private void addnewpatient_Click(object sender, EventArgs e)
        {
            var frm = new Add_New_Patients();
            frm.ShowDialog();
            frm.doctor_id = doctor_id;
        }

       
        private void label27_Click(object sender, EventArgs e)
        {
            label27.BackColor = Color.White;
            //label4.BackColor = Color.FromArgb(204, 235, 255);
            label9.BackColor = Color.FromArgb(204, 235, 255);
            //label8.BackColor = Color.FromArgb(204, 235, 255);
            dgv_procedure.Rows.Clear();
            DataTable dprcdr = this.cntrl.procedure();
            all_proc(dprcdr);
        }
        public void all_proc(DataTable dprcdr)
        {
            if (dprcdr.Rows.Count > 0)
            {
                dgv_procedure.Rows.Clear();
                int i = 0;
                foreach (DataRow dr in dprcdr.Rows)
                {
                    dgv_procedure.Rows.Add();
                    string prcdr = dprcdr.Rows[i]["name"].ToString();
                    dgv_procedure.Rows[i].Cells["c_procedures"].Value = prcdr;
                    DateTime startDateTime = Convert.ToDateTime(dctr_dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
                    DateTime startDateTime1 = Convert.ToDateTime(dctr_dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
                    DataTable pro_count = this.cntrl.today_procedure_count(prcdr, startDateTime, startDateTime1);
                    p_count = pro_count.Rows.Count;
                    dgv_procedure.Rows[i].Cells["c_count"].Value = p_count;
                    i++;
                    //this.dgv_procedure.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Regular);
                    //this.dgv_procedure.DefaultCellStyle.ForeColor = Color.DimGray;
                    //this.dgv_procedure.DefaultCellStyle.SelectionForeColor = Color.Black;
                    //this.dgv_procedure.DefaultCellStyle.SelectionBackColor = Color.White;
                    //this.dgv_procedure.RowsDefaultCellStyle.BackColor = Color.White;
                    //this.dgv_procedure.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    //this.dgv_procedure.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                    //this.dgv_procedure.GridColor = Color.LightGray;
                }
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            label9.BackColor = Color.White;
            //label4.BackColor = Color.FromArgb(204, 235, 255);
            //label8.BackColor = Color.FromArgb(204, 235, 255);
            label27.BackColor = Color.FromArgb(204, 235, 255);
            dgv_procedure.Rows.Clear();
            DateTime startDateTime = Convert.ToDateTime(dctr_dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
            DateTime startDateTime1 = Convert.ToDateTime(dctr_dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
            DataTable dt_prc = this.cntrl.get_tdy_prc(doctor_id, startDateTime,startDateTime1);
            today_prcd(dt_prc);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //userControl_label3.BackColor = Color.White;
            //label8.BackColor = Color.FromArgb(204, 235, 255);
            //label9.BackColor = Color.FromArgb(204, 235, 255);
            //label27.BackColor = Color.FromArgb(204, 235, 255);
            //dgv_doctors.Rows.Clear();
            //DataTable dtn = this.cntrl.get_all_nrstoday();
            //all_nurses(dtn);
        }

        private void label8_Click(object sender, EventArgs e)
        {
           // label8.BackColor = Color.White;
           // label9.BackColor = Color.FromArgb(204, 235, 255);
           // //label4.BackColor = Color.FromArgb(204, 235, 255);
           // //label27.BackColor = Color.FromArgb(204, 235, 255);
           // dgv_doctors.Rows.Clear();
           // DataTable dtn = this.cntrl.get_all_nrstoday();
           //all_nurses(dtn);
        }

        private void dctrcombo_Selectedindexchanged(object sender, EventArgs e)
        {
            DateTime startdate = Convert.ToDateTime(dctr_dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
            DateTime enddate = Convert.ToDateTime(dctr_dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
            if (dctrcombo.SelectedIndex == 0)
            {
                dgv_aptschedule.Rows.Clear();
                //dgv_doctors.Rows.Clear();
                //dgv_procedure.Rows.Clear();
                DataTable dt = this.cntrl.get_tdy_apnmt(doctor_id, startdate, enddate);
                today_app(dt);
                //DataTable dtn = this.cntrl.get_all_nrstoday();
                //all_nurses(dtn);
                //DataTable dt_prc = this.cntrl.get_tdy_prc(doctor_id, startdate, enddate);
                //today_prcd(dt_prc);

            }
            if(dctrcombo.SelectedIndex==1)
            {
                dgv_aptschedule.Rows.Clear();
                //dgv_doctors.Rows.Clear();
                //dgv_procedure.Rows.Clear();
                DataTable dtb = this.cntrl.weeklyappointcount(doctor_id);
                today_app(dtb);
                //DataTable dtn = this.cntrl.get_all_nrstoday();
                //all_nurses(dtn);
                //DataTable dtp2_week = this.cntrl.weeklyprocedure(doctor_id);
                //fill_proc_weekly(dtp2_week);
            }
            if(dctrcombo.SelectedIndex==2)
            {
                dgv_aptschedule.Rows.Clear();
                //dgv_doctors.Rows.Clear();
                //dgv_procedure.Rows.Clear();
                DataTable dtb = this.cntrl.Monthlyappointcount(doctor_id);
                today_app(dtb);
                //DataTable dtn = this.cntrl.get_all_nrstoday();
                //all_nurses(dtn);
                //DataTable dtp2_month = this.cntrl.monthlyprocedure(doctor_id);
                //fill_proc_monthly(dtp2_month);

            }
        }
        public void fill_proc_weekly(DataTable dtp2_week)
        {
            if (dtp2_week.Rows.Count > 0)
            {
                dgv_procedure.Rows.Clear();
                int i = 0;

                foreach (DataRow dr in dtp2_week.Rows)
                {
                    dgv_procedure.Rows.Add();
                    string prcdr = dtp2_week.Rows[i]["plan_new_procedure"].ToString();//plan_new_procedure
                    dgv_procedure.Rows[i].Cells["c_procedures"].Value = prcdr;
                    DateTime startDateTime = Convert.ToDateTime(dctr_dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
                    DateTime startDateTime1 = Convert.ToDateTime(dctr_dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
                    DataTable pro_count = this.cntrl.weekly_procedure_count(prcdr,doctor_id);
                    p_count =pro_count.Rows.Count;
                    dgv_procedure.Rows[i].Cells["c_count"].Value = p_count;
                    i++;
                    //this.dgv_procedure.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Regular);
                    //this.dgv_procedure.DefaultCellStyle.ForeColor = Color.DimGray;
                    //this.dgv_procedure.DefaultCellStyle.SelectionForeColor = Color.Black;
                    //this.dgv_procedure.DefaultCellStyle.SelectionBackColor = Color.White;
                    //this.dgv_procedure.RowsDefaultCellStyle.BackColor = Color.White;
                    //this.dgv_procedure.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    //this.dgv_procedure.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                    //this.dgv_procedure.GridColor = Color.LightGray;
                }
            }
        }
        public void fill_proc_monthly(DataTable dtp2_month)
        {
            if (dtp2_month.Rows.Count > 0)
            {
                dgv_procedure.Rows.Clear();
                int i = 0;


                foreach (DataRow dr in dtp2_month.Rows)
                {

                    dgv_procedure.Rows.Add();
                    string prcdr = dtp2_month.Rows[i]["name"].ToString();
                    dgv_procedure.Rows[i].Cells["c_procedures"].Value = prcdr;
                    DateTime startDateTime = Convert.ToDateTime(dctr_dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
                    DateTime startDateTime1 = Convert.ToDateTime(dctr_dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
                    DataTable pro_count = this.cntrl.monthly_procedure_count(prcdr);
                    p_count = pro_count.Rows.Count;
                    dgv_procedure.Rows[i].Cells["c_count"].Value = p_count;
                    i++;
                    //this.dgv_procedure.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Regular);
                    //this.dgv_procedure.DefaultCellStyle.ForeColor = Color.DimGray;
                    //this.dgv_procedure.DefaultCellStyle.SelectionForeColor = Color.Black;
                    //this.dgv_procedure.DefaultCellStyle.SelectionBackColor = Color.White;
                    //this.dgv_procedure.RowsDefaultCellStyle.BackColor = Color.White;
                    //this.dgv_procedure.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    //this.dgv_procedure.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                    //this.dgv_procedure.GridColor = Color.LightGray;
                }
            }
        }

        private void btn_book_appointments_Click(object sender, EventArgs e)
        {
            string calndr = privi_mdl.Add_privillege(doctor_id);
            if (int.Parse(calndr) > 0)
            {
                bool cal = true;
                var form2 = new AppointmentBooking(cal);
                form2.doctor_id = doctor_id;
                form2.dashboard_flag = true;
                form2.ShowDialog();
                DateTime startdate = Convert.ToDateTime(dctr_dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
                DateTime enddate = Convert.ToDateTime(dctr_dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
                DataTable dt = this.cntrl.get_tdy_apnmt(doctor_id, startdate, enddate);
                today_app(dt);
                todayApp_count(dt);
                DataTable dt_prc = this.cntrl.get_tdy_prc(doctor_id, startdate, enddate);
                today_prcd(dt_prc);
                DataTable dtprc = this.cntrl.get_tdyprc(doctor_id, startdate, enddate);
                prcd_count(dtprc);
                DataTable new_patient = this.cntrl.new_patient_count(doctor_id, startdate, enddate);
                newPatient_count(new_patient);
                form2.Dispose();
            }
            else
            {
                MessageBox.Show("There is No Privilege to add appointment", "Security Role", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            } 
           
        }

        private void panelDoctor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            //label4.BackColor = Color.White;
            //label8.BackColor = Color.FromArgb(204, 235, 255);
            ////label9.BackColor = Color.FromArgb(204, 235, 255);
            ////label27.BackColor = Color.FromArgb(204, 235, 255);
            //dgv_doctors.Rows.Clear();
            //DataTable dtn = this.cntrl.get_all_nrstoday();
            //all_nurses(dtn);
        }
    }
}
