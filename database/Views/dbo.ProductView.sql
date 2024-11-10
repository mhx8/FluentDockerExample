USE [Products];
GO

CREATE VIEW [dbo].[ProductView] AS
SELECT 
    [Id],
    [Name],
    [Description],
    [Price],
    [Stock]
FROM [dbo].[Product];