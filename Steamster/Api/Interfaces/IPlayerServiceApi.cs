using System.Threading.Tasks;
using Steamster.Api.Api.Models;

namespace Steamster.Api.Api.Interfaces
{
    public interface IPlayerServiceApi
    {
        Task<UserGameListData> GetGamesByUser(string userId);
    }
}
