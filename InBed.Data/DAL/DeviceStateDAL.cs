using InBed.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Data.DAL
{
    public class DeviceStateDAL
    {
        #region 常量参数
        private readonly string columnName = @"Id,DevID,IsBed,IsOnline,PressureValue,avHR,ReceiveTime,InBedTime";
        private readonly string tableName = "DeviceState";
        private string strWhere = "1=1";
        #endregion

        public DeviceStatusEntity Query(int DevID)
        {
            strWhere = "DevID=@DevID";
            var dy = new DynamicParameters();
            dy.Add("DevID", DevID);
            return DbHelper<DeviceStatusEntity>.GetInstance().GetModel(tableName, columnName, strWhere, dy);
        }
        public List<DeviceStatusEntity> Query(List<int> devIds)
        {
            strWhere = "DevID in ("+ string.Join(",", devIds) + ")";
            var dy = new DynamicParameters();
            return DbHelper<DeviceStatusEntity>.GetInstance().List(columnName,tableName,strWhere, dy);
        }

    }
}
