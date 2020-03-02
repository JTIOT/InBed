using Autofac;
using Autofac.Extras.Quartz;
using Autofac.Integration.Mvc;
using Quartz;
using Quartz.Impl;
using System;

namespace InBed.Web.Jobs
{
    public class EmailJobScheduler
    {
        public void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

            var scoppe = AutofacDependencyResolver.Current.RequestLifetimeScope.Resolve<AutofacJobFactory>();
            scheduler.JobFactory = scoppe;

            scheduler.Start();
            //每天8点15分执行分析统计
            IJobDetail job = JobBuilder.Create<IJob>().Build();
            ITrigger trigger = TriggerBuilder.Create()
               .WithCronSchedule("0 15 13 ? * *")       
                .Build();
            scheduler.ScheduleJob(job, trigger);
        }
    }
    public class ElderAnalysisJob
    {
        public void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

            var scoppe = AutofacDependencyResolver.Current.RequestLifetimeScope.Resolve<AutofacJobFactory>();
            scheduler.JobFactory = scoppe;

            scheduler.Start();
            IJobDetail job = JobBuilder.Create<IJob>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("triggerName", "groupName")
                .WithSimpleSchedule(t =>
                    t.WithIntervalInSeconds(60)
                        .RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}