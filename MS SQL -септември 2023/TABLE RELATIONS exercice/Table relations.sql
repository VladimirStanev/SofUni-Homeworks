CREATE TABLE Passports(
	PassportID INT PRIMARY KEY IDENTITY(101, 1),
	PassportNumber NVARCHAR(20)
)

CREATE TABLE Persons(
	PersonID INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(50),
	Salary DECIMAL(8,2),
	PassportID INT UNIQUE FOREIGN KEY REFERENCES Passports(PassportID)
)

INSERT INTO Passports(PassportNumber)
VALUES('N34FG21B'),
	('K65LO4R7'),
	('ZE657QP2')


INSERT INTO Persons(FirstName, Salary, PassportID)
VALUES('Roberto', 43300.00, 102),
	('Tom', 56100.00, 103),
	('Yana', 60200.00, 101)

UPDATE Persons
SET FirstName = 'Yana'
WHERE PersonID = 3

ALTER TABLE Persons
ADD UNIQUE(PassportID)

--1

CREATE TABLE Manufacturers(
	ManufacturerID INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL,
	EstablishedOn DATETIME2
)

CREATE TABLE Models(
	ModelID INT PRIMARY KEY IDENTITY(101, 1),
	[Name] NVARCHAR(50) NOT NULL,
	ManufacturerID INT FOREIGN KEY REFERENCES Manufacturers(ManufacturerID)
)

INSERT INTO Manufacturers([Name], EstablishedOn)
VALUES('BMW', '07/03/1916'),
	('Tesla', '01/01/2003'),
	('Lada', '01/05/1966')

INSERT INTO Models([Name], ManufacturerID)
VALUES('X1', 1),
	('i6', 1),
	('Model S', 2),
	('Model X', 2),
	('Model 3', 2),
	('Nova', 3)

--2

CREATE TABLE Students(
	StudentID INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Exams(
	ExamID INT PRIMARY KEY IDENTITY(101, 1),
	[Name] VARCHAR(50) NOT NULL
)

CREATE TABLE StudentsExams(
	StudentID INT,
	ExamID INT,
	PRIMARY KEY(StudentID, ExamID),
	FOREIGN KEY(StudentID) REFERENCES Students(StudentID),
	FOREIGN KEY(ExamID) REFERENCES Exams(ExamID)
)

INSERT INTO Students([Name])
VALUES('Mila'),
	('Toni'),
	('Ron')

INSERT INTO Exams([Name])
VALUES('SpringMVC'),
	('Neo4j'),
	('Oracle 11g')

INSERT INTO StudentsExams(StudentID, ExamID)
VALUES(1, 101),
	(1, 102),
	(2, 101),
	(3, 103),
	(2, 102),
	(2, 103)

--3

CREATE TABLE Teachers(
	TeacherID INT PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL,
	ManagerID INT REFERENCES Teachers(TeacherID)
)

INSERT INTO Teachers(TeacherID, [Name], ManagerID)
VALUES(101, 'John', NULL),
(102, 'Maya', 106),
(103, 'Silvia', 106),
(104, 'Ted', 105),
(105, 'Mark', 101),
(106, 'Greta', 101)

--4



--5

CREATE TABLE Majors(
	MajorID INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) NOT NULL
)

CREATE TABLE Subjects(
	SubjectID INT PRIMARY KEY IDENTITY,
	SubjectName VARCHAR(50) NOT NULL
)

CREATE TABLE Students(
	StudentID INT PRIMARY KEY,
	StudentNumber INT UNIQUE NOT NULL,
	StudentName NVARCHAR(50) NOT NULL,
	MajorID INT FOREIGN KEY REFERENCES Majors(MajorID) NOT NULL
)

CREATE TABLE Agenda(
	StudentID INT FOREIGN KEY REFERENCES Students(StudentID),
	SubjectID INT FOREIGN KEY REFERENCES Subjects(SubjectID),
	PRIMARY KEY(StudentID, SubjectID)
)

CREATE TABLE Payments(
	PaymentID INT PRIMARY KEY,
	PaymentDate DATETIME2,
	PaymentAmount DECIMAL(8,2),
	StudentID INT FOREIGN KEY REFERENCES Students(StudentID)
)

--6

SELECT m.MountainRange, PeakName, Elevation FROM Peaks AS p
JOIN Mountains AS m on m.Id = p.MountainId
WHERE MountainId = (SELECT Id FROM Mountains
					WHERE MountainRange = 'Rila')
ORDER BY Elevation DESC

--9 PEAKS in RILA from Geography DataBase