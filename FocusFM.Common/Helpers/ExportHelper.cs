using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;
using iText.Kernel.Pdf.Event;
using iText.Commons.Actions;
using System.Data;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
namespace FocusFM.Common.Helpers
{
    public static class ExportHelper
    {
        public static byte[] ExportToExcel(DataTable dataTable, string headerName, string sheetName = "Sheet1")
        {
            // Create an in-memory workbook
            var workbook = new XLWorkbook();

            // Create the worksheet
            var worksheet = workbook.Worksheets.Add(sheetName);

            // Merge the entire first row (across all columns)
            var headerRange = worksheet.Range(worksheet.Cell(1, 1), worksheet.Cell(1, dataTable.Columns.Count));
            headerRange.Merge();

            // Add custom text to the merged first row
            headerRange.Value =headerName; // Custom header text after merge

            // Apply formatting (bold, center-aligned, background color)
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            headerRange.Style.Fill.BackgroundColor = XLColor.DarkCyan; // Background color for merged header

            // Add the column headers (second row)
            for (int col = 1; col <= dataTable.Columns.Count; col++)
            {
                worksheet.Cell(2, col).Value = dataTable.Columns[col - 1].ColumnName;
                worksheet.Cell(2, col).Style.Font.Bold = true;  // Make header bold
                worksheet.Cell(2, col).Style.Fill.BackgroundColor = XLColor.BlueGray; // Header background color
                worksheet.Cell(2, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; // Center-align headers
            }

            // Populate the worksheet with data starting from row 3 (since row 1 is for the merged header and row 2 is for column headers)
            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    // Handle DBNull and ensure proper conversion of the object to string or desired type
                    var cellValue = dataTable.Rows[row][col];
                    worksheet.Cell(row + 3, col + 1).Value = cellValue != DBNull.Value ? cellValue.ToString() : string.Empty;
                }
            }

            // Adjust column widths to fit the content
            worksheet.Columns().AdjustToContents();

            // Write the workbook to a byte array
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return stream.ToArray();
            }

        }
    }
}
