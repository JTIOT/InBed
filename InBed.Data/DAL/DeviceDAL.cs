using InBed.Core.Extentions;
using InBed.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Data.DAL
{
   public class DeviceDAL
    {
        #region 常量参数
        private readonly string columnName = @"Id,Facilitator_Code,Facilitator_Name,device_number,device_type,device_model,is_enable,Creater,CreateDateTime,IsDeleted,ModifyTime,Modifier,status";
        private readonly string tableName = "Device";
        private string strWhere = "1=1";
        #endregion
        public bool AddDevice(DeviceEntity entity)
        {
            string strSQL = @"insert into Device(Facilitator_Code,Facilitator_Name,device_number,device_type,device_model,is_enable,Creater,CreateDateTime,IsDeleted,status)
                                            values(@Facilitator_Code,@Facilitator_Name,@device_number,@device_type,@device_model,@is_enable,@Creater,@CreateDateTime,@IsDeleted,@status)";
            return DbHelper<DeviceEntity>.GetInstance().Insert(strSQL, entity) > 0;
        }
        /// <summary>
        /// 删除多条设备信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DelDevice(List<int> ids)
        {
            strWhere = "id in ("+ string.Join(",", ids) + ")";
            var dy = new DynamicParameters();

            return DbHelper<DeviceEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        /// <summary>
        /// 删除单条设备
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelDevice(int id)
        {
            strWhere = "id =@id";
            var dy = new DynamicParameters();
            dy.Add("id", id);
            return DbHelper<DeviceEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        /// <summary>
        /// 修改设备信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool EditDevice(DeviceEntity entity)
        {
            string strSQL = @"update Device set device_type=@device_type,device_model=@device_model,is_enable=@is_enable,Modifier=@Modifier,ModifyTime=@ModifyTime where id=@id";
            var dy = new DynamicParameters();
            dy.Add("device_type", entity.device_type);
            dy.Add("device_model", entity.device_model);
            dy.Add("is_enable", entity.is_enable);
            dy.Add("ModifyTime", DateTime.Now);
            dy.Add("Modifier", entity.Modifier);
            dy.Add("id", entity.Id);
            return DbHelper<DeviceEntity>.GetInstance().Update(strSQL, dy) > 0;
        }
        public DeviceEntity Details(int id)
        {
            strWhere = "id=@id";
            var dy = new DynamicParameters();
            dy.Add("id", id);
            return DbHelper<DeviceEntity>.GetInstance().GetModel(tableName, columnName, strWhere, dy);
        }
        public DeviceEntity Query(string  code)
        {
            strWhere = "device_number=@code";
            var dy = new DynamicParameters();
            dy.Add("code", code);
            return DbHelper<DeviceEntity>.GetInstance().GetModel(tableName, columnName, strWhere, dy);
        }
        public List<DeviceEntity> GetWithPages(int start, int length,string facilitator, string number,string model, int type ,int status, DateTime s_time, DateTime e_time,
                                                                                                    string orderBy, out int count, string orderDir = "desc")
        {
            strWhere = " IsDeleted=@IsDeleted";
            var dy = new DynamicParameters();
            if (facilitator != "F-00001")
            {
                strWhere += "and  Facilitator_Code=@FacilitatorCode ";
                dy.Add("FacilitatorCode", facilitator);
            }
            
            if (!StringExtension.IsBlank(number))
            {
                strWhere += " and(device_number like @device_number)";
                dy.Add("device_number", "%" + number + "%");
            }
            if (!StringExtension.IsBlank(model))
            {
                strWhere += " and(device_model like @device_model)";
                dy.Add("device_model", "%" + model + "%");
            }
            if (type>0)
            {
                strWhere += " and(device_type=@device_type)";
                dy.Add("device_type", type);
            }
            if (status > -1)
            {
                strWhere += " and(status=@status)";
                dy.Add("status", status);
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
            return DbHelper<DeviceEntity>.GetInstance().GetQueryManyForPage(data, dy, out count);
        }


        public DeviceEntity GetElderDevice(int elderId)
        {
            string strSQL = @"select Device.* from Device (nolock)
                                        right join DeviceBinding (nolock) on  DeviceBinding.DeviceId=Device.id
                                        right join Elder (nolock) on Elder.id=DeviceBinding.ElderId
                                        where Elder.id=@elderId";
            var dy = new DynamicParameters();
            dy.Add("elderId", elderId);
            return DbHelper<DeviceEntity>.GetInstance().List(strSQL, dy).FirstOrDefault();

        }
    }
}
