
namespace PappyjoeMVC.View
{
    partial class Choose_Consumables_Pharmacy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Choose_Consumables_Pharmacy));
            this.chk_consume = new System.Windows.Forms.CheckBox();
            this.button_save = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chk_consume
            // 
            this.chk_consume.AutoSize = true;
            this.chk_consume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_consume.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_consume.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.chk_consume.Location = new System.Drawing.Point(79, 156);
            this.chk_consume.Name = "chk_consume";
            this.chk_consume.Size = new System.Drawing.Size(102, 21);
            this.chk_consume.TabIndex = 6;
            this.chk_consume.Text = "Consumables";
            this.chk_consume.UseVisualStyleBackColor = true;
            this.chk_consume.CheckedChanged += new System.EventHandler(this.chk_consume_CheckedChanged);
            // 
            // button_save
            // 
            this.button_save.BackColor = System.Drawing.Color.LimeGreen;
            this.button_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_save.ForeColor = System.Drawing.Color.White;
            this.button_save.Location = new System.Drawing.Point(197, 156);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(75, 28);
            this.button_save.TabIndex = 7;
            this.button_save.Text = "Save";
            this.button_save.UseVisualStyleBackColor = false;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(29, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(592, 34);
            this.label1.TabIndex = 8;
            this.label1.Text = "You need to select an Initial Value. You can choose an alphanumeric prefix (will " +
    "not be incremented), \r\nand a numeric part which will be automatically incremente" +
    "d for every new Bill.";
            // 
            // Choose_Consumables_Pharmacy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.chk_consume);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Choose_Consumables_Pharmacy";
            this.Text = "Consumables / Pharmacy";
            this.Load += new System.EventHandler(this.Choose_Consumables_Pharmacy_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chk_consume;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Label label1;
    }
}