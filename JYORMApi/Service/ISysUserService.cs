using JYORMApi.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JYORMApi.Service
{
    public interface ISysUserService
    {
        public Task<int> AddOne(SysUser sysUser);

        public Task<List<SysUser>> List();
    }
}
