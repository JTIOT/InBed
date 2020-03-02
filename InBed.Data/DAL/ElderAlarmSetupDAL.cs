using InBed.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Data.DAL
{
    public class ElderAlarmSetupDAL
    {
        #region 常量参数
        private readonly string columnName = @"id,Elder_id,maxheart,minheart,leavebedtime,maxbreath,minbreath,pausebreath,inbedtime,set_type,sleeptime,crea_time,creater,modify_time,modifier,s_time,e_time";
        private readonly string tableName = "ElderAlarmSetup";
        private string strWhere = "1=1";
        #endregion
        public bool AddElderAlarmSetup(ElderAlarmSetupEntity entity)
        {
            string strSQL = @"insert into ElderAlarmSetup(Elder_id,maxheart,minheart,leavebedtime,maxbreath,minbreath,pausebreath,inbedtime,set_type,crea_time,creater,s_time,e_time)
                                            values(@Elder_id,@maxheart,@minheart,@leavebedtime,@maxbreath,@minbreath,@pausebreath,@inbedtime,@set_type,@crea_time,@creater,@s_time,@e_time)";
            return DbHelper<ElderAlarmSetupEntity>.GetInstance().Insert(strSQL, entity) > 0;
        }
        /// <summary>
        /// 删除多条用户信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DelElderAlarmSetup(List<int> ids)
        {
            strWhere = "id in (@id)";
            var dy = new DynamicParameters();
            dy.Add("id", string.Join(",", ids));
            return DbHelper<ElderAlarmSetupEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        /// <summary>
        /// 删除单条用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelElderAlarmSetup(int id)
        {
            strWhere = "id =@id";
            var dy = new DynamicParameters();
            dy.Add("id", id);
            return DbHelper<ElderAlarmSetupEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }

        public bool EditElderAlarmSetup(ElderAlarmSetupEntity entity)
        {
            string strSQL = @"update ElderAlarmSetup set maxheart=@maxheart,minheart=@minheart,leavebedtime=@leavebedtime,maxbreath=@maxbreath,minbreath=@minbreath,
                                                            pausebreath=@pausebreath,inbedtime=@inbedtime,set_type=@set_type,sleeptime=@sleeptime,Modifier=@Modifier,modify_time=@ModifyTime,s_time=@s_time,e_time=@e_time where id=@id";
            var dy = new DynamicParameters();
            dy.Add("maxheart", entity.maxheart);
            dy.Add("minheart", entity.minheart);
            dy.Add("leavebedtime", entity.leavebedtime);
            dy.Add("maxbreath", entity.maxbreath);
            dy.Add("minbreath", entity.minbreath);
            dy.Add("pausebreath", entity.pausebreath);
            dy.Add("inbedtime", entity.inbedtime);
            dy.Add("set_type", entity.set_type);
            dy.Add("ModifyTime", DateTime.Now);
            dy.Add("Modifier", entity.Modifier);
            dy.Add("sleeptime", entity.sleeptime);
            dy.Add("s_time", entity.s_time);
            dy.Add("e_time", entity.e_time);
            dy.Add("id", entity.Id);
            return DbHelper<ElderAlarmSetupEntity>.GetInstance().Update(strSQL, dy) > 0;
        }

        public ElderAlarmSetupEntity Details(int id)
        {
            strWhere = "id=@id";
            var dy = new DynamicParameters();
            dy.Add("id", id);
            return DbHelper<ElderAlarmSetupEntity>.GetInstance().GetModel(tableName, columnName, strWhere, dy);
        }

        public ElderAlarmSetupEntity Query(int elderID)
        {
            strWhere = "Elder_id=@Elder_id";
            var dy = new DynamicParameters();
            dy.Add("Elder_id", elderID);
            return DbHelper<ElderAlarmSetupEntity>.GetInstance().List(columnName, tableName, strWhere, dy).FirstOrDefault();
        }
        public List<ElderAlarmSetupEntity> AnalysisQuery()
        {
            return DbHelper<ElderAlarmSetupEntity>.GetInstance().List(columnName, tableName, "", new DynamicParameters());
        }
        public List<ElderAlarmSetupEntity> GetWithPages(int start, int length, List<int> editid,string orderBy, out int count, string orderDir = "desc")
        {
            strWhere = "";
            var dy = new DynamicParameters();
            if (editid!=null && editid.Any())
            {
                strWhere += " Elder_id in (" + string.Join(",", editid) + ")";
               
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
            return DbHelper<ElderAlarmSetupEntity>.GetInstance().GetQueryManyForPage(data, dy, out count);
        }
    }
}

