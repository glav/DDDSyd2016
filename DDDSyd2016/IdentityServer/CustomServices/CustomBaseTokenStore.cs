using IdentityServer3.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using Newtonsoft.Json;
using DDDSyd2016.IdentityServer.Serialization;

namespace DDDSyd2016.IdentityServer.CustomServices
{
    public abstract class CustomBaseTokenStore<T> where T : class
    {
        protected readonly TokenType _tokenType;
        protected readonly IScopeStore _scopeStore;
        protected IClientStore _clientStore;
        protected DapperRepo _repo;

        public CustomBaseTokenStore(DapperRepo repo, TokenType tokenType, IScopeStore scopeStore, IClientStore clientStore)
        {
            _tokenType = tokenType;
            _scopeStore = scopeStore;
            _clientStore = clientStore;
            _repo = repo;
        }

        JsonSerializerSettings GetJsonSerializerSettings()
        {
            var settings = new JsonSerializerSettings();
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //};
            settings.Converters.Add(new ClaimConverter());
            settings.Converters.Add(new ClaimsPrincipalConverter());
            settings.Converters.Add(new ClientConverter(_clientStore));
            settings.Converters.Add(new ScopeConverter(_scopeStore));
            return settings;
        }

        protected string ConvertToJson(T value)
        {
            return JsonConvert.SerializeObject(value,GetJsonSerializerSettings());
        }

        protected T ConvertFromJson(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, GetJsonSerializerSettings());
        }

        public Task<IEnumerable<ITokenMetadata>> GetAllAsync(string subject)
        {
            var tokens = _repo.GetTokensBySubject(subject, (int)_tokenType);

            var results = tokens.Select(x => ConvertFromJson(x.JsonCode)).ToArray();
            return Task.FromResult<IEnumerable<ITokenMetadata>>(results.Cast<ITokenMetadata>());
        }

        public Task<T> GetAsync(string key)
        {
            var record = _repo.GetToken(key,(int)_tokenType);
            if (record != null)
            {
                return Task.FromResult<T>(ConvertFromJson(record.JsonCode));
            }
            return Task.FromResult<T>(null);
        }

        public Task RemoveAsync(string key)
        {
            _repo.DeleteTokenByKey(key, (int)_tokenType);
            return Task.FromResult(0);
        }

        public Task RevokeAsync(string subject, string client)
        {
            _repo.DeleteTokenBySubjectAndClient(subject, client, (int)_tokenType);
            return Task.FromResult(0);
        }

        public abstract Task StoreAsync(string key, T value);
    }
}
