namespace Steamster.Api.Api.Queries
{
    using Newtonsoft.Json;
    using Steamster.Api.Api.Models;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class GetUserRecentlyPlayedGames
    {
        public GetUserRecentlyPlayedGames(HttpClient client, string apiKey, string userSteamId)
        {
            SteamClient = client;
            _userSteamId = userSteamId;
            _apiKey = apiKey;
        }

        private HttpClient SteamClient { get; }

        private string _userSteamId;
        private string _apiKey;

        public async Task<RecentlyPlayedGameData> ExecuteAsync()
        {
            try
            {
                //TODO: Make this into an extension method that checks for null values
                var path = $"IPlayerService/GetRecentlyPlayedGames/v0001/?key={_apiKey}&steamid={_userSteamId}&format=json";

                HttpResponseMessage response = await SteamClient.GetAsync(path).ConfigureAwait(false);

                var results = new RecentlyPlayedGameData();

                if (response.IsSuccessStatusCode)
                {
                    var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    results = JsonConvert.DeserializeObject<RecentlyPlayedGameData>(dataAsString);
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
