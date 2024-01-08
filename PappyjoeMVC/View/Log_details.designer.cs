
namespace PappyjoeMVC.View
{
    partial class Log_details
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
            this.label41 = new System.Windows.Forms.Label();
            this.dgv_log_details = new System.Windows.Forms.DataGridView();
            this.c_uname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_ltype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_close = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_log_details)).BeginInit();
            this.SuspendLayout();
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label41.Location = new System.Drawing.Point(12, 9);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(104, 21);
            this.label41.TabIndex = 284;
            this.label41.Text = "Login Details";
            // 
            // dgv_log_details
            // 
            this.dgv_log_details.BackgroundColor = System.Drawing.Color.White;
            this.dgv_log_details.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_log_details.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgv_log_details.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_log_details.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_log_details.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.c_uname,
            this.c_ltype,
            this.c_id});
            this.dgv_log_details.GridColor = System.Drawing.Color.White;
            this.dgv_log_details.Location = new System.Drawing.Point(0, 48);
            this.dgv_log_details.Name = "dgv_log_details";
            this.dgv_log_details.RowHeadersVisible = false;
            this.dgv_log_details.Size = new System.Drawing.Size(638, 286);
            this.dgv_log_details.TabIndex = 285;
            // 
            // c_uname
            // 
            this.c_uname.HeaderText = "User Name";
            this.c_uname.Name = "c_uname";
            this.c_uname.Width = 180;
            // 
            // c_ltype
            // 
            this.c_ltype.HeaderText = "Login Type";
            this.c_ltype.Name = "c_ltype";
            this.c_ltype.Width = 180;
            // 
            // c_id
            // 
            this.c_id.HeaderText = "ID";
            this.c_id.Name = "c_id";
            this.c_id.Width = 180;
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.Tomato;
            this.btn_close.ForeColor = System.Drawing.Color.Black;
            this.btn_close.Location = new System.Drawing.Point(467, 5);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(60, 32);
            this.btn_close.TabIndex = 286;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = false;
            this.btn_close.Click += new System.EventHandler(this.button1_Click);
            // 
            // Log_details
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 450);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.dgv_log_details);
            this.Controls.Add(this.label41);
            this.Name = "Log_details";
            this.Text = "Log_details";
            this.Load += new System.EventHandler(this.Log_details_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_log_details)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.DataGridView dgv_log_details;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_uname;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_ltype;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_id;
    }
}