using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Exceptions
{
    public class TransactionNotFoundExeption : Exception
    {
        public TransactionNotFoundExeption(string message) : base(message){}
    }
}