using InBed.Core.Extentions;
using InBed.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Data.DAL
{
   public class RoleDAL
    {
        #region 常量参数
        private readonly string columnName = @"Id,Name,Description,Creater,CreateDateTime,IsDeleted,ModifyTime,Modifier,FacilitatorCode,FacilitatorName";
        private readonly string tableName = "Roles";
        private string strWhere = "1=1";
        #endregion
        public bool AddRole(RoleEntity entity)
        {
            string strSQL = @"insert into Roles(Name,Description,Creater,CreateDateTime,IsDeleted,FacilitatorCode,FacilitatorName)
                                            values(@Name,@Description,@Creater,@CreateDateTime,@IsDeleted,@FacilitatorCode,@FacilitatorName)";
            return DbHelper<RoleEntity>.GetInstance().Insert(strSQL, entity) > 0;
        }
        /// <summary>
        /// 删除多条用户信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DelRole(List<int> ids)
        {
            strWhere = "id in (@id)";
            var dy = new DynamicParameters();
            dy.Add("id", string.Join(",", ids));
            return DbHelper<RoleEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        /// <summary>
        /// 删除单条用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelRole(int id)
        {
            strWhere = "id =@id";
            var dy = new DynamicParameters();
            dy.Add("id", id);
            return DbHelper<RoleEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        public List<RoleEntity> Query(List<int> ids, string f_cde)
        {
            strWhere += " and facilitatorCode=@facilitatorCode ";
            if (ids.Count > 0)
            {
                strWhere += " and id in (" + string.Join(",", ids) + ")";

            }
            var dy = new DynamicParameters();
            dy.Add("facilitatorCode", f_cde);
            return DbHelper<RoleEntity>.GetInstance().List(columnName, tableName, strWhere, dy);
        }
        public bool EditRole(RoleEntity entity)
        {
            string strSQL = @"update Roles set Name=@Name,Description=@Description,Modifier=@Modifier,ModifyTime=@ModifyTime where id=@id";
            var dy = new DynamicParameters();
            dy.Add("Name", entity.Name);
            dy.Add("Description", entity.Description);
            dy.Add("ModifyTime", DateTime.Now);
            dy.Add("Modifier", entity.Modifier);
            dy.Add("id", entity.Id);
            return DbHelper<RoleEntity>.GetInstance().Update(strSQL, dy) > 0;
        }

        public RoleEntity Details(int id)
        {
            strWhere = "id=@id";
            var dy = new DynamicParameters();
            dy.Add("id", id);
            return DbHelper<RoleEntity>.GetInstance().GetModel(tableName, columnName, strWhere, dy);
        }
        public List<RoleEntity> GetWithPages(int start, int length, string name, string facilitatorCode, string description,
                                                                                                            string orderBy, out int count, string orderDir = "desc")
        {
            strWhere += " and facilitatorCode=@facilitatorCode ";
            var dy = new DynamicParameters();
            if (!StringExtension.IsBlank(name))
            {
                strWhere += " and(name like @name)";
                dy.Add("name", "%" + name + "%");
            }
            if (!StringExtension.IsBlank(description))
            {
                strWhere += " and description like @description ";
                dy.Add("description", "%" + description + "%");
            }
            dy.Add("facilitatorCode", facilitatorCode);
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
            return DbHelper<RoleEntity>.GetInstance().GetQueryManyForPage(data, dy, out count);
        }

    }

}
