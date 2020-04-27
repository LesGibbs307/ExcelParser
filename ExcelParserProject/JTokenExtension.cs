using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace ExcelParserProject
{
    public static class JTokenExtension
    {
        private static JToken jsonProps = null;

        private static string[] jsonHeaders;

        private static bool result = false;
        public static IEnumerable Collection { get; set; }
        
        private static bool CompareValues(this JToken node, string[] arr)
        {
            if (node.Type == JTokenType.Array)
            {
                if (node.ToObject<string[]>().SequenceEqual(arr)) { return true; }
            }
            return false;
        }

        private static string SetMethodCall()
        {
            return new StackFrame(1).GetMethod().Name;
        }

        public static List<dynamic> GetDataList(this JToken node)
        {
            string currentMethod = SetMethodCall();
            WalkNode(node, currentMethod);
            return null;
        }

        private static void CheckMethodCall(this JToken node, string methodName, bool result, string[] jsonHeaders)
        {
            if (methodName == "CheckIfValid")
            {
                result = CompareValues(node, jsonHeaders);
                SetValue(result, node);
            } else if(methodName == "GetDataList")
            {
                AddToCollection(node);
            }
        }

        private static void AddToCollection(this JToken node)
        {
            foreach(var child in node.Children<JProperty>())
            {
                if(child.Name == "Rows")
                {
                    int test = 1;
                }
                int test3 = 3;
            }
        }

        public static bool CheckIfValid(this JToken node, object obj)
        {
            string currentMethod = SetMethodCall();
            jsonHeaders = obj.GetType().GetProperties()
                    .Select(p =>
                    {
                        return p.Name.ToString();
                    })
                    .ToArray();
            WalkNode(node, currentMethod);
            return result;
        }

        private static void SetValue(bool value, JToken node)
        {
            if (value) { jsonProps = node; }
        }

        private static void WalkNode(this JToken node, string methodCall) 
        {
            switch (node.Type)
            {
                case JTokenType.Object:
                    {
                        foreach (var child in node.Children<JProperty>())
                        {
                            CheckMethodCall(child, methodCall, result, jsonHeaders);
                            child.Value.WalkNode(methodCall);  
                        }
                        break;
                    }
                case JTokenType.Array:
                    {
                        foreach (var child in node.Children())
                        {
                            CheckMethodCall(child, methodCall, result, jsonHeaders);
                            child.WalkNode(methodCall);                            
                        }
                        break;
                    }
            }
            if(jsonProps != null) { result = true; }
        }
    }
}