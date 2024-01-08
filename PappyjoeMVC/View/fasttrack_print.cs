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
    public partial class fasttrack_print : Form
    {
        public fasttrack_print()
        {
            InitializeComponent();
        }
        public  bool vital_print = false;
        public  bool clinic_print = false;
        public  bool treat_print = false;
        public  bool pres_print = false;
        private void btn_print_Click(object sender, EventArgs e)
        {
            //Fasttrack_window_New frm = new Fasttrack_window_New();
            //if (chk_selectall.Checked == true)
            //{
            //    clinic_print = true;
            //    vital_print = true;
            //    pres_print = true;
            //    treat_print = true;
            //}
             if (chk_clinic.Checked == true)
                clinic_print = true;
             else
                clinic_print = false;
            if (chk_vitals.Checked == true)
                vital_print = true;
            else
                vital_print = false;
            if (chk_prescription.Checked == true)
                pres_print = true;
            else
                pres_print = false;
            if (chk_invoice.Checked == true)
                treat_print = true;
            else
                treat_print = false;

            var frm = new Fasttrack_window_New(clinic_print, vital_print, pres_print, treat_print);
            frm.Closed += (sender1, args) => this.Close();
            this.Close();
        }
    }
}
