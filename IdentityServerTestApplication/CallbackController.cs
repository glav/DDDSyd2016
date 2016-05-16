using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace IdentityServerTestApplication
{
    public class CallbackController : ApiController
    {
        [HttpGet]
        public async Task<string> Index(string code, string state = null)
        {
            var msg = $"Callback received. 'Authorisation code' of [{code}] and 'state' parameter of [{state}] was passed in.";
            Output.Write(msg);

            var accessTokenHandler = new AuthCodeCallbackHandler();
            var token = await accessTokenHandler.HandleAuthCodeCallbackAsync(code, state);
            if (token != null)
            {
                Output.Write("Successfully obtained a token, now attempting to validate it.");
                await accessTokenHandler.ValidateAccessTokenAsync(token.access_token);
            } else
            {
                Output.Write("Unable to request a token.", InfoType.Error);
            }
            return msg;
        }
    }
}
