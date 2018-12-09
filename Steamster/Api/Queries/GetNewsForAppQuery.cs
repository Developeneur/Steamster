namespace Steamster.Api.Api.Queries
{
    using Newtonsoft.Json;
    using Steamster.Api.Api.Models;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class GetNewsForAppQuery
    {
        public GetNewsForAppQuery(HttpClient client, string appId, string count, string maxLength)
        {
            SteamClient = client;
            _appId = appId;
            _count = count;
            _maxLength = maxLength;
        }

        private string _appId { get; }
        private string _count { get; }
        private string _maxLength { get; }

        private HttpClient SteamClient { get; }

        public async Task<AppNewsData> ExecuteAsync()
        {
            try
            {
                //TODO: Make this into an extension method that checks for null values
                var path = $"ISteamNews/GetNewsForApp/v0002/?appid={_appId}&count={_count}&maxlength={_maxLength}&format=json";

                HttpResponseMessage response = await SteamClient.GetAsync(path).ConfigureAwait(false);

                var results = new AppNewsData();

                if (response.IsSuccessStatusCode)
                {
                    var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    results = JsonConvert.DeserializeObject<AppNewsData>(dataAsString);
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
