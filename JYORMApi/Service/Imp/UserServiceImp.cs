using Microsoft.Extensions.Configuration;
using MyCommonTool.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JYORMApi.Attr;
using JYORMApi.Dao;
using JYORMApi.Entity;
using JYORMApi.Model;
using JYORMApi.Utils;

namespace JYORMApi.Service.Imp
{
    public class UserServiceImp : IUserService
    {
        private readonly UserDao _userDao;
        private readonly IConfiguration _configuration;

        //public UserServiceImp(UserDao userDao, IConfiguration configuration)
        //{
        //    _userDao = userDao;
        //    _configuration = configuration;
        //}

        [Transactions(true)]
        public async Task<User> Login(User user)
        {
            // 对密码进行加密
            //var key = _configuration.GetSection("AppSettings")["Key"];
            //user.Password = CommonUtils.Encrypt(key, user.Password);

            var result = await _userDao.Find(user);
            if (result == null) return result;

            // 更新用户的登录信息
            //result.UpdateTokenTime = DateTime.Now.CutMillisecondFromDateTime();
            //result.LastLoginTime = result.UpdateTokenTime;
            //result.UpdateDate = result.UpdateTokenTime;
            //await _userDao.Update(result);
            //if (result != null)
            //{
            //    var expireTime = Convert.ToInt32(_configuration["AppSettings:ExpireTime"]);
            //    result.Token = "Bearer " + Utils.JwtHelper.IssueJwt(key, expireTime, result);
            //}

            return result;
        }

        [TransactionsAttribute(true)]
        public async Task<string> RefreshToken(User user)
        {
            // 对密码进行加密
            var key = _configuration.GetSection("AppSettings")["Key"];

            var result = await _userDao.FindOne(user);
            // 如果用户的最后登录时间小于7天，执行刷新token，否则提示需要重新登录
            if (result.LastLoginTime.AddDays(7) < DateTime.Now)
                throw new CoreCommonException("距离上次登录的时间过长，请尝试重新登录", ResultCode.AuthExpireError);

            // 更新用户的登录信息
            result.UpdateTokenTime = DateTime.Now.CutMillisecondFromDateTime();
            result.UpdateDate = result.UpdateTokenTime;
            await _userDao.Update(result);

            if (result != null)
            {
                var expireTime = Convert.ToInt32(_configuration["AppSettings:ExpireTime"]);
                result.Token = "Bearer " + JwtHelper.IssueJwt(key, expireTime, result);
            }

            return result.Token;
        }

        public async Task<List<User>> Test()
        {
            return await _userDao.Test();
        }
    }
}