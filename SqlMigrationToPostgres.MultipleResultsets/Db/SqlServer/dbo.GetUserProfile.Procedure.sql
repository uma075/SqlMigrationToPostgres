USE [MigrationDB]
GO
/****** Object:  StoredProcedure [dbo].[GetUserProfile]    Script Date: 7/9/2021 11:04:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetUserProfile]	
	@UserId int,
	@ProfileID uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    Select *from dbo.[User] u where u.UserSqlId=@UserId;
	Select *from dbo.CustomEvent u where u.UserSqlId=@UserId;
	Select *from dbo.Profile u where u.ProfileID=@ProfileID;
END
