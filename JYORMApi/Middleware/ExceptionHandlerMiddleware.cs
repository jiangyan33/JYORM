using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using JYORMApi.Model;
using System;

namespace JYORMApi.Middleware
{
    /// <summary>
    /// 全局异常处理中间件
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        public ExceptionHandlerMiddleware(RequestDelegate _)
        {
        }

        public async Task InvokeAsync(HttpContext context, IWebHostEnvironment env)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature.Error is CommonException ex)
            {
                // 主动抛出的错误信息
                context.Response.StatusCode = ex.Code == ResultCode.AuthError ? 401 : 200;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new Result<string>(ex.Code, ex.Message)), Encoding.Default);
                return;
            }

            var ret = new Result<Exception>(ResultCode.LogicError, "服务器发生异常. ");

            if (env.IsDevelopment())
            {
                ret.Message += exceptionHandlerPathFeature.Error?.Message;
                ret.Data = exceptionHandlerPathFeature.Error;
            }
            await context.Response.WriteAsync(JsonConvert.SerializeObject(ret));
        }
    }
}