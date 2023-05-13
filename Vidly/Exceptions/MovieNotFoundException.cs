using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Exceptions
{
    public class MovieNotFoundException : Exception
    {
        public MovieNotFoundException(string message) : base(message)
        {
            
        }
    }
}