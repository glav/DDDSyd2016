using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerTestApplication
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Output.Display("\nIdentityServer Authorisation Code Test Harness\n\n\n",InfoType.Emphasis);
            Config.GetConfigFromUserInput();

            if (Config.CreateNewLogFile)
            {
                Output.ClearLogFile();
            }
            var hoster = new ApiSelfHoster();
            Output.Write($"Starting up host: {Config.CallbackAbsoluteEndpointUrl}");
            hoster.BeginHosting();

            Output.Display("\nHit Enter to stop\n");
            Console.ReadLine();

            hoster.StopHosting();
            Output.FinishOutputSession();

        }
    }
}
