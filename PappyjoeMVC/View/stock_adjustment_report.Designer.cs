namespace PappyjoeMVC.View
{
    partial class stock_adjustment_report
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(stock_adjustment_report));
            this.panel1 = new System.Windows.Forms.Panel();
            this.Lab_Msg = new System.Windows.Forms.Label();
            this.Grvsummary = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.batch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.action = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updatedqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.previous_stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.current_stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gst = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_show = new System.Windows.Forms.Button();
            this.btnEXPORT = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnprint = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtp1ReceptReceivedPerMonth1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerdaily1 = new System.Windows.Forms.DateTimePicker();
            this.cmb_action = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.Lab_totalExpense = new System.Windows.Forms.Label();
            this.Lab_TotalIncome = new System.Windows.Forms.Label();
            this.Lab_AmountPaid = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grvsummary)).BeginInit();
            this.panel5.SuspendLayout();
            this.label46.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.Lab_Msg);
            this.panel1.Controls.Add(this.Grvsummary);
            this.panel1.Location = new System.Drawing.Point(2, 103);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1354, 575);
            this.panel1.TabIndex = 246;
            // 
            // Lab_Msg
            // 
            this.Lab_Msg.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Lab_Msg.BackColor = System.Drawing.Color.Wheat;
            this.Lab_Msg.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_Msg.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.Lab_Msg.Location = new System.Drawing.Point(241, 242);
            this.Lab_Msg.Name = "Lab_Msg";
            this.Lab_Msg.Size = new System.Drawing.Size(561, 25);
            this.Lab_Msg.TabIndex = 278;
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
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.Grvsummary.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Grvsummary.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grvsummary.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Grvsummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grvsummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.date,
            this.itemcode,
            this.itemname,
            this.batch,
            this.action,
            this.updatedqty,
            this.previous_stock,
            this.current_stock,
            this.unit,
            this.rate,
            this.gst,
            this.Total});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Grvsummary.DefaultCellStyle = dataGridViewCellStyle5;
            this.Grvsummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grvsummary.Location = new System.Drawing.Point(0, 0);
            this.Grvsummary.Name = "Grvsummary";
            this.Grvsummary.ReadOnly = true;
            this.Grvsummary.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.Grvsummary.RowHeadersVisible = false;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            this.Grvsummary.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.Grvsummary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Grvsummary.Size = new System.Drawing.Size(1354, 575);
            this.Grvsummary.TabIndex = 222;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            this.id.Width = 30;
            // 
            // date
            // 
            this.date.FillWeight = 88.8231F;
            this.date.HeaderText = "DATE";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            this.date.Width = 65;
            // 
            // itemcode
            // 
            this.itemcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.itemcode.FillWeight = 110.4362F;
            this.itemcode.HeaderText = "ITEM CODE";
            this.itemcode.Name = "itemcode";
            this.itemcode.ReadOnly = true;
            this.itemcode.Width = 127;
            // 
            // itemname
            // 
            this.itemname.FillWeight = 187.3262F;
            this.itemname.HeaderText = "ITEM NAME";
            this.itemname.Name = "itemname";
            this.itemname.ReadOnly = true;
            this.itemname.Width = 230;
            // 
            // batch
            // 
            this.batch.FillWeight = 65.45151F;
            this.batch.HeaderText = "BATCH";
            this.batch.Name = "batch";
            this.batch.ReadOnly = true;
            this.batch.Width = 74;
            // 
            // action
            // 
            this.action.FillWeight = 130.66F;
            this.action.HeaderText = "ACTION";
            this.action.Name = "action";
            this.action.ReadOnly = true;
            this.action.Width = 180;
            // 
            // updatedqty
            // 
            this.updatedqty.FillWeight = 57.54324F;
            this.updatedqty.HeaderText = "UPDATED QTY";
            this.updatedqty.Name = "updatedqty";
            this.updatedqty.ReadOnly = true;
            this.updatedqty.Width = 90;
            // 
            // previous_stock
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.previous_stock.DefaultCellStyle = dataGridViewCellStyle3;
            this.previous_stock.FillWeight = 63.42777F;
            this.previous_stock.HeaderText = "PREVIOUS STOCK";
            this.previous_stock.Name = "previous_stock";
            this.previous_stock.ReadOnly = true;
            this.previous_stock.Width = 120;
            // 
            // current_stock
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.current_stock.DefaultCellStyle = dataGridViewCellStyle4;
            this.current_stock.FillWeight = 83.08049F;
            this.current_stock.HeaderText = "CURRENT STOCK";
            this.current_stock.Name = "current_stock";
            this.current_stock.ReadOnly = true;
            this.current_stock.Width = 120;
            // 
            // unit
            // 
            this.unit.FillWeight = 119.1646F;
            this.unit.HeaderText = "UNIT";
            this.unit.Name = "unit";
            this.unit.ReadOnly = true;
            this.unit.Width = 65;
            // 
            // rate
            // 
            this.rate.FillWeight = 108.9576F;
            this.rate.HeaderText = "RATE";
            this.rate.Name = "rate";
            this.rate.ReadOnly = true;
            this.rate.Width = 123;
            // 
            // gst
            // 
            this.gst.FillWeight = 57.81705F;
            this.gst.HeaderText = "GST(%)";
            this.gst.Name = "gst";
            this.gst.ReadOnly = true;
            this.gst.Width = 40;
            // 
            // Total
            // 
            this.Total.FillWeight = 127.3123F;
            this.Total.HeaderText = "TOTAL AMOUNT";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            this.Total.Width = 110;
            // 
            // btn_show
            // 
            this.btn_show.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_show.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_show.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_show.Location = new System.Drawing.Point(3, 2);
            this.btn_show.Name = "btn_show";
            this.btn_show.Size = new System.Drawing.Size(79, 32);
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
            this.btnEXPORT.Location = new System.Drawing.Point(194, 0);
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
            this.buttonClose.Location = new System.Drawing.Point(298, 0);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(100, 32);
            this.buttonClose.TabIndex = 212;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btn_show);
            this.panel5.Controls.Add(this.btnEXPORT);
            this.panel5.Controls.Add(this.buttonClose);
            this.panel5.Controls.Add(this.btnprint);
            this.panel5.Location = new System.Drawing.Point(820, 52);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(536, 36);
            this.panel5.TabIndex = 245;
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Image = ((System.Drawing.Image)(resources.GetObject("btnprint.Image")));
            this.btnprint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnprint.Location = new System.Drawing.Point(88, 1);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(100, 32);
            this.btnprint.TabIndex = 7;
            this.btnprint.Text = "Print";
            this.btnprint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(285, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 17);
            this.label2.TabIndex = 250;
            this.label2.Text = "To";
            // 
            // dtp1ReceptReceivedPerMonth1
            // 
            this.dtp1ReceptReceivedPerMonth1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp1ReceptReceivedPerMonth1.Location = new System.Drawing.Point(72, 65);
            this.dtp1ReceptReceivedPerMonth1.Name = "dtp1ReceptReceivedPerMonth1";
            this.dtp1ReceptReceivedPerMonth1.Size = new System.Drawing.Size(206, 22);
            this.dtp1ReceptReceivedPerMonth1.TabIndex = 248;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(25, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 17);
            this.label3.TabIndex = 249;
            this.label3.Text = "From";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(341, 40);
            this.label1.TabIndex = 244;
            this.label1.Text = "Stock Adjustment  Report";
            // 
            // dateTimePickerdaily1
            // 
            this.dateTimePickerdaily1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerdaily1.Location = new System.Drawing.Point(313, 65);
            this.dateTimePickerdaily1.Name = "dateTimePickerdaily1";
            this.dateTimePickerdaily1.Size = new System.Drawing.Size(223, 22);
            this.dateTimePickerdaily1.TabIndex = 247;
            // 
            // cmb_action
            // 
            this.cmb_action.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.cmb_action.FormattingEnabled = true;
            this.cmb_action.Items.AddRange(new object[] {
            "All Report",
            "Adjust to Opening Stock Excess or Shortage",
            "Adjust to Damage",
            "Adjust to Shortage",
            "Adjust to Excess"});
            this.cmb_action.Location = new System.Drawing.Point(595, 61);
            this.cmb_action.Name = "cmb_action";
            this.cmb_action.Size = new System.Drawing.Size(219, 21);
            this.cmb_action.TabIndex = 1007;
            this.cmb_action.SelectedIndexChanged += new System.EventHandler(this.cmb_action_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(548, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 1006;
            this.label4.Text = "Action";
            // 
            // label46
            // 
            this.label46.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label46.BackColor = System.Drawing.Color.Gainsboro;
            this.label46.Controls.Add(this.label5);
            this.label46.Controls.Add(this.Lab_totalExpense);
            this.label46.Controls.Add(this.Lab_TotalIncome);
            this.label46.Controls.Add(this.Lab_AmountPaid);
            this.label46.Location = new System.Drawing.Point(17, 684);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(1337, 53);
            this.label46.TabIndex = 1008;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Gainsboro;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.ForestGreen;
            this.label5.Location = new System.Drawing.Point(1253, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 17);
            this.label5.TabIndex = 43;
            this.label5.Text = "0.00";
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
            // stock_adjustment_report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1355, 741);
            this.Controls.Add(this.label46);
            this.Controls.Add(this.cmb_action);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtp1ReceptReceivedPerMonth1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePickerdaily1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "stock_adjustment_report";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Adjustment Report";
            this.Load += new System.EventHandler(this.stock_adjustment_report_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grvsummary)).EndInit();
            this.panel5.ResumeLayout(false);
            this.label46.ResumeLayout(false);
            this.label46.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Lab_Msg;
        private System.Windows.Forms.DataGridView Grvsummary;
        private System.Windows.Forms.Button btn_show;
        private System.Windows.Forms.Button btnEXPORT;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtp1ReceptReceivedPerMonth1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerdaily1;
        private System.Windows.Forms.ComboBox cmb_action;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel label46;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label Lab_totalExpense;
        private System.Windows.Forms.Label Lab_TotalIncome;
        private System.Windows.Forms.Label Lab_AmountPaid;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemname;
        private System.Windows.Forms.DataGridViewTextBoxColumn batch;
        private System.Windows.Forms.DataGridViewTextBoxColumn action;
        private System.Windows.Forms.DataGridViewTextBoxColumn updatedqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn previous_stock;
        private System.Windows.Forms.DataGridViewTextBoxColumn current_stock;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn gst;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
    }
}