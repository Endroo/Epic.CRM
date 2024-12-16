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
        Task<PageResult<IEnumerable<CustomerDto>>> GetAll(QueryParams queryParams);
        Task<Result> EditCustomer(int customerId, CustomerRegisterDto dto);
        Task<Result> DeleteCustomer(int customerId);
        Task<Result> CreateCustomer(CustomerRegisterDto dto);
    }
}
