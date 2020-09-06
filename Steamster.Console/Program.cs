using System.Threading.Tasks;
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
        }
    }
}
