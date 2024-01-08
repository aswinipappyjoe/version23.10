using PappyjoeMVC.Controller;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PappyjoeMVC.View
{
    public partial class Practice_Staff_Setting : Form
    {
        Staff_controller cntrl = new Staff_controller();
        public string idd = "";
        int status_pre = 0;
        DataTable dt_mail = new DataTable();
        public string doctor_id = "1"; string userid = "";
        public bool flag_mail = false; string id = "";
        public string status = "No";
        string calendrcolor = "0";
        public Practice_Staff_Setting()
        {
            InitializeComponent();
        }
        private void Practice_Staff_Setting_Load(object sender, EventArgs e)
        {
            try
            {
                tabControl1.Controls.Remove(tabPage3); tabControl1.Controls.Remove(tabPage2);
                if (chkPATadd.Checked == true && chkPATedit.Checked == true && chkPATdelete.Checked == true)
                {
                    chkPAT.Checked = true;
                }
                else
                {
                    chkPAT.Checked = false;
                }
                if (PappyjoeMVC.Model.Connection.MyGlobals.project_type.TrimEnd() == "Pharmacy")
                {
                    chkAPT.Visible = false;
                    chkAPTadd.Visible = false;
                    chkAPTedit.Visible = false;
                    chkAPTdelete.Visible = false;
                    chkAPTClinicAppoinment.Visible = false;
                    //chkPAT.Checked = false;
                    //chkPATadd.Checked = false;
                    //chkPATedit.Checked = false;
                    //chkPATdelete.Checked = false;
                    chkPMT.Visible = false;
                    chkPMTadd.Visible = false;
                    //chkCLMSadd.Checked = false;
                    //chkEMR.Visible = false;
                    chkEMRCF.Visible = false;
                    chkEMRF.Visible = false;
                    chkEMRFP.Visible = false;
                    chkEMRP.Visible = false;
                    chkEMRTP.Visible = false;
                    chkEMRCFadd.Visible = false;
                    chkEMRCFedit.Visible = false;
                    chkEMRCFdelete.Visible = false;
                    chkEMRFadd.Visible = false;
                    chkEMRI.Visible = false;
                    chkEMRIadd.Visible = false;
                    chkEMRIdelete.Visible = false;
                    chkEMRIedit.Visible = false;
                    chkEMRFPadd.Visible = false;
                    chkEMRFPdelete.Visible = false;
                    chkEMRPadd.Visible = false;
                    chkEMRPedit.Visible = false;
                    chkEMRPdelete.Visible = false;
                    chkEMRTPadd.Visible = false;
                    chkEMRTPedit.Visible = false;
                    chkEMRTPdelete.Visible = false;
                    //chkRPT.Visible = false;
                    chkRPTAPTadd.Visible = false;
                    //chkRPTDSadd.Checked = false;
                    chkRPTEMRadd.Visible = false;
                    //chkRPTEXPadd.Checked = false;
                    chkRPTINCadd.Visible = false;
                    chkRPTINVadd.Visible = false;
                    chkRPTIncom.Visible = false;
                    //chkRPTPATadd.Checked = false;
                    chkRPTPAYadd.Visible = false;
                    chkadjustment.Visible = false;
                    //chkINVAIadd.Checked = false;
                    //chkINVASadd.Checked = false;
                    //chkINVCSadd.Checked = false;
                    //chkINVPIadd.Checked = false;
                    chkstockledger.Visible = false;
                    //chkInvenSale.Checked = false;
                    //chkInventory.Checked = false;
                    chkConsltn.Visible = false;
                    //chkCommnctn.Checked = false;
                    //chkExpnse.Checked = false;
                    chkLabTrackng.Visible = false;
                    chkCalendar.Visible = false;
                    //chk_inv.Visible = false;

                    //chkSales.Checked = false;
                    //chkPurchase.Checked = false;
                    chkLabTrackng.Visible = false;
                    //chkadjustment.Checked = false;
                    //chkstockledger.C/*h*/ecked = false;
                    //chkstocktransfer.Checked = false;
                }
                else
                {
                    if (chkAPTadd.Checked == true && chkAPTedit.Checked == true && chkAPTdelete.Checked == true)
                    {
                        chkAPT.Checked = true;
                    }
                    else
                    {
                        chkAPT.Checked = false;
                    }
                    if (chkPMTadd.Checked == true)
                    {
                        chkPMT.Checked = true;
                    }
                    else
                    {
                        chkPMT.Checked = false;
                    }
                }

               
                
               
               
                label15.Enabled = true;
                txt_fee.Enabled = true;
                label16.Enabled = true;
                txt_followup_fee.Enabled = true;
                label19.Enabled = true;
                txt_period.Enabled = true;
                DataTable dtb = this.cntrl.Fill_StaffGrid();
                FillStaffGrid(dtb);
                panel_color.Hide();
                dataGridView_Staff.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
                dataGridView_Staff.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridView_Staff.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 9, FontStyle.Regular);
                dataGridView_Staff.EnableHeadersVisualStyles = false;
                foreach (DataGridViewColumn cl in dataGridView_Staff.Columns)
                {
                    cl.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView_notification.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
                dataGridView_notification.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridView_notification.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 9, FontStyle.Regular);
                dataGridView_notification.EnableHeadersVisualStyles = false;
                foreach (DataGridViewColumn cl in dataGridView_notification.Columns)
                {
                    cl.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView_users.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
                dataGridView_users.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridView_users.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 9, FontStyle.Regular);
                dataGridView_users.EnableHeadersVisualStyles = false;
                foreach (DataGridViewColumn cl in dataGridView_users.Columns)
                {
                    cl.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                DataTable d1 = this.cntrl.Get_DoctorId(doctor_id);
                if(d1.Rows.Count > 0)
                {
                    if (d1.Rows[0]["login_type"].ToString() == "admin" || d1.Rows[0]["login_type"].ToString() == "ADMIN")
                    {
                        button__manage_addoctor.Show();
                        status_pre = 1; tabPage4.Show();
                    }
                    else
                    {
                        //this.tabControl1.TabPages[4].Dispose();
                        tabPage4.Hide();
                        button__manage_addoctor.Hide();
                        status_pre = 0;
                    }
                }
                RefreshCheckboxes();
                cmbStaffType.SelectedIndex = 0;
                //staff availability
                //DataTable staff = this.cntrl.LoadStaff();
                //DGV_Staff.Visible = true;
                //if (staff.Rows.Count > 0)
                //{
                //    LoadStaff(staff);
                //    if (DGV_Staff.Rows.Count > 0)
                //    {
                //        DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn()
                //        {
                //            Name = "Make Unavailable"
                //        };
                //        DGV_Staff.Columns.Add(col);
                //        col.Width = 150;
                //        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //        //DataGridViewCheckBoxColumn check2 = new DataGridViewCheckBoxColumn()
                //        //{

                //        //};
                //        //DGV_Staff.Columns.Add(check2);
                //        //check2.Width = 50;
                //        //check2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //    }
                //    //int dt_row = 0;
                //    //foreach (DataGridViewRow row in DGV_Staff.Rows)
                //    //{
                //    //    if (staff.Rows[dt_row]["availability"].ToString().Trim() == "Unavailabile")
                //    //    {
                //    //        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[4];
                //    //        chk.Selected = true;
                //    //    }
                //    //    dt_row++;
                //    //}
                //    //foreach (DataGridViewColumn cl in DGV_Staff.Columns)
                //    //{
                //    //    cl.SortMode = DataGridViewColumnSortMode.NotSortable;
                //    //}
                //    for (int i = 0; i < staff.Rows.Count; i++)
                //    {
                //        DataGridViewRow row = new DataGridViewRow();
                //        row = DGV_Staff.Rows[i];
                //        string confirmsms = staff.Rows[i]["availability"].ToString();
                //        if (confirmsms == "Unavailabile")
                //        {
                //            row.Cells[4].Value = true;
                //        }
                //    }
                //    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void LoadStaff(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                int t1 = 0,k=1;
                DGV_Staff.Rows.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    DGV_Staff.Rows.Add();
                    DGV_Staff.Rows[t1].Cells[0].Value = dr["id"].ToString();
                    DGV_Staff.Rows[t1].Cells[1].Value =k;
                    DGV_Staff.Rows[t1].Cells[2].Value = dr["doctor_name"].ToString();
                    DGV_Staff.Rows[t1].Cells[3].Value = dr["login_type"].ToString();
                    DGV_Staff.Rows[t1].Cells[4].Value = dr["availability"].ToString();
                    t1++; k++;
                    DGV_Staff.RowsDefaultCellStyle.ForeColor = Color.Black;
                    DGV_Staff.RowsDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Regular);
                }
                DGV_Staff.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
                DGV_Staff.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                DGV_Staff.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                //DGV_Staff.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                //DGV_Staff.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                //DGV_Staff.Columns[2].DefaultCellStyle.Alignm/*e*/nt = DataGridViewContentAlignment.MiddleLeft;
                DGV_Staff.EnableHeadersVisualStyles = false;
            }
        }
        public void FillStaffGrid(DataTable dtb)
        {
            try
            {
                if (dtb.Rows.Count > 0)
                {
                    int i = 0;
                    dataGridView_Staff.Rows.Clear();
                    foreach (DataRow dr in dtb.Rows)
                    {
                        dataGridView_Staff.Rows.Add();
                        dataGridView_Staff.Rows[i].Cells["S_ID"].Value = dr["id"].ToString();
                        dataGridView_Staff.Rows[i].Cells["Doctor_Name"].Value = dr["doctor_name"].ToString();
                        dataGridView_Staff.Rows[i].Cells["Mobile_Number"].Value = dr["mobile_number"].ToString();
                        dataGridView_Staff.Rows[i].Cells["Role"].Value = dr["login_type"].ToString();
                        dataGridView_Staff.Rows[i].Cells["fee"].Value = dr["fee"].ToString();
                        dataGridView_Staff.Rows[i].Cells["followup_fee"].Value = dr["followup_fee"].ToString();
                        dataGridView_Staff.Rows[i].Cells["followup_period"].Value = dr["followup_period"].ToString();
                        dataGridView_Staff.Rows[i].Cells["Activated_Login"].Value = dr["activate_login"].ToString();
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView_Staff_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            {
                try
                {
                    id = dataGridView_Staff.Rows[e.RowIndex].Cells[0].Value.ToString();
                    if (dataGridView_Staff.CurrentCell.ColumnIndex == 4)
                    {
                        string Active_Login = dataGridView_Staff.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                        if (Active_Login == "Yes")
                        {
                            DialogResult res;
                            if (dataGridView_Staff.Rows[e.RowIndex].Cells["Role"].Value.ToString() == "doctor")
                            {
                                res = MessageBox.Show("Are you sure you want to Deactivate this Doctor?", "Deactivation confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            }
                            else
                            {
                                res = MessageBox.Show("Are you sure you want to Deactivate this Staff?", "Deactivation confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            }
                            if (res == DialogResult.Yes)
                            {
                                dataGridView_Staff.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "No";
                                this.cntrl.Update_Login(id);
                            }
                        }
                        if (Active_Login == "No")
                        {
                            DialogResult res;
                            if (dataGridView_Staff.Rows[e.RowIndex].Cells["Role"].Value.ToString() == "doctor")
                            {
                                res = MessageBox.Show("Are you sure you want to Activate this Doctor?", "Activation confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            }
                            else
                            {
                                res = MessageBox.Show("Are you sure you want to Activate this Staff?", "Activation confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            }
                            if (res == DialogResult.Yes)
                            {
                                dataGridView_Staff.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "Yes";
                                this.cntrl.update_loginstatus_Yes(id);
                            }
                        }
                    }
                    if (e.RowIndex >= 0)
                    {
                        if (dataGridView_Staff.CurrentCell.OwningColumn.Name == "Edit")
                        {
                            string type = dataGridView_Staff.Rows[e.RowIndex].Cells["Role"].Value.ToString();
                            //if (type.TrimEnd() != "admin" && type.TrimEnd() != "ADMIN" && type.TrimEnd() != "Admin")// if (type!="admin")
                            //{
                            id = dataGridView_Staff.Rows[e.RowIndex].Cells[0].Value.ToString();
                            var form2 = new doctors(type); // Doctor_Profile(type);
                            form2.doctor_id = doctor_id;
                            form2.doc = id;
                            form2.Show();
                            //}
                            //else
                            //{
                            //    MessageBox.Show("Cannot edit admin details !!", "Error!...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //}
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
           
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            DataTable dtb = this.cntrl.Fill_StaffGrid();
            FillStaffGrid(dtb);
        }
        //notification
        public void refresh()
        {
            button_refresh.Hide();
            DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn()
            {
                Name = "Confirm sms"
            };
            dataGridView_notification.Columns.Add(col);
            col.Width = 100;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCheckBoxColumn col1 = new DataGridViewCheckBoxColumn()
            {
                Name = "Schedule sms"
            };
            dataGridView_notification.Columns.Add(col1);
            col1.Width = 100;
            col1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewCheckBoxColumn col2 = new DataGridViewCheckBoxColumn()
            {
                Name = "Confirm email"
            };
            dataGridView_notification.Columns.Add(col2);
            col2.Width = 100;
            col2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewCheckBoxColumn col3 = new DataGridViewCheckBoxColumn()
            {
                Name = "Schedule email"
            };
            dataGridView_notification.Columns.Add(col3);
            col3.Width = 100;
            col3.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataTable dtb = this.cntrl.Get_DctrDetails();
            GetDctrDetails(dtb);
            DataTable dtb1 = this.cntrl.GetDctr_notificationvalue();
            GetNotificationData(dtb1);
        }
        public void GetDctrDetails(DataTable dtb)
        {
            dataGridView_notification.DataSource = null;
            if (dtb.Rows.Count > 0)
            {
                dataGridView_notification.DataSource = dtb;
            }
        }
        public void GetNotificationData(DataTable dt4)
        {
            try
            {
                if (dt4.Rows.Count > 0)
                {
                    for (int i = 0; i < dt4.Rows.Count; i++)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row = dataGridView_notification.Rows[i];
                        string confirmsms = dt4.Rows[i][2].ToString();
                        if (confirmsms == "1")
                        {
                            row.Cells[0].Value = true;
                        }
                        else
                        {
                            row.Cells[0].Value = false;
                        }
                        string schedulesms = dt4.Rows[i][3].ToString();
                        if (schedulesms == "1")
                        {
                            row.Cells[1].Value = true;
                        }
                        else
                        {
                            row.Cells[1].Value = false;
                        }
                        string confirmemail = dt4.Rows[i][4].ToString();
                        if (confirmemail == "1")
                        {
                            row.Cells[2].Value = true;
                        }
                        else
                        {
                            row.Cells[2].Value = false;
                        }
                        string schedemail = dt4.Rows[i][5].ToString();
                        if (schedemail == "1")
                        {
                            row.Cells[3].Value = true;
                        }
                        else
                        {
                            row.Cells[3].Value = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                if (button_refresh.Visible == true)
                {
                    refresh();
                }
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                if (status_pre == 1)//Admin User only
                {
                    DataTable dtUserprivilege = this.cntrl.User_privillage();
                    dataGridView_users.DataSource = dtUserprivilege;
                    dataGridView_users.ClearSelection();
                }
                else
                {
                   // this.tabControl1.TabPages[3].Dispose();
                }
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                if(DGV_Staff.Rows.Count==0)
                {
                    DataTable staff = this.cntrl.LoadStaff();
                    if (staff.Rows.Count > 0)
                    {
                        LoadStaff(staff);
                        if (DGV_Staff.Rows.Count > 0)
                        {
                            DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn()
                            {
                                Name = "Make Unavailable"
                            };
                            DGV_Staff.Columns.Add(col);
                            col.Width = 150;
                            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                       
                        for (int i = 0; i < staff.Rows.Count; i++)
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            row = DGV_Staff.Rows[i];
                            string confirmsms = staff.Rows[i]["availability"].ToString();
                            if (confirmsms == "Unavailabile")
                            {
                                row.Cells[5].Value = true;
                            }
                        }
                    }
                }
                else
                {

                }

                //DGV_Staff.Visible = true;

                







                //DataTable staff = this.cntrl.LoadStaff();
                //    DGV_Staff.Visible = true;
                //    //DGV_Staff.RowCount = 0;
                //    //DGV_Staff.ColumnCount = 2;
                //    if (staff.Rows.Count > 0)
                //    {
                //        //LoadStaff(staff);
                //        //if (DGV_Staff.Rows.Count > 0)
                //        //{
                //        //    DataGridViewCheckBoxColumn check2 = new DataGridViewCheckBoxColumn()
                //        //    {

                //        //    };
                //        //    DGV_Staff.Columns.Add(check2);
                //        //    check2.Width = 50;
                //        //    check2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //        //}
                //        //foreach (DataGridViewRow row in DGV_Staff.Rows)
                //        //{
                //        //    if (row.Cells["available"].Value.ToString().Trim() == "Unavailabile")
                //        //    {
                //        //        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[3];
                //        //        chk.Selected = true;
                //        //    }
                //        //}
                //        //foreach (DataGridViewColumn cl in DGV_Staff.Columns)
                //        //{
                //        //    cl.SortMode = DataGridViewColumnSortMode.NotSortable;
                //        //}
                //        //if (dt4.Rows.Count > 0)
                //        //{
                //            //for (int i = 0; i < staff.Rows.Count; i++)
                //            //{
                //            //    DataGridViewRow row = new DataGridViewRow();
                //            //    row = DGV_Staff.Rows[i];
                //            //    string confirmsms = staff.Rows[i]["availability"].ToString();
                //            //    if (confirmsms == "Unavailabile")
                //            //    {
                //            //        row.Cells[3].Value = true;
                //            //    }
                //            //    //else
                //            //    //{
                //            //    //    row.Cells[0].Value = false;
                //            //    //}
                //            //    //string schedulesms = dt4.Rows[i][3].ToString();
                //            //    //if (schedulesms == "1")
                //            //    //{
                //            //    //    row.Cells[1].Value = true;
                //            //    //}
                //            //    //else
                //            //    //{
                //            //    //    row.Cells[1].Value = false;
                //            //    //}
                //            //    //string confirmemail = dt4.Rows[i][4].ToString();
                //            //    //if (confirmemail == "1")
                //            //    //{
                //            //    //    row.Cells[2].Value = true;
                //            //    //}
                //            //    //else
                //            //    //{
                //            //    //    row.Cells[2].Value = false;
                //            //    //}
                //            //    //string schedemail = dt4.Rows[i][5].ToString();
                //            //    //if (schedemail == "1")
                //            //    //{
                //            //    //    row.Cells[3].Value = true;
                //            //    //}
                //            //    //else
                //            //    //{
                //            //    //    row.Cells[3].Value = false;
                //            //    //}
                //            //}
                //        //}
                //    }
            }
        }

        private void button_notification_Save_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView_notification.Rows.Count; i++)
                {
                    DataTable dt_dctrId = this.cntrl.Doctr_id();
                    string idd = dt_dctrId.Rows[i][0].ToString();
                    string name = dt_dctrId.Rows[i][1].ToString();
                   
                    if (Convert.ToBoolean(dataGridView_notification.Rows[i].Cells[0].Value) == true)
                    {
                        DataTable dt_doc = this.cntrl.Get_Doctor_notification(idd);
                        if (dt_doc.Rows.Count > 0)
                        {
                            DataTable dt_notification = this.cntrl.ifexsists_dctrnotification(idd);
                            this.cntrl.Update_notification(dt_notification, idd);
                        }
                        else
                        {
                            this.cntrl.Save_Notification(idd);
                        }
                    }
                    else// if checkbox1 confirm sms not checked
                    {
                        DataTable dt_doc = this.cntrl.Get_Doctor_notification(idd);
                        if (dt_doc.Rows.Count > 0)
                        {
                            DataTable dt_notification = this.cntrl.ifexsists_dctrnotification(idd);
                            this.cntrl.update_confirm_sms(dt_notification, idd);
                        }
                    }
                    if (Convert.ToBoolean(dataGridView_notification.Rows[i].Cells[1].Value) == true) // for second checkbox schedule sms
                    {
                        DataTable dt_doc = this.cntrl.Get_Doctor_notification(idd);
                        if (dt_doc.Rows.Count > 0)
                        {
                            DataTable dt_notification = this.cntrl.ifexsists_dctrnotification(idd);
                            this.cntrl.update_shedule_sms1(dt_notification, idd);
                        }
                        else
                        {
                            this.cntrl.save_shedule_sms(idd);
                        }
                    }
                    else
                    {
                        DataTable dt_doc = this.cntrl.Get_Doctor_notification(idd);
                        if (dt_doc.Rows.Count > 0)
                        {
                            DataTable dt_notification = this.cntrl.ifexsists_dctrnotification(idd);
                            this.cntrl.update_shedule_sms0(dt_notification, idd);
                        }
                    }
                    if (Convert.ToBoolean(dataGridView_notification.Rows[i].Cells[2].Value) == true)// for checkbox 3 confirm email
                    {
                        DataTable dt_doc = this.cntrl.Get_Doctor_notification(idd);
                        if (dt_doc.Rows.Count > 0)
                        {
                            DataTable dt_notification = this.cntrl.ifexsists_dctrnotification(idd);
                            this.cntrl.update_confirm_email1(dt_notification, idd);
                        }
                        else
                        {
                            this.cntrl.save_confirm_email(idd);
                        }
                    }
                    else
                    {
                        DataTable dt_doc = this.cntrl.Get_Doctor_notification(idd);
                        if (dt_doc.Rows.Count > 0)
                        {
                            DataTable dt_notification = this.cntrl.ifexsists_dctrnotification(idd);
                            this.cntrl.update_confirmemail0(dt_notification, idd);
                        }
                    }
                    if (Convert.ToBoolean(dataGridView_notification.Rows[i].Cells[3].Value) == true)// checkbox 4 schedule email
                    {
                        DataTable dt_doc = this.cntrl.Get_Doctor_notification(idd);
                        if (dt_doc.Rows.Count > 0)
                        {
                            DataTable dt_notification = this.cntrl.ifexsists_dctrnotification(idd);
                            this.cntrl.update_shedule_email1(dt_notification, idd);
                        }
                        else
                        {
                            this.cntrl.save_shedule_email(idd);
                        }
                    }
                    else
                    {
                        DataTable dt_doc = this.cntrl.Get_Doctor_notification(idd);
                        if (dt_doc.Rows.Count > 0)
                        {
                            DataTable dt_notification = this.cntrl.ifexsists_dctrnotification(idd);
                            this.cntrl.update_shedule_email0(dt_notification, idd);
                        }
                    }
                }
                MessageBox.Show("Successfully Saved !!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Brush BackBrush = new SolidBrush(Color.Transparent);
            Brush ForeBrush = new SolidBrush(Color.DarkSlateGray);
            TabPage page = tabControl1.TabPages[e.Index];
            e.Graphics.FillRectangle(BackBrush, e.Bounds);
            Rectangle paddedBounds = e.Bounds;
            int yOffset = (e.State == DrawItemState.Selected) ? -2 : 1;
            paddedBounds.Offset(1, yOffset);
            TextRenderer.DrawText(e.Graphics, page.Text, Font, paddedBounds, page.ForeColor);
            //Rasmi User privilege
            if (status_pre == 1)//Admin User only
            {
                if (e.Index == 2)//User Privilege tab page fill- becoz 1 & 2 Remove, Automaticaly rearranged index 3 to 1 
                {
                    DataTable dtUserprivilege = this.cntrl.User_privillage();
                    dataGridView_users.DataSource = dtUserprivilege;
                    dataGridView_users.ClearSelection();
                }
            }
            else if (e.Index == 3)
            {
                this.tabControl1.TabPages[3].Dispose();
            }
            //user privilege code ends here
        }

        private void btnAssignPrivilege_Click(object sender, EventArgs e)
        {
            if (userid == "")
            {
                MessageBox.Show("Select a User !!", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (chkSales.Checked == true || chkPurchase.Checked == true || chkstockledger.Checked == true || chkstocktransfer.Checked == true || chkadjustment.Checked == true)
                {
                    chk_inv.Checked = true;
                }
                else
                    chk_inv.Checked = false;
                if (chkEMRCF.Checked == true || chkEMRTP.Checked == true || chkEMRFP.Checked == true || chkEMRP.Checked == true || chkEMRI.Checked == true || chkEMRF.Checked == true || chkPAT.Checked == true || chkAPT.Checked == true || chkPMT.Checked == true || chk_vital.Checked == true)
                {
                    chkEMR.Checked = true;
                }
                else
                {
                    chkEMR.Checked = false;
                }
                if (chkRPTINCadd.Checked == true || chkRPTIncom.Checked == true || chkRPTPAYadd.Checked == true || chkRPTAPTadd.Checked == true || chkRPTPATadd.Checked == true || chkRPTINVadd.Checked == true || chkRPTEMRadd.Checked == true)
                {
                    chkRPT.Checked = true;
                }
                else
                {
                    chkRPT.Checked = false;
                }

                string strvalue = "";

                if (chkCalendar.Checked == true)
                    strvalue = ",('" + userid + "','CALENDAR','A')";//permission for Calendar
                if (chk_appo_show.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','APT','S')";//Show permission for appointment section
                if (chkAPTadd.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','APT','A')";//Add permission for appointment section
                if (chkAPTedit.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','APT','E')";//Edit permission for appointment section
                if (chkAPTdelete.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','APT','D')";//Delete permission for appointment section
                //Patients
                if (chkPATadd.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','PAT','A')";//Add permission for Patient section
                if (chkPATedit.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','PAT','E')";//Edit permission for Patient section
                if (chkPATdelete.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','PAT','D')";//Delete permission for Patient section
                if (patient_show.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','PAT','S')";//show permission for Patient section

                //payments
                if (chkPMTadd.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','PMT','A')";//Add permission for Payment section
                if (payments_show.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','PMT','S')";//show permission for Payment section
                if (chk_refund.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','PMTRF','A')";//Delete permission for Patient section
                //
                if (chkCLMSadd.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','CLMS','A')";//Add permission for Clinic Management setting section
                if (chkEMR.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMR','A')";//Add permission for EMR/Records main section
                // clinic findings
                if (chkEMRCFadd.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRCF','A')";//Add permission for EMR Clinical Finding  section
                if (chkEMRCFedit.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRCF','E')";//Edit permission for  EMR Clinical Finding section
                if (chkEMRCFdelete.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRCF','D')";//Delete permission for  EMR Clinical Finding section
                if (chk_c_finding_show.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRCF','S')";//show permission for  EMR Clinical Finding section
                //treatment  
                if (chkEMRTPadd.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRTP','A')";//Add permission for EMR Treatment Plans section
                if (chkEMRTPedit.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRTP','E')";//Edit permission for  EMR Treatment Plans  section
                if (chkEMRTPdelete.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRTP','D')";//Delete permission for  EMR  Treatment Plans section
                if (treatment_show.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRTP','S')";//show permission for  EMR  Treatment Plans section
                //finished procedure
                if (chkEMRFPadd.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRFP','A')";//Add permission for EMR  Finished Procedure  section
                if (chkEMRFPdelete.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRFP','D')";//Delete permission for  EMR  Finished Procedure section
                if (finished_show.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRFP','S')";//show permission for  EMR  Finished Procedure section
                if (chkEMRFadd.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRF','A')";//Add permission for EMR Files  section Attachment
                if (attachmnt_show.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRF','S')";//Add permission for EMR Files  section Attachment
                if (chk_attach_delete.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRF','D')";//Add permission for EMR Files  section Attachment

                //(dr["Category"].ToString() == "EMRF")//attachment
                //{
                //    if (dr["Permission"].ToString() == "A")
                //        chkEMRFadd.Checked = true;
                //    if (dr["Permission"].ToString() == "S")
                //        attachmnt_show.Checked = true;
                //    if (dr["Permission"].ToString() == "D")
                //        chk_attach_delete.Checked = true;





                //prescription
                if (chkEMRPadd.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRP','A')";//Add permission for EMR Prescription  section
                if (chkEMRPedit.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRP','E')";//Edit permission for  EMR Prescription section
                if (chkEMRPdelete.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRP','D')";//Delete permission for  EMR Prescription section
                if (prescrption_show.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRP','S')";//show permission for  EMR Prescription section

                //invoice  invoice_show
                if (chkEMRIadd.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRI','A')";//Add permission for EMR Invoice  section
                if (chkEMRIedit.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRI','E')";//Edit permission for  EMR Invoice section
                if (chkEMRIdelete.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRI','D')";//Delete permission for  EMR Invoice section
                if (invoice_show.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRI','S')";//show permission for  EMR Invoice section
                                                                           //vitals
                                                                           //if (chk_vital.Checked == true)
                                                                           //    strvalue = strvalue + ",('" + userid + "','EMRV','S')";//show permission for  EMR VITAL section
                if (chk_vital_show.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRV','S')";//show permission for  EMR VITAL section
                if (chk_vital_add.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRV','A')";//show permission for  EMR VITAL section
                if (chk_vital_edit.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRV','E')";//show permission for  EMR VITAL section
                //

                //lab
                if (chk_lab_add.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRL','A')";//show permission for  EMR lab section
                if (chk_lab_show.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EMRL','S')";//show permission for  EMR lab section

                if (chkRPT.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','RPT','A')";//Add permission for Report Main section
                //Report
                //if (chkRPTDSadd.Checked == true)
                //    strvalue = strvalue + ",('" + userid + "','RPTDS','A')";//Add permission for Report Section Daily Summary  section
                if (chkRPTINCadd.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','RPTINC','A')";//Add permission for Report Section Income  section
                if (chkRPTPAYadd.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','RPTPAY','A')";//Add permission for Report Section Payment  section
                if (chkRPTAPTadd.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','RPTAPT','A')";//Add permission for Report Section Appointment  section
                if (chkRPTPATadd.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','RPTPAT','A')";//Add permission for Report Section Patient  section
                //if (chkRPTEXPadd.Checked == true)
                //    strvalue = strvalue + ",('" + userid + "','RPTEXP','A')";//Add permission for Report Section Expenses  section
                if (chkRPTINVadd.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','RPTINV','A')";//Add permission for Report Section Inventory  section
                if (chkRPTIncom.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','RPTINCM','A')";
                if (chkRPTEMRadd.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','RPTEMR','A')";//Add permission for Report Section EMR  section
                //if (chkINVAIadd.Checked == true)
                //    strvalue = strvalue + ",('" + userid + "','INVAI','A')";//Add permission for Inventory Add Item  section
                //if (chk_profile.Checked == true)
                //    strvalue = strvalue + ",('" + userid + "','INVAS','A')";//Add permission for Inventory Add Stock  section
                //if (chkINVCSadd.Checked == true)
                //    strvalue = strvalue + ",('" + userid + "','INVCS','A')";//Add permission for Inventory Consume Stock  section
                //if (chkINVPIadd.Checked == true)
                //    strvalue = strvalue + ",('" + userid + "','INVPI','A')";//Add permission for Inventory Print Items section
                ////if (chkstockledger.Checked == true)
                ////    strvalue = strvalue + ",('" + userid + "','INVVS','A')";//Add permission for Inventory View Stock section
                //if (chkInvenSale.Checked == true)
                ////    strvalue = strvalue + ",('" + userid + "','INVSALE','A')";//Add permission for Inventory View Stock section
                //if (chkInventory.Checked == true)
                //    strvalue = strvalue + ",('" + userid + "','INVENTORY','A')";// permission for main inventory
                if (chkConsltn.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','CONSULTATION','A')";//permission for main Consultation
                if (chkCommnctn.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','COMMUNICATION','A')";//permission for main Communication
                if (chkExpnse.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','EXPENSE','A')";//permission for main Expense
                if (chkLabTrackng.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','LABTRACKING','A')";//permission for main Labtracking
                if (chk_inv.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','INVENTORY','A')";//permission for main Inventory
                //if (chk_inv.Checked == true)
                //    strvalue = strvalue + ",('" + userid + "','Inventory','A')";//permission for main Inventory
                //if (chk_inv.Checked == true)
                //    strvalue = strvalue + ",('" + userid + "','Inventory','A')";//permission for main Inventory
                if (chkSales.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','Sales','A')";//permission for  Sales
                if (chkPurchase.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','Purchase','A')";//permission for  Purchase
                if (chkstocktransfer.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','StockTransfer','A')";//permission for  StockTransfer
                if (chkadjustment.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','StockAdjustment','A')";//permission for  StockAdjustment
                if (chkstockledger.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','StockLedger','S')";//permission for  StockLedger
                if (chk_profile.Checked == true)
                    strvalue = strvalue + ",('" + userid + "','Profile','S')";//permission for  profile
                //if (chk_report_show.Checked == true)
                //    strvalue = strvalue + ",('" + userid + "','RPT','S')";//permission for  reports
                this.cntrl.delete_userprivillage(userid);
                if (strvalue != "")
                {
                    string strvalue1 = "";
                    strvalue1 = strvalue.Substring(1, strvalue.Length - 1);
                    this.cntrl.save_userprivillage(strvalue1);
                }
                MessageBox.Show("Successfully Saved !!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ", Select Privileges and  Try Again !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
 
        private void dataGridView_users_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            {
                int r = e.RowIndex;
                userid = dataGridView_users.Rows[r].Cells[0].Value.ToString();
            }
        }
        //user privillage
        private void RefreshCheckboxes()
        {
            payments_show.Checked = false;
            invoice_show.Checked = false;
            finished_show.Checked = false;
            treatment_show.Checked = false;
            chk_c_finding_show.Checked = false;
            payments_show.Checked = false;
            patient_show.Checked = false;
            chk_profile.Checked = false;
            chk_profile.Checked = false;
            chk_appo_show.Checked = false;
            chkAPT.Checked = false;
            chkAPTadd.Checked = false;
            chkAPTedit.Checked = false;
            chkAPTdelete.Checked = false;
            chkAPTClinicAppoinment.Checked = false;
            chkPAT.Checked = false;
            chkPATadd.Checked = false;
            chkPATedit.Checked = false;
            chkPATdelete.Checked = false;
            chkPMT.Checked = false;
            chkPMTadd.Checked = false;
            chkCLMSadd.Checked = false;
            chkEMR.Checked = false;
            chkEMRCF.Checked = false;
            chkEMRF.Checked = false;
            chkEMRFP.Checked = false;
            chkEMRP.Checked = false;
            chkEMRTP.Checked = false;
            chkEMRCFadd.Checked = false;
            chkEMRCFedit.Checked = false;
            chkEMRCFdelete.Checked = false;
            //chkEMRFadd.Checked = false;
            chkEMRI.Checked = false;
            chkEMRIadd.Checked = false;
            chkEMRIdelete.Checked = false;
            chkEMRIedit.Checked = false;
            chkEMRFPadd.Checked = false;
            chkEMRFPdelete.Checked = false;
            chkEMRPadd.Checked = false;
            chkEMRPedit.Checked = false;
            chkEMRPdelete.Checked = false;
            chkEMRTPadd.Checked = false;
            chkEMRTPedit.Checked = false;
            chkEMRTPdelete.Checked = false;
            chkRPT.Checked = false;
            chkRPTAPTadd.Checked = false;
            //chkRPTDSadd.Checked = false;
            chkRPTEMRadd.Checked = false;
            //chkRPTEXPadd.Checked = false;
            chkRPTINCadd.Checked = false;
            chkRPTINVadd.Checked = false;
            chkRPTIncom.Checked = false;
            chkRPTPATadd.Checked = false;
            chkRPTPAYadd.Checked = false;
            chkadjustment.Checked = false;
            chkINVAIadd.Checked = false;
            chk_profile.Checked = false;
            chkINVCSadd.Checked = false;
            //chkINVPIadd.Checked = false;
            //chkstockledger.Checked = false;
            //chkInvenSale.Checked = false;
            chkInventory.Checked = false;
            chkConsltn.Checked = false;
            chkCommnctn.Checked = false;
            chkExpnse.Checked = false;
            chkLabTrackng.Checked = false;
            chkCalendar.Checked = false;
            chk_inv.Checked = false;

            chkSales.Checked = false;
            chkPurchase.Checked = false;
            chkLabTrackng.Checked = false;
            chkadjustment.Checked = false;
            chkstockledger.Checked = false;
            chkstocktransfer.Checked = false;

            chk_vital.Checked = false;
            chk_vital_show.Checked = false;
            chk_vital_add.Checked = false;
            chk_vital_edit.Checked = false;
            chk_refund.Checked = false;
            chk_refund_add.Checked = false;

            chk_lab_show.Checked = false;
            chk_lab_add.Checked = false;
            chkLab.Checked = false;

            chkEMRFadd.Checked = false;
            attachmnt_show.Checked = false;
            chk_attach_delete.Checked = false;

            //    if (dr["Permission"].ToString() == "A")
            //        chkEMRFadd.Checked = true;
            //    if (dr["Permission"].ToString() == "S")
            //        attachmnt_show.Checked = true;
            //    if (dr["Permission"].ToString() == "D")
            //        chk_attach_delete.Checked = true;

        }

        private void dataGridView_users_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int r = e.RowIndex;
                userid = dataGridView_users.Rows[r].Cells[0].Value.ToString();
                DataTable dt = this.cntrl.Get_userPrivillageData(userid);
                RefreshCheckboxes();
                SetCheckboxfromDB(dt);
            }
            catch (Exception ex)
            {
            }
        }
        public bool flag_ty = false;
        //private void SetCheckboxfromDB(DataTable dt)
        //{
        //    flag_ty = true;
        //    foreach (   DataRow dr in dt.Rows)
        //    {
        //        if (dr["Category"].ToString() == "PAT")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkPATadd.Checked = true;
        //            if (dr["Permission"].ToString() == "E")
        //                chkPATedit.Checked = true;
        //            if (dr["Permission"].ToString() == "D")
        //                chkPATdelete.Checked = true;
        //            if (chkPATadd.Checked == true || chkPATedit.Checked == true || chkPATdelete.Checked == true)
        //            {
        //                chkPAT.Checked = true;
        //            }
        //            else
        //            {
        //                chkPAT.Checked = false;
        //            }
        //        }
        //        else if (dr["Category"].ToString() == "APT")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkAPTadd.Checked = true;
        //            if (dr["Permission"].ToString() == "E")
        //                chkAPTedit.Checked = true;
        //            if (dr["Permission"].ToString() == "D") 
        //                chkAPTdelete.Checked = true;
        //            if (dr["Permission"].ToString() == "AP")
        //                chkAPTClinicAppoinment.Checked = true;
        //            if (chkAPTadd.Checked == true || chkAPTedit.Checked == true || chkAPTdelete.Checked == true || chkAPTClinicAppoinment.Checked == true)
        //            {
        //                chkAPT.Checked = true;
        //            }
        //            else
        //            {
        //                chkAPT.Checked = false;
        //            }
        //        }
        //        else if (dr["Category"].ToString() == "PMT")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkPMTadd.Checked = true;
        //            if (chkPMTadd.Checked == true)
        //            {
        //                chkPMT.Checked = true;
        //            }
        //            else
        //            {
        //                chkPMT.Checked = false;
        //            }
        //        }
        //        else if (dr["Category"].ToString() == "CLMS")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkCLMSadd.Checked = true;
        //        }
        //        else if (dr["Category"].ToString() == "CALENDAR")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkCalendar.Checked = true;
        //        }
        //        else if (dr["Category"].ToString() == "EMR")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkEMR.Checked = true;
        //        }
        //        else if (dr["Category"].ToString() == "EMRCF")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkEMRCFadd.Checked = true;

        //            if (dr["Permission"].ToString() == "E")
        //                chkEMRCFedit.Checked = true;
        //            if (dr["Permission"].ToString() == "D")
        //                chkEMRCFdelete.Checked = true;
        //            if (chkEMRCFadd.Checked == true || chkEMRCFedit.Checked == true || chkEMRCFdelete.Checked == true)
        //            {
        //                chkEMRCF.Checked = true;
        //                //chkEMR.Checked = true;
        //             }
        //            else
        //            {
        //                chkEMRCF.Checked = false;
        //                //chkEMR.Checked = false;
        //            }
        //        }
        //        else if (dr["Category"].ToString() == "EMRTP")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkEMRTPadd.Checked = true;
        //            if (dr["Permission"].ToString() == "E")
        //                chkEMRTPedit.Checked = true;
        //            if (dr["Permission"].ToString() == "D")
        //                chkEMRTPdelete.Checked = true;
        //            if (chkEMRTPadd.Checked == true || chkEMRTPedit.Checked == true || chkEMRTPdelete.Checked == true)
        //            {
        //                chkEMRTP.Checked = true;
        //                //chkEMR.Checked = true;
        //            }
        //            else
        //            {
        //                chkEMRTP.Checked = false;
        //                //chkEMR.Checked = false;
        //            }
        //        }
        //        else if (dr["Category"].ToString() == "EMRFP")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkEMRFPadd.Checked = true;
        //            if (dr["Permission"].ToString() == "D")
        //                chkEMRFPdelete.Checked = true;
        //            if (chkEMRFPadd.Checked == true || chkEMRFPdelete.Checked == true)
        //            {
        //                chkEMRFP.Checked = true;
        //                //chkEMR.Checked = true;
        //            }
        //            else
        //            {
        //                chkEMRFP.Checked = false;
        //                //chkEMR.Checked = false;
        //            }
        //        }
        //        else if (dr["Category"].ToString() == "EMRF")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkEMRFadd.Checked = true;
        //            if (chkEMRFadd.Checked == true)
        //            {
        //                chkEMRF.Checked = true;
        //            }
        //            else
        //            {
        //                chkEMRF.Checked = false;
        //            }
        //        }
        //        else if (dr["Category"].ToString() == "EMRP")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkEMRPadd.Checked = true;
        //            if (dr["Permission"].ToString() == "E")
        //                chkEMRPedit.Checked = true;
        //            if (dr["Permission"].ToString() == "D")
        //                chkEMRPdelete.Checked = true;
        //            if (chkEMRPadd.Checked == true || chkEMRPedit.Checked == true || chkEMRPdelete.Checked == true)
        //            {
        //                chkEMRP.Checked = true;
        //                //chkEMR.Checked = true;
        //            }
        //            else
        //            {
        //                chkEMRP.Checked = false;
        //                //chkEMR.Checked = false;
        //            }
        //        }
        //        else if (dr["Category"].ToString() == "EMRI")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkEMRIadd.Checked = true;
        //            if (dr["Permission"].ToString() == "E")
        //                chkEMRIedit.Checked = true;
        //            if (dr["Permission"].ToString() == "D")
        //                chkEMRIdelete.Checked = true;
        //            if (chkEMRIadd.Checked == true || chkEMRIedit.Checked == true || chkEMRIdelete.Checked == true)
        //            {
        //                chkEMRI.Checked = true;
        //                //chkEMR.Checked = true;
        //            }
        //            else
        //            {
        //                chkEMRI.Checked = false;
        //                //chkEMR.Checked = false;
        //            }
        //        }
        //        else if (dr["Category"].ToString() == "RPT")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkRPT.Checked = true;
        //            //chkRPT.Checked = false;
        //        }
        //        //else if (dr["Category"].ToString() == "RPTDS")
        //        //{
        //        //    if (dr["Permission"].ToString() == "A")
        //        //        chkRPTDSadd.Checked = true;
        //        //    //chkRPT.Checked = false;
        //        //}
        //        else if (dr["Category"].ToString() == "RPTINC")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkRPTINCadd.Checked = true;
        //            //chkRPT.Checked = false;
        //        }
        //        else if (dr["Category"].ToString() == "RPTINCM")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkRPTIncom.Checked = true;
        //            //chkRPT.Checked = false;
        //        }

        //        else if (dr["Category"].ToString() == "RPTPAY")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkRPTPAYadd.Checked = true;
        //            //chkRPT.Checked = false;
        //        }
        //        else if (dr["Category"].ToString() == "RPTAPT")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkRPTAPTadd.Checked = true;
        //            //chkRPT.Checked = false;
        //        }
        //        else if (dr["Category"].ToString() == "RPTPAT")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkRPTPATadd.Checked = true;
        //            //chkRPT.Checked = false;
        //        }
        //        //else if (dr["Category"].ToString() == "RPTEXP")
        //        //{
        //        //    if (dr["Permission"].ToString() == "A")
        //        //        chkRPTEXPadd.Checked = true;
        //        //    //chkRPT.Checked = false;
        //        //}
        //        else if (dr["Category"].ToString() == "RPTINV")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkRPTINVadd.Checked = true;
        //            //chkRPT.Checked = false;
        //        }
        //        else if (dr["Category"].ToString() == "RPTEMR")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkRPTEMRadd.Checked = true;
        //            //chkRPT.Checked = false;
        //        }
        //        //else if (dr["Category"].ToString() == "INVAI")
        //        //{
        //        //    if (dr["Permission"].ToString() == "A")
        //        //        chkINVAIadd.Checked = true;
        //        //    //chkINV.Checked = false;
        //        //}
        //        //else if (dr["Category"].ToString() == "INVAS")
        //        //{
        //        //    if (dr["Permission"].ToString() == "A")
        //        //        chkINVASadd.Checked = true;

        //        //}
        //        //else if (dr["Category"].ToString() == "INVCS")
        //        //{
        //        //    if (dr["Permission"].ToString() == "A")
        //        //        chkINVCSadd.Checked = true;
        //        //}
        //        //else if (dr["Category"].ToString() == "INVPI")
        //        //{
        //        //    if (dr["Permission"].ToString() == "A")
        //        //        chkINVPIadd.Checked = true;
        //        //}
        //        else if (dr["Category"].ToString() == "INVVS")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkstockledger.Checked = true;
        //        }
        //        //else if (dr["Category"].ToString() == "INVSALE")
        //        //{
        //        //    if (dr["Permission"].ToString() == "A")
        //        //        chkInvenSale.Checked = true;
        //        //}
        //        else if (dr["Category"].ToString() == "INVENTORY")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkInventory.Checked = true;
        //        }
        //        else if (dr["Category"].ToString() == "CONSULTATION")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkConsltn.Checked = true;
        //        }
        //        else if (dr["Category"].ToString() == "COMMUNICATION")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkCommnctn.Checked = true;
        //        }
        //        else if (dr["Category"].ToString() == "EXPENSE")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkExpnse.Checked = true;
        //        }
        //        else if (dr["Category"].ToString() == "LABTRACKING")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkLabTrackng.Checked = true;
        //        }
        //        else if (dr["Category"].ToString() == "Inventory")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chk_inv.Checked = true;
        //        }
        //        else if (dr["Category"].ToString() == "Sales")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkSales.Checked = true;
        //        }
        //        else if (dr["Category"].ToString() == "Purchase")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkPurchase.Checked = true;
        //        }
        //        else if (dr["Category"].ToString() == "StockAdjustment")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkadjustment.Checked = true;
        //        }
        //        else if (dr["Category"].ToString() == "StockTransfer")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkstocktransfer.Checked = true;
        //        }
        //        else if (dr["Category"].ToString() == "StockLedger")
        //        {
        //            if (dr["Permission"].ToString() == "A")
        //                chkstockledger.Checked = true;
        //        }

        //    }
        //    if(chkEMRCF.Checked==true || chkEMRTP.Checked == true || chkEMRFP.Checked == true || chkEMRP.Checked ==true || chkEMRI.Checked == true)
        //    {
        //        chkEMR.Checked = true;
        //    }
        //    else
        //    {
        //        chkEMR.Checked = false;
        //    }
        //    if (chkRPTINCadd.Checked ==true || chkRPTIncom.Checked == true || chkRPTPAYadd.Checked == true || chkRPTAPTadd.Checked == true || chkRPTPATadd.Checked ==true  || chkRPTINVadd.Checked== true || chkRPTEMRadd.Checked == true )
        //    {
        //        chkRPT.Checked = true;
        //    }
        //    else
        //    {
        //        chkRPT.Checked = false;
        //    }
        //    if (chkstockledger.Checked == true || chkstocktransfer.Checked == true || chkadjustment.Checked == true || chkPurchase.Checked == true || chkSales.Checked == true)
        //    {
        //        chk_inv.Checked = true;
        //    }
        //    else
        //    {
        //        chk_inv.Checked = false;
        //    }
        //    flag_ty = false;
        //}
        private void SetCheckboxfromDB(DataTable dt)
        {
            flag_ty = true;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Category"].ToString() == "PAT")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkPATadd.Checked = true;
                    if (dr["Permission"].ToString() == "E")
                        chkPATedit.Checked = true;
                    if (dr["Permission"].ToString() == "D")
                        chkPATdelete.Checked = true;
                    if (dr["Permission"].ToString() == "S")
                        patient_show.Checked = true;
                    if (chkPATadd.Checked == true || chkPATedit.Checked == true || chkPATdelete.Checked == true || patient_show.Checked == true)
                    {
                        chkPAT.Checked = true;
                    }
                    else
                    {
                        chkPAT.Checked = false;
                    }
                }
                else if (dr["Category"].ToString() == "APT")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkAPTadd.Checked = true;
                    if (dr["Permission"].ToString() == "E")
                        chkAPTedit.Checked = true;
                    if (dr["Permission"].ToString() == "D")
                        chkAPTdelete.Checked = true;
                    if (dr["Permission"].ToString() == "S")
                        chk_appo_show.Checked = true;
                    if (chkAPTadd.Checked == true || chkAPTedit.Checked == true || chkAPTdelete.Checked == true || chk_appo_show.Checked == true)//|| chkAPTClinicAppoinment.Checked == true
                    {
                        chkAPT.Checked = true;
                    }
                    else
                    {
                        chkAPT.Checked = false;
                    }
                }
                else if (dr["Category"].ToString() == "PMT")//chkPMT payments
                {
                    if (dr["Permission"].ToString() == "A")
                        chkPMTadd.Checked = true;
                    if (dr["Permission"].ToString() == "S")
                        payments_show.Checked = true;
                    if (chkPMTadd.Checked == true || payments_show.Checked == true)
                    {
                        chkPMT.Checked = true;
                    }
                    else
                    {
                        chkPMT.Checked = false;
                    }
                }
                else if (dr["Category"].ToString() == "PMTRF")
                {
                    if (dr["Permission"].ToString() == "A")
                        chk_refund_add.Checked = true;

                    if (chk_refund_add.Checked == true)
                    {
                        chk_refund.Checked = true;
                    }
                }
                else if (dr["Category"].ToString() == "CLMS")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkCLMSadd.Checked = true;
                }
                else if (dr["Category"].ToString() == "CALENDAR")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkCalendar.Checked = true;
                }
                else if (dr["Category"].ToString() == "EMR")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkEMR.Checked = true;
                }
                else if (dr["Category"].ToString() == "EMRCF")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkEMRCFadd.Checked = true;

                    if (dr["Permission"].ToString() == "E")
                        chkEMRCFedit.Checked = true;
                    if (dr["Permission"].ToString() == "D")
                        chkEMRCFdelete.Checked = true;
                    if (dr["Permission"].ToString() == "S")
                        chk_c_finding_show.Checked = true;
                    if (chkEMRCFadd.Checked == true || chkEMRCFedit.Checked == true || chkEMRCFdelete.Checked == true || chk_c_finding_show.Checked == true)
                    {
                        chkEMRCF.Checked = true;
                        //chkEMR.Checked = true;
                    }
                    else
                    {
                        chkEMRCF.Checked = false;
                        //chkEMR.Checked = false;
                    }
                }
                else if (dr["Category"].ToString() == "EMRTP")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkEMRTPadd.Checked = true;
                    if (dr["Permission"].ToString() == "E")
                        chkEMRTPedit.Checked = true;
                    if (dr["Permission"].ToString() == "D")
                        chkEMRTPdelete.Checked = true;
                    if (dr["Permission"].ToString() == "S")
                        treatment_show.Checked = true;
                    if (chkEMRTPadd.Checked == true || chkEMRTPedit.Checked == true || chkEMRTPdelete.Checked == true || treatment_show.Checked == true)
                    {
                        chkEMRTP.Checked = true;
                        //chkEMR.Checked = true;
                    }
                    else
                    {
                        chkEMRTP.Checked = false;
                        //chkEMR.Checked = false;
                    }
                }
                else if (dr["Category"].ToString() == "EMRFP")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkEMRFPadd.Checked = true;
                    if (dr["Permission"].ToString() == "D")
                        chkEMRFPdelete.Checked = true;
                    if (dr["Permission"].ToString() == "S")
                        finished_show.Checked = true;
                    if (chkEMRFPadd.Checked == true || chkEMRFPdelete.Checked == true || finished_show.Checked == true)
                    {
                        chkEMRFP.Checked = true;
                        //chkEMR.Checked = true;
                    }
                    else
                    {
                        chkEMRFP.Checked = false;
                        //chkEMR.Checked = false;
                    }
                }
                else if (dr["Category"].ToString() == "EMRF")//attachment
                {
                    if (dr["Permission"].ToString() == "A")
                        chkEMRFadd.Checked = true;
                    if (dr["Permission"].ToString() == "S")
                        attachmnt_show.Checked = true;
                    if (dr["Permission"].ToString() == "D")
                        chk_attach_delete.Checked = true;
                    if (chkEMRFadd.Checked == true || attachmnt_show.Checked ==true || chk_attach_delete.Checked ==true)
                    {
                        chkEMRF.Checked = true;
                    }
                    else
                    {
                        chkEMRF.Checked = false;
                    }
                }
                else if (dr["Category"].ToString() == "EMRP")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkEMRPadd.Checked = true;
                    if (dr["Permission"].ToString() == "E")
                        chkEMRPedit.Checked = true;
                    if (dr["Permission"].ToString() == "D")
                        chkEMRPdelete.Checked = true;
                    if (dr["Permission"].ToString() == "S")
                        prescrption_show.Checked = true;
                    if (chkEMRPadd.Checked == true || chkEMRPedit.Checked == true || chkEMRPdelete.Checked == true || prescrption_show.Checked == true)
                    {
                        chkEMRP.Checked = true;
                        //chkEMR.Checked = true;
                    }
                    else
                    {
                        chkEMRP.Checked = false;
                        //chkEMR.Checked = false;
                    }
                }
                else if (dr["Category"].ToString() == "EMRI")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkEMRIadd.Checked = true;
                    if (dr["Permission"].ToString() == "E")
                        chkEMRIedit.Checked = true;
                    if (dr["Permission"].ToString() == "D")
                        chkEMRIdelete.Checked = true;
                    if (dr["Permission"].ToString() == "S")
                        invoice_show.Checked = true;
                    if (chkEMRIadd.Checked == true || chkEMRIedit.Checked == true || chkEMRIdelete.Checked == true || invoice_show.Checked == true)
                    {
                        chkEMRI.Checked = true;
                        //chkEMR.Checked = true;
                    }
                    else
                    {
                        chkEMRI.Checked = false;
                        //chkEMR.Checked = false;
                    }
                }
                else if (dr["Category"].ToString() == "EMRV")
                {
                    if (dr["Permission"].ToString() == "A")
                        chk_vital_add.Checked = true;
                    if (dr["Permission"].ToString() == "E")
                        chk_vital_edit.Checked = true;
                    //if (dr["Permission"].ToString() == "D")
                    //    chkEMRIdelete.Checked = true;
                    if (dr["Permission"].ToString() == "S")
                        chk_vital_show.Checked = true;
                    if (chk_vital_add.Checked == true || chk_vital_edit.Checked == true || chk_vital_show.Checked == true )
                    {
                        chkEMRI.Checked = true;
                        //chkEMR.Checked = true;
                    }
                    else
                    {
                        chkEMRI.Checked = false;
                        //chkEMR.Checked = false;
                    }
                }


                else if (dr["Category"].ToString() == "RPT")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkRPT.Checked = true;
                    //chkRPT.Checked = false;
                }
                //else if (dr["Category"].ToString() == "RPTDS")
                //{
                //    if (dr["Permission"].ToString() == "A")
                //        chkRPTDSadd.Checked = true;
                //    //chkRPT.Checked = false;
                //}
                else if (dr["Category"].ToString() == "RPTINC")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkRPTINCadd.Checked = true;
                    //chkRPT.Checked = false;
                }
                else if (dr["Category"].ToString() == "RPTINCM")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkRPTIncom.Checked = true;
                    //chkRPT.Checked = false;
                }

                else if (dr["Category"].ToString() == "RPTPAY")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkRPTPAYadd.Checked = true;
                    //chkRPT.Checked = false;
                }
                else if (dr["Category"].ToString() == "RPTAPT")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkRPTAPTadd.Checked = true;
                    //chkRPT.Checked = false;
                }
                else if (dr["Category"].ToString() == "RPTPAT")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkRPTPATadd.Checked = true;
                    //chkRPT.Checked = false;
                }
                //else if (dr["Category"].ToString() == "RPTEXP")
                //{
                //    if (dr["Permission"].ToString() == "A")
                //        chkRPTEXPadd.Checked = true;
                //    //chkRPT.Checked = false;
                //}
                else if (dr["Category"].ToString() == "RPTINV")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkRPTINVadd.Checked = true;
                    //chkRPT.Checked = false;
                }
                else if (dr["Category"].ToString() == "RPTEMR")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkRPTEMRadd.Checked = true;
                    //chkRPT.Checked = false;
                }
                //else if (dr["Category"].ToString() == "INVAI")
                //{
                //    if (dr["Permission"].ToString() == "A")
                //        chkINVAIadd.Checked = true;
                //    //chkINV.Checked = false;
                //}
                //else if (dr["Category"].ToString() == "INVAS")
                //{
                //    if (dr["Permission"].ToString() == "A")
                //        chk_profile.Checked = true;

                //}
                //else if (dr["Category"].ToString() == "INVCS")
                //{
                //    if (dr["Permission"].ToString() == "A")
                //        chkINVCSadd.Checked = true;
                //}
                //else if (dr["Category"].ToString() == "INVPI")
                //{
                //    if (dr["Permission"].ToString() == "A")
                //        chkINVPIadd.Checked = true;
                //}
                //else if (dr["Category"].ToString() == "INVVS")
                //{
                //    if (dr["Permission"].ToString() == "A")
                //        chkstockledger.Checked = true;
                //}
                //else if (dr["Category"].ToString() == "INVSALE")
                //{
                //    if (dr["Permission"].ToString() == "A")
                //        chkInvenSale.Checked = true;
                //}
                //else if (dr["Category"].ToString() == "INVENTORY")
                //{
                //    if (dr["Permission"].ToString() == "A")
                //        chkInventory.Checked = true;
                //}
                else if (dr["Category"].ToString() == "CONSULTATION")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkConsltn.Checked = true;
                }
                else if (dr["Category"].ToString() == "COMMUNICATION")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkCommnctn.Checked = true;
                }
                else if (dr["Category"].ToString() == "EXPENSE")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkExpnse.Checked = true;
                }
                else if (dr["Category"].ToString() == "LABTRACKING")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkLabTrackng.Checked = true;
                }
                else if (dr["Category"].ToString() == "INVENTORY")
                {
                    if (dr["Permission"].ToString() == "A")
                        chk_inv.Checked = true;
                }
                else if (dr["Category"].ToString() == "Sales")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkSales.Checked = true;
                }
                else if (dr["Category"].ToString() == "Purchase")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkPurchase.Checked = true;
                }
                else if (dr["Category"].ToString() == "StockAdjustment")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkadjustment.Checked = true;
                }
                else if (dr["Category"].ToString() == "StockTransfer")
                {
                    if (dr["Permission"].ToString() == "A")
                        chkstocktransfer.Checked = true;
                }
                else if (dr["Category"].ToString() == "StockLedger")
                {
                    if (dr["Permission"].ToString() == "S")
                        chkstockledger.Checked = true;
                }
                else if (dr["Category"].ToString() == "Profile")
                {
                    if (dr["Permission"].ToString() == "S")
                        chk_profile.Checked = true;
                }///////////////////jdzdghjga///////
                else if (dr["Category"].ToString() == "EMRL")
                {
                    if (dr["Permission"].ToString() == "S")
                        chk_lab_show.Checked = true;

                    if (dr["Permission"].ToString() == "A")
                        chk_lab_add.Checked = true;
                    if (chk_lab_show.Checked == true || chk_lab_add.Checked == true )
                    {
                        chkLab.Checked = true;
                    }
                    else
                    {
                        chkLab.Checked = false;
                    }
                }
               
                

                //else if (dr["Category"].ToString() == "Reports")
                //{
                //    if (dr["Permission"].ToString() == "S")
                //        chk_report_show.Checked = true;
                //}
            }
            //if(chkEMRCF.Checked==true || chkEMRTP.Checked == true || chkEMRFP.Checked == true || chkEMRP.Checked ==true || chkEMRI.Checked == true || chkEMRF.Checked==true || chkPAT.Checked==true || chkAPT.Checked==true || chkPMT.Checked==true || chk_vital.Checked==true)
            //{
            //    chkEMR.Checked = true;
            //}
            //else
            //{
            //    chkEMR.Checked = false;
            //}
            //if (chkRPTINCadd.Checked ==true || chkRPTIncom.Checked == true || chkRPTPAYadd.Checked == true || chkRPTAPTadd.Checked == true || chkRPTPATadd.Checked ==true  || chkRPTINVadd.Checked== true || chkRPTEMRadd.Checked == true )
            //{
            //    chkRPT.Checked = true;
            //}
            //else
            //{
            //    chkRPT.Checked = false;
            //}
            if (chkstockledger.Checked == true || chkstocktransfer.Checked == true || chkadjustment.Checked == true || chkPurchase.Checked == true || chkSales.Checked == true)
            {
                chk_inv.Checked = true;
            }
            else
            {
                chk_inv.Checked = false;
            }
            flag_ty = false;
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dataGridView_users.ClearSelection();
            RefreshCheckboxes();
        }

        private void chkPAT_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkPAT.Checked == true)
            {
                chkPATadd.Checked = true;
                chkPATedit.Checked = true;
                chkPATdelete.Checked = true;
            }
            else
            {
                chkPATadd.Checked = false;
                chkPATedit.Checked = false;
                chkPATdelete.Checked = false;
            }
        }

        private void chkAPT_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkAPT.Checked == true)
            {
                chkAPTadd.Checked = true;
                chkAPTedit.Checked = true;
                chkAPTdelete.Checked = true;
                chkAPTClinicAppoinment.Checked = true;
            }
            else
            {
                chkAPTadd.Checked = false;
                chkAPTedit.Checked = false;
                chkAPTdelete.Checked = false;
                chkAPTClinicAppoinment.Checked = false;
            }
        }

        private void chkPMT_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkPMT.Checked == true)
            {
                chkPMTadd.Checked = true;
            }
            else
            {
                chkPMTadd.Checked = false;
            }
        }

        private void chkEMR_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkEMR.Checked == false)
            {
                chkEMRCF.Checked = false;
                chkEMRTP.Checked = false;
                chkEMRP.Checked = false;
                chkEMRFP.Checked = false;
                chkEMRF.Checked = false;
                chkEMRI.Checked = false;
            }
            //else
            //{
            //    chkEMRCF.Checked = false;
            //    chkEMRTP.Checked = false;
            //    chkEMRP.Checked = false;
            //    chkEMRFP.Checked = false;
            //    chkEMRF.Checked = false;
            //    chkEMRI.Checked = false;
            //}
        }

        private void chkEMRCF_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkEMRCF.Checked == true)
            {
                chkEMR.Checked = true;
                chkEMRCFadd.Checked = true;
                chkEMRCFedit.Checked = true;
                chkEMRCFdelete.Checked = true;
            }
            else
            {
                chkEMRCFadd.Checked = false;
                chkEMRCFedit.Checked = false;
                chkEMRCFdelete.Checked = false;
            }
        }

        private void chkEMRTP_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkEMRTP.Checked == true)
            {
                chkEMR.Checked = true;
                chkEMRTPadd.Checked = true;
                chkEMRTPedit.Checked = true;
                chkEMRTPdelete.Checked = true;
            }
            else
            {
                chkEMRTPadd.Checked = false;
                chkEMRTPedit.Checked = false;
                chkEMRTPdelete.Checked = false;
            }
        }

        private void chkEMRP_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkEMRP.Checked == true)
            {
                chkEMR.Checked = true;
                chkEMRPadd.Checked = true;
                chkEMRPedit.Checked = true;
                chkEMRPdelete.Checked = true;
            }
            else
            {
                chkEMRPadd.Checked = false;
                chkEMRPedit.Checked = false;
                chkEMRPdelete.Checked = false;
            }
        }

        private void chkEMRFP_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkEMRFP.Checked == true)
            {
                chkEMR.Checked = true;
                chkEMRFPadd.Checked = true;
                chkEMRFPdelete.Checked = true;
            }
            else
            {
                chkEMRFPadd.Checked = false;
                chkEMRFPdelete.Checked = false;
            }
        }

        private void chkEMRF_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkEMRF.Checked == true)
            {
                chkEMR.Checked = true;
                chkEMRFadd.Checked = true;
            }
            else
            {
                chkEMRFadd.Checked = false;
            }
        }

        private void chkEMRI_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkEMRI.Checked == true)
            {
                chkEMR.Checked = true;
                chkEMRIadd.Checked = true;
                chkEMRIedit.Checked = true;
                chkEMRIdelete.Checked = true;
            }
            else
            {
                chkEMRIadd.Checked = false;
                chkEMRIedit.Checked = false;
                chkEMRIdelete.Checked = false;
            }
        }

        private void chkRPT_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkRPT.Checked == false)
            {
                //chkRPTDSadd.Checked = false;
                chkRPTAPTadd.Checked = false;
                chkRPTPATadd.Checked = false;
                chkRPTPAYadd.Checked = false;
                chkRPTINCadd.Checked = false;
                chkRPTEMRadd.Checked = false;
                chkRPTINVadd.Checked = false;
                //chkRPTEXPadd.Checked = false;
                chkRPTIncom.Checked = false;
            }
            //else
            //{
                
            //    //chkRPTDSadd.Checked = true;
            //    chkRPTAPTadd.Checked = true;
            //    chkRPTPATadd.Checked = true;
            //    chkRPTPAYadd.Checked = true;
            //    chkRPTINCadd.Checked = true;
            //    chkRPTEMRadd.Checked = true;
            //    chkRPTINVadd.Checked = true;
            //    chkRPTEXPadd.Checked = true;
            //    chkRPTIncom.Checked = true;
            //}
        }

        private void cmbStaffType_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbStaffType_MouseLeave(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
        }

        private void cmbStaffType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStaffType.SelectedItem != null)
            {
                errorProvider1.Dispose();
                if(cmbStaffType.Text.Trim()== "DOCTOR")
                {
                    label15.Enabled = true;
                    txt_fee.Enabled = true;
                    label16.Enabled = true;
                    txt_followup_fee.Enabled = true;
                    label19.Enabled = true;
                    txt_period.Enabled = true;
                }
                else
                {
                    label15.Enabled = false;
                    txt_fee.Enabled = false;
                    label16.Enabled = false;
                    txt_followup_fee.Enabled = false;
                    label19.Enabled = false;
                    txt_period.Enabled = false;
                }
            }
        }

        private void text_doctorname_KeyPress(object sender, KeyPressEventArgs e)
        {
            string arr = "!@#$%^&*()+=-[]0123456789\\\';,./{}|\":<>?";
            for (int k = 0; k < arr.Length; k++)
            {
                if (e.KeyChar == arr[k])
                {
                    e.Handled = true;
                    break;
                }
            }
        }

        private void text_doctorname_TextChanged(object sender, EventArgs e)
        {
            if (text_doctorname.Text != "")
            {
                errorProvider1.Dispose();
                label_doctor_error.Hide();
            }
        }

        private void text_password_TextChanged(object sender, EventArgs e)
        {
            if (text_password.Text != "" && text_PassConfrim.Text != "")
            {
                errorProvider1.Dispose();
                labPasword.Hide();
            }
        }

        private void text_PassConfrim_TextChanged(object sender, EventArgs e)
        {
           if (text_password.Text != "" && text_PassConfrim.Text != "")
            {
                if(text_password.Text == text_PassConfrim.Text )
                {
                    errorProvider1.Dispose();
                    label69.Hide();
                    labPasword.Hide();
                }
                else
                {
                    errorProvider1.SetError(text_PassConfrim, "error");
                    label69.Show();
                }
                errorProvider1.Dispose();
                labPasword.Hide();
            }
        }

        private void text_mobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 10 && !char.IsDigit(ch))
            {
                e.Handled = true;
                errorProvider1.SetError(text_mobile, "error");
            }
            else
            {
                if (text_mobile.TextLength != 10 && text_mobile.Text != "")
                {
                    e.Handled = false;
                    errorProvider1.Dispose();
                }
                else if (text_mobile.TextLength == 10)
                {
                    e.Handled = false;
                    errorProvider1.Dispose();
                    Lab_InvalidNumber.Hide();
                }
            }
        }

        private void text_mobile_Leave(object sender, EventArgs e)
        {
            if (text_mobile.TextLength != 10 && text_mobile.Text != "")
            {
                Lab_InvalidNumber.Visible = true;
                return;
            }
            else
                Lab_InvalidNumber.Visible = false;
        }

        private void text_email_KeyPress(object sender, KeyPressEventArgs e)
        {
            errorProvider1.Dispose();
            label_email_error.Hide();
        }
        private void text_email_Leave(object sender, EventArgs e)
        {
            string _mail = this.cntrl.GetMailId(text_email.Text);
            if (dt_mail.Rows.Count == 0)
            {
                flag_mail = true;
            }
            else
            {
                flag_mail = false;
            }
        }

        private void radio_login_yes_CheckedChanged(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            lab_Activation.Hide();
        }

        private void radio_login_no_CheckedChanged(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            lab_Activation.Hide();
        }

        private void choosecolor_Click(object sender, EventArgs e)
        {
            panel_color.Hide();
            choosecolor.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel_color.Show();
            choosecolor.Show();
        }
        private void label57_Click(object sender, EventArgs e)
        {
            calendrcolor = "0";
            Color a = label57.ForeColor;
            button1.BackColor = a;
            panel_color.Hide();
            choosecolor.Hide();
        }

        private void label58_Click(object sender, EventArgs e)
        {
            calendrcolor = "1";
            Color a = label58.ForeColor;
            button1.BackColor = a;
            panel_color.Hide();
            choosecolor.Hide();
        }

        private void label59_Click(object sender, EventArgs e)
        {
            calendrcolor = "2";
            Color a = label59.ForeColor;
            button1.BackColor = a;
            panel_color.Hide();
            choosecolor.Hide();
        }

        private void label60_Click(object sender, EventArgs e)
        {
            calendrcolor = "3";
            Color a = label60.ForeColor;
            button1.BackColor = a;
            panel_color.Hide();
            choosecolor.Hide();
        }

        private void label61_Click(object sender, EventArgs e)
        {
            calendrcolor = "4";
            Color a = label61.ForeColor;
            button1.BackColor = a;
            panel_color.Hide();
            choosecolor.Hide();
        }

        private void label62_Click(object sender, EventArgs e)
        {
            calendrcolor = "5";
            Color a = label62.ForeColor;
            button1.BackColor = a;
            panel_color.Hide();
            choosecolor.Hide();
        }

        private void label56_Click(object sender, EventArgs e)
        {
            calendrcolor = "6";
            Color a = label56.ForeColor;
            button1.BackColor = a;
            panel_color.Hide();
            choosecolor.Hide();
        }

        private void label63_Click(object sender, EventArgs e)
        {
            calendrcolor = "7";
            Color a = label63.ForeColor;
            button1.BackColor = a;
            panel_color.Hide();
            choosecolor.Hide();
        }

        private void label64_Click(object sender, EventArgs e)
        {
            calendrcolor = "8";
            Color a = label64.ForeColor;
            button1.BackColor = a;
            panel_color.Hide();
            choosecolor.Hide();
        }

        private void label65_Click(object sender, EventArgs e)
        {
            calendrcolor = "9";
            Color a = label65.ForeColor;
            button1.BackColor = a;
            panel_color.Hide();
            choosecolor.Hide();
        }

        private void label66_Click(object sender, EventArgs e)
        {
            calendrcolor = "10";
            Color a = label66.ForeColor;
            button1.BackColor = a;
            panel_color.Hide();
            choosecolor.Hide();
        }

        private void button__manage_addoctor_Click(object sender, EventArgs e)
        {
            panel_manage.Hide();
            errorProvider1.Dispose();
        }
        private void button_savedoctor_Click(object sender, EventArgs e)
        {
            if (radio_login_no.Checked == true)
            {
                status = "No";
            }
            if (radio_login_yes.Checked == true)
            {
                status = "Yes";
            }
            if (text_password.Text != "" && text_password.Text == text_PassConfrim.Text && flag_mail == true)
            {
                if (radio_login_no.Checked == true || radio_login_yes.Checked == true)
                {
                    if(cmbStaffType.Text.Trim()=="DOCTOR")
                    {
                        if(txt_fee.Text!="" && txt_followup_fee.Text!="" && txt_period.Text !="")
                        {

                        }
                        else
                        {
                            MessageBox.Show("Mandatory feilds must be fill...", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    this.cntrl.save_staff(text_doctorname.Text, text_mobile.Text, text_email.Text, text_reg_no.Text, calendrcolor, status, cmbStaffType.Text.Trim(), text_PassConfrim.Text, txt_fee.Text, txt_followup_fee.Text, txt_period.Text);
                    DataTable dt_maxid = this.cntrl.get_dr_maxid();
                    if(dt_maxid.Rows.Count>0)
                    {
                       this.cntrl.save_login(text_email.Text, text_PassConfrim.Text, cmbStaffType.Text, dt_maxid.Rows[0][0].ToString());
                    }
                    MessageBox.Show("Successfully Saved !!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    text_doctorname.Clear();
                    text_email.Clear();
                    text_mobile.Clear();
                    text_reg_no.Clear();
                    text_PassConfrim.Clear();
                    text_password.Clear();
                    txt_fee.Text = ""; 
                    txt_followup_fee.Text = "";
                    txt_period.Text = "";
                    radio_login_yes.Checked = false;
                    radio_login_no.Checked = false;
                    DataTable dtb = this.cntrl.Fill_StaffGrid();
                    FillStaffGrid(dtb);
                }
                else
                {
                    errorProvider1.SetError(label7, "Choose Yes Or No");
                }
            }
            else if (flag_mail == false)
            {
                MessageBox.Show("Email Id already exist..", "Existed Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Please Confirm the password Correctly..", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_closedoctor_Click(object sender, EventArgs e)
        {
            panel_manage.Show();
            errorProvider1.Dispose();
            DataTable dtb = this.cntrl.Fill_StaffGrid();
            FillStaffGrid(dtb);
        }

        private void Practice_Staff_Setting_Activated(object sender, EventArgs e)
        {
            if (chkPATadd.Checked == true && chkPATedit.Checked == true && chkPATdelete.Checked == true)
            {
                chkPAT.Checked = true;
            }
            else
            {
                chkPAT.Checked = false;
            }
            if (chkAPTadd.Checked == true && chkAPTedit.Checked == true && chkAPTdelete.Checked == true && chkAPTClinicAppoinment.Checked == true)
            {
                chkAPT.Checked = true;
            }
            else
            {
                chkAPT.Checked = false;
            }
            if (chkPMTadd.Checked == true)
            {
                chkPMT.Checked = true;
            }
            else
            {
                chkPMT.Checked = false;
            }
        }

        private void dataGridView_Staff_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_staff_availa_add_Click(object sender, EventArgs e)
        {
            if(DGV_Staff.Rows.Count>0)
            {
                int j = 0;
                for (int i=0;i<DGV_Staff.Rows.Count;i++)
                {
                    if (DGV_Staff.CurrentCell is DataGridViewCheckBoxCell)
                        DGV_Staff.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    bool value = Convert.ToBoolean(DGV_Staff.Rows[i].Cells[5].EditedFormattedValue);
                    string name = DGV_Staff.Rows[i].Cells[0].Value.ToString();
                    if (value == true)
                    {
                       j= this.cntrl.Save_doctor_availability(name);
                    }
                    else if (value == false)
                    {
                       j= this.cntrl.update_doctor_availability(name);
                    }
                }
                if( j==1)
                {
                    MessageBox.Show("Saved Successfully !..", "Data Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void chkRPTINCadd_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkRPTINCadd.Checked == true)
            {
                chkRPT.Checked = true;
            }
        }

        private void chkRPTIncom_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkRPTIncom.Checked == true)
            {
                chkRPT.Checked = true;
            }
        }

        private void chkRPTAPTadd_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkRPTAPTadd.Checked == true)
            {
                chkRPT.Checked = true;
            }
        }

        private void chkRPTPATadd_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkRPTPATadd.Checked == true)
            {
                chkRPT.Checked = true;
            }
        }

        private void chkRPTEMRadd_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkRPTEMRadd.Checked == true)
            {
                chkRPT.Checked = true;
            }
        }

        private void chkRPTPAYadd_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkRPTPAYadd.Checked == true)
            {
                chkRPT.Checked = true;
            }
        }

        private void chkRPTINVadd_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkRPTINVadd.Checked == true)
            {
                chkRPT.Checked = true;
            }
        }

        private void chkAPTClinicAppoinment_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkAPTClinicAppoinment.Checked == true)
            {
                chkRPT.Checked = true;
            }
        }

        private void txt_fee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            //string a = txt_Qty.Text;
            //string b = a.TrimStart('0');
            //txt_Qty.Text = b;
        }

        private void txt_followup_fee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_period_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void chk_inv_CheckedChanged(object sender, EventArgs e)
        {
            if(flag_ty==false)
            {
                if (chk_inv.Checked == true)
                {
                    chkSales.Checked = true;
                    chkPurchase.Checked = true;
                    chkstocktransfer.Checked = true;
                    chkstockledger.Checked = true;
                    chkadjustment.Checked = true;
                }
                else
                {
                    chkSales.Checked = false;
                    chkPurchase.Checked = false;
                    chkstocktransfer.Checked = false;
                    chkstockledger.Checked = false;
                    chkadjustment.Checked = false;
                }
            }
           
        }

        private void btn_staff_refresh_Click(object sender, EventArgs e)
        {
            if(DGV_Staff.Rows.Count>=0)
            {
                DataTable staff = this.cntrl.LoadStaff();
                if(staff.Rows.Count>0)
                {
                    for (int i = 0; i < staff.Rows.Count; i++)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row = DGV_Staff.Rows[i];
                        string confirmsms = staff.Rows[i]["availability"].ToString();
                        if (confirmsms == "Unavailabile")
                        {
                            row.Cells[5].Value = true;
                        }
                    }
                }
               
            }
        }

        private void text_password_ModifiedChanged(object sender, EventArgs e)
        {

        }
        public static bool hasSpecialChar(string input)//bhj
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (char item in specialChar)
            {
                if (input.Contains(item.ToString()))
                    return true;
               
            }
           
            return false;


        }

        private void text_password_Leave(object sender, EventArgs e)//bhj
        {
            hasSpecialChar(text_password.Text);
            //errorProvider1.Dispose();
            if (hasSpecialChar(text_password.Text) == false)
            {
                errorProvider1.SetError(text_password, "Password should contain at least one special case character");
                labPasword.Show();
            }
            else if(hasSpecialChar(text_password.Text) ==true)
            {
                errorProvider1.Dispose();
                labPasword.Hide();
            }


        }
    }
}
