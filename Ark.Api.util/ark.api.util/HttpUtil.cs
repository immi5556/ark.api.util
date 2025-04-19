using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ark.net.util
{
    public class HttpUtil
    {
        static readonly HttpClient client = new HttpClient();
        public static async Task Get(string url)
        {
            using HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
        }
        public static async Task<T> Get<T>(string url)
        {
            using HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseBody);
        }
        public static async Task<Tout> Post<Tin, Tout>(string url, Tin tin)
        {
            using HttpResponseMessage response = await client.PostAsJsonAsync<Tin>(url, tin);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Tout>();
            
        }
        public static async Task<string> PostForm(string url, Dictionary<string, string> to_post)
        {
            using HttpResponseMessage response = await client.PostAsync(url, new FormUrlEncodedContent(to_post));           
            var resp = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            return resp;
        }
    }
}