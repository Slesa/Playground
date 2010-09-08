CREATE DATABASE NHibernateSketches
GO

USE NHibernateSketches
GO

CREATE TABLE Employee (
	id int identity primary key,
	name nvarchar(50),
	manager int )
GO

