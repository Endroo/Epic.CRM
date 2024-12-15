using Epic.CRM.Common;
using Epic.CRM.DataDomain.Helpers;
using Epic.CRM.DataDomain.Interfaces.Repositories;
using Epic.CRM.DataDomain.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Epic.CRM.DataDomain.Repositories
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(EpicCrmDbContext empDBContext) : base(empDBContext)
        {

        }

        public async Task<IEnumerable<AppUser>> GetAll(QueryParams queryParams)
        {
            var query = GetAll(new FindOptions { IsAsNoTracking = true });

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
                    case "profession":
                        query = queryParams.SortOrder == "desc" ?
                                query.OrderByDescending(x => x.Profession) :
                                query.OrderBy(x => x.Profession);
                        break;
                    case "email":
                        query = queryParams.SortOrder == "desc" ?
                                query.OrderByDescending(x => x.Email) :
                                query.OrderBy(x => x.Email);
                        break;
                    case "appUserId":
                    case "id":
                        query = queryParams.SortOrder == "desc" ?
                                query.OrderByDescending(x => x.AppUserId) :
                                query.OrderBy(x => x.AppUserId);
                        break;
                    case "isadmin":
                        query = queryParams.SortOrder == "desc" ?
                                query.OrderByDescending(x => x.IsAdmin) :
                                query.OrderBy(x => x.IsAdmin);
                        break;
                    default:
                        break;
                }
            }
            else
                query = query.OrderByDescending(x => x.AppUserId);


            if (queryParams.SearchString is not null)
            {
                query = query.Where(x => x.Name.Contains(queryParams.SearchString) ||
                                         x.Email.Contains(queryParams.SearchString) ||
                                         x.Profession.Contains(queryParams.SearchString));
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

        public AppUser GetById(int appUserId)
        {
            return FindOne(x => x.AppUserId == appUserId, new FindOptions { IsAsNoTracking = true });
        }

        public AppUser GetByIdentityId(string identityId)
        {
            return FindOne(x => x.AspNetUserId == identityId, new FindOptions { IsAsNoTracking = true });
        }
    }
}
