using Steamster.Api.Api.Models;
using Steamster.Output.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steamster.Output
{
    public class Program
    {
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
               GetGameResults(apiKey,userKey).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an exception: {ex.ToString()}");
            }
           

            Console.WriteLine("----------------");
            Console.WriteLine("All Done!!");
            Console.WriteLine("----------------");

        }

        private static async Task GetGameResults(string apiKey,string userKey)
        {
            var command = new GetUsersGames(apiKey,userKey);
            var results = await command.ExecuteAsync();

            int index = 1;
            foreach (var result in results.response.games)
            {
                Console.WriteLine($"Game {index}: app_id - {result.appid}, playtime_forever - {result.playtime_forever}");
                index++;
            }

            //return new UserGameListData();
        }
    }
}
