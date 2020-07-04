using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FunctionChallenge.DataAccessLayer.Entities
{
    public class Point
    {
        [Key]
        public int PKey { get; set; }
        public int CKey { get; set; }
        public double x { get; set; }
        public double y { get; set; }
    }
}
