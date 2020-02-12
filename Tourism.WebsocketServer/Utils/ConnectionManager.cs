using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Tourism.WebsocketServer.Utils
{
    public class ConnectionManager
    {
        private ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        public WebSocket GetSocketById(string id)
        {
            return _sockets.FirstOrDefault(p => p.Key == id).Value;
        }

        public ConcurrentDictionary<string, WebSocket> GetAll()
        {
            return _sockets;
        }

        public string GetId(WebSocket socket)
        {
            return _sockets.FirstOrDefault(p => p.Value == socket).Key;
        }

        public void AddSocket(string senderId, WebSocket socket)
        {
            if (!_sockets.ContainsKey(senderId))
            {
                _sockets.TryAdd(senderId, socket);
            }
            //_sockets.TryAdd("f3f13528293b4c6bb8186305c37e8668", socket);
            //_sockets.TryAdd("f3f13537293b4c6bb8186305c37e8668", socket);
        }

        public async Task RemoveSocket(string id)
        {
            WebSocket socket;
            _sockets.TryRemove(id, out socket);
            await socket.CloseAsync(
                closeStatus: WebSocketCloseStatus.NormalClosure,
                statusDescription: "Closed by the ConnectionManager",
                cancellationToken: CancellationToken.None);
        }

        //private string CreateConnectionId()
        //{
        //    return Guid.NewGuid().ToString();
        //}
    }
}
