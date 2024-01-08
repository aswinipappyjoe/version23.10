using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PappyjoeMVC.View
{
    public partial class Download_Excel_Format : Form
    {
        public Download_Excel_Format()
        {
            InitializeComponent();
        }

        private void btn_download_Click(object sender, EventArgs e)
        {
            if (cmb_Forms.Text == "Patients")
            {
                Patients_download();
            }
            if (cmb_Forms.Text == "Prescriptions")
            {
                Prescriptions_download();
            }
            if (cmb_Forms.Text == "Procedures")
            {
                Procedures_download();
            }
            if (cmb_Forms.Text == "Complaints")
            {
                Complaints_download();
            }
            if (cmb_Forms.Text == "Diagnosis")
            {
                Diagnosis_download();
            }
            if (cmb_Forms.Text == "Investigations")
            {
                Investigations_download();
            }
            if (cmb_Forms.Text == "Notes")
            {
                Notes_download();
            }
            if (cmb_Forms.Text == "Observations")
            {
                Observations_download();
            }
            if (cmb_Forms.Text == "")
            {
                MessageBox.Show("Please select an option", "Not Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void Patients_download()
        {
            string PathName = "";
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx;*.xls";
                saveFileDialog1.FileName = "Patients List.xlsx";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    PathName = saveFileDialog1.FileName;
                    Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                    ExcelApp.Application.Workbooks.Add(Type.Missing);
                    Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                    object misValue = System.Reflection.Missing.Value;
                    xlWorkBook = ExcelApp.Workbooks.Add(misValue);
                    ExcelApp.Cells[1, 1] = "Patient Name";
                    ExcelApp.Cells[1, 1].Font.Size = 10;
                    ExcelApp.Columns.ColumnWidth = 20;
                    ExcelApp.Cells[1, 2] = "Gender";
                    ExcelApp.Cells[1, 2].Font.Size = 10;
                    ExcelApp.Cells[1, 3] = "Age";
                    ExcelApp.Cells[1, 3].Font.Size = 10;
                    ExcelApp.Cells[1, 4] = "Mobile";
                    ExcelApp.Cells[1, 4].Font.Size = 10;
                    ExcelApp.Cells[1, 5] = "Street Address";
                    ExcelApp.Cells[1, 5].Font.Size = 10;
                    ExcelApp.Cells[1, 6] = "Locality";
                    ExcelApp.Cells[1, 6].Font.Size = 10;
                    ExcelApp.Cells[1, 7] = "File";
                    ExcelApp.Cells[1, 7].Font.Size = 10;

                    ExcelApp.ActiveWorkbook.SaveAs(PathName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, misValue,
            misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, misValue, misValue, misValue, misValue, misValue);

                    ExcelApp.ActiveWorkbook.Saved = true;
                    ExcelApp.Quit();
                    MessageBox.Show("Successfully Downloaded the Standard Excel Format", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Prescriptions_download()
        {
            string PathName = "";
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx;*.xls";
                saveFileDialog1.FileName = "Prescriptions List.xlsx";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    PathName = saveFileDialog1.FileName;
                    Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                    ExcelApp.Application.Workbooks.Add(Type.Missing);
                    Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                    object misValue = System.Reflection.Missing.Value;
                    xlWorkBook = ExcelApp.Workbooks.Add(misValue);
                    ExcelApp.Cells[1, 1] = "Name";
                    ExcelApp.Cells[1, 1].Font.Size = 10;
                    ExcelApp.Columns.ColumnWidth = 20;
                    ExcelApp.Cells[1, 2] = "Generic";
                    ExcelApp.Cells[1, 2].Font.Size = 10;
                    ExcelApp.Cells[1, 3] = "Type";
                    ExcelApp.Cells[1, 3].Font.Size = 10;
                    ExcelApp.Cells[1, 4] = "Strength";
                    ExcelApp.Cells[1, 4].Font.Size = 10;
                    ExcelApp.Cells[1, 5] = "Unit";
                    ExcelApp.Cells[1, 5].Font.Size = 10;
                    ExcelApp.Cells[1, 6] = "Instructions";
                    ExcelApp.Cells[1, 6].Font.Size = 10;

                    ExcelApp.ActiveWorkbook.SaveAs(PathName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, misValue,
            misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, misValue, misValue, misValue, misValue, misValue);

                    ExcelApp.ActiveWorkbook.Saved = true;
                    ExcelApp.Quit();
                    MessageBox.Show("Successfully Downloaded the Standard Excel Format", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Procedures_download()
        {
            string PathName = "";
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx;*.xls";
                saveFileDialog1.FileName = "Procedures List.xlsx";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    PathName = saveFileDialog1.FileName;
                    Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                    ExcelApp.Application.Workbooks.Add(Type.Missing);
                    Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                    object misValue = System.Reflection.Missing.Value;
                    xlWorkBook = ExcelApp.Workbooks.Add(misValue);
                    ExcelApp.Cells[1, 1] = "Procedure Name";
                    ExcelApp.Cells[1, 1].Font.Size = 10;
                    ExcelApp.Columns.ColumnWidth = 20;
                    ExcelApp.Cells[1, 2] = "Procedure Cost";
                    ExcelApp.Cells[1, 2].Font.Size = 10;

                    ExcelApp.ActiveWorkbook.SaveAs(PathName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, misValue,
            misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, misValue, misValue, misValue, misValue, misValue);

                    ExcelApp.ActiveWorkbook.Saved = true;
                    ExcelApp.Quit();
                    MessageBox.Show("Successfully Downloaded the Standard Excel Format", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Complaints_download()
        {
            string PathName = "";
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx;*.xls";
                saveFileDialog1.FileName = "Complaints List.xlsx";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    PathName = saveFileDialog1.FileName;
                    Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                    ExcelApp.Application.Workbooks.Add(Type.Missing);
                    Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                    object misValue = System.Reflection.Missing.Value;
                    xlWorkBook = ExcelApp.Workbooks.Add(misValue);
                    ExcelApp.Cells[1, 1] = "Complaints";
                    ExcelApp.Cells[1, 1].Font.Size = 10;
                    ExcelApp.Columns.ColumnWidth = 20;

                    ExcelApp.ActiveWorkbook.SaveAs(PathName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, misValue,
            misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, misValue, misValue, misValue, misValue, misValue);

                    ExcelApp.ActiveWorkbook.Saved = true;
                    ExcelApp.Quit();
                    MessageBox.Show("Successfully Downloaded the Standard Excel Format", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Diagnosis_download()
        {
            string PathName = "";
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx;*.xls";
                saveFileDialog1.FileName = "Diagnosis List.xlsx";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    PathName = saveFileDialog1.FileName;
                    Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                    ExcelApp.Application.Workbooks.Add(Type.Missing);
                    Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                    object misValue = System.Reflection.Missing.Value;
                    xlWorkBook = ExcelApp.Workbooks.Add(misValue);
                    ExcelApp.Cells[1, 1] = "Diagnosis";
                    ExcelApp.Cells[1, 1].Font.Size = 10;
                    ExcelApp.Columns.ColumnWidth = 20;

                    ExcelApp.ActiveWorkbook.SaveAs(PathName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, misValue,
            misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, misValue, misValue, misValue, misValue, misValue);

                    ExcelApp.ActiveWorkbook.Saved = true;
                    ExcelApp.Quit();
                    MessageBox.Show("Successfully Downloaded the Standard Excel Format", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Investigations_download()
        {
            string PathName = "";
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx;*.xls";
                saveFileDialog1.FileName = "Investigations List.xlsx";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    PathName = saveFileDialog1.FileName;
                    Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                    ExcelApp.Application.Workbooks.Add(Type.Missing);
                    Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                    object misValue = System.Reflection.Missing.Value;
                    xlWorkBook = ExcelApp.Workbooks.Add(misValue);
                    ExcelApp.Cells[1, 1] = "Investigations";
                    ExcelApp.Cells[1, 1].Font.Size = 10;
                    ExcelApp.Columns.ColumnWidth = 20;

                    ExcelApp.ActiveWorkbook.SaveAs(PathName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, misValue,
            misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, misValue, misValue, misValue, misValue, misValue);

                    ExcelApp.ActiveWorkbook.Saved = true;
                    ExcelApp.Quit();
                    MessageBox.Show("Successfully Downloaded the Standard Excel Format", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Notes_download()
        {
            string PathName = "";
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx;*.xls";
                saveFileDialog1.FileName = "Notes List.xlsx";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    PathName = saveFileDialog1.FileName;
                    Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                    ExcelApp.Application.Workbooks.Add(Type.Missing);
                    Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                    object misValue = System.Reflection.Missing.Value;
                    xlWorkBook = ExcelApp.Workbooks.Add(misValue);
                    ExcelApp.Cells[1, 1] = "Notes";
                    ExcelApp.Cells[1, 1].Font.Size = 10;
                    ExcelApp.Columns.ColumnWidth = 20;

                    ExcelApp.ActiveWorkbook.SaveAs(PathName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, misValue,
            misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, misValue, misValue, misValue, misValue, misValue);

                    ExcelApp.ActiveWorkbook.Saved = true;
                    ExcelApp.Quit();
                    MessageBox.Show("Successfully Downloaded the Standard Excel Format", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Observations_download()
        {
            string PathName = "";
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx;*.xls";
                saveFileDialog1.FileName = "Observations List.xlsx";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    PathName = saveFileDialog1.FileName;
                    Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                    ExcelApp.Application.Workbooks.Add(Type.Missing);
                    Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                    object misValue = System.Reflection.Missing.Value;
                    xlWorkBook = ExcelApp.Workbooks.Add(misValue);
                    ExcelApp.Cells[1, 1] = "Observations";
                    ExcelApp.Cells[1, 1].Font.Size = 10;
                    ExcelApp.Columns.ColumnWidth = 20;

                    ExcelApp.ActiveWorkbook.SaveAs(PathName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, misValue,
            misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, misValue, misValue, misValue, misValue, misValue);

                    ExcelApp.ActiveWorkbook.Saved = true;
                    ExcelApp.Quit();
                    MessageBox.Show("Successfully Downloaded the Standard Excel Format", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
