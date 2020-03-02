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
    public class OnBedRecordService :  IDependency, IOnBedRecordService
    {
        #region 构造函数注册上下文
        public IDbContextScopeFactory _dbScopeFactory { get; set; }

        public ResultDto<ElderAnalysisDto> HalfMonthOnBedRecord(string f_code, int elderID)
        {
            ResultDto<ElderAnalysisDto> res = new ResultDto<ElderAnalysisDto>();
            try
            {
                var elderEntity = new ElderDAL().Details(elderID);
                if (elderEntity != null)
                {
                    int count = 0;
                    if (!elderEntity.FacilitatorCode.Equals(f_code))
                    {
                        return new ResultDto<ElderAnalysisDto>();
                    }
                    List<int> ids = new List<int>();
                    ids.Add(elderEntity.Id);
                    var AnalysisEntity = new ElderAnalysisDAL().GetWithPages(0, Int32.MaxValue, ids, DateTime.Now.AddDays(-15).ToString("yyyy - MM - dd").Trim(), DateTime.Now.AddDays(-1).ToString("yyyy - MM - dd").Trim(), "id", out count);
                    if (AnalysisEntity != null && AnalysisEntity.Any())
                    {
                        List<ElderAnalysisDto> data = new List<ElderAnalysisDto>();
                        AnalysisEntity.ForEach(item => data.Add(new ElderAnalysisDto
                        {
                            AnalysisDate = item.AnalysisDate,
                            DepthSeepLong = item.DepthSeepLong,
                            ElderID = item.ElderID,
                            FallAsleepTime = item.FallAsleepTime,
                            GetUpTime = item.GetUpTime.ToString(),
                            Id = item.Id,
                            InBedTime = item.InBedTime.ToString(),
                            NightCount = item.NightCount,
                            ShallowSleepLong = item.ShallowSeepLong,
                            SleepLong = item.SeepLong,
                            SoberCount = item.SoberCount,
                            SoberSleepLong = item.SoberSeepLong,
                            SoberTime = item.SoberTime
                        }));
                        res.data = data;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return res;
        }

        /// <summary>
        /// 历史分析数据
        /// </summary>
        /// <param name="f_code"></param>
        /// <param name="elderID"></param>
        /// <param name="s_data"></param>
        /// <param name="e_data"></param>
        /// <returns></returns>
        public ResultDto<ElderAnalysisDto> HistoryAnalysis(string f_code, string elderName, DateTime s_data, DateTime e_data)
        {
            ResultDto<ElderAnalysisDto> res = new ResultDto<ElderAnalysisDto>();

            try
            {
                int count = 0;
                var elderentity = new ElderDAL().Query(f_code, elderName);
                if (elderentity != null && elderentity.Any())
                {
                    List<int> ids = elderentity.Select(p => p.Id).ToList();
                    var AnalysisEntity = new ElderAnalysisDAL().GetWithPages(0, Int32.MaxValue, ids, s_data.ToString(), e_data.ToString(), "id", out count);
                    if (AnalysisEntity != null && AnalysisEntity.Any())
                    {
                        List<ElderAnalysisDto> data = new List<ElderAnalysisDto>();
                        AnalysisEntity.ForEach(item => data.Add(new ElderAnalysisDto
                        {
                            AnalysisDate = item.AnalysisDate,
                            DepthSeepLong = item.DepthSeepLong,
                            ElderID = item.ElderID,
                            FallAsleepTime = item.FallAsleepTime,
                            GetUpTime = item.GetUpTime.ToString(),
                            Id = item.Id,
                            InBedTime = item.InBedTime.ToString(),
                            NightCount = item.NightCount,
                            ShallowSleepLong = item.ShallowSeepLong,
                            SleepLong = item.SeepLong,
                            SoberCount = item.SoberCount,
                            SoberSleepLong = item.SoberSeepLong,
                            SoberTime = item.SoberTime
                        }));
                        res.data = data;
                    }
                }

            }
            catch (Exception ex)
            {


            }



            return res;
        }


        #endregion

    }
}
