namespace PappyjoeMVC.View
{
    partial class ListItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lb_room = new System.Windows.Forms.Label();
            this.lb_status = new System.Windows.Forms.Label();
            this.lb_patient = new System.Windows.Forms.Label();
            this.lb_roomid = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lb_room
            // 
            this.lb_room.Font = new System.Drawing.Font("Castellar", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_room.ForeColor = System.Drawing.Color.White;
            this.lb_room.Location = new System.Drawing.Point(8, 7);
            this.lb_room.Name = "lb_room";
            this.lb_room.Size = new System.Drawing.Size(75, 33);
            this.lb_room.TabIndex = 0;
            this.lb_room.Text = "label1";
            this.lb_room.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_status
            // 
            this.lb_status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_status.Font = new System.Drawing.Font("Papyrus", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_status.ForeColor = System.Drawing.Color.White;
            this.lb_status.Location = new System.Drawing.Point(115, 7);
            this.lb_status.Name = "lb_status";
            this.lb_status.Size = new System.Drawing.Size(61, 19);
            this.lb_status.TabIndex = 0;
            this.lb_status.Text = "label1";
            this.lb_status.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_patient
            // 
            this.lb_patient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_patient.Font = new System.Drawing.Font("OCR A Extended", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_patient.ForeColor = System.Drawing.Color.MediumSpringGreen;
            this.lb_patient.Location = new System.Drawing.Point(38, 35);
            this.lb_patient.Name = "lb_patient";
            this.lb_patient.Size = new System.Drawing.Size(100, 44);
            this.lb_patient.TabIndex = 0;
            this.lb_patient.Text = "label1";
            this.lb_patient.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_roomid
            // 
            this.lb_roomid.AutoSize = true;
            this.lb_roomid.Location = new System.Drawing.Point(21, 79);
            this.lb_roomid.Name = "lb_roomid";
            this.lb_roomid.Size = new System.Drawing.Size(35, 13);
            this.lb_roomid.TabIndex = 1;
            this.lb_roomid.Text = "label1";
            // 
            // ListItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Green;
            this.Controls.Add(this.lb_roomid);
            this.Controls.Add(this.lb_patient);
            this.Controls.Add(this.lb_status);
            this.Controls.Add(this.lb_room);
            this.Name = "ListItem";
            this.Size = new System.Drawing.Size(184, 107);
            //this.Click += new System.EventHandler(this.ListItem_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_room;
        private System.Windows.Forms.Label lb_status;
        private System.Windows.Forms.Label lb_patient;
        private System.Windows.Forms.Label lb_roomid;
    }
}
