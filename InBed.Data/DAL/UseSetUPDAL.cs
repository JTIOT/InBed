using InBed.Core.Extentions;
using InBed.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Data.DAL
{
    public class UseSetUPDAL
    {
        #region 常量参数
        private readonly string columnName = @"id ,facilitator_code ,facilitator_name ,Elder_id ,start_data ,end_data ,is_enable ,crea_time,creater,modify_time,modifier";
        private readonly string tableName = "SystemElderSetup";
        private string strWhere = "1=1";
        #endregion

        public List<ElderSetupEntity> GetWithPages(int start, int length,string f_code, int elderID, DateTime s_data, DateTime e_data,
                                                                                                string orderBy, out int count, string orderDir = "desc")
        {

            var dy = new DynamicParameters();

            if (f_code != "F-00001")
            {
                strWhere += " and facilitator_code = @facilitator_code";
                dy.Add("facilitator_code", f_code);
            }
            if (elderID!=0)
            {
                strWhere += " and Elder_id = @Elder_id ";
                dy.Add("Elder_id", elderID);
            }
          if (s_data > DateTime.MinValue)
            {
                strWhere += " and start_data >= @s_data ";
                dy.Add("s_data", s_data);
            }
            if (e_data > DateTime.MinValue)
            {
                strWhere += " and end_data <= @e_data ";
                dy.Add("e_data", e_data);
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
            return DbHelper<ElderSetupEntity>.GetInstance().GetQueryManyForPage(data, dy, out count);
        }

        public ElderSetupEntity GetDetail(int id)
        {
            strWhere = "id=@id";
            var dy = new DynamicParameters();
            dy.Add("id", id);
            return DbHelper<ElderSetupEntity>.GetInstance().GetModel(tableName, columnName, strWhere, dy);
        }
        public bool EditElderSetup(ElderSetupEntity entity)
        {
            string strSQL = @"update SystemElderSetup set start_data=@start_data,end_data=@end_data,is_enable=@is_enable,Modifier=@Modifier,modify_time=@ModifyTime where id=@id";
            var dy = new DynamicParameters();
            dy.Add("start_data", entity.start_data);
            dy.Add("end_data", entity.end_data);
            dy.Add("is_enable", entity.is_enable);
            dy.Add("ModifyTime", DateTime.Now);
            dy.Add("Modifier", entity.Modifier);
            dy.Add("id", entity.Id);
            return DbHelper<ElderSetupEntity>.GetInstance().Update(strSQL, dy) > 0;
        }
        public bool DelElderSetup(List<int> ids)
        {
            strWhere = "Elder_id in ("+ string.Join(",", ids) + ")";
            var dy = new DynamicParameters();
            return DbHelper<ElderSetupEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        public bool AddElderSetup(ElderSetupEntity entity)
        {
            string strSQL = @"insert into SystemElderSetup(facilitator_code ,facilitator_name ,Elder_id ,start_data ,end_data ,is_enable ,crea_time,creater)
                                            values(@facilitator_code ,@facilitator_name ,@Elder_id ,@start_data ,@end_data ,@is_enable ,@crea_time,@creater)";
            return DbHelper<ElderSetupEntity>.GetInstance().Insert(strSQL, entity) > 0;
        }
    }
}
