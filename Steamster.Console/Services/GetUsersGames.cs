using Steamster.Api.Api.Client;
using Steamster.Api.Api.Constants;
using Steamster.Api.Api.Models;
using System.Threading.Tasks;

namespace Steamster.Output.Services
{
    public class GetUsersGames
    {
        private string _userName;
        private string _userSteamId;
        private string _apiKey;

        public GetUsersGames(string apiKey, string userSteamId = SteamIds.DevelopeneurSteamId)
        {
            //_userName = "Developeneur";
            _apiKey = apiKey;
            _userSteamId = userSteamId;
        }

        public async Task<UserGameListData> ExecuteAsync()
        {
            var client = new SteamApiClient(_apiKey);

            var stats = await client.GetUsersGames(_userSteamId).ConfigureAwait(false);//may need to pass in a different parameter. Temp

            //var results = await client.GetUserInformation(PlayerName).ConfigureAwait(false);
            return stats;
        }
    }
}
