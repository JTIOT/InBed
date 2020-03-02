using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using InBed.Service.Dto;
using InBed.Service.Request;

namespace InBed.Service.Abstracts
{ 
	/// <summary>
    /// Role业务契约
    /// </summary>
    public partial interface IRoleService
    {
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Result<RoleDto> AddRole(RoleDto model);
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Result<RoleDto> EditRole(RoleDto model);
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<RoleDto> DelRole(int id);
        /// <summary>
        /// 详细星信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RoleDto Detail(int id);
        /// <summary>
        /// 删除多个角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Result<RoleDto> DelRole(List<int> ids);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ResultDto<RoleDto> GetWithPages(RoleRequest request);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        ResultDto<RoleDto> Query(List<int> ids, string f_code);
    }
}
