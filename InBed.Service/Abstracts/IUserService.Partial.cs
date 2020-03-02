using InBed.Service.Request;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using InBed.Service.Dto;

namespace InBed.Service.Abstracts
{ 
	/// <summary>
    /// User业务契约
    /// </summary>
    public partial interface IUserService
    {

        ResultDto<UserDto> GetWithPages(GetUserRequest request);
        /// <summary>
        /// 根据id获取单条用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserDto GetDtail(int id);
        /// <summary>
        /// 添加系统登录用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Result<UserDto> AddUser(UserDto model);
        /// <summary>
        /// 编辑系统登录用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Result<UserDto> EditUser(UserDto model);
        /// <summary>
        /// 删除单个用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<UserDto> DelUser(int id);
        /// <summary>
        /// 同事删除多个用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Result<UserDto> DelUser(List<int> ids);

    } 
}
