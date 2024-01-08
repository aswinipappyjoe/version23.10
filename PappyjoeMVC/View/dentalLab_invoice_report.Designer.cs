
namespace PappyjoeMVC.View
{
    partial class dentalLab_invoice_report
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dentalLab_invoice_report));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnEXPORT = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btn_show = new System.Windows.Forms.Button();
            this.dateTimePickerdaily1 = new System.Windows.Forms.DateTimePicker();
            this.combodoctors = new System.Windows.Forms.ComboBox();
            this.Grvsummary = new System.Windows.Forms.DataGridView();
            this.SL_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PATIENT_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INVOICE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RECEIPT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRODUCT_AND_SERVICE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Doctor_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label37 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grvsummary)).BeginInit();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.btnEXPORT);
            this.panel5.Controls.Add(this.btnprint);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.buttonClose);
            this.panel5.Location = new System.Drawing.Point(0, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1287, 55);
            this.panel5.TabIndex = 221;
            // 
            // btnEXPORT
            // 
            this.btnEXPORT.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnEXPORT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEXPORT.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEXPORT.ForeColor = System.Drawing.Color.White;
            this.btnEXPORT.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEXPORT.Location = new System.Drawing.Point(1142, 7);
            this.btnEXPORT.Name = "btnEXPORT";
            this.btnEXPORT.Size = new System.Drawing.Size(100, 32);
            this.btnEXPORT.TabIndex = 213;
            this.btnEXPORT.Text = "Export to Excel";
            this.btnEXPORT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEXPORT.UseVisualStyleBackColor = false;
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Image = ((System.Drawing.Image)(resources.GetObject("btnprint.Image")));
            this.btnprint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnprint.Location = new System.Drawing.Point(1043, 7);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(100, 32);
            this.btnprint.TabIndex = 7;
            this.btnprint.Text = "Print";
            this.btnprint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnprint.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(5, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(345, 40);
            this.label1.TabIndex = 219;
            this.label1.Text = "Dental Lab Invoice Report";
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.Color.Tomato;
            this.buttonClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonClose.Location = new System.Drawing.Point(1241, 7);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(100, 32);
            this.buttonClose.TabIndex = 212;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.label37);
            this.panel6.Controls.Add(this.dateTimePicker1);
            this.panel6.Controls.Add(this.btn_show);
            this.panel6.Controls.Add(this.dateTimePickerdaily1);
            this.panel6.Controls.Add(this.combodoctors);
            this.panel6.Location = new System.Drawing.Point(1, 58);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1190, 50);
            this.panel6.TabIndex = 222;
            // 
            // btn_show
            // 
            this.btn_show.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_show.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_show.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_show.Location = new System.Drawing.Point(808, 12);
            this.btn_show.Name = "btn_show";
            this.btn_show.Size = new System.Drawing.Size(81, 28);
            this.btn_show.TabIndex = 221;
            this.btn_show.Text = "Show";
            this.btn_show.UseVisualStyleBackColor = false;
            // 
            // dateTimePickerdaily1
            // 
            this.dateTimePickerdaily1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerdaily1.Location = new System.Drawing.Point(50, 16);
            this.dateTimePickerdaily1.Name = "dateTimePickerdaily1";
            this.dateTimePickerdaily1.Size = new System.Drawing.Size(223, 22);
            this.dateTimePickerdaily1.TabIndex = 2;
            // 
            // combodoctors
            // 
            this.combodoctors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combodoctors.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.combodoctors.FormattingEnabled = true;
            this.combodoctors.Location = new System.Drawing.Point(609, 15);
            this.combodoctors.Name = "combodoctors";
            this.combodoctors.Size = new System.Drawing.Size(162, 21);
            this.combodoctors.TabIndex = 1;
            // 
            // Grvsummary
            // 
            this.Grvsummary.AllowUserToAddRows = false;
            this.Grvsummary.AllowUserToDeleteRows = false;
            this.Grvsummary.AllowUserToResizeColumns = false;
            this.Grvsummary.AllowUserToResizeRows = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.Bisque;
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.Black;
            this.Grvsummary.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.Grvsummary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Grvsummary.BackgroundColor = System.Drawing.Color.White;
            this.Grvsummary.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grvsummary.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.Grvsummary.ColumnHeadersHeight = 28;
            this.Grvsummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Grvsummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SL_NO,
            this.PATIENT_NAME,
            this.INVOICE,
            this.RECEIPT,
            this.PRODUCT_AND_SERVICE,
            this.cost,
            this.paid,
            this.Doctor_name});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.Bisque;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Grvsummary.DefaultCellStyle = dataGridViewCellStyle15;
            this.Grvsummary.Location = new System.Drawing.Point(0, 111);
            this.Grvsummary.Name = "Grvsummary";
            this.Grvsummary.ReadOnly = true;
            this.Grvsummary.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.Grvsummary.RowHeadersVisible = false;
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.Black;
            this.Grvsummary.RowsDefaultCellStyle = dataGridViewCellStyle16;
            this.Grvsummary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Grvsummary.Size = new System.Drawing.Size(1287, 468);
            this.Grvsummary.TabIndex = 223;
            // 
            // SL_NO
            // 
            this.SL_NO.HeaderText = "SL.";
            this.SL_NO.Name = "SL_NO";
            this.SL_NO.ReadOnly = true;
            // 
            // PATIENT_NAME
            // 
            this.PATIENT_NAME.DataPropertyName = "pt_name";
            this.PATIENT_NAME.HeaderText = "PATIENT NAME";
            this.PATIENT_NAME.Name = "PATIENT_NAME";
            this.PATIENT_NAME.ReadOnly = true;
            this.PATIENT_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // INVOICE
            // 
            this.INVOICE.DataPropertyName = "invoice_no";
            this.INVOICE.HeaderText = "INVOICE NO";
            this.INVOICE.Name = "INVOICE";
            this.INVOICE.ReadOnly = true;
            this.INVOICE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // RECEIPT
            // 
            this.RECEIPT.DataPropertyName = "receipt_no";
            this.RECEIPT.HeaderText = "RECEIPT NO";
            this.RECEIPT.Name = "RECEIPT";
            this.RECEIPT.ReadOnly = true;
            this.RECEIPT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RECEIPT.Visible = false;
            // 
            // PRODUCT_AND_SERVICE
            // 
            this.PRODUCT_AND_SERVICE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PRODUCT_AND_SERVICE.DataPropertyName = "services";
            this.PRODUCT_AND_SERVICE.HeaderText = "PRODUCT AND SERVICES";
            this.PRODUCT_AND_SERVICE.Name = "PRODUCT_AND_SERVICE";
            this.PRODUCT_AND_SERVICE.ReadOnly = true;
            this.PRODUCT_AND_SERVICE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PRODUCT_AND_SERVICE.Width = 150;
            // 
            // cost
            // 
            this.cost.HeaderText = "COST";
            this.cost.Name = "cost";
            this.cost.ReadOnly = true;
            // 
            // paid
            // 
            this.paid.HeaderText = "AMOUNT PAID";
            this.paid.Name = "paid";
            this.paid.ReadOnly = true;
            this.paid.Visible = false;
            // 
            // Doctor_name
            // 
            this.Doctor_name.DataPropertyName = "doctor_name";
            this.Doctor_name.HeaderText = "DOCTOR";
            this.Doctor_name.Name = "Doctor_name";
            this.Doctor_name.ReadOnly = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(310, 17);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(223, 22);
            this.dateTimePicker1.TabIndex = 222;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label37.Location = new System.Drawing.Point(6, 17);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(38, 17);
            this.label37.TabIndex = 289;
            this.label37.Text = "From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(288, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 17);
            this.label2.TabIndex = 290;
            this.label2.Text = "To";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(562, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 291;
            this.label3.Text = "Lab Name";
            // 
            // dentalLab_invoice_report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 582);
            this.Controls.Add(this.Grvsummary);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Name = "dentalLab_invoice_report";
            this.Text = "dentalLab_invoice_report";
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grvsummary)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnEXPORT;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btn_show;
        private System.Windows.Forms.DateTimePicker dateTimePickerdaily1;
        private System.Windows.Forms.ComboBox combodoctors;
        private System.Windows.Forms.DataGridView Grvsummary;
        private System.Windows.Forms.DataGridViewTextBoxColumn SL_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PATIENT_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn INVOICE;
        private System.Windows.Forms.DataGridViewTextBoxColumn RECEIPT;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRODUCT_AND_SERVICE;
        private System.Windows.Forms.DataGridViewTextBoxColumn cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn paid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Doctor_name;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label37;
    }
}