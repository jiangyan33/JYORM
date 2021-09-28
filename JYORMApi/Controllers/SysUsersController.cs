using JYORMApi.Entity;
using JYORMApi.Model;
using JYORMApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JYORMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SysUsersController : ControllerBase
    {
        private readonly ILogger<SysUsersController> _logger;

        private readonly ISysUserService _sysUserService;

        public SysUsersController(ILogger<SysUsersController> logger, ISysUserService sysUserService)
        {
            _logger = logger;
            _sysUserService = sysUserService;
        }

        [HttpGet("{id}")]
        public async Task<Result<SysUser>> Get(long id)
        {
            var result = await _sysUserService.Get(id);
            return new Result<SysUser>(result);
        }

        [HttpGet("Pages")]
        public async Task<Result<PageResult<SysUser>>> GetPages([FromQuery] SysUser sysUser)
        {
            var result = await _sysUserService.GetPages(sysUser);

            return new Result<PageResult<SysUser>>(result);
        }

        [HttpGet]
        public async Task<Result<List<SysUser>>> Get([FromQuery] SysUser sysUser)
        {
            var result = await _sysUserService.Get(sysUser);

            return new Result<List<SysUser>>(result);
        }

    }
}
