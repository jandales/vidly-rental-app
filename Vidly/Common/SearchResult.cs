using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Common
{
    public class SearchResult<T>
    {
        public string Keyword { get; set; } = string.Empty;

        public int ItemFoundCount { get; set; } = 0;

        public int Page { get; set; } = 0;

        public int TotalPage { get; set; } = 0;

        public int PageSize { get; set; } = 0;

        public IEnumerable<T> Data { get; set; }
    }
}