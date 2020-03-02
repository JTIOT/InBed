using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using InBed.Service.Dto;
using InBed.Service.Request;

namespace InBed.Service.Abstracts
{
    public partial interface IDeviceService
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ResultDto<DeviceDto> GetWithPages(DeviceRequest request);
        /// <summary>
        /// 添加设备
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Result<DeviceDto> AddDevice(DeviceDto model);
        /// <summary>
        /// 修改设备
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Result<DeviceDto> EditDevice(DeviceDto model);
        /// <summary>
        /// 删除单个设备
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<DeviceDto> DelDevice(int id);
        /// <summary>
        /// 删除多个设备
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<DeviceDto> DelDevice(List<int> id);
        /// <summary>
        /// 获取设备详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DeviceDto GetDeteail(int id);
        /// <summary>
        /// 向右转移
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="rightF_code"></param>
        /// <returns></returns>
        Result<DeviceDto> RightMetastasis(List<BindingDto> listData, string rightF_code);
        /// <summary>
        /// 向左转移
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="rightF_code"></param>
        /// <returns></returns>
        Result<DeviceDto> Metastasis(List<BindingDto> listData, string leftF_code);



    }
}
