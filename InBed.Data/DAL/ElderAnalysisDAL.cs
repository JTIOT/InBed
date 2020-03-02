using InBed.Core.Extentions;
using InBed.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Data.DAL
{
    public class ElderAnalysisDAL
    {
        #region 常量参数
        private readonly string columnName = @"ID ,AnalysisDate,ElderID,InBedTime ,GetUpTime ,FallAsleepTime ,SoberTime ,NightCount ,SeepLong,DepthSeepLong,ShallowSeepLong,SoberSeepLong,SoberCount";
        private readonly string tableName = "ElderAnalysis";
        private string strWhere = "1=1";
        #endregion
        public bool AddElderAnalysis(ElderAnalysisEntity entity)
        {
            string strSQL = @"insert into ElderAnalysis(AnalysisDate,ElderID,InBedTime,GetUpTime,FallAsleepTime,SoberTime,NightCount,SeepLong,DepthSeepLong,ShallowSeepLong,SoberSeepLong,SoberCount)
                                            values(@AnalysisDate,@ElderID,@InBedTime,@GetUpTime,@FallAsleepTime,@SoberTime,@NightCount,@SeepLong,@DepthSeepLong,@ShallowSeepLong,@SoberSeepLong,@SoberCount)";
            return DbHelper<ElderAnalysisEntity>.GetInstance().Insert(strSQL, entity) > 0;
        }

        public List<ElderAnalysisEntity> GetWithPages(int start, int length, List<int> elderids, string s_date, string e_date,
                                                                                                           string orderBy, out int count, string orderDir = "desc")
        {
            var dy = new DynamicParameters();
            if (elderids != null && elderids.Any())
            {
                strWhere += " and ElderID in ("+ string.Join(",", elderids) + ")";
            }
            if (!StringExtension.IsBlank(s_date))
            {
                strWhere += " and(AnalysisDate >= @s_date)";
                dy.Add("s_date", s_date);
            }
            if (!StringExtension.IsBlank(e_date))
            {
                strWhere += " and(AnalysisDate <= @e_date)";
                dy.Add("e_date", e_date);
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
            return DbHelper<ElderAnalysisEntity>.GetInstance().GetQueryManyForPage(data, dy, out count);
        }

    }
}
