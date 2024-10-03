USE [Company]
GO

/****** Object:  StoredProcedure [dbo].[sp_UpdateCompany]    Script Date: 03/10/2024 21:33:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_UpdateCompany]
    (@Id INT,
    @Name NVARCHAR(100),
    @Ticker NVARCHAR(10),
    @Exchange NVARCHAR(50),
    @Isin NVARCHAR(20),
    @WebsiteUrl NVARCHAR(200)
	)

AS

BEGIN

    UPDATE tbl_Company
    SET Name = @Name, 
	    Ticker = @Ticker, 
		Exchange = @Exchange, 
		Isin = @Isin, 
		WebsiteUrl = @WebsiteUrl
    WHERE Id = @Id;

END;
GO


