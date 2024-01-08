namespace PappyjoeMVC.View
{
    partial class stock_updation_list
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(stock_updation_list));
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label41 = new System.Windows.Forms.Label();
            this.cmb_action = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnShow = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DTP_To = new System.Windows.Forms.DateTimePicker();
            this.DTP_From = new System.Windows.Forms.DateTimePicker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RefNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.slno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lab_Msg = new System.Windows.Forms.Label();
            this.dgv_sales = new System.Windows.Forms.DataGridView();
            this.colmore = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_sales)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel4.Controls.Add(this.btnCancel);
            this.panel4.Controls.Add(this.label41);
            this.panel4.Location = new System.Drawing.Point(1, 1);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(896, 35);
            this.panel4.TabIndex = 284;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.Tomato;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(819, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 27);
            this.btnCancel.TabIndex = 207;
            this.btnCancel.Text = "CLOSE";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Visible = false;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label41.Location = new System.Drawing.Point(4, 6);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(203, 21);
            this.label41.TabIndex = 290;
            this.label41.Text = "STOCK ADJUSTMENT  LIST";
            // 
            // cmb_action
            // 
            this.cmb_action.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.cmb_action.FormattingEnabled = true;
            this.cmb_action.Items.AddRange(new object[] {
            "Stock Out",
            "Stock In",
            "Stock Update"});
            this.cmb_action.Location = new System.Drawing.Point(618, 12);
            this.cmb_action.Name = "cmb_action";
            this.cmb_action.Size = new System.Drawing.Size(172, 21);
            this.cmb_action.TabIndex = 299;
            this.cmb_action.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(570, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 298;
            this.label1.Text = "Action";
            this.label1.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.cmb_action);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.BtnShow);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.DTP_To);
            this.panel1.Controls.Add(this.DTP_From);
            this.panel1.Location = new System.Drawing.Point(0, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(897, 46);
            this.panel1.TabIndex = 281;
            // 
            // BtnShow
            // 
            this.BtnShow.BackColor = System.Drawing.Color.LimeGreen;
            this.BtnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnShow.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.BtnShow.ForeColor = System.Drawing.Color.White;
            this.BtnShow.Location = new System.Drawing.Point(820, 9);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(74, 27);
            this.BtnShow.TabIndex = 297;
            this.BtnShow.Text = "Show";
            this.BtnShow.UseVisualStyleBackColor = false;
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(10, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 17);
            this.label8.TabIndex = 292;
            this.label8.Text = "Date from";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(294, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 17);
            this.label2.TabIndex = 291;
            this.label2.Text = "Date To";
            // 
            // DTP_To
            // 
            this.DTP_To.Location = new System.Drawing.Point(352, 12);
            this.DTP_To.Name = "DTP_To";
            this.DTP_To.Size = new System.Drawing.Size(200, 20);
            this.DTP_To.TabIndex = 3;
            // 
            // DTP_From
            // 
            this.DTP_From.Location = new System.Drawing.Point(81, 12);
            this.DTP_From.Name = "DTP_From";
            this.DTP_From.Size = new System.Drawing.Size(200, 20);
            this.DTP_From.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.Gray;
            this.panel3.Location = new System.Drawing.Point(-2, 38);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(900, 1);
            this.panel3.TabIndex = 283;
            // 
            // cost
            // 
            this.cost.HeaderText = "Unit Cost";
            this.cost.Name = "cost";
            this.cost.ReadOnly = true;
            this.cost.Visible = false;
            this.cost.Width = 110;
            // 
            // labname
            // 
            this.labname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.labname.HeaderText = "Lab Name";
            this.labname.Name = "labname";
            this.labname.ReadOnly = true;
            // 
            // date
            // 
            this.date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.date.HeaderText = "Date";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            // 
            // RefNo
            // 
            this.RefNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.RefNo.HeaderText = "RefNo";
            this.RefNo.Name = "RefNo";
            this.RefNo.ReadOnly = true;
            // 
            // slno
            // 
            this.slno.HeaderText = "SlNo";
            this.slno.Name = "slno";
            this.slno.ReadOnly = true;
            this.slno.Width = 75;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // Lab_Msg
            // 
            this.Lab_Msg.BackColor = System.Drawing.Color.Wheat;
            this.Lab_Msg.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_Msg.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.Lab_Msg.Location = new System.Drawing.Point(161, 212);
            this.Lab_Msg.Name = "Lab_Msg";
            this.Lab_Msg.Size = new System.Drawing.Size(563, 25);
            this.Lab_Msg.TabIndex = 278;
            this.Lab_Msg.Text = "No Records Found. Please change the date and then try again!..";
            this.Lab_Msg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Lab_Msg.Visible = false;
            // 
            // dgv_sales
            // 
            this.dgv_sales.AllowUserToAddRows = false;
            this.dgv_sales.AllowUserToDeleteRows = false;
            this.dgv_sales.AllowUserToResizeColumns = false;
            this.dgv_sales.AllowUserToResizeRows = false;
            this.dgv_sales.BackgroundColor = System.Drawing.Color.White;
            this.dgv_sales.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_sales.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgv_sales.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_sales.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_sales.ColumnHeadersHeight = 25;
            this.dgv_sales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_sales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.slno,
            this.RefNo,
            this.date,
            this.labname,
            this.cost,
            this.colmore});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_sales.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_sales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_sales.GridColor = System.Drawing.Color.LightGray;
            this.dgv_sales.Location = new System.Drawing.Point(0, 0);
            this.dgv_sales.Name = "dgv_sales";
            this.dgv_sales.ReadOnly = true;
            this.dgv_sales.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_sales.RowHeadersVisible = false;
            this.dgv_sales.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_sales.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_sales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_sales.Size = new System.Drawing.Size(895, 468);
            this.dgv_sales.TabIndex = 0;
            this.dgv_sales.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_sales_CellClick);
            // 
            // colmore
            // 
            this.colmore.HeaderText = "";
            this.colmore.Image = global::PappyjoeMVC.Properties.Resources.lab_icon;
            this.colmore.Name = "colmore";
            this.colmore.ReadOnly = true;
            this.colmore.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colmore.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colmore.Width = 20;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.Lab_Msg);
            this.panel2.Controls.Add(this.dgv_sales);
            this.panel2.Location = new System.Drawing.Point(2, 94);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(895, 468);
            this.panel2.TabIndex = 282;
            // 
            // stock_updation_list
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(899, 564);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "stock_updation_list";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Updation List";
            this.Load += new System.EventHandler(this.stock_updation_list_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_sales)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.ComboBox cmb_action;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnShow;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker DTP_To;
        private System.Windows.Forms.DateTimePicker DTP_From;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridViewTextBoxColumn cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn labname;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn RefNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn slno;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.Label Lab_Msg;
        private System.Windows.Forms.DataGridView dgv_sales;
        private System.Windows.Forms.DataGridViewImageColumn colmore;
        private System.Windows.Forms.Panel panel2;
    }
}