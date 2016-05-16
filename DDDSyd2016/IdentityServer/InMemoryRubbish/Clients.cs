using System.Collections.Generic;
using IdentityServer3.Core.Models;

namespace DDDSyd2016.IdentityServer.InMemoryRubbish
{
    public class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "Client One",
                    ClientId = "Client1",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },

                    // server to server communication
                    Flow = Flows.ResourceOwner,

                    // only allowed to access api1
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "email",
                        "read",
                        "write",
                        "offline_access"
                    },
                    AccessTokenType = AccessTokenType.Jwt,
                    RefreshTokenUsage = TokenUsage.ReUse
                },

                new Client
                {
                    ClientName = "Client Two (Jwt token)",
                    ClientId = "Client2",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },

                    // human involved
                    Flow = Flows.AuthorizationCode,

                    RedirectUris = new List<string>
                    {
                        "http://localhost:5000/callback",
                    },

                    // access to identity data and api1
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "email",
                        "read",
                        "offline_access"
                    },
                    AccessTokenType = AccessTokenType.Jwt
                },
                new Client
                {
                    ClientName = "Client Three (Ref token)",
                    ClientId = "Client3",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },

                    // server to server communication
                    Flow = Flows.AuthorizationCode,
                    RedirectUris = new List<string>
                    {
                        "http://localhost:5000/callback",
                    },

                    // only allowed to access api1
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "email",
                        "read",
                        "write",
                        "offline_access"
                    },
                    AccessTokenType = AccessTokenType.Reference
                },


            };
        }
    }
}