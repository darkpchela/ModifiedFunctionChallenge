using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModifiedFunctionChallenge.Models
{
    public class ChartViewerViewModel
    {
        public IEnumerable<string> chartNames { get; set; }
        public double a { get; set; }
        public double b { get; set; }
        public double c { get; set; }
        public int step { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public string chartName { get; set; }
        public string points { get; set; }
    }
}
