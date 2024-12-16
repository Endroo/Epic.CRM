using Epic.CRM.Common;
using Epic.CRM.DataDomain.Helpers;
using Epic.CRM.DataDomain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.CRM.DataDomain.Interfaces.Repositories
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        AppUser GetByIdentityId(string identityId, FindOptions findOptions = null);
        AppUser GetById(int appUserId, FindOptions findOptions = null);
        Task<IEnumerable<AppUser>> GetAll(QueryParams queryParams);
    }
}
