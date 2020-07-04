using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FunctionChallenge.Models
{
    public class ChartViewModel
    {
        public int a { get; set; }
        public int b { get; set; }
        public int c { get; set; }
        public int step { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public string points { get; set; }
        public string chartName { get; set; }
    }
}
