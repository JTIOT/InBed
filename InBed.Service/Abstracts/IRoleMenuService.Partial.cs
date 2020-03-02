using InBed.Service.Request;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using InBed.Service.Dto;

namespace InBed.Service.Abstracts
{ 
	/// <summary>
    /// RoleMenu业务契约
    /// </summary>
    public partial interface IRoleMenuService
    {
        /// <summary>
        /// 给角色分配菜单
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        Result<RoleMenuDto> AddRoleMenu(List<RoleMenuDto> dtos, int roleId);
        /// <summary>
        /// 根据角色获取菜单列表
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        List<RoleMenuDto> Query(int roleId);
    } 
}
