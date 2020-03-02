using InBed.Core.Extentions;
using InBed.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Data.DAL
{
    public class UserDAL
    {
        #region 常量参数
        private readonly string columnName = @"Id,FacilitatorName,FacilitatorCode,LoginName,RealName,Email,Password,Status,Creater,CreateDateTime,IsDeleted,ModifyTime,Modifier";
        private readonly string tableName = "users";
        private string strWhere = "1=1";
        #endregion
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool AddUser(UserEntity entity)
        {
            string strSQL = @"insert into users(FacilitatorName,FacilitatorCode,LoginName,RealName,Email,Password,Status,Creater,CreateDateTime,IsDeleted)
                                            values(@FacilitatorName,@FacilitatorCode,@LoginName,@RealName,@Email,@Password,@Status,@Creater,@CreateDateTime,@IsDeleted)";
            return DbHelper<UserEntity>.GetInstance().Insert(strSQL, entity) > 0;
        }
        /// <summary>
        /// 用户修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool EditUser(UserEntity entity)
        {
            string strSQL = @"update users set Email=@Email,RealName=@RealName,Status=@Status,Modifier=@Modifier,ModifyTime=@ModifyTime where id=@id";
            var dy = new DynamicParameters();
            dy.Add("Email", entity.Email);
            dy.Add("RealName", entity.RealName);
            dy.Add("Status", entity.Status);
            dy.Add("ModifyTime", DateTime.Now);
            dy.Add("Modifier", entity.Modifier);
            dy.Add("id", entity.Id);
            return DbHelper<UserEntity>.GetInstance().Update(strSQL, dy) > 0;
        }
        /// <summary>
        /// 删除多条用户信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DelUser(List<int> ids)
        {
            strWhere = "id in (@id)";
            var dy = new DynamicParameters();
            dy.Add("id", string.Join(",", ids));
            return DbHelper<UserEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        /// <summary>
        /// 删除单条用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelUser(int id)
        {
            strWhere = "id =@id";
            var dy = new DynamicParameters();
            dy.Add("id", id);
            return DbHelper<UserEntity>.GetInstance().Delete(tableName, strWhere, dy) > 0;
        }
        /// <summary>
        /// 用户详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserEntity Details(int id)
        {
            strWhere = "id=@id";
            var dy = new DynamicParameters();
            dy.Add("id", id);
            return DbHelper<UserEntity>.GetInstance().GetModel(tableName, columnName, strWhere, dy);
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="creater"></param>
        /// <param name="name"></param>
        /// <param name="orderBy"></param>
        /// <param name="count"></param>
        /// <param name="orderDir"></param>
        /// <returns></returns>
        public List<UserEntity> GetWithPages(int start, int length, string facilitator, string loginName, string realname,DateTime s_time,DateTime e_time,
                                                                                                            string orderBy, out int count, string orderDir = "desc")
        {
            strWhere = "IsDeleted=@IsDeleted";
            var dy = new DynamicParameters();
            if (facilitator != "F-00001")
            {
                strWhere += " and FacilitatorCode=@FacilitatorCode ";
                dy.Add("FacilitatorCode", facilitator);
            }
                
            if (!StringExtension.IsBlank(loginName))
            {
                strWhere += " and(LoginName like @LoginName)";
                dy.Add("LoginName", "%" + loginName + "%");
            }
            if (!StringExtension.IsBlank(realname))
            {
                strWhere += " and(RealName like @RealName)";
                dy.Add("RealName", "%" + realname + "%");
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
            return DbHelper<UserEntity>.GetInstance().GetQueryManyForPage(data, dy, out count);
        }
        /// <summary>
        /// 获取用户信息（根据登录名获取）
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public UserEntity GetUser(string loginName)
        {
            strWhere = "LoginName=@LoginName";
            var dy = new DynamicParameters();
            dy.Add("LoginName", loginName);
            return DbHelper<UserEntity>.GetInstance().GetModel(tableName, columnName, strWhere, dy);
        }

    }


}
