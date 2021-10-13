using JYORMApi.Entity;
using JYORMApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JYORMApi.Service
{
    public interface ISysUserService
    {
        public Task<List<SysUser>> List();

        public Task<SysUser> Get(long id);

        public Task<List<SysUser>> Get(SysUser sysUser);

        public Task<PageResult<SysUser>> GetPages(SysUser sysUser);

        public Task<int> Add(SysUser sysUser);
    }
}
