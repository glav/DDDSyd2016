using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDDSyd2016.IdentityServer.Models
{
    public class ClientSecret
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTimeOffset> Expiration { get; set; }
        public int Client_Id { get; set; }
    }
}