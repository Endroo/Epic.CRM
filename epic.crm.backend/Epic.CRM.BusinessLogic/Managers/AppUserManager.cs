using Azure.Core;

using Epic.CRM.BusinessLogic.Helpers;
using Epic.CRM.BusinessLogic.Interfaces;
using Epic.CRM.Common;
using Epic.CRM.DataDomain.Dtos;
using Epic.CRM.DataDomain.Helpers;
using Epic.CRM.DataDomain.Interfaces.Repositories;
using Epic.CRM.DataDomain.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Epic.CRM.BusinessLogic.Managers
{

    public class AppUserManager : IAppUserManager
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAppUserRepository _appUserRepository;

        public AppUserManager(UserManager<IdentityUser> userManager, IAppUserRepository appUserRepository)
        {
            _userManager = userManager;
            _appUserRepository = appUserRepository;
        }

        public async Task<Result> CreateUser(AppUserRegisterDto dto)
        {
            var result = new Result();
            try
            {
                result = CreateUserValidation(dto);
                if (result.ResultStatus == ResultStatusEnum.Success)
                {
                    IdentityUser identityUser = new IdentityUser
                    {
                        UserName = dto.Email,
                        Email = dto.Email,
                    };

                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var registerUserResult = await _userManager.CreateAsync(identityUser, dto.Password);

                        if (!registerUserResult.Succeeded)
                        {
                            result.Errors.Add(ErrorCodes.identity_error_cant_create_user);
                            return result;
                        }

                        List<string> roles = new List<string> { "User" };
                        if (dto.IsAdmin) roles.Add("Admin");

                        var addToRoleResult = await _userManager.AddToRolesAsync(identityUser, roles);

                        if (!addToRoleResult.Succeeded)
                        {
                            result.Errors.Add(ErrorCodes.identity_error_cant_add_to_role);
                            return result;
                        }

                        AppUser appUser = dto.Map();
                        appUser.AspNetUserId = identityUser.Id;

                        await _appUserRepository.AddAsync(appUser);

                        scope.Complete();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ErrorCodes.common_error_internal_server_error);
            }

            return result;
        }

        public async Task<Result> DeleteUser(int appUserId)
        {
            var result = new Result();
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var appUser = _appUserRepository.GetById(appUserId, new FindOptions { IsIgnoreAutoIncludes = true });

                    if (appUser is not null)
                        _appUserRepository.Delete(appUser);
                    else
                    {
                        result.Errors.Add(ErrorCodes.account_error_no_user_found);
                        return result;
                    }

                    var identityUser = await _userManager.FindByIdAsync(appUser.AspNetUserId);

                    if (identityUser is not null)
                    {
                        var deleteResult = await _userManager.DeleteAsync(identityUser);
                        if(!deleteResult.Succeeded)
                        {
                            result.Errors.Add(ErrorCodes.identity_error_cant_delete_user);
                        }     
                    }                        
                    else
                    {
                        result.Errors.Add(ErrorCodes.account_error_no_user_found);
                        return result;
                    }

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(ErrorCodes.common_error_internal_server_error);
            }

            return result;
        }

        public async Task<Result> EditUser(int appUserId, AppUserEditDto dto)
        {
            var result = new Result();
            try
            {
                result = EditUserValidation(dto);
                if (result.ResultStatus == ResultStatusEnum.Success)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var appUser = _appUserRepository.GetById(appUserId, new FindOptions { IsIgnoreAutoIncludes = true });
                        if (appUser is not null)
                        {
                            if (appUser.IsAdmin != dto.IsAdmin)
                            {
                                var identityUser = await _userManager.FindByIdAsync(appUser.AspNetUserId);
                                if (identityUser is null)
                                {
                                    result.Errors.Add(ErrorCodes.account_error_no_user_found);
                                    return result;
                                }

                                if (appUser.IsAdmin && !dto.IsAdmin.Value)
                                {
                                    var removeResult = await _userManager.RemoveFromRoleAsync(identityUser, "Admin");
                                    if (!removeResult.Succeeded)
                                    {
                                        result.Errors.Add(ErrorCodes.identity_error_cant_remove_from_role);
                                        return result;
                                    }
                                }
                                else if (!appUser.IsAdmin && dto.IsAdmin.Value)
                                {
                                    var addToRoleResult = await _userManager.AddToRoleAsync(identityUser, "Admin");

                                    if (!addToRoleResult.Succeeded)
                                    {
                                        result.Errors.Add(ErrorCodes.identity_error_cant_add_to_role);
                                        return result;
                                    }
                                }
                            }

                            dto.Update(appUser);

                            _appUserRepository.Update(appUser);

                        }
                        else
                            result.Errors.Add(ErrorCodes.account_error_no_user_found);

                        scope.Complete();   
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ErrorCodes.common_error_internal_server_error);
            }

            return result;
        }

        public async Task<PageResult<IEnumerable<AppUserDto>>> GetAll(QueryParams queryParams)
        {
            var result = new PageResult<IEnumerable<AppUserDto>>(queryParams);
            try
            {
                queryParams = queryParams ?? new QueryParams();
                var appUserList = await _appUserRepository.GetAll(queryParams);
                result.Data = appUserList.Select(x => new AppUserDto().Map(x));
            }
            catch (Exception ex)
            {
                result.Errors.Add(ErrorCodes.common_error_internal_server_error);
            }

            return result;
        }

        public async Task<DataResult<AppUserDto>> GetById(int appUserId)
        {
            var result = new DataResult<AppUserDto>();
            try
            {
                var appUser = _appUserRepository.GetById(appUserId, new FindOptions { IsAsNoTracking = true });
                if (appUser is not null)
                {
                    var identityUser = await _userManager.FindByIdAsync(appUser.AspNetUserId);
                    if (identityUser is not null)
                        result.Data = new AppUserDto().Map(appUser);
                    else
                        result.Errors.Add(ErrorCodes.account_error_no_user_found);
                }
                else
                    result.Errors.Add(ErrorCodes.account_error_no_user_found);
            }
            catch (Exception ex)
            {
                result.Errors.Add(ErrorCodes.common_error_internal_server_error);
            }

            return result;
        }

        /// <summary>
        /// Get by Identity user id. No include.
        /// </summary>
        /// <param name="identityUserId"></param>
        /// <returns></returns>
        public async Task<DataResult<AppUserDto>> GetByIdentityUserId(string identityUserId)
        {
            var result = new DataResult<AppUserDto>();
            try
            {
                var appUser = _appUserRepository.GetByIdentityId(identityUserId, new FindOptions { IsAsNoTracking = true });
                if (appUser is not null)
                {
                    result.Data = new AppUserDto().Map(appUser);
                }
                else
                    result.Errors.Add(ErrorCodes.account_error_no_user_found);
            }
            catch (Exception ex)
            {
                result.Errors.Add(ErrorCodes.common_error_internal_server_error);
            }

            return result;
        }

        public async Task<DataResult<AppUserDto>> GetByUserName(string userName)
        {
            var result = new DataResult<AppUserDto>();
            try
            {
                var identityUser = await _userManager.FindByNameAsync(userName);

                if (identityUser is not null)
                {
                    var appUser = _appUserRepository.GetByIdentityId(identityUser.Id, new FindOptions { IsAsNoTracking = true });
                    if (appUser is not null)
                        result.Data = new AppUserDto().Map(appUser);
                    else
                        result.Errors.Add(ErrorCodes.account_error_no_user_found);
                }
                else
                    result.Errors.Add(ErrorCodes.account_error_no_user_found);
            }
            catch (Exception ex)
            {
                result.Errors.Add(ErrorCodes.common_error_internal_server_error);
            }

            return result;
        }

        //public async Task<DataResult<AppUserDto>> GetLoggedInUser(string identityUserId)
        //{
        //    var result = new DataResult<AppUserDto>();
        //    try
        //    {
        //        if (!string.IsNullOrWhiteSpace(identityUserId))
        //        {
        //            var appUser = _appUserRepository.GetByIdentityId(identityUserId, new FindOptions { IsAsNoTracking = true });
        //            if (appUser is not null)
        //                result.Data = new AppUserDto().Map(appUser);
        //            else
        //                result.Errors.Add($"No user found. Username: {appUser.Email}");
        //        }
        //        else
        //            result.Errors.Add("User is not logged in.");
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Errors.Add(ErrorCodes.common_error_internal_server_error);
        //    }

        //    return result;
        //}

        private Result CreateUserValidation(AppUserRegisterDto dto)
        {
            var result = new Result();
            if (dto is null)
                result.Errors.Add(ErrorCodes.user_error_invalid_form);

            var identityUser = _userManager.FindByNameAsync(dto.Email).Result;

            if (identityUser is not null)
                result.Errors.Add(ErrorCodes.user_error_user_already_registered);
            if (string.IsNullOrWhiteSpace(dto.Email))
                result.Errors.Add(ErrorCodes.user_error_invalid_email);
            if (string.IsNullOrWhiteSpace(dto.Name))
                result.Errors.Add(ErrorCodes.user_error_invalid_name);
            if (string.IsNullOrWhiteSpace(dto.Profession))
                result.Errors.Add(ErrorCodes.user_error_invalid_profession);

            return result;
        }

        private Result EditUserValidation(AppUserEditDto dto)
        {
            var result = new Result();

            if (dto is null)
                result.Errors.Add(ErrorCodes.user_error_invalid_form);
            if (string.IsNullOrWhiteSpace(dto.Name))
                result.Errors.Add(ErrorCodes.user_error_invalid_name);
            if (string.IsNullOrWhiteSpace(dto.Profession))
                result.Errors.Add(ErrorCodes.user_error_invalid_profession);
            if (dto.IsAdmin is null)
                result.Errors.Add(ErrorCodes.user_error_invalid_role);

            return result;
        }
    }

}
