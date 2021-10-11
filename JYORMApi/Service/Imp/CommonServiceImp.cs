using JYORMApi.Dao;
using JYORMApi.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JYORMApi.Service.Imp
{
    public class CommonServiceImp : ICommonService
    {
        private readonly CommonMapper _commonMapper;

        public CommonServiceImp(CommonMapper commonMapper)
        {
            _commonMapper = commonMapper;
        }

        public async Task<List<Columns>> GetTableCreateDesc()
        {
            return await _commonMapper.GetTableCreateDesc();
        }
    }
}
