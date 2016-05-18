using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDDSyd2016.IdentityServer.Models
{
    public class Token
    {
        public string TokenKey { get; set; }
        public int TokenType { get; set; }
        public string ClientId { get; set; }
        public string SubjectId { get; set; }
        public System.DateTimeOffset Expiry { get; set; }
        public string JsonCode { get; set; }
        public string AuthCodeChallenge { get; set; }
        public string AuthCodeChallengeMethod { get; set; }
        public Nullable<bool> IsOpenId { get; set; }
        public string Nonce { get; set; }
        public string RedirectUri { get; set; }
        public string SessionId { get; set; }
        public Nullable<bool> WasConsentShown { get; set; }
    }
}