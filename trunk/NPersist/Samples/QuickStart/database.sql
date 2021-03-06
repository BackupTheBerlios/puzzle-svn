if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tblAuthor_Book_Authors_AuthorId]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Authors_Books] DROP CONSTRAINT FK_tblAuthor_Book_Authors_AuthorId
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tblAuthor_Book_BookId]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Authors_Books] DROP CONSTRAINT FK_tblAuthor_Book_BookId
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tblReview_Book_BookId]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Reviews] DROP CONSTRAINT FK_tblReview_Book_BookId
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Authors]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Authors]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Authors_Books]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Authors_Books]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Books]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Books]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Reviews]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Reviews]
GO

CREATE TABLE [dbo].[Authors] (
	[AuthorId] [int] IDENTITY (1, 1) NOT NULL ,
	[FirstName] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[LastName] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Authors_Books] (
	[AuthorId] [int] NULL ,
	[BookId] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Books] (
	[BookId] [int] IDENTITY (1, 1) NOT NULL ,
	[Isbn] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Title] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Reviews] (
	[ReviewId] [int] IDENTITY (1, 1) NOT NULL ,
	[Body] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[BookId] [int] NOT NULL ,
	[Grade] [int] NOT NULL ,
	[Reviewer] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Authors] WITH NOCHECK ADD 
	CONSTRAINT [PK_Authors] PRIMARY KEY  CLUSTERED 
	(
		[AuthorId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Books] WITH NOCHECK ADD 
	CONSTRAINT [PK_Books] PRIMARY KEY  CLUSTERED 
	(
		[BookId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Reviews] WITH NOCHECK ADD 
	CONSTRAINT [PK_Reviews] PRIMARY KEY  CLUSTERED 
	(
		[ReviewId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Authors_Books] ADD 
	CONSTRAINT [FK_tblAuthor_Book_Authors_AuthorId] FOREIGN KEY 
	(
		[AuthorId]
	) REFERENCES [dbo].[Authors] (
		[AuthorId]
	),
	CONSTRAINT [FK_tblAuthor_Book_BookId] FOREIGN KEY 
	(
		[BookId]
	) REFERENCES [dbo].[Books] (
		[BookId]
	)
GO

ALTER TABLE [dbo].[Reviews] ADD 
	CONSTRAINT [FK_tblReview_Book_BookId] FOREIGN KEY 
	(
		[BookId]
	) REFERENCES [dbo].[Books] (
		[BookId]
	)
GO

