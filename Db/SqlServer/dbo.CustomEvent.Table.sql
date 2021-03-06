USE [MigrationDB]
GO
/****** Object:  Table [dbo].[CustomEvent]    Script Date: 4/19/2021 2:49:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomEvent](
	[CustomEventSqlId] [int] IDENTITY(1,1) NOT NULL,
	[UserSqlId] [int] NOT NULL,
	[CustomEventDate] [datetime] NOT NULL,
	[FullName] [nvarchar](255) NULL,
	[Source] [nvarchar](max) NULL,
 CONSTRAINT [PK_CustomEvent] PRIMARY KEY CLUSTERED 
(
	[CustomEventSqlId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[CustomEvent]  WITH CHECK ADD  CONSTRAINT [FK_ReadEvent_Users] FOREIGN KEY([UserSqlId])
REFERENCES [dbo].[User] ([UserSqlId])
GO
ALTER TABLE [dbo].[CustomEvent] CHECK CONSTRAINT [FK_ReadEvent_Users]
GO
