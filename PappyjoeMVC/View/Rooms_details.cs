using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PappyjoeMVC.Model;
using PappyjoeMVC.Controller;

namespace PappyjoeMVC.View
{
    public partial class Rooms_details : Form
    {
        Connection db = new Connection();
        Room_controller cntrl = new Room_controller();
        public string doctor_id = "", patient_id = "";
        public static bool flag = false;

        public Rooms_details()
        {
            InitializeComponent();
        }

        private void Rooms_details_Load(object sender, EventArgs e)
        {
            //booking_display();
            lb_date.Text = DateTime.Now.ToLongDateString();
            DataTable dtb = this.cntrl.Rooms();
            Rooms();
            buttonOverview();
            lst_patients.Visible = false; lst_patients.Location = new Point(txt_PBsearchPat.Location.X, txt_PBsearchPat.Location.Y + 22);
            pnl_PatientBook.Visible = false;
        }

        
        private void booking_display()
        {
            Rooms_flowpanel.Controls.Clear();
            DataTable rooms = this.cntrl.Rooms();
            if (rooms.Rows.Count>0)
            {
                ListItem[] lst = new ListItem[rooms.Rows.Count];

                for (int i = 0; i < lst.Length; i++)
                {
                    lst[i] = new ListItem();
                    lst[i].Room = rooms.Rows[i]["room_no"].ToString();
                    lst[i].RoomId = Convert.ToInt32( rooms.Rows[i]["id"].ToString());
                    //lst[i].Status= rooms.Rows[i]["gender"].ToString();
                    //lst[i].Patient = rooms.Rows[i]["pt_name"].ToString();
                    Rooms_flowpanel.Controls.Add(lst[i]);
                    if (lst[i].BackColor==Color.Green)
                    {
                        
                        lst[i].MouseClick += new MouseEventHandler(ListItem_MouseClick);
                    }
                }
            }
            else
            {
                Rooms_flowpanel.Controls.Clear();
            }
        }
        void ListItem_MouseClick(object sender, EventArgs e)
        {
            pnl_PatientBook.Visible = true;
            pnl_PatientBook.Location = new Point(pnl_main.Location.X + 20, pnl_main.Location.Y + 20);
            lb_PBadmn.Text = DateTime.Now.ToString();
        }

        private void btn_AddRoom_Click(object sender, EventArgs e)
        {
            if (txt_roomno.Text!="")
            {
                if(btn_AddRoom.Text=="Add")
                {
                    int i = this.cntrl.addroom(txt_roomno.Text);
                    Rooms();
                    txt_roomno.Text = "";

                }
                else
                {
                    int i = this.cntrl.update_room(room_id, txt_roomno.Text);
                    Rooms();
                    txt_roomno.Text = "";/////
                }
                
            }
        }

        
        private void Rooms()
        {
            DataTable rooms = this.cntrl.Rooms();
            if (rooms.Rows.Count>0)
            {
                dgv_rooms.Rows.Clear();
                for (int i = 0; i < rooms.Rows.Count; i++)
                {
                    dgv_rooms.Rows.Add();
                    dgv_rooms.Rows[i].Cells["id"].Value = rooms.Rows[i]["id"].ToString();
                    dgv_rooms.Rows[i].Cells["room"].Value = rooms.Rows[i]["room_no"].ToString();
                }
                
            }
            else
            {
                dgv_rooms.Rows.Clear();
            }
            foreach (DataGridViewColumn column in dgv_rooms.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dgv_rooms.EnableHeadersVisualStyles = false;
            dgv_rooms.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dgv_rooms.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv_rooms.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            dgv_rooms.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv_rooms.ColumnHeadersVisible = true;
            dgv_rooms.ScrollBars = ScrollBars.Vertical;
        }

        private void btn_settings_Click(object sender, EventArgs e)
        {
            buttonSettings();
        }

        private void btn_Overview_Click(object sender, EventArgs e)
        {
            buttonOverview();
        }
        private void buttonOverview()
        {
            btn_Overview.BackColor = Color.White;
            btn_Overview.ForeColor = Color.RoyalBlue;
            btn_settings.BackColor = Color.RoyalBlue;
            btn_settings.ForeColor = Color.White;
            pnl_settings.Visible = false;
            booking_display();
        }
        private void buttonSettings()
        {
            btn_settings.BackColor = Color.White;
            btn_settings.ForeColor = Color.RoyalBlue;
            btn_Overview.BackColor = Color.RoyalBlue;
            btn_Overview.ForeColor = Color.White;
            pnl_settings.Visible = true;
        }

        private void txt_PBsearchPat_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && lst_patients.Items.Count > 0)
            {
                lst_patients.Focus();
            }
            else if (e.KeyCode == Keys.Enter && lst_patients.Items.Count > 0)
            {
                if (lst_patients.SelectedItems.Count > 0)
                {
                    string value = lst_patients.SelectedValue.ToString();
                    DataTable patient = new DataTable();
                    patient = this.cntrl.get_patient_details(value);
                    if (patient.Rows.Count > 0)
                    {
                        lb_PBname.Text = patient.Rows[0]["pt_name"].ToString();
                        lb_PBpatId.Text = patient.Rows[0]["pt_id"].ToString();
                        patient_id = patient.Rows[0]["id"].ToString();
                        lb_PBmobNo.Text = patient.Rows[0]["primary_mobile_number"].ToString();
                        lst_patients.Visible = false;
                        txt_PBsearchPat.Focus();
                    }
                }
            }
        }

