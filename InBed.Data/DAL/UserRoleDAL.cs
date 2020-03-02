using InBed.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Data.DAL
{
   public class UserRoleDAL
    {
        #region 常量参数
        private readonly string columnName = @"Id,UserId,RoleId,Creater,CreateDateTime,IsDeleted,ModifyTime,Modifier";
        private readonly string tableName = "UserRole";
        private string strWhere = "1=1";
        #endregion
        public bool AddUserRole(UserRoleEntity entity)
        {
            string strSQL = @"insert into UserRole(UserId,RoleId,Creater,CreateDateTime,IsDeleted)
                                            values(@UserId,@RoleId,@Creater,@CreateDateTime,@IsDeleted)";
            return DbHelper<UserRoleEntity>.GetInstance().Insert(strSQL, entity) > 0;
        }
        public bool EditUserRole(UserRoleEntity entity)
        {
            string strSQL = @"update UserRole set UserId=@UserId,RoleId=@RoleId,IsDeleted=@IsDeleted,Modifier=@Modifier,ModifyTime=@ModifyTime where id=@id";
            var dy = new DynamicParameters();
            dy.Add("UserId", entity.UserId);
            dy.Add("RoleId", entity.RoleId);
            dy.Add("IsDeleted", entity.IsDeleted);
            dy.Add("ModifyTime", DateTime.Now);
            dy.Add("Modifier", entity.Modifier);
            dy.Add("id", entity.Id);
            return DbHelper<UserRoleEntity>.GetInstance().Update(strSQL, dy) > 0;
        }
        public bool DelUserRole(List<int> ids)
        {
            strWhere = "id in (@id)";
            var dy = new DynamicParameters();
            dy.Add("id", string.Join(",", ids));
            return DbHelper<UserRoleEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        public bool DelUserRole(int id, int userID)
        {
            strWhere = "RoleId =@id AND UserId=@UserId";
            var dy = new DynamicParameters();
            dy.Add("id", id);
            dy.Add("UserId", userID);
            return DbHelper<UserRoleEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        public List<UserRoleEntity> GetUserRole(int userid)
        {
            strWhere = "userid =@userid";
            var dy = new DynamicParameters();
            dy.Add("userid", userid);
            return DbHelper<UserRoleEntity>.GetInstance().List(columnName, tableName, strWhere, dy);
        }
        public List<UserRoleEntity> Query(int userId)
        {
            strWhere = "UserId=@UserId and IsDeleted=@IsDeleted";
            var dy = new DynamicParameters();
            dy.Add("UserId", userId);
            dy.Add("IsDeleted", false);
            return DbHelper<UserRoleEntity>.GetInstance().List(columnName, tableName, strWhere, dy);
        }

    }
}
