using IdentityServer3.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core;
using System.Security.Claims;

namespace DDDSyd2016.IdentityServer.CustomServices
{
    public class CustomClientStore : IClientStore
    {
        private DapperRepo _repo;

        public CustomClientStore(DapperRepo repo)
        {
            _repo = repo;
        }
        public Task<IdentityServer3.Core.Models.Client> FindClientByIdAsync(string clientId)
        {
            //var client = DummyClientStore().FirstOrDefault(c => c.ClientId == clientId);
            //return client;
            var client = _repo.GetClient(clientId);
            if (client != null)
            {
                var idClient = new IdentityServer3.Core.Models.Client
                {
                    AbsoluteRefreshTokenLifetime = client.AbsoluteRefreshTokenLifetime,
                    AccessTokenLifetime = client.AccessTokenLifetime,
                    AccessTokenType = (AccessTokenType)client.AccessTokenType,
                    AllowAccessToAllCustomGrantTypes = client.AllowAccessToAllGrantTypes,
                    AllowAccessToAllScopes = client.AllowAccessToAllScopes,
                    AllowClientCredentialsOnly = client.AllowClientCredentialsOnly,
                    AllowRememberConsent = client.AllowRememberConsent,
                    AlwaysSendClientClaims = client.AlwaysSendClientClaims,
                    AuthorizationCodeLifetime = client.AuthorizationCodeLifetime,
                    ClientId = client.ClientId,
                    ClientName = client.ClientName,
                    ClientUri = client.ClientUri,
                    Enabled = client.Enabled,
                    EnableLocalLogin = client.EnableLocalLogin,
                    Flow = (Flows)client.Flow,
                    IdentityTokenLifetime = client.IdentityTokenLifetime,
                    IncludeJwtId = client.IncludeJwtId,
                    LogoUri = client.LogoUri,
                    LogoutSessionRequired = client.LogoutSessionRequired,
                    LogoutUri = client.LogoutUri,
                    PrefixClientClaims = client.PrefixClientClaims,
                    RefreshTokenExpiration = (TokenExpiration)client.RefreshTokenExpiration,
                    RefreshTokenUsage = (TokenUsage)client.RefreshTokenUsage,
                    RequireConsent = client.RequireConsent,
                    RequireSignOutPrompt = client.RequireSignOutPrompt,
                    SlidingRefreshTokenLifetime = client.SlidingRefreshTokenLifetime,
                    UpdateAccessTokenClaimsOnRefresh = client.UpdateAccessTokenOnRefresh,

                    AllowedScopes = client.ClientScopes.ToList(),
                    ClientSecrets = client.ClientSecrets.Select(s =>
                    {
                        return new Secret(s.Value, s.Description, s.Expiration);
                    }).ToList(),
                    RedirectUris = client.ClientRedirectUris.Select(r => r.Uri).ToList()
                };
                //AllowedScopes = client.ClientScopes.Select(s => s.Scope).ToList(),
                //ClientSecrets = client.ClientSecrets.Select(s =>
                //{
                //    return new Secret(s.Value, s.Description, s.Expiration);
                //}).ToList(),
                //RedirectUris = client.ClientRedirectUris.Select(r => r.Uri).ToList(),

                return Task.FromResult<IdentityServer3.Core.Models.Client>(idClient);
            }
            return Task.FromResult<IdentityServer3.Core.Models.Client>(null);
        }

    }
}
