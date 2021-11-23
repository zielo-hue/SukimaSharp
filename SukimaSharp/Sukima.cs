using System.Text.Json;
using SukimaSharp;
using SukimaSharp.Rest;
using SukimaSharp.WebSocket;

namespace SukimaSharp
{
    public class Sukima
    {
        public Sukima(string token, ClientConfig config = null)
        {

        }

        public string Token { get; }

        // internal JsonSerializer Serializer { get; set; }
        public ClientConfig Config { get; internal set; }
        public SukimaRestClient Rest { get; internal set; }
        internal SukimaSocketClient WebSocket;
    }
}