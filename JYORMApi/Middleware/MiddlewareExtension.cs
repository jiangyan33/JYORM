using Microsoft.AspNetCore.Builder;

namespace JYORMApi.Middleware
{
    public static class MiddlewareExtension
    {
        /// <summary>
        /// 跨域请求处理中间件
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCorsMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<CorsMiddleware>();
            return applicationBuilder;
        }

        /// <summary>
        /// 全局异常处理中间件
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<ExceptionHandlerMiddleware>();
            return applicationBuilder;
        }
    }
}