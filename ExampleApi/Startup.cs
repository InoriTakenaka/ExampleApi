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
            // ע������

            // ������������м��
            // ʹ��web api ֧�ֿ������

            /*
             * ǰ����Ŀ��localhost:3000
             * API: localhost:5000
             */

            /**
             * options=> CorsOptions
             * �������ÿ������ߵ�
             * AddCors->��ӿ����м��
             * 
             *  AllowAnyOrigins -> �����κ�Դ
             *  WithOrigins -> ����ָ��Դ�������
             *  AllowAnyHeader -> �����κ�HTTP����ͷ application/json ��������
             *  AllowAnyMethod -> �����κ�Method 
             *  �����Methodָ����HTTP Method
             *  
             *  ���������ú��˿������
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
            // ʹ�ÿ�����м��
            app.UseCors(
                builder=> {
                    builder.AllowAnyOrigin();
                });
            app.UseAuthorization();
            /* ���·���ս��  
             * �������Endpoints ���ܰ�����ӳ�䵽������
             */
            app.UseEndpoints(endpoints => {
                //endpoints.MapGet(pattern:"/api/[controller]/[action]/{id}")
                endpoints.MapControllers();
            });
        }
    }
}
