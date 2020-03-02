/*******************************************************************************
* Copyright (C) InBed.Com
* 
* Author: dj.wong
* Create Date: 09/04/2015 11:47:14
* Description: Automated building by service@InBed.com 
* 
* Revision History:
* Date         Author               Description
*
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using AutoMapper;
using EntityFramework.Extensions;
using InBed.Core;
using InBed.Core.Extentions;
using InBed.Core.Log;
using InBed.Data;
using InBed.Entity;
using InBed.Service.Dto;
using InBed.Service.Enum;
using InBed.Data.DAL;
using InBed.Service.Request;

namespace InBed.Service.Abstracts
{
    public partial class UserService : IDependency, IUserService
    {
        public IMenuService menuService { get; set; }

        public IRoleService roleService { get; set; }


        public IUserRoleService userRoleService { get; set; }

        public IRoleMenuService roleMenuService { get; set; }

        public ILoginLogService loginLogService { get; set; }

        public Result<UserDto> AddUser(UserDto model)
        {
            Result<UserDto> R_model = new Result<UserDto>();
            try
            {
                var entity = Mapper.Map<UserDto, UserEntity>(model);
                if (StringExtension.IsBlank(entity.LoginName))
                {
                    return new Result<UserDto>() { flag = false, msg = "添加登录用户名，不能为空！" };
                }
                if (StringExtension.IsBlank(entity.Password))
                {
                    return new Result<UserDto>() { flag = false, msg = "添加登录用户密码，不能为空！" };
                }
                var flag = new UserDAL().AddUser(entity);
                if (flag)
                {
                    R_model.flag = true;
                }
                else
                {
                    R_model.flag = false;
                    R_model.msg = "添加用户失败";
                }

            }
            catch (Exception ex)
            {
                R_model.flag = false;
                R_model.msg = ex.Message;
            }
            return R_model;


        }

        public Result<UserDto> DelUser(List<int> ids)
        {
            Result<UserDto> R_model = new Result<UserDto>();
            try
            {
                if (new UserDAL().DelUser(ids))
                {
                    R_model.flag = true;
                    R_model.msg = "系统用户删除成功！";
                }
                else
                {
                    R_model.flag = false;
                    R_model.msg = "系统用户删除失败！";
                }
            }
            catch (Exception ex)
            {
                R_model.flag = false;
                R_model.msg = ex.Message;
            }
            return R_model;
        }

        public Result<UserDto> DelUser(int id)
        {
            Result<UserDto> R_model = new Result<UserDto>();
            try
            {
                if (new UserDAL().DelUser(id))
                {
                    R_model.flag = true;
                    R_model.msg = "系统用户删除成功！";
                }
                else
                {
                    R_model.flag = false;
                    R_model.msg = "系统用户删除失败！";
                }
            }
            catch (Exception ex)
            {
                R_model.flag = false;
                R_model.msg = ex.Message;
            }
            return R_model;
        }

        public Result<UserDto> EditUser(UserDto model)
        {
            Result<UserDto> R_model = new Result<UserDto>();
            try
            {
                var entity = Mapper.Map<UserDto, UserEntity>(model);
                var flag = new UserDAL().EditUser(entity);
                if (flag)
                {
                    R_model.flag = true;
                    R_model.msg = "系统用户编辑成功！";
                }
                else
                {
                    R_model.flag = false;
                    R_model.msg = "系统用户编辑失败！";
                }
            }
            catch (Exception ex)
            {
                R_model.flag = false;
                R_model.msg = ex.Message;
            }
            return R_model;
        }

        public UserDto GetDtail(int id)
        {
            try
            {
                var userDb = new UserDAL().Details(id);
                return Mapper.Map<UserEntity, UserDto>(userDb);
            }
            catch (Exception ex)
            {
                return new UserDto();
            }
        }

        public ResultDto<UserDto> GetWithPages(GetUserRequest request)
        {
            ResultDto<UserDto> R_model = new ResultDto<UserDto>();
            try
            {
                int count = 0;
                var entity = new UserDAL().GetWithPages(request.Start, request.Length, request.FacilitatorCode, request.UserName, request.RealName,
                                                                        request.StartTime, request.EndTime, request.OrderBy, out count, request.OrderDir);
                R_model.recordsTotal = count;
                R_model.data = Mapper.Map<List<UserEntity>, List<UserDto>>(entity);
            }
            catch (Exception ex)
            {
                R_model.recordsTotal = 0;
                R_model.data = new List<UserDto>();
            }
            return R_model;
        }
        public List<MenuDto> GetMyMenus(int userId)
        {
            try
            {
                //获取我的角色
                var userRoles = new UserRoleDAL().GetUserRole(userId);
                var roleIds = userRoles.Select(item => item.RoleId).FirstOrDefault();
                //获取我的角色权限
                var roleMenus = new RoleMenuDAL().Query(roleIds);
                var menuIds = roleMenus.Select(item => item.MenuId).Distinct();
                var entity = new MenuDAL().Query(menuIds.ToList());
                return Mapper.Map<List<MenuEntity>, List<MenuDto>>(entity);
            }
            catch (Exception ex)
            {
                return new List<MenuDto>();
            }
        }

        public Result<UserDto> Login(UserDto dto)
        {
            Result<UserDto> res = new Result<UserDto>();
            try
            {
                var user = new UserDAL().GetUser(dto.LoginName);
                var userDto = Mapper.Map<UserEntity, UserDto>(user);
                if (user == null)
                    res.msg = "无效的用户";
                else
                {
                    //loginLogService.Add(new LoginLogDto
                    //{
                    //    UserId = user.Id,
                    //    LoginName = user.LoginName,
                    //    IP = WebHelper.GetClientIP(),
                    //    Mac = WebHelper.GetClientMACAddress()
                    //});
                    if (userDto.Password != dto.Password.ToMD5())
                        res.msg = "登录密码错误";
                    else if (user.IsDeleted)
                        res.msg = "用户已被删除";
                    else if (userDto.Status == UserStatus.未激活)
                        res.msg = "账号未被激活";
                    else if (userDto.Status == UserStatus.禁用)
                        res.msg = "账号被禁用";
                    else
                    {
                        res.flag = true;
                        res.msg = "登录成功";
                        res.data = userDto;

                        string onlineUser = user.FacilitatorCode + "#" + user.FacilitatorName + "#" + user.LoginName;
                        //写入注册信息
                        DateTime expiration = dto.IsRememberMe
                            ? DateTime.Now.AddDays(7)
                            : DateTime.Now.Add(FormsAuthentication.Timeout);
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2,
                            onlineUser,
                            DateTime.Now,
                            expiration,
                            true,
                            user.Id.ToString(),
                            FormsAuthentication.FormsCookiePath);

                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                         FormsAuthentication.Encrypt(ticket))
                        {
                            HttpOnly = true,
                            Expires = expiration,
                        };
                        HttpContext.Current.Response.Cookies.Add(cookie);
                    }
                }
            }
            catch (Exception ex)
            {
                res.msg = ex.Message;
                res.flag = false;
            }
            return res;
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
            var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        public ResultDto<RoleDto> GetMyRoles(RoleRequest query, int userId)
        {
            try
            {
                var userRoleDb = userRoleService.Query(userId).OrderBy(item => item.CreateDateTime).ToList();
                if (userRoleDb.Count > 0)
                {
                    var roleIds = userRoleDb.Select(item => item.RoleId).Distinct().ToList();

                    RoleRequest quest = new RoleRequest
                    {
                        Name = query.Name,
                        Start = query.Start,
                        Length = query.Length,
                        OrderBy = "id",
                        OrderDir = "desc",
                    };
                    var roleDb = roleService.Query(roleIds, query.FacilitatorCode);
                    return roleDb;
                }
                else
                {
                    var dto = new ResultDto<RoleDto>
                    {
                        recordsTotal = 0,
                        data = new List<RoleDto>()
                    };
                    return dto;
                }
            }
            catch (Exception ex)
            {
                var dto = new ResultDto<RoleDto>
                {
                    recordsTotal = 0,
                    data = new List<RoleDto>()
                };
                return dto;
            }
        }

        public ResultDto<RoleDto> GetNotMyRoles(RoleRequest query, int userId)
        {
            try
            {
                var userRoleDb = userRoleService.Query(userId).OrderBy(item => item.CreateDateTime).ToList();

                var roleIds = userRoleDb.Select(item => item.RoleId).Distinct().ToList();
                var roleDb = roleService.Query(new List<int>(), query.FacilitatorCode);
                var allIds = roleDb.data.Select(item => item.Id).Distinct().ToList();

                var Notids = allIds.Union(roleIds).Except(allIds.Intersect(roleIds)).ToList();
                var NotMyRoleDb = roleService.Query(Notids, query.FacilitatorCode);
                return NotMyRoleDb;

                //var dto = new ResultDto<RoleDto>
                //{
                //    recordsTotal = 0,
                //    data = new List<RoleDto>()
                //};
                // return dto;


            }
            catch (Exception ex)
            {
                var dto = new ResultDto<RoleDto>
                {
                    recordsTotal = 0,
                    data = new List<RoleDto>()
                };
                return dto;
            }

        }


    }
}
