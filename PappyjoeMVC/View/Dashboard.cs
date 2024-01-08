using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using PappyjoeMVC.Model;
using PappyjoeMVC.Controller;


namespace PappyjoeMVC.View
{
    public partial class Dashboard : Form
    {

        public Dashboard()
        {
            InitializeComponent();
        }
        public string doctor_id, dname = "";
        public int pat_count1 = 0, pcount = 0;
        Connection db = new Connection();
       
        dashboard_controller cntrl = new dashboard_controller();
       
        private void Dashboard_Load(object sender, EventArgs e)
        {
            flag = true;
            DateTime startDateTime = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
            DateTime startDateTime1 = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
            DataTable dtb = this.cntrl.get_all_aponmt(startDateTime, startDateTime1);
            today_app_shedl(dtb);
            DataTable dt1 = this.cntrl.get_tdy_dctr(startDateTime, startDateTime1);
            all_dctr(dt1);
            //DataTable dtprc = this.cntrl.get_tdyprc(doctor_id, startDateTime, startDateTime1);
            //all_proc(dtprc);
            DataTable dtp2 = this.cntrl.procedure(startDateTime, startDateTime1);
            fill_proc(dtp2);

            DataTable dt = this.cntrl.get_all_dctrtoday();
            count_actv_dr(dt);
            DataTable dtn= this.cntrl.get_all_nrstoday();
            nurses_count(dtn);
            DataTable dtr = this.cntrl.get_all_rcptoday();
            receptn__count(dtr);

            DataTable dtp_count = this.cntrl.procedure_count(startDateTime, startDateTime1);
            procedure_count(dtp_count);

            DataTable dtp1 = this.cntrl.all_Patient_count(startDateTime, startDateTime1);
            pat_count(dtp1);
          
            dctrcombo.SelectedIndex = 0;
           
          
            apt_count(dtb);
            
            DataTable rec_amt = this.cntrl.rec_amt1(startDateTime, startDateTime1);
            reciept_amnt(rec_amt);
            DataTable sale_amt = this.cntrl.sale_amt1(startDateTime, startDateTime1);
            inv_sale_amnt(sale_amt);
            DataTable cname= this.cntrl.cname();
            if (cname.Rows.Count > 0)
            {
             label2.Text = cname.Rows[0][0].ToString();
                
            }
            DataTable uname = this.cntrl.uname(doctor_id);
            if (uname.Rows.Count > 0)
            {
                label8.Text = uname.Rows[0][0].ToString();
                label7.Text = uname.Rows[0][1].ToString();
            }
            DataTable new_patient = this.cntrl.new_patient_count(doctor_id, startDateTime, startDateTime);
            newPatient_count(new_patient);
            DataTable dtl = this.cntrl.get_lab_notify(startDateTime,startDateTime1);
            lab_ttcount(dtl);
            userControl_label1.BackColor = Color.White;
            userControl_label3.BackColor = Color.FromArgb(204, 235, 255);
            userControl_label6.BackColor = Color.FromArgb(204, 235, 255);
            userControl_label2.BackColor = Color.White;
            flag = false;
        }
        public void lab_ttcount(DataTable dtl)
        {
            if (dtl.Rows.Count > 0)
            {
                label19.Text = dtl.Rows.Count.ToString();
            }
        }
        public void newPatient_count(DataTable new_patient)
        {
            if (new_patient.Rows.Count > 0)
            {
                labelpat_count.Text = new_patient.Rows.Count.ToString();
            }
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
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime startDateTime = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
            DateTime startDateTime1 = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
            DataTable dtb = this.cntrl.get_all_aponmt(startDateTime, startDateTime1);
            today_app_shedl(dtb);
            DataTable dt = this.cntrl.get_all_dctrtoday(startDateTime, startDateTime1);
            all_dctr(dt);
        }

