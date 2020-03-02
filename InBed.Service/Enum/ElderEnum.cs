using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Enum
{
    /// <summary>
    /// 学历
    /// </summary>
    public enum EducationEnum
    {
        /// <summary>
        /// 文盲（没上过学）
        /// </summary>
        illiteracy = 0,
        /// <summary>
        /// 小学
        /// </summary>
        Small = 1,
        /// <summary>
        /// 中学
        /// </summary>
        Middle = 2,
        /// <summary>
        /// 大学
        /// </summary>
        University = 3,
        /// <summary>
        /// 不详
        /// </summary>
        Unknown = 4


    }
    /// <summary>
    /// 婚姻状况
    /// </summary>
    public enum MarriageStatus
    {
        /// <summary>
        /// 未婚
        /// </summary>
        unmarried=0,
        /// <summary>
        /// 已婚
        /// </summary>

        married=1,
        /// <summary>
        /// 离婚
        /// </summary>
        divorce=2,
        /// <summary>
        /// 分居
        /// </summary>
        separation = 3,
        /// <summary>
        /// 丧偶
        /// </summary>
        Widowed =4
    }
    /// <summary>
    /// 付款类型
    /// </summary>
    public enum HospitalPaymentEnum
    {
        /// <summary>
        /// 全部公费
        /// </summary>
        allPublicWhole = 1,
        /// <summary>
        /// 部份公费
        /// </summary>
        partPublicWhole=2,
        /// <summary>
        /// 职工医疗保险
        /// </summary>
        workerShealth=3,
        /// <summary>
        /// 居民医疗保险
        /// </summary>
        residentShealth = 4,
        /// <summary>
        /// 商业医疗保险
        /// </summary>
        businessShealth = 5,
        /// <summary>
        /// 农村医疗保险
        /// </summary>
        countrysideShealth = 6,
        /// <summary>
        /// 贫困救助
        /// </summary>
        povertyRelief = 7,
        /// <summary>
        /// 自负医疗费
        /// </summary>
        selfPayment = 8,

    }
    /// <summary>
    /// 血型
    /// </summary>
    public enum BloodType
    {
        A=1,
        B=2,
        O=3,
        AB=4,
        RH=5
    }
    /// <summary>
    /// 职业
    /// </summary>
    public enum OccupationEnum
    {
        /// <summary>
        /// 工人
        /// </summary>
        Worker = 1,
        /// <summary>
        /// 企业技术员
        /// </summary>
        Retirees = 2,
        /// <summary>
        /// 企业管理者
        /// </summary>
        Administrator = 3,
        /// <summary>
        /// 军人
        /// </summary>
        Soldier = 4,
        /// <summary>
        /// 企业家
        /// </summary>
        Entrepreneur = 5,
        /// <summary>
        /// 学生
        /// </summary>
        Student=6,
        /// <summary>
        /// 服务人员
        /// </summary>
        Waiter=7
    }
    /// <summary>
    /// 残疾
    /// </summary>
    public enum IncompleteEnum
    {
        NoIncomplete=0,
        /// <summary>
        /// 听力
        /// </summary>
        hearing=1,
        /// <summary>
        /// 言语
        /// </summary>
        speech=2,
        /// <summary>
        /// 肢体
        /// </summary>
        Limbs=3,
        /// <summary>
        /// 智力
        /// </summary>
        intelligence=4,
        /// <summary>
        /// 眼
        /// </summary>
        eye=5,
        /// <summary>
        /// 精神
        /// </summary>
        spirit=6



    }
    /// <summary>
    /// 性别
    /// </summary>
    public enum SexEnum
    {
        男=1,
        女=0

    }

}
