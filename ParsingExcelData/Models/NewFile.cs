using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text.RegularExpressions;
using ExcelParserProject.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ParsingExcelData.Models
{
    public class NewFile
    {
        public KeyValuePair<bool, string> ResultData { get; set; } // this data needs to be a part of Financial
        public static IFormFile FileInfo { get; set; }
        public BaseFile BaseFile { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public NewFile(IFormFile file)
        {
            FileInfo = file;
            FileName = Path.GetFileName(FileInfo.FileName);
            FilePath = SetFilePath();
            CreateFileInDirectory();
            CheckFileType();
        }

        private JToken[] FormatFile(string baseFile)
        {
            var file = (JObject)JsonConvert.DeserializeObject(baseFile);
            JToken headers = null;
            JToken rows = null;
            var name = JTokenExtension.GetMemberName(() => headers);
            headers = JTokenExtension.GetType(file, name);
            name = JTokenExtension.GetMemberName(() => rows);
            rows = JTokenExtension.GetType(file, name);
            return new JToken [] { headers, rows };
        }

        private KeyValuePair<bool, string> CheckFileType()
        {
            Regex reg = new Regex(@"([^\.]+$)");
            Match match = reg.Match(FileName);
            BaseFile = match.ToString().Contains("xls") ? BaseFile = new Excel(FileName, FilePath)
                                                        : BaseFile = new CSV(FileName, FilePath);
            var results = FormatFile(JsonConvert.SerializeObject(BaseFile));
            ResultData = new FinancialItem().ParseData(results);
            File.Delete(FilePath);
            // send file to cosmos db
            // delete temporary file here
            return ResultData;
        }

        private string SetFilePath()
        {            
            return Path.Combine(Directory.GetCurrentDirectory(), @"www", FileName);
        }
        private async void CreateFileInDirectory()
        {
            try
            {
                using (var fileStream = new FileStream(FilePath, FileMode.Create))
                {
                    await FileInfo.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
