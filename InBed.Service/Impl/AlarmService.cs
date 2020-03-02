using InBed.Core;
using InBed.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InBed.Service.Dto;
using InBed.Data.DAL;
using InBed.Service.Enum;
using InBed.Service.Request;
using InBed.Core.Extentions;

namespace InBed.Service.Impl
{
    public class AlarmService : IDependency, IAlarmService
    {
        /// <summary>
        /// 获取服务商所有告警
        /// </summary>
        /// <param name="f_code"></param>
        /// <returns></returns>
        public ResultDto<AlarmDto> GetAlarm(string f_code)
        {
            ResultDto<AlarmDto> res = new ResultDto<AlarmDto>();
            try
            {
                var alarmEntity = new AlarmDAL().GetAlarm(f_code);
                List<AlarmDto> r_data = new List<AlarmDto>();
                alarmEntity.ForEach(item => r_data.Add(new AlarmDto
                {
                    ID = item.ID,
                    AlarmReason = item.AlarmReason,
                    AlarmTime = item.AlarmTime,
                    AlarmType = (AlarmType)item.AlarmType,
                    DeviceNumber = item.DeviceNumber,
                    OnLineTime = item.OnLineTime,
                    UploadTime = item.UploadTime,
                    ElderName = item.ElderName,
                     ElderID=item.ElderId
                }));
                res.data = r_data;
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        /// <summary>
        /// 处理告警
        /// </summary>
        /// <param name="alarmID"></param>
        /// <param name="modifier"></param>
        /// <param name="mag"></param>
        /// <returns></returns>
        public Result<AlarmDto> HandleAlarm(int alarmID, int modifier,  string mag)
        {
            Result<AlarmDto> res = new Result<AlarmDto>();
            try
            {
                AlarmDAL alarm = new AlarmDAL();
                var entity = alarm.GetDetail(alarmID);
                entity.HandleOpinions = mag;
                entity.IsHandle = 1;
                entity.Modifier = modifier;
                res.flag = alarm.HandleAlarm(entity);
                res.msg = res.flag ? "处理成功！" : "处理失败！";      
            }
            catch (Exception ex)
            {
                res.flag = false;
                res.msg = "处理失败！";
            }
            return res;
        }

        public Result<TypeAlarmDto> GetAlarmHR(string f_code,int elderid, DateTime s_time, DateTime e_time)
        {
            Result<TypeAlarmDto> res = new Result<TypeAlarmDto>();
            try
            {
                TypeAlarmDto dto = new TypeAlarmDto();
                ElderDAL elderObj = new ElderDAL();
                var elderEntity = elderObj.Query(f_code);
                var elder = elderEntity.Find(item => item.Id == elderid);
                if (elder != null)
                {
                    dto.elderID = elder.Id;
                    dto.elderName = elder.name;
                    dto.birthday = elder.birthday;
                    dto.contacts = elder.contacts;
                    dto.c_telephone = elder.c_telephone;
                    dto.homeaddress = elder.homeaddress;
                    dto.homephone = elder.homephone;
                    dto.sex = (SexEnum)elder.sex;
                    var alarmSetUP = new ElderAlarmSetupDAL().Query(elder.Id);
                    if(alarmSetUP!=null)
                    {
                        dto.alarmSetup = new AlarmSetupDto
                        {
                            inbedtime = alarmSetUP.inbedtime,
                            leavebedtime = alarmSetUP.leavebedtime,
                            maxbreath = alarmSetUP.maxbreath,
                            maxheart = alarmSetUP.maxheart,
                            minbreath = alarmSetUP.minbreath,
                            minheart = alarmSetUP.minheart,
                            pausebreath = alarmSetUP.pausebreath                            
                        };

                    }
                    var ElderDeviceInfo = elderObj.GetElderDeviceInfo(f_code, elder.Id);
                    var DeviceNumber = ElderDeviceInfo == null?"": ElderDeviceInfo.DeviceNumber;
                    var listData = new HearbeatStatusDAL().GetRealTime(ClassUtility.GetDeviceName(DeviceNumber), s_time, e_time);
                    List<AlarmData> data = new List<AlarmData>();
                    listData.ForEach(item => data.Add(new AlarmData {
                         value=item.HeartbeatRate.ToString(),
                          time=item.ReceiveTime.ToString()
                    }));

                    dto.data = data;


                }
            }
            catch (Exception ex)
            {

            }
            return res;


        }

        public ResultDto<AlarmDto> GetWithPages(AlarmRequest request)
        {
            ResultDto<AlarmDto> res = new ResultDto<AlarmDto>();
            try
            {
                int count = 0;
                var elderEntity = new ElderDAL().Query(request.FacilitatorCode, request.ElderName);
                if (elderEntity != null && elderEntity.Any())
                {
                    var itemEntity = elderEntity.FirstOrDefault();
                    var entity = new AlarmDAL().GetWithPages(request.Start, request.Length, itemEntity.Id, request.S_data, request.E_data, request.AlarmType, request.OrderBy, out count, request.OrderDir);
                    List<AlarmDto> listData = new List<AlarmDto>();
                    if (entity != null && entity.Any())
                    {
                        entity.ForEach(item => listData.Add(new AlarmDto
                        {
                            ElderName = itemEntity.name,
                            AlarmReason = item.AlarmReason,
                            AlarmTime = item.AlarmTime,
                            AlarmType = (AlarmType)item.AlarmType,
                            ID = item.ID,
                            HandleOpinions = item.HandleOpinions,
                            DeviceNumber = item.DeviceNumber,
                            IsHandle = (StrsHandle)item.IsHandle,
                            Modifier = item.Modifier,
                            OnLineTime = item.OnLineTime,
                            UploadTime = item.UploadTime
                        }));
                    }
                    res.recordsTotal = count;
                    res.data = listData;
                }  
            }
            catch (Exception ex)
            {
                res.recordsTotal = 0;
                res.data = new List<AlarmDto>();
            }
            return res;
        }

        public ResultDto<MessageDto> GetMessage(string f_code)
        {
            ResultDto<MessageDto> res = new ResultDto<MessageDto>();
            List<MessageDto> MessageList = new List<MessageDto>();
            try
            {
                var elderEntity = new ElderDAL().Query(f_code);
                if (elderEntity != null && elderEntity.Any())
                {
                    var ids = elderEntity.Select(item => item.Id).ToList();
                    int count = 0;
                    var bindingEntity = new DeviceBindingDAL().Query(ids);
                    var alarmSetupentity = new ElderAlarmSetupDAL().GetWithPages(0, int.MaxValue, ids, "id", out count);
                    var devids = bindingEntity.Select(item => item.DeviceId).ToList();
                    var devstataEntity = new DeviceStateDAL().Query(devids);
                    if (bindingEntity != null)
                    {
                        foreach (var item in bindingEntity)
                        {
                            var tempSetupentity = alarmSetupentity.Find(p => p.Elder_id == item.ElderId);
                            if (tempSetupentity != null)
                            {
                                if (StringExtension.IsBlank(tempSetupentity.s_time) || StringExtension.IsBlank(tempSetupentity.e_time))
                                {
                                    continue;
                                }
                                var tempStataEntity = devstataEntity.Find(p => p.DevID == item.DeviceId.ToString());
                                if (tempStataEntity!=null)
                                {
                                    if (tempStataEntity.IsBed == 0 && tempStataEntity.InBedTime < DateTime.Now.AddMinutes(-tempSetupentity.leavebedtime))
                                    {

                                        string s_time = DateTime.Now.ToString("yyyy-MM-dd") + " " + tempSetupentity.s_time;
                                        string e_time = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " " + tempSetupentity.e_time;
                                        var elderitem = elderEntity.Find(p => p.Id == tempSetupentity.Elder_id);
                                        if (tempStataEntity.InBedTime > DateTime.Parse(s_time) && tempStataEntity.InBedTime < DateTime.Parse(e_time))
                                        {

                                            MessageList.Add(new MessageDto
                                            {
                                                ElderID = elderitem.Id,
                                                ElderName = elderitem.name,
                                                OutBedTime = tempStataEntity.InBedTime.ToString(),
                                                Message = elderitem.name + "老人：" + tempStataEntity.InBedTime.ToString() + " 离床超过正常时长，请注意！！！！"
                                            });
                                        }
                                        else if(tempStataEntity.InBedTime < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + tempSetupentity.e_time))
                                        {
                                            MessageList.Add(new MessageDto
                                            {
                                                ElderID = elderitem.Id,
                                                ElderName = elderitem.name,
                                                OutBedTime = tempStataEntity.InBedTime.ToString(),
                                                Message = elderitem.name + "老人：" + tempStataEntity.InBedTime.ToString() + " 离床超过正常时长，请注意！！！！"
                                            });
                                        }
                                    }
                                    
                                }
                            }
                        }
                    }
                }
                res.data = MessageList;
            }
            catch (Exception ex)
            {

            }
            return res;
        }
    }
}
