using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
using OfficeOpenXml;
using System.IO;
using System.Drawing;
using DMR;

namespace DMR
{
    public class EPP_ExcelManager
    {
        static ExcelRange range;
        //=======================================================
        public static void Save(DataGridView dgv, string fileName, string sheetName, bool alsoWriteColHeader, int startColumn, bool showSuccessMesage = true)
        {
            Save(dgv, fileName, sheetName, alsoWriteColHeader, startColumn, dgv.ColumnCount - 1, false, showSuccessMesage);
        }
        //-------------------------------------------------------
        public static void Save(DataGridView dgv, string fileName, string sheetName, bool alsoWriteColHeader, int startColumn, bool alsoWriteRowHeader, bool showSuccessMesage = true)
        {
            Save(dgv, fileName, sheetName, alsoWriteColHeader, startColumn, dgv.ColumnCount - 1, alsoWriteRowHeader, 0, 0, 0, 0, showSuccessMesage, null);
        }
        //-------------------------------------------------------
        public static void Save(DataGridView dgv, string fileName, string sheetName, bool alsoWriteColHeader, int startColumn, int endColumn, bool alsoWriteRowHeader, bool showSuccessMesage = true)
        {
            Save(dgv, fileName, sheetName, alsoWriteColHeader, startColumn, endColumn, alsoWriteRowHeader, 0, 0, 0, 0, showSuccessMesage, null);
        }
        //-------------------------------------------------------
        public static void Save(DataGridView dgv, string fileName, string sheetName, bool alsoWriteColHeader, int startColumn, int endColumn, bool alsoWriteRowHeader,
                        int extraDataRows, int extraDataCols, int extraDataBeginRowInFile, int extraDataBeginColInFile, bool showSuccessMesage, params  string[] extraData)
        {
            try
            {
                FileInfo newFile = new FileInfo(fileName);
                if (newFile == null) { InformationManager.Set_Info(new Errors("File error")); return; }

                if (newFile.Exists)
                {
                    newFile.Delete();  // ensures we create a new workbook
                    newFile = new FileInfo(fileName);
                }
                if (newFile == null) { InformationManager.Set_Info(new Errors("Can not create Excel file")); return; }

                ExcelPackage package = new ExcelPackage(newFile);
                if (package == null) { InformationManager.Set_Info(new Errors("Can not create Excel book")); return; }

                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(sheetName);
                if (worksheet == null) { InformationManager.Set_Info(new Errors("Can not create Excel sheet")); return; }

                int begRow = 1;
                int begColumn = 1;

                if (alsoWriteRowHeader) begColumn++;
                if (alsoWriteColHeader) begRow++;

                if (alsoWriteColHeader)
                {
                    for (int i = startColumn; i <= endColumn; i++)
                        worksheet.Cells[1, begColumn + i - startColumn].Value = dgv.Columns[i].HeaderText;
                }

                if (alsoWriteRowHeader)
                {
                    for (int i = 0; i < dgv.RowCount; i++)
                        worksheet.Cells[begRow + i, 1].Value = dgv.Rows[i].HeaderCell.Value;
                }

                for (int i = 0; i < dgv.RowCount; i++)
                    for (int j = startColumn; j <= endColumn; j++)
                        worksheet.Cells[begRow + i, begColumn + j - startColumn].Value = dgv[j, i].Value;


                if (extraData != null)
                {
                    int id = 0;

                    for (int i = 0; i < extraDataRows; i++)
                        for (int j = 0; j < extraDataCols; j++, id++)
                            worksheet.Cells[extraDataBeginRowInFile + i, extraDataBeginColInFile + j].Value = extraData[id];
                }

                package.Save();

                if (showSuccessMesage)
                    InformationManager.Set_Info("Excel file saved successfully", "Status:", "Saving");
            }
            catch (Exception ex)
            {
                InformationManager.Set_Info(new Errors("Saving error", ex.Message));
            }
        }
        //-------------------------------------------------------
        //returns true on success
        public static bool Open(ref List<List<string>> result, out int columnCount, string fileName, bool skipFirstRow)
        {
            columnCount = 0;

            try
            {
                result.Clear();

                byte[] file = File.ReadAllBytes(fileName);
                if (file == null) { InformationManager.Set_Info(new Errors("File error")); return false; }

                MemoryStream ms = new MemoryStream(file);
                if (ms == null) { InformationManager.Set_Info(new Errors("Can not load file")); return false; }

                ExcelPackage package = new ExcelPackage(ms);
                if (package == null) { InformationManager.Set_Info(new Errors("Can not load Excel book")); return false; }

                // get first workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1/*1-based*/];
                if (worksheet == null) { InformationManager.Set_Info(new Errors("Can not load Excel sheet")); return false; }

                if (package.Workbook.Worksheets.Count == 0)
                    return true;

                columnCount = worksheet.Dimension.Columns;

                int begRow = skipFirstRow ? 2 : 1;

                for (int i = begRow; i <= worksheet.Dimension.Rows; i++)
                {
                    int listID = result.Count;
                    result.Add(new List<string>());

                    for (int j = 1; j <= worksheet.Dimension.Columns; j++)
                    {
                        object val = worksheet.Cells[i + worksheet.Dimension.Start.Row - 1,
                                                     j + worksheet.Dimension.Start.Column - 1].Value;

                        if (val != null)
                            result[listID].Add(val.ToString());
                        else
                            result[listID].Add("");
                    }

                }

                return true;
            }
            catch (Exception ex)
            {
                InformationManager.Set_Info(new Errors("Loading Error", ex.Message));
                return false;
            }
        }
        //-------------------------------------------------------
        public static string SaveFileDialog()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel(*.xlsx)|*.xlsx";

