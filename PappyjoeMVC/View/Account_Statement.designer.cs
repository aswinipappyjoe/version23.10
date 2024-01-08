
namespace PappyjoeMVC.View
{
    partial class Account_Statement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Account_Statement));
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv_Stock = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.voucher = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.purno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.return_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountpaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.advance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Due = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pur_return = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_show = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.btnReport = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtp_from = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.dtp_to = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Stock)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dgv_Stock);
            this.panel1.Location = new System.Drawing.Point(1, 91);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1155, 490);
            this.panel1.TabIndex = 0;
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
            this.purno,
            this.return_no,
            this.supplier,
            this.amount,
            this.amountpaid,
            this.advance,
            this.Due,
            this.pur_return});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Stock.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_Stock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Stock.Location = new System.Drawing.Point(0, 0);
            this.dgv_Stock.Name = "dgv_Stock";
            this.dgv_Stock.ReadOnly = true;
            this.dgv_Stock.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Stock.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_Stock.RowHeadersVisible = false;
            this.dgv_Stock.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_Stock.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_Stock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv_Stock.Size = new System.Drawing.Size(1155, 490);
            this.dgv_Stock.TabIndex = 281;
            this.dgv_Stock.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Stock_CellClick);
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            this.id.Width = 50;
            // 
            // date
            // 
            this.date.FillWeight = 60F;
            this.date.HeaderText = "Date";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            // 
            // voucher
            // 
            this.voucher.FillWeight = 120F;
            this.voucher.HeaderText = "Voucher No";
            this.voucher.Name = "voucher";
            this.voucher.ReadOnly = true;
            this.voucher.Width = 80;
            // 
            // purno
            // 
            this.purno.FillWeight = 40F;
            this.purno.HeaderText = "Purchase No";
            this.purno.Name = "purno";
            this.purno.ReadOnly = true;
            this.purno.Width = 80;
            // 
            // return_no
            // 
            this.return_no.HeaderText = "Return No";
            this.return_no.Name = "return_no";
            this.return_no.ReadOnly = true;
            // 
            // supplier
            // 
            this.supplier.HeaderText = "Supplier";
            this.supplier.Name = "supplier";
            this.supplier.ReadOnly = true;
            this.supplier.Width = 260;
            // 
            // amount
            // 
            this.amount.HeaderText = "Total Amount";
            this.amount.Name = "amount";
            this.amount.ReadOnly = true;
            this.amount.Width = 110;
            // 
            // amountpaid
            // 
            this.amountpaid.FillWeight = 50F;
            this.amountpaid.HeaderText = "Amount Paid";
            this.amountpaid.Name = "amountpaid";
            this.amountpaid.ReadOnly = true;
            this.amountpaid.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.amountpaid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.amountpaid.Width = 130;
            // 
            // advance
            // 
            this.advance.HeaderText = "Advance";
            this.advance.Name = "advance";
            this.advance.ReadOnly = true;
            this.advance.Width = 90;
            // 
            // Due
            // 
            this.Due.FillWeight = 50F;
            this.Due.HeaderText = "Due";
            this.Due.Name = "Due";
            this.Due.ReadOnly = true;
            this.Due.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Due.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // pur_return
            // 
            this.pur_return.HeaderText = "Return Amount";
            this.pur_return.Name = "pur_return";
            this.pur_return.ReadOnly = true;
            this.pur_return.Width = 110;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btn_show);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cmbUnit);
            this.panel2.Controls.Add(this.btnReport);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.dtp_from);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.dtp_to);
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1155, 88);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // btn_show
            // 
            this.btn_show.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_show.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_show.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_show.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_show.ForeColor = System.Drawing.Color.White;
            this.btn_show.Location = new System.Drawing.Point(1048, 56);
            this.btn_show.Name = "btn_show";
            this.btn_show.Size = new System.Drawing.Size(78, 25);
            this.btn_show.TabIndex = 312;
            this.btn_show.Text = "SHOW";
            this.btn_show.UseVisualStyleBackColor = false;
            this.btn_show.Click += new System.EventHandler(this.btn_show_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(577, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 17);
            this.label2.TabIndex = 311;
            this.label2.Text = "Supplier :";
            // 
            // cmbUnit
            // 
            this.cmbUnit.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(646, 56);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(227, 21);
            this.cmbUnit.TabIndex = 310;
            this.cmbUnit.SelectedIndexChanged += new System.EventHandler(this.cmbUnit_SelectedIndexChanged);
            // 
            // btnReport
            // 
            this.btnReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReport.BackColor = System.Drawing.Color.LimeGreen;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.White;
            this.btnReport.Location = new System.Drawing.Point(1048, 11);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(78, 25);
            this.btnReport.TabIndex = 309;
            this.btnReport.Text = "PRINT";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(11, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(177, 21);
            this.label8.TabIndex = 308;
            this.label8.Text = "ACCOUNT STATEMENT";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(11, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 307;
            this.label1.Text = "From Date :";
            // 
            // dtp_from
            // 
            this.dtp_from.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_from.Location = new System.Drawing.Point(91, 56);
            this.dtp_from.Name = "dtp_from";
            this.dtp_from.Size = new System.Drawing.Size(196, 20);
            this.dtp_from.TabIndex = 306;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label12.Location = new System.Drawing.Point(299, 56);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 17);
            this.label12.TabIndex = 305;
            this.label12.Text = "To  Date :";
            // 
            // dtp_to
            // 
            this.dtp_to.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_to.Location = new System.Drawing.Point(367, 56);
            this.dtp_to.Name = "dtp_to";
            this.dtp_to.Size = new System.Drawing.Size(196, 20);
            this.dtp_to.TabIndex = 304;
            // 
            // Account_Statement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1154, 580);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Account_Statement";
            this.Tag = " ";
            this.Text = "Account Statement";
            this.Load += new System.EventHandler(this.Account_Statement_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Stock)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgv_Stock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtp_from;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtp_to;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_show;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn voucher;
        private System.Windows.Forms.DataGridViewTextBoxColumn purno;
        private System.Windows.Forms.DataGridViewTextBoxColumn return_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountpaid;
        private System.Windows.Forms.DataGridViewTextBoxColumn advance;
        private System.Windows.Forms.DataGridViewTextBoxColumn Due;
        private System.Windows.Forms.DataGridViewTextBoxColumn pur_return;
    }
}