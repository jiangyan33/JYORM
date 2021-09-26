using JYORMApi.Dao;
using JYORMApi.Entity;
using JYORMApi.Model;
using JYORMApi.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Reflection;

namespace JYORMApi.Utils
{
    public static class ServiceCollectionExtension
    {

        /// <summary>
        /// 初始化服务实例
        /// </summary>
        /// <param name="services"></param>
        public static void InitService(this IServiceCollection services)
        {
            // 实例化数据库操作实例
            services.AddTransient<DefaultDBClient>();

            // HttpClient单例对象
            services.AddSingleton<HttpClient>();

            // 通过反射，将指定的服务全都注入到服务容器中
            // loadFrom:已知程序集的文件名或路径，加载程序集。loadFile是加载程序集中的内容，在这里无法使用
            var assTypes = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var item in assTypes)
            {
                //持久化层
                if (item.Namespace == "JYORMApi.Dao" || item.Namespace == "JYORMApi.Service.Imp")
                {
                    services.AddTransient(item);
                }
                // 加载业务层代理对象
                if (item.Namespace == "JYORMApi.Service")
                {
                    services.AddTransient(item, ServiceProvider =>
                    {
                        var impType = assTypes.ToList().Find(x => x.GetInterface(item.FullName) != null);
                        var serviceImp = ServiceProvider.GetService(impType);
                        var service = typeof(DispatchProxy).GetMethod("Create").MakeGenericMethod(new Type[] { item, typeof(ServiceProxy) }).Invoke(null, null);
                        (service as ServiceProxy).TargetInstance = serviceImp;

                        return service;
                    });
                }
            }
        }

        /// <summary>
        /// 初始化认证信息
        /// </summary>
        /// <param name="services"></param>
        /// <param name="key"></param>
        public static void InitAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            string key = configuration["AppSettings:Key"];
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.SaveToken = true;//保存token,后台验证token是否生效(重要)
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // 不校验过期事件
                    ValidateLifetime = false,
                    ValidateIssuer = false,
                    // token中需要有过期时间信息
                    RequireExpirationTime = true,//是否验证失效时间
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key))
                };
                options.Events = new JwtBearerEvents()
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();
                        context.Response.Clear();
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new Result(ResultCode.AuthError, "Authorization验证失败.")));
                    },
                    OnTokenValidated = async context =>
                    {
                        var payload = (context.SecurityToken as JwtSecurityToken).Payload;
                        var date = DateTimeHelper.GetDateTimeFromTimestamp((payload[JwtRegisteredClaimNames.Exp]).ParseToLong());

                        var authorId = Convert.ToInt64(payload[JwtRegisteredClaimNames.Jti]);
                        context.HttpContext.Items.Add("Id", authorId);

                        // 手动校验token的过期时间
                        // 根据http请求实例化一个子容器
                        using var scope = context.HttpContext.RequestServices.CreateScope();
                        var authorDao = scope.ServiceProvider.GetService<UserDao>();
                        var author = await authorDao.FindOne(new User { Id = authorId });
                        // token过期时间
                        var expireTime = Convert.ToInt32(configuration["AppSettings:ExpireTime"]);

                        if (date.AddMinutes(-expireTime).ToString("yyyy-MM-dd HH:mm:ss") != author.UpdateTokenTime.ToString("yyyy-MM-dd HH:mm:ss"))
                            throw new CoreAuthException("Authorization验证失败", (int)ResultCode.AuthError);

                        // 判断token是否过期
                        if (date < DateTime.Now && !context.HttpContext.Request.Path.Value.Contains("users/RefreshToken"))
                            throw new CoreAuthException("认证信息过期，请重新获取", (int)ResultCode.AuthExpireError);
                    }
                };
            });
        }
    }
}
