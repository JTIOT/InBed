using InBed.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InBed.Service.Dto;
using InBed.Service.Request;
using InBed.Entity;
using AutoMapper;
using InBed.Core.Extentions;
using InBed.Data.DAL;
using InBed.Service.Enum;
using InBed.Core;

namespace InBed.Service.Impl
{
    public class ElderAlarmSetupService : IDependency,IElderAlarmSetupService
    {
        

        public Result<AlarmSetupDto> DelAlarmSetup(List<int> id)
        {
            Result<AlarmSetupDto> res = new Result<AlarmSetupDto>();
            try
            {
                if (new ElderAlarmSetupDAL().DelElderAlarmSetup(id))
                {
                    res.flag = true;
                    res.msg = "老人告警设置删除成功！";
                }
                else
                {
                    res.flag = false;
                    res.msg = "老人告警设置删除成功！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public Result<AlarmSetupDto> DelAlarmSetup(int id)
        {
            Result<AlarmSetupDto> res = new Result<AlarmSetupDto>();
            try
            {
                if (new ElderAlarmSetupDAL().DelElderAlarmSetup(id))
                {
                    res.flag = true;
                    res.msg = "老人告警设置删除成功！";
                }
                else
                {
                    res.flag = false;
                    res.msg = "老人告警设置删除成功！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public Result<AlarmSetupDto> EditAlarmSetup(AlarmSetupDto model)
        {
            Result<AlarmSetupDto> res = new Result<AlarmSetupDto>();
            try
            {
                ElderAlarmSetupDAL AlarmSetupDAL = new ElderAlarmSetupDAL();
                var oldentity = AlarmSetupDAL.Details(model.Id);
                oldentity.maxbreath = model.maxbreath;
                oldentity.maxheart = model.maxheart;
                oldentity.minbreath = model.minbreath;
                oldentity.minheart = model.minheart;
                oldentity.inbedtime = model.inbedtime;
                oldentity.leavebedtime = model.leavebedtime;
                oldentity.pausebreath = model.pausebreath;
                oldentity.set_type = (int)model.set_type;
                oldentity.e_time = model.e_time;
                oldentity.s_time = model.s_time;
                var flag = new ElderAlarmSetupDAL().EditElderAlarmSetup(oldentity);
                if (flag)
                {
                    res.flag = true;
                    res.msg = "老人告警设置信息修改成功！";
                }
                else
                {
                    res.flag = false;
                    res.msg = "老人告警设置信息修改失败！";
                }
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = ex.Message;
            }
            return res;
        }

        public AlarmSetupDto GetDeteail(int id)
        {
            try
            {
                List<ElderAlarmSetupEntity> AlarmSetupEntity = new List<ElderAlarmSetupEntity>();
                AlarmSetupEntity.Add(new ElderAlarmSetupDAL().Details(id));
                List<AlarmSetupDto> date = getDto(AlarmSetupEntity);
                if (date != null && date.Any())
                {
                    AlarmSetupDto item = date.FirstOrDefault();
                    var elderEntit = new ElderDAL().Details(item.ElderId);
                    if (elderEntit != null)
                    {
                        item.FacilitatorCode = elderEntit.FacilitatorCode;
                        item.FacilitatorName = elderEntit.FacilitatorName;
                        item.ElderName = elderEntit.name;
                        item.birthday = elderEntit.birthday;
                    }
                    return item;
                }
                else
                {
                    return new AlarmSetupDto();
                }
                
            }
            catch (Exception ex)
            {
                return new AlarmSetupDto();
            }
        }

        public ResultDto<AlarmSetupDto> GetWithPages(AlarmSetupRequest request)
        {
            ResultDto<AlarmSetupDto> res = new ResultDto<AlarmSetupDto>();
            try
            {
                var elderEntit = new ElderDAL().Query(request.FacilitatorCode, request.ElderName, request.ElderPhone);
                if (elderEntit != null && elderEntit.Any())
                {
                    var ids = elderEntit.Select(item => item.Id).ToList();
                    int count = 0;
                    var entity = new ElderAlarmSetupDAL().GetWithPages(request.Start, request.Length, ids, request.OrderBy, out count, request.OrderDir);
                    res.recordsTotal = count;
                    List<AlarmSetupDto> date= getDto(entity);
                    foreach (var item in date)
                    {
                        var obj = elderEntit.Find(p => p.Id == item.ElderId);
                        if (obj != null)
                        {
                            item.FacilitatorCode = obj.FacilitatorCode;
                            item.FacilitatorName = obj.FacilitatorName;
                            item.ElderName = obj.name;
                            item.birthday = obj.birthday;
                        }
                    }
                    res.data = date;
                }
            }
            catch (Exception ex)
            {
                res.recordsTotal = 0;
                res.data = new List<AlarmSetupDto>();
            }
            return res;
        }
        public List<AlarmSetupDto> getDto(List<ElderAlarmSetupEntity> entity)
        {
            List<AlarmSetupDto> res = new List<AlarmSetupDto>();
            entity.ForEach(item => res.Add(new AlarmSetupDto
            {
                Id = item.Id,
                ElderId = (int)item.Elder_id,
                maxbreath = item.maxbreath,
                maxheart = item.maxheart,
                minbreath = item.minbreath,
                minheart = item.minheart,
                inbedtime = item.inbedtime,
                leavebedtime = item.leavebedtime,
                pausebreath = item.pausebreath,
                set_type = (SetType)item.set_type,
                 e_time=item.e_time,
                  s_time=item.s_time
                
            }));
            return res;
        }

    }
}
