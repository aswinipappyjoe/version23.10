using PappyjoeMVC.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PappyjoeMVC.View
{
    public partial class Special_Cases : Form
    {
        public string doctor_id = "";
        public string patient_id = "";
        Graphics g;
        int x = -1;
        int y = -1;
        bool moving = false;
        Pen pen;
        Add_Attachments_controller ctrlr = new Add_Attachments_controller();
        public Special_Cases()
        {
            InitializeComponent();
            g = pb_main.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pen = new Pen(Color.Black, 2);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;//
        }

        private void pb_skin_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.skin;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.skin;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void pb_eye_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.eye;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.eye;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void pb_tooth_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.AdultTooth;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.AdultTooth;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void pb_brain_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.brain;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.brain;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void pb_heart_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.heart;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.heart;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void pb_stomach_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.stomach;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.stomach;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void pb_pancreas_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.pancreas;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.pancreas;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void pb_Bladder_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.bladder;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.bladder;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void pb_malers_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.male_reproductive_system;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.male_reproductive_system;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void pb_intestine_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.instestinies;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.instestinies;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void pb_femalers_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.female_reproductive_system;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.female_reproductive_system;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void pb_kidneys_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.kidneys;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.kidneys;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void pb_gallbladder_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.gallbladder;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.gallbladder;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void pb_liver_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.liver;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.liver;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void pb_ear_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.ear;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.ear;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void pb_tongue_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.Tongue;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.Tongue;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void pb_lungs_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.lungs;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.lungs;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void pb_nose_Click(object sender, EventArgs e)
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.nose;
                    pb_main.BackgroundImageLayout = ImageLayout.Zoom;
                }
            }
            else
            {
                pb_main.BackgroundImage = PappyjoeMVC.Properties.Resources.nose;
                pb_main.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            pen.Color = p.BackColor;
        }
        private Bitmap bmp = new Bitmap(256, 256);
        private void pb_main_MouseDown(object sender, MouseEventArgs e)
        {
                moving = true;
                x = e.X;
                y = e.Y;
        }
        

        private void pb_main_MouseMove(object sender, MouseEventArgs e)
        {
            //using (g = Graphics.FromImage(bmp))
            {
                if (moving && x != -1 && y != -1)
                {
                    g.DrawLine(pen, new Point(x, y), e.Location);
                    x = e.X;
                    y = e.Y;
                }
            }
        }

        private void pb_main_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;
            x = -1;
            y = -1;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            pb_main.Invalidate();
            pb_main.Controls.Remove(btn_clear);
            pb_main.Controls.Remove(txt_panel);
            txt_panel.Clear();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Save();
        }

        public void Save()
        {
            ////string x = ClientSize.ToString();
            ///.................
            ///
            Bitmap bmp = new Bitmap(pb_main.ClientSize.Width, pb_main.ClientSize.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            Rectangle rect = pb_main.RectangleToScreen(pb_main.ClientRectangle);
            g.CopyFromScreen(rect.Location, Point.Empty, pb_main.Size);
            //g.CopyFromScreen(PointToScreen(pb_main.Location), new Point(0, 0), pb_main.Size);
            string server = this.ctrlr.getserver();
            string outputFileName = @"\\" + server + "\\Pappyjoe_utilities\\Attachments\\Special_case" + DateTime.Now.ToString("dd - MM - yyyy_hh - mm - ss") + ".jpg";
            //.............................
            //string outputFileName = @"D:\\Special_case" + DateTime.Now.ToString("dd - MM - yyyy_hh - mm - ss") + ".jpg";
            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(outputFileName, FileMode.Create, FileAccess.ReadWrite))
                {
                    bmp.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                    bmp.Dispose();
                }
            }
            string realfile = "Special_case" + DateTime.Now.ToString("dd - MM - yyyy_hh - mm - ss") + ".jpg";
            string pathimage = "\\" + "\\Pappyjoe_utilities" + "\\" + "\\Attachments\\" + "\\" + realfile;
            string catgry = "General";
            this.ctrlr.insattach(patient_id, realfile, pathimage, doctor_id, catgry);
            MessageBox.Show("Image saved successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void save_currntImage()
        {
            if (pb_main.BackgroundImage != null)
            {
                DialogResult res = MessageBox.Show("Click OK and click Save to save the work, click Cancel to open new body part", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                }
            }
        }

        TextBox txt_panel = new TextBox();
        bool isDrag = false;
        int lastY = 0;
        int lastX = 0;
        void txt_panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y >= (txt_panel.ClientRectangle.Bottom - 5) &&
            e.Y <= (txt_panel.ClientRectangle.Bottom + 5))
            {
                ((TextBox)sender).Cursor = Cursors.Cross;

                isDrag = true;
                lastY = e.Y;
            }
            if (e.X >= (txt_panel.ClientRectangle.Right - 5) &&
            e.X <= (txt_panel.ClientRectangle.Right + 5))
            {
                ((TextBox)sender).Cursor = Cursors.Cross;

                isDrag = true;
                lastX = e.X;
            }
        }
        void txt_panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrag)
            {
                ((TextBox)sender).Cursor = Cursors.Cross;

                txt_panel.Height += (e.Y - lastY);
                lastY = e.Y;
            }
            if (isDrag)
            {
                ((TextBox)sender).Cursor = Cursors.Cross;

                txt_panel.Width += (e.X - lastX);
                lastX = e.X;
            }
        }
        void txt_panel_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrag)
            {
                isDrag = false;
                ((TextBox)sender).Cursor = Cursors.Default;

            }
        }

        private void pb_main_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Point s = e.Location;
            txt_panel.Location = s;
            pb_main.Controls.Add(txt_panel);
            txt_panel.Font = new Font("Segoe UI", 10);
            txt_panel.Width = 200;
            txt_panel.Multiline = true;
            txt_panel.MouseDown += new MouseEventHandler(txt_panel_MouseDown);
            txt_panel.MouseMove += new MouseEventHandler(txt_panel_MouseMove);
            txt_panel.MouseUp += new MouseEventHandler(txt_panel_MouseUp);
        }

        private void Special_Cases_Load(object sender, EventArgs e)
        {

        }
    }
}
