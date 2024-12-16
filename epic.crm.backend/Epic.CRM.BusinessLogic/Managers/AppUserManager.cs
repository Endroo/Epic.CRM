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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppUserManager(UserManager<IdentityUser> userManager, IAppUserRepository appUserRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _appUserRepository = appUserRepository;
            _httpContextAccessor = httpContextAccessor;
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
                            result.Errors.AddRange(registerUserResult.Errors.Select(x => x.Description));
                            return result;
                        }

                        List<string> roles = new List<string> { "User" };
                        if (dto.IsAdmin) roles.Add("Admin");

                        var addToRoleResult = await _userManager.AddToRolesAsync(identityUser, roles);

                        if (!addToRoleResult.Succeeded)
                        {
                            result.Errors.AddRange(addToRoleResult.Errors.Select(x => x.Description));
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
                result.Errors.Add(ex.ToString());
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
                    var identityUser = await _userManager.FindByIdAsync(appUser.AspNetUserId);
                    var loggedInUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

                    if(loggedInUser.Id == identityUser.Id)
                    {
                        result.Errors.Add($"Can not delete logged in user.");
                        return result;
                    }

                    if (appUser is not null)
                        _appUserRepository.Delete(appUser);
                    else
                    {
                        result.Errors.Add($"No user found");
                        return result;
                    }

                    if (identityUser is not null)
                    {
                        var deleteResult = await _userManager.DeleteAsync(identityUser);
                        if(!deleteResult.Succeeded)
                        {
                            result.Errors.AddRange(deleteResult.Errors.Select(x => x.Description));
                        }     
                    }                        
                    else
                    {
                        result.Errors.Add($"No user found");
                        return result;
                    }

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.ToString());
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
                                    result.Errors.Add("User not found.");
                                    return result;
                                }

                                if (appUser.IsAdmin && !dto.IsAdmin.Value)
                                {
                                    var removeResult = await _userManager.RemoveFromRoleAsync(identityUser, "Admin");
                                    if (!removeResult.Succeeded)
                                    {
                                        result.Errors.AddRange(removeResult.Errors.Select(x => x.Description));
                                        return result;
                                    }
                                }
                                else if (!appUser.IsAdmin && dto.IsAdmin.Value)
                                {
                                    var addToRoleResult = await _userManager.AddToRoleAsync(identityUser, "Admin");

                                    if (!addToRoleResult.Succeeded)
                                    {
                                        result.Errors.AddRange(addToRoleResult.Errors.Select(x => x.Description));
                                        return result;
                                    }
                                }
                            }

                            appUser.Name = dto.Name;
                            appUser.Profession = dto.Profession;
                            appUser.IsAdmin = dto.IsAdmin.Value;

                            _appUserRepository.Update(appUser);

                        }
                        else
                            result.Errors.Add($"No user found");

                        scope.Complete();   
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.ToString());
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
                result.Errors.Add(ex.ToString());
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
                        result.Errors.Add($"No user found");
                }
                else
                    result.Errors.Add($"No user found");
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.ToString());
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
                        result.Errors.Add($"No user found. Username: {identityUser.Email}");
                }
                else
                    result.Errors.Add("No user found");
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.ToString());
            }

            return result;
        }

        public async Task<DataResult<AppUserDto>> GetLoggedInUser()
        {
            var result = new DataResult<AppUserDto>();
            try
            {
                ClaimsPrincipal userPrincipal = _httpContextAccessor.HttpContext.User;

                var identityUser = await _userManager.GetUserAsync(userPrincipal);

                if (identityUser is not null)
                {
                    var appUser = _appUserRepository.GetByIdentityId(identityUser.Id, new FindOptions { IsAsNoTracking = true });
                    if (appUser is not null)
                        result.Data = new AppUserDto().Map(appUser);
                    else
                        result.Errors.Add($"No user found. Username: {identityUser.Email}");
                }
                else
                    result.Errors.Add("User is not logged in.");
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.ToString());
            }

            return result;
        }

        private Result CreateUserValidation(AppUserRegisterDto dto)
        {
            var result = new Result();
            if (dto is null)
                result.Errors.Add("Invalid or missing register form");

            var identityUser = _userManager.FindByNameAsync(dto.Email).Result;

            if (identityUser is not null)
                result.Errors.Add($"User already registered. Username: {dto.Email}");
            if (string.IsNullOrWhiteSpace(dto.Email))
                result.Errors.Add("Invalid or missing email");
            if (string.IsNullOrWhiteSpace(dto.Name))
                result.Errors.Add("Invalid or missing name");
            if (string.IsNullOrWhiteSpace(dto.Profession))
                result.Errors.Add("Invalid or missing profession");

            return result;
        }

        private Result EditUserValidation(AppUserEditDto dto)
        {
            var result = new Result();

            if (dto is null)
                result.Errors.Add("Invalid or missing edit form");
            if (string.IsNullOrWhiteSpace(dto.Name))
                result.Errors.Add("Invalid or missing name");
            if (string.IsNullOrWhiteSpace(dto.Profession))
                result.Errors.Add("Invalid or missing profession");
            if (dto.IsAdmin is null)
                result.Errors.Add("Missing Admin information");

            return result;
        }
    }

}
