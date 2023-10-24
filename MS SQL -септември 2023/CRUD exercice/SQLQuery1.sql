SELECT * FROM Departments
--02

SELECT Name
FROM Departments
--03

SELECT FirstName, LastName, Salary
FROM Employees
--04

SELECT FirstName, MiddleName, LastName
FROM Employees
--05

SELECT FirstName + '.' + LastName + '@softuni.bg' AS [Full Email Address]
FROM Employees
--06

SELECT DISTINCT Salary AS [Salary]
FROM Employees
--07

SELECT * FROM Employees
WHERE JobTitle = 'Sales Representative'
--08

SELECT FirstName, LastName, JobTitle
FROM Employees
WHERE Salary BETWEEN 20000 AND 30000
--09

SELECT CONCAT_WS(' ', FirstName, MiddleName, LastName) AS [Full Name]
FROM Employees
WHERE Salary IN(25000, 14000, 12500, 23600)
--10

SELECT FirstName, LastName
FROM Employees
WHERE ManagerID IS NULL
--11

SELECT FirstName, LastName, Salary
FROM Employees
WHERE Salary > 50000
ORDER BY Salary DESC
--12

SELECT TOP(5) FirstName, LastName
FROM Employees
ORDER BY Salary DESC
--13

SELECT FirstName, LastName
FROM Employees
WHERE DepartmentID <> 4
--14

SELECT *
FROM Employees
ORDER BY Salary DESC, FirstName, LastName DESC, MiddleName
--15