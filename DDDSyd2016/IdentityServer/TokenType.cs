using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDDSyd2016.IdentityServer
{
    public enum TokenType : short
    {
        AuthorizationCode = 1,
        TokenHandle = 2,
        RefreshToken = 3
    }
}