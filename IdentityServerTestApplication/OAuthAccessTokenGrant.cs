using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerTestApplication
{
    public class OAuthAccessTokenGrant
    {
        /// <summary>
        /// The access token when authentication is successfull. This is typically a short lived token and expires in a relatively short time.
        /// </summary>
		public string access_token { get; set; }
        /// <summary>
        /// The type of token returned. Currently, only 'Bearer' is supported.
        /// </summary>
		public string token_type { get; set; }
        /// <summary>
        /// The time in seconds when the access token expires. Another access token can be requested using the refresh token.
        /// </summary>
		public int expires_in { get; set; }
        /// <summary>
        /// The refresh token issued as part of a successfull authentication request. This token is typically long lived, and may not expire at all.
        /// </summary>
		public string refresh_token { get; set; }
        /// <summary>
        /// The scope of access granted. Currently, this is the scope access descriptor (eg. "full") and a list of space separated id's that 
        /// represent each file  that the authenticated user has access to in the form "full fileid:1 fileid:1234 fileid:567"
        /// </summary>
		public string scope { get; set; }
    }
}
