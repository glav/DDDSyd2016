using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDSyd2016.IdentityServer.CustomServices
{
    public class CustomRefreshTokenStore : CustomBaseTokenStore<IdentityServer3.Core.Models.RefreshToken>, IRefreshTokenStore
    {
        public CustomRefreshTokenStore(DapperRepo repo, IScopeStore scopeStore, IClientStore clientStore) 
            : base(repo, TokenType.RefreshToken,scopeStore,clientStore)
        {
        }

        public override Task StoreAsync(string key, RefreshToken value)
        {
            var token = _repo.GetToken(key, (int)_tokenType);
            if (token == null)
            {
                token = new Models.Token
                {
                    TokenKey = key,
                    SubjectId = value.SubjectId,
                    ClientId = value.ClientId,
                    JsonCode = ConvertToJson(value),
                    TokenType = (int)_tokenType,
                    Expiry = value.CreationTime.AddSeconds(value.LifeTime)
                };
                _repo.InsertToken(token);
                return Task.FromResult(0);
            }

            token.Expiry = value.CreationTime.AddSeconds(value.LifeTime);
            _repo.UpdateTokenExpiry(key, token.Expiry);
            return Task.FromResult(0);
        }
    }
}
