using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDDSyd2016.IdentityServer.Models
{
    public class ScopeClaim
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool AlwaysIncludeInIdToken { get; set; }
        public int Scope_Id { get; set; }

        public virtual Scope Scope { get; set; }
    }
}