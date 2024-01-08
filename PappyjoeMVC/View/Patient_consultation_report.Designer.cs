
namespace PappyjoeMVC.View
{
    partial class Patient_consultation_report
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Patient_consultation_report));
            this.dgvPatient = new System.Windows.Forms.DataGridView();
            this.sl_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patient_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patient_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.age = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Doctor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f_fee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_Export = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnprint = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmb_doctor = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerdailytreatment2 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePickerdailytreatment1 = new System.Windows.Forms.DateTimePicker();
            this.btnselect = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Lab_Total = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Lab_Msg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatient)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvPatient
            // 
            this.dgvPatient.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.dgvPatient.AllowUserToAddRows = false;
            this.dgvPatient.AllowUserToDeleteRows = false;
            this.dgvPatient.AllowUserToResizeColumns = false;
            this.dgvPatient.AllowUserToResizeRows = false;
            this.dgvPatient.BackgroundColor = System.Drawing.Color.White;
            this.dgvPatient.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPatient.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPatient.ColumnHeadersHeight = 28;
            this.dgvPatient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPatient.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sl_no,
            this.patient_id,
            this.patient_name,
            this.Sex,
            this.age,
            this.Doctor,
            this.date,
            this.fee,
            this.f_fee,
            this.total});
            this.dgvPatient.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvPatient.GridColor = System.Drawing.Color.Gainsboro;
            this.dgvPatient.Location = new System.Drawing.Point(0, 143);
            this.dgvPatient.MultiSelect = false;
            this.dgvPatient.Name = "dgvPatient";
            this.dgvPatient.ReadOnly = true;
            this.dgvPatient.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvPatient.RowHeadersVisible = false;
            this.dgvPatient.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomLeft;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.dgvPatient.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPatient.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPatient.Size = new System.Drawing.Size(1262, 563);
            this.dgvPatient.TabIndex = 252;
            // 
            // sl_no
            // 
            this.sl_no.HeaderText = "SlNo";
            this.sl_no.Name = "sl_no";
            this.sl_no.ReadOnly = true;
            this.sl_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.sl_no.Width = 50;
            // 
            // patient_id
            // 
            this.patient_id.HeaderText = "PATIENT ID";
            this.patient_id.Name = "patient_id";
            this.patient_id.ReadOnly = true;
            this.patient_id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.patient_id.Width = 75;
            // 
            // patient_name
            // 
            this.patient_name.HeaderText = "PATIENT NAME";
            this.patient_name.Name = "patient_name";
            this.patient_name.ReadOnly = true;
            this.patient_name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.patient_name.Width = 260;
            // 
            // Sex
            // 
            this.Sex.HeaderText = "GENDER";
            this.Sex.Name = "Sex";
            this.Sex.ReadOnly = true;
            this.Sex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Sex.Width = 60;
            // 
            // age
            // 
            this.age.HeaderText = "AGE";
            this.age.Name = "age";
            this.age.ReadOnly = true;
            this.age.Width = 60;
            // 
            // Doctor
            // 
            this.Doctor.HeaderText = "DOCTOR";
            this.Doctor.Name = "Doctor";
            this.Doctor.ReadOnly = true;
            this.Doctor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Doctor.Width = 250;
            // 
            // date
            // 
            this.date.HeaderText = "LAST VISITD DATE";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            this.date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.date.Width = 125;
            // 
            // fee
            // 
            this.fee.HeaderText = "Fee";
            this.fee.Name = "fee";
            this.fee.ReadOnly = true;
            this.fee.Width = 90;
            // 
            // f_fee
            // 
            this.f_fee.HeaderText = "FOLLOWUP FEE";
            this.f_fee.Name = "f_fee";
            this.f_fee.ReadOnly = true;
            this.f_fee.Width = 150;
            // 
            // total
            // 
            this.total.HeaderText = "TOTAL AMOUNT PAID";
            this.total.Name = "total";
            this.total.ReadOnly = true;
            this.total.Width = 130;
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.Tomato;
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Close.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_Close.Location = new System.Drawing.Point(1074, 30);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(85, 30);
            this.btn_Close.TabIndex = 112;
            this.btn_Close.Text = "CLOSE";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_Export
            // 
            this.btn_Export.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_Export.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Export.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Export.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Export.ForeColor = System.Drawing.Color.White;
            this.btn_Export.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btn_Export.Location = new System.Drawing.Point(857, 30);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(132, 30);
            this.btn_Export.TabIndex = 11;
            this.btn_Export.Text = "EXPORT TO EXCEL";
            this.btn_Export.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Export.UseVisualStyleBackColor = false;
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(10, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(379, 40);
            this.label6.TabIndex = 0;
            this.label6.Text = "Patients Consultation Report";
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Image = ((System.Drawing.Image)(resources.GetObject("btnprint.Image")));
            this.btnprint.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnprint.Location = new System.Drawing.Point(989, 30);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(85, 30);
            this.btnprint.TabIndex = 10;
            this.btnprint.Text = "PRINT";
            this.btnprint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cmb_doctor);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dateTimePickerdailytreatment2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dateTimePickerdailytreatment1);
            this.panel1.Controls.Add(this.btnselect);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.Lab_Total);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.panel1.Location = new System.Drawing.Point(0, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1262, 77);
            this.panel1.TabIndex = 250;
            // 
            // cmb_doctor
            // 
            this.cmb_doctor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_doctor.FormattingEnabled = true;
            this.cmb_doctor.Location = new System.Drawing.Point(580, 29);
            this.cmb_doctor.Name = "cmb_doctor";
            this.cmb_doctor.Size = new System.Drawing.Size(219, 25);
            this.cmb_doctor.TabIndex = 114;
            this.cmb_doctor.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.cmb_doctor.Click += new System.EventHandler(this.cmb_doctor_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(529, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Doctor:";
            // 
            // dateTimePickerdailytreatment2
            // 
            this.dateTimePickerdailytreatment2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerdailytreatment2.Location = new System.Drawing.Point(309, 28);
            this.dateTimePickerdailytreatment2.Name = "dateTimePickerdailytreatment2";
            this.dateTimePickerdailytreatment2.Size = new System.Drawing.Size(207, 22);
            this.dateTimePickerdailytreatment2.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(14, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "From:";
            // 
            // dateTimePickerdailytreatment1
            // 
            this.dateTimePickerdailytreatment1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerdailytreatment1.Location = new System.Drawing.Point(60, 28);
            this.dateTimePickerdailytreatment1.Name = "dateTimePickerdailytreatment1";
            this.dateTimePickerdailytreatment1.Size = new System.Drawing.Size(207, 22);
            this.dateTimePickerdailytreatment1.TabIndex = 5;
            // 
            // btnselect
            // 
            this.btnselect.BackColor = System.Drawing.Color.LimeGreen;
            this.btnselect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnselect.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnselect.ForeColor = System.Drawing.Color.White;
            this.btnselect.Location = new System.Drawing.Point(816, 28);
            this.btnselect.Name = "btnselect";
            this.btnselect.Size = new System.Drawing.Size(75, 23);
            this.btnselect.TabIndex = 7;
            this.btnselect.Text = "Show";
            this.btnselect.UseVisualStyleBackColor = false;
            this.btnselect.Click += new System.EventHandler(this.btnselect_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(279, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "To:";
            // 
            // Lab_Total
            // 
            this.Lab_Total.AutoSize = true;
            this.Lab_Total.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_Total.ForeColor = System.Drawing.Color.Red;
            this.Lab_Total.Location = new System.Drawing.Point(1007, 32);
            this.Lab_Total.Name = "Lab_Total";
            this.Lab_Total.Size = new System.Drawing.Size(45, 17);
            this.Lab_Total.TabIndex = 2;
            this.Lab_Total.Text = "label2";
            this.Lab_Total.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(924, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "TOTAL COUNT:";
            this.label5.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.btn_Close);
            this.panel2.Controls.Add(this.btnprint);
            this.panel2.Controls.Add(this.btn_Export);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1262, 66);
            this.panel2.TabIndex = 251;
            // 
            // Lab_Msg
            // 
            this.Lab_Msg.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Lab_Msg.BackColor = System.Drawing.Color.Wheat;
            this.Lab_Msg.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_Msg.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.Lab_Msg.Location = new System.Drawing.Point(263, 426);
            this.Lab_Msg.Name = "Lab_Msg";
            this.Lab_Msg.Size = new System.Drawing.Size(563, 25);
            this.Lab_Msg.TabIndex = 278;
            this.Lab_Msg.Text = "No Records Found. Please change the date and then try again!..";
            this.Lab_Msg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Lab_Msg.Visible = false;
            // 
            // Patient_consultation_report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 661);
            this.Controls.Add(this.Lab_Msg);
            this.Controls.Add(this.dgvPatient);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Patient_consultation_report";
            this.Text = "Patient_consultation_report";
            this.Load += new System.EventHandler(this.Patient_consultation_report_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatient)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvPatient;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Export;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dateTimePickerdailytreatment2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePickerdailytreatment1;
        private System.Windows.Forms.Button btnselect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label Lab_Total;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_doctor;
        private System.Windows.Forms.Label Lab_Msg;
        private System.Windows.Forms.DataGridViewTextBoxColumn sl_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn patient_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn patient_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sex;
        private System.Windows.Forms.DataGridViewTextBoxColumn age;
        private System.Windows.Forms.DataGridViewTextBoxColumn Doctor;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn fee;
        private System.Windows.Forms.DataGridViewTextBoxColumn f_fee;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
    }
}