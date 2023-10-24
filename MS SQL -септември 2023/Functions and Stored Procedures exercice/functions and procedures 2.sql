--PART 1 - SOFTUNI DATABASE QUERIES

CREATE PROCEDURE usp_GetEmployeesSalaryAbove35000
AS 
	SELECT FirstName,
	LastName
	FROM Employees
	WHERE Salary > 35000

------------------------------------------

EXEC usp_GetEmployeesSalaryAbove35000

--1

CREATE OR ALTER PROCEDURE usp_GetEmployeesSalaryAboveNumber
@salaryLevel DECIMAL(18,4)
AS
	SELECT FirstName, LastName
	FROM Employees
	WHERE Salary >= @salaryLevel

-------------------------------------------

EXEC usp_GetEmployeesSalaryAboveNumber 30000

--2

CREATE OR ALTER PROCEDURE usp_GetTownsStartingWith
@stringParameter NVARCHAR(50)
AS
	SELECT [Name]
	FROM Towns
	WHERE [Name] LIKE @stringParameter + '%'

--------------------------------------------

EXEC usp_GetTownsStartingWith 's'

--3

CREATE OR ALTER PROCEDURE usp_GetEmployeesFromTown
@townName NVARCHAR(50)
AS
	SELECT 
	FirstName,
	LastName
	FROM Employees AS e 
	JOIN Addresses AS a ON a.AddressID = e.AddressID
	JOIN Towns AS t ON t.TownID = a.TownID
	WHERE t.Name = @townName

------------------------------------------------

EXEC usp_GetEmployeesFromTown 'Sofia'

--4

CREATE OR ALTER FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS VARCHAR(8) 
AS
BEGIN
	DECLARE @salaryLevel VARCHAR(8)

	IF (@salary < 30000)
		SET @salaryLevel = 'Low';
	ELSE IF (@salary <= 50000)
		SET @salaryLevel = 'Average';
	ELSE
		SET @salaryLevel = 'High';

	RETURN @salaryLevel;
END


------second way

CREATE OR ALTER FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS VARCHAR(8) 
AS
BEGIN
	IF (@salary < 30000)
		RETURN 'Low'
	ELSE IF (@salary <= 50000)
		RETURN 'AVERAGE'
	
	RETURN 'High'
END

-----------------------------------------

SELECT dbo.ufn_GetSalaryLevel(13500.00) AS SalaryLevel
FROM Employees

--5

CREATE OR ALTER PROCEDURE usp_EmployeesBySalaryLevel
@SalaryLevel NVARCHAR(50)
AS
BEGIN
	SELECT	
	FirstName,
	LastName
	FROM Employees
	WHERE dbo.ufn_GetSalaryLevel(Salary) = @SalaryLevel
END

------------------------------------------------

EXEC usp_EmployeesBySalaryLevel 'Low'

--6

CREATE OR ALTER FUNCTION ufn_IsWordComprised(@setOfLetters NVARCHAR(50), @word NVARCHAR(100))
RETURNS BIT
AS
BEGIN
	DECLARE @i INT = 1
	WHILE @i <= LEN(@word)
	BEGIN
		 DECLARE @ch NVARCHAR(1) = SUBSTRING(@word, @i, 1)
		 IF CHARINDEX(@ch, @setOfLetters) = 0
			RETURN 0
		 ELSE 
			SET @i += 1
	END
	RETURN 1
END

----------------------------------------------------

SELECT dbo.ufn_IsWordComprised('oistmiahf', 'Sofia')

--7

CREATE OR ALTER PROCEDURE usp_DeleteEmployeesFromDepartment 
@departmentId INT
AS
BEGIN
	DECLARE @employeesDelete TABLE(ID INT)

	INSERT INTO @employeesDelete(ID)
	SELECT EmployeeID
	FROM Employees
	WHERE DepartmentID = @departmentId

	ALTER TABLE Departments
	ALTER COLUMN ManagerID INT

	UPDATE Departments
	SET ManagerID = NULL
	WHERE ManagerID IN (SELECT * FROM @employeesDelete)

	UPDATE Employees
	SET ManagerID = NULL
	WHERE ManagerID IN (SELECT * FROM @employeesDelete)

	DELETE FROM EmployeesProjects
	WHERE EmployeeID IN (SELECT * FROM @employeesDelete)

	DELETE FROM Employees
	WHERE DepartmentID = @departmentId

	DELETE FROM Departments
	WHERE DepartmentID = @departmentId

	SELECT COUNT(*)
	FROM Employees
	WHERE DepartmentID = @departmentId
END

------------

EXEC usp_DeleteEmployeesFromDepartment 1

SELECT * FROM Departments

--8

--PART 2 QUERIES FOR BANK DATABASE

CREATE OR ALTER PROCEDURE usp_GetHoldersFullName
AS
	SELECT CONCAT_WS(' ', FirstName, LastName)
	FROM AccountHolders

EXEC dbo.usp_GetHoldersFullName

--9

CREATE OR ALTER PROCEDURE usp_GetHoldersWithBalanceHigherThan
@numberParameter MONEY
AS
	SELECT ah.FirstName, ah.LastName
	FROM AccountHolders ah
	LEFT JOIN Accounts a ON ah.Id = a.AccountHolderId
	GROUP BY ah.Id, ah.FirstName, ah.LastName
	HAVING SUM(a.Balance) > @numberParameter
	ORDER BY FirstName, LastName

----------------------------------------

EXEC usp_GetHoldersWithBalanceHigherThan 2100

--10

CREATE OR ALTER FUNCTION ufn_CalculateFutureValue(@Sum DECIMAL(18,2), @Rate FLOAT, @Years INT)
RETURNS DECIMAL(20,4)
AS
BEGIN
	RETURN @Sum * POWER((1 + @Rate), @Years)
END

--𝐹𝑉=𝐼×((1+𝑅)𝑇)
---------------

SELECT dbo.ufn_CalculateFutureValue(1000, .1, 5)