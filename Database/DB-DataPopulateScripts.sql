
-- Fill up our scopes table
IF NOT EXISTS (SELECT 1 FROM [Auth].[Scopes] WHERE Name='openid') 
BEGIN
	SET IDENTITY_INSERT [Auth].[Scopes] ON
	INSERT INTO [Auth].[Scopes]
	([Id]
      ,[Enabled]
      ,[Name]
      ,[DisplayName]
      ,[Description]
      ,[Required]
      ,[Emphasize]
      ,[Type]
      ,[IncludeAllClaimsForUser]  -- note: We set this to 1/true in most cases to ensure the FileIds claim is included when validating the token
      ,[ClaimsRule]
      ,[ShowInDiscoveryDocument]
      ,[AllowUnrestrictedIntrospection])
	VALUES
	( 1,1, N'openid', NULL, NULL, 1, 0, 0, 1, NULL, 1, 0 ), 
	( 2,1, N'profile', NULL, NULL, 0, 1, 0, 1, NULL, 1, 0 ), 
	( 3,1, N'email', NULL, NULL, 0, 1, 0, 0, NULL, 1, 0 ), 
	( 4,1, N'offline_access', N'Constant access to your data without re-authorising', NULL, 0, 1, 1, 1, NULL, 1, 0 ), 
	( 5,1, N'read', N'Read data', NULL, 0, 0, 1, 1, NULL, 1, 0 ), 
	( 6,1, N'write', N'Write data', NULL, 0, 1, 1, 1, NULL, 1, 0 )
	SET IDENTITY_INSERT [Auth].[Scopes] OFF
END
GO


-- Set a Local developer client to use the Authorisation code
IF NOT EXISTS (SELECT 1 FROM [Auth].[Clients] WHERE ClientId='LocalDeveloper')
BEGIN
SET IDENTITY_INSERT [Auth].[Clients] ON
INSERT INTO [Auth].[Clients]
           ([Id]
		   ,[Enabled]
           ,[ClientId]
           ,[ClientName]
           ,[ClientUri]
           ,[LogoUri]
           ,[RequireConsent]
           ,[AllowRememberConsent]
           ,[Flow]
           ,[AllowClientCredentialsOnly]
           ,[LogoutUri]
           ,[LogoutSessionRequired]
           ,[RequireSignOutPrompt]
           ,[AllowAccessToAllScopes]
           ,[IdentityTokenLifetime]
           ,[AccessTokenLifetime]
           ,[AuthorizationCodeLifetime]
           ,[AbsoluteRefreshTokenLifetime]
           ,[SlidingRefreshTokenLifetime]
           ,[RefreshTokenUsage]
           ,[UpdateAccessTokenOnRefresh]
           ,[RefreshTokenExpiration]
           ,[AccessTokenType]
           ,[EnableLocalLogin]
           ,[IncludeJwtId]
           ,[AlwaysSendClientClaims]
           ,[PrefixClientClaims]
           ,[AllowAccessToAllGrantTypes])
     VALUES
           (1
		   ,1
           ,'localdeveloper'
           ,'Local Development Client'
           ,'http://localhost:5000/'
           ,'https://somesite.com/logo/logo.jpg'
           ,1
           ,1
           ,0 -- Authorisation Code
           ,0
           ,null
           ,0
           ,0
           ,1
           ,360 -- 5 minutes
           ,3600 -- 1 hour
           ,360 -- 5 minutes
           ,86400 -- 24 hours
           ,3600
           ,0 -- reuse
           ,1
           ,0 -- absolute (1 == sliding)
           ,1 -- Reference
           ,1
           ,1
           ,1
           ,1
           ,1)
SET IDENTITY_INSERT [Auth].[Clients] OFF
END
GO

