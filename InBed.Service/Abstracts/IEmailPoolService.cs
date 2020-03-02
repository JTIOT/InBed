using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InBed.Service.Dto;

namespace InBed.Service.Abstracts
{
    public partial interface IEmailPoolService
    {
        /// <summary>
        /// 获取指定数量的待发送邮件
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        List<EmailPoolDto> GetWithReceivers(int top);
    }
}
