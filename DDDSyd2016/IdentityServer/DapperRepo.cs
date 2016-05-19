using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using DDDSyd2016.IdentityServer.Models;

namespace DDDSyd2016.IdentityServer
{
    public class DapperRepo
    {
        private System.Data.IDbConnection GetOpenConnection()
        {
            var conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IdentityServer"].ConnectionString);
            conn.Open();
            return conn;
        }

        public Models.Client GetClient(string clientId)
        {
            using (var conn = GetOpenConnection())
            {
                Models.Client result = conn.Query<Client>(@"SELECT * FROM Auth.Clients WHERE ClientId = @ClientId", new { ClientId = clientId }).FirstOrDefault();
                if (result != null)
                {
                    // not the most efficient way but you get the idea
                    result.ClientRedirectUris = conn.Query<ClientRedirectUri>(@"SELECT * FROM Auth.ClientRedirectUris WHERE Client_Id = @ClientId", new { ClientId = result.Id });
                    result.ClientScopes = conn.Query<string>(@"SELECT Scope FROM Auth.ClientScopes WHERE Client_Id = @ClientId", new { ClientId = result.Id });
                    result.ClientSecrets = conn.Query<ClientSecret>(@"SELECT * FROM Auth.ClientSecrets WHERE Client_Id = @ClientId", new { ClientId = result.Id });
                }
                return result;
            }
        }

        public Token GetToken(string key, int tokenType)
        {
            using (var conn = GetOpenConnection())
            {
                var result = conn.Query<Token>(@"SELECT * FROM Auth.Tokens WHERE TokenKey = @TokenKey and TokenType=@TokenType", new { TokenKey = key, TokenType = tokenType }).FirstOrDefault();
                return result;
            }
        }

        public void DeleteTokenByKey(string key, int tokenType)
        {
            using (var conn = GetOpenConnection())
            {
                var result = conn.Execute(@"delete FROM Auth.Tokens WHERE TokenKey = @TokenKey and TokenType=@TokenType", new { TokenKey = key, TokenType = tokenType });
            }
        }

        public void DeleteTokenBySubjectAndClient(string subject, string client, int tokenType)
        {
            using (var conn = GetOpenConnection())
            {
                var result = conn.Execute(@"delete FROM Auth.Tokens WHERE SubjectId = @SubjectId and ClientId=@ClientId and TokenType=@TokenType", new { SubjectId = subject, ClientId = client, TokenType = tokenType });
            }
        }

        public IEnumerable<Token> GetTokensBySubject(string subject, int tokenType)
        {
            using (var conn = GetOpenConnection())
            {
                var results = conn.Query<Token>(@"SELECT * FROM Auth.Tokens WHERE SubjectId = @Subject and TokenType=@TokenType", new { Subject = subject, TokenType = tokenType });
                return results;
            }
        }

        public Consent GetConsentBySubjectAndClient(string subject, string client)
        {
            using (var conn = GetOpenConnection())
            {
                var result = conn.Query<Consent>(@"SELECT * FROM Auth.Consents WHERE Subject = @Subject and ClientId=@ClientId", new { Subject = subject, ClientId = client }).FirstOrDefault();
                return result;
            }

        }

        public IEnumerable<Consent> GetConsentsBySubject(string subject)
        {
            using (var conn = GetOpenConnection())
            {
                var result = conn.Query<Consent>(@"SELECT * FROM Auth.Consents WHERE Subject = @Subject", new { Subject = subject });
                return result;
            }

        }

        public void DeleteConsentBySubjectAndClient(string subject, string client)
        {
            using (var conn = GetOpenConnection())
            {
                var result = conn.Query<Consent>(@"delete FROM Auth.Consents WHERE Subject = @Subject and ClientId=@ClientId", new { Subject = subject, ClientId = client }).FirstOrDefault();
            }

        }

