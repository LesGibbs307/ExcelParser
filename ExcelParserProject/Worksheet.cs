using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelParserProject
{
    public class Worksheet
    {
        public string Name { get; set; }
        public object[] Headers { get; set; }
        public List<object[]> Rows = new List<object[]>();

        internal Worksheet()
        {

        }
    }
}
