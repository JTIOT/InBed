using InBed.Core.Extentions;
using InBed.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Data.DAL
{
    public  class AlarmDAL
    {
        #region 常量参数
        private readonly string columnName = @"ID ,DeviceNumber ,ElderID ,AlarmType ,AlarmTime ,UploadTime ,OnLineTime ,AlarmReason,IsHandle,HandleOpinions,Modifier";
        private readonly string tableName = "AlarmList";
        private string strWhere = "1=1";
        #endregion
        /// <summary>
        /// 处理告警
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool HandleAlarm(AlarmEntity entity)
        {
            string strSQL = @"update AlarmList set IsHandle=@IsHandle,HandleOpinions=@HandleOpinions,Modifier=@Modifier where id=@id";
            var dy = new DynamicParameters();
            dy.Add("IsHandle", entity.IsHandle);
            dy.Add("HandleOpinions", entity.HandleOpinions);
            dy.Add("Modifier", entity.Modifier);
            dy.Add("id", entity.ID);
            return DbHelper<AlarmEntity>.GetInstance().Update(strSQL, dy) > 0;
        }
        /// <summary>
        /// 获取告警
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<AlarmEntity> GetAlarm(string f_code)
        {

            string strSQL = @"select AlarmList.ID ,DeviceNumber ,ElderID ,AlarmType ,AlarmTime ,UploadTime ,OnLineTime ,AlarmReason,IsHandle,HandleOpinions,AlarmList.Modifier,name as ElderName,Elder.Id as ElderId from AlarmList (nolock) 
                                            right join Elder (nolock)  on Elder.Id =AlarmList.ElderID
                                            where AlarmList.AlarmType<>218 and IsHandle=0   ";
            var dy = new DynamicParameters();
            if (f_code != "F-00001")
            {
                strSQL += " and  Elder.FacilitatorCode=@FacilitatorCode";
                dy.Add("FacilitatorCode", f_code);
            }
           
            strSQL += " order by AlarmTime desc";

            return DbHelper<AlarmEntity>.GetInstance().List(strSQL, dy);
        }
        public AlarmEntity GetDetail(int id)
        {

            string strSQL = @"select AlarmList.ID ,DeviceNumber ,ElderID ,AlarmType ,AlarmTime ,UploadTime ,OnLineTime ,AlarmReason,IsHandle,HandleOpinions,AlarmList.Modifier,name as ElderName from AlarmList (nolock) 
                                            right join Elder (nolock)  on Elder.Id=AlarmList.ElderID
                                            where AlarmList.ID=@id";
            var dy = new DynamicParameters();
            dy.Add("id", id);

            return DbHelper<AlarmEntity>.GetInstance().List(strSQL, dy).FirstOrDefault();
        }

        public List<AlarmEntity> GetTypeAlarm(string f_code,int alermType,string s_time,string e_time)
        {
            string strSQL = @"select ID ,DeviceNumber ,ElderID ,AlarmType ,AlarmTime ,UploadTime ,OnLineTime ,AlarmReason,IsHandle,HandleOpinions,Modifier,name as ElderName from AlarmList (nolock) 
                                            right join Elder (nolock)  on Elder.Id=AlarmList.ElderID
                                            where AlarmList.AlarmType=@AlarmType and IsHandle=0 and  Elder.Facilitator_Code=@Facilitator_Code and AlarmTime between @s_time and @e_time";
            var dy = new DynamicParameters();
            dy.Add("FacilitatorCode", f_code);
            dy.Add("AlarmType", alermType);
            dy.Add("AlarmType", alermType);
            dy.Add("s_time", s_time);
            dy.Add("e_time", e_time);
            return DbHelper<AlarmEntity>.GetInstance().List(strSQL, dy);
        }

        public List<AlarmEntity> GetWithPages(int start, int length, int elderID, string s_data, string e_data, int type,
                                                                                                 string orderBy, out int count, string orderDir = "desc")
        {

            var dy = new DynamicParameters();
           
            if (elderID!=0)
            {
                strWhere += " and ElderID = @ElderID";
                dy.Add("ElderID", elderID);
            }
            if (!StringExtension.IsBlank(s_data))
            {
                strWhere += " and AlarmTime >= @s_data ";
                dy.Add("s_data", s_data.Trim());
            }
            if (!StringExtension.IsBlank(e_data))
            {
                strWhere += " and AlarmTime <= @e_data ";
                dy.Add("e_data", e_data.Trim());
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
            return DbHelper<AlarmEntity>.GetInstance().GetQueryManyForPage(data, dy, out count);
        }
    }
}
