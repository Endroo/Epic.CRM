using Epic.CRM.BusinessLogic.Helpers;
using Epic.CRM.Common;
using Epic.CRM.DataDomain.Dtos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.CRM.BusinessLogic.Interfaces
{
    public interface IWorkManager
    {
        Task<PageResult<IEnumerable<WorkDto>>> GetAll(string identityUserId, QueryParams queryParams);
        Task<Result> EditWork(int workId, WorkEditRegisterDto dto);
        Task<Result> DeleteWork(int workId);
        Task<Result> CreateWork(string identityUserId, WorkEditRegisterDto dto);
    }
}
