using System.Collections.Generic;
using IdentityServer3.Core.Models;

namespace DDDSyd2016.IdentityServer.InMemoryRubbish
{
    public class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new[]
            {
                // standard OpenID Connect scopes
                StandardScopes.OpenId,
                StandardScopes.ProfileAlwaysInclude,
                StandardScopes.EmailAlwaysInclude,
                StandardScopes.OfflineAccess,

                // API - access token will 
                // contain roles of user
                new Scope
                {
                    Name = "read",
                    DisplayName = "Read Only",
                    Type = ScopeType.Resource,

                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("read")
                    }
                }
            };
        }
    }
}