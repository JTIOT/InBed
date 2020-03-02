using InBed.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Data.DAL
{
   public  class OnBedRecordDAL
    {

        #region 常量参数
        private readonly string columnName = @"id,deviceID,OnBedTime,LeaveBedTime,CreateTime,OnBedDetectTime,OnBedKeyIndex,LeaveBedKeyIndex";
        private readonly string tableName = "OnBedRecord";
        private string strWhere = "1=1";
        #endregion
        public bool AddOnBedRecord(OnBedRecordEntity entity)
        {
            string strSQL = @"insert into OnBedRecord(deviceID,OnBedTime,LeaveBedTime,CreateTime,OnBedDetectTime,OnBedKeyIndex,LeaveBedKeyIndex)
                                            values(@deviceID,@OnBedTime,@LeaveBedTime,@CreateTime,@OnBedDetectTime,@OnBedKeyIndex,@LeaveBedKeyIndex)";
            return DbHelper<OnBedRecordEntity>.GetInstance().Insert(strSQL, entity) > 0;
        }
        /// <summary>
        /// 删除多条用户信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DelOnBedRecord(List<int> ids)
        {
            strWhere = "id in (@id)";
            var dy = new DynamicParameters();
            dy.Add("id", string.Join(",", ids));
            return DbHelper<OnBedRecordEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        /// <summary>
        /// 删除单条用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelOnBedRecord(int id)
        {
            strWhere = "id =@id";
            var dy = new DynamicParameters();
            dy.Add("id", id);
            return DbHelper<OnBedRecordEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        public List<HalfMonthOnBedRecordEntity> GetHalfMonthOnBedRecord(int elderID)
        {
            string strSQL = @"select ProductID,CONVERT(nvarchar(10),CreateTime,20) as Date,OnBedTime,LeaveBedTime 
                                from OnBedRecord where ProductID=@ProductID 
                                and CONVERT(nvarchar(12),CreateTime,20)  between CONVERT(nvarchar(16),dateadd(day,-15, CONVERT (nvarchar(12),GETDATE(),112))+'12:00',20) 
                                and CONVERT(nvarchar(16),CONVERT(nvarchar(12),GETDATE(),20)+'12:00',20)
                                order by id ASC ";
            var dy = new DynamicParameters();
            dy.Add("ProductID", elderID);
            return DbHelper<HalfMonthOnBedRecordEntity>.GetInstance().List(strSQL, dy);
        }
        public DayOnBedRecordEntity GetDayInBedRecord(int elderID,DateTime OnBedTime)
        {
            string strSQL = @"select top 1 ProductID,CONVERT(nvarchar(10),CreateTime,20) as Date,OnBedTime,LeaveBedTime 
                                from OnBedRecord where ProductID=@ProductID 
                                and OnBedTime<@OnBedTime order by OnBedTime desc";
            var dy = new DynamicParameters();
            dy.Add("ProductID", elderID);
            dy.Add("OnBedTime", OnBedTime);
            return DbHelper<DayOnBedRecordEntity>.GetInstance().List(strSQL, dy).FirstOrDefault();
        }
        public DayOnBedRecordEntity GetDayOutBedRecord(int elderID, DateTime LeaveBedTime)
        {
            string strSQL = @"select top 1 ProductID,CONVERT(nvarchar(10),CreateTime,20) as Date,OnBedTime,LeaveBedTime 
                                from OnBedRecord where ProductID=@ProductID 
                                and LeaveBedTime>@LeaveBedTime";
            var dy = new DynamicParameters();
            dy.Add("ProductID", elderID);
            dy.Add("LeaveBedTime", LeaveBedTime);
            return DbHelper<DayOnBedRecordEntity>.GetInstance().List(strSQL, dy).FirstOrDefault();
        }
        public OutBedRecordCountEntity GetOutBedCount(int elderID, DateTime OnBedTime, DateTime LeaveBedTime)
        {
            string strSQL = @"select count(id) as count from OnBedRecord where  ProductID=@ProductID 
                                and OnBedTime>=@OnBedTime and  LeaveBedTime<@LeaveBedTime";
            var dy = new DynamicParameters();
            dy.Add("ProductID", elderID);
            dy.Add("OnBedTime", OnBedTime);
            dy.Add("LeaveBedTime", LeaveBedTime);
            return DbHelper<OutBedRecordCountEntity>.GetInstance().List(strSQL, dy).FirstOrDefault();
        }

    }
}
