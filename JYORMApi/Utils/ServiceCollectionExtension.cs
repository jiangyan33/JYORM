using JYORMApi.Model;
using JYORMApi.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;

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
                if (item.Namespace == "JYORMApi.Mapper" || item.Namespace == "JYORMApi.Service.Imp")
                {
                    services.AddTransient(item);
                }
                // 加载业务层代理对象
                if (item.Namespace == "JYORMApi.Service")
                {
                    services.AddTransient(item, ServiceProvider =>
                    {
                        // 查找item接口的实现
                        var impType = assTypes.ToList().Find(x => x.GetInterface(item.FullName) != null);
                        return ServiceProvider.GetService(impType);
                    });
                }
            }
        }

        /// <summary>
        /// 初始化JWT认证信息
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void InitAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            string key = configuration["AppSettings:Key"];
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    // 在认证成功之后是否将令牌token存储在Microsoft.AspNetCore.Authentication.AuthenticationProperties中
                    options.SaveToken = true;
                    // 设置用于验证标识令牌的参数（主要设置的是jwt中的payload中的信息是否进行验证）。现在只验证过期时间和密钥
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // 是否验证声明周期
                        ValidateLifetime = false,
                        // 是否验证签发人
                        ValidateIssuer = false,
                        // 是否验证受众
                        ValidateAudience = false,

                        //是否验证失效时间
                        RequireExpirationTime = true,

                        // 是否验证jwt中的密钥信息
                        ValidateIssuerSigningKey = true,
                        // 服务器端保存的密钥
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key))
                    };
                    // jwt验证过程中的事件
                    options.Events = new JwtBearerEvents()
                    {
                        // 在将质询发送回调用方之前调用。(实际是认证失败后的返回值信息)
                        OnChallenge = async context =>
                        {
                            context.HandleResponse();
                            context.Response.Clear();
                            context.Response.ContentType = "application/json";
                            context.Response.StatusCode = 401;
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(new CommonException(ResultCode.AuthError)));
                        },
                        // 在安全令牌通过验证且ClaimsIdentity（payload中的内容）已生成
                        OnTokenValidated = context =>
                       {
                           var payload = (context.SecurityToken as JwtSecurityToken).Payload;
                           var sysUserId = Convert.ToInt64(payload[ClaimTypes.Sid]);
                           context.HttpContext.Items.Add("Id", sysUserId);
                           return System.Threading.Tasks.Task.CompletedTask;
                       }
                    };
                });
        }

        /// <summary>
        /// 初始化Swagger服务
        /// </summary>
        /// <param name="services"></param>
        public static void InitSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Json Web Token 验证请求头使用Bearer模式. 请求头参数示例: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",//Jwt default param name
                    In = ParameterLocation.Header,//Jwt store address
                    Type = SecuritySchemeType.ApiKey//Security scheme type
                });

                // 加验证类型为Bearer
                option.AddSecurityRequirement(new OpenApiSecurityRequirement {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                var assembly = typeof(Program).Assembly;
                var basePath = Path.GetDirectoryName(assembly.Location);
                var xmlPath = Path.Combine(basePath, $"{assembly.GetName().Name}.xml");
                option.IncludeXmlComments(xmlPath);
            });
        }
    }
}
