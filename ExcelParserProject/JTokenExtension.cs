using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Linq.Expressions;

namespace ExcelParserProject
{
    public static class JTokenExtension
    {
      
        private static JToken jsonProps = null;

        //private static string[] jsonHeaders;

        private static bool result = false;
        //public static IEnumerable Collection { get; set; }

        public static bool CheckIfValid(this JToken node, object obj)
        {

            //string currentMethod = SetMethodCall();
            string[] jsonHeaders = obj.GetType().GetProperties()
                    .Select(p =>
                    {
                        return p.Name.ToString();
                    })
                    .ToArray();
            string nodeValue = string.Join(",", node);
            string jsonValue = string.Join(",", jsonHeaders);
            bool result = (jsonValue.Contains(nodeValue)) ? true : false;
            return result;
        }

        public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
        {
            MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }
        private static string ToCapitalize(string name)
        {
            return char.ToUpper(name[0]) + name.Substring(1);
        }

        public static JToken GetType(JToken node, string variableName)
        {
            variableName = ToCapitalize(variableName);
            if(node[variableName] == null) {
                node.WalkNode(variableName);
            } else
            {
                jsonProps = node[variableName];
                return node;
            }
            return jsonProps;
        }
        
        private static void WalkNode(this JToken node, string? variableName) 
        {
            switch (node.Type)
            {
                case JTokenType.Object:
                    {
                        foreach (var child in node.Children<JProperty>())
                        {
                            //GetType(child.Value, variableName);
                            //CheckMethodCall(child, methodCall, result, jsonHeaders);
                            child.Value.WalkNode(variableName);
                        }
                        break;
                    }
                case JTokenType.Array:
                    {
                        foreach (var child in node.Children())
                        {
                            if(variableName != null)
                            {
                                GetType(child, variableName);
                                break;
                            }

                            //CheckMethodCall(child["Headers"], methodCall, result, jsonHeaders);
                            child.WalkNode(variableName);                            
                        }
                        break;
                    }
            }
            
            //if(jsonProps != null) { result = true; }            
        }
    }
}







//private static string SetMethodCall()
//{
//    return new StackFrame(1).GetMethod().Name;
//}

//public static List<dynamic> GetDataList(this JToken node)
//{
//    string currentMethod = SetMethodCall();
//    WalkNode(node, currentMethod); // fix later
//    return null;
//}

//private static void CheckMethodCall(this JToken node, string methodName, bool result, string[] jsonHeaders)
//{
//    if (methodName == "CheckIfValid")
//    {
//        result = CompareValues(node, jsonHeaders);
//        SetValue(result, node);
//    } else if(methodName == "GetDataList")
//    {
//        AddToCollection(node);
//    }
//}

//private static void AddToCollection(this JToken node)
//{
//    foreach(var child in node.Children<JProperty>())
//    {
//        if(child.Name == "Rows")
//        {
//            int test = 1;
//        }
//        int test3 = 3;
//    }
//}



//private static void SetValue(bool value, JToken node)
//{
//    if (value) { jsonProps = node; }
//}