        public void today_app_shedl(DataTable dtb)
        {
            dgv_aptschedule.Rows.Clear();
            if (dtb.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in dtb.Rows)
                {
                    dgv_aptschedule.Rows.Add();
                    dgv_aptschedule.Rows[i].Cells["id"].Value = dtb.Rows[i]["pt_id"].ToString();
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
                //SetFontAndColors();
            }
           
        }
        public void all_proc(DataTable dprcdr)
        {
            if (dprcdr.Rows.Count > 0)
            {
                dgv_proced_count.Rows.Clear();
                int i = 0;
                foreach (DataRow dr in dprcdr.Rows)
                {

                    dgv_proced_count.Rows.Add();
                    string prcdr =  dprcdr.Rows[i][0].ToString();//name  plan_new_procedure
                    dgv_proced_count.Rows[i].Cells[0].Value = prcdr;
                    DateTime startDateTime = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
                    DateTime startDateTime1 = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
                    DataTable pro_count = this.cntrl.today_procedure_count(prcdr, startDateTime, startDateTime1);
                    pcount = pro_count.Rows.Count;
                    if(pcount>0)
                        dgv_proced_count.Rows[i].Cells[1].Value = pcount;
                    i++;
                    
                }
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
        class doctor_fill
        {
            public string Name { get; set; }
            public int Surname { get; set; }
        }
        public void all_dctr(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                dgv_doctor_app.Rows.Clear();
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {

                    dgv_doctor_app.Rows.Add();
                    string name = dt.Rows[i]["doctor_name"].ToString();
                    dgv_doctor_app.Rows[i].Cells[0].Value = name; 
                    DateTime startDateTime = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
                    DateTime startDateTime1 = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
                    DataTable dtdctr = this.cntrl.get_specific_doctr(name, startDateTime, startDateTime1);
                    pat_count1 = dtdctr.Rows.Count;
                    if (pat_count1 > 0)
                        dgv_doctor_app.Rows[i].Cells[1].Value = pat_count1;
                    i++;
                }
                //SetFontAndColors();
            }
           
        }
     
        public void fill_proc(DataTable dtp2)
        {
            if (dtp2.Rows.Count > 0)
            {
                dgv_proced_count.Rows.Clear();
                int i = 0;
                int k = 0;
                foreach (DataRow dr in dtp2.Rows)
                {
                    if (dtp2.Rows[i]["plan_new_procedure"].ToString() != "")
                    {

                        dgv_proced_count.Rows.Add();
                        string prcdr = dtp2.Rows[i]["plan_new_procedure"].ToString();
                        dgv_proced_count.Rows[k].Cells[0].Value = prcdr;
                        DateTime startDateTime = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
                        DateTime startDateTime1 = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
                        DataTable pro_count = this.cntrl.today_procedure_count(prcdr, startDateTime, startDateTime1);
                        pcount = pro_count.Rows.Count;
                        if(pcount>0)
                           dgv_proced_count.Rows[k].Cells[1].Value = pcount;
                        k = k + 1;
                    }
                    i++;
                }
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

        private void dgv_doctors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if(e.RowIndex>=0)
            //{
            //    string name = dgv_doctor_app.CurrentRow.Cells[0].Value.ToString();
            //    DateTime startDateTime = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
            //    DateTime startDateTime1 = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
            //    DataTable dtdctr = this.cntrl.get_specific_doctr(name, startDateTime, startDateTime1);
            //    dgv_aptschedule.Rows.Clear();
            //    if (dtdctr.Rows.Count > 0)
            //    {
            //        dgv_aptschedule.Rows.Clear();
            //        int i = 0;
            //        foreach (DataRow dr in dtdctr.Rows)
            //        {
            //            dgv_aptschedule.Rows.Add();
            //            dgv_aptschedule.Rows[i].Cells["c_name"].Value = dtdctr.Rows[i]["pt_name"].ToString();
            //            DateTime daytime = Convert.ToDateTime(dtdctr.Rows[i]["start_datetime"].ToString());
            //            string day = daytime.ToString("dd/MM/yyyy");
            //            string time = daytime.ToString("hh:mm tt");
            //            dgv_aptschedule.Rows[i].Cells["c_date"].Value = day;
            //            dgv_aptschedule.Rows[i].Cells["c_vtime"].Value = time;
            //            dgv_aptschedule.Rows[i].Cells["c_dname"].Value = dtdctr.Rows[i]["doctor_name"].ToString();
            //            dgv_aptschedule.Rows[i].Cells["c_cond"].Value = dtdctr.Rows[i]["plan_new_procedure"].ToString();
            //            i++;
            //        }
            //        //SetFontAndColors();
            //    }
            //}
        }
        public void count_actv_dr( DataTable dctr_tdy)
        {
            if(dctr_tdy.Rows.Count>0)
            {
                label9.Text = dctr_tdy.Rows.Count.ToString();
            }
        }

        public void apt_count(DataTable dtb)
        {
            if (dtb.Rows.Count > 0)
            {
                labelapt_count.Text = dtb.Rows.Count.ToString();
            }
        }
        public void pat_count(DataTable dtp1)
        {
            if (dtp1.Rows.Count > 0)
            {
                label11.Text = dtp1.Rows.Count.ToString();
            }
        }

        public void procedure_count(DataTable dtp_count)
        {
            if (dtp_count.Rows.Count > 0)
            {
                labelprc_count.Text = dtp_count.Rows.Count.ToString();
            }
        }

        public void reciept_amnt(DataTable rec_amt)
        {
            decimal total = 0;
            if (rec_amt.Rows.Count > 0)
            {
                if(rec_amt.Rows[0][0].ToString()!="")
                {
                    total = Convert.ToDecimal(rec_amt.Rows[0][0].ToString()) - Convert.ToDecimal(rec_amt.Rows[0][1].ToString());
                    labeltotal_rec_count.Text = total.ToString();
                }
               
            }
        }
        public void inv_sale_amnt(DataTable sale_amt)
        {
            if (sale_amt.Rows.Count > 0)
            {
              label21.Text = sale_amt.Rows[0][0].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form2 = new Add_New_Patients();
            form2.ShowDialog();
            form2.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var form2 = new Add_Appointment();
            form2.ShowDialog();
            form2.Dispose();
        }

       
        private void btn_book_appointments_Click(object sender, EventArgs e)
        {
            bool cal_flg = true;
            var form2 = new AppointmentBooking(cal_flg);//   Main_Calendar();
            form2.doctor_id = doctor_id;
            form2.dashboard_flag = true;
            form2.ShowDialog();
            //bhj.......
            DateTime startDateTime = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
            DateTime startDateTime1 = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
            DataTable dtb = this.cntrl.get_all_aponmt(startDateTime, startDateTime1);
            DataTable dt1 = this.cntrl.get_all_dctrtoday();//   get_tdy_dctr(startDateTime, startDateTime1);
            if (dtb.Rows.Count > 0)
            {
                today_app_shedl(dtb);
                apt_count(dtb);
                pat_count(dtb);
                count_actv_dr(dt1);
            }

            DataTable dtp_count = this.cntrl.procedure_count(startDateTime, startDateTime1);
            DataTable dtp2 = this.cntrl.procedure(startDateTime, startDateTime1);
            if (dtp2.Rows.Count > 0)
            {
                procedure_count(dtp_count);
                fill_proc(dtp2);
            }
            //......bhj
            form2.Dispose();
        }
        public bool flag = false;
        private void dctrcombo_onSelectedIndexChanged(object sender, EventArgs e)
        {
            if(flag==false)
            {
                DateTime startDateTime = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
                DateTime startDateTime1 = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
                if (dctrcombo.SelectedIndex == 0)
                {
                    dgv_aptschedule.Rows.Clear();
                    //dgv_doctor_app.Rows.Clear();
                    //dgv_proced_count.Rows.Clear();
                    DataTable dtb = this.cntrl.get_all_aponmt(startDateTime, startDateTime1);
                    today_app_shedl(dtb);
                    //DataTable dtp2 = this.cntrl.procedure(startDateTime, startDateTime1);
                    //fill_proc(dtp2);
                    //DataTable dt = this.cntrl.get_all_dctrtoday();
                    //all_dctr(dt);
                }
                else if (dctrcombo.SelectedIndex == 1)
                {
                    dgv_aptschedule.Rows.Clear();
                    //dgv_doctor_app.Rows.Clear();
                    //dgv_proced_count.Rows.Clear();
                    DataTable dtb = this.cntrl.weeklyappointcount();
                    today_app_shedl(dtb);
                    //DataTable dtp2_week = this.cntrl.weeklyprocedure();
                    //fill_proc_weekly(dtp2_week);
                    //DataTable dt_week = this.cntrl.weeklydctr();
                    //all_dctr_week(dt_week);
                }
                else if (dctrcombo.SelectedIndex == 2)
                {
                    dgv_aptschedule.Rows.Clear();
                    //dgv_doctor_app.Rows.Clear();
                    //dgv_proced_count.Rows.Clear();
                    DataTable dtb = this.cntrl.Monthlyappointcount();
                    today_app_shedl(dtb);
                    //DataTable dtp2_month = this.cntrl.monthlyprocedure();
                    //fill_proc_monthly(dtp2_month);
                    //DataTable dt_monthly = this.cntrl.monthlydctr();
                    //all_dctr_monthly(dt_monthly);
                }
            }
          
        }
        //public void fill_proc_weekly(DataTable dtp2_week)
        //{
        //    if (dtp2_week.Rows.Count > 0)
        //    {
        //        dgv_proced_count.Rows.Clear();
        //        int i = 0;
        //        int k = 0;

        //        foreach (DataRow dr in dtp2_week.Rows)
        //        {
        //            if (dtp2_week.Rows[i]["name"].ToString() != "")
        //            {
        //                string prcdr = dtp2_week.Rows[i]["name"].ToString();
        //                dgv_proced_count.Rows.Add();
        //                //  string prcdr = dtp2_week.Rows[i]["name"].ToString();//plan_new_procedure
        //                dgv_proced_count.Rows[k].Cells[0].Value = prcdr;
        //                DateTime startDateTime = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
        //                DateTime startDateTime1 = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
        //                DataTable pro_count = this.cntrl.weekly_procedure_count(prcdr);
        //                pcount = pro_count.Rows.Count;
        //                dgv_proced_count.Rows[k].Cells[1].Value = pcount;
        //                k = k + 1;
        //            }
        //            i++;
        //        }
        //        //this.dgv_procedure.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Regular);
        //        //this.dgv_procedure.DefaultCellStyle.ForeColor = Color.DimGray;
        //        //this.dgv_procedure.DefaultCellStyle.SelectionForeColor = Color.Black;
        //        //this.dgv_procedure.DefaultCellStyle.SelectionBackColor = Color.White;
        //        //this.dgv_procedure.RowsDefaultCellStyle.BackColor = Color.White;
        //        //this.dgv_procedure.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //        //this.dgv_procedure.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        //        //this.dgv_procedure.GridColor = Color.LightGray;
        //    }
        //}
        //public void all_dctr_week(DataTable dt_week)
        //{
        //    if (dt_week.Rows.Count > 0)
        //    {
        //        dgv_doctor_app.Rows.Clear();
        //        int i = 0;
        //        foreach (DataRow dr in dt_week.Rows)
        //        {
        //            dgv_doctor_app.Rows.Add();
        //            string name = dt_week.Rows[i]["doctor_name"].ToString();
        //            dgv_doctor_app.Rows[i].Cells[0].Value = name;
        //            DataTable dtdctr = this.cntrl.get_specific_doctr_weekly(name);
        //            pat_count1 = dtdctr.Rows.Count;
        //            dgv_doctor_app.Rows[i].Cells[1].Value = pat_count1;
        //            i++;
        //            //SetFontAndColors();
        //        }
        //    }

        //}
        //public void all_dctr_monthly(DataTable dt_monthly)
        //{
        //    if (dt_monthly.Rows.Count > 0)
        //    {
        //        dgv_doctor_app.Rows.Clear();
        //        int i = 0;
        //        foreach (DataRow dr in dt_monthly.Rows)
        //        {
        //            dgv_doctor_app.Rows.Add();
        //            string name = dt_monthly.Rows[i]["doctor_name"].ToString();
        //            dgv_doctor_app.Rows[i].Cells[0].Value = name; 
        //            DataTable dtdctr = this.cntrl.get_specific_doctr_monthly(name);
        //            pat_count1 = dtdctr.Rows.Count;
        //            dgv_doctor_app.Rows[i].Cells[1].Value = pat_count1;
        //            i++;
        //            //SetFontAndColors();
        //        }
        //    }

        //}
        //public void fill_proc_monthly(DataTable dtp2_month)
        //{
        //    if (dtp2_month.Rows.Count > 0)
        //    {
        //        dgv_proced_count.Rows.Clear();
        //        int i = 0;
        //        int k = 0;
        //        foreach (DataRow dr in dtp2_month.Rows)
        //        {
        //            if (dtp2_month.Rows[i]["name"].ToString() != "")
        //            {
        //                string prcdr = dtp2_month.Rows[i]["name"].ToString();
        //                dgv_proced_count.Rows.Add();

        //                dgv_proced_count.Rows[k].Cells[0].Value = prcdr;
        //                DateTime startDateTime = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
        //                DateTime startDateTime1 = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
        //                DataTable pro_count = this.cntrl.monthly_procedure_count(prcdr);
        //                pcount = pro_count.Rows.Count;
        //                dgv_proced_count.Rows[k].Cells[1].Value = pcount;
        //                k = k + 1;
        //            }
        //            i++;
        //            //this.dgv_procedure.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Regular);
        //            //this.dgv_procedure.DefaultCellStyle.ForeColor = Color.DimGray;
        //            //this.dgv_procedure.DefaultCellStyle.SelectionForeColor = Color.Black;
        //            //this.dgv_procedure.DefaultCellStyle.SelectionBackColor = Color.White;
        //            //this.dgv_procedure.RowsDefaultCellStyle.BackColor = Color.White;
        //            //this.dgv_procedure.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //            //this.dgv_procedure.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        //            //this.dgv_procedure.GridColor = Color.LightGray;
        //        }
        //    }
        //}

       
        private void userControl_label3_Click(object sender, EventArgs e)
        {
            userControl_label3.BackColor = Color.White;
            userControl_label1.BackColor = Color.FromArgb(204, 235, 255);
            //userControl_label6.BackColor = Color.FromArgb(204, 235, 255);
            //userControl_label2.BackColor = Color.FromArgb(204, 235, 255);
            //dgv_doctors.Rows.Clear();
            DataTable dt = this.cntrl.get_all_dctrtoday();
            all_dctr(dt);
        }

        private void userControl_label1_Click(object sender, EventArgs e)
        {
            userControl_label1.BackColor = Color.White;
            userControl_label3.BackColor = Color.FromArgb(204, 235, 255);
            //userControl_label6.BackColor = Color.FromArgb(204, 235, 255);
            //userControl_label2.BackColor = Color.FromArgb(204, 235, 255);
            //dgv_doctors.Rows.Clear();
            DateTime startDateTime = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
            DateTime startDateTime1 = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
            DataTable dt = this.cntrl.get_tdy_dctr(startDateTime, startDateTime1);//tdy dctr
            all_dctr(dt);

        }

        private void userControl_label6_Click(object sender, EventArgs e)
        {
            userControl_label6.BackColor = Color.White;
            userControl_label2.BackColor = Color.FromArgb(204, 235, 255);
            //userControl_label1.BackColor = Color.FromArgb(204, 235, 255);
            //userControl_label3.BackColor = Color.FromArgb(204, 235, 255);
            //dgv_proced_count.Rows.Clear();
            DataTable dprcdr = this.cntrl.procedure();
            all_proc(dprcdr);
        }

        private void userControl_label2_Click(object sender, EventArgs e)
        {
            userControl_label2.BackColor = Color.White;
            userControl_label6.BackColor = Color.FromArgb(204, 235, 255);
            //userControl_label1.BackColor = Color.FromArgb(204, 235, 255);
            //userControl_label3.BackColor = Color.FromArgb(204, 235, 255);
            //dgv_proced_count.Rows.Clear();
            DateTime startDateTime = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
            DateTime startDateTime1 = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
            DataTable dtp2 = this.cntrl.procedure(startDateTime, startDateTime1);
            fill_proc(dtp2);
        }

        private void label12_Click(object sender, EventArgs e)
        {
            //var frm = new Patients();
            //frm.ShowDialog();
            //frm.Dispose();
        }
        public void openform(Form myForm)
        {
            Panel p = this.Parent as Panel;
            if (p != null)
            {
                myForm.FormBorderStyle = FormBorderStyle.None;
                myForm.TopLevel = false;
                myForm.Dock = DockStyle.Fill;
                p.Controls.Add(myForm);

                myForm.Show();
                this.Close();
               
            }
        }
        private void label20_Click(object sender, EventArgs e)
        {
            //var frm = new lab_notification();
            //frm.ShowDialog();
            //frm.Dispose();
        }

        private void userControl_label1_Load(object sender, EventArgs e)
        {

        }

        private void userControl_label6_Load(object sender, EventArgs e)
        {

        }

        private void userControl_label3_Load(object sender, EventArgs e)
        {

        }

        private void dgv_doctor_app_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string name = dgv_doctor_app.CurrentRow.Cells[0].Value.ToString();
                DateTime startDateTime = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 00:01");
                DateTime startDateTime1 = Convert.ToDateTime(dtp.Value.ToString("yyyy-MM-dd") + " 23:59");
                DataTable dtdctr = this.cntrl.get_specific_doctr(name, startDateTime, startDateTime1);
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
                    //SetFontAndColors();
                }
            }
        }

