using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text.RegularExpressions;
using ExcelParserProject;

namespace ParsingExcelData.Models
{
    public class NewFile
    {
        public string fileName;
        public string filePath;
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

        private void CheckFileType()
        {
            Regex reg = new Regex(@"([^\.]+$)");
            Match match = reg.Match(fileName);
            if (match.ToString() == "xlsx")
            {
                BaseFile = new Excel(fileName, filePath);

            } else
            {
                BaseFile = new CSV();
            }
            
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
