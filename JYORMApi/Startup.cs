using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IdentityModel.Tokens.Jwt;
using JYORMApi.Middleware;
using JYORMApi.Utils;
using JYORMApi.Converters;
using JYORMApi.Filters;

namespace JYORMApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            // 清除微软默认的Jwt声明
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(opt => opt.Filters.Add<ValidationResultFilterAttribute>())
           .AddNewtonsoftJson(opt =>
              {
                  opt.SerializerSettings.Converters.Add(new DateTimeConverter());
                  opt.SerializerSettings.ContractResolver = new OrderedContractResolver();
              });
            services.InitService();
            //services.InitAuthentication(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.UseExceptionHandlerMiddleware();
            });

            app.UseStaticFiles();

            app.MapWhen(context =>
            {
                return !context.Request.Path.Value.StartsWith("/api");
            }, appBuilder =>
            {
                var option = new RewriteOptions();
                option.AddRewrite(".*", "/index.html", true);
                appBuilder.UseRewriter(option);
                appBuilder.UseStaticFiles();
            });
            app.UseRouting();

            // 开启跨域请求
            app.UseCorsMiddleware();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}