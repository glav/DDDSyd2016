using IdentityServer3.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDSyd2016.IdentityServer.CustomServices
{
    public class CustomTokenHandleStore : CustomBaseTokenStore<IdentityServer3.Core.Models.Token>, ITokenHandleStore
    {
        public CustomTokenHandleStore(DapperRepo repo, IScopeStore scopeStore, IClientStore clientStore)
            : base(repo, TokenType.TokenHandle, scopeStore, clientStore)
        {
        }

        public override Task StoreAsync(string key, IdentityServer3.Core.Models.Token value)
        {
            var token = new Models.Token
            {
                TokenKey = key,
                SubjectId = value.SubjectId,
                ClientId = value.ClientId,
                JsonCode = ConvertToJson(value),
                TokenType = (int)_tokenType,
                Expiry = DateTimeOffset.UtcNow.AddSeconds(value.Lifetime),

            };
            _repo.InsertToken(token);

            return Task.FromResult(0);
        }
    }
}
