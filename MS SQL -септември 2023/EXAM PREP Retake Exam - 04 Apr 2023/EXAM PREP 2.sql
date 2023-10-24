--SECTION 1

CREATE DATABASE Accounting

-----

CREATE TABLE Countries(
Id INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(10) NOT NULL
)

CREATE TABLE Addresses(
Id INT PRIMARY KEY IDENTITY,
StreetName NVARCHAR(20) NOT NULL,
StreetNumber INT,
PostCode INT NOT NULL,
City VARCHAR(25) NOT NULL,
CountryId INT FOREIGN KEY REFERENCES Countries(Id) NOT NULL 
)

CREATE TABLE Vendors(
Id INT PRIMARY KEY IDENTITY,
[Name] NVARCHAR(25) NOT NULL,
NumberVAT NVARCHAR(15) NOT NULL,
AddressId INT FOREIGN KEY REFERENCES Addresses(Id) NOT NULL
)

CREATE TABLE Clients(
Id INT PRIMARY KEY IDENTITY,
[Name] NVARCHAR(25) NOT NULL,
NumberVAT NVARCHAR(15) NOT NULL,
AddressId INT FOREIGN KEY REFERENCES Addresses(Id) NOT NULL
)

CREATE TABLE Categories(
Id INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(10) NOT NULL
)

CREATE TABLE Products(
Id INT PRIMARY KEY IDENTITY,
[Name] NVARCHAR(35) NOT NULL,
Price DECIMAL(18,2) NOT NULL,
CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
VendorId INT FOREIGN KEY REFERENCES Vendors(Id) NOT NULL
)

CREATE TABLE Invoices(
Id INT PRIMARY KEY IDENTITY,
Number INT UNIQUE NOT NULL,
IssueDate DateTime2 NOT NULL,
DueDate DateTime2 NOT NULL,
Amount DECIMAL(18,2) NOT NULL,
Currency VARCHAR(5) NOT NULL,
ClientId INT FOREIGN KEY REFERENCES Clients(Id) NOT NULL 
)

CREATE TABLE ProductsClients(
ProductId INT FOREIGN KEY REFERENCES Products(Id) NOT NULL,
ClientId INT FOREIGN KEY REFERENCES Clients(Id) NOT NULL,
CONSTRAINT PK_ProductsClients PRIMARY KEY (ProductId, ClientId)
)

--1

--INSERTION OF "Dataset.sql"

--SECTION 2

INSERT INTO Products([Name], Price, CategoryId, VendorId)
VALUES('SCANIA Oil Filter XD01', 78.69, 1, 1),
	('MAN Air Filter XD01', 97.38, 1, 5),
	('DAF Light Bulb 05FG87', 55.00, 2, 13),
	('ADR Shoes 47-47.5', 49.85, 3, 5),
	('Anti-slip pads S', 5.87, 5, 7)

INSERT INTO Invoices(Number, IssueDate, DueDate, Amount, Currency, ClientId)
VALUES(1219992181, '2023-03-01', '2023-04-30', 180.96, 'BGN', 3),
	(1729252340, '2022-11-06', '2023-01-04', 158.18, 'EUR', 13),
	(1950101013, '2023-02-17', '2023-04-18', 615.15, 'USD', 19)

--2

UPDATE Invoices
SET DueDate = '2023-04-01'
WHERE IssueDate BETWEEN '2022-11-01' AND '2022-11-30'

UPDATE Clients
SET AddressId = 3
WHERE [Name] LIKE '%CO%'

--3

DELETE FROM ProductsClients WHERE ClientId = 11
DELETE FROM Invoices WHERE ClientId = 11
DELETE FROM Clients WHERE NumberVAT LIKE 'IT%'

--4

--SECTION 3

SELECT 
Number,
Currency
FROM Invoices
ORDER BY Amount DESC, DueDate ASC

--5

SELECT
p.Id,
p.[Name],
p.Price,
c.[Name] AS CategoryName
FROM Products p
JOIN Categories c ON c.Id = p.CategoryId
WHERE c.[Name] IN ('ADR', 'Others')
ORDER BY Price DESC

--6

SELECT
cl.Id,
cl.[Name],
CONCAT(a.StreetName, ' ', a.StreetNumber, ', ', a.City , ', ', a.PostCode, ', ', c.[Name]) AS [Address]
FROM Clients AS cl
JOIN Addresses AS a ON cl.AddressId = a.Id
JOIN Countries AS c ON a.CountryId = c.Id
LEFT JOIN ProductsClients AS pc ON pc.ClientId = cl.Id
WHERE pc.ProductId IS NULL
ORDER BY cl.[Name] ASC

--7

SELECT TOP(7)
i.Number,
i.Amount,
c.[Name] AS Client
FROM Invoices i
JOIN Clients c ON c.Id = i.ClientId
WHERE (i.IssueDate < '2023-01-01' AND i.Currency = 'EUR')
	OR (i.Amount > 500.00 AND c.NumberVAT LIKE 'DE%')
ORDER BY i.Number, i.Amount DESC

--8

SELECT
c.[Name] as Client,
MAX(p.Price),
c.NumberVAT AS [VAT Number]
FROM Clients c
JOIN ProductsClients pc ON pc.ClientId = c.Id
JOIN Products p ON p.Id = pc.ProductId
WHERE c.[Name] NOT LIKE '%KG'
GROUP BY c.[Name], c.NumberVAT
ORDER BY MAX(p.Price) DESC

--9

SELECT
c.[Name] AS Client,
FLOOR(AVG(p.Price)) as [Average Price]
FROM Clients c
JOIN ProductsClients pc ON pc.ClientId = c.Id
JOIN Products p ON pc.ProductId = p.Id
JOIN Vendors v ON p.VendorId = v.Id
WHERE v.NumberVAT LIKE '%FR%'
GROUP BY c.[Name]
ORDER BY FLOOR(AVG(p.Price)) ASC, c.[Name] DESC

--10

--SECTION 4

CREATE FUNCTION udf_ProductWithClients(@name NVARCHAR(50))
RETURNS INT
AS 
BEGIN
	DECLARE @result INT = 
	(
		SELECT COUNT(*)
		FROM Clients AS c
		JOIN ProductsClients AS pc ON pc.ClientId = c.Id
		JOIN Products AS p ON pc.ProductId = p.Id
		WHERE p.[Name] = @name
	)

	IF(@result is NULL)
		SET @result = 0
	RETURN @result
END

--------

SELECT dbo.udf_ProductWithClients('DAF FILTER HU12103X')

--11

CREATE PROCEDURE usp_SearchByCountry
@country NVARCHAR(50)
AS
	SELECT
	v.[Name] AS Vendor,
	v.NumberVAT AS VAT,
	CONCAT_WS(' ', a.StreetName, a.StreetNumber) AS [Street Info],
	CONCAT_WS(' ', a.City, a.PostCode) AS [City Info]
	FROM Vendors v
	JOIN Addresses a ON a.Id = v.AddressId
	JOIN Countries c ON c.Id = a.CountryId
	WHERE c.[Name] = @country
	ORDER BY v.[Name], a.City

--------

EXEC usp_SearchByCountry 'France'

--12