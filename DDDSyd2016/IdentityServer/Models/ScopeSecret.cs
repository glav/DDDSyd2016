using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDDSyd2016.IdentityServer.Models
{
    public class ScopeSecret
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTimeOffset> Expiration { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public int Scope_Id { get; set; }

        public virtual Scope Scope { get; set; }
    }
}