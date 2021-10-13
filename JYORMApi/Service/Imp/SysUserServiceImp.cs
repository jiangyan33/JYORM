using JYORMApi.Mapper;
using JYORMApi.Entity;
using JYORMApi.Model;
using JYORMApi.Persistence;
using SqlSugar;
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

        #region 通用的操作
        public async Task<SysUser> Get(long id)
        {
            return await _DB.GetInstance.Queryable<SysUser>().SingleAsync(x => x.Id == id);
        }

        public async Task<List<SysUser>> Get(SysUser sysUser)
        {
            return await _DB.GetInstance.Queryable<SysUser>().Where(CreateExpression(sysUser).ToExpression())
                .OrderBy(x => x.CreateTime, OrderByType.Asc).ToListAsync();
        }

        public async Task<PageResult<SysUser>> GetPages(SysUser sysUser)
        {
            RefAsync<int> rows = 0;

            var result = await _DB.GetInstance.Queryable<SysUser>().Where(CreateExpression(sysUser).ToExpression()).ToPageListAsync(sysUser.PageNum, sysUser.PageSize, rows);

            return new PageResult<SysUser>(result, sysUser.PageSize, rows);
        }

        private static Expressionable<SysUser> CreateExpression(SysUser sysUser)
        {
            var exp = Expressionable.Create<SysUser>();
            if (!string.IsNullOrEmpty(sysUser.UserName))
            {
                exp.And(x => x.UserName == sysUser.UserName);
            }

            if (!string.IsNullOrEmpty(sysUser.Email))
            {
                exp.And(x => x.Email == sysUser.Email);
            }

            if (!string.IsNullOrEmpty(sysUser.Telephone))
            {
                exp.And(x => x.Telephone == sysUser.Telephone);
            }

            if (!string.IsNullOrEmpty(sysUser.Password))
            {
                exp.And(x => x.Password == sysUser.Password);
            }

            if (!string.IsNullOrEmpty(sysUser.Search))
            {
                exp.And(x => x.UserName.Contains(sysUser.Search));
            }
            return exp;
        }
        #endregion 通用的操作

        public async Task<List<SysUser>> List()
        {
            return await _sysUserMapper.GetByName();
        }

        public async Task<int> Add(SysUser sysUser)
        {
            return await _DB.GetInstance.Insertable(sysUser).IgnoreColumns(ignoreNullColumn: true).ExecuteReturnIdentityAsync();
        }
    }
}
