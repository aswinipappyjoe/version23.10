namespace PappyjoeMVC.View
{
    partial class StockTransfer_Report
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockTransfer_Report));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btn_show = new System.Windows.Forms.Button();
            this.btnEXPORT = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_stckINorOUT = new System.Windows.Forms.ComboBox();
            this.dateTo = new System.Windows.Forms.DateTimePicker();
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.dgv_Sockdetails = new System.Windows.Forms.DataGridView();
            this.SlNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.batch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockinout = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gst = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.currentstock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalamount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label46 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.Lab_totalExpense = new System.Windows.Forms.Label();
            this.Lab_TotalIncome = new System.Windows.Forms.Label();
            this.Lab_AmountPaid = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Sockdetails)).BeginInit();
            this.label46.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cmb_stckINorOUT);
            this.panel2.Controls.Add(this.dateTo);
            this.panel2.Controls.Add(this.dateFrom);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1337, 92);
            this.panel2.TabIndex = 226;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btn_show);
            this.panel5.Controls.Add(this.btnEXPORT);
            this.panel5.Controls.Add(this.buttonClose);
            this.panel5.Controls.Add(this.btnprint);
            this.panel5.Location = new System.Drawing.Point(716, 53);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(424, 36);
            this.panel5.TabIndex = 220;
            // 
            // btn_show
            // 
            this.btn_show.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_show.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_show.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_show.Location = new System.Drawing.Point(203, 2);
            this.btn_show.Name = "btn_show";
            this.btn_show.Size = new System.Drawing.Size(100, 32);
            this.btn_show.TabIndex = 221;
            this.btn_show.Text = "Show";
            this.btn_show.UseVisualStyleBackColor = false;
            this.btn_show.Click += new System.EventHandler(this.btn_show_Click);
            // 
            // btnEXPORT
            // 
            this.btnEXPORT.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnEXPORT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEXPORT.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEXPORT.ForeColor = System.Drawing.Color.White;
            this.btnEXPORT.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEXPORT.Location = new System.Drawing.Point(103, 2);
            this.btnEXPORT.Name = "btnEXPORT";
            this.btnEXPORT.Size = new System.Drawing.Size(100, 32);
            this.btnEXPORT.TabIndex = 213;
            this.btnEXPORT.Text = "Export to Excel";
            this.btnEXPORT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEXPORT.UseVisualStyleBackColor = false;
            this.btnEXPORT.Click += new System.EventHandler(this.btnEXPORT_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.Color.Tomato;
            this.buttonClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonClose.Location = new System.Drawing.Point(303, 2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(100, 32);
            this.buttonClose.TabIndex = 212;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Image = ((System.Drawing.Image)(resources.GetObject("btnprint.Image")));
            this.btnprint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnprint.Location = new System.Drawing.Point(3, 2);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(100, 32);
            this.btnprint.TabIndex = 7;
            this.btnprint.Text = "Print";
            this.btnprint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(285, 40);
            this.label1.TabIndex = 219;
            this.label1.Text = "Stock Transfer Report";
            // 
            // cmb_stckINorOUT
            // 
            this.cmb_stckINorOUT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_stckINorOUT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_stckINorOUT.FormattingEnabled = true;
            this.cmb_stckINorOUT.Items.AddRange(new object[] {
            "Stock In",
            "Stock Out"});
            this.cmb_stckINorOUT.Location = new System.Drawing.Point(36, 60);
            this.cmb_stckINorOUT.Name = "cmb_stckINorOUT";
            this.cmb_stckINorOUT.Size = new System.Drawing.Size(162, 21);
            this.cmb_stckINorOUT.TabIndex = 1;
            this.cmb_stckINorOUT.SelectedIndexChanged += new System.EventHandler(this.cmb_stckINorOUT_SelectedIndexChanged);
            // 
            // dateTo
            // 
            this.dateTo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTo.Location = new System.Drawing.Point(433, 60);
            this.dateTo.Name = "dateTo";
            this.dateTo.Size = new System.Drawing.Size(223, 22);
            this.dateTo.TabIndex = 2;
            // 
            // dateFrom
            // 
            this.dateFrom.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateFrom.Location = new System.Drawing.Point(204, 60);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(223, 22);
            this.dateFrom.TabIndex = 2;
            // 
            // dgv_Sockdetails
            // 
            this.dgv_Sockdetails.AllowUserToAddRows = false;
            this.dgv_Sockdetails.AllowUserToDeleteRows = false;
            this.dgv_Sockdetails.AllowUserToResizeColumns = false;
            this.dgv_Sockdetails.AllowUserToResizeRows = false;
            this.dgv_Sockdetails.BackgroundColor = System.Drawing.Color.White;
            this.dgv_Sockdetails.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_Sockdetails.ColumnHeadersHeight = 31;
            this.dgv_Sockdetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_Sockdetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SlNo,
            this.date,
            this.itemcode,
            this.itemname,
            this.labname,
            this.batch,
            this.stockinout,
            this.unit,
            this.gst,
            this.rate,
            this.currentstock,
            this.totalamount});
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Sockdetails.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgv_Sockdetails.GridColor = System.Drawing.Color.White;
            this.dgv_Sockdetails.Location = new System.Drawing.Point(0, 93);
            this.dgv_Sockdetails.Name = "dgv_Sockdetails";
            this.dgv_Sockdetails.ReadOnly = true;
            this.dgv_Sockdetails.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_Sockdetails.RowHeadersVisible = false;
            this.dgv_Sockdetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_Sockdetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_Sockdetails.Size = new System.Drawing.Size(1337, 502);
            this.dgv_Sockdetails.TabIndex = 227;
            // 
            // SlNo
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SlNo.DefaultCellStyle = dataGridViewCellStyle1;
            this.SlNo.HeaderText = "Sl.No";
            this.SlNo.Name = "SlNo";
            this.SlNo.ReadOnly = true;
            this.SlNo.Width = 40;
            // 
            // date
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.date.DefaultCellStyle = dataGridViewCellStyle2;
            this.date.HeaderText = "DATE";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            // 
            // itemcode
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.itemcode.DefaultCellStyle = dataGridViewCellStyle3;
            this.itemcode.HeaderText = "ITEM CODE";
            this.itemcode.Name = "itemcode";
            this.itemcode.ReadOnly = true;
            // 
            // itemname
            // 
            this.itemname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.itemname.DefaultCellStyle = dataGridViewCellStyle4;
            this.itemname.HeaderText = "ITEM NAME";
            this.itemname.Name = "itemname";
            this.itemname.ReadOnly = true;
            // 
            // labname
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.labname.DefaultCellStyle = dataGridViewCellStyle5;
            this.labname.HeaderText = "LAB NAME";
            this.labname.Name = "labname";
            this.labname.ReadOnly = true;
            this.labname.Width = 150;
            // 
            // batch
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.batch.DefaultCellStyle = dataGridViewCellStyle6;
            this.batch.HeaderText = "BATCH NO";
            this.batch.Name = "batch";
            this.batch.ReadOnly = true;
            // 
            // stockinout
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.stockinout.DefaultCellStyle = dataGridViewCellStyle7;
            this.stockinout.HeaderText = "STOCK QTY";
            this.stockinout.Name = "stockinout";
            this.stockinout.ReadOnly = true;
            this.stockinout.Width = 70;
            // 
            // unit
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.unit.DefaultCellStyle = dataGridViewCellStyle8;
            this.unit.HeaderText = "UNIT";
            this.unit.Name = "unit";
            this.unit.ReadOnly = true;
            // 
            // gst
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.gst.DefaultCellStyle = dataGridViewCellStyle9;
            this.gst.HeaderText = "GST";
            this.gst.Name = "gst";
            this.gst.ReadOnly = true;
            this.gst.Width = 75;
            // 
            // rate
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.rate.DefaultCellStyle = dataGridViewCellStyle10;
            this.rate.HeaderText = "RATE";
            this.rate.Name = "rate";
            this.rate.ReadOnly = true;
            this.rate.Width = 80;
            // 
            // currentstock
            // 
            this.currentstock.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.currentstock.DefaultCellStyle = dataGridViewCellStyle11;
            this.currentstock.HeaderText = "CURRENT STOCK";
            this.currentstock.Name = "currentstock";
            this.currentstock.ReadOnly = true;
            this.currentstock.Visible = false;
            // 
            // totalamount
            // 
            this.totalamount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.totalamount.DefaultCellStyle = dataGridViewCellStyle12;
            this.totalamount.HeaderText = "TOTAL AMOUNT";
            this.totalamount.Name = "totalamount";
            this.totalamount.ReadOnly = true;
            // 
            // label46
            // 
            this.label46.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label46.BackColor = System.Drawing.Color.Gainsboro;
            this.label46.Controls.Add(this.label2);
            this.label46.Controls.Add(this.Lab_totalExpense);
            this.label46.Controls.Add(this.Lab_TotalIncome);
            this.label46.Controls.Add(this.Lab_AmountPaid);
            this.label46.Location = new System.Drawing.Point(0, 596);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(1337, 53);
            this.label46.TabIndex = 273;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.ForestGreen;
            this.label2.Location = new System.Drawing.Point(1253, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 17);
            this.label2.TabIndex = 43;
            this.label2.Text = "0.00";
            // 
            // Lab_totalExpense
            // 
            this.Lab_totalExpense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Lab_totalExpense.AutoSize = true;
            this.Lab_totalExpense.BackColor = System.Drawing.Color.Gainsboro;
            this.Lab_totalExpense.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_totalExpense.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.Lab_totalExpense.Location = new System.Drawing.Point(1156, 19);
            this.Lab_totalExpense.Name = "Lab_totalExpense";
            this.Lab_totalExpense.Size = new System.Drawing.Size(94, 17);
            this.Lab_totalExpense.TabIndex = 42;
            this.Lab_totalExpense.Text = "Total Amount:";
            // 
            // Lab_TotalIncome
            // 
            this.Lab_TotalIncome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Lab_TotalIncome.AutoSize = true;
            this.Lab_TotalIncome.BackColor = System.Drawing.Color.Gainsboro;
            this.Lab_TotalIncome.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_TotalIncome.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.Lab_TotalIncome.Location = new System.Drawing.Point(1005, 23);
            this.Lab_TotalIncome.Name = "Lab_TotalIncome";
            this.Lab_TotalIncome.Size = new System.Drawing.Size(75, 17);
            this.Lab_TotalIncome.TabIndex = 41;
            this.Lab_TotalIncome.Text = "Total Rate :";
            // 
            // Lab_AmountPaid
            // 
            this.Lab_AmountPaid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Lab_AmountPaid.AutoSize = true;
            this.Lab_AmountPaid.BackColor = System.Drawing.Color.Gainsboro;
            this.Lab_AmountPaid.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_AmountPaid.ForeColor = System.Drawing.Color.ForestGreen;
            this.Lab_AmountPaid.Location = new System.Drawing.Point(1086, 22);
            this.Lab_AmountPaid.Name = "Lab_AmountPaid";
            this.Lab_AmountPaid.Size = new System.Drawing.Size(33, 17);
            this.Lab_AmountPaid.TabIndex = 39;
            this.Lab_AmountPaid.Text = "0.00";
            // 
            // StockTransfer_Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1337, 649);
            this.Controls.Add(this.label46);
            this.Controls.Add(this.dgv_Sockdetails);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StockTransfer_Report";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Transfer Report";
            this.Load += new System.EventHandler(this.StockTransfer_Report_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Sockdetails)).EndInit();
            this.label46.ResumeLayout(false);
            this.label46.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btn_show;
        private System.Windows.Forms.Button btnEXPORT;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_stckINorOUT;
        private System.Windows.Forms.DateTimePicker dateTo;
        private System.Windows.Forms.DateTimePicker dateFrom;
        private System.Windows.Forms.DataGridView dgv_Sockdetails;
        private System.Windows.Forms.Panel label46;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Lab_totalExpense;
        private System.Windows.Forms.Label Lab_TotalIncome;
        private System.Windows.Forms.Label Lab_AmountPaid;
        private System.Windows.Forms.DataGridViewTextBoxColumn SlNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemname;
        private System.Windows.Forms.DataGridViewTextBoxColumn labname;
        private System.Windows.Forms.DataGridViewTextBoxColumn batch;
        private System.Windows.Forms.DataGridViewTextBoxColumn stockinout;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn gst;
        private System.Windows.Forms.DataGridViewTextBoxColumn rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn currentstock;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalamount;
    }
}