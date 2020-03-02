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
    public class ElderService : ServiceBase<ElderEntity>, IDependency, IElderService
    {
        #region 构造函数注册上下文
        public IDbContextScopeFactory _dbScopeFactory { get; set; }
        #endregion
        public bool Add(List<ElderDto> models)
        {
            using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var entities = Mapper.Map<List<ElderDto>, List<ElderEntity>>(models);
                dbSet.AddRange(entities);
                return db.SaveChanges() > 0;
            }
        }

        public bool Add(ElderDto dto)
        {
            using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var entity = new ElderEntity()
                {
                    CreateDateTime = dto.CreateDateTime,
                    Creater = dto.Creater,
                    IsDeleted = false,
                    birthday = dto.birthday,
                    blood_type = dto.blood_type,
                    contacts = dto.contacts,
                    c_telephone = dto.c_telephone,
                    deformity_number = dto.deformity_number,
                    dispensedmode = dto.dispensedmode,
                    education = dto.education,
                    genetic_disease = dto.genetic_disease,
                    homephone = dto.homephone,
                    H_allergydrug = dto.H_allergydrug,
                    H_disease = dto.H_disease,
                    Id = dto.Id,
                    is_deformity = dto.is_deformity,
                    is_genetic = dto.is_genetic,
                    is_ops = dto.is_ops,
                    marriageStatus = dto.marriageStatus,
                    name = dto.name,
                    nation = dto.nation,
                    number = dto.number,
                    occupation = dto.occupation,
                    ops_data = dto.ops_data,
                    ops_disease = dto.ops_disease,
                    resident_type = dto.resident_type,
                    photo = dto.photo,
                    sex = dto.sex,
                    workunit = dto.workunit,
                    Facilitator_Code = dto.facilitatorCode,
                    Facilitator_Name = dto.facilitatorName               
                };
                dbSet.Add(entity);
                var count = db.SaveChanges();
                return count > 0;
            }
        }

        public bool Delete(Expression<Func<ElderDto, bool>> exp)
        {
            throw new NotImplementedException();
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

        public ElderDto GetOne(Expression<Func<ElderDto, bool>> exp)
        {
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var where = exp.Cast<ElderDto, ElderEntity, bool>();
                var entity = dbSet.AsNoTracking().FirstOrDefault(where);
                return GetDto(entity);
            }
        }

        public ResultDto<ElderDto> GetWithPages(QueryBase queryBase, Expression<Func<ElderDto, bool>> exp, string orderBy, string orderDir = "desc")
        {
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var where = exp.Cast<ElderDto, ElderEntity, bool>();
                //var order = orderExp.Cast<MenuDto, MenuEntity, OrderKeyType>();
                var query = GetQuery(dbSet, where, orderBy, orderDir);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
                var list = query_list.ToList();
                var DtoList = new List<ElderDto>();

                list.ForEach(p => DtoList.Add(GetDto(p)));
                var dto = new ResultDto<ElderDto>
                {
                    recordsTotal = query_count.Value,
                    data = DtoList
                };
                return dto;
            }
        }

        public ResultDto<ElderDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<ElderDto, bool>> exp, Expression<Func<ElderDto, OrderKeyType>> orderExp, bool isDesc = true)
        {
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var where = exp.Cast<ElderDto, ElderEntity, bool>();
                var order = orderExp.Cast<ElderDto, ElderEntity, OrderKeyType>();
                var query = GetQuery(dbSet, where, order, isDesc);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
                var list = query_list.ToList();

                var dto = new ResultDto<ElderDto>
                {
                    recordsTotal = query_count.Value,
                    data = Mapper.Map<List<ElderEntity>, List<ElderDto>>(list)
                };
                return dto;
            }
        }

        public List<ElderDto> Query<OrderKeyType>(Expression<Func<ElderDto, bool>> exp, Expression<Func<ElderDto, OrderKeyType>> orderExp, bool isDesc = true)
        {
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var where = exp.Cast<ElderDto, ElderEntity, bool>();
                var order = orderExp.Cast<ElderDto, ElderEntity, OrderKeyType>();
                var query = GetQuery(dbSet, where, order, isDesc);
                var list = query.ToList();
                return Mapper.Map<List<ElderEntity>, List<ElderDto>>(list);
            }
        }

        public bool Update(IEnumerable<ElderDto> dtos)
        {
            using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var entities = Mapper.Map<IEnumerable<ElderDto>, IEnumerable<ElderEntity>>(dtos);
                dbSet.AddOrUpdate(entities.ToArray());
                return db.SaveChanges() > 0;
            }
        }

        public bool Update(ElderDto dto)
        {
            using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
                var entity = new ElderEntity()
                {
                    CreateDateTime = dto.CreateDateTime,
                    Creater = dto.Creater,
                    IsDeleted = false,
                    birthday = dto.birthday,
                    blood_type = dto.blood_type,
                    contacts = dto.contacts,
                    c_telephone = dto.c_telephone,
                    deformity_number = dto.deformity_number,
                    dispensedmode = dto.dispensedmode,
                    education = dto.education,
                    genetic_disease = dto.genetic_disease,
                    homephone = dto.homephone,
                    H_allergydrug = dto.H_allergydrug,
                    H_disease = dto.H_disease,
                    Id = dto.Id,
                    is_deformity = dto.is_deformity,
                    is_genetic = dto.is_genetic,
                    is_ops = dto.is_ops,
                    marriageStatus = dto.marriageStatus,
                    name = dto.name,
                    nation = dto.nation,
                    number = dto.number,
                    occupation = dto.occupation,
                    ops_data = dto.ops_data,
                    ops_disease = dto.ops_disease,
                    resident_type = dto.resident_type,
                    photo = dto.photo,
                    sex = dto.sex,
                    workunit = dto.workunit,
                    Facilitator_Code = dto.facilitatorCode,
                    Facilitator_Name = dto.facilitatorName
                };
                dbSet.AddOrUpdate(entity);
                return db.SaveChanges() > 0;
            }
        }
        private ElderDto GetDto(ElderEntity entity)
        {
            return new ElderDto()
            {
                Id = entity.Id,
                facilitatorName = entity.Facilitator_Name,
                facilitatorCode = entity.Facilitator_Code,
                number = entity.number,
                name = entity.name,
                nation = entity.nation,
                birthday = entity.birthday,
                blood_type = entity.blood_type,
                contacts = entity.contacts,
                c_telephone = entity.c_telephone,
                deformity_number = entity.deformity_number,
                dispensedmode = entity.dispensedmode,
                education = entity.education,
                genetic_disease = entity.genetic_disease,
                homeaddress = entity.homeaddress,
                homephone = entity.homephone,
                H_allergydrug = entity.H_allergydrug,
                H_disease = entity.H_disease,
                is_deformity = entity.is_deformity,
                is_genetic = entity.is_genetic,
                is_ops = entity.is_ops,
                marriageStatus = entity.marriageStatus,
                occupation = entity.occupation,
                ops_data = entity.ops_data,
                ops_disease = entity.ops_disease,
                resident_type = entity.resident_type,
                sex = entity.sex,
                workunit = entity.workunit,
                photo = entity.photo,
                Modifier = (int)entity.Modifier,
                IsDeleted = entity.IsDeleted,
                CreateDateTime = entity.CreateDateTime,
                Creater = entity.Creater
            };
        }
    }
}
