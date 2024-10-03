USE [Company]
GO

/****** Object:  StoredProcedure [dbo].[sp_InsertCompany]    Script Date: 03/10/2024 22:03:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_InsertCompany]

    @Name NVARCHAR(100),

    @Ticker NVARCHAR(10),

    @Exchange NVARCHAR(50),

    @Isin NVARCHAR(20),

    @WebsiteUrl NVARCHAR(200)

AS

BEGIN

    INSERT INTO tbl_Company (Name, Ticker, Exchange, Isin, WebsiteUrl)

    VALUES (@Name, @Ticker, @Exchange, @Isin, @WebsiteUrl);

END;
GO


