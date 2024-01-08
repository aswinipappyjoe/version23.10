
namespace PappyjoeMVC.View
{
    partial class certificate_found
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(certificate_found));
            this.Dgv_Attachment = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label41 = new System.Windows.Forms.Label();
            this.Column4 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.photo = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Attachment)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Dgv_Attachment
            // 
            this.Dgv_Attachment.AllowUserToAddRows = false;
            this.Dgv_Attachment.AllowUserToDeleteRows = false;
            this.Dgv_Attachment.AllowUserToResizeColumns = false;
            this.Dgv_Attachment.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.Dgv_Attachment.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Dgv_Attachment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Dgv_Attachment.BackgroundColor = System.Drawing.Color.White;
            this.Dgv_Attachment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Dgv_Attachment.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.Dgv_Attachment.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Dgv_Attachment.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Dgv_Attachment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Attachment.ColumnHeadersVisible = false;
            this.Dgv_Attachment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column2,
            this.Column3,
            this.Column1,
            this.Column5,
            this.Column6,
            this.photo});
            this.Dgv_Attachment.Location = new System.Drawing.Point(6, 43);
            this.Dgv_Attachment.Name = "Dgv_Attachment";
            this.Dgv_Attachment.ReadOnly = true;
            this.Dgv_Attachment.RowHeadersVisible = false;
            this.Dgv_Attachment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Dgv_Attachment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Dgv_Attachment.Size = new System.Drawing.Size(954, 482);
            this.Dgv_Attachment.TabIndex = 268;
            this.Dgv_Attachment.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Attachment_CellClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label41);
            this.panel2.Location = new System.Drawing.Point(6, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(954, 42);
            this.panel2.TabIndex = 284;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label41.Location = new System.Drawing.Point(12, 10);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(101, 20);
            this.label41.TabIndex = 275;
            this.label41.Text = "CERTIFICATES";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "DEL";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column4.Visible = false;
            this.Column4.Width = 70;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "date";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 250;
            // 
            // Column3
            // 
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column3.HeaderText = "doc";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 240;
            // 
            // Column1
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column1.HeaderText = "Photo Name";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 250;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "id";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Column6";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Visible = false;
            // 
            // photo
            // 
            this.photo.HeaderText = "Photo";
            this.photo.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.photo.Name = "photo";
            this.photo.ReadOnly = true;
            // 
            // certificate_found
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 524);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.Dgv_Attachment);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "certificate_found";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "certificate_found";
            this.Load += new System.EventHandler(this.certificate_found_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Attachment)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView Dgv_Attachment;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.DataGridViewImageColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewImageColumn photo;
    }
}