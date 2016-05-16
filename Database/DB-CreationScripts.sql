GO


/****** Object:  Schema [BankFeeds]    Script Date: 4/02/2016 3:22:37 PM ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'Auth')
EXEC sys.sp_executesql N'CREATE SCHEMA [Auth]'

GO


/****** Object:  Table [Auth].[ClientClaims]    Script Date: 4/02/2016 3:23:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Auth].[ClientClaims]') AND type in (N'U'))
BEGIN
CREATE TABLE [Auth].[ClientClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](250) NOT NULL,
	[Value] [nvarchar](250) NOT NULL,
	[Client_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ClientClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Auth].[ClientCorsOrigins]    Script Date: 4/02/2016 3:23:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Auth].[ClientCorsOrigins]') AND type in (N'U'))
BEGIN
CREATE TABLE [Auth].[ClientCorsOrigins](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Origin] [nvarchar](150) NOT NULL,
	[Client_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ClientCorsOrigins] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Auth].[ClientCustomGrantTypes]    Script Date: 4/02/2016 3:23:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Auth].[ClientCustomGrantTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [Auth].[ClientCustomGrantTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GrantType] [nvarchar](250) NOT NULL,
	[Client_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ClientCustomGrantTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Auth].[ClientIdPRestrictions]    Script Date: 4/02/2016 3:23:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Auth].[ClientIdPRestrictions]') AND type in (N'U'))
BEGIN
CREATE TABLE [Auth].[ClientIdPRestrictions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Provider] [nvarchar](200) NOT NULL,
	[Client_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ClientIdPRestrictions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Auth].[ClientPostLogoutRedirectUris]    Script Date: 4/02/2016 3:23:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Auth].[ClientPostLogoutRedirectUris]') AND type in (N'U'))
BEGIN
CREATE TABLE [Auth].[ClientPostLogoutRedirectUris](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uri] [nvarchar](2000) NOT NULL,
	[Client_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ClientPostLogoutRedirectUris] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Auth].[ClientRedirectUris]    Script Date: 4/02/2016 3:23:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Auth].[ClientRedirectUris]') AND type in (N'U'))
BEGIN
CREATE TABLE [Auth].[ClientRedirectUris](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uri] [nvarchar](2000) NOT NULL,
	[Client_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ClientRedirectUris] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Auth].[Clients]    Script Date: 4/02/2016 3:23:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Auth].[Clients]') AND type in (N'U'))
BEGIN
CREATE TABLE [Auth].[Clients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[ClientId] [nvarchar](200) NOT NULL,
	[ClientName] [nvarchar](200) NOT NULL,
	[ClientUri] [nvarchar](2000) NULL,
	[LogoUri] [nvarchar](max) NULL,
	[RequireConsent] [bit] NOT NULL,
	[AllowRememberConsent] [bit] NOT NULL,
	[Flow] [int] NOT NULL,
	[AllowClientCredentialsOnly] [bit] NOT NULL,
	[LogoutUri] [nvarchar](max) NULL,
	[LogoutSessionRequired] [bit] NOT NULL,
	[RequireSignOutPrompt] [bit] NOT NULL,
	[AllowAccessToAllScopes] [bit] NOT NULL,
	[IdentityTokenLifetime] [int] NOT NULL,
	[AccessTokenLifetime] [int] NOT NULL,
	[AuthorizationCodeLifetime] [int] NOT NULL,
	[AbsoluteRefreshTokenLifetime] [int] NOT NULL,
	[SlidingRefreshTokenLifetime] [int] NOT NULL,
	[RefreshTokenUsage] [int] NOT NULL,
	[UpdateAccessTokenOnRefresh] [bit] NOT NULL,
	[RefreshTokenExpiration] [int] NOT NULL,
	[AccessTokenType] [int] NOT NULL,
	[EnableLocalLogin] [bit] NOT NULL,
	[IncludeJwtId] [bit] NOT NULL,
	[AlwaysSendClientClaims] [bit] NOT NULL,
	[PrefixClientClaims] [bit] NOT NULL,
	[AllowAccessToAllGrantTypes] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Clients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [Auth].[ClientScopes]    Script Date: 4/02/2016 3:23:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Auth].[ClientScopes]') AND type in (N'U'))
BEGIN
CREATE TABLE [Auth].[ClientScopes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Scope] [nvarchar](200) NOT NULL,
	[Client_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ClientScopes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Auth].[ClientSecrets]    Script Date: 4/02/2016 3:23:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Auth].[ClientSecrets]') AND type in (N'U'))
BEGIN
CREATE TABLE [Auth].[ClientSecrets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](250) NOT NULL,
	[Type] [nvarchar](250) NULL,
	[Description] [nvarchar](2000) NULL,
	[Expiration] [datetimeoffset](7) NULL,
	[Client_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ClientSecrets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Auth].[ScopeClaims]    Script Date: 4/02/2016 3:23:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Auth].[ScopeClaims]') AND type in (N'U'))
BEGIN
CREATE TABLE [Auth].[ScopeClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[AlwaysIncludeInIdToken] [bit] NOT NULL,
	[Scope_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ScopeClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Auth].[Scopes]    Script Date: 4/02/2016 3:23:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Auth].[Scopes]') AND type in (N'U'))
BEGIN
CREATE TABLE [Auth].[Scopes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[DisplayName] [nvarchar](200) NULL,
	[Description] [nvarchar](1000) NULL,
	[Required] [bit] NOT NULL,
	[Emphasize] [bit] NOT NULL,
	[Type] [int] NOT NULL,
	[IncludeAllClaimsForUser] [bit] NOT NULL,
	[ClaimsRule] [nvarchar](200) NULL,
	[ShowInDiscoveryDocument] [bit] NOT NULL,
	[AllowUnrestrictedIntrospection] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Scopes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Auth].[ScopeSecrets]    Script Date: 4/02/2016 3:23:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Auth].[ScopeSecrets]') AND type in (N'U'))
BEGIN
CREATE TABLE [Auth].[ScopeSecrets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[Expiration] [datetimeoffset](7) NULL,
	[Type] [nvarchar](250) NULL,
	[Value] [nvarchar](250) NOT NULL,
	[Scope_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ScopeSecrets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Auth].[FK_dbo.ClientClaims_dbo.Clients_Client_Id]') AND parent_object_id = OBJECT_ID(N'[Auth].[ClientClaims]'))
ALTER TABLE [Auth].[ClientClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ClientClaims_dbo.Clients_Client_Id] FOREIGN KEY([Client_Id])
REFERENCES [Auth].[Clients] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Auth].[FK_dbo.ClientClaims_dbo.Clients_Client_Id]') AND parent_object_id = OBJECT_ID(N'[Auth].[ClientClaims]'))
ALTER TABLE [Auth].[ClientClaims] CHECK CONSTRAINT [FK_dbo.ClientClaims_dbo.Clients_Client_Id]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Auth].[FK_dbo.ClientCorsOrigins_dbo.Clients_Client_Id]') AND parent_object_id = OBJECT_ID(N'[Auth].[ClientCorsOrigins]'))
ALTER TABLE [Auth].[ClientCorsOrigins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ClientCorsOrigins_dbo.Clients_Client_Id] FOREIGN KEY([Client_Id])
REFERENCES [Auth].[Clients] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Auth].[FK_dbo.ClientCorsOrigins_dbo.Clients_Client_Id]') AND parent_object_id = OBJECT_ID(N'[Auth].[ClientCorsOrigins]'))
ALTER TABLE [Auth].[ClientCorsOrigins] CHECK CONSTRAINT [FK_dbo.ClientCorsOrigins_dbo.Clients_Client_Id]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Auth].[FK_dbo.ClientCustomGrantTypes_dbo.Clients_Client_Id]') AND parent_object_id = OBJECT_ID(N'[Auth].[ClientCustomGrantTypes]'))
ALTER TABLE [Auth].[ClientCustomGrantTypes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ClientCustomGrantTypes_dbo.Clients_Client_Id] FOREIGN KEY([Client_Id])
REFERENCES [Auth].[Clients] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Auth].[FK_dbo.ClientCustomGrantTypes_dbo.Clients_Client_Id]') AND parent_object_id = OBJECT_ID(N'[Auth].[ClientCustomGrantTypes]'))
ALTER TABLE [Auth].[ClientCustomGrantTypes] CHECK CONSTRAINT [FK_dbo.ClientCustomGrantTypes_dbo.Clients_Client_Id]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Auth].[FK_dbo.ClientIdPRestrictions_dbo.Clients_Client_Id]') AND parent_object_id = OBJECT_ID(N'[Auth].[ClientIdPRestrictions]'))
ALTER TABLE [Auth].[ClientIdPRestrictions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ClientIdPRestrictions_dbo.Clients_Client_Id] FOREIGN KEY([Client_Id])
REFERENCES [Auth].[Clients] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Auth].[FK_dbo.ClientIdPRestrictions_dbo.Clients_Client_Id]') AND parent_object_id = OBJECT_ID(N'[Auth].[ClientIdPRestrictions]'))
ALTER TABLE [Auth].[ClientIdPRestrictions] CHECK CONSTRAINT [FK_dbo.ClientIdPRestrictions_dbo.Clients_Client_Id]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Auth].[FK_dbo.ClientPostLogoutRedirectUris_dbo.Clients_Client_Id]') AND parent_object_id = OBJECT_ID(N'[Auth].[ClientPostLogoutRedirectUris]'))
ALTER TABLE [Auth].[ClientPostLogoutRedirectUris]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ClientPostLogoutRedirectUris_dbo.Clients_Client_Id] FOREIGN KEY([Client_Id])
REFERENCES [Auth].[Clients] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Auth].[FK_dbo.ClientPostLogoutRedirectUris_dbo.Clients_Client_Id]') AND parent_object_id = OBJECT_ID(N'[Auth].[ClientPostLogoutRedirectUris]'))
ALTER TABLE [Auth].[ClientPostLogoutRedirectUris] CHECK CONSTRAINT [FK_dbo.ClientPostLogoutRedirectUris_dbo.Clients_Client_Id]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Auth].[FK_dbo.ClientRedirectUris_dbo.Clients_Client_Id]') AND parent_object_id = OBJECT_ID(N'[Auth].[ClientRedirectUris]'))
ALTER TABLE [Auth].[ClientRedirectUris]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ClientRedirectUris_dbo.Clients_Client_Id] FOREIGN KEY([Client_Id])
REFERENCES [Auth].[Clients] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Auth].[FK_dbo.ClientRedirectUris_dbo.Clients_Client_Id]') AND parent_object_id = OBJECT_ID(N'[Auth].[ClientRedirectUris]'))
ALTER TABLE [Auth].[ClientRedirectUris] CHECK CONSTRAINT [FK_dbo.ClientRedirectUris_dbo.Clients_Client_Id]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Auth].[FK_dbo.ClientScopes_dbo.Clients_Client_Id]') AND parent_object_id = OBJECT_ID(N'[Auth].[ClientScopes]'))
ALTER TABLE [Auth].[ClientScopes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ClientScopes_dbo.Clients_Client_Id] FOREIGN KEY([Client_Id])
REFERENCES [Auth].[Clients] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Auth].[FK_dbo.ClientScopes_dbo.Clients_Client_Id]') AND parent_object_id = OBJECT_ID(N'[Auth].[ClientScopes]'))
ALTER TABLE [Auth].[ClientScopes] CHECK CONSTRAINT [FK_dbo.ClientScopes_dbo.Clients_Client_Id]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Auth].[FK_dbo.ClientSecrets_dbo.Clients_Client_Id]') AND parent_object_id = OBJECT_ID(N'[Auth].[ClientSecrets]'))
ALTER TABLE [Auth].[ClientSecrets]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ClientSecrets_dbo.Clients_Client_Id] FOREIGN KEY([Client_Id])
REFERENCES [Auth].[Clients] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Auth].[FK_dbo.ClientSecrets_dbo.Clients_Client_Id]') AND parent_object_id = OBJECT_ID(N'[Auth].[ClientSecrets]'))
ALTER TABLE [Auth].[ClientSecrets] CHECK CONSTRAINT [FK_dbo.ClientSecrets_dbo.Clients_Client_Id]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Auth].[FK_dbo.ScopeClaims_dbo.Scopes_Scope_Id]') AND parent_object_id = OBJECT_ID(N'[Auth].[ScopeClaims]'))
ALTER TABLE [Auth].[ScopeClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ScopeClaims_dbo.Scopes_Scope_Id] FOREIGN KEY([Scope_Id])
REFERENCES [Auth].[Scopes] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Auth].[FK_dbo.ScopeClaims_dbo.Scopes_Scope_Id]') AND parent_object_id = OBJECT_ID(N'[Auth].[ScopeClaims]'))
ALTER TABLE [Auth].[ScopeClaims] CHECK CONSTRAINT [FK_dbo.ScopeClaims_dbo.Scopes_Scope_Id]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Auth].[FK_dbo.ScopeSecrets_dbo.Scopes_Scope_Id]') AND parent_object_id = OBJECT_ID(N'[Auth].[ScopeSecrets]'))
ALTER TABLE [Auth].[ScopeSecrets]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ScopeSecrets_dbo.Scopes_Scope_Id] FOREIGN KEY([Scope_Id])
REFERENCES [Auth].[Scopes] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Auth].[FK_dbo.ScopeSecrets_dbo.Scopes_Scope_Id]') AND parent_object_id = OBJECT_ID(N'[Auth].[ScopeSecrets]'))
ALTER TABLE [Auth].[ScopeSecrets] CHECK CONSTRAINT [FK_dbo.ScopeSecrets_dbo.Scopes_Scope_Id]
GO



/**********************************************************************************************************************/
/******************************** Non Identity Server specific tables *************************************************/
/**********************************************************************************************************************/

