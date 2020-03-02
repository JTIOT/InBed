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
using InBed.Service.Enum;
using InBed.Data;
using InBed.Data.DAL;
using InBed.Service.Request;

namespace InBed.Service.Abstracts
{
   public  class DeviceBindingService : IDependency, IDeviceBindingService
    {
        #region 构造函数注册上下文
        public IDbContextScopeFactory _dbScopeFactory { get; set; }

        #endregion
        public Result<DeviceBindingDto> AddDeviceBinding(DeviceBindingDto model)
        {
            Result<DeviceBindingDto> res = new Result<DeviceBindingDto>();
            try
            {
                var entity = Mapper.Map<DeviceBindingDto, DeviceBindingEntity>(model);
                if (entity.DeviceId <= 0)
                {
                    return new Result<DeviceBindingDto>() { flag = false, msg = "必须选择要绑定的设备id" };
                }
                if (entity.ElderId <= 0)
                {
                    return new Result<DeviceBindingDto>() { flag = false, msg = "必须选择要与设备绑定的老人id" };
                }
                var flag = new DeviceBindingDAL().AddDeviceBinding(entity);
                if (flag)
                {
                    res.flag = true;
                }
                else
                {
                    res.flag = false;
                    res.msg = "设备与人员绑定失败！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public Result<DeviceBindingDto> DelDeviceBinding(List<int> ids)
        {
            Result<DeviceBindingDto> res = new Result<DeviceBindingDto>();
            try
            {
                if (new DeviceBindingDAL().DelBinding(ids))
                {
                    res.flag = true;
                    res.msg = "设备解除绑定成功！";
                }
                else
                {
                    res.flag = false;
                    res.msg = "设备解除绑定失败！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public Result<DeviceBindingDto> DelDeviceBinding(int id)
        {
            Result<DeviceBindingDto> res = new Result<DeviceBindingDto>();
            try
            {
                if (new DeviceBindingDAL().DelBinding(id))
                {
                    res.flag = true;
                    res.msg = "设备解除绑定成功！";
                }
                else
                {
                    res.flag = false;
                    res.msg = "设备解除绑定失败！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public Result<DeviceBindingDto> EditDeviceBinding(DeviceBindingDto model)
        {
            Result<DeviceBindingDto> res = new Result<DeviceBindingDto>();
            try
            {
                var entity = Mapper.Map<DeviceBindingDto, DeviceBindingEntity>(model);
                var flag = new DeviceBindingDAL().EditBinding(entity);
                if (flag)
                {
                    res.flag = true;
                    res.msg = "设备绑定修改成功！";
                }
                else
                {
                    res.flag = false;
                    res.msg = "设备绑定修改失败！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public Result<DeviceBindingDto> GetDeteail(int id)
        {
            throw new NotImplementedException();
        }

        public ResultDto<BindingDto> GetWithPages(DeviceBindingRequest request)
        {
            ResultDto<BindingDto> res = new ResultDto<BindingDto>();
            try
            {
                int count = 0;
                var entity = new DeviceBindingDAL().GetWithPages(request.Start, request.Length, request.FacilitatorCode, request.ElderName, request.ElderPhone, request.DeviceNumber, request.DeviceType,
                                                                        request.OrderBy, request.OrderDir);
                res.recordsTotal = entity.Count;
                entity.Skip(request.Start * request.Length).Take(request.Length);
                res.data = toDto(entity.Skip(request.Start).Take(request.Length).ToList());
            }
            catch (Exception ex)
            {
                res.recordsTotal = 0;
                res.data = new List<BindingDto>();
            }
            return res;
        }
        public List<BindingDto> toDto(List<BindingEntity> entity)
        {
            List<BindingDto> res = new List<BindingDto>();
            entity.ForEach(item => res.Add(new BindingDto {
                Name = item.Name,
                birthday = item.birthday.ToString(),
                homephone = item.homephone,
                model = item.model,
                number = item.number,
                sex = (SexEnum)item.sex,
                status = (DeviceStatus)item.status,
                type = (DeviceType)item.type,
                Id = item.id
            }));
            return res;
        }


    }
}
