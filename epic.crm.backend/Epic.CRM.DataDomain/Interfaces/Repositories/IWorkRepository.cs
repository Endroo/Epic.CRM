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
    public interface IWorkRepository : IRepository<Work>
    {
        Task<IEnumerable<Work>> GetAll(int appUserId, QueryParams queryParams);
        Work GetById(int customerId, FindOptions findOptions = null);
    }
}
