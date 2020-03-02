using InBed.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Abstracts
{
    public partial interface IHeartRateService
    {
        /// <summary>
        /// 实时心率
        /// </summary>
        /// <param name="f_code"></param>
        /// <param name="elderID"></param>
        /// <returns></returns>
        HeartRateDto CurrentHeartRate(string f_code, int elderID);
        /// <summary>
        /// 历史心率
        /// </summary>
        /// <param name="f_code"></param>
        /// <param name="elderID"></param>
        /// <returns></returns>
        HeartRateDto HistoryHeartRate(string f_code, string ElderName, string s_data,string e_data);

        /// <summary>
        /// 历史呼吸
        /// </summary>
        /// <param name="f_code"></param>
        /// <param name="elderID"></param>
        /// <returns></returns>
       List<BreathingRate> HistoryBreathing(string f_code, string ElderName, string s_data, string e_data);
        /// <summary>
        /// 历史重压值
        /// </summary>
        /// <param name="f_code"></param>
        /// <param name="ElderName"></param>
        /// <param name="s_data"></param>
        /// <param name="e_data"></param>
        /// <returns></returns>

        List<Pressure> HistoryWeight(string f_code, string ElderName, string s_data, string e_data);

        

    }
}
