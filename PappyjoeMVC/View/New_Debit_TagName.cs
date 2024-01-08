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
    public partial class New_Debit_TagName : Form
    {
        TagnameDebit_controller cntrl = new TagnameDebit_controller();

        public New_Debit_TagName()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtTagName.Text))
                {
                    DataTable Account_Name = this.cntrl.submit(txtTagName.Text);
                    if (Account_Name.Rows.Count > 0)
                    {
                        MessageBox.Show("Already exist", "Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTagName.Focus();
                        txtTagName.Clear();
                    }
                    else
                    {
                        int i = this.cntrl.insert(txtTagName.Text);
                        MessageBox.Show("Successfully Submited !!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTagName.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
