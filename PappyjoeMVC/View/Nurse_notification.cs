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
    public partial class Nurse_notification : Form
    {
        Add_Nurses_Notes_controller cntrl = new Add_Nurses_Notes_controller();
        public DataTable dt_tonurse = new DataTable();
        public Nurse_notification()
        {
            InitializeComponent();
        }

        private void Nurse_notification_Load(object sender, EventArgs e)
        {
            load_grid(DateTime.Now.Date.ToString("yyyy-MM-dd"));
          
        }
        public void load_grid(string date)
        {
            //DataTable dt_noti = this.cntrl.get_patient_notification(date);
            DataTable dt_noti = this.cntrl.get_patient_notification(date);
            if (dt_noti.Rows.Count > 0)
            {
                DGV_notify.Rows.Clear();
                int k = 1;
                int row = 0;
                for (int i = 0; i < dt_noti.Rows.Count; i++)
                {
                    dt_tonurse = this.cntrl.get_tonurse_treatment(dt_noti.Rows[i]["mainid"].ToString());
                    if (dt_tonurse.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt_tonurse.Rows)
                        {//Tonurse_paid
                            DataTable dt_status = this.cntrl.get_nurse_note_status(dt_noti.Rows[i]["pt_id"].ToString(), Convert.ToDateTime(dt_noti.Rows[i]["date"].ToString()).ToString("yyyy-MM-dd"), dr["procedure_id"].ToString());
                            DataTable dt_getinvoice = this.cntrl.get_invoiceid(dt_noti.Rows[i]["pt_id"].ToString(), Convert.ToDateTime(dt_noti.Rows[i]["date"].ToString()).ToString("yyyy-MM-dd"), dr["procedure_id"].ToString());

                            DGV_notify.Rows.Add();
                            DGV_notify.Rows[row].Cells["slno"].Value = k;
                            DGV_notify.Rows[row].Cells["ptid"].Value = dt_noti.Rows[i]["patientid"].ToString();
                            DGV_notify.Rows[row].Cells["name"].Value = dt_noti.Rows[i]["pt_name"].ToString();
                            DGV_notify.Rows[row].Cells["date"].Value = Convert.ToDateTime(dt_noti.Rows[i]["date"].ToString()).ToString("dd-MM-yyyy");
                            DGV_notify.Rows[row].Cells["phone"].Value = dt_noti.Rows[i]["primary_mobile_number"].ToString();
                            DGV_notify.Rows[row].Cells["Treatment"].Value = dr["procedure_name"].ToString();//treat_id
                            DGV_notify.Rows[row].Cells["treat_id"].Value = dr["procedure_id"].ToString();//
                            if (dt_status.Rows.Count > 0)
                            {
                                DGV_notify.Rows[row].Cells["status"].Value = "Completed";
                                DGV_notify.Rows[row].Cells["nid"].Value = dt_status.Rows[0]["id"].ToString();//bhj
                            }
                            else
                            { 
                                DGV_notify.Rows[row].Cells["status"].Value = "";
                                DGV_notify.Rows[row].Cells["nid"].Value = "";//bhj
                            }


                            DGV_notify.Rows[row].Cells["pt_id"].Value = dt_noti.Rows[i]["pt_id"].ToString();
                            DGV_notify.Rows[row].Cells["plan_id"].Value = dt_noti.Rows[i]["mainid"].ToString();
                            DGV_notify.Rows[row].Cells["dr_name"].Value = dt_noti.Rows[i]["dr_name"].ToString();
                            DGV_notify.Rows[row].Cells["dr_id"].Value = dt_noti.Rows[i]["dr_id"].ToString();//bh1
                            if (dt_getinvoice.Rows.Count > 0)
                            {
                                if (dt_getinvoice.Rows[0]["Tonurse_paid"].ToString() == "True")
                                {
                                    if (dt_getinvoice.Rows[0]["status"].ToString() == "0")
                                    {
                                        DGV_notify.Rows[row].Cells["payment"].Value = "Paid";
                                    }
                                    else
                                    {
                                        DGV_notify.Rows[row].Cells["payment"].Value = "Not Paid";
                                    }
                                }
                                else
                                {
                                    DGV_notify.Rows[row].Cells["payment"].Value = "Not Paid";
                                }
                            }
                            else
                            {
                                DGV_notify.Rows[row].Cells["payment"].Value = "Not Paid";
                            }
                            DGV_notify.Rows[row].Cells["add"].Value = PappyjoeMVC.Properties.Resources.add__1_;
                            DGV_notify.Rows[row].Cells["edit"].Value = PappyjoeMVC.Properties.Resources.editicon;
                            k++; row++;
                        }
                    }
                }
            }
            //if (dt_noti.Rows.Count > 0)
            //{
            //    DGV_notify.Rows.Clear();
            //    int k = 1;
            //    int row = 0;
            //    for (int i = 0; i < dt_noti.Rows.Count; i++)
            //    {
            //        dt_tonurse = this.cntrl.get_tonurse_treatment(dt_noti.Rows[i]["mainid"].ToString());
            //        if (dt_tonurse.Rows.Count > 0)
            //        {
            //            DataTable dt_getinvoice = new DataTable();
            //            //dt_procedure = dt_tonurse;
            //            DataTable dt_invoice = this.cntrl.get_invno(dt_noti.Rows[i]["pt_id"].ToString(), Convert.ToDateTime(dt_noti.Rows[i]["date"].ToString()).ToString("yyyy-MM-dd"), dt_tonurse.Rows[0]["procedure_id"].ToString());
            //            if(dt_invoice.Rows.Count>0)
            //            {
            //                 dt_getinvoice = this.cntrl.get_invoiceid(dt_noti.Rows[i]["pt_id"].ToString(), Convert.ToDateTime(dt_noti.Rows[i]["date"].ToString()).ToString("yyyy-MM-dd"), dt_invoice.Rows[0]["invoice_no"].ToString());
            //            }
            //            DataTable dt_status = this.cntrl.get_nurse_note_status(dt_noti.Rows[i]["pt_id"].ToString(),Convert.ToDateTime( dt_noti.Rows[i]["date"].ToString()).ToString("yyyy-MM-dd"), dt_noti.Rows[i]["mainid"].ToString());

            //            DGV_notify.Rows.Add();
            //            DGV_notify.Rows[row].Cells["slno"].Value = k;
            //            DGV_notify.Rows[row].Cells["ptid"].Value = dt_noti.Rows[i]["patientid"].ToString();
            //            DGV_notify.Rows[row].Cells["name"].Value = dt_noti.Rows[i]["pt_name"].ToString();
            //            DGV_notify.Rows[row].Cells["date"].Value = Convert.ToDateTime(dt_noti.Rows[i]["date"].ToString()).ToString("dd-MM-yyyy");
            //            DGV_notify.Rows[row].Cells["phone"].Value = dt_noti.Rows[i]["primary_mobile_number"].ToString();
            //            if(dt_status.Rows.Count>0)
            //            {
            //                DGV_notify.Rows[row].Cells["status"].Value = "Completed";
            //            }
            //            else
            //                DGV_notify.Rows[row].Cells["status"].Value = "";

            //            DGV_notify.Rows[row].Cells["pt_id"].Value = dt_noti.Rows[i]["pt_id"].ToString();
            //            DGV_notify.Rows[row].Cells["plan_id"].Value = dt_noti.Rows[i]["mainid"].ToString();
            //            DGV_notify.Rows[row].Cells["dr_name"].Value = dt_noti.Rows[i]["dr_name"].ToString();
            //            if (dt_getinvoice.Rows.Count > 0)
            //            {
            //                if (dt_getinvoice.Rows[0]["status"].ToString() == "0")
            //                {
            //                    DGV_notify.Rows[row].Cells["payment"].Value = "Paid";

            //                }
            //                else
            //                {
            //                    DGV_notify.Rows[row].Cells["payment"].Value = "Not Paid";
            //                }
            //            }
            //            else
            //            {
            //                DGV_notify.Rows[row].Cells["payment"].Value = "Not Paid";
            //            }
            //            DGV_notify.Rows[row].Cells["add"].Value = PappyjoeMVC.Properties.Resources.add__1_;
            //            DGV_notify.Rows[row].Cells["edit"].Value = PappyjoeMVC.Properties.Resources.editicon;
            //            k++; row++;

            //        }
            //    }
            //}
        }
        public Boolean flag_notify = false;
        public int nurse_note_id = 0;
        private void DGV_notify_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            {
                string plan_id = "", patient_id, Pt_name, doctor = "", pid = "", date = "", procedurename = "", pro_id = "";
                if (DGV_notify.CurrentCell.OwningColumn.Name == "add")
                {
                    plan_id = DGV_notify.CurrentRow.Cells["plan_id"].Value.ToString();
                    patient_id = DGV_notify.CurrentRow.Cells["pt_id"].Value.ToString();
                    pid = DGV_notify.CurrentRow.Cells["ptid"].Value.ToString();
                    Pt_name = DGV_notify.CurrentRow.Cells["name"].Value.ToString();
                    doctor = DGV_notify.CurrentRow.Cells["dr_name"].Value.ToString();
                    date = DGV_notify.CurrentRow.Cells["date"].Value.ToString();
                    procedurename = DGV_notify.CurrentRow.Cells["Treatment"].Value.ToString();
                    pro_id = DGV_notify.CurrentRow.Cells["treat_id"].Value.ToString();
                    var form2 = new Add_Nurses_Notes();
                    form2.plan_id = plan_id;
                    form2.patient_id = patient_id;
                    form2.Pt_name = Pt_name;
                    form2.doctor = doctor;
                    form2.pid = pid;
                    form2.date = date;
                    form2.dt_tonurse = dt_tonurse;
                    form2.procedurename = procedurename;
                    form2.pro_id = pro_id;
                    form2.ShowDialog();
                    // load_grid(Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
                    load_grid(dateTimePicker3.Value.ToString("yyyy-MM-dd"));//bhj
                }
                if (DGV_notify.CurrentCell.OwningColumn.Name == "edit")//bhj
                {
                    flag_notify = false;
                    plan_id = DGV_notify.CurrentRow.Cells["plan_id"].Value.ToString();
                    patient_id = DGV_notify.CurrentRow.Cells["pt_id"].Value.ToString();
                    pid = DGV_notify.CurrentRow.Cells["ptid"].Value.ToString();
                    Pt_name = DGV_notify.CurrentRow.Cells["name"].Value.ToString();
                    doctor = DGV_notify.CurrentRow.Cells["dr_name"].Value.ToString();
                    date = DGV_notify.CurrentRow.Cells["date"].Value.ToString();
                    // DateTime daytime = Convert.ToDateTime(dtb.Rows[i]["start_datetime"].ToString());

                    //string time = daytime.ToString("hh:mm tt");
                    procedurename = DGV_notify.CurrentRow.Cells["Treatment"].Value.ToString();
                    pro_id = DGV_notify.CurrentRow.Cells["treat_id"].Value.ToString();
                    string dr_id = DGV_notify.CurrentRow.Cells["dr_id"].Value.ToString();
                    string nnid = DGV_notify.CurrentRow.Cells["nid"].Value.ToString();
                    nurse_note_id = int.Parse(nnid);
                    var frm = new Add_Nurses_Notes();
                    frm.nurse_note_id = nurse_note_id;
                    frm.plan_id = plan_id;
                    frm.patient_id = patient_id;
                    frm.procedurename = procedurename;
                    frm.pro_id = pro_id;
                    frm.Pt_name = Pt_name;
                    frm.pid = pid;
                    frm.doctor = doctor;
                    frm.date = date;
                    frm.dr_id = dr_id;
                    frm.flag_notify = true;
                    frm.ShowDialog();
                    // load_grid(Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
                    load_grid(dateTimePicker3.Value.ToString("yyyy-MM-dd"));

                }
            }
        }

        private void btnshow_Click(object sender, EventArgs e)
        {
            load_grid(dateTimePicker3.Value.ToString("yyyy-MM-dd"));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
