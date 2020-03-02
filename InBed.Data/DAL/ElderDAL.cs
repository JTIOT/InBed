using InBed.Core.Extentions;
using InBed.Entity;
using InBed.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Data.DAL
{
   public class ElderDAL
    {
        #region 常量参数
        private readonly string columnName = @"Id,name,photo,sex,birthday,number,workunit,homephone,contacts,c_telephone,resident_type,nation,blood_type,education,occupation,marriageStatus,dispensedmode,H_allergydrug,H_disease,is_ops,ops_disease,ops_data,is_genetic,genetic_disease,is_deformity,deformity_number,FacilitatorCode,FacilitatorName,homeaddress,Creater,CreateDateTime,IsDeleted,ModifyTime,Modifier,IsWelfare,MapPoint";
        private readonly string tableName = "Elder";
        private string strWhere = "1=1";
        #endregion
        public bool AddElder(ElderEntity entity)
        {
            string strSQL = @"insert into Elder(name,photo,sex,birthday,number,workunit,homephone,contacts,c_telephone,resident_type,nation,blood_type,education,occupation,marriageStatus,dispensedmode,H_allergydrug,H_disease,is_ops,ops_disease,ops_data,is_genetic,genetic_disease,is_deformity,deformity_number,FacilitatorCode,FacilitatorName,homeaddress,Creater,CreateDateTime,IsDeleted,IsWelfare,MapPoint)
                                            values(@name,@photo,@sex,@birthday,@number,@workunit,@homephone,@contacts,@c_telephone,@resident_type,@nation,@blood_type,@education,@occupation,@marriageStatus,@dispensedmode,@H_allergydrug,@H_disease,@is_ops,@ops_disease,@ops_data,@is_genetic,@genetic_disease,@is_deformity,@deformity_number,@FacilitatorCode,@FacilitatorName,@homeaddress,@Creater,@CreateDateTime,@IsDeleted,@IsWelfare,@MapPoint)";
            return DbHelper<ElderEntity>.GetInstance().Insert(strSQL, entity) > 0;
        }
        /// <summary>
        /// 删除多条用户信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DelElder(List<int> ids)
        {
            strWhere = "id in (@id)";
            var dy = new DynamicParameters();
            dy.Add("id", string.Join(",", ids));
            return DbHelper<ElderEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        /// <summary>
        /// 删除单条用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelElder(int id)
        {
            strWhere = "id =@id";
            var dy = new DynamicParameters();
            dy.Add("id", id);
            return DbHelper<ElderEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        public ElderEntity Details(int id)
        {
            strWhere = "id=@id";
            var dy = new DynamicParameters();
            dy.Add("id", id);
            return DbHelper<ElderEntity>.GetInstance().GetModel(tableName, columnName, strWhere, dy);
        }
        public ElderEntity[] AllDetails()
        {
            return DbHelper<ElderEntity>.GetInstance().GetModel(tableName);
        }
        /// <summary>
        /// 修改老人信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool EditElder(ElderEntity entity)
        {
            string strSQL = @"update Elder set name=@name,sex=@sex,birthday=@birthday,number=@number,
                                               workunit=@workunit,homephone=@homephone,contacts=@contacts,
                                               c_telephone=@c_telephone,resident_type=@resident_type,nation=@nation,
                                               blood_type=@blood_type,education=@education,occupation=@occupation,
                                               marriageStatus=@marriageStatus,dispensedmode=@dispensedmode,
                                               H_allergydrug=@H_allergydrug,H_disease=@H_disease,is_ops=@is_ops,
                                               ops_disease=@ops_disease,ops_data=@ops_data,is_genetic=@is_genetic,
                                               genetic_disease=@genetic_disease,is_deformity=@is_deformity,
                                               deformity_number=@deformity_number,homeaddress=@homeaddress,
                                               IsDeleted=@IsDeleted,ModifyTime=@ModifyTime,Modifier=@Modifier,IsWelfare=@IsWelfare,MapPoint=@MapPoint where id=@id";

            var dy = new DynamicParameters();
            dy.Add("name", entity.name);
            dy.Add("photo", entity.photo);
            dy.Add("sex", entity.sex);
            dy.Add("birthday", entity.birthday);
            dy.Add("number", entity.number);
            dy.Add("workunit", entity.workunit);
            dy.Add("homephone", entity.homephone);
            dy.Add("contacts", entity.contacts);
            dy.Add("c_telephone", entity.c_telephone);
            dy.Add("resident_type", entity.resident_type);
            dy.Add("nation", entity.nation);
            dy.Add("blood_type", entity.blood_type);
            dy.Add("education", entity.education);
            dy.Add("occupation", entity.occupation);
            dy.Add("marriageStatus", entity.marriageStatus);
            dy.Add("dispensedmode", entity.dispensedmode);
            dy.Add("H_allergydrug", entity.H_allergydrug);
            dy.Add("H_disease", entity.H_disease);
            dy.Add("is_ops", entity.is_ops);
            dy.Add("ops_disease", entity.ops_disease);
            dy.Add("ops_data", entity.ops_data);
            dy.Add("is_genetic", entity.is_genetic);
            dy.Add("genetic_disease", entity.genetic_disease);
            dy.Add("is_deformity", entity.is_deformity);
            dy.Add("deformity_number", entity.deformity_number);
            dy.Add("homeaddress", entity.homeaddress);
            dy.Add("IsDeleted", entity.IsDeleted);
            dy.Add("ModifyTime", DateTime.Now);
            dy.Add("Modifier", entity.Modifier);
            dy.Add("id", entity.Id);
            dy.Add("IsWelfare", entity.IsWelfare);
            dy.Add("MapPoint", entity.MapPoint);

            return DbHelper<ElderEntity>.GetInstance().Update(strSQL, dy) > 0;
        }

        public bool UpdateElderFacilitator(ElderEntity entity)
        {
            string strSQL = @"update Elder set FacilitatorCode=@FacilitatorCode,FacilitatorName=@FacilitatorName where id=@id";
            var dy = new DynamicParameters();
            dy.Add("FacilitatorCode", entity.FacilitatorCode);
            dy.Add("FacilitatorName", entity.FacilitatorName);
            dy.Add("id", entity.Id);
            return DbHelper<ElderEntity>.GetInstance().Update(strSQL, dy) > 0;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="facilitator"></param>
        /// <param name="name"></param>
        /// <param name="number"></param>
        /// <param name="HomePhone"></param>
        /// <param name="Sex"></param>
        /// <param name="s_time"></param>
        /// <param name="e_time"></param>
        /// <param name="orderBy"></param>
        /// <param name="count"></param>
        /// <param name="orderDir"></param>
        /// <returns></returns>
        public List<ElderEntity> GetWithPages(int start, int length, string facilitator, string name, string number, string HomePhone, int Sex, DateTime s_time, DateTime e_time,
                                                                                                  string orderBy, out int count, string orderDir = "desc")
        {
            var dy = new DynamicParameters();
            if (facilitator != "F-00001")
            {
                strWhere = "FacilitatorCode=@FacilitatorCode and IsDeleted=@IsDeleted";
                dy.Add("FacilitatorCode", facilitator);
                dy.Add("IsDeleted", false);
            }
            else
            {
                strWhere = " IsDeleted=@IsDeleted";
                dy.Add("IsDeleted", false);
            }
           
            
           
            if (!StringExtension.IsBlank(name))
            {
                strWhere += " and(name like @name)";
                dy.Add("name", "%" + name + "%");
            }
            if (!StringExtension.IsBlank(number))
            {
                strWhere += " and(number like @number)";
                dy.Add("number", "%" + number + "%");
            }
            if (!StringExtension.IsBlank(HomePhone))
            {
                strWhere += " and(HomePhone like @HomePhone)";
                dy.Add("HomePhone", "%" + HomePhone + "%");
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
            return DbHelper<ElderEntity>.GetInstance().GetQueryManyForPage(data, dy, out count);
        }
        /// <summary>
        /// 获取系统总人数与每日新增人数
        /// </summary>
        /// <param name="facilitator"></param>
        /// <returns></returns>
        public ElderCountEntity GetElderCount(string facilitator)
        {
            string strSQL = string.Empty;
            var dy = new DynamicParameters();
            if (facilitator != "F-00001")
            {
                strSQL = @"select count(id) as sumCount,(select count(id) from Elder where FacilitatorCode=@FacilitatorCode and IsDeleted=@IsDeleted and CreateDateTime>= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ) newCount from Elder where FacilitatorCode=@FacilitatorCode and IsDeleted=@IsDeleted";
                dy.Add("FacilitatorCode", facilitator);
            }
            else
            {
                strSQL = @"select count(id) as sumCount,(select count(id) from Elder where IsDeleted=@IsDeleted and CreateDateTime>= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ) newCount from Elder where  IsDeleted=@IsDeleted";
            }
            dy.Add("IsDeleted", false);
            return DbHelper<ElderCountEntity>.GetInstance().List(strSQL, dy).FirstOrDefault();
        }
        /// <summary>
        /// 获取各省总人数
        /// </summary>
        /// <param name="facilitator"></param>
        /// <returns></returns>
        public List<ProvinceNumberEntity> GetProvinceNumber(string facilitator,int level)
        {
            string strSQL = string.Empty;
            string whereSQL= string.Empty;
            string gropSQL = string.Empty;

            switch (level)
            {
                case 1:
                    strSQL = @"select Facilitator.province AS PlaceNamee,count(Elder.Id) as number from Elder 
                                                    left join Facilitator on Facilitator.code=Elder.FacilitatorCode ";
                    gropSQL = @" group by Facilitator.province";
                    break;
                case 2:
                    strSQL = @"select Facilitator.city AS PlaceNamee,count(Elder.Id) as number from Elder 
                                                    left join Facilitator on Facilitator.code=Elder.FacilitatorCode ";
                    gropSQL = @"  group by Facilitator.city";
                    break;
                case 3:
                    strSQL = @"select Facilitator.district AS PlaceNamee,count(Elder.Id) as number from Elder 
                                                    left join Facilitator on Facilitator.code=Elder.FacilitatorCode ";
                    gropSQL = @" group by Facilitator.district";
                    break;

            }
            var dy = new DynamicParameters();
            if (facilitator != "F-00001")
            {
                whereSQL = "  where Elder.FacilitatorCode=@FacilitatorCode  ";
                dy.Add("FacilitatorCode", facilitator);
            }
            strSQL = strSQL + whereSQL + gropSQL;
            return DbHelper<ProvinceNumberEntity>.GetInstance().List(strSQL, dy);
        }
        /// <summary>
        /// 在线各省人数
        /// </summary>
        /// <param name="facilitator"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public List<ProvinceNumberEntity> GetOnlineProvinceNumber(string facilitator, int level)
        {
            string strSQL = string.Empty;
            string whereSQL = string.Empty;
            string gropSQL = string.Empty;

            switch (level)
            {
                case 1:
                    strSQL = @"select Facilitator.province AS PlaceNamee,count(Elder.Id) as number from Elder 
                                    left join Facilitator on Facilitator.code=Elder.FacilitatorCode
                                    left join DeviceBinding on DeviceBinding.ElderId=Elder.Id
                                    left join DeviceState on DeviceState.DevID=DeviceBinding.DeviceId";                                 
                    gropSQL = @" group by Facilitator.province";
                    break;
                case 2:
                    strSQL = @"select Facilitator.city AS PlaceNamee,count(Elder.Id) as number from Elder 
                                    left join Facilitator on Facilitator.code=Elder.FacilitatorCode
                                    left join DeviceBinding on DeviceBinding.ElderId=Elder.Id
                                    left join DeviceState on DeviceState.DevID=DeviceBinding.DeviceId ";
                    gropSQL = @"  group by Facilitator.city";
                    break;
                case 3:
                    strSQL = @"select Facilitator.district AS PlaceNamee,count(Elder.Id) as number from Elder 
                                    left join Facilitator on Facilitator.code=Elder.FacilitatorCode
                                    left join DeviceBinding on DeviceBinding.ElderId=Elder.Id
                                    left join DeviceState on DeviceState.DevID=DeviceBinding.DeviceId ";
                    gropSQL = @" group by Facilitator.district";
                    break;

            }
            var dy = new DynamicParameters();

            whereSQL = " where  DeviceState.IsOnline = 1 ";
            if (facilitator != "F-00001")
            {
                whereSQL = "  and  Elder.FacilitatorCode=@FacilitatorCode ";
                dy.Add("FacilitatorCode", facilitator);
            }

            strSQL = strSQL + whereSQL + gropSQL;
            return DbHelper<ProvinceNumberEntity>.GetInstance().List(strSQL, dy);
        }



        public List<ElderEntity> Query(string f_code,string name=null,string phone=null)
        {
            var dy = new DynamicParameters();
            if(f_code!="F-00001")
            {
                strWhere = "FacilitatorCode=@FacilitatorCode";
                dy.Add("FacilitatorCode", f_code);
            }          
            if (!StringExtension.IsBlank(name))
            {
                strWhere += " and(name like @name)";
                dy.Add("name", "%" + name + "%");
            }
            if (!StringExtension.IsBlank(phone))
            {
                strWhere += " and(homephone like @homephone)";
                dy.Add("homephone", "%" + phone + "%");
            }
            return DbHelper<ElderEntity>.GetInstance().List(columnName, tableName, strWhere, dy);

        }

        public List<AgeGroupEntity> AgeGroup(string facilitator)
        {
            string strSQL = string.Empty;
            var dy = new DynamicParameters();
            if (facilitator != "F-00001")
            {
                strSQL = @"SELECT '60-70年龄段' Age, COUNT(*) Number FROM Elder 
                                        right join DeviceBinding on DeviceBinding.ElderId=Elder.Id
                                        left join deviceAllot on deviceAllot.deviceID=DeviceBinding.DeviceId
                                        WHERE deviceAllot.facilitator=@FacilitatorCode and   FLOOR(datediff(DY,birthday,getdate())/365.25) BETWEEN 60 AND 70
                         UNION ALL
                              SELECT '70-75年龄段' Age, COUNT(*) Number FROM Elder
                                        right join DeviceBinding on DeviceBinding.ElderId=Elder.Id
                                        left join deviceAllot on deviceAllot.deviceID=DeviceBinding.DeviceId
                                        WHERE deviceAllot.facilitator=@FacilitatorCode and FLOOR(datediff(DY,birthday,getdate())/365.25) BETWEEN 71 AND 75
                        UNION ALL
                              SELECT '75-80年龄段' Age, COUNT(*) Number FROM Elder 
                                        right join DeviceBinding on DeviceBinding.ElderId=Elder.Id
                                        left join deviceAllot on deviceAllot.deviceID=DeviceBinding.DeviceId
                                        WHERE deviceAllot.facilitator=@FacilitatorCode and FLOOR(datediff(DY,birthday,getdate())/365.25) BETWEEN 76 AND 80
                        UNION ALL
                              SELECT '80以上' Age, COUNT(*) Number FROM Elder 
                                        right join DeviceBinding on DeviceBinding.ElderId=Elder.Id
                                        left join deviceAllot on deviceAllot.deviceID=DeviceBinding.DeviceId
                                        WHERE deviceAllot.facilitator=@FacilitatorCode and FLOOR(datediff(DY,birthday,getdate())/365.25) BETWEEN 81 AND 150";

                dy.Add("FacilitatorCode", facilitator);
            }
            else
            {
                strSQL = @"SELECT '60以下' Age, COUNT(*) Number FROM Elder
                                        right join DeviceBinding on DeviceBinding.ElderId=Elder.Id
                                        left join deviceAllot on deviceAllot.deviceID=DeviceBinding.DeviceId
                                        WHERE  FLOOR(datediff(DY,birthday,getdate())/365.25) BETWEEN 0 AND 59
                        UNION ALL
                                SELECT '60-70年龄段' Age, COUNT(*) Number FROM Elder 
                                        right join DeviceBinding on DeviceBinding.ElderId=Elder.Id
                                        left join deviceAllot on deviceAllot.deviceID=DeviceBinding.DeviceId
                                        WHERE   FLOOR(datediff(DY,birthday,getdate())/365.25) BETWEEN 60 AND 70
                         UNION ALL
                              SELECT '70-75年龄段' Age, COUNT(*) Number FROM Elder
                                        right join DeviceBinding on DeviceBinding.ElderId=Elder.Id
                                        left join deviceAllot on deviceAllot.deviceID=DeviceBinding.DeviceId
                                        WHERE  FLOOR(datediff(DY,birthday,getdate())/365.25) BETWEEN 71 AND 75
                        UNION ALL
                              SELECT '75-80年龄段' Age, COUNT(*) Number FROM Elder 
                                        right join DeviceBinding on DeviceBinding.ElderId=Elder.Id
                                        left join deviceAllot on deviceAllot.deviceID=DeviceBinding.DeviceId
                                        WHERE  FLOOR(datediff(DY,birthday,getdate())/365.25) BETWEEN 76 AND 80
                        UNION ALL
                              SELECT '80以上' Age, COUNT(*) Number FROM Elder 
                                        right join DeviceBinding on DeviceBinding.ElderId=Elder.Id
                                        left join deviceAllot on deviceAllot.deviceID=DeviceBinding.DeviceId
                                        WHERE  FLOOR(datediff(DY,birthday,getdate())/365.25) BETWEEN 81 AND 150";
            }
            
               
                
            return DbHelper<AgeGroupEntity>.GetInstance().List(strSQL, dy);
        }

        /// <summary>
        /// 获取老人位置信息
        /// </summary>
        /// <param name="facilitator"></param>
        /// <returns></returns>
        public List<MapPositionEntity> MapPosition(string facilitator,string province)
        {
            
            string strSQL = @"select Elder.id,Facilitator.name as FacilitatorName,homeaddress,MapPoint,
                                        case  when onbed.LeaveBedTime is null then 0 else 1 end flag
                                         from Elder (nolock)
                                        left join (
                                        select productid,OnBedTime,LeaveBedTime,OnBedDetectTime  from [OnBedRecord] a
                                        where exists
                                        (
	                                        select 1 from (select  productid,max(OnBedTime) as OnBedTime from OnBedRecord group by productid) as  x
	                                        where x.productid=a.productid and x.OnBedTime=a.OnBedTime 
                                        )) as onbed  on onbed.productid=Elder.id 
                                        left join Facilitator  on  Facilitator.code= Elder.FacilitatorCode where  ";
            var dy = new DynamicParameters();
            if (facilitator!="F-00001")
            {
                strWhere += " and Elder.FacilitatorCode=@FacilitatorCode";
                dy.Add("FacilitatorCode", facilitator);
            }
            if (!StringExtension.IsBlank(province))
            {
                strWhere += " and  Facilitator.province like '%"+ province.Substring(0,2) + "%' ";
            }
           
            return DbHelper<MapPositionEntity>.GetInstance().List(strSQL+ strWhere, dy);
        }

        public ElderDeviceInfoEntity GetElderDeviceInfo(string facilitator,int elderId)
        {
            string Str_sql = @"select ElderId,name as ElderName,DeviceId,device_number as DeviceNumber from DeviceBinding
                                        left join Elder on Elder.id=DeviceBinding.ElderId
                                        left join Device on Device.id=DeviceBinding.DeviceId
                                        where ElderId=@ElderId ";
            var dy = new DynamicParameters();
            dy.Add("ElderId", elderId);
            if (facilitator != "F-00001")
            {
                Str_sql += " and Elder.FacilitatorCode=@FacilitatorCode";
                dy.Add("FacilitatorCode", facilitator);
            }
            return DbHelper<ElderDeviceInfoEntity>.GetInstance().List(Str_sql, dy).FirstOrDefault();
        }
        public List<ElderEntity> AnalysisQuery()
        {
            strWhere = " IsDeleted=@IsDeleted"; 
            var dy = new DynamicParameters();
            dy.Add("IsDeleted", false);
            return DbHelper<ElderEntity>.GetInstance().List(columnName, tableName, strWhere, dy);
        }

        public List<ElderEntity> ElderQuery(string f_code, string queryValue)
        {
            var dy = new DynamicParameters();
            if (f_code != "F-00001")
            {
                strWhere = "FacilitatorCode=@FacilitatorCode";
                dy.Add("FacilitatorCode", f_code);
            }
            if (!StringExtension.IsBlank(queryValue))
            {
                strWhere += " and(name like @name or homephone like @homephone)";
                dy.Add("name", "%" + queryValue + "%");
                dy.Add("homephone", "%" + queryValue + "%");
            }
            return DbHelper<ElderEntity>.GetInstance().List(columnName, tableName, strWhere, dy);

        }


        public List<ElderListEntity> ElderListQuery(string f_code, string queryValue=null)
        {
            string StrSQL = @"select elder.id,name,birthday,sex,homephone,homeaddress,MapPoint,DeviceId, 
                                        CASE WHEN avHR IS NULL THEN 0 ELSE avHR END as avHR ,
                                        CASE WHEN IsBed IS NULL THEN 0 ELSE IsBed END as IsBed , 
                                        CASE WHEN IsOnline IS NULL THEN 0 ELSE IsOnline END as IsOnline   
                                        from elder  
                                        left join DeviceBinding on elder.Id=DeviceBinding.ElderId 
                                        left join DeviceState on DeviceBinding.DeviceId=DeviceState.DevID 
                                        ";
            string strOrder = " order by IsBed desc ";
            var dy = new DynamicParameters();
            strWhere = " where  1=1";
            if (f_code != "F-00001")
            {
                strWhere += " and Elder.FacilitatorCode= @FacilitatorCode";
                dy.Add("FacilitatorCode", f_code);
            }
            if (!StringExtension.IsBlank(queryValue))
            {
                strWhere += " and(name like @name or homephone like @homephone)";
                dy.Add("name", "%" + queryValue + "%");
                dy.Add("homephone", "%" + queryValue + "%");
            }
            StrSQL = StrSQL + strWhere + strOrder;
            return DbHelper<ElderListEntity>.GetInstance().List(StrSQL, dy);
        }

    }
}
