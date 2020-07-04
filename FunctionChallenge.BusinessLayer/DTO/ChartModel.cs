using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FunctionChallenge.BusinessLayer.DTO
{
    public class ChartModel
    {
        public double a { get; set; }
        public double b { get; set; }
        public double c { get; set; }
        public double step { get; set; }
        public double from { get; set; }
        public double to { get; set; }
        public string chartName { get; set; }
        public string points { get; set; }
    }
}
