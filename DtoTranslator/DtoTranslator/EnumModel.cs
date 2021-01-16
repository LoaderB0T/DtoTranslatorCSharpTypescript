using System;
using System.Collections.Generic;
using System.Text;

namespace DtoTranslator
{
    class EnumModel: ObjectModel
    {
        public EnumModel(string name)
        {
            this.Name = name;
            this.Values = new List<EnumValueModel>();
        }

        public List<EnumValueModel> Values { get; set; }
    }
}
