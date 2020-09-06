using Newtonsoft.Json;
using Steamster.Api.Api.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Steamster.Api.Queries
{
    public class GetGamesByUserQuery
    {
        public GetGamesByUserQuery(HttpClient client, string apiKey,string userSteamId)
        {
            SteamClient = client;
            _userSteamId = userSteamId;
            _apiKey = apiKey;
        }

        private string _userSteamId { get; }
        private string _apiKey { get; }

        private HttpClient SteamClient { get; }

        public async Task<UserGameListData> ExecuteAsync()
        {
            try
            {
                //TODO: Make this into an extension method that checks for null values
                var path = $"IPlayerService/GetOwnedGames/v0001/?key={_apiKey}&steamid={_userSteamId}&format=json";

                HttpResponseMessage response = await SteamClient.GetAsync(path).ConfigureAwait(false);

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
