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


namespace PappyjoeMVC.View
{
    public partial class NursesDashboard : Form
    {
        public NursesDashboard()
        {
            InitializeComponent();
        }
        public string doctor_id;
        public string id, pid;
        NursesDashboard_controller cntrl = new NursesDashboard_controller();
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void NursesDashboard_Load(object sender, EventArgs e)
        {
            DateTime startdate = Convert.ToDateTime(dtp_nurse.Value.ToString("yyyy-MM-dd") + " 00:01");
            DateTime enddate = Convert.ToDateTime(dtp_nurse.Value.ToString("yyyy-MM-dd") + " 23:59");
            //DataTable dtn = this.cntrl.get_nnotify_count();
            DataTable dtn = this.cntrl.total_tonurse_notify(startdate,enddate);
            DataTable nnote = this.cntrl.cmplt_nnotification(startdate,enddate);
            load_nurse_grid(startdate,enddate);
            nncount(dtn);
            completed_nurse_notify(nnote);
            load_lab_grid(startdate, enddate);
            //DataTable dt_getinvoice = this.cntrl.get_invoiceid(startdate, enddate);
            //lab_ttcount(dt_getinvoice);

            //lab
            DataTable dtl = this.cntrl.get_lab_notify(startdate, enddate);
            lab_ttcount(dtl);
            DataTable pending_lab = this.cntrl.pending_lab_notify(startdate, enddate);
            pendingLab_count(pending_lab);
            DataTable completed_lab = this.cntrl.completed_lab_notify(startdate, enddate);
            CompletedLab_count(completed_lab);





        }
        public void load_nurse_grid(DateTime startdate,DateTime enddate)
        {
            DataTable tonurse = this.cntrl.get_nnotify(startdate, enddate);
            if(tonurse.Rows.Count>0)
            {
                dgvnurse.Rows.Clear();
                for (int i = 0; i < tonurse.Rows.Count; i++)
                {
                    dgvnurse.Rows.Add();
                    dgvnurse.Rows[i].Cells["cid"].Value = tonurse.Rows[i]["pid"].ToString();
                    dgvnurse.Rows[i].Cells["cname"].Value = tonurse.Rows[i]["pt_name"].ToString();
                    dgvnurse.Rows[i].Cells["cdname"].Value = tonurse.Rows[i]["dr_name"].ToString();
                    dgvnurse.Rows[i].Cells["ctreatment"].Value = tonurse.Rows[i]["procedure_name"].ToString();
                    DataTable nstatus = this.cntrl.get_nstatus(tonurse.Rows[i]["pid"].ToString(), tonurse.Rows[i]["procedure_id"].ToString(), startdate, enddate);
                    if (nstatus.Rows.Count > 0)
                    {
                        if (nstatus.Rows[0][0].ToString() == "Completed")
                        {
                            dgvnurse.Rows[i].Cells["cstatus"].Value = "completed";
                        }
                        else
                        {
                            dgvnurse.Rows[i].Cells["cstatus"].Value = "pending";
                        }
                       
                    }
                    dgvnurse.Rows[i].Cells["cstatus"].Value = "pending";
                }
            }

        }
        public void nncount(DataTable dtn)
        {


            if (dtn.Rows.Count > 0)
            {
                ttlnursenote.Text = dtn.Rows.Count.ToString();
            }


        }
        public void completed_nurse_notify(DataTable nnote)
        {
            if(nnote.Rows.Count>0)
            {
                nursecompltd.Text = nnote.Rows.Count.ToString();
            }
        }
        public void lab_ttcount(DataTable dtl)
        {
            if (dtl.Rows.Count > 0)
            {
                totallabnote.Text = dtl.Rows.Count.ToString();
            }
        }


        public void load_lab_grid(DateTime startdate, DateTime enddate)
        {
            DataTable dtl = this.cntrl.get_lab_notify(startdate,enddate);
            if (dtl.Rows.Count > 0)
            {
                dgvlab.Rows.Clear();

                for (int i = 0; i < dtl.Rows.Count; i++)
                {

                    dgvlab.Rows.Add();
                    dgvlab.Rows[i].Cells["c_pid"].Value = dtl.Rows[i]["pt_id"].ToString();
                    dgvlab.Rows[i].Cells["c_name"].Value = dtl.Rows[i]["pt_name"].ToString();
                   // dgvlab.Rows[i].Cells["c_prcd"].Value =
                     //string prcdr=dtl.Rows[i]["work_id"].ToString();
                     string name=dtl.Rows[i]["doctor_name"].ToString();
                    dgvlab.Rows[i].Cells["c_dname"].Value = name;//dtl.Rows[i]["dr_id"].ToString();
                 
                    if(dtl.Rows[i]["status"].ToString()=="Completed")
                        {
                        dgvlab.Rows[i].Cells["c_status"].Value = "Completed";
                    }
                    else
                        dgvlab.Rows[i].Cells["c_status"].Value = "Pending";
                }
                            




                
            }
        }

        public void pendingLab_count(DataTable pendingLab)
        {
            if (pendingLab.Rows.Count > 0)
            {
                labpending.Text = pendingLab.Rows.Count.ToString();
            }
        }
        public void CompletedLab_count(DataTable pendingLab)
        {
            if (pendingLab.Rows.Count > 0)
            {
                labcompld.Text = pendingLab.Rows.Count.ToString();
            }
        }



        private void label4_Click(object sender, EventArgs e)
            {

            }
        

       

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void nursepend_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            var frm = new Add_Nurses_Notes();

            frm.ShowDialog();
            frm.Dispose();
        }
    }
}
