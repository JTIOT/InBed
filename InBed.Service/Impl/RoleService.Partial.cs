
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
using InBed.Service.Request;

namespace InBed.Service.Abstracts
{ 
	/// <summary>
    /// Role业务契约
    /// </summary>
    public partial class RoleService :  IDependency, IRoleService
    {
		#region 构造函数注册上下文
		public IDbContextScopeFactory _dbScopeFactory {get;set;}

        //private readonly IDbContextScopeFactory _dbScopeFactory;

        //public RoleService(IDbContextScopeFactory dbScopeFactory)
        //{
        //    _dbScopeFactory = dbScopeFactory;
        //}

        #endregion

        public Result<RoleDto> AddRole(RoleDto model)
        {
            Result<RoleDto> R_model = new Result<RoleDto>();
            try
            {
                var entity = Mapper.Map<RoleDto, RoleEntity>(model);
                if (StringExtension.IsBlank(entity.Name))
                {
                    return new Result<RoleDto>() { flag = false, msg = "角色不能为空！" };
                }
                var flag = new RoleDAL().AddRole(entity);
                if (flag)
                {
                    R_model.flag = true;
                }
                else
                {
                    R_model.flag = false;
                    R_model.msg = "添加角色失败";
                }

            }
            catch (Exception ex)
            {
                R_model.flag = false;
                R_model.msg = ex.Message;
            }
            return R_model;
        }

        public Result<RoleDto> EditRole(RoleDto model)
        {
            Result<RoleDto> res = new Result<RoleDto>();
            try
            {
                var entity = Mapper.Map<RoleDto, RoleEntity>(model);
                var flag = new RoleDAL().EditRole(entity);
                if (flag)
                {
                    res.flag = true;
                    res.msg = "系统用户编辑成功！";
                }
                else
                {
                    res.flag = false;
                    res.msg = "系统用户编辑失败！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public Result<RoleDto> DelRole(List<int> ids)
        {
            Result<RoleDto> R_model = new Result<RoleDto>();
            try
            {
                if (new RoleDAL().DelRole(ids))
                {
                    R_model.flag = true;
                    R_model.msg = "系统角色删除成功！";
                }
                else
                {
                    R_model.flag = false;
                    R_model.msg = "系统角色删除失败！";
                }
            }
            catch (Exception ex)
            {
                R_model.flag = false;
                R_model.msg = ex.Message;
            }
            return R_model;
        }

        public Result<RoleDto> DelRole(int id)
        {
            Result<RoleDto> R_model = new Result<RoleDto>();
            try
            {
                if (new RoleDAL().DelRole(id))
                {
                    R_model.flag = true;
                    R_model.msg = "系统角色删除成功！";
                }
                else
                {
                    R_model.flag = false;
                    R_model.msg = "系统角色删除失败！";
                }
            }
            catch (Exception ex)
            {
                R_model.flag = false;
                R_model.msg = ex.Message;
            }
            return R_model;
        }

        public RoleDto Detail(int id)
        {
            try
            {
                var userDb = new RoleDAL().Details(id);
                return Mapper.Map<RoleEntity, RoleDto>(userDb);
            }
            catch (Exception ex)
            {
                return new RoleDto();
            }
        }

        public ResultDto<RoleDto> GetWithPages(RoleRequest request)
        {
            ResultDto<RoleDto> res = new ResultDto<RoleDto>();
            try
            {
                int count = 0;
                var entity = new RoleDAL().GetWithPages(request.Start, request.Length, request.Name,
                                                                        request.FacilitatorCode, request.Description, request.OrderBy, out count, request.OrderDir);
                res.recordsTotal = count;
                res.data = Mapper.Map<List<RoleEntity>, List<RoleDto>>(entity);
            }
            catch (Exception ex)
            {
                res.recordsTotal = 0;
                res.data = new List<RoleDto>();
            }
            return res;
        }

        public ResultDto<RoleDto> Query(List<int> ids, string f_code)
        {
            ResultDto<RoleDto> res = new ResultDto<RoleDto>();
            try
            {
                List<RoleDto> data = new List<RoleDto>();
                var entity = new RoleDAL().Query(ids, f_code);
                if (entity != null || !entity.Any())
                {
                    entity.ForEach(p => data.Add(new RoleDto
                    {

                        Id = p.Id,
                        CreateDateTime = p.CreateDateTime,
                        Description = p.Description,
                        FacilitatorCode = p.FacilitatorCode,
                        FacilitatorName = p.FacilitatorName,
                        Name = p.Name,
                        Creater = p.Creater,
                        Modifier = p.Modifier

                    }));
                }
                res.recordsTotal = entity.Count;
                res.data = data;
                return res;

            }
            catch (Exception ex)
            {
                return new ResultDto<RoleDto>();
            }



        }
    } 
}
