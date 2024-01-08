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
    public partial class lab_notification : Form
    {
        public DataTable dt_tonurse = new DataTable();
        LabWorks_controller cntrl = new LabWorks_controller();
        public lab_notification()
        {
            InitializeComponent();
        }

        private void lab_notification_Load(object sender, EventArgs e)
        {
            load_grid(DateTime.Now.Date.ToString("yyyy-MM-dd"));
        }

        public void load_grid(string date)
        {
            DataTable dt_noti = this.cntrl.get_patient_notification(date);
            if(dt_noti.Rows.Count>0)
            {

                DGV_notify.Rows.Clear();
                int k = 1;
                for (int i = 0; i < dt_noti.Rows.Count; i++)
                {
                    //dt_tonurse = this.cntrl.get_tonurse_treatment(dt_noti.Rows[i]["mainid"].ToString());
                    //if (dt_tonurse.Rows.Count > 0)
                    //{
                        //dt_procedure = dt_tonurse;
                        //DataTable dt_getinvoice = this.cntrl.get_invoiceid(dt_noti.Rows[i]["pt_id"].ToString(), Convert.ToDateTime(dt_noti.Rows[i]["date"].ToString()).ToString("yyyy-MM-dd"));
                        //DataTable dt_status = this.cntrl.get_nurse_note_status(dt_noti.Rows[i]["pt_id"].ToString(), Convert.ToDateTime(dt_noti.Rows[i]["date"].ToString()).ToString("yyyy-MM-dd"));
                        DataTable dt_doctor = this.cntrl.docname(dt_noti.Rows[i]["dr_id"].ToString());
                        DGV_notify.Rows.Add();
                        DGV_notify.Rows[i].Cells["slno"].Value = k;
                        DGV_notify.Rows[i].Cells["ptid"].Value = dt_noti.Rows[i]["patientid"].ToString();
                        DGV_notify.Rows[i].Cells["name"].Value = dt_noti.Rows[i]["pt_name"].ToString();
                        DGV_notify.Rows[i].Cells["date"].Value = Convert.ToDateTime(dt_noti.Rows[i]["date"].ToString()).ToString("dd-MM-yyyy");
                        DGV_notify.Rows[i].Cells["phone"].Value = dt_noti.Rows[i]["primary_mobile_number"].ToString();
                        //if (dt_status.Rows.Count > 0)
                        //{
                        //    DGV_notify.Rows[i].Cells["status"].Value = "Completed";
                        //}
                        //else
                        //    DGV_notify.Rows[i].Cells["status"].Value = "";

                        DGV_notify.Rows[i].Cells["pt_id"].Value = dt_noti.Rows[i]["pt_id"].ToString();
                        DGV_notify.Rows[i].Cells["plan_id"].Value = dt_noti.Rows[i]["work_id"].ToString();
                        DGV_notify.Rows[i].Cells["dr_name"].Value = dt_doctor.Rows[i]["doctor_name"].ToString();
                        //if (dt_getinvoice.Rows.Count > 0)
                        //{
                        //    if (dt_getinvoice.Rows[0]["status"].ToString() == "0")
                        //    {
                        //        DGV_notify.Rows[i].Cells["payment"].Value = "Paid";

                        //    }
                        //    else
                        //    {
                        //        DGV_notify.Rows[i].Cells["payment"].Value = "Not Paid";
                        //    }
                        //}
                        //else
                        //{
                        //    DGV_notify.Rows[i].Cells["payment"].Value = "Not Paid";
                        //}
                        DGV_notify.Rows[i].Cells["add"].Value = PappyjoeMVC.Properties.Resources.add__1_;
                        DGV_notify.Rows[i].Cells["edit"].Value = PappyjoeMVC.Properties.Resources.editicon;
                        k++;

                    //}
                }
            }

        }

        private void DGV_notify_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            {
                string plan_id = "", patient_id, Pt_name, doctor = "", pid = "", date = "";
                if (DGV_notify.CurrentCell.OwningColumn.Name == "add")
                {
                    plan_id = DGV_notify.CurrentRow.Cells["plan_id"].Value.ToString();
                    patient_id = DGV_notify.CurrentRow.Cells["pt_id"].Value.ToString();
                    pid = DGV_notify.CurrentRow.Cells["ptid"].Value.ToString();
                    Pt_name = DGV_notify.CurrentRow.Cells["name"].Value.ToString();
                    doctor = DGV_notify.CurrentRow.Cells["dr_name"].Value.ToString();
                    date = DGV_notify.CurrentRow.Cells["date"].Value.ToString();
                    var form2 = new LabWorks();
                    //form2.plan_id = plan_id;
                    form2.patient_id = patient_id;
                    //form2.Pt_name = Pt_name;
                    //form2.doctor = doctor;
                    //form2.pid = pid;
                    //form2.date = date;
                    //form2.dt_tonurse = dt_tonurse;
                    form2.ShowDialog();

                    ////var form2 = new Prescription_Show();
                    ////form2.doctor_id = doctor_id;
                    //form2.patient_id = patient_id;
                    //openform(form2);
                    load_grid(Convert.ToDateTime(date).ToString("yyyy-MM-dd"));

                }
            }
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
