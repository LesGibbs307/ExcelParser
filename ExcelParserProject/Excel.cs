using System;
using System.IO;
using ExcelDataReader;
using System.Text;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Microsoft.CSharp.RuntimeBinder;
using System.Threading.Tasks;

namespace ExcelParserProject.Domain
{
    public class Excel : BaseFile
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public List<Worksheet> Worksheets = new List<Worksheet>();

        public Excel(string fileName, string filePath)
        {
            FileName = fileName;
            FilePath = filePath;
            ReadFile(FilePath, new Worksheet());
        }




        private async Task CreateWorkSheet(dynamic table, dynamic row, Worksheet worksheet)
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

        private async Task IterateWorkBook(dynamic values, Worksheet worksheet)
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
                                    await CreateWorkSheet(table, row, worksheet);
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

        private async void ReadFile(string filePath, Worksheet worksheet)
        {
            try 
            { 
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        do
                        {
                            while (reader.Read())
                            {      
                            }
                        } while (reader.NextResult());
                        var conf = new ExcelReaderConfiguration { Password = "Test" };
                        var excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream, conf);
                        var result = reader.AsDataSet();
                        var workbook = result.Tables;
                        await IterateWorkBook(workbook, worksheet);
                    }
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}