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

namespace InBed.Service.Abstracts
{
    public partial class DeviceService : ServiceBase<DeviceEntity>, IDependency, IDeviceService
    {
        #region 构造函数注册上下文
        public IDbContextScopeFactory _dbScopeFactory { get; set; }
        #endregion

        #region IMenuService 接口实现
        /// <summary>
        /// 批量添加设备
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool Add(List<DeviceDto> models)
        {
            using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var entities = Mapper.Map<List<DeviceDto>, List<DeviceEntity>>(models);
                dbSet.AddRange(entities);
                return db.SaveChanges() > 0;
            }
        }
        /// <summary>
        /// 添加设备
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public bool Add(DeviceDto device)
        {
            using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var entity = new DeviceEntity()
                {
                    CreateDateTime = device.CreateDateTime,
                    Creater = device.Creater,
                    device_model = device.device_model,
                    device_number = device.device_number,
                    device_type = (int)device.device_type,
                    Facilitator_Code = device.Facilitator_Code,
                    Facilitator_Name = device.Facilitator_Name,
                    IsDeleted = false,
                    is_enable = device.is_enable,
                    status = (int)device.status
                };
                dbSet.Add(entity);
                var count = db.SaveChanges();
                return count > 0;
            }
        }
        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public bool Delete(Expression<Func<DeviceDto, bool>> exp)
        {
            using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var where = exp.Cast<DeviceDto, DeviceEntity, bool>();

                var models = dbSet.Where(where);
                dbSet.RemoveRange(models);
                return db.SaveChanges() > 0;
            }
        }
        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);

                var model = dbSet.FirstOrDefault(item => item.Id == id);
                dbSet.Remove(model);
                return db.SaveChanges() > 0;
            }
        }

        public DeviceDto GetOne(Expression<Func<DeviceDto, bool>> exp)
        {
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var where = exp.Cast<DeviceDto, DeviceEntity, bool>();
                var entity = dbSet.AsNoTracking().FirstOrDefault(where);
                return GetDto(entity);
            }
        }

        public ResultDto<DeviceDto> GetWithPages(QueryBase queryBase, Expression<Func<DeviceDto, bool>> exp, string orderBy, string orderDir = "desc")
        {
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var where = exp.Cast<DeviceDto, DeviceEntity, bool>();
                //var order = orderExp.Cast<MenuDto, MenuEntity, OrderKeyType>();
                var query = GetQuery(dbSet, where, orderBy, orderDir);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
                var list = query_list.ToList();
                var DtoList = new List<DeviceDto>();  
                 
                list.ForEach(p => DtoList.Add(GetDto(p)));
                var dto = new ResultDto<DeviceDto>
                {
                    recordsTotal = query_count.Value,
                   data= DtoList
                };
                return dto;
            }
        }

        public ResultDto<DeviceDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<DeviceDto, bool>> exp, Expression<Func<DeviceDto, OrderKeyType>> orderExp, bool isDesc = true)
        {
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var where = exp.Cast<DeviceDto, DeviceEntity, bool>();
                var order = orderExp.Cast<DeviceDto, DeviceEntity, OrderKeyType>();
                var query = GetQuery(dbSet, where, order, isDesc);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
                var list = query_list.ToList();

                var dto = new ResultDto<DeviceDto>
                {
                    recordsTotal = query_count.Value,
                    data = Mapper.Map<List<DeviceEntity>, List<DeviceDto>>(list)
                };
                return dto;
            }
        }

        public List<DeviceDto> Query<OrderKeyType>(Expression<Func<DeviceDto, bool>> exp, Expression<Func<DeviceDto, OrderKeyType>> orderExp, bool isDesc = true)
        {
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var where = exp.Cast<DeviceDto, DeviceEntity, bool>();
                var order = orderExp.Cast<DeviceDto, DeviceEntity, OrderKeyType>();
                var query = GetQuery(dbSet, where, order, isDesc);
                var list = query.ToList();
                return Mapper.Map<List<DeviceEntity>, List<DeviceDto>>(list);
            }
        }

        public bool Update(IEnumerable<DeviceDto> dtos)
        {
            using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var entities = Mapper.Map<IEnumerable<DeviceDto>, IEnumerable<DeviceEntity>>(dtos);
                dbSet.AddOrUpdate(entities.ToArray());
                return db.SaveChanges() > 0;
            }
        }

        public bool Update(DeviceDto dto)
        {
            using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var entity = new DeviceEntity()
                {
                    Modifier= dto.Modifier,
                    ModifyTime=DateTime.Now,
                    device_model = dto.device_model,
                    device_type = (int)dto.device_type,
                    IsDeleted = dto.IsDeleted,
                    is_enable = dto.is_enable,
                    status = (int)dto.status
                };
                dbSet.AddOrUpdate(entity);
                return db.SaveChanges() > 0;
            }
        }


        private DeviceDto GetDto(DeviceEntity entity)
        {
            return new DeviceDto()
            {
                Id = entity.Id,
                Facilitator_Code = entity.Facilitator_Code,
                Facilitator_Name = entity.Facilitator_Name,
                device_number = entity.device_number,
                device_type = (DeviceType)entity.device_type,
                device_model = entity.device_model,
                is_enable = entity.is_enable,
                status = (DeviceStatus)entity.status,
                IsDeleted = entity.IsDeleted,
                CreateDateTime = entity.CreateDateTime,
                Creater = entity.Creater
            };
        }
        #endregion
    }
}
