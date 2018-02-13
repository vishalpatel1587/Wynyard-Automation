using System;
using System.Configuration;
using System.IO;
using Aspose.Cells;
using Microsoft.Web.Services3.Referral;

namespace TestAutomation.Helpers
{
    public class CsvFileReader
    {
        private readonly string _fileName;
        public const string NullValue= "NULL";

        public CsvFileReader(string fileName)
        {

            _fileName = fileName;
            var lic = new License();
            lic.SetLicense(Path.GetFullPath(Environment.CurrentDirectory + @"/../../Aspose.Total.lic"));
        }

        public int GetNumberOfDataRowsOnWorksheet(int worksheetNumber)
        {
            DeleteBlankRowsOnWorksheet(worksheetNumber);
            var ws = GetWorksheet(worksheetNumber);
            var numOfRows = ws.Cells.MaxDataRow;
            return numOfRows;
        }

        public void DeleteBlankRowsOnWorksheet(int worksheetNumber)
        {
            var ws = GetWorksheet(worksheetNumber);
            ws.Cells.DeleteBlankRows();
        }

        public string GetStringValue(int rowNumber, int columnNumber)
        {
            var cell = GetValue(rowNumber, columnNumber);
            var value = cell.StringValue;
            return value;
          
        }

        public int? GetIntValue(int rowNumber, int columnNumber)
        {
            var cell = GetValue(rowNumber, columnNumber);
            string TempString = cell.StringValue;
            int? value = ToNullableInt32(TempString);
            return value;
        }

        public int? ToNullableInt32(string s)
        {
            int i;
            if (Int32.TryParse(s, out i)) return i;
            return 0;
        }
        public double GetDoubleValue(int rowNumber, int columnNumber)
        {
            var cell = GetValue(rowNumber, columnNumber);
            var value = cell.DoubleValue;
            return value;
        }

        public DateTime? GetDateTimeValue(int rowNumber, int columnNumber)
        {
            var cell = GetValue(rowNumber, columnNumber);
           // DateTime cell2 = cell.DateTimeValue;
            if (cell.StringValue != NullValue)
            {
                var value = cell.DateTimeValue;
                return value;

            }
            /*Returning the min value i.e. 1/1/0001 12:00:00 AM because when sqldatareaderextension class reads
             the value from the database, if the date time value is null, it assings it to 1/1/0001 12:00:00 AM- Vishal*/
             return DateTime.MinValue;

        }

        private Cell GetValue(int rowNumber, int columnNumber)
        {
            var ws = GetWorksheet(0);
            var cell = ws.Cells[rowNumber, columnNumber];
            return cell;
        }

        private Worksheet GetWorksheet(int wsNumber)
        {
            var loadOptions = new LoadOptions(LoadFormat.CSV);
            var wb = new Workbook(_fileName, loadOptions);
            var ws = wb.Worksheets[wsNumber];
            return ws;
        }
    }
}
