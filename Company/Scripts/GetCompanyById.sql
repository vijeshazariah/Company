USE [Company]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetCompanyById]    Script Date: 03/10/2024 21:30:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetCompanyById]

    @Id INT

AS

BEGIN

    SELECT [ID]
		  ,[Name]
		  ,[Ticker]
		  ,[Exchange]
		  ,[ISIN]
		  ,[WebsiteUrl]
  FROM [dbo].[tbl_Company] WHERE Id = @Id;

END;
GO


