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

namespace Epic.CRM.BusinessLogic.Managers
{
    public class WorkManager : IWorkManager
    {
        private readonly IAppUserManager _userManager;
        private readonly IWorkRepository _workRepository;

        public WorkManager(IAppUserManager userManager, IWorkRepository workRepository)
        {
            _userManager = userManager;
            _workRepository = workRepository;
        }

        public async Task<Result> CreateWork(string identityUserId, WorkEditRegisterDto dto)
        {
            var result = new Result();
            try
            {
                result = EditWorkValidation(dto);

                if (result.ResultStatus == ResultStatusEnum.Success)
                {
                    var getLoggedUserResult = await _userManager.GetByIdentityUserId(identityUserId);
                    if (getLoggedUserResult.ResultStatus == ResultStatusEnum.Fail)
                    {
                        result.Errors = getLoggedUserResult.Errors;
                        return result;
                    }

                    var loggedinUser = getLoggedUserResult.Data;

                    Work work = dto.Map();
                    work.AppUserId = loggedinUser.AppUserId;
                    await _workRepository.AddAsync(work);
                }

                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ErrorCodes.common_error_internal_server_error);
            }

            return result;
        }

        public async Task<Result> DeleteWork(int workId)
        {
            var result = new Result();
            try
            {
                var work = _workRepository.GetById(workId, new FindOptions { IsIgnoreAutoIncludes = true });
                if (work is not null)
                    _workRepository.Delete(work);
                else
                    result.Errors.Add(ErrorCodes.work_error_no_customer_found);
            }
            catch (Exception ex)
            {
                result.Errors.Add(ErrorCodes.common_error_internal_server_error);
            }

            return result;
        }

        public async Task<Result> EditWork(int workId, WorkEditRegisterDto dto)
        {
            var result = new Result();
            try
            {
                var work = _workRepository.GetById(workId);
                if (work is not null)
                {
                    dto.Update(work);

                    _workRepository.Update(work);
                }
                else
                    result.Errors.Add(ErrorCodes.work_error_no_customer_found);

            }
            catch (Exception ex)
            {
                result.Errors.Add(ErrorCodes.common_error_internal_server_error);
            }

            return result;
        }

        public async Task<PageResult<IEnumerable<WorkDto>>> GetAll(string identityUserId, QueryParams queryParams)
        {
            var result = new PageResult<IEnumerable<WorkDto>>(queryParams);
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
                var workList = await _workRepository.GetAll(loggedinUser.AppUserId, queryParams);
                result.Data = workList.Select(x => new WorkDto().Map(x));
            }
            catch (Exception ex)
            {
                result.Errors.Add(ErrorCodes.common_error_internal_server_error);
            }

            return result;
        }

        private Result EditWorkValidation(WorkEditRegisterDto dto)
        {
            var result = new Result();
            if (dto is null)
                result.Errors.Add(ErrorCodes.work_error_invalid_form);

            if (string.IsNullOrWhiteSpace(dto.Name))
                result.Errors.Add(ErrorCodes.work_error_invalid_name);

            return result;
        }
    }
}
