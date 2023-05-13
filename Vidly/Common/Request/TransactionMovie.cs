using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Common.Request
{
    public class TransactionMovie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }         
        public int Qty { get; set; }
        public float Price { get; set; }
    }
}