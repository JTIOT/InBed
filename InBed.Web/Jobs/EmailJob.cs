using System;
using System.Linq;
using InBed.Core.Log;
using InBed.Service.Abstracts;
using InBed.Service.Config;
using InBed.Service.Enum;
using Quartz;

namespace InBed.Web.Jobs
{
    /// <summary>
    /// 邮件发送Job
    /// </summary>
    public class EmailJob : IJob
    {
        public IElderService elderService { get; set; }



        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            elderService.ElderAnalysis();
        }
    }
}