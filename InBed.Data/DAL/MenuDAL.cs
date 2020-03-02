using InBed.Core.Extentions;
using InBed.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Data.DAL
{
   public class MenuDAL
    {
        #region 常量参数
        private readonly string columnName = @"Id,ParentId,Type,Name,Url,[Order],Creater,CreateDateTime,IsDeleted,ModifyTime,Modifier,Icon";
        private readonly string tableName = "Menu";
        private string strWhere = "1=1";
        #endregion
        public bool AddMenu(MenuEntity entity)
        {
            string strSQL = @"insert into Menu(ParentId,Type,Name,Url,[Order],Creater,CreateDateTime,IsDeleted,Icon)
                                            values(@ParentId,@Type,@Name,@Url,@[Order],@Creater,@CreateDateTime,@IsDeleted,@Icon)";
            return DbHelper<MenuEntity>.GetInstance().Insert(strSQL, entity) > 0;
        }
        /// <summary>
        /// 删除多条用户信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DelMenu(List<int> ids)
        {
            strWhere = "id in ("+ string.Join(",", ids) + ")";
            var dy = new DynamicParameters();
            return DbHelper<MenuEntity>.GetInstance().Delete(tableName, strWhere ,dy) > 0;
        }
        /// <summary>
        /// 删除单条用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelMenu(int id)
        {
            strWhere = "id =@id";
            var dy = new DynamicParameters();
            dy.Add("id", id);
            return DbHelper<MenuEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        public bool EditMenu(MenuEntity entity)
        {
            string strSQL = @"update Menu set ParentId=@ParentId,Type=@Type,Name=@Name,Url=@Url,[Order]=@Order,Modifier=@Modifier,ModifyTime=@ModifyTime where id=@id";
            var dy = new DynamicParameters();
            dy.Add("ParentId", entity.Order);
            dy.Add("Type", entity.Order);
            dy.Add("Name", entity.Order);
            dy.Add("Url", entity.Order);
            dy.Add("Order", entity.Order);
            dy.Add("ModifyTime", DateTime.Now);
            dy.Add("Modifier", entity.Modifier);
            dy.Add("id", entity.Id);
            return DbHelper<MenuEntity>.GetInstance().Update(strSQL, dy) > 0;
        }
        public MenuEntity Details(int id)
        {
            strWhere = "id=@id";
            var dy = new DynamicParameters();
            dy.Add("id", id);
            return DbHelper<MenuEntity>.GetInstance().GetModel(tableName, columnName, strWhere, dy);
        }
        public List<MenuEntity> GetWithPages(int start, int length,int Pid, int type, string name,DateTime s_time, DateTime e_time,
                                                                                                    string orderBy, out int count, string orderDir = "desc")
        {
            var dy = new DynamicParameters();
            if (!StringExtension.IsBlank(name))
            {
                strWhere += " and(Name like @Name)";
                dy.Add("Name", "%" + name + "%");
            }
            if (Pid>0)
            {
                strWhere += " and(ParentId = @ParentId)";
                dy.Add("ParentId", Pid);
            }
            if (type > 0)
            {
                strWhere += " and(Type = @Type)";
                dy.Add("Type", type);
            }
            if (s_time > DateTime.MinValue)
            {
                strWhere += " and CreateDateTime >=@s_time ";
                dy.Add("s_time", s_time);
            }
            if (e_time > DateTime.MinValue)
            {
                strWhere += " and CreateDateTime<=@e_time)";
                dy.Add("e_time", e_time);
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
            return DbHelper<MenuEntity>.GetInstance().GetQueryManyForPage(data, dy, out count);
        }

        public List<MenuEntity> Query(List<int> menuIds)
        {
            strWhere = "id in (" + string.Join(",", menuIds) + ")";
            var dy = new DynamicParameters();
            return DbHelper<MenuEntity>.GetInstance().List(columnName, tableName, strWhere, dy);

        }
        public List<MenuEntity> Query(int type)
        {
            strWhere = "Type =@Type";
            var dy = new DynamicParameters();
            dy.Add("Type", type);
            return DbHelper<MenuEntity>.GetInstance().List(columnName, tableName, strWhere, dy);

        }



    }
}
