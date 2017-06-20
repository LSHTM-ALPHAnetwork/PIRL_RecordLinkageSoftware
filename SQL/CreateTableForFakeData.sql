USE [KisesaDSS]
GO

/****** Object:  Table [dbo].[DSSIndividuals29]    Script Date: 24/01/2017 16:29:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FAKE100](
	[ID] [int] NOT NULL,
	[AbidanceID] [float] NULL,
	[idlong] [float] NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[Sex] [nvarchar](255) NULL,
	[dob] [datetime] NULL,
	[current_status] [nvarchar](255) NULL,
	[residencestartdate] [datetime] NULL,
	[residenceenddate] [datetime] NULL,
	[endeventname] [nvarchar](255) NULL,
	[starteventname] [nvarchar](255) NULL,
	[bcode] [float] NULL,
	[hhnumber] [float] NULL,
	[hhfname] [nvarchar](255) NULL,
	[hhsname] [nvarchar](255) NULL,
	[LineNumber] [float] NULL,
	[village] [float] NULL,
	[villagename] [nvarchar](255) NULL,
	[subvillage] [float] NULL,
	[subvillagename] [nvarchar](255) NULL,
	[tencell] [float] NULL,
	[balozi_first_name] [nvarchar](255) NULL,
	[balozi_second_name] [nvarchar](255) NULL,
	[hholdestFirst] [nvarchar](255) NULL,
	[hholdestLast] [nvarchar](255) NULL
) ON [PRIMARY]

GO


