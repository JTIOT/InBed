using InBed.Core.Extentions;
using InBed.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Data.DAL
{
   public class PageViewDAL
    {

        #region 常量参数
        private readonly string columnName = @"Id,UserId,LoginName,Url,IP,Creater,CreateDateTime,IsDeleted,ModifyTime,Modifier";
        private readonly string tableName = "PageView";
        private string strWhere = "1=1";
        #endregion
        public bool AddPageView(PageViewEntity entity)
        {
            string strSQL = @"insert into PageView(UserId,LoginName,Url,IP,Creater,CreateDateTime,IsDeleted)
                                            values(@UserId,@LoginName,@Url,@IP,@Creater,@CreateDateTime,@IsDeleted)";
            return DbHelper<PageViewEntity>.GetInstance().Insert(strSQL, entity) > 0;
        }
        /// <summary>
        /// 删除多条用户信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DelPageView(List<int> ids)
        {
            strWhere = "id in (@id)";
            var dy = new DynamicParameters();
            dy.Add("id", string.Join(",", ids));
            return DbHelper<PageViewEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        /// <summary>
        /// 删除单条用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelPageView(int id)
        {
            strWhere = "id =@id";
            var dy = new DynamicParameters();
            dy.Add("id", id);
            return DbHelper<PageViewEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        public List<PageViewEntity> GetWithPages(int start, int length, string name, string ip, DateTime s_time, DateTime e_time,
                                                                                                           string orderBy, out int count, string orderDir = "desc")
        {
   
            var dy = new DynamicParameters();
            if (!StringExtension.IsBlank(name))
            {
                strWhere += " and(LoginName like @LoginName)";
                dy.Add("LoginName", "%" + name + "%");
            }
            if (!StringExtension.IsBlank(ip))
            {
                strWhere += " and IP like @IP ";
                dy.Add("IP", "%" + ip + "%");
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
            return DbHelper<PageViewEntity>.GetInstance().GetQueryManyForPage(data, dy, out count);
        }
    }
}
