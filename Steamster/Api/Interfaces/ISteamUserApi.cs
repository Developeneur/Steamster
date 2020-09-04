using System.Collections.Generic;
using System.Threading.Tasks;
using Steamster.Api.Api.Models;

namespace Steamster.Api.Api.Interfaces
{
    public interface ISteamUserApi
    {
        Task<IEnumerable<SteamPlayerSummary>> GetPlayerSummaries(string apiKey, List<string> userId);
    }
}
