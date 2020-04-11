using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text.RegularExpressions;
using ExcelParserProject;

namespace ParsingExcelData.Models
{
    public class NewFile
    {
        public static IFormFile File { get; set; }
        static string FileName { get; set; }
        
        //public static CSV Csv;


        public NewFile(IFormFile file)
        {
            File = file;
        }

        private void CheckFileType(string fileName, string filePath, Stream fileStream)
        {
            Regex reg = new Regex(@"([^\.]+$)");
            Match match = reg.Match(fileName);
            if (match.ToString() == "xlsx")
            {
                Excel.ReadFile(fileName, filePath, fileStream);
            }
            //else { 
            //    Csv.ReadFile(); 
            //}
            
        }

        public async void CreateFileInDirectory()
        {
            try
            {
                //check if file exist
                var fileName = Path.GetFileName(File.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"www", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await File.CopyToAsync(fileStream);
                    CheckFileType(fileName, filePath, fileStream);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
