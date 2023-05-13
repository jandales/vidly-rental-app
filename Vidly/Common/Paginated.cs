using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Common
{
    public class Paginated<T>
    {
        public int Page { get; set; } = 0;

        public int? Start { get; set; } = 0;

        public int TotalPage { get; set; } = 0;

        public int? PageSize { get; set; } = 0;

        public IEnumerable<T> Data { get; set; }
    }
}