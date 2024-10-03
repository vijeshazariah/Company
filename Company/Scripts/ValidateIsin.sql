USE [Company]
GO

/****** Object:  StoredProcedure [dbo].[sp_ValidateIsin]    Script Date: 03/10/2024 22:16:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ValidateIsin]
    @Isin NVARCHAR(12)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM tbl_Company WHERE Isin = @Isin)
    BEGIN
        -- ISIN already exists
        SELECT CAST(0 AS BIT) AS IsValid
    END
    ELSE
    BEGIN
        -- ISIN is valid (not existing)
        SELECT CAST(1 AS BIT) AS IsValid
    END
END
GO


