using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Exceptions
{
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException(string message) : base(message) { }
    }
}