USE [Company]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetAllCompanies]    Script Date: 03/10/2024 21:27:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetAllCompanies]

AS

BEGIN

    SELECT [ID]
		  ,[Name]
		  ,[Ticker]
		  ,[Exchange]
		  ,[ISIN]
		  ,[WebsiteUrl]
  FROM [dbo].[tbl_Company];

END;
GO


