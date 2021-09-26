using JYORMApi.Persistence;
using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JYORMApi.Dao
{
    public class SysUserMapper<SysUser>
    {
        private readonly IAdo Ado;

        public SysUserMapper(DefaultDBClient client)
        {
            Ado = client.GetInstance.Ado;
        }

        public async Task<List<SysUser>> GetByName()
        {
            var sql = "select * from sys_user where user_name=@Name";
            var result = await Ado.SqlQueryAsync<SysUser>(sql, new { Name = "江岩" });
            return result;
        }
    }
}
