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

                #region Swagger����Jwt��֤
                //����Ȩ��С��
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                //��header�����token�����ݵ���̨
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���)ֱ���������������Bearer {token}(ע������֮����һ���ո�) \"",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
                    Type = SecuritySchemeType.ApiKey
                });
                #endregion
            });
            services.Configure<TokenManagement>(Configuration.GetSection("JwtToken"));

            // ��appsettings.json�л�ȡ JwtToken ������ ���� ���token������
            // ��ȡJwtToken json���� Ȼ�����л��� TokenManagement�����ʵ��
            TokenManagement token = Configuration.GetSection("JwtToken").Get<TokenManagement>();

            // ���������֤
            services.AddAuthentication(x => {
                // ѡ��ʹ��JwtBearer�ķ�ʽ���������֤
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                // Challenge��ָ��ÿһ�������֤�Ĺ���
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })  // ���JwtBearer �����ҽ������� 
                .AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                // Token��֤����
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters {
                    //�Ƿ���֤ǩ��
                    ValidateIssuerSigningKey = true,
                    //���ܵ���Կ����Ҫת����byte���飩
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                   // ������
                    ValidIssuer = token.Issuer,
                    // ����
                    ValidAudience = token.Audience,
                    // �Ƿ���֤������
                    ValidateIssuer = false,
                    // �Ƿ���֤����
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
            //����Ҫ����������֤����������� Authorizationǰ��
            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            // ����Ҫ�������Ȩ
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
