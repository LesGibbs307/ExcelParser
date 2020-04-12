using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExcelParserProject
{
    public abstract class BaseFile
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
