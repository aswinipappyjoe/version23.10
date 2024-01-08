using PappyjoeMVC.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PappyjoeMVC.View
{
    public partial class Inactive_patients : Form
    {
        Patients_controller cntrl = new Patients_controller();
        Profile_Details_controller ctrl = new Profile_Details_controller();
        public string doctor_id = "";
        public Inactive_patients()
        {
            InitializeComponent();
        }

        private void Inactive_patients_Load(object sender, EventArgs e)
        {
            Fillgrid();
            Design_Datagrid();
        }

        public void Fillgrid()
        {
            DataTable dtb = this.cntrl.innactive_patients();
            DGVInactive.ColumnHeadersVisible = true;
            DGVInactive.Columns.Clear();
            DGVInactive.Rows.Clear();
            foreach (DataColumn column in dtb.Columns)
            {
                DGVInactive.Columns.Add(column.ColumnName, column.ColumnName);
            }
            if (DGVInactive.Columns.Count > 0)
            {
                for (int j = 0; j < dtb.Rows.Count; j++)
                {
                    DGVInactive.Rows.Add();
                    for (int i = 0; i < dtb.Columns.Count; i++)
                    {
                        DGVInactive.Rows[j].Cells[i].Value = dtb.Rows[j][i].ToString();
                        DGVInactive.RowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
                        DGVInactive.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        DGVInactive.SelectionMode = DataGridViewSelectionMode.CellSelect;
                        DGVInactive.AllowUserToResizeColumns = false;
                        DGVInactive.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                        DGVInactive.Rows[j].Cells[i].Style.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Regular);
                        DGVInactive.CellBorderStyle = DataGridViewCellBorderStyle.None;
                    }
                }
                DGVInactive.Columns[0].Visible = false;
            }
        }

        public void Design_Datagrid()
        {
            if (DGVInactive.Columns.Count > 0)
            {
                DGVInactive.Columns[0].Width = 1;
                DGVInactive.Columns[1].Width = 45;
                DGVInactive.Columns[2].Width = 200;
                DGVInactive.Columns[3].Width = 60;
                DGVInactive.Columns[4].Width = 40;
                //DGVInactive.Columns[5].Width = 80;
                //DGVInactive.Columns[6].Width = 140;
                //DGVInactive.Columns[7].Width = 140;
                //DGVInactive.Columns[8].Width = 100;
            }
            DGVInactive.EnableHeadersVisualStyles = false;
            DGVInactive.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            DGVInactive.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DGVInactive.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            DGVInactive.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            DGVInactive.ColumnHeadersVisible = true;
            DGVInactive.ScrollBars = ScrollBars.Vertical;
            foreach (DataGridViewColumn column in DGVInactive.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        public string patient_id = "";
        private void DGVInactive_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            {
                patient_id = DGVInactive.Rows[e.RowIndex].Cells[0].Value.ToString();
                DataTable dtb = this.ctrl.Get_Patient_details(patient_id);
                patientload(dtb);
            }
           
        }
        public void patientload(DataTable rs_patients)
        {
            if (rs_patients.Rows.Count > 0)
            {
                if (rs_patients.Rows[0]["pt_name"].ToString() != "")
                {
                    txtPatientName.Text = rs_patients.Rows[0]["pt_name"].ToString();                    
                }
                if (rs_patients.Rows[0]["pt_id"].ToString() != "")
                {
                    txtPatientId.Text = rs_patients.Rows[0]["pt_id"].ToString();                    
                }
                if (rs_patients.Rows[0]["aadhar_id"].ToString() != "")
                {
                    txtAdhaarId.Text = rs_patients.Rows[0]["aadhar_id"].ToString();
                }
                if (rs_patients.Rows[0]["gender"].ToString() != "")
                {
                    txtGender.Text = rs_patients.Rows[0]["gender"].ToString();
                }
                if (rs_patients.Rows[0]["date_of_birth"].ToString() != "")
                {
                    txtDob.Text = DateTime.Parse(rs_patients.Rows[0]["date_of_birth"].ToString()).ToString("MM/dd/yyyy");
                }
                if (rs_patients.Rows[0]["primary_mobile_number"].ToString() != "")
                {
                    txtPrimaryMobNo.Text = rs_patients.Rows[0]["primary_mobile_number"].ToString();
                }
                if (rs_patients.Rows[0]["age"].ToString() != "")
                {
                    if (Convert.ToInt32(rs_patients.Rows[0]["age"].ToString()) != 0)
                    {
                        txtAge.Text = rs_patients.Rows[0]["age"].ToString();
                    }
                }
                if (rs_patients.Rows[0]["Opticket"].ToString() != "")
                {
                    txtopticket.Text = rs_patients.Rows[0]["Opticket"].ToString();
                }
                if (rs_patients.Rows[0]["Visited"].ToString() != "")
                {
                    if (DateTime.Parse(rs_patients.Rows[0]["Visited"].ToString()).ToString("MM/dd/yyyy") != "")
                    {
                        txtvisiteddate.Text = DateTime.Parse(rs_patients.Rows[0]["Visited"].ToString()).ToString("MM/dd/yyyy");
                    }
                }
            }
        }

        public void clear()
        {
            txtPatientName.Text = "";
            txtPatientId.Text = "";
            txtAdhaarId.Text = "";
            txtGender.Text = "";
            txtDob.Text = "";
            txtPrimaryMobNo.Text = "";
            txtAge.Text = "";
            txtopticket.Text = "";
            txtvisiteddate.Text = "";
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_toActive_Click(object sender, EventArgs e)
        {
            int toactive = this.ctrl.toActivePatient(patient_id);
            Fillgrid();
            Design_Datagrid();
            clear();
        }
    }
}
