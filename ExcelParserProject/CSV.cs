using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelParserProject.Domain
{
    public class CSV : BaseFile
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public List<Worksheet> Worksheets { get; set; }

        public CSV(string fileName, string filePath)
        {
            FileName = fileName;
            FilePath = filePath;

        }
    }
}
