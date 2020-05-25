using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text.RegularExpressions;
using ExcelParserProject;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ParsingExcelData.Models
{
    public class NewFile
    {
        public string fileName;
        public string filePath;
        public string data;
        public static IFormFile File { get; set; }
        public BaseFile BaseFile { get; set; }


        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        public NewFile(IFormFile file)
        {
            File = file;
            FileName = Path.GetFileName(File.FileName);
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

        private string CheckFileType()
        {
            Regex reg = new Regex(@"([^\.]+$)");
            Match match = reg.Match(fileName);
            if (match.ToString() == "xlsx")//john needs change this to support xls and xlsx
            {
                BaseFile = new Excel(fileName, filePath);

            } else
            {
                BaseFile = new CSV();
            }
            var results = FormatFile(JsonConvert.SerializeObject(BaseFile));
            data = new FinancialItem().ParseData(results);
            return data;
        }

        private string SetFilePath()
        {            
            return Path.Combine(Directory.GetCurrentDirectory(), @"www", fileName);
        }
        private async void CreateFileInDirectory()
        {
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await File.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
