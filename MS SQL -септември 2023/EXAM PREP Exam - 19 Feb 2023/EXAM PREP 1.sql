--SECTION 1

CREATE DATABASE Boardgames

-----------------------------

CREATE TABLE Categories(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Creators(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL,
	Email NVARCHAR(30) NOT NULL
)

CREATE TABLE PlayersRanges(
	Id INT PRIMARY KEY IDENTITY,
	PlayersMin INT NOT NULL,
	PlayersMax INT NOT NULL
)

CREATE TABLE Addresses(
	Id INT PRIMARY KEY IDENTITY,
	StreetName NVARCHAR(100) NOT NULL,
	StreetNumber INT NOT NULL,
	Town NVARCHAR(30) NOT NULL,
	Country NVARCHAR(50) NOT NULL,
	ZIP INT NOT NULL
)

CREATE TABLE Publishers(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(30) UNIQUE NOT NULL,
	AddressId INT FOREIGN KEY REFERENCES Addresses(Id) NOT NULL,
	Website NVARCHAR(40),
	Phone NVARCHAR(20)
)

CREATE TABLE Boardgames(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(30) NOT NULL,
	YearPublished INT NOT NULL,
	Rating DECIMAL(4,2) NOT NULL,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
	PublisherId INT FOREIGN KEY REFERENCES Publishers(Id) NOT NULL,
	PlayersRangeId INT FOREIGN KEY REFERENCES PlayersRanges(Id) NOT NULL
)

CREATE TABLE CreatorsBoardgames(
	CreatorId INT FOREIGN KEY REFERENCES Creators(Id),
	BoardgameId INT FOREIGN KEY REFERENCES Boardgames(Id),
	PRIMARY KEY(CreatorId, BoardgameId)
)

--1

--Section 2

----------------------------
--Inserted "Dataset.sql"
----------------------------

INSERT INTO Boardgames ([Name], YearPublished, Rating, CategoryId, PublisherId, PlayersRangeId)
	VALUES	('Deep Blue', 2019, 5.67, 1, 15, 7),
			('Paris', 2016, 9.78, 7, 1, 5),
			('Catan: Starfarers', 2021, 9.87, 7, 13, 6),
			('Bleeding Kansas', 2020, 3.25, 3, 7, 4),
			('One Small Step', 2019, 5.75, 5, 9, 2)

INSERT INTO Publishers([Name], AddressId, Website, Phone)
	VALUES	('Agman Games', 5, 'www.agmangames.com', '+16546135542'),
			('Amethyst Games', 7, 'www.amethystgames.com', '+15558889992'),
			('BattleBooks', 13, 'www.battlebooks.com', '+12345678907')

--2

UPDATE PlayersRanges
SET PlayersMax += 1
WHERE PlayersMax = 2 AND PlayersMin = 2

UPDATE Boardgames 
SET [Name] += 'V2'
WHERE YearPublished >= '2020'

--------

SELECT * FROM Boardgames

--3

DELETE FROM CreatorsBoardgames WHERE BoardgameId IN (1, 16, 31, 47)
DELETE FROM Boardgames WHERE PublisherId IN (1, 16)
DELETE FROM Publishers WHERE AddressId = 5
DELETE FROM Addresses WHERE Left(Town, 1) = 'L'

--4

--Section 3

--RECREATE EVERYTHING AND INSERT "Dataset.sql" AGAIN

SELECT 
[Name],
Rating
FROM Boardgames
ORDER BY YearPublished ASC, [Name] DESC

--5

SELECT
b.Id,
b.[Name],
b.YearPublished,
c.[Name]
FROM Boardgames AS b 
JOIN Categories AS c ON b.CategoryId = c.Id
WHERE c.Name IN ('Strategy Games', 'Wargames')
ORDER BY YearPublished DESC

--6

SELECT
c.Id,
CONCAT_WS(' ', c.FirstName, c.LastName) AS CreatorName,
c.Email
FROM Creators AS c
LEFT JOIN CreatorsBoardgames cb ON cb.CreatorId = c.Id
WHERE cb.BoardgameId IS NULL
ORDER BY CreatorName

----------
--lectors variant

SELECT
	Id, 
	CONCAT_WS(' ', FirstName, LastName)[CreatorName], 
	Email
FROM
	Creators
WHERE id NOT IN(
				SELECT CreatorId FROM CreatorsBoardgames
				)

--7

SELECT TOP(5)
b.[Name],
b.Rating,
c.[Name] AS CategoryName
FROM Boardgames AS b
JOIN Categories AS c ON c.Id = b.CategoryId
JOIN PlayersRanges AS p ON p.Id = b.PlayersRangeId
WHERE Rating > 7
	AND(b.[Name] LIKE '%a%' OR Rating > 7.50)
	AND PlayersMin >= 2
	AND PlayersMax <= 5
ORDER BY b.[Name], Rating DESC

--8

SELECT
FullName, Email, Rating FROM
(SELECT
CONCAT_WS(' ', FirstName, LastName) AS FullName,
Email, bg.[Name] ,bg.Rating,
RANK() OVER (PARTITION BY Email ORDER BY rating DESC) AS GameRating
FROM Creators c
JOIN CreatorsBoardgames cb ON cb.CreatorId = c.Id
JOIN Boardgames bg ON bg.Id = cb.BoardgameId
WHERE RIGHT(Email, 4) = '.com') AS subq
WHERE GameRating = 1
ORDER BY FullName

--9

SELECT 
c.LastName,
CEILING(AVG(bg.Rating)) AS AverageRating,
p.[Name]
FROM Creators c
JOIN CreatorsBoardgames cb ON cb.CreatorId = c.Id
JOIN Boardgames bg ON bg.Id = cb.BoardgameId
JOIN Publishers p ON p.Id = bg.PublisherId
WHERE p.[Name] = 'Stonemaier Games'
GROUP BY c.LastName, p.[Name]
ORDER BY AVG(bg.Rating) DESC

--10

CREATE FUNCTION udf_CreatorWithBoardgames(@name NVARCHAR(50))
RETURNS INT
AS
BEGIN
	DECLARE @countOfGames INT =
	(
		SELECT
		COUNT(BoardgameId)
		FROM CreatorsBoardgames cb
		JOIN Creators c ON c.Id = cb.CreatorId
		WHERE c.FirstName = @name
	)
	RETURN @countOfGames
END

-----------------------

SELECT dbo.udf_CreatorWithBoardgames('Bruno')

--11

CREATE PROCEDURE usp_SearchByCategory
@category NVARCHAR(30)
AS
	DECLARE @categoryId INT =
		(SELECT Id
		FROM Categories
		WHERE [Name] = @category)
	
	SELECT
	b.[Name],
	b.YearPublished,
	b.Rating,
	@category AS CategoryName,
	p.[Name] AS PublisherName,
	CONCAT_WS(' ', pr.PlayersMin, 'people')[MinPlayers],
	CONCAT_WS(' ', pr.PlayersMax, 'people')[MaxPlayers]
	FROM Boardgames b
JOIN Publishers p ON b.PublisherId = p.Id
JOIN PlayersRanges pr ON pr.Id = b.PlayersRangeId
WHERE
	b.CategoryId = @categoryId
ORDER BY p.[Name], b.YearPublished DESC

-----------

EXEC usp_SearchByCategory 'Wargames'

--12