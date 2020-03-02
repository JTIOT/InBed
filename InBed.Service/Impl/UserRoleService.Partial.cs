
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using EntityFramework.Extensions;
using AutoMapper;
using InBed.Core;
using InBed.Core.Extentions;
using InBed.Entity;
using InBed.Service.Dto;
using Mehdime.Entity;
using InBed.Data.DAL;

namespace InBed.Service.Abstracts
{ 
	/// <summary>
    /// UserRole业务契约
    /// </summary>
    public partial class UserRoleService : IDependency, IUserRoleService
    {
		#region 构造函数注册上下文
		public IDbContextScopeFactory _dbScopeFactory {get;set;}

        //private readonly IDbContextScopeFactory _dbScopeFactory;

        //public UserRoleService(IDbContextScopeFactory dbScopeFactory)
        //{
        //    _dbScopeFactory = dbScopeFactory;
        //}

        #endregion

        public Result<UserRoleDto> AddUserRole(List<UserRoleDto> models)
        {
            Result<UserRoleDto> res = new Result<UserRoleDto>();
            try
            {
                foreach (var model in models)
                {
                    var entity = Mapper.Map<UserRoleDto, UserRoleEntity>(model);
                    if (entity.UserId <= 0)
                    {
                        return new Result<UserRoleDto>() { flag = false, msg = "请选择用户！" };
                    }
                    if (entity.RoleId <= 0)
                    {
                        return new Result<UserRoleDto>() { flag = false, msg = "请选择角色！" };
                    }
                    var flag = new UserRoleDAL().AddUserRole(entity);
                    if (flag)
                    {
                        res.flag = true;
                    }
                    else
                    {
                        res.flag = false;
                        res.msg = "用户分配角色失败！";
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public Result<UserRoleDto> DelUserRole(List<int> ids)
        {
            Result<UserRoleDto> res = new Result<UserRoleDto>();
            try
            {
                if (new UserRoleDAL().DelUserRole(ids))
                {
                    res.flag = true;
                    res.msg = "用户角色删除成功！";
                }
                else
                {
                    res.flag = false;
                    res.msg = "用户角色删除失败！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public Result<UserRoleDto> DelUserRole(int roleid, int userid)
        {
            Result<UserRoleDto> res = new Result<UserRoleDto>();
            try
            {
                if (new UserRoleDAL().DelUserRole(roleid, userid))
                {
                    res.flag = true;
                    res.msg = "用户角色删除成功！";
                }
                else
                {
                    res.flag = false;
                    res.msg = "用户角色删除失败！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public Result<UserRoleDto> EditUserRole(UserRoleDto model)
        {
            Result<UserRoleDto> res = new Result<UserRoleDto>();
            try
            {
                var entity = Mapper.Map<UserRoleDto, UserRoleEntity>(model);
                var flag = new UserRoleDAL().EditUserRole(entity);
                if (flag)
                {
                    res.flag = true;
                    res.msg = "用户角色编辑成功！";
                }
                else
                {
                    res.flag = false;
                    res.msg = "用户角色编辑失败！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public ResultDto<UserRoleDto> GetUserRole(int userID)
        {
            ResultDto<UserRoleDto> res = new ResultDto<UserRoleDto>();
            try
            {
                var entity = new UserRoleDAL().GetUserRole(userID);
                res.recordsTotal = entity.Count;
                res.data = Mapper.Map<List<UserRoleEntity>, List<UserRoleDto>>(entity);
            }
            catch (Exception ex)
            {
                res.recordsTotal = 0;
                res.data = new List<UserRoleDto>();
            }

            return res;

        }
        public List<UserRoleDto> Query(int userId)
        {
            return Mapper.Map<List<UserRoleEntity>, List<UserRoleDto>>(new UserRoleDAL().Query(userId));
        }
    } 
}
