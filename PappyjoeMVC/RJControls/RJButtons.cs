using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Drawing.Design;
namespace PappyjoeMVC.RJControls
{
  public class RJButtons :Button
    {
        //fields
        private int bordersize = 1;
        private int borderRadius = 40;
        private Color borderColor = Color.FromArgb(71, 139, 184);

        //constructor
       public RJButtons()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(150,40);
            this.BackColor = Color.MediumSlateBlue;
            this.ForeColor = Color.White;
        }

        //methods
        private GraphicsPath GetFigurePath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X,rect.Y,radius,radius,180,90);
            path.AddArc(rect.Width-radius,rect.Y,radius,radius,270,90);
            path.AddArc(rect.Width-radius,rect.Height-radius,radius,radius,0,90);
            path.AddArc(rect.X,rect.Height-radius,radius,radius,90,90);
            path.CloseFigure();
            return path;

        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            //base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            RectangleF rectSurface = new RectangleF(0, 0, this.Width, this.Height);
            RectangleF rectBorder = new RectangleF(1, 1, this.Width - 0.8F, this.Height - 1);
            if (borderRadius > 2)
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - 1F))
                using (Pen PenSurface = new Pen(this.Parent.BackColor, 2))
                using (Pen PenBorder = new Pen(borderColor, bordersize))
                {
                    PenBorder.Alignment = PenAlignment.Inset;
                    this.Region = new Region(pathSurface);
                    pevent.Graphics.DrawPath(PenSurface, pathSurface);

                    //button border
                    if (bordersize >= 1)
                    {
                        pevent.Graphics.DrawPath(PenBorder, pathBorder);

                    }
                }

            }
            else //normal button
            {
                //button surface
                this.Region = new Region(rectSurface);
                if (bordersize >= 1)
                {
                    using (Pen penBorder = new Pen(borderColor, bordersize))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        pevent.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }

            }
        }
        //protected override void OnPaint(PaintEventArgs pevent)
        //{
        //    base.OnPaint(pevent);
        //    pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        //    RectangleF rectSurface = new RectangleF(0,0,this.Width,this.Height);
        //    RectangleF rectBorder = new RectangleF(1,1,this.Width-0.8F,this.Height-1);
        //    if(borderRadius>2)
        //    {
        //        using(GraphicsPath pathSurface =GetFigurePath(rectSurface,borderRadius))
        //        using (GraphicsPath pathBorder=GetFigurePath(rectBorder,borderRadius-1F))
        //        using (Pen PenSurface = new Pen(this.Parent.BackColor, 2))
        //        using (Pen PenBorder = new Pen(borderColor, bordersize))
        //        {
        //            PenBorder.Alignment = PenAlignment.Inset;
        //            this.Region = new Region(pathSurface);
        //            pevent.Graphics.DrawPath(PenSurface, pathSurface);

        //            //button border
        //            if(bordersize >=1)
        //            {
        //                pevent.Graphics.DrawPath(PenBorder,pathBorder);

        //            }
        //        }

        //    }
        //    else //normal button
        //    {
        //        //button surface
        //        this.Region = new Region(rectSurface);
        //        if(bordersize>=1)
        //        {
        //            using(Pen penBorder =new Pen(borderColor,bordersize))
        //            {
        //                penBorder.Alignment = PenAlignment.Inset;
        //                pevent.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
        //            }
        //        }

        //    }
        //}
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Parent.BackColorChanged += new EventHandler(container_BackColorChanged);
        }

        private void container_BackColorChanged(object sender, EventArgs e)
        {
            if (this.DesignMode)
                this.Invalidate();
        }
    }
}
