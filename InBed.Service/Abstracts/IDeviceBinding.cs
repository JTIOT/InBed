using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using InBed.Service.Dto;
using InBed.Entity;
using InBed.Service.Request;

namespace InBed.Service.Abstracts
{
    public partial interface IDeviceBindingService
    {/// <summary>
     /// 查看设备与人员绑定信息
     /// </summary>
     /// <param name="request"></param>
     /// <returns></returns>
        ResultDto<BindingDto> GetWithPages(DeviceBindingRequest request);
        /// <summary>
        /// 给老人绑定设备
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Result<DeviceBindingDto> AddDeviceBinding(DeviceBindingDto model);
        /// <summary>
        /// 修改老人使用设备
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Result<DeviceBindingDto> EditDeviceBinding(DeviceBindingDto model);
        /// <summary>
        /// 删除老人与设备绑定
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<DeviceBindingDto> DelDeviceBinding(int id);
        /// <summary>
        /// 同时删除多个设备绑定
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Result<DeviceBindingDto> DelDeviceBinding(List<int> ids);
        /// <summary>
        /// 获取绑定详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<DeviceBindingDto> GetDeteail(int id);
    }
}
