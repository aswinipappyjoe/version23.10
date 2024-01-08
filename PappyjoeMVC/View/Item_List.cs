using PappyjoeMVC.Controller;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PappyjoeMVC.View
{
    public partial class Item_List : Form
    {
        public static bool Item_flag = false;
        Item_List_Controller cntrl = new Item_List_Controller();
        Supplier suplier = new Supplier();
        Manufacture manufacture = new Manufacture();
        Choose_Consumables_Pharmacy consume = new Choose_Consumables_Pharmacy();
        public string doctor_id = "", pat_id = "";
        public Item_List()
        {
            InitializeComponent();
        }

        public Item_List(string formname)
        {
            InitializeComponent();
            this.formname = formname;
        }
        private void FrmItemList_Load(object sender, EventArgs e)
        {
            DataTable dtb = cntrl.Get_CompanyNAme();
            if (dtb.Rows.Count > 0)
            {
                string clinicn = "";
                clinicn = dtb.Rows[0][0].ToString();
            }
            string dt_doctor = cntrl.Get_DoctorName(doctor_id);
            btn_ItemList.BackColor = Color.SteelBlue;
            string manufacture = Cmb_Manufacture.SelectedIndex.ToString();
            DataTable dt_items = this.cntrl.Fill_Grid("Pharmacy");
            Fill_Grid(dt_items);
            DataTable dt_c = this.cntrl.Fill_Grid_totalcount("Pharmacy");
            if(dt_c.Rows.Count>50)
            {
                lb_showMore.Visible = true;
            }
            else
            {
                lb_showMore.Visible = false;
            }
            Dgv_Product.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            Dgv_Product.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            Dgv_Product.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sego UI", 9, FontStyle.Regular);
            Dgv_Product.EnableHeadersVisualStyles = false;
            DataTable dt = this.cntrl.Fill_manufactureCombo();
            Fill_ManufactureCombo(dt);
        }
        public void Fill_ManufactureCombo(DataTable gp_rs)
        {
            Cmb_Manufacture.Items.Clear();
            Cmb_Manufacture.Items.Add("All Manufacture");
            Cmb_Manufacture.ValueMember = "0";
            Cmb_Manufacture.DisplayMember = "All Manufacture";
            if (gp_rs.Rows.Count > 0)
            {

                for (int i = 0; i < gp_rs.Rows.Count; i++)
                {
                    Cmb_Manufacture.Items.Add(gp_rs.Rows[i]["manufacturer"].ToString());
                    Cmb_Manufacture.ValueMember = gp_rs.Rows[i]["id"].ToString();
                    Cmb_Manufacture.DisplayMember = gp_rs.Rows[i]["manufacturer"].ToString();
                }
                Cmb_Manufacture.SelectedIndex = 0;
            }
        }

        public void Fill_Grid(DataTable dtb)
        {
            if (dtb.Rows.Count > 0)
            {
                int k = 1;
                Dgv_Product.RowCount = 0;
                for (int i = 0; i < dtb.Rows.Count; i++)
                {
                    Dgv_Product.Rows.Add();
                    Dgv_Product.Rows[i].Cells["id"].Value = dtb.Rows[i]["id"].ToString();
                    Dgv_Product.Rows[i].Cells["slno"].Value =k;
                    Dgv_Product.Rows[i].Cells["Colid"].Value = dtb.Rows[i]["item_code"].ToString();
                    Dgv_Product.Rows[i].Cells["Colname"].Value = dtb.Rows[i]["item_name"].ToString();
                    if(dtb.Rows[i]["Stock"].ToString()=="")
                    {
                        Dgv_Product.Rows[i].Cells["colStock"].Value = "0";
                    }
                    else
                    {
                        Dgv_Product.Rows[i].Cells["colStock"].Value = dtb.Rows[i]["Stock"].ToString();
                    }
                    Dgv_Product.Rows[i].Cells["ColEdit"].Value = PappyjoeMVC.Properties.Resources.editicon;
                    Dgv_Product.Rows[i].Cells["ColDelete"].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                    k++;
                }
            }
            else
            {
                Dgv_Product.Rows.Clear();
            }
        }
        public void Fill_Grid_showmore(DataTable dtb)
        {
            if (dtb.Rows.Count > 0)
            {
               
                int row = Dgv_Product.Rows.Count;
                int k =Convert.ToInt32( Dgv_Product.Rows[row-1].Cells["slno"].Value.ToString())+1;
                for (int i = 0; i < dtb.Rows.Count; i++)
                {
                    Dgv_Product.Rows.Add();
                    Dgv_Product.Rows[row].Cells["id"].Value = dtb.Rows[i]["id"].ToString();
                    Dgv_Product.Rows[row].Cells["slno"].Value = k;
                    Dgv_Product.Rows[row].Cells["Colid"].Value = dtb.Rows[i]["item_code"].ToString();
                    Dgv_Product.Rows[row].Cells["Colname"].Value = dtb.Rows[i]["item_name"].ToString();
                    if (dtb.Rows[i]["Stock"].ToString() == "")
                    {
                        Dgv_Product.Rows[row].Cells["colStock"].Value = "0";

                    }
                    else
                    {
                        Dgv_Product.Rows[row].Cells["colStock"].Value = dtb.Rows[i]["Stock"].ToString();

                    }
                    Dgv_Product.Rows[row].Cells["ColEdit"].Value = PappyjoeMVC.Properties.Resources.editicon;
                    Dgv_Product.Rows[row].Cells["ColDelete"].Value = PappyjoeMVC.Properties.Resources.deleteicon;
                    k++; row++;
                }
            }
            else
            {
                lb_showMore.Visible = false;
              
            }
        }
        private void btn_ItemList_Click(object sender, EventArgs e)
        {
            btn_ItemList.BackColor = Color.SteelBlue;
            btn_Manufacture.BackColor = Color.DodgerBlue;
            btnSuplier.BackColor = Color.DodgerBlue;
            button6.BackColor = Color.DodgerBlue;
            chk_consume.Checked = false;
            manufacture.Hide();
            suplier.Hide();consume.Hide();
            DataTable dt = this.cntrl.Fill_manufactureCombo();
            Fill_ManufactureCombo(dt);
            DataTable dt_items = this.cntrl.Fill_Grid("Pharmacy");
            Fill_Grid(dt_items);
        }

        private void Cmb_Manufacture_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb_Manufacture.SelectedIndex > 0)
            {
                lb_showMore.Visible = false;
                string selectedValue;
                selectedValue = Cmb_Manufacture.Text;
                DataTable dtb = this.cntrl.Get_manufacturename(selectedValue);
                Get_manufacturename(dtb);
            }
            else
            {
                if(chk_consume.Checked==true)
                {
                    lb_showMore.Visible = false;
                    DataTable dt_items = this.cntrl.Fill_Grid("Consumable");
                    Fill_Grid(dt_items);
                }
                else
                {
                    DataTable dt_items = this.cntrl.Fill_Grid("Pharmacy");
                    Fill_Grid(dt_items);
                    DataTable dt_c = this.cntrl.Fill_Grid_totalcount("Pharmacy");
                    if (dt_c.Rows.Count > 50)
                    {
                        lb_showMore.Visible = true;
                    }
                    else
                    {
                        lb_showMore.Visible = false;
                    }
                }
               
            }
        }

        public void Get_manufacturename(DataTable dt_manu)
        {
            if (dt_manu.Rows.Count > 0)
            {
                DataTable dtb = this.cntrl.get_items_with_manufacture(Convert.ToInt32(dt_manu.Rows[0][0].ToString()));
                Fill_Grid(dtb);
            }
        }
        public static bool search_flag = false;
        private string formname;

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            search_flag = true;
            if (txt_search.Text != "")
            {
                lb_showMore.Visible = false;
                if (Cmb_Manufacture.SelectedIndex <= 0)
                {
                    if(chk_consume.Checked==true)
                    {
                        DataTable dtb = this.cntrl.Search(txt_search.Text,"Consumable");
                        Fill_Grid(dtb);
                    }
                    else
                    {
                        DataTable dtb = this.cntrl.Search(txt_search.Text,"Pharmacy");
                        Fill_Grid(dtb);
                    }
                    
                }
                else
                {
                    string manufactr = Cmb_Manufacture.Text;
                    if (manufactr != "")
                    {
                        DataTable dt_manu = this.cntrl.manufactureName(manufactr);
                        DataTable dt = new DataTable();
                        if (chk_consume.Checked==true)
                        {
                           dt = this.cntrl.Search_wit_manufacture(txt_search.Text, dt_manu.Rows[0][0].ToString(),"Consumable");

                        }
                        else
                        {
                             dt = this.cntrl.Search_wit_manufacture(txt_search.Text, dt_manu.Rows[0][0].ToString(), "Pharmacy");

                        }
                        Fill_Grid(dt);
                    }
                }
            }
            else
            {
                if (Cmb_Manufacture.SelectedIndex <= 0)
                {
                    DataTable dt_c = new DataTable();
                    if (chk_consume.Checked == true)
                    {
                        dt_c = this.cntrl.Fill_Grid_totalcount("Consumable");
                        DataTable dt_items = this.cntrl.Fill_Grid("Consumable");
                        Fill_Grid(dt_items);
                    }
                    else
                    {
                         dt_c = this.cntrl.Fill_Grid_totalcount("Pharmacy");
                        DataTable dt_items = this.cntrl.Fill_Grid("Pharmacy");
                        Fill_Grid(dt_items);
                    }
                  
                    if (dt_c.Rows.Count > 50)
                    {
                        lb_showMore.Visible = true;
                    }
                    else
                    {
                        lb_showMore.Visible = false;
                    }
                }
            }
        }

        private void btn_AddNewItem_Click(object sender, EventArgs e)
        {
            var form2 = new Add_Drug();
            form2.ShowDialog();
            form2.Dispose();
        }

        private void btn_Manufacture_Click(object sender, EventArgs e)
        {
            btn_Manufacture.BackColor = Color.SteelBlue;
            btn_ItemList.BackColor = Color.DodgerBlue;
            btnSuplier.BackColor = Color.DodgerBlue;
            button6.BackColor = Color.DodgerBlue;
            suplier.Hide();consume.Hide();
            manufacture.Close();
            if (manufacture == null || manufacture.IsDisposed)
                manufacture = new Manufacture();
            manufacture.TopLevel = false;
            manufacture.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            manufacture.Show();
        }

        private void btnSuplier_Click(object sender, EventArgs e)
        {
            btnSuplier.BackColor = Color.SteelBlue;
            btn_ItemList.BackColor = Color.DodgerBlue;
            btn_Manufacture.BackColor = Color.DodgerBlue;
            button6.BackColor = Color.DodgerBlue;
            manufacture.Hide(); suplier.Hide();
            suplier.Close();consume.Hide();
            if (suplier == null || suplier.IsDisposed)
                suplier = new Supplier();
            suplier.TopLevel = false;
            suplier.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            suplier.Show();
        }

        private void Dgv_Product_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (Dgv_Product.Rows.Count > 0)
                {
                    string itemcode = Dgv_Product.CurrentRow.Cells["id"].Value.ToString();
                    if (Dgv_Product.CurrentCell.OwningColumn.Name == "ColEdit")
                    {
                        DataTable dtb = this.cntrl.Get_itemDetails(itemcode);
                        if (dtb.Rows.Count > 0)
                        {
                            var form2 = new Add_Drug(dtb);
                            form2.ShowDialog();
                            form2.Dispose();
                        }
                    }
                    else if (Dgv_Product.CurrentCell.OwningColumn.Name == "ColDelete")
                    {
                        int i = 0;int j = 0;
                        DialogResult res = MessageBox.Show("Are you sure you want to delete..?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (res == DialogResult.Yes)
                        {
                            int index = Dgv_Product.CurrentRow.Index;
                            DataTable dt = this.cntrl.get_stock(itemcode);
                            if (dt.Rows[0][0].ToString() != "")
                            {
                                if (Convert.ToDecimal(dt.Rows[0][0].ToString()) > 0)
                                {
                                    MessageBox.Show("Item have stock", "Can't Delete..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                else if (dt.Rows[0][0].ToString() == "0.00")
                                {
                                    MessageBox.Show("Item is being used by another inventory module", "Can't Delete..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                            else
                            {
                                i = this.cntrl.delete(itemcode);
                                j = this.cntrl.delete_batchwithNoitem(itemcode);
                                if (i > 0 || j > 0)
                                {
                                    Dgv_Product.Rows.RemoveAt(index);
                                    if(chk_consume.Checked==true)
                                    {
                                        DataTable dtb = this.cntrl.Fill_Grid("Consumable");
                                        Fill_Grid(dtb);
                                    }
                                    else
                                    {
                                        DataTable dtb = this.cntrl.Fill_Grid("Pharmacy");
                                        Fill_Grid(dtb);
                                    }
                                   
                                }
                            }
                        }
                    }
                }
            }
        }

        private void FrmItemList_Activated(object sender, EventArgs e)
        {
            if (search_flag == false)
            {
                DataTable dtb = this.cntrl.Fill_Grid("Pharmacy");
                Fill_Grid(dtb);
            }
        }

        public void openform(Form myForm)
        {
            Panel p = this.Parent as Panel;
            if (p != null)
            {
                myForm.FormBorderStyle = FormBorderStyle.None;
                myForm.TopLevel = false;
                myForm.Dock = DockStyle.Fill;
                p.Controls.Add(myForm);
                myForm.Show();
                this.Close();
            }
        }

        private void Cmb_Manufacture_Click(object sender, EventArgs e)
        {
            DataTable dt = this.cntrl.Fill_manufacture();
            Fill_ManufactureCombo(dt);
        }
        public int row_count = 0;
        private void lb_showMore_Click(object sender, EventArgs e)
        {
            int count = row_count + 20;
            DataTable dtb = this.cntrl.Fill_Grid_scroll(count);
            Fill_Grid_showmore(dtb);
            row_count = count;
        }

        private void panel_main_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chk_consume_CheckedChanged(object sender, EventArgs e)
        {
            if(chk_consume.Checked==true)
            {
                lb_showMore.Visible = false;
                DataTable dt_items = this.cntrl.Fill_Grid("Consumable");
                Fill_Grid(dt_items);
            }
            else
            {
                DataTable dt_items = this.cntrl.Fill_Grid("Pharmacy");
                Fill_Grid(dt_items);
                DataTable dt_c = this.cntrl.Fill_Grid_totalcount("Pharmacy");
                if (dt_c.Rows.Count > 50)
                {
                    lb_showMore.Visible = true;
                }
                else
                {
                    lb_showMore.Visible = false;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button6.BackColor = Color.SteelBlue;
            btn_ItemList.BackColor = Color.DodgerBlue;
            btn_Manufacture.BackColor = Color.DodgerBlue;
            btnSuplier.BackColor = Color.DodgerBlue;
            suplier.Hide();
            manufacture.Close();
            if (consume == null || consume.IsDisposed)
                consume = new Choose_Consumables_Pharmacy();
            consume.TopLevel = false;
            consume.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            consume.Show();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            var form2 = new Practice_Details();
            form2.doctor_id = doctor_id;
            openform(form2);
        }

    }
}

