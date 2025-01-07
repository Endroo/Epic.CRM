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
    public interface ICustomerManager
    {
        Task<PageResult<IEnumerable<CustomerDto>>> GetAll(string identityUserId, QueryParams queryParams);
        Task<DataResult<CustomerDto>> GetById(int id);
        Task<Result> EditCustomer(int customerId, CustomerEditRegisterDto dto);
        Task<Result> DeleteCustomer(int customerId);
        Task<Result> CreateCustomer(string identityUserId, CustomerEditRegisterDto dto);
    }
}
