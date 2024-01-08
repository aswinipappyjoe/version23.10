namespace PappyjoeMVC.View
{
    partial class Log
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Log));
            this.panellog = new System.Windows.Forms.Panel();
            this.lb_showmore = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_action = new System.Windows.Forms.ComboBox();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.DGV_Log = new System.Windows.Forms.DataGridView();
            this.log_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.log_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.log_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.log_stage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label41 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panellog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Log)).BeginInit();
            this.SuspendLayout();
            // 
            // panellog
            // 
            this.panellog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panellog.BackColor = System.Drawing.Color.White;
            this.panellog.Controls.Add(this.lb_showmore);
            this.panellog.Controls.Add(this.label2);
            this.panellog.Controls.Add(this.label1);
            this.panellog.Controls.Add(this.cmb_action);
            this.panellog.Controls.Add(this.txtsearch);
            this.panellog.Controls.Add(this.label8);
            this.panellog.Controls.Add(this.DGV_Log);
            this.panellog.Location = new System.Drawing.Point(0, 39);
            this.panellog.Name = "panellog";
            this.panellog.Size = new System.Drawing.Size(875, 487);
            this.panellog.TabIndex = 55;
            // 
            // lb_showmore
            // 
            this.lb_showmore.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_showmore.AutoSize = true;
            this.lb_showmore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lb_showmore.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_showmore.Location = new System.Drawing.Point(804, 43);
            this.lb_showmore.Name = "lb_showmore";
            this.lb_showmore.Size = new System.Drawing.Size(64, 15);
            this.lb_showmore.TabIndex = 285;
            this.lb_showmore.Text = "Show more";
            this.lb_showmore.Click += new System.EventHandler(this.lb_showmore_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(90, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 17);
            this.label2.TabIndex = 58;
            this.label2.Text = "User Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(445, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 17);
            this.label1.TabIndex = 57;
            this.label1.Text = "Action Done";
            // 
            // cmb_action
            // 
            this.cmb_action.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_action.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.cmb_action.FormattingEnabled = true;
            this.cmb_action.Location = new System.Drawing.Point(532, 14);
            this.cmb_action.Name = "cmb_action";
            this.cmb_action.Size = new System.Drawing.Size(110, 24);
            this.cmb_action.TabIndex = 56;
            this.cmb_action.SelectedIndexChanged += new System.EventHandler(this.cmb_action_SelectedIndexChanged);
            // 
            // txtsearch
            // 
            this.txtsearch.BackColor = System.Drawing.Color.White;
            this.txtsearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtsearch.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearch.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.txtsearch.Location = new System.Drawing.Point(176, 14);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(263, 22);
            this.txtsearch.TabIndex = 55;
            this.txtsearch.TextChanged += new System.EventHandler(this.txtsearch_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(12, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 17);
            this.label8.TabIndex = 54;
            this.label8.Text = "Search by";
            // 
            // DGV_Log
            // 
            this.DGV_Log.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.DGV_Log.AllowUserToAddRows = false;
            this.DGV_Log.AllowUserToDeleteRows = false;
            this.DGV_Log.AllowUserToResizeColumns = false;
            this.DGV_Log.AllowUserToResizeRows = false;
            this.DGV_Log.BackgroundColor = System.Drawing.Color.White;
            this.DGV_Log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DGV_Log.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.DGV_Log.ColumnHeadersHeight = 25;
            this.DGV_Log.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DGV_Log.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.log_id,
            this.user_id,
            this.log_type,
            this.log_Description,
            this.date,
            this.time,
            this.log_stage});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV_Log.DefaultCellStyle = dataGridViewCellStyle1;
            this.DGV_Log.GridColor = System.Drawing.Color.White;
            this.DGV_Log.Location = new System.Drawing.Point(6, 61);
            this.DGV_Log.Name = "DGV_Log";
            this.DGV_Log.ReadOnly = true;
            this.DGV_Log.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV_Log.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DGV_Log.RowHeadersVisible = false;
            this.DGV_Log.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DGV_Log.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DGV_Log.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGV_Log.Size = new System.Drawing.Size(857, 414);
            this.DGV_Log.TabIndex = 53;
            this.DGV_Log.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_Log_CellClick);
            // 
            // log_id
            // 
            this.log_id.HeaderText = "Id";
            this.log_id.Name = "log_id";
            this.log_id.ReadOnly = true;
            this.log_id.Visible = false;
            // 
            // user_id
            // 
            this.user_id.HeaderText = "User Name";
            this.user_id.Name = "user_id";
            this.user_id.ReadOnly = true;
            this.user_id.Width = 115;
            // 
            // log_type
            // 
            this.log_type.HeaderText = "Log Type";
            this.log_type.Name = "log_type";
            this.log_type.ReadOnly = true;
            this.log_type.Width = 135;
            // 
            // log_Description
            // 
            this.log_Description.HeaderText = "Description";
            this.log_Description.Name = "log_Description";
            this.log_Description.ReadOnly = true;
            this.log_Description.Width = 225;
            // 
            // date
            // 
            this.date.HeaderText = "Date";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            this.date.Width = 125;
            // 
            // time
            // 
            this.time.HeaderText = "Time";
            this.time.Name = "time";
            this.time.ReadOnly = true;
            // 
            // log_stage
            // 
            this.log_stage.HeaderText = "Action Done";
            this.log_stage.Name = "log_stage";
            this.log_stage.ReadOnly = true;
            this.log_stage.Width = 136;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.Red;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(780, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 30);
            this.btnCancel.TabIndex = 194;
            this.btnCancel.Text = "CLOSE";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label41.Location = new System.Drawing.Point(2, 6);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(128, 21);
            this.label41.TabIndex = 283;
            this.label41.Text = "User Log Details";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.DarkGray;
            this.panel5.Location = new System.Drawing.Point(-4, 35);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(918, 1);
            this.panel5.TabIndex = 284;
            // 
            // Log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(880, 526);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.label41);
            this.Controls.Add(this.panellog);
            this.Controls.Add(this.btnCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Log";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log";
            this.Load += new System.EventHandler(this.Log_Load);
            this.panellog.ResumeLayout(false);
            this.panellog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Log)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panellog;
        internal System.Windows.Forms.DataGridView DGV_Log;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.ComboBox cmb_action;
        private System.Windows.Forms.DataGridViewTextBoxColumn log_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn log_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn log_Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn time;
        private System.Windows.Forms.DataGridViewTextBoxColumn log_stage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_showmore;
    }
}