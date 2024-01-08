namespace PappyjoeMVC.View
{
    partial class Monthly_gst_report
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Monthly_gst_report));
            this.panel1 = new System.Windows.Forms.Panel();
            this.Lab_Msg = new System.Windows.Forms.Label();
            this.Grvsummary = new System.Windows.Forms.DataGridView();
            this.btn_show = new System.Windows.Forms.Button();
            this.btnEXPORT = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerdaily1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtp1ReceptReceivedPerMonth1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.rad_sales = new System.Windows.Forms.RadioButton();
            this.rad_pur = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txt_gst = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ls_gst = new System.Windows.Forms.Label();
            this.ls_amount = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grvsummary)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.panel1.Location = new System.Drawing.Point(2, 94);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1366, 592);
            this.panel1.TabIndex = 232;
            // 
            // Lab_Msg
            // 
            this.Lab_Msg.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Lab_Msg.BackColor = System.Drawing.Color.Wheat;
            this.Lab_Msg.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_Msg.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.Lab_Msg.Location = new System.Drawing.Point(247, 251);
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
            this.Grvsummary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
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
            this.Grvsummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grvsummary.Location = new System.Drawing.Point(0, 0);
            this.Grvsummary.Name = "Grvsummary";
            this.Grvsummary.ReadOnly = true;
            this.Grvsummary.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.Grvsummary.RowHeadersVisible = false;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.Grvsummary.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.Grvsummary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Grvsummary.Size = new System.Drawing.Size(1366, 592);
            this.Grvsummary.TabIndex = 222;
            this.Grvsummary.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grvsummary_CellContentClick);
            // 
            // btn_show
            // 
            this.btn_show.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_show.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_show.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_show.Location = new System.Drawing.Point(677, 4);
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
            this.btnEXPORT.Location = new System.Drawing.Point(1155, 3);
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
            this.buttonClose.Location = new System.Drawing.Point(1254, 3);
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
            this.btnprint.Location = new System.Drawing.Point(1056, 3);
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
            this.label1.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(282, 40);
            this.label1.TabIndex = 230;
            this.label1.Text = "Monthly GST  Report";
            // 
            // dateTimePickerdaily1
            // 
            this.dateTimePickerdaily1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerdaily1.Location = new System.Drawing.Point(293, 4);
            this.dateTimePickerdaily1.Name = "dateTimePickerdaily1";
            this.dateTimePickerdaily1.Size = new System.Drawing.Size(223, 22);
            this.dateTimePickerdaily1.TabIndex = 233;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(265, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 17);
            this.label2.TabIndex = 236;
            this.label2.Text = "To";
            // 
            // dtp1ReceptReceivedPerMonth1
            // 
            this.dtp1ReceptReceivedPerMonth1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp1ReceptReceivedPerMonth1.Location = new System.Drawing.Point(52, 4);
            this.dtp1ReceptReceivedPerMonth1.Name = "dtp1ReceptReceivedPerMonth1";
            this.dtp1ReceptReceivedPerMonth1.Size = new System.Drawing.Size(206, 22);
            this.dtp1ReceptReceivedPerMonth1.TabIndex = 234;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(5, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 17);
            this.label3.TabIndex = 235;
            this.label3.Text = "From";
            // 
            // rad_sales
            // 
            this.rad_sales.AutoSize = true;
            this.rad_sales.Checked = true;
            this.rad_sales.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.rad_sales.Location = new System.Drawing.Point(620, 9);
            this.rad_sales.Name = "rad_sales";
            this.rad_sales.Size = new System.Drawing.Size(51, 17);
            this.rad_sales.TabIndex = 238;
            this.rad_sales.TabStop = true;
            this.rad_sales.Text = "Sales";
            this.rad_sales.UseVisualStyleBackColor = true;
            this.rad_sales.Click += new System.EventHandler(this.rad_sales_Click);
            // 
            // rad_pur
            // 
            this.rad_pur.AutoSize = true;
            this.rad_pur.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.rad_pur.Location = new System.Drawing.Point(544, 9);
            this.rad_pur.Name = "rad_pur";
            this.rad_pur.Size = new System.Drawing.Size(70, 17);
            this.rad_pur.TabIndex = 237;
            this.rad_pur.Text = "Purchase";
            this.rad_pur.UseVisualStyleBackColor = true;
            this.rad_pur.CheckedChanged += new System.EventHandler(this.rad_pur_CheckedChanged);
            this.rad_pur.Click += new System.EventHandler(this.rad_pur_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btnEXPORT);
            this.panel3.Controls.Add(this.buttonClose);
            this.panel3.Controls.Add(this.btnprint);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(1, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1365, 54);
            this.panel3.TabIndex = 239;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txt_gst);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.btn_show);
            this.panel2.Controls.Add(this.rad_sales);
            this.panel2.Controls.Add(this.rad_pur);
            this.panel2.Controls.Add(this.dtp1ReceptReceivedPerMonth1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.dateTimePickerdaily1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(3, 56);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1363, 37);
            this.panel2.TabIndex = 222;
            // 
            // txt_gst
            // 
            this.txt_gst.Location = new System.Drawing.Point(825, 7);
            this.txt_gst.Name = "txt_gst";
            this.txt_gst.ReadOnly = true;
            this.txt_gst.Size = new System.Drawing.Size(164, 20);
            this.txt_gst.TabIndex = 240;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(770, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 17);
            this.label8.TabIndex = 239;
            this.label8.Text = "GST IN";
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BackColor = System.Drawing.Color.Gainsboro;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.label6);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Controls.Add(this.ls_gst);
            this.panel6.Controls.Add(this.ls_amount);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Location = new System.Drawing.Point(3, 687);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1368, 49);
            this.panel6.TabIndex = 279;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Bisque;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkGreen;
            this.label6.Location = new System.Drawing.Point(1276, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 20);
            this.label6.TabIndex = 18;
            this.label6.Text = "0.00";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Gainsboro;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkGreen;
            this.label4.Location = new System.Drawing.Point(627, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 20);
            this.label4.TabIndex = 16;
            this.label4.Text = "0.00";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Gainsboro;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkGreen;
            this.label7.Location = new System.Drawing.Point(1119, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 20);
            this.label7.TabIndex = 17;
            this.label7.Text = "0.00";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Gainsboro;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkGreen;
            this.label5.Location = new System.Drawing.Point(431, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "0.00";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ls_gst
            // 
            this.ls_gst.AutoSize = true;
            this.ls_gst.BackColor = System.Drawing.Color.Gainsboro;
            this.ls_gst.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ls_gst.ForeColor = System.Drawing.Color.DarkGreen;
            this.ls_gst.Location = new System.Drawing.Point(952, 12);
            this.ls_gst.Name = "ls_gst";
            this.ls_gst.Size = new System.Drawing.Size(37, 20);
            this.ls_gst.TabIndex = 14;
            this.ls_gst.Text = "0.00";
            this.ls_gst.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ls_amount
            // 
            this.ls_amount.AutoSize = true;
            this.ls_amount.BackColor = System.Drawing.Color.Gainsboro;
            this.ls_amount.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ls_amount.ForeColor = System.Drawing.Color.DarkGreen;
            this.ls_amount.Location = new System.Drawing.Point(790, 12);
            this.ls_amount.Name = "ls_amount";
            this.ls_amount.Size = new System.Drawing.Size(37, 20);
            this.ls_amount.TabIndex = 9;
            this.ls_amount.Text = "0.00";
            this.ls_amount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Gainsboro;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label9.Location = new System.Drawing.Point(190, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 21);
            this.label9.TabIndex = 7;
            this.label9.Text = "TOTAL(INR):";
            // 
            // Monthly_gst_report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1370, 741);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Monthly_gst_report";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Monthly GST Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Monthly_gst_report_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grvsummary)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView Grvsummary;
        private System.Windows.Forms.Button btn_show;
        private System.Windows.Forms.Button btnEXPORT;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerdaily1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtp1ReceptReceivedPerMonth1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Lab_Msg;
        private System.Windows.Forms.RadioButton rad_sales;
        private System.Windows.Forms.RadioButton rad_pur;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label ls_gst;
        private System.Windows.Forms.Label ls_amount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_gst;
        private System.Windows.Forms.Label label8;
    }
}