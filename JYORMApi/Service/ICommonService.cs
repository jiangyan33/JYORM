using System.Collections.Generic;
using System.Threading.Tasks;

namespace JYORMApi.Service
{
    public interface ICommonService
    {
        public Task<List<JYORMCommon.Entity.Columns>> GetTableCreateDesc();
    }
}
