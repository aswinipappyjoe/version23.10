namespace PappyjoeMVC.View
{
    partial class Stocktransfer_itemlist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Stocktransfer_itemlist));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_ItemCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Btn_Add = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Lab_Msg = new System.Windows.Forms.Label();
            this.dgv_item_details = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.slno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_item_details)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txt_ItemCode);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.Btn_Add);
            this.panel2.Controls.Add(this.btn_close);
            this.panel2.Location = new System.Drawing.Point(3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(649, 56);
            this.panel2.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(64, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 308;
            this.label1.Text = "No Items Found";
            this.label1.Visible = false;
            // 
            // txt_ItemCode
            // 
            this.txt_ItemCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ItemCode.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.txt_ItemCode.Location = new System.Drawing.Point(64, 10);
            this.txt_ItemCode.Name = "txt_ItemCode";
            this.txt_ItemCode.Size = new System.Drawing.Size(367, 23);
            this.txt_ItemCode.TabIndex = 307;
            this.txt_ItemCode.Text = "Search by Item Code,Item Name";
            this.txt_ItemCode.Click += new System.EventHandler(this.txt_ItemCode_Click);
            this.txt_ItemCode.TextChanged += new System.EventHandler(this.txt_ItemCode_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(14, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 17);
            this.label2.TabIndex = 306;
            this.label2.Text = "Search";
            // 
            // Btn_Add
            // 
            this.Btn_Add.BackColor = System.Drawing.Color.LimeGreen;
            this.Btn_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Add.ForeColor = System.Drawing.Color.White;
            this.Btn_Add.Location = new System.Drawing.Point(491, 9);
            this.Btn_Add.Name = "Btn_Add";
            this.Btn_Add.Size = new System.Drawing.Size(75, 25);
            this.Btn_Add.TabIndex = 305;
            this.Btn_Add.Text = "OK";
            this.Btn_Add.UseVisualStyleBackColor = false;
            this.Btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.Tomato;
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close.ForeColor = System.Drawing.Color.White;
            this.btn_close.Location = new System.Drawing.Point(569, 9);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 25);
            this.btn_close.TabIndex = 302;
            this.btn_close.Text = "CLOSE";
            this.btn_close.UseVisualStyleBackColor = false;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Lab_Msg);
            this.panel1.Controls.Add(this.dgv_item_details);
            this.panel1.Location = new System.Drawing.Point(3, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(649, 385);
            this.panel1.TabIndex = 5;
            // 
            // Lab_Msg
            // 
            this.Lab_Msg.AutoSize = true;
            this.Lab_Msg.BackColor = System.Drawing.Color.Wheat;
            this.Lab_Msg.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_Msg.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.Lab_Msg.Location = new System.Drawing.Point(173, 143);
            this.Lab_Msg.Name = "Lab_Msg";
            this.Lab_Msg.Size = new System.Drawing.Size(177, 20);
            this.Lab_Msg.TabIndex = 304;
            this.Lab_Msg.Text = "Item does not have batch";
            this.Lab_Msg.Visible = false;
            // 
            // dgv_item_details
            // 
            this.dgv_item_details.AllowUserToAddRows = false;
            this.dgv_item_details.AllowUserToDeleteRows = false;
            this.dgv_item_details.AllowUserToResizeColumns = false;
            this.dgv_item_details.AllowUserToResizeRows = false;
            this.dgv_item_details.BackgroundColor = System.Drawing.Color.White;
            this.dgv_item_details.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_item_details.ColumnHeadersHeight = 25;
            this.dgv_item_details.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_item_details.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.slno,
            this.itemcode,
            this.name,
            this.stock});
            this.dgv_item_details.GridColor = System.Drawing.Color.LightGray;
            this.dgv_item_details.Location = new System.Drawing.Point(3, 0);
            this.dgv_item_details.MultiSelect = false;
            this.dgv_item_details.Name = "dgv_item_details";
            this.dgv_item_details.ReadOnly = true;
            this.dgv_item_details.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_item_details.RowHeadersVisible = false;
            this.dgv_item_details.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.dgv_item_details.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_item_details.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_item_details.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_item_details.Size = new System.Drawing.Size(646, 382);
            this.dgv_item_details.TabIndex = 0;
            // 
            // Id
            // 
            this.Id.HeaderText = " Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Id.Visible = false;
            this.Id.Width = 80;
            // 
            // slno
            // 
            this.slno.HeaderText = "Sl.No";
            this.slno.Name = "slno";
            this.slno.ReadOnly = true;
            this.slno.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.slno.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.slno.Width = 80;
            // 
            // itemcode
            // 
            this.itemcode.HeaderText = "ItemCode";
            this.itemcode.Name = "itemcode";
            this.itemcode.ReadOnly = true;
            this.itemcode.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.itemcode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.itemcode.Width = 155;
            // 
            // name
            // 
            this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.name.HeaderText = "Description";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // stock
            // 
            this.stock.HeaderText = "Stock";
            this.stock.Name = "stock";
            this.stock.ReadOnly = true;
            this.stock.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.stock.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.stock.Width = 90;
            // 
            // Stocktransfer_itemlist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(657, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(673, 489);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(673, 489);
            this.Name = "Stocktransfer_itemlist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Items List";
            this.Load += new System.EventHandler(this.Stocktransfer_itemlist_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_item_details)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Lab_Msg;
        private System.Windows.Forms.DataGridView dgv_item_details;
        private System.Windows.Forms.Button Btn_Add;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_ItemCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn slno;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn stock;
    }
}