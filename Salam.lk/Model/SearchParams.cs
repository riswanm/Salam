using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Salam.lk.Model
{
    public class SearchParams
    {
        public string SearchText { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}