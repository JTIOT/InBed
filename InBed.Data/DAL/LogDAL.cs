using InBed.Core.Extentions;
using InBed.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Data.DAL
{
    public class LogDAL
    {
        #region 常量参数
        private readonly string columnName = @"Id,LoginName,IP,mac,Creater,CreateDateTime,IsDeleted,ModifyTime,Modifier";
        private readonly string tableName = "loginlog";
        private string strWhere = "1=1";
        #endregion

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool AddLog(LoginLogEntity entity)
        {
            string strSQL = @"insert into loginlog(LoginName,IP,mac,Creater,CreateDateTime,IsDeleted)
                                            values(@LoginName,@IP,@mac,@Creater,@CreateDateTime,@IsDeleted)";
            return DbHelper<LoginLogEntity>.GetInstance().Insert(strSQL, entity) > 0;
        }
        /// <summary>
          /// 删除多条用户信息
          /// </summary>
          /// <param name="ids"></param>
          /// <returns></returns>
        public bool DelLog(List<int> ids)
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
        public bool DelLog(int id)
        {
            strWhere = "id =@id";
            var dy = new DynamicParameters();
            dy.Add("id", id);
            return DbHelper<LoginLogEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="creater"></param>
        /// <param name="name"></param>
        /// <param name="orderBy"></param>
        /// <param name="count"></param>
        /// <param name="orderDir"></param>
        /// <returns></returns>
        public List<LoginLogEntity> GetWithPages(int start, int length, string loginName, string IP, DateTime s_time, DateTime e_time,
                                                                                                            string orderBy, out int count, string orderDir = "desc")
        {

            var dy = new DynamicParameters();
            if (!StringExtension.IsBlank(loginName))
            {
                strWhere += " and(LoginName like @LoginName)";
                dy.Add("LoginName", "%" + loginName + "%");
            }
            if (!StringExtension.IsBlank(IP))
            {
                strWhere += " and(IP like @IP)";
                dy.Add("IP", "%" + IP + "%");
            }
            if (s_time > DateTime.MinValue)
            {
                strWhere += " and CreateDateTime >=@s_time ";
                dy.Add("s_time", s_time);
            }
            if (e_time > DateTime.MinValue)
            {
                strWhere += " and CreateDateTime<=@e_time";
                dy.Add("e_time", e_time);
            }
            dy.Add("IsDeleted", false);
            SelectBuilder data = new SelectBuilder
            {
                Column = columnName,
                PageIndex = start,
                PageSize = length,
                OrderBy = orderBy,
                TableName = tableName,
                WhereSql = strWhere,
                OrderDir = orderDir
            };
            return DbHelper<LoginLogEntity>.GetInstance().GetQueryManyForPage(data, dy, out count);
        }

    }
}