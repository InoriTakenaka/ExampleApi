using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApi {

    using Microsoft.EntityFrameworkCore;
    using ExampleApi.Services;
    public class Startup {

        readonly string CorsPolicy_ = "MyCorsPolicy";

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddControllers();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExampleApi", Version = "v1" });
            });
            // 注入依赖

            // 添加允许跨域的中间件
            // 使得web api 支持跨域访问

            /*
             * 前端项目：localhost:3000
             * API: localhost:5000
             */

            /**
             * options=> CorsOptions
             * 用来配置跨域政策的
             * AddCors->添加跨域中间件
             * 
             *  AllowAnyOrigins -> 允许任何源
             *  WithOrigins -> 允许指定源跨域访问
             *  AllowAnyHeader -> 允许任何HTTP请求头 application/json 复杂请求
             *  AllowAnyMethod -> 允许任何Method 
             *  这里的Method指的是HTTP Method
             *  
             *  这样就配置好了跨域策略
             */
            services.AddCors(options => {
                options.AddPolicy(name: CorsPolicy_,
                    builder => {
                        builder.WithOrigins("http://localhost:3000")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });
            ///
            ///services.add
            ///
            services.AddScoped<IMenuServices, MenuServices>();
            services.AddRouting();
            services.AddDbContext<ExampleApi.DataSource.ExampleContext>(options => {
                options.UseSqlite("Data source=E:\\workspace\\SE 2\\ExampleApi\\DB\\example.db");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExampleApi v1"));
            }

            app.UseRouting();
            // 使用跨域的中间件
            app.UseCors(
                builder=> {
                    builder.AllowAnyOrigin();
                });
            app.UseAuthorization();
            /* 添加路由终结点  
             * 加上这个Endpoints 才能把请求映射到控制器
             */
            app.UseEndpoints(endpoints => {
                //endpoints.MapGet(pattern:"/api/[controller]/[action]/{id}")
                endpoints.MapControllers();
            });
        }
    }
}
