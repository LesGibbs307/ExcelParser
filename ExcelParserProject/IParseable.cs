using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelParserProject
{
    public interface IParseable
    {
        void ToParseData();
        void ReadFile();
    }
}
