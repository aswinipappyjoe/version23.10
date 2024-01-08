
namespace PappyjoeMVC.View
{
    partial class Stock_Ledger
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Stock_Ledger));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_all_stk = new System.Windows.Forms.Button();
            this.lbPatient = new System.Windows.Forms.ListBox();
            this.txtitemcode = new System.Windows.Forms.TextBox();
            this.lb_itemname = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtp_from = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.dtp_to = new System.Windows.Forms.DateTimePicker();
            this.dgv_Stock = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.voucher = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.particulars = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receipts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receipt_rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.issuse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.issuse_rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Stock)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.btnReport);
            this.panel1.Location = new System.Drawing.Point(-1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1178, 34);
            this.panel1.TabIndex = 276;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(4, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 21);
            this.label8.TabIndex = 291;
            this.label8.Text = "STOCK LEDGER";
            // 
            // btnReport
            // 
            this.btnReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReport.BackColor = System.Drawing.Color.LimeGreen;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.White;
            this.btnReport.Location = new System.Drawing.Point(862, 4);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(93, 25);
            this.btnReport.TabIndex = 273;
            this.btnReport.Text = "PRINT";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.LimeGreen;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(899, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(65, 24);
            this.btnSave.TabIndex = 202;
            this.btnSave.Text = "Show";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel3.Location = new System.Drawing.Point(-24, -15);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1264, 1);
            this.panel3.TabIndex = 279;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel2.Controls.Add(this.btn_all_stk);
            this.panel2.Controls.Add(this.lbPatient);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.txtitemcode);
            this.panel2.Controls.Add(this.lb_itemname);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.dtp_from);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.dtp_to);
            this.panel2.Location = new System.Drawing.Point(-1, 36);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1178, 98);
            this.panel2.TabIndex = 278;
            // 
            // btn_all_stk
            // 
            this.btn_all_stk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_all_stk.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_all_stk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_all_stk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_all_stk.ForeColor = System.Drawing.Color.White;
            this.btn_all_stk.Location = new System.Drawing.Point(4, 44);
            this.btn_all_stk.Name = "btn_all_stk";
            this.btn_all_stk.Size = new System.Drawing.Size(114, 24);
            this.btn_all_stk.TabIndex = 311;
            this.btn_all_stk.Text = "All Items Stock";
            this.btn_all_stk.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_all_stk.UseVisualStyleBackColor = false;
            this.btn_all_stk.Click += new System.EventHandler(this.btn_all_stk_Click);
            // 
            // lbPatient
            // 
            this.lbPatient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPatient.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbPatient.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPatient.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lbPatient.FormattingEnabled = true;
            this.lbPatient.Location = new System.Drawing.Point(646, 33);
            this.lbPatient.Name = "lbPatient";
            this.lbPatient.Size = new System.Drawing.Size(244, 54);
            this.lbPatient.TabIndex = 310;
            this.lbPatient.Visible = false;
            this.lbPatient.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbPatient_MouseClick);
            // 
            // txtitemcode
            // 
            this.txtitemcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtitemcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtitemcode.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtitemcode.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.txtitemcode.Location = new System.Drawing.Point(646, 9);
            this.txtitemcode.MaxLength = 25;
            this.txtitemcode.Name = "txtitemcode";
            this.txtitemcode.Size = new System.Drawing.Size(244, 22);
            this.txtitemcode.TabIndex = 309;
            this.txtitemcode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPatient_KeyUp);
            // 
            // lb_itemname
            // 
            this.lb_itemname.AutoSize = true;
            this.lb_itemname.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_itemname.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lb_itemname.Location = new System.Drawing.Point(5, 71);
            this.lb_itemname.Name = "lb_itemname";
            this.lb_itemname.Size = new System.Drawing.Size(87, 21);
            this.lb_itemname.TabIndex = 306;
            this.lb_itemname.Text = "ItemName";
            this.lb_itemname.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(569, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 17);
            this.label2.TabIndex = 308;
            this.label2.Text = "Item Code :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 303;
            this.label1.Text = "From Date :";
            // 
            // dtp_from
            // 
            this.dtp_from.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_from.Location = new System.Drawing.Point(86, 9);
            this.dtp_from.Name = "dtp_from";
            this.dtp_from.Size = new System.Drawing.Size(196, 20);
            this.dtp_from.TabIndex = 302;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label12.Location = new System.Drawing.Point(294, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 17);
            this.label12.TabIndex = 301;
            this.label12.Text = "To  Date :";
            // 
            // dtp_to
            // 
            this.dtp_to.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_to.Location = new System.Drawing.Point(362, 9);
            this.dtp_to.Name = "dtp_to";
            this.dtp_to.Size = new System.Drawing.Size(196, 20);
            this.dtp_to.TabIndex = 300;
            // 
            // dgv_Stock
            // 
            this.dgv_Stock.AllowUserToAddRows = false;
            this.dgv_Stock.AllowUserToDeleteRows = false;
            this.dgv_Stock.AllowUserToResizeColumns = false;
            this.dgv_Stock.AllowUserToResizeRows = false;
            this.dgv_Stock.BackgroundColor = System.Drawing.Color.White;
            this.dgv_Stock.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_Stock.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgv_Stock.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Stock.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Stock.ColumnHeadersHeight = 25;
            this.dgv_Stock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_Stock.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.date,
            this.voucher,
            this.particulars,
            this.type,
            this.receipts,
            this.receipt_rate,
            this.issuse,
            this.issuse_rate,
            this.balance});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Stock.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgv_Stock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Stock.Location = new System.Drawing.Point(0, 0);
            this.dgv_Stock.Name = "dgv_Stock";
            this.dgv_Stock.ReadOnly = true;
            this.dgv_Stock.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Stock.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgv_Stock.RowHeadersVisible = false;
            this.dgv_Stock.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_Stock.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_Stock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv_Stock.Size = new System.Drawing.Size(1174, 507);
            this.dgv_Stock.TabIndex = 280;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.id.Visible = false;
            this.id.Width = 50;
            // 
            // date
            // 
            this.date.FillWeight = 60F;
            this.date.HeaderText = "Date";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            this.date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.date.Width = 65;
            // 
            // voucher
            // 
            this.voucher.FillWeight = 120F;
            this.voucher.HeaderText = "Voucher No";
            this.voucher.Name = "voucher";
            this.voucher.ReadOnly = true;
            this.voucher.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.voucher.Width = 125;
            // 
            // particulars
            // 
            this.particulars.FillWeight = 40F;
            this.particulars.HeaderText = "Particulars";
            this.particulars.Name = "particulars";
            this.particulars.ReadOnly = true;
            this.particulars.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.particulars.Width = 250;
            // 
            // type
            // 
            this.type.HeaderText = "Type";
            this.type.Name = "type";
            this.type.ReadOnly = true;
            this.type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.type.Width = 169;
            // 
            // receipts
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.receipts.DefaultCellStyle = dataGridViewCellStyle2;
            this.receipts.FillWeight = 50F;
            this.receipts.HeaderText = "Receipts";
            this.receipts.Name = "receipts";
            this.receipts.ReadOnly = true;
            this.receipts.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.receipts.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.receipts.Width = 130;
            // 
            // receipt_rate
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.receipt_rate.DefaultCellStyle = dataGridViewCellStyle3;
            this.receipt_rate.FillWeight = 50F;
            this.receipt_rate.HeaderText = "Receipts Rate";
            this.receipt_rate.Name = "receipt_rate";
            this.receipt_rate.ReadOnly = true;
            this.receipt_rate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.receipt_rate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.receipt_rate.Width = 90;
            // 
            // issuse
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.issuse.DefaultCellStyle = dataGridViewCellStyle4;
            this.issuse.FillWeight = 50F;
            this.issuse.HeaderText = "Issuse ";
            this.issuse.Name = "issuse";
            this.issuse.ReadOnly = true;
            this.issuse.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.issuse.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.issuse.Width = 130;
            // 
            // issuse_rate
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.issuse_rate.DefaultCellStyle = dataGridViewCellStyle5;
            this.issuse_rate.FillWeight = 60F;
            this.issuse_rate.HeaderText = "Issuse Rate";
            this.issuse_rate.Name = "issuse_rate";
            this.issuse_rate.ReadOnly = true;
            this.issuse_rate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.issuse_rate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.issuse_rate.Width = 90;
            // 
            // balance
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.balance.DefaultCellStyle = dataGridViewCellStyle6;
            this.balance.FillWeight = 60F;
            this.balance.HeaderText = "Balance ";
            this.balance.Name = "balance";
            this.balance.ReadOnly = true;
            this.balance.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.balance.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.balance.Width = 110;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel4.Controls.Add(this.dgv_Stock);
            this.panel4.Location = new System.Drawing.Point(3, 143);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1174, 507);
            this.panel4.TabIndex = 281;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.DarkGray;
            this.panel5.Location = new System.Drawing.Point(-7, 136);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1500, 1);
            this.panel5.TabIndex = 279;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.DarkGray;
            this.panel6.Location = new System.Drawing.Point(-1, 32);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1182, 1);
            this.panel6.TabIndex = 308;
            // 
            // Stock_Ledger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1182, 651);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Stock_Ledger";
            this.Text = "Stock Ledger";
            this.Load += new System.EventHandler(this.Stock_Ledger_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Stock)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgv_Stock;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtp_from;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtp_to;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbPatient;
        private System.Windows.Forms.TextBox txtitemcode;
        private System.Windows.Forms.Button btn_all_stk;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lb_itemname;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn voucher;
        private System.Windows.Forms.DataGridViewTextBoxColumn particulars;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn receipts;
        private System.Windows.Forms.DataGridViewTextBoxColumn receipt_rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn issuse;
        private System.Windows.Forms.DataGridViewTextBoxColumn issuse_rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn balance;
    }
}