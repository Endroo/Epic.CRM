using Epic.CRM.Common;
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
        AppUser GetByIdentityId(string identityId);
        AppUser GetById(int appUserId);
        Task<IEnumerable<AppUser>> GetAll(QueryParams queryParams);
    }
}
