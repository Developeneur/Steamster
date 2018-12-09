namespace Steamster.Console.Services
{
    using Steamster.Api.Api.Client;
    using Steamster.Api.Api.Models;
    using System;
    using System.Threading.Tasks;

    public class GetGames
    {
        private string _apiKey;
        private int _appId;
        private SteamApiClient _client;

        public GetGames(string apiKey)
        {
            _apiKey = apiKey;
        }
        public GetGames(string apiKey, int appId/*May not need*/)
        {
            _apiKey = apiKey;
            _appId = appId;
        }

        public async Task<GameData> ExecuteAsync()
        {
            InitializeClient();

            var stats = await _client.GetGameData(_appId).ConfigureAwait(false);//may need to pass in a different parameter. Temp

            //var results = await client.GetUserInformation(PlayerName).ConfigureAwait(false);
            return stats;
        }

        private void InitializeClient()
        {
            if(_client == null)
            {
                _client = new SteamApiClient(_apiKey);
            }

        }

        public void SetAppId(int appId)
        {
            _appId = appId;
        }
    }
}
