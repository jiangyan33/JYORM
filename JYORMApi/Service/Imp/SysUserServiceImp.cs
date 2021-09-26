using JYORMApi.Dao;
using JYORMApi.Entity;
using JYORMApi.Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JYORMApi.Service.Imp
{
    public class SysUserServiceImp : ISysUserService
    {
        private readonly DefaultDBClient _DB;

        private readonly SysUserMapper<SysUser> _sysUserMapper;
        public SysUserServiceImp(DefaultDBClient DB, SysUserMapper<SysUser> sysUserMapper)
        {
            _DB = DB;
            _sysUserMapper = sysUserMapper;
        }

        public async Task<int> AddOne(SysUser sysUser)
        {
            sysUser.UserName = "江岩";
            sysUser.Password = "123456";
            sysUser.CreateBy = 1;
            sysUser.UpdateBy = 1;
            return await _DB.GetInstance.Insertable(sysUser).IgnoreColumns(ignoreNullColumn: true).ExecuteCommandAsync();
        }

        public async Task<List<SysUser>> List()
        {
            return await _sysUserMapper.GetByName();
        }

    }
}
