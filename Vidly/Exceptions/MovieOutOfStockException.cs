using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Exceptions
{
    public class MovieOutOfStockException : Exception
    {
        public MovieOutOfStockException(string message) : base(message) 
        {

        }
    }
}