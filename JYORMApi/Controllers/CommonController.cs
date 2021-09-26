﻿using JYORMApi.Model;
using JYORMApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        [HttpGet("GetTableCreateDesc")]
        public async Task<Result> GetTableCreateDesc()
        {
            var result = await _commonService.GetTableCreateDesc();
            return new Result(data: result);
        }
    }
}