IF NOT EXISTS (SELECT 1 FROM [Auth].[Clients] WHERE ClientId='Client1')
BEGIN
SET IDENTITY_INSERT [Auth].[Clients] ON
INSERT INTO [Auth].[Clients]
           ([Id]
		   ,[Enabled]
           ,[ClientId]
           ,[ClientName]
           ,[ClientUri]
           ,[LogoUri]
           ,[RequireConsent]
           ,[AllowRememberConsent]
           ,[Flow]
           ,[AllowClientCredentialsOnly]
           ,[LogoutUri]
           ,[LogoutSessionRequired]
           ,[RequireSignOutPrompt]
           ,[AllowAccessToAllScopes]
           ,[IdentityTokenLifetime]
           ,[AccessTokenLifetime]
           ,[AuthorizationCodeLifetime]
           ,[AbsoluteRefreshTokenLifetime]
           ,[SlidingRefreshTokenLifetime]
           ,[RefreshTokenUsage]
           ,[UpdateAccessTokenOnRefresh]
           ,[RefreshTokenExpiration]
           ,[AccessTokenType]
           ,[EnableLocalLogin]
           ,[IncludeJwtId]
           ,[AlwaysSendClientClaims]
           ,[PrefixClientClaims]
           ,[AllowAccessToAllGrantTypes])
     VALUES
           (2
		   ,1
           ,'Client1'
           ,'Client Numero Uno'
           ,'http://localhost:5000/'
           ,'https://somesite.com/logo/logo.jpg'
           ,1
           ,1
           ,4 -- Resource owner
           ,0
           ,null
           ,0
           ,0
           ,1
           ,360 -- 5 minutes
           ,3600 -- 1 hour
           ,360 -- 5 minutes
           ,86400 -- 24 hours
           ,3600
           ,0 -- reuse
           ,1
           ,0 -- absolute (1 == sliding)
           ,1 -- Reference
           ,1
           ,1
           ,1
           ,1
           ,1)
	SET IDENTITY_INSERT [Auth].[Clients] OFF
END
go


IF NOT EXISTS (SELECT 1 FROM [Auth].[Clients] WHERE ClientId='Client2')
BEGIN
SET IDENTITY_INSERT [Auth].[Clients] ON
INSERT INTO [Auth].[Clients]
           ([Id]
		   ,[Enabled]
           ,[ClientId]
           ,[ClientName]
           ,[ClientUri]
           ,[LogoUri]
           ,[RequireConsent]
           ,[AllowRememberConsent]
           ,[Flow]
           ,[AllowClientCredentialsOnly]
           ,[LogoutUri]
           ,[LogoutSessionRequired]
           ,[RequireSignOutPrompt]
           ,[AllowAccessToAllScopes]
           ,[IdentityTokenLifetime]
           ,[AccessTokenLifetime]
           ,[AuthorizationCodeLifetime]
           ,[AbsoluteRefreshTokenLifetime]
           ,[SlidingRefreshTokenLifetime]
           ,[RefreshTokenUsage]
           ,[UpdateAccessTokenOnRefresh]
           ,[RefreshTokenExpiration]
           ,[AccessTokenType]
           ,[EnableLocalLogin]
           ,[IncludeJwtId]
           ,[AlwaysSendClientClaims]
           ,[PrefixClientClaims]
           ,[AllowAccessToAllGrantTypes])
     VALUES
           (3
		   ,1
           ,'Client2'
           ,'Client Number 2'
           ,'http://localhost:5000/'
           ,'https://somesite.com/logo/logo.jpg'
           ,1
           ,1
           ,0 -- Authorisation Code
           ,0
           ,null
           ,0
           ,0
           ,1
           ,360 -- 5 minutes
           ,3600 -- 1 hour
           ,360 -- 5 minutes
           ,86400 -- 24 hours
           ,3600
           ,0 -- reuse
           ,1
           ,0 -- absolute (1 == sliding)
           ,1 -- Reference
           ,1
           ,1
           ,1
           ,1
           ,1)
	SET IDENTITY_INSERT [Auth].[Clients] OFF
END
go



-- Associate the developer client with some scopes
IF NOT EXISTS (SELECT 1 FROM [Auth].[ClientScopes] WHERE Client_Id=1)
BEGIN
  insert INTO [Auth].[ClientScopes]
	([Scope],[Client_Id])
	VALUES
    ('read',1)

  INSERT INTO [Auth].[ClientScopes]
	([Scope],[Client_Id])
	VALUES
    ('write',1)


	insert INTO [Auth].[ClientScopes]
	([Scope],[Client_Id])
	VALUES
    ('offline_access',1)

