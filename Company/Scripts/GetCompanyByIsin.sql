USE [Company]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetCompanyByIsin]    Script Date: 03/10/2024 21:31:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetCompanyByIsin]

    @Isin NVARCHAR(20)

AS

BEGIN

    SELECT [ID]
		  ,[Name]
		  ,[Ticker]
		  ,[Exchange]
		  ,[ISIN]
		  ,[WebsiteUrl]
  FROM [dbo].[tbl_Company] WHERE Isin = @Isin;

END;
GO


