using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using JYORMApi.Model;

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
            if (exceptionHandlerPathFeature.Error is CoreCommonException ex)
            {
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync(
                  JsonConvert.SerializeObject(new Result((ResultCode)System.Enum.Parse(typeof(ResultCode), ex.Code.ToString()), ex.Message)), Encoding.Default);
                return;
            }
            else if (exceptionHandlerPathFeature.Error is CoreAuthException authException)
            {
                context.Response.Clear();
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 401;

                await context.Response.WriteAsync(
                   JsonConvert.SerializeObject(new Result((ResultCode)System.Enum.Parse(typeof(ResultCode), authException.Code.ToString()), authException.Message)), Encoding.Default);
                return;
            }
            var ret = new Result(ResultCode.UnknownError, "服务器发生异常. ");

            if (env.IsDevelopment())
            {
                ret.Message += exceptionHandlerPathFeature.Error?.Message;
                ret.Data = exceptionHandlerPathFeature.Error;
            }
            await context.Response.WriteAsync(JsonConvert.SerializeObject(ret));
        }
    }
}