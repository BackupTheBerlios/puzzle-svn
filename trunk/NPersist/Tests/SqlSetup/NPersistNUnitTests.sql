if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tblBookInfo_Book_BookId]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblBookInfo] DROP CONSTRAINT FK_tblBookInfo_Book_BookId
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tblClsTblWorkFolder_ClsTblFolderId_ClsTblFolderType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblClsTblWorkFolder] DROP CONSTRAINT FK_tblClsTblWorkFolder_ClsTblFolderId_ClsTblFolderType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tblClsTblEmployee_ClsTblPersonId_ClsTblPersonType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblClsTblEmployee] DROP CONSTRAINT FK_tblClsTblEmployee_ClsTblPersonId_ClsTblPersonType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tblClsTblFolder_Person_ClsTblPersonId_Person_ClsTblPersonType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblClsTblFolder] DROP CONSTRAINT FK_tblClsTblFolder_Person_ClsTblPersonId_Person_ClsTblPersonType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tblClsTblWorkFolder_Employee_ClsTblPersonId_Employee_ClsTblPersonType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblClsTblWorkFolder] DROP CONSTRAINT FK_tblClsTblWorkFolder_Employee_ClsTblPersonId_Employee_ClsTblPersonType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tblCncTblWorkFolder_CncTblFolderId_CncTblFolderType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblCncTblWorkFolder] DROP CONSTRAINT FK_tblCncTblWorkFolder_CncTblFolderId_CncTblFolderType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tblCncTblEmployee_CncTblPersonId_CncTblPersonType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblCncTblEmployee] DROP CONSTRAINT FK_tblCncTblEmployee_CncTblPersonId_CncTblPersonType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Person]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblCncTblFolder] DROP CONSTRAINT FK_Person
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Employee]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblCncTblWorkFolder] DROP CONSTRAINT FK_Employee
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_WF_Person]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblCncTblWorkFolder] DROP CONSTRAINT FK_WF_Person
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tblBook_Cover_CoverId]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblBook] DROP CONSTRAINT FK_tblBook_Cover_CoverId
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tblCyclicB_tblCyclicA]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblCyclicB] DROP CONSTRAINT FK_tblCyclicB_tblCyclicA
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tblCyclicA_tblCyclicB]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblCyclicA] DROP CONSTRAINT FK_tblCyclicA_tblCyclicB
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tblInvCyclicB_tblInvCyclicA]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblInvCyclicB] DROP CONSTRAINT FK_tblInvCyclicB_tblInvCyclicA
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tblInvCyclicA_tblInvCyclicB]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblInvCyclicA] DROP CONSTRAINT FK_tblInvCyclicA_tblInvCyclicB
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tblProfile_tblSections]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblProfile] DROP CONSTRAINT FK_tblProfile_tblSections
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tblSections_tblSections]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblSections] DROP CONSTRAINT FK_tblSections_tblSections
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tblSngTblFolder_Employee_SngTblPersonId_Employee_SngTblPersonType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblSngTblFolder] DROP CONSTRAINT FK_tblSngTblFolder_Employee_SngTblPersonId_Employee_SngTblPersonType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tblSngTblFolder_Person_SngTblPersonId_Person_SngTblPersonType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblSngTblFolder] DROP CONSTRAINT FK_tblSngTblFolder_Person_SngTblPersonId_Person_SngTblPersonType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tblProfile_tblUser]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblProfile] DROP CONSTRAINT FK_tblProfile_tblUser
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FolderID]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FolderID]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblBook]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblBook]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblBookInfo]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblBookInfo]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblClsTblEmployee]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblClsTblEmployee]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblClsTblFolder]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblClsTblFolder]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblClsTblPerson]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblClsTblPerson]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblClsTblWorkFolder]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblClsTblWorkFolder]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblCncTblEmployee]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblCncTblEmployee]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblCncTblFolder]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblCncTblFolder]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblCncTblPerson]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblCncTblPerson]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblCncTblWorkFolder]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblCncTblWorkFolder]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblCover]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblCover]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblCyclicA]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblCyclicA]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblCyclicB]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblCyclicB]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblInvCyclicA]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblInvCyclicA]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblInvCyclicB]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblInvCyclicB]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblProfile]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblProfile]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblSections]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblSections]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblSngTblFolder]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblSngTblFolder]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblSngTblPerson]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblSngTblPerson]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblUser]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblUser]
GO

