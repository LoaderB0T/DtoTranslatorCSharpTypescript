using System;
using System.Collections.Generic;
using System.Text;

namespace DtoTranslator
{
    class PropertyModel
    {
        public string Name { get; set; }
        public TypeModel Type { get; set; }
        public bool IsNullable { get; set; }

        public string GetString(string newLine)
        {
            var nullableOrNot = IsNullable ? "?" : "";
            return "  public " + NamingConvention.ConvertName(Name) + nullableOrNot + ": " + NamingConvention.ConvertType(Type) + ";" + newLine;
        }
    }
}
