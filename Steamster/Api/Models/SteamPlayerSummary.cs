using System;

namespace Steamster.Api.Api.Models
{
    public class SteamPlayerSummary
    {
        public string SteamId { get; set; }

        public int CommunityVisibilityState { get; set; }

        public int ProfileState { get; set; }

        public string PersonName { get; set; }

        public string ProfileUrl { get; set; }

        public string Avatar { get; set; }

        public string AvatarMedium { get; set; }

        public string AvatarFull { get; set; }

        public string AVatarHash { get; set; }

        //public DateTime LastLogOff { get; set; }
        public string LastLogOff { get; set; }

        public int PersonaState { get; set; }

        public string PrimaryClanId { get; set; }

        //public DateTime TimeCreated { get; set; }
        public string TimeCreated { get; set; }

        public int PersonaStateFlags { get; set; }
    }
}
