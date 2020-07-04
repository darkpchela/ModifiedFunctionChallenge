using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionChallenge.BusinessLayer.Infrastructure
{
    public class CustomValidationException : Exception
    {
        public string Property { get; protected set; }
        public CustomValidationException(string message, string prop) : base(message)
        {
            Property = prop;
        }
    }
}
