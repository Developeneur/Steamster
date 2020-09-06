using System.Linq;
using System.Threading.Tasks;
using Steamster.Api.Api.Client;
using Steamster.Api.Api.Models;
using Steamster.Console.Services;
using Steamster.Output.Services;

namespace Steamster.Console
{
    using System;

    public class SteamsterClient
    {
        static Random rnd = new Random();
        private  SteamApiClient _steamApiClient;

        private UserGameListData _userGameList;
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
                while (success)
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
            if (_userGameList == null)
            {
                _userGameList = await _steamApiClient.GetUsersGames(userId).ConfigureAwait(false);
            }

            var command2 = new GetGames(apiKey);

            GameData game;
            int id; 

            do
            {
                var appIds = _userGameList.response.games.Select(x => x.appid).ToList();

                //One Route
                int r = rnd.Next(appIds.Count);
                //TODO: Filter results
                 id = appIds[r];
                 var gameToRemove = _userGameList.response.games.FirstOrDefault(x => x.appid == id);
                command2.SetAppId(id);

                game = await command2.ExecuteAsync().ConfigureAwait(false);

                if (game.game.gameName == null)
                {
                    //REmove app id from list
                    var removed = _userGameList.response.games.Remove(gameToRemove);
                    if (removed)
                    {
                        Console.WriteLine($"INFO: App Id resulted in a null game name. Game Removed From List AppId: {id}");
                    }
                    
                }
                if (game.game.gameName == "")
                {
                    Console.WriteLine($"INFO: App Id resulted in a game name of \"\". Game NOT Removed From List AppId: {id}");
                }
            } while (game.game.gameName == null);


            Console.WriteLine($"Random Game Name - {game.game.gameName} ,app_id - {id}");
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
