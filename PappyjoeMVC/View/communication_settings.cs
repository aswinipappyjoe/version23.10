using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using PappyjoeMVC.Controller;

namespace PappyjoeMVC.View
{
    public partial class Communication_Settings : Form
    {
        public Communication_Settings()
        {
            InitializeComponent();
        }
        Communication_Setting_controller cntrl=new Communication_Setting_controller();
        private void communication_settings_Load(object sender, EventArgs e)
        {
            DataTable sms =this.cntrl. getsmstabledata();
            if (sms.Rows.Count > 0)
            {
                if (String.IsNullOrWhiteSpace(sms.Rows[0]["smsName"].ToString()))
                {
                    label24.Visible = true;
                }
                else
                {
                    label24.Visible = false;
                    textSmsUname.Text = sms.Rows[0]["smsName"].ToString();
                }
                if (String.IsNullOrWhiteSpace(sms.Rows[0]["smsPass"].ToString()))
                {
                    label24.Visible = true;
                }
                else
                {
                    label24.Visible = false;
                    textSmsPassword.Text = sms.Rows[0]["smsPass"].ToString();
                }
                if (String.IsNullOrWhiteSpace(sms.Rows[0]["emailName"].ToString()))
                {
                    label25.Visible = true;
                }
                else
                {
                    label25.Visible = false;
                    textEmailUname.Text = sms.Rows[0]["emailName"].ToString();
                }
                if (String.IsNullOrWhiteSpace(sms.Rows[0]["emailPass"].ToString()))
                {
                    label25.Visible = true;
                }
                else
                {
                    label25.Visible = false;
                    textEmailPassword.Text = sms.Rows[0]["emailPass"].ToString();
                }
            }
            else
            {
                label24.Visible = true;
                label25.Visible = true;
            }
            DataTable dt = this.cntrl.ReminderTime();
            if (dt.Rows.Count>0)
            {
                if (dt.Rows[0]["patientRemsmsTime"].ToString() != "")
                {
                    txtScheduletime.Text = dt.Rows[0]["patientRemsmsTime"].ToString();
                }
                else
                {
                    this.cntrl.save_ScheduleTime("01:00:00");
                }
                if (dt.Rows[0]["doctorApptCountsmsTime"].ToString() != "")
                {
                    txt_scheduleTimeDoc.Text = dt.Rows[0]["doctorApptCountsmsTime"].ToString();
                }
                else
                {
                    this.cntrl.updateDocRemTme("07:00:00");
                }
                if (dt.Rows[0]["pat_welcSMS"].ToString() != "0")
                {
                    cbPatWelcSms.Checked = true;
                }
                if (dt.Rows[0]["pat_appoRemSMS"].ToString() != "0")
                {
                    cbPatAptmnRemSms.Checked = true;
                }
                if (dt.Rows[0]["doc_appoCntSMS"].ToString() != "0")
                {
                    cbDocApntCntSms.Checked = true;
                }
            }
            DataTable smscnt = this.cntrl.getsmscnt();
            if (smscnt.Rows[0]["sms"].ToString() == "")
            {
                lbSMSbalance.Text = "0";
            }
            else
            {
                lbSMSbalance.Text = PappyjoeMVC.Model.EncryptionDecryption.Decrypt(smscnt.Rows[0]["sms"].ToString(), "ch3lSeAW0n2o2!C1");
            }
        }

