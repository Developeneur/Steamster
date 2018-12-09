namespace Steamster.Output
{
    using Steamster.Console.Services;
    using Steamster.Output.Services;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class Program
    {
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Steamster!");
            Console.ReadKey();

            Console.WriteLine("Please Enter your apiKey");
            var apiKey = Console.ReadLine();

            Console.WriteLine("Okay please enter the user id you would like to lookup");
            var userKey = Console.ReadLine();
            try
            {
                //callWebApi().Wait();
                //GetGameResults(apiKey,userKey).Wait();
                while (true)
                {
                    Console.WriteLine("Getting Random Game...");
                    GetRandomGame(apiKey, userKey).Wait();

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

        private static async Task GetRandomGame(string apiKey, string userKey)
        {
            var command = new GetUsersGames(apiKey, userKey);
            var results = await command.ExecuteAsync();

            var command2 = new GetGames(apiKey);

            var appIds = results.response.games.Select(x => x.appid).ToList();

            //One Route
            int r = rnd.Next(appIds.Count);

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

        private static async Task GetGameResults(string apiKey,string userKey)
        {
            var command = new GetUsersGames(apiKey,userKey);
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

            //return new UserGameListData();
        }
    }
}
