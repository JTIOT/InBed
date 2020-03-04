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
using InBed.Service.DTO;
using InBed.Data.DAL;
using InBed.Service.Request;
using System.Diagnostics;

namespace InBed.Service.Abstracts
{
    public class ElderService : IDependency, IElderService
    {
        #region 构造函数注册上下文
        public IDbContextScopeFactory _dbScopeFactory { get; set; }

        public IHearbeatStatusService hearbeatStatusService { get; set; }

        public IOnBedRecordService onBedRecordService { get; set; }
        public IDeviceBindingService deviceBindingService { get; set; }
        public ISleepingService sleepingService { get; set; }




        #endregion
        public Result<ElderDto> AddElder(ElderDto model)
        {
            Result<ElderDto> res = new Result<ElderDto>();
            try
            {
                var entity = new ElderEntity {
                    FacilitatorCode = model.FacilitatorCode,
                    FacilitatorName = model.FacilitatorName,
                    birthday = model.birthday,
                    blood_type = model.blood_type,
                    contacts = model.contacts,
                    CreateDateTime = model.CreateDateTime,
                    Creater = model.Creater,
                    c_telephone = model.c_telephone,
                    deformity_number = model.deformity_number,
                    dispensedmode = model.dispensedmode,
                    education = model.education,
                    genetic_disease = model.genetic_disease,
                    homeaddress = model.homeaddress,
                    homephone = model.homephone,
                    H_allergydrug = model.H_allergydrug,
                    H_disease = model.H_disease,
                    IsDeleted = model.IsDeleted,
                    IsWelfare = model.IsWelfare,
                    is_deformity = model.is_deformity,
                    is_genetic = model.is_genetic,
                    is_ops = model.is_ops,
                    marriageStatus = model.marriageStatus,
                    MapPoint = model.MapPoint,
                    name = model.name,
                    nation = model.nation,
                    number = model.number,
                    occupation = model.occupation,
                    ops_data = DateTime.Parse("1900-1-1"),
                    ops_disease = model.ops_disease,
                    photo = model.photo,
                    resident_type = model.resident_type,
                    sex = (int)model.sex,
                    workunit = model.workunit
                };
                if (StringExtension.IsBlank(entity.name))
                {
                    return new Result<ElderDto>() { flag = false, msg = "老人姓名不能为空，请输入！" };
                }
                if (StringExtension.IsBlank(entity.homephone))
                {
                    return new Result<ElderDto>() { flag = false, msg = "老人电话不能为空，请输入！" };
                }
                if (StringExtension.IsBlank(entity.homephone))
                {
                    return new Result<ElderDto>() { flag = false, msg = "老人电话号码格式不正确，请重新输入！" };
                }
                if (StringExtension.IsBlank(entity.homeaddress))
                {
                    return new Result<ElderDto>() { flag = false, msg = "老人住址不能为空，请输入！" };
                }
                var flag = new ElderDAL().AddElder(entity);
                if (flag)
                {
                    res.flag = true;
                }
                else
                {
                    res.flag = false;
                    res.msg = "老人信息添加失败！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public Result<ElderDto> DelElder(List<int> ids)
        {
            Result<ElderDto> res = new Result<ElderDto>();
            try
            {
                if (new ElderDAL().DelElder(ids))
                {
                    res.flag = true;
                    res.msg = "老人信息删除成功！";
                }
                else
                {
                    res.flag = false;
                    res.msg = "老人信息删除失败！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public Result<ElderDto> DelElder(int id)
        {
            Result<ElderDto> res = new Result<ElderDto>();
            try
            {
                if (new ElderDAL().DelElder(id))
                {
                    res.flag = true;
                    res.msg = "老人信息删除成功！";
                }
                else
                {
                    res.flag = false;
                    res.msg = "老人信息删除失败！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public Result<ElderDto> EditElder(ElderDto model)
        {
            Result<ElderDto> res = new Result<ElderDto>();
            try
            {
                var entity = new ElderEntity
                {
                    FacilitatorCode = model.FacilitatorCode,
                    FacilitatorName = model.FacilitatorName,
                    birthday = model.birthday,
                    blood_type = model.blood_type,
                    contacts = model.contacts,
                     ModifyTime = DateTime.Now,
                     Modifier = model.Creater,
                    c_telephone = model.c_telephone,
                    deformity_number = model.deformity_number,
                    dispensedmode = model.dispensedmode,
                    education = model.education,
                    genetic_disease = model.genetic_disease,
                    homeaddress = model.homeaddress,
                    homephone = model.homephone,
                    H_allergydrug = model.H_allergydrug,
                    H_disease = model.H_disease,
                    IsDeleted = model.IsDeleted,
                    IsWelfare = model.IsWelfare,
                    is_deformity = model.is_deformity,
                    is_genetic = model.is_genetic,
                    is_ops = model.is_ops,
                    marriageStatus = model.marriageStatus,
                    MapPoint = model.MapPoint,
                    name = model.name,
                    nation = model.nation,
                    number = model.number,
                    occupation = model.occupation,
                    ops_data = DateTime.Parse("1900-1-1"),
                    ops_disease = model.ops_disease,
                    photo = model.photo,
                    resident_type = model.resident_type,
                    sex = (int)model.sex,
                    workunit = model.workunit,
                     Id=model.Id

                };
                var flag = new ElderDAL().EditElder(entity);
                if (flag)
                {
                    res.flag = true;
                    res.msg = "老人信息修改成功！";
                }
                else
                {
                    res.flag = false;
                    res.msg = "老人信息修改失败！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public ElderDto GetDeteail(int id)
        {
            try
            {
                var entity = new ElderDAL().Details(id);
                if (entity != null)
                {
                    return new ElderDto {
                        birthday = entity.birthday,
                        blood_type = entity.blood_type,
                        contacts = entity.contacts,
                        CreateDateTime = entity.CreateDateTime,
                        Creater = entity.Creater,
                        c_telephone = entity.c_telephone,
                        deformity_number = entity.deformity_number,
                        dispensedmode = entity.dispensedmode,
                        education = entity.education,
                        FacilitatorCode = entity.FacilitatorCode,
                        FacilitatorName = entity.FacilitatorName,
                        genetic_disease = entity.genetic_disease,
                        homeaddress = entity.homeaddress,
                        homephone = entity.homephone,
                        H_allergydrug = entity.H_allergydrug,
                        H_disease = entity.H_disease,
                        Id = entity.Id,
                        IsDeleted = entity.IsDeleted,
                        is_deformity = entity.is_deformity,
                        is_genetic = entity.is_genetic,
                        is_ops = entity.is_ops,
                        marriageStatus = entity.marriageStatus,
                        Modifier = entity.Modifier,
                        name = entity.name,
                        nation = entity.nation,
                        number = entity.number,
                        occupation = entity.occupation,
                        ops_data = entity.ops_data,
                        ops_disease = entity.ops_disease,
                        photo = entity.photo,
                        resident_type = entity.resident_type,
                        sex = (SexEnum)entity.sex,
                        workunit = entity.workunit,
                        IsWelfare = entity.IsWelfare,
                        MapPoint = entity.MapPoint

                    };
                }
                else
                {
                    return new ElderDto();
                }
            }
            catch (Exception ex)
            {
                return new ElderDto();
            }
        }

        public ResultDto<ElderDto> GetWithPages(ElderRequest request)
        {
            ResultDto<ElderDto> res = new ResultDto<ElderDto>();
            try
            {
                int count = 0;
                var entity = new ElderDAL().GetWithPages(request.Start, request.Length, request.FacilitatorCode, request.Name, request.Number, request.HomePhone, request.Sex,
                                                                        request.StartTime, request.EndTime, request.OrderBy, out count, request.OrderDir);
                res.recordsTotal = count;
                res.data = getDto(entity);
            }
            catch (Exception ex)
            {
                res.recordsTotal = 0;
                res.data = new List<ElderDto>();
            }
            return res;
        }
        public List<ElderDto> getDto(List<ElderEntity> entity)
        {
            List<ElderDto> res = new List<ElderDto>();
            entity.ForEach(item => res.Add(new ElderDto
            {
                birthday = item.birthday,
                blood_type = item.blood_type,
                contacts = item.contacts,
                CreateDateTime = item.CreateDateTime,
                Creater = item.Creater,
                c_telephone = item.c_telephone,
                deformity_number = item.deformity_number,
                dispensedmode = item.dispensedmode,
                education = item.education,
                FacilitatorCode = item.FacilitatorCode,
                FacilitatorName = item.FacilitatorName,
                genetic_disease = item.genetic_disease,
                homeaddress = item.homeaddress,
                homephone = item.homephone,
                H_allergydrug = item.H_allergydrug,
                H_disease = item.H_disease,
                Id = item.Id,
                IsDeleted = item.IsDeleted,
                is_deformity = item.is_deformity,
                is_genetic = item.is_genetic,
                is_ops = item.is_ops,
                marriageStatus = item.marriageStatus,
                Modifier = item.Modifier,
                name = item.name,
                nation = item.nation,
                number = item.number,
                occupation = item.occupation,
                ops_data = item.ops_data,
                ops_disease = item.ops_disease,
                photo = item.photo,
                resident_type = item.resident_type,
                sex = (SexEnum)item.sex,
                workunit = item.workunit,
                IsWelfare = item.IsWelfare,
                MapPoint = item.MapPoint

            }));
            return res;
        }
        public ElderCountDto GetElderCount(string facilitator)
        {
            try
            {

                var entity = new ElderDAL().GetElderCount(facilitator);
                ElderCountDto res = new ElderCountDto
                {
                    NewCount = entity.NewCount,
                    SumCount = entity.SumCount
                };
                return res;

            }
            catch (Exception ex)
            {
                return new ElderCountDto();
            }
        }

        public List<AgeGroupDto> AgeGroup(string facilitator)
        {
            try
            {
                List<AgeGroupDto> res = new List<AgeGroupDto>();
                var entity = new ElderDAL().AgeGroup(facilitator);
                if (entity != null && entity.Any())
                {
                    entity.ForEach(item => res.Add(new AgeGroupDto
                    {
                        name = item.Age,
                        value = item.Number
                    }));
                }
                return res;
            }
            catch (Exception ex)
            {
                return new List<AgeGroupDto>();
            }
        }

        public List<MapPositionDto> GetMapPosition(string facilitator, string province)
        {
            try
            {
                List<MapPositionDto> res = new List<MapPositionDto>();
                var entity = new ElderDAL().MapPosition(facilitator, province);
                if (entity != null && entity.Any())
                {
                    foreach (var item in entity)
                    {
                        if (ObjClass.ElderMap.ContainsKey(item.Id))
                        {

                            ObjClass.ElderMap[item.Id].IsBed = item.IsBed;
                            ObjClass.ElderMap[item.Id].Flag = 1;
                            ObjClass.ElderMap[item.Id].NewTime = DateTime.Now;
                            ObjClass.ElderMap[item.Id].MapPoint = item.MapPoint;
                            ObjClass.ElderMap[item.Id].Homeaddress = item.Homeaddress;
                            ObjClass.ElderMap[item.Id].FacilitatorName = item.FacilitatorName;

                        }
                        else
                        {
                            ObjClass.ElderMap.Add(item.Id, new MapPositionDto
                            {
                                Id = item.Id,
                                IsBed = item.IsBed,
                                Homeaddress = item.Homeaddress,
                                MapPoint = item.MapPoint,
                                Flag = 1,
                                NewTime = DateTime.Now,
                                FacilitatorName = item.FacilitatorName
                            });
                        }
                    }
                    var data = (from kv in ObjClass.ElderMap.Values where kv.Flag == 1 select kv).ToList<MapPositionDto>();
                    data.ForEach(item => res.Add(new MapPositionDto
                    {
                        Id = item.Id,
                        IsBed = item.IsBed,
                        MapPoint = item.MapPoint,
                        Homeaddress = item.Homeaddress,
                        Flag = 1,
                        FacilitatorName = item.FacilitatorName,
                        NewTime = DateTime.Now
                    }));
                    foreach (var item in ObjClass.ElderMap)
                    {
                        item.Value.Flag = 0;

                    }

                }
                return res;
            }
            catch (Exception ex)
            {
                return new List<MapPositionDto>();
            }
        }
        public Result<ElderStatisticsDto> GetElderStatistics(string facilitator, int elderID, string customerID)
        { 
            Result<ElderStatisticsDto> res = new Result<ElderStatisticsDto>();
            try
            {
                ElderStatisticsDto data = new ElderStatisticsDto();
                var elderEntity = new ElderDAL().Details(elderID);
                if (elderEntity != null)
                {
                    int count = 0;
                    data.ElderID = elderEntity.Id

;
                    data.ElderName = elderEntity.name

;
                    data.Birthday = elderEntity.birthday;
                    data.Sex = (SexEnum)elderEntity.sex;
                    data.HomePhone = elderEntity.homephone;
                    data.HomeAddress = elderEntity.homeaddress;
                    List<int> ids = new List<int>();
                    ids.Add(elderEntity.Id);

                    //get analysis data
                    //var AnalysisEntity = new ElderAnalysisDAL().GetWithPages(0, 10, ids, DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), "id", out count);
                    //if (AnalysisEntity != null && AnalysisEntity.Any())
                    //{
                    //    var TopData = AnalysisEntity.FirstOrDefault();
                    //    Debug.WriteLine(TopData);
                    //    data.SleepLength = TopData.SeepLong.ToString();
                    //    data.InBedTime = TopData.InBedTime.ToString();
                    //    data.NightCount = TopData.NightCount;
                    //    data.DeepSleepLength = TopData.DepthSeepLong.ToString();
                    //    data.GetUpTime = TopData.GetUpTime.ToString();
                    //    data.FallAsleepTime = TopData.FallAsleepTime.ToString();
                    //    data.FallAsleepLength = TopData.FallAsleepTime.Subtract(TopData.InBedTime).TotalMinutes.ToString();
                    //}

                    //get daily analysis entity (Refer to DailyAnalysisEntity)
                    //var dailyAnalysisEntity = new DailyAnalysisDAL().GetWithPages(customerID, DateTime.Now.AddDays(-1).ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd"));

                    var dailyAnalysisEntity = new List<DailyAnalysisEntity>();
                    dailyAnalysisEntity.Add(new DailyAnalysisDAL().GetData(customerID));

                    if (dailyAnalysisEntity.Count > 0)
                    {
                        //get first data from array
                        var retData = dailyAnalysisEntity.FirstOrDefault();
                        data.SleepLength = retData.sleepingTime;
                        data.NightCount = retData.nightTimeAwakenings.ToInt();
                        data.DeepSleepLength = retData.deepSleepTime;
                        
                        data.AverageBreath = retData.avgBR;
                        data.AverageHM = retData.avgHB;

                        DateTime goToBedTime = DateTime.ParseExact(retData.gotoBedTime, "yyyyMMddHHmmssfff",
                            System.Globalization.CultureInfo.InvariantCulture);
                        data.InBedTime = goToBedTime.ToString("yyyy/MM/dd HH:mm");

                        if (retData.getupTime != null)
                        {
                            DateTime GetUpTime = DateTime.ParseExact(retData.getupTime, "yyyyMMddHHmmssfff",
                                System.Globalization.CultureInfo.InvariantCulture);
                            data.GetUpTime = GetUpTime.ToString("yyyy/MM/dd HH:mm");
                        }
                        else
                        {
                            data.GetUpTime = "8:30以後";
                        }

                        DateTime fallAsSleepTime = goToBedTime.AddMinutes(retData.sleepLatency.ToInt());
                        data.FallAsleepTime = fallAsSleepTime.ToString("yyyy/MM/dd HH:mm");


                        DateTime inBedTime = DateTime.ParseExact(retData.gotoBedTime, "yyyyMMddHHmmssfff",
                            System.Globalization.CultureInfo.InvariantCulture);

                        data.FallAsleepLength = fallAsSleepTime.Subtract(inBedTime).TotalMinutes.ToString();

                    }
                }
                res.data = data;
            }
            catch (Exception ex)
            {

            }
            return res;
        }

        public List<ProvinceNumberDto> GetPlaceNameeNumber(string facilitator)
        {
            List<ProvinceNumberDto> res = new List<ProvinceNumberDto>();
            try
            {
                var F_Entity = new FacilitatorDAL().Query(facilitator);
                if (F_Entity != null)
                {
                    var entity = new ElderDAL().GetProvinceNumber(F_Entity.code, F_Entity.F_Level);
                    if (entity != null && entity.Any())
                    {
                        entity.ForEach(item => res.Add(new ProvinceNumberDto
                        {
                            Number = item.Number,
                            PlaceNamee = item.PlaceNamee

                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                return new List<ProvinceNumberDto>();
            }
            return res;
        }


        public List<ProvinceOnlineNumberDto> GetPlaceOnlineNumber(string facilitator)
        {
            List<ProvinceOnlineNumberDto> res = new List<ProvinceOnlineNumberDto>();
            try
            {
                var F_Entity = new FacilitatorDAL().Query(facilitator);
                if (F_Entity != null)
                {
                    var entity = new ElderDAL().GetProvinceNumber(F_Entity.code, F_Entity.F_Level);
                    if (entity != null && entity.Any())
                    {
                        entity.ForEach(item => res.Add(new ProvinceOnlineNumberDto
                        {
                            SumNumber = item.Number,
                            PlaceNamee = item.PlaceNamee
                        }));
                    }
                    var onlineEntity = new ElderDAL().GetOnlineProvinceNumber(F_Entity.code, F_Entity.F_Level);
                    if (onlineEntity != null && onlineEntity.Any())
                    {
                        foreach (var onlin in onlineEntity)
                        {
                            var item = res.Find(p => p.PlaceNamee== onlin.PlaceNamee);
                            item.OnlineNumber = onlin.Number;
                        }
                        
                    }
                }
                
            }
            catch (Exception ex)
            {
                return new List<ProvinceOnlineNumberDto>();
            }
            return res;
        }


        public void ElderAnalysis()
        {
            var FallAsleepTime = DateTime.Parse(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 18:00 ");//入睡时间           
            var SoberTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 08:00 ");//清醒时间           
            var inBedTime = DateTime.Parse(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 18:00 ");//上床时间          
            var OutBedTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 08:00 "); //起床时间          
            var nightCount = 0;//起夜次数
            var DepthSeepLong = 0;//浅睡时长
            var ShallowSeepLong = 0;//深睡时长
            var SoberSeepLong = 0;//清醒时长
            var SoberCount = 0;//清醒次数
            //获取所有老人
            var elderEntity = new ElderDAL().AnalysisQuery();
            //获取所有老人设置信息
            var alarmSetupEntity = new ElderAlarmSetupDAL().AnalysisQuery();
            //获取所有老人当天睡眠数据
            var sleepDataEntity = new SleepingTimePeriodDAL().GetDaySleepingDate();
            if (elderEntity != null && elderEntity.Any())
            {
                OnBedRecordDAL OnBedRecord = new OnBedRecordDAL();
                foreach (var elder in elderEntity)
                {
                    nightCount = 0;//起夜次数
                    DepthSeepLong = 0;//浅睡时长
                    ShallowSeepLong = 0;//深睡时长
                    SoberSeepLong = 0;//清醒时长
                   SoberCount = 0;//清醒次数

                    //获取当前老人的设置信息
                    var setup = alarmSetupEntity.Find(item => item.Elder_id == elder.Id);
                    //获取睡觉时间
                    var sleeptime = string.Empty;
                    if (setup != null && setup.sleeptime != null)
                    {
                        sleeptime = setup.sleeptime;
                    }
                    else
                    {
                        sleeptime = "18:00";
                    }

                    sleeptime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " " + sleeptime;
                    if (sleeptime != string.Empty)
                    {
                        //获取比设定时间最近（小于）时间
                        var up_elderSleep = sleepDataEntity.FindAll(item => item.ProductID == elder.Id && DateTime.Parse(item.BeginTime) > DateTime.Parse(sleeptime)).OrderBy(item => item.BeginTime).FirstOrDefault();
                     //   var temp_elderSleep = sleepDataEntity.FindAll(item => item.ProductID == elder.Id && DateTime.Parse(item.BeginTime) <= DateTime.Parse(sleeptime)).OrderByDescending(item => item.BeginTime).FirstOrDefault();
                        if (up_elderSleep == null)
                        {
                            continue;
                        }
                        //if (up_elderSleep == null)
                        //{
                        //    // up_elderSleep = temp_elderSleep;
                        //    continue;
                        //}
                        if (up_elderSleep.SleepType == (int)SleepType.浅睡)
                        {
                            FallAsleepTime = DateTime.Parse(up_elderSleep.BeginTime);
                        }
                        else if (up_elderSleep.SleepType == (int)SleepType.深睡)
                        {
                            //获取比设定时间最近（大于）时间
                            var D_elderSleep = sleepDataEntity.FindAll(item => item.ProductID == elder.Id && DateTime.Parse(item.BeginTime) < DateTime.Parse(sleeptime) && item.SleepType == (int)SleepType.浅睡).OrderByDescending(item => item.BeginTime).FirstOrDefault();
                            if (D_elderSleep != null && !StringExtension.IsBlank(D_elderSleep.BeginTime))
                                FallAsleepTime = DateTime.Parse(D_elderSleep.BeginTime);
                        }
                        else
                        {
                            var D_elderSleep = sleepDataEntity.FindAll(item => item.ProductID == elder.Id && DateTime.Parse(item.BeginTime) > DateTime.Parse(sleeptime) && item.SleepType == (int)SleepType.浅睡).OrderBy(item => item.BeginTime).FirstOrDefault();
                            if (D_elderSleep != null && !StringExtension.IsBlank(D_elderSleep.BeginTime))
                                FallAsleepTime = DateTime.Parse(D_elderSleep.BeginTime);
                        }
                        var sober_elderSleep = sleepDataEntity.FindAll(item => item.ProductID == elder.Id && DateTime.Parse(item.BeginTime) < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 12:00") && item.SleepType == (int)SleepType.浅睡).OrderByDescending(item => item.BeginTime).FirstOrDefault();
                        if (sober_elderSleep != null && !StringExtension.IsBlank(sober_elderSleep.StopTime))
                            SoberTime = DateTime.Parse(sober_elderSleep.StopTime);
                    }
                    var inbedEntity = OnBedRecord.GetDayInBedRecord(elder.Id, FallAsleepTime);
                    if (inbedEntity != null && !StringExtension.IsBlank(inbedEntity.OnBedTime))
                        inBedTime = DateTime.Parse(inbedEntity.OnBedTime);
                    var outbedEntity = OnBedRecord.GetDayOutBedRecord(elder.Id, SoberTime);
                    if (outbedEntity != null && !StringExtension.IsBlank(outbedEntity.LeaveBedTime))
                        OutBedTime = DateTime.Parse(outbedEntity.LeaveBedTime);
                    var countEntity = OnBedRecord.GetOutBedCount(elder.Id, inBedTime, OutBedTime);
                    if (countEntity != null)
                        nightCount = countEntity.count;

                    //获取老人从入睡时间到清醒时间的所有数据
                    var ElderSleepData = sleepDataEntity.FindAll(item => item.ProductID == elder.Id && DateTime.Parse(item.BeginTime) >= FallAsleepTime && DateTime.Parse(item.BeginTime) <= SoberTime).OrderBy(item => item.BeginTime);
                    var DayItemEntity = ElderSleepData.FirstOrDefault();
                    for (int i = 0; i < ElderSleepData.ToList().Count; i++)
                    {
                        if (i != 0)
                        {
                            //睡眠类型发生改变计算各个睡眠类型的时长
                            if (ElderSleepData.ToList()[i].SleepType != DayItemEntity.SleepType)
                            {
                                if (DayItemEntity.SleepType == (int)SleepType.浅睡)
                                {                                    
                                    DepthSeepLong += Convert.ToInt32(DateTime.Parse(ElderSleepData.ToList()[i].BeginTime).Subtract(DateTime.Parse(DayItemEntity.BeginTime)).TotalMinutes);
                                }
                                else if (DayItemEntity.SleepType == (int)SleepType.深睡)
                                {
                                    ShallowSeepLong += Convert.ToInt32(DateTime.Parse(ElderSleepData.ToList()[i].BeginTime).Subtract(DateTime.Parse(DayItemEntity.BeginTime)).TotalMinutes);
                                }
                                else if (DayItemEntity.SleepType == (int)SleepType.清醒)
                                {
                                    SoberSeepLong += Convert.ToInt32(DateTime.Parse(ElderSleepData.ToList()[i].BeginTime).Subtract(DateTime.Parse(DayItemEntity.BeginTime)).TotalMinutes);
                                }
                                DayItemEntity = ElderSleepData.ToList()[i];
                            }

                        }
                    }
                    //计算睡眠周期内的清醒次数
                    var SoberEntity = sleepDataEntity.FindAll(item => item.ProductID == elder.Id && DateTime.Parse(item.BeginTime) >= FallAsleepTime && DateTime.Parse(item.BeginTime) <= SoberTime && item.SleepType == (int)SleepType.清醒).OrderBy(item => item.BeginTime);
                    if (SoberEntity != null)
                    {
                        SoberCount = SoberEntity.ToList().Count;
                    }

                    //插入每日数据分析结果
                    ElderAnalysisEntity ElderAnalysis = new ElderAnalysisEntity
                    {
                        AnalysisDate = DateTime.Now.ToString("yyyy-MM-dd"),
                        InBedTime = inBedTime,
                        GetUpTime = OutBedTime,
                        FallAsleepTime = FallAsleepTime,
                        SoberTime = SoberTime,
                        DepthSeepLong = DepthSeepLong,
                        ShallowSeepLong = ShallowSeepLong,
                        SoberSeepLong = SoberSeepLong,
                        NightCount = nightCount,
                        SoberCount = SoberCount,
                        SeepLong = DepthSeepLong + ShallowSeepLong + SoberSeepLong,
                        ElderID = elder.Id
                    };
                    bool falg = new ElderAnalysisDAL().AddElderAnalysis(ElderAnalysis);
                }

            }
        }

        public ElderDto[] GetDetails()
        {
            try
            {
                var arrEntity = new ElderDAL().AllDetails();
                var elderList = new List<ElderDto>();

                if (arrEntity != null)
                {
                    for (int i = 0; i < arrEntity.Length; i++)
                    {
                        var entity = arrEntity[i];

                        var elder = new ElderDto
                        {
                            birthday = entity.birthday,
                            blood_type = entity.blood_type,
                            contacts = entity.contacts,
                            CreateDateTime = entity.CreateDateTime,
                            Creater = entity.Creater,
                            c_telephone = entity.c_telephone,
                            deformity_number = entity.deformity_number,
                            dispensedmode = entity.dispensedmode,
                            education = entity.education,
                            FacilitatorCode = entity.FacilitatorCode,
                            FacilitatorName = entity.FacilitatorName,
                            genetic_disease = entity.genetic_disease,
                            homeaddress = entity.homeaddress,
                            homephone = entity.homephone,
                            H_allergydrug = entity.H_allergydrug,
                            H_disease = entity.H_disease,
                            Id = entity.Id,
                            IsDeleted = entity.IsDeleted,
                            is_deformity = entity.is_deformity,
                            is_genetic = entity.is_genetic,
                            is_ops = entity.is_ops,
                            marriageStatus = entity.marriageStatus,
                            Modifier = entity.Modifier,
                            name = entity.name,
                            nation = entity.nation,
                            number = entity.number,
                            occupation = entity.occupation,
                            ops_data = entity.ops_data,
                            ops_disease = entity.ops_disease,
                            photo = entity.photo,
                            resident_type = entity.resident_type,
                            sex = (SexEnum)entity.sex,
                            workunit = entity.workunit,
                            IsWelfare = entity.IsWelfare,
                            MapPoint = entity.MapPoint

                        };

                        elderList.Add(elder);
                    }

                    return elderList.ToArray();
                }
                else
                {
                    return new ElderDto[0];
                }
            }
            catch (Exception ex)
            {
                return new ElderDto[0];
            }
        }
        public ResultDto<ElderInfoDto> GetElderPages(ElderRequest request)
        {
            ResultDto<ElderInfoDto> res = new ResultDto<ElderInfoDto>();
            List<ElderInfoDto> dataList = new List<ElderInfoDto>();
            try
            {
                int count = 0;
                ElderDAL obj = new ElderDAL();
                var entity = obj.ElderListQuery(request.FacilitatorCode);
                res.recordsTotal = entity.Count;               
                if (entity != null && entity.Any())
                {
                    var displayEntity = entity.Skip(request.Start * request.Length).Take(request.Length).ToList();
                    foreach (var item in displayEntity)
                    {
                        ElderInfoDto ElderInfo = new ElderInfoDto();
                        ElderInfo.ElderID = item.id;
                        ElderInfo.ElderName = item.name;
                        ElderInfo.birthday = item.birthday;
                        ElderInfo.sex = (SexEnum)item.sex;
                        ElderInfo.HomePhone = item.homephone;
                        ElderInfo.HomeAdderss = item.homeaddress;
                        ElderInfo.MapPoint = item.MapPoint;
                        ElderInfo.HD = item.avHR;
                        ElderInfo.Online = item.IsOnline;
                        ElderInfo.Inbed = item.IsBed;

                        if (ElderInfo.HD <= 0)
                        {
                            ElderInfo.Online = 1;
                        }
                        else if(ElderInfo.HD > 0)
                        {
                            if(ElderInfo.Inbed == 0)
                            {
                                ElderInfo.HD = 0;
                            }
                        }

                        ///告警设置
                        var setElderEntity = new ElderAlarmSetupDAL().Query(item.id);
                        AlarmSetupDto setDto;
                        if (setElderEntity != null)
                        {
                            setDto = new AlarmSetupDto
                            {
                                inbedtime = setElderEntity.inbedtime,
                                leavebedtime = setElderEntity.leavebedtime,
                                maxbreath = setElderEntity.maxbreath,
                                maxheart = setElderEntity.maxheart,
                                minbreath = setElderEntity.minbreath,
                                minheart = setElderEntity.minheart,
                                pausebreath = setElderEntity.pausebreath
                            };
                        }
                        else
                        {
                            setDto = new AlarmSetupDto
                            {
                                maxheart = 90,
                                minheart = 50,
                            };
                        }
                        ElderInfo.AlarmSetup = setDto;
                        dataList.Add(ElderInfo);


                    }
                    res.data = dataList;
                }
            }
            catch (Exception ex)
            {


            }
            return res;

        }

        public ResultDto<ElderInfoDto> ElderQuery(ElderRequest request)
        {
            ResultDto<ElderInfoDto> res = new ResultDto<ElderInfoDto>();
            List<ElderInfoDto> dataList = new List<ElderInfoDto>();
            try
            {
                int count = 0;
                ElderDAL obj = new ElderDAL();
                var entity = obj.ElderListQuery(request.FacilitatorCode, request.Name);
                res.recordsTotal = entity.Count;
                if (entity != null && entity.Any())
                {
                    var displayEntity = entity.Skip(request.Start * request.Length).Take(request.Length).ToList();
                    foreach (var item in displayEntity)
                    {
                        ElderInfoDto ElderInfo = new ElderInfoDto();
                        ElderInfo.ElderID = item.id;
                        ElderInfo.ElderName = item.name;
                        ElderInfo.birthday = item.birthday;
                        ElderInfo.sex = (SexEnum)item.sex;
                        ElderInfo.HomePhone = item.homephone;
                        ElderInfo.HomeAdderss = item.homeaddress;
                        ElderInfo.MapPoint = item.MapPoint;
                        ElderInfo.HD = item.avHR;
                        ElderInfo.Online = item.IsOnline;
                        ElderInfo.Inbed = item.IsBed;

                        if (ElderInfo.HD <= 0)
                        {
                            ElderInfo.Online = 1;
                        }
                        else if (ElderInfo.HD > 0)
                        {
                            if (ElderInfo.Inbed == 0)
                            {
                                ElderInfo.HD = 0;
                            }
                        }

                        dataList.Add(ElderInfo);
                    }
                    res.data = dataList;
                }
            }
            catch (Exception ex)
            {


            }
            return res;
        }
        /// <summary>
        /// 老人在床重量变化查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<Pressure> ElderPressure(ElderRequest request)
        {
            List<Pressure> res = new List<Pressure>();
            try
            {
                List<ElderInfoDto> dataList = new List<ElderInfoDto>();
                var elderEentity = new ElderDAL().ElderQuery(request.FacilitatorCode, request.Name);
                if (elderEentity != null && elderEentity.Any())
                {
                    var itemEentity = elderEentity.FirstOrDefault();
                    var devEntity = new DeviceDAL().GetElderDevice(itemEentity.Id);

                    if (devEntity != null)
                    {
                        var PressureEentity = new HearbeatStatusDAL().GetRealTime(ClassUtility.GetDeviceName(devEntity.device_number), request.StartTime, request.EndTime);
                        if (PressureEentity != null)
                        {
                            PressureEentity.ForEach(p => res.Add(new Pressure { id = p.ID, time = p.ReceiveTime.ToString(), value = p.PressureValue.ToString() }));

                        }
                    }
                }            

            }
            catch (Exception ex)
            {

            }
            return res;
        }

        public List<SleepDto> GetCurrentDaySleepingDate(string f_code, int id)
        {
            List<SleepDto> res = new List<SleepDto>();
            try
            {
                var elderEentity = new ElderDAL().Query(f_code);
                if (elderEentity != null && elderEentity.Any())
                {
                    var entity = new SleepingTimePeriodDAL().GetCurrentDaySleepingDate(id);
                    if (entity != null && entity.Any())
                    {
                        res.AddRange(ConvertMinuteData(entity));
                    }
                }

            }
            catch (Exception ex)
            {
                return new List<SleepDto>();
            }
            return res;
        }

        public List<SleepDto> ConvertMinuteData(List<DaySleepingEntity> data)
        {
            List<SleepDto> res = new List<SleepDto>();
            for (int i = 0; i < data.Count; i++)
            {
                if (i < data.Count - 1)
                {
                    res.Add(new SleepDto { BeginTime = data[i].BeginTime, SleepType = (SleepType)data[i].SleepType });
                    DateTime endTime = DateTime.Now;
                    if (!StringExtension.IsBlank(data[i].StopTime))
                    {
                        endTime = DateTime.Parse(data[i].StopTime);

                    }
                    else
                    {
                        endTime = DateTime.Parse(data[i + 1].BeginTime);
                    }
                    var minutes = Convert.ToInt32(DateTime.Parse(data[i + 1].BeginTime).Subtract(endTime).TotalMinutes);
                    if (minutes > 2)
                    {
                        res.Add(new SleepDto { BeginTime = data[i].StopTime, SleepType = SleepType.离床 });
                        data[i + 1].BeginTime = DateTime.Parse(data[i + 1].BeginTime).AddMinutes(-6).ToString("yyyy/MM/dd HH:mm:ss");
                    }
                }
                else
                {
                    res.Add(new SleepDto { BeginTime = data[i].BeginTime, SleepType = (SleepType)data[i].SleepType });
                }

            }


            return res;

        }




    }
}
