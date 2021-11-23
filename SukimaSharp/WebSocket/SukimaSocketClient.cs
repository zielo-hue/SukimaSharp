using System;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SukimaSharp.WebSocket
{
    /// <summary>
    /// The socket client to handle the websocket connection.
    /// </summary>
    /// <remarks>
    /// As of this commit this is incomplete! Avoid until I figure out websockets.
    /// </remarks>
    internal class SukimaSocketClient
    {
        public SukimaSocketClient(Sukima client)
        {
            Client = client;
        }
        
        private Sukima Client { get; }

        internal ClientWebSocket WebSocket;
        internal CancellationToken CancellationToken = new CancellationToken();

        internal async Task SetupWebsocket()
        {
            while (!CancellationToken.IsCancellationRequested)
            {
                using (WebSocket = new ClientWebSocket())
                {
                    try
                    {
                        await WebSocket.ConnectAsync(new Uri($"{Client.Config.WebsocketUrl}?format=json"),
                            CancellationToken);
                        await Send(WebSocket,
                            JsonConvert.SerializeObject(new AuthenticateRequest {Token = Client.Token}),
                            CancellationToken);
                        await Receive(WebSocket, CancellationToken);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("--- WebSocket Exception ---\n" + $"{ex}");
                    }
                }
            }
        }

        private Task Send(ClientWebSocket socket, string data, CancellationToken stoppingToken)
            => socket.SendAsync(Encoding.UTF8.GetBytes(data), WebSocketMessageType.Text, true, stoppingToken);

        private async Task Receive(ClientWebSocket socket, CancellationToken cancellationToken)
        {
            
        }
        
        internal class AuthenticateRequest
        {
            [JsonProperty("Authorization")]
            public string Type = "Authenticate";

            [JsonProperty("token")]
            public string Token;
        }
    }
}