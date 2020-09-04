using System.CodeDom;
using System.Linq;
using System.Threading.Tasks;
using Steamster.Api.Api.Client;
using Steamster.Console.Services;
using Steamster.Output.Services;

namespace Steamster.Console
{
    using System;

    public class SteamsterClient
    {
        static Random rnd = new Random();
        private  SteamApiClient _steamApiClient;

        private string _apiKey;
        private string _userId;
        public SteamsterClient()
        {
            
        }

        public async Task Run()
        {
            //TODO: Maybe move the input out of the client
            DisplayWelcomeScreen();
           

            try
            {
                var success = await VerifySignIn().ConfigureAwait(false);

                //callWebApi().Wait();
                //GetGameResults(apiKey,userKey).Wait();
                while (true)
                {
                    Console.WriteLine("Getting Random Game...");
                    await GetRandomGame(_apiKey, _userId).ConfigureAwait(false);

                    Console.WriteLine("----------------");
                    Console.WriteLine("All Done!!");
                    Console.WriteLine("----------------");

                    Console.ReadLine();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an exception: {ex.ToString()}");
            }


            Console.WriteLine("----------------");
            Console.WriteLine("All Done!!");
            Console.WriteLine("----------------");

            Console.ReadLine();
        }

       
        private async Task<bool> VerifySignIn()
        {
            _steamApiClient = new SteamApiClient(_apiKey);

            return await _steamApiClient.SignIn(_userId);
        }

        private void DisplayWelcomeScreen()
        {
            Console.WriteLine("Welcome to Steamster!");
            /*Main Menu*/



            /*End of Main Menu*/
            Console.ReadKey();

            Console.WriteLine("Please Enter your apiKey");
            _apiKey = Console.ReadLine();

            Console.WriteLine("Okay please enter the user id you would like to lookup");
            _userId = Console.ReadLine();
        }


        private  async Task GetRandomGame(string apiKey, string userId)
        {
            //var command = new GetUsersGames(apiKey, userId);
            var results = await _steamApiClient.GetUsersGames(userId).ConfigureAwait(false);

            var command2 = new GetGames(apiKey);

            var appIds = results.response.games.Select(x => x.appid).ToList();

            //One Route
            int r = rnd.Next(appIds.Count);
            //TODO: Filter results
            var id = appIds[r];

            command2.SetAppId(id);

            var game = await command2.ExecuteAsync().ConfigureAwait(false);

            Console.WriteLine($"Random Game Name - {game.game.gameName} ,app_id - {id}");

            //MessageBox.Show((string)list[r]);

            //int index = 1;
            //foreach (var result in results.response.games)
            //{
            //    command2.SetAppId(result.appid);

            //    var game = await command2.ExecuteAsync().ConfigureAwait(false);

            //    Console.WriteLine($"Game {index}: appName - {game.game.gameName} ,app_id - {result.appid}, playtime_forever - {result.playtime_forever}");
            //    index++;
            //}
        }

        private static async Task GetGameResults(string apiKey, string userKey)
        {
            var command = new GetUsersGames(apiKey, userKey);
            var results = await command.ExecuteAsync();

            var command2 = new GetGames(apiKey);
            int index = 1;
            foreach (var result in results.response.games)
            {
                command2.SetAppId(result.appid);

                var game = await command2.ExecuteAsync().ConfigureAwait(false);

                Console.WriteLine($"Game {index}: appName - {game.game.gameName} ,app_id - {result.appid}, playtime_forever - {result.playtime_forever}");
                index++;
            }
        }
    }
}
