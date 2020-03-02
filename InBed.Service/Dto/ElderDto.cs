using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using InBed.Service.Enum;
using System;
using InBed.Core;

namespace InBed.Service.Dto
{
    public class ElderDto : BaseDto
    {
        [DisplayName("服务商编号*"), Required, StringLength(20, MinimumLength = 5, ErrorMessage = "长度在5-20个字符之间")]
        public string FacilitatorCode { get; set; }
        [DisplayName("服务商名称*"), Required, StringLength(20, MinimumLength = 5, ErrorMessage = "长度在5-20个字符之间")]
        public string FacilitatorName { get; set; }
        [DisplayName("姓名*"), Required, StringLength(20, MinimumLength = 5, ErrorMessage = "长度在2-10个字符之间")]
        public string name { get; set; }
        [DisplayName("照片*")]
        public byte[] photo { get; set; }
        [DisplayName("性别*")]
        public SexEnum sex { get; set; }
         [DisplayName("出生日期*")]
        public string birthday { get; set; }
        [DisplayName("身份证号*")]
        public string number { get; set; }
        [DisplayName("工作单位*")]
        public string workunit { get; set; }
        [DisplayName("家庭电话*")]
        public string homephone { get; set; }
        [DisplayName("联系人*")]
        public string contacts { get; set; }
        [DisplayName("联系人电话*")]
        public string c_telephone { get; set; }
        [DisplayName("常住类型*")]
        public int resident_type { get; set; }
        [DisplayName("民族*")]
        public int nation { get; set; }

        [DisplayName("血型*")]
        public int blood_type { get; set; }
        [DisplayName("学历*")]
        public int education { get; set; }
        [DisplayName("职业*")]
        public int occupation { get; set; }
        [DisplayName("婚姻状况*")]
        public int marriageStatus { get; set; }
        [DisplayName("医疗费支付方式*")]
        public int dispensedmode { get; set; }
        [DisplayName("过敏药物史*")]
        public string H_allergydrug { get; set; }
        [DisplayName("疾病史*")]
        public string H_disease { get; set; }
        [DisplayName("是否手术过*")]
        public int is_ops { get; set; }
        [DisplayName("手术病名*")]
        public string ops_disease { get; set; }
        [DisplayName("手术时间*")]
        public DateTime ops_data { get; set; }
        [DisplayName("是否有遗传病*")]
        public int is_genetic { get; set; }
        [DisplayName("遗传病名*")]
        public string genetic_disease { get; set; }
        [DisplayName("是否有残疾")]
        public int is_deformity { get; set; }
        [DisplayName("残疾证编号")]
        public string deformity_number { get; set; }
        [DisplayName("家庭地址*")]
        public string homeaddress { get; set; }
        [DisplayName("是否公益老人*")]
        public int IsWelfare { get; set; }

        public string MapPoint { get; set; }

        //年龄
        public int Age
        {
            get { return ClassUtility.CalculateAge(DateTime.Parse(birthday), DateTime.Now); }
        }

       
        public string sexName
        {
            get { return sex.ToString(); }
        }
    }
}
