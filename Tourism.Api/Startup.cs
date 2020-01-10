using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using Tourism.IServer;
using Tourism.Server;

namespace Tourism.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddAuthorization();
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(option =>
                {
                    option.Authority = Configuration.GetSection("IdpUrl")?.Value;
                    option.RequireHttpsMetadata = false;
                    option.ApiName = Configuration.GetSection("ApiKey")?.Value;
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Tourism.Api",
                    Version = "v1",
                    Description = "一个关于旅游项目的API"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddSingleton<ITravelInfoService, TravelInfoService>();
            services.AddSingleton<ISettingService, SettingService>();
            services.AddSingleton<ICouponService, CouponService>();
            services.AddSingleton<IAdministrativeAreaService, AdministrativeAreaService>();
            services.AddSingleton<IMediaInfoService, MediaInfoService>();
            services.AddSingleton<ICustomerServer, CustomerServer>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tourism.Api V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            //二者缺一不可，UseAuthentication缺少，不能验证，UseAuthorization缺少，报错
            app.UseAuthentication();//认证
            app.UseAuthorization();//授权
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
