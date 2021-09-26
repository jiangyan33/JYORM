using JYORMApi.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JYORMApi.Service.Imp
{
    public class CommonServiceImp : ICommonService
    {
        private readonly CommonDao _commonDao;

        //public CommonServiceImp(CommonDao commonDao)
        //{
        //    _commonDao = commonDao;
        //}

        public async Task<List<JYORMCommon.Entity.Columns>> GetTableCreateDesc()
        {
            return await _commonDao.GetTableCreateDesc();
        }
    }
}
