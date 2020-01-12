
--Tabelle Erstellen
CREATE TABLE Employee(
	Id INT PRIMARY KEY IDENTITY(1,1),
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	BirthDay DATETIME NOT NULL,
	UserName NVARCHAR(50) NOT NULL,
	UserPassword NVARCHAR(255) NOT NULL,
)

CREATE TABLE VacationRequest(
	Id INT PRIMARY KEY IDENTITY(1,1),
	EmployeeId INT NOT NULL FOREIGN KEY REFERENCES Employee(Id),
	FromDate DATETIME NOT NULL,
	UntilDate DATETIME NOT NULL,
	VacationType NVARCHAR(24) NOT NULL, --PAYED_VACATION UNPAYED_VACATION
	CreatedOn DATETIME NOT NULL DEFAULT(GETDATE()),
)

--Daten in die Tabelle schreiben
INSERT INTO Employee (FirstName, LastName, BirthDay, UserName, UserPassword) 
VALUES 
('Scarlet', 'Hayes', '1989-04-04', 'HayesS', 'SuperPassword21'), 
('Gordon', 'Freeman', '1989-04-04', 'FreemanG', 'SuperPassword21'), 
('Vader', 'Williams', '1989-04-04', 'WilliamsV', 'SuperPassword21') 
--Alle Daten der Tabelle anzeigen
SELECT * FROM Employee
WHERE FirstName LIKE '%mi%' OR LastName LIKE '%un%'
--Filtern
SELECT * FROM Employee WHERE LastName = 'Patuk'	OR LastName = 'LastName2'
--UPDATE TabellenName SET Spalte1 = 'Wert1', Spalte2 = Wert2 WHERE SpalteX = FilterX AND/OR SpalteY=FilterY
UPDATE Employee SET FirstName = 'Pearl', LastName = 'Cook', UserName = 'CookP' 
--DELETE FROM Tabelle WHERE Spalte1 =>< Wert1 AND/OR
DELETE FROM Employee WHERE Id > 5

SELECT * FROM VacationRequest
INSERT INTO VacationRequest (EmployeeId, FromDate, UntilDate, VacationType)	
VALUES (25, '2020-01-05', '2020-01-09', 'PAYED_VACATION')

 SELECT * FROM Employee as e
 LEFT JOIN VacationRequest as v
 ON e.Id = v.EmployeeId
 WHERE e.Id = 2







