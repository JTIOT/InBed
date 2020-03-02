using InBed.Service.Request;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using InBed.Service.Dto;

namespace InBed.Service.Abstracts
{ 
	/// <summary>
    /// PageView业务契约
    /// </summary>
    public partial interface IPageViewService
    {
        Result<PageViewDto> Add(PageViewDto pageview);
        /// <summary>
        /// 分页获取pageview
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<PageViewDto> GetWithPages(PageViewRequest request);
    } 
}
