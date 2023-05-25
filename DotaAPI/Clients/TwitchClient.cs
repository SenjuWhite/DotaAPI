using DotaAPI.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace DotaAPI.Clients
{
    public class TwitchClient
    {
        private static string _clientId;
        private static string _accessToken;
        private static string _baseAddress;
        private HttpClient client;

        public TwitchClient()
        {
            _clientId = Constants.clientId;
            _accessToken = Constants.accessToken;
            _baseAddress = Constants.baseAddress;
            client = new HttpClient();
            client.BaseAddress = new Uri(_baseAddress);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            client.DefaultRequestHeaders.Add("Client-Id", _clientId);
        }

        public async Task<ScheduleResponse> GetChannelStreamScheduleAsync(string broadcasterId)
        {
            var response = await client.GetAsync($"schedule?broadcaster_id={broadcasterId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var schedule = JsonConvert.DeserializeObject<ScheduleResponse>(content);
            return schedule;
        }      
    }
}
