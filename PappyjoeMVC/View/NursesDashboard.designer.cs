
namespace PappyjoeMVC.View
{
    partial class NursesDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtp_nurse = new System.Windows.Forms.DateTimePicker();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dgvnurse = new System.Windows.Forms.DataGridView();
            this.cid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cptid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cdname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctreatment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label9 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgvlab = new System.Windows.Forms.DataGridView();
            this.c_pid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cpid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_dname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_prcd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label8 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.nursecompltd = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.nursepend = new System.Windows.Forms.Label();
            this.ttlnursenote = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.totallabnote = new System.Windows.Forms.Label();
            this.labpending = new System.Windows.Forms.Label();
            this.labcompld = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvnurse)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvlab)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dtp_nurse);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(0, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1170, 529);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // dtp_nurse
            // 
            this.dtp_nurse.Location = new System.Drawing.Point(550, 32);
            this.dtp_nurse.Name = "dtp_nurse";
            this.dtp_nurse.Size = new System.Drawing.Size(188, 20);
            this.dtp_nurse.TabIndex = 5;
            this.dtp_nurse.Visible = false;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Controls.Add(this.dgvnurse);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Location = new System.Drawing.Point(599, 217);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(517, 258);
            this.panel5.TabIndex = 4;
            // 
            // dgvnurse
            // 
            this.dgvnurse.BackgroundColor = System.Drawing.Color.White;
            this.dgvnurse.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvnurse.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvnurse.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvnurse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvnurse.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cid,
            this.cptid,
            this.cname,
            this.cdname,
            this.ctreatment,
            this.cstatus});
            this.dgvnurse.Location = new System.Drawing.Point(3, 51);
            this.dgvnurse.Name = "dgvnurse";
            this.dgvnurse.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvnurse.RowHeadersVisible = false;
            this.dgvnurse.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.dgvnurse.Size = new System.Drawing.Size(511, 150);
            this.dgvnurse.TabIndex = 14;
            // 
            // cid
            // 
            this.cid.HeaderText = "Patient id";
            this.cid.Name = "cid";
            // 
            // cptid
            // 
            this.cptid.HeaderText = "ptid";
            this.cptid.Name = "cptid";
            this.cptid.Visible = false;
            // 
            // cname
            // 
            this.cname.HeaderText = "Name";
            this.cname.Name = "cname";
            // 
            // cdname
            // 
            this.cdname.HeaderText = "Doctor";
            this.cdname.Name = "cdname";
            this.cdname.Width = 150;
            // 
            // ctreatment
            // 
            this.ctreatment.HeaderText = "Treatment";
            this.ctreatment.Name = "ctreatment";
            this.ctreatment.Visible = false;
            // 
            // cstatus
            // 
            this.cstatus.HeaderText = "Status";
            this.cstatus.Name = "cstatus";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(34, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Nurses Notification";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.dgvlab);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Location = new System.Drawing.Point(36, 217);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(517, 258);
            this.panel4.TabIndex = 3;
            // 
            // dgvlab
            // 
            this.dgvlab.BackgroundColor = System.Drawing.Color.White;
            this.dgvlab.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvlab.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvlab.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvlab.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvlab.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.c_pid,
            this.cpid,
            this.c_name,
            this.c_dname,
            this.c_prcd,
            this.c_status});
            this.dgvlab.Location = new System.Drawing.Point(3, 46);
            this.dgvlab.Name = "dgvlab";
            this.dgvlab.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvlab.RowHeadersVisible = false;
            this.dgvlab.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvlab.Size = new System.Drawing.Size(511, 150);
            this.dgvlab.TabIndex = 6;
            // 
            // c_pid
            // 
            this.c_pid.HeaderText = "Patient id";
            this.c_pid.Name = "c_pid";
            // 
            // cpid
            // 
            this.cpid.HeaderText = "pt_id";
            this.cpid.Name = "cpid";
            this.cpid.Visible = false;
            // 
            // c_name
            // 
            this.c_name.HeaderText = "Name";
            this.c_name.Name = "c_name";
            // 
            // c_dname
            // 
            this.c_dname.HeaderText = "Doctor";
            this.c_dname.Name = "c_dname";
            this.c_dname.Width = 150;
            // 
            // c_prcd
            // 
            this.c_prcd.HeaderText = "Treatment";
            this.c_prcd.Name = "c_prcd";
            this.c_prcd.Visible = false;
            // 
            // c_status
            // 
            this.c_status.HeaderText = "Status";
            this.c_status.Name = "c_status";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(32, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Lab Notification";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Location = new System.Drawing.Point(3, 95);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1121, 100);
            this.panel2.TabIndex = 1;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.Controls.Add(this.label7);
            this.panel7.Controls.Add(this.nursecompltd);
            this.panel7.Controls.Add(this.label12);
            this.panel7.Controls.Add(this.label10);
            this.panel7.Controls.Add(this.label11);
            this.panel7.Controls.Add(this.nursepend);
            this.panel7.Controls.Add(this.ttlnursenote);
            this.panel7.Location = new System.Drawing.Point(596, 9);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(518, 78);
            this.panel7.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(14, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Nurses Notification";
            // 
            // nursecompltd
            // 
            this.nursecompltd.AutoSize = true;
            this.nursecompltd.ForeColor = System.Drawing.Color.DodgerBlue;
            this.nursecompltd.Location = new System.Drawing.Point(378, 55);
            this.nursecompltd.Name = "nursecompltd";
            this.nursecompltd.Size = new System.Drawing.Size(13, 13);
            this.nursecompltd.TabIndex = 11;
            this.nursecompltd.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(355, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "Completed";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(27, 31);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Total Notification";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(195, 31);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "Pending";
            // 
            // nursepend
            // 
            this.nursepend.AutoSize = true;
            this.nursepend.ForeColor = System.Drawing.Color.DodgerBlue;
            this.nursepend.Location = new System.Drawing.Point(211, 56);
            this.nursepend.Name = "nursepend";
            this.nursepend.Size = new System.Drawing.Size(13, 13);
            this.nursepend.TabIndex = 10;
            this.nursepend.Text = "0";
            this.nursepend.Click += new System.EventHandler(this.nursepend_Click);
            // 
            // ttlnursenote
            // 
            this.ttlnursenote.AutoSize = true;
            this.ttlnursenote.ForeColor = System.Drawing.Color.DodgerBlue;
            this.ttlnursenote.Location = new System.Drawing.Point(57, 55);
            this.ttlnursenote.Name = "ttlnursenote";
            this.ttlnursenote.Size = new System.Drawing.Size(13, 13);
            this.ttlnursenote.TabIndex = 9;
            this.ttlnursenote.Text = "0";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.Controls.Add(this.label6);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Controls.Add(this.totallabnote);
            this.panel6.Controls.Add(this.labpending);
            this.panel6.Controls.Add(this.labcompld);
            this.panel6.Location = new System.Drawing.Point(33, 9);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(517, 78);
            this.panel6.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "Lab Notification";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Total Notification";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(227, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Pending";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(372, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Completed";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // totallabnote
            // 
            this.totallabnote.AutoSize = true;
            this.totallabnote.ForeColor = System.Drawing.Color.DodgerBlue;
            this.totallabnote.Location = new System.Drawing.Point(53, 53);
            this.totallabnote.Name = "totallabnote";
            this.totallabnote.Size = new System.Drawing.Size(13, 13);
            this.totallabnote.TabIndex = 6;
            this.totallabnote.Text = "0";
            // 
            // labpending
            // 
            this.labpending.AutoSize = true;
            this.labpending.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labpending.Location = new System.Drawing.Point(244, 53);
            this.labpending.Name = "labpending";
            this.labpending.Size = new System.Drawing.Size(13, 13);
            this.labpending.TabIndex = 7;
            this.labpending.Text = "0";
            // 
            // labcompld
            // 
            this.labcompld.AutoSize = true;
            this.labcompld.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labcompld.Location = new System.Drawing.Point(394, 53);
            this.labcompld.Name = "labcompld";
            this.labcompld.Size = new System.Drawing.Size(13, 13);
            this.labcompld.TabIndex = 8;
            this.labcompld.Text = "0";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(36, 47);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(159, 42);
            this.panel3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(5, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Maxwell Hospital,Mumbai";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Dashboard\r\n";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1039, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 24);
            this.button1.TabIndex = 0;
            this.button1.Text = "Add NursesNote";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // NursesDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 531);
            this.Controls.Add(this.panel1);
            this.Name = "NursesDashboard";
            this.Text = "NursesDashboard";
            this.Load += new System.EventHandler(this.NursesDashboard_Load);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvnurse)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvlab)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label nursecompltd;
        private System.Windows.Forms.Label nursepend;
        private System.Windows.Forms.Label ttlnursenote;
        private System.Windows.Forms.Label labcompld;
        private System.Windows.Forms.Label labpending;
        private System.Windows.Forms.Label totallabnote;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgvlab;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.DateTimePicker dtp_nurse;
        private System.Windows.Forms.DataGridView dgvnurse;
        private System.Windows.Forms.DataGridViewTextBoxColumn cid;
        private System.Windows.Forms.DataGridViewTextBoxColumn cptid;
        private System.Windows.Forms.DataGridViewTextBoxColumn cname;
        private System.Windows.Forms.DataGridViewTextBoxColumn cdname;
        private System.Windows.Forms.DataGridViewTextBoxColumn ctreatment;
        private System.Windows.Forms.DataGridViewTextBoxColumn cstatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_pid;
        private System.Windows.Forms.DataGridViewTextBoxColumn cpid;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_dname;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_prcd;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_status;
    }
}