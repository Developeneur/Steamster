using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Steamster.Api.Api.Constants;
using Steamster.Api.Api.Models;

namespace Steamster.Api.Api.Interfaces
{
    public class PlayerServiceApi : IPlayerServiceApi
    {
        private string _apiKey;
        private HttpClient _client;
        private  string RouteRoot => "IPlayerService";

        public PlayerServiceApi(HttpClient client,string apiKey)
        {
            _apiKey = apiKey;
            _client = client;
        }

        public async Task<UserGameListData> GetGamesByUser(string userId)
        {
            var callRoute = "/GetOwnedGames/v0001/";
            try
            {
                //TODO: Make this into an extension method that checks for null values
                var path = $"{RouteRoot}{callRoute}?key={_apiKey}&steamid={userId}&format=json";
                HttpResponseMessage response = await _client.GetAsync(path).ConfigureAwait(false);

                var results = new UserGameListData();

                if (response.IsSuccessStatusCode)
                {
                    var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    results = JsonConvert.DeserializeObject<UserGameListData>(dataAsString);
                }

                return results;
            }
            catch (Exception e)
            {
                Console.Write($"Stock Intel Exception Thrown: {e.Message}");
                return null;
            }
        }
    }
}
