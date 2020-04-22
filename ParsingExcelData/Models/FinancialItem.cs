using ExcelParserProject;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ParsingExcelData
{
    public class FinancialItem
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Range")]
        public string Range { get; set; }

        [JsonProperty("Priority")]
        public string Priority { get; set; }

        [JsonProperty("Amount")]
        public decimal Amount { get; set; }

        [JsonProperty("AmountOwed")]
        public dynamic AmountOwed { get; set; }



        public FinancialItem()
        {

        }
        public string ConvertToJson<T>(T data)
        {
            try
            {
                var result = JsonConvert.SerializeObject(data);
                var jobj = (JObject)JsonConvert.DeserializeObject(result);
                bool isValid = JTokenExtension.CheckIfValid(jobj, this);
                if (isValid)
                {

                }
                else
                {
                    throw new Exception("Value is in value");
                }
                return result;
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
    }
}
