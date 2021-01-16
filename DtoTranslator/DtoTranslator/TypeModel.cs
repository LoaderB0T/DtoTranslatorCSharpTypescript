using System;
using System.Collections.Generic;
using System.Text;

namespace DtoTranslator
{
    public class TypeModel
    {
        public string TypeName { get; set; }
        public List<TypeModel> ChildTypes { get; set; }
    }
}
