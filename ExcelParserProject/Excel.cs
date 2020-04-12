using System;
using ExcelParserProject;
using System.IO;
using Microsoft.Extensions.FileProviders;
using System.Text.RegularExpressions;
using ExcelDataReader;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Collections;

namespace ExcelParserProject
{
    public class Excel: BaseFile
    {
        private List<IEnumerable> sheets;        
        public Excel(string fileName, string filePath)
        {
            FileName = fileName;
            FilePath = filePath;
            ReadFile(FileName, FilePath);
        }
                     
        private void ReadFile(string fileName, string filePath)
        {
            try 
            { 
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    // Auto-detect format, supports:
                    //  - Binary Excel files (2.0-2003 format; *.xls)
                    //  - OpenXml Excel files (2007 format; *.xlsx)
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        // Choose one of either 1 or 2:

                        // 1. Use the reader methods
                        do
                        {
                            while (reader.Read())
                            {                                
                            }
                        } while (reader.NextResult());
                        var conf = new ExcelReaderConfiguration { Password = "yourPassword" };
                        var excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream, conf);
                        // 2. Use the AsDataSet extension method
                        var result = reader.AsDataSet();

                        // The result of each spreadsheet is in result.Tables
                    }
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
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



    }
}
