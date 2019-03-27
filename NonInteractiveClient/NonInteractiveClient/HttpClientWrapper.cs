using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace NonInteractiveClient
{
    public class HttpClientWrapper
    {
        private HttpClient client;
        private Authenticator authenticator;

        public HttpClientWrapper(string url)
        {
            client = new HttpClient {
                BaseAddress = new Uri(url)
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            authenticator = new Authenticator();
        }

        public string Get(string url)
        {
            var token = authenticator.GetToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = client.GetAsync(url).Result;

            httpResponse.EnsureSuccessStatusCode();
            return httpResponse.Content.ReadAsStringAsync().Result;
        }
    }
}
