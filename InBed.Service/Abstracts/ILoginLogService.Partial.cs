using InBed.Service.Request;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using InBed.Service.Dto;

namespace InBed.Service.Abstracts
{ 
	/// <summary>
    /// LoginLog业务契约
    /// </summary>
    public partial interface ILoginLogService
    {
        /// <summary>
        /// 删除单个日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<LoginLogDto> DelLog(int id);
        /// <summary>
        /// 删除多个日志
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Result<LoginLogDto> DelLog(List<int> ids);
        /// <summary>
        /// 添加登录日志
        /// </summary>
        /// <param name="loginlog"></param>
        /// <returns></returns>
        Result<LoginLogDto> AddLoginLog(LoginLogDto model);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ResultDto<LoginLogDto> GetWithPages(LogRequest request);
    } 
}
