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
                    Flow = Flows.ClientCredentials,

                    // only allowed to access api1
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "email",
                        "read",
                        "write",
                        "offline_access"
                    }
                },

                new Client
                {
                    ClientName = "Client Two",
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
                    }
                }
            };
        }
    }
}