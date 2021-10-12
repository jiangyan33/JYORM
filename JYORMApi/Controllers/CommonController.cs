using JYORMApi.Entity;
using JYORMApi.Model;
using JYORMApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCommonTool.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JYORMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ILogger<CommonController> _logger;
        private readonly ICommonService _commonService;

        public CommonController(ILogger<CommonController> logger, ICommonService commonService)
        {
            _logger = logger;
            _commonService = commonService;
        }

        /// <summary>
        /// 创建数据库表描述信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTableCreateDesc")]
        public async Task<Result<List<Columns>>> GetTableCreateDesc()
        {
            var result = await _commonService.GetTableCreateDesc();
            return new Result<List<Columns>>(result);
        }


        /// <summary>
        /// 生成实体类
        /// </summary>
        /// <param name="nameStr">命名空间名称</param>
        /// <returns></returns>
        [HttpGet("CreateModel")]
        public async Task<Result<int>> CreateModel([FromQuery] string nameStr)
        {
            var result = await _commonService.CreateModel(nameStr);
            return new Result<int>(1);
        }

        /// <summary>
        /// 事务测试
        /// </summary>
        /// <returns></returns>
        [HttpGet("Test")]
        public async Task<Result<int>> Test()
        {
            await _commonService.TestTransaction();
            return new Result<int>(1);
        }
    }
}
