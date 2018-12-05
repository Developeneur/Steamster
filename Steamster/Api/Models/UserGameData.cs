namespace Steamster.Api.Api.Models
{
using System.Collections.Generic;
    public class UserGameData
    {
        public int game_count { get; set; }

        public List<GameDataForUser> games { get; set; }
    }
}
