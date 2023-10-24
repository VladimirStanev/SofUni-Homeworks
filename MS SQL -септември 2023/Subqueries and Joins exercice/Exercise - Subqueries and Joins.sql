-- Part 1 >>>>> SOFTUNI DATABASE

SELECT TOP(5)
EmployeeID,
JobTitle,
a.AddressID,
a.AddressText
FROM Employees AS e JOIN Addresses AS a ON a.AddressID = e.AddressID
ORDER BY AddressID

--1

SELECT TOP(50)
FirstName,
LastName,
t.Name AS Town,
a.AddressText
FROM Employees AS e
JOIN Addresses AS a ON e.AddressID = a.AddressID
JOIN Towns as t ON a.TownID = t.TownID
ORDER BY FirstName, LastName

--2

SELECT 
EmployeeID,
FirstName,
LastName,
d.Name AS DepartmentName
FROM Employees e 
JOIN Departments d ON d.DepartmentID = e.DepartmentID
WHERE d.Name = 'Sales'
ORDER BY EmployeeID

--3

SELECT TOP(5)
EmployeeID,
FirstName,
Salary,
d.Name AS DepartmentName
FROM Employees e
JOIN Departments d ON e.DepartmentID = d.DepartmentID
WHERE Salary > 15000
ORDER BY d.DepartmentID

--4

SELECT TOP(3)
e.EmployeeID,
FirstName
FROM Employees AS e
WHERE e.EmployeeID NOT IN (SElECT EmployeeID FROM EmployeesProjects)
ORDER BY EmployeeID

--5

SELECT
FirstName,
LastName,
HireDate,
d.Name AS DeptName
FROM Employees AS e
JOIN Departments AS d ON d.DepartmentID = e.DepartmentID
WHERE HireDate > '1.1.1999' AND d.Name IN ('Sales', 'Finance')
ORDER BY HireDate

--6

SELECT TOP(5)
e.EmployeeID,
FirstName,
p.Name AS ProjectName
FROM Employees e
JOIN EmployeesProjects ep ON ep.EmployeeID = e.EmployeeID
JOIN Projects p ON p.ProjectID = ep.ProjectID
WHERE p.StartDate > '2002-08-13' AND p.EndDate IS NULL
ORDER BY e.EmployeeID

--7

SELECT
e.EmployeeID,
FirstName,
	CASE
		WHEN p.StartDate > '2004-12-31' THEN NULL
		ELSE p.Name
	END ProjectName
FROM Employees e
JOIN EmployeesProjects ep ON ep.EmployeeID = e.EmployeeID
JOIN Projects p ON p.ProjectID = ep.ProjectID
WHERE e.EmployeeID = 24

--8

SELECT e.EmployeeID, e.FirstName, e.ManagerID, m.FirstName AS ManagerName
FROM Employees e
JOIN Employees m ON e.ManagerID = m.EmployeeID
WHERE e.ManagerID IN (3,7)
ORDER BY e.EmployeeID

--9

SELECT TOP(50)
e.EmployeeID,
CONCAT_WS(' ', e.FirstName, e.LastName) AS EmployeeName,
CONCAT_WS(' ', m.FirstName, m.LastName) AS ManagerName,
d.Name AS DepartmentName
FROM Employees e
JOIN Employees m ON e.ManagerID = m.EmployeeID
JOIN Departments d ON e.DepartmentID = d.DepartmentID
ORDER BY e.EmployeeID

--10

SELECT TOP(1)
AVG(e.Salary) AS MinAverageSalary
FROM Employees e
JOIN Departments d ON d.DepartmentID = e.DepartmentID
GROUP BY d.[Name]
ORDER BY MinAverageSalary

--11

-- Part 2 >>>>> GEOGRAPHY DATABASE

SELECT 
c.CountryCode,
m.MountainRange,
p.PeakName,
p.Elevation
FROM Countries c
JOIN MountainsCountries mc ON mc.CountryCode = c.CountryCode
JOIN Mountains m ON m.Id = mc.MountainId
JOIN Peaks p ON p.MountainId = m.Id
WHERE c.CountryCode = 'BG' AND p.Elevation > 2835
ORDER BY p.Elevation DESC

--12

SELECT 
mc.CountryCode,
COUNT(m.MountainRange) AS MountainRanges
FROM Mountains m
JOIN MountainsCountries mc ON m.Id = mc.MountainId
GROUP BY mc.CountryCode
HAVING mc.CountryCode IN ('BG', 'RU', 'US')
ORDER BY MountainRanges DESC

--13

SELECT TOP(5)
c.CountryName,
r.RiverName
FROM Countries c
LEFT JOIN CountriesRivers cr ON cr.CountryCode = c.CountryCode
LEFT JOIN Rivers r ON r.Id = cr.RiverId
WHERE c.ContinentCode = 'AF'
ORDER BY CountryName

--14