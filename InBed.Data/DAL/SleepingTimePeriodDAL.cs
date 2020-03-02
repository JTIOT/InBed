using InBed.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Data.DAL
{
   public class SleepingTimePeriodDAL
    {
        #region 常量参数
        private readonly string columnName = @"id,deviceID,BeginTime,StopTime,BeginKeyIndex,StopKeyIndex,MaxHB,MinHB,MaxHBKeyIndex,MinHBKeyIndex,MaxHBTime,MinHBTime,SleepType";
        private readonly string tableName = "SleepingTimePeriod";
        private string strWhere = "1=1";
        #endregion
        public bool AddSleepingTimePeriod(SleepingTimePeriodEntity entity)
        {
            string strSQL = @"insert into SleepingTimePeriod(id,deviceID,BeginTime,StopTime,BeginKeyIndex,StopKeyIndex,MaxHB,MinHB,MaxHBKeyIndex,MinHBKeyIndex,MaxHBTime,MinHBTime,SleepType)
                                            values(@id,@deviceID,@BeginTime,@StopTime,@BeginKeyIndex,@StopKeyIndex,@MaxHB,@MinHB,@MaxHBKeyIndex,@MinHBKeyIndex,@MaxHBTime,@MinHBTime,@SleepType)";
            return DbHelper<SleepingTimePeriodEntity>.GetInstance().Insert(strSQL, entity) > 0;
        }
        /// <summary>
        /// 删除多条用户信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DelSleepingTimePeriod(List<int> ids)
        {
            strWhere = "id in (@id)";
            var dy = new DynamicParameters();
            dy.Add("id", string.Join(",", ids));
            return DbHelper<SleepingTimePeriodEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        /// <summary>
        /// 删除单条用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelSleepingTimePeriod(int id)
        {
            strWhere = "id =@id";
            var dy = new DynamicParameters();
            dy.Add("id", id);
            return DbHelper<SleepingTimePeriodEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        public List<WeekSleepingEntity> GetWeekSleepingDate(int elderID)
        {
            string strSQL = @"select ProductID,CONVERT(nvarchar(10),BeginTime,20) as Data,BeginTime,SleepType from [dbo].[SleepingTimePeriod] where ProductID=@ProductID AND  BeginTime between CONVERT(nvarchar(16),dateadd(day,-7, CONVERT (nvarchar(12),GETDATE(),112))+'12:00',20) 
and CONVERT(nvarchar(16),CONVERT(nvarchar(12),GETDATE(),20)+'12:00',20)
order by id ASC";
            var dy = new DynamicParameters();
            dy.Add("ProductID", elderID);
            return DbHelper<WeekSleepingEntity>.GetInstance().List(strSQL, dy);
        }
        public List<DaySleepingEntity> GetDaySleepingDate()
        {
            string strSQL = @"select ProductID,CONVERT(nvarchar(10),BeginTime,20) as Data,BeginTime,StopTime,SleepType from [dbo].[SleepingTimePeriod] where BeginTime between CONVERT(nvarchar(16),dateadd(day,-1, CONVERT (nvarchar(12),GETDATE(),112))+'12:00',20) 
                        and CONVERT(nvarchar(16),CONVERT(nvarchar(12),GETDATE(),20)+'12:00',20)
                        order by id ASC";
            return DbHelper<DaySleepingEntity>.GetInstance().List(strSQL, new DynamicParameters());
        }
        /// <summary>
        /// 获取前一天中午12点后到现在的睡眠状态数据
        /// </summary>
        /// <param name="elderID"></param>
        /// <returns></returns>
        public List<DaySleepingEntity> GetCurrentDaySleepingDate(int elderID)
        {
            string strSQL = @"select ProductID,CONVERT(nvarchar(10),BeginTime,20) as Data,BeginTime,StopTime,SleepType from [dbo].[SleepingTimePeriod] where BeginTime between CONVERT(nvarchar(16),dateadd(day,-1, CONVERT (nvarchar(12),GETDATE(),112))+'12:00',20) 
                        and CONVERT(nvarchar(20),GETDATE(),20) and ProductID=@ProductID 
                        order by id ASC";
            var dy = new DynamicParameters();
            dy.Add("ProductID", elderID);
            return DbHelper<DaySleepingEntity>.GetInstance().List(strSQL, dy);
        }
    }
}

