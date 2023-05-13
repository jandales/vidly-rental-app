using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Vidly.Common
{
    public class PaginatedRequest
    {
        public const int Page_Size = 25;      
        public int Page { get; set; } = 1;
        public string Filter { get; set; } = "all";
    }
}