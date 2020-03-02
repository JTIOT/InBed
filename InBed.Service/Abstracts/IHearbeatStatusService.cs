using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using InBed.Service.Dto;
using InBed.Service.Request;
namespace InBed.Service.Abstracts
{
    public interface IHearbeatStatusService
    {
        /// <summary>
        /// 查询符合调价的 emailpool
        /// </summary>
        /// <param name="exp">过滤条件</param>
        /// <param name="orderExp">排序条件</param>
        /// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        List<HearbeatStatusDto> Query<OrderKeyType>(Expression<Func<HearbeatStatusDto, bool>> exp, Expression<Func<HearbeatStatusDto, OrderKeyType>> orderExp, bool isDesc = true);
    }
}
