using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer3;
using IdentityServer3.Core.Configuration;
using System.Security.Cryptography.X509Certificates;
using IdentityServer3.Core;
using IdentityServer3.Core.Extensions;
using Owin;
using IdentityServer3.Core.Logging;
using DDDSyd2016.IdentityServer.Diagnostics;
using DDDSyd2016.IdentityServer.InMemoryRubbish;

namespace DDDSyd2016.IdentityServer.IdentityServerConfig
{
    public static class IdentityServerSetup
    {
        public static void UseIdentityServerSimpleSetup(this IAppBuilder app)
        {
            
            LogProvider.SetCurrentLogProvider(new SimpleDiagnosticLoggerProvider(AppDomain.CurrentDomain.SetupInformation.ApplicationBase));
            LogProvider.GetCurrentClassLogger().Log(IdentityServer3.Core.Logging.LogLevel.Info, () => { return "Starting up..."; });

            app.Map("/identity", idApp =>
             {
                 var options = new IdentityServerOptions
                 {
                     SiteName = "Glavs Secret Identity Server",
                     RequireSsl =  false,
                     IssuerUri = "http://AuthOmeSite.com",
                     SigningCertificate = new X509Certificate2(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\IdentityServer\\SomeCert.pfx", "idsrv3test"),

                     // Setup your logging options - not necessary tho.
                     LoggingOptions = new LoggingOptions
                     {
                         EnableWebApiDiagnostics = true,
                         EnableHttpLogging = true,
                         WebApiDiagnosticsIsVerbose = true,
                         EnableKatanaLogging = true
                     },

                     // In memory rubbish just to get going
                     Factory = new IdentityServerServiceFactory()
                        .UseInMemoryClients(Clients.Get())
                        .UseInMemoryUsers(Users.Get())
                        .UseInMemoryScopes(Scopes.Get()),

                     EnableWelcomePage = true  // leave enabled initially. Can disable and replace later
                     
                     
                 };


                 idApp.UseIdentityServer(options);


             });
        }
    }
}
