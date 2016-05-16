using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerTestApplication
{
    public static class Config
    {
        static Config()
        {
            LocalPort = ExtensionMethods.GetNumberFromConfigFile("LocalPort",8080);
            CallbackEndpoint = ExtensionMethods.GetStringFromConfigFile("CallbackEndpoint", "Callback");
            IdentityServerEndpoint = ExtensionMethods.GetStringFromConfigFile("IdentityServerEndpoint", "https://staging.api.saasu.com/identity/");
            ClientId = ExtensionMethods.GetStringFromConfigFile("ClientId", "TestAuth-e5ky3RswCSM=");
            ClientSecret = ExtensionMethods.GetStringFromConfigFile("ClientSecret", "5rfcV+tgDgdZk51eCBDJJ13e1hhw3N12ZZLfaJMsOL8=");
        }


        public static void GetConfigFromUserInput()
        {
            bool isComplete = false;
            while (!isComplete)
            {
                Output.Display("\nCreate a new log file? (Y/n - 'No' will append to existing file):> ");
                var newLog = Console.ReadLine();
                Output.Display($"Enter a local port to listen on:\n[ Default: {LocalPort} ]:> ");
                var port = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(port))
                {
                    port = LocalPort.ToString();
                }
                Output.Display($"Enter a local endpoint to listen on\n[ Default: {CallbackEndpoint} ]:> ");
                var endpoint = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(endpoint))
                {
                    endpoint = CallbackEndpoint;
                }

                Output.Display($"Enter Identity Server endpoint to use\n[ Default: {IdentityServerEndpoint} ]:> ");
                var identityEndpoint = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(identityEndpoint))
                {
                    identityEndpoint = IdentityServerEndpoint;
                }

                Output.Display($"Enter Client Id to use\n[ Default: {ClientId} ]:> ");
                var clientid = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(clientid))
                {
                    clientid = ClientId;
                }

                Output.Display($"Enter Client Secret to use\n[ Default: {ClientSecret} ]:> ");
                var clientsecret = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(clientsecret))
                {
                    clientsecret = ClientSecret;
                }

                Console.WriteLine("\nConfiguration is:");
                if (newLog.IsYes())
                {
                    Output.Display("* A new log file will be created.\n");
                }
                else
                {
                    Output.Display("* Exiting log file will be appended to.\n");
                }

                Output.Display($"* Local callback endpoint is:\n\t[");
                Output.Display(ConstructEndpointUrl(endpoint, port), InfoType.Emphasis);
                Output.Display(" ].\n");
                Output.Display("* Identity Server endpoint is:\n\t[ ");
                Output.Display(identityEndpoint, InfoType.Emphasis);
                Output.Display(" ].\n");
                Output.Display($"* Client Id is: [ ");
                Output.Display(clientid, InfoType.Emphasis);
                Output.Display(" ].\n");
                Output.Display($"* Client Secret is: [ ");
                Output.Display(clientsecret, InfoType.Emphasis);
                Output.Display(" ].\n");
                Output.Display(">> Is this correct? (Y/n)> ");
                var answer = Console.ReadLine();
                if (answer.IsYes())
                {
                    if (newLog.IsYes())
                    {
                        CreateNewLogFile = true;
                    }
                    else
                    {
                        CreateNewLogFile = false;
                    }
                    CallbackEndpoint = endpoint;
                    LocalPort = port.AsNumber(80);
                    if (LocalPort >= 80)
                    {
                        isComplete = true;
                    }
                    IdentityServerEndpoint = identityEndpoint;

                    ClientId = clientid;
                    ClientSecret = clientsecret;

                }
                else
                {
                    Output.Display("\n");
                }
            }

        }

        public static string CallbackAbsoluteEndpointUrl
        {
            get
            {
                return ConstructEndpointUrl(CallbackEndpoint, LocalPort.ToString());
            }
        }

        private static string ConstructEndpointUrl(string endpoint, string port)
        {
            return $"http://localhost:{port}/{endpoint}";
        }

        public static int LocalPort { get; private set; }
        public static string CallbackEndpoint { get; private set; }
        public static bool CreateNewLogFile { get; private set; }
        public static string IdentityServerEndpoint { get; private set; }
        public static string ClientId { get; private set; }
        public static string ClientSecret { get; private set; }

        public static string ToString()
        {
            return $"Configuration:{System.Environment.NewLine}\tCreate New Log file: {CreateNewLogFile}{System.Environment.NewLine}\tCallback Endpoint: {CallbackAbsoluteEndpointUrl}{System.Environment.NewLine}\tIdentity Server Endpoint: {IdentityServerEndpoint}";
        }

    }

}
