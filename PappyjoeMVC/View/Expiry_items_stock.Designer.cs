
namespace PappyjoeMVC.View
{
    partial class Expiry_items_stock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Expiry_items_stock));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.DGV_Stock = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.batch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clunit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Stock)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(839, 54);
            this.panel1.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.BackColor = System.Drawing.Color.Red;
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(737, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(95, 30);
            this.button4.TabIndex = 294;
            this.button4.Text = "CLOSE";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(7, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 21);
            this.label8.TabIndex = 293;
            this.label8.Text = "EXPIRED ITEMS";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.DGV_Stock);
            this.panel2.Location = new System.Drawing.Point(-1, 57);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(839, 474);
            this.panel2.TabIndex = 1;
            // 
            // DGV_Stock
            // 
            this.DGV_Stock.AllowUserToAddRows = false;
            this.DGV_Stock.AllowUserToDeleteRows = false;
            this.DGV_Stock.AllowUserToResizeColumns = false;
            this.DGV_Stock.AllowUserToResizeRows = false;
            this.DGV_Stock.BackgroundColor = System.Drawing.Color.White;
            this.DGV_Stock.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DGV_Stock.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.DGV_Stock.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV_Stock.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGV_Stock.ColumnHeadersHeight = 25;
            this.DGV_Stock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DGV_Stock.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.SLNO,
            this.item_code,
            this.item_name,
            this.batch,
            this.qty,
            this.clunit,
            this.RSP});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGV_Stock.DefaultCellStyle = dataGridViewCellStyle2;
            this.DGV_Stock.GridColor = System.Drawing.Color.DimGray;
            this.DGV_Stock.Location = new System.Drawing.Point(0, 0);
            this.DGV_Stock.Name = "DGV_Stock";
            this.DGV_Stock.ReadOnly = true;
            this.DGV_Stock.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV_Stock.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DGV_Stock.RowHeadersVisible = false;
            this.DGV_Stock.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DGV_Stock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGV_Stock.Size = new System.Drawing.Size(823, 474);
            this.DGV_Stock.TabIndex = 1;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.id.Visible = false;
            this.id.Width = 20;
            // 
            // SLNO
            // 
            this.SLNO.FillWeight = 14.47914F;
            this.SLNO.HeaderText = "SL.NO.";
            this.SLNO.Name = "SLNO";
            this.SLNO.ReadOnly = true;
            this.SLNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SLNO.Width = 50;
            // 
            // item_code
            // 
            this.item_code.FillWeight = 30.10132F;
            this.item_code.HeaderText = "ITEM CODE";
            this.item_code.Name = "item_code";
            this.item_code.ReadOnly = true;
            this.item_code.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // item_name
            // 
            this.item_name.FillWeight = 47.54298F;
            this.item_name.HeaderText = "ITEM NAME";
            this.item_name.Name = "item_name";
            this.item_name.ReadOnly = true;
            this.item_name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.item_name.Width = 250;
            // 
            // batch
            // 
            this.batch.FillWeight = 394.4162F;
            this.batch.HeaderText = "Batch Number";
            this.batch.Name = "batch";
            this.batch.ReadOnly = true;
            this.batch.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.batch.Width = 90;
            // 
            // qty
            // 
            this.qty.FillWeight = 39.01161F;
            this.qty.HeaderText = "STOCK";
            this.qty.Name = "qty";
            this.qty.ReadOnly = true;
            this.qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clunit
            // 
            this.clunit.FillWeight = 30.00803F;
            this.clunit.HeaderText = "Product Date";
            this.clunit.Name = "clunit";
            this.clunit.ReadOnly = true;
            this.clunit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clunit.Width = 120;
            // 
            // RSP
            // 
            this.RSP.FillWeight = 209.4374F;
            this.RSP.HeaderText = "Expiry Date";
            this.RSP.Name = "RSP";
            this.RSP.ReadOnly = true;
            this.RSP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RSP.Width = 90;
            // 
            // Expiry_items_stock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(834, 533);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Expiry_items_stock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Expired Items";
            this.Load += new System.EventHandler(this.Expiry_items_stock_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Stock)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView DGV_Stock;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn batch;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn clunit;
        private System.Windows.Forms.DataGridViewTextBoxColumn RSP;
    }
}