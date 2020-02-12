using log4net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tourism.WebsocketServer.Utils
{
    public abstract class WebSocketHandler
    {
        private ILog _log;
        protected ConnectionManager WebSocketConnectionManager { get; set; }

        public WebSocketHandler(ConnectionManager webSocketConnectionManager)
        {
            WebSocketConnectionManager = webSocketConnectionManager;
            _log = LogManager.GetLogger(typeof(WebSocketHandler));
        }

        public virtual async Task OnConnected(string senderId, WebSocket socket)
        {
            WebSocketConnectionManager.AddSocket(senderId, socket);
        }

        public virtual async Task OnDisconnected(WebSocket socket)
        {
            await WebSocketConnectionManager.RemoveSocket(WebSocketConnectionManager.GetId(socket));
        }

        public async Task SendMessageAsync(WebSocket socket, string message)
        {
            try
            {
                if (socket.State != WebSocketState.Open)
                {
                    return;
                }

                await socket.SendAsync(
                    buffer: new ArraySegment<byte>(
                        array: Encoding.UTF8.GetBytes(message)),
                    messageType: WebSocketMessageType.Text,
                    endOfMessage: true,
                    cancellationToken: CancellationToken.None);
            }
            catch (Exception ex)
            {
                _log.Error("SendMessageAsync method error:" + ex);
                throw;
            }
        }

        public async Task SendMessageAsync(string socketId, string message)
        {
            await SendMessageAsync(WebSocketConnectionManager.GetSocketById(socketId), message);
        }

        public async Task SendMessageToAllAsync(string socketId, string msg,bool isObj=true)
        {
            try
            {
                foreach (var pair in WebSocketConnectionManager.GetAll())
                {
                    if (isObj)
                    {
                        var msgTemplate = JsonConvert.DeserializeObject<MsgTemplate>(msg);
                        if (pair.Value.State == WebSocketState.Open)
                        {
                            if (pair.Key == msgTemplate.ReceiverID || pair.Key == socketId)
                            {
                                await SendMessageAsync(pair.Value, msg);
                            }
                        }
                    }
                    else
                    {
                        if (pair.Value.State == WebSocketState.Open)
                        {
                            if (pair.Key == socketId)
                            {
                                await SendMessageAsync(pair.Value, msg);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("SendMessageToAllAsync method error:" + ex);
                throw;
            }
        }

        public abstract Task ReceiveAsync(WebSocket socket,WebSocketReceiveResult result, byte[] buffer);
    }
}
