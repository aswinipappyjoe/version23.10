
namespace PappyjoeMVC.View
{
    partial class Nurse_notification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Nurse_notification));
            this.label41 = new System.Windows.Forms.Label();
            this.panellog = new System.Windows.Forms.Panel();
            this.DGV_notify = new System.Windows.Forms.DataGridView();
            this.btnshow = new System.Windows.Forms.Button();
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.slno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dr_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ptid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Treatment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.treat_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pt_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plan_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.payment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dr_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.add = new System.Windows.Forms.DataGridViewImageColumn();
            this.edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.panellog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_notify)).BeginInit();
            this.SuspendLayout();
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label41.Location = new System.Drawing.Point(9, 6);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(144, 21);
            this.label41.TabIndex = 286;
            this.label41.Text = "Nurse Notification";
            // 
            // panellog
            // 
            this.panellog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panellog.BackColor = System.Drawing.Color.White;
            this.panellog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panellog.Controls.Add(this.DGV_notify);
            this.panellog.Location = new System.Drawing.Point(1, 77);
            this.panellog.Name = "panellog";
            this.panellog.Size = new System.Drawing.Size(1027, 458);
            this.panellog.TabIndex = 284;
            // 
            // DGV_notify
            // 
            this.DGV_notify.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.DGV_notify.AllowUserToAddRows = false;
            this.DGV_notify.AllowUserToDeleteRows = false;
            this.DGV_notify.AllowUserToResizeColumns = false;
            this.DGV_notify.AllowUserToResizeRows = false;
            this.DGV_notify.BackgroundColor = System.Drawing.Color.White;
            this.DGV_notify.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DGV_notify.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.DGV_notify.ColumnHeadersHeight = 25;
            this.DGV_notify.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DGV_notify.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.slno,
            this.date,
            this.dr_name,
            this.ptid,
            this.name,
            this.phone,
            this.Treatment,
            this.treat_id,
            this.Status,
            this.pt_id,
            this.plan_id,
            this.payment,
            this.dr_id,
            this.nid,
            this.add,
            this.edit});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV_notify.DefaultCellStyle = dataGridViewCellStyle1;
            this.DGV_notify.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV_notify.GridColor = System.Drawing.Color.White;
            this.DGV_notify.Location = new System.Drawing.Point(0, 0);
            this.DGV_notify.Name = "DGV_notify";
            this.DGV_notify.ReadOnly = true;
            this.DGV_notify.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV_notify.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DGV_notify.RowHeadersVisible = false;
            this.DGV_notify.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DGV_notify.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DGV_notify.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGV_notify.Size = new System.Drawing.Size(1025, 456);
            this.DGV_notify.TabIndex = 53;
            this.DGV_notify.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_notify_CellClick);
            // 
            // btnshow
            // 
            this.btnshow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnshow.BackColor = System.Drawing.Color.LimeGreen;
            this.btnshow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnshow.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnshow.ForeColor = System.Drawing.Color.White;
            this.btnshow.Location = new System.Drawing.Point(334, 50);
            this.btnshow.Name = "btnshow";
            this.btnshow.Size = new System.Drawing.Size(58, 23);
            this.btnshow.TabIndex = 248;
            this.btnshow.TabStop = false;
            this.btnshow.Text = "Show";
            this.btnshow.UseVisualStyleBackColor = false;
            this.btnshow.Click += new System.EventHandler(this.btnshow_Click);
            // 
            // dateTimePicker3
            // 
            this.dateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker3.Location = new System.Drawing.Point(57, 50);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.Size = new System.Drawing.Size(154, 20);
            this.dateTimePicker3.TabIndex = 247;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(16, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 17);
            this.label2.TabIndex = 58;
            this.label2.Text = "Date";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.Red;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(933, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 30);
            this.btnCancel.TabIndex = 285;
            this.btnCancel.Text = "CLOSE";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(-4, 43);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1041, 1);
            this.panel2.TabIndex = 296;
            // 
            // slno
            // 
            this.slno.HeaderText = "Sl.No";
            this.slno.Name = "slno";
            this.slno.ReadOnly = true;
            this.slno.Width = 50;
            // 
            // date
            // 
            this.date.HeaderText = "Date";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            this.date.Width = 80;
            // 
            // dr_name
            // 
            this.dr_name.HeaderText = "Doctor";
            this.dr_name.Name = "dr_name";
            this.dr_name.ReadOnly = true;
            this.dr_name.Width = 125;
            // 
            // ptid
            // 
            this.ptid.HeaderText = "Patient Id";
            this.ptid.Name = "ptid";
            this.ptid.ReadOnly = true;
            this.ptid.Width = 80;
            // 
            // name
            // 
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 225;
            // 
            // phone
            // 
            this.phone.HeaderText = "Phone";
            this.phone.Name = "phone";
            this.phone.ReadOnly = true;
            this.phone.Width = 90;
            // 
            // Treatment
            // 
            this.Treatment.HeaderText = "Treatment";
            this.Treatment.Name = "Treatment";
            this.Treatment.ReadOnly = true;
            // 
            // treat_id
            // 
            this.treat_id.HeaderText = "treat_id";
            this.treat_id.Name = "treat_id";
            this.treat_id.ReadOnly = true;
            this.treat_id.Visible = false;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 90;
            // 
            // pt_id
            // 
            this.pt_id.HeaderText = "pt_id";
            this.pt_id.Name = "pt_id";
            this.pt_id.ReadOnly = true;
            this.pt_id.Visible = false;
            // 
            // plan_id
            // 
            this.plan_id.HeaderText = "plan_id";
            this.plan_id.Name = "plan_id";
            this.plan_id.ReadOnly = true;
            this.plan_id.Visible = false;
            // 
            // payment
            // 
            this.payment.HeaderText = "Payment Status";
            this.payment.Name = "payment";
            this.payment.ReadOnly = true;
            // 
            // dr_id
            // 
            this.dr_id.HeaderText = "dr_id";
            this.dr_id.Name = "dr_id";
            this.dr_id.ReadOnly = true;
            this.dr_id.Visible = false;
            // 
            // nid
            // 
            this.nid.HeaderText = "nid";
            this.nid.Name = "nid";
            this.nid.ReadOnly = true;
            this.nid.Visible = false;
            // 
            // add
            // 
            this.add.HeaderText = "add";
            this.add.Image = global::PappyjoeMVC.Properties.Resources.add__1_;
            this.add.Name = "add";
            this.add.ReadOnly = true;
            this.add.Width = 30;
            // 
            // edit
            // 
            this.edit.HeaderText = "edit";
            this.edit.Image = global::PappyjoeMVC.Properties.Resources.editicon;
            this.edit.Name = "edit";
            this.edit.ReadOnly = true;
            this.edit.Width = 30;
            // 
            // Nurse_notification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1029, 536);
            this.Controls.Add(this.btnshow);
            this.Controls.Add(this.dateTimePicker3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label41);
            this.Controls.Add(this.panellog);
            this.Controls.Add(this.btnCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Nurse_notification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nurse Notification";
            this.Load += new System.EventHandler(this.Nurse_notification_Load);
            this.panellog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_notify)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Panel panellog;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DataGridView DGV_notify;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.Button btnshow;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn slno;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn dr_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn ptid;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn Treatment;
        private System.Windows.Forms.DataGridViewTextBoxColumn treat_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn pt_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn plan_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn payment;
        private System.Windows.Forms.DataGridViewTextBoxColumn dr_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn nid;
        private System.Windows.Forms.DataGridViewImageColumn add;
        private System.Windows.Forms.DataGridViewImageColumn edit;
    }
}