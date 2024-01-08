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
    [DefaultEvent("onSelectedIndexChanged")]
    class RjCombobox : UserControl
    {
        //fields
        private Color backcolor = Color.WhiteSmoke;
        private Color iconcolor = Color.MediumSlateBlue;
        private Color listbackcolor = Color.FromArgb(230, 228, 245);
        private Color listtextcolor = Color.DimGray;
        private Color listbordercolor = Color.MediumSlateBlue;
        private int bordersize = 1;
        private Color Bordercolor = Color.Black;
        //items
        private ComboBox cmbList;
        private Label lblText;
        private Button btnIcon;

        public new Color Backcolor { get { return  backcolor; } set { backcolor = value;
                lblText.BackColor = backcolor;btnIcon.BackColor = backcolor;
            } }
        public Color Iconcolor { get => iconcolor; set { iconcolor = value; btnIcon.Invalidate(); } }
        public Color Listbackcolor { get => listbackcolor; set { listbackcolor = value; cmbList.BackColor = listbackcolor; } }
        public Color Listtextcolor { get => listtextcolor; set => listtextcolor = value; }
        public Color Listbordercolor { get => listbordercolor; set { listbordercolor = value;cmbList.ForeColor = listtextcolor; } }
        public int Bordersize { get => bordersize; set { bordersize = value; this.Padding = new Padding(bordersize); AdjustComboboxDimensions(); } }
        public Color BorderColor { get => Bordercolor; set { Bordercolor = value; base.BackColor=BorderColor; } }
        //events
        public event EventHandler onSelectedIndexChanged;//default event
        public override Color ForeColor { get => base.ForeColor; set { base.ForeColor = value; lblText.ForeColor = value; } }
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                lblText.Font = value;
                cmbList.Font = value;
               
            }
        }
        public string Texts
        {
            get
            {
                return lblText.Text;

            }
            set
            {
                lblText.Text = value;
            }
        }
        public ComboBoxStyle DropDownStyle
        {
            get
            {
                return cmbList.DropDownStyle;
            }
            set
            {
                if (cmbList.DropDownStyle != ComboBoxStyle.Simple)
                    cmbList.DropDownStyle = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Localizable(true)]
        [MergableProperty(false)]
     
        public ComboBox. ObjectCollection Items {

            get
            {
                return cmbList.Items;
            }
        }
        [AttributeProvider(typeof(IListSource))]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.Repaint)]
      
        public object DataSource { get { return cmbList.DataSource; } set {cmbList.DataSource=value; } }
       

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
     
        public AutoCompleteStringCollection AutoCompleteCustomSource { get {
                return cmbList.AutoCompleteCustomSource;
            } set { cmbList.AutoCompleteCustomSource = value; } }
        [Browsable(true)]
        [DefaultValue(AutoCompleteSource.None)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public AutoCompleteSource AutoCompleteSource { get {
                return cmbList. AutoCompleteSource;
            } set {
                cmbList.AutoCompleteSource = value;
            } }
        
        [Browsable(true)]
        [DefaultValue(AutoCompleteMode.None)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public AutoCompleteMode AutoCompleteMode { get {
                return cmbList. AutoCompleteMode;
            } set {
                cmbList.AutoCompleteMode = value
                ;
            } }
       
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedItem { get {
                return cmbList.SelectedItem;
            }
            set
            {
                cmbList.SelectedItem = value
                ;
            }
        }
      
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public  int SelectedIndex {
            get
            {
                return cmbList.SelectedIndex;
            }
            set
            {
                cmbList.SelectedIndex = value
                ;
            }
        }
       

        //constructor
        public RjCombobox()
        {
            cmbList = new ComboBox();
            lblText = new Label();
            btnIcon = new Button();
            this.SuspendLayout();

            //combobox dropdown list
            cmbList.BackColor = listbackcolor;
            cmbList.Font = new Font(this.Font.Name, 10F);
            cmbList.ForeColor = listtextcolor;
            cmbList.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);
            cmbList.TextChanged += new EventHandler(ComboBox_TextChanged);

            //button :icon
            btnIcon.Dock = DockStyle.Right;
            btnIcon.FlatStyle = FlatStyle.Flat;
            btnIcon.FlatAppearance.BorderSize = 0;
            btnIcon.BackColor = backcolor;
            btnIcon.Size = new Size(30, 30);
            btnIcon.Cursor = Cursors.Hand;

            btnIcon.Click += new EventHandler(Icon_Click);
            btnIcon.Paint += new PaintEventHandler(Icon_Paint);

            //label
            lblText.Dock = DockStyle.Fill;
            lblText.AutoSize = false; 
            lblText.BackColor = backcolor;
            lblText.TextAlign = ContentAlignment.MiddleLeft;
            lblText.Padding = new Padding(8, 0, 0, 0);//8
            lblText.Font = new Font(this.Font.Name, 9F);
            lblText.Click += new EventHandler(Surface_click);

            //user control
            this.Controls.Add(lblText);
            this.Controls.Add(btnIcon);
            this.Controls.Add(cmbList);
            this.MinimumSize = new Size(100, 30);//200
            this.Size = new Size(200, 30);
            this.ForeColor = Color.DimGray;
            this.Padding = new Padding(bordersize);
            base.BackColor = Bordercolor;
            //this.backcolor = borderc
            this.ResumeLayout();
            AdjustComboboxDimensions();

        }

        private void Icon_Paint(object sender, PaintEventArgs e)
        {
            int iconwidth = 11;
            int iconheight = 6;
            var rectIcon = new Rectangle((btnIcon.Width - iconwidth) / 2, (btnIcon.Height - iconheight) / 2, iconwidth, iconheight);
            Graphics graph = e.Graphics;// Graphics();

            using (GraphicsPath path = new GraphicsPath())
            using (Pen pen = new Pen(iconcolor, 2))
            {
                graph.SmoothingMode = SmoothingMode.AntiAlias;
                path.AddLine(rectIcon.X, rectIcon.Y, rectIcon.X + (iconwidth / 2), rectIcon.Bottom);
                path.AddLine(rectIcon.X + (iconwidth / 2), rectIcon.Bottom, rectIcon.Right, rectIcon.Y);
                graph.DrawPath(pen, path);
            }
        }

        private void AdjustComboboxDimensions()
        {
            cmbList.Width = lblText.Width;
            cmbList.Location = new Point()
            {
                X = this.Width - this.Padding.Right - cmbList.Width,
                Y = lblText.Bottom - cmbList.Height
            };
        }
        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (onSelectedIndexChanged != null)
                onSelectedIndexChanged.Invoke(sender, e);

            lblText.Text = cmbList.Text;
        }
        private void comboBox_Load(object sender, EventArgs e)
        {
            //this.OnLoad(e);
            //LoadComboBox(cmb_name,  dataSource,  valueMember,  displayMember);
            //BindComboBox(comboBox,  boundDataSource,  boundDataMember);
        }
        private void Surface_click(object sender, EventArgs e)
        {
            this.OnClick(e);
            cmbList.Select();
            if (cmbList.DropDownStyle == ComboBoxStyle.DropDownList)
                cmbList.DroppedDown= true;
        }

        private void Icon_Click(object sender, EventArgs e)
        {
            cmbList.Select();
            //if (cmbList.DropDownStyle == ComboBoxStyle.DropDownList)
                cmbList.DroppedDown = true;
        }

        private void ComboBox_TextChanged(object sender, EventArgs e)
        {
            lblText.Text = cmbList.Text;
        }

        //public static void LoadComboBox(ComboBox comboBox, object dataSource, string valueMember, string displayMember)
        //{
        //    comboBox.DataSource = dataSource;

        //    comboBox.ValueMember = valueMember;
        //    comboBox.DisplayMember = displayMember;
        //}
        //public static void BindComboBox(ComboBox comboBox, object boundDataSource, string boundDataMember)
        //{
        //    comboBox.DataBindings.Clear();
        //    comboBox.DataBindings.Add("SelectedValue", boundDataSource, boundDataMember);
        //}
        //private void Icon_paint(object sender,PaintEventArgs e )
        //{
        //    //feilds

        //}

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // RjCombobox
            // 
            this.Name = "RjCombobox";
            this.Size = new System.Drawing.Size(112, 29);
            this.ResumeLayout(false);

        }


    }
}
