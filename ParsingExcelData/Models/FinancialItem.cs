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
        public string Amount { get; set; }

        [JsonProperty("AmountOwed")]
        public string AmountOwed { get; set; }

        [JsonProperty("TimeSpan")]
        public string TimeSpan { get; set; }

        public FinancialItem()
        {

        }

        public List<FinancialItem> IterateData(JToken[] arr)
        {
            List<FinancialItem> items = new List<FinancialItem>();
            int interate = 0;

            foreach (var item in arr[1])
            {
                this.Name = arr[1][interate][0].ToString();
                this.Type = arr[1][interate][1].ToString();
                this.Range = arr[1][interate][2].ToString();
                this.Priority = arr[1][interate][3].ToString();
                this.Amount = arr[1][interate][4].ToString();
                this.AmountOwed = arr[1][interate][5].ToString();
                this.TimeSpan = arr[1][interate][6].ToString();
                items.Add(this);
                interate++;
            }
            return items;
        }

        public string ParseData(JToken[] arr)
        {
            try
            {
                bool isValidHeader = JTokenExtension.CheckIfValid(arr[0], this);
                if (!isValidHeader)
                {
                    throw new Exception();
                }
                else
                {
                    List<FinancialItem> obj = IterateData(arr);
                    return JsonConvert.SerializeObject(obj);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }
        public string ConvertToJson<T>(T data)
        {
            try
            {
                var result = JsonConvert.SerializeObject(data);
                var jobj = (JObject)JsonConvert.DeserializeObject(result);
                // Get header
                // Check if header is similar to json property
                // Find body object
                // Find t
                
                //var headers = JTokenExtension.GetValue(jobj);
                var worksheet = jobj["Worksheets"];
                //bool isValid = JTokenExtension.CheckIfValid(worksheet, this);
                //if (isValid)
                //{
                //    dynamic items = new List<FinancialItem>();
                //    items = JTokenExtension.Collection;
                //    items = JTokenExtension.GetDataList(jobj);
                //}
                //else
                //{
                //    throw new Exception("Correct Headers aren't located in document");
                //}
                return result.ToString();
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
    }
}
