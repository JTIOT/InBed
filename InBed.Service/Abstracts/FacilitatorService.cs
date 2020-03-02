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
    public partial class FacilitatorService : ServiceBase<FacilitatorEntity>, IDependency, IFacilitatorService
    {

        #region 构造函数注册上下文
        public IDbContextScopeFactory _dbScopeFactory { get; set; }
        #endregion

        #region IFacilitatorService 接口实现

        public bool Add(List<FacilitatorDto> models)
        {
            using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var entities = Mapper.Map<List<FacilitatorDto>, List<FacilitatorEntity>>(models);
                dbSet.AddRange(entities);
                return db.SaveChanges() > 0;
            }
        }

        public bool Add(FacilitatorDto dto)
        {
            using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var entity = new FacilitatorEntity()
                {
                    code = dto.Code,
                    name = dto.Name,
                    number = dto.Number,
                    contacts = dto.Contacts,
                    province = dto.Province,
                    city = dto.City,
                    district = dto.District,
                    address = dto.Address,
                    capital = dto.Capital,
                    management = dto.Management,
                    Creater = dto.Creater,
                    CreateDateTime = DateTime.Now,
                    IsDeleted = false,
                    up_id = dto.UP_id       
                };
                dbSet.Add(entity);
                var count = db.SaveChanges();
                return count > 0;
            }
        }

        public bool Delete(Expression<Func<FacilitatorDto, bool>> exp)
        {
            using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var where = exp.Cast<FacilitatorDto, FacilitatorEntity, bool>();

                var models = dbSet.Where(where);
                dbSet.RemoveRange(models);
                return db.SaveChanges() > 0;
            }
        }

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

        public FacilitatorDto GetOne(Expression<Func<FacilitatorDto, bool>> exp)
        {
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var where = exp.Cast<FacilitatorDto, FacilitatorEntity, bool>();
                var entity = dbSet.AsNoTracking().FirstOrDefault(where);
                return GetDto(entity);
            }
        }

        public ResultDto<FacilitatorDto> GetWithPages(QueryBase queryBase, Expression<Func<FacilitatorDto, bool>> exp, string orderBy, string orderDir = "desc")
        {
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var where = exp.Cast<FacilitatorDto, FacilitatorEntity, bool>();
                //var order = orderExp.Cast<MenuDto, MenuEntity, OrderKeyType>();
                var query = GetQuery(dbSet, where, orderBy, orderDir);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
                var list = query_list.ToList();
                var DtoList = new List<FacilitatorDto>();

                list.ForEach(p => DtoList.Add(GetDto(p)));
                var dto = new ResultDto<FacilitatorDto>
                {
                    recordsTotal = query_count.Value,
                    data = DtoList
                };
                return dto;
            }
        }

        public ResultDto<FacilitatorDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<FacilitatorDto, bool>> exp, Expression<Func<FacilitatorDto, OrderKeyType>> orderExp, bool isDesc = true)
        {
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var where = exp.Cast<FacilitatorDto, FacilitatorEntity, bool>();
                var order = orderExp.Cast<FacilitatorDto, FacilitatorEntity, OrderKeyType>();
                var query = GetQuery(dbSet, where, order, isDesc);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
                var list = query_list.ToList();

                var dto = new ResultDto<FacilitatorDto>
                {
                    recordsTotal = query_count.Value,
                    data = Mapper.Map<List<FacilitatorEntity>, List<FacilitatorDto>>(list)
                };
                return dto;
            }
        }

        public List<FacilitatorDto> Query<OrderKeyType>(Expression<Func<FacilitatorDto, bool>> exp, Expression<Func<FacilitatorDto, OrderKeyType>> orderExp, bool isDesc = true)
        {
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var where = exp.Cast<FacilitatorDto, FacilitatorEntity, bool>();
                var order = orderExp.Cast<FacilitatorDto, FacilitatorEntity, OrderKeyType>();
                var query = GetQuery(dbSet, where, order, isDesc);
                var list = query.ToList();
                return Mapper.Map<List<FacilitatorEntity>, List<FacilitatorDto>>(list);
            }
        }

        public bool Update(IEnumerable<FacilitatorDto> dto)
        {
            using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var entities = Mapper.Map<IEnumerable<FacilitatorDto>, IEnumerable<FacilitatorEntity>>(dto);
                dbSet.AddOrUpdate(entities.ToArray());
                return db.SaveChanges() > 0;
            }
        }

        public bool Update(FacilitatorDto dto)
        {
            using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var entity = new FacilitatorEntity()
                {
                    Id = dto.Id,
                    address = dto.Address,
                    capital = dto.Capital,
                    code = dto.Code,
                    city = dto.City,
                    contacts = dto.Contacts,
                    district = dto.District,
                    management = dto.Management,
                    name = dto.Name,
                    number = dto.Number,
                    province = dto.Province,
                    IsDeleted = dto.IsDeleted,
                    Modifier = dto.Modifier,
                    ModifyTime = DateTime.Now ,
                    up_id=dto.UP_id                 
                };
                dbSet.AddOrUpdate(entity);
                return db.SaveChanges() > 0;
            }
        }
        private FacilitatorDto GetDto(FacilitatorEntity entity)
        {
            return new FacilitatorDto()
            {
                Id = entity.Id,
                Address = entity.address,
                Capital = entity.capital,
                City = entity.city,
                Code = entity.code,
                Contacts = entity.contacts,
                District = entity.district,
                Management = entity.management,
                Name = entity.name,
                Number = entity.number,
                Province = entity.province,
                IsDeleted = entity.IsDeleted,
                CreateDateTime = entity.CreateDateTime,
                Creater = entity.Creater
            };
        }
        #endregion
    }
}
