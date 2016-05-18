using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDSyd2016.IdentityServer.CustomServices
{
    public class CustomClientPermissionsService : IClientPermissionsService
    {
        readonly IPermissionsStore permissionsStore;
        readonly IClientStore clientStore;
        readonly IScopeStore scopeStore;
        readonly ILocalizationService localizationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultClientPermissionsService" /> class.
        /// </summary>
        /// <param name="permissionsStore">The permissions store.</param>
        /// <param name="clientStore">The client store.</param>
        /// <param name="scopeStore">The scope store.</param>
        /// <param name="localizationService">The localization service.</param>
        /// <exception cref="System.ArgumentNullException">permissionsStore
        /// or
        /// clientStore
        /// or
        /// scopeStore</exception>
        public CustomClientPermissionsService(IPermissionsStore permissionsStore,IClientStore clientStore,IScopeStore scopeStore,ILocalizationService localizationService)
        {
            if (permissionsStore == null) throw new ArgumentNullException("permissionsStore");
            if (clientStore == null) throw new ArgumentNullException("clientStore");
            if (scopeStore == null) throw new ArgumentNullException("scopeStore");
            if (localizationService == null) throw new ArgumentNullException("localizationService");

            this.permissionsStore = permissionsStore;
            this.clientStore = clientStore;
            this.scopeStore = scopeStore;
            this.localizationService = localizationService;
        }

        /// <summary>
        /// Gets the client permissions asynchronous.
        /// </summary>
        /// <param name="subject">The subject identifier.</param>
        /// <returns>
        /// A list of client permissions
        /// </returns>
        /// <exception cref="System.ArgumentNullException">subject</exception>
        public virtual async Task<IEnumerable<ClientPermission>> GetClientPermissionsAsync(string subject)
        {
            if (String.IsNullOrWhiteSpace(subject))
            {
                throw new ArgumentNullException("subject");
            }

            var consents = await this.permissionsStore.LoadAllAsync(subject);

            var scopesNeeded = consents.Select(x => x.Scopes).SelectMany(x => x).Distinct();
            var scopes = await scopeStore.FindScopesAsync(scopesNeeded);

            var list = new List<ClientPermission>();
            foreach (var consent in consents)
            {
                var client = await clientStore.FindClientByIdAsync(consent.ClientId);
                if (client != null)
                {
                    var identityScopes =
                        from s in scopes
                        where s.Type == ScopeType.Identity && consent.Scopes.Contains(s.Name)
                        select new ClientPermissionDescription
                        {
                            DisplayName = s.DisplayName ?? localizationService.GetScopeDisplayName(s.Name),
                            Description = s.Description ?? localizationService.GetScopeDescription(s.Name)
                        };

                    var resourceScopes =
                        from s in scopes
                        where s.Type == ScopeType.Resource && consent.Scopes.Contains(s.Name)
                        select new ClientPermissionDescription
                        {
                            DisplayName = s.DisplayName ?? localizationService.GetScopeDisplayName(s.Name),
                            Description = s.Description ?? localizationService.GetScopeDescription(s.Name)
                        };

                    list.Add(new ClientPermission
                    {
                        ClientId = client.ClientId,
                        ClientName = client.ClientName,
                        ClientUrl = client.ClientUri,
                        ClientLogoUrl = client.LogoUri,
                        IdentityPermissions = identityScopes,
                        ResourcePermissions = resourceScopes
                    });
                }
            }

            return list;
        }

        /// <summary>
        /// Revokes the client permissions asynchronous.
        /// </summary>
        /// <param name="subject">The subject identifier.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// subject
        /// or
        /// clientId
        /// </exception>
        public virtual async Task RevokeClientPermissionsAsync(string subject, string clientId)
        {
            if (String.IsNullOrWhiteSpace(subject))
            {
                throw new ArgumentNullException("subject");
            }

            if (String.IsNullOrWhiteSpace(clientId))
            {
                throw new ArgumentNullException("clientId");
            }

            await this.permissionsStore.RevokeAsync(subject, clientId);
        }
    }
}
