SELECT COUNT(*)
FROM WizzardDeposits

--1

SELECT TOP(1) MagicWandSize AS LongestMagicWand
FROM WizzardDeposits
ORDER BY MagicWandSize DESC


SELECT MAX(MagicWandSize) AS LongestMagicWand
FROM WizzardDeposits

--2

SELECT 
DepositGroup,
MAX(MagicWandSize) AS LongestMagicWand
FROM WizzardDeposits
GROUP BY DepositGroup

--3

SELECT TOP(2)
DepositGroup
FROM WizzardDeposits
GROUP BY DepositGroup
ORDER BY AVG(MagicWandSize)

--4

SELECT 
DepositGroup,
SUM(DepositAmount) AS TotalSum
FROM WizzardDeposits
GROUP BY DepositGroup

--5

SELECT 
DepositGroup,
SUM(DepositAmount) AS TotalSum
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup

--6

SELECT * FROM
(SELECT 
DepositGroup,
SUM(DepositAmount) AS TotalSum
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup) AS subq
WHERE subq.TotalSum < 150000
ORDER BY subq.TotalSum DESC

--7

SELECT
w.DepositGroup,
w.MagicWandCreator,
MIN(DepositCharge) AS MinDepositCharge
FROM WizzardDeposits as w
GROUP BY w.DepositGroup, w.MagicWandCreator
ORDER BY w.MagicWandCreator, w.DepositGroup

--8

SELECT 
AgeGroup,
COUNT(AgeGroup) AS WizardCount
FROM 
(
	SELECT 
		CASE 
			WHEN [Age] <= 10 THEN '[0-10]'
			WHEN [Age] > 10 AND [Age] <= 20 THEN '[11-20]'
			WHEN [Age] > 20 AND [Age] <= 30 THEN '[21-30]'
			WHEN [Age] > 30 AND [Age] <= 40 THEN '[31-40]'
			WHEN [Age] > 40 AND [Age] <= 50 THEN '[41-50]'
			WHEN [Age] > 50 AND [Age] <= 60 THEN '[51-60]'
			ELSE '[61+]'
		END AS [AgeGroup]
		FROM [WizzardDeposits]
) as a
group by AgeGroup

--9

SELECT DISTINCT LEFT(FirstName, 1) AS FirstLetter
FROM WizzardDeposits
WHERE DepositGroup = 'Troll Chest'
ORDER BY FirstLetter


SELECT
DISTINCT FirstLetter
FROM
(
	SELECT SUBSTRING(FirstName, 1, 1) AS FirstLetter
	FROM WizzardDeposits
	WHERE DepositGroup = 'Troll Chest'
) AS letters
ORDER BY FirstLetter

--10

SELECT
DepositGroup,
IsDepositExpired,
AVG(DepositInterest) AS AverageInterest
FROM WizzardDeposits
WHERE DepositStartDate > '1985-01-01'
GROUP BY DepositGroup, IsDepositExpired
ORDER BY DepositGroup DESC, IsDepositExpired ASC 

--11

select
	sum(w1.[DepositAmount] - w2.[DepositAmount]) as [SumDifference]
from [WizzardDeposits] as [w1]
left join [WizzardDeposits] as [w2]
	on w1.[Id] = w2.[Id] - 1

--12

--PART 2 QUERIES FOR SOFTUNI DATABASE

SELECT 
DepartmentID,
SUM(Salary) AS TotalSalary
FROM Employees
GROUP BY DepartmentID

--13

SELECT 
DepartmentID,
MIN(Salary) AS MinimumSalary
FROM Employees
WHERE DepartmentID IN (2,5,7) AND HireDate > '2000-01-01'
GROUP BY DepartmentID

--14

SELECT * INTO EmployeesNew
FROM Employees
WHERE Salary > 30000

DELETE
FROM EmployeesNew
WHERE ManagerID = 42

UPDATE EmployeesNew
SET Salary = Salary + 5000
WHERE DepartmentID = 1

SELECT DepartmentID, AVG(Salary) AS AverageSalary
FROM EmployeesNew
GROUP BY DepartmentID

DELETE EmployeesNew

--15

SELECT 
DepartmentID,
MAX(Salary) AS MaxSalary
FROM Employees
GROUP BY DepartmentID
HAVING MAX(Salary) NOT BETWEEN 30000 AND 70000

--16

SELECT 
COUNT(EmployeeID) AS [Count]
FROM Employees
WHERE ManagerID IS NULL

--17

SELECT DepartmentID,
MAX(Salary) AS ThirdHighestSalary
FROM
(
	SELECT DepartmentID, Salary,
	DENSE_RANK() OVER (PARTITION BY DepartmentID ORDER BY Salary DESC) AS [Rank]
	FROM Employees
) AS t
WHERE [Rank] = 3
GROUP BY DepartmentID

--18