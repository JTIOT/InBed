
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
    /// RoleMenu业务契约
    /// </summary>
    public partial class RoleMenuService :  IDependency, IRoleMenuService
    {
        #region 构造函数注册上下文
        public IDbContextScopeFactory _dbScopeFactory { get; set; }

        //private readonly IDbContextScopeFactory _dbScopeFactory;

        //public RoleMenuService(IDbContextScopeFactory dbScopeFactory)
        //{
        //    _dbScopeFactory = dbScopeFactory;
        //}

        #endregion

        public Result<RoleMenuDto> AddRoleMenu(List<RoleMenuDto> dtos, int roleId)
        {
            Result<RoleMenuDto> res = new Result<RoleMenuDto>();
            try
            {
                RoleMenuDAL obj = new RoleMenuDAL();
                var entity = Mapper.Map<List<RoleMenuDto>, List<RoleMenuEntity>>(dtos);
                int count = 0;
                var roleEntity = obj.Query(roleId);
                if (roleEntity.Count > 0)
                {
                    if (obj.DelRoleMenu(roleId))
                    {
                        foreach (RoleMenuEntity item in entity)
                        {
                            if (obj.AddRoleMenu(item))
                            {
                                count++;
                            }

                        }

                    }
                }
                else {
                    foreach (RoleMenuEntity item in entity)
                    {
                        if (obj.AddRoleMenu(item))
                        {
                            count++;
                        }

                    }

                }
                if (obj.DelRoleMenu(roleId))
                {
                    foreach (RoleMenuEntity item in entity)
                    {
                        if (obj.AddRoleMenu(item))
                        {
                            count++;
                        }

                    }

                }
                if (count == dtos.Count)
                {
                    res.flag = true;
                    res.msg = "角色分配菜单成功！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = "角色分配菜单失败！";
            }
            return res;
        }

        public List<RoleMenuDto> Query(int roleId)
        {
            try
            {
                var entity = new RoleMenuDAL().Query(roleId);
                return Mapper.Map<List<RoleMenuEntity>, List<RoleMenuDto>>(entity);
            }
            catch (Exception ex)
            {
                return new List<RoleMenuDto>();
            }

        }
    }
}
