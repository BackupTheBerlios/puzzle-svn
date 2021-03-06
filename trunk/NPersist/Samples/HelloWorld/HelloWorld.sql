if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Employees]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Employees]
GO

CREATE TABLE [dbo].[Employees] (
	[EmployeeId] [int] IDENTITY (1, 1) NOT NULL ,
	[Name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Employees] WITH NOCHECK ADD 
	CONSTRAINT [PK_Employees] PRIMARY KEY  CLUSTERED 
	(
		[EmployeeId]
	)  ON [PRIMARY] 
GO

