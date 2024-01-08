
namespace PappyjoeMVC.View
{
    partial class stock_det
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(stock_det));
            this.button1 = new System.Windows.Forms.Button();
            this.dgv_stockdetials = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.sno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_batch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_exp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_stockdetials)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Tomato;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(392, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 27);
            this.button1.TabIndex = 0;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgv_stockdetials
            // 
            this.dgv_stockdetials.AllowUserToAddRows = false;
            this.dgv_stockdetials.AllowUserToDeleteRows = false;
            this.dgv_stockdetials.AllowUserToResizeColumns = false;
            this.dgv_stockdetials.AllowUserToResizeRows = false;
            this.dgv_stockdetials.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_stockdetials.BackgroundColor = System.Drawing.Color.White;
            this.dgv_stockdetials.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_stockdetials.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_stockdetials.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_stockdetials.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_stockdetials.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sno,
            this.c_batch,
            this.c_exp,
            this.c_stock,
            this.rate});
            this.dgv_stockdetials.EnableHeadersVisualStyles = false;
            this.dgv_stockdetials.GridColor = System.Drawing.Color.White;
            this.dgv_stockdetials.Location = new System.Drawing.Point(0, 45);
            this.dgv_stockdetials.Name = "dgv_stockdetials";
            this.dgv_stockdetials.ReadOnly = true;
            this.dgv_stockdetials.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_stockdetials.RowHeadersVisible = false;
            this.dgv_stockdetials.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_stockdetials.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_stockdetials.Size = new System.Drawing.Size(465, 281);
            this.dgv_stockdetials.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(5, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(135, 21);
            this.label8.TabIndex = 2;
            this.label8.Text = "Batch Wise Stock";
            // 
            // sno
            // 
            this.sno.HeaderText = "Sno";
            this.sno.Name = "sno";
            this.sno.ReadOnly = true;
            this.sno.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.sno.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.sno.Visible = false;
            // 
            // c_batch
            // 
            this.c_batch.HeaderText = "Batch";
            this.c_batch.Name = "c_batch";
            this.c_batch.ReadOnly = true;
            this.c_batch.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.c_batch.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.c_batch.Width = 150;
            // 
            // c_exp
            // 
            this.c_exp.HeaderText = "Exp-date";
            this.c_exp.Name = "c_exp";
            this.c_exp.ReadOnly = true;
            this.c_exp.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.c_exp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // c_stock
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.c_stock.DefaultCellStyle = dataGridViewCellStyle2;
            this.c_stock.HeaderText = "Stock";
            this.c_stock.Name = "c_stock";
            this.c_stock.ReadOnly = true;
            this.c_stock.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.c_stock.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // rate
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.rate.DefaultCellStyle = dataGridViewCellStyle3;
            this.rate.HeaderText = "Cost(Without GST)";
            this.rate.Name = "rate";
            this.rate.ReadOnly = true;
            this.rate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.rate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.rate.Width = 110;
            // 
            // stock_det
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(466, 328);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dgv_stockdetials);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "stock_det";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Batch Details";
            this.Load += new System.EventHandler(this.stock_det_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_stockdetials)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgv_stockdetials;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn sno;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_batch;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_exp;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_stock;
        private System.Windows.Forms.DataGridViewTextBoxColumn rate;
    }
}