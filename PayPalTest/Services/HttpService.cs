using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Collections.Generic;

namespace PayPalTest.Service
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public HttpService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<string> Post()
        {
            string url = _configuration["PayPal:EndpointUrl"];
            return await _client.GetStringAsync(url);
        }

        public async Task<string> GetAccessToken()
        {
            var url = $"{_configuration["PayPal:EndpointUrl"]}/v1/oauth2/token";
            var auth = $"{_configuration["PayPal:ClientId"]}:{_configuration["PayPal:ClientSecret"]}";
            var data = "grant_type=client_credentials";

            using (HttpClient client = new HttpClient())
            {
                var base64Auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(auth));

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Auth);

                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = new StringContent(data, Encoding.UTF8, "application/x-www-form-urlencoded");

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    dynamic json = JsonConvert.DeserializeObject(jsonResponse);
                    return json.access_token;
                }
                else
                {
                    Debug.WriteLine($"Error: {response.StatusCode}");
                    return null;
                }
            }

        }
    }
}
