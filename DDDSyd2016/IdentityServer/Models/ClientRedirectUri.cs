using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDDSyd2016.IdentityServer.Models
{
    public class ClientRedirectUri
    {
        public int Id { get; set; }
        public string Uri { get; set; }
        public int Client_Id { get; set; }
    }
}