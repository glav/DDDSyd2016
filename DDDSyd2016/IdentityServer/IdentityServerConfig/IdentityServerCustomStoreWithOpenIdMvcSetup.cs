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
using IdentityServer3.Core.Services;
using DDDSyd2016.IdentityServer.CustomServices;
using Microsoft.Owin.Security.OpenIdConnect;

namespace DDDSyd2016.IdentityServer.IdentityServerConfig
{
    public static class IdentityServerCustomStoreWithOpenIdMvcSetup
    {
        public static void UseIdentityServerCustomStoreWithOpenIdMvcSetup(this IAppBuilder app)
        {

            LogProvider.SetCurrentLogProvider(new SimpleDiagnosticLoggerProvider(AppDomain.CurrentDomain.SetupInformation.ApplicationBase));
            LogProvider.GetCurrentClassLogger().Log(IdentityServer3.Core.Logging.LogLevel.Info, () => { return "Starting up custom store implementation..."; });

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
                    Factory = new IdentityServerServiceFactory(),
                    EnableWelcomePage = true,

                    AuthenticationOptions = new AuthenticationOptions
                    {
                        CookieOptions = new IdentityServer3.Core.Configuration.CookieOptions
                        {
                            AllowRememberMe = true,
                            IsPersistent = false,
                            RememberMeDuration = TimeSpan.FromMinutes(1),

                        },
                        EnableSignOutPrompt = false
                    },

                };

                // View options for things like consent form
                SetFactoryAndViewOpptions(options);
                SetupCustomImplementationHooks(options);

                idApp.UseIdentityServer(options);
            });

            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies",
            });
            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = "http://localhost:35373/identity",
                ClientId = "mvc",
                RedirectUri = "http://localhost:35373/",
                ResponseType = "id_token",

                SignInAsAuthenticationType = "Cookies"
            });
        }

        private static void SetFactoryAndViewOpptions(IdentityServerOptions options)
        {
            var viewOptions = new DefaultViewServiceOptions();
            viewOptions.Stylesheets.Add("/Content/IdentityServer/CustomIdentityServerStyles.css");
            viewOptions.CustomViewDirectory = string.Format("{0}\\Content\\IdentityServer", AppDomain.CurrentDomain.BaseDirectory);


            options.Factory.CorsPolicyService = new Registration<ICorsPolicyService>(new DefaultCorsPolicyService { AllowAll = true });
            options.EnableWelcomePage = false;
#if DEBUG
            options.EnableWelcomePage = true;
#endif

#if DEBUG
            viewOptions.CacheViews = false;
#endif
            options.Factory.ConfigureDefaultViewService(viewOptions);
        }

        private static void SetupCustomImplementationHooks(IdentityServerOptions options)
        {
            options.Factory.Register(new Registration<DapperRepo>());
            options.Factory.Register(new Registration<MyMembershipService>());
            
            options.Factory.UserService = new Registration<IUserService, CustomIdentityUserService>();
            options.Factory.ClientStore = new Registration<IClientStore, CustomClientStore>();
            options.Factory.AuthorizationCodeStore = new Registration<IAuthorizationCodeStore, CustomAuthorizationCodeStore>();
            options.Factory.ConsentStore = new Registration<IConsentStore, CustomConsentStore>();
            options.Factory.ScopeStore = new Registration<IScopeStore, CustomScopeStore>();
            options.Factory.TokenHandleStore = new Registration<ITokenHandleStore, CustomTokenHandleStore>();
            options.Factory.RefreshTokenStore = new Registration<IRefreshTokenStore, CustomRefreshTokenStore>();
            options.Factory.ClientPermissionsService = new Registration<IClientPermissionsService, CustomClientPermissionsService>();

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