        private void dgv_aptschedule_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if(e.RowIndex>=0)
            //{
            //    if(dgv_aptschedule.CurrentCell.OwningColumn.Name== "c_name")
            //    {
            //        var frm = new Patient_Profile_Details();
            //        frm.patient_id = dgv_aptschedule.CurrentRow.Cells["id"].Value.ToString();
            //        frm.doctor_id = doctor_id;
            //        frm.ShowDialog();
            //        frm.Dispose();
            //    }
            //}
        }


        private void SetFontAndColors()
        {
           // this.dgv_doctors.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Regular);
           // this.dgv_doctors.DefaultCellStyle.ForeColor = Color.DimGray;
           // this.dgv_doctors.DefaultCellStyle.SelectionForeColor = Color.Gray;
           // this.dgv_doctors.DefaultCellStyle.SelectionBackColor = Color.White;
           // this.dgv_doctors.RowsDefaultCellStyle.BackColor = Color.White;
           // this.dgv_doctors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
           //this.dgv_doctors.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
           // this.dgv_doctors.GridColor = Color.LightGray;
           // this.dgv_aptschedule.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
           // this.dgv_aptschedule.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
           // this.dgv_aptschedule.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Regular);
           // this.dgv_aptschedule.DefaultCellStyle.ForeColor = Color.DimGray;
           // this.dgv_aptschedule.DefaultCellStyle.SelectionForeColor = Color.Gray;//black
           // this.dgv_aptschedule.DefaultCellStyle.SelectionBackColor = Color.White;
           // this.dgv_aptschedule.RowsDefaultCellStyle.BackColor = Color.White;
           // this.dgv_aptschedule.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
           // this.dgv_aptschedule.GridColor = Color.LightGray;//WhiteSmoke
           // this.dgv_aptschedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
           // this.dgv_aptschedule.ScrollBars = ScrollBars.Vertical;
        }

      
    }
}
