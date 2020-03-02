using InBed.Core.Extentions;
using InBed.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Data.DAL
{
   public class FacilitatorDAL
    {
        #region 常量参数
        private readonly string columnName = @"id,code,name,number,contacts,province,city,district,address,capital,management,up_id,Creater,CreateDateTime,IsDeleted,ModifyTime,Modifier,F_Level";
        private readonly string tableName = "Facilitator";
        private string strWhere = "1=1";
        #endregion
        public bool AddFacilitator(FacilitatorEntity entity)
        {
            string strSQL = @"insert into Facilitator(code,name,number,contacts,province,city,district,address,capital,management,up_id,Creater,CreateDateTime,IsDeleted,F_Level)
                                            values(@code,@name,@number,@contacts,@province,@city,@district,@address,@capital,@management,@up_id,@Creater,@CreateDateTime,@IsDeleted,@F_Level)";
            return DbHelper<FacilitatorEntity>.GetInstance().Insert(strSQL, entity) > 0;
        }

        /// <summary>
        /// 删除多条用户信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DelFacilitator(List<int> ids)
        {
            strWhere = "id in (@id)";
            var dy = new DynamicParameters();
            dy.Add("id", string.Join(",", ids));
            return DbHelper<FacilitatorEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        /// <summary>
        /// 删除单条用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelFacilitator(int id)
        {
            strWhere = "id =@id";
            var dy = new DynamicParameters();
            dy.Add("id", id);
            return DbHelper<FacilitatorEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }

        public List<FacilitatorEntity> IsDelFacilitator(int id)
        {
            strWhere = "up_id=@id ";
            var dy = new DynamicParameters();
            dy.Add("id", id);
            return DbHelper<FacilitatorEntity>.GetInstance().List(columnName, tableName, strWhere, dy);           
        }
        public List<FacilitatorEntity> GetlowerFacilitator(int id)
        {
            var dy = new DynamicParameters();
            if (id!=0)
            {

                strWhere = "up_id=@id ";
                dy.Add("id", id);
            }

            return DbHelper<FacilitatorEntity>.GetInstance().List(columnName, tableName, strWhere, dy);
        }
        public FacilitatorEntity Details(int id)
        {
            strWhere = "id=@id";
            var dy = new DynamicParameters();
            dy.Add("id", id);
            return DbHelper<FacilitatorEntity>.GetInstance().GetModel(tableName, columnName, strWhere, dy);
        }
        public bool EditFacilitator(FacilitatorEntity entity)
        {
            string strSQL = @"update Facilitator set name=@name,number=@number,address=@address,Modifier=@Modifier,ModifyTime=@ModifyTime,F_Level=@F_Level where id=@id";
            var dy = new DynamicParameters();
            dy.Add("name", entity.name);
            dy.Add("number", entity.number);
            dy.Add("address", entity.address);
            dy.Add("ModifyTime", DateTime.Now);
            dy.Add("Modifier", entity.Modifier);
            dy.Add("id", entity.Id);
            dy.Add("F_Level", entity.F_Level);
            return DbHelper<FacilitatorEntity>.GetInstance().Update(strSQL, dy) > 0;
        }
        public FacilitatorEntity Query(string facilitator)
        {
            strWhere = "Code=@Code";
            var dy = new DynamicParameters();
            dy.Add("Code", facilitator);
            return DbHelper<FacilitatorEntity>.GetInstance().GetModel(tableName, columnName, strWhere, dy);
        }
        public List<FacilitatorEntity> GetWithPages(int start, int length, int f_id, string Name, string Number,string adders, DateTime s_time, DateTime e_time,
                                                                                                                  string orderBy, out int count, string orderDir = "desc")
        {
            strWhere = "up_id=@f_id and IsDeleted=@IsDeleted";
            var dy = new DynamicParameters();
            if (!StringExtension.IsBlank(Name))
            {
                strWhere += " and(Name like @Name)";
                dy.Add("Name", "%" + Name + "%");
            }
            if (!StringExtension.IsBlank(adders))
            {
                strWhere += " and(address like @address)";
                dy.Add("address", "%" + adders + "%");
            }
            if (!StringExtension.IsBlank(Number))
            {
                strWhere += " and(Number like @Number)";
                dy.Add("Number", "%" + Number + "%");
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
            dy.Add("f_id", f_id);
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
            return DbHelper<FacilitatorEntity>.GetInstance().GetQueryManyForPage(data, dy, out count);
        }

    }
}

