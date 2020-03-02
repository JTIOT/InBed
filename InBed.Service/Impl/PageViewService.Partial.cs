
/*******************************************************************************
* Copyright (C)  InBed.Com
* 
* Author: dj.wong
* Create Date: 04/20/2016 18:42:18
* Description: Automated building by service@InBed.com 
* 
* Revision History:
* Date         Author               Description
*
*********************************************************************************/

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
    /// PageView业务契约
    /// </summary>
    public partial class PageViewService :  IDependency, IPageViewService
    {
        public Result<PageViewDto> Add(PageViewDto pageview)
        {
            Result<PageViewDto> res = new Result<PageViewDto>();
            try
            {
                var entity = Mapper.Map<PageViewDto, PageViewEntity>(pageview);
                
                var flag = new PageViewDAL().AddPageView(entity);
                if (flag)
                {
                    res.flag = true;
                }
                else
                {
                    res.flag = false;
                    res.msg = "页面访问添加失败！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public ResultDto<PageViewDto> GetWithPages(PageViewRequest request)
        {
            ResultDto<PageViewDto> R_model = new ResultDto<PageViewDto>();
            try
            {
                int count = 0;
                var entity = new PageViewDAL().GetWithPages(request.Start, request.Length, request.LoginName, request.IP,
                                                                        request.StartTime, request.EndTime, request.OrderBy, out count, request.OrderDir);
                R_model.recordsTotal = count;
                R_model.data = Mapper.Map<List<PageViewEntity>, List<PageViewDto>>(entity);
            }
            catch (Exception ex)
            {
                R_model.recordsTotal = 0;
                R_model.data = new List<PageViewDto>();
            }
            return R_model; 
        }
    }
}
