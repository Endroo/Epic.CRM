using Epic.CRM.BusinessLogic.Helpers;
using Epic.CRM.Common;
using Epic.CRM.DataDomain.Dtos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Epic.CRM.BusinessLogic.Interfaces
{
    public interface IAppUserManager
    {
        Task<Result> CreateUser(AppUserRegisterDto dto);
        Task<DataResult<AppUserDto>> GetLoggedInUser(ClaimsPrincipal userPrincipal);
        Task<DataResult<AppUserDto>> GetByUserName(string userName); 
        Task<DataResult<AppUserDto>> GetById(int id);
        Task<PageResult<IEnumerable<AppUserDto>>> GetAll(QueryParams queryParams);
        Task<Result> EditUser(int appUserId, AppUserEditDto dto); 
        Task<Result> DeleteUser(int appUserId);
    }
}
