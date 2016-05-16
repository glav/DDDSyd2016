using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerTestApplication
{
    public class AuthCodeCallbackHandler
    {
        public AuthCodeCallbackHandler()
        {
        }

        public async Task<OAuthAccessTokenGrant> HandleAuthCodeCallbackAsync(string code, string state)
        {
            Output.Write("Constructing HTTP Access Token request to IdentityServer");
            HttpClient client = new HttpClient();
            var fullBasicAuth = Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes($"{Config.ClientId}:{Config.ClientSecret}"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", fullBasicAuth);
            var body = $"grant_type=authorization_code&code={code}&clientId={Config.ClientId}&redirect_uri={Config.CallbackAbsoluteEndpointUrl}";
            var content = new System.Net.Http.StringContent(body);
            content.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";

            var accessTokenUrl = $"{Config.IdentityServerEndpoint}connect/token";
            Output.Write($"Performing access token request to: [ {accessTokenUrl} ]");
            try
            {
                var identityResult = await client.PostAsync(accessTokenUrl, content);
                var numericCode = (int)identityResult.StatusCode;
                Output.Write($"Request complete. Status Code: [{identityResult.StatusCode} ({numericCode})], message:[{identityResult.ReasonPhrase}]");
                if ((int)identityResult.StatusCode < 300)
                {
                    Output.Write("Request successful, obtaining access token details.");
                    var grant = await identityResult.Content.ReadAsAsync<OAuthAccessTokenGrant>();
                    Output.Write($"Access token details:{System.Environment.NewLine} token=[{grant.access_token}], refresh_token=[{grant.refresh_token}], expires_in=[{grant.expires_in}], scope=[{grant.scope}]");
                    return grant;

                } else
                {
                    Output.Write("Status code indicates failure. Full response body is:",InfoType.Error);
                    string result;
                    if (identityResult.TryGetContentValue<string>(out result))
                    {
                        Output.Write(result);
                    } else
                    {
                        Output.Write("- No content in body response -");
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Output.Write($"Error performing access token request. Exception: [{ex.Message}]", InfoType.Error);
            }
            return null;

        }

        public async Task ValidateAccessTokenAsync(string token)
        {
            Output.Write("Constructing HTTP Validate Token request to IdentityServer");
            var httpClient = new HttpClient();
            var url = $"{Config.IdentityServerEndpoint}connect/accesstokenvalidation?token=" + token;

            try
            {
                var result = await httpClient.GetAsync(url);
                var numericCode = (int)result.StatusCode;
                Output.Write($"Request complete.");
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Output.Write($"Error: StatusCode:{result.StatusCode}, Message:{result.ReasonPhrase}", InfoType.Error);
                }
                else
                {
                    Output.Write("Token validated. Contents are:");
                    Output.Write(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                Output.Write($"Error Exception: {ex.Message}", InfoType.Error);
            }
        }

    }
}
