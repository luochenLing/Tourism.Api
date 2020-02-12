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
                //KeepAliveInterval - 向客户端发送“ping”帧的频率，以确保代理保持连接处于打开状态。 默认值为 2 分钟。
                KeepAliveInterval = TimeSpan.FromSeconds(120),
                //ReceiveBufferSize - 用于接收数据的缓冲区的大小。 高级用户可能需要对其进行更改，以便根据数据大小调整性能。 默认值为 4 KB。
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
