using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDDSyd2016.IdentityServer.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string LoginId { get; set; }
        public bool IsActive { get; set; }
        public string PasswordHash { get; set; }
    }
}