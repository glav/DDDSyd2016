using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDDSyd2016.IdentityServer.Models
{
    public class Scope
    {
        public Scope()
        {
            this.ScopeClaims = new HashSet<ScopeClaim>();
            this.ScopeSecrets = new HashSet<ScopeSecret>();
        }

        public int Id { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public int Type { get; set; }
        public bool IncludeAllClaimsForUser { get; set; }
        public string ClaimsRule { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public bool AllowUnrestrictedIntrospection { get; set; }

        public virtual ICollection<ScopeClaim> ScopeClaims { get; set; }
        public virtual ICollection<ScopeSecret> ScopeSecrets { get; set; }
    }
}