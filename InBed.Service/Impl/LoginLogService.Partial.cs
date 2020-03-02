
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
using InBed.Service.Request;
using InBed.Data.DAL;

namespace InBed.Service.Abstracts
{ 
	/// <summary>
    /// LoginLog业务契约
    /// </summary>
    public partial class LoginLogService : IDependency, ILoginLogService
    {
		#region 构造函数注册上下文
		public IDbContextScopeFactory _dbScopeFactory {get;set;}

        //private readonly IDbContextScopeFactory _dbScopeFactory;

        //public LoginLogService(IDbContextScopeFactory dbScopeFactory)
        //{
        //    _dbScopeFactory = dbScopeFactory;
        //}

        #endregion

        public Result<LoginLogDto> AddLoginLog(LoginLogDto model)
        {
            Result<LoginLogDto> res = new Result<LoginLogDto>();
            try
            {
                var entity = Mapper.Map<LoginLogDto, LoginLogEntity>(model);
                if (StringExtension.IsBlank(entity.LoginName))
                {
                    return new Result<LoginLogDto>() { flag = false, msg = "添加登录日志失败，用户名不能为空！" };
                }
                var flag = new LogDAL().AddLog(entity);
                if (flag)
                {
                    res.flag = true;
                }
                else
                {
                    res.flag = false;
                    res.msg = "添加用户登录日志失败！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public Result<LoginLogDto> DelLog(List<int> ids)
        {
            Result<LoginLogDto> R_model = new Result<LoginLogDto>();
            try
            {
                if (new LogDAL().DelLog(ids))
                {
                    R_model.flag = true;
                    R_model.msg = "系统用户登录日志删除成功！";
                }
                else
                {
                    R_model.flag = false;
                    R_model.msg = "系统用户登录日志删除失败！";
                }
            }
            catch (Exception ex)
            {
                R_model.flag = false;
                R_model.msg = ex.Message;
            }
            return R_model;
        }

        public Result<LoginLogDto> DelLog(int id)
        {
            Result<LoginLogDto> R_model = new Result<LoginLogDto>();
            try
            {
                if (new LogDAL().DelLog(id))
                {
                    R_model.flag = true;
                    R_model.msg = "系统用户登录日志删除成功！";
                }
                else
                {
                    R_model.flag = false;
                    R_model.msg = "系统用户登录日志删除失败！";
                }
            }
            catch (Exception ex)
            {
                R_model.flag = false;
                R_model.msg = ex.Message;
            }
            return R_model;
        }

        public ResultDto<LoginLogDto> GetWithPages(LogRequest request)
        {
            ResultDto<LoginLogDto> R_model = new ResultDto<LoginLogDto>();
            try
            {
                int count = 0;
                var entity = new LogDAL().GetWithPages(request.Start, request.Length, request.LoginName, request.IP,
                                                                        request.StartTime, request.EndTime, request.OrderBy, out count, request.OrderDir);
                R_model.recordsTotal = count;
                R_model.data = Mapper.Map<List<LoginLogEntity>, List<LoginLogDto>>(entity);
            }
            catch (Exception ex)
            {
                R_model.recordsTotal = 0;
                R_model.data = new List<LoginLogDto>();
            }
            return R_model;
        }
    } 
}
