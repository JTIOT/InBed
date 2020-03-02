using InBed.Service.Abstracts;
using InBed.Service.Impl;
using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.QuartzJob
{
    public class ElderAnalysisJob : IJob
    {
        protected readonly ILog _logger = LogManager.GetLogger(typeof(ElderAnalysisJob));
        public void Execute(IJobExecutionContext context)
        {
            try {
                ElderService analysis = new ElderService();
                analysis.ElderAnalysis();
                _logger.InfoFormat("ProductSaleStatistic执行成功");
            }
            catch (Exception ex)
            {

                _logger.InfoFormat("ProductSaleStatistic执行异常:" + ex.Message);
            }
        }
    }
}
