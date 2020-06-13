using System;
using System.Collections;
using System.Collections.Generic;

namespace ExcelParserProject.Domain
{
    public abstract class BaseFile
    {
        internal string FileName { get; set; }
        internal string FilePath { get; set; }

        public List<Worksheet> Worksheets = new List<Worksheet>();
    }
}
