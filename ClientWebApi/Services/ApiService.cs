using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientWebApi.Services
{
    public class ApiService : IApiService
    {
        public async Task<string> Call(string Uri, string bodyString, Encoding encoding, string mediaType)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(Uri);
            request.Method = HttpMethod.Post;

            request.Headers.Add("Accept", "*/*");

            var content = new StringContent(bodyString, encoding, mediaType);
            request.Content = content;

            var response = await client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
