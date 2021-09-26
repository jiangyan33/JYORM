using MyCommonTool.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JYORMApi.Entity;
using JYORMApi.Persistence;

namespace JYORMApi.Dao
{
    public class UserDao
    {
        private readonly MySqlDB _mysqlDB;

        //public UserDao(MySqlDB mysqlDB)
        //{
        //    _mysqlDB = mysqlDB;
        //}

        public async Task<User> Find(User user)
        {
            var sql = $"select * from user where name='{user.Name}' and password ='{user.Password}' and status=1";

            var result = await _mysqlDB.GetEntityAsync<User>(sql);
            return result.FirstOrDefault();
        }

        public async Task<User> FindOne(User user)
        {
            var sql = $"select * from user where id='{user.Id}' and status=1";

            var result = await _mysqlDB.GetEntityAsync<User>(sql);
            return result.FirstOrDefault();
        }

        public async Task<int> Update(User user)
        {
            var sql = @$"update user set
                                name=@Name,
                                email=@Email,
                                password=@Password,
                                birthday=@Birthday,
                                sex=@Sex,
                                telephone=@Telephone,
                                description=@Description,
                                create_date=@CreateDate,
                                update_date=@UpdateDate,
                                last_login_time=@LastLoginTime,
                                update_token_time=@UpdateTokenTime,
                                status=@Status
                              Where id=@Id";

            return (int)await _mysqlDB.ExecuteAsync(sql, CommonUtils.ConvertObjToKeyPairObject(user));
        }

        public async Task<List<User>> Test()
        {
            var sql = $"select * from user";

            return await _mysqlDB.GetEntityAsync<User>(sql);
        }
    }
}