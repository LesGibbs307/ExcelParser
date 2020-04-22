using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace ExcelParserProject
{
    public static class JTokenExtension
    {
        private static bool CompareObjects(this JToken node, string[] arr)
        {
            if (node.Type == JTokenType.Array)
            {
                if (node.ToObject<string[]>().SequenceEqual(arr))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CheckIfValid(this JToken node, object obj)
        {
            var arr = obj.GetType().GetProperties()
                                    .Select(p =>
                                    {
                                        return p.Name.ToString();
                                    })
                                    .ToArray();
            return WalkNodes(node, arr);            
        }

        private static bool WalkNodes(this JToken node, string[] arr)
        {
            bool result = false;
            switch (node.Type)
            {
                case JTokenType.Object:
                    foreach(var child in node.Children<JProperty>())
                    {
                        result = CompareObjects(child, arr);
                        if (!result)
                        {
                            child.Value.WalkNodes(arr);
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;
                case JTokenType.Array:
                    foreach(var child in node.Children())
                    {
                        result = CompareObjects(child, arr);
                        if (!result) { 
                            child.WalkNodes(arr);
                        } else
                        {
                            break;
                        }
                    }
                    return result;
            }
            return result;
        }
    }
}
