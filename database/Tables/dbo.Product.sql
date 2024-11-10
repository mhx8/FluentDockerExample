USE [Products];
GO

CREATE TABLE [dbo].[Product] (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    Price DECIMAL(18, 2) NOT NULL,
    Stock INT NOT NULL
);