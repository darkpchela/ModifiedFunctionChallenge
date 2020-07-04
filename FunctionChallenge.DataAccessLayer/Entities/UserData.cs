using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FunctionChallenge.DataAccessLayer.Entities
{
    public class UserData
    {
        [Key]
        public int UDKey { get; set; }
        public double x1 { get; set; }
        public double xn { get; set; }
        public double step { get; set; }
        public double a { get; set; }
        public double b { get; set; }
        public double c { get; set; }
    }
}
