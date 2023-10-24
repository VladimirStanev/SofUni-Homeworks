SELECT FirstName, LastName
FROM Employees
WHERE FirstName LIKE 'Sa%'

--1

SELECT FirstName, LastName
FROM Employees
WHERE LastName LIKE '%ei%'

--2

SELECT FirstName
FROM Employees
WHERE DepartmentID IN (3, 10) AND
	  HireDate BETWEEN '1995-01-01' AND '2005-12-31'

-- 3

SELECT FirstName, LastName
FROM Employees
WHERE JobTitle NOT LIKE '%engineer%' 

--4

SELECT [Name]
FROM Towns
WHERE LEN([Name]) = 5 OR LEN([Name]) = 6
ORDER BY [Name]

--5

SELECT TownID, [Name]
FROM Towns
WHERE [Name] LIKE '[MKBE]%' 
ORDER BY  [Name]

--6

SELECT TownID, [Name]
FROM Towns
WHERE [Name] NOT LIKE '[RBD]%' 
ORDER BY  [Name]

--7
CREATE VIEW V_EmployeesHiredAfter2000
AS
SELECT FirstName, LastName
FROM Employees
WHERE HireDate > '2000-12-31'

--8

SELECT FirstName, LastName
FROM Employees
WHERE LEN(LastName) = 5

--9

SELECT EmployeeID, FirstName, LastName, Salary,
DENSE_RANK() OVER (PARTITION BY Salary ORDER BY EmployeeID) AS [Rank]
FROM Employees
WHERE Salary BETWEEN 10000 AND 50000
ORDER BY Salary DESC

--10

SELECT * FROM
	(SELECT EmployeeID, FirstName, LastName, Salary,
	DENSE_RANK() OVER (PARTITION BY Salary ORDER BY EmployeeID) AS [Rank]
	FROM Employees
	WHERE Salary BETWEEN 10000 AND 50000) AS [Subquery]
	WHERE Subquery.Rank = 2
ORDER BY Salary DESC

--11

SELECT CountryName, IsoCode
FROM Countries
WHERE CountryName LIKE '%a%a%a%'
ORDER BY IsoCode

--12

SELECT p.PeakName, r.RiverName, LOWER(LEFT(p.PeakName, LEN(p.PeakName) - 1) + r.RiverName) AS Mix
FROM Peaks p, Rivers r
WHERE RIGHT(p.PeakName, 1) = LEFT(r.RiverName, 1)
ORDER BY Mix

--13

SELECT TOP(50) [Name], FORMAT([Start], 'yyyy-MM-dd') Start
FROM Games
WHERE YEAR([Start]) BETWEEN 2011 AND 2012
ORDER BY [Start],[NAME]

--14