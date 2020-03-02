using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using InBed.Service.Dto;
using InBed.Service.Request;

namespace InBed.Service.Abstracts
{ 
	/// <summary>
    /// UserRole业务契约
    /// </summary>
    public partial interface IUserRoleService
    {
        /// <summary>
        /// 根据用户id获取角色
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        ResultDto<UserRoleDto> GetUserRole(int userID);
        /// <summary>
        /// 添加用户角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Result<UserRoleDto> AddUserRole(List<UserRoleDto> models);
        /// <summary>
        /// 修改用户角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Result<UserRoleDto> EditUserRole(UserRoleDto model);
        /// <summary>
        /// 删除用户角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <summary>

        Result<UserRoleDto> DelUserRole(int roleid, int userid);
        /// <summary>
        /// 同事删除多个用户角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Result<UserRoleDto> DelUserRole(List<int> ids);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<UserRoleDto> Query(int userId);
    } 
}
