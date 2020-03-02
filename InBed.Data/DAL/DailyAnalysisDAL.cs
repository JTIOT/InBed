using InBed.Core.Extentions;
using InBed.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Data.DAL
{
    public class DailyAnalysisDAL
    {
        //,customerId,gotoBedTime,getupTime,sleepingTime,getupNum,nightTimeAwakenings,deepSleepTime,sleepQuality,maxHB,maxHBTime,minHB,minHBTime,isLoseSleep,currDate,sendStatus,isNeedToSend,isGetup,operId,operTime,remark,insertYmd,insertId,updateYmd,updateId,avgHB,avgBR,sleepScore,sleepLatency
        #region 常量参数
        //private readonly string columnName = "*";
        //private string tableName = "OPENQUERY([AP-SERVER], 'SELECT * FROM BAL.dbo.CustomerDailySchedule WHERE ";
        //private string strWhere = "";

        private string sqlQuery =
            "SELECT TOP 1 * FROM OPENQUERY([AP-SERVER], 'SELECT * FROM BAL.dbo.CustomerDailySchedule WHERE";

        private string sqlOrderBy = " order by currDate desc";
        #endregion

        public List<DailyAnalysisEntity> GetWithPages(string customerId, string s_date, string e_date)
        {

            sqlQuery += " customerId = ''" + customerId + "''";
            sqlQuery += " and currDate >= ''" + s_date + "''";
            sqlQuery += " and currDate <= ''" + e_date + "'' ')";

            var dy = new DynamicParameters();
            sqlQuery = sqlQuery + sqlOrderBy;
            return DbHelper<DailyAnalysisEntity>.GetInstance().List(sqlQuery, dy);

            //if (customerId != null)
            //{
            //    //strWhere += " and customerId = '" + customerId + "'";
            //    tableName += "customerId = ''" + customerId + "'' ')";
            //}
            //if (!StringExtension.IsBlank(s_date))
            //{
            //    //strWhere += " and insertYmd >= '" + s_date + "'";
            //    //tableName += " and insertYmd >= '" + s_date + "'";
            //    //dy.Add("s_date", s_date);
            //}
            //if (!StringExtension.IsBlank(e_date))
            //{
            //    //strWhere += " and insertYmd <= '" + e_date + "'";
            //    //tableName += " and insertYmd <= '" + e_date + "'')";
            //    //dy.Add("e_date", e_date);
            //}
            //SelectBuilder data = new SelectBuilder
            //{
            //    Column = columnName,
            //    PageIndex = start,
            //    PageSize = length,
            //    OrderBy = orderBy,
            //    TableName = tableName,
            //    WhereSql = strWhere,
            //    OrderDir = orderDir
            //};
            //return DbHelper<DailyAnalysisEntity>.GetInstance().GetQueryManyForPage(data, dy, out count);

        }

    }
}
