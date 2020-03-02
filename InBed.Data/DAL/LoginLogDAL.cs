using InBed.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Data.DAL
{
    public class LoginLogDAL
    {
        #region 常量参数
        private readonly string columnName = @"Id,LoginName,IP,mac,Creater,CreateDateTime,IsDeleted,ModifyTime,Modifier";
        private readonly string tableName = "LoginLog";
        private string strWhere = "1=1";
        #endregion

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool AddLoginLog(LoginLogEntity entity)
        {
            string strSQL = @"insert into LoginLog(LoginName,IP,mac,Creater,CreateDateTime,IsDeleted)
                                            values(@LoginName,@IP,@mac,@Creater,@CreateDateTime,@IsDeleted)";
            return DbHelper<LoginLogEntity>.GetInstance().Insert(strSQL, entity) > 0;
        }
        /// <summary>
          /// 删除多条用户信息
          /// </summary>
          /// <param name="ids"></param>
          /// <returns></returns>
        public bool DelLoginLog(List<int> ids)
        {
            strWhere = "id in (@id)";
            var dy = new DynamicParameters();
            dy.Add("id", string.Join(",", ids));
            return DbHelper<LoginLogEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        /// <summary>
        /// 删除单条用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelLoginLog(int id)
        {
            strWhere = "id =@id";
            var dy = new DynamicParameters();
            dy.Add("id", id);
            return DbHelper<LoginLogEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }

    }
}