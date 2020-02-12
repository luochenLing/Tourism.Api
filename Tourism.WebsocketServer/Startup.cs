using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Net.WebSockets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Tourism.WebsocketServer.Utils;

namespace Tourism.WebsocketServer
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
            services.AddWebSocketManager();
            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseWebSockets(new WebSocketOptions()
            {
                //KeepAliveInterval - ��ͻ��˷��͡�ping��֡��Ƶ�ʣ���ȷ�����������Ӵ��ڴ�״̬�� Ĭ��ֵΪ 2 ���ӡ�
                KeepAliveInterval = TimeSpan.FromSeconds(120),
                //ReceiveBufferSize - ���ڽ������ݵĻ������Ĵ�С�� �߼��û�������Ҫ������и��ģ��Ա�������ݴ�С�������ܡ� Ĭ��ֵΪ 4 KB��
                ReceiveBufferSize = 4 * 1024
            });

            app.UseSession();
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;

            app.MapWebSocketManager("/ws", serviceProvider.GetService<ChatMessageHandler>());

            //app.UseCookiePolicy(new CookiePolicyOptions
            //{
            //    HttpOnly = HttpOnlyPolicy.Always
            //});


            ////app.UseMiddleware<ChatWebSocketMiddleware>();

            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Path == "/ws")
            //    {
            //        WebSocket websocket = await context.WebSockets.AcceptWebSocketAsync();
            //        await Echo(context, websocket);
            //    }
            //    else
            //    {
            //        await next();
            //    }
            //});


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private async Task Echo(HttpContext context, WebSocket websocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await websocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                await websocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);
                result = await websocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await websocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
    }
}
