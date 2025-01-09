using Epic.CRM.BusinessLogic.Helpers;
using Epic.CRM.BusinessLogic.Interfaces;
using Epic.CRM.Common;
using Epic.CRM.DataDomain.Dtos;
using Epic.CRM.DataDomain.Helpers;
using Epic.CRM.DataDomain.Interfaces.Repositories;
using Epic.CRM.DataDomain.Models;
using Epic.CRM.DataDomain.Repositories;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Epic.CRM.BusinessLogic.Managers
{
    public class CustomerManager : ICustomerManager
    {
        private readonly IAppUserManager _userManager;
        private readonly ICustomerRepository _customerRepository;

        public CustomerManager(IAppUserManager userManager, ICustomerRepository customerRepository)
        {
            _userManager = userManager;
            _customerRepository = customerRepository;
        }

        public async Task<Result> CreateCustomer(string identityUserId, CustomerEditRegisterDto dto)
        {
            var result = new Result();
            try
            {
                result = EditCustomerValidation(dto);

                if (result.ResultStatus == ResultStatusEnum.Success)
                {
                    var getLoggedUserResult = await _userManager.GetByIdentityUserId(identityUserId);
                    if (getLoggedUserResult.ResultStatus == ResultStatusEnum.Fail)
                    {
                        result.Errors = getLoggedUserResult.Errors;
                        return result;
                    }

                    var loggedinUser = getLoggedUserResult.Data;

                    Customer customer = dto.Map();
                    customer.AppUserId = loggedinUser.AppUserId;
                    await _customerRepository.AddAsync(customer);
                }

                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ErrorCodes.common_error_internal_server_error);
            }

            return result;
        }

        public async Task<Result> DeleteCustomer(int customerId)
        {
            var result = new Result();
            try
            {
                var customer = _customerRepository.GetById(customerId, new FindOptions { IsIgnoreAutoIncludes = true });
                if (customer is not null)
                    _customerRepository.Delete(customer);
                else
                    result.Errors.Add(ErrorCodes.customer_error_no_customer_found);
            }
            catch (Exception ex)
            {
                result.Errors.Add(ErrorCodes.common_error_internal_server_error);
            }

            return result;
        }

        public async Task<Result> EditCustomer(int customerId, CustomerEditRegisterDto dto)
        {
            var result = new Result();
            try
            {
                var customer = _customerRepository.GetById(customerId);
                if (customer is not null)
                {
                    dto.Update(customer);

                    _customerRepository.Update(customer);
                }
                else
                    result.Errors.Add(ErrorCodes.customer_error_no_customer_found);

            }
            catch (Exception ex)
            {
                result.Errors.Add(ErrorCodes.common_error_internal_server_error);
            }

            return result;
        }

        public async Task<PageResult<IEnumerable<CustomerDto>>> GetAll(string identityUserId, QueryParams queryParams)
        {
            var result = new PageResult<IEnumerable<CustomerDto>>(queryParams);
            try
            {
                var getLoggedUserResult = await _userManager.GetByIdentityUserId(identityUserId);
                if (getLoggedUserResult.ResultStatus == ResultStatusEnum.Fail)
                {
                    result.Errors = getLoggedUserResult.Errors;
                    return result;
                }

                queryParams = queryParams ?? new QueryParams();

                var loggedinUser = getLoggedUserResult.Data;
                var customerList = await _customerRepository.GetAll(loggedinUser.AppUserId, queryParams);
                result.Data = customerList.Select(x => new CustomerDto().Map(x));
            }
            catch (Exception ex)
            {
                result.Errors.Add(ErrorCodes.common_error_internal_server_error);
            }

            return result;
        }

        public async Task<DataResult<CustomerDto>> GetById(int customerId)
        {
            var result = new DataResult<CustomerDto>();
            try
            {
                var customer =  _customerRepository.GetById(customerId);
                if (customer is null)
                    result.Errors.Add(ErrorCodes.customer_error_no_customer_found);

                result.Data = new CustomerDto().Map(customer);
            }
            catch (Exception ex)
            {
                result.Errors.Add(ErrorCodes.common_error_internal_server_error);
            }

            return result;
        }

        private Result EditCustomerValidation(CustomerEditRegisterDto dto)
        {
            var result = new Result();
            if (dto is null)
                result.Errors.Add(ErrorCodes.customer_error_invalid_form);

            if (string.IsNullOrWhiteSpace(dto.Email))
                result.Errors.Add(ErrorCodes.customer_error_invalid_email);
            if (string.IsNullOrWhiteSpace(dto.Name))
                result.Errors.Add(ErrorCodes.customer_error_invalid_name);

            return result;
        }
    }
}
