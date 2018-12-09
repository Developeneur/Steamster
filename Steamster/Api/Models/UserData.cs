namespace Steamster.Api.Api.Models
{
    using System.Collections.Generic;

    public class UserData
    {
        public string UserName { get; set; }
        public string UserSteamId { get; set; }
        public int NumberOfGamesOwned { get; set; }

        public List<RecentlyPlayedGameData> RecentlyPlayedGames { get; set; }
    }
}