        public void InsertConsent(Consent consent)
        {
            using (var conn = GetOpenConnection())
            {
                conn.Execute(@"INSERT INTO [Auth].[Consents] ([Subject],[ClientId],[Scopes]) VALUES  (@Subject, @ClientId, @Scopes)", new { Subject = consent.Subject, ClientId = consent.ClientId, Scopes = consent.Scopes });
            }
        }

        public void UpdateConsent(Consent consent)
        {
            using (var conn = GetOpenConnection())
            {
                conn.Execute(@"update [Auth].[Consents] set Scopes=@Scopes WHERE Subject = @Subject and ClientId=@ClientId", new { Subject = consent.Subject, ClientId = consent.ClientId, Scopes = consent.Scopes });
            }
        }

        public IEnumerable<Models.Scope> GetAllScopes()
        {
            using (var conn = GetOpenConnection())
            {
                var result = conn.Query<Models.Scope>(@"SELECT * FROM Auth.Scopes");
                return result;
            }
        }

        public IEnumerable<Models.ScopeClaim> GetAllScopeClaims()
        {
            using (var conn = GetOpenConnection())
            {
                var result = conn.Query<Models.ScopeClaim>(@"SELECT * FROM Auth.ScopeClaims");
                return result;
            }
        }
        public IEnumerable<Models.ScopeSecret> GetAllScopeSecrets()
        {
            using (var conn = GetOpenConnection())
            {
                var result = conn.Query<Models.ScopeSecret>(@"SELECT * FROM Auth.ScopeSecrets");
                return result;
            }
        }

        public User GetUserById(long id)
        {
            using (var conn = GetOpenConnection())
            {
                var results = conn.Query<User>(@"SELECT * FROM Auth.Users WHERE Id = @UserId", new { UserId = id}).FirstOrDefault();
                return results;
            }
        }

        public User GetUserByLoginId(string loginId)
        {
            using (var conn = GetOpenConnection())
            {
                var results = conn.Query<User>(@"SELECT * FROM Auth.Users WHERE LoginId = @LoginId", new { LoginId = loginId }).FirstOrDefault();
                return results;
            }
        }

        public void InsertToken(Token token)
        {
            using (var conn = GetOpenConnection())
            {
                conn.Execute(@"INSERT INTO [Auth].[Tokens]
           ([TokenKey]
           ,[TokenType]
           ,[ClientId]
           ,[SubjectId]
           ,[Expiry]
           ,[JsonCode]
           ,[AuthCodeChallenge]
           ,[AuthCodeChallengeMethod]
           ,[IsOpenId]
           ,[Nonce]
           ,[RedirectUri]
           ,[SessionId]
           ,[WasConsentShown])
     VALUES
           (@key
           ,@TokenType
           ,@ClientId
           ,@SubjectId
           ,@Expiry
           ,@JsonCode
           ,@AuthCodeChallenge
           ,@AuthCodeChallengeMethod
           ,@IsOpenId
           ,@Nonce
           ,@RedirectUri
           ,@SessionId
           ,@WasConsentShown)", new
                {
                    key = token.TokenKey,
                    TokenType = token.TokenType,
                    ClientId = token.ClientId,
                    SubjectId = token.SubjectId,
                    Expiry = token.Expiry,
                    JsonCode = token.JsonCode,
                    AuthCodeChallenge = token.AuthCodeChallenge,
                    AuthCodeChallengeMethod = token.AuthCodeChallengeMethod,
                    IsOpenId = token.IsOpenId,
                    Nonce = token.Nonce,
                    RedirectUri = token.RedirectUri,
                    SessionId = token.SessionId,
                    WasConsentShown = token.WasConsentShown
                });
            }
        }

        public void UpdateTokenExpiry(string key, DateTimeOffset expiry)
        {
            using (var conn = GetOpenConnection())
            {
                conn.Execute(@"update [Auth].[Tokens] set [Expiry] = @Expiry where [TokenKey]=@TokenKey", new
                {
                    TokenKey = key,
                    Expiry = expiry
                });
            }
        }
    }
}