using System.Threading;
using System.Threading.Tasks;
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
            Task.Run(() => steamsterClient.Run()).Wait();
            //Thread t1 = new Thread(async () =>
            //{
            //    await .ConfigureAwait(false);

            //    Console.WriteLine("Thread 1 Finished");
            //});


            //t1.Start();

            //t1.Join();
        }
    }
}
