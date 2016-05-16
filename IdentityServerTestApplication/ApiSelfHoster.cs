using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace IdentityServerTestApplication
{
    public class ApiSelfHoster
    {
        private Task _hostingThread = null;
        CancellationTokenSource _cancelToken;

        public ApiSelfHoster()
        {
        }

        public void BeginHosting()
        {
            if (_hostingThread != null && !_hostingThread.IsCanceled && !_hostingThread.IsCompleted && !_hostingThread.IsFaulted)
            {
                // already running
                Output.Write("Hosting service already running",InfoType.Warning);
                return;
            }

            if (_hostingThread != null)
            {
                _hostingThread.Dispose();
            }

            _cancelToken = new CancellationTokenSource();

            _hostingThread = Task.Factory.StartNew(() =>
            {
                var config = new HttpSelfHostConfiguration($"http://localhost:{Config.LocalPort}");

                config.Routes.MapHttpRoute(
                    "Callback endpoint", Config.CallbackEndpoint,
                    new { controller="Callback", action="Index", id = RouteParameter.Optional });

                config.Routes.MapHttpRoute(
                    "API Default", "api/{controller}/{id}",
                    new { id = RouteParameter.Optional });

                using (HttpSelfHostServer server = new HttpSelfHostServer(config))
                {
                    Output.Write($"Started listening on {Config.CallbackAbsoluteEndpointUrl}");
                    server.OpenAsync().Wait();
                    while (!_cancelToken.IsCancellationRequested)
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                    Output.Write($"Stopped listening on {config.BaseAddress}");
                    //Console.WriteLine("Press Enter to quit.");
                    //Console.ReadLine();
                }

            }, _cancelToken.Token);
        }

        public void StopHosting()
        {
            _cancelToken.Cancel();
        }
    }
}
