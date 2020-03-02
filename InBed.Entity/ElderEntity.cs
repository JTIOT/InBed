using System.Collections.Generic;
using InBed.Entity.Base;
using Newtonsoft.Json;
using System;

namespace InBed.Entity
{
    public  class ElderEntity : BaseEntity
    {
        /// <summary>
        /// 服务商编号
        /// </summary>
        public string FacilitatorCode { get; set; }
        /// <summary>
        /// 服务商名称
        /// </summary>
        public string FacilitatorName { get; set; }
        /// <summary>
        /// 老人信息
        /// </summary>
        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public byte[] photo { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int sex { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string number { get; set; }
        /// <summary>
        /// 工作单位
        /// </summary>
        public string workunit { get; set; }
        /// <summary>
        /// 家庭电话
        /// </summary>
        public string homephone { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string contacts { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string c_telephone { get; set; }
        /// <summary>
        /// 常住类型
        /// </summary>
        public int resident_type { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public int nation { get; set; }
        /// <summary>
        /// 血型
        /// </summary>
        public int blood_type { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        public int education { get; set; }
        /// <summary>
        /// 职业
        /// </summary>
        public int occupation { get; set; }
        /// <summary>
        /// 婚姻状况
        /// </summary>
        public int marriageStatus { get; set; }
        /// <summary>
        /// 医疗费支付方式
        /// </summary>
        public int dispensedmode { get; set; }
        /// <summary>
        /// 过敏药物史
        /// </summary>
        public string H_allergydrug { get; set; }
        /// <summary>
        /// 疾病史
        /// </summary>
        public string H_disease { get; set; }
        /// <summary>
        /// 是否手术
        /// </summary>
        public int is_ops { get; set; }
        /// <summary>
        /// 手术病名
        /// </summary>
        public string ops_disease { get; set; }
        /// <summary>
        /// 手术时间
        /// </summary>
        public DateTime ops_data { get; set; }
        /// <summary>
        /// 是否有遗传病
        /// </summary>
        public int is_genetic { get; set; }
        /// <summary>
        /// 遗传病名
        /// </summary>
        public string genetic_disease { get; set; }
        /// <summary>
        /// 是否有残疾
        /// </summary>
        public int is_deformity { get; set; }
        /// <summary>
        /// 残疾证编号
        /// </summary>
        public string deformity_number { get; set; }
     
        /// <summary>
        /// 居住地址
        /// </summary>
        public string homeaddress { get; set; }
        /// <summary>
        /// 是否是公益老人
        /// </summary>
        public int IsWelfare { get; set; }
        public string MapPoint { get; set; }


    }
}
