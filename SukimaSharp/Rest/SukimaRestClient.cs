using System;
using System.Linq;
using System.Net.Http;

namespace SukimaSharp.Rest
{
    public class SukimaRestClient
    {
        public SukimaRestClient(Sukima client)
        {
            Client = client;
            if (string.IsNullOrEmpty(Client.Config.ApiUrl))
                throw new ("API_URL not set!");
            if (!Uri.IsWellFormedUriString(client.Config.ApiUrl, UriKind.Absolute))
                throw new ("Client config API_URL is an invalid format.");
            if (!Client.Config.ApiUrl.EndsWith('/'))
                Client.Config.ApiUrl += "/";

            HttpClient = new HttpClient()
            {
                BaseAddress = new Uri(Client.Config.UploadUrl)
            };
            HttpClient.DefaultRequestHeaders.Add("Authorization", Client.Token);
            FileHttpClient = new HttpClient()
            {
                BaseAddress = new Uri(Client.Config.UploadUrl)
            };
        }
        
        public Sukima Client { get; }
        internal HttpClient HttpClient;
        internal HttpClient FileHttpClient;
    }
}