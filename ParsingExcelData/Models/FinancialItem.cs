using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public dynamic ConvertToJson(dynamic items)
        {
            foreach(var item in items)
            {
                var result = JsonConvert.SerializeObject(item.Headers);
            }
            return items;
        }
    }
}
