using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Exceptions
{
    public class TransactionNotIssuedException : Exception
    {
        public TransactionNotIssuedException(string message) : base(message) { }
    }
}