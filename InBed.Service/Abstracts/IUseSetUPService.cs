using InBed.Service.Dto;
using InBed.Service.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Abstracts
{
    public partial interface IUseSetUPService
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ResultDto<UseSetUPDto> GetWithPages(UseSetUPRequest request);
        /// <summary>
        /// 添加老人使用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Result<SystemElderSetupDto> AddUseSet(SystemElderSetupDto model);
        /// <summary>
        /// 修改老人使用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Result<SystemElderSetupDto> EditUseSet(SystemElderSetupDto model);
        /// <summary>
        /// 详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SystemElderSetupDto GetDtail(int id);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Result<SystemElderSetupDto> DelUseSet(List<int> ids);
    }
}
