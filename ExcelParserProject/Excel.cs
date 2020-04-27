using System;
using System.IO;
using ExcelDataReader;
using System.Text;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Microsoft.CSharp.RuntimeBinder;

namespace ExcelParserProject
{
    public class Excel: BaseFile
    {
        private Worksheet worksheet = new Worksheet();
        public Excel(string fileName, string filePath)
        {
            FileName = fileName;
            FilePath = filePath;
            ReadFile(FileName, FilePath);
        }

        private void CreateWorkSheet(dynamic table, dynamic row)
        {
            var thisRow = row.ItemArray;
            try 
            {
                if (worksheet.Headers == null)
                {
                    worksheet.Name = table.TableName;
                    worksheet.Headers = thisRow;
                } else
                {
                    var list = new List<dynamic>(thisRow);
                    list.Add(table.TableName);
                    worksheet.Rows.Add(list.ToArray());
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void IterateWorkBook(dynamic values)
        {
            try
            {                
                foreach (DataTable table in values)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        foreach (DataColumn column in table.Columns)
                        {
                            int rowLength = row.ItemArray.Length -1;
                            if (row[column] != null)
                            {                                
                                if(column.Ordinal == rowLength)
                                {
                                    CreateWorkSheet(table, row);
                                }
                            }
                        }
                    }
                    Worksheets.Add(worksheet);
                }
                
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
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