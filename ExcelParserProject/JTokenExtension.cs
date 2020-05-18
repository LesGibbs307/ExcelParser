using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ExcelParserProject
{
    public static class JTokenExtension
    {
      
        private static JToken jsonProps = null;

        public static bool CheckIfValid(this JToken node, object obj)
        {
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
                            child.WalkNode(variableName);                            
                        }
                        break;
                    }
            }
        }
    }
}