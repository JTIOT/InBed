using InBed.Entity;
using System.Collections.Generic;

namespace InBed.Data.DAL
{
    public class RoleMenuDAL
    {

        #region 常量参数
        private readonly string columnName = @"Id,RoleId,MenuId,Creater,CreateDateTime,IsDeleted,ModifyTime,Modifier";
        private readonly string tableName = "RoleMenu";
        private string strWhere = "1=1";
        #endregion
        public bool AddRoleMenu(RoleMenuEntity entity)
        {
            string strSQL = @"insert into RoleMenu(RoleId,MenuId,Creater,CreateDateTime,IsDeleted)
                                            values(@RoleId,@MenuId,@Creater,@CreateDateTime,@IsDeleted)";
            return DbHelper<RoleMenuEntity>.GetInstance().Insert(strSQL, entity) > 0;
        }
        /// <summary>
        /// 删除多条用户信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DelRoleMenu(List<int> ids)
        {
            strWhere = "RoleId in (@RoleId)";
            var dy = new DynamicParameters();
            dy.Add("RoleId", string.Join(",", ids));
            return DbHelper<RoleMenuEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        /// <summary>
        /// 删除单条用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelRoleMenu(int id)
        {
            strWhere = "RoleId =@RoleId";
            var dy = new DynamicParameters();
            dy.Add("RoleId", id);
            return DbHelper<RoleMenuEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }

        public List<RoleMenuEntity> Query(int r_id)
        {
            strWhere = "RoleId = @RoleId";
            var dy = new DynamicParameters();
            dy.Add("RoleId",  r_id);
            return DbHelper<RoleMenuEntity>.GetInstance().List(columnName,tableName, strWhere, dy);
        }


    }
}