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
    public partial class Dashboard_Reception : Form
    {
        public Dashboard_Reception()
        {
            InitializeComponent();
        }
        public string doctor_id;
        public int pat_count1 = 0,p_count=0;
        dashboard_controller cntrl = new dashboard_controller();
        private void Dashboard_Reception_Load(object sender, EventArgs e)
        {
            
            DataTable cname = this.cntrl.cname();
            if (cname.Rows.Count > 0)
            {
                label2.Text = "Welcome to " + cname.Rows[0][0].ToString();
            }
            DataTable uname = this.cntrl.uname(doctor_id);
            if (uname.Rows.Count > 0)
            {
                label11.Text = uname.Rows[0][0].ToString();
                label10.Text = uname.Rows[0][1].ToString();
            }
            DateTime startdate = Convert.ToDateTime(rec_dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
            DateTime startdate1= Convert.ToDateTime(rec_dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
            DataTable dtb = this.cntrl.get_all_aponmt(startdate, startdate1);
            get_all_apt(dtb);
            DataTable dt = this.cntrl.get_all_dctrtoday(startdate,startdate1);
            get_all_dctr(dt);
            DataTable dtp= this.cntrl.procedure(startdate,startdate1);
            get_all_prcd(dtp);
            DataTable pcount= this.cntrl.new_Patient_count(startdate,startdate1);
            apt_count(dtb);
            pat_count(pcount);
            DataTable dt_ = this.cntrl.procedure_count_receptn(startdate, startdate1);//
            prcdr_count(dt_);
           DataTable rec_amt = this.cntrl.totalRec_rcptnst(doctor_id,startdate,startdate1);
            total_rec_amt(rec_amt);
            dctrcombo.SelectedIndex = 0;
            label8.BackColor = Color.White;
            label9.BackColor = Color.White;
        }
        public void get_all_apt(DataTable dtb)
        {
            if (dtb.Rows.Count > 0)
            {
                dgv_aptschedule.Rows.Clear();
                int i = 0;
                foreach (DataRow dr in dtb.Rows)
                {
                    dgv_aptschedule.Rows.Add();
                    dgv_aptschedule.Rows[i].Cells["c_name"].Value = dtb.Rows[i]["pt_name"].ToString();
                    DateTime daytime = Convert.ToDateTime(dtb.Rows[i]["start_datetime"].ToString());
                    string day = daytime.ToString("dd/MM/yyyy");
                    string time = daytime.ToString("hh:mm tt");
                    dgv_aptschedule.Rows[i].Cells["c_date"].Value = day;
                    dgv_aptschedule.Rows[i].Cells["c_vtime"].Value = time;
                    dgv_aptschedule.Rows[i].Cells["c_dname"].Value = dtb.Rows[i]["doctor_name"].ToString();
                    dgv_aptschedule.Rows[i].Cells["c_cond"].Value = dtb.Rows[i]["plan_new_procedure"].ToString();
                    i++;
                }
            }
        }
        public void get_all_dctr(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                dgv_doctors.Rows.Clear();
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    dgv_doctors.Rows.Add();
                    string name = dt.Rows[i]["doctor_name"].ToString();
                    dgv_doctors.Rows[i].Cells["c_dnames"].Value = name; 
                    DateTime startdate = Convert.ToDateTime(rec_dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
                    DateTime startdate1 = Convert.ToDateTime(rec_dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
                    DataTable dtdctr = this.cntrl.get_specific_doctr(name, startdate, startdate1);
                    pat_count1 = dtdctr.Rows.Count;
                    if(pat_count1>0)
                       dgv_doctors.Rows[i].Cells["c_apt_count"].Value = pat_count1;
                    i++;
                }
            }
        }
        public void get_all_prcd(DataTable dtp)
        {
            if (dtp.Rows.Count > 0)
            {
                dgv_procedure.Rows.Clear();
                int i = 0;
                foreach (DataRow dr in dtp.Rows)
                {
                    if (dr[0].ToString()!="")
                    {
                        dgv_procedure.Rows.Add();
                        string prcdr = dr[0].ToString();//plan_new_procedure
                        dgv_procedure.Rows[i].Cells["c_procedures"].Value = prcdr;
                        DateTime startdate = Convert.ToDateTime(rec_dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
                        DateTime startdate1 = Convert.ToDateTime(rec_dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
                        DataTable pro_count = this.cntrl.today_procedure_count(prcdr, startdate, startdate1);
                        p_count = pro_count.Rows.Count;
                        if(p_count>0)
                           dgv_procedure.Rows[i].Cells["c_count"].Value = p_count;
                        i++;
                    }
                }
            }
        }

    
        public void apt_count(DataTable dtb)
        {
            if(dtb.Rows.Count>0)
            {
                labelapt_count.Text = dtb.Rows.Count.ToString();
            }
    }
        public void pat_count(DataTable pcount)
        {
            if(pcount.Rows.Count>0)
            {
                labelpat_count.Text = pcount.Rows.Count.ToString();
            }
        }
        public void prcdr_count(DataTable dtp)
        {
          if(dtp.Rows.Count>0)
            {
                labelprc_count.Text = dtp.Rows.Count.ToString();
            }
        }

        private void dgv_doctors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string name = dgv_doctors.CurrentRow.Cells["c_dnames"].Value.ToString();
            DateTime startdate = Convert.ToDateTime(rec_dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
            DateTime startdate1= Convert.ToDateTime(rec_dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
            DataTable dtdctr = this.cntrl.get_specific_doctr(name, startdate, startdate1);
            dgv_aptschedule.Rows.Clear();
            if (dtdctr.Rows.Count > 0)
            {
                dgv_aptschedule.Rows.Clear();
                int i = 0;
                foreach (DataRow dr in dtdctr.Rows)
                {
                    dgv_aptschedule.Rows.Add();
                    dgv_aptschedule.Rows[i].Cells["c_name"].Value = dtdctr.Rows[i]["pt_name"].ToString();
                    DateTime daytime = Convert.ToDateTime(dtdctr.Rows[i]["start_datetime"].ToString());
                    string day = daytime.ToString("dd/MM/yyyy");
                    string time = daytime.ToString("hh:mm tt");
                    dgv_aptschedule.Rows[i].Cells["c_date"].Value = day;
                    dgv_aptschedule.Rows[i].Cells["c_vtime"].Value = time;
                    dgv_aptschedule.Rows[i].Cells["c_dname"].Value = dtdctr.Rows[i]["doctor_name"].ToString();
                    dgv_aptschedule.Rows[i].Cells["c_cond"].Value = dtdctr.Rows[i]["plan_new_procedure"].ToString();
                    i++;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime startdate = Convert.ToDateTime(rec_dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
            DateTime startdate1 = Convert.ToDateTime(rec_dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
            DataTable dtb = this.cntrl.get_all_aponmt(startdate, startdate1);
            get_all_apt(dtb);
            DataTable dt = this.cntrl.get_all_dctrtoday(startdate, startdate1);
            get_all_dctr(dt);
        }

        private void addnewpatient_Click(object sender, EventArgs e)
        {
            var frm = new Add_New_Patients();
            frm.ShowDialog();
            frm.doctor_id = doctor_id;
            
        }

      

        private void dctrcombo_onSelactedIndexChanged(object sender, EventArgs e)
        {
            DateTime startDateTime = Convert.ToDateTime(rec_dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
            DateTime startDateTime1 = Convert.ToDateTime(rec_dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
            if (dctrcombo.SelectedIndex == 0)
            {
                dgv_aptschedule.Rows.Clear();
                DataTable dtb = this.cntrl.get_all_aponmt(startDateTime, startDateTime1);
                get_all_apt(dtb);
            }
            else if (dctrcombo.SelectedIndex == 1)
            {
                dgv_aptschedule.Rows.Clear();
                DataTable dtb = this.cntrl.weeklyappointcount();
                get_all_apt(dtb);
            }
            else if (dctrcombo.SelectedIndex == 2)
            {
                dgv_aptschedule.Rows.Clear();
                DataTable dtb = this.cntrl.Monthlyappointcount();
                get_all_apt(dtb);
            }
        }

     

        public void total_rec_amt(DataTable rec_amt)
        {
            if (rec_amt.Rows.Count > 0)
            {
                labeltotal_rec_count.Text = rec_amt.Rows[0][0].ToString();
            }
        }

      

        private void btn_book_appointments_Click(object sender, EventArgs e)
        {
             var form2 = new AppointmentBooking();//   Main_Calendar();
            form2.doctor_id = doctor_id;
            form2.dashboard_flag = true;
            form2.ShowDialog();
            DateTime startdate = Convert.ToDateTime(rec_dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
            DateTime startdate1 = Convert.ToDateTime(rec_dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
            DataTable dtb = this.cntrl.get_all_aponmt(startdate, startdate1);
            get_all_apt(dtb);
            DataTable dt = this.cntrl.get_all_dctrtoday(startdate, startdate1);
            get_all_dctr(dt);
            DataTable dtp = this.cntrl.procedure(startdate, startdate1);
            get_all_prcd(dtp);
            form2.Dispose();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            label8.BackColor = Color.White;
            label4.BackColor = Color.FromArgb(204, 235, 255);
            DateTime startdate = Convert.ToDateTime(rec_dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
            DateTime startdate1 = Convert.ToDateTime(rec_dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
            DataTable dt = this.cntrl.get_all_dctrtoday(startdate, startdate1);
            get_all_dctr(dt);
        }

       

        private void label9_Click(object sender, EventArgs e)
        {
            label9.BackColor = Color.White;
            label27.BackColor = Color.FromArgb(204, 235, 255);
            DateTime startdate = Convert.ToDateTime(rec_dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
            DateTime startdate1 = Convert.ToDateTime(rec_dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
            DataTable dtp = this.cntrl.procedure(startdate, startdate1);
            get_all_prcd(dtp);
        }

        private void label27_Click(object sender, EventArgs e)
        {
            label27.BackColor = Color.White;
            label9.BackColor = Color.FromArgb(204, 235, 255);
            dgv_procedure.Rows.Clear();
            DataTable dprcdr = this.cntrl.procedure();
            get_all_prcd(dprcdr);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            label4.BackColor = Color.White;
            label8.BackColor = Color.FromArgb(204, 235, 255);
            dgv_doctors.Rows.Clear();
            DataTable dt = this.cntrl.get_all_dctrtoday();
            get_all_dctr(dt);
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
                    DateTime startDateTime = Convert.ToDateTime(rec_dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
                    DateTime startDateTime1 = Convert.ToDateTime(rec_dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
                    DataTable pro_count = this.cntrl.monthly_procedure_count(prcdr);
                    p_count = pro_count.Rows.Count;
                    dgv_procedure.Rows[i].Cells["c_count"].Value = p_count;
                    i++;

                }
            }
        }


    }
}
