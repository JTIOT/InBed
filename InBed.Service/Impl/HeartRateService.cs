using InBed.Core;
using InBed.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InBed.Service.Dto;
using InBed.Data.DAL;
using System.Diagnostics;
using InBed.Core.Extentions;

namespace InBed.Service.Impl
{
    public class HeartRateService : IDependency, IHeartRateService
    {
        /// <summary>
        /// 实时心率
        /// </summary>
        /// <param name="f_code"></param>
        /// <param name="elderID"></param>
        /// <returns></returns>
        public HeartRateDto CurrentHeartRate(string f_code, int elderID)
        {
            try
            {
                HeartRateDto res = new HeartRateDto();
                string StrReturn = string.Empty;
                var elderEntity = new ElderDAL().GetElderDeviceInfo(f_code, elderID);
                if (elderEntity != null)
                {
                    List<HeartRateData> resData = new List<HeartRateData>();
                    List<BreathingRate> BR = new List<BreathingRate>();
                    List<Pressure> PressureList = new List<Pressure>();
                    var realTimeEntity = new HearbeatStatusDAL().GetRealTime(ClassUtility.GetDeviceName(elderEntity.DeviceNumber));
                    if (realTimeEntity != null && realTimeEntity.Any())
                    {
                       
                        //根据老人id创建老人心率对象
                        foreach (var item in realTimeEntity)
                        {
                            if (ObjClass.HeartRateMap.ContainsKey(elderEntity.ElderId))
                            { 
                                    ObjClass.HeartRateMap[elderEntity.ElderId].SetData(item.ID, item.HeartbeatRate, item.ReceiveTime.ToString());                                                              
                            }
                            else
                            {
                                ElderHeartRate itemElder = new ElderHeartRate(elderEntity.ElderId, item.HeartbeatRate, item.ReceiveTime.ToString());
                                ObjClass.HeartRateMap.Add(elderEntity.ElderId, itemElder);
                            }
                            //呼吸
                            if (ObjClass.BRMap.ContainsKey(elderEntity.ElderId))
                            {
                                ObjClass.BRMap[elderEntity.ElderId].SetData(item.ID, item.BreathingRate, item.ReceiveTime.ToString());
                            }
                            else {
                                ObjClass.BRMap.Add(elderEntity.ElderId, new ElderBR(item.ID, item.BreathingRate, item.ReceiveTime.ToString()));
                            }
                          
                        }
                        ObjClass.HeartRateMap[elderEntity.ElderId].ListHD.ForEach(item => resData.Add(new HeartRateData
                        {
                            time = item.time,
                            value = item.value
                        }));

                        ObjClass.BRMap[elderEntity.ElderId].ListBR.ForEach(item => BR.Add(new BreathingRate
                        {
                            time = item.time,
                            value = item.value
                        }));
                        var pressureTimeEntity = new HearbeatStatusDAL().GetPressureTime(ClassUtility.GetDeviceName(elderEntity.DeviceNumber));
                        if (pressureTimeEntity != null && pressureTimeEntity.Any())
                        {
                            foreach (var item in pressureTimeEntity)
                            {
                                //重压值
                                if (ObjClass.PressureMap.ContainsKey(elderEntity.ElderId))
                                {
                                    ObjClass.PressureMap[elderEntity.ElderId].SetData(item.ID, item.PressureValue, item.ReceiveTime.ToString());
                                }
                                else
                                {
                                    ObjClass.PressureMap.Add(elderEntity.ElderId, new ElderPressure(item.ID, item.PressureValue, item.ReceiveTime.ToString()));
                                }
                            }
                            ObjClass.PressureMap[elderEntity.ElderId].ListPressure.ForEach(item => PressureList.Add(new Pressure
                            {
                                time = item.time,
                                value = item.value
                            }));
                        }
                        ///告警设置
                        var setElderEntity = new ElderAlarmSetupDAL().Query(elderID);
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
                        res.ElderSetUP = setDto;
                        res.Data.AddRange(resData);
                        res.BRData.AddRange(BR);
                        res.PressureData.AddRange(PressureList);
                        ObjClass.HeartRateMap[elderEntity.ElderId].ListHD.Clear();
                        ObjClass.BRMap[elderEntity.ElderId].ListBR.Clear();
                        ObjClass.PressureMap[elderEntity.ElderId].ListPressure.Clear();
                    }
                }
                return res;
            }
            catch (Exception ex)
            {
                return new HeartRateDto();
            }
        }

