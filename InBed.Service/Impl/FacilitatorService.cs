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
    public partial class FacilitatorService :  IDependency, IFacilitatorService
    {

        #region 构造函数注册上下文
        public IDbContextScopeFactory _dbScopeFactory { get; set; }
        #endregion

        public Result<FacilitatorDto> AddFacilitator(FacilitatorDto model)
        {
            Result<FacilitatorDto> res = new Result<FacilitatorDto>();
            try
            {
                var entity = new FacilitatorEntity
                {
                    address = model.Address,
                    capital = model.Capital,
                    city = model.City,
                    code = model.Code,
                    contacts = model.Contacts,
                    CreateDateTime = model.CreateDateTime,
                    Creater = model.Creater,
                    district = model.District,
                    F_Level = (int)model.F_Level,
                    IsDeleted = model.IsDeleted,
                    management = model.Management,
                    name = model.Name,
                    number = model.Number,
                    province = model.Province,
                    up_id = model.UP_id
                };




                if (StringExtension.IsBlank(entity.name))
                {
                    return new Result<FacilitatorDto>() { flag = false, msg = "服务商不能为空，请输入！" };
                }
                if (StringExtension.IsBlank(entity.number))
                {
                    return new Result<FacilitatorDto>() { flag = false, msg = "服务商工商注册编号不能为空，请输入！" };
                }
                if (StringExtension.IsValidPhone(entity.address))
                {
                    return new Result<FacilitatorDto>() { flag = false, msg = "服务商地址不能为空，请输入！" };
                }
                var flag = new FacilitatorDAL().AddFacilitator(entity);
                if (flag)
                {
                    string pwd = "123456";
                    var userEntity = new UserEntity
                    {
                        LoginName = model.Code,
                        Password = pwd.ToMD5(),
                        FacilitatorCode = model.Code,
                        FacilitatorName = model.Name,
                        IsDeleted = false,
                        Status = (int)UserStatus.已激活,
                        Creater = model.Creater,
                        CreateDateTime = DateTime.Now,
                        RealName = "系统初始账户",
                        Email = "123@qq.com" 
                    };
                    var userflag = new UserDAL().AddUser(userEntity);
                    res.flag = userflag;
                }
                else
                {
                    res.flag = false;
                    res.msg = "服务商信息添加失败！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public Result<FacilitatorDto> DelFacilitator(List<int> ids)
        {
            Result<FacilitatorDto> res = new Result<FacilitatorDto>();
            try
            {
                FacilitatorDAL obj = new FacilitatorDAL();
                string msg = "";
                foreach (int id in ids)
                {
                    var entity = obj.IsDelFacilitator(id);
                    if (entity.Count<1)
                    {
                        if (!obj.DelFacilitator(id))
                        {
                            msg +="服务商删除失败！";
                        }
                    }
                    else
                    {
                        msg += "服务商下还有服务老人，不能删除";
                    }
                }
                if (StringExtension.IsBlank(msg))
                {
                    res.flag = true;
                    res.msg = "服务商删除成功！";
                }
                else
                {
                    res.flag = false;
                    res.msg = msg;
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public Result<FacilitatorDto> DelFacilitator(int id)
        {
            Result<FacilitatorDto> res = new Result<FacilitatorDto>();
            try
            {
                FacilitatorDAL obj = new FacilitatorDAL();
                var entity = obj.IsDelFacilitator(id);
                if (entity == null)
                {
                    if (obj.DelFacilitator(id))
                    {
                        res.flag = true;
                        res.msg = "服务商删除成功！";
                    }
                    else
                    {
                        res.flag = false;
                        res.msg = "服务商删除失败！";
                    }
                }
                else
                {
                    res.flag = false;
                    res.msg = "服务商下还有服务老人，不能删除";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public Result<FacilitatorDto> EditFacilitator(FacilitatorDto model)
        {
            Result<FacilitatorDto> res = new Result<FacilitatorDto>();
            try
            {
                FacilitatorDAL obj = new FacilitatorDAL();
                var entity = obj.Details(model.Id);
                entity.name = model.Name;
                entity.number = model.Number;
                entity.address = model.Address;
                entity.Modifier = model.Modifier;
                var flag = obj.EditFacilitator(entity);
                if (flag)
                {
                    res.flag = true;
                    res.msg = "服务商信息修改成功！";
                }
                else
                {
                    res.flag = false;
                    res.msg = "服务商信息修改失败！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public FacilitatorDto GetDeteail(int id)
        {
            try
            {
                var entity = new FacilitatorDAL().Details(id);
                if (entity != null)
                {
                    return new FacilitatorDto
                    {
                        Id = entity.Id,
                        Address = entity.address,
                        Capital = entity.capital,
                        City = entity.city,
                        Code = entity.code,
                        Contacts = entity.contacts,
                        CreateDateTime = entity.CreateDateTime,
                        Creater = entity.Creater,
                        District = entity.district,
                        IsDeleted = entity.IsDeleted,
                        Management = entity.management,
                        Modifier = entity.Modifier,
                        Name = entity.name,
                        Number = entity.number,
                        Province = entity.province,
                        UP_id = entity.up_id
                    };

                }
                else {
                    return new FacilitatorDto();
                }  
            }
            catch (Exception ex)
            {
                return new FacilitatorDto();
            }
        }
        public FacilitatorDto Query(string facilitator)
        {
            try {
                var entity = new FacilitatorDAL().Query(facilitator);
                if (entity != null)
                {
                    return new FacilitatorDto
                    {
                        Id = entity.Id,
                        Address = entity.address,
                        Capital = entity.capital,
                        City = entity.city,
                        Code = entity.code,
                        Contacts = entity.contacts,
                        CreateDateTime = entity.CreateDateTime,
                        Creater = entity.Creater,
                        District = entity.district,
                        IsDeleted = entity.IsDeleted,
                        Management = entity.management,
                        Modifier = entity.Modifier,
                        Name = entity.name,
                        Number = entity.number,
                        Province = entity.province,
                        UP_id = entity.up_id
                    };

                }
                else {
                    return new FacilitatorDto();
                }
            }
            catch (Exception ex)
            {
                return new FacilitatorDto();
            }
        }
        public List<FacilitatorDto> toDto(List<FacilitatorEntity> entity)
        {
            var res = new List<FacilitatorDto>();
            entity.ForEach(item => res.Add(new FacilitatorDto
            {
                Id = item.Id,
                Address = item.address,
                Capital = item.capital,
                City = item.city,
                Code = item.code,
                Contacts = item.contacts,
                CreateDateTime = item.CreateDateTime,
                Creater = item.Creater,
                District = item.district,
                IsDeleted = item.IsDeleted,
                Management = item.management,
                Modifier = item.Modifier,
                Name = item.name,
                Number = item.number,
                Province = item.province,
                UP_id = item.up_id
            }));
            return res;
        }

        public ResultDto<FacilitatorDto> GetWithPages(FacilitatorRequest request)
        {
            ResultDto<FacilitatorDto> res = new ResultDto<FacilitatorDto>();
            try
            {
                FacilitatorDAL obj = new FacilitatorDAL();
                var ent = obj.Query(request.FacilitatorCode);
                if (ent != null)
                {
                    int count = 0;
                    var entity = obj.GetWithPages(request.Start, request.Length, ent.Id, request.Name, request.Number, request.Adders,
                                                                            request.StartTime, request.EndTime, request.OrderBy, out count, request.OrderDir);
                    res.recordsTotal = count;
                    res.data = toDto(entity);
                }

               
            }
            catch (Exception ex)
            {
                res.recordsTotal = 0;
                res.data = new List<FacilitatorDto>();
            }
            return res;
        }

        public List<FacilitatorDto> GetlowerFacilitator(string  code)
        {
            List<FacilitatorDto> res = new List<FacilitatorDto>();
            try {
                var facilitatorEnttiy= new FacilitatorDAL().Query(code);
                if (facilitatorEnttiy != null)
                {
                    var enttiy = new FacilitatorDAL().GetlowerFacilitator(facilitatorEnttiy.Id);
                    if (enttiy != null)
                    {
                        enttiy.ForEach(P => res.Add(new FacilitatorDto
                        {
                            Id = P.Id,
                            Code = P.code,
                            Name = P.name
                        }));
                    }
                }
              
            }
            catch (Exception ex)
            {
                return new List<FacilitatorDto>();
            }
            return res;

        }
    }
}
