using System.Collections.Generic;
using System.Threading.Tasks;
using JYORMApi.Entity;


namespace JYORMApi.Service
{
    public interface ICommonService
    {
        public Task<List<Columns>> GetTableCreateDesc();

        public Task TestTransaction();

        public Task<bool> CreateModel(string nameStr);
    }
}
