using Newtonsoft.Json;
using Steamster.Api.Api.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Steamster.Api.Api.Queries
{
    public class GetGameDataQuery
    {
        public GetGameDataQuery(HttpClient client, string apiKey, int appId)
        {
            SteamClient = client;
            _appId = appId;
            _apiKey = apiKey;
        }

        private HttpClient SteamClient { get; }

        private int _appId;
        private string _apiKey;

        public async Task<GameData> ExecuteAsync()
        {
            try
            {
                //TODO: Make this into an extension method that checks for null values
                var path = $"ISteamUserStats/GetSchemaForGame/v2/?key={_apiKey}&appid={_appId}";

                HttpResponseMessage response = await SteamClient.GetAsync(path).ConfigureAwait(false);

                var results = new GameData();

                if (response.IsSuccessStatusCode)
                {
                    var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    results = JsonConvert.DeserializeObject<GameData>(dataAsString);
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
