using Epic.CRM.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.CRM.BusinessLogic.Helpers
{
    public class Result
    {
        public ResultStatusEnum ResultStatus => Errors.Any() ? ResultStatusEnum.Fail : ResultStatusEnum.Success;
        public List<string> Errors { get; set; } = new();

        public override string ToString()
        {
            return $"{ResultStatus}";
        }
    }

    public class DataResult<T> : Result where T : class
    {
        public T? Data { get; set; }
    }

    public class PageResult<T> : DataResult<T> where T: class, IEnumerable<object>
    {
        public PageResult(QueryParams queryParams)
        {
            QueryParams = queryParams;
        }
        public QueryParams QueryParams { get; set; }
        public int? ItemCount => Data?.Count();
    }

    public enum ResultStatusEnum
    {
        Success,
        Fail
    }
}
