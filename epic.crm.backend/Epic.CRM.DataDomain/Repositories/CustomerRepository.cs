using Epic.CRM.Common;
using Epic.CRM.DataDomain.Helpers;
using Epic.CRM.DataDomain.Interfaces.Repositories;
using Epic.CRM.DataDomain.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.CRM.DataDomain.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly EpicCrmDbContext _epicCRMDBContext;
        public CustomerRepository(EpicCrmDbContext epicCRMDBContext) : base(epicCRMDBContext)
        {
            _epicCRMDBContext = epicCRMDBContext;
        }

        public async Task<IEnumerable<Customer>> GetAll(int appUserId, QueryParams queryParams)
        {
            var query = GetAll(new FindOptions { IsAsNoTracking = true })
                        .Where(x => x.AppUserId == appUserId);
            int pageIndex = 0;

            if (queryParams.SortColumn is not null)
            {
                switch (queryParams.SortColumn.ToLower())
                {
                    case "name":
                        query = queryParams.SortOrder == "desc" ?
                                query.OrderByDescending(x => x.Name) :
                                query.OrderBy(x => x.Name);
                        break;
                    case "email":
                        query = queryParams.SortOrder == "desc" ?
                                query.OrderByDescending(x => x.Email) :
                                query.OrderBy(x => x.Email);
                        break;
                    case "id":
                        query = queryParams.SortOrder == "desc" ?
                                query.OrderByDescending(x => x.CustomerId) :
                                query.OrderBy(x => x.AppUserId);
                        break;
                    default:
                        query = query.OrderByDescending(x => x.CustomerId);
                        break;
                }
            }
            else
                query = query.OrderByDescending(x => x.AppUserId);


            if (queryParams.SearchString is not null)
            {
                query = query.Where(x => x.Name.Contains(queryParams.SearchString) ||
                                         x.Email.Contains(queryParams.SearchString));
            }


            if (queryParams.PageIndex.HasValue)
                pageIndex = queryParams.PageIndex.Value;
            if (queryParams.PageSize.HasValue)
            {
                var pageSize = queryParams.PageSize.Value;
                query = query.Skip(pageSize * pageIndex).Take(pageSize);
            }

            return await query.ToListAsync();
        }

        public Customer GetById(int customerId, FindOptions findOptions = null)
        {
            return FindOne(x => x.CustomerId == customerId, findOptions);
        }
    }
}
