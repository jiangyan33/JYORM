using System.Collections.Generic;
using System.Threading.Tasks;
using JYORMApi.Entity;

namespace JYORMApi.Service
{
    public interface IUserService
    {
        public Task<User> Login(User user);

        public Task<string> RefreshToken(User user);

        public Task<List<User>> Test();
    }
}