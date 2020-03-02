using InBed.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Dto
{
    public class UseSetUPDto: BaseDto
    {

        /// <summary>
        /// 老人ID
        /// </summary>
        public int ElderID { get; set; }
        /// <summary>
        /// 老人姓名
        /// </summary>
        public string ElderName { get; set; }
        /// <summary>
        /// 老人电话
        /// </summary>
        public string ElderPhone { get; set; }
        /// <summary>
        /// 系统使用开始时间
        /// </summary>
        public string start_data { get; set; }
        /// <summary>
        /// 系统停用时间
        /// </summary>
        public string end_data { get; set; }
       
        /// <summary>
        /// 出生日期
        /// </summary>
        public string birthday { get; set; }
        //年龄
        public int ElderAge
        {
            get { return ClassUtility.CalculateAge(DateTime.Parse(birthday), DateTime.Now); }
        }
        /// <summary>
        /// 所剩天数
        /// </summary>
        public int SurplusCount
        {
            
            get {
                    var count = ClassUtility.ToEndTime(end_data).Subtract(DateTime.Now).TotalDays;
                    if (count > 0)
                        return Convert.ToInt32(count);                   
                    else
                        return 0;
                }
        }

        
        public int UseCount
        {

            get
            {
                var count = ClassUtility.ToEndTime(end_data).Subtract(ClassUtility.ToEndTime(start_data)).TotalDays;
                if (count > 0)
                    return Convert.ToInt32(count);
                else
                    return 0;
            }
        }


    }
}