            string fileName = "";

            if (sfd.ShowDialog() == DialogResult.OK)
                fileName = sfd.FileName;

            //check if other type of file is selected
            if (!fileName.ToLower().EndsWith(".xlsx"))
                fileName = "";

            sfd.Dispose();

            return fileName;
        }
        //-------------------------------------------------------
        public static string OpenFileDialog()
        {
            return OpenFileDialog("Open");
        }
        //-------------------------------------------------------
        public static string OpenFileDialog(string title)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel(*.xlsx)|*.xlsx";
            ofd.Title = title;

            string fileName = "";

            if (ofd.ShowDialog() == DialogResult.OK)
                fileName = ofd.FileName;

            //check if other type of file is selected
            if (!fileName.ToLower().EndsWith(".xlsx"))
                fileName = "";

            ofd.Dispose();

            return fileName;
        }
        //-------------------------------------------------------
        public static ExcelRange GetRange(ExcelWorksheet sheet, int r1, int c1, int r2, int c2)
        {
            return sheet.Cells[r1, c1, r2, c2];
        }
        //-------------------------------------------------------
        public static void AddComment(ExcelWorksheet sheet, int r1, int c1, int r2, int c2, string comment)
        {
            range = sheet.Cells[r1, c1, r2, c2];
            range.AddComment(comment, "");
        }
        //-------------------------------------------------------
        public static void AddComment(ExcelWorksheet sheet, ExcelRange range, string comment)
        {
            range.AddComment(comment, "");
        }
        //-------------------------------------------------------
        public static void SetBackColor(ExcelWorksheet sheet, int r1, int c1, int r2, int c2, Color color)
        {
            range = sheet.Cells[r1, c1, r2, c2];
            range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;//throws an exception if it is not set
            range.Style.Fill.BackgroundColor.SetColor(color);
        }
        //-------------------------------------------------------
        public static void SetBackColor(ExcelWorksheet sheet, ExcelRange range, Color color)
        {
            range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;//throws an exception if it is not set
            range.Style.Fill.BackgroundColor.SetColor(color);
        }
        //-------------------------------------------------------
        public static void SetBackColor_AlternatingRows(ExcelWorksheet sheet, int r1, int c1, int r2, int c2, Color color1, Color color2, bool startWithColor1)
        {
            for (int i = r1; i <= r2; i++)
            {
                range = sheet.Cells[i, c1, i, c2];

                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;//throws an exception if it is not set

                if (startWithColor1)
                    range.Style.Fill.BackgroundColor.SetColor(color1);
                else
                    range.Style.Fill.BackgroundColor.SetColor(color2);

                startWithColor1 = !startWithColor1;
            }
        }
        //-------------------------------------------------------
        public static void SetBackColor_AlternatingRows(ExcelWorksheet sheet, int r1, int c1, int r2, int c2, Color color, bool startWithColor)
        {
            //other color = none

            for (int i = r1; i <= r2; i++)
            {
                range = sheet.Cells[i, c1, i, c2];
                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;//throws an exception if it is not set

                if (startWithColor)
                    range.Style.Fill.BackgroundColor.SetColor(color);

                startWithColor = !startWithColor;
            }
        }
        //-------------------------------------------------------
        public static void SetForeColor(ExcelWorksheet sheet, int r1, int c1, int r2, int c2, Color color)
        {
            range = sheet.Cells[r1, c1, r2, c2];
            range.Style.Font.Color.SetColor(color);
        }
        //-------------------------------------------------------
        public static void SetForeColor(ExcelWorksheet sheet, ExcelRange range, Color color)
        {
            range.Style.Font.Color.SetColor(color);
        }
        //-------------------------------------------------------
        public static void SetFont(ExcelWorksheet sheet, int r1, int c1, int r2, int c2, int size, string name, bool bold, bool italic)
        {
            range = sheet.Cells[r1, c1, r2, c2];

            range.Style.Font.Name = name;
            range.Style.Font.Size = size;
            range.Style.Font.Bold = bold;
            range.Style.Font.Italic = italic;
        }
        //-------------------------------------------------------
        public static void SetFont(ExcelWorksheet sheet, ExcelRange range, int size, string name, bool bold, bool italic)
        {
            range.Style.Font.Name = name;
            range.Style.Font.Size = size;
            range.Style.Font.Bold = bold;
            range.Style.Font.Italic = italic;
        }
        //-------------------------------------------------------
        public static void Merge(ExcelWorksheet sheet, int r1, int c1, int r2, int c2)
        {
            range = sheet.Cells[r1, c1, r2, c2];

            if (!range.Merge)
                range.Merge = true;
        }
        //-------------------------------------------------------
        public static void Merge(ExcelWorksheet sheet, ExcelRange range)
        {
            if (!range.Merge)
                range.Merge = true;
        }
        //-------------------------------------------------------
        public static void MergeAndPrepare_MediumBoxBorder(ExcelWorksheet sheet, int r1, int c1, int r2, int c2, string value,
                                            Color bkColor, Color foreColor, bool alignLeft, int angle)
        {
            range = sheet.Cells[r1, c1, r2, c2];

            Merge(sheet, range);
            SetText(sheet, range, value);
            SetBackColor(sheet, range, bkColor);
            SetForeColor(sheet, range, foreColor);

            SetAlignment(sheet, range,
                alignLeft ? OfficeOpenXml.Style.ExcelHorizontalAlignment.Left : OfficeOpenXml.Style.ExcelHorizontalAlignment.Center,
                OfficeOpenXml.Style.ExcelVerticalAlignment.Center);

            SetOutBorder_MediumLine(sheet, range);

            SetTextDirection(sheet, range, angle);
        }
        //-------------------------------------------------------
        public static void MergeAndPrepare(ExcelWorksheet sheet, int r1, int c1, int r2, int c2, string value,
                                            Color bkColor, Color foreColor, bool alignLeft, int angle)
        {
            range = sheet.Cells[r1, c1, r2, c2];
            Merge(sheet, range);

            SetText(sheet, range, value);
            SetBackColor(sheet, range, bkColor);
            SetForeColor(sheet, range, foreColor);

            SetAlignment(sheet, range,
                alignLeft ? OfficeOpenXml.Style.ExcelHorizontalAlignment.Left : OfficeOpenXml.Style.ExcelHorizontalAlignment.Center,
                OfficeOpenXml.Style.ExcelVerticalAlignment.Center);

            SetTextDirection(sheet, range, angle);
        }
        //-------------------------------------------------------
        public static void MergeAndPrepare(ExcelWorksheet sheet, int r1, int c1, int r2, int c2, string value,
                                            Color bkColor, Color foreColor)
        {
            range = sheet.Cells[r1, c1, r2, c2];
            Merge(sheet, range);

            SetText(sheet, range, value);
            SetBackColor(sheet, range, bkColor);
            SetForeColor(sheet, range, foreColor);
        }
        //-------------------------------------------------------
        public static void MergeAndSet(ExcelWorksheet sheet, int r1, int c1, int r2, int c2, string value)
        {
            range = sheet.Cells[r1, c1, r2, c2];
            Merge(sheet, range);

            SetText(sheet, range, value);
        }
        //-------------------------------------------------------
        public static void MergeAndSet(ExcelWorksheet sheet, ExcelRange range, string value)
        {
            Merge(sheet, range);

            SetText(sheet, range, value);
        }
        //-------------------------------------------------------
        public static void MergeAndSet(ExcelWorksheet sheet, int r1, int c1, int r2, int c2, decimal value)
        {
            ExcelRange range = sheet.Cells[r1, c1, r2, c2];
            Merge(sheet, range);

            SetDecimalValue(sheet, range.Start.Row, range.Start.Column, value);
        }
        //-------------------------------------------------------
        public static void MergeAndSet(ExcelWorksheet sheet, ExcelRange range, decimal value)
        {
            Merge(sheet, range);

            SetDecimalValue(sheet, range.Start.Row, range.Start.Column, value);
        }
        //-------------------------------------------------------
        public static void Prepare(ExcelWorksheet sheet, int r1, int c1, int r2, int c2, string value,
                                            Color bkColor, Color foreColor, bool alignLeft)
        {
            range = sheet.Cells[r1, c1, r2, c2];

            SetText(sheet, range, value);
            SetBackColor(sheet, range, bkColor);
            SetForeColor(sheet, range, foreColor);

            SetAlignment(sheet, range,
                alignLeft ? OfficeOpenXml.Style.ExcelHorizontalAlignment.Left : OfficeOpenXml.Style.ExcelHorizontalAlignment.Center,
                OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
        }
        //-------------------------------------------------------
        public static void Prepare(ExcelWorksheet sheet, int r1, int c1, int r2, int c2,
                                            Color bkColor, Color foreColor, bool alignLeft)
        {
            range = sheet.Cells[r1, c1, r2, c2];

            SetBackColor(sheet, range, bkColor);
            SetForeColor(sheet, range, foreColor);

            SetAlignment(sheet, range,
                alignLeft ? OfficeOpenXml.Style.ExcelHorizontalAlignment.Left : OfficeOpenXml.Style.ExcelHorizontalAlignment.Center,
                OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
        }
        //-------------------------------------------------------
        public static void Prepare_ThinInOutBorder(ExcelWorksheet sheet, int r1, int c1, int r2, int c2, string value,
                                            Color bkColor, Color foreColor, bool alignLeft, int angle)
        {
            range = sheet.Cells[r1, c1, r2, c2];

            SetText(sheet, range, value);
            SetBackColor(sheet, range, bkColor);
            SetForeColor(sheet, range, foreColor);

            SetAlignment(sheet, range,
                alignLeft ? OfficeOpenXml.Style.ExcelHorizontalAlignment.Left : OfficeOpenXml.Style.ExcelHorizontalAlignment.Center,
                OfficeOpenXml.Style.ExcelVerticalAlignment.Center);

            SetInBorder(sheet, range, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            SetOutBorder(sheet, range, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);

            SetTextDirection(sheet, range, angle);
        }
        //-------------------------------------------------------
        public static void SetOutBorder_ThinLine(ExcelWorksheet sheet, int r1, int c1, int r2, int c2)
        {
            SetOutBorder(sheet, r1, c1, r2, c2,
                Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        }
        //-------------------------------------------------------
        public static void SetOutBorder_ThinLine(ExcelWorksheet sheet, ExcelRange range)
        {
            SetOutBorder(sheet, range,
                Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        }
        //-------------------------------------------------------
        public static void SetOutBorder_MediumLine(ExcelWorksheet sheet, int r1, int c1, int r2, int c2)
        {
            SetOutBorder(sheet, r1, c1, r2, c2,
                Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Medium);
        }
        //-------------------------------------------------------
        public static void SetOutBorder_MediumLine(ExcelWorksheet sheet, ExcelRange range)
        {
            SetOutBorder(sheet, range,
                Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Medium);
        }
        //-------------------------------------------------------
        public static void SetOutBorder_ThickLine(ExcelWorksheet sheet, int r1, int c1, int r2, int c2)
        {
            SetOutBorder(sheet, r1, c1, r2, c2,
                Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thick);
        }
        //-------------------------------------------------------
        public static void SetOutBorder_ThickLine(ExcelWorksheet sheet, ExcelRange range)
        {
            SetOutBorder(sheet, range,
                Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thick);
        }
        //-------------------------------------------------------
        public static void SetOutBorder(ExcelWorksheet sheet, int r1, int c1, int r2, int c2/*, Excel.XlBorderWeight weight*/, Color color, OfficeOpenXml.Style.ExcelBorderStyle style)
        {
            range = sheet.Cells[r1, c1, r2, c2];
            range.Style.Border.BorderAround(style, color);
        }
        //-------------------------------------------------------
        public static void SetOutBorder(ExcelWorksheet sheet, ExcelRange range/*, Excel.XlBorderWeight weight*/, Color color, OfficeOpenXml.Style.ExcelBorderStyle style)
        {
            range.Style.Border.BorderAround(style, color);
        }
        //-------------------------------------------------------
        public static void SetInBorder(ExcelWorksheet sheet, int r1, int c1, int r2, int c2/*, Excel.XlBorderWeight weight*/, Color color, OfficeOpenXml.Style.ExcelBorderStyle style)
        {
            range = sheet.Cells[r1, c1, r2, c2];

            //***************
            range.Style.Border.Left.Style = style;
            range.Style.Border.Left.Color.SetColor(color);

            //***************
            range.Style.Border.Top.Style = style;
            range.Style.Border.Top.Color.SetColor(color);

            //***************
            range.Style.Border.Right.Style = style;
            range.Style.Border.Right.Color.SetColor(color);

            //***************
            range.Style.Border.Bottom.Style = style;
            range.Style.Border.Bottom.Color.SetColor(color);
        }
        //-------------------------------------------------------
        public static void SetInBorder(ExcelWorksheet sheet, ExcelRange range/*, Excel.XlBorderWeight weight*/, Color color, OfficeOpenXml.Style.ExcelBorderStyle style)
        {
            //***************
            range.Style.Border.Left.Style = style;
            range.Style.Border.Left.Color.SetColor(color);

            //***************
            range.Style.Border.Top.Style = style;
            range.Style.Border.Top.Color.SetColor(color);

            //***************
            range.Style.Border.Right.Style = style;
            range.Style.Border.Right.Color.SetColor(color);

            //***************
            range.Style.Border.Bottom.Style = style;
            range.Style.Border.Bottom.Color.SetColor(color);
        }
        //-------------------------------------------------------
        public static void SetFormula(ExcelWorksheet sheet, int r1, int c1, int r2, int c2, string formula/*without =*/)
        {
            range = sheet.Cells[r1, c1, r2, c2];

            range.Formula = "=" + formula;
        }
        //-------------------------------------------------------
        public static void SetFormula(ExcelWorksheet sheet, ExcelRange range, string formula/*without =*/)
        {
            range.Formula = "=" + formula;
        }
        ////-------------------------------------------------------
        //public static string GetText(ExcelWorksheet sheet, int r1, int c1, int r2, int c2)
        //{
        //    range = sheet.Cells[r1, c1, r2, c2];

        //    return range.Value.ToString();
        //}
        //-------------------------------------------------------
        public static void SetText(ExcelWorksheet sheet, int r1, int c1, int r2, int c2, string value)
        {
            range = sheet.Cells[r1, c1, r2, c2];

            range.Style.Numberformat.Format = "@";//text fromat
            range.Value = value;
        }
        //-------------------------------------------------------
        public static void SetText(ExcelWorksheet sheet, int r, int c, string value)
        {
            range = sheet.Cells[r, c, r, c];

            range.Style.Numberformat.Format = "@";//text fromat
            range.Value = value;
        }
        //-------------------------------------------------------
        public static void SetText(ExcelWorksheet sheet, ExcelRange range, string value)
        {
            range.Style.Numberformat.Format = "@";//text fromat
            range.Value = value;
        }
        //-------------------------------------------------------
        //inserting array of values is much more faster than one by one
        public static void SetText(ExcelWorksheet sheet, int r1, int c1, int r2, int c2, object[] value)
        {
            range = sheet.Cells[r1, c1, r2, c2];

            range.Style.Numberformat.Format = "@";//text fromat

            int id = 0;
            for (int i = r1; i <= r2; i++)
                for (int j = c1; j <= c2; j++, id++)
                    range[i, j].Value = value[id];
        }
        //-------------------------------------------------------
        public static void SetDecimalValue(ExcelWorksheet sheet, int r1, int c1, int r2, int c2, object value)
        {
            range = sheet.Cells[r1, c1, r2, c2];

            range.Style.Numberformat.Format = "0.###";//decimal(n, 3)

            for (int i = r1; i <= r2; i++)
                for (int j = c1; j <= c2; j++)
                    range[i, j].Value = value;
        }
        //-------------------------------------------------------
        public static void SetDecimalValue(ExcelWorksheet sheet, int r, int c, object value)
        {
            range = sheet.Cells[r, c, r, c];
            range.Style.Numberformat.Format = "0.###";//decimal(n, 3)
            range.Value = value;
        }
        //-------------------------------------------------------
        public static void SetDecimalValue(ExcelWorksheet sheet, int r1, int c1, int r2, int c2, object[] value)
        {
            range = sheet.Cells[r1, c1, r2, c2];

            range.Style.Numberformat.Format = "0.###";//decimal(n, 3)

            int id = 0;
            for (int i = r1; i <= r2; i++)
                for (int j = c1; j <= c2; j++, id++)
                    range[i, j].Value = value[id];
        }
        //-------------------------------------------------------
        public static void SetDecimalValue(ExcelWorksheet sheet, int r1, int c1, int r2, int c2, object[,] value)
        {
            range = sheet.Cells[r1, c1, r2, c2];

            range.Style.Numberformat.Format = "0.###";//decimal(n, 3)

            for (int i = r1; i <= r2; i++)
                for (int j = c1; j <= c2; j++)
                    range[i, j].Value = value[i - r1, j - c1];
        }
        //-------------------------------------------------------
        public static void SetText(ExcelWorksheet sheet, int r1, int c1, int r2, int c2, object value)
        {
            range = sheet.Cells[r1, c1, r2, c2];

            range.Style.Numberformat.Format = "@";//text fromat
            range.Value = value;
        }
        //-------------------------------------------------------
        public static void SetText(ExcelWorksheet sheet, int r, int c, object value)
        {
            range = sheet.Cells[r, c, r, c];

            range.Style.Numberformat.Format = "@";//text fromat
            range.Value = value;
        }
        //-------------------------------------------------------
        public static void SetAlignment(ExcelWorksheet sheet, int r1, int c1, int r2, int c2, OfficeOpenXml.Style.ExcelHorizontalAlignment hor, OfficeOpenXml.Style.ExcelVerticalAlignment vert)
        {
            range = sheet.Cells[r1, c1, r2, c2];

            range.Style.HorizontalAlignment = hor;
            range.Style.VerticalAlignment = vert;
        }
        //-------------------------------------------------------
        public static void SetAlignment(ExcelWorksheet sheet, ExcelRange range, OfficeOpenXml.Style.ExcelHorizontalAlignment hor, OfficeOpenXml.Style.ExcelVerticalAlignment vert)
        {
            range.Style.HorizontalAlignment = hor;
            range.Style.VerticalAlignment = vert;
        }
        //-------------------------------------------------------
        public static void SetHeight(ExcelWorksheet sheet, int r1, int r2, float height/*logical*/)
        {
            for (int i = r1; i <= r2; i++)
                sheet.Row(i).Height = height;
        }
        //-------------------------------------------------------
        public static void SetHeight(ExcelWorksheet sheet, int r, float height/*logical*/)
        {
            SetHeight(sheet, r, r, height);
        }
        //-------------------------------------------------------
        public static void SetWidth(ExcelWorksheet sheet, int c1, int c2, float width/*logical*/)
        {
            for (int i = c1; i <= c2; i++)
                sheet.Column(i).Width = width;
        }
        //-------------------------------------------------------
        public static void SetWidth(ExcelWorksheet sheet, int c, float width/*logical*/)
        {
            SetWidth(sheet, c, c, width);
        }
        //-------------------------------------------------------
        public static void SetSize(ExcelWorksheet sheet, int r1, int c1, int r2, int c2, float width/*logical*/, float height/*logical*/)
        {
            for (int i = r1; i <= r2; i++)
                sheet.Row(i).Height = height;

            for (int i = c1; i <= c2; i++)
                sheet.Column(i).Width = width;
        }
        //-------------------------------------------------------
        public static void SetSize(ExcelWorksheet sheet, ExcelRange range, float width/*logical*/, float height/*logical*/)
        {
            for (int i = range.Start.Row; i <= range.End.Row; i++)
                sheet.Row(i).Height = height;

            for (int i = range.Start.Column; i <= range.End.Column; i++)
                sheet.Column(i).Width = width;
        }
        //-------------------------------------------------------
        public static void SetTextDirection(ExcelWorksheet sheet, int r1, int c1, int r2, int c2, int angle/*in Degrees : -90<->90*/)
        {
            //angle in EPPlus
            //
            //       input             apply
            //     _________          ________
            //      0<->90             0<->90
            //      91<->180          -1<->-90
            //        Else              Invalid

            //Map angle to range 0<->180
            if (angle < -90) angle = -90;
            if (angle > 90) angle = 90;
            if (angle < 0) angle = 90 - angle;

            range = sheet.Cells[r1, c1, r2, c2];
            range.Style.TextRotation = angle;
        }
        //-------------------------------------------------------
        public static void SetTextDirection(ExcelWorksheet sheet, ExcelRange range, int angle/*in Degrees : -90<->90*/)
        {
            //angle in EPPlus
            //
            //       EPPlus           our input angle
            //     _________          ________
            //      0<->90             0<->90
            //      91<->180          -1<->-90
            //        Else              Invalid

            //Map angle to range 0<->180
            if (angle < -90) angle = -90;
            if (angle > 90) angle = 90;
            if (angle < 0) angle = 90 - angle;

            range.Style.TextRotation = angle;
        }
        //-------------------------------------------------------
        public static void SetTextWrap(ExcelWorksheet sheet, int r1, int c1, int r2, int c2, bool wrap)
        {
            range = sheet.Cells[r1, c1, r2, c2];

            range.Style.WrapText = wrap;
        }
        //-------------------------------------------------------
        public static void SetTextWrap(ExcelWorksheet sheet, ExcelRange range, bool wrap)
        {
            range.Style.WrapText = wrap;
        }
        //-------------------------------------------------------
        public static void SetImage(ExcelWorksheet sheet, int l, int t, int w, int h, string imageFileName, string objectNameInFile)
        {
            var picture = sheet.Drawings.AddPicture(objectNameInFile, Image.FromFile(imageFileName));
            picture.SetPosition(t, l);
            picture.SetSize(w, h);
        }
        //-------------------------------------------------------
        public static void SetImage(ExcelWorksheet sheet, int l, int t, int w, int h, Image img, string objectNameInFile)
        {
            var picture = sheet.Drawings.AddPicture(objectNameInFile, img);
            picture.SetPosition(t, l);
            picture.SetSize(w, h);
        }
        //-------------------------------------------------------
        public static void SetColumnsFreez(ExcelWorksheet sheet, int c)
        {
            sheet.View.FreezePanes(1, c + 1);//all rows before 1, all columns before c+1
        }
        //-------------------------------------------------------
        public static void SetRowsFreez(ExcelWorksheet sheet, int r)
        {
            sheet.View.FreezePanes(r + 1, 1);//all rows before r+1, all columns before 1
        }
        //-------------------------------------------------------
        public static Color LighterColor(Color c)
        {
            int dr = 255 - c.R;
            int dg = 255 - c.G;
            int db = 255 - c.B;

            float factor = 0.5f;

            return Color.FromArgb((int)(c.R + dr * factor),
                (int)(c.G + dg * factor),
                (int)(c.B + db * factor));
        }
        //-------------------------------------------------------
        public static Color DarkerColor(Color c)
        {
            float factor = 0.5f;

            return Color.FromArgb((int)(c.R * factor),
                (int)(c.G * factor),
                (int)(c.B * factor));
        }
        //-------------------------------------------------------
        public static ExcelRange UnionRanges(ExcelWorksheet sheet, ExcelRange r1, ExcelRange r2)
        {
            return sheet.Cells[range.FullAddress + "," + r2.FullAddress];
        }
    }

    //-------------------------------------------------------

    public class ExcelRange_Name_EPP
    {
        public ExcelRange range = null;
        public string name = "";
    }
}
