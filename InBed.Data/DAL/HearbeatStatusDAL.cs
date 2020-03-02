using InBed.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Data.DAL
{
   public class HearbeatStatusDAL
    {
        #region 常量参数
        private readonly string columnName = @"id,DeviceID,HeartbeatRate,BreathingRate,PressureValue,HaveHR,FileIndex,SleepActivity,TimeStamp,ReceiveTime";
        private readonly string tableName = "HearbeatStatus";
        private string strWhere = "1=1";
        #endregion
        public bool AddHearbeatStatus(HearbeatStatusEntity entity)
        {
            string strSQL = @"insert into HearbeatStatus(DeviceID,HeartbeatRate,BreathingRate,PressureValue,HaveHR,FileIndex,SleepActivity,TimeStamp,ReceiveTime)
                                            values(@DeviceID,@HeartbeatRate,@BreathingRate,@PressureValue,@HaveHR,@FileIndex,@SleepActivity,@TimeStamp,@ReceiveTime)";
            return DbHelper<HearbeatStatusEntity>.GetInstance().Insert(strSQL, entity) > 0;
        }
        /// <summary>
        /// 删除多条用户信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DelHearbeatStatus(List<int> ids)
        {
            strWhere = "id in (@id)";
            var dy = new DynamicParameters();
            dy.Add("id", string.Join(",", ids));
            return DbHelper<HearbeatStatusEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        /// <summary>
        /// 删除单条用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelHearbeatStatus(int id)
        {
            strWhere = "id =@id";
            var dy = new DynamicParameters();
            dy.Add("id", id);
            return DbHelper<HearbeatStatusEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }

        public List<RealTimeEntity> GetRealTime(string device)
        {
            string strSQL = @"select top 300 id,HeartbeatRate,BreathingRate,PressureValue,SleepActivity,ReceiveTime from Bedplate2015.dbo.[" + device + "] where HeartbeatRate<200 order by id  desc";
            var dy = new DynamicParameters();
            return DbHelper<RealTimeEntity>.GetInstance().List(strSQL, dy);
        }
        public List<RealTimeEntity> GetPressureTime(string device)
        {
            string strSQL = @"select id,HeartbeatRate,BreathingRate,PressureValue,SleepActivity,ReceiveTime from Bedplate2015.dbo.[" + device + "] where ReceiveTime>'"+DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss") + "' order by ReceiveTime  desc";
            var dy = new DynamicParameters();
            return DbHelper<RealTimeEntity>.GetInstance().List(strSQL, dy);
        }
        public List<RealTimeEntity> GetPressureTime(string device,DateTime s_time, DateTime e_time)
        {
            string strSQL = @"select id,HeartbeatRate,BreathingRate,PressureValue,SleepActivity,ReceiveTime from Bedplate2015.dbo.[" + device + "] where ReceiveTime between @s_time and @e_time order by ReceiveTime  desc";
            var dy = new DynamicParameters();
            dy.Add("s_time", s_time);
            dy.Add("e_time", e_time);
            return DbHelper<RealTimeEntity>.GetInstance().List(strSQL, dy);
        }
        public List<RealTimeEntity> GetBreathingTime(string device, DateTime s_time, DateTime e_time)
        {
            string strSQL = @"select id,HeartbeatRate,BreathingRate,PressureValue,SleepActivity,ReceiveTime from Bedplate2015.dbo.[" + device + "] where ReceiveTime between @s_time and @e_time and BreathingRate>0 order by ReceiveTime  desc";
            var dy = new DynamicParameters();
            dy.Add("s_time", s_time);
            dy.Add("e_time", e_time);
            return DbHelper<RealTimeEntity>.GetInstance().List(strSQL, dy);
        }
        public List<RealTimeEntity> GetRealTime(string device,DateTime s_time,DateTime e_time)
        {
            string strSQL = @"select id,HeartbeatRate,BreathingRate,PressureValue,SleepActivity,ReceiveTime from Bedplate2015.dbo.[" + device + "] where ReceiveTime between @s_time and @e_time and HBReliability>70 order by id  desc";
            var dy = new DynamicParameters();
            dy.Add("s_time", s_time);
            dy.Add("e_time", e_time);
            return DbHelper<RealTimeEntity>.GetInstance().List(strSQL, dy);
        }
        public List<RealTimeEntity> GetTop10Data(string device)
        {
            string strSQL = @"select top 5 id,HeartbeatRate,BreathingRate,PressureValue,SleepActivity,ReceiveTime from Bedplate2015.dbo.[" + device + "] where  HBReliability>70 order by id  desc";
            var dy = new DynamicParameters();
            return DbHelper<RealTimeEntity>.GetInstance().List(strSQL, dy);
        }
        public List<RealTimeEntity> GetBedData(string device)
        {
            string strSQL = @"select top 10 id,HeartbeatRate,BreathingRate,PressureValue,SleepActivity,ReceiveTime from Bedplate2015.dbo.[" + device + "]  order by id  desc";
            var dy = new DynamicParameters();
            return DbHelper<RealTimeEntity>.GetInstance().List(strSQL, dy);
        }
    }
}
