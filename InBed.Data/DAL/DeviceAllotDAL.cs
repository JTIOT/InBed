using InBed.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Data.DAL
{
    public class DeviceAllotDAL
    {
        #region 常量参数
        private readonly string columnName = @"Id,deviceID,up_facilitator,facilitator,crea_time,creater,modify_time,modifier";
        private readonly string tableName = "deviceAllot";
        private string strWhere = "1=1";
        #endregion
        public bool AddDeviceAllot(DeviceAllotEntity entity)
        {
            string strSQL = @"insert into deviceAllot(deviceID,up_facilitator,facilitator,crea_time,creater)
                                            values(@deviceID,@up_facilitator,@facilitator,@CreateDateTime,@creater)";
            return DbHelper<DeviceAllotEntity>.GetInstance().Insert(strSQL, entity) > 0;
        }
    }
}
