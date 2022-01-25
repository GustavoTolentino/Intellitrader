CREATE DATABASE TesteGustavo 
GO
--: id (string), firstName (string), surname (string), age (int), creationDate (DateTime).

USE TesteGustavo
GO

CREATE TABLE Usuario 
(
	id UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
	firstName VARCHAR(250) NOT NULL,
	surname VARCHAR(250),
	age INT NOT NULL,
	creationDate DATETIME NOT NULL
)
GO

SELECT * FROM USUARIO