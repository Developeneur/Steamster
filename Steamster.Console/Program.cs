using System.Threading;
using Steamster.Api.Api.Client;
using Steamster.Console;

namespace Steamster.Output
{
    using System;

    public class Program
    {
        
        static void Main(string[] args)
        {
            var steamsterClient = new SteamsterClient();

            DateTime startTime = DateTime.Now;

            Thread t1 = new Thread(async () =>
            {
                await steamsterClient.Run().ConfigureAwait(false);

                Console.WriteLine("Thread 1 Finished");
            });


            t1.Start();

            t1.Join();
        }
    }
}
