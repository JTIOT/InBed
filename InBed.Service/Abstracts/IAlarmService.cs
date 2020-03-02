using InBed.Service.Dto;
using InBed.Service.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Abstracts
{
    public partial interface IAlarmService
    {
        /// <summary>
        /// 获取未处理的告警信息
        /// </summary>
        /// <param name="f_code"></param>
        /// <returns></returns>
        ResultDto<AlarmDto> GetAlarm(string f_code);
        /// <summary>
        /// 处理告警
        /// </summary>
        /// <param name="alarmID">告警id</param>
        /// <param name="mag">处理方式</param>
        /// <returns></returns>
        Result<AlarmDto> HandleAlarm(int alarmID,int modifier, string mag);
        /// <summary>
        /// 获取心率告警数据
        /// </summary>
        /// <param name="f_code"></param>
        /// <param name="s_time"></param>
        /// <param name="e_time"></param>
        /// <returns></returns>
        Result<TypeAlarmDto> GetAlarmHR(string f_code, int elderid, DateTime s_time, DateTime e_time);
        /// <summary>
        /// 获取老人告警历史数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ResultDto<AlarmDto> GetWithPages(AlarmRequest request);
        /// <summary>
        /// 获取离床提醒
        /// </summary>
        /// <param name="f_code"></param>
        /// <returns></returns>
        ResultDto<MessageDto> GetMessage(string f_code);
    }
}
