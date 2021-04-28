using AuthorizationCenterApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace AuthorizationCenterApi {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddControllers();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthorizationCenterApi", Version = "v1" });

                #region Swagger开启Jwt认证
                //开启权限小锁
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                //在header中添加token，传递到后台
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme {
                    Description = "JWT授权(数据将在请求头中进行传递)直接在下面框中输入Bearer {token}(注意两者之间是一个空格) \"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
                #endregion
            });
            services.Configure<TokenManagement>(Configuration.GetSection("JwtToken"));

            // 从appsettings.json中获取 JwtToken 配置项 里面 配的token的内容
            // 获取JwtToken json对象 然后反序列化成 TokenManagement对象的实例
            TokenManagement token = Configuration.GetSection("JwtToken").Get<TokenManagement>();

            // 启用身份认证
            services.AddAuthentication(x => {
                // 选择使用JwtBearer的方式进行身份认证
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                // Challenge：指代每一次身份认证的过程
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })  // 添加JwtBearer ，并且进行配置 
                .AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                // Token验证参数
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters {
                    //是否验证签名
                    ValidateIssuerSigningKey = true,
                    //解密的密钥（需要转化成byte数组）
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                   // 发行人
                    ValidIssuer = token.Issuer,
                    // 受众
                    ValidAudience = token.Audience,
                    // 是否验证发行人
                    ValidateIssuer = false,
                    // 是否验证受众
                    ValidateAudience = false
                   
                };
            });
            services.AddScoped<IOauthService, OauthService>();
            // services.AddScoped<IUserService, UserService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthorizationCenterApi v1"));
            }
            //声明要求进行身份认证，必须出现在 Authorization前面
            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            // 声明要求进行授权
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
