using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Tourism.WebsocketServer.Utils
{
    public class ChatMessageHandler : WebSocketHandler
    {
        public ChatMessageHandler(ConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {

        }

        public override async Task OnConnected(string senderId, WebSocket socket)
        {
          
            await base.OnConnected(senderId, socket);

            var socketId = WebSocketConnectionManager.GetId(socket);

            //var msg = $"{socketId} is now connected";


            //await SendMessageToAllAsync(socketId, msg,false);
        }

        public override async Task ReceiveAsync(WebSocket socket,WebSocketReceiveResult result, byte[] buffer)
        {
            var socketId = WebSocketConnectionManager.GetId(socket);

            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

            await SendMessageToAllAsync(socketId, message);
        }

    }
}
