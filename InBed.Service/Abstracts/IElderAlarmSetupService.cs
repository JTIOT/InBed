using InBed.Service.Dto;
using InBed.Service.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Abstracts
{
    public partial interface IElderAlarmSetupService
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ResultDto<AlarmSetupDto> GetWithPages(AlarmSetupRequest request);
        Result<AlarmSetupDto> EditAlarmSetup(AlarmSetupDto model);
        /// <summary>
        /// 删除单个告警设置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<AlarmSetupDto> DelAlarmSetup(int id);
        /// <summary>
        /// 删除多个告警设置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<AlarmSetupDto> DelAlarmSetup(List<int> id);
        /// <summary>
        /// 获取告警详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AlarmSetupDto GetDeteail(int id);
    }
}
