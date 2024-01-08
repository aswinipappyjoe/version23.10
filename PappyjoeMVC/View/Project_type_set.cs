using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PappyjoeMVC.View;
using PappyjoeMVC.Controller;
using PappyjoeMVC.Model;

namespace PappyjoeMVC.View
{
    public partial class Project_type_set : Form
    {
        public Project_type_set()
        {
            InitializeComponent();
        }
        Connection db = new Connection();
        GlobalVariables gv = new GlobalVariables();
        private void btnnext_Click(object sender, EventArgs e)
        {
            try
            {
                string type = "";
                if (radioButton1.Checked == true)
                {
                    type = radioButton1.Text;
                }
                else if(radioButton2.Checked==true)
                {
                    type = radioButton2.Text;
                }
                else
                {
                    type = rad_pharmacy.Text;
                }
                DataTable dt_type = db.table("select * from tbl_pappyjoe_settings");
                if (dt_type.Rows.Count == 0)
                {
                    PappyjoeMVC.Model.Connection.MyGlobals.project_type = type;
                    db.execute("insert into tbl_pappyjoe_settings(id,project_type)values('1','" + type + "')");
                    //Application.EnableVisualStyles();
                    //Application.SetCompatibleTextRenderingDefault(false);
                    //Application.Run(new Login());
                    //Login frm = new Login();
                    ////frm.frameid = "1";
                    //frm.Show(this);
                    //frm.Dispose();

                    this.Hide();
                    var form2 = new Login();
                    form2.Closed += (s, args) => this.Close();
                    form2.Show();
                }
            }
            catch(Exception ex)
            {

            }
           
        }

        private void Project_type_set_Load(object sender, EventArgs e)
        {

        }
    }
}
