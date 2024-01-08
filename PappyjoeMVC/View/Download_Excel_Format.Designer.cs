namespace PappyjoeMVC.View
{
    partial class Download_Excel_Format
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_Forms = new System.Windows.Forms.ComboBox();
            this.btn_download = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(290, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Download Standard Excel Format";
            // 
            // cmb_Forms
            // 
            this.cmb_Forms.FormattingEnabled = true;
            this.cmb_Forms.Items.AddRange(new object[] {
            "Patients",
            "Prescriptions",
            "Procedures",
            "Complaints",
            "Diagnosis",
            "Investigations",
            "Notes",
            "Observations"});
            this.cmb_Forms.Location = new System.Drawing.Point(98, 92);
            this.cmb_Forms.Name = "cmb_Forms";
            this.cmb_Forms.Size = new System.Drawing.Size(234, 21);
            this.cmb_Forms.TabIndex = 1;
            // 
            // btn_download
            // 
            this.btn_download.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_download.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_download.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_download.ForeColor = System.Drawing.Color.White;
            this.btn_download.Location = new System.Drawing.Point(338, 91);
            this.btn_download.Name = "btn_download";
            this.btn_download.Size = new System.Drawing.Size(75, 23);
            this.btn_download.TabIndex = 2;
            this.btn_download.Text = "Download";
            this.btn_download.UseVisualStyleBackColor = false;
            this.btn_download.Click += new System.EventHandler(this.btn_download_Click);
            // 
            // Download_Excel_Format
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_download);
            this.Controls.Add(this.cmb_Forms);
            this.Controls.Add(this.label1);
            this.Name = "Download_Excel_Format";
            this.Text = "Download_Excel_Format";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_Forms;
        private System.Windows.Forms.Button btn_download;
    }
}