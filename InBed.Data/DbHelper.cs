using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InBed.Core;

namespace InBed.Data
{
    public class DbHelper<T> where T : class
    {
        #region 构造函数

        private static DbHelper<T> _instance;
        public static DbHelper<T> GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DbHelper<T>();
            }
            return _instance;
        }

        #endregion

        public List<T> GetQueryManyForPage(SelectBuilder data, DynamicParameters dy, out int count, SqlConnection context = null, IDbTransaction transaction = null)
        {
            List<T> list = new List<T>();
            if (context == null) context = Db.GetInstance().SqlRContext();
            string sqlStrCount = Db.GetInstance().GetSqlForCount(data);
            count = context.ExecuteScalar(sqlStrCount, dy, transaction).ToObjInt();
            if (count > 0)
            {
                string sqlStr = Db.GetInstance().GetSqlForQuery(data);
                list = context.Query<T>(sqlStr, dy, transaction).ToList();
            }
            return list;
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int Insert(string query, T model, SqlConnection context = null, IDbTransaction transaction = null)
        {
            if (context == null) context = Db.GetInstance().SqlWContext();
            return context.Execute(query, model, transaction);
        }

        public int InsertId(string query, T model, SqlConnection context = null, IDbTransaction transaction = null)
        {
            if (context == null) context = Db.GetInstance().SqlWContext();
            return context.ExecuteScalar(query, model, transaction).ToObjInt();
        }

        public T[] GetModel(string tablename, SqlConnection context = null, IDbTransaction transaction = null)
        {
            if (context == null) context = Db.GetInstance().SqlRContext();
            StringBuilder sb = new StringBuilder("select * ");
            sb.Append(" from  " + tablename);
            return context.Query<T>(sb.ToString(), null, transaction).ToArray();
        }

        /// <summary>
        /// 修改
        /// </summary>
        public int Update(string query, T model, SqlConnection context = null, IDbTransaction transaction = null)
        {
            if (context == null) context = Db.GetInstance().SqlWContext();
            return context.Execute(query, model, transaction);
        }

        public int Update(string query, DynamicParameters dy, SqlConnection context = null, IDbTransaction transaction = null)
        {
            if (context == null) context = Db.GetInstance().SqlWContext();
            return context.Execute(query, dy, transaction);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(string tablename, string wheresql, DynamicParameters dy, SqlConnection context = null, IDbTransaction transaction = null)
        {
            if (context == null) context = Db.GetInstance().SqlWContext();
            StringBuilder sb = new StringBuilder("delete ");
            sb.Append(tablename);
            sb.Append(" where " + wheresql);
            return context.Execute(sb.ToString(), dy, transaction);
        }


        /// <summary>
        /// 获取单个实体
        /// </summary>

        public T GetModel(string tablename, string column, string wheresql, DynamicParameters dy, SqlConnection context = null, IDbTransaction transaction = null)
        {
            if (context == null) context = Db.GetInstance().SqlRContext();
            StringBuilder sb = new StringBuilder("select top 1 ");
            sb.Append(column);
            sb.Append(" from  " + tablename);
            sb.Append(" (nolock) where " + wheresql);
            return context.Query<T>(sb.ToString(), dy, transaction).FirstOrDefault();
        }

        public T Detail(string tablename, string column, DynamicParameters dy, SqlConnection context = null, IDbTransaction transaction = null)
        {
            if (context == null) context = Db.GetInstance().SqlRContext();
            StringBuilder sb = new StringBuilder("select top 1 ");
            sb.Append(column);
            sb.Append(" from  " + tablename);
            sb.Append(" (nolock) where Id=@Id");
            return context.Query<T>(sb.ToString(), dy, transaction).FirstOrDefault();
        }

        /// <summary>
        /// 实体列表
        /// </summary>
        public List<T> List(string query, DynamicParameters dy, SqlConnection context = null, IDbTransaction transaction = null)
        {
            if (context == null) context = Db.GetInstance().SqlRContext();
            return context.Query<T>(query, dy, transaction).ToList();
        }
        public List<T> BalList(string query, DynamicParameters dy, SqlConnection context = null, IDbTransaction transaction = null)
        {
            if (context == null) context = Db.GetInstance().BalSqlRContext();
            return context.Query<T>(query, dy, transaction).ToList();
        }

        public List<T> List(string column, string tableName, string where = null, DynamicParameters dy = null, SqlConnection context = null, IDbTransaction transaction = null)
        {
            if (context == null) context = Db.GetInstance().SqlRContext();
            StringBuilder sb = new StringBuilder("select ");
            sb.Append(column);
            sb.Append(" from " + tableName + " (nolock) ");
            if (!string.IsNullOrEmpty(where))
            {
                sb.Append("where " + where);
            }
            return context.Query<T>(sb.ToString(), dy, transaction).ToList();
        }


    }
}
