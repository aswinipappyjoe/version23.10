namespace PappyjoeMVC.View
{
    partial class daily_GST_report
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(daily_GST_report));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Lab_Msg = new System.Windows.Forms.Label();
            this.Grvsummary = new System.Windows.Forms.DataGridView();
            this.btn_show = new System.Windows.Forms.Button();
            this.btnEXPORT = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.rad_sales = new System.Windows.Forms.RadioButton();
            this.rad_pur = new System.Windows.Forms.RadioButton();
            this.dateTimePickerdaily1 = new System.Windows.Forms.DateTimePicker();
            this.btnprint = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.ls_gst = new System.Windows.Forms.Label();
            this.ls_amount = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_gst = new System.Windows.Forms.TextBox();
            this.DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BILLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUSTOMERNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TAXAMOUNT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TAX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOTAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grvsummary)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.Lab_Msg);
            this.panel1.Controls.Add(this.Grvsummary);
            this.panel1.Location = new System.Drawing.Point(2, 102);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1351, 587);
            this.panel1.TabIndex = 228;
            // 
            // Lab_Msg
            // 
            this.Lab_Msg.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Lab_Msg.BackColor = System.Drawing.Color.Wheat;
            this.Lab_Msg.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_Msg.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.Lab_Msg.Location = new System.Drawing.Point(271, 270);
            this.Lab_Msg.Name = "Lab_Msg";
            this.Lab_Msg.Size = new System.Drawing.Size(561, 25);
            this.Lab_Msg.TabIndex = 279;
            this.Lab_Msg.Text = "No Records Found. Please change the date and then try again!..";
            this.Lab_Msg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Lab_Msg.Visible = false;
            // 
            // Grvsummary
            // 
            this.Grvsummary.AllowUserToAddRows = false;
            this.Grvsummary.AllowUserToDeleteRows = false;
            this.Grvsummary.AllowUserToResizeColumns = false;
            this.Grvsummary.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Bisque;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.Grvsummary.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Grvsummary.BackgroundColor = System.Drawing.Color.White;
            this.Grvsummary.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grvsummary.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Grvsummary.ColumnHeadersHeight = 28;
            this.Grvsummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Grvsummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DATE,
            this.BILLNO,
            this.CUSTOMERNAME,
            this.TAXAMOUNT,
            this.TAX,
            this.TOTAL,
            this.MODE});
            this.Grvsummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grvsummary.GridColor = System.Drawing.SystemColors.ButtonShadow;
            this.Grvsummary.Location = new System.Drawing.Point(0, 0);
            this.Grvsummary.Name = "Grvsummary";
            this.Grvsummary.ReadOnly = true;
            this.Grvsummary.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.Grvsummary.RowHeadersVisible = false;
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black;
            this.Grvsummary.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.Grvsummary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Grvsummary.Size = new System.Drawing.Size(1351, 587);
            this.Grvsummary.TabIndex = 222;
            // 
            // btn_show
            // 
            this.btn_show.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_show.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_show.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_show.Location = new System.Drawing.Point(420, 14);
            this.btn_show.Name = "btn_show";
            this.btn_show.Size = new System.Drawing.Size(75, 24);
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
            this.btnEXPORT.Location = new System.Drawing.Point(1120, 3);
            this.btnEXPORT.Name = "btnEXPORT";
            this.btnEXPORT.Size = new System.Drawing.Size(105, 30);
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
            this.buttonClose.Location = new System.Drawing.Point(1219, 3);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 30);
            this.buttonClose.TabIndex = 212;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.txt_gst);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.btn_show);
            this.panel5.Controls.Add(this.rad_sales);
            this.panel5.Controls.Add(this.rad_pur);
            this.panel5.Controls.Add(this.dateTimePickerdaily1);
            this.panel5.Location = new System.Drawing.Point(2, 56);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1351, 43);
            this.panel5.TabIndex = 227;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(9, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 17);
            this.label2.TabIndex = 232;
            this.label2.Text = "Date";
            // 
            // rad_sales
            // 
            this.rad_sales.AutoSize = true;
            this.rad_sales.Checked = true;
            this.rad_sales.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.rad_sales.Location = new System.Drawing.Point(362, 19);
            this.rad_sales.Name = "rad_sales";
            this.rad_sales.Size = new System.Drawing.Size(51, 17);
            this.rad_sales.TabIndex = 231;
            this.rad_sales.TabStop = true;
            this.rad_sales.Text = "Sales";
            this.rad_sales.UseVisualStyleBackColor = true;
            this.rad_sales.CheckedChanged += new System.EventHandler(this.rad_sales_CheckedChanged);
            this.rad_sales.Click += new System.EventHandler(this.rad_sales_Click);
            // 
            // rad_pur
            // 
            this.rad_pur.AutoSize = true;
            this.rad_pur.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.rad_pur.Location = new System.Drawing.Point(286, 19);
            this.rad_pur.Name = "rad_pur";
            this.rad_pur.Size = new System.Drawing.Size(70, 17);
            this.rad_pur.TabIndex = 230;
            this.rad_pur.Text = "Purchase";
            this.rad_pur.UseVisualStyleBackColor = true;
            this.rad_pur.CheckedChanged += new System.EventHandler(this.rad_pur_CheckedChanged);
            this.rad_pur.Click += new System.EventHandler(this.rad_pur_Click);
            // 
            // dateTimePickerdaily1
            // 
            this.dateTimePickerdaily1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerdaily1.Location = new System.Drawing.Point(50, 18);
            this.dateTimePickerdaily1.Name = "dateTimePickerdaily1";
            this.dateTimePickerdaily1.Size = new System.Drawing.Size(223, 22);
            this.dateTimePickerdaily1.TabIndex = 229;
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Image = ((System.Drawing.Image)(resources.GetObject("btnprint.Image")));
            this.btnprint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnprint.Location = new System.Drawing.Point(1034, 3);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(85, 30);
            this.btnprint.TabIndex = 7;
            this.btnprint.Text = "Print";
            this.btnprint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.btnEXPORT);
            this.panel3.Controls.Add(this.buttonClose);
            this.panel3.Controls.Add(this.btnprint);
            this.panel3.Location = new System.Drawing.Point(2, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1351, 53);
            this.panel3.TabIndex = 232;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(16, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 40);
            this.label1.TabIndex = 226;
            this.label1.Text = "Daily GST Report";
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BackColor = System.Drawing.Color.Bisque;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.ls_gst);
            this.panel6.Controls.Add(this.ls_amount);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Location = new System.Drawing.Point(2, 692);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1351, 49);
            this.panel6.TabIndex = 280;
            this.panel6.Paint += new System.Windows.Forms.PaintEventHandler(this.panel6_Paint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Bisque;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkGreen;
            this.label3.Location = new System.Drawing.Point(1100, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 20);
            this.label3.TabIndex = 15;
            this.label3.Text = "0.00";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ls_gst
            // 
            this.ls_gst.AutoSize = true;
            this.ls_gst.BackColor = System.Drawing.Color.Bisque;
            this.ls_gst.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ls_gst.ForeColor = System.Drawing.Color.DarkGreen;
            this.ls_gst.Location = new System.Drawing.Point(899, 8);
            this.ls_gst.Name = "ls_gst";
            this.ls_gst.Size = new System.Drawing.Size(37, 20);
            this.ls_gst.TabIndex = 14;
            this.ls_gst.Text = "0.00";
            this.ls_gst.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ls_amount
            // 
            this.ls_amount.AutoSize = true;
            this.ls_amount.BackColor = System.Drawing.Color.Bisque;
            this.ls_amount.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ls_amount.ForeColor = System.Drawing.Color.DarkGreen;
            this.ls_amount.Location = new System.Drawing.Point(699, 8);
            this.ls_amount.Name = "ls_amount";
            this.ls_amount.Size = new System.Drawing.Size(37, 20);
            this.ls_amount.TabIndex = 9;
            this.ls_amount.Text = "0.00";
            this.ls_amount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Bisque;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label9.Location = new System.Drawing.Point(488, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 21);
            this.label9.TabIndex = 7;
            this.label9.Text = "TOTAL(INR):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(537, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 17);
            this.label4.TabIndex = 233;
            this.label4.Text = "GST IN";
            // 
            // txt_gst
            // 
            this.txt_gst.Location = new System.Drawing.Point(592, 17);
            this.txt_gst.Name = "txt_gst";
            this.txt_gst.ReadOnly = true;
            this.txt_gst.Size = new System.Drawing.Size(164, 20);
            this.txt_gst.TabIndex = 234;
            // 
            // DATE
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.DATE.DefaultCellStyle = dataGridViewCellStyle3;
            this.DATE.HeaderText = "Date";
            this.DATE.Name = "DATE";
            this.DATE.ReadOnly = true;
            this.DATE.Width = 200;
            // 
            // BILLNO
            // 
            this.BILLNO.DataPropertyName = "pt_name";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.BILLNO.DefaultCellStyle = dataGridViewCellStyle4;
            this.BILLNO.HeaderText = "Bill No";
            this.BILLNO.Name = "BILLNO";
            this.BILLNO.ReadOnly = true;
            this.BILLNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BILLNO.Width = 200;
            // 
            // CUSTOMERNAME
            // 
            this.CUSTOMERNAME.DataPropertyName = "invoice_no";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.CUSTOMERNAME.DefaultCellStyle = dataGridViewCellStyle5;
            this.CUSTOMERNAME.HeaderText = "Name";
            this.CUSTOMERNAME.Name = "CUSTOMERNAME";
            this.CUSTOMERNAME.ReadOnly = true;
            this.CUSTOMERNAME.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CUSTOMERNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CUSTOMERNAME.Width = 201;
            // 
            // TAXAMOUNT
            // 
            this.TAXAMOUNT.DataPropertyName = "receipt_no";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            this.TAXAMOUNT.DefaultCellStyle = dataGridViewCellStyle6;
            this.TAXAMOUNT.HeaderText = "Taxable Amount";
            this.TAXAMOUNT.Name = "TAXAMOUNT";
            this.TAXAMOUNT.ReadOnly = true;
            this.TAXAMOUNT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TAXAMOUNT.Width = 200;
            // 
            // TAX
            // 
            this.TAX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TAX.DataPropertyName = "services";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            this.TAX.DefaultCellStyle = dataGridViewCellStyle7;
            this.TAX.HeaderText = "Tax";
            this.TAX.Name = "TAX";
            this.TAX.ReadOnly = true;
            this.TAX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TAX.Width = 150;
            // 
            // TOTAL
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            this.TOTAL.DefaultCellStyle = dataGridViewCellStyle8;
            this.TOTAL.HeaderText = "Total";
            this.TOTAL.Name = "TOTAL";
            this.TOTAL.ReadOnly = true;
            this.TOTAL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TOTAL.Width = 200;
            // 
            // MODE
            // 
            this.MODE.DataPropertyName = "doctor_name";
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            this.MODE.DefaultCellStyle = dataGridViewCellStyle9;
            this.MODE.HeaderText = "Mode Of Payment";
            this.MODE.Name = "MODE";
            this.MODE.ReadOnly = true;
            this.MODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MODE.Width = 200;
            // 
            // daily_GST_report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1355, 741);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "daily_GST_report";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Daily GST  Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.daily_GST_report_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grvsummary)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView Grvsummary;
        private System.Windows.Forms.Button btn_show;
        private System.Windows.Forms.Button btnEXPORT;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DateTimePicker dateTimePickerdaily1;
        private System.Windows.Forms.Label Lab_Msg;
        private System.Windows.Forms.RadioButton rad_sales;
        private System.Windows.Forms.RadioButton rad_pur;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label ls_gst;
        private System.Windows.Forms.Label ls_amount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_gst;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn BILLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUSTOMERNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn TAXAMOUNT;
        private System.Windows.Forms.DataGridViewTextBoxColumn TAX;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn MODE;
    }
}