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
using IdentityServer3.Core.Services.Default;
using DDDSyd2016.IdentityServer.Diagnostics;
using DDDSyd2016.IdentityServer.InMemoryRubbish;

namespace DDDSyd2016.IdentityServer.IdentityServerConfig
{
    public static class IdentityServerCustomViewSetup
    {
        public static void UseIdentityServerCustomViewSetup(this IAppBuilder app)
        {

            LogProvider.SetCurrentLogProvider(new SimpleDiagnosticLoggerProvider(AppDomain.CurrentDomain.SetupInformation.ApplicationBase));
            LogProvider.GetCurrentClassLogger().Log(IdentityServer3.Core.Logging.LogLevel.Info, () => { return "Starting up custom view implementation..."; });

            var requireSsl = true;
#if DEBUG
            requireSsl = false;
#endif

            app.Map("/Identity", idApp =>
             {
                 var options = new IdentityServerOptions
                 {
                     SiteName = "Glavs Secret Identity Server",
                     RequireSsl = requireSsl,
                     IssuerUri = "http://AuthOmeSite.com",
                     SigningCertificate = CertificateLoader.LoadCertificate(),

                     LoggingOptions = GetFullLoggingConfig(),
                     Factory = GetInMemoryFactoryOptions(),
                     EnableWelcomePage = true
                 };

                 // View options for things like consent form
                 var viewOptions = new DefaultViewServiceOptions();
                 viewOptions.Stylesheets.Add("/Content/IdentityServer/CustomIdentityServerStyles.css");
                 viewOptions.CustomViewDirectory = string.Format("{0}\\Content\\IdentityServer", AppDomain.CurrentDomain.BaseDirectory);

#if DEBUG
                 viewOptions.CacheViews = false;
#endif
                 options.Factory.ConfigureDefaultViewService(viewOptions);

                 

                 idApp.UseIdentityServer(options);


             });
        }

        // In memory rubbish just to get going
        private static IdentityServerServiceFactory GetInMemoryFactoryOptions()
        {
            var factory = new IdentityServerServiceFactory()
                                    .UseInMemoryClients(Clients.Get())
                                    .UseInMemoryUsers(Users.Get())
                                    .UseInMemoryScopes(Scopes.Get());
            return factory;
        }

        private static LoggingOptions GetFullLoggingConfig()
        {
            return new LoggingOptions
            {
                EnableWebApiDiagnostics = true,
                EnableHttpLogging = true,
                WebApiDiagnosticsIsVerbose = true,
                EnableKatanaLogging = true
            };
        }
    }
}
