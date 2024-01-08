namespace PappyjoeMVC.View
{
    partial class Rooms_details
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Rooms_details));
            this.Rooms_flowpanel = new System.Windows.Forms.FlowLayoutPanel();
            this.pnl_Top = new System.Windows.Forms.Panel();
            this.lb_date = new System.Windows.Forms.Label();
            this.pnl_left = new System.Windows.Forms.Panel();
            this.btn_settings = new System.Windows.Forms.Button();
            this.btn_Overview = new System.Windows.Forms.Button();
            this.pnl_main = new System.Windows.Forms.Panel();
            this.pnl_PatientBook = new System.Windows.Forms.Panel();
            this.btn_CancelBookingpnl = new System.Windows.Forms.Button();
            this.btn_SaveBooking = new System.Windows.Forms.Button();
            this.lst_patients = new System.Windows.Forms.ListBox();
            this.txt_PBsearchPat = new System.Windows.Forms.TextBox();
            this.lb_PBroomno = new System.Windows.Forms.Label();
            this.lb_PBadmn = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lb_PBmobNo = new System.Windows.Forms.Label();
            this.lb_PBname = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lb_PBpatId = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnl_settings = new System.Windows.Forms.Panel();
            this.dgv_rooms = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.room = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.btn_AddRoom = new System.Windows.Forms.Button();
            this.txt_roomno = new System.Windows.Forms.TextBox();
            this.lb_roomNo = new System.Windows.Forms.Label();
            this.pnl_Top.SuspendLayout();
            this.pnl_left.SuspendLayout();
            this.pnl_main.SuspendLayout();
            this.pnl_PatientBook.SuspendLayout();
            this.pnl_settings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rooms)).BeginInit();
            this.SuspendLayout();
            // 
            // Rooms_flowpanel
            // 
            this.Rooms_flowpanel.AutoScroll = true;
            this.Rooms_flowpanel.Location = new System.Drawing.Point(83, 96);
            this.Rooms_flowpanel.Name = "Rooms_flowpanel";
            this.Rooms_flowpanel.Size = new System.Drawing.Size(677, 371);
            this.Rooms_flowpanel.TabIndex = 0;
            // 
            // pnl_Top
            // 
            this.pnl_Top.BackColor = System.Drawing.Color.RoyalBlue;
            this.pnl_Top.Controls.Add(this.lb_date);
            this.pnl_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Top.Location = new System.Drawing.Point(0, 0);
            this.pnl_Top.Name = "pnl_Top";
            this.pnl_Top.Size = new System.Drawing.Size(1027, 36);
            this.pnl_Top.TabIndex = 1;
            // 
            // lb_date
            // 
            this.lb_date.AutoSize = true;
            this.lb_date.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_date.ForeColor = System.Drawing.Color.White;
            this.lb_date.Location = new System.Drawing.Point(25, 13);
            this.lb_date.Name = "lb_date";
            this.lb_date.Size = new System.Drawing.Size(41, 17);
            this.lb_date.TabIndex = 0;
            this.lb_date.Text = "label1";
            // 
            // pnl_left
            // 
            this.pnl_left.BackColor = System.Drawing.Color.RoyalBlue;
            this.pnl_left.Controls.Add(this.btn_settings);
            this.pnl_left.Controls.Add(this.btn_Overview);
            this.pnl_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_left.Location = new System.Drawing.Point(0, 36);
            this.pnl_left.Name = "pnl_left";
            this.pnl_left.Size = new System.Drawing.Size(180, 548);
            this.pnl_left.TabIndex = 2;
            // 
            // btn_settings
            // 
            this.btn_settings.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_settings.FlatAppearance.BorderSize = 0;
            this.btn_settings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_settings.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_settings.ForeColor = System.Drawing.Color.White;
            this.btn_settings.Location = new System.Drawing.Point(0, 39);
            this.btn_settings.Name = "btn_settings";
            this.btn_settings.Size = new System.Drawing.Size(180, 39);
            this.btn_settings.TabIndex = 0;
            this.btn_settings.Text = "Settings";
            this.btn_settings.UseVisualStyleBackColor = true;
            this.btn_settings.Click += new System.EventHandler(this.btn_settings_Click);
            // 
            // btn_Overview
            // 
            this.btn_Overview.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Overview.FlatAppearance.BorderSize = 0;
            this.btn_Overview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Overview.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Overview.ForeColor = System.Drawing.Color.White;
            this.btn_Overview.Location = new System.Drawing.Point(0, 0);
            this.btn_Overview.Name = "btn_Overview";
            this.btn_Overview.Size = new System.Drawing.Size(180, 39);
            this.btn_Overview.TabIndex = 0;
            this.btn_Overview.Text = "Overview";
            this.btn_Overview.UseVisualStyleBackColor = true;
            this.btn_Overview.Click += new System.EventHandler(this.btn_Overview_Click);
            // 
            // pnl_main
            // 
            this.pnl_main.Controls.Add(this.pnl_PatientBook);
            this.pnl_main.Controls.Add(this.pnl_settings);
            this.pnl_main.Controls.Add(this.Rooms_flowpanel);
            this.pnl_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_main.Location = new System.Drawing.Point(180, 36);
            this.pnl_main.Name = "pnl_main";
            this.pnl_main.Size = new System.Drawing.Size(847, 548);
            this.pnl_main.TabIndex = 3;
            // 
            // pnl_PatientBook
            // 
            this.pnl_PatientBook.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnl_PatientBook.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_PatientBook.Controls.Add(this.btn_CancelBookingpnl);
            this.pnl_PatientBook.Controls.Add(this.btn_SaveBooking);
            this.pnl_PatientBook.Controls.Add(this.lst_patients);
            this.pnl_PatientBook.Controls.Add(this.txt_PBsearchPat);
            this.pnl_PatientBook.Controls.Add(this.lb_PBroomno);
            this.pnl_PatientBook.Controls.Add(this.lb_PBadmn);
            this.pnl_PatientBook.Controls.Add(this.label5);
            this.pnl_PatientBook.Controls.Add(this.lb_PBmobNo);
            this.pnl_PatientBook.Controls.Add(this.lb_PBname);
            this.pnl_PatientBook.Controls.Add(this.label4);
            this.pnl_PatientBook.Controls.Add(this.lb_PBpatId);
            this.pnl_PatientBook.Controls.Add(this.label6);
            this.pnl_PatientBook.Controls.Add(this.label3);
            this.pnl_PatientBook.Controls.Add(this.label2);
            this.pnl_PatientBook.Controls.Add(this.label1);
            this.pnl_PatientBook.Location = new System.Drawing.Point(520, 26);
            this.pnl_PatientBook.Name = "pnl_PatientBook";
            this.pnl_PatientBook.Size = new System.Drawing.Size(290, 248);
            this.pnl_PatientBook.TabIndex = 2;
            // 
            // btn_CancelBookingpnl
            // 
            this.btn_CancelBookingpnl.BackColor = System.Drawing.Color.Tomato;
            this.btn_CancelBookingpnl.FlatAppearance.BorderSize = 0;
            this.btn_CancelBookingpnl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_CancelBookingpnl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CancelBookingpnl.ForeColor = System.Drawing.Color.White;
            this.btn_CancelBookingpnl.Location = new System.Drawing.Point(213, 205);
            this.btn_CancelBookingpnl.Name = "btn_CancelBookingpnl";
            this.btn_CancelBookingpnl.Size = new System.Drawing.Size(54, 23);
            this.btn_CancelBookingpnl.TabIndex = 3;
            this.btn_CancelBookingpnl.Text = "Cancel";
            this.btn_CancelBookingpnl.UseVisualStyleBackColor = false;
            this.btn_CancelBookingpnl.Click += new System.EventHandler(this.btn_CancelBookingpnl_Click);
            // 
            // btn_SaveBooking
            // 
            this.btn_SaveBooking.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_SaveBooking.FlatAppearance.BorderSize = 0;
            this.btn_SaveBooking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SaveBooking.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SaveBooking.ForeColor = System.Drawing.Color.White;
            this.btn_SaveBooking.Location = new System.Drawing.Point(153, 205);
            this.btn_SaveBooking.Name = "btn_SaveBooking";
            this.btn_SaveBooking.Size = new System.Drawing.Size(54, 23);
            this.btn_SaveBooking.TabIndex = 3;
            this.btn_SaveBooking.Text = "Save";
            this.btn_SaveBooking.UseVisualStyleBackColor = false;
            this.btn_SaveBooking.Click += new System.EventHandler(this.btn_SaveBooking_Click);
            // 
            // lst_patients
            // 
            this.lst_patients.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_patients.FormattingEnabled = true;
            this.lst_patients.ItemHeight = 17;
            this.lst_patients.Location = new System.Drawing.Point(85, 40);
            this.lst_patients.Name = "lst_patients";
            this.lst_patients.Size = new System.Drawing.Size(182, 89);
            this.lst_patients.TabIndex = 2;
            this.lst_patients.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lst_patients_MouseClick);
            this.lst_patients.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lst_patients_KeyUp);
            // 
            // txt_PBsearchPat
            // 
            this.txt_PBsearchPat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_PBsearchPat.Location = new System.Drawing.Point(82, 37);
            this.txt_PBsearchPat.Name = "txt_PBsearchPat";
            this.txt_PBsearchPat.Size = new System.Drawing.Size(182, 22);
            this.txt_PBsearchPat.TabIndex = 1;
            this.txt_PBsearchPat.Click += new System.EventHandler(this.txt_PBsearchPat_Click);
            this.txt_PBsearchPat.TextChanged += new System.EventHandler(this.txt_PBsearchPat_TextChanged);
            this.txt_PBsearchPat.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_PBsearchPat_KeyUp);
            // 
            // lb_PBroomno
            // 
            this.lb_PBroomno.AutoSize = true;
            this.lb_PBroomno.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_PBroomno.Location = new System.Drawing.Point(138, 193);
            this.lb_PBroomno.Name = "lb_PBroomno";
            this.lb_PBroomno.Size = new System.Drawing.Size(0, 17);
            this.lb_PBroomno.TabIndex = 0;
            // 
            // lb_PBadmn
            // 
            this.lb_PBadmn.AutoSize = true;
            this.lb_PBadmn.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_PBadmn.Location = new System.Drawing.Point(138, 161);
            this.lb_PBadmn.Name = "lb_PBadmn";
            this.lb_PBadmn.Size = new System.Drawing.Size(0, 17);
            this.lb_PBadmn.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(31, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Room No:";
            // 
            // lb_PBmobNo
            // 
            this.lb_PBmobNo.AutoSize = true;
            this.lb_PBmobNo.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_PBmobNo.Location = new System.Drawing.Point(138, 132);
            this.lb_PBmobNo.Name = "lb_PBmobNo";
            this.lb_PBmobNo.Size = new System.Drawing.Size(0, 17);
            this.lb_PBmobNo.TabIndex = 0;
            // 
            // lb_PBname
            // 
            this.lb_PBname.AutoSize = true;
            this.lb_PBname.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_PBname.Location = new System.Drawing.Point(138, 103);
            this.lb_PBname.Name = "lb_PBname";
            this.lb_PBname.Size = new System.Drawing.Size(0, 17);
            this.lb_PBname.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(31, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Admission Date:";
            // 
            // lb_PBpatId
            // 
            this.lb_PBpatId.AutoSize = true;
            this.lb_PBpatId.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_PBpatId.Location = new System.Drawing.Point(138, 71);
            this.lb_PBpatId.Name = "lb_PBpatId";
            this.lb_PBpatId.Size = new System.Drawing.Size(0, 17);
            this.lb_PBpatId.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(31, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Mobile No:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(31, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Patient Id:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search";
            // 
            // pnl_settings
            // 
            this.pnl_settings.Controls.Add(this.dgv_rooms);
            this.pnl_settings.Controls.Add(this.btn_AddRoom);
            this.pnl_settings.Controls.Add(this.txt_roomno);
            this.pnl_settings.Controls.Add(this.lb_roomNo);
            this.pnl_settings.Location = new System.Drawing.Point(16, 15);
            this.pnl_settings.Name = "pnl_settings";
            this.pnl_settings.Size = new System.Drawing.Size(338, 270);
            this.pnl_settings.TabIndex = 1;
            // 
            // dgv_rooms
            // 
            this.dgv_rooms.AllowUserToResizeColumns = false;
            this.dgv_rooms.AllowUserToResizeRows = false;
            this.dgv_rooms.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_rooms.BackgroundColor = System.Drawing.Color.White;
            this.dgv_rooms.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgv_rooms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_rooms.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.room,
            this.edit,
            this.delete});
            this.dgv_rooms.Location = new System.Drawing.Point(68, 70);
            this.dgv_rooms.Name = "dgv_rooms";
            this.dgv_rooms.RowHeadersVisible = false;
            this.dgv_rooms.Size = new System.Drawing.Size(230, 166);
            this.dgv_rooms.TabIndex = 3;
            this.dgv_rooms.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_rooms_CellClick);
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // room
            // 
            this.room.FillWeight = 257.2374F;
            this.room.HeaderText = "Rooms";
            this.room.Name = "room";
            // 
            // edit
            // 
            this.edit.FillWeight = 58.94848F;
            this.edit.HeaderText = "";
            this.edit.Image = global::PappyjoeMVC.Properties.Resources.editicon;
            this.edit.Name = "edit";
            this.edit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.edit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // delete
            // 
            this.delete.FillWeight = 70.90915F;
            this.delete.HeaderText = "";
            this.delete.Image = global::PappyjoeMVC.Properties.Resources.deleteicon;
            this.delete.Name = "delete";
            this.delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.delete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // btn_AddRoom
            // 
            this.btn_AddRoom.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_AddRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddRoom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddRoom.ForeColor = System.Drawing.Color.White;
            this.btn_AddRoom.Location = new System.Drawing.Point(243, 43);
            this.btn_AddRoom.Name = "btn_AddRoom";
            this.btn_AddRoom.Size = new System.Drawing.Size(55, 23);
            this.btn_AddRoom.TabIndex = 2;
            this.btn_AddRoom.Text = "Add";
            this.btn_AddRoom.UseVisualStyleBackColor = false;
            this.btn_AddRoom.Click += new System.EventHandler(this.btn_AddRoom_Click);
            // 
            // txt_roomno
            // 
            this.txt_roomno.Location = new System.Drawing.Point(138, 44);
            this.txt_roomno.Name = "txt_roomno";
            this.txt_roomno.Size = new System.Drawing.Size(100, 20);
            this.txt_roomno.TabIndex = 1;
            // 
            // lb_roomNo
            // 
            this.lb_roomNo.AutoSize = true;
            this.lb_roomNo.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_roomNo.Location = new System.Drawing.Point(68, 46);
            this.lb_roomNo.Name = "lb_roomNo";
            this.lb_roomNo.Size = new System.Drawing.Size(69, 17);
            this.lb_roomNo.TabIndex = 0;
            this.lb_roomNo.Text = "Room No:";
            // 
            // Rooms_details
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1027, 584);
            this.Controls.Add(this.pnl_main);
            this.Controls.Add(this.pnl_left);
            this.Controls.Add(this.pnl_Top);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Rooms_details";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rooms Details";
            this.Load += new System.EventHandler(this.Rooms_details_Load);
            this.pnl_Top.ResumeLayout(false);
            this.pnl_Top.PerformLayout();
            this.pnl_left.ResumeLayout(false);
            this.pnl_main.ResumeLayout(false);
            this.pnl_PatientBook.ResumeLayout(false);
            this.pnl_PatientBook.PerformLayout();
            this.pnl_settings.ResumeLayout(false);
            this.pnl_settings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rooms)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel Rooms_flowpanel;
        private System.Windows.Forms.Panel pnl_Top;
        private System.Windows.Forms.Panel pnl_left;
        private System.Windows.Forms.Panel pnl_main;
        private System.Windows.Forms.Label lb_date;
        private System.Windows.Forms.Button btn_settings;
        private System.Windows.Forms.Button btn_Overview;
        private System.Windows.Forms.Panel pnl_settings;
        private System.Windows.Forms.DataGridView dgv_rooms;
        private System.Windows.Forms.Button btn_AddRoom;
        private System.Windows.Forms.TextBox txt_roomno;
        private System.Windows.Forms.Label lb_roomNo;
        private System.Windows.Forms.Panel pnl_PatientBook;
        private System.Windows.Forms.ListBox lst_patients;
        private System.Windows.Forms.TextBox txt_PBsearchPat;
        private System.Windows.Forms.Label lb_PBroomno;
        private System.Windows.Forms.Label lb_PBadmn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lb_PBname;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lb_PBpatId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_CancelBookingpnl;
        private System.Windows.Forms.Button btn_SaveBooking;
        private System.Windows.Forms.Label lb_PBmobNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn room;
        private System.Windows.Forms.DataGridViewImageColumn edit;
        private System.Windows.Forms.DataGridViewImageColumn delete;
    }
}