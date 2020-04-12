using System;
using System.Collections;
using System.Collections.Generic;

namespace ExcelParserProject
{
    public abstract class BaseFile
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public List<object> Transactions { get; set; }
    }
}
