using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using InBed.Service.Dto;
using InBed.Service.Request;

namespace InBed.Service.Abstracts
{
    public partial interface IFacilitatorService
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ResultDto<FacilitatorDto> GetWithPages(FacilitatorRequest request);
        /// <summary>
        /// 添加服务商
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Result<FacilitatorDto> AddFacilitator(FacilitatorDto model);
        /// <summary>
        /// 修改服务商
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Result<FacilitatorDto> EditFacilitator(FacilitatorDto model);
        /// <summary>
        /// 删除单个服务商
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<FacilitatorDto> DelFacilitator(int id);
        /// <summary>
        /// 删除多个服务商
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<FacilitatorDto> DelFacilitator(List<int> id);
        /// <summary>
        /// 获取服务商详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FacilitatorDto GetDeteail(int id);
        /// <summary>
        /// 获取上级服务商
        /// </summary>
        /// <param name="facilitator"></param>
        /// <returns></returns>
        FacilitatorDto Query(string facilitator);
        /// <summary>
        /// 获取下级服务商名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<FacilitatorDto> GetlowerFacilitator(string  code);
    }
}
