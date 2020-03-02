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

namespace InBed.Service.Abstracts
{
    public class SleepingService : IDependency, ISleepingService
    {
        #region 构造函数注册上下文
        public IDbContextScopeFactory _dbScopeFactory { get; set; }
        #endregion
        public ResultDto<SleepingTimeDto> WeekSleepingDate(string f_code, int elderID)
        {
            ResultDto<SleepingTimeDto> res = new ResultDto<SleepingTimeDto>();
            try
            {
                var elderEntity = new ElderDAL().Details(elderID);
                if (elderEntity != null)
                {
                    if (!elderEntity.FacilitatorCode.Equals(f_code))
                    {
                        return new ResultDto<SleepingTimeDto>() ;
                    }
                    var sleepEntity = new SleepingTimePeriodDAL().GetWeekSleepingDate(elderEntity.Id);
                    List<SleepingTimeDto> resList = new List<SleepingTimeDto>();
                    List<SleepData> DataObj = new List<SleepData>();

                    var GroupEntity = sleepEntity.GroupBy(p => p.Data);
                    foreach (var entity in GroupEntity)
                    {
                        var listData = entity.ToList();
                        SleepingTimeDto item = new SleepingTimeDto();
                        item.Date = listData.FirstOrDefault().Data;
                        item.ElderID = elderEntity.Id;
                        item.ElderName = elderEntity.name;
                        bool falg = true;
                        for (int i = 0; i < listData.Count; i++)
                        {
                            if (falg)
                            {
                                item.objData.Add(new SleepData
                                {
                                    BeginTime = DateTime.Parse(listData[i].BeginTime),
                                    SleepType = (SleepType)listData[i].SleepType,
                                    EndTime = DateTime.Parse(listData[i].BeginTime)
                                });
                                falg = false;
                            }
                            if (i < listData.Count - 1)
                            {
                                if (listData[i].SleepType != listData[i + 1].SleepType)
                                {
                                    item.objData[item.objData.Count - 1].EndTime = DateTime.Parse(listData[i + 1].BeginTime);
                                    falg = true;
                                }
                            }
                            else
                            {
                                item.objData[item.objData.Count - 1].EndTime = DateTime.Parse(listData[i].BeginTime);
                            }
                        }
                        resList.Add(item);
                    }
                    res.data = resList;
                }

            }
            catch (Exception ex)
            {

            }
            return res;
        }
    }
}
