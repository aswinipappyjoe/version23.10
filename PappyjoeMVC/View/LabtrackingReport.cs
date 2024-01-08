using PappyjoeMVC.Controller;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PappyjoeMVC.View
{
    public partial class LabtrackingReport : Form
    {
        public static LabtrackingReport form;
        public LabtrackingReport()
        {
            InitializeComponent();
            form = this;
        }
        public string doctor_id = "", patient_id = "", chstatus = "";
        LabtrackingReport_controller ctrlr = new LabtrackingReport_controller();

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(textBox2.Text, "^[0-9]+$"))
                {
                    if (e.KeyChar == Convert.ToChar(Keys.Enter))
                    {
                        if (String.IsNullOrWhiteSpace(textBox2.Text))
                        {
                            DataTable keyprs = this.ctrlr.txtkeypress();
                            dataGridView1.DataSource = keyprs;
                        }
                        else
                        {
                            DataTable keyprs2 = this.ctrlr.txtkeypress2(textBox2.Text);
                            dataGridView1.DataSource = keyprs2;

                        }
                    }
                }
                else
                {
                    if (textBox2.Text != "")
                    {
                        textBox2.Text = textBox2.Text.Remove(textBox2.Text.Length - 1);
                        MessageBox.Show("Enter only Alphabets", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(textBox2.Text, "^[0-9]+$"))
                {
                    if (String.IsNullOrWhiteSpace(textBox2.Text))
                    {
                        DataTable keyup = this.ctrlr.txtkeyup();
                        dataGridView1.DataSource = keyup;
                    }
                    else
                    {
                        DataTable keyup2 = this.ctrlr.txtkeyup2(textBox2.Text);
                        dataGridView1.DataSource = keyup2;
                    }
                }
                else
                {
                    if (textBox2.Text != "")
                    {
                        textBox2.Text = textBox2.Text.Remove(textBox2.Text.Length - 1);
                        MessageBox.Show("Enter only number", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            DataTable stact = this.ctrlr.stactive();
            dataGridView1.DataSource = stact;
        }
        private void btnsent_Click(object sender, EventArgs e)
        {
            DataTable stsnt = this.ctrlr.statsent();
            dataGridView1.DataSource = stsnt;
        }
        private void btn_Inproduction_Click(object sender, EventArgs e)
        {
            DataTable dt_pt = this.ctrlr.statinproductn();
            dataGridView1.DataSource = dt_pt;
        }
        private void btn_Intransit_Click(object sender, EventArgs e)
        {
            DataTable dt_pt = this.ctrlr.statintransit();
            dataGridView1.DataSource = dt_pt;
        }
        private void btn_Recieved_Click(object sender, EventArgs e)
        {
            DataTable dt_pt = this.ctrlr.statreceived();
            dataGridView1.DataSource = dt_pt;
        }
        private void btn_Overdue_Click(object sender, EventArgs e)
        {
            DataTable dt_pt = this.ctrlr.statoverdue();
            dataGridView1.DataSource = dt_pt;
        }
        private void btn_today_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now;
            DataTable dt_pt = this.ctrlr.duedtetoday(today.ToString("yyyy-MM-dd"));
            dataGridView1.DataSource = dt_pt;
        }
        private void btn_tomorrow_Click(object sender, EventArgs e)
        {
            DateTime tomorrow = DateTime.Now.AddDays(1);
            DataTable dt_pt = this.ctrlr.duedtetommarrow(tomorrow.ToString("yyyy-MM-dd"));
            dataGridView1.DataSource = dt_pt;
        }
        private void btn_Neworder_Click(object sender, EventArgs e)
        {
            var form2 = new Trackingnullstatus();
            form2.ShowDialog();
            form2.Dispose();
        }

        private void LabtrackingReport_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable clinicname = this.ctrlr.practicedetails();
                if (clinicname.Rows.Count > 0)
                {
                    string clinicn = "";
                    clinicn = clinicname.Rows[0][0].ToString();
                }
                DataTable dt_pt = this.ctrlr.notnullstatus();
                dataGridView1.DataSource = null;
                if (dt_pt.Rows.Count>0)
                {
                    dataGridView1.DataSource = dt_pt;
                }
               
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 9, FontStyle.Regular);
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridView1.EnableHeadersVisualStyles = false;
                foreach (DataGridViewColumn cl in dataGridView1.Columns)
                {
                    cl.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                DataTable lbnme = this.ctrlr.getLabnames();
                cb_labnames.DisplayMember = "labname";
                cb_labnames.ValueMember = "labname";
                cb_labnames.DataSource = lbnme;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnShwlab_Click(object sender, EventArgs e)
        {
            DataTable dt_pt = this.ctrlr.lbnmerel(cb_labnames.Text);
            //dataGridView1.DataSource = null;
            if (dt_pt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt_pt;
            }

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 9, FontStyle.Regular);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.EnableHeadersVisualStyles = false;
            foreach (DataGridViewColumn cl in dataGridView1.Columns)
            {
                cl.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(e.RowIndex>=0)//bhj
                {
                var senderGrid = (DataGridView)sender;
                if (e.ColumnIndex >=0)
                {
                    int k = e.RowIndex;
                    string jobno = dataGridView1.Rows[k].Cells[1].Value.ToString();
                    string patient = dataGridView1.Rows[k].Cells[2].Value.ToString();
                    string phoneno= dataGridView1.Rows[k].Cells[3].Value.ToString();
                    string doctor = dataGridView1.Rows[k].Cells[4].Value.ToString();
                    string lab = dataGridView1.Rows[k].Cells[5].Value.ToString();
                    string workname = dataGridView1.Rows[k].Cells[6].Value.ToString();
                    string due = dataGridView1.Rows[k].Cells[7].Value.ToString();
                    string status = dataGridView1.Rows[k].Cells[8].Value.ToString();
                    ChangeStatus statuschange = new PappyjoeMVC.View.ChangeStatus(jobno, patient, doctor, lab, workname, due, status);
                    statuschange.ShowDialog();
                    DataTable dt = this.ctrlr.selectall(jobno);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        chstatus = dt.Rows[i][13].ToString();
                    }
                    dataGridView1.Rows[k].Cells[8].Value = chstatus;
                }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error !..", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
