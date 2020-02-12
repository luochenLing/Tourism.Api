using log4net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Tourism.WebsocketServer.Utils;

namespace Tourism.WebsocketServer.Middleware
{
    public class WebSocketManagerMiddleware
    {
        private readonly RequestDelegate _next;
        private ILog _log;
        private WebSocketHandler _webSocketHandler { get; set; }

        public WebSocketManagerMiddleware(RequestDelegate next, WebSocketHandler webSocketHandler)
        {
            _next = next;
            _webSocketHandler = webSocketHandler;
            _log = LogManager.GetLogger(typeof(WebSocketManagerMiddleware));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (!context.WebSockets.IsWebSocketRequest)
                {
                    await _next.Invoke(context);
                    return;
                }

                var socket = await context.WebSockets.AcceptWebSocketAsync();

                string senderId = context.Request.Query["sid"];
                await _webSocketHandler.OnConnected(senderId, socket);

                await Receive(socket, async (result, buffer) =>
                {
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        await _webSocketHandler.ReceiveAsync(socket, result, buffer);
                        return;
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await _webSocketHandler.OnDisconnected(socket);
                        return;
                    }
                });
            }
            catch (Exception ex)
            {
                _log.Error("Invoke method error:" + ex);
                throw;
            }
        }

        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            try
            {
                var buffer = new byte[1024 * 4];
                while (socket.State == WebSocketState.Open)
                {
                    var result = await socket.ReceiveAsync(
                        buffer: new ArraySegment<byte>(buffer),
                        cancellationToken: CancellationToken.None);
                    handleMessage(result, buffer);
                }
            }
            catch (Exception ex)
            {
                _log.Error("Receive method error:" + ex);
                throw;
            }
        }
    }
}
