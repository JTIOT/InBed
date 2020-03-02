using InBed.Core;
using InBed.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InBed.Service.Dto;
using InBed.Service.Request;
using InBed.Data.DAL;

namespace InBed.Service.Impl
{
    public class ElderAnalysisService : IDependency, IElderAnalysisService
    {
        public ResultDto<ElderAnalysisDto> GetWithPages(ElderAnalysisRequest request)
        {
            ResultDto<ElderAnalysisDto> res = new ResultDto<ElderAnalysisDto>();
            try
            {
                int count = 0;
                List<int> ids = new List<int>();
                if (request.ElderID != 0)
                {
                    ids.Add(request.ElderID);
                }
                else
                {
                    var elderEntity = new ElderDAL().Query(request.FacilitatorCode);
                    ids.AddRange(elderEntity.Select(item => item.Id

));
                }
                var entity = new ElderAnalysisDAL().GetWithPages(request.Start, request.Length, ids,
                                                                        request.StartTime, request.EndTime, request.OrderBy, out count, request.OrderDir);
                List<ElderAnalysisDto> data = new List<ElderAnalysisDto>();

                entity.ForEach(p => data.Add(new ElderAnalysisDto
                {
                    AnalysisDate = p.AnalysisDate,
                    ElderID = p.ElderID,
                    DepthSeepLong = p.DepthSeepLong,
                    FallAsleepTime = p.FallAsleepTime,
                    GetUpTime = p.GetUpTime.ToString(),
                    InBedTime = p.InBedTime.ToString(),
                    NightCount = p.NightCount,
                    ShallowSleepLong = p.ShallowSeepLong,
                    SleepLong = p.SeepLong,
                    SoberCount = p.SoberCount,
                    SoberSleepLong = p.SoberSeepLong,
                    SoberTime = p.SoberTime,
                    Id = p.Id
                }));


                res.recordsTotal = count;
                res.data = data;
            }
            catch (Exception ex)
            {
                res.recordsTotal = 0;
                res.data = new List<ElderAnalysisDto>();
            }
            return res;
        }

        public ResultDto<ElderAnalysisDto> ElderAnalysis(string f_code, string elderName, string S_data, string E_data)
        {
            ResultDto<ElderAnalysisDto> res = new ResultDto<ElderAnalysisDto>();
            try
            {
                var elderEntity = new ElderDAL().Query(f_code, elderName);
                if (elderEntity != null)
                {
                    int count = 0;
                    List<int> ids = new List<int>();
                    ids.Add(elderEntity.FirstOrDefault().Id);
                    var AnalysisEntity = new ElderAnalysisDAL().GetWithPages(0, Int32.MaxValue, ids, S_data.Trim(), E_data.Trim(), "id", out count);
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
                            Id = item.Id

,
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
    }
}
