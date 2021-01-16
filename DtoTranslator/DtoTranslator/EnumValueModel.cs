namespace DtoTranslator
{
    public class EnumValueModel
    {
        public string Name { get; set; }
        public int Index { get; set; }

        public string GetString(string newLine, bool lastOne)
        {
            return "  " + NamingConvention.ConvertEnumName(Name) + " = " + Index + (lastOne ? "" : ",") + newLine;
        }
    }
}