using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DtoTranslator
{
    static class NamingConvention
    {
        public static string ConvertType(TypeModel typeModel)
        {
            if (typeModel.ChildTypes != null && typeModel.ChildTypes.Count > 0)
            {
                var internalType = new List<string>();
                foreach (var childType in typeModel.ChildTypes)
                {
                    internalType.Add(ConvertType(childType));
                }
                return ConvertTypeName(typeModel.TypeName) + "<" + internalType.Aggregate((a, b) => a + ", " + b) + ">";
            }
            else
            {
                return ConvertTypeName(typeModel.TypeName);
            }
        }

        public static string ConvertEnumName(string name)
        {
            var rgx = new Regex(@"([A-Z0-9])");
            var a = rgx.Split(name, int.MaxValue, 1);
            string res = "";
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i].Length == 1)
                {
                    res += "_" + a[i];
                }
                else
                {
                    res += a[i];
                }
            }
            return res.ToUpperInvariant();
        }

        private static string ConvertTypeName(string typeName)
        {
            string arrayBrackets = "";
            while (typeName.EndsWith("[]"))
            {
                arrayBrackets += "[]";
                typeName = typeName.Substring(0, typeName.Length - 2);
            }
            switch (typeName)
            {
                case "Int16":
                case "Int32":
                case "Int64":
                case "Single":
                case "Double":
                case "Float":
                case "Long":
                case "Decimal":
                case "Byte":
                    return "number" + arrayBrackets;
                case "Boolean":
                    return "boolean" + arrayBrackets;
                case "String":
                    return "string" + arrayBrackets;
                case "List":
                case "IEnumerable":
                    return "Array" + arrayBrackets;
                case "DateTime":
                    return "string" + arrayBrackets;
                case "Object":
                    return "any" + arrayBrackets;
                default: return typeName;
            }
        }

        public static string ConvertName(string name)
        {
            string result = name.Substring(0, 1).ToLower() + name.Substring(1);
            return result;
        }
    }
}
