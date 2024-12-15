using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.CRM.Common
{
    public class QueryParams
    {
        public string? SortOrder { get; set; }
        public string? SortColumn { get; set; }
        public string? SearchString { get; set; }
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
    }
}