        private void buttonSms_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable sms = this.cntrl.Getsmaname();
                if (sms.Rows.Count > 0)
                {
                    this.cntrl.update_sms(textSmsUname.Text, textSmsPassword.Text);
                    MessageBox.Show("Successfully Updated !!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    this.cntrl.save_sms(textSmsUname.Text, textSmsPassword.Text);
                    MessageBox.Show("Successfully Saved !!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                DataTable sms1 = this.cntrl.getsmstabledata();
                if (sms.Rows.Count > 0)
                {
                    if (String.IsNullOrWhiteSpace(sms1.Rows[0]["smsName"].ToString()))
                    {
                        label24.Visible = true;
                    }
                    else
                    {
                        label24.Visible = false;
                        textSmsUname.Text = sms1.Rows[0]["smsName"].ToString();
                    }
                    if (String.IsNullOrWhiteSpace(sms1.Rows[0]["smsPass"].ToString()))
                    {
                        label24.Visible = true;
                    }
                    else
                    {
                        label24.Visible = false;
                        textSmsPassword.Text = sms1.Rows[0]["smsPass"].ToString();
                    }
                    if (String.IsNullOrWhiteSpace(sms1.Rows[0]["emailName"].ToString()))
                    {
                        label25.Visible = true;
                    }
                    else
                    {
                        label25.Visible = false;
                        textEmailUname.Text = sms1.Rows[0]["emailName"].ToString();
                    }
                    if (String.IsNullOrWhiteSpace(sms1.Rows[0]["emailPass"].ToString()))
                    {
                        label25.Visible = true;
                    }
                    else
                    {
                        label25.Visible = false;
                        textEmailPassword.Text = sms1.Rows[0]["emailPass"].ToString();
                    }
                }
                else
                {
                    label24.Visible = true;
                    label25.Visible = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void buttonEmail_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable sms = this.cntrl.Getsmaname();
                if (sms.Rows.Count > 0)
                {
                    this.cntrl.update_email(textEmailUname.Text, textEmailPassword.Text);
                    MessageBox.Show("Successfully Updated !!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    this.cntrl.Save_email(textEmailUname.Text, textEmailPassword.Text);
                    MessageBox.Show("Successfully Saved !!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                DataTable sms1 = this.cntrl.getsmstabledata();
                if (sms1.Rows.Count > 0)
                {
                    if (String.IsNullOrWhiteSpace(sms1.Rows[0]["smsName"].ToString()))
                    {
                        label24.Visible = true;
                    }
                    else
                    {
                        label24.Visible = false;
                        textSmsUname.Text = sms1.Rows[0]["smsName"].ToString();
                    }
                    if (String.IsNullOrWhiteSpace(sms1.Rows[0]["smsPass"].ToString()))
                    {
                        label24.Visible = true;
                    }
                    else
                    {
                        label24.Visible = false;
                        textSmsPassword.Text = sms1.Rows[0]["smsPass"].ToString();
                    }
                    if (String.IsNullOrWhiteSpace(sms1.Rows[0]["emailName"].ToString()))
                    {
                        label25.Visible = true;
                    }
                    else
                    {
                        label25.Visible = false;
                        textEmailUname.Text = sms1.Rows[0]["emailName"].ToString();
                    }
                    if (String.IsNullOrWhiteSpace(sms1.Rows[0]["emailPass"].ToString()))
                    {
                        label25.Visible = true;
                    }
                    else
                    {
                        label25.Visible = false;
                        textEmailPassword.Text = sms1.Rows[0]["emailPass"].ToString();
                    }
                }
                else
                {
                    label24.Visible = true;
                    label25.Visible = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        private void btnSaveScheduledtime_Click(object sender, EventArgs e)
        {
            bool isOK = Regex.IsMatch(txtScheduletime.Text, @"[0-2][0-9]\:[0-6][0-9]\:[0-5][0-9]");
            if (isOK)
            {
                string beforetime = txtScheduletime.Text.ToString();
                this.cntrl.save_ScheduleTime(beforetime);
                MessageBox.Show("Scheduling time is saved", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("The time format is incorrect","Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_SavescheduleTimeDoc_Click(object sender, EventArgs e)
        {
            bool isOK = Regex.IsMatch(txt_scheduleTimeDoc.Text, @"[0-2][0-9]\:[0-6][0-9]\:[0-5][0-9]");
            if (isOK)
            {
                string Doctime = txt_scheduleTimeDoc.Text.ToString();
                this.cntrl.updateDocRemTme(Doctime);
                MessageBox.Show("Scheduling time is saved", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("The time format is incorrect", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cbPatWelcSms_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPatWelcSms.Checked==true)
            {
                this.cntrl.WelcSms(1);
            }
            else
            {
                this.cntrl.WelcSms(0);
            }
        }

        private void cbPatAptmnRemSms_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPatAptmnRemSms.Checked == true)
            {
                this.cntrl.patAppRem(1);
            }
            else
            {
                this.cntrl.patAppRem(0);
            }
        }

        private void cbDocApntCntSms_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDocApntCntSms.Checked == true)
            {
                this.cntrl.doc_appoCntSMS(1);
            }
        }

        private void btn_pwrdOK_Click(object sender, EventArgs e)
        {
            if (txtSMScountPwd.Text=="123")
            {
                txtSMScount.Show();
                label12.Show();
                btn_saveSmsCount.Show();
                label10.Visible = false;
                txtSMScountPwd.Visible = false;
                btn_pwrdOK.Visible = false;
                txtSMScount.Location = new Point(10, 25);
                label12.Location = new Point(10, 7);
                btn_saveSmsCount.Location = new Point(186, 24);
            }
            txtSMScountPwd.Text = "";
        }

        private void btn_saveSmsCount_Click(object sender, EventArgs e)
        {
            txtSMScount.Visible = false;
            label12.Visible = false;
            btn_saveSmsCount.Visible = false;
            label10.Visible = true;
            txtSMScountPwd.Visible = true;
            btn_pwrdOK.Visible = true;
            pnlSmsCountset.Visible = false;
             
            if (txtSMScount.Text!="")
            {
                string Encrypt = PappyjoeMVC.Model.EncryptionDecryption.Encrypt(txtSMScount.Text, "ch3lSeAW0n2o2!C1");
                this.cntrl.smsCount(Encrypt);
            }
            
            DataTable dt = this.cntrl.getsmscnt();
            if (dt.Rows[0]["sms"].ToString()!="")
            {
                lbSMSbalance.Text = PappyjoeMVC.Model.EncryptionDecryption.Decrypt(dt.Rows[0]["sms"].ToString(), "ch3lSeAW0n2o2!C1");
            }
            txtSMScount.Text = "";
        }

        private void lbSMSbalance_DoubleClick(object sender, EventArgs e)
        {
            pnlSmsCountset.Visible = true;
            pnlSmsCountset.Location = new Point(225, 570);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtSMScount.Visible = false;
            label12.Visible = false;
            btn_saveSmsCount.Visible = false;
            label10.Visible = true;
            txtSMScountPwd.Visible = true;
            btn_pwrdOK.Visible = true;
            pnlSmsCountset.Visible = false;
            pnlSmsCountset.Visible = false;
        }
    }
}
