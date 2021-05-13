using System;
using System.Collections.Generic;
using System.Text;

namespace DtoTranslator
{
    class ClassModel : ObjectModel
    {
        public ClassModel(string name)
        {
            this.Name = name;
            this.Props = new List<PropertyModel>();
        }

        public List<PropertyModel> Props { get; set; }

        public string ParentClass { get; set; }
    }
}
