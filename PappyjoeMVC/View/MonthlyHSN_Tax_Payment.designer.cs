
namespace PappyjoeMVC.View
{
    partial class MonthlyHSN_Tax_Payment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonthlyHSN_Tax_Payment));
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.rad_pur = new System.Windows.Forms.RadioButton();
            this.rad_sales = new System.Windows.Forms.RadioButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btn_show = new System.Windows.Forms.Button();
            this.btnEXPORT = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.dgvhsn = new System.Windows.Forms.DataGridView();
            this.colhsn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coltaxamount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colrate5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colrate12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colrate18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colrate28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coltotalamount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lab_Msg = new System.Windows.Forms.Label();
            this.txt_gst = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvhsn)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(399, 40);
            this.label1.TabIndex = 231;
            this.label1.Text = "Monthly HSN Wise Tax Report";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Gainsboro;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(16, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 17);
            this.label3.TabIndex = 236;
            this.label3.Text = "From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(276, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 17);
            this.label2.TabIndex = 237;
            this.label2.Text = "To";
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Location = new System.Drawing.Point(61, 59);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerFrom.TabIndex = 238;
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Location = new System.Drawing.Point(300, 59);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerTo.TabIndex = 239;
            // 
            // rad_pur
            // 
            this.rad_pur.AutoSize = true;
            this.rad_pur.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.rad_pur.Location = new System.Drawing.Point(655, 63);
            this.rad_pur.Name = "rad_pur";
            this.rad_pur.Size = new System.Drawing.Size(70, 17);
            this.rad_pur.TabIndex = 240;
            this.rad_pur.Text = "Purchase";
            this.rad_pur.UseVisualStyleBackColor = true;
            this.rad_pur.Click += new System.EventHandler(this.rad_pur_Click);
            // 
            // rad_sales
            // 
            this.rad_sales.AutoSize = true;
            this.rad_sales.Checked = true;
            this.rad_sales.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.rad_sales.Location = new System.Drawing.Point(728, 63);
            this.rad_sales.Name = "rad_sales";
            this.rad_sales.Size = new System.Drawing.Size(51, 17);
            this.rad_sales.TabIndex = 241;
            this.rad_sales.TabStop = true;
            this.rad_sales.Text = "Sales";
            this.rad_sales.UseVisualStyleBackColor = true;
            this.rad_sales.CheckedChanged += new System.EventHandler(this.rad_sales_CheckedChanged);
            this.rad_sales.Click += new System.EventHandler(this.rad_sales_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btn_show);
            this.panel5.Controls.Add(this.btnEXPORT);
            this.panel5.Controls.Add(this.btnprint);
            this.panel5.Controls.Add(this.buttonClose);
            this.panel5.Location = new System.Drawing.Point(790, 44);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(410, 36);
            this.panel5.TabIndex = 242;
            // 
            // btn_show
            // 
            this.btn_show.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_show.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_show.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_show.Location = new System.Drawing.Point(3, 2);
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
            this.btnEXPORT.Location = new System.Drawing.Point(201, 2);
            this.btnEXPORT.Name = "btnEXPORT";
            this.btnEXPORT.Size = new System.Drawing.Size(100, 32);
            this.btnEXPORT.TabIndex = 213;
            this.btnEXPORT.Text = "Export to Excel";
            this.btnEXPORT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEXPORT.UseVisualStyleBackColor = false;
            this.btnEXPORT.Click += new System.EventHandler(this.btnEXPORT_Click);
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Image = ((System.Drawing.Image)(resources.GetObject("btnprint.Image")));
            this.btnprint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnprint.Location = new System.Drawing.Point(102, 2);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(100, 32);
            this.btnprint.TabIndex = 7;
            this.btnprint.Text = "Print";
            this.btnprint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.Color.Tomato;
            this.buttonClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonClose.Location = new System.Drawing.Point(300, 2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(100, 32);
            this.buttonClose.TabIndex = 212;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // dgvhsn
            // 
            this.dgvhsn.AllowUserToAddRows = false;
            this.dgvhsn.BackgroundColor = System.Drawing.Color.White;
            this.dgvhsn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvhsn.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvhsn.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvhsn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvhsn.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colhsn,
            this.colqty,
            this.coltaxamount,
            this.colrate5,
            this.colrate12,
            this.colrate18,
            this.colrate28,
            this.coltotalamount});
            this.dgvhsn.EnableHeadersVisualStyles = false;
            this.dgvhsn.Location = new System.Drawing.Point(3, 86);
            this.dgvhsn.Name = "dgvhsn";
            this.dgvhsn.RowHeadersVisible = false;
            this.dgvhsn.Size = new System.Drawing.Size(1230, 457);
            this.dgvhsn.TabIndex = 243;
            // 
            // colhsn
            // 
            this.colhsn.HeaderText = "HSN CODE";
            this.colhsn.Name = "colhsn";
            this.colhsn.Width = 110;
            // 
            // colqty
            // 
            this.colqty.HeaderText = "QUANTITY";
            this.colqty.Name = "colqty";
            this.colqty.Visible = false;
            // 
            // coltaxamount
            // 
            this.coltaxamount.HeaderText = "NON-TAXABLE AMOUNT";
            this.coltaxamount.Name = "coltaxamount";
            this.coltaxamount.Width = 170;
            // 
            // colrate5
            // 
            this.colrate5.HeaderText = "TAXABLE AMOUNT(5%)";
            this.colrate5.Name = "colrate5";
            this.colrate5.Width = 150;
            // 
            // colrate12
            // 
            this.colrate12.HeaderText = "TAXABLE AMOUNT(12%)";
            this.colrate12.Name = "colrate12";
            this.colrate12.Width = 170;
            // 
            // colrate18
            // 
            this.colrate18.HeaderText = "TAXABLE AMOUNT(18%)";
            this.colrate18.Name = "colrate18";
            this.colrate18.Width = 170;
            // 
            // colrate28
            // 
            this.colrate28.HeaderText = "TAXABLE AMOUNT(28%)";
            this.colrate28.Name = "colrate28";
            this.colrate28.Width = 170;
            // 
            // coltotalamount
            // 
            this.coltotalamount.HeaderText = "TOTAL AMOUNT";
            this.coltotalamount.Name = "coltotalamount";
            this.coltotalamount.Width = 150;
            // 
            // Lab_Msg
            // 
            this.Lab_Msg.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Lab_Msg.BackColor = System.Drawing.Color.Wheat;
            this.Lab_Msg.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_Msg.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.Lab_Msg.Location = new System.Drawing.Point(338, 317);
            this.Lab_Msg.Name = "Lab_Msg";
            this.Lab_Msg.Size = new System.Drawing.Size(567, 25);
            this.Lab_Msg.TabIndex = 279;
            this.Lab_Msg.Text = "No Records Found. Please change the date and then try again!..";
            this.Lab_Msg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_gst
            // 
            this.txt_gst.Enabled = false;
            this.txt_gst.Location = new System.Drawing.Point(596, 59);
            this.txt_gst.Name = "txt_gst";
            this.txt_gst.Size = new System.Drawing.Size(41, 20);
            this.txt_gst.TabIndex = 280;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(541, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 281;
            this.label4.Text = "GST NO";
            // 
            // MonthlyHSN_Tax_Payment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1245, 562);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_gst);
            this.Controls.Add(this.Lab_Msg);
            this.Controls.Add(this.dgvhsn);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.rad_sales);
            this.Controls.Add(this.rad_pur);
            this.Controls.Add(this.dateTimePickerTo);
            this.Controls.Add(this.dateTimePickerFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "MonthlyHSN_Tax_Payment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HSN wise tax payment";
            this.Load += new System.EventHandler(this.MonthlyHSN_Tax_Payment_Load);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvhsn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.RadioButton rad_pur;
        private System.Windows.Forms.RadioButton rad_sales;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btn_show;
        private System.Windows.Forms.Button btnEXPORT;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.DataGridView dgvhsn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colhsn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn coltaxamount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colrate5;
        private System.Windows.Forms.DataGridViewTextBoxColumn colrate12;
        private System.Windows.Forms.DataGridViewTextBoxColumn colrate18;
        private System.Windows.Forms.DataGridViewTextBoxColumn colrate28;
        private System.Windows.Forms.DataGridViewTextBoxColumn coltotalamount;
        private System.Windows.Forms.Label Lab_Msg;
        private System.Windows.Forms.TextBox txt_gst;
        private System.Windows.Forms.Label label4;
    }
}