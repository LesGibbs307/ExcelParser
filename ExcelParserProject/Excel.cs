using System;
using ExcelParserProject;
using System.IO;
using Microsoft.Extensions.FileProviders;
using System.Text.RegularExpressions;
using ExcelDataReader;

namespace ExcelParserProject
{
    public static class Excel// : IParseable
    {
        
        private static string FilePath;
        private static int sheets;


        public static void ReadFile(string fileName, string filePath, dynamic stream)
        {
            var reader = ExcelReaderFactory.CreateReader(stream, new ExcelReaderConfiguration()
            {
                // Gets or sets the encoding to use when the input XLS lacks a CodePage
                // record, or when the input CSV lacks a BOM and does not parse as UTF8. 
                // Default: cp1252 (XLS BIFF2-5 and CSV only)
               //FallbackEncoding = Encoding.GetEncoding(1252),

                // Gets or sets the password used to open password protected workbooks.
                Password = "password",

                // Gets or sets an array of CSV separator candidates. The reader 
                // autodetects which best fits the input data. Default: , ; TAB | # 
                // (CSV only)
                AutodetectSeparators = new char[] { ',', ';', '\t', '|', '#' },

                // Gets or sets a value indicating whether to leave the stream open after
                // the IExcelDataReader object is disposed. Default: false
                LeaveOpen = false,

                // Gets or sets a value indicating the number of rows to analyze for
                // encoding, separator and field count in a CSV. When set, this option
                // causes the IExcelDataReader.RowCount property to throw an exception.
                // Default: 0 - analyzes the entire file (CSV only, has no effect on other
                // formats)
                AnalyzeInitialCsvRows = 0,
            });
        }

        //public void ReadFile()
        //{
        //    workBook = excel.Workbooks.Open(FileName);
        //    //workSheet = workbook.
        //    // define workbook
        //    //check how many number of sheets in 
        //    // Get header names
        //    // get a collection of data

        //    //workSheet = workBook.WorkSheets[]
        //    // throw new NotImplementedException();
        //}


        public static void ToParseData()
        {
            throw new NotImplementedException();
        }
    }
}
