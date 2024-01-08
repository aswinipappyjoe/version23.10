using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PappyjoeMVC.Model;
namespace PappyjoeMVC.View
{
    public partial class certificate_found : Form
    {
        Attachments_model mdl = new Attachments_model();
        Connection db = new Connection();
        public string patient_id = "";
        public certificate_found()
        {
            InitializeComponent();
        }

        private void certificate_found_Load(object sender, EventArgs e)
        {
            DataTable attach = this.mdl.get_certificate(patient_id);
            FillAttachmentGrid(attach);
        }
        public void FillAttachmentGrid(DataTable attach)
        {
            if (attach.Rows.Count > 0)
            {
                string doctor = "";
                for (int i = 0; i < attach.Rows.Count; i++)
                {
                    string paths = db.server();
                    string path = attach.Rows[i]["path"].ToString();
                    //if (decimal.Parse(attach.Rows[i]["dr_id"].ToString()) > 0)
                    //{
                    //    this.ctrlr.Get_DoctorName(attach.Rows[i]["dr_id"].ToString());
                    //    doctor = dctr;
                    //}
                    string ext = Path.GetExtension(attach.Rows[i]["Path"].ToString());
                    //                     0                          1                                                   2                                             3                       4                            5                    6  
                    Dgv_Attachment.Rows.Add("", "Date : " + String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(attach.Rows[i]["date"].ToString())), "Category:" + attach.Rows[i]["CategoryName"].ToString(), "Added By : Dr." + doctor, attach.Rows[i]["photo_name"].ToString(), attach.Rows[i]["id"].ToString(), ext);
                    Dgv_Attachment.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    Dgv_Attachment.Columns[1].DefaultCellStyle.Font = new System.Drawing.Font("Seigo", 8, FontStyle.Regular);
                    Dgv_Attachment.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    Dgv_Attachment.Columns[3].DefaultCellStyle.Font = new System.Drawing.Font("Seigo", 9, FontStyle.Bold);
                    Dgv_Attachment.Columns[3].DefaultCellStyle.ForeColor = Color.Green;
                    Dgv_Attachment.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    Dgv_Attachment.Columns[2].DefaultCellStyle.Font = new System.Drawing.Font("Seigo", 7, FontStyle.Bold);
                    Dgv_Attachment.Columns[5].Visible = false;
                    Dgv_Attachment.Columns[0].Width = 50;
                    Dgv_Attachment.Columns[1].Width = 225;
                    Dgv_Attachment.Columns[2].Width = 210;
                    Dgv_Attachment.Columns[3].Width = 150;
                    Dgv_Attachment.Columns[4].Width = 150;
                    Dgv_Attachment.Columns[5].Width = 10;
                    Dgv_Attachment.Rows[i].Cells[0].Value = PappyjoeMVC.Properties.Resources.erase_icon;
                    Dgv_Attachment.Rows[i].Height = 85;
                    if (File.Exists(paths + path) == true)
                    {
                        if (ext.ToLower() == ".jpeg" || ext.ToLower() == ".jpg" || ext.ToLower() == ".gif" || ext.ToLower() == ".png")
                        {
                            try
                            {
                                using (FileStream stream = new FileStream(paths + path, FileMode.Open, FileAccess.Read))
                                {
                                    Image image = Image.FromStream(stream);
                                    stream.Dispose();
                                    Dgv_Attachment.Rows[i].Cells[6].Value = image;
                                }
                            }
                            catch (Exception ex)
                            { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                        else if (ext.ToLower() == ".docx" || ext.ToLower() == ".doc")
                        {
                            Dgv_Attachment.Rows[i].Cells[6].Value = PappyjoeMVC.Properties.Resources.word_doc_icon;
                        }
                        else if (ext.ToLower() == ".xls" || ext.ToLower() == ".xlsx")
                        {
                            Dgv_Attachment.Rows[i].Cells[6].Value = PappyjoeMVC.Properties.Resources.excel_doc_icon;
                        }
                        else if (ext.ToLower() == ".pdf")
                        {
                            Dgv_Attachment.Rows[i].Cells[6].Value = PappyjoeMVC.Properties.Resources.pdf;
                        }
                    }
                    else
                    {
                        Dgv_Attachment.Rows[i].Cells[6].Value = PappyjoeMVC.Properties.Resources.no_image_icon_6;
                    }
                }
            }
            else { }
        }
        public int attach_id = 0;

        public object File_Type { get; private set; }

        private void Dgv_Attachment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                attach_id = int.Parse(Dgv_Attachment.Rows[e.RowIndex].Cells[5].Value.ToString());

                try
                {
                    string path = mdl.getpath(attach_id);
                    getpath(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void getpath(string path1)
        {
            try
            {
                string realfile = "";
                string paths = db.server();
                if (path1 != "")
                {
                    realfile = paths + path1;
                    if (File.Exists(realfile) == true)
                    {
                        //if (File_Type.ToLower() == ".jpeg" || File_Type.ToLower() == ".jpg" || File_Type.ToLower() == ".gif" || File_Type.ToLower() == ".png")
                        //{
                        //    Image_Zoom frm = new Image_Zoom();
                        //    frm.attach_id = attach_id;
                        //    frm.ShowDialog(this);
                        //    frm.Dispose();
                        //}
                        //else if (File_Type.ToLower() == ".docx" || File_Type.ToLower() == ".doc")
                        //{
                        //    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                        //    saveFileDialog1.Filter = "Word Documents|*.doc | Word Documents 2010|*.docx";
                        //    saveFileDialog1.Title = "Save an Image File";
                        //    saveFileDialog1.ShowDialog();
                        //    if (saveFileDialog1.FileName != "")
                        //    {
                        //        if (attach_id != 0)
                        //        {
                        //            if (File.Exists(realfile))
                        //            {
                        //                System.IO.File.Copy(realfile, saveFileDialog1.FileName);
                        //                MessageBox.Show("Successfully Saved !!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //            }
                        //            else
                        //            {
                        //                MessageBox.Show("File Not Found", "Not Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //            }
                        //        }
                        //    }
                        //}
                        //else if (File_Type.ToLower() == ".xls" || File_Type.ToLower() == ".xlsx")
                        //{
                        //    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                        //    saveFileDialog1.Filter = "Excel Worksheets|*.xls | Excel Worksheets 2010|*.xlsx";
                        //    saveFileDialog1.Title = "Save an Image File";
                        //    saveFileDialog1.ShowDialog();
                        //    if (saveFileDialog1.FileName != "")
                        //    {
                        //        if (attach_id != 0)
                        //        {
                        //            if (File.Exists(realfile))
                        //            {
                        //                System.IO.File.Copy(realfile, saveFileDialog1.FileName);
                        //                MessageBox.Show("Successfully Saved !!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //            }
                        //            else
                        //            {
                        //                MessageBox.Show("File Not Found", "Not Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //            }
                        //        }
                        //    }
                        //}
                        //else if (File_Type.ToLower() == ".pdf")
                        //{
                            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                            saveFileDialog1.Filter = "Pdf Files|*.pdf";
                            saveFileDialog1.Title = "Save an Image File";
                            saveFileDialog1.ShowDialog();
                            if (saveFileDialog1.FileName != "")
                            {
                                if (attach_id != 0)
                                {
                                    if (File.Exists(realfile))
                                    {
                                        System.IO.File.Copy(realfile, saveFileDialog1.FileName);
                                        MessageBox.Show("Successfully Saved !!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("File Not Found", "Not Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                        //}
                    }
                    else
                    {
                        MessageBox.Show("Image Not Found", "Not Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
