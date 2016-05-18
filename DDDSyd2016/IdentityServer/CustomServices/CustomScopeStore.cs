
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer3.Core.Services;
using DDDSyd2016.IdentityServer.IdentityServer.Extensions;

namespace DDDSyd2016.IdentityServer.CustomServices
{
    public class CustomScopeStore : IScopeStore
    {
        private DapperRepo _repo;
        public CustomScopeStore(DapperRepo repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<IdentityServer3.Core.Models.Scope>> FindScopesAsync(IEnumerable<string> scopeNames)
        {

            var scopes = await GetAllScopesAsync();

            if (scopeNames != null && scopeNames.Any())
            {
                scopes = from s in scopes
                         where scopeNames.Contains(s.Name)
                         select s;
            }

            return scopes;
        }

        public async Task<IEnumerable<IdentityServer3.Core.Models.Scope>> GetScopesAsync(bool publicOnly = true)
        {
            var scopes = await GetAllScopesAsync();

            if (publicOnly)
            {
                scopes = from s in scopes
                         where s.ShowInDiscoveryDocument == true
                         select s;
            }

            return scopes;
        }

        private Task<IEnumerable<IdentityServer3.Core.Models.Scope>> GetAllScopesAsync()
        {
            var scopesToReturn = new List<IdentityServer3.Core.Models.Scope>();

            var scopes = _repo.GetAllScopes();
            var allScopeClaims = _repo.GetAllScopeClaims();
            var allScopeSecrets = _repo.GetAllScopeSecrets();

            scopes.ToList().ForEach(s =>
            {
                var scopeSecrets = allScopeSecrets.Where(c => c. Scope_Id == s.Id);
                var scopeClaims = allScopeClaims.Where(c => c.Scope_Id == s.Id);
                var identityModel = s.ToIdentityServerModel();
                identityModel.Claims = scopeClaims.ToIdentityServerModels().ToList();
                identityModel.ScopeSecrets = scopeSecrets.ToIdentityServerModels().ToList();
                scopesToReturn.Add(identityModel);
            });
            return Task.FromResult<IEnumerable<IdentityServer3.Core.Models.Scope>>(scopesToReturn);
        }

    }
}
