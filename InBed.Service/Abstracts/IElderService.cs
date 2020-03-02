using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using InBed.Service.Dto;
using InBed.Service.Request;
using InBed.Service.DTO;

namespace InBed.Service.Abstracts
{
    public partial interface IElderService
    {

            /// <summary>
            /// 分页查询
            /// </summary>
            /// <param name="request"></param>
            /// <returns></returns>
            ResultDto<ElderDto> GetWithPages(ElderRequest request);
            /// <summary>
            /// 添加老人信息
            /// </summary>
            /// <param name="model"></param>
            /// <returns></returns>
            Result<ElderDto> AddElder(ElderDto model);
            /// <summary>
            /// 修改老人信息
            /// </summary>
            /// <param name="model"></param>
            /// <returns></returns>
            Result<ElderDto> EditElder(ElderDto model);
            /// <summary>
            /// 删除单个老人信息
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            Result<ElderDto> DelElder(int id);
            /// <summary>
            /// 删除多个老人信息
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            Result<ElderDto> DelElder(List<int> id);
            /// <summary>
            /// 获取老人详细信息
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            ElderDto GetDeteail(int id);
            ElderDto[] GetDetails();
        /// <summary>
        /// 获取系统总人数及当日增长人数
        /// </summary>
        /// <param name="facilitator"></param>
        /// <returns></returns>
        ElderCountDto GetElderCount(string facilitator);
            /// <summary>
            /// 年龄段统计
            /// </summary>
            /// <param name="facilitator"></param>
            /// <returns></returns>
            List<AgeGroupDto> AgeGroup(string facilitator);
            /// <summary>
            /// 获取老人地图位置
            /// </summary>
            /// <param name="facilitator"></param>
            /// <returns></returns>
            List<MapPositionDto> GetMapPosition(string facilitator,string province);

        /// <summary>
        /// 获取老人统计
        /// </summary>
        /// <param name="facilitator"></param>
        /// <param name="elderID"></param>
        /// <returns></returns>
        Result<ElderStatisticsDto> GetElderStatistics(string facilitator, int elderID, string customerID);
            /// <summary>
            /// 获取服务商下的人数
            /// </summary>
            /// <param name="facilitator"></param>
            /// <param name="elderID"></param>
            /// <returns></returns>
        List<ProvinceNumberDto> GetPlaceNameeNumber(string facilitator);
        /// <summary>
        /// 获取在线数量与总人数
        /// </summary>
        /// <param name="facilitator"></param>
        /// <returns></returns>
        List<ProvinceOnlineNumberDto> GetPlaceOnlineNumber(string facilitator);
        /// <summary>
        /// 老人信息统计
        /// </summary>
        /// <returns></returns>
        void ElderAnalysis();

            /// <summary>
            /// 加载老人信息
            /// </summary>
            /// <param name="request"></param>
            /// <returns></returns>
            ResultDto<ElderInfoDto> GetElderPages(ElderRequest request);

        /// <summary>
        /// 老人查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ResultDto<ElderInfoDto> ElderQuery(ElderRequest request);

        List<SleepDto> GetCurrentDaySleepingDate(string f_code,int elderid);
    }
}
