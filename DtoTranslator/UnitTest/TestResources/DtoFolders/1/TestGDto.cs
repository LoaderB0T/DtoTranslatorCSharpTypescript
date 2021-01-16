using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTest.TestResources.Dtos._1
{
    public class TestGDto
    {
        public Dictionary<string, int> PropA { get; set; }
        public List<List<int>> PropB { get; set; }
        public List<List<Dictionary<string, List<int>>>> PropC { get; set; }
        public Tuple<string, int, bool, decimal> PropD { get; set; }
    }
}
