using IdentityServer3.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDSyd2016.IdentityServer.CustomServices
{
    public class CustomConsentStore : IConsentStore
    {
        private readonly DapperRepo _repo;

        public CustomConsentStore(DapperRepo repo)
        {
            _repo = repo;

        }

        public Task<IdentityServer3.Core.Models.Consent> LoadAsync(string subject, string client)
        {

            var found = _repo.GetConsentBySubjectAndClient(subject, client);
            if (found == null)
            {
                return Task.FromResult<IdentityServer3.Core.Models.Consent>(null);
            }
            var result = new IdentityServer3.Core.Models.Consent
            {
                Subject = found.Subject,
                ClientId = found.ClientId,
                Scopes = ParseScopes(found.Scopes)
            };

            return Task.FromResult<IdentityServer3.Core.Models.Consent>(result);


        }

        public Task UpdateAsync(IdentityServer3.Core.Models.Consent consent)
        {
            var item = _repo.GetConsentBySubjectAndClient(consent.Subject, consent.ClientId);
            bool needToInsert = false;

            if (item == null)
            {
                item = new Models.Consent
                {
                    Subject = consent.Subject,
                    ClientId = consent.ClientId
                };
                needToInsert = true;
            }

            if (consent.Scopes == null || !consent.Scopes.Any())
            {
                if (!needToInsert)
                {
                    _repo.DeleteConsentBySubjectAndClient(consent.Subject, consent.ClientId);
                }
            }

            item.Scopes = StringifyScopes(consent.Scopes);

            if (needToInsert)
            {
                _repo.InsertConsent(item);
            }
            else
            {
                _repo.UpdateConsent(item);
            }

            return Task.FromResult(0);
        }

        public Task<IEnumerable<IdentityServer3.Core.Models.Consent>> LoadAllAsync(string subject)
        {
            var found = _repo.GetConsentsBySubject(subject);

            var results = found.Select(x => new IdentityServer3.Core.Models.Consent
            {
                Subject = x.Subject,
                ClientId = x.ClientId,
                Scopes = ParseScopes(x.Scopes)
            });

            return Task.FromResult<IEnumerable<IdentityServer3.Core.Models.Consent>>(results.ToArray().AsEnumerable());
        }

        private IEnumerable<string> ParseScopes(string scopes)
        {
            if (scopes == null || String.IsNullOrWhiteSpace(scopes))
            {
                return Enumerable.Empty<string>();
            }

            return scopes.Split(',');
        }

        private string StringifyScopes(IEnumerable<string> scopes)
        {
            if (scopes == null || !scopes.Any())
            {
                return null;
            }

            return scopes.Aggregate((s1, s2) => s1 + "," + s2);
        }

        public Task RevokeAsync(string subject, string client)
        {
            _repo.DeleteConsentBySubjectAndClient(subject, client);
            return Task.FromResult(0);
        }
    }
}
