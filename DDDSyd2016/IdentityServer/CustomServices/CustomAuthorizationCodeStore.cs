using IdentityServer3.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using Newtonsoft.Json;

namespace DDDSyd2016.IdentityServer.CustomServices
{
    public class CustomAuthorizationCodeStore : CustomBaseTokenStore<AuthorizationCode>, IAuthorizationCodeStore
    {
        public CustomAuthorizationCodeStore(DapperRepo repo, IScopeStore scopeStore, IClientStore clientStore) 
            : base(repo, TokenType.AuthorizationCode,scopeStore,clientStore)
        {
        }


        public override Task StoreAsync(string key, AuthorizationCode value)
        {

            var expiry = value.Client != null ? value.Client.AuthorizationCodeLifetime : 3600;
            var authCodeRecord = new DDDSyd2016.IdentityServer.Models.Token()
            {
                TokenKey = key,
                TokenType = (int)TokenType.AuthorizationCode,
                Expiry = DateTimeOffset.UtcNow.AddSeconds(expiry),
                IsOpenId = value.IsOpenId,
                Nonce = value.Nonce,
                RedirectUri = value.RedirectUri,
                SessionId = value.SessionId,
                SubjectId = value.SubjectId,
                WasConsentShown = value.WasConsentShown,
                JsonCode = ConvertToJson(value),
                ClientId = value.ClientId,
                AuthCodeChallenge = value.CodeChallenge,
                AuthCodeChallengeMethod = value.CodeChallengeMethod
            };
            _repo.InsertToken(authCodeRecord);
            return Task.FromResult(0);
        }
    }
}
