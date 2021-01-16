using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitTest.TestResources.Dtos._1.Enums;

namespace UnitTest.TestResources.Dtos._1
{
    public class TestEDto
    {
        public string PropA { get; set; }
        public bool PropB { get; set; }
        public bool PropC { get; set; }
        public TestEA PropD { get; set; }
        public TestEB PropE { get; set; }
        public TestEC PropF { get; set; }
    }
}