/****** Object:  Table [Auth].[Developer]    Script Date: 17/02/2016 10:36:56 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Auth].[Developer]') AND type IN (N'U'))
BEGIN
CREATE TABLE [Auth].[Developer](
	[Id] BIGINT IDENTITY(1,1) NOT NULL,
	[LoginId] [NVARCHAR](128) NOT NULL,
	[IsActive] [BIT] NOT NULL DEFAULT 1,
	[IsAccountValidated] [BIT] NOT NULL DEFAULT 0,
	[ValidationToken] [NVARCHAR](256) NOT NULL,
	[ValidationTokenExpiry] DATETIME NULL,
	[PasswordHashed] [NVARCHAR](250) NOT NULL,
	[PasswordSalt] [NVARCHAR](16),
	[VersionId] [ROWVERSION],
	[DateCreated] [DATETIME] DEFAULT GETUTCDATE() NOT NULL,
	[DateModified] [DATETIME] DEFAULT GETUTCDATE() NULL,
	[LastLoginDate] [DATETIME] DEFAULT GETUTCDATE() NULL,
	[Name] NVARCHAR(200) NOT NULL,
	[LogoUri] NVARCHAR(2000) NULL,
	[WebsiteUri] NVARCHAR(2000) NULL,
	[Phone] NVARCHAR(30) NULL
 CONSTRAINT [PK_dbo.Developer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 
END
GO

/****** Object:  Table [Auth].[DeveloperClients]    Script Date: 17/02/2016 12:41:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Auth].[DeveloperClients]') AND type IN (N'U'))
BEGIN
CREATE TABLE [Auth].[DeveloperClients](
	[Id] [BIGINT] IDENTITY(1,1) NOT NULL,
	[DeveloperId] [BIGINT] NOT NULL,
	[ClientId] [INT] NOT NULL,
 CONSTRAINT [PK_dbo.DeveloperClients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Auth].[FK_DeveloperClients_Clients]') AND parent_object_id = OBJECT_ID(N'[Auth].[DeveloperClients]'))
ALTER TABLE [Auth].[DeveloperClients]  WITH CHECK ADD  CONSTRAINT [FK_DeveloperClients_Clients] FOREIGN KEY([ClientId])
REFERENCES [Auth].[Clients] ([Id])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Auth].[FK_DeveloperClients_Clients]') AND parent_object_id = OBJECT_ID(N'[Auth].[DeveloperClients]'))
ALTER TABLE [Auth].[DeveloperClients] CHECK CONSTRAINT [FK_DeveloperClients_Clients]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Auth].[FK_DeveloperClients_Developer]') AND parent_object_id = OBJECT_ID(N'[Auth].[DeveloperClients]'))
ALTER TABLE [Auth].[DeveloperClients]  WITH CHECK ADD  CONSTRAINT [FK_DeveloperClients_Developer] FOREIGN KEY([DeveloperId])
REFERENCES [Auth].[Developer] ([Id])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Auth].[FK_DeveloperClients_Developer]') AND parent_object_id = OBJECT_ID(N'[Auth].[DeveloperClients]'))
ALTER TABLE [Auth].[DeveloperClients] CHECK CONSTRAINT [FK_DeveloperClients_Developer]
GO



/****** Object:  StoredProcedure [Auth].[usp_GetClientForDeveloperLogin]    Script Date: 1/03/2016 9:48:48 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Auth].[usp_GetClientForDeveloperLogin]') AND type IN (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [Auth].[usp_GetClientForDeveloperLogin] AS' 
END
GO

ALTER PROCEDURE [Auth].[usp_GetClientForDeveloperLogin]
	@loginId NVARCHAR(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT C.* FROM Auth.Developer I
		INNER JOIN Auth.DeveloperClients IC ON IC.DeveloperId = I.Id
		INNER JOIN Auth.Clients C ON C.Id = IC.ClientId
		WHERE I.LoginId = @loginId
END

GO



/****** Object:  Table [Auth].[Tokens]    Script Date: 8/03/2016 7:41:06 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Auth].[Tokens]') AND type IN (N'U'))
BEGIN
CREATE TABLE [Auth].[Tokens](
	[TokenKey] [NVARCHAR](128) NOT NULL,
	[TokenType] [INT] NOT NULL,
	[ClientId] [NVARCHAR](200) NOT NULL,
	[SubjectId] [NVARCHAR](200) NULL,
	[Expiry] [DATETIMEOFFSET](7) NOT NULL,
	[JsonCode] [NVARCHAR](3072) NULL,
	[AuthCodeChallenge] [NVARCHAR](250) NULL,
	[AuthCodeChallengeMethod] [NVARCHAR](50) NULL,
	[IsOpenId] [BIT] NULL,
	[Nonce] [NVARCHAR](200) NULL,
	[RedirectUri] [NVARCHAR](2000) NULL,
	[SessionId] [NVARCHAR](200) NULL,
	[WasConsentShown] [BIT] NULL,
 CONSTRAINT [PK_Tokens] PRIMARY KEY CLUSTERED 
(
	[TokenKey] ASC,
	[TokenType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO


/****** Object:  Table [Auth].[Consents]    Script Date: 8/03/2016 7:41:14 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Auth].[Consents]') AND type IN (N'U'))
BEGIN
CREATE TABLE [Auth].[Consents](
	[Subject] [NVARCHAR](200) NOT NULL,
	[ClientId] [NVARCHAR](200) NOT NULL,
	[Scopes] [NVARCHAR](2000) NOT NULL,
 CONSTRAINT [PK_Consents] PRIMARY KEY CLUSTERED 
(
	[Subject] ASC,
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO


