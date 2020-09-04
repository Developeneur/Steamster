using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Steamster.Api.Api.Models;

namespace Steamster.Api.Api.Interfaces
{
    public class SteamUserApi : ISteamUserApi
    {
        private string RouteRoot => "ISteamUser";

        private string _apiKey;
        private readonly HttpClient _httpClient;
        public SteamUserApi(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
        }


        public async Task<IEnumerable<SteamPlayerSummary>> GetPlayerSummaries(string apiKey, List<string> userIds)
        {
            var callRoute = "/GetPlayerSummaries/v0002/";
            try
            {
                //TODO: Make this into an extension method that checks for null values
                var path = $"{RouteRoot}{callRoute}?key={_apiKey}&steamids={userIds.FirstOrDefault()}&format=json";
                HttpResponseMessage response = await _httpClient.GetAsync(path).ConfigureAwait(false);


                if (!response.IsSuccessStatusCode)
                {
                    return new List<SteamPlayerSummary>();
                }

                var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var  results = JsonConvert.DeserializeObject<IEnumerable<SteamPlayerSummary>>(dataAsString);

                return results;
            }
            catch (Exception e)
            {
                Console.Write($"Stock Intel Exception Thrown: {e.Message}");
                return null;
            };
        }
        //    public async Task<UserGameListData> GetGamesByUser(string userId)
        //    {

        //    }
    }
}
