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
using InBed.Service.Request;
using InBed.Data.DAL;

namespace InBed.Service.Abstracts
{
    public partial class DeviceService : IDependency,IDeviceService
    {
        #region 构造函数注册上下文
        public IDbContextScopeFactory _dbScopeFactory { get; set; }
        #endregion

        public Result<DeviceDto> AddDevice(DeviceDto model)
        {
            Result<DeviceDto> res = new Result<DeviceDto>();
            try
            {
                var entity = new DeviceEntity
                {
                    CreateDateTime = DateTime.Now,
                    Creater = model.Creater,
                    device_model = model.device_model,
                    device_number = model.device_number,
                    device_type = (int)model.device_type,
                    Facilitator_Code = model.FacilitatorCode,
                    Facilitator_Name = model.FacilitatorName,
                    is_enable = model.is_enable,
                    status = (int)model.status,
                    IsDeleted = false
                };
                if (entity.device_type <= 0)
                {
                    return new Result<DeviceDto>() { flag = false, msg = "必须选择设备类型" };
                }
                if (StringExtension.IsBlank(entity.device_model))
                {
                    return new Result<DeviceDto>() { flag = false, msg = "设备型号不能为空，请输入！" };
                }
                if (entity.device_type <= 0)
                {
                    return new Result<DeviceDto>() { flag = false, msg = "请选择设备类型！" };
                }
                var flag = new DeviceDAL().AddDevice(entity);
                if (flag)
                {
                    res.flag = true;
                }
                else
                {
                    res.flag = false;
                    res.msg = "设备添加失败！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public Result<DeviceDto> Metastasis(List<BindingDto> listData, string leftF_code)
        {
            Result<DeviceDto> res = new Result<DeviceDto>();
            try {
                ElderDAL elderObj = new ElderDAL();
                DeviceDAL devObj = new DeviceDAL();
                DeviceAllotDAL devAllot = new DeviceAllotDAL();

                var elder_falg = false;
                var devAllot_falg = false;
                var dev_falg = false;
                var f_Entity = new FacilitatorDAL().Query(leftF_code);
                if (f_Entity == null)
                {
                    res.flag = false;
                    res.msg = "接受服务商不存在！";
                    return res;
                }
                foreach (var item in listData)
                {
                    //1、更新老人信息(item.FacilitatorName为用户现在所在的服务商)
                    var elderEntity = elderObj.Query(item.FacilitatorCode, item.Name).FirstOrDefault();
                    if (elderEntity != null)
                    {
                        elderEntity.FacilitatorCode = f_Entity.code;
                        elderEntity.FacilitatorName = f_Entity.name;
                        elder_falg = elderObj.UpdateElderFacilitator(elderEntity);

                    }
                    //2、修改设备信息
                    var devEntity = devObj.Query(item.number);
                    if (devEntity != null)
                    {
                        devEntity.Facilitator_Code = f_Entity.code;
                        devEntity.Facilitator_Name = f_Entity.name;
                        dev_falg = devObj.EditDevice(devEntity);
                    }
                    //3、添加设备分配日志
                    var devAllt = new DeviceAllotEntity
                    {
                        deviceID = devEntity.Id,
                        up_facilitator = item.FacilitatorCode,
                        facilitator = f_Entity.code,
                        Creater = item.Creater,
                        CreateDateTime = DateTime.Now
                    };
                    devAllot_falg = devAllot.AddDeviceAllot(devAllt);
                    if (devAllot_falg && dev_falg && elder_falg)
                    {
                        res.flag = true;
                        res.msg = "成功！";
                    }
                    else
                    {
                        res.flag = false;
                        res.msg = "失败！";
                    }
                   



                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = "失败！";

            }
            return res;
        }

        public Result<DeviceDto> DelDevice(List<int> ids)
        {
            Result<DeviceDto> res = new Result<DeviceDto>();
            try
            {
                if (new DeviceDAL().DelDevice(ids))
                {
                    res.flag = true;
                    res.msg = "设备删除成功！";
                }
                else
                {
                    res.flag = false;
                    res.msg = "设备删除失败！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public Result<DeviceDto> DelDevice(int id)
        {
            Result<DeviceDto> res = new Result<DeviceDto>();
            try
            {
                if (new DeviceDAL().DelDevice(id))
                {
                    res.flag = true;
                    res.msg = "设备删除成功！";
                }
                else
                {
                    res.flag = false;
                    res.msg = "设备删除失败！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public Result<DeviceDto> EditDevice(DeviceDto model)
        {
            Result<DeviceDto> res = new Result<DeviceDto>();
            try
            {
                //var entity = Mapper.Map<DeviceDto, DeviceEntity>(model);
                var entity = new DeviceEntity
                {
                   
                    device_model = model.device_model,
                    device_number = model.device_number,
                    device_type = (int)model.device_type,
                    Facilitator_Code = model.FacilitatorCode,
                    Facilitator_Name = model.FacilitatorName,
                    is_enable = model.is_enable,
                    status = (int)model.status,
                    IsDeleted = false,
                     Modifier=model.Modifier,
                      ModifyTime = DateTime.Now,
                       Id=model.Id
                };
                var flag = new DeviceDAL().EditDevice(entity);
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

        public DeviceDto GetDeteail(int id)
        {
            try
            {
                var deviceDb = new DeviceDAL().Details(id);
                if (deviceDb != null)
                {

                    return new DeviceDto
                    {
                        CreateDateTime = deviceDb.CreateDateTime,
                        Creater = deviceDb.Creater,
                        device_model = deviceDb.device_model,
                        device_number = deviceDb.device_number,
                        device_type = (DeviceType)deviceDb.device_type,
                        FacilitatorCode = deviceDb.Facilitator_Code,
                        FacilitatorName = deviceDb.Facilitator_Name,
                        Id = deviceDb.Id,
                        IsDeleted = deviceDb.IsDeleted,
                        is_enable = deviceDb.is_enable,
                        Modifier = deviceDb.Modifier,
                        status = (DeviceStatus)deviceDb.status

                    };
                }
                else
                {
                    return new DeviceDto();
                }


            }
            catch (Exception ex)
            {
                return new DeviceDto();
            }
        }

        public ResultDto<DeviceDto> GetWithPages(DeviceRequest request)
        {
            ResultDto<DeviceDto> res = new ResultDto<DeviceDto>();
            try
            {
                int count = 0;
                var entity = new DeviceDAL().GetWithPages(request.Start, request.Length, request.FacilitatorCode, request.Number, request.model
                    , request.DeviceType, request.DeviceState,
                                                                        request.StartTime, request.EndTime, request.OrderBy, out count, request.OrderDir);
                res.recordsTotal = count;
                res.data = toDto(entity);
            }
            catch (Exception ex)
            {
                res.recordsTotal = 0;
                res.data = new List<DeviceDto>();
            }
            return res;
        }

        public Result<DeviceDto> RightMetastasis(List<BindingDto> listData, string rightF_code)
        {
            throw new NotImplementedException();
        }

        public List<DeviceDto> toDto(List<DeviceEntity> entity)
        {
            List<DeviceDto> res = new List<DeviceDto>();
            entity.ForEach(item => res.Add(new DeviceDto
            {
                CreateDateTime = item.CreateDateTime,
                Creater = item.Creater,
                device_model = item.device_model,
                device_number = item.device_number,
                device_type = (DeviceType)item.device_type,
                FacilitatorCode = item.Facilitator_Code,
                FacilitatorName = item.Facilitator_Name,
                Id = item.Id,
                IsDeleted = item.IsDeleted,
                is_enable = item.is_enable,
                Modifier = item.Modifier,
                status = (DeviceStatus)item.status

            }));
            return res;

        }
    }
}
