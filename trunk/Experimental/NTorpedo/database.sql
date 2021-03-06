if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_AUCTION_ITEM]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[AUCTION] DROP CONSTRAINT FK_AUCTION_ITEM
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_BID_AUCTION]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[BID] DROP CONSTRAINT FK_BID_AUCTION
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_AUCTION_USER]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[AUCTION] DROP CONSTRAINT FK_AUCTION_USER
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_BID_USER]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[BID] DROP CONSTRAINT FK_BID_USER
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ITEM]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ITEM]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AUCTION]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[AUCTION]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BID]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[BID]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[USER]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[USER]
GO

CREATE TABLE [dbo].[ITEM] (
	[ITEM_ID] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ITEM_NAME] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[GRAPHIC_FILENAME] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[DESCRIPTION] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[VERSION] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[AUCTION] (
	[AUCTION_ID] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[SELLER] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ITEM] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[LOW_PRICE] [float] NOT NULL ,
	[VERSION] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[BID] (
	[BID_ID] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[AUCTION] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[BUYER] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[AMOUNT] [float] NOT NULL ,
	[MAX_AMOUNT] [float] NOT NULL ,
	[VERSION] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[USER] (
	[USER_ID] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[VERSION] [int] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ITEM] WITH NOCHECK ADD 
	CONSTRAINT [PK_ITEM] PRIMARY KEY  CLUSTERED 
	(
		[ITEM_ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[AUCTION] WITH NOCHECK ADD 
	CONSTRAINT [PK_AUCTION] PRIMARY KEY  CLUSTERED 
	(
		[AUCTION_ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[BID] WITH NOCHECK ADD 
	CONSTRAINT [PK_BID] PRIMARY KEY  CLUSTERED 
	(
		[BID_ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[USER] WITH NOCHECK ADD 
	CONSTRAINT [PK_USER] PRIMARY KEY  CLUSTERED 
	(
		[USER_ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[AUCTION] ADD 
	CONSTRAINT [FK_AUCTION_ITEM] FOREIGN KEY 
	(
		[ITEM]
	) REFERENCES [dbo].[ITEM] (
		[ITEM_ID]
	),
	CONSTRAINT [FK_AUCTION_USER] FOREIGN KEY 
	(
		[SELLER]
	) REFERENCES [dbo].[USER] (
		[USER_ID]
	)
GO

ALTER TABLE [dbo].[BID] ADD 
	CONSTRAINT [FK_BID_AUCTION] FOREIGN KEY 
	(
		[AUCTION]
	) REFERENCES [dbo].[AUCTION] (
		[AUCTION_ID]
	),
	CONSTRAINT [FK_BID_USER] FOREIGN KEY 
	(
		[BUYER]
	) REFERENCES [dbo].[USER] (
		[USER_ID]
	)
GO

