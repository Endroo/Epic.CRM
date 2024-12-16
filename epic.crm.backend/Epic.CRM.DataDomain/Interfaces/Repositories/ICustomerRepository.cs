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
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetAll(int appUserId, QueryParams queryParams);
        Customer GetById(int customerId, FindOptions findOptions = null);
    }
}
