using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FunctionChallenge.Models
{
    public class ChartViewModel
    {
        [Required]
        [Range(-100, 100, ErrorMessage = "Value should be between -100 and 100")]
        public double a { get; set; }
        [Required]
        [Range(-100, 100, ErrorMessage = "Value should be between -100 and 100")]
        public double b { get; set; }
        [Required]
        [Range(-100, 100, ErrorMessage = "Value should be between -100 and 100")]
        public double c { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "Value should be between 1 and 100")]
        public double step { get; set; }
        [Required]
        [Range(-100, 100, ErrorMessage = "Value should be between -100 and 100")]
        public double from { get; set; }
        [Required]
        [Range(-100, 100, ErrorMessage = "Value should be between -100 and 100")]
        public double to { get; set; }
        public string chartName { get; set; }
        public string points { get; set; }
    }
}
