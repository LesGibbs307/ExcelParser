using ExcelParserProject.Domain;
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
        public decimal AmountOwed { get; set; }

        [JsonProperty("TimeSpan")]
        public string TimeSpan { get; set; }

        public FinancialItem()
        {

        }

        public List<FinancialItem> IterateData(JToken[] arr)
        {
            List<FinancialItem> items = new List<FinancialItem>();
            int iterate = 0;
            var rows = arr[1];

            foreach (var row in rows)
            {
                FinancialItem item = new FinancialItem();
                item.Name = rows[iterate][0].ToString();
                item.Type = rows[iterate][1].ToString();
                item.Range = rows[iterate][2].ToString();
                item.Priority = rows[iterate][3].ToString();
                item.Amount = JTokenExtension.ToDecimal(rows[iterate][4]);
                item.AmountOwed = JTokenExtension.ToDecimal(rows[iterate][5]);
                item.TimeSpan = rows[iterate][6].ToString();
                items.Add(item);
                iterate++;            
            }
            return items;
        }

        public KeyValuePair<bool, string> ParseData(JToken[] arr)
        {
            try
            {
                bool isValidHeader = JTokenExtension.CheckIfValid(arr[0], this);
                if (isValidHeader)
                {
                    List<FinancialItem> obj = IterateData(arr);
                    string json = JsonConvert.SerializeObject(obj);
                    return new KeyValuePair<bool, string>(isValidHeader, json);
                }
                else
                {
                    throw new Exception("Header data is missing");
                }
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string> (false, ex.Message);
            }
        }
        public string ConvertToJson<T>(T data)
        {
            try
            {
                var result = JsonConvert.SerializeObject(data);
                var jobj = (JObject)JsonConvert.DeserializeObject(result);
                var worksheet = jobj["Worksheets"];
                return result.ToString();
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
    }
}