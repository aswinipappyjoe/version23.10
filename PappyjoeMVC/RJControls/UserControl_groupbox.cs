using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PappyjoeMVC.RJControls
{
    public partial class UserControl_groupbox : UserControl
    {
        private Color bordercolor = Color.MediumSlateBlue;
        private int bordersize = 2;
        private bool underlinestyle = false;
        private Color borderfocuscolor = Color.HotPink;
        private bool IsFocused = false;
        private Color placeholdercolor = Color.DimGray;
        private string placeholderText = "";
        private bool Isplaceholder = false;
        private bool Ispasswordchar = false;
        /// <summary>
        /// 
        private int borderradius = 0;
        public UserControl_groupbox()
        {
            InitializeComponent();
        }
        [Category("RJ Code Advanced")]
        public Color Bordercolor
        {
            get
            {
                return bordercolor;
            }
            set
            {
                bordercolor = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advanced")]
        public int Borderstyle
        {
            get
            {
                return bordersize;
            }
            set
            {
                bordersize = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advanced")]
        public bool Underlinestyle
        {
            get
            {
                return underlinestyle;
            }
            set
            {
                underlinestyle = value;
                this.Invalidate();
            }
        }
        //[Category("RJ Code Advanced")]
        //public bool multiline
        //{
        //    get
        //    {
        //        return textBox1.Multiline;
        //    }
        //    set
        //    {
        //        textBox1.Multiline = value;
        //        //this.Invalidate();
        //    }
        //}
        [Category("RJ Code Advanced")]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                groupBox1.BackColor = value;
            }
        }
        [Category("RJ Code Advanced")]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                groupBox1.ForeColor = value;
            }
        }
        [Category("RJ Code Advanced")]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                groupBox1.Font = value;
                if (this.DesignMode)
                    UpdateControlHeight();
            }
        }
        [Category("RJ Code Advanced")]
        public string Text
        {
            get
            {
                return groupBox1.Text;
            }
            set
            {
                //base.ForeColor = value;
                groupBox1.Text = value;
            }
        }
        [Category("RJ Code Advanced")]
        public int BorderRadius
        {
            get
            {
                return borderradius;
            }
            set
            {
                if (value >= 0)
                {
                    borderradius = value;
                    this.Invalidate();
                }

            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;// Graphics();
            if (BorderRadius > 1)
            {
                var rectBorderSmooth = this.ClientRectangle;
                var rectBorder = Rectangle.Inflate(rectBorderSmooth, bordersize, bordersize);
                var smoothsize = bordersize > 0 ? bordersize : 1;

                using (GraphicsPath pathBorderSmooth = GetFigurePath(rectBorderSmooth, borderradius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderradius - bordersize))
                using (Pen penBorderSmooth = new Pen(this.Parent.BackColor, smoothsize))
                using (Pen penBorder = new Pen(bordercolor, bordersize))
                {
                    this.Region = new Region(pathBorderSmooth);
                    if (borderradius > 15) SetTextBoxRoundedRegion();
                    graph.SmoothingMode = SmoothingMode.AntiAlias;
                    penBorder.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
                    if (IsFocused) penBorder.Color = borderfocuscolor;
                    if (underlinestyle)
                    {
                        graph.DrawPath(penBorderSmooth, pathBorderSmooth);
                        //draw border
                        graph.SmoothingMode = SmoothingMode.None;
                        graph.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 1);

                    }
                    else  //normal style
                    {
                        graph.DrawPath(penBorderSmooth, pathBorderSmooth);
                        graph.DrawPath(penBorder, pathBorder);
                        //graph.DrawRectangle(penBorder, 0, 0, this.Width - 0.5F, this.Height - 0.5F);

                    }
                }
            }
            else
            {
                using (Pen penBorder = new Pen(Bordercolor, bordersize))
                {
                    this.Region = new Region(this.ClientRectangle);
                    penBorder.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                    if (underlinestyle)
                        graph.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 1);
                    else
                        graph.DrawRectangle(penBorder, 0, 0, this.Width - 0.5F, this.Height - 0.5F);
                }
            }

        }
        private void SetTextBoxRoundedRegion()
        {
            GraphicsPath pathTxT;
            //if (multiline)
            //{
            //    pathTxT = GetFigurePath(textBox1.ClientRectangle, borderradius - bordersize);
            //    textBox1.Region = new Region(pathTxT);

            //}
            //else
            //{
                pathTxT = GetFigurePath(groupBox1.ClientRectangle, bordersize * 2);
            groupBox1.Region = new Region(pathTxT);
            //}
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            //if(this.DesignMode)
            UpdateControlHeight();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.DesignMode)
                UpdateControlHeight();
        }
        private void UpdateControlHeight()
        {
            //if (textBox1.Multiline == false)
            //{
                int txthieght = TextRenderer.MeasureText("Text", this.Font).Height + 1;
            //groupBox1.Multiline = true;
            groupBox1.MinimumSize = new Size(0, txthieght);
                this.Height = groupBox1.Height + this.Padding.Top + this.Padding.Bottom;
            //}
        }
        private GraphicsPath GetFigurePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curvesize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curvesize, curvesize, 180, 99);
            path.AddArc(rect.Right - curvesize, rect.Y, curvesize, curvesize, 270, 90);
            path.AddArc(rect.Right - curvesize, rect.Bottom - curvesize, curvesize, curvesize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curvesize, curvesize, curvesize, 90, 90);
            path.CloseFigure();
            return path;
        }

    }
}
