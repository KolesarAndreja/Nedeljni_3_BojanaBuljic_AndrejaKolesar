IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'dbRecipeKeeper')
CREATE DATABASE dbRecipeKeeper;
GO
USE dbRecipeKeeper
--dropping tables
IF OBJECT_ID('vwRecipe') IS NOT NULL
DROP VIEW vwRecipe;

IF OBJECT_ID('tblIngredient') IS NOT NULL
DROP TABLE tblIngredient;

IF OBJECT_ID('tblRecipe') IS NOT NULL
DROP TABLE tblRecipe;

IF OBJECT_ID('tblUser') IS NOT NULL
DROP TABLE tblUser;

CREATE TABLE tblUser(
	userId INT PRIMARY KEY IDENTITY(1,1),
	fullname VARCHAR(40) not null,
	username VARCHAR(30) not null,
	password VARCHAR(30) not null,
	role varchar(20) not null
	);

CREATE TABLE tblRecipe(
	recipeId INT PRIMARY KEY IDENTITY(1,1),
	title VARCHAR(30) not null,
	type VARCHAR(25) not null,
	numberOfPersons INT not null,
	description VARCHAR(200) NOT NULL,
	creationDate DATE NOT NULL,
	authorId INT FOREIGN KEY REFERENCES tblUser(userId)
	);

CREATE TABLE tblIngredient(
	ingridientId INT PRIMARY KEY IDENTITY(1,1),
	name VARCHAR(25) not null,
	quantity INT not null,
	status VARCHAR(20) default 'unresolved',
	recipeId INT FOREIGN KEY REFERENCES tblRecipe(recipeId)
);

GO
CREATE VIEW vwRecipe
as
select r.*, u.fullname, u.role, u.password, u.username
from tblRecipe r
inner join tblUser u
on r.authorId = u.userId;