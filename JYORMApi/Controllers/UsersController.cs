using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JYORMApi.Entity;
using JYORMApi.Model;
using JYORMApi.Service;

namespace JYORMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        private readonly ISysUserService _sysUserService;

        public UsersController(ILogger<UsersController> logger, ISysUserService sysUserService)
        {
            _logger = logger;
            _sysUserService = sysUserService;
        }

        /// <summary>
        /// 用户登录接口
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet("login")]
        public async Task<Result> Get([FromQuery] SysUser user)
        {
            var res = await _sysUserService.AddOne(new SysUser());
            //if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Password)) throw new CoreCommonException("用户名或者密码不能为空", ResultCode.ArgumentError);
            //var result = await _userService.Login(user);
            //if (result == null) throw new CoreCommonException("用户名或者密码错误", ResultCode.ArgumentError);

            return new Result(res);
        }


        [HttpGet("Test")]
        public async Task<List<SysUser>> Test()
        {
            return await _sysUserService.List();
        }
    }
}