        public HeartRateDto HistoryHeartRate(string f_code, string elderName, string s_data, string e_data)
        {
            HeartRateDto res = new HeartRateDto();
            try
            {
                var elderEentity = new ElderDAL().Query(f_code, elderName);
                if (elderEentity != null && elderEentity.Any())
                {

                    var itemEentity = elderEentity.FirstOrDefault();
                    var devEntity = new DeviceDAL().GetElderDevice(itemEentity.Id);
                    if (devEntity != null)
                    {
                        var hdEentity = new HearbeatStatusDAL().GetRealTime(ClassUtility.GetDeviceName(devEntity.device_number), DateTime.Parse(s_data), DateTime.Parse(e_data));
                        List<HeartRateData> HD = new List<HeartRateData>();
                        hdEentity.ForEach(P => HD.Add(new HeartRateData
                        {
                            time = P.ReceiveTime.ToString(),
                            value = P.HeartbeatRate.ToString()
                        }));

                        List<BreathingRate> BR = new List<BreathingRate>();
                        hdEentity.FindAll(p => p.BreathingRate != 0).ForEach(p => BR.Add(new BreathingRate
                        {
                            time = p.ReceiveTime.ToString(),
                            value = p.BreathingRate.ToString()
                        }));
                        var setElderEntity = new ElderAlarmSetupDAL().Query(itemEentity.Id);
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
                        res.ElderSetUP = setDto;
                        res.Data.AddRange(HD);
                        res.BRData.AddRange(BR);

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return res;
        }

        public List<BreathingRate> HistoryBreathing(string f_code, string elderName, string s_data, string e_data)
        {
            List<BreathingRate> res = new List<BreathingRate>();
            try
            {
                var elderEentity = new ElderDAL().Query(f_code, elderName);
                if (elderEentity != null && elderEentity.Any())
                {

                    var itemEentity = elderEentity.FirstOrDefault();
                    var devEntity = new DeviceDAL().GetElderDevice(itemEentity.Id);
                    if (devEntity != null)
                    {
                        var brEentity = new HearbeatStatusDAL().GetBreathingTime(ClassUtility.GetDeviceName(devEntity.device_number), DateTime.Parse(s_data), DateTime.Parse(e_data));
                        brEentity.ForEach(P => res.Add(new BreathingRate
                        {
                            time = P.ReceiveTime.ToString(),
                            value = P.BreathingRate.ToString()
                        }));     
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return res;
        }

        public List<Pressure> HistoryWeight(string f_code, string elderName, string s_data, string e_data)
        {
            List<Pressure> res = new List<Pressure>();
            try
            {
                var elderEentity = new ElderDAL().Query(f_code, elderName);
                if (elderEentity != null && elderEentity.Any())
                {

                    var itemEentity = elderEentity.FirstOrDefault();
                    var devEntity = new DeviceDAL().GetElderDevice(itemEentity.Id);
                    if (devEntity != null)
                    {
                        var pressureEentity = new HearbeatStatusDAL().GetPressureTime(ClassUtility.GetDeviceName(devEntity.device_number), DateTime.Parse(s_data), DateTime.Parse(e_data));
                        pressureEentity.ForEach(P => res.Add(new Pressure
                        {
                             id=P.ID,
                            time = P.ReceiveTime.ToString(),
                            value = P.PressureValue.ToString()
                        }));
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
