using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using InBed.Service.Dto;
using InBed.Entity;

namespace InBed.Service.Abstracts
{
    public partial interface IOnBedRecordService
    {
        ResultDto<ElderAnalysisDto> HalfMonthOnBedRecord(string f_code, int elderID);
        /// <summary>
        /// 历史分析数据
        /// </summary>
        /// <param name="f_code"></param>
        /// <param name="elderName"></param>
        /// <param name="s_data"></param>
        /// <param name="e_data"></param>
        /// <returns></returns>
        ResultDto<ElderAnalysisDto> HistoryAnalysis(string f_code, string elderName, DateTime s_data, DateTime e_data);
    }
}