        private void txt_PBsearchPat_Click(object sender, EventArgs e)
        {
            if (txt_PBsearchPat.Text == "Search by Patient Name")
            {
                txt_PBsearchPat.Text = "";
            }
            txt_PBsearchPat.Text = "";
            lb_PBpatId.Text = "";
            if (flag == false)
            {
                if (txt_PBsearchPat.Text != "")
                {
                    // lst_patients.Show();
                    lst_patients.Location = new Point(txt_PBsearchPat.Location.X, txt_PBsearchPat.Location.Y + 22);
                    DataTable dtdr = this.cntrl.search_patient(txt_PBsearchPat.Text);//srch_patient(txt_PBsearchPat.Text, txt_PBsearchPat.Text);
                    lst_patients.DataSource = dtdr;
                    lst_patients.DisplayMember = "patient";
                    lst_patients.ValueMember = "id";
                }
                else
                {
                    DataTable dtdr = this.cntrl.search_patient(txt_PBsearchPat.Text);
                    lst_patients.DataSource = dtdr;
                    lst_patients.DisplayMember = "patient";
                    lst_patients.ValueMember = "id";
                }
                if (lst_patients.Items.Count > 0)
                {
                    lst_patients.Show();
                }
                else
                {
                    lst_patients.Hide();
                }
            }
        }

        private void lst_patients_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && lst_patients.Items.Count > 0)
            {
                if (lst_patients.SelectedItems.Count > 0)
                {
                    string value = lst_patients.SelectedValue.ToString();
                    DataTable patient = new DataTable();
                    patient = this.cntrl.get_patient_details(value);
                    if (patient.Rows.Count > 0)
                    {
                        lb_PBname.Text = patient.Rows[0]["pt_name"].ToString();
                        lb_PBpatId.Text = patient.Rows[0]["pt_id"].ToString();
                        patient_id = patient.Rows[0]["id"].ToString();
                        lb_PBmobNo.Text = patient.Rows[0]["primary_mobile_number"].ToString();
                        lst_patients.Visible = false;
                        txt_PBsearchPat.Focus();
                    }
                }
            }
        }

        private void lst_patients_MouseClick(object sender, MouseEventArgs e)
        {
            if (lst_patients.SelectedItems.Count > 0)
            {
                string value = lst_patients.SelectedValue.ToString();
                DataTable patient = new DataTable();
                patient = this.cntrl.get_patient_details(value);
                if (patient.Rows.Count > 0)
                {
                    lb_PBname.Text = patient.Rows[0]["pt_name"].ToString();
                    lb_PBpatId.Text = patient.Rows[0]["pt_id"].ToString();
                    patient_id = patient.Rows[0]["id"].ToString();
                    lb_PBmobNo.Text = patient.Rows[0]["primary_mobile_number"].ToString();
                    lst_patients.Visible = false;
                }
                else
                {
                    MessageBox.Show("Please choose  Correct patient....", "Data Not Found..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_CancelBookingpnl_Click(object sender, EventArgs e)
        {
            pnl_PatientBook.Visible = false;
        }

        private void btn_SaveBooking_Click(object sender, EventArgs e)
        {
            
        }
        public string room_id = "";
        private void dgv_rooms_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            {
                room_id = dgv_rooms.CurrentRow.Cells["id"].Value.ToString();
                if(dgv_rooms.CurrentCell.OwningColumn.Name=="edit")
                {
                    txt_roomno.Text= dgv_rooms.CurrentRow.Cells["room"].Value.ToString();
                    btn_AddRoom.Text = "Update";
                }
                if (dgv_rooms.CurrentCell.OwningColumn.Name == "delete")
                {
                    DialogResult res = MessageBox.Show("Are you sure you want to delete?", "Delete confirmation",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.No)
                    {
                    }
                    else
                    {
                        this.cntrl.delete(room_id);
                        dgv_rooms.Rows.RemoveAt(e.RowIndex);

                    } 
                }
            }
        }

        private void txt_PBsearchPat_TextChanged(object sender, EventArgs e)
        {
            if (flag == false)
            {
                if (txt_PBsearchPat.Text != "")
                {
                    // lst_patients.Show();
                    lst_patients.Location = new Point(txt_PBsearchPat.Location.X, txt_PBsearchPat.Location.Y + 22);
                    DataTable dtdr = this.cntrl.search_patient(txt_PBsearchPat.Text);//srch_patient(txt_PBsearchPat.Text, txt_PBsearchPat.Text);
                    lst_patients.DataSource = dtdr;
                    lst_patients.DisplayMember = "patient";
                    lst_patients.ValueMember = "id";
                }
                else
                {
                    DataTable dtdr = this.cntrl.search_patient(txt_PBsearchPat.Text);
                    lst_patients.DataSource = dtdr;
                    lst_patients.DisplayMember = "patient";
                    lst_patients.ValueMember = "id";
                }
                if (lst_patients.Items.Count > 0)
                {
                    lst_patients.Show();
                }
                else
                {
                    lst_patients.Hide();
                }
            }
        }
    }
}
