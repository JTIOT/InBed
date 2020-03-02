using InBed.Service.Request;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using InBed.Service.Dto;

namespace InBed.Service.Abstracts
{ 
	/// <summary>
    /// Menu业务契约
    /// </summary>
    public partial interface IMenuService
    {
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Result<MenuDto> AddMenu(MenuDto model);
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Result<MenuDto> EditMenu(MenuDto model);
        /// <summary>
        /// 删除单个菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<MenuDto> DelMenu(int id);
        /// <summary>
        /// 删除多个菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<MenuDto> DelMenu(List<int> id);
        /// <summary>
        /// 查询菜单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ResultDto<MenuDto> GetWithPages(MenuRequest request);
        /// <summary>
        /// 根据id获取单条菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MenuDto GetDtail(int id);
        List<MenuDto> Query(int type);
    } 
}