END

IF NOT EXISTS (SELECT 1 FROM [Auth].[ClientScopes] WHERE Client_Id=2)
BEGIN
  insert INTO [Auth].[ClientScopes]
	([Scope],[Client_Id])
	VALUES
    ('read',2)

  INSERT INTO [Auth].[ClientScopes]
	([Scope],[Client_Id])
	VALUES
    ('write',2)


	insert INTO [Auth].[ClientScopes]
	([Scope],[Client_Id])
	VALUES
    ('offline_access',2)

END

IF NOT EXISTS (SELECT 1 FROM [Auth].[ClientScopes] WHERE Client_Id=3)
BEGIN
  insert INTO [Auth].[ClientScopes]
	([Scope],[Client_Id])
	VALUES
    ('read',3)

  INSERT INTO [Auth].[ClientScopes]
	([Scope],[Client_Id])
	VALUES
    ('write',3)


	insert INTO [Auth].[ClientScopes]
	([Scope],[Client_Id])
	VALUES
    ('offline_access',3)

END



-- Assocate the developer client with some client secrets. these are 'secret' and 'devsecret' (SHA256)
IF NOT EXISTS (SELECT 1 FROM [Auth].[ClientSecrets] WHERE Client_Id=1)
BEGIN
  insert INTO [Auth].[ClientSecrets]
	([Value],[Client_Id])
	VALUES
    ('K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=',1) -- 'secret'

  INSERT INTO [Auth].[ClientSecrets]
	([Value],[Client_Id])
	VALUES
    ('ICV2xdxqVa0wzHL88j5gSPA1bDR/AEXkpj2hnOmYgvY=',1) -- 'devsecret'
END

IF NOT EXISTS (SELECT 1 FROM [Auth].[ClientSecrets] WHERE Client_Id=2)
BEGIN
  insert INTO [Auth].[ClientSecrets]
	([Value],[Client_Id])
	VALUES
    ('K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=',2) -- 'secret'

END

IF NOT EXISTS (SELECT 1 FROM [Auth].[ClientSecrets] WHERE Client_Id=3)
BEGIN
  insert INTO [Auth].[ClientSecrets]
	([Value],[Client_Id])
	VALUES
    ('K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=',3) -- 'secret'

END



-- Add in a valid callback Uri for the localdeveloper client
IF NOT EXISTS (SELECT 1 FROM [Auth].[ClientRedirectUris] WHERE [Client_Id]=1)
begin
	INSERT INTO [Auth].[ClientRedirectUris]
			   ([Uri]
			   ,[Client_Id])
		 VALUES
			   ('http://localhost:5000/callback'
			   ,1)
	-- Add in more redirect Uri's to support staging, production etc
	
end

IF NOT EXISTS (SELECT 1 FROM [Auth].[ClientRedirectUris] WHERE [Client_Id]=2)
begin
	INSERT INTO [Auth].[ClientRedirectUris]
			   ([Uri]
			   ,[Client_Id])
		 VALUES
			   ('http://localhost:5000/callback'
			   ,2)
	-- Add in more redirect Uri's to support staging, production etc
	
END

IF NOT EXISTS (SELECT 1 FROM [Auth].[ClientRedirectUris] WHERE [Client_Id]=3)
begin
	INSERT INTO [Auth].[ClientRedirectUris]
			   ([Uri]
			   ,[Client_Id])
		 VALUES
			   ('http://localhost:5000/callback'
			   ,3)
	-- Add in more redirect Uri's to support staging, production etc
	
end



-- Add in a valid callback Uri for the localdeveloper client
IF NOT EXISTS (SELECT 1 FROM [Auth].[Users] WHERE [LoginId]='freddy@test.com')
begin
	INSERT INTO [Auth].[Users]
			   ([Firstname], [Surname], [LoginId], [IsActive], [PasswordHash])
		 VALUES
			   ('freddy','Kruger','freddy@test.com',1,'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=') -- passwordhash is 'password'
	
END




