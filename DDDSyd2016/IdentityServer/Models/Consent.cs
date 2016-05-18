using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDDSyd2016.IdentityServer.Models
{
    public class Consent
    {
        public string Subject { get; set; }
        public string ClientId { get; set; }
        public string Scopes { get; set; }
    }
}