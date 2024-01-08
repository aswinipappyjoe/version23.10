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
    public partial class Patient_Previous_History : Form
    {
        public string doctor_id = "";
        public string patient_id = "";
        Profile_Details_controller cntrl = new Profile_Details_controller();

        public Patient_Previous_History()
        {
            InitializeComponent();
        }

        private void Patient_Previous_History_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtb = this.cntrl.Get_Patient_details(patient_id);
                if (dtb.Rows.Count > 0)
                {
                    txt_notes.Text = dtb.Rows[0]["Notes"].ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
