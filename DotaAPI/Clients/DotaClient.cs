using Newtonsoft.Json;
using DotaAPI.Models;

namespace DotaAPI.Clients
{ 
    public class DotaClient
    {
        private HttpClient client;
        private static string _address;
        private static string _apiKey;
        public DotaClient()
        {
            _address = Constants.address_Dota;
            _apiKey = Constants.apikey_Dota;
            client = new HttpClient();
            client.BaseAddress = new Uri(_address);
        }
        public async Task<PlayerInfo> GetPlayerASync(int id)
        {
            var response = await client.GetAsync($"/api/players/{id}?api_key={_apiKey}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PlayerInfo>(content);
            return result;
        }
        public async Task<MatchInfo> GetMatchASync(long id)
        {
            var response = await client.GetAsync($"/api/matches/{id}?api_key={_apiKey}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<MatchInfo>(content);
            return result;
        }
        public async Task<List<PlayerRecentMatchesInfo.Matches>> GetPlayerMatchesAsync(int id)
        {
            var response = await client.GetAsync($"/api/players/{id}/recentMatches?api_key={_apiKey}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<PlayerRecentMatchesInfo.Matches>>(content);
            return result;
        }
        public async Task<List<HeroesInfo.Hero>> GetHeroesAsync()
        {
            var response = await client.GetAsync($"/api/heroes?api_key={_apiKey}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<HeroesInfo.Hero>>(content);
            return result;
        }
        public async Task<PlayerWLInfo> GetPlayerWLAsync(int id)
        {
            var response = await client.GetAsync($"/api/players/{id}/wl?api_key={_apiKey}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PlayerWLInfo>(content);
            return result;
        }

    }
    
}
