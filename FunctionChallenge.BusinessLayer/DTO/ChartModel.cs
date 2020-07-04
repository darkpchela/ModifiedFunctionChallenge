using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FunctionChallenge.BusinessLayer.DTO
{
    public class ChartModel
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
        public int step { get; set; }
        [Required]
        [Range(-100, 100, ErrorMessage = "Value should be between -100 and 100")]
        public int from { get; set; }
        [Required]
        [Range(-100, 100, ErrorMessage = "Value should be between -100 and 100")]
        public int to { get; set; }

        public string chartName { get; set; }
        public string points { get; set; }
    }
}
