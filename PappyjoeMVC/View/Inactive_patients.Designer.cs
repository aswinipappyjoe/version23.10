namespace PappyjoeMVC.View
{
    partial class Inactive_patients
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inactive_patients));
            this.label1 = new System.Windows.Forms.Label();
            this.DGVInactive = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox_PatientPhoto = new System.Windows.Forms.PictureBox();
            this.lab_PatientName = new System.Windows.Forms.Label();
            this.txtPatientName = new System.Windows.Forms.TextBox();
            this.labPatientId = new System.Windows.Forms.Label();
            this.txtPatientId = new System.Windows.Forms.TextBox();
            this.lblopti = new System.Windows.Forms.Label();
            this.txtopticket = new System.Windows.Forms.TextBox();
            this.labAdhaarId = new System.Windows.Forms.Label();
            this.txtAdhaarId = new System.Windows.Forms.TextBox();
            this.labAge = new System.Windows.Forms.Label();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.labGender = new System.Windows.Forms.Label();
            this.txtGender = new System.Windows.Forms.TextBox();
            this.labDob = new System.Windows.Forms.Label();
            this.txtDob = new System.Windows.Forms.TextBox();
            this.LabDateOfAdm = new System.Windows.Forms.Label();
            this.txtvisiteddate = new System.Windows.Forms.TextBox();
            this.labPrimaryMobileNumber = new System.Windows.Forms.Label();
            this.txtPrimaryMobNo = new System.Windows.Forms.TextBox();
            this.btn_toActive = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVInactive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_PatientPhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Inactive Patients";
            // 
            // DGVInactive
            // 
            this.DGVInactive.AllowUserToAddRows = false;
            this.DGVInactive.AllowUserToDeleteRows = false;
            this.DGVInactive.AllowUserToResizeColumns = false;
            this.DGVInactive.AllowUserToResizeRows = false;
            this.DGVInactive.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVInactive.BackgroundColor = System.Drawing.Color.White;
            this.DGVInactive.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DGVInactive.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.DGVInactive.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DGVInactive.GridColor = System.Drawing.Color.White;
            this.DGVInactive.Location = new System.Drawing.Point(17, 53);
            this.DGVInactive.Name = "DGVInactive";
            this.DGVInactive.ReadOnly = true;
            this.DGVInactive.RowHeadersVisible = false;
            this.DGVInactive.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DGVInactive.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DGVInactive.Size = new System.Drawing.Size(359, 385);
            this.DGVInactive.TabIndex = 1;
            this.DGVInactive.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVInactive_CellClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(407, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Patient Details";
            // 
            // pictureBox_PatientPhoto
            // 
            this.pictureBox_PatientPhoto.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_PatientPhoto.Image")));
            this.pictureBox_PatientPhoto.Location = new System.Drawing.Point(412, 53);
            this.pictureBox_PatientPhoto.Name = "pictureBox_PatientPhoto";
            this.pictureBox_PatientPhoto.Size = new System.Drawing.Size(76, 71);
            this.pictureBox_PatientPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_PatientPhoto.TabIndex = 74;
            this.pictureBox_PatientPhoto.TabStop = false;
            // 
            // lab_PatientName
            // 
            this.lab_PatientName.AutoSize = true;
            this.lab_PatientName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_PatientName.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lab_PatientName.Location = new System.Drawing.Point(408, 138);
            this.lab_PatientName.Name = "lab_PatientName";
            this.lab_PatientName.Size = new System.Drawing.Size(107, 21);
            this.lab_PatientName.TabIndex = 225;
            this.lab_PatientName.Text = "Patient Name:";
            // 
            // txtPatientName
            // 
            this.txtPatientName.BackColor = System.Drawing.Color.White;
            this.txtPatientName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPatientName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientName.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.txtPatientName.Location = new System.Drawing.Point(511, 137);
            this.txtPatientName.Multiline = true;
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.ReadOnly = true;
            this.txtPatientName.Size = new System.Drawing.Size(305, 22);
            this.txtPatientName.TabIndex = 227;
            // 
            // labPatientId
            // 
            this.labPatientId.AutoSize = true;
            this.labPatientId.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labPatientId.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.labPatientId.Location = new System.Drawing.Point(435, 166);
            this.labPatientId.Name = "labPatientId";
            this.labPatientId.Size = new System.Drawing.Size(80, 21);
            this.labPatientId.TabIndex = 228;
            this.labPatientId.Text = "Patient ID:";
            // 
            // txtPatientId
            // 
            this.txtPatientId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPatientId.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientId.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.txtPatientId.Location = new System.Drawing.Point(511, 165);
            this.txtPatientId.Name = "txtPatientId";
            this.txtPatientId.Size = new System.Drawing.Size(117, 22);
            this.txtPatientId.TabIndex = 229;
            // 
            // lblopti
            // 
            this.lblopti.AutoSize = true;
            this.lblopti.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblopti.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblopti.Location = new System.Drawing.Point(453, 278);
            this.lblopti.Name = "lblopti";
            this.lblopti.Size = new System.Drawing.Size(62, 21);
            this.lblopti.TabIndex = 262;
            this.lblopti.Text = "File No:";
            // 
            // txtopticket
            // 
            this.txtopticket.BackColor = System.Drawing.Color.White;
            this.txtopticket.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtopticket.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtopticket.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.txtopticket.Location = new System.Drawing.Point(512, 277);
            this.txtopticket.Name = "txtopticket";
            this.txtopticket.ReadOnly = true;
            this.txtopticket.Size = new System.Drawing.Size(123, 22);
            this.txtopticket.TabIndex = 263;
            // 
            // labAdhaarId
            // 
            this.labAdhaarId.AutoSize = true;
            this.labAdhaarId.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labAdhaarId.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.labAdhaarId.Location = new System.Drawing.Point(425, 194);
            this.labAdhaarId.Name = "labAdhaarId";
            this.labAdhaarId.Size = new System.Drawing.Size(90, 21);
            this.labAdhaarId.TabIndex = 264;
            this.labAdhaarId.Text = "Aadhaar ID:";
            // 
            // txtAdhaarId
            // 
            this.txtAdhaarId.BackColor = System.Drawing.Color.White;
            this.txtAdhaarId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAdhaarId.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdhaarId.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.txtAdhaarId.Location = new System.Drawing.Point(511, 193);
            this.txtAdhaarId.Name = "txtAdhaarId";
            this.txtAdhaarId.ReadOnly = true;
            this.txtAdhaarId.Size = new System.Drawing.Size(207, 22);
            this.txtAdhaarId.TabIndex = 265;
            // 
            // labAge
            // 
            this.labAge.AutoSize = true;
            this.labAge.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labAge.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.labAge.Location = new System.Drawing.Point(475, 222);
            this.labAge.Name = "labAge";
            this.labAge.Size = new System.Drawing.Size(40, 21);
            this.labAge.TabIndex = 266;
            this.labAge.Text = "Age:";
            // 
            // txtAge
            // 
            this.txtAge.BackColor = System.Drawing.Color.White;
            this.txtAge.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAge.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAge.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.txtAge.Location = new System.Drawing.Point(511, 221);
            this.txtAge.Name = "txtAge";
            this.txtAge.ReadOnly = true;
            this.txtAge.Size = new System.Drawing.Size(99, 22);
            this.txtAge.TabIndex = 267;
            // 
            // labGender
            // 
            this.labGender.AutoSize = true;
            this.labGender.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labGender.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.labGender.Location = new System.Drawing.Point(449, 250);
            this.labGender.Name = "labGender";
            this.labGender.Size = new System.Drawing.Size(64, 21);
            this.labGender.TabIndex = 268;
            this.labGender.Text = "Gender:";
            // 
            // txtGender
            // 
            this.txtGender.BackColor = System.Drawing.Color.White;
            this.txtGender.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtGender.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGender.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.txtGender.Location = new System.Drawing.Point(510, 249);
            this.txtGender.Name = "txtGender";
            this.txtGender.ReadOnly = true;
            this.txtGender.Size = new System.Drawing.Size(123, 22);
            this.txtGender.TabIndex = 269;
            // 
            // labDob
            // 
            this.labDob.AutoSize = true;
            this.labDob.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDob.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.labDob.Location = new System.Drawing.Point(415, 306);
            this.labDob.Name = "labDob";
            this.labDob.Size = new System.Drawing.Size(100, 21);
            this.labDob.TabIndex = 270;
            this.labDob.Text = "Date of Birth:";
            // 
            // txtDob
            // 
            this.txtDob.BackColor = System.Drawing.Color.White;
            this.txtDob.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDob.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDob.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.txtDob.Location = new System.Drawing.Point(511, 305);
            this.txtDob.Name = "txtDob";
            this.txtDob.ReadOnly = true;
            this.txtDob.Size = new System.Drawing.Size(185, 22);
            this.txtDob.TabIndex = 271;
            // 
            // LabDateOfAdm
            // 
            this.LabDateOfAdm.AutoSize = true;
            this.LabDateOfAdm.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabDateOfAdm.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.LabDateOfAdm.Location = new System.Drawing.Point(408, 334);
            this.LabDateOfAdm.Name = "LabDateOfAdm";
            this.LabDateOfAdm.Size = new System.Drawing.Size(107, 21);
            this.LabDateOfAdm.TabIndex = 272;
            this.LabDateOfAdm.Text = "Date Of  Adm:";
            // 
            // txtvisiteddate
            // 
            this.txtvisiteddate.BackColor = System.Drawing.Color.White;
            this.txtvisiteddate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtvisiteddate.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtvisiteddate.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.txtvisiteddate.Location = new System.Drawing.Point(511, 333);
            this.txtvisiteddate.Name = "txtvisiteddate";
            this.txtvisiteddate.ReadOnly = true;
            this.txtvisiteddate.Size = new System.Drawing.Size(181, 22);
            this.txtvisiteddate.TabIndex = 273;
            // 
            // labPrimaryMobileNumber
            // 
            this.labPrimaryMobileNumber.AutoSize = true;
            this.labPrimaryMobileNumber.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labPrimaryMobileNumber.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.labPrimaryMobileNumber.Location = new System.Drawing.Point(392, 362);
            this.labPrimaryMobileNumber.Name = "labPrimaryMobileNumber";
            this.labPrimaryMobileNumber.Size = new System.Drawing.Size(123, 21);
            this.labPrimaryMobileNumber.TabIndex = 274;
            this.labPrimaryMobileNumber.Text = "Mobile Number:";
            // 
            // txtPrimaryMobNo
            // 
            this.txtPrimaryMobNo.BackColor = System.Drawing.Color.White;
            this.txtPrimaryMobNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPrimaryMobNo.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrimaryMobNo.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.txtPrimaryMobNo.Location = new System.Drawing.Point(511, 361);
            this.txtPrimaryMobNo.Name = "txtPrimaryMobNo";
            this.txtPrimaryMobNo.ReadOnly = true;
            this.txtPrimaryMobNo.Size = new System.Drawing.Size(207, 22);
            this.txtPrimaryMobNo.TabIndex = 275;
            // 
            // btn_toActive
            // 
            this.btn_toActive.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_toActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_toActive.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_toActive.ForeColor = System.Drawing.Color.White;
            this.btn_toActive.Location = new System.Drawing.Point(612, 11);
            this.btn_toActive.Name = "btn_toActive";
            this.btn_toActive.Size = new System.Drawing.Size(115, 40);
            this.btn_toActive.TabIndex = 276;
            this.btn_toActive.Text = "Convert to Active Patient";
            this.btn_toActive.UseVisualStyleBackColor = false;
            this.btn_toActive.Click += new System.EventHandler(this.btn_toActive_Click);
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.Tomato;
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.ForeColor = System.Drawing.Color.White;
            this.btn_close.Location = new System.Drawing.Point(730, 11);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 40);
            this.btn_close.TabIndex = 276;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = false;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // Inactive_patients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(817, 450);
            this.ControlBox = false;
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_toActive);
            this.Controls.Add(this.txtPrimaryMobNo);
            this.Controls.Add(this.labPrimaryMobileNumber);
            this.Controls.Add(this.txtvisiteddate);
            this.Controls.Add(this.LabDateOfAdm);
            this.Controls.Add(this.txtDob);
            this.Controls.Add(this.labDob);
            this.Controls.Add(this.txtGender);
            this.Controls.Add(this.labGender);
            this.Controls.Add(this.txtAge);
            this.Controls.Add(this.labAge);
            this.Controls.Add(this.txtAdhaarId);
            this.Controls.Add(this.labAdhaarId);
            this.Controls.Add(this.txtopticket);
            this.Controls.Add(this.lblopti);
            this.Controls.Add(this.txtPatientId);
            this.Controls.Add(this.labPatientId);
            this.Controls.Add(this.txtPatientName);
            this.Controls.Add(this.lab_PatientName);
            this.Controls.Add(this.pictureBox_PatientPhoto);
            this.Controls.Add(this.DGVInactive);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Inactive_patients";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inactive Patients";
            this.Load += new System.EventHandler(this.Inactive_patients_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVInactive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_PatientPhoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.DataGridView DGVInactive;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox_PatientPhoto;
        private System.Windows.Forms.Label lab_PatientName;
        public System.Windows.Forms.TextBox txtPatientName;
        private System.Windows.Forms.Label labPatientId;
        public System.Windows.Forms.TextBox txtPatientId;
        private System.Windows.Forms.Label lblopti;
        public System.Windows.Forms.TextBox txtopticket;
        private System.Windows.Forms.Label labAdhaarId;
        public System.Windows.Forms.TextBox txtAdhaarId;
        private System.Windows.Forms.Label labAge;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.Label labGender;
        public System.Windows.Forms.TextBox txtGender;
        private System.Windows.Forms.Label labDob;
        public System.Windows.Forms.TextBox txtDob;
        private System.Windows.Forms.Label LabDateOfAdm;
        private System.Windows.Forms.TextBox txtvisiteddate;
        private System.Windows.Forms.Label labPrimaryMobileNumber;
        public System.Windows.Forms.TextBox txtPrimaryMobNo;
        private System.Windows.Forms.Button btn_toActive;
        private System.Windows.Forms.Button btn_close;
    }
}