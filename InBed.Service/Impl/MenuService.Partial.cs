
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
    /// Menu业务契约
    /// </summary>
    public partial class MenuService : IDependency, IMenuService
    {
		#region 构造函数注册上下文
		public IDbContextScopeFactory _dbScopeFactory {get;set;}

        //private readonly IDbContextScopeFactory _dbScopeFactory;

        //public MenuService(IDbContextScopeFactory dbScopeFactory)
        //{
        //    _dbScopeFactory = dbScopeFactory;
        //}

        #endregion

        public Result<MenuDto> AddMenu(MenuDto model)
        {
            Result<MenuDto> res = new Result<MenuDto>();
            try
            {
                var entity = Mapper.Map<MenuDto, MenuEntity>(model);
                if (new MenuDAL().AddMenu(entity))
                {
                    res.flag = true;
                    res.msg = "菜单添加成功！";
                }
                else
                {
                    res.flag = false;
                    res.msg = "菜单添加失败！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = "菜单添加失败！";
            }
            return res;
        }

        public Result<MenuDto> DelMenu(List<int> id)
        {
            Result<MenuDto> res = new Result<MenuDto>();
            try
            {
                if (new MenuDAL().DelMenu(id))
                {
                    res.flag = true;
                    res.msg = "系统菜单删除成功！";
                }
                else
                {
                    res.flag = false;
                    res.msg = "系统菜单删除失败！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public Result<MenuDto> DelMenu(int id)
        {
            Result<MenuDto> res = new Result<MenuDto>();
            try
            {
                if (new MenuDAL().DelMenu(id))
                {
                    res.flag = true;
                    res.msg = "系统菜单删除成功！";
                }
                else
                {
                    res.flag = false;
                    res.msg = "系统菜单删除失败！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public Result<MenuDto> EditMenu(MenuDto model)
        {
            Result<MenuDto> res = new Result<MenuDto>();
            try
            {
                var entity = Mapper.Map<MenuDto, MenuEntity>(model);
                var flag = new MenuDAL().EditMenu(entity);
                if (flag)
                {
                    res.flag = true;
                    res.msg = "系统菜单编辑成功！";
                }
                else
                {
                    res.flag = false;
                    res.msg = "系统菜单编辑失败！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public MenuDto GetDtail(int id)
        {
            try
            {
                var menuDb = new MenuDAL().Details(id);
                return Mapper.Map<MenuEntity, MenuDto>(menuDb);
            }
            catch (Exception ex)
            {
                return new MenuDto();
            }
        }

        public ResultDto<MenuDto> GetWithPages(MenuRequest request)
        {
            ResultDto<MenuDto> res = new ResultDto<MenuDto>();
            try
            {
                int count = 0;
                var entity = new MenuDAL().GetWithPages(request.Start, request.Length, request.P_id, (int)request.Type, request.Name,
                                                                        request.StartTime, request.EndTime, request.OrderBy, out count, request.OrderDir);
                res.recordsTotal = count;
                res.data = Mapper.Map<List<MenuEntity>, List<MenuDto>>(entity);
            }
            catch (Exception ex)
            {
                res.recordsTotal = 0;
                res.data = new List<MenuDto>();
            }
            return res;
        }
        public List<MenuDto> Query(int type)
        {
            try
            {
                var entity = new MenuDAL().Query(type);
                return Mapper.Map<List<MenuEntity>, List<MenuDto>>(entity);
            }
            catch (Exception ex)
            {
                return new List<MenuDto>();
            }

        }
    } 
}
