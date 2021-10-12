using JYORMApi.Entity;
using JYORMApi.Model;
using JYORMApi.Service;
using JYORMApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace JYORMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SysUsersController : ControllerBase
    {
        private readonly ILogger<SysUsersController> _logger;

        private readonly ISysUserService _sysUserService;

        private readonly IConfiguration _configuration;

        public SysUsersController(ILogger<SysUsersController> logger, ISysUserService sysUserService, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _sysUserService = sysUserService;
        }

        /// <summary>
        ///  获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<Result<SysUser>> Get()
        {
            var id = 5;
            var result = await _sysUserService.Get(id);
            return new Result<SysUser>(result);
        }

        /// <summary>
        /// 分页查找
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        [HttpGet("Pages")]
        public async Task<Result<PageResult<SysUser>>> GetPages([FromQuery] SysUser sysUser)
        {
            var result = await _sysUserService.GetPages(sysUser);

            return new Result<PageResult<SysUser>>(result);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        [HttpGet("login")]
        public async Task<Result<string>> Get([FromQuery] SysUser sysUser)
        {
            if (string.IsNullOrEmpty(sysUser.UserName) || string.IsNullOrEmpty(sysUser.Password)) throw new CommonException(ResultCode.ArgumentError);

            var result = await _sysUserService.Get(sysUser);
            if (result.Count == 0) return new Result<string>(null);
            string key = _configuration["AppSettings:Key"];

            var token = "Bearer " + JwtHelper.IssueJwt(key, 3, result[0]);
            return new Result<string>(token);
        }
    }
}
