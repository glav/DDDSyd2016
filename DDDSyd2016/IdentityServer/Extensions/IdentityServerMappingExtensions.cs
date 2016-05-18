using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDSyd2016.IdentityServer.IdentityServer.Extensions
{
    public static class SaasuToIdentityServerMappingExtensions
    {
        public static IdentityServer3.Core.Models.Scope ToIdentityServerModel(this Models.Scope scope)
        {
            if (scope == null)
            {
                return null;
            }

            return new IdentityServer3.Core.Models.Scope()
            {
                AllowUnrestrictedIntrospection = scope.AllowUnrestrictedIntrospection,
                ClaimsRule = scope.ClaimsRule,
                Description = scope.Description,
                DisplayName = scope.DisplayName,
                Emphasize = scope.Emphasize,
                Enabled = scope.Enabled,
                IncludeAllClaimsForUser = scope.IncludeAllClaimsForUser,
                Name = scope.Name,
                Required = scope.Required,
                ShowInDiscoveryDocument = scope.ShowInDiscoveryDocument,
                Type = (IdentityServer3.Core.Models.ScopeType)scope.Type
                
            };
        }

        public static IdentityServer3.Core.Models.ScopeClaim ToIdentityServerModel(this Models.ScopeClaim scopeClaim)
        {
            if (scopeClaim == null)
            {
                return null;
            }

            return new IdentityServer3.Core.Models.ScopeClaim()
            {
                AlwaysIncludeInIdToken = scopeClaim.AlwaysIncludeInIdToken,
                Description = scopeClaim.Description,
                Name = scopeClaim.Name
            };
        }

        public static IEnumerable<IdentityServer3.Core.Models.ScopeClaim> ToIdentityServerModels(this IEnumerable<Models.ScopeClaim> scopeClaims)
        {
            var returnList = new List<IdentityServer3.Core.Models.ScopeClaim>();
            if (scopeClaims == null)
            {
                return null;
            }
            scopeClaims.ToList().ForEach(c => returnList.Add(c.ToIdentityServerModel()));
            return returnList;
        }

        public static IdentityServer3.Core.Models.Secret ToIdentityServerModel(this Models.ScopeSecret secret)
        {
            if (secret == null)
            {
                return null;
            }

            return new IdentityServer3.Core.Models.Secret()
            {
                Description = secret.Description,
                Expiration = secret.Expiration,
                Type = secret.Type,
                Value = secret.Value
            };
        }
        public static IEnumerable<IdentityServer3.Core.Models.Secret> ToIdentityServerModels(this IEnumerable<Models.ScopeSecret> scopeSecrets)
        {
            var returnList = new List<IdentityServer3.Core.Models.Secret>();
            if (scopeSecrets == null)
            {
                return null;
            }
            scopeSecrets.ToList().ForEach(c => returnList.Add(c.ToIdentityServerModel()));
            return returnList;
        }

    }
}
