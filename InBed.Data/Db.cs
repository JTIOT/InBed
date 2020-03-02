using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using InBed.Core;

namespace InBed.Data
{
    public class Db
    {
        #region 构造函数

        private static Db _instance;
        public static Db GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Db();
            }
            return _instance;
        }

        #endregion

        #region Context
        public SqlConnection SqlRContext()//读取连接
        {
            return new SqlConnection(DBConstant.SqlR);
        }

        public SqlConnection SqlWContext()//操作连接
        {
            return new SqlConnection(DBConstant.SqlW);
        }
        public SqlConnection BalSqlRContext()//读取连接
        {
            return new SqlConnection(DBConstant.BalSqlR);
        }

        public SqlConnection BalSqlWContext()//操作连接
        {
            return new SqlConnection(DBConstant.BalSqlW);
        }
        #endregion

        #region 分页语句拼接
        public string GetSqlForQuery(SelectBuilder data)
        {
            StringBuilder sb = new StringBuilder("select ");
            if (data.Top > 0)
            {
                sb.Append("top " + data.Top);
            }
            sb.Append(data.Column);
            sb.Append(" from " + data.TableName + " (nolock) ");
            if (!string.IsNullOrEmpty(data.WhereSql))
            {
                sb.Append(" where " + data.WhereSql);
            }

            if (!string.IsNullOrEmpty(data.GroupBy))
            {
                sb.Append(" group by " + data.GroupBy);
            }

            if (!string.IsNullOrEmpty(data.OrderBy))
            {
                sb.Append(" order by " + data.OrderBy + " "+ data.OrderDir);
            }

            if (data.PageIndex >= 0 && data.PageSize > 0)
            {
                
              //  sb.AppendFormat(" OFFSET  {0} ROWS FETCH NEXT  {1} ROWS ONLY", (data.PageIndex - 1) * data.PageSize, data.PageSize);
                sb.AppendFormat(" OFFSET  {0} ROWS FETCH NEXT  {1} ROWS ONLY", data.PageIndex, data.PageSize);
            }

            return sb.ToString();
        }

        public string GetSqlForCount(SelectBuilder data)
        {
            StringBuilder sb = new StringBuilder("select count(1) from ");
            sb.Append(data.TableName);
            if (!string.IsNullOrEmpty(data.WhereSql))
            {
                sb.Append(" where " + data.WhereSql);
            }
            return sb.ToString();
        }
        #endregion

    }
}
