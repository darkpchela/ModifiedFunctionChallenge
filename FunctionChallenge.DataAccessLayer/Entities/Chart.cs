using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FunctionChallenge.DataAccessLayer.Entities
{
    public class Chart
    {
        [Key]
        public int CKey { get; set; }
        public int UDKey { get; set; }
        public string ChartName { get; set; }
    }
}
