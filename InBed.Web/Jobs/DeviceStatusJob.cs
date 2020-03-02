using System;
using System.Linq;
using InBed.Core.Log;
using InBed.Service.Abstracts;
using InBed.Service.Config;
using InBed.Service.Enum;
using Quartz;

namespace InBed.Web.Jobs
{
    public class DeviceStatusJob : IJob
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