using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelParserProject.Domain
{
    public class Worksheet
    {
        public List<object[]> Rows = new List<object[]>();
        public string Name { get; set; }
        public object[] Headers { get; set; }
        
        internal Worksheet()
        {

        }
    }
}