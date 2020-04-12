using System;
using System.IO;
using ExcelDataReader;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Collections;


namespace ExcelParserProject
{
    public class Excel: BaseFile
    {
        private List<IEnumerable> worksheet;
        private int sheets;
        public Excel(string fileName, string filePath)
        {
            FileName = fileName;
            FilePath = filePath;
            ReadFile(FileName, FilePath);
        }

        private void IterateWorkBook(dynamic values)
        {
            
            foreach (DataTable table in values) { 
                foreach (DataRow row in table.Rows) { 
                    foreach (DataColumn column in table.Columns) { 
                        if (row[column] != null) {
                            //var obj = new IEnumerable
                            //{
                            //    row[column]
                            //};
                            //Transactions.Add(row[column]);
                            Console.WriteLine(row[column]);
                        }
                    }
                }
            }
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
                        var conf = new ExcelReaderConfiguration { Password = "JohnGibson" };
                        var excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream, conf);
                        var result = reader.AsDataSet();
                        var workbook = result.Tables;
                        IterateWorkBook(workbook);
                    }
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