CREATE TABLE [dbo].[FolderID] (
	[Ap_Code] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Func_Code] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[FolderDescript] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[FolderID] [int] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblBook] (
	[BookId] [int] IDENTITY (1, 1) NOT NULL ,
	[Cover_CoverId] [int] NOT NULL ,
	[Name] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblBookInfo] (
	[BookInfoId] [int] IDENTITY (1, 1) NOT NULL ,
	[Book_BookId] [int] NOT NULL ,
	[ISBN] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblClsTblEmployee] (
	[ClsTblPersonId] [int] NOT NULL ,
	[ClsTblPersonType] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[EmploymentDate] [datetime] NOT NULL ,
	[Salary] [decimal](18, 0) NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblClsTblFolder] (
	[ClsTblFolderId] [int] IDENTITY (1, 1) NOT NULL ,
	[ClsTblFolderType] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Name] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Person_ClsTblPersonId] [int] NULL ,
	[Person_ClsTblPersonType] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblClsTblPerson] (
	[ClsTblPersonId] [int] IDENTITY (1, 1) NOT NULL ,
	[ClsTblPersonType] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[FirstName] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[LastName] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblClsTblWorkFolder] (
	[ClsTblFolderId] [int] NOT NULL ,
	[ClsTblFolderType] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Employee_ClsTblPersonId] [int] NOT NULL ,
	[Employee_ClsTblPersonType] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[WorkType] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblCncTblEmployee] (
	[CncTblPersonId] [int] NOT NULL ,
	[CncTblPersonType] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[EmploymentDate] [datetime] NOT NULL ,
	[FirstName] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[LastName] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Salary] [decimal](18, 0) NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblCncTblFolder] (
	[CncTblFolderId] [int] IDENTITY (1, 1) NOT NULL ,
	[CncTblFolderType] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Name] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Person_CncTblPersonId] [int] NULL ,
	[Person_CncTblPersonType] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblCncTblPerson] (
	[CncTblPersonId] [int] IDENTITY (1, 1) NOT NULL ,
	[CncTblPersonType] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[FirstName] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[LastName] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblCncTblWorkFolder] (
	[CncTblFolderId] [int] NOT NULL ,
	[CncTblFolderType] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Employee_CncTblPersonId] [int] NOT NULL ,
	[Employee_CncTblPersonType] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Name] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Person_CncTblPersonId] [int] NOT NULL ,
	[Person_CncTblPersonType] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[WorkType] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblCover] (
	[CoverId] [int] IDENTITY (1, 1) NOT NULL ,
	[Color] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblCyclicA] (
	[CyclicAID] [int] IDENTITY (1, 1) NOT NULL ,
	[CyclicBID] [int] NULL ,
	[SomeText] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblCyclicB] (
	[CyclicBID] [int] IDENTITY (1, 1) NOT NULL ,
	[CyclicAID] [int] NULL ,
	[SomeText] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblInvCyclicA] (
	[InvCyclicAID] [int] IDENTITY (1, 1) NOT NULL ,
	[InvCyclicBID] [int] NULL ,
	[SomeText] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblInvCyclicB] (
	[InvCyclicBID] [int] IDENTITY (1, 1) NOT NULL ,
	[InvCyclicAID] [int] NULL ,
	[SomeText] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblProfile] (
	[UserID] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[DOB] [datetime] NOT NULL ,
	[FirstName] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[LastName] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[SectionID] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblSections] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[Descr] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ParentID] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblSngTblFolder] (
	[SngTblFolderId] [int] IDENTITY (1, 1) NOT NULL ,
	[SngTblFolderType] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Employee_SngTblPersonId] [int] NULL ,
	[Employee_SngTblPersonType] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Name] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Person_SngTblPersonId] [int] NULL ,
	[Person_SngTblPersonType] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[WorkType] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblSngTblPerson] (
	[SngTblPersonId] [int] IDENTITY (1, 1) NOT NULL ,
	[SngTblPersonType] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[EmploymentDate] [datetime] NULL ,
	[FirstName] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[LastName] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Salary] [decimal](18, 0) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblUser] (
	[UserID] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[LastLogon] [datetime] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[FolderID] WITH NOCHECK ADD 
	CONSTRAINT [PK_FolderID] PRIMARY KEY  CLUSTERED 
	(
		[Ap_Code],
		[Func_Code]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblBook] WITH NOCHECK ADD 
	CONSTRAINT [PK_tblBook] PRIMARY KEY  CLUSTERED 
	(
		[BookId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblBookInfo] WITH NOCHECK ADD 
	CONSTRAINT [PK_tblBookInfo] PRIMARY KEY  CLUSTERED 
	(
		[BookInfoId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblClsTblEmployee] WITH NOCHECK ADD 
	CONSTRAINT [PK_tblClsTblEmployee] PRIMARY KEY  CLUSTERED 
	(
		[ClsTblPersonId],
		[ClsTblPersonType]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblClsTblFolder] WITH NOCHECK ADD 
	CONSTRAINT [PK_tblClsTblFolder] PRIMARY KEY  CLUSTERED 
	(
		[ClsTblFolderId],
		[ClsTblFolderType]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblClsTblPerson] WITH NOCHECK ADD 
	CONSTRAINT [PK_tblClsTblPerson] PRIMARY KEY  CLUSTERED 
	(
		[ClsTblPersonId],
		[ClsTblPersonType]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblClsTblWorkFolder] WITH NOCHECK ADD 
	CONSTRAINT [PK_tblClsTblWorkFolder] PRIMARY KEY  CLUSTERED 
	(
		[ClsTblFolderId],
		[ClsTblFolderType]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblCncTblEmployee] WITH NOCHECK ADD 
	CONSTRAINT [PK_tblCncTblEmployee] PRIMARY KEY  CLUSTERED 
	(
		[CncTblPersonId],
		[CncTblPersonType]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblCncTblFolder] WITH NOCHECK ADD 
	CONSTRAINT [PK_tblCncTblFolder] PRIMARY KEY  CLUSTERED 
	(
		[CncTblFolderId],
		[CncTblFolderType]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblCncTblPerson] WITH NOCHECK ADD 
	CONSTRAINT [PK_tblCncTblPerson] PRIMARY KEY  CLUSTERED 
	(
		[CncTblPersonId],
		[CncTblPersonType]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblCncTblWorkFolder] WITH NOCHECK ADD 
	CONSTRAINT [PK_tblCncTblWorkFolder] PRIMARY KEY  CLUSTERED 
	(
		[CncTblFolderId],
		[CncTblFolderType]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblCover] WITH NOCHECK ADD 
	CONSTRAINT [PK_tblCover] PRIMARY KEY  CLUSTERED 
	(
		[CoverId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblCyclicA] WITH NOCHECK ADD 
	CONSTRAINT [PK_tblCyclicA] PRIMARY KEY  CLUSTERED 
	(
		[CyclicAID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblCyclicB] WITH NOCHECK ADD 
	CONSTRAINT [PK_CyclicB] PRIMARY KEY  CLUSTERED 
	(
		[CyclicBID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblInvCyclicA] WITH NOCHECK ADD 
	CONSTRAINT [PK_InvCyclicA] PRIMARY KEY  CLUSTERED 
	(
		[InvCyclicAID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblInvCyclicB] WITH NOCHECK ADD 
	CONSTRAINT [PK_tblInvCyclicB] PRIMARY KEY  CLUSTERED 
	(
		[InvCyclicBID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblProfile] WITH NOCHECK ADD 
	CONSTRAINT [PK_tblProfile] PRIMARY KEY  CLUSTERED 
	(
		[UserID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblSections] WITH NOCHECK ADD 
	CONSTRAINT [PK_tblSections] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblSngTblFolder] WITH NOCHECK ADD 
	CONSTRAINT [PK_tblSngTblFolder] PRIMARY KEY  CLUSTERED 
	(
		[SngTblFolderId],
		[SngTblFolderType]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblSngTblPerson] WITH NOCHECK ADD 
	CONSTRAINT [PK_tblSngTblPerson] PRIMARY KEY  CLUSTERED 
	(
		[SngTblPersonId],
		[SngTblPersonType]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblUser] WITH NOCHECK ADD 
	CONSTRAINT [PK_tblUser] PRIMARY KEY  CLUSTERED 
	(
		[UserID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblBook] ADD 
	CONSTRAINT [FK_tblBook_Cover_CoverId] FOREIGN KEY 
	(
		[Cover_CoverId]
	) REFERENCES [dbo].[tblCover] (
		[CoverId]
	)
GO

ALTER TABLE [dbo].[tblBookInfo] ADD 
	CONSTRAINT [FK_tblBookInfo_Book_BookId] FOREIGN KEY 
	(
		[Book_BookId]
	) REFERENCES [dbo].[tblBook] (
		[BookId]
	)
GO

ALTER TABLE [dbo].[tblClsTblEmployee] ADD 
	CONSTRAINT [FK_tblClsTblEmployee_ClsTblPersonId_ClsTblPersonType] FOREIGN KEY 
	(
		[ClsTblPersonId],
		[ClsTblPersonType]
	) REFERENCES [dbo].[tblClsTblPerson] (
		[ClsTblPersonId],
		[ClsTblPersonType]
	)
GO

ALTER TABLE [dbo].[tblClsTblFolder] ADD 
	CONSTRAINT [FK_tblClsTblFolder_Person_ClsTblPersonId_Person_ClsTblPersonType] FOREIGN KEY 
	(
		[Person_ClsTblPersonId],
		[Person_ClsTblPersonType]
	) REFERENCES [dbo].[tblClsTblPerson] (
		[ClsTblPersonId],
		[ClsTblPersonType]
	)
GO

ALTER TABLE [dbo].[tblClsTblWorkFolder] ADD 
	CONSTRAINT [FK_tblClsTblWorkFolder_ClsTblFolderId_ClsTblFolderType] FOREIGN KEY 
	(
		[ClsTblFolderId],
		[ClsTblFolderType]
	) REFERENCES [dbo].[tblClsTblFolder] (
		[ClsTblFolderId],
		[ClsTblFolderType]
	),
	CONSTRAINT [FK_tblClsTblWorkFolder_Employee_ClsTblPersonId_Employee_ClsTblPersonType] FOREIGN KEY 
	(
		[Employee_ClsTblPersonId],
		[Employee_ClsTblPersonType]
	) REFERENCES [dbo].[tblClsTblPerson] (
		[ClsTblPersonId],
		[ClsTblPersonType]
	)
GO

ALTER TABLE [dbo].[tblCncTblEmployee] ADD 
	CONSTRAINT [FK_tblCncTblEmployee_CncTblPersonId_CncTblPersonType] FOREIGN KEY 
	(
		[CncTblPersonId],
		[CncTblPersonType]
	) REFERENCES [dbo].[tblCncTblPerson] (
		[CncTblPersonId],
		[CncTblPersonType]
	)
GO

ALTER TABLE [dbo].[tblCncTblFolder] ADD 
	CONSTRAINT [FK_Person] FOREIGN KEY 
	(
		[Person_CncTblPersonId],
		[Person_CncTblPersonType]
	) REFERENCES [dbo].[tblCncTblPerson] (
		[CncTblPersonId],
		[CncTblPersonType]
	)
GO

ALTER TABLE [dbo].[tblCncTblWorkFolder] ADD 
	CONSTRAINT [FK_Employee] FOREIGN KEY 
	(
		[Employee_CncTblPersonId],
		[Employee_CncTblPersonType]
	) REFERENCES [dbo].[tblCncTblPerson] (
		[CncTblPersonId],
		[CncTblPersonType]
	),
	CONSTRAINT [FK_tblCncTblWorkFolder_CncTblFolderId_CncTblFolderType] FOREIGN KEY 
	(
		[CncTblFolderId],
		[CncTblFolderType]
	) REFERENCES [dbo].[tblCncTblFolder] (
		[CncTblFolderId],
		[CncTblFolderType]
	),
	CONSTRAINT [FK_WF_Person] FOREIGN KEY 
	(
		[Person_CncTblPersonId],
		[Person_CncTblPersonType]
	) REFERENCES [dbo].[tblCncTblPerson] (
		[CncTblPersonId],
		[CncTblPersonType]
	)
GO

ALTER TABLE [dbo].[tblCyclicA] ADD 
	CONSTRAINT [FK_tblCyclicA_tblCyclicB] FOREIGN KEY 
	(
		[CyclicBID]
	) REFERENCES [dbo].[tblCyclicB] (
		[CyclicBID]
	)
GO

ALTER TABLE [dbo].[tblCyclicB] ADD 
	CONSTRAINT [FK_tblCyclicB_tblCyclicA] FOREIGN KEY 
	(
		[CyclicAID]
	) REFERENCES [dbo].[tblCyclicA] (
		[CyclicAID]
	)
GO

ALTER TABLE [dbo].[tblInvCyclicA] ADD 
	CONSTRAINT [FK_tblInvCyclicA_tblInvCyclicB] FOREIGN KEY 
	(
		[InvCyclicBID]
	) REFERENCES [dbo].[tblInvCyclicB] (
		[InvCyclicBID]
	)
GO

ALTER TABLE [dbo].[tblInvCyclicB] ADD 
	CONSTRAINT [FK_tblInvCyclicB_tblInvCyclicA] FOREIGN KEY 
	(
		[InvCyclicAID]
	) REFERENCES [dbo].[tblInvCyclicA] (
		[InvCyclicAID]
	)
GO

ALTER TABLE [dbo].[tblProfile] ADD 
	CONSTRAINT [FK_tblProfile_tblSections] FOREIGN KEY 
	(
		[SectionID]
	) REFERENCES [dbo].[tblSections] (
		[ID]
	),
	CONSTRAINT [FK_tblProfile_tblUser] FOREIGN KEY 
	(
		[UserID]
	) REFERENCES [dbo].[tblUser] (
		[UserID]
	)
GO

ALTER TABLE [dbo].[tblSections] ADD 
	CONSTRAINT [FK_tblSections_tblSections] FOREIGN KEY 
	(
		[ParentID]
	) REFERENCES [dbo].[tblSections] (
		[ID]
	)
GO

ALTER TABLE [dbo].[tblSngTblFolder] ADD 
	CONSTRAINT [FK_tblSngTblFolder_Employee_SngTblPersonId_Employee_SngTblPersonType] FOREIGN KEY 
	(
		[Employee_SngTblPersonId],
		[Employee_SngTblPersonType]
	) REFERENCES [dbo].[tblSngTblPerson] (
		[SngTblPersonId],
		[SngTblPersonType]
	),
	CONSTRAINT [FK_tblSngTblFolder_Person_SngTblPersonId_Person_SngTblPersonType] FOREIGN KEY 
	(
		[Person_SngTblPersonId],
		[Person_SngTblPersonType]
	) REFERENCES [dbo].[tblSngTblPerson] (
		[SngTblPersonId],
		[SngTblPersonType]
	)
GO

