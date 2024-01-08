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
    public partial class appoinment_cancellation_reason : Form
    {
        public appoinment_cancellation_reason()
        {
            InitializeComponent();
        }

        private void appoinment_cancellation_reason_Load(object sender, EventArgs e)
        {
            txt_reason.Text = "";
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            var form2 = new Main_Calendar(txt_reason.Text);
            form2.Closed += (sender1, args) => this.Close();
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            var form2 = new Main_Calendar("Cancel");
            form2.Closed += (sender1, args) => this.Close();
            this.Close();
        }
    }
}
