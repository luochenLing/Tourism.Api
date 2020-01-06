using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.AddSingleton<ITravelInfoService, TravelInfoService>();
            services.AddSingleton<ISettingService, SettingService>();
            services.AddSingleton<ICouponService, CouponService>();
            services.AddSingleton<IAdministrativeAreaService, AdministrativeAreaService>();
            services.AddSingleton<IMediaInfoService, MediaInfoService>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
