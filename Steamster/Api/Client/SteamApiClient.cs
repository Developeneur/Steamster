using Steamster.Api.Api.Models;
using Steamster.Api.Queries;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Steamster.Api.Api.Client
{
    public class SteamApiClient
    {
        static HttpClient client;

        public string ApiKey { get; }

        public SteamApiClient(string apiKey)
        {
            ApiKey = apiKey;

            client = new HttpClient();

            client.BaseAddress = new Uri("http://api.steampowered.com/");//TODO: May need to alter this
            
            //To update uri
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<UserGameListData> GetUsersGames(string userSteamId)
        {
            try
            {
                //Create lookup item
                var query = new GetGamesByUserQuery(client,ApiKey, userSteamId);//Extend or implement

                var results = await query.ExecuteAsync().ConfigureAwait(false);

                return results;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new UserGameListData()
                {
                };
            }
        }
    }
}
