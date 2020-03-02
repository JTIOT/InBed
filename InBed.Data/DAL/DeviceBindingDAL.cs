using InBed.Core.Extentions;
using InBed.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Data.DAL
{
    public class DeviceBindingDAL
    {
        #region 常量参数
        private readonly string columnName = @"id,ElderId,DeviceId,IsDeleted,crea_time,creater,modify_time,modifier";
        private readonly string tableName = "DeviceBinding";
        private string strWhere = "1=1";
        #endregion
        public bool AddDeviceBinding(DeviceBindingEntity entity)
        {
            string strSQL = @"insert into DeviceBinding(ElderId,DeviceId,IsDeleted,crea_time,creater)
                                            values(@ElderId,@DeviceId,@IsDeleted,@crea_time,@creater)";
            return DbHelper<DeviceBindingEntity>.GetInstance().Insert(strSQL, entity) > 0;
        }
        /// <summary>
        /// 删除多条用户信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DelBinding(List<int> ids)
        {
            strWhere = "id in (@id)";
            var dy = new DynamicParameters();
            dy.Add("id", string.Join(",", ids));
            return DbHelper<DeviceBindingEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        /// <summary>
        /// 删除单条用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelBinding(int id)
        {
            strWhere = "id =@id";
            var dy = new DynamicParameters();
            dy.Add("id", id);
            return DbHelper<DeviceBindingEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool EditBinding(DeviceBindingEntity entity)
        {
            string strSQL = @"update DeviceBinding set ElderId=@ElderId,DeviceId=@DeviceId,Modifier=@Modifier,ModifyTime=@ModifyTime where id=@id";
            var dy = new DynamicParameters();
            dy.Add("ElderId", entity.ElderId);
            dy.Add("DeviceId", entity.DeviceId);
            dy.Add("ModifyTime", DateTime.Now);
            dy.Add("Modifier", entity.Modifier);
            dy.Add("id", entity.Id);
            return DbHelper<DeviceBindingEntity>.GetInstance().Update(strSQL, dy) > 0;
        }
        public List<BindingEntity> GetWithPages(int start, int length, string f_code, string name, string phone, string d_number, int d_type,
                                                                                            string orderBy, string orderDir = "desc")
        {

            string StrSQL = @"select Elder.id,Name,birthday,sex,homephone,device_number as number ,device_type as type  ,device_model as model,status from Elder
                                                            right join DeviceBinding on DeviceBinding.ElderId=Elder.Id
                                                            left join Device on Device.id=DeviceBinding.DeviceId";

            var dy = new DynamicParameters();
            strWhere = " where  1=1";
           if (f_code != "F-00001")
            {
                strWhere += " and Elder.FacilitatorCode= @FacilitatorCode";
                dy.Add("FacilitatorCode", f_code);
            }
            if (!StringExtension.IsBlank(name))
            {
                strWhere += " and Elder.name like @name";
                dy.Add("name", "%" + name + "%");
            }
            if (!StringExtension.IsBlank(phone))
            {
                strWhere += " and Elder.homephone like @homephone";
                dy.Add("homephone", "%" + phone + "%");
            }
            if (!StringExtension.IsBlank(d_number))
            {
                strWhere += " and Device.device_number like @device_number";
                dy.Add("device_number", "%" + d_number + "%");
            }
            if (d_type>0)
            {
                strWhere += " and Device.device_type =@d_type";
                dy.Add("d_type", d_type);
            }
            StrSQL = StrSQL + strWhere;


          //  return DbHelper<BindingEntity>.GetInstance().GetQueryManyForPage(data, dy, out count);
            return DbHelper<BindingEntity>.GetInstance().List(StrSQL, dy);
        }

        public List<DeviceBindingEntity> Query(List<int> ids)
        {
            var dy = new DynamicParameters();
            strWhere = "ElderId in (" + string.Join(",", ids) + ")";
       
            return DbHelper<DeviceBindingEntity>.GetInstance().List(columnName, tableName, strWhere, dy);
        }

    }